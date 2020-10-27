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
using System.IO;
using System.Collections;
using System.Text;

public partial class AJAX_Pages_AnswerVerificationPopup : PageBase
{
    string sz_CompanyID = "";
    string sz_Bill_Number = "";
    string sz_CaseID = "";
    string sz_specialty = "";
    string sz_Verification_ID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        sz_Bill_Number = Request.QueryString["BillNumber"].ToString();
        sz_specialty = Request.QueryString["Specialty"].ToString();
        sz_Verification_ID = Request.QueryString["Verification_ID"].ToString();

        sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
        Bill_Sys_UploadFile _objUploadFile = new Bill_Sys_UploadFile();
        ArrayList arrCaseId = new ArrayList();
        ArrayList arrBillNo = new ArrayList();
        ArrayList arrSpec = new ArrayList();
        arrCaseId.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
        arrBillNo.Add(sz_Bill_Number);
        arrSpec.Add(sz_specialty);
        _objUploadFile.sz_bill_no = arrBillNo;
        _objUploadFile.sz_case_id = arrCaseId;
        _objUploadFile.sz_speciality_id = arrSpec;
        ViewState["VSUpload"] = _objUploadFile;
        //ScriptManager.RegisterStartupScript(this, GetType(), "showUploadFilePopupOnAnswerVerification", "showUploadFilePopupOnAnswerVerification('" + sz_Bill_Number + "','" + sz_Verification_ID + "','" + sz_specialty + "');", true);
    }
    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        AnswerVerification template = new AnswerVerification();
        new ArrayList();
        ArrayList list = new ArrayList();
        try
        {
            if (!this.fuUploadReport.HasFile)
            {
                this.Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');showUploadFilePopupOnAnswerVerification();</script>");
                return;
            }
            else
            {
                Bill_Sys_UploadFile fileUpload = (Bill_Sys_UploadFile)ViewState["VSUpload"]; 
                ArrayList arrSpec = new ArrayList();
                ArrayList arrCaseId = new ArrayList();
                string sz_FileName = "";
                string sz_File = "";
                string sz_UserName = "";
                string sz_UserId = "";
                ArrayList UploadObj = new ArrayList();

                fileUpload.sz_FileName = fuUploadReport.FileName;
                fileUpload.sz_File = this.fuUploadReport.FileBytes;
                fileUpload.sz_UserName=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME.ToString();
                fileUpload.sz_UserId=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                fileUpload.sz_company_id = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                fileUpload.sz_flag = "VR";
                //fileUpload.sz_case_id = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                list = new FileUpload().UploadFile(fileUpload);
                
                if (list != null)
                {
                    template.InsertPaymentImage(sz_Bill_Number, sz_CompanyID, list[0].ToString(), ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, sz_Verification_ID,"VERIFICATIONANSWER");
                }

                usrMessage1.PutMessage("File Upload Successfully!");
                usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage1.Show();
                //this.BindGrid();
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