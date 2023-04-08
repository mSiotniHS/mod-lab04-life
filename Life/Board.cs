namespace Life
{
	public class Board
	{
		public readonly Cell[,] Cells;
		public readonly uint CellSize;

		public int Rows => Cells.GetLength(0);
		public int Columns => Cells.GetLength(1);

		public Board(BoardSettings settings)
		{
			CellSize = settings.CellSize;

			var rows = settings.Height / settings.CellSize;
			var columns = settings.Width / settings.CellSize;

			Cells = new Cell[rows, columns];

			for (var x = 0; x < rows; x++)
			{
				for (var y = 0; y < columns; y++)
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

		public void AddFragment(Fragment fragment, (int, int) topLeftPoint)
		{
			var (x, y) = topLeftPoint;

			for (var row = x; row < fragment.Rows; row++)
			{
				for (var column = y; column < fragment.Columns; column++)
				{
					Cells[row, column].IsAlive = fragment.Matrix[row][column];
				}
			}
		}

		public void AddFragment(Fragment fragment) => AddFragment(fragment, (0, 0));

		public Fragment ToFragment()
		{
			var matrix = new bool[Rows][];

			for (var row = 0; row < Rows; row++)
			{
				matrix[row] = new bool[Columns];

				for (var column = 0; column < Columns; column++)
				{
					matrix[row][column] = Cells[row, column].IsAlive;
				}
			}

			return new Fragment(matrix);
		}

		private void ConnectNeighbors()
		{
			int BeginBoundaryCheck(int max, int value) =>
				value > 0
					? value - 1
					: max - 1;

			int EndBoundaryCheck(int max, int value) =>
				value < max - 1
					? value + 1
					: 0;

			for (var x = 0; x < Rows; x++)
			{
				for (var y = 0; y < Columns; y++)
				{
					var xL = BeginBoundaryCheck(Rows, x);
					var xR = EndBoundaryCheck(Rows, x);

					var yT = BeginBoundaryCheck(Columns, y);
					var yB = EndBoundaryCheck(Columns, y);

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
