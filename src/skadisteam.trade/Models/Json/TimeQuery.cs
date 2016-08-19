using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json
{
    internal class TimeQuery
    {
        [JsonProperty("response")]
        internal TimeQueryResponse Response { get; set; }
    }
}
