using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using gbmodel = gb.mbs.da.model;
using gb.mbs.da.common;
using System.Text.RegularExpressions;

namespace gb.mbs.da.services.bill
{
    public class SrvPOM
    {
        private string sSQLCon = dataaccess.ConnectionManager.GetConnectionString(null);

        public void PrintPOM(DataSet p_oDSGroupData, bool p_isCombinedPOM, int p_iPOMID, string p_sFilePath, gbmodel.patient.Patient p_oPatient)
        {
            string officeName = "";
            string officeAddress = "";
            DataSet dsOffice = GetOfficeDetails(p_oPatient.Account.ID);

            if (dsOffice.Tables[0] != null && dsOffice.Tables[0].Rows.Count != 0 && dsOffice.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_oDSGroupData.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < dsOffice.Tables[0].Rows.Count; j++)
                    {
                        if (p_oDSGroupData.Tables[0].Rows[i]["Doctor ID"].ToString() == dsOffice.Tables[0].Rows[j]["Doctor ID"].ToString())
                        {
                            p_oDSGroupData.Tables[0].Rows[i]["Reffering Office"] = dsOffice.Tables[0].Rows[j]["Office ID"].ToString();
                            p_oDSGroupData.Tables[0].Rows[i]["Office Address"] = dsOffice.Tables[0].Rows[j]["Office Address"].ToString();
                            p_oDSGroupData.Tables[0].Rows[i]["Office City"] = dsOffice.Tables[0].Rows[j]["Office City"].ToString();
                            p_oDSGroupData.Tables[0].Rows[i]["Office State"] = dsOffice.Tables[0].Rows[j]["Office State"].ToString();
                            p_oDSGroupData.Tables[0].Rows[i]["Office Zip"] = dsOffice.Tables[0].Rows[j]["Office Zip"].ToString();
                        }
                    }
                }
            }

            string[] columns = { "Reffering Office", "Provider", "Office Address", "Office City", "Office State", "Office Zip" };

            DataTable dtDistinctProvider = SelectDistinctProvider(p_oDSGroupData.Tables[0], columns);
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 15, 15, 20, 20);
            MemoryStream m = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, m);

            document.Open();

            DataTable dtCaseType = new DataTable();
            dtCaseType.Columns.Add("sz_case_type_abbreviation_id");

            DataRow dr = dtCaseType.NewRow();
            dr["sz_case_type_abbreviation_id"] = "WC000000000000000001";
            dtCaseType.Rows.Add(dr);

            dr = dtCaseType.NewRow();
            dr["sz_case_type_abbreviation_id"] = "WC000000000000000002";
            dtCaseType.Rows.Add(dr);

            #region if NOT Combined POM
            if (!p_isCombinedPOM)
            {
                for (int i = 0; i < dtDistinctProvider.Rows.Count; i++)
                {
                    int lineNumber = 0;
                    //todo: break on each different provider

                    float[] wtblBase = { 4f };
                    PdfPTable tblMain = new PdfPTable(wtblBase);
                    tblMain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblMain.WidthPercentage = 100;

                    float[] wdPomId = { 4f };
                    PdfPTable tblPomId = new PdfPTable(wdPomId);
                    tblPomId.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblPomId.WidthPercentage = 100;
                    if (p_iPOMID != 0)
                    {
                        tblPomId.AddCell(new Phrase("POM ID: " + p_iPOMID, iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                    }
                    else
                    {
                        tblPomId.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                    }
                    officeName = dtDistinctProvider.Rows[i]["Provider"].ToString();
                    officeAddress = "\n " + dtDistinctProvider.Rows[i]["Office Address"].ToString() + "\n" + dtDistinctProvider.Rows[i]["Office City"].ToString() + "\n" + dtDistinctProvider.Rows[i]["Office State"].ToString() + " " + dtDistinctProvider.Rows[i]["Office Zip"].ToString(); ;

                    float[] wd1 = { 1f, 2f, 3f, 3f, 2.5f };
                    PdfPTable tblHeader = new PdfPTable(wd1);
                    tblHeader.WidthPercentage = 100;
                    tblHeader.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                    tblHeader = GetTableHeader(wd1, officeName, officeAddress);

                    float[] wd2 = { 1f, 1.7f, 4f, 1.5f, 1f, 2.5f, 3f, 2.8f, 3f, 1f, 2f, 1f, 3f };
                    PdfPTable tblPOM = new PdfPTable(wd2);
                    tblPOM.WidthPercentage = 100;
                    tblPOM.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;

                    float[] wdTemp = { 1f, 1.7f, 4f, 1.5f, 1f, 2.5f, 3f, 2.8f, 3f, 1f, 2f, 1f, 3f };
                    PdfPTable tblTemp = new PdfPTable(wdTemp);
                    tblTemp.WidthPercentage = 100;
                    tblTemp.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;

                    float[] wdFooter = { 1f, 2f, 2f, 8f };
                    PdfPTable tblFooter = new PdfPTable(wdFooter);
                    tblFooter.WidthPercentage = 100;
                    tblFooter.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                    tblFooter = GetFooterTable(wdFooter);
                    //todo: add data table header
                    bool isHeaderAdded = false;
                    float[] wdNFPOM = { 1f, 1.5f, 4f, 1.5f, 1f, 2.5f, 3f, 2.8f, 3f, 1f, 2f, 1f, 3f };
                    PdfPTable tblNFPOM = null;
                    for (int j = 0; j < dtCaseType.Rows.Count; j++)
                    {
                        string sCaseTypeAbbreviationID = dtCaseType.Rows[j]["sz_case_type_abbreviation_id"].ToString();

                        DataTable dtPrintRows = null; // get dataset from original dataset where office_id (provider) = loop.provider AND case type = WC...001
                        DataRow[] result = p_oDSGroupData.Tables[0].Select("[Reffering Office] = '" + dtDistinctProvider.Rows[i]["Reffering Office"].ToString() + "' AND [CaseType] = '" + sCaseTypeAbbreviationID + "' ");

                        dtPrintRows = new DataTable();
                        dtPrintRows.Columns.Add("Bill ID");
                        dtPrintRows.Columns.Add("Case Id");
                        dtPrintRows.Columns.Add("Patient Name");
                        dtPrintRows.Columns.Add("Spciality");
                        dtPrintRows.Columns.Add("Reffering Office");
                        dtPrintRows.Columns.Add("Bill Amount");
                        dtPrintRows.Columns.Add("Bill Date");
                        dtPrintRows.Columns.Add("Claim Number");
                        dtPrintRows.Columns.Add("Insurance Company");
                        dtPrintRows.Columns.Add("Insurance Address");
                        dtPrintRows.Columns.Add("Min Date Of Service");
                        dtPrintRows.Columns.Add("Max Date Of Service");
                        dtPrintRows.Columns.Add("CaseType");
                        dtPrintRows.Columns.Add("WC_ADDRESS");
                        dtPrintRows.Columns.Add("Case #");
                        dtPrintRows.Columns.Add("Provider");
                        dtPrintRows.Columns.Add("Office Address");
                        dtPrintRows.Columns.Add("Office City");
                        dtPrintRows.Columns.Add("Office State");
                        dtPrintRows.Columns.Add("Office Zip");

                        foreach (DataRow row in result)
                        {
                            dtPrintRows.ImportRow(row);
                        }

                        DataSet dataSet = new DataSet();
                        dataSet.Tables.Add(dtPrintRows);
                        tblTemp = GetPOMHeader(wd2);

                        switch (sCaseTypeAbbreviationID)
                        {
                            case "WC000000000000000001":
                                //for loop for printing all WC with WC Board
                                tblNFPOM = null;
                                if (dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count == 0)
                                {
                                    continue;
                                }

                                for (int k = 0; k < dataSet.Tables[0].Rows.Count; k++)
                                {
                                    var normalFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                    var boldFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                                    Phrase phraseInsurance = new Phrase();
                                    phraseInsurance.Add(new Chunk("Worker's Compensation Board" + "\n", boldFont));
                                    phraseInsurance.Add(new Chunk(dataSet.Tables[0].Rows[k]["WC_ADDRESS"].ToString(), normalFont));
                                    lineNumber = lineNumber + 1;
                                    string pName = dataSet.Tables[0].Rows[k]["Patient Name"].ToString();
                                    string sPatientName = Regex.Replace(pName, ",{2,}", ",").Trim(',');

                                    RegexOptions options = RegexOptions.None;
                                    Regex regex = new Regex("[ ]{2,}", options);
                                    sPatientName = regex.Replace(sPatientName, " ");

                                    string[] patientName = null;
                                    if (sPatientName.Contains(","))
                                    {
                                        patientName = sPatientName.Split(',');
                                    }
                                    else
                                    {
                                        patientName = sPatientName.Split(' ');
                                    }

                                    tblPOM.AddCell(new Phrase(lineNumber.ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Claim Number"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(phraseInsurance));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Spciality"].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Min Date Of Service"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase("$ " + dataSet.Tables[0].Rows[k]["Bill Amount"].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Max Date Of Service"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase("", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Bill ID"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(patientName[1].ToString() + " , " + patientName[0].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Case #"].ToString(), GetDataTableRowFont()));

                                    if (k % 13 == 0 && k != 0)
                                    {
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblMain.AddCell(tblTemp);
                                        tblMain.AddCell(tblPOM);
                                        tblMain.AddCell(tblFooter);
                                        document.Add(tblMain);
                                        tblPOM.FlushContent();
                                        tblMain.FlushContent();
                                        document.NewPage();
                                    }
                                }
                                tblMain.AddCell(tblPomId);
                                tblMain.AddCell(tblHeader);
                                tblMain.AddCell(tblTemp);
                                tblMain.AddCell(tblPOM);
                                tblMain.AddCell(tblFooter);
                                document.Add(tblMain);
                                tblPOM.FlushContent();
                                tblMain.FlushContent();
                                document.NewPage();

                                //loop for printing all WC with respective insurance
                                lineNumber = 0;

                                for (int k = 0; k < dataSet.Tables[0].Rows.Count; k++)
                                {
                                    var normalFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                    var boldFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                                    Phrase phraseInsurance = new Phrase();
                                    phraseInsurance.Add(new Chunk(dataSet.Tables[0].Rows[k]["Insurance Company"].ToString() + "\n", boldFont));
                                    phraseInsurance.Add(new Chunk(dataSet.Tables[0].Rows[k]["Insurance Address"].ToString(), normalFont));

                                    lineNumber = lineNumber + 1;
                                    string pName = dataSet.Tables[0].Rows[k]["Patient Name"].ToString();

                                    string sPatientName = Regex.Replace(pName, ",{2,}", ",").Trim(',');

                                    RegexOptions options = RegexOptions.None;
                                    Regex regex = new Regex("[ ]{2,}", options);
                                    sPatientName = regex.Replace(sPatientName, " ");

                                    string[] patientName = null;
                                    if (sPatientName.Contains(","))
                                    {
                                        patientName = sPatientName.Split(',');
                                    }
                                    else
                                    {
                                        patientName = sPatientName.Split(' ');
                                    }

                                    tblPOM.AddCell(new Phrase(lineNumber.ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Claim Number"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(phraseInsurance));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Spciality"].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Min Date Of Service"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase("$ " + dataSet.Tables[0].Rows[k]["Bill Amount"].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Max Date Of Service"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase("", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Bill ID"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(patientName[1].ToString() + " , " + patientName[0].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Case #"].ToString(), GetDataTableRowFont()));
                                    isHeaderAdded = false;
                                    if (k % 13 == 0 && k != 0)
                                    {
                                        isHeaderAdded = true;
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblMain.AddCell(tblTemp);
                                        tblMain.AddCell(tblPOM);
                                        tblMain.AddCell(tblFooter);
                                        document.Add(tblMain);
                                        tblPOM.FlushContent();
                                        tblMain.FlushContent();
                                        document.NewPage();
                                    }
                                }

                                DataRow[] rsNF = p_oDSGroupData.Tables[0].Select("[Reffering Office] = '" + dtDistinctProvider.Rows[i]["Reffering Office"].ToString() + "' AND [CaseType] = 'WC000000000000000002'");

                                if (rsNF != null && rsNF.Length > 0)
                                {
                                    // there are NF records still to be printed in the same table. do not flush the currect WC records to PDF
                                    tblNFPOM = tblPOM;
                                    tblNFPOM.WidthPercentage = 100;
                                    tblNFPOM.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                                }
                                else
                                {
                                    if (!isHeaderAdded)
                                    {
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblMain.AddCell(tblTemp);
                                    }
                                    tblMain.AddCell(tblPOM);
                                    tblMain.AddCell(tblFooter);
                                    document.Add(tblMain);
                                    tblPOM.FlushContent();
                                    tblMain.FlushContent();
                                    document.NewPage();
                                }

                                break;
                            case "WC000000000000000002":
                                if (tblNFPOM == null)
                                {
                                    tblNFPOM = new PdfPTable(wdNFPOM);
                                    tblNFPOM.WidthPercentage = 100;
                                    tblNFPOM.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                                }

                                bool hasNFRecords = false;
                                for (int k = 0; k < dataSet.Tables[0].Rows.Count; k++)
                                {
                                    var normalFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                    var boldFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                                    Phrase phraseInsurance = new Phrase();
                                    phraseInsurance.Add(new Chunk(dataSet.Tables[0].Rows[k]["Insurance Company"].ToString() + "\n", boldFont));
                                    phraseInsurance.Add(new Chunk(dataSet.Tables[0].Rows[k]["Insurance Address"].ToString(), normalFont));
                                    lineNumber = lineNumber + 1;
                                    hasNFRecords = true;

                                    string pName = dataSet.Tables[0].Rows[k]["Patient Name"].ToString();
                                    string sPatientName = Regex.Replace(pName, ",{2,}", ",").Trim(',');

                                    RegexOptions options = RegexOptions.None;
                                    Regex regex = new Regex("[ ]{2,}", options);
                                    sPatientName = regex.Replace(sPatientName, " ");

                                    string[] patientName = null;
                                    if (sPatientName.Contains(","))
                                    {
                                        patientName = sPatientName.Split(',');
                                    }
                                    else
                                    {
                                        patientName = sPatientName.Split(' ');
                                    }

                                    tblNFPOM.AddCell(new Phrase(lineNumber.ToString(), GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Claim Number"].ToString(), GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(phraseInsurance));
                                    tblNFPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Spciality"].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Min Date Of Service"].ToString(), GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase("$ " + dataSet.Tables[0].Rows[k]["Bill Amount"].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Max Date Of Service"].ToString(), GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase("", GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Bill ID"].ToString(), GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(patientName[1].ToString() + " , " + patientName[0].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Case #"].ToString(), GetDataTableRowFont()));

                                    if (k % 13 == 0 && k != 0)
                                    {
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblPOM = GetPOMHeader(wd2);
                                        tblMain.AddCell(tblPOM);
                                        tblMain.AddCell(tblNFPOM);
                                        tblMain.AddCell(tblFooter);
                                        document.Add(tblMain);
                                        tblNFPOM.FlushContent();
                                        tblPOM.FlushContent();
                                        tblMain.FlushContent();
                                        document.NewPage();
                                    }
                                }

                                if (hasNFRecords)
                                {
                                    if (tblNFPOM.Rows.Count == 0)
                                    {
                                    }
                                    else
                                    {
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblPOM = GetPOMHeader(wd2);
                                        tblMain.AddCell(tblPOM);
                                        tblMain.AddCell(tblNFPOM);
                                        tblMain.AddCell(tblFooter);
                                        document.Add(tblMain);
                                        tblPOM.FlushContent();
                                        tblMain.FlushContent();
                                        document.NewPage();
                                    }
                                }
                                hasNFRecords = false;
                                break;
                        }
                    }
                }
            }
            #endregion

            #region if Combined POM
            else
            {
                ArrayList getBillNumbers = new ArrayList();
                DataTable dt = new DataTable();
                bool isHeaderAdded = false;
                for (int i = 0; i < dtDistinctProvider.Rows.Count; i++)
                {
                    int lineNumber = 0;
                    //todo: break on each different provider

                    float[] wtblBase = { 4f };
                    PdfPTable tblMain = new PdfPTable(wtblBase);
                    tblMain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblMain.WidthPercentage = 100;

                    float[] wdPomId = { 4f };
                    PdfPTable tblPomId = new PdfPTable(wdPomId);
                    tblPomId.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblPomId.WidthPercentage = 100;
                    if (p_iPOMID != 0)
                    {
                        tblPomId.AddCell(new Phrase("POM ID: " + p_iPOMID, iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                    }
                    else
                    {
                        tblPomId.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                    }

                    officeName = dtDistinctProvider.Rows[i]["Provider"].ToString();
                    officeAddress = "\n" + dtDistinctProvider.Rows[i]["Office Address"].ToString() + "\n" + dtDistinctProvider.Rows[i]["Office City"].ToString() + "\n" + dtDistinctProvider.Rows[i]["Office State"].ToString() + " " + dtDistinctProvider.Rows[i]["Office Zip"].ToString();
                    float[] wd1 = { 1f, 2f, 3f, 3f, 2.5f };
                    PdfPTable tblHeader = new PdfPTable(wd1);
                    tblHeader.WidthPercentage = 100;
                    tblHeader.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                    tblHeader = GetTableHeader(wd1, officeName, officeAddress);

                    float[] wd2 = { 0.6f, 1.8f, 4f, 1.2f, 1f, 2f, 4f, 1, 2.5f };
                    PdfPTable tblPOM = new PdfPTable(wd2);
                    tblPOM.WidthPercentage = 100;
                    tblPOM.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;

                    float[] wdTemp = { 0.6f, 1.8f, 4f, 1.2f, 1f, 2f, 4f, 1, 2.5f };
                    PdfPTable tblTemp = new PdfPTable(wdTemp);
                    tblTemp.WidthPercentage = 100;
                    tblTemp.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;

                    float[] wdFooter = { 1f, 2f, 2f, 8f };
                    PdfPTable tblFooter = new PdfPTable(wdFooter);
                    tblFooter.WidthPercentage = 100;
                    tblFooter.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                    tblFooter = GetFooterTable(wdFooter);
                    //todo: add data table header

                    float[] wdNFPOM = { 0.6f, 1.8f, 4f, 1.2f, 1f, 2f, 4f, 1f, 2.5f };
                    PdfPTable tblNFPOM = null;
                    bool hasNFOnly = false;
                    for (int j = 0; j < dtCaseType.Rows.Count; j++)
                    {
                        string sCaseTypeAbbreviationID = dtCaseType.Rows[j]["sz_case_type_abbreviation_id"].ToString();

                        DataTable dtPrintRows = null; // get dataset from original dataset where office_id (provider) = loop.provider AND case type = WC...001

                        DataRow[] result = p_oDSGroupData.Tables[0].Select("[Reffering Office] = '" + dtDistinctProvider.Rows[i]["Reffering Office"].ToString() + "' AND [CaseType] = '" + sCaseTypeAbbreviationID + "' ");

                        dtPrintRows = new DataTable();
                        dtPrintRows.Columns.Add("Bill ID");
                        dtPrintRows.Columns.Add("Case Id");
                        dtPrintRows.Columns.Add("Patient Name");
                        dtPrintRows.Columns.Add("Spciality");
                        dtPrintRows.Columns.Add("Reffering Office");
                        dtPrintRows.Columns.Add("Bill Amount");
                        dtPrintRows.Columns.Add("Bill Date");
                        dtPrintRows.Columns.Add("Claim Number");
                        dtPrintRows.Columns.Add("Insurance Company");
                        dtPrintRows.Columns.Add("Insurance Address");
                        dtPrintRows.Columns.Add("Min Date Of Service");
                        dtPrintRows.Columns.Add("Max Date Of Service");
                        dtPrintRows.Columns.Add("CaseType");
                        dtPrintRows.Columns.Add("WC_ADDRESS");
                        dtPrintRows.Columns.Add("Case #");
                        dtPrintRows.Columns.Add("Provider");
                        dtPrintRows.Columns.Add("Office Address");
                        dtPrintRows.Columns.Add("Office City");
                        dtPrintRows.Columns.Add("Office State");
                        dtPrintRows.Columns.Add("Office Zip");

                        foreach (DataRow row in result)
                        {
                            dtPrintRows.ImportRow(row);
                        }

                        DataSet dataSet = new DataSet();
                        dataSet.Tables.Add(dtPrintRows);
                        tblTemp = GetMultiplePOMHeader(wdTemp);
                        switch (sCaseTypeAbbreviationID)
                        {
                            case "WC000000000000000001":
                                tblNFPOM = null;
                                if (dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count == 0)
                                {
                                    hasNFOnly = true;
                                    continue;
                                }
                                string[] columnsNames = { "Reffering Office", "CaseType", "Case Id", "Claim Number", "Insurance Company", "Insurance Address", "WC_ADDRESS", "Patient Name", "Case #" };
                                dt = new DataTable();
                                dt = GetDistinctRecords(dataSet.Tables[0], columnsNames);
                                string sInsuranceNameAddress = "";
                                DataRow[] rsNF = p_oDSGroupData.Tables[0].Select("[Reffering Office] = '" + dtDistinctProvider.Rows[i]["Reffering Office"].ToString() + "' AND [CaseType] = 'WC000000000000000002'");
                                int iCount = 0;
                                for (int k = 0; k < dt.Rows.Count; k++)
                                {
                                    getBillNumbers = GetBillNumbers(p_oDSGroupData, dataSet, dt.Rows[k]["Reffering Office"].ToString(), dt.Rows[k]["CaseType"].ToString(), dt.Rows[k]["Case #"].ToString());
                                    for (int count = 0; count < getBillNumbers.Count; count++)
                                    {
                                        iCount += StringExtensions.LineCount(getBillNumbers[count].ToString());
                                    }
                                    if (getBillNumbers.Count > 0)
                                    {
                                        for (int c = 0; c < getBillNumbers.Count; c++)
                                        {
                                            var normalFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                            var boldFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                                            Phrase phraseInsurance = new Phrase();
                                            phraseInsurance.Add(new Chunk("Workers Compensation Board" + "\n", boldFont));
                                            phraseInsurance.Add(new Chunk(dt.Rows[k]["WC_ADDRESS"].ToString(), normalFont));

                                            lineNumber = lineNumber + 1;
                                            string pName = dt.Rows[k]["Patient Name"].ToString();
                                            string sPatientName = Regex.Replace(pName, ",{2,}", ",").Trim(',');

                                            RegexOptions options = RegexOptions.None;
                                            Regex regex = new Regex("[ ]{2,}", options);
                                            sPatientName = regex.Replace(sPatientName, " ");

                                            string[] patientName = null;
                                            if (sPatientName.Contains(","))
                                            {
                                                patientName = sPatientName.Split(',');
                                            }
                                            else
                                            {
                                                patientName = sPatientName.Split(' ');
                                            }

                                            tblPOM.AddCell(new Phrase(lineNumber.ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(dt.Rows[k]["Claim Number"].ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(phraseInsurance));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(getBillNumbers[c].ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(patientName[1].ToString() + " , " + patientName[0].ToString() + "\n" + dt.Rows[k]["Case #"].ToString(), GetMultiplePOMDataTableRowFont()));

                                            if (iCount >= 13)
                                            {
                                                tblMain.AddCell(tblPomId);
                                                tblMain.AddCell(tblHeader);
                                                tblMain.AddCell(tblTemp);
                                                tblMain.AddCell(tblPOM);
                                                tblMain.AddCell(tblFooter);
                                                document.Add(tblMain);
                                                tblPOM.FlushContent();
                                                tblMain.FlushContent();
                                                document.NewPage();
                                                iCount = 0;
                                            }
                                        }
                                    }

                                }
                                tblMain.AddCell(tblPomId);
                                tblMain.AddCell(tblHeader);
                                tblMain.AddCell(tblTemp);
                                tblMain.AddCell(tblPOM);
                                tblMain.AddCell(tblFooter);
                                document.Add(tblMain);
                                tblPOM.FlushContent();
                                tblMain.FlushContent();
                                document.NewPage();

                                if (hasNFOnly)
                                {
                                    tblMain.AddCell(tblPomId);
                                    tblMain.AddCell(tblHeader);
                                    tblPOM = GetMultiplePOMHeader(wd2);
                                    tblMain.AddCell(tblPOM);
                                }
                                //loop for printing all WC with respective insurance
                                lineNumber = 0;
                                iCount = 0;

                                for (int k = 0; k < dt.Rows.Count; k++)
                                {
                                    getBillNumbers = GetBillNumbers(p_oDSGroupData, dataSet, dt.Rows[k]["Reffering Office"].ToString(), dt.Rows[k]["CaseType"].ToString(), dt.Rows[k]["Case #"].ToString());
                                    for (int count = 0; count < getBillNumbers.Count; count++)
                                    {
                                        iCount += StringExtensions.LineCount(getBillNumbers[count].ToString());
                                    }
                                    if (getBillNumbers.Count > 0)
                                    {
                                        for (int c = 0; c < getBillNumbers.Count; c++)
                                        {
                                            var normalFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                            var boldFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                                            Phrase phraseInsurance = new Phrase();
                                            phraseInsurance.Add(new Chunk(dt.Rows[k]["Insurance Company"].ToString() + "\n", boldFont));
                                            phraseInsurance.Add(new Chunk(dt.Rows[k]["Insurance Address"].ToString(), normalFont));
                                            sInsuranceNameAddress = "";

                                            lineNumber = lineNumber + 1;
                                            string pName = dt.Rows[k]["Patient Name"].ToString();
                                            string sPatientName = Regex.Replace(pName, ",{2,}", ",").Trim(',');

                                            RegexOptions options = RegexOptions.None;
                                            Regex regex = new Regex("[ ]{2,}", options);
                                            sPatientName = regex.Replace(sPatientName, " ");

                                            string[] patientName = null;
                                            if (sPatientName.Contains(","))
                                            {
                                                patientName = sPatientName.Split(',');
                                            }
                                            else
                                            {
                                                patientName = sPatientName.Split(' ');
                                            }

                                            tblPOM.AddCell(new Phrase(lineNumber.ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(dt.Rows[k]["Claim Number"].ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(phraseInsurance));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(getBillNumbers[c].ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(patientName[1].ToString() + " , " + patientName[0].ToString() + "\n" + dt.Rows[k]["Case #"].ToString(), GetMultiplePOMDataTableRowFont()));
                                            isHeaderAdded = false;
                                            if (iCount >= 13)
                                            {
                                                isHeaderAdded = true;
                                                tblMain.AddCell(tblPomId);
                                                tblMain.AddCell(tblHeader);
                                                tblMain.AddCell(tblTemp);
                                                tblMain.AddCell(tblPOM);
                                                tblMain.AddCell(tblFooter);
                                                document.Add(tblMain);
                                                tblPOM.FlushContent();
                                                tblMain.FlushContent();
                                                document.NewPage();
                                                iCount = 0;
                                            }
                                        }
                                    }
                                }

                                if (rsNF != null && rsNF.Length > 0)
                                {
                                    // there are NF records still to be printed in the same table. do not flush the currect WC records to PDF
                                    tblNFPOM = tblPOM;
                                    tblNFPOM.WidthPercentage = 100;
                                    tblNFPOM.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                                }
                                else
                                {
                                    
                                    if (!isHeaderAdded)
                                    {
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblMain.AddCell(tblTemp);
                                    }

                                    tblMain.AddCell(tblPOM);
                                    tblMain.AddCell(tblFooter);
                                    document.Add(tblMain);
                                    getBillNumbers.Clear();
                                    tblPOM.FlushContent();
                                    tblMain.FlushContent();
                                    document.NewPage();
                                }

                                break;
                            case "WC000000000000000002":
                                if (tblNFPOM == null)
                                {
                                    tblNFPOM = new PdfPTable(wdNFPOM);
                                    tblNFPOM.WidthPercentage = 100;
                                    tblNFPOM.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                                }

                                sInsuranceNameAddress = "";
                                bool hasNFRecords = false;

                                iCount = 0;
                                string[] colNames = { "Reffering Office", "CaseType", "Case Id", "Claim Number", "Insurance Company", "Insurance Address", "Patient Name", "Case #" };
                                dt = new DataTable();
                                dt = GetDistinctRecords(dataSet.Tables[0], colNames);
                                for (int k = 0; k < dt.Rows.Count; k++)
                                {
                                    getBillNumbers = new ArrayList();
                                    getBillNumbers = GetBillNumbers(p_oDSGroupData, dataSet, dt.Rows[k]["Reffering Office"].ToString(), dt.Rows[k]["CaseType"].ToString(), dt.Rows[k]["Case #"].ToString());
                                    for (int count = 0; count < getBillNumbers.Count; count++)
                                    {
                                        iCount += StringExtensions.LineCount(getBillNumbers[count].ToString());
                                    }
                                    if (getBillNumbers.Count > 0)
                                    {
                                        for (int c = 0; c < getBillNumbers.Count; c++)
                                        {
                                            var normalFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                            var boldFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                                            Phrase phraseInsurance = new Phrase();
                                            phraseInsurance.Add(new Chunk(dt.Rows[k]["Insurance Company"].ToString() + "\n", boldFont));
                                            phraseInsurance.Add(new Chunk(dt.Rows[k]["Insurance Address"].ToString(), normalFont));

                                            lineNumber = lineNumber + 1;

                                            string pName = dt.Rows[k]["Patient Name"].ToString();
                                            string sPatientName = Regex.Replace(pName, ",{2,}", ",").Trim(',');

                                            RegexOptions options = RegexOptions.None;
                                            Regex regex = new Regex("[ ]{2,}", options);
                                            sPatientName = regex.Replace(sPatientName, " ");

                                            string[] patientName = null;
                                            if (sPatientName.Contains(","))
                                            {
                                                patientName = sPatientName.Split(',');
                                            }
                                            else
                                            {
                                                patientName = sPatientName.Split(' ');
                                            }

                                            tblNFPOM.AddCell(new Phrase(lineNumber.ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblNFPOM.AddCell(new Phrase(dt.Rows[k]["Claim Number"].ToString(), GetMultiplePOMDataTableRowFont()));
                                            sInsuranceNameAddress = dt.Rows[k]["Insurance Company"].ToString() + "\n" + dt.Rows[k]["Insurance Address"].ToString();
                                            tblNFPOM.AddCell(new Phrase(phraseInsurance));
                                            tblNFPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblNFPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblNFPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblNFPOM.AddCell(new Phrase(getBillNumbers[c].ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblNFPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblNFPOM.AddCell(new Phrase(patientName[1].ToString() + " , " + patientName[0].ToString() + "\n" + dt.Rows[k]["Case #"].ToString(), GetMultiplePOMDataTableRowFont()));
                                            hasNFRecords = true;

                                            if (iCount > 13)
                                            {
                                                tblMain.AddCell(tblPomId);
                                                tblMain.AddCell(tblHeader);
                                                tblPOM = GetMultiplePOMHeader(wd2);
                                                tblMain.AddCell(tblPOM);
                                                tblMain.AddCell(tblNFPOM);
                                                tblMain.AddCell(tblFooter);
                                                document.Add(tblMain);
                                                tblNFPOM.FlushContent();
                                                tblPOM.FlushContent();
                                                tblMain.FlushContent();
                                                document.NewPage();
                                                iCount = 0;
                                            }
                                        }
                                    }
                                }
                                if (hasNFRecords)
                                {
                                    if (tblNFPOM.Rows.Count == 0)
                                    {

                                    }
                                    else
                                    {
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblPOM = GetMultiplePOMHeader(wd2);
                                        tblMain.AddCell(tblPOM);
                                        tblMain.AddCell(tblNFPOM);
                                        tblMain.AddCell(tblFooter);
                                        document.Add(tblMain);
                                        tblPOM.FlushContent();
                                        tblMain.FlushContent();
                                        document.NewPage();
                                    }
                                }
                                hasNFRecords = false;
                                break;
                        }
                    }
                }
            }
            #endregion

            document.Close();
            //TODO: Add the function to DA as a common function which can give you the path based on the 
            // necessary input. This common function will be used from all DA henceforth
            // replace null below with proper value
            System.IO.File.WriteAllBytes(p_sFilePath, m.GetBuffer());
        }

        public void PrintVerificationtPOM(DataSet p_oDSGroupData, bool p_isCombinedPOM, int p_iPOMID, string p_sFilePath, gbmodel.patient.Patient p_oPatient)
        {
            string officeName = "";
            string officeAddress = "";

            DataSet dsOffice = GetOfficeDetails(p_oPatient.Account.ID);

            if (dsOffice.Tables[0] != null && dsOffice.Tables[0].Rows.Count != 0 && dsOffice.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < p_oDSGroupData.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < dsOffice.Tables[0].Rows.Count; j++)
                    {
                        if (p_oDSGroupData.Tables[0].Rows[i]["Doctor ID"].ToString() == dsOffice.Tables[0].Rows[j]["Doctor ID"].ToString())
                        {
                            p_oDSGroupData.Tables[0].Rows[i]["Reffering Office ID"] = dsOffice.Tables[0].Rows[j]["Office ID"].ToString();
                            p_oDSGroupData.Tables[0].Rows[i]["Office Address"] = dsOffice.Tables[0].Rows[j]["Office Address"].ToString();
                            p_oDSGroupData.Tables[0].Rows[i]["Office City"] = dsOffice.Tables[0].Rows[j]["Office City"].ToString();
                            p_oDSGroupData.Tables[0].Rows[i]["Office State"] = dsOffice.Tables[0].Rows[j]["Office State"].ToString();
                            p_oDSGroupData.Tables[0].Rows[i]["Office Zip"] = dsOffice.Tables[0].Rows[j]["Office Zip"].ToString();
                        }
                    }
                }
            }

            string[] columns = { "Reffering Office ID", "Reffering Office", "Office Address", "Office City", "Office State", "Office Zip" };

            DataTable dtDistinctProvider = SelectDistinctProvider(p_oDSGroupData.Tables[0], columns);
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 15, 15, 20, 20);
            MemoryStream m = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, m);

            document.Open();

            DataTable dtCaseType = new DataTable();
            dtCaseType.Columns.Add("sz_case_type_abbreviation_id");

            DataRow dr = dtCaseType.NewRow();
            dr["sz_case_type_abbreviation_id"] = "WC000000000000000001";
            dtCaseType.Rows.Add(dr);

            dr = dtCaseType.NewRow();
            dr["sz_case_type_abbreviation_id"] = "WC000000000000000002";
            dtCaseType.Rows.Add(dr);

            #region if NOT Combined POM

            if (!p_isCombinedPOM)
            {
                for (int i = 0; i < dtDistinctProvider.Rows.Count; i++)
                {
                    int lineNumber = 0;
                    //todo: break on each different provider

                    float[] wtblBase = { 4f };
                    PdfPTable tblMain = new PdfPTable(wtblBase);
                    tblMain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblMain.WidthPercentage = 100;

                    float[] wdPomId = { 4f };
                    PdfPTable tblPomId = new PdfPTable(wdPomId);
                    tblPomId.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblPomId.WidthPercentage = 100;
                    if (p_iPOMID != 0)
                    {
                        tblPomId.AddCell(new Phrase("POM ID: " + p_iPOMID, iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                    }
                    else
                    {
                        tblPomId.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                    }

                    officeName = dtDistinctProvider.Rows[i]["Reffering Office"].ToString();
                    officeAddress = "\n " + dtDistinctProvider.Rows[i]["Office Address"].ToString() + "\n" + dtDistinctProvider.Rows[i]["Office City"].ToString() + "\n" + dtDistinctProvider.Rows[i]["Office State"].ToString() + " " + dtDistinctProvider.Rows[i]["Office Zip"].ToString(); ;

                    float[] wd1 = { 1f, 2f, 3f, 3f, 2.5f };
                    PdfPTable tblHeader = new PdfPTable(wd1);
                    tblHeader.WidthPercentage = 100;
                    tblHeader.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                    tblHeader = GetTableHeader(wd1, officeName, officeAddress);

                    float[] wd2 = { 1f, 1.7f, 4f, 1.5f, 1f, 2.5f, 3f, 2.8f, 3f, 1f, 2f, 1f, 3f };
                    PdfPTable tblPOM = new PdfPTable(wd2);
                    tblPOM.WidthPercentage = 100;
                    tblPOM.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;

                    float[] wdTemp = { 1f, 1.7f, 4f, 1.5f, 1f, 2.5f, 3f, 2.8f, 3f, 1f, 2f, 1f, 3f };
                    PdfPTable tblTemp = new PdfPTable(wdTemp);
                    tblTemp.WidthPercentage = 100;
                    tblTemp.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;

                    float[] wdFooter = { 1f, 2f, 2f, 8f };
                    PdfPTable tblFooter = new PdfPTable(wdFooter);
                    tblFooter.WidthPercentage = 100;
                    tblFooter.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                    tblFooter = GetFooterTable(wdFooter);
                    //todo: add data table header
                    bool isHeaderAdded=false;
                    float[] wdNFPOM = { 1f, 1.7f, 4f, 1.5f, 1f, 2.5f, 3f, 2.8f, 3f, 1f, 2f, 1f, 3f };
                    PdfPTable tblNFPOM = null;

                    for (int j = 0; j < dtCaseType.Rows.Count; j++)
                    {
                        string sCaseTypeAbbreviationID = dtCaseType.Rows[j]["sz_case_type_abbreviation_id"].ToString();

                        DataTable dtPrintRows = null; // get dataset from original dataset where office_id (provider) = loop.provider AND case type = WC...001
                        DataRow[] result = p_oDSGroupData.Tables[0].Select("[Reffering Office ID] = '" + dtDistinctProvider.Rows[i]["Reffering Office ID"].ToString() + "' AND [CaseType Id] = '" + sCaseTypeAbbreviationID + "' ");

                        dtPrintRows = new DataTable();
                        dtPrintRows.Columns.Add("Bill No");
                        dtPrintRows.Columns.Add("Case Id");
                        dtPrintRows.Columns.Add("Patient Name");
                        dtPrintRows.Columns.Add("Speciality");
                        dtPrintRows.Columns.Add("Reffering Office");
                        dtPrintRows.Columns.Add("Bill Amount");
                        dtPrintRows.Columns.Add("Bill Date");
                        dtPrintRows.Columns.Add("Insurance Claim No");
                        dtPrintRows.Columns.Add("Insurance Company");
                        dtPrintRows.Columns.Add("Insurance Address");
                        dtPrintRows.Columns.Add("Min Date Of Service");
                        dtPrintRows.Columns.Add("Max Date Of Service");
                        dtPrintRows.Columns.Add("CaseType Id");
                        dtPrintRows.Columns.Add("WC_ADDRESS");
                        dtPrintRows.Columns.Add("Case No");
                        dtPrintRows.Columns.Add("InsDescription");
                        dtPrintRows.Columns.Add("Reffering Office ID");
                        dtPrintRows.Columns.Add("Office Address");
                        dtPrintRows.Columns.Add("Office City");
                        dtPrintRows.Columns.Add("Office State");
                        dtPrintRows.Columns.Add("Office Zip");

                        foreach (DataRow row in result)
                        {
                            dtPrintRows.ImportRow(row);
                        }

                        DataSet dataSet = new DataSet();
                        dataSet.Tables.Add(dtPrintRows);
                        tblTemp = GetPOMHeader(wd2);

                        switch (sCaseTypeAbbreviationID)
                        {
                            case "WC000000000000000001":
                                //for loop for printing all WC with WC Board
                                tblNFPOM = null;
                                if (dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count == 0)
                                {
                                    continue;
                                }

                                for (int k = 0; k < dataSet.Tables[0].Rows.Count; k++)
                                {
                                    var normalFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                    var boldFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                                    Phrase phraseInsurance = new Phrase();
                                    phraseInsurance.Add(new Chunk("Worker's Compensation Board" + "\n", boldFont));
                                    phraseInsurance.Add(new Chunk(dataSet.Tables[0].Rows[k]["WC_ADDRESS"].ToString(), normalFont));
                                    lineNumber = lineNumber + 1;
                                    string pName = dataSet.Tables[0].Rows[k]["Patient Name"].ToString();
                                    string sPatientName = Regex.Replace(pName, ",{2,}", ",").Trim(',');

                                    RegexOptions options = RegexOptions.None;
                                    Regex regex = new Regex("[ ]{2,}", options);
                                    sPatientName = regex.Replace(sPatientName, " ");

                                    string[] patientName =null;
                                    if(sPatientName.Contains(","))
                                    {
                                        patientName = sPatientName.Split(',');
                                    }
                                    else
                                    {
                                        patientName = sPatientName.Split(' ');
                                    }
                                     

                                    tblPOM.AddCell(new Phrase(lineNumber.ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Insurance Claim No"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(phraseInsurance));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Speciality"].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Min Date Of Service"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase("$ " + dataSet.Tables[0].Rows[k]["Bill Amount"].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Max Date Of Service"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase("", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Bill No"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(patientName[1].ToString() + " , " + patientName[0].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Case No"].ToString(), GetDataTableRowFont()));

                                    if (k % 13 == 0 && k != 0)
                                    {
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblMain.AddCell(tblTemp);
                                        tblMain.AddCell(tblPOM);
                                        tblMain.AddCell(tblFooter);
                                        document.Add(tblMain);
                                        tblPOM.FlushContent();
                                        tblMain.FlushContent();
                                        document.NewPage();
                                    }
                                }
                                tblMain.AddCell(tblPomId);
                                tblMain.AddCell(tblHeader);
                                tblMain.AddCell(tblTemp);
                                tblMain.AddCell(tblPOM);
                                tblMain.AddCell(tblFooter);
                                document.Add(tblMain);
                                tblPOM.FlushContent();
                                tblMain.FlushContent();
                                document.NewPage();

                                //loop for printing all WC with respective insurance
                                lineNumber = 0;
                                for (int k = 0; k < dataSet.Tables[0].Rows.Count; k++)
                                {
                                    var normalFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                    var boldFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                                    Phrase phraseInsurance = new Phrase();
                                    phraseInsurance.Add(new Chunk(dataSet.Tables[0].Rows[k]["Insurance Company"].ToString() + "\n", boldFont));
                                    phraseInsurance.Add(new Chunk(dataSet.Tables[0].Rows[k]["Insurance Address"].ToString(), normalFont));

                                    lineNumber = lineNumber + 1;
                                    string pName = dataSet.Tables[0].Rows[k]["Patient Name"].ToString();
                                    string sPatientName = Regex.Replace(pName, ",{2,}", ",").Trim(',');

                                    RegexOptions options = RegexOptions.None;
                                    Regex regex = new Regex("[ ]{2,}", options);
                                    sPatientName = regex.Replace(sPatientName, " ");

                                    string[] patientName = null;
                                    if (sPatientName.Contains(","))
                                    {
                                        patientName = sPatientName.Split(',');
                                    }
                                    else
                                    {
                                        patientName = sPatientName.Split(' ');
                                    }

                                    tblPOM.AddCell(new Phrase(lineNumber.ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Insurance Claim No"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(phraseInsurance));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Speciality"].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Min Date Of Service"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase("$ " + dataSet.Tables[0].Rows[k]["Bill Amount"].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Max Date Of Service"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase("", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Bill No"].ToString(), GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblPOM.AddCell(new Phrase(patientName[1].ToString() + " , " + patientName[0].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Case No"].ToString(), GetDataTableRowFont()));
                                    isHeaderAdded = false;
                                    if (k % 13 == 0 && k != 0)
                                    {
                                        isHeaderAdded = true;
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblMain.AddCell(tblTemp);
                                        tblMain.AddCell(tblPOM);
                                        tblMain.AddCell(tblFooter);
                                        document.Add(tblMain);
                                        tblPOM.FlushContent();
                                        tblMain.FlushContent();
                                        document.NewPage();
                                    }
                                }

                                DataRow[] rsNF = p_oDSGroupData.Tables[0].Select("[Reffering Office ID] = '" + dtDistinctProvider.Rows[i]["Reffering Office ID"].ToString() + "' AND [CaseType Id] = 'WC000000000000000002'");

                                if (rsNF != null && rsNF.Length > 0)
                                {
                                    // there are NF records still to be printed in the same table. do not flush the currect WC records to PDF
                                    tblNFPOM = tblPOM;
                                    tblNFPOM.WidthPercentage = 100;
                                    tblNFPOM.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                                }
                                else
                                {
                                    if (!isHeaderAdded)
                                    {
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblMain.AddCell(tblTemp);
                                    }
                                    tblMain.AddCell(tblPOM);
                                    tblMain.AddCell(tblFooter);
                                    document.Add(tblMain);
                                    tblPOM.FlushContent();
                                    tblMain.FlushContent();
                                    document.NewPage();
                                }

                                break;
                            case "WC000000000000000002":
                                if (tblNFPOM == null)
                                {
                                    tblNFPOM = new PdfPTable(wdNFPOM);
                                    tblNFPOM.WidthPercentage = 100;
                                    tblNFPOM.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                                }

                                bool hasNFRecords = false;
                                for (int k = 0; k < dataSet.Tables[0].Rows.Count; k++)
                                {
                                    var normalFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                    var boldFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                                    Phrase phraseInsurance = new Phrase();
                                    phraseInsurance.Add(new Chunk(dataSet.Tables[0].Rows[k]["Insurance Company"].ToString() + "\n", boldFont));
                                    phraseInsurance.Add(new Chunk(dataSet.Tables[0].Rows[k]["Insurance Address"].ToString(), normalFont));
                                    lineNumber = lineNumber + 1;
                                    hasNFRecords = true;

                                    string pName = dataSet.Tables[0].Rows[k]["Patient Name"].ToString();
                                    string sPatientName = Regex.Replace(pName, ",{2,}", ",").Trim(',');

                                    RegexOptions options = RegexOptions.None;
                                    Regex regex = new Regex("[ ]{2,}", options);
                                    sPatientName = regex.Replace(sPatientName, " ");

                                    string[] patientName = null;
                                    if (sPatientName.Contains(","))
                                    {
                                        patientName = sPatientName.Split(',');
                                    }
                                    else
                                    {
                                        patientName = sPatientName.Split(' ');
                                    }

                                    tblNFPOM.AddCell(new Phrase(lineNumber.ToString(), GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Insurance Claim No"].ToString(), GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(phraseInsurance));
                                    tblNFPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Speciality"].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Min Date Of Service"].ToString(), GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase("$ " + dataSet.Tables[0].Rows[k]["Bill Amount"].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Max Date Of Service"].ToString(), GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase("", GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(dataSet.Tables[0].Rows[k]["Bill No"].ToString(), GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(" ", GetDataTableRowFont()));
                                    tblNFPOM.AddCell(new Phrase(patientName[1].ToString() + " , " + patientName[0].ToString() + Environment.NewLine + dataSet.Tables[0].Rows[k]["Case No"].ToString(), GetDataTableRowFont()));

                                    if (k % 15 == 0 && k != 0)
                                    {
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblPOM = GetPOMHeader(wd2);
                                        tblMain.AddCell(tblPOM);
                                        tblMain.AddCell(tblNFPOM);
                                        tblMain.AddCell(tblFooter);
                                        document.Add(tblMain);
                                        tblNFPOM.FlushContent();
                                        tblPOM.FlushContent();
                                        tblMain.FlushContent();
                                        document.NewPage();
                                    }
                                }

                                if (hasNFRecords)
                                {
                                    if (tblNFPOM.Rows.Count == 0)
                                    {

                                    }
                                    else
                                    {
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblPOM = GetPOMHeader(wd2);
                                        tblMain.AddCell(tblPOM);
                                        tblMain.AddCell(tblNFPOM);
                                        tblMain.AddCell(tblFooter);
                                        document.Add(tblMain);
                                        tblPOM.FlushContent();
                                        tblMain.FlushContent();
                                        document.NewPage();
                                    }
                                }
                                hasNFRecords = false;
                                break;
                        }
                    }
                }
            }
            #endregion

            #region if Combined POM
            else
            {
                ArrayList getBillNumbers = new ArrayList();
                DataTable dt = new DataTable();
                bool isHeaderAdded = true;
                for (int i = 0; i < dtDistinctProvider.Rows.Count; i++)
                {
                    int lineNumber = 0;
                    //todo: break on each different provider

                    float[] wtblBase = { 4f };
                    PdfPTable tblMain = new PdfPTable(wtblBase);
                    tblMain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblMain.WidthPercentage = 100;

                    float[] wdPomId = { 4f };
                    PdfPTable tblPomId = new PdfPTable(wdPomId);
                    tblPomId.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    tblPomId.WidthPercentage = 100;
                    if (p_iPOMID != 0)
                    {
                        tblPomId.AddCell(new Phrase("POM ID: " + p_iPOMID, iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                    }
                    else
                    {
                        tblPomId.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                    }

                    officeName = dtDistinctProvider.Rows[i]["Reffering Office"].ToString();
                    officeAddress = "\n" + dtDistinctProvider.Rows[i]["Office Address"].ToString() + "\n" + dtDistinctProvider.Rows[i]["Office City"].ToString() + "\n" + dtDistinctProvider.Rows[i]["Office State"].ToString() + " " + dtDistinctProvider.Rows[i]["Office Zip"].ToString();
                    float[] wd1 = { 1f, 2f, 3f, 3f, 2.5f };
                    PdfPTable tblHeader = new PdfPTable(wd1);
                    tblHeader.WidthPercentage = 100;
                    tblHeader.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                    tblHeader = GetTableHeader(wd1, officeName, officeAddress);

                    float[] wd2 = { 0.6f, 1.8f, 4f, 1.2f, 1f, 2f, 4f, 1, 2.5f };
                    PdfPTable tblPOM = new PdfPTable(wd2);
                    tblPOM.WidthPercentage = 100;
                    tblPOM.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;

                    float[] wdTemp = { 0.6f, 1.8f, 4f, 1.2f, 1f, 2f, 4f, 1, 2.5f };
                    PdfPTable tblTemp = new PdfPTable(wdTemp);
                    tblTemp.WidthPercentage = 100;
                    tblTemp.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;

                    float[] wdFooter = { 1f, 2f, 2f, 8f };
                    PdfPTable tblFooter = new PdfPTable(wdFooter);
                    tblFooter.WidthPercentage = 100;
                    tblFooter.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                    tblFooter = GetFooterTable(wdFooter);
                    //todo: add data table header

                    float[] wdNFPOM = { 0.6f, 1.8f, 4f, 1.2f, 1f, 2f, 4f, 1f, 2.5f };
                    PdfPTable tblNFPOM = null;
                    bool hasNFOnly = false;
                    for (int j = 0; j < dtCaseType.Rows.Count; j++)
                    {
                        string sCaseTypeAbbreviationID = dtCaseType.Rows[j]["sz_case_type_abbreviation_id"].ToString();

                        DataTable dtPrintRows = null; // get dataset from original dataset where office_id (provider) = loop.provider AND case type = WC...001

                        DataRow[] result = p_oDSGroupData.Tables[0].Select("[Reffering Office ID] = '" + dtDistinctProvider.Rows[i]["Reffering Office ID"].ToString() + "' AND [CaseType Id] = '" + sCaseTypeAbbreviationID + "' ");

                        dtPrintRows = new DataTable();
                        dtPrintRows.Columns.Add("Bill No");
                        dtPrintRows.Columns.Add("Case Id");
                        dtPrintRows.Columns.Add("Patient Name");
                        dtPrintRows.Columns.Add("Speciality");
                        dtPrintRows.Columns.Add("Reffering Office");
                        dtPrintRows.Columns.Add("Bill Amount");
                        dtPrintRows.Columns.Add("Bill Date");
                        dtPrintRows.Columns.Add("Insurance Claim No");
                        dtPrintRows.Columns.Add("Insurance Company");
                        dtPrintRows.Columns.Add("Insurance Address");
                        dtPrintRows.Columns.Add("Min Date Of Service");
                        dtPrintRows.Columns.Add("Max Date Of Service");
                        dtPrintRows.Columns.Add("CaseType Id");
                        dtPrintRows.Columns.Add("WC_ADDRESS");
                        dtPrintRows.Columns.Add("Case No");
                        dtPrintRows.Columns.Add("InsDescription");
                        dtPrintRows.Columns.Add("Reffering Office ID");
                        dtPrintRows.Columns.Add("Office Address");
                        dtPrintRows.Columns.Add("Office City");
                        dtPrintRows.Columns.Add("Office State");
                        dtPrintRows.Columns.Add("Office Zip");

                        foreach (DataRow row in result)
                        {
                            dtPrintRows.ImportRow(row);
                        }

                        DataSet dataSet = new DataSet();
                        dataSet.Tables.Add(dtPrintRows);
                        tblTemp = GetMultiplePOMHeader(wdTemp);

                        switch (sCaseTypeAbbreviationID)
                        {
                            case "WC000000000000000001":
                                tblNFPOM = null;
                                if (dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count == 0)
                                {
                                    hasNFOnly = true;
                                    continue;
                                }
                                string[] columnsNames = { "Reffering Office ID", "CaseType Id", "Case Id", "Insurance Claim No", "Insurance Company", "Insurance Address", "WC_ADDRESS", "Patient Name", "Case No" };
                                dt = new DataTable();
                                dt = GetDistinctRecords(dataSet.Tables[0], columnsNames);
                                string sInsuranceNameAddress = "";
                                DataRow[] rsNF = p_oDSGroupData.Tables[0].Select("[Reffering Office ID] = '" + dtDistinctProvider.Rows[i]["Reffering Office ID"].ToString() + "' AND [CaseType Id] = 'WC000000000000000002'");
                                int iCount = 0;
                                for (int k = 0; k < dt.Rows.Count; k++)
                                {
                                    getBillNumbers = GetBillNumbersVerificationPOM(p_oDSGroupData, dataSet, dt.Rows[k]["Reffering Office ID"].ToString(), dt.Rows[k]["CaseType Id"].ToString(), dt.Rows[k]["Case No"].ToString());
                                    for (int count = 0; count < getBillNumbers.Count; count++)
                                    {
                                        iCount += StringExtensions.LineCount(getBillNumbers[count].ToString());
                                    }
                                    if (getBillNumbers.Count > 0)
                                    {
                                        for (int c = 0; c < getBillNumbers.Count; c++)
                                        {
                                            var normalFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                            var boldFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                                            Phrase phraseInsurance = new Phrase();
                                            phraseInsurance.Add(new Chunk("Workers Compensation Board" + "\n", boldFont));
                                            phraseInsurance.Add(new Chunk(dt.Rows[k]["WC_ADDRESS"].ToString(), normalFont));

                                            lineNumber = lineNumber + 1;
                                            string pName = dt.Rows[k]["Patient Name"].ToString();
                                            string sPatientName = Regex.Replace(pName, ",{2,}", ",").Trim(',');

                                            RegexOptions options = RegexOptions.None;
                                            Regex regex = new Regex("[ ]{2,}", options);
                                            sPatientName = regex.Replace(sPatientName, " ");

                                            string[] patientName = null;
                                            if (sPatientName.Contains(","))
                                            {
                                                patientName = sPatientName.Split(',');
                                            }
                                            else
                                            {
                                                patientName = sPatientName.Split(' ');
                                            }

                                            tblPOM.AddCell(new Phrase(lineNumber.ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(dt.Rows[k]["Insurance Claim No"].ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(phraseInsurance));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(getBillNumbers[c].ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(patientName[1].ToString() + " , " + patientName[0].ToString() + "\n" + dt.Rows[k]["Case No"].ToString(), GetMultiplePOMDataTableRowFont()));

                                            if (iCount >= 13)
                                            {
                                                tblMain.AddCell(tblPomId);
                                                tblMain.AddCell(tblHeader);
                                                tblMain.AddCell(tblTemp);
                                                tblMain.AddCell(tblPOM);
                                                tblMain.AddCell(tblFooter);
                                                document.Add(tblMain);
                                                tblPOM.FlushContent();
                                                tblMain.FlushContent();
                                                document.NewPage();
                                                iCount = 0;
                                            }
                                        }
                                    }
                                }

                                tblMain.AddCell(tblPomId);
                                tblMain.AddCell(tblHeader);
                                tblMain.AddCell(tblTemp);
                                tblMain.AddCell(tblPOM);
                                tblMain.AddCell(tblFooter);
                                document.Add(tblMain);
                                tblPOM.FlushContent();
                                tblMain.FlushContent();
                                document.NewPage();


                                if (hasNFOnly)
                                {
                                    tblMain.AddCell(tblPomId);
                                    tblMain.AddCell(tblHeader);
                                    tblPOM = GetMultiplePOMHeader(wd2);
                                    tblMain.AddCell(tblPOM);
                                }
                                //loop for printing all WC with respective insurance
                                lineNumber = 0;
                                iCount = 0;
                                for (int k = 0; k < dt.Rows.Count; k++)
                                {
                                    getBillNumbers = GetBillNumbersVerificationPOM(p_oDSGroupData, dataSet, dt.Rows[k]["Reffering Office ID"].ToString(), dt.Rows[k]["CaseType Id"].ToString(), dt.Rows[k]["Case No"].ToString());
                                    for (int count = 0; count < getBillNumbers.Count; count++)
                                    {
                                        iCount += StringExtensions.LineCount(getBillNumbers[count].ToString());
                                    }
                                    if (getBillNumbers.Count > 0)
                                    {
                                        for (int c = 0; c < getBillNumbers.Count; c++)
                                        {
                                            var normalFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                            var boldFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                                            Phrase phraseInsurance = new Phrase();
                                            phraseInsurance.Add(new Chunk(dt.Rows[k]["Insurance Company"].ToString() + "\n", boldFont));
                                            phraseInsurance.Add(new Chunk(dt.Rows[k]["Insurance Address"].ToString(), normalFont));
                                            sInsuranceNameAddress = "";
                                            lineNumber = lineNumber + 1;
                                            string pName = dt.Rows[k]["Patient Name"].ToString();
                                            string sPatientName = Regex.Replace(pName, ",{2,}", ",").Trim(',');

                                            RegexOptions options = RegexOptions.None;
                                            Regex regex = new Regex("[ ]{2,}", options);
                                            sPatientName = regex.Replace(sPatientName, " ");

                                            string[] patientName = null;
                                            if (sPatientName.Contains(","))
                                            {
                                                patientName = sPatientName.Split(',');
                                            }
                                            else
                                            {
                                                patientName = sPatientName.Split(' ');
                                            }

                                            tblPOM.AddCell(new Phrase(lineNumber.ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(dt.Rows[k]["Insurance Claim No"].ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(phraseInsurance));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(getBillNumbers[c].ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblPOM.AddCell(new Phrase(patientName[1].ToString() + " , " + patientName[0].ToString() + "\n" + dt.Rows[k]["Case No"].ToString(), GetMultiplePOMDataTableRowFont()));
                                            isHeaderAdded = false;
                                            if (iCount >= 13)
                                            {
                                                isHeaderAdded = true;
                                                tblMain.AddCell(tblPomId);
                                                tblMain.AddCell(tblHeader);
                                                tblMain.AddCell(tblTemp);
                                                tblMain.AddCell(tblPOM);
                                                tblMain.AddCell(tblFooter);
                                                document.Add(tblMain);
                                                tblPOM.FlushContent();
                                                tblMain.FlushContent();
                                                document.NewPage();
                                                iCount = 0;
                                            }
                                        }
                                    }
                                }

                                if (rsNF != null && rsNF.Length > 0)
                                {
                                    // there are NF records still to be printed in the same table. do not flush the currect WC records to PDF
                                    tblNFPOM = tblPOM;
                                    tblNFPOM.WidthPercentage = 100;
                                    tblNFPOM.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                                }
                                else
                                {
                                    if (!isHeaderAdded)
                                    {
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblMain.AddCell(tblTemp);
                                    }
                                    tblMain.AddCell(tblPOM);
                                    tblMain.AddCell(tblFooter);
                                    document.Add(tblMain);
                                    getBillNumbers.Clear();
                                    tblPOM.FlushContent();
                                    tblMain.FlushContent();
                                    document.NewPage();
                                }

                                break;
                            case "WC000000000000000002":
                                if (tblNFPOM == null)
                                {
                                    tblNFPOM = new PdfPTable(wdNFPOM);
                                    tblNFPOM.WidthPercentage = 100;
                                    tblNFPOM.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
                                }

                                sInsuranceNameAddress = "";
                                bool hasNFRecords = false;
                                string[] colNames = { "Reffering Office ID", "CaseType Id", "Case Id", "Insurance Claim No", "Insurance Company", "Insurance Address", "Patient Name", "Case No" };
                                dt = new DataTable();
                                dt = GetDistinctRecords(dataSet.Tables[0], colNames);
                                iCount = 0;
                                for (int k = 0; k < dt.Rows.Count; k++)
                                {
                                    getBillNumbers = new ArrayList();
                                    getBillNumbers = GetBillNumbersVerificationPOM(p_oDSGroupData, dataSet, dt.Rows[k]["Reffering Office ID"].ToString(), dt.Rows[k]["CaseType Id"].ToString(), dt.Rows[k]["Case No"].ToString());
                                    for (int count = 0; count < getBillNumbers.Count; count++)
                                    {
                                        iCount += StringExtensions.LineCount(getBillNumbers[count].ToString());
                                    }
                                    if (getBillNumbers.Count > 0)
                                    {
                                        for (int c = 0; c < getBillNumbers.Count; c++)
                                        {
                                            var normalFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                            var boldFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

                                            Phrase phraseInsurance = new Phrase();
                                            phraseInsurance.Add(new Chunk(dt.Rows[k]["Insurance Company"].ToString() + "\n", boldFont));
                                            phraseInsurance.Add(new Chunk(dt.Rows[k]["Insurance Address"].ToString(), normalFont));

                                            lineNumber = lineNumber + 1;

                                            string pName = dt.Rows[k]["Patient Name"].ToString();
                                            string sPatientName = Regex.Replace(pName, ",{2,}", ",").Trim(',');

                                            RegexOptions options = RegexOptions.None;
                                            Regex regex = new Regex("[ ]{2,}", options);
                                            sPatientName = regex.Replace(sPatientName, " ");

                                            string[] patientName = null;
                                            if (sPatientName.Contains(","))
                                            {
                                                patientName = sPatientName.Split(',');
                                            }
                                            else
                                            {
                                                patientName = sPatientName.Split(' ');
                                            }

                                            tblNFPOM.AddCell(new Phrase(lineNumber.ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblNFPOM.AddCell(new Phrase(dt.Rows[k]["Insurance Claim No"].ToString(), GetMultiplePOMDataTableRowFont()));
                                            sInsuranceNameAddress = dt.Rows[k]["Insurance Company"].ToString() + "\n" + dt.Rows[k]["Insurance Address"].ToString();
                                            tblNFPOM.AddCell(new Phrase(phraseInsurance));
                                            tblNFPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblNFPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblNFPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblNFPOM.AddCell(new Phrase(getBillNumbers[c].ToString(), GetMultiplePOMDataTableRowFont()));
                                            tblNFPOM.AddCell(new Phrase(" ", GetMultiplePOMDataTableRowFont()));
                                            tblNFPOM.AddCell(new Phrase(patientName[1].ToString() + " , " + patientName[0].ToString() + "\n" + dt.Rows[k]["Case No"].ToString(), GetMultiplePOMDataTableRowFont()));
                                            hasNFRecords = true;

                                            if (iCount > 13)
                                            {
                                                tblMain.AddCell(tblPomId);
                                                tblMain.AddCell(tblHeader);
                                                tblPOM = GetMultiplePOMHeader(wd2);
                                                tblMain.AddCell(tblPOM);
                                                tblMain.AddCell(tblNFPOM);
                                                tblMain.AddCell(tblFooter);
                                                document.Add(tblMain);
                                                tblNFPOM.FlushContent();
                                                tblPOM.FlushContent();
                                                tblMain.FlushContent();
                                                document.NewPage();
                                                iCount = 0;
                                            }
                                        }
                                    }
                                }
                                if (hasNFRecords)
                                {
                                    if (tblNFPOM.Rows.Count == 0)
                                    {

                                    }
                                    else
                                    {
                                        tblMain.AddCell(tblPomId);
                                        tblMain.AddCell(tblHeader);
                                        tblPOM = GetMultiplePOMHeader(wd2);
                                        tblMain.AddCell(tblPOM);
                                        tblMain.AddCell(tblNFPOM);
                                        tblMain.AddCell(tblFooter);
                                        document.Add(tblMain);
                                        tblPOM.FlushContent();
                                        tblMain.FlushContent();
                                        document.NewPage();
                                    }
                                }
                                hasNFRecords = false;
                                break;
                        }
                    }
                }
            }
            #endregion

            document.Close();
            //TODO: Add the function to DA as a common function which can give you the path based on the 
            // necessary input. This common function will be used from all DA henceforth
            // replace null below with proper value
            System.IO.File.WriteAllBytes(p_sFilePath, m.GetBuffer());
        }

        #region Private methods for POM
        private PdfPTable GetTableHeader(float[] wd1, string officeName, string officeAddress)
        {
            var normalFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
            var boldFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);

            Phrase phraseOffice = new Phrase();
            phraseOffice.Add(new Chunk(officeName, boldFont));
            phraseOffice.Add(new Chunk(officeAddress, normalFont));

            PdfPTable tblHeader = new PdfPTable(wd1);
            tblHeader.WidthPercentage = 100;
            tblHeader.AddCell(new Phrase("Name and Address of Sender", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase(phraseOffice));
            PdfPTable tblContent = new PdfPTable(2);
            tblContent.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            PdfPCell cellMail = new PdfPCell(new Phrase("Indicate type of Mail", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            cellMail.Colspan = 2;
            cellMail.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellMail.HorizontalAlignment = 1;
            PdfPCell cell1 = new PdfPCell(new Phrase("Registered Insured COD Certified", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            PdfPCell cell2 = new PdfPCell(new Phrase("Return Receipt for Merchandise Int'l Recorded Del. Express Mail", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblContent.AddCell(cellMail);
            tblContent.AddCell(cell1);
            tblContent.AddCell(cell2);
            tblHeader.AddCell(tblContent);

            PdfPTable tblContentTwo = new PdfPTable(1);
            tblContentTwo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            PdfPCell cellRegistered = new PdfPCell(new Phrase("Check appropriate block for Registered Mail", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            cellRegistered.Border = iTextSharp.text.Rectangle.NO_BORDER;
            PdfPCell cell3 = new PdfPCell(new Phrase("With Postal Insurance" + "\n" + "Without Postal Insurance", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            cell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellRegistered.Colspan = 1;
            tblContentTwo.AddCell(cellRegistered);
            tblContentTwo.AddCell(cell3);
            tblHeader.AddCell(tblContentTwo);

            PdfPTable tblContentThree = new PdfPTable(1);
            tblContentThree.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            PdfPCell cellStamp = new PdfPCell(new Phrase("Affix Stamp here if issued as certificate of mailing or for additional copies of this bill", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            cellStamp.Border = iTextSharp.text.Rectangle.NO_BORDER;
            PdfPCell cell4 = new PdfPCell(new Phrase("Postmark and Date of Receipt", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));
            cell4.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellStamp.Colspan = 1;
            tblContentThree.AddCell(cellStamp);
            tblContentThree.AddCell(cell4);
            tblHeader.AddCell(tblContentThree);
            return tblHeader;
        }

        private PdfPTable GetPOMHeader(float[] wd2)
        {
            PdfPTable tblHeader = new PdfPTable(wd2);
            tblHeader.WidthPercentage = 100;
            tblHeader.AddCell(new Phrase("Line", GetDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Article Number", GetDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Name of Addressee, Street,and Post Office Address", GetDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Postage", GetDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Fee", GetDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Handling Charge", GetDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Act. Value (if regis.)", GetDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Insured Value", GetDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Due Sender if COD", GetDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("S.D. fee", GetDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("R.R. fee", GetDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("S.H. fee", GetDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Rest Del fee remarks", GetDataTableHeaderFont()));
            return tblHeader;
        }

        private iTextSharp.text.Font GetDataTableHeaderFont()
        {
            return iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
        }

        private iTextSharp.text.Font GetDataTableRowFont()
        {
            return iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
        }

        private PdfPTable GetMultiplePOMHeader(float[] wd2)
        {
            PdfPTable tblHeader = new PdfPTable(wd2);
            tblHeader.WidthPercentage = 100;
            tblHeader.AddCell(new Phrase("Line", GetMultiplePOMDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Article Number", GetMultiplePOMDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Name of Addressee, Street,and Post Office Address", GetMultiplePOMDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Postage", GetMultiplePOMDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Fee", GetMultiplePOMDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Handling Charge", GetMultiplePOMDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Insured Value -- Due Sender if COD -- S.D. fee", GetMultiplePOMDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("S.H. fee", GetMultiplePOMDataTableHeaderFont()));
            tblHeader.AddCell(new Phrase("Rest Del fee Remarks", GetMultiplePOMDataTableHeaderFont()));
            return tblHeader;
        }

        private iTextSharp.text.Font GetMultiplePOMDataTableHeaderFont()
        {
            return iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
        }

        private iTextSharp.text.Font GetMultiplePOMDataTableRowFont()
        {
            return iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
        }

        private PdfPTable GetFooterTable(float[] wd2)
        {
            string text = "The full declaration of value is required on all domestic and international registered mail.";
            text += " The maximum indemnity payable for the reconstruction of nonnegotiable documents";
            text += " under Express Mail document reconstruction insurance is $500 per piece subject to";
            text += " additional limitations for multiple pieces lost or damages in a single catastrophic";
            text += " additional limitations for multiple pieces lost or damages in a single catastrophic";
            text += " occurrence. The maximum indemnity payable on Express Mail merchandise is $500, but";
            text += " optional Express Mail Service merchandise insurance is available for up to $5000 to";
            text += " some, but not all countries. The maximum indemnity payable is $25,000 for registered";
            text += " mail. See Domestic Mail Manual R900, S913, and S921 for limitations of coverage on";
            text += " insured and COD mail. See International Mail Manual for limitations of coverage on";
            text += " international mail. Special handling charges apply only to Standard Mail (A) and";
            text += " Standard Mail (B) parcels.";

            PdfPTable tblHeader = new PdfPTable(wd2);
            tblHeader.WidthPercentage = 100;
            tblHeader.AddCell(new Phrase("Total Number of Pieces Listed by Sender", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase("Total Number of Pieces Received at Post Office", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase("Postmaster, Per (Name of receiving employee)", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase(text, iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            return tblHeader;

        }

        private ArrayList GetBillNumbers(DataSet dsGroupData, DataSet ds, string referringOfficeId, string caseTypeId, string caseNo)
        {
            string billNumbers = "";
            ArrayList arrBillNo = new ArrayList();
            DataTable dtBillNos = new DataTable();
            dtBillNos.Columns.Add("Bill ID");
            dtBillNos.Columns.Add("Case Id");
            dtBillNos.Columns.Add("Patient Name");
            dtBillNos.Columns.Add("Spciality");
            dtBillNos.Columns.Add("Reffering Office");
            dtBillNos.Columns.Add("Bill Amount");
            dtBillNos.Columns.Add("Bill Date");
            dtBillNos.Columns.Add("Claim Number");
            dtBillNos.Columns.Add("Insurance Company");
            dtBillNos.Columns.Add("Insurance Address");
            dtBillNos.Columns.Add("Min Date Of Service");
            dtBillNos.Columns.Add("Max Date Of Service");
            dtBillNos.Columns.Add("CaseType");
            dtBillNos.Columns.Add("WC_ADDRESS");
            dtBillNos.Columns.Add("Case #");
            dtBillNos.Columns.Add("Provider");
            dtBillNos.Columns.Add("Office Address");
            dtBillNos.Columns.Add("Office City");
            dtBillNos.Columns.Add("Office State");
            dtBillNos.Columns.Add("Office Zip");
            DataRow[] result = dsGroupData.Tables[0].Select("[Reffering Office] = '" + referringOfficeId + "' AND [CaseType] = '" + caseTypeId + "' AND [Case #] = '" + caseNo + "' ");
            foreach (DataRow row in result)
            {
                dtBillNos.ImportRow(row);
            }
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dtBillNos);

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (i % 9 == 0 && i != 0)
                {
                    arrBillNo.Add(billNumbers);
                    billNumbers = "";
                }

                billNumbers = billNumbers += dataSet.Tables[0].Rows[i]["Spciality"].ToString() + "-" + dataSet.Tables[0].Rows[i]["Min Date Of Service"].ToString() + "-" + " $ " + dataSet.Tables[0].Rows[i]["Bill Amount"].ToString() + "-" + dataSet.Tables[0].Rows[i]["Max Date Of Service"].ToString() + "-" + dataSet.Tables[0].Rows[i]["Bill ID"].ToString() + "\n\n\n\n";
            }
            if (billNumbers != "")
            {
                arrBillNo.Add(billNumbers);
            }

            if (arrBillNo.Count == 0)
            {
                arrBillNo.Add(billNumbers);
            }

            return arrBillNo;
        }

        private ArrayList GetBillNumbersVerificationPOM(DataSet dsGroupData, DataSet ds, string referringOfficeId, string caseTypeId, string caseNo)
        {
            string billNumbers = "";
            ArrayList arrBillNo = new ArrayList();
            DataTable dtBillNos = new DataTable();
            dtBillNos.Columns.Add("Bill No");
            dtBillNos.Columns.Add("Case Id");
            dtBillNos.Columns.Add("Patient Name");
            dtBillNos.Columns.Add("Speciality");
            dtBillNos.Columns.Add("Reffering Office");
            dtBillNos.Columns.Add("Bill Amount");
            dtBillNos.Columns.Add("Bill Date");
            dtBillNos.Columns.Add("Insurance Claim No");
            dtBillNos.Columns.Add("Insurance Company");
            dtBillNos.Columns.Add("Insurance Address");
            dtBillNos.Columns.Add("Min Date Of Service");
            dtBillNos.Columns.Add("Max Date Of Service");
            dtBillNos.Columns.Add("CaseType Id");
            dtBillNos.Columns.Add("WC_ADDRESS");
            dtBillNos.Columns.Add("Case No");
            dtBillNos.Columns.Add("InsDescription");
            dtBillNos.Columns.Add("Reffering Office ID");
            dtBillNos.Columns.Add("Office Address");
            dtBillNos.Columns.Add("Office City");
            dtBillNos.Columns.Add("Office State");
            dtBillNos.Columns.Add("Office Zip");
            DataRow[] result = dsGroupData.Tables[0].Select("[Reffering Office ID] = '" + referringOfficeId + "' AND [CaseType Id] = '" + caseTypeId + "' AND [Case No] = '" + caseNo + "' ");
            foreach (DataRow row in result)
            {
                dtBillNos.ImportRow(row);
            }
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dtBillNos);

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                if (i % 9 == 0 && i != 0)
                {
                    arrBillNo.Add(billNumbers);
                    billNumbers = "";
                }

                billNumbers = billNumbers += dataSet.Tables[0].Rows[i]["Speciality"].ToString() + "-" + dataSet.Tables[0].Rows[i]["Min Date Of Service"].ToString() + "-" + " $ " + dataSet.Tables[0].Rows[i]["Bill Amount"].ToString() + "-" + dataSet.Tables[0].Rows[i]["Max Date Of Service"].ToString() + "-" + dataSet.Tables[0].Rows[i]["Bill No"].ToString() + "\n\n\n\n";
            }
            if (billNumbers != "")
            {
                arrBillNo.Add(billNumbers);
            }

            if (arrBillNo.Count == 0)
            {
                arrBillNo.Add(billNumbers);
            }

            return arrBillNo;
        }

        private DataTable SelectDistinctProvider(DataTable dt, string[] Columns)
        {
            DataTable dtUniqueRecords = new DataTable();
            dtUniqueRecords = dt.DefaultView.ToTable(true, Columns);
            return dtUniqueRecords;
        }

        private DataTable GetDistinctRecords(DataTable dt, string[] Columns)
        {
            DataTable dtUniqueRecords = new DataTable();
            dtUniqueRecords = dt.DefaultView.ToTable(true, Columns);
            return dtUniqueRecords;
        }

        private DataSet GetOfficeDetails(string companyId)
        {
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
            DataSet dsResult = new DataSet();
            try
            {
                sqlCon.Open();
                SqlCommand sqlCommand = new SqlCommand("sp_get_office_details", sqlCon);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@sz_company_id", companyId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                da.Fill(dsResult);

            }
            catch (SqlException ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return dsResult;
        }
        #endregion Private methods for POM
    }
}