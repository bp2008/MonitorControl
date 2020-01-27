using BPUtil;
using BPUtil.Forms;
using BPUtil.NativeWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorControl
{
	static class Program
	{
		public static Settings settings;
		public static MonitorControlService service = null;

		private static TrayIconApplicationContext context;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				if (!SingleInstance.Start())
					return;

				Directory.CreateDirectory(Globals.WritableDirectoryBase);
				Directory.CreateDirectory(Globals.WritableDirectoryBase + "Logs/");
				Globals.OverrideErrorFilePath(() => Globals.WritableDirectoryBase + "Logs/" + Globals.AssemblyName + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + ".txt");
				Environment.CurrentDirectory = Globals.WritableDirectoryBase;

				Logger.CatchAll();

				settings = new Settings();
				settings.Load();
				settings.SaveIfNoExist();

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				service = new MonitorControlService();
				service.Start();

				// Optionally, one could add the icon to Project > Properties > Resources.resx and access it via Properties.Resources.simple_monitor_icon.
				ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
				Icon icon = (Icon)resources.GetObject("$this.Icon");

				context = new TrayIconApplicationContext(icon, Globals.AssemblyTitle + " " + Globals.AssemblyVersion, Context_CreateContextMenu, Context_DoubleClick);

				Application.Run(context);
			}
			finally
			{
				SingleInstance.Stop();
				service?.Stop();
				context?.Dispose();
			}
		}

		public static void Exit()
		{
			context?.ExitThread();
		}

		private static bool Context_CreateContextMenu(TrayIconApplicationContext context)
		{
			context.AddToolStripMenuItem("&Configure " + Globals.AssemblyTitle, Context_Configure, Properties.Resources.settings64);
			context.AddToolStripSeparator();
			context.AddToolStripMenuItem("E&xit " + Globals.AssemblyTitle, (sender, e) => { context.ExitThread(); }, Properties.Resources.close64);
			return true;
		}
		private static void Context_DoubleClick()
		{
			OpenConfigurationForm();
		}

		private static void Context_Configure(object sender, EventArgs e)
		{
			OpenConfigurationForm();
		}

		#region Configuration Form
		static MainForm mf = null;
		private static void OpenConfigurationForm()
		{
			if (mf != null)
				mf.Activate();
			else
			{
				mf = new MainForm();
				mf.FormClosed += Mf_FormClosed;
				mf.Show();
			}
		}

		private static void Mf_FormClosed(object sender, FormClosedEventArgs e)
		{
			mf = null;
		}
		#endregion
	}
}
