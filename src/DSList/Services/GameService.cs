using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSList.DTOs;
using DSList.Entities;
using DSList.Interfaces;
using DSList.Repository;
using Microsoft.Extensions.Logging;

namespace DSList.Services
{
    public class GameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<GameService> _logger;

        public GameService(IGameRepository gameRepository, ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }

        public async Task<GameDTO> FindByIdAsync(long gameId)
        {
            if (gameId <= 0)
            {
                throw new ArgumentException("O ID do jogo deve ser um valor positivo.");
            }

            try
            {
                _logger.LogInformation($"Buscando jogo com ID={gameId}");

                //Esse metodo tem que ser criado no Repository
                var game = await _gameRepository.GameById(gameId);

                if (game == null)
                {
                    _logger.LogWarning($"Jogo com ID={gameId} não encontrado.");
                    throw new KeyNotFoundException("Jogo não encontrado.");
                }

                return game;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar jogo com ID={gameId}");
                throw;
            }
        }

        public async Task<IEnumerable<GameMinDTO>> FindAllAsync()
        {
            try
            {
                _logger.LogInformation("Buscando todos os jogos.");

                //Esse metodo tem que ser criado no Repository
                var games = await _gameRepository.AllGames();
                return games.Select(game => new GameMinDTO(game));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os jogos.");
                throw;
            }
        }

        public async Task<IEnumerable<GameMinDTO>> FindByGameListAsync(long listId)
        {
            if (listId <= 0)
            {
                throw new ArgumentException("O ID da lista deve ser um valor positivo.");
            }

            try
            {
                _logger.LogInformation($"Buscando jogos pela lista com ID={listId}");

                //Esse metodo tem que ser criado no Repository
                var games = await _gameRepository.SearchByListAsync(listId);
                return games.Select(game => new GameMinDTO(game));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar jogos para a lista com ID={listId}");
                throw;
            }
        }
    }
}
