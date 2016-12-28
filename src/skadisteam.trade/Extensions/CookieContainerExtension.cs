using System;
using System.Net;
using skadisteam.trade.Constants;

namespace skadisteam.trade.Extensions
{
    internal static class CookieContainerExtension
    {
        internal static CookieContainer AddWebTradeEligilityCookie(
            this CookieContainer cookieContainer)
        {
            return
                cookieContainer.AddCookie(Uris.SteamCommunityBase,
                    "webTradeEligibility",
                    "%7B%22allowed%22%3A1%2C%22allowed_at_time%22%3A0%2C%22steamguard_required_days%22%3A15%2C%22sales_this_year%22%3A1182%2C%22max_sales_per_year%22%3A-1%2C%22forms_requested%22%3A0%2C%22new_device_cooldown_days%22%3A7%7D");
        }

        internal static CookieContainer EnableMobileCookieContainer(
            this CookieContainer cookieContainer, long steamCommunityId)
        {
            return cookieContainer.AddDobCookie()
                .AddMobileClientVersion()
                .AddAndroidMobileClient()
                .AddSteamIdCookie(steamCommunityId);
        }

        internal static CookieContainer DisableMobileCookieContainer(
            this CookieContainer cookieContainer)
        {
            return cookieContainer.DeleteMobileCookiesByName("steamid")
                .DeleteMobileCookiesByName("dob")
                .DeleteMobileCookiesByName("mobileClientVersion")
                .DeleteMobileCookiesByName("mobileClient");
        }

        private static CookieContainer AddAndroidMobileClient(
            this CookieContainer cookieContainer)
        {
            return cookieContainer.AddCookie(Uris.SteamCommunityBaseSecured,
                "mobileClient", "android");
        }

        private static CookieContainer AddMobileClientVersion(
            this CookieContainer cookieContainer)
        {
            return cookieContainer.AddCookie(Uris.SteamCommunityBaseSecured,
                "mobileClientVersion", "0 (2.1.3)");
        }

        private static CookieContainer AddDobCookie(
            this CookieContainer cookieContainer)
        {
            return cookieContainer.AddCookie(Uris.SteamCommunityBaseSecured,
                "dob", "");
        }

        private static CookieContainer AddSteamIdCookie(
            this CookieContainer cookieContainer, long steamCommunityId)
        {
            return cookieContainer.AddCookie(Uris.SteamCommunityBaseSecured,
                "steamid", steamCommunityId);
        }

        private static CookieContainer AddCookie(
            this CookieContainer cookieContainer, Uri uri, string name,
            string value)
        {
            cookieContainer.Add(uri, new Cookie(name, value)
            {
                Domain = uri.Host
            });
            return cookieContainer;
        }

        private static CookieContainer AddCookie(
            this CookieContainer cookieContainer, Uri uri, string name,
            long value)
        {
            cookieContainer.Add(uri, new Cookie(name, value.ToString())
            {
                Domain = uri.Host
            });
            return cookieContainer;
        }

        private static CookieContainer DeleteMobileCookiesByName(
            this CookieContainer cookieContainer, string name)
        {
            var cookies =
                cookieContainer.GetCookies(Uris.SteamCommunityBaseSecured);
            foreach (Cookie cookie in cookies)
            {
                if (cookie.Name == name)
                {
                    cookie.Expires = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                }
            }
            return cookieContainer;
        }
    }
}
