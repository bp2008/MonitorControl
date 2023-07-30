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
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.nudSyncPort = new System.Windows.Forms.NumericUpDown();
			this.txtSyncAddress = new System.Windows.Forms.TextBox();
			this.cbSyncHTTPS = new System.Windows.Forms.CheckBox();
			this.ddlSyncFailureAction = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cbSyncMuteWhenOff = new System.Windows.Forms.CheckBox();
			this.cbAllowLocalOverride = new System.Windows.Forms.CheckBox();
			this.nudInputsRequiredToWake = new System.Windows.Forms.NumericUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.txtCommandsOn = new System.Windows.Forms.TextBox();
			this.txtCommandsOff = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.txtCommandsOffAfterDelay = new System.Windows.Forms.TextBox();
			this.btnOpenDataFolder = new System.Windows.Forms.Button();
			this.btnOpenWebInterface = new System.Windows.Forms.Button();
			this.cbStartAutomatically = new System.Windows.Forms.CheckBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnManageHotkeys = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.nudHttpPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHttpsPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdleMs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSyncPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudInputsRequiredToWake)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "HTTP Port:";
			this.toolTip1.SetToolTip(this.label1, "[1-65535]\r\n\r\nIf 0, the port is dynamically selected.\r\n\r\nIf -1, the protocol is di" +
        "sabled.\r\n");
			// 
			// nudHttpPort
			// 
			this.nudHttpPort.Location = new System.Drawing.Point(79, 35);
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
			this.nudHttpsPort.Location = new System.Drawing.Point(254, 35);
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
			this.label2.Location = new System.Drawing.Point(180, 37);
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
			this.lblCurrentHttp.Location = new System.Drawing.Point(12, 58);
			this.lblCurrentHttp.Name = "lblCurrentHttp";
			this.lblCurrentHttp.Size = new System.Drawing.Size(94, 13);
			this.lblCurrentHttp.TabIndex = 5;
			this.lblCurrentHttp.Text = "Active Ports: -1, -1";
			this.toolTip1.SetToolTip(this.lblCurrentHttp, "[1-65535]\r\n\r\nIf 0, the port is dynamically selected.\r\n\r\nIf -1, the protocol is di" +
        "sabled.");
			// 
			// nudIdleMs
			// 
			this.nudIdleMs.Location = new System.Drawing.Point(78, 76);
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
			this.label3.Location = new System.Drawing.Point(19, 78);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Idle Time:";
			this.toolTip1.SetToolTip(this.label3, "The system is considered idle this many milliseconds after the last user input.");
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 30000;
			this.toolTip1.InitialDelay = 500;
			this.toolTip1.ReshowDelay = 100;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(164, 78);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "milliseconds";
			this.toolTip1.SetToolTip(this.label4, "The system is considered idle this many milliseconds after the last user input.");
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 259);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(101, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "IP Address Whitelist";
			this.toolTip1.SetToolTip(this.label5, "Separate items with new lines, spaces, tabs, or commas.");
			// 
			// txtIpWhitelist
			// 
			this.txtIpWhitelist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtIpWhitelist.Location = new System.Drawing.Point(12, 275);
			this.txtIpWhitelist.Multiline = true;
			this.txtIpWhitelist.Name = "txtIpWhitelist";
			this.txtIpWhitelist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtIpWhitelist.Size = new System.Drawing.Size(317, 120);
			this.txtIpWhitelist.TabIndex = 20;
			this.toolTip1.SetToolTip(this.txtIpWhitelist, "Separate items with new lines, spaces, tabs, or commas.");
			this.txtIpWhitelist.TextChanged += new System.EventHandler(this.txtIpWhitelist_TextChanged);
			// 
			// btnExitProgram
			// 
			this.btnExitProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExitProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.btnExitProgram.Location = new System.Drawing.Point(548, 401);
			this.btnExitProgram.Name = "btnExitProgram";
			this.btnExitProgram.Size = new System.Drawing.Size(146, 23);
			this.btnExitProgram.TabIndex = 40;
			this.btnExitProgram.Text = "Exit Program";
			this.toolTip1.SetToolTip(this.btnExitProgram, "Closes the program. If you simply close this window, the program will remain runn" +
        "ing in the system tray.");
			this.btnExitProgram.UseVisualStyleBackColor = false;
			this.btnExitProgram.Click += new System.EventHandler(this.btnExitProgram_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 22);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 13);
			this.label7.TabIndex = 19;
			this.label7.Text = "Address:";
			this.toolTip1.SetToolTip(this.label7, "Host name or IP address of the server we are synchronizing with. Leave empty to d" +
        "isable sync.");
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(204, 22);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(29, 13);
			this.label8.TabIndex = 20;
			this.label8.Text = "Port:";
			this.toolTip1.SetToolTip(this.label8, "[1-65535] HTTP or HTTPS listening port on the server we are synchronizing with.");
			// 
			// nudSyncPort
			// 
			this.nudSyncPort.Location = new System.Drawing.Point(239, 20);
			this.nudSyncPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudSyncPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudSyncPort.Name = "nudSyncPort";
			this.nudSyncPort.Size = new System.Drawing.Size(72, 20);
			this.nudSyncPort.TabIndex = 11;
			this.toolTip1.SetToolTip(this.nudSyncPort, "[1-65535] HTTP or HTTPS listening port on the server we are synchronizing with.");
			this.nudSyncPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudSyncPort.ValueChanged += new System.EventHandler(this.nudSyncPort_ValueChanged);
			// 
			// txtSyncAddress
			// 
			this.txtSyncAddress.Location = new System.Drawing.Point(60, 19);
			this.txtSyncAddress.Name = "txtSyncAddress";
			this.txtSyncAddress.Size = new System.Drawing.Size(138, 20);
			this.txtSyncAddress.TabIndex = 10;
			this.toolTip1.SetToolTip(this.txtSyncAddress, "Host name or IP address of the server we are synchronizing with. Leave empty to d" +
        "isable sync.");
			this.txtSyncAddress.TextChanged += new System.EventHandler(this.txtSyncAddress_TextChanged);
			// 
			// cbSyncHTTPS
			// 
			this.cbSyncHTTPS.AutoSize = true;
			this.cbSyncHTTPS.Location = new System.Drawing.Point(239, 47);
			this.cbSyncHTTPS.Name = "cbSyncHTTPS";
			this.cbSyncHTTPS.Size = new System.Drawing.Size(62, 17);
			this.cbSyncHTTPS.TabIndex = 13;
			this.cbSyncHTTPS.Text = "HTTPS";
			this.toolTip1.SetToolTip(this.cbSyncHTTPS, "Check this box to use HTTPS. Uncheck to use HTTP.");
			this.cbSyncHTTPS.UseVisualStyleBackColor = true;
			this.cbSyncHTTPS.CheckedChanged += new System.EventHandler(this.cbSyncHTTPS_CheckedChanged);
			// 
			// ddlSyncFailureAction
			// 
			this.ddlSyncFailureAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlSyncFailureAction.FormattingEnabled = true;
			this.ddlSyncFailureAction.Items.AddRange(new object[] {
            "No Action",
            "Turn Off",
            "Turn On"});
			this.ddlSyncFailureAction.Location = new System.Drawing.Point(82, 45);
			this.ddlSyncFailureAction.Name = "ddlSyncFailureAction";
			this.ddlSyncFailureAction.Size = new System.Drawing.Size(121, 21);
			this.ddlSyncFailureAction.TabIndex = 12;
			this.toolTip1.SetToolTip(this.ddlSyncFailureAction, "Action to take if the remote server is unreachable.");
			this.ddlSyncFailureAction.SelectedIndexChanged += new System.EventHandler(this.ddlSyncFailureAction_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(70, 13);
			this.label6.TabIndex = 25;
			this.label6.Text = "On Sync Fail:";
			this.toolTip1.SetToolTip(this.label6, "Action to take if the remote server is unreachable.");
			// 
			// cbSyncMuteWhenOff
			// 
			this.cbSyncMuteWhenOff.AutoSize = true;
			this.cbSyncMuteWhenOff.Location = new System.Drawing.Point(6, 72);
			this.cbSyncMuteWhenOff.Name = "cbSyncMuteWhenOff";
			this.cbSyncMuteWhenOff.Size = new System.Drawing.Size(200, 17);
			this.cbSyncMuteWhenOff.TabIndex = 14;
			this.cbSyncMuteWhenOff.Text = "Mute audio when turning off monitors";
			this.toolTip1.SetToolTip(this.cbSyncMuteWhenOff, "Mute audio when turning off monitors because of remote server sync.\r\n\r\nAudio retu" +
        "rns to previous state when monitors turn back on.");
			this.cbSyncMuteWhenOff.UseVisualStyleBackColor = true;
			this.cbSyncMuteWhenOff.CheckedChanged += new System.EventHandler(this.cbSyncMuteWhenOff_CheckedChanged);
			// 
			// cbAllowLocalOverride
			// 
			this.cbAllowLocalOverride.AutoSize = true;
			this.cbAllowLocalOverride.Location = new System.Drawing.Point(6, 95);
			this.cbAllowLocalOverride.Name = "cbAllowLocalOverride";
			this.cbAllowLocalOverride.Size = new System.Drawing.Size(218, 17);
			this.cbAllowLocalOverride.TabIndex = 15;
			this.cbAllowLocalOverride.Text = "Allow local input to override synced state";
			this.toolTip1.SetToolTip(this.cbAllowLocalOverride, "If enabled, local inputs can un-sync this machine \r\nfrom the remote server until " +
        "the remote server \r\nhas another state change.\r\n\r\nIf disabled, the synced state i" +
        "s continuously enforced.\r\n");
			this.cbAllowLocalOverride.UseVisualStyleBackColor = true;
			this.cbAllowLocalOverride.CheckedChanged += new System.EventHandler(this.cbAllowLocalOverride_CheckedChanged);
			// 
			// nudInputsRequiredToWake
			// 
			this.nudInputsRequiredToWake.Location = new System.Drawing.Point(146, 102);
			this.nudInputsRequiredToWake.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.nudInputsRequiredToWake.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudInputsRequiredToWake.Name = "nudInputsRequiredToWake";
			this.nudInputsRequiredToWake.Size = new System.Drawing.Size(50, 20);
			this.nudInputsRequiredToWake.TabIndex = 8;
			this.toolTip1.SetToolTip(this.nudInputsRequiredToWake, resources.GetString("nudInputsRequiredToWake.ToolTip"));
			this.nudInputsRequiredToWake.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudInputsRequiredToWake.ValueChanged += new System.EventHandler(this.nudInputsRequiredToWake_ValueChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(19, 104);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(121, 13);
			this.label9.TabIndex = 61;
			this.label9.Text = "Inputs required to wake:";
			this.toolTip1.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(202, 104);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(121, 13);
			this.label10.TabIndex = 62;
			this.label10.Text = "(during 10-second span)";
			this.toolTip1.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
			// 
			// txtCommandsOn
			// 
			this.txtCommandsOn.Location = new System.Drawing.Point(335, 275);
			this.txtCommandsOn.Multiline = true;
			this.txtCommandsOn.Name = "txtCommandsOn";
			this.txtCommandsOn.Size = new System.Drawing.Size(359, 120);
			this.txtCommandsOn.TabIndex = 26;
			this.toolTip1.SetToolTip(this.txtCommandsOn, resources.GetString("txtCommandsOn.ToolTip"));
			this.txtCommandsOn.TextChanged += new System.EventHandler(this.txtCommandsOn_TextChanged);
			// 
			// txtCommandsOff
			// 
			this.txtCommandsOff.Location = new System.Drawing.Point(335, 31);
			this.txtCommandsOff.Multiline = true;
			this.txtCommandsOff.Name = "txtCommandsOff";
			this.txtCommandsOff.Size = new System.Drawing.Size(359, 91);
			this.txtCommandsOff.TabIndex = 24;
			this.toolTip1.SetToolTip(this.txtCommandsOff, resources.GetString("txtCommandsOff.ToolTip"));
			this.txtCommandsOff.TextChanged += new System.EventHandler(this.txtCommandsOff_TextChanged);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(335, 13);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(269, 13);
			this.label11.TabIndex = 65;
			this.label11.Text = "Additional commands to run when turning monitors OFF:";
			this.toolTip1.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(335, 259);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(265, 13);
			this.label12.TabIndex = 66;
			this.label12.Text = "Additional commands to run when turning monitors ON:";
			this.toolTip1.SetToolTip(this.label12, resources.GetString("label12.ToolTip"));
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(335, 125);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(359, 32);
			this.label13.TabIndex = 68;
			this.label13.Text = "Additional commands to run 9 seconds after turning monitors OFF, if state is stil" +
    "l OFF:";
			this.toolTip1.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
			// 
			// txtCommandsOffAfterDelay
			// 
			this.txtCommandsOffAfterDelay.Location = new System.Drawing.Point(335, 160);
			this.txtCommandsOffAfterDelay.Multiline = true;
			this.txtCommandsOffAfterDelay.Name = "txtCommandsOffAfterDelay";
			this.txtCommandsOffAfterDelay.Size = new System.Drawing.Size(359, 96);
			this.txtCommandsOffAfterDelay.TabIndex = 25;
			this.toolTip1.SetToolTip(this.txtCommandsOffAfterDelay, resources.GetString("txtCommandsOffAfterDelay.ToolTip"));
			this.txtCommandsOffAfterDelay.TextChanged += new System.EventHandler(this.txtCommandsOffAfterDelay_TextChanged);
			// 
			// btnOpenDataFolder
			// 
			this.btnOpenDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOpenDataFolder.Location = new System.Drawing.Point(12, 401);
			this.btnOpenDataFolder.Name = "btnOpenDataFolder";
			this.btnOpenDataFolder.Size = new System.Drawing.Size(146, 23);
			this.btnOpenDataFolder.TabIndex = 30;
			this.btnOpenDataFolder.Text = "Open Data Folder";
			this.btnOpenDataFolder.UseVisualStyleBackColor = true;
			this.btnOpenDataFolder.Click += new System.EventHandler(this.btnOpenDataFolder_Click);
			// 
			// btnOpenWebInterface
			// 
			this.btnOpenWebInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOpenWebInterface.Location = new System.Drawing.Point(12, 430);
			this.btnOpenWebInterface.Name = "btnOpenWebInterface";
			this.btnOpenWebInterface.Size = new System.Drawing.Size(146, 23);
			this.btnOpenWebInterface.TabIndex = 50;
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
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(548, 430);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(146, 23);
			this.btnOK.TabIndex = 60;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cbAllowLocalOverride);
			this.groupBox1.Controls.Add(this.cbSyncMuteWhenOff);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.ddlSyncFailureAction);
			this.groupBox1.Controls.Add(this.cbSyncHTTPS);
			this.groupBox1.Controls.Add(this.txtSyncAddress);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.nudSyncPort);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Location = new System.Drawing.Point(12, 135);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(317, 121);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Sync with another MonitorControl server";
			// 
			// btnManageHotkeys
			// 
			this.btnManageHotkeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnManageHotkeys.Location = new System.Drawing.Point(167, 401);
			this.btnManageHotkeys.Name = "btnManageHotkeys";
			this.btnManageHotkeys.Size = new System.Drawing.Size(146, 23);
			this.btnManageHotkeys.TabIndex = 32;
			this.btnManageHotkeys.Text = "Manage Hotkeys";
			this.btnManageHotkeys.UseVisualStyleBackColor = true;
			this.btnManageHotkeys.Click += new System.EventHandler(this.btnManageHotkeys_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(706, 465);
			this.Controls.Add(this.btnManageHotkeys);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.txtCommandsOff);
			this.Controls.Add(this.txtCommandsOn);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.nudInputsRequiredToWake);
			this.Controls.Add(this.groupBox1);
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
			this.Controls.Add(this.txtCommandsOffAfterDelay);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MonitorControl";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.nudHttpPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHttpsPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdleMs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSyncPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudInputsRequiredToWake)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
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
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown nudSyncPort;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtSyncAddress;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox ddlSyncFailureAction;
		private System.Windows.Forms.CheckBox cbSyncHTTPS;
		private System.Windows.Forms.CheckBox cbSyncMuteWhenOff;
		private System.Windows.Forms.CheckBox cbAllowLocalOverride;
		private System.Windows.Forms.NumericUpDown nudInputsRequiredToWake;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtCommandsOn;
		private System.Windows.Forms.TextBox txtCommandsOff;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox txtCommandsOffAfterDelay;
		private System.Windows.Forms.Button btnManageHotkeys;
	}
}

