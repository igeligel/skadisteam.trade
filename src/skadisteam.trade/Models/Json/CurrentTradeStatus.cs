using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json
{
    internal class CurrentTradeStatus
    {
        [JsonProperty(PropertyName = "newversion")]
        internal bool NewVersion { get; set; }

        [JsonProperty(PropertyName = "version")]
        internal int Version { get; set; }

        [JsonProperty(PropertyName = "me")]
        internal PlayerTradeStatus Me { get; set; }

        [JsonProperty(PropertyName = "them")]
        internal PlayerTradeStatus Them { get; set; }
    }
}
