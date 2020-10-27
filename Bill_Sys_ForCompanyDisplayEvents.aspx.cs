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

public partial class Bill_Sys_ForCompanyDisplayEvents : PageBase
{
    Bill_Sys_Calender _bill_Sys_Calender;
    string szBillCompanyID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; ;
            txtCompanyid.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtCaseID.Text = Session["CASE_ID"].ToString();
            if (Session["CURRENTTIME"] != null)
            {
                if (Session["CALENDER_STATE"] != null && Session["CURRENT_DATE"] != null)
                {
                    BindGrid(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()), Session["CALENDER_STATE"].ToString(), Convert.ToInt32(Session["CURRENTTIME"].ToString()));
                }
            }
            else
            {
                if (Session["CALENDER_STATE"] != null && Session["CURRENT_DATE"] != null)
                {
                    BindGrid(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()), Session["CALENDER_STATE"].ToString(), 0);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_ForCompanyDisplayEvents.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void BindGrid(DateTime  date,string flag,int time)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_Calender = new Bill_Sys_Calender();
        try
        {
            if (time > 0)
            {
                //BindGrid(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()), Session["CALENDER_STATE"].ToString(), 0);
                grdDisplayEvents.DataSource = _bill_Sys_Calender.GetEventDetailList(date, flag, txtCaseID.Text, txtCompanyid.Text, time);
            }
            else
            {
                grdDisplayEvents.DataSource = _bill_Sys_Calender.GetEventDetailList(date, "FOR_COMPANYDAY", txtCaseID.Text, txtCompanyid.Text, time);
            }
            
            grdDisplayEvents.DataBind();
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
    
    protected void grdDisplayEvents_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (Session["CALENDER_STATE"].ToString() == "FOR_COMPANYDAY")
            {
                grdDisplayEvents.Columns[0].Visible = false;
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
    protected void grdDisplayEvents_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdDisplayEvents.CurrentPageIndex = e.NewPageIndex;
            if (Session["CALENDER_STATE"] != null && Session["CURRENT_DATE"] != null)
            {
                if (Session["CURRENT_TIME"] != null)
                {
                    BindGrid(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()), Session["CALENDER_STATE"].ToString(), Convert.ToInt32(Session["CURRENT_TIME"]));
                }
                else
                {
                    BindGrid(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()), Session["CALENDER_STATE"].ToString(), 0);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
