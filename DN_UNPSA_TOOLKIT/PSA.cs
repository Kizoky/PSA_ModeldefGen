using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DN_UNPSA_TOOLKIT
{
	public class PSA
	{
		public VChunkHeader fileHeader;

		public VChunkHeader bonesHeader;

		public List<FNamedBoneBinary> bonesData;

		public VChunkHeader animHeader;

		public List<AnimInfoBinary> animData;

		public VChunkHeader keysHeader;

		public List<VQuatAnimKey> keysData;

		public PSAFileInfo fileInfo;

		public void load(string filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			this.fileInfo = default(PSAFileInfo);
			this.fileInfo.FullPath = filePath;
			this.fileInfo.FileName = fileInfo.Name;
			this.fileInfo.FilePath = fileInfo.DirectoryName;
			this.fileInfo.FileSize = fileInfo.Length;
			FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			fileHeader = (VChunkHeader)Utils.getRawData(binaryReader, default(VChunkHeader).GetType());
			this.fileInfo.FileVersion = fileHeader.TypeFlag;
			bonesHeader = (VChunkHeader)Utils.getRawData(binaryReader, default(VChunkHeader).GetType());
			int dataCount = bonesHeader.DataCount;
			this.fileInfo.Bones = dataCount;
			this.fileInfo.BoneNames = new string[dataCount];
			bonesData = new List<FNamedBoneBinary>(dataCount);
			for (int i = 0; i < dataCount; i++)
			{
				bonesData.Add((FNamedBoneBinary)Utils.getRawData(binaryReader, default(FNamedBoneBinary).GetType()));
				this.fileInfo.BoneNames[i] = bonesData.ElementAt(i).Name;
			}
			animHeader = (VChunkHeader)Utils.getRawData(binaryReader, default(VChunkHeader).GetType());
			dataCount = animHeader.DataCount;
			this.fileInfo.Animations = dataCount;
			this.fileInfo.AnimationNames = new string[dataCount];
			animData = new List<AnimInfoBinary>(dataCount);
			for (int j = 0; j < dataCount; j++)
			{
				animData.Add((AnimInfoBinary)Utils.getRawData(binaryReader, default(AnimInfoBinary).GetType()));
				this.fileInfo.AnimationNames[j] = animData.ElementAt(j).Name;
			}
			keysHeader = (VChunkHeader)Utils.getRawData(binaryReader, default(VChunkHeader).GetType());
			dataCount = keysHeader.DataCount;
			this.fileInfo.Keys = dataCount;
			keysData = new List<VQuatAnimKey>(dataCount);
			for (int k = 0; k < dataCount; k++)
			{
				keysData.Add((VQuatAnimKey)Utils.getRawData(binaryReader, default(VQuatAnimKey).GetType()));
			}
			binaryReader.Close();
			fileStream.Close();
		}

		public void saveMultiple(string folderPath)
		{
			int dataCount = animHeader.DataCount;
			for (int i = 0; i < dataCount; i++)
			{
				AnimInfoBinary anim = animData.ElementAt(i);
				FileStream fileStream = new FileStream(folderPath + "\\" + anim.Name + ".psa", FileMode.Create, FileAccess.Write);
				BinaryWriter binaryWriter = new BinaryWriter(fileStream);
				binaryWriter.Write(Utils.RawSerialize(fileHeader));
				binaryWriter.Write(Utils.RawSerialize(bonesHeader));
				int dataCount2 = bonesHeader.DataCount;
				for (int j = 0; j < dataCount2; j++)
				{
					binaryWriter.Write(Utils.RawSerialize(bonesData.ElementAt(j)));
				}
				VChunkHeader vChunkHeader = copyHeader(animHeader);
				vChunkHeader.DataCount = 1;
				binaryWriter.Write(Utils.RawSerialize(vChunkHeader));
				AnimInfoBinary animInfoBinary = copyAnimationData(anim);
				animInfoBinary.FirstRawFrame = 0;
				binaryWriter.Write(Utils.RawSerialize(animInfoBinary));
				VChunkHeader vChunkHeader2 = copyHeader(keysHeader);
				vChunkHeader2.DataCount = animInfoBinary.TotalBones * animInfoBinary.NumRawFrames;
				binaryWriter.Write(Utils.RawSerialize(vChunkHeader2));
				int num = anim.FirstRawFrame * animInfoBinary.TotalBones;
				int num2 = (anim.FirstRawFrame + anim.NumRawFrames) * animInfoBinary.TotalBones;
				for (int k = num; k < num2; k++)
				{
					binaryWriter.Write(Utils.RawSerialize(keysData.ElementAt(k)));
				}
				binaryWriter.Close();
				fileStream.Close();
			}
		}

		private VChunkHeader copyHeader(VChunkHeader header)
		{
			VChunkHeader result = default(VChunkHeader);
			result.ChunkID = header.ChunkID;
			result.TypeFlag = header.TypeFlag;
			result.DataSize = header.DataSize;
			result.DataCount = header.DataCount;
			return result;
		}

		private AnimInfoBinary copyAnimationData(AnimInfoBinary anim)
		{
			AnimInfoBinary result = default(AnimInfoBinary);
			result.Name = anim.Name;
			result.Group = anim.Group;
			result.TotalBones = anim.TotalBones;
			result.RootInclude = anim.RootInclude;
			result.KeyCompressionStyle = anim.KeyCompressionStyle;
			result.KeyQuotum = anim.KeyQuotum;
			result.KeyReduction = anim.KeyReduction;
			result.TrackTime = anim.TrackTime;
			result.AnimRate = anim.AnimRate;
			result.StartBone = anim.StartBone;
			result.FirstRawFrame = anim.FirstRawFrame;
			result.NumRawFrames = anim.NumRawFrames;
			return result;
		}
	}
}
