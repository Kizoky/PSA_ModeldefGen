using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DN_UNPSA_TOOLKIT
{
	public class ExportOptionsForm : Form
	{
		private IContainer components;

		private Button button1;

		private Button button2;

		private CheckBox checkBox1;

		public ExportOptionsForm()
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
			button1 = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			checkBox1 = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			button1.Location = new System.Drawing.Point(116, 229);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(75, 23);
			button1.TabIndex = 1;
			button1.Text = "Ok";
			button1.UseVisualStyleBackColor = true;
			button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			button2.Location = new System.Drawing.Point(197, 229);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(75, 23);
			button2.TabIndex = 2;
			button2.Text = "Cancel";
			button2.UseVisualStyleBackColor = true;
			checkBox1.AutoSize = true;
			checkBox1.Location = new System.Drawing.Point(13, 13);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new System.Drawing.Size(194, 17);
			checkBox1.TabIndex = 3;
			checkBox1.Text = "Include PSK bind position (frame 0) ";
			checkBox1.UseVisualStyleBackColor = true;
			base.AcceptButton = button1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = button2;
			base.ClientSize = new System.Drawing.Size(284, 264);
			base.ControlBox = false;
			base.Controls.Add(checkBox1);
			base.Controls.Add(button2);
			base.Controls.Add(button1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ExportOptionsForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "PSA Export Options";
			base.TopMost = true;
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
