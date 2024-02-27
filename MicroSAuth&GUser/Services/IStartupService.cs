using MicroSAuth_GUser.Data;
using MicroSAuth_GUser.DTOs;
using static MicroSAuth_GUser.DTOs.ServiceResponses;

namespace MicroSAuth_GUser.Services
{
    public interface IStartupService
    {
        Task<List<StartupDTO>> AffichageStartups();
        Task<StartupDTO> AffichageStartupById(string id);
        Task SupprimerStartupById(string id);
        Task<GeneralResponse> MettreAJourStartup(string id, StartupDTO startupDTO);


    }
}
