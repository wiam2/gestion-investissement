
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroSAuth_GUser.DTOs
{
    public class StartupDTO : UserDTO
    {

        public string? Id { get; set; } = string.Empty;
        [Required]
        public string Nomstr { get; set; } = string.Empty;
        [Required]
        public DateTime? DateInscription { get; set; } =null;
      
        public string Fondateur { get; set; } = string.Empty;
        [Required]
        public float? Capital { get; set; } = null;
        [Required]
        public string Ville { get; set; } = string.Empty;
        [Required]
        public string Lieu { get; set; } = string.Empty;
        [Required]
        public string Domaine { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string siteweb { get; set; } = string.Empty;
        [Required]
        public int? Capacite { get; set; } = null;

      
    }
}

