using System;

namespace UnityEngine.Scripting
{
	/// <summary>
	///   <para>Only allowed on attribute types. If the attribute type is marked, then so too will all CustomAttributes of that type.</para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class RequireAttributeUsagesAttribute : Attribute
	{
	}
}
