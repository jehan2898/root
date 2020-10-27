using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using WCPDFData;
using ProcedureCode;
using Bill_Sys_Psy_DAO;
using System.Web;
using Microsoft.SqlServer.Management.Common;

namespace PsychologistReport
{
    public class PsychologyBillPDF
    {

        public string PsyPDF(string bill_no, string caseId, string companyId, string CompanyName, string userId, string userName, string case_no, string speciality,string filepath)
        {
            string szFilename = bill_no + "_" + caseId + "_" + DateTime.Now.ToString("MM_dd_yyyyhhmm") + ".pdf"; ;
            GeneratePsylogistReport(filepath, bill_no, companyId, szFilename);
            return szFilename;
        }
        public string PsyPDF(string bill_no, string caseId, string companyId, string CompanyName, string userId, string userName, string case_no, string speciality, string filepath,ServerConnection conn)
        {
            string szFilename = bill_no + "_" + caseId + "_" + DateTime.Now.ToString("MM_dd_yyyyhhmm") + ".pdf"; ;
            GeneratePsylogistReport(filepath, bill_no, companyId, szFilename,conn);
            return szFilename;
        }
        public void GeneratePsylogistReport(string sBasePDFPath, string bill_no, string Company_ID,string szFilename)
        {

            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            ds = GetPDFData(bill_no);
            ds1 = getDiagnosysData(bill_no);
           
            PsychologistBill pb = new PsychologistBill();
            WCPDFData.WCPDFData wd = new WCPDFData.WCPDFData();

            pb = wd.GetWCPdfData(ds, ds1);

            ArrayList ARR = new ArrayList();
            ARR = pb.ar;

            const int FONT_SIZE = 7;
            const int LAST_PAGE_HEADER = 10;
            const int HEADER_FONT_SIZE = 9;
            const int SUBHEDER_FONT_SIZE = 6;
            const string FONT_NAME = "Arial";



            string sFilePath = System.Configuration.ConfigurationSettings.AppSettings["BASEPATH"].ToString() + sBasePDFPath + szFilename;// +bill_no + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
            string sCheckPath = System.Configuration.ConfigurationSettings.AppSettings["CHECKBOXPATH"].ToString();
            string sUnCheckPath = System.Configuration.ConfigurationSettings.AppSettings["UNCHECKBOXPATH"].ToString();


            if (!Directory.Exists(System.Configuration.ConfigurationSettings.AppSettings["BASEPATH"].ToString() + sBasePDFPath))
            {
                Directory.CreateDirectory(System.Configuration.ConfigurationSettings.AppSettings["BASEPATH"].ToString() + sBasePDFPath);
            }


            MemoryStream m = new MemoryStream();
            FileStream fs = new FileStream(sFilePath, System.IO.FileMode.OpenOrCreate);
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 20, 20, 10, 10);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            float[] wMain = { 6f };
            PdfPTable tblMain = new PdfPTable(wMain);
            tblMain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblMain.WidthPercentage = 100;
            while (ARR.Count != 0)
            {
                tblMain = GenerateFirst(pb, ARR, sCheckPath, sUnCheckPath, FONT_SIZE, HEADER_FONT_SIZE, SUBHEDER_FONT_SIZE, FONT_NAME, LAST_PAGE_HEADER);
                document.NewPage();
                document.Add(tblMain);
            }
            document.NewPage();
            tblMain = GenerateSecond(FONT_SIZE, HEADER_FONT_SIZE, SUBHEDER_FONT_SIZE, FONT_NAME, LAST_PAGE_HEADER);
            document.Add(tblMain);
            document.Close();


        }
        public void GeneratePsylogistReport(string sBasePDFPath, string bill_no, string Company_ID, string szFilename,ServerConnection conn)
        {

            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            ds = GetPDFData(bill_no,conn);
            ds1 = getDiagnosysData(bill_no);

            PsychologistBill pb = new PsychologistBill();
            WCPDFData.WCPDFData wd = new WCPDFData.WCPDFData();

            pb = wd.GetWCPdfData(ds, ds1);

            ArrayList ARR = new ArrayList();
            ARR = pb.ar;

            const int FONT_SIZE = 7;
            const int LAST_PAGE_HEADER = 10;
            const int HEADER_FONT_SIZE = 9;
            const int SUBHEDER_FONT_SIZE = 6;
            const string FONT_NAME = "Arial";



            string sFilePath = System.Configuration.ConfigurationSettings.AppSettings["BASEPATH"].ToString() + sBasePDFPath + szFilename;// +bill_no + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
            string sCheckPath = System.Configuration.ConfigurationSettings.AppSettings["CHECKBOXPATH"].ToString();
            string sUnCheckPath = System.Configuration.ConfigurationSettings.AppSettings["UNCHECKBOXPATH"].ToString();


            if (!Directory.Exists(System.Configuration.ConfigurationSettings.AppSettings["BASEPATH"].ToString() + sBasePDFPath))
            {
                Directory.CreateDirectory(System.Configuration.ConfigurationSettings.AppSettings["BASEPATH"].ToString() + sBasePDFPath);
            }


            MemoryStream m = new MemoryStream();
            FileStream fs = new FileStream(sFilePath, System.IO.FileMode.OpenOrCreate);
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 20, 20, 10, 10);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            float[] wMain = { 6f };
            PdfPTable tblMain = new PdfPTable(wMain);
            tblMain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblMain.WidthPercentage = 100;
            while (ARR.Count != 0)
            {
                tblMain = GenerateFirst(pb, ARR, sCheckPath, sUnCheckPath, FONT_SIZE, HEADER_FONT_SIZE, SUBHEDER_FONT_SIZE, FONT_NAME, LAST_PAGE_HEADER);
                document.NewPage();
                document.Add(tblMain);
            }
            document.NewPage();
            tblMain = GenerateSecond(FONT_SIZE, HEADER_FONT_SIZE, SUBHEDER_FONT_SIZE, FONT_NAME, LAST_PAGE_HEADER);
            document.Add(tblMain);
            document.Close();


        }

        public DataSet GetPDFData(string bill_no)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
            DataSet ds = new DataSet();
            
            try
            {
                con.Open();
                SqlCommand sqlCmd;
                try
                {
                    if (System.Web.HttpContext.Current.Session["GenerateBill_insType"].ToString() == "Secondary")
                    {
                        sqlCmd = new SqlCommand("SP_PSY_PDF_DATA_SEC", con);
                        sqlCmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    }
                    else
                    {
                        sqlCmd = new SqlCommand("SP_PSY_PDF_DATA", con);
                        sqlCmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    }
                }
                catch
                { 
                    sqlCmd = new SqlCommand("SP_PSY_PDF_DATA", con);
                    sqlCmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

                }
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", bill_no);
                //sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", Company_ID);

                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                sqlda.Fill(ds);

            }
            catch (Exception ex)
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
        public DataSet GetPDFData(string bill_no,ServerConnection conn)
        {


            DataSet ds = new DataSet();

            try
            {
                string Query = "";
                
                
               
                SqlCommand sqlCmd;
                try
                {
                    if (System.Web.HttpContext.Current.Session["GenerateBill_insType"].ToString() == "Secondary")
                    {
                        Query = Query + "Exec SP_PSY_PDF_DATA_SEC ";
                        
                    }
                    else
                    {
                        Query = Query + "Exec SP_PSY_PDF_DATA ";
                       
                    }
                }
                catch
                {
                    Query = Query + "Exec SP_PSY_PDF_DATA ";
                    

                }
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", bill_no, ",");
               
                Query = Query.TrimEnd(',');
                ds= conn.ExecuteWithResults(Query);

            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
            }
            finally
            {
               
            }
            return ds;
        }
        public DataSet getDiagnosysData(string bill_no)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
            DataSet ds = new DataSet();

            try
            {
                con.Open();
                SqlCommand sqlCmd = new SqlCommand("GET_DIAGNOSYS_CODES", con);
                sqlCmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", bill_no);
                sqlCmd.Parameters.AddWithValue("@FLAG", "SERVICE_TABLE");

                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                sqlda.Fill(ds);
            }
            catch (Exception ex)
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
        public DataSet getDiagnosysData(string bill_no,ServerConnection conn)
        {
           
            DataSet ds = new DataSet();

            try
            {
                string Query = "";
                Query = Query + "Exec GET_DIAGNOSYS_CODES ";
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", bill_no, ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "SERVICE_TABLE", ",");
                Query = Query.TrimEnd(',');


                ds = conn.ExecuteWithResults(Query);
               
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);

            }
            finally
            {
                
            }
            return ds;
        }
        public PdfPTable GenerateFirst(PsychologistBill pb, ArrayList ARR, string sCheckPath, string sUnCheckPath, int FONT_SIZE, int HEADER_FONT_SIZE, int SUBHEDER_FONT_SIZE, string FONT_NAME, int LAST_PAGE_HEADER)
        {

            float[] wMain = { 6f };
            PdfPTable tblMain = new PdfPTable(wMain);
            tblMain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblMain.WidthPercentage = 100;

            float[] wBase = { 4f };
            PdfPTable tblBase = new PdfPTable(wBase);
            tblBase.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
            tblBase.WidthPercentage = 100;


            float[] fTop = { 1.8f, 1.7f, 1.5f, 0.5f };
            PdfPTable Ttop = new PdfPTable(fTop);
            Ttop.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            Ttop.WidthPercentage = 100;

            Ttop.AddCell(new Phrase("ATTENDING PSYCHOLOGIST'S REPORT", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            Ttop.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            Ttop.AddCell(new Phrase("STATE OF NEW YORK WORKERS' COMPENSATION BOARD", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            Ttop.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            Ttop.AddCell(new Phrase("SERVICES PROVIDED UNDER WCB PREFERRED PROVIDER ORGANIZATION (PPO) PROGRAM?", iTextSharp.text.FontFactory.GetFont(FONT_NAME, SUBHEDER_FONT_SIZE, iTextSharp.text.Color.BLACK)));


            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(sCheckPath);
            ImgObj.ScaleAbsolute(10f, 10f);
            PdfPCell cellObj = new PdfPCell(ImgObj);
            cellObj.Border = Rectangle.NO_BORDER;
            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
            cellObj.FixedHeight = 10f;



            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(sUnCheckPath);
            ImgObj1.ScaleAbsolute(10f, 10f);
            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
            cellObj1.Border = Rectangle.NO_BORDER;
            cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellObj1.FixedHeight = 10f;


            float[] fYesNo = { 0.8f, 0.3f, 0.6f, 0.3f };
            PdfPTable TYesNo = new PdfPTable(fYesNo);
            TYesNo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TYesNo.WidthPercentage = 100;

            if (pb.PPOYes == "1")
            {
                TYesNo.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TYesNo.AddCell(cellObj);
                TYesNo.DefaultCell.Border = Rectangle.NO_BORDER;

                TYesNo.AddCell(new Phrase("No", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TYesNo.AddCell(cellObj1);
                TYesNo.DefaultCell.Border = Rectangle.NO_BORDER;

                Ttop.AddCell(TYesNo);
                tblMain.AddCell(Ttop);
            }
            else if (pb.PPOYes == "2")
            {
                TYesNo.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TYesNo.AddCell(cellObj1);
                TYesNo.DefaultCell.Border = Rectangle.NO_BORDER;

                TYesNo.AddCell(new Phrase("No", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TYesNo.AddCell(cellObj);
                TYesNo.DefaultCell.Border = Rectangle.NO_BORDER;

                Ttop.AddCell(TYesNo);
                tblMain.AddCell(Ttop);
            }
            else
            {
                TYesNo.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TYesNo.AddCell(cellObj1);
                TYesNo.DefaultCell.Border = Rectangle.NO_BORDER;

                TYesNo.AddCell(new Phrase("No", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TYesNo.AddCell(cellObj1);
                TYesNo.DefaultCell.Border = Rectangle.NO_BORDER;

                Ttop.AddCell(TYesNo);
                tblMain.AddCell(Ttop);
            }

            float[] ftimeprog = { 0.4f, 1f, 0.4f, 1f, 0.4f, 4f, 7.9f };
            PdfPTable Ttimeprog = new PdfPTable(ftimeprog);
            Ttimeprog.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            Ttimeprog.WidthPercentage = 100;

            if (pb.AttendingTime == "1")
                Ttimeprog.AddCell(cellObj);
            else
                Ttimeprog.AddCell(cellObj1);

            //Ttimeprog.AddCell(cellObj1);
            Ttimeprog.DefaultCell.Border = Rectangle.NO_BORDER;

            Ttimeprog.DefaultCell.Border = Rectangle.BOX;
            Ttimeprog.AddCell(new Phrase("48 HR. INITIAL", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            if (pb.AttendingTime == "2")
                Ttimeprog.AddCell(cellObj);
            else
                Ttimeprog.AddCell(cellObj1);

            //Ttimeprog.AddCell(cellObj1);
            Ttimeprog.DefaultCell.Border = Rectangle.NO_BORDER;

            Ttimeprog.DefaultCell.Border = Rectangle.BOX;
            Ttimeprog.AddCell(new Phrase("15 DAY INITIAL", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            if (pb.AttendingTime == "3")
                Ttimeprog.AddCell(cellObj);
            else
                Ttimeprog.AddCell(cellObj1);


            //Ttimeprog.AddCell(cellObj1);
            Ttimeprog.DefaultCell.Border = Rectangle.NO_BORDER;

            Ttimeprog.DefaultCell.Border = Rectangle.BOX;
            float[] fDayProg = { 1.1f, 2.5f };
            PdfPTable TDayProg = new PdfPTable(fDayProg);
            TDayProg.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TDayProg.WidthPercentage = 100;

            //TDayProg.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            TDayProg.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            TDayProg.AddCell(new Phrase("90 DAY PROGRESS", iTextSharp.text.FontFactory.GetFont(FONT_NAME, SUBHEDER_FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            TDayProg.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            TDayProg.AddCell(new Phrase("SEE ITEM 1 ON REVERSE FOR FILING INSTRUCTIONS", iTextSharp.text.FontFactory.GetFont(FONT_NAME, SUBHEDER_FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            Ttimeprog.AddCell(TDayProg);

            Ttimeprog.DefaultCell.Border = Rectangle.NO_BORDER;
            Ttimeprog.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            Ttimeprog.AddCell(new Phrase("PLEASE TYPE ALL INFORMATION - COMPLETE ALL ITEMS", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            tblMain.AddCell(Ttimeprog);

            float[] fHeadCell = { 2f, 3f, 3f, 2f, 6f, 4f };
            PdfPTable THeadCell = new PdfPTable(fHeadCell);

            THeadCell.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            THeadCell.WidthPercentage = 100;

            THeadCell.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            THeadCell.AddCell(new Phrase("WCB CASE NO.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            THeadCell.AddCell(new Phrase("CARRIER CASE NO. (IF KNOWN)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            THeadCell.AddCell(new Phrase("DATE OF INJURY", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            THeadCell.AddCell(new Phrase("& TIME", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            THeadCell.AddCell(new Phrase("ADDRESS WHERE INJURY OCCURRED (CITY, TOWN OR VILLAGE)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            THeadCell.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            THeadCell.AddCell(new Phrase("INJURED PERSON'S SOCIAL SECURITY NUMBER", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            THeadCell.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            THeadCell.AddCell(new Phrase(pb.WCBCaseNo, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            THeadCell.AddCell(new Phrase(pb.CarrierCaseNo, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            THeadCell.AddCell(new Phrase(pb.DateOfInjury, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            THeadCell.AddCell(new Phrase(pb.Time, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            THeadCell.AddCell(new Phrase(pb.AddressWhereInjuryOccured, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            THeadCell.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            THeadCell.AddCell(new Phrase(pb.InjuredPersonSSN, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            tblBase.AddCell(THeadCell);

            float[] fPersonalInfo = { 1f, 3.8f, 3.8f, 2f };
            PdfPTable TPersonalInfo = new PdfPTable(fPersonalInfo);
            TPersonalInfo.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            TPersonalInfo.WidthPercentage = 100;

            TPersonalInfo.AddCell(new Phrase("INJURED PERSON", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            float[] fName = { 1f, 1f, 1f };
            PdfPTable TName = new PdfPTable(fName);
            TName.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TName.WidthPercentage = 100;

            TName.AddCell(new Phrase("(First Name)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TName.AddCell(new Phrase("(Middle Initial)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TName.AddCell(new Phrase("(Last Name)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TName.AddCell(new Phrase(pb.InjuredPersonFirstName, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TName.AddCell(new Phrase(pb.InjuredPersonMI, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TName.AddCell(new Phrase(pb.InjuredPersonLastName, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPersonalInfo.AddCell(TName);

            float[] fAdd = { 4f };
            PdfPTable TAdd = new PdfPTable(fAdd);
            TAdd.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TAdd.WidthPercentage = 100;

            TAdd.AddCell(new Phrase("ADDRESS (Include Apt. No.)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TAdd.AddCell(new Phrase(pb.InjuredPersonAddress, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));


            TPersonalInfo.AddCell(TAdd);

            float[] fTele = { 4f };
            PdfPTable TTele = new PdfPTable(fTele);
            TTele.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TTele.WidthPercentage = 100;

            TPersonalInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            TTele.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            TTele.AddCell(new Phrase("TELEPHONE NO.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TTele.AddCell(new Phrase(pb.InjuredPersonTelephoneNo, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPersonalInfo.AddCell(TTele);

            TPersonalInfo.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            TPersonalInfo.AddCell(new Phrase("EMPLOYER*", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPersonalInfo.AddCell(new Phrase(pb.EmployerName, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPersonalInfo.AddCell(new Phrase(pb.EmployerAddress, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            float[] fDOB = { 4f };
            PdfPTable TDOB = new PdfPTable(fDOB);
            TDOB.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TDOB.WidthPercentage = 100;

            TPersonalInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            TDOB.AddCell(new Phrase("PATIENT'S DATE OF BIRTH", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TDOB.AddCell(new Phrase(pb.PatientDateOfBirth, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPersonalInfo.AddCell(TDOB);
            TPersonalInfo.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            TPersonalInfo.AddCell(new Phrase("INSURANCE CARRIER", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPersonalInfo.AddCell(new Phrase(pb.InsuranceCarrierName, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPersonalInfo.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;

            TPersonalInfo.AddCell(new Phrase(pb.InsuranceCarrierAddress, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPersonalInfo.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPersonalInfo.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            TPersonalInfo.AddCell(new Phrase("REFERRING PHYSICIAN", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPersonalInfo.AddCell(new Phrase(pb.ReferringPhysicianName, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPersonalInfo.AddCell(new Phrase(pb.ReferringPhysicianAddress, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            float[] fPhyTele = { 4f };
            PdfPTable TPhyTele = new PdfPTable(fPhyTele);
            TPhyTele.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPhyTele.WidthPercentage = 100;

            TPersonalInfo.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPhyTele.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            TPhyTele.AddCell(new Phrase("TELEPHONE NO.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPhyTele.AddCell(new Phrase(pb.ReferringPhysicianTelephoneNo, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPersonalInfo.AddCell(TPhyTele);

            tblBase.AddCell(TPersonalInfo);

            float[] fVFBLchkmain = { 4f };
            PdfPTable TVFBLchkmain = new PdfPTable(fVFBLchkmain);
            TVFBLchkmain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TVFBLchkmain.WidthPercentage = 100;

            float[] fVFBLchk = { 5.5f, 0.3f, 0.8f, 0.3f, 0.8f };
            PdfPTable TVFBLchk = new PdfPTable(fVFBLchk);
            TVFBLchk.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TVFBLchk.WidthPercentage = 100;

            TVFBLchk.AddCell(new Phrase("*If treatment was under the VFBL or VAWBL show as \"Employer\" the liable political subdivision and check one:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            if (pb.TreatmentUnderCHKBox == "1")
            {
                TVFBLchk.AddCell(cellObj);
                TVFBLchk.AddCell(new Phrase("VFBL", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TVFBLchk.AddCell(cellObj1);
                TVFBLchk.AddCell(new Phrase("VAWBL", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else if (pb.TreatmentUnderCHKBox == "2")
            {
                TVFBLchk.AddCell(cellObj1);
                TVFBLchk.AddCell(new Phrase("VFBL", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TVFBLchk.AddCell(cellObj);
                TVFBLchk.AddCell(new Phrase("VAWBL", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                TVFBLchk.AddCell(cellObj1);
                TVFBLchk.AddCell(new Phrase("VFBL", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TVFBLchk.AddCell(cellObj1);
                TVFBLchk.AddCell(new Phrase("VAWBL", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            TVFBLchkmain.AddCell(TVFBLchk);
            tblBase.AddCell(TVFBLchkmain);

            tblBase.AddCell(new Phrase("    If you have filed a previous report, setting forth a history of the injury, enter its date " + pb.PreviousHistoryDate + " and complete Items 3 to 18. If not, complete ALL items.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            float[] fHistory = { 0.4f, 25f };
            PdfPTable THistory = new PdfPTable(fHistory);
            THistory.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            THistory.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            THistory.WidthPercentage = 100;

            THistory.AddCell(new Phrase("HISTORY", iTextSharp.text.FontFactory.GetFont(FONT_NAME, SUBHEDER_FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fHistoryPt = { 4f };
            PdfPTable THistoryPt = new PdfPTable(fHistoryPt);
            THistoryPt.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            THistoryPt.WidthPercentage = 100;

            THistoryPt.AddCell(new Phrase("1. Describe incident or occupational history that precipitated onset of related symptoms:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            THistoryPt.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;

            THistoryPt.AddCell(new Phrase(pb.IncidentHistory, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            //THistoryPt.AddCell(new Phrase("I was waching movie with my family in the Abhiruchi theater Cinema hall. suddenly crowd started running here and there. one of that jumped on my knee", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            THistoryPt.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            THistoryPt.AddCell(new Phrase("2. Has patient given any history of pre-existing psychological impairment? If so, describe specifically.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            THistoryPt.AddCell(new Phrase(pb.HistoryPreExistingInjury, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            THistory.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            THistory.AddCell(THistoryPt);

            THistory.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            THistory.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            THistory.AddCell(new Phrase("\n\n\nEVALUATION/TREATMENT", iTextSharp.text.FontFactory.GetFont(FONT_NAME, SUBHEDER_FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fTreatmentPt = { 4f };
            PdfPTable TTreatmentPt = new PdfPTable(fTreatmentPt);
            TTreatmentPt.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TTreatmentPt.WidthPercentage = 100;

            float[] fReferralmain = { 4f };
            PdfPTable TReferralmain = new PdfPTable(fReferralmain);
            TReferralmain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TReferralmain.WidthPercentage = 100;


            float[] fReferral = { 1.5f, 0.2f, 2.5f, 0.2f, 3f, 0.2f, 4f };
            PdfPTable TReferral = new PdfPTable(fReferral);
            TReferral.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TReferral.WidthPercentage = 100;


            if (pb.ReferralCHKBox == "1")
            {
                TReferral.AddCell(new Phrase("3. Referral was for:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TReferral.AddCell(cellObj);
                TReferral.AddCell(new Phrase("Evaluation Only (Complete item a)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TReferral.AddCell(cellObj1);
                TReferral.AddCell(new Phrase("Treatment Only (Complete item b-1,2)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TReferral.AddCell(cellObj1);
                TReferral.AddCell(new Phrase("Evaluation and Treatment (Complete items a and b-1,2)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else if (pb.ReferralCHKBox == "2")
            {
                TReferral.AddCell(new Phrase("3. Referral was for:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TReferral.AddCell(cellObj1);
                TReferral.AddCell(new Phrase("Evaluation Only (Complete item a)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TReferral.AddCell(cellObj);
                TReferral.AddCell(new Phrase("Treatment Only (Complete item b-1,2)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TReferral.AddCell(cellObj1);
                TReferral.AddCell(new Phrase("Evaluation and Treatment (Complete items a and b-1,2)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else if (pb.ReferralCHKBox == "3")
            {
                TReferral.AddCell(new Phrase("3. Referral was for:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TReferral.AddCell(cellObj1);
                TReferral.AddCell(new Phrase("Evaluation Only (Complete item a)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TReferral.AddCell(cellObj1);
                TReferral.AddCell(new Phrase("Treatment Only (Complete item b-1,2)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TReferral.AddCell(cellObj);
                TReferral.AddCell(new Phrase("Evaluation and Treatment (Complete items a and b-1,2)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                TReferral.AddCell(new Phrase("3. Referral was for:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TReferral.AddCell(cellObj1);
                TReferral.AddCell(new Phrase("Evaluation Only (Complete item a)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TReferral.AddCell(cellObj1);
                TReferral.AddCell(new Phrase("Treatment Only (Complete item b-1,2)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TReferral.AddCell(cellObj1);
                TReferral.AddCell(new Phrase("Evaluation and Treatment (Complete items a and b-1,2)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }



            TReferralmain.AddCell(TReferral);
            TTreatmentPt.AddCell(TReferralmain);

            float[] fReferralPt = { 4f };
            PdfPTable TReferralPt = new PdfPTable(fReferralPt);
            TReferralPt.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TReferralPt.WidthPercentage = 100;


            TReferralPt.AddCell(new Phrase("a. Your evaluation:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TReferralPt.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;

            TReferralPt.AddCell(new Phrase(pb.YourEvaluation, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TReferralPt.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TReferralPt.AddCell(new Phrase("b. (1) Patient's condition and progress:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TReferralPt.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;

            TReferralPt.AddCell(new Phrase(pb.PatientConditionAndProgress, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TTreatmentPt.AddCell(TReferralPt);

            float[] fReferralPt3b = { 8.5f, 0.2f, 2.5f };
            PdfPTable TReferralPt3b = new PdfPTable(fReferralPt3b);
            TReferralPt3b.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TReferralPt3b.WidthPercentage = 100;

            TReferralPt3b.AddCell(new Phrase("b. (2) Treatment and planned future treatment. If an authorization request is required (see items 4 & 5 on reverse), check box", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            if (pb.TreatmentB2CHKBox == "1")
                TReferralPt3b.AddCell(cellObj);
            else
                TReferralPt3b.AddCell(cellObj1);

            TReferralPt3b.AddCell(new Phrase("and explain below. If additional", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TReferralPt3b.AddCell(new Phrase("space is necessary, please attach request.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TReferralPt3b.AddCell("");
            TReferralPt3b.AddCell("");

            TTreatmentPt.AddCell(TReferralPt3b);

            float[] fReferralPt3bSpace = { 4f };
            PdfPTable TReferralPt3bSpace = new PdfPTable(fReferralPt3bSpace);
            TReferralPt3bSpace.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TReferralPt3bSpace.WidthPercentage = 100;


            TReferralPt3bSpace.AddCell(new Phrase(pb.PlannedFutureTreatment, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TTreatmentPt.AddCell(TReferralPt3bSpace);
            TTreatmentPt.AddCell("");

            float[] fTreatmentPt4 = { 2f, 2f };
            PdfPTable TTreatmentPt4 = new PdfPTable(fTreatmentPt4);
            TTreatmentPt4.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
            TTreatmentPt4.WidthPercentage = 100;

            float[] fTreatmentPt4a = { 1.5f, 1f };
            PdfPTable TTreatmentPt4a = new PdfPTable(fTreatmentPt4a);
            TTreatmentPt4a.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
            TTreatmentPt4a.WidthPercentage = 100;

            TTreatmentPt4a.AddCell(new Phrase("4. Date(s) of visits on which this report is based", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TTreatmentPt4a.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            TTreatmentPt4a.AddCell(new Phrase("Date of First Visit", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TTreatmentPt4a.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;

            TTreatmentPt4a.AddCell(new Phrase(pb.DateOfVisitOnWichReportIsBased, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TTreatmentPt4a.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            TTreatmentPt4a.AddCell(new Phrase(pb.DateOfFirstVisit, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TTreatmentPt4.AddCell(TTreatmentPt4a);

            float[] fTreatmentPt4d = { 4f };
            PdfPTable TTreatmentPt4d = new PdfPTable(fTreatmentPt4d);
            TTreatmentPt4d.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TTreatmentPt4d.WidthPercentage = 100;

            float[] fTreatmentPt4b = { 2f, 0.2f, 0.8f, 0.2f, 1.3f, 1f };
            PdfPTable TTreatmentPt4b = new PdfPTable(fTreatmentPt4b);
            TTreatmentPt4b.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TTreatmentPt4b.WidthPercentage = 100;

            TTreatmentPt4b.AddCell(new Phrase("Will patient be seen again?", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            if (pb.PatientSeenAgainCHKBox == "1")
            {
                TTreatmentPt4b.AddCell(cellObj);
                TTreatmentPt4b.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TTreatmentPt4b.AddCell(cellObj1);
                TTreatmentPt4b.AddCell(new Phrase("No   If yes, when:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else if (pb.PatientSeenAgainCHKBox == "2")
            {
                TTreatmentPt4b.AddCell(cellObj1);
                TTreatmentPt4b.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TTreatmentPt4b.AddCell(cellObj);
                TTreatmentPt4b.AddCell(new Phrase("No   If yes, when:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                TTreatmentPt4b.AddCell(cellObj1);
                TTreatmentPt4b.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TTreatmentPt4b.AddCell(cellObj1);
                TTreatmentPt4b.AddCell(new Phrase("No   If yes, when:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }


            TTreatmentPt4b.AddCell(new Phrase(pb.WhenWillPatientSeenAgain, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));


            float[] fTreatmentPt4c = { 3.3f, 0.2f, 0.5f, 0.2f, 0.5f };
            PdfPTable TTreatmentPt4c = new PdfPTable(fTreatmentPt4c);
            TTreatmentPt4c.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TTreatmentPt4c.WidthPercentage = 100;

            TTreatmentPt4c.AddCell(new Phrase("If no, was patient referred back to attending doctor:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            if (pb.PatientAttendingDoctor == "1")
            {
                TTreatmentPt4c.AddCell(cellObj);
                TTreatmentPt4c.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TTreatmentPt4c.AddCell(cellObj1);
                TTreatmentPt4c.AddCell(new Phrase("No", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else if (pb.PatientAttendingDoctor == "2")
            {
                TTreatmentPt4c.AddCell(cellObj1);
                TTreatmentPt4c.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TTreatmentPt4c.AddCell(cellObj);
                TTreatmentPt4c.AddCell(new Phrase("No", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                TTreatmentPt4c.AddCell(cellObj1);
                TTreatmentPt4c.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TTreatmentPt4c.AddCell(cellObj1);
                TTreatmentPt4c.AddCell(new Phrase("No", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }

            TTreatmentPt4d.AddCell(TTreatmentPt4b);
            TTreatmentPt4d.AddCell(TTreatmentPt4c);

            TTreatmentPt4.AddCell(TTreatmentPt4d);

            TTreatmentPt.AddCell(TTreatmentPt4);

            float[] fTreatmentPt5main = { 4f };
            PdfPTable TTreatmentPt5main = new PdfPTable(fTreatmentPt5main);
            TTreatmentPt5main.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TTreatmentPt5main.WidthPercentage = 100;

            float[] fTreatmentPt5 = { 3f, 0.3f, 0.8f, 0.3f, 8f, 2.5f, 3f, 2.5f };
            PdfPTable TTreatmentPt5 = new PdfPTable(fTreatmentPt5);
            TTreatmentPt5.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TTreatmentPt5.WidthPercentage = 100;

            TTreatmentPt5.AddCell(new Phrase("5. Is patient working?", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            if (pb.PatienWorking == "1")
            {
                TTreatmentPt5.AddCell(cellObj);
                TTreatmentPt5.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TTreatmentPt5.AddCell(cellObj1);
                TTreatmentPt5.AddCell(new Phrase("No   If yes, date(s) patient: resumed limited work of any kind", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else if (pb.PatienWorking == "2")
            {
                TTreatmentPt5.AddCell(cellObj);
                TTreatmentPt5.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TTreatmentPt5.AddCell(cellObj1);
                TTreatmentPt5.AddCell(new Phrase("No   If yes, date(s) patient: resumed limited work of any kind", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                TTreatmentPt5.AddCell(cellObj1);
                TTreatmentPt5.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TTreatmentPt5.AddCell(cellObj1);
                TTreatmentPt5.AddCell(new Phrase("No   If yes, date(s) patient: resumed limited work of any kind", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }






            TTreatmentPt5.AddCell(new Phrase(pb.ResumedLimitedWork, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TTreatmentPt5.AddCell(new Phrase("resumed regular work", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TTreatmentPt5.AddCell(new Phrase(pb.ResumedRegularWork, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TTreatmentPt5main.AddCell(TTreatmentPt5);
            TTreatmentPt.AddCell(TTreatmentPt5main);

            THistory.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            THistory.AddCell(TTreatmentPt);
            THistory.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;

            THistory.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            THistory.AddCell(new Phrase("CR", iTextSharp.text.FontFactory.GetFont(FONT_NAME, SUBHEDER_FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fCr = { 12.5f, 0.2f, 0.8f, 0.2f, 0.8f };
            PdfPTable TCr = new PdfPTable(fCr);
            TCr.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TCr.WidthPercentage = 100;

            TCr.AddCell(new Phrase("6. Was the occurrence described above (or in your previous report) the competent producing cause of the injury or disability (if any) sustained?", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            if (pb.OccurenceCauseInjuryCHKBox == "1")
            {
                TCr.AddCell(cellObj);
                TCr.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TCr.AddCell(cellObj1);
                TCr.AddCell(new Phrase("No", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else if (pb.OccurenceCauseInjuryCHKBox == "2")
            {
                TCr.AddCell(cellObj1);
                TCr.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TCr.AddCell(cellObj);
                TCr.AddCell(new Phrase("No", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                TCr.AddCell(cellObj1);
                TCr.AddCell(new Phrase("Yes", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TCr.AddCell(cellObj1);
                TCr.AddCell(new Phrase("No", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            }
            THistory.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            THistory.AddCell(TCr);
            THistory.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;

            THistory.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            THistory.AddCell(new Phrase("REMARKS", iTextSharp.text.FontFactory.GetFont(FONT_NAME, SUBHEDER_FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));


            float[] fRemarks = { 4f };
            PdfPTable TRemarks = new PdfPTable(fRemarks);
            TRemarks.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TRemarks.WidthPercentage = 100;

            TRemarks.AddCell(new Phrase("7. Enter here additional pertinent information", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TRemarks.AddCell(new Phrase(pb.AdditionalPertinentInformation, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            THistory.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            THistory.AddCell(TRemarks);
            THistory.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;

            THistory.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            THistory.AddCell(new Phrase("\n\n\n\nBILLING \n\n\n FORM", iTextSharp.text.FontFactory.GetFont(FONT_NAME, SUBHEDER_FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fBillForm = { 4f };
            PdfPTable TBillForm = new PdfPTable(fBillForm);
            TBillForm.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TBillForm.WidthPercentage = 100;

            float[] fBillForm8 = { 2.6f, 1.7f };
            PdfPTable TBillForm8 = new PdfPTable(fBillForm8);
            TBillForm8.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TBillForm8.WidthPercentage = 100;


            TBillForm8.AddCell(new Phrase("8. Diagnosis or nature of disease or injury (Relate Items 1,2,3 or 4 to Item 9E by line.) Enter code and", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm8.AddCell(new Phrase("describe nature of injury.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
            float[] fBillForm8a = { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f };
            PdfPTable TBillForm8a = new PdfPTable(fBillForm8a);
            TBillForm8a.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            TBillForm8a.WidthPercentage = 100;

            ArrayList printDiag = new ArrayList();
            printDiag = pb.DiagAr;
            int diaCount = printDiag.Count;
            int columncount = 0;
            if (diaCount > 16)
                columncount = 8;
            else
            {

                int temp = 0;
                for (int i = 0; i < diaCount; i++)
                {
                    if (temp == i)
                    {
                        TBillForm8a.AddCell(new Phrase((i + 1).ToString() + ")" + printDiag[i].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                        temp += 2;
                        columncount++;
                    }
                }
                for (int i = 0; i < (8 - columncount); i++)
                {
                    TBillForm8a.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                }
                columncount = 0;
                temp = 1;
                for (int i = 0; i < diaCount; i++)
                {
                    if (temp == i)
                    {
                        TBillForm8a.AddCell(new Phrase((i + 1).ToString() + ")" + printDiag[i].ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                        temp += 2;
                        columncount++;
                    }
                }
                for (int i = 0; i < (8 - columncount); i++)
                {
                    TBillForm8a.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                }
            }




            //TBillForm8a.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm8a.AddCell(new Phrase(pb.DiagCodeA, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            //TBillForm8a.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm8a.AddCell(new Phrase(pb.DiagCodeC, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            //TBillForm8a.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm8a.AddCell(new Phrase(pb.DiagCodeB, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            //TBillForm8a.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm8a.AddCell(new Phrase(pb.DiagCodeD, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            //TBillForm8a.AddCell(new Phrase("1.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            //TBillForm8a.AddCell(new Phrase("21.15", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            //TBillForm8a.AddCell(new Phrase("3.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            //TBillForm8a.AddCell(new Phrase("15.18", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            //TBillForm8a.AddCell(new Phrase("2.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            //TBillForm8a.AddCell(new Phrase("18", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            //TBillForm8a.AddCell(new Phrase("4.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            //TBillForm8a.AddCell(new Phrase("16.00", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            float[] fBillForm9 = { 1.8f, 0.8f, 0.5f, 2f, 0.7f, 0.8f, 0.6f, 0.4f, 1.8f };
            PdfPTable TBillForm9 = new PdfPTable(fBillForm9);
            TBillForm9.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
            TBillForm9.WidthPercentage = 100;

            TBillForm9.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            TBillForm9.AddCell(new Phrase("9.           A", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            TBillForm9.AddCell(new Phrase("B", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("C", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("D (USE WCB CODES)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("E", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("F", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("G", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("H", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("I", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TBillForm9.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;

            TBillForm9.AddCell(new Phrase("Dates of Service", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("Place of", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("Leave", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("Procedures, Services or Supplies", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("Diagnosis", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("Days or", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("COB", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("Zip Code Where Service was", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TBillForm9.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            TBillForm9.AddCell(new Phrase("From          To", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("Service", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("Blank", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("(Explain Unusual Circumstances)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("Code", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("$ Charges", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("Units", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            TBillForm9.AddCell(new Phrase("Rendered", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            float[] fBillForm9box = { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.8f, 0.5f, 1f, 0.8f, 0.2f, 0.7f, 0.6f, 0.2f, 0.6f, 0.4f, 1.8f };
            PdfPTable TBillForm9box = new PdfPTable(fBillForm9box);
            TBillForm9box.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            TBillForm9box.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER | iTextSharp.text.Rectangle.LEFT_BORDER;
            TBillForm9box.WidthPercentage = 100;

            TBillForm9box.AddCell(new Phrase("MM", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("DD", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("YY", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("MM", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("DD", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("YY", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("CPT/HCPCS", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("MODIFIER", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TBillForm9box.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TBillForm9box.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;

            double totalCharge = 0.0f;
            for (int l = 0; l < ARR.Count && l < 6; l++)
            {
                ProcedureCodeDetails procobject = new ProcedureCodeDetails();
                procobject = (ProcedureCodeDetails)ARR[l];

                totalCharge = totalCharge + Convert.ToDouble(procobject.charges);

                TBillForm9box.AddCell(new Phrase(procobject.frMM, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.frDD, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.frYY, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.toMM, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.toDD, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.toYY, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.placeOfService, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.leaveBlank, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.CPT_HCPCS, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.modifier, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.submodifier, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.diagCode, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.charges, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.leaveBlank, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.daysUnit, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.COB, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
                TBillForm9box.AddCell(new Phrase(procobject.zip, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            }
            if (ARR.Count > 6)
            {
                for (int i = 0; i < 6; i++)
                {
                    ARR.RemoveAt(0);
                }

            }
            else
            {
                while (ARR.Count != 0)
                    ARR.RemoveAt(0);
            }



            TBillForm.AddCell(TBillForm8);
            TBillForm.AddCell(TBillForm8a);

            TBillForm.AddCell(TBillForm9);
            TBillForm.AddCell(TBillForm9box);

            THistory.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            THistory.AddCell(TBillForm);
            THistory.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;

            THistory.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            THistory.AddCell(new Phrase("\n\n\nSIGNATURE", iTextSharp.text.FontFactory.GetFont(FONT_NAME, SUBHEDER_FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            float[] fSignature = { 4f };
            PdfPTable TSignature = new PdfPTable(fSignature);
            TSignature.DefaultCell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            TSignature.WidthPercentage = 100;

            float[] fSignatureContaint1 = { 2.5f, 7f };
            PdfPTable TSignatureContaint1 = new PdfPTable(fSignatureContaint1);
            TSignatureContaint1.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            TSignatureContaint1.WidthPercentage = 100;

            float[] fSignature10 = { 4f };
            PdfPTable TSignature10 = new PdfPTable(fSignature10);
            TSignature10.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TSignature10.WidthPercentage = 100;

            float[] fSignature10a = { 2f, 0.4f, 0.4f };
            PdfPTable TSignature10a = new PdfPTable(fSignature10a);
            TSignature10a.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TSignature10a.WidthPercentage = 100;

            TSignature10a.AddCell(new Phrase("10. Federal Tax I.D. Number", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TSignature10a.AddCell(new Phrase("SSN", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TSignature10a.AddCell(new Phrase("EIN", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TSignature10a.AddCell(new Phrase(pb.FederalTaxIDNO, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            if (pb.ssn == "1")
                TSignature10a.AddCell(cellObj);
            else
                TSignature10a.AddCell(cellObj1);
            TSignature10a.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            if (pb.ein == "1")
                TSignature10a.AddCell(cellObj);
            else
                TSignature10a.AddCell(cellObj1);

            TSignature10.AddCell(TSignature10a);
            TSignatureContaint1.AddCell(TSignature10);

            float[] fSignature11 = { 1.4f, 1.5f, 1.3f, 1.4f, 1.4f };
            PdfPTable TSignature11 = new PdfPTable(fSignature11);
            TSignature11.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            TSignature11.WidthPercentage = 100;

            TSignature11.AddCell(new Phrase("11. WCB Authorization Number", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignature11.AddCell(new Phrase("12. Patient's Account Number", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignature11.AddCell(new Phrase("13. Total Charges", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignature11.AddCell(new Phrase("14. Amt. Paid (carrier use only)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignature11.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TSignature11.AddCell(new Phrase("15. Bal. Due (carrier use only)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TSignature11.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;

            TSignature11.AddCell(new Phrase(pb.FederalTaxIDNO, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignature11.AddCell(new Phrase(pb.PatientAccountNo, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignature11.AddCell(new Phrase(totalCharge.ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignature11.AddCell(new Phrase(pb.AmountPaid, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TSignature11.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            TSignature11.AddCell(new Phrase(totalCharge.ToString(), iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TSignatureContaint1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TSignatureContaint1.AddCell(TSignature11);

            float[] fSignatureContaint2 = { 9f, 1.8f };
            PdfPTable TSignatureContaint2 = new PdfPTable(fSignatureContaint2);
            TSignatureContaint2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TSignatureContaint2.WidthPercentage = 100;


            float[] fSignatureContaint2a = { 2.9f, 3.1f, 2.9f };
            PdfPTable TSignatureContaint2a = new PdfPTable(fSignatureContaint2a);
            TSignatureContaint2a.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            TSignatureContaint2a.WidthPercentage = 100;
            TSignatureContaint2a.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            TSignatureContaint2a.AddCell(new Phrase("Affirmed Under Penalty of Perjury", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignatureContaint2a.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            TSignatureContaint2a.AddCell(new Phrase("17. Psychologist's Name, Address & Phone No.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignatureContaint2a.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TSignatureContaint2a.AddCell(new Phrase("18. Billing Name, Address & Phone Number", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignatureContaint2a.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;

            TSignatureContaint2a.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            TSignatureContaint2a.AddCell(new Phrase(pb.SignatureOfTreatingPsychologist, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignatureContaint2a.AddCell(new Phrase(pb.PsychologistNameAdressPhoneNO, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignatureContaint2a.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            TSignatureContaint2a.AddCell(new Phrase(pb.BillingNameAdressPhoneNO, iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignatureContaint2a.DefaultCell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
            TSignatureContaint2a.AddCell(new Phrase("16. Signature of Treating Psychologist  Date", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignatureContaint2a.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TSignatureContaint2a.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TSignatureContaint2a.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            float[] fSignatureContaint2b = { 4f };
            PdfPTable TSignatureContaint2b = new PdfPTable(fSignatureContaint2b);
            TSignatureContaint2b.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
            TSignatureContaint2b.WidthPercentage = 100;

            TSignatureContaint2b.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            TSignatureContaint2b.AddCell(new Phrase("THE INJURED WORKER SHOULD NOT PAY THIS BILL.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            TSignatureContaint2.AddCell(TSignatureContaint2a);
            TSignatureContaint2.AddCell(TSignatureContaint2b);

            TSignature.AddCell(TSignatureContaint1);
            TSignature.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TSignature.AddCell(TSignatureContaint2);

            THistory.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            THistory.AddCell(TSignature);
            tblBase.AddCell(THistory);
            tblMain.AddCell(tblBase);

            float[] fFooter = { 1f, 3f, 1f };
            PdfPTable tFooter = new PdfPTable(fFooter);
            tFooter.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tFooter.WidthPercentage = 100;

            tFooter.AddCell(new Phrase("PS-4 (1-11)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tFooter.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tFooter.AddCell(new Phrase("SEE REVERSE SIDE FOR IMPORTANT INFORMATION", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            tFooter.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tFooter.AddCell(new Phrase("www.wcb.ny.gov", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            tblMain.AddCell(tFooter);

            return tblMain;

        }

        public PdfPTable GenerateSecond(int FONT_SIZE, int HEADER_FONT_SIZE, int SUBHEDER_FONT_SIZE, string FONT_NAME, int LAST_PAGE_HEADER)
        {
            float[] wMain = { 6f };
            PdfPTable tblMain = new PdfPTable(wMain);
            tblMain.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tblMain.WidthPercentage = 100;

            float[] fHeader2 = { 4f };
            PdfPTable THeader2 = new PdfPTable(fHeader2);
            THeader2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            THeader2.WidthPercentage = 100;
            THeader2.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            THeader2.AddCell(new Phrase("IMPORTANT", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD | Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
            THeader2.AddCell(new Phrase("TO THE PSYCHOLOGIST", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD | Font.UNDERLINE, iTextSharp.text.Color.BLACK)));

            tblMain.AddCell(THeader2);

            float[] fPsychoInstruction = { 4f };
            PdfPTable TPsychoInstruction = new PdfPTable(fPsychoInstruction);
            TPsychoInstruction.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPsychoInstruction.WidthPercentage = 100;

            float[] fPsychoInstruction1 = { 4f };
            PdfPTable TPsychoInstruction1 = new PdfPTable(fPsychoInstruction1);
            TPsychoInstruction1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPsychoInstruction1.WidthPercentage = 100;

            TPsychoInstruction1.AddCell(new Phrase("1.   This form is to be used to file reports in workers' compensation, volunteer firefighters' or volunteer ambulance workers' benefit cases as follows:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction1.AddCell(new Phrase("             48 HOUR INITIAL REPORT - File this form, complete in all details, within 48 hours after you first render treatment.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction1.AddCell(new Phrase("             15 DAY INITIAL REPORT - File this form within 15 days after you first render treatment.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction1.AddCell(new Phrase("             90 DAY PROGRESS REPORT - Following the filing of the 15 day Initial Report, file this form and thereafter during continuing treatment without further request,", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction1.AddCell(new Phrase("             when a follow-up visit is necessary, except the intervals between reports shall be no more than 90 days.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction1.AddCell(new Phrase("      All reports are to be filed with the Workers' Compensation Board, the workers' compensation insurance carrier (or self-insured employer), and if the patient is represented", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction1.AddCell(new Phrase("      by an attorney or licensed representative, with such representative. If the claimant is not represented, a copy must be sent to the claimant.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPsychoInstruction.AddCell(TPsychoInstruction1);

            float[] fPsychoInstruction2 = { 4f };
            PdfPTable TPsychoInstruction2 = new PdfPTable(fPsychoInstruction2);
            TPsychoInstruction2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPsychoInstruction2.WidthPercentage = 100;

            TPsychoInstruction2.AddCell(new Phrase("2.   Please ask your patient for his/her WCB Case Number and the Insurance Carrier's Case Number, if they are known to him/her, and show these numbers on your reports. In", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction2.AddCell(new Phrase("      addition, ask your patient if he/she has retained a representative. If so, ask for the name and address of the representative. You are required to send copies of all reports to", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction2.AddCell(new Phrase("      the patient's representative, if any.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPsychoInstruction.AddCell(TPsychoInstruction2);

            float[] fPsychoInstruction3 = { 4f };
            PdfPTable TPsychoInstruction3 = new PdfPTable(fPsychoInstruction3);
            TPsychoInstruction3.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPsychoInstruction3.WidthPercentage = 100;

            TPsychoInstruction3.AddCell(new Phrase("3.   This form must be signed by the psychologist and must contain his/her authorization number, address and telephone number.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPsychoInstruction.AddCell(TPsychoInstruction3);


            float[] fPsychoInstruction4 = { 4f };
            PdfPTable TPsychoInstruction4 = new PdfPTable(fPsychoInstruction4);
            TPsychoInstruction4.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPsychoInstruction4.WidthPercentage = 100;

            float[] fPsychoInstruction4Head = { 1.2f, 2.8f };
            PdfPTable TPsychoInstruction4Head = new PdfPTable(fPsychoInstruction4Head);
            TPsychoInstruction4Head.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPsychoInstruction4Head.WidthPercentage = 100;

            TPsychoInstruction4Head.AddCell(new Phrase("4.   AUTHORIZATION FOR SPECIAL SERVICES", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction4Head.AddCell(new Phrase(" - Prior authorization for procedures enumerated in Section 13-a (5) of the Workers' Compensation Law costing more", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPsychoInstruction4.AddCell(TPsychoInstruction4Head);

            TPsychoInstruction4.AddCell(new Phrase("      than $1,000 or those procedures requiring pre-authorization pursuant to the Medical Treatment Guidelines, must be requested from the self-insured employer or insurance", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction4.AddCell(new Phrase("      carrier. In addition, authorization must be requested for any biofeedback treatments, regardless of the cost, or any special diagnostic laboratory tests which may be", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction4.AddCell(new Phrase("      performed by psychologists. Where a claimant has been referred by an authorized physician to a psychologist for evaluation purposes only and not for treatment, prior", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction4.AddCell(new Phrase("      authorization must be requested if the cost of consultation exceeds $1,000. Prior authorization is not necessary if the procedure/treatment is consistent with the", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction4.AddCell(new Phrase("      Medical than Treatment Guidelines.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPsychoInstruction.AddCell(TPsychoInstruction4);

            float[] fPsychoInstruction5 = { 4f };
            PdfPTable TPsychoInstruction5 = new PdfPTable(fPsychoInstruction5);
            TPsychoInstruction5.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPsychoInstruction5.WidthPercentage = 100;

            TPsychoInstruction5.AddCell(new Phrase("5.   AUTHORIZATION MUST BE REQUESTED AS FOLLOWS:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction5.AddCell(new Phrase("      a. Telephone the self-insured employer or insurance carrier, explain the need for the special services, and request the necessary authorization.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction5.AddCell(new Phrase("      b. Confirm the request in writing, setting forth the medical necessity for the special services in item 3b(2) on this form. Attach copy of request, if necessary.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction5.AddCell(new Phrase("      c. The self-insured employer or insurance carrier may have the patient examined within 4 working days of the request for authorization, if the patient is hospitalized, or", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction5.AddCell(new Phrase("      within 30 calendar days if the patient is not hospitalized.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction5.AddCell(new Phrase("      d. If authorization or denial is not forthcoming within 30 calendar days, notify the nearest office of the Workers' Compensation Board.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPsychoInstruction.AddCell(TPsychoInstruction5);

            float[] fPsychoInstruction6 = { 4f };
            PdfPTable TPsychoInstruction6 = new PdfPTable(fPsychoInstruction6);
            TPsychoInstruction6.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPsychoInstruction6.WidthPercentage = 100;

            float[] fPsychoInstruction6Head = { 1.2f, 2.7f };
            PdfPTable TPsychoInstruction6Head = new PdfPTable(fPsychoInstruction6Head);
            TPsychoInstruction6Head.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPsychoInstruction6Head.WidthPercentage = 100;

            TPsychoInstruction6Head.AddCell(new Phrase("6.   LIMITATION OF PSYCHOLOGY TREATMENT ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction6Head.AddCell(new Phrase("- Treatment by a psychologist is limited as defined in Article 153 of the Education Law, in the Workers' Compensation", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction6.AddCell(TPsychoInstruction6Head);
            TPsychoInstruction6.AddCell(new Phrase("        Law, and the Rules of the Chair relative to Psychology Practice.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPsychoInstruction.AddCell(TPsychoInstruction6);

            float[] fPsychoInstruction7 = { 4f };
            PdfPTable TPsychoInstruction7 = new PdfPTable(fPsychoInstruction7);
            TPsychoInstruction7.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPsychoInstruction7.WidthPercentage = 100;

            float[] fPsychoInstruction7Head = { 1.5f, 10.2f };
            PdfPTable TPsychoInstruction7Head = new PdfPTable(fPsychoInstruction7Head);
            TPsychoInstruction7Head.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPsychoInstruction7Head.WidthPercentage = 100;

            TPsychoInstruction7Head.AddCell(new Phrase("7.   HIPAA Notice ", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction7Head.AddCell(new Phrase("- In order to adjudicate a workers' compensation claim, WCL13-a(4)(a) and 12 NYCRR 325-1.3 require health care providers to regularly file medical", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction7.AddCell(TPsychoInstruction7Head);
            TPsychoInstruction7.AddCell(new Phrase("      reports of treatment with the Board and the carrier or employer. Pursuant to 45 CFR 164.512 these legally required medical reports are exempt from HIPAA's restrictions on", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPsychoInstruction7.AddCell(new Phrase("      disclosure of health information.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPsychoInstruction.AddCell(TPsychoInstruction7);

            tblMain.AddCell(TPsychoInstruction);

            float[] fDeclarationBox = { 3f };
            PdfPTable TDeclarationBox = new PdfPTable(fDeclarationBox);
            TDeclarationBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
            TDeclarationBox.WidthPercentage = 60;

            float[] fDeclaration = { 3f };
            PdfPTable TDeclaration = new PdfPTable(fDeclaration);
            TDeclaration.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TDeclaration.WidthPercentage = 60;

            TDeclaration.AddCell(new Phrase("ANY PERSON WHO KNOWINGLY AND WITH INTENT TO DEFRAUD PRESENTS, CAUSES TO BE PRESENTED, OR", iTextSharp.text.FontFactory.GetFont(FONT_NAME, HEADER_FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TDeclaration.AddCell(new Phrase("PREPARES WITH KNOWLEDGE OR BELIEF THAT IT WILL BE PRESENTED TO OR BY AN INSURER, OR SELF-INSURER,", iTextSharp.text.FontFactory.GetFont(FONT_NAME, HEADER_FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TDeclaration.AddCell(new Phrase("ANY INFORMATION CONTAINING ANY FALSE MATERIAL STATEMENT OR CONCEALS ANY MATERIAL FACT SHALL BE", iTextSharp.text.FontFactory.GetFont(FONT_NAME, HEADER_FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TDeclaration.AddCell(new Phrase("GUILTY OF A CRIME AND SUBJECT TO SUBSTANTIAL FINES AND IMPRISONMENT.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, HEADER_FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TDeclarationBox.AddCell(TDeclaration);

            tblMain.AddCell(TDeclarationBox);

            float[] fPatientInstuctionBox = { 4f };
            PdfPTable TPatientInstuctionBox = new PdfPTable(fPatientInstuctionBox);
            TPatientInstuctionBox.DefaultCell.Border = iTextSharp.text.Rectangle.BOX;
            TPatientInstuctionBox.WidthPercentage = 100;

            float[] fPatientInstuction = { 4f };
            PdfPTable TPatientInstuction = new PdfPTable(fPatientInstuction);
            TPatientInstuction.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TPatientInstuction.WidthPercentage = 100;

            TPatientInstuction.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            TPatientInstuction.AddCell(new Phrase("IMPORTANT TO THE PATIENT", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            TPatientInstuction.AddCell(new Phrase("YOUR DOCTORS' BILLS (AND BILLS FOR HOSPITALS AND OTHER SERVICES OF A MEDICAL NATURE) WILL BE PAID BY YOUR EMPLOYER, THE LIABLE", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell(new Phrase("POLITICAL SUBDIVISION OR ITS INSURANCE COMPANY OR THE UNAFFILIATED VOLUNTEER AMBULANCE SERVICE IF YOUR CLAIM IS ALLOWED. DO NOT PAY", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell(new Phrase("THESE BILLS YOURSELF, UNLESS YOUR CASE IS DISALLOWED OR CLOSED FOR FAILURE TO PROSECUTE.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell("");
            TPatientInstuction.AddCell(new Phrase("IF YOU HAVE ANY QUESTIONS CONCERNING THIS NOTICE OR YOUR CASE, OR WITH RESPECT TO YOUR RIGHTS UNDER THE WORKERS' COMPENSATION", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell(new Phrase("LAW, OR THE VOLUNTEER FIREFIGHTERS' OR VOLUNTEER AMBULANCE WORKERS' LAWS, YOU SHOULD CONSULT THE NEAREST OFFICE OF THE BOARD", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell(new Phrase("FOR ADVICE. ALWAYS USE THE CASE NUMBERS SHOWN ON THE OTHER SIDE OF THIS NOTICE, OR ON OTHER PAPERS RECEIVED BY YOU, IF YOU FIND IT", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell(new Phrase("NECESSARY TO COMMUNICATE WITH THE BOARD OR THE CARRIER. ALSO, MENTION YOUR SOCIAL SECURITY NUMBER IF YOU WRITE OR CALL THE BOARD.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell("");
            TPatientInstuction.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            TPatientInstuction.AddCell(new Phrase("IMPORTANTE PARA EL PACIENTE", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            TPatientInstuction.AddCell(new Phrase("LAS FACTURAS POR SERVICIOS MEDICOS INCLUYENDO HOSPITALES Y TODO SERVICIO DE NATURALEZA MEDICA SERA PAGADO POR EL PATRONO O POR", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell(new Phrase("LA ENTIDAD RESPONSABLE O SU COMPANIA DE SEGUROS SEGUN SEA EL CASO; SI SU RECLAMACION ES APROBADA. NO PAGUE ESTAS FACTURAS A", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell(new Phrase("MENOS QUE SU CASO SEA DESESTIMADO EN SU FONDO O ARCHIVADO POR NO REALIZAR LOS TRAMITES CORRESPONDIENTES.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell("");
            TPatientInstuction.AddCell(new Phrase("SI USTED TIENE ALGUNA PREGUNTA, EN RELACION A ESTA NOTIFICACION O A SU CASO O EN RELACION A SUS DERECHOS BAJO LA LEY DE COMPENSACION", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell(new Phrase("OBRERA O LA LEY DE BOMBEROS VOLUNTARIOS O LA LEY DE SERVICIOS DE AMBULANCIAS VOLUNTARIOS DEBE COMUNICARSE CON LA OFICINA MAS", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell(new Phrase("CERCANA DE LA JUNTA PARA ORIENTACION. SIEMPRE USE EL NUMERO DEL CASO QUE APARECE EN LA PARTE DEL FRENTE DE ESTA NOTIFICACION, O EN", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell(new Phrase("OTROS DOCUMENTOS RECIBIDOS POR USTED. SI LE ES NECESARIO COMUNICARSE CON LA JUNTA O CON EL \"CARRIER.\" TAMBIEN MENCIONE EN SU", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TPatientInstuction.AddCell(new Phrase("COMUNICACION ORAL O ESCRITA SU NUMERO DE SEGURO SOCIAL.", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));

            TPatientInstuctionBox.AddCell(TPatientInstuction);
            tblMain.AddCell(TPatientInstuctionBox);

            float[] fWCAddress = { 4f };
            PdfPTable TWCAddress = new PdfPTable(fWCAddress);
            TWCAddress.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TWCAddress.WidthPercentage = 60;

            TWCAddress.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            TWCAddress.AddCell(new Phrase("__________________________________________________________________", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Color.BLACK)));

            TWCAddress.AddCell(new Phrase("Reports should be sent directly to the Workers' Compensation Board address listed below:", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            TWCAddress.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            TWCAddress.AddCell(new Phrase("NYS Workers' Compensation Board", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            TWCAddress.AddCell(new Phrase("Centralized Mailing", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            TWCAddress.AddCell(new Phrase("PO Box 5205", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            TWCAddress.AddCell(new Phrase("Binghamton, NY 13902-5205", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            tblMain.AddCell(TWCAddress);

            float[] fCustCare = { 1.5f, 1.5f };
            PdfPTable TCustCare = new PdfPTable(fCustCare);
            TCustCare.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TCustCare.WidthPercentage = 60;

            TCustCare.AddCell(new Phrase("Customer Service Toll-Free Line: 877-632-4996", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));
            TCustCare.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            TCustCare.AddCell(new Phrase("Statewide Fax Line: 877-533-0337", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            tblMain.AddCell(TCustCare);


            float[] fFooter2 = { 4f };
            PdfPTable TFooter2 = new PdfPTable(fFooter2);
            TFooter2.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            TFooter2.WidthPercentage = 100;

            TFooter2.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            TFooter2.AddCell(new Phrase("THE WORKERS' COMPENSATION BOARD EMPLOYS AND SERVES PEOPLE WITH DISABILITIES WITHOUT DISCRIMINATION", iTextSharp.text.FontFactory.GetFont(FONT_NAME, FONT_SIZE, iTextSharp.text.Color.BLACK)));
            TFooter2.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            TFooter2.AddCell(new Phrase("PS-4 Reverse (1-11)", iTextSharp.text.FontFactory.GetFont(FONT_NAME, LAST_PAGE_HEADER, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK)));

            tblMain.AddCell(TFooter2);

            return tblMain;
        }

    }
}
