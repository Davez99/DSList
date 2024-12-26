using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DSList.DTOs;
using DSList.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DSList.Controllers
{
    [ApiController]
    [Route("lists")]
    public class GameListController : ControllerBase
    {
        private readonly GameListService _gameListService;
        private readonly GameService _gameService;
        private readonly ILogger<GameListController> _logger;

        public GameListController(GameListService gameListService, GameService gameService, ILogger<GameListController> logger)
        {
            _gameListService = gameListService;
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<GameListDTO>> FindById(long id)
        {
            try
            {
                var list = await _gameListService.FindByIdAsync(id);
                if (list == null)
                {
                    _logger.LogWarning($"Game list with ID {id} not found.");
                    return NotFound();
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching game list by ID.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameListDTO>>> FindAll()
        {
            try
            {
                var lists = await _gameListService.FindAllAsync();
                return Ok(lists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all game lists.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{listId:long}/games")]
        public async Task<ActionResult<IEnumerable<GameMinDTO>>> FindGames(long listId)
        {
            try
            {
                var games = await _gameService.FindByGameListAsync(listId);
                return Ok(games);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching games for the list.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("{listId:long}/replacement")]
        public async Task<IActionResult> Move(long listId, [FromBody] ReplacementDTO body)
        {
            
            try
            {
                await _gameListService.MoveAsync(listId, body.SourceIndex, body.DestinationIndex);
                return NoContent();
            }
            catch (Exception ex)
            { 
                _logger.LogError(ex, "Error reordering games in the list.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}