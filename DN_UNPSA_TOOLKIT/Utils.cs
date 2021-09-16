using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace DN_UNPSA_TOOLKIT
{
	internal class Utils
	{
		public static string FilenameFromString(string name)
		{
			string text = name.Trim();
			text = text.Replace(" ", "-");
			if (text.IndexOf("--") > -1)
			{
				while (text.IndexOf("--") > -1)
				{
					text = text.Replace("--", "-");
				}
			}
			text = Regex.Replace(text, "[^a-zA-Z0-9\\-]", "");
			if (text.Length > 50)
			{
				text = text.Substring(0, 49);
			}
			char[] trimChars = new char[2] { '-', '.' };
			text = text.TrimStart(trimChars);
			return text.TrimEnd(trimChars);
		}

		public static byte[] RawSerialize(object anything)
		{
			int num = Marshal.SizeOf(anything);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr(anything, intPtr, fDeleteOld: false);
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			Marshal.FreeHGlobal(intPtr);
			return array;
		}

		public static object RawDeserialize(byte[] rawdatas, Type anytype)
		{
			int num = Marshal.SizeOf(anytype);
			if (num > rawdatas.Length)
			{
				return null;
			}
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.Copy(rawdatas, 0, intPtr, num);
			object result = Marshal.PtrToStructure(intPtr, anytype);
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		public static object getRawData(BinaryReader br, Type type)
		{
			new object();
			int num = Marshal.SizeOf(type);
			_ = new byte[num];
			return RawDeserialize(br.ReadBytes(num), type);
		}
	}
}
