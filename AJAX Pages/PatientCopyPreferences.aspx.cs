using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;

public partial class AJAX_Pages_PatientCopyPreferences : PageBase
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

    private void loadexisting()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        DataSet ds = new DataSet();
        UserPreferences obj = new UserPreferences();

        ds = obj.GetUserPreferences(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, "CopyPatients");
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
                        if (KeyVal.Length > 1)
                        {
                            if (KeyVal[1].ToString().ToLower() == "adjuster")
                            {
                                chkAdjuster.Checked = true;
                            }
                            if (KeyVal[1].ToString().ToLower() == "attorney")
                            {
                                chkAttorney.Checked = true;
                            }
                        }
                        else
                        {
                            if (KeyVal[0].ToString().ToLower() == "adjuster")
                            {
                                chkAdjuster.Checked = true;
                            }
                            if (KeyVal[0].ToString().ToLower() == "attorney")
                            {
                                chkAttorney.Checked = true;
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

    protected void btnRemovePreferences_Click(object sender, EventArgs e)
    {
        string userid = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        string companyid = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string pagename = "CopyPatients";
        UserPreferences obj = new UserPreferences();
        obj.UP_Remove(userid, companyid, pagename);
        lblMessage.Visible = true;
        lblMessage.Text = "Your preferences were removed successfully";
        clearControl();
    }

    private void clearControl()
    {
        chkAttorney.Checked = false;
        chkAdjuster.Checked = false;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string load = "";
        if (chkAttorney.Checked)
        {
            load = "COPY_ADDITIONAL_ENTITIES=ATTORNEY";
        }
        if (chkAdjuster.Checked)
        {
            if (load == "")
            {
                load = "COPY_ADDITIONAL_ENTITIES=ADJUSTER";
            }
            else
            {
                load = "COPY_ADDITIONAL_ENTITIES=ATTORNEY,ADJUSTER";
            }
        }
        else
        {
            
        }

        //load = load + ",SHOW_ONLY_LAST="+ ddl_list.SelectedItem.Value.ToString();
        string userid = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        string companyid = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string pagename = "CopyPatients";

        UserPreferences obj = new UserPreferences();
        obj.save_Delivery_report(pagename, userid, companyid, load);
        lblMessage.Visible = true;
        lblMessage.Text = "Your preferences were changed successfully";
    }
}