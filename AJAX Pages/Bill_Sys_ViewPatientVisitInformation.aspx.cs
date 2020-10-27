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
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp;
using iTextSharp.text.html;
using iTextSharp.text;
using System.Data.SqlClient;

public partial class AJAX_Pages_Bill_Sys_ViewPatientVisitInformation : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (Request.QueryString["caseid"] != null && Request.QueryString["cmpid"] != null)
            {
                string szCaseID = Request.QueryString["caseid"].ToString();
                string szCompanyID = Request.QueryString["cmpid"].ToString();

                DataSet ds = new DataSet();
                MUVGenerateFunction objSettings = new MUVGenerateFunction();
                ds = GetPatienView(szCaseID, szCompanyID);
                string szfirstname = "";
                string szlastname = "";
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString() != "")
                {
                    szfirstname = ds.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                    szfirstname = szfirstname.Replace(" ", string.Empty);
                    szfirstname = szfirstname.Replace(".", string.Empty);
                    szfirstname = szfirstname.Replace(",", string.Empty);
                }
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString() != "")
                {
                    szlastname = ds.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                    szlastname = szlastname.Replace(" ", string.Empty);
                    szlastname = szlastname.Replace(".", string.Empty);
                    szlastname = szlastname.Replace(",", string.Empty);
                }
                string path = objSettings.getApplicationSetting("PatientInfoSaveFilePath");
                string OpenFilepath = objSettings.getApplicationSetting("PatientInfoOpenFilePath");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string newPdfFilename = szfirstname.Trim() + "_" + szlastname.Trim() + "_" + DateTime.Now.ToString("MM_dd_yyyyhhmm") + ".pdf";
                string pdfPath = path + newPdfFilename;
                MemoryStream m = new MemoryStream();
                iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 36, 36, 20, 20);
                float[] wBase = { 4f };
                PdfPTable tblBase = new PdfPTable(wBase);
                tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                tblBase.WidthPercentage = 100;
                tblBase.AddCell(" ");
                PdfWriter writer = PdfWriter.GetInstance(document, m);
                document.Open();
                #region "for printed by"
                float[] width = { 4f, 4f };
                PdfPTable tblprintby = new PdfPTable(width);
                tblprintby.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tblprintby.DefaultCell.Border = Rectangle.NO_BORDER;
                tblprintby.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                tblprintby.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                tblprintby.AddCell(new Phrase("Printed By : " + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                tblprintby.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                tblprintby.AddCell(new Phrase("Printed on : " + DateTime.Now.ToString("MM/dd/yyyy"), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                tblBase.AddCell(tblprintby);
                #endregion
                tblBase.AddCell(" ");

                #region "for patient information"
                float[] wdh = { 4f };
                PdfPTable tblheading = new PdfPTable(wdh);
                tblheading.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tblheading.DefaultCell.Border = Rectangle.NO_BORDER;
                tblheading.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                tblheading.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                tblheading.AddCell(new Phrase("Visit Details", iTextSharp.text.FontFactory.GetFont("Arial", 14, Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblBase.AddCell(tblheading);
                #endregion

                #region for Patient Information
                float[] w11 = { 3f, 3f, 3f, 3f };
                PdfPTable table = new PdfPTable(w11);
                table.WidthPercentage = 100;
                table.DefaultCell.BorderColor = Color.BLACK;
                PdfPCell cell1 = new PdfPCell(new Phrase("Patient Information", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Color.BLACK)));
                cell1.Colspan = 4;
                cell1.BackgroundColor = Color.LIGHT_GRAY;
                cell1.BorderColor = Color.BLACK;

                table.AddCell(cell1);
                table.AddCell(new Phrase("First Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Middle Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["MI"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["MI"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Last Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("D.O.B", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["DOB"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["DOB"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Address", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Phone No", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PATIENT_PHONE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("SSN", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Location", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_LOCATION_NAME"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_LOCATION_NAME"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Policy Holder", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_POLICY_HOLDER"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }

                table.AddCell(new Phrase("Insurance Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_INSURANCE_NAME"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Claim No", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Accident Date", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["DT_ADMISSION_DATE"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["DT_ADMISSION_DATE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                PdfPCell cell2 = new PdfPCell(new Phrase(""));
                cell2.Colspan = 2;
                cell2.BorderColor = Color.BLACK;
                table.AddCell(cell2);
                tblBase.AddCell(table);
                #endregion
                tblBase.AddCell(" ");
            
                #region for Visit Information
                DataSet ds3 = new DataSet();
                ds3 = GetPatienVisitInfo(szCaseID, szCompanyID);     
                int flag = 0;
                int checkstart = 0;
                int count = 0;
                int fix = 25;
                int start = 0;
                int end = 0;
                count = ds3.Tables[0].Rows.Count;
                while(flag==0)
                {
                    if (start == 0)
                    {
                        end = 25;
                        if(count<=25)
                        {
                            end = count;
                            flag = 1;
                        }  
                    }
                    else
                    { 
                        start = end;
                        end = end + 30;
                        if (end >= count)
                        {
                            end = count;
                            flag = 1;
                        }
                    }
                    float[] wd1 = { 1.5f, 4f, 1.5f, 1.5f, 4f, 3f, 1.2f };
                    PdfPTable tblVisit = new PdfPTable(wd1);
                    tblVisit.WidthPercentage = 100;
                    tblVisit.DefaultCell.BorderColor = Color.BLACK;
                    tblVisit = genratesVisitTable(start, end, ds3);
                    tblBase.AddCell(tblVisit);
                    if (checkstart == 0)
                    {
                        checkstart = 1;
                        start = 1;                      
                    }
                }
                #endregion
                tblBase.AddCell(" ");
                document.Add(tblBase);
                document.Close();
                System.IO.File.WriteAllBytes(pdfPath, m.GetBuffer());
                string OpenPdfFilepath = OpenFilepath + newPdfFilename;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "sandeep", "<script type='text/javascript'>window.location.href='" + OpenPdfFilepath + "'</script>");
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    public PdfPTable genratesVisitTable(int start, int end, DataSet ds3)
    {
        float[] wd1 = { 1.5f, 4f, 1.5f, 1.5f, 4f, 3f, 1.2f };
        PdfPTable tblVisit = new PdfPTable(wd1);
        tblVisit.WidthPercentage = 100;
        tblVisit.DefaultCell.BorderColor = Color.BLACK;
        PdfPCell cell3 = new PdfPCell(new Phrase("Visit Information", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Color.BLACK)));
        PdfPCell cellVisitDate = new PdfPCell(new Phrase("Visit Date", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
        PdfPCell cellDocName = new PdfPCell(new Phrase("Doctor Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
        PdfPCell cellSpeciality = new PdfPCell(new Phrase("Specialty", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
        PdfPCell cellVisitType = new PdfPCell(new Phrase("Visit Type", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
        PdfPCell cellProviderName = new PdfPCell(new Phrase("Provider Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
        PdfPCell cellProcedureCode = new PdfPCell(new Phrase("Procedure Code", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
        PdfPCell cellBillUnBill = new PdfPCell(new Phrase("Billed/ UnBilled", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
        cell3.Colspan = 7;
        cellVisitDate.Colspan = 1;
        cellDocName.Colspan = 1;
        cellSpeciality.Colspan = 1;
        cellVisitType.Colspan = 1;
        cellProviderName.Colspan = 1;
        cellProcedureCode.Colspan = 1;
        cellBillUnBill.Colspan = 1;
        cell3.BorderColor = Color.BLACK;
        cell3.BackgroundColor = Color.LIGHT_GRAY;
        tblVisit.AddCell(cell3);
        tblVisit.AddCell(cellVisitDate);
        tblVisit.AddCell(cellDocName);
        tblVisit.AddCell(cellSpeciality);
        tblVisit.AddCell(cellVisitType);
        tblVisit.AddCell(cellProviderName);
        tblVisit.AddCell(cellProcedureCode);
        tblVisit.AddCell(cellBillUnBill);

        for (int i = start; i < end ; i++)
        {
            if (ds3.Tables[0].Rows[i]["DOB"].ToString() != "")
            {
                tblVisit.AddCell(new Phrase(Convert.ToString(ds3.Tables[0].Rows[i]["DOB"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            if (ds3.Tables[0].Rows[i]["SZ_DOCTOR_NAME"].ToString() != "")
            {
                tblVisit.AddCell(new Phrase(Convert.ToString(ds3.Tables[0].Rows[i]["SZ_DOCTOR_NAME"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            if (ds3.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP"].ToString() != "")
            {
                tblVisit.AddCell(new Phrase(Convert.ToString(ds3.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            if (ds3.Tables[0].Rows[i]["VISIT_TYPE"].ToString() != "")
            {
                tblVisit.AddCell(new Phrase(Convert.ToString(ds3.Tables[0].Rows[i]["VISIT_TYPE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            if (ds3.Tables[0].Rows[i]["SZ_OFFICE"].ToString() != "")
            {
                tblVisit.AddCell(new Phrase(Convert.ToString(ds3.Tables[0].Rows[i]["SZ_OFFICE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            if (ds3.Tables[0].Rows[i]["sz_code"].ToString() != "")
            {
                tblVisit.AddCell(new Phrase(Convert.ToString(ds3.Tables[0].Rows[i]["sz_code"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            if (ds3.Tables[0].Rows[i]["BT_STATUS"].ToString() != "")
            {
                tblVisit.AddCell(new Phrase(Convert.ToString(ds3.Tables[0].Rows[i]["BT_STATUS"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
            else
            {
                tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
            }
        }
        PdfPCell cell14 = new PdfPCell(new Phrase(""));
        cell14.Colspan = 2;
        cell14.BorderColor = Color.BLACK;
        tblVisit.AddCell(cell14);
        return tblVisit;
    }
    public DataSet GetPatienVisitInfo(string caseID, string companyID)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds1 = new DataSet();
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd1 = new SqlCommand("SP_GetVisitInformation", sqlCon);
            sqlCmd1.CommandType = CommandType.StoredProcedure;
            sqlCmd1.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd1.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd1);
            sqlda.Fill(ds1);
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds1;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    public DataSet GetPatienView(string caseID, string companyID)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds = new DataSet();
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_PATIENTVISITPOP_VIEW", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
         }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
