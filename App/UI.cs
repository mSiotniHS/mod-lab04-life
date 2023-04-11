using System;
using System.Globalization;
using System.IO;
using Life;

namespace App
{
	internal static class UI
	{
		public static Board GenerateStateRandomly(BoardSettings settings)
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

			return BoardBuilder.Randomized(settings, liveDensity);
		}

		public static Board LoadState(BoardSettings settings, string assetsDir)
		{
			Console.WriteLine("Введите название файла:");
			Console.Write("> ");

			var fileName = Console.ReadLine();
			var path = Path.Combine(assetsDir, $"{fileName!}.txt");
			var raw = File.ReadAllText(path);
			var fragment = Fragment.FromString(raw);

			var board = new Board(settings);
			board.AddFragment(fragment, (3, 3));
			return board;
		}

		public static Board LoadSave(BoardSettings settings, string savePath)
		{
			var raw = File.ReadAllText(savePath);
			var fragment = Fragment.FromString(raw);

			var board = new Board(settings);
			board.AddFragment(fragment);
			return board;
		}
	}
}
