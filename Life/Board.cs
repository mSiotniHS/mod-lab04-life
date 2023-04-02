﻿namespace Life
{
	public class Board
	{
		public readonly Cell[,] Cells;
		public readonly uint CellSize;

		public int Columns => Cells.GetLength(0);
		public int Rows => Cells.GetLength(1);

		public Board(BoardSettings settings)
		{
			CellSize = settings.CellSize;

			var columns = settings.Width / settings.CellSize;
			var rows = settings.Height / settings.CellSize;

			Cells = new Cell[columns, rows];

			for (var x = 0; x < columns; x++)
			{
				for (var y = 0; y < rows; y++)
				{
					Cells[x, y] = new Cell();
				}
			}

			ConnectNeighbors();
		}

		public Board(Cell[,] cells, uint cellSize)
		{
			Cells = cells; // WARNING: копировать?
			CellSize = cellSize;

			ConnectNeighbors();
		}

		public void Advance()
		{
			foreach (var cell in Cells)
				cell.DetermineNextLiveState();
			foreach (var cell in Cells)
				cell.Advance();
		}

		private void ConnectNeighbors()
		{
			for (var x = 0; x < Columns; x++)
			{
				for (var y = 0; y < Rows; y++)
				{
					var xL = (x > 0) ? x - 1 : Columns - 1;
					var xR = (x < Columns - 1) ? x + 1 : 0;

					var yT = (y > 0) ? y - 1 : Rows - 1;
					var yB = (y < Rows - 1) ? y + 1 : 0;

					Cells[x, y].Neighbors.Add(Cells[xL, yT]);
					Cells[x, y].Neighbors.Add(Cells[x, yT]);
					Cells[x, y].Neighbors.Add(Cells[xR, yT]);
					Cells[x, y].Neighbors.Add(Cells[xL, y]);
					Cells[x, y].Neighbors.Add(Cells[xR, y]);
					Cells[x, y].Neighbors.Add(Cells[xL, yB]);
					Cells[x, y].Neighbors.Add(Cells[x, yB]);
					Cells[x, y].Neighbors.Add(Cells[xR, yB]);
				}
			}
		}
	}
}
