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
using SIGPLUSLib;
using System.IO;
using PDFValueReplacement;
using Vintasoft.Barcode;
using Vintasoft.Pdf;
using System.Drawing;
using System.Drawing.Drawing2D;
using log4net;
using HP1;
using HP1.HP1BO;

public partial class AJAX_Pages_ProviderSign : PageBase
{
    System.Drawing.Image img;
    string DoctorImagePath = "";
    string PatientPath = "";
    string CaseBarcodePath = "";
    string sz_CompanyID = "";
    string sz_CaseID = "";
    string sz_CompanyName = "";
    string billNo = "";
    string DoctorImagePathlogical = "";
    Bill_Sys_NF3_Template objNF3Template;
    Bill_Sys_CheckoutBO _objcheckOut;
    string SignType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String Sign = Request.QueryString["Sign"].ToString();
        if (Request.QueryString["Sign"].ToString() == "HealthProvider1")//Patient And Doctor Sign Is Saperated For AC Doctor Screen
        {
            Sign = "HealthProvider1";
            SignType = "HealthProvider1";
        }
        else
        {
            Sign = "HealthProvider2";
            SignType = "HealthProvider2";
        }
        billNo = Session["BillNumber"].ToString();
        sz_CaseID=Session["HP1_Case"].ToString();
        _objcheckOut = new Bill_Sys_CheckoutBO();
        DigitalSign signobj = new DigitalSign();
        objNF3Template = new Bill_Sys_NF3_Template();
        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        
        int flag = 0;
        bool SignSaved = false;
        try
        {
            String szDefaultPath = (objNF3Template.getPhysicalPath()) + sz_CompanyName + "/" + sz_CaseID + "/Signs/";
            String szLogicaleDefaultPath = sz_CompanyName + "/" + sz_CaseID + "/Signs/";
            if (!Directory.Exists(szDefaultPath))
            {
                Directory.CreateDirectory(szDefaultPath);
            }
            szDefaultPath = szDefaultPath + DateTime.Now.ToString("MMddyyyyhhmmssffff");
        
            if (Sign == "HealthProvider1")
            {
                string ProviderImagePath = szDefaultPath + "_HealthProvider1.jpg";
                string LogicalProviderImagePath = ProviderImagePath.Replace(objNF3Template.getPhysicalPath(),"");// szLogicaleDefaultPath + DateTime.Now.ToString("MMddyyyyhhmmssffff") + "_HealthProvider1.jpg";
                SignSaved = signobj.SignSave(Request.Form["hidden"], ProviderImagePath);
                if (!SignSaved)
                {
                    Label1.Text = "Sign is corrupted. Please re-sign.";
                    Label1.Visible = true;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "kk11", "ShowPatientSignaturePopup();", true);
                    //Response.Redirect("Bill_Sys_CO_CHNotesUPDATE.aspx?EID=" + sz_EventID + "&CaseID=" + Session["ChkOutCaseID"].ToString() + "&cmp=" + sz_CompanyID, false);
                }
                //SaveSignPath = SaveSignPath + "_HealthProvider1.jpg";
                Hp1DAL objHp1 = new Hp1DAL();
                objHp1.SaveProviderSignPath(sz_CompanyID, sz_CaseID, billNo, LogicalProviderImagePath);
            }
            else
            {
                string ProviderImagePath2 = szDefaultPath + "_HealthProvider2.jpg";
                string LogicalProviderImagePath2 = ProviderImagePath2.Replace(objNF3Template.getPhysicalPath(), "");// szLogicaleDefaultPath + DateTime.Now.ToString("MMddyyyyhhmmssffff") + "_HealthProvider2.jpg";
                SignSaved = signobj.SignSave(Request.Form["hiddenpatient"], ProviderImagePath2);
                if (!SignSaved)
                {
                    Label1.Text = "Sign is corrupted. Please re-sign.";
                    Label1.Visible = true;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "kk11", "ShowPatientSignaturePopup();", true);
                    //Response.Redirect("Bill_Sys_CO_CHNotesUPDATE.aspx?EID=" + sz_EventID + "&CaseID=" + Session["ChkOutCaseID"].ToString() + "&cmp=" + sz_CompanyID, false);
                }
                //SaveSignPath = SaveSignPath + "_HealthProvider2.jpg";
                Hp1DAL objHp1 = new Hp1DAL();
                objHp1.SaveProviderSignPath2(sz_CompanyID, sz_CaseID, billNo, LogicalProviderImagePath2);
            }


            //Page.RegisterStartupScript("mm", "<script type='text/javascript'>window.close()</script>");

        }
        catch (Exception ex)
        {
            Label1.Text = "Page Laod :" + ex.ToString();
            //  throw ex;
        }
        if (SignSaved)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "kk11", "ClosePopup();", true);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (SignType == "HealthProvider1")
        {
            Response.Redirect("Bill_Sys_ProviderSign.aspx", false);
        }
        else if (SignType == "HealthProvider2")
        {
            Response.Redirect("Bill_Sys_ProviderSign2.aspx", false);
            //ScriptManager.RegisterStartupScript(this, GetType(), "kk11", "ClosePopup();", true);
        }
    }
}