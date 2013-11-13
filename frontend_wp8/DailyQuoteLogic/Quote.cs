using System.Text.RegularExpressions;

namespace DailyQuoteLogic
{
	public class Quote
	{
		public string Text { get; set; }
		public string Author { get; set; }

		public Quote()
		{
		}

		internal Quote(string html)
		{
			var matches = Regex.Matches(html, @"(?<=\>).*?(?=<)", RegexOptions.Multiline);
			Text = string.Format("\"{0}\"", matches[0].Value);
			Author = matches[1].Value;
		}

		public override string ToString()
		{
			return string.Format("{0}\r\n- {1}", Text, Author);
		}
	}
}