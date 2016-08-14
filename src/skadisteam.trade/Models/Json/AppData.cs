using System.Collections.Generic;
using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json
{
    internal class AppData
    {
        [JsonProperty(PropertyName = "appid")]
        internal int AppId { get; set; }

        [JsonProperty(PropertyName = "name")]
        internal string Name { get; set; }

        [JsonProperty(PropertyName = "icon")]
        internal string Icon { get; set; }

        [JsonProperty(PropertyName = "link")]
        internal string Link { get; set; }

        [JsonProperty(PropertyName = "asset_count")]
        internal int AssetCount { get; set; }

        [JsonProperty(PropertyName = "inventory_logo")]
        internal string InventoryLogo { get; set; }

        [JsonProperty(PropertyName = "trade_permissions")]
        internal string TradePermissions { get; set; }

        [JsonProperty(PropertyName = "rgContexts")]
        internal Dictionary<string, AppContext> RgContexts { get; set; }
    }
}
