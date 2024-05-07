using MicroS_Postes.Entities;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace MicroS_Postes.Repositories
{
    public interface IGestionPoste
    {

        Task DeletePoste(int id);
        Task AnnulerPoste(int id);
       
        

    }
}
