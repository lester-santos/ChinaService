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
using DontiaChinaProxy.App_Code.Models;
using System.ServiceModel;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using OnBarcode.Barcode;
using System.Media;
using DontiaChinaProxy.App_Code;



namespace DontiaChinaProxy
{
    public class LOGClassPdf
    {
        private static ServiceData myServiceData;
        private DatabaseHandler _dbHandler;
        public LOGClassPdf(DatabaseHandler _db)
        {
            _dbHandler = _db;
        }

        public static iTextSharp.text.Font SEGOE_UI(float fontsize, int fontstyle)
        {
            var fontName = "Segoe-UI";
            if (!FontFactory.IsRegistered(fontName))
            {
                var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\segoeui.ttf";
                FontFactory.Register(fontPath);
            }
            return FontFactory.GetFont(fontName, fontsize, fontstyle);
        }

        public LOGClassPdf()
        {
            // TODO: Complete member initialization
            initServiceData();
        }



        private void initServiceData()
        {
            if (myServiceData == null)
                myServiceData = new ServiceData();
        }

        public Bitmap GenerateQR(string code)
        {
            var bw = new ZXing.BarcodeWriter();
            var encOptions = new ZXing.Common.EncodingOptions() { Width = 200, Height = 200, Margin = 0 };
            bw.Options = encOptions;
            bw.Format = ZXing.BarcodeFormat.QR_CODE;
            var result = new Bitmap(bw.Write(code));

            return result;
        }


        public ModPdfReport CreateLOGPDFReport(string _RequestNo, List<byte[]> _images, List<byte[]> _imgDontiaLink, List<byte[]> _imgDontiaFooter)
        {
            ModPdfReport _pdfreport = null;
            try
            {

                MemoryStream _stream = new MemoryStream();

                #region Get Data for LOG Report
                DataSet _report = null;
                DatabaseHandler.Parameters i1 = new DatabaseHandler.Parameters("@RequestNo", _RequestNo);
                DatabaseHandler.ParameterCollection ic = new DatabaseHandler.ParameterCollection();
                ic.Add(i1);
                _report = new DataSet();
                _report = _dbHandler.GetDataSet("sp_GetLOGInformation", ic);
                _report.DataSetName = "LOGINFORMATION";
                _report.Tables[0].TableName = "LOGInfo";
                _report.Tables[1].TableName = "TreatmentInfo";
                _report.Tables[2].TableName = "ContactInfo";
                _report.Tables[3].TableName = "ProviderContactInfo";

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
                //iText.Font SEGOE = iText.FontFactory.GetFont("Segoe-UI", 15f);
                iText.Font HeaderFont = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 25f); //SEGOE_UI(25f, iText.Font.BOLD);
                iText.Font dataFontBold = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 15f); //SEGOE_UI(15f, iText.Font.BOLD); //
                iText.Font dataFontNormal = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 12f); //SEGOE_UI(12f, iText.Font.NORMAL); //
                iText.Font dataFontNormalUnderline = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 8f, iText.Font.UNDERLINE);
                iText.Font TitleFont = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 11f);
                iText.Font dataFontAssessedamt = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 11f);
                iText.Font Footer = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 5f);
                iText.Font dataFooterBold = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 8f); //SEGOE_UI(8f, iText.Font.NORMAL); //iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 5f);
                #endregion

                #region Report Paper Header
                string LOGNo = _report.Tables[0].Rows[0]["LOGNo"].ToString();
                iText.Image imgDontia = iText.Image.GetInstance(_images.ElementAt(0).ToArray());
                imgDontia.ScaleToFit(150, 250);
                imgDontia.SetAbsolutePosition(120, 120);
                iText.Image imgDontiaLink = iText.Image.GetInstance(_imgDontiaLink.ElementAt(0).ToArray());
                imgDontiaLink.ScaleToFit(200, 300);
                imgDontiaLink.SetAbsolutePosition(120, 120);
                iText.Phrase _ContentHeader = new iText.Phrase();
                List<iText.Chunk> _HeaderChunk = new List<iText.Chunk>();
                _HeaderChunk.Add(new iText.Chunk("" + Environment.NewLine, HeaderFont));
                List<iText.Chunk> _HeaderChunk2 = new List<iText.Chunk>();
                _HeaderChunk2.Add(new iText.Chunk("Verification Form" + Environment.NewLine, HeaderFont));
                _HeaderChunk2.Add(new iText.Chunk("" + Environment.NewLine, HeaderFont));
                string serviceprovider = _report.Tables["ContactInfo"].Rows[0]["ServiceProvider"].ToString().TrimEnd();
                _ContentHeader.AddRange(_HeaderChunk);
                char[] _sep = { '-' };
                string _qrCodeText = _report.Tables["LOGInfo"].Rows[0]["LOGNo"].ToString() + "," +
                                     _report.Tables["LOGInfo"].Rows[0]["IssueDate"].ToString() + "," +
                                     serviceprovider + "," +
                                     _RequestNo;

                BarcodeQRCode _qrcode = new BarcodeQRCode(_qrCodeText, 123, 123, null);
                iText.Image imgQrCode = _qrcode.GetImage();
                imgQrCode.Border = 0;
                imgQrCode.ScaleToFit(180f, 180f);

                iTextPdf.PdfPCell _headerImage1 = new iTextPdf.PdfPCell(imgDontia);
                _headerImage1.Border = 0;
                _headerImage1.PaddingTop = 5f;

                iTextPdf.PdfPCell _headerImage2 = new iTextPdf.PdfPCell(imgDontiaLink);
                _headerImage2.Border = 0;
                _headerImage2.PaddingTop = 5f;

                iTextPdf.PdfPCell _headerCompanyInfo = new iTextPdf.PdfPCell(_ContentHeader);
                _headerCompanyInfo.Border = 0;
                _headerCompanyInfo.HorizontalAlignment = iTextPdf.PdfPCell.ALIGN_CENTER;
                _headerCompanyInfo.SetLeading(0.0f, 1.2f);
                iTextPdf.PdfPCell _headerQrCode = null;
                _headerQrCode = new iTextPdf.PdfPCell(imgQrCode);
                _headerQrCode.Border = 0;
                _headerQrCode.Rowspan = 5;
                //_headerQrCode.HorizontalAlignment = iTextPdf.PdfPCell.ALIGN_LEFT;
                _headerQrCode.PaddingLeft = -200;

                //Create Pdf Table
                iTextPdf.PdfPTable _Header1 = new iTextPdf.PdfPTable(3);
                //_Header.WidthPercentage = 110f;
                float[] HeadercolWidthPercentages = { 20, 45, 35 };
                //Add cells to table
                _Header1.SetWidths(HeadercolWidthPercentages);
                _Header1.AddCell(_headerImage1);
                _Header1.AddCell(_headerCompanyInfo);
                _Header1.AddCell(_headerCompanyInfo);
                _Header1.WidthPercentage = 90f;
                Document.Add(_Header1);

                ////Create Pdf Table

                //iTextPdf.PdfPTable _Header2 = new iTextPdf.PdfPTable(3);
                //iTextPdf.PdfPCell _Header2Left = new iTextPdf.PdfPCell(_Blank);
                //iTextPdf.PdfPCell _Header2Right = new iTextPdf.PdfPCell(_Blank);
                //_Header2Left.Border = 0;
                //_Header2Right.Border = 0;
                ////_Header.WidthPercentage = 110f;
                //float[] Header2colWidthPercentages = { 40, 20, 40 };
                ////Add cells to table
                //_Header2.SetWidths(Header2colWidthPercentages);
                //_Header2.AddCell(_Header2Left);
                //_Header2.AddCell(_headerQrCode);
                //_Header2.AddCell(_Header2Right);
                //_Header2.WidthPercentage = 90f;
                //Document.Add(_Header2);

                //Create Pdf Table
                iText.Phrase _Blank = new iText.Phrase();
                _Blank.AddRange(_HeaderChunk);
                iText.Phrase _VerificationCode = new iText.Phrase();
                iTextPdf.PdfPCell _ContentHeader3 = new iTextPdf.PdfPCell(_VerificationCode);
                _VerificationCode.AddRange(_HeaderChunk2);
                iTextPdf.PdfPCell _Header3Left = new iTextPdf.PdfPCell(_Blank);
                iTextPdf.PdfPCell _Header3Right = new iTextPdf.PdfPCell(_Blank);
                _Header3Left.Border = 0;
                _Header3Right.Border = 0;
                _ContentHeader3.Border = 0;
                _ContentHeader3.HorizontalAlignment = iTextPdf.PdfPCell.ALIGN_CENTER;
                float[] Header3colWidthPercentages = { 20, 60, 20 };
                iTextPdf.PdfPTable _Header3 = new iTextPdf.PdfPTable(3);
                _Header3.SetWidths(Header3colWidthPercentages);
                _Header3.AddCell(_Header3Left);
                _Header3.AddCell(_ContentHeader3);
                _Header3.AddCell(_Header3Right);
                _Header3.WidthPercentage = 90f;
                Document.Add(_Header3);


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
                string PatientName = _report.Tables[0].Rows[0]["Patient"].ToString();
                string PatientID = _report.Tables[0].Rows[0]["MemberID"].ToString();
                string Corporation = _report.Tables[0].Rows[0]["CorpName"].ToString();
                string ServiceProvider = _report.Tables[0].Rows[0]["ServiceProviderName"].ToString();
                string MemberID = _report.Tables[0].Rows[0]["MemberID"].ToString();
                string Address = _report.Tables[0].Rows[0]["Address"].ToString();
                string IssueDate = _report.Tables[0].Rows[0]["IssueDate"].ToString();
                string AvailmentDate = _report.Tables[0].Rows[0]["AvailmentDate"].ToString();
                string RequestBy = _report.Tables[0].Rows[0]["RequestBy"].ToString();
                string RequestDate = _report.Tables[0].Rows[0]["RequestDate"].ToString();
                string ContactMobile = "(Mobile)";
                string ContactLandLine = "(Landline)";
                string ContactFax = "(Fax)";
                int m = 0;
                int l = 0;
                int f = 0;
                for (int i = 0; i < _report.Tables[3].Rows.Count; i++)
                {

                    if (_report.Tables[3].Rows[i]["ContactType"].ToString() == "M")
                    {
                        if (m < 3)
                        {
                            if (m == 0)
                            {
                                ContactMobile = ContactMobile + _report.Tables[3].Rows[i]["ProviderContact"].ToString();
                                m++;
                            }
                            else
                            {
                                ContactMobile = ContactMobile + " ," + _report.Tables[3].Rows[i]["ProviderContact"].ToString();
                                m++;
                            }

                        }
                    }
                    if (_report.Tables[3].Rows[i]["ContactType"].ToString() == "L")
                    {
                        if (l < 3)
                        {
                            if (l == 0)
                            {
                                ContactLandLine = ContactLandLine + _report.Tables[3].Rows[i]["ProviderContact"].ToString();
                                l++;
                            }
                            else
                            {
                                ContactLandLine = ContactLandLine + " ," + _report.Tables[3].Rows[i]["ProviderContact"].ToString();
                                l++;
                            }

                        }
                    }
                    if (_report.Tables[3].Rows[i]["ContactType"].ToString() == "F")
                    {
                        if (f < 3)
                        {
                            if (f == 0)
                            {
                                ContactFax = ContactFax + _report.Tables[3].Rows[i]["ProviderContact"].ToString();
                                f++;
                            }
                            else
                            {
                                ContactFax = ContactFax + " ," + _report.Tables[3].Rows[i]["ProviderContact"].ToString();
                                f++;
                            }

                        }
                    }

                }

                iTextPdf.PdfPTable tblBody = new iTextPdf.PdfPTable(1);
                iTextPdf.PdfPTable tblMaxicareMemberHeader = new iTextPdf.PdfPTable(3);
                iText.Phrase HeaderCaption1 = new iText.Phrase("", dataFontNormal);
                iTextPdf.PdfPCell CellHeader1 = new iTextPdf.PdfPCell(HeaderCaption1);
                CellHeader1.Colspan = 4;
                CellHeader1.HorizontalAlignment = 1;
                CellHeader1.BackgroundColor = new iTextSharp.text.BaseColor(7, 112, 189);
                tblBody.AddCell(CellHeader1);
                iTextPdf.PdfContentByte pdfContentByte = _pdf.DirectContent;
                pdfContentByte.SetColorFill(new iTextPdf.CMYKColor(0f, 0f, 0f, 1f));

                List<iText.Chunk> _chnkLeftMemberInformation = new List<iText.Chunk>();
                List<iText.Chunk> _chnkCenterMemberInformation = new List<iText.Chunk>();
                float[] WidthPercentageMaxicareMemberDetails = { 25f, 75f, 0f };
                tblMaxicareMemberHeader.SetWidths(WidthPercentageMaxicareMemberDetails);



                _chnkLeftMemberInformation.Add(new iText.Chunk("Verification Code" + Environment.NewLine, dataFontBold));
                _chnkCenterMemberInformation.Add(new iText.Chunk(LOGNo + Environment.NewLine, dataFontBold));
                iText.Phrase _VerificationRowCol1 = new iText.Phrase();
                iText.Phrase _VerificationRowCol2 = new iText.Phrase();
                _VerificationRowCol1.AddRange(_chnkLeftMemberInformation);
                _VerificationRowCol2.AddRange(_chnkCenterMemberInformation);
                iTextPdf.PdfPCell _CellVerificationLabel = new iTextPdf.PdfPCell(_VerificationRowCol1);
                _CellVerificationLabel.Border = 0;
                _CellVerificationLabel.SetLeading(0.0f, 1.6f);
                iTextPdf.PdfPCell _CellVerificationCode = new iTextPdf.PdfPCell(_VerificationRowCol2);
                _CellVerificationCode.Border = 0;
                _CellVerificationCode.SetLeading(0.0f, 1.6f);
                tblMaxicareMemberHeader.AddCell(_CellVerificationLabel);
                tblMaxicareMemberHeader.AddCell(_CellVerificationCode);
                tblMaxicareMemberHeader.AddCell(_headerQrCode);
                _chnkLeftMemberInformation.Clear();
                _chnkCenterMemberInformation.Clear();




                _chnkLeftMemberInformation.Add(new iText.Chunk("Appointment Date :" + Environment.NewLine, dataFontNormal));
                _chnkCenterMemberInformation.Add(new iText.Chunk(AvailmentDate + Environment.NewLine, dataFontNormal));
                _chnkLeftMemberInformation.Add(new iText.Chunk("Issued On :" + Environment.NewLine, dataFontNormal));
                _chnkCenterMemberInformation.Add(new iText.Chunk(RequestDate + Environment.NewLine, dataFontNormal));
                _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                _chnkCenterMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                iText.Phrase _DateRowCol1 = new iText.Phrase();
                iText.Phrase _DateRowCol2 = new iText.Phrase();
                _DateRowCol1.AddRange(_chnkLeftMemberInformation);
                _DateRowCol2.AddRange(_chnkCenterMemberInformation);
                iTextPdf.PdfPCell _CellDateCol1 = new iTextPdf.PdfPCell(_DateRowCol1);
                _CellDateCol1.Border = 0;
                _CellDateCol1.SetLeading(0.0f, 1.6f);
                iTextPdf.PdfPCell _CellDateCol2 = new iTextPdf.PdfPCell(_DateRowCol2);
                _CellDateCol2.Border = 0;
                _CellDateCol2.Colspan = 2;
                _CellDateCol2.SetLeading(0.0f, 1.6f);
                tblMaxicareMemberHeader.AddCell(_CellDateCol1);
                tblMaxicareMemberHeader.AddCell(_CellDateCol2);
                _chnkLeftMemberInformation.Clear();
                _chnkCenterMemberInformation.Clear();


                _chnkLeftMemberInformation.Add(new iText.Chunk("Member Detail" + Environment.NewLine, dataFontBold));
                iText.Phrase _MemberDetailsHeader = new iText.Phrase();
                _MemberDetailsHeader.AddRange(_chnkLeftMemberInformation);
                _MemberDetailsHeader.AddRange(_chnkCenterMemberInformation);
                iTextPdf.PdfPCell _CellMemberHeader = new iTextPdf.PdfPCell(_MemberDetailsHeader);
                _CellMemberHeader.Border = 0;
                _CellMemberHeader.Colspan = 3;
                _CellMemberHeader.SetLeading(0.0f, 1.6f);
                tblMaxicareMemberHeader.AddCell(_CellMemberHeader);
                _chnkLeftMemberInformation.Clear();

                _chnkLeftMemberInformation.Add(new iText.Chunk("Card No:" + Environment.NewLine, dataFontNormal));
                _chnkCenterMemberInformation.Add(new iText.Chunk(MemberID + Environment.NewLine, dataFontNormal));
                _chnkLeftMemberInformation.Add(new iText.Chunk("Name :" + Environment.NewLine, dataFontNormal));
                _chnkCenterMemberInformation.Add(new iText.Chunk(PatientName + Environment.NewLine, dataFontNormal));
                _chnkLeftMemberInformation.Add(new iText.Chunk("Company :" + Environment.NewLine, dataFontNormal));
                _chnkCenterMemberInformation.Add(new iText.Chunk(Corporation + Environment.NewLine, dataFontNormal));
                _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                _chnkCenterMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                iText.Phrase _MemberDetailsRowCol1 = new iText.Phrase();
                iText.Phrase _MemberDetailsRowCol2 = new iText.Phrase();
                _MemberDetailsRowCol1.AddRange(_chnkLeftMemberInformation);
                _MemberDetailsRowCol2.AddRange(_chnkCenterMemberInformation);
                iTextPdf.PdfPCell _CellMemberDetailsLabel = new iTextPdf.PdfPCell(_MemberDetailsRowCol1);
                _CellMemberDetailsLabel.Border = 0;
                _CellMemberDetailsLabel.SetLeading(0.0f, 1.6f);
                iTextPdf.PdfPCell _CellMemberDetailsInfo = new iTextPdf.PdfPCell(_MemberDetailsRowCol2);
                _CellMemberDetailsInfo.Border = 0;
                _CellMemberDetailsInfo.Colspan = 2;
                _CellMemberDetailsInfo.SetLeading(0.0f, 1.6f);
                tblMaxicareMemberHeader.AddCell(_CellMemberDetailsLabel);
                tblMaxicareMemberHeader.AddCell(_CellMemberDetailsInfo);
                _chnkLeftMemberInformation.Clear();
                _chnkCenterMemberInformation.Clear();


                _chnkLeftMemberInformation.Add(new iText.Chunk("Clinic Detail" + Environment.NewLine, dataFontBold));
                iText.Phrase _ClinicDetailsHeader = new iText.Phrase();
                _ClinicDetailsHeader.AddRange(_chnkLeftMemberInformation);
                _ClinicDetailsHeader.AddRange(_chnkCenterMemberInformation);
                iTextPdf.PdfPCell _CellClinicDetailsHeader = new iTextPdf.PdfPCell(_ClinicDetailsHeader);
                _CellClinicDetailsHeader.Border = 0;
                _CellClinicDetailsHeader.Colspan = 3;
                _CellClinicDetailsHeader.SetLeading(0.0f, 1.6f);
                tblMaxicareMemberHeader.AddCell(_CellClinicDetailsHeader);
                _chnkLeftMemberInformation.Clear();

                _chnkLeftMemberInformation.Add(new iText.Chunk("Clinic Name :" + Environment.NewLine, dataFontNormal));
                _chnkCenterMemberInformation.Add(new iText.Chunk(ServiceProvider + Environment.NewLine, dataFontNormal));
                _chnkLeftMemberInformation.Add(new iText.Chunk("Contact No :", dataFontNormal));
                if (!string.IsNullOrEmpty(_report.Tables[3].Rows[0]["ProviderContact"].ToString()))
                {
                    if (!string.IsNullOrEmpty(ContactMobile))
                    {
                        _chnkCenterMemberInformation.Add(new iText.Chunk(ContactMobile + Environment.NewLine, dataFontNormal));
                        _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                        //_chnkLeftMemberInformation.Add(new iText.Chunk("                                         ", dataFontNormal));
                    }
                    if (!string.IsNullOrEmpty(ContactLandLine))
                    {
                        _chnkCenterMemberInformation.Add(new iText.Chunk(ContactLandLine + Environment.NewLine, dataFontNormal));
                        _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                        //_chnkLeftMemberInformation.Add(new iText.Chunk("                                         ", dataFontNormal));
                    }
                    if (!string.IsNullOrEmpty(ContactFax))
                    {
                        _chnkCenterMemberInformation.Add(new iText.Chunk(ContactFax + Environment.NewLine, dataFontNormal));
                        _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                    }
                    else
                    {
                        _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                    }

                }
                else
                {
                    _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                }
                _chnkLeftMemberInformation.Add(new iText.Chunk("Address :" + Environment.NewLine, dataFontNormal));
                _chnkCenterMemberInformation.Add(new iText.Chunk(Address + Environment.NewLine, dataFontNormal));
                _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                _chnkCenterMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                iText.Phrase _ClinicDetailsRowCol1 = new iText.Phrase();
                iText.Phrase _ClinicDetailsRowCol2 = new iText.Phrase();
                _ClinicDetailsRowCol1.AddRange(_chnkLeftMemberInformation);
                _ClinicDetailsRowCol2.AddRange(_chnkCenterMemberInformation);
                iTextPdf.PdfPCell _CellClinicDetailsLabel = new iTextPdf.PdfPCell(_ClinicDetailsRowCol1);
                _CellClinicDetailsLabel.Border = 0;
                _CellClinicDetailsLabel.SetLeading(0.0f, 1.6f);
                iTextPdf.PdfPCell _CellClinicDetails = new iTextPdf.PdfPCell(_ClinicDetailsRowCol2);
                _CellClinicDetails.Border = 0;
                _CellClinicDetails.Colspan = 2;
                _CellClinicDetails.SetLeading(0.0f, 1.6f);
                tblMaxicareMemberHeader.AddCell(_CellClinicDetailsLabel);
                tblMaxicareMemberHeader.AddCell(_CellClinicDetails);
                _chnkLeftMemberInformation.Clear();
                _chnkCenterMemberInformation.Clear();


                _chnkLeftMemberInformation.Add(new iText.Chunk("Location", dataFontBold));
                _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                iText.Phrase _MapLocationHeader = new iText.Phrase();
                _MapLocationHeader.AddRange(_chnkLeftMemberInformation);
                _MapLocationHeader.AddRange(_chnkCenterMemberInformation);
                iTextPdf.PdfPCell _CellMapLocationHeader = new iTextPdf.PdfPCell(_MapLocationHeader);
                _CellMapLocationHeader.Border = 0;
                _CellMapLocationHeader.Colspan = 3;
                _CellMapLocationHeader.SetLeading(0.0f, 1.6f);
                tblMaxicareMemberHeader.AddCell(_CellMapLocationHeader);
                _chnkLeftMemberInformation.Clear();



                string _MapCoordinates = _report.Tables["ContactInfo"].Rows[0]["Latitude"].ToString() + "," + _report.Tables["ContactInfo"].Rows[0]["Longitude"].ToString();
                //Map style roadmap
                try
                {
                    iText.Image LocationImage = iText.Image.GetInstance("http://maps.google.com/maps/api/staticmap?center=" + _MapCoordinates + "&zoom=17&size=425x200&scale=2&maptype=roadmap&markers=color:red|color:red|label:H|" + _MapCoordinates + "&sensor=false");
                    LocationImage.ScaleToFit(425, 200);
                    //LocationImage.GrayFill;
                    LocationImage.Alignment = iText.Image.UNDERLYING;
                    LocationImage.SetAbsolutePosition(0, 0);
                    iTextPdf.PdfPCell locationcell = new iTextPdf.PdfPCell(LocationImage);
                    locationcell.PaddingBottom = 20;
                    locationcell.PaddingLeft = 30;
                    locationcell.PaddingRight = 30;
                    locationcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    locationcell.Colspan = 3;
                    locationcell.Border = 0;
                    iText.Phrase _MapLocationHeader2 = new iText.Phrase();
                    _MapLocationHeader2.AddRange(_chnkLeftMemberInformation);
                    _MapLocationHeader2.AddRange(_chnkCenterMemberInformation);
                    iTextPdf.PdfPCell _CellMapLocationHeader2 = new iTextPdf.PdfPCell(locationcell);
                    _CellMapLocationHeader2.Border = 0;
                    _CellMapLocationHeader2.Colspan = 3;
                    _CellMapLocationHeader2.SetLeading(0.0f, 1.6f);
                    _CellMapLocationHeader2.HorizontalAlignment = 1;
                    tblMaxicareMemberHeader.AddCell(_CellMapLocationHeader2);
                    _chnkLeftMemberInformation.Clear();
                }
                catch (Exception)
                {

                    _chnkLeftMemberInformation.Add(new iText.Chunk("Location Not Available!", dataFontNormal));
                    _chnkLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                    iText.Phrase _MapLocationHeader2 = new iText.Phrase();
                    _MapLocationHeader2.AddRange(_chnkLeftMemberInformation);
                    _MapLocationHeader2.AddRange(_chnkCenterMemberInformation);
                    iTextPdf.PdfPCell _CellMapLocationHeader2 = new iTextPdf.PdfPCell(_MapLocationHeader2);
                    _CellMapLocationHeader2.Border = 0;
                    _CellMapLocationHeader2.Colspan = 3;
                    _CellMapLocationHeader2.SetLeading(0.0f, 1.6f);
                    _CellMapLocationHeader2.HorizontalAlignment = 1;
                    tblMaxicareMemberHeader.AddCell(_CellMapLocationHeader2);
                    _chnkLeftMemberInformation.Clear();
                }

                //Map End



                iText.Phrase _phLeftMemberInformation = new iText.Phrase();
                _phLeftMemberInformation.AddRange(_chnkLeftMemberInformation);
                iTextPdf.PdfPCell _LeftMemberInfo = new iTextPdf.PdfPCell(_phLeftMemberInformation);
                _LeftMemberInfo.Border = 0;
                _LeftMemberInfo.SetLeading(0.0f, 1.6f);

                iText.Phrase _phCenterMemberInformation = new iText.Phrase();
                _phCenterMemberInformation.AddRange(_chnkCenterMemberInformation);
                iTextPdf.PdfPCell _CenterMemberInfo = new iTextPdf.PdfPCell(_phCenterMemberInformation);
                _CenterMemberInfo.Border = 0;
                _CenterMemberInfo.SetLeading(0.0f, 1.6f);


                iText.Phrase _phRightMemberInformation = new iText.Phrase();
                iTextPdf.PdfPCell _RightMemberInfo = new iTextPdf.PdfPCell(_Blank);
                _RightMemberInfo.Border = 0;
                _RightMemberInfo.PaddingBottom = 5f;
                _RightMemberInfo.SetLeading(0.0f, 1.6f);

                _phLeftMemberInformation.Add(new iText.Chunk("", dataFontBold));
                _phLeftMemberInformation.Add(new iText.Chunk(Environment.NewLine));
                pdfContentByte.Rectangle(93f, Document.PageSize.Height - 0f, 268f, 1.6f);

                tblMaxicareMemberHeader.AddCell(_LeftMemberInfo);
                tblMaxicareMemberHeader.AddCell(_CenterMemberInfo);
                tblMaxicareMemberHeader.AddCell(_RightMemberInfo);
                tblBody.AddCell(tblMaxicareMemberHeader);

                #endregion

                #region Instructions

                tblBody.WidthPercentage = 90f;
                Document.Add(tblBody);
                // Document.Add(table);

                #endregion
                #region FooterAgreement

                iText.Image DontiaFooter = iText.Image.GetInstance(_imgDontiaFooter.ElementAt(0).ToArray());
                DontiaFooter.ScaleToFit(180, 260);
                DontiaFooter.SetAbsolutePosition(120, 120);
                iTextPdf.PdfPCell FooterImage = new iTextPdf.PdfPCell(DontiaFooter);
                FooterImage.Border = 0;
                FooterImage.PaddingTop = 5f;
                iTextPdf.PdfPTable tblFooter = new iTextPdf.PdfPTable(2);
                float[] FootercolWidthPercentages = { 67, 33 };
                tblFooter.SetWidths(FootercolWidthPercentages);
                tblFooter.AddCell(_headerCompanyInfo);
                tblFooter.AddCell(FooterImage);
                tblFooter.WidthPercentage = 90f;
                Document.Add(tblFooter);

                //iText.Phrase _ContentAgreementBody = new iText.Phrase();
                //List<iText.Chunk> _AgreementChunkBody = new List<iText.Chunk>();
                //_AgreementChunkBody.Add(new iText.Chunk("Call our Customer Service Center 24/7 ", dataFooterBold));
                //_AgreementChunkBody.Add(new iText.Chunk(Environment.NewLine));
                //_AgreementChunkBody.Add(new iText.Chunk("Customer Support : +66 2625 9106, +63 2 755 6553", Footer));
                //_AgreementChunkBody.Add(new iText.Chunk(Environment.NewLine));
                //_AgreementChunkBody.Add(new iText.Chunk("+1 212 444 0600", Footer));
                //_AgreementChunkBody.Add(new iText.Chunk(Environment.NewLine));
                //_AgreementChunkBody.Add(new iText.Chunk("(Long distance charge may aplly)", Footer));

                //_ContentAgreementBody.AddRange(_AgreementChunkBody);

                //iTextPdf.PdfPCell _AgreementBody = new iTextPdf.PdfPCell(_ContentAgreementBody);
                //_AgreementBody.HorizontalAlignment = iTextPdf.PdfPCell.ALIGN_RIGHT;
                //_AgreementBody.Border = 0;
                //_AgreementBody.SetLeading(0.00f, 1.2f);

                //iTextPdf.PdfPTable tblConforme = new iTextPdf.PdfPTable(1);


                //tblConforme.AddCell(_AgreementBody);
                //tblConforme.WidthPercentage = 90f;
                //Document.Add(tblConforme);
                #endregion
                Document.Close();
                Document.Dispose();
                _pdfreport = new ModPdfReport()
                {
                    FileByteStream = _stream,
                    PdfFileName = _fileName
                };

            }
            catch (Exception ex)
            {
                //myServiceData.ErrorMessage = ex.Message.ToString();
                //throw new FaultException<ServiceData>(myServiceData, ex.ToString());
            }




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
                    dbConnect = new SqlConnection(ConfigurationManager.ConnectionStrings["ConDentaLINKSGDB_Service"].ConnectionString);
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



        public byte[] PrintLOG(string x)
        {
            DatabaseHandler _db = new DatabaseHandler(constring);
            LOGClassPdf _method = new LOGClassPdf(_db);
            byte[] loaReport;
            loaReport = _method.PrintLOG1(x);
            return loaReport;
        }

        private byte[] PrintLOG1(string x)
        {
            byte[] data = null;
            LOGImage LOGImage = new LOGImage();
            ModPdfReport _report = new ModPdfReport();
            _report = CreateLOGPDFReport(x, LOGImage.ConvertImageToByte(), LOGImage.ImgDontiaLink(),LOGImage.ImgDontiaFooter());
            data = _report.FileByteStream.ToArray();
            return data;
        }

        public byte[] GenerateQR_eCard(string prefixText)
        {
            var bw = new ZXing.BarcodeWriter();
            var encOptions = new ZXing.Common.EncodingOptions() { Width = 90, Height = 90, Margin = 0 };
            bw.Options = encOptions;
            bw.Format = ZXing.BarcodeFormat.QR_CODE;
            var result = bw.Write(prefixText);

            MemoryStream myMemoryStream = new MemoryStream();
            result.Save(myMemoryStream, ImageFormat.Jpeg);
            return myMemoryStream.ToArray();
        }



        public string _fileName { get; set; }
        public string RequestNo { get; set; }
        public string constring { get; set; }
    }
}
