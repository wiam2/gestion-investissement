using MicroS_Postes.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace MicroS_Postes.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Poste> Postes { get; set; }
        public DbSet<PosteInvestisseur> PostesInvestisseur { get; set; }
        public DbSet<PosteStartup> PostesStartup { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PosteInvestisseur>().ToTable("PostesInvestisseurs");
            modelBuilder.Entity<PosteStartup>().ToTable("PostesStartups");
        }


    }
}
