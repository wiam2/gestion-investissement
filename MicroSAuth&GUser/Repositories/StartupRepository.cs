using MicroSAuth_GUser.Data;
using MicroSAuth_GUser.DTOs;
using MicroSAuth_GUser.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static MicroSAuth_GUser.DTOs.ServiceResponses;

namespace MicroSAuth_GUser.Repositories

{
    public class StartupRepository : IStartupService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        private object investisseur;

        public StartupRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
        }

        public async Task<StartupDTO> AffichageStartupById(string id)
        {
            var startup = await _context.Startups.FindAsync(id);
            if (startup == null)
            {
                return null;
            }
            return new StartupDTO
            {
              
               
                Nomstr = startup.Nomstr,

                Email = startup.Email,
                siteweb = startup.siteweb,
                Capacite = startup.Capacite,
                Lieu = startup.Lieu,
                Fondateur = startup.Fondateur,
                Password = startup.PasswordHash,
                Ville = startup.Ville,
                Domaine = startup.Domaine,
                DateInscription = startup.DateInscription,
                Description = startup.Description

            };

            }

        public async Task<List<StartupDTO>> AffichageStartups()
        {
            var startups = await _context.Startups.ToListAsync();
            return startups.Select(startup => new StartupDTO
            {
               
             Id=startup.Id,
                Nomstr=startup.Nomstr,
                
                Email = startup.Email,
                siteweb=startup.siteweb,
                Capacite=startup.Capacite,
                Lieu=startup.Lieu,
                Fondateur=startup.Fondateur,
                Password=startup.PasswordHash,
                Ville=startup.Ville,
                Domaine=startup.Domaine,
                DateInscription=startup.DateInscription,
                Description=startup.Description

               

            }).ToList(); ;
        }

        public async Task SupprimerStartupById(string id)
        {
          
                var Starup = await _context.Startups.FindAsync(id);
                if (Starup == null)
                {
                    return;
                }

                _context.Startups.Remove(Starup);

                await _context.SaveChangesAsync();
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }

        public async Task<GeneralResponse> MettreAJourStartup(string id, StartupDTO startupDTO)
        {
            var startup = await _context.Startups.FindAsync(id);
            if (startup == null)
            {
                return new GeneralResponse(false, "Startup not found");
            }

            startup.Nomstr = startupDTO.Nomstr;
            startup.DateInscription = startupDTO.DateInscription;
            startup.Fondateur = startupDTO.Fondateur;
            startup.Capital = startupDTO.Capital ?? 0.0f;
            startup.Ville = startupDTO.Ville;
            startup.Lieu = startupDTO.Lieu;
            startup.Domaine = startupDTO.Domaine;
            startup.Description = startupDTO.Description;
            startup.siteweb = startupDTO.siteweb;
            startup.Capacite = startupDTO.Capacite;
            startup.Email = startupDTO.Email;
            startup.PasswordHash = startupDTO.Password;
            startup.UserName = startupDTO.Email;
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            user.Email = startup.Email;

            _userManager.UpdateNormalizedEmailAsync(user);
            _userManager.UpdateNormalizedUserNameAsync(user);
            startup.PasswordHash = _userManager.PasswordHasher.HashPassword(user, startupDTO.Password);


            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Startup updated successfully");
        }
    }
    }

    
    

