using AjaxControlToolkit;
using ASP;
using ExtendedDropDownList;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bill_Sys_BillReport : Page, IRequiresSessionState
{

    

    private Bill_Sys_ReportBO _reportBO;

    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;

    private ArrayList objAL;

   

    public Bill_Sys_BillReport()
    {
    }

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._reportBO = new Bill_Sys_ReportBO();
        this.objAL = new ArrayList();
        int num = 0;
        try
        {
            this.objAL.Add(this.txtCompanyID.Text);
            this.objAL.Add(this.extddlBillStatus.Text);
            this.objAL.Add(this.extddlSpeciality.Text);
            this.objAL.Add(this.txtFromDate.Text);
            this.objAL.Add(this.txtToDate.Text);
            this.objAL.Add(this.extddlLocation.Text);
            this.objAL.Add(this.lstProcedureCode.SelectedValue);
            this.objAL.Add(this.extddlDoctor.Text);
            if (this.chkVerificationsent.Checked)
            {
                this.objAL.Add("VS");
                num++;
            }
            if (this.chkVerificationreceived.Checked)
            {
                this.objAL.Add("VR");
                num++;
            }
            if (this.chkdenail.Checked)
            {
                this.objAL.Add("DEN");
                num++;
            }
            if (this.chkPaidfull.Checked)
            {
                this.objAL.Add("FBP");
                num++;
            }
            this.Session["SZ_PROCEDURE_ID"] = this.lstProcedureCode.SelectedValue;
            DataSet dataSet = new DataSet();
            if (num == 1)
            {
                dataSet = this._reportBO.getBillReportSelect(this.objAL, "SP_GET_BILL_REPORT_SELECT");
            }
            else if (num == 4)
            {
                dataSet = this._reportBO.getBillReportSelect(this.objAL, "SP_GET_BILL_REPORT_ALL");
            }
            else if (num != 3)
            {
                dataSet = (num != 2 ? this._reportBO.getBillReport(this.objAL) : this._reportBO.getBillReportTwo(this.objAL));
            }
            else
            {
                dataSet = this._reportBO.getBillReportThree(this.objAL);
            }
            this.grdAllReports.DataSource = dataSet.Tables[0];
            this.grdAllReports.DataBind();
            decimal num1 = new decimal(0);
            foreach (DataGridItem item in this.grdAllReports.Items)
            {
                if (item.Cells[4].Text.Trim() == "&nbsp;")
                {
                    continue;
                }
                num1 = num1 + Convert.ToDecimal(item.Cells[4].Text);
            }
            this.lblTotal.Text = num1.ToString();
            if (!this.extddlLocation.Visible || !(this.extddlLocation.Text != "") || !(this.extddlLocation.Text != "NA"))
            {
                this.Session["sz_Location_Id"] = "";
            }
            else
            {
                this.Session["sz_Location_Id"] = this.extddlLocation.Text;
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

    public void BindProcedureCodeList(object sender, EventArgs e)
    {
        Bill_Sys_ReferalEvent billSysReferalEvent = new Bill_Sys_ReferalEvent();
        DataSet dataSet = new DataSet();
        string text = this.txtCompanyID.Text;
        string str = this.extddlSpeciality.Text;
        dataSet = billSysReferalEvent.GetProcedureCodeAndDescription(this.extddlSpeciality.Text, text);
        this.lstProcedureCode.DataSource = dataSet.Tables[0];
        this.lstProcedureCode.DataTextField = "DESCRIPTION";
        this.lstProcedureCode.DataValueField = "CODE";
        this.lstProcedureCode.DataBind();
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
            this.ExportToExcel();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.BindGrid();
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

    private void ExportToExcel()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<table border='1px'>");
            for (int i = 0; i < this.grdAllReports.Items.Count; i++)
            {
                if (i == 0)
                {
                    stringBuilder.Append("<tr>");
                    for (int j = 0; j < this.grdAllReports.Columns.Count; j++)
                    {
                        if (this.grdAllReports.Columns[j].Visible)
                        {
                            stringBuilder.Append("<td>");
                            stringBuilder.Append(this.grdAllReports.Columns[j].HeaderText);
                            stringBuilder.Append("</td>");
                        }
                    }
                    stringBuilder.Append("</tr>");
                }
                stringBuilder.Append("<tr>");
                for (int k = 0; k < this.grdAllReports.Columns.Count; k++)
                {
                    if (this.grdAllReports.Columns[k].Visible)
                    {
                        stringBuilder.Append("<td>");
                        stringBuilder.Append(this.grdAllReports.Items[i].Cells[k].Text);
                        stringBuilder.Append("</td>");
                    }
                }
                stringBuilder.Append("</tr>");
            }
            stringBuilder.Append("</table>");
            string str = string.Concat(this.getFileName("EXCEL"), ".xls");
            StreamWriter streamWriter = new StreamWriter(string.Concat(ConfigurationManager.AppSettings["EXCEL_SHEET"], str));
            streamWriter.Write(stringBuilder);
            streamWriter.Close();
            base.Response.Redirect(string.Concat(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"], str), false);
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
        DateTime now = DateTime.Now;
        string[] pSzBillNumber = new string[] { p_szBillNumber, "_", this.getRandomNumber(), "_", now.ToString("yyyyMMddHHmmssms") };
        return string.Concat(pSzBillNumber);
    }

    private string getRandomNumber()
    {
        return (new Random()).Next(1, 10000).ToString();
    }

    protected void grdAllReports_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "View")
            {
                this.Session["sz_StatusID"] = e.Item.Cells[5].Text;
                this.Session["sz_DoctorID"] = e.Item.Cells[6].Text;
                base.Response.Redirect("Bill_Sys_ViewBillRecordDetails.aspx", false);
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

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!base.IsPostBack)
            {
                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlBillStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
                {
                    this.extddlLocation.Visible = true;
                    this.lblLocationName.Visible = true;
                    this.extddlLocation.Flag_ID = this.txtCompanyID.Text.ToString();
                }
                this.BindGrid();
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
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_Sys_BillReport.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}