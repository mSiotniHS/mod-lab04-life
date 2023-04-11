using System;
using System.Collections.Generic;
using System.Linq;

namespace Life.Analytics
{
	[Serializable]
	public struct Report
	{
		[Serializable]
		public struct IterationReport
		{
			[Serializable]
			public struct SymmetryReport
			{
				public double Vertical { get; set; }
				public double Horizontal { get; set; }

				public static SymmetryReport DrawUp(Board board) =>
					new SymmetryReport
					{
						Horizontal = AnalyticMethods.HorizontalSymmetryRate(board),
						Vertical = AnalyticMethods.VerticalSymmetryRate(board)
					};
			}

			[Serializable]
			public struct CellCountingReport
			{
				public uint Alive { get; set; }
				public uint Dead { get; set; }

				public static CellCountingReport DrawUp(Board board)
				{
					var aliveCellCount = AnalyticMethods.CountCells(board);

					return new CellCountingReport
					{
						Alive = aliveCellCount,
						Dead = (uint) (board.Rows * board.Columns) - aliveCellCount
					};
				}
			}

			[Serializable]
			public struct ShapeSearchReport
			{
				public string Name { get; set; }
				public uint Count { get; set; }

				public static ShapeSearchReport DrawUp(Board board, (string, Fragment) shape)
				{
					var (name, fragment) = shape;
					var foundCount = AnalyticMethods.CountFragment(board, fragment);

					return new ShapeSearchReport
					{
						Name = name,
						Count = foundCount
					};
				}
			}

			public uint IterationNo { get; set; }
			public SymmetryReport Symmetry { get; set; }
			public CellCountingReport CellCounting { get; set; }
			public List<ShapeSearchReport> FoundShapes { get; set; }
		}

		public BoardSettings Settings { get; }
		public List<IterationReport> Iterations { get; }

		[NonSerialized]
		private readonly List<(string, Fragment)> _shapesToFind;

		[NonSerialized] private uint _iterationCounter;

		public Report(BoardSettings settings, List<(string, Fragment)> shapesToFind)
		{
			Settings = settings;
			Iterations = new List<IterationReport>();
			_shapesToFind = shapesToFind;
			_iterationCounter = 0;
		}

		public void Add(Board board)
		{
			var foundShapes = _shapesToFind
				.Select(shape => IterationReport.ShapeSearchReport.DrawUp(board, shape))
				.ToList();

			var report = new IterationReport
			{
				IterationNo = _iterationCounter,
				Symmetry = IterationReport.SymmetryReport.DrawUp(board),
				CellCounting = IterationReport.CellCountingReport.DrawUp(board),
				FoundShapes = foundShapes
			};

			Iterations.Add(report);

			_iterationCounter++;
		}
	}
}
