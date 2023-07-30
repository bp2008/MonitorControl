
namespace MonitorControl
{
	partial class HotkeyForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnAddHotkey = new System.Windows.Forms.Button();
			this.panelHotkeys = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// btnAddHotkey
			// 
			this.btnAddHotkey.Location = new System.Drawing.Point(12, 12);
			this.btnAddHotkey.Name = "btnAddHotkey";
			this.btnAddHotkey.Size = new System.Drawing.Size(134, 23);
			this.btnAddHotkey.TabIndex = 0;
			this.btnAddHotkey.Text = "Add Hotkey";
			this.btnAddHotkey.UseVisualStyleBackColor = true;
			this.btnAddHotkey.Click += new System.EventHandler(this.btnAddHotkey_Click);
			// 
			// panelHotkeys
			// 
			this.panelHotkeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelHotkeys.AutoScroll = true;
			this.panelHotkeys.Location = new System.Drawing.Point(0, 41);
			this.panelHotkeys.Name = "panelHotkeys";
			this.panelHotkeys.Size = new System.Drawing.Size(329, 207);
			this.panelHotkeys.TabIndex = 1;
			// 
			// HotkeyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(329, 248);
			this.Controls.Add(this.panelHotkeys);
			this.Controls.Add(this.btnAddHotkey);
			this.DoubleBuffered = true;
			this.MinimumSize = new System.Drawing.Size(345, 287);
			this.Name = "HotkeyForm";
			this.Text = "HotkeyForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HotkeyForm_FormClosing);
			this.Load += new System.EventHandler(this.HotkeyForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnAddHotkey;
		private System.Windows.Forms.Panel panelHotkeys;
	}
}