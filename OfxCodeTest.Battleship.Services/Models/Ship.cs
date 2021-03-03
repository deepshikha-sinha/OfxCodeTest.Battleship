using System.Collections.Generic;

namespace OfxCodeTest.Battleship.Services.Models
{
    public class Ship
    {
        public string ShipId { get; set; }
        public ShipOrientation Orientation { get; set; }
        public int Length { get; set; }

        public int Hits { get; set; }

        public bool IsSunk => Hits >= Length;

        public List<Coordinate> Coordinates { get; set; }
    }
}
