using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfxCodeTest.Battleship.Services.Interfaces;
using OfxCodeTest.Battleship.Services.Models;

namespace OfxCodeTest.Battleship.Controller
{
    /// <summary>
    /// A single player Battleship Game
    /// </summary>
    [Route("")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IPlayerService _playerService;
        private readonly IBoardService _boardService;

        public GameController(IGameService gameService, IPlayerService playerService, IBoardService boardService)
        {
            _gameService = gameService;
            _playerService = playerService;
            _boardService = boardService;
        }


        /// <summary>
        /// Creates a Single Player player game and assign an empty 10 X 10 board to the player
        /// </summary>
        /// <returns>A singleton instance of board, with a single player assigned to a single board</returns>
        [HttpPost("board/create")]
        [ProducesResponseType(typeof(Board), 200)]
        [ProducesResponseType(typeof(ObjectResult), 500)]
        public IActionResult CreateGameBoard()
        {
            try
            {
                var game = _gameService.CreateGame();

                return game?.Player?.Board != null ? Ok(game.Player.Board) : StatusCode(StatusCodes.Status500InternalServerError, "Board could not be created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Adds Ship To the board
        /// </summary>
        /// <param name="shipLength">Length of the ship - Must be between 1 and 10</param>
        /// <param name="shipOrientation">Orientation of the ship - Must be Horizontal or Vertical</param>
        /// <param name="shipStartRow">Start Row Position of the ship on the board - Must be between 1 and 10</param>
        /// <param name="shipStartColumn">Start Column Position the ship on the board - Must be between 1 and 10</param>
        /// <returns>Instance of the Ship if Valid, Bad Request If Invalid</returns>
        [HttpPost("ship/place")]
        [ProducesResponseType(typeof(Ship), 200)]
        [ProducesResponseType(typeof(ObjectResult), 500)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult AddBattleshipsOnBoard([Required, Range(1,10) ]int shipLength,
            [Required] ShipOrientation shipOrientation,
            [Required, Range(1, 10)] int shipStartRow,
            [Required, Range(1, 10)] int shipStartColumn)
        {
            try
            {
                var game = _gameService.GetGame();

                if (game?.Player.Board == null)
                    throw new Exception("Invalid Board");

                var ship = _playerService.PlaceShip(shipLength, shipOrientation,
                    new Coordinate(shipStartRow, shipStartColumn));
                return ship != null
                    ? Ok(ship)
                    : StatusCode(StatusCodes.Status500InternalServerError, "Ship could not be placed");
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest("Ship coordinates fall outside board boundary");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Attacks the ship at given coordinates and reports back the status
        /// </summary>
        /// <param name="shotRow">Row Coordinate to attack on the board - Must be between 1 and 10</param>
        /// <param name="shotColumn">Column Coordinate to attack on the board - Must be between 1 and 10</param>
        /// <returns>Attack Status - HIT, MISS, ALREADYHIT, HITANDSUNK</returns>

        [HttpPost("attack")]
        [ProducesResponseType(typeof(ShotType), 200)]
        [ProducesResponseType(typeof(ObjectResult), 500)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult AttackShip([Required, Range(1, 10)] int shotRow, [Required, Range(1, 10)] int shotColumn)
        {
            try
            {
                var game = _gameService.GetGame();

                if (game?.Player.Board == null)
                    throw new Exception("Invalid Board");

                var shotType = _boardService.AttackShip(new Coordinate(shotRow, shotColumn));
                if(shotType == ShotType.Hit)
                    shotType = _playerService.ProcessAttack(new Coordinate(shotRow, shotColumn));
                return shotType == ShotType.Hit ? Ok(shotType) : BadRequest(shotType);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
