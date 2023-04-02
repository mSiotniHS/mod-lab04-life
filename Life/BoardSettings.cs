using System;

namespace Life
{
	[Serializable]
	public struct BoardSettings
	{
		public uint Width { get; set; }
		public uint Height { get; set; }
		public uint CellSize { get; set; }
	}
}
