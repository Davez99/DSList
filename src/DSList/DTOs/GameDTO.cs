using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSList.Entities;

namespace DSList.DTOs
{
    public class GameDTO
    {
        public long GameId { get; set; }
        public string Title { get; set; }
        public int? GameYear { get; set; }
        public string Genre { get; set; }
        public string Platforms { get; set; }
        public double? Score { get; set; }
        public string ImgUrl { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        // Construtor que recebe a entidade Game

        // public GameDTO(Game entity)
        // {
        //     GameId = entity.GameId;
        //     Title = entity.Title;
        //     GameYear = entity.GameYear;
        //     Genre = entity.Genre;
        //     Platforms = entity.Platforms;
        //     Score = entity.Score;
        //     ImgUrl = entity.ImgUrl;
        //     ShortDescription = entity.ShortDescription;
        //     LongDescription = entity.LongDescription;
        // }

        //Contrutor vazio para chamadas somente do DTO
        public GameDTO()
        {
        }

        public GameDTO(long gameId, string title, int? gameYear, string genre, string platforms, double? score, string imgUrl, string shortDescription, string longDescription)
        {
            GameId = gameId;
            Title = title;
            GameYear = gameYear;
            Genre = genre;
            Platforms = platforms;
            Score = score;
            ImgUrl = imgUrl;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
        }
    }
}