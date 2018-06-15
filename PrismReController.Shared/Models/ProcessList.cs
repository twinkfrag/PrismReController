using System;
using System.Diagnostics;
using System.Linq;

namespace PrismReController.Shared.Models
{
	[Serializable]
	public class ProcessList
	{
		public SerialProcess[] SerialProcesses { get; set; }

		public Process[] LocalProcesses => SerialProcesses.Select(x => x.ToProcess()).ToArray();
	}
}
