using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OfxCodeTest.Battleship.Services.Models;
using Xunit;

namespace OfxCodeTest.Battleship.Tests
{
    public class BattleshipAttackStatusTests : BattleshipTestsBase
    {
        public BattleshipAttackStatusTests()
        {
            Setup();
        }

        private void Setup()
        {
            CreateBoard();
            AddShipToBoard(2, ShipOrientation.Horizontal, 2, 2);
            AddShipToBoard(5, ShipOrientation.Vertical, 6, 5);
            AddShipToBoard(3, ShipOrientation.Horizontal, 4, 8);
            AddShipToBoard(4, ShipOrientation.Vertical, 3, 1);
        }



        [Theory]
        [InlineData(2, 2, ShotType.Hit)]
        [InlineData(8, 5, ShotType.Hit)]
        public void AttackOccupiedCoordinatesReturnsHitStatus(int row, int column, ShotType shotType)
        {
            AttackShip(row, column);
            var okObjectResult = Result.Should().BeOfType<OkObjectResult>().Subject;
            var attackStatus = okObjectResult.Value.Should().BeAssignableTo<ShotType>().Subject;
            Assert.Equal(shotType, attackStatus);
        }

        [Theory]
        [InlineData(6, 8, ShotType.Miss)]
        [InlineData(9, 9, ShotType.Miss)]
        public void AttackEmptyCoordinatesReturnsMissedStatus(int row, int column, ShotType shotType)
        {
            AttackShip(row, column);
            var badRequestObjectResult = Result.Should().BeOfType<BadRequestObjectResult>().Subject;
            var attackStatus = badRequestObjectResult.Value.Should().BeAssignableTo<ShotType>().Subject;
            Assert.Equal(shotType, attackStatus);
        }

        [Fact]
        public void AttackingSameCoordinateReturnsAlreadyHit()
        {
            AttackShip(2,2);
            var okObjectResult = Result.Should().BeOfType<OkObjectResult>().Subject;
            var attackStatus = okObjectResult.Value.Should().BeAssignableTo<ShotType>().Subject;
            Assert.Equal(ShotType.Hit, attackStatus);
            
            AttackShip(2, 2);
            var badRequestObjectResult = Result.Should().BeOfType<BadRequestObjectResult>().Subject;
            attackStatus = badRequestObjectResult.Value.Should().BeAssignableTo<ShotType>().Subject;
            Assert.Equal(ShotType.AlreadyHit, attackStatus);

        }

        [Fact]
        public void HittingAllCoordinatesOfAShipReturnsSunkStatus()
        {
            AttackShip(2, 2);
            var okObjectResult = Result.Should().BeOfType<OkObjectResult>().Subject;
            var attackStatus = okObjectResult.Value.Should().BeAssignableTo<ShotType>().Subject;
            Assert.Equal(ShotType.Hit, attackStatus);
            
            AttackShip(2, 3);
            var badRequestObjectResult = Result.Should().BeOfType<BadRequestObjectResult>().Subject;
            attackStatus = badRequestObjectResult.Value.Should().BeAssignableTo<ShotType>().Subject;
            Assert.Equal(ShotType.HitAndSunk, attackStatus);

        }


    }
}
