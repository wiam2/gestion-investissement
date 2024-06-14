/*using ChatApplicationYT.Hub;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceMessagerie.Controllers
{
    public class FichierController : ControllerBase
    {
         private ChatHub message;
        public FichierController(ChatHub chat)
        {
            message = chat;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, string Emeteur, string Recepteur, string FileName)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("Le fichier est vide ou inexistant.");
            }

            try
            {
                // Appel de votre méthode pour envoyer le fichier
                await message.SendFile(file, Emeteur, Recepteur, FileName);
                return Ok("Fichier envoyé avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors de l'envoi du fichier : {ex.Message}");
            }
        }
    }
}*/
