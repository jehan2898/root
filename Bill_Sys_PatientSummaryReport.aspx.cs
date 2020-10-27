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
using System.Text;
using System.IO;

public partial class Bill_Sys_PatientSummaryReport : PageBase
{
    GeneratePatientInfoPDF _generatePatientInfoPDF;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
       
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_PatientSummaryReport.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
   
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
            {
            StringBuilder sbMainData = new StringBuilder();
            GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
            string pdffilename = "";
            Boolean flag = false;
            for (int i = 0; i < grdPatientSummaryReport.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdPatientSummaryReport.Items[i].FindControl("chkPrint");
                String szCurrentCaseData = "";
                 if (chkDelete1.Checked)
                 {
                     flag = true;
                     string strHtml = File.ReadAllText(ConfigurationManager.AppSettings["PATIENT_INFO_HTML"]);
                     strHtml = objPDF.getReplacedString(strHtml, grdPatientSummaryReport.Items[i].Cells[0].Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                     sbMainData.Append(strHtml);
                 }
            }

            if (flag)
            {
                //Generate PDF
                SautinSoft.PdfMetamorphosis objHTMToPDF = new SautinSoft.PdfMetamorphosis();
                objHTMToPDF.Serial = "10007706603";
                string htmfilename = getFileName("P") + ".htm";
                pdffilename = getFileName("P") + ".pdf";
                StreamWriter sw = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + htmfilename);
                sw.Write(sbMainData.ToString());
                sw.Close();
                Int32 iTemp;
                iTemp = objHTMToPDF.HtmlToPdfConvertFile(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + htmfilename, ApplicationSettings.GetParameterValue("EXCEL_SHEET") + pdffilename);

                // Open PDF

                string szDefaultPath = ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET");
                string szPDFName = "";
                szPDFName = szDefaultPath + pdffilename;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFName.ToString() + "'); ", true);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }

    protected void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _generatePatientInfoPDF = new GeneratePatientInfoPDF();
            DataSet objDatasetResult = new DataSet();
           //Code to get fill grid location id wise
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
            {
                DataTable objDTLocationWise = new DataTable();
                objDatasetResult = _generatePatientInfoPDF.getPatientSummaryReport(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID,"NotNull");
                objDTLocationWise = DisplayLocationInGrid(objDatasetResult);
                grdPatientSummaryReport.DataSource = objDTLocationWise;
                grdPatientSummaryReport.DataBind();
                for (int i = 0; i < grdPatientSummaryReport.Items.Count; i++)
                {
                    if (grdPatientSummaryReport.Items[i].Cells[1].Text.Trim() == "<b>Location</b>")
                    {
                        ((CheckBox)grdPatientSummaryReport.Items[i].Cells[6].FindControl("chkPrint")).Visible = false;
                    }
                }
               
            }//end code
            else
            {
                objDatasetResult = _generatePatientInfoPDF.getPatientSummaryReport(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                grdPatientSummaryReport.DataSource = objDatasetResult;
                grdPatientSummaryReport.DataBind();
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }



    #region "Display Location wise patient in grid"

    public DataTable DisplayLocationInGrid(DataSet p_objDS)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet objDS = new DataSet();
        objDS = p_objDS;
        DataTable objDT = new DataTable();
        try
        {



            objDT.Columns.Add("SZ_CASE_ID");
            objDT.Columns.Add("SZ_CASE_NO");
            objDT.Columns.Add("SZ_PATIENT_NAME");
            objDT.Columns.Add("SZ_INSURANCE_NAME");
            objDT.Columns.Add("DT_DATE_OF_ACCIDENT");
            objDT.Columns.Add("SZ_ATTORNEY_NAME");
            objDT.Columns.Add("SZ_LOCATION_NAME");
            objDT.Columns.Add("DATE_OF_FIRST_TREATMENT");
            objDT.Columns.Add("DATE_OF_LAST_TREATMENT");

         

            DataRow objDR;
            string sz_Location_Name = "NA";




            for (int i = 0; i < objDS.Tables[0].Rows.Count; i++)
            {
                if (objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString().Equals(sz_Location_Name))
                {
                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_ID"] = objDS.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                    objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                    objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_INSURANCE_NAME"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString();
                    objDR["DT_DATE_OF_ACCIDENT"] = objDS.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    objDR["SZ_ATTORNEY_NAME"] = objDS.Tables[0].Rows[i]["SZ_ATTORNEY_NAME"].ToString();
                    objDR["SZ_LOCATION_NAME"] = objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString();
                    objDR["DATE_OF_FIRST_TREATMENT"] = objDS.Tables[0].Rows[i]["DATE_OF_FIRST_TREATMENT"].ToString();
                    objDR["DATE_OF_LAST_TREATMENT"] = objDS.Tables[0].Rows[i]["DATE_OF_LAST_TREATMENT"].ToString();
                    objDT.Rows.Add(objDR);
                    sz_Location_Name = objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString();


                }
                else
                {


                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_NO"] = "<b>Location</b>";
                    objDR["SZ_PATIENT_NAME"] = "<b>" + objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString() + "<b>";
                    objDR["SZ_INSURANCE_NAME"] = "";
                    objDR["DT_DATE_OF_ACCIDENT"] = "";
                    objDR["SZ_ATTORNEY_NAME"] = "";
                    objDR["SZ_LOCATION_NAME"] = "";
                    objDR["DATE_OF_FIRST_TREATMENT"] = "";
                    objDR["DATE_OF_LAST_TREATMENT"] = "";
                 
                    int count = grdPatientSummaryReport.Items.Count;



                    objDT.Rows.Add(objDR);


                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_ID"] = objDS.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                    objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                    objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_INSURANCE_NAME"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString();
                    objDR["DT_DATE_OF_ACCIDENT"] = objDS.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    objDR["SZ_ATTORNEY_NAME"] = objDS.Tables[0].Rows[i]["SZ_ATTORNEY_NAME"].ToString();
                    objDR["SZ_LOCATION_NAME"] = objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString();
                    objDR["DATE_OF_FIRST_TREATMENT"] = objDS.Tables[0].Rows[i]["DATE_OF_FIRST_TREATMENT"].ToString();
                    objDR["DATE_OF_LAST_TREATMENT"] = objDS.Tables[0].Rows[i]["DATE_OF_LAST_TREATMENT"].ToString();
                    objDT.Rows.Add(objDR);
                    sz_Location_Name = objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString();





                }
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
       
        return objDT;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion
}
