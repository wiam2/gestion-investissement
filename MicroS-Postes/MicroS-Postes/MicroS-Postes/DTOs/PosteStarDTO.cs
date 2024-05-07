using System.ComponentModel.DataAnnotations;

namespace MicroS_Postes.DTOs
{
    public class PosteStarDTO
    {
        public int Id { get; set; } 
        public string IdOwner { get; set; } = string.Empty;
        [Required]
        public string Titre { get; set; } = string.Empty;
        public DateTime? DatePoste { get; set; } = null;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public float? Montant { get; set; } = null;
        [Required]
        public string Secteur { get; set; } = string.Empty;
        public int? Status { get; set; } = 0;

        public string Image { get; set; } = string.Empty;
        public string EtapeDev { get; set; } = string.Empty;

        public int? NumLikes { get; set; } = 0;
    }
}
