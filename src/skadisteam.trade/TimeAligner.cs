using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using skadisteam.trade.Models.Json;

namespace skadisteam.trade
{
    public class TimeAligner
    {
        private static bool _aligned;
        private static int _timeDifference;

        public static long GetSteamTime()
        {
            if (!_aligned)
            {
                AlignTime();
            }
            return Util.GetSystemUnixTime() + _timeDifference;
        }

        private static void AlignTime()
        {
            var currentTime = Util.GetSystemUnixTime();
            var httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                AutomaticDecompression =
                    DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = new Uri("https://api.steampowered.com");
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("steamid", "0")
                });
                var result = client.PostAsync("/ITwoFactorService/QueryTime/v0001", content).Result;
                var resultContent = result.Content.ReadAsStringAsync().Result;
                var query = JsonConvert.DeserializeObject<TimeQuery>(resultContent);
                _timeDifference = (int)(query.Response.ServerTime - currentTime);
                _aligned = true;
            }
        }
    }
}
