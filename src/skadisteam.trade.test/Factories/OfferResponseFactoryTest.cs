using skadisteam.trade.Factories;
using skadisteam.trade.Models;
using skadisteam.trade.Models.Json.AcceptingOffers;
using skadisteam.trade.Models.TradeOffer;
using Xunit;

namespace skadisteam.trade.test.Factories
{
    public class OfferResponseFactoryTest
    {
        private static string GetAcceptedOffer(bool needsMobileConfirmation,
            bool needsEmailConfirmation, string emailDomain)
        {
            var needsMobileConfirmationText = needsMobileConfirmation
                ? "true"
                : "false";
            var needsEmailConfirmationText = needsEmailConfirmation
                ? "true"
                : "false";
            return $"{{\"tradeid\":null,\"needs_mobile_confirmation\":{needsMobileConfirmationText},\"needs_email_confirmation\":{needsEmailConfirmationText},\"email_domain\":\"{emailDomain}\"}}";
        }

        [Fact]
        public void SuccessfulResponseTypeCheck()
        {
            var inputText = GetAcceptedOffer(true, true, "gmail.com");
            var response = OfferResponseFactory.Create(inputText);
            Assert.Equal(typeof(MobileConfirmationResponse), response.GetType());
        }

        [Fact]
        public void SuccessfulResponseDomainGmailCheck()
        {
            var inputText = GetAcceptedOffer(true, true, "gmail.com");
            var response = OfferResponseFactory.Create(inputText);
            var mobileConfirmationResponse =
                (MobileConfirmationResponse)response;
            Assert.Equal("gmail.com", mobileConfirmationResponse.EmailDomain);
        }

        [Fact]
        public void SuccessfulResponseDomainOutlookCheck()
        {
            var inputText = GetAcceptedOffer(true, true, "outlook.com");
            var response = OfferResponseFactory.Create(inputText);
            var mobileConfirmationResponse =
                (MobileConfirmationResponse)response;
            Assert.Equal("outlook.com", mobileConfirmationResponse.EmailDomain);
        }

        [Fact]
        public void SuccessfulResponseNeedsMobileConfirmationCheck()
        {
            var inputText = GetAcceptedOffer(true, true, "gmail.com");
            var response = OfferResponseFactory.Create(inputText);
            var mobileConfirmationResponse =
                (MobileConfirmationResponse)response;
            Assert.True(mobileConfirmationResponse.NeedsMobileConfirmation);
        }

        [Fact]
        public void SuccessfulResponseNotNeedsMobileConfirmationCheck()
        {
            var inputText = GetAcceptedOffer(false, true, "gmail.com");
            var response = OfferResponseFactory.Create(inputText);
            var mobileConfirmationResponse =
                (MobileConfirmationResponse)response;
            Assert.False(mobileConfirmationResponse.NeedsMobileConfirmation);
        }

        [Fact]
        public void SuccessfulResponseNeedsEmailConfirmationCheck()
        {
            var inputText = GetAcceptedOffer(true, true, "gmail.com");
            var response = OfferResponseFactory.Create(inputText);
            var mobileConfirmationResponse =
                (MobileConfirmationResponse)response;
            Assert.True(mobileConfirmationResponse.NeedsEmailConfirmation);
        }

        [Fact]
        public void SuccessfulResponseNotNeedsEmailConfirmationCheck()
        {
            var inputText = GetAcceptedOffer(true, false, "gmail.com");
            var response = OfferResponseFactory.Create(inputText);
            var mobileConfirmationResponse =
                (MobileConfirmationResponse)response;
            Assert.False(mobileConfirmationResponse.NeedsEmailConfirmation);
        }

        [Fact]
        public void ErrorTypeCheck()
        {
            const string inputText =
                "{\"strError\":\"There was an error accepting this trade offer.  Please try again later. (11)\"}";
            var response = OfferResponseFactory.Create(inputText);
            Assert.Equal(typeof(AcceptOfferErrorResponse), response.GetType());
        }

        [Fact]
        public void ErrorInvalidStateCheck()
        {
            const string inputText =
                "{\"strError\":\"There was an error accepting this trade offer.  Please try again later. (11)\"}";
            var response =
                (AcceptOfferErrorResponse)
                OfferResponseFactory.Create(inputText);
            Assert.Equal(SteamError.InvalidState, response.SteamError);
        }

        [Fact]
        public void ErrorTimeoutCheck()
        {
            const string inputText =
                "{\"strError\":\"There was an error accepting this trade offer.  Please try again later. (16)\"}";
            var response =
                (AcceptOfferErrorResponse)
                OfferResponseFactory.Create(inputText);
            Assert.Equal(SteamError.Timeout, response.SteamError);
        }

        [Fact]
        public void TradeReceiptTypeCheck()
        {
            const string inputText = "{\"tradeid\":\"1038475776073187653\"}";
            var response = OfferResponseFactory.Create(inputText);
            Assert.Equal(typeof(TradeReceiptResponse), response.GetType());
        }

        [Fact]
        public void FirstTradeReceiptCheck()
        {
            const string inputText = "{\"tradeid\":\"1038475776073187653\"}";
            var response =
                (TradeReceiptResponse) OfferResponseFactory.Create(inputText);
            Assert.Equal(1038475776073187653, response.TradeId);
        }

        [Fact]
        public void SecondTradeReceiptCheck()
        {
            const string inputText = "{\"tradeid\":\"1038475776073187654\"}";
            var response =
                (TradeReceiptResponse)OfferResponseFactory.Create(inputText);
            Assert.Equal(1038475776073187654, response.TradeId);
        }
    }
}
