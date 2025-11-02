namespace UnityEngine.TextCore.LowLevel
{
	/// <summary>
	///   <para>Error code returned by the various FontEngine functions.</para>
	/// </summary>
	public enum FontEngineError
	{
		/// <summary>
		///   <para>Error code returned when the function was successfully executed.</para>
		/// </summary>
		Success = 0,
		/// <summary>
		///   <para>Error code returned by the LoadFontFace function when the file path to the source font file appears invalid.</para>
		/// </summary>
		Invalid_File_Path = 1,
		/// <summary>
		///   <para>Error code returned by the LoadFontFace function when the source font file is of an unknown or invalid format.</para>
		/// </summary>
		Invalid_File_Format = 2,
		/// <summary>
		///   <para>Error code returned by the LoadFontFace function when the source font file appears invalid or improperly formatted.</para>
		/// </summary>
		Invalid_File_Structure = 3,
		/// <summary>
		///   <para>Error code indicating an invalid font file.</para>
		/// </summary>
		Invalid_File = 4,
		/// <summary>
		///   <para>Error code indicating failure to load one of the tables of the font file.</para>
		/// </summary>
		Invalid_Table = 8,
		/// <summary>
		///   <para>Error code returned by the LoadGlyph function when referencing an invalid or out of range glyph index value.</para>
		/// </summary>
		Invalid_Glyph_Index = 16,
		/// <summary>
		///   <para>Error code returned by the LoadGlyph function when referencing an invalid Unicode character value.</para>
		/// </summary>
		Invalid_Character_Code = 17,
		/// <summary>
		///   <para>Error code returned by the LoadGlyph or SetFaceSize functions using an invalid pointSize value.</para>
		/// </summary>
		Invalid_Pixel_Size = 23,
		/// <summary>
		///   <para>Error code indicating failure to initialize the font engine library.</para>
		/// </summary>
		Invalid_Library = 33,
		/// <summary>
		///   <para>Error code indicating an invalid font face.</para>
		/// </summary>
		Invalid_Face = 35,
		/// <summary>
		///   <para>Error code indicating failure to initialize the font engine library and / or successfully load a font face.</para>
		/// </summary>
		Invalid_Library_or_Face = 41,
		/// <summary>
		///   <para>Error code returned when the FontEngine glyph packing or rendering process has been cancelled.</para>
		/// </summary>
		Atlas_Generation_Cancelled = 100,
		Invalid_SharedTextureData = 101
	}
}
