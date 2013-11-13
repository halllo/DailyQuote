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
			DailyQuoteLogic.DailyQuoteLogic.GetQuote().ContinueWith(t =>
			{
				var dailyQuote = t.Result;
				DailyQuoteLogic.DailyQuoteLogic.Cache(dailyQuote);
				DailyQuoteLogic.DailyQuoteLogic.UpdateTile(dailyQuote).ContinueWith(success =>
				{
					NotifyComplete();
				});
			});
		}
	}
}