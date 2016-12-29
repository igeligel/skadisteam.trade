using skadisteam.trade.Factories;
using skadisteam.trade.Models;
using skadisteam.trade.Models.Json.AcceptingOffers;
using Xunit;

namespace skadisteam.trade.test.Factories
{
    public class SteamErrorFactoryTest
    {
        private const string SendingOfferFail =
            "There was an error sending your trade offer. Please try again later. (2)";
        private const string AcceptOfferFail =
            "There was an error accepting this trade offer.  Please try again later. (2)";
        private const string SendingOfferServiceUnavailable =
            "There was an error sending your trade offer. Please try again later. (20)";
        private const string AcceptOfferServiceUnavailable =
            "There was an error accepting this trade offer.  Please try again later. (20)";

        [Fact]
        public void SteamErrorResponseSendingOfferFailCheck()
        {
            var steamErrorResponse = new SteamErrorResponse
            {
                StrError = SendingOfferFail
            };
            var result = SteamErrorFactory.ParseError(steamErrorResponse);
            Assert.Equal(SteamError.Fail, result);
        }

        [Fact]
        public void SteamErrorResponseAcceptOfferFailCheck()
        {
            var steamErrorResponse = new SteamErrorResponse
            {
                StrError = AcceptOfferFail
            };
            var result = SteamErrorFactory.ParseError(steamErrorResponse);
            Assert.Equal(SteamError.Fail, result);
        }

        [Fact]
        public void SteamErrorResponseSendingOfferServiceUnavailableCheck()
        {
            var steamErrorResponse = new SteamErrorResponse
            {
                StrError = SendingOfferServiceUnavailable
            };
            var result = SteamErrorFactory.ParseError(steamErrorResponse);
            Assert.Equal(SteamError.ServiceUnavailable, result);
        }

        [Fact]
        public void SteamErrorResponseAcceptOfferServiceUnavailableCheck()
        {
            var steamErrorResponse = new SteamErrorResponse
            {
                StrError = AcceptOfferServiceUnavailable
            };
            var result = SteamErrorFactory.ParseError(steamErrorResponse);
            Assert.Equal(SteamError.ServiceUnavailable, result);
        }

        [Fact]
        public void SendingOfferFailCheck()
        {
            var result = SteamErrorFactory.ParseError(SendingOfferFail);
            Assert.Equal(SteamError.Fail, result);
        }

        [Fact]
        public void AcceptOfferFailCheck()
        {
            var result = SteamErrorFactory.ParseError(AcceptOfferFail);
            Assert.Equal(SteamError.Fail, result);
        }

        [Fact]
        public void SendingOfferServiceUnavailableCheck()
        {
            var result =
                SteamErrorFactory.ParseError(SendingOfferServiceUnavailable);
            Assert.Equal(SteamError.ServiceUnavailable, result);
        }

        [Fact]
        public void AcceptOfferServiceUnavailableCheck()
        {
            var result =
                SteamErrorFactory.ParseError(AcceptOfferServiceUnavailable);
            Assert.Equal(SteamError.ServiceUnavailable, result);
        }
    }
}
