using DailyQuote.Core;
using System;
using System.Linq;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace BackgroundTask
{
	public sealed class TileUpdateBackgroundTask : IBackgroundTask
	{
		public async void Run(IBackgroundTaskInstance taskInstance)
		{
			var deferral = taskInstance.GetDeferral();
			var quote = await DailyQuote.Core.Get.CurrentQuote();

			UpdateTile(quote);

			deferral.Complete();
		}

		static void UpdateTile(Get.Quote quote)
		{
			var updater = TileUpdateManager.CreateTileUpdaterForApplication();
			updater.EnableNotificationQueue(true);
			updater.Clear();

			{
				var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text09);
				var textElements = tileXml.GetElementsByTagName("text");
				textElements[1].InnerText = $"\"{quote.Text}\"";
				textElements[0].InnerText = quote.Author;
				updater.Update(new TileNotification(tileXml));
			}
			{
				var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text02);
				var textElements = tileXml.GetElementsByTagName("text");
				textElements[1].InnerText = $"\"{quote.Text}\"";
				textElements[0].InnerText = quote.Author;
				updater.Update(new TileNotification(tileXml));
			}
		}
	}
}
