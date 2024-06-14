using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MicroSAuth_GUser.DTOs;
using MicroSAuth_GUser.Services;
using MicroSAuth_GUser.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MicroSAuth_GUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IUserAccount userAccount;
        public AccountController(AppDbContext context, IEmailService emailService, IConfiguration configuration, IUserAccount userAccount)
        {
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
            this.userAccount = userAccount;
        }

        [HttpPost("registerInvestisseur")]
        public async Task<IActionResult> RegisterInvestisseur(DTOs.InvestisseurDTO investisseurDTO)
        {
            var response = await userAccount.CreateAccountInvestisseur(investisseurDTO);
            return Ok(response);
        }
        [HttpPost("registerStartup")]
        public async Task<IActionResult> RegisterStartup(StartupDTO startupDTO)
        {
            var response = await userAccount.CreateAccountStartup(startupDTO);
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await userAccount.LoginAccount(loginDTO);
            return Ok(response);
        }
        [HttpPost("forget_password")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var user = await _context.Investisseurs.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return NotFound("User not found or coming from Github or Google");

            var resetPasswordToken = createRandomToken();

            user.PasswordHash = resetPasswordToken.Item1; // token string
            /* user.resetPasswordTokenExpiring = resetPasswordToken.Item2; // expiring date */
            _emailService.SendEmail(user.Email, resetPasswordToken.Item1);
            /* if (user.phoneNumber != null)
             {
                 _smsService.SendSMS(user.phoneNumber,
                     "Bonjour " + user.firstName +
                     ".\n\nRenitialisation de votre mot de passe.\n\nVous recevez dans votre boite mail un lien pour rénitialiser votre mot de pass.\n\n Si vous n'avez rien fait, Prière de ne pas cliquer la dessus.");
             }*/
            await _context.SaveChangesAsync();
            return Ok("check your email");
        }
        private (string, DateTime) createRandomToken()
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expires = DateTime.Now.AddMinutes(15);

            var token = new JwtSecurityToken(
                expires: expires,
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return (jwt, expires);
        }
        [HttpGet("getINFO/{id}")]
        public async Task<IActionResult> GetUserRoleAndEmail(string id)
        {
            var (role, email) = await userAccount.GetUserRoleAndEmailAsync(id);
            if (role != null && email != null)
            {
                return Ok(new { Role = role, Email = email });
            }
            else
            {
                return NotFound(); // Ou BadRequest() selon votre logique d'erreur
            }
        }
    }
}
