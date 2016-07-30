using System;
using System.Net.Http;
using skadisteam.trade.Constants;

namespace skadisteam.trade.Factories
{
    internal static class RequestFactory
    {
        internal static HttpResponseMessage GetTradeOfferResponseMessage(
            HttpClientHandler httpClientHandler, string path, long steamCommunityId)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = new Uri("http://steamcommunity.com");
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.CacheControl, HttpHeaderValues.NoCache);
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.Pragma, HttpHeaderValues.NoCache);
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.Accept, HttpHeaderValues.AcceptHtmlImagesAndXml);
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.AcceptEncoding, HttpHeaderValues.GzipDeflateOrSdch);
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.AcceptLanguage, HttpHeaderValues.AcceptLanguageEnglish);
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.UserAgent, HttpHeaderValues.ChromeUserAgent);
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.Referer, "http://steamcommunity.com/profiles/" + steamCommunityId + "/home");
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.UpgradeInsecureRequest, "1");
                client.DefaultRequestHeaders.Host = new Uri("http://steamcommunity.com").Host;
                response = client.GetAsync(path).Result;
            }
            return response;
        }
    }
}
