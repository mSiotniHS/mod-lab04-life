using System.Collections.Generic;
using Life.Analytics;
using Xunit;

namespace Life.Tests
{
	public abstract class AnalyticMethodsTests
	{
		public class VerticalSymmetryRate
		{
			[Theory]
			[MemberData(nameof(WorksCorrectlyData))]
			public void WorksCorrectly(uint width, uint height, string rawFragment, double rate)
			{
				var board = new Board(new BoardSettings {Width = width, Height = height, CellSize = 1});
				board.AddFragment(Fragment.FromString(rawFragment));

				var actual = AnalyticMethods.VerticalSymmetryRate(board);
				Assert.Equal(rate, actual);
			}

			public static IEnumerable<object[]> WorksCorrectlyData()
			{
				yield return new object[] { 2, 2, "00\n11", 1.0 };
				yield return new object[] { 2, 2, "01\n11", 0.5 };
				yield return new object[] { 2, 2, "01\n10", 0 };
			}
		}
	}
}
