using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace DN_UNPSA_TOOLKIT
{
	public class MainForm : Form
	{
		private string SelectedPSAFile;

		private Dictionary<string, PSA> psaFilesDictionary = new Dictionary<string, PSA>();

		private PSK pskFile;

		private BoneTreeForm boneTreeForm;

		private FullAnimationsInfoForm fullAnimationInfoForm;

		private IContainer components;

		private Button openPSAFilesButton;

		private DataGridView dgvPSAFiles;

		private DataGridView dgvMotions;

		private DataGridView dgvOutputFile;

		private Button saveMultipleButton;

		private Button addMotionButton;

		private Button saveOutputButton;

		private DataGridView dgvPSKFile;

		private Button openPSKFileButton;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn Column7;

		private DataGridViewTextBoxColumn Column8;

		private DataGridViewTextBoxColumn Column9;

		private DataGridViewTextBoxColumn Column10;

		private DataGridViewTextBoxColumn Column11;

		private DataGridViewTextBoxColumn Column12;

		private DataGridViewTextBoxColumn Column1;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private Button boneTreeButton;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column6;

		private Button button1;

		private Button button2;

		private DataGridView dgvPSKBones;

		private Button button3;

		private Button button5;

		private Button button4;

		private DataGridViewCheckBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn Column15;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		private Label label1;

		private Button fullAnimationsInfoButton;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn Column17;

		private DataGridViewTextBoxColumn Column23;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn Column18;

		private DataGridViewTextBoxColumn Column19;

		private DataGridViewTextBoxColumn Column22;

		private DataGridViewTextBoxColumn Column13;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn Column14;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column16;

		private Button button6;

		private TextBox textBox1;

		private CheckBox checkBox1;

		private DataGridViewTextBoxColumn Column4;

		public MainForm()
		{
			InitializeComponent();
		}

		private void openPSKFileButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "PSK File|*.psk";
			openFileDialog.Title = "Select a Unreal PSK File";
			openFileDialog.Multiselect = false;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				pskFile = null;
				pskFile = new PSK();
				pskFile.load(openFileDialog.FileName);
				PopulateModelGrid(pskFile);
				updatePSKBonesList();
				updateBonesTreePSK();
				BonesTreePSKPSACheck();
			}
		}

		private void PopulateModelGrid(PSK pskFile)
		{
			dgvPSKFile.Rows.Clear();
			PSKFileInfo fileInfo = pskFile.fileInfo;
			dgvPSKFile.Rows.Add(fileInfo.FileName, Math.Round((float)fileInfo.FileSize / 1024f, 1), fileInfo.Points, fileInfo.Wedges, fileInfo.Faces, fileInfo.Materials, fileInfo.Bones, fileInfo.Influences);
		}

		private void openPSAFilesButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "PSA File|*.psa";
			openFileDialog.Title = "Open Unreal PSA Files";
			openFileDialog.Multiselect = true;
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			string[] fileNames = openFileDialog.FileNames;
			int num = fileNames.Length;
			for (int i = 0; i < num; i++)
			{
				string fileName = Path.GetFileName(fileNames[i]);
				if (!psaFilesDictionary.ContainsKey(fileName))
				{
					PSA pSA = new PSA();
					pSA.load(fileNames[i]);
					PSAFileInfo fileInfo = pSA.fileInfo;
					psaFilesDictionary.Add(fileInfo.FileName, pSA);
					dgvPSAFiles.Rows.Add(fileInfo.FileName, fileInfo.Animations, fileInfo.Bones);
				}
				else
				{
					MessageBox.Show("File " + fileName + " already open.");
				}
			}
			button6.Enabled = true;
		}

		private void PopulateMotionGrid(PSA psaFile)
		{
			dgvMotions.Rows.Clear();
			int dataCount = psaFile.animHeader.DataCount;
			for (int i = 0; i < dataCount; i++)
			{
				dgvMotions.Rows.Add(i + 1, psaFile.animData[i].Name, psaFile.animData[i].Group, psaFile.animData[i].TrackTime, psaFile.animData[i].AnimRate, psaFile.animData[i].FirstRawFrame, psaFile.animData[i].NumRawFrames);
			}
		}

		private void PopulateNewMotionGrid(PSA psaFile, int motionIndex)
		{
			dgvOutputFile.Rows.Add(SelectedPSAFile, motionIndex + 1, psaFile.animData[motionIndex].Name, psaFile.animData[motionIndex].Group, psaFile.animData[motionIndex].TotalBones, psaFile.animData[motionIndex].TrackTime, psaFile.animData[motionIndex].AnimRate, psaFile.animData[motionIndex].NumRawFrames);
		}

		public void StatusStripOutput(string ms)
		{
			toolStripStatusLabel1.Text = ms;
		}

		private void dgvPSAFiles_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				SelectedPSAFile = dgvPSAFiles.CurrentRow.Cells[0].Value.ToString();
				PSA psaFile = psaFilesDictionary[SelectedPSAFile];
				PopulateMotionGrid(psaFile);
				PopulateFullInfo();
				updateBonesTreePSA();
				BonesTreePSKPSACheck();
			}
			catch (Exception)
			{
				dgvMotions.Rows.Clear();
				updateBonesTreePSA();
				BonesTreePSKPSACheck();
			}
		}

		private void dgvPSAFiles_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			try
			{
				string fileName = psaFilesDictionary[SelectedPSAFile].fileInfo.FileName;
				psaFilesDictionary.Remove(SelectedPSAFile);
				for (int num = dgvOutputFile.Rows.Count - 1; num > -1; num--)
				{
					DataGridViewRow dataGridViewRow = dgvOutputFile.Rows[num];
					if (dataGridViewRow.Cells[0].Value.ToString() == fileName)
					{
						dgvOutputFile.Rows.Remove(dataGridViewRow);
					}
				}
				PopulateFullInfo();
			}
			catch (ArgumentNullException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void PopulateFullInfo()
		{
			if (fullAnimationInfoForm == null)
			{
				return;
			}
			fullAnimationInfoForm.dgvFullInfo.Rows.Clear();
			fullAnimationInfoForm.Text = "Full Animations Info";
			if (dgvPSAFiles.Rows.Count >= 1)
			{
				PSA pSA = psaFilesDictionary[SelectedPSAFile];
				fullAnimationInfoForm.Text = SelectedPSAFile;
				int dataCount = pSA.animHeader.DataCount;
				for (int i = 0; i < dataCount; i++)
				{
					fullAnimationInfoForm.dgvFullInfo.Rows.Add(i + 1, pSA.animData[i].Name, pSA.animData[i].Group, pSA.animData[i].TotalBones, pSA.animData[i].RootInclude, pSA.animData[i].KeyCompressionStyle, pSA.animData[i].KeyQuotum, pSA.animData[i].KeyReduction, pSA.animData[i].TrackTime, pSA.animData[i].AnimRate, pSA.animData[i].StartBone, pSA.animData[i].FirstRawFrame, pSA.animData[i].NumRawFrames);
				}
			}
		}

		private void fullAnimationsInfoButton_Click(object sender, EventArgs e)
		{
			if (fullAnimationInfoForm == null)
			{
				fullAnimationInfoForm = new FullAnimationsInfoForm();
				fullAnimationInfoForm.FormClosed += FullAnimationsInfoForm_FormClosed;
				fullAnimationInfoForm.Show();
				PopulateFullInfo();
			}
			else
			{
				fullAnimationInfoForm.Dispose();
				fullAnimationInfoForm = null;
			}
		}

		private void FullAnimationsInfoForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			fullAnimationInfoForm = null;
		}

		private void boneTreeButton_Click(object sender, EventArgs e)
		{
			if (boneTreeForm == null)
			{
				boneTreeForm = new BoneTreeForm();
				boneTreeForm.FormClosed += BoneTreeForm_FormClosed;
				boneTreeForm.Show();
				updateBonesTreePSK();
				updateBonesTreePSA();
				BonesTreePSKPSACheck();
			}
			else
			{
				boneTreeForm.Dispose();
				boneTreeForm = null;
			}
		}

		private void BoneTreeForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			boneTreeForm = null;
		}

		private void updateBonesTreePSK()
		{
			if (boneTreeForm != null)
			{
				boneTreeForm.tvPSK.Nodes.Clear();
				if (pskFile != null)
				{
					boneTreeForm.pskFileName.Text = pskFile.fileInfo.FileName + " (" + pskFile.bonesHeader.DataCount + " bones)";
					boneTreeForm.tvPSK.Nodes.Add(bonesTree(pskFile.bonesHeader, pskFile.bonesData));
					boneTreeForm.tvPSK.ExpandAll();
					boneTreeForm.tvPSK.SelectedNode = boneTreeForm.tvPSK.Nodes[0];
				}
				else
				{
					boneTreeForm.pskFileName.Text = "No PSK File";
				}
			}
		}

		private void updateBonesTreePSA()
		{
			if (boneTreeForm != null)
			{
				boneTreeForm.tvPSA.Nodes.Clear();
				if (psaFilesDictionary.Count > 0)
				{
					PSA pSA = psaFilesDictionary[SelectedPSAFile];
					boneTreeForm.psaFileName.Text = pSA.fileInfo.FileName + " (" + pSA.bonesHeader.DataCount + " bones)";
					boneTreeForm.tvPSA.Nodes.Add(bonesTree(pSA.bonesHeader, pSA.bonesData));
					boneTreeForm.tvPSA.ExpandAll();
					boneTreeForm.tvPSA.SelectedNode = boneTreeForm.tvPSA.Nodes[0];
				}
				else
				{
					boneTreeForm.psaFileName.Text = "No PSA File";
				}
			}
		}

		private void BonesTreePSKPSACheck()
		{
			if (boneTreeForm == null)
			{
				return;
			}
			bool flag = true;
			if (pskFile != null)
			{
				ClearColor(boneTreeForm.tvPSK.Nodes);
			}
			else
			{
				flag = false;
			}
			if (psaFilesDictionary.Count > 0)
			{
				ClearColor(boneTreeForm.tvPSA.Nodes);
			}
			else
			{
				flag = false;
			}
			if (!flag)
			{
				return;
			}
			PSA pSA = psaFilesDictionary[SelectedPSAFile];
			int dataCount = pskFile.bonesHeader.DataCount;
			for (int i = 0; i < dataCount; i++)
			{
				if (boneTreeForm.tvPSA.Nodes.Find(pskFile.bonesData[i].Name, searchAllChildren: true).Length == 0)
				{
					TreeNode[] array = boneTreeForm.tvPSK.Nodes.Find(pskFile.bonesData[i].Name, searchAllChildren: true);
					for (int j = 0; j < array.Length; j++)
					{
						array[j].BackColor = Color.Red;
					}
				}
			}
			dataCount = pSA.bonesHeader.DataCount;
			for (int k = 0; k < dataCount; k++)
			{
				if (boneTreeForm.tvPSK.Nodes.Find(pSA.bonesData[k].Name, searchAllChildren: true).Length == 0)
				{
					TreeNode[] array = boneTreeForm.tvPSA.Nodes.Find(pSA.bonesData[k].Name, searchAllChildren: true);
					for (int j = 0; j < array.Length; j++)
					{
						array[j].BackColor = Color.Red;
					}
				}
			}
		}

		private void OrderOutputRows(bool upOrDown)
		{
			DataGridViewSelectedRowCollection selectedRows = dgvOutputFile.SelectedRows;
			if (selectedRows.Count < 1)
			{
				return;
			}
			int index = selectedRows[0].Index;
			if (upOrDown)
			{
				if (index > 0)
				{
					dgvOutputFile.Rows.RemoveAt(index);
					dgvOutputFile.Rows.Insert(index - 1, selectedRows[0]);
					dgvOutputFile.Rows[index - 1].Selected = true;
				}
			}
			else if (index < dgvOutputFile.Rows.Count - 1)
			{
				dgvOutputFile.Rows.RemoveAt(index);
				dgvOutputFile.Rows.Insert(index + 1, selectedRows[0]);
				dgvOutputFile.Rows[index + 1].Selected = true;
			}
		}

		private void ClearColor(TreeNodeCollection nodes)
		{
			nodes[0].BackColor = Color.White;
			foreach (TreeNode node in nodes)
			{
				ClearRecursive(node);
			}
		}

		private void ClearRecursive(TreeNode treeNode)
		{
			foreach (TreeNode node in treeNode.Nodes)
			{
				node.BackColor = Color.White;
				ClearRecursive(node);
			}
		}

		private TreeNode bonesTree(VChunkHeader bonesHeader, List<FNamedBoneBinary> bonesData)
		{
			List<TreeNode> list = new List<TreeNode>();
			int dataCount = bonesHeader.DataCount;
			for (int i = 0; i < dataCount; i++)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Name = bonesData[i].Name;
				treeNode.Text = bonesData[i].Name;
				list.Add(treeNode);
				if (i > 0)
				{
					list[bonesData[i].ParentIndex].Nodes.Add(treeNode);
				}
			}
			return list[0];
		}

		private void addMotionButton_Click(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection selectedRows = dgvMotions.SelectedRows;
			if (selectedRows.Count >= 1)
			{
				int count = selectedRows.Count;
				PSA psaFile = psaFilesDictionary[SelectedPSAFile];
				for (int num = count - 1; num >= 0; num--)
				{
					PopulateNewMotionGrid(psaFile, (int)selectedRows[num].Cells[0].Value - 1);
				}
			}
		}

		private void saveMultipleButton_Click(object sender, EventArgs e)
		{
			if (dgvMotions.Rows.Count < 1)
			{
				MessageBox.Show("No animations to export.");
				return;
			}
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = "Select a Folder";
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				psaFilesDictionary[SelectedPSAFile].saveMultiple(folderBrowserDialog.SelectedPath);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			OrderOutputRows(upOrDown: true);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			OrderOutputRows(upOrDown: false);
		}

		private void updatePSKBonesList()
		{
			dgvPSKBones.Rows.Clear();
			for (int i = 0; i < pskFile.bonesData.Count; i++)
			{
				dgvPSKBones.Rows.Add(true, i + 1, pskFile.bonesData[i].Name);
			}
		}

		private void saveOutputButton_Click(object sender, EventArgs e)
		{
			if (pskFile == null)
			{
				MessageBox.Show("No PSK file opened");
				return;
			}
			if (dgvOutputFile.Rows.Count < 1)
			{
				MessageBox.Show("No animations to export.");
				return;
			}
			bool flag = false;
			foreach (DataGridViewRow item2 in (IEnumerable)dgvPSKBones.Rows)
			{
				if ((bool)item2.Cells[0].Value)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				MessageBox.Show("Any bone selected.");
				return;
			}
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.OverwritePrompt = true;
			saveFileDialog.AddExtension = true;
			saveFileDialog.Filter = "PSA File|*.psa";
			saveFileDialog.Title = "Save Unreal PSA File";
			NumberFormatInfo provider = new NumberFormatInfo
			{
				NumberGroupSeparator = "."
			};
			if (saveFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			string fileName = saveFileDialog.FileName;
			List<FNamedBoneBinary> list = new List<FNamedBoneBinary>();
			foreach (FNamedBoneBinary bonesDatum in pskFile.bonesData)
			{
				foreach (DataGridViewRow item3 in (IEnumerable)dgvPSKBones.Rows)
				{
					if (bonesDatum.Name == item3.Cells[2].Value.ToString())
					{
						if ((bool)item3.Cells[0].Value)
						{
							list.Add(bonesDatum);
						}
						break;
					}
				}
			}
			FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			VChunkHeader vChunkHeader = default(VChunkHeader);
			vChunkHeader.ChunkID = "ANIMHEAD";
			vChunkHeader.TypeFlag = 1999801;
			vChunkHeader.DataSize = 0;
			vChunkHeader.DataCount = 0;
			binaryWriter.Write(Utils.RawSerialize(vChunkHeader));
			VChunkHeader vChunkHeader2 = default(VChunkHeader);
			vChunkHeader2.ChunkID = "BONENAMES";
			vChunkHeader2.TypeFlag = 1999801;
			vChunkHeader2.DataSize = 120;
			vChunkHeader2.DataCount = list.Count;
			binaryWriter.Write(Utils.RawSerialize(vChunkHeader2));
			for (int i = 0; i < list.Count; i++)
			{
				binaryWriter.Write(Utils.RawSerialize(list[i]));
			}
			VChunkHeader vChunkHeader3 = default(VChunkHeader);
			vChunkHeader3.ChunkID = "ANIMINFO";
			vChunkHeader3.TypeFlag = 1999801;
			vChunkHeader3.DataSize = 168;
			vChunkHeader3.DataCount = dgvOutputFile.Rows.Count;
			binaryWriter.Write(Utils.RawSerialize(vChunkHeader3));
			int num = 0;
			List<VQuatAnimKey> list2 = new List<VQuatAnimKey>();
			foreach (DataGridViewRow item4 in (IEnumerable)dgvOutputFile.Rows)
			{
				PSA pSA = psaFilesDictionary[item4.Cells[0].Value.ToString()];
				AnimInfoBinary animInfoBinary = pSA.animData[(int)item4.Cells[1].Value - 1];
				AnimInfoBinary animInfoBinary2 = default(AnimInfoBinary);
				float animRate = float.Parse(item4.Cells[6].Value.ToString(), provider);
				float trackTime = float.Parse(item4.Cells[5].Value.ToString(), provider);
				animInfoBinary2.Name = item4.Cells[2].Value.ToString();
				animInfoBinary2.Group = item4.Cells[3].Value.ToString();
				animInfoBinary2.TotalBones = list.Count;
				animInfoBinary2.RootInclude = animInfoBinary.RootInclude;
				animInfoBinary2.KeyCompressionStyle = animInfoBinary.KeyCompressionStyle;
				animInfoBinary2.KeyQuotum = animInfoBinary.KeyQuotum;
				animInfoBinary2.KeyReduction = animInfoBinary.KeyReduction;
				animInfoBinary2.TrackTime = trackTime;
				animInfoBinary2.AnimRate = animRate;
				animInfoBinary2.StartBone = animInfoBinary.StartBone;
				animInfoBinary2.FirstRawFrame = num;
				animInfoBinary2.NumRawFrames = animInfoBinary.NumRawFrames;
				int[] array = new int[list.Count];
				for (int j = 0; j < list.Count; j++)
				{
					for (int k = 0; k < pSA.bonesData.Count; k++)
					{
						if (list[j].Name == pSA.bonesData[k].Name)
						{
							array[j] = k;
							break;
						}
						array[j] = -1;
					}
				}
				int num2 = animInfoBinary.FirstRawFrame + animInfoBinary.NumRawFrames;
				for (int l = animInfoBinary.FirstRawFrame; l < num2; l++)
				{
					for (int m = 0; m < list.Count; m++)
					{
						VQuatAnimKey item;
						if (array[m] > -1)
						{
							int index = l * animInfoBinary.TotalBones + array[m];
							item = pSA.keysData[index];
						}
						else
						{
							item = default(VQuatAnimKey);
							item.Orientation.W = 0f;
							item.Orientation.X = 0f;
							item.Orientation.Y = 0f;
							item.Orientation.Z = 0f;
							item.Position.X = 0f;
							item.Position.Y = 0f;
							item.Position.Z = 0f;
							item.Time = 0f;
						}
						list2.Add(item);
					}
				}
				num += animInfoBinary.NumRawFrames;
				binaryWriter.Write(Utils.RawSerialize(animInfoBinary2));
			}
			int count = list2.Count;
			VChunkHeader vChunkHeader4 = default(VChunkHeader);
			vChunkHeader4.ChunkID = "ANIMKEYS";
			vChunkHeader4.TypeFlag = 1999801;
			vChunkHeader4.DataSize = 32;
			vChunkHeader4.DataCount = count;
			binaryWriter.Write(Utils.RawSerialize(vChunkHeader4));
			for (int n = 0; n < count; n++)
			{
				binaryWriter.Write(Utils.RawSerialize(list2[n]));
			}
			binaryWriter.Close();
			fileStream.Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			pskBonesExportCheckboxes(1);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			pskBonesExportCheckboxes(0);
		}

		private void button5_Click(object sender, EventArgs e)
		{
			pskBonesExportCheckboxes(2);
		}

		public void pskBonesExportCheckboxes(int action)
		{
			if (dgvPSKBones.Rows.Count < 1)
			{
				return;
			}
			bool flag = false;
			switch (action)
			{
			default:
				return;
			case 2:
			{
				for (int i = 0; i < dgvPSKBones.Rows.Count; i++)
				{
					dgvPSKBones.Rows[i].Cells[0].Value = !(bool)dgvPSKBones.Rows[i].Cells[0].Value;
				}
				return;
			}
			case 0:
				flag = false;
				break;
			case 1:
				flag = true;
				break;
			}
			for (int j = 0; j < dgvPSKBones.Rows.Count; j++)
			{
				dgvPSKBones.Rows[j].Cells[0].Value = flag;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			openPSAFilesButton = new System.Windows.Forms.Button();
			dgvPSAFiles = new System.Windows.Forms.DataGridView();
			dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dgvMotions = new System.Windows.Forms.DataGridView();
			Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dgvOutputFile = new System.Windows.Forms.DataGridView();
			dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			saveMultipleButton = new System.Windows.Forms.Button();
			addMotionButton = new System.Windows.Forms.Button();
			saveOutputButton = new System.Windows.Forms.Button();
			dgvPSKFile = new System.Windows.Forms.DataGridView();
			dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			openPSKFileButton = new System.Windows.Forms.Button();
			groupBox1 = new System.Windows.Forms.GroupBox();
			groupBox2 = new System.Windows.Forms.GroupBox();
			checkBox1 = new System.Windows.Forms.CheckBox();
			textBox1 = new System.Windows.Forms.TextBox();
			button6 = new System.Windows.Forms.Button();
			fullAnimationsInfoButton = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			button5 = new System.Windows.Forms.Button();
			button4 = new System.Windows.Forms.Button();
			button3 = new System.Windows.Forms.Button();
			dgvPSKBones = new System.Windows.Forms.DataGridView();
			dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			button2 = new System.Windows.Forms.Button();
			button1 = new System.Windows.Forms.Button();
			boneTreeButton = new System.Windows.Forms.Button();
			statusStrip1 = new System.Windows.Forms.StatusStrip();
			toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			((System.ComponentModel.ISupportInitialize)dgvPSAFiles).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvMotions).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvOutputFile).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvPSKFile).BeginInit();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvPSKBones).BeginInit();
			statusStrip1.SuspendLayout();
			SuspendLayout();
			openPSAFilesButton.Location = new System.Drawing.Point(6, 19);
			openPSAFilesButton.Name = "openPSAFilesButton";
			openPSAFilesButton.Size = new System.Drawing.Size(100, 23);
			openPSAFilesButton.TabIndex = 4;
			openPSAFilesButton.Text = "Open...";
			openPSAFilesButton.UseVisualStyleBackColor = true;
			openPSAFilesButton.Click += new System.EventHandler(openPSAFilesButton_Click);
			dgvPSAFiles.AllowUserToAddRows = false;
			dgvPSAFiles.AllowUserToResizeRows = false;
			dgvPSAFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dgvPSAFiles.BackgroundColor = System.Drawing.Color.White;
			dgvPSAFiles.CausesValidation = false;
			dgvPSAFiles.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dgvPSAFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dgvPSAFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgvPSAFiles.Columns.AddRange(dataGridViewTextBoxColumn1, Column5, Column6);
			dgvPSAFiles.Location = new System.Drawing.Point(6, 48);
			dgvPSAFiles.MultiSelect = false;
			dgvPSAFiles.Name = "dgvPSAFiles";
			dgvPSAFiles.ReadOnly = true;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dgvPSAFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
			dgvPSAFiles.RowHeadersVisible = false;
			dgvPSAFiles.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dgvPSAFiles.RowsDefaultCellStyle = dataGridViewCellStyle3;
			dgvPSAFiles.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dgvPSAFiles.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvPSAFiles.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dgvPSAFiles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgvPSAFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgvPSAFiles.Size = new System.Drawing.Size(332, 223);
			dgvPSAFiles.StandardTab = true;
			dgvPSAFiles.TabIndex = 5;
			dgvPSAFiles.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvPSAFiles_RowsRemoved);
			dgvPSAFiles.SelectionChanged += new System.EventHandler(dgvPSAFiles_SelectionChanged);
			dataGridViewTextBoxColumn1.HeaderText = "File";
			dataGridViewTextBoxColumn1.MinimumWidth = 90;
			dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			dataGridViewTextBoxColumn1.ReadOnly = true;
			Column5.FillWeight = 40f;
			Column5.HeaderText = "Animations";
			Column5.Name = "Column5";
			Column5.ReadOnly = true;
			Column6.FillWeight = 30f;
			Column6.HeaderText = "Bones";
			Column6.Name = "Column6";
			Column6.ReadOnly = true;
			dgvMotions.AllowUserToAddRows = false;
			dgvMotions.AllowUserToDeleteRows = false;
			dgvMotions.AllowUserToResizeRows = false;
			dgvMotions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dgvMotions.BackgroundColor = System.Drawing.Color.White;
			dgvMotions.CausesValidation = false;
			dgvMotions.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dgvMotions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			dgvMotions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgvMotions.Columns.AddRange(Column13, dataGridViewTextBoxColumn2, Column14, Column3, Column2, Column16, Column4);
			dgvMotions.Location = new System.Drawing.Point(344, 48);
			dgvMotions.Name = "dgvMotions";
			dgvMotions.ReadOnly = true;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dgvMotions.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
			dgvMotions.RowHeadersVisible = false;
			dgvMotions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvMotions.RowsDefaultCellStyle = dataGridViewCellStyle6;
			dgvMotions.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dgvMotions.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvMotions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dgvMotions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgvMotions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgvMotions.Size = new System.Drawing.Size(634, 223);
			dgvMotions.TabIndex = 6;
			Column13.FillWeight = 15f;
			Column13.HeaderText = "Index";
			Column13.Name = "Column13";
			Column13.ReadOnly = true;
			dataGridViewTextBoxColumn2.FillWeight = 70f;
			dataGridViewTextBoxColumn2.HeaderText = "Name";
			dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			dataGridViewTextBoxColumn2.ReadOnly = true;
			Column14.FillWeight = 30f;
			Column14.HeaderText = "Group";
			Column14.Name = "Column14";
			Column14.ReadOnly = true;
			Column3.FillWeight = 25f;
			Column3.HeaderText = "Track Time";
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column2.FillWeight = 15f;
			Column2.HeaderText = "FPS";
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column16.FillWeight = 20f;
			Column16.HeaderText = "First Frame";
			Column16.Name = "Column16";
			Column16.ReadOnly = true;
			Column4.FillWeight = 20f;
			Column4.HeaderText = "Frames";
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			dgvOutputFile.AllowUserToAddRows = false;
			dgvOutputFile.AllowUserToResizeRows = false;
			dgvOutputFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dgvOutputFile.BackgroundColor = System.Drawing.Color.White;
			dgvOutputFile.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvOutputFile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			dgvOutputFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgvOutputFile.Columns.AddRange(dataGridViewTextBoxColumn4, Column17, Column23, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn6, Column18, Column19, Column22);
			dgvOutputFile.Location = new System.Drawing.Point(344, 277);
			dgvOutputFile.MultiSelect = false;
			dgvOutputFile.Name = "dgvOutputFile";
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dgvOutputFile.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
			dgvOutputFile.RowHeadersVisible = false;
			dgvOutputFile.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvOutputFile.RowsDefaultCellStyle = dataGridViewCellStyle9;
			dgvOutputFile.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dgvOutputFile.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvOutputFile.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dgvOutputFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgvOutputFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgvOutputFile.Size = new System.Drawing.Size(634, 207);
			dgvOutputFile.TabIndex = 8;
			dataGridViewTextBoxColumn4.FillWeight = 30f;
			dataGridViewTextBoxColumn4.HeaderText = "Source File";
			dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			dataGridViewTextBoxColumn4.ReadOnly = true;
			dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column17.FillWeight = 15f;
			Column17.HeaderText = "Index";
			Column17.Name = "Column17";
			Column17.ReadOnly = true;
			Column17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column23.FillWeight = 40f;
			Column23.HeaderText = "Name*";
			Column23.MaxInputLength = 64;
			Column23.Name = "Column23";
			Column23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn3.FillWeight = 30f;
			dataGridViewTextBoxColumn3.HeaderText = "Group*";
			dataGridViewTextBoxColumn3.MaxInputLength = 55;
			dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn6.FillWeight = 15f;
			dataGridViewTextBoxColumn6.HeaderText = "Bones";
			dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			dataGridViewTextBoxColumn6.ReadOnly = true;
			dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column18.FillWeight = 30f;
			Column18.HeaderText = "Track Time*";
			Column18.Name = "Column18";
			Column18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column19.FillWeight = 15f;
			Column19.HeaderText = "FPS*";
			Column19.MaxInputLength = 4;
			Column19.Name = "Column19";
			Column19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column19.ToolTipText = "Frames per second";
			Column22.FillWeight = 15f;
			Column22.HeaderText = "Frames";
			Column22.Name = "Column22";
			Column22.ReadOnly = true;
			Column22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			saveMultipleButton.Location = new System.Drawing.Point(638, 19);
			saveMultipleButton.Name = "saveMultipleButton";
			saveMultipleButton.Size = new System.Drawing.Size(212, 23);
			saveMultipleButton.TabIndex = 9;
			saveMultipleButton.Text = "Save All Animations to Multiple Files...";
			saveMultipleButton.UseVisualStyleBackColor = true;
			saveMultipleButton.Click += new System.EventHandler(saveMultipleButton_Click);
			addMotionButton.Location = new System.Drawing.Point(856, 19);
			addMotionButton.Name = "addMotionButton";
			addMotionButton.Size = new System.Drawing.Size(122, 23);
			addMotionButton.TabIndex = 11;
			addMotionButton.Text = "Add Animation(s)";
			addMotionButton.UseVisualStyleBackColor = true;
			addMotionButton.Click += new System.EventHandler(addMotionButton_Click);
			saveOutputButton.Location = new System.Drawing.Point(842, 490);
			saveOutputButton.Name = "saveOutputButton";
			saveOutputButton.Size = new System.Drawing.Size(136, 23);
			saveOutputButton.TabIndex = 12;
			saveOutputButton.Text = "Save PSA File...";
			saveOutputButton.UseVisualStyleBackColor = true;
			saveOutputButton.Click += new System.EventHandler(saveOutputButton_Click);
			dgvPSKFile.AllowUserToAddRows = false;
			dgvPSKFile.AllowUserToDeleteRows = false;
			dgvPSKFile.AllowUserToResizeColumns = false;
			dgvPSKFile.AllowUserToResizeRows = false;
			dgvPSKFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dgvPSKFile.BackgroundColor = System.Drawing.Color.White;
			dgvPSKFile.CausesValidation = false;
			dgvPSKFile.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvPSKFile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
			dgvPSKFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgvPSKFile.Columns.AddRange(dataGridViewTextBoxColumn5, Column7, Column8, Column9, Column10, Column11, Column12, Column1);
			dgvPSKFile.Location = new System.Drawing.Point(6, 48);
			dgvPSKFile.MultiSelect = false;
			dgvPSKFile.Name = "dgvPSKFile";
			dgvPSKFile.ReadOnly = true;
			dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dgvPSKFile.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
			dgvPSKFile.RowHeadersVisible = false;
			dgvPSKFile.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvPSKFile.RowsDefaultCellStyle = dataGridViewCellStyle12;
			dgvPSKFile.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dgvPSKFile.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvPSKFile.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dgvPSKFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgvPSKFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgvPSKFile.Size = new System.Drawing.Size(972, 55);
			dgvPSKFile.TabIndex = 13;
			dataGridViewTextBoxColumn5.HeaderText = "File";
			dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			dataGridViewTextBoxColumn5.ReadOnly = true;
			dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column7.FillWeight = 20f;
			Column7.HeaderText = "Size (kb)";
			Column7.Name = "Column7";
			Column7.ReadOnly = true;
			Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column8.FillWeight = 20f;
			Column8.HeaderText = "Points";
			Column8.Name = "Column8";
			Column8.ReadOnly = true;
			Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column9.FillWeight = 20f;
			Column9.HeaderText = "Wedges";
			Column9.Name = "Column9";
			Column9.ReadOnly = true;
			Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column10.FillWeight = 20f;
			Column10.HeaderText = "Faces";
			Column10.Name = "Column10";
			Column10.ReadOnly = true;
			Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column11.FillWeight = 20f;
			Column11.HeaderText = "Materials";
			Column11.Name = "Column11";
			Column11.ReadOnly = true;
			Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column12.FillWeight = 20f;
			Column12.HeaderText = "Bones";
			Column12.Name = "Column12";
			Column12.ReadOnly = true;
			Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.FillWeight = 20f;
			Column1.HeaderText = "Influences";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			openPSKFileButton.Location = new System.Drawing.Point(6, 19);
			openPSKFileButton.Name = "openPSKFileButton";
			openPSKFileButton.Size = new System.Drawing.Size(100, 23);
			openPSKFileButton.TabIndex = 14;
			openPSKFileButton.Text = "Open...";
			openPSKFileButton.UseVisualStyleBackColor = true;
			openPSKFileButton.Click += new System.EventHandler(openPSKFileButton_Click);
			groupBox1.Controls.Add(openPSKFileButton);
			groupBox1.Controls.Add(dgvPSKFile);
			groupBox1.Location = new System.Drawing.Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(984, 114);
			groupBox1.TabIndex = 16;
			groupBox1.TabStop = false;
			groupBox1.Text = "PSK File";
			groupBox2.Controls.Add(checkBox1);
			groupBox2.Controls.Add(textBox1);
			groupBox2.Controls.Add(button6);
			groupBox2.Controls.Add(fullAnimationsInfoButton);
			groupBox2.Controls.Add(label1);
			groupBox2.Controls.Add(button5);
			groupBox2.Controls.Add(button4);
			groupBox2.Controls.Add(button3);
			groupBox2.Controls.Add(dgvPSKBones);
			groupBox2.Controls.Add(button2);
			groupBox2.Controls.Add(button1);
			groupBox2.Controls.Add(boneTreeButton);
			groupBox2.Controls.Add(openPSAFilesButton);
			groupBox2.Controls.Add(dgvPSAFiles);
			groupBox2.Controls.Add(saveOutputButton);
			groupBox2.Controls.Add(dgvMotions);
			groupBox2.Controls.Add(dgvOutputFile);
			groupBox2.Controls.Add(addMotionButton);
			groupBox2.Controls.Add(saveMultipleButton);
			groupBox2.Location = new System.Drawing.Point(12, 142);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(984, 524);
			groupBox2.TabIndex = 17;
			groupBox2.TabStop = false;
			groupBox2.Text = "PSA Files";
			checkBox1.AutoSize = true;
			checkBox1.Location = new System.Drawing.Point(344, 0);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new System.Drawing.Size(107, 17);
			checkBox1.TabIndex = 24;
			checkBox1.Text = "Skip Frames by 2";
			checkBox1.UseVisualStyleBackColor = true;
			checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
			textBox1.BackColor = System.Drawing.Color.Silver;
			textBox1.ForeColor = System.Drawing.Color.Red;
			textBox1.Location = new System.Drawing.Point(454, -2);
			textBox1.MaxLength = 4;
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(35, 20);
			textBox1.TabIndex = 23;
			textBox1.Text = "TNT";
			textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
			button6.Enabled = false;
			button6.Location = new System.Drawing.Point(344, 19);
			button6.Name = "button6";
			button6.Size = new System.Drawing.Size(145, 23);
			button6.TabIndex = 22;
			button6.Text = "GZDoom modeldef export";
			button6.UseVisualStyleBackColor = true;
			button6.Click += new System.EventHandler(button6_Click);
			fullAnimationsInfoButton.Location = new System.Drawing.Point(495, 19);
			fullAnimationsInfoButton.Name = "fullAnimationsInfoButton";
			fullAnimationsInfoButton.Size = new System.Drawing.Size(137, 23);
			fullAnimationsInfoButton.TabIndex = 21;
			fullAnimationsInfoButton.Text = "Full Animations Info";
			fullAnimationsInfoButton.UseVisualStyleBackColor = true;
			fullAnimationsInfoButton.Click += new System.EventHandler(fullAnimationsInfoButton_Click);
			label1.AutoSize = true;
			label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			label1.Location = new System.Drawing.Point(609, 487);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(73, 13);
			label1.TabIndex = 20;
			label1.Text = "* editable field";
			button5.Location = new System.Drawing.Point(168, 490);
			button5.Name = "button5";
			button5.Size = new System.Drawing.Size(75, 23);
			button5.TabIndex = 19;
			button5.Text = "Toggle";
			button5.UseVisualStyleBackColor = true;
			button5.Click += new System.EventHandler(button5_Click);
			button4.Location = new System.Drawing.Point(87, 490);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(75, 23);
			button4.TabIndex = 18;
			button4.Text = "Unselect All";
			button4.UseVisualStyleBackColor = true;
			button4.Click += new System.EventHandler(button4_Click);
			button3.Location = new System.Drawing.Point(6, 490);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(75, 23);
			button3.TabIndex = 17;
			button3.Text = "Select All";
			button3.UseVisualStyleBackColor = true;
			button3.Click += new System.EventHandler(button3_Click);
			dgvPSKBones.AllowUserToAddRows = false;
			dgvPSKBones.AllowUserToResizeRows = false;
			dgvPSKBones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dgvPSKBones.BackgroundColor = System.Drawing.Color.White;
			dgvPSKBones.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dgvPSKBones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
			dgvPSKBones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgvPSKBones.Columns.AddRange(dataGridViewTextBoxColumn7, Column15, dataGridViewTextBoxColumn8);
			dgvPSKBones.Location = new System.Drawing.Point(6, 277);
			dgvPSKBones.MultiSelect = false;
			dgvPSKBones.Name = "dgvPSKBones";
			dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dgvPSKBones.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
			dgvPSKBones.RowHeadersVisible = false;
			dgvPSKBones.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dgvPSKBones.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dgvPSKBones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgvPSKBones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgvPSKBones.Size = new System.Drawing.Size(332, 207);
			dgvPSKBones.StandardTab = true;
			dgvPSKBones.TabIndex = 16;
			dataGridViewTextBoxColumn7.FillWeight = 30f;
			dataGridViewTextBoxColumn7.HeaderText = "Export";
			dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			Column15.DefaultCellStyle = dataGridViewCellStyle15;
			Column15.FillWeight = 20f;
			Column15.HeaderText = "Index";
			Column15.Name = "Column15";
			Column15.ReadOnly = true;
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle16;
			dataGridViewTextBoxColumn8.HeaderText = "PSK Bones";
			dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			dataGridViewTextBoxColumn8.ReadOnly = true;
			button2.Location = new System.Drawing.Point(400, 490);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(50, 23);
			button2.TabIndex = 15;
			button2.Text = "?";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(button2_Click);
			button1.Location = new System.Drawing.Point(344, 490);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(50, 23);
			button1.TabIndex = 14;
			button1.Text = "?";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			boneTreeButton.Location = new System.Drawing.Point(161, 19);
			boneTreeButton.Name = "boneTreeButton";
			boneTreeButton.Size = new System.Drawing.Size(177, 23);
			boneTreeButton.TabIndex = 13;
			boneTreeButton.Text = "PSK/PSA Bone Tree";
			boneTreeButton.UseVisualStyleBackColor = true;
			boneTreeButton.Click += new System.EventHandler(boneTreeButton_Click);
			statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { toolStripStatusLabel1 });
			statusStrip1.Location = new System.Drawing.Point(0, 692);
			statusStrip1.Name = "statusStrip1";
			statusStrip1.Size = new System.Drawing.Size(1008, 22);
			statusStrip1.SizingGrip = false;
			statusStrip1.TabIndex = 18;
			statusStrip1.Text = "statusStrip1";
			toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
			toolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			toolStripStatusLabel1.Size = new System.Drawing.Size(811, 17);
			toolStripStatusLabel1.Text = "David Najar's Unreal Engine PSA Animation file format toolkit (DN UnPSA Toolkit) v0.38b.  Developed by David Najar (david_najar@hotmail.com) 04/2009";
			toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1008, 714);
			base.Controls.Add(statusStrip1);
			base.Controls.Add(groupBox2);
			base.Controls.Add(groupBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.HelpButton = true;
			base.MaximizeBox = false;
			base.Name = "MainForm";
			base.ShowIcon = false;
			Text = "DN UnPSA Toolkit v0.38b (Edited by Kizoky)";
			base.Load += new System.EventHandler(MainForm_Load);
			((System.ComponentModel.ISupportInitialize)dgvPSAFiles).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvMotions).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvOutputFile).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvPSKFile).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvPSKBones).EndInit();
			statusStrip1.ResumeLayout(false);
			statusStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
		}
	
		// Program edited here - Kizoky (also a bunch of buttons for it)
		private string Sprite = "TNT";
		private void Modeldef_Generate(PSA pSA, int dataCount)
		{
            string ExeLoc = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string Output = Path.Combine(ExeLoc, "PSA_Modeldef_Output.txt");
			
			bool skipFrame = checkBox1.Checked;
			string[] alphabet = new string[37]
			{
				"A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
				"K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
				"U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3",
				"4", "5", "6", "7", "8", "9", "10"
			};
			int spriteset = -1;
			using (StreamWriter streamWriter = File.AppendText(Output))
			{
				for (int i = 0; i < dataCount; i++)
				{
					spriteset++;
					streamWriter.WriteLine($"// {pSA.animData[i].Name} (1st Frame: {pSA.animData[i].FirstRawFrame} | Last Frame: {pSA.animData[i].FirstRawFrame + pSA.animData[i].NumRawFrames - 1})");
					int alph = 0;
					for (int j = pSA.animData[i].FirstRawFrame; j < pSA.animData[i].FirstRawFrame + pSA.animData[i].NumRawFrames; j++)
					{
						streamWriter.WriteLine($"     FrameIndex {Sprite}{alphabet[spriteset]} {alphabet[alph]} 0 {j}");
						alph++;
						if (alph >= 26)
						{
							alph = 0;
							spriteset++;
							streamWriter.WriteLine();
						}
						
						if (spriteset >= alphabet.Length - 1)
						{
							spriteset = 0;
						}
						
						if (skipFrame)
						{
							j++;
						}
					}
					streamWriter.WriteLine();
				}
			}
			Process.Start(@Output);
		}
	
		// Program edited here - Kizoky (also a bunch of buttons for it)
		private void button6_Click(object sender, EventArgs e)
		{
			PSA pSA = psaFilesDictionary[SelectedPSAFile];
			int dataCount = pSA.animHeader.DataCount;
			
			Modeldef_Generate(pSA, dataCount);
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Program edited here - Kizoky (also a bunch of buttons for it)
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			Sprite = textBox1.Text;
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
		}
	}
}
