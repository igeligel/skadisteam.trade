using Newtonsoft.Json;

namespace skadisteam.trade.Models.Json
{
    internal class TimeQueryResponse
    {
        [JsonProperty("server_time")]
        public long ServerTime { get; set; }
    }
}
