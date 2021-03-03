using OfxCodeTest.Battleship.Services.Models;

namespace OfxCodeTest.Battleship.Services.Interfaces
{
    public interface IPlayerService
    {
        Player CreatePlayer();
        Player GetPlayer();
        Ship PlaceShip(int shipLength, ShipOrientation shipOrientation, Coordinate shipPositionCoordinates);

        ShotType ProcessAttack(Coordinate shotCoordinates);


    }
}
