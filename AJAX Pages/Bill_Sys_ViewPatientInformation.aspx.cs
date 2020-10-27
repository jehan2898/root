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

public partial class AJAX_Pages_Bill_Sys_ViewPatientInformation : PageBase
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
                string path = ApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                string OpenFilepath = ApplicationSettings.GetParameterValue("PatientInfoOpenFilePath"); 
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
                tblheading.AddCell(new Phrase("Patient Information", iTextSharp.text.FontFactory.GetFont("Arial", 14, Font.BOLD, iTextSharp.text.Color.BLACK)));
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
                table.AddCell(new Phrase("Gender", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_GENDER"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_GENDER"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
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
                table.AddCell(new Phrase("City", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PATIENT_CITY"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("State", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PATIENT_STATE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Cell Phone #", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["sz_patient_cellno"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["sz_patient_cellno"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }

                table.AddCell(new Phrase("Home Phone", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PATIENT_PHONE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }

                table.AddCell(new Phrase("Work", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_WORK_PHONE"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_WORK_PHONE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Zip", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PATIENT_ZIP"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Email", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_EMAIL"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PATIENT_EMAIL"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Extn.", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_WORK_PHONE_EXTENSION"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_WORK_PHONE_EXTENSION"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Attorney", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_ATTORNEY"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_ATTORNEY"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                table.AddCell(new Phrase("Case Type", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_CASE_TYPE"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_CASE_TYPE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }

                table.AddCell(new Phrase("Attorney Address", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_ATTORNEY_ADDRESS"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_ATTORNEY_ADDRESS"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }

                table.AddCell(new Phrase("Attorney Phone", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["sz_attorney_phone"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["sz_attorney_phone"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    table.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }

                table.AddCell(new Phrase("Case Status", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_CASE_STATUS"].ToString() != "")
                {
                    table.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_CASE_STATUS"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
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

               


                PdfPCell cell2 = new PdfPCell(new Phrase(""));
                cell2.Colspan = 2;
                cell2.BorderColor = Color.BLACK;
                table.AddCell(cell2);
                tblBase.AddCell(table);
                #endregion

                tblBase.AddCell(" ");

                #region for Insurance Information
                float[] wd1 = { 3f, 3f, 3f, 3f };
                PdfPTable tblInsurance = new PdfPTable(wd1);
                tblInsurance.WidthPercentage = 100;
                tblInsurance.DefaultCell.BorderColor = Color.BLACK;
                PdfPCell cell3 = new PdfPCell(new Phrase("Insurance Information", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Color.BLACK)));
                cell3.Colspan = 4;
                cell3.BorderColor = Color.BLACK;
                cell3.BackgroundColor = Color.LIGHT_GRAY;
                tblInsurance.AddCell(cell3);
                tblInsurance.AddCell(new Phrase("Policy Holder", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString() != "")
                {
                    tblInsurance.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_POLICY_HOLDER"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblInsurance.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                
                tblInsurance.AddCell(new Phrase("Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString() != "")
                {
                    tblInsurance.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_INSURANCE_NAME"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblInsurance.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblInsurance.AddCell(new Phrase("Address", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString() != "")
                {
                    tblInsurance.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblInsurance.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblInsurance.AddCell(new Phrase("City", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString() != "")
                {
                    tblInsurance.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_INSURANCE_CITY"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblInsurance.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblInsurance.AddCell(new Phrase("State", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString() != "")
                {
                    tblInsurance.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_INSURANCE_STATE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblInsurance.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblInsurance.AddCell(new Phrase("Zip", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString() != "")
                {
                    tblInsurance.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblInsurance.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblInsurance.AddCell(new Phrase("Phone", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString() != "")
                {
                    tblInsurance.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblInsurance.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblInsurance.AddCell(new Phrase("Fax", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_FAX_NUMBER"].ToString() != "")
                {
                    tblInsurance.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_FAX_NUMBER"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblInsurance.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblInsurance.AddCell(new Phrase("Contact Person", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_CONTACT_PERSON"].ToString() != "")
                {
                    tblInsurance.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_CONTACT_PERSON"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblInsurance.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblInsurance.AddCell(new Phrase("Claim File #", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "")
                {
                    tblInsurance.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblInsurance.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblInsurance.AddCell(new Phrase("Policy #", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "")
                {
                    tblInsurance.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_POLICY_NUMBER"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblInsurance.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }

                PdfPCell cell14 = new PdfPCell(new Phrase(""));
                cell14.Colspan = 2;
                cell14.BorderColor = Color.BLACK;
                tblInsurance.AddCell(cell14);
                tblBase.AddCell(tblInsurance);
                #endregion

                tblBase.AddCell(" ");

                #region for Accident Information
                float[] wd2 = { 3f, 3f, 3f, 3f };
                PdfPTable tblAccident = new PdfPTable(wd2);
                tblAccident.WidthPercentage = 100;
                tblAccident.DefaultCell.BorderColor = Color.BLACK;
                PdfPCell cell4 = new PdfPCell(new Phrase("Accident Information", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Color.BLACK)));
                cell4.Colspan = 4;
                cell4.BorderColor = Color.BLACK;
                cell4.BackgroundColor = Color.LIGHT_GRAY;
                tblAccident.AddCell(cell4);
                tblAccident.AddCell(new Phrase("Accident Date", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "")
                {
                    tblAccident.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["DT_ACCIDENT_DATE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAccident.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAccident.AddCell(new Phrase("Plate Number", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString() != "")
                {
                    tblAccident.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PLATE_NO"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAccident.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAccident.AddCell(new Phrase("Report Number", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString() != "")
                {
                    tblAccident.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_REPORT_NO"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAccident.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAccident.AddCell(new Phrase("Address", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != "")
                {
                    tblAccident.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAccident.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAccident.AddCell(new Phrase("City", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != "")
                {
                    tblAccident.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAccident.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAccident.AddCell(new Phrase("State", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_ACCIDENT_INSURANCE_STATE"].ToString() != "")
                {
                    tblAccident.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_ACCIDENT_INSURANCE_STATE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAccident.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAccident.AddCell(new Phrase("Hospital Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_HOSPITAL_NAME"].ToString() != "")
                {
                    tblAccident.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_HOSPITAL_NAME"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAccident.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAccident.AddCell(new Phrase("Hospital Address", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_HOSPITAL_ADDRESS"].ToString() != "")
                {
                    tblAccident.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_HOSPITAL_ADDRESS"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAccident.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAccident.AddCell(new Phrase("Date of Admission", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["DT_ADMISSION_DATE"].ToString() != "")
                {
                    tblAccident.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["DT_ADMISSION_DATE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAccident.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAccident.AddCell(new Phrase("Additional Patient", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString() != "")
                {
                    tblAccident.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAccident.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAccident.AddCell(new Phrase("Describe Injury", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_DESCRIBE_INJURY"].ToString() != "")
                {
                    tblAccident.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_DESCRIBE_INJURY"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAccident.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAccident.AddCell(new Phrase("Patient Type", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString() != "")
                {
                    tblAccident.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PATIENT_TYPE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAccident.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }

                tblBase.AddCell(tblAccident);
                #endregion

                tblBase.AddCell(" ");

                #region for Employer Information
                float[] wd3 = { 3f, 3f, 3f, 3f };
                PdfPTable tblEmployer = new PdfPTable(wd3);
                tblEmployer.WidthPercentage = 100;
                tblEmployer.DefaultCell.BorderColor = Color.BLACK;
                PdfPCell cell5 = new PdfPCell(new Phrase("Employer Information", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Color.BLACK)));
                cell5.Colspan = 4;
                cell5.BorderColor = Color.BLACK;
                cell5.BackgroundColor = Color.LIGHT_GRAY;
                tblEmployer.AddCell(cell5);
                tblEmployer.AddCell(new Phrase("Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString() != "")
                {
                    tblEmployer.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblEmployer.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblEmployer.AddCell(new Phrase("Address", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString() != "")
                {
                    tblEmployer.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblEmployer.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblEmployer.AddCell(new Phrase("City", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString() != "")
                {
                    tblEmployer.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblEmployer.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblEmployer.AddCell(new Phrase("State", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString() != "")
                {
                    tblEmployer.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblEmployer.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblEmployer.AddCell(new Phrase("Zip", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString() != "")
                {
                    tblEmployer.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblEmployer.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblEmployer.AddCell(new Phrase("Phone", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE"].ToString() != "")
                {
                    tblEmployer.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblEmployer.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblEmployer.AddCell(new Phrase("Date of First Treatment", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString() != "")
                {
                    tblEmployer.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["DT_FIRST_TREATMENT"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblEmployer.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblEmployer.AddCell(new Phrase("Chart #", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_CHART_NO"].ToString() != "")
                {
                    tblEmployer.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_CHART_NO"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblEmployer.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }

                tblBase.AddCell(tblEmployer);
                #endregion

                tblBase.AddCell(" ");

                #region for Adjuster Information
                float[] wd4 = { 3f, 3f, 3f, 3f };
                PdfPTable tblAdjuster = new PdfPTable(wd4);
                tblAdjuster.WidthPercentage = 100;
                tblAdjuster.DefaultCell.BorderColor = Color.BLACK;
                PdfPCell cell6 = new PdfPCell(new Phrase("Adjuster Information", iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Color.BLACK)));
                cell6.Colspan = 4;
                cell6.BorderColor = Color.BLACK;
                cell6.BackgroundColor = Color.LIGHT_GRAY;
                tblAdjuster.AddCell(cell6);
                tblAdjuster.AddCell(new Phrase("Name", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_ADJUSTER_NAME"].ToString() != "")
                {
                    tblAdjuster.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_ADJUSTER_NAME"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAdjuster.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAdjuster.AddCell(new Phrase("Phone", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_PHONE"].ToString() != "")
                {
                    tblAdjuster.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_PHONE"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAdjuster.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAdjuster.AddCell(new Phrase("Extension", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_EXTENSION"].ToString() != "")
                {
                    tblAdjuster.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_EXTENSION"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAdjuster.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAdjuster.AddCell(new Phrase("Fax", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_FAX"].ToString() != "")
                {
                    tblAdjuster.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_FAX"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAdjuster.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                tblAdjuster.AddCell(new Phrase("Email", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                if (ds.Tables[0].Rows[0]["SZ_EMAIL"].ToString() != "")
                {
                    tblAdjuster.AddCell(new Phrase(Convert.ToString(ds.Tables[0].Rows[0]["SZ_EMAIL"]), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAdjuster.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                PdfPCell cell7 = new PdfPCell(new Phrase(""));
                cell7.Colspan = 2;
                cell7.BorderColor = Color.BLACK;
                tblAdjuster.AddCell(cell7);
                tblBase.AddCell(tblAdjuster);
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
             

                #region "for websource information"
                float[] wd5 = { 4f };
                PdfPTable tblSource = new PdfPTable(wd5);
                tblSource.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tblSource.DefaultCell.Border = Rectangle.NO_BORDER;
                tblSource.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                tblSource.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
                if (ds.Tables[0].Rows[0]["SZ_WEBSOURCE"].ToString() != "")
                {
                    tblSource.AddCell(new Phrase("Source : " + ds.Tables[0].Rows[0]["SZ_WEBSOURCE"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }
                else
                {
                    tblAdjuster.AddCell(new Phrase("-", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                }

                tblBase.AddCell(tblSource);
                #endregion
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

    public DataSet GetPatienView(string caseID, string companyID)
    {//Logging Start
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
            SqlCommand sqlCmd = new SqlCommand("SP_PATIENTPOP_VIEW", sqlCon);
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
