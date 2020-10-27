using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;

public partial class Popup_UP_PatientList : PageBase
{
    Bill_Sys_BillingCompanyObject obj = null;
    protected string szCompanyName = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {

        obj = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
        
        if (obj != null)
        {
            szCompanyName = obj.SZ_COMPANY_NAME;
        }
        
        lblMessage.Text = "";
        lblMessage.Text = "";

        if (!IsPostBack)
        {
            loadexisting();
        }
    }

    protected void btnRemovePreferences_Click(object sender, EventArgs e)
    {
        string userid = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        string companyid = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string pagename = "SearchCase";
        UserPreferences obj = new UserPreferences();
        obj.UP_Remove(userid,companyid,pagename);
        lblMessage.Visible = true;
        lblMessage.Text = "Your preferences were removed successfully";
    }
    
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string load = "";
        if (rbt1.Checked)
        {
            load = "DO_NOT_LOAD_PATIENT_LIST=true";

            load = load + ",SHOW_ONLY_LAST=" + "00";
            load = load + ",SHOW_ONLY_LAST40VIEWED=" + "00";
        }
        else if (rbt2.Checked)
        {

            load = "DO_NOT_LOAD_PATIENT_LIST=false";
            load = load + ",SHOW_ONLY_LAST="  + ddl_list.SelectedItem.Value.ToString();
            load = load + ",SHOW_ONLY_LAST40VIEWED=" + "00";
        }
        else
        {
            load = "DO_NOT_LOAD_PATIENT_LIST=false";
            load = load + ",SHOW_ONLY_LAST=" + "00";
            load = load + ",SHOW_ONLY_LAST40VIEWED=" + ddl_list40.SelectedItem.Value.ToString();
        }

        //load = load + ",SHOW_ONLY_LAST="+ ddl_list.SelectedItem.Value.ToString();
        string userid=((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        string companyid=((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string pagename="SearchCase";
       
        UserPreferences obj = new UserPreferences();
        obj.save_Delivery_report(pagename, userid, companyid, load);
        lblMessage.Visible = true;
        lblMessage.Text = "Your preferences were changed successfully";
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
        
        ds = obj.GetUserPreferences(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, "SearchCase");
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["sz_preferences"].ToString() != "")
            {
                string[] Condition = ds.Tables[0].Rows[0]["sz_preferences"].ToString().Split(',');
                for (int i = 0; i < Condition.Length; i++)
                {
                    string[] KeyVal = Condition[i].ToString().Split('=');
                    try
                    {

                        if (KeyVal[0].ToString() == "DO_NOT_LOAD_PATIENT_LIST" && KeyVal[1].ToString() == "true")
                        {
                            //chk_load.Checked = true;
                            rbt1.Checked = true;
                        }
                        if (KeyVal[0].ToString() == "SHOW_ONLY_LAST" && KeyVal[1].ToString() != "00")
                        {
                            rbt2.Checked = true;
                            foreach (Object selecteditem in ddl_list.Items)
                            {
                               
                                if (selecteditem.ToString().Equals(KeyVal[1].ToString()))
                                {
                                    ddl_list.Text= KeyVal[1];
                                   
                                }
                            }
                        }

                        if (KeyVal[0].ToString() == "SHOW_ONLY_LAST40VIEWED=" && KeyVal[1].ToString() != "00")
                        {
                            rbt3.Checked = true;
                            foreach (Object selecteditem in ddl_list40.Items)
                            {

                                if (selecteditem.ToString().Equals(KeyVal[1].ToString()))
                                {
                                    ddl_list40.Text = KeyVal[1];
                                    //ddl_list.Text = KeyVal[1];

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
                }
            }
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}