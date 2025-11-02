using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore
{
	/// <summary>
	///   <para>A rectangle that defines the position of a glyph within an atlas texture.</para>
	/// </summary>
	[Serializable]
	[UsedByNativeCode]
	public struct GlyphRect : IEquatable<GlyphRect>
	{
		[NativeName("x")]
		[SerializeField]
		private int m_X;

		[NativeName("y")]
		[SerializeField]
		private int m_Y;

		[SerializeField]
		[NativeName("width")]
		private int m_Width;

		[NativeName("height")]
		[SerializeField]
		private int m_Height;

		private static readonly GlyphRect s_ZeroGlyphRect = new GlyphRect(0, 0, 0, 0);

		/// <summary>
		///   <para>The x position of the glyph in the font atlas texture.</para>
		/// </summary>
		public int x
		{
			get
			{
				return m_X;
			}
			set
			{
				m_X = value;
			}
		}

		/// <summary>
		///   <para>The y position of the glyph in the font atlas texture.</para>
		/// </summary>
		public int y
		{
			get
			{
				return m_Y;
			}
			set
			{
				m_Y = value;
			}
		}

		/// <summary>
		///   <para>The width of the glyph.</para>
		/// </summary>
		public int width
		{
			get
			{
				return m_Width;
			}
			set
			{
				m_Width = value;
			}
		}

		/// <summary>
		///   <para>The height of the glyph.</para>
		/// </summary>
		public int height
		{
			get
			{
				return m_Height;
			}
			set
			{
				m_Height = value;
			}
		}

		/// <summary>
		///   <para>A GlyphRect with all values set to zero. Shorthand for writing GlyphRect(0, 0, 0, 0).</para>
		/// </summary>
		public static GlyphRect zero => s_ZeroGlyphRect;

		/// <summary>
		///   <para>Constructor for a new GlyphRect.</para>
		/// </summary>
		/// <param name="x">The x position of the glyph in the atlas texture.</param>
		/// <param name="y">The y position of the glyph in the atlas texture.</param>
		/// <param name="width">The width of the glyph.</param>
		/// <param name="height">The height of the glyph.</param>
		/// <param name="rect">The Rect used to construct the new GlyphRect.</param>
		public GlyphRect(int x, int y, int width, int height)
		{
			m_X = x;
			m_Y = y;
			m_Width = width;
			m_Height = height;
		}

		/// <summary>
		///   <para>Constructor for a new GlyphRect.</para>
		/// </summary>
		/// <param name="x">The x position of the glyph in the atlas texture.</param>
		/// <param name="y">The y position of the glyph in the atlas texture.</param>
		/// <param name="width">The width of the glyph.</param>
		/// <param name="height">The height of the glyph.</param>
		/// <param name="rect">The Rect used to construct the new GlyphRect.</param>
		public GlyphRect(Rect rect)
		{
			m_X = (int)rect.x;
			m_Y = (int)rect.y;
			m_Width = (int)rect.width;
			m_Height = (int)rect.height;
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
