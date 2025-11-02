using System;
using System.Collections.Generic;
using System.Linq;

namespace NiceIO
{
	internal static class Extensions
	{
		public static IEnumerable<NPath> Copy(this IEnumerable<NPath> self, NPath dest)
		{
			if (dest.IsRelative)
			{
				throw new ArgumentException("When copying multiple files, the destination cannot be a relative path");
			}
			dest.EnsureDirectoryExists();
			return self.Select((NPath p) => p.Copy(dest.Combine(p.FileName))).ToArray();
		}

		public static IEnumerable<NPath> Move(this IEnumerable<NPath> self, NPath dest)
		{
			if (dest.IsRelative)
			{
				throw new ArgumentException("When moving multiple files, the destination cannot be a relative path");
			}
			dest.EnsureDirectoryExists();
			return self.Select((NPath p) => p.Move(dest.Combine(p.FileName))).ToArray();
		}

		public static IEnumerable<NPath> Delete(this IEnumerable<NPath> self)
		{
			foreach (NPath item in self)
			{
				item.Delete();
			}
			return self;
		}

		public static IEnumerable<string> InQuotes(this IEnumerable<NPath> self, SlashMode slashMode = SlashMode.Forward)
		{
			return self.Select((NPath p) => p.InQuotes(slashMode));
		}

		public static NPath ToNPath(this string path)
		{
			return new NPath(path);
		}

		public static IEnumerable<NPath> ToNPaths(this IEnumerable<string> paths)
		{
			return paths.Select((string p) => new NPath(p));
		}

		public static NPath[] ResolveWithFileSystem(this IEnumerable<NPath> paths)
		{
			return paths.Select((NPath p) => p.ResolveWithFileSystem()).ToArray();
		}

		public static string[] InQuotesResolved(this IEnumerable<NPath> paths)
		{
			return paths.Select((NPath p) => p.InQuotesResolved()).ToArray();
		}
	}
}
