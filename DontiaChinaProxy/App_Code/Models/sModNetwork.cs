using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DontiaChinaProxy.App_Code.Models
{
    [DataContract]
    public class sModNetwork
    {
        //Benefit Module
        [DataMember]
        public string Search { get; set; }

        [DataMember]
        public string BenefitDesc { get; set; }

        [DataMember]
        public string BenefitCode { get; set; }

        [DataMember]
        public string Limit { get; set; }

        [DataMember]
        public string Createby { get; set; }

        //Treatment Module
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string TreatmentCode { get; set; }

        [DataMember]
        public string TreatmentName { get; set; }

        [DataMember]
        public string TreatmentDesc { get; set; }

        [DataMember]
        public string Check { get; set; }

        [DataMember]
        public string List { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string EffectiveDate { get; set; }

        [DataMember]
        public string ExpiryDate { get; set; }

        [DataMember]
        public string Price { get; set; }

        [DataMember]
        public string DentistCode { get; set; }

        [DataMember]
        public string ClinicCode { get; set; }

        [DataMember]
        public string PerProvider { get; set; }

        [DataMember]
        public string Provider { get; set; }
    }
}