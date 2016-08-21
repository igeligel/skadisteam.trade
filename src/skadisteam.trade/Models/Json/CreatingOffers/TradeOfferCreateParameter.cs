using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json.CreatingOffers
{
    public class TradeOfferCreateParameter
    {
        [JsonProperty(PropertyName = "trade_offer_access_token")]
        public string TradeOfferAccessToken { get; set; }
    }
}
