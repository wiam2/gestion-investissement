from flask import Flask, jsonify
import requests
import tensorflow as tf
import pandas as pd
app = Flask(__name__)

postes_df= pd.read_csv("postes_startup.csv")

loaded_model = tf.keras.models.load_model(r'D:\Users\SystemeRecommandationSave-20240503T174711Z-001\SystemeRecommandationSave')

@app.route('/')
def get_posts():
    return "hello world !"

    # # Faites une requête HTTP pour récupérer les données depuis l'API .NET
    # response = requests.get('http://localhost:5198/api/PosteInv/PostesInvestisseurZero')

    # # Vérifiez si la requête a réussi (statut 200)
    # if response.status_code == 200:
    #     # Convertissez la réponse en JSON et renvoyez-la
    #     return jsonify(response.json())
    # else:
    #     # Si la requête a échoué, renvoyez un message d'erreur
    #     return jsonify({'error': 'Failed to fetch posts from .NET API'}), 500

@app.route('/ReturnStartupPostes/<string:id>')
def send_StartupPosts(id):
    print(id)

    # Utilisez l'ID récupéré de l'URL pour obtenir les recommandations
    _, titles = loaded_model(tf.constant([id]))

    indices_1d = titles.numpy().reshape(-1)

    # Récupérer les ID des postes correspondant aux indices recommandés
    recommended_post_ids = postes_df.iloc[indices_1d]["ID_Poste"].tolist()

    # Retournez les ID sous forme JSON
    return jsonify({"recommended_post_ids": recommended_post_ids})
@app.route('/ReturnInvPostes/<string:id>')
def send_InvPosts(id):
    print(id)

    # Utilisez l'ID récupéré de l'URL pour obtenir les recommandations
    _, titles = loaded_model(tf.constant([id]))

    indices_1d = titles.numpy().reshape(-1)

    # Récupérer les ID des postes correspondant aux indices recommandés
    recommended_post_ids = postes_df.iloc[indices_1d]["ID_Poste"].tolist()

    # Retournez les ID sous forme JSON
    return jsonify({"recommended_post_ids": recommended_post_ids})

if __name__ == '__main__':
    app.run(debug=True)
    
