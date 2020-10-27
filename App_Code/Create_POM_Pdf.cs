using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Componend;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using log4net;
using log4net.Config;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;

public class Create_POM_Pdf
{
	public Create_POM_Pdf()
	{
        
	}
    Font arialbold = FontFactory.GetFont("Arial", 8f, Font.BOLD, Color.BLACK);
    Font arialbolheader = FontFactory.GetFont("Arial", 12f, Font.BOLD, Color.GRAY);
    Font arialheaderbold = FontFactory.GetFont("Arial", 8f, Font.BOLD, Color.GRAY);
    Font arialheader = FontFactory.GetFont("Arial", 8f, Font.NORMAL, Color.GRAY);
    Font arialboldadj = FontFactory.GetFont("Arial", 7f, Font.BOLD, Color.BLACK);
    Font arial = FontFactory.GetFont("Arial", 8f, Color.BLACK);
    Font arialsmall = FontFactory.GetFont("Arial", 7f, Color.BLACK);
    Font arialboldsmall = FontFactory.GetFont("Arial", 6.4f, Font.BOLD, Color.BLACK);
    Font arialten = FontFactory.GetFont("Arial", 10f, Font.NORMAL, Color.BLACK);

    //public void generatePdf(String filePath, DataSet ds, int I_POM_ID)
    //{
    //    ArrayList objal = new ArrayList();

    //    Font arial = FontFactory.GetFont("Arial", 8f, Color.BLACK);


    //    MemoryStream m = new MemoryStream();
    //    FileStream fs = new FileStream(filePath, System.IO.FileMode.OpenOrCreate);
    //    iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 20, 20, 10, 10);
    //    PdfWriter writer = PdfWriter.GetInstance(document, fs);
    //    document.Open();

    //    float[] wMain = { 6f };
    //    PdfPTable tblMain = new PdfPTable(wMain);
    //    tblMain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
    //    tblMain.WidthPercentage = 100;
    //    tblMain.TotalWidth = 500f;
    //    //tblMain.WriteSelectedRows(0, -1, document.Left + 38, document.Top - 10, writer.DirectContent);

    //    float[] wBase = { 4f };
    //    PdfPTable tblBase = new PdfPTable(wBase);
    //    tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
    //    tblBase.WidthPercentage = 100;

    //    tblBase = testGenerate(document, writer, ds, I_POM_ID);
    //    tblMain.AddCell(tblBase);
    //    document.Add(tblMain);
    //    document.Close();

    //}
    //public PdfPTable testGenerate(iTextSharp.text.Document document, PdfWriter writer, DataSet ds,int I_POM_ID)
    //{
    //    PdfPTable ptnmbillTable = new PdfPTable(1);
    //    ptnmbillTable.HorizontalAlignment = Element.ALIGN_LEFT;
    //    ptnmbillTable.TotalWidth = 500f;
    //    ptnmbillTable.LockedWidth = true;

    //    PdfPCell nmBillCell = new PdfPCell(new Phrase("POM ID : " + I_POM_ID, arialbold));
    //    nmBillCell.FixedHeight = 30f;
    //    nmBillCell.HorizontalAlignment = 0;
    //    nmBillCell.Border = 0;
    //    ptnmbillTable.AddCell(nmBillCell);

    //    ptnmbillTable.WriteSelectedRows(0, -1, document.Left + 38, document.Top - 19, writer.DirectContent);
        
    //    //float[] wBase = { 1f };
    //    //PdfPTable tblBase = new PdfPTable(wBase);
    //    //tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
    //    //tblBase.WidthPercentage = 100;

    //    //float[] fHeader = { 1f, 1.7f, 1f };
    //    //PdfPTable tblHeader = new PdfPTable(fHeader);
    //    //tblHeader.SpacingBefore = 70;
    //    //tblHeader.SpacingAfter = 10;
    //    //tblHeader.DefaultCell.Border = Rectangle.NO_BORDER;
    //    //PdfPCell header = new PdfPCell(new Phrase("POM ID" + ":"+ I_POM_ID, arialbolheader));
    //    //header.Border = Rectangle.NO_BORDER;
    //    //header.HorizontalAlignment = 1;
    //    //header.Colspan = 3;
    //    //tblHeader.AddCell(header);

    //    //PdfPCell submission1 = new PdfPCell(new Phrase(".", arial));
    //    //submission1.Border = Rectangle.NO_BORDER;
    //    //tblHeader.AddCell(submission1);

    //    //PdfPTable insurerdetailsTable = new PdfPTable(1);
    //    //insurerdetailsTable.TotalWidth = 210f;
    //    //insurerdetailsTable.LockedWidth = true;

    //    //PdfPCell captionCell1 = getPdfPCell("Total Number of Pieces Listed by Sender", 10f, 0, 1.5f, Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER);
    //    //PdfPCell captionCell2 = getPdfPCell("Total Number of Pieces Received at Post Office", 15f, 0, 1.5f,  Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER);

    //    //insurerdetailsTable.AddCell(captionCell1);
    //    //insurerdetailsTable.WriteSelectedRows(0, -1, document.Left + 38, document.Top - 90, writer.DirectContent);
    //    //float[] ftblPatientMain = { 2f, 2f, 2f };
    //    //PdfPTable tblPatientMain = new PdfPTable(ftblPatientMain);

    //    //float[] ffirstRow = { 2f, 2f };
    //    //PdfPTable tblfirstRow = new PdfPTable(ffirstRow);
    //    //PdfPCell firstCellOne = new PdfPCell(new Phrase("Name and", arialbold));
    //    //PdfPCell firstCellTwo = new PdfPCell(new Phrase("address of", arialbold));
    //    //PdfPCell firstCellThree = new PdfPCell(new Phrase("sender", arialbold));
    //    //firstCellOne.HorizontalAlignment = 0;
    //    //firstCellTwo.HorizontalAlignment = 0;
    //    //firstCellThree.HorizontalAlignment = 0;
    //    //firstCellOne.Border = Rectangle.RIGHT_BORDER;
    //    //firstCellTwo.Border = Rectangle.RIGHT_BORDER;
    //    //firstCellThree.Border = Rectangle.RIGHT_BORDER|Rectangle.BOTTOM_BORDER;
    //    //tblfirstRow.AddCell(firstCellOne);
    //    //tblfirstRow.AddCell(firstCellTwo);
    //    //tblfirstRow.AddCell(firstCellThree);
    //    //tblBase.AddCell(tblfirstRow);
    //    //tblBase.AddCell(tblHeader);
    //    //ptnmbillTable.AddCell(tblPatientMain);

    //    return ptnmbillTable;
    //}
    //private PdfPCell getPdfPCell(string p_sPhrase, float p_FixedHeight, int p_HorizontalAlignment, float p_BorderWidth, int p_Border)
    //{
    //    PdfPCell dtCell1 = new PdfPCell(new Phrase(p_sPhrase, arial));
    //    dtCell1.FixedHeight = p_FixedHeight;
    //    dtCell1.HorizontalAlignment = p_HorizontalAlignment;
    //    dtCell1.BorderWidth = p_BorderWidth;
    //    dtCell1.Border = p_Border;
    //    return dtCell1;
    //}

    public void generatePdf(string base_file_Path,String filePath, DataSet ds, int I_POM_ID, DataSet headerDs, string pdffilename)
    {
        string strProvider = null;
        int iFlag = 0;
        string billidWC = "";
        int printi = 0;
        ++printi;
        int printiWC=0;
        string strCasetype = null;

        ArrayList objal = new ArrayList();

        Font arial = FontFactory.GetFont("Arial", 8f, Color.BLACK);

        MemoryStream m = new MemoryStream();
        FileStream fs = new FileStream(filePath, System.IO.FileMode.OpenOrCreate);
        iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 20, 20, 10, 10);
        PdfWriter writer = PdfWriter.GetInstance(document, fs);
        document.Open();

        float[] wMain = { 6f };
        PdfPTable tblMain = new PdfPTable(wMain);
        tblMain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        tblMain.WidthPercentage = 100;
        tblMain.TotalWidth = 500f;

        float[] wBase = { 4f };
        PdfPTable tblBase = new PdfPTable(wBase);
        tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        tblBase.WidthPercentage = 100;

        tblBase = generateAppoinmentPdf(base_file_Path,document, writer, ds, I_POM_ID, headerDs, filePath);
                        
        tblMain.AddCell(tblBase);
        document.Add(tblMain);
        document.Close();

    }
    public PdfPTable generateAppoinmentPdf(string base_file_Path,Document document, PdfWriter writer, DataSet ds, int I_POM_ID, DataSet headerDs, string filePath)
    {
        string strProvider = null;
        int iFlag = 0;
        string billidWC = "";
        int printi = 0;
        int printiWC = 0;
        int iRecordOnpageNF = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        int iRecordOnpageWC = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        int iRecordsPerPageNF = 0;
        int iRecordsPerPageWC = 0;
        int _iCountWC = 0;
        int _iCountNF = 0;
        bool iWCCount = false;
        int iPageCount = 1;

        //iTextSharp.text.Document doc = new iTextSharp.text.Document();
        
        //doc.Open();
        //string filename = base_file_Path;
        //PdfReader reader = new PdfReader(filename);

        //doc.SetPageSize(reader.GetPageSizeWithRotation(1));
        //document.NewPage();

        float[] wMain = { 6f };
        PdfPTable tblMainTable = new PdfPTable(wMain);
        tblMainTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        tblMainTable.WidthPercentage = 100;
        tblMainTable.TotalWidth = 500f;
        
        float[] wheader = { 4f };
        PdfPTable tblheader = new PdfPTable(wheader);
        tblheader.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        tblheader.WidthPercentage = 100;
        tblheader = CreateHeader(document, writer, ds, I_POM_ID, headerDs);

        //Middle table
        float[] wMiddleheader = { 0.25f, 0.60f, 2f, 0.60f, 0.40f, 1f, 1.5f, 1f, 1.5f, 0.50f, 0.50f, 0.50f, 1.5f };
        PdfPTable tblMain = new PdfPTable(wMiddleheader);
        tblMain.WidthPercentage = 100;
        tblMain.DefaultCell.BorderColor = Color.BLACK;
        tblMain = CreateMiddleHeader(document, writer, ds, I_POM_ID, headerDs);

        // Content table header
        Font fntContentTableHeading = new Font(iTextSharp.text.FontFactory.GetFont("Arial", 6, Font.NORMAL, iTextSharp.text.Color.BLACK));
        
        if (ds != null)
        {
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sCaseNumber = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string strCasetype = ds.Tables[0].Rows[i]["CaseType"].ToString();
                        if (strCasetype == "WC000000000000000001")
                        {
                            if (iFlag == 0)
                            {
                                strProvider = ds.Tables[0].Rows[i]["Provider"].ToString();
                                iFlag = 1;
                                billidWC = ds.Tables[0].Rows[i]["Bill ID"].ToString();
                                iRecordsPerPageWC = 0;
                            }
                            if (strProvider.Equals(ds.Tables[0].Rows[i]["Provider"].ToString()))
                            {
                                ++printiWC;
                                ++printi;
                                if (iRecordsPerPageWC >= iRecordOnpageWC)
                                {
                                    CreateHeader(document, writer, ds, I_POM_ID, headerDs);
                                    CreateFooter(document, writer, ds, I_POM_ID, headerDs);
                                    iWCCount = true;

                                    iRecordsPerPageWC = 1;
                                    _iCountWC = 1;
                                    printiWC = 1;
                                }
                                if (iWCCount == true)
                                {
                                    //CreateMiddleHeader(document, writer, ds, I_POM_ID, headerDs);
                                    iWCCount = false;
                                }

                                //tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                                //CreateMiddleHeaderValues(document, writer, ds, I_POM_ID, headerDs, printi.ToString());
                                //tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                                //tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Claim Number"]), fntContentTableHeading));
                                //tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Insurance Company"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Insurance Address"]), fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                //tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Spciality"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Min Date Of Service"]), fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("$" + Convert.ToString(ds.Tables[0].Rows[i]["Bill Amount"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Max Date Of Service"]), fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                //tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Bill ID"]), fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                //tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Patient Name"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Case #"]), fntContentTableHeading));

                                _iCountNF++;

                                if (_iCountNF >= iRecordOnpageNF)
                                {
                                    iRecordsPerPageNF = _iCountNF;
                                    if (iRecordsPerPageNF >= iRecordOnpageNF)
                                    {
                                        CreateHeader(document, writer, ds, I_POM_ID, headerDs);
                                        CreateFooter(document, writer, ds, I_POM_ID, headerDs);
                                        //CreateMiddleHeader(document, writer, ds, I_POM_ID, headerDs);
                                        iRecordsPerPageNF = 0;
                                        _iCountNF = 0;
                                        printi = 0;
                                    }
                                }
                                
                                //tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                                //CreateMiddleHeaderValuesWC(document, writer, ds, I_POM_ID, headerDs,printi.ToString());
                                //tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                                //tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Claim Number"]), fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("Worker's Compensation Board " + Convert.ToString(ds.Tables[0].Rows[i]["WC_ADDRESS"]), fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                //tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Spciality"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Min Date Of Service"]), fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("$" + Convert.ToString(ds.Tables[0].Rows[i]["Bill Amount"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Max Date Of Service"]), fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                //tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Bill ID"]), fntContentTableHeading));
                                //tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                //tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Patient Name"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Case #"]), fntContentTableHeading));
                                
                                _iCountWC++;
                                iRecordsPerPageWC++;
                            }
                            else
                            {
                                printi = 1;
                                printiWC = 1;
                                // SUPPOSE NEW PROVIDER OR CHANGE PROVIDER WHICH NO MATCH WITH PRIVOUS PROIVDER
                                if (_iCountWC > 0)
                                {
                                    CreateHeader(document, writer, ds, I_POM_ID, headerDs);
                                    CreateFooter(document, writer, ds, I_POM_ID, headerDs);
                                    iWCCount = true;
                                    //genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                                    printiWC = 1;
                                }
                                if (iWCCount == true)
                                {
                                    CreateMiddleHeader(document, writer, ds, I_POM_ID, headerDs);
                                    iWCCount = false;
                                }

                                CreateHeader(document, writer, ds, I_POM_ID, headerDs);
                                CreateFooter(document, writer, ds, I_POM_ID, headerDs);

                                // NEW PROVIDER AND BILLID SAVE IN VARIABLES
                                strProvider = ds.Tables[0].Rows[i]["Provider"].ToString();
                                billidWC = ds.Tables[0].Rows[i]["Bill ID"].ToString();
                                CreateMiddleHeader(document, writer, ds, I_POM_ID, headerDs);

                                //append nf html for wc bill
                                //tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                                //CreateMiddleHeaderValues(document, writer, ds, I_POM_ID, headerDs, printi.ToString());
                                tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Claim Number"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Insurance Company"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Insurance Address"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Spciality"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Min Date Of Service"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("$" + Convert.ToString(ds.Tables[0].Rows[i]["Bill Amount"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Max Date Of Service"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Bill ID"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Patient Name"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Case #"]), fntContentTableHeading));
                                
                                _iCountNF++;
                                if (_iCountNF >= iRecordOnpageNF)
                                {
                                    iRecordsPerPageNF = _iCountNF;
                                    if (iRecordsPerPageNF >= iRecordOnpageNF)
                                    {
                                        CreateHeader(document, writer, ds, I_POM_ID, headerDs);
                                        CreateFooter(document, writer, ds, I_POM_ID, headerDs);
                                        CreateMiddleHeader(document, writer, ds, I_POM_ID, headerDs);
                                        iRecordsPerPageNF = 0;
                                        _iCountNF = 0;
                                        printi = 1;
                                    }
                                }

                                //append wc html for wc bill
                                //tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                                //CreateMiddleHeaderValuesWC(document, writer, ds, I_POM_ID, headerDs, printi.ToString());
                                tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Claim Number"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("Worker's Compensation Board " + Convert.ToString(ds.Tables[0].Rows[i]["WC_ADDRESS"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Spciality"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Min Date Of Service"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("$" + Convert.ToString(ds.Tables[0].Rows[i]["Bill Amount"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Max Date Of Service"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Bill ID"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Patient Name"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Case #"]), fntContentTableHeading));
                                _iCountWC++;

                                iPageCount++;

                                iRecordsPerPageWC = 1;
                                iRecordsPerPageNF = 1;
                                _iCountNF = 1;
                            }
                        }
                        else if (strCasetype != "WC000000000000000001")
                        {
                            if (iFlag == 0)
                            {
                                strProvider = ds.Tables[0].Rows[i]["Provider"].ToString();
                                iFlag = 1;
                            }
                            
                            if (strProvider.Equals(ds.Tables[0].Rows[i]["Provider"].ToString()))
                            {
                                

                                string strCaseNo = ds.Tables[0].Rows[i]["Case #"].ToString();

                                if (ds != null)
                                {
                                    if (ds.Tables != null)
                                    {
                                        for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                                        {
                                            string strCaseOne = ds.Tables[0].Rows[k]["Case #"].ToString();
                                            for (int l = k + 1; l < ds.Tables[0].Rows.Count; l++)
                                            {
                                                string strCaseTwo = ds.Tables[0].Rows[l]["Case #"].ToString();
                                                if (strCaseOne == strCaseTwo)
                                                {
                                                    printi.ToString();
                                                    ds.Tables[0].Rows[l]["Claim Number"] = "";
                                                    ds.Tables[0].Rows[l][12] = "";
                                                    ds.Tables[0].Rows[l][13] = "";
                                                    ds.Tables[0].Rows[l]["Patient Name"] = "";
                                                    ds.Tables[0].Rows[l]["Case #"] = "";
                                                    ds.Tables[0].Rows[l]["WC_ADDRESS"] = "";

                                                    tblMain.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                                                    tblMain.DefaultCell.BorderColorTop = Color.WHITE;
                                                }
                                                //else
                                                //{
                                                //    ++printi;
                                                //    tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                                                //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Claim Number"]), fntContentTableHeading));
                                                //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Insurance Company"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Insurance Address"]), fntContentTableHeading));
                                                //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                                //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                                //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                                //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                                //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Spciality"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Min Date Of Service"]), fntContentTableHeading));
                                                //    tblMain.AddCell(new Phrase("$" + Convert.ToString(ds.Tables[0].Rows[i]["Bill Amount"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Max Date Of Service"]), fntContentTableHeading));
                                                //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                                //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Bill ID"]), fntContentTableHeading));
                                                //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                                //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Patient Name"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Case #"]), fntContentTableHeading));
                                                //}
                                            }
                                        }
                                    }
                                }
                                ++printi;
                                tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Claim Number"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Insurance Company"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Insurance Address"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Spciality"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Min Date Of Service"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("$" + Convert.ToString(ds.Tables[0].Rows[i]["Bill Amount"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Max Date Of Service"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Bill ID"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Patient Name"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Case #"]), fntContentTableHeading));

                            }
                        }
                        if (!ds.Tables[0].Rows[i]["Case #"].ToString().Equals(sCaseNumber))
                        {
                            ds.Tables[0].Rows[i]["show_top_border"] = "show";
                        }
                        else
                        {
                            ds.Tables[0].Rows[i]["show_top_border"] = "noshow";
                        }
                        sCaseNumber = ds.Tables[0].Rows[i]["Case #"].ToString();
                    }
                    if (iWCCount == false && _iCountWC > 0)
                    {
                        //document.Open();
                        //CreateHeader(document, writer, ds, I_POM_ID, headerDs);
                        //CreateFooter(document, writer, ds, I_POM_ID, headerDs);
                        //CreateMiddleHeader(document, writer, ds, I_POM_ID, headerDs);
                        ////CreateMiddleHeaderValuesWC(document, writer, ds, I_POM_ID, headerDs, printi.ToString());
                        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        //{
                        //    tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Claim Number"]), fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("Worker's Compensation Board " + Convert.ToString(ds.Tables[0].Rows[i]["WC_ADDRESS"]), fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Spciality"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Min Date Of Service"]), fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("$" + Convert.ToString(ds.Tables[0].Rows[i]["Bill Amount"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Max Date Of Service"]), fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Bill ID"]), fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Patient Name"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Case #"]), fntContentTableHeading));
                        //}
                    }

                    if (_iCountNF > 0)
                    {
                        
                        
                        //document.NewPage();
                        //CreateHeader(document, writer, ds, I_POM_ID, headerDs);
                        //CreateFooter(document, writer, ds, I_POM_ID, headerDs);
                        //CreateMiddleHeader(document, writer, ds, I_POM_ID, headerDs);
                        ////CreateMiddleHeaderValues(document, writer, ds, I_POM_ID, headerDs, printi.ToString());
                        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        //{
                        //    tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Claim Number"]), fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Insurance Company"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Insurance Address"]), fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Spciality"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Min Date Of Service"]), fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("$" + Convert.ToString(ds.Tables[0].Rows[i]["Bill Amount"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Max Date Of Service"]), fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Bill ID"]), fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase("", fntContentTableHeading));
                        //    tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Patient Name"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Case #"]), fntContentTableHeading));
                        //}
                        //doc.NewPage();
                        //doc.Add(tblMain);
                    }
                }
            }
        }

        //Middle table

        //footer Table
        float[] Wfooter = { 4f };
        PdfPTable tblfooter = new PdfPTable(Wfooter);
        tblfooter.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
        tblfooter.WidthPercentage = 100;
        tblfooter = CreateFooter(document, writer, ds, I_POM_ID, headerDs);
        //footer Table

        tblMainTable.AddCell(tblheader);
        tblMainTable.AddCell(tblMain);
        //tblMainTable.AddCell(tblMain);
        tblMainTable.AddCell(tblfooter);
        //doc.Add(tblMainTable);

        //iTextSharp.text.Document doc = new iTextSharp.text.Document();
        //PdfWriter.GetInstance(doc, new FileStream("HelloWorld.pdf", FileMode.Create));
        //doc.Open();
        //doc.Add(new Paragraph("Hello World!"));
        //doc.NewPage();
        //doc.Add(new Paragraph("Hello World on a new page!"));

        return tblMainTable;
    }
    
    public PdfPTable CreateHeader(iTextSharp.text.Document document, PdfWriter writer, DataSet ds, int I_POM_ID, DataSet headerDs)
    {
        float[] wBase = { 1f };
        PdfPTable tblBase = new PdfPTable(wBase);
        tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        tblBase.WidthPercentage = 100;

        float[] fHeader = { 1f, 1.7f, 1f };
        PdfPTable tblHeader = new PdfPTable(fHeader);
        tblHeader.SpacingBefore = 70;
        tblHeader.SpacingAfter = 10;
        tblHeader.DefaultCell.Border = Rectangle.NO_BORDER;
        PdfPCell header = new PdfPCell(new Phrase("POM ID : " + I_POM_ID, arialbolheader));
        header.Border = Rectangle.NO_BORDER;
        header.HorizontalAlignment = 0;
        header.Colspan = 3;
        tblHeader.AddCell(header);

        float[] wd1 = { 1f, 4f, 4f, 3f, 3f };
        PdfPTable tblVisit = new PdfPTable(wd1);
        tblVisit.WidthPercentage = 100;
        tblVisit.DefaultCell.BorderColor = Color.BLACK;

        iTextSharp.text.Font fntHeader = new Font(iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.NORMAL, iTextSharp.text.Color.BLACK));
        //1
        tblVisit.AddCell(new Phrase("Name and Address of Sender", fntHeader));

        //2
        tblVisit.AddCell(new Phrase(Convert.ToString(headerDs.Tables[0].Rows[0]["SZ_COMPANY_NAME"]) + "" + Convert.ToString(headerDs.Tables[0].Rows[0]["SZ_ADDRESS1"]) + "" + Convert.ToString(headerDs.Tables[0].Rows[0]["SZ_ADDRESS2"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

        // Table mail type
        PdfPTable tblMailHeading = new PdfPTable(1);
        tblMailHeading.WidthPercentage = 100;
        //3//to add inside type mail cell
        PdfPCell typeMailCell = new PdfPCell(new Phrase("Indicate type of Mail", fntHeader));
        tblMailHeading.AddCell(typeMailCell);

        PdfPTable tblMailTypes = new PdfPTable(2);
        PdfPCell c1 = new PdfPCell(new Phrase("Registered", fntHeader));
        PdfPCell c2 = new PdfPCell(new Phrase("Return receipt for merchandise", fntHeader));
        
        PdfPCell c3 = new PdfPCell(new Phrase("Insured", fntHeader));
        PdfPCell c4 = new PdfPCell(new Phrase("Intl. recorded del.", fntHeader));

        PdfPCell c5 = new PdfPCell(new Phrase("COD", fntHeader));
        PdfPCell c6 = new PdfPCell(new Phrase("Express mail", fntHeader));

        PdfPCell c7 = new PdfPCell(new Phrase("Certified", fntHeader));

        tblMailTypes.AddCell(c1);
        tblMailTypes.AddCell(c2);
        tblMailTypes.AddCell(c3);
        tblMailTypes.AddCell(c4);
        tblMailTypes.AddCell(c5);
        tblMailTypes.AddCell(c6);
        tblMailTypes.AddCell(c7);
        tblMailHeading.AddCell(tblMailTypes);
        tblVisit.AddCell(tblMailHeading);

        PdfPTable tblregmail = new PdfPTable(1);
        tblregmail.WidthPercentage = 100;
        tblregmail.AddCell(new Phrase("Check appropriate block for Registered Mail", fntHeader));

        PdfPTable tblregmailoptions = new PdfPTable(1);
        tblregmailoptions.AddCell(new Phrase("With postal Insurance", fntHeader));
        tblregmailoptions.AddCell(new Phrase("Without postal Insurance", fntHeader));
        tblregmail.AddCell(tblregmailoptions);
        tblVisit.AddCell(tblregmail);

        PdfPTable tblstamp = new PdfPTable(1);
        tblstamp.WidthPercentage = 100;
        tblstamp.AddCell(new Phrase("Affix stamp here if issued as certificate of mailing or for additional copies of this bill", fntHeader));

        PdfPTable tblstampColumns = new PdfPTable(1);
        tblstampColumns.AddCell(new Phrase("Postmark and Date of Receipt", fntHeader));
        //tblstampColumns.AddCell(new Phrase("Without postal Insurance", fntHeader));
        tblstamp.AddCell(tblstampColumns);
        tblVisit.AddCell(tblstamp);
        tblBase.AddCell(tblVisit);
        return tblBase;
    }
    public PdfPTable CreateFooter(iTextSharp.text.Document document, PdfWriter writer, DataSet ds, int I_POM_ID, DataSet headerDs)
    {
        float[] wBase = { 1f };
        PdfPTable tblBase = new PdfPTable(wBase);
        tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        tblBase.WidthPercentage = 100;

        //footer Table
        float[] wd4 = { 0.50f, 1.5f, 1.5f, 4f };
        PdfPTable tblFooter = new PdfPTable(wd4);
        tblFooter.WidthPercentage = 100;
        tblFooter.DefaultCell.BorderColor = Color.BLACK;

        //1
        tblFooter.AddCell(new Phrase("Total Number of Pieces Listed by Sender", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

        //2
        tblFooter.AddCell(new Phrase("Total Number of Pieces Received at Post Office", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

        //3
        tblFooter.AddCell(new Phrase("Postmaster, Per (Name of receiving employee)", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

        //4
        tblFooter.AddCell(new Phrase("The full declaration of value is required on all domestic and international registered mail.The maximum indemnity payable for the reconstruction of nonnegotiable documents under Express Mail document reconstruction insurance is $500 per piece subject to additional limitations for multiple pieces lost or damages in a single catastrophic occurrence. The maximum indemnity payable on Express Mail merchandise is $500, but optional Express Mail Service merchandise insurance is available for up to $5000 to some, but not all countries. The maximum indemnity payable is $25,000 for registered mail. See Domestic Mail Manual R900, S913, and S921 for limitations of coverage on insured and COD mail. See International Mail Manual for limitations of coverage on international mail. Special handling charges apply only to Standard Mail (A) and Standard Mail (B) parcels.", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
        //footer Table

        tblBase.AddCell(tblFooter);
        return tblBase;
    }
    public PdfPTable CreateMiddleHeader(iTextSharp.text.Document document, PdfWriter writer, DataSet ds, int I_POM_ID, DataSet headerDs)
    {
        //Middle table
        float[] wd3 = { 0.25f, 0.60f, 2f, 0.60f, 0.40f, 1f, 1.5f, 1f, 1.5f, 0.50f, 0.50f, 0.50f, 1.5f };
        PdfPTable tblMain = new PdfPTable(wd3);
        tblMain.WidthPercentage = 100;
        tblMain.DefaultCell.BorderColor = Color.BLACK;

        // Content table header
        Font fntContentTableHeading = new Font(iTextSharp.text.FontFactory.GetFont("Arial", 6, Font.NORMAL, iTextSharp.text.Color.BLACK));
        tblMain.AddCell(new Phrase("Line", fntContentTableHeading));
        tblMain.AddCell(new Phrase("Article Number", fntContentTableHeading));
        tblMain.AddCell(new Phrase("Name of Addressee, Street, and Post Office Address", fntContentTableHeading));
        tblMain.AddCell(new Phrase("Postage", fntContentTableHeading));
        tblMain.AddCell(new Phrase("Fee", fntContentTableHeading));
        tblMain.AddCell(new Phrase("Handling charge", fntContentTableHeading));
        tblMain.AddCell(new Phrase("Act.Value(If regis.)", fntContentTableHeading));
        tblMain.AddCell(new Phrase("Insured Value", fntContentTableHeading));
        tblMain.AddCell(new Phrase("Due Sender if COD", fntContentTableHeading));
        tblMain.AddCell(new Phrase("R.R Fee", fntContentTableHeading));
        tblMain.AddCell(new Phrase("S.D Fee", fntContentTableHeading));
        tblMain.AddCell(new Phrase("S.H Fee", fntContentTableHeading));
        tblMain.AddCell(new Phrase("Rest Del Fee Remarks", fntContentTableHeading));

        return tblMain;
    }
    public PdfPTable CreateMiddleHeaderValues(iTextSharp.text.Document document, PdfWriter writer, DataSet ds, int I_POM_ID, DataSet headerDs,string printi)
    {
        //Middle table
        float[] wd3 = { 0.25f, 0.60f, 2f, 0.60f, 0.40f, 1f, 1.5f, 1f, 1.5f, 0.50f, 0.50f, 0.50f, 1.5f };
        PdfPTable tblMain = new PdfPTable(wd3);
        tblMain.WidthPercentage = 100;
        tblMain.DefaultCell.BorderColor = Color.BLACK;

        // Content table header
        Font fntContentTableHeading = new Font(iTextSharp.text.FontFactory.GetFont("Arial", 6, Font.NORMAL, iTextSharp.text.Color.BLACK));
        if (ds != null)
        {
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sCaseNumber = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string strCasetype = ds.Tables[0].Rows[i]["CaseType"].ToString();
                        if (strCasetype == "WC000000000000000001")
                        {
                            string strProvider = ds.Tables[0].Rows[i]["Provider"].ToString();
                            if (strProvider.Equals(ds.Tables[0].Rows[i]["Provider"].ToString()))
                            {
                                tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Claim Number"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Insurance Company"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Insurance Address"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Spciality"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Min Date Of Service"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("$" + Convert.ToString(ds.Tables[0].Rows[i]["Bill Amount"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Max Date Of Service"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Bill ID"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Patient Name"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Case #"]), fntContentTableHeading));
                            }
                        }
                    }
                }
            }
        }

        return tblMain;
    }
    public PdfPTable CreateMiddleHeaderValuesWC(iTextSharp.text.Document document, PdfWriter writer, DataSet ds, int I_POM_ID, DataSet headerDs,string printi)
    {
        //Middle table
        float[] wd3 = { 0.25f, 0.60f, 2f, 0.60f, 0.40f, 1f, 1.5f, 1f, 1.5f, 0.50f, 0.50f, 0.50f, 1.5f };
        PdfPTable tblMain = new PdfPTable(wd3);
        tblMain.WidthPercentage = 100;
        tblMain.DefaultCell.BorderColor = Color.BLACK;

        // Content table header
        Font fntContentTableHeading = new Font(iTextSharp.text.FontFactory.GetFont("Arial", 6, Font.NORMAL, iTextSharp.text.Color.BLACK));
        if (ds != null)
        {
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sCaseNumber = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string strCasetype = ds.Tables[0].Rows[i]["CaseType"].ToString();
                        if (strCasetype == "WC000000000000000001")
                        {
                            string strProvider = ds.Tables[0].Rows[i]["Provider"].ToString();
                            if (strProvider.Equals(ds.Tables[0].Rows[i]["Provider"].ToString()))
                            {
                                tblMain.AddCell(new Phrase((printi).ToString(), fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Claim Number"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("Worker's Compensation Board " + Convert.ToString(ds.Tables[0].Rows[i]["WC_ADDRESS"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Spciality"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Min Date Of Service"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("$" + Convert.ToString(ds.Tables[0].Rows[i]["Bill Amount"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["Max Date Of Service"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Bill ID"]), fntContentTableHeading));
                                tblMain.AddCell(new Phrase("", fntContentTableHeading));
                                tblMain.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[i]["Patient Name"]) + "" + Convert.ToString(ds.Tables[0].Rows[i]["Case #"]), fntContentTableHeading));
                            }
                        }
                    }
                }
            }
        }

        return tblMain;
    }
}