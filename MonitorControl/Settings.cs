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
		/// If enabled, multiple inputs will be required to keep the monitors awake after they've been turned off by this app.
		/// </summary>
		public bool preventAccidentalWakeup = false;
		public int inputsRequiredToWake = 0;
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
		/// Commands to run when turning on monitors (one command per line).
		/// </summary>
		public string commandsOn = "";

		/// <summary>
		/// Returns the <see cref="ip_whitelist"/> string with line breaks converted to Windows format. The XML writer breaks them otherwise.
		/// </summary>
		/// <returns></returns>
		public string GetIpWhitelistString()
		{
			return ip_whitelist.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
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
