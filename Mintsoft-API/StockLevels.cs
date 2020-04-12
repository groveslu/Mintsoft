using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mintsoft.API
{


    public class StockLevelResult
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int ClientId { get; set; }
        public string SKU { get; set; }
        public int Level { get; set; }
        public bool PreOrderable { get; set; }
        public bool Bundle { get; set; }
        public int LowStockLevel { get; set; }
        public List<StockLevelBreakdown> Breakdown { get; set; }
    }

    public class StockLevelBreakdown
    {
        public int Quantity { get; set; }
        public string BatchNo { get; set; }
        public string SerialNo { get; set; }
        public string BestBefore { get; set; }
        public string Type { get; set; }
    }

}
