using skadisteam.trade.Interfaces;

namespace skadisteam.trade.Models.DataEconomy
{
    public class PublicDataEconomy : IDataEconomy
    {
        public int AppId { get; set; }

        public long AssetId { get; set; }

        public int ContextId { get; set; }
        
        public long SteamCommunityId { get; set; }
    }
}
