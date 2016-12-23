using System.Text.RegularExpressions;
using AngleSharp.Dom;
using skadisteam.trade.Models.Confirmation;
using System.Linq;
using skadisteam.trade.Factories;

namespace skadisteam.trade.Extensions
{
    internal static class MobileConfirmationExtension
    {
        internal static TradeConfirmation ToTradeConfirmation(this MobileConfirmation mobileConfirmation, IElement domElement)
        {
            var tradeConfirmation = new TradeConfirmation
            {
                Id = mobileConfirmation.Id,
                Key = mobileConfirmation.Key,
                Creator = mobileConfirmation.Creator,
                Time = mobileConfirmation.Time
            };
            tradeConfirmation.PartnerOnlineStatus =
                OnlineStatusFactory.Create(domElement);
            tradeConfirmation.PartnerImage =
                domElement.QuerySelector(".playerAvatar img")
                    .Attributes.FirstOrDefault(e => e.Name == "src").Value;

            tradeConfirmation.PartnerName = domElement.QuerySelector(
                    ".mobileconf_list_entry_description").Children[0].TextContent
                .Replace("Trade with ", string.Empty);

            var itemsContent = domElement.QuerySelector(
                ".mobileconf_list_entry_description").Children[1].TextContent;

            tradeConfirmation.Items = Regex.Split(itemsContent, ", ").ToList();
            return tradeConfirmation;
        }

        internal static MarketListingConfirmation ToMarketListingConfirmation(this MobileConfirmation mobileConfirmation, IElement domElement)
        {
            return null;
        }
    }
}
