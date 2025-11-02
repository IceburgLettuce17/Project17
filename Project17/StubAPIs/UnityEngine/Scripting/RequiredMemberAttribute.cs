using System;

namespace UnityEngine.Scripting
{
	/// <summary>
	///   <para>When a type is marked, all of it's members with [RequiredMember] will be marked.</para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public class RequiredMemberAttribute : Attribute
	{
	}
}
