using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace MaxiFlexProxy.App_Code.Models
{
    [DataContract]
    public class sModTransaction
    {
        [DataMember]
        public string footer { get; set; }

        [DataMember]
        public string header { get; set; }
    }
}