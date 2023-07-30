
using System;

namespace MonitorControl
{
	partial class HotkeyControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.txtHotkeyInput = new System.Windows.Forms.TextBox();
			this.ddlHotkeyAction = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnDeleteHotkey = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Hotkey:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtHotkeyInput
			// 
			this.txtHotkeyInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtHotkeyInput.BackColor = System.Drawing.SystemColors.Window;
			this.txtHotkeyInput.Location = new System.Drawing.Point(53, 4);
			this.txtHotkeyInput.Name = "txtHotkeyInput";
			this.txtHotkeyInput.ReadOnly = true;
			this.txtHotkeyInput.Size = new System.Drawing.Size(190, 20);
			this.txtHotkeyInput.TabIndex = 1;
			this.txtHotkeyInput.TextChanged += new System.EventHandler(this.txtHotkeyInput_TextChanged);
			// 
			// ddlHotkeyAction
			// 
			this.ddlHotkeyAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ddlHotkeyAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlHotkeyAction.FormattingEnabled = true;
			this.ddlHotkeyAction.Location = new System.Drawing.Point(53, 30);
			this.ddlHotkeyAction.Name = "ddlHotkeyAction";
			this.ddlHotkeyAction.Size = new System.Drawing.Size(190, 21);
			this.ddlHotkeyAction.TabIndex = 2;
			this.ddlHotkeyAction.SelectedIndexChanged += new System.EventHandler(this.ddlHotkeyAction_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 21);
			this.label2.TabIndex = 3;
			this.label2.Text = "Action:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnDeleteHotkey
			// 
			this.btnDeleteHotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteHotkey.Location = new System.Drawing.Point(249, 1);
			this.btnDeleteHotkey.Name = "btnDeleteHotkey";
			this.btnDeleteHotkey.Size = new System.Drawing.Size(75, 50);
			this.btnDeleteHotkey.TabIndex = 4;
			this.btnDeleteHotkey.Text = "delete hotkey";
			this.btnDeleteHotkey.UseVisualStyleBackColor = true;
			this.btnDeleteHotkey.Click += new System.EventHandler(this.btnDeleteHotkey_Click);
			// 
			// HotkeyControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.btnDeleteHotkey);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ddlHotkeyAction);
			this.Controls.Add(this.txtHotkeyInput);
			this.Controls.Add(this.label1);
			this.Name = "HotkeyControl";
			this.Size = new System.Drawing.Size(330, 55);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtHotkeyInput;
		private System.Windows.Forms.ComboBox ddlHotkeyAction;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnDeleteHotkey;
	}
}
