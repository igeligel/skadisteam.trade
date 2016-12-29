using System;
using skadisteam.trade.Factories;
using skadisteam.trade.Models;
using skadisteam.trade.Models.Confirmation;
using Xunit;

namespace skadisteam.trade.test.Factories
{
    public class UrlPathFactoryTest
    {
        [Fact]
        public void GetBasicTradeOfferGabenCheck()
        {
            var path = UrlPathFactory.GetBasicTradeOffer(76561197960287930);
            Assert.Equal("/profiles/76561197960287930/tradeoffers/", path);
        }

        [Fact]
        public void GetBasicTradeOfferBenDotComCheck()
        {
            var path = UrlPathFactory.GetBasicTradeOffer(76561197993404877);
            Assert.Equal("/profiles/76561197993404877/tradeoffers/", path);
        }

        [Fact]
        public void FirstTradeOfferCheck()
        {
            var path = UrlPathFactory.TradeOffer(1742913322);
            Assert.Equal("/tradeoffer/1742913322/", path);
        }

        [Fact]
        public void SecondTradeOfferCheck()
        {
            var path = UrlPathFactory.TradeOffer(1742913420);
            Assert.Equal("/tradeoffer/1742913420/", path);
        }

        [Fact]
        public void FirstDeclineOfferCheck()
        {
            var path = UrlPathFactory.DeclineOffer(1742913322);
            Assert.Equal("/tradeoffer/1742913322/decline", path);
        }

        [Fact]
        public void SecondDeclineOfferCheck()
        {
            var path = UrlPathFactory.DeclineOffer(1742913420);
            Assert.Equal("/tradeoffer/1742913420/decline", path);
        }

        [Fact]
        public void FirstAcceptOfferCheck()
        {
            var path = UrlPathFactory.AcceptOffer(1742913322);
            Assert.Equal("/tradeoffer/1742913322/accept", path);
        }

        [Fact]
        public void SecondAcceptOfferCheck()
        {
            var path = UrlPathFactory.AcceptOffer(1742913420);
            Assert.Equal("/tradeoffer/1742913420/accept", path);
        }

        [Fact]
        public void ConfirmationUrlFirstDeviceIdCheck()
        {
            var confirmationUrlParameter = new ConfirmationUrlParameter
            {
                DeviceId = "android:3852f158-ba5f-443f-b528-ae47d16d226e",
                IdentitySecret = "hnMWapNvIXr+8L8/0ulxgdO78IE=",
                SteamCommunityId = 76561197960287930,
                ConfirmationTag = ConfirmationTag.Allow,
                MobileConfirmation = CreateDefaultMobileConfirmation()
            };
            var path = UrlPathFactory.ConfirmationUrl(confirmationUrlParameter);
            Assert.True(
                path.Contains("p=android:3852f158-ba5f-443f-b528-ae47d16d226e"));
        }

        [Fact]
        public void ConfirmationUrlSecondDeviceIdCheck()
        {
            var confirmationUrlParameter = new ConfirmationUrlParameter
            {
                DeviceId = "android:c36355d9-07bc-4b21-b4a2-2273673ca968",
                IdentitySecret = "hnMWapNvIXr+8L8/0ulxgdO78IE=",
                SteamCommunityId = 76561197960287930,
                ConfirmationTag = ConfirmationTag.Allow,
                MobileConfirmation = CreateDefaultMobileConfirmation()
            };
            var path = UrlPathFactory.ConfirmationUrl(confirmationUrlParameter);
            Assert.True(
                path.Contains("p=android:c36355d9-07bc-4b21-b4a2-2273673ca968"));
        }

        [Fact]
        public void ConfirmationUrlSteamIdGabenCheck()
        {
            var confirmationUrlParameter = new ConfirmationUrlParameter
            {
                DeviceId = "android:3852f158-ba5f-443f-b528-ae47d16d226e",
                IdentitySecret = "hnMWapNvIXr+8L8/0ulxgdO78IE=",
                SteamCommunityId = 76561197960287930,
                ConfirmationTag = ConfirmationTag.Allow,
                MobileConfirmation = CreateDefaultMobileConfirmation()
            };
            var path = UrlPathFactory.ConfirmationUrl(confirmationUrlParameter);
            Assert.True(path.Contains("a=76561197960287930"));
        }

        [Fact]
        public void ConfirmationUrlSteamIdBenDotComCheck()
        {
            var confirmationUrlParameter = new ConfirmationUrlParameter
            {
                DeviceId = "android:3852f158-ba5f-443f-b528-ae47d16d226e",
                IdentitySecret = "hnMWapNvIXr+8L8/0ulxgdO78IE=",
                SteamCommunityId = 76561197993404877,
                ConfirmationTag = ConfirmationTag.Allow,
                MobileConfirmation = CreateDefaultMobileConfirmation()
            };
            var path = UrlPathFactory.ConfirmationUrl(confirmationUrlParameter);
            Assert.True(path.Contains("a=76561197993404877"));
        }

        [Fact]
        public void ConfirmationUrlAllowTagCheck()
        {
            var confirmationUrlParameter = new ConfirmationUrlParameter
            {
                DeviceId = "android:3852f158-ba5f-443f-b528-ae47d16d226e",
                IdentitySecret = "hnMWapNvIXr+8L8/0ulxgdO78IE=",
                SteamCommunityId = 76561197993404877,
                ConfirmationTag = ConfirmationTag.Allow,
                MobileConfirmation = CreateDefaultMobileConfirmation()
            };
            var path = UrlPathFactory.ConfirmationUrl(confirmationUrlParameter);
            Assert.True(path.Contains("tag=allow"));
        }

        [Fact]
        public void ConfirmationUrlConfirmTagCheck()
        {
            var confirmationUrlParameter = new ConfirmationUrlParameter
            {
                DeviceId = "android:3852f158-ba5f-443f-b528-ae47d16d226e",
                IdentitySecret = "hnMWapNvIXr+8L8/0ulxgdO78IE=",
                SteamCommunityId = 76561197993404877,
                ConfirmationTag = ConfirmationTag.Confirm,
                MobileConfirmation = CreateDefaultMobileConfirmation()
            };
            var path = UrlPathFactory.ConfirmationUrl(confirmationUrlParameter);
            Assert.True(path.Contains("tag=conf"));
        }
        
        private static MobileConfirmation CreateDefaultMobileConfirmation()
        {
            return new MobileConfirmation
            {
                Id = 1608621571,
                Creator = 1752586421,
                Key = 2676537912014354512,
                Time = DateTime.Parse("Friday, February 27, 2009 12:12:22 PM")
            };
        }
    }
}
