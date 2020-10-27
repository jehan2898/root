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
using Vintasoft.Barcode;
using Vintasoft.Pdf;
using System.Drawing;
using System.Drawing.Drawing2D;
using log4net;
using System.Data.SqlClient;
using System.Configuration;

public partial class AJAX_Pages_Bill_Sys_Save_AOB_Sign : PageBase
{
    System.Drawing.Image img;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        string sz_CasId = ((Bill_Sys_CaseObject)(Session["CASE_OBJECT"])).SZ_CASE_ID.ToString();
        string BasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
        string SignPath = sz_CompanyName + "/" + sz_CasId + "/" + "Signs/";

        if (!Directory.Exists(BasePath + SignPath))
        {
            Directory.CreateDirectory(BasePath + SignPath);

        }
        DigitalSign signobj = new DigitalSign();
        if (Request.QueryString["Sign"].ToString() == "Patient")//Patient And Doctor Sign Is Saperated For AC Doctor Screen
        {
            string PathForDB = SignPath + sz_CasId + "_AOB_Patient_Sign.bmp";
            string PatientImagePath = BasePath + PathForDB;
            try
            {
                signobj.SignSave(Request.Form["hiddenPatient"], PatientImagePath);
                SaveSign(sz_CasId, "UPDATE_PATIENT_SIGN_PATH", PathForDB);
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



        }
        else if (Request.QueryString["Sign"].ToString() == "Provider")
        {//Provider
            string PathForDB = SignPath + sz_CasId + "_AOB_Provider_Sign.bmp";
            string ProvidrImagePath = BasePath + PathForDB;
            try
            {

                signobj.SignSave(Request.Form["hidden"], ProvidrImagePath);
                SaveSign(sz_CasId, "UPDATE_PROVIDER_SIGN_PATH", PathForDB);
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

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void SaveSign(string szCaseId, string flag, string Path)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_AOB_NEW", conn);
            cmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            if (flag == "UPDATE_PATIENT_SIGN_PATH")
            {
                cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGN", Path);
            }
            else if (flag == "UPDATE_PROVIDER_SIGN_PATH")
            {
                cmd.Parameters.AddWithValue("@SZ_PROVIDER_SIGN", Path);
            }
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            cmd.Parameters.AddWithValue("@FLAG", flag);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            conn.Close();
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
