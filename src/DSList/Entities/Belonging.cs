using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSList.Entities
{
    [Table("tb_belonging")]
    public class Belonging
    {
        public BelongingPK Id { get; set; } = new BelongingPK();

        public int Position { get; set; }

        // Métodos para facilitar o acesso às entidades relacionadas
        [NotMapped]
        public Game Game
        {
            get => Id.Game;
            set => Id.Game = value;
        }

        [NotMapped]
        public GameList GameList
        {
            get => Id.GameList;
            set => Id.GameList = value;
        }

        public override bool Equals(object obj)
        {
            return obj is Belonging other &&
                   Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}