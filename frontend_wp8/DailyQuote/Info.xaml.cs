using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System.Windows;

namespace DailyQuote
{
	public partial class Info : PhoneApplicationPage
	{
		public Info()
		{
			InitializeComponent();
		}

		private void OpenQuickCards(object sender, RoutedEventArgs e)
		{
			new MarketplaceDetailTask
			{
				ContentIdentifier = "bdc1b17f-8069-4753-b498-596d08b8fc60",
				ContentType = MarketplaceContentType.Applications
			}.Show();
		}
	}
}