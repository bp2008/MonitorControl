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
		/// <summary>
		/// A string that is either "on" or "off".
		/// </summary>
		public static string currentMonitorStatus { get; private set; } = "on";
		private static object myLock = new object();
		private static bool didMute = false;
		public MonitorControlServer(X509Certificate2 cert = null) : base(cert == null ? null : new SimpleCertificateSelector(cert))
		{
		}

		public override void handleGETRequest(HttpProcessor p)
		{
			try
			{
				if (!Program.settings.IpIsWhitelisted(p.RemoteIPAddressStr))
				{
					p.Response.Simple("403 Forbidden");
					return;
				}
				string successMessage = "<div>" + p.Request.Page + " command successful</div>";
				if (p.Request.Page == "on")
				{
					On(p);
				}
				else if (p.Request.Page == "off")
				{
					double partialwake = p.Request.GetDoubleParam("partialwake", 0).Clamp(0, 1);
					int idleTimeMs = p.Request.GetIntParam("ifidle", 0);
					int delayMs = p.Request.GetIntParam("delay", 0);
					bool mute = p.Request.GetBoolParam("mute");
					Action setOff = () =>
					{
						if (partialwake > 0)
						{
							PartialWakeController.BeginPartialWakeState(mute, partialwake);
						}
						else
						{
							if (idleTimeMs <= 0 || LastInput.GetLastInputAgeMs() >= idleTimeMs)
							{
								Off(mute);
							}
						}
					};
					if (delayMs > 0)
					{
						SetTimeout.OnBackground(setOff, delayMs);
					}
					else
						setOff();
				}
				else if (p.Request.Page == "off_if_idle")
				{
					OffIfIdle(Program.settings.idleTimeMs, p.Request.GetBoolParam("mute"));
				}
				else if (p.Request.Page == "standby")
				{
					OffTracker.NotifyMonitorOff();
					currentMonitorStatus = "off";
					SetMonitorInState(1);
					RunAdditionalCommandsInBackground(CommandSet.Off);
				}
				else if (p.Request.Page == "status")
				{
					p.Response.FullResponseUTF8(currentMonitorStatus, "text/plain; charset=utf-8");
					return;
				}
				else if (p.Request.Page == "idle")
				{
					p.Response.FullResponseUTF8(LastInput.GetLastInputAgeMs().ToString(), "text/plain; charset=utf-8");
					return;
				}
				else if (p.Request.Page == "smarttoggle")
				{
					if (currentMonitorStatus == "on")
					{
						OffIfIdle(Program.settings.idleTimeMs, p.Request.GetBoolParam("mute"));
					}
					else
					{
						On(p);
					}
				}
				else if (p.Request.Page == "cancel")
				{
					StopMonitorOffThread();
				}
				else if (p.Request.Page == "getvolume")
				{
					int level = 0;
					try
					{
						level = (int)Math.Round(AudioManager.GetMasterVolume());
						//level = Audio.GetVolume();
					}
					catch (Exception ex)
					{
						p.Response.Simple("500 Internal Server Error", ex.ToString());
						return;
					}
					p.Response.FullResponseUTF8(level.ToString(), "text/plain; charset=utf-8");
				}
				else if (p.Request.Page == "setvolume")
				{
					int level = p.Request.GetIntParam("level");
					level = level.Clamp(0, 100);
					try
					{
						AudioManager.SetMasterVolume(level);
						//Audio.SetVolume(level);
					}
					catch (Exception ex)
					{
						p.Response.Simple("500 Internal Server Error", ex.ToString());
						return;
					}
					p.Response.FullResponseUTF8(level.ToString(), "text/plain; charset=utf-8");
				}
				else if (p.Request.Page == "mute")
				{
					try
					{
						AudioManager.SetMasterVolumeMute(true);
						//Audio.SetMute(true);
					}
					catch (Exception ex)
					{
						p.Response.Simple("500 Internal Server Error", ex.ToString());
						return;
					}
					p.Response.FullResponseUTF8("muted", "text/plain; charset=utf-8");
				}
				else if (p.Request.Page == "unmute")
				{
					try
					{
						AudioManager.SetMasterVolumeMute(false);
						//Audio.SetMute(false);
					}
					catch (Exception ex)
					{
						p.Response.Simple("500 Internal Server Error", ex.ToString());
						return;
					}
					p.Response.FullResponseUTF8("unmuted", "text/plain; charset=utf-8");
				}
				else if (p.Request.Page == "getmute")
				{
					bool muted = false;
					try
					{
						muted = AudioManager.GetMasterVolumeMute();
						//muted = Audio.GetMute();
					}
					catch (Exception ex)
					{
						p.Response.Simple("500 Internal Server Error", ex.ToString());
						return;
					}
					p.Response.FullResponseUTF8(muted ? "muted" : "unmuted", "text/plain; charset=utf-8");
				}
				else if (p.Request.Page == "mousemove")
				{
					int dx = p.Request.GetIntParam("dx"); // x change
					int dy = p.Request.GetIntParam("dy"); // y change
					int delay = p.Request.GetIntParam("delay").Clamp(1, 200); // Approximate milliseconds between movements.
					int times = p.Request.GetIntParam("times").Clamp(1, 5000 / delay); // Number of times to perform the change.  Limited to about 5 seconds of movement.
					try
					{
						DragMouse(dx, dy, delay, times);
					}
					catch (Exception ex)
					{
						p.Response.Simple("500 Internal Server Error", ex.ToString());
						return;
					}
					p.Response.FullResponseUTF8("move complete. moved " + dx + "," + dy + " " + times + " times with " + delay + " ms delay", "text/plain; charset=utf-8");
				}
				else if (p.Request.Page == "AllowLocalOverride")
				{
					Program.settings.syncAllowLocalOverride = true;
					Program.settings.Save();
					p.Response.FullResponseUTF8("✅ Allow local input to override synced state", "text/plain; charset=utf-8");
				}
				else if (p.Request.Page == "DisallowLocalOverride")
				{
					Program.settings.syncAllowLocalOverride = false;
					Program.settings.Save();
					p.Response.FullResponseUTF8("❌ Allow local input to override synced state", "text/plain; charset=utf-8");
				}
				else if (p.Request.Page == "GetDisplayIdleTimeout")
				{
					try
					{
						uint timeout = WinSleep.GetMonitorTimeoutSeconds_AC();
						p.Response.FullResponseUTF8(timeout.ToString(), "text/plain; charset=utf-8");
					}
					catch (Exception ex)
					{
						Logger.Debug(ex);
						p.Response.FullResponseUTF8(ex.Message, "text/plain; charset=utf-8", "500 Internal Server Error");
					}
				}
				else if (p.Request.Page == "SetDisplayIdleTimeout")
				{
					long v = p.Request.GetLongParam("seconds", -1);
					if (v < 0)
					{
						p.Response.FullResponseUTF8("Missing or invalid seconds parameter.", "text/plain; charset=utf-8", "400 Bad Request");
						return;
					}
					uint seconds = (uint)v.Clamp(0, uint.MaxValue);
					try
					{
						WinSleep.SetMonitorTimeoutSeconds_AC(seconds);
						p.Response.FullResponseUTF8("OK", "text/plain; charset=utf-8");
					}
					catch (Exception ex)
					{
						Logger.Debug(ex);
						p.Response.FullResponseUTF8(ex.Message, "text/plain; charset=utf-8", "500 Internal Server Error");
					}
				}
				else
					successMessage = "";

				if (p.Response.StatusString.StartsWith("404"))
				{
					string monitorIdleTimeout = "";
					try
					{
						uint timeout = WinSleep.GetMonitorTimeoutSeconds_AC();
						if (timeout == 0)
							monitorIdleTimeout = "never";
						else
							monitorIdleTimeout = TimeUtil.ToDHMS((long)timeout * 1000);
					}
					catch (Exception ex)
					{
						Logger.Debug(ex);
						monitorIdleTimeout = ex.Message;
					}

					p.Response.FullResponseUTF8("<html><head><title>Monitor Control Service</title></head>"
						+ "<style type=\"text/css\">"
						+ " table { border-collapse: collapse; }"
						+ " th, td { border: 1px solid black; padding: 3px 5px; }"
						+ "</style>"
						+ "<body>"
						+ "<p class=\"result\">" + successMessage + "</p>"
						+ "<p class=\"syncStatus\">(Remote server \"" + Program.settings.syncAddress + "\") " + HttpUtility.HtmlEncode(MonitorControlService.syncedServerStatus) + "</p>"
						+ "<p>Current display status: " + Program.CurrentDisplayState + "</p>"
						+ "<p>(Power settings) Monitors off after: " + monitorIdleTimeout + "</p>"
						+ "<table>"
						//+ "<thead>"
						//+ "<tr><th></th><th></th></tr>"
						//+ "</thead>"
						+ "<tbody>"
						+ BuildRow("on", "turn displays on")
						+ BuildRow("on?offAfterSecs=15", "turn displays on, then after 15 seconds, off")
						+ BuildRow("cancel", "cancel a scheduled monitor off command (see above)")
						+ BuildRow("off", "turn displays off")
						+ BuildRow("off?partialwake=0.5", "immediately shows the partial wake dialog at 50% progress which will turn displays off if insufficient input is received")
						+ BuildRow("off?partialwake=0.5&mute=1", "immediately shows the partial wake dialog at 50% progress which will turn displays off if insufficient input is received. If displays are turned off, this command also mutes computer audio until the computer is no longer idle.")
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
						+ BuildRow("AllowLocalOverride", "Enables the setting \"Allow local input to override synced state\"")
						+ BuildRow("DisallowLocalOverride", "Disables the setting \"Allow local input to override synced state\"")
						+ BuildRow("GetDisplayIdleTimeout", "Gets the display idle timeout in the current OS power profile, in seconds (0 means never turn off the displays due to inactivity)")
						+ BuildRow("SetDisplayIdleTimeout?seconds=0", "Sets the display idle timeout in the current OS power profile, to never turn off the displays.")
						+ BuildRow("SetDisplayIdleTimeout?seconds=1", "Sets the display idle timeout in the current OS power profile, to 1 second.")
						+ BuildRow("SetDisplayIdleTimeout?seconds=300", "Sets the display idle timeout in the current OS power profile, to 300 seconds (5 minutes).")
						+ BuildRow("SetDisplayIdleTimeout?seconds=600", "Sets the display idle timeout in the current OS power profile, to 600 seconds (10 minutes).")
						+ BuildRow("SetDisplayIdleTimeout?seconds=900", "Sets the display idle timeout in the current OS power profile, to 900 seconds (15 minutes).")
						+ "</tbody>"
						+ "</table>"
						+ "</body>"
						+ "</html>", "text/html; charset=utf-8");
				}
			}
			finally
			{
				p.Response.Headers.Add("Access-Control-Allow-Origin", "*");
			}
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
			OffTracker.NotifyMonitorOn();
			currentMonitorStatus = "on";
			SetMonitorInState(-1);
			RunAdditionalCommandsInBackground(CommandSet.On);
			lock (myLock)
			{
				StopMonitorOffThread();
				StopWaitForWakeThread();
				if (p != null)
				{
					int offAfterSecs = p.Request.GetIntParam("offAfterSecs", 0);
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
			Logger.Info("Monitors turning off (mute=" + mute + ").");
			lock (myLock)
			{
				UnmuteIfNeeded();

				didMute = false;
				if (mute && !AudioManager.GetMasterVolumeMute())
				{
					// 2024 April, it was noticed that muting may cause the screens to immediately wake up, so now I mute before putting screens to sleep.
					AudioManager.SetMasterVolumeMute(true);
					didMute = true;
					Thread.Sleep(5); // I hate that this is necessary.
				}

				OffTracker.NotifyMonitorOff();
				currentMonitorStatus = "off";
				SetMonitorInState(2);
				RunAdditionalCommandsInBackground(CommandSet.Off);

				//didMute = false;
				//if (mute && !AudioManager.GetMasterVolumeMute())
				//{
				//	AudioManager.SetMasterVolumeMute(true);
				//	didMute = true;
				//}
				uint lastInputAge = LastInput.GetLastInputAgeMs();
				StopMonitorOffThread();
				StopWaitForWakeThread();
				StartWaitForWakeThread(new { lastInputAge });
			}
		}

		public override void handlePOSTRequest(HttpProcessor p)
		{
		}

		public override bool shouldLogSocketBind()
		{
			return true;
		}

		public override bool shouldLogRequestsToFile()
		{
			return Program.settings.logWebserverRequests;
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
				OffTracker.NotifyMonitorOff();
				currentMonitorStatus = "off";
				SetMonitorInState(2);
				RunAdditionalCommandsInBackground(CommandSet.Off);
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
				Stopwatch sleepTimer = Stopwatch.StartNew();
				int timesToSetOff = 12;
				bool inputDetected = false;
				// In theory the last input counter will roll over after 49.7102696 days. Or maybe it'll just stop incrementing?  Either way, lets hope the PC doesn't stay idle that long.
				while (true)
				{
					uint inputAge = LastInput.GetLastInputAgeMs();
					if (inputAge < lastInputAge)
					{
						inputDetected = true;
						break;
					}
					if (currentMonitorStatus != "off")
						break;
					lastInputAge = inputAge;
					if (!OffTracker.DidExecuteDelayedOffCommands
						&& (
						/*inputAge > OffTracker.DelayedOffCommandsDelayMs ||*/
						OffTracker.TimeSinceMonitorsTurnedOff > OffTracker.DelayedOffCommandsDelayMs
						))
					{
						RunAdditionalCommandsInBackground(CommandSet.OffAfterDelay);
						OffTracker.NotifyExecutedDelayedOffCommands();
					}
					Thread.Sleep(1000);
					if (timesToSetOff > 0 && sleepTimer.ElapsedMilliseconds >= 5000)
					{
						sleepTimer.Restart();
						timesToSetOff--;
						SetMonitorInState(2);
					}
				}
				if (inputDetected)
				{
					Logger.Info("Input detected. Assuming monitors are waking.");
				}
				bool doPartialWakeLogic = Program.settings.inputWakefulnessStrength > 1 && currentMonitorStatus == "off";
				bool lastOffDidMute = didMute;

				currentMonitorStatus = "on";
				RunAdditionalCommandsInBackground(CommandSet.On);
				UnmuteIfNeeded();
				OffTracker.NotifyMonitorOn();

				if (doPartialWakeLogic)
				{
					// Shows the partial wake dialog which begins a countdown.
					// If the user provides sufficient input before the countdown expires, the screens stay awake.  Otherwise, the screens are turned off.
					PartialWakeController.BeginPartialWakeState(lastOffDidMute, (double)Program.settings.partialWakeStart / (double)Program.settings.partialWakeMax);
				}
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

		private static EventWaitHandle additionalCommandsLock = new EventWaitHandle(true, EventResetMode.AutoReset);
		private static void RunAdditionalCommandsInBackground(CommandSet commandSet)
		{
			// We won't trigger new commands to run while commands are already running.
			additionalCommandsLock.WaitOne();
			SetTimeout.OnBackground(() =>
			{
				try
				{
					string commandStr;
					if (commandSet == CommandSet.Off)
						commandStr = Program.settings.commandsOff;
					else if (commandSet == CommandSet.OffAfterDelay)
						commandStr = Program.settings.commandsOffAfterDelay;
					else
						commandStr = Program.settings.commandsOn;

					string[] commands = commandStr
						.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
						.Select(cmd => cmd.Trim())
						.Where(cmd => !cmd.StartsWith("#"))
						.ToArray();
					//List<ProcessRunnerHandle> processes = new List<ProcessRunnerHandle>();
					foreach (string cmdRaw in commands)
					{
						string cmd = cmdRaw;
						if (!string.IsNullOrWhiteSpace(cmd))
						{
							string args = "";
							if (cmd.StartsWith("\""))
							{
								int idxSecondQuote = cmd.IndexOf('"', 1);
								if (idxSecondQuote > -1)
								{
									args = cmd.Substring(idxSecondQuote + 1).Trim();
									cmd = cmd.Substring(1, idxSecondQuote - 1);
								}
							}
							Logger.Info(cmd + " " + args);
							ProcessRunnerHandle proc = ProcessRunner.RunProcess(cmd, args, std =>
							{
								if (!string.IsNullOrEmpty(std))
									Logger.Info(cmd + ": " + std);
							}, err =>
							{
								if (!string.IsNullOrEmpty(err))
									Logger.Info(cmd + ": " + err);
							});
							proc.WaitForExit();
							Thread.Sleep(1000);
							//processes.Add(proc);
						}
					}
					//foreach (ProcessRunnerHandle proc in processes)
					//{
					//	proc.WaitForExit();
					//}
				}
				finally
				{
					additionalCommandsLock.Set();
				}
			}, 0);
		}

		private static void SetMonitorInState(int state)
		{
			Console.WriteLine("MonitorInState: " + state);
			DefWindowProc(GetDesktopWindow(), 0x112, (IntPtr)0xF170, (IntPtr)state);
		}

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
		[DllImport("user32.dll")]
		static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);
		[DllImport("user32.dll", SetLastError = false)]
		static extern IntPtr GetDesktopWindow();
	}

	public enum CommandSet
	{
		Off,
		OffAfterDelay,
		On
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