using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OfxCodeTest.Battleship.Services.Models;
using Xunit;

namespace OfxCodeTest.Battleship.Tests
{
    public class ShipPlacementTests : BattleshipTestsBase
    {
        public ShipPlacementTests()
        {
            Setup();
        }

        private void Setup()
        {
            CreateBoard();
        }


        [Theory]
        [InlineData(2, ShipOrientation.Horizontal, 2, 2)]
        [InlineData(5, ShipOrientation.Vertical, 6, 5)]
        [InlineData(3, ShipOrientation.Horizontal, 4, 8)]
        [InlineData(4, ShipOrientation.Vertical, 3, 1)]
        public void AddingShipToBoardReturnsAValidShipId(int length, ShipOrientation orientation, int row, int column)
        {
            AddShipToBoard(length, orientation, row, column);
            var okObjectResult = Result.Should().BeOfType<OkObjectResult>().Subject;
            var ship = okObjectResult.Value.Should().BeAssignableTo<Ship>().Subject;
            Assert.NotEmpty(ship.ShipId);
            Assert.Equal(length, ship.Coordinates.Count);
            Assert.Equal(length, ship.Length);
            Assert.Equal(0, ship.Hits);
        }

        [Theory]
        [InlineData(7, ShipOrientation.Vertical, 6, 5)]
        [InlineData(4, ShipOrientation.Horizontal, 4, 8)]
        public void AddingShipOutsideBoardBoundaryThrowsException(int length, ShipOrientation orientation, int row,
            int column)
        {
            AddShipToBoard(length, orientation, row, column);
            var badRequestObjectResult = Result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestObjectResult.Value.Should().Be("Ship coordinates fall outside board boundary");
        }

    }
}
