using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using AngleSharp.Dom;
using skadisteam.trade.Interfaces;
using System.Linq;
using skadisteam.trade.Extensions;
using skadisteam.trade.Models.Confirmation;

namespace skadisteam.trade.Factories
{
    public static class MobileConfirmationFactory
    {
        internal static IMobileConfirmation Create(IElement domElement)
        {
            var id = long.Parse(domElement.Attributes.FirstOrDefault(
                    e => e.Name == "data-confid").Value);
            var key =
                    ulong.Parse(domElement.Attributes.FirstOrDefault(
                        e => e.Name == "data-key").Value);
            var creator =
                    int.Parse(domElement.Attributes.FirstOrDefault(
                        e => e.Name == "data-creator").Value);
            var type =
                (ConfirmationType)
                int.Parse(
                    domElement.Attributes.FirstOrDefault(
                        e => e.Name == "data-type").Value);
            var time = domElement.QuerySelector(
                    ".mobileconf_list_entry_description").Children[2].TextContent;
            var mobileConfirmation = new MobileConfirmation
            {
                Id = id,
                Creator = creator,
                Key = key,
                // TODO: Time!
                Time = DateTime.UtcNow
            };
            
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (type)
            {
                case ConfirmationType.ConfirmTrade:
                    var tradeConfirmation =
                        mobileConfirmation.ToTradeConfirmation(domElement);
                    return tradeConfirmation;
                case ConfirmationType.CreateListing:
                    var marketListingConfirmation =
                        new MarketListingConfirmation();
                    return marketListingConfirmation;
            }
            return mobileConfirmation;
        }

        internal static string GenerateConfirmationUrl(string devideId, string identitySecret, long steamCommunityId)
        {
            const string endpoint = "/mobileconf/conf?";
            const string tag = "conf";
            var queryString = GenerateConfirmationQueryParams(tag, devideId, identitySecret, steamCommunityId);
            return endpoint + queryString;
        }

        internal static string GenerateConfirmationQueryParams(string tag, string deviceId, string identitySecret, long steamCommunityId)
        {
            var time = TimeAligner.GetSteamTime();
            return "p=" + deviceId + "&a=" + steamCommunityId + "&k=" +
                   _generateConfirmationHashForTime(identitySecret, time, tag) + "&t=" + time + "&m=android&tag=" + tag;
        }

        private static string _generateConfirmationHashForTime(string identitySecret, long time, string tag)
        {
            var decode = Convert.FromBase64String(identitySecret);
            var n2 = 8;
            if (tag != null)
            {
                if (tag.Length > 32)
                {
                    n2 = 8 + 32;
                }
                else
                {
                    n2 = 8 + tag.Length;
                }
            }
            var array = new byte[n2];
            var n3 = 8;
            while (true)
            {
                var n4 = n3 - 1;
                if (n3 <= 0)
                {
                    break;
                }
                array[n4] = (byte)time;
                time >>= 8;
                n3 = n4;
            }
            if (tag != null)
            {
                Array.Copy(Encoding.UTF8.GetBytes(tag), 0, array, 8, n2 - 8);
            }

            try
            {
                var hmacGenerator = new HMACSHA1 { Key = decode };
                var hashedData = hmacGenerator.ComputeHash(array);
                var encodedData = Convert.ToBase64String(hashedData);
                var hash = WebUtility.UrlEncode(encodedData);
                return hash;
            }
            catch (Exception)
            {
                return null; //Fix soon: catch-all is BAD!
            }
        }
    }
}
