using System;

namespace Life
{
	public static class BoardBuilder
	{
		private static readonly Random Random = new Random();

		public static Board Randomized(BoardSettings settings, double liveDensity)
		{
			var board = new Board(settings);
			Randomize(board.Cells, liveDensity);

			return board;
		}

		private static void Randomize(Cell[,] cells, double liveDensity)
		{
			foreach (var cell in cells)
				cell.IsAlive = Random.NextDouble() < liveDensity;
		}
	}
}
