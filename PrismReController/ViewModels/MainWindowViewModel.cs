using Livet;
using Livet.Commands;
using PrismReController.Models;

namespace PrismReController.ViewModels
{
	public class MainWindowViewModel : ViewModel
	{
		public MainWindowViewModel()
		{
#if DEBUG
			Destination = @"localhost";
#endif

			ConnectCommand = new ViewModelCommand(() => new Connection(Destination));
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
