using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSList.DTOs
{
    public class GameListDTO
    {
        public GameListDTO()
        {
            
        }
        public GameListDTO(long listId, string name)
        {
            ListId = listId;
            Name = name;
        }

        public long ListId { get; set; }
        public string Name { get; set; }
    }
}