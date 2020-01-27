namespace MonitorControl
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.label1 = new System.Windows.Forms.Label();
			this.nudHttpPort = new System.Windows.Forms.NumericUpDown();
			this.nudHttpsPort = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.lblCurrentHttp = new System.Windows.Forms.Label();
			this.nudIdleMs = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtIpWhitelist = new System.Windows.Forms.TextBox();
			this.btnExitProgram = new System.Windows.Forms.Button();
			this.btnOpenDataFolder = new System.Windows.Forms.Button();
			this.btnOpenWebInterface = new System.Windows.Forms.Button();
			this.cbStartAutomatically = new System.Windows.Forms.CheckBox();
			this.btnOK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.nudHttpPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHttpsPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdleMs)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "HTTP Port:";
			this.toolTip1.SetToolTip(this.label1, "[1-65535]\r\n\r\nIf 0, the port is dynamically selected.\r\n\r\nIf -1, the protocol is di" +
        "sabled.\r\n");
			// 
			// nudHttpPort
			// 
			this.nudHttpPort.Location = new System.Drawing.Point(79, 46);
			this.nudHttpPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudHttpPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.nudHttpPort.Name = "nudHttpPort";
			this.nudHttpPort.Size = new System.Drawing.Size(72, 20);
			this.nudHttpPort.TabIndex = 1;
			this.toolTip1.SetToolTip(this.nudHttpPort, "[1-65535]\r\n\r\nIf 0, the port is dynamically selected.\r\n\r\nIf -1, the protocol is di" +
        "sabled.\r\n");
			this.nudHttpPort.ValueChanged += new System.EventHandler(this.nudHttpPort_ValueChanged);
			// 
			// nudHttpsPort
			// 
			this.nudHttpsPort.Location = new System.Drawing.Point(246, 46);
			this.nudHttpsPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudHttpsPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.nudHttpsPort.Name = "nudHttpsPort";
			this.nudHttpsPort.Size = new System.Drawing.Size(72, 20);
			this.nudHttpsPort.TabIndex = 3;
			this.toolTip1.SetToolTip(this.nudHttpsPort, "[1-65535]\r\n\r\nIf 0, the port is dynamically selected.\r\n\r\nIf -1, the protocol is di" +
        "sabled.\r\n");
			this.nudHttpsPort.ValueChanged += new System.EventHandler(this.nudHttpsPort_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(172, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "HTTPS Port:";
			this.toolTip1.SetToolTip(this.label2, "[1-65535]\r\n\r\nIf 0, the port is dynamically selected.\r\n\r\nIf -1, the protocol is di" +
        "sabled.\r\n");
			// 
			// lblCurrentHttp
			// 
			this.lblCurrentHttp.AutoSize = true;
			this.lblCurrentHttp.Location = new System.Drawing.Point(12, 77);
			this.lblCurrentHttp.Name = "lblCurrentHttp";
			this.lblCurrentHttp.Size = new System.Drawing.Size(94, 13);
			this.lblCurrentHttp.TabIndex = 5;
			this.lblCurrentHttp.Text = "Active Ports: -1, -1";
			this.toolTip1.SetToolTip(this.lblCurrentHttp, "[1-65535]\r\n\r\nIf 0, the port is dynamically selected.\r\n\r\nIf -1, the protocol is di" +
        "sabled.");
			// 
			// nudIdleMs
			// 
			this.nudIdleMs.Location = new System.Drawing.Point(71, 112);
			this.nudIdleMs.Maximum = new decimal(new int[] {
            3600000,
            0,
            0,
            0});
			this.nudIdleMs.Name = "nudIdleMs";
			this.nudIdleMs.Size = new System.Drawing.Size(80, 20);
			this.nudIdleMs.TabIndex = 7;
			this.toolTip1.SetToolTip(this.nudIdleMs, "The system is considered idle this many milliseconds after the last user input.");
			this.nudIdleMs.ValueChanged += new System.EventHandler(this.nudIdleMs_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 114);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Idle Time:";
			this.toolTip1.SetToolTip(this.label3, "The system is considered idle this many milliseconds after the last user input.");
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(157, 114);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "milliseconds";
			this.toolTip1.SetToolTip(this.label4, "The system is considered idle this many milliseconds after the last user input.");
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 153);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(101, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "IP Address Whitelist";
			this.toolTip1.SetToolTip(this.label5, "Separate items with new lines, spaces, tabs, or commas.");
			// 
			// txtIpWhitelist
			// 
			this.txtIpWhitelist.Location = new System.Drawing.Point(12, 169);
			this.txtIpWhitelist.Multiline = true;
			this.txtIpWhitelist.Name = "txtIpWhitelist";
			this.txtIpWhitelist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtIpWhitelist.Size = new System.Drawing.Size(317, 120);
			this.txtIpWhitelist.TabIndex = 9;
			this.toolTip1.SetToolTip(this.txtIpWhitelist, "Separate items with new lines, spaces, tabs, or commas.");
			this.txtIpWhitelist.TextChanged += new System.EventHandler(this.txtIpWhitelist_TextChanged);
			// 
			// btnExitProgram
			// 
			this.btnExitProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.btnExitProgram.Location = new System.Drawing.Point(183, 295);
			this.btnExitProgram.Name = "btnExitProgram";
			this.btnExitProgram.Size = new System.Drawing.Size(146, 23);
			this.btnExitProgram.TabIndex = 13;
			this.btnExitProgram.Text = "Exit Program";
			this.toolTip1.SetToolTip(this.btnExitProgram, "Closes the program. If you simply close this window, the program will remain runn" +
        "ing in the system tray.");
			this.btnExitProgram.UseVisualStyleBackColor = false;
			this.btnExitProgram.Click += new System.EventHandler(this.btnExitProgram_Click);
			// 
			// btnOpenDataFolder
			// 
			this.btnOpenDataFolder.Location = new System.Drawing.Point(12, 295);
			this.btnOpenDataFolder.Name = "btnOpenDataFolder";
			this.btnOpenDataFolder.Size = new System.Drawing.Size(146, 23);
			this.btnOpenDataFolder.TabIndex = 11;
			this.btnOpenDataFolder.Text = "Open Data Folder";
			this.btnOpenDataFolder.UseVisualStyleBackColor = true;
			this.btnOpenDataFolder.Click += new System.EventHandler(this.btnOpenDataFolder_Click);
			// 
			// btnOpenWebInterface
			// 
			this.btnOpenWebInterface.Location = new System.Drawing.Point(12, 324);
			this.btnOpenWebInterface.Name = "btnOpenWebInterface";
			this.btnOpenWebInterface.Size = new System.Drawing.Size(146, 23);
			this.btnOpenWebInterface.TabIndex = 15;
			this.btnOpenWebInterface.Text = "Open Web Interface";
			this.btnOpenWebInterface.UseVisualStyleBackColor = true;
			this.btnOpenWebInterface.Click += new System.EventHandler(this.btnOpenWebInterface_Click);
			// 
			// cbStartAutomatically
			// 
			this.cbStartAutomatically.AutoSize = true;
			this.cbStartAutomatically.Location = new System.Drawing.Point(15, 12);
			this.cbStartAutomatically.Name = "cbStartAutomatically";
			this.cbStartAutomatically.Size = new System.Drawing.Size(155, 17);
			this.cbStartAutomatically.TabIndex = 0;
			this.cbStartAutomatically.Text = "Start Program Automatically";
			this.cbStartAutomatically.UseVisualStyleBackColor = true;
			this.cbStartAutomatically.CheckedChanged += new System.EventHandler(this.cbStartAutomatically_CheckedChanged);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(183, 324);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(146, 23);
			this.btnOK.TabIndex = 17;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(341, 359);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.cbStartAutomatically);
			this.Controls.Add(this.btnOpenWebInterface);
			this.Controls.Add(this.btnExitProgram);
			this.Controls.Add(this.btnOpenDataFolder);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtIpWhitelist);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.nudIdleMs);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblCurrentHttp);
			this.Controls.Add(this.nudHttpsPort);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.nudHttpPort);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "MonitorControl";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.nudHttpPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHttpsPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdleMs)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nudHttpPort;
		private System.Windows.Forms.NumericUpDown nudHttpsPort;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblCurrentHttp;
		private System.Windows.Forms.NumericUpDown nudIdleMs;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtIpWhitelist;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnOpenDataFolder;
		private System.Windows.Forms.Button btnExitProgram;
		private System.Windows.Forms.Button btnOpenWebInterface;
		private System.Windows.Forms.CheckBox cbStartAutomatically;
		private System.Windows.Forms.Button btnOK;
	}
}

