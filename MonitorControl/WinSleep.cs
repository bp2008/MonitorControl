using BPUtil.NativeWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonitorControl
{
	/// <summary>
	/// Offers safe wrappers around Windows power management functions to get and set monitor timeout and other power settings.
	/// </summary>
	public static class WinSleep
	{
		/// <summary>
		/// Sets the time until monitors turn off when on AC power.
		/// </summary>
		/// <param name="seconds">Idle time in seconds until monitors turn off. 0 = never turn off</param>
		/// <exception cref="Win32Exception"></exception>
		public static void SetMonitorTimeoutSeconds_AC(uint seconds)
		{
			Guid activeScheme = PowerGetActiveScheme();
			PowerWriteACValueIndex(activeScheme, NativeMethods.GUID_VIDEO_SUBGROUP, NativeMethods.OPTION_VIDEOIDLE, seconds);
			PowerSetActiveScheme(activeScheme); // This is necessary to apply the change.
		}
		/// <summary>
		/// Gets the idle time in seconds until monitors turn off when on AC power.
		/// </summary>
		/// <returns>The idle time in seconds until monitors turn off. 0 = never turn off</returns>
		/// <exception cref="Win32Exception"></exception>
		public static uint GetMonitorTimeoutSeconds_AC()
		{
			Guid activeScheme = PowerGetActiveScheme();
			uint value = PowerReadACValueIndex(activeScheme, NativeMethods.GUID_VIDEO_SUBGROUP, NativeMethods.OPTION_VIDEOIDLE);
			return value;
		}
		/// <summary>
		/// Safely gets the GUID of the currently active power scheme, freeing the unmanaged memory allocated by the native function that is called.
		/// </summary>
		/// <returns>The GUID of the currently active power scheme.</returns>
		/// <exception cref="Win32Exception"></exception>
		private static Guid PowerGetActiveScheme()
		{
			Win32Helper.ThrowIfFailed(NativeMethods.PowerGetActiveScheme(IntPtr.Zero, out IntPtr pSchemeGuid), "PowerGetActiveScheme");
			try
			{
				return Marshal.PtrToStructure<Guid>(pSchemeGuid);
			}
			finally
			{
				LocalFree(pSchemeGuid);
			}

		}
		/// <summary>
		/// Safely calls the native PowerReadACValueIndex function to get the AC value index of a specified power setting in a specified power scheme.
		/// </summary>
		/// <param name="PowerSchemeGuid">Guid of the power scheme (see <see cref="PowerGetActiveScheme"/>).</param>
		/// <param name="SubGroupOfPowerSettingsGuid">The subgroup of power settings. This parameter can be one of the <c>NativeMethods.*SUBGROUP*</c> values.</param>
		/// <param name="PowerSettingsGuid">The identifier of the power setting.</param>
		/// <returns>The AC value index.</returns>
		private static uint PowerReadACValueIndex(Guid PowerSchemeGuid, Guid SubGroupOfPowerSettingsGuid, Guid PowerSettingsGuid)
		{
			Win32Helper.ThrowIfFailed(NativeMethods.PowerReadACValueIndex(IntPtr.Zero, ref PowerSchemeGuid, ref SubGroupOfPowerSettingsGuid, ref PowerSettingsGuid, out uint value), "PowerReadACValueIndex");
			return value;
		}
		/// <summary>
		/// Safely calls the native PowerWriteACValueIndex function to get the AC value index of a specified power setting in a specified power scheme.
		/// </summary>
		/// <param name="PowerSchemeGuid">Guid of the power scheme (see <see cref="PowerGetActiveScheme"/>).</param>
		/// <param name="SubGroupOfPowerSettingsGuid">The subgroup of power settings. This parameter can be one of the <c>NativeMethods.*SUBGROUP*</c> values.</param>
		/// <param name="PowerSettingsGuid">The identifier of the power setting.</param>
		/// <param name="AcValueIndex">The AC value index.</param>
		private static void PowerWriteACValueIndex(Guid PowerSchemeGuid, Guid SubGroupOfPowerSettingsGuid, Guid PowerSettingsGuid, uint AcValueIndex)
		{
			Win32Helper.ThrowIfFailed(NativeMethods.PowerWriteACValueIndex(IntPtr.Zero, ref PowerSchemeGuid, ref SubGroupOfPowerSettingsGuid, ref PowerSettingsGuid, AcValueIndex), "PowerWriteACValueIndex");
		}
		/// <summary>
		/// Safely sets the currently active power scheme.  This is necessary to call to apply changes made to the currently active scheme.
		/// </summary>
		/// <returns>The GUID of the currently active power scheme.</returns>
		/// <exception cref="Win32Exception"></exception>
		private static void PowerSetActiveScheme(Guid pSchemeGuid)
		{
			Win32Helper.ThrowIfFailed(NativeMethods.PowerSetActiveScheme(IntPtr.Zero, ref pSchemeGuid), "PowerSetActiveScheme");
		}
		/// <summary>
		/// Calls the native LocalFree function to free memory allocated by a native function.  Throws a Win32Exception if LocalFree fails.
		/// </summary>
		/// <param name="ptr">A handle to the local memory object. This handle is returned by either the LocalAlloc or LocalReAlloc function. It is not safe to free memory allocated with GlobalAlloc.</param>
		private static void LocalFree(IntPtr ptr)
		{
			if (ptr != IntPtr.Zero)
			{
				IntPtr result = NativeMethods.LocalFree(ptr);
				if (result != IntPtr.Zero)
					Win32Helper.ThrowLastWin32Error("LocalFree failed");
			}
		}
	}
	class NativeMethods
	{
		/// <summary>
		/// Retrieves the active power scheme and returns a GUID that identifies the scheme.
		/// </summary>
		/// <param name="RootPowerKey">This parameter is reserved for future use and must be set to NULL.</param>
		/// <param name="ActivePolicyGuid">A pointer that receives a pointer to a GUID structure. Use the LocalFree function to free this memory.</param>
		/// <returns></returns>
		[DllImport("PowrProf.dll", SetLastError = true)]
		internal static extern uint PowerGetActiveScheme(IntPtr RootPowerKey, out IntPtr ActivePolicyGuid);

		/// <summary>
		/// Retrieves the AC index of the specified power setting.
		/// </summary>
		/// <param name="RootPowerKey">This parameter is reserved for future use and must be set to NULL.</param>
		/// <param name="SchemeGuid">The identifier of the power scheme.</param>
		/// <param name="SubGroupOfPowerSettingsGuid">The subgroup of power settings. This parameter can be one of the "SUBGROUP" values defined in WinNT.h. Use <see cref="NO_SUBGROUP_GUID"/> to refer to the default power scheme.</param>
		/// <param name="PowerSettingGuid">The identifier of the power setting.</param>
		/// <param name="Value">A pointer to a variable that receives the AC value index.</param>
		/// <returns>Returns ERROR_SUCCESS (zero) if the call was successful, and a nonzero value if the call failed.</returns>
		[DllImport("PowrProf.dll", SetLastError = true)]
		internal static extern uint PowerReadACValueIndex(IntPtr RootPowerKey, ref Guid SchemeGuid, ref Guid SubGroupOfPowerSettingsGuid, ref Guid PowerSettingGuid, out uint Value);
		/// <summary>
		/// Sets the AC value index of the specified power setting.
		/// </summary>
		/// <param name="RootPowerKey">This parameter is reserved for future use and must be set to NULL.</param>
		/// <param name="SchemeGuid">The identifier of the power scheme.</param>
		/// <param name="SubGroupOfPowerSettingsGuid">The subgroup of power settings. This parameter can be one of the "SUBGROUP" values defined in WinNT.h. Use <see cref="NO_SUBGROUP_GUID"/> to refer to the default power scheme.</param>
		/// <param name="PowerSettingGuid">The identifier of the power setting.</param>
		/// <param name="Value">The AC value index.</param>
		/// <returns>Returns ERROR_SUCCESS (zero) if the call was successful, and a nonzero value if the call failed.</returns>
		/// <remarks>Changes to the settings for the active power scheme do not take effect until you call the <see cref="PowerSetActiveScheme"/> function.</remarks>
		[DllImport("PowrProf.dll", SetLastError = true)]
		internal static extern uint PowerWriteACValueIndex(IntPtr RootPowerKey, ref Guid SchemeGuid, ref Guid SubGroupOfPowerSettingsGuid, ref Guid PowerSettingGuid, uint Value);

		[DllImport("PowrProf.dll", SetLastError = true)]
		internal static extern uint PowerReadDCValueIndex(IntPtr RootPowerKey, ref Guid SchemeGuid, ref Guid SubGroupOfPowerSettingsGuid, ref Guid PowerSettingGuid, out uint Value);

		[DllImport("PowrProf.dll", SetLastError = true)]
		internal static extern uint PowerWriteDCValueIndex(IntPtr RootPowerKey, ref Guid SchemeGuid, ref Guid SubGroupOfPowerSettingsGuid, ref Guid PowerSettingGuid, uint Value);

		[DllImport("PowrProf.dll", SetLastError = true)]
		internal static extern uint PowerSetActiveScheme(IntPtr RootPowerKey, ref Guid SchemeGuid);
		/// <summary>
		/// Frees the specified local memory object and invalidates its handle.
		/// </summary>
		/// <param name="hMem">A handle to the local memory object. This handle is returned by either the LocalAlloc or LocalReAlloc function. It is not safe to free memory allocated with GlobalAlloc.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is NULL.</para>
		/// <para>If the function fails, the return value is equal to a handle to the local memory object. To get extended error information, call GetLastError.</para>
		/// </returns>
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LocalFree(IntPtr hMem);


		/// <summary>
		/// Settings in this subgroup are part of the default power scheme.
		/// </summary>
		public static Guid NO_SUBGROUP_GUID = new Guid("fea3413e-7e05-4911-9a71-700331f1c294");
		/// <summary>
		/// Settings in this subgroup are part of the default power scheme. (Alias of <see cref="NO_SUBGROUP_GUID"/>)
		/// </summary>
		public static Guid GUID_SUBGROUP_NONE = NO_SUBGROUP_GUID;
		/// <summary>
		/// Settings in this subgroup control system sleep settings.
		/// </summary>
		public static Guid GUID_SLEEP_SUBGROUP = new Guid("238c9fa8-0aad-41ed-83f4-97be242c8f20");
		/// <summary>
		/// Settings in this subgroup control configuration of the video power management features.
		/// </summary>
		public static Guid GUID_VIDEO_SUBGROUP = new Guid("7516b95f-f776-4464-8c53-06167f40cc99");
		/// <summary>
		/// Settings in this subgroup control configuration of the system power buttons.
		/// </summary>
		public static Guid GUID_SYSTEM_BUTTON_SUBGROUP = new Guid("4f971e89-eebd-4455-a8de-9e59040e7347");
		/// <summary>
		/// Settings in this subgroup control configuration of processor power management features.
		/// </summary>
		public static Guid GUID_PROCESSOR_SETTINGS_SUBGROUP = new Guid("54533251-82be-4824-96c1-47b60b740d00");
		/// <summary>
		/// Settings in this subgroup control battery alarm trip points and actions.
		/// </summary>
		public static Guid GUID_BATTERY_SUBGROUP = new Guid("e73a048d-bf27-4f12-9731-8b2076e8891f");
		/// <summary>
		/// Settings in this subgroup control PCI Express settings.
		/// </summary>
		public static Guid GUID_PCIEXPRESS_SETTINGS_SUBGROUP = new Guid("501a4d13-42af-4429-9fd1-a8218c268e20");

		/// <summary>
		/// Top level options
		/// </summary>
		public static Guid OPTION_CONSOLELOCK = new Guid("0e796bdb-100d-47d6-a2d5-f7d2daa51f51");

		/// <summary>
		/// Sleep options
		/// </summary>
		public static Guid OPTION_RTCWAKE = new Guid("bd3b718a-0680-4d9d-8ab2-e1d2b4ac806d");
		/// <summary>
		/// Turn the computer to sleep after x seconds, minmum value: 0, maximum value: 4294967295
		/// </summary>
		public static Guid OPTION_STANDBYIDLE = new Guid("29f6c1db-86da-48c5-9fdb-f2b67b1f44da");
		/// <summary>
		/// Hybernate the computer after x seconds, minmum value: 0, maximum value: 4294967295
		/// </summary>
		public static Guid OPTION_HYBERNATEIDLE = new Guid("9d7815a6-7ee4-497e-8888-515a05f02364");

		// Display options

		/// <summary>
		/// Display brightness, minimum value: 0, maximum value: 100
		/// </summary>
		public static Guid OPTION_BRIGHTNESS = new Guid("aded5e82-b909-4619-9949-f5d71dac0bcb");
		/// <summary>
		/// Dim display after x seconds, minmum value: 0, maximum value: 4294967295.  For some reason this is the same GUID as <see cref="OPTION_VIDEOIDLE"/>, so I suspect "VIDEODIM" is not implemented.
		/// </summary>
		public static Guid OPTION_VIDEODIM = new Guid("3c0bc021-c8a8-4e07-a973-6b14cbcb2b7e");
		/// <summary>
		/// Turn off display after x seconds, minmum value: 0, maximum value: 4294967295
		/// </summary>
		public static Guid OPTION_VIDEOIDLE = new Guid("3c0bc021-c8a8-4e07-a973-6b14cbcb2b7e");

		// Button options

		/// <summary>
		/// Lid close action
		/// </summary>
		public static Guid OPTION_LIDACTION = new Guid("5ca83367-6e45-459f-a27b-476b1d01c936");
		/// <summary>
		/// Power button action
		/// </summary>
		public static Guid OPTION_PBUTTONACTION = new Guid("7648efa3-dd9c-4e3e-b566-50f929386280");
		/// <summary>
		/// Sleep button action
		/// </summary>
		public static Guid OPTION_SBUTTONACTION = new Guid("96996bc0-ad50-47ec-923b-6f41874dd9eb");

		// Button actions

		public static uint ACTION_DO_NOTHING = 0;
		public static uint ACTION_SLEEP = 1;
		public static uint ACTION_HIBERNATE = 2;
		public static uint ACTION_SHUT_DOWN = 3;

		// Universal constants

		public static uint FALSE = 0;
		public static uint TRUE = 1;

		public static uint NEVER = 0;
	}
}