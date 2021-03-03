namespace OfxCodeTest.Battleship.Services.Models
{
    public class Cell
    {
        public Coordinate Coordinate { get; set; }
        public bool IsOccupied { get; set; }
        public bool HasBeenHit { get; set; }

        public Cell(int row, int column)
        {
            Coordinate = new Coordinate(row, column);
            IsOccupied = false;
            HasBeenHit = false;

        }
    }
}
