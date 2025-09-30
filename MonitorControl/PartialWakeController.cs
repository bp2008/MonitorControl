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
		/// <summary>
		/// <para>Begins the partial wake protocol.</para>
		/// <para>After iterations where the user provides an input, wakefulness increases.</para>
		/// <para>When the user does not provide an input, wakefulness decreases.</para>
		/// <para>If wakefulness reaches 0, screens turn off.</para>
		/// <para>If wakefulness reaches 1, screens stay on.</para>
		/// </summary>
		private static double Wakefulness;
		/// <summary>
		/// Each partial wake thread will abort itself if its thread ID counter does not match this value.
		/// </summary>
		private static long LiveThreadId = 0;
		public static void BeginPartialWake(bool mute, double wakefulness, string reason)
		{
			lock (startupLock)
			{
				StopPartialWake();

				Mute = mute;
				Wakefulness = wakefulness.Clamp(0, 1);

				partialWakeThread = new Thread(PartialWakeThreadLoop);
				partialWakeThread.IsBackground = true;
				partialWakeThread.Name = "Partial Wake Thread";
				partialWakeThread.Start(new
				{
					myThreadId = Interlocked.Increment(ref LiveThreadId),
					reason
				});
			}
		}
		/// <summary>
		/// Stops the currently active partial wake protocol, if any.
		/// </summary>
		public static void StopPartialWake()
		{
			Interlocked.Increment(ref LiveThreadId);
		}
		/// <summary>
		/// Amount of wakefulness that is lost per second (based on total seconds of wakefulness progress bar).
		/// </summary>
		static double wakefulnessLossPerSecond
		{
			get
			{
				return 1.0 / (double)Program.settings.partialWakeMax;
			}
		}
		private static bool Abort(long myThreadId)
		{
			return Interlocked.Read(ref LiveThreadId) != myThreadId;
		}
		private static void PartialWakeThreadLoop(object arg)
		{
			dynamic args = arg;
			Logger.Info("Partial wake thread starting (" + (string)args.reason + ")");
			long myThreadId = args.myThreadId;

			const int iterationInterval = 100;
			int strength = Program.settings.inputWakefulnessStrength <= 0 ? 3 : Program.settings.inputWakefulnessStrength.Clamp(1, 10);
			double wakefulnessGainPerInput = strength * 0.0125;

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
					pwn.Progress = (int)Math.Round(Wakefulness * 100);
					pwns.Add(pwn);
					SetTimeout.OnBackground(() => { Application.Run(pwn); }, 0);
				}


				double wakefulnessLossPerInterval = wakefulnessLossPerSecond * (iterationInterval / 1000.0);
				while (!Abort(myThreadId) && Wakefulness > 0 && Wakefulness < 1)
				{
					int secondsRemaining = (int)(Wakefulness / wakefulnessLossPerSecond);
					int Progress = (int)Math.Round(Wakefulness * 100);
					// Update all partial wake dialogs
					foreach (PartialWakeNotifier pwn in pwns)
					{
						pwn.SetSecondsRemaining(secondsRemaining);
						pwn.Progress = Progress;
					}

					Thread.Sleep(iterationInterval);

					if (LastInput.GetLastInputAgeMs() < iterationInterval)
						Wakefulness += wakefulnessGainPerInput;
					else
						Wakefulness -= wakefulnessLossPerInterval;
				}
				bool abort = Abort(myThreadId);
				if (!abort && Wakefulness <= 0)
				{
					Logger.Info("Partial wake thread is turning off monitors due to inactivity.");
					MonitorControlServer.Off(Mute);
				}
				else if (Wakefulness >= 1)
				{
					Logger.Info("Partial wake thread is allowing monitors to stay awake due to continued user input.");
				}
				else if (abort)
				{
					Logger.Info("Partial wake thread was commanded to stop early and will take no further action.");
				}
				else
				{
					Logger.Info("Partial wake thread is exiting for an unhandled reason.");
				}
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
					try
					{
						pwn.Terminate();
					}
					catch (Exception ex)
					{
						Logger.Debug(ex);
					}
				}
				partialWakeThread = null;
			}
		}
	}
}
