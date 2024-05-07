using MicroS_Postes.DTOs;
using MicroS_Postes.Entities;
using MicroS_Postes.Repositories;

namespace MicroS_Postes.Services
{
    public interface IGestionPosteInv : IGestionPoste
    {
        Task CreatePosteInv(PosteInvDTO poste);
        Task UpdatePosteInv(int id, PosteInvDTO poste);
        Task<List<PosteInvDTO>> GetPostesInvZero();
        Task<List<PosteInvDTO>> GetAllPostesInv();
        Task<PosteInvDTO> GetPosteInvById(int id);


    }
}
