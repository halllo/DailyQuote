using System.IO.IsolatedStorage;

namespace DailyQuoteLogic
{
	public static class Settings
	{
		private const string IS_RandomizeTileColor_Key = "dailyQuoteTileRandomColor";
		public static bool RandomizeTileColor
		{
			get { return GetSetting(IS_RandomizeTileColor_Key, true); }
			set { SetSetting(IS_RandomizeTileColor_Key, value); }
		}

		private const string IS_RandomizeLockScreenColor_Key = "dailyQuoteLockScreenRandomColor";
		public static bool RandomizeLockScreenColor
		{
			get { return GetSetting(IS_RandomizeLockScreenColor_Key, true); }
			set { SetSetting(IS_RandomizeLockScreenColor_Key, value); }
		}

		private const string IS_AccentColor_Key = "dailyQuoteAccentColor";
		public static System.Windows.Media.Color AccentColor
		{
			get { return GetSetting(IS_AccentColor_Key, System.Windows.Media.Colors.Blue); }
			set { SetSetting(IS_AccentColor_Key, value); }
		}

		private static T GetSetting<T>(string settingKey, T @default)
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			return settings.Contains(settingKey) ? (T)settings[settingKey] : @default;
		}

		private static void SetSetting<T>(string settingKey, T value)
		{
			var settings = IsolatedStorageSettings.ApplicationSettings;
			if (!settings.Contains(settingKey))
				settings.Add(settingKey, default(T));
			settings[settingKey] = value;
			settings.Save();
		}
	}
}
