using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore
{
	/// <summary>
	///   <para>A Glyph is the visual representation of a text element or character.</para>
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	[UsedByNativeCode]
	public class Glyph
	{
		[NativeName("index")]
		[SerializeField]
		private uint m_Index;

		[NativeName("metrics")]
		[SerializeField]
		private GlyphMetrics m_Metrics;

		[NativeName("glyphRect")]
		[SerializeField]
		private GlyphRect m_GlyphRect;

		[SerializeField]
		[NativeName("scale")]
		private float m_Scale;

		[NativeName("atlasIndex")]
		[SerializeField]
		private int m_AtlasIndex;

		/// <summary>
		///   <para>The index of the glyph in the source font file.</para>
		/// </summary>
		public uint index
		{
			get
			{
				return m_Index;
			}
			set
			{
				m_Index = value;
			}
		}

		/// <summary>
		///   <para>The metrics that define the size, position and spacing of a glyph when performing text layout.</para>
		/// </summary>
		public GlyphMetrics metrics
		{
			get
			{
				return m_Metrics;
			}
			set
			{
				m_Metrics = value;
			}
		}

		/// <summary>
		///   <para>A rectangle that defines the position of a glyph within an atlas texture.</para>
		/// </summary>
		public GlyphRect glyphRect
		{
			get
			{
				return m_GlyphRect;
			}
			set
			{
				m_GlyphRect = value;
			}
		}

		/// <summary>
		///   <para>The relative scale of the glyph. The default value is 1.0.</para>
		/// </summary>
		public float scale
		{
			get
			{
				return m_Scale;
			}
			set
			{
				m_Scale = value;
			}
		}

		/// <summary>
		///   <para>The index of the atlas texture that contains this glyph.</para>
		/// </summary>
		public int atlasIndex
		{
			get
			{
				return m_AtlasIndex;
			}
			set
			{
				m_AtlasIndex = value;
			}
		}

		/// <summary>
		///   <para>Constructor for a new glyph.</para>
		/// </summary>
		/// <param name="glyph">Glyph used as a reference for the new glyph.</param>
		/// <param name="index">The index of the glyph in the font file.</param>
		/// <param name="metrics">The metrics of the glyph.</param>
		/// <param name="glyphRect">The GlyphRect defining the position of the glyph in the atlas texture.</param>
		/// <param name="scale">The relative scale of the glyph.</param>
		/// <param name="atlasIndex">The index of the atlas texture that contains the glyph.</param>
		public Glyph()
		{
			m_Index = 0u;
			m_Metrics = default(GlyphMetrics);
			m_GlyphRect = default(GlyphRect);
			m_Scale = 1f;
			m_AtlasIndex = 0;
		}

		/// <summary>
		///   <para>Constructor for a new glyph.</para>
		/// </summary>
		/// <param name="glyph">Glyph used as a reference for the new glyph.</param>
		/// <param name="index">The index of the glyph in the font file.</param>
		/// <param name="metrics">The metrics of the glyph.</param>
		/// <param name="glyphRect">The GlyphRect defining the position of the glyph in the atlas texture.</param>
		/// <param name="scale">The relative scale of the glyph.</param>
		/// <param name="atlasIndex">The index of the atlas texture that contains the glyph.</param>
		public Glyph(Glyph glyph)
		{
			m_Index = glyph.index;
			m_Metrics = glyph.metrics;
			m_GlyphRect = glyph.glyphRect;
			m_Scale = glyph.scale;
			m_AtlasIndex = glyph.atlasIndex;
		}

		internal Glyph(GlyphMarshallingStruct glyphStruct)
		{
			m_Index = glyphStruct.index;
			m_Metrics = glyphStruct.metrics;
			m_GlyphRect = glyphStruct.glyphRect;
			m_Scale = glyphStruct.scale;
			m_AtlasIndex = glyphStruct.atlasIndex;
		}

		/// <summary>
		///   <para>Constructor for a new glyph.</para>
		/// </summary>
		/// <param name="glyph">Glyph used as a reference for the new glyph.</param>
		/// <param name="index">The index of the glyph in the font file.</param>
		/// <param name="metrics">The metrics of the glyph.</param>
		/// <param name="glyphRect">The GlyphRect defining the position of the glyph in the atlas texture.</param>
		/// <param name="scale">The relative scale of the glyph.</param>
		/// <param name="atlasIndex">The index of the atlas texture that contains the glyph.</param>
		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect)
		{
			m_Index = index;
			m_Metrics = metrics;
			m_GlyphRect = glyphRect;
			m_Scale = 1f;
			m_AtlasIndex = 0;
		}

		/// <summary>
		///   <para>Constructor for a new glyph.</para>
		/// </summary>
		/// <param name="glyph">Glyph used as a reference for the new glyph.</param>
		/// <param name="index">The index of the glyph in the font file.</param>
		/// <param name="metrics">The metrics of the glyph.</param>
		/// <param name="glyphRect">The GlyphRect defining the position of the glyph in the atlas texture.</param>
		/// <param name="scale">The relative scale of the glyph.</param>
		/// <param name="atlasIndex">The index of the atlas texture that contains the glyph.</param>
		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			m_Index = index;
			m_Metrics = metrics;
			m_GlyphRect = glyphRect;
			m_Scale = scale;
			m_AtlasIndex = atlasIndex;
		}

		/// <summary>
		///   <para>Compares two glyphs to determine if they have the same values.</para>
		/// </summary>
		/// <param name="other">The glyph to compare with.</param>
		/// <returns>
		///   <para>Returns true if the glyphs have the same values. False if not.</para>
		/// </returns>
		public bool Compare(Glyph other)
		{
			return index == other.index && metrics == other.metrics && glyphRect == other.glyphRect && scale == other.scale && atlasIndex == other.atlasIndex;
		}
	}
}
