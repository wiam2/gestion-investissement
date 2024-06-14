/*using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> SendMessage(string userRep, [FromBody] string message , string userId)
        {
            if (string.IsNullOrEmpty(userRep) || string.IsNullOrEmpty(message)|| string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            await _hubContext.Clients.Client(userRep).SendAsync("ReceiveMessage", userId,message);

            return Ok();
        }
    }
}*/

/*using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using ServiceMessagerie.Models;

namespace ChatApplicationYT.Controllers
{
  
    public class TestController : Controller
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public TestController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<IActionResult> SendMessage()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Lets Program Bot", "Hello from TestController", DateTime.Now);
            return Ok();
        }

        public async Task<IActionResult> SendMessageToGroup()
        {
            await _hubContext.Clients.Group("Group1").SendAsync("ReceiveMessage", "Lets Program Bot", "Hello from Group1", DateTime.Now);
            return Ok();
        }
    }
}*/