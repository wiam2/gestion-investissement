from typing import Dict, Text, Tuple

import numpy as np
import tensorflow as tf
import pandas as pd
import tensorflow_datasets as tfds
import tensorflow_recommenders as tfrs

postes_df= pd.read_csv("postes_startup.csv")

loaded_model = tf.keras.models.load_model(r'D:\Users\SystemeRecommandationSave-20240503T174711Z-001\SystemeRecommandationSave')

id_user="IDO1"
_, titles = loaded_model(tf.constant([id_user]))
print(titles)
indices_1d = titles.numpy().reshape(-1)

# Récupérer les ID des postes correspondant aux indices recommandés
recommended_post_ids = postes_df.iloc[indices_1d]["ID_Poste"].tolist()

print(f"Top 3 recommandations pour l'utilisateur {id_user}: {recommended_post_ids}")