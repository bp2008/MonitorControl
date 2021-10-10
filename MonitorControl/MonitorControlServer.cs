using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using BPUtil;
using BPUtil.NativeWin;
using BPUtil.NativeWin.AudioController;
using BPUtil.SimpleHttp;

namespace MonitorControl
{
	public class MonitorControlServer : HttpServer
	{
		private static Thread MonitorOffTimerThread;
		private static Thread thrWaitForWake;
		private static string currentMonitorStatus = "on";
		private static object myLock = new object();
		private static bool didMute = false;
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
				int delayMs = p.GetIntParam("delay", 0);
				Action setOff = () =>
				{
					if (idleTimeMs <= 0 || LastInput.GetLastInputAgeMs() >= idleTimeMs)
					{
						Off(p.GetBoolParam("mute"));
					}
				};
				if (delayMs > 0)
				{
					SetTimeout.OnBackground(setOff, delayMs);
				}
				else
					setOff();
			}
			else if (p.requestedPage == "off_if_idle")
			{
				OffIfIdle(Program.settings.idleTimeMs, p.GetBoolParam("mute"));
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
				p.outputStream.Write(LastInput.GetLastInputAgeMs());
				return;
			}
			else if (p.requestedPage == "smarttoggle")
			{
				if (currentMonitorStatus == "on")
				{
					OffIfIdle(Program.settings.idleTimeMs, p.GetBoolParam("mute"));
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
			else if (p.requestedPage == "getvolume")
			{
				int level = 0;
				try
				{
					level = (int)Math.Round(AudioManager.GetMasterVolume());
					//level = Audio.GetVolume();
				}
				catch (Exception ex)
				{
					p.writeSuccess("text/plain; charset=utf-8", responseCode: "500 Internal Server Error");
					p.outputStream.Write(ex.ToString());
					return;
				}
				p.writeSuccess("text/plain");
				p.outputStream.Write(level);
			}
			else if (p.requestedPage == "setvolume")
			{
				int level = p.GetIntParam("level");
				level = level.Clamp(0, 100);
				try
				{
					AudioManager.SetMasterVolume(level);
					//Audio.SetVolume(level);
				}
				catch (Exception ex)
				{
					p.writeSuccess("text/plain; charset=utf-8", responseCode: "500 Internal Server Error");
					p.outputStream.Write(ex.ToString());
					return;
				}
				p.writeSuccess("text/plain");
				p.outputStream.Write(level);
			}
			else if (p.requestedPage == "mute")
			{
				try
				{
					AudioManager.SetMasterVolumeMute(true);
					//Audio.SetMute(true);
				}
				catch (Exception ex)
				{
					p.writeSuccess("text/plain; charset=utf-8", responseCode: "500 Internal Server Error");
					p.outputStream.Write(ex.ToString());
					return;
				}
				p.writeSuccess("text/plain");
				p.outputStream.Write("muted");
			}
			else if (p.requestedPage == "unmute")
			{
				try
				{
					AudioManager.SetMasterVolumeMute(false);
					//Audio.SetMute(false);
				}
				catch (Exception ex)
				{
					p.writeSuccess("text/plain; charset=utf-8", responseCode: "500 Internal Server Error");
					p.outputStream.Write(ex.ToString());
					return;
				}
				p.writeSuccess("text/plain");
				p.outputStream.Write("unmuted");
			}
			else if (p.requestedPage == "getmute")
			{
				bool muted = false;
				try
				{
					muted = AudioManager.GetMasterVolumeMute();
					//muted = Audio.GetMute();
				}
				catch (Exception ex)
				{
					p.writeSuccess("text/plain; charset=utf-8", responseCode: "500 Internal Server Error");
					p.outputStream.Write(ex.ToString());
					return;
				}
				p.writeSuccess("text/plain");
				p.outputStream.Write(muted ? "muted" : "unmuted");
			}
			else if (p.requestedPage == "mousemove")
			{
				int dx = p.GetIntParam("dx"); // x change
				int dy = p.GetIntParam("dy"); // y change
				int delay = p.GetIntParam("delay").Clamp(1, 200); // Approximate milliseconds between movements.
				int times = p.GetIntParam("times").Clamp(1, 5000 / delay); // Number of times to perform the change.  Limited to about 5 seconds of movement.
				try
				{
					DragMouse(dx, dy, delay, times);
				}
				catch (Exception ex)
				{
					p.writeSuccess("text/plain; charset=utf-8", responseCode: "500 Internal Server Error");
					p.outputStream.Write(ex.ToString());
					return;
				}
				p.writeSuccess("text/plain");
				p.outputStream.Write("move complete. moved " + dx + "," + dy + " " + times + " times with " + delay + " ms delay");
			}
			else
				successMessage = "";

			if (p.responseWritten)
				return;

			p.writeSuccess(additionalHeaders: additionalHeaders);
			p.outputStream.Write("<html><head><title>Monitor Control Service</title></head>"
				+ "<style type=\"text/css\">"
				+ " table { border-collapse: collapse; }"
				+ " th, td { border: 1px solid black; padding: 3px 5px; }"
				+ "</style>"
				+ "<body>"
				+ "<p class=\"result\">" + successMessage + "</p>"
				+ "<p class=\"syncStatus\">(Remote server \"" + Program.settings.syncAddress + "\") " + HttpUtility.HtmlEncode(MonitorControlService.syncedServerStatus) + "</p>"
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
				+ BuildRow("off?delay=3000&ifidle=2900", "wait 3000ms, then turn displays off if idle for 2900ms")
				+ BuildRow("off?delay=3000&ifidle=2900&mute=1", "wait 3000ms, then turn displays off if idle for 2900ms, and also mute until the computer is no longer idle")
				+ BuildRow("off_if_idle", "turn displays off if idle for the configured idle time (" + Program.settings.idleTimeMs + " ms)")
				+ BuildRow("off_if_idle?mute=1", "turn displays off if idle for the configured idle time (" + Program.settings.idleTimeMs + " ms). Also mute until the computer is no longer idle.")
				+ BuildRow("standby", "change displays to standby state -- probably does nothing")
				+ BuildRow("status", "return the current status (\"on\" or \"off\")")
				+ BuildRow("idle", "return the time in milliseconds since the last user input")
				+ BuildRow("smarttoggle", "if status is \"on\" then <b>off_if_idle</b>, else <b>on</b>. Also supports the <b>mute</b> boolean argument.")
				+ BuildRow("getvolume", "returns default audio device volume from 0 to 100")
				+ BuildRow("setvolume?level=10", "sets default audio device volume from 0 to 100")
				+ BuildRow("mute", "mutes default audio device")
				+ BuildRow("unmute", "unmutes default audio device")
				+ BuildRow("getmute", "returns \"muted\" or \"unmuted\"")
				+ BuildRow("mousemove?dx=2&dy=-2&delay=4&times=5", "moves the mouse cursor up 2px and to the right 2px, 5 times, waiting 4ms between each movement. Max delay 200ms. Max 5 seconds movement per command.")
				+ "</tbody>"
				+ "</table>"
				+ "</body>"
				+ "</html>");
		}

		public static void DragMouse(int dx, int dy, int delay, int times)
		{
			Stopwatch sw = Stopwatch.StartNew();
			EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.ManualReset);
			for (int i = 0; i < times; i++)
			{
				int nextClick = i * delay;
				int waitFor = nextClick - (int)sw.ElapsedMilliseconds;
				if (waitFor > 0)
					ewh.WaitOne(waitFor);

				Mouse.MoveCursor(dx, dy);
			}
		}

		private string BuildRow(string link, string description)
		{
			return "<tr><td><a href=\"" + StringUtil.HtmlAttributeEncode(link) + "\">" + HttpUtility.HtmlEncode(link) + "</a></td><td>" + description + "</td></tr>";
		}

		public static void On(HttpProcessor p)
		{
			UnmuteIfNeeded();
			currentMonitorStatus = "on";
			SetMonitorInState(-1);
			lock (myLock)
			{
				StopMonitorOffThread();
				StopWaitForWakeThread();
				if (p != null)
				{
					int offAfterSecs = p.GetIntParam("offAfterSecs", 0);
					if (offAfterSecs > 0)
					{
						StartMonitorOffThread(offAfterSecs);
					}
				}
			}
			SetTimeout.OnBackground(() =>
			{
				//Cursor.Show();
				//IntPtr desktopHandle = NativeMethods.GetDesktopWindow();
				//if (desktopHandle != null && desktopHandle != IntPtr.Zero)
				//	NativeMethods.SetForegroundWindow(desktopHandle);
				int dx = Cursor.Position.X < 10 ? 1 : -1;
				int dy = Cursor.Position.Y < 10 ? 1 : -1;
				DragMouse(dx, dy, 1, 1);
				DragMouse(-dx, -dy, 1, 1);
			}, 0);
		}
		public static void OffIfIdle(int idleTimeMs, bool mute)
		{
			if (LastInput.GetLastInputAgeMs() >= idleTimeMs)
			{
				Off(mute);
			}
		}
		public static void Off(bool mute)
		{
			UnmuteIfNeeded();

			currentMonitorStatus = "off";
			SetMonitorInState(2);

			uint lastInputAge = LastInput.GetLastInputAgeMs();
			lock (myLock)
			{
				didMute = false;
				if (mute && !AudioManager.GetMasterVolumeMute())
				{
					AudioManager.SetMasterVolumeMute(true);
					didMute = true;
				}
				StopMonitorOffThread();
				StopWaitForWakeThread();
				StartWaitForWakeThread(new { lastInputAge });
			}
		}

		public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
		{
		}

		protected override void stopServer()
		{
			UnmuteIfNeeded();
			StopMonitorOffThread();
			StopWaitForWakeThread();
		}

		private static void StopMonitorOffThread()
		{
			try
			{
				if (MonitorOffTimerThread != null && MonitorOffTimerThread.IsAlive)
					MonitorOffTimerThread.Abort();
			}
			catch (Exception ex)
			{
				Logger.Debug(ex);
			}
		}
		private static void StartMonitorOffThread(int offAfterSecs)
		{
			try
			{
				MonitorOffTimerThread = new Thread(MonitorOffProcedure);
				MonitorOffTimerThread.IsBackground = true;
				MonitorOffTimerThread.Name = "Turn off monitor after delay";
				MonitorOffTimerThread.Start(offAfterSecs);
			}
			catch (Exception ex)
			{
				Logger.Debug(ex);
			}
		}

		private static void MonitorOffProcedure(object arg)
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
		private static void StopWaitForWakeThread()
		{
			try
			{
				if (thrWaitForWake != null && thrWaitForWake.IsAlive)
					thrWaitForWake.Abort();
			}
			catch (Exception ex)
			{
				Logger.Debug(ex);
			}
		}
		private static void StartWaitForWakeThread(object arg)
		{
			try
			{
				thrWaitForWake = new Thread(WaitForWakeProcedure);
				thrWaitForWake.IsBackground = true;
				thrWaitForWake.Name = "Wait for wake";
				thrWaitForWake.Start(arg);
			}
			catch (Exception ex)
			{
				Logger.Debug(ex);
			}
		}
		private static void WaitForWakeProcedure(object arg)
		{
			try
			{
				dynamic args = arg;
				uint lastInputAge = args.lastInputAge;
				// In theory the last input counter will roll over after 49.7102696 days. Or maybe it'll just stop incrementing?  Either way, lets hope the PC doesn't stay idle that long.
				while (true)
				{
					uint inputAge = LastInput.GetLastInputAgeMs();
					if (inputAge < lastInputAge)
						break;
					lastInputAge = inputAge;
					Thread.Sleep(1000);
				}
				currentMonitorStatus = "on";
				UnmuteIfNeeded();
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex)
			{
				Logger.Debug(ex);
			}
		}

		private static void UnmuteIfNeeded()
		{
			if (didMute)
			{
				AudioManager.SetMasterVolumeMute(false);
				didMute = false;
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


// Attempted to use nuget package "CCDWrapper" to get monitor state (path.targetInfo.targetAvailable), but it does not reveal true monitor state on my system.
//string state = "unknown";
//try
//{
//	CCD.Enum.QueryDisplayFlags qdf = CCD.Enum.QueryDisplayFlags.DatabaseCurrent;
//	CCD.Enum.StatusCode result = CCD.Wrapper.GetDisplayConfigBufferSizes(qdf, out int numPathArrayElements, out int numModeInfoArrayElements);
//	if (result == CCD.Enum.StatusCode.Success)
//	{
//		StringBuilder sb = new StringBuilder();
//		sb.AppendLine("numPathArrayElements: " + numPathArrayElements);
//		sb.AppendLine("numModeInfoArrayElements: " + numModeInfoArrayElements);

//		CCD.Struct.DisplayConfigPathInfo[] paths = new CCD.Struct.DisplayConfigPathInfo[numPathArrayElements];
//		CCD.Struct.DisplayConfigModeInfo[] modes = new CCD.Struct.DisplayConfigModeInfo[numModeInfoArrayElements];
//		CCD.Enum.DisplayConfigTopologyId topologyId;
//		result = CCD.Wrapper.QueryDisplayConfig(qdf, ref numPathArrayElements, paths, ref numModeInfoArrayElements, modes, out topologyId);

//		if (result == CCD.Enum.StatusCode.Success)
//		{
//			sb.AppendLine("numPathArrayElements: " + numPathArrayElements);
//			sb.AppendLine("numModeInfoArrayElements: " + numModeInfoArrayElements);
//			sb.AppendLine("TopologyId: " + topologyId);

//			sb.AppendLine("Paths:");
//			foreach (CCD.Struct.DisplayConfigPathInfo path in paths.Take(numPathArrayElements))
//			{
//				sb.AppendLine("{");
//				sb.AppendLine("\tFlags: " + path.flags.GetAllMatchedFlagsStr());
//				sb.AppendLine("\tSourceInfo: " + Newtonsoft.Json.JsonConvert.SerializeObject(path.sourceInfo, Newtonsoft.Json.Formatting.None));
//				sb.AppendLine("\tTargetInfo: " + Newtonsoft.Json.JsonConvert.SerializeObject(path.targetInfo, Newtonsoft.Json.Formatting.None));
//				sb.AppendLine("}");
//			}

//			sb.AppendLine("Modes:");
//			foreach (CCD.Struct.DisplayConfigModeInfo mode in modes.Take(numModeInfoArrayElements))
//			{
//				sb.AppendLine(Newtonsoft.Json.JsonConvert.SerializeObject(mode, Newtonsoft.Json.Formatting.Indented));
//			}
//			state = sb.ToString();
//		}
//		else
//			state = "QueryDisplayConfig returned " + result;
//	}
//	else
//		state = "GetDisplayConfigBufferSizes returned " + result;
//	//muted = Audio.GetMute();
//}
//catch (Exception ex)
//{
//	p.writeSuccess("text/plain; charset=utf-8", responseCode: "500 Internal Server Error");
//	p.outputStream.Write(ex.ToString());
//	return;
//}
//p.writeSuccess("text/html; charset=utf-8");
//p.outputStream.Write("<html><head><meta http-equiv=\"refresh\" content=\"1\" /></head><body><div style=\"white-space: pre;\">");
//p.outputStream.Write(HttpUtility.HtmlEncode(state));
//p.outputStream.Write("</div></body></html>");