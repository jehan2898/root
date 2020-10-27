using AjaxControlToolkit;
using ASP;
using ExtendedDropDownList;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XGridPagination;
using XGridSearchTextBox;
using XGridView;

public partial class Bill_Sys_VisitReport : Page, IRequiresSessionState
{

    private Bill_Sys_ReportBO _reportBO;

    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;

    private ArrayList objAL;

    private SqlConnection sqlCon;

    private SqlCommand sqlCmd;

    private SqlDataAdapter sqlda;

    private SqlDataReader dr;


    public Bill_Sys_VisitReport()
    {
    }

    private void BindGrid()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!this.chkShowBillAmount.Checked)
            {
                this.grdAllReports.Columns[8].Visible = false;
            }
            else
            {
                this.grdAllReports.Columns[8].Visible = true;
            }
            if (!this.chkShowDenial.Checked)
            {
                this.txtShowDenials.Text = "0";
            }
            else
            {
                this.txtShowDenials.Text = "1";
            }
            this.Searchtd.Visible = true;
            this.Exceltd.Visible = true;
            this.grdAllReports.XGridBindSearch();
            this.TotalAmount();
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
        try
        {
            this.BindGrid();
        }
        catch (Exception exception)
        {
            string str = exception.Message.ToString();
            str = str.Replace("\n", " ");
            base.Response.Redirect(string.Concat("Bill_Sys_ErrorPage.aspx?ErrMsg=", str));
        }
    }

    protected void chkShowDenial_CheckedChange(object sender, EventArgs e)
    {
        if (this.chkShowDenial.Checked)
        {
            this.txtDenialFromDate.BackColor = Color.White;
            this.txtDenialToDate.BackColor = Color.White;
            this.txtDenialFromDate.Enabled = true;
            this.txtDenialToDate.Enabled = true;
            this.imgbtnDenialFromDate.Enabled = true;
            this.imgbtnDenialToDate.Enabled = true;
            return;
        }
        this.txtDenialFromDate.BackColor = Color.LightGray;
        this.txtDenialToDate.BackColor = Color.LightGray;
        this.txtDenialFromDate.Enabled = false;
        this.txtDenialToDate.Enabled = false;
        this.imgbtnDenialFromDate.Enabled = false;
        this.imgbtnDenialToDate.Enabled = false;
    }

    protected void ExportToExcel()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<table border='1px'>");
        for (int i = 0; i < this.grdAllReports.Rows.Count; i++)
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
                    if (k != 1 || !(this.grdAllReports.Rows[i].Cells[6].Text == "&nbsp;"))
                    {
                        stringBuilder.Append(this.grdAllReports.Rows[i].Cells[k].Text);
                    }
                    else
                    {
                        stringBuilder.Append("<b>Location</b>");
                    }
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

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        if (this.chkShowBillAmount.Checked)
        {
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat(" window.location.href ='", ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET"), this.grdAllReports.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")), "';"), true);
            return;
        }
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat(" window.location.href ='", ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET"), this.grdAllReports.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")), "';"), true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.chkShowDenial.Attributes.Add("onClick", "javascript:ChkChange();");
            this.con.SourceGrid = this.grdAllReports;
            this.txtSearchBox.SourceGrid = this.grdAllReports;
            this.grdAllReports.Page = this.Page;
            this.grdAllReports.PageNumberList = this.con;
            base.Title = "Patient Visit Summary Report";
            this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            if (!base.IsPostBack)
            {
                this.txtDenialFromDate.BackColor = Color.LightGray;
                this.txtDenialToDate.BackColor = Color.LightGray;
                this.chkShowDenial.Checked = false;
                this.txtShowDenials.Text = "0";
                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1")
                {
                    this.hdnLocation.Value = "0";
                }
                else
                {
                    this.extddlLocation.Visible = true;
                    this.lblLocationName.Visible = true;
                    this.extddlLocation.Flag_ID = this.txtCompanyID.Text.ToString();
                }
            }
        }
        catch (Exception exception)
        {
            string str = exception.Message.ToString();
            str = str.Replace("\n", " ");
            base.Response.Redirect(string.Concat("Bill_Sys_ErrorPage.aspx?ErrMsg=", str));
        }
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_Sys_VisitReport.aspx");
        }
    }

    protected void TotalAmount()
    {
        this.sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        decimal num = new decimal(0);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_VISIT_REPORT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", this.txtCompanyID.Text);
                this.sqlCmd.Parameters.AddWithValue("@sz_location_id", this.extddlLocation.Text);
                this.sqlCmd.Parameters.AddWithValue("@sz_doctor_id", this.extddlDoctor.Text);
                this.sqlCmd.Parameters.AddWithValue("@dt_from_event_date", this.txtFromDate.Text);
                this.sqlCmd.Parameters.AddWithValue("@dt_to_event_date", this.txtToDate.Text);
                this.sqlCmd.Parameters.AddWithValue("@DT_DENIAL_CREATED_DATE_FROM", this.txtDenialFromDate.Text);
                this.sqlCmd.Parameters.AddWithValue("@DT_DENIAL_CREATED_DATE_TO", this.txtDenialToDate.Text);
                this.sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", this.extddlSpeciality.Text);
                this.sqlCmd.Parameters.AddWithValue("@I_START_INDEX", "1");
                this.sqlCmd.Parameters.AddWithValue("@I_END_INDEX", "100000");
                this.sqlCmd.Parameters.AddWithValue("@SZ_ORDER_BY", "");
                this.sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", "");
                this.sqlCmd.Parameters.AddWithValue("@sz_show_denials", this.txtShowDenials.Text);
                (new SqlDataAdapter(this.sqlCmd)).Fill(dataSet);
                this.Session["SpecialityCount"] = dataSet.Tables[0];
            }
            catch (SqlException sqlException)
            {
                string str = sqlException.Message.ToString();
                str = str.Replace("\n", " ");
                base.Response.Redirect(string.Concat("Bill_Sys_ErrorPage.aspx?ErrMsg=", str));
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
        {
            num = num + Convert.ToDecimal(dataSet.Tables[0].Rows[i]["Total Amount"]);
        }
        this.lblTotAmtValue.Text = string.Concat("$", num.ToString());
        try
        {
            if (dataSet.Tables[2].Rows.Count > 0)
            {
                this.lblTotPatientsValue.Text = dataSet.Tables[2].Rows[0][0].ToString();
            }
        }
        catch
        {
        }
        try
        {
            if (dataSet.Tables[1].Rows.Count > 0)
            {
                this.lblTotVisitsValue.Text = dataSet.Tables[1].Rows[0][0].ToString();
            }
        }
        catch
        {
        }
    }
}