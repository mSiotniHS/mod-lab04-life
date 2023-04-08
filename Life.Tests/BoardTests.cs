using Xunit;

namespace Life.Tests
{
	public abstract class BoardTests
	{
		private static readonly BoardSettings Settings = new BoardSettings
		{
			Width = 25,
			Height = 25,
			CellSize = 1
		};

		public class AddFragment
		{
			[Fact]
			public void AddsFragmentCorrectly()
			{
				var board = new Board(Settings);
				var fragment = Fragment.FromString("010\n100\n011");

				board.AddFragment(fragment);

				for (var row = 0; row < fragment.Rows; row++)
				{
					for (var column = 0; column < fragment.Columns; column++)
					{
						Assert.Equal(fragment.Matrix[row][column], board.Cells[row, column].IsAlive);
					}
				}
			}
		}

		public class ToFragment
		{
			[Fact]
			public void GeneratesCorrectFragment()
			{
				var board = new Board(new [,]
				{
					{ new Cell {IsAlive = true} , new Cell {IsAlive = false} },
					{ new Cell {IsAlive = false}, new Cell {IsAlive = true}  }
				}, 1);

				var expected = Fragment.FromString("10\n01");
				var actual = board.ToFragment();

				Assert.Equal(expected.Matrix, actual.Matrix);
			}
		}

		public class Advance
		{
			[Fact]
			public void WorksCorrectly()
			{
				var fragment = Fragment.FromString("0010\n1010\n0100\n0000");
				var board = new Board(new BoardSettings
				{
					Width = 4,
					Height = 4,
					CellSize = 1
				});

				board.AddFragment(fragment);
				board.Advance();

				const string expected1 = "0101\n0011\n0100\n0000";
				var actual1 = board.ToFragment().ToString();
				Assert.Equal(expected1, actual1);

				board.Advance();

				const string expected2 = "1001\n0101\n0010\n1010";
				var actual2 = board.ToFragment().ToString();
				Assert.Equal(expected2, actual2);

				board.Advance();

				const string expected3 = "0000\n0101\n1010\n1010";
				var actual3 = board.ToFragment().ToString();
				Assert.Equal(expected3, actual3);
			}
		}
	}
}
