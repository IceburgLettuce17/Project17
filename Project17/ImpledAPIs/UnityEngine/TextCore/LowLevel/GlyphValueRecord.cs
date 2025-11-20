using System;
namespace UnityEngine.TextCore.LowLevel
{
	[Serializable]
	public struct GlyphValueRecord : IEquatable<GlyphValueRecord>
	{
		public float xPlacement;

		public float yPlacement;

		public float xAdvance;

		public float yAdvance;

		public GlyphValueRecord(float xPlacement, float yPlacement, float xAdvance, float yAdvance)
		{
			this.xPlacement = xPlacement;
			this.yPlacement = yPlacement;
			this.xAdvance = xAdvance;
			this.yAdvance = yAdvance;
		}

		public static GlyphValueRecord operator +(GlyphValueRecord a, GlyphValueRecord b)
		{
			GlyphValueRecord result = default(GlyphValueRecord);
			result.xPlacement = a.xPlacement + b.xPlacement;
			result.yPlacement = a.yPlacement + b.yPlacement;
			result.xAdvance = a.xAdvance + b.xAdvance;
			result.yAdvance = a.yAdvance + b.yAdvance;
			return result;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals(GlyphValueRecord other)
		{
			return base.Equals((object)other);
		}

		public static bool operator ==(GlyphValueRecord lhs, GlyphValueRecord rhs)
		{
			return lhs.xPlacement == rhs.xPlacement && lhs.yPlacement == rhs.yPlacement && lhs.xAdvance == rhs.xAdvance && lhs.yAdvance == rhs.yAdvance;
		}

		public static bool operator !=(GlyphValueRecord lhs, GlyphValueRecord rhs)
		{
			return !(lhs == rhs);
		}
	}
}
