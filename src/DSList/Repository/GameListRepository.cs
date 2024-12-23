using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSList.Data;
using DSList.Entities;
using Microsoft.EntityFrameworkCore;
using Dapper;
using DSList.Interfaces;
using DSList.DTOs;

namespace DSList.Repository
{
    public class GameListRepository : IGameListRepository
    {
        private readonly GameContext _context;
        private readonly ILogger<GameListRepository> _logger;

        public GameListRepository(GameContext context, ILogger<GameListRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<GameListDTO>> FindAllAsync()
        {
            return await _context.GamesLists
     .Select(x => new GameListDTO(x.ListId, x.Name))
     .ToListAsync();

        }

        public async Task<GameList> FindByIdAsync(long id)
        {
            return await _context.GamesLists.FindAsync(id);
        }

        public async Task<List<Game>> SearchByListAsync(long id)
        {
            var query = @"
                SELECT g.Id, g.Title, g.GameYear, g.ImgUrl, g.ShortDescription
                FROM tb_game g
                INNER JOIN tb_belonging b ON g.Id = b.GameId
                WHERE b.ListId = @ListId
                ORDER BY b.Position";

            using var connection = _context.Database.GetDbConnection();
            return (await connection.QueryAsync<Game>(query, new { ListId = id })).ToList();
        }

        public async Task UpdateBelongingPositionAsync(long listId, long gameId, int newPosition)
        {
            if (listId <= 0 || gameId <= 0 || newPosition < 0)
            {
                throw new ArgumentException("Os parâmetros devem ser valores positivos.");
            }

            var parametros = new DynamicParameters();

            var query = @"
                UPDATE tb_belonging 
                SET position = @NewPosition 
                WHERE list_id = @ListId AND game_id = @GameId";

            parametros.Add("@ListId", listId);
            parametros.Add("@GameId", gameId);
            parametros.Add("@NewPosition", newPosition);

            try
            {
                _logger.LogInformation($"Atualizando posição: ListId={listId}, GameId={gameId}, NewPosition={newPosition}");
                await _context.Database.GetDbConnection().ExecuteAsync(query, parametros);
                _logger.LogInformation("Atualização Concluída com sucesso.");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Erro ao atualizar a posição de pertencimento.");
                throw;
            }
        }
    }

}