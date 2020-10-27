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
using Componend;
using NOTES_OBJECT;

public partial class Bill_Sys_Verification : PageBase
{

    Bill_Sys_DenialBO _bill_Sys_DenialBO;
    SaveOperation _saveOperation;
    Bill_Sys_NotesBO _bill_Sys_Notes;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["TM_SZ_BILL_ID"]!=null && Request.QueryString["caseId"]!=null)
                {
                    if (Request.QueryString["TM_SZ_BILL_ID"].ToString() != "" && Request.QueryString["caseId"].ToString()!="")
                    {
                        Session["TM_SZ_BILL_ID"] = Request.QueryString["TM_SZ_BILL_ID"].ToString();
                        txtCaseID.Text = Request.QueryString["caseID"].ToString();
                        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        txtNoteCode.Text = Note_Code.New_Note_Added;
                    }
                }
            }
            //lblMsg.Text = "";
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
            cv.MakeReadOnlyPage("Bill_Sys_Verification.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

  
  


    protected void btnOK_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            extddlNoteType.Text = "NTY0001";
            txtNoteDesc.Text = "Bill # " + Session["TM_SZ_BILL_ID"].ToString() + " - Verification Sent";
            _saveOperation = new SaveOperation();
            
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "notes.xml";
            _saveOperation.SaveMethod();
            _bill_Sys_Notes = new Bill_Sys_NotesBO();
            _bill_Sys_Notes.UpdateBillStatusForVerification(Session["TM_SZ_BILL_ID"].ToString(),0,txtNotes.Text);

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>  parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.reload(); parent.document.getElementById('lblMsg').value='Denial added successfully.';</script>");
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
