using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlcCtrl;

namespace PrismReController.Models
{
	public class Connection
	{
		public static IList<Connection> Connectings { get; } = new List<Connection>();


		public VlcController Vlc { get; private set; }

		public Connection(string destination)
		{
			var vlcPort = Shared.Properties.Settings.Default.VlcPort;
			var vlcPassword = Shared.Properties.Settings.Default.VlcPassword;

			var vlchost = new UriBuilder("http", destination, vlcPort).Uri;
			Vlc = new VlcController
			{
				HostUrl = vlchost.AbsoluteUri,
				Password = vlcPassword,
			};
			Task.Run(async () =>
				{
					for (int i = 0; ++i < Shared.Properties.Settings.Default.ConnectionRetryCount; await Task.Delay(100))
					{
						if (Vlc.IsConnected)
						{
							VlcConnectingsRaiseChanged?.Invoke(this, null);
							break;
						}
					}
				});

			Connectings.Add(this);
		}

		public static event EventHandler VlcConnectingsRaiseChanged;
	}
}
