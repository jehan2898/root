using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using log4net;
using mbs.LienBills;
using iTextSharp.text.pdf;
using iTextSharp;
using iTextSharp.text.html;
using iTextSharp.text;

/// <summary>
/// Summary description for Bill_Sys_GenerateFUReport
/// </summary>
public class Bill_Sys_GenerateFUReport
{
    string sz_UserID = "";
    string sz_CompanyID = "";
    String szLastPDFFileName = "";
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    CaseDetailsBO objCaseDetailsBO;
    PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    Bill_Sys_NF3_Template objNF3Template;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    private ArrayList pdfbills;
    private ArrayList EventId;
    private ArrayList objAL;

    private Bill_Sys_ReportBO _reportBO;

    SqlConnection Sqlcon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);

    private static ILog log = LogManager.GetLogger("Master Billing");

    public Bill_Sys_GenerateFUReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /* Added by: Kunal
     * Generate FUReport for doctors visits*/

    //For AC Speciality
    public void billsperpatient(ArrayList pdfbills, string szUserName, string szUserId, string szCompanyId, string strCheckPath, string strUnCheckPath, string szSpecialityID)
    {
        SqlDataAdapter da;
        string pdfpath = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        string query;
        try
        {
            for (int ii = 0; ii < pdfbills.Count; ii++)
            {
                query = "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + pdfbills[ii].ToString() + "', @FLAG='PDF'";
                da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //create file
                string filename = "FUReport_" + pdfbills[ii] + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                string filepath = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')) + "/" +
                                  ds.Tables[0].Rows[0]["CASEID"].ToString() + "/No Fault File/Medicals/AC/FUReport/";
                string destinationdir = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')) + "\\" +
                                         ds.Tables[0].Rows[0]["CASEID"].ToString() + "\\No Fault File\\Medicals\\AC\\FUReport\\";
                pdfpath = ConfigurationManager.AppSettings["DocumentManagerURL"] + filepath + filename;
                MemoryStream m = new MemoryStream();
                //create pdf
                iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 36, 36, 20, 36);
                PdfWriter writer = PdfWriter.GetInstance(document, m);
                document.Open();
                string[] officeadd = ds.Tables[0].Rows[0]["Office_Address"].ToString().Split(';');
                string officeaddress = "";
                for (int z = 0; z < officeadd.Length; z++)
                {
                    if ((officeadd[z] != null) && (officeadd[z] != ""))
                        officeaddress = officeaddress + officeadd[z] + ",";
                }
                string[] companyadd = ds.Tables[0].Rows[0]["Company_Address"].ToString().Split(';');
                string companyaddress = "";
                for (int z = 0; z < companyadd.Length; z++)
                {
                    if ((companyadd[z] != null) && (companyadd[z] != ""))
                        companyaddress = companyaddress + companyadd[z] + ",";
                }
                #region heading table
                float[] width ={ 1f, 2f, 1f, 2f, .5f, 1f };
                PdfPTable heading = new PdfPTable(width);
                heading.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                heading.DefaultCell.Border = Rectangle.NO_BORDER;
                heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                heading.DefaultCell.Colspan = 6;
                heading.AddCell(new Phrase("ACUPUNCTURE DAILY TREATMENT NOTES", iTextSharp.text.FontFactory.GetFont("Arial", 12, Font.BOLD, iTextSharp.text.Color.BLACK)));
                // heading.AddCell(new Phrase(companyaddress, iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                heading.AddCell(new Phrase(""));
                heading.DefaultCell.Colspan = 1;
                heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                heading.AddCell(new Phrase("Case #", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                heading.DefaultCell.Colspan = 5;
                heading.AddCell(new Phrase(ds.Tables[0].Rows[0]["CaseNo"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                heading.DefaultCell.Colspan = 1;
                heading.AddCell(new Phrase("Last Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                heading.AddCell(new Phrase(ds.Tables[0].Rows[0]["Patient_Last_Name"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                heading.AddCell(new Phrase("First Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                heading.AddCell(new Phrase(ds.Tables[0].Rows[0]["Patient_First_Name"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                heading.AddCell(new Phrase("DOA", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                heading.AddCell(new Phrase(Convert.ToDateTime(ds.Tables[0].Rows[0]["Accident_Date"]).ToString("MM/dd/yyyy"), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                heading.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin, writer.DirectContent);
                #endregion
                #region Datatable
                width = new float[] { 1f, 1.5f, 10f };
                int i = 0;
                PdfPTable datatable = new PdfPTable(width);
                datatable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                datatable.AddCell(new Phrase(""));
                datatable.AddCell(new Phrase("Date", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                datatable.AddCell(new Phrase(""));
                //data starts
                Boolean check = true;
                int count = 0;
                while (check)
                {
                    if (datatable.TotalHeight > document.PageSize.Height - document.TopMargin - document.BottomMargin - 50)
                    {
                        datatable.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin - heading.TotalHeight - 1, writer.DirectContent);
                        document.NewPage();
                        heading.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin, writer.DirectContent);
                        datatable = new PdfPTable(width);
                        datatable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                        heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        datatable.AddCell(new Phrase(""));
                        datatable.AddCell(new Phrase("Date", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        datatable.AddCell(new Phrase(""));
                    }
                    datatable.AddCell(new Phrase(((++count).ToString()), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    datatable.AddCell(new Phrase(Convert.ToDateTime(ds.Tables[0].Rows[i]["Visit_Date"]).ToString("MM/dd/yyyy"), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    //Create Table For Showing Procedure Codes, Doctor Notes, Complaints
                    float[] outertablewidth ={ 3f, 2f, 2f };
                    PdfPTable outertable = new PdfPTable(outertablewidth);
                    outertable.TotalWidth = datatable.TotalWidth - 4f;
                    outertable.DefaultCell.Border = Rectangle.NO_BORDER;
                    //Add Procedure Codes
                    float[] innerwidth ={ 1f, 3f };
                    PdfPTable innertable1 = new PdfPTable(innerwidth);
                    innertable1.DefaultCell.Border = Rectangle.NO_BORDER;
                    //innertable1.DefaultCell.PaddingBottom = 3f;
                    //innertable1.DefaultCell.PaddingLeft = 3f;
                    //innertable1.DefaultCell.PaddingRight = 3f;
                    //innertable1.DefaultCell.PaddingTop = 3f;
                    innertable1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    innertable1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    int u = i;
                    int z = 0;
                    PdfPTable innertable11 = null;
                    char no = Convert.ToChar(82);

                    DataSet dsProc = new DataSet();
                    dsProc = GET_PROCEDURECODE_USING_EVENTID(ds.Tables[0].Rows[i]["EventID"].ToString());

                    string path = strCheckPath;
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(path);

                    if (ds.Tables[0].Rows[u]["Code"].ToString() == "")
                        innertable1.AddCell(new Phrase(""));
                    else
                    {
                        //float[] innertable11width ={ 1f, 2f };
                        //innertable11 = new PdfPTable(innertable11width);
                        //innertable11.DefaultCell.Border = Rectangle.NO_BORDER;
                        img.ScaleAbsolute(10f, 10f);
                        for (int Count = 0; Count < dsProc.Tables[0].Rows.Count; Count++)
                        {

                            PdfPCell cell1 = new PdfPCell(img);
                            cell1.Border = Rectangle.NO_BORDER;
                            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
                            cell1.VerticalAlignment = Element.ALIGN_TOP;
                            //innertable11.DefaultCell.Colspan = 2;
                            cell1.FixedHeight = 12f;
                            //innertable11.AddCell(cell1);
                            innertable1.AddCell(cell1);

                            innertable1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            innertable1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                            //innertable11.AddCell(new Phrase(dsProc.Tables[0].Rows[Count]["Column1"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                            innertable1.AddCell(new Phrase(dsProc.Tables[0].Rows[Count]["Column1"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));


                        }
                        //innertable1.AddCell(innertable11);
                    }
                    while (ds.Tables[0].Rows[i]["EventID"].ToString() == ds.Tables[0].Rows[u]["EventID"].ToString())
                    {
                        z++;
                        if (z == 3)
                            z = 0;
                        if (u == ds.Tables[0].Rows.Count - 1)
                        {
                            check = false;
                            break;
                        }
                        u++;
                    }

                    if (z != 0)
                    {
                        while (z != 3)
                        {
                            innertable1.AddCell(new Phrase(""));
                            z++;
                        }
                    }
                    outertable.AddCell(innertable1);
                    //Add Doctor's Notes
                    innertable1 = new PdfPTable(1);
                    innertable1.DefaultCell.Border = Rectangle.NO_BORDER;
                    innertable1.DefaultCell.PaddingBottom = 3f;
                    innertable1.DefaultCell.PaddingLeft = 3f;
                    innertable1.DefaultCell.PaddingRight = 3f;
                    innertable1.DefaultCell.PaddingTop = 3f;
                    innertable1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    innertable1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                    innertable1.AddCell(new Phrase("DOCTOR NOTES:-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
                    innertable1.AddCell(new Phrase(ds.Tables[0].Rows[i]["DOCTOR_NOTE"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    outertable.AddCell(innertable1);
                    //Add Complaints
                    innertable1 = new PdfPTable(1);
                    innertable1.DefaultCell.Border = Rectangle.NO_BORDER;
                    innertable1.DefaultCell.PaddingBottom = 3f;
                    innertable1.DefaultCell.PaddingLeft = 3f;
                    innertable1.DefaultCell.PaddingRight = 3f;
                    innertable1.DefaultCell.PaddingTop = 3f;
                    innertable1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    innertable1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                    innertable1.AddCell(new Phrase("Complaints:-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Color.BLACK)));
                    innertable1.AddCell(new Phrase(ds.Tables[0].Rows[i]["Complaints"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    outertable.AddCell(innertable1);

                    outertable.DefaultCell.Colspan = 3;
                    outertable.AddCell(new Phrase(""));
                    outertable.AddCell(new Phrase(""));
                    outertable.AddCell(new Phrase(""));
                    outertable.AddCell(new Phrase(""));
                    width = new float[] { 1f, 1.5f, 1f, 1.5f };
                    PdfPTable innertable2 = new PdfPTable(width);
                    innertable2.DefaultCell.PaddingLeft = 3f;
                    innertable2.DefaultCell.Border = Rectangle.NO_BORDER;
                    innertable2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    innertable2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                    innertable2.AddCell(new Phrase("Patient's Signature", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    innertable2.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    //patient's signature img start
                    try
                    {
                        iTextSharp.text.Image patientim = iTextSharp.text.Image.GetInstance(ds.Tables[0].Rows[u]["Patient_Sign"].ToString().Replace("/", "\\"));
                        patientim.ScaleAbsoluteHeight(30);
                        patientim.ScaleAbsoluteWidth(50);
                        PdfPCell imagecell1 = new PdfPCell(patientim);
                        imagecell1.HorizontalAlignment = Element.ALIGN_LEFT;
                        imagecell1.Border = Rectangle.BOTTOM_BORDER;
                        imagecell1.PaddingBottom = 1f;
                        innertable2.AddCell(imagecell1);
                    }
                    catch
                    {
                        innertable2.AddCell(new Phrase(""));
                    }
                    //end
                    innertable2.DefaultCell.Border = Rectangle.NO_BORDER;
                    innertable2.AddCell(new Phrase("Provider's Signature", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    innertable2.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    //provider's signature img start
                    try
                    {
                        iTextSharp.text.Image providerim = iTextSharp.text.Image.GetInstance(ds.Tables[0].Rows[u]["Doctor_Sign"].ToString().Replace("/", "\\"));
                        providerim.ScaleAbsoluteHeight(30);
                        providerim.ScaleAbsoluteWidth(50);
                        PdfPCell imagecell2 = new PdfPCell(providerim);
                        imagecell2.HorizontalAlignment = Element.ALIGN_LEFT;
                        imagecell2.Border = Rectangle.BOTTOM_BORDER;
                        imagecell2.PaddingBottom = 1f;
                        innertable2.AddCell(imagecell2);
                    }
                    catch
                    {
                        innertable2.AddCell(new Phrase(""));
                    }
                    innertable2.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    innertable2.DefaultCell.Border = Rectangle.NO_BORDER;
                    //end
                    innertable2.AddCell(new Phrase(""));
                    innertable2.AddCell(new Phrase(""));
                    innertable2.DefaultCell.Colspan = 2;
                    innertable2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    innertable2.AddCell(new Phrase(officeaddress, iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    outertable.AddCell(innertable2);
                    datatable.AddCell(outertable);
                    i = u;
                }
                string sz_Case_id = ds.Tables[0].Rows[0]["CASEID"].ToString();
                datatable.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin - heading.TotalHeight - 1, writer.DirectContent);
                document.Close();
                query = "exec SP_INSERT_AC_BILLING_REPORT_TO_DOCMANAGER @SZ_CASE_ID='" + ds.Tables[0].Rows[0]["CASEID"].ToString() + "', @SZ_FILE_NAME='" + filename + "', " +
                        "@SZ_FILE_PATH='" + filepath + "', @SZ_LOGIN_ID ='" + szUserName + "'";
                da = new SqlDataAdapter(query, con);
                ds = new DataSet();
                da.Fill(ds);
                destinationdir = ds.Tables[1].Rows[0][0].ToString() + destinationdir;

                string szImageID = ds.Tables[2].Rows[0][0].ToString();

                if (!Directory.Exists(destinationdir))
                    Directory.CreateDirectory(destinationdir);
                System.IO.File.WriteAllBytes(destinationdir + filename, m.GetBuffer());
                #endregion

                string ImageId = ds.Tables[2].Rows[0][0].ToString();

                query = "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + pdfbills[ii].ToString() + "', @FLAG='GET_EVENT_ID'";
                da = new SqlDataAdapter(query, con);
                DataSet dsEventId = new DataSet();
                da.Fill(dsEventId);

                if (dsEventId.Tables.Count >= 0)
                {
                    for (int Event = 0; Event < dsEventId.Tables[0].Rows.Count; Event++)
                    {
                        query = "exec SP_UPLOAD_REPORT_FOR_VISIT  @SZ_USER_NAME='" + szUserName + "',@SZ_FILE_NAME='" + filename + "',@SZ_COMPANY_ID='" + szCompanyId + "', @SZ_CASE_ID='" + sz_Case_id + "'," +
                                " @SZ_USER_ID ='" + szUserId + "',@SZ_PROCEDURE_GROUP_ID ='" + szSpecialityID + "',@I_IMAGE_ID ='" + ImageId + "',@SZ_EVENT_ID ='" + dsEventId.Tables[0].Rows[Event][0].ToString() + "'";
                        da = new SqlDataAdapter(query, con);
                        ds = new DataSet();
                        da.Fill(ds);
                    }
                }

                InsertFUReport(pdfbills[ii].ToString(), sz_Case_id, szSpecialityID, filename, filepath, szCompanyId, szUserName, szImageID);
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    //For SYN Speciality
    public void SYNbillsperpatient(ArrayList pdfbills, string szUserName, string szUserId, string szCompanyId, string strCheckPath, string strUnCheckPath, string szSpecialityID)
    {
        SqlDataAdapter da;
        string pdfpath = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        objNF3Template = new Bill_Sys_NF3_Template();
        string query;
        try
        {
            query = "exec [SP_PDFBILLS_MASTERBILLING_SYN]  @SZ_BILL_NUMBER='" + pdfbills[0].ToString() + "', @FLAG='PDF'";
            da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string sz_case_id = ds.Tables[0].Rows[0]["CASEID"].ToString();
            Document doc = new Document(iTextSharp.text.PageSize.A4);

            string filename = "FUReport_" + pdfbills[0] + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
            string filepath = "", destinationdir = "";
            filepath = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')) + "/" +
                              ds.Tables[0].Rows[0]["CASEID"].ToString() + "/No Fault File/Medicals/SYN/FUReport/";
            destinationdir = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')) + "\\" +
                                     ds.Tables[0].Rows[0]["CASEID"].ToString() + "\\No Fault File\\Medicals\\SYN\\FUReport\\";
            pdfpath = ConfigurationManager.AppSettings["DocumentManagerURL"] + filepath + filename;
            MemoryStream m = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, m);
            doc.Open();

            PdfPTable HeadingTable = new PdfPTable(1);

            HeadingTable.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            HeadingTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            HeadingTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            HeadingTable.DefaultCell.Border = Rectangle.NO_BORDER;
            HeadingTable.AddCell(new Phrase(ds.Tables[0].Rows[0]["Office_Name"].ToString(), FontFactory.GetFont("Arial", 9, Font.BOLD)));
            //HeadingTable.AddCell(new Phrase(ds.Tables[0].Rows[0]["Office_Address"].ToString(), FontFactory.GetFont("Arial", 9, Font.BOLD)));
            //HeadingTable.AddCell(new Phrase(ds.Tables[0].Rows[0]["Office_State"].ToString() + "," + ds.Tables[0].Rows[0]["Office_City"].ToString() + " " + ds.Tables[0].Rows[0]["Office_Zip"].ToString(), FontFactory.GetFont("Arial", 9, Font.BOLD)));
            HeadingTable.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin, writer.DirectContent);
            float Previous_Height = HeadingTable.TotalHeight;
            PdfPTable HeadingTable1 = new PdfPTable(1);
            HeadingTable1.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            HeadingTable1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            HeadingTable1.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            HeadingTable1.DefaultCell.Border = Rectangle.NO_BORDER;
            HeadingTable1.AddCell(new Phrase("CLINICAL BIOELECTRONIC MEDICINE USING SYNAPTIC - 3400", FontFactory.GetFont("Arial", 12)));
            HeadingTable1.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height - 30, writer.DirectContent);
            Previous_Height = Previous_Height + HeadingTable1.TotalHeight + 30;
            float[] width = new float[] { 1f, 3f, 1f, 3f };
            PdfPTable PatientDetails = new PdfPTable(width);
            PatientDetails.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            PatientDetails.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            PatientDetails.DefaultCell.Border = Rectangle.NO_BORDER;
            PatientDetails.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //1st row
            PatientDetails.AddCell(new Phrase("Patient :", FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            PatientDetails.AddCell(new Phrase(ds.Tables[0].Rows[0]["PATIENT_FIRST_NAME"].ToString() + " " + ds.Tables[0].Rows[0]["PATIENT_LAST_NAME"].ToString(), FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.NO_BORDER;
            PatientDetails.AddCell(new Phrase("D.O.B :", FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            PatientDetails.AddCell(new Phrase(ds.Tables[0].Rows[0]["DATE OF BIRTH"].ToString(), FontFactory.GetFont("Arial", 9)));
            //2nd row
            PatientDetails.DefaultCell.Border = Rectangle.NO_BORDER;
            PatientDetails.AddCell(new Phrase("D.O.A :", FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            PatientDetails.AddCell(new Phrase(ds.Tables[0].Rows[0]["ACCIDENT_DATE"].ToString(), FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.NO_BORDER;
            PatientDetails.AddCell(new Phrase("Insurance :", FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            PatientDetails.AddCell(new Phrase(ds.Tables[0].Rows[0]["INSURANCE NAME"].ToString(), FontFactory.GetFont("Arial", 9)));
            //3rd row
            PatientDetails.DefaultCell.Border = Rectangle.NO_BORDER;
            PatientDetails.AddCell(new Phrase("Claim# :", FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            PatientDetails.AddCell(new Phrase(ds.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString(), FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.NO_BORDER;
            PatientDetails.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
            PatientDetails.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
            PatientDetails.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height - 30, writer.DirectContent);
            Previous_Height = Previous_Height + PatientDetails.TotalHeight + 30;
            width = new float[] { 1f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f };
            PdfPTable Event_Details = new PdfPTable(width);

            Event_Details.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            Event_Details.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //add heading
            Event_Details.AddCell(new Phrase("S.No.", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("Treatment Day", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("Treatment Time", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("Intensity", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("Bias", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("Area", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("L.O.P Before", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("L.O.P After", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("Sign", FontFactory.GetFont("Arial", 8, Font.BOLD)));

            query = "exec [SP_PDFBILLS_MASTERBILLING_SYN]  @SZ_CASE_ID='" + sz_case_id.ToString() + "', @FLAG='GET_TREATMENT_INFORMATION'";
            da = new SqlDataAdapter(query, con);
            DataSet dsTreatment = new DataSet();
            da.Fill(dsTreatment);

            int i = 1;
            foreach (DataRow dr in dsTreatment.Tables[0].Rows)
            {
                Event_Details.AddCell(new Phrase((i++).ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[0].ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[1].ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[2].ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[3].ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[4].ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[5].ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[6].ToString(), FontFactory.GetFont("Arial", 8)));
                if (dr[7].ToString() != "" && dr[7].ToString() != "null" && File.Exists(dr[7].ToString()) == true)
                {
                    Event_Details.AddCell(iTextSharp.text.Image.GetInstance(dr[7].ToString()));
                }
                else
                {
                    Event_Details.AddCell(new Phrase("", FontFactory.GetFont("Arial", 8)));
                }
                if ((Event_Details.TotalHeight > doc.PageSize.Height - Previous_Height - doc.TopMargin - doc.BottomMargin - 50) && (ds.Tables[0].Rows.Count > i))
                {
                    Event_Details.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height - 30, writer.DirectContent);
                    doc.NewPage();
                    Previous_Height = 0;
                    Event_Details = new PdfPTable(width);
                    Event_Details.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
                    Event_Details.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //add heading
                    Event_Details.AddCell(new Phrase("S.No.", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("Treatment Day", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("Treatment Time", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("Intensity", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("Bias", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("Area", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("L.O.P Before", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("L.O.P After", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("Sign", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                }
            }
            Event_Details.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height - 30, writer.DirectContent);
            Previous_Height = Previous_Height + Event_Details.TotalHeight + 30;

            query = "exec SP_PDFBILLS_MASTERBILLING_SYN   @SZ_BILL_NUMBER= '" + pdfbills[0].ToString() + "', @FLAG='GET_PROCEDURE_GROUP'";
            da = new SqlDataAdapter(query, con);
            DataSet ProcedureGroup = new DataSet();
            da.Fill(ProcedureGroup);



            query = "exec SP_PDFBILLS_MASTERBILLING_SYN  @SZ_PROCEDURE_GROUP_ID='" + ProcedureGroup.Tables[0].Rows[0][0].ToString() + "',@SZ_CASE_ID='" + sz_case_id.ToString() + "', @FLAG='GET_DOCTOR_DIAGNOSIS_CODE'";
            da = new SqlDataAdapter(query, con);
            DataSet dsDiagnosys = new DataSet();
            da.Fill(dsDiagnosys);
            string Data = "";
            foreach (DataRow dr in dsDiagnosys.Tables[0].Rows)
            {
                if ((dr[0].ToString() != "") || (dr[1].ToString() != ""))
                {
                    if (dr[1].ToString() != "")
                        Data = Data + dr[0].ToString() + "-" + dr[1].ToString() + ",";
                    else
                        Data = Data = dr[0].ToString() + ",";
                }
            }
            width = new float[] { 2f, 4f };
            PdfPTable Diagnosis_Details = new PdfPTable(width);
            Diagnosis_Details.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            Diagnosis_Details.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            Diagnosis_Details.DefaultCell.VerticalAlignment = Element.ALIGN_BOTTOM;
            Diagnosis_Details.DefaultCell.Border = Rectangle.NO_BORDER;
            Diagnosis_Details.AddCell(new Phrase("TENTATIVE DIAGNOSIS :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            Diagnosis_Details.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            Diagnosis_Details.AddCell(new Phrase(Data, FontFactory.GetFont("Arial", 9)));
            if (Diagnosis_Details.TotalHeight > doc.PageSize.Height - Previous_Height - doc.TopMargin - doc.BottomMargin - 50)
            {
                doc.NewPage();
                Previous_Height = 0;
            }
            Diagnosis_Details.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height, writer.DirectContent);
            Previous_Height = Previous_Height + Diagnosis_Details.TotalHeight + 30;

            query = "exec SP_PDFBILLS_MASTERBILLING_SYN  @SZ_CASE_ID='" + sz_case_id.ToString() + "', @FLAG='GET_TREATMENT_PLAN'";
            da = new SqlDataAdapter(query, con);
            DataSet dsTreatmentPlan = new DataSet();
            da.Fill(dsTreatmentPlan);

            Data = "";
            foreach (DataRow dr in dsTreatmentPlan.Tables[0].Rows)
            {
                if (dr[0].ToString() != "")
                {
                    Data = Data + dr[0].ToString() + ",";

                }
            }
            PdfPTable Doctor_Notes = new PdfPTable(width);
            Doctor_Notes.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            Doctor_Notes.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            Doctor_Notes.DefaultCell.VerticalAlignment = Element.ALIGN_BOTTOM;
            Doctor_Notes.DefaultCell.Border = Rectangle.NO_BORDER;
            Doctor_Notes.AddCell(new Phrase("TREATMENT PLAN :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            Doctor_Notes.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            Doctor_Notes.AddCell(new Phrase(Data, FontFactory.GetFont("Arial", 9)));
            if (Doctor_Notes.TotalHeight > doc.PageSize.Height - Previous_Height - doc.TopMargin - doc.BottomMargin - 50)
            {
                doc.NewPage();
                Previous_Height = 0;
            }
            Doctor_Notes.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height - 30, writer.DirectContent);
            Previous_Height = Previous_Height + 30;
            Data = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr[0].ToString() != "")
                {
                    Data = Data + dr[0].ToString() + ",";

                }
            }
            PdfPTable Comments = new PdfPTable(width);
            Comments.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            Comments.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            Comments.DefaultCell.VerticalAlignment = Element.ALIGN_BOTTOM;
            Comments.DefaultCell.Border = Rectangle.NO_BORDER;
            Comments.AddCell(new Phrase("COMMENTS :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            Comments.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            Comments.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
            if (Comments.TotalHeight > doc.PageSize.Height - Previous_Height - doc.TopMargin - doc.BottomMargin - 50)
            {
                doc.NewPage();
                Previous_Height = 0;
            }
            Comments.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height - 30, writer.DirectContent);

            doc.Close();
            string sz_Case_id = ds.Tables[0].Rows[0]["CASEID"].ToString();

            query = "exec SP_INSERT_SYN_BILLING_REPORT_TO_DOCMANAGER @SZ_CASE_ID='" + sz_case_id + "', @SZ_FILE_NAME='" + filename + "', " +
                       "@SZ_FILE_PATH='" + filepath + "', @SZ_LOGIN_ID ='" + szUserName + "'";
            da = new SqlDataAdapter(query, con);
            ds = new DataSet();
            da.Fill(ds);
            destinationdir = ds.Tables[1].Rows[0][0].ToString() + destinationdir;

            string szImageId = ds.Tables[2].Rows[0][0].ToString();

            if (!Directory.Exists(destinationdir))
                Directory.CreateDirectory(destinationdir);
            System.IO.File.WriteAllBytes(destinationdir + filename, m.GetBuffer());

            InsertFUReport(pdfbills[0].ToString(), sz_Case_id, szSpecialityID, filename, filepath, szCompanyId, szUserName, szImageId);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    //For CH Speciality
    public void CHBillsPerPatient_iText(ArrayList pdfbills, string szUserName, string szUserId, string szCompanyId, string strCheckPath, string strUnCheckPath, string szSpecialityID)
    {
        SqlDataAdapter da;
        ArrayList arrList = new ArrayList();
        int i = 0;
        string pdfpath = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        string query;
        try
        {
            log.Debug(" Start CHBillsPerPatient_iText method.");
            for (int ii = 0; ii < pdfbills.Count; ii++)
            {
            BR1:
                query = "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + pdfbills[ii].ToString() + "', @FLAG='PDF'";
                da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                log.Debug(" SP_PDFBILLS_MASTERBILLING ececuted.");

                //create file
                string filename = "FUReport_" + pdfbills[ii] + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                string filepath = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')) + "/" +
                                  ds.Tables[0].Rows[0]["CASEID"].ToString() + "/No Fault File/Medicals/CH/FUReport/";
                string destinationdir = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')) + "\\" +
                                         ds.Tables[0].Rows[0]["CASEID"].ToString() + "\\No Fault File\\Medicals\\CH\\FUReport\\";
                pdfpath = ConfigurationManager.AppSettings["DocumentManagerURL"] + filepath + filename;
                log.Debug("pdfpath : " + pdfpath);

                query = "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + pdfbills[ii].ToString() + "', @FLAG='GET_EVENT_ID'";
                da = new SqlDataAdapter(query, con);
                DataSet dsEventId = new DataSet();
                da.Fill(dsEventId);
                log.Debug("executed SP_PDFBILLS_MASTERBILLING");

                string Companyname = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')).ToString();

                arrList.Add(pdfpath);
                log.Debug("Start writing pdf.");
                int j = 0;
                MemoryStream m = new MemoryStream();
                iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 36, 36, 20, 36);
                #region Create PDF
                //create pdf
                float[] widthbase ={ 4f };
                PdfPTable tblBase = new PdfPTable(widthbase);
                tblBase.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                //tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                tblBase.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tblBase.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tblBase.DefaultCell.Colspan = 1;
                PdfWriter writer = PdfWriter.GetInstance(document, m);
                document.Open();
                #region heading table
                float[] width1 ={ 4f };
                PdfPTable heading1 = new PdfPTable(width1);
                heading1.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                heading1.DefaultCell.Border = Rectangle.NO_BORDER;
                heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                heading1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                heading1.AddCell(new Phrase("CH NOTES", iTextSharp.text.FontFactory.GetFont("Arial", 12, Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblBase.AddCell(heading1);

                for (int Event = i; Event < dsEventId.Tables[0].Rows.Count; Event++)
                {
                    if ((tblBase.TotalHeight > document.PageSize.Height - document.TopMargin - document.BottomMargin - 50) || (Event >= 1))
                    {
                        tblBase.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin - heading1.TotalHeight - 1, writer.DirectContent);

                        tblBase = new PdfPTable(widthbase);
                        tblBase.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                        tblBase.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tblBase.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                        tblBase.DefaultCell.Colspan = 1;

                        //tblBase.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin - tblBase.TotalHeight - 1, writer.DirectContent);
                        document.NewPage();
                        heading1 = new PdfPTable(width1);
                        heading1.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                        heading1.DefaultCell.Border = Rectangle.NO_BORDER;
                        heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        heading1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                        heading1.AddCell(new Phrase("CH NOTES", iTextSharp.text.FontFactory.GetFont("Arial", 12, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        tblBase.AddCell(heading1);
                    }
                    DataSet ds1 = new DataSet();
                    string strEventID = dsEventId.Tables[0].Rows[Event][0].ToString();
                    ds1 = GetChairoView(strEventID);

                    float[] widthba ={ 4f };
                    PdfPTable tblba = new PdfPTable(widthba);
                    tblba.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                    float[] width ={ 1f, 2f, 1f, 2f, 1f, 2f };
                    PdfPTable heading = new PdfPTable(width);
                    heading.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    heading.DefaultCell.Border = Rectangle.NO_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;

                    heading.AddCell(new Phrase("Patient Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase("-" + " " + ds1.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    heading.AddCell(new Phrase("Doctor", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    heading.AddCell(new Phrase("-" + " " + ds1.Tables[0].Rows[0]["SZ_DOCTOR_NAME"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    heading.DefaultCell.Colspan = 1;

                    DateTime dtDate = Convert.ToDateTime(ds1.Tables[0].Rows[0]["DT_DATE"].ToString());
                    string dtDateFinal = dtDate.ToString("MM-dd-yyyy");
                    //heading.AddCell(new Phrase("Date", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase(""));
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    heading.AddCell(new Phrase("Date -" + " " + dtDateFinal, iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    heading.AddCell(new Phrase(""));
                    heading.AddCell(new Phrase(""));
                    heading.AddCell(new Phrase(""));
                    heading.AddCell(new Phrase(""));
                    heading.AddCell(new Phrase(""));
                    heading.AddCell(new Phrase(""));

                    tblba.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblba.AddCell(heading);
                    tblba.DefaultCell.Border = Rectangle.BOX;
                    //heading.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin, writer.DirectContent);
                #endregion



                    #region Pain
                    //Table Complaints

                    float[] widthSubject ={ 1f, 2f, 1f, 2f };
                    PdfPTable tblSubject = new PdfPTable(widthSubject);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblSubject.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblSubject.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblSubject.DefaultCell.Colspan = 4;

                    tblSubject.AddCell(new Phrase("Subjective : The patient reported the following information. ", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    //tblSubject.AddCell(new Phrase("The patient reported the following information.", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblSubject.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblSubject.DefaultCell.Colspan = 1;
                    if (ds1.Tables[0].Rows[0]["BT_NO_CHANGE_IN_MY_CONDITION"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblSubject.AddCell(cellObj);
                        tblSubject.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblSubject.AddCell(cellObj1);
                        tblSubject.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblSubject.AddCell(new Phrase("THERE IS NO CHANGE IN MY CONDITION", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_CHANGE_IN_MY_CONDITION"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblSubject.AddCell(cellObj);
                        tblSubject.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblSubject.AddCell(cellObj1);
                        tblSubject.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblSubject.AddCell(new Phrase("THERE IS CHANGE IN MY CONDITION", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_MY_CONDITION_IS_ABOUT_SAME"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblSubject.AddCell(cellObj);
                        tblSubject.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblSubject.AddCell(cellObj1);
                        tblSubject.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblSubject.AddCell(new Phrase("MY CONDITION IS ABOUT SAME", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    tblSubject.AddCell(new Phrase(""));
                    tblSubject.AddCell(new Phrase(""));

                    tblba.AddCell(tblSubject);

                    //Table Pain Grade
                    float[] widthPainGrades ={ 2f, 1f, 2f, 1f, 2f, 1f, 2f, 1f, 2f };
                    PdfPTable tblPainGrades = new PdfPTable(widthPainGrades);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblPainGrades.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    //tblPainGrades.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblPainGrades.DefaultCell.Colspan = 1;
                    tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblPainGrades.AddCell(new Phrase("Pain Grades", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    if (ds1.Tables[0].Rows[0]["BT_MILD"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj1);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblPainGrades.AddCell(new Phrase("MILD", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_MODERATE"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj1);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblPainGrades.AddCell(new Phrase("MODERATE", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_SEVERE"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj1);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblPainGrades.AddCell(new Phrase("SEVERE", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_VERY_SEVERE"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj1);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblPainGrades.AddCell(new Phrase("VERY SEVERE", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    tblba.AddCell(tblPainGrades);

                    float[] widthPain ={ 4f, 2f };
                    PdfPTable tblPain = new PdfPTable(widthPain);
                    tblPain.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblPain.DefaultCell.Colspan = 1;
                    tblPain.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    tblPain.AddCell(new Phrase("Treatment", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblPain.AddCell(new Phrase("Location", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblPain.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblPain.DefaultCell.Colspan = 1;
                    tblPain.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPain.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;

                    #region Treatment
                    float[] widthObj1 ={ 1f, 2f, 1f, 2f, 1f, 2f, 1f, 2f };
                    PdfPTable tblObj1 = new PdfPTable(widthObj1);
                    tblObj1.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    //tblObj1.TotalHeight = document.PageSize.Height - document.TopMargin - document.BottomMargin - 50;
                    //heading.DefaultCell.Border = Rectangle.NO_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblObj1.AddCell(new Phrase(""));
                    tblObj1.AddCell(new Phrase("Objective", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblObj1.AddCell(new Phrase(""));
                    tblObj1.AddCell(new Phrase("Objective", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblObj1.AddCell(new Phrase(""));
                    tblObj1.AddCell(new Phrase("Objective", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblObj1.AddCell(new Phrase(""));
                    tblObj1.AddCell(new Phrase("Objective", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_SPASM"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("S:SPASM", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_CERVICAL"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("CERVICAL", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_PELVIS"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("PELVIS", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));


                    if (ds1.Tables[0].Rows[0]["BT_QUAD"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("QUAD", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_EDEMA"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("E:EDEMA", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_THORACIC"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("THORACIC", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));


                    if (ds1.Tables[0].Rows[0]["BT_TRAPEZIUS"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("TRAPEZIUS", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_LEVATOR_SCAPULAE"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("LEVATOR SCAPULAE", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_TRIGGER_POINTS"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("TP:TRIGGER POINTS", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_LUMBAR"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("LUMBAR", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));


                    if (ds1.Tables[0].Rows[0]["BT_RHOMBOIDS"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("RHOMBOIDS", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_CERVICAL_PARASPINAL"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("CERVICAL PARASPINAL", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_FIXATION"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("FX:FIXATION", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_SACRUM"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("SACRUM", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    //3

                    if (ds1.Tables[0].Rows[0]["BT_PIRIFORMIS"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("PIRIFORMIS", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_THORACIC_PARASPINAL"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("THORACIC PARASPINAL", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_STERNOCLEIDOMASTOID"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("STERNOCLEIDOMASTOID(SCM)", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_QL"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("QL", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    tblObj1.AddCell(new Phrase(""));
                    tblObj1.AddCell(new Phrase(""));

                    if (ds1.Tables[0].Rows[0]["BT_LUMBAR_PARASPINAL"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        //cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        //cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblObj1.AddCell(cellObj1);
                        //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    tblObj1.AddCell(new Phrase("LUMBAR PARASPINAL", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    tblObj1.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblObj1.AddCell(new Phrase(""));
                    tblObj1.AddCell(new Phrase(""));
                    tblObj1.AddCell(new Phrase(""));
                    tblObj1.AddCell(new Phrase(""));
                    tblObj1.AddCell(new Phrase(""));
                    tblObj1.AddCell(new Phrase(""));
                    tblObj1.AddCell(new Phrase(""));
                    tblObj1.AddCell(new Phrase(""));

                    tblPain.AddCell(tblObj1);

                    #endregion

                    #region Location
                    float[] widthSec_Pain ={ 2f, 1f, 1f, 1f, 1f };
                    PdfPTable tblSec_Pain = new PdfPTable(widthSec_Pain);
                    tblSec_Pain.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    //heading.DefaultCell.Border = Rectangle.NO_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblSec_Pain.AddCell(new Phrase(""));
                    tblSec_Pain.AddCell(new Phrase("Right", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblSec_Pain.AddCell(new Phrase("Left", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblSec_Pain.AddCell(new Phrase("Both", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblSec_Pain.AddCell(new Phrase("Pain Level", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    //1
                    if (ds1.Tables[0].Rows[0]["BT_HEADACHE_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_HEADACHE_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_HEADACHE_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("HEADACHE", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_HEADACHE_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_HEADACHE_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_HEADACHE_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HEADACHE"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HEADACHE"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HEADACHE"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }

                    }

                    if (ds1.Tables[0].Rows[0]["BT_HAND_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_HAND_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_HAND_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("HAND", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_HAND_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_HAND_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_HAND_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HAND"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HAND"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HAND"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }

                    }

                    //2
                    if (ds1.Tables[0].Rows[0]["BT_NECK_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_NECK_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_NECK_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("NECK", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_NECK_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_NECK_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_NECK_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_NECK"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_NECK"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_NECK"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }

                    }

                    if (ds1.Tables[0].Rows[0]["BT_FINGERS_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_FINGERS_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_FINGERS_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("FINGERS", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_FINGERS_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_FINGERS_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_FINGERS_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FINGERS"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FINGERS"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FINGERS"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }


                    }

                    //3
                    if (ds1.Tables[0].Rows[0]["BT_MID_BACK_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_MID_BACK_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_MID_BACK_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("MID BACK", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_MID_BACK_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_MID_BACK_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_MID_BACK_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_MID_BACK"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_MID_BACK"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_MID_BACK"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }

                    }

                    if (ds1.Tables[0].Rows[0]["BT_HIP_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_HIP_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_HIP_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("HIP", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_HIP_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_HIP_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_HIP_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HIP"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HIP"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HIP"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }

                    }

                    //4
                    if (ds1.Tables[0].Rows[0]["BT_LOW_BACK_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_LOW_BACK_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_LOW_BACK_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("LOW BACK", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_LOW_BACK_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_LOW_BACK_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_LOW_BACK_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOW_BACK"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOW_BACK"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOW_BACK"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }

                    }

                    if (ds1.Tables[0].Rows[0]["BT_THIGH_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_THIGH_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_THIGH_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("THIGH", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_THIGH_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_THIGH_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_THIGH_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_THIGH"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_THIGH"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_THIGH"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }

                    }

                    //5
                    if (ds1.Tables[0].Rows[0]["BT_JAW_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_JAW_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_JAW_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("JAW", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_JAW_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_JAW_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_JAW_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_JAW"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_JAW"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_JAW"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }

                    }

                    if (ds1.Tables[0].Rows[0]["BT_KNEE_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_KNEE_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_KNEE_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("KNEE", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_KNEE_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_KNEE_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_KNEE_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_KNEE"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_KNEE"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_KNEE"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }

                    }

                    //6
                    if (ds1.Tables[0].Rows[0]["BT_SHOULDER_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_SHOULDER_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_SHOULDER_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("SHOULDER", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_SHOULDER_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_SHOULDER_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_SHOULDER_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_SHOULDER"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_SHOULDER"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_SHOULDER"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }

                    }

                    if (ds1.Tables[0].Rows[0]["BT_LOWER_LEG_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_LOWER_LEG_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_LOWER_LEG_BOTH"].ToString() == "1")
                    {

                        tblSec_Pain.AddCell(new Phrase("LOWER LEG", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_LOWER_LEG_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_LOWER_LEG_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_LOWER_LEG_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOWER_LEG"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOWER_LEG"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOWER_LEG"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }

                    }

                    //7
                    if (ds1.Tables[0].Rows[0]["BT_ELBOW_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_ELBOW_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_ELBOW_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("ELBOW", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_ELBOW_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_ELBOW_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_ELBOW_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_ELBOW"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_ELBOW"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_ELBOW"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }


                    }

                    if (ds1.Tables[0].Rows[0]["BT_FOOT_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_FOOT_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_FOOT_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("FOOT", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_FOOT_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_FOOT_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_FOOT_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;

                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FOOT"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FOOT"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FOOT"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }


                    }

                    //8
                    if (ds1.Tables[0].Rows[0]["BT_WRIST_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_WRIST_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_WRIST_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("WRIST", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_WRIST_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_WRIST_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_WRIST_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_WRIST"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_WRIST"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_WRIST"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }

                    }

                    if (ds1.Tables[0].Rows[0]["BT_TOES_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TOES_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TOES_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("TOES", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TOES_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TOES_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TOES_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            // tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_TOES"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_TOES"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_TOES"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }


                    }
                    tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblSec_Pain.AddCell(new Phrase(""));
                    tblSec_Pain.AddCell(new Phrase(""));
                    tblSec_Pain.AddCell(new Phrase(""));
                    tblSec_Pain.AddCell(new Phrase(""));
                    tblPain.AddCell(tblSec_Pain);

                    #endregion

                    tblba.AddCell(tblPain);
                    #endregion
                    //Table subjective add comments
                    float[] widthAddComments ={ 2f, 6f };
                    PdfPTable tblAddComments = new PdfPTable(widthAddComments);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblSubject.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    //tblAddComments.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblAddComments.DefaultCell.Colspan = 1;
                    tblAddComments.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblAddComments.AddCell(new Phrase("Additional Comments : ", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblAddComments.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    tblAddComments.AddCell(new Phrase(ds1.Tables[0].Rows[0]["SZ_SUBJECTIVE_ADDITIONAL_COMMENTS"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblAddComments.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblAddComments.AddCell(new Phrase(""));
                    tblAddComments.AddCell(new Phrase("Based on the report of the patient, additional information regarding this date of service may be found in the patients file.", iTextSharp.text.FontFactory.GetFont("Arial", 6, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblba.AddCell(tblAddComments);

                    //Table Range of motion
                    #region Range of motion
                    float[] widthRangeOfMotion ={ 4f, 4f };
                    PdfPTable tblRangeOfMotion = new PdfPTable(widthRangeOfMotion);
                    tblRangeOfMotion.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblRangeOfMotion.DefaultCell.Colspan = 1;
                    tblRangeOfMotion.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    tblRangeOfMotion.AddCell(new Phrase("Range of motion : ", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblRangeOfMotion.AddCell(new Phrase("Pain    R:Restriction", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblRangeOfMotion.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblRangeOfMotion.DefaultCell.Colspan = 2;
                    tblPain.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblRangeOfMotion.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;

                    float[] widthROM ={ 3f, 1f, 3f, 1f, 3f, 1f, 3f, 1f, 3f, 1f, 3f, 1f, 3f };
                    PdfPTable tblROM = new PdfPTable(widthROM);
                    tblROM.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    //tblObj1.TotalHeight = document.PageSize.Height - document.TopMargin - document.BottomMargin - 50;
                    //heading.DefaultCell.Border = Rectangle.NO_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    //1
                    tblROM.AddCell(new Phrase("CREVICAL :", iTextSharp.text.FontFactory.GetFont("Arial", 6, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    //2
                    if (ds1.Tables[0].Rows[0]["BT_CERVICAL_FLEX"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //3
                    tblROM.AddCell(new Phrase("FLEX", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //4
                    if (ds1.Tables[0].Rows[0]["BT_CERVICAL_EXT"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //5
                    tblROM.AddCell(new Phrase("EXT", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //6
                    if (ds1.Tables[0].Rows[0]["BT_CERVICAL_RT_ROT"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //7
                    tblROM.AddCell(new Phrase("RT.ROT", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //8
                    if (ds1.Tables[0].Rows[0]["BT_CERVICAL_LFT_ROT"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //9
                    tblROM.AddCell(new Phrase("LFT.ROT", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //10
                    if (ds1.Tables[0].Rows[0]["BT_CERVICAL_RT_LAT_FLEX"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //11
                    tblROM.AddCell(new Phrase("RT.LAT.FLEX", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //12
                    if (ds1.Tables[0].Rows[0]["BT_CERVICAL_LFT_LAT_FLEX"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //13
                    tblROM.AddCell(new Phrase("LFT.LAT.FLEX", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));


                    //1
                    tblROM.AddCell(new Phrase("THORACIC :", iTextSharp.text.FontFactory.GetFont("Arial", 6, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    //2
                    if (ds1.Tables[0].Rows[0]["BT_THORACIC_FLEX"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //3
                    tblROM.AddCell(new Phrase("FLEX", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //4
                    if (ds1.Tables[0].Rows[0]["BT_THORACIC_EXT"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //5
                    tblROM.AddCell(new Phrase("EXT", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //6
                    if (ds1.Tables[0].Rows[0]["BT_THORACIC_RT_ROT"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //7
                    tblROM.AddCell(new Phrase("RT.ROT", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //8
                    if (ds1.Tables[0].Rows[0]["BT_THORACIC_LFT_ROT"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //9
                    tblROM.AddCell(new Phrase("LFT.ROT", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //10
                    if (ds1.Tables[0].Rows[0]["BT_THORACIC_RT_LAT_FLEX"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //11
                    tblROM.AddCell(new Phrase("RT.LAT.FLEX", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //12
                    if (ds1.Tables[0].Rows[0]["BT_THORACIC_LFT_LAT_FLEX"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //13
                    tblROM.AddCell(new Phrase("LFT.LAT.FLEX", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    //1
                    tblROM.AddCell(new Phrase("LUMBAR :", iTextSharp.text.FontFactory.GetFont("Arial", 6, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    //2
                    if (ds1.Tables[0].Rows[0]["BT_LUMBAR_FLEX"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //3
                    tblROM.AddCell(new Phrase("FLEX", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //4
                    if (ds1.Tables[0].Rows[0]["BT_LUMBAR_EXT"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //5
                    tblROM.AddCell(new Phrase("EXT", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //6
                    if (ds1.Tables[0].Rows[0]["BT_LUMBAR_RT_ROT"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //7
                    tblROM.AddCell(new Phrase("RT.ROT", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //8
                    if (ds1.Tables[0].Rows[0]["BT_LUMBAR_LFT_ROT"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //9
                    tblROM.AddCell(new Phrase("LFT.ROT", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));

                    //10
                    if (ds1.Tables[0].Rows[0]["BT_LUMBAR_RT_LAT_FLEX"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //11
                    tblROM.AddCell(new Phrase("RT.LAT.FLEX", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //12
                    if (ds1.Tables[0].Rows[0]["BT_LUMBAR_LFT_LAT_FLEX"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblROM.AddCell(cellObj);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblROM.AddCell(cellObj1);
                        tblROM.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //13
                    tblROM.AddCell(new Phrase("LFT.LAT.FLEX", iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));


                    tblRangeOfMotion.AddCell(tblROM);
                    tblRangeOfMotion.AddCell(new Phrase(""));
                    tblba.AddCell(tblRangeOfMotion);
                    #endregion

                    //Table subjective add comments
                    float[] widthAddObjComments ={ 2f, 6f };
                    PdfPTable tblAddObjComments = new PdfPTable(widthAddObjComments);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblAddObjComments.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    //tblAddObjComments.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblAddObjComments.DefaultCell.Colspan = 1;
                    tblAddObjComments.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblAddObjComments.AddCell(new Phrase("Additional Comments : ", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblAddObjComments.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    tblAddObjComments.AddCell(new Phrase(ds1.Tables[0].Rows[0]["SZ_OBJECTIVE_ADDITIONAL_COMMENTS"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblAddObjComments.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblAddObjComments.AddCell(new Phrase(""));
                    tblAddObjComments.AddCell(new Phrase("Based on the report of the patient, additional information regarding this date of service may be found in the patients file.", iTextSharp.text.FontFactory.GetFont("Arial", 6, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblba.AddCell(tblAddObjComments);

                    float[] widthAssessment ={ 1f, 3f, 1f, 3f, 1f, 3f };
                    PdfPTable tblAssesment = new PdfPTable(widthAssessment);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblAssesment.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblAssesment.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblAssesment.DefaultCell.Colspan = 6;

                    tblAssesment.AddCell(new Phrase("Assessment : ", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblAssesment.DefaultCell.Colspan = 1;
                    tblAssesment.DefaultCell.Border = Rectangle.NO_BORDER;
                    if (ds1.Tables[0].Rows[0]["BT_ASSESSMENT_NO_CHANGE"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblAssesment.AddCell(cellObj);
                        tblAssesment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblAssesment.AddCell(cellObj1);
                        tblAssesment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblAssesment.AddCell(new Phrase("No Change", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_ASSESSMENT_IMPROVING"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblAssesment.AddCell(cellObj);
                        tblAssesment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblAssesment.AddCell(cellObj1);
                        tblAssesment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblAssesment.AddCell(new Phrase("Improving", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));


                    if (ds1.Tables[0].Rows[0]["BT_ASSESSMENT_FLAIR_UP"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblAssesment.AddCell(cellObj);
                        tblAssesment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblAssesment.AddCell(cellObj1);
                        tblAssesment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblAssesment.AddCell(new Phrase("Flair up", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_ASSESSMENT_AS_EXPECTED"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblAssesment.AddCell(cellObj);
                        tblAssesment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblAssesment.AddCell(cellObj1);
                        tblAssesment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblAssesment.AddCell(new Phrase("As Expected", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_ASSESSMENT_SLOWER_THAN_EXPECTED"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblAssesment.AddCell(cellObj);
                        tblAssesment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblAssesment.AddCell(cellObj1);
                        tblAssesment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblAssesment.AddCell(new Phrase("Slower than Expected", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    tblAssesment.AddCell(new Phrase(""));
                    tblAssesment.AddCell(new Phrase(""));

                    tblba.AddCell(tblAssesment);

                    float[] widthPCond ={ 1f, 3f, 1f, 3f, 1f, 3f, 1f, 3f };
                    PdfPTable tblPCond = new PdfPTable(widthPCond);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblPCond.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblPCond.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblPCond.DefaultCell.Colspan = 8;

                    tblPCond.AddCell(new Phrase("Due to nature of patient's condition they should", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblPCond.DefaultCell.Colspan = 1;
                    tblPCond.DefaultCell.Border = Rectangle.NO_BORDER;
                    if (ds1.Tables[0].Rows[0]["BT_STOP_ALL_ACTIVITES"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblPCond.AddCell(cellObj);
                        tblPCond.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPCond.AddCell(cellObj1);
                        tblPCond.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblPCond.AddCell(new Phrase("Stop all Activities", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_REDUCE_ALL_ACTIVITES"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblPCond.AddCell(cellObj);
                        tblPCond.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPCond.AddCell(cellObj1);
                        tblPCond.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblPCond.AddCell(new Phrase("Reduce all Activities", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_RESUME_LIGHT_ACTIVITES"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblPCond.AddCell(cellObj);
                        tblPCond.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPCond.AddCell(cellObj1);
                        tblPCond.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblPCond.AddCell(new Phrase("Resume Light Activities", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_RESUME_ALL_ACTIVITES"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblPCond.AddCell(cellObj);
                        tblPCond.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPCond.AddCell(cellObj1);
                        tblPCond.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblPCond.AddCell(new Phrase("Resume All Activities", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    tblba.AddCell(tblPCond);


                    float[] widthTre ={ 1f, 3f, 1f, 3f, 1f, 3f };
                    PdfPTable tblTre = new PdfPTable(widthTre);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblTre.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblTre.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblTre.DefaultCell.Colspan = 6;
                    tblTre.AddCell(new Phrase("Treatment : All treatment is being rendered based on subjective complaints and examination finding and is medically necessary.", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    //tblTre.AddCell(new Phrase("All treatment is being rendered based on subjective complaints and examination finding and is medically necessary.", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    tblTre.DefaultCell.Colspan = 1;

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_CERVICAL"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblTre.AddCell(cellObj);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblTre.AddCell(cellObj1);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblTre.AddCell(new Phrase("C:Cervical", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_THORACIC"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblTre.AddCell(cellObj);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblTre.AddCell(cellObj1);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblTre.AddCell(new Phrase("T:Throracic", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_LUMBAR"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblTre.AddCell(cellObj);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblTre.AddCell(cellObj1);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblTre.AddCell(new Phrase("L:Lumber", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_DORSOLUMBAR"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblTre.AddCell(cellObj);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblTre.AddCell(cellObj1);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblTre.AddCell(new Phrase("D/L:DorsoLumbar", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_SACROILIAC"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblTre.AddCell(cellObj);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblTre.AddCell(cellObj1);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblTre.AddCell(new Phrase("SI:Sacroiliac", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_TEMPROMANDIBULAR_JOINT"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblTre.AddCell(cellObj);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblTre.AddCell(cellObj1);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblTre.AddCell(new Phrase("TJ:Tempromandibular Joint", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_CERVICOTHORACIC"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblTre.AddCell(cellObj);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblTre.AddCell(cellObj1);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblTre.AddCell(new Phrase("Cervicothoracic", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_LUMBOPELVIC"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblTre.AddCell(cellObj);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblTre.AddCell(cellObj1);
                        tblTre.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    tblTre.AddCell(new Phrase("Lumbopelvic", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    tblTre.AddCell(new Phrase(""));
                    tblTre.AddCell(new Phrase(""));

                    tblba.AddCell(tblTre);


                    #region TableComplaints
                    //Table Complaints
                    DataSet dsComplaints = new DataSet();
                    dsComplaints = GET_COMPLIANTS_USING_EVENTID(strEventID);

                    float[] widthComplaints ={ 1f, 4f, 1f, 4f };
                    PdfPTable tblComplaints = new PdfPTable(widthComplaints);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblComplaints.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblComplaints.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblComplaints.DefaultCell.Colspan = 4;
                    tblComplaints.AddCell(new Phrase("Complaints :", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblComplaints.DefaultCell.Colspan = 1;

                    for (int Count = 0; Count < dsComplaints.Tables[0].Rows.Count; Count++)
                    {

                        string path = strCheckPath;
                        iTextSharp.text.Image Img1 = iTextSharp.text.Image.GetInstance(path);
                        Img1.ScaleAbsolute(10f, 10f);
                        //Img1.Height=5f;
                        PdfPCell cell = new PdfPCell(Img1);
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cell.FixedHeight = 12f;
                        tblComplaints.AddCell(cell);
                        tblComplaints.DefaultCell.Border = Rectangle.NO_BORDER;
                        tblComplaints.AddCell(new Phrase(dsComplaints.Tables[0].Rows[Count]["SZ_COMPLAINT"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                        tblComplaints.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        tblComplaints.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                    }
                    if (dsComplaints.Tables[0].Rows.Count % 2 != 0)
                    {
                        tblComplaints.AddCell(new Phrase(""));
                        tblComplaints.AddCell(new Phrase(""));
                    }
                    tblba.AddCell(tblComplaints);
                    #endregion

                    DataSet dsProc = new DataSet();
                    dsProc = GET_PROCEDURECODE_USING_EVENTID(strEventID);

                    float[] widthCode ={ 1f, 3f, 1f, 3f };
                    PdfPTable tblCode = new PdfPTable(widthCode);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblCode.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblCode.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblCode.DefaultCell.Colspan = 4;
                    tblCode.AddCell(new Phrase("Code :", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblCode.DefaultCell.Colspan = 1;

                    for (int Count = 0; Count < dsProc.Tables[0].Rows.Count; Count++)
                    {

                        string path = strCheckPath;
                        iTextSharp.text.Image Img1 = iTextSharp.text.Image.GetInstance(path);
                        Img1.ScaleAbsolute(10f, 10f);
                        //Img1.Height=5f;
                        PdfPCell cell = new PdfPCell(Img1);
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cell.FixedHeight = 12f;
                        tblCode.AddCell(cell);
                        tblCode.DefaultCell.Border = Rectangle.NO_BORDER;
                        tblCode.AddCell(new Phrase(dsProc.Tables[0].Rows[Count]["Column1"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                        tblCode.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        tblCode.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                    }
                    if (dsProc.Tables[0].Rows.Count % 2 != 0)
                    {
                        tblCode.AddCell(new Phrase(""));
                        tblCode.AddCell(new Phrase(""));
                    }
                    tblba.AddCell(tblCode);



                    //Table Sign
                    float[] widthSign ={ 1f, 2f, 1f, 2f };
                    PdfPTable tblSign = new PdfPTable(widthSign);
                    tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblSign.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblSign.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    //tblSign.DefaultCell.Border = Rectangle.BOX;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    //tblSign.DefaultCell.Colspan = 2;
                    tblSign.AddCell(new Phrase("Patient Sign", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    try
                    {
                        iTextSharp.text.Image providerim = iTextSharp.text.Image.GetInstance(ds1.Tables[0].Rows[0]["SZ_PATIENT_SIGN_PATH"].ToString().Replace("/", "\\"));
                        providerim.ScaleAbsoluteHeight(30);
                        providerim.ScaleAbsoluteWidth(50);
                        PdfPCell imagecell2 = new PdfPCell(providerim);
                        imagecell2.HorizontalAlignment = Element.ALIGN_LEFT;
                        imagecell2.Border = Rectangle.BOTTOM_BORDER;
                        imagecell2.PaddingBottom = 1f;
                        tblSign.AddCell(imagecell2);
                    }
                    catch (Exception ex)
                    {
                        tblSign.AddCell(new Phrase(""));
                    }
                    //tblSign.AddCell(new Phrase(""));

                    tblSign.AddCell(new Phrase("Doctor Sign", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    try
                    {
                        iTextSharp.text.Image providerim1 = iTextSharp.text.Image.GetInstance(ds1.Tables[0].Rows[0]["SZ_DOCTOR_SIGN_PATH"].ToString().Replace("/", "\\"));
                        providerim1.ScaleAbsoluteHeight(30);
                        providerim1.ScaleAbsoluteWidth(50);
                        PdfPCell imagecell = new PdfPCell(providerim1);
                        imagecell.HorizontalAlignment = Element.ALIGN_LEFT;
                        imagecell.Border = Rectangle.BOTTOM_BORDER;
                        imagecell.PaddingBottom = 1f;
                        tblSign.AddCell(imagecell);
                        tblSign.AddCell(new Phrase(""));
                    }
                    catch (Exception ex)
                    {
                        tblSign.AddCell(new Phrase(""));
                    }
                    //tblSign.DefaultCell.Colspan = 1;
                    tblba.AddCell(tblSign);
                    tblBase.AddCell(tblba);
                #endregion

                    float[] widthblank ={ 4f };
                    PdfPTable tblBlank = new PdfPTable(widthblank);
                    tblBlank.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblBlank.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblBlank.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tblBlank.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                    tblBlank.DefaultCell.Colspan = 1;
                    tblBlank.AddCell(new Phrase(""));
                    tblBlank.AddCell(new Phrase(""));
                    tblBase.AddCell(tblBlank);

                }
                tblBase.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin - heading1.TotalHeight - 1, writer.DirectContent);
                document.Close();

                string sz_Case_id = ds.Tables[0].Rows[0]["CASEID"].ToString();

                query = "exec SP_INSERT_CH_BILLING_REPORT_TO_DOCMANAGER @SZ_CASE_ID='" + sz_Case_id + "', @SZ_FILE_NAME='" + filename + "', " +
                "@SZ_FILE_PATH='" + filepath + "', @SZ_LOGIN_ID ='" + szUserName + "'";
                da = new SqlDataAdapter(query, con);
                ds = new DataSet();
                da.Fill(ds);
                log.Debug("executed SP_INSERT_CH_BILLING_REPORT_TO_DOCMANAGER ");

                destinationdir = ds.Tables[1].Rows[0][0].ToString() + destinationdir;

                string szImageId = ds.Tables[2].Rows[0][0].ToString();

                if (!Directory.Exists(destinationdir))
                    Directory.CreateDirectory(destinationdir);
                System.IO.File.WriteAllBytes(destinationdir + filename, m.GetBuffer());
                log.Debug("pdf generated successfully.");

                string ImageId = ds.Tables[2].Rows[0][0].ToString();

                query = "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + pdfbills[ii].ToString() + "', @FLAG='GET_EVENT_ID'";
                da = new SqlDataAdapter(query, con);
                DataSet dsEventId1 = new DataSet();
                da.Fill(dsEventId1);
                log.Debug("ececuted SP_PDFBILLS_MASTERBILLING ");

                if (dsEventId1.Tables.Count >= 0)
                {
                    for (int Event = 0; Event < dsEventId1.Tables[0].Rows.Count; Event++)
                    {
                        query = "exec SP_UPLOAD_REPORT_FOR_VISIT  @SZ_USER_NAME='" + szUserName + "',@SZ_FILE_NAME='" + filename + "',@SZ_COMPANY_ID='" + szCompanyId + "', @SZ_CASE_ID='" + sz_Case_id.ToString() + "'," +
                                " @SZ_USER_ID ='" + szUserId + "',@SZ_PROCEDURE_GROUP_ID ='" + szSpecialityID + "',@I_IMAGE_ID ='" + ImageId.ToString() + "',@SZ_EVENT_ID ='" + dsEventId1.Tables[0].Rows[Event][0].ToString() + "'";
                        da = new SqlDataAdapter(query, con);
                        ds = new DataSet();
                        da.Fill(ds);
                        log.Debug("executed SP_UPLOAD_REPORT_FOR_VISIT  ");
                    }
                }

                InsertFUReport(pdfbills[ii].ToString(), sz_Case_id, szSpecialityID, filename, filepath, szCompanyId, szUserName, szImageId);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }


    }

    //For PT Speciality
    public void PTTest(ArrayList pdfbills, string szUserName, string szUserId, string szCompanyId, string strCheckPath, string strUnCheckPath, string szSpecialityID)
    {
        SqlDataAdapter da;
        ArrayList arrList = new ArrayList();
        int i = 0;
        string pdfpath = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        string query;
        try
        {
            log.Debug("Start PTTest method.");
            for (int ii = 0; ii < pdfbills.Count; ii++)
            {
            BR1:
                query = "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + pdfbills[ii].ToString() + "', @FLAG='PDF'";
                da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                log.Debug("SP_PDFBILLS_MASTERBILLING :" + ds.Tables[0].Rows.Count);
                //create file
                string filename = "FUReport_" + pdfbills[ii] + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                string filepath = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')) + "/" +
                                  ds.Tables[0].Rows[0]["CASEID"].ToString() + "/No Fault File/Medicals/PT/FUReport/";
                string destinationdir = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')) + "\\" +
                                         ds.Tables[0].Rows[0]["CASEID"].ToString() + "\\No Fault File\\Medicals\\PT\\FUReport\\";
                pdfpath = ConfigurationManager.AppSettings["DocumentManagerURL"] + filepath + filename;
                log.Debug("pdfpath : " + pdfpath);

                query = "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + pdfbills[ii].ToString() + "', @FLAG='GET_EVENT_ID'";
                da = new SqlDataAdapter(query, con);
                DataSet dsEventId = new DataSet();
                da.Fill(dsEventId);
                log.Debug("SP_PDFBILLS_MASTERBILLING : " + dsEventId.Tables[0].Rows.Count);

                query = "exec SP_GET_PT_PATIENT_INFO @SZ_CASE_ID='" + ds.Tables[0].Rows[0]["CASEID"].ToString() + "'";
                da = new SqlDataAdapter(query, con);
                DataSet dsPatientInfo = new DataSet();
                da.Fill(dsPatientInfo);
                log.Debug("SP_GET_PT_PATIENT_INFO : " + dsPatientInfo.Tables[0].Rows.Count);

                string Companyname = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')).ToString();

                arrList.Add(pdfpath);

                log.Debug("Start writting pdf.");
                int j = 0;
                MemoryStream m = new MemoryStream();
                iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 36, 36, 20, 36);
                #region Create PDF
                //create pdf
                float[] widthbase ={ 4f };
                PdfPTable tblBase = new PdfPTable(widthbase);
                tblBase.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                //tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                tblBase.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tblBase.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tblBase.DefaultCell.Colspan = 1;
                PdfWriter writer = PdfWriter.GetInstance(document, m);
                document.Open();
                #region heading table
                float[] width1 ={ 4f };
                PdfPTable heading1 = new PdfPTable(width1);
                heading1.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                heading1.DefaultCell.Border = Rectangle.NO_BORDER;
                heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                heading1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                heading1.AddCell(new Phrase("PT NOTES", iTextSharp.text.FontFactory.GetFont("Arial", 12, Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblBase.AddCell(heading1);

                //page 1  - Available space=document.PageSize.Height - document.TopMargin - document.BottomMargin - 50
                ///Event 1- tblEVent1 = x
                /////Event 2 - tblEvent 2 =y
                //x+y>document.PageSize.Height - document.TopMargin - document.BottomMargin - 50
                for (int Event = i; Event < dsEventId.Tables[0].Rows.Count; Event++)
                {


                    if ((tblBase.TotalHeight > document.PageSize.Height - document.TopMargin - document.BottomMargin - 50) || (Event >= 1))
                    {
                        //tblBase = new PdfPTable(widthbase);
                        //tblBase.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                        //tblBase.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        //tblBase.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                        //tblBase.DefaultCell.Colspan = 1;
                        log.Debug("Write bill on new page.");
                        tblBase.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin - heading1.TotalHeight - 1, writer.DirectContent);

                        tblBase = new PdfPTable(widthbase);
                        tblBase.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                        tblBase.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tblBase.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                        tblBase.DefaultCell.Colspan = 1;

                        //tblBase.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin - tblBase.TotalHeight - 1, writer.DirectContent);
                        document.NewPage();
                        heading1 = new PdfPTable(width1);
                        heading1.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                        heading1.DefaultCell.Border = Rectangle.NO_BORDER;
                        heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        heading1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                        heading1.AddCell(new Phrase("PT NOTES", iTextSharp.text.FontFactory.GetFont("Arial", 12, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        tblBase.AddCell(heading1);
                    }
                    DataSet ds1 = new DataSet();
                    string strEventID = dsEventId.Tables[0].Rows[Event][0].ToString();
                    ds1 = GetPtview(strEventID);

                    float[] widthba ={ 4f };
                    PdfPTable tblba = new PdfPTable(widthba);
                    tblba.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                    float[] width ={ 2f, 2f, 2f, 2f };
                    PdfPTable heading = new PdfPTable(width);
                    heading.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    heading.DefaultCell.Border = Rectangle.NO_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;

                    heading.AddCell(new Phrase("Patient Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase("-" + " " + ds1.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    heading.AddCell(new Phrase("Case #", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase("-" + " " + ds1.Tables[0].Rows[0]["SZ_CASE_NO"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    heading.DefaultCell.Colspan = 1;

                    DateTime dtDOA = Convert.ToDateTime(dsPatientInfo.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString());
                    string dtDOAFinal = dtDOA.ToString("MM-dd-yyyy");
                    heading.AddCell(new Phrase("Date of Accident", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase("-" + " " + dtDOAFinal, iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    heading.AddCell(new Phrase("Insurance Company", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase("-" + " " + dsPatientInfo.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    heading.DefaultCell.Colspan = 1;

                    heading.AddCell(new Phrase("Claim Number", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase("-" + " " + dsPatientInfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    DateTime dtDate = Convert.ToDateTime(ds1.Tables[0].Rows[0]["DT_DATE"].ToString());
                    string dtDateFinal = dtDate.ToString("MM-dd-yyyy");
                    heading.AddCell(new Phrase("Date", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase("-" + " " + dtDateFinal, iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    heading.DefaultCell.Colspan = 1;

                    heading.AddCell(new Phrase("Patient Complaints", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase("-" + " " + ds1.Tables[0].Rows[0]["SZ_COMPLAINTS"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    //heading.AddCell(new Phrase("Precautions", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    //heading.AddCell(new Phrase("-" + " " + ds1.Tables[0].Rows[0]["SZ_PRECAUTIONS"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase(""));
                    heading.AddCell(new Phrase(""));
                    heading.DefaultCell.Colspan = 1;

                    tblba.AddCell(heading);
                    //heading.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin, writer.DirectContent);
                #endregion

                    #region TableComplaints
                    //Table Complaints
                    DataSet dsComplaints = new DataSet();
                    dsComplaints = GET_COMPLIANTS_USING_EVENTID(strEventID);
                    log.Debug("End of GET_COMPLIANTS_USING_EVENTID method.");

                    float[] widthComplaints ={ 1f, 3f, 1f, 3f };
                    PdfPTable tblComplaints = new PdfPTable(widthComplaints);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblComplaints.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblComplaints.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblComplaints.DefaultCell.Colspan = 4;
                    tblComplaints.AddCell(new Phrase("Complaints", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    // heading.AddCell(new Phrase(companyaddress, iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //tblCode.DefaultCell.Border = Rectangle.NO_BORDER;
                    //tblCode.AddCell(new Phrase(""));
                    //tblCode.AddCell(new Phrase(""));
                    //tblCode.AddCell(new Phrase(""));
                    tblComplaints.DefaultCell.Colspan = 1;

                    for (int Count = 0; Count < dsComplaints.Tables[0].Rows.Count; Count++)
                    {

                        string path = strCheckPath;
                        iTextSharp.text.Image Img1 = iTextSharp.text.Image.GetInstance(path);
                        Img1.ScaleAbsolute(10f, 10f);
                        //Img1.Height=5f;
                        PdfPCell cell = new PdfPCell(Img1);
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cell.FixedHeight = 12f;
                        tblComplaints.AddCell(cell);
                        tblComplaints.DefaultCell.Border = Rectangle.NO_BORDER;
                        tblComplaints.AddCell(new Phrase(dsComplaints.Tables[0].Rows[Count]["SZ_COMPLAINT"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                        tblComplaints.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        tblComplaints.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                    }
                    if (dsComplaints.Tables[0].Rows.Count % 2 != 0)
                    {
                        tblComplaints.AddCell(new Phrase(""));
                        tblComplaints.AddCell(new Phrase(""));
                    }
                    tblba.AddCell(tblComplaints);
                    #endregion

                    #region Pain
                    //Table Complaints

                    float[] widthPainGrades ={ 2f, 1f, 2f, 1f, 2f, 1f, 2f, 1f, 2f };
                    PdfPTable tblPainGrades = new PdfPTable(widthPainGrades);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblPainGrades.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    //tblPainGrades.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    //tblPainGrades.DefaultCell.Colspan = 9;
                    //tblPainGrades.AddCell(new Phrase("Pain", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    // heading.AddCell(new Phrase(companyaddress, iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //tblCode.DefaultCell.Border = Rectangle.NO_BORDER;
                    //tblCode.AddCell(new Phrase(""));
                    //tblCode.AddCell(new Phrase(""));
                    //tblCode.AddCell(new Phrase(""));
                    tblPainGrades.DefaultCell.Colspan = 1;
                    tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblPainGrades.AddCell(new Phrase("Pain Grades", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    if (ds1.Tables[0].Rows[0]["BT_MILD"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj1);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblPainGrades.AddCell(new Phrase("MILD", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_MODERATE"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj1);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblPainGrades.AddCell(new Phrase("MODERATE", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_SEVERE"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj1);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblPainGrades.AddCell(new Phrase("SEVERE", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_VERY_SEVERE"].ToString() == "1")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPainGrades.AddCell(cellObj1);
                        tblPainGrades.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblPainGrades.AddCell(new Phrase("VERY SEVERE", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    tblba.AddCell(tblPainGrades);

                    float[] widthPain ={ 2f, 2f };
                    PdfPTable tblPain = new PdfPTable(widthPain);
                    tblPain.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblPain.DefaultCell.Colspan = 1;
                    tblPain.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    tblPain.AddCell(new Phrase("Treatment", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblPain.AddCell(new Phrase("Location", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblPain.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblPain.DefaultCell.Colspan = 1;
                    tblPain.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPain.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;

                    #region Treatment

                    float[] widthSec_Treatment ={ 3f, 1f, 1f, 1f };
                    PdfPTable tblSec_Treatment = new PdfPTable(widthSec_Treatment);
                    tblSec_Treatment.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    //heading.DefaultCell.Border = Rectangle.NO_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblSec_Treatment.AddCell(new Phrase(""));
                    tblSec_Treatment.AddCell(new Phrase("Right", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblSec_Treatment.AddCell(new Phrase("Left", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblSec_Treatment.AddCell(new Phrase("Both", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    //1
                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("HEADACHE", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_HEADACHE_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_HAND_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_HAND_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_HAND_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("HAND", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_HAND_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_HAND_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_HAND_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    //2
                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_NECK_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_NECK_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_NECK_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("NECK", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_NECK_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_NECK_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_NECK_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("FINGERS", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_FINGERS_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    //3
                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("MID BACK", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_MID_BACK_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_HIP_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_HIP_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_HIP_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("HIP", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_HIP_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_HIP_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_HIP_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    //4
                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("LOW BACK", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_LOW_BACK_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_THIGH_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_THIGH_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_THIGH_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("THIGH", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_THIGH_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_THIGH_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_THIGH_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    //5
                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_JAW_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_JAW_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_JAW_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("JAW", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_JAW_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_JAW_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_JAW_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_KNEE_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_KNEE_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_KNEE_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("KNEE", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_KNEE_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_KNEE_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_KNEE_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    //6
                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("SHOULDER", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_SHOULDER_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_BOTH"].ToString() == "1")
                    {

                        tblSec_Treatment.AddCell(new Phrase("LOWER LEG", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_LOWER_LEG_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    //7
                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("ELBOW", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_ELBOW_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_FOOT_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_FOOT_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_FOOT_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("FOOT", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_FOOT_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_FOOT_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_FOOT_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    //8
                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_WRIST_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_WRIST_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_WRIST_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("WRIST", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_WRIST_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_WRIST_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_WRIST_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_TREATMENT_TOES_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_TOES_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TREATMENT_TOES_BOTH"].ToString() == "1")
                    {
                        tblSec_Treatment.AddCell(new Phrase("TOES", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_TOES_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_TOES_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TREATMENT_TOES_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj);
                            //tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Treatment.AddCell(cellObj1);
                            // tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                    }
                    tblSec_Treatment.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblSec_Treatment.AddCell(new Phrase(""));
                    tblSec_Treatment.AddCell(new Phrase(""));
                    tblSec_Treatment.AddCell(new Phrase(""));
                    tblSec_Treatment.AddCell(new Phrase(""));
                    tblPain.AddCell(tblSec_Treatment);

                    #endregion

                    #region Location
                    float[] widthSec_Pain ={ 2f, 1f, 1f, 1f, 1f };
                    PdfPTable tblSec_Pain = new PdfPTable(widthSec_Pain);
                    tblSec_Pain.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    //heading.DefaultCell.Border = Rectangle.NO_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblSec_Pain.AddCell(new Phrase(""));
                    tblSec_Pain.AddCell(new Phrase("Right", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblSec_Pain.AddCell(new Phrase("Left", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblSec_Pain.AddCell(new Phrase("Both", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblSec_Pain.AddCell(new Phrase("Pain Level", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    //1
                    if (ds1.Tables[0].Rows[0]["BT_HEADACHE_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_HEADACHE_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_HEADACHE_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("HEADACHE", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_HEADACHE_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_HEADACHE_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_HEADACHE_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HEADACHE"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HEADACHE"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HEADACHE"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));
                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_HAND_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_HAND_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_HAND_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("HAND", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_HAND_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_HAND_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_HAND_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HAND"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HAND"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HAND"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    //2
                    if (ds1.Tables[0].Rows[0]["BT_NECK_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_NECK_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_NECK_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("NECK", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_NECK_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_NECK_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_NECK_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_NECK"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_NECK"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_NECK"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_FINGERS_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_FINGERS_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_FINGERS_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("FINGERS", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_FINGERS_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_FINGERS_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_FINGERS_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FINGERS"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FINGERS"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FINGERS"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    //3
                    if (ds1.Tables[0].Rows[0]["BT_MID_BACK_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_MID_BACK_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_MID_BACK_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("MID BACK", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_MID_BACK_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_MID_BACK_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_MID_BACK_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_MID_BACK"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_MID_BACK"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_MID_BACK"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_HIP_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_HIP_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_HIP_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("HIP", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_HIP_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_HIP_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_HIP_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HIP"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HIP"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_HIP"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    //4
                    if (ds1.Tables[0].Rows[0]["BT_LOW_BACK_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_LOW_BACK_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_LOW_BACK_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("LOW BACK", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_LOW_BACK_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_LOW_BACK_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_LOW_BACK_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOW_BACK"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOW_BACK"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOW_BACK"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_THIGH_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_THIGH_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_THIGH_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("THIGH", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_THIGH_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_THIGH_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_THIGH_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_THIGH"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_THIGH"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_THIGH"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    //5
                    if (ds1.Tables[0].Rows[0]["BT_JAW_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_JAW_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_JAW_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("JAW", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_JAW_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_JAW_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_JAW_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_JAW"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_JAW"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_JAW"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_KNEE_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_KNEE_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_KNEE_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("KNEE", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_KNEE_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_KNEE_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_KNEE_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_KNEE"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_KNEE"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_KNEE"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    //6
                    if (ds1.Tables[0].Rows[0]["BT_SHOULDER_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_SHOULDER_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_SHOULDER_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("SHOULDER", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_SHOULDER_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_SHOULDER_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_SHOULDER_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_SHOULDER"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_SHOULDER"] == "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_SHOULDER"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_LOWER_LEG_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_LOWER_LEG_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_LOWER_LEG_BOTH"].ToString() == "1")
                    {

                        tblSec_Pain.AddCell(new Phrase("LOWER LEG", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_LOWER_LEG_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_LOWER_LEG_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_LOWER_LEG_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOWER_LEG"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOWER_LEG"] == "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_LOWER_LEG"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    //7
                    if (ds1.Tables[0].Rows[0]["BT_ELBOW_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_ELBOW_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_ELBOW_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("ELBOW", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_ELBOW_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_ELBOW_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_ELBOW_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_ELBOW"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_ELBOW"] == "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_ELBOW"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_FOOT_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_FOOT_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_FOOT_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("FOOT", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_FOOT_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_FOOT_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_FOOT_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FOOT"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FOOT"] == "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_FOOT"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }


                    //8
                    if (ds1.Tables[0].Rows[0]["BT_WRIST_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_WRIST_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_WRIST_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("WRIST", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_WRIST_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_WRIST_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_WRIST_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_WRIST"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_WRIST"] != "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_WRIST"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }

                    if (ds1.Tables[0].Rows[0]["BT_TOES_RIGHT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TOES_LEFT"].ToString() == "1" || ds1.Tables[0].Rows[0]["BT_TOES_BOTH"].ToString() == "1")
                    {
                        tblSec_Pain.AddCell(new Phrase("TOES", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        if (ds1.Tables[0].Rows[0]["BT_TOES_RIGHT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TOES_LEFT"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }

                        if (ds1.Tables[0].Rows[0]["BT_TOES_BOTH"].ToString() == "1")
                        {
                            string pathObj = strCheckPath;
                            iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                            ImgObj.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj = new PdfPCell(ImgObj);
                            //cellObj.Border = Rectangle.NO_BORDER;
                            cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj);
                            //tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        else
                        {
                            string pathObj1 = strUnCheckPath;
                            iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                            ImgObj1.ScaleAbsolute(10f, 10f);
                            PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                            //cellObj1.Border = Rectangle.NO_BORDER;
                            cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellObj1.FixedHeight = 12f;
                            tblSec_Pain.AddCell(cellObj1);
                            // tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                        }
                        if (ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_TOES"] != null || ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_TOES"] == "")
                        {
                            tblSec_Pain.AddCell((new Phrase(" " + ds1.Tables[0].Rows[0]["SZ_PAIN_LEVEL_TOES"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK))));
                        }
                        else
                        {
                            tblSec_Pain.AddCell((new Phrase("")));

                        }
                    }
                    tblSec_Pain.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblSec_Pain.AddCell(new Phrase(""));
                    tblSec_Pain.AddCell(new Phrase(""));
                    tblSec_Pain.AddCell(new Phrase(""));
                    tblSec_Pain.AddCell(new Phrase(""));
                    tblPain.AddCell(tblSec_Pain);

                    #endregion

                    tblba.AddCell(tblPain);
                    #endregion

                    #region TableTreatment
                    float[] widthtreat ={ 4f, 4f, 4f };
                    PdfPTable tblTreatment = new PdfPTable(widthtreat);
                    tblTreatment.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    //heading.DefaultCell.Border = Rectangle.NO_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblTreatment.DefaultCell.Colspan = 3;
                    tblTreatment.AddCell(new Phrase("Treatment", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    // heading.AddCell(new Phrase(companyaddress, iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //tblTreatment.AddCell(new Phrase(""));
                    //tblTreatment.AddCell(new Phrase(""));
                    tblTreatment.DefaultCell.Colspan = 1;
                    tblTreatment.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblTreatment.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;

                    //Table for Objective
                    float[] widthObj ={ 1f, 4f };
                    PdfPTable tblObj = new PdfPTable(widthObj);
                    tblObj.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblObj.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblObj.DefaultCell.Colspan = 2;
                    tblObj.AddCell(new Phrase("Objective", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    // heading.AddCell(new Phrase(companyaddress, iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    tblObj.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblObj.AddCell(new Phrase(""));
                    tblObj.DefaultCell.Colspan = 1;
                    tblObj.DefaultCell.Border = Rectangle.NO_BORDER;

                    if (ds1.Tables[0].Rows[0]["BT_OBJECTIVE1"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj.FixedHeight = 12f;
                        tblObj.AddCell(cellObj);
                        tblObj.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj1.FixedHeight = 12f;
                        tblObj.AddCell(cellObj1);
                        tblObj.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblObj.AddCell(new Phrase("Patient states condition is the same", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_OBJECTIVE2"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj.FixedHeight = 12f;
                        tblObj.AddCell(cellObj);
                        tblObj.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj1.FixedHeight = 12f;
                        tblObj.AddCell(cellObj1);
                        tblObj.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblObj.AddCell(new Phrase("Patient states little improvement in condition", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_OBJECTIVE3"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj.FixedHeight = 12f;
                        tblObj.AddCell(cellObj);
                        tblObj.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj1.FixedHeight = 12f;
                        tblObj.AddCell(cellObj1);
                        tblObj.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    //tblObj.AddCell(new Phrase(""));
                    tblObj.AddCell(new Phrase("Patient states much improvement in condition", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblObj.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblObj.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                    tblTreatment.AddCell(tblObj);

                    //Table Assessment
                    float[] widthAssessment ={ 1f, 4f };
                    PdfPTable tblAssessment = new PdfPTable(widthAssessment);

                    tblAssessment.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblAssessment.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblAssessment.DefaultCell.Colspan = 2;
                    tblAssessment.AddCell(new Phrase("Assessment", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    // heading.AddCell(new Phrase(companyaddress, iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    tblAssessment.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblAssessment.AddCell(new Phrase(""));
                    tblAssessment.DefaultCell.Colspan = 1;
                    tblAssessment.DefaultCell.Border = Rectangle.NO_BORDER;

                    if (ds1.Tables[0].Rows[0]["BT_PATIENT_TOLERATED"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj.FixedHeight = 12f;
                        tblAssessment.AddCell(cellObj);
                        tblAssessment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj1.FixedHeight = 12f;
                        tblAssessment.AddCell(cellObj1);
                        tblAssessment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //tblAssessment.AddCell(new Phrase(""));
                    tblAssessment.AddCell(new Phrase("Patient tolerated maximum level", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_ASS_OTHER_COMMENTS"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj.FixedHeight = 12f;
                        tblAssessment.AddCell(cellObj);
                        tblAssessment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj1.FixedHeight = 12f;
                        tblAssessment.AddCell(cellObj1);
                        tblAssessment.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //tblAssessment.AddCell(new Phrase(""));
                    tblAssessment.AddCell(new Phrase("Other Comments", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblAssessment.AddCell(new Phrase(""));
                    tblAssessment.AddCell(new Phrase(ds1.Tables[0].Rows[0]["SZ_OTHER_COMMENTS"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblAssessment.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblAssessment.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                    tblTreatment.AddCell(tblAssessment);

                    //Table Plan
                    float[] widthPlan ={ 1f, 4f };
                    PdfPTable tblPlan = new PdfPTable(widthPlan);

                    tblPlan.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblPlan.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblPlan.DefaultCell.Colspan = 2;
                    tblPlan.AddCell(new Phrase("Plan", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    // heading.AddCell(new Phrase(companyaddress, iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    tblPlan.DefaultCell.Border = Rectangle.NO_BORDER;


                    tblPlan.AddCell(new Phrase(""));
                    tblPlan.DefaultCell.Colspan = 1;
                    tblPlan.DefaultCell.Border = Rectangle.NO_BORDER;

                    if (ds1.Tables[0].Rows[0]["BT_COTINUE_THERAPY"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj.FixedHeight = 12f;
                        tblPlan.AddCell(cellObj);
                        tblPlan.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj1.FixedHeight = 12f;
                        tblPlan.AddCell(cellObj1);
                        tblPlan.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //tblPlan.AddCell(new Phrase(""));
                    tblPlan.AddCell(new Phrase("Continue/Progress theropy as prescibed", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    if (ds1.Tables[0].Rows[0]["BT_PLAN_OTHER_COMMENTS"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj.VerticalAlignment = Element.ALIGN_TOP;
                        cellObj.FixedHeight = 12f;
                        tblPlan.AddCell(cellObj);
                        tblPlan.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cellObj1.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cellObj1.FixedHeight = 12f;
                        tblPlan.AddCell(cellObj1);
                        tblPlan.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    //tblPlan.AddCell(new Phrase(""));
                    tblPlan.AddCell(new Phrase("Other Comments", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblPlan.AddCell(new Phrase(""));
                    tblPlan.AddCell(new Phrase(ds1.Tables[0].Rows[0]["SZ_BT_PLAN_OHER_COMMENTS"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblPlan.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPlan.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                    tblTreatment.AddCell(tblPlan);
                    tblba.AddCell(tblTreatment);

                    //Table Code
                    DataSet dsProc = new DataSet();
                    dsProc = GET_PROCEDURECODE_USING_EVENTID(strEventID);

                    float[] widthCode ={ 1f, 3f, 1f, 3f };
                    PdfPTable tblCode = new PdfPTable(widthCode);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblCode.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblCode.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblCode.DefaultCell.Colspan = 4;
                    tblCode.AddCell(new Phrase("Code", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    // heading.AddCell(new Phrase(companyaddress, iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //tblCode.DefaultCell.Border = Rectangle.NO_BORDER;
                    //tblCode.AddCell(new Phrase(""));
                    //tblCode.AddCell(new Phrase(""));
                    //tblCode.AddCell(new Phrase(""));
                    tblCode.DefaultCell.Colspan = 1;

                    for (int Count = 0; Count < dsProc.Tables[0].Rows.Count; Count++)
                    {

                        string path = strCheckPath;
                        iTextSharp.text.Image Img1 = iTextSharp.text.Image.GetInstance(path);
                        Img1.ScaleAbsolute(10f, 10f);
                        //Img1.Height=5f;
                        PdfPCell cell = new PdfPCell(Img1);
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cell.FixedHeight = 12f;
                        tblCode.AddCell(cell);
                        tblCode.DefaultCell.Border = Rectangle.NO_BORDER;
                        tblCode.AddCell(new Phrase(dsProc.Tables[0].Rows[Count]["Column1"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                        tblCode.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        tblCode.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                    }
                    if (dsProc.Tables[0].Rows.Count % 2 != 0)
                    {
                        tblCode.AddCell(new Phrase(""));
                        tblCode.AddCell(new Phrase(""));
                    }
                    tblba.AddCell(tblCode);

                    //Table Sign
                    float[] widthSign ={ 1f, 2f, 1f, 2f };
                    PdfPTable tblSign = new PdfPTable(widthSign);
                    tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblSign.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblSign.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    //tblSign.DefaultCell.Border = Rectangle.BOX;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    //tblSign.DefaultCell.Colspan = 2;
                    tblSign.AddCell(new Phrase("Patient Sign", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    try
                    {
                        iTextSharp.text.Image providerim = iTextSharp.text.Image.GetInstance(ds1.Tables[0].Rows[0]["SZ_PATIENT_SIGN_PATH"].ToString().Replace("/", "\\"));
                        providerim.ScaleAbsoluteHeight(30);
                        providerim.ScaleAbsoluteWidth(50);
                        PdfPCell imagecell2 = new PdfPCell(providerim);
                        imagecell2.HorizontalAlignment = Element.ALIGN_LEFT;
                        imagecell2.Border = Rectangle.BOTTOM_BORDER;
                        imagecell2.PaddingBottom = 1f;
                        tblSign.AddCell(imagecell2);
                    }
                    catch (Exception ex)
                    {
                        tblSign.AddCell(new Phrase(""));
                    }
                    //tblSign.AddCell(new Phrase(""));

                    tblSign.AddCell(new Phrase("Doctor Sign", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    try
                    {
                        iTextSharp.text.Image providerim1 = iTextSharp.text.Image.GetInstance(ds1.Tables[0].Rows[0]["SZ_DOCTOR_SIGN_PATH"].ToString().Replace("/", "\\"));
                        providerim1.ScaleAbsoluteHeight(30);
                        providerim1.ScaleAbsoluteWidth(50);
                        PdfPCell imagecell = new PdfPCell(providerim1);
                        imagecell.HorizontalAlignment = Element.ALIGN_LEFT;
                        imagecell.Border = Rectangle.BOTTOM_BORDER;
                        imagecell.PaddingBottom = 1f;
                        tblSign.AddCell(imagecell);
                        tblSign.AddCell(new Phrase(""));
                    }
                    catch (Exception ex)
                    {
                        tblSign.AddCell(new Phrase(""));
                    }
                    //tblSign.DefaultCell.Colspan = 1;
                    tblba.AddCell(tblSign);
                    tblBase.AddCell(tblba);
                    #endregion
                #endregion
                    float[] widthblank ={ 4f };
                    PdfPTable tblBlank = new PdfPTable(widthblank);
                    tblBlank.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblBlank.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblBlank.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tblBlank.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                    tblBlank.DefaultCell.Colspan = 1;
                    tblBlank.AddCell(new Phrase(""));
                    tblBlank.AddCell(new Phrase(""));
                    tblBase.AddCell(tblBlank);

                }
                tblBase.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin - heading1.TotalHeight - 1, writer.DirectContent);
                document.Close();
                log.Debug("document close.");
                string sz_Case_id = ds.Tables[0].Rows[0]["CASEID"].ToString();



                query = "exec SP_INSERT_PT_BILLING_REPORT_TO_DOCMANAGER @SZ_CASE_ID='" + sz_Case_id + "', @SZ_FILE_NAME='" + filename + "', " +
                "@SZ_FILE_PATH='" + filepath + "', @SZ_LOGIN_ID ='" + szUserName + "'";
                da = new SqlDataAdapter(query, con);
                ds = new DataSet();
                da.Fill(ds);
                log.Debug("SP_INSERT_PT_BILLING_REPORT_TO_DOCMANAGER : " + ds.Tables[0].Rows.Count);

                log.Debug("Start creating directory if directory not exist.");
                destinationdir = ds.Tables[1].Rows[0][0].ToString() + destinationdir;
                if (!Directory.Exists(destinationdir))
                    Directory.CreateDirectory(destinationdir);
                System.IO.File.WriteAllBytes(destinationdir + filename, m.GetBuffer());
                log.Debug("creating directory successful.");
                log.Debug("Writting PT pdf is successful.");

                string ImageId = ds.Tables[2].Rows[0][0].ToString();

                query = "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + pdfbills[ii].ToString() + "', @FLAG='GET_EVENT_ID'";
                da = new SqlDataAdapter(query, con);
                DataSet dsEventId1 = new DataSet();
                da.Fill(dsEventId1);
                log.Debug("SP_PDFBILLS_MASTERBILLING : " + dsEventId1.Tables[0].Rows.Count);

                if (dsEventId1.Tables.Count >= 0)
                {
                    for (int Event = 0; Event < dsEventId1.Tables[0].Rows.Count; Event++)
                    {
                        query = "exec SP_UPLOAD_REPORT_FOR_VISIT  @SZ_USER_NAME='" + szUserName + "',@SZ_FILE_NAME='" + filename + "',@SZ_COMPANY_ID='" + szCompanyId + "', @SZ_CASE_ID='" + sz_Case_id.ToString() + "'," +
                                " @SZ_USER_ID ='" + szUserId + "',@SZ_PROCEDURE_GROUP_ID ='" + szSpecialityID + "',@I_IMAGE_ID ='" + ImageId.ToString() + "',@SZ_EVENT_ID ='" + dsEventId1.Tables[0].Rows[Event][0].ToString() + "'";
                        da = new SqlDataAdapter(query, con);
                        ds = new DataSet();
                        da.Fill(ds);
                        log.Debug("SP_UPLOAD_REPORT_FOR_VISIT executed.");
                    }
                }

                InsertFUReport(pdfbills[ii].ToString(), sz_Case_id, szSpecialityID, filename, filepath, szCompanyId, szUserName, ImageId);
            }


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    //For WBSpeciality
    public void WBBillsPerPatient(ArrayList pdfbills, string szUserName, string szUserId, string szCompanyId, string strCheckPath, string strUnCheckPath, string szSpecialityID)
    {
        SqlDataAdapter da;
        ArrayList arrList = new ArrayList();
        string pdfpath = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        string query;
        try
        {
            for (int ii = 0; ii < pdfbills.Count; ii++)
            {
                query = "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + pdfbills[ii].ToString() + "', @FLAG='PDF'";
                da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //create file
                string filename = "FUReport_" + pdfbills[ii] + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                string filepath = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')) + "/" +
                                  ds.Tables[0].Rows[0]["CASEID"].ToString() + "/No Fault File/Medicals/WB/FUReport/";
                string destinationdir = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')) + "\\" +
                                         ds.Tables[0].Rows[0]["CASEID"].ToString() + "\\No Fault File\\Medicals\\WB\\FUReport\\";
                pdfpath = ConfigurationManager.AppSettings["DocumentManagerURL"] + filepath + filename;

                query = "exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + pdfbills[ii].ToString() + "', @FLAG='GET_EVENT_ID'";
                da = new SqlDataAdapter(query, con);
                DataSet dsEventId = new DataSet();
                da.Fill(dsEventId);

                string Companyname = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')).ToString();

                query = "exec SP_GET_WB_OFFICE_INFO @SZ_BILL_NO='" + pdfbills[ii] + "'";
                da = new SqlDataAdapter(query, con);
                DataSet dsWBOff_Info = new DataSet();
                da.Fill(dsWBOff_Info);

                MemoryStream m = new MemoryStream();
                iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 36, 36, 20, 36);
                #region Create PDF
                //create pdf
                float[] widthbase ={ 4f };
                PdfPTable tblBase = new PdfPTable(widthbase);
                tblBase.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                tblBase.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tblBase.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tblBase.DefaultCell.Colspan = 1;

                float[] widthheading ={ 4f };
                PdfPTable tblheading = new PdfPTable(widthheading);
                tblheading.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tblheading.DefaultCell.Border = Rectangle.NO_BORDER;
                tblheading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tblheading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tblheading.DefaultCell.Colspan = 1;
                PdfWriter writer = PdfWriter.GetInstance(document, m);
                document.Open();
                try
                {
                    tblheading.AddCell(new Phrase(dsWBOff_Info.Tables[0].Rows[0]["OFFICE_NAME"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 12, Font.BOLD, iTextSharp.text.Color.BLACK)));
                }
                catch (Exception ex)
                {
                    tblheading.AddCell(new Phrase(""));
                }
                try
                {
                    tblheading.AddCell(new Phrase(dsWBOff_Info.Tables[0].Rows[0]["OFFICE_ADDRESS"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                }
                catch (Exception ex)
                {
                    tblheading.AddCell(new Phrase(""));
                }
                try
                {
                    tblheading.AddCell(new Phrase(dsWBOff_Info.Tables[0].Rows[0]["OFFICE_CITY"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                }
                catch (Exception ex)
                {
                    tblheading.AddCell(new Phrase(""));
                }
                float[] widthSubheading ={ 3f, 3f };
                PdfPTable tblSubheading = new PdfPTable(widthSubheading);
                tblSubheading.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tblSubheading.DefaultCell.Border = Rectangle.NO_BORDER;
                tblSubheading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                tblSubheading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tblSubheading.AddCell(new Phrase("Tel: " + dsWBOff_Info.Tables[0].Rows[0]["OFFICE_PHONE"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblSubheading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                tblSubheading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tblSubheading.AddCell(new Phrase("Fax: " + dsWBOff_Info.Tables[0].Rows[0]["OFFICE_FAX"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblheading.DefaultCell.Border = Rectangle.BOTTOM_BORDER;

                tblheading.AddCell(tblSubheading);
                tblBase.AddCell(tblheading);
                for (int Event = 0; Event < dsEventId.Tables[0].Rows.Count; Event++)
                {

                    if (Event == 3)
                    {
                        tblBase.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin - tblheading.TotalHeight - 1, writer.DirectContent);
                        document.NewPage();
                        tblBase = new PdfPTable(widthbase);
                        tblBase.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                        tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                        tblBase.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tblBase.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                        tblBase.DefaultCell.Colspan = 1;
                    }

                    string strEventID = dsEventId.Tables[0].Rows[Event][0].ToString();
                    query = "exec SP_WB_NOTES  @I_EVENT_ID='" + strEventID + "'";
                    da = new SqlDataAdapter(query, con);
                    DataSet dsWB_data = new DataSet();
                    da.Fill(dsWB_data);

                    float[] widthbase2 ={ 4f };
                    PdfPTable tblBase2 = new PdfPTable(widthbase2);
                    tblBase2.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblBase2.DefaultCell.Border = Rectangle.BOX;
                    tblBase2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tblBase2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                    tblBase2.DefaultCell.Colspan = 1;

                    if (Event == 0)
                    {
                        float[] widthSubheading1 ={ 3f };
                        PdfPTable tblSubheading1 = new PdfPTable(widthSubheading1);
                        tblSubheading1.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                        tblSubheading.DefaultCell.Colspan = 2;

                        tblSubheading1.DefaultCell.Border = Rectangle.NO_BORDER;
                        tblSubheading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tblSubheading1.AddCell(new Phrase("PHYSICAL THERAPY AQUA-MED", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        tblBase2.AddCell(tblSubheading1);
                    }

                    float[] width ={ 2f, 2f, 2f, 2f };
                    PdfPTable heading = new PdfPTable(width);
                    heading.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                    heading.DefaultCell.Border = Rectangle.NO_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;

                    heading.AddCell(new Phrase("Patient Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase("-" + " " + dsWB_data.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["ACCIDENT_DATE"].ToString());
                    string dtDOA = dt.ToString("MM-dd-yyyy");
                    heading.AddCell(new Phrase("Date of Accident", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase("-" + " " + dtDOA, iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    heading.AddCell(new Phrase("Date", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading.AddCell(new Phrase("-" + " " + DateTime.Now.Date.ToString("MM-dd-yyyy"), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    heading.AddCell(new Phrase(""));
                    heading.AddCell(new Phrase(""));

                    tblBase2.AddCell(heading);

                    //Table 3
                    DataSet dsProc = new DataSet();
                    dsProc = GET_PROCEDURECODE_USING_EVENTID(strEventID);

                    float[] widthCode ={ 1f, 3f, 1f, 3f };
                    PdfPTable tblCode = new PdfPTable(widthCode);
                    tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblCode.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblCode.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblCode.DefaultCell.Colspan = 4;
                    tblCode.AddCell(new Phrase("Code", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    // heading.AddCell(new Phrase(companyaddress, iTextSharp.text.FontFactory.GetFont("Arial", 6, iTextSharp.text.Color.BLACK)));
                    //tblCode.DefaultCell.Border = Rectangle.NO_BORDER;
                    //tblCode.AddCell(new Phrase(""));
                    //tblCode.AddCell(new Phrase(""));
                    //tblCode.AddCell(new Phrase(""));
                    tblCode.DefaultCell.Colspan = 1;

                    for (int Count = 0; Count < dsProc.Tables[0].Rows.Count; Count++)
                    {

                        string path = strCheckPath;
                        iTextSharp.text.Image Img1 = iTextSharp.text.Image.GetInstance(path);
                        Img1.ScaleAbsolute(10f, 10f);
                        //Img1.Height=5f;
                        PdfPCell cell = new PdfPCell(Img1);
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                        cell.FixedHeight = 12f;
                        tblCode.AddCell(cell);
                        tblCode.DefaultCell.Border = Rectangle.NO_BORDER;
                        tblCode.AddCell(new Phrase(dsProc.Tables[0].Rows[Count]["Column1"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                        tblCode.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        tblCode.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                    }
                    if (dsProc.Tables[0].Rows.Count % 2 != 0)
                    {
                        tblCode.AddCell(new Phrase(""));
                        tblCode.AddCell(new Phrase(""));
                    }
                    tblBase2.AddCell(tblCode);


                    //Table 4
                    float[] widthTreat ={ 1f, 4f, 5f };
                    PdfPTable tblTreat = new PdfPTable(widthTreat);
                    tblTreat.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                    //tblTreat.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblTreat.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tblTreat.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;

                    tblTreat.AddCell(new Phrase("Treatment Date", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblTreat.AddCell(new Phrase("Notes", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    //-----------------------------------------------------------------------------------------------------------------------------------
                    float[] widthTreat2 ={ 8f };
                    PdfPTable tblTreat2 = new PdfPTable(widthTreat2);
                    tblTreat2.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblTreat2.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    tblTreat2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tblTreat2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                    //-----------------------------------------------------------------------------------------------
                    float[] widthTreat2a ={ 8f };
                    PdfPTable tblTreat2a = new PdfPTable(widthTreat2a);
                    tblTreat2a.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblTreat2a.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblTreat2a.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tblTreat2a.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                    tblTreat2a.AddCell(new Phrase("P R O G R E S S    N O T E S", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    //tblTreat2a.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    tblTreat2.AddCell(tblTreat2a);
                    //------------------------------------------------------------------------------------------------
                    float[] widthTreat2b ={ 1f, 1f, 1f, 1f, 1f, 3f, 3f };
                    PdfPTable tblTreat2b = new PdfPTable(widthTreat2b);
                    tblTreat2b.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblTreat2.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblTreat2b.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblTreat2b.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tblTreat2b.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;

                    tblTreat2b.AddCell(new Phrase("1", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblTreat2b.AddCell(new Phrase("2", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblTreat2b.AddCell(new Phrase("3", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblTreat2b.AddCell(new Phrase("4", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblTreat2b.AddCell(new Phrase("5", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    //tblTreat2b.AddCell(new Phrase("6", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    //tblTreat2b.AddCell(new Phrase("7", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblTreat2b.AddCell(new Phrase("Patient Sign", iTextSharp.text.FontFactory.GetFont("Arial", 6, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblTreat2b.AddCell(new Phrase("Doctor Sign", iTextSharp.text.FontFactory.GetFont("Arial", 6, Font.BOLD, iTextSharp.text.Color.BLACK)));

                    tblTreat2.AddCell(tblTreat2b);
                    tblTreat.AddCell(tblTreat2);
                    //----------------------------------------------------------------------------------------------------------

                    DateTime treatdate = Convert.ToDateTime(dsWB_data.Tables[0].Rows[0]["TREATMENT_DATE"]);
                    string final = treatdate.ToString("MM-dd-yyyy");
                    tblTreat.AddCell(new Phrase(final, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblTreat.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblTreat.AddCell(new Phrase(dsWB_data.Tables[0].Rows[0]["SZ_NOTES"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));


                    float[] widthPLevel ={ 1f, 1f, 1f, 1f, 1f, 3f, 3f };
                    PdfPTable tblPLevel = new PdfPTable(widthPLevel);
                    tblPLevel.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblPLevel.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblPLevel.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tblPLevel.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;

                    if (dsWB_data.Tables[0].Rows[0]["BT_NOT_EFFECTIVE"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblPLevel.AddCell(cellObj);
                        //tblPLevel.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblPLevel.AddCell(cellObj1);
                        //tblPLevel.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    if (dsWB_data.Tables[0].Rows[0]["BT_FAIR"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblPLevel.AddCell(cellObj);
                        //tblPLevel.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblPLevel.AddCell(cellObj1);
                        //tblPLevel.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    if (dsWB_data.Tables[0].Rows[0]["BT_MODERATE"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblPLevel.AddCell(cellObj);
                        //tblPLevel.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblPLevel.AddCell(cellObj1);
                        //tblPLevel.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    if (dsWB_data.Tables[0].Rows[0]["BT_SIGNIFICANT"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblPLevel.AddCell(cellObj);
                        //tblPLevel.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblPLevel.AddCell(cellObj1);
                        //tblPLevel.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    if (dsWB_data.Tables[0].Rows[0]["BT_OTHERS"].ToString() == "True")
                    {
                        string pathObj = strCheckPath;
                        iTextSharp.text.Image ImgObj = iTextSharp.text.Image.GetInstance(pathObj);
                        ImgObj.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj = new PdfPCell(ImgObj);
                        cellObj.Border = Rectangle.NO_BORDER;
                        cellObj.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj.FixedHeight = 12f;
                        tblPLevel.AddCell(cellObj);
                        //tblPLevel.DefaultCell.Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        string pathObj1 = strUnCheckPath;
                        iTextSharp.text.Image ImgObj1 = iTextSharp.text.Image.GetInstance(pathObj1);
                        ImgObj1.ScaleAbsolute(10f, 10f);
                        PdfPCell cellObj1 = new PdfPCell(ImgObj1);
                        cellObj1.Border = Rectangle.NO_BORDER;
                        cellObj1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cellObj1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cellObj1.FixedHeight = 12f;
                        tblPLevel.AddCell(cellObj1);
                        //tblPLevel.DefaultCell.Border = Rectangle.NO_BORDER;
                    }

                    try
                    {
                        iTextSharp.text.Image providerim = iTextSharp.text.Image.GetInstance(dsWB_data.Tables[0].Rows[0]["SZ_PATIENT_SIGN_PATH"].ToString().Replace("/", "\\"));
                        providerim.ScaleAbsoluteHeight(30);
                        providerim.ScaleAbsoluteWidth(50);
                        PdfPCell imagecell2 = new PdfPCell(providerim);
                        imagecell2.HorizontalAlignment = Element.ALIGN_LEFT;
                        imagecell2.Border = Rectangle.NO_BORDER;
                        imagecell2.PaddingBottom = 1f;
                        tblPLevel.AddCell(imagecell2);
                    }
                    catch (Exception ex)
                    {
                        tblPLevel.AddCell(new Phrase(""));
                    }
                    //tblSign.AddCell(new Phrase(""));


                    try
                    {
                        iTextSharp.text.Image providerim1 = iTextSharp.text.Image.GetInstance(dsWB_data.Tables[0].Rows[0]["SZ_DOCTOR_SIGN_PATH"].ToString().Replace("/", "\\"));
                        providerim1.ScaleAbsoluteHeight(30);
                        providerim1.ScaleAbsoluteWidth(50);
                        PdfPCell imagecell = new PdfPCell(providerim1);
                        imagecell.HorizontalAlignment = Element.ALIGN_LEFT;
                        imagecell.Border = Rectangle.NO_BORDER;
                        imagecell.PaddingBottom = 1f;
                        tblPLevel.AddCell(imagecell);
                        //tblPLevel.AddCell(new Phrase(""));
                    }
                    catch (Exception ex)
                    {
                        tblPLevel.AddCell(new Phrase(""));
                    }

                    tblTreat.AddCell(tblPLevel);

                    tblBase2.AddCell(tblTreat);

                    float[] widthPDesc ={ 1f, 2f, 1f, 2f };
                    PdfPTable tblPDesc = new PdfPTable(widthPDesc);
                    tblPDesc.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                    tblPDesc.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    tblPDesc.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPDesc.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPDesc.DefaultCell.Colspan = 4;
                    tblPDesc.AddCell(new Phrase("PROGRESS LEVEL", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblPDesc.DefaultCell.Colspan = 1;
                    tblPDesc.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblPDesc.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    tblPDesc.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    tblPDesc.AddCell(new Phrase("1", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblPDesc.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPDesc.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPDesc.AddCell(new Phrase("Not Effective", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    tblPDesc.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    tblPDesc.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    tblPDesc.AddCell(new Phrase("2", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblPDesc.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPDesc.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPDesc.AddCell(new Phrase("Fair", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    tblPDesc.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    tblPDesc.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    tblPDesc.AddCell(new Phrase("3", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblPDesc.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPDesc.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPDesc.AddCell(new Phrase("Moderate", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    tblPDesc.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    tblPDesc.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    tblPDesc.AddCell(new Phrase("4", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblPDesc.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPDesc.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPDesc.AddCell(new Phrase("Significant", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    tblPDesc.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    tblPDesc.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    tblPDesc.AddCell(new Phrase("5", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblPDesc.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPDesc.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    tblPDesc.AddCell(new Phrase("Others", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                    tblPDesc.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblPDesc.AddCell(new Phrase("", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    tblBase2.AddCell(tblPDesc);

                    //Table Complaints
                    DataSet dsComplaints = new DataSet();
                    dsComplaints = GET_COMPLIANTS_USING_EVENTID(strEventID);

                    float[] widthComplaints ={ 1f, 3f, 1f, 3f };
                    PdfPTable tblComplaints = new PdfPTable(widthComplaints);
                    //tblBase.DefaultCell.Border = Rectangle.BOX;
                    tblComplaints.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblComplaints.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                    heading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    tblComplaints.DefaultCell.Colspan = 4;
                    tblComplaints.AddCell(new Phrase("Complaints :", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                    tblComplaints.DefaultCell.Colspan = 1;

                    for (int Count = 0; Count < dsComplaints.Tables[0].Rows.Count; Count++)
                    {
                        string path = strCheckPath;
                        iTextSharp.text.Image Img1 = iTextSharp.text.Image.GetInstance(path);
                        Img1.ScaleAbsolute(10f, 10f);
                        PdfPCell cell = new PdfPCell(Img1);
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.FixedHeight = 12f;
                        tblComplaints.AddCell(cell);
                        tblComplaints.DefaultCell.Border = Rectangle.NO_BORDER;
                        tblComplaints.AddCell(new Phrase(dsComplaints.Tables[0].Rows[Count]["SZ_COMPLAINT"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));

                        tblComplaints.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        tblComplaints.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    }
                    if (dsComplaints.Tables[0].Rows.Count % 2 != 0)
                    {
                        tblComplaints.AddCell(new Phrase(""));
                        tblComplaints.AddCell(new Phrase(""));
                    }
                    //tblba.AddCell(tblComplaints);

                    //tblSign.DefaultCell.Colspan = 1;
                    tblBase2.AddCell(tblComplaints);
                    tblBase.AddCell(tblBase2);

                    float[] widthblank ={ 4f };
                    PdfPTable tblBlank = new PdfPTable(widthblank);
                    tblBlank.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    tblBlank.DefaultCell.Border = Rectangle.NO_BORDER;
                    tblBlank.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    tblBlank.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                    tblBlank.DefaultCell.Colspan = 1;
                    tblBlank.AddCell(new Phrase(""));
                    tblBlank.AddCell(new Phrase(""));
                    tblBase.AddCell(tblBlank);


                #endregion
                }
                tblBase.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin - tblheading.TotalHeight - 1, writer.DirectContent);
                document.Close();

                string sz_Case_id = ds.Tables[0].Rows[0]["CASEID"].ToString();

                query = "exec SP_INSERT_WB_BILLING_REPORT_TO_DOCMANAGER @SZ_CASE_ID='" + sz_Case_id + "', @SZ_FILE_NAME='" + filename + "', " +
                "@SZ_FILE_PATH='" + filepath + "', @SZ_LOGIN_ID ='" + szUserName + "'";
                da = new SqlDataAdapter(query, con);
                ds = new DataSet();
                da.Fill(ds);

                destinationdir = ds.Tables[1].Rows[0][0].ToString() + destinationdir;

                string szImageId = ds.Tables[2].Rows[0][0].ToString();

                if (!Directory.Exists(destinationdir))
                    Directory.CreateDirectory(destinationdir);
                System.IO.File.WriteAllBytes(destinationdir + filename, m.GetBuffer());

                InsertFUReport(pdfbills[ii].ToString(), sz_Case_id, szSpecialityID, filename, filepath, szCompanyId, szUserName, szImageId);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    public DataSet GET_PROCEDURECODE_USING_EVENTID(string sz_Event_ID)
    {
        DataSet ds = new DataSet();
        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(constring);
        SqlDataAdapter adop;
        try
        {
            //   con = new SqlConnection(strsqlCon);
            con.Open();


            SqlCommand cmd = new SqlCommand("GET_PROCEDURE_CODE_USING_EVENT_ID", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_Event_ID);

            cmd.CommandType = CommandType.StoredProcedure;
            adop = new SqlDataAdapter(cmd);
            adop.Fill(ds);
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

    public DataSet GET_COMPLIANTS_USING_EVENTID(string sz_Event_ID)
    {
        log.Debug("Start GET_COMPLIANTS_USING_EVENTID method.");
        DataSet ds = new DataSet();
        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(constring);
        SqlDataAdapter adop;
        try
        {
            //   con = new SqlConnection(strsqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_PT_COMPLAINTS", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_Event_ID);

            cmd.CommandType = CommandType.StoredProcedure;
            adop = new SqlDataAdapter(cmd);
            adop.Fill(ds);
            log.Debug("SP_GET_PT_COMPLAINTS : " + ds.Tables[0].Rows.Count);
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

    public DataSet GetChairoView(string eventID)
    {

        DataSet ds = new DataSet();
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_CHAIRO_NOTES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID ", eventID);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);

            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
    }

    public DataSet GetPtview(string eventID)
    {
        log.Debug("Start GetPtView method.");
        DataSet ds = new DataSet();
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_PT_NOTES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", eventID);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);

            sqlda.Fill(ds);
            log.Debug("SP_PT_NOTES : " + ds.Tables[0].Rows.Count);
            log.Debug("End of GetPtView method.");
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
    }

    public string GetSpecId(string szBillNo, string szCompanyId)
    {
        string szSpecId = "";
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_DOCTOR_SPECIALITY_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", szBillNo);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szSpecId = Convert.ToString(dr["SPECIALTTY_ID"].ToString());
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        finally { sqlCon.Close(); }
        return szSpecId;
    }

    public void InsertFUReport(string szBillno, string szCaseId, string szProcCodeId, string szFilename, string szFilePath, string szCompanyId, string szUserName, string szImageId)
    {
        log.Debug("Start InsertFUReport method.");
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_INSERT_FUREPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", szBillno);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            sqlCmd.Parameters.AddWithValue("@SZ_PROC_CODE", szProcCodeId);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", szFilename);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", szFilePath);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", szUserName);
            sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", szImageId);
            sqlCmd.ExecuteNonQuery();
            
            log.Debug("End of InsertFUReport method.");
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
    }

    public DataSet GetFUInformation(string szBillNo, string szProcedureGroupId, string szCompanyId)
    {
        log.Debug("Start GetFUInformation method.");
        DataSet ds = new DataSet();
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_FUREPORT_INFORMATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillNo);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szProcedureGroupId);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);

            sqlda.Fill(ds);
            log.Debug("SP_GET_FUREPORT_INFORMATION : " + ds.Tables[0].Rows.Count);
            log.Debug("End of GetFUInformation method.");
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
    }

    public void DeleteFUReportFile(string szFilename, string szFilePath, string szImageId, string szCaseID, string szSpecialityID, string szCompanyId, string szBillNo)
    {
        log.Debug("Start DeleteFUReportFile method.");
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_DELETE_FUREPORT_FILE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            //sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", szFilename);
            //sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", szFilePath);
            sqlCmd.Parameters.AddWithValue("@SZ_IMAGE_ID", szImageId);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", szBillNo);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szSpecialityID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
            sqlCmd.ExecuteNonQuery();

            log.Debug("End of DeleteFUReportFile method.");
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
    }
}
