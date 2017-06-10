using System.Diagnostics;
using System.IO;
using PrismReController.Shared.Models;
using PrismReController.Shared.Properties;

namespace PrismReController.Display.Models
{
	public class VlcRunService
	{
		public static void Run(FileInfo exeFile)
		{
			var info = new ProcessStartInfo(exeFile.FullName, $"--extraintf=http --http-port {Settings.Default.VlcPort} --http-password {Settings.Default.VlcPassword}");
			try
			{
				var process = Process.Start(info);

				Settings.Default.Save();
			}
			catch (FileNotFoundException)
			{
				StatusService.Current.Notify("VLC is Not Found.");
			}			
		}
	}
}
