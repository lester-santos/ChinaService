using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MedilinkSecurity;
using DontiaChinaProxy.App_Code.Models;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.Threading;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DontiaChinaProxy.App_Code
{
    public class BusinessLogicLayer
    {
        DataAccessLayer _DataAccess = new DataAccessLayer();
        string _strSMTPServer = ConfigurationManager.AppSettings["IPSMTPServer"].ToString();
        string _strEmailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString();
        string _strUri = ConfigurationManager.AppSettings["StrUri"].ToString();
        string _strDentaLINKLeasedLink = ConfigurationManager.AppSettings["DentaLINKLeasedLink"].ToString();
        string _strCreateCredentialBcc = ConfigurationManager.AppSettings["CreateCredentialEmailBcc"].ToString();

        public string[] GetTreatment(string prefixText)
        {
            DataTable _dt = _DataAccess.AutoComplete(prefixText);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }

        public string[] PriceClinic(string prefix)
        {

            DataTable _dt = _DataAccess.AutoCompleteTreatmentClinic(prefix);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }

        public string[] PriceDentist(string prefix)
        {

            DataTable _dt = _DataAccess.AutoCompleteTreatmentDentist(prefix);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }

        //public string[] ucLGTreatment(string prefixText, string contextKey)
        //{
        //    DataAccessLayer _DataAccess = new DataAccessLayer();
        //    DataTable _dt = _DataAccess.ACClaimTreatment(prefixText,);
        //    List<string> responses = new List<string>();
        //    foreach (DataRow dr in _dt.Rows)
        //    {
        //        responses.Add((dr[0].ToString()));
        //    }
        //    return responses.ToArray();
        //}
        public string[] ucBillingCorporation(string prefix)
        {

            DataTable _dt = _DataAccess.ucBillingCorporation(prefix);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }


        public string[] PlanBenefit(string prefix)
        {

            DataTable _dt = _DataAccess.AutoCompletePlanBenefit(prefix);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }

        public string[] GetMainCorp(string prefix)
        {

            DataTable _dt = _DataAccess.AutoCompleteMainCorp(prefix);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }

        public string[] GetPlanID(string prefix)
        {

            DataTable _dt = _DataAccess.AutoCompletePlanID(prefix);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }

        public string[] ucClaimTreatment(string prefixText, string UserID, string UserType)
        {

            DataTable _dt = _DataAccess.ACClaimTreatment(prefixText, UserID, UserType);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {

                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }

            }
            return responses.ToArray();
        }



        public string[] GetSGPostal(string prefixText)
        {

            DataTable _dt = _DataAccess.GetSGPostal(prefixText);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }

        public string[] GetCorporation(string prefixText)
        {

            DataTable _dt = _DataAccess.GetCorporation_AC(prefixText);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }
        //Daryl
        public string[] GetPatient(string prefixText, string contextKey, string corpCode)
        {

            DataTable _dt = _DataAccess.GetPatient_AC(prefixText, contextKey, corpCode);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }

        public string[] GetClinic(string prefixText)
        {

            DataTable _dt = _DataAccess.GetClinic_AC(prefixText);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }



        public string[] ucLGGetServiceProvider(string prefixText)
        {

            DataTable _dt = _DataAccess.ucLGGetServiceProvider(prefixText);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }

        public string[] ucGetHREmailAddress(string prefixText)
        {

            DataTable _dt = _DataAccess.ucGetHREmailAddress(prefixText);
            List<string> responses = new List<string>();
            foreach (DataRow dr in _dt.Rows)
            {
                responses.Add((dr[0].ToString()));
            }
            return responses.ToArray();
        }

        public DataSet EMR(string memberID)
        {

            DataSet ds = _DataAccess.EMR(memberID);
            return ds;
        }
        public byte[] EMRpdfFile(string Filename)
        {


            string EMRContainer = ConfigurationManager.AppSettings["UploadedEMRfiles"].ToString();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(EMRContainer);
            container.CreateIfNotExists();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(Filename);
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                blockBlob.DownloadToStream(ms);
                return ms.ToArray();
            }
        }


        public bool ValidateCredentials(string UserId, string LoginPwd)
        {
            bool _Valid = true;
            Crypto _Crypto = new Crypto();
            DataTable _dt = _DataAccess.ValidateCredentials(UserId);

            if (_dt.Rows.Count == 0)
            {
                _Valid = false;
            }
            else
            {
                if (LoginPwd == _Crypto.Decrypt(_dt.Rows[0]["Password"].ToString()))
                {
                    _Valid = true;
                }
                else
                {
                    _Valid = false;
                }

            }

            return _Valid;

        }

        public string[] ReferralAdd(string RequestNo, string PatientName, string ServiceProvider, string AvailmentDate)
        {

            DataTable _dt = _DataAccess.ReferralAdd(RequestNo, PatientName, ServiceProvider, AvailmentDate);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {

                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }

            }
            return responses.ToArray();
        }

        public string[] SequenceAdd()
        {

            DataTable _dt = _DataAccess.SequenceAdd();
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {

                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }

            }
            return responses.ToArray();
        }

        public bool InsertAppointment(string MemberID, string ClinicCode, string ContactNo, string PreferredDateFrom, string PreferredDateTo)
        {
            return _DataAccess.InsertAppointment(MemberID, ClinicCode, ContactNo, PreferredDateFrom, PreferredDateTo);
        }
        public string GetMembersContactNo(string MemberID)
        {
            return _DataAccess.GetMembersContactNo(MemberID);
        }

        // ===== eCard =====

        public string GetSequenceLOGReq()
        {
            string _strReqNo = string.Empty;
            string _ret = _DataAccess.GetSequenceLOGReq();
            string _strLOGRequest = _ret.ToString();
            string _strPadLeftLOG = _strLOGRequest.PadLeft(9, '0');
            _strReqNo = "6" + _strPadLeftLOG.Trim();
            return _strReqNo;
        }

        public bool RequestVerificationCode_eCard(string MemberID, string ClinicCode, string ContactNo, string Fullname, string PreferredDateFrom, string PreferredDateTo)
        {
            return _DataAccess.RequestVerificationCode_eCard(MemberID, ClinicCode, ContactNo, Fullname, PreferredDateFrom, PreferredDateTo);
        }

        public bool UpdateVerificationCode_eCard(string MemberID, string ClinicCode, string ContactNo, string Fullname, string PreferredDateFrom)
        {
            return _DataAccess.UpdateVerificationCode_eCard(MemberID, ClinicCode, ContactNo, Fullname, PreferredDateFrom);
        }


        public string[] GetPatientName(string prefixText, string _strMainMemberID, string _strCorpCode)
        {
            DataTable _dt = _DataAccess.GetPatientName(prefixText, _strMainMemberID, _strCorpCode);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {

                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }

            }

            return responses.ToArray();
        }

        public DataTable SearchLOG(string _strMainMemberID)
        {
            return _DataAccess.SearchLOG(_strMainMemberID);
        }
    

        public string ValidateCredential(string _strUserID, string _strPwd)
        {
            string _strReturn = string.Empty;
            string _strInputPwd = _strPwd.Trim();
            bool _success = true;
            List<sModUser> _listUser = new List<sModUser>();
            sModUser _modUser = new sModUser();
            string _strMemberStatus = string.Empty;
            string _strExpiryDate = string.Empty;
            string _strCardNo = string.Empty;
            string _strExpiryDateMMddYYYY = string.Empty;
            string _strReadPDPAPolicy = string.Empty;

            sModTransaction _ModTran = new sModTransaction()
            {
                header = HttpContext.Current.Server.MapPath(@"~\img\Email\Header.jpg"),
                footer = HttpContext.Current.Server.MapPath(@"~\img\Email\Footer.jpg")
            };

            try
            {
                _listUser = _DataAccess.Login(_strUserID);
                if (_listUser.Count > 0)
                {

                    Crypto _Crypto = new Crypto();
                    if (_strInputPwd == _Crypto.Decrypt(_listUser[0].LoginPwd) && _listUser[0].Active == "Y")
                    {
                        if (_listUser[0].ClinicStatus == "N")
                        {
                            _strReturn = "FAILED|The Clinic where you belong is already inactive.||||||||";
                        }
                        else
                        {

                            if (_listUser[0].UserType.ToString() != "E" && string.IsNullOrEmpty(_listUser[0].MemberID))
                            {
                                _strReturn = "FAILED|This application is intend for Member only.||||||||";
                            }
                            else
                            {
                                string _strMemberExpiryDate = _DataAccess.GetMemberExpiryDate_eCard(_listUser[0].MemberID.ToString().Trim());
                                string[] MemberExpiry = _strMemberExpiryDate.Split('|');
                                _strExpiryDate = MemberExpiry[0].ToString();
                                _strMemberStatus = MemberExpiry[1].ToString();
                                _strCardNo = MemberExpiry[2].ToString();
                                _strExpiryDateMMddYYYY = MemberExpiry[3].ToString();

                                if (_strMemberStatus.ToString() != "A")
                                {
                                    //_strReturn = "FAILED|Your Account is Deactivated. Please contact your System Admin.||||||";
                                    _strReturn = "FAILED|Member not yet approved activated.||||||||";
                                }
                                else
                                {

                                    //bool isActive = _DataAccess.isUserActive_eCard(_strUserID);
                                    string _strMainMemberID = string.Empty;

                                    //if (isActive == true)
                                    //{
                                    //    _strReturn = "FAILED|User is Currently logged in.||||||";
                                    //}
                                    //else
                                    //{
                                    //======================================================
                                    //                    SUCCESS HERE
                                    //======================================================

                                    _modUser = new sModUser();
                                    _modUser.UserID = _listUser[0].UserID.ToString();
                                    _modUser.LoginPwd = "";
                                    _modUser.Active = "Y";
                                    _modUser.WrongPasswordCnt = 0;
                                    _modUser.ReqType = "UpdateLastLogin";
                                    //_modUser.ReadPDPAPolicy = _listUser[0].ReadPDPAPolicy.ToString();
                                    _success = _DataAccess.UpateUserDetails(_modUser);
                                    _strMainMemberID = _DataAccess.GetMainMemberID(_listUser[0].MemberID);

                                    _strReturn = "SUCCESS|Welcome|" + _listUser[0].CorpCode + "|" + _listUser[0].MemberID + "|" + _strMainMemberID.Trim() + "|" + _listUser[0].FirstName + " " + _listUser[0].LastName + "|" + _listUser[0].CorpName + "|" + _strExpiryDate.ToString() + "|" + _strCardNo.ToString() + "|" + _strExpiryDateMMddYYYY.ToString() + "|" + _listUser[0].ReadPDPAPolicy;
                                    //}                               
                                }
                            }
                        }
                    }
                    else
                    {
                        if (_listUser[0].Active == "N")
                        {
                            _strReturn = "FAILED|Your Account is Deactivated. Please contact your System Admin||||||||";
                        }
                        else if (_listUser[0].WrongPasswordCnt > 2)
                        {
                            _modUser = new sModUser();
                            string _strNewPwd = CreateRandomPassword(9);
                            _modUser.UserID = _listUser[0].UserID.ToString();
                            _modUser.LoginPwd = _Crypto.Encrypt(_strNewPwd);
                            _modUser.Active = "";
                            _modUser.WrongPasswordCnt = _listUser[0].WrongPasswordCnt;
                            _modUser.ReqType = "ExcessiveWrongPwd";
                            _success = _DataAccess.UpateUserDetails(_modUser);
                            SendEmailExceedPasswordAttempt(_ModTran.header, _ModTran.footer, _listUser[0].FirstName + " " + _listUser[0].LastName, _listUser[0].UserID.ToString(), _strNewPwd);
                            _strReturn = "FAILED|Your password has been reset and sent to your registered email address.||||||||";

                        }
                        else
                        {
                            int _CntPwdTry;
                            _CntPwdTry = _listUser[0].WrongPasswordCnt + 1;

                            if (_CntPwdTry > 2)
                            {


                                _modUser = new sModUser();
                                string _strNewPwd = CreateRandomPassword(9);
                                _modUser.UserID = _listUser[0].UserID.ToString();
                                _modUser.LoginPwd = _Crypto.Encrypt(_strNewPwd);
                                _modUser.Active = "";
                                _modUser.WrongPasswordCnt = _CntPwdTry;
                                _modUser.ReqType = "ExcessiveWrongPwd";
                                _success = _DataAccess.UpateUserDetails(_modUser);
                                //SendEmailExceedPasswordAttempt(_listUser[0].UserID.ToString(), _strNewPwd);
                                SendEmailExceedPasswordAttempt(_ModTran.header, _ModTran.footer, _listUser[0].FirstName + " " + _listUser[0].LastName, _listUser[0].UserID.ToString(), _strNewPwd);
                                _strReturn = "FAILED|Your password has been reset and sent to your registered email address.||||||||";


                            }
                            else
                            {
                                _modUser = new sModUser();
                                _modUser.UserID = _listUser[0].UserID.ToString();
                                _modUser.LoginPwd = "";
                                _modUser.Active = "";
                                _modUser.WrongPasswordCnt = _CntPwdTry;
                                _modUser.ReqType = "UpdatePwdCount";
                                _success = _DataAccess.UpateUserDetails(_modUser);
                                _strReturn = "FAILED|Username or Password is incorrect.||||||||";


                            }
                        }
                    }
                }
                else
                {
                    _strReturn = "NO|Username or Password is incorrect.||||||||";
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _strReturn;
        }


        public string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "abcdefghijkmnopqrstuvwxyz!@$?ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }

            return new string(chars);
        }


        public string[] GetTreatmentPlan(string prefixText)
        {
            DataTable _dt = _DataAccess.GetTreatmentPlan(prefixText);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }
            return responses.ToArray();
        }

        public string[] SearchLOGList(string _strMainMemberID)
        {
            //DataAccessLayer _DataAccess = new DataAccessLayer();
            //return _DataAccess.SearchLOG(_strMainMemberID);

            DataTable _dt = _DataAccess.SearchLOG(_strMainMemberID);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {

                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }

            }

            return responses.ToArray();
        }

        public string[] GetClinicCoordinates_eCard(string prefixText)
        {
            DataTable _dt = _DataAccess.GetClinicCoordinates_eCard(prefixText);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }

            return responses.ToArray();
        }

        public string[] GetPhysicianProvider_eCard(string ClinicCode)
        {
            DataTable _dt = _DataAccess.GetPhysicianProvider_eCard(ClinicCode);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }

            return responses.ToArray();
        }

        public string[] GetMemberDependentsRA(string MemberID)
        {
            DataTable _dt = _DataAccess.GetMemberDependentsRA(MemberID);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }

            return responses.ToArray();
        }


        public string[] GetPhysicianSchedule_eCard(string DentistCode)
        {
            DataTable _dt = _DataAccess.GetPhysicianSchedule_eCard(DentistCode);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }

            return responses.ToArray();
        }


        public string[] GetAllClinic_eCard()
        {
            DataTable _dt = _DataAccess.GetAllClinic_eCard();
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }

            return responses.ToArray();
        }

        public string[] SearchLOG_eCard_iOS(string _strMainMemberID)
        {
            DataTable _dt = _DataAccess.SearchLOG(_strMainMemberID);
            List<string> responses = new List<string>();
            if (_dt.Rows.Count == 0)
            {
                responses.Add(("NO RECORD FOUND"));
            }
            else
            {
                foreach (DataRow dr in _dt.Rows)
                {
                    responses.Add((dr[0].ToString()));
                }
            }

            return responses.ToArray();
        }

      


        public bool _SendMail(List<string> msgTo, List<string> msgBCC, List<string> msgCC, string msgBody, List<string> msgAttachment, string msgSubject, System.Net.Mail.AlternateView view)
        {
            bool _succcess = false;
            SmtpClient sc = new SmtpClient(_strSMTPServer);

            MailMessage mm = new MailMessage();

            mm.IsBodyHtml = true;
            mm.From = new MailAddress(_strEmailFrom);
            mm.Body = msgBody;
            mm.Subject = msgSubject;
            mm.Priority = MailPriority.High;
            mm.AlternateViews.Add(view);
            if (msgTo != null)
            {
                foreach (string strmsgTo in msgTo)
                {
                    mm.To.Add(strmsgTo.ToString());
                }
            }

            if (msgBCC != null)
            {
                foreach (string strmsgBCC in msgBCC)
                {
                    mm.Bcc.Add(strmsgBCC.ToString());
                }
            }

            //if (msgCC != null)
            //{
            //    foreach (string strmsgCC in msgCC)
            //    {
            //        mm.CC.Add(strmsgCC.ToString());
            //    }
            //}

            if (msgAttachment != null)
            {
                foreach (string strMsgAttachment in msgAttachment)
                {
                    mm.Attachments.Add(new Attachment(strMsgAttachment));
                }
            }

            try
            {
                sc.Send(mm);
                _succcess = true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            { }

            return _succcess;
        }

        private bool SendEmailExceedPasswordAttempt(string header, string footer, string Fullname, string username, string Password)
        {
            string _strCreateCredentialCc = ConfigurationManager.AppSettings["CreateCredentialEmailBcc"].ToString();
            bool _success = true;
            try
            {
                Crypto _Crypto = new Crypto();

                List<string> msgTo = new List<string>();
                List<string> msgBCC = new List<string>();
                List<string> msgCC = new List<string>();
                string msgBody = string.Empty;
                List<string> msgAttachment = new List<string>();
                string msgSubject = string.Empty;
                var inlineLogoHeader = new LinkedResource(header);
                inlineLogoHeader.ContentId = Guid.NewGuid().ToString();
                var inlineLogoFooter = new LinkedResource(footer);
                inlineLogoFooter.ContentId = Guid.NewGuid().ToString();
                string link = string.Format(@_strUri + "://" + _strDentaLINKLeasedLink + "/Login.aspx?x=|" + _Crypto.Encrypt(Password)) + "|" + _Crypto.Encrypt(username);

                StringBuilder sb = new StringBuilder();
                msgTo.Add(username);
                string[] arrEmailBcc = _strCreateCredentialBcc.Split(';');
                foreach (string str in arrEmailBcc)
                { msgBCC.Add(str); }
                msgSubject = "DóntiaCare Exceeded Wrong Password Attempt";
                sb.Append("<style>table,th,td {border : 1px solid black; border-collapse: collapse;}th, td { padding: 5px; text-align: left; font-size : 10pt} p{font-size : 10pt}</style>");
                sb.Append("<img style='width:100%' src= cid:" + inlineLogoHeader.ContentId + " />");
                sb.Append("<h4>EXCEEDED WRONG PASSWORD ATTEMPT</h4>");
                sb.Append("<p>Hi " + Fullname + ",</p>");
                sb.Append("<p>Please be informed that your password has been reset.</p>");
                sb.Append("<p>Please click <a href=" + link + ">here</a> to login to your account</p>");
                sb.Append("<p>If the link does not work, please copy and paste this below link on your web browser</p>");
                sb.Append("<p>" + link + "</p><br/>");
                sb.Append("<p>Thanks,<br/> DóntiaCare</p>");
                sb.Append("<p style='font-size: 10pt; font-family: Tahoma;'>Replies sent to this email address cannot be answered. For additional help, please contact us at <span style='color:#FE9A2E'>+65 6737 8088</span> or <span style='color:#FE9A2E'><u>enquiry@dontiacare.com</u></span></p>");
                sb.Append("<img style='width:100%' src=cid:" + inlineLogoFooter.ContentId + " />");
                msgBody = sb.ToString();
                var view = AlternateView.CreateAlternateViewFromString(msgBody, null, "text/html");
                view.LinkedResources.Add(inlineLogoHeader);
                view.LinkedResources.Add(inlineLogoFooter);
                _success = _SendMail(msgTo, msgBCC, msgCC, msgBody, msgAttachment, msgSubject, view);


                return _success;

            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void BackgroundWorker(string _param1, string param2, string _strtype)
        {
            object _obj = new object[3] { _param1, param2, _strtype };
            Thread _thread = new System.Threading.Thread(new ParameterizedThreadStart(worker));
            _thread.Start(_obj);
        }

        private void worker(object _objparam)
        {
            try
            {
                Array _arr = new object[3];
                _arr = (Array)_objparam;


                if ((string)_arr.GetValue(2) == "ExcessiveWrongPwdEmail")
                {
                    //ExcessiveWrongPwdEmail((string)_arr.GetValue(0), (string)_arr.GetValue(1));
                }


            }
            catch
            { }
        }

        public bool isAppLatestVersion_eCard(string _strAppVersion)
        {
            bool _return = false;
            DataTable _dt = _DataAccess.GetAndroidLatestVersion_eCard();
            int VersionFromClient = Convert.ToInt32(_strAppVersion);
            int LatestVersion = 0;
            if (_dt.Rows.Count > 0)
            {
                LatestVersion = Convert.ToInt32(_dt.Rows[0]["Version"].ToString());

                if (VersionFromClient < LatestVersion)
                {
                    _return = false;
                }
                else
                {
                    _return = true;
                }

            }
            return _return;
        }

        public string RequestForgotPwd_eCard(string _strUserID)
        {
            string _strReturn = string.Empty;
            if (_DataAccess.ValidateEmaillAdd(_strUserID.Trim(), "OLD"))
            {
                bool _success = false;
                string _strNewPwd = CreateRandomPassword(9);
                Crypto _Crypto = new Crypto();
                sModUser _modUser = new sModUser();
                _modUser.UserID = _strUserID.Trim();
                _modUser.LoginPwdNew = _Crypto.Encrypt(_strNewPwd);
                _modUser.ReqType = "ChangePwdOnly";
                _success = _DataAccess.UpateUserDetails(_modUser);
                string Fullname = _DataAccess.GetPrincipalFullname(_strUserID.Trim());

                if (_success == true)
                {
                    SendEmailForgotPwd(Fullname, _strNewPwd, _strUserID);
                    _strReturn = "Success|We send confirmation to your email.";
                }
            }
            else
            {
                _strReturn = "FAILED|User ID does not exist.";
            }

            return _strReturn;


        }


        private void SendEmailForgotPwd(string FullName, string Pswd, string _strUserID)
        {
            sModTransaction _ModTran = new sModTransaction()
            {
                header = HttpContext.Current.Server.MapPath(@"~\img\Email\Header.jpg"),
                footer = HttpContext.Current.Server.MapPath(@"~\img\Email\Footer.jpg")
            };

            SendEmailChangePassword(_ModTran, FullName, _strUserID.Trim(), Pswd);

        }

        public bool SendEmailChangePassword(sModTransaction _ModTransaction, string Fullname, string username, string Password)
        {

            bool _success = true;
            try
            {
                Crypto _Crypto = new Crypto();

                List<string> msgTo = new List<string>();
                List<string> msgBCC = new List<string>();
                List<string> msgCC = new List<string>();
                string msgBody = string.Empty;
                List<string> msgAttachment = new List<string>();
                string msgSubject = string.Empty;
                var inlineLogoHeader = new LinkedResource(_ModTransaction.header);
                inlineLogoHeader.ContentId = Guid.NewGuid().ToString();
                var inlineLogoFooter = new LinkedResource(_ModTransaction.footer);
                inlineLogoFooter.ContentId = Guid.NewGuid().ToString();
                string link = string.Format(@_strUri + "://" + _strDentaLINKLeasedLink + "/Login.aspx?x=H4sht4g" + _Crypto.Encrypt(Password)) + "H4sht4g" + _Crypto.Encrypt(username);
                StringBuilder sb = new StringBuilder();
                msgTo.Add(username);
                string[] arrEmailBcc = _strCreateCredentialBcc.Split(';');
                foreach (string str in arrEmailBcc)
                { msgBCC.Add(str); }
                msgSubject = "DóntiaCare Reset Password";
                sb.Append("<style>table,th,td {border : 1px solid black; border-collapse: collapse;}th, td { padding: 5px; text-align: left; font-size : 10pt} p{font-size : 10pt}</style>");
                sb.Append("<img style='width:100%' src= cid:" + inlineLogoHeader.ContentId + " />");
                //sb.Append("<h4></h4>");
                sb.Append("<p>Hi " + Fullname + ",</p>");
                sb.Append("<p>The password for your DontiaLink account, " + username + ", was recently changed. Please click <a href=" + link + ">here</a> to login to your account.</p>");
                sb.Append("<p>If the link does not work, please copy and paste this below link on your web browser</p>");
                sb.Append("<p><span style='color:#0000EE'><u>" + link + "</u></span></p><br/>");
                sb.Append("<p>If you made this change, you’re all set.</p>");
                sb.Append("<p>If not, please take these steps to secure your account: <br/> 1. Reset your password on any sign-in screen by selecting the ‘Forgot Password’ link. <br/>2. Feel free to reach out to Customer Care for questions or concerns.</p><br/>");
                sb.Append("<p>Thanks,<br/> DóntiaCare</p>");
                sb.Append("<p style='font-size: 10pt; font-family: Tahoma;'>Replies sent to this email address cannot be answered. For additional help, please contact us at <span style='color:#FE9A2E'>+65 6737 8088</span> or <span style='color:#FE9A2E'><u>enquiry@dontiacare.com</u></span></p>");
                sb.Append("<img style='width:100%' src=cid:" + inlineLogoFooter.ContentId + " />");
                msgBody = sb.ToString();
                var view = AlternateView.CreateAlternateViewFromString(msgBody, null, "text/html");
                view.LinkedResources.Add(inlineLogoHeader);
                view.LinkedResources.Add(inlineLogoFooter);
                _success = _SendMail(msgTo, msgBCC, msgCC, msgBody, msgAttachment, msgSubject, view);


                return _success;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        internal void UpdatePDPAPolicy(string UserName, string Ver)
        {
            _DataAccess.UpdatePDPAPolicy(UserName, Ver);
        }

        public bool UpdateClaimRead(string logNo)
        {
            return _DataAccess.UpdateClaimRead(logNo);
        }
    }
}