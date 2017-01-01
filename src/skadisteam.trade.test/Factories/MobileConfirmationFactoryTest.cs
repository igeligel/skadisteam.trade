using System;
using AngleSharp.Parser.Html;
using skadisteam.trade.Factories;
using skadisteam.trade.Models;
using skadisteam.trade.Models.Confirmation;
using Xunit;

namespace skadisteam.trade.test.Factories
{
    public class MobileConfirmationFactoryTest
    {
        private const string DefaultDeviceId =
            "android:416f74b-3e59-4868-9794-9cf5c22001f2";

        private const string DefaultIdentitySecret =
            "hnMWapNvIXr+8L8/0ulxgdO78IE=";

        private const ConfirmationTag DefaultConfirmationTag =
            ConfirmationTag.Confirm;

        private const long GabenSteamId = 76561197960287930;

        private readonly HtmlParser _parser = new HtmlParser();

        [Fact]
        public void GenerateConfirmationUrlDefaultDeviceIdCheck()
        {
            var result =
                MobileConfirmationFactory.GenerateConfirmationUrl(
                    DefaultDeviceId, DefaultIdentitySecret, GabenSteamId);
            Assert.True(
                result.Contains("p=android:416f74b-3e59-4868-9794-9cf5c22001f2"));
        }

        [Fact]
        public void GenerateConfirmationUrlCustomDeviceIdCheck()
        {
            var result =
                MobileConfirmationFactory.GenerateConfirmationUrl(
                    "p=android:5fa32fee-8638-4e0e-8fc3-7a42de049998",
                    DefaultIdentitySecret, GabenSteamId);
            Assert.True(
                result.Contains("p=android:5fa32fee-8638-4e0e-8fc3-7a42de049998"));
        }

        [Fact]
        public void GenerateConfirmationUrlDefaultSteamIdCheck()
        {
            var result =
                MobileConfirmationFactory.GenerateConfirmationUrl(
                    DefaultDeviceId, DefaultIdentitySecret, GabenSteamId);
            Assert.True(
                result.Contains("a=76561197960287930"));
        }

        [Fact]
        public void GenerateConfirmationUrlSecondSteamIdCheck()
        {
            var result =
                MobileConfirmationFactory.GenerateConfirmationUrl(
                    DefaultDeviceId, DefaultIdentitySecret, 76561197993404877);
            Assert.True(
                result.Contains("a=76561197993404877"));
        }

        [Fact]
        public void GenerateConfirmationQueryParamsDefaultTagCheck()
        {

            var result =
                MobileConfirmationFactory.GenerateConfirmationQueryParams(
                    DefaultConfirmationTag, DefaultDeviceId,
                    DefaultIdentitySecret, GabenSteamId);
            Assert.True(result.Contains("tag=conf"));
        }

        [Fact]
        public void GenerateConfirmationQueryParamsAllowTagCheck()
        {

            var result =
                MobileConfirmationFactory.GenerateConfirmationQueryParams(
                    ConfirmationTag.Allow, DefaultDeviceId,
                    DefaultIdentitySecret, GabenSteamId);
            Assert.True(result.Contains("tag=allow"));
        }

        [Fact]
        public void GenerateConfirmationQueryParamsDefaultSteamIdCheck()
        {

            var result =
                MobileConfirmationFactory.GenerateConfirmationQueryParams(
                    DefaultConfirmationTag, DefaultDeviceId,
                    DefaultIdentitySecret, GabenSteamId);
            Assert.True(result.Contains("a=76561197960287930"));
        }

        [Fact]
        public void GenerateConfirmationQueryParamsCustomSteamIdCheck()
        {

            var result =
                MobileConfirmationFactory.GenerateConfirmationQueryParams(
                    DefaultConfirmationTag, DefaultDeviceId,
                    DefaultIdentitySecret, 76561197993404877);
            Assert.True(result.Contains("a=76561197993404877"));
        }

        [Fact]
        public void GenerateConfirmationQueryParamsDefaultDeviceIdCheck()
        {

            var result =
                MobileConfirmationFactory.GenerateConfirmationQueryParams(
                    DefaultConfirmationTag, DefaultDeviceId,
                    DefaultIdentitySecret, GabenSteamId);
            Assert.True(
                result.Contains("p=android:416f74b-3e59-4868-9794-9cf5c22001f2"));
        }

        [Fact]
        public void GenerateConfirmationQueryParamsCustomDeviceIdCheck()
        {

            var result =
                MobileConfirmationFactory.GenerateConfirmationQueryParams(
                    DefaultConfirmationTag,
                    "android:5fa32fee-8638-4e0e-8fc3-7a42de049998",
                    DefaultIdentitySecret, GabenSteamId);
            Assert.True(
                result.Contains("p=android:5fa32fee-8638-4e0e-8fc3-7a42de049998"));
        }

        [Fact]
        public void CreateTradeConfirmationTypeCheck()
        {
            var document = _parser.Parse(CreateDefaultTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var mobileConfirmation = MobileConfirmationFactory.Create(element);
            Assert.Equal(typeof(TradeConfirmation), mobileConfirmation.GetType());
        }

        [Fact]
        public void CreateTradeConfirmationIdCheck()
        {
            var document = _parser.Parse(CreateDefaultTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var mobileConfirmation = MobileConfirmationFactory.Create(element);
            Assert.Equal(1630806521, mobileConfirmation.Id);
        }

        [Fact]
        public void CreateTradeConfirmationAlternativeIdCheck()
        {
            var document = _parser.Parse(CreateAlternativeTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var mobileConfirmation = MobileConfirmationFactory.Create(element);
            Assert.Equal(1630806610, mobileConfirmation.Id);
        }

        [Fact]
        public void CreateTradeConfirmationKeyCheck()
        {
            var document = _parser.Parse(CreateDefaultTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var mobileConfirmation = MobileConfirmationFactory.Create(element);
            Assert.Equal(17602044977073925579, mobileConfirmation.Key);
        }

        [Fact]
        public void CreateTradeConfirmationAlternativeKeyCheck()
        {
            var document = _parser.Parse(CreateAlternativeTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var mobileConfirmation = MobileConfirmationFactory.Create(element);
            Assert.Equal(17602044977073925610, mobileConfirmation.Key);
        }

        [Fact]
        public void CreateTradeConfirmationCreatorCheck()
        {
            var document = _parser.Parse(CreateDefaultTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var mobileConfirmation = MobileConfirmationFactory.Create(element);
            Assert.Equal(1747401421, mobileConfirmation.Creator);
        }

        [Fact]
        public void CreateTradeConfirmationAlternativeCreatorCheck()
        {
            var document = _parser.Parse(CreateAlternativeTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var mobileConfirmation = MobileConfirmationFactory.Create(element);
            Assert.Equal(1747401111, mobileConfirmation.Creator);
        }

        [Fact]
        public void CreateTradeConfirmationTimeCheck()
        {
            var document = _parser.Parse(CreateDefaultTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var mobileConfirmation = MobileConfirmationFactory.Create(element);
            var result = DateTime.UtcNow.AddMinutes(-1) <=
                         mobileConfirmation.Time &&
                         DateTime.UtcNow.AddMinutes(1) >=
                         mobileConfirmation.Time;
            Assert.True(result);
        }

        [Fact]
        public void CreateTradeConfirmationAlternativeTimeCheck()
        {
            var document = _parser.Parse(CreateAlternativeTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var mobileConfirmation = MobileConfirmationFactory.Create(element);
            var result = DateTime.UtcNow.AddHours(-3).AddMinutes(-1) <=
                         mobileConfirmation.Time &&
                         DateTime.UtcNow.AddHours(-3).AddMinutes(1) >=
                         mobileConfirmation.Time;
            Assert.True(result);
        }

        [Fact]
        public void CreateTradeConfirmationDefaultItemsCountCheck()
        {
            var document = _parser.Parse(CreateDefaultTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var tradeConfirmation =
                (TradeConfirmation)MobileConfirmationFactory.Create(element);
            Assert.Equal(2, tradeConfirmation.Items.Count);
        }

        [Fact]
        public void CreateTradeConfirmationAlternativeItemsCountCheck()
        {
            var document = _parser.Parse(CreateAlternativeTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var tradeConfirmation =
                (TradeConfirmation)MobileConfirmationFactory.Create(element);
            Assert.Equal(3, tradeConfirmation.Items.Count);
        }

        [Fact]
        public void CreateTradeConfirmationDefaultItemsCheck()
        {
            var document = _parser.Parse(CreateDefaultTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var tradeConfirmation =
                (TradeConfirmation)MobileConfirmationFactory.Create(element);
            var items = tradeConfirmation.Items;
            Assert.True(items.Contains("AK-47 | Redline") &&
                        items.Contains("AWP | Asiimov"));
        }

        [Fact]
        public void CreateTradeConfirmationAlternativeItemsCheck()
        {
            var document = _parser.Parse(CreateAlternativeTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var tradeConfirmation =
                (TradeConfirmation)MobileConfirmationFactory.Create(element);
            var items = tradeConfirmation.Items;
            Assert.True(items.Contains("AK-47 | Vulcan") &&
                        items.Contains("AK-47 | Blue Laminate") &&
                        items.Contains("M4A1-S | Cyrex"));
        }

        [Fact]
        public void CreateTradeConfirmationDefaultPartnerNameCheck()
        {
            var document = _parser.Parse(CreateDefaultTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var tradeConfirmation =
                (TradeConfirmation)MobileConfirmationFactory.Create(element);
            Assert.Equal("Gaben", tradeConfirmation.PartnerName);
        }

        [Fact]
        public void CreateTradeConfirmationAlternativePartnerNameCheck()
        {
            var document = _parser.Parse(CreateAlternativeTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var tradeConfirmation =
                (TradeConfirmation)MobileConfirmationFactory.Create(element);
            Assert.Equal("Username #2", tradeConfirmation.PartnerName);
        }

        [Fact]
        public void CreateTradeConfirmationDefaultPartnerImageCheck()
        {
            var document = _parser.Parse(CreateDefaultTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var tradeConfirmation =
                (TradeConfirmation)MobileConfirmationFactory.Create(element);
            Assert.Equal(
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/c5/c5d56249ee5d28a07db4ac9f7f60af961fab5426.jpg",
                tradeConfirmation.PartnerImage);
        }

        [Fact]
        public void CreateTradeConfirmationAlternativePartnerImageCheck()
        {
            var document = _parser.Parse(CreateAlternativeTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var tradeConfirmation =
                (TradeConfirmation)MobileConfirmationFactory.Create(element);
            Assert.Equal(
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/ad/ad691b571a043b1f997ba2b54f7c4da5d0ada2a9.jpg",
                tradeConfirmation.PartnerImage);
        }

        [Fact]
        public void CreateTradeConfirmationOfflineStatusCheck()
        {
            var document = _parser.Parse(CreateDefaultTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var tradeConfirmation =
                (TradeConfirmation)MobileConfirmationFactory.Create(element);
            Assert.Equal(OnlineStatus.Offline,
                tradeConfirmation.PartnerOnlineStatus);
        }

        [Fact]
        public void CreateTradeConfirmationOnlineStatusCheck()
        {
            var document = _parser.Parse(CreateAlternativeTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var tradeConfirmation =
                (TradeConfirmation)MobileConfirmationFactory.Create(element);
            Assert.Equal(OnlineStatus.Online,
                tradeConfirmation.PartnerOnlineStatus);
        }

        [Fact]
        public void CreateTradeConfirmationIngameStatusCheck()
        {
            var document =
                _parser.Parse(CreateSecondAlternativeTradeConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var tradeConfirmation =
                (TradeConfirmation)MobileConfirmationFactory.Create(element);
            Assert.Equal(OnlineStatus.InGame,
                tradeConfirmation.PartnerOnlineStatus);
        }

        private static string CreateDefaultTradeConfirmation()
        {
            const long id = 1630806521;
            const ulong key = 17602044977073925579;
            const int creator = 1747401421;
            const string partnerStatus = "offline";
            const string avatar =
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/c5/c5d56249ee5d28a07db4ac9f7f60af961fab5426.jpg";
            const string tradePartner = "Gaben";
            const string itemsInTrade = "AK-47 | Redline, AWP | Asiimov";
            const string time = "Just now";
            return CreateTradeConfirmation(id, key, creator,
                partnerStatus, avatar, tradePartner, itemsInTrade, time);
        }

        private static string CreateAlternativeTradeConfirmation()
        {
            const long id = 1630806610;
            const ulong key = 17602044977073925610;
            const int dataType = 2;
            const int creator = 1747401111;
            const string partnerStatus = "online";
            const string avatar =
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/ad/ad691b571a043b1f997ba2b54f7c4da5d0ada2a9.jpg";
            const string tradePartner = "Username #2";
            const string itemsInTrade =
                "AK-47 | Vulcan, AK-47 | Blue Laminate, M4A1-S | Cyrex";
            const string time = "3 hours ago";
            return CreateTradeConfirmation(id, key, creator, partnerStatus,
                avatar, tradePartner, itemsInTrade, time);
        }

        private static string CreateSecondAlternativeTradeConfirmation()
        {
            const long id = 1630806610;
            const ulong key = 17602044977073925610;
            const int dataType = 2;
            const int creator = 1747401111;
            const string partnerStatus = "in-game";
            const string avatar =
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/ad/ad691b571a043b1f997ba2b54f7c4da5d0ada2a9.jpg";
            const string tradePartner = "Username #2";
            const string itemsInTrade =
                "AK-47 | Vulcan, AK-47 | Blue Laminate, M4A1-S | Cyrex";
            const string time = "3 hours ago";
            return CreateTradeConfirmation(id, key, creator, partnerStatus,
                avatar, tradePartner, itemsInTrade, time);
        }

        private static string CreateTradeConfirmation(long id, ulong key,
            int creator, string partnerStatus, string avatar,
            string tradePartner, string itemsInTrade, string time)
        {
            var mediumAvatar = avatar.Replace(".jpg", "_medium.jpg");
            return
                $"<div><div class=\"mobileconf_list_entry\" id=\"conf{id}\" data-confid=\"{id}\" data-key=\"{key}\" data-type=\"2\" data-creator=\"{creator}\" data-cancel=\"Cancel\" data-accept=\"Accept\" ><div class=\"mobileconf_list_entry_content\"><div class=\"mobileconf_list_entry_icon\"><div class=\"playerAvatar {partnerStatus}\"><img src=\"{avatar}\" srcset=\"{avatar} 1x, {mediumAvatar} 2x\"></div></div><div class=\"mobileconf_list_entry_description\"><div>Trade with {tradePartner}</div><div>{itemsInTrade}</div><div>{time}</div></div></div><div class=\"mobileconf_list_entry_sep\"></div></div></div>";
        }

        [Fact]
        public void CreateMarketListingConfirmationTypeCheck()
        {
            var document =
                _parser.Parse(CreateDefaultMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                MobileConfirmationFactory.Create(element);
            Assert.Equal(typeof(MarketListingConfirmation),
                marketListingConfirmation.GetType());
        }

        [Fact]
        public void CreateMarketListingConfirmationIdCheck()
        {
            var document =
                _parser.Parse(CreateDefaultMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal(1630806410, marketListingConfirmation.Id);
        }

        [Fact]
        public void CreateMarketListingConfirmationAlternativeIdCheck()
        {
            var document =
                _parser.Parse(CreateAlternativeMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal(1630806754, marketListingConfirmation.Id);
        }

        [Fact]
        public void CreateMarketListingConfirmationKeyCheck()
        {
            var document =
                _parser.Parse(CreateDefaultMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal(17602044977073925000, marketListingConfirmation.Key);
        }

        [Fact]
        public void CreateMarketListingConfirmationAlternativeKeyCheck()
        {
            var document =
                _parser.Parse(CreateAlternativeMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal(17602044977073925200, marketListingConfirmation.Key);
        }

        [Fact]
        public void CreateMarketListingConfirmationCreatorCheck()
        {
            var document =
                _parser.Parse(CreateDefaultMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal(1747401000, marketListingConfirmation.Creator);
        }

        [Fact]
        public void CreateMarketListingConfirmationAlternativeCreatorCheck()
        {
            var document =
                _parser.Parse(CreateAlternativeMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal(1747401005, marketListingConfirmation.Creator);
        }

        [Fact]
        public void CreateMarketListingConfirmationTimeCheck()
        {
            var document =
                _parser.Parse(CreateDefaultMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            var result = DateTime.UtcNow.AddMinutes(-1) <=
                         marketListingConfirmation.Time &&
                         DateTime.UtcNow.AddMinutes(1) >=
                         marketListingConfirmation.Time;
            Assert.True(result);
        }

        [Fact]
        public void CreateMarketListingConfirmationAlternativeTimeCheck()
        {
            var document =
                _parser.Parse(CreateAlternativeMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            var result = DateTime.UtcNow.AddHours(-3).AddMinutes(-1) <=
                         marketListingConfirmation.Time &&
                         DateTime.UtcNow.AddHours(-3).AddMinutes(1) >=
                         marketListingConfirmation.Time;
            Assert.True(result);
        }

        [Fact]
        public void CreateMarketListingConfirmationItemCheck()
        {
            var document =
                _parser.Parse(CreateDefaultMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal("Best Use Of A Farm Animal",
                marketListingConfirmation.Item);
        }

        [Fact]
        public void CreateMarketListingConfirmationAlternativeItemCheck()
        {
            var document =
                _parser.Parse(CreateAlternativeMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal("Intergalactic Bubbles Booster Pack",
                marketListingConfirmation.Item);
        }

        [Fact]
        public void CreateMarketListingConfirmationCurrencyCheck()
        {
            var document =
                _parser.Parse(CreateDefaultMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal("�", marketListingConfirmation.Currency);
        }

        [Fact]
        public void CreateMarketListingConfirmationAlternativeCurrencyCheck()
        {
            var document =
                _parser.Parse(CreateAlternativeMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal("$", marketListingConfirmation.Currency);
        }

        [Fact]
        public void CreateMarketListingConfirmationPriceCheck()
        {
            var document =
                _parser.Parse(CreateDefaultMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal(new decimal(0.08), marketListingConfirmation.SellPrice);
        }

        [Fact]
        public void CreateMarketListingConfirmationAlternativePriceCheck()
        {
            var document =
                _parser.Parse(CreateAlternativeMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal(new decimal(0.12), marketListingConfirmation.SellPrice);
        }

        [Fact]
        public void CreateMarketListingConfirmationTaxPriceCheck()
        {
            var document =
                _parser.Parse(CreateDefaultMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal(new decimal(0.06),
                marketListingConfirmation.SellPriceAfterTaxes);
        }

        [Fact]
        public void CreateMarketListingConfirmationAlternativeTaxPriceCheck()
        {
            var document =
                _parser.Parse(CreateAlternativeMarketListingConfirmation());
            var element = document.QuerySelector(".mobileconf_list_entry");
            var marketListingConfirmation =
                (MarketListingConfirmation)
                MobileConfirmationFactory.Create(element);
            Assert.Equal(new decimal(0.10),
                marketListingConfirmation.SellPriceAfterTaxes);
        }

        private static string CreateDefaultMarketListingConfirmation()
        {
            const long id = 1630806410;
            const ulong key = 17602044977073925000;
            const int creator = 1747401000;
            const string imageUrl =
                "https://steamcommunity-a.akamaihd.net/economy/image/IzMF03bi9WpSBq-S-ekoE33L-iLqGFHVaU25ZzQNQcXdA3g5gMEPvUZZEaiHLrVJRsl8vGuCUY7Cjc9ehDNVzDMDfHSojyQrcex4NM6b4gDotam4D3G8RzGPDmr_HUxlVsIAbDWIoiX8uLzDFzXIQul5Qg1QfPZR-zJPNZ2IN0M71Y5f-DC7kRUsGkMqcJUedFm-zndKf60rz2cQKtkGzGaff8mI0gpvbRM7Xb-xVu3GPNPzwn0gWUhnHfYYNYOR7HC--JbwbqPUfP5ibfE0_YuX2VFUWZxNPwKOdS8/32fx32f";
            const string itemName = "Best Use Of A Farm Animal";
            const string price = "0,08";
            const string priceAfterTax = "0,06";
            const string currency = "�";
            const string time = "Just now";
            return CreateMarketListingConfirmation(id, key, creator, imageUrl,
                itemName, price, priceAfterTax, currency, time);
        }

        private static string CreateAlternativeMarketListingConfirmation()
        {
            const long id = 1630806754;
            const ulong key = 17602044977073925200;
            const int creator = 1747401005;
            const string imageUrl =
                "https://steamcommunity-a.akamaihd.net/economy/image/IzMF03bi9WpSBq-S-ekoE33L-iLqGFHVaU25ZzQNQcXdBnY7ltYLvVIHHqLGevUZFZVx5mnYWInWiYBMnGVPwXRTdy75z3NoTTBSwlg/32fx32f";
            const string itemName = "Intergalactic Bubbles Booster Pack";
            const string price = "0,12";
            const string priceAfterTax = "0,10";
            const string currency = "$";
            const string time = "3 hours ago";
            return CreateMarketListingConfirmation(id, key, creator, imageUrl,
                itemName, price, priceAfterTax, currency, time);
        }

        private static string CreateMarketListingConfirmation(long id, ulong key,
            int creator, string imageUrl, string itemName, string price,
            string priceAfterTax, string currency, string time)
        {
            return
                $"<div><div class=\"mobileconf_list_entry\" id=\"conf{id}\" data-confid=\"{id}\" data-key=\"{key}\" data-type=\"3\" data-creator=\"{creator}\" data-cancel=\"Cancel\" data-accept=\"Create Listing\" > <div class=\"mobileconf_list_entry_content\"> <div class=\"mobileconf_list_entry_icon\"> <div style=\"border: 1px solid transparent;\"><img src=\"{imageUrl}\" srcset=\"{imageUrl} 1x, {imageUrl}dpx2x 2x\"></div></div><div class=\"mobileconf_list_checkbox\"> <input id=\"multiconf_{id}\" data-confid=\"{id}\" data-key=\"{key}\" value=\"1\" type=\"checkbox\"> </div><div class=\"mobileconf_list_entry_description\"> <div>Sell - {itemName}</div><div> {price}{currency} ({priceAfterTax}{currency})</div><div>{time}</div></div></div><div class=\"mobileconf_list_entry_sep\"></div></div></div>";
        }
    }
}
