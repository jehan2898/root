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
public partial class Bill_Sys_MRIReport : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            btnSearch.Attributes.Add("onclick", "return ChekReportType();");
            if (!IsPostBack)
            {
                lblHeader.Text = "Reports - Procedure";
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlOffice.Flag_ID = txtCompanyID.Text;
                extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                chkShowReport.Checked = true;
                
            }
            
                lblOffice.Text = "Office Name ";
         
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
        //#region "check version readonly or not"
        //string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        //if (app_status.Equals("True"))
        //{
        //    Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
        //    cv.MakeReadOnlyPage("Bill_Sys_ProcedureReport.aspx");
        //}
        //#endregion
    }

   

    //private void BindGrid()
    //{
    //    _reportBO = new Bill_Sys_ReportBO();
    //    try
    //    {
            
    //            grdAllReports.DataSource = _reportBO.GetProcedureReports("SP_REPORT_PROCEDURE_REPORT", txtFromDate.Text, txtToDate.Text, extddlOffice.Text, ddlStatus.SelectedValue, txtCompanyID.Text,"", null);
    //            grdAllReports.DataBind();


    //            grdForReport.DataSource = _reportBO.GetProcedureReports("SP_REPORT_PROCEDURE_REPORT", txtFromDate.Text, txtToDate.Text, extddlOffice.Text, ddlStatus.SelectedValue, txtCompanyID.Text,"", null);
    //            grdForReport.DataBind();
             

    //         if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == false)
    //         {
    //             grdAllReports.Columns[5].Visible = false;
    //             grdForReport.Columns[5].Visible = false;
    //         }
         
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //string str;
            //str = "Bill_Sys_ShowReport.aspx?StartDate=" + txtFromDate.Text + "&EndDate=" + txtToDate.Text + "&OfficeId=" + extddlOffice.Text + "&DocorId=" + extddlDoctor.Text + "&Status=" + ddlStatus.SelectedValue;
            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str.ToString() + "','Sign','toolbar=no,directories=no,menubar=yes,scrollbars=yes,status=no,resizable=yes,width=700,height=575'); ", true);
                if (chkShowReport.Checked == true&& chkMissingInfo.Checked==true)
            {
                int iFlag = 1;
                string CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                Bill_Sys_ReportBO _objRPO = new Bill_Sys_ReportBO();
                string Showpath = "";// _objRPO.GenrateHtmlForShowReport(CompanyID, txtFromDate.Text, txtToDate.Text, extddlOffice.Text, extddlDoctor.Text, ddlStatus.SelectedValue, iFlag);
                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_EMG_BILL == "True")
                {
                    Showpath = _objRPO.GenrateHtmlForShowReportEMG(CompanyID, txtFromDate.Text, txtToDate.Text, extddlOffice.Text, extddlDoctor.Text, ddlStatus.SelectedValue, iFlag);
                }
                else
                {

                    Showpath = _objRPO.GenrateHtmlForShowReport(CompanyID, txtFromDate.Text, txtToDate.Text, extddlOffice.Text, extddlDoctor.Text, ddlStatus.SelectedValue, iFlag);
                }

                string Missingpath = _objRPO.GenrateHtmlForMissingReport(CompanyID, extddlOffice.Text, iFlag);
                Bill_Sys_NF3_Template _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
              string szPhisicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + "Reports/SHOW_MISSINF.pdf";
              MergePDF.MergePDFFiles(Showpath, Missingpath, szPhisicalPath);
              string open_Path = ApplicationSettings.GetParameterValue("DocumentManagerURL") + "Reports/SHOW_MISSINF.pdf";
              Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + open_Path + "','Sign','toolbar=no,directories=no,menubar=yes,scrollbars=yes,status=no,resizable=yes,width=700,height=575'); ", true);
               
            }
            else if (chkShowReport.Checked == true)
            {
                int iFlag = 0;
                string CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                Bill_Sys_ReportBO _objRPO = new Bill_Sys_ReportBO();
                string path = "";
                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_EMG_BILL == "True")
                {
                     path = _objRPO.GenrateHtmlForShowReportEMG(CompanyID, txtFromDate.Text, txtToDate.Text, extddlOffice.Text, extddlDoctor.Text, ddlStatus.SelectedValue, iFlag);
                }
                else
                {

                     path = _objRPO.GenrateHtmlForShowReport(CompanyID, txtFromDate.Text, txtToDate.Text, extddlOffice.Text, extddlDoctor.Text, ddlStatus.SelectedValue, iFlag);
                }
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + path + "','Sign','toolbar=no,directories=no,menubar=yes,scrollbars=yes,status=no,resizable=yes,width=700,height=575'); ", true);
            }else if( chkMissingInfo.Checked=true)
            {
                int iFlag = 0; 
                string CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                Bill_Sys_ReportBO _objRPO = new Bill_Sys_ReportBO();
                string path = _objRPO.GenrateHtmlForMissingReport(CompanyID,extddlOffice.Text,iFlag);
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + path + "','Sign','toolbar=no,directories=no,menubar=yes,scrollbars=yes,status=no,resizable=yes,width=700,height=575'); ", true);

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
    //protected void grdAllReports_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    //{
    //    try
    //    {
    //        grdAllReports.CurrentPageIndex = e.NewPageIndex;
    //        BindGrid();
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    //private void ExportToExcel()
    //{
    //    try
    //    {
    //        StringBuilder strHtml = new StringBuilder();
    //        strHtml.Append("<table border='1px'>");
    //        for (int icount = 0; icount < grdForReport.Items.Count; icount++)
    //        {
    //            if (icount == 0)
    //            {
    //                strHtml.Append("<tr>");
    //                for (int i = 0; i < grdForReport.Columns.Count; i++)
    //                {
    //                    if (grdForReport.Columns[i].Visible == true)
    //                    {
    //                        strHtml.Append("<td>");
    //                        strHtml.Append(grdForReport.Columns[i].HeaderText);
    //                        strHtml.Append("</td>");
    //                    }
    //                }

    //                strHtml.Append("</tr>");
    //            }

    //            strHtml.Append("<tr>");
    //            for (int j = 0; j < grdForReport.Columns.Count; j++)
    //            {
    //                if (grdForReport.Columns[j].Visible == true)
    //                {
    //                    strHtml.Append("<td>");
    //                    strHtml.Append(grdForReport.Items[icount].Cells[j].Text);
    //                    strHtml.Append("</td>");
    //                }
    //            }
    //            strHtml.Append("</tr>");

    //        }
    //        strHtml.Append("</table>");
    //        string filename = getFileName("EXCEL") + ".xls";
    //        StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
    //        sw.Write(strHtml);
    //        sw.Close();

    //        Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);


    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Error/PFS_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}
    //private string getFileName(string p_szBillNumber)
    //{
    //    String szBillNumber = "";
    //    szBillNumber = p_szBillNumber;
    //    String szFileName;
    //    DateTime currentDate;
    //    currentDate = DateTime.Now;
    //    szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
    //    return szFileName;
    //}
    //private string getRandomNumber()
    //{
    //    System.Random objRandom = new Random();
    //    return objRandom.Next(1, 10000).ToString();
    //}
    //protected void btnExportToExcel_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ExportToExcel();
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Error/PFS_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}
   
}
