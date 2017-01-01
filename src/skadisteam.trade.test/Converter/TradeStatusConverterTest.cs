using System.Collections.Generic;
using skadisteam.trade.Converter;
using skadisteam.trade.Models.Json;

namespace skadisteam.trade.test.Converter
{
    public class TradeStatusConverterTest
    {
        public void Check()
        {
            var currentTradeStatus = new CurrentTradeStatus
            {
                NewVersion = true,
                Version = 1
            };

            var myPlayerTradeStatus = new PlayerTradeStatus {Ready = false};
            var myAssets = new List<Asset>
            {
                CreateAsset("730", "2", "1", "8493147330"),
                CreateAsset("730", "2", "1", "8493147636")
            };

            myPlayerTradeStatus.Assets = myAssets;
            currentTradeStatus.Me = myPlayerTradeStatus;
            
            var themPlayerTradeStatus = new PlayerTradeStatus {Ready = false};
            var themAssets = new List<Asset>
            {
                CreateAsset("730", "2", "1", "8493147347"),
                CreateAsset("730", "2", "1", "8493147288"),
                CreateAsset("730", "2", "1", "3772054880"),
                CreateAsset("753", "6", "1", "3771629952"),
                CreateAsset("753", "6", "1", "3771184344")
            };
            themPlayerTradeStatus.Assets = themAssets;

            currentTradeStatus.Them = themPlayerTradeStatus;

            var result = TradeStatusConverter.Convert(currentTradeStatus);
        }

        private static Asset CreateAsset(string appId, string contextId,
            string amount, string assetId)
        {
            var asset = new Asset
            {
                AppId = appId,
                Amount = amount,
                ContextId = contextId,
                AssetId = assetId
            };
            return asset;
        }
    }
}
