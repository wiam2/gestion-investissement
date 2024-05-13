namespace MicroServiceMessagerie.Controllers
{
    using MicroServiceMessagerie.Hub;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly ConversationService _conversationService;

        public ConversationController(ConversationService conversationService)
        {
            _conversationService = conversationService;
        }
        [HttpGet("{username}")]


        public async Task<ActionResult<string>> GetConversations(string username)
        {
            try
            {
                // Appel du service pour récupérer les conversations sous forme de chaîne JSON
                var conversationJson = await _conversationService.GetConversationsByUsername(username);

                // Retourner la chaîne JSON en tant que ActionResult avec un code de statut OK (200)
                return Ok(conversationJson);
            }
            catch (Exception ex)
            {
                // Retourner une réponse d'erreur avec un code de statut 500 (Internal Server Error)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    } 
}
