using skadisteam.trade.Constants;
using skadisteam.trade.Interfaces;
using skadisteam.trade.Models.DataEconomy;

namespace skadisteam.trade.Factories
{
    internal static class DataEconomyFactory
    {
        internal static IDataEconomy GetEconomy(string dataEconomyItem)
        {
            if (dataEconomyItem.Contains(RegexPatterns.ClassInfo))
            {
                return new PrivateDataEconomy
                {
                    AppId =
                        int.Parse(dataEconomyItem.Split(Characters.BackSlash)[1]),
                    ClassId =
                        long.Parse(
                            dataEconomyItem.Split(Characters.BackSlash)[2]),
                    InstanceId =
                        long.Parse(
                            dataEconomyItem.Split(Characters.BackSlash)[3])
                };
            }
            return new PublicDataEconomy
            {
                AppId =
                    int.Parse(dataEconomyItem.Split(Characters.BackSlash)[0]),
                ContextId =
                    int.Parse(dataEconomyItem.Split(Characters.BackSlash)[1]),
                AssetId =
                    long.Parse(dataEconomyItem.Split(Characters.BackSlash)[2]),
                SteamCommunityId =
                    long.Parse(dataEconomyItem.Split(Characters.BackSlash)[3])
            };
        }
    }
}
