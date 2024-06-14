using MicroSAuth_GUser.Data;
using MicroSAuth_GUser.DTOs;
using Microsoft.AspNetCore.Identity;

namespace MicroSAuth_GUser.Repositories
{
    public class ApiRequests
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        private readonly InvestisseurRepository _investisseurRepository;

        private readonly StartupRepository _StartupRepository;
        public ApiRequests(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, AppDbContext context, InvestisseurRepository investisseurRepository, StartupRepository startupRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
            _investisseurRepository = investisseurRepository;
            _StartupRepository = startupRepository;
        }
        public async Task<string> GetNomByEmail(string email)
        {
            Console.WriteLine(email);
            var getUser = await _userManager.FindByEmailAsync(email);
            if (getUser != null)
            {
                var getUserRole = await _userManager.GetRolesAsync(getUser);
                if (getUserRole != null && getUserRole.Any() && getUserRole.First() == "RInvestisseur")
                {
                    var inv = await _investisseurRepository.AffichageinvistisseurById(getUser.Id);
                    return $"{inv.Prenom} {inv.Nom}";
                }
                else if (getUserRole != null && getUserRole.Any() && getUserRole.First() == "RStartup")
                {
                    var star = await _StartupRepository.AffichageStartupById(getUser.Id);
                    return $"{star.Nomstr}";
                }
            }
            return null;
        }


    }
}
