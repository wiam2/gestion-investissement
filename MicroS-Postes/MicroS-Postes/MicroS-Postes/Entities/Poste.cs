using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroS_Postes.Entities
{
    public class Poste
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [Required]
        public string Image { get; set; } = string.Empty;
        public int? NumLikes { get; set; } = 0;


    }
}
