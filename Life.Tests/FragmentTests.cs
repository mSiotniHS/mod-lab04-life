using System;
using Xunit;

namespace Life.Tests
{
	public abstract class FragmentTests
	{
		public class ToStringTests
		{
			[Fact]
			public void GeneratesCorrectString()
			{
				var fragment = new Fragment(new[]
				{
					new []{ false, true, false },
					new []{ true, true, true },
					new []{ false, false, true }
				});

				var actual = fragment.ToString();
				const string expected = "010\n111\n001";

				Assert.Equal(expected, actual);
			}
		}

		public class FromStringTests
		{
			[Fact]
			public void GeneratesCorrectFragment()
			{
				const string raw = "010\n111\n001";

				var actual = Fragment.FromString(raw);
				var expected = new Fragment(new[]
				{
					new []{ false, true, false },
					new []{ true, true, true },
					new []{ false, false, true }
				});

				Assert.Equal(expected.Matrix, actual.Matrix);
			}

			[Fact]
			public void ThrowsIfStringHasIllegalCharacters()
			{
				const string raw = "010\n111\n002";

				Assert.Throws<ArgumentOutOfRangeException>(() => Fragment.FromString(raw));
			}
		}
	}
}
