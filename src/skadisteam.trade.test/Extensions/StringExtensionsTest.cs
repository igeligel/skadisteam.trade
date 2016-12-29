using skadisteam.trade.Extensions;
using Xunit;

namespace skadisteam.trade.test.Extensions
{
    public class StringExtensionsTest
    {
        [Fact]
        public void RemoveNewLineNoContentCheck()
        {
            const string input = "";
            var result = input.RemoveNewLines();
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void RemoveNewLineContentCheck()
        {
            const string input = "adawawn\n awdnjawbawnhafbhgb\t\rdwajnda";
            var result = input.RemoveNewLines();
            Assert.Equal("adawawn awdnjawbawnhafbhgb\t\rdwajnda", result);
        }

        [Fact]
        public void RemoveTabNoContentCheck()
        {
            const string input = "";
            var result = input.RemoveTabs();
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void RemoveTabContentCheck()
        {
            const string input = "adawawn\n awdnjawbawnhafbhgb\t\rdwajnda";
            var result = input.RemoveTabs();
            Assert.Equal("adawawn\n awdnjawbawnhafbhgb\rdwajnda", result);
        }

        [Fact]
        public void RemoveOfferingTradeMessageCheck()
        {
            const string input = "Gaben offered you a trade:";
            var result = input.RemoveOfferingTradeMessage();
            Assert.Equal("Gaben ", result);
        }
    }
}
