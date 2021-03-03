using Microsoft.AspNetCore.Mvc;
using OfxCodeTest.Battleship.Controller;
using OfxCodeTest.Battleship.Services.Implementation;
using OfxCodeTest.Battleship.Services.Interfaces;
using OfxCodeTest.Battleship.Services.Models;

namespace OfxCodeTest.Battleship.Tests
{
    public class BattleshipTestsBase
    {
        protected IActionResult Result;

        private readonly GameController _gameController;
        
        public BattleshipTestsBase()
        {
            var shipValidator = new ShipValidator();
            var boardService = new BoardService();
            var playerService  = new PlayerService(boardService, shipValidator);
            IGameService gameService = new GameService(playerService);
            _gameController = new GameController(gameService, playerService, boardService);
        }

        protected void CreateBoard()
        {
            Result = _gameController.CreateGameBoard();
        }

        protected void AddShipToBoard(int shipLength, ShipOrientation shipOrientation, int shipStartRow, int shipStartColumn)
        {
            Result = _gameController.AddBattleshipsOnBoard(shipLength, shipOrientation, shipStartRow, shipStartColumn);
        }

        protected void AttackShip(int shipStartRow, int shipStartColumn)
        {
            Result = _gameController.AttackShip(shipStartRow, shipStartColumn);
            
        }
    }
}