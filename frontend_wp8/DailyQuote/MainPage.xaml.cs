using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System;
using System.Windows;

namespace DailyQuote
{
	public partial class MainPage : PhoneApplicationPage
	{
		public MainPage()
		{
			InitializeComponent();
			headerDate.Text = DateTime.Now.ToShortDateString();
			Loaded += MainPage_Loaded;
		}

		async void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			new UpdateBackgroundAgent().StartPeriodicAgent();

			var quote = DailyQuoteLogic.DailyQuoteLogic.GetCachedDailyQuote();
			if (quote != null)
			{
				quoteTextBlock.Text = quote.ToString();
				quoteTextBlock.Background = DailyQuoteLogic.DailyQuoteLogic.RandomColor();
				quoteTextBlock.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				pintToStartHint.Visibility = System.Windows.Visibility.Visible;
			}
		}

		private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
		{
			new WebBrowserTask { Uri = new Uri("http://www.brainyquote.com/quotes_of_the_day.html", UriKind.Absolute) }.Show();
		}
	}
}