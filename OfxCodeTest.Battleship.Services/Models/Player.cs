using System.Collections.Generic;

namespace OfxCodeTest.Battleship.Services.Models
{
    public class Player
    {
        
        public string Name { get; set; }
        public Board Board { get; set; }
        public List<Ship> Ships { get; set; }

        public int ShipCount => Ships?.Count ?? 0;


    }
}
