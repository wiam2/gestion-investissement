import csv
import random
import string
from datetime import datetime, timedelta

titres_et_descriptions = {
    "Développement de la prochaine génération de batteries écologiques": "Rejoignez-nous dans notre mission pour révolutionner l'industrie des batteries avec des solutions écologiques et durables.",
    "Innovation dans le domaine de la santé numérique": "Notre startup développe des solutions de santé numérique innovantes pour améliorer l'accès aux soins de santé et optimiser la gestion des données médicales.",
    "Technologie de pointe pour la gestion des déchets plastiques": "Nous utilisons la technologie de pointe pour transformer les déchets plastiques en ressources précieuses, contribuant ainsi à un avenir plus propre et plus durable.",
    "Révolution dans l'agriculture urbaine durable": "Rejoignez notre startup dans notre quête pour révolutionner l'agriculture urbaine en développant des solutions durables pour nourrir les villes de demain.",
    "Innovation dans les transports électriques autonomes": "Notre startup développe des solutions de transport électrique autonomes pour rendre les déplacements plus écologiques, efficaces et sûrs.",
    "Biotechnologie pour la santé et le bien-être": "Nous sommes engagés dans la recherche et le développement de traitements biotechnologiques novateurs pour améliorer la santé et le bien-être de tous.",
    "Réinvention de l'industrie de la mode avec des matériaux durables": "Rejoignez-nous dans notre mission pour réinventer l'industrie de la mode en utilisant des matériaux durables et des pratiques éthiques.",
    "Technologie blockchain pour la finance décentralisée": "Notre startup utilise la technologie blockchain pour créer des solutions de finance décentralisée, offrant ainsi plus d'inclusion et de transparence financière.",
    "Intelligence artificielle pour la gestion de l'énergie": "Nous développons des solutions basées sur l'intelligence artificielle pour optimiser la gestion de l'énergie et favoriser la transition vers un avenir énergétique durable.",
    "Innovation dans les technologies de l'eau propre": "Rejoignez-nous dans notre mission pour développer des technologies de traitement de l'eau propres et durables, assurant un accès à l'eau potable pour tous.",
    "Technologie de recyclage avancée pour les déchets électroniques": "Nous utilisons une technologie de recyclage avancée pour récupérer et recycler efficacement les déchets électroniques, contribuant ainsi à réduire la pollution et à préserver les ressources naturelles.",
    "Plateforme d'apprentissage automatique pour la personnalisation des soins de santé": "Notre plateforme utilise l'apprentissage automatique pour analyser les données médicales et fournir des recommandations personnalisées aux patients, améliorant ainsi la qualité des soins de santé.",
    "Innovation dans les technologies de stockage d'énergie à base de graphène": "Nous développons des solutions de stockage d'énergie révolutionnaires basées sur le graphène, offrant des performances supérieures et une durabilité exceptionnelle.",
    "Plateforme de finance participative pour les projets durables": "Rejoignez notre plateforme de finance participative dédiée aux projets durables, permettant à chacun de contribuer à la transition vers une économie plus verte.",
    "Biotechnologie marine pour la découverte de nouveaux médicaments": "Notre startup utilise la biotechnologie marine pour explorer les ressources marines et découvrir de nouveaux médicaments prometteurs pour traiter les maladies.",
    "Réinvention de l'éducation avec des technologies d'apprentissage immersif": "Nous réinventons l'éducation en utilisant des technologies d'apprentissage immersif telles que la réalité virtuelle et augmentée pour offrir des expériences d'apprentissage plus engageantes et efficaces.",
    "Innovation dans les matériaux de construction écologiques": "Rejoignez-nous dans notre mission pour développer des matériaux de construction écologiques innovants qui réduisent l'empreinte carbone de l'industrie de la construction.",
    "Plateforme de logistique intelligente pour une chaîne d'approvisionnement durable": "Notre plateforme utilise l'intelligence artificielle pour optimiser la logistique et réduire l'impact environnemental de la chaîne d'approvisionnement, favorisant ainsi la durabilité.",
    "Technologie de reconnaissance faciale pour la sécurité et la confidentialité": "Nous développons une technologie de reconnaissance faciale avancée qui garantit à la fois la sécurité et la protection de la vie privée des utilisateurs.",
    "Révolution dans la production alimentaire avec la biotechnologie cellulaire": "Rejoignez notre startup pour révolutionner la production alimentaire en utilisant la biotechnologie cellulaire pour cultiver de la viande et d'autres produits alimentaires sans élevage animal.",
    "Plateforme de gestion des déchets organiques pour une économie circulaire": "Notre plateforme facilite la gestion des déchets organiques en les transformant en ressources précieuses telles que le compost, contribuant ainsi à promouvoir une économie circulaire.",
    "Innovation dans les énergies marines renouvelables": "Nous développons des technologies innovantes pour exploiter l'énergie des océans, y compris les marées et les vagues, afin de fournir une source d'énergie propre et durable.",
    "Technologie de pointe pour la détection précoce du cancer": "Notre startup utilise des technologies de pointe telles que l'imagerie médicale et l'analyse génomique pour détecter le cancer à un stade précoce, améliorant ainsi les chances de guérison.",
    "Plateforme de commerce électronique équitable pour les artisans locaux": "Rejoignez notre plateforme de commerce électronique dédiée à la promotion des produits artisanaux locaux, assurant des conditions équitables pour les artisans et une consommation responsable pour les consommateurs.",
    "Innovation dans les drones agricoles pour l'agriculture de précision": "Nous développons des drones agricoles équipés de capteurs avancés et d'intelligence artificielle pour permettre une agriculture de précision et durable.",
    "Technologie de réalité augmentée pour la formation professionnelle": "Notre startup utilise la réalité augmentée pour offrir une formation professionnelle immersive et interactive, améliorant ainsi l'apprentissage et la rétention des compétences.",
    "Plateforme de santé mentale en ligne pour le bien-être émotionnel": "Rejoignez notre plateforme de santé mentale en ligne qui offre un soutien et des ressources pour améliorer le bien-être émotionnel et la santé mentale.",
    "Innovation dans les véhicules électriques autonomes pour une mobilité durable": "Nous développons des véhicules électriques autonomes intelligents pour offrir une mobilité durable et efficiente dans les villes et au-delà.",
    "Plateforme de gestion de l'énergie domestique pour une consommation responsable": "Notre plateforme permet aux utilisateurs de surveiller et de gérer leur consommation d'énergie domestique, favorisant ainsi une utilisation responsable et efficace de l'énergie.",
    "Technologie de détection des fuites d'eau pour une gestion efficiente des ressources": "Nous utilisons des technologies avancées de détection des fuites d'eau pour prévenir les pertes et promouvoir une gestion plus efficiente des ressources hydriques.",
    "Technologie de traitement des déchets plastiques pour une économie circulaire": "Nous utilisons une technologie de pointe pour convertir les déchets plastiques en matières premières réutilisables, contribuant ainsi à réduire la pollution plastique et à promouvoir une économie circulaire.",
    "Plateforme de crowdfunding pour les projets artistiques et culturels": "Rejoignez notre plateforme de crowdfunding dédiée à soutenir les projets artistiques et culturels, permettant aux artistes et aux créateurs de concrétiser leurs idées créatives.",
    "Innovation dans les textiles durables à base de biomatériaux": "Nous développons des textiles innovants à partir de biomatériaux durables et écologiques, offrant des alternatives aux textiles traditionnels et réduisant l'empreinte environnementale de l'industrie de la mode.",
    "Technologie de purification de l'air pour des environnements intérieurs sains": "Notre startup propose une technologie de purification de l'air avancée pour créer des environnements intérieurs sains et sûrs, réduisant ainsi les risques pour la santé associés à la pollution de l'air intérieur.",
    "Plateforme de partage de compétences pour l'économie du partage": "Rejoignez notre plateforme de partage de compétences qui connecte les personnes ayant des compétences spécifiques avec celles qui cherchent à apprendre, favorisant ainsi l'échange de connaissances et de compétences.",
    "Innovation dans les solutions de stockage d'eau pour les régions arides": "Nous développons des solutions de stockage d'eau innovantes adaptées aux régions arides, aidant les communautés à sécuriser leur approvisionnement en eau et à faire face aux défis de la sécheresse.",
    "Technologie de réalité virtuelle pour la réhabilitation médicale": "Notre startup utilise la réalité virtuelle pour offrir des programmes de réhabilitation médicale personnalisés et efficaces, aidant les patients à récupérer plus rapidement et à améliorer leur qualité de vie.",
    "Plateforme d'analyse de données pour l'amélioration des performances sportives": "Rejoignez notre plateforme d'analyse de données sportives qui utilise l'intelligence artificielle pour fournir des informations précieuses aux athlètes et aux entraîneurs, améliorant ainsi les performances sportives.",
    "Innovation dans les technologies de captage et de stockage du carbone": "Nous développons des technologies innovantes de captage et de stockage du carbone pour lutter contre le changement climatique en réduisant les émissions de dioxyde de carbone dans l'atmosphère.",
    "Plateforme de location de vêtements pour une mode durable": "Rejoignez notre plateforme de location de vêtements qui permet aux consommateurs de louer des vêtements à la mode plutôt que de les acheter, contribuant ainsi à réduire l'empreinte environnementale de l'industrie de la mode.",
    "Technologie de pointe pour la production d'aliments à base de plantes": "Notre startup utilise des technologies de pointe pour développer des aliments à base de plantes innovants qui offrent une alternative durable et nutritive aux produits d'origine animale.",
    "Plateforme de covoiturage électrique pour des déplacements urbains écologiques": "Rejoignez notre plateforme de covoiturage électrique qui encourage les déplacements urbains écologiques en facilitant le partage de trajets en voiture électrique entre les utilisateurs.",
    "Innovation dans les matériaux d'emballage biodégradables": "Nous développons des matériaux d'emballage biodégradables innovants pour réduire les déchets plastiques et promouvoir une consommation responsable et respectueuse de l'environnement.",
    "Technologie de surveillance environnementale pour la conservation de la biodiversité": "Notre startup propose une technologie de surveillance environnementale avancée pour surveiller et protéger la biodiversité, contribuant ainsi à la conservation des écosystèmes fragiles.",
    "Plateforme de coaching en développement personnel pour une vie épanouie": "Rejoignez notre plateforme de coaching en développement personnel qui offre des ressources et un soutien pour aider les individus à atteindre leurs objectifs de vie et à cultiver un sentiment de bien-être et de satisfaction.",
    "Innovation dans les solutions de recyclage des textiles usagés": "Nous développons des solutions innovantes pour le recyclage des textiles usagés, transformant les vêtements en fin de vie en nouvelles matières premières pour l'industrie textile.",
    "Technologie de bioimpression 3D pour la médecine régénérative": "Notre startup utilise la bioimpression 3D pour créer des tissus et des organes humains sur mesure, ouvrant de nouvelles possibilités dans le domaine de la médecine régénérative et de la transplantation d'organes.",
    "Plateforme de crowdfunding immobilier pour l'investissement participatif": "Rejoignez notre plateforme de crowdfunding immobilier qui permet aux investisseurs de participer à des projets immobiliers prometteurs, offrant ainsi des opportunités d'investissement accessibles et transparentes.",
    "Innovation dans les technologies de désalinisation pour l'accès à l'eau potable": "Nous développons des technologies de désalinisation innovantes pour fournir un accès à l'eau potable dans les régions où l'eau douce est rare, contribuant ainsi à résoudre les problèmes de pénurie d'eau.",
    "Plateforme de formation en ligne pour le développement des compétences numériques": "Rejoignez notre plateforme de formation en ligne qui offre des cours et des ressources pour aider les individus à développer leurs compétences numériques et à s'adapter à l'économie numérique en évolution.",
    "Plateforme d'analyse de données pour l'optimisation des processus industriels": "Rejoignez notre plateforme d'analyse de données qui utilise l'apprentissage automatique pour optimiser les processus industriels, améliorant ainsi l'efficacité opérationnelle et réduisant les coûts de production.",
    "Technologie de pointe pour la capture et la valorisation du CO2 atmosphérique": "Notre startup développe une technologie de pointe pour capturer et valoriser le dioxyde de carbone atmosphérique, offrant ainsi une solution innovante pour lutter contre le changement climatique.",
    "Plateforme de prêt entre pairs pour le financement de projets durables": "Rejoignez notre plateforme de prêt entre pairs qui connecte les emprunteurs cherchant à financer des projets durables avec des prêteurs intéressés par l'investissement dans des initiatives à impact positif.",
    "Innovation dans les solutions de stockage d'énergie à base de batteries": "Nous développons des solutions de stockage d'énergie à base de batteries avancées pour répondre à la demande croissante en énergie renouvelable et soutenir la transition vers un réseau électrique décarboné.",
    "Plateforme de vente en ligne de produits écologiques et durables": "Rejoignez notre plateforme de vente en ligne dédiée à la commercialisation de produits écologiques et durables, offrant aux consommateurs une alternative responsable aux produits conventionnels.",
    "Technologie de pointe pour la détection précoce des maladies cardiaques": "Notre startup utilise des technologies de pointe telles que l'imagerie médicale et l'intelligence artificielle pour la détection précoce des maladies cardiaques, améliorant ainsi les chances de traitement et de récupération des patients.",
    "Plateforme de location de voitures électriques pour des déplacements urbains verts": "Rejoignez notre plateforme de location de voitures électriques qui offre une alternative écologique à la possession de véhicules privés, favorisant ainsi des modes de déplacement plus durables en milieu urbain.",
    "Innovation dans les matériaux de construction écologiques et recyclables": "Nous développons des matériaux de construction écologiques et recyclables qui réduisent l'empreinte carbone des projets de construction et contribuent à la durabilité du secteur de la construction.",
    "Technologie de télémédecine pour l'accès aux soins de santé en zone rurale": "Notre startup propose une technologie de télémédecine qui permet aux populations rurales d'accéder à des soins de santé de qualité à distance, réduisant ainsi les barrières géographiques à l'accès aux services médicaux.",
    "Plateforme d'investissement participatif dans les projets d'énergies renouvelables": "Rejoignez notre plateforme d'investissement participatif dédiée au financement de projets d'énergies renouvelables tels que les parcs solaires et éoliens, permettant aux investisseurs de soutenir la transition vers une énergie propre.",
    "Innovation dans les solutions de purification de l'eau pour les communautés rurales": "Nous développons des solutions de purification de l'eau abordables et durables pour les communautés rurales qui n'ont pas accès à l'eau potable, améliorant ainsi la santé et le bien-être des populations locales.",
    "Plateforme de partage de ressources pour les petites entreprises": "Rejoignez notre plateforme de partage de ressources qui permet aux petites entreprises de partager des équipements, des espaces de travail et des compétences, favorisant ainsi la collaboration et la croissance économique locale.",
    "Technologie de pointe pour l'agriculture de précision et la gestion des ressources naturelles": "Notre startup utilise des technologies de pointe telles que l'Internet des objets (IoT) et l'analyse de données pour mettre en œuvre des pratiques agricoles de précision, améliorant ainsi l'efficacité de la production agricole et la gestion durable des ressources naturelles.",
    "Plateforme de financement participatif pour les initiatives sociales et solidaires": "Rejoignez notre plateforme de financement participatif dédiée à soutenir les initiatives sociales et solidaires telles que les projets communautaires, les programmes éducatifs et les actions humanitaires, permettant à chacun de contribuer à des causes qui lui tiennent à cœur.",
    "Innovation dans les technologies de recyclage des plastiques en océan": "Nous développons des technologies innovantes pour collecter et recycler les déchets plastiques dans les océans, contribuant ainsi à réduire la pollution plastique et à protéger les écosystèmes marins fragiles.",
    "Technologie de surveillance de la qualité de l'air pour les villes intelligentes": "Notre startup propose une technologie de surveillance de la qualité de l'air basée sur des capteurs IoT pour aider les villes à surveiller et à gérer la pollution atmosphérique, améliorant ainsi la santé et le bien-être des habitants.",
    "Plateforme de financement participatif pour les projets artistiques et culturels": "Rejoignez notre plateforme de financement participatif dédiée à soutenir les projets artistiques et culturels tels que les films indépendants, les expositions d'art et les festivals de musique, offrant aux créateurs une alternative de financement accessible et transparente.",
    "Technologie de réalité augmentée pour l'apprentissage interactif": "Notre startup utilise la réalité augmentée pour créer des expériences d'apprentissage interactives et immersives, offrant aux utilisateurs une nouvelle façon d'explorer le monde et d'acquérir des connaissances.",
    "Plateforme de partage de repas pour lutter contre le gaspillage alimentaire": "Rejoignez notre plateforme de partage de repas qui permet aux restaurants, supermarchés et particuliers de partager les excédents alimentaires avec ceux qui en ont besoin, réduisant ainsi le gaspillage alimentaire et l'insécurité alimentaire.",
}
# Initialiser le compteur
id_counter = 1

# Fonction pour générer l'identifiant
def generate_id():
    global id_counter
    id_str = f"IDO{id_counter}"
    id_counter += 1
    return id_str
def random_string(length):
    letters = string.ascii_letters
    return ''.join(random.choice(letters) for _ in range(length))

# Fonction pour générer une date aléatoire entre start_date et end_date
def random_datetime(start_datetime, end_datetime):
    delta = end_datetime - start_datetime
    random_seconds = random.randrange(int(delta.total_seconds()))
    random_datetime = start_datetime + timedelta(seconds=random_seconds)
    return random_datetime.isoformat() if random_datetime else None
# Fonction pour générer une chaîne aléatoire
def generate_random_poste_star():
    poste_star_dto = {}
    poste_star_dto['Id'] = random.randint(1, 1000)
    poste_star_dto['IdOwner'] = generate_id()
    
    # Sélectionner aléatoirement un titre parmi les titres significatifs
    titre = random.choice(list(titres_et_descriptions.keys()))
    poste_star_dto['Titre'] = titre
    
    # Utiliser le titre sélectionné pour obtenir la description correspondante
    description = titres_et_descriptions[titre]
    poste_star_dto['Description'] = description
    
    poste_star_dto['DatePoste'] = random_datetime(datetime(2020, 1, 1), datetime(2023, 12, 31))
    poste_star_dto['Montant'] = random.choice([100000,200000,300000,400000,500000,600000,700000,800000,900000])
    poste_star_dto['Secteur'] = random.choice(['Finance', 'Technologie', 'Sante', 'Immobilier', 'energie ', 'Biotechnologie', 'Industrie'])
    poste_star_dto['Status'] = 0
    poste_star_dto['Image'] = random_string(30)
    poste_star_dto['EtapeDev'] = random.choice(['Concept', 'Développement', 'Test', 'Production'])
    poste_star_dto['NumLikes'] = random.randint(0, 100)
    
    return poste_star_dto

# Générer 10 enregistrements aléatoires de postes d'investisseurs
# for _ in range(68):
#     poste_star = generate_random_poste_star()
#     print(poste_star)

# Générer 91 enregistrements aléatoires de postes d'investisseurs
with open('postes_startup.csv', mode='w', newline='') as csvfile:
    fieldnames = ['Id', 'IdOwner', 'Titre', 'Description', 'DatePoste', 'Montant', 'Secteur', 'Status', 'Image', 'EtapeDev', 'NumLikes']
    writer = csv.DictWriter(csvfile, fieldnames=fieldnames)

    writer.writeheader()
    for _ in range(91):
        poste_star_dto = generate_random_poste_star()
        writer.writerow(poste_star_dto)

print("Les données ont été écrites dans le fichier postes_star_dto.csv.")