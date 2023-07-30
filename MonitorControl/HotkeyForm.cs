using BPUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorControl
{
	public partial class HotkeyForm : Form
	{
		public HotkeyForm()
		{
			InitializeComponent();
			this.SetLocationNearMouse();
		}

		private void HotkeyForm_Load(object sender, EventArgs e)
		{
			MonitorControlService.hotkeyEditorOpen = true;
			MonitorControlService.keyboardHook.KeyPressedAsync += KeyboardHook_KeyPressedAsync;

			foreach (Hotkey hotkey in Program.settings.hotkeys)
			{
				CreateHotkeyControl(hotkey);
			}

			//panelHotkeys.HorizontalScroll.Enabled = false;
			//panelHotkeys.VerticalScroll.Enabled = true;
		}

		private void HotkeyForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			MonitorControlService.keyboardHook.KeyPressedAsync -= KeyboardHook_KeyPressedAsync;
			MonitorControlService.hotkeyEditorOpen = false;
		}

		private void KeyboardHook_KeyPressedAsync(object sender, BPUtil.NativeWin.AsyncKeyPressEventArgs e)
		{
			Control focusedControl = FindFocusedControl(this);
			while (focusedControl != null)
			{
				if (focusedControl is HotkeyControl)
				{
					(focusedControl as HotkeyControl).HotkeyText = e.ToString();
					(focusedControl as HotkeyControl).RaiseActionChanged();
					break;
				}
				focusedControl = focusedControl.Parent;
			}
		}

		public static Control FindFocusedControl(Control control)
		{
			var container = control as IContainerControl;
			while (container != null)
			{
				control = container.ActiveControl;
				container = control as IContainerControl;
			}
			return control;
		}

		private void btnAddHotkey_Click(object sender, EventArgs e)
		{
			Hotkey hotkey = new Hotkey();
			Program.settings.hotkeys.Add(hotkey);
			Program.settings.Save();
			CreateHotkeyControl(hotkey);
		}

		private void CreateHotkeyControl(Hotkey hotkey)
		{
			HotkeyControl control = new HotkeyControl();
			control.Dock = DockStyle.Top;
			control.HotkeyText = hotkey.Text;
			control.Action = hotkey.Action;

			control.Delete += (sender1, e1) =>
			{
				panelHotkeys.Controls.Remove(control);
				Program.settings.hotkeys.Remove(hotkey);
			};

			control.ActionChanged += (sender2, e2) =>
			{
				hotkey.Text = control.HotkeyText;
				hotkey.Action = control.Action;
				Program.settings.Save();
			};

			panelHotkeys.Controls.Add(control);
		}
	}
}
