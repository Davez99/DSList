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
    [Route("games")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;
        private readonly ILogger<GameController> _logger;

        public GameController(GameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<GameDTO>> FindById(long id)
        {
            try
            {
                var game = await _gameService.FindByIdAsync(id);
                if (game == null)
                {
                    _logger.LogWarning($"Game with ID {id} not found.");
                    return NotFound();
                }

                return Ok(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching game by ID.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameMinDTO>>> FindAll()
        {
            try
            {
                var games = await _gameService.FindAllAsync();
                return Ok(games);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all games.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}