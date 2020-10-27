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

public partial class PatientSignature : PageBase
{
    private readonly string STR_PREFIX_PATIENT_SIGN = "_PatientSign.jpg";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["EventId"] != null)
        {
            Session["EventID"] = Request.QueryString["EventId"].ToString();
        }
    }

    //protected void btn_SaveSign(object sender, EventArgs e)
    //{
    //    bool SignSaved = false;
    //    string SaveSignPath = "";
    //    String szDefaultPath = "";
    //    String Sign = "";

    //    System.Drawing.Image img;
    //    string DoctorImagePath = "";
    //    string PatientPath = "";
    //    string CaseBarcodePath = "";
    //    string sz_CompanyID = "";
    //    string sz_CompanyName = "";
    //    string sz_EventID = "";
    //    string DoctorImagePathlogical = "";
    //    Bill_Sys_NF3_Template objNF3Template;
    //    Bill_Sys_CheckoutBO _objcheckOut;
    //    string SignType = "";


    //    try
    //    {
    //        _objcheckOut = new Bill_Sys_CheckoutBO();
    //        DigitalSign signobj = new DigitalSign();
    //        objNF3Template = new Bill_Sys_NF3_Template();
    //        DataSet dsObj = new DataSet();
    //        dsObj = _objcheckOut.PatientName((Session["EventID"].ToString()));
    //        Session["ChkOutCaseID"] = dsObj.Tables[0].Rows[0][1].ToString();

    //        sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
    //        szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() + "/Signs/";
    //        SaveSignPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() + "/Signs/";
    //        if (!Directory.Exists(szDefaultPath))
    //        {
    //            Directory.CreateDirectory(szDefaultPath);
    //        }

    //        SignatureSaver.doctor.dao.EventDAO objChDAO = new SignatureSaver.doctor.dao.EventDAO();
    //        objChDAO.CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    //        objChDAO.EventID = Convert.ToInt64(Session["EventID"].ToString());
    //        objChDAO.SignatureByteData = Request.Form["hidden"];
    //        objChDAO.PatientSignatureLogicalPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() + "/Signs/" + Convert.ToInt64(Session["EventID"].ToString()) + STR_PREFIX_PATIENT_SIGN;
    //        objChDAO.PatientSignaturePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() + "/Signs/" + Convert.ToInt64(Session["EventID"].ToString()) + STR_PREFIX_PATIENT_SIGN;
    //        objChDAO.Speciality_Name = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_PROCEDURE_GROUP_NAME;
    //        objChDAO.SpecialityID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_SPECIALITYID;
    //        string sReturn = new SignatureSaver.doctor.services.ServiceDoctor().SavePatientSign(objChDAO);

    //        if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_PATIENT_SIGN_CORRUPT))
    //        {
    //            lblMsg.Text = "Sign is corrupted please resign.";
    //            lblMsg.Visible = true;
    //        }

    //        if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_PATIENT_SIGN_ABSENT))
    //        {
    //            lblMsg.Text = "Sign does not saved. please contact admin.";
    //            lblMsg.Visible = true;
    //        }
    //        if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_PATIENT_SIGN_DATABASE_FAIL))
    //        {
    //            lblMsg.Text = "Sign does not stored. please contact admin.";
    //            lblMsg.Visible = true;
    //        }
    //        if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_PATIENT_SIGN_SAVED_SUCCESS))
    //        {
    //            lblMsg.Text = "Sign saved succefully";
    //            lblMsg.Visible = true;
    //            if (chkShow.Checked)
    //            {
    //                string openPath = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
    //                imagepatient.ImageUrl = openPath + objChDAO.PatientSignatureLogicalPath;
    //                imagepatient.Visible = true;
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, GetType(), "kk11", "ClosePopup();", true);
    //            }
    //        }


    //    }
    //    catch (Exception i)
    //    {

    //    }
    //}

    //protected void btnSave_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        Bill_Sys_NF3_Template objNF3Template;
    //        Bill_Sys_CheckoutBO _objcheckOut = new Bill_Sys_CheckoutBO();
    //        DataSet dsObj = new DataSet();
    //        dsObj = _objcheckOut.PatientName(Session["EventID"].ToString());
    //        Session["ChkOutCaseID"] = dsObj.Tables[0].Rows[0][1].ToString();

    //        if (!WebSignature1.ExportToStreamOnly()) return;
    //        if (WebSignature1.ImageBytes.Length < 100) return;





    //        objNF3Template = new Bill_Sys_NF3_Template();
    //        SignatureSaver.doctor.dao.EventDAO objChDAO = new SignatureSaver.doctor.dao.EventDAO();
    //        //CH_Notes.doctor.dao.EventDAO objChDAO = new CH_Notes.doctor.dao.EventDAO();
    //        objChDAO.CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    //        objChDAO.EventID = Convert.ToInt64(Session["EventID"].ToString());
    //        objChDAO.SignatureByteData = Request.Form["hidden"];
    //        objChDAO.SignatureByteData = WebSignature1.Data;
    //        objChDAO.PatientSignatureLogicalPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() + "/Signs/" + Convert.ToInt64(Session["EventID"].ToString()) + STR_PREFIX_PATIENT_SIGN;
    //        objChDAO.PatientSignaturePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() + "/Signs/" + Convert.ToInt64(Session["EventID"].ToString()) + STR_PREFIX_PATIENT_SIGN;
    //        objChDAO.Speciality_Name = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_PROCEDURE_GROUP_NAME;
    //        objChDAO.SpecialityID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_SPECIALITYID;


    //        string strFolderPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["ChkOutCaseID"].ToString() + "/Signs/";
    //        string strFileName = Convert.ToInt64(Session["EventID"].ToString()) + STR_PREFIX_PATIENT_SIGN;
    //        if (!Directory.Exists(strFolderPath))
    //        {
    //            Directory.CreateDirectory(strFolderPath);
    //        }
    //        WebSignature1.ImageTextColor = System.Drawing.Color.Black;
    //        WebSignature1.bTransparentSignatureImage = false;
    //        WebSignature1.ExportToImageOnly(strFolderPath, strFileName);
    //        //WebSignature1.ExportToImageAndStream(strFolderPath, strFileName);




    //        string sReturn = new SignatureSaver.doctor.services.ServiceDoctor().SaveTouchPatientSign(objChDAO); //SaveTouchPatientSign(objChDAO);

    //        if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_DOCTOR_SIGN_CORRUPT))
    //        {
    //            lblMsg.Text = "Sign is corrupted please resign.";
    //            lblMsg.Visible = true;
    //            usrMessage.PutMessage("Sign is corrupted please resign.");
    //            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
    //            usrMessage.Show();
    //        }

    //        if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_DOCTOR_SIGN_ABSENT))
    //        {
    //            lblMsg.Text = "Sign does not saved. please contact admin.";
    //            lblMsg.Visible = true;
    //            usrMessage.PutMessage("Sign does not saved. please contact admin.");
    //            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
    //            usrMessage.Show();
    //        }
    //        if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_DOCTOR_SIGN_DATABASE_FAIL))
    //        {
    //            lblMsg.Text = "Sign does not stored. please contact admin.";
    //            lblMsg.Visible = true;
    //            usrMessage.PutMessage("Sign saved Successfully");
    //            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
    //            usrMessage.Show();
    //        }
    //        if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_DOCTOR_SIGN_SAVED_SUCCESS))
    //        {
    //            usrMessage.PutMessage("Sign saved Successfully");
    //            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
    //            usrMessage.Show();
    //        }
    //        string openPath = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
    //        Image_signature.ImageUrl = openPath + objChDAO.PatientSignatureLogicalPath;
    //        Image_signature.Visible = true;





    //        //string strConString = ConfigurationManager.AppSettings["WebSignatureConnectionString"].ToString();

    //        //SqlConnection myConnection = new SqlConnection(strConString);
    //        //SqlCommand myCommand = new SqlCommand("insert into WebSignature (signatureImage,ip,FileName,YourName) values(@signatureImage,@ip,@FileName,@YourName)", myConnection);
    //        //myCommand.Parameters.Add("@signatureImage", SqlDbType.Image).Value = WebSignature1.ImageBytes;
    //        //myCommand.Parameters.Add("@ip", SqlDbType.VarChar).Value = Page.Request.UserHostAddress.ToString();
    //        //myCommand.Parameters.Add("@FileName", SqlDbType.VarChar).Value = strFileName;
    //        //myCommand.Parameters.Add("@YourName", SqlDbType.VarChar).Value = txtName.Text;
    //        //try
    //        //{
    //        //    myConnection.Open();
    //        //    myCommand.ExecuteScalar();
    //        //    lblMsg.Text = "Successfully Saved the image into the Database.";
    //        //}
    //        //catch
    //        //{
    //        //    lblMsg.Text = "Fail to save the image into the database.";
    //        //}
    //        //myCommand.Dispose();
    //        //myConnection.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        usrMessage.PutMessage(ex.Message.ToString());
    //        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
    //        usrMessage.Show();
    //    }

    //}
}