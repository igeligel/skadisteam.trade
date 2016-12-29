using skadisteam.trade.Factories;
using Xunit;

namespace skadisteam.trade.test.Factories
{
    public class UrlFactoryTest
    {
        [Fact]
        public void SteamCommunityHomeGabenCheck()
        {
            var path = UrlFactory.SteamCommunityHome(76561197960287930);
            Assert.Equal(
                "http://steamcommunity.com/profiles/76561197960287930/home",
                path);
        }

        [Fact]
        public void SteamCommunityHomeBenDotComCheck()
        {
            var path = UrlFactory.SteamCommunityHome(76561197993404877);
            Assert.Equal(
                "http://steamcommunity.com/profiles/76561197993404877/home",
                path);
        }

        [Fact]
        public void TradeOfferPageGabenCheck()
        {
            var path = UrlFactory.TradeOfferPage(76561197960287930);
            Assert.Equal(
                "http://steamcommunity.com/profiles/76561197960287930/tradeoffers/",
                path);
        }

        [Fact]
        public void TradeOfferPageBenDotComCheck()
        {
            var path = UrlFactory.TradeOfferPage(76561197993404877);
            Assert.Equal(
                "http://steamcommunity.com/profiles/76561197993404877/tradeoffers/",
                path);
        }

        [Fact]
        public void FirstTradeOfferCheck()
        {
            var path = UrlFactory.TradeOffer(1742913322);
            Assert.Equal(
                "https://steamcommunity.com/tradeoffer/1742913322/",
                path);
        }

        [Fact]
        public void SecondTradeOfferCheck()
        {
            var path = UrlFactory.TradeOffer(1742913420);
            Assert.Equal(
                "https://steamcommunity.com/tradeoffer/1742913420/",
                path);
        }

        // TODO:
        [Fact]
        public void TradeOfferExecutionGabenCheck()
        {
            var path = UrlFactory.TradeOfferExecution(76561197960287930,
                "WTgOCOot");
            Assert.Equal(
                "https://steamcommunity.com/tradeoffer/new/?partner=22202&token=WTgOCOot",
                path);
            // 76561197960265728
        }

        [Fact]
        public void TradeOfferExecutionBenDotComCheck()
        {
            var path = UrlFactory.TradeOfferExecution(76561197993404877,
                "caFgHzkM");
            Assert.Equal(
                "https://steamcommunity.com/tradeoffer/new/?partner=33139149&token=caFgHzkM",
                path);
        }
    }
}
