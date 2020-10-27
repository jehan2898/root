using AjaxControlToolkit;
using DevExpress.Web;
using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_Bill_Sys_Transfer_Report : Page, IRequiresSessionState
{


    private string lawfirmIDs = "";


    public AJAX_Pages_Bill_Sys_Transfer_Report()
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
            string str = "";
            for (int i = 0; i < this.grdLawFirm.VisibleRowCount; i++)
            {
                GridViewDataColumn item = (GridViewDataColumn)this.grdLawFirm.Columns[0];
                CheckBox checkBox = (CheckBox)this.grdLawFirm.FindRowCellTemplateControl(i, item, "chkall1");
                if (checkBox != null && checkBox.Checked)
                {
                    ASPxGridView aSPxGridView = this.grdLawFirm;
                    string[] strArrays = new string[] { "CODE" };
                    string str1 = aSPxGridView.GetRowValues(i, strArrays).ToString();
                    str = string.Concat(str, ",", str1);
                }
            }
            if (str != "")
            {
                str = str.Remove(0, 1);
            }
            string str2 = this.txtFromDate.Text.ToString();
            string str3 = this.txtToDate.Text.ToString();
            Bill_Sys_Transfer_Report billSysTransferReport = new Bill_Sys_Transfer_Report();
            DataSet dataSet = new DataSet();
            dataSet = billSysTransferReport.getTransferedCaseDetail(this.txtCompanyID.Text, str, str2, str3);
            this.grdTransfer.DataSource = dataSet;
            this.grdTransfer.DataBind();
            this.Session["TransferGridData"] = dataSet;
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

    private void bindLawFirmList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Transfer_Report billSysTransferReport = new Bill_Sys_Transfer_Report();
        try
        {
            DataSet dataSet = new DataSet();
            dataSet = billSysTransferReport.getLawfirmList(this.txtCompanyID.Text);
            this.grdLawFirm.DataSource = dataSet;
            this.grdLawFirm.DataBind();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.BindGrid();
    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.hdnClick.Value = "0";
            DataSet dataSet = new DataSet();
            dataSet = (DataSet)this.Session["TransferGridData"];
            this.grdTransfer.DataSource = dataSet;
            this.grdTransfer.DataBind();
            ASPxGridViewExporter aSPxGridViewExporter = this.gridExport;
            DateTime now = DateTime.Now;
            aSPxGridViewExporter.FileName = string.Concat("Transfer_Details(", now.ToString("ddMMMMyyyy"), ")");
            this.gridExport.WriteXlsToResponse();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        this.btnSearch.Attributes.Add("onClick", "return mapSelectedClick()");
        this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        if (!base.IsPostBack)
        {
            this.bindLawFirmList();
            this.hdnClick.Value = "0";
        }
        if (this.hdnClick.Value == "0")
        {
            this.BindGrid();
        }
    }
}