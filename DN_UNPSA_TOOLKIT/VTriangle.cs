using System.Runtime.InteropServices;

namespace DN_UNPSA_TOOLKIT
{
	public struct VTriangle
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U2)]
		public ushort[] WedgeIndex;

		public byte MatIndex;

		public byte AuxMatIndex;

		public uint SmoothingGroups;
	}
}
