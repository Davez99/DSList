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
        public DbSet<BelongingPK> BelongingPKs { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     // Configuração da entidade Belonging
        //     modelBuilder.Entity<Belonging>(entity =>
        //     {
        //         // Configura a propriedade 'Id' como chave composta
        //         entity.OwnsOne(b => b.Id, owned =>
        //         {
        //             // Relacionamento com Game
        //             owned.HasOne(pk => pk.Game)
        //                 .WithMany()
        //                 .HasForeignKey("GameId")
        //                 .OnDelete(DeleteBehavior.Cascade);

        //             // Relacionamento com GameList
        //             owned.HasOne(pk => pk.GameList)
        //                 .WithMany()
        //                 .HasForeignKey("ListId")
        //                 .OnDelete(DeleteBehavior.Cascade);
        //         });

        //         // Define 'Id' como chave primária
        //         entity.HasKey("Id.GameId", "Id.ListId");
        //     });

        //     base.OnModelCreating(modelBuilder);
        // }

    }
}