using System;
using Livet;
using Livet.Commands;
using PrismReController.Models;
using PrismReController.Shared.Models;

namespace PrismReController.ViewModels
{
	public class ControlWindowViewModel : ViewModel
	{
		public ControlWindowViewModel()
		{
			ConnectCommand = new ViewModelCommand(async () =>
			{
				void statusUpdate(object sender, EventArgs e)
				{
					Connection.VlcConnectingsRaiseChanged -= statusUpdate;
					StatusService.Current.Notify($"Connect to {Destination}");
				}

				Connection.VlcConnectingsRaiseChanged += statusUpdate;
				connection = await Connection.CreateAsync(Destination);
			});
		}

		private string destination;

		public string Destination
		{
			get => destination;
			set
			{
				if (destination == value) return;
				destination = value;
				RaisePropertyChanged();
			}
		}

		private string connectedDest = "Not Connected";

		public string ConnectedDest
		{
			get => connectedDest;
			set
			{
				if (connectedDest == value) return;
				connectedDest = value;
				RaisePropertyChanged();
			}
		}

		private Connection connection;

		public ViewModelCommand ConnectCommand { get; }
	}
}
