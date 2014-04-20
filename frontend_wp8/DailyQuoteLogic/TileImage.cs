using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DailyQuoteLogic
{
	internal static class TileImage
	{
		public static Uri Create(string filename, string text, ImageSource img, SolidColorBrush backgroundColor, double textSize, bool titleClear, Size tileSize)
		{
			var background = new Rectangle();
			background.Width = tileSize.Width;
			background.Height = tileSize.Height;
			background.Fill = backgroundColor;

			var image = new Image();
			image.Source = img;
			image.Width = tileSize.Width - 4;
			image.Height = tileSize.Height - 4;
			image.Stretch = Stretch.Uniform;

			TextBlock textBlock = new TextBlock();
			textBlock.Width = tileSize.Width - 30;
			textBlock.Height = tileSize.Height - 30;
			textBlock.TextWrapping = TextWrapping.Wrap;
			textBlock.Text = text;
			textBlock.FontSize = textSize;
			textBlock.Foreground = new SolidColorBrush(img != null ? Colors.Black : Colors.White);
			textBlock.FontFamily = new FontFamily("Segoe WP");

			var textShadow = new Rectangle();
			textShadow.Width = textBlock.ActualWidth + 12;
			textShadow.Height = textBlock.ActualHeight + 12;
			textShadow.Opacity = 0.8;
			textShadow.Fill = new SolidColorBrush(Colors.White);

			var titleShadow = new Rectangle();
			titleShadow.Width = tileSize.Width;
			titleShadow.Height = 50;
			titleShadow.Opacity = 0.7;
			titleShadow.Fill = backgroundColor;

			var tileImage = "/Shared/ShellContent/" + filename;
			using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
			{
				var bitmap = new WriteableBitmap((int)tileSize.Width, (int)tileSize.Height);
				bitmap.Render(background, new TranslateTransform());
				bitmap.Render(image, new TranslateTransform() { X = 2, Y = 2 });
				if (img != null) bitmap.Render(textShadow, new TranslateTransform() { X = 5 - 2, Y = 5 - 2 });
				bitmap.Render(textBlock, new TranslateTransform() { X = 15, Y = 5 });
				if (titleClear) bitmap.Render(titleShadow, new TranslateTransform() { X = 0, Y = tileSize.Height - titleShadow.Height });
				var stream = store.CreateFile(tileImage);
				bitmap.Invalidate();
				bitmap.SaveJpeg(stream, (int)tileSize.Width, (int)tileSize.Height, 0, 100);
				stream.Close();
			}
			return new Uri("isostore:" + tileImage, UriKind.Absolute);
		}
	}
}
