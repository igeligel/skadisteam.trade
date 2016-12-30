using skadisteam.trade.Factories;
using skadisteam.trade.Models.DataEconomy;
using Xunit;

namespace skadisteam.trade.test.Factories
{
    public class DataEconomyFactoryTest
    {
        private const string PrivateInventoryText =
            "classinfo/730/1835681706/143865972";

        private const string PublicInventoryText =
            "730/2/8559820174/76561198245341096";

        [Fact]
        public void PrivateDataEconomyTypeCheck()
        {
            var result = DataEconomyFactory.GetEconomy(PrivateInventoryText);
            Assert.Equal(typeof(PrivateDataEconomy), result.GetType());
        }

        [Fact]
        public void PublicDataEconomyTypeCheck()
        {
            var result = DataEconomyFactory.GetEconomy(PublicInventoryText);
            Assert.Equal(typeof(PublicDataEconomy), result.GetType());
        }

        [Fact]
        public void PrivateDataEconomyAppIdCsgoCheck()
        {
            var inputText = CreatePrivateInventoryText(730, 1835681706,
                143865972);
            var result =
                (PrivateDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(730, result.AppId);
        }

        [Fact]
        public void PrivateDataEconomyAppIdDotaCheck()
        {
            var inputText = CreatePrivateInventoryText(570, 1835681706,
                143865972);
            var result =
                (PrivateDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(570, result.AppId);
        }

        [Fact]
        public void PrivateDataEconomyFirstClassIdCheck()
        {
            var inputText = CreatePrivateInventoryText(730, 1835681706,
                143865972);
            var result =
                (PrivateDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(1835681706, result.ClassId);
        }

        [Fact]
        public void PrivateDataEconomySecondClassIdCheck()
        {
            var inputText = CreatePrivateInventoryText(730, 1835681707,
                143865972);
            var result =
                (PrivateDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(1835681707, result.ClassId);
        }

        [Fact]
        public void PrivateDataEconomyFirstInstanceIdCheck()
        {
            var inputText = CreatePrivateInventoryText(730, 1835681706,
                143865972);
            var result =
                (PrivateDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(143865972, result.InstanceId);
        }

        [Fact]
        public void PrivateDataEconomySecondInstanceIdCheck()
        {
            var inputText = CreatePrivateInventoryText(730, 1835681706,
                143865973);
            var result =
                (PrivateDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(143865973, result.InstanceId);
        }
        
        [Fact]
        public void PublicDataEconomyAppIdCsgoCheck()
        {
            var inputText = CreatePublicInventoryText(730, 2, 6866243843,
                76561197960287930);
            var result =
                (PublicDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(730, result.AppId);
        }

        [Fact]
        public void PublicDataEconomyAppIdDotaCheck()
        {
            var inputText = CreatePublicInventoryText(570, 2, 6866243843,
                76561197960287930);
            var result =
                (PublicDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(570, result.AppId);
        }

        [Fact]
        public void PublicDataEconomyContextIdDotaCheck()
        {
            var inputText = CreatePublicInventoryText(570, 2, 6866243843,
                76561197960287930);
            var result =
                (PublicDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(2, result.ContextId);
        }

        [Fact]
        public void PublicDataEconomyContextIdSteamCheck()
        {
            var inputText = CreatePublicInventoryText(753, 6, 6866243843,
                76561197960287930);
            var result =
                (PublicDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(6, result.ContextId);
        }

        [Fact]
        public void PublicDataEconomyFirstAssetIdCheck()
        {
            var inputText = CreatePublicInventoryText(753, 6, 6866243843,
                76561197960287930);
            var result =
                (PublicDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(6866243843, result.AssetId);
        }

        [Fact]
        public void PublicDataEconomySecondAssetIdCheck()
        {
            var inputText = CreatePublicInventoryText(753, 6, 6866243844,
                76561197960287930);
            var result =
                (PublicDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(6866243844, result.AssetId);
        }

        [Fact]
        public void PublicDataEconomySteamIdGabenCheck()
        {
            var inputText = CreatePublicInventoryText(753, 6, 6866243844,
                76561197960287930);
            var result =
                (PublicDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(76561197960287930, result.SteamCommunityId);
        }

        [Fact]
        public void PublicDataEconomySteamIdBenDotComCheck()
        {
            var inputText = CreatePublicInventoryText(753, 6, 6866243844,
                76561197993404877);
            var result =
                (PublicDataEconomy)
                DataEconomyFactory.GetEconomy(inputText);
            Assert.Equal(76561197993404877, result.SteamCommunityId);
        }

        private static string CreatePrivateInventoryText(int appId, long classId,
            long instanceId)
        {
            return $"classinfo/{appId}/{classId}/{instanceId}";
        }

        private static string CreatePublicInventoryText(int appId, int contextId,
            long assetId, long steamCommunityId)
        {
            return $"{appId}/{contextId}/{assetId}/{steamCommunityId}";
        }
    }
}
