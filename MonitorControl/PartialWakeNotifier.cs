using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BPUtil;

namespace MonitorControl
{
	public partial class PartialWakeNotifier : Form
	{
		SetTimeout.TimeoutHandle resetTopTimeout;
		SetTimeout.TimeoutHandle resetTop2Timeout;
		/// <summary>
		/// Local variable to hold the current progress so that it can be returned synchronously to any thread.
		/// </summary>
		int progress = 0;
		public PartialWakeNotifier()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the displayed progress from 0 to 100.
		/// </summary>
		public int Progress
		{
			get
			{
				return progress;
			}
			set
			{
				SetProgress(value.Clamp(0, 100));
			}
		}

		private void SetProgress(int p)
		{ 
			progress = p;
			if (InvokeRequired)
				Invoke((Action<int>)SetProgress, p);
			else
			{
				progressBar1.SetProgressNoAnimation(p);
			}
		}

		public void Terminate()
		{
			if (InvokeRequired)
				Invoke((Action)Terminate);
			else
			{
				this.Close();
			}
		}
		public void SetSecondsRemaining(int seconds)
		{
			if (InvokeRequired)
				Invoke((Action<int>)SetSecondsRemaining, seconds);
			else
			{
				lblRemaining.Text = seconds + " seconds remaining";
			}
		}
		private void PartialWakeNotifier_Load(object sender, EventArgs e)
		{
			SetWindowOnTop();
			resetTopTimeout = SetTimeout.OnGui(SetWindowOnTop, 1000, this);
			resetTop2Timeout = SetTimeout.OnGui(SetWindowOnTop, 2000, this);
		}
		private void SetWindowOnTop()
		{
			this.WindowState = FormWindowState.Minimized;
			this.Show();
			this.WindowState = FormWindowState.Normal;
		}

		private void PartialWakeNotifier_FormClosing(object sender, FormClosingEventArgs e)
		{
			Try.Catch(() =>
			{
				resetTopTimeout?.Cancel();
			});
			Try.Catch(() =>
			{
				resetTop2Timeout?.Cancel();
			});
		}
	}
}
