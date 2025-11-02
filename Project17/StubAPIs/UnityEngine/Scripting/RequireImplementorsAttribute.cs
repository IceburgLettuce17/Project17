using System;

namespace UnityEngine.Scripting
{
	/// <summary>
	///   <para>When the interface type is marked, all types implementing that interface will be marked.</para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
	public class RequireImplementorsAttribute : Attribute
	{
	}
}
