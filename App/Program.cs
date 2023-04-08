using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using Life;

namespace App
{
	internal static class Program
	{
		private static Board _board;

		private static void Reset()
		{
			const string settingsPath = @".\assets\settings.json";
			var rawSettings = File.ReadAllText(settingsPath);
			var settings = JsonSerializer.Deserialize<BoardSettings>(rawSettings);

			const double liveDensity = 0.5;

			_board = BoardBuilder.Randomized(settings, liveDensity);
		}

		private static void Render()
		{
			for (var row = 0; row < _board.Rows; row++)
			{
				for (var col = 0; col < _board.Columns; col++)
				{
					var cell = _board.Cells[row, col];
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
