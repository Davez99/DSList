using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSList.DTOs;
using DSList.Entities;

namespace DSList.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> AllGames();
        Task<GameDTO> GameById(long id);
        Task<IEnumerable<Game>> SearchByListAsync(long listId);
    }
}