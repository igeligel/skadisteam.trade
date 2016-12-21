using Newtonsoft.Json;
using skadisteam.trade.Interfaces;

namespace skadisteam.trade.Models.Json.AcceptingOffers
{
    public class TradeReceiptResponse : IAcceptOfferResponse
    {
        [JsonProperty(PropertyName = "tradeid")]
        public long TradeId { get; set; }
    }
}
