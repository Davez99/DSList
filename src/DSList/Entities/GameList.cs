using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSList.Entities
{
    [Table("tb_game_list")]
    public class GameList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ListId { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(100)] // Limita o tamanho do nome, ajustável conforme necessidade
        public string Name { get; set; }

        // Construtor vazio para uso pelo Entity Framework
        public GameList() { }

        // Construtor com parâmetros
        public GameList(long id, string name)
        {
            ListId = id;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is GameList gameList && ListId == gameList.ListId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ListId);
        }
    }
}
