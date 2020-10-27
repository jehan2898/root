using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DownloadFiles : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ArrayList downloadFiles = (ArrayList )Session["Download_Files"];
        if(downloadFiles != null)
        {
            for (int i = 0; i < downloadFiles.Count; i++)
            {
                HyperLink link = new HyperLink();
                link.ID = "lnk_" + i;
                link.NavigateUrl = (string) downloadFiles[i];
                link.Text = "Click to download file";
                this.pnlDownloads.Controls.Add(link);
            }
        }

        Session["Download_Files"] = null;
    }
}