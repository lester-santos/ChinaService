using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace MaxiFlexProxy.App_Code.Models
{
    [DataContract]
    public class sModUser
    {
        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MI { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string EmailAdd { get; set; }

        [DataMember]
        public string LoginPwd { get; set; }

        [DataMember]
        public string LoginPwdNew { get; set; }

        [DataMember]
        public string LoginPwdNewComfirmation { get; set; }

        [DataMember]
        public string LoginPwdRetries { get; set; }

        [DataMember]
        public string DateActivated { get; set; }

        [DataMember]
        public string Active { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Role { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public string CreatedDate { get; set; }

        [DataMember]
        public string UpdatedBy { get; set; }

        [DataMember]
        public string UpdatedDate { get; set; }

        [DataMember]
        public string LastLoginDate { get; set; }

        [DataMember]
        public string ModuleID { get; set; }

        [DataMember]
        public int ParentID { get; set; }

        [DataMember]
        public int WrongPasswordCnt { get; set; }

        [DataMember]
        public string UserType { get; set; }

        [DataMember]
        public string ProcessType { get; set; }

        [DataMember]
        public string ConfirmationCode { get; set; }

        [DataMember]
        public string CorpCode { get; set; }

        [DataMember]
        public string ClinicCode { get; set; }

        [DataMember]
        public string DentistCode { get; set; }

        [DataMember]
        public string EmployeeNo { get; set; }

        [DataMember]
        public string IsApprove { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string UserLevel { get; set; }

        [DataMember]
        public string UserLevelName { get; set; }

        [DataMember]
        public string LastChangePwdDate { get; set; }

        [DataMember]
        public string NoOfDaysLastChangePwd { get; set; }

        [DataMember]
        public string NoOfDaysLastLogIn { get; set; }

        [DataMember]
        public string MemberID { get; set; }

        [DataMember]
        public string CorpName { get; set; }

        [DataMember]
        public string ReqType { get; set; }

        [DataMember]
        public string Photo { get; set; }

        [DataMember]
        public string ClinicName { get; set; }

        [DataMember]
        public string DentistName { get; set; }

        [DataMember]
        public string DateFormat { get; set; }

        [DataMember]
        public string ClinicStatus { get; set; }


        [DataMember]
        public string ReadPDPAPolicy { get; set; }

        [DataMember]
        public string PlanCode { get; set; }

        [DataMember]
        public string WithCardDesign { get; set; }

        [DataMember]
        public string IDNo { get; set; }

        [DataMember]
        public string SetSecurityQuestion { get; set; }
    }
}