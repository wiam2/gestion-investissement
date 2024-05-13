using MicroSAuth_GUser.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;

namespace MicroServiceMessagerie.Hub
{
    public class ConversationService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public ConversationService( AppDbContext context , HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /* public async Task<Dictionary<string, string>> GetConversationsByUsername(string username)
         {
             // Dictionnaire pour stocker les noms des parties de la conversation et leurs identifiants
             Dictionary<string, string> conversationIds = new Dictionary<string, string>();

             // Recherche des conversations dans lesquelles le nom d'utilisateur est présent
             var conversations = await _context.Conversation
                 .Where(c => c.idconversation.Contains(username))
                 .ToListAsync();

             // Client HTTP pour appeler l'API de l'autre microservice
             using (var httpClient = new HttpClient())
             {
                 // Configuration de l'adresse de base de l'API de l'autre microservice
                // httpClient.BaseAddress = new Uri("http://localhost:5050");

                 // Boucle à travers les conversations pour extraire les parties pertinentes de l'ID et obtenir leurs noms
                 foreach (var conversation in conversations)
                 {
                     // Diviser l'identifiant de conversation en parties
                     string[] parts = conversation.idconversation.Split('_');

                     // Récupérer la partie de l'ID qui ne correspond pas à l'utilisateur
                     string otherParty = parts[0] == username ? parts[1] : parts[0];

                     // Appel à l'API de l'autre microservice pour obtenir le nom de l'utilisateur
                     HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5050/api/ApiRequests/{otherParty}");

                     // Vérifier si la requête a réussi
                     if (response.IsSuccessStatusCode)
                     {
                         // Lire le contenu de la réponse
                         string content = await response.Content.ReadAsStringAsync();

                         // Ajouter le nom de l'utilisateur au dictionnaire
                         conversationIds.Add(otherParty, content);
                     }
                     else
                     {
                         // Gérer les erreurs, par exemple en lançant une exception
                         throw new Exception($"Erreur lors de l'appel de l'API : {response.StatusCode}");
                     }
                 }
             }

             // Retourner le dictionnaire contenant les noms des parties de la conversation et leurs identifiants
             return conversationIds;
         }*/
        public async Task<string> GetConversationsByUsername(string username)
        {
            // Dictionnaire pour stocker les noms des parties de la conversation et leurs identifiants
            Dictionary<string, string> conversationIds = new Dictionary<string, string>();

            // Recherche des conversations dans lesquelles le nom d'utilisateur est présent
            var conversations = await _context.Conversation
     .Where(c =>
         c.idconversation.StartsWith(username + "_") || // Votre cas spécifique
         c.idconversation.EndsWith("_" + username) || // Peut-être utile dans d'autres cas
         c.idconversation.Equals(username) // Si le nom d'utilisateur est exactement égal à l'ID de conversation
     )
     .ToListAsync();
            // Client HTTP pour appeler l'API de l'autre microservice
            using (var httpClient = new HttpClient())
            {
                // Boucle à travers les conversations pour extraire les parties pertinentes de l'ID et obtenir leurs noms
                foreach (var conversation in conversations)
                {
                    // Diviser l'identifiant de conversation en parties
                    string[] parts = conversation.idconversation.Split('_');

                    // Récupérer la partie de l'ID qui ne correspond pas à l'utilisateur
                    string otherParty = parts[0] == username ? parts[1] : parts[0];
                    Console.WriteLine(otherParty);

                    // Appel à l'API de l'autre microservice pour obtenir le nom de l'utilisateur
                    HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5050/api/ApiRequests/{otherParty}");

                    // Vérifier si la requête a réussi
                    if (response.IsSuccessStatusCode)
                    {
                        // Lire le contenu de la réponse
                        string content = await response.Content.ReadAsStringAsync();

                        // Ajouter le nom de l'utilisateur au dictionnaire
                        conversationIds.Add(otherParty, content);
                    }
                    else
                    {
                        // Gérer les erreurs, par exemple en lançant une exception
                        throw new Exception($"Erreur lors de l'appel de l'API : {response.StatusCode}");
                    }
                }
            }

            // Sérialiser le dictionnaire en JSON
            string json = JsonSerializer.Serialize(conversationIds);

            // Retourner la chaîne JSON
            return json;
        }




    }
}
