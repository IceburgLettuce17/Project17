using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore
{
	/// <summary>
	///   <para>A set of values that define the size, position and spacing of a glyph when performing text layout.</para>
	/// </summary>
	[Serializable]
	[UsedByNativeCode]
	public struct GlyphMetrics : IEquatable<GlyphMetrics>
	{
		[NativeName("width")]
		[SerializeField]
		private float m_Width;

		[SerializeField]
		[NativeName("height")]
		private float m_Height;

		[NativeName("horizontalBearingX")]
		[SerializeField]
		private float m_HorizontalBearingX;

		[NativeName("horizontalBearingY")]
		[SerializeField]
		private float m_HorizontalBearingY;

		[SerializeField]
		[NativeName("horizontalAdvance")]
		private float m_HorizontalAdvance;

		/// <summary>
		///   <para>The width of the glyph.</para>
		/// </summary>
		public float width
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
		public float height
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
		///   <para>The horizontal distance from the current drawing position (origin) relative to the element's left bounding box edge (bbox).</para>
		/// </summary>
		public float horizontalBearingX
		{
			get
			{
				return m_HorizontalBearingX;
			}
			set
			{
				m_HorizontalBearingX = value;
			}
		}

		/// <summary>
		///   <para>The vertical distance from the current baseline relative to the element's top bounding box edge (bbox).</para>
		/// </summary>
		public float horizontalBearingY
		{
			get
			{
				return m_HorizontalBearingY;
			}
			set
			{
				m_HorizontalBearingY = value;
			}
		}

		/// <summary>
		///   <para>The horizontal distance to increase (left to right) or decrease (right to left) the drawing position relative to the origin of the text element.</para>
		/// </summary>
		public float horizontalAdvance
		{
			get
			{
				return m_HorizontalAdvance;
			}
			set
			{
				m_HorizontalAdvance = value;
			}
		}

		/// <summary>
		///   <para>Constructs a new GlyphMetrics structure.</para>
		/// </summary>
		/// <param name="width">The width of the glyph.</param>
		/// <param name="height">The height of the glyph.</param>
		/// <param name="bearingX">The horizontal bearingX.</param>
		/// <param name="bearingY">The horizontal bearingY.</param>
		/// <param name="advance">The horizontal advance.</param>
		public GlyphMetrics(float width, float height, float bearingX, float bearingY, float advance)
		{
			m_Width = width;
			m_Height = height;
			m_HorizontalBearingX = bearingX;
			m_HorizontalBearingY = bearingY;
			m_HorizontalAdvance = advance;
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
