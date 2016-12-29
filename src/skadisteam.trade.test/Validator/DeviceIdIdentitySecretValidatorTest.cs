using System;
using skadisteam.trade.Validator;
using Xunit;

namespace skadisteam.trade.test.Validator
{
    public class DeviceIdIdentitySecretValidatorTest
    {
        private const string DeviceId = "android:3852f158-ba5f-443f-b528-ae47d16d226e";
        private const string IdentitySecret = "hnMWapNvIXr+8L8/0ulxgdO78IE=";

        [Fact]
        public void NoDeviceIdAndIdentitySecretCheck()
        {
            Exception exception =
                Assert.Throws<ArgumentException>(
                    () => DeviceIdIdentitySecretValidator.Validate("", ""));
            Assert.Equal(
                "You have not set the device id and the identity secret which is required for this functionality. To fix this please use a constructor of this class where the device id and the identity secret is required as parameter.",
                exception.Message);
        }

        [Fact]
        public void NoDeviceIdCheck()
        {
            Exception exception =
                Assert.Throws<ArgumentException>(
                    () =>
                        DeviceIdIdentitySecretValidator.Validate("",
                            IdentitySecret));
            Assert.Equal(
                "You have not set your Device Id which is needed for this functionality. You should call the constructor of the client where you can set the device id.",
                exception.Message);
        }

        [Fact]
        public void NoIdentitySecretCheck()
        {
            Exception exception =
                Assert.Throws<ArgumentException>(
                    () =>
                        DeviceIdIdentitySecretValidator.Validate(DeviceId,
                            ""));
            Assert.Equal(
                "You have not set your identity secret which is needed for this functionality. You should call the constructor of the client where you can set the identity secret.",
                exception.Message);
        }
    }
}
