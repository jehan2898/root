using AjaxControlToolkit;
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

public partial class Bill_Sys_DenialReport : Page, IRequiresSessionState
{

    private Bill_Sys_ReportBO _reportBO;

    private CaseDetailsBO _objCaseDetailsBO = new CaseDetailsBO();

    private ReportDAO _ReportDAO;

    private SearchReportBO _SearchReportBO;


    public Bill_Sys_DenialReport()
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
            this.txtBillStatus.Text = "DEN";
            if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.txtFlag.Text = "BILLING";
            }
            else
            {
                this.txtFlag.Text = "REF";
            }
            this.txtDay.Text = "";
            this.fillControl();
            this.grdvDenial.XGridBindSearch();
            this.Clear();
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

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        this.ExportToExcel();
    }

    protected void btnLitigantion_Click(object sender, EventArgs e)
    {
        ArrayList arrayLists = new ArrayList();
        for (int i = 0; i < this.grdvDenial.Rows.Count; i++)
        {
            if (((CheckBox)this.grdvDenial.Rows[i].FindControl("ChkLitigantion")).Checked)
            {
                Bill_sys_litigantion billSysLitigantion = new Bill_sys_litigantion();
                string text = this.grdvDenial.Rows[i].Cells[0].Text;
                arrayLists.Add(text);
            }
        }
        (new Bill_Sys_NF3_Template()).UpdateLitigantion(arrayLists, this.txtCompanyID.Text);
        this.usrMessage.PutMessage("Selected Bill sended litigation!");
        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
        this.usrMessage.Show();
        this.BindGrid();
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
            //this.PusSignColorChange();
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

    public void Clear()
    {
        this.txtDay.Text = "";
        this.txtFromDate.Text = "";
        this.txtToDate.Text = "";
        this.txtBillNo.Text = "";
        this.txtCaseNo.Text = "";
        this.txtPatientName.Text = "";
        this.txtDenial.Text = "";
        this.txtDoctor.Text = "";
        this.txtLocation.Text = "";
        this.txtCaseType.Text = "";
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
            for (int i = 0; i < this.grdvDenial.Rows.Count; i++)
            {
                if (i == 0)
                {
                    stringBuilder.Append("<tr>");
                    for (int j = 0; j < this.grdvDenial.Columns.Count - 2; j++)
                    {
                        if (this.grdvDenial.Columns[j].Visible)
                        {
                            stringBuilder.Append("<td>");
                            stringBuilder.Append(this.grdvDenial.Columns[j].HeaderText);
                            stringBuilder.Append("</td>");
                        }
                    }
                    stringBuilder.Append("</tr>");
                }
                stringBuilder.Append("<tr>");
                for (int k = 0; k < this.grdvDenial.Columns.Count - 2; k++)
                {
                    if (this.grdvDenial.Columns[k].Visible && k == 2)
                    {
                        stringBuilder.Append("<td>");
                        stringBuilder.Append(this.grdvDenial.Rows[i].Cells[this.grdvDenial.Columns.Count - 1].Text);
                        stringBuilder.Append("</td>");
                    }
                    else if (this.grdvDenial.Columns[k].Visible)
                    {
                        stringBuilder.Append("<td>");
                        stringBuilder.Append(this.grdvDenial.Rows[i].Cells[k].Text);
                        stringBuilder.Append("</td>");
                    }
                }
                stringBuilder.Append("</tr>");
            }
            stringBuilder.Append("</table>");
            string str = string.Concat(this.getFileName("EXCEL"), ".xls");
            StreamWriter streamWriter = new StreamWriter(string.Concat(ApplicationSettings.GetParameterValue( "EXCEL_SHEET"), str));
            streamWriter.Write(stringBuilder);
            streamWriter.Close();
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat(" window.location.href ='", ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET"), str, "';"), true);
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

    public void fillControl()
    {
        this.txtBillStatus.Visible = true;
        this.txtPatientName.Visible = true;
        this.txtFlag.Visible = true;
        this.txtDay.Visible = true;
        this.txtDay.Visible = true;
        this.txtCompanyID.Visible = true;
        this.txtBillNo.Visible = true;
        this.txtCaseNo.Visible = true;
        this.txtFromDate.Visible = true;
        this.txtToDate.Visible = true;
        this.txtBillNo.Text = this.txtupdateBillNo.Text;
        this.txtCaseNo.Text = this.txtupdateCaseNo.Text;
        this.txtFromDate.Text = this.txtupdatefromdate.Text;
        this.txtToDate.Text = this.txtupdateToDate.Text;
        this.txtPatientName.Text = this.txtupdatePatientName.Text;
        this.txtDenialFromdt.Text = this.txtDenialFromDate.Text;
        this.txtDenialTodt.Text = this.txtDenialToDate.Text;
        if (this.extddenial.Text != "NA" && this.extddenial.Text != "")
        {
            this.txtDenial.Text = this.extddenial.Selected_Text;
        }
        if (this.extddlDoctor.Text != "NA" && this.extddlDoctor.Text != "")
        {
            this.txtDoctor.Text = this.extddlDoctor.Text;
        }
        if (this.extddlLocation.Text != "NA" && this.extddlLocation.Text != "")
        {
            this.txtLocation.Text = this.extddlLocation.Text;
        }
        if (this.extddlCaseType.Text != "NA" && this.extddlCaseType.Text != "")
        {
            this.txtCaseType.Text = this.extddlCaseType.Text;
        }
        this.txtPaidUnPaid.Text = this.rblView.SelectedValue.ToString();
        this.txtBillStatus.Visible = false;
        this.txtDay.Visible = false;
        this.txtCompanyID.Visible = false;
        this.txtFlag.Visible = false;
        this.txtBillNo.Visible = false;
        this.txtCaseNo.Visible = false;
        this.txtDay.Visible = false;
        this.txtFromDate.Visible = false;
        this.txtToDate.Visible = false;
        this.txtPatientName.Visible = false;
    }

    public DataSet GetDenialInfo(string sz_CompanyID, string SZ_BILL_NUMBER)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_MANAGE_GET_DENIAL_REASON", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_BILL_NUMBER);
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
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
            
        }
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return dataSet;
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

    protected void grdvDenial_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Sort"))
        {
            if (this.ViewState["SortExp"] == null)
            {
                this.ViewState["SortExp"] = e.CommandArgument.ToString();
                this.ViewState["SortOrder"] = "ASC";
                DataSet dataSet = new DataSet();
                dataSet = (DataSet)this.Session["Grid"];
                DataView defaultView = dataSet.Tables[0].DefaultView;
                defaultView.Sort = string.Concat(this.ViewState["SortExp"], " ", this.ViewState["SortOrder"]);
                this.grdvDenial.DataSource = defaultView;
                this.grdvDenial.DataBind();
            }
            else if (this.ViewState["SortExp"].ToString() != e.CommandArgument.ToString())
            {
                this.ViewState["SortOrder"] = "ASC";
                this.ViewState["SortExp"] = e.CommandArgument.ToString();
            }
            else if (this.ViewState["SortOrder"].ToString() != "ASC")
            {
                this.ViewState["SortOrder"] = "ASC";
            }
            else
            {
                this.ViewState["SortOrder"] = "DESC";
            }
        }
        int num = 0;
        if (e.CommandName.ToString() == "DenialPLS")
        {
            for (int i = 0; i < this.grdvDenial.Rows.Count; i++)
            {
                LinkButton linkButton = (LinkButton)this.grdvDenial.Rows[i].FindControl("lnkDM");
                LinkButton linkButton1 = (LinkButton)this.grdvDenial.Rows[i].FindControl("lnkDP");
                if (linkButton.Visible)
                {
                    linkButton.Visible = false;
                    linkButton1.Visible = true;
                }
            }
            this.grdvDenial.Columns[13].Visible = true;
            num = Convert.ToInt32(e.CommandArgument);
            string str = "div1";
            str = string.Concat(str, this.grdvDenial.DataKeys[num][2].ToString());
            GridView denialInfo = (GridView)this.grdvDenial.Rows[num].FindControl("grdDenial");
            LinkButton linkButton2 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkDP");
            LinkButton linkButton3 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkDM");
            string str1 = this.grdvDenial.DataKeys[num][2].ToString();
            string text = this.txtCompanyID.Text;
            denialInfo.DataSource = this.GetDenialInfo(text, str1);
            denialInfo.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mp", string.Concat("ShowDenialChildGrid('", str, "') ;"), true);
            linkButton2.Visible = false;
            linkButton3.Visible = true;
        }
        if (e.CommandName.ToString() == "DenialMNS")
        {
            this.grdvDenial.Columns[13].Visible = false;
            num = Convert.ToInt32(e.CommandArgument);
            string str2 = "div";
            str2 = string.Concat(str2, this.grdvDenial.DataKeys[num][2].ToString());
            LinkButton linkButton4 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkDP");
            LinkButton linkButton5 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkDM");
            linkButton4.Visible = true;
            linkButton5.Visible = false;
        }
    }

    protected void grdvDenial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gridView = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            int num = -1;
            foreach (DataControlField column in gridView.Columns)
            {
                e.Row.Cells[gridView.Columns.IndexOf(column)].CssClass = "headerstyle";
                if (column.SortExpression != gridView.SortExpression)
                {
                    continue;
                }
                num = gridView.Columns.IndexOf(column);
            }
            if (num > -1)
            {
                e.Row.Cells[num].CssClass = (gridView.SortDirection == SortDirection.Ascending ? "sortascheaderstyle" : "sortdescheaderstyle");
            }
        }
    }

    protected void grdvVerificationSent_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat(" window.location.href ='", ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET"), this.grdvDenial.ExportToExcel(ApplicationSettings.GetParameterValue( "EXCEL_SHEET")), "';"), true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.grdvDenial.Columns[13].Visible = false;
        this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        this.btnLitigantion.Attributes.Add("onclick", "return Validate()");
        this.Clear();
        this.txtBillStatus.Text = "DEN";
        try
        {
            this.txtCompanyID.Visible = true;
            this.txtFlag.Visible = true;
            this.txtFromDate.Visible = true;
            this.txtToDate.Visible = true;
            if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.txtFlag.Text = "BILLING";
            }
            else
            {
                this.txtFlag.Text = "REF";
            }
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.con.SourceGrid = this.testgrid1;
            this.testgrid1.Page = this.Page;
            this.testgrid1.PageNumberList = this.con;
            this.con.SourceGrid = this.grdvDenial;
            this.txtSearchBox.SourceGrid = this.grdvDenial;
            this.grdvDenial.Page = this.Page;
            this.grdvDenial.PageNumberList = this.con;
            this.fillControl();
            if (!base.IsPostBack)
            {
                this.extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlLocation.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddenial.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.grdvDenial.XGridBindSearch();
               // this.PusSignColorChange();
                this.testgrid1.XGridBindSearch();
            }
            this.txtCompanyID.Visible = false;
            this.txtFlag.Visible = false;
            this.txtToDate.Visible = false;
            this.txtFromDate.Visible = false;
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

    public void PusSignColorChange()
    {
        for (int i = 0; i < this.grdvDenial.Rows.Count; i++)
        {
            string str = this.grdvDenial.DataKeys[i][3].ToString();
            LinkButton red = (LinkButton)this.grdvDenial.Rows[i].FindControl("lnkDM");
            LinkButton linkButton = (LinkButton)this.grdvDenial.Rows[i].FindControl("lnkDP");
            if (str.ToLower() != "true")
            {
                linkButton.Visible = false;
            }
            else
            {
                linkButton.ForeColor = Color.Red;
                red.ForeColor = Color.Red;
            }
        }
    }
}