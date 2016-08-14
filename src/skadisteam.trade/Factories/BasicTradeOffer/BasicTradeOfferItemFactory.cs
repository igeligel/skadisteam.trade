using System.Linq;
using AngleSharp.Dom;
using skadisteam.trade.Models;
using skadisteam.trade.Models.BasicTradeOffer;

namespace skadisteam.trade.Factories.BasicTradeOffer
{
    internal static class BasicTradeOfferItemFactory
    {
        internal static BasicTradeOfferItem Create(IElement angleSharpElement)
        {
            var basicTradeOfferItem = new BasicTradeOfferItem();
            var dataEconomyItem =
                angleSharpElement.Attributes.FirstOrDefault(
                    e => e.Name == "data-economy-item").Value;
            basicTradeOfferItem.DataEconomy =
                DataEconomyFactory.GetEconomy(dataEconomyItem);
            basicTradeOfferItem.ItemPicture =
                angleSharpElement.Children.FirstOrDefault(
                    e => e.TagName == "IMG")
                    .Attributes.FirstOrDefault(e => e.Name == "src")
                    .Value;
            return basicTradeOfferItem;
        }
    }
}
