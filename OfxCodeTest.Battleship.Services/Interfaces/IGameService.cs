using OfxCodeTest.Battleship.Services.Models;

namespace OfxCodeTest.Battleship.Services.Interfaces
{
    public interface IGameService
    {
        public Game CreateGame();
        public Game GetGame();

    }
}
