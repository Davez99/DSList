using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DSList.Data;
using Microsoft.Extensions.Logging;
using DSList.DTOs;
using Microsoft.EntityFrameworkCore;
using DSList.Interfaces;
using DSList.Entities;

namespace DSList.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly GameContext _context;
        private readonly ILogger<GameRepository> _logger;

        public GameRepository(GameContext context, ILogger<GameRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Game>> AllGames()
        {
            return await _context.Games.Select(g => new Game(
            g.GameId
            , g.Title
            , g.GameYear
            , g.Genre
            , g.Platforms
            , g.Score
            , g.ImgUrl
            , g.ShortDescription
            , g.LongDescription
            ))
                .ToListAsync();
        }

        public async Task<GameDTO> GameById(long id)
        {
            var result = await _context.Games.FindAsync(id);
            return new GameDTO(
                result.GameId
                , result.Title
                , result.GameYear
                , result.Genre
                , result.Platforms
                , result.Score
                , result.ImgUrl
                , result.ShortDescription
                , result.LongDescription
            );
        }

        public async Task<IEnumerable<GameMinDTO>> SearchByListAsync(long listId)
        {
            if (listId <= 0)
            {
                throw new ArgumentException("O ID da lista deve ser um valor positivo.");
            }

            var query = @"
                SELECT 
                    tb_game.GameId, 
                    tb_game.title, 
                    tb_game.game_year AS Year, 
                    tb_game.img_url AS ImgUrl, 
                    tb_game.short_description AS ShortDescription, 
                    tb_belonging.position AS Position
                FROM tb_game
                INNER JOIN tb_belonging ON tb_game.id = tb_belonging.game_id
                WHERE tb_belonging.list_id = @ListId
                ORDER BY tb_belonging.position";

            var parametros = new DynamicParameters();
            parametros.Add("@ListId", listId, DbType.Int64);

            try
            {
                _logger.LogInformation($"Buscando jogos pela lista com ID={listId}");
                var result = await _context.Database.GetDbConnection().QueryAsync<GameMinDTO>(query, parametros);
                _logger.LogInformation($"Busca concluída com sucesso para a lista com ID={listId}");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar jogos para a lista com ID={listId}");
                throw;
            }
        }

        async Task<IEnumerable<Game>> IGameRepository.SearchByListAsync(long listId)
        {
            if (listId <= 0)
            {
                throw new ArgumentException("Identificação não pode ser nula nem 0");
            }
            var query = @"
                SELECT g.GameId, g.title, g.game_year, g.img_url, g.short_description
                FROM tb_game g
                INNER JOIN tb_belonging b ON g.GameId = b.game_id
                WHERE b.list_id = @ListId
                ORDER BY b.position";


            var parametros = new DynamicParameters();
            parametros.Add("@ListId", listId);

            try
            {
                _logger.LogInformation($"Buscando jogos pela lista com ID={listId}");
                var result = await _context.Database.GetDbConnection().QueryAsync<Game>(query, parametros);
                _logger.LogInformation($"Busca concluída com sucesso para a lista com ID={listId}");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar jogos para a lista com ID={listId}");
                throw;
            }

            throw new NotImplementedException();
        }
    }
}