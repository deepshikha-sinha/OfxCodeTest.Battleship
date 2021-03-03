using OfxCodeTest.Battleship.Services.Models;

namespace OfxCodeTest.Battleship.Services.Interfaces
{
    public interface IShipValidator
    {
        bool ValidateShipDimensions(Board board, Ship ship, int startRow, int startColumn);
        void CalculateShipPosition(Board board, Ship ship, int startRow, int startColumn);
    }
}
