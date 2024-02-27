using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroSAuth_GUser.Data
{
    public class ApplicationInvestisseur : ApplicationUser
    {
       
       
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Cin { get; set; }
        public string Tel { get; set; }
        public string ProfilDescription { get; set; }

        public ApplicationInvestisseur(string nom, string prenom, string cIN, string tel, string profilDescription)
        {
            Nom = nom;
            Prenom = prenom;
            Cin = cIN;
            Tel = tel;
            ProfilDescription = profilDescription;
        }
        public ApplicationInvestisseur() { }
    }
}
