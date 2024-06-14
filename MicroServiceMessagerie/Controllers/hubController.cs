using ChatApplicationYT.Hub;
using MicroSAuth_GUser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServiceMessagerie.Models;

namespace MicroServiceMessagerie.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class hubController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly AppDbContext _conversationContext;

        public hubController(IHubContext<ChatHub> hubContext, AppDbContext conversationContext)
        {
            _hubContext = hubContext;
            _conversationContext = conversationContext;
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinRoom(Conversation userConnection)
        {
            string conversationId = GenerateConversationId(userConnection.Emeteur, userConnection.Recepteur);
            if (conversationId == null)
            {
                return BadRequest("Failed to generate conversation ID");
            }

            Conversation conversation = await _conversationContext.Conversation.FindAsync(conversationId);

            if (conversation == null)
            {
                conversation = new Conversation { idconversation = conversationId, Emeteur = userConnection.Emeteur, Recepteur = userConnection.Recepteur };
                userConnection.idconversation = conversationId;
                await _conversationContext.Conversation.AddAsync(conversation);
                await _conversationContext.SaveChangesAsync();
            }

            await _hubContext.Groups.AddToGroupAsync(conversationId, conversationId);

            // Envoyer un message pour informer que l'utilisateur a rejoint la conversation
            await _hubContext.Clients.Group(conversationId).SendAsync("ReceiveMessage", "", $"{userConnection.Emeteur} has Joined the Conversation", DateTime.Now);

            return Ok();
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(string message, Conversation conversation)
        {
            string conversationId = GenerateConversationId(conversation.Emeteur, conversation.Recepteur);

            await _hubContext.Clients.Group(conversationId).SendAsync("ReceiveMessage", conversation.Emeteur, message, DateTime.Now);

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

            return Ok();
        }

        private string GenerateConversationId(string emeteur, string recepteur)
        {
            var conversationId = string.Join("_", new[] { emeteur, recepteur }.OrderBy(x => x).ToArray());
            return conversationId;
        }
    }
}
