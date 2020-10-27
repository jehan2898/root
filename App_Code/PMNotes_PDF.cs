using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data.SqlClient;
using PMNotes;
using System.Collections;
using System.Configuration;

/// <summary>
/// Summary description for PMNotes_PDF
/// </summary>
/// 

namespace PMNotes
{
    public class PMNotes_PDF
    {
        string patientSignPath = "";
        string drSignPath = "";
        string SZ_DOCTOR_ID = "";
        iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 18, 18, 15, 15);
        PdfWriter writer;

        iTextSharp.text.Font arialheaderitalic = FontFactory.GetFont("Arial", 9f, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.Color.BLACK);
        iTextSharp.text.Font arialboldheader = FontFactory.GetFont("Arial", 9f, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
        iTextSharp.text.Font arialbold = FontFactory.GetFont("Arial", 8f, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
        iTextSharp.text.Font arialboldadj = FontFactory.GetFont("Arial", 7f, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
        iTextSharp.text.Font arial = FontFactory.GetFont("Arial", 8f, iTextSharp.text.Color.BLACK);
        iTextSharp.text.Font arialsmall = FontFactory.GetFont("Arial", 7f, iTextSharp.text.Color.BLACK);
        iTextSharp.text.Font arialboldsmall = FontFactory.GetFont("Arial", 6.4f, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
        iTextSharp.text.Font arialsmallitalic = FontFactory.GetFont("Arial", 7f, iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK);
        iTextSharp.text.Font arialboldsmallitalic = FontFactory.GetFont("Arial", 6.4f, iTextSharp.text.Font.BOLDITALIC, iTextSharp.text.Color.BLACK);

        string strsqlCon = System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString();
        public PMNotes_PDF()
        {

        }

        public void GeneratePMReport(string filePath, string billNumber, string UserID, string companyID)
        {
            MemoryStream m = new MemoryStream();

            writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            DataSet ds = new DataSet();
            ds = GetEventIDs(billNumber);

            PMNotes_EVENTID_DAO pmeventid = new PMNotes_EVENTID_DAO();
            PMNotes_Data pd = new PMNotes_Data();
            ArrayList arr = new ArrayList();

            pmeventid = pd.GetEventID(ds);
            arr = pmeventid.ar;

            for (int i = 0; i < arr.Count; i++)
            {
                pmeventid = (PMNotes_EVENTID_DAO)arr[i];
                string i_event_id = pmeventid.I_EVENT_ID.ToString();

                DataSet ds1 = new DataSet();
                ds1 = GetEventDetails(i_event_id);

                PMNotes_DAO oPmNotesDao = new PMNotes_DAO();
                oPmNotesDao = pd.GetPMEventData(ds1);

                #region Procedure Code

                SZ_DOCTOR_ID = GetDoctorID(i_event_id);
                DataSet dsAllProcCode = new DataSet();
                dsAllProcCode = GetAllProcedureCode(SZ_DOCTOR_ID);

                DataSet dsProcCodeList = new DataSet();
                dsProcCodeList = GetProcedureCodeList(i_event_id);

                PMNotes_ProcCode_DAO pmProcCodedata = new PMNotes_ProcCode_DAO();
                pmProcCodedata = pd.GetAllProcCode(dsAllProcCode);

                PMNotes_ProcCode_DAO pmProcCodelist = new PMNotes_ProcCode_DAO();
                pmProcCodelist = pd.GetSelectedProcCode(dsProcCodeList);

                #endregion

                GeneratePDF(writer, oPmNotesDao, pmProcCodedata, pmProcCodelist);
                if (i < arr.Count)
                    document.NewPage();
            }
            document.Close();
                
        }

        private string getFileName()
        {
            String szFileName;
            DateTime currentDate;
            currentDate = DateTime.Now;
            szFileName = getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms") + ".pdf";
            return szFileName;
        }

        private string getRandomNumber()
        {
            System.Random objRandom = new Random();
            return objRandom.Next(1, 10000).ToString();
        }

        public void GeneratePDF(PdfWriter writer, PMNotes_DAO oPmNotesDao, PMNotes_ProcCode_DAO pmProcCodedata, PMNotes_ProcCode_DAO pmProcCodelist)
        {
            string FontStyle = "Arial";
            int MidHeadtSize = 6;
            string chkboxpath = ConfigurationSettings.AppSettings["CHchkboxpath"];
            iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(chkboxpath + "/checkbox.JPG");
            iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(chkboxpath + "/uncheckbox.JPG");

            float[] widthbase = { 4f };
            PdfPTable tblBase = new PdfPTable(widthbase);
            tblBase.WidthPercentage = 100;
            tblBase.TotalWidth = 700;
            tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblBase.DefaultCell.Colspan = 1;

            tblBase.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            tblBase.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
            tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblBase.DefaultCell.BorderWidth = 2f;
            tblBase.AddCell(new Phrase(((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME.ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            tblBase.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblBase.AddCell(new Phrase("INITIAL E X A M I N A T I O N", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fBillingInfo = { 4f, 4f, 4f, 4f };
            PdfPTable tblHeader = new PdfPTable(fBillingInfo);
            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblHeader.DefaultCell.FixedHeight = 14f;
            tblHeader.WidthPercentage = 100;

            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblHeader.AddCell(new Phrase("Patient's Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;

            tblHeader.AddCell(new Phrase(oPmNotesDao.PatientName, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase("Date of Accident", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblHeader.AddCell(new Phrase(oPmNotesDao.DateOfAccident.ToString("dd/MM/yyyy"), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));

            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.NO_BORDER;
            tblHeader.AddCell(new Phrase("Date of Examination", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblHeader.AddCell(new Phrase(oPmNotesDao.VisitDate.ToString("dd/MM/yyyy"), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase("Date of Birth", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblHeader.AddCell(new Phrase(oPmNotesDao.DOB.ToString("dd/MM/yyyy"), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));

            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblHeader.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblHeader.AddCell(new Phrase("Address:", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblHeader.AddCell(new Phrase(oPmNotesDao.PatientAddress, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblHeader.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblHeader.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblHeader.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;

            float[] fCurrentAllergies = { 4f };
            PdfPTable tblCurrentAllergies = new PdfPTable(fCurrentAllergies);
            tblCurrentAllergies.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblCurrentAllergies.DefaultCell.FixedHeight = 14f;
            tblCurrentAllergies.WidthPercentage = 100;

            tblCurrentAllergies.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblCurrentAllergies.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            tblCurrentAllergies.AddCell(new Phrase("Medical History", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));


            float[] fCurrentAllergiesData = { 4f, 4f, 4f, 4f };
            PdfPTable tblCurrentAllergiesData = new PdfPTable(fCurrentAllergiesData);
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblCurrentAllergiesData.DefaultCell.FixedHeight = 14f;
            tblCurrentAllergiesData.WidthPercentage = 100;

            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase("Current Allergies:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase(oPmNotesDao.CurrentAllergies, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.AddCell(new Phrase("Current Medications:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase(oPmNotesDao.CurrentMedications, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;

            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase("Vitals:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase("Height:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase(oPmNotesDao.Height, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.AddCell(new Phrase("Weight", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase(oPmNotesDao.Weight, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase("Past History:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase("Medical:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase(oPmNotesDao.Medical, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.AddCell(new Phrase("Injuries", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase(oPmNotesDao.Injuries, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase("Hospitalization/Surgeries:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase(oPmNotesDao.Surguries, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.AddCell(new Phrase("Social History", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase(oPmNotesDao.SocialHistory, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase("Smoking Status:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase(oPmNotesDao.SmokingStatus, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.AddCell(new Phrase("Alchohal use", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase(oPmNotesDao.AlcoholUse, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase("Drug Use:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase(oPmNotesDao.DrugUse, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.AddCell(new Phrase("Employment", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblCurrentAllergiesData.AddCell(new Phrase(oPmNotesDao.Employement, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentAllergiesData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;


            float[] fCurrentComplaints = { 4f };
            PdfPTable tblCurrentComplaints = new PdfPTable(fCurrentComplaints);
            tblCurrentComplaints.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblCurrentComplaints.DefaultCell.FixedHeight = 14f;
            tblCurrentComplaints.WidthPercentage = 100;

            tblCurrentComplaints.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblCurrentComplaints.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            tblCurrentComplaints.AddCell(new Phrase("Current Complaints", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fCurrentComplaintsData = { 4f, 4f, 4f, 4f };
            PdfPTable tblCurrentComplaintsData = new PdfPTable(fCurrentAllergiesData);
            tblCurrentComplaintsData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblCurrentComplaintsData.DefaultCell.FixedHeight = 14f;
            tblCurrentComplaintsData.WidthPercentage = 100;

            tblCurrentComplaintsData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblCurrentComplaintsData.AddCell(new Phrase("Chief Complaint:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentComplaintsData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblCurrentComplaintsData.AddCell(new Phrase(oPmNotesDao.ChiefComplaint, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentComplaintsData.AddCell(new Phrase("Reason for Visit:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentComplaintsData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblCurrentComplaintsData.AddCell(new Phrase(oPmNotesDao.ReasonForVisit, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblCurrentComplaintsData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblCurrentComplaintsData.AddCell(new Phrase("ROS:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblCurrentComplaintsData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblCurrentComplaintsData.AddCell(new Phrase(oPmNotesDao.ROS, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentComplaintsData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentComplaintsData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblCurrentComplaintsData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblCurrentComplaintsData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;

            float[] fPainAssessment1 = { 4f };
            PdfPTable tblPainAssessment1 = new PdfPTable(fPainAssessment1);
            tblPainAssessment1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment1.DefaultCell.FixedHeight = 14f;
            tblPainAssessment1.WidthPercentage = 100;

            tblPainAssessment1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            tblPainAssessment1.AddCell(new Phrase("Pain Assessment 1", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fPainAssessment1Data = { 4f, 4f, 4f, 4f };
            PdfPTable tblPainAssessment1Data = new PdfPTable(fPainAssessment1Data);
            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment1Data.DefaultCell.FixedHeight = 14f;
            tblPainAssessment1Data.WidthPercentage = 100;

            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblPainAssessment1Data.AddCell(new Phrase("Pain Description:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblPainAssessment1Data.AddCell(new Phrase(oPmNotesDao.PainDescription, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.AddCell(new Phrase("Present Pain Level:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblPainAssessment1Data.AddCell(new Phrase(oPmNotesDao.PainLevel, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment1Data.AddCell(new Phrase("Location:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment1Data.AddCell(new Phrase(oPmNotesDao.Location, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.AddCell(new Phrase("Quality:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblPainAssessment1Data.AddCell(new Phrase(oPmNotesDao.Quality, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment1Data.AddCell(new Phrase("Severity:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment1Data.AddCell(new Phrase(oPmNotesDao.Severity, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.AddCell(new Phrase("Modifying factors:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblPainAssessment1Data.AddCell(new Phrase(oPmNotesDao.ModifyingFactors, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblPainAssessment1Data.AddCell(new Phrase("Associated Symptoms:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblPainAssessment1Data.AddCell(new Phrase(oPmNotesDao.AssociatedSymptoms, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblPainAssessment1Data.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPainAssessment1Data.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;

            float[] fPainAssessment2 = { 4f };
            PdfPTable tblPainAssessment2 = new PdfPTable(fPainAssessment2);
            tblPainAssessment2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment2.DefaultCell.FixedHeight = 14f;
            tblPainAssessment2.WidthPercentage = 100;

            tblPainAssessment2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            tblPainAssessment2.AddCell(new Phrase("Pain Assessment 2", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fPainAssessment2Data = { 4f, 4f, 4f, 4f };
            PdfPTable tblPainAssessment2Data = new PdfPTable(fPainAssessment2Data);
            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment2Data.DefaultCell.FixedHeight = 14f;
            tblPainAssessment2Data.WidthPercentage = 100;

            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblPainAssessment2Data.AddCell(new Phrase("Pain Description:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblPainAssessment2Data.AddCell(new Phrase(oPmNotesDao.PainDescriptionTwo, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.AddCell(new Phrase("Present Pain Level:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblPainAssessment2Data.AddCell(new Phrase(oPmNotesDao.PainLevelTwo, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment2Data.AddCell(new Phrase("Location:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment2Data.AddCell(new Phrase(oPmNotesDao.LocationTwo, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.AddCell(new Phrase("Quality:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblPainAssessment2Data.AddCell(new Phrase(oPmNotesDao.QualityTwo, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment2Data.AddCell(new Phrase("Severity:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPainAssessment2Data.AddCell(new Phrase(oPmNotesDao.SeverityTwo, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.AddCell(new Phrase("Modifying factors:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            tblPainAssessment2Data.AddCell(new Phrase(oPmNotesDao.ModifyingFactorsTwo, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblPainAssessment2Data.AddCell(new Phrase("Associated Symptoms:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblPainAssessment2Data.AddCell(new Phrase(oPmNotesDao.AssociatedSymptomsTwo, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblPainAssessment2Data.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPainAssessment2Data.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;


            float[] fExam = { 4f };
            PdfPTable tblExam = new PdfPTable(fExam);
            tblExam.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblExam.DefaultCell.FixedHeight = 14f;
            tblExam.WidthPercentage = 100;

            tblExam.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblExam.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            tblExam.AddCell(new Phrase("Exam", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fExamData = { 4f, 4f, 4f, 4f };
            PdfPTable tblExamData = new PdfPTable(fExamData);
            tblExamData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblExamData.DefaultCell.FixedHeight = 14f;
            tblExamData.WidthPercentage = 100;

            tblExamData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblExamData.AddCell(new Phrase("Modifying Factors:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblExamData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblExamData.AddCell(new Phrase(oPmNotesDao.ModifyingFactorsExam, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblExamData.AddCell(new Phrase("Associated Symptoms:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblExamData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblExamData.AddCell(new Phrase(oPmNotesDao.AssociatedSymptomsExam, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));

            tblExamData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblExamData.AddCell(new Phrase("Neck:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblExamData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblExamData.AddCell(new Phrase(oPmNotesDao.Neck, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblExamData.AddCell(new Phrase("Back:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblExamData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            tblExamData.AddCell(new Phrase(oPmNotesDao.Back, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblExamData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;

            float[] fAssessment = { 4f };
            PdfPTable tblAssessment = new PdfPTable(fAssessment);
            tblAssessment.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblAssessment.DefaultCell.FixedHeight = 14f;
            tblAssessment.WidthPercentage = 100;

            tblAssessment.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblAssessment.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            tblAssessment.AddCell(new Phrase("Assessment", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fAssessmentData = { 4f, 4f, 4f, 4f };
            PdfPTable tblAssessmentData = new PdfPTable(fAssessmentData);
            tblAssessmentData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblAssessmentData.DefaultCell.FixedHeight = 14f;
            tblAssessmentData.WidthPercentage = 100;

            tblAssessmentData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblAssessmentData.AddCell(new Phrase("Assessment:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblAssessmentData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblAssessmentData.AddCell(new Phrase(oPmNotesDao.Assesment, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblAssessmentData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblAssessmentData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblAssessmentData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblAssessmentData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;


            float[] fDiagnosis = { 4f };
            PdfPTable tblDiagnosis = new PdfPTable(fDiagnosis);
            tblDiagnosis.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblDiagnosis.DefaultCell.FixedHeight = 14f;
            tblDiagnosis.WidthPercentage = 100;

            tblDiagnosis.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblDiagnosis.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            tblDiagnosis.AddCell(new Phrase("Diagnosis", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fDiagnosisData = { 4f, 4f, 4f, 4f };
            PdfPTable tblDiagnosisData = new PdfPTable(fDiagnosisData);
            tblDiagnosisData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblDiagnosisData.DefaultCell.FixedHeight = 14f;
            tblDiagnosisData.WidthPercentage = 100;

            tblDiagnosisData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblDiagnosisData.AddCell(new Phrase("Diagnosis Code:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblDiagnosisData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblDiagnosisData.AddCell(new Phrase(oPmNotesDao.DiagnosisCode, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblDiagnosisData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblDiagnosisData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblDiagnosisData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblDiagnosisData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;


            #region Procedure Code

            //float currY_PC = 612f;

            PdfPTable procCodeTable = new PdfPTable(4);
            procCodeTable.HorizontalAlignment = Element.ALIGN_LEFT;
            procCodeTable.TotalWidth = 555f;

            float[] widths18 = new float[] { 13f, 247f, 13f, 247f };
            procCodeTable.SetWidths(widths18);
            procCodeTable.LockedWidth = true;

            PdfPCell hdprocCodeCell = new PdfPCell(new Phrase("Procedure Code", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            hdprocCodeCell.FixedHeight = 13f;
            hdprocCodeCell.HorizontalAlignment = 0;
            hdprocCodeCell.Colspan = 6;
            hdprocCodeCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;

            procCodeTable.AddCell(hdprocCodeCell);

            ArrayList arAllProcCode = new ArrayList();
            ArrayList arSelectedProcCode = new ArrayList();
            ArrayList arCmp2 = new ArrayList();

            arAllProcCode = pmProcCodedata.ar;
            arSelectedProcCode = pmProcCodelist.arpc;

            PdfPCell imageCell1 = new PdfPCell(jpg1);
            imageCell1.FixedHeight = 12f;
            imageCell1.HorizontalAlignment = 1;
            imageCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            imageCell1.Border = 0;

            PdfPCell imageCell2 = new PdfPCell(jpg2);
            imageCell2.FixedHeight = 12f;
            imageCell2.HorizontalAlignment = 1;
            imageCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            imageCell2.Border = 0;

            if (arAllProcCode != null)
            {
                if (arSelectedProcCode != null)
                {
                    for (int i = 0; i < arSelectedProcCode.Count; i++)
                    {
                        pmProcCodelist = (PMNotes_ProcCode_DAO)arSelectedProcCode[i];
                        arCmp2.Add(pmProcCodelist.SZ_PROC_CODE);
                    }
                }

                for (int i = 0; i < arAllProcCode.Count; i++)
                {
                    pmProcCodedata = (PMNotes_ProcCode_DAO)arAllProcCode[i];

                    PdfPCell procCodeCell = new PdfPCell(new Phrase(pmProcCodedata.code, arialsmall));
                    procCodeCell.FixedHeight = 12f;
                    procCodeCell.HorizontalAlignment = 0;
                    procCodeCell.Border = 0;

                    if (arCmp2 != null)
                    {
                        if (arCmp2.Contains(pmProcCodedata.SZ_TYPE_CODE_ID))
                        {
                            procCodeTable.AddCell(imageCell1);
                            procCodeTable.AddCell(procCodeCell);
                        }
                        else
                        {
                            //procCodeTable.AddCell(imageCell2);
                            //procCodeTable.AddCell(procCodeCell);
                        }
                    }
                    else
                    {
                        //procCodeTable.AddCell(imageCell2);
                        //procCodeTable.AddCell(procCodeCell);
                    }
                }

                int rem2 = arAllProcCode.Count % 2;
                if (rem2 != 0)
                {
                    if (rem2 == 1)
                    {
                        PdfPCell AdjCell3 = new PdfPCell(new Phrase("", arialsmall));
                        AdjCell3.FixedHeight = 12f;
                        AdjCell3.HorizontalAlignment = 0;
                        AdjCell3.Colspan = 2;
                        AdjCell3.Border = 0;
                        procCodeTable.AddCell(AdjCell3);
                    }
                }
            }


            #endregion


            float[] fPlain = { 4f };
            PdfPTable tblPlain = new PdfPTable(fPlain);
            tblPlain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPlain.DefaultCell.FixedHeight = 14f;
            tblPlain.WidthPercentage = 100;

            tblPlain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPlain.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            tblPlain.AddCell(new Phrase("Plain", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fPlainData = { 4f, 4f, 4f, 4f };
            PdfPTable tblPlainData = new PdfPTable(fPlainData);
            tblPlainData.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblPlainData.DefaultCell.FixedHeight = 14f;
            tblPlainData.WidthPercentage = 100;

            tblPlainData.DefaultCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblPlainData.AddCell(new Phrase("Plain:", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblPlainData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblPlainData.AddCell(new Phrase(oPmNotesDao.Plain, iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPlainData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPlainData.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            tblPlainData.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FontStyle, MidHeadtSize, iTextSharp.text.Color.BLACK)));
            tblPlainData.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;


            float[] fRom = { 4f };
            PdfPTable tblRom = new PdfPTable(fRom);
            tblRom.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblRom.DefaultCell.FixedHeight = 14f;
            tblRom.WidthPercentage = 100;

            tblRom.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblRom.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            tblRom.AddCell(new Phrase("ROM", iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            PdfPTable tblContent = new PdfPTable(6);

            PdfPCell cellCervical = new PdfPCell(new Phrase("Range of Motion: Cervical"));
            cellCervical.Colspan = 3;
            cellCervical.HorizontalAlignment = 1;
            PdfPCell cellLumbar = new PdfPCell(new Phrase("Range of Motion: Lumbar"));
            cellLumbar.Colspan = 3;
            cellLumbar.HorizontalAlignment = 1;

            tblContent.AddCell(cellCervical);
            tblContent.AddCell(cellLumbar);

            tblContent.AddCell("");
            tblContent.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            tblContent.AddCell(new Phrase("Normal", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblContent.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            tblContent.AddCell(new Phrase("Observed", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell("");
            tblContent.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            tblContent.AddCell(new Phrase("Normal", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblContent.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            tblContent.AddCell(new Phrase("Observed", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));


            tblContent.AddCell(new Phrase("Flexion", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase(oPmNotesDao.FlexionNormalOne, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase(oPmNotesDao.FlexionObservedOne, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase("Flexion", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase(oPmNotesDao.FlexionNormalTwo, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase(oPmNotesDao.FlexionObservedTwo, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));

            tblContent.AddCell(new Phrase("Extension", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase(oPmNotesDao.ExtensionNormalOne, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase(oPmNotesDao.ExtensionObservedOne, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase("Extension", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase(oPmNotesDao.ExtensionNormalTwo, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase(oPmNotesDao.ExtensionObservedTwo, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));

            tblContent.AddCell(new Phrase("Rotaion", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase(oPmNotesDao.RotationNormalOne, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase(oPmNotesDao.RotationObservedOne, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase("Rotaion", iTextSharp.text.FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase(oPmNotesDao.RotationNormalTwo, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));
            tblContent.AddCell(new Phrase(oPmNotesDao.RotationObservedTwo, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK)));

            PdfPTable tblSign = new PdfPTable(4);

            tblSign.HorizontalAlignment = Element.ALIGN_LEFT;
            tblSign.WidthPercentage = 100;
            tblSign.TotalWidth = 555;

            float[] widths7 = new float[] { 80f, 80f, 80f, 80f };
            tblSign.SetWidths(widths7);
            tblSign.LockedWidth = true;

            if (oPmNotesDao.bt_pat_sign_success == "1") { patientSignPath = oPmNotesDao.SZ_PATIENT_SIGN_PATH; }
            if (oPmNotesDao.bt_doc_sign_success == "1") { drSignPath = oPmNotesDao.SZ_DOCTOR_SIGN_PATH; }

            PdfPCell hddoctorSignCell = new PdfPCell(new Phrase("DOCTOR SIGNATURE:", iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            hddoctorSignCell.FixedHeight = 60f;
            hddoctorSignCell.HorizontalAlignment = 2;
            hddoctorSignCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            hddoctorSignCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            PdfPCell doctorSignCell;
            if (oPmNotesDao.bt_doc_sign_success == "1")
            {
                iTextSharp.text.Image doctorSign = iTextSharp.text.Image.GetInstance(drSignPath);
                doctorSign.ScaleAbsolute(110f, 55f);

                doctorSignCell = new PdfPCell(doctorSign);
                doctorSignCell.FixedHeight = 60f;
                doctorSignCell.HorizontalAlignment = 1;
                doctorSignCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                doctorSignCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            }
            else
            {
                doctorSignCell = new PdfPCell(new Phrase(""));
                doctorSignCell.FixedHeight = 60f;
                doctorSignCell.HorizontalAlignment = 1;
                doctorSignCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                doctorSignCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
            }

            PdfPCell hdpatientSignCell = new PdfPCell(new Phrase("PATIENT SIGNATURE:", iTextSharp.text.FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            hdpatientSignCell.FixedHeight = 60f;
            hdpatientSignCell.HorizontalAlignment = 2;
            hdpatientSignCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            hdpatientSignCell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            PdfPCell patientSignCell;
            if (oPmNotesDao.bt_pat_sign_success == "1")
            {
                iTextSharp.text.Image patientSign = iTextSharp.text.Image.GetInstance(patientSignPath);
                patientSign.ScaleAbsolute(110f, 55f);

                patientSignCell = new PdfPCell(patientSign);
                patientSignCell.FixedHeight = 60f;
                patientSignCell.HorizontalAlignment = 1;
                patientSignCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                patientSignCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            }
            else
            {
                patientSignCell = new PdfPCell();
                patientSignCell.FixedHeight = 60f;
                patientSignCell.HorizontalAlignment = 1;
                patientSignCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                patientSignCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
            }

            tblSign.AddCell(hdpatientSignCell);
            tblSign.AddCell(patientSignCell);
            tblSign.AddCell(hddoctorSignCell);
            tblSign.AddCell(doctorSignCell);

            tblBase.AddCell(tblHeader);
            tblBase.AddCell(tblCurrentAllergies);
            tblBase.AddCell(tblCurrentAllergiesData);
            tblBase.AddCell(tblCurrentComplaints);
            tblBase.AddCell(tblCurrentComplaintsData);
            tblBase.AddCell(tblPainAssessment1);
            tblBase.AddCell(tblPainAssessment1Data);
            tblBase.AddCell(tblPainAssessment2);
            tblBase.AddCell(tblPainAssessment2Data);
            tblBase.AddCell(tblExam);
            tblBase.AddCell(tblExamData);
            tblBase.AddCell(tblAssessment);
            tblBase.AddCell(tblAssessmentData);
            tblBase.AddCell(tblDiagnosis);
            tblBase.AddCell(tblDiagnosisData);
            tblBase.AddCell(procCodeTable);
            tblBase.AddCell(tblPlain);
            tblBase.AddCell(tblPlainData);
            tblBase.AddCell(tblRom);
            tblBase.AddCell(tblContent);
            tblBase.AddCell(tblSign);
            document.Add(tblBase);
        }

        public DataSet GetPMNotesDetails(string CaseId, string companyId)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                sqlConnection = new SqlConnection(strsqlCon);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCommand.CommandText = "sp_get_pmnotes_details_from_caseid";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Parameters.AddWithValue("@i_case_id", CaseId);
                sqlCommand.Parameters.AddWithValue("@sz_comnpany_id", companyId);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(ds);


            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally { sqlConnection.Close(); }
            return ds;
        }

        public string GetDoctorID(string i_event_id)
        {
            String strsqlCon;
            SqlConnection conn;
            SqlDataReader dr;
            SqlCommand comm;

            string DoctId = "";
            strsqlCon = ConfigurationSettings.AppSettings["Connection_String"].ToString();

            try
            {
                conn = new SqlConnection(strsqlCon);
                conn.Open();
                comm = new SqlCommand("sp_get_doctor_id", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@i_event_id", i_event_id);
                dr = comm.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DoctId = dr[0].ToString();
                    }
                }
                conn.Close();
                return DoctId;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return DoctId;
            }

            return DoctId;
        }

        public DataSet GetEventIDs(string bill_no)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
            DataSet ds = new DataSet();

            try
            {
                con.Open();
                SqlCommand sqlCmd = new SqlCommand("SP_GET_NOTE_EVENT_ID", con);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", bill_no);

                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                sqlda.Fill(ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return ds;
        }

        public DataSet GetEventDetails(string i_event_id)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
            DataSet ds = new DataSet();

            try
            {
                con.Open();
                SqlCommand sqlCmd = new SqlCommand("SP_GET_MST_PM_NOTES_DATA", con);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", i_event_id);

                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                sqlda.Fill(ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return ds;
        }

        public DataSet GetProcedureCodeList(string I_EVENT_ID)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
            DataSet ds = new DataSet();

            try
            {
                con.Open();
                SqlCommand sqlCmd = new SqlCommand("sp_get_Proccode_id", con);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", I_EVENT_ID);
                sqlCmd.Parameters.AddWithValue("@flag", "getProcCode");
                sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", "");

                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                sqlda.Fill(ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return ds;
        }

        public DataSet GetAllProcedureCode(string SZ_DOCTOR_ID)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
            DataSet ds = new DataSet();

            try
            {
                con.Open();
                SqlCommand sqlCmd = new SqlCommand("sp_find_procedure_code_from_doctor", con);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@sz_doctor_id", SZ_DOCTOR_ID);
                sqlCmd.Parameters.AddWithValue("@sz_order_by", "");
                sqlCmd.Parameters.AddWithValue("@i_start_index", "1");
                sqlCmd.Parameters.AddWithValue("@i_end_index", "10");
                sqlCmd.Parameters.AddWithValue("@sz_search_text", "");

                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                sqlda.Fill(ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return ds;
        }
    }
}