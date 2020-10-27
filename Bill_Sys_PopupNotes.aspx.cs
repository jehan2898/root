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
using NOTES_OBJECT;
public partial class Bill_Sys_PopupNotes : PageBase
{
    private PopupBO _popupBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        btnNoteSave.Attributes.Add("onclick", "return formValidator('form1','txtNoteDesc,extddlNotesType');");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_PopupNotes.aspx");
        }
        #endregion
    }

    protected void btnNoteSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_NotesBO _objNotesBO = new Bill_Sys_NotesBO();
            ArrayList _objAL = new ArrayList();
            _objAL.Add(Note_Code.New_Note_Added);
            if (Request.QueryString["Flag"] != null )
            {
                if (Request.QueryString["Flag"].ToString() == "True")
                {
                    _popupBO = new PopupBO();
                    _objAL.Add(_popupBO.GetCompanyID(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID));
                }
                else
                {
                    _objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                }
            }
            else
            {
                _objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            }
            _objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            _objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
            if (chkReminderPopup.Checked == true)
                _objAL.Add("NTY0002");
            else
                _objAL.Add(extddlNotesType.Text);
            _objAL.Add(txtNoteDesc.Text);
            _objNotesBO.SaveNotes(_objAL);
            txtNoteDesc.Text = "";
            chkReminderPopup.Checked = false;
            lblMsg.Text = "Notes added.";
            lblMsg.Visible = true;
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
