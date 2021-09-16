using System.Runtime.InteropServices;

namespace DN_UNPSA_TOOLKIT
{
	public struct VMaterial
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string MaterialName;

		public int TextureIndex;

		public uint PolyFlags;

		public int AuxMaterial;

		public uint AuxFlags;

		public int LodBias;

		public int LodStyle;
	}
}
