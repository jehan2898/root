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
using Kalitte.Dashboard.Framework;
using Kalitte.Dashboard.Framework.Design;
using Kalitte.Dashboard.Framework.Modules;
using Kalitte.Dashboard.Framework.Types;

public partial class CreateDashboard : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DashboardInstance instance = new DashboardInstance();
        instance.InstanceKey=Guid.NewGuid();
        instance.Title= ctlTitle.Text;
        instance.Icon = Kalitte.Dashboard.Framework.WidgetIcon.Zoom;
        instance.Username = System.Threading.Thread.CurrentPrincipal.Identity.Name;
        
        instance.Columns.Add(new DashboardColumn(45));
        instance.Columns.Add(new DashboardColumn(55));
        DashboardFramework.CreateDashboard(instance);
        Label2.Text = "Dashboard Created";
    }
}
