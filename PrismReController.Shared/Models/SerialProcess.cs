using System;
using System.Diagnostics;

namespace PrismReController.Shared.Models
{
	[Serializable]
	public class SerialProcess
	{
		public int Id { get; set; }

		public string MainWindowTitle { get; set; }

		public Process ToProcess() => Process.GetProcessById(Id);

		public static SerialProcess FromProcess(Process process) => 
			new SerialProcess { Id = process.Id, MainWindowTitle = process.MainWindowTitle };
	}
}