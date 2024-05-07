using MicroS_Postes.DTOs;
using MicroS_Postes.Entities;
using MicroS_Postes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroS_Postes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly GestionLike _serviceGlike;
        public LikesController(GestionLike service)
        {
            this._serviceGlike = service;

        }
        [HttpGet("Alllikes")]
        public async Task<IActionResult> GetLikes()
        {
            var likes = await _serviceGlike.GetAllLikes();

            return Ok(likes);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Addlike(string idUser,int  idPoste)
        {
            try
            {
                await _serviceGlike.AddLike(idUser, idPoste);
                return Ok("Like ajouté avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite: {ex.Message}");
            }
        }
        [HttpDelete("Delete/{iduser}/{idposte}")]
        public async Task<IActionResult> Deletelike(string iduser,int idposte )
        {
            await _serviceGlike.DeleteLike(iduser, idposte);
            return NoContent();
        }

    }
}
