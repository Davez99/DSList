using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

       
    }
}