/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_Patient.aspx.cs
/*Purpose              :       To Add and Edit Patient
/*Author               :       Sandeep Y
/*Date of creation     :       22 Dec 2008  s
/*Modified By          :
/*Modified Date        :
/************************************************************/

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
using Componend;
using System.Data.SqlClient;
using System.Collections;
using DotNetUtils;
using XGridPagination;
using System.Data;
using System.Text;
using System.IO;
using System.Drawing;

public partial class Bill_Sys_PatientEntry : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    Bill_Sys_CaseObject _objCaseObject;
    private DataTable dt = new DataTable();
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    Bill_Sys_CheckoutBO _obj_CheckOutBO;
    Bill_Sys_Calender _bill_Sys_Calender;
    ArrayList objAdd;
    Bill_Sys_Visit_BO _bill_Sys_Visit_BO;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            DataSet ds = new DataSet();
            DataSet DoctorName=new DataSet();
            Bill_Sys_CheckoutBO _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
          
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtUserId.Text = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();
 
            //ds = _obj_CheckOutBO.GetProcedureGroupID(txtUserId.Text, txtCompanyID.Text);
            DoctorName = _obj_CheckOutBO.GetDoctorUserID(txtUserId.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            txtDoctorId.Text = DoctorName.Tables[0].Rows[0][0].ToString();
            //if (ds.Tables.Count > 0)
            //{
            //    txtProcedureGroupId.Text = ds.Tables[0].Rows[0][0].ToString();
            //}

            this.con.SourceGrid = grdPatientList;
            this.txtSearchBox.SourceGrid = grdPatientList;
            this.grdPatientList.Page = this.Page;
            this.grdPatientList.PageNumberList = this.con;
            this.Title = "Patient List";


            ajAutoName.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlPatientLocation.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

             DataSet dsMadatoryField = new DataSet();       

           dsMadatoryField= BindMadatoryField();

           if (dsMadatoryField.Tables[0].Rows.Count > 0)
           {



               string str = null;
               for (int i = 0; i < dsMadatoryField.Tables[0].Rows.Count; i++)
               {


                   if (dsMadatoryField.Tables[0].Rows.Count > 1)
                   {
                       if (str == null)
                       {
                           str = "'frmPatient','txtPatientFName,txtPatientLName,extddlCaseType," + dsMadatoryField.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString();
                           string strControal = dsMadatoryField.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString();
                           strControal = "l" + strControal;

                           Label lblControl = (Label)Page.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl(strControal);
                           lblControl.Visible = true;
                       } if (i == dsMadatoryField.Tables[0].Rows.Count - 1)
                       {
                           str = str + "," + dsMadatoryField.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString() + "'";

                           string strControal1 = dsMadatoryField.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString();
                           strControal1 = "l" + strControal1;
                           Label lblControl1 = (Label)Page.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl(strControal1);
                           lblControl1.Visible = true;
                       }
                       else
                       {
                           str = str + "," + dsMadatoryField.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString();

                           string strControal1 = dsMadatoryField.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString();
                           strControal1 = "l" + strControal1;
                           Label lblControl1 = (Label)Page.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl(strControal1);
                           lblControl1.Visible = true;
                       }
                   }
                   else
                   {
                       str = "'frmPatient','txtPatientFName,txtPatientLName,extddlCaseType," + dsMadatoryField.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString() + "'";
                       string strControal = dsMadatoryField.Tables[0].Rows[i]["SZ_CONTROL_NAME"].ToString();
                       strControal = "l" + strControal;

                       Label lblControl = (Label)Page.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl(strControal);
                       lblControl.Visible = true;

                   }
               }
               btnSave.Attributes.Add("onclick", "return formValidator(" + str + ");");

           }
           else
           {
               btnSave.Attributes.Add("onclick", "return formValidator('frmPatient','txtPatientFName,txtPatientLName,extddlCaseType');");

           }

               btnUpdate.Attributes.Add("onclick", "return formValidator('frmPatient','txtPatientFName,txtPatientLName,txtDateOfBirth,txtPatientAddress,txtPatientCity,txtPatientPhone,txtDateofAccident,extddlCaseType,extddlCaseStatus');");
                                                     
           
            lblChart.Visible = false;
            txtChartNo.Visible = false;
            txtRefChartNumber.Visible = false;
            chkAutoIncr.Visible = false;
            lblChartNo.Visible = false;
            
            

            bool bt_referring_facility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
            if ((bt_referring_facility == false ) && (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
            {
                lblChart.Visible = false;
                txtChartNo.Visible = false;
                 
            }
            else if ((bt_referring_facility == false) && (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "0"))
            {
                lblChart.Visible = true;
                txtChartNo.Visible = true;
               
            }

            if ((bt_referring_facility == true) || (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
            {
               

                lblLocation.Visible = false;
                extddlLocation.Visible = false;
             

            }
            else
            {
             
                //lblLocation.Visible = true;
                //extddlLocation.Visible = true;
             

            }

            extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
            extddlProvider.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();           
            extddlLocation.Flag_ID = txtCompanyID.Text.ToString();
            if (Session["Flag"] != null && Session["Flag"].ToString() == "true")
            {
                
                grdPatientList.Visible = false;
                btnUpdate.Visible = false;
            }
            else
            {
                
                if (!IsPostBack)
                {
                  
                    btnUpdate.Enabled = false;
                }
            }
                      extddlCaseStatus.Text = GetOpenCaseStatus();// "CS000000000000000001";
            extddlCaseStatus.Enabled = false;
            
            GetRefCompany();
            GetPreviousChartNo(txtCompanyID.Text);

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
        /*vivek 18_Feb_2010*/
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_Patient.aspx");
        }
        #endregion

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        Session["Flag"] = null;
        base.OnUnload(e);

    }

    public DataSet BindMadatoryField()
    {
        DataSet ds = new DataSet();
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            String strConn;
            SqlConnection conn;
            SqlCommand comm;
            SqlDataAdapter da;
            SqlDataReader dr;
            conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            conn.Open();
            comm = new SqlCommand();
            comm.CommandText = "SP_GET_SELECTED_MANDATORY_FIELD";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PAGE", "Bill_Sys_Patient.aspx");
            string szCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCmpId);
            da = new SqlDataAdapter(comm);

            da.Fill(ds);
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
        return ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }   

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            string strCaseType = _associateDiagnosisCodeBO.GetCaseTypeName(extddlCaseType.Text);
            if (strCaseType == "WC000000000000000001")//(chkAssociateCode.Checked)
            {
                txtAssociateDiagnosisCode.Text = "1";
            }
            else
            {
                txtAssociateDiagnosisCode.Text = "0";
            }
            _editOperation.Primary_Value = Session["PatientID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "Patient.xml";
            _editOperation.UpdateMethod();


            _editOperation.Primary_Value = txtAccidentID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "PatientAccidentInfo.xml";
            _editOperation.UpdateMethod();


            _editOperation.Primary_Value = txtCaseID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "PatientCaseMaster.xml";
            _editOperation.UpdateMethod();


            BindGrid();
           
            //lblMsg.Visible = true;
            Page.MaintainScrollPositionOnPostBack = false;
            //lblMsg.Text = " Patient Information Updated successfully ! ";

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

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _listOperation = new ListOperation();
        try
        {
            _listOperation.WebPage = this.Page;
            _listOperation.Xml_File = "Patient.xml";
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
                          
    private string GetOpenCaseStatus()
    {
        String strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        SqlDataAdapter sqlda;
        SqlDataReader dr;
        DataSet ds;
        string caseStatusID = "";
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("Select SZ_CASE_STATUS_ID FROM MST_CASE_STATUS WHERE SZ_STATUS_NAME='OPEN' AND SZ_COMPANY_ID='" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "'", sqlCon);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                caseStatusID = Convert.ToString(dr[0].ToString());
            }
        }
        catch { }
        return caseStatusID;
    }

    private string GetWCCaseType()
    {
        String strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        SqlDataAdapter sqlda;
        SqlDataReader dr;
        DataSet ds;
        string caseStatusID = "";
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("Select SZ_CASE_TYPE_ID FROM MST_CASE_TYPE WHERE SZ_CASE_TYPE_ID='" + Session["CASETYPE"].ToString() + "' AND SZ_ABBRIVATION_ID='WC000000000000000001' AND SZ_COMPANY_ID='" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "'", sqlCon);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                caseStatusID = Convert.ToString(dr[0].ToString());
            }
        }
        catch { }
        return caseStatusID;
    }

    private string GetNFCaseType()
    {
        String strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        SqlDataAdapter sqlda;
        SqlDataReader dr;
        DataSet ds;
        string caseStatusID = "";
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SELECT SZ_CASE_TYPE_ID FROM MST_CASE_TYPE WHERE SZ_CASE_TYPE_ID='" + Session["CASETYPE"].ToString() + "' AND SZ_ABBRIVATION_ID='WC000000000000000002' AND SZ_COMPANY_ID='" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "'", sqlCon);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                caseStatusID = Convert.ToString(dr[0].ToString());
            }
        }
        catch { }
        return caseStatusID;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        Boolean chartflag = false;
        if (txtChartNo.Visible == true)
        {
            if (!txtChartNo.Text.Equals(""))
            {
                chartflag = true;

                if (!_bill_Sys_PatientBO.ExistChartNumber(txtCompanyID.Text, txtChartNo.Text, "CHART"))
                {
                    SaveInformation();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "<script language='javascript'>alert('" + txtChartNo.Text + "' + ' chart no is already exist ...!');</script>", true);
                }
            }
        }
        else if (txtRefChartNumber.Visible == true)
        {
            if (!txtRefChartNumber.Text.Equals(""))
            {
                chartflag = true;
                if (!_bill_Sys_PatientBO.ExistChartNumber(txtCompanyID.Text, txtRefChartNumber.Text, "REF"))
                {
                    SaveInformation();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "<script language='javascript'>alert('" + txtRefChartNumber.Text + "' + ' chart no is already exist');</script>", true);
                }
            }
        }

        if (!chartflag)
        {
            SaveInformation();
        }
    }   

    public void SaveInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            txtDateofAccident.Text = DotNetUtils.UtilDate.FormatDate("", txtDateofAccident.Text.ToString());
            if (txtDateOfBirth.Text != "")
            {
                txtDateOfBirth.Text = DotNetUtils.UtilDate.FormatDate("", txtDateOfBirth.Text.ToString());
            }
            if (txtDateOfInjury.Text != "")
            {
                txtDateOfInjury.Text = DotNetUtils.UtilDate.FormatDate("", txtDateOfInjury.Text.ToString());
            }

            DateTime dtPassDate = new DateTime();
            if (txtDateofAccident.Text != "")
                dtPassDate = Convert.ToDateTime(txtDateofAccident.Text);

            if (extddlLocation.Visible == true || DateTime.Today >= dtPassDate)
            {
                foreach (ListItem li in rdolstPatientType.Items)
                {
                    if (li.Selected == true)
                    {
                        txtPatientType.Text = li.Value.ToString();
                        break;
                    }
                }

                ArrayList _arrayList = new ArrayList();

                _arrayList.Add(txtPatientFName.Text);
                _arrayList.Add(txtPatientLName.Text);
                if (txtDateofAccident.Text != "") { _arrayList.Add(txtDateofAccident.Text); } else { _arrayList.Add(null); }                 
                _arrayList.Add(extddlCaseType.Text);

                if (txtDateOfBirth.Text != "") { _arrayList.Add(txtDateOfBirth.Text); } else { _arrayList.Add(null); }
                _arrayList.Add(txtCompanyID.Text);
                _arrayList.Add("existpatient");
                _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                String iexists = _bill_Sys_PatientBO.CheckPatientExists(_arrayList);


                if (iexists != "")
                {
                    msgPatientExists.InnerHtml = iexists;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "openExistsPage();", true);                    
                }
                else
                {
                    SaveData();

                    // Notes - Case Created 
                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "CASE_CREATED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "";

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);



                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        usrMessage.PutMessage("Patient Added Successfully");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();                        
                    }
                    else
                    {
                        usrMessage.PutMessage("Patient Added Successfully");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();                        
                    }
                    BindPatientGrid();
                    hidIsSaved.Value = "1";
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "alert('Accident date should not be greater than current date ...!');", true);                
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
                   
    private void SaveData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        Billing_Sys_ManageNotesBO manageNotes;
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();

        try
        {
            string strCaseType = _associateDiagnosisCodeBO.GetCaseTypeName(extddlCaseType.Text);
            if (strCaseType == "WC000000000000000001")//(chkAssociateCode.Checked)
            {
                txtAssociateDiagnosisCode.Text = "1";
            }
            else
            {
                txtAssociateDiagnosisCode.Text = "0";
            }

            GetDoctorsLocation();





            // check Ref chart number checkbox checked or not

            if (chkAutoIncr.Checked)
            {
                txtRefChartNumber.Text = "";
                string ID = "";
                ID = _bill_Sys_PatientBO.GetMaxChartNumber(txtCompanyID.Text);
                if (ID != "")
                {
                    txtRefChartNumber.Text = ID;
                }
                else
                {
                    txtRefChartNumber.Text = "1";
                }
            }


            manageNotes = new Billing_Sys_ManageNotesBO();

            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "DoctorScreenPatientEntry.xml";            
            _saveOperation.SaveMethod();

         
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            Session["PassedCaseID"] = _bill_Sys_PatientBO.GetLatestEnterCaseID(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            _objCaseObject = new Bill_Sys_CaseObject();
            CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
            _objCaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(Session["PassedCaseID"].ToString(), "");
            _objCaseObject.SZ_CASE_ID = Session["PassedCaseID"].ToString();
            _objCaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_objCaseObject.SZ_PATIENT_ID);
            _objCaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_objCaseObject.SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            _objCaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            Session["CASE_OBJECT"] = _objCaseObject;
            Session["QStrCaseID"] = _objCaseObject.SZ_CASE_ID;
            Session["Case_ID"] = _objCaseObject.SZ_CASE_ID;
            Session["Archived"] = "0";
            Session["QStrCID"] = _objCaseObject.SZ_CASE_ID;
            Session["SelectedID"] = _objCaseObject.SZ_CASE_ID;
            Session["DataEntryFlag"] = true;
           
            Page.MaintainScrollPositionOnPostBack = false;           
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

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SaveData();
            // Notes - Case Created 
            _DAO_NOTES_EO = new DAO_NOTES_EO();
            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "CASE_CREATED";
            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "";

            _DAO_NOTES_BO = new DAO_NOTES_BO();
            _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);



            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                usrMessage.PutMessage("Patient Added Successfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                //Response.Redirect("Bill_Sys_ReCaseDetails.aspx", false);
            }
            else
            {
                usrMessage.PutMessage("Patient Added Successfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                //Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
            }
            hidIsSaved.Value = "1";

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

    protected void GetRefCompany()
    {
        _bill_Sys_PatientBO= new Bill_Sys_PatientBO();
        string id = "";
        id = _bill_Sys_PatientBO.GetRefCompany(txtCompanyID.Text);
        if (id == "True" && (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
        {
            txtRefChartNumber.Visible = true;
            chkAutoIncr.Visible = false;
            lblChartNo.Visible = true;
           
        
        }
        else if (id == "False" && (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "0"))
        {
            txtRefChartNumber.Visible = false ;
            chkAutoIncr.Visible = false ;
            lblChartNo.Visible = false;
          
        }

    }

    private void GetPreviousChartNo(string _companyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            DataTable dt = new DataTable();
            bool bt_referring_facility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
            dt = _bill_Sys_PatientBO.Get_Max_RFO_and_ChartNo(_companyID);
            if (bt_referring_facility == true && (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
            {
                lblPchartno.Visible = true;
                lblPreChartNo.Visible = true;
                lblPreChartNo.Text = dt.Rows[0]["RFO_CHART_NO"].ToString();

            }
            else if (bt_referring_facility == false && (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
            {
                lblPchartno.Visible = true;
                lblPreChartNo.Visible = true;
                lblPreChartNo.Text = dt.Rows[0]["CHART_NO"].ToString();
                
                

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

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            BindPatientGrid();
        }
        catch(Exception ex)
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

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        DataSet dsForExportToExcel = new DataSet();
        dsForExportToExcel = BindExportToExcel();
        grdPatientListExportToExcel.DataSource = dsForExportToExcel;
        grdPatientListExportToExcel.DataBind();
        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdPatientList.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
        StringBuilder strHtml = new StringBuilder();
        strHtml.Append("<table border='1px'>");
        for (int icount = 0; icount < grdPatientListExportToExcel.Rows.Count; icount++)
        {
            if (icount == 0)
            {
                strHtml.Append("<tr>");
                for (int i = 0; i < grdPatientListExportToExcel.Columns.Count; i++)
                {
                    if (grdPatientListExportToExcel.Columns[i].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdPatientListExportToExcel.Columns[i].HeaderText);
                        strHtml.Append("</td>");
                    }
                }

                strHtml.Append("</tr>");
            }

            strHtml.Append("<tr>");
            for (int j = 0; j < grdPatientListExportToExcel.Columns.Count; j++)
            {
                if (grdPatientListExportToExcel.Columns[j].Visible == true)
                {
                    strHtml.Append("<td>");
                    
                        strHtml.Append(grdPatientListExportToExcel.Rows[icount].Cells[j].Text);
                    

                    strHtml.Append("</td>");
                }
            }
            strHtml.Append("</tr>");
        }
        strHtml.Append("</table>");
        string filename = getFileName("EXCEL") + ".xls";
        StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
        sw.Write(strHtml);
        sw.Close();

        Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);
    }

    private string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }
    
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }   

    protected void grdPatientList_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Open Form")
            {                
                String szProcedureGroupID = grdPatientList.DataKeys[index][0].ToString();
                String szProcedureGroup = grdPatientList.DataKeys[index][1].ToString();
                String szEventID = grdPatientList.DataKeys[index][2].ToString();
                String szVisitType = grdPatientList.DataKeys[index][3].ToString();
                String szCaseID = grdPatientList.DataKeys[index][4].ToString();
                TransferControl(szEventID, szProcedureGroupID, szProcedureGroup, szVisitType, szCaseID);
            }
            else if (e.CommandName == "Add Visit")
            {
                Session["SZ_CASE_ID"] = grdPatientList.DataKeys[index][4].ToString();
                Session["SZ_PATIENT_NAME"] = grdPatientList.DataKeys[index][5].ToString();
                Session["SZ_PATIENT_ID"] = grdPatientList.DataKeys[index][6].ToString();
                Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                if (_bill_Sys_Calender.CheckReferralExists(txtDoctorId.Text, txtCompanyID.Text))
                {
                    RowProcedureCode.Visible = true;
                    RowProcedureCodeList.Visible = true;
                    RowVisitStatus.Visible = true;
                    GetProcedureCode(txtDoctorId.Text);
                }
                else
                {
                    RowProcedureCode.Visible = false;
                    RowProcedureCodeList.Visible = false;
                    RowVisitStatus.Visible = false;
                }
                txtVisitDate.Text = DateTime.Today.Date.ToString("MM/dd/yyyy");
                BindVisiTypeList(ref listVisitType);
                ModalPopupExtender1.Show();
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

    public void TransferControl(String p_szEventID, String p_szProcedureGroupID, String p_szProcedureGroup, String p_szVisitType, String p_szCaseID)
    {

        SpecialityPDFDAO _obj = new SpecialityPDFDAO();
        _obj.ProcedureGroup = p_szProcedureGroup;
        _obj.ProcedureGroupID = p_szProcedureGroupID;
        _obj.VisitType = p_szVisitType;
        _obj.CaseID = p_szCaseID;
        _obj.EventID = p_szEventID;
        Session["SPECIALITY_PDF_OBJECT"] = _obj;
        Session["eventId"] = p_szEventID;

        if (p_szProcedureGroup == "PT" || p_szProcedureGroup == "pt")
        {
            Response.Redirect("../Bill_Sys_CO_PTNotes.aspx?EID=" + p_szEventID, false);
        }
        else if (p_szProcedureGroup == "IM" || p_szProcedureGroup == "im")
        {
            if (p_szVisitType == "IE")
            {
                Response.Redirect("../Bill_Sys_IM_HistoryOfPresentIillness.aspx?EID=" + p_szEventID, false);
            }
            else if (p_szVisitType == "FU")
            {
                Response.Redirect("../Bill_Sys_FUIM_StartExamination.aspx?EID=" + p_szEventID, false);
            }
            else
            {
                Response.Redirect("../Bill_Sys_CO_PTNotes_1.aspx?EID=" + p_szEventID, false);
            }
        }
        else if (p_szProcedureGroup == "AC" || p_szProcedureGroup == "ac")
        {
            if (p_szVisitType == "FU")
            {
                Response.Redirect("../Bill_Sys_AC_Acupuncture_Followup.aspx?EID=" + p_szEventID, false);
            }
            else if (p_szVisitType == "IE")
            {
                Response.Redirect("../Bill_Sys_AC_Accu_Initial_1.aspx?EID=" + p_szEventID, false);
            }
            else
            {
                Response.Redirect("../Bill_Sys_CO_PTNotes_1.aspx?EID=" + p_szEventID, false);
            }
        }
        else if (p_szProcedureGroup == "ROM" || p_szProcedureGroup == "rom")
        {
            Response.Redirect("../Bill_Sys_Rom.aspx?EID=" + p_szEventID, false);
        }
        else if (p_szProcedureGroup == "CH" || p_szProcedureGroup == "ch")
        {
            if (p_szVisitType == "IE")
            {
                Response.Redirect("../Bill_Sys_CO_Chiro_Ca.aspx?EID=" + p_szEventID, false);
            }
            else
            {
                Response.Redirect("../Bill_Sys_CO_PTNotes_1.aspx?EID=" + p_szEventID, false);
            }
        }
        else
        {
            Response.Redirect("../Bill_Sys_CO_PTNotes_1.aspx?EID=" + p_szEventID, false);
        }
    }

    protected void con_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int i = Convert.ToInt32(con.SelectedValue);
        grdPatientList.SelectablePageIndexChanged(Convert.ToInt32(con.SelectedValue) - 1);
        grdPatientList.XGridDatasetNumber = 2;
        grdPatientList.XGridBindSearch();
        DataTable objDSLocationWise = new DataTable();
        objDSLocationWise = (DataTable)grdPatientList.XGridDataset;
        con.SelectedValue = i.ToString();


        for (int j = 0; j < grdPatientList.Rows.Count; j++)
        {
            if (grdPatientList.DataKeys[j][3].ToString().Equals(""))
            {
                Label lblOpen = new Label();
                LinkButton lnk = new LinkButton();
                Label lblVisit = new Label();
                LinkButton lnkVisit = new LinkButton();
                lblOpen = (Label)grdPatientList.Rows[j].FindControl("lblOpenForm");
                lnk = (LinkButton)grdPatientList.Rows[j].FindControl("lnkOpenForms");
                lblVisit = (Label)grdPatientList.Rows[j].FindControl("lblVisitType");
                lnkVisit = (LinkButton)grdPatientList.Rows[j].FindControl("lnkAddVisit");
                lnk.Visible = false;
                lblOpen.Visible = true;
                lnkVisit.Visible = true;
                lblVisit.Visible = false;            
            }
        }
    }

    public DataSet BindExportToExcel()
    {
    
        //SqlParameter sqlParam = new SqlParameter();
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_DOCTOR_PATIENT_LIST", sqlCon);
            sqlCmd.CommandType  = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            //sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", txtUserId.Text);             
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", txtPatientName.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", extddlPatientLocation.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
            sqlCmd.Parameters.AddWithValue("@I_START_INDEX", "1");
            sqlCmd.Parameters.AddWithValue("@SZ_ORDER_BY", "");
            sqlCmd.Parameters.AddWithValue("@I_END_INDEX", "100000");
            sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", ""); 
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

    public void BindPatientGrid()
    {            
        grdPatientList.XGridBindSearch();

        for (int i = 0; i < grdPatientList.Rows.Count; i++)
        {
            if (grdPatientList.DataKeys[i][3].ToString().Equals(""))
            {
                Label lblOpen = new Label();
                LinkButton lnk = new LinkButton();
                Label lblVisit = new Label();
                LinkButton lnkVisit = new LinkButton();
                lblOpen = (Label)grdPatientList.Rows[i].FindControl("lblOpenForm");
                lnk = (LinkButton)grdPatientList.Rows[i].FindControl("lnkOpenForms");
                lblVisit = (Label)grdPatientList.Rows[i].FindControl("lblVisitType");
                lnkVisit = (LinkButton)grdPatientList.Rows[i].FindControl("lnkAddVisit");
                lnk.Visible = false;
                lblOpen.Visible = true;
                lnkVisit.Visible = true;
                lblVisit.Visible = false;                                                
            }
        }        
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();                 
    }

    private void GetProcedureCode(string p_szDoctorID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
        try
        {
            lstProcedureCode.Items.Clear();
            lstProcedureCode.DataSource = _bill_Sys_Calender.GetReferringDoctorProcedureCodeList(txtCompanyID.Text, txtDoctorId.Text);
            lstProcedureCode.DataTextField = "DESCRIPTION";
            lstProcedureCode.DataValueField = "CODE";
            lstProcedureCode.DataBind();
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

    protected void btnAddVisit_onClick(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            ArrayList objL;

            String patientname = "";
            int intIncorrectVisits = 0;
            int intIncorrectVisits2 = 0;
            int intIncorrectVisits3 = 0;
            Boolean iEvisitExists = false;
            Boolean visitExists = false;
                               

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings.Get("MyConnectionString"));
            SqlCommand comd = new SqlCommand("SP_CHECK_INITIALE_VALUATIONEXISTS");
            comd.CommandType = CommandType.StoredProcedure;
            comd.Connection = con;
            comd.Connection.Open();
            comd.Parameters.AddWithValue("@SZ_CASE_ID", Session["SZ_CASE_ID"].ToString());
            comd.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
            comd.Parameters.AddWithValue("@SZ_PATIENT_ID", Session["SZ_PATIENT_ID"].ToString());
            comd.Parameters.AddWithValue("@SZ_DOCTOR_ID", txtDoctorId.Text);
            comd.Parameters.AddWithValue("@VISIT_DATE", txtVisitDate.Text);
            string visitType = listVisitType.SelectedItem.Text;
            comd.Parameters.AddWithValue("@VISIT_TYPE", visitType);


            SqlParameter objIEExists = new SqlParameter("@INITIAL_EXISTS", SqlDbType.Bit);
            objIEExists.Direction = ParameterDirection.Output;
            comd.Parameters.Add(objIEExists);
            SqlParameter objVisitStatus = new SqlParameter("@VISIT_EXISTS", SqlDbType.Bit, 20);
            objVisitStatus.Direction = ParameterDirection.Output;
            comd.Parameters.Add(objVisitStatus);
            comd.ExecuteNonQuery();
            comd.Connection.Close();

                    iEvisitExists = Convert.ToBoolean(objIEExists.Value);
                    visitExists = Convert.ToBoolean(objVisitStatus.Value);

                    if (listVisitType.SelectedIndex  != 0 && iEvisitExists == false && lstProcedureCode.Visible == false)
                    {                         
                        intIncorrectVisits++;
                    }
                    if (listVisitType.SelectedIndex == 0 && iEvisitExists == true)
                    {                        
                        intIncorrectVisits2++;
                    }
                    else if (visitExists == true)
                    {                         
                        intIncorrectVisits3++;
                    }                     
               
            if (intIncorrectVisits > 0 && lstProcedureCode.Visible == false)
            {

                usrMessage.PutMessage("Schedules cannot be saved because patient is visiting first time hence there visit type should be Initial Evaluation.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                //lblMsg.Text = "Schedules cannot be saved because patient shown in yellow color are visiting first time hence there visit type should be Initial Evaluation.";
                //lblMsg.Focus();
                return;
            }
            if (intIncorrectVisits2 > 0)
            {
                usrMessage.PutMessage("Schedules cannot be saved because patient  already has Initial Evaluation.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                //lblMsg.Text = "Schedules cannot be saved because patient shown in pink color already has Initial Evaluation.";
                //lblMsg.Focus();
                return;
            }
            if (intIncorrectVisits3 > 0)
            {
                usrMessage.PutMessage("Schedules cannot be saved because patient already has this visit");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                //lblMsg.Text = "Schedules cannot be saved because patient shown in red color already has this visit";
                //lblMsg.Focus();
                return;
            }
             
                if (lstProcedureCode.Visible == false && Convert.ToDateTime(txtVisitDate.Text) > Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "alert('Visit for future date cannot be added...!');", true);                   
                }
                else
                {
                    Boolean hStatus = false;


                    _bill_Sys_Calender = new Bill_Sys_Calender();
                    objAdd = new ArrayList();
                    objAdd.Add(Session["SZ_CASE_ID"].ToString());//Case Id
                    objAdd.Add(txtVisitDate.Text);//Appointment date
                    objAdd.Add("8.30");//Appointment time
                    objAdd.Add("");//Notes                     
                    objAdd.Add(txtDoctorId.Text);
                    objAdd.Add("TY000000000000000003");//Type
                    objAdd.Add(txtCompanyID.Text);
                    objAdd.Add("AM");
                    objAdd.Add("9.00");
                    objAdd.Add("AM");
                    objAdd.Add("2");



                    // Save default Visit Type as 'C' (Consultation')
                    if (lstProcedureCode.Visible == true)
                    {
                        Bill_Sys_PatientVisitBO objPV = new Bill_Sys_PatientVisitBO();
                        String szDefaultVisitTypeID = objPV.GetDefaultVisitType(txtCompanyID.Text);
                        objAdd.Add(szDefaultVisitTypeID);//Type
                    }
                    else
                        objAdd.Add(listVisitType.SelectedItem.Value);//Type
                    _bill_Sys_Calender.SaveEvent(objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());


                    ArrayList objGetEvent = new ArrayList();
                    objGetEvent.Add(Session["SZ_CASE_ID"].ToString());                    
                    objGetEvent.Add(txtDoctorId.Text);
                    objGetEvent.Add(txtCompanyID.Text);
                    int eventID = _bill_Sys_Calender.GetEventID(objGetEvent);
                    if (lstProcedureCode.Visible == true)
                    {
                        _bill_Sys_Calender = new Bill_Sys_Calender();
                        objAdd = new ArrayList();
                        objAdd.Add(eventID);
                        objAdd.Add(false);                      
                        objAdd.Add(ddlStatus.SelectedValue);
                        _bill_Sys_Calender.UPDATE_Event_Status(objAdd);


                        foreach (ListItem lstItem in lstProcedureCode.Items)
                        {
                            if (lstItem.Selected == true)
                            {
                                objAdd = new ArrayList();
                                objAdd.Add(lstItem.Value);
                                objAdd.Add(eventID);                             
                                objAdd.Add(ddlStatus.SelectedValue);
                                _bill_Sys_Calender.Save_Event_RefferPrcedure(objAdd);
                            }
                        }
                    }
                    // Start : Save appointment Notes.

                    _DAO_NOTES_EO = new DAO_NOTES_EO();
                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Date : " + txtVisitDate.Text;

                    _DAO_NOTES_BO = new DAO_NOTES_BO();
                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    _DAO_NOTES_EO.SZ_CASE_ID = Session["SZ_CASE_ID"].ToString();
                    _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                    hStatus = true;
                    // End 

                    if (hStatus == true)
                    {
                        usrMessage.PutMessage("Appointment scheduled successfully.");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();
                        BindPatientGrid();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindVisiTypeList(ref RadioButtonList listVisitType)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
        try
        {
            listVisitType.Items.Clear();
            listVisitType.DataSource = _bill_Sys_Calender.GET_Visit_Types(txtCompanyID.Text);
            listVisitType.DataTextField = "VISIT_TYPE";
            listVisitType.DataValueField = "VISIT_TYPE_ID";
            listVisitType.DataBind();
            listVisitType.Items[0].Selected = true;
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

    public void GetDoctorsLocation()
    {
        //To Save PAtient Locatio As Login Doctor Location

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings.Get("Connection_String"));
        SqlCommand comd = new SqlCommand("SP_GET_DOCTOR_LOCATION");
        DataSet ds = new DataSet();
        comd.CommandType = CommandType.StoredProcedure;
        comd.Connection = con;
        comd.Connection.Open();
        comd.Parameters.AddWithValue("@SZ_DOCTOR_ID", txtDoctorId.Text);
        comd.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
        SqlDataAdapter da = new SqlDataAdapter(comd);
        da.Fill(ds);
        if (ds.Tables.Count > 0)
        {
            txtLocationId.Text = ds.Tables[0].Rows[0][0].ToString();
        }
        //End Of Code
    }
}

