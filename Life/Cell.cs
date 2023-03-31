using System.Collections.Generic;
using System.Linq;

namespace Life
{
	public class Cell
	{
		public bool IsAlive;
		public readonly List<Cell> Neighbors = new List<Cell>();
		private bool _isAliveNext;

		public void DetermineNextLiveState()
		{
			var liveNeighbors = Neighbors.Count(x => x.IsAlive);
			if (IsAlive)
				_isAliveNext = liveNeighbors == 2 || liveNeighbors == 3;
			else
				_isAliveNext = liveNeighbors == 3;
		}

		public void Advance()
		{
			IsAlive = _isAliveNext;
		}
	}
}
