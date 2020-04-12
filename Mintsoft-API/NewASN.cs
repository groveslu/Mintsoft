using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mintsoft.API
{
    public class NewASN
    {
        [DataMember]
        public int WarehouseId { get; set; }

        [DataMember]
        public String POReference { get; set; }

        [DataMember]
        public String Supplier { get; set; }

        [DataMember]
        public DateTime EstimatedDelivery { get; set; }

        [DataMember]
        public String Comments { get; set; }

        [DataMember]
        public String GoodsInType { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public int? ProductSupplierId { get; set; }

        [DataMember]
        public int? ClientId { get; set; }

        [DataMember]
        public List<NewASNItem> Items { get; set; }



    }

    public class NewASNItem
    {
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public String SKU { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }

    [DataContract]
    public class NewASNConnectAction
    {
        [DataMember]
        public String Type { get; set; }

        [DataMember]
        public String SourceASNId { get; set; }

        [DataMember]
        public bool Complete { get; set; }

        [DataMember]
        public int AccountId { get; set; }

        [DataMember]
        public String ExtraCode1 { get; set; }

        [DataMember]
        public String ExtraCode2 { get; set; }
    }
}
