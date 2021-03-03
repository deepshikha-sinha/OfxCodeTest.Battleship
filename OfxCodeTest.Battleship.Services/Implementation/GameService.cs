using OfxCodeTest.Battleship.Services.Interfaces;
using OfxCodeTest.Battleship.Services.Models;

namespace OfxCodeTest.Battleship.Services.Implementation
{
    public class GameService : IGameService
    {
        private readonly IPlayerService _playerService;
        private Game Game { get; set; }
        public GameService(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        

        public Game CreateGame()
        {
            Game = new Game
            {
                Player = _playerService.CreatePlayer()
            };
            return Game;
        }

        public Game GetGame()
        {
            return Game;
        }
    }
}
