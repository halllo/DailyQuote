using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DailyQuoteLogicTests
{
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void IsInValidCacheTime_1hStillCurrent()
		{
			Assert.IsTrue(DailyQuoteLogic.Caching.IsCacheTimeDeemedCurrent(
				cacheTime: new DateTime(2000, 1, 1, 1, 0, 0),
				now: new DateTime(2000, 1, 1, 2, 0, 0)));
		}
		[TestMethod]
		public void IsInValidCacheTime_12hStillCurrent()
		{
			Assert.IsTrue(DailyQuoteLogic.Caching.IsCacheTimeDeemedCurrent(
				cacheTime: new DateTime(2000, 1, 1, 1, 0, 0),
				now: new DateTime(2000, 1, 1, 13, 0, 0)));
		}
		[TestMethod]
		public void IsInValidCacheTime_13hOutOfDate()
		{
			Assert.IsFalse(DailyQuoteLogic.Caching.IsCacheTimeDeemedCurrent(
				cacheTime: new DateTime(2000, 1, 1, 1, 0, 0),
				now: new DateTime(2000, 1, 1, 14, 0, 0)));
		}

		[TestMethod]
		public void GetQuote_ReturnsQuoteHtml()
		{
			var quoteFuture = DailyQuoteLogic.Get.QuoteFromWebsite();
			quoteFuture.Wait();
			var quote = quoteFuture.Result;
			Assert.IsTrue(string.IsNullOrEmpty(quote) == false);
		}

		[TestMethod]
		public void Parse_Html_TextAndAuhtor()
		{
			var quote = new DailyQuoteLogic.Quote(
@"class=""bqQuoteLink""><a title=""view quote"" href=""/quotes/quotes/m/martinluth101309.html"">We must learn to live together as brothers or perish together as fools.</a></span><br>
<span class=""bodybold""><a title=""view author"" href=""/quotes/authors/m/martin_luther_king_jr.html"">Martin Luther King, Jr.</a></span>
</div>");

			Assert.AreEqual("We must learn to live together as brothers or perish together as fools.", quote.Text);
			Assert.AreEqual("Martin Luther King, Jr.", quote.Author);
		}
	}
}
