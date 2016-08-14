using System.Collections.Generic;

namespace skadisteam.trade.Models.TradeOffer
{
    public class AppData
    {
        public int AppId { get; set; }
        
        public string Name { get; set; }
        
        public string Icon { get; set; }
        
        public string Link { get; set; }
        
        public int AssetCount { get; set; }
        
        public string InventoryLogo { get; set; }
        
        public string TradePermissions { get; set; }

        public List<AppContext> RgContexts { get; set; }
    }
}
