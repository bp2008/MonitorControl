using BPUtil;
using BPUtil.NativeWin;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
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
using Action = System.Action;

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
				cbStartAutomatically.Checked = CheckAutomaticStartup();
			}
			catch (Exception)
			{
				MessageBox.Show("Insufficient permission to access Task Scheduler.");
			}

			nudHttpPort.Value = Program.settings.http_port;
			nudHttpsPort.Value = Program.settings.https_port;
			nudIdleMs.Value = Program.settings.idleTimeMs;
			cbLogRequests.Checked = Program.settings.logWebserverRequests;
			nudProgressBarLength.Value = Program.settings.partialWakeMax;
			nudProgressBarStart.Value = Program.settings.partialWakeStart;
			nudInputWakefulnessStrength.Value = Program.settings.inputWakefulnessStrength;
			txtIpWhitelist.Text = Program.settings.GetIpWhitelistString();
			txtSyncAddress.Text = Program.settings.syncAddress;
			nudSyncPort.Value = Program.settings.syncPort;
			ddlSyncFailureAction.SelectedIndex = BPMath.Clamp(Program.settings.syncFailureAction, 0, 2);
			cbSyncHTTPS.Checked = Program.settings.syncHTTPS;
			cbSyncMuteWhenOff.Checked = Program.settings.syncMute;
			cbAllowLocalOverride.Checked = Program.settings.syncAllowLocalOverride;
			txtCommandsOff.Text = Settings.FixStoredString(Program.settings.commandsOff);
			txtCommandsOffAfterDelay.Text = Settings.FixStoredString(Program.settings.commandsOffAfterDelay);
			txtCommandsOn.Text = Settings.FixStoredString(Program.settings.commandsOn);
			nudDisplayIdleTimeout.Value = Program.settings.displayIdleTimeoutSeconds.Clamp(0, uint.MaxValue);

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
				lblCurrentHttp.Text = "Active Ports: " + string.Join(",", Program.service.GetBindings().Select(b => b.Endpoint.Port));
			}
		}

		private void nudHttpPort_ValueChanged(object sender, EventArgs e)
		{
			int old = Program.settings.http_port;
			Program.settings.http_port = (int)nudHttpPort.Value;
			if (old != Program.settings.http_port)
			{
				Program.settings.Save();
				Program.service.BindHttpServerSockets();
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
				Program.service.BindHttpServerSockets();
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

		private void cbLogRequests_CheckedChanged(object sender, EventArgs e)
		{
			bool old = Program.settings.logWebserverRequests;
			Program.settings.logWebserverRequests = cbLogRequests.Checked;
			if (old != Program.settings.logWebserverRequests)
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

		private void cbAllowLocalOverride_CheckedChanged(object sender, EventArgs e)
		{
			bool old = Program.settings.syncAllowLocalOverride;
			Program.settings.syncAllowLocalOverride = cbAllowLocalOverride.Checked;
			if (old != Program.settings.syncAllowLocalOverride)
				Program.settings.Save();
		}

		private void nudProgressBarLength_ValueChanged(object sender, EventArgs e)
		{
			int old = Program.settings.partialWakeMax;
			Program.settings.partialWakeMax = (int)nudProgressBarLength.Value;
			if (old != Program.settings.partialWakeMax)
			{
				EnforcePartialWakeProgressBarLimits();
				Program.settings.Save();
			}
		}

		private void nudProgressBarStart_ValueChanged(object sender, EventArgs e)
		{
			int old = Program.settings.partialWakeStart;
			Program.settings.partialWakeStart = (int)nudProgressBarStart.Value;
			if (old != Program.settings.partialWakeStart)
			{
				EnforcePartialWakeProgressBarLimits();
				Program.settings.Save();
			}
		}

		private void EnforcePartialWakeProgressBarLimits()
		{
			if (nudProgressBarStart.Value >= nudProgressBarLength.Value)
				nudProgressBarStart.Value = (int)nudProgressBarLength.Value - 1;
		}

		private void nudInputsRequiredToWake_ValueChanged(object sender, EventArgs e)
		{
			int old = Program.settings.inputWakefulnessStrength;
			Program.settings.inputWakefulnessStrength = (int)nudInputWakefulnessStrength.Value;
			if (old != Program.settings.inputWakefulnessStrength)
				Program.settings.Save();
		}

		private void txtCommandsOff_TextChanged(object sender, EventArgs e)
		{
			string old = Program.settings.commandsOff;
			Program.settings.commandsOff = txtCommandsOff.Text;
			if (old != Program.settings.commandsOff)
				Program.settings.Save();
		}
		private void txtCommandsOffAfterDelay_TextChanged(object sender, EventArgs e)
		{
			string old = Program.settings.commandsOffAfterDelay;
			Program.settings.commandsOffAfterDelay = txtCommandsOffAfterDelay.Text;
			if (old != Program.settings.commandsOffAfterDelay)
				Program.settings.Save();
		}

		private void txtCommandsOn_TextChanged(object sender, EventArgs e)
		{
			string old = Program.settings.commandsOn;
			Program.settings.commandsOn = txtCommandsOn.Text;
			if (old != Program.settings.commandsOn)
				Program.settings.Save();
		}

		private void btnOpenDataFolder_Click(object sender, EventArgs e)
		{
			Process.Start(Globals.WritableDirectoryBase);
		}

		private void btnOpenWebInterface_Click(object sender, EventArgs e)
		{
			BPUtil.SimpleHttp.HttpServer.Binding binding = Program.service.GetBindings().FirstOrDefault(b => b.AllowedConnectionTypes.HasFlag(BPUtil.SimpleHttp.AllowedConnectionTypes.https));
			if (binding != null)
			{
				Process.Start("https://127.0.0.1" + (binding.Endpoint.Port == 443 ? "" : (":" + binding.Endpoint.Port)) + "/");
				return;
			}

			binding = Program.service.GetBindings().FirstOrDefault(b => b.AllowedConnectionTypes.HasFlag(BPUtil.SimpleHttp.AllowedConnectionTypes.http));
			if (binding != null)
			{
				Process.Start("https://127.0.0.1" + (binding.Endpoint.Port == 80 ? "" : (":" + binding.Endpoint.Port)) + "/");
				return;
			}

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
				bool isStartingAutomatically = CheckAutomaticStartup();
				if (cbStartAutomatically.Checked != isStartingAutomatically)
				{
					if (cbStartAutomatically.Checked)
					{
						CreateStartupTask();
					}
					else
					{
						DeleteStartupTask();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		public static void MigrateLegacyAutostart()
		{
			RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

			object value = rk.GetValue(Globals.AssemblyName);

			if (value != null && (value is string))
			{
				CreateStartupTask();
				rk.DeleteValue(Globals.AssemblyName, false);
			}
		}
		private const string TaskName = "MonitorControl Automatic Startup";
		private static void CreateStartupTask()
		{
			if (CheckAutomaticStartup())
				DeleteStartupTask();
			using (TaskService ts = new TaskService())
			{
				TaskDefinition td = ts.NewTask();
				td.RegistrationInfo.Description = "Start the MonitorControl tray application as administrator.";
				td.Triggers.Add(new LogonTrigger());
				td.Actions.Add(new ExecAction(Globals.ApplicationDirectoryBase + Globals.ExecutableNameWithExtension, null, Globals.ApplicationRoot));
				td.Principal.RunLevel = TaskRunLevel.Highest;
				td.Settings.AllowDemandStart = true;
				td.Settings.DisallowStartIfOnBatteries = false;
				td.Settings.ExecutionTimeLimit = TimeSpan.Zero;
				td.Settings.Hidden = false;
				td.Settings.RestartCount = 1440;
				td.Settings.RestartInterval = TimeSpan.FromMinutes(1);
				td.Settings.RunOnlyIfIdle = false;
				td.Settings.RunOnlyIfNetworkAvailable = false;
				td.Settings.StartWhenAvailable = true;
				td.Settings.StopIfGoingOnBatteries = false;
				td.Settings.Volatile = false;

				ts.RootFolder.RegisterTaskDefinition(TaskName, td);
			}
		}
		private static void DeleteStartupTask()
		{
			using (TaskService ts = new TaskService())
			{
				if (ts.RootFolder.Tasks.Any(t => t.Name == TaskName))
					ts.RootFolder.DeleteTask(TaskName);
			}
		}
		/// <summary>
		/// Returns true if the program is configured to start automatically.
		/// </summary>
		/// <returns></returns>
		private static bool CheckAutomaticStartup()
		{
			using (TaskService ts = new TaskService())
			{
				return ts.RootFolder.Tasks.Any(t => t.Name == TaskName);
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnManageHotkeys_Click(object sender, EventArgs e)
		{
			HotkeyForm hkForm = new HotkeyForm();
			hkForm.ShowDialog();
		}

		private void nudDisplayIdleTimeout_ValueChanged(object sender, EventArgs e)
		{
			if (Program.settings.displayIdleTimeoutSeconds != (int)nudDisplayIdleTimeout.Value)
			{
				Program.settings.displayIdleTimeoutSeconds = (int)nudDisplayIdleTimeout.Value;
				Program.settings.Save();
				if (Program.settings.displayIdleTimeoutSeconds >= 0)
				{
					WinSleep.SetMonitorTimeoutSeconds_AC((uint)Program.settings.displayIdleTimeoutSeconds.Clamp(0, uint.MaxValue));
				}
			}
		}
	}
}
