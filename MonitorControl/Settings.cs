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
