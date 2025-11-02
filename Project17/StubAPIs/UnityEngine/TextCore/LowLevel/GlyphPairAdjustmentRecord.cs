using System;
using System.Diagnostics;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	/// <summary>
	///   <para>The positional adjustment  values of a pair of glyphs.</para>
	/// </summary>
	[Serializable]
	[DebuggerDisplay("First glyphIndex = {m_FirstAdjustmentRecord.m_GlyphIndex},  Second glyphIndex = {m_SecondAdjustmentRecord.m_GlyphIndex}")]
	[UsedByNativeCode]
	public struct GlyphPairAdjustmentRecord
	{
		[SerializeField]
		[NativeName("firstAdjustmentRecord")]
		private GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		[SerializeField]
		[NativeName("secondAdjustmentRecord")]
		private GlyphAdjustmentRecord m_SecondAdjustmentRecord;

		[SerializeField]
		private FontFeatureLookupFlags m_FeatureLookupFlags;

		/// <summary>
		///   <para>The positional adjustment values for the first glyph.</para>
		/// </summary>
		public GlyphAdjustmentRecord firstAdjustmentRecord
		{
			get
			{
				return m_FirstAdjustmentRecord;
			}
			set
			{
				m_FirstAdjustmentRecord = value;
			}
		}

		/// <summary>
		///   <para>The positional adjustment values for the second glyph.</para>
		/// </summary>
		public GlyphAdjustmentRecord secondAdjustmentRecord
		{
			get
			{
				return m_SecondAdjustmentRecord;
			}
			set
			{
				m_SecondAdjustmentRecord = value;
			}
		}

		public FontFeatureLookupFlags featureLookupFlags
		{
			get
			{
				return m_FeatureLookupFlags;
			}
			set
			{
				m_FeatureLookupFlags = value;
			}
		}

		/// <summary>
		///   <para>Constructor for new glyph pair adjustment record.</para>
		/// </summary>
		/// <param name="firstAdjustmentRecord">The positional adjustment values for the first glyph.</param>
		/// <param name="secondAdjustmentRecord">The positional adjustment values for the second glyph.</param>
		public GlyphPairAdjustmentRecord(GlyphAdjustmentRecord firstAdjustmentRecord, GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			m_FirstAdjustmentRecord = firstAdjustmentRecord;
			m_SecondAdjustmentRecord = secondAdjustmentRecord;
			m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}
	}
}
