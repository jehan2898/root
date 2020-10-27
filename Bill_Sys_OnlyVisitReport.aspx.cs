using AjaxControlToolkit;
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

public partial class Bill_Sys_OnlyVisitReport : Page, IRequiresSessionState
{

    private Bill_Sys_ReportBO _reportBO;

    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;

    private ArrayList objAL;


    public Bill_Sys_OnlyVisitReport()
    {
    }

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
        decimal num = new decimal(0);
        this.objAL = new ArrayList();
        try
        {
            this.objAL.Add(this.txtCompanyID.Text);
            this.objAL.Add(this.extddlDoctor.Text);
            this.objAL.Add(this.txtFromDate.Text);
            this.objAL.Add(this.txtToDate.Text);
            this.objAL.Add("");
            this.objAL.Add("");
            this.objAL.Add(this.extddlSpeciality.Text);
            this.objAL.Add(this.extddlLocation.Text);
            DataSet dataSet = new DataSet();
            dataSet = this._bill_Sys_Visit_BO.getVisitReport(this.objAL);
            this.grdAllReports.DataSource = dataSet.Tables[0];
            this.grdAllReports.DataBind();
            Label str = this.lblCountVlaues;
            int count = dataSet.Tables[0].Rows.Count;
            str.Text = count.ToString();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                num = num + Convert.ToDecimal(dataSet.Tables[0].Rows[i]["total_Amount"]);
            }
            this.lblTotAmtValue.Text = string.Concat("$", num.ToString());
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
            stringBuilder.Append(string.Concat("Total Amount : ", this.lblTotAmtValue.Text, "<br>"));
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

    protected void grdAllReports_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdAllReports.CurrentPageIndex = e.NewPageIndex;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            if (!base.IsPostBack)
            {
                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
                {
                    this.extddlLocation.Visible = true;
                    this.lblLocationName.Visible = true;
                    this.extddlLocation.Flag_ID = this.txtCompanyID.Text.ToString();
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
        
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_Sys_OnlyVisitReport.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}