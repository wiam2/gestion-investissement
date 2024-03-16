using MicroSAuth_GUser.Data;
using MicroSAuth_GUser.DTOs;
using MicroSAuth_GUser.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroSAuth_GUser.Controllers
{
    [ApiController]

   
    [Route("[controller]")]
    public class InvestisseurController : ControllerBase
    {
        private readonly InvestisseurRepository _investisseurRepository;

        public InvestisseurController(InvestisseurRepository investisseurRepository)
        {
            _investisseurRepository = investisseurRepository;
        }

        [HttpGet("affichageInvestisseurs")]
        public async Task<IActionResult> GetAllInvestisseurs()
        {
            var investisseurs = await _investisseurRepository.Affichageinvistisseurs();
            return Ok(investisseurs);
        }
        [HttpGet("affichageInvestisseur/{id}")]

        public async Task<IActionResult> GetInvestisseurById(string id)
        {
            var investisseurDTO = await _investisseurRepository.AffichageinvistisseurById(id);
            if (investisseurDTO == null)
            {
                return NotFound();
            }

            return Ok(investisseurDTO);
        }
        [HttpDelete("DeleteInvestisseur/{id}")]
        public async Task<IActionResult> SupprimerInvestisseur(string id)
        {
            await _investisseurRepository.SupprimerInvestisseurById(id);
            return NoContent();
        }
        [HttpPut("UpdateInvestisseur/{id}")]
        public async Task<IActionResult> MettreAJourInvestisseur(string id, InvestisseurDTO investisseurDTO)
        {
            
            await _investisseurRepository.MettreAJourInvestisseur(id, investisseurDTO);

            return NoContent();
        }

    }
}