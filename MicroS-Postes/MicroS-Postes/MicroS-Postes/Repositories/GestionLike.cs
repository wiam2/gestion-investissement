using MicroS_Postes.Data;
using MicroS_Postes.DTOs;
using MicroS_Postes.Entities;
using MicroS_Postes.Services;
using Microsoft.EntityFrameworkCore;

namespace MicroS_Postes.Repositories
{
    public class GestionLike : IGestionLike
    {
        private readonly DataContext databaseContext;
        public GestionLike(DataContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<List<Like>> GetAllLikes()
        {
            var likes= await databaseContext.Likes.ToListAsync();
            return likes.Select(likes => new Like
            {

                 IdLike = likes.IdLike,
                 IdLOwner =likes.IdLOwner,
                 IdPoste =likes.IdPoste
    }).ToList();
        }
       
        public async Task AddLike(string idUser, int idposte)
        {
            // Vérifier si une entrée avec les mêmes valeurs IdLOwner et IdPoste existe déjà
            var existingLike = await databaseContext.Likes.FirstOrDefaultAsync(l => l.IdLOwner == idUser && l.IdPoste == idposte);

            // Si une entrée existe déjà, vous pouvez choisir de ne pas ajouter le like
            if (existingLike != null)
            {
                // Gérer le cas où le like existe déjà
                // Vous pouvez lancer une exception, afficher un message à l'utilisateur, etc.
                // Par exemple, lancer une exception peut être une approche :
                throw new InvalidOperationException("Le like existe déjà.");
            }
            var Like = new Like
            {
                IdLOwner = idUser,
                IdPoste = idposte
            };

            // Ajout du Like à la base de données
            databaseContext.Likes.Add(Like);

            // Incrémentation du nombre de likes du Poste
            var poste = await databaseContext.Postes.FirstOrDefaultAsync(p => p.Id == idposte);
            if (poste != null)
            {
                poste.NumLikes += 1;
            }

            // Sauvegarde des modifications
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteLike(string idUser, int idposte)
        {
            // Créer une requête LINQ pour rechercher l'enregistrement dans la table des Likes
            var likeQuery = from ulike in databaseContext.Likes
                            where ulike.IdLOwner == idUser && ulike.IdPoste == idposte
                            select ulike;

            // Exécuter la requête pour récupérer l'enregistrement
            var like = await likeQuery.FirstOrDefaultAsync();

            if (like == null)
            {
                // L'enregistrement n'existe pas, aucune action nécessaire
                return;
            }

            databaseContext.Likes.Remove(like);
            // Recherche le poste associé à l'ID idLike
            var poste = await databaseContext.Postes.FirstOrDefaultAsync(p => p.Id == idposte);
            if (poste != null)
            {
                poste.NumLikes -= 1;
                poste.NumLikes = poste.NumLikes < 0 ? 0 : poste.NumLikes;
            }

            // Sauvegarde des modifications
            await databaseContext.SaveChangesAsync();
            
        }
        
    }
}
