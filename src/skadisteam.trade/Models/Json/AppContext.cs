using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json
{
    internal class AppContext
    {
        [JsonProperty(PropertyName = "asset_count")]
        internal int AssetCount { get; set; }

        [JsonProperty(PropertyName = "id")]
        internal int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        internal string Name { get; set; }
    }
}
