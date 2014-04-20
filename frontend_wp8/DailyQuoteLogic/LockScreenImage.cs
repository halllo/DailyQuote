using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DailyQuoteLogic
{
	internal static class LockScreenImage
	{
		public static Uri Create(string filename, string text, SolidColorBrush backgroundColor, double textSize, Size size)
		{
			var background = new Rectangle();
			background.Width = size.Width;
			background.Height = size.Height;
			background.Fill = backgroundColor;

			var textBlock = new TextBlock();
			textBlock.Width = 500;
			textBlock.Height = 500;
			textBlock.TextWrapping = TextWrapping.Wrap;
			textBlock.Text = text;
			textBlock.FontSize = textSize;
			textBlock.Foreground = new SolidColorBrush(Colors.White);
			textBlock.FontFamily = new FontFamily("Segoe WP");

			var tileImage = "/Shared/ShellContent/" + filename;
			using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
			{
				var bitmap = new WriteableBitmap((int)size.Width, (int)size.Height);
				bitmap.Render(background, new TranslateTransform());
				bitmap.Render(textBlock, new TranslateTransform() { X = 39, Y = 88 });
				var stream = store.CreateFile(tileImage);
				bitmap.Invalidate();
				bitmap.SaveJpeg(stream, (int)size.Width, (int)size.Height, 0, 80);
				stream.Close();
			}
			return new Uri("ms-appdata:///local" + tileImage, UriKind.Absolute);
		}
	}
}
