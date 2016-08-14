using System.Collections.Generic;
using AngleSharp.Dom;
using skadisteam.trade.Models;
using System.Linq;
using skadisteam.trade.Models.BasicTradeOffer;

namespace skadisteam.trade.Factories.BasicTradeOffer
{
    internal static class BasicTradeOfferItemListFactory
    {
        internal static List<BasicTradeOfferItem> CreateProfileItems(IHtmlCollection<IElement> tradeOfferItems)
        {
            var partnerItems = tradeOfferItems.LastOrDefault();
            var partnerItemInstances =
                partnerItems.QuerySelectorAll(".trade_item");
            return partnerItemInstances.Select(BasicTradeOfferItemFactory.Create).ToList();
        }

        internal static List<BasicTradeOfferItem> CreatePartnerItems(IHtmlCollection<IElement> tradeOfferItems)
        {
            var partnerItems = tradeOfferItems.FirstOrDefault();
            var partnerItemInstances =
                partnerItems.QuerySelectorAll(".trade_item");
            return partnerItemInstances.Select(BasicTradeOfferItemFactory.Create).ToList();
        }
    }
}
