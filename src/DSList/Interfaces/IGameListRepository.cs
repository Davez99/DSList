using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSList.DTOs;
using DSList.Entities;

namespace DSList.Interfaces
{
    public interface IGameListRepository
    {
        Task<GameList> FindByIdAsync(long id);
        Task<IEnumerable<GameListDTO>> FindAllAsync();
        Task<List<Game>> SearchByListAsync(long id);
        Task UpdateBelongingPositionAsync(long listId, long gameId, int index);
    }
}