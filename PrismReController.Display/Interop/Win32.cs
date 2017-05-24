using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PrismReController.Display.Interop
{
	public static class Win32
	{
		[DllImport("user32.dll")]
		[return:MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);
	}
}
