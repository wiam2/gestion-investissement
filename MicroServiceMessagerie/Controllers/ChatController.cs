/*using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceMessagerie.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _chatService.AuthenticateUserAsync(request.email, request.Password);

            if (token != null)
            {
                // Stocker le token dans la session
                // HttpContext.Session.SetString("AuthToken", token);
                string authToken = token;
                return Ok(authToken);
            }

            return BadRequest();
        }
    }

            public class LoginRequest
    {
        public string email { get; set; }
        public string Password { get; set; }
    }
}*/