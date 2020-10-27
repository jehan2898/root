
/***********************************************************/
/*Project Name         :       Medical Billing System
/*Description          :       sort visite time
/*Author               :       Sandeep Y
/*Date of creation     :       15 Dec 2008
/*Modified By (Last)   :       Sandeep Yadav
/*Modified By (S-Last) :       Atul Jadhav 
/*Modified Date        :       13 May 2010 
/*Last Change          :       To integrate AJAX on out schedule calendar page we need to move all pages which are under
 *                             calendare menu to AJAX Pages folder. Change control name from javascript and file 
 *                             path from .cs file.
/************************************************************/


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
using Componend;
using System.IO;
using System.Text;
public partial class Bill_Sys_OutScheduleReport : PageBase
{
    #region "Variables"
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private string UserId;
    Bill_Sys_BillTransaction_BO _billTransactionBO;
    static int iFlage = 0; //flag add for making diffrace between sort and search
    #endregion

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        UserId =((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        iFlage = 0;
        ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        try
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
               txtChartNO.Attributes.Add("onkeypress", "return clickButton1(event,'/')");
              
            }
            extddlReferringFacility.Flag_ID = txtCompanyID.Text.ToString();
            Bill_Sys_BillingCompanyDetails_BO objOffID = new Bill_Sys_BillingCompanyDetails_BO();
            string sz_Off_Id = objOffID.GetRefDocID(txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
            if (sz_Off_Id != "")
            {
                extddlOffice.Text = sz_Off_Id;
                extddlOffice.Enabled = false;
                extddlDoctor.Flag_Key_Value="GET_REFERRINGOFF_DOCTORLIST";
                extddlDoctor.Flag_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            }
            else
            {
                extddlDoctor.Flag_Key_Value="GETDOCTORLIST";
                extddlDoctor.Flag_ID = txtCompanyID.Text;
            }
                extddlOffice.Flag_ID = txtCompanyID.Text.ToString();
                
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
            {
                txtChartNO.Visible = false;
                lblChartNO.Visible = false;
            }
            else
            {
                txtChartNO.Visible = true;
                lblChartNO.Visible = true;
                

            }

            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                if(sz_Off_Id!="")
                {
                    extddlDoctor.Flag_Key_Value="GET_REFERRINGOFF_DOCTORLIST";
                    extddlDoctor.Flag_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                }
                else
                {
                    extddlDoctor.Flag_Key_Value = "GET_DOCTOR_LIST_TXNCALENDAR";
                extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                extddlReferringFacility.Visible = false;
                lblTestFacility.Visible = false;
                
            }
            else
            {
                extddlOffice.Visible = false;
                lblOffice.Visible = false;
                extddlDoctor.Flag_Key_Value = "GETDOCTORLIST";
                extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            if (!Page.IsPostBack)
            {
                
              //  BindGrid();
              
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
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_OutScheduleReport.aspx");
        }
        #endregion

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    #endregion

    #region "Event Hanlder"


    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           
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
   
    protected void grdScheduleReport_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdScheduleReport.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
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

   
    #endregion

    #region "Fetch Method"  
  
    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        ScheduleReportBO _objSR = new ScheduleReportBO();
        try
        {
            DataSet ds ;
            ds = null;
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {

                ds= _objSR.getCalenderOutScheduleReportNEW(txtStartDate.Text, txtEndDate.Text, txtCompanyID.Text, "", extddlDoctor.Text, ddlStatus.SelectedValue, extddlOffice.Text, txtSort.Text,txtPatientName.Text,txtCaseNo.Text,txtChartNO.Text,UserId);
                
            }
            else
            {
                if (extddlReferringFacility.Text != "NA")
                {
                    ds= _objSR.getCalenderOutScheduleReportNEW(txtStartDate.Text, txtEndDate.Text, txtCompanyID.Text, extddlReferringFacility.Text, extddlDoctor.Text, ddlStatus.SelectedValue, extddlOffice.Text, txtSort.Text,txtPatientName.Text,txtCaseNo.Text,txtChartNO.Text,UserId);
                    
                }
                else
                {
                    ds= _objSR.getCalenderOutScheduleReportNEW(txtStartDate.Text, txtEndDate.Text, txtCompanyID.Text, "", extddlDoctor.Text, ddlStatus.SelectedValue, extddlOffice.Text, txtSort.Text,txtPatientName.Text,txtCaseNo.Text,txtChartNO.Text,UserId);
                    
                }
            }

          //  grdScheduleReport.CurrentPageIndex = 0;
           // grdForReport.CurrentPageIndex = 0;

            ///sording date descending at page laod
            #region "sort visite date for page load"
            if (ds!=null)
            {
                if (iFlage==0)
                {
                   if(ds.Tables[0].Rows.Count>0)
                    {
                        DataTable sourceTable = (DataTable)ds.Tables[0];
                        DataView view = new DataView(sourceTable);

                        view.Sort = "VISIT DESC";
                        grdScheduleReport.DataSource = view;
                        grdForReport.DataSource = view;
                        grdScheduleReport.DataBind();
                        grdForReport.DataBind();

                    }
                    else
                    {
                        grdScheduleReport.DataSource = ds;
                        grdForReport.DataSource = ds;
                        grdScheduleReport.DataBind();
                        grdForReport.DataBind();
                    }
                }
                else
                {
                    grdScheduleReport.DataSource = ds;
                    grdForReport.DataSource = ds;
                    grdScheduleReport.DataBind();
                    grdForReport.DataBind();
                }
            }
            #endregion
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
            {
                grdScheduleReport.Columns[1].Visible = false;
                grdForReport.Columns[0].Visible = false;
            }
            //manipulateColumns();
            //make();
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                
            }
            else
            {
                grdScheduleReport.Columns[0].Visible = false;
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

    protected void manipulateColumns()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (Request.QueryString["Type"] != null)
            {
                if (Request.QueryString["Type"].ToString() != "NoShow")
                {
                    foreach (DataGridItem  objDGI in grdScheduleReport.Items)
                    {
                        if(objDGI.Cells[8].Text.ToString() == "No Show")
                            objDGI.Visible = false;
                    }
                }

                if (Request.QueryString["Type"].ToString() != "Scheduled")
                {
                    foreach (DataGridItem objDGI in grdScheduleReport.Items)
                    {
                        if (objDGI.Cells[8].Text.ToString() == "Scheduled")
                            objDGI.Visible = false;
                    }
                }

                if (Request.QueryString["Type"].ToString() != "Done")
                {
                    foreach (DataGridItem objDGI in grdScheduleReport.Items)
                    {
                        if (objDGI.Cells[8].Text.ToString() == "Done")
                            objDGI.Visible = false;
                    }
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion

    #region "Generate Report"
    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        txtSort.Text = "";
        grdScheduleReport.CurrentPageIndex = 0;
        grdForReport.CurrentPageIndex = 0;
        try
        {
            BindGrid();
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
    #endregion

    #region "Export to Excel"
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
            for (int icount = 0; icount < grdForReport.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdForReport.Columns.Count; i++)
                    {
                        if (grdForReport.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdForReport.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdForReport.Columns.Count; j++)
                {
                    if (grdForReport.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdForReport.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ApplicationSettings.GetParameterValue( "EXCEL_SHEET") + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + filename, false);


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
    #endregion

    #region "NO USE"
    //#region "Bind Label For Dash Board"
    //protected void setLabels()
    //{
    //    DashBoardBO _obj = new DashBoardBO();
    //    _billTransactionBO = new Bill_Sys_BillTransaction_BO();
    //    try
    //    {
    //        DayOfWeek day = Convert.ToDateTime(System.DateTime.Today.ToString()).DayOfWeek;
    //        int days = day - DayOfWeek.Sunday;

    //        DateTime start = Convert.ToDateTime(System.DateTime.Today.ToString()).AddDays(-days);
    //        DateTime end = start.AddDays(6);

    //        lblAppointmentToday.Text = _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "GET_APPOINTMENT");
    //        lblAppointmentWeek.Text = _obj.getAppoinmentCount(start.ToString(), end.ToString(), txtCompanyID.Text, "GET_APPOINTMENT");

    //        lblBillStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=Paid' onclick=\"javascript:OpenPage('Paid');\" > " + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_PAID_LIST_COUNT", txtCompanyID.Text) + "</a>";
    //        lblBillStatus.Text += " Paid Bills  </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=UnPaid' onclick=\"javascript:OpenPage('UnPaid');\" > " + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_UNPAID_LIST_COUNT", txtCompanyID.Text) + "</a> Un-Paid Bills </li></ul>";

    //        lblDesk.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_LitigationDesk.aspx?Type=Litigation' onclick=\"javascript:OpenPage('Litigation');\" > " + _billTransactionBO.GetCaseCount("SP_LITIGATION_WRITEOFF_DESK", "GET_LETIGATION_COUNT", txtCompanyID.Text) + "</a>" + " bills due for litigation";

    //        lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=MissingProvider' onclick=\"javascript:OpenPage('MissingProvider');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_PROVIDER") + "</a>";
    //        lblMissingInformation.Text += " provider information missing  </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
    //        lblMissingInformation.Text += " insurance company missing </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_ATTORNEY") + "</a>";
    //        lblMissingInformation.Text += " attorney missing </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_CLAIM_NUMBER") + "</a> claim number missing </li></ul>";
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}
    //#endregion
    #endregion

    #region "grdScheduleReport Grid Event"
    protected void grdScheduleReport_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdScheduleReport_ItemCommand(object source, DataGridCommandEventArgs e)
    {
       
        if (e.CommandName.ToString() == "OfficeSearch")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "ChartNoSearch")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + " DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "CaseNoSearch")
        {
          
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + " DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "PatientNameSearch")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + " DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "VisteDateSearch")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + " DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        iFlage = 1;
        BindGrid();
    }
    #endregion
}
