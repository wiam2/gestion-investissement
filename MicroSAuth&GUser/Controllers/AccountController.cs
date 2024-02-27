using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MicroSAuth_GUser.DTOs;
using MicroSAuth_GUser.Services;

namespace MicroSAuth_GUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAccount userAccount) : ControllerBase
    {
        [HttpPost("registerInvestisseur")]
        public async Task<IActionResult> RegisterInvestisseur(DTOs.InvestisseurDTO investisseurDTO)
        {
            var response = await userAccount.CreateAccountInvestisseur(investisseurDTO);
            return Ok(response);
        }
        [HttpPost("registerStartup")]
        public async Task<IActionResult> RegisterStartup(StartupDTO startupDTO)
        {
            var response = await userAccount.CreateAccountStartup(startupDTO);
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await userAccount.LoginAccount(loginDTO);
            return Ok(response);
        }

    }
}
