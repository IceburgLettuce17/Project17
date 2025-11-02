using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	/// <summary>
	///   <para>The positional adjustment values of a glyph.</para>
	/// </summary>
	[Serializable]
	[UsedByNativeCode]
	public struct GlyphAdjustmentRecord
	{
		[NativeName("glyphIndex")]
		[SerializeField]
		private uint m_GlyphIndex;

		[NativeName("glyphValueRecord")]
		[SerializeField]
		private GlyphValueRecord m_GlyphValueRecord;

		/// <summary>
		///   <para>The index of the glyph in the source font file.</para>
		/// </summary>
		public uint glyphIndex
		{
			get
			{
				return m_GlyphIndex;
			}
			set
			{
				m_GlyphIndex = value;
			}
		}

		/// <summary>
		///   <para>The GlyphValueRecord contains the positional adjustments of the glyph.</para>
		/// </summary>
		public GlyphValueRecord glyphValueRecord
		{
			get
			{
				return m_GlyphValueRecord;
			}
			set
			{
				m_GlyphValueRecord = value;
			}
		}

		/// <summary>
		///   <para>Constructor for new glyph adjustment record.</para>
		/// </summary>
		/// <param name="glyphIndex">The index of the glyph in the source font file.</param>
		/// <param name="glyphValueRecord">The GlyphValueRecord contains the positional adjustments of the glyph.</param>
		public GlyphAdjustmentRecord(uint glyphIndex, GlyphValueRecord glyphValueRecord)
		{
			m_GlyphIndex = glyphIndex;
			m_GlyphValueRecord = glyphValueRecord;
		}
	}
}
