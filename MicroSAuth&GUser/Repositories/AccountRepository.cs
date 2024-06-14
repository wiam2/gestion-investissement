using MicroSAuth_GUser.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MicroSAuth_GUser.DTOs;
using MicroSAuth_GUser.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static MicroSAuth_GUser.DTOs.ServiceResponses;
using Microsoft.AspNetCore.Mvc;

namespace MicroSAuth_GUser.Repositories
{
    public class AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IUserAccount
    {
        public IConfiguration Config { get; } = config;


        public async Task<GeneralResponse> CreateAccountInvestisseur(InvestisseurDTO investisseurDTO)
        {
            if (investisseurDTO is null) return new GeneralResponse(false, "Model is empty");
            var newUser = new ApplicationInvestisseur()
            {
                Nom = investisseurDTO.Nom,
                Prenom = investisseurDTO.Prenom,
                Cin = investisseurDTO.Cin,
                Tel = investisseurDTO.Tel,
                ProfilDescription = investisseurDTO.ProfilDescription,
                Email = investisseurDTO.Email,
                PasswordHash = investisseurDTO.Password,
                UserName = investisseurDTO.Email
            };
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new GeneralResponse(false, "User registered already");

            var createUser = await userManager.CreateAsync(newUser!, investisseurDTO.Password);
            if (!createUser.Succeeded)
            {
                // Il y a eu une erreur lors de la création de l'utilisateur
                var errorMessages = string.Join(", ", createUser.Errors.Select(e => e.Description));
                return new GeneralResponse(false, $"Error occured.. please try again. Details: {errorMessages}");
            }

            // Créer le rôle RInvestisseur s'il n'existe pas déjà
            var investisseurRole = new IdentityRole("RInvestisseur");
            await roleManager.CreateAsync(investisseurRole);

            // Ajouter l'utilisateur au rôle Investisseur
            var addToRoleResult = await userManager.AddToRoleAsync(newUser, "RInvestisseur");
            if (!addToRoleResult.Succeeded)
            {
                return new GeneralResponse(false, "Failed to assign role to user.");
            }

            return new GeneralResponse(true, "Account Created");
        }
        public async Task<GeneralResponse> CreateAccountStartup(StartupDTO startupDTO)
        {
            if (startupDTO is null) return new GeneralResponse(false, "Model is empty");
            float capital = startupDTO.Capital ?? 0.0f;

            var newUser = new ApplicationStartup()
            {
                Nomstr = startupDTO.Nomstr,
                DateInscription = startupDTO.DateInscription,

                Fondateur = startupDTO.Fondateur,
                Capital = capital,
                Ville = startupDTO.Ville,
                Lieu = startupDTO.Lieu,
                Domaine = startupDTO.Domaine,
                Description = startupDTO.Description,
                siteweb = startupDTO.siteweb,
                Capacite = startupDTO.Capacite,
                Email = startupDTO.Email,
                PasswordHash = startupDTO.Password,
                UserName = startupDTO.Email
            };
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new GeneralResponse(false, "User registered already");

            var createUser = await userManager.CreateAsync(newUser!, startupDTO.Password);
            if (!createUser.Succeeded)
            {
                // Il y a eu une erreur lors de la création de l'utilisateur
                var errorMessages = string.Join(", ", createUser.Errors.Select(e => e.Description));
                return new GeneralResponse(false, $"Error occured.. please try again. Details: {errorMessages}");
            }
            // Créer le rôle RStartup s'il n'existe pas déjà
            var startupRole = new IdentityRole("RStartup");
            await roleManager.CreateAsync(startupRole);

            // Ajouter l'utilisateur au rôle Investisseur
            var addToRoleResult = await userManager.AddToRoleAsync(newUser, "RStartup");
            if (!addToRoleResult.Succeeded)
            {
                return new GeneralResponse(false, "Failed to assign role to user.");
            }

            return new GeneralResponse(true, "Account Created");

        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null!, "Login container is empty");

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new LoginResponse(false, null!, "User not found");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null!, "Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token!, "Login completed");
        }



        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

public async Task<(string, string)> GetUserRoleAndEmailAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var roles = await userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();
                var email = user.Email;
                return (role, email);
            }
            else
            {
                // Si l'utilisateur n'est pas trouvé, vous pouvez renvoyer des valeurs par défaut ou null.
                return (null, null);
            }
        }

    }
}
