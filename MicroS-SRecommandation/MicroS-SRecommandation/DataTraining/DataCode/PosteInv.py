import csv
import random
import string
from datetime import datetime, timedelta

titres_et_descriptions = {
    "Investissement dans l'énergie solaire communautaire": "Je suis intéressé à investir dans des projets d'énergie solaire communautaire qui fournissent une source d'énergie propre et renouvelable à nos communautés locales.",
    "Opportunité d'investissement dans les parcs éoliens offshore": "Je cherche à investir dans des parcs éoliens offshore pour soutenir le développement de l'énergie éolienne en mer et contribuer à la transition vers une énergie plus propre.",
    "Proposition d'investissement dans les projets hydroélectriques": "Je suis à la recherche d'opportunités d'investissement dans les projets hydroélectriques qui exploitent la puissance de l'eau pour produire de l'électricité propre et durable.",
    "Investissement dans les technologies agricoles de précision": "Je suis intéressé à investir dans des technologies agricoles de précision qui améliorent l'efficacité et la durabilité de l'agriculture moderne.",
    "Opportunité d'investissement dans les serres hydroponiques": "Je cherche des opportunités d'investissement dans les serres hydroponiques pour soutenir le développement de l'agriculture urbaine et la production alimentaire durable.",
    "Proposition d'investissement dans les drones agricoles": "Je suis à la recherche d'opportunités d'investissement dans les start-ups spécialisées dans les drones agricoles pour aider les agriculteurs à surveiller et à gérer leurs cultures de manière plus efficace.",
    "Investissement dans la biotechnologie médicale": "Je suis intéressé à investir dans des entreprises de biotechnologie médicale qui développent des traitements novateurs pour les maladies graves et les conditions médicales non satisfaites.",
    "Opportunité d'investissement dans la biotechnologie alimentaire": "Je cherche des opportunités d'investissement dans les entreprises de biotechnologie alimentaire qui développent des alternatives durables aux produits alimentaires traditionnels.",
    "Proposition d'investissement dans la bioremédiation environnementale": "Je suis à la recherche d'opportunités d'investissement dans les entreprises de biotechnologie environnementale qui développent des solutions de dépollution pour restaurer et protéger notre environnement.",
    "Investissement dans les plateformes de commerce électronique éthiques": "Je suis intéressé à investir dans des plateformes de commerce électronique qui mettent l'accent sur la durabilité, l'éthique et la transparence dans leurs opérations commerciales.",
    "Opportunité d'investissement dans les applications de livraison éco-responsables": "Je cherche des opportunités d'investissement dans les applications de livraison qui privilégient les pratiques durables et réduisent leur impact environnemental.",
    "Investissement dans les centrales solaires en toiture": "Je suis intéressé à investir dans les centrales solaires en toiture qui utilisent l'espace disponible sur les toits des bâtiments pour générer de l'électricité propre et renouvelable.",
    "Opportunité d'investissement dans les fermes solaires flottantes": "Je cherche des opportunités d'investissement dans les fermes solaires flottantes qui exploitent les plans d'eau pour installer des panneaux solaires et produire de l'énergie verte.",
    "Proposition d'investissement dans les parcs éoliens en mer du Nord": "Je suis à la recherche d'opportunités d'investissement dans les parcs éoliens en mer du Nord qui bénéficient de vents forts et constants pour produire de l'électricité éolienne offshore.",
    "Investissement dans l'agriculture biologique et régénérative": "Je suis intéressé à investir dans l'agriculture biologique et régénérative qui favorise la santé des sols, la biodiversité et la durabilité à long terme.",
    "Opportunité d'investissement dans les cultures hydroponiques verticales": "Je cherche des opportunités d'investissement dans les cultures hydroponiques verticales qui utilisent des tours de culture pour maximiser l'espace et la productivité des cultures.",
    "Proposition d'investissement dans les start-ups de thérapie génique": "Je suis à la recherche d'opportunités d'investissement dans les start-ups de thérapie génique qui développent des traitements révolutionnaires pour les maladies génétiques rares.",
    "Investissement dans les alternatives aux produits laitiers": "Je suis intéressé à investir dans les alternatives aux produits laitiers qui offrent des options sans produits laitiers pour répondre aux besoins des consommateurs soucieux de leur santé et de l'environnement.",
    "Opportunité d'investissement dans les technologies de captage du carbone": "Je cherche des opportunités d'investissement dans les technologies de captage du carbone qui permettent de retirer le dioxyde de carbone de l'atmosphère pour lutter contre le changement climatique.",
    "Proposition d'investissement dans les start-ups de recyclage des batteries": "Je suis à la recherche d'opportunités d'investissement dans les start-ups de recyclage des batteries qui récupèrent les matériaux précieux des batteries usagées pour une économie circulaire et durable.",
    "Investissement dans les initiatives de logistique durable": "Je suis intéressé à investir dans les initiatives de logistique durable qui optimisent les processus de transport et de distribution pour réduire les émissions de carbone et minimiser les déchets.",
    "Proposition d'investissement dans les start-ups de commerce électronique inclusif": "Je suis à la recherche d'opportunités d'investissement dans les start-ups de commerce électronique qui favorisent l'inclusion sociale et économique pour tous les utilisateurs.",
    "Investissement dans les parcs solaires urbains": "Opportunité d'investissement dans les parcs solaires installés en milieu urbain pour promouvoir l'énergie solaire dans les zones peuplées.",
    "Opportunité d'investissement dans les éoliennes domestiques": "Proposition d'investissement dans les éoliennes domestiques pour produire de l'électricité éolienne à petite échelle pour les particuliers.",
    "Proposition d'investissement dans l'agriculture urbaine": "Investissement dans l'agriculture urbaine pour soutenir la production alimentaire locale et la durabilité environnementale en milieu urbain.",
    "Investissement dans les fermes solaires communautaires": "Opportunité d'investissement dans les fermes solaires communautaires pour fournir de l'énergie renouvelable aux collectivités locales.",
    "Opportunité d'investissement dans les technologies agricoles intelligentes": "Proposition d'investissement dans les technologies agricoles intelligentes pour améliorer l'efficacité et la productivité agricoles.",
    "Proposition d'investissement dans la recherche biotechnologique": "Investissement dans la recherche biotechnologique pour développer des traitements innovants pour les maladies et les conditions médicales.",
    "Investissement dans les alternatives végétales à la viande": "Opportunité d'investissement dans les alternatives végétales à la viande pour répondre à la demande croissante de protéines durables et sans cruauté animale.",
    "Opportunité d'investissement dans la restauration des écosystèmes": "Proposition d'investissement dans la restauration des écosystèmes pour protéger la biodiversité et lutter contre le changement climatique.",
    "Investissement dans les initiatives de recyclage des plastiques": "Opportunité d'investissement dans les initiatives de recyclage des plastiques pour réduire la pollution plastique et promouvoir une économie circulaire.",
    "Proposition d'investissement dans les entreprises de logistique verte": "Investissement dans les entreprises de logistique verte pour améliorer l'efficacité et réduire l'empreinte carbone des chaînes d'approvisionnement.",
    "Investissement dans l'énergie solaire urbaine": "Opportunité d'investissement dans l'énergie solaire urbaine pour promouvoir l'adoption de l'énergie propre dans les zones urbaines.",
    "Opportunité d'investissement dans les parcs éoliens côtiers": "Proposition d'investissement dans les parcs éoliens installés le long des côtes pour exploiter l'énergie éolienne en mer.",
    "Proposition d'investissement dans l'hydroélectricité": "Investissement dans l'hydroélectricité pour utiliser la force de l'eau dans la production d'électricité.",
    "Investissement dans les technologies agricoles durables": "Opportunité d'investissement dans les technologies agricoles durables pour soutenir une agriculture respectueuse de l'environnement.",
    "Opportunité d'investissement dans les serres verticales": "Proposition d'investissement dans les serres verticales pour maximiser l'utilisation de l'espace et augmenter la production alimentaire.",
    "Proposition d'investissement dans les biotechnologies médicales": "Investissement dans les biotechnologies médicales pour développer des traitements innovants pour les maladies humaines.",
    "Investissement dans les alternatives végétales": "Opportunité d'investissement dans les alternatives végétales pour répondre à la demande croissante de protéines végétales.",
    "Opportunité d'investissement dans la conservation des forêts": "Proposition d'investissement dans la conservation des forêts pour préserver la biodiversité et les écosystèmes forestiers.",
    "Investissement dans le recyclage des déchets plastiques": "Opportunité d'investissement dans le recyclage des déchets plastiques pour réduire la pollution plastique et promouvoir une économie circulaire.",
    "Proposition d'investissement dans la logistique verte": "Investissement dans la logistique verte pour améliorer l'efficacité et réduire l'empreinte carbone des chaînes d'approvisionnement.",
    "Investissement dans l'énergie solaire résidentielle": "Investissez dans l'énergie solaire pour les maisons afin de réduire les factures d'électricité et de soutenir les énergies renouvelables.",
    "Opportunité d'investissement dans les parcs éoliens terrestres": "Investissez dans les parcs éoliens terrestres pour produire de l'électricité propre et contribuer à la transition énergétique.",
    "Proposition d'investissement dans la production d'hydroélectricité": "Investissez dans la production d'hydroélectricité pour exploiter la force de l'eau et fournir de l'électricité durable.",
    "Investissement dans les technologies agricoles innovantes": "Investissez dans des technologies agricoles innovantes pour augmenter les rendements et réduire l'empreinte environnementale de l'agriculture.",
    "Opportunité d'investissement dans les fermes verticales": "Investissez dans les fermes verticales pour produire des aliments locaux de manière efficace et durable, même dans les zones urbaines.",
    "Proposition d'investissement dans la recherche médicale": "Investissez dans la recherche médicale pour soutenir le développement de nouveaux traitements et améliorer la santé humaine.",
    "Investissement dans les alternatives à la viande": "Investissez dans les alternatives à la viande pour répondre à la demande croissante de produits alimentaires durables et respectueux des animaux.",
    "Opportunité d'investissement dans la conservation marine": "Investissez dans la conservation marine pour protéger les écosystèmes marins fragiles et promouvoir la durabilité des océans.",
    "Proposition d'investissement dans le recyclage des déchets": "Investissez dans le recyclage des déchets pour réduire les déchets et préserver les ressources naturelles.",
    "Investissement dans la mobilité électrique": "Investissez dans la mobilité électrique pour soutenir la transition vers des transports plus propres et réduire les émissions de gaz à effet de serre.",
    "Investissement dans l'énergie solaire domestique": "Investissez dans des installations solaires pour les maisons afin de réduire les coûts énergétiques et soutenir les énergies renouvelables.",
    "Opportunité d'investissement dans les parcs éoliens terrestres": "Investissez dans des parcs éoliens terrestres pour produire de l'électricité verte et contribuer à la transition énergétique.",
    "Proposition d'investissement dans l'hydroélectricité": "Investissez dans des projets hydroélectriques pour exploiter la puissance de l'eau et générer de l'électricité durable.",
    "Investissement dans les technologies agricoles innovantes": "Investissez dans des technologies agricoles innovantes pour accroître les rendements et promouvoir une agriculture plus durable.",
    "Opportunité d'investissement dans les fermes verticales": "Investissez dans des fermes verticales pour produire des aliments locaux de manière efficace et durable, même en milieu urbain.",
    "Proposition d'investissement dans la recherche médicale": "Investissez dans la recherche médicale pour soutenir le développement de nouveaux traitements et améliorer la santé mondiale.",
    "Investissement dans les alternatives à la viande": "Investissez dans des alternatives végétales à la viande pour répondre à la demande croissante de produits alimentaires durables.",
    "Opportunité d'investissement dans la conservation marine": "Investissez dans la conservation marine pour protéger les écosystèmes marins et promouvoir une utilisation durable des océans.",
    "Proposition d'investissement dans le recyclage des déchets": "Investissez dans le recyclage des déchets pour réduire les déchets et promouvoir une économie circulaire.",
    "Investissement dans la mobilité électrique": "Investissez dans la mobilité électrique pour soutenir le développement de véhicules propres et réduire les émissions de carbone.",
    "Investissement dans les panneaux solaires résidentiels": "Investissez dans des panneaux solaires pour les résidences afin de réduire les factures d'électricité et promouvoir l'énergie propre.",
    "Opportunité d'investissement dans les parcs éoliens communautaires": "Investissez dans des parcs éoliens appartenant à la communauté pour soutenir les projets d'énergie renouvelable à l'échelle locale.",
    "Proposition d'investissement dans la gestion des déchets organiques": "Investissez dans des solutions de gestion des déchets organiques pour réduire les déchets alimentaires et produire du compost.",
    "Investissement dans les fermes agricoles verticales": "Investissez dans des fermes agricoles verticales pour maximiser l'utilisation de l'espace et produire des cultures toute l'année.",
    "Opportunité d'investissement dans la santé numérique": "Investissez dans des technologies de santé numérique pour améliorer l'accès aux soins de santé et optimiser la gestion des données médicales.",
    "Proposition d'investissement dans les solutions de stockage d'énergie": "Investissez dans des solutions de stockage d'énergie pour optimiser l'utilisation des énergies renouvelables et stabiliser le réseau électrique.",
    "Investissement dans les start-ups de recyclage des plastiques": "Investissez dans des start-ups innovantes qui transforment les plastiques recyclés en nouveaux produits et matériaux.",
    "Opportunité d'investissement dans les projets de restauration des écosystèmes": "Investissez dans des projets de restauration des écosystèmes pour préserver la biodiversité et restaurer les habitats naturels.",
    "Proposition d'investissement dans l'éducation en ligne": "Investissez dans des plateformes d'éducation en ligne pour promouvoir l'apprentissage à distance et l'accès à l'éducation pour tous.",
    "Investissement dans les solutions de transport partagé": "Investissez dans des solutions de transport partagé pour réduire la congestion urbaine et les émissions de gaz à effet de serre.",
    "Investissement dans les parcs solaires flottants": "Investissez dans des parcs solaires flottants pour exploiter les ressources solaires disponibles sur les plans d'eau et réduire l'empreinte environnementale des installations solaires.",
    "Opportunité d'investissement dans les batteries au lithium": "Investissez dans les technologies de batteries au lithium pour soutenir la transition vers les véhicules électriques et le stockage d'énergie renouvelable.",
    "Proposition d'investissement dans les réseaux électriques intelligents": "Investissez dans les réseaux électriques intelligents pour moderniser l'infrastructure énergétique et faciliter l'intégration des énergies renouvelables.",
    "Investissement dans les solutions de captage et de stockage du carbone": "Investissez dans les technologies de captage et de stockage du carbone pour réduire les émissions de CO2 et atténuer les effets du changement climatique.",
    "Opportunité d'investissement dans les solutions d'efficacité énergétique": "Investissez dans les solutions d'efficacité énergétique pour réduire la consommation d'énergie et les coûts opérationnels des bâtiments et des infrastructures.",
    "Proposition d'investissement dans les fermes d'algues marines": "Investissez dans les fermes d'algues marines pour produire des biocarburants, des produits alimentaires et des matériaux durables à partir de ressources marines renouvelables.",
    "Investissement dans les technologies de stockage thermique": "Investissez dans les technologies de stockage thermique pour optimiser l'utilisation de l'énergie solaire thermique et des sources de chaleur renouvelables.",
    "Opportunité d'investissement dans les projets de régénération des sols": "Investissez dans les projets de régénération des sols pour restaurer la fertilité des sols et améliorer la productivité agricole de manière durable.",
    "Proposition d'investissement dans les initiatives de reforestation": "Investissez dans les initiatives de reforestation pour lutter contre la déforestation, restaurer les écosystèmes et capturer le carbone de l'atmosphère.",
    "Investissement dans les technologies de culture cellulaire": "Investissez dans les technologies de culture cellulaire pour produire de la viande cultivée en laboratoire et des produits alimentaires sans cruauté animale.",
    "Opportunité d'investissement dans les entreprises de recyclage des textiles": "Investissez dans les entreprises de recyclage des textiles pour réduire les déchets textiles et promouvoir l'économie circulaire dans l'industrie de la mode.",
    "Proposition d'investissement dans les start-ups de gestion des déchets électroniques": "Investissez dans les start-ups de gestion des déchets électroniques pour récupérer et recycler les composants électroniques obsolètes de manière écologique.",
    "Investissement dans les fermes urbaines verticales": "Investissez dans les fermes urbaines verticales pour produire des aliments frais localement et maximiser l'utilisation des espaces urbains limités.",
    "Opportunité d'investissement dans les projets d'adaptation au changement climatique": "Investissez dans les projets d'adaptation au changement climatique pour renforcer la résilience des communautés et des infrastructures face aux impacts climatiques.",
    "Proposition d'investissement dans les start-ups de recyclage des déchets plastiques océaniques": "Investissez dans les start-ups de recyclage des déchets plastiques océaniques pour nettoyer les océans et prévenir la pollution plastique.",
    "Investissement dans les technologies de désalinisation de l'eau": "Investissez dans les technologies de désalinisation de l'eau pour fournir de l'eau potable aux régions arides et répondre aux besoins croissants en eau douce.",
    "Opportunité d'investissement dans les projets de restauration des zones humides": "Investissez dans les projets de restauration des zones humides pour préserver la biodiversité, filtrer les eaux usées et atténuer les inondations.",
    "Proposition d'investissement dans les entreprises d'éducation à l'environnement": "Investissez dans les entreprises d'éducation à l'environnement pour sensibiliser le public aux enjeux environnementaux et promouvoir des comportements durables.",
    "Investissement dans les technologies de surveillance de la qualité de l'air": "Investissez dans les technologies de surveillance de la qualité de l'air pour mesurer et réduire la pollution de l'air dans les zones urbaines.",
    "Opportunité d'investissement dans les solutions de mobilité urbaine durable": "Investissez dans les solutions de mobilité urbaine durable pour réduire la congestion routière et les émissions de gaz à effet de serre en milieu urbain."   
}

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
def generate_random_poste_inv():
    poste_inv = {}
    poste_inv['Id'] = random.randint(1, 1000)
    poste_inv['IdOwner'] = random_string(10)
    
    # Sélectionner aléatoirement un titre parmi les titres significatifs
    titre = random.choice(list(titres_et_descriptions.keys()))
    poste_inv['Titre'] = titre
    
    # Utiliser le titre sélectionné pour obtenir la description correspondante
    description = titres_et_descriptions[titre]
    poste_inv['Description'] = description
    
    poste_inv['DatePoste'] = random_datetime(datetime(2020, 1, 1), datetime(2023, 12, 31))
    poste_inv['Montant'] = random.choice([100000,200000,300000,400000,500000,600000,700000,800000,900000])
    poste_inv['Secteur'] = random.choice(['Finance', 'Technologie', 'Santé', 'Immobilier', 'Énergie ', 'Biotechnologie', 'Industrie'])
    poste_inv['Status'] = 0
    poste_inv['Image'] = random_string(30)
    poste_inv['TypeInvestissement'] = random.choice(['Actions', 'Obligations', 'Immobilier'])
    poste_inv['NumLikes'] = random.randint(0, 100)
    
    return poste_inv

# Générer 91 enregistrements aléatoires de postes d'investisseurs
with open('postes_investisseurs.csv', mode='w', newline='') as csvfile:
    fieldnames = ['Id', 'IdOwner', 'Titre', 'Description', 'DatePoste', 'Montant', 'Secteur', 'Status', 'Image', 'TypeInvestissement', 'NumLikes']
    writer = csv.DictWriter(csvfile, fieldnames=fieldnames)

    writer.writeheader()
    for _ in range(91):
        poste_inv = generate_random_poste_inv()
        writer.writerow(poste_inv)

print("Les données ont été écrites dans le fichier postes_investisseurs.csv.")

