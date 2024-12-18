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

namespace DSList.Repository
{
    public class GameRepository
    {
        private readonly GameContext _context;
        private readonly ILogger<GameRepository> _logger;

        public GameRepository(GameContext context, ILogger<GameRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<GameMinDTO>> SearchByListAsync(long listId)
        {
            if (listId <= 0)
            {
                throw new ArgumentException("O ID da lista deve ser um valor positivo.");
            }

            var query = @"
                SELECT 
                    tb_game.id, 
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
    }
}