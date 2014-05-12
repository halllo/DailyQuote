using Microsoft.Phone.Scheduler;
using System.Diagnostics;
using System.Windows;

namespace DailyQuoteBackgroundAgent
{
	public class ScheduledAgent : ScheduledTaskAgent
	{
		static ScheduledAgent()
		{
			Deployment.Current.Dispatcher.BeginInvoke(delegate
			{
				Application.Current.UnhandledException += UnhandledException;
			});
		}

		private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}
		}

		protected override void OnInvoke(ScheduledTask task)
		{
			if (DailyQuoteLogic.Caching.IsQuoteCurrent() == false)
			{
				DailyQuoteLogic.Get.Quote().ContinueWith(t =>
				{
					var dailyQuote = t.Result;
					DailyQuoteLogic.Caching.Cache(dailyQuote);
					DailyQuoteLogic.Caching.RememberQuoteAsCurrent();
					DailyQuoteLogic.PhoneIntegration.UpdateTileAndLockscreen(dailyQuote).ContinueWith(success =>
					{
						NotifyComplete();
					});
				});
			}
		}
	}
}