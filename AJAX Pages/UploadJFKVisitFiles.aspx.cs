using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class AJAX_Pages_UploadJFKVisitFiles : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UploadButton.Attributes.Add("onclick", "return Validate();");
            txtCaseID.Text = Request.QueryString["caseid"].ToString();
            txtVisitId.Text = Request.QueryString["visitId"].ToString();
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtCompanyName.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            txtUserID.Text=((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            txtUserName.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString();
             
        }
                  

    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            EmployerVisitDO objEmployerVisitDO = new EmployerVisitDO();
            objEmployerVisitDO.CaseID = txtCaseID.Text;
            objEmployerVisitDO.CompanyID =  txtCompanyID.Text;
            objEmployerVisitDO.CompanyName = txtCompanyName.Text;
            objEmployerVisitDO.UserID = txtUserID.Text;
            objEmployerVisitDO.UserName = txtUserName.Text;            
            objEmployerVisitDO.VisitId = txtVisitId.Text;
            objEmployerVisitDO.FileName = ReportUpload.FileName;
            EmployerBO objEmployerBO = new EmployerBO();
            string sPath = objEmployerBO.SaveVisitDocument(objEmployerVisitDO);
            string sBasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
            if (!Directory.Exists(sBasePath + sPath))
            {
                Directory.CreateDirectory(sBasePath + sPath);

            }
            if(sPath!="")
            {
                ReportUpload.SaveAs(sBasePath + sPath  + ReportUpload.FileName);
                lblmsg.Text="File saved successfully";
                lblmsg.Visible = true;
            }else
            {
                lblmsg.Text="unable to save file";
                lblmsg.Visible = true;
            }

        }
        catch (Exception ex)
        {

            lblmsg.Text = ex.ToString();
            lblmsg.Visible = true;
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