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
using PrismReController.Display.Models;
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

			ExecuteVlcCommand = new ViewModelCommand(() =>
			{
				VlcRunService.Run(new FileInfo(@"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe"));
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

		private FileInfo vlcExe;

		public FileInfo VlcExe
		{
			get => vlcExe;
			set
			{
				if (vlcExe == value) return;
				vlcExe = value;
				RaisePropertyChanged();
			}
		}

		public ViewModelCommand ExecuteVlcCommand { get; }
	}
}
