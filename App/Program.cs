using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using Life;
using Life.Analytics;

namespace App
{
	internal static class Program
	{
		private const string AssetsDir = @".\assets";
		private const string OutDir = @".\out";

		private static readonly string SettingsPath = Path.Combine(AssetsDir, "settings.json");
		private static readonly string StillShapesPath = Path.Combine(AssetsDir, "still");
		private static readonly string SavePath = Path.Combine(OutDir, "save.txt");
		private static readonly string ReportPath = Path.Combine(OutDir, "report.json");

		private static Board _board;
		private static Report _report;

		private static void Render()
		{
			for (var row = 0; row < _board.Rows; row++)
			{
				for (var col = 0; col < _board.Columns; col++)
				{
					var cell = _board.Cells[row, col];
					Console.Write(cell.IsAlive ? '█' : ' ');
				}
				Console.WriteLine();
			}
		}

		private static Settings LoadSettings()
		{
			Settings settings;
			try
			{
				var rawSettings = File.ReadAllText(SettingsPath);
				settings = JsonSerializer.Deserialize<Settings>(rawSettings);

				Console.WriteLine("Файл с настройками найден.");
			}
			catch
			{
				Console.WriteLine("Возникла ошибка при чтении из файла. Использую стандартные настройки.");
				settings = new Settings
				{
					IterationCount = 10,
					BoardSettings = new BoardSettings
					{
						Width = 50,
						Height = 20,
						CellSize = 1
					}
				};
			}

			return settings;
		}

		private static void Main()
		{
			Console.WriteLine("Игра \"Жизнь\"\n");

			var settings = LoadSettings();
			var boardSettings = settings.BoardSettings;

			_report = new Report(boardSettings, GetShapesToFind());

			Console.WriteLine("Выберите действие:");
			Console.WriteLine("1) Рандомно сгенерировать состояние");
			Console.WriteLine("2) Загрузить состояние");

			var saveFileExists = File.Exists(SavePath);

			if (saveFileExists)
			{
				Console.WriteLine("3) Загрузить последнее состояние");
			}

			while (true)
			{
				Console.Write("> ");
				var input = Console.ReadKey();
				Console.WriteLine();

				if (input.Key == ConsoleKey.D1)
				{
					_board = UI.GenerateStateRandomly(boardSettings);
					break;
				}

				if (input.Key == ConsoleKey.D2)
				{
					_board = UI.LoadState(boardSettings, AssetsDir);
					break;
				}

				if (input.Key == ConsoleKey.D3 && saveFileExists)
				{
					_board = UI.LoadSave(boardSettings, SavePath);
					break;
				}

				return;
			}

			Console.Clear();
			Render();
			_report.Add(_board);

			for (var i = 0; i < settings.IterationCount; i++)
			{
				_board.Advance();

				Console.Clear();
				Render();
				_report.Add(_board);

				Thread.Sleep(500);
			}

			Directory.CreateDirectory(OutDir);
			File.WriteAllText(SavePath, _board.ToFragment().ToString());
			File.WriteAllText(ReportPath, JsonSerializer.Serialize(_report));
		}

		private static List<(string, Fragment)> GetShapesToFind()
		{
			var shapesToFind = new List<(string, Fragment)>();
			var shapeFileNames = Directory.GetFiles(StillShapesPath, "*.txt", SearchOption.TopDirectoryOnly);
			foreach (var shapeFileName in shapeFileNames)
			{
				var name = Path.GetFileNameWithoutExtension(shapeFileName);
				var fragment = Fragment.FromString(File.ReadAllText(shapeFileName));

				shapesToFind.Add((name, fragment));
			}

			return shapesToFind;
		}
	}
}
