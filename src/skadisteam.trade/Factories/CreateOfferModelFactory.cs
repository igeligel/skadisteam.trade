using System.Collections.Generic;
using skadisteam.trade.Models.Json.CreatingOffers;
using skadisteam.trade.Models.TradeOffer;

namespace skadisteam.trade.Factories
{
    internal static class CreateOfferModelFactory
    {
        internal static CreateOffer Create(List<Asset> myAssets, List<Asset> partnerAssets)
        {
            CreateOffer createOffer = new CreateOffer();
            createOffer.Version = 4;
            createOffer.NewVersion = true;

            createOffer.Me = new TradePartner();
            createOffer.Me.Currency = new List<object>();
            createOffer.Me.Ready = false;

            createOffer.Me.Assets = new List<Models.Json.Asset>();
            foreach (var myAsset in myAssets)
            {
                createOffer.Me.Assets.Add(new Models.Json.Asset
                {
                    Amount = myAsset.Amount,
                    AppId = myAsset.AppId,
                    AssetId = myAsset.AssetId,
                    ContextId = myAsset.ContextId
                });
            }


            createOffer.Them = new TradePartner();
            createOffer.Them.Currency = new List<object>();
            createOffer.Them.Ready = false;

            createOffer.Them.Assets = new List<Models.Json.Asset>();
            foreach (var partnerAsset in partnerAssets)
            {
                createOffer.Them.Assets.Add(new Models.Json.Asset
                {
                    Amount = partnerAsset.Amount,
                    AppId = partnerAsset.AppId,
                    AssetId = partnerAsset.AssetId,
                    ContextId = partnerAsset.ContextId
                });
            }
            return createOffer;
        }
    }
}
