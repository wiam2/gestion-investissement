using MicroSAuth_GUser.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MicroSAuth_GUser.Data
{
    //public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
    //{
    //    public DbSet<ApplicationStartup> Startups { get; set; }
    //    public DbSet<ApplicationInvestisseur> Investisseurs { get; set; }
    //}
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<ApplicationStartup> Startups { get; set; }
        public DbSet<ApplicationInvestisseur> Investisseurs { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Définir le nom de la table pour l'entité ApplicationStartup
            modelBuilder.Entity<ApplicationStartup>().ToTable("Startups");

            // Définir le nom de la table pour l'entité ApplicationInvestisseur
            modelBuilder.Entity<ApplicationInvestisseur>().ToTable("Investisseurs");
        }
    }
}
