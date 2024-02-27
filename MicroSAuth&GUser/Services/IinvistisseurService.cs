using MicroSAuth_GUser.Data;
using MicroSAuth_GUser.DTOs;

namespace MicroSAuth_GUser.Services
{
    public interface IinvistisseurService
    {
        Task<List<InvestisseurDTO>> Affichageinvistisseurs();
        Task<InvestisseurDTO> AffichageinvistisseurById(string id);
        Task SupprimerInvestisseurById(string id);
        Task MettreAJourInvestisseur(string id, InvestisseurDTO investisseurDTO);
    }
}
