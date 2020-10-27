using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

/// <summary>
/// Summary description for CoverContract_pdf
/// </summary>
public class CoverContract_pdf
{
    private string strconn;

    private SqlConnection conn;

    private SqlCommand cmd;

    private SqlDataReader sdr;

    public CoverContract_pdf()
    {
        this.strconn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public void GenerateCoverContract(string companyID, string caseID, string sz_bill_no, string path,out bool isGenerated)
    {
        const int FONT_SIZE = 8;
        const int LAST_PAGE_HEADER = 10;
        const int HEADER_FONT_SIZE = 16;
        const int SUBHEADER_FONT_SIZE = 10;
        const string FONT_NAME = "Arial";
        isGenerated = false;
        try
        {
            MemoryStream ms = new MemoryStream();
            FileStream fs = new FileStream(path, System.IO.FileMode.OpenOrCreate);
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 20, 20, 10, 10);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            DataSet ds = this.getvalue(companyID, caseID, sz_bill_no);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    
                    {
                        float[] Table = { 4f };
                        PdfPTable Table1 = new PdfPTable(Table);
                        Table1.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
                        Table1.WidthPercentage = 100;

                        string headerImgPath = ConfigurationSettings.AppSettings["ContractHeaderImagePath"].ToString();
                        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(headerImgPath);
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(jpg);
                        float[] Heading = { 4f };
                        PdfPTable Head = new PdfPTable(Heading);
                        Head.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        Head.WidthPercentage = 100;
                        Head.AddCell(ImgObj);
                        document.Add(Head);


                        float[] Date = { 4f, 35f, 140f };
                        PdfPTable date = new PdfPTable(Date);
                        date.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        date.WidthPercentage = 100;
                        date.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        date.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        date.AddCell(new Phrase("Date:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        date.AddCell(new Phrase(ds.Tables[0].Rows[0]["Bill_Date"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        document.Add(date);


                        float[] Datespace = { 4f };
                        PdfPTable dsp = new PdfPTable(Datespace);
                        dsp.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        dsp.WidthPercentage = 100;
                        dsp.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        dsp.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        dsp.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        document.Add(dsp);

                        float[] InsuranceCompany = { 4f, 35f, 140f };
                        PdfPTable insu_Comp = new PdfPTable(InsuranceCompany);
                        insu_Comp.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        insu_Comp.WidthPercentage = 100;
                        insu_Comp.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        insu_Comp.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_Comp.AddCell(new Phrase("Insurance Company:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_Comp.AddCell(new Phrase(ds.Tables[0].Rows[0]["InsuranceName"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        insu_Comp.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_Comp.AddCell(new Phrase("Insurance Address:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_Comp.AddCell(new Phrase(ds.Tables[0].Rows[0]["InsuranceAddress"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        document.Add(insu_Comp);

                        float[] Datespace1 = { 4f };
                        PdfPTable dsp1 = new PdfPTable(Datespace1);
                        dsp1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        dsp1.WidthPercentage = 100;
                        dsp1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        dsp1.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        dsp1.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        document.Add(dsp1);

                        //float[] PN = { 35f, 105f, 35f };
                        //PdfPTable PatientName = new PdfPTable(PN);
                        //PatientName.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        //PatientName.WidthPercentage = 100;
                        //PatientName.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        //PatientName.AddCell(new Phrase("Patient's Name:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        //PatientName.AddCell(new Phrase(ds.Tables[0].Rows[0]["PatinetLName"].ToString() + ' ' + ds.Tables[0].Rows[0]["PatinetFName"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        //PatientName.AddCell(Table1);
                        //document.Add(PatientName);

                        float[] InsuranceAddress = { 4f, 35f, 140f };
                        PdfPTable insu_addrs = new PdfPTable(InsuranceAddress);
                        insu_addrs.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        insu_addrs.WidthPercentage = 100;
                        insu_addrs.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        insu_addrs.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase("Patient's Name:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase(ds.Tables[0].Rows[0]["PatinetLName"].ToString() + ' ' + ds.Tables[0].Rows[0]["PatinetFName"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase("Claim #:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase(ds.Tables[0].Rows[0]["ClaimNo"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase("Date of Loss:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase(ds.Tables[0].Rows[0]["DOA"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase("Date of Service:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase(ds.Tables[0].Rows[0]["DOS"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase("Amount Billed:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        insu_addrs.AddCell(new Phrase(ds.Tables[0].Rows[0]["BILL_AMOUNT"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        document.Add(insu_addrs);


                        float[] space = { 4f };
                        PdfPTable sp = new PdfPTable(space);
                        sp.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        sp.WidthPercentage = 100;
                        sp.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        sp.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        sp.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                        document.Add(sp);



                        float[] PatientAccordance1 = { 0.1f, 4f };
                        PdfPTable patient_accordance1 = new PdfPTable(PatientAccordance1);
                        patient_accordance1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        patient_accordance1.WidthPercentage = 100;
                        patient_accordance1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        patient_accordance1.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        patient_accordance1.DefaultCell.SetLeading(0, 1.2f);
                        patient_accordance1.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        string APH = "Notwithstanding the charges set forth on the bill contemporaneously sent with this letter, the treating provider will accept as payment in full, the amount of $ ";
                        string APH1 = "";
                        if (ds.Tables[0].Rows[0]["CONTRACT_AMOUNT"].ToString().ToLower() != "")
                        {
                            APH1 += ds.Tables[0].Rows[0]["CONTRACT_AMOUNT"].ToString();
                            //patient_accordance.AddCell((new Phrase(ds.Tables[0].Rows[0]["BILL_AMOUNT"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK))));
                            //APH += patient_accordance;
                        }

                        APH1 += ", the sum which the parties have previously agreed to as full payment on a voluntarily paid claims basis for date of service ";
                        if (ds.Tables[0].Rows[0]["DOS"].ToString().ToLower() != "")
                        {
                            APH1 += ds.Tables[0].Rows[0]["DOS"].ToString();
                        }
                        APH1 += ", and as more fully set forth in the terms of the subject agreement between the parties dated ";
                        if (ds.Tables[0].Rows[0]["date"].ToString().ToLower() != "")
                        {
                            APH1 += ds.Tables[0].Rows[0]["date"].ToString();
                        }
                        APH1 += ".";
                        if (APH1 != "")
                        {
                            APH += APH1;
                        }
                        patient_accordance1.AddCell(new Phrase(APH, iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        document.Add(patient_accordance1);


                        //float[] PatientAccordance2 = { 0.1f,4f };
                        //PdfPTable patient_accordance2 = new PdfPTable(PatientAccordance2);
                        //patient_accordance2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        //patient_accordance2.WidthPercentage = 100;
                        //patient_accordance2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        //patient_accordance2.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        //string APH2 = "date of service ";
                        //string APH3 = "";
                        //if (ds.Tables[0].Rows[0]["DOS"].ToString().ToLower() != "")
                        //{
                        //    APH3 += ds.Tables[0].Rows[0]["DOS"].ToString();
                        //}
                        //APH3 += " forward, as set forth in the subject Agreement between the parties dated ";
                        //if (ds.Tables[0].Rows[0]["date"].ToString().ToLower() != "")
                        //{
                        //    APH3 += ds.Tables[0].Rows[0]["date"].ToString();
                        //}
                        //APH3 += ".";
                        //if (APH3 != "")
                        //{
                        //    APH2 += APH3;
                        //}
                        //patient_accordance2.AddCell(new Phrase(APH2, iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        //document.Add(patient_accordance2);

                        float[] Space2 = { 4f };
                        PdfPTable sp2 = new PdfPTable(Space2);
                        sp2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        sp2.WidthPercentage = 100;
                        sp2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        sp2.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        document.Add(sp2);

                        float[] payment = { 0.1f, 4f };
                        PdfPTable pay = new PdfPTable(payment);
                        pay.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        pay.WidthPercentage = 100;
                        pay.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        pay.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        pay.DefaultCell.SetLeading(0, 1.2f);
                        pay.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        pay.AddCell(new Phrase("          In the event you choose for any reason, not to make payment as set forth above, the treating provider has previously reserved it’s rights (in said agreement) , and may seek payment of this bill through the appropriate collection efforts. In seeking such payment through collection efforts, treating provider may do so as otherwise provided for in the aforementioned agreement and without providing for any previously agreed upon reductions.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        document.Add(pay);



                        float[] Space1 = { 4f };
                        PdfPTable sp1 = new PdfPTable(Space1);
                        sp1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        sp1.WidthPercentage = 100;
                        sp1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        sp1.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        sp1.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        document.Add(sp1);

                        float[] Sincerely = { 0.1f, 4f };
                        PdfPTable sincerely = new PdfPTable(Sincerely);
                        sincerely.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        sincerely.WidthPercentage = 100;
                        sincerely.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        sincerely.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));
                        sincerely.AddCell(new Phrase("Sincerely,", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));

                        document.Add(sincerely);


                        float[] Space12 = { 4f };
                        PdfPTable sp12 = new PdfPTable(Space12);
                        sp12.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        sp12.WidthPercentage = 100;
                        sp12.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        sp12.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        sp12.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                        document.Add(sp12);

                        float[] Sincerely1 = { 0.1f, 4f };
                        PdfPTable sincerely1 = new PdfPTable(Sincerely1);
                        sincerely1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        sincerely1.WidthPercentage = 100;
                        sincerely1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        sincerely1.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));
                        sincerely1.AddCell(new Phrase("Billing/Collection Department", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));
                        document.Add(sincerely1);
                        document.Close();
                        isGenerated = true;
                    }
           
        }

        catch (Exception ex)
        {
            isGenerated = false;

        }
    }
    public void GenerateCoverContractGorMedicalFacility(string companyID, string caseID, string sz_bill_no, string path,out bool isGenerated)
    {
        const int FONT_SIZE = 8;
        const int LAST_PAGE_HEADER = 10;
        const int HEADER_FONT_SIZE = 16;
        const int SUBHEADER_FONT_SIZE = 10;
        const string FONT_NAME = "Arial";

        try
        {
            isGenerated = false;
            MemoryStream ms = new MemoryStream();
            FileStream fs = new FileStream(path, System.IO.FileMode.OpenOrCreate);
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 20, 20, 10, 10);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            DataSet ds = this.getvalueForMedicalFacility(companyID, caseID, sz_bill_no);
            if (ds.Tables[0].Rows[0]["CONTRACT_AMOUNT"] is DBNull)
                isGenerated = false;
            else
            {
                isGenerated = true;
                float[] Table = { 4f };
                PdfPTable Table1 = new PdfPTable(Table);
                Table1.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
                Table1.WidthPercentage = 100;

                string headerImgPath = ConfigurationSettings.AppSettings["ContractHeaderImagePath"].ToString();
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(headerImgPath);
                iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(jpg);
                float[] Heading = { 4f };
                PdfPTable Head = new PdfPTable(Heading);
                Head.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                Head.WidthPercentage = 100;
                Head.AddCell(ImgObj);
                document.Add(Head);


                float[] Date = { 4f, 35f, 140f };
                PdfPTable date = new PdfPTable(Date);
                date.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                date.WidthPercentage = 100;
                date.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                date.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                date.AddCell(new Phrase("Date:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                date.AddCell(new Phrase(ds.Tables[0].Rows[0]["Bill_Date"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                document.Add(date);


                float[] Datespace = { 4f };
                PdfPTable dsp = new PdfPTable(Datespace);
                dsp.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                dsp.WidthPercentage = 100;
                dsp.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                dsp.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                dsp.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                document.Add(dsp);

                float[] InsuranceCompany = { 4f, 35f, 140f };
                PdfPTable insu_Comp = new PdfPTable(InsuranceCompany);
                insu_Comp.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                insu_Comp.WidthPercentage = 100;
                insu_Comp.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                insu_Comp.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_Comp.AddCell(new Phrase("Insurance Company:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_Comp.AddCell(new Phrase(ds.Tables[0].Rows[0]["InsuranceName"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                insu_Comp.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_Comp.AddCell(new Phrase("Insurance Address:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_Comp.AddCell(new Phrase(ds.Tables[0].Rows[0]["InsuranceAddress"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                document.Add(insu_Comp);

                float[] Datespace1 = { 4f };
                PdfPTable dsp1 = new PdfPTable(Datespace1);
                dsp1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                dsp1.WidthPercentage = 100;
                dsp1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                dsp1.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                dsp1.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                document.Add(dsp1);

                //float[] PN = { 35f, 105f, 35f };
                //PdfPTable PatientName = new PdfPTable(PN);
                //PatientName.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                //PatientName.WidthPercentage = 100;
                //PatientName.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                //PatientName.AddCell(new Phrase("Patient's Name:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                //PatientName.AddCell(new Phrase(ds.Tables[0].Rows[0]["PatinetLName"].ToString() + ' ' + ds.Tables[0].Rows[0]["PatinetFName"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                //PatientName.AddCell(Table1);
                //document.Add(PatientName);

                float[] InsuranceAddress = { 4f, 35f, 140f };
                PdfPTable insu_addrs = new PdfPTable(InsuranceAddress);
                insu_addrs.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                insu_addrs.WidthPercentage = 100;
                insu_addrs.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                insu_addrs.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase("Patient's Name:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase(ds.Tables[0].Rows[0]["PatinetLName"].ToString() + ' ' + ds.Tables[0].Rows[0]["PatinetFName"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase("Claim #:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase(ds.Tables[0].Rows[0]["ClaimNo"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase("Date of Loss:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase(ds.Tables[0].Rows[0]["DOA"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase("Date of Service:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase(ds.Tables[0].Rows[0]["DOS"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase("Amount Billed:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                insu_addrs.AddCell(new Phrase(ds.Tables[0].Rows[0]["BILL_AMOUNT"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                document.Add(insu_addrs);


                float[] space = { 4f };
                PdfPTable sp = new PdfPTable(space);
                sp.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                sp.WidthPercentage = 100;
                sp.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                sp.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                sp.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
                document.Add(sp);



                float[] PatientAccordance1 = { 0.1f, 4f };
                PdfPTable patient_accordance1 = new PdfPTable(PatientAccordance1);
                patient_accordance1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                patient_accordance1.WidthPercentage = 100;
                patient_accordance1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                patient_accordance1.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                patient_accordance1.DefaultCell.SetLeading(0, 1.2f);
                patient_accordance1.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                string APH = "Notwithstanding the charges set forth on the bill contemporaneously sent with this letter, the treating provider will accept as payment in full, the amount of $ ";
                string APH1 = "";
                if (ds.Tables[0].Rows[0]["CONTRACT_AMOUNT"].ToString().ToLower() != "")
                {
                    APH1 += ds.Tables[0].Rows[0]["CONTRACT_AMOUNT"].ToString();
                    //patient_accordance.AddCell((new Phrase(ds.Tables[0].Rows[0]["BILL_AMOUNT"].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK))));
                    //APH += patient_accordance;
                }

                APH1 += ", the sum which the parties have previously agreed to as full payment on a voluntarily paid claims basis for date of service ";
                if (ds.Tables[0].Rows[0]["DOS"].ToString().ToLower() != "")
                {
                    APH1 += ds.Tables[0].Rows[0]["DOS"].ToString();
                }
                APH1 += ", and as more fully set forth in the terms of the subject agreement between the parties dated ";
                if (ds.Tables[0].Rows[0]["date"].ToString().ToLower() != "")
                {
                    APH1 += ds.Tables[0].Rows[0]["date"].ToString();
                }
                APH1 += ".";
                if (APH1 != "")
                {
                    APH += APH1;
                }
                patient_accordance1.AddCell(new Phrase(APH, iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                document.Add(patient_accordance1);


                //float[] PatientAccordance2 = { 0.1f,4f };
                //PdfPTable patient_accordance2 = new PdfPTable(PatientAccordance2);
                //patient_accordance2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                //patient_accordance2.WidthPercentage = 100;
                //patient_accordance2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                //patient_accordance2.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                //string APH2 = "date of service ";
                //string APH3 = "";
                //if (ds.Tables[0].Rows[0]["DOS"].ToString().ToLower() != "")
                //{
                //    APH3 += ds.Tables[0].Rows[0]["DOS"].ToString();
                //}
                //APH3 += " forward, as set forth in the subject Agreement between the parties dated ";
                //if (ds.Tables[0].Rows[0]["date"].ToString().ToLower() != "")
                //{
                //    APH3 += ds.Tables[0].Rows[0]["date"].ToString();
                //}
                //APH3 += ".";
                //if (APH3 != "")
                //{
                //    APH2 += APH3;
                //}
                //patient_accordance2.AddCell(new Phrase(APH2, iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                //document.Add(patient_accordance2);

                float[] Space2 = { 4f };
                PdfPTable sp2 = new PdfPTable(Space2);
                sp2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                sp2.WidthPercentage = 100;
                sp2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                sp2.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                document.Add(sp2);

                float[] payment = { 0.1f, 4f };
                PdfPTable pay = new PdfPTable(payment);
                pay.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pay.WidthPercentage = 100;
                pay.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pay.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                pay.DefaultCell.SetLeading(0, 1.2f);
                pay.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                pay.AddCell(new Phrase("          In the event you choose for any reason, not to make payment as set forth above, the treating provider has previously reserved it’s rights (in said agreement) , and may seek payment of this bill through the appropriate collection efforts. In seeking such payment through collection efforts, treating provider may do so as otherwise provided for in the aforementioned agreement and without providing for any previously agreed upon reductions.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                document.Add(pay);



                float[] Space1 = { 4f };
                PdfPTable sp1 = new PdfPTable(Space1);
                sp1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                sp1.WidthPercentage = 100;
                sp1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                sp1.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                sp1.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                document.Add(sp1);

                float[] Sincerely = { 0.1f, 4f };
                PdfPTable sincerely = new PdfPTable(Sincerely);
                sincerely.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                sincerely.WidthPercentage = 100;
                sincerely.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                sincerely.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));
                sincerely.AddCell(new Phrase("Sincerely,", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));

                document.Add(sincerely);


                float[] Space12 = { 4f };
                PdfPTable sp12 = new PdfPTable(Space12);
                sp12.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                sp12.WidthPercentage = 100;
                sp12.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                sp12.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                sp12.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
                document.Add(sp12);

                float[] Sincerely1 = { 0.1f, 4f };
                PdfPTable sincerely1 = new PdfPTable(Sincerely1);
                sincerely1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                sincerely1.WidthPercentage = 100;
                sincerely1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                sincerely1.AddCell(new Phrase(" ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));
                sincerely1.AddCell(new Phrase("Billing/Collection Department", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK)));
                document.Add(sincerely1);
                document.Close();
            }

        }

        catch (Exception ex)
        {

            isGenerated = false;
        }
    }
    public DataSet getvalue(string cmpID, string caseID, string billNo)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();
        sqlConnection.Open();
        try
        {

            SqlCommand sqlcom = new SqlCommand("GetContractData", sqlConnection);
            sqlcom.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.AddWithValue("@CompanyId", cmpID);
            sqlcom.Parameters.AddWithValue("@CaseId", caseID);
            sqlcom.Parameters.AddWithValue("@BillNo", billNo);
            SqlDataAdapter sda = new SqlDataAdapter(sqlcom);
            sda.Fill(ds);
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return ds;
    }
    public DataSet getvalueForMedicalFacility(string cmpID, string caseID, string billNo)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();
        sqlConnection.Open();
        try
        {

            SqlCommand sqlcom = new SqlCommand("GetContractDataForMedicalFacility", sqlConnection);
            sqlcom.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.AddWithValue("@CompanyId", cmpID);
            sqlcom.Parameters.AddWithValue("@CaseId", caseID);
            sqlcom.Parameters.AddWithValue("@BillNo", billNo);
            SqlDataAdapter sda = new SqlDataAdapter(sqlcom);
            sda.Fill(ds);
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return ds;
    }
}
