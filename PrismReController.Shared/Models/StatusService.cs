using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Livet;

namespace PrismReController.Shared.Models
{
	/// <summary>
	/// メイン ウィンドウ下部に表示されるステータス バーへのアクセスを提供します。
	/// </summary>
	public class StatusService : NotificationObject
	{
		#region static members

		public static StatusService Current { get; } = new StatusService();

		#endregion

		private readonly Subject<string> notifier;
		private string persisitentMessage = string.Empty;
		private string notificationMessage;

		#region Message 変更通知プロパティ

		/// <summary>
		/// 現在のステータスを示す文字列を取得します。
		/// </summary>
		public string Message
		{
			get => notificationMessage ?? persisitentMessage;
			set
			{
				if (persisitentMessage == value) return;
				persisitentMessage = value;
				RaiseMessagePropertyChanged();
			}
		}

		#endregion

		private StatusService()
		{
			notifier = new Subject<string>();
			notifier
				.Do(x =>
				{
					notificationMessage = x;
					RaiseMessagePropertyChanged();
				})
				.Throttle(TimeSpan.FromMilliseconds(5000))
				.Subscribe(_ =>
				{
					notificationMessage = null;
					RaiseMessagePropertyChanged();
				});
		}

		public void Set(string message) => Message = message;

		public void Notify(string message) => notifier.OnNext(message);

		// ReSharper disable once ExplicitCallerInfoArgument
		private void RaiseMessagePropertyChanged() => base.RaisePropertyChanged(nameof(Message));
	}
}
