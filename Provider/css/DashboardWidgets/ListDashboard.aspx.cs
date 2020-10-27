using System;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Kalitte.Dashboard.Framework;

public partial class ListDashboard : PageBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List.DataSource = DashboardFramework.GetDashboards();
            List.DataBind();
        }
    }
    protected void List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Kalitte.Dashboard.Framework.Types.DashboardInstance instance = e.Item.DataItem as Kalitte.Dashboard.Framework.Types.DashboardInstance;
            HyperLink Link = e.Item.FindControl("link") as HyperLink;
            Link.Text = instance.Title;
            Link.NavigateUrl = "ViewDashboard.aspx?Key=" + instance.InstanceKey.ToString();


            
        }
    }
}
