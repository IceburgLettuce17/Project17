using System;

namespace UnityEngine
{
	/// <summary>
	///   <para>Encapsulates a Texture2D and its shader property name to give Sprite-based renderers access to a secondary texture, in addition to the main Sprite texture.</para>
	/// </summary>
	[Serializable]
	public struct SecondarySpriteTexture
	{
		/// <summary>
		///   <para>The shader property name of the secondary Sprite texture. Use this name to identify and sample the texture in the shader.</para>
		/// </summary>
		public string name;

		/// <summary>
		///   <para>The texture to be used as a secondary Sprite texture.</para>
		/// </summary>
		public Texture2D texture;
	}
}
