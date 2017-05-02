using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismReController.Shared.Properties;

namespace PrismReController.Display.Models
{
	public class VlcRunService
	{
		public static void Run(FileInfo exeFile)
		{
			var info = new ProcessStartInfo(exeFile.FullName, $"--extraintf=http --http-port {Settings.Default.VlcPort} --http-password {Settings.Default.VlcPassword}");
			Process.Start(info);
		}
	}
}
