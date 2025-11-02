using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace NiceIO
{
	internal class NPath : IComparable, IEquatable<NPath>
	{
		private class SetCurrentDirectoryOnDispose : IDisposable
		{
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private readonly NPath _003CDirectory_003Ek__BackingField;

			public NPath Directory
			{
				[CompilerGenerated]
				get
				{
					return _003CDirectory_003Ek__BackingField;
				}
			}

			public SetCurrentDirectoryOnDispose(NPath directory)
			{
				_003CDirectory_003Ek__BackingField = directory;
			}

			public void Dispose()
			{
				SetCurrentDirectory(Directory);
			}
		}

		private abstract class NPathTLSCallback<T, TConcreteType> : IDisposable
		{
			[ThreadStatic]
			private static List<NPathTLSCallback<T, TConcreteType>> _activeCallbacks;

			private readonly Action<T> _callback;

			internal NPathTLSCallback(Action<T> callback)
			{
				_callback = callback;
				if (_activeCallbacks == null)
				{
					_activeCallbacks = new List<NPathTLSCallback<T, TConcreteType>>();
				}
				_activeCallbacks.Add(this);
			}

			public void Dispose()
			{
				if (_activeCallbacks == null || !_activeCallbacks.Remove(this))
				{
					throw new ObjectDisposedException(GetType().Name);
				}
				if (_activeCallbacks.Count == 0)
				{
					_activeCallbacks = null;
				}
			}

			protected internal static void Invoke(T globRequest)
			{
				if (_activeCallbacks == null)
				{
					return;
				}
				foreach (NPathTLSCallback<T, TConcreteType> activeCallback in _activeCallbacks)
				{
					activeCallback._callback(globRequest);
				}
			}
		}

		private sealed class GlobbingCallback : NPathTLSCallback<GlobRequest, GlobbingCallback>
		{
			public GlobbingCallback(Action<GlobRequest> callback)
				: base(callback)
			{
			}

			internal static void Invoke(NPath path, string filter, bool recurse)
			{
				GlobRequest globRequest = default(GlobRequest);
				globRequest.Path = path;
				globRequest.Filters = new string[1] { filter };
				globRequest.Recurse = recurse;
				NPathTLSCallback<GlobRequest, GlobbingCallback>.Invoke(globRequest);
			}

			internal static void Invoke(NPath path, string[] extensions, bool recurse)
			{
				GlobRequest globRequest = default(GlobRequest);
				globRequest.Path = path;
				globRequest.Filters = extensions.Select((string ext) => "*." + ext.TrimStart('.')).ToArray();
				globRequest.Recurse = recurse;
				NPathTLSCallback<GlobRequest, GlobbingCallback>.Invoke(globRequest);
			}
		}

		private class ReadContentsCallback : NPathTLSCallback<NPath, ReadContentsCallback>
		{
			public ReadContentsCallback(Action<NPath> callback)
				: base(callback)
			{
			}
		}

		private class StatCallback : NPathTLSCallback<NPath, StatCallback>
		{
			public StatCallback(Action<NPath> callback)
				: base(callback)
			{
			}
		}

		public abstract class FileSystem
		{
			[ThreadStatic]
			internal static FileSystem _active;

			public static FileSystem Active
			{
				get
				{
					FileSystem obj = _active ?? MakeFileSystemForCurrentMachine();
					_active = obj;
					return obj;
				}
			}

			private static FileSystem MakeFileSystemForCurrentMachine()
			{
				if (CalculateIsWindows())
				{
					return new WindowsFileSystem();
				}
				return new PosixFileSystem();
			}

			public abstract NPath[] Directory_GetFiles(NPath path, string filter, SearchOption searchOptions);

			public abstract bool Directory_Exists(NPath path);

			public abstract bool File_Exists(NPath path);

			public abstract void File_WriteAllBytes(NPath path, byte[] bytes);

			public abstract void File_Copy(NPath path, NPath destinationPath, bool overWrite);

			public abstract void File_Delete(NPath path);

			public abstract void File_Move(NPath path, NPath destinationPath);

			public abstract void File_WriteAllText(NPath path, string contents);

			public abstract string File_ReadAllText(NPath path);

			public abstract void File_WriteAllLines(NPath path, string[] contents);

			public abstract byte[] File_ReadAllBytes(NPath path);

			public abstract string[] File_ReadAllLines(NPath path);

			public abstract void File_SetLastWriteTimeUtc(NPath path, DateTime lastWriteTimeUtc);

			public abstract DateTime File_GetLastWriteTimeUtc(NPath path);

			public abstract long File_GetSize(NPath path);

			public abstract void File_SetAttributes(NPath path, FileAttributes value);

			public abstract FileAttributes File_GetAttributes(NPath path);

			public abstract void Directory_CreateDirectory(NPath path);

			public abstract void Directory_Delete(NPath path, bool b);

			public abstract void Directory_Move(NPath path, NPath destPath);

			public abstract NPath Directory_GetCurrentDirectory();

			public abstract void Directory_SetCurrentDirectory(NPath directoryPath);

			public abstract NPath[] Directory_GetDirectories(NPath path, string filter, SearchOption searchOptions);

			public abstract NPath Resolve(NPath path);

			public abstract bool IsSymbolicLink(NPath path);

			public abstract void CreateSymbolicLink(NPath fromPath, NPath targetPath, bool targetIsFile);
		}

		private abstract class SystemIOFileSystem : FileSystem
		{
			public override NPath[] Directory_GetFiles(NPath path, string filter, SearchOption searchOptions)
			{
				return Directory.GetFiles(path.ToString(SlashMode.Native), filter, searchOptions).ToNPaths().ToArray();
			}

			public override bool Directory_Exists(NPath path)
			{
				return Directory.Exists(path.ToString(SlashMode.Native));
			}

			public override bool File_Exists(NPath path)
			{
				return File.Exists(path.ToString(SlashMode.Native));
			}

			public override void File_WriteAllBytes(NPath path, byte[] bytes)
			{
				File.WriteAllBytes(path.ToString(SlashMode.Native), bytes);
			}

			public override void File_Copy(NPath path, NPath destinationPath, bool overWrite)
			{
				File.Copy(path.ToString(SlashMode.Native), destinationPath.ToString(SlashMode.Native), overWrite);
			}

			public override void File_Delete(NPath path)
			{
				File.Delete(path.ToString(SlashMode.Native));
			}

			public override void File_Move(NPath path, NPath destinationPath)
			{
				File.Move(path.ToString(SlashMode.Native), destinationPath.ToString(SlashMode.Native));
			}

			public override void File_WriteAllText(NPath path, string contents)
			{
				File.WriteAllText(path.ToString(SlashMode.Native), contents);
			}

			public override string File_ReadAllText(NPath path)
			{
				return File.ReadAllText(path.ToString(SlashMode.Native));
			}

			public override void File_WriteAllLines(NPath path, string[] contents)
			{
				File.WriteAllLines(path.ToString(SlashMode.Native), contents);
			}

			public override string[] File_ReadAllLines(NPath path)
			{
				return File.ReadAllLines(path.ToString(SlashMode.Native));
			}

			public override byte[] File_ReadAllBytes(NPath path)
			{
				return File.ReadAllBytes(path.ToString(SlashMode.Native));
			}

			public override void File_SetLastWriteTimeUtc(NPath path, DateTime lastWriteTimeUtc)
			{
				File.SetLastWriteTimeUtc(path.ToString(SlashMode.Native), lastWriteTimeUtc);
			}

			public override DateTime File_GetLastWriteTimeUtc(NPath path)
			{
				return File.GetLastWriteTimeUtc(path.ToString(SlashMode.Native));
			}

			public override void File_SetAttributes(NPath path, FileAttributes value)
			{
				File.SetAttributes(path.ToString(SlashMode.Native), value);
			}

			public override FileAttributes File_GetAttributes(NPath path)
			{
				return File.GetAttributes(path.ToString(SlashMode.Native));
			}

			public override long File_GetSize(NPath path)
			{
				return new FileInfo(path.ToString(SlashMode.Native)).Length;
			}

			public override void Directory_CreateDirectory(NPath path)
			{
				Directory.CreateDirectory(path.ToString(SlashMode.Native));
			}

			public override void Directory_Delete(NPath path, bool b)
			{
				Directory.Delete(path.ToString(SlashMode.Native), b);
			}

			public override void Directory_Move(NPath path, NPath destPath)
			{
				Directory.Move(path.ToString(SlashMode.Native), destPath.ToString(SlashMode.Native));
			}

			public override NPath Directory_GetCurrentDirectory()
			{
				return Directory.GetCurrentDirectory();
			}

			public override void Directory_SetCurrentDirectory(NPath path)
			{
				Directory.SetCurrentDirectory(path.ToString(SlashMode.Native));
			}

			public override NPath[] Directory_GetDirectories(NPath path, string filter, SearchOption searchOptions)
			{
				return Directory.GetDirectories(path.ToString(SlashMode.Native), filter, searchOptions).ToNPaths().ToArray();
			}

			public override NPath Resolve(NPath path)
			{
				return path;
			}
		}

		private class WindowsFileSystem : SystemIOFileSystem
		{
			private static class Win32Native
			{
				[Flags]
				public enum SymbolicLinkFlags
				{
					File = 0,
					Directory = 1,
					AllowUnprivilegedCreate = 2
				}

				public enum CreationDisposition : uint
				{
					New = 1u,
					CreateAlways,
					OpenExisting,
					OpenAlways,
					TruncateExisting
				}

				[Flags]
				public enum FileAccess : uint
				{
					GenericRead = 0x80000000u,
					GenericWrite = 0x40000000u,
					GenericExecute = 0x20000000u,
					GenericAll = 0x10000000u,
					FileReadAttributes = 0x80u,
					FileWriteAttributes = 0x100u,
					FileAppendData = 4u
				}

				[Flags]
				public enum FileAttributes : uint
				{
					Readonly = 1u,
					Hidden = 2u,
					System = 4u,
					Directory = 0x10u,
					Archive = 0x20u,
					Device = 0x40u,
					Normal = 0x80u,
					Temporary = 0x100u,
					SparseFile = 0x200u,
					ReparsePoint = 0x400u,
					Compressed = 0x800u,
					Offline = 0x1000u,
					NotContentIndexed = 0x2000u,
					Encrypted = 0x4000u,
					Write_Through = 0x80000000u,
					Overlapped = 0x40000000u,
					NoBuffering = 0x20000000u,
					RandomAccess = 0x10000000u,
					SequentialScan = 0x8000000u,
					DeleteOnClose = 0x4000000u,
					BackupSemantics = 0x2000000u,
					PosixSemantics = 0x1000000u,
					OpenReparsePoint = 0x200000u,
					OpenNoRecall = 0x100000u,
					FirstPipeInstance = 0x80000u
				}

				[Flags]
				public enum MoveFileExFlags : uint
				{
					None = 0u,
					ReplaceExisting = 1u,
					CopyAllowed = 2u,
					DelayUntilReboot = 4u,
					WriteThrough = 8u,
					CreateHardlink = 0x10u,
					FailIfNotTrackable = 0x20u
				}

				[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
				public struct FIND_DATA
				{
					public FileAttributes dwFileAttributes;

					public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;

					public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;

					public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;

					public uint nFileSizeHigh;

					public uint nFileSizeLow;

					public uint dwReserved0;

					public uint dwReserved1;

					[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
					public string cFileName;

					[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
					public string cAlternateFileName;
				}

				public const long ERROR_FILE_NOT_FOUND = 2L;

				public const int ERROR_INVALID_NAME = 123;

				public const int ERROR_DIR_NOT_EMPTY = 145;

				public const int MAX_PATH_LEN = 260;

				public const uint IO_REPARSE_TAG_SYMLINK = 2684354572u;

				public const uint INVALID_FILE_ATTRIBUTES = uint.MaxValue;

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				public static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SymbolicLinkFlags dwFlags);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
				public static extern IntPtr FindFirstFile(string fileName, out FIND_DATA findData);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
				[return: MarshalAs(UnmanagedType.Bool)]
				public static extern bool FindNextFile(IntPtr handle, out FIND_DATA findData);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
				public static extern bool FindClose(IntPtr handle);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				public static extern uint GetFileAttributes(string fileName);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				public static extern void SetFileAttributes(string fileName, uint attributes);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				[return: MarshalAs(UnmanagedType.Bool)]
				public static extern bool DeleteFile(string lpFileName);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				[return: MarshalAs(UnmanagedType.Bool)]
				public static extern bool CopyFile(string sourceFileName, string destFileName, bool failIfExists);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				[return: MarshalAs(UnmanagedType.Bool)]
				public static extern bool MoveFile(string existingFileName, string newFileName);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				[return: MarshalAs(UnmanagedType.Bool)]
				public static extern bool MoveFileEx(string existingFileName, string newFileName, MoveFileExFlags exFlags);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				[return: MarshalAs(UnmanagedType.Bool)]
				public static extern bool CreateDirectory(string lpPathName, IntPtr lpSecurityAttributes);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				[return: MarshalAs(UnmanagedType.Bool)]
				public static extern bool RemoveDirectory(string lpPathName);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				[return: MarshalAs(UnmanagedType.Bool)]
				public static extern bool SetCurrentDirectory(string lpPathName);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				internal static extern uint GetCurrentDirectory(uint nBufferLength, char[] lpBuffer);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				public static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess, FileShare dwShareMode, IntPtr lpSecurityAttributes, CreationDisposition dwCreationDisposition, FileAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "SetFileTime", SetLastError = true)]
				[return: MarshalAs(UnmanagedType.Bool)]
				public static extern bool SetLastWriteFileTime(IntPtr hFile, IntPtr lpCreationTime, IntPtr lpLastAccessTime, ref long lpLastWriteTime);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				public static extern uint GetShortPathName(string lpszLongPath, char[] lpszShortPath, uint cchBuffer);

				[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
				public static extern uint GetLongPathName(string lpszShortPath, char[] lpszLongPath, uint cchBuffer);
			}

			public override long File_GetSize(NPath path)
			{
				if (path._path.Length < 260)
				{
					return base.File_GetSize(path);
				}
				string fileName = MakeLongPath(path).TrimEnd('\\');
				Win32Native.FIND_DATA findData;
				IntPtr intPtr = Win32Native.FindFirstFile(fileName, out findData);
				if (intPtr == new IntPtr(-1))
				{
					throw new FileNotFoundException(string.Format("The path {0} does not exist.", path), path.ToString());
				}
				Win32Native.FindClose(intPtr);
				long num = findData.nFileSizeLow;
				return (num < 0 && (long)findData.nFileSizeHigh > 0L) ? (num + 4294967296L + (long)findData.nFileSizeHigh * 4294967296L) : (((long)findData.nFileSizeHigh > 0L) ? (num + (long)findData.nFileSizeHigh * 4294967296L) : ((num >= 0) ? num : (num + 4294967296L)));
			}

			public override bool File_Exists(NPath path)
			{
				if (path._path.Length < 260)
				{
					return base.File_Exists(path);
				}
				string fileName = MakeLongPath(path).TrimEnd('\\');
				uint fileAttributes = Win32Native.GetFileAttributes(fileName);
				return fileAttributes != uint.MaxValue && (fileAttributes & 0x10) == 0;
			}

			public override void File_Delete(NPath path)
			{
				if (!IsSymbolicLink(path) && path._path.Length < 260)
				{
					base.File_Delete(path);
				}
				else
				{
					InternalFileDelete(path);
				}
			}

			private void InternalFileDelete(NPath path)
			{
				string text = MakeLongPath(path).TrimEnd('\\');
				if (!Win32Native.DeleteFile(text) && !Win32Native.MoveFileEx(text, null, Win32Native.MoveFileExFlags.DelayUntilReboot))
				{
					throw new IOException(string.Format("Cannot delete file {0}.", path), new Win32Exception(Marshal.GetLastWin32Error()));
				}
			}

			public override void File_Copy(NPath path, NPath destinationPath, bool overWrite)
			{
				if (path._path.Length < 260 && destinationPath._path.Length < 260)
				{
					base.File_Copy(path, destinationPath, overWrite);
					return;
				}
				string sourceFileName = MakeLongPath(path).TrimEnd('\\');
				string destFileName = MakeLongPath(destinationPath).TrimEnd('\\');
				if (!Win32Native.CopyFile(sourceFileName, destFileName, !overWrite))
				{
					throw new IOException(string.Format("Cannot copy file {0} to {1}.", path, destinationPath), new Win32Exception(Marshal.GetLastWin32Error()));
				}
			}

			public override void File_Move(NPath path, NPath destinationPath)
			{
				if (path._path.Length < 260 && destinationPath._path.Length < 260)
				{
					base.File_Move(path, destinationPath);
					return;
				}
				string text = MakeLongPath(path).TrimEnd('\\');
				string text2 = MakeLongPath(destinationPath).TrimEnd('\\');
				if (!Win32Native.MoveFile(text, text2))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (!Win32Native.CopyFile(text, text2, true))
					{
						throw new IOException(string.Format("Cannot move file {0} to {1}.", path, destinationPath), new Win32Exception(lastWin32Error));
					}
					if (!Win32Native.DeleteFile(text))
					{
						throw new IOException(string.Format("Cannot move file {0} to {1}.", path, destinationPath), new Win32Exception(lastWin32Error));
					}
				}
			}

			public override bool Directory_Exists(NPath path)
			{
				if (path._path.Length < 260)
				{
					return base.Directory_Exists(path);
				}
				string fileName = MakeLongPath(path).TrimEnd('\\');
				uint fileAttributes = Win32Native.GetFileAttributes(fileName);
				return fileAttributes != uint.MaxValue && ((fileAttributes & 0x10u) != 0 || (fileAttributes & 0x400) != 0);
			}

			public override void Directory_CreateDirectory(NPath path)
			{
				if (path._path.Length < 248)
				{
					base.Directory_CreateDirectory(path);
				}
				else
				{
					InternalCreateDirectory(path);
				}
			}

			private void InternalCreateDirectory(NPath directoryPath)
			{
				if (Directory_Exists(directoryPath))
				{
					return;
				}
				string text = directoryPath.ToString(SlashMode.Native).TrimEnd('\\');
				int num = text.LastIndexOf("\\");
				if (num > 2)
				{
					InternalCreateDirectory(new NPath(text.Substring(0, num)));
				}
				string lpPathName = MakeLongPath(text, 248);
				if (Win32Native.CreateDirectory(lpPathName, IntPtr.Zero))
				{
					return;
				}
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 123 && directoryPath.FileName.Length > 255)
				{
					throw new PathTooLongException("Directory name " + directoryPath.FileName + " exceeds limit of 255 characters.");
				}
				throw new IOException(string.Format("Cannot create directory {0}.", directoryPath), new Win32Exception(lastWin32Error));
			}

			public override void Directory_Delete(NPath path, bool recursive)
			{
				if (recursive)
				{
					NPath[] array = Directory_GetFiles(path, "*", SearchOption.TopDirectoryOnly);
					NPath[] array2 = Directory_GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
					try
					{
						NPath[] array3 = array;
						foreach (NPath path2 in array3)
						{
							FileAttributes fileAttributes = File_GetAttributes(path2);
							if ((fileAttributes & FileAttributes.ReadOnly) != 0)
							{
								File_SetAttributes(path2, fileAttributes & ~FileAttributes.ReadOnly);
							}
							File_Delete(path2);
						}
						NPath[] array4 = array2;
						foreach (NPath path3 in array4)
						{
							Directory_Delete(path3, true);
						}
					}
					catch (IOException innerException)
					{
						throw new IOException(string.Format("Cannot delete directory {0}.", path), innerException);
					}
				}
				string lpPathName = MakeLongPath(path).TrimEnd('\\');
				if (!Win32Native.RemoveDirectory(lpPathName))
				{
					throw new IOException(string.Format("Cannot delete directory {0}.", path), new Win32Exception(Marshal.GetLastWin32Error()));
				}
			}

			public override void Directory_Move(NPath path, NPath destinationPath)
			{
				if (path._path.Length < 260 && destinationPath._path.Length < 260)
				{
					base.Directory_Move(path, destinationPath);
					return;
				}
				string existingFileName = MakeLongPath(path).TrimEnd('\\');
				string newFileName = MakeLongPath(destinationPath).TrimEnd('\\');
				if (Win32Native.MoveFileEx(existingFileName, newFileName, Win32Native.MoveFileExFlags.CopyAllowed | Win32Native.MoveFileExFlags.WriteThrough))
				{
					return;
				}
				int lastWin32Error = Marshal.GetLastWin32Error();
				try
				{
					if (!Directory_Exists(destinationPath))
					{
						Directory_CreateDirectory(destinationPath);
					}
					NPath[] array = Directory_GetFiles(path, "*", SearchOption.AllDirectories);
					NPath[] array2 = array;
					foreach (NPath nPath in array2)
					{
						NPath nPath2 = destinationPath.Combine(nPath.RelativeTo(path));
						nPath2.Parent.EnsureDirectoryExists();
						File_Copy(nPath, nPath2, false);
					}
					Directory_Delete(path, true);
				}
				catch (IOException)
				{
					throw new IOException(string.Format("Cannot move directory {0} to {1}.", path, destinationPath), new Win32Exception(lastWin32Error));
				}
			}

			public override bool IsSymbolicLink(NPath path)
			{
				string fileName = MakeLongPath(path).TrimEnd('\\');
				Win32Native.FIND_DATA findData;
				IntPtr intPtr = Win32Native.FindFirstFile(fileName, out findData);
				if (intPtr == new IntPtr(-1))
				{
					throw new FileNotFoundException(string.Format("The path {0} does not exist.", path), path.ToString());
				}
				Win32Native.FindClose(intPtr);
				if ((findData.dwFileAttributes & Win32Native.FileAttributes.ReparsePoint) != Win32Native.FileAttributes.ReparsePoint)
				{
					return false;
				}
				return findData.dwReserved0 == 2684354572u;
			}

			public override void CreateSymbolicLink(NPath fromPath, NPath targetPath, bool targetIsFile)
			{
				Win32Native.SymbolicLinkFlags symbolicLinkFlags = Win32Native.SymbolicLinkFlags.File;
				if (CalculateIsWindows10())
				{
					symbolicLinkFlags |= Win32Native.SymbolicLinkFlags.AllowUnprivilegedCreate;
				}
				if (!targetIsFile)
				{
					symbolicLinkFlags |= Win32Native.SymbolicLinkFlags.Directory;
				}
				string text = MakeLongPath(fromPath).TrimEnd('\\');
				string lpTargetFileName = MakeLongPath(targetPath).TrimEnd('\\');
				if (!Win32Native.CreateSymbolicLink(text, lpTargetFileName, symbolicLinkFlags))
				{
					throw new IOException(string.Format("Cannot create symbolic link {0} from {1}.", text, targetPath), new Win32Exception(Marshal.GetLastWin32Error()));
				}
			}

			public override FileAttributes File_GetAttributes(NPath path)
			{
				if (path._path.Length < 260)
				{
					return base.File_GetAttributes(path);
				}
				string fileName = MakeLongPath(path).TrimEnd('\\');
				return (FileAttributes)((int)Win32Native.GetFileAttributes(fileName) & 0xFFFF);
			}

			public override void File_SetAttributes(NPath path, FileAttributes value)
			{
				if (path._path.Length < 260)
				{
					base.File_SetAttributes(path, value);
					return;
				}
				string fileName = MakeLongPath(path).TrimEnd('\\');
				Win32Native.SetFileAttributes(fileName, (uint)value);
			}

			public override NPath[] Directory_GetFiles(NPath path, string filter, SearchOption searchOptions)
			{
				if (!Directory_Exists(path))
				{
					throw new DirectoryNotFoundException(string.Format("The path {0} does not exist.", path));
				}
				List<NPath> list = new List<NPath>();
				string fileName = MakeLongPath(path, 260 - filter.Length - 1).TrimEnd('\\') + "\\" + filter;
				Win32Native.FIND_DATA findData;
				IntPtr intPtr = Win32Native.FindFirstFile(fileName, out findData);
				if (intPtr != new IntPtr(-1))
				{
					try
					{
						do
						{
							string cFileName = findData.cFileName;
							if ((findData.dwFileAttributes & Win32Native.FileAttributes.Directory) == 0)
							{
								list.Add(path.Combine(cFileName));
							}
						}
						while (Win32Native.FindNextFile(intPtr, out findData));
					}
					finally
					{
						Win32Native.FindClose(intPtr);
					}
				}
				if (searchOptions == SearchOption.AllDirectories)
				{
					NPath[] array = Directory_GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
					foreach (NPath path2 in array)
					{
						list.AddRange(Directory_GetFiles(path2, filter, searchOptions));
					}
				}
				return list.ToArray();
			}

			public override NPath[] Directory_GetDirectories(NPath path, string filter, SearchOption searchOptions)
			{
				List<NPath> list = new List<NPath>();
				string fileName = MakeLongPath(path, 260 - filter.Length - 1).TrimEnd('\\') + "\\" + filter;
				Win32Native.FIND_DATA findData;
				IntPtr intPtr = Win32Native.FindFirstFile(fileName, out findData);
				if (intPtr != new IntPtr(-1))
				{
					try
					{
						do
						{
							string cFileName = findData.cFileName;
							if ((findData.dwFileAttributes & Win32Native.FileAttributes.Directory) != 0 && cFileName != "." && cFileName != "..")
							{
								list.Add(path.Combine(cFileName));
							}
						}
						while (Win32Native.FindNextFile(intPtr, out findData));
					}
					finally
					{
						Win32Native.FindClose(intPtr);
					}
				}
				if (searchOptions == SearchOption.AllDirectories)
				{
					NPath[] array = Directory_GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
					foreach (NPath path2 in array)
					{
						list.AddRange(Directory_GetDirectories(path2, filter, searchOptions));
					}
				}
				return list.ToArray();
			}

			public override void File_WriteAllText(NPath path, string contents)
			{
				if (path._path.Length < 260)
				{
					base.File_WriteAllText(path, contents);
					return;
				}
				string filePath = MakeLongPath(path).TrimEnd('\\');
				using (SafeFileHandle handle = CreateFileHandle(path, filePath, Win32Native.CreationDisposition.CreateAlways, Win32Native.FileAccess.GenericWrite, FileShare.Read))
				{
					using (FileStream stream = new FileStream(handle, FileAccess.Write))
					{
						using (StreamWriter streamWriter = new StreamWriter(stream))
						{
							streamWriter.Write(contents, new UTF8Encoding(false, true));
						}
					}
				}
			}

			public override void File_WriteAllLines(NPath path, string[] contents)
			{
				File_WriteAllText(path, string.Join(Environment.NewLine, contents));
			}

			public override void File_WriteAllBytes(NPath path, byte[] bytes)
			{
				if (path._path.Length < 260)
				{
					base.File_WriteAllBytes(path, bytes);
					return;
				}
				if (!path.Parent.Exists())
				{
					path.Parent.CreateDirectory();
				}
				string filePath = MakeLongPath(path).TrimEnd('\\');
				using (SafeFileHandle handle = CreateFileHandle(path, filePath, Win32Native.CreationDisposition.CreateAlways, Win32Native.FileAccess.GenericWrite, FileShare.Read))
				{
					using (FileStream fileStream = new FileStream(handle, FileAccess.Write))
					{
						fileStream.Write(bytes, 0, bytes.Length);
					}
				}
			}

			public override string File_ReadAllText(NPath path)
			{
				if (path._path.Length < 260)
				{
					return base.File_ReadAllText(path);
				}
				string filePath = MakeLongPath(path).TrimEnd('\\');
				using (SafeFileHandle handle = CreateFileHandle(path, filePath, Win32Native.CreationDisposition.OpenExisting, Win32Native.FileAccess.GenericRead, FileShare.Read))
				{
					string result;
					using (FileStream stream = new FileStream(handle, FileAccess.Read))
					{
						using (StreamReader streamReader = new StreamReader(stream, new UTF8Encoding(false, true)))
						{
							result = streamReader.ReadToEnd();
						}
					}
					return result;
				}
			}

			public override string[] File_ReadAllLines(NPath path)
			{
				if (path._path.Length < 260)
				{
					return base.File_ReadAllLines(path);
				}
				List<string> list = new List<string>();
				string filePath = MakeLongPath(path).TrimEnd('\\');
				using (SafeFileHandle handle = CreateFileHandle(path, filePath, Win32Native.CreationDisposition.OpenExisting, Win32Native.FileAccess.GenericRead, FileShare.Read))
				{
					using (FileStream stream = new FileStream(handle, FileAccess.Read))
					{
						using (StreamReader streamReader = new StreamReader(stream, new UTF8Encoding(false, true)))
						{
							string item;
							while ((item = streamReader.ReadLine()) != null)
							{
								list.Add(item);
							}
						}
					}
				}
				return list.ToArray();
			}

			public override byte[] File_ReadAllBytes(NPath path)
			{
				if (path._path.Length < 260)
				{
					return base.File_ReadAllBytes(path);
				}
				byte[] array = null;
				string filePath = MakeLongPath(path).TrimEnd('\\');
				using (SafeFileHandle handle = CreateFileHandle(path, filePath, Win32Native.CreationDisposition.OpenExisting, Win32Native.FileAccess.GenericRead, FileShare.Read))
				{
					using (FileStream fileStream = new FileStream(handle, FileAccess.Read))
					{
						array = new byte[fileStream.Length];
						fileStream.Read(array, 0, array.Length);
					}
				}
				return array;
			}

			public override void File_SetLastWriteTimeUtc(NPath path, DateTime lastWriteTimeUtc)
			{
				if (path._path.Length < 260)
				{
					base.File_SetLastWriteTimeUtc(path, lastWriteTimeUtc);
					return;
				}
				string filePath = MakeLongPath(path).TrimEnd('\\');
				using (SafeFileHandle safeFileHandle = CreateFileHandle(path, filePath, Win32Native.CreationDisposition.OpenExisting, Win32Native.FileAccess.FileWriteAttributes, FileShare.ReadWrite))
				{
					long lpLastWriteTime = lastWriteTimeUtc.ToFileTime();
					if (!Win32Native.SetLastWriteFileTime(safeFileHandle.DangerousGetHandle(), IntPtr.Zero, IntPtr.Zero, ref lpLastWriteTime))
					{
						throw new IOException(string.Format("Cannot set last write time to {0}.", path), new Win32Exception(Marshal.GetLastWin32Error()));
					}
				}
			}

			public override DateTime File_GetLastWriteTimeUtc(NPath path)
			{
				if (path._path.Length < 260)
				{
					return base.File_GetLastWriteTimeUtc(path);
				}
				string fileName = MakeLongPath(path).TrimEnd('\\');
				Win32Native.FIND_DATA findData;
				IntPtr intPtr = Win32Native.FindFirstFile(fileName, out findData);
				if (intPtr == new IntPtr(-1))
				{
					return DateTime.MinValue;
				}
				try
				{
					if (intPtr.ToInt64() == 2)
					{
						return DateTime.MinValue;
					}
					long fileTime = ((long)findData.ftLastWriteTime.dwHighDateTime << 32) + findData.ftLastWriteTime.dwLowDateTime;
					return DateTime.FromFileTimeUtc(fileTime);
				}
				finally
				{
					Win32Native.FindClose(intPtr);
				}
			}

			public override void Directory_SetCurrentDirectory(NPath path)
			{
				if (path._path.Length < 260)
				{
					base.Directory_SetCurrentDirectory(path);
					return;
				}
				string shortName = GetShortName(path);
				if (Win32Native.SetCurrentDirectory(shortName))
				{
					return;
				}
				throw new IOException(string.Format("Cannot set current directory to {0}.", path), new Win32Exception(Marshal.GetLastWin32Error()));
			}

			public override NPath Directory_GetCurrentDirectory()
			{
				NPath nPath = Directory.GetCurrentDirectory();
				if (nPath._path.IndexOf('~') > 0)
				{
					return GetLongName(nPath);
				}
				return nPath;
			}

			private static string GetShortName(NPath path)
			{
				string lpszLongPath = MakeLongPath(path).TrimEnd('\\');
				char[] array = new char[260];
				uint shortPathName = Win32Native.GetShortPathName(lpszLongPath, array, (uint)array.Length);
				if (shortPathName == 0)
				{
					throw new IOException(string.Format("Cannot get short path name for {0}.", path), new Win32Exception(Marshal.GetLastWin32Error()));
				}
				string text = new string(array);
				return text.Substring(0, (int)shortPathName);
			}

			private static string GetLongName(NPath path)
			{
				char[] lpszLongPath = null;
				uint longPathName = Win32Native.GetLongPathName(path._path, lpszLongPath, 0u);
				if (longPathName == 0)
				{
					throw new IOException(string.Format("Cannot get long path name for {0}.", path), new Win32Exception(Marshal.GetLastWin32Error()));
				}
				lpszLongPath = new char[longPathName];
				if (Win32Native.GetLongPathName(path._path, lpszLongPath, longPathName) == 0)
				{
					throw new IOException(string.Format("Cannot get long path name for {0}.", path), new Win32Exception(Marshal.GetLastWin32Error()));
				}
				string text = new string(lpszLongPath);
				return text.Substring(0, (int)(longPathName - 1));
			}

			private static SafeFileHandle CreateFileHandle(NPath path, string filePath, Win32Native.CreationDisposition creationDisposition, Win32Native.FileAccess fileAccess, FileShare fileShare)
			{
				SafeFileHandle safeFileHandle = Win32Native.CreateFile(filePath, fileAccess, fileShare, IntPtr.Zero, creationDisposition, Win32Native.FileAttributes.Normal, IntPtr.Zero);
				if (safeFileHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 123 && path.FileName.Length > 255)
					{
						throw new PathTooLongException("File name " + path.FileName + " exceeds limit of 255 characters.");
					}
					throw new IOException("Cannot access " + filePath + ".", new Win32Exception(lastWin32Error));
				}
				return safeFileHandle;
			}

			private static string MakeLongPath(NPath path, int maxLength = 260)
			{
				NPath nPath = path._path;
				if (nPath.IsRelative)
				{
					nPath = nPath.MakeAbsolute();
				}
				string text = nPath.ToString(SlashMode.Native);
				if (string.IsNullOrEmpty(text) || text.StartsWith("\\\\?\\"))
				{
					return text;
				}
				if (text.Length >= maxLength)
				{
					return "\\\\?\\" + text;
				}
				return text;
			}
		}

		private class PosixFileSystem : SystemIOFileSystem
		{
			private static class PosixNative
			{
				public struct Stat
				{
					public ulong st_dev;

					public ulong st_ino;

					public uint st_mode;

					[NonSerialized]
					private uint _padding_;

					public ulong st_nlink;

					public uint st_uid;

					public uint st_gid;

					public ulong st_rdev;

					public long st_size;

					public long st_blksize;

					public long st_blocks;

					public long st_atime;

					public long st_mtime;

					public long st_ctime;

					public long st_atime_nsec;

					public long st_mtime_nsec;

					public long st_ctime_nsec;
				}

				private const uint Mono_Posix_FilePermissions_S_IFLNK = 40960u;

				[DllImport("libc", SetLastError = true)]
				public static extern int symlink([MarshalAs(UnmanagedType.LPStr)] string targetPath, [MarshalAs(UnmanagedType.LPStr)] string linkPath);

				[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_lstat", SetLastError = true)]
				public static extern int lstat(string file_name, out Stat buf);

				public static bool S_ISLNK(uint m)
				{
					return (m & 0xA000) == 40960;
				}
			}

			public override bool IsSymbolicLink(NPath path)
			{
				PosixNative.Stat buf;
				if (PosixNative.lstat(path.ToString(SlashMode.Native), out buf) != 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new IOException(string.Format("Failed to create stat path {0} (error code {1})", this, lastWin32Error), lastWin32Error);
				}
				return PosixNative.S_ISLNK(buf.st_mode);
			}

			public override void CreateSymbolicLink(NPath fromPath, NPath targetPath, bool targetIsFile)
			{
				if (PosixNative.symlink(targetPath.ToString(SlashMode.Native), fromPath.ToString(SlashMode.Native)) != 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new IOException(string.Format("Failed to create symlink (error code {0})", lastWin32Error), lastWin32Error);
				}
			}

			public override void File_SetAttributes(NPath path, FileAttributes value)
			{
				if ((value & FileAttributes.Hidden) != 0)
				{
					throw new NotSupportedException(string.Format("{0} file attribute is only supported on Windows.", FileAttributes.Hidden));
				}
				base.File_SetAttributes(path, value);
			}
		}

		private class WithFileSystemHelper : IDisposable
		{
			private FileSystem _previousFileSystem;

			private FileSystem _newFileSystem;

			public WithFileSystemHelper(FileSystem newFileSystem)
			{
				_previousFileSystem = FileSystem.Active;
				_newFileSystem = newFileSystem;
				FileSystem._active = newFileSystem;
			}

			public void Dispose()
			{
				if (FileSystem._active != _newFileSystem)
				{
					throw new InvalidOperationException("While disposing WithFileSystem result, the originally set FileSystem was not the active one.");
				}
				FileSystem._active = _previousFileSystem;
			}
		}

		private class WithFrozenDirectoryHelper : IDisposable
		{
			public void Dispose()
			{
				_frozenCurrentDirectory = null;
			}
		}

		private static readonly bool k_IsCaseSensitiveFileSystem = !CalculateIsWindows() && Directory.Exists("/proc");

		private static readonly bool k_IsWindows = CalculateIsWindows();

		private static readonly StringComparison PathStringComparison = (k_IsCaseSensitiveFileSystem ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);

		private readonly string _path;

		[ThreadStatic]
		private static NPath _frozenCurrentDirectory;

		private const int MethodImplOptions_AggressiveInlining = 256;

		private static NPath Empty
		{
			get
			{
				return new NPath("");
			}
		}

		public NPath Parent
		{
			get
			{
				if (IsRoot)
				{
					throw new ArgumentException(string.Format("Parent invoked on {0}", this));
				}
				for (int num = _path.Length - 1; num >= 0; num--)
				{
					if (num == 0)
					{
						return (_path[0] == '/') ? new NPath("/") : new NPath("");
					}
					if (_path[num] == '/')
					{
						int length = ((_path[num - 1] == ':' || _path[0] == '/') ? (num + 1) : num);
						string path = _path.Substring(0, length);
						return new NPath(path);
					}
				}
				return Empty;
			}
		}

		public bool IsRelative
		{
			get
			{
				if (_path[0] == '/')
				{
					return false;
				}
				if (_path.Length >= 3 && _path[1] == ':' && _path[2] == '/')
				{
					return false;
				}
				if (_path[0] == '\\')
				{
					return false;
				}
				return true;
			}
		}

		public string FileName
		{
			get
			{
				ThrowIfRoot();
				if (_path.Length == 0)
				{
					return string.Empty;
				}
				if (_path == ".")
				{
					return string.Empty;
				}
				for (int num = _path.Length - 1; num >= 0; num--)
				{
					if (_path[num] == '/')
					{
						return (num == _path.Length - 1) ? string.Empty : _path.Substring(num + 1);
					}
				}
				return _path;
			}
		}

		public string FileNameWithoutExtension
		{
			get
			{
				return Path.GetFileNameWithoutExtension(FileName);
			}
		}

		public int Depth
		{
			get
			{
				if (IsRoot)
				{
					return 0;
				}
				if (IsCurrentDir)
				{
					return 0;
				}
				int num = (IsRelative ? 1 : 0);
				for (int i = 0; i != _path.Length; i++)
				{
					if (_path[i] == '/')
					{
						num++;
					}
				}
				return num;
			}
		}

		public bool IsCurrentDir
		{
			get
			{
				return ToString() == ".";
			}
		}

		public string Extension
		{
			get
			{
				if (IsRoot)
				{
					throw new ArgumentException("A root directory does not have an extension");
				}
				for (int num = _path.Length - 1; num >= 0; num--)
				{
					char c = _path[num];
					if (c == '.' || c == '/')
					{
						return _path.Substring(num + 1);
					}
				}
				return string.Empty;
			}
		}

		public string UNCServerName
		{
			get
			{
				if (!IsUNC)
				{
					return null;
				}
				int num = _path.IndexOf('/');
				if (num < 0)
				{
					num = _path.Length;
				}
				return _path.Substring(2, num - 2);
			}
		}

		public string DriveLetter
		{
			get
			{
				return (_path.Length >= 2 && _path[1] == ':') ? _path[0].ToString() : null;
			}
		}

		public bool IsRoot
		{
			get
			{
				if (_path == "/")
				{
					return true;
				}
				if (_path.Length == 3 && _path[1] == ':' && _path[2] == '/')
				{
					return true;
				}
				if (IsUNC && _path.Length == _path.IndexOf('/') + 1)
				{
					return true;
				}
				return false;
			}
		}

		public bool IsUNC
		{
			get
			{
				return IsUNCPath(_path);
			}
		}

		public bool IsSymbolicLink
		{
			get
			{
				return FileSystem.Active.IsSymbolicLink(this);
			}
		}

		public static NPath CurrentDirectory
		{
			get
			{
				return _frozenCurrentDirectory ?? FileSystem.Active.Directory_GetCurrentDirectory();
			}
		}

		public static NPath HomeDirectory
		{
			get
			{
				if (Path.DirectorySeparatorChar == '\\')
				{
					return new NPath(Environment.GetEnvironmentVariable("USERPROFILE"));
				}
				return new NPath(Environment.GetEnvironmentVariable("HOME"));
			}
		}

		public static NPath SystemTemp
		{
			get
			{
				return new NPath(Path.GetTempPath());
			}
		}

		public IEnumerable<NPath> RecursiveParents
		{
			get
			{
				NPath candidate = this;
				while (!candidate.IsRoot && !(candidate._path == "."))
				{
					candidate = candidate.Parent;
					yield return candidate;
				}
			}
		}

		public FileAttributes Attributes
		{
			get
			{
				return FileSystem.Active.File_GetAttributes(this);
			}
			set
			{
				FileSystem.Active.File_SetAttributes(this, value);
			}
		}

		private static bool CalculateIsWindows()
		{
			return Environment.OSVersion.Platform == PlatformID.Win32Windows || Environment.OSVersion.Platform == PlatformID.Win32NT;
		}

		private static bool CalculateIsWindows10()
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				return false;
			}
			string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "kernel32.dll");
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(fileName);
			return versionInfo.ProductMajorPart >= 10;
		}

		public NPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException();
			}
			_path = MakeCompletelyWellFormatted(path);
		}

		private NPath(string path, bool guaranteed_well_formed)
		{
			if (!guaranteed_well_formed)
			{
				throw new ArgumentException("For not well formed paths, use the public NPath constructor");
			}
			_path = path;
		}

		private static bool IsUNCPath(string path)
		{
			return path.Length > 2 && path[0] == '\\' && path[1] == '\\';
		}

		private static string ConvertToForwardSlashPath(string path)
		{
			if (IsUNCPath(path))
			{
				return "\\\\" + path.Substring(2).Replace("\\", "/");
			}
			return path.Replace("\\", "/");
		}

		private static string MakeCompletelyWellFormatted(string path, bool doubleDotsAreCollapsed = false)
		{
			if (path == ".")
			{
				return ".";
			}
			if (path.Length == 0)
			{
				return ".";
			}
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			char c = '\0';
			for (int i = 0; i != path.Length; i++)
			{
				char c2 = path[i];
				char c3 = ((path.Length > i + 1) ? path[i + 1] : '\0');
				bool flag3 = c2 == '.';
				if (flag3 && i == 0)
				{
					flag2 = true;
				}
				bool flag4 = IsSlash(c2);
				flag = flag || (!flag3 && !flag4);
				if (!doubleDotsAreCollapsed && (flag || !flag2) && flag3 && c == '.')
				{
					return MakeCompletelyWellFormatted(CollapseDoubleDots(path), true);
				}
				if (flag3 && (IsSlash(c) || c == '\0') && (IsSlash(c3) || c3 == '\0'))
				{
					return MakeCompletelyWellFormatted(CollapseSingleDots(path));
				}
				if (c2 == '\\' && (!IsUNCPath(path) || i >= 2))
				{
					return MakeCompletelyWellFormatted(ConvertToForwardSlashPath(path));
				}
				if (c2 == '/' && c == '/')
				{
					return MakeCompletelyWellFormatted(CollapseDoubleSlashes(path));
				}
				if (c2 == '/')
				{
					num++;
				}
				c = c2;
			}
			char c4 = path[path.Length - 1];
			char c5 = ((path.Length >= 2) ? path[path.Length - 2] : '\0');
			if (c4 == '/')
			{
				if (c5 == '\0' || c5 == ':')
				{
					return path;
				}
				if (num == 1 && IsUNCPath(path))
				{
					return path;
				}
				return path.Substring(0, path.Length - 1);
			}
			if (num == 0 && IsUNCPath(path))
			{
				return path + "/";
			}
			return path;
		}

		private static string CollapseSingleDots(string path)
		{
			string text = ConvertToForwardSlashPath(path).Replace("/./", "/");
			if (text.StartsWith("./", StringComparison.Ordinal))
			{
				text = text.Substring(2);
			}
			if (text.EndsWith("/.", StringComparison.Ordinal))
			{
				text = text.Substring(0, text.Length - 2);
			}
			return text;
		}

		private static string CollapseDoubleSlashes(string path)
		{
			return ConvertToForwardSlashPath(path).Replace("//", "/");
		}

		private static string CollapseDoubleDots(string path)
		{
			path = ConvertToForwardSlashPath(path);
			bool flag = path[0] == '/';
			bool flag2 = path[1] == ':' && path[2] == '/';
			bool flag3 = IsUNCPath(path);
			bool flag4 = flag || flag2 || flag3;
			int num = 0;
			if (flag4)
			{
				num = 1;
			}
			if (flag2)
			{
				num = 3;
			}
			if (flag3)
			{
				num = path.IndexOf('/') + 1;
			}
			Stack<string> stack = new Stack<string>();
			int num2 = num;
			for (int i = num; i != path.Length; i++)
			{
				if (path[i] != '/' && i != path.Length - 1)
				{
					continue;
				}
				int num3 = ((i == path.Length - 1) ? 1 : 0);
				string text = path.Substring(num2, i - num2 + num3);
				if (text == "..")
				{
					if (stack.Count == 0)
					{
						if (flag4)
						{
							throw new ArgumentException("Cannot parse path because it's ..'ing beyond the root: " + path);
						}
						stack.Push(text);
					}
					else if (stack.Peek() == "..")
					{
						stack.Push(text);
					}
					else
					{
						stack.Pop();
					}
				}
				else
				{
					stack.Push(text);
				}
				num2 = i + 1;
			}
			return path.Substring(0, num) + string.Join("/", stack.Reverse().ToArray());
		}

		[MethodImpl((MethodImplOptions)256)]
		private static bool IsSlash(char c)
		{
			return c == '/' || c == '\\';
		}

		public NPath Combine(string append)
		{
			if (IsSlash(append[0]))
			{
				throw new ArgumentException("You cannot .Combine a non-relative path: " + append);
			}
			return new NPath(_path + "/" + append);
		}

		public NPath Combine(string append1, string append2)
		{
			return new NPath(_path + "/" + append1 + "/" + append2);
		}

		public NPath Combine(NPath append)
		{
			if (append == null)
			{
				throw new ArgumentNullException("append");
			}
			char c = append._path[0];
			if (IsSlash(c))
			{
				throw new ArgumentException("You cannot .Combine a non-relative path: " + append._path);
			}
			if (c == '.' || _path[0] == '.' || _path.Length == 1)
			{
				return new NPath(_path + "/" + append._path);
			}
			return new NPath(_path + "/" + (((object)append != null) ? append.ToString() : null), true);
		}

		public NPath Combine(params NPath[] append)
		{
			StringBuilder stringBuilder = new StringBuilder(ToString());
			foreach (NPath nPath in append)
			{
				if (!nPath.IsRelative)
				{
					throw new ArgumentException(string.Format("You cannot .Combine a non-relative path: {0}", nPath));
				}
				stringBuilder.Append("/");
				stringBuilder.Append(nPath);
			}
			return new NPath(stringBuilder.ToString());
		}

		public NPath RelativeTo(NPath path)
		{
			if (IsRelative || path.IsRelative)
			{
				return MakeAbsolute().RelativeTo(path.MakeAbsolute());
			}
			string path2 = _path;
			string path3 = path._path;
			if (path2 == path3)
			{
				return ".";
			}
			if (!HasSameDriveLetter(path) || !HasSameUNCServerName(path))
			{
				return this;
			}
			if (path.IsRoot)
			{
				return new NPath(path2.Substring(path3.Length));
			}
			if (path2.StartsWith(path3, PathStringComparison) && path2.Length >= path3.Length && IsSlash(path2[path3.Length]))
			{
				return new NPath(path2.Substring(Math.Min(path3.Length + 1, path2.Length)));
			}
			StringBuilder stringBuilder = new StringBuilder();
			NPath[] array = path.RecursiveParents.ToArray();
			foreach (NPath nPath in array)
			{
				stringBuilder.Append("../");
				if (IsChildOf(nPath))
				{
					stringBuilder.Append(path2.Substring(nPath.ToString().Length));
					return new NPath(stringBuilder.ToString());
				}
			}
			throw new ArgumentException();
		}

		public NPath ChangeExtension(string extension)
		{
			ThrowIfRoot();
			string text = ToString();
			int num = -1;
			for (int num2 = text.Length - 1; num2 >= 0; num2--)
			{
				if (text[num2] == '.')
				{
					num = num2;
					break;
				}
				if (text[num2] == '/')
				{
					break;
				}
			}
			string text2 = ((extension.Length == 0) ? extension : WithDot(extension));
			if (num == -1)
			{
				return text + text2;
			}
			return text.Substring(0, num) + text2;
		}

		public bool HasDirectory(string dir)
		{
			if (dir.Contains("/") || dir.Contains("\\"))
			{
				throw new ArgumentException("Directory cannot contain slash " + dir);
			}
			if (dir == ".")
			{
				throw new ArgumentException("Single dot is not an allowed argument");
			}
			if (_path.StartsWith(dir + "/", PathStringComparison))
			{
				return true;
			}
			if (_path.EndsWith("/" + dir, PathStringComparison))
			{
				return true;
			}
			return _path.Contains("/" + dir + "/");
		}

		public bool Exists(NPath append = null)
		{
			return FileExists(append) || DirectoryExists(append);
		}

		public bool DirectoryExists(NPath append = null)
		{
			NPath nPath = ((append != null) ? Combine(append) : this);
			NPathTLSCallback<NPath, StatCallback>.Invoke(nPath);
			return FileSystem.Active.Directory_Exists(nPath);
		}

		public bool FileExists(NPath append = null)
		{
			NPath nPath = ((append != null) ? Combine(append) : this);
			NPathTLSCallback<NPath, StatCallback>.Invoke(nPath);
			return FileSystem.Active.File_Exists(nPath);
		}

		private bool HasSameUNCServerName(NPath other)
		{
			return UNCServerName == other.UNCServerName;
		}

		private bool HasSameDriveLetter(NPath other)
		{
			return DriveLetter == other.DriveLetter;
		}

		public string InQuotes(SlashMode slashMode = SlashMode.Forward)
		{
			return "\"" + ToString(slashMode) + "\"";
		}

		public override string ToString()
		{
			return _path;
		}

		public string ToString(SlashMode slashMode)
		{
			if (slashMode == SlashMode.Forward || (slashMode == SlashMode.Native && !k_IsWindows))
			{
				return _path;
			}
			return _path.Replace("/", "\\");
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as NPath);
		}

		public bool Equals(NPath p)
		{
			return p != null && string.Equals(p._path, _path, PathStringComparison);
		}

		public static bool operator ==(NPath a, NPath b)
		{
			if ((NPath)a == b)
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a.Equals(b);
		}

		public override int GetHashCode()
		{
			if (k_IsCaseSensitiveFileSystem)
			{
				return _path.GetHashCode();
			}
			uint num = 27644437u;
			int i = 0;
			for (int length = _path.Length; i < length; i++)
			{
				uint num2 = _path[i];
				if (num2 > 128)
				{
					num2 = 128u;
				}
				num2 |= 0x20u;
				num ^= (num << 5) ^ num2;
			}
			return (int)num;
		}

		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return -1;
			}
			return string.Compare(_path, ((NPath)obj)._path, PathStringComparison);
		}

		public static bool operator !=(NPath a, NPath b)
		{
			return !(a == b);
		}

		public bool HasExtension(params string[] extensions)
		{
			if (extensions.Contains("*"))
			{
				return true;
			}
			if (extensions.Length == 0)
			{
				return FileName.Contains(".");
			}
			string extension = ("." + Extension).ToUpperInvariant();
			return extensions.Any((string e) => WithDot(e).ToUpperInvariant() == extension);
		}

		private static string WithDot(string extension)
		{
			return extension.StartsWith(".", StringComparison.Ordinal) ? extension : ("." + extension);
		}

		public NPath[] Files(string filter, bool recurse = false)
		{
			GlobbingCallback.Invoke(this, filter, recurse);
			return FileSystem.Active.Directory_GetFiles(_path, filter, recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
		}

		public NPath[] Files(bool recurse = false)
		{
			return Files("*", recurse);
		}

		public NPath[] Files(string[] extensions, bool recurse = false)
		{
			if (!DirectoryExists() || extensions.Length == 0)
			{
				return new NPath[0];
			}
			GlobbingCallback.Invoke(this, extensions, recurse);
			return (from p in FileSystem.Active.Directory_GetFiles(this, "*", recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
				where extensions.Contains(p.Extension)
				select p).ToArray();
		}

		public NPath[] Contents(string filter, bool recurse = false)
		{
			return Files(filter, recurse).Concat(Directories(filter, recurse)).ToArray();
		}

		public NPath[] Contents(bool recurse = false)
		{
			return Contents("*", recurse);
		}

		public NPath[] Directories(string filter, bool recurse = false)
		{
			GlobbingCallback.Invoke(this, filter, recurse);
			return FileSystem.Active.Directory_GetDirectories(this, filter, recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
		}

		public NPath[] Directories(bool recurse = false)
		{
			return Directories("*", recurse);
		}

		public NPath CreateFile()
		{
			ThrowIfRoot();
			EnsureParentDirectoryExists();
			FileSystem.Active.File_WriteAllBytes(this, new byte[0]);
			return this;
		}

		public NPath CreateFile(NPath file)
		{
			if (!file.IsRelative)
			{
				throw new ArgumentException("You cannot call CreateFile() on an existing path with a non relative argument");
			}
			return Combine(file).CreateFile();
		}

		public NPath CreateDirectory()
		{
			if (IsRoot)
			{
				throw new NotSupportedException("CreateDirectory is not supported on a root level directory because it would be dangerous:" + ToString());
			}
			FileSystem.Active.Directory_CreateDirectory(this);
			return this;
		}

		public NPath CreateDirectory(NPath directory)
		{
			if (!directory.IsRelative)
			{
				throw new ArgumentException("Cannot call CreateDirectory with an absolute argument");
			}
			return Combine(directory).CreateDirectory();
		}

		public NPath CreateSymbolicLink(NPath targetPath, bool targetIsFile = true)
		{
			ThrowIfRoot();
			if (Exists())
			{
				throw new InvalidOperationException("Cannot create symbolic link at {this} because it already exists as a file or directory.");
			}
			FileSystem.Active.CreateSymbolicLink(this, targetPath, targetIsFile);
			return this;
		}

		public NPath Copy(NPath dest)
		{
			return Copy(dest, (NPath p) => true);
		}

		public NPath Copy(NPath dest, Func<NPath, bool> fileFilter)
		{
			if (dest.DirectoryExists())
			{
				return CopyWithDeterminedDestination(dest.Combine(FileName), fileFilter);
			}
			return CopyWithDeterminedDestination(dest, fileFilter);
		}

		public NPath MakeAbsolute(NPath @base = null)
		{
			if (!IsRelative)
			{
				return this;
			}
			return (@base ?? CurrentDirectory).Combine(this);
		}

		private NPath CopyWithDeterminedDestination(NPath destination, Func<NPath, bool> fileFilter)
		{
			destination = destination.MakeAbsolute();
			if (FileExists())
			{
				if (!fileFilter(destination))
				{
					return null;
				}
				destination.EnsureParentDirectoryExists();
				FileSystem.Active.File_Copy(this, destination, true);
				return destination;
			}
			if (DirectoryExists())
			{
				destination.EnsureDirectoryExists();
				NPath[] array = Contents();
				foreach (NPath nPath in array)
				{
					nPath.CopyWithDeterminedDestination(destination.Combine(nPath.RelativeTo(this)), fileFilter);
				}
				return destination;
			}
			throw new ArgumentException("Copy() called on path that doesnt exist: " + ToString());
		}

		public void Delete(DeleteMode deleteMode = DeleteMode.Normal)
		{
			if (IsRoot)
			{
				throw new NotSupportedException("Delete is not supported on a root level directory because it would be dangerous:" + ToString());
			}
			if (FileExists())
			{
				FileSystem.Active.File_Delete(this);
				return;
			}
			if (DirectoryExists())
			{
				try
				{
					FileSystem.Active.Directory_Delete(this, true);
					return;
				}
				catch (IOException)
				{
					if (deleteMode == DeleteMode.Normal)
					{
						throw;
					}
					return;
				}
			}
			throw new InvalidOperationException("Trying to delete a path that does not exist: " + ToString());
		}

		public NPath DeleteIfExists(DeleteMode deleteMode = DeleteMode.Normal)
		{
			if (FileExists() || DirectoryExists())
			{
				Delete(deleteMode);
			}
			return this;
		}

		public NPath DeleteContents()
		{
			if (IsRoot)
			{
				throw new NotSupportedException("DeleteContents is not supported on a root level directory because it would be dangerous:" + ToString());
			}
			if (FileExists())
			{
				throw new InvalidOperationException("It is not valid to perform this operation on a file");
			}
			if (DirectoryExists())
			{
				try
				{
					Files().Delete();
					Directories().Delete();
				}
				catch (IOException)
				{
					if (Files(true).Any())
					{
						throw;
					}
				}
				return this;
			}
			return EnsureDirectoryExists();
		}

		public static NPath CreateTempDirectory(string prefix = "")
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Path.GetTempPath());
			stringBuilder.Append("/");
			if (!string.IsNullOrEmpty(prefix))
			{
				stringBuilder.Append(prefix);
				stringBuilder.Append("_");
			}
			stringBuilder.Append(Path.GetRandomFileName());
			NPath nPath = new NPath(stringBuilder.ToString());
			return nPath.CreateDirectory();
		}

		public NPath Move(NPath dest)
		{
			if (IsRoot)
			{
				throw new NotSupportedException("Move is not supported on a root level directory because it would be dangerous:" + ToString());
			}
			if (dest.DirectoryExists())
			{
				return Move(dest.Combine(FileName));
			}
			if (FileExists())
			{
				dest.EnsureParentDirectoryExists();
				FileSystem.Active.File_Move(this, dest);
				return dest;
			}
			if (DirectoryExists())
			{
				FileSystem.Active.Directory_Move(this, dest);
				return dest;
			}
			throw new ArgumentException("Move() called on a path that doesn't exist: " + ToString());
		}

		public static IDisposable SetCurrentDirectory(NPath directory)
		{
			SetCurrentDirectoryOnDispose result = new SetCurrentDirectoryOnDispose(CurrentDirectory);
			FileSystem.Active.Directory_SetCurrentDirectory(directory);
			return result;
		}

		private void ThrowIfRoot()
		{
			if (IsRoot)
			{
				throw new ArgumentException("You are attempting an operation that is not valid on a root level directory");
			}
		}

		public NPath EnsureDirectoryExists(NPath append = null)
		{
			NPath nPath = ((append != null) ? Combine(append) : this);
			if (nPath.DirectoryExists())
			{
				return nPath;
			}
			nPath.EnsureParentDirectoryExists();
			nPath.CreateDirectory();
			return nPath;
		}

		public NPath EnsureParentDirectoryExists()
		{
			Parent.EnsureDirectoryExists();
			return this;
		}

		public NPath FileMustExist()
		{
			if (!FileExists())
			{
				throw new FileNotFoundException("File was expected to exist : " + ToString());
			}
			return this;
		}

		public NPath DirectoryMustExist()
		{
			if (!DirectoryExists())
			{
				throw new DirectoryNotFoundException("Expected directory to exist : " + ToString());
			}
			return this;
		}

		public bool IsChildOf(NPath potentialBasePath)
		{
			if (IsRelative != potentialBasePath.IsRelative)
			{
				return MakeAbsolute().IsChildOf(potentialBasePath.MakeAbsolute());
			}
			if ((!IsRelative && !HasSameDriveLetter(potentialBasePath)) || !HasSameUNCServerName(potentialBasePath))
			{
				return false;
			}
			if (potentialBasePath.IsRoot)
			{
				return true;
			}
			if (IsRelative && potentialBasePath._path == ".")
			{
				return !_path.StartsWith("..", StringComparison.Ordinal);
			}
			string path = potentialBasePath._path;
			int length = path.Length;
			return _path.Length > length + 1 && _path.StartsWith(path, PathStringComparison) && _path[length] == '/';
		}

		public bool IsSameAsOrChildOf(NPath potentialBasePath)
		{
			return MakeAbsolute() == potentialBasePath.MakeAbsolute() || IsChildOf(potentialBasePath);
		}

		public NPath ParentContaining(NPath needle)
		{
			return RecursiveParents.FirstOrDefault((NPath p) => p.Exists(needle));
		}

		public NPath WriteAllText(string contents)
		{
			EnsureParentDirectoryExists();
			FileSystem.Active.File_WriteAllText(this, contents);
			return this;
		}

		public NPath ReplaceAllText(string contents)
		{
			if (FileExists() && ReadAllText() == contents)
			{
				return this;
			}
			WriteAllText(contents);
			return this;
		}

		public NPath WriteAllBytes(byte[] bytes)
		{
			EnsureParentDirectoryExists();
			FileSystem.Active.File_WriteAllBytes(this, bytes);
			return this;
		}

		public string ReadAllText()
		{
			NPathTLSCallback<NPath, ReadContentsCallback>.Invoke(this);
			return FileSystem.Active.File_ReadAllText(this);
		}

		public byte[] ReadAllBytes()
		{
			NPathTLSCallback<NPath, ReadContentsCallback>.Invoke(this);
			return FileSystem.Active.File_ReadAllBytes(this);
		}

		public NPath WriteAllLines(string[] contents)
		{
			EnsureParentDirectoryExists();
			FileSystem.Active.File_WriteAllLines(this, contents);
			return this;
		}

		public string[] ReadAllLines()
		{
			NPathTLSCallback<NPath, ReadContentsCallback>.Invoke(this);
			return FileSystem.Active.File_ReadAllLines(this);
		}

		public IEnumerable<NPath> CopyFiles(NPath destination, bool recurse, Func<NPath, bool> fileFilter = null)
		{
			destination.EnsureDirectoryExists();
			return (from file in Files(recurse).Where(fileFilter ?? new Func<NPath, bool>(AlwaysTrue))
				select file.Copy(destination.Combine(file.RelativeTo(this)))).ToArray();
		}

		public IEnumerable<NPath> MoveFiles(NPath destination, bool recurse, Func<NPath, bool> fileFilter = null)
		{
			if (IsRoot)
			{
				throw new NotSupportedException("MoveFiles is not supported on this directory because it would be dangerous:" + ToString());
			}
			destination.EnsureDirectoryExists();
			return (from file in Files(recurse).Where(fileFilter ?? new Func<NPath, bool>(AlwaysTrue))
				select file.Move(destination.Combine(file.RelativeTo(this)))).ToArray();
		}

		private static bool AlwaysTrue(NPath p)
		{
			return true;
		}

		public static implicit operator NPath(string input)
		{
			return (input != null) ? new NPath(input) : null;
		}

		public NPath SetLastWriteTimeUtc(DateTime lastWriteTimeUtc)
		{
			FileSystem.Active.File_SetLastWriteTimeUtc(this, lastWriteTimeUtc);
			return this;
		}

		public DateTime GetLastWriteTimeUtc()
		{
			return FileSystem.Active.File_GetLastWriteTimeUtc(this);
		}

		public long GetFileSize()
		{
			return FileSystem.Active.File_GetSize(this);
		}

		public static IDisposable WithGlobbingCallback(Action<GlobRequest> callback)
		{
			return new GlobbingCallback(callback);
		}

		public static IDisposable WithReadContentsCallback(Action<NPath> callback)
		{
			return new ReadContentsCallback(callback);
		}

		public static IDisposable WithStatCallback(Action<NPath> callback)
		{
			return new StatCallback(callback);
		}

		public static IDisposable WithFileSystem(FileSystem fileSystem)
		{
			return new WithFileSystemHelper(fileSystem);
		}

		public NPath ResolveWithFileSystem()
		{
			return FileSystem.Active.Resolve(this);
		}

		public string InQuotesResolved(SlashMode slashMode = SlashMode.Forward)
		{
			return ResolveWithFileSystem().InQuotes(slashMode);
		}

		public static IDisposable WithFrozenCurrentDirectory(NPath frozenCurrentDirectory)
		{
			if (_frozenCurrentDirectory != null)
			{
				throw new InvalidOperationException(string.Format("{0} called, while there was already a frozen current directory set: {1}", "WithFrozenCurrentDirectory", _frozenCurrentDirectory));
			}
			_frozenCurrentDirectory = frozenCurrentDirectory;
			return new WithFrozenDirectoryHelper();
		}
	}
}
