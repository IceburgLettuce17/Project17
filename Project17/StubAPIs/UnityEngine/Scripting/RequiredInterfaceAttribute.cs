using System;

namespace UnityEngine.Scripting
{
	/// <summary>
	///   <para>When a type is marked, all interface implementations of the specified types will be marked.</para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true)]
	public class RequiredInterfaceAttribute : Attribute
	{
		public RequiredInterfaceAttribute(Type interfaceType)
		{
		}
	}
}
