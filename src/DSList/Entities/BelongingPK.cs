using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSList.Entities
{
    public class BelongingPK
    {
        [Key]
        [Column("game_id")]
        public Game Game { get; set; }

        [Column("list_id")]
        public GameList GameList { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BelongingPK other &&
                   Equals(Game, other.Game) &&
                   Equals(GameList, other.GameList);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Game, GameList);
        }
    }
}