using System;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	/// <summary>
	///   <para>The various options (flags) used by the FontEngine when loading glyphs from a font face.</para>
	/// </summary>
	[Flags]
	[UsedByNativeCode]
	public enum GlyphLoadFlags
	{
		/// <summary>
		///   <para>Load glyph metrics and bitmap representation if available for the current face size.</para>
		/// </summary>
		LOAD_DEFAULT = 0,
		/// <summary>
		///   <para>Load glyphs at default font units without scaling. This flag implies LOAD_NO_HINTING and LOAD_NO_BITMAP and unsets LOAD_RENDER.</para>
		/// </summary>
		LOAD_NO_SCALE = 1,
		/// <summary>
		///   <para>Load glyphs without hinting.</para>
		/// </summary>
		LOAD_NO_HINTING = 2,
		/// <summary>
		///   <para>Load glyph metrics and render outline using 8-bit or antialiased image of the glyph.</para>
		/// </summary>
		LOAD_RENDER = 4,
		/// <summary>
		///   <para>Load glyphs and ignore embedded bitmap strikes.</para>
		/// </summary>
		LOAD_NO_BITMAP = 8,
		/// <summary>
		///   <para>Load glyphs using the auto hinter instead of the font's native hinter.</para>
		/// </summary>
		LOAD_FORCE_AUTOHINT = 0x20,
		/// <summary>
		///   <para>Load glyph metrics and render outline using 1-bit monochrome.</para>
		/// </summary>
		LOAD_MONOCHROME = 0x1000,
		/// <summary>
		///   <para>Load glyphs using the font's native hinter.</para>
		/// </summary>
		LOAD_NO_AUTOHINT = 0x8000,
		/// <summary>
		///   <para>Load glyph metrics without using the 'hdmx' table. This flag is mostly used to validate font data.</para>
		/// </summary>
		LOAD_COMPUTE_METRICS = 0x200000,
		/// <summary>
		///   <para>Load glyph metrics without allocating and loading the bitmap data.</para>
		/// </summary>
		LOAD_BITMAP_METRICS_ONLY = 0x400000
	}
}
