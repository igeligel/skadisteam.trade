using System.Collections.Generic;
using skadisteam.trade.Models.Json;
using skadisteam.trade.Models.TradeOffer;
using Asset = skadisteam.trade.Models.TradeOffer.Asset;
using PlayerTradeStatus = skadisteam.trade.Models.TradeOffer.PlayerTradeStatus;

namespace skadisteam.trade.Converter
{
    internal static class TradeStatusConverter
    {
        internal static TradeStatus Convert(CurrentTradeStatus currentTradeStatus)
        {
            var tradeStatus = new TradeStatus
            {
                NewVersion = currentTradeStatus.NewVersion,
                Version = currentTradeStatus.Version,
                Me = new PlayerTradeStatus(),
                Them = new PlayerTradeStatus()
            };

            tradeStatus.Me.Ready = currentTradeStatus.Me.Ready;
            tradeStatus.Me.Assets = new List<Asset>();
            foreach (var asset in currentTradeStatus.Me.Assets)
            {
                tradeStatus.Me.Assets.Add(new Asset
                {
                    AssetId = asset.AssetId,
                    Amount = asset.Amount,
                    AppId = asset.AppId,
                    ContextId = asset.ContextId
                });
            }

            tradeStatus.Them.Ready = currentTradeStatus.Them.Ready;
            tradeStatus.Them.Assets = new List<Asset>();
            foreach (var asset in currentTradeStatus.Them.Assets)
            {
                tradeStatus.Them.Assets.Add(new Asset
                {
                    AssetId = asset.AssetId,
                    Amount = asset.Amount,
                    AppId = asset.AppId,
                    ContextId = asset.ContextId
                });
            }
            return tradeStatus;
        }
    }
}
