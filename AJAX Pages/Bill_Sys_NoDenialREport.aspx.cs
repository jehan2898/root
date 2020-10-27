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

public partial class AJAX_Pages_Bill_Sys_NoDenialREport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
       
        this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        this.rblpomanswered.Attributes.Add("onChange", "javascript:selectValue();");
        try
        {
            //this.testgrid1.PageNumberList = this.con;
            this.con.SourceGrid = this.grdvNoDenial;
            this.txtSearchBox.SourceGrid = this.grdvNoDenial;
            this.grdvNoDenial.Page = this.Page;
            this.grdvNoDenial.PageNumberList = this.con;
            if (!base.IsPostBack)
            {
                this.extddlInsurance.DataSource = GetAllIns(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.extddlInsurance.DataBind();
                //this.extddlInsurance.Items.Insert(0, new ListItem("---Select---", "-1"));
                this.extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                txtBillNo.Text = txtupdateBillNo.Text;
                txtCaseType.Text = extddlCaseType.Text;
                string ins = string.Empty;
                foreach (ListItem li in extddlInsurance.Items)
                {
                    if (li.Selected)
                        ins = ins + "'" + li.Value + "',";

                }
                ins = ins.TrimEnd(',');
                DateTime now = DateTime.Now;
                DateTime startDate = new DateTime(now.Year, now.Month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                txtInsurance.Text = ins;
                txtCaseNo.Text = txtupdateCaseNo.Text;
                txtDays.Text = ddldays.Text;
                txtPOMVerification.Text = rblpomanswered.SelectedValue.ToString();
                txtFromDate.Text = txtupdatefromdate.Text= startDate.ToString("MM/dd/yyyy");
                txtToDate.Text = txtupdateToDate.Text= endDate.ToString("MM/dd/yyyy");
                txtCaseType.Text = extddlCaseType.Text;
                //this.fillControl();
                this.grdvNoDenial.XGridBindSearch(); ;
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

            this.txtDay.Text = "";
            txtBillNo.Text = txtupdateBillNo.Text;
            txtCaseType.Text = extddlCaseType.Text;
            string ins = string.Empty;
            foreach (ListItem li in extddlInsurance.Items)
            {
                if (li.Selected)
                    ins = ins + "'" + li.Value + "',";

            }
            ins = ins.TrimEnd(',');
            txtInsurance.Text = ins;
            txtCaseNo.Text = txtupdateCaseNo.Text;
            txtDays.Text = ddldays.Text;
            txtPOMVerification.Text = rblpomanswered.SelectedValue.ToString();
            txtFromDate.Text = txtupdatefromdate.Text;
            txtToDate.Text = txtupdateToDate.Text;
            txtCaseType.Text = extddlCaseType.Text;
            //this.fillControl();
            this.grdvNoDenial.XGridBindSearch();

            //this.Clear();
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
    protected void btnLitigantion_Click(object sender, EventArgs e)
    {
        ArrayList arrayLists = new ArrayList();
        for (int i = 0; i < this.grdvNoDenial.Rows.Count; i++)
        {
            if (((CheckBox)this.grdvNoDenial.Rows[i].FindControl("ChkLitigantion")).Checked)
            {
                Bill_sys_litigantion billSysLitigantion = new Bill_sys_litigantion();
                string text = this.grdvNoDenial.Rows[i].Cells[0].Text;
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
            if(rblpomanswered.SelectedValue == "1")
            {
                lblpomdays.Text = "No denial since days of POM";
            }
            else if(rblpomanswered.SelectedValue == "2")
            {
                lblpomdays.Text = "No denial since days of verification answered";
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

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat(" window.location.href ='", ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET"), this.grdvNoDenial.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")), "';"), true);
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
            for (int i = 0; i < this.grdvNoDenial.Rows.Count; i++)
            {
                if (i == 0)
                {
                    stringBuilder.Append("<tr>");
                    for (int j = 0; j < this.grdvNoDenial.Columns.Count - 2; j++)
                    {
                        if (this.grdvNoDenial.Columns[j].Visible)
                        {
                            stringBuilder.Append("<td>");
                            stringBuilder.Append(this.grdvNoDenial.Columns[j].HeaderText);
                            stringBuilder.Append("</td>");
                        }
                    }
                    stringBuilder.Append("</tr>");
                }
                stringBuilder.Append("<tr>");
                for (int k = 0; k < this.grdvNoDenial.Columns.Count - 2; k++)
                {
                    if (this.grdvNoDenial.Columns[k].Visible && k == 2)
                    {
                        stringBuilder.Append("<td>");
                        stringBuilder.Append(this.grdvNoDenial.Rows[i].Cells[this.grdvNoDenial.Columns.Count - 1].Text);
                        stringBuilder.Append("</td>");
                    }
                    else if (this.grdvNoDenial.Columns[k].Visible)
                    {
                        stringBuilder.Append("<td>");
                        stringBuilder.Append(this.grdvNoDenial.Rows[i].Cells[k].Text);
                        stringBuilder.Append("</td>");
                    }
                }
                stringBuilder.Append("</tr>");
            }
            stringBuilder.Append("</table>");
            string str = string.Concat(this.getFileName("EXCEL"), ".xls");
            StreamWriter streamWriter = new StreamWriter(string.Concat(ApplicationSettings.GetParameterValue("EXCEL_SHEET"), str));
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
    public DataTable GetAllIns(string p_szCompanyID)
    {
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();
        string str = "";
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_MST_INSURANCE_COMPANY", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "INSURANCE_LIST");
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
            if ((ds != null) && (ds.Tables[0] != null))
            {
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return null;
    }


}