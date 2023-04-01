namespace Life
{
	public class Board
	{
		public readonly Cell[,] Cells;
		public readonly uint CellSize;

		public int Columns => Cells.GetLength(0);
		public int Rows => Cells.GetLength(1);

		public Board(Cell[,] cells, uint cellSize)
		{
			Cells = cells; // WARNING: копировать?
			CellSize = cellSize;
		}

		public void Advance()
		{
			foreach (var cell in Cells)
				cell.DetermineNextLiveState();
			foreach (var cell in Cells)
				cell.Advance();
		}
	}
}
