using System.Collections.Generic;
using System.Linq;
using OfxCodeTest.Battleship.Services.Models;

namespace OfxCodeTest.Battleship.Services.Extensions
{
    public static class CellExtension
    {
        public static List<Cell> Range(this List<Cell> cells, int startRow, int startColumn, int endRow, int endColumn)
        {
            return cells.Where(x => x.Coordinate.Row >= startRow
                                    && x.Coordinate.Column >= startColumn
                                    && x.Coordinate.Row <= endRow
                                    && x.Coordinate.Column <= endColumn).ToList();

        }

        public static Cell At(this List<Cell> cells, int row, int column)
        {
            return cells.First(x => x.Coordinate.Row == row && x.Coordinate.Column == column);
        }
       
    }
}
