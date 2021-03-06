﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using BPUtil;
using BPUtil.NativeWin;
using BPUtil.NativeWin.AudioController;
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

		private void OffIfIdle(int idleTimeMs, bool mute)
		{
			if (LastInput.GetLastInputAgeMs() >= idleTimeMs)
			{
				Off(mute);
			}
		}
		private void Off(bool mute)
		{
			currentMonitorStatus = "off";
			SetMonitorInState(2);

			if (mute)
			{
				uint lastInputAge = LastInput.GetLastInputAgeMs();
				if (!AudioManager.GetMasterVolumeMute())
				{
					AudioManager.SetMasterVolumeMute(true);
					Thread thrUnmute = new Thread(() =>
					{
						// In theory the last input counter will roll over after 49.7102696 days. Or maybe it'll just stop incrementing?  Either way, lets hope the PC doesn't stay idle that long.
						while (true)
						{
							uint inputAge = LastInput.GetLastInputAgeMs();
							if (inputAge < lastInputAge)
								break;
							lastInputAge = inputAge;
							Thread.Sleep(1000);
						}
						AudioManager.SetMasterVolumeMute(false);
					});
					thrUnmute.IsBackground = true;
					thrUnmute.Name = "Unmute when user returns";
					thrUnmute.Start();
				}
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
