using System.Collections.Generic;
using System.Linq;
using skadisteam.trade.Converter;
using skadisteam.trade.Models.Json;
using Xunit;
using AppContext = skadisteam.trade.Models.Json.AppContext;

namespace skadisteam.trade.test.Converter
{
    public class AppContextDataConverterTest
    {
        [Fact]
        public void TestMultipleAppDatas()
        {
            var testDictionary = new Dictionary<string, AppData>
            {
                { "730", CreateCsGoAppData() },
                { "753", CreateSteamAppData() }
            };
            var result = AppContextDataConverter.FromJsonModel(testDictionary);
            var csgoBackpack = result.FirstOrDefault(e => e.AppId == 730);
            Assert.NotNull(csgoBackpack);
            Assert.Equal(987, csgoBackpack.AssetCount);
            Assert.Equal(730, csgoBackpack.AppId);
            Assert.Equal(
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/730/69f7ebe2735c366c65c0b33dae00e12dc40edbe4.jpg",
                csgoBackpack.Icon);
            Assert.Equal(
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/730/3ab6e87a04994b900881f694284a75150e640536.png",
                csgoBackpack.InventoryLogo);
            Assert.Equal("http://steamcommunity.com/app/730", csgoBackpack.Link);
            Assert.Equal("FULL", csgoBackpack.TradePermissions);
            TestRgContext(csgoBackpack, 2, 987, "Backpack");

            var steamBackpack = result.FirstOrDefault(e => e.AppId == 753);
            Assert.NotNull(steamBackpack);
            Assert.Equal(444, steamBackpack.AssetCount);
            Assert.Equal(753, steamBackpack.AppId);
            Assert.Equal(
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/753/135dc1ac1cd9763dfc8ad52f4e880d2ac058a36c.jpg",
                steamBackpack.Icon);
            Assert.Equal(
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/753/db8ca9e130b7b37685ab2229bf5a288aefc3f0fa.png",
                steamBackpack.InventoryLogo);
            Assert.Equal("http://steamcommunity.com/app/753", steamBackpack.Link);
            Assert.Equal("FULL", steamBackpack.TradePermissions);
            TestRgContext(steamBackpack, 1, 0, "Gifts");
            TestRgContext(steamBackpack, 3, 44, "Coupons");
            TestRgContext(steamBackpack, 6, 211, "Community");
            TestRgContext(steamBackpack, 7, 346, "Item Rewards");
        }
        

        [Fact]
        public void TestSingleCsgoAppData()
        {
            var testDictionary = new Dictionary<string, AppData>
            {
                {"730", CreateCsGoAppData()}
            };
            var result = AppContextDataConverter.FromJsonModel(testDictionary);
            var csgoBackpack = result.FirstOrDefault(e => e.AppId == 730);
            Assert.NotNull(csgoBackpack);
            Assert.Equal(987, csgoBackpack.AssetCount);
            Assert.Equal(730, csgoBackpack.AppId);
            Assert.Equal(
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/730/69f7ebe2735c366c65c0b33dae00e12dc40edbe4.jpg",
                csgoBackpack.Icon);
            Assert.Equal(
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/730/3ab6e87a04994b900881f694284a75150e640536.png",
                csgoBackpack.InventoryLogo);
            Assert.Equal("http://steamcommunity.com/app/730", csgoBackpack.Link);
            Assert.Equal("FULL", csgoBackpack.TradePermissions);
            TestRgContext(csgoBackpack, 2, 987, "Backpack");
        }

        [Fact]
        public void TestSingleSteamAppData()
        {
            var testDictionary = new Dictionary<string, AppData>
            {
                {"753", CreateSteamAppData()}
            };
            var result = AppContextDataConverter.FromJsonModel(testDictionary);
            var steamBackpack = result.FirstOrDefault(e => e.AppId == 753);
            Assert.NotNull(steamBackpack);
            Assert.Equal(444, steamBackpack.AssetCount);
            Assert.Equal(753, steamBackpack.AppId);
            Assert.Equal(
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/753/135dc1ac1cd9763dfc8ad52f4e880d2ac058a36c.jpg",
                steamBackpack.Icon);
            Assert.Equal(
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/753/db8ca9e130b7b37685ab2229bf5a288aefc3f0fa.png",
                steamBackpack.InventoryLogo);
            Assert.Equal("http://steamcommunity.com/app/753", steamBackpack.Link);
            Assert.Equal("FULL", steamBackpack.TradePermissions);

            TestRgContext(steamBackpack, 1, 0, "Gifts");
            TestRgContext(steamBackpack, 3, 44, "Coupons");
            TestRgContext(steamBackpack, 6, 211, "Community");
            TestRgContext(steamBackpack, 7, 346, "Item Rewards");
        }

        private static void TestRgContext(Models.TradeOffer.AppData appData, int id, int assetCount, string name)
        {
            var appContext = appData.RgContexts.FirstOrDefault(e => e.Id == id);
            TestRgContext(appContext, id, assetCount, name);
        }

        private static void TestRgContext(Models.TradeOffer.AppContext appContext, int id, int assetCount, string name)
        {
            Assert.NotNull(appContext);
            Assert.Equal(id, appContext.Id);
            Assert.Equal(assetCount, appContext.AssetCount);
            Assert.Equal(name, appContext.Name);
        }
        
        private static AppData CreateCsGoAppData()
        {
            var appData = new AppData
            {
                Name = "730",
                AppId = 730,
                AssetCount = 987,
                Icon =
                    "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/730/69f7ebe2735c366c65c0b33dae00e12dc40edbe4.jpg",
                InventoryLogo =
                    "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/730/3ab6e87a04994b900881f694284a75150e640536.png",
                Link = "http://steamcommunity.com/app/730",
                TradePermissions = "FULL",
                RgContexts = CreateCsgoRgContexts()
            };
            return appData;
        }

        private static AppData CreateSteamAppData()
        {
            var appData = new AppData
            {
                Name = "753",
                AppId = 753,
                AssetCount = 444,
                Icon =
                    "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/753/135dc1ac1cd9763dfc8ad52f4e880d2ac058a36c.jpg",
                InventoryLogo =
                    "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/753/db8ca9e130b7b37685ab2229bf5a288aefc3f0fa.png",
                Link = "http://steamcommunity.com/app/753",
                TradePermissions = "FULL",
                RgContexts = CreateSteamRgContexts()
            };
            return appData;
        }

        private static Dictionary<string, AppContext> CreateCsgoRgContexts()
        {
            var rgContexts = new Dictionary<string, AppContext>
            {
                {"2", BackpackAppContext()}
            };
            return rgContexts;
        }

        private static Dictionary<string, AppContext> CreateSteamRgContexts()
        {
            var rgContexts = new Dictionary<string, AppContext>
            {
                { "1", GiftsAppContext() },
                { "3", CouponsAppContext() },
                { "6", CommunityAppContext() },
                { "7", ItemRewardsAppContext() }
            };
            return rgContexts;
        }

        private static AppContext BackpackAppContext()
        {
            return new AppContext
            {
                Name = "Backpack",
                AssetCount = 987,
                Id = 2
            };
        }

        private static AppContext GiftsAppContext()
        {
            return new AppContext
            {
                Name = "Gifts",
                AssetCount = 0,
                Id = 1
            };
        }

        private static AppContext CouponsAppContext()
        {
            return new AppContext
            {
                Name = "Coupons",
                AssetCount = 44,
                Id = 3
            };
        }

        private static AppContext CommunityAppContext()
        {
            return new AppContext
            {
                Name = "Community",
                AssetCount = 211,
                Id = 6
            };
        }

        private static AppContext ItemRewardsAppContext()
        {
            return new AppContext
            {
                Name = "Item Rewards",
                AssetCount = 346,
                Id = 7
            };
        }
    }
}
