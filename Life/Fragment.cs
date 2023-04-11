using System;
using System.Linq;
using System.Text;

namespace Life
{
	public sealed class Fragment
	{
		public bool[][] Matrix { get; }

		public int Rows => Matrix.Length;
		public int Columns => Matrix[0].Length;

		public Fragment(bool[][] matrix)
		{
			Matrix = matrix;
		}

		public static Fragment FromString(string raw)
		{
			var matrix = raw
				.TrimEnd(Environment.NewLine.ToCharArray())
				.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
				.Select(row => row
					.ToCharArray()
					.Select(item => item switch
					{
						'0' => false,
						'1' => true,
						_ => throw new ArgumentOutOfRangeException(nameof(item), item, null)
					})
					.ToArray())
				.ToArray();

			return new Fragment(matrix);
		}

		public override string ToString()
		{
			var builder = new StringBuilder();

			for (var row = 0; row < Rows; row++)
			{
				for (var column = 0; column < Columns; column++)
				{
					builder.Append(Matrix[row][column] ? '1' : '0');
				}

				if (row != Rows - 1) builder.Append('\n');
			}

			return builder.ToString();
		}
	}
}
