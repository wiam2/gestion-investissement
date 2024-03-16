using MicroSAuth_GUser.Repositories;
using MicroSAuth_GUser.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroSAuth_GUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class test(IUserAccount userAccount) : ControllerBase
    {
        [HttpPost("registerInvestisseur")]
        public async Task<IActionResult> RegisterInvestisseur(DTOs.InvestisseurDTO investisseurDTO)
        {
            var response = await userAccount.CreateAccountInvestisseur(investisseurDTO);
            return Ok(response);
        }
        [HttpGet("affichageInvestisseurs")]
        public async Task<IActionResult> GetAllInvestisseurs()
        {
           
            return Ok();
        }
       

      
    }
}
