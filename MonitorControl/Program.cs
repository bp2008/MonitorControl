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
		/// <summary>
		/// <para>Gets the current display state.  It will be the "Unknown" state until the first state change message is received from the OS.</para>
		/// <para>The value should become known shortly after application startup.</para>
		/// </summary>
		public static DisplayState CurrentDisplayState => context?.CurrentDisplayState ?? DisplayState.Unknown;

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
				Globals.OverrideErrorFilePath(() => Globals.WritableDirectoryBase + "Logs/" + Globals.AssemblyName + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month.ToString().PadLeft(2, '0') + ".txt");
				Environment.CurrentDirectory = Globals.WritableDirectoryBase;

				Logger.CatchAll();

				settings = new Settings();
				settings.Load();
				if (settings.displayIdleTimeoutSeconds < 0)
				{
					try
					{
						settings.displayIdleTimeoutSeconds = WinSleep.GetMonitorTimeoutSeconds_AC();
					}
					catch (Exception ex)
					{
						Logger.Info("Error loading preferred monitor idle timeout value from OS: " + ex.Message);
					}
					if (settings.displayIdleTimeoutSeconds >= 0)
						settings.Save();
				}
				settings.SaveIfNoExist();

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);


				try
				{
					MainForm.MigrateLegacyAutostart();
				}
				catch (Exception)
				{
				}

				service = new MonitorControlService();
				try
				{
					MonitorControlServer.RestorePreferredMonitorIdleTimeout();
				}
				catch (Exception ex)
				{
					Logger.Info("Error accessing monitor idle timeout setting in OS power settings: " + ex.Message);
				}
				service.Start();


				// Optionally, one could add the icon to Project > Properties > Resources.resx and access it via Properties.Resources.simple_monitor_icon.
				ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
				Icon icon = (Icon)resources.GetObject("$this.Icon");

				TrayIconApplicationOptions options = new TrayIconApplicationOptions(icon);
				options.tooltipText = Globals.AssemblyTitle + " " + Globals.AssemblyVersion;
				options.onCreateContextMenu = Context_CreateContextMenu;
				options.onDoubleClick = Context_DoubleClick;
				options.ListenForDisplayStateChanges = true;
				options.DisplayStateChanged += MonitorControlServer.DisplayStateChanged;
				context = new TrayIconApplicationContext(options);

				Application.Run(context);
			}
			finally
			{
				try
				{
					SingleInstance.Stop();
				}
				catch { }
				try
				{
					service?.Stop();
				}
				catch { }
				try
				{
					context?.Dispose();
				}
				catch { }
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
				mf.SetLocationNearMouse();
			}
		}

		private static void Mf_FormClosed(object sender, FormClosedEventArgs e)
		{
			mf = null;
		}
		#endregion
	}
}
