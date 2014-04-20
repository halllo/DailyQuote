using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DailyQuoteLogic
{
	public static class Get
	{
		public static async Task<string> QuoteFromWebsite()
		{
			var httpClient = new HttpClient();
			var brainyQuoteHtml = await httpClient.GetStringAsync("http://www.brainyquote.com/quotes_of_the_day.html?" + DateTime.Now.Ticks);
			var quotesHtml = Regex.Matches(brainyQuoteHtml, "class=\"bqQuoteLink\">.*?</div>", RegexOptions.Singleline);
			var quoteHtml = quotesHtml[0];
			return quoteHtml.Value;
		}

		public static async Task<Quote> Quote()
		{
			return new Quote(await QuoteFromWebsite());
		}

	}
}