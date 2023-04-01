using System;

namespace Life
{
	public static class BoardBuilder
	{
		public static Board Build(BoardSettings settings)
		{
			var columns = settings.Width / settings.CellSize;
			var rows = settings.Height / settings.CellSize;

			var cells = new Cell[columns, rows];

			for (var x = 0; x < columns; x++)
			{
				for (var y = 0; y < rows; y++)
				{
					cells[x, y] = new Cell();
				}
			}

			ConnectNeighbors(cells, (int) columns, (int) rows);
			Randomize(cells, settings.LiveDensity);

			return new Board(cells, settings.CellSize);
		}

		private static readonly Random Random = new Random();

		private static void Randomize(Cell[,] cells, double liveDensity)
		{
			foreach (var cell in cells)
				cell.IsAlive = Random.NextDouble() < liveDensity;
		}

		private static void ConnectNeighbors(Cell[,] cells, int columns, int rows)
		{
			for (var x = 0; x < columns; x++)
			{
				for (var y = 0; y < rows; y++)
				{
					var xL = (x > 0) ? x - 1 : columns - 1;
					var xR = (x < columns - 1) ? x + 1 : 0;

					var yT = (y > 0) ? y - 1 : rows - 1;
					var yB = (y < rows - 1) ? y + 1 : 0;

					cells[x, y].Neighbors.Add(cells[xL, yT]);
					cells[x, y].Neighbors.Add(cells[x, yT]);
					cells[x, y].Neighbors.Add(cells[xR, yT]);
					cells[x, y].Neighbors.Add(cells[xL, y]);
					cells[x, y].Neighbors.Add(cells[xR, y]);
					cells[x, y].Neighbors.Add(cells[xL, yB]);
					cells[x, y].Neighbors.Add(cells[x, yB]);
					cells[x, y].Neighbors.Add(cells[xR, yB]);
				}
			}
		}
	}
}
