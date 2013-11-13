using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyQuoteLogicTests
{
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void GetQuote_ReturnsQuoteHtml()
		{
			var quoteFuture = DailyQuoteLogic.DailyQuoteLogic.GetQuoteFromWebsite();
			quoteFuture.Wait();
			var quote = quoteFuture.Result;
			Assert.IsTrue(string.IsNullOrEmpty(quote) == false);
		}


		[TestMethod]
		public void Parse_Html_TextAndAuhtor()
		{
			var quote = new DailyQuoteLogic.Quote(
@"<div class=""bqQuoteLink"">The return we reap from generous actions is not always evident.</div>
<a href=""/quotes/quotes/f/francescog101492.html"">Francesco Guicciardini</a>");

			Assert.AreEqual("The return we reap from generous actions is not always evident.", quote.Text);
			Assert.AreEqual("Francesco Guicciardini", quote.Author);
		}
	}
}
