using System;

namespace UnityEngine.TextCore
{
	[Serializable]
	public struct GlyphRect : IEquatable<GlyphRect>
	{
		public int x;

		public int y;

		public int width;

		public int height;

		public static GlyphRect zero => new GlyphRect(0, 0, 0, 0);

		public GlyphRect(int x, int y, int width, int height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public GlyphRect(Rect rect)
		{
			x = (int)rect.x;
			y = (int)rect.y;
			width = (int)rect.width;
			height = (int)rect.height;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals(GlyphRect other)
		{
			return base.Equals((object)other);
		}

		public static bool operator ==(GlyphRect lhs, GlyphRect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		public static bool operator !=(GlyphRect lhs, GlyphRect rhs)
		{
			return !(lhs == rhs);
		}
	}
}

