using System;
using System.Net;

namespace skadisteam.trade.Extensions
{
    public static class CookieContainerExtension
    {
        internal static CookieContainer AddWebTradeEligilityCookie(
            this CookieContainer cookieContainer)
        {
            var steamCommunityUri = new Uri("http://steamcommunity.com");
            Cookie steamLanguageCookie = new Cookie("webTradeEligibility", "%7B%22allowed%22%3A1%2C%22allowed_at_time%22%3A0%2C%22steamguard_required_days%22%3A15%2C%22sales_this_year%22%3A1182%2C%22max_sales_per_year%22%3A-1%2C%22forms_requested%22%3A0%2C%22new_device_cooldown_days%22%3A7%7D")
            {
                Domain = steamCommunityUri.Host
            };

            cookieContainer.Add(steamCommunityUri, steamLanguageCookie);
            return cookieContainer;
        }
    }
}
