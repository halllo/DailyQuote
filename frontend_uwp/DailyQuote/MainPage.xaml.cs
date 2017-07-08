using DailyQuote.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DailyQuote
{
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
			this.Loaded += MainPage_Loaded;
		}

		private async void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			RegisterBackgroundTask();
			await GetQuote();
		}

		private async void AppBarButton_RefreshClick(object sender, RoutedEventArgs e)
		{
			await GetQuote();
		}

		private async void RegisterBackgroundTask()
		{
			var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
			if (backgroundAccessStatus == BackgroundAccessStatus.AlwaysAllowed ||
				backgroundAccessStatus == BackgroundAccessStatus.AllowedSubjectToSystemPolicy)
			{
				foreach (var task in BackgroundTaskRegistration.AllTasks)
				{
					if (task.Value.Name == taskName)
					{
						task.Value.Unregister(true);
					}
				}

				BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder();
				taskBuilder.Name = taskName;
				taskBuilder.TaskEntryPoint = taskEntryPoint;
				taskBuilder.SetTrigger(new TimeTrigger(240, false));
				var registration = taskBuilder.Register();
			}
		}
		private const string taskName = "TileUpdateBackgroundTask";
		private const string taskEntryPoint = "BackgroundTask.TileUpdateBackgroundTask";

		private async Task GetQuote()
		{
			this.quoteLoadingProgress.Visibility = Visibility.Visible;
			try
			{
				var quote = await Get.CurrentQuote();
				this.quoteTextBlock.Text = $"\"{quote.Text}\"\n\n{quote.Author}";
			}
			catch (Exception ex)
			{
				await Message(ex.Message);
			}
			this.quoteLoadingProgress.Visibility = Visibility.Collapsed;
		}

		public static async Task Message(string text)
		{
			await new MessageDialog(text).ShowAsync();
		}

		private void AppBarButton_LikeClick(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(InfoPage));
		}
	}
}
