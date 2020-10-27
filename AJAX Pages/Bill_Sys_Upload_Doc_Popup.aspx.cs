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
using OboutInc.EasyMenu_Pro;
using System.IO;

public partial class AJAX_Pages_Bill_Sys_Upload_Doc_Popup : PageBase
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["UploadReport_VisitType"] = Request.QueryString["Type"].ToString();
            Session["UploadReport_DoctorId"] = Request.QueryString["Doc"].ToString();
            string Pid = "";
            if (Request.QueryString["PGID"].ToString() == null || Request.QueryString["PGID"].ToString() == "")
            {
                Pid = "&nbsp";
            }
            else
            {
                Pid = Request.QueryString["PGID"].ToString();
            }
            Bill_Sys_Upload_VisitReport GetSpecialty = new Bill_Sys_Upload_VisitReport();
            string ProcedureGroupId = "";
            if ((Pid.Substring(0, 1) == "&") || (Pid.Substring(0, 1) == "n"))
                ProcedureGroupId = GetSpecialty.GetDoctorSpecialty(Session["UploadReport_DoctorId"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString());
            else
                ProcedureGroupId = Pid;

            Session["UploadReport_ProcedureGroupId"] = ProcedureGroupId;
            Session["UploadReport_EventId"] = Request.QueryString["Eve"].ToString();
        }
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if ((Session["UploadReport_DoctorId"] != null) && (Session["UploadReport_VisitType"] != null) && (Session["UploadReport_EventId"] != null) && (Session["UploadReport_ProcedureGroupId"] != null))
            {
                if ((Session["UploadReport_DoctorId"].ToString() != "") && (Session["UploadReport_VisitType"].ToString() != "") && (Session["UploadReport_ProcedureGroupId"].ToString() != "") && (Session["UploadReport_EventId"].ToString() != ""))
                {
                    Bill_Sys_Upload_VisitReport _bill_Sys_Report_Upload = new Bill_Sys_Upload_VisitReport();
                    if (ReportUpload.HasFile)
                    {
                        //check specialty node
                        ArrayList UploadObj = new ArrayList();
                        UploadObj.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString());
                        UploadObj.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString());
                        UploadObj.Add(Session["UploadReport_DoctorId"].ToString());
                        UploadObj.Add(Session["UploadReport_VisitType"].ToString());
                        UploadObj.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME.ToString());
                        UploadObj.Add(ReportUpload.FileName);
                        UploadObj.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                        UploadObj.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
                        UploadObj.Add(Session["UploadReport_EventId"].ToString());
                        UploadObj.Add(Session["UploadReport_ProcedureGroupId"].ToString());
                        string Result = _bill_Sys_Report_Upload.Upload_Report_For_Visit(UploadObj);
                        if (Result != "Failed")
                        {
                            if (!(Directory.Exists(Result)))
                                Directory.CreateDirectory(Result);
                            ReportUpload.SaveAs(Result + ReportUpload.FileName);
                            Msglbl.Text = "Document Saved Successfully";
                        }
                        else
                        {

                            Msglbl.Text = "Unable to save the Document";
                        }
                    }
                    else
                        Msglbl.Text = "No File Selected";
                }
            }
            else
                Msglbl.Text = "Doctor or VisitType unknown ";
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
