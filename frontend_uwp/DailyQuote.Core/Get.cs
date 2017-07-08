using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DailyQuote.Core
{
	public static class Get
	{
		public static async Task<Quote> CurrentQuote()
		{
			var httpClient = new HttpClient();
			var brainyQuoteHtml = await httpClient.GetStringAsync("http://www.brainyquote.com/quotes_of_the_day.html?" + DateTime.Now.Ticks);
			var quotesHtml = Regex.Matches(brainyQuoteHtml, @"<div class=""clearfix"">(.*?)</div>", RegexOptions.Singleline);
			var quoteHtml = quotesHtml[0];

			return new Quote(quoteHtml.Value);
		}

		public class Quote
		{
			public string Text { get; set; }
			public string Author { get; set; }
			
			internal Quote(string html)
			{
				var matches = Regex.Matches(html, @"(?<=\>).*?(?=<)", RegexOptions.Multiline);
				var notEmptyMatches = MatchCollectionExtensions.ToEnumerable(matches).Where(m => string.IsNullOrEmpty(m) == false).ToList();
				
				Text = UnEscape(string.Format("{0}", notEmptyMatches[0]));
				Author = UnEscape(notEmptyMatches[1]);
			}

			private static string UnEscape(string str)
			{
				return str.Replace("&amp;", "&")
						  .Replace("&gt;", ">")
						  .Replace("&lt;", "<")
						  .Replace("&quot;", "\"")
						  .Replace("&apos;", "\'")
						  .Replace("&#39;", "\'")
						  ;
			}
		}

		private static class MatchCollectionExtensions
		{
			public static IEnumerable<string> ToEnumerable(MatchCollection matches)
			{
				for (int i = 0; i < matches.Count; i++)
				{
					yield return matches[i].Value;
				}
			}
		}
	}
}
