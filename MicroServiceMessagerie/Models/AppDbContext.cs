using MicroSAuth_GUser.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceMessagerie.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MicroSAuth_GUser.Data
{

    public class AppDbContext : DbContext
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Message{ get; set; }
       
        public DbSet<Conversation> Conversation { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Définir le nom de la table pour l'entité ApplicationStartup
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<Conversation>().ToTable("Conversation");



        }
    }
}
