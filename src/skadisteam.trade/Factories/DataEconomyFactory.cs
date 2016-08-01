using skadisteam.trade.Interfaces;
using skadisteam.trade.Models.DataEconomy;

namespace skadisteam.trade.Factories
{
    internal static class DataEconomyFactory
    {
        internal static IDataEconomy GetEconomy(string dataEconomyItem)
        {

            if (dataEconomyItem.Contains("classinfo"))
            {
                return new PrivateDataEconomy
                {
                    AppId = int.Parse(dataEconomyItem.Split('/')[1]),
                    ClassId = long.Parse(dataEconomyItem.Split('/')[2]),
                    InstanceId = long.Parse(dataEconomyItem.Split('/')[3])
                };
            }
            else
            {
                return new PublicDataEconomy
                {
                    AppId = int.Parse(dataEconomyItem.Split('/')[0]),
                    ContextId = int.Parse(dataEconomyItem.Split('/')[1]),
                    AssetId = long.Parse(dataEconomyItem.Split('/')[2]),
                    SteamCommunityId = long.Parse(dataEconomyItem.Split('/')[3])
                };
            }
        }
    }
}
