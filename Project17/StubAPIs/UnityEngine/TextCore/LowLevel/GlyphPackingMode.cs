using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	/// <summary>
	///   <para>The modes available when packing glyphs into an atlas texture.</para>
	/// </summary>
	[UsedByNativeCode]
	public enum GlyphPackingMode
	{
		/// <summary>
		///   <para>Place the glyph against the short side of a free space to minimize the length of the shorter leftover side.</para>
		/// </summary>
		BestShortSideFit,
		/// <summary>
		///   <para>Place the glyph against the longer side of a free space to minimize the length of the longer leftover side.</para>
		/// </summary>
		BestLongSideFit,
		/// <summary>
		///   <para>Place the glyph into the smallest free space available in which it can fit.</para>
		/// </summary>
		BestAreaFit,
		/// <summary>
		///   <para>Place the glyph into available free space in a Tetris like fashion.</para>
		/// </summary>
		BottomLeftRule,
		/// <summary>
		///   <para>Place the glyph into the available free space by trying to maximize the contact point between it and other glyphs.</para>
		/// </summary>
		ContactPointRule
	}
}
