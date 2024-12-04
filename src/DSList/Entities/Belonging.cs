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
        [Column("game_id")]
        public int GameId { get; set; }
        [Column("list_id")]
        public int ListId { get; set; }

        [Column("position")]
        public int Position { get; set; }

         // Construtor vazio para uso pelo Entity Framework
        public Belonging() { }

        // Construtor completo
        public Belonging(int gameId, int listId, int position)
        {
            GameId = gameId;
            ListId = listId;
            Position = position;
        }
    }
}