using System;
using System.Runtime.Serialization;

namespace Mintsoft.API
{
    [DataContract]
    public class NewOrderConnectAction
    {
        [DataMember]
        public String Type { get; set; }

        [DataMember]
        public String SourceOrderId { get; set; }

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