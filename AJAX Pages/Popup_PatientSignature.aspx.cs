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
    public static readonly string MSG_PATIENT_SIGN_ABSENT = "absent";
    public static readonly string MSG_PATIENT_SIGN_CORRUPT = "corrupt";
    
    public static readonly string MSG_PATIENT_SIGN_SAVED_SUCCESS = "success";

    private readonly string STR_PREFIX_PATIENT_SIGN = "_PatientSign.jpg";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hdlCasID.Value = Request.QueryString["CaseId"].ToString();
            string CaseID = hdlCasID.Value;
        }
        if (Request.QueryString["EventId"] != null)
        {
            Session["EventID"] = Request.QueryString["EventId"].ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {//Logging Start
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
        string szPath = "";
        string sz_CompanyName = "";
        string sz_EventID = "";
        string DoctorImagePathlogical = "";
        Bill_Sys_NF3_Template objNF3Template;
        Bill_Sys_CheckoutBO _objcheckOut;
        string SignType = "";
        string CaseID = "";


        try
        {
            _objcheckOut = new Bill_Sys_CheckoutBO();
            DigitalSign signobj = new DigitalSign();
            objNF3Template = new Bill_Sys_NF3_Template();
            DataSet dsObj = new DataSet();

            
            string sFilename = Request.QueryString["CaseId"].ToString() + "_" + DateTime.Now.ToString("MMddyyyyss");
            CaseID = Request.QueryString["CaseId"].ToString();
            sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/";
            SaveSignPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/";
            Session["DefaultPath"] = szDefaultPath;
            Session["SignPath"] = SaveSignPath;
            if (!Directory.Exists(szDefaultPath))
            {
                Directory.CreateDirectory(szDefaultPath);
            }

            SignatureSaver.doctor.dao.EventDAO objChDAO = new SignatureSaver.doctor.dao.EventDAO();
            objChDAO.CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            sz_CompanyID = objChDAO.CompanyID;
            
            objChDAO.SignatureByteData = Request.Form["hidden"];
            objChDAO.PatientSignatureLogicalPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/" + sFilename + STR_PREFIX_PATIENT_SIGN;
            objChDAO.PatientSignaturePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/" + sFilename + STR_PREFIX_PATIENT_SIGN;
            string szSignaturePath = objChDAO.PatientSignaturePath;
            
            
            string sReturn = SavePatientSign(objChDAO);

            ArrayList arrevent1 = new ArrayList();
            arrevent1 = (ArrayList)Session["Events"];

            for (int i = 0; i < arrevent1.Count; i++)
            {
                Bill_Sys_CheckinBO obj = new Bill_Sys_CheckinBO();
                obj.GetSpecialityByEventID(arrevent1[i].ToString(), sz_CompanyID, szSignaturePath);
            }

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
                
            }
            Session["SignPath"] = objChDAO.PatientSignaturePath;
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

    public string SavePatientSign(SignatureSaver.doctor.dao.EventDAO obj)
    {
        string sReturn = MSG_PATIENT_SIGN_SAVED_SUCCESS;
        int iSignSuccessFlag = 1;

        if (!SaveSign(obj.SignatureByteData, obj.PatientSignaturePath))
            sReturn = MSG_PATIENT_SIGN_CORRUPT;

        if (!CheckSignExists(obj.PatientSignaturePath))
            sReturn = MSG_PATIENT_SIGN_ABSENT;

        if (!CheckSignSize(obj.PatientSignaturePath))
            sReturn = MSG_PATIENT_SIGN_CORRUPT;

        if (sReturn.Equals(MSG_PATIENT_SIGN_ABSENT) || sReturn.Equals(MSG_PATIENT_SIGN_CORRUPT))
            iSignSuccessFlag = 0;

        return sReturn;
    }

    private bool SaveSign(string sByteData, string sFullSignPath)
    {
        System.Drawing.Image img;

        if (sByteData == null)
        {
            return false;
        }

        SIGPLUSLib.SigPlus sigObj_Patient = new SIGPLUSLib.SigPlus();
        sigObj_Patient.InitSigPlus();

        sigObj_Patient.AutoKeyStart();
        sigObj_Patient.AutoKeyFinish();
        sigObj_Patient.SigCompressionMode = 1;
        sigObj_Patient.EncryptionMode = 2;
        sigObj_Patient.SigString = sByteData;

        if (sigObj_Patient.NumberOfTabletPoints() > 0)
        {
            long size = 0;
            byte[] byteValue;

            sigObj_Patient.ImageFileFormat = 0;
            sigObj_Patient.ImageXSize = 150;
            sigObj_Patient.ImageYSize = 75;
            sigObj_Patient.ImagePenWidth = 8;
            sigObj_Patient.SetAntiAliasParameters(1, 600, 700);
            sigObj_Patient.JustifyX = 5;
            sigObj_Patient.JustifyY = 5;
            sigObj_Patient.JustifyMode = 5;

            sigObj_Patient.BitMapBufferWrite();
            size = sigObj_Patient.BitMapBufferSize();
            byteValue = new byte[size];
            byteValue = (byte[])sigObj_Patient.GetBitmapBufferBytes();

            // byte value to check

            sigObj_Patient.BitMapBufferClose();
            System.IO.MemoryStream ms = new System.IO.MemoryStream(byteValue);

            img = System.Drawing.Image.FromStream(ms);
            img.Save(sFullSignPath, System.Drawing.Imaging.ImageFormat.Bmp);
        }
        return true;
    }

    private bool CheckSignExists(string Path)
    {
        return File.Exists(Path);
    }

    private bool CheckSignSize(string Path)
    {
        if (File.Exists(Path))
        {
            FileInfo info = new FileInfo(Path);
            long length = info.Length;
            long num2 = Convert.ToInt64(ConfigurationSettings.AppSettings["signsize"].ToString());
            if (length > num2)
            {
                return true;
            }
        }
        return false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!WebSignature1.ExportToStreamOnly()) return;
        if (WebSignature1.ImageBytes.Length < 100) return;

        string sFilename = Request.QueryString["CaseId"].ToString() + "_" + DateTime.Now.ToString("MMddyyyyss");
        string CaseID = Request.QueryString["CaseId"].ToString();

        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
        SignatureSaver.doctor.dao.EventDAO objChDAO = new SignatureSaver.doctor.dao.EventDAO();
        
        objChDAO.CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string sz_CompanyID = objChDAO.CompanyID;
        
        objChDAO.SignatureByteData = Request.Form["hidden"];
        objChDAO.SignatureByteData = WebSignature1.Data;
        objChDAO.PatientSignatureLogicalPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/" + sFilename + STR_PREFIX_PATIENT_SIGN;
        objChDAO.PatientSignaturePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/" + sFilename + STR_PREFIX_PATIENT_SIGN;
        string szSignaturePath = objChDAO.PatientSignaturePath;
        
        string strFolderPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/";
        
        if (!Directory.Exists(strFolderPath))
        {
            Directory.CreateDirectory(strFolderPath);
        }
        WebSignature1.ImageTextColor = System.Drawing.Color.Black;
        WebSignature1.bTransparentSignatureImage = false;
        WebSignature1.ExportToImageOnly(strFolderPath, sFilename);
        
        string sReturn = SaveTouchPatientSign(objChDAO);
        
        ArrayList arrevent1 = new ArrayList();
        arrevent1 = (ArrayList)Session["Events"];

        for (int i = 0; i < arrevent1.Count; i++)
        {
            Bill_Sys_CheckinBO obj = new Bill_Sys_CheckinBO();
            obj.GetSpecialityByEventID(arrevent1[i].ToString(), sz_CompanyID, szSignaturePath);
        }

        if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_DOCTOR_SIGN_CORRUPT))
        {
            lblMsg.Text = "Sign is corrupted please resign.";
            lblMsg.Visible = true;
            usrMessage.PutMessage("Sign is corrupted please resign.");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
        }

        if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_DOCTOR_SIGN_ABSENT))
        {
            lblMsg.Text = "Sign does not saved. please contact admin.";
            lblMsg.Visible = true;
            usrMessage.PutMessage("Sign does not saved. please contact admin.");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
        }
        if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_DOCTOR_SIGN_DATABASE_FAIL))
        {
            lblMsg.Text = "Sign does not stored. please contact admin.";
            lblMsg.Visible = true;
            usrMessage.PutMessage("Sign saved Successfully");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
        }
        if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_DOCTOR_SIGN_SAVED_SUCCESS))
        {
            usrMessage.PutMessage("Sign saved Successfully");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
        }
        
        Session["SignPath"] = objChDAO.PatientSignaturePath;
    }

    public string SaveTouchPatientSign(SignatureSaver.doctor.dao.EventDAO obj)
    {
        string sReturn = MSG_PATIENT_SIGN_SAVED_SUCCESS;
        int iSignSuccessFlag = 1;

        if (!CheckSignExists(obj.PatientSignaturePath))
            sReturn = MSG_PATIENT_SIGN_ABSENT;

        if (sReturn.Equals(MSG_PATIENT_SIGN_ABSENT) || sReturn.Equals(MSG_PATIENT_SIGN_CORRUPT))
            iSignSuccessFlag = 0;

        return sReturn;
    }

}