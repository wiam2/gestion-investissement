using MicroSAuth_GUser.DTOs;
using MicroSAuth_GUser.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroSAuth_GUser.Controllers
{
    [ApiController]
   
    [Route("[controller]")]
    public class StartupController : ControllerBase
    {
        private readonly StartupRepository _StartupRepository;

        public StartupController(StartupRepository StartupRepository)
        {
            _StartupRepository = StartupRepository;
        }

        [HttpGet("AllStartups")]
        public async Task<IActionResult> GetAllStartups()
        {
            var Startups = await _StartupRepository.AffichageStartups();

            return Ok(Startups);
        }

        [HttpGet("affichageStartupbyid/{id}")]

        public async Task<IActionResult> GetInvestisseurById(string id)
        {
            var StartupDTO = await _StartupRepository.AffichageStartupById(id);
            if (StartupDTO == null)
            {
                return NotFound();
            }

            return Ok(StartupDTO);
        }
        [HttpDelete("DeleteStartup/{id}")]
        public async Task<IActionResult> SupprimerInvestisseur(string id)
        {
            await _StartupRepository.SupprimerStartupById(id);
            return NoContent();
        }
        [HttpPut("UpdateStartup/{id}")]
        public async Task<IActionResult> MettreAJourInvestisseur(string id, StartupDTO StartupDTO)
        {

            await _StartupRepository.MettreAJourStartup(id,StartupDTO);

            return NoContent();
        }


    }
}
