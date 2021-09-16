using System.Runtime.InteropServices;

namespace DN_UNPSA_TOOLKIT
{
	public struct AnimInfoBinary
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string Name;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string Group;

		public int TotalBones;

		public int RootInclude;

		public int KeyCompressionStyle;

		public int KeyQuotum;

		public float KeyReduction;

		public float TrackTime;

		public float AnimRate;

		public int StartBone;

		public int FirstRawFrame;

		public int NumRawFrames;
	}
}
