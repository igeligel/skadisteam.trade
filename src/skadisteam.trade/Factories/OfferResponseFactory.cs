using skadisteam.trade.Converter;
using skadisteam.trade.Interfaces;
using skadisteam.trade.Models;
using skadisteam.trade.Models.Json.AcceptingOffers;
using skadisteam.trade.Models.TradeOffer;

namespace skadisteam.trade.Factories
{
    internal static class OfferResponseFactory
    {
        internal static IAcceptOfferResponse Create(string responseBody)
        {
            if (responseBody.Contains("needs_mobile_confirmation"))
            {
                var mobileConfirmationResponse =
                (MobileConfirmationResponse)
                JsonToAcceptOfferResponse
                    .ParseAcceptOffer<MobileConfirmationResponse>(responseBody);
                if (mobileConfirmationResponse != null)
                    return mobileConfirmationResponse;
            }
            if (responseBody.Contains("strError"))
            {
                var steamErrorResponse =
                (SteamErrorResponse)
                JsonToAcceptOfferResponse.ParseAcceptOffer<SteamErrorResponse>(
                    responseBody);

                var acceptOfferErrorResponse = new AcceptOfferErrorResponse
                {
                    SteamError = SteamError.Undefined
                };
                if (steamErrorResponse == null) return acceptOfferErrorResponse;
                acceptOfferErrorResponse.SteamError =
                    SteamErrorFactory.ParseError(steamErrorResponse);
                return acceptOfferErrorResponse;
            }
            if (!responseBody.Contains("tradeid")) return null;
            var tradeReceiptResponse =
                (TradeReceiptResponse)
                JsonToAcceptOfferResponse.ParseAcceptOffer<TradeReceiptResponse>
                    (responseBody);
            return tradeReceiptResponse;
        }
    }
}
