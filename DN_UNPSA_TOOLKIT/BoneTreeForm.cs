using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DN_UNPSA_TOOLKIT
{
	public class BoneTreeForm : Form
	{
		private IContainer components;

		private SplitContainer splitContainer1;

		public TreeView tvPSA;

		public TreeView tvPSK;

		public Label pskFileName;

		public Label psaFileName;

		private Label label1;

		private Label label2;

		public BoneTreeForm()
		{
			InitializeComponent();
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
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			label1 = new System.Windows.Forms.Label();
			pskFileName = new System.Windows.Forms.Label();
			tvPSK = new System.Windows.Forms.TreeView();
			label2 = new System.Windows.Forms.Label();
			psaFileName = new System.Windows.Forms.Label();
			tvPSA = new System.Windows.Forms.TreeView();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			SuspendLayout();
			splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer1.Location = new System.Drawing.Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Panel1.Controls.Add(label1);
			splitContainer1.Panel1.Controls.Add(pskFileName);
			splitContainer1.Panel1.Controls.Add(tvPSK);
			splitContainer1.Panel2.Controls.Add(label2);
			splitContainer1.Panel2.Controls.Add(psaFileName);
			splitContainer1.Panel2.Controls.Add(tvPSA);
			splitContainer1.Size = new System.Drawing.Size(534, 379);
			splitContainer1.SplitterDistance = 267;
			splitContainer1.TabIndex = 0;
			label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			label1.Location = new System.Drawing.Point(12, 349);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(191, 13);
			label1.TabIndex = 2;
			label1.Text = "Bones in red were not found in PSA file";
			pskFileName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			pskFileName.Location = new System.Drawing.Point(13, 12);
			pskFileName.Name = "pskFileName";
			pskFileName.Size = new System.Drawing.Size(251, 13);
			pskFileName.TabIndex = 1;
			pskFileName.Text = "label1";
			tvPSK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tvPSK.HideSelection = false;
			tvPSK.Location = new System.Drawing.Point(12, 31);
			tvPSK.Name = "tvPSK";
			tvPSK.Size = new System.Drawing.Size(252, 311);
			tvPSK.TabIndex = 0;
			label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			label2.AutoSize = true;
			label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			label2.Location = new System.Drawing.Point(4, 348);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(191, 13);
			label2.TabIndex = 2;
			label2.Text = "Bones in red were not found in PSK file";
			psaFileName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			psaFileName.Location = new System.Drawing.Point(4, 12);
			psaFileName.Name = "psaFileName";
			psaFileName.Size = new System.Drawing.Size(247, 13);
			psaFileName.TabIndex = 1;
			psaFileName.Text = "label1";
			tvPSA.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tvPSA.HideSelection = false;
			tvPSA.Location = new System.Drawing.Point(3, 31);
			tvPSA.Name = "tvPSA";
			tvPSA.Size = new System.Drawing.Size(248, 311);
			tvPSA.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(534, 379);
			base.Controls.Add(splitContainer1);
			base.Name = "BoneTreeForm";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "PSK/PSA Bones Tree";
			base.TopMost = true;
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel1.PerformLayout();
			splitContainer1.Panel2.ResumeLayout(false);
			splitContainer1.Panel2.PerformLayout();
			splitContainer1.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
