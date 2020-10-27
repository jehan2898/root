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
using System.Data.SqlClient;

public partial class AJAX_Pages_PhysicianSign : PageBase
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
    string sz_signDt = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String Sign = Request.QueryString["Sign"].ToString();
        if (Request.QueryString["Sign"].ToString() == "Physician")//Patient And Doctor Sign Is Saperated For AC Doctor Screen
        {
            Sign = "Physician";
            SignType = "Physician";
        }
        else if (Request.QueryString["Sign"].ToString() == "NotaryPublic")
        {
            Sign = "NotaryPublic";
            SignType = "NotaryPublic";
        }
        else
        {
            Sign = "Others";
            SignType = "Others";
        }
        
        if (!Page.IsPostBack)
        {            
            billNo = Session["BillNumber"].ToString();
            sz_CaseID = Session["HPJ1_Case"].ToString();
            sz_signDt = Session["SignDt"].ToString();
            _objcheckOut = new Bill_Sys_CheckoutBO();
            DigitalSign signobj = new DigitalSign();
            objNF3Template = new Bill_Sys_NF3_Template();
            sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;

            int flag = 0;
            bool SignSaved = false;
            try
            {
                String szDefaultPath = (objNF3Template.getPhysicalPath()) + sz_CompanyName + "/" + sz_CaseID + "/HPJ1/";
                String szLogicaleDefaultPath = sz_CompanyName + "/" + sz_CaseID + "/HPJ1/";
                if (!Directory.Exists(szDefaultPath))
                {
                    Directory.CreateDirectory(szDefaultPath);
                }
                szDefaultPath = szDefaultPath + DateTime.Now.ToString("MMddyyyyhhmmssffff");

                if (Sign == "Physician")
                {
                    string PhysicianImagePath = szDefaultPath + "_Physician.jpg";
                    string LogicalImagePath = PhysicianImagePath.Replace(objNF3Template.getPhysicalPath(), "");// szLogicaleDefaultPath + DateTime.Now.ToString("MMddyyyyhhmmssffff") + "_HealthProvider1.jpg";
                    SignSaved = signobj.SignSave(Request.Form["hidden"], PhysicianImagePath);
                    if (!SignSaved)
                    {
                        Label1.Text = "Sign is corrupted. Please re-sign.";
                        Label1.Visible = true;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "kk11", "ShowPatientSignaturePopup();", true);
                        //Response.Redirect("Bill_Sys_CO_CHNotesUPDATE.aspx?EID=" + sz_EventID + "&CaseID=" + Session["ChkOutCaseID"].ToString() + "&cmp=" + sz_CompanyID, false);
                    }
                    //SaveSignPath = SaveSignPath + "_HealthProvider1.jpg";
                    //Hp1DAL objHp1 = new Hp1DAL();
                    //objHp1.SaveProviderSignPath(sz_CompanyID, sz_CaseID, billNo, LogicalProviderImagePath);

                    //UPDATE SIGN IN HPJ1
                    else
                    {
                        Session["HPJ1_Physician_HPJ1_Sign_Path"] = LogicalImagePath;
                        Session["HPJ1_Physician_HPJ1_Sign_Success"] = "1";
                        //SaveSign(sz_CompanyID, sz_CaseID, billNo, LogicalImagePath, sz_signDt);
                    }

                    
                }
                else if (Sign == "NotaryPublic")
                {
                    string NotaryPublicImagePath = szDefaultPath + "_NotaryPublic.jpg";
                    string LogicalImagePath = NotaryPublicImagePath.Replace(objNF3Template.getPhysicalPath(), "");// szLogicaleDefaultPath + DateTime.Now.ToString("MMddyyyyhhmmssffff") + "_HealthProvider1.jpg";
                    SignSaved = signobj.SignSave(Request.Form["hidden"], NotaryPublicImagePath);
                    if (!SignSaved)
                    {
                        Label1.Text = "Sign is corrupted. Please re-sign.";
                        Label1.Visible = true;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "kk11", "ShowPatientSignaturePopup();", true);
                        //Response.Redirect("Bill_Sys_CO_CHNotesUPDATE.aspx?EID=" + sz_EventID + "&CaseID=" + Session["ChkOutCaseID"].ToString() + "&cmp=" + sz_CompanyID, false);
                    }
                    //SaveSignPath = SaveSignPath + "_HealthProvider1.jpg";
                    //Hp1DAL objHp1 = new Hp1DAL();
                    //objHp1.SaveProviderSignPath(sz_CompanyID, sz_CaseID, billNo, LogicalProviderImagePath);

                    //UPDATE SIGN IN HPJ1
                    else
                    {
                        Session["HPJ1_Notary_HPJ1_Sign_Path"] = LogicalImagePath;
                        Session["HPJ1_Notary_HPJ1_Sign_Success"] = "1";
                        //SaveSign(sz_CompanyID, sz_CaseID, billNo, LogicalImagePath, sz_signDt);
                    }
                }
                else
                {
                    string OtherImagePath = szDefaultPath + "_Other.jpg";
                    string LogicalImagePath = OtherImagePath.Replace(objNF3Template.getPhysicalPath(), "");// szLogicaleDefaultPath + DateTime.Now.ToString("MMddyyyyhhmmssffff") + "_HealthProvider2.jpg";
                    SignSaved = signobj.SignSave(Request.Form["hidden"], OtherImagePath);
                    if (!SignSaved)
                    {
                        Label1.Text = "Sign is corrupted. Please re-sign.";
                        Label1.Visible = true;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "kk11", "ShowPatientSignaturePopup();", true);
                        //Response.Redirect("Bill_Sys_CO_CHNotesUPDATE.aspx?EID=" + sz_EventID + "&CaseID=" + Session["ChkOutCaseID"].ToString() + "&cmp=" + sz_CompanyID, false);
                    }
                    //SaveSignPath = SaveSignPath + "_HealthProvider2.jpg";

                    //Hp1DAL objHp1 = new Hp1DAL();
                    //objHp1.SaveProviderSignPath2(sz_CompanyID, sz_CaseID, billNo, LogicalPhysicianImagePath2);

                    //UPDATE HPJ1
                    else
                    {
                        Session["HPJ1_Other_HPJ1_Sign_Path"] = LogicalImagePath;
                        Session["HPJ1_Other_HPJ1_Sign_Success"] = "1";
                        //SaveSign(sz_CompanyID, sz_CaseID, billNo, LogicalImagePath, sz_signDt);
                    }
                    
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
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (SignType == "Physician")
        {
            Response.Redirect("Bill_Sys_HPJ1_Physician_Signature.aspx", false);
        }
        else if (SignType == "NotaryPublic")
        {
            Response.Redirect("Bill_Sys_HPJ1_NotaryPublic_Signature.aspx", false);            
        }
        else
        {
            Response.Redirect("Bill_Sys_HPJ1_Other_Signature.aspx", false);            
        }
    }

    private void SaveSign(string sz_CompanyID, string sz_CaseID, string billNo, string LogicalImagePath, string sz_signDt)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();

        try
        {
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_SAVE_HPJ1_SIGN", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_CaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", billNo);
            if (SignType == "Physician")
            {
                sqlCmd.Parameters.AddWithValue("@SZ_PHYSICIANS_SIGN_PATH", LogicalImagePath);
                sqlCmd.Parameters.AddWithValue("@DT_PHYSICIANS_SIGN_DATE", sz_signDt);
                sqlCmd.Parameters.AddWithValue("@SZ_NOTARY_PUBLIC_SIGN_PATH", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@DT_NOTARY_PUBLIC_SIGN_DATE", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@SZ_OTHERS_SIGN_PATH", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@DT_OTHERS_SIGN_DATE", DBNull.Value);
            }
            else if (SignType == "NotaryPublic")
            {
                sqlCmd.Parameters.AddWithValue("@SZ_PHYSICIANS_SIGN_PATH", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@DT_PHYSICIANS_SIGN_DATE", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@SZ_NOTARY_PUBLIC_SIGN_PATH", LogicalImagePath);
                sqlCmd.Parameters.AddWithValue("@DT_NOTARY_PUBLIC_SIGN_DATE", sz_signDt);
                sqlCmd.Parameters.AddWithValue("@SZ_OTHERS_SIGN_PATH", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@DT_OTHERS_SIGN_DATE", DBNull.Value);
            }
            else
            {
                sqlCmd.Parameters.AddWithValue("@SZ_PHYSICIANS_SIGN_PATH", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@DT_PHYSICIANS_SIGN_DATE", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@SZ_NOTARY_PUBLIC_SIGN_PATH", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@DT_NOTARY_PUBLIC_SIGN_DATE", DBNull.Value);
                sqlCmd.Parameters.AddWithValue("@SZ_OTHERS_SIGN_PATH", LogicalImagePath);
                sqlCmd.Parameters.AddWithValue("@DT_OTHERS_SIGN_DATE", sz_signDt);
            }
            sqlCmd.Parameters.AddWithValue("@FLAG", SignType);
            sqlCmd.ExecuteNonQuery();

            
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}