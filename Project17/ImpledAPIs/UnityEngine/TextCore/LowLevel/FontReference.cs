using System.Diagnostics;

namespace UnityEngine.TextCore.LowLevel
{
	[DebuggerDisplay("{familyName} - {styleName}")]
	internal struct FontReference
	{
		public string familyName;

		public string styleName;

		public int faceIndex;

		public string filePath;
	}
}
