using MicroS_Postes.DTOs;
using MicroS_Postes.Entities;

namespace MicroS_Postes.Services
{
    public interface IGestionLike
    {
        Task DeleteLike(string idUser, int idposte);
        Task AddLike(string idUser, int idposte);
        Task<List<Like>> GetAllLikes();

    }
}
