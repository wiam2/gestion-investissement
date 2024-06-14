using MicroSAuth_GUser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using ServiceMessagerie.Models;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ChatApplicationYT.Hub;

public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
{
   
    //private  List<Conversation> _userConnections;
    private Dictionary<string, Conversation> _connection;
    private readonly HttpClient _httpClient;

    private readonly AppDbContext _conversationContext;
  

    public ChatHub(Dictionary<string, Conversation> connection,AppDbContext dbcontext, HttpClient httpClient)
    {
        _connection = connection;
        _conversationContext = dbcontext;
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    }
  
public async Task JoinRoom(Conversation userConnection)
    {
        string conversationId = GenerateConversationId(userConnection.Emeteur, userConnection.Recepteur);
        if (conversationId == null)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "Error", "Failed to generate conversation ID", DateTime.Now);
            return;
        }

        Conversation conversation = await _conversationContext.Conversation.FindAsync(conversationId);

        if (conversation == null)
        {
            conversation = new Conversation { idconversation = conversationId, Emeteur = userConnection.Emeteur, Recepteur = userConnection.Recepteur };
            userConnection.idconversation = conversationId;
            await _conversationContext.Conversation.AddAsync(conversation);
            await _conversationContext.SaveChangesAsync();
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);

        _connection.Add(Context.ConnectionId, conversation);

        var url = $"http://localhost:8081/api/fichier/{conversationId}/Fichiers";
        var response = await _httpClient.GetAsync(url);
        List<Fichier> fichiers = new List<Fichier>();
       
       
            var responseContent = await response.Content.ReadAsStringAsync();
            fichiers = JsonConvert.DeserializeObject<List<Fichier>>(responseContent);
        Console.WriteLine(fichiers);




        IEnumerable<Message> messages = await _conversationContext.Message.Where(m => m.conversation.idconversation == conversationId).ToListAsync();
        List<Message_File> messages_files = new List<Message_File>();

        if (messages != null && messages.Any())  
        {
            foreach (Message message in messages)
            {
                messages_files.Add(new Message_File
                {
                    Emeteur = message.Emeteur,
                    message = message.Contenu,
                    typemessage = "message",
                    date = message.Date
                });
            } }
        if (fichiers != null && fichiers.Any())
        {
            foreach (Fichier fichier in fichiers)
            {
                messages_files.Add(new Message_File
                {
                    Emeteur = fichier.Emeteur,
                    message = fichier.nomFichier,
                    typemessage = "fichier",
                    date = fichier.date
                });
            }
        }
            if(messages_files != null)
            messages_files = messages_files.OrderBy(mf => mf.date).ToList();

            foreach (Message_File fichier_message in messages_files)
            {
                await Clients.Group(conversation.idconversation).SendAsync("ReceiveMessage", fichier_message.Emeteur, fichier_message.message, fichier_message.typemessage, fichier_message.date);
            }
        

        await Clients.Group(_connection[Context.ConnectionId].idconversation).SendAsync("ReceiveMessage", "", $"{_connection[Context.ConnectionId].Emeteur} has Joined the Conversation", "message", DateTime.Now);
    }



    public async Task SendMessage(string message, Conversation conversation)
    {
        string conversationId = GenerateConversationId(conversation.Emeteur, conversation.Recepteur);

        await Clients.Group(conversationId).SendAsync("ReceiveMessage", conversation.Emeteur, message,"message", DateTime.Now);
        Conversation conversation1 = await _conversationContext.Conversation.FindAsync(conversationId);
        if (conversation1 != null)
        {
            Message newMessage = new Message
            {
                Id = 0,
                Emeteur = conversation.Emeteur,
                Recepteur = conversation.Recepteur,
                Contenu = message,
                Date = DateTime.Now,
                conversation = conversation1
            };

            await _conversationContext.Message.AddAsync(newMessage);
            await _conversationContext.SaveChangesAsync();

        }
    }



    public async Task SendFile( string sender, string recipient , string filename)
    {
        Console.WriteLine(sender, recipient, filename);
        string conversationId = GenerateConversationId(sender, recipient);

        await Clients.Group(conversationId).SendAsync("ReceiveMessage", sender, filename,"fichier" ,DateTime.Now);
    }



    public Task SendConnectedUser(string conversationid)
    {
        return Clients.Group(conversationid).SendAsync("ConnectedUser", "hi");
    }
    private string GenerateConversationId(string emeteur, string recepteur)
    {
        var conversationId = string.Join("_", new[] { emeteur, recepteur }.OrderBy(x => x).ToArray());
        return conversationId;
    }


    


}
public class Fichier
{
    public long Id { get; set; }
   
  
    public string MyPath { get; set; } // chemin de Fichier

    public string IdConversation { get; set; }
    public string Emeteur { get; set; }
    public string Recepteur { get; set; }

    public DateTime date { get; set; }

    public string nomFichier { get; set; }
}
public class Message_File
{
    public string  Emeteur { get; set; }
    public string message { get; set; }
    public string typemessage { get; set; }
    public DateTime date { get; set; }


}