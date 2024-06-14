using MicroS_Postes.DTOs;
using MicroS_Postes.Entities;
using MicroS_Postes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace MicroS_Postes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosteInvController : ControllerBase
    {
        private readonly GestionPosteInvestisseur _serviceInv;
        private readonly XGestionUser _gestionUser;
        public PosteInvController( GestionPosteInvestisseur service, XGestionUser gestionUser)
        {
            _serviceInv = service;
            _gestionUser = gestionUser;

        }
        [HttpGet("PostesInvestisseurZero")]
        public async Task<IActionResult> GetPostesInvZero()
        {
            var Investisseurs = await _serviceInv.GetPostesInvZero();

            return Ok(Investisseurs);
        }

        [HttpGet("getPosteById/{id}")]

        public async Task<IActionResult> GetPosteInvestisseurById(int id)
        {
            var investisseurDTO = await _serviceInv.GetPosteInvById(id);
            if (investisseurDTO == null)
            {
                return NotFound();
            }

            return Ok(investisseurDTO);
        }
        

        [HttpPost("CreatePoste")]
        public async Task<IActionResult> CreatePosteInvs(PosteInvDTO poste)
        {
            try
            {
                await _serviceInv.CreatePosteInv(poste);
                return Ok("PosteInvestisseur créé avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite: {ex.Message}");
            }
        }
        [HttpPatch("ValiderPoste/{id}")]
        public async Task<IActionResult> ValiderPoste(int id)
        {
            try
            {
                await _serviceInv.AnnulerPoste(id);
                return Ok("Poste annulé avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite: {ex.Message}");
            }
        }
      
  [HttpGet("getPosteByUserId/{id}")]

  public async Task<IActionResult> GetStartupByUserId(string id)
        {
            var startupDTO = await _serviceInv.GetPostesByUserId(id);
            if (startupDTO == null)
            {
                return NotFound();
            }

            return Ok(startupDTO);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePosteInv(int id)
        {
            await _serviceInv.DeletePoste(id);
            return NoContent();
        }
        [HttpGet("AllPostes")]
        public async Task<IActionResult> GetAllPostes()
        {
            var postes = await _serviceInv.GetAllPostesInv();

            return Ok(postes);
        }
        [HttpPut("UpdatePoste/{id}")]
        public async Task<IActionResult> UpdatePosteInvestisseur(int id, PosteInvDTO poste)
        {

            await _serviceInv.UpdatePosteInv(id, poste);

            return NoContent();
        }
        [HttpGet("AllPostesDecsending")]
        public async Task<IActionResult> GetAllPostesDecsending()
        {
            var postes = await _serviceInv.GetAllPostesInvDescending();

            return Ok(postes);
        }
        [HttpGet("PostesInvestisseurZeroDecsending")]
        public async Task<IActionResult> GetPostesInvZeroDecsending()
        {
            var Investisseurs = await _serviceInv.GetPostesInvZeroDescending();

            return Ok(Investisseurs);
        }
        [HttpGet("getInvestisseurById/{id}")]

        public async Task<IActionResult> GetInvestisseurById(string id)
        {
            // Utilisation de XGestionUser pour obtenir l'investisseur par son ID
            var investisseurDTO = await _gestionUser.XGetInvestisseurById(id);

            // Faites quelque chose avec investisseurDTO, par exemple, retournez-le
           
           
            if (investisseurDTO == null)
            {
                return NotFound();
            }

            return Ok(investisseurDTO);
        }
        [HttpGet("RecomInvPosteById/{id}")]

        public async Task<IActionResult> XGetRecomInvPostesById(string id)
        {
            var InvIDs = await _serviceInv.XGetRecomInvPostesById(id);
            if (InvIDs == null)
            {
                return NotFound();
            }

            return Ok(InvIDs);
        }

    }
}


