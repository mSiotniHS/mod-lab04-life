using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading;
using Life;

namespace App
{
	internal static class Program
	{
		private const string AssetsDir = @".\assets";
		private const string OutDir = @".\out";

		private static readonly string SettingsPath = Path.Combine(AssetsDir, "settings.json");
		private static readonly string SavePath = Path.Combine(OutDir, "save.txt");

		private static Board _board;

		private static void GenerateStateRandomly(BoardSettings settings)
		{
			Console.WriteLine("Введите плотность генерации (значение от 0 до 1, через точку):");
			Console.Write("> ");

			double liveDensity;

			while (true)
			{
				var input = Console.ReadLine();
				var successfulParse = double.TryParse(
					input,
					NumberStyles.Float,
					CultureInfo.InvariantCulture,
					out var parsed);

				if (!successfulParse) continue;
				if (parsed < 0 || parsed > 1) continue;

				liveDensity = parsed;
				break;
			}

			_board = BoardBuilder.Randomized(settings, liveDensity);
		}

		private static void LoadState(BoardSettings settings)
		{
			Console.WriteLine("Введите название файла:");
			Console.Write("> ");

			var fileName = Console.ReadLine();
			var path = Path.Combine(AssetsDir, $"{fileName!}.txt");
			var raw = File.ReadAllText(path);
			var fragment = Fragment.FromString(raw);

			_board = new Board(settings);
			_board.AddFragment(fragment, (3, 3));
		}

		private static void LoadSave(BoardSettings settings)
		{
			var raw = File.ReadAllText(SavePath);
			var fragment = Fragment.FromString(raw);

			_board = new Board(settings);
			_board.AddFragment(fragment);
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

		private static BoardSettings LoadSettings()
		{
			BoardSettings settings;
			try
			{
				var rawSettings = File.ReadAllText(SettingsPath);
				settings = JsonSerializer.Deserialize<BoardSettings>(rawSettings);

				Console.WriteLine("Файл с настройками найден.");
			}
			catch
			{
				Console.WriteLine("Возникла ошибка при чтении из файла. Использую стандартные настройки.");
				settings = new BoardSettings
				{
					Width = 50,
					Height = 20,
					CellSize = 1
				};
			}

			return settings;
		}

		private static void Main()
		{
			Console.WriteLine("Игра \"Жизнь\"\n");

			var settings = LoadSettings();

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
					GenerateStateRandomly(settings);
					break;
				}

				if (input.Key == ConsoleKey.D2)
				{
					LoadState(settings);
					break;
				}

				if (input.Key == ConsoleKey.D3 && saveFileExists)
				{
					LoadSave(settings);
					break;
				}

				Console.WriteLine("Некорректный ввод");
			}

			for (var i = 0; i < 10; i++)
			{
				Console.Clear();
				Render();
				_board.Advance();
				Thread.Sleep(1000);
			}

			Directory.CreateDirectory(OutDir);
			File.WriteAllText(SavePath, _board.ToFragment().ToString());
		}
	}
}
