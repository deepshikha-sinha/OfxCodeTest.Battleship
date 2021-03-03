
using System;
using System.Collections.Generic;
using System.Linq;
using OfxCodeTest.Battleship.Services.Interfaces;
using OfxCodeTest.Battleship.Services.Models;

namespace OfxCodeTest.Battleship.Services.Implementation
{
    public class PlayerService : IPlayerService
    {
        private readonly IBoardService _boardService;
        private readonly IShipValidator _shipValidator;

        public Player Player { get; set; }
    
        public PlayerService(IBoardService boardService, IShipValidator shipValidator)
        {
            _boardService = boardService;
            _shipValidator = shipValidator;
        }
        public Player CreatePlayer()
        {
            var board = _boardService.CreateBoard();
            if (board == null)
                throw new Exception("Cannot Create Board");
            Player = new Player
            {
                Name = "Single Player",
                Board = board,
                Ships = new List<Ship>()
            };

            return Player;

        }

        public Player GetPlayer()
        {
            return Player;
        }



        public Ship PlaceShip(int shipLength, ShipOrientation shipOrientation, Coordinate shipPositionCoordinates)
        {
            if (Player?.Board == null)
                throw new Exception("Board Does not exist");

            var ship = new Ship { ShipId = $"Ship_{Player.ShipCount + 1}",  Length = shipLength, Orientation = shipOrientation, Coordinates = new List<Coordinate>() };


            if (!_shipValidator.ValidateShipDimensions(Player.Board, ship, shipPositionCoordinates.Row, shipPositionCoordinates.Column))
                throw new IndexOutOfRangeException();

            _shipValidator.CalculateShipPosition(Player.Board, ship, shipPositionCoordinates.Row, shipPositionCoordinates.Column);

            Player.Ships ??= new List<Ship>();
            Player.Ships.Add(ship);

            return ship;
        }

        public ShotType ProcessAttack(Coordinate shotCoordinates)
        {
            if(Player?.Board == null)
             throw new Exception("Board Does not exist");
            if (Player?.Ships == null || !Player.Ships.Any()) 
                return ShotType.Miss;
            
            var ship = Player.Ships.First(x => x.Coordinates.Any(c => c.Row == shotCoordinates.Row && c.Column == shotCoordinates.Column));
            if (ship == null)
                throw new Exception("Invalid Ship");

            Player.Ships.First(s => s.ShipId.Equals(ship.ShipId)).Hits++;

            return ship.IsSunk ? ShotType.HitAndSunk : ShotType.Hit;
        }
    }
}
