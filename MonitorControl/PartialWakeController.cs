using BPUtil;
using BPUtil.NativeWin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorControl
{
	public static class PartialWakeController
	{
		private static Thread partialWakeThread = null;
		private static volatile bool abort = false;
		private static object startupLock = new object();
		private static bool Mute;
		private static int MillisecondsToWait;
		private static int InputsRequired;
		private static int Progress;
		public static void BeginPartialWakeState(bool mute, int millisecondsToWait, int inputsRequired)
		{
			lock (startupLock)
			{
				abort = true;
				partialWakeThread?.Join();
				abort = false;

				Mute = mute;
				MillisecondsToWait = millisecondsToWait;
				InputsRequired = inputsRequired;
				Progress = 0;

				partialWakeThread = new Thread(PartialWakeThreadLoop);
				partialWakeThread.IsBackground = true;
				partialWakeThread.Name = "Partial Wake Thread";
				partialWakeThread.Start();
			}
		}
		private static void PartialWakeThreadLoop()
		{
			List<PartialWakeNotifier> pwns = new List<PartialWakeNotifier>();
			try
			{
				// Open partial wake dialogs on each monitor
				foreach (Screen screen in Screen.AllScreens)
				{
					PartialWakeNotifier pwn = new PartialWakeNotifier();
					pwn.StartPosition = FormStartPosition.Manual;
					int x = screen.Bounds.Left + screen.Bounds.Width / 2 - pwn.Width / 2;
					int y = screen.Bounds.Top + screen.Bounds.Height / 2 - pwn.Height / 2;
					pwn.Location = new Point(x, y);
					pwns.Add(pwn);
					SetTimeout.OnBackground(() => { Application.Run(pwn); }, 0);
				}
				int totalInputs = 0;
				const int iterationInterval = 100;
				Stopwatch sw = Stopwatch.StartNew();
				while (!abort && sw.ElapsedMilliseconds < MillisecondsToWait && totalInputs < InputsRequired)
				{
					long beforeSleep = sw.ElapsedMilliseconds;
					int secondsRemaining = (int)((MillisecondsToWait - beforeSleep) / 1000.0);
					// Set seconds remaining of all partial wake dialogs
					foreach (PartialWakeNotifier pwn in pwns)
					{
						pwn.SetSecondsRemaining(secondsRemaining);
					}

					Thread.Sleep(iterationInterval);

					if (LastInput.GetLastInputAgeMs() < iterationInterval)
					{
						totalInputs++;
						MillisecondsToWait += (int)(sw.ElapsedMilliseconds - beforeSleep);
						Progress = (int)((totalInputs / (double)InputsRequired) * 100);

						// Set progress of all partial wake dialogs
						foreach (PartialWakeNotifier pwn in pwns)
						{
							pwn.Progress = Progress;
						}
					}
				}
				if (!abort && totalInputs < InputsRequired)
					MonitorControlServer.Off(Mute);
			}
			catch (Exception ex)
			{
				Logger.Debug(ex);
			}
			finally
			{
				// Close all partial wake dialogs
				foreach (PartialWakeNotifier pwn in pwns)
				{
					pwn.Terminate();
				}
				partialWakeThread = null;
			}
		}
	}
}
