using System;
using Livet;
using Livet.Commands;
using PrismReController.Models;

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
					WindowStatus = $"Connect to {Destination}";
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

		private string windowStatus;

		public string WindowStatus
		{
			get => windowStatus;
			set
			{
				if (windowStatus == value) return;
				windowStatus = value;
				RaisePropertyChanged();
			}
		}

		public ViewModelCommand ConnectCommand { get; }
	}
}
