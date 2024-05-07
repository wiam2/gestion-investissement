using MicroS_Postes.Data;
using MicroS_Postes.DTOs;
using MicroS_Postes.Entities;
using MicroS_Postes.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace MicroS_Postes.Repositories
{
    public class GestionPosteInvestisseur : IGestionPosteInv
    {
        private readonly DataContext databaseContext;
        private readonly HttpClient _httpClient;
        public GestionPosteInvestisseur(DataContext databaseContext, HttpClient httpClient)
        {
            this.databaseContext = databaseContext;
            _httpClient = httpClient;
        }

        public async Task AnnulerPoste(int id)
        {
            var poste = databaseContext.Postes.FirstOrDefault(p => p.Id == id);

            poste.Status = 1;

            // Sauvegarder les modifications dans la base de données
            databaseContext.SaveChanges();
        }

        public async Task CreatePosteInv(PosteInvDTO poste)
        {
            //if (poste.DatePoste == null || poste.DatePoste == default)
            //{
            //    poste.DatePoste = DateTime.UtcNow;
            //}
            var posteInvestisseur = new PosteInvestisseur
            {
                Titre = poste.Titre,
                IdOwner = poste.IdOwner,
                DatePoste = DateTime.UtcNow,
                Description = poste.Description,
                Montant = poste.Montant,
                Secteur = poste.Secteur,
                Status = 0,
                Image = poste.Image,
                TypeInvestissement = poste.TypeInvestissement,
                NumLikes = 0
            };

            // Ajoutez posteStartup à votre base de données à l'aide de votre DbContext
            databaseContext.PostesInvestisseur.Add(posteInvestisseur);
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeletePoste(int id)
        {
            var poste = await databaseContext.PostesInvestisseur.FindAsync(id);
            if (poste == null)
            {
                return;
            }

            databaseContext.PostesInvestisseur.Remove(poste);

            await databaseContext.SaveChangesAsync();
        }

        public async Task<List<PosteInvDTO>> GetAllPostesInv()
        {
            var postes = await databaseContext.PostesInvestisseur.ToListAsync();
            return postes.Select(poste => new PosteInvDTO
            {

                Id = poste.Id,
                Titre = poste.Titre,
                IdOwner = poste.IdOwner,
                DatePoste = poste.DatePoste,
                Description = poste.Description,
                Montant = poste.Montant,
                Secteur = poste.Secteur,
                Status = poste.Status,
                Image = poste.Image,
                TypeInvestissement = poste.TypeInvestissement,
                 NumLikes = poste.NumLikes

            }).ToList(); 
        }

        public async Task<PosteInvDTO> GetPosteInvById(int id)
        {
            var poste = await databaseContext.PostesInvestisseur.FindAsync(id);
            if (poste == null)
            {
                return null;
            }

            return new PosteInvDTO
            {

                Id = poste.Id,
                Titre = poste.Titre,
                IdOwner = poste.IdOwner,
                DatePoste = poste.DatePoste,
                Description = poste.Description,
                Montant = poste.Montant,
                Secteur = poste.Secteur,
                Status = poste.Status,
                Image = poste.Image,
                TypeInvestissement = poste.TypeInvestissement,
                NumLikes = poste.NumLikes
            };
        }

        public async Task<List<PosteInvDTO>> GetPostesInvZero()
        {
            var Postes = await databaseContext.PostesInvestisseur
                    .OfType<PosteInvestisseur>()
                    .Where(p => p.Status == 0)
                    .ToListAsync()
                    ;
            return Postes.Select(poste => new PosteInvDTO
            {

                Id = poste.Id,
                Titre = poste.Titre,
                IdOwner = poste.IdOwner,
                DatePoste = poste.DatePoste,
                Description = poste.Description,
                Montant = poste.Montant,
                Secteur = poste.Secteur,
                Status = poste.Status,
                Image = poste.Image,
                TypeInvestissement = poste.TypeInvestissement,
                NumLikes = poste.NumLikes

            }).ToList();
        }

        public async Task UpdatePosteInv(int id, PosteInvDTO poste)
        {
            var postes = await databaseContext.PostesInvestisseur.FindAsync(id);
            if (postes == null)
            {
                return;
            }

            postes.Titre = poste.Titre;
        
            postes.Description = poste.Description;
            postes.Montant = poste.Montant;
            postes.Secteur = poste.Secteur;
            
            postes.Image = poste.Image;
            postes.TypeInvestissement = poste.TypeInvestissement;

            // Mettre à jour l'utilisateur dans la base de données
            await databaseContext.SaveChangesAsync();
        }
        public async Task<List<PosteInvDTO>> GetAllPostesInvDescending()
        {
            var postes = await databaseContext.PostesInvestisseur.OrderByDescending(poste => poste.DatePoste).ToListAsync();
            return postes.Select(poste => new PosteInvDTO
            {
                Id = poste.Id,
                Titre = poste.Titre,
                IdOwner = poste.IdOwner,
                DatePoste = poste.DatePoste,
                Description = poste.Description,
                Montant = poste.Montant,
                Secteur = poste.Secteur,
                Status = poste.Status,
                Image = poste.Image,
                TypeInvestissement = poste.TypeInvestissement,
                NumLikes = poste.NumLikes


            }).ToList();
        }
        public async Task<List<PosteInvDTO>> GetPostesInvZeroDescending()
        {
            var Postes = await databaseContext.PostesInvestisseur
                    .OfType<PosteInvestisseur>()
                    .Where(p => p.Status == 0)
                    .OrderByDescending(poste => poste.DatePoste)
                    .ToListAsync()
                    ;
            return Postes.Select(poste => new PosteInvDTO
            {

                Id = poste.Id,
                Titre = poste.Titre,
                IdOwner = poste.IdOwner,
                DatePoste = poste.DatePoste,
                Description = poste.Description,
                Montant = poste.Montant,
                Secteur = poste.Secteur,
                Status = poste.Status,
                Image = poste.Image,
                TypeInvestissement = poste.TypeInvestissement,
                NumLikes = poste.NumLikes

            }).ToList();
        }
        public async Task<dynamic> XGetRecomInvPostesById(string id)
        {
            var response = await _httpClient.GetAsync($"http://127.0.0.1:5000/ReturnInvPostes/{id}");
            response.EnsureSuccessStatusCode(); // Throw exception on non-success status codes
            var RecomInvPostes = await response.Content.ReadAsAsync<dynamic>();

            // Transformez RecomStartupPostes en une liste d'ID de poste
            var recommended_post_ids = ((IEnumerable<dynamic>)RecomInvPostes.recommended_post_ids).Select(item => (int)item).ToList();


            // Retournez les ID sous forme JSON
            return new { recommended_post_ids };
        }

    }
}
