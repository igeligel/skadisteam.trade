using System.Collections.Generic;
using skadisteam.trade.Models.TradeOffer;

namespace skadisteam.trade.Converter
{
    internal class AppContextDataConverter
    {
        internal static List<AppData> FromJsonModel(Dictionary<string, Models.Json.AppData> jsonAppData)
        {
            var result = new List<AppData>();
            foreach (var data in jsonAppData)
            {
                var appData = new AppData
                {
                    Name = data.Value.Name,
                    AppId = data.Value.AppId,
                    AssetCount = data.Value.AssetCount,
                    Icon = data.Value.Icon,
                    InventoryLogo = data.Value.InventoryLogo,
                    Link = data.Value.Link,
                    TradePermissions = data.Value.TradePermissions,
                    RgContexts = new List<AppContext>()
                };

                foreach (var jsonAppContext in data.Value.RgContexts)
                {
                    var appContext = new AppContext
                    {
                        Name = jsonAppContext.Value.Name,
                        AssetCount = jsonAppContext.Value.AssetCount,
                        Id = jsonAppContext.Value.Id
                    };
                    appData.RgContexts.Add(appContext);
                }
                result.Add(appData);
            }
            return result;
        }
    }
}
