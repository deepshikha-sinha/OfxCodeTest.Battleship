using OfxCodeTest.Battleship.Services.Models;

namespace OfxCodeTest.Battleship.Services.Interfaces
{
    public interface IBoardService
    {
        Board CreateBoard();
        Board GetBoard();
        ShotType AttackShip(Coordinate shotCoordinates);
    }
}
