using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json.CreatingOffers
{
    internal class CreateOffer
    {
        [JsonProperty(PropertyName = "newversion")]
        public bool NewVersion { get; set; }

        [JsonProperty(PropertyName = "version")]
        internal int Version { get; set; }

        [JsonProperty(PropertyName = "me")]
        public TradePartner Me { get; set; }
        
        [JsonProperty(PropertyName = "them")]
        public TradePartner Them { get; set; }
    }
}
