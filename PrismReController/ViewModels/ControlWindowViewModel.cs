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
			ConnectCommand = new ViewModelCommand(() =>
			{
				void statusUpdate(object sender, EventArgs e)
				{
					Connection.VlcConnectingsRaiseChanged -= statusUpdate;
					StatusService.Current.Notify($"Connect to {Destination}");
				}

				Connection.VlcConnectingsRaiseChanged += statusUpdate;
				new Connection(Destination);
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

		public ViewModelCommand ConnectCommand { get; }
	}
}
