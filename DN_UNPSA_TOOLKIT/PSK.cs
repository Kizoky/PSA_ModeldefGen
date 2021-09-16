using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DN_UNPSA_TOOLKIT
{
	public class PSK
	{
		public VChunkHeader fileHeader;

		public VChunkHeader pointsHeader;

		public List<VPoint> pointsData;

		public VChunkHeader wedgesHeader;

		public List<VVertex> wedgesData;

		public VChunkHeader facesHeader;

		public List<VTriangle> facesData;

		public VChunkHeader materialsHeader;

		public List<VMaterial> materialsData;

		public VChunkHeader bonesHeader;

		public List<FNamedBoneBinary> bonesData;

		public VChunkHeader influencesHeader;

		public List<VRawBoneInfluence> influencesData;

		public PSKFileInfo fileInfo;

		public void load(string filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			this.fileInfo = default(PSKFileInfo);
			this.fileInfo.FullPath = filePath;
			this.fileInfo.FileName = fileInfo.Name;
			this.fileInfo.FilePath = fileInfo.DirectoryName;
			this.fileInfo.FileSize = fileInfo.Length;
			FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			fileHeader = (VChunkHeader)Utils.getRawData(binaryReader, default(VChunkHeader).GetType());
			this.fileInfo.FileVersion = fileHeader.TypeFlag;
			pointsHeader = (VChunkHeader)Utils.getRawData(binaryReader, default(VChunkHeader).GetType());
			int dataCount = pointsHeader.DataCount;
			this.fileInfo.Points = dataCount;
			pointsData = new List<VPoint>(dataCount);
			for (int i = 0; i < dataCount; i++)
			{
				pointsData.Add((VPoint)Utils.getRawData(binaryReader, default(VPoint).GetType()));
			}
			wedgesHeader = (VChunkHeader)Utils.getRawData(binaryReader, default(VChunkHeader).GetType());
			dataCount = wedgesHeader.DataCount;
			this.fileInfo.Wedges = dataCount;
			wedgesData = new List<VVertex>(dataCount);
			for (int j = 0; j < dataCount; j++)
			{
				wedgesData.Add((VVertex)Utils.getRawData(binaryReader, default(VVertex).GetType()));
			}
			facesHeader = (VChunkHeader)Utils.getRawData(binaryReader, default(VChunkHeader).GetType());
			dataCount = facesHeader.DataCount;
			this.fileInfo.Faces = dataCount;
			facesData = new List<VTriangle>(dataCount);
			for (int k = 0; k < dataCount; k++)
			{
				facesData.Add((VTriangle)Utils.getRawData(binaryReader, default(VTriangle).GetType()));
			}
			materialsHeader = (VChunkHeader)Utils.getRawData(binaryReader, default(VChunkHeader).GetType());
			dataCount = materialsHeader.DataCount;
			this.fileInfo.Materials = dataCount;
			this.fileInfo.MaterialNames = new string[dataCount];
			materialsData = new List<VMaterial>(dataCount);
			for (int l = 0; l < dataCount; l++)
			{
				materialsData.Add((VMaterial)Utils.getRawData(binaryReader, default(VMaterial).GetType()));
				this.fileInfo.MaterialNames[l] = materialsData.ElementAt(l).MaterialName;
			}
			bonesHeader = (VChunkHeader)Utils.getRawData(binaryReader, default(VChunkHeader).GetType());
			dataCount = bonesHeader.DataCount;
			this.fileInfo.Bones = dataCount;
			this.fileInfo.BoneNames = new string[dataCount];
			bonesData = new List<FNamedBoneBinary>(dataCount);
			for (int m = 0; m < dataCount; m++)
			{
				bonesData.Add((FNamedBoneBinary)Utils.getRawData(binaryReader, default(FNamedBoneBinary).GetType()));
				this.fileInfo.BoneNames[m] = bonesData.ElementAt(m).Name;
			}
			influencesHeader = (VChunkHeader)Utils.getRawData(binaryReader, default(VChunkHeader).GetType());
			dataCount = influencesHeader.DataCount;
			this.fileInfo.Influences = dataCount;
			influencesData = new List<VRawBoneInfluence>(dataCount);
			for (int n = 0; n < dataCount; n++)
			{
				influencesData.Add((VRawBoneInfluence)Utils.getRawData(binaryReader, default(VRawBoneInfluence).GetType()));
			}
			binaryReader.Close();
			fileStream.Close();
		}

		public void save()
		{
			FileStream fileStream = new FileStream("test.psk", FileMode.Create, FileAccess.Write);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(Utils.RawSerialize(fileHeader));
			binaryWriter.Write(Utils.RawSerialize(pointsHeader));
			int dataCount = pointsHeader.DataCount;
			for (int i = 0; i < dataCount; i++)
			{
				binaryWriter.Write(Utils.RawSerialize(pointsData.ElementAt(i)));
			}
			binaryWriter.Write(Utils.RawSerialize(wedgesHeader));
			dataCount = wedgesHeader.DataCount;
			for (int j = 0; j < dataCount; j++)
			{
				binaryWriter.Write(Utils.RawSerialize(wedgesData.ElementAt(j)));
			}
			binaryWriter.Write(Utils.RawSerialize(facesHeader));
			dataCount = facesHeader.DataCount;
			for (int k = 0; k < dataCount; k++)
			{
				binaryWriter.Write(Utils.RawSerialize(facesData.ElementAt(k)));
			}
			binaryWriter.Write(Utils.RawSerialize(materialsHeader));
			dataCount = materialsHeader.DataCount;
			Console.WriteLine(dataCount);
			for (int l = 0; l < dataCount; l++)
			{
				binaryWriter.Write(Utils.RawSerialize(materialsData.ElementAt(l)));
			}
			binaryWriter.Write(Utils.RawSerialize(bonesHeader));
			dataCount = bonesHeader.DataCount;
			for (int m = 0; m < dataCount; m++)
			{
				binaryWriter.Write(Utils.RawSerialize(bonesData.ElementAt(m)));
			}
			binaryWriter.Write(Utils.RawSerialize(influencesHeader));
			dataCount = influencesHeader.DataCount;
			for (int n = 0; n < dataCount; n++)
			{
				binaryWriter.Write(Utils.RawSerialize(influencesData.ElementAt(n)));
			}
			binaryWriter.Close();
			fileStream.Close();
		}
	}
}
