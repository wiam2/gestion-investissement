from typing import Dict, Text, Tuple

import numpy as np
import tensorflow as tf
import pandas as pd
import tensorflow_datasets as tfds
import tensorflow_recommenders as tfrs
# Ratings data.
ratings_df= pd.read_csv("likes.csv")

postes_df= pd.read_csv("postes_startup.csv")

ratings = tf.data.Dataset.from_tensor_slices(dict(ratings_df))
postes = tf.data.Dataset.from_tensor_slices(dict(postes_df))

# Select the basic features.
ratings = ratings.map(lambda x: {
    "Titre": x["Titre"],
    "ID_User": x["ID_User"],
    "ID_Poste": x["ID_Poste"]
})
postes = postes.map(lambda x: {
    "Titre": x["Titre"],
    "ID_Poste": x["ID_Poste"]
})
user_ids_vocabulary = tf.keras.layers.StringLookup(mask_token=None)
user_ids_vocabulary.adapt(ratings.map(lambda x: x["ID_User"]))

poste_titles_vocabulary = tf.keras.layers.StringLookup(mask_token=None)
poste_titles_vocabulary.adapt(postes.map(lambda x: x["Titre"]))

class PosteModel(tfrs.Model):
  # We derive from a custom base class to help reduce boilerplate. Under the hood,
  # these are still plain Keras Models.

  def __init__(
      self,
      user_model: tf.keras.Model,
      poste_model: tf.keras.Model,
      task: tfrs.tasks.Retrieval):
    super().__init__()

    # Set up user and movie representations.
    self.user_model = user_model
    self.poste_model = poste_model

    # Set up a retrieval task.
    self.task = task

  def compute_loss(self, features: Dict[Text, tf.Tensor], training=False) -> tf.Tensor:
    # Define how the loss is computed.

    user_embeddings = self.user_model(features["ID_User"])
    poste_embeddings = self.poste_model(features["Titre"])

    return self.task(user_embeddings, poste_embeddings)
 
  def call(self, features: Dict[Text, tf.Tensor], training=False) -> Tuple[tf.Tensor, tf.Tensor]:
        user_embeddings = self.user_model(features["ID_User"])
        poste_embeddings = self.poste_model(features["Titre"])
        return self.task(user_embeddings, poste_embeddings), features["ID_Poste"] 
  
  # Define user and movie models.
user_model = tf.keras.Sequential([
    user_ids_vocabulary,
    tf.keras.layers.Embedding(user_ids_vocabulary.vocab_size(), 64)
])
poste_model = tf.keras.Sequential([
    poste_titles_vocabulary,
    tf.keras.layers.Embedding(poste_titles_vocabulary.vocab_size(), 64)
])


task = tfrs.tasks.Retrieval(
    metrics=tfrs.metrics.FactorizedTopK(
        postes.map(lambda x: x["Titre"]).batch(128).map(poste_model) 
    )
)

# Séparation des données en train et test
trainset_size = 0.8 * ratings .__len__().numpy()
# Randomly shuffle data and split between train and test.
tf.random.set_seed(42)
ratings_shuffled = ratings.shuffle(
    buffer_size=100_000,
    seed=42,
    reshuffle_each_iteration=False
)

train = ratings_shuffled.take(trainset_size)
test = ratings_shuffled.skip(trainset_size)

# Create a retrieval model.
model = PosteModel(user_model, poste_model, task)
model.compile(optimizer=tf.keras.optimizers.Adagrad(0.5))

# Train for 3 epochs.
model.fit(train.batch(4096), epochs=20)
model.evaluate(test.batch(50), return_dict=True)


index = tfrs.layers.factorized_top_k.BruteForce(model.user_model)


index.index_from_dataset(
    test.map(lambda x: (x["Titre"], x["ID_Poste"])).batch(100).map(model.poste_model)
)
_, indices = index(tf.constant(["IDO1"]))
indices_1d = indices.numpy().reshape(-1)

# Récupérer les ID des postes correspondant aux indices recommandés
recommended_post_ids = postes_df.iloc[indices_1d]["ID_Poste"].tolist()

print(f"Top 3 recommandations pour l'utilisateur 43: {recommended_post_ids[:20]}")


save_path = '/SystemeRec/model_test'

index.save(save_path, save_format='tf')