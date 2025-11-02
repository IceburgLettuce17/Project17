using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore
{
	/// <summary>
	///   <para>A structure that contains information about a given typeface and for a specific point size.</para>
	/// </summary>
	[Serializable]
	[UnityEngine.Scripting.UsedByNativeCode]
	public struct FaceInfo
	{
		[SerializeField]
		[NativeName("familyName")]
		private string m_FamilyName;

		[NativeName("styleName")]
		[SerializeField]
		private string m_StyleName;

		[SerializeField]
		[NativeName("pointSize")]
		private int m_PointSize;

		[SerializeField]
		[NativeName("scale")]
		private float m_Scale;

		[SerializeField]
		[NativeName("lineHeight")]
		private float m_LineHeight;

		[NativeName("ascentLine")]
		[SerializeField]
		private float m_AscentLine;

		[SerializeField]
		[NativeName("capLine")]
		private float m_CapLine;

		[SerializeField]
		[NativeName("meanLine")]
		private float m_MeanLine;

		[SerializeField]
		[NativeName("baseline")]
		private float m_Baseline;

		[SerializeField]
		[NativeName("descentLine")]
		private float m_DescentLine;

		[SerializeField]
		[NativeName("superscriptOffset")]
		private float m_SuperscriptOffset;

		[SerializeField]
		[NativeName("superscriptSize")]
		private float m_SuperscriptSize;

		[NativeName("subscriptOffset")]
		[SerializeField]
		private float m_SubscriptOffset;

		[NativeName("subscriptSize")]
		[SerializeField]
		private float m_SubscriptSize;

		[NativeName("underlineOffset")]
		[SerializeField]
		private float m_UnderlineOffset;

		[SerializeField]
		[NativeName("underlineThickness")]
		private float m_UnderlineThickness;

		[SerializeField]
		[NativeName("strikethroughOffset")]
		private float m_StrikethroughOffset;

		[NativeName("strikethroughThickness")]
		[SerializeField]
		private float m_StrikethroughThickness;

		[NativeName("tabWidth")]
		[SerializeField]
		private float m_TabWidth;

		/// <summary>
		///   <para>The name of the font typeface also known as family name.</para>
		/// </summary>
		public string familyName
		{
			get
			{
				return m_FamilyName;
			}
			set
			{
				m_FamilyName = value;
			}
		}

		/// <summary>
		///   <para>The style name of the typeface which defines both the visual style and weight of the typeface.</para>
		/// </summary>
		public string styleName
		{
			get
			{
				return m_StyleName;
			}
			set
			{
				m_StyleName = value;
			}
		}

		/// <summary>
		///   <para>The point size used for sampling the typeface.</para>
		/// </summary>
		public int pointSize
		{
			get
			{
				return m_PointSize;
			}
			set
			{
				m_PointSize = value;
			}
		}

		/// <summary>
		///   <para>The relative scale of the typeface.</para>
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
		///   <para>The line height represents the distance between consecutive lines of text.</para>
		/// </summary>
		public float lineHeight
		{
			get
			{
				return m_LineHeight;
			}
			set
			{
				m_LineHeight = value;
			}
		}

		/// <summary>
		///   <para>The Ascent line is typically located at the top of the tallest glyph in the typeface.</para>
		/// </summary>
		public float ascentLine
		{
			get
			{
				return m_AscentLine;
			}
			set
			{
				m_AscentLine = value;
			}
		}

		/// <summary>
		///   <para>The Cap line is typically located at the top of capital letters.</para>
		/// </summary>
		public float capLine
		{
			get
			{
				return m_CapLine;
			}
			set
			{
				m_CapLine = value;
			}
		}

		/// <summary>
		///   <para>The Mean line is typically located at the top of lowercase letters.</para>
		/// </summary>
		public float meanLine
		{
			get
			{
				return m_MeanLine;
			}
			set
			{
				m_MeanLine = value;
			}
		}

		/// <summary>
		///   <para>The Baseline is an imaginary line upon which all glyphs appear to rest on.</para>
		/// </summary>
		public float baseline
		{
			get
			{
				return m_Baseline;
			}
			set
			{
				m_Baseline = value;
			}
		}

		/// <summary>
		///   <para>The Descent line is typically located at the bottom of the glyph with the lowest descender in the typeface.</para>
		/// </summary>
		public float descentLine
		{
			get
			{
				return m_DescentLine;
			}
			set
			{
				m_DescentLine = value;
			}
		}

		/// <summary>
		///   <para>The position of characters using superscript.</para>
		/// </summary>
		public float superscriptOffset
		{
			get
			{
				return m_SuperscriptOffset;
			}
			set
			{
				m_SuperscriptOffset = value;
			}
		}

		/// <summary>
		///   <para>The relative size / scale of superscript characters.</para>
		/// </summary>
		public float superscriptSize
		{
			get
			{
				return m_SuperscriptSize;
			}
			set
			{
				m_SuperscriptSize = value;
			}
		}

		/// <summary>
		///   <para>The position of characters using subscript.</para>
		/// </summary>
		public float subscriptOffset
		{
			get
			{
				return m_SubscriptOffset;
			}
			set
			{
				m_SubscriptOffset = value;
			}
		}

		/// <summary>
		///   <para>The relative size / scale of subscript characters.</para>
		/// </summary>
		public float subscriptSize
		{
			get
			{
				return m_SubscriptSize;
			}
			set
			{
				m_SubscriptSize = value;
			}
		}

		/// <summary>
		///   <para>The position of the underline.</para>
		/// </summary>
		public float underlineOffset
		{
			get
			{
				return m_UnderlineOffset;
			}
			set
			{
				m_UnderlineOffset = value;
			}
		}

		/// <summary>
		///   <para>The thickness of the underline.</para>
		/// </summary>
		public float underlineThickness
		{
			get
			{
				return m_UnderlineThickness;
			}
			set
			{
				m_UnderlineThickness = value;
			}
		}

		/// <summary>
		///   <para>The position of the strikethrough.</para>
		/// </summary>
		public float strikethroughOffset
		{
			get
			{
				return m_StrikethroughOffset;
			}
			set
			{
				m_StrikethroughOffset = value;
			}
		}

		/// <summary>
		///   <para>The thickness of the strikethrough.</para>
		/// </summary>
		public float strikethroughThickness
		{
			get
			{
				return m_StrikethroughThickness;
			}
			set
			{
				m_StrikethroughThickness = value;
			}
		}

		/// <summary>
		///   <para>The width of the tab character.</para>
		/// </summary>
		public float tabWidth
		{
			get
			{
				return m_TabWidth;
			}
			set
			{
				m_TabWidth = value;
			}
		}

		internal FaceInfo(string familyName, string styleName, int pointSize, float scale, float lineHeight, float ascentLine, float capLine, float meanLine, float baseline, float descentLine, float superscriptOffset, float superscriptSize, float subscriptOffset, float subscriptSize, float underlineOffset, float underlineThickness, float strikethroughOffset, float strikethroughThickness, float tabWidth)
		{
			m_FamilyName = familyName;
			m_StyleName = styleName;
			m_PointSize = pointSize;
			m_Scale = scale;
			m_LineHeight = lineHeight;
			m_AscentLine = ascentLine;
			m_CapLine = capLine;
			m_MeanLine = meanLine;
			m_Baseline = baseline;
			m_DescentLine = descentLine;
			m_SuperscriptOffset = superscriptOffset;
			m_SuperscriptSize = superscriptSize;
			m_SubscriptOffset = subscriptOffset;
			m_SubscriptSize = subscriptSize;
			m_UnderlineOffset = underlineOffset;
			m_UnderlineThickness = underlineThickness;
			m_StrikethroughOffset = strikethroughOffset;
			m_StrikethroughThickness = strikethroughThickness;
			m_TabWidth = tabWidth;
		}

		/// <summary>
		///   <para>Compares the information in this FaceInfo structure with the information in the given FaceInfo structure to determine whether they have the same values.</para>
		/// </summary>
		/// <param name="other">The FaceInfo structure to compare this FaceInfo structure with.</param>
		/// <returns>
		///   <para>Returns true if the FaceInfo structures have the same values. False if not.</para>
		/// </returns>
		public bool Compare(FaceInfo other)
		{
			return familyName == other.familyName && styleName == other.styleName && pointSize == other.pointSize && FontEngineUtilities.Approximately(scale, other.scale) && FontEngineUtilities.Approximately(lineHeight, other.lineHeight) && FontEngineUtilities.Approximately(ascentLine, other.ascentLine) && FontEngineUtilities.Approximately(capLine, other.capLine) && FontEngineUtilities.Approximately(meanLine, other.meanLine) && FontEngineUtilities.Approximately(baseline, other.baseline) && FontEngineUtilities.Approximately(descentLine, other.descentLine) && FontEngineUtilities.Approximately(superscriptOffset, other.superscriptOffset) && FontEngineUtilities.Approximately(superscriptSize, other.superscriptSize) && FontEngineUtilities.Approximately(subscriptOffset, other.subscriptOffset) && FontEngineUtilities.Approximately(subscriptSize, other.subscriptSize) && FontEngineUtilities.Approximately(underlineOffset, other.underlineOffset) && FontEngineUtilities.Approximately(underlineThickness, other.underlineThickness) && FontEngineUtilities.Approximately(strikethroughOffset, other.strikethroughOffset) && FontEngineUtilities.Approximately(strikethroughThickness, other.strikethroughThickness) && FontEngineUtilities.Approximately(tabWidth, other.tabWidth);
		}
	}
}
