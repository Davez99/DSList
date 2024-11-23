using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSList.Entities
{
    public class BelongingPK
    {
        public long GameId { get; set; }
        public long ListId { get; set; }

        // Relacionamentos (chaves estrangeiras)
        [ForeignKey("GameId")]
        public Game Game { get; set; }

        [ForeignKey("ListId")]
        public GameList GameList { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BelongingPK other &&
                   GameId == other.GameId &&
                   ListId == other.ListId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GameId, ListId);
        }
    }
}