using System.Collections.Generic;
using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json
{
    internal class PlayerTradeStatus
    {
        [JsonProperty(PropertyName = "assets")]
        internal List<Asset> Assets { get; set; }

        [JsonProperty(PropertyName = "ready")]
        internal bool Ready { get; set; }
    }
}
