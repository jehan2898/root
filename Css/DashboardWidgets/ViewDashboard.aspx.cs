using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class ViewDashboard : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String key = Request["Key"];
        if(String.IsNullOrEmpty(key))
            Response.Redirect("~/");
        else if (!Page.IsPostBack)
        {
            DashboardSurface1.DashboardKey = key;
            WidgetTypeList1.DataBind();
            //ConfigDashBoard();
            
        }
    }

    private void ConfigDashBoard()
    {
        DashBoardBO _objDashBoardBO = new DashBoardBO();
        try
        {
            Hashtable htAllocatedTabs = new Hashtable();
            ArrayList arAllTabs = new ArrayList();
            DataTable dt = _objDashBoardBO.GetConfigDashBoard(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE);
            foreach (DataRow dr in dt.Rows)
            {
                htAllocatedTabs.Add(dr[0].ToString(), dr[0].ToString());
            }
            for (int i = 0; i < ((System.Web.UI.WebControls.ListControl)(((System.Web.UI.WebControls.CompositeControl)(WidgetTypeList1)).Controls[1])).Items.Count; i++)
            {
                arAllTabs.Add(((System.Web.UI.WebControls.ListControl)(((System.Web.UI.WebControls.CompositeControl)(WidgetTypeList1)).Controls[1])).Items[i].Text);
            }
            int totCount=arAllTabs.Count-1;
            for (int i = totCount; i >= 0; i--)
            {
                //if(!htAllocatedTabs.ContainsKey(arAllTabs[i].ToString().Trim()))
                //    ((System.Web.UI.WebControls.ListControl)(((System.Web.UI.WebControls.CompositeControl)(WidgetTypeList1)).Controls[1])).Items.RemoveAt(i);

                if(!htAllocatedTabs.ContainsKey(((System.Web.UI.WebControls.ListControl)(((System.Web.UI.WebControls.CompositeControl)(WidgetTypeList1)).Controls[1])).Items[i].Text.Trim()))
                {
                    {((System.Web.UI.WebControls.ListControl)(((System.Web.UI.WebControls.CompositeControl)(WidgetTypeList1)).Controls[1])).Items.RemoveAt(i);}

                }
            }

        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }


    //protected void DashboardSurface1_WidgetDataBound(object sender, Kalitte.Dashboard.Framework.WidgetEventArgs e)
    //{

    //    if (e.Instance.Type.Title == "Reports")
    //    {
    //        //e.Instance.Type.UserCanEdit = false;
    //        e.Instance.Type.Closable = true;
            
    //    }

    //}
    //protected void DashboardSurface1_WidgetAdding(object sender, Kalitte.Dashboard.Framework.WidgetEventArgs e)
    //{
    //    e.Instance.ConfirmClose = true;
    //}
    //protected void DashboardSurface1_WidgetLoad(object sender, Kalitte.Dashboard.Framework.WidgetEventArgs e)
    //{
    //    e.Instance.WidgetSettings.Remove(e.Instance.Key.ToString());
    //}
    protected void DashboardSurface1_DashboardCreating(object sender, Kalitte.Dashboard.Framework.DashboardEventArgs e)
    {
        String kEY= e.Instance.UserSettings.Count.ToString();
    }
}
