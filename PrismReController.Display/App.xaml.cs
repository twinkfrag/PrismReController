using System.Windows;

namespace PrismReController.Display
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			Livet.DispatcherHelper.UIDispatcher = this.Dispatcher;
		}
	}
}
