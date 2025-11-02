using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	/// <summary>
	///   <para>The rendering modes used by the Font Engine to render glyphs.</para>
	/// </summary>
	[UsedByNativeCode]
	public enum GlyphRenderMode
	{
		/// <summary>
		///   <para>Renders a bitmap representation of the glyph from an 8-bit or antialiased image of the glyph outline with hinting.</para>
		/// </summary>
		SMOOTH_HINTED = 4121,
		/// <summary>
		///   <para>Renders a bitmap representation of the glyph from an 8-bit or antialiased image of the glyph outline with no hinting.</para>
		/// </summary>
		SMOOTH = 4117,
		/// <summary>
		///   <para>Renders a bitmap representation of the glyph from a binary (1-bit monochrome) image of the glyph outline with hinting.</para>
		/// </summary>
		RASTER_HINTED = 4122,
		/// <summary>
		///   <para>Renders a bitmap representation of the glyph from a binary (1-bit monochrome) image of the glyph outline with no hinting.</para>
		/// </summary>
		RASTER = 4118,
		/// <summary>
		///   <para>Renders a signed distance field (SDF) representation of the glyph from a binary (1-bit monochrome) image of the glyph outline with no hinting.</para>
		/// </summary>
		SDF = 4134,
		/// <summary>
		///   <para>Renders a signed distance field (SDF) representation of the glyph from a binary (1-bit monochrome) image of the glyph outline with no hinting.</para>
		/// </summary>
		SDF8 = 8230,
		/// <summary>
		///   <para>Renders a signed distance field (SDF) representation of the glyph from a binary (1-bit monochrome) image of the glyph outline with no hinting.</para>
		/// </summary>
		SDF16 = 16422,
		/// <summary>
		///   <para>Renders a signed distance field (SDF) representation of the glyph from a binary (1-bit monochrome) image of the glyph outline with no hinting.</para>
		/// </summary>
		SDF32 = 32806,
		/// <summary>
		///   <para>Renders a signed distance field (SDF) representation of the glyph from an 8-bit or antialiased image of the glyph outline with hinting.</para>
		/// </summary>
		SDFAA_HINTED = 4169,
		/// <summary>
		///   <para>Renders a signed distance field (SDF) representation of the glyph from an 8-bit or antialiased image of the glyph outline with no hinting.</para>
		/// </summary>
		SDFAA = 4165
	}
}
