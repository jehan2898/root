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

public partial class AJAX_Pages_Bill_Sys_ViewVisitInformation : PageBase
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
            if (Request.QueryString["caseid"] != null )
            {
                string szCaseID = Request.QueryString["caseid"].ToString();
                string szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();

                DataSet ds = new DataSet();
                MUVGenerateFunction objSettings = new MUVGenerateFunction();
                ds = GetVisitInfo(szCaseID, szCompanyID);
                string szfirstname = "";
                string szlastname = "";
                if (ds.Tables[0].Rows[0]["PatientName"].ToString() != "")
                {
                    szfirstname = ds.Tables[0].Rows[0]["PatientName"].ToString();
                    szfirstname = szfirstname.Replace(" ", string.Empty);
                    szfirstname = szfirstname.Replace(".", string.Empty);
                    szfirstname = szfirstname.Replace(",", string.Empty);
                }
                //if (ds.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString() != "")
                //{
                //    szlastname = ds.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                //    szlastname = szlastname.Replace(" ", string.Empty);
                //    szlastname = szlastname.Replace(".", string.Empty);
                //    szlastname = szlastname.Replace(",", string.Empty);
                //}
                string path =  ConfigurationManager.AppSettings["VisitInfoPDFPATH"].ToString();// objSettings.getApplicationSetting("PatientInfoSaveFilePath");
                string OpenFilepath = objSettings.getApplicationSetting("PatientInfoOpenFilePath");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string newPdfFilename = szfirstname.Trim() + "_Visit_Information_" + DateTime.Now.ToString("MM_dd_yyyyhhmm") + ".pdf";
                string pdfPath = path + newPdfFilename;
                MemoryStream m = new MemoryStream();
                iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 36, 36, 20, 20);
                float[] wBase = { 4f };
                PdfPTable tblBase = new PdfPTable(wBase);
                tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                tblBase.WidthPercentage = 100;
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
                //tblheading.AddCell(new Phrase("Patient Information", iTextSharp.text.FontFactory.GetFont("Arial", 14, Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblBase.AddCell(tblheading);
                #endregion

                #region for Personal Information
                float[] w11 = { 3f, 3f, 3f, 3f };
                PdfPTable table = new PdfPTable(w11);
                table.WidthPercentage = 100;
                table.DefaultCell.BorderColor = Color.BLACK;
                PdfPCell cell1 = new PdfPCell(new Phrase("Personal Information", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Color.BLACK)));
                cell1.Colspan = 4;
                cell1.BackgroundColor = Color.LIGHT_GRAY;
                cell1.BorderColor = Color.BLACK;
                table.AddCell(cell1);
                table.AddCell(new Phrase("Patient Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["PatientName"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["PatientName"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Case #", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["CaseNo"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["CaseNo"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
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
                
                PdfPCell cell2 = new PdfPCell(new Phrase(""));
                cell2.Colspan = 2;
                cell2.BorderColor = Color.BLACK;
                table.AddCell(cell2);
                tblBase.AddCell(table);
                #endregion

                tblBase.AddCell(" ");

                #region for Visit Information
                float[] wd1 = { 3f, 3f, 3f, 3f,3f,3f };
                PdfPTable tblVisit = new PdfPTable(wd1);
                tblVisit.WidthPercentage = 100;
                tblVisit.DefaultCell.BorderColor = Color.BLACK;
                PdfPCell cell3 = new PdfPCell(new Phrase("Visit Information", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Color.BLACK)));
                cell3.Colspan = 6;
                cell3.BorderColor = Color.BLACK;
                cell3.BackgroundColor = Color.LIGHT_GRAY;
                tblVisit.AddCell(cell3);
                tblVisit.AddCell(new Phrase("Doctor Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblVisit.AddCell(new Phrase("Provider", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblVisit.AddCell(new Phrase("Visit Type", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblVisit.AddCell(new Phrase("Status", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblVisit.AddCell(new Phrase("Procedure Code", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                tblVisit.AddCell(new Phrase("Bill Status", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                int j = 0;
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (j <= 5)
                    {
                        if (ds.Tables[1].Rows[i]["DctorName"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(ds.Tables[1].Rows[i]["DctorName"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        }


                        if (ds.Tables[1].Rows[i]["Provider"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(ds.Tables[1].Rows[i]["Provider"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        }

                        if (ds.Tables[1].Rows[i]["VisitType"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(ds.Tables[1].Rows[i]["VisitType"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        }

                        if (ds.Tables[1].Rows[i]["STATUS"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(ds.Tables[1].Rows[i]["STATUS"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        }

                        if (ds.Tables[1].Rows[i]["ProcedureCode"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(ds.Tables[1].Rows[i]["ProcedureCode"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        }

                        if (ds.Tables[1].Rows[i]["BillStatus"].ToString() != "")
                        {
                            tblVisit.AddCell(new Phrase(Convert.ToString(ds.Tables[1].Rows[i]["BillStatus"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        }
                        else
                        {
                            tblVisit.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                        }
                        j++;
                    }
                    else 
                    {
                        document.NewPage();
                        j = 0;
                    }
                }
                
                

                PdfPCell cell14 = new PdfPCell(new Phrase(""));
                cell14.Colspan = 2;
                cell14.BorderColor = Color.BLACK;
                tblVisit.AddCell(cell14);
                tblBase.AddCell(tblVisit);
                #endregion
                tblBase.AddCell(" ");
                tblBase.AddCell(" ");
                tblBase.AddCell(" ");
                tblBase.AddCell(" ");
                tblBase.AddCell(" ");
                tblBase.AddCell(" ");
                tblBase.AddCell(" ");
                tblBase.AddCell(" ");
                tblBase.AddCell(" ");
                tblBase.AddCell(" ");
                tblBase.AddCell(" ");
             

                //#region "for websource information"
                //float[] wd5 = { 4f };
                //PdfPTable tblSource = new PdfPTable(wd5);
                //tblSource.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                //tblSource.DefaultCell.Border = Rectangle.NO_BORDER;
                //tblSource.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                //tblSource.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                //if (ds.Tables[0].Rows[0]["SZ_WEBSOURCE"].ToString() != "")
                //{
                //    tblSource.AddCell(new Phrase("Source : " + ds.Tables[0].Rows[0]["SZ_WEBSOURCE"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                //}
                //else
                //{
                //    tblInsurance.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                //}

                //tblBase.AddCell(tblSource);
                //#endregion
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

    public DataSet GetVisitInfo(string caseID, string companyID)
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
            SqlCommand sqlCmd = new SqlCommand("sp_get_visit_information", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

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
