using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DontiaChinaProxy.App_Code;
using System.Data;
using System.Drawing;
using System.ServiceModel.Channels;

namespace DontiaChinaProxy
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DentaLINKData" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DentaLINKData.svc or DentaLINKData.svc.cs at the Solution Explorer and start debugging.
    public class DontiaChinaData : IDontiaChinaData
    {
        BusinessLogicLayer _BusinessLogic = new BusinessLogicLayer();
        public void DoWork()
        {
        }

        public string[] GetTreatment(string prefix)
        {           
            return _BusinessLogic.GetTreatment(prefix);
        }
        public string[] PriceClinic(string prefix)
        {
            return _BusinessLogic.PriceClinic(prefix);
        }
        public string[] PriceDentist(string prefix)
        {
            return _BusinessLogic.PriceDentist(prefix);
        }
        public string[] PlanBenefit(string prefix)
        {
            return _BusinessLogic.PlanBenefit(prefix);
        }
        public string[] ucClaimTreatment(string prefixText, string UserID, string UserType)
        {
            return _BusinessLogic.ucClaimTreatment(prefixText, UserID, UserType);
        }

        public string[] ucBillingCorporation(string prefix)
        {
            return _BusinessLogic.ucBillingCorporation(prefix);
        }
        public string[] GetMainCorp(string prefix)
        {
            return _BusinessLogic.GetMainCorp(prefix);
        }
        public string[] GetPlanID(string prefix)
        {
            return _BusinessLogic.GetPlanID(prefix);
        }
        public string ReportBilling(string _billNo, string _corpCode, decimal _gtotal, string _LogNo)
        {
            ReportGenerator _pdf = new App_Code.ReportGenerator();
            return _pdf.BillingPdf(_billNo, _corpCode, _gtotal, _LogNo);
        }
        public byte[] LOGReport(string x)
        {
            LOGClassPdf _LogCLass = new LOGClassPdf();
            return _LogCLass.PrintLOG(x);
        }
        public string[] GetSGPostal(string prefixText) 
        {
            return _BusinessLogic.GetSGPostal(prefixText);
        }
        public string[] GetCorporation(string prefixText)
        {
            return _BusinessLogic.GetCorporation(prefixText);
        }
        public string[] GetPatient(string prefixText, string contextKey, string corpCode)
        {
            return _BusinessLogic.GetPatient(prefixText, contextKey, corpCode);
        }
        public string[] GetClinic(string prefixText)
        {
            return _BusinessLogic.GetClinic(prefixText);
        }
        public string[] ucLGGetServiceProvider(string prefixText)
        {
            return _BusinessLogic.ucLGGetServiceProvider(prefixText);
        }
        public bool ValidateCredentials(string UserID, string LoginPwd)
        {
            return _BusinessLogic.ValidateCredentials(UserID, LoginPwd);
            //return true;
        }

        public string[] ucGetHREmailAddress(string prefixText)
        {
            return _BusinessLogic.ucGetHREmailAddress(prefixText);
        }
        public bool InsertAppointment(string MemberID, string ClinicCode, string ContactNo, string PreferredDateFrom, string PreferredDateTo)
        {
              return _BusinessLogic.InsertAppointment(MemberID, ClinicCode, ContactNo, PreferredDateFrom, PreferredDateTo);
        }
        public string GetMembersContactNo(string MemberID)
        {
            return _BusinessLogic.GetMembersContactNo(MemberID);
        }
        public Bitmap GenerateQR(string prefixText)
        {
            LOGClassPdf Log = new LOGClassPdf();
            return Log.GenerateQR(prefixText);
        }

        public byte[] PrintLOG(string x)
        {
            LOGClassPdf Log = new LOGClassPdf();
            return Log.PrintLOG(x);
        }

        public byte[] GenerateQR_eCard(string prefixText)
        {
            LOGClassPdf Log = new LOGClassPdf();
            return Log.GenerateQR_eCard(prefixText);
        }
        public bool UpdateClaimRead(string logNo)
        {
            return _BusinessLogic.UpdateClaimRead(logNo);
        }

        public void UpdatePDPAPolicy(string UserName, string Ver)
        {
            _BusinessLogic.UpdatePDPAPolicy(UserName, Ver);
        }

        // ====== eCard ======
        public string GetSequenceLOGReq()
        {
            return _BusinessLogic.GetSequenceLOGReq();
        }

        public bool RequestVerificationCode_eCard(string MemberID, string ClinicCode, string ContactNo, string Fullname, string PreferredDateFrom, string PreferredDateTo)
        {
            return _BusinessLogic.RequestVerificationCode_eCard(MemberID, ClinicCode, ContactNo, Fullname, PreferredDateFrom, PreferredDateTo);
        }

        public bool UpdateVerificationCode_eCard(string MemberID, string ClinicCode, string ContactNo, string Fullname, string PreferredDateFrom)
        {
            return _BusinessLogic.UpdateVerificationCode_eCard(MemberID, ClinicCode, ContactNo, Fullname, PreferredDateFrom);
        }

        public string[] GetPatientName_eCard(string prefixText, string _strMainMemberID, string _strCorpCode)
        {
            return _BusinessLogic.GetPatientName(prefixText, _strMainMemberID, _strCorpCode);
        }

        public DataTable SearchLOG_eCard(string _strMainMemberID )
        {
            return _BusinessLogic.SearchLOG(_strMainMemberID );
        }
        public string[] GetMemberDependentsRA(string _strMainMemberID)
        {
            return _BusinessLogic.GetMemberDependentsRA(_strMainMemberID);
        }

        public string ValidateCredential_eCard(string _strUserID, string _strPwd) 
        {
            return _BusinessLogic.ValidateCredential(_strUserID, _strPwd);
        }

        public string[] GetTreatmentPlan(string prefix)
        {
            return _BusinessLogic.GetTreatmentPlan(prefix);
        }

        public string[] SearchLOGList_eCard(string _strMainMemberID)
        {
            return _BusinessLogic.SearchLOGList(_strMainMemberID);
        }


        public string[] GetClinicCoordinates_eCard(string prefixText)
        {
            return _BusinessLogic.GetClinicCoordinates_eCard(prefixText);
        }

        public string[] GetPhysicianProvider_eCard(string ClinicCode)
        {
            return _BusinessLogic.GetPhysicianProvider_eCard(ClinicCode);
        }

        public string[] GetPhysicianSchedule_eCard(string DentistCode)
        {
            return _BusinessLogic.GetPhysicianSchedule_eCard(DentistCode);
        }

        public string[] GetAllClinic_eCard()
        {
            return _BusinessLogic.GetAllClinic_eCard();
        }

        public string[] SearchLOG_eCard_iOS(string _strMainMemberID)
        {
            return _BusinessLogic.SearchLOG_eCard_iOS(_strMainMemberID);
        }

        public bool isAppLatestVersion_eCard(string _strAppVersion)
        {
            return _BusinessLogic.isAppLatestVersion_eCard(_strAppVersion);
        }

        public string RequestForgotPwd_eCard(string _strUserID)
        {
            return _BusinessLogic.RequestForgotPwd_eCard(_strUserID);
        }

        public string GetClientIPAddress()
        {
            string ip = string.Empty; ;
            var props = OperationContext.Current.IncomingMessageProperties;
            var endpointProperty = props[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpointProperty != null)
            {
                ip = endpointProperty.Address;
            }
            return ip;
        }

    }


}
