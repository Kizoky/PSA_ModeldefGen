using System.Runtime.InteropServices;

namespace DN_UNPSA_TOOLKIT
{
	public struct VChunkHeader
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
		public string ChunkID;

		public int TypeFlag;

		public int DataSize;

		public int DataCount;
	}
}
