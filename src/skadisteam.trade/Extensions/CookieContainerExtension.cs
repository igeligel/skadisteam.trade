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
            return cookieContainer.AddCookie(Uris.SteamCommunityBase,
                CookieNames.WebTradeEligibility,
                CookieValues.WebTradeEligibility);
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
            return cookieContainer.DeleteMobileCookiesByName(CookieNames.SteamId)
                .DeleteMobileCookiesByName(CookieNames.Dob)
                .DeleteMobileCookiesByName(CookieNames.MobileClientVersion)
                .DeleteMobileCookiesByName(CookieNames.MobileClient);
        }

        private static CookieContainer AddAndroidMobileClient(
            this CookieContainer cookieContainer)
        {
            return cookieContainer.AddCookie(Uris.SteamCommunityBaseSecured,
                CookieNames.MobileClient, CookieValues.MobileClientAndroid);
        }

        private static CookieContainer AddMobileClientVersion(
            this CookieContainer cookieContainer)
        {
            return cookieContainer.AddCookie(Uris.SteamCommunityBaseSecured,
                CookieNames.MobileClientVersion,
                CookieValues.MobileClientVersion);
        }

        private static CookieContainer AddDobCookie(
            this CookieContainer cookieContainer)
        {
            return cookieContainer.AddCookie(Uris.SteamCommunityBaseSecured,
                CookieNames.Dob, string.Empty);
        }

        private static CookieContainer AddSteamIdCookie(
            this CookieContainer cookieContainer, long steamCommunityId)
        {
            return cookieContainer.AddCookie(Uris.SteamCommunityBaseSecured,
                CookieNames.SteamId, steamCommunityId);
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
