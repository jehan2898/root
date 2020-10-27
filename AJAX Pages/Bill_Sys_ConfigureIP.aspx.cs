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
using System.Data.SqlClient;
public partial class AJAX_Pages_Bill_Sys_ConfigureIP : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        grdUserRoll.Columns[5].Visible = true;
        grdUserRoll.Columns[6].Visible = true;
        grdUserRoll.Columns[7].Visible = true;
        try
        {
            if (!(Session["IPAdmin"] == "True"))
            {
                throw new Exception();
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "MM123", "alert('You are not authorized to view this page');history.go(-1);", true);
        }
        this.con.SourceGrid = grdUserRoll;
        this.txtSearchBox.SourceGrid = grdUserRoll;
        this.grdUserRoll.Page = this.Page;
        this.grdUserRoll.PageNumberList = this.con;
        this.con2.SourceGrid = grdUserRoll2;
        this.txtSearchBox2.SourceGrid = grdUserRoll2;
        this.grdUserRoll2.Page = this.Page;
        this.grdUserRoll2.PageNumberList = this.con;
        extddlUserRoll.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        ExtendedDropDownList12.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        if(TabContainer1.ActiveTab.ID=="SetIpTab")
            txtUserRoleID.Text = extddlUserRoll.Text;
        else
            txtUserRoleID.Text = ExtendedDropDownList12.Text;
        btnSearch.Attributes.Add("onclick", "return checkslect();");
        btnSave.Attributes.Add("onclick", "return SelectCheck()");
        btnSearch2.Attributes.Add("onclick", "return checkslect2();");
     //   btnSave2.Attributes.Add("onclick", "return SelectCheck2()");
        grdUserRoll2.Columns[5].Visible = false;
        grdUserRoll2.Columns[6].Visible = false;
        grdUserRoll2.Columns[7].Visible = false;
        //grdUserRoll.Columns[5].Visible = false;
        //grdUserRoll.Columns[6].Visible = false;
        //grdUserRoll.Columns[7].Visible = false;
        if (!IsPostBack)
        {
          
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        }
      
    }
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdUserRoll.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);

    }
    protected void lnkExportTOExcel2_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm1", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdUserRoll2.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnSearch2_Click(object sender, EventArgs e)
    {
        BindGrid2();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            foreach (GridViewRow gr in grdUserRoll2.Rows)
            {
                if (gr.Cells[4].Text == "Enabled")
                    ((CheckBox)gr.Cells[0].FindControl("ChkIPAuthorization2")).Checked = true;
                else if (gr.Cells[4].Text == "Disabled")
                    ((CheckBox)gr.Cells[0].FindControl("ChkIPAuthorization2")).Checked = false;
                else if (gr.Cells[4].Text == "Admin")
                {
                    gr.Enabled = false;
                    ((CheckBox)gr.Cells[0].FindControl("ChkIPAuthorization2")).Checked = false;
                }
            }
            grdUserRoll.Columns[5].Visible = false;
            grdUserRoll.Columns[6].Visible = false;
            grdUserRoll.Columns[7].Visible = false;
        }
        catch(Exception ex)
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

        grdUserRoll.XGridBindSearch();
        if (grdUserRoll.Rows.Count > 0)
            SaveIpTable.Visible = true;
        else
            SaveIpTable.Visible = false;

    }
    public void BindGrid2()
    {
        grdUserRoll2.Columns[5].Visible = true;
        grdUserRoll2.Columns[6].Visible = true;
        grdUserRoll2.Columns[7].Visible = true;
        grdUserRoll2.XGridBindSearch();
        foreach (GridViewRow gr in grdUserRoll2.Rows)
        {
            if (gr.Cells[4].Text == "Enabled")
                ((CheckBox)gr.Cells[0].FindControl("ChkIPAuthorization2")).Checked = true;
            else if(gr.Cells[4].Text=="Disabled")
                ((CheckBox)gr.Cells[0].FindControl("ChkIPAuthorization2")).Checked = false;
        }
        if (grdUserRoll2.Rows.Count > 0)
            btnSave2.Enabled = true;
        else
            btnSave2.Enabled = false;
        grdUserRoll2.Columns[5].Visible = false;
        grdUserRoll2.Columns[6].Visible = false;
        grdUserRoll2.Columns[7].Visible = false;
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
       if (txtIpAddress.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "mm1", "alert('Please enter IP Address in the textbox');", true);
            return;
        }
        string userid = "";
       // grdUserRoll.Columns[7].Visible = true;
        for (int i = 0; i < grdUserRoll.Rows.Count; i++)
        {
            if (((CheckBox)grdUserRoll.Rows[i].FindControl("ChkIPAuthorization")).Checked == true)
            {
                userid = userid+"''"+grdUserRoll.Rows[i].Cells[7].Text.ToString()+"'',";
            }
        }
       // grdUserRoll.Columns[7].Visible = false;
        if (userid.Length > 0)
            userid=userid.Remove(userid.Length - 1);
        Boolean flag=false;
        Boolean flag2 = false;
        int flag1;
        string[] IpArray = txtIpAddress.Text.Trim().Split(',');
        for (int i = 0; i < IpArray.Length; i++)
        {
            if (IpArray[i].Trim() != "")
            {
                string[] Ip = IpArray[i].Trim().Split('.');
                if (Ip.Length == 4)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if ((!Int32.TryParse(Ip[z].Trim(), out flag1)) || (Ip[z].ToString().Trim().Length > 3))
                        {
                            flag2 = true;
                            break;
                        }
                    }
                }
                else
                    flag2 = true;
                if (flag2 == true)
                {
                    flag = false;
                    break;
                }
            }
            else
                break;
            flag = true;
        }
        if (flag == false)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "mm1", "alert('Please enter valid IP Address in the textbox');", true);
            return;
        }
        InsertIP(IpArray, userid);
        txtIpAddress.Text = "";
        grdUserRoll.XGridBindSearch();
    }
    protected void InsertIP(string[] IpArray, string userid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds;
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        try
        {
            for (int i = 0; i < IpArray.Length; i++)
            {
                if (i == 1)
                    txtPreviousIP.Value = "False";
                if (IpArray[i].Trim() != "")
                {
                    SqlDataAdapter da=new SqlDataAdapter("EXEC SP_INSERT_USER_IP @SZ_IP_ADDRESS='"+IpArray[i]+"',@SZ_USER='"+userid+"', @SZ_COMPANY_ID='"+txtCompanyID.Text+"', @flag='"+txtPreviousIP.Value.ToString()+"'",con);
                    da.SelectCommand.ExecuteNonQuery();
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "mm1", "alert('IP Address Updated successfully');", true);
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "mm1", "alert('Error Occured. Details: "+ex.Message+"');", true);
        }
        finally
        {
            if(con.State == ConnectionState.Open)
                con.Close();
        }
    }

    protected void btnSave2_Click(object sender, EventArgs e)
    {
      
        string EnableUserid = "";
        for (int i = 0; i < grdUserRoll2.Rows.Count; i++)
        {
            if ((((CheckBox)grdUserRoll2.Rows[i].FindControl("ChkIPAuthorization2")).Checked == true) && (grdUserRoll2.Rows[i].Cells[4].Text == "Disabled"))
            {
                EnableUserid = EnableUserid + "''" + grdUserRoll2.Rows[i].Cells[7].Text.ToString() + "'',";
            }
        }
        if (EnableUserid.Length > 0)
            EnableUserid = EnableUserid.Remove(EnableUserid.Length - 1);
        string DisableUserid = "";
        for (int i = 0; i < grdUserRoll2.Rows.Count; i++)
        {
            if ((((CheckBox)grdUserRoll2.Rows[i].FindControl("ChkIPAuthorization2")).Checked == false) && (grdUserRoll2.Rows[i].Cells[4].Text == "Enabled"))
            {
                DisableUserid = DisableUserid + "''" + grdUserRoll2.Rows[i].Cells[7].Text.ToString() + "'',";
            }
        }
        if (DisableUserid.Length > 0)
            DisableUserid = DisableUserid.Remove(DisableUserid.Length - 1);
        ConfigureIP(EnableUserid,DisableUserid);
     
        BindGrid2();
    }
    protected void ConfigureIP(string EnableUserid, string DisableUserid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds;
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        try
        {
            string EnableFlag="true";
            string DisableFlag="true";
            if (EnableUserid.Length < 5)
                EnableFlag = "false";
            if (DisableUserid.Length < 5)
                DisableFlag= "false";
           SqlDataAdapter da = new SqlDataAdapter("EXEC SP_CONFIGURE_USER_IP @SZ_ENABLE_USER='" + EnableUserid + "',@SZ_DISABLE_USER='" + DisableUserid + "',@EnableFlag='"+EnableFlag+"', @DisableFlag='"+DisableFlag+"', @SZ_COMPANY_ID='" + txtCompanyID.Text + "'", con);
           da.SelectCommand.ExecuteNonQuery();
           ScriptManager.RegisterStartupScript(this, GetType(), "mm1", "alert('IP Authorization Updated successfully');", true);
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
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


    }
}
