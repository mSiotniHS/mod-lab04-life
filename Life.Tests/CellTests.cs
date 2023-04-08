using Xunit;

namespace Life.Tests
{
	public abstract class CellTests
	{
		public class Ctor
		{
			[Fact]
			public void CreatesInstanceWithExpectedState()
			{
				var actual = new Cell();

				Assert.False(actual.IsAlive);
				Assert.Empty(actual.Neighbors);

				actual.Advance();
				Assert.False(actual.IsAlive);
			}
		}

		public abstract class DetermineNextLiveState
		{
			public class IfDead
			{
				[Fact]
				public void DecidesNotToComeAliveWithOver3Neighbors()
				{
					var cell = new Cell
					{
						IsAlive = false,
						Neighbors =
						{
							new Cell {IsAlive = true},
							new Cell {IsAlive = true},
							new Cell {IsAlive = true},
							new Cell {IsAlive = true},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false}
						}
					};

					cell.DetermineNextLiveState();
					cell.Advance();
					Assert.False(cell.IsAlive);
				}

				[Fact]
				public void DecidesNotToComeAliveWithUnder3Neighbors()
				{
					var cell = new Cell
					{
						IsAlive = false,
						Neighbors =
						{
							new Cell {IsAlive = true},
							new Cell {IsAlive = true},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false}
						}
					};

					cell.DetermineNextLiveState();
					cell.Advance();
					Assert.False(cell.IsAlive);
				}

				[Fact]
				public void DecidesToComeAliveWith3Neighbors()
				{
					var cell = new Cell
					{
						IsAlive = false,
						Neighbors =
						{
							new Cell {IsAlive = true},
							new Cell {IsAlive = true},
							new Cell {IsAlive = true},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false}
						}
					};

					cell.DetermineNextLiveState();
					cell.Advance();
					Assert.True(cell.IsAlive);
				}
			}

			public class IfAlive
			{
				[Fact]
				public void DecidesToContinueLivingWith2Neighbors()
				{
					var cell = new Cell
					{
						IsAlive = true,
						Neighbors =
						{
							new Cell {IsAlive = true},
							new Cell {IsAlive = true},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false}
						}
					};

					cell.DetermineNextLiveState();
					cell.Advance();
					Assert.True(cell.IsAlive);
				}

				[Fact]
				public void DecidesToContinueLivingWith3Neighbors()
				{
					var cell = new Cell
					{
						IsAlive = false,
						Neighbors =
						{
							new Cell {IsAlive = true},
							new Cell {IsAlive = true},
							new Cell {IsAlive = true},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false}
						}
					};

					cell.DetermineNextLiveState();
					cell.Advance();
					Assert.True(cell.IsAlive);
				}

				[Fact]
				public void DecidesToDieWithOver3Neighbors()
				{
					var cell = new Cell
					{
						IsAlive = false,
						Neighbors =
						{
							new Cell {IsAlive = true},
							new Cell {IsAlive = true},
							new Cell {IsAlive = true},
							new Cell {IsAlive = true},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false}
						}
					};

					cell.DetermineNextLiveState();
					cell.Advance();
					Assert.False(cell.IsAlive);
				}

				[Fact]
				public void DecidesToDieWithUnder2Neighbors()
				{
					var cell = new Cell
					{
						IsAlive = false,
						Neighbors =
						{
							new Cell {IsAlive = true},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false},
							new Cell {IsAlive = false}
						}
					};

					cell.DetermineNextLiveState();
					cell.Advance();
					Assert.False(cell.IsAlive);
				}
			}
		}
	}
}
