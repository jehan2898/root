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
using DevExpress.Web;
using System.Data.SqlClient;

public partial class AJAX_Pages_Bill_Sys_DeskInFo : PageBase
{
    public DataSet ds;
    public SqlCommand sqlCmd;
    public SqlConnection sqlCon;
    public SqlDataAdapter sqlda;
    public string strConn;
    public void BinGrid()
    {
        string str = "";
        if (this.Session["Subflag"] != null)
        {
            this.txtSubFlag.Text = this.Session["Subflag"].ToString();
        }
        if ((this.Session["ALL_OFF_ID"] != null) && (this.Session["ALL_OFF_ID"].ToString() == "YES"))
        {
            for (int i = 0; i < this.grdProvider.VisibleRowCount; i++)
            {
                if (str == "")
                {
                    str = "'" + this.grdProvider.GetRowValues(i, new string[] { "OFF_ID" }).ToString() + "'";
                }
                else
                {
                    str = str + ",'" + this.grdProvider.GetRowValues(i, new string[] { "OFF_ID" }).ToString() + "'";
                }
            }
            this.txtOfficeID.Text = str;
        }
        else
        {
            this.txtOfficeID.Text = "'" + this.Session["OFF_ID"].ToString() + "'";
        }
        this.grdBillSearch.XGridBindSearch();
        if (this.grdBillSearch.RecordCount > 0)
        {
            this.trSearch.Visible = true;
            if (this.txtFlag.Text == "2")
            {
                //  this.btnloaned.Visible = true;
                //  this.btnsold.Visible = false;
                this.btnselect.Visible = true;
                //   this.btnRevert.Visible = true;
            }
            else if (this.txtFlag.Text == "3")
            {
                //  this.btnloaned.Visible = false;
                //   this.btnsold.Visible = true;
                this.btnselect.Visible = true;
                // this.btnRevert.Visible = true;
            }
            else if ((this.txtFlag.Text == "0") || (this.txtFlag.Text == "1"))
            {
                // this.btnloaned.Visible = true;
                //  this.btnsold.Visible = true;
                this.btnselect.Visible = true;
                //  this.btnRevert.Visible = false;
            }
            this.Label1.Visible = true;
            this.txtSum.Visible = true;
        }
        else
        {
            // this.btnloaned.Visible = false;
            //   this.btnsold.Visible = false;
            this.btnselect.Visible = false;
            this.btnSelectAll.Visible = false;
            // this.btnRevert.Visible = false;
            this.txtSum.Visible = false;
            this.Label1.Visible = false;
            this.trSearch.Visible = true;
        }
    }

    protected void btnloaned_Click(object sender, EventArgs e)
    {
        Bill_Sys_BillTransaction_BO n_bo;
        string str = "";
        if (base.Request.QueryString["id"] != null)
        {
            str = base.Request.QueryString["id"].ToString();
        }
        else
        {
            str = "";
        }
        if (this.Session["SelectAll"] != null)
        {
            string str2 = this.Session["OFF_ID"].ToString();
            n_bo = new Bill_Sys_BillTransaction_BO();
            if (n_bo.UpdateStatusAll(this.txtCompanyId.Text, this.txtLoginCompanyId.Text, str2, this.txtSearchBox.Text, "LND", this.txtFlag.Text) >= 1)
            {
                if (str != "2")
                {
                    this.usrMessage.PutMessage("All bills in the current selection with 'Bill Downloaded' status updated successfully to Loaned.");
                }
                else
                {
                    this.usrMessage.PutMessage("Bill Status Updated Succesfully.");
                }
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                this.BinGrid();
            }
            else
            {
                if (str != "2")
                {
                    this.usrMessage.PutMessage("Error in transaction, only bills with 'Bill Downloaded' status can be changed to Loaned.");
                }
                else
                {
                    this.usrMessage.PutMessage("Error in transaction.");
                }
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            this.btnSelectAll_Click(null, null);
        }
        else
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdBillSearch.Rows[i].FindControl("ChkDelete");
                if (box.Checked)
                {
                    list.Add(this.grdBillSearch.DataKeys[i][1].ToString());
                }
            }
            n_bo = new Bill_Sys_BillTransaction_BO();
            if (n_bo.UpdateStatus(list, this.txtLoginCompanyId.Text, "LND") >= 1)
            {
                if (str != "2")
                {
                    this.usrMessage.PutMessage("All bills in the current selection with 'Bill Downloaded' status updated successfully to Loaned.");
                }
                else
                {
                    this.usrMessage.PutMessage("Bill Status Updated Succesfully.");
                }
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                this.grdBillSearch.XGridBindSearch();
            }
            else
            {
                if (str != "2")
                {
                    this.usrMessage.PutMessage("Error in transaction, only bills with 'Bill Downloaded' status can be changed to Loaned.");
                }
                else
                {
                    this.usrMessage.PutMessage("Error in transaction.");
                }
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
        }
    }

    protected void btnRevert_Click(object sender, EventArgs e)
    {
        if (this.Session["SelectAll"] != null)
        {
            string str = this.Session["OFF_ID"].ToString();
            Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
            if (n_bo.RevertStatausAll(this.txtCompanyId.Text, this.txtLoginCompanyId.Text, str, this.txtSearchBox.Text, this.txtFlag.Text) == 1)
            {
                this.usrMessage.PutMessage("Bill status updated successfully.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                this.BinGrid();
            }
            else
            {
                this.usrMessage.PutMessage("erro in transaction");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            this.btnSelectAll_Click(null, null);
        }
        else
        {
            ArrayList list = new ArrayList();
            for (int j = 0; j < this.grdBillSearch.Rows.Count; j++)
            {
                CheckBox box = (CheckBox)this.grdBillSearch.Rows[j].FindControl("ChkDelete");
                if (box.Checked)
                {
                    list.Add(this.grdBillSearch.DataKeys[j][1].ToString());
                }
            }
            Bill_Sys_BillTransaction_BO n_bo2 = new Bill_Sys_BillTransaction_BO();
            if (n_bo2.RevertStataus(list, this.txtLoginCompanyId.Text) == 1)
            {
                this.usrMessage.PutMessage("Bill status revert successfully.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                this.grdBillSearch.XGridBindSearch();
            }
            else
            {
                this.usrMessage.PutMessage("erro in transaction");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
        }
        this.Session["SelectAll"] = null;
        this.grdLitigationCompanyWise.XGridBind();
        for (int i = 0; i < this.grdLitigationCompanyWise.Rows.Count; i++)
        {
            Label label = (Label)this.grdLitigationCompanyWise.Rows[i].FindControl("lblAmount");
            LinkButton button = (LinkButton)this.grdLitigationCompanyWise.Rows[i].FindControl("lnkAmt");
            switch (i)
            {
                case 0:
                case 1:
                    label.Visible = true;
                    button.Visible = false;
                    break;

                default:
                    label.Visible = false;
                    button.Visible = true;
                    break;
            }
        }
    }

    protected void btnselect_Click(object sender, EventArgs e)
    {
        double num = 0.0;
        for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
        {
            CheckBox box = (CheckBox)this.grdBillSearch.Rows[i].FindControl("ChkDelete");
            if (box.Checked)
            {
                string str = this.grdBillSearch.DataKeys[i][0].ToString();
                switch (str)
                {
                    case "":
                    case null:
                    case "&nbsp;":
                        str = "0";
                        break;
                }
                num += Convert.ToDouble(str);
            }
        }
        this.txtSum.Text = Convert.ToString(num);
    }

    protected void btnSelectAll_Click(object sender, EventArgs e)
    {
        if (this.btnSelectAll.Text == "Select All")
        {
            Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
            double num = 0.0;
            DataSet set = new DataSet();
            string str = "";
            if (this.Session["OFF_ID"] != null)
            {
                str = this.Session["OFF_ID"].ToString();
            }
            set = n_bo.GetBills(this.txtCompanyId.Text, this.txtLoginCompanyId.Text, str, this.txtSearchBox.Text, this.txtFlag.Text);
            for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdBillSearch.Rows[i].FindControl("ChkDelete");
                box.Checked = true;
                box.Enabled = false;
            }
            for (int j = 0; j < set.Tables[0].Rows.Count; j++)
            {
                string str2 = set.Tables[0].Rows[j][5].ToString();
                switch (str2)
                {
                    case "":
                    case null:
                    case "&nbsp;":
                        str2 = "0";
                        break;
                }
                num += Convert.ToDouble(str2);
            }
            this.txtSum.Text = Convert.ToString(num);
            this.btnSelectAll.Text = "Deselect All";
            this.hselectVlaue.Value = "2";
            this.Session["SelectAll"] = "TRUE";
        }
        else if (this.btnSelectAll.Text == "Deselect All")
        {
            this.Session["SelectAll"] = null;
            this.hselectVlaue.Value = "1";
            this.txtSum.Text = "";
            this.btnSelectAll.Text = "Select All";
            for (int k = 0; k < this.grdBillSearch.Rows.Count; k++)
            {
                CheckBox box2 = (CheckBox)this.grdBillSearch.Rows[k].FindControl("ChkDelete");
                box2.Enabled = true;
                box2.Checked = false;
            }
        }
    }

    protected void btnsold_Click(object sender, EventArgs e)
    {
        Bill_Sys_BillTransaction_BO n_bo;
        string str = "";
        if (base.Request.QueryString["id"] != null)
        {
            str = base.Request.QueryString["id"].ToString();
        }
        else
        {
            str = "";
        }
        if (this.Session["SelectAll"] != null)
        {
            string str2 = this.Session["OFF_ID"].ToString();
            n_bo = new Bill_Sys_BillTransaction_BO();
            if (n_bo.UpdateStatusAll(this.txtCompanyId.Text, this.txtLoginCompanyId.Text, str2, this.txtSearchBox.Text, "SLD", this.txtFlag.Text) >= 1)
            {
                if (str != "3")
                {
                    this.usrMessage.PutMessage("All bills in the current selection with 'Bill Downloaded' status updated successfully to Sold.");
                }
                else
                {
                    this.usrMessage.PutMessage("Bill Status updated succesfully.");
                }
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                this.grdBillSearch.XGridBindSearch();
            }
            else
            {
                if (str != "3")
                {
                    this.usrMessage.PutMessage("Error in transaction, only bills with 'Bill Downloaded' status can be changed to Sold.");
                }
                else
                {
                    this.usrMessage.PutMessage("Error in transaction.");
                }
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            this.btnSelectAll_Click(null, null);
        }
        else
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdBillSearch.Rows[i].FindControl("ChkDelete");
                if (box.Checked)
                {
                    list.Add(this.grdBillSearch.DataKeys[i][1].ToString());
                }
            }
            n_bo = new Bill_Sys_BillTransaction_BO();
            if (n_bo.UpdateStatus(list, this.txtLoginCompanyId.Text, "SLD") >= 1)
            {
                if (str != "3")
                {
                    this.usrMessage.PutMessage("All bills in the current selection with 'Bill Downloaded' status updated successfully to Sold.");
                }
                else
                {
                    this.usrMessage.PutMessage("Bill Status Updated successfully.");
                }
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                this.BinGrid();
            }
            else
            {
                if (str.ToString() != "3")
                {
                    this.usrMessage.PutMessage("Error in transaction, only bills with 'Bill Downloaded' status can be changed to Sold.");
                }
                else
                {
                    this.usrMessage.PutMessage("Error in transaction.");
                }
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
        }
    }

    protected string CheckNull(object ogrid)
    {
        if (ogrid == DBNull.Value)
        {
            return "";
        }
        return DateTime.Parse(ogrid.ToString()).ToString("MM/dd/yyyy");
    }

    public DataSet GetSummury()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("sp_provider_summary", this.sqlCon);
            this.sqlCmd.CommandType = CommandType.StoredProcedure;
            this.sqlCmd.Parameters.AddWithValue("@sz_company_id", this.txtCompanyId.Text);
            this.sqlCmd.Parameters.AddWithValue("@sz_login_company", this.txtLoginCompanyId.Text);
            this.sqlCmd.Parameters.AddWithValue("@sz_office_id", this.txtOfficeID.Text);
            this.sqlCmd.Parameters.AddWithValue("@sz_flag", this.txtFlag.Text);
            this.sqlda = new SqlDataAdapter(this.sqlCmd);
            this.sqlda.Fill(this.ds);
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
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdCaseCount_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int num = Convert.ToInt32(e.CommandArgument.ToString());
        string str = this.grdCaseCount.DataKeys[num][0].ToString();
        this.txtSubFlag.Text = str;
        this.Session["Subflag"] = str;
        this.BinGrid();
    }

    protected void grdLitigationCompanyWise_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowBills")
        {
            int num = Convert.ToInt32(e.CommandArgument.ToString());
            string str = this.grdLitigationCompanyWise.DataKeys[num][0].ToString();
            string str2 = this.grdLitigationCompanyWise.DataKeys[num][1].ToString();
            this.lblInsName.Text = str2;
            this.lblInsName.Visible = true;
            this.Session["ALL_OFF_ID"] = null;
            this.Session["OFF_ID"] = str;
            this.txtOfficeID.Text = "'" + str + "'";
            this.Session["Subflag"] = null;
            this.txtSubFlag.Text = "";
            this.BinGrid();
            this.grdCaseCount.XGridBind();
            for (int i = 0; i < this.grdCaseCount.Rows.Count; i++)
            {
                LinkButton button = (LinkButton)this.grdCaseCount.Rows[i].FindControl("lnkcount");
                button.Enabled = true;
            }
            this.btnSelectAll.Visible = true;
            this.btnSelectAll.Text = "Select All";
            this.Session["SelectAll"] = null;
            this.txtSum.Text = "";
        }
    }

    protected void grdProvider_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            e.KeyValue.ToString();
            string str = e.KeyValue.ToString();
            string str2 = this.grdProvider.GetRowValues(e.VisibleIndex, new string[] { "SZ_OFFICE" }).ToString();
            this.lblInsName.Text = str2;
            this.lblInsName.Visible = true;
            this.Session["ALL_OFF_ID"] = null;
            this.Session["OFF_ID"] = str;
            this.txtOfficeID.Text = "'" + str + "'";
            this.Session["Subflag"] = null;
            this.txtSubFlag.Text = "";
            this.BinGrid();
            this.grdCaseCount.XGridBind();
            for (int i = 0; i < this.grdCaseCount.Rows.Count; i++)
            {
                LinkButton button = (LinkButton)this.grdCaseCount.Rows[i].FindControl("lnkcount");
                button.Enabled = true;
            }
            this.btnSelectAll.Visible = true;
            this.btnSelectAll.Text = "Select All";
            this.Session["SelectAll"] = null;
            this.txtSum.Text = "";
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
        if (this.Session["Subflag"] != null)
        {
            this.txtSubFlag.Text = this.Session["Subflag"].ToString();
        }
        else
        {
            this.txtSubFlag.Text = "";
        }
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdBillSearch.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.grdLitigationCompanyWise.Page = this.Page;
        this.grdCaseCount.Page = this.Page;
        this.con.SourceGrid = this.grdBillSearch;
        this.txtSearchBox.SourceGrid = this.grdBillSearch;
        this.grdBillSearch.Page = this.Page;
        this.grdBillSearch.PageNumberList = this.con;
        // this.btnloaned.Attributes.Add("onclick", "return checkSelected();");
        this.btnselect.Attributes.Add("onclick", "return checkSelected();");
        //  this.btnsold.Attributes.Add("onclick", "return checkSelected();");
        this.btnSelectAll.Attributes.Add("onclick", "return checkSelectedAll();");
        if (!base.IsPostBack)
        {
            this.Session["Subflag"] = null;
            this.lblInsName.Visible = false;
            this.btnSelectAll.Visible = false;
            this.lblInsName.Text = "";
            this.txtSum.Text = "";
            this.txtSubFlag.Text = "";
            this.hselectVlaue.Value = "1";
            string str = base.Request.QueryString["LfirmId"].ToString();
            this.Session["OFF_ID"] = null;
            this.Session["ALL_OFF_ID"] = "YES";
            this.txtLoginCompanyId.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.txtCompanyId.Text = str;
            if (base.Request.QueryString["id"] != null)
            {
                this.txtFlag.Text = base.Request.QueryString["id"].ToString();
                if ((base.Request.QueryString["id"].ToString() == "") || !(base.Request.QueryString["id"].ToString() == "2"))
                {
                }
            }
            else
            {
                this.txtFlag.Text = "0";
            }
            this.grdLitigationCompanyWise.XGridBind();
            this.grdCaseCount.XGridBind();
            for (int i = 0; i < this.grdLitigationCompanyWise.Rows.Count; i++)
            {
                Label label = (Label)this.grdLitigationCompanyWise.Rows[i].FindControl("lblAmount");
                LinkButton button = (LinkButton)this.grdLitigationCompanyWise.Rows[i].FindControl("lnkAmt");
                switch (i)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        label.Visible = true;
                        button.Visible = false;
                        break;

                    default:
                        label.Visible = false;
                        button.Visible = true;
                        break;
                }
            }
            // this.btnsold.Visible = false;
            //this.btnloaned.Visible = false;
            this.btnselect.Visible = false;
            this.btnSelectAll.Visible = false;
            //this.btnRevert.Visible = false;
            this.txtSum.Visible = false;
            this.Label1.Visible = false;
            this.trSearch.Visible = false;
            this.grdProvider.DataSource = this.GetSummury();
            this.grdProvider.DataBind();
            this.BinGrid();
        }
    }
}
