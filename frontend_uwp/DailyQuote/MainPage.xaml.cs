using System;
using System.Linq;
using System.Threading.Tasks;
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
			await GetQuote();
		}

		private async void AppBarButton_RefreshClick(object sender, RoutedEventArgs e)
		{
			await GetQuote();
		}

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
	}
}
