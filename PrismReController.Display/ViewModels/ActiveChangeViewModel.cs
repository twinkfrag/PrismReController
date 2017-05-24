using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using PrismReController.Display.Interop;
using PrismReController.Display.Models;

namespace PrismReController.Display.ViewModels
{
	public class ActiveChangeViewModel : ViewModel
	{
		public Process[] WindowTitleList => WindowService.WindowProcesses;

		public void Activate(Process process) => process.SetForeground();
	}
}
