using skadisteam.trade.Extensions;
using skadisteam.trade.Models.Confirmation;
using Xunit;

namespace skadisteam.trade.test.Extensions
{
    public class ConfirmationTagExtensionsTest
    {
        [Fact]
        public void AllowTagCheck()
        {
            const ConfirmationTag allowConfirmationTag = ConfirmationTag.Allow;
            var result = allowConfirmationTag.ToText();
            Assert.Equal(typeof(string), result.GetType());
            Assert.Equal("allow", result);
        }

        [Fact]
        public void ConfirmationTagCheck()
        {
            const ConfirmationTag confirmConfirmationTag =
                ConfirmationTag.Confirm;
            var result = confirmConfirmationTag.ToText();
            Assert.Equal(typeof(string), result.GetType());
            Assert.Equal("conf", result);
        }
    }
}
