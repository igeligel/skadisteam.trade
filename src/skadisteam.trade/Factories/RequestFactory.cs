using System;
using System.Net.Http;
using skadisteam.trade.Constants;
using skadisteam.trade.Extensions;

namespace skadisteam.trade.Factories
{
    internal static class RequestFactory
    {
        internal static HttpResponseMessage GetTradeOffersListResponseMessage(
            HttpClientHandler httpClientHandler, string path, long steamCommunityId)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = Uris.SteamCommunityBase;
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.CacheControl, HttpHeaderValues.NoCache);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Pragma, HttpHeaderValues.NoCache);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Accept,
                    HttpHeaderValues.AcceptHtmlImagesAndXml);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptEncoding,
                    HttpHeaderValues.GzipDeflateOrSdch);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptLanguage,
                    HttpHeaderValues.AcceptLanguageEnglish);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UserAgent, HttpHeaderValues.ChromeUserAgent);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.Referer,
                    Urls.SteamCommunityBase + "/profiles/" + steamCommunityId +
                    "/home");
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UpgradeInsecureRequest, "1");
                client.DefaultRequestHeaders.Host = Uris.SteamCommunityBase.Host;
                response = client.GetAsync(path).Result;
            }
            return response;
        }

        internal static HttpResponseMessage GetTradeOfferResponseMessage(
            HttpClientHandler httpClientHandler, string path, long steamCommunityId)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = new Uri("https://steamcommunity.com");
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.AcceptEncoding, "gzip, deflate, sdch, br");
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.AcceptEncoding, "de-DE,de;q=0.8,en-US;q=0.6,en;q=0.4,it;q=0.2");
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.UserAgent, "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36");
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.Referer, "http://steamcommunity.com/profiles/" + steamCommunityId + "/tradeoffers/");
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.UpgradeInsecureRequest, "1");
                client.DefaultRequestHeaders.Host =
                    new Uri("https://steamcommunity.com").Host;
                response = client.GetAsync(path).Result;
            }
            return response;
        }

        internal static HttpResponseMessage GetConfirmationData(HttpClientHandler httpClientHandler, string path, long steamCommunityId)
        {
            httpClientHandler.CookieContainer.EnableMobileCookieContainer(
                steamCommunityId);
            HttpResponseMessage httpResponseMessage;
            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = new Uri("https://steamcommunity.com");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.AcceptEncoding, HttpHeaderValues.GzipDeflateOrSdch);
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.AcceptLanguage, "de-DE,en-US;q=0.8");
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    "User-Agent", "Mozilla/5.0 (Linux; Android 5.1.1; Nexus 4 Build/LMY48T; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/46.0.2490.76 Mobile Safari/537.36");
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-Requested-With", "com.valvesoftware.android.steam.community");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                client.DefaultRequestHeaders.Host = "steamcommunity.com";
                httpResponseMessage = client.GetAsync(path).Result;
            }
            httpClientHandler.CookieContainer.DisableMobileCookieContainer();
            return httpResponseMessage;
        }

        internal static HttpResponseMessage ApproveConfirmations(HttpClientHandler httpClientHandler, string path, string referer, long steamCommunityId)
        {
            httpClientHandler.CookieContainer.EnableMobileCookieContainer(
                steamCommunityId);
            HttpResponseMessage response;
            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = new Uri("https://steamcommunity.com");
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.Accept, "*/*");
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.AcceptEncoding, HttpHeaderValues.GzipDeflateOrSdch);
                client.DefaultRequestHeaders.TryAddWithoutValidation(HttpHeaderKeys.AcceptLanguage, "de-DE,en-US;q=0.8");
                client.DefaultRequestHeaders.TryAddWithoutValidation(
                    HttpHeaderKeys.UserAgent,
                    "Mozilla/5.0 (Linux; Android 5.1.1; Nexus 4 Build/LMY48T; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/46.0.2490.76 Mobile Safari/537.36");
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Referer",
                    referer);
                client.DefaultRequestHeaders.Host = "steamcommunity.com";
                response = client.GetAsync(path).Result;
            }
            httpClientHandler.CookieContainer.DisableMobileCookieContainer();
            return response;
        }
    }
}
