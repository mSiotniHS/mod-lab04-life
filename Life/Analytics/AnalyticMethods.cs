namespace Life.Analytics
{
	public static class AnalyticMethods
	{
		/// <summary>
		/// Оценивает степень симметричности доски по вертикали.
		/// </summary>
		/// <param name="board">Доска игры "Жизнь"</param>
		/// <returns>Число в диапазоне [0,1], означающее степень симметричности.
		/// 1 --- абсолютная симметричность, 0 --- абсолютная несимметричность.</returns>
		public static double VerticalSymmetryRate(Board board)
		{
			var diffCount = 0;

			for (var row = 0; row < board.Rows; row++)
			{
				for (var column = 0; column < board.Columns / 2; column++)
				{
					var left = board.Cells[row, column];
					var right = board.Cells[row, board.Columns - column - 1];

					if (left.IsAlive != right.IsAlive)
					{
						diffCount++;
					}
				}
			}

			return 1.0 - (double) (diffCount * 2) / (board.Rows * board.Columns);
		}

		/// <summary>
		/// Оценивает степень симметричности доски <paramref name="board"/> по горизонтали.
		/// </summary>
		/// <param name="board">Доска игры "Жизнь"</param>
		/// <returns>Число в диапазоне [0,1], означающее степень симметричности.
		/// 1 --- абсолютная симметричность, 0 --- абсолютная несимметричность.</returns>
		public static double HorizontalSymmetryRate(Board board)
		{
			var diffCount = 0;

			for (var column = 0; column < board.Columns; column++)
			{
				for (var row = 0; row < board.Rows / 2; row++)
				{
					var top = board.Cells[row, column];
					var bottom = board.Cells[board.Rows - row - 1, column];

					if (top.IsAlive != bottom.IsAlive)
					{
						diffCount++;
					}
				}
			}

			return 1.0 - (double) (diffCount * 2) / (board.Rows * board.Columns);
		}

		/// <summary>
		/// Считает, сколько предоставленного фрагмента было найдено на доске.
		/// </summary>
		public static uint CountFragment(Board board, Fragment fragment)
		{
			var count = 0u;

			for (var row = 0; row < board.Rows - fragment.Rows; row++)
			{
				for (var column = 0; column < board.Columns - fragment.Columns; column++)
				{
					if (FindFragment(board, (row, column), fragment)) count++;
				}
			}

			return count;
		}

		/// <summary>
		/// Определяет, есть ли фрагмент на доске при заданной верхней левой координате
		/// </summary>
		/// <param name="board">Доска, на которой ищется фрагмент</param>
		/// <param name="topLeftPoint">Координаты точки, где будет располагаться верхний левый угол фрагмента</param>
		/// <param name="fragment">Искомый фрагмент</param>
		/// <returns><c>true</c>, если фрагмент найден; иначе <c>false</c></returns>
		public static bool FindFragment(Board board, (int, int) topLeftPoint, Fragment fragment)
		{
			var (x, y) = topLeftPoint;

			for (var row = 0; row < fragment.Rows; row++)
			{
				for (var column = 0; column < fragment.Columns; column++)
				{
					if (fragment.Matrix[row][column] != board.Cells[x + row, y + column].IsAlive)
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Подсчитывает число живых клеток на доске.
		/// </summary>
		/// <param name="board">Доска</param>
		/// <returns>Ненулевое целое число, отражающее число живых клеток</returns>
		public static uint CountCells(Board board)
		{
			var count = 0u;

			for (var row = 0; row < board.Rows; row++)
			{
				for (var column = 0; column < board.Columns; column++)
				{
					if (board.Cells[row, column].IsAlive) count++;
				}
			}

			return count;
		}
	}
}
