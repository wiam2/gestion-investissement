using MicroS_Postes.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroS_Postes.Repositories;

namespace MicroS_Postes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosteStarController : ControllerBase
    {

        private readonly GestionPosteStartup _serviceStar; 

        public PosteStarController(GestionPosteStartup service)
        {
            this._serviceStar = service;


        }

        [HttpGet("PostesStartupZero")]
        public async Task<IActionResult> GetPostesStartupZero()
        {
            var Startups = await _serviceStar.GetPostesStarZero();

            return Ok(Startups);
        }
        [HttpGet("getPosteById/{id}")]

        public async Task<IActionResult> GetStartupById(int id)
        {
            var startupDTO = await _serviceStar.GetPosteStarById(id);
            if (startupDTO == null)
            {
                return NotFound();
            }

            return Ok(startupDTO);
        }
        [HttpPost("CreatePoste")]
        public async Task<IActionResult> CreatePosteStars(PosteStarDTO poste)
        {
            try
            {
                await _serviceStar.CreatePosteStar(poste);
                return Ok("PosteStartup créé avec succès.");
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
                await _serviceStar.AnnulerPoste(id);
                return Ok("Poste annulé avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite: {ex.Message}");
            }
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePosteStar(int id)
        {
            await _serviceStar.DeletePoste(id);
            return NoContent();
        }
        [HttpGet("AllPostes")]
        public async Task<IActionResult> GetAllPostes()
        {
            var postes = await _serviceStar.GetAllPostesStar();

            return Ok(postes);
        }
        [HttpPut("UpdatePoste/{id}")]
        public async Task<IActionResult> UpdatePosteStartup(int id, PosteStarDTO poste)
        {

            await _serviceStar.UpdatePosteStar(id, poste);

            return NoContent();
        }
        [HttpGet("AllPostesDecsending")]
        public async Task<IActionResult> GetAllPostesDecsending()
        {
            var postes = await _serviceStar.GetAllPostesStarDescending();

            return Ok(postes);
        }
        [HttpGet("PostesStartupZeroDecsending")]
        public async Task<IActionResult> GetPostesInvZeroDecsending()
        {
            var Investisseurs = await _serviceStar.GetPostesStarZeroDescending();

            return Ok(Investisseurs);
        }
        [HttpGet("RecomtartupPosteById/{id}")]

        public async Task<IActionResult> XGetRecomStartupPostesById(string id)
        {
            var startupIDs = await _serviceStar.XGetRecomStartupPostesById(id);
            if (startupIDs == null)
            {
                return NotFound();
            }

            return Ok(startupIDs);
        }

    }
}
