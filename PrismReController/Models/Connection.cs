using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VlcCtrl;
using SharedSettings = PrismReController.Shared.Properties.Settings;

namespace PrismReController.Models
{
	public class Connection : IDisposable
	{
		#region FactoryMethod

		private Connection() { }

		public static async Task<Connection> CreateAsync(string destination)
			=> await new Connection().EstablishAsync(destination);

		#endregion


		public static IList<Connection> Connectings { get; } = new List<Connection>();


		public VlcController Vlc { get; private set; }

		public ClientWebSocket WebSocket { get; private set; }

		private Task ReceiveTask { get; set; }


		public async Task<Connection> EstablishAsync(string destination)
		{
			var vlcPort = SharedSettings.Default.VlcPort;
			var vlcPassword = SharedSettings.Default.VlcPassword;

			var vlchost = new UriBuilder("http", destination, vlcPort).Uri;
			Vlc = new VlcController
			{
				HostUrl = vlchost.AbsoluteUri,
				Password = vlcPassword,
			};
			Vlc.ConnectionChanged += VlcConnectingsRaiseChanged;


			var connectionPort = SharedSettings.Default.ConnectionPort;
			WebSocket = new ClientWebSocket();
			await WebSocket.ConnectAsync(
				new UriBuilder("ws", destination, connectionPort).Uri,
				CancellationToken.None);

			ReceiveTask = Task.Run(async () =>
			{
				var buff = new ArraySegment<byte>();
				await WebSocket.ReceiveAsync(buff, CancellationToken.None);
			});

			Connectings.Add(this);
			return this;
		}

		public static event EventHandler<EventArgs> VlcConnectingsRaiseChanged;

		public async void Dispose()
		{
			await WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disposed", CancellationToken.None);
		}
	}
}
