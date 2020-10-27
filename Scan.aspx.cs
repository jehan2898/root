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
using System.Collections.Specialized;
using RequiredDocuments;
using gb.mbs.da;
using gb.mbs.da.service;
using gb.mbs.da.services.common;
using web.utils.onlinelogger;//-- for onling log use
using System.Threading.Tasks;

public partial class Scan : System.Web.UI.Page
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
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SetupScanCookie();
        ScanBo scanBo = new ScanBo();
        // btnUploadScan.Attributes.Add("onclick", "return btnUpload_onclick()");
        OnlineLogger.Clear();
        OnlineLogger.Log("Page loaded", OnlineLogType.DEBUG);
        //lblMsg.Visible = false;
        companyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        companyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        userId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        userName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        userRoleId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;
        //patientId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID;

        if (Request.QueryString["scansource"] != null)
        {
            scanSource = Request.QueryString["scansource"].ToString();
            OnlineLogger.Log("Source - " + scanSource, OnlineLogType.DEBUG);
        }

        #region scan-upload-clicked
        if (Request.QueryString["operation"] != null)
        {
            HttpPostedFile file = HttpContext.Current.Request.Files["RemoteFile"];
            OnlineLogger.Log("RemoteFile - " + file, OnlineLogType.DEBUG);

            switch (scanSource)
            {
                #region payment
                case "payment":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("payment Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
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
                            if (ValidateExtenation(fileName))
                            {
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
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }
                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in appointment:" + ex, OnlineLogType.ERROR);
                    }
                    break;
                #endregion

                #region docmgr
                case "docmgr":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("Document Manager Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
                        }
                        else
                        {
                            fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                            gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                            //p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["caseid"].ToString());

                            p_oPatient.Account = new gb.mbs.da.model.account.Account();
                            p_oPatient.Account.ID = companyId;


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
                            if (ValidateExtenation(fileName))
                            {
                                file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                                ArrayList lstFiles = new ArrayList();
                                gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                                p_oFile.Path = PhysicalPath + "ImageScanned/";
                                p_oFile.Name = fileName;
                                lstFiles.Add(p_oFile);

                                gb.mbs.da.model.common.DocumentNode c_oNode = new gb.mbs.da.model.common.DocumentNode();
                                c_oNode.ID = Convert.ToInt32(Request.QueryString["nodeid"].ToString());

                                // end online log //
                                gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                                p_oUser.UserName = userName;

                                SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, c_oNode, lstFiles);
                                OnlineLogger.Log("To save Appontment Process start:", OnlineLogType.DEBUG);
                                p_oSrvScan.ToDocMgr(p_oUser);
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }
                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in appointment:" + ex, OnlineLogType.ERROR);
                    }
                    break;
                #endregion

                #region billdenial
                case "billdenial":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("billdenial Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
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
                            if (ValidateExtenation(fileName))
                            {
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

                                // end online log//

                                gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                                p_oUser.ID = userId;
                                p_oUser.UserName = userName;
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }

                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in billdenial:" + ex, OnlineLogType.ERROR);
                    }
                    break;
                #endregion

                #region appointment
                case "appointment":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("appointment Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
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
                            if (ValidateExtenation(fileName))
                            {
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
                                OnlineLogger.Log("To save Appontment Process start:", OnlineLogType.DEBUG);
                                p_oSrvScan.ToAppointment(p_oAppointment, p_oUser);
                                lblMsg.Visible = true;
                                lblMsg.Text = "Documet(s) uploaded successfully";
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }


                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in appointment:" + ex, OnlineLogType.ERROR);
                    }

                    break;
                #endregion

                #region billverification
                case "billverification":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("billverification Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
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
                            if (ValidateExtenation(fileName))
                            {
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

                                // end online log//

                                gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                                p_oUser.ID = userId;
                                p_oUser.UserName = userName;
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }

                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in billverification:" + ex, OnlineLogType.ERROR);
                    }
                    break;
                case "billeor":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("billverification Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
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
                            if (ValidateExtenation(fileName))
                            {
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

                                // end online log//

                                gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                                p_oUser.ID = userId;
                                p_oUser.UserName = userName;
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }

                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in Eor:" + ex, OnlineLogType.ERROR);
                    }
                    break;
                #endregion

                #region verificationsent
                case "verificationsent":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("verificationsent Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
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

                            if (ValidateExtenation(fileName))
                            {
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

                                // end online log//

                                gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                                p_oUser.ID = userId;
                                p_oUser.UserName = userName;
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }

                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in verificationsent:" + ex, OnlineLogType.ERROR);
                    }
                    break;
                #endregion

                #region verificationdenial
                case "verificationdenial":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("verificationdenial Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
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
                            if (ValidateExtenation(fileName))
                            {
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

                                // end online log//

                                gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                                p_oUser.ID = userId;
                                p_oUser.UserName = userName;
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }
                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in verificationdenial:" + ex, OnlineLogType.ERROR);
                    }
                    break;
                #endregion

                #region verificationpopup
                case "verificationpopup":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("verificationpopup Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
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
                            if (ValidateExtenation(fileName))
                            {
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

                                // end online log//

                                gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                                p_oUser.ID = userId;
                                p_oUser.UserName = userName;
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }
                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in verificationpopup:" + ex, OnlineLogType.ERROR);
                    }
                    break;
                #endregion

                #region generaldenial
                case "generaldenial":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("generaldenial Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
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
                            if (ValidateExtenation(fileName))
                            {
                                file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                                ArrayList lstFiles = new ArrayList();
                                gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                                p_oFile.Path = PhysicalPath + "ImageScanned/";
                                p_oFile.Name = fileName;
                                lstFiles.Add(p_oFile);

                                gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                                p_oNode.ID = Convert.ToInt32(Request.QueryString["nodeId"].ToString());

                                gb.mbs.da.model.bill.Bill p_obill = new gb.mbs.da.model.bill.Bill();

                                p_obill.Denial = new List<gb.mbs.da.model.bill.denial.Denial>();
                                List<gb.mbs.da.model.bill.denial.Denial> lstDenial = new List<gb.mbs.da.model.bill.denial.Denial>();
                                gb.mbs.da.model.bill.denial.Denial oCurrentDenial = new gb.mbs.da.model.bill.denial.Denial();
                                oCurrentDenial.Id = Request.QueryString["denialId"].ToString();
                                lstDenial.Add(oCurrentDenial);
                                p_obill.Denial = lstDenial;

                                // end online log//

                                gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                                p_oUser.ID = userId;
                                p_oUser.UserName = userName;
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";

                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }
                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in generaldenial:" + ex, OnlineLogType.ERROR);
                    }
                    break;
                #endregion

                #region pom
                case "pom":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("pom Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
                        }
                        else
                        {
                            fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);
                            gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient(); ;
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
                            if (ValidateExtenation(fileName))
                            {
                                file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                                ArrayList lstFiles = new ArrayList();
                                gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                                p_oFile.Path = PhysicalPath + "ImageScanned/";
                                p_oFile.Name = fileName;
                                lstFiles.Add(p_oFile);

                                gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                                p_oNode.Type = "";

                                gb.mbs.da.model.bill.Bill p_oBill = new gb.mbs.da.model.bill.Bill();
                                p_oBill.PomId = Convert.ToInt32(Request.QueryString["pomid"].ToString());
                                p_oBill.BillStatus = new gb.mbs.da.model.bill.BillStatus();
                                p_oBill.BillStatus.Code = Request.QueryString["pomstatus"].ToString();

                                // end online log//

                                gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                                p_oUser.ID = userId;
                                p_oUser.UserName = userName;
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }
                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in pom:" + ex, OnlineLogType.ERROR);
                    }
                    break;
                #endregion

                #region jfkvisit
                case "Jfkvisitdoc":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("Document Manager Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
                        }
                        else
                        {
                            fileName = gb.mbs.da.services.common.SrvFile.ValidateFile(file.FileName);

                            gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                            p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["caseid"].ToString());

                            p_oPatient.Account = new gb.mbs.da.model.account.Account();
                            p_oPatient.Account.ID = companyId;


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
                            if (ValidateExtenation(fileName))
                            {
                                file.SaveAs(PhysicalPath + "ImageScanned/" + fileName);

                                ArrayList lstFiles = new ArrayList();
                                gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();
                                p_oFile.Path = PhysicalPath + "ImageScanned/";
                                p_oFile.Name = fileName;
                                lstFiles.Add(p_oFile);

                                gb.mbs.da.model.common.DocumentNode c_oVisit = new gb.mbs.da.model.common.DocumentNode();
                                c_oVisit.ID = Convert.ToInt32(Request.QueryString["visitid"].ToString());

                                // end online log //
                                gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                                p_oUser.UserName = userName;

                                SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, c_oVisit, lstFiles);
                                OnlineLogger.Log("To save Appontment Process start:", OnlineLogType.DEBUG);
                                p_oSrvScan.ToSaveVisitDocument(p_oUser);
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }
                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in appointment:" + ex, OnlineLogType.ERROR);
                    }
                    break;
                #endregion

                #region InvoicePayment
                case "invoicepayment":
                    try
                    {
                        Result returnResult1 = new Result();
                        OnlineLogger.Log("payment Validationscan start:", OnlineLogType.DEBUG);
                        returnResult1 = ValidateScan();
                        if (returnResult1.msg_code == "fail")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = returnResult1.msg;
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
                            if (ValidateExtenation(fileName))
                            {
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
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Entered file name does not have valid extension";
                            }
                        }//end else
                    }
                    catch (Exception ex)
                    {
                        OnlineLogger.Log("Error Occured in appointment:" + ex, OnlineLogType.ERROR);
                    }
                    break;
                    #endregion

            }
        }
        #endregion scan-upload-clicked
        else
        {
            caseId = "";
            if (scanSource == "reqdoc")
            {
                caseId = Request.QueryString["csid"].ToString();
            }
            else if (scanSource == "payment" || scanSource == "appointment" || scanSource == "docmgr" || scanSource == "JfkVisitDoc")
            {
                caseId = Request.QueryString["caseid"].ToString();
            }
            else if (scanSource == "billdenial")
            {
                caseId = Request.QueryString["csid"].ToString();
            }
            else if (scanSource == "billverification" || scanSource == "billeor" || scanSource == "verificationsent" || scanSource == "verificationdenial" || scanSource == "verificationpopup" || scanSource == "generaldenial")
            {
                caseId = Request.QueryString["csId"].ToString();
            }
            if (scanSource == "pom")
            {
                caseId = "1234"; // A dummy case id is sent here which is just used to bypass the validation for caseid. Later Case id is used from the stored procedure in SrvScan
                txtEnterFileName.Text = gb.mbs.da.services.common.SrvFile.ValidateFile("p" + "_" + Request.QueryString["pomid"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmssms") + ".pdf");
                uploadFilename.Value = txtEnterFileName.Text;
            }
            if (scanSource == "invoicepayment")
            {
                caseId = "1234"; // A dummy case id is sent here which is just used to bypass the validation for caseid. Later Case id is used from the stored procedure in SrvScan
                txtEnterFileName.Text = gb.mbs.da.services.common.SrvFile.ValidateFile("Payment" + "_" + Request.QueryString["billno"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmssms") + ".pdf");
                uploadFilename.Value = txtEnterFileName.Text;
            }
            else
            {
                DataSet ds = scanBo.GetCaseInfo(caseId, companyId);
                string patientName = "";
                string caseNo = "";
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        patientName = ds.Tables[0].Rows[0]["PatientName"].ToString();
                        caseNo = ds.Tables[0].Rows[0]["SZ_CASE_NO"].ToString();
                        txtEnterFileName.Text = gb.mbs.da.services.common.SrvFile.ValidateFile(patientName + "_" + caseNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmssms") + ".pdf");
                        uploadFilename.Value = txtEnterFileName.Text;
                        //onling log//
                        OnlineLogger.Log("Patient - " + patientName, OnlineLogType.DEBUG);
                        OnlineLogger.Log("Case # - " + caseNo, OnlineLogType.DEBUG);
                    }
                }
            }

            srcFlag.Value = scanSource;
            hdnQueryString.Value = Request.QueryString.ToString();

            OnlineLogger.Log("File Name - " + txtEnterFileName.Text, OnlineLogType.DEBUG);
            OnlineLogger.Log("File to upload - " + uploadFilename.Value, OnlineLogType.DEBUG);

            OnlineLogger.Log("Parameters - " + hdnQueryString.Value, OnlineLogType.DEBUG);

            Result returnResult = new Result();
            returnResult = ValidateScan();

            if (returnResult.msg_code == "fail")
            {
                lblMsg.Visible = true;
                lblMsg.Text = returnResult.msg;
                OnlineLogger.Log("Error - " + returnResult, OnlineLogType.ERROR);
                btnUpload.Disabled = true;
            }

            if (Request.QueryString["maxcnt"] != null)
            {
                string[] FileUploadCnt = { "FileUpload1", "FileUpload2", "FileUpload3", "FileUpload4", "FileUpload5" };
                try
                {
                    int iCount = Convert.ToInt32(Request.QueryString["maxcnt"].ToString());
                    if (iCount > FileUploadCnt.Length)
                    {
                        iCount = FileUploadCnt.Length;
                    }
                    for (int i = 0; i < iCount; i++)
                    {
                        string ctrl = FileUploadCnt[i].ToString();
                        System.Web.UI.WebControls.FileUpload upload = (System.Web.UI.WebControls.FileUpload)this.FindControl(ctrl);
                        if (upload != null)
                        {
                            upload.Enabled = true;
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
        OnlineLogger.Save();
    }
    private void SetupScanCookie()
    {
        string FormsCookie = "";
        NameValueCollection headers = base.Request.Headers;
        for (int i = 0; i < headers.Count; i++)
        {
            string key = headers.GetKey(i);
            string value = headers.Get(i);
            if (key == "Cookie")
            {
                FormsCookie = value;
            }
        }
        // Define the name and type of the client scripts on the page.
        String csname1 = "PopupScript";
        Type cstype = this.GetType();

        // Get a ClientScriptManager reference from the Page class.
        ClientScriptManager cs = Page.ClientScript;

        // Check to see if the startup script is already registered.
        if (!cs.IsStartupScriptRegistered(cstype, csname1))
        {
            StringBuilder cstext1 = new StringBuilder();
            cstext1.Append("<script type=text/javascript> var FormsCookie = '");
            cstext1.Append(FormsCookie);
            cstext1.Append("';</script>");

            cs.RegisterStartupScript(cstype, csname1, cstext1.ToString());
        }
    }
    private string SrvFile(string p)
    {
        throw new NotImplementedException();
    }

    protected void btnUploadFiles_Click(object sender, EventArgs e)
    {
        OnlineLogger.Clear();
        OnlineLogger.Log("Upload clicked:", OnlineLogType.DEBUG);
        companyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        companyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        userId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        userName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        userRoleId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;

        //online log//
        OnlineLogger.Log("companyId:" + companyId, OnlineLogType.DEBUG);
        OnlineLogger.Log("companyName:" + companyName, OnlineLogType.DEBUG);
        OnlineLogger.Log("userId:" + userId, OnlineLogType.DEBUG);
        OnlineLogger.Log("userName:" + userName, OnlineLogType.DEBUG);
        OnlineLogger.Log("userRoleId:" + userRoleId, OnlineLogType.DEBUG);

        //end//

        ArrayList olist = new ArrayList();
        gb.mbs.da.model.common.ApplicationSettings p_oApplicationSettings = new gb.mbs.da.model.common.ApplicationSettings();
        string PhysicalPath = p_oApplicationSettings.GetParameterValue("PatientInfoSaveFilePath");
        //string PhysicalPath = "D:/GreenBills_Temp/temp_files/";
        int Flag = 0;
        //string filepath = 
        //online log//
        OnlineLogger.Log("PhysicalPath:" + PhysicalPath, OnlineLogType.DEBUG);

        string FileName1 = "";
        string FileName2 = "";
        string FileName3 = "";
        string FileName4 = "";
        string FileName5 = "";
        //end//

        if (FileUpload1.FileName != "")
        {
            string extension = "";
            string result = "";
            extension = Path.GetExtension(FileUpload1.FileName);
            result = Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            FileName1 = result + "_" + DateTime.Now.ToString("MMddyyyysstt") + extension;
            olist.Add(WriteFile(FileUpload1.FileBytes, FileName1, PhysicalPath));
            Flag = 1;
        }
        if (FileUpload2.FileName != "")
        {
            string extension = "";
            string result = "";
            extension = Path.GetExtension(FileUpload2.FileName);
            result = Path.GetFileNameWithoutExtension(FileUpload2.FileName);
            FileName2 = result + "_" + DateTime.Now.ToString("MMddyyyysstt") + extension;
            olist.Add(WriteFile(FileUpload2.FileBytes, FileName2, PhysicalPath));
            Flag = 1;
        }
        if (FileUpload3.FileName != "")
        {
            string extension = "";
            string result = "";
            extension = Path.GetExtension(FileUpload3.FileName);
            result = Path.GetFileNameWithoutExtension(FileUpload3.FileName);
            FileName3 = result + "_" + DateTime.Now.ToString("MMddyyyysstt") + extension;
            olist.Add(WriteFile(FileUpload3.FileBytes, FileName3, PhysicalPath));
            Flag = 1;
        }
        if (FileUpload4.FileName != "")
        {
            string extension = "";
            string result = "";
            extension = Path.GetExtension(FileUpload4.FileName);
            result = Path.GetFileNameWithoutExtension(FileUpload4.FileName);
            FileName4 = result + "_" + DateTime.Now.ToString("MMddyyyysstt") + extension;
            olist.Add(WriteFile(FileUpload4.FileBytes, FileName4, PhysicalPath));
            Flag = 1;
        }

        if (FileUpload5.FileName != "")
        {
            string extension = "";
            string result = "";
            extension = Path.GetExtension(FileUpload5.FileName);
            result = Path.GetFileNameWithoutExtension(FileUpload5.FileName);
            FileName5 = result + "_" + DateTime.Now.ToString("MMddyyyysstt") + extension;
            olist.Add(WriteFile(FileUpload5.FileBytes, FileName5, PhysicalPath));
            Flag = 1;
        }
        if (Flag == 0)
        {
            lblMsg.Visible = true;
            lblMsg.Text = " There are no files selected to upload. You must select atleast 1 file to proceed";
            OnlineLogger.Log("Error Occured in file upload:" + lblMsg.Text, OnlineLogType.ERROR);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);
        }
        else
        {
            switch (scanSource)
            {

                case "payment":
                    {
                        try
                        {
                            gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                            p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["caseid"].ToString());
                            p_oPatient.Account = new gb.mbs.da.model.account.Account();
                            p_oPatient.Account.ID = companyId;
                            p_oPatient.Account.Name = companyName;


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



                            SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, olist);
                            p_oSrvScan.ToPayment(p_obill, p_oUser);

                            lblMsg.Visible = true;
                            lblMsg.Text = "Document(s) uploaded successfully";
                            SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                            OnlineLogger.Log("Reduired Docuemnt:Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);


                        }
                        catch (Exception ex)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = ex.ToString();
                            OnlineLogger.Log("Error Occured in case reqdoc:" + ex, OnlineLogType.ERROR);
                            throw;
                        }
                        break;
                    }

                case "docmgr":
                    try
                    {
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["caseid"].ToString());
                        gb.mbs.da.model.common.DocumentNode c_oNode = new gb.mbs.da.model.common.DocumentNode();
                        // Notetype is set empty because it is not further required in the procedure 
                        //and is used to pass in SrvScanUpload class object.
                        c_oNode.ID = Convert.ToInt32(Request.QueryString["nodeid"].ToString());

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, c_oNode, olist);
                        p_oSrvScan.ToDocMgr(p_oUser);

                        lblMsg.Visible = true;
                        lblMsg.Text = "Document(s) uploaded successfully";
                        SaveActivityLog("DOC_UPLOADED", c_oNode.ID + " " + c_oNode.Name, p_oPatient.CaseID.ToString());
                        OnlineLogger.Log("Reduired Docuemnt:Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);


                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = ex.ToString();
                        OnlineLogger.Log("Error Occured in case reqdoc:" + ex, OnlineLogType.ERROR);
                        throw;
                    }
                    break;

                case "billdenial":
                    try
                    {
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;


                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        // Notetype is set empty because it is not further required in the procedure 
                        //and is used to pass in SrvScanUpload class object.
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

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, olist);
                        p_oSrvScan.ToDenial(p_obill, p_oUser);

                        lblMsg.Visible = true;
                        lblMsg.Text = "Document(s) uploaded successfully";
                        SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                        OnlineLogger.Log("Reduired Docuemnt:Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);


                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = ex.ToString();
                        OnlineLogger.Log("Error Occured in case reqdoc:" + ex, OnlineLogType.ERROR);
                        throw;
                    }
                    break;

                case "appointment":
                    {
                        OnlineLogger.Log("appointment Scansource:", OnlineLogType.DEBUG);
                        lblMsg.Visible = false;
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["caseid"].ToString());
                        p_oPatient.CaseNo = Convert.ToInt32(Request.QueryString["caseno"].ToString());
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;
                        p_oPatient.Account.HasTestFacility = Convert.ToBoolean(Request.QueryString["testfacility"].ToString());

                        //online log//
                        OnlineLogger.Log("p_oPatient.CaseID:" + p_oPatient.CaseID, OnlineLogType.DEBUG);
                        OnlineLogger.Log(" p_oPatient.CaseNo:" + p_oPatient.CaseNo, OnlineLogType.DEBUG);
                        OnlineLogger.Log(" p_oPatient.Account:" + p_oPatient.Account, OnlineLogType.DEBUG);
                        OnlineLogger.Log(" p_oPatient.Account.ID:" + companyId, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oPatient.Account.Name:" + p_oPatient.Account.Name, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oPatient.Account.HasTestFacility :" + p_oPatient.Account.HasTestFacility, OnlineLogType.DEBUG);

                        //end//


                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();

                        gb.mbs.da.model.appointment.Appointment p_oAppointment = new gb.mbs.da.model.appointment.Appointment();
                        p_oAppointment.ID = Convert.ToInt32(Request.QueryString["eventid"].ToString());
                        p_oAppointment.TypeID = Request.QueryString["visittypeid"].ToString();
                        p_oAppointment.Type = Request.QueryString["visittype"].ToString();
                        p_oAppointment.Speciality = new gb.mbs.da.model.speciality.Speciality();
                        p_oAppointment.Speciality.ID = Request.QueryString["specialityid"].ToString();

                        //online log//
                        OnlineLogger.Log("p_oAppointment.ID:" + p_oAppointment.ID, OnlineLogType.DEBUG);
                        OnlineLogger.Log(" p_oAppointment.TypeID:" + p_oAppointment.TypeID, OnlineLogType.DEBUG);
                        OnlineLogger.Log(" p_oAppointment.Type:" + p_oAppointment.Type, OnlineLogType.DEBUG);
                        OnlineLogger.Log(" p_oAppointment.Speciality" + p_oAppointment.Speciality, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oAppointment.Speciality.ID:" + p_oAppointment.Speciality.ID, OnlineLogType.DEBUG);

                        //end//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;
                        //online log//
                        OnlineLogger.Log("p_oUser.ID:" + p_oAppointment.ID, OnlineLogType.DEBUG);
                        OnlineLogger.Log("p_oUser.UserName:" + p_oAppointment.TypeID, OnlineLogType.DEBUG);
                        //end//
                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, olist);
                        if (p_oPatient.Account.HasTestFacility == true)
                        {
                            try
                            {
                                p_oSrvScan.ToAppointmentWithReferringFacility(p_oAppointment, p_oUser);
                                lblMsg.Visible = true;
                                lblMsg.Text = "Document(s) uploaded successfully";
                                SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                                OnlineLogger.Log("Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                                //break;
                            }
                            catch (Exception ex)
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = ex.Message.ToString();
                                break;
                            }
                        }
                        else
                        {
                            p_oSrvScan.ToAppointment(p_oAppointment, p_oUser);
                            lblMsg.Visible = true;
                            lblMsg.Text = "Document(s) uploaded successfully";
                            SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                            OnlineLogger.Log("Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                            //break;
                        }
                        break;
                    }

                case "billverification":
                    try
                    {
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;


                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        // Notetype is set empty because it is not further required in the procedure 
                        //and is used to pass in SrvScanUpload class object.
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

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, olist);
                        p_oSrvScan.ToVerification(p_obill, p_oUser);

                        lblMsg.Visible = true;
                        lblMsg.Text = "Document(s) uploaded successfully";
                        SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                        OnlineLogger.Log("Reduired Docuemnt:Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = ex.ToString();
                        OnlineLogger.Log("Error Occured in case reqdoc:" + ex, OnlineLogType.ERROR);
                        throw;
                    }
                    break;
                case "billeor":
                    try
                    {
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;


                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        // Notetype is set empty because it is not further required in the procedure 
                        //and is used to pass in SrvScanUpload class object.
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

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, olist);
                        p_oSrvScan.ToEOR(p_obill, p_oUser);

                        lblMsg.Visible = true;
                        lblMsg.Text = "Document(s) uploaded successfully";
                        SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                        OnlineLogger.Log("Reduired Docuemnt:Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = ex.ToString();
                        OnlineLogger.Log("Error Occured in case reqdoc:" + ex, OnlineLogType.ERROR);
                        throw;
                    }
                    break;

                case "verificationsent":
                    try
                    {
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;


                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        // Notetype is set empty because it is not further required in the procedure 
                        //and is used to pass in SrvScanUpload class object.
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

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, olist);
                        p_oSrvScan.ToVerificationSent(p_obill, p_oUser);

                        lblMsg.Visible = true;
                        lblMsg.Text = "Document(s) uploaded successfully";
                        SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                        OnlineLogger.Log("Reduired Docuemnt:Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = ex.ToString();
                        OnlineLogger.Log("Error Occured in case reqdoc:" + ex, OnlineLogType.ERROR);
                        throw;
                    }
                    break;

                case "verificationdenial":
                    try
                    {
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;


                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        // Notetype is set empty because it is not further required in the procedure 
                        //and is used to pass in SrvScanUpload class object.
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

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, olist);
                        p_oSrvScan.ToVerificationDenial(p_obill, p_oUser);

                        lblMsg.Visible = true;
                        lblMsg.Text = "Document(s) uploaded successfully";
                        SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                        OnlineLogger.Log("Reduired Docuemnt:Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);


                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = ex.ToString();
                        OnlineLogger.Log("Error Occured in case reqdoc:" + ex, OnlineLogType.ERROR);
                        throw;
                    }
                    break;

                case "verificationpopup":
                    try
                    {
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;


                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        // Notetype is set empty because it is not further required in the procedure 
                        //and is used to pass in SrvScanUpload class object.
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

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, olist);
                        p_oSrvScan.ToVerificationPopup(p_obill, p_oUser);

                        lblMsg.Visible = true;
                        lblMsg.Text = "Document(s) uploaded successfully";
                        SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                        OnlineLogger.Log("Reduired Docuemnt:Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);


                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = ex.ToString();
                        OnlineLogger.Log("Error Occured in case reqdoc:" + ex, OnlineLogType.ERROR);
                        throw;
                    }
                    break;

                case "generaldenial":
                    try
                    {
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csId"].ToString());
                        p_oPatient.CaseNo = Convert.ToInt32(Request.QueryString["caseNo"].ToString());
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;


                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        p_oNode.ID = Convert.ToInt32(Request.QueryString["nodeId"].ToString());

                        gb.mbs.da.model.bill.Bill p_obill = new gb.mbs.da.model.bill.Bill();

                        p_obill.Denial = new List<gb.mbs.da.model.bill.denial.Denial>();
                        List<gb.mbs.da.model.bill.denial.Denial> lstDenial = new List<gb.mbs.da.model.bill.denial.Denial>();
                        gb.mbs.da.model.bill.denial.Denial oCurrentDenial = new gb.mbs.da.model.bill.denial.Denial();
                        oCurrentDenial.Id = Request.QueryString["denialId"].ToString();
                        lstDenial.Add(oCurrentDenial);
                        p_obill.Denial = lstDenial;

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, olist);
                        p_oSrvScan.ToGeneralDenial(p_obill, p_oUser);

                        lblMsg.Visible = true;
                        lblMsg.Text = "Document(s) uploaded successfully";
                        SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                        OnlineLogger.Log("Reduired Docuemnt:Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);


                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = ex.ToString();
                        OnlineLogger.Log("Error Occured in case reqdoc:" + ex, OnlineLogType.ERROR);
                        throw;
                    }
                    break;

                case "pom":
                    try
                    {
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;


                        gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                        // Notetype is set empty because it is not further required in the procedure 
                        //and is used to pass in SrvScanUpload class object.
                        p_oNode.Type = "";

                        gb.mbs.da.model.bill.Bill p_oBill = new gb.mbs.da.model.bill.Bill();
                        p_oBill.PomId = Convert.ToInt32(Request.QueryString["pomid"].ToString());
                        p_oBill.BillStatus = new gb.mbs.da.model.bill.BillStatus();
                        p_oBill.BillStatus.Code = Request.QueryString["pomstatus"].ToString();

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, olist);

                        //    var task3 = new Task(() => p_oSrvScan.ToPOM(p_oBill, p_oUser),
                        //TaskCreationOptions.LongRunning);
                        //    task3.Start();
                        p_oSrvScan.ToPOM(p_oBill, p_oUser);

                        lblMsg.Visible = true;
                        lblMsg.Text = "Document(s) uploaded successfully";
                        OnlineLogger.Log("pom:Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                        SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = ex.ToString();
                        OnlineLogger.Log("Error Occured in case pom:" + ex, OnlineLogType.ERROR);
                        throw;
                    }
                    break;

                case "JfkVisitDoc":
                    try
                    {
                        gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                        p_oPatient.Account = new gb.mbs.da.model.account.Account();
                        p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["caseid"].ToString());
                        p_oPatient.Account.ID = companyId;
                        p_oPatient.Account.Name = companyName;

                        gb.mbs.da.model.common.DocumentNode c_oVisit = new gb.mbs.da.model.common.DocumentNode();
                        c_oVisit.ID = Convert.ToInt32(Request.QueryString["visitid"].ToString());

                        // end online log//

                        gb.mbs.da.model.user.User p_oUser = new gb.mbs.da.model.user.User();
                        p_oUser.ID = userId;
                        p_oUser.UserName = userName;

                        SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, c_oVisit, olist);
                        p_oSrvScan.ToSaveVisitDocument(p_oUser);

                        lblMsg.Visible = true;
                        lblMsg.Text = "Document(s) uploaded successfully";
                        SaveActivityLog("DOC_UPLOADED", c_oVisit.ID + " " + c_oVisit.Name, p_oPatient.CaseID.ToString());
                        OnlineLogger.Log("Reduired Docuemnt:Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);


                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = ex.ToString();
                        OnlineLogger.Log("Error Occured in case reqdoc:" + ex, OnlineLogType.ERROR);
                        throw;
                    }
                    break;

                case "reqdoc":
                    {
                        try
                        {

                            OnlineLogger.Log("On reduired Doc", OnlineLogType.DEBUG);
                            lblMsg.Visible = false;
                            gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();
                            p_oPatient.CaseID = Convert.ToInt32(Request.QueryString["csid"].ToString());
                            p_oPatient.Account = new gb.mbs.da.model.account.Account();
                            p_oPatient.Account.ID = companyId;
                            p_oPatient.Account.Name = companyName;
                            gb.mbs.da.model.common.DocumentNode p_oNode = new gb.mbs.da.model.common.DocumentNode();
                            p_oNode.ID = Convert.ToInt32(Request.QueryString["inodeid"].ToString());

                            //online log//
                            OnlineLogger.Log("p_oPatient.CaseID:" + p_oPatient.CaseID, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oPatient.Account:" + p_oPatient.Account, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oPatient.Account.ID:" + p_oPatient.Account.ID, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oPatient.Account.Name:" + p_oPatient.Account.Name, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oNode.ID:" + p_oNode.ID, OnlineLogType.DEBUG);

                            //end//


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


                            //online log//
                            OnlineLogger.Log("p_oRequiredDocument.ID:" + p_oRequiredDocument.ID, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oRequiredDocument.Type" + p_oRequiredDocument.Type, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oRequiredDocument.AssignedTo:" + p_oRequiredDocument.AssignedTo, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oRequiredDocument.AssignedTo.ID" + p_oRequiredDocument.AssignedTo.ID, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oRequiredDocument.AssignedTo.UserName:" + p_oRequiredDocument.AssignedTo.UserName, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oRequiredDocument.UpdatedBy:" + p_oRequiredDocument.UpdatedBy, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oRequiredDocument.UpdatedBy.ID:" + p_oRequiredDocument.UpdatedBy.ID, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oRequiredDocument.UpdatedBy.UserName" + p_oRequiredDocument.UpdatedBy.UserName, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oRequiredDocument.UpdatedBy.Role" + p_oRequiredDocument.UpdatedBy.Role, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oRequiredDocument.UpdatedBy.Role.ID" + p_oRequiredDocument.UpdatedBy.Role.ID, OnlineLogType.DEBUG);
                            OnlineLogger.Log("p_oRequiredDocument.Note" + p_oRequiredDocument.Note, OnlineLogType.DEBUG);

                            //end//
                            OnlineLogger.Log("save process of required document:", OnlineLogType.DEBUG);
                            SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, olist);
                            p_oSrvScan.ToOldRequiredDocuments(p_oRequiredDocument);
                            lblMsg.Visible = true;
                            lblMsg.Text = "Documet(s) uploaded successfully";
                            SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                            OnlineLogger.Log("Reduired Docuemnt:Documet(s) uploaded successfully:", OnlineLogType.DEBUG);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = ex.ToString();
                            OnlineLogger.Log("Error Occured in case reqdoc:" + ex, OnlineLogType.ERROR);
                            throw;
                        }
                        break;
                    }
                case "invoicepayment":
                    {
                        try
                        {
                            gb.mbs.da.model.patient.Patient p_oPatient = new gb.mbs.da.model.patient.Patient();

                            p_oPatient.Account = new gb.mbs.da.model.account.Account();
                            p_oPatient.Account.ID = companyId;
                            p_oPatient.Account.Name = companyName;


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



                            SrvScanUpload p_oSrvScan = new SrvScanUpload(p_oPatient, p_oNode, olist);
                            p_oSrvScan.ToJFKPayment(p_obill, p_oUser);

                            lblMsg.Visible = true;
                            lblMsg.Text = "Document(s) uploaded successfully";
                            SaveActivityLog("DOC_UPLOADED", p_oNode.ID + " " + p_oNode.Name, p_oPatient.CaseID.ToString());
                            OnlineLogger.Log("Reduired Docuemnt:Document(s) uploaded successfully:", OnlineLogType.DEBUG);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);


                        }
                        catch (Exception ex)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = ex.ToString();
                            OnlineLogger.Log("Error Occured in case reqdoc:" + ex, OnlineLogType.ERROR);
                            throw;
                        }
                        break;
                    }
            }
            if (Flag != 0)
            {

            }
            OnlineLogger.Save();
            divUploadFiles.Attributes.Add("display", "inline");
            divScan.Attributes.Add("display", "none");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MyFunction()", true);
        }
    }
    private void SaveActivityLog(string Title, string desc, string caseid)
    {
        DAO_NOTES_EO _DAO_NOTES_EO = null;
        DAO_NOTES_BO _DAO_NOTES_BO = null;
        Bill_Sys_BillingCompanyDetails_BO billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
        _DAO_NOTES_EO = new DAO_NOTES_EO();
        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = Title;
        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = desc;
        _DAO_NOTES_BO = new DAO_NOTES_BO();
        _DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
        _DAO_NOTES_EO.SZ_CASE_ID = caseid;
        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);



    }
    private gb.mbs.da.model.common.File WriteFile(byte[] buffer, string p_filename, string p_filepath)
    {
        gb.mbs.da.model.common.File p_oFile = new gb.mbs.da.model.common.File();

        string sFileName = p_filename;

        sFileName = sFileName.Replace("$", "_");
        sFileName = sFileName.Replace("@", "_");
        sFileName = sFileName.Replace("#", "_");
        sFileName = sFileName.Replace("^", "_");
        sFileName = sFileName.Replace("&", "_");
        sFileName = sFileName.Replace("*", "_");
        sFileName = sFileName.Replace("(", "_");
        sFileName = sFileName.Replace(")", "_");
        sFileName = sFileName.Replace("?", "_");
        sFileName = sFileName.Replace("+", "_");
        sFileName = sFileName.Replace("=", "_");
        sFileName = sFileName.Replace("/", "_");
        sFileName = sFileName.Replace("\\", "_");
        sFileName = sFileName.Replace("%", "_");
        sFileName = sFileName.Replace("~", "_");
        sFileName = sFileName.Replace("!", "_");
        sFileName = sFileName.Replace("|", "_");
        sFileName = sFileName.Replace("{", "_");
        sFileName = sFileName.Replace("}", "_");
        sFileName = sFileName.Replace("[", "_");
        sFileName = sFileName.Replace("]", "_");

        string sFilePath = p_filepath;
        string sUploadRoot = p_filepath;

        string[] sFileProperties = new string[2];
        System.Random objRandom = new Random();
        String sRandom = objRandom.Next(1, 10000).ToString();
        String spdfName;

        DateTime currentDate;
        currentDate = DateTime.Now;
        spdfName = sRandom + "_" + currentDate.ToString("yyyyMMddHHmmssms");

        sFilePath = sUploadRoot + "/" + sFileName;

        if (!System.IO.Directory.Exists((sUploadRoot)))
        {
            System.IO.Directory.CreateDirectory((sUploadRoot));
        }

        if (sFilePath != null)
        {
            if (sFilePath != "")
            {
                if (!System.IO.File.Exists(sFilePath))
                {
                    FileStream stream = new FileStream(sFilePath, FileMode.CreateNew, FileAccess.ReadWrite);
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(buffer);
                    writer.Close();
                    stream.Close();
                }
                p_oFile.Name = sFileName;
                p_oFile.Path = p_filepath;
            }
        }
        return p_oFile;

    }

    // If the data value in p_oValue is "" OR "NA" OR "&nbps" OR IS NULL return TRUE
    // Else return false
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

    private Result ValidateScan()
    {
        OnlineLogger.Log("Validate input parameters", OnlineLogType.DEBUG);
        Result returnResult = new Result();
        returnResult.msg = "";
        returnResult.msg_code = "fail";
        int iFlag = 0;

        string sScanSource = null;

        if (Request.QueryString["scansource"] != null)
        {
            //TOOD: Validate company id, case id and user id. If not available then stop processing

            if (!IsEmptyOrNull(companyId) && !IsEmptyOrNull(userId) && !IsEmptyOrNull(caseId))
            {
                sScanSource = Request.QueryString["scansource"];
                switch (sScanSource.ToLower())
                {
                    case "payment":
                        if (Request.QueryString["paymentid"] == null || Request.QueryString["billno"] == null || Request.QueryString["nodetype"] == null || Request.QueryString["specialty"] == null || Request.QueryString["caseid"] == null)
                        {
                            OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                            iFlag = 1;
                            lblMsg.Text = "Request is missing required input parameters. You cannot proceed further.";
                            //TODO: Show message to user. Stop processing
                        }
                        else
                        {
                            string sPaymentID = Request.QueryString["paymentid"].ToString();
                            string sBillNO = Request.QueryString["billno"].ToString();
                            string sNodeType = Request.QueryString["nodetype"].ToString();
                            string sSpecialty = Request.QueryString["specialty"].ToString();
                            string sCaseID = Request.QueryString["caseid"].ToString();

                            if (!IsEmptyOrNull(sPaymentID) && !IsEmptyOrNull(sBillNO) && !IsEmptyOrNull(sNodeType) && !IsEmptyOrNull(sSpecialty) && !IsEmptyOrNull(sCaseID))
                            {
                            }
                            else
                            {
                                iFlag = 1;
                                OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                                lblMsg.Text = "Input parameters in request are invalid";
                            }
                        }
                        break;

                    case "docmgr":
                        if (Request.QueryString["caseid"] == null || Request.QueryString["nodeid"] == null || Request.QueryString["caseno"] == null)
                        {
                            OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                            iFlag = 1;
                            lblMsg.Text = "Request is missing required input parameters. You cannot proceed further.";
                            //TODO: Show message to user. Stop processing
                        }
                        else
                        {
                            string sCaseID = Request.QueryString["caseid"].ToString();
                            string sNodeID = Request.QueryString["nodeid"].ToString();
                            string sCaseNO = Request.QueryString["caseno"].ToString();


                            if (!IsEmptyOrNull(sCaseID) && !IsEmptyOrNull(sNodeID) && !IsEmptyOrNull(sCaseNO))
                            {
                            }
                            else
                            {
                                iFlag = 1;
                                OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                                lblMsg.Text = "Input parameters in request are invalid";
                            }
                        }
                        break;

                    case "billdenial":
                        if (Request.QueryString["spId"] == null || Request.QueryString["billNo"] == null || Request.QueryString["denialDate"] == null || Request.QueryString["verificationId"] == null)
                        {
                            OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                            iFlag = 1;
                            lblMsg.Text = "Request is missing required input parameters. You cannot proceed further.";
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
                                lblMsg.Text = "Input parameters in request are invalid";
                            }
                        }
                        break;
                    case "reqdoc":
                        if (!IsEmptyOrNull(companyId) && !IsEmptyOrNull(userId) && !IsEmptyOrNull(caseId))
                        {
                            if (Request.QueryString["scansource"].ToString() == "reqdoc")
                            {
                                // Bad Request: Case, account information missing in request. Operation halted.
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
                        }
                        else
                        {
                            iFlag = 1;
                            OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                        }
                        //TODO: Check the exact scan source flag name for required documents and add the request
                        //validation logic here
                        break;
                    case "appointment":
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
                        if (Request.QueryString["caseid"].ToString() == "" || Request.QueryString["caseid"].ToString() == "&nbsp;" || Request.QueryString["caseid"].ToString() == "NA")
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
                        if (Request.QueryString["eventid"].ToString() == "" || Request.QueryString["eventid"].ToString() == "&nbsp;" || Request.QueryString["eventid"].ToString() == "NA")
                        {
                            iFlag = 1;
                            if (returnResult.msg == "")
                            {
                                returnResult.msg = returnResult.msg + "event id";
                                OnlineLogger.Log("returnResult.msg Case:" + returnResult.msg, OnlineLogType.DEBUG);
                            }
                            else
                            {
                                returnResult.msg = returnResult.msg + ",event id";
                                OnlineLogger.Log("returnResult.msg Case:" + returnResult.msg, OnlineLogType.DEBUG);
                            }
                        }
                        if (Request.QueryString["visittype"].ToString() == "" || Request.QueryString["visittype"].ToString() == "&nbsp;" || Request.QueryString["visittype"].ToString() == "NA")
                        {
                            iFlag = 1;
                            if (returnResult.msg == "")
                            {
                                returnResult.msg = returnResult.msg + "visit type";
                                OnlineLogger.Log("returnResult.msg Case:" + returnResult.msg, OnlineLogType.DEBUG);
                            }
                            else
                            {
                                returnResult.msg = returnResult.msg + ",visit type";
                                OnlineLogger.Log("returnResult.msg Case:" + returnResult.msg, OnlineLogType.DEBUG);
                            }
                        }
                        if (Request.QueryString["specialityid"].ToString() == "" || Request.QueryString["specialityid"].ToString() == "&nbsp;" || Request.QueryString["specialityid"].ToString() == "NA")
                        {
                            iFlag = 1;
                            if (returnResult.msg == "")
                            {
                                returnResult.msg = returnResult.msg + "specialty id";
                                OnlineLogger.Log("returnResult.msg Case:" + returnResult.msg, OnlineLogType.DEBUG);
                            }
                            else
                            {
                                returnResult.msg = returnResult.msg + ",specialty id";
                                OnlineLogger.Log("returnResult.msg Case:" + returnResult.msg, OnlineLogType.DEBUG);
                            }
                        }
                        break;
                    case "billverification":
                        if (Request.QueryString["spId"] == null || Request.QueryString["billNo"] == null || Request.QueryString["csId"] == null || Request.QueryString["vrId"] == null)
                        {
                            OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                            iFlag = 1;
                            lblMsg.Text = "Request is missing required input parameters. You cannot proceed further.";
                        }
                        else
                        {
                            string sCaseId = Request.QueryString["csId"].ToString();
                            string sSpecialty = Request.QueryString["spId"].ToString();
                            string sBillNO = Request.QueryString["billNo"].ToString().TrimEnd(',');
                            string sVerificationId = Request.QueryString["vrId"].ToString();

                            if (!IsEmptyOrNull(sSpecialty) && !IsEmptyOrNull(sBillNO) && !IsEmptyOrNull(sCaseId) && !IsEmptyOrNull(sVerificationId))
                            {

                            }
                            else
                            {
                                iFlag = 1;
                                OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                                lblMsg.Text = "Input parameters in request are invalid";
                            }
                        }
                        break;
                    case "billeor":
                        if (Request.QueryString["spId"] == null || Request.QueryString["billNo"] == null || Request.QueryString["csId"] == null || Request.QueryString["vrId"] == null)
                        {
                            OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                            iFlag = 1;
                            lblMsg.Text = "Request is missing required input parameters. You cannot proceed further.";
                        }
                        else
                        {
                            string sCaseId = Request.QueryString["csId"].ToString();
                            string sSpecialty = Request.QueryString["spId"].ToString();
                            string sBillNO = Request.QueryString["billNo"].ToString().TrimEnd(',');
                            string sVerificationId = Request.QueryString["vrId"].ToString();

                            if (!IsEmptyOrNull(sSpecialty) && !IsEmptyOrNull(sBillNO) && !IsEmptyOrNull(sCaseId) && !IsEmptyOrNull(sVerificationId))
                            {

                            }
                            else
                            {
                                iFlag = 1;
                                OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                                lblMsg.Text = "Input parameters in request are invalid";
                            }
                        }
                        break;
                    case "verificationsent":
                        if (Request.QueryString["spId"] == null || Request.QueryString["billNo"] == null || Request.QueryString["csId"] == null || Request.QueryString["vrId"] == null)
                        {
                            OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                            iFlag = 1;
                            lblMsg.Text = "Request is missing required input parameters. You cannot proceed further.";
                        }
                        else
                        {
                            string sCaseId = Request.QueryString["csId"].ToString();
                            string sSpecialty = Request.QueryString["spId"].ToString();
                            string sBillNO = Request.QueryString["billNo"].ToString().TrimEnd(',');
                            string sVerificationId = Request.QueryString["vrId"].ToString();

                            if (!IsEmptyOrNull(sSpecialty) && !IsEmptyOrNull(sBillNO) && !IsEmptyOrNull(sCaseId) && !IsEmptyOrNull(sVerificationId))
                            {

                            }
                            else
                            {
                                iFlag = 1;
                                OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                                lblMsg.Text = "Input parameters in request are invalid";
                            }
                        }
                        break;
                    case "verificationdenial":
                        if (Request.QueryString["spId"] == null || Request.QueryString["billNo"] == null || Request.QueryString["verificationId"] == null)
                        {
                            OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                            iFlag = 1;
                            lblMsg.Text = "Request is missing required input parameters. You cannot proceed further.";
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
                                lblMsg.Text = "Input parameters in request are invalid";
                            }
                        }
                        break;
                    case "verificationpopup":
                        if (Request.QueryString["spId"] == null || Request.QueryString["billNo"] == null || Request.QueryString["verificationId"] == null || Request.QueryString["flag"] == null)
                        {
                            OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                            iFlag = 1;
                            lblMsg.Text = "Request is missing required input parameters. You cannot proceed further.";
                        }
                        else
                        {
                            string sSpecialty = Request.QueryString["spId"].ToString();
                            string sBillNO = Request.QueryString["billNo"].ToString();
                            string sVerificationId = Request.QueryString["verificationId"].ToString();
                            string sFlag = Request.QueryString["flag"].ToString();

                            if (!IsEmptyOrNull(sSpecialty) && !IsEmptyOrNull(sBillNO) && !IsEmptyOrNull(sVerificationId) && !IsEmptyOrNull(sFlag))
                            {

                            }
                            else
                            {
                                iFlag = 1;
                                OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                                lblMsg.Text = "Input parameters in request are invalid";
                            }
                        }
                        break;

                    case "generaldenial":
                        if (Request.QueryString["denialId"] == null || Request.QueryString["nodeId"] == null)
                        {
                            OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                            iFlag = 1;
                            lblMsg.Text = "Request is missing required input parameters. You cannot proceed further.";
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
                                lblMsg.Text = "Input parameters in request are invalid";
                            }
                        }
                        break;
                    case "pom":
                        if (Request.QueryString["pomid"] == null || Request.QueryString["pomstatus"] == null)
                        {
                            OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                            iFlag = 1;
                            lblMsg.Text = "Request is missing required input parameters. You cannot proceed further.";
                            //TODO: Show message to user. Stop processing
                        }
                        else
                        {
                            string sPomID = Request.QueryString["pomid"].ToString();
                            string sPomStatus = Request.QueryString["pomstatus"].ToString();

                            if (!IsEmptyOrNull(sPomID) && !IsEmptyOrNull(sPomStatus))
                            {
                            }
                            else
                            {
                                iFlag = 1;
                                OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                                lblMsg.Text = "Input parameters in request are invalid";
                            }
                        }
                        break;

                    case "jfkvisitdoc":
                        if (Request.QueryString["caseid"] == null || Request.QueryString["visitid"] == null || Request.QueryString["caseno"] == null)
                        {
                            OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                            iFlag = 1;
                            lblMsg.Text = "Request is missing required input parameters. You cannot proceed further.";
                            //TODO: Show message to user. Stop processing
                        }
                        else
                        {
                            string sCaseID = Request.QueryString["caseid"].ToString();
                            string sNodeID = Request.QueryString["visitid"].ToString();
                            string sCaseNO = Request.QueryString["caseno"].ToString();


                            if (!IsEmptyOrNull(sCaseID) && !IsEmptyOrNull(sNodeID) && !IsEmptyOrNull(sCaseNO))
                            {
                            }
                            else
                            {
                                iFlag = 1;
                                OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                                lblMsg.Text = "Input pa1330rameters in request are invalid";
                            }
                        }
                        break;
                    case "invoicepayment":
                        if (Request.QueryString["billno"] == null || Request.QueryString["paymentid"] == null)
                        {
                            OnlineLogger.Log("Request is missing required input parameters. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                            iFlag = 1;
                            lblMsg.Text = "Request is missing required input parameters. You cannot proceed further.";
                            //TODO: Show message to user. Stop processing
                        }
                        else
                        {
                            string sPaymentID = Request.QueryString["paymentid"].ToString();
                            string sBillNO = Request.QueryString["billno"].ToString();


                            if (!IsEmptyOrNull(sPaymentID) && !IsEmptyOrNull(sBillNO))
                            {
                            }
                            else
                            {
                                iFlag = 1;
                                OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
                                lblMsg.Text = "Input parameters in request are invalid";
                            }
                        }
                        break;

                    default:
                        lblMsg.Text = "Scan source is unknown. Operation halted";
                        OnlineLogger.Log("Scan source is unknown. Operation halted", OnlineLogType.DEBUG);
                        //TODO: Source is unknown. Stop processing. Do not proceed. Show error message.
                        //Put error in online log
                        break;
                }
            }
            else
            {
                iFlag = 1;
                OnlineLogger.Log("Input parameters in request are missing. Parameters:" + "caseId:-" + caseId + "companyId:" + companyId + "userId:" + userId, OnlineLogType.DEBUG);
            }
        }
        else
        {
            //TODO: Stop scanning - source is not known
            iFlag = 1;
            OnlineLogger.Log("Input parameters in request are invalid. Parameters:" + Request.QueryString.ToString(), OnlineLogType.DEBUG);
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
            OnlineLogger.Log("Validate input parameters [Status] - Succeeded", OnlineLogType.DEBUG);
        }

        OnlineLogger.Log("Validate Scan returnResult:" + returnResult, OnlineLogType.DEBUG);
        return returnResult;
    }

    private bool ValidateExtenation(string sFileName)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        bool bReturn = false;

        try
        {
            string sExtension = Path.GetExtension(sFileName);
            if (sExtension.ToLower() == ".pdf")
            {
                bReturn = true;

            }
        }
        catch (Exception ex)
        {
            bReturn = false;

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        return bReturn;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
}