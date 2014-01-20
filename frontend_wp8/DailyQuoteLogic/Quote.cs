using System.Collections.Generic;
using System.Linq;
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
			var notEmptyMatches = matches.ToEnumerable().Where(m => string.IsNullOrEmpty(m) == false).ToList();
			Text = string.Format("{0}", notEmptyMatches[0]);
			Author = notEmptyMatches[1];
		}

		public override string ToString()
		{
			return string.Format("\"{0}\" ~ {1}", Text, Author);
		}
	}

	public static class MatchCollectionExtensions
	{
		public static IEnumerable<string> ToEnumerable(this MatchCollection matches)
		{
			for (int i = 0; i < matches.Count; i++)
			{
				yield return matches[i].Value;
			}
		}
	}
}