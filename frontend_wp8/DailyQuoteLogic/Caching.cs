using System;
using System.IO.IsolatedStorage;

namespace DailyQuoteLogic
{
	public static class Caching
	{
		private const string IS_DailyQuoteDay_Key = "dailyQuoteDay";
		public static bool IsQuoteOfToday()
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			var dailyQuoteDay = settings.Contains(IS_DailyQuoteDay_Key) ? (int)settings[IS_DailyQuoteDay_Key] : 0;
			return dailyQuoteDay == DateTime.Today.Day;
		}

		public static void RememberTodayAsQuoteDay()
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			if (!settings.Contains(IS_DailyQuoteDay_Key))
				settings.Add(IS_DailyQuoteDay_Key, 0);
			settings[IS_DailyQuoteDay_Key] = DateTime.Today.Day;
			settings.Save();
		}


		private const string IS_DailyQuote_Key = "dailyQuote";
		public static void Cache(Quote dailyQuote)
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			if (!settings.Contains(IS_DailyQuote_Key))
				settings.Add(IS_DailyQuote_Key, null);
			settings[IS_DailyQuote_Key] = dailyQuote;
			settings.Save();
		}

		public static Quote GetCachedDailyQuote()
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			return settings.Contains(IS_DailyQuote_Key) ? settings[IS_DailyQuote_Key] as Quote : null;
		}
	}
}
