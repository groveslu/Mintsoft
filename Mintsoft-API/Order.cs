using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mintsoft.API
{
    [DataContract]
    public class NewOrder
    {
        [DataMember]
        public String OrderNumber { get; set; }

        [DataMember]
        public String ExternalOrderReference { get; set; }


        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public String CompanyName { get; set; }

        [DataMember]
        public String FirstName { get; set; }

        [DataMember]
        public String LastName { get; set; }

        [DataMember]
        public String Address1 { get; set; }

        [DataMember]
        public String Address2 { get; set; }

        [DataMember]
        public String Address3 { get; set; }

        [DataMember]
        public String Town { get; set; }

        [DataMember]
        public String County { get; set; }

        [DataMember]
        public String PostCode { get; set; }

        [DataMember]
        public String Country { get; set; }

        [DataMember]
        public String Email { get; set; }

        [DataMember]
        public String Phone { get; set; }

        [DataMember]
        public String Mobile { get; set; }

        [DataMember]
        public String CourierService { get; set; }

        [DataMember]
        public int? CourierServiceId { get; set; }

        [DataMember]
        public String Channel { get; set; }

        [DataMember]
        public int? ChannelId { get; set; }

        [DataMember]
        public String Warehouse { get; set; }

        [DataMember]
        public int? WarehouseId { get; set; }


        [DataMember]
        [Obsolete]
        public DateTime? DeliveryDate { get; set; }

        [DataMember]
        [Obsolete]
        public DateTime? DespatchDate { get; set; }

        [DataMember]
        public DateTime? RequiredDeliveryDate { get; set; }


        [DataMember]
        public DateTime? RequiredDespatchDate { get; set; }

        [DataMember]
        public String Comments { get; set; }

        [DataMember]
        public String DeliveryNotes { get; set; }

        [DataMember]
        public String GiftMessages { get; set; }

        [DataMember]
        public decimal OrderValue { get; set; }


        [DataMember]
        public List<NewOrderItem> OrderItems;

        [DataMember]
        public List<NewOrderNameValue> OrderNameValues { get; set; }
    }

    [DataContract]
    public class NewOrderNameValue
    {
        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public String Value { get; set; }
    }

    [DataContract]
    public class NewOrderItem
    {
        [DataMember]
        public String SKU { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public String Details { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public decimal UnitPriceVat { get; set; }

        [DataMember]
        public List<NewOrderItemNameValue> OrderItemNameValues { get; set; }

        [DataMember]
        public int? WarehouseId { get; set; }

    }

    [DataContract]
    public class NewOrderItemNameValue
    {
        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public String Value { get; set; }
    }

    [DataContract]
    public class NewOrderComment
    {
        [DataMember]
        public String Comment { get; set; }

        [DataMember]
        public bool Admin { get; set; }
    }
}
