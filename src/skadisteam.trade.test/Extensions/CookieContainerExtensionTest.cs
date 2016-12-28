using System;
using System.Net;
using Xunit;
using skadisteam.trade.Extensions;

namespace skadisteam.trade.test.Extensions
{
    public class CookieContainerExtensionTest
    {
        private readonly Uri _steamCommunityBaseUri =
            new Uri("http://steamcommunity.com");

        [Fact]
        public void NullWebTradeEligilityCookieTradCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.AddWebTradeEligilityCookie();
            var resultingCookies =
                cookieContainer.GetCookies(_steamCommunityBaseUri);
            Assert.NotNull(resultingCookies);
        }

        [Fact]
        public void NameWebTradeEligilityCookieCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.AddWebTradeEligilityCookie();
            var resultingCookies =
                cookieContainer.GetCookies(_steamCommunityBaseUri);
            Assert.Equal("webTradeEligibility",
                resultingCookies["webTradeEligibility"].Name);
        }

        [Fact]
        public void ValueWebTradeEligilityCookieCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.AddWebTradeEligilityCookie();
            var resultingCookies =
                cookieContainer.GetCookies(_steamCommunityBaseUri);
            Assert.Equal(
                "%7B%22allowed%22%3A1%2C%22allowed_at_time%22%3A0%2C%22steamguard_required_days%22%3A15%2C%22sales_this_year%22%3A1182%2C%22max_sales_per_year%22%3A-1%2C%22forms_requested%22%3A0%2C%22new_device_cooldown_days%22%3A7%7D",
                resultingCookies["webTradeEligibility"].Value);
        }

        [Fact]
        public void NullEnableMobileCookiesCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.EnableMobileCookieContainer(76561197960287930);
            var resultingCookies =
                cookieContainer.GetCookies(new Uri("https://steamcommunity.com"));
            Assert.NotNull(resultingCookies);
        }

        [Fact]
        public void AmountEnableMobileCookiesCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.EnableMobileCookieContainer(76561197960287930);
            var resultingCookies =
                cookieContainer.GetCookies(new Uri("https://steamcommunity.com"));
            Assert.Equal(4, resultingCookies.Count);
        }

        [Fact]
        public void NameEnableMobileCookiesDobCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.EnableMobileCookieContainer(76561197960287930);
            var resultingCookies =
               cookieContainer.GetCookies(new Uri("https://steamcommunity.com"));
            Assert.Equal("dob", resultingCookies["dob"].Name);
        }

        [Fact]
        public void ValueEnableMobileCookiesDobCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.EnableMobileCookieContainer(76561197960287930);
            var resultingCookies =
               cookieContainer.GetCookies(new Uri("https://steamcommunity.com"));
            Assert.Equal("", resultingCookies["dob"].Value);
        }

        [Fact]
        public void NameEnableMobileCookiesMobileClientCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.EnableMobileCookieContainer(76561197960287930);
            var resultingCookies =
               cookieContainer.GetCookies(new Uri("https://steamcommunity.com"));
            Assert.Equal("mobileClientVersion",
                resultingCookies["mobileClientVersion"].Name);
        }

        [Fact]
        public void ValueEnableMobileCookiesMobileClientCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.EnableMobileCookieContainer(76561197960287930);
            var resultingCookies =
               cookieContainer.GetCookies(new Uri("https://steamcommunity.com"));
            Assert.Equal("0 (2.1.3)",
                resultingCookies["mobileClientVersion"].Value);
        }

        [Fact]
        public void NameEnableMobileCookiesAndroidMobileClientCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.EnableMobileCookieContainer(76561197960287930);
            var resultingCookies =
               cookieContainer.GetCookies(new Uri("https://steamcommunity.com"));
            Assert.Equal("mobileClient",
                resultingCookies["mobileClient"].Name);
        }

        [Fact]
        public void ValueEnableMobileCookiesAndroidMobileClientCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.EnableMobileCookieContainer(76561197960287930);
            var resultingCookies =
               cookieContainer.GetCookies(new Uri("https://steamcommunity.com"));
            Assert.Equal("android",
                resultingCookies["mobileClient"].Value);
        }

        [Fact]
        public void NameEnableMobileCookiesSteamIdCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.EnableMobileCookieContainer(76561197960287930);
            var resultingCookies =
               cookieContainer.GetCookies(new Uri("https://steamcommunity.com"));
            Assert.Equal("steamid",
                resultingCookies["steamid"].Name);
        }

        [Fact]
        public void ValueEnableMobileCookiesSteamIdCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.EnableMobileCookieContainer(76561197960287930);
            var resultingCookies =
               cookieContainer.GetCookies(new Uri("https://steamcommunity.com"));
            Assert.Equal("76561197960287930",
                resultingCookies["steamid"].Value);
        }

        [Fact]
        public void NullDisableMobileCookieCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.EnableMobileCookieContainer(76561197960287930);
            cookieContainer.DisableMobileCookieContainer();
            Assert.NotNull(cookieContainer);
        }

        [Fact]
        public void EmptyDisableMobileCookieCheck()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.EnableMobileCookieContainer(76561197960287930);
            cookieContainer.DisableMobileCookieContainer();
            Assert.Equal(1, cookieContainer.Count);
        }
    }
}
