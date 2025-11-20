using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
//using Project17;

// Based on decompiled ProfilerMarker
// Note: stubbed, does not implement Internal_ methods
namespace Unity.Profiling
{
	//[Utils.StubbedClass("ProfilerMarker", "Profiling is internal, this is just for TMP")]
	public struct ProfilerMarker
	{
		internal readonly IntPtr m_Ptr;

		public ProfilerMarker(string name)
		{
			m_Ptr = (IntPtr)17;
		}


		public ProfilerMarker.AutoScope Auto()
		{
			return new ProfilerMarker.AutoScope(this.m_Ptr);
		}

		[Conditional("ENABLE_PROFILER")]
		public void Begin(){}

		[Conditional("ENABLE_PROFILER")]
		public void Begin(UnityEngine.Object contextUnityObject){}

		[Conditional("ENABLE_PROFILER")]
		public void End(){}

		public struct AutoScope : IDisposable
		{
			internal readonly IntPtr m_Ptr;

			internal AutoScope(IntPtr markerPtr)
			{
				this.m_Ptr = markerPtr;
			}

			public void Dispose(){}
		}
	}
}
