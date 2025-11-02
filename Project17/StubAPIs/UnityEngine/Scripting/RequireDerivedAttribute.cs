using System;

namespace UnityEngine.Scripting
{
	/// <summary>
	///   <para>When the type is marked, all types derived from that type will also be marked.</para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class RequireDerivedAttribute : Attribute
	{
	}
}
