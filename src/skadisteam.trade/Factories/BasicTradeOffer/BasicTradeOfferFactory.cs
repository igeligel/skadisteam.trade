using System;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Dom;

namespace skadisteam.trade.Factories.BasicTradeOffer
{
    internal static class BasicTradeOfferFactory
    {
        internal static Models.BasicTradeOffer Create(IElement angleSharpElement)
        {
            var tradeOfferItems =
                angleSharpElement.QuerySelectorAll(".tradeoffer_item_list");
            var basicTradeOffer = new Models.BasicTradeOffer
            {
                Partner =
                    BasicTradeOfferPartnerFactory.Create(angleSharpElement),
                ProfileItems =
                    BasicTradeOfferItemListFactory.CreateProfileItems(
                        tradeOfferItems),
                PartnerItems =
                    BasicTradeOfferItemListFactory.CreatePartnerItems(
                        tradeOfferItems),
                Id = GetTradeOfferId(angleSharpElement),
                Active = TradeOfferActive(angleSharpElement),
                ExpiringDate = GetExpireDate(angleSharpElement)
            };
            return basicTradeOffer;
        }

        private static int GetTradeOfferId(IElement angleSharpElement)
        {
            return int.Parse(angleSharpElement.Id.Replace("tradeofferid_", ""));
        }

        private static bool TradeOfferActive(IParentNode angleSharpElement)
        {
            var activeClassName =
                angleSharpElement.Children.FirstOrDefault(
                    e => e.ClassList.Contains("tradeoffer_items_ctn"))
                    .ClassName;
            return activeClassName.Contains("active");
        }

        private static DateTime GetExpireDate(IParentNode angleSharpElement)
        {
            var test = angleSharpElement.LastElementChild.TextContent;
            var bla = Regex.Split(test, "Offer expires on ")[1];
            bla = bla.Replace("\t", "").Replace("\n", "");
            var expireTime = DateTime.ParseExact(bla, "dd MMM",
                System.Globalization.CultureInfo.InvariantCulture);
            return expireTime;
        }
    }
}
