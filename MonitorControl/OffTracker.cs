using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorControl
{
	/// <summary>
	/// Tracks the monitor state in such a way that "partial wakes" are ignored if they never change into full wakes.  This class helps determine when it is time to run delayed "off" commands.
	/// </summary>
	public static class OffTracker
	{
		private static Stopwatch swUptime = Stopwatch.StartNew();
		private static long? timeOfLastMonitorOffCommandFromUser = null;
		/// <summary>
		/// Gets the number of milliseconds to wait before executing delayed off commands.
		/// </summary>
		public static readonly long DelayedOffCommandsDelayMs = (long)TimeSpan.FromSeconds(9).TotalMilliseconds;
		/// <summary>
		/// Gets a value indiciating if the delayed off commands have been executed since the last time the system transitioned into "full wake" mode.
		/// </summary>
		public static bool DidExecuteDelayedOffCommands
		{
			get; private set;
		}
		/// <summary>
		/// Call when the system enters "off" mode.
		/// </summary>
		public static void NotifyMonitorOff()
		{
			if (timeOfLastMonitorOffCommandFromUser == null)
				timeOfLastMonitorOffCommandFromUser = swUptime.ElapsedMilliseconds;
		}
		/// <summary>
		/// Call when the system enters "full wake" mode.
		/// </summary>
		public static void NotifyMonitorOn()
		{
			timeOfLastMonitorOffCommandFromUser = null;
			DidExecuteDelayedOffCommands = false;
		}
		public static void NotifyExecutedDelayedOffCommands()
		{
			DidExecuteDelayedOffCommands = true;
		}
		/// <summary>
		/// Gets the time in milliseconds since the last time the system transitioned from "full wake" to "off" mode.
		/// </summary>
		public static long TimeSinceMonitorsTurnedOff
		{
			get
			{
				long? last = timeOfLastMonitorOffCommandFromUser;
				if (last == null)
					return 0;
				else
					return swUptime.ElapsedMilliseconds - last.Value;
			}
		}
	}
}
