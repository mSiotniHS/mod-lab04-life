using System;
using System.Threading;
using Life;

namespace App
{
	internal static class Program
	{
		private static Board _board;

		private static void Reset()
		{
			var settings = new BoardSettings
			{
				Width = 50,
				Height = 20,
				CellSize = 1,
				LiveDensity = 0.5
			};

			_board = BoardBuilder.Build(settings);
		}

		private static void Render()
		{
			for (var row = 0; row < _board.Rows; row++)
			{
				for (var col = 0; col < _board.Columns; col++)
				{
					var cell = _board.Cells[col, row];
					Console.Write(cell.IsAlive ? '█' : ' ');
				}
				Console.Write('\n');
			}
		}

		private static void Main()
		{
			Reset();
			while (true)
			{
				Console.Clear();
				Render();
				_board.Advance();
				Thread.Sleep(1000);
			}
		}
	}
}
