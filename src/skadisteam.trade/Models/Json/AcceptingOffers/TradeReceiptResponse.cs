using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json.AcceptingOffers
{
    public class TradeReceiptResponse
    {
        [JsonProperty(PropertyName = "tradeid")]
        public long TradeId { get; set; }
    }
}
