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

public partial class Bill_Sys_Denial : PageBase
{

    Bill_Sys_DenialBO _bill_Sys_DenialBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["TM_SZ_BILL_ID"]!=null)
                {
                    if (Request.QueryString["TM_SZ_BILL_ID"].ToString() != "")
                    {
                        Session["TM_SZ_BILL_ID"] = Request.QueryString["TM_SZ_BILL_ID"].ToString();
                        BindData();
                    }
                }
            }
            //lblMsg.Text = "";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_Denial.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

  
  


    protected void btnOK_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_DenialBO = new Bill_Sys_DenialBO();
            
            if (grdDenial.Items.Count == 0)
            {
                _bill_Sys_DenialBO.UpdateDenial(Session["TM_SZ_BILL_ID"].ToString(), 1, txtReason.Text, DateTime.Now.Date, "FirstDenial");
                BindData();
                //lblMsg.Text = "First denial saved successfully.";
            }
            else if (grdDenial.Items.Count == 1)
            {
                _bill_Sys_DenialBO.UpdateDenial(Session["TM_SZ_BILL_ID"].ToString(), 2, txtReason.Text, DateTime.Now.Date, "SecondDenial");
                BindData();
                //lblMsg.Text = "Second denial saved successfully.";
            }
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden'; parent.document.getElementById('lblMsg').value='Denial added successfully.';</script>");
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_DenialBO = new Bill_Sys_DenialBO();
            grdDenial.DataSource = _bill_Sys_DenialBO.GET_Denial(Session["TM_SZ_BILL_ID"].ToString());
            grdDenial.DataBind();
            if (grdDenial.Items.Count == 0)
            {
                lblDenial.Text = "First Denial";
            }
            else if (grdDenial.Items.Count == 1)
            {
                lblDenial.Text = "Second Denial";
            }
            else if (grdDenial.Items.Count == 2)
            {
                lblDenial.Text = "";
                txtReason.Visible = false;
                btnOK.Visible = false;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
