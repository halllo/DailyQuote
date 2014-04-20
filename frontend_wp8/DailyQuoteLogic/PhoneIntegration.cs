using Microsoft.Phone.Shell;
using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DailyQuoteLogic
{
	public static class PhoneIntegration
	{
		public static Task<bool> UpdateTile(Quote quote)
		{
			TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();
			Deployment.Current.Dispatcher.BeginInvoke(new Action(() =>
			{
				try
				{
					var text = string.Format("\"{0}\"", quote.Text);

					var mediumFrontJpg = TileImage.Create("mediumFront.jpg", text, null, RandomColor(), 45, true, new Size(336, 336));
					var mediumBackJpg = TileImage.Create("mediumBack.jpg", text, null, RandomColor(), 45, true, new Size(336, 336));
					var wideFrontJpg = TileImage.Create("wideFront.jpg", text, null, RandomColor(), 45, true, new Size(691, 336));
					var wideBackJpg = TileImage.Create("wideBack.jpg", text, null, RandomColor(), 45, true, new Size(691, 336));

					ShellTile.ActiveTiles.First().Update(new FlipTileData()
					{
						Title = quote.Author,
						BackTitle = quote.Author,

						BackgroundImage = mediumFrontJpg,
						BackBackgroundImage = mediumBackJpg,

						WideBackgroundImage = wideFrontJpg,
						WideBackBackgroundImage = wideBackJpg
					});

					completionSource.SetResult(true);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					completionSource.SetResult(false);
				}
			}));

			return completionSource.Task;
		}

		public static SolidColorBrush RandomColor()
		{
			var colors = new[] {
					Color.FromArgb(255, 168,196,0),
					Color.FromArgb(255, 96,169,23),
					Color.FromArgb(255, 0,138,0),
					Color.FromArgb(255, 0,171,169),
					Color.FromArgb(255, 27,161,226),
					Color.FromArgb(255, 0,80,239),
					Color.FromArgb(255, 106,0,255),
					Color.FromArgb(255, 170,0,255),
					Color.FromArgb(255, 244,141,208),
					Color.FromArgb(255, 216,0,115),
					Color.FromArgb(255, 162,0,37),
					Color.FromArgb(255, 229,20,0),
					Color.FromArgb(255, 250,104,0),
					Color.FromArgb(255, 240,163,10),
					Color.FromArgb(255, 227,200,0),
					Color.FromArgb(255, 130,90,44),
					Color.FromArgb(255, 109,135,100),
					Color.FromArgb(255, 100,118,135),
					Color.FromArgb(255, 118,96,138),
					Color.FromArgb(255, 160,82,45),
				};

			int random = new Random().Next(colors.Length);
			return new SolidColorBrush(colors[random]);
		}
	}
}
