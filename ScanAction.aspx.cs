using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Data;
using System.Text;
using System.Configuration;
using RequiredDocuments;
using gb.mbs.da;
using gb.mbs.da.service;
using gb.mbs.da.services.common;
using web.utils.onlinelogger;//-- for onling log use


public partial class ScanAction : PageBase
{
    private ScanDao scanDao;
    private FileUpload _FileUpload;
    private ArrayList arrImgId;
    string scanSource = "";
    string eventId = "";
    string caseId = "";
    string patientId = "";
    string procedureGroupId = "";
    string doctorId = "";
    string fileName = "";
    string companyId = "";
    string companyName = "";
    string userId = "";
    string userName = "";
    string userRoleId = "";
    string PhysicalPath = "";
    ArrayList listNodeType = new ArrayList();
    ArrayList listSpeciality = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        ScanBo scanBo = new ScanBo();
        // btnUploadScan.Attributes.Add("onclick", "return btnUpload_onclick()");
        OnlineLogger.Clear();
        OnlineLogger.Log("On Scan Page Load", OnlineLogType.DEBUG);
        //lblMsg.Visible = false;
        companyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        companyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        userId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        userName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        userRoleId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;
        //   patientId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID;

         HttpPostedFile file;
        if (Request.QueryString["scansource"] != null)
        {
            scanSource = Request.QueryString["scansource"].ToString();
            OnlineLogger.Log("Sacn source:" + scanSource, OnlineLogType.DEBUG);
        }

        if (Request.QueryString["operation"] != null)
        {
             file = HttpContext.Current.Request.Files["RemoteFile"];

            if (scanSource == "reqdoc")
            {
                OnlineLogger.Log("scan- reqdoc" + scanSource, OnlineLogType.DEBUG);
                try
                {
                    Result returnResult = new Result();
                    returnResult = validateScan();

                    if (returnResult.msg_code == "fail")
                    {
                        OnlineLogger.Log("Validation failed - " + returnResult.msg, OnlineLogType.ERROR);
                    }
                    else
                    {
                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csid"].ToString());
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;
                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        p_oNode.ID = Convert.ToInt32(Request.QueryString["inodeid"].ToString());

                        //onling log//

                        OnlineLogger.Log("Filename - " + fileName, OnlineLogType.DEBUG);
                        OnlineLogger.Log("File.CaseID - " + p_oPatient.CaseID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("File.Account - " + p_oPatient.Account, OnlineLogType.DEBUG);
                        OnlineLogger.Log("File.Account.ID - " + companyId, OnlineLogType.DEBUG);
                        OnlineLogger.Log("File.Account.Name - " + companyName, OnlineLogType.DEBUG);
                        OnlineLogger.Log("Node.ID - " + p_oNode.ID, OnlineLogType.DEBUG);
                        // end online log//

                        gb.mbs.da.model.document.RequiredDocument p_oRequiredDocument = new gb.mbs.da.model.document.RequiredDocument();
                        p_oRequiredDocument.ID = Convert.ToInt32(Request.QueryString["docid"].ToString());
                        p_oRequiredDocument.Type = Convert.ToInt32(Request.QueryString["doctypeid"].ToString());
                        p_oRequiredDocument.AssignedTo = new gb.mbs.da.model.user.User();
                        p_oRequiredDocument.AssignedTo.ID = Request.QueryString["assingeto"].ToString();
                        p_oRequiredDocument.AssignedTo.UserName = Request.QueryString["username"].ToString();
                        p_oRequiredDocument.UpdatedBy = new gb.mbs.da.model.user.User();
                        p_oRequiredDocument.UpdatedBy.ID = userId;
                        p_oRequiredDocument.UpdatedBy.UserName = userName;
                        p_oRequiredDocument.UpdatedBy.Role = new gb.mbs.da.model.user.Role();
                        p_oRequiredDocument.UpdatedBy.Role.ID = userRoleId;
                        p_oRequiredDocument.Note = Request.QueryString["notes"].ToString();

                        //onling log//
                        OnlineLogger.Log("Required Document - ", OnlineLogType.DEBUG);
                        OnlineLogger.Log("RequiredDocument.ID - " + p_oRequiredDocument.ID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("RequiredDocument.Type - " + p_oRequiredDocument.Type, OnlineLogType.DEBUG);
                        OnlineLogger.Log("RequiredDocument.AssignedTo - " + p_oRequiredDocument.AssignedTo, OnlineLogType.DEBUG);
                        OnlineLogger.Log("RequiredDocument.AssignedTo.ID - " + p_oRequiredDocument.AssignedTo.ID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("RequiredDocument.AssignedTo.UserName - " + p_oRequiredDocument.AssignedTo.UserName, OnlineLogType.DEBUG);
                        OnlineLogger.Log("RequiredDocument.UpdatedBy - " + p_oRequiredDocument.UpdatedBy, OnlineLogType.DEBUG);
                        OnlineLogger.Log("RequiredDocument.UpdatedBy.ID - " + p_oRequiredDocument.UpdatedBy.ID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("RequiredDocument.UpdatedBy.UserName - " + p_oRequiredDocument.UpdatedBy.UserName, OnlineLogType.DEBUG);
                        OnlineLogger.Log("RequiredDocument.UpdatedBy.Role - " + p_oRequiredDocument.UpdatedBy.Role, OnlineLogType.DEBUG);
                        OnlineLogger.Log("RequiredDocument.UpdatedBy.Role.ID - " + p_oRequiredDocument.UpdatedBy.Role.ID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("RequiredDocument.Note - " + p_oRequiredDocument.Note, OnlineLogType.DEBUG);
                        // end online log//

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);

                        OnlineLogger.Log("Adding files to repository", OnlineLogType.DEBUG);
                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        p_oSrvScan.ToOldRequiredDocuments(p_oRequiredDocument);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);
                        OnlineLogger.Log("File(s) uploaded successfully:", OnlineLogType.DEBUG);
                        Response.ClearContent();
                        Response.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert(" + ex.ToString() + ");", true);
                    OnlineLogger.Log("[Error-ReqDoc]:" + ex, OnlineLogType.ERROR);
                }
            }
            else if (scanSource == "payment")
            {

                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("payment Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        OnlineLogger.Log("payment Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);

                    }
                    else
                    {

                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["caseid"].ToString());

                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);

                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        p_oNode.Type = Request.QueryString["nodetype"].ToString();

                        gb.mbs.da.model.bill.Bill p_obill = new gb.mbs.da.model.bill.Bill();
                        p_obill.Number = Request.QueryString["billno"].ToString();
                        p_obill.Specialty = new gb.mbs.da.model.specialty.Specialty();
                        p_obill.Specialty.ID = Request.QueryString["specialty"].ToString();
                        p_obill.Payment = new List<gb.mbs.da.model.payment.Payment>();
                        gb.mbs.da.model.payment.Payment p_oBillPayment = new gb.mbs.da.model.payment.Payment();
                        p_oBillPayment.Id = Request.QueryString["paymentid"].ToString();
                        p_obill.Payment.Add(p_oBillPayment);

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;



                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        OnlineLogger.Log("To save Appontment Process start:", OnlineLogType.DEBUG);
                        p_oSrvScan.ToPayment(p_obill, p_oUser);
                        //lblMsg.Visible = true;
                        //lblMsg.Text = "Documet(s) uploaded successfully";

                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in appointment:" + ex, OnlineLogType.ERROR);
                }
            }

            else if (scanSource == "appointment")
            {
                OnlineLogger.Log("appointment process start:", OnlineLogType.DEBUG);
                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("appointment Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        ///lblMsg.Visible = true;
                        ///lblMsg.Text = returnResult1.msg;
                        OnlineLogger.Log("appointment Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);
                    }
                    else
                    {
                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["caseid"].ToString());
                        p_oPatient.CaseNo = Convert.ToInt32(Request.QueryString["caseno"].ToString());
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;
                        p_oPatient.Account.HasTestFacility = Convert.ToBoolean(Request.QueryString["testfacility"].ToString());

                        //onling log//

                        OnlineLogger.Log("fileName:" + fileName, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oPatient.CaseID:" + p_oPatient.CaseID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oPatient.CaseNo:" + p_oPatient.CaseNo, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oPatient.Account.ID:" + p_oPatient.Account.ID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oPatient.Account.Name:" + p_oPatient.Account.Name, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oPatient.Account.HasTestFacility :" + p_oPatient.Account.HasTestFacility, OnlineLogType.DEBUG);

                        // end online log//

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);

                        //onling log//
                        OnlineLogger.Log("File Path Processing:", OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oFile.Path:" + p_oFile.Path, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oFile.Name:" + p_oFile.Name, OnlineLogType.DEBUG);
                        OnlineLogger.Log("List of file:" + lstFiles, OnlineLogType.DEBUG);
                        //end log//


                        //Scan.aspx?caseid="  "&caseno="  "&eventid="  + "&specialityid="+ "&visittype="  "&visittypeid="  "&scansource=" + arrObj[0].scansource;
                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();

                        gb.mbs.da.model.appointment.Appointment p_oAppointment = new gb.mbs.da.model.appointment.Appointment();
                        p_oAppointment.ID = Convert.ToInt32(Request.QueryString["eventid"].ToString());
                        p_oAppointment.TypeID = Request.QueryString["visittypeid"].ToString();
                        p_oAppointment.Type = Request.QueryString["visittype"].ToString();
                        p_oAppointment.Speciality = new gb.mbs.da.model.speciality.Speciality();
                        p_oAppointment.Speciality.ID = Request.QueryString["specialityid"].ToString();

                        //onling log//

                        OnlineLogger.Log("Appontment Process:", OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oAppointment.ID:" + p_oAppointment.ID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oAppointment.TypeID:" + p_oAppointment.TypeID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oAppointment.Speciality:" + p_oAppointment.Speciality, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oAppointment.Speciality.ID:" + p_oAppointment.Speciality.ID, OnlineLogType.DEBUG);

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        //onling log//
                        OnlineLogger.Log("p_oUser.ID:" + p_oUser.ID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oUser.UserName:" + p_oUser.UserName, OnlineLogType.DEBUG);

                        //end log//

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        if (p_oPatient.Account.HasTestFacility == true)
                        {
                            try
                            {
                                p_oSrvScan.ToAppointmentWithReferringFacility(p_oAppointment, p_oUser);
                                OnlineLogger.Log("Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                            }
                            catch (Exception ex)
                            {
                                OnlineLogger.Log("error occured during scan :", OnlineLogType.DEBUG);
                                throw;
                            }
                        }
                        else
                        {
                            OnlineLogger.Log("To save Appontment Process start:", OnlineLogType.DEBUG);
                            p_oSrvScan.ToAppointment(p_oAppointment, p_oUser);
                        }  
                        ///lblMsg.Visible = true;
                        ///lblMsg.Text = "Documet(s) uploaded successfully";
                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in appointment:" + ex, OnlineLogType.ERROR);
                }
            }
            else if (scanSource == "docmgr")
            {
                OnlineLogger.Log("appointment process start:", OnlineLogType.DEBUG);
                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("appointment Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        ///lblMsg.Visible = true;
                        ///lblMsg.Text = returnResult1.msg;
                        OnlineLogger.Log("appointment Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);
                    }
                    else
                    {
                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["caseid"].ToString());

                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;

                        //onling log//

                        OnlineLogger.Log("fileName:" + fileName, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oPatient.CaseID:" + p_oPatient.CaseID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oPatient.CaseNo:" + p_oPatient.CaseNo, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oPatient.Account.ID:" + p_oPatient.Account.ID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oPatient.Account.Name:" + p_oPatient.Account.Name, OnlineLogType.DEBUG);

                        // end online log//

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);
                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);
                        //onling log//
                        OnlineLogger.Log("File Path Processing:", OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oFile.Path:" + p_oFile.Path, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oFile.Name:" + p_oFile.Name, OnlineLogType.DEBUG);
                        OnlineLogger.Log("List of file:" + lstFiles, OnlineLogType.DEBUG);
                        //end log//

                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        p_oNode.ID = Convert.ToInt32(Request.QueryString["nodeid"].ToString());
                        // end online log//
                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;
                        //onling log//
                        OnlineLogger.Log("p_oUser.ID:" + p_oUser.ID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oUser.UserName:" + p_oUser.UserName, OnlineLogType.DEBUG);
                        //end log//
                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        OnlineLogger.Log("To save Documet Process start:", OnlineLogType.DEBUG);
                        p_oSrvScan.ToDocMgr(p_oUser);
                        ///lblMsg.Visible = true;
                        ///lblMsg.Text = "Documet(s) uploaded successfully";
                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in appointment:" + ex, OnlineLogType.ERROR);
                }
            }


            else if (scanSource == "billdenial")
            {
                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("denial Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        OnlineLogger.Log("denial Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);

                    }
                    else
                    {

                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());

                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);

                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        p_oNode.Type = "";

                        gb.mbs.da.model.bill.Bill p_obill = new gb.mbs.da.model.bill.Bill();
                        p_obill.Number = Request.QueryString["billNo"].ToString();
                        p_obill.Specialty = new gb.mbs.da.model.specialty.Specialty();
                        p_obill.Specialty.ID = Request.QueryString["spId"].ToString();

                        p_obill.Denial = new List<gb.mbs.da.model.bill.denial.Denial>();
                        List<gb.mbs.da.model.bill.denial.Denial> lstDenial = new List<gb.mbs.da.model.bill.denial.Denial>();
                        gb.mbs.da.model.bill.denial.Denial oCurrentDenial = new gb.mbs.da.model.bill.denial.Denial();
                        oCurrentDenial.Id = Request.QueryString["verificationId"].ToString();
                        lstDenial.Add(oCurrentDenial);
                        p_obill.Denial = lstDenial;

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        OnlineLogger.Log("Save denial process start:", OnlineLogType.DEBUG);
                        p_oSrvScan.ToDenial(p_obill, p_oUser);
                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in billdenial:" + ex, OnlineLogType.ERROR);
                }
            }
            else if (scanSource == "billverification")
            {
                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("billverification Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        OnlineLogger.Log("billverification Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);

                    }
                    else
                    {
                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());

                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);

                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        p_oNode.Type = "";

                        gb.mbs.da.model.bill.Bill p_obill = new gb.mbs.da.model.bill.Bill();
                        p_obill.Number = Request.QueryString["billNo"].ToString().TrimEnd(',');
                        p_obill.Specialty = new gb.mbs.da.model.specialty.Specialty();
                        p_obill.Specialty.ID = Request.QueryString["spId"].ToString();

                        p_obill.Verification = new List<gb.mbs.da.model.bill.verification.Verification>();
                        List<gb.mbs.da.model.bill.verification.Verification> lstVerification = new List<gb.mbs.da.model.bill.verification.Verification>();
                        gb.mbs.da.model.bill.verification.Verification oCurrentVerification = new gb.mbs.da.model.bill.verification.Verification();
                        oCurrentVerification.Id = Request.QueryString["vrId"].ToString();
                        lstVerification.Add(oCurrentVerification);
                        p_obill.Verification = lstVerification;

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        p_oSrvScan.ToVerification(p_obill, p_oUser);
                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in billverification:" + ex, OnlineLogType.ERROR);
                }
            }
            else if (scanSource == "billeor")
            {
                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("billverification Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        OnlineLogger.Log("billEOR Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);

                    }
                    else
                    {
                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());

                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);

                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        p_oNode.Type = "";

                        gb.mbs.da.model.bill.Bill p_obill = new gb.mbs.da.model.bill.Bill();
                        p_obill.Number = Request.QueryString["billNo"].ToString().TrimEnd(',');
                        p_obill.Specialty = new gb.mbs.da.model.specialty.Specialty();
                        p_obill.Specialty.ID = Request.QueryString["spId"].ToString();

                        p_obill.Verification = new List<gb.mbs.da.model.bill.verification.Verification>();
                        List<gb.mbs.da.model.bill.verification.Verification> lstVerification = new List<gb.mbs.da.model.bill.verification.Verification>();
                        gb.mbs.da.model.bill.verification.Verification oCurrentVerification = new gb.mbs.da.model.bill.verification.Verification();
                        oCurrentVerification.Id = Request.QueryString["vrId"].ToString();
                        lstVerification.Add(oCurrentVerification);
                        p_obill.Verification = lstVerification;

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        p_oSrvScan.ToEOR(p_obill, p_oUser);
                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in billEOR:" + ex, OnlineLogType.ERROR);
                }
            }
            else if (scanSource == "verificationsent")
            {
                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("verificationsent Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        OnlineLogger.Log("verificationsent Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);

                    }
                    else
                    {
                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());

                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);

                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        p_oNode.Type = "";

                        gb.mbs.da.model.bill.Bill p_obill = new gb.mbs.da.model.bill.Bill();
                        p_obill.Number = Request.QueryString["billNo"].ToString().TrimEnd(',');
                        p_obill.Specialty = new gb.mbs.da.model.specialty.Specialty();
                        p_obill.Specialty.ID = Request.QueryString["spId"].ToString();

                        p_obill.Verification = new List<gb.mbs.da.model.bill.verification.Verification>();
                        List<gb.mbs.da.model.bill.verification.Verification> lstVerification = new List<gb.mbs.da.model.bill.verification.Verification>();
                        gb.mbs.da.model.bill.verification.Verification oCurrentVerification = new gb.mbs.da.model.bill.verification.Verification();
                        oCurrentVerification.Id = Request.QueryString["vrId"].ToString();
                        lstVerification.Add(oCurrentVerification);
                        p_obill.Verification = lstVerification;

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        p_oSrvScan.ToVerificationSent(p_obill, p_oUser);
                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in verificationsent:" + ex, OnlineLogType.ERROR);
                }
            }

            else if (scanSource == "verificationdenial")
            {
                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("verificationdenial Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        OnlineLogger.Log("verificationdenial Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);

                    }
                    else
                    {

                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());

                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);

                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        p_oNode.Type = "";

                        gb.mbs.da.model.bill.Bill p_obill = new gb.mbs.da.model.bill.Bill();
                        p_obill.Number = Request.QueryString["billNo"].ToString();
                        p_obill.Specialty = new gb.mbs.da.model.specialty.Specialty();
                        p_obill.Specialty.ID = Request.QueryString["spId"].ToString();

                        p_obill.Denial = new List<gb.mbs.da.model.bill.denial.Denial>();
                        List<gb.mbs.da.model.bill.denial.Denial> lstDenial = new List<gb.mbs.da.model.bill.denial.Denial>();
                        gb.mbs.da.model.bill.denial.Denial oCurrentDenial = new gb.mbs.da.model.bill.denial.Denial();
                        oCurrentDenial.Id = Request.QueryString["verificationId"].ToString();
                        lstDenial.Add(oCurrentDenial);
                        p_obill.Denial = lstDenial;

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        OnlineLogger.Log("Save verificationdenial process start:", OnlineLogType.DEBUG);
                        p_oSrvScan.ToVerificationDenial(p_obill, p_oUser);
                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in verificationdenial:" + ex, OnlineLogType.ERROR);
                }
            }

            else if (scanSource == "verificationpopup")
            {
                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("denial Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        OnlineLogger.Log("denial Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);

                    }
                    else
                    {

                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());

                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);

                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        p_oNode.Type = "";

                        gb.mbs.da.model.bill.Bill p_obill = new gb.mbs.da.model.bill.Bill();
                        p_obill.Number = Request.QueryString["billNo"].ToString();
                        p_obill.Specialty = new gb.mbs.da.model.specialty.Specialty();
                        p_obill.Specialty.ID = Request.QueryString["spId"].ToString();
                        p_obill.Process = Request.QueryString["flag"].ToString();

                        if (p_obill.Process.ToLower() == "den")
                        {
                            p_obill.Denial = new List<gb.mbs.da.model.bill.denial.Denial>();
                            List<gb.mbs.da.model.bill.denial.Denial> lstDenial = new List<gb.mbs.da.model.bill.denial.Denial>();
                            gb.mbs.da.model.bill.denial.Denial oCurrentDenial = new gb.mbs.da.model.bill.denial.Denial();
                            oCurrentDenial.Id = Request.QueryString["verificationId"].ToString();
                            lstDenial.Add(oCurrentDenial);
                            p_obill.Denial = lstDenial;
                        }
                        else
                        {
                            p_obill.Verification = new List<gb.mbs.da.model.bill.verification.Verification>();
                            List<gb.mbs.da.model.bill.verification.Verification> lstVerification = new List<gb.mbs.da.model.bill.verification.Verification>();
                            gb.mbs.da.model.bill.verification.Verification oCurrentVerification = new gb.mbs.da.model.bill.verification.Verification();
                            oCurrentVerification.Id = Request.QueryString["verificationId"].ToString();
                            lstVerification.Add(oCurrentVerification);
                            p_obill.Verification = lstVerification;
                        }

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        OnlineLogger.Log("Save verificationpopup process start:", OnlineLogType.DEBUG);
                        p_oSrvScan.ToVerificationPopup(p_obill, p_oUser);
                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in verificationpopup:" + ex, OnlineLogType.ERROR);
                }
            }

            else if (scanSource == "generaldenial")
            {
                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("generaldenial Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        OnlineLogger.Log("generaldenial Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);

                    }
                    else
                    {

                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());
                        p_oPatient.CaseNo = Convert.ToInt32(Request.QueryString["caseNo"].ToString());

                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);

                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        p_oNode.ID = Convert.ToInt32(Request.QueryString["nodeId"].ToString()); ;

                        gb.mbs.da.model.bill.Bill p_obill = new gb.mbs.da.model.bill.Bill();

                        p_obill.Denial = new List<gb.mbs.da.model.bill.denial.Denial>();
                        List<gb.mbs.da.model.bill.denial.Denial> lstDenial = new List<gb.mbs.da.model.bill.denial.Denial>();
                        gb.mbs.da.model.bill.denial.Denial oCurrentDenial = new gb.mbs.da.model.bill.denial.Denial();
                        oCurrentDenial.Id = Request.QueryString["denialId"].ToString();
                        lstDenial.Add(oCurrentDenial);
                        p_obill.Denial = lstDenial;

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        OnlineLogger.Log("Save generaldenial process start:", OnlineLogType.DEBUG);
                        p_oSrvScan.ToGeneralDenial(p_obill, p_oUser);
                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in generaldenial:" + ex, OnlineLogType.ERROR);
                }
            }

            else if (scanSource == "pom")
            {
                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("pom Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        OnlineLogger.Log("pom Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);

                    }
                    else
                    {
                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);

                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        // Notetype is set empty because it is not further required in the procedure 
                        //and is used to pass in SrvScanUpload class object.
                        p_oNode.Type = "";

                        gb.mbs.da.model.bill.Bill p_oBill = new gb.mbs.da.model.bill.Bill();
                        p_oBill.PomId = Convert.ToInt32(Request.QueryString["pomid"].ToString());
                        p_oBill.BillStatus = new gb.mbs.da.model.bill.BillStatus();
                        p_oBill.BillStatus.Code = Request.QueryString["pomstatus"].ToString();

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        OnlineLogger.Log("Save pom process start:", OnlineLogType.DEBUG);
                        p_oSrvScan.ToPOM(p_oBill, p_oUser);
                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in pom:" + ex, OnlineLogType.ERROR);
                }
            }

            else if (scanSource == "jfkvisitdoc")
            {
                OnlineLogger.Log("appointment process start:", OnlineLogType.DEBUG);
                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("appointment Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        ///lblMsg.Visible = true;
                        ///lblMsg.Text = returnResult1.msg;
                        OnlineLogger.Log("appointment Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);
                    }
                    else
                    {
                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["caseid"].ToString());

                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;

                        //onling log//

                        OnlineLogger.Log("fileName:" + fileName, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oPatient.CaseID:" + p_oPatient.CaseID, OnlineLogType.DEBUG);                       
                        OnlineLogger.Log("p_oPatient.Account.ID:" + p_oPatient.Account.ID, OnlineLogType.DEBUG);                       

                        // end online log//

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);
                        //onling log//
                        OnlineLogger.Log("File Path Processing:", OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oFile.Path:" + p_oFile.Path, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oFile.Name:" + p_oFile.Name, OnlineLogType.DEBUG);
                        OnlineLogger.Log("List of file:" + lstFiles, OnlineLogType.DEBUG);
                        //end log//

                        gb.mbs.da.model.common.DocumentNode c_oVisit = new gb.mbs.da.model.common.DocumentNode();
                        c_oVisit.ID = Convert.ToInt32(Request.QueryString["visitid"].ToString());
                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.UserName = userName;
                        //onling log//
                        OnlineLogger.Log("p_oUser.ID:" + p_oUser.ID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oUser.UserName:" + p_oUser.UserName, OnlineLogType.DEBUG);
                        //end log//
                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, c_oVisit, lstFiles);
                        OnlineLogger.Log("To save Appontment Process start:", OnlineLogType.DEBUG);
                        p_oSrvScan.ToSaveVisitDocument(p_oUser);
                        //lblMsg.Visible = true;
                        //lblMsg.Text = "Documet(s) uploaded successfully";
                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in appointment:" + ex, OnlineLogType.ERROR);
                }
            }
            else if (scanSource == "invoicepayment")
            {

                try
                {
                    Result returnResult1 = new Result();
                    OnlineLogger.Log("payment Validationscan start:", OnlineLogType.DEBUG);
                    returnResult1 = validateScan();
                    if (returnResult1.msg_code == "fail")
                    {
                        OnlineLogger.Log("payment Validationscan fail:" + returnResult1.msg, OnlineLogType.ERROR);

                    }
                    else
                    {

                        fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();


                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;

                        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
                        PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");

                        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

                        if (!Directory.Exists(PhysicalPath + "ImageScanned/"))
                        {
                            Directory.CreateDirectory(PhysicalPath + "ImageScanned/");
                        }

                        fileName = fileName.Replace("$", "_");
                        fileName = fileName.Replace("@", "_");
                        fileName = fileName.Replace("#", "_");
                        fileName = fileName.Replace("^", "_");
                        fileName = fileName.Replace("&", "_");
                        fileName = fileName.Replace("*", "_");
                        fileName = fileName.Replace("(", "_");
                        fileName = fileName.Replace(")", "_");
                        fileName = fileName.Replace("?", "_");
                        fileName = fileName.Replace("+", "_");
                        fileName = fileName.Replace("=", "_");
                        fileName = fileName.Replace("/", "_");
                        fileName = fileName.Replace("\\", "_");
                        fileName = fileName.Replace("%", "_");
                        fileName = fileName.Replace("~", "_");
                        fileName = fileName.Replace("!", "_");
                        fileName = fileName.Replace("|", "_");
                        fileName = fileName.Replace("{", "_");
                        fileName = fileName.Replace("}", "_");
                        fileName = fileName.Replace("[", "_");
                        fileName = fileName.Replace("]", "_");
                        fileName = fileName.Replace(" ", "_");

                        file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                        ArrayList lstFiles = new ArrayList();
                        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                        p_oFile.Path = PhysicalPath + "ImageScanned/";
                        p_oFile.Name = fileName;
                        lstFiles.Add(p_oFile);

                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();


                        gb.mbs.da.model.bill.Bill p_obill = new gb.mbs.da.model.bill.Bill();
                        p_obill.Number = Request.QueryString["billno"].ToString();

                        p_obill.Payment = new List<gb.mbs.da.model.payment.Payment>();
                        gb.mbs.da.model.payment.Payment p_oBillPayment = new gb.mbs.da.model.payment.Payment();
                        p_oBillPayment.Id = Request.QueryString["paymentid"].ToString();
                        p_obill.Payment.Add(p_oBillPayment);

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;



                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, lstFiles);
                        OnlineLogger.Log("To save Appontment Process start:", OnlineLogType.DEBUG);
                        p_oSrvScan.ToJFKPayment(p_obill, p_oUser);
                        //lblMsg.Visible = true;
                        //lblMsg.Text = "Documet(s) uploaded successfully";

                    }
                }
                catch (Exception ex)
                {
                    OnlineLogger.Log("Error Occured in appointment:" + ex, OnlineLogType.ERROR);
                }
            }
        }
       
        else
        {
            //string patientName = scanBo.GetPatientName(patientId);
            //string caseNo = scanBo.GetCaseNo(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, companyId);
            ///txtEnterFileName.Text = gb.mbs.da.services.common.SrvFile.ValidateFile(patientName + "_" + caseNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmssms") + ".pdf");
            ///uploadFilename.Value = txtEnterFileName.Text;
            ///srcFlag.Value = scanSource;
            ///hdnQueryString.Value = Request.QueryString.ToString();

            //onling log//
            ///OnlineLogger.Log("patientName:" + patientName, OnlineLogType.DEBUG);
            ///OnlineLogger.Log("caseNo:" + caseNo, OnlineLogType.DEBUG);

            ///OnlineLogger.Log("txtEnterFileName:" + txtEnterFileName.Text, OnlineLogType.DEBUG);
            ///OnlineLogger.Log("uploadFilename.Value:" + uploadFilename.Value, OnlineLogType.DEBUG);

            ///OnlineLogger.Log("ServiceFlagvalue:" + srcFlag.Value, OnlineLogType.DEBUG);
            ///OnlineLogger.Log("HiddenQueryString:" + hdnQueryString.Value, OnlineLogType.DEBUG);

            //end log//

            Result returnResult = new Result();
            returnResult = validateScan();
            if (returnResult.msg_code == "fail")
            {
                ///lblMsg.Visible = true;
                ///lblMsg.Text = returnResult.msg;

                OnlineLogger.Log("Error Occured:" + returnResult, OnlineLogType.ERROR);
                ///btnUpload.Disabled = true;
            }
        }
        OnlineLogger.Save();
    }

    private Result validateScan()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        OnlineLogger.Log("Validate scan Process start", OnlineLogType.DEBUG);
        Result returnResult = new Result();
        returnResult.msg = "";
        returnResult.msg_code = "fail";
        int iFlag = 0;
        try
        {
            if (Request.QueryString["scansource"].ToString() == "reqdoc")
            {
                // Bad Request: Case, account information missing in request. Operation halted.
                OnlineLogger.Log("Request.QueryString[scansource]=reqdoc", OnlineLogType.DEBUG);
                if (Request.QueryString["csid"].ToString() == "" || Request.QueryString["csid"].ToString() == "&nbsp;" || Request.QueryString["csid"].ToString() == "NA")
                {
                    iFlag = 1;
                    if (returnResult.msg == "")
                    {
                        returnResult.msg = returnResult.msg + "Case";
                        OnlineLogger.Log("returnResult.msg Case:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                    else
                    {
                        returnResult.msg = returnResult.msg + ",Case";
                        OnlineLogger.Log("returnResult.msg Case:" + returnResult.msg, OnlineLogType.DEBUG);
                    }

                }
                if (Request.QueryString["inodeid"].ToString() == "" || Request.QueryString["inodeid"].ToString() == "&nbsp;" || Request.QueryString["inodeid"].ToString() == "NA")
                {
                    if (returnResult.msg == "")
                    {
                        returnResult.msg = returnResult.msg + "Node Id";
                        OnlineLogger.Log("returnResult.msg NodeId:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                    else
                    {
                        returnResult.msg = returnResult.msg + ",Node Id";
                        OnlineLogger.Log("returnResult.msg NodeId:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                }
                if (companyId == "" || companyId == "&nbsp;" || companyId == "NA")
                {
                    iFlag = 1;
                    //returnResult.msg = returnResult.msg + "company session is expired.\n\r";
                    if (returnResult.msg == "")
                    {
                        returnResult.msg = returnResult.msg + "Company Id";
                        OnlineLogger.Log("returnResult.msg CompanyId:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                    else
                    {
                        returnResult.msg = returnResult.msg + ",Company Id";
                        OnlineLogger.Log("returnResult.msg CompanyId:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                }
                if (userId == "" || userId == "&nbsp;" || userId == "NA")
                {
                    iFlag = 1;
                    // returnResult.msg = returnResult.msg + "user session is expired \n\r";
                    if (returnResult.msg == "")
                    {
                        returnResult.msg = returnResult.msg + "User Id";
                        OnlineLogger.Log("returnResult.msg UserId:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                    else
                    {
                        returnResult.msg = returnResult.msg + ",User Id";
                        OnlineLogger.Log("returnResult.msg UserId:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                }
                if (Request.QueryString["doctypeid"].ToString() == "" || Request.QueryString["doctypeid"].ToString() == "&nbsp;" || Request.QueryString["doctypeid"].ToString() == "NA")
                {
                    iFlag = 1;
                    // returnResult.msg = returnResult.msg + "user session is expired \n\r";
                    if (returnResult.msg == "")
                    {
                        returnResult.msg = returnResult.msg + "Document Type Id";
                        OnlineLogger.Log("returnResult.msg Document Type Id:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                    else
                    {
                        returnResult.msg = returnResult.msg + ",Documet Type Id";
                        OnlineLogger.Log("returnResult.msg Document Type Id:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                }
                if (Request.QueryString["docid"].ToString() == "" || Request.QueryString["docid"].ToString() == "&nbsp;" || Request.QueryString["docid"].ToString() == "NA")
                {
                    iFlag = 1;
                    // returnResult.msg = returnResult.msg + "user session is expired \n\r";
                    if (returnResult.msg == "")
                    {
                        returnResult.msg = returnResult.msg + "Document Id";
                        OnlineLogger.Log("returnResult.msg Document Id:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                    else
                    {
                        returnResult.msg = returnResult.msg + ",Document Id";
                        OnlineLogger.Log("returnResult.msg Document Id:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                }
            }
            else if (Request.QueryString["scansource"].ToString() == "pom")
            {
                if (Request.QueryString["pomid"].ToString() == "" || Request.QueryString["pomid"].ToString() == "&nbsp;" || Request.QueryString["pomid"].ToString() == "NA")
                {
                    iFlag = 1;
                    if (returnResult.msg == "")
                    {
                        returnResult.msg = returnResult.msg + "pomid";
                        OnlineLogger.Log("returnResult.msg specialty:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                    else
                    {
                        returnResult.msg = returnResult.msg + ",pomid";
                        OnlineLogger.Log("returnResult.msg Case:" + returnResult.msg, OnlineLogType.DEBUG);
                    }

                }

            }
            else if (Request.QueryString["scansource"].ToString() == "docmgr")
            {
                if (Request.QueryString["nodeid"].ToString() == "" || Request.QueryString["nodeid"].ToString() == "&nbsp;" || Request.QueryString["nodeid"].ToString() == "NA")
                {
                    iFlag = 1;
                    if (returnResult.msg == "")
                    {
                        returnResult.msg = returnResult.msg + "Node Id";
                        OnlineLogger.Log("returnResult.msg specialty:" + returnResult.msg, OnlineLogType.DEBUG);
                    }
                    else
                    {
                        returnResult.msg = returnResult.msg + ",Node id";
                        OnlineLogger.Log("returnResult.msg Case:" + returnResult.msg, OnlineLogType.DEBUG);
                    }

                }

            }
            else if (Request.QueryString["scansource"].ToString() == "billdenial")
            {
                if (Request.QueryString["spId"] == null || Request.QueryString["billNo"] == null || Request.QueryString["denialDate"] == null || Request.QueryString["verificationId"] == null)
                {
                    OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    iFlag = 1;
                }
                else
                {
                    string sSpecialty = Request.QueryString["spId"].ToString();
                    string sBillNO = Request.QueryString["billNo"].ToString();
                    string sVerificationId = Request.QueryString["verificationId"].ToString();

                    if (!IsEmptyOrNull(sSpecialty) && !IsEmptyOrNull(sBillNO) && !IsEmptyOrNull(sSpecialty) && !IsEmptyOrNull(sVerificationId))
                    {

                    }
                    else
                    {
                        iFlag = 1;
                        OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    }
                }
            }
            else if (Request.QueryString["scansource"].ToString() == "billverification")
            {
                if (Request.QueryString["spId"] == null || Request.QueryString["billNo"] == null || Request.QueryString["csId"] == null || Request.QueryString["vrId"] == null)
                {
                    OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    iFlag = 1;
                }
                else
                {
                    string sCaseId = Request.QueryString["csId"].ToString();
                    string sSpecialty = Request.QueryString["spId"].ToString();
                    string sBillNO = Request.QueryString["billNo"].ToString().TrimEnd(',');
                    string sVerificationId = Request.QueryString["vrId"].ToString();

                    if (!IsEmptyOrNull(sSpecialty) && !IsEmptyOrNull(sBillNO) && !IsEmptyOrNull(sSpecialty) && !IsEmptyOrNull(sVerificationId))
                    {

                    }
                    else
                    {
                        iFlag = 1;
                        OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    }
                }
            }
            else if (Request.QueryString["scansource"].ToString() == "verificationsent")
            {
                if (Request.QueryString["spId"] == null || Request.QueryString["billNo"] == null || Request.QueryString["csId"] == null || Request.QueryString["vrId"] == null)
                {
                    OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    iFlag = 1;
                }
                else
                {
                    string sCaseId = Request.QueryString["csId"].ToString();
                    string sSpecialty = Request.QueryString["spId"].ToString();
                    string sBillNO = Request.QueryString["billNo"].ToString().TrimEnd(',');
                    string sVerificationId = Request.QueryString["vrId"].ToString();

                    if (!IsEmptyOrNull(sBillNO) && !IsEmptyOrNull(sSpecialty) && !IsEmptyOrNull(sVerificationId))
                    {

                    }
                    else
                    {
                        iFlag = 1;
                        OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    }
                }
            }
            else if (Request.QueryString["scansource"].ToString() == "verificationdenial")
            {
                if (Request.QueryString["spId"] == null || Request.QueryString["billNo"] == null || Request.QueryString["verificationId"] == null)
                {
                    OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    iFlag = 1;
                }
                else
                {
                    string sSpecialty = Request.QueryString["spId"].ToString();
                    string sBillNO = Request.QueryString["billNo"].ToString();
                    string sVerificationId = Request.QueryString["verificationId"].ToString();

                    if (!IsEmptyOrNull(sBillNO) && !IsEmptyOrNull(sSpecialty) && !IsEmptyOrNull(sVerificationId))
                    {

                    }
                    else
                    {
                        iFlag = 1;
                        OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    }
                }
            }
            else if (Request.QueryString["scansource"].ToString() == "verificationpopup")
            {
                if (Request.QueryString["spId"] == null || Request.QueryString["billNo"] == null || Request.QueryString["verificationId"] == null || Request.QueryString["flag"] == null)
                {
                    OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    iFlag = 1;
                }
                else
                {
                    string sSpecialty = Request.QueryString["spId"].ToString();
                    string sBillNO = Request.QueryString["billNo"].ToString();
                    string sVerificationId = Request.QueryString["verificationId"].ToString();

                    if (!IsEmptyOrNull(sSpecialty) && !IsEmptyOrNull(sBillNO) && !IsEmptyOrNull(sVerificationId))
                    {

                    }
                    else
                    {
                        iFlag = 1;
                        OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    }
                }
            }
            else if (Request.QueryString["scansource"].ToString() == "generaldenial")
            {
                if (Request.QueryString["denialId"] == null || Request.QueryString["nodeId"] == null)
                {
                    OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    iFlag = 1;
                }
                else
                {
                    string sDenialId = Request.QueryString["denialId"].ToString();
                    string sNodeId = Request.QueryString["nodeId"].ToString();

                    if (!IsEmptyOrNull(sDenialId) && !IsEmptyOrNull(sNodeId))
                    {

                    }
                    else
                    {
                        iFlag = 1;
                        OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    }
                }
            }
            else if (Request.QueryString["scansource"].ToString() == "invoicepayment")
            {
                if (Request.QueryString["billno"] == null || Request.QueryString["paymentid"] == null)
                {
                    OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    iFlag = 1;
                }
                else
                {
                    string sPayment = Request.QueryString["paymentid"].ToString();
                    string sBill = Request.QueryString["billno"].ToString();

                    if (!IsEmptyOrNull(sPayment) && !IsEmptyOrNull(sBill))
                    {

                    }
                    else
                    {
                        iFlag = 1;
                        OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    }
                }
            }
            else if (Request.QueryString["scansource"].ToString() == "jfkvisitdoc")
            {
                if (Request.QueryString["caseid"] == null || Request.QueryString["visitid"] == null || Request.QueryString["caseno"] == null)
                {
                    OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    iFlag = 1;
                }
                else
                {
                    string sCaseId = Request.QueryString["caseid"].ToString();
                    string sVisitId = Request.QueryString["visitid"].ToString();
                    string sCaseNo = Request.QueryString["caseno"].ToString();

                    if (!IsEmptyOrNull(sCaseId) && !IsEmptyOrNull(sVisitId) && !IsEmptyOrNull(sCaseNo))
                    {

                    }
                    else
                    {
                        iFlag = 1;
                        OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                    }
                }

            }


            if (iFlag == 1)
            {
                returnResult.msg = "Bad Request:" + returnResult.msg + " information missing in request. Operation halted.";
                returnResult.msg_code = "fail";
                OnlineLogger.Log("Bad Request:" + returnResult.msg, OnlineLogType.ERROR);
            }
            else
            {
                returnResult.msg_code = "success";
                OnlineLogger.Log("Validate Scan sucess:" + returnResult.msg, OnlineLogType.DEBUG);
            }
        }
        catch (Exception ex)
        {
            returnResult.msg_code = "fail";
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
        return returnResult;
    }

    private bool IsEmptyOrNull(string p_oValue)
    {
        if (p_oValue.Trim() == "" || p_oValue.Contains("&nbsp") || p_oValue == "NA" || p_oValue == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}