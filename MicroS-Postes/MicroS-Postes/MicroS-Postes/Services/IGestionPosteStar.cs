using MicroS_Postes.DTOs;
using MicroS_Postes.Repositories;

namespace MicroS_Postes.Services
{
    public interface IGestionPosteStar : IGestionPoste
    {
        Task CreatePosteStar(PosteStarDTO poste);
        Task UpdatePosteStar(int id, PosteStarDTO poste);
        Task<List<PosteStarDTO>> GetPostesStarZero();
        Task<List<PosteStarDTO>> GetAllPostesStar();
        Task<PosteStarDTO> GetPosteStarById(int id);
    }
}
