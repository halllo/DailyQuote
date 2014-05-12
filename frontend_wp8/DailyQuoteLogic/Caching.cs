using System;
using System.IO.IsolatedStorage;

namespace DailyQuoteLogic
{
	public static class Caching
	{
		private const string IS_DailyQuoteCacheTime_Key = "dailyQuoteCacheTime";
		public static bool IsQuoteCurrent()
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			var dailyQuoteCacheTime = settings.Contains(IS_DailyQuoteCacheTime_Key) ? (DateTime)settings[IS_DailyQuoteCacheTime_Key] : new DateTime();
			return IsCacheTimeDeemedCurrent(dailyQuoteCacheTime, DateTime.Now);
		}

		public static void RememberQuoteAsCurrent()
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			if (!settings.Contains(IS_DailyQuoteCacheTime_Key))
				settings.Add(IS_DailyQuoteCacheTime_Key, new DateTime());
			settings[IS_DailyQuoteCacheTime_Key] = DateTime.Now;
			settings.Save();
		}

		internal static bool IsCacheTimeDeemedCurrent(DateTime cacheTime, DateTime now)
		{
			return cacheTime.AddHours(12) >= now;
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
