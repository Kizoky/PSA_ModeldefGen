using System.Runtime.InteropServices;

namespace DN_UNPSA_TOOLKIT
{
	public struct FNamedBoneBinary
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string Name;

		public uint Flags;

		public int NumChildren;

		public int ParentIndex;

		public VJointPos BonePos;
	}
}
