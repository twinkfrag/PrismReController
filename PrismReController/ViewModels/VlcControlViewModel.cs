using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.Commands;
using PrismReController.Models;
using PrismReController.Shared.Utils;
using VlcCtrl;

namespace PrismReController.ViewModels
{
	public class VlcControlViewModel : ViewModel
	{
		public VlcControlViewModel()
		{
			PlayCommand = new ViewModelCommand(
				async () => await Connection.Connectings.Select(async x => await x.Vlc.Play()).WhenAll(), 
				() => Connection.Connectings.Any(x => x.Vlc?.IsConnected ?? false));
			PauseCommand = new ViewModelCommand(
				async () => await Connection.Connectings.Select(async x => await x.Vlc.Pause()).WhenAll(),
				() => Connection.Connectings.Any(x => x.Vlc?.IsConnected ?? false));
			StopCommand = new ViewModelCommand(
				async () => await Connection.Connectings.Select(async x => await x.Vlc.Stop()).WhenAll(),
				() => Connection.Connectings.Any(x => x.Vlc?.IsConnected ?? false));

			Connection.VlcConnectingsRaiseChanged += (_, __) => CommandsRaiseCanExecuteChanged();
		}

		public ViewModelCommand PlayCommand { get; }

		public ViewModelCommand PauseCommand { get; }

		public ViewModelCommand StopCommand { get; }

		public void CommandsRaiseCanExecuteChanged()
		{
			PlayCommand.RaiseCanExecuteChanged();
			PauseCommand.RaiseCanExecuteChanged();
			StopCommand.RaiseCanExecuteChanged();
		}
	}
}
