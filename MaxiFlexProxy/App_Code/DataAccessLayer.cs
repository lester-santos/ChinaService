using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MaxiFlexProxy.App_Code.Models;
using System.ServiceModel;

namespace MaxiFlexProxy.App_Code
{
    public class DataAccessLayer : LibCon
    {
        private static ServiceData myServiceData;


        public DataAccessLayer()
        {
            initServiceData();
        }

        private void initServiceData()
        {
            if (myServiceData == null)
                myServiceData = new ServiceData();
        }

        //AutoComplete Treatments
        public DataTable AutoComplete(string autocomplete)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spTreatmentAutoComplete", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = autocomplete;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }


        public DataTable AutoCompleteTreatmentClinic(string prefix)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spTreatmentAutoCompleteTreatmentClinic", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = prefix;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }

        public DataTable AutoCompleteTreatmentDentist(string prefix)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spTreatmentAutoCompleteTreatmentDentist", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = prefix;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }

        public DataTable AutoCompleteTreatment(string prefixText, string contextKey)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("sp_GetLGTreatment", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Treatment", SqlDbType.VarChar).Value = prefixText;
                _cmd.Parameters.Add("@ServiceProvider", SqlDbType.VarChar).Value = contextKey;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);

                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }

        public DataTable ACClaimTreatment(string prefixText, string UserID, string UserType)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spACClaimTreatment", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Treatment", SqlDbType.VarChar).Value = prefixText;
                _cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;
                _cmd.Parameters.Add("@UserType", SqlDbType.VarChar).Value = UserType;
                //_cmd.Parameters.Add("@ServiceProvider", SqlDbType.VarChar).Value = contextKey;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);

                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }


        public DataTable ucBillingCorporation(string prefix)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spBillingCorporationAutocomplete", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@search", SqlDbType.VarChar).Value = prefix;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }

        public DataTable AutoCompletePlanBenefit(string prefix)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spTreatmentAutoCompletePlanBenefit", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = prefix;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }

        public DataTable AutoCompleteMainCorp(string prefix)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("_spGetMainCorp_AC", _DentalConOpen());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MainCorp", SqlDbType.VarChar).Value = prefix;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                adapter.Dispose();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return dt;
        }

        public DataTable AutoCompletePlanID(string prefix)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("_spGetPlanCode_AC", _DentalConOpen());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PlanID", SqlDbType.VarChar).Value = prefix;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                adapter.Dispose();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return dt;
        }

        public DataTable ReportBillingData(string _corpCode, string _LogNo)
        {
            DataTable ds = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("_spReportBillingData", _DentalConOpen());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CorpCode", SqlDbType.VarChar).Value = _corpCode;
                cmd.Parameters.Add("@LOGNo", SqlDbType.VarChar).Value = _LogNo;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
                adapter.Dispose();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return ds;
        }

        public DataTable GetSGPostal(string _strPostal)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("_spGetSGPostal_AC", _DentalConOpen());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Postal", SqlDbType.VarChar).Value = _strPostal;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(_dt);
                adapter.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {  throw(ex);  }
            finally { _DentalConClose(); }

            return _dt;
        }

        public DataTable GetCorporation_AC(string _strCorp)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetCorporation_AC", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Corp", SqlDbType.VarChar).Value = _strCorp;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            { _DentalConClose(); }
            return _dt;
        }

        //Daryl
        public DataTable GetPatient_AC(string _strPatient, string contextKey, string corpCode)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("sp_GetPatient", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Patient", SqlDbType.VarChar).Value = _strPatient;
                _cmd.Parameters.Add("@MainMemberID", SqlDbType.VarChar).Value = contextKey;
                _cmd.Parameters.Add("@CorpCode", SqlDbType.VarChar).Value = corpCode;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            { _DentalConClose(); }
            return _dt;
        }

        public DataTable GetClinic_AC(string _strClinic)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetClinic_AC", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Clinic", SqlDbType.VarChar).Value = _strClinic;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            { _DentalConClose(); }
            return _dt;
        }

        public DataTable ucLGGetServiceProvider(string prefixText)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("sp_GetServiceProvider", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@ClinicName", SqlDbType.VarChar).Value = prefixText;
              //  _cmd.Parameters.Add("@Treatment", SqlDbType.VarChar).Value = prefixText;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);

                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }

        public DataTable ucGetHREmailAddress(string prefixText)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetAllHRsEmailAddress", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@EAdd", SqlDbType.VarChar).Value = prefixText;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);

                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }

        public DataTable ValidateCredentials(string UserId)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spValidateLoginCredentials", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserId;
                //_cmd.Parameters.Add("@LoginPwd", SqlDbType.VarChar).Value = LoginPwd;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);

                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }

        public DataTable ReferralAdd(string RequestNo, string PatientName, string ServiceProvider, string AvailmentDate)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("sp_AddReferral", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@RequestNo", SqlDbType.VarChar).Value = RequestNo;
                _cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = PatientName;
                _cmd.Parameters.Add("@ServiceProvider", SqlDbType.VarChar).Value = ServiceProvider;           
                _cmd.Parameters.Add("@AvailmentDate", SqlDbType.VarChar).Value = AvailmentDate;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();

            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }
        public DataTable SequenceAdd()
        {

            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("sp_AddSequence", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
            }
            return _dt;
        }

        // == eCard

        public string GetSequenceLOGReq()
        {
            string _strReturn = string.Empty;
            try
            {
                SqlCommand _cmd = new SqlCommand("_spUpdateSequence", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@SeqCode", SqlDbType.VarChar).Value = "LogReq";
                _strReturn = Convert.ToString((int)_cmd.ExecuteScalar());

            }
            catch (Exception)
            { throw; }
            finally { _DentalConClose(); }
            return _strReturn;
        }

        public bool RequestVerificationCode_eCard(string MemberID, string ClinicCode, string ContactNo, string Fullname, string PreferredDateFrom, string PreferredDateTo)
        {
            try
            {
                SqlCommand _cmd = new SqlCommand("InsertAppointment", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@MemberID", SqlDbType.VarChar).Value = MemberID;
                _cmd.Parameters.Add("@ClinicCode", SqlDbType.VarChar).Value = ClinicCode;
                _cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = ContactNo;
                _cmd.Parameters.Add("@PreferredDateFrom", SqlDbType.Date).Value = Convert.ToDateTime(PreferredDateFrom);
                _cmd.Parameters.Add("@PreferredDateTo", SqlDbType.Date).Value = Convert.ToDateTime(PreferredDateTo);
                _cmd.Parameters.Add("@FULLNAME", SqlDbType.VarChar).Value = Fullname;
                _cmd.ExecuteNonQuery();
                _cmd.Dispose();
                return true;
            }
            catch (Exception)
            { return false; }
            finally
            {
                _DentalConClose();
            }
        }

        public bool UpdateVerificationCode_eCard(string MemberID, string ClinicCode, string ContactNo, string Fullname, string PreferredDateFrom)
        {
            try
            {
                SqlCommand _cmd = new SqlCommand("UpdateAppointment", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@MemberID", SqlDbType.VarChar).Value = MemberID;
                _cmd.Parameters.Add("@ClinicCode", SqlDbType.VarChar).Value = ClinicCode;
                _cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = ContactNo;
                _cmd.Parameters.Add("@PreferredDateFrom", SqlDbType.Date).Value = Convert.ToDateTime(PreferredDateFrom);
                _cmd.Parameters.Add("@FULLNAME", SqlDbType.VarChar).Value = Fullname;
                _cmd.Parameters.Add("@AppType", SqlDbType.VarChar).Value = "U";
                _cmd.ExecuteNonQuery();
                _cmd.Dispose();
                return true;
            }
            catch (Exception)
            { return false; }
            finally
            {
                _DentalConClose();
            }
        }



        public DataTable GetPatientName(string prefixText, string _strMainMemberID, string _strCorpCode)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetPatientName_eCard", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Patient", SqlDbType.VarChar).Value = prefixText;
                _cmd.Parameters.Add("@MainMemberID", SqlDbType.VarChar).Value = _strMainMemberID;
                _cmd.Parameters.Add("@CorpCode", SqlDbType.VarChar).Value = _strCorpCode;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception ex)
            {
                myServiceData.ErrorMessage = ex.Message.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }

        public DataTable SearchLOG(string _strMainMemberID)
        {
            DataTable _dt = new DataTable("dtLOG");
            try
            {
                SqlCommand _cmd = new SqlCommand("_spSearchLOG_eCard", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@MainMemberID", SqlDbType.VarChar).Value = _strMainMemberID.Trim();
                //_cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = _strPatientName.Trim();
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception ex)
            {
                myServiceData.ErrorMessage = ex.Message.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }

        public List<sModUser> Login(string _strUserID)
        {
            List<sModUser> _listUsers = new List<sModUser>();
            sModUser _modUser = new sModUser();
            DataTable _dt = new DataTable();

            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetUser", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = _strUserID.Trim();
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();

                foreach (DataRow _row in _dt.Rows)
                {
                    _modUser = new sModUser();
                    _modUser.FirstName = _row["FirstName"].ToString();
                    _modUser.LastName = _row["LastName"].ToString();
                    _modUser.UserID = _row["UserName"].ToString();
                    _modUser.LoginPwd = _row["Password"].ToString();
                    _modUser.WrongPasswordCnt = Convert.ToInt32(_row["WrongPasswordCnt"].ToString());
                    _modUser.Active = _row["IsActive"].ToString();
                    _modUser.UserType = _row["UserType"].ToString();
                    _modUser.LastLoginDate = _row["LastLoginDate"].ToString();
                    _modUser.EmployeeNo = _row["EmployeeNo"].ToString();
                    _modUser.CorpCode = _row["CorpCode"].ToString();
                    _modUser.NoOfDaysLastChangePwd = _row["NoOfDaysLastChangePwd"].ToString();
                    _modUser.NoOfDaysLastLogIn = _row["NoOfDaysLastLogIn"].ToString();
                    _modUser.MemberID = _row["MemberID"].ToString();
                    _modUser.CorpName = _row["CorpName"].ToString();
                    _modUser.ClinicCode = _row["ClinicCode"].ToString();
                    _modUser.ClinicName = _row["ClinicName"].ToString();
                    _modUser.DentistCode = _row["DentistCode"].ToString();
                    _modUser.DentistName = _row["DentistName"].ToString();
                    _modUser.UserLevel = _row["UserLevel"].ToString();
                    _modUser.ClinicStatus = _row["IsClinicActive"].ToString();
                    _modUser.ReadPDPAPolicy = _row["ReadPDPAPolicy"].ToString();
                    _modUser.PlanCode = _row["PlanCode"].ToString();
                    _modUser.WithCardDesign = _row["WithCardDesign"].ToString();
                    _modUser.IDNo = _row["NRICNo"].ToString();
                    _modUser.SetSecurityQuestion = _row["SetSecurityQuestion"].ToString();


                    _listUsers.Add(_modUser);
                }
            }
            catch (Exception ex)
            {
                myServiceData.ErrorMessage = ex.Message.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
            finally
            {
                _DentalConClose();
            }
            return _listUsers;
        }


        public bool UpateUserDetails(sModUser _paramModUsers)
        {
            bool _success = false;
            try
            {
                SqlCommand _cmd = new SqlCommand("_spUpateUserDetails", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = _paramModUsers.UserID;
                _cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = _paramModUsers.LoginPwd;
                _cmd.Parameters.Add("@PasswordNew", SqlDbType.VarChar).Value = _paramModUsers.LoginPwdNew;
                _cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = _paramModUsers.Active;
                _cmd.Parameters.Add("@WrongPasswordCnt", SqlDbType.Int).Value = _paramModUsers.WrongPasswordCnt;
                _cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = _paramModUsers.ReqType;
                _cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = _paramModUsers.Remarks;
                _cmd.ExecuteNonQuery();
                _cmd.Dispose();
                _success = true;
            }
            catch (Exception ex)
            {
                myServiceData.ErrorMessage = ex.Message.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
            finally { _DentalConClose(); }
            return _success;
        }



        public string GetMainMemberID(string _strMemberID)
        {

            string _strReturn= string.Empty;
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetMainMemberID", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@MemberID", SqlDbType.VarChar).Value = _strMemberID.Trim();
                _strReturn = (string)_cmd.ExecuteScalar();
                _cmd.Dispose();
            }
            catch (Exception ex)
            {
                myServiceData.ErrorMessage = ex.Message.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
            finally
            {
                _DentalConClose();
            }
            return _strReturn;
        }

        public DataTable GetTreatmentPlan(string autocomplete)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spTreatmentPlanAutoComplete", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = autocomplete;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }

        public DataTable GetClinicCoordinates_eCard(string prefixText)
        {
                    DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetClinicCoordinates", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@PrefixText", SqlDbType.VarChar).Value = prefixText;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception ex)
            {
                myServiceData.ErrorMessage = ex.Message.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        
        }

        public DataTable GetPhysicianProvider_eCard(string ClinicCode)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetPhysicianProvider", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@ClinicCode", SqlDbType.VarChar).Value = ClinicCode.Trim();
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        
        }

        public DataTable GetPhysicianSchedule_eCard(string DentistCode)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetPhysicianSchedule_eCard", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@DentistCode", SqlDbType.VarChar).Value = DentistCode.Trim();
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
            return _dt;

        }

        public string GetMemberExpiryDate_eCard(string _strMemberID)
        {
            string _strRet = string.Empty;
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetMemberExpiryDate", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@MemberID", SqlDbType.VarChar).Value = _strMemberID.Trim();
                _strRet = (string)_cmd.ExecuteScalar();
                _cmd.Dispose();
            }
            catch (Exception ex)
            {
                myServiceData.ErrorMessage = ex.Message.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
            finally
            {
                _DentalConClose();
            }
            return _strRet;
        }


        public DataTable GetAllClinic_eCard()
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetAllClinic_eCard", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception ex)
            {
                myServiceData.ErrorMessage = ex.Message.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
            finally
            {
                _DentalConClose();
            }
            return _dt;

        }

        public bool isUserActive_eCard(string _strUserName)
        {
            bool isActive = false;
            string _strReturn = string.Empty;
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetActiveUser_eCard", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = _strUserName.Trim();
                _strReturn = (string)_cmd.ExecuteScalar();
                _cmd.Dispose();

                if (_strReturn == "ACTIVE")
                {
                    isActive = true;
                }
                else
                {
                    isActive = false;
                }
            }
            catch (Exception ex)
            {
                myServiceData.ErrorMessage = ex.Message.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
            finally
            {
                _DentalConClose();
            }
            return isActive;
        }


        public DataTable GetAndroidLatestVersion_eCard()
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetAndroidAppLatestVersion", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception ex)
            {
                myServiceData.ErrorMessage = ex.Message.ToString();
                throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }

        public string GetPrincipalFullname(string UserID)
        {
            object Fullname = new object();
            try
            {
                SqlCommand _cmd = new SqlCommand("GetPrincipalFullname", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;
                Fullname = _cmd.ExecuteScalar();

                if (string.IsNullOrEmpty(Fullname.ToString()))
                {
                    Fullname = "";
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _DentalConClose();
            }
            return Fullname.ToString();
        }


        public bool ValidateEmaillAdd(string EmailAdd, string Type)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spCheckEmailAdd", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@EmailAdd", SqlDbType.VarChar).Value = EmailAdd;
                _cmd.Parameters.Add("@EmailType", SqlDbType.VarChar).Value = Type;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _DentalConClose();
            }
            bool x = false;
            if (_dt.Rows.Count != 0)
            {
                x = true;
            }

            return x;
        }
        public bool InsertAppointment(string MemberID, string ClinicCode, string ContactNo, string PreferredDateFrom, string PreferredDateTo)
        {
            try
            {
                SqlCommand _cmd = new SqlCommand("InsertAppointment", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@MemberID", SqlDbType.VarChar).Value = MemberID;
                _cmd.Parameters.Add("@ClinicCode", SqlDbType.VarChar).Value = ClinicCode;
                _cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = ContactNo;
                _cmd.Parameters.Add("@PreferredDateFrom", SqlDbType.Date).Value = Convert.ToDateTime(PreferredDateFrom);
                _cmd.Parameters.Add("@PreferredDateTo", SqlDbType.Date).Value = Convert.ToDateTime(PreferredDateTo);
                _cmd.ExecuteNonQuery();
                _cmd.Dispose();
                return true;
            }
            catch (Exception)
            { return false; }
            finally
            {
                _DentalConClose();
            }
        }
        public string GetMembersContactNo(string MemberID)
        {
            object ContactNo = new object();
            try
            {
                SqlCommand _cmd = new SqlCommand("GetMembersContactNo", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@MemberID", SqlDbType.VarChar).Value = MemberID;
                ContactNo = _cmd.ExecuteScalar();

                if (string.IsNullOrEmpty(ContactNo.ToString()))
                {
                    ContactNo = "";
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _DentalConClose();
            }
            return ContactNo.ToString();
        }

        internal void UpdatePDPAPolicy(string UserName, string Ver)
        {
            try
            {
                SqlCommand _cmd = new SqlCommand("_spUpdatePDPAPolicy", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;
                _cmd.Parameters.Add("@Ver", SqlDbType.VarChar).Value = Ver;
                _cmd.ExecuteNonQuery();
                _cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            finally
            {
                _DentalConClose();
            }
        }

        public bool UpdateClaimRead(string logNo)
        {
            try
            {
                SqlCommand _cmd = new SqlCommand("_spUpdateClaimRead", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@LOGNo", SqlDbType.VarChar).Value = logNo;
                _cmd.ExecuteNonQuery();
                _cmd.Dispose();
                return true;
            }
            catch (Exception)
            { return false; }
            finally
            {
                _DentalConClose();
            }
        }
        public DataTable GetMemberDependentsRA(string MemberID)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetMemberDependentsRA_service", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@MainMemberID", SqlDbType.VarChar).Value = MemberID;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(_dt);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _DentalConClose();
            }
            return _dt;
        }
        public DataSet EMR(string MemberID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand _cmd = new SqlCommand("spEMR", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@MemberID", SqlDbType.VarChar).Value = MemberID;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(ds);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _DentalConClose();
            }
            return ds;
        }

        public bool ResetPasswordKeystone(string UserName, string CardNo, out string MemberDetails)
        {
            try
            {
                SqlCommand _cmd = new SqlCommand("_spResetPassword", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;
                _cmd.Parameters.Add("@CardNo", SqlDbType.VarChar).Value = CardNo;
                MemberDetails = (string)_cmd.ExecuteScalar();
                _cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MemberDetails = "";
                return false;
            }
            finally
            {
                _DentalConClose();
            }
        }

        public bool SetSecurityQuestion(string idNo, string questionCode, string answer)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spInsertSecurityQuestion", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@IDNo", SqlDbType.VarChar).Value = idNo;
                _cmd.Parameters.Add("@QuestionCode", SqlDbType.VarChar).Value = questionCode;
                _cmd.Parameters.Add("@Answer", SqlDbType.VarChar).Value = answer;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(dt);
                _adapter.Dispose();
                _cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            { return false; }
            finally { _DentalConClose(); }
        }

        public DataTable GetSecurityQuestion(string IDNo)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("spGetSecurityQuestion", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@IDNo", SqlDbType.VarChar).Value = IDNo;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(dt);
                _adapter.Dispose();
                _cmd.Dispose();

            }
            catch (Exception ex)
            { throw ex; }
            finally { _DentalConClose(); }
            return dt;
        }

        public string ChangeCardNo(string IDNo)
        {
            string MemberDetails = String.Empty;
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand _cmd = new SqlCommand("spChangeCarNoPerMember", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@IDNo", SqlDbType.VarChar).Value = IDNo;
                MemberDetails = (string)_cmd.ExecuteScalar();
                _cmd.Dispose();
            }
            catch (Exception ex)
            {
                return "";
                throw ex;
            }
            finally { _DentalConClose(); }
            return MemberDetails;
        }

        public void ChangePasswordToCardNo(string Password, string CardNo)
        {
            try
            {
                SqlCommand _cmd = new SqlCommand("spChangePasswordToCardNo", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;
                _cmd.Parameters.Add("@CardNo", SqlDbType.VarChar).Value = CardNo;
                _cmd.ExecuteNonQuery();
                _cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { _DentalConClose(); }
        }

        public DataSet GetLOAInformation(string ClaimNo)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand _cmd = new SqlCommand("_spGetLOAInformation", _DentalConOpen());
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@ClaimNo", SqlDbType.VarChar).Value = ClaimNo;
                SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
                _adapter.Fill(ds);
                _adapter.Dispose();
                _cmd.Dispose();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                _DentalConClose();
            }
            return ds;
        }
    }
}