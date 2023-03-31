using System;
using System.Threading;

namespace Life
{
    internal static class Program
    {
        private static Board _board;

        private static void Reset()
        {
            _board = new Board(
                width: 50,
                height: 20,
                cellSize: 1,
                liveDensity: 0.5);
        }

        private static void Render()
        {
            for (var row = 0; row < _board.Rows; row++)
            {
                for (var col = 0; col < _board.Columns; col++)
                {
                    var cell = _board.Cells[col, row];
                    Console.Write(cell.IsAlive ? '*' : ' ');
                }
                Console.Write('\n');
            }
        }

        private static void Main()
        {
            Reset();
            while(true)
            {
                Console.Clear();
                Render();
                _board.Advance();
                Thread.Sleep(1000);
            }
        }
    }
}
