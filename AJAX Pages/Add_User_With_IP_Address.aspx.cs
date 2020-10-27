using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using log4net;
using System.Configuration;
using System.Collections;

public partial class AJAX_Pages_Add_User_With_IP_Address : PageBase
{
    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;
    Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    SqlDataReader dr;
    private static ILog log = LogManager.GetLogger("Add_User_With_IP_Address");
    User_Login_With_IP_Validation obj = new User_Login_With_IP_Validation();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string sz_Company_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            
            string sz_ip_address = Request.QueryString["sz_ip_address"].ToString();
            string SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            DataSet ds_Bind = new DataSet();
            ds_Bind = obj.getCompanyWise_Users(sz_Company_ID);
            grd_User_List.DataSource = ds_Bind;
            grd_User_List.DataBind();
            DataSet ds=new DataSet();
            ds = obj.GetValidateIP(sz_Company_ID, SZ_USER_ID, sz_ip_address);
            for (int i = 0; i < grd_User_List.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grd_User_List.Rows[i].FindControl("chkVisit");
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (ds.Tables[0].Rows[j]["sz_user_id"].ToString() == grd_User_List.DataKeys[i][0].ToString())
                    {
                        chk.Checked = true;
                    }
                }
            }
        }
    }
    //public void BindGrid(string sz_company_id)
    //{
    //    try
    //    {

    //        grd_User_List.DataSource = getCompanyWise_Users(sz_company_id);
    //        grd_User_List.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}

    protected void btn_save_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            User_Login_With_IP_Validation obj = new User_Login_With_IP_Validation();
            ArrayList arr_Check = new ArrayList();
            string Company_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string sz_ip_address = Request.QueryString["sz_ip_address"].ToString();

            ArrayList arr_User = new ArrayList();
            for (int i = 0; i<grd_User_List.Rows.Count ; i++)
            {
                CheckBox chk = (CheckBox)grd_User_List.Rows[i].FindControl("chkVisit");
                if (chk.Checked)
                {
                    Bill_Sys_IP_Objects objIP = new Bill_Sys_IP_Objects();
                    string sz_Company_ID = Company_ID;
                    string ip_address = sz_ip_address;
                    string SZ_USER_ID = grd_User_List.DataKeys[i][0].ToString();
                    string SZ_USER_NAME = grd_User_List.DataKeys[i][1].ToString();

                    objIP.SZ_COMPANY_ID = sz_Company_ID;
                    objIP.SZ_IP_ADDRESS = ip_address;
                    objIP.SZ_USER_ID = SZ_USER_ID;
                    objIP.SZ_USER_NAME = SZ_USER_NAME;
                    arr_User.Add(objIP);
                }
              
            }
            log.Debug("Before save_IP_With_Multiple()");
            string str_success = obj.save_IP_With_Multiple(arr_User, Company_ID, sz_ip_address);
            //objIP = (Bill_Sys_IP_Objects)arr_Check[arr_Check.Count - 1];
            string message = "";
            if (str_success== "1")
            {
                message = "User added successfully ";
                lblMessage.Text = message;
            }
            else
            {
                lblMessage.Text = "User not added";
            }
            lblMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "fncLocal", "fncLocal();", true);
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
}