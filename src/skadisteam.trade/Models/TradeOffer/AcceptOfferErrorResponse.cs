using skadisteam.trade.Interfaces;

namespace skadisteam.trade.Models.TradeOffer
{
    public class AcceptOfferErrorResponse: IAcceptOfferResponse
    {
        public SteamError SteamError { get; set; }
    }
}
