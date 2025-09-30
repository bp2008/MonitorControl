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
			this.nudInputWakefulnessStrength = new System.Windows.Forms.NumericUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.txtCommandsOn = new System.Windows.Forms.TextBox();
			this.txtCommandsOff = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.txtCommandsOffAfterDelay = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.nudProgressBarLength = new System.Windows.Forms.NumericUpDown();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.nudProgressBarStart = new System.Windows.Forms.NumericUpDown();
			this.label18 = new System.Windows.Forms.Label();
			this.nudDisplayIdleTimeout = new System.Windows.Forms.NumericUpDown();
			this.btnOpenDataFolder = new System.Windows.Forms.Button();
			this.btnOpenWebInterface = new System.Windows.Forms.Button();
			this.cbStartAutomatically = new System.Windows.Forms.CheckBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnManageHotkeys = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.cbLogRequests = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.nudHttpPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHttpsPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdleMs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSyncPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudInputWakefulnessStrength)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudProgressBarLength)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudProgressBarStart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudDisplayIdleTimeout)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "HTTP Port:";
			this.toolTip1.SetToolTip(this.label1, "[1-65535]\r\n\r\nIf 0, the port is dynamically selected.\r\n\r\nIf -1, the protocol is di" +
        "sabled.\r\n");
			// 
			// nudHttpPort
			// 
			this.nudHttpPort.Location = new System.Drawing.Point(97, 30);
			this.nudHttpPort.Margin = new System.Windows.Forms.Padding(4);
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
			this.nudHttpPort.Size = new System.Drawing.Size(96, 22);
			this.nudHttpPort.TabIndex = 21;
			this.toolTip1.SetToolTip(this.nudHttpPort, "[1-65535]\r\n\r\nIf 0, the port is dynamically selected.\r\n\r\nIf -1, the protocol is di" +
        "sabled.\r\n");
			this.nudHttpPort.ValueChanged += new System.EventHandler(this.nudHttpPort_ValueChanged);
			// 
			// nudHttpsPort
			// 
			this.nudHttpsPort.Location = new System.Drawing.Point(319, 30);
			this.nudHttpsPort.Margin = new System.Windows.Forms.Padding(4);
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
			this.nudHttpsPort.Size = new System.Drawing.Size(96, 22);
			this.nudHttpsPort.TabIndex = 22;
			this.toolTip1.SetToolTip(this.nudHttpsPort, "[1-65535]\r\n\r\nIf 0, the port is dynamically selected.\r\n\r\nIf -1, the protocol is di" +
        "sabled.\r\n");
			this.nudHttpsPort.ValueChanged += new System.EventHandler(this.nudHttpsPort_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(220, 32);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "HTTPS Port:";
			this.toolTip1.SetToolTip(this.label2, "[1-65535]\r\n\r\nIf 0, the port is dynamically selected.\r\n\r\nIf -1, the protocol is di" +
        "sabled.\r\n");
			// 
			// lblCurrentHttp
			// 
			this.lblCurrentHttp.AutoSize = true;
			this.lblCurrentHttp.Location = new System.Drawing.Point(8, 58);
			this.lblCurrentHttp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCurrentHttp.Name = "lblCurrentHttp";
			this.lblCurrentHttp.Size = new System.Drawing.Size(112, 16);
			this.lblCurrentHttp.TabIndex = 5;
			this.lblCurrentHttp.Text = "Active Ports: -1, -1";
			this.toolTip1.SetToolTip(this.lblCurrentHttp, "[1-65535]\r\n\r\nIf 0, the port is dynamically selected.\r\n\r\nIf -1, the protocol is di" +
        "sabled.");
			// 
			// nudIdleMs
			// 
			this.nudIdleMs.Location = new System.Drawing.Point(97, 84);
			this.nudIdleMs.Margin = new System.Windows.Forms.Padding(4);
			this.nudIdleMs.Maximum = new decimal(new int[] {
            3600000,
            0,
            0,
            0});
			this.nudIdleMs.Name = "nudIdleMs";
			this.nudIdleMs.Size = new System.Drawing.Size(107, 22);
			this.nudIdleMs.TabIndex = 23;
			this.toolTip1.SetToolTip(this.nudIdleMs, "The system is considered idle this many milliseconds after the last user input.\r\n" +
        "(Affects Web Server API commands that require \"idle\" state)");
			this.nudIdleMs.ValueChanged += new System.EventHandler(this.nudIdleMs_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(19, 86);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "Idle Time:";
			this.toolTip1.SetToolTip(this.label3, "The system is considered idle this many milliseconds after the last user input.\r\n" +
        "(Affects Web Server API commands that require \"idle\" state)");
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
			this.label4.Location = new System.Drawing.Point(212, 86);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(25, 16);
			this.label4.TabIndex = 8;
			this.label4.Text = "ms";
			this.toolTip1.SetToolTip(this.label4, "The system is considered idle this many milliseconds after the last user input.\r\n" +
        "(Affects Web Server API commands that require \"idle\" state)");
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 486);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(126, 16);
			this.label5.TabIndex = 10;
			this.label5.Text = "IP Address Whitelist";
			this.toolTip1.SetToolTip(this.label5, "Separate items with new lines, spaces, tabs, or commas.");
			// 
			// txtIpWhitelist
			// 
			this.txtIpWhitelist.Location = new System.Drawing.Point(16, 506);
			this.txtIpWhitelist.Margin = new System.Windows.Forms.Padding(4);
			this.txtIpWhitelist.Multiline = true;
			this.txtIpWhitelist.Name = "txtIpWhitelist";
			this.txtIpWhitelist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtIpWhitelist.Size = new System.Drawing.Size(421, 147);
			this.txtIpWhitelist.TabIndex = 50;
			this.toolTip1.SetToolTip(this.txtIpWhitelist, "Separate items with new lines, spaces, tabs, or commas.");
			this.txtIpWhitelist.TextChanged += new System.EventHandler(this.txtIpWhitelist_TextChanged);
			// 
			// btnExitProgram
			// 
			this.btnExitProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExitProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.btnExitProgram.Location = new System.Drawing.Point(731, 661);
			this.btnExitProgram.Margin = new System.Windows.Forms.Padding(4);
			this.btnExitProgram.Name = "btnExitProgram";
			this.btnExitProgram.Size = new System.Drawing.Size(195, 28);
			this.btnExitProgram.TabIndex = 92;
			this.btnExitProgram.Text = "Exit Program";
			this.toolTip1.SetToolTip(this.btnExitProgram, "Closes the program. If you simply close this window, the program will remain runn" +
        "ing in the system tray.");
			this.btnExitProgram.UseVisualStyleBackColor = false;
			this.btnExitProgram.Click += new System.EventHandler(this.btnExitProgram_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 27);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(61, 16);
			this.label7.TabIndex = 19;
			this.label7.Text = "Address:";
			this.toolTip1.SetToolTip(this.label7, "Host name or IP address of the server we are synchronizing with. Leave empty to d" +
        "isable sync.");
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(272, 27);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(34, 16);
			this.label8.TabIndex = 20;
			this.label8.Text = "Port:";
			this.toolTip1.SetToolTip(this.label8, "[1-65535] HTTP or HTTPS listening port on the server we are synchronizing with.");
			// 
			// nudSyncPort
			// 
			this.nudSyncPort.Location = new System.Drawing.Point(319, 25);
			this.nudSyncPort.Margin = new System.Windows.Forms.Padding(4);
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
			this.nudSyncPort.Size = new System.Drawing.Size(96, 22);
			this.nudSyncPort.TabIndex = 42;
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
			this.txtSyncAddress.Location = new System.Drawing.Point(80, 23);
			this.txtSyncAddress.Margin = new System.Windows.Forms.Padding(4);
			this.txtSyncAddress.Name = "txtSyncAddress";
			this.txtSyncAddress.Size = new System.Drawing.Size(183, 22);
			this.txtSyncAddress.TabIndex = 41;
			this.toolTip1.SetToolTip(this.txtSyncAddress, "Host name or IP address of the server we are synchronizing with. Leave empty to d" +
        "isable sync.");
			this.txtSyncAddress.TextChanged += new System.EventHandler(this.txtSyncAddress_TextChanged);
			// 
			// cbSyncHTTPS
			// 
			this.cbSyncHTTPS.AutoSize = true;
			this.cbSyncHTTPS.Location = new System.Drawing.Point(319, 58);
			this.cbSyncHTTPS.Margin = new System.Windows.Forms.Padding(4);
			this.cbSyncHTTPS.Name = "cbSyncHTTPS";
			this.cbSyncHTTPS.Size = new System.Drawing.Size(75, 20);
			this.cbSyncHTTPS.TabIndex = 44;
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
			this.ddlSyncFailureAction.Location = new System.Drawing.Point(109, 55);
			this.ddlSyncFailureAction.Margin = new System.Windows.Forms.Padding(4);
			this.ddlSyncFailureAction.Name = "ddlSyncFailureAction";
			this.ddlSyncFailureAction.Size = new System.Drawing.Size(160, 24);
			this.ddlSyncFailureAction.TabIndex = 43;
			this.toolTip1.SetToolTip(this.ddlSyncFailureAction, "Action to take if the remote server is unreachable.");
			this.ddlSyncFailureAction.SelectedIndexChanged += new System.EventHandler(this.ddlSyncFailureAction_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 59);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(85, 16);
			this.label6.TabIndex = 25;
			this.label6.Text = "On Sync Fail:";
			this.toolTip1.SetToolTip(this.label6, "Action to take if the remote server is unreachable.");
			// 
			// cbSyncMuteWhenOff
			// 
			this.cbSyncMuteWhenOff.AutoSize = true;
			this.cbSyncMuteWhenOff.Location = new System.Drawing.Point(8, 89);
			this.cbSyncMuteWhenOff.Margin = new System.Windows.Forms.Padding(4);
			this.cbSyncMuteWhenOff.Name = "cbSyncMuteWhenOff";
			this.cbSyncMuteWhenOff.Size = new System.Drawing.Size(242, 20);
			this.cbSyncMuteWhenOff.TabIndex = 45;
			this.cbSyncMuteWhenOff.Text = "Mute audio when turning off monitors";
			this.toolTip1.SetToolTip(this.cbSyncMuteWhenOff, "Mute audio when turning off monitors because of remote server sync.\r\n\r\nAudio retu" +
        "rns to previous state when monitors turn back on.");
			this.cbSyncMuteWhenOff.UseVisualStyleBackColor = true;
			this.cbSyncMuteWhenOff.CheckedChanged += new System.EventHandler(this.cbSyncMuteWhenOff_CheckedChanged);
			// 
			// cbAllowLocalOverride
			// 
			this.cbAllowLocalOverride.AutoSize = true;
			this.cbAllowLocalOverride.Location = new System.Drawing.Point(8, 117);
			this.cbAllowLocalOverride.Margin = new System.Windows.Forms.Padding(4);
			this.cbAllowLocalOverride.Name = "cbAllowLocalOverride";
			this.cbAllowLocalOverride.Size = new System.Drawing.Size(270, 20);
			this.cbAllowLocalOverride.TabIndex = 46;
			this.cbAllowLocalOverride.Text = "Allow local input to override synced state";
			this.toolTip1.SetToolTip(this.cbAllowLocalOverride, "If enabled, local inputs can un-sync this machine \r\nfrom the remote server until " +
        "the remote server \r\nhas another state change.\r\n\r\nIf disabled, the synced state i" +
        "s continuously enforced.\r\n");
			this.cbAllowLocalOverride.UseVisualStyleBackColor = true;
			this.cbAllowLocalOverride.CheckedChanged += new System.EventHandler(this.cbAllowLocalOverride_CheckedChanged);
			// 
			// nudInputWakefulnessStrength
			// 
			this.nudInputWakefulnessStrength.Location = new System.Drawing.Point(200, 87);
			this.nudInputWakefulnessStrength.Margin = new System.Windows.Forms.Padding(4);
			this.nudInputWakefulnessStrength.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.nudInputWakefulnessStrength.Name = "nudInputWakefulnessStrength";
			this.nudInputWakefulnessStrength.Size = new System.Drawing.Size(67, 22);
			this.nudInputWakefulnessStrength.TabIndex = 33;
			this.toolTip1.SetToolTip(this.nudInputWakefulnessStrength, "Each input event adds progress to the \"Partial Wake\" progress bar.\r\n\r\nIncrease th" +
        "is number to increase the rate of fill.");
			this.nudInputWakefulnessStrength.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.nudInputWakefulnessStrength.ValueChanged += new System.EventHandler(this.nudInputsRequiredToWake_ValueChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(9, 90);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(165, 16);
			this.label9.TabIndex = 61;
			this.label9.Text = "Input wakefulness strength:";
			this.toolTip1.SetToolTip(this.label9, "Each input event adds progress to the \"Partial Wake\" progress bar.\r\n\r\nIncrease th" +
        "is number to increase the rate of fill.");
			// 
			// txtCommandsOn
			// 
			this.txtCommandsOn.Location = new System.Drawing.Point(451, 439);
			this.txtCommandsOn.Margin = new System.Windows.Forms.Padding(4);
			this.txtCommandsOn.Multiline = true;
			this.txtCommandsOn.Name = "txtCommandsOn";
			this.txtCommandsOn.Size = new System.Drawing.Size(477, 214);
			this.txtCommandsOn.TabIndex = 80;
			this.toolTip1.SetToolTip(this.txtCommandsOn, resources.GetString("txtCommandsOn.ToolTip"));
			this.txtCommandsOn.TextChanged += new System.EventHandler(this.txtCommandsOn_TextChanged);
			// 
			// txtCommandsOff
			// 
			this.txtCommandsOff.Location = new System.Drawing.Point(447, 38);
			this.txtCommandsOff.Margin = new System.Windows.Forms.Padding(4);
			this.txtCommandsOff.Multiline = true;
			this.txtCommandsOff.Name = "txtCommandsOff";
			this.txtCommandsOff.Size = new System.Drawing.Size(477, 168);
			this.txtCommandsOff.TabIndex = 60;
			this.toolTip1.SetToolTip(this.txtCommandsOff, resources.GetString("txtCommandsOff.ToolTip"));
			this.txtCommandsOff.TextChanged += new System.EventHandler(this.txtCommandsOff_TextChanged);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(447, 16);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(334, 16);
			this.label11.TabIndex = 65;
			this.label11.Text = "Additional commands to run when turning monitors OFF:";
			this.toolTip1.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(447, 420);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(328, 16);
			this.label12.TabIndex = 66;
			this.label12.Text = "Additional commands to run when turning monitors ON:";
			this.toolTip1.SetToolTip(this.label12, resources.GetString("label12.ToolTip"));
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(447, 210);
			this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(479, 39);
			this.label13.TabIndex = 68;
			this.label13.Text = "Additional commands to run 9 seconds after turning monitors OFF, if state is stil" +
    "l OFF:";
			this.toolTip1.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
			// 
			// txtCommandsOffAfterDelay
			// 
			this.txtCommandsOffAfterDelay.Location = new System.Drawing.Point(451, 254);
			this.txtCommandsOffAfterDelay.Margin = new System.Windows.Forms.Padding(4);
			this.txtCommandsOffAfterDelay.Multiline = true;
			this.txtCommandsOffAfterDelay.Name = "txtCommandsOffAfterDelay";
			this.txtCommandsOffAfterDelay.Size = new System.Drawing.Size(477, 162);
			this.txtCommandsOffAfterDelay.TabIndex = 70;
			this.toolTip1.SetToolTip(this.txtCommandsOffAfterDelay, resources.GetString("txtCommandsOffAfterDelay.ToolTip"));
			this.txtCommandsOffAfterDelay.TextChanged += new System.EventHandler(this.txtCommandsOffAfterDelay_TextChanged);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(273, 90);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(99, 16);
			this.label10.TabIndex = 62;
			this.label10.Text = "(0-10; default: 3)";
			this.toolTip1.SetToolTip(this.label10, "Each input event adds progress to the \"Partial Wake\" progress bar.\r\n\r\nIncrease th" +
        "is number to increase the rate of fill.");
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(233, 26);
			this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(133, 16);
			this.label14.TabIndex = 65;
			this.label14.Text = "(seconds; default: 20)";
			this.toolTip1.SetToolTip(this.label14, "The full length of the \"Partial Wake\" progress bar, in seconds.");
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(9, 26);
			this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(132, 16);
			this.label15.TabIndex = 64;
			this.label15.Text = "Progress Bar Length:";
			this.toolTip1.SetToolTip(this.label15, "The full length of the \"Partial Wake\" progress bar, in seconds.");
			// 
			// nudProgressBarLength
			// 
			this.nudProgressBarLength.Location = new System.Drawing.Point(159, 23);
			this.nudProgressBarLength.Margin = new System.Windows.Forms.Padding(4);
			this.nudProgressBarLength.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.nudProgressBarLength.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.nudProgressBarLength.Name = "nudProgressBarLength";
			this.nudProgressBarLength.Size = new System.Drawing.Size(67, 22);
			this.nudProgressBarLength.TabIndex = 31;
			this.toolTip1.SetToolTip(this.nudProgressBarLength, "The full length of the \"Partial Wake\" progress bar, in seconds.");
			this.nudProgressBarLength.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.nudProgressBarLength.ValueChanged += new System.EventHandler(this.nudProgressBarLength_ValueChanged);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(233, 58);
			this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(133, 16);
			this.label16.TabIndex = 68;
			this.label16.Text = "(seconds; default: 10)";
			this.toolTip1.SetToolTip(this.label16, "The \"Partial Wake\" progress bar begins at this position when monitor wake is dete" +
        "cted.\r\n\r\nThis is the number of seconds after which monitors will go back to slee" +
        "p if inputs are not detected.");
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(9, 58);
			this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(119, 16);
			this.label17.TabIndex = 67;
			this.label17.Text = "Progress Bar Start:";
			this.toolTip1.SetToolTip(this.label17, "The \"Partial Wake\" progress bar begins at this position when monitor wake is dete" +
        "cted.\r\n\r\nThis is the number of seconds after which monitors will go back to slee" +
        "p if inputs are not detected.");
			// 
			// nudProgressBarStart
			// 
			this.nudProgressBarStart.Location = new System.Drawing.Point(159, 55);
			this.nudProgressBarStart.Margin = new System.Windows.Forms.Padding(4);
			this.nudProgressBarStart.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.nudProgressBarStart.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.nudProgressBarStart.Name = "nudProgressBarStart";
			this.nudProgressBarStart.Size = new System.Drawing.Size(67, 22);
			this.nudProgressBarStart.TabIndex = 32;
			this.toolTip1.SetToolTip(this.nudProgressBarStart, "The \"Partial Wake\" progress bar begins at this position when monitor wake is dete" +
        "cted.\r\n\r\nThis is the number of seconds after which monitors will go back to slee" +
        "p if inputs are not detected.");
			this.nudProgressBarStart.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.nudProgressBarStart.ValueChanged += new System.EventHandler(this.nudProgressBarStart_ValueChanged);
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(16, 451);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(193, 16);
			this.label18.TabIndex = 102;
			this.label18.Text = "Turn off display after (seconds):";
			this.toolTip1.SetToolTip(this.label18, resources.GetString("label18.ToolTip"));
			// 
			// nudDisplayIdleTimeout
			// 
			this.nudDisplayIdleTimeout.Location = new System.Drawing.Point(223, 449);
			this.nudDisplayIdleTimeout.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
			this.nudDisplayIdleTimeout.Name = "nudDisplayIdleTimeout";
			this.nudDisplayIdleTimeout.Size = new System.Drawing.Size(110, 22);
			this.nudDisplayIdleTimeout.TabIndex = 103;
			this.toolTip1.SetToolTip(this.nudDisplayIdleTimeout, resources.GetString("nudDisplayIdleTimeout.ToolTip"));
			this.nudDisplayIdleTimeout.ValueChanged += new System.EventHandler(this.nudDisplayIdleTimeout_ValueChanged);
			// 
			// btnOpenDataFolder
			// 
			this.btnOpenDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOpenDataFolder.Location = new System.Drawing.Point(16, 661);
			this.btnOpenDataFolder.Margin = new System.Windows.Forms.Padding(4);
			this.btnOpenDataFolder.Name = "btnOpenDataFolder";
			this.btnOpenDataFolder.Size = new System.Drawing.Size(195, 28);
			this.btnOpenDataFolder.TabIndex = 90;
			this.btnOpenDataFolder.Text = "Open Data Folder";
			this.btnOpenDataFolder.UseVisualStyleBackColor = true;
			this.btnOpenDataFolder.Click += new System.EventHandler(this.btnOpenDataFolder_Click);
			// 
			// btnOpenWebInterface
			// 
			this.btnOpenWebInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOpenWebInterface.Location = new System.Drawing.Point(16, 696);
			this.btnOpenWebInterface.Margin = new System.Windows.Forms.Padding(4);
			this.btnOpenWebInterface.Name = "btnOpenWebInterface";
			this.btnOpenWebInterface.Size = new System.Drawing.Size(195, 28);
			this.btnOpenWebInterface.TabIndex = 100;
			this.btnOpenWebInterface.Text = "Open Web Interface";
			this.btnOpenWebInterface.UseVisualStyleBackColor = true;
			this.btnOpenWebInterface.Click += new System.EventHandler(this.btnOpenWebInterface_Click);
			// 
			// cbStartAutomatically
			// 
			this.cbStartAutomatically.AutoSize = true;
			this.cbStartAutomatically.Location = new System.Drawing.Point(20, 15);
			this.cbStartAutomatically.Margin = new System.Windows.Forms.Padding(4);
			this.cbStartAutomatically.Name = "cbStartAutomatically";
			this.cbStartAutomatically.Size = new System.Drawing.Size(194, 20);
			this.cbStartAutomatically.TabIndex = 1;
			this.cbStartAutomatically.Text = "Start Program Automatically";
			this.cbStartAutomatically.UseVisualStyleBackColor = true;
			this.cbStartAutomatically.CheckedChanged += new System.EventHandler(this.cbStartAutomatically_CheckedChanged);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(731, 696);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(195, 28);
			this.btnOK.TabIndex = 101;
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
			this.groupBox1.Location = new System.Drawing.Point(16, 298);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox1.Size = new System.Drawing.Size(423, 149);
			this.groupBox1.TabIndex = 40;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Sync with another MonitorControl server";
			// 
			// btnManageHotkeys
			// 
			this.btnManageHotkeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnManageHotkeys.Location = new System.Drawing.Point(223, 661);
			this.btnManageHotkeys.Margin = new System.Windows.Forms.Padding(4);
			this.btnManageHotkeys.Name = "btnManageHotkeys";
			this.btnManageHotkeys.Size = new System.Drawing.Size(195, 28);
			this.btnManageHotkeys.TabIndex = 91;
			this.btnManageHotkeys.Text = "Manage Hotkeys";
			this.btnManageHotkeys.UseVisualStyleBackColor = true;
			this.btnManageHotkeys.Click += new System.EventHandler(this.btnManageHotkeys_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.label17);
			this.groupBox2.Controls.Add(this.nudProgressBarStart);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Controls.Add(this.nudProgressBarLength);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.nudInputWakefulnessStrength);
			this.groupBox2.Location = new System.Drawing.Point(16, 166);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox2.Size = new System.Drawing.Size(423, 124);
			this.groupBox2.TabIndex = 30;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Partial Wake";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.cbLogRequests);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.nudHttpPort);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.nudHttpsPort);
			this.groupBox3.Controls.Add(this.lblCurrentHttp);
			this.groupBox3.Controls.Add(this.nudIdleMs);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Location = new System.Drawing.Point(16, 43);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox3.Size = new System.Drawing.Size(423, 116);
			this.groupBox3.TabIndex = 20;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Web Server";
			// 
			// cbLogRequests
			// 
			this.cbLogRequests.AutoSize = true;
			this.cbLogRequests.Location = new System.Drawing.Point(292, 85);
			this.cbLogRequests.Margin = new System.Windows.Forms.Padding(4);
			this.cbLogRequests.Name = "cbLogRequests";
			this.cbLogRequests.Size = new System.Drawing.Size(113, 20);
			this.cbLogRequests.TabIndex = 24;
			this.cbLogRequests.Text = "Log Requests";
			this.cbLogRequests.UseVisualStyleBackColor = true;
			this.cbLogRequests.CheckedChanged += new System.EventHandler(this.cbLogRequests_CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(941, 739);
			this.Controls.Add(this.nudDisplayIdleTimeout);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.btnManageHotkeys);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.txtCommandsOff);
			this.Controls.Add(this.txtCommandsOn);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.cbStartAutomatically);
			this.Controls.Add(this.btnOpenWebInterface);
			this.Controls.Add(this.btnExitProgram);
			this.Controls.Add(this.btnOpenDataFolder);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtIpWhitelist);
			this.Controls.Add(this.txtCommandsOffAfterDelay);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MonitorControl";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.nudHttpPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHttpsPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdleMs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSyncPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudInputWakefulnessStrength)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudProgressBarLength)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudProgressBarStart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudDisplayIdleTimeout)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
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
		private System.Windows.Forms.NumericUpDown nudInputWakefulnessStrength;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtCommandsOn;
		private System.Windows.Forms.TextBox txtCommandsOff;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox txtCommandsOffAfterDelay;
		private System.Windows.Forms.Button btnManageHotkeys;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.NumericUpDown nudProgressBarStart;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.NumericUpDown nudProgressBarLength;
		private System.Windows.Forms.CheckBox cbLogRequests;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.NumericUpDown nudDisplayIdleTimeout;
	}
}

