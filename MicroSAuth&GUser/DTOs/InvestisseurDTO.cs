
using System.ComponentModel.DataAnnotations;


namespace MicroSAuth_GUser.DTOs
{
    public class InvestisseurDTO : UserDTO
    {
        public string? Id { get; set; } = string.Empty;

        [Required]
        public string Nom { get; set; } = string.Empty;
        [Required]
        public string Prenom { get; set; } = string.Empty;
        [Required]
        public string Cin { get; set; } = string.Empty;
        [Required]
        public string Tel { get; set; } = string.Empty;
        [Required]
        public string ProfilDescription { get; set; } = string.Empty;

     
    }
}
