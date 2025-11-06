# Project17
A compatibility layer for Unity 2017 (use .NET 4.6 experimental for best experience


# WHAT WORKS
- Mathematics (com.unity.mathematics) version 1.3.3 (bundled with Project17)

# WHAT DOESN'T WORK
- TextMeshPro (com.unity.textmeshpro) pretty much any version that has >= version 1.1.0 TMP_FontAsset (even with stub TextCore and Profiling APIs, it's a hassle to replace the TextCore calls manually...)
- Burst (com.unity.burst) pretty much any version (uses Unity.Collections APIs and is practically useless in Unity 2017)
- 2018.3's Package Manager (UI and Client)
- Device Simulator (com.unity.device-simulator) latest (uses some unimpled UIElements APIs, so no shims)
- Visual Scripting (com.unity.visual-scripting) latest (uses newer C# APIs like Cloners)

# DISCLAIMER
Almost nothing you import will work out of the box, even with this package. You may have to port it to C# 6.0, or add your own Stub/Shim APIs (until Project17 implements them). Don't file an issue if it's about a "feature FEATURE cannot be used as it is not part of the C# 6.0 specification" error.
  
# Stubs list
- UnityEngine Bindings and Scripting APIs (this makes it easier to port some Unity native components)
- TextCore (from 2020.3, required for TMP)
- Unity.Profiling (required for TMP)

# Shims list
- I've only done VisualElement and MouseManipulator from UIElements for now.
- In case you don't know what a shim is, it's basically backporting newer APIs using older ones

# My own implementations of APIs
- TickTimerHelper from UnityEditor (original class was private)
