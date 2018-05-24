using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mintsoft.API
{
    [DataContract]
    public class NewOrderResult
    {
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public int DropShipOrderId { get; set; }

        [DataMember]
        public String OrderNumber { get; set; }

        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public int OrderStatusId { get; set; }

        [DataMember]
        public String OrderStatus { get; set; }

        [DataMember]
        public String Message { get; set; }
    }
}
