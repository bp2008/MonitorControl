namespace MonitorControl
{
	/// <summary>
	/// An action assigned to a hotkey.
	/// </summary>
	public class Hotkey
	{
		/// <summary>
		/// String defining the hotkey.
		/// </summary>
		public string Text;

		/// <summary>
		/// Describes the action to be taken.
		/// </summary>
		public ActionType Action;

		/// <summary>
		/// Constructs an empty Hotkey.
		/// </summary>
		public Hotkey() { }
		/// <summary>
		/// Constructs a Hotkey with parameters.
		/// </summary>
		public Hotkey(string Text, ActionType Action)
		{
			this.Text = Text;
			this.Action = Action;
		}
	}
	/// <summary>
	/// Enumeration of possible hotkey actions.
	/// </summary>
	public enum ActionType
	{
		MonitorsOff,
		MonitorsOffAndMute,
		Mute,
		Unmute
	}
}