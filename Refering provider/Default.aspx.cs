using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Refering_provider_Default : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    { }
         private void ListControlCollections()
    {
        ArrayList controlList = new ArrayList();
        AddControls(Page.Controls, controlList);

        foreach (string str in controlList)
        {
        Response.Write(str + "<br/>");
        }
       Response.Write("Total Controls:" + controlList.Count);
    }

    private void AddControls(ControlCollection page, ArrayList controlList)
    {
        foreach (Control c in page)
        {
            if (c.ID != null)
            {
                controlList.Add(c.ID);
            }

            if (c.HasControls())
            {
                AddControls(c.Controls, controlList);
            }
        }
    }
}