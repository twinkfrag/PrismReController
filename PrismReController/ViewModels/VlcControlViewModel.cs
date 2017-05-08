using System;
using System.Linq;
using Livet;
using Livet.Commands;
using PrismReController.Models;
using PrismReController.Shared.Utils;

namespace PrismReController.ViewModels
{
	public class VlcControlViewModel : ViewModel
	{
		public VlcControlViewModel()
		{
			PlayCommand = new ViewModelCommand(
				async () => await Connection.Connectings.Select(x => x.Vlc.Play()).WhenAll(), 
				() => Connection.Connectings.Any(x => x.Vlc?.IsConnected ?? false));
			PauseCommand = new ViewModelCommand(
				async () => await Connection.Connectings.Select(x => x.Vlc.Pause()).WhenAll(),
				() => Connection.Connectings.Any(x => x.Vlc?.IsConnected ?? false));
			StopCommand = new ViewModelCommand(
				async () => await Connection.Connectings.Select(x => x.Vlc.Stop()).WhenAll(),
				() => Connection.Connectings.Any(x => x.Vlc?.IsConnected ?? false));

			Connection.VlcConnectingsRaiseChanged += CommandsRaiseCanExecuteChanged;
			Connection.VlcConnectingsRaiseChanged += (_, __) => RaisePropertyChanged(nameof(ConnectedVlcCount));
		}

		public ViewModelCommand PlayCommand { get; }

		public ViewModelCommand PauseCommand { get; }

		public ViewModelCommand StopCommand { get; }

		public int ConnectedVlcCount => Connection.Connectings.Count(x => x.Vlc.IsConnected);

		public void CommandsRaiseCanExecuteChanged(object sender, EventArgs e)
		{
			PlayCommand.RaiseCanExecuteChanged();
			PauseCommand.RaiseCanExecuteChanged();
			StopCommand.RaiseCanExecuteChanged();
		}
	}
}
