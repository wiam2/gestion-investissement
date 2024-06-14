using MicroS_Postes.Data;
using MicroS_Postes.DTOs;
using MicroS_Postes.Entities;
using MicroS_Postes.Services;

using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using System.Net.Http;

namespace MicroS_Postes.Repositories
{
    public class GestionPosteStartup : IGestionPosteStar
    {
        private readonly DataContext databaseContext;
        private readonly HttpClient _httpClient;
        public GestionPosteStartup(DataContext databaseContext , HttpClient httpClient)
        {
            this.databaseContext = databaseContext;
            _httpClient = httpClient;
        }
        public async Task AnnulerPoste(int id)
        {
            // Récupérer le poste avec l'identifiant donné depuis la base de données
            var poste = databaseContext.Postes.FirstOrDefault(p => p.Id == id);

                poste.Status = 1;

                // Sauvegarder les modifications dans la base de données
                databaseContext.SaveChanges();

          
        }

      
       // public IEnumerable<Poste> GetPostesZero2()
       // {
       //     var postesZero = databaseContext.Postes
       //.OfType<PosteStartup>() 
       //.Where(p => p.Status == 0)
       //.ToList();

       //     return postesZero;
       // }
        //public async Task<List<PosteStarDTO>> GetPostesZero()
        //{
        //    var Postestartups = await databaseContext.PostesStartup
        //        .OfType<PosteStartup>()
        //        .Where(p => p.Status == 0)
        //        .ToListAsync();
        //    return Postestartups.Select(postestartup => new PosteStarDTO { 

        //        Id = postestartup.Id,
        //        Titre = postestartup.Titre,

        //        IdOwner = postestartup.IdOwner,
        //        DatePoste = postestartup.DatePoste,
        //        Description = postestartup.Description,
        //        Montant = postestartup.Montant,
        //        Secteur = postestartup.Secteur,
        //        Status = postestartup.Status,
        //        Image= postestartup.Image,
        //        EtapeDev = postestartup.EtapeDev

        //    }).ToList(); 
        //}
    

        public async Task CreatePosteStar(PosteStarDTO poste)
        {
            var posteStartup = new PosteStartup
            {
                Titre = poste.Titre,
                IdOwner = poste.IdOwner,
                DatePoste = DateTime.UtcNow,
                Description = poste.Description,
                Montant = poste.Montant,
                Secteur = poste.Secteur,
                Status = 0,
                Image = poste.Image,
                EtapeDev = poste.EtapeDev,
                NumLikes = 0
            };

            // Ajoutez posteStartup à votre base de données à l'aide de votre DbContext
            databaseContext.PostesStartup.Add(posteStartup);
            databaseContext.SaveChanges();
          
        }

        
        public async Task UpdatePosteStar(int id, PosteStarDTO poste)
        {
            var postes = await databaseContext.PostesStartup.FindAsync(id);
            if (postes == null)
            {
                return;
            }

            postes.Titre = poste.Titre;
           
            postes.Description = poste.Description;
            postes.Montant = poste.Montant;
            postes.Secteur = poste.Secteur;
           
            postes.Image = poste.Image;
            postes.EtapeDev = poste.EtapeDev;

            // Mettre à jour l'utilisateur dans la base de données
            await databaseContext.SaveChangesAsync();

        }

        public async Task<List<PosteStarDTO>> GetPostesStarZero()
            {
                var Postestartups = await databaseContext.PostesStartup
                    .OfType<PosteStartup>()
                    .Where(p => p.Status == 0)
                    .ToListAsync();
                return Postestartups.Select(postestartup => new PosteStarDTO
                {

                    Id = postestartup.Id,
                    Titre = postestartup.Titre,

                    IdOwner = postestartup.IdOwner,
                    DatePoste = postestartup.DatePoste,
                    Description = postestartup.Description,
                    Montant = postestartup.Montant,
                    Secteur = postestartup.Secteur,
                    Status = postestartup.Status,
                    Image = postestartup.Image,
                    EtapeDev = postestartup.EtapeDev,
                    NumLikes = postestartup.NumLikes

                }).ToList();
            }
       
 public async Task<List<PosteStarDTO>> GetPostesByUserId(string userId)
        {
            // Filtrer les postes par l'ID de l'utilisateur
            var postes = await databaseContext.PostesStartup
                                              .Where(poste => poste.IdOwner == userId)
                                              .ToListAsync();

            // Mapper les postes filtrés vers DTO
            return postes.Select(poste => new PosteStarDTO
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
                EtapeDev = poste.EtapeDev,
                NumLikes = poste.NumLikes
            }).ToList();
        }

        public async Task<List<PosteStarDTO>> GetAllPostesStar()
        {

            var postes= await databaseContext.PostesStartup.ToListAsync();
            return postes.Select(poste => new PosteStarDTO
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
                EtapeDev = poste.EtapeDev,
                NumLikes = poste.NumLikes

            }).ToList(); ;
        }

        public async Task<PosteStarDTO> GetPosteStarById(int id)
        {
            var poste = await databaseContext.PostesStartup.FindAsync(id);
            if (poste == null)
            {
                return null;
            }

            return new PosteStarDTO
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
                EtapeDev = poste.EtapeDev,
                NumLikes = poste.NumLikes
            };
        }

       
         
        public async Task DeletePoste(int id)
        {
            var poste = await databaseContext.PostesStartup.FindAsync(id);
            if (poste == null)
            {
                return;
            }

            databaseContext.PostesStartup.Remove(poste);

            await databaseContext.SaveChangesAsync();
        }

        public async Task<List<PosteStarDTO>> GetAllPostesStarDescending()
        {
            var postes = await databaseContext.PostesStartup.OrderByDescending(poste => poste.DatePoste).ToListAsync();
            return postes.Select(poste => new PosteStarDTO
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
                EtapeDev = poste.EtapeDev,
                NumLikes = poste.NumLikes
            }).ToList();
        }
        public async Task<List<PosteStarDTO>> GetPostesStarZeroDescending()
        {
            var Postes = await databaseContext.PostesStartup
                    .OfType<PosteStartup>()
                    .Where(p => p.Status == 0)
                    .OrderByDescending(poste => poste.DatePoste)
                    .ToListAsync()
                    ;
            return Postes.Select(poste => new PosteStarDTO
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
                EtapeDev = poste.EtapeDev,
                NumLikes = poste.NumLikes

            }).ToList();
        }
        public async Task<dynamic> XGetRecomStartupPostesById(string id)
        {
            var response = await _httpClient.GetAsync($"http://127.0.0.1:5000/ReturnStartupPostes/{id}");
            response.EnsureSuccessStatusCode(); // Throw exception on non-success status codes
            var RecomStartupPostes = await response.Content.ReadAsAsync<dynamic>();

            // Transformez RecomStartupPostes en une liste d'ID de poste
            var recommended_post_ids = ((IEnumerable<dynamic>)RecomStartupPostes.recommended_post_ids).Select(item => (int)item).ToList();


            // Retournez les ID sous forme JSON
            return new { recommended_post_ids };
        }

    }
}
