using DontiaChinaProxy.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Drawing;

namespace DontiaChinaProxy
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDentaLINKData" in both code and config file together.
    [ServiceContract]
    public interface IDontiaChinaData
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        string[] GetTreatment(string prefix);
        [OperationContract]
        string[] GetTreatmentPlan(string prefixText);
        [OperationContract]
        string[] PriceClinic(string prefix);
        [OperationContract]
        string[] PriceDentist(string prefix);
        [OperationContract]
        string[] PlanBenefit(string prefix);
        //[OperationContract]
        //string[] ucLGTreatment(string prefix, string contextkey);
        [OperationContract]
        string[] ucBillingCorporation(string prefix);

        [OperationContract]
        string[] GetMemberDependentsRA(string prefix);

        [OperationContract]
        string[] GetMainCorp(string prefix);
        //[OperationContract]
        //string[] ucLGServiceProvider(string prefix);
        [OperationContract]
        string ReportBilling(string _billNo, string _corpCode, decimal _gtotal, string _LogNo);
        [OperationContract]
        string[] GetPlanID(string prefix);
        [OperationContract]
        string[] ucClaimTreatment(string prefix, string UserID, string UserType);
        [OperationContract]
        byte[] LOGReport(string x);
        [OperationContract]
        string[] GetSGPostal(string prefix);

        [OperationContract]
        string[] GetCorporation(string prefixText);

        [OperationContract]
        string[] GetPatient(string prefixText, string contextKey, string corpCode);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        string[] GetClinic(string prefixText);

        [OperationContract]
        string[] ucLGGetServiceProvider(string prefixText);
        [OperationContract]
        bool ValidateCredentials(string UserID, string LoginPwd);

        [OperationContract]
        string[] ucGetHREmailAddress(string prefixText);

        [OperationContract]
        Bitmap GenerateQR(string prefixText);

        [OperationContract]
        bool InsertAppointment(string MemberID, string ClinicCode, string ContactNo, string PreferredDateFrom, string PreferredDateTo);

        [OperationContract]
        void UpdatePDPAPolicy(string UserName, string Ver);

        [OperationContract]
        bool UpdateClaimRead(string logNo);

        [OperationContract]
        string GetMembersContactNo(string MemberID);

        [OperationContract]
        DataSet EMR(string MemberID);

        [OperationContract]
        byte[] EMRpdfFile(string Filename);
        // ====== eCard ======

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        string GetSequenceLOGReq();

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        bool RequestVerificationCode_eCard(string MemberID, string ClinicCode, string ContactNo, string Fullname, string PreferredDateFrom, string PreferredDateTo);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        bool UpdateVerificationCode_eCard(string MemberID, string ClinicCode, string ContactNo, string Fullname, string PreferredDateFrom);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        string[] GetPatientName_eCard(string prefixText, string _strMainMemberID, string _strCorpCode);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        DataTable SearchLOG_eCard(string _strMainMemberID );

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        string ValidateCredential_eCard(string _strUserID, string _strPwd);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        byte[] PrintLOG(string x);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        byte[] GenerateQR_eCard(string prefixText);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        string[] SearchLOGList_eCard(string _strMainMemberID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        string[] GetClinicCoordinates_eCard(string prefixText);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        string[] GetPhysicianProvider_eCard(string ClinicCode);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        string[] GetPhysicianSchedule_eCard(string DentistCode);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        string[] GetAllClinic_eCard();

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        string[] SearchLOG_eCard_iOS(string _strMainMemberID);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        bool isAppLatestVersion_eCard(string _strAppVersion);

        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        string RequestForgotPwd_eCard(string _strUserID);


        [OperationContract]
        [FaultContract(typeof(ServiceData))]
        string GetClientIPAddress();


        [OperationContract]
        bool ChangePasswordKeystone(string memberID, string userName, string newPassword);
    }

}
