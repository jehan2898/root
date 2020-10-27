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
using System.Text;
using System.Collections.Generic;
using System.Drawing;

public partial class Bill_Sys_ShowUnSentNF2 : PageBase
{
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
     private Bill_Sys_Case _bill_Sys_Case;
    private Bill_Sys_ReportBO _reportBO;
    private Bill_Sys_UserObject oC_UserObject = null;
    private Bill_Sys_BillingCompanyObject oC_Account = null;
    private Bill_Sys_SystemObject oC_SystemObject = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        btnSendMail.Attributes.Add("onclick", "return Confirm_Send_Mail();");
        try
        {
            oC_UserObject = (Bill_Sys_UserObject)(Session["USER_OBJECT"]);
            oC_Account = (Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"]);
            oC_SystemObject = (Bill_Sys_SystemObject)(Session["SYSTEM_OBJECT"]);

            if (oC_Account == null || oC_UserObject == null || oC_SystemObject == null)
            {
                DisableInput();
                lblMessage.Text = "Oops! Your session has expired. You must re-login to proceed !";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            if (!IsPostBack)
            {
                txtCompanyID.Text = oC_Account.SZ_COMPANY_ID;
                //setLabels();
                if ((oC_SystemObject.SZ_LOCATION == "1"))
                {
                    extddlLocation.Visible = true;
                    lblLocationName.Visible = true;
                    
                    extddlLocation.Flag_ID = txtCompanyID.Text.ToString();
                }
                extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
                //call to procedure
               /// extddlCaseType.Text=
                _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();

                    extddlCaseType.Text = _bill_Sys_BillingCompanyDetails_BO.getNF2CaseType(txtCompanyID.Text);
                
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
       
        #region "check version readonly or not"
        string app_status = oC_Account.SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_ShowUnSentNF2.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();


            if (extddlLocation.Visible == true && extddlLocation.Text != "" && extddlLocation.Text != "NA")
            {
                 
                    grdUnsentNF2.DataSource = _bill_Sys_BillingCompanyDetails_BO.getUnsentNF2(txtCompanyID.Text, ddlStatus.SelectedValue, extddlLocation.Text,extddlCaseType.Text);
            }
            else
            {

                grdUnsentNF2.DataSource = _bill_Sys_BillingCompanyDetails_BO.getUnsentNF2(txtCompanyID.Text, ddlStatus.SelectedValue, null, extddlCaseType.Text);
            }
            
            grdUnsentNF2.DataBind();
            grdUnsentNF2.VirtualItemCount = _bill_Sys_BillingCompanyDetails_BO.iSearchRecordCount;
            lblCount.Text = "Total Records: " + _bill_Sys_BillingCompanyDetails_BO.iSearchRecordCount.ToString();
            if (ddlStatus.SelectedValue == "0")
            {
                grdUnsentNF2.Columns[0].Visible = false;
            }
            else
            {
                grdUnsentNF2.Columns[0].Visible = true;
            }
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
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


    #region "Bind Label For Dash Board"
    protected void setLabels()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO _obj = new DashBoardBO();
       Bill_Sys_BillTransaction_BO _billTransactionBO = new Bill_Sys_BillTransaction_BO();
        try
        {
            DayOfWeek day = Convert.ToDateTime(System.DateTime.Today.ToString()).DayOfWeek;
            int days = day - DayOfWeek.Sunday;

            DateTime start = Convert.ToDateTime(System.DateTime.Today.ToString()).AddDays(-days);
            DateTime end = start.AddDays(6);

            lblAppointmentToday.Text = _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "GET_APPOINTMENT");
            lblAppointmentWeek.Text = _obj.getAppoinmentCount(start.ToString(), end.ToString(), txtCompanyID.Text, "GET_APPOINTMENT");

            lblBillStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=Paid' onclick=\"javascript:OpenPage('Paid');\" > " + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_PAID_LIST_COUNT", txtCompanyID.Text) + "</a>";
            lblBillStatus.Text += " Paid Bills  </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=UnPaid' onclick=\"javascript:OpenPage('UnPaid');\" > " + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_UNPAID_LIST_COUNT", txtCompanyID.Text) + "</a> Un-Paid Bills </li></ul>";

            lblDesk.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_LitigationDesk.aspx?Type=Litigation' onclick=\"javascript:OpenPage('Litigation');\" > " + _billTransactionBO.GetCaseCount("SP_LITIGATION_WRITEOFF_DESK", "GET_LETIGATION_COUNT", txtCompanyID.Text) + "</a>" + " bills due for litigation";

            //lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=MissingProvider' onclick=\"javascript:OpenPage('MissingProvider');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_PROVIDER") + "</a>";
            //lblMissingInformation.Text += " provider information missing  </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
            lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
            lblMissingInformation.Text += " insurance company missing </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_ATTORNEY") + "</a>";
            lblMissingInformation.Text += " attorney missing </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_CLAIM_NUMBER") + "</a> claim number missing </li>";
            lblMissingInformation.Text += "<li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingReportNumber' onclick=\"javascript:OpenPage('MissingReportNumber');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_REPORT_NUMBER") + "</a> report number missing </li>";
            lblMissingInformation.Text += "<li> <a href='Bill_Sys_ShowUnSentNF2.aspx' > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "UNSENTNF2") + "</a> unsent NF2 </li></ul>";
            
            lblReport.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=R' onclick=\"javascript:OpenPage('Litigation');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_RECEIVED_COUNT") + "</a>" + " Received Report";
            lblReport.Text += "</li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=P' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_PENDING_COUNT") + "</a> Pending Report </li></ul>";

            lblProcedureStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li>" + _obj.getBilledUnbilledProcCode(txtCompanyID.Text, "GET_BILLEDPROC") + " billed procedure codes";
            lblProcedureStatus.Text += "</li>  <li>" + _obj.getBilledUnbilledProcCode(txtCompanyID.Text, "GET_UNBILLEDPROC") + " Un-billed procedure codes </li></ul>";


            lblVisits.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_VISIT_COUNT") + " Visits</li>";
            lblVisits.Text += "<li>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_BILLED_VISIT_COUNT") + " Billed visits </li>";
            lblVisits.Text += "<li>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_UNBILLED_VISIT_COUNT") + " Un-billed visits </li></ul>";


            ConfigDashBoard();

        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
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
    private void ConfigDashBoard()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO _objDashBoardBO = new DashBoardBO();
        try
        {

            DataTable dt = _objDashBoardBO.GetConfigDashBoard(oC_UserObject.SZ_USER_ROLE);
            foreach (DataRow dr in dt.Rows)
            {
                switch (dr[0].ToString())
                {
                    case "Daily Appointment":
                        tblDailyAppointment.Visible = true;
                        break;
                    case "Weekly Appointment":
                        tblWeeklyAppointment.Visible = true;
                        break;
                    case "Bill Status":
                        tblBillStatus.Visible = true;
                        break;
                    case "Desk":
                        tblDesk.Visible = true;
                        break;
                    case "Missing Information":
                        tblMissingInfo.Visible = true;
                        break;
                    case "Report Section":
                        tblReportSection.Visible = true;
                        break;
                    case "Procedure Status":
                        tblBilledUnbilledProcCode.Visible = true;
                        break;
                    case "Visits":
                        tblVisits.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
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
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
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
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string pdffile = GeneratePDF();
            if (pdffile != "")
            {
                string szDefaultPath = ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET");
                string szPDFName = "";
                szPDFName = szDefaultPath + pdffile;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFName.ToString() + "'); ", true);
            }
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
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

    protected string GenerateHTML()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string genhtml = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";//</td><td><b>Patient Name</b></td><td><b>Insurance Company</b></td><td><b>Insurance Address</b></td><td><b>Claim No</b></td><td><b>Date Of Accident</b></td></tr>";
        try
        {
            int i = 0;
            foreach (DataGridItem dg in grdUnsentNF2.Items)
            {
                if (((CheckBox)dg.Cells[0].FindControl("ChkSent")).Checked == true)
                {
                    genhtml += "<tr><td style='font-size:9px'>" + (i+1).ToString() + "</td><td style='font-size:9px'>" + dg.Cells[8].Text + "</td><td style='font-size:9px'><b>" + dg.Cells[4].Text + "</b><br/>" + dg.Cells[9].Text + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + dg.Cells[1].Text + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + dg.Cells[3].Text + "</td></tr>";
                    //genhtml += "<tr><td>" + dg.Cells[2].Text + "</td><td>" + dg.Cells[3].Text + "</td><td>" + dg.Cells[8].Text + "</td><td>" + dg.Cells[7].Text + "</td><td>" + dg.Cells[4].Text + "</td></tr>";
                    i = i + 1;
                    Session["VL_COUNT"] = i;
                }

            }
            genhtml += "</table>";
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
       
        return genhtml;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected string GeneratePDF()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
        string pdffilename = "";
        try
        {
            string szFileData = File.ReadAllText(ConfigurationManager.AppSettings["NF2_SENT_MAIL_HTML"]);
            szFileData = objPDF.getNF2MailDetails(szFileData, oC_Account.SZ_COMPANY_ID);
            string strHtml = GenerateHTML();
            szFileData = szFileData.Replace("VL_SZ_TABLE_DATA", strHtml);
            szFileData = szFileData.Replace("VL_SZ_CASE_COUNT", Session["VL_COUNT"].ToString());
            SautinSoft.PdfMetamorphosis objHTMToPDF = new SautinSoft.PdfMetamorphosis();
            objHTMToPDF.Serial = "10007706603";
            string htmfilename = getFileName("P") + ".htm";
            pdffilename = getFileName("P") + ".pdf";
            StreamWriter sw = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + htmfilename);
            sw.Write(szFileData);
            sw.Close();
            Int32 iTemp;
            iTemp = objHTMToPDF.HtmlToPdfConvertFile(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + htmfilename, ApplicationSettings.GetParameterValue("EXCEL_SHEET") + pdffilename);
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        
        return pdffilename;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    private void ExportToExcel()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdUnsentNF2.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdUnsentNF2.Columns.Count; i++)
                    {
                        if (grdUnsentNF2.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdUnsentNF2.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdUnsentNF2.Columns.Count; j++)
                {
                    if (grdUnsentNF2.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        if (j == 1)
                        {
                            strHtml.Append(grdUnsentNF2.Items[icount].Cells[11].Text);

                        }

                        else
                        {

                            strHtml.Append(grdUnsentNF2.Items[icount].Cells[j].Text);
                        }
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET")+ filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + filename, false);


        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
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
   
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ExportToExcel();
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
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
    //protected void grdUnsentNF_ItemCommand(object source, DataGridCommandEventArgs e)
    //{
    //    try
    //    {

    //        if (e.CommandName.ToString() == "Select")
    //        {

    //            CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
    //            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
    //            _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
    //            _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
    //            _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[3].Text;
    //            _bill_Sys_CaseObject.SZ_CASE_NO= ((LinkButton)e.Item.FindControl("lnkSelectCase2")).Text;
    //            Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

    //            _bill_Sys_Case = new Bill_Sys_Case();
    //            _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

    //            Session["CASEINFO"] = _bill_Sys_Case;
    //            Response.Redirect("AJAX PAGES/Bill_Sys_CaseDetails.aspx", false);
    //        }

           
           
           
    //    }
    //    catch (Exception ex)
    //    {
    //        //string strError = ex.Message.ToString();
    //        //strError = strError.Replace("\n", " ");
    //        this.usrMessage.PutMessage(ex.ToString());
    //        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
    //        this.usrMessage.Show();
    //        this.lblMsg.Text = ex.ToString();
    //        this.lblMsg.Visible = true;
    //    }
    //}

    private void DisableInput()
    {
        List<Control> lstControl = new List<Control>();
        IterateFormControls(lstControl, this.Page);
        foreach (Control c in lstControl)
        {
            if (c.GetType() == typeof(TextBox))
            {
                TextBox t = (TextBox)c;
                t.Enabled = false;
            }

            if (c.GetType() == typeof(Button))
            {
                Button b = (Button)c;
                b.Enabled = false;
            }

            if (c.GetType() == typeof(RadioButton))
            {
                RadioButton r = (RadioButton)c;
                r.Enabled = false;
            }

            if (c.GetType() == typeof(GridView))
            {
                GridView g = (GridView)c;
                g.Enabled = false;
            }
        }
    }

    private void IterateFormControls(List<Control> lstControl, Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c is Control)
            {
                lstControl.Add(c);

                if (c.Controls.Count > 0)
                {
                    IterateFormControls(lstControl, c);
                }
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        for (int i = 0; i < grdUnsentNF2.Items.Count; i++)
        {
            if (oC_Account.BT_REFERRING_FACILITY)
            {
                LinkButton button = (LinkButton)this.grdUnsentNF2.Items[i].Cells[2].FindControl("lnkSelectRCase");
                button.Visible = true;
                grdUnsentNF2.Columns[2].Visible = true;
            }
            else
            {
                LinkButton button = (LinkButton)this.grdUnsentNF2.Items[i].Cells[1].FindControl("lnkSelectCase");
                button.Visible = true;
                grdUnsentNF2.Columns[1].Visible = true;
            }
        }
    }
}
