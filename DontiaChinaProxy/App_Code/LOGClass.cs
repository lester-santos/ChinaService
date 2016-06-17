using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using iTextPdfQrCode = iTextSharp.text.pdf.qrcode;
using iTextPdfCodec = iTextSharp.text.pdf.codec;
using iText = iTextSharp.text;
using iTextPdf = iTextSharp.text.pdf;
using iTextSharp.text.pdf;
using System.Drawing;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using System.Runtime.Serialization;
using iTextSharp.text;
using System.Configuration;

namespace DontiaChinaProxy.App_Code.Models
{

    public class LOGClass
    {

        private DatabaseHandler _dbHandler;
        public LOGClass(DatabaseHandler _db)
        {
            _dbHandler = _db;
        }

        public LOGClass()
        {
            // TODO: Complete member initialization
        }

        public ModPdfReport CreateLOGPDFReport(string _RequestNo, List<byte[]> _images)
        {
            MemoryStream _stream = new MemoryStream();
            ModPdfReport _pdfreport = null;

            #region Get Data for LOG Report

            DataSet _report = null;
            DatabaseHandler.Parameters i1 = new DatabaseHandler.Parameters("@RequestNo", _RequestNo);

            DatabaseHandler.ParameterCollection ic = new DatabaseHandler.ParameterCollection();
            ic.Add(i1);
            _report = new DataSet();
            _report = _dbHandler.GetDataSet("sp_GetLOGInformation", ic);

            _report.DataSetName = "LOGINFORMATION";
            _report.Tables[0].TableName = "LOGInfo";
            #endregion

            #region Create and open document

            //Start: Create Pdf
            //Paper Size        
            iText.Document Document = new iText.Document(iText.PageSize.A4, 0, 0, 5, 5);

            //Create Pdf Instance
            iTextPdf.PdfWriter _pdf = default(iTextPdf.PdfWriter);
            _pdf = iTextPdf.PdfWriter.GetInstance(Document, _stream);

            //Open pdf document
            Document.Open();

            //Fonts
            iText.Font HeaderFont = iText.FontFactory.GetFont(iText.FontFactory.TIMES_ROMAN, 15, iText.Font.BOLDITALIC);
            iText.Font dataFontBold = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 7f);
            iText.Font dataFontNormal = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 7f);
            iText.Font dataFontNormalUnderline = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 8f, iText.Font.UNDERLINE);
            iText.Font TitleFont = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 13f);
            iText.Font dataFontAssessedamt = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 11f);
            iText.Font HeaderdataFont = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 7f, iText.BaseColor.WHITE);
            iText.Font dataFontDocumentNo = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 5f);
            #endregion

            #region Report Paper Header
            iText.Image imgDontia = iText.Image.GetInstance(_images.ElementAt(0).ToArray());
            imgDontia.ScaleToFit(200, 350);
            iText.Phrase _ContentHeader = new iText.Phrase();
            List<iText.Chunk> _HeaderChunk = new List<iText.Chunk>();
            _HeaderChunk.Add(new iText.Chunk("Letter of Guarantee" + Environment.NewLine, HeaderFont));
            _ContentHeader.AddRange(_HeaderChunk);
            char[] _sep = { '-' };
            string _qrCodeText = _report.Tables["LOGInfo"].Rows[0]["MemberId"].ToString() + ":" +
                                 _report.Tables["LOGInfo"].Rows[0]["ServiceProvider"].ToString() + ":" +
                                 _report.Tables["LOGInfo"].Rows[0]["TreatmentCode"].ToString() + ":" +
                                 _report.Tables["LOGInfo"].Rows[0]["AvailmentDate"].ToString() + ":" +
                                 _RequestNo;

            BarcodeQRCode _qrcode = new BarcodeQRCode(_qrCodeText, 123, 123, null);
            iText.Image imgQrCode = _qrcode.GetImage();
            imgQrCode.Border = 0;
            imgQrCode.ScaleToFit(90f, 90f);

            iTextPdf.PdfPCell _headerImage = new iTextPdf.PdfPCell(imgDontia);
            _headerImage.Border = 0;
            _headerImage.PaddingTop = 5f;

            iTextPdf.PdfPCell _headerCompanyInfo = new iTextPdf.PdfPCell(_ContentHeader);
            _headerCompanyInfo.Border = 0;
            _headerCompanyInfo.HorizontalAlignment = iTextPdf.PdfPCell.ALIGN_CENTER;
            _headerCompanyInfo.SetLeading(0.0f, 1.2f);

            iTextPdf.PdfPCell _headerQrCode = null;

            _headerQrCode = new iTextPdf.PdfPCell(imgQrCode);
            _headerQrCode.Border = 0;
            _headerQrCode.HorizontalAlignment = iTextPdf.PdfPCell.ALIGN_LEFT;
            _headerQrCode.PaddingLeft = -5;

            //Create Pdf Table
            iTextPdf.PdfPTable _Header = new iTextPdf.PdfPTable(3);
            //_Header.WidthPercentage = 110f;
            float[] HeadercolWidthPercentages = { 20, 60, 20 };
            //Add cells to table
            _Header.SetWidths(HeadercolWidthPercentages);
            _Header.AddCell(_headerImage);
            _Header.AddCell(_headerCompanyInfo);
            _Header.AddCell(_headerQrCode);
            _Header.WidthPercentage = 90f;

            //Add header to pdf document
            Document.Add(_Header);
            #endregion

            #region Title


            iText.Phrase _ContentUpperTitle = new iText.Phrase();

            iTextPdf.PdfPCell _UpperTitleLeft = new iTextPdf.PdfPCell();
            _UpperTitleLeft.Border = 0;
            //Title
            iTextPdf.PdfPCell _UpperTitle = new iTextPdf.PdfPCell(_ContentUpperTitle);
            _UpperTitle.Border = 0;
            _UpperTitle.HorizontalAlignment = iTextPdf.PdfPCell.ALIGN_CENTER;
            #endregion

            #region Member Information

            //string PatientName = _report.Tables[0].Rows[0]["FULLNAME"].ToString() + "   -    " + _report.Tables[0].Rows[0]["CardNo"].ToString();
            //string Contactinfo = _report.Tables[0].Rows[0]["MobileNo"].ToString() + "   -    " + _report.Tables[0].Rows[0]["Email"].ToString();
            //string Company = _report.Tables[0].Rows[0]["CORPNAME"].ToString();
            //string AttendingDoctor = _report.Tables[0].Rows[0]["PHYSICIAN"].ToString();
            //string ProviderCode = _report.Tables[0].Rows[0]["PROVIDERCODE"].ToString();
            //string ProviderName = _report.Tables[0].Rows[0]["PROVIDERNAME"].ToString();
            //string ReferringDoctor = "";
            //string Gender = _report.Tables[0].Rows[0]["GENDER"].ToString();
            //string Age = _report.Tables[0].Rows[0]["AGE"].ToString();
            //string Plan = _report.Tables[0].Rows[0]["PLANCODE"].ToString();
            //string PolicyNo = _report.Tables[0].Rows[0]["POLICYNO"].ToString();
            //string EffectiveDate = _report.Tables[0].Rows[0]["EFFECTIVEDATE"].ToString();
            //string ExpiryDate = _report.Tables[0].Rows[0]["EXPIRYDATE"].ToString();
            //string DateofAvailment = _report.Tables[0].Rows[0]["ADMISSIONDATE"].ToString();


            iTextPdf.PdfPTable tblBody = new iTextPdf.PdfPTable(1);
            iTextPdf.PdfPTable tblMaxicareMemberHeader = new iTextPdf.PdfPTable(2);
            iText.Phrase HeaderCaption1 = new iText.Phrase("", HeaderdataFont);
            iTextPdf.PdfPCell CellHeader1 = new iTextPdf.PdfPCell(HeaderCaption1);

            CellHeader1.Colspan = 2;
            CellHeader1.HorizontalAlignment = 1;
            CellHeader1.BackgroundColor = new iTextSharp.text.BaseColor(7, 112, 189);

            tblBody.AddCell(CellHeader1);

            iTextPdf.PdfContentByte pdfContentByte = _pdf.DirectContent;

            pdfContentByte.SetColorFill(new iTextPdf.CMYKColor(0f, 0f, 0f, 1f));




            List<iText.Chunk> _chnkLeftMemberInformation = new List<iText.Chunk>();

            _chnkLeftMemberInformation.Add(new iText.Chunk("LOG No.: ", dataFontBold));
            _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine, dataFontNormal));
            pdfContentByte.Rectangle(78f, Document.PageSize.Height - 182f, 313f, .3f);
            pdfContentByte.Stroke();


            _chnkLeftMemberInformation.Add(new iText.Chunk("Name of Patient: ", dataFontBold));
            _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine, dataFontNormal));
            pdfContentByte.Rectangle(91f, Document.PageSize.Height - 171f, 300f, .3f);
            pdfContentByte.Stroke();


            _chnkLeftMemberInformation.Add(new iText.Chunk("Service Provider: ", dataFontBold));
            _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine, dataFontNormal));
            pdfContentByte.Rectangle(104f, Document.PageSize.Height - 216f, 287f, .3f);
            pdfContentByte.Stroke();



            _chnkLeftMemberInformation.Add(new iText.Chunk("TreatMent(s): ", dataFontBold));
            _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine, dataFontNormal));
            pdfContentByte.Rectangle(93f, Document.PageSize.Height - 227f, 298f, .3f);
            pdfContentByte.Stroke();


            _chnkLeftMemberInformation.Add(new iText.Chunk("Date of Availment: ", dataFontBold));
            _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine, dataFontNormal));
            pdfContentByte.Rectangle(470f, Document.PageSize.Height - 238f, 309f, .3f);
            pdfContentByte.Stroke();

            iText.Phrase _phLeftMemberInformation = new iText.Phrase();
            _phLeftMemberInformation.AddRange(_chnkLeftMemberInformation);
            iTextPdf.PdfPCell _LeftMemberInfo = new iTextPdf.PdfPCell(_phLeftMemberInformation);
            _LeftMemberInfo.Border = 0;
            _LeftMemberInfo.SetLeading(0.0f, 1.6f);


            iText.Phrase _phRightMemberInformation = new iText.Phrase();


            iTextPdf.PdfPCell _RightMemberInfo = new iTextPdf.PdfPCell(_phRightMemberInformation);
            _RightMemberInfo.Border = 0;
            _RightMemberInfo.PaddingBottom = 5f;
            _RightMemberInfo.SetLeading(0.0f, 1.6f);

            float[] WidthPercentageMaxicareMemberInformation = { 70f, 30f };
            tblMaxicareMemberHeader.SetWidths(WidthPercentageMaxicareMemberInformation);

            tblMaxicareMemberHeader.AddCell(_LeftMemberInfo);
            tblMaxicareMemberHeader.AddCell(_RightMemberInfo);

            tblBody.AddCell(tblMaxicareMemberHeader);

            #endregion

            #region Instructions
            iText.Phrase _InstructionHeader = new iText.Phrase("", HeaderdataFont);
            iTextPdf.PdfPCell _CellHeader2 = new iTextPdf.PdfPCell(_InstructionHeader);
            _CellHeader2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            _CellHeader2.BackgroundColor = new iTextSharp.text.BaseColor(7, 112, 189);

            tblBody.AddCell(_CellHeader2);


            tblBody.WidthPercentage = 90f;
            Document.Add(tblBody);
            #endregion


            Document.Close();
            Document.Dispose();
            _pdfreport = new ModPdfReport()
            {
                FileByteStream = _stream,
                PdfFileName = _fileName
            };

            return _pdfreport;
        }
        public class ModPdfReport
        {
            [DataMember(Name = "FileByteStream", Order = 2)]
            public MemoryStream FileByteStream { get; set; }

            [DataMember(Name = "PdfFileName", Order = 3)]
            public string PdfFileName { get; set; }

            [DataMember(Name = "IsSuccess", Order = 4)]
            public string IsSuccess { get; set; }
        }




        public class DatabaseHandler
        {
            private string Conn_string;
            private string _error;

            public DatabaseHandler(string connection_string)
            {
                Conn_string = connection_string;
            }

            public class Parameters
            {
                private string _parameter_name;
                private object _parameter_value;
                private ParameterDirection _parameter_direction;

                public Parameters()
                {

                }

                public Parameters(string parameterName, object parameterValue)
                {
                    this._parameter_name = parameterName;
                    this._parameter_value = parameterValue;
                }

                public Parameters(string parameterName, object parameterValue, ParameterDirection _direction)
                {
                    this._parameter_name = parameterName;
                    this._parameter_value = parameterValue;
                    this._parameter_direction = _direction;
                }

                public string ParameterName
                {
                    get { return _parameter_name; }
                    set { _parameter_name = value; }
                }

                public object ParameterValue
                {
                    get { return _parameter_value; }
                    set { _parameter_value = value; }
                }

                public ParameterDirection ParameterDirection
                {
                    get { return _parameter_direction; }
                    set { _parameter_direction = value; }
                }
            }

            public class ParameterCollection : CollectionBase
            {
                public void Add(Parameters param)
                {
                    List.Add(param);
                }

                public void Remove(int index)
                {
                    if (index > Count - 1 || index < 0)
                    {

                    }
                    else
                    {
                        List.RemoveAt(index);
                    }
                }

                public Parameters Item(int index)
                {
                    return (Parameters)List[index];
                }
            }

            public int ExecuteNonQuery(string name, CommandType cType)
            {
                int rowsAffected = 0;

                try
                {
                    SqlConnection _conn = new SqlConnection(Conn_string);
                    SqlCommand cmd = new SqlCommand(name, _conn);
                    cmd.CommandType = cType;
                    cmd.CommandTimeout = 10000;

                    if (_conn.State == ConnectionState.Closed) _conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Dispose(); _conn.Dispose(); _conn.Close();
                }
                catch (SqlException ex)
                {
                    _error = ex.Number.ToString() + ": " + ex.Message + "\nStack Trace:\n" + ex.StackTrace;
                    return -1;
                }

                return rowsAffected;
            }

            public int ExecuteNonQuery(string spName, ParameterCollection spParameters)
            {
                int rowsAffected = 0;

                try
                {
                    SqlConnection _conn = new SqlConnection(Conn_string);
                    SqlCommand cmd = new SqlCommand(spName, _conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 20000;

                    foreach (Parameters param in spParameters)
                    {
                        if (param.ParameterDirection == ParameterDirection.Output)
                        {
                            cmd.Parameters.AddWithValue(param.ParameterName, param.ParameterValue).Direction = ParameterDirection.Output;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(param.ParameterName, param.ParameterValue);
                        }

                    }
                    if (_conn.State == ConnectionState.Closed) _conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    _conn.Close();
                }
                catch (SqlException ex)
                {
                    _error = ex.Number.ToString() + ": " + ex.Message + "\nStack Trace:\n" + ex.StackTrace;
                    return -1;
                }

                return rowsAffected;
            }

            public object ExecuteScalar(string name, CommandType cType)
            {
                object returnObject;

                try
                {
                    SqlConnection _conn = new SqlConnection(Conn_string);
                    SqlCommand cmd = new SqlCommand(name, _conn);
                    cmd.CommandType = cType;

                    if (_conn.State == ConnectionState.Closed) _conn.Open();
                    returnObject = cmd.ExecuteScalar();
                    cmd.Dispose();
                    _conn.Dispose();
                    _conn.Close();
                }
                catch (SqlException ex)
                {
                    _error = ex.Number.ToString() + ": " + ex.Message + "\nStack Trace:\n" + ex.StackTrace;
                    return null;
                }

                return returnObject;
            }

            public object ExecuteScalar(string spName, ParameterCollection spParameters)
            {
                object returnObject;

                try
                {
                    SqlConnection _conn = new SqlConnection(Conn_string);
                    SqlCommand cmd = new SqlCommand(spName, _conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (Parameters param in spParameters)
                    {
                        cmd.Parameters.AddWithValue(param.ParameterName, param.ParameterValue);
                    }
                    if (_conn.State == ConnectionState.Closed) _conn.Open();
                    returnObject = cmd.ExecuteScalar();
                    _conn.Close();
                }
                catch (SqlException ex)
                {
                    _error = ex.Number.ToString() + ": " + ex.Message + "\nStack Trace:\n" + ex.StackTrace;
                    return null;
                }

                return returnObject;
            }

            public object ExecuteScalar(StoredProcedure sp, ParameterCollection spParameters)
            {
                object returnObject;

                try
                {
                    SqlConnection _conn = new SqlConnection(Conn_string);
                    SqlCommand cmd = new SqlCommand(sp.SPName, _conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (Parameters param in spParameters)
                    {
                        cmd.Parameters.AddWithValue(param.ParameterName, param.ParameterValue);
                    }
                    if (_conn.State == ConnectionState.Closed) _conn.Open();
                    returnObject = cmd.ExecuteScalar();
                    _conn.Close();
                }
                catch (SqlException ex)
                {
                    _error = ex.Number.ToString() + ": " + ex.Message + "\nStack Trace:\n" + ex.StackTrace;
                    return null;
                }

                return returnObject;
            }

            public DataTable GetDataTable(string name, CommandType cType)
            {
                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    SqlConnection _conn = new SqlConnection(Conn_string);
                    SqlCommand cmd = new SqlCommand(name, _conn);
                    cmd.CommandType = cType;
                    cmd.CommandTimeout = 10000;

                    if (_conn.State == ConnectionState.Closed) _conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    ds.DataSetName = "ResultDataSet";
                    da.Fill(ds);
                    ds.Tables[0].TableName = "Result";
                    dt = ds.Tables[0];

                    cmd.Dispose();
                    _conn.Dispose();
                    _conn.Close();
                }
                catch (SqlException ex)
                {
                    _error = ex.Number.ToString() + ": " + ex.Message + "\nStack Trace:\n" + ex.StackTrace;
                    return dt;
                }

                return dt;
            }

            public DataTable GetDataTable(string sp_name, ParameterCollection parameter)
            {
                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    SqlConnection _conn = new SqlConnection(Conn_string);
                    SqlCommand cmd = new SqlCommand(sp_name, _conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2000;

                    foreach (Parameters p in parameter)
                    {
                        cmd.Parameters.AddWithValue(p.ParameterName, p.ParameterValue);
                    }
                    if (_conn.State == ConnectionState.Closed) _conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    ds.DataSetName = "ResultDataSet";
                    da.Fill(ds);
                    ds.Tables[0].TableName = "Result";
                    dt = ds.Tables[0];
                    cmd.Dispose();
                    _conn.Dispose();
                    _conn.Close();
                }
                catch (SqlException ex)
                {
                    _error = ex.Number.ToString() + ": " + ex.Message + "\nStack Trace:\n" + ex.StackTrace;
                    return dt;
                }

                return dt;
            }

            public DataSet GetDataSet(string name, CommandType cType)
            {
                DataSet ds = null;
                try
                {
                    SqlConnection _conn = new SqlConnection(Conn_string);
                    SqlCommand cmd = new SqlCommand(name, _conn);
                    cmd.CommandType = cType;
                    cmd.CommandTimeout = 10000;
                    if (_conn.State == ConnectionState.Closed) _conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    cmd.Dispose(); _conn.Dispose(); _conn.Close();
                }
                catch (SqlException ex)
                {
                    _error = ex.Number.ToString() + ": " + ex.Message + "\nStack Trace:\n" + ex.StackTrace;
                    return ds;
                }

                return ds;
            }

            public DataSet GetDataSet(string spName, ParameterCollection spParameters)
            {
                DataSet ds = null;

                try
                {
                    SqlConnection dbConnect = new SqlConnection();
                    dbConnect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConDentaLinkSGDB"].ConnectionString);
                    dbConnect.Open();
                    SqlCommand cmd = new SqlCommand(spName, dbConnect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (Parameters param in spParameters)
                    {
                        cmd.Parameters.AddWithValue("@RequestNo", param.ParameterValue);
                    }
                    if (dbConnect.State == ConnectionState.Closed)
                    { dbConnect.Open(); }
                    SqlDataAdapter _adapter = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    _adapter.Fill(ds);

                    cmd.Dispose();
                    dbConnect.Dispose();
                    dbConnect.Close();
                }
                catch (SqlException ex)
                {
                    _error = ex.Number.ToString() + ": " + ex.Message + "\nStack Trace:\n" + ex.StackTrace;
                    return ds;
                }

                return ds;
            }

            public class StoredProcedure
            {
                public string SPName { get; set; }
                public StoredProcedure(string name)
                {
                    if (!Regex.IsMatch(name, @"^[a-zA-Z0-9_]+$")) throw new Exception("Invalid stored procedure name.");
                    this.SPName = name;
                }
            }
        }



        public byte[] PrintLOG()
        {
            DatabaseHandler _db = new DatabaseHandler(constring);
            LOGClass _method = new LOGClass(_db);
            byte[] loaReport;
            loaReport = _method.PrintLOG1();
            return loaReport;
        }

        private byte[] PrintLOG1()
        {
            byte[] data = null;
            ModPdfReport _report = new ModPdfReport();
            _report = CreateLOGPDFReport(RequestNo, LOGImage.ConvertImageToByte());
            data = _report.FileByteStream.ToArray();
            return data;
        }



        public string _fileName { get; set; }
        public string RequestNo { get; set; }
        public string constring { get; set; }
    }

}






