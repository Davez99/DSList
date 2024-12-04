using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSList.Entities;
using Microsoft.EntityFrameworkCore;

namespace DSList.Data
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        { }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameList> GamesLists { get; set; }
        public DbSet<Belonging> Belongings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração de chave composta no belonging
            modelBuilder.Entity<Belonging>()
                .HasKey(entity => new
            {
                entity.ListId,
                entity.GameId,
            });


            base.OnModelCreating(modelBuilder);
        }

    }
}