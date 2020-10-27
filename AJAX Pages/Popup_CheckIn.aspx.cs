using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Collections;
using DevExpress.Web;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;
using System.Web.UI.HtmlControls;
using log4net;

public partial class AJAX_Pages_Popup_CheckIn : PageBase
{
    Bill_Sys_CheckinBO objCheckinBO;
    string sz_UserID = "";
    public static readonly string MSG_PATIENT_SIGN_ABSENT = "absent";
    public static readonly string MSG_PATIENT_SIGN_CORRUPT = "corrupt";
    public static readonly string MSG_PATIENT_SIGN_SAVED_SUCCESS = "success";
    private readonly string STR_PREFIX_PATIENT_SIGN = "_PatientSign.jpg";
    private static ILog log = LogManager.GetLogger("Popup_CheckIn");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string checkInUserId = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();
            hdlCasID.Value = Request.QueryString["CaseId"].ToString();
            BindGrid();
            Session["Events"] = null;
            this.btn_Save.Attributes.Add("onclick", "return OnSave();");
            string sCheck=WebSignature1.MinPoints.ToString();
            linkSign.Visible = false;
            hdnLinkSign.Value = "";
        }
    }

    public void BindGrid()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            objCheckinBO = new Bill_Sys_CheckinBO();
            grdDoctorList.DataSource = objCheckinBO.getDoctorSpeciality(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            grdDoctorList.DataBind();
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

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        log.Debug("In btn_Save_Click");
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

        Bill_Sys_CheckBO objCheckVisits = new Bill_Sys_CheckBO();
        objCheckVisits = null;
        string sLinkPath = "";
        try
        {
            
            if (RadioButtonList1.SelectedValue == "1")
            {
                log.Debug("In tab One");
                //For Save sign by signature pad
                _objcheckOut = new Bill_Sys_CheckoutBO();
                DigitalSign signobj = new DigitalSign();
                objNF3Template = new Bill_Sys_NF3_Template();
                DataSet dsObj = new DataSet();


                string sFilename = Request.QueryString["CaseId"].ToString() + "_" + DateTime.Now.ToString("MMddyyyyss");
                log.Debug("file Name : " + sFilename);
                CaseID = Request.QueryString["CaseId"].ToString();
                sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                szDefaultPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/";
                log.Debug("Default Path : " + szDefaultPath);
                SaveSignPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/";
                log.Debug("SavePath : " + SaveSignPath);
                Session["DefaultPath"] = szDefaultPath;
                Session["SignPath"] = SaveSignPath;
                if (!Directory.Exists(szDefaultPath))
                {
                    Directory.CreateDirectory(szDefaultPath);
                }
                log.Debug(" Before SignatureSaver Method");
                SignatureSaver.doctor.dao.EventDAO objChDAO = new SignatureSaver.doctor.dao.EventDAO();
                objChDAO.CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                sz_CompanyID = objChDAO.CompanyID;

                objChDAO.SignatureByteData = Request.Form["hidden"];
                objChDAO.PatientSignatureLogicalPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/" + sFilename + STR_PREFIX_PATIENT_SIGN;
                objChDAO.PatientSignaturePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/" + sFilename + STR_PREFIX_PATIENT_SIGN;
                string szSignaturePath = objChDAO.PatientSignaturePath;

                sLinkPath = ConfigurationManager.AppSettings["VIEWWORD_DOC"].ToString() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/" + sFilename + STR_PREFIX_PATIENT_SIGN;
                log.Debug("Before SavePatientSign() ");
                string sReturn = SavePatientSign(objChDAO);
                log.Debug("value : " + sReturn);

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
                    log.Debug("In sreturn=success");
                    ArrayList _objArray = new ArrayList();
                    for (int i = 0; i < grdDoctorList.Rows.Count; i++)
                    {
                        CheckBox chk = (CheckBox)grdDoctorList.Rows[i].FindControl("chkVisit");
                        if (chk.Checked)
                        {
                            string str = txtAppointmentDate.Text;
                            string[] strArray = str.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (strArray.Length >= 1)
                            {
                                foreach (string item in strArray)
                                {
                                    Bill_Sys_CheckBO obj = new Bill_Sys_CheckBO();

                                    string checkInUserId = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();
                                    string caseid = Request.QueryString["CaseId"].ToString();
                                    string patFname = Request.QueryString["patFname"].ToString();
                                    string patLname = Request.QueryString["patLname"].ToString();
                                    string szcompanyid = Request.QueryString["szcompanyid"].ToString();
                                    string insurance = Request.QueryString["insurance"].ToString();
                                    string claimnumber = Request.QueryString["claimnumber"].ToString();
                                    string dateofaccident = Request.QueryString["dateofaccident"].ToString();
                                    string casenumber = Request.QueryString["casenumber"].ToString();
                                    string patientname = Request.QueryString["patientname"].ToString();

                                    string Visitdate = item;

                                    string doctorID = grdDoctorList.DataKeys[i][0].ToString();
                                    string specialty = grdDoctorList.DataKeys[i][1].ToString();
                                    string doctorName = grdDoctorList.DataKeys[i][2].ToString();
                                    string specialtyName = grdDoctorList.DataKeys[i][3].ToString();
                                    string userid = grdDoctorList.DataKeys[i][4].ToString();

                                    obj.SZ_CHECKIN_USER_ID = checkInUserId;
                                    obj.SZ_VISIT_DATE = item;
                                    obj.SZ_CASE_ID = caseid;
                                    obj.SZ_COMPANY_ID = szcompanyid;
                                    obj.SZ_INSURANCE_COMPANY = insurance;
                                    obj.SZ_CLAIM_NO = claimnumber;
                                    if (dateofaccident != "")
                                    {
                                        obj.DT_DATE_OF_ACCIDENT = Convert.ToDateTime(dateofaccident).ToString("MM/dd/yyyy");
                                    }
                                    obj.SZ_PATIENT_FIRST_NAME = patFname;
                                    obj.SZ_PATIENT_LAST_NAME = patLname;
                                    obj.SZ_CASE_NO = casenumber;
                                    obj.SZ_PATIENT_NAME = patientname;
                                    obj.SZ_DOCTOR_ID = doctorID;
                                    obj.SZ_PROCEDURE_GROUP_ID = specialty;
                                    obj.SZ_DOCTOR_NAME = doctorName;
                                    obj.SZ_PROCEDURE_GROUP = specialtyName;
                                    obj.SZ_VISIT_DATE = item;
                                    obj.SZ_USER_ID = userid;
                                    obj.SZ_SIGN_PATH = szSignaturePath;
                                    obj.BIT_OF_SIGNPATH = "1";
                                    obj.DT_DATE = item;

                                    ArrayList objAdd = new ArrayList();
                                    Bill_Sys_PatientVisitBO objPV = new Bill_Sys_PatientVisitBO();

                                    obj.DT_EVENT_TIME = "8.30";//Appointment time
                                    obj.SZ_EVENT_NOTES = "";
                                    obj.SZ_TYPE_CODE_ID = "TY000000000000000003";
                                    obj.DT_EVENT_TIME_TYPE = "AM";
                                    obj.DT_EVENT_END_TIME = "9.00";
                                    obj.DT_EVENT_END_TIME_TYPE = "AM";

                                    _objArray.Add(obj);
                                }
                            }
                        }
                    }

                    Bill_Sys_CheckinBO obj1 = new Bill_Sys_CheckinBO();
                    Bill_Sys_CheckBO objReturn = new Bill_Sys_CheckBO();
                    objReturn = obj1.SaveVisitByDoctor(_objArray);
                    Clear();
                    if (objReturn.SZ_PRINT_SUCCESS_MESSAGE != "" || objReturn.SZ_PRINT_ERROR_MESSAGE != "")
                    {
                        string msg = "";

                        if (objReturn.SZ_PRINT_SUCCESS_MESSAGE != "")
                        {//Appointment (s) other than <"+obj.SZ_VISIT_DATE+"> were added successfully. The appointment (s) shown here already exist for the selected patient
                            msg = msg + "Appointment (s)  " + objReturn.SZ_PRINT_SUCCESS_MESSAGE + " were added successfully";
                            
                            lblMsg.Text = msg;
                            //lblMsg.Visible = true;
                        }
                        else if (objReturn.SZ_PRINT_ERROR_MESSAGE != "")
                        {
                            msg = "<br/>"+ msg + "The appointment (s) " + objReturn.SZ_PRINT_ERROR_MESSAGE + " shown here already exist for the selected patient";
                            
                            lblMsg.Text = msg;
                            //lblMsg.Visible = true;
                        }

                    }
                    else
                    {
                        lblMsg.Text = "Fail to add Visit(s) ....";
                    }
                    lblMsg.Visible = true;
                    linkSign.Visible = true;
                }
                Session["SignPath"] = objChDAO.PatientSignaturePath;
                //For Save sign by signature pad
            }
            else
                if (RadioButtonList1.SelectedValue == "2")
                {
                    log.Debug("In tab two");
                if (!WebSignature1.ExportToStreamOnly()) return;
                if (WebSignature1.ImageBytes.Length < 100) return;

                string sFilename = Request.QueryString["CaseId"].ToString() + "_" + DateTime.Now.ToString("MMddyyyyss");
                log.Debug("file Name : " + sFilename);
                CaseID = Request.QueryString["CaseId"].ToString();

                objNF3Template = new Bill_Sys_NF3_Template();
                log.Debug("Before  SignatureSaver.doctor.dao.EventDAO");
                SignatureSaver.doctor.dao.EventDAO objChDAO = new SignatureSaver.doctor.dao.EventDAO();

                objChDAO.CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                sz_CompanyID = objChDAO.CompanyID;

                //objChDAO.SignatureByteData = Request.Form["hidden"];
                log.Debug(" Before objChDAO.SignatureByteData");
                objChDAO.SignatureByteData = WebSignature1.Data;
                if (objChDAO.SignatureByteData.ToString()=="") 
                {
                    lblMsg.Text = "Please sign before continuing";
                    return;
                }
                objChDAO.PatientSignatureLogicalPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/" + sFilename + STR_PREFIX_PATIENT_SIGN;
                objChDAO.PatientSignaturePath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/" + sFilename + STR_PREFIX_PATIENT_SIGN;
                string szSignaturePath = objChDAO.PatientSignaturePath;
                sLinkPath = ConfigurationManager.AppSettings["VIEWWORD_DOC"].ToString() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/" + sFilename + STR_PREFIX_PATIENT_SIGN;

                string strFolderPath = (objNF3Template.getPhysicalPath()) + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseID + "/Signs/";

                if (!Directory.Exists(strFolderPath))
                {
                    Directory.CreateDirectory(strFolderPath);
                }
                log.Debug("Before WebSignature1.ImageTextColor");
                WebSignature1.ImageTextColor = System.Drawing.Color.Black;
                WebSignature1.bTransparentSignatureImage = false;
                WebSignature1.ExportToImageOnly(strFolderPath, sFilename + STR_PREFIX_PATIENT_SIGN);
                log.Debug("Before SaveTouchPatientSign()");

                string sReturn = SaveTouchPatientSign(objChDAO);
                log.Debug("Value : " + sReturn);

                if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_DOCTOR_SIGN_CORRUPT))
                {
                    lblMsg.Text = "Sign is corrupted please resign.";
                    lblMsg.Visible = true;
                }

                if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_DOCTOR_SIGN_ABSENT))
                {
                    lblMsg.Text = "Sign does not saved. please contact admin.";
                    lblMsg.Visible = true;
                }
                if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_DOCTOR_SIGN_DATABASE_FAIL))
                {
                    lblMsg.Text = "Sign does not stored. please contact admin.";
                    lblMsg.Visible = true;
                }
                if (sReturn.Equals(SignatureSaver.doctor.services.ServiceDoctor.MSG_DOCTOR_SIGN_SAVED_SUCCESS))
                {
                    log.Debug("In success");
                    ArrayList _objArray = new ArrayList();
                    for (int i = 0; i < grdDoctorList.Rows.Count; i++)
                    {
                        CheckBox chk = (CheckBox)grdDoctorList.Rows[i].FindControl("chkVisit");
                        if (chk.Checked)
                        {
                            string str = txtAppointmentDate.Text;
                            string[] strArray = str.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (strArray.Length >= 1)
                            {
                                foreach (string item in strArray)
                                {
                                    Bill_Sys_CheckBO obj = new Bill_Sys_CheckBO();

                                    string checkInUserId = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();
                                    string caseid = Request.QueryString["CaseId"].ToString();
                                    string patFname = Request.QueryString["patFname"].ToString();
                                    string patLname = Request.QueryString["patLname"].ToString();
                                    string szcompanyid = Request.QueryString["szcompanyid"].ToString();
                                    string insurance = Request.QueryString["insurance"].ToString();
                                    string claimnumber = Request.QueryString["claimnumber"].ToString();
                                    string dateofaccident = Request.QueryString["dateofaccident"].ToString();
                                    string casenumber = Request.QueryString["casenumber"].ToString();
                                    string patientname = Request.QueryString["patientname"].ToString();

                                    string Visitdate = item;

                                    string doctorID = grdDoctorList.DataKeys[i][0].ToString();
                                    string specialty = grdDoctorList.DataKeys[i][1].ToString();
                                    string doctorName = grdDoctorList.DataKeys[i][2].ToString();
                                    string specialtyName = grdDoctorList.DataKeys[i][3].ToString();
                                    string userid = grdDoctorList.DataKeys[i][4].ToString();

                                    obj.SZ_CHECKIN_USER_ID = checkInUserId;
                                    obj.SZ_VISIT_DATE = item;
                                    obj.SZ_CASE_ID = caseid;
                                    obj.SZ_COMPANY_ID = szcompanyid;
                                    obj.SZ_INSURANCE_COMPANY = insurance;
                                    obj.SZ_CLAIM_NO = claimnumber;
                                    if (dateofaccident != "")
                                    {
                                        obj.DT_DATE_OF_ACCIDENT = Convert.ToDateTime(dateofaccident).ToString("MM/dd/yyyy");
                                    }
                                    obj.SZ_CASE_NO = casenumber;
                                    obj.SZ_PATIENT_NAME = patientname;
                                    obj.SZ_DOCTOR_ID = doctorID;
                                    obj.SZ_PROCEDURE_GROUP_ID = specialty;
                                    obj.SZ_DOCTOR_NAME = doctorName;
                                    obj.SZ_PROCEDURE_GROUP = specialtyName;
                                    obj.SZ_VISIT_DATE = item;
                                    obj.SZ_USER_ID = userid;
                                    obj.SZ_SIGN_PATH = szSignaturePath;
                                    obj.BIT_OF_SIGNPATH = "1";
                                    obj.DT_DATE = item;


                                    ArrayList objAdd = new ArrayList();
                                    Bill_Sys_PatientVisitBO objPV = new Bill_Sys_PatientVisitBO();

                                    obj.DT_EVENT_TIME = "8.30";//Appointment time
                                    obj.SZ_EVENT_NOTES = "";
                                    obj.SZ_TYPE_CODE_ID = "TY000000000000000003";
                                    obj.DT_EVENT_TIME_TYPE = "AM";
                                    obj.DT_EVENT_END_TIME = "9.00";
                                    obj.DT_EVENT_END_TIME_TYPE = "AM";

                                    _objArray.Add(obj);
                                }
                            }
                        }
                    }

                    Bill_Sys_CheckinBO obj1 = new Bill_Sys_CheckinBO();
                    Bill_Sys_CheckBO objReturn1 = new Bill_Sys_CheckBO();
                    log.Debug("Before SaveVisitByDoctor()");
                    objReturn1=obj1.SaveVisitByDoctor(_objArray);
                    Clear();

                    string msg = "";
                    if (objReturn1.SZ_PRINT_SUCCESS_MESSAGE != "" || objReturn1.SZ_PRINT_ERROR_MESSAGE != "")
                    {
                        

                        if (objReturn1.SZ_PRINT_SUCCESS_MESSAGE != "")
                        {
                            msg = msg + "Appointment (s) " + objReturn1.SZ_PRINT_SUCCESS_MESSAGE.ToString() + " were added successfully "+"<br/>";
                        }
                        if (objReturn1.SZ_PRINT_ERROR_MESSAGE != "")
                        {
                            msg = "<br/>" + msg + "The appointment (s) " + objReturn1.SZ_PRINT_ERROR_MESSAGE.ToString() + " shown here already exist for the selected patient";
                        }

                        lblMsg.Text = msg;
                    }
                    else
                    {
                        lblMsg.Text = "Fail to add Visit(s) ....";
                    }
                    lblMsg.Visible = true;
                }

                Session["SignPath"] = objChDAO.PatientSignaturePath;
                log.Debug("Session[SignPath]" + objChDAO.PatientSignaturePath);
            }
            log.Debug("sLinkPath : " +sLinkPath);

            if (sLinkPath != "")
            {
                log.Debug("In sLink_Path");
                linkSign.Visible = true;
                hdnLinkSign.Value = sLinkPath;
            }
            
                
        }
        catch(Exception ex)
        {
            string strError = ex.Message.ToString();
            lbl_error_Msg.Text = strError;
            //Response.Write("<script>alert('Exception: ')</script>");
        }
        lbl_error_Msg.Visible = true;
    }

    public void Clear()
    {
        txtAppointmentDate.Text = "";
        foreach (GridViewRow row in grdDoctorList.Rows)
        {
            CheckBox cb = row.FindControl("chkVisit") as CheckBox;
            if (cb != null)
                cb.Checked = false;

        }
    }
    
    protected void txtUpdate1_Click(object sender, EventArgs e)
    {
        if (Session["SignPath"] != null)
        {
            btn_Save.Enabled = true;
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
    private bool CheckCursorSignSize(string Path)
    {
        log.Debug("In CheckCursorSignSize()");
        if (File.Exists(Path))
        {
            FileInfo info = new FileInfo(Path);
            long length = info.Length;
            log.Debug("length : " + length);
            long num2 = Convert.ToInt64(ConfigurationSettings.AppSettings["cursorsignsize"].ToString());
            log.Debug("num2 : " + num2);
            log.Debug("Before length > num2");
            if (length > num2)
            {
                return true;
            }
        }
        return false;
    }

    public string SaveTouchPatientSign(SignatureSaver.doctor.dao.EventDAO obj)
    {
        log.Debug("In SaveTouchPatientSign()");
        string sReturn = MSG_PATIENT_SIGN_SAVED_SUCCESS;
        int iSignSuccessFlag = 1;

        if (!CheckCursorSignSize(obj.PatientSignaturePath))
            sReturn = MSG_PATIENT_SIGN_CORRUPT;
        log.Debug("sReturn : " + sReturn);

        if (!CheckSignExists(obj.PatientSignaturePath))
            sReturn = MSG_PATIENT_SIGN_ABSENT;
        log.Debug("sReturn : " + sReturn);

        if (sReturn.Equals(MSG_PATIENT_SIGN_ABSENT) || sReturn.Equals(MSG_PATIENT_SIGN_CORRUPT))
            iSignSuccessFlag = 0;
        log.Debug("After iSignSuccessFlag = 0");
        log.Debug("Before sReturn");
        return sReturn;
        log.Debug("sReturn : " + sReturn);
    }

    //protected void btn_Close_Click(object sender, EventArgs e)
    //{
        
    //}
}