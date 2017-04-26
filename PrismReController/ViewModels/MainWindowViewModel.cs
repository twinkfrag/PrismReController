using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Livet;
using Livet.Commands;
using VlcCtrl;

namespace PrismReController.ViewModels
{
	public class MainWindowViewModel : ViewModel
	{
		private const int Retry = 100;

		public MainWindowViewModel()
		{
#if DEBUG
			Url = @"http://localhost:8080/";
			Password = "a";
#endif

			ConnectCommand = new ViewModelCommand(async () =>
			{
				await Task.Run(() =>
				{
					Vlc = new VlcController
					{
						HostUrl = Url,
						Password = Password,
					};
				});
				for (int i = 0; ++i < Retry; await Task.Delay(100))
				{
					if (Vlc.IsConnected)
					{
						PlayCommand.RaiseCanExecuteChanged();
						PauseCommand.RaiseCanExecuteChanged();
						break;
					}
				}
			});
			PlayCommand = new ViewModelCommand(async () => await Vlc.Play(), () => Vlc?.IsConnected ?? false);
			PauseCommand = new ViewModelCommand(async () => await Vlc.Pause(), () => Vlc?.IsConnected ?? false);
		}

		public VlcController Vlc { get; private set; }

		private string url;

		public string Url
		{
			get => url;
			set
			{
				if (url == value) return;
				url = value;
				RaisePropertyChanged();
			}
		}

		private string password;

		public string Password
		{
			get => password;
			set
			{
				if (password == value) return;
				password = value;
				RaisePropertyChanged();
			}
		}

		public ViewModelCommand ConnectCommand { get; }

		public ViewModelCommand PlayCommand { get; }

		public ViewModelCommand PauseCommand { get; }
	}
}
