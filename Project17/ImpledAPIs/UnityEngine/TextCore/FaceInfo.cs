using System;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore
{
	[Serializable]
	public struct FaceInfo
	{
		internal int faceIndex;
		public string familyName;

		public string styleName;

		public int pointSize;

		public float scale;

		public float lineHeight;

		public float ascentLine;
		public float capLine;

		public float meanLine;

		public float baseline;

		public float descentLine;

		public float superscriptOffset;

		public float superscriptSize;

		public float subscriptOffset;

		public float subscriptSize;

		public float underlineOffset;

		public float underlineThickness;
		public float strikethroughOffset;

		public float strikethroughThickness;

		public float tabWidth;

		internal FaceInfo(string familyName, string styleName, int pointSize, float scale, float lineHeight, float ascentLine, float capLine, float meanLine, float baseline, float descentLine, float superscriptOffset, float superscriptSize, float subscriptOffset, float subscriptSize, float underlineOffset, float underlineThickness, float strikethroughOffset, float strikethroughThickness, float tabWidth)
		{
			this.faceIndex = 0;
			this.familyName = familyName;
			this.styleName = styleName;
			this.pointSize = pointSize;
			this.scale = scale;
			this.lineHeight = lineHeight;
			this.ascentLine = ascentLine;
			this.capLine = capLine;
			this.meanLine = meanLine;
			this.baseline = baseline;
			this.descentLine = descentLine;
			this.superscriptOffset = superscriptOffset;
			this.superscriptSize = superscriptSize;
			this.subscriptOffset = subscriptOffset;
			this.subscriptSize = subscriptSize;
			this.underlineOffset = underlineOffset;
			this.underlineThickness = underlineThickness;
			this.strikethroughOffset = strikethroughOffset;
			this.strikethroughThickness = strikethroughThickness;
			this.tabWidth = tabWidth;
		}

		public bool Compare(FaceInfo other)
		{
			return familyName == other.familyName && styleName == other.styleName && faceIndex == other.faceIndex && pointSize == other.pointSize && FontEngineUtilities.Approximately(scale, other.scale) && FontEngineUtilities.Approximately(lineHeight, other.lineHeight) && FontEngineUtilities.Approximately(ascentLine, other.ascentLine) && FontEngineUtilities.Approximately(capLine, other.capLine) && FontEngineUtilities.Approximately(meanLine, other.meanLine) && FontEngineUtilities.Approximately(baseline, other.baseline) && FontEngineUtilities.Approximately(descentLine, other.descentLine) && FontEngineUtilities.Approximately(superscriptOffset, other.superscriptOffset) && FontEngineUtilities.Approximately(superscriptSize, other.superscriptSize) && FontEngineUtilities.Approximately(subscriptOffset, other.subscriptOffset) && FontEngineUtilities.Approximately(subscriptSize, other.subscriptSize) && FontEngineUtilities.Approximately(underlineOffset, other.underlineOffset) && FontEngineUtilities.Approximately(underlineThickness, other.underlineThickness) && FontEngineUtilities.Approximately(strikethroughOffset, other.strikethroughOffset) && FontEngineUtilities.Approximately(strikethroughThickness, other.strikethroughThickness) && FontEngineUtilities.Approximately(tabWidth, other.tabWidth);
		}
	}
}
