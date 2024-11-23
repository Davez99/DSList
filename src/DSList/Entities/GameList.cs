using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSList.Entities
{
    [Table("tb_game_list")]
    public class GameList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public GameList() { }

        public GameList(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is GameList gameList &&
                   Id == gameList.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}