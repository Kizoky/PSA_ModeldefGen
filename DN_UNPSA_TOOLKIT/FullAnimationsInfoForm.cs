using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DN_UNPSA_TOOLKIT
{
	public class FullAnimationsInfoForm : Form
	{
		private IContainer components;

		public DataGridView dgvFullInfo;

		private DataGridViewTextBoxColumn Column13;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn Column14;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column6;

		private DataGridViewTextBoxColumn Column7;

		private DataGridViewTextBoxColumn Column8;

		private DataGridViewTextBoxColumn Column9;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column16;

		private Button button1;

		private DataGridViewTextBoxColumn Column4;

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
			dgvFullInfo = new System.Windows.Forms.DataGridView();
			Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)dgvFullInfo).BeginInit();
			SuspendLayout();
			dgvFullInfo.AllowUserToAddRows = false;
			dgvFullInfo.AllowUserToDeleteRows = false;
			dgvFullInfo.AllowUserToResizeRows = false;
			dgvFullInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			dgvFullInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dgvFullInfo.BackgroundColor = System.Drawing.Color.White;
			dgvFullInfo.CausesValidation = false;
			dgvFullInfo.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dgvFullInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dgvFullInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dgvFullInfo.Columns.AddRange(Column13, dataGridViewTextBoxColumn2, Column14, Column1, Column5, Column6, Column7, Column8, Column9, Column2, Column3, Column16, Column4);
			dgvFullInfo.Location = new System.Drawing.Point(12, 33);
			dgvFullInfo.Name = "dgvFullInfo";
			dgvFullInfo.ReadOnly = true;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dgvFullInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
			dgvFullInfo.RowHeadersVisible = false;
			dgvFullInfo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgvFullInfo.RowsDefaultCellStyle = dataGridViewCellStyle3;
			dgvFullInfo.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dgvFullInfo.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgvFullInfo.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dgvFullInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgvFullInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			dgvFullInfo.Size = new System.Drawing.Size(984, 414);
			dgvFullInfo.TabIndex = 7;
			Column13.FillWeight = 30f;
			Column13.HeaderText = "Index";
			Column13.Name = "Column13";
			Column13.ReadOnly = true;
			dataGridViewTextBoxColumn2.HeaderText = "Name";
			dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			dataGridViewTextBoxColumn2.ReadOnly = true;
			Column14.FillWeight = 50f;
			Column14.HeaderText = "Group";
			Column14.Name = "Column14";
			Column14.ReadOnly = true;
			Column1.FillWeight = 25f;
			Column1.HeaderText = "Total Bones";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column5.FillWeight = 25f;
			Column5.HeaderText = "Root Include";
			Column5.Name = "Column5";
			Column5.ReadOnly = true;
			Column6.FillWeight = 25f;
			Column6.HeaderText = "Key Compression Style";
			Column6.Name = "Column6";
			Column6.ReadOnly = true;
			Column7.FillWeight = 25f;
			Column7.HeaderText = "Key Quotum";
			Column7.Name = "Column7";
			Column7.ReadOnly = true;
			Column8.FillWeight = 25f;
			Column8.HeaderText = "Key Reduction";
			Column8.Name = "Column8";
			Column8.ReadOnly = true;
			Column9.FillWeight = 40f;
			Column9.HeaderText = "Track Time";
			Column9.Name = "Column9";
			Column9.ReadOnly = true;
			Column2.FillWeight = 50f;
			Column2.HeaderText = "Animation Rate";
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column3.FillWeight = 40f;
			Column3.HeaderText = "Start Bone";
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column16.FillWeight = 35f;
			Column16.HeaderText = "First Raw Frame";
			Column16.Name = "Column16";
			Column16.ReadOnly = true;
			Column4.FillWeight = 50f;
			Column4.HeaderText = "Num Raw Frames";
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			button1.Location = new System.Drawing.Point(12, 4);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(150, 23);
			button1.TabIndex = 8;
			button1.Text = "Export to GZDoom modeldef";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1008, 459);
			base.Controls.Add(button1);
			base.Controls.Add(dgvFullInfo);
			base.Name = "FullAnimationsInfoForm";
			base.ShowIcon = false;
			base.TopMost = true;
			((System.ComponentModel.ISupportInitialize)dgvFullInfo).EndInit();
			ResumeLayout(false);
		}

		public FullAnimationsInfoForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}
	}
}
