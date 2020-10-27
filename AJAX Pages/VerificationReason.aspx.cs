using DevExpress.Web;
using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AJAX_Pages_VerificationReason : Page, IRequiresSessionState
{

    protected void btnadd_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txt_reasonid.Text = "0";
            Verificationfunction verificationfunction = new Verificationfunction();
            string text = this.txt_reasonid.Text;
            string str = this.txt_companyid.Text;
            string text1 = this.memo_descver.Text;
            string sZUSERID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            string sZUSERID1 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            if (this.memo_descver.Text != "")
            {
                verificationfunction.Save_Reexamination(text, str, text1, sZUSERID, sZUSERID1);
                this.lblmsg.Visible = true;
                this.lblmsg.Text = "Verification Saved Successfully";
            }
            else
            {
                this.ClearControl();
                
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
        
        this.loadverification();
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.memo_descver.Text = "";
            this.lblmsg.Visible = false;

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


    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Verificationfunction verificationfunction = new Verificationfunction();
            string text = this.txt_reasonid.Text;
            string str = this.txt_companyid.Text;
            string text1 = this.memo_descver.Text;
            string sZUSERID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            string sZUSERID1 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            verificationfunction.Update_Verification(text, str, text1, sZUSERID1, sZUSERID);
            this.ClearControl();
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
       
        //this.ClearControl();
        this.loadverification();
        this.btnadd.Enabled = true;
        this.btnupdate.Enabled = false;
        this.memo_descver.Text = "";
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void deleteverification()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Verificationfunction verificationfunction = new Verificationfunction();
            verificationfunction.delete_Verification(this.txt_reasonid.Text, this.txt_companyid.Text);
            this.ClearControl();
            this.lblmsg.Visible = true;
            this.lblmsg.Text = "Verification Successfully Deleted.";
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
        this.loadverification();
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdVisits_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        if (e.CommandArgs.CommandName.ToString().ToLower() == "select")
        {
            int visibleIndex = e.VisibleIndex;
            ASPxGridView aSPxGridView = this.grdVisits;
            string[] strArrays = new string[] { "sz_reason" };
            string str = aSPxGridView.GetRowValues(visibleIndex, strArrays).ToString();
            ASPxGridView aSPxGridView1 = this.grdVisits;
            string[] strArrays1 = new string[] { "i_reason_id" };
            string str1 = aSPxGridView1.GetRowValues(visibleIndex, strArrays1).ToString();
            this.memo_descver.Text = str;
            this.txt_reasonid.Text = str1;
            this.btnadd.Enabled = false;
            this.btnupdate.Enabled = true;
        }
        if (e.CommandArgs.CommandName.ToString().ToLower() == "delete")
        {
            int num = e.VisibleIndex;
            ASPxGridView aSPxGridView2 = this.grdVisits;
            string[] strArrays2 = new string[] { "sz_reason" };
            aSPxGridView2.GetRowValues(num, strArrays2).ToString();
            ASPxGridView aSPxGridView3 = this.grdVisits;
            string[] strArrays3 = new string[] { "i_reason_id" };
            string str2 = aSPxGridView3.GetRowValues(num, strArrays3).ToString();
            ASPxGridView aSPxGridView4 = this.grdVisits;
            string[] strArrays4 = new string[] { "sz_company_id" };
            string str3 = aSPxGridView4.GetRowValues(num, strArrays4).ToString();
            this.txt_reasonid.Text = str2;
            this.txt_companyid.Text = str3;
            this.deleteverification();
        }
    }

    protected void loadverification()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataSet dataSet = (new Verificationfunction()).Loadverification(this.txt_companyid.Text);
            this.grdVisits.DataSource = dataSet;
            this.grdVisits.DataBind();
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
        if (!base.IsPostBack)
        {
            this.txt_companyid.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.loadverification();
            this.btnadd.Enabled = true;
            this.btnupdate.Enabled = false;
        }
    }
}