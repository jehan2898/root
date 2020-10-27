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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;

public partial class Popup_PatientSignature : PageBase
{
    private readonly string STR_PREFIX_PATIENT_SIGN = "_PatientSign.jpg";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["EventId"] != null)
        {
            Session["EventID"] = Request.QueryString["EventId"].ToString();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        bool SignSaved = false;
        string SaveSignPath = "";
        String szDefaultPath = "";
        String Sign = "";

        System.Drawing.Image img;
        string DoctorImagePath = "";
        string PatientPath = "";
        string CaseBarcodePath = "";
        string sz_CompanyID = "";
        string sz_CompanyName = "";
        string sz_EventID = "";
        string DoctorImagePathlogical = "";
        Bill_Sys_NF3_Template objNF3Template;
        Bill_Sys_CheckoutBO _objcheckOut;
        string SignType = "";


        try
        {
            _objcheckOut = new Bill_Sys_CheckoutBO();
            DigitalSign signobj = new DigitalSign();
            objNF3Template = new Bill_Sys_NF3_Template();
            DataSet dsObj = new DataSet();
            dsObj = _objcheckOut.PatientName((Session["EventID"].ToString()));
            Session["ChkOutCaseID"] = dsObj.Tables[0].Rows[0][1].ToString();

            sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() + "/Signs/";
            SaveSignPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() + "/Signs/";
            if (!Directory.Exists(szDefaultPath))
            {
                Directory.CreateDirectory(szDefaultPath);
            }

            SignatureSaver.doctor.dao.EventDAO objChDAO = new SignatureSaver.doctor.dao.EventDAO();
            objChDAO.CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            objChDAO.EventID = Convert.ToInt64(Session["EventID"].ToString());
            objChDAO.SignatureByteData = Request.Form["hidden"];
            objChDAO.PatientSignatureLogicalPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() + "/Signs/" + Convert.ToInt64(Session["EventID"].ToString()) + STR_PREFIX_PATIENT_SIGN;
            objChDAO.PatientSignaturePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() + "/Signs/" + Convert.ToInt64(Session["EventID"].ToString()) + STR_PREFIX_PATIENT_SIGN;
            //objChDAO.Speciality_Name = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_PROCEDURE_GROUP_NAME;  // SVN
            //objChDAO.SpecialityID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_SPECIALITYID;   // SVN
            string sReturn = new SignatureSaver.doctor.services.ServiceDoctor().SavePatientSign(objChDAO);

            if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_PATIENT_SIGN_CORRUPT))
            {
                lblMsg.Text = "Sign is corrupted please resign.";
                lblMsg.Visible = true;
            }

            if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_PATIENT_SIGN_ABSENT))
            {
                lblMsg.Text = "Sign does not saved. please contact admin.";
                lblMsg.Visible = true;
            }
            if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_PATIENT_SIGN_DATABASE_FAIL))
            {
                lblMsg.Text = "Sign does not stored. please contact admin.";
                lblMsg.Visible = true;
            }
            if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_PATIENT_SIGN_SAVED_SUCCESS))
            {
                lblMsg.Text = "Sign saved succefully";
                lblMsg.Visible = true;
                if (chkShow.Checked)
                {
                    string openPath = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
                    imagepatient.ImageUrl = openPath + objChDAO.PatientSignatureLogicalPath;
                    imagepatient.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "kk11", "ClosePopup();", true);
                }
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}