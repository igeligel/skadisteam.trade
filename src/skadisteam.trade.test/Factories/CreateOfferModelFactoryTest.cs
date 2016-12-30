using System.Collections.Generic;
using System.Linq;
using skadisteam.trade.Factories;
using skadisteam.trade.Models.TradeOffer;
using Xunit;

namespace skadisteam.trade.test.Factories
{
    public class CreateOfferModelFactoryTest
    {
        [Fact]
        public void VersionCheck()
        {
            var result = CreateOfferModelFactory.Create(
                CreateDefaultMyAssets(), CreateDefaultPartnerAssets());
            Assert.Equal(4, result.Version);
        }

        [Fact]
        public void NewVersionCheck()
        {
            var result = CreateOfferModelFactory.Create(
                CreateDefaultMyAssets(), CreateDefaultPartnerAssets());
            Assert.True(result.NewVersion);
        }

        [Fact]
        public void MeReadyCheck()
        {
            var result = CreateOfferModelFactory.Create(
                CreateDefaultMyAssets(), CreateDefaultPartnerAssets());
            Assert.Equal(false, result.Me.Ready);
        }

        [Fact]
        public void PartnerReadyCheck()
        {
            var result = CreateOfferModelFactory.Create(
                CreateDefaultMyAssets(), CreateDefaultPartnerAssets());
            Assert.Equal(false, result.Them.Ready);
        }

        [Fact]
        public void AmountMyAssetsCheck()
        {
            var result = CreateOfferModelFactory.Create(
                CreateDefaultMyAssets(), CreateDefaultPartnerAssets());
            Assert.Equal(2, result.Me.Assets.Count);
        }

        [Fact]
        public void AmountPartnerAssetsCheck()
        {
            var result = CreateOfferModelFactory.Create(
                CreateDefaultMyAssets(), CreateDefaultPartnerAssets());
            Assert.Equal(3, result.Them.Assets.Count);
        }

        [Fact]
        public void ContainsMyAssetsCheck()
        {
            var result = CreateOfferModelFactory.Create(
                CreateDefaultMyAssets(), CreateDefaultPartnerAssets());
            var myAssetIds = result.Me.Assets.Select(e=> e.AssetId).ToList();
            Assert.True(myAssetIds.Contains("6866381273") &&
                        myAssetIds.Contains("6866381274"));
        }

        [Fact]
        public void ContainsPartnerAssetsCheck()
        {
            var result = CreateOfferModelFactory.Create(
                CreateDefaultMyAssets(), CreateDefaultPartnerAssets());
            var themAssetIds =
                result.Them.Assets.Select(e => e.AssetId).ToList();
            Assert.True(themAssetIds.Contains("6866381277") &&
                        themAssetIds.Contains("6866381278") &&
                        themAssetIds.Contains("6866381279"));
        }

        private static List<Asset> CreateDefaultMyAssets()
        {
            return new List<Asset>
            {
                CreateCsgoAsset(6866381273),
                CreateCsgoAsset(6866381274)
            };
        }

        private static List<Asset> CreateDefaultPartnerAssets()
        {
            return new List<Asset>
            {
                CreateCsgoAsset(6866381277),
                CreateCsgoAsset(6866381278),
                CreateCsgoAsset(6866381279)
            };
        }

        private static Asset CreateCsgoAsset(long assetId)
        {
            return new Asset
            {
                Amount = "1",
                AppId = "730",
                ContextId = "2",
                AssetId = assetId.ToString()
            };
        }
    }
}
