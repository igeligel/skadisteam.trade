namespace skadisteam.trade.Extensions
{
    internal static class StringExtensions
    {
        internal static string RemoveNewLines(this string input)
        {
            return input.Replace("\n", string.Empty);
        }

        internal static string RemoveTabs(this string input)
        {
            return input.Replace("\t", string.Empty);
        }

        internal static string RemoveOfferingTradeMessage(this string input)
        {
            return input.Replace("offered you a trade:", string.Empty);
        } 
    }
}
