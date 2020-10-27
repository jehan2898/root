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

public partial class AJAX_Pages_Bill_Sys_ViewUnBilledReason : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string szProcID = Request.QueryString["Eventprocid"].ToString();
            Bill_Sys_NotesBO objNotes = new Bill_Sys_NotesBO();
            DataSet ds = new DataSet();
            ds = objNotes.GetUnBilledReason(szProcID);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["BT_UNBILLABLE"].ToString().ToLower() == "true")
                    {
                        chkReason.Checked = true;
                    }
                    else
                    {
                        chkReason.Checked = false;
                    }
                    if (ds.Tables[0].Rows[0]["SZ_UNBILLABLE_REASON"].ToString().Trim() != "")
                    {
                        txtAddReason.Text = ds.Tables[0].Rows[0]["SZ_UNBILLABLE_REASON"].ToString().Trim();
                    }


                }
            }

        }
    }
}
