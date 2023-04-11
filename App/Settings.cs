using System;
using Life;

namespace App
{
	[Serializable]
	public struct Settings
	{
		public uint IterationCount { get; set; }
		public BoardSettings BoardSettings { get; set; }
	}
}
