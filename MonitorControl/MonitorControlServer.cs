using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using BPUtil;
using BPUtil.SimpleHttp;

namespace MonitorControl
{
	public class MonitorControlServer : HttpServer
	{
		Thread MonitorOffTimerThread;
		private string currentMonitorStatus = "on";
		public MonitorControlServer(int port, int httpsPort = -1, X509Certificate2 cert = null) : base(port, httpsPort, cert)
		{
		}

		public override void handleGETRequest(HttpProcessor p)
		{
			if (!Program.settings.IpIsWhitelisted(p.RemoteIPAddressStr))
			{
				p.writeFailure("403 Forbidden");
				return;
			}
			string successMessage = "<div>" + p.requestedPage + " command successful</div>";
			List<KeyValuePair<string, string>> additionalHeaders = new List<KeyValuePair<string, string>>();
			additionalHeaders.Add(new KeyValuePair<string, string>("Access-Control-Allow-Origin", "*"));
			if (p.requestedPage == "on")
			{
				On(p);
			}
			else if (p.requestedPage == "off")
			{
				int idleTimeMs = p.GetIntParam("ifidle", 0);
				if (idleTimeMs <= 0 || BPUtil.NativeWin.LastInput.GetLastInputAgeMs() >= idleTimeMs)
				{
					currentMonitorStatus = "off";
					SetMonitorInState(2);
				}
			}
			else if (p.requestedPage == "off_if_idle")
			{
				OffIfIdle();
			}
			else if (p.requestedPage == "standby")
			{
				currentMonitorStatus = "off";
				SetMonitorInState(1);
			}
			else if (p.requestedPage == "status")
			{
				p.writeSuccess("text/plain", additionalHeaders: additionalHeaders);
				p.outputStream.Write(currentMonitorStatus);
				return;
			}
			else if (p.requestedPage == "idle")
			{
				p.writeSuccess("text/plain", additionalHeaders: additionalHeaders);
				p.outputStream.Write(BPUtil.NativeWin.LastInput.GetLastInputAgeMs());
				return;
			}
			else if (p.requestedPage == "smarttoggle")
			{
				if (currentMonitorStatus == "on")
				{
					OffIfIdle();
				}
				else
				{
					On(p);
				}
			}
			else if (p.requestedPage == "cancel")
			{
				StopMonitorOffThread();
			}
			else
				successMessage = "";

			p.writeSuccess(additionalHeaders: additionalHeaders);
			p.outputStream.Write("<html><head><title>Monitor Control Service</title></head>"
				+ "<style type=\"text/css\">"
				+ " table { border-collapse: collapse; }"
				+ " th, td { border: 1px solid black; padding: 3px 5px; }"
				+ "</style>"
				+ "<body>"
				+ "<p class=\"result\">" + successMessage + "</p>"
				+ "<table>"
				//+ "<thead>"
				//+ "<tr><th></th><th></th></tr>"
				//+ "</thead>"
				+ "<tbody>"
				+ BuildRow("on", "turn displays on")
				+ BuildRow("on?offAfterSecs=15", "turn displays on, then after 15 seconds, off")
				+ BuildRow("cancel", "cancel a scheduled monitor off command (see above)")
				+ BuildRow("off", "turn displays off")
				+ BuildRow("off?ifidle=3000", "turn displays off if idle for 3000ms")
				+ BuildRow("off_if_idle", "turn displays off if idle for the configured idle time (" + Program.settings.idleTimeMs + " ms)")
				+ BuildRow("standby", "change displays to standby state -- probably does nothing")
				+ BuildRow("status", "return the current status (\"on\" or \"off\")")
				+ BuildRow("idle", "return the time in milliseconds since the last user input")
				+ BuildRow("smarttoggle", "if status is \"on\" then <b>off_if_idle</b>, else <b>on</b>")
				+ "</tbody>"
				+ "</table>"
				+ "</body>"
				+ "</html>");
		}
		private string BuildRow(string link, string description)
		{
			return "<tr><td><a href=\"" + link + "\">" + link + "</a></td><td>" + description + "</td></tr>";
		}

		private void On(HttpProcessor p)
		{
			currentMonitorStatus = "on";
			SetMonitorInState(-1);
			int offAfterSecs = p.GetIntParam("offAfterSecs", 0);
			if (offAfterSecs > 0)
			{
				lock (this)
				{
					StopMonitorOffThread();
					StartMonitorOffThread(offAfterSecs);
				}
			}
		}

		private void OffIfIdle()
		{
			if (BPUtil.NativeWin.LastInput.GetLastInputAgeMs() >= Program.settings.idleTimeMs)
			{
				currentMonitorStatus = "off";
				SetMonitorInState(2);
			}
		}

		public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
		{
		}

		protected override void stopServer()
		{
			StopMonitorOffThread();
		}

		private void StopMonitorOffThread()
		{
			try
			{
				if (MonitorOffTimerThread != null && MonitorOffTimerThread.IsAlive)
					MonitorOffTimerThread.Abort();
			}
			catch (Exception) { }
		}
		private void StartMonitorOffThread(int offAfterSecs)
		{
			try
			{
				MonitorOffTimerThread = new Thread(MonitorOffProcedure);
				MonitorOffTimerThread.Start(offAfterSecs);
			}
			catch (Exception) { }
		}

		private void MonitorOffProcedure(object arg)
		{
			try
			{
				Thread.Sleep((int)arg * 1000);
				currentMonitorStatus = "off";
				SetMonitorInState(2);
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex)
			{
				Logger.Debug(ex);
			}
		}
		private static void SetMonitorInState(int state)
		{
			Console.WriteLine(state);
			DefWindowProc(GetDesktopWindow(), 0x112, (IntPtr)0xF170, (IntPtr)state);
		}
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
		[DllImport("user32.dll")]
		static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);
		[DllImport("user32.dll", SetLastError = false)]
		static extern IntPtr GetDesktopWindow();
	}
}
