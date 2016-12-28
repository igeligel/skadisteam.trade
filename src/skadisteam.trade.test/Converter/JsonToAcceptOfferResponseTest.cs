using skadisteam.trade.Models.Json.AcceptingOffers;
using Xunit;

namespace skadisteam.trade.test.Converter
{
    public class JsonToAcceptOfferResponseTest
    {
        [Fact]
        public void MobileConfirmationResponseCheck()
        {
            /*{  
                "tradeid":9223372036854775807,
                "needs_mobile_confirmation":true,
                "needs_email_confirmation":false,
                "email_domain":"gmail.com"
            }*/
            const string jsonString =
                "{\"tradeid\":9223372036854775807,\"needs_mobile_confirmation\":true,\"needs_email_confirmation\":false,\"email_domain\":\"gmail.com\"}";
            var mobileConfirmationResponse =
                (MobileConfirmationResponse)
                trade.Converter.JsonToAcceptOfferResponse
                    .ParseAcceptOffer<MobileConfirmationResponse>(jsonString);
            Assert.Equal(typeof(MobileConfirmationResponse),
                mobileConfirmationResponse.GetType());
            Assert.Equal(9223372036854775807, mobileConfirmationResponse.TradeId);
            Assert.True(mobileConfirmationResponse.NeedsMobileConfirmation);
            Assert.False(mobileConfirmationResponse.NeedsEmailConfirmation);
            Assert.Equal("gmail.com", mobileConfirmationResponse.EmailDomain);
        }

        [Fact]
        public void TradeReceiptResponseCheck()
        {
            /*{  
                "tradeid":9223372036854775807
            }*/
            const string jsonString = "{\"tradeid\":9223372036854775805}";
            var tradeReceiptResponse =
                (TradeReceiptResponse)
                trade.Converter.JsonToAcceptOfferResponse.ParseAcceptOffer<TradeReceiptResponse>
                    (jsonString);
            Assert.Equal(typeof(TradeReceiptResponse),
                tradeReceiptResponse.GetType());
            Assert.Equal(9223372036854775805, tradeReceiptResponse.TradeId);
        }

        public void SteamErrorResponseCheck()
        {
            /*{  
                "StrError":"There was an error accepting this trade offer. Please try again later. (28)"
            }*/
            const string jsonString =
                "{\"StrError\":\"There was an error accepting this trade offer. Please try again later. (28)\"}";
            var steamErrorResponse =
                (SteamErrorResponse)
                trade.Converter.JsonToAcceptOfferResponse
                    .ParseAcceptOffer<SteamErrorResponse>(jsonString);
            Assert.Equal(typeof(SteamErrorResponse),
                steamErrorResponse.GetType());
            Assert.Equal(
                "There was an error accepting this trade offer. Please try again later. (28)",
                steamErrorResponse.StrError);
        }
    }
}
