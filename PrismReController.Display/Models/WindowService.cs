using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismReController.Display.Interop;

namespace PrismReController.Display.Models
{
	public static class WindowService
	{
		public static Process[] WindowProcesses =>
			Process.GetProcesses()
				   .Where(x => x.MainWindowHandle != IntPtr.Zero && !string.IsNullOrEmpty(x.MainWindowTitle))
				   .ToArray();

		public static void SetForeground(this Process process)
		{
			if (process != null && process.MainWindowHandle != IntPtr.Zero)
			{
				Win32.SetForegroundWindow(process.MainWindowHandle);
			}
		}
	}
}
