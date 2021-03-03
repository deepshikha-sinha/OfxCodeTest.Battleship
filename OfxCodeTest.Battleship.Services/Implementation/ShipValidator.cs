using System.Collections.Generic;
using System.Linq;
using OfxCodeTest.Battleship.Services.Extensions;
using OfxCodeTest.Battleship.Services.Interfaces;
using OfxCodeTest.Battleship.Services.Models;

namespace OfxCodeTest.Battleship.Services.Implementation
{
    public class ShipValidator : IShipValidator
    {
        public bool ValidateShipDimensions(Board board, Ship ship, int startRow, int startColumn)
        {
            var endColumn = ship.Orientation == ShipOrientation.Horizontal ? startColumn + ship.Length - 1 : startColumn;
            var endRow = ship.Orientation == ShipOrientation.Vertical ? startRow + ship.Length - 1 : startRow;

            if (endRow > 10 || endColumn > 10)
            {
                return false;
            }

            //Check if specified cells are occupied
            var affectedCells = board.Cells.Range(startRow, startColumn, endRow, endColumn);
            return !affectedCells.Any(x => x.IsOccupied);
        }

        public void CalculateShipPosition(Board board, Ship ship, int startRow, int startColumn)
        {
            var endColumn = ship.Orientation == ShipOrientation.Horizontal ? startColumn + ship.Length - 1 : startColumn;
            var endRow = ship.Orientation == ShipOrientation.Vertical ? startRow + ship.Length - 1 : startRow;
            //Get the Coordinate of the Board where Ships are placed
            ship.Coordinates = new List<Coordinate>();
            if (ship.Orientation == ShipOrientation.Horizontal)
            {
                for (var i = startColumn; i <= endColumn; i++)
                {
                    ship.Coordinates.Add(new Coordinate(startRow, i ));
                }
                
            }
            else
            {
                for (var i = startRow; i <= endRow; i++)
                {
                    ship.Coordinates.Add(new Coordinate(i, startColumn));
                }
            }

            var affectedCells = board.Cells.Range(startRow, startColumn, endRow, endColumn);

            foreach (var cell in affectedCells)
            {
                cell.IsOccupied = true;
            }
        }
    }
}
