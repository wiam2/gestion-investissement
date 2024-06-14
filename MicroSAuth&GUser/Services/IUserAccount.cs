

using MicroSAuth_GUser.DTOs;
using static MicroSAuth_GUser.DTOs.ServiceResponses;

namespace MicroSAuth_GUser.Services;

public interface IUserAccount
{
    Task<GeneralResponse> CreateAccountInvestisseur(InvestisseurDTO investisseurDTO);
    Task<GeneralResponse> CreateAccountStartup(StartupDTO startupDTO);
    Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    Task<(string, string)> GetUserRoleAndEmailAsync(string userId);
}

