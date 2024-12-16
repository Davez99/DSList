using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSList.Entities;
using DSList.Interfaces;

namespace DSList.DTOs
{
    public class GameMinDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public string ImgUrl { get; set; }
        public string ShortDescription { get; set; }

        // Construtor que recebe a entidade Game
        public GameMinDTO(Game entity)
        {
            Id = entity.GameId;
            Title = entity.Title;
            Year = entity.GameYear;
            ImgUrl = entity.ImgUrl;
            ShortDescription = entity.ShortDescription;
        }

        // Construtor que recebe a projeção GameMinProjection
        public GameMinDTO(IGameMinProjection projection)
        {
            Id = projection.Id;
            Title = projection.Title;
            Year = projection.GameYear;
            ImgUrl = projection.ImgUrl;
            ShortDescription = projection.ShortDescription;
        }
    }

}