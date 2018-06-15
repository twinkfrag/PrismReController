using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharedSettings = PrismReController.Shared.Properties.Settings;

namespace PrismReController.Display.Models
{
	public class Connection : IDisposable
	{
		#region FactoryMethod

		private Connection() { }

		public static async Task<Connection> CreateAsync() 
			=> await new Connection().EstablishAsync();

		#endregion


		private bool connectionLoop = false;

		public HttpListener Listener { get; private set; }

		public WebSocket WebSocket { get; private set; }

		private async Task<Connection> EstablishAsync()
		{

			var connectionPort = SharedSettings.Default.ConnectionPort;
			Listener = new HttpListener();
			Listener.Prefixes.Add($"http://*:{connectionPort}/");
			Listener.Start();

			connectionLoop = true;
			while (connectionLoop)
			{
				var context = await Listener.GetContextAsync();
				if (context.Request.IsWebSocketRequest)
				{
					WebSocket = (await context.AcceptWebSocketAsync(null)).WebSocket;

				}
				else
				{
					context.Response.StatusCode = 400;
					context.Response.Close();
				}
			}
			return this;
		}

		public void Dispose()
		{
			connectionLoop = false;
			Listener?.Stop();
			WebSocket?.Dispose();
		}
	}
}
