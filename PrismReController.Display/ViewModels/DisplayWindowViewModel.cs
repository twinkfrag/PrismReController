using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.Commands;
using Livet.Messaging.IO;
using PrismReController.Display.Models;
using PrismReController.Display.Properties;
using PrismReController.Shared.Utils;

namespace PrismReController.Display.ViewModels
{
	public class DisplayWindowViewModel : ViewModel
	{
		public DisplayWindowViewModel()
		{
			Task.Run(async () =>
			{
				IpAddr =
					(await Dns.GetHostAddressesAsync(Dns.GetHostName()))
					.Where(x => x.AddressFamily == AddressFamily.InterNetwork)
					.JoinToString();
			});

			VlcExeSelectionCommand = new ListenerCommand<OpeningFileSelectionMessage>(m =>
			{
				VlcExe = m.Response[0];
			});

			ExecuteVlcCommand = new ViewModelCommand(() =>
			{
				VlcRunService.Run(new FileInfo(VlcExe));
			});
		}


		private string ipAddr;

		public string IpAddr
		{
			get => ipAddr;
			set
			{
				if (ipAddr == value) return;
				ipAddr = value;
				RaisePropertyChanged();
			}
		}

		public string VlcExe
		{
			get => Settings.Default.VlcExePath;
			set
			{
				if (Settings.Default.VlcExePath == value) return;
				Settings.Default.VlcExePath = value;
				RaisePropertyChanged();
			}
		}

		public string VlcDir => Directory.GetParent(VlcExe).FullName;

		public ListenerCommand<OpeningFileSelectionMessage> VlcExeSelectionCommand { get; }

		public ViewModelCommand ExecuteVlcCommand { get; }
	}
}
