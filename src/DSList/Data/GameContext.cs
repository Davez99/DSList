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
        public GameContext(DbContextOptions<DbContext> options) : base(options)
        { }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameList> GamesLists { get; set; }
        public DbSet<Belonging> Belongings { get; set; }
        public DbSet<BelongingPK> BelongingPKs { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Belonging>()
                .HasKey(b => new { b.Id.GameId, b.Id.ListId });

            modelBuilder.Entity<Belonging>()
                .HasOne(b => b.Id.Game)
                .WithMany()
                .HasForeignKey(b => b.Id.GameId);

            modelBuilder.Entity<Belonging>()
                .HasOne(b => b.Id.GameList)
                .WithMany()
                .HasForeignKey(b => b.Id.ListId);

            base.OnModelCreating(modelBuilder);
        }

    }
}