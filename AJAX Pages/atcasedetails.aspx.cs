/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_CaseDetails.aspx.cs
/*Purpose              :       Associatecaseno copy source to destination and deassociate case no
/*Author               :       Manoj c
/*Date of creation     :       17 Dec 2008  
/*Modified By          :       Prashant zope
/*Modified Date        :       29 April 2010
/************************************************************/

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Componend;
using log4net;
using NOTES_OBJECT;
using NotesComponent;
using Reminders;
using System.ServiceProcess;
using System.Web;
//using Microsoft.Office.Interop.Word;
public partial class AJAX_Pages_atcasedetails : PageBase
{
    SqlConnection sqlCon;
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Billing_Sys_ManageNotesBO _manageNotesBO;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private ArrayList _arrayList;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    private NotesOperation _notesOperation;
    private CaseDetailsBO _caseDetailsBO;
    private Bill_Sys_MemoBO _memoBO;
    private Patient_TVBO _patient_tvbo;

    public string caseID = "";
    private string associatecaseno = "";
    private string associtecasenoAllow = ""; // concanate allow case
    private string associatecasenoNotAllow = "";// concanate caseno for  different address
    private string associatecasenoUpdate = ""; //only update sourcepath but all destinati path case same need
    private string associatecasenoNull = ""; // concanate  all source and destination null
    Boolean updateFlag = false;
    Regex commonrange = new Regex("[^0-9)]");

    private static ILog log = LogManager.GetLogger("atCasedetails");

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        this.Page.LoadComplete += new EventHandler(Page_Load_Complete);
        log.Debug("Bill_Sys_Casedetails. Method - Page_Load_Start : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + ":" + DateTime.Now.Millisecond.ToString());
        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "ss", "plusvisibleimage();", true);

        //string urlleft = Request.Url.GetLeftPart(UriPartial.Path);
        //string url = Request.Url.ToString();

        string Param = Request.QueryString["dt"].ToString();

        string changeParam = WebUtils.DecodeUrlString(Param);

        String strPassPhrase = "Pas5pr@se";        // can be any string
        String strSaltValue = "s@1tValue";       // can be any string
        String strHashAlgorithm = "SHA1";         // can be "MD5"
        int intPasswordIterations = 2;          // can be any number
        String strInitVector = "@1B2c3D4e5F6g7H8";// must be 16 bytes
        int intKeySize = 256;

        string decrupt = Bill_Sys_EncryDecry.Decrypt(changeParam, strPassPhrase, strSaltValue, strHashAlgorithm, intPasswordIterations, strInitVector, intKeySize);

        //string decryptUrl = urlleft+"?" + decrupt;

        string[] spl = decrupt.Split('&');

        string caseid=spl[0].Replace("CaseID=","");
        string id_ = spl[1].Replace("cmp=", "");

        string compnyid= id_.Replace("'","");

        try
        {
            if (!IsPostBack)
            {
                if (caseid != null)
                {
                    //caseID = caseid;
                    if (caseid.ToString() != "")
                    {
                        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                        Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                        _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(caseid.ToString(), "");
                        _bill_Sys_CaseObject.SZ_CASE_ID = caseid.ToString();
                        if (compnyid != null)
                        {
                            _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, compnyid.ToString());
                            Session["company"] = compnyid.ToString();
                        }
                        else
                        {
                            _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, Session["company"].ToString());
                        }

                        _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                        _bill_Sys_CaseObject.SZ_COMAPNY_ID = Session["company"].ToString();
                        Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                    }

                }
                if (Session["CASE_OBJECT"] != null)
                {
                    txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    Session["company"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    Response.Redirect("Bill_Sys_SearchCase.aspx", false);
                }

                LoadNoteGrid();
                caseID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                ShowPopupNotes(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);


                //hlnkAssociate.Visible = true;
                grdAssociatedDiagnosisCode.Visible = false;
                //divAssociatedCode.Visible = true;
                _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                grdAssociatedDiagnosisCode.DataSource = _bill_Sys_ProcedureCode_BO.GetAssociatedDiagnosisCode_List(caseID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID).Tables[0];
                grdAssociatedDiagnosisCode.DataBind();
                //////////////////////
                //CREATE SESSION FOR DOC MANAGER,TEMPLATE MANAGER,Notes,Bills

                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = txtCaseID.Text;

                Session["CASEINFO"] = _bill_Sys_Case;

                Session["PassedCaseID"] = txtCaseID.Text;
                String szURL = "";
                String szCaseID = Session["PassedCaseID"].ToString();
                Session["QStrCaseID"] = szCaseID;
                Session["Case_ID"] = szCaseID;
                Session["Archived"] = "0";
                Session["QStrCID"] = szCaseID;
                Session["SelectedID"] = szCaseID;
                Session["DM_User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["SN"] = "0";
                Session["LastAction"] = "vb_CaseInformation.aspx";


                Session["SZ_CASE_ID_NOTES"] = txtCaseID.Text;

                Session["TM_SZ_CASE_ID"] = txtCaseID.Text;
              
                LoadDataOnPage();
                UserPatientInfoControl.GetPatienDeskList(((Bill_Sys_CaseObject)(Session["CASE_OBJECT"])).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
               
            }


            if (!IsPostBack)
            {
                ReminderBO objReminder = null;
                DataSet dsReminder = null;
                string strUserId = "";
                string SzCaseID = "";
                DateTime dtCurrent_Date;
                objReminder = new ReminderBO();
                dsReminder = new DataSet();
                strUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                SzCaseID = txtCaseID.Text;
                dtCurrent_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                dsReminder = objReminder.LoadReminderDetailsForCaseDeatils(strUserId, dtCurrent_Date, SzCaseID);

            }
            
            log.Debug("Bill_Sys_Casedetails. Method - Page_Load_End : " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + ":" + DateTime.Now.Millisecond.ToString());
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
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CaseDetails.aspx");
        }
        #endregion

        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_Load_Complete(object sender, EventArgs e)
    {
        try
        {
            SessionCheck.Value = ((Bill_Sys_CaseObject)(Session["CASE_OBJECT"])).SZ_CASE_ID;
        }
        catch
        {
            SessionCheck.Value = "";
        }
    }

    #region "Load Data"


    public void LoadDataOnPage()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
            _bill_Sys_CaseObject.SZ_PATIENT_ID = "";
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
          
            extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
           
            extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();
            

            if (Session["PassedCaseID"] != null)
            {
                txtPatientID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID;
                GetPatientDetails();
                Session["Associate_Case_ID"] = Session["PassedCaseID"];
                txtCaseID.Text = Session["PassedCaseID"].ToString();
                EditOperation _editOperation = new EditOperation();
                _editOperation.Xml_File = "CaseDetails.xml";
                _editOperation.WebPage = this.Page;
                _editOperation.Primary_Value = Session["PassedCaseID"].ToString();
                _editOperation.LoadData();

                _editOperation = new EditOperation();
                _editOperation.Xml_File = "CaseDetailsForLabel.xml";
                _editOperation.WebPage = this.Page;
                _editOperation.Primary_Value = Session["PassedCaseID"].ToString();
                _editOperation.LoadData();




                Bill_Sys_PatientBO _Bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                string sz_CaseCarrier = _Bill_Sys_PatientBO.CheckCarriercode(txtCaseID.Text, txtCompanyID.Text);
               


            }
            else
            {
                Response.Redirect("Bill_Sys_SearchCase.aspx", false);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion

    #region "Update Data"
    protected void UpdateData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtCaseID.Text.ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "CaseMaster.xml";
            _editOperation.UpdateMethod();


            _editOperation.Primary_Value = txtCaseID.Text.ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "PatientAcccidentInfoEntry.xml";
            _editOperation.UpdateMethod();



            LoadDataOnPage();
            lblMsg.Visible = true;
            lblMsg.Text = "Case Details Updated Successfully ...!";
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
    #endregion

    protected void Page_LoadComplete(object sender, EventArgs e)
    {

    }
    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
           
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
    private void ShowPopupNotes(string szCaseid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _manageNotesBO = new Billing_Sys_ManageNotesBO();
        _arrayList = new ArrayList();
        try
        {
            _arrayList = _manageNotesBO.GetPopupNotesDesc(szCaseid, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            for (int i = 0; i < _arrayList.Count; i++)
            {
                Response.Write("<script language='javascript'>alert('" + _arrayList[i].ToString() + "');</script>");

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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
   
   
    private void GetPatientDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SqlCommand command;
            SqlDataReader reader;
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            DataSet patientInfo = this._bill_Sys_PatientBO.GetPatientInfo(this.txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (patientInfo.Tables[0].Rows.Count > 0)
            {
                this.DtlView.DataSource = patientInfo;
                this.DtlView.DataBind();
                if (patientInfo.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;")
                {
                    //this.txtPatientFName.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                }
                if (patientInfo.Tables[0].Rows[0]["sz_source_company_name"].ToString() != "")
                {

                }

                if ((patientInfo.Tables[0].Rows[0]["BT_WRONG_PHONE"].ToString() == "True") && (patientInfo.Tables[0].Rows[0]["BT_WRONG_PHONE"].ToString() != ""))
                {
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)this.DtlView.FindControl("chkViewWrongPhone");
                    //  this.chkWrongPhone.Checked = true;
                    chk.Checked = true;
                }
                else
                {
                    //this.chkWrongPhone.Checked = false;
                }

                Label label = (Label)this.DtlView.Items[0].FindControl("lblViewMiddleName");
                label.Text = patientInfo.Tables[0].Rows[0]["MI"].ToString();

                //lblViewMiddleName.Text = patientInfo.Tables[0].Rows[0]["MI"].ToString();
                if ((patientInfo.Tables[0].Rows[0]["DT_DOB"].ToString() != "01/01/1900") && (patientInfo.Tables[0].Rows[0]["DT_DOB"].ToString() != "&nbsp;"))
                {

                }
                else
                {

                }
                Label label2 = (Label)this.DtlView.Items[0].FindControl("lblViewGender");
                label2.Text = patientInfo.Tables[0].Rows[0]["SZ_GENDER"].ToString();
                
                if ((patientInfo.Tables[0].Rows[0]["DT_INJURY"].ToString() != "01/01/1900") && (patientInfo.Tables[0].Rows[0]["DT_INJURY"].ToString() != "&nbsp;"))
                {
                    // this.txtDateOfInjury.Text = patientInfo.Tables[0].Rows[0]["DT_INJURY"].ToString();
                }
                else
                {
                    // this.txtDateOfInjury.Text = "";
                }
                // this.txtJobTitle.Text = patientInfo.Tables[0].Rows[0]["SZ_JOB_TITLE"].ToString();
                // this.txtWorkActivites.Text = patientInfo.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES"].ToString();

                if ((patientInfo.Tables[0].Rows[0]["BT_TRANSPORTATION"] == "True") && (patientInfo.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"] != ""))
                {
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)this.DtlView.Items[0].FindControl("chkTransportation");
                    chk.Checked = true;
                    chk.Checked = true;
                }
                else
                {

                }

                if (patientInfo.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString() != "&nbsp;")
                {
                    string str = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString();


                }
                //Label label7 = (Label)this.DtlView.Items[0].FindControl("lblVLocation1");
                //label7.Text = patientInfo.Tables[0].Rows[0]["sz_location_Name"].ToString();
                if ((patientInfo.Tables[0].Rows[0]["SZ_CASE_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_CASE_ID"].ToString() != ""))
                {
                    this.txtCaseID.Text = patientInfo.Tables[0].Rows[0]["SZ_CASE_ID"].ToString();
                }
                if ((patientInfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString() != ""))
                {
                    this.extddlCaseType.Text = patientInfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString();
                    this.txtCaseTypeID.Text = patientInfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString();
                    Label label8 = (Label)this.DtlView.Items[0].FindControl("lblViewCasetype");
                    if ((this.extddlCaseType.Text != "NA") && (this.extddlCaseType.Text != ""))
                    {
                        label8.Text = this.extddlCaseType.Selected_Text;
                    }
                }
                if ((patientInfo.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString() != ""))
                {
                    //this.extddlProvider.Text = patientInfo.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString();
                }
                if ((patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != ""))
                {

                }
                if ((patientInfo.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString() != ""))
                {
                    this.extddlCaseStatus.Text = patientInfo.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString();
                    Label label9 = (Label)this.DtlView.Items[0].FindControl("lblViewCaseStatus");
                    if ((this.extddlCaseStatus.Text != "NA") && (this.extddlCaseStatus.Text != ""))
                    {
                        label9.Text = this.extddlCaseStatus.Selected_Text;
                    }
                }
                if ((patientInfo.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString() != ""))
                {
                    //this.extddlAttorney.Text = patientInfo.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString();
                    //this.hdnattorneycode.Value = this.extddlAttorney.Text;
                    //Label label10 = (Label)this.DtlView.Items[0].FindControl("lblViewAttorney");
                    //if ((this.extddlAttorney.Text != "NA") && (this.extddlAttorney.Text != ""))
                    //{
                    //    label10.Text = this.extddlAttorney.Selected_Text;
                    //}
                }

                if ((patientInfo.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != ""))
                {
                    //this.txtPolicyNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString();
                    //if (this.txtPolicyNumber.Text.Equals("NA"))
                    //{
                    //    this.txtPolicyNumber.Text = "";
                    //}
                }
                if ((patientInfo.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString() != ""))
                {

                }
                if ((patientInfo.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString() != ""))
                {
                    //this.txtPolicyReport.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString();
                }

                if ((patientInfo.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString() != ""))
                {
                    //this.extddlLocation.Text = patientInfo.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString();
                    //Label label12 = (Label)this.DtlView.Items[0].FindControl("lblVLocation1");
                    //if ((this.extddlLocation.Text != "NA") && (this.extddlLocation.Text != ""))
                    //{
                    //    label12.Text = this.extddlLocation.Selected_Text;
                    //}
                }
                if (((patientInfo.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString() != "")) && ((patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "")))
                {
                    try
                    {

                    }
                    catch
                    {
                    }
                }
                string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
                this.sqlCon = new SqlConnection(connectionString);
                DataSet dataSet = new DataSet();
                try
                {
                    string str3 = "SELECT *   FROM mst_case_type_wise_ui_access_control WHERE sz_case_type_id = '" + this.txtCaseTypeID.Text + "' and sz_company_id = '" + this.txtCompanyID.Text + "' and sz_page_name = 'Workarea' ";
                    this.sqlCon.Open();
                    command = new SqlCommand(str3, this.sqlCon);
                    new SqlDataAdapter(command).Fill(dataSet);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                finally
                {
                    if (this.sqlCon.State == ConnectionState.Open)
                    {
                        this.sqlCon.Close();
                    }
                }
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        string str4 = dataSet.Tables[0].Rows[i]["sz_control_name"].ToString();
                        string str5 = dataSet.Tables[0].Rows[i]["sz_control_type"].ToString();
                        if (dataSet.Tables[0].Rows[i]["sz_access_type"].ToString() == "hidden")
                        {

                        }
                    }
                }


                string str7 = "";
                string str8 = "";
                string str9 = "";
                string str10 = "";
                string cmdText = "select SZ_INSURANCE_ID FROM  MST_SEC_INSURANCE_DETAIL  WHERE SZ_CASE_ID='" + this.txtCaseID.Text + "' AND SZ_COMPANY_ID='" + this.txtCompanyID.Text + "'";
                command = new SqlCommand(cmdText, this.sqlCon);
                SqlDataAdapter adapter2 = new SqlDataAdapter(command);
                try
                {
                    this.sqlCon.Open();
                    command = new SqlCommand(cmdText, this.sqlCon);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["SZ_INSURANCE_ID"] != DBNull.Value)
                        {
                            str7 = reader["SZ_INSURANCE_ID"].ToString();
                        }
                    }
                }
                catch (SqlException exception2)
                {
                    exception2.Message.ToString();
                }
                finally
                {
                    if (this.sqlCon.State == ConnectionState.Open)
                    {
                        this.sqlCon.Close();
                    }
                }
                if (str7 != "")
                {
                    string str11 = "select SZ_INSURANCE_NAME FROM MST_INSURANCE_COMPANY WHERE SZ_INSURANCE_ID='" + str7 + "'";
                    try
                    {
                        this.sqlCon.Open();
                        reader = new SqlCommand(str11, this.sqlCon).ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader["SZ_INSURANCE_NAME"] != DBNull.Value)
                            {
                                //this.txtSecInsName.Text = reader["SZ_INSURANCE_NAME"].ToString();
                            }
                        }
                    }
                    catch (SqlException exception3)
                    {
                        exception3.Message.ToString();
                    }
                    finally
                    {
                        if (this.sqlCon.State == ConnectionState.Open)
                        {
                            this.sqlCon.Close();
                        }
                    }
                    //added for ticket # FR-244
                    //if (str10 != "")
                    //{
                    string str13 = "select SZ_POLICY_NUMBER,SZ_CLAIM_NUMBER FROM TXN_PRIVATE_INTAKE WHERE SZ_INSURANCE_ID='" + str7 + "'";
                    try
                    {
                        this.sqlCon.Open();
                        reader = new SqlCommand(str13, this.sqlCon).ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader["SZ_POLICY_NUMBER"] != DBNull.Value)
                            {
                                // this.txtPolicy.Text = reader["SZ_POLICY_NUMBER"].ToString();
                            }
                            if (reader["SZ_CLAIM_NUMBER"] != DBNull.Value)
                            {
                                // this.txtClaim.Text = reader["SZ_CLAIM_NUMBER"].ToString();
                            }
                        }
                    }
                    catch (SqlException exception3)
                    {
                        exception3.Message.ToString();
                    }
                    finally
                    {
                        if (this.sqlCon.State == ConnectionState.Open)
                        {
                            this.sqlCon.Close();
                        }
                    }
                    //}
                    //added for ticket # FR-244
                }
                else
                {
                    //  this.txtSecInsName.Text = "";
                }
                string str12 = "select SZ_ADDRESS_ID, SZ_INSURANCE_TYPE from MST_SEC_INSURANCE_DETAIL where  SZ_CASE_ID='" + this.txtCaseID.Text + "' and SZ_COMPANY_ID='" + this.txtCompanyID.Text + "' and SZ_INSURANCE_ID='" + str7 + "'";
                try
                {
                    this.sqlCon.Open();
                    reader = new SqlCommand(str12, this.sqlCon).ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["SZ_ADDRESS_ID"] != DBNull.Value)
                        {
                            str8 = reader["SZ_ADDRESS_ID"].ToString();
                        }
                        if (reader["SZ_INSURANCE_TYPE"] != DBNull.Value)
                        {
                            str9 = reader["SZ_INSURANCE_TYPE"].ToString();
                        }
                    }
                }
                catch (SqlException exception4)
                {
                    exception4.Message.ToString();
                }
                finally
                {
                    if (this.sqlCon.State == ConnectionState.Open)
                    {
                        this.sqlCon.Close();
                    }
                }
                if (str9 != "")
                {

                }
                if (str8 != "")
                {
                    DataSet set3 = null;
                    try
                    {
                        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
                        SqlCommand selectCommand = new SqlCommand("SP_MST_INSURANCE_ADDRESS", connection);
                        selectCommand.CommandType = CommandType.StoredProcedure;
                        selectCommand.Parameters.Add("@SZ_INS_ADDRESS_ID", str8);
                        selectCommand.Parameters.Add("@FLAG", "LIST");
                        adapter2 = new SqlDataAdapter(selectCommand);
                        set3 = new DataSet();
                        adapter2.Fill(set3);
                        connection.Close();
                        if (set3.Tables[0].Rows.Count > 0)
                        {

                        }
                       // goto Label_2983;
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



                if ((patientInfo.Tables[0].Rows[0]["sz_company_name"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["sz_company_name"].ToString() != ""))
                {
                    Label label14 = (Label)this.DtlView.Items[0].FindControl("lblcopyfrom");
                    label14.Text = patientInfo.Tables[0].Rows[0]["sz_company_name"].ToString();
                }
                if (patientInfo.Tables[0].Rows[0]["sz_reffering_office_id"].ToString() != "")
                {
                }
                if (patientInfo.Tables[0].Rows[0]["sz_reffering_doctor_id"].ToString() != "")
                {
                }
                this.lblMsg.Visible = false;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
    
   
    public void LoadNoteGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _listOperation = new ListOperation();
        try
        {
           // grdNotes.CurrentPageIndex = 0;
            _listOperation.WebPage = this.Page;
            _listOperation.Xml_File = "NoteSearch.xml";
            _listOperation.LoadList();
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
    public DataSet GetViewBillInfo(string caseID, string companyID)
    {
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_CD_VIEW_BILLS", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
    }
    
    public DataSet GetPatienView(string caseID, string companyID)
    {

        DataSet ds = new DataSet();
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_PATIENTPOP_VIEW", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);

            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
    }
    public string GetCompanyName(string szCompanyId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        log.Debug("Get Company Name;");
        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(constring);
        string szCompanyName = "";
        try
        {
            con.Open();
            log.Debug("GET_COMPANY_NAME @SZ_COMPANY_ID='" + szCompanyId + "'");
            SqlCommand cmd = new SqlCommand("GET_COMPANY_NAME", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            szCompanyName = ds.Tables[0].Rows[0][0].ToString();
            log.Debug("Company Name : " + ds.Tables[0].Rows[0][0].ToString());
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
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        log.Debug("Return Company Name : " + szCompanyName);
        return szCompanyName;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
  
}