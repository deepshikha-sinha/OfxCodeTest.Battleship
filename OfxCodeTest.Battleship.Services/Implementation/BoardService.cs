using System;
using System.Collections.Generic;
using OfxCodeTest.Battleship.Services.Extensions;
using OfxCodeTest.Battleship.Services.Interfaces;
using OfxCodeTest.Battleship.Services.Models;

namespace OfxCodeTest.Battleship.Services.Implementation
{
    public class BoardService : IBoardService
    {
        private Board Board { get; set; }

        public Board CreateBoard()
        {
            try
            {
                Board = new Board{Cells = new List<Cell>()};
                for (var x = 1; x <= 10; x++)
                {
                    for (var y = 1; y <= 10; y++)
                    {
                        Board.Cells.Add(new Cell(x, y));
                    }
                }

                return Board;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while creating board : {ex.Message}");
            }
        }

        public Board GetBoard()
        {
            return Board;
        }

        public ShotType AttackShip(Coordinate shotCoordinates)
        {
           if (Board == null)
                throw new Exception("Board Does not exist");

           var cell = Board.Cells.At(shotCoordinates.Row, shotCoordinates.Column);
           if (!cell.IsOccupied)
               return ShotType.Miss;
           if (cell.HasBeenHit) return ShotType.AlreadyHit;
            cell.HasBeenHit = true;
           return ShotType.Hit;
        }
    }
}
