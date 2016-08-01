using skadisteam.trade.Interfaces;

namespace skadisteam.trade.Models.DataEconomy
{
    public class PrivateDataEconomy : IDataEconomy
    {
        public int AppId { get; set; }

        public long ClassId { get; set; }

        public long InstanceId { get; set; }
    }
}
