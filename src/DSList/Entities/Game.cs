using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSList.Entities
{
    [Table("tb_game")]
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GameId { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(255)] // Limita o tamanho, ajustável conforme necessidade
        public string Title { get; set; }

        [Column("game_year")]
        public int? GameYear { get; set; }

        [Column("genre")]
        [MaxLength(100)] // Limita o tamanho
        public string Genre { get; set; }

        [Column("platforms")]
        [MaxLength(200)] // Limita o tamanho
        public string Platforms { get; set; }

        [Column("score")]
        public double? Score { get; set; }

        [Column("img_url")]
        [MaxLength(500)] // Limita o tamanho
        public string ImgUrl { get; set; }

        [Column("short_description", TypeName = "TEXT")]
        public string ShortDescription { get; set; }

        [Column("long_description", TypeName = "TEXT")]
        public string LongDescription { get; set; }

        // Construtor vazio para Entity Framework
        public Game() { }

        // Construtor completo
        public Game(
            long id,
            string title,
            int? gameYear,
            string genre,
            string platforms,
            double? score,
            string imgUrl,
            string shortDescription,
            string longDescription)
        {
            GameId = id;
            Title = title;
            GameYear = gameYear;
            Genre = genre;
            Platforms = platforms;
            Score = score;
            ImgUrl = imgUrl;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
        }

        public override bool Equals(object obj)
        {
            return obj is Game game && GameId == game.GameId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GameId);
        }
    }
}
