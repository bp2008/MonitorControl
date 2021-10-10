using BPUtil;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorControl
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			this.Text += " " + Globals.AssemblyVersion;

			try
			{
				cbStartAutomatically.Checked = GetStartup();
			}
			catch (Exception)
			{
				MessageBox.Show("Insufficient permission to access registry.");
			}

			nudHttpPort.Value = Program.settings.http_port;
			nudHttpsPort.Value = Program.settings.https_port;
			nudIdleMs.Value = Program.settings.idleTimeMs;
			txtIpWhitelist.Text = Program.settings.GetIpWhitelistString();
			txtSyncAddress.Text = Program.settings.syncAddress;
			nudSyncPort.Value = Program.settings.syncPort;
			ddlSyncFailureAction.SelectedIndex = BPMath.Clamp(Program.settings.syncFailureAction, 0, 2);
			cbSyncHTTPS.Checked = Program.settings.syncHTTPS;
			cbSyncMuteWhenOff.Checked = Program.settings.syncMute;

			SetCurrentHttpPorts();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Program.service.SocketBound += Service_SocketBound;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Program.service.SocketBound -= Service_SocketBound;
		}

		private void Service_SocketBound(object sender, string e)
		{
			SetCurrentHttpPorts();
		}
		private void SetCurrentHttpPorts()
		{
			if (lblCurrentHttp.InvokeRequired)
				lblCurrentHttp.Invoke((Action)SetCurrentHttpPorts);
			else
			{
				lblCurrentHttp.Text = "Active Ports: " + Program.service.http_port + ", " + Program.service.https_port;
			}
		}

		private void nudHttpPort_ValueChanged(object sender, EventArgs e)
		{
			int old = Program.settings.http_port;
			Program.settings.http_port = (int)nudHttpPort.Value;
			if (old != Program.settings.http_port)
			{
				Program.settings.Save();
				Program.service.Start();
				SetTimeout.OnGui(SetCurrentHttpPorts, 100, this);
			}
		}

		private void nudHttpsPort_ValueChanged(object sender, EventArgs e)
		{
			int old = Program.settings.https_port;
			Program.settings.https_port = (int)nudHttpsPort.Value;
			if (old != Program.settings.https_port)
			{
				Program.settings.Save();
				Program.service.Start();
				SetTimeout.OnGui(SetCurrentHttpPorts, 100, this);
			}
		}

		private void nudIdleMs_ValueChanged(object sender, EventArgs e)
		{
			int old = Program.settings.idleTimeMs;
			Program.settings.idleTimeMs = (int)nudIdleMs.Value;
			if (old != Program.settings.idleTimeMs)
				Program.settings.Save();
		}

		private void txtSyncAddress_TextChanged(object sender, EventArgs e)
		{
			string old = Program.settings.syncAddress;
			Program.settings.syncAddress = txtSyncAddress.Text;
			if (old != Program.settings.syncAddress)
				Program.settings.Save();
		}

		private void nudSyncPort_ValueChanged(object sender, EventArgs e)
		{
			int old = Program.settings.syncPort;
			Program.settings.syncPort = (int)nudSyncPort.Value;
			if (old != Program.settings.syncPort)
				Program.settings.Save();
		}

		private void txtIpWhitelist_TextChanged(object sender, EventArgs e)
		{
			string old = Program.settings.GetIpWhitelistString();
			Program.settings.ip_whitelist = txtIpWhitelist.Text;
			if (old != Program.settings.GetIpWhitelistString())
				Program.settings.Save();
		}

		private void ddlSyncFailureAction_SelectedIndexChanged(object sender, EventArgs e)
		{
			int old = Program.settings.syncFailureAction;
			Program.settings.syncFailureAction = ddlSyncFailureAction.SelectedIndex;
			if (old != Program.settings.syncFailureAction)
				Program.settings.Save();
		}

		private void cbSyncHTTPS_CheckedChanged(object sender, EventArgs e)
		{
			bool old = Program.settings.syncHTTPS;
			Program.settings.syncHTTPS = cbSyncHTTPS.Checked;
			if (old != Program.settings.syncHTTPS)
				Program.settings.Save();
		}
		private void cbSyncMuteWhenOff_CheckedChanged(object sender, EventArgs e)
		{
			bool old = Program.settings.syncMute;
			Program.settings.syncMute = cbSyncMuteWhenOff.Checked;
			if (old != Program.settings.syncMute)
				Program.settings.Save();
		}

		private void btnOpenDataFolder_Click(object sender, EventArgs e)
		{
			Process.Start(Globals.WritableDirectoryBase);
		}

		private void btnOpenWebInterface_Click(object sender, EventArgs e)
		{
			if (Program.service.https_port > 0)
				Process.Start("https://127.0.0.1" + (Program.service.https_port == 443 ? "" : (":" + Program.service.https_port)) + "/");
			else if (Program.service.http_port > 0)
				Process.Start("http://127.0.0.1" + (Program.service.http_port == 80 ? "" : (":" + Program.service.http_port)) + "/");
			else
				MessageBox.Show("The web server is not active. Please change the port number(s) above.");
		}

		private void btnExitProgram_Click(object sender, EventArgs e)
		{
			Program.Exit();
		}

		private void cbStartAutomatically_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				SetStartup(cbStartAutomatically.Checked);
			}
			catch (Exception)
			{
				MessageBox.Show("Insufficient permission to access registry.");
			}
		}

		private void SetStartup(bool startAutomatically)
		{
			RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

			if (startAutomatically)
				rk.SetValue(Globals.AssemblyName, Application.ExecutablePath);
			else
				rk.DeleteValue(Globals.AssemblyName, false);
		}

		private bool GetStartup()
		{
			RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);

			object value = rk.GetValue(Globals.AssemblyTitle);

			if (value != null && (value is string) && Application.ExecutablePath.Equals((string)value, StringComparison.OrdinalIgnoreCase))
				return true;
			else
				return false;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnInterceptionInstall_Click(object sender, EventArgs e)
		{
			if (!BPUtil.NativeWin.Admin.StartAsAdmin("cmd", "/C \"" + Globals.ApplicationDirectoryBase + "install-interception.exe\" /install & pause", Globals.ApplicationDirectoryBase))
			{
				MessageBox.Show("Failed to run interception installer. Please run \"install-interception.exe /install\" from an elevated command prompt.");
			}
		}

		private void btnInterceptionUninstall_Click(object sender, EventArgs e)
		{
			if (!BPUtil.NativeWin.Admin.StartAsAdmin("cmd", "/C \"" + Globals.ApplicationDirectoryBase + "install-interception.exe\" /uninstall & pause", Globals.ApplicationDirectoryBase))
			{
				MessageBox.Show("Failed to run interception uninstaller. Please run \"install-interception.exe /uninstall\" from an elevated command prompt.");
			}
		}
	}
}
