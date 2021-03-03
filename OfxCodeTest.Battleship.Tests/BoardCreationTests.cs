using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OfxCodeTest.Battleship.Services.Models;
using Xunit;

namespace OfxCodeTest.Battleship.Tests
{
    public class BoardCreationTests : BattleshipTestsBase
    {

        [Fact]
        public void CreateBoardCreatesATenByTenEmptyBoard()
        {
            CreateBoard();
            var okObjectResult = Result.Should().BeOfType<OkObjectResult>().Subject;
            var board = okObjectResult.Value.Should().BeAssignableTo<Board>().Subject;
            Assert.NotNull(board);
            Assert.NotNull(board.Cells);
            Assert.Equal(100, board.Cells.Count);
        }
    }
}
