using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json
{
    internal class Asset
    {
        [JsonProperty(PropertyName = "appid")]
        internal string AppId { get; set; }

        [JsonProperty(PropertyName = "contextid")]
        internal string ContextId { get; set; }

        [JsonProperty(PropertyName = "amount")]
        internal string Amount { get; set; }

        [JsonProperty(PropertyName = "assetid")]
        internal string AssetId { get; set; }
    }
}
