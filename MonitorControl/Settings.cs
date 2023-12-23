using BPUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorControl
{
	public class Settings : SerializableObjectBase
	{
		public int http_port = 8001;
		public int https_port = -1;
		public int idleTimeMs = 3000;
		public string ip_whitelist = "127.0.0.1";
		public string syncAddress = "";
		/// <summary>
		/// [5-60; Default 20].  Number of idle seconds for the "Partial Wake" progress bar to go from full to empty.  A higher number means more inputs will be required.
		/// </summary>
		public int partialWakeMax = 20;
		/// <summary>
		/// [4-59; Default 10].  Number of idle seconds the "Partial Wake" progress bar should begin with.
		/// </summary>
		public int partialWakeStart = 10;
		/// <summary>
		/// [0-10] 0 to disable, 1 is minimum strength, 10 is maximum strength.  At lower strength, more inputs are required to perform a full wake.
		/// </summary>
		public int inputWakefulnessStrength = 3;
		public int syncPort = 8001;
		/// <summary>
		/// (0: No action) …  (1: Turn Off) … (2: Turn On)
		/// </summary>
		public int syncFailureAction = 0;
		public bool syncHTTPS = false;
		/// <summary>
		/// Mute audio when turning off monitors because of remote server sync.
		/// </summary>
		public bool syncMute;
		/// <summary>
		/// If enabled, local inputs can un-sync this machine from the remote server until the remote server has another state change.
		/// If disabled, the synced state is continuously enforced.
		/// </summary>
		public bool syncAllowLocalOverride = false;
		/// <summary>
		/// Commands to run when turning off monitors (one command per line).
		/// </summary>
		public string commandsOff = "";
		/// <summary>
		/// Commands to run after a long delay after turning off monitors (one command per line).
		/// </summary>
		public string commandsOffAfterDelay = "";
		/// <summary>
		/// Commands to run when turning on monitors (one command per line).
		/// </summary>
		public string commandsOn = "";
		/// <summary>
		/// List of hotkeys.
		/// </summary>
		public List<Hotkey> hotkeys = new List<Hotkey>();

		/// <summary>
		/// Returns the <see cref="ip_whitelist"/> string with line breaks converted to Windows format. The XML writer breaks them otherwise.
		/// </summary>
		/// <returns></returns>
		public string GetIpWhitelistString()
		{
			return FixStoredString(ip_whitelist);
		}
		/// <summary>
		/// Returns the string with line breaks converted to Windows format. The XML writer breaks them otherwise.
		/// </summary>
		/// <returns></returns>
		public static string FixStoredString(string storedString)
		{
			return storedString.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
		}

		public bool IpIsWhitelisted(string ip)
		{
			if (ip_whitelist == null)
				return false;
			else
				return ip_whitelist.Split(new char[] { ' ', '\r', '\n', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries).Contains(ip);
		}
	}
}
