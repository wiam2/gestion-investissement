using MicroSAuth_GUser.Data;
using MicroSAuth_GUser.DTOs;
using MicroSAuth_GUser.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MicroSAuth_GUser.Repositories
{
    public class InvestisseurRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public InvestisseurRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
        }

        public async Task<List<InvestisseurDTO>> Affichageinvistisseurs()
        {

            var investisseurs = await _context.Investisseurs.ToListAsync();
            return investisseurs.Select(investisseur => new InvestisseurDTO
            {
               
                Id=investisseur.Id,
                Nom = investisseur.Nom,
                Prenom = investisseur.Prenom,
                Email = investisseur.Email,
                Tel = investisseur.Tel,
                ProfilDescription=investisseur.ProfilDescription,
                Password=investisseur.PasswordHash,
                Cin = investisseur.Cin

            }).ToList(); ;
        }


        public async Task<InvestisseurDTO> AffichageinvistisseurById(string id)
        {
            var investisseur = await _context.Investisseurs.FindAsync(id);
            if (investisseur == null)
            {
                return null;
            }

            return new InvestisseurDTO
            {
               
              
                Nom = investisseur.Nom,
                Prenom = investisseur.Prenom,
                Email = investisseur.Email,
                Tel = investisseur.Tel,
                ProfilDescription = investisseur.ProfilDescription,
                Password = investisseur.PasswordHash,
                Cin = investisseur.Cin
            };
        }
        public async Task SupprimerInvestisseurById(string id)
        {
            var investisseur = await _context.Investisseurs.FindAsync(id);
            if (investisseur == null)
            {
                return;
            }

            _context.Investisseurs.Remove(investisseur);

            await _context.SaveChangesAsync();
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
        public async Task MettreAJourInvestisseur(string id, InvestisseurDTO investisseurDTO)
        {
            var investisseur = await _context.Investisseurs.FindAsync(id);
            if (investisseur == null)
            {
                return;
            }

            investisseur.Nom = investisseurDTO.Nom;
            investisseur.Prenom = investisseurDTO.Prenom;
            investisseur.Cin = investisseurDTO.Cin;
            investisseur.Tel = investisseurDTO.Tel;
            investisseur.ProfilDescription = investisseurDTO.ProfilDescription;
            investisseur.Email = investisseurDTO.Email;
            investisseur.UserName = investisseurDTO.Email;

            ApplicationUser user = await _userManager.FindByIdAsync(id);
            user.Email = investisseur.Email;

            _userManager.UpdateNormalizedEmailAsync(user);
            _userManager.UpdateNormalizedUserNameAsync(user);
            investisseur.PasswordHash = _userManager.PasswordHasher.HashPassword(user, investisseurDTO.Password);

          

            // Mettre à jour l'utilisateur dans la base de données


            await _context.SaveChangesAsync();

        }
    }
}

