using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroSAuth_GUser.Data
{
    public class ApplicationStartup : ApplicationUser
    {
        public string Nomstr { get; set; }
        public DateTime? DateInscription { get; set; }
        public string  Fondateur { get; set; }
        public float? Capital  { get; set; }
        public string Ville { get; set; }
        public string Lieu { get; set; }
        public string Domaine { get; set; }
        public string Description { get; set; }
        public string siteweb { get; set; }
        public int? Capacite { get; set; }

    }
}
