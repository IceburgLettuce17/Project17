# Project17
A compatibility layer for Unity 2017 (use .NET 4.6 experimental for best experience


# WHAT WORKS
- Mathematics (com.unity.mathematics) version 1.3.3 (bundled with Project17)

# WHAT DOESN'T WORK
- TextMeshPro >= 1.4.0 (even with stub TextCore and Profiling APIs)
- Burst (com.unity.burst)
- 2018.3's Package Manager (UI and Client)

# DISCLAIMER
Almost nothing you import will work out of the box, even with this package. You may have to port it to C# 6.0, or add your own Stub APIs (until Project17 implements them). Don't file an issue if it's about a "feature FEATURE cannot be used as it is not part of the C# 6.0 specification" error.
  
