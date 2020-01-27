using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BPUtil;

namespace MonitorControl
{
	public class MonitorControlService
	{
		MonitorControlServer httpServer;
		/// <summary>
		/// An event which is raised when the http server binds a socket, indicating that a listening port number may have changed.
		/// </summary>
		public event EventHandler<string> SocketBound = delegate { };

		/// <summary>
		/// Currently active http port.
		/// </summary>
		public int http_port { get; private set; }
		/// <summary>
		/// Currently active https port.
		/// </summary>
		public int https_port { get; private set; }

		public MonitorControlService()
		{
			BPUtil.SimpleHttp.SimpleHttpLogger.RegisterLogger(BPUtil.Logger.httpLogger);
		}
		/// <summary>
		/// Stops and then starts the service.
		/// </summary>
		public void Start()
		{
			Stop();
			Logger.StartLoggingThreads();
			http_port = -1;
			https_port = -1;
			httpServer = new MonitorControlServer(Program.settings.http_port, Program.settings.https_port);
			httpServer.SocketBound += HttpServer_SocketBound;
			httpServer.Start();
		}

		/// <summary>
		/// Stops the service.
		/// </summary>
		public void Stop()
		{
			httpServer?.Stop();
			httpServer = null;
			Logger.StopLoggingThreads();
		}

		private void HttpServer_SocketBound(object sender, string e)
		{
			http_port = httpServer.Port_http;
			https_port = httpServer.Port_https;
			SocketBound(sender, e);
		}
	}
}
