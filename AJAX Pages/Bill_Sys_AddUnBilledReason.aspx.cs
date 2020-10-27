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
using System.Data;
using System.Data.SqlClient;

public partial class AJAX_Pages_Bill_Sys_AddUnBilledReason : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        chkReason.Attributes.Add("onclick", "return chkcompanyname();");
        if (!IsPostBack)
        {
            chkReason.Checked = false;
            txtAddReason.Enabled = false;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        ArrayList arrAddReason = new ArrayList();
        ArrayList arrUpdateReason = new ArrayList();
        string StrReasonValue = "";

        Bill_Sys_NotesBO objAddReason = new Bill_Sys_NotesBO();
        try
        {
            if (chkReason.Checked)
            {
                StrReasonValue = "1";
            }
            else
            {
                StrReasonValue = "0";
            }
            arrAddReason = (ArrayList)Session["AddUnbilledReason"];
            for (int i = 0; i < arrAddReason.Count; i++)
            {
                string strReason = arrAddReason[i].ToString();
                arrUpdateReason.Add(strReason);
            }
            int iReturn = objAddReason.AddUnBilledReason(arrUpdateReason, txtAddReason.Text, StrReasonValue);
            if (iReturn > 0)
            {
                usrMessage.PutMessage("Reason Updated Successfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            else
            {
                usrMessage.PutMessage("Failed");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


}
