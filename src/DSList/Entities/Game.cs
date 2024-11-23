using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSList.Entities
{
    [Table("tb_game")]
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Column("game_year")]
        public int? Year { get; set; }

        public string Genre { get; set; }

        public string Platforms { get; set; }

        public double? Score { get; set; }

        [Column("img_url")]
        public string ImgUrl { get; set; }

        [Column(TypeName = "TEXT")]
        public string ShortDescription { get; set; }

        [Column(TypeName = "TEXT")]
        public string LongDescription { get; set; }

        public Game() { }

        public Game(long id, string title, int? year, string genre, string platforms, double? score, string imgUrl, string shortDescription, string longDescription)
        {
            Id = id;
            Title = title;
            Year = year;
            Genre = genre;
            Platforms = platforms;
            Score = score;
            ImgUrl = imgUrl;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
        }

        public override bool Equals(object obj)
        {
            return obj is Game game &&
                   Id == game.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}