using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OfxCodeTest.Battleship.Services.Models;
using Xunit;

namespace OfxCodeTest.Battleship.Tests
{
    public class ExceptionIsThrownIfBoardIsNotPresent : BattleshipTestsBase
    {
        [Fact]
        public void AddingShipToBoardReturnsThrowsAnExceptionIfBoardIsNotCreated()
        {
            AddShipToBoard(2, ShipOrientation.Horizontal, 2, 2);
            var objectResult = Result.Should().BeOfType<ObjectResult>().Subject;
            var exception = objectResult.Value.Should().BeOfType<Exception>().Subject;
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("Invalid Board", exception.Message);

        }

        [Fact]
        public void AttackShipThrowsAnExceptionIfBoardIsNotCreated()
        {
            AttackShip(2, 2);
            var objectResult = Result.Should().BeOfType<ObjectResult>().Subject;
            var exception = objectResult.Value.Should().BeOfType<Exception>().Subject;
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("Invalid Board", exception.Message);

        }
    }
}
