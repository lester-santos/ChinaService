using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;


namespace MaxiFlexProxy
{
    public class LOApdfGenerator
    {
        public byte[] byteLOApdf(string ClaimNo, DataSet ds)
        {
            DataTable dtMemberInfo = ds.Tables[0];
            DataTable dtICD = ds.Tables[1];
            DataTable dtCPT = ds.Tables[2];
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 60f, 60f, 50f, 30f);
            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, ms);
            Paragraph BlankPara = new Paragraph("");
            BaseColor WhiteColor = WebColors.GetRGBColor("#FFFFFF");
            BaseColor HeaderColor = WebColors.GetRGBColor("#033B6F");
            BaseColor Blue = WebColors.GetRGBColor("#045ED3");
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font HelveticaNormal = FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font timesbold = new iTextSharp.text.Font(bfTimes, 7, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font timesItalic = new iTextSharp.text.Font(bfTimes, 11, iTextSharp.text.Font.ITALIC, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font helvetica = FontFactory.GetFont(FontFactory.HELVETICA, 6, Font.NORMAL, iTextSharp.text.BaseColor.BLACK);


            doc.Open();





            iTextSharp.text.Image Maxijpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/img/LOA/maxicarehome.jpg"));
            Maxijpg.ScaleToFit(70f, 70f);
            //Maxijpg.Alignment = iTextSharp.text.Image.TEXTWRAP | iTextSharp.text.Image.ALIGN_LEFT;
            //Maxijpg.IndentationLeft = 9f;
            //Maxijpg.SpacingAfter = 9f;
            Maxijpg.SetAbsolutePosition(63, 720);


            Chunk c1 = new Chunk(Environment.NewLine + "Maxicare Healthcare Corporation" + Environment.NewLine, timesItalic);
            Chunk c2 = new Chunk("Main Office: 203 Maxicare Tower, Salcedo Street, Legaspi Village, Makati City" + Environment.NewLine, helvetica);
            Chunk c3 = new Chunk("Toll-Free No.: 1-800-10-889-6294 Call Center Toll-Free No.: 1-800-10-5821-900" + Environment.NewLine, helvetica);
            Chunk c4 = new Chunk("Corporate Trunkline: 9086-900 Call Center Hotline: 5821-900" + Environment.NewLine, helvetica);
            Chunk c5 = new Chunk("E-mail: inquiry_customer_care@maxicare.com.ph" + Environment.NewLine, helvetica);
            Chunk c6 = new Chunk("Homepage: http://www.maxicare.com.ph" + Environment.NewLine, helvetica);
            Phrase pLogo = new Phrase();
            pLogo.Add(c1);
            pLogo.Add(c2);
            pLogo.Add(c3);
            pLogo.Add(c4);
            pLogo.Add(c5);
            pLogo.Add(c6);
            Paragraph pLogoCenter = new Paragraph();
            pLogoCenter.Alignment = Element.ALIGN_CENTER;
            pLogoCenter.Add(pLogo);
            //pLogoCenter.Leading = 0;
            //pLogoCenter.MultipliedLeading = 0;
            pLogoCenter.SetLeading(7, 0);



            BarcodeQRCode _qrcode = new BarcodeQRCode(dtMemberInfo.Rows[0]["CardNo"].ToString() + "-" + ClaimNo + "-" + dtMemberInfo.Rows[0]["AvailmentDate"].ToString(), 123, 123, null);
            Image imgQrCode = _qrcode.GetImage();
            imgQrCode.SetAbsolutePosition(425, 685);


            doc.Add(Maxijpg);
            doc.Add(imgQrCode);
            doc.Add(pLogoCenter);


            BlankPara.SpacingBefore = 25;
            doc.Add(BlankPara);
            Paragraph paraTitle = new Paragraph("Letter of Authorization", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD, Blue));
            paraTitle.Alignment = Element.ALIGN_CENTER;
            doc.Add(paraTitle);



            BlankPara.SpacingBefore = 5;
            doc.Add(BlankPara);
            Paragraph paraHospital = new Paragraph("  " + dtMemberInfo.Rows[0]["ClinicName"].ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.BOLD, iTextSharp.text.BaseColor.BLACK));
            paraHospital.Alignment = Element.ALIGN_LEFT;
            doc.Add(paraHospital);



            BlankPara.SpacingBefore = 15;
            doc.Add(BlankPara);


            Chunk cdep1 = new Chunk("  Credit and Collection/" + Environment.NewLine, helvetica);
            Chunk cdep2 = new Chunk("   Billing Department/" + Environment.NewLine, helvetica);
            Chunk cdep3 = new Chunk("   Accounting Department/" + Environment.NewLine, helvetica);
            Chunk cdep4 = new Chunk("   Industrial Clinic/" + Environment.NewLine, helvetica);
            Phrase pdep = new Phrase();
            pdep.Add(cdep1);
            pdep.Add(cdep2);
            pdep.Add(cdep3);
            pdep.Add(cdep4);
            Paragraph paraDepartment = new Paragraph(pdep);
            paraDepartment.Alignment = Element.ALIGN_LEFT;
            paraDepartment.SetLeading(7, 0);
            doc.Add(paraDepartment);


            BlankPara.SpacingBefore = 10;
            doc.Add(BlankPara);
            Paragraph paraCertification = new Paragraph("  This is to Certify that MAXICARE will pay for the hospital bills of bona fide MAXICARE Member named herein, with the exception of below-specified charges to be collected from        the same Member during discharge.", helvetica);
            paraCertification.Alignment = Element.ALIGN_LEFT;
            doc.Add(paraCertification);


            BlankPara.SpacingBefore = 10;
            doc.Add(BlankPara);

            #region Member Information

            PdfPTable MemberInfoTableCell = new PdfPTable(9);

            /////////////////////// First Row /////////////////////////////////
            PdfPCell Membercell = new PdfPCell(new Phrase(p("Name: ", dtMemberInfo.Rows[0]["FULLNAME"].ToString())));
            Membercell.Colspan = 4;
            Membercell.Border = Rectangle.NO_BORDER;
            MemberInfoTableCell.AddCell(Membercell);

            Membercell = new PdfPCell(new Phrase(p("Sex: ", dtMemberInfo.Rows[0]["GENDER"].ToString())));
            Membercell.Border = Rectangle.NO_BORDER;
            MemberInfoTableCell.AddCell(Membercell);


            Membercell = new PdfPCell(new Phrase(p("Age: ", dtMemberInfo.Rows[0]["AGE"].ToString())));
            Membercell.Colspan = 4;
            Membercell.Border = Rectangle.NO_BORDER;
            MemberInfoTableCell.AddCell(Membercell);

            //Membercell = new PdfPCell(new Phrase(p("Type Of Plan: ", "BRONZE")));
            //Membercell.Colspan = 2;
            //Membercell.Border = Rectangle.NO_BORDER;
            //MemberInfoTableCell.AddCell(Membercell);
            ////////////////////////////// Second Row /////////////////////////////
            Membercell = new PdfPCell(new Phrase(p("Company: ", dtMemberInfo.Rows[0]["CORPNAME"].ToString())));
            Membercell.Colspan = 5;
            Membercell.Border = Rectangle.NO_BORDER;
            MemberInfoTableCell.AddCell(Membercell);

            Membercell = new PdfPCell(new Phrase(p("Maxicare ID #: ", dtMemberInfo.Rows[0]["CARDNO"].ToString())));
            Membercell.Colspan = 4;
            Membercell.Border = Rectangle.NO_BORDER;
            MemberInfoTableCell.AddCell(Membercell);
            ////////////////////////////// Third Row /////////////////////////////
            Membercell = new PdfPCell(new Phrase(p("Attending Doctors: ", dtMemberInfo.Rows[0]["PHYSICIANNAME"].ToString())));
            Membercell.Colspan = 5;
            Membercell.Border = Rectangle.NO_BORDER;
            MemberInfoTableCell.AddCell(Membercell);

            Membercell = new PdfPCell(new Phrase(p("Effectivity: ", Convert.ToDateTime(dtMemberInfo.Rows[0]["EffectiveDate"]).ToString("dd-MMM-yyyy"))));
            Membercell.Colspan = 2;
            Membercell.Border = Rectangle.NO_BORDER;
            MemberInfoTableCell.AddCell(Membercell);

            Membercell = new PdfPCell(new Phrase(p("Expiry: ", Convert.ToDateTime(dtMemberInfo.Rows[0]["ExpiryDate"]).ToString("dd-MMM-yyyy"))));
            Membercell.Colspan = 2;
            Membercell.Border = Rectangle.NO_BORDER;
            MemberInfoTableCell.AddCell(Membercell);
            ////////////////////////////// Fourth Row /////////////////////////////
            Membercell = new PdfPCell(new Phrase(p("With Room Entitlement Of: ", dtMemberInfo.Rows[0]["RoomTypeDesc"].ToString())));
            Membercell.Colspan = 5;
            Membercell.Border = Rectangle.NO_BORDER;
            MemberInfoTableCell.AddCell(Membercell);

            Membercell = new PdfPCell(new Phrase(p("Date Admitted: ", Convert.ToDateTime(dtMemberInfo.Rows[0]["AvailmentDate"]).ToString("dd-MMM-yyyy"))));
            Membercell.Colspan = 4;
            Membercell.Border = Rectangle.NO_BORDER;
            MemberInfoTableCell.AddCell(Membercell);
            ////////////////////////////// Fifth Row /////////////////////////////
            //Membercell = new PdfPCell(new Phrase(""));
            //Membercell.Colspan = 5;
            //Membercell.Border = Rectangle.NO_BORDER;
            //MemberInfoTableCell.AddCell(Membercell);

            //Membercell = new PdfPCell(new Phrase(p("Room No: ", "1,0000.00")));
            //Membercell.Colspan = 4;
            //Membercell.Border = Rectangle.NO_BORDER;
            //MemberInfoTableCell.AddCell(Membercell);
            /////////////////////////////////////////////////////////////////////////


            PdfPTable MemberInfoHeader = new PdfPTable(1);
            PdfPCell MemberInfoCell = new PdfPCell(new Phrase("MEMBER INFORMATION", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, Font.NORMAL, WhiteColor)));
            MemberInfoCell.BorderWidth = 1;
            MemberInfoCell.Padding = 5;
            MemberInfoCell.PaddingTop = 3;
            MemberInfoCell.BorderColor = HeaderColor;
            MemberInfoCell.HorizontalAlignment = Element.ALIGN_CENTER;

            MemberInfoCell.BackgroundColor = HeaderColor;
            MemberInfoHeader.AddCell(MemberInfoCell);
            MemberInfoHeader.SetWidthPercentage(new float[] { 598f }, PageSize.LETTER);
            MemberInfoHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            doc.Add(MemberInfoHeader);

            PdfPTable MemberInfoTable = new PdfPTable(1);
            MemberInfoTable.AddCell(MemberInfoTableCell);
            MemberInfoTable.SetWidthPercentage(new float[1] { 598f }, PageSize.LETTER);
            MemberInfoTable.HorizontalAlignment = Element.ALIGN_CENTER;
            doc.Add(MemberInfoTable);

            #endregion


            BlankPara.SpacingBefore = 5;
            doc.Add(BlankPara);

            #region Diagnosis

            PdfPTable DiagnosisTableCell = new PdfPTable(4);

            PdfPCell Diagnosiscell = new PdfPCell(new Phrase("ICD CODE", timesbold));
            Diagnosiscell.Border = Rectangle.NO_BORDER;
            DiagnosisTableCell.AddCell(Diagnosiscell);
            Diagnosiscell = new PdfPCell(new Phrase("DIAGNOSIS", timesbold));
            Diagnosiscell.Colspan = 3;
            Diagnosiscell.Border = Rectangle.NO_BORDER;
            DiagnosisTableCell.AddCell(Diagnosiscell);

            foreach (DataRow dr in dtICD.Rows)
            {
                Diagnosiscell = new PdfPCell(new Phrase(dr["ICDCode"].ToString(), HelveticaNormal));
                Diagnosiscell.Border = Rectangle.NO_BORDER;
                DiagnosisTableCell.AddCell(Diagnosiscell);
                Diagnosiscell = new PdfPCell(new Phrase(dr["ICDDesc"].ToString(), HelveticaNormal));
                Diagnosiscell.Colspan = 3;
                Diagnosiscell.Border = Rectangle.NO_BORDER;
                DiagnosisTableCell.AddCell(Diagnosiscell);
            }


            PdfPTable DiagnosisInfoHeader = new PdfPTable(1);
            PdfPCell DiagnosisCell = new PdfPCell(new Phrase("DIAGNOSIS", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, Font.NORMAL, WhiteColor)));
            DiagnosisCell.BorderWidth = 1;
            DiagnosisCell.Padding = 5;
            DiagnosisCell.PaddingTop = 3;
            DiagnosisCell.BorderColor = HeaderColor;
            DiagnosisCell.HorizontalAlignment = Element.ALIGN_CENTER;
            DiagnosisCell.BackgroundColor = HeaderColor;
            DiagnosisInfoHeader.AddCell(DiagnosisCell);
            DiagnosisInfoHeader.SetWidthPercentage(new float[1] { 598f }, PageSize.LETTER);
            DiagnosisInfoHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            doc.Add(DiagnosisInfoHeader);

            PdfPTable DiagnosisTable = new PdfPTable(1);
            DiagnosisTable.AddCell(DiagnosisTableCell);
            DiagnosisTable.SetWidthPercentage(new float[1] { 598f }, PageSize.LETTER);
            DiagnosisTable.HorizontalAlignment = Element.ALIGN_CENTER;
            doc.Add(DiagnosisTable);

            #endregion


            BlankPara.SpacingBefore = 5;
            doc.Add(BlankPara);

            #region Procedure

            PdfPTable ProcedureTableCell = new PdfPTable(4);




            PdfPCell Procedurecell = new PdfPCell(new Phrase("CPT CODE", timesbold));
            Procedurecell.Border = Rectangle.NO_BORDER;
            ProcedureTableCell.AddCell(Procedurecell);
            Procedurecell = new PdfPCell(new Phrase("CPT DESCRIPTION", timesbold));
            Procedurecell.Colspan = 3;
            Procedurecell.Border = Rectangle.NO_BORDER;
            ProcedureTableCell.AddCell(Procedurecell);

            foreach (DataRow dr in dtCPT.Rows)
            {
                Procedurecell = new PdfPCell(new Phrase(dr["CPTCode"].ToString(), HelveticaNormal));
                Procedurecell.Border = Rectangle.NO_BORDER;
                ProcedureTableCell.AddCell(Procedurecell);
                Procedurecell = new PdfPCell(new Phrase(dr["CPTDesc"].ToString(), HelveticaNormal));
                Procedurecell.Colspan = 3;
                Procedurecell.Border = Rectangle.NO_BORDER;
                ProcedureTableCell.AddCell(Procedurecell);
            }



            PdfPTable ProcedureInfoHeader = new PdfPTable(1);
            PdfPCell ProcedureCell = new PdfPCell(new Phrase("PROCEDURE/S", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, Font.NORMAL, WhiteColor)));
            ProcedureCell.BorderWidth = 1;
            ProcedureCell.Padding = 5;
            ProcedureCell.PaddingTop = 3;
            ProcedureCell.BorderColor = HeaderColor;
            ProcedureCell.HorizontalAlignment = Element.ALIGN_CENTER;
            ProcedureCell.BackgroundColor = HeaderColor;
            ProcedureInfoHeader.AddCell(ProcedureCell);
            ProcedureInfoHeader.SetWidthPercentage(new float[1] { 598f }, PageSize.LETTER);
            ProcedureInfoHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            doc.Add(ProcedureInfoHeader);

            PdfPTable ProcedureTable = new PdfPTable(1);
            ProcedureTable.AddCell(ProcedureTableCell);
            ProcedureTable.SetWidthPercentage(new float[1] { 598f }, PageSize.LETTER);
            ProcedureTable.HorizontalAlignment = Element.ALIGN_CENTER;
            doc.Add(ProcedureTable);

            #endregion
            BlankPara.SpacingBefore = 5;
            doc.Add(BlankPara);

            #region Instruction

            PdfPTable InstructionTableCell = new PdfPTable(9);

            PdfPCell Instructioncell = new PdfPCell(new Phrase(p("IMPORTANT: ", "")));
            Instructioncell.Border = Rectangle.NO_BORDER;
            InstructionTableCell.AddCell(Instructioncell);

            Instructioncell = new PdfPCell(new Phrase(p("", "This space intended only for clarificatory statements. Use of this space is not authorized for procedure approvals, rate changes or any other information with financial impact. Any such remarks shall not be considered or processed at claims settlement.")));
            Instructioncell.Colspan = 8;
            Instructioncell.Border = Rectangle.NO_BORDER;
            InstructionTableCell.AddCell(Instructioncell);

            Instructioncell = new PdfPCell(new Phrase(p(" ", "")));
            Instructioncell.Colspan = 9;
            Instructioncell.Border = Rectangle.NO_BORDER;
            InstructionTableCell.AddCell(Instructioncell);

            PdfPTable InstructionInfoHeader = new PdfPTable(1);
            PdfPCell InstructionCell = new PdfPCell(new Phrase("INSTRUCTION TO PROVIDER", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, Font.NORMAL, WhiteColor)));
            InstructionCell.BorderWidth = 1;
            InstructionCell.Padding = 5;
            InstructionCell.PaddingTop = 3;
            InstructionCell.BorderColor = HeaderColor;
            InstructionCell.HorizontalAlignment = Element.ALIGN_CENTER;
            InstructionCell.BackgroundColor = HeaderColor;
            InstructionInfoHeader.AddCell(InstructionCell);
            InstructionInfoHeader.SetWidthPercentage(new float[1] { 598f }, PageSize.LETTER);
            InstructionInfoHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            doc.Add(InstructionInfoHeader);

            PdfPTable InstructionTable = new PdfPTable(1);
            InstructionTable.AddCell(InstructionTableCell);
            InstructionTable.SetWidthPercentage(new float[1] { 598f }, PageSize.LETTER);
            InstructionTable.HorizontalAlignment = Element.ALIGN_CENTER;
            doc.Add(InstructionTable);


            #endregion

            BlankPara.SpacingBefore = 5;
            doc.Add(BlankPara);

            string _MapCoordinates = dtMemberInfo.Rows[0]["Latitude"].ToString() + "," + dtMemberInfo.Rows[0]["Longitude"].ToString();
            //Map style roadmap
            PdfPTable location = new PdfPTable(1);
            location.SetWidthPercentage(new float[1] { 598f }, PageSize.LETTER);
            location.HorizontalAlignment = Element.ALIGN_CENTER;
            Image LocationImage;
            try
            {
                LocationImage = Image.GetInstance("http://maps.google.com/maps/api/staticmap?center=" + _MapCoordinates + "&zoom=17&size=425x200&scale=2&maptype=roadmap&markers=color:red|color:red|label:H|" + _MapCoordinates + "&sensor=false");

            }
            catch (Exception)
            {
                LocationImage = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/img/LOA/maxicarehome.jpg"));
            }
            LocationImage.ScaleToFit(350, 210);
            LocationImage.Alignment = Image.UNDERLYING;
            LocationImage.SetAbsolutePosition(0, 0);
            PdfPCell locationcell = new PdfPCell(LocationImage);
            locationcell.PaddingBottom = 10;
            locationcell.PaddingLeft = 10;
            locationcell.PaddingRight = 10;
            locationcell.PaddingTop = 10;
            locationcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            locationcell.Colspan = 3;
            locationcell.BorderWidth = 1;
            PdfPCell _CellMapLocationHeader2 = new PdfPCell(locationcell);
            _CellMapLocationHeader2.BorderWidth = 1;
            _CellMapLocationHeader2.BorderColor = BaseColor.GRAY;
            _CellMapLocationHeader2.Colspan = 3;
            _CellMapLocationHeader2.SetLeading(0.0f, 1.6f);
            _CellMapLocationHeader2.HorizontalAlignment = 1;

            location.AddCell(_CellMapLocationHeader2);

            //doc.Add(location);
            Paragraph pInstruction = new Paragraph("   The following hospital charges will not be paid by MAXICARE hence, should be collected from the above named Member during discharge:", helvetica);
            pInstruction.SpacingBefore = 5;
            pInstruction.SpacingAfter = 2;
            doc.Add(pInstruction);


            #region Empty table

            PdfPTable EmptyTableCell = new PdfPTable(1);

            int x = 0;
            while (x < 3)
            {
                PdfPCell Emptycell = new PdfPCell(new Phrase(p(" ", "")));
                Emptycell.Border = Rectangle.NO_BORDER;
                EmptyTableCell.AddCell(Emptycell);
                x++;
            }

            PdfPTable EmptyTable = new PdfPTable(1);
            EmptyTable.AddCell(EmptyTableCell);
            EmptyTable.SetWidthPercentage(new float[1] { 598f }, PageSize.LETTER);
            EmptyTable.HorizontalAlignment = Element.ALIGN_CENTER;
            doc.Add(EmptyTable);


            #endregion
            BlankPara.SpacingBefore = 5;
            doc.Add(BlankPara);

            PdfPTable IssuedInfoTableCell = new PdfPTable(9);

            /////////////////////// First Row /////////////////////////////////
            PdfPCell Issuedcell = new PdfPCell(new Phrase(p(" Prepared and Issued By:", "")));
            Issuedcell.Colspan = 7;
            Issuedcell.Border = Rectangle.NO_BORDER;
            Issuedcell.HorizontalAlignment = Element.ALIGN_LEFT;
            IssuedInfoTableCell.AddCell(Issuedcell);

            Issuedcell = new PdfPCell(new Phrase(p(" Approved and Released By:", "")));
            Issuedcell.Colspan = 2;
            Issuedcell.Border = Rectangle.NO_BORDER;
            IssuedInfoTableCell.AddCell(Issuedcell);
            /////////////////////// Second Row /////////////////////////////////
            Issuedcell = new PdfPCell(new Phrase(p(" ", "")));
            Issuedcell.Colspan = 9;
            Issuedcell.Border = Rectangle.NO_BORDER;
            Issuedcell.HorizontalAlignment = Element.ALIGN_LEFT;
            IssuedInfoTableCell.AddCell(Issuedcell);
            /////////////////////// third Row /////////////////////////////////
            Issuedcell = new PdfPCell(new Phrase(p(" ", dtMemberInfo.Rows[0]["IssueBy"].ToString())));
            Issuedcell.Colspan = 7;
            Issuedcell.Border = Rectangle.NO_BORDER;
            Issuedcell.HorizontalAlignment = Element.ALIGN_LEFT;
            IssuedInfoTableCell.AddCell(Issuedcell);

            Issuedcell = new PdfPCell(new Phrase(p(" ", dtMemberInfo.Rows[0]["IssueBy"].ToString())));
            Issuedcell.Colspan = 2;
            Issuedcell.Border = Rectangle.NO_BORDER;
            IssuedInfoTableCell.AddCell(Issuedcell);
            /////////////////////// fourth Row /////////////////////////////////
            Issuedcell = new PdfPCell(new Phrase(p(" ", DateTime.Now.ToString("dd-MMM-yyyy"))));
            Issuedcell.Colspan = 7;
            Issuedcell.Border = Rectangle.NO_BORDER;
            Issuedcell.HorizontalAlignment = Element.ALIGN_LEFT;
            IssuedInfoTableCell.AddCell(Issuedcell);

            Issuedcell = new PdfPCell(new Phrase(p(" ", DateTime.Now.ToString("dd-MMM-yyyy"))));
            Issuedcell.Colspan = 2;
            Issuedcell.Border = Rectangle.NO_BORDER;
            IssuedInfoTableCell.AddCell(Issuedcell);


            IssuedInfoTableCell.SetWidthPercentage(new float[9] { 68f, 68f, 68f, 68f, 68f, 68f, 68f, 68f, 68f }, PageSize.LETTER);
            doc.Add(IssuedInfoTableCell);

            doc.Close();
            doc.Dispose();






            byte[] Result = ms.ToArray();
            return Result;
            //System.IO.File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Document/GeneratedLOA/") + "LOA" + ClaimNo  , Result);
        }

        private Phrase p(string Title, string Description)
        {
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font HelveticaNormal = FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font timesbold = new iTextSharp.text.Font(bfTimes, 7, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

            Chunk title = new Chunk(Title, timesbold);
            Chunk description = new Chunk(Description, HelveticaNormal);
            Phrase phrase = new Phrase();
            phrase.Add(title);
            phrase.Add(description);
            return phrase;
        }

        public byte[] DownloadOPConLoaPdfReport(string _ClaimNo, DataSet ds)
        {
            // MemoryStream _stream = new MemoryStream();
            byte[] sFileByteStream = null;

            #region Get Data for Loa Report

            DataTable dtMemberInfo = ds.Tables[0];
            DataTable dtICD = ds.Tables[1];
            DataTable dtCPT = ds.Tables[2];


            #endregion

            string pdfTemplate = System.Web.Hosting.HostingEnvironment.MapPath(@"~\img\PdfReports\LOA\LOAOPCON.pdf");
            string img = System.Web.Hosting.HostingEnvironment.MapPath(@"~\img\PdfReports\LOA\MaxicareLogo.png");
            string imgDisapprove = System.Web.Hosting.HostingEnvironment.MapPath(@"~\img\PdfReports\LOA\DisapprovedPic.png");
            string imgApprove = System.Web.Hosting.HostingEnvironment.MapPath(@"~\img\PdfReports\LOA\ForApprovalPic.png");
            string imgPending = System.Web.Hosting.HostingEnvironment.MapPath(@"~\img\PdfReports\LOA\PendingPic.png");


            //string pdfTemplate = System.Web.Hosting.HostingEnvironment.MapPath(@"~\LoaPdfReports\LOAOPLAB.pdf");
            using (var _stream = new MemoryStream())
            {
                using (PdfStamper pdfStamper = new PdfStamper(new PdfReader(pdfTemplate), _stream))
                {
                    iTextSharp.text.pdf.PdfContentByte pdfPage = pdfStamper.GetOverContent(1);
                    AcroFields pdfFormFields = pdfStamper.AcroFields;


                    Image imgmaxi = Image.GetInstance(img);
                    imgmaxi.SetAbsolutePosition(50, 833);
                    imgmaxi.ScalePercent(43, 35);

                    char[] _sep = { '-' };




                    string _qrCodeText = dtMemberInfo.Rows[0]["CardNo"].ToString() + "-" + _ClaimNo + "-" + dtMemberInfo.Rows[0]["AvailmentDate"].ToString();

                    BarcodeQRCode _qrcode = new BarcodeQRCode(_qrCodeText, 1, 1, null);
                    Image imgQrCode = _qrcode.GetImage();
                    imgQrCode.Border = 0;
                    //(x Left - right + ,y up + down -)
                    //imgQrCode.SetAbsolutePosition(474, 820);
                    //imgQrCode.SetAbsolutePosition(455, 675);
                    //imgQrCode.SetAbsolutePosition(455, 665);
                    //imgQrCode.SetAbsolutePosition(465, 803);
                    //imgQrCode.SetAbsolutePosition(445, 725);
                    imgQrCode.SetAbsolutePosition(440, 843);
                    //imgQrCode.ScalePercent(275);
                    imgQrCode.ScalePercent(220);

                    var pdfContentByte = pdfStamper.GetOverContent(1);
                    pdfContentByte.AddImage(imgQrCode);
                    pdfContentByte.AddImage(imgmaxi);


                    Image imgwatermark = Image.GetInstance(imgApprove);
                    imgwatermark.SetAbsolutePosition(50, 290);
                    pdfContentByte.AddImage(imgwatermark);



                    pdfContentByte.BeginText();
                    BaseFont normal = BaseFont.CreateFont(BaseFont.HELVETICA, "Cp1252", false);
                    BaseFont headerFont = BaseFont.CreateFont(BaseFont.TIMES_BOLDITALIC, "Cp1252", false);
                    pdfContentByte.SetFontAndSize(normal, 7);
                    pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Main Office: Maxicare Tower, 203 Salcedo Street, Legaspi Village, Makati City", 300, 883, 0);
                    pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Toll - Free No.: 1-800-10-889-6294 | Call Center Toll - Free No.: 1-800-10-5821-900", 300, 875, 0);
                    pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Corporate Trunkline: 9086-900 | Call Center Hotline: 582-1900", 300, 867, 0);
                    pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Email: inquiry_customer_care@maxicare.com.ph | SMS Inquiry: 0918-889 MAXI (6294)", 300, 859, 0);
                    pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Homepage: http://www.maxicare.com.ph", 300, 851, 0);
                    pdfContentByte.SetFontAndSize(headerFont, 18);
                    pdfContentByte.SetColorFill(BaseColor.BLUE);
                    pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Maxicare Healthcare Corporation", 300, 893, 0);
                    pdfContentByte.EndText();


                    pdfFormFields.SetField("chkInitial", "1");


                    pdfFormFields.SetField("txtLoeNo", dtMemberInfo.Rows[0]["RequestNo"].ToString());
                    pdfFormFields.SetField("txtLoaNo", _ClaimNo);
                    pdfFormFields.SetField("txtDateIssued", Convert.ToDateTime(dtMemberInfo.Rows[0]["IssueDate"]).ToString("dd-MMM-yyyy"));
                    pdfFormFields.SetField("txtValidityDate", Convert.ToDateTime(dtMemberInfo.Rows[0]["IssueDate"]).ToString("dd-MMM-yyyy"));

                    pdfFormFields.SetField("txtPatientName", dtMemberInfo.Rows[0]["FullName"].ToString() + " - " + dtMemberInfo.Rows[0]["CardNo"].ToString());




                    string Contactinfo;
                    string MobileNo = dtMemberInfo.Rows[0]["ContactNo"].ToString();
                    string Email = dtMemberInfo.Rows[0]["EmailAddress"].ToString();

                    if (MobileNo == "" & Email != "")
                    {
                        Contactinfo = Email;
                    }
                    else if (MobileNo != "" & Email == "")
                    {
                        Contactinfo = MobileNo;
                    }
                    else if (MobileNo != "" & Email != "")
                    {
                        Contactinfo = MobileNo + "   -   " + Email;
                    }
                    else
                    {
                        Contactinfo = "";
                    }

                    pdfFormFields.SetField("txtContactInfo", Contactinfo);

                    pdfFormFields.SetField("txtCompany", dtMemberInfo.Rows[0]["CorpName"].ToString());
                    pdfFormFields.SetField("txtAttendingDoctor", dtMemberInfo.Rows[0]["PhysicianName"].ToString());
                    pdfFormFields.SetField("txtHospitalName", dtMemberInfo.Rows[0]["ClinicName"].ToString());
                    pdfFormFields.SetField("txtReferringDoctor", "");
                    pdfFormFields.SetField("txtSex", dtMemberInfo.Rows[0]["Gender"].ToString());
                    pdfFormFields.SetField("txtPlan", dtMemberInfo.Rows[0]["Plandesc"].ToString());
                    pdfFormFields.SetField("txtPolicyNo", dtMemberInfo.Rows[0]["CardNo"].ToString());

                    string EffectivityDate = Convert.ToDateTime(dtMemberInfo.Rows[0]["EffectiveDate"]).ToString("dd-MMM-yyyy");
                    string ExpiryDate = Convert.ToDateTime(dtMemberInfo.Rows[0]["ExpiryDate"]).ToString("dd-MMM-yyyy");
                    pdfFormFields.SetField("txtEffectivityExpiryDate", EffectivityDate + " - " + ExpiryDate);

                    pdfFormFields.SetField("txtApprovedBy", dtMemberInfo.Rows[0]["IssueBy"].ToString());
                    pdfFormFields.SetField("txtAvailmentDate", dtMemberInfo.Rows[0]["AvailmentDate"].ToString());
                    pdfFormFields.SetField("txtAge", dtMemberInfo.Rows[0]["Age"].ToString());


                    pdfFormFields.SetField("txtPreparedBy", dtMemberInfo.Rows[0]["IssueBy"].ToString());

                    if (dtICD.Rows.Count > 0)

                    {
                        string strICDDesc = "";
                        foreach (DataRow dr in dtICD.Rows)
                        {
                            strICDDesc += dr["ICDDesc"].ToString() + "\n";
                        }
                        pdfFormFields.SetField("txtChiefComplaint", strICDDesc);

                    }

                    pdfFormFields.SetField("chkRequired", "1");

                    pdfStamper.FormFlattening = true;
                    pdfStamper.Close();

                    //byte[] buffer = memoryStream.ToArray();
                    //string convertBuffer = Convert.ToBase64String(buffer);
                    //System.Web.HttpContext.Current.Response.OutputStream.Write(buffer, 0, buffer.Length);


                }

                sFileByteStream = _stream.ToArray();



            }
            return sFileByteStream;

        }
        public byte[] DownloadOPLabLoaPdfReport(string _ClaimNo, DataSet ds)
        {
            // MemoryStream _stream = new MemoryStream();
            byte[] sFileByteStream = null;


            #region Get Data for Loa Report

            DataTable dtMemberInfo = ds.Tables[0];
            DataTable dtICD = ds.Tables[1];
            DataTable dtCPT = ds.Tables[2];

            #endregion


            string pdfTemplate;

            if (dtCPT.Rows.Count <= 5)
            {
                pdfTemplate = System.Web.Hosting.HostingEnvironment.MapPath(@"~\img\PdfReports\LOA\LOAOPLAB_5.pdf");
            }
            else if (dtCPT.Rows.Count >= 6 && dtCPT.Rows.Count <= 10)
            {
                pdfTemplate = System.Web.Hosting.HostingEnvironment.MapPath(@"~\img\PdfReports\LOA\LOAOPLAB_10.pdf");
            }

            else if (dtCPT.Rows.Count >= 10 && dtCPT.Rows.Count <= 20)
            {
                pdfTemplate = System.Web.Hosting.HostingEnvironment.MapPath(@"~\img\PdfReports\LOA\LOAOPLAB_20.pdf");
            }

            else
            {
                pdfTemplate = System.Web.Hosting.HostingEnvironment.MapPath(@"~\img\PdfReports\LOA\LOAOPLAB_20.pdf");
            }

            string img = System.Web.Hosting.HostingEnvironment.MapPath(@"~\img\PdfReports\LOA\MaxicareLogo.png");

            //string pdfTemplate = System.Web.Hosting.HostingEnvironment.MapPath(@"~\LoaPdfReports\LOAOPLAB.pdf");
            using (var _stream = new MemoryStream())
            {
                using (PdfStamper pdfStamper = new PdfStamper(new PdfReader(pdfTemplate), _stream))
                {
                    iTextSharp.text.pdf.PdfContentByte pdfPage = pdfStamper.GetOverContent(1);
                    AcroFields pdfFormFields = pdfStamper.AcroFields;
                    Image imgmaxi = Image.GetInstance(img);
                    imgmaxi.SetAbsolutePosition(50, 833);
                    imgmaxi.ScalePercent(43, 35);

                    char[] _sep = { '-' };


                    //string sLOENO = _report.Tables["BASICINFO"].Rows[0]["LOENO"].ToString();
                    //string _amount = string.IsNullOrEmpty(_report.Tables["LOAINFO"].Rows[0]["ASSESSEDAMT"].ToString()) == true ? "" : _report.Tables["LOAINFO"].Rows[0]["ASSESSEDAMT"].ToString();

                    string _qrCodeText = dtMemberInfo.Rows[0]["CardNo"].ToString() + "-" + _ClaimNo + "-" + dtMemberInfo.Rows[0]["AvailmentDate"].ToString();

                    BarcodeQRCode _qrcode = new BarcodeQRCode(_qrCodeText, 0, 0, null);
                    Image imgQrCode = _qrcode.GetImage();
                    imgQrCode.Border = 0;


                    imgQrCode.SetAbsolutePosition(466, 843);

                    //imgQrCode.SetAbsolutePosition(300, 750);

                    //imgQrCode.ScalePercent(275);
                    imgQrCode.ScalePercent(220);

                    var pdfContentByte = pdfStamper.GetOverContent(1);
                    pdfContentByte.AddImage(imgQrCode);
                    pdfContentByte.AddImage(imgmaxi);


                    pdfContentByte.BeginText();
                    BaseFont normal = BaseFont.CreateFont(BaseFont.HELVETICA, "Cp1252", false);
                    BaseFont headerFont = BaseFont.CreateFont(BaseFont.TIMES_BOLDITALIC, "Cp1252", false);
                    pdfContentByte.SetFontAndSize(normal, 7);
                    pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Main Office: Maxicare Tower, 203 Salcedo Street, Legaspi Village, Makati City", 300, 883, 0);
                    pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Toll - Free No.: 1-800-10-889-6294 | Call Center Toll - Free No.: 1-800-10-5821-900", 300, 875, 0);
                    pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Corporate Trunkline: 9086-900 | Call Center Hotline: 582-1900", 300, 867, 0);
                    pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Email: inquiry_customer_care@maxicare.com.ph | SMS Inquiry: 0918-889 MAXI (6294)", 300, 859, 0);
                    pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Homepage: http://www.maxicare.com.ph", 300, 851, 0);
                    pdfContentByte.SetFontAndSize(headerFont, 18);
                    pdfContentByte.SetColorFill(BaseColor.BLUE);
                    pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Maxicare Healthcare Corporation", 300, 893, 0);
                    pdfContentByte.EndText();


                    string cptcode = "";
                    for (int y = 0; y < dtCPT.Rows.Count; y++)
                    {
                        cptcode = cptcode + dtCPT.Rows[y]["CPTCode"].ToString() + "\n";
                    }
                    string cptdesc = "";
                    for (int y = 0; y < dtCPT.Rows.Count; y++)
                    {
                        cptdesc = cptdesc + dtCPT.Rows[y]["CPTDesc"].ToString() + "\n";
                    }
                    string icdcode = "";
                    for (int y = 0; y < dtICD.Rows.Count; y++)
                    {
                        icdcode = icdcode + dtICD.Rows[y]["ICDCode"].ToString() + "\n";
                    }
                    string diagnosis = "";
                    for (int y = 0; y < dtICD.Rows.Count; y++)
                    {
                        diagnosis = diagnosis + dtICD.Rows[y]["ICDDesc"].ToString() + "\n";
                    }



                    pdfFormFields.SetField("txtLOENo", dtMemberInfo.Rows[0]["RequestNo"].ToString());
                    pdfFormFields.SetField("txtProvName", dtMemberInfo.Rows[0]["ClinicName"].ToString());
                    pdfFormFields.SetField("txtDoctor", dtMemberInfo.Rows[0]["PhysicianName"].ToString());
                    pdfFormFields.SetField("txtPatientName", dtMemberInfo.Rows[0]["FullName"].ToString());
                    pdfFormFields.SetField("txtMaxiNo", dtMemberInfo.Rows[0]["CardNo"].ToString());
                    pdfFormFields.SetField("txtAccountName", dtMemberInfo.Rows[0]["CorpName"].ToString());
                    pdfFormFields.SetField("txtAge", dtMemberInfo.Rows[0]["Age"].ToString());
                    pdfFormFields.SetField("txtSex", dtMemberInfo.Rows[0]["Gender"].ToString());
                    pdfFormFields.SetField("txtPlanNo", "");
                    pdfFormFields.SetField("txtPlanCode", dtMemberInfo.Rows[0]["PlanCode"].ToString());
                    //pdfFormFields.SetField("txtRoomNo", _report.Tables[0].Rows[0]["PLANCODE"].ToString());
                    pdfFormFields.SetField("txtRoomNo", "");
                    pdfFormFields.SetField("txtIssuedDate", Convert.ToDateTime(dtMemberInfo.Rows[0]["IssueDate"]).ToString("dd-MMM-yyyy"));
                    pdfFormFields.SetField("txtIssuedPlace", "");
                    //pdfFormFields.SetField("txtIssuedBy", _report.Tables[1].Rows[0]["ISSUEDBY"].ToString());
                    pdfFormFields.SetField("txtApprovedBy", dtMemberInfo.Rows[0]["IssueBy"].ToString());
                    pdfFormFields.SetField("txtValidityDate", Convert.ToDateTime(dtMemberInfo.Rows[0]["IssueDate"]).ToString("dd-MMM-yyyy"));
                    pdfFormFields.SetField("txtVisitDate", Convert.ToDateTime(dtMemberInfo.Rows[0]["AvailmentDate"]).ToString("dd-MMM-yyyy"));
                    pdfFormFields.SetField("txtProceduresCode", cptcode);
                    pdfFormFields.SetField("txtProceduresDesc", cptdesc);
                    pdfFormFields.SetField("txtICDCode", icdcode);
                    pdfFormFields.SetField("txtDiagnosis", diagnosis);

                    pdfStamper.FormFlattening = true;
                    pdfStamper.Close();

                    //byte[] buffer = memoryStream.ToArray();
                    //string convertBuffer = Convert.ToBase64String(buffer);
                    //System.Web.HttpContext.Current.Response.OutputStream.Write(buffer, 0, buffer.Length);


                }

                sFileByteStream = _stream.ToArray();


            }
            return sFileByteStream;

        }

    }
}