using System;

namespace UnityEngine.TextCore
{
	[Serializable]
	public struct GlyphMetrics : IEquatable<GlyphMetrics>
	{
		public float width;
		
		public float height;

		public float horizontalBearingX;

		public float horizontalBearingY;

		public float horizontalAdvance;

		public GlyphMetrics(float width, float height, float bearingX, float bearingY, float advance)
		{
			this.width = width;
			this.height = height;
			this.horizontalBearingX = bearingX;
			this.horizontalBearingY = bearingY;
			this.horizontalAdvance = advance;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals(GlyphMetrics other)
		{
			return base.Equals((object)other);
		}

		public static bool operator ==(GlyphMetrics lhs, GlyphMetrics rhs)
		{
			return lhs.width == rhs.width && lhs.height == rhs.height && lhs.horizontalBearingX == rhs.horizontalBearingX && lhs.horizontalBearingY == rhs.horizontalBearingY && lhs.horizontalAdvance == rhs.horizontalAdvance;
		}

		public static bool operator !=(GlyphMetrics lhs, GlyphMetrics rhs)
		{
			return !(lhs == rhs);
		}
	}
}
