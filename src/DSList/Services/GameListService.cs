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
    public class GameListService
    {
        private readonly IGameListRepository _gameListRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<GameListService> _logger;

        public GameListService(
            IGameListRepository gameListRepository,
            IGameRepository gameRepository,
            ILogger<GameListService> logger)
        {
            _gameListRepository = gameListRepository;
            _gameRepository = gameRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<GameListDTO>> FindAllAsync()
        {
            try
            {
                _logger.LogInformation("Buscando todas as listas de jogos.");
                var gameLists = await _gameListRepository.FindAllAsync();
                return gameLists.Select(list => new GameListDTO(list.ListId, list.Name));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as listas de jogos.");
                throw;
            }
        }

        public async Task<GameListDTO> FindByIdAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("O ID deve ser um valor positivo.");
            }

            try
            {
                _logger.LogInformation($"Buscando lista de jogos com ID={id}");
                var gameList = await _gameListRepository.FindByIdAsync(id);

                if (gameList == null)
                {
                    _logger.LogWarning($"Lista de jogos com ID={id} não encontrada.");
                    throw new KeyNotFoundException("Lista de jogos não encontrada.");
                }

                return new GameListDTO(gameList.ListId, gameList.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar lista de jogos com ID={id}");
                throw;
            }
        }

        public async Task MoveAsync(long listId, int sourceIndex, int destinationIndex)
        {
            if (listId <= 0 || sourceIndex < 0 || destinationIndex < 0)
            {
                throw new ArgumentException("Os parâmetros devem ser válidos e positivos.");
            }

            try
            {
                _logger.LogInformation($"Reordenando jogos na lista ID={listId}, SourceIndex={sourceIndex}, DestinationIndex={destinationIndex}");
                var gameList = (await _gameRepository.SearchByListAsync(listId)).ToList();

                if (gameList == null || !gameList.Any())
                {
                    throw new InvalidOperationException("Lista de jogos está vazia ou não encontrada.");
                }

                var game = gameList[sourceIndex];
                gameList.RemoveAt(sourceIndex);
                gameList.Insert(destinationIndex, game);

                int min = Math.Min(sourceIndex, destinationIndex);
                int max = Math.Max(sourceIndex, destinationIndex);

                for (int i = min; i <= max; i++)
                {
                    await _gameListRepository.UpdateBelongingPositionAsync(listId, gameList[i].GameId, i);
                }

                _logger.LogInformation("Reordenação concluída com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao reordenar jogos na lista ID={listId}");
                throw;
            }
        }
    }
}
