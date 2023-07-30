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
	public partial class HotkeyControl : UserControl
	{
		/// <summary>
		/// Event raised when the user wishes to delete this hotkey.
		/// </summary>
		public event EventHandler Delete = delegate { };
		/// <summary>
		/// Event raised when the action was modified by the user.
		/// </summary>
		public event EventHandler ActionChanged = delegate { };

		private bool ready = false;
		/// <summary>
		/// Gets or sets the ActionType.
		/// </summary>
		public ActionType Action
		{
			get
			{
				return (ActionType)ddlHotkeyAction.SelectedIndex;
			}
			set
			{
				SetAction(value);
			}
		}

		/// <summary>
		/// Gets or sets the hotkey text.
		/// </summary>
		public string HotkeyText
		{
			get
			{
				return txtHotkeyInput.Text;
			}
			set
			{
				SetHotkeyText(value);
			}
		}
		private void SetAction(ActionType value)
		{
			if (this.InvokeRequired)
				this.Invoke((Action<ActionType>)SetAction, value);
			else
			{
				ready = false;
				ddlHotkeyAction.SelectedIndex = (int)value;
				ready = true;
			}
		}
		private void SetHotkeyText(string value)
		{
			if (this.InvokeRequired)
				this.Invoke((Action<string>)SetHotkeyText, value);
			else
			{
				ready = false;
				txtHotkeyInput.Text = value;
				ready = true;
			}
		}
		public HotkeyControl()
		{
			InitializeComponent();

			ddlHotkeyAction.Items.AddRange(Enum.GetNames(typeof(ActionType)));
			ddlHotkeyAction.SelectedIndex = 0;

			ready = true;
		}

		private void btnDeleteHotkey_Click(object sender, EventArgs e)
		{
			Delete.Invoke(this, new EventArgs());
		}

		private void ddlHotkeyAction_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!ready)
				return;
			RaiseActionChanged();
		}

		private void txtHotkeyInput_TextChanged(object sender, EventArgs e)
		{
			if (!ready)
				return;
			RaiseActionChanged();
		}
		public void RaiseActionChanged()
		{
			if (this.InvokeRequired)
				this.Invoke((Action)RaiseActionChanged);
			else
				ActionChanged.Invoke(this, new EventArgs());
		}
	}
}
