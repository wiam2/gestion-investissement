using MicroSAuth_GUser.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MicroSAuth_GUser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiRequestsController : ControllerBase
    {
        private readonly ApiRequests _userService;

        public ApiRequestsController(ApiRequests userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<string>> GetNomByEmail(string email)
        {
            var nom = await _userService.GetNomByEmail(email);
            if (nom == null)
            {
                return NotFound();
            }
            return Ok(nom);
        }
    }
}
