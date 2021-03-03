namespace OfxCodeTest.Battleship.Services.Models
{
    public class Coordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Coordinate(int x, int y)
        {
            Row = x;
            Column = y;
        }
    }
}
