using System.Windows.Controls;
using System.Windows.Markup;

namespace PrismReController.Shared.Views
{
	[ContentProperty(nameof(Content))]
	public partial class StatusBar
	{
		public StatusBar()
		{
			InitializeComponent();
		}

		public object Content
		{
			get => MainControl.Content;
			set => MainControl.Content = value;
		}
	}
}
