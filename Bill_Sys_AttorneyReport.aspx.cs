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

public partial class Bill_Sys_AttorneyReport : Page, IRequiresSessionState
{

    private Bill_Sys_ReportBO _reportBO;

    private CaseDetailsBO _objCaseDetailsBO = new CaseDetailsBO();
    public Bill_Sys_AttorneyReport()
    {
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
            DataSet dataSet = new DataSet();
            ArrayList arrayLists = new ArrayList();
            arrayLists.Add(this.txtCompanyID.Text);
            arrayLists.Add(this.txtFromDate.Text);
            arrayLists.Add(this.txtPatientName.Text.ToString().Trim());
            arrayLists.Add(this.txtCaseNo.Text.ToString().Trim());
            arrayLists.Add(this.extddlAttorney.Text);
            arrayLists.Add(this.extddlCaseStatus.Text);
            arrayLists.Add(this.extddlCaseType.Text);
            arrayLists.Add(this.extddlInsuranceCompany.Text);
            this._reportBO = new Bill_Sys_ReportBO();
            dataSet = this._reportBO.Get_Attorney_Report(arrayLists);
            this.grdPayment.DataSource = dataSet;
            this.grdPayment.DataBind();
            this.grdExcel.DataSource = dataSet;
            this.grdExcel.DataBind();
            this.Session["DataBind"] = dataSet;
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

    protected void btnSearch_Click1(object sender, EventArgs e)
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
            for (int i = 0; i < this.grdExcel.Items.Count; i++)
            {
                if (i == 0)
                {
                    stringBuilder.Append("<tr>");
                    for (int j = 0; j < this.grdExcel.Columns.Count; j++)
                    {
                        if (this.grdExcel.Columns[j].Visible)
                        {
                            stringBuilder.Append("<td>");
                            stringBuilder.Append(this.grdExcel.Columns[j].HeaderText);
                            stringBuilder.Append("</td>");
                        }
                    }
                    stringBuilder.Append("</tr>");
                }
                stringBuilder.Append("<tr>");
                for (int k = 0; k < this.grdExcel.Columns.Count; k++)
                {
                    if (this.grdExcel.Columns[k].Visible)
                    {
                        stringBuilder.Append("<td>");
                        stringBuilder.Append(this.grdExcel.Items[i].Cells[k].Text);
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

    protected void grdPayment_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        DataSet dataSet = new DataSet();
        dataSet = (DataSet)this.Session["DataBind"];
        DataView defaultView = dataSet.Tables[0].DefaultView;
        if (e.CommandName.ToString() == "Patient Name")
        {
            if (this.txtSort.Text != string.Concat(e.CommandArgument, " ASC"))
            {
                this.txtSort.Text = string.Concat(e.CommandArgument, " ASC");
            }
            else
            {
                this.txtSort.Text = string.Concat(e.CommandArgument, "  DESC");
            }
        }
        else if (e.CommandName.ToString() == "Case No")
        {
            if (this.txtSort.Text != string.Concat(e.CommandArgument, " ASC"))
            {
                this.txtSort.Text = string.Concat(e.CommandArgument, " ASC");
            }
            else
            {
                this.txtSort.Text = string.Concat(e.CommandArgument, " DESC");
            }
        }
        else if (e.CommandName.ToString() == "Inasurance Name")
        {
            if (this.txtSort.Text != string.Concat(e.CommandArgument, " ASC"))
            {
                this.txtSort.Text = string.Concat(e.CommandArgument, " ASC");
            }
            else
            {
                this.txtSort.Text = string.Concat(e.CommandArgument, " DESC");
            }
        }
        else if (e.CommandName.ToString() == "Attorney Name")
        {
            if (this.txtSort.Text != string.Concat(e.CommandArgument, " ASC"))
            {
                this.txtSort.Text = string.Concat(e.CommandArgument, " ASC");
            }
            else
            {
                this.txtSort.Text = string.Concat(e.CommandArgument, " DESC");
            }
        }
        else if (e.CommandName.ToString() == "Accident Date")
        {
            if (this.txtSort.Text != string.Concat(e.CommandArgument, " ASC"))
            {
                this.txtSort.Text = string.Concat(e.CommandArgument, " ASC");
            }
            else
            {
                this.txtSort.Text = string.Concat(e.CommandArgument, " DESC");
            }
        }
        defaultView.Sort = this.txtSort.Text;
        this.grdPayment.DataSource = defaultView;
        this.grdPayment.DataBind();
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
                this.extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlCaseStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlAttorney.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlInsuranceCompany.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                string caseSatusId = this._objCaseDetailsBO.GetCaseSatusId(this.txtCompanyID.Text);
                this.extddlCaseStatus.Text = caseSatusId;
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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}