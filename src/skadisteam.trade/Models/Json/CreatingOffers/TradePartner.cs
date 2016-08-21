using System.Collections.Generic;
using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json.CreatingOffers
{
    internal class TradePartner
    {
        [JsonProperty(PropertyName = "assets")]
        internal List<Asset> Assets { get; set; }
        [JsonProperty(PropertyName = "currency")]
        internal List<object> Currency { get; set; }
        [JsonProperty(PropertyName = "ready")]
        internal bool Ready { get; set; }
    }
}
