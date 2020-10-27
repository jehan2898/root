using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AJAX_Pages_Popup_UP_UnbilledVisitReport : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            loadexisting();
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
             string load = "";
        if (rbt1.Checked)
        {
            load = "VISITS=ALL";

            
        }
        else if (rbt2.Checked)
        {

            load = "VISITS=MINIMUM_DATE";
           
        }
      

        //load = load + ",SHOW_ONLY_LAST="+ ddl_list.SelectedItem.Value.ToString();
        string userid=((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        string companyid=((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string pagename = "UnbilledVisits";
       
        UserPreferences obj = new UserPreferences();
        obj.save_Delivery_report(pagename, userid, companyid, load);
        lblMessage.Visible = true;
        lblMessage.Text = "Your preferences were changed successfully";
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
    protected void btnRemovePreferences_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            string userid = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            string companyid = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string pagename = "UnbilledVisits";
            UserPreferences obj = new UserPreferences();
            obj.UP_Remove(userid, companyid, pagename);
            lblMessage.Visible = true;
            lblMessage.Text = "Your preferences were removed successfully";

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
    private void loadexisting()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        DataSet ds = new DataSet();
        UserPreferences obj = new UserPreferences();
        try
        {

            ds = obj.GetUserPreferences(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, "UnbilledVisits");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["sz_preferences"].ToString() != "")
                {
                    string[] Condition = ds.Tables[0].Rows[0]["sz_preferences"].ToString().Split('=');
                    if (Condition.Length == 2)
                    {
                        if (Condition[1].ToString().ToString().ToLower()=="all")
                        {
                            rbt1.Checked=true ;
                            rbt2.Checked = false;

                        }
                        else if (Condition[1].ToString().ToString().ToLower()=="minimum_date")
                        {


                            rbt2.Checked = true;
                            rbt1.Checked = false;

                        }
                    }

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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}