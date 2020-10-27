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

public partial class AJAX_Pages_Bill_Sys_Merge_Popup : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindListBox();
        }
    }
    public void BindListBox()
    {
        Bill_Sys_Document_Manager _Bill_Sys_Document_Manager=new Bill_Sys_Document_Manager();
        DataSet dsmergedoc = new DataSet();
        dsmergedoc = _Bill_Sys_Document_Manager.GetDocumentmanagerMerge();
        lstPDF.DataSource = dsmergedoc;
        lstPDF.DataTextField = "SZ_FILENAME";
        lstPDF.DataValueField = "SZ_IMAGEID";
        lstPDF.DataBind();
    }
}
