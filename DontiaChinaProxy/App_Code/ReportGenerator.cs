using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iText = iTextSharp.text;
using System.Web;
using System.Data;
using iTextSharp.text.pdf.draw;

namespace DontiaChinaProxy.App_Code
{
    public class ReportGenerator
    {
        public string BillingPdf(string _billno, string _corpCode, decimal _gtotal, string _LogNo)
        {
            var billing = new Document();

            //use a variable to let my code fit across the page...
            string path = HttpContext.Current.Server.MapPath("Document/Billing/");
            string imagepath = HttpContext.Current.Server.MapPath("img/Dontiacare_03.png");
            string date = DateTime.Now.ToShortDateString().Replace("/", "");
            string dir = path + _billno +'_'+ date + ".pdf";
            PdfWriter.GetInstance(billing, new FileStream(dir, FileMode.Create));

            #region get data for billing pdf
            DataAccessLayer _billingdata = new DataAccessLayer();
            DataTable _dt = _billingdata.ReportBillingData(_corpCode, _LogNo);

            //_report = _dbHandler.GetDataSet("sp_POSGetEligibilityInfo", ic);
            //_report.DataSetName = "LOEINFORMATION";
            //_report.Tables[0].TableName = "ELIGIBILITYINFO";
            #endregion 
            #region Fonts
            iText.Font HeaderFont = iText.FontFactory.GetFont(iText.FontFactory.TIMES_ROMAN, 18, iText.Font.BOLDITALIC);
            iText.Font dataFontBold = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 8f);
            iText.Font dataFontNormal = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 7f);
            iText.Font dataFontNormalUnderline = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 8f, iText.Font.UNDERLINE);
            iText.Font TitleFont = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 13f);
            iText.Font dataFontAssessedamt = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA_BOLD, 11f);
            iText.Font HeaderdataFont = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 7f, iText.BaseColor.WHITE);
            iText.Font dataFontDocumentNo = iText.FontFactory.GetFont(iText.FontFactory.HELVETICA, 5f);

            float[] widthHeader = new float[] { 22f, 35f, 35f, 35f, 45f, 25f, 25f };
            #endregion

            billing.Open();

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagepath);
            logo.ScaleToFit(150f, 250f);
            billing.Add(logo);

            #region header
            PdfPTable table = new PdfPTable(3);
            PdfPCell cell = new PdfPCell(new Phrase("Billing Report", HeaderFont));
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);
            billing.Add(table);
            #endregion

            #region information
            PdfPTable tableInfo = new PdfPTable(3);
            billing.Add(new Paragraph("Billing No: "+_billno));
            billing.Add(new Paragraph("Billing Date: " + DateTime.Now.ToShortDateString()));
            billing.Add(new Paragraph("Billing Account: " + _dt.Rows[1]["AccountName"].ToString() + "\n\n"));
            
            #endregion
            string z = string.Empty;
            int flag = _dt.Rows.Count;
            decimal tp = 0;
            decimal gTotal = 0;
            #region Input data
           
            for (int x = 0; x < flag; x++)
            {
                //PdfPTable tableData = new PdfPTable(7);
                //tableData.TotalWidth = 500f;
                //tableData.LockedWidth = true;
                //tableData.SetWidths(widthHeader);
                if (x != 0)
                {
                    int dummy = x + 1;
                    decimal que = Convert.ToDecimal(_dt.Rows[x]["Price"].ToString());
                    tp = tp + que;
                    gTotal = gTotal + que;

                    if (_dt.Rows[x]["LOGNo"].ToString() != "")
                    { //with border
                        PdfPTable tableData = new PdfPTable(7);
                        tableData.TotalWidth = 500f;
                        tableData.LockedWidth = true;
                        tableData.SetWidths(widthHeader);

                        Phrase _DataLogNo = new Phrase(_dt.Rows[x]["LOGNo"].ToString(), dataFontNormal);
                        PdfPCell _cellDataLogNo = new PdfPCell(_DataLogNo);
                        _cellDataLogNo.BorderWidthRight = 0;
                        _cellDataLogNo.BorderWidthBottom = 0;
                        _cellDataLogNo.HorizontalAlignment = 1;
                        tableData.AddCell(_cellDataLogNo);

                        Phrase _DataMember = new Phrase(_dt.Rows[x]["MemberName"].ToString(), dataFontNormal);
                        PdfPCell _cellDataMember = new PdfPCell(_DataMember);
                        _cellDataMember.BorderWidthBottom = 0;
                        _cellDataMember.BorderWidthLeft = 0;
                        _cellDataMember.BorderWidthRight = 0;
                        _cellDataMember.HorizontalAlignment = 0;
                        tableData.AddCell(_cellDataMember);

                        Phrase _DataAvailmentDate = new Phrase(_dt.Rows[x]["AvailmentDate"].ToString(), dataFontNormal);
                        PdfPCell _cellDataAvailmentDate = new PdfPCell(_DataAvailmentDate);
                        _cellDataAvailmentDate.BorderWidthBottom = 0;
                        _cellDataAvailmentDate.BorderWidthLeft = 0;
                        _cellDataAvailmentDate.BorderWidthRight = 0;
                        _cellDataAvailmentDate.HorizontalAlignment = 1;
                        tableData.AddCell(_cellDataAvailmentDate);

                        Phrase _DataProvider = new Phrase(_dt.Rows[x]["ServiceProvider"].ToString(), dataFontNormal);
                        PdfPCell _cellDataProvider = new PdfPCell(_DataProvider);
                        _cellDataProvider.HorizontalAlignment = 0;
                        _cellDataProvider.BorderWidthBottom = 0;
                        _cellDataProvider.BorderWidthLeft = 0;
                        _cellDataProvider.BorderWidthRight = 0;
                        tableData.AddCell(_cellDataProvider);

                        Phrase _DataTreatment = new Phrase(_dt.Rows[x]["TreatmentName"].ToString(), dataFontNormal);
                        PdfPCell _cellDataTreatment = new PdfPCell(_DataTreatment);
                        _cellDataTreatment.BorderWidthBottom = 0;
                        _cellDataTreatment.BorderWidthLeft = 0;
                        _cellDataTreatment.BorderWidthRight = 0;
                        _cellDataTreatment.HorizontalAlignment = 0;
                        tableData.AddCell(_cellDataTreatment);

                        Phrase _DataPrice = new Phrase(_dt.Rows[x]["Price"].ToString(), dataFontNormal);
                        PdfPCell _cellDataPrice = new PdfPCell(_DataPrice);
                        _cellDataPrice.BorderWidthBottom = 0;
                        _cellDataPrice.BorderWidthLeft = 0;
                        _cellDataPrice.BorderWidthRight = 0;
                        _cellDataPrice.HorizontalAlignment = 1;
                        tableData.AddCell(_cellDataPrice);

                        Phrase _DataTotal = new Phrase(null, dataFontNormal);
                        PdfPCell _cellTotal = new PdfPCell(_DataTotal);
                        _cellTotal.BorderWidthBottom = 0;
                        _cellTotal.BorderWidthLeft = 0;
                        _cellTotal.HorizontalAlignment = 1;
                        tableData.AddCell(_cellTotal);
                        billing.Add(tableData);
                    }
                    else
                    {
                        PdfPTable tableData = new PdfPTable(7);
                        tableData.TotalWidth = 500f;
                        tableData.LockedWidth = true;
                        tableData.SetWidths(widthHeader);

                        Phrase _DataLogNo = new Phrase(null, dataFontNormal);
                        PdfPCell _cellDataLogNo = new PdfPCell(_DataLogNo);
                        _cellDataLogNo.HorizontalAlignment = 1;
                        _cellDataLogNo.BorderWidthTop = 0;
                        _cellDataLogNo.BorderWidthRight = 0;
                        _cellDataLogNo.BorderWidthBottom = 0;
                        _cellDataLogNo.Colspan = 4;
                        tableData.AddCell(_cellDataLogNo);

                        Phrase _dataTreament = new Phrase(_dt.Rows[x]["TreatmentName"].ToString(), dataFontNormal);
                        PdfPCell _celltTreatment = new PdfPCell(_dataTreament);
                        _celltTreatment.BorderWidthTop = 0;
                        _celltTreatment.BorderWidthLeft = 0;
                        _celltTreatment.BorderWidthRight = 0;
                        _celltTreatment.BorderWidthBottom = 0;
                        tableData.AddCell(_celltTreatment);

                        Phrase _DataPrice = new Phrase(_dt.Rows[x]["Price"].ToString(), dataFontNormal);
                        PdfPCell _cellDataPrice = new PdfPCell(_DataPrice);
                        _cellDataPrice.HorizontalAlignment = 1;
                        _cellDataPrice.BorderWidthTop = 0;
                        _cellDataPrice.BorderWidthLeft = 0;
                        _cellDataPrice.BorderWidthRight = 0;
                        _cellDataPrice.BorderWidthBottom = 0;
                        tableData.AddCell(_cellDataPrice);

                        Phrase _DataTotal = new Phrase(null, dataFontNormal);
                        PdfPCell _cellTotal = new PdfPCell(_DataTotal);
                        _cellTotal.HorizontalAlignment = 1;
                        _cellTotal.BorderWidthTop = 0;
                        _cellTotal.BorderWidthLeft = 0;
                        _cellTotal.BorderWidthBottom = 0;
                        tableData.AddCell(_cellTotal);
                        billing.Add(tableData);

                        //T0talPrice
                        PdfPTable tableData1 = new PdfPTable(7);
                        tableData1.TotalWidth = 500f;
                        tableData1.LockedWidth = true;
                        tableData1.SetWidths(widthHeader);

                        Phrase _DatacolSPan = new Phrase(null, dataFontNormal);
                        PdfPCell _cellSPan = new PdfPCell(_DatacolSPan);
                        _cellSPan.HorizontalAlignment = 1;
                        _cellSPan.Colspan = 6;
                        _cellSPan.BorderWidthRight = 0;
                        _cellSPan.BorderWidthTop = 0;
                        _cellSPan.BorderWidthBottom = 0;
                        tableData1.AddCell(_cellSPan);

                        Phrase _DataGTOtal = new Phrase(tp.ToString(), dataFontNormal);
                        PdfPCell _cellGTotal = new PdfPCell(_DataGTOtal);
                        _cellGTotal.HorizontalAlignment = 1;
                        _cellGTotal.BorderWidthLeft = 0;
                        _cellGTotal.BorderWidthTop = 0;
                        _cellGTotal.BorderWidthBottom = 0;
                        tableData1.AddCell(_cellGTotal);
                        billing.Add(tableData1);
                        tp = 0;
                    }

                }
                else
                {
                    PdfPTable tableData = new PdfPTable(7);
                    tableData.TotalWidth = 500f;
                    tableData.LockedWidth = true;
                    tableData.SetWidths(widthHeader);

                    Phrase _DataLogNo = new Phrase(_dt.Rows[x]["LOGNo"].ToString(), dataFontNormal);
                    PdfPCell _cellDataLogNo = new PdfPCell(_DataLogNo);
                    _cellDataLogNo.BackgroundColor = new iTextSharp.text.BaseColor(238, 108, 23);
                    _cellDataLogNo.BorderWidthBottom = 0;
                    _cellDataLogNo.BorderWidthRight = 0;
                    _cellDataLogNo.HorizontalAlignment = 1;
                    tableData.AddCell(_cellDataLogNo);

                    Phrase _DataMember = new Phrase(_dt.Rows[x]["MemberName"].ToString(), dataFontNormal);
                    PdfPCell _cellDataMember = new PdfPCell(_DataMember);
                    _cellDataMember.BackgroundColor = new iTextSharp.text.BaseColor(238, 108, 23);
                    _cellDataMember.BorderWidthBottom = 0;
                    _cellDataMember.HorizontalAlignment = 1;
                    tableData.AddCell(_cellDataMember);

                    Phrase _DataAvailmentDate = new Phrase(_dt.Rows[x]["AvailmentDate"].ToString(), dataFontNormal);
                    PdfPCell _cellDataAvailmentDate = new PdfPCell(_DataAvailmentDate);
                    _cellDataAvailmentDate.BackgroundColor = new iTextSharp.text.BaseColor(238, 108, 23);
                    _cellDataAvailmentDate.BorderWidthBottom = 0;
                    _cellDataAvailmentDate.HorizontalAlignment = 1;
                    tableData.AddCell(_cellDataAvailmentDate);

                    Phrase _DataProvider = new Phrase(_dt.Rows[x]["ServiceProvider"].ToString(), dataFontNormal);
                    PdfPCell _cellDataProvider = new PdfPCell(_DataProvider);
                    _cellDataProvider.BackgroundColor = new iTextSharp.text.BaseColor(238, 108, 23);
                    _cellDataProvider.BorderWidthBottom = 0;
                    _cellDataProvider.HorizontalAlignment = 1;
                    tableData.AddCell(_cellDataProvider);

                    Phrase _DataTreatment = new Phrase(_dt.Rows[x]["TreatmentName"].ToString(), dataFontNormal);
                    PdfPCell _cellDataTreatment = new PdfPCell(_DataTreatment);
                    _cellDataTreatment.BackgroundColor = new iTextSharp.text.BaseColor(238, 108, 23);
                    _cellDataTreatment.BorderWidthBottom = 0;
                    _cellDataTreatment.HorizontalAlignment = 1;
                    tableData.AddCell(_cellDataTreatment);

                    Phrase _DataPrice = new Phrase(_dt.Rows[x]["Price"].ToString(), dataFontNormal);
                    PdfPCell _cellDataPrice = new PdfPCell(_DataPrice);
                    _cellDataPrice.BackgroundColor = new iTextSharp.text.BaseColor(238, 108, 23);
                    _cellDataPrice.BorderWidthBottom = 0;
                    _cellDataPrice.HorizontalAlignment = 1;
                    tableData.AddCell(_cellDataPrice);

                    Phrase _DataTotalBill = new Phrase("Total Price", dataFontNormal);
                    PdfPCell _cellDataTotalBill = new PdfPCell(_DataTotalBill);
                    _cellDataTotalBill.BackgroundColor = new iTextSharp.text.BaseColor(238, 108, 23);
                    _cellDataTotalBill.BorderWidthBottom = 0;
                    _cellDataTotalBill.HorizontalAlignment = 1;
                    tableData.AddCell(_cellDataTotalBill);
                    billing.Add(tableData);
                }

            }
            //T0talPrice
            PdfPTable tableData2 = new PdfPTable(7);
            tableData2.TotalWidth = 500f;
            tableData2.LockedWidth = true;
            tableData2.SetWidths(widthHeader);

            Phrase _Datacollast = new Phrase(null, dataFontNormal);
            PdfPCell _cellSPanlast = new PdfPCell(_Datacollast);
            _cellSPanlast.HorizontalAlignment = 1;
            _cellSPanlast.Colspan = 6;
            _cellSPanlast.BorderWidthRight = 0;
            _cellSPanlast.BorderWidthBottom = 0;
            _cellSPanlast.BorderWidthTop = 0;
            tableData2.AddCell(_cellSPanlast);

            Phrase _DataGTOtallast = new Phrase(tp.ToString(), dataFontNormal);
            PdfPCell _cellGTotallast = new PdfPCell(_DataGTOtallast);
            _cellGTotallast.HorizontalAlignment = 1;
            _cellGTotallast.BorderWidthLeft = 0;
            _cellGTotallast.BorderWidthBottom = 0;
            _cellGTotallast.BorderWidthTop = 0;
            tableData2.AddCell(_cellGTotallast);
            billing.Add(tableData2);
            tp = 0;
            ////Grand Total
            PdfPTable tableGT = new PdfPTable(7);
            tableGT.TotalWidth = 500f;
            tableGT.LockedWidth = true;
            tableGT.SetWidths(widthHeader);

            Phrase _Datagt = new Phrase(null, dataFontNormal);
            PdfPCell _cellgt = new PdfPCell(_Datagt);
            _cellgt.HorizontalAlignment = 1;
            _cellgt.BorderWidthLeft = 0;
            _cellgt.BorderWidthBottom = 0;
            _cellgt.BorderWidthRight = 0;
            _cellgt.Colspan = 6;
            tableGT.AddCell(_cellgt);

            Phrase _Datagtshow = new Phrase(gTotal.ToString() + "\n", dataFontNormal);
            PdfPCell _cellgtshow = new PdfPCell(_Datagtshow);
            _cellgtshow.HorizontalAlignment = 1;
            _cellgtshow.BorderWidthLeft = 0;
            _cellgtshow.BorderWidthRight = 0;
            tableGT.AddCell(_cellgtshow);
            billing.Add(tableGT);

            PdfPTable tableline = new PdfPTable(7);
            tableline.TotalWidth = 500f;
            tableline.LockedWidth = true;
            tableline.SpacingBefore = 2;
            tableline.SetWidths(widthHeader);

            Phrase _DataLine = new Phrase(null, dataFontNormal);
            PdfPCell _cellline2 = new PdfPCell(_DataLine);
            _cellline2.HorizontalAlignment = 1;
            _cellline2.Border = 0;
            _cellline2.Colspan = 6;
            tableline.AddCell(_cellline2);

            Phrase _DatagLine2 = new Phrase(null, dataFontNormalUnderline);
            PdfPCell _cellline3 = new PdfPCell(_DatagLine2);
            _cellline3.HorizontalAlignment = 1;
            _cellline3.BorderWidthLeft = 0;
            _cellline3.BorderWidthBottom = 0;
            _cellline3.BorderWidthRight = 0;
            tableline.AddCell(_cellline3);
            billing.Add(tableline);

            #endregion
            billing.Close();
            return dir;
        }
    }
}
