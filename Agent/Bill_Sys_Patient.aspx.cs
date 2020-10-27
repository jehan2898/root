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

public partial class Bill_Sys_Patient : PageBase
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
    SqlConnection SqlCon;
    protected void Page_Load(object sender, EventArgs e)
    {

        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            txtUserId.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            aceInsSearch.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
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

            
            //    imgbtnDateofBirth.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateOfBirth,'imgbtnDateofBirth','MM/dd/yyyy'); return false;");
            //    imgbtnDateofInjury.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateOfInjury,'imgbtnDateofInjury','MM/dd/yyyy'); return false;");
            //    imgbtnDateofAccident.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateofAccident,'imgbtnDateofAccident','MM/dd/yyyy'); return false;");

            //Atul--btnSave.Attributes.Add("onclick", "return formValidator('frmPatient','txtPatientFName,txtPatientLName,txtPatientAge,txtCaseName,extddlCaseType,extddlProvider,extddlInsuranceCompany,extddlCaseStatus,extddlAttorney,extddlAdjuster,txtClaimNumber,txtPolicyNumber,txtDateofAccident');");
            //btnUpdate.Attributes.Add("onclick", "return formValidator('frmPatient','txtPatientFName,txtPatientLName,txtPatientAge,txtCaseName,extddlCaseType,extddlProvider,extddlInsuranceCompany,extddlCaseStatus,extddlAttorney,extddlAdjuster,txtClaimNumber,txtPolicyNumber,txtDateofAccident');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmPatient','txtPatientFName,txtPatientLName,txtDateOfBirth,txtPatientAddress,txtPatientCity,txtPatientPhone,txtDateofAccident,extddlCaseType,extddlCaseStatus');");
            btnAddAttorney.Attributes.Add("onclick", "return PopupformValidator('frmAttorney','txtAttorneyFirstName,txtAttorneyLastName,txtAttorneyCity','AttorneyErrordiv');");
            btnSaveAdjuster.Attributes.Add("onclick", "return PopupformValidator('aspnetForm','txtAdjusterPopupName','AdjusterErrordiv');");
            txtPatientEmail.Attributes.Add("onfocusout", "return isEmail('frmPatient','txtPatientEmail');");
            btnSaveInsurance.Attributes.Add("onclick", "return PopupformValidator('aspnetForm','txtInsuranceCompanyName','InsuranceErrordiv');");
            txtInsuranceEmail.Attributes.Add("onfocusout", "return isEmail('frmPatient','txtInsuranceEmail');");
          //  btnSaveAddress.Attributes.Add("onclick", "return formValidator('frmPatient','txtInsuranceAddressCode');");
           
            lblChart.Visible = false;
            txtChartNo.Visible = false;
            txtRefChartNumber.Visible = false;
            chkAutoIncr.Visible = false;
            lblChartNo.Visible = false;
            
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            bool bt_referring_facility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
            if ((bt_referring_facility == false ) && (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
            {
                lblChart.Visible = false;
                txtChartNo.Visible = false;
                //lblLocation.Visible = false;
                //extddlLocation.Visible = false;
            }
            else if ((bt_referring_facility == false) && (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "0"))
            {
                lblChart.Visible = true;
                txtChartNo.Visible = true;
                //lblLocation.Visible = false;
                //extddlLocation.Visible = false;
            }

            if ((bt_referring_facility == true) || (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
            {
                //Atul- btnSave.Attributes.Add("onclick", "return formValidator('frmPatient','txtPatientFName,txtPatientLName,txtDateOfBirth,txtPatientAddress,txtPatientCity,txtPatientPhone,txtDateofAccident,extddlCaseType');");
                //btnSave.Attributes.Add("onclick", "return formValidator('frmPatient','txtPatientFName,txtPatientLName,extddlCaseType');");


                lblLocation.Visible = false;
                extddlLocation.Visible = false;
                //lblRed.Visible = false;
                //sid.Visible = true;
                //mAdd.Visible = true;
                //mCity.Visible = true;
                //mDOB.Visible = true;
                //mPhone.Visible = true;

            }
            else
            {
              //Atul--  btnSave.Attributes.Add("onclick", "return formValidator('frmPatient','extddlLocation,txtPatientFName,txtPatientLName,txtDateOfBirth,txtPatientAddress,txtPatientCity,txtPatientPhone,txtDateofAccident,extddlCaseType,extddlCaseStatus');");
               // btnSave.Attributes.Add("onclick", "return formValidator('frmPatient','extddlLocation,txtPatientFName,txtPatientLName,extddlCaseType');");

                lblLocation.Visible = true;
                extddlLocation.Visible = true;
               // lblRed.Visible = true;
                //sid.Visible = false;
                //mAdd.Visible = false;
                //mCity.Visible = false;
                //mDOB.Visible = false;
                //mPhone.Visible = false;

            }

            extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
            extddlProvider.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();
            extddlAttorney.Flag_ID = txtCompanyID.Text.ToString();
            extddlAdjuster.Flag_ID = txtCompanyID.Text.ToString();
            extddlInsuranceCompany.Flag_ID = txtCompanyID.Text.ToString();
            extddlInsuranceCode.Flag_ID = txtCompanyID.Text.ToString();
            extddlLocation.Flag_ID = txtCompanyID.Text.ToString();
            if (Session["Flag"] != null && Session["Flag"].ToString() == "true")
            {
                //TreeMenuControl1.Visible = false;
                grdPatientList.Visible = false;
                btnUpdate.Visible = false;
            }
            else
            {
                //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
                if (!IsPostBack)
                {
                   // BindGrid();
                    btnUpdate.Enabled = false;
                }
            }
            // if (Session["CASETYPE"] != null)
            // {
            //extddlCaseType.Text=Session["CASETYPE"].ToString();
            //extddlCaseType.Enabled = false;
            extddlCaseStatus.Text = GetOpenCaseStatus();// "CS000000000000000001";
            extddlCaseStatus.Enabled = false;
            //if (extddlCaseType.Text == "CT000000000000000001" ) { lblWcbNumber.Text = "WCB Number"; } else if (extddlCaseType.Text == "CT000000000000000002") { lblWcbNumber.Text = "Policy Number"; }
            if (extddlCaseType.Text == GetWCCaseType()) { lblWcbNumber.Text = "Policy Number"; } else if (extddlCaseType.Text == GetNFCaseType()) { lblWcbNumber.Text = "Policy Number"; }
            // }
            //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            //{
            //    aIncheduleTD.Visible = false;

            //    aOutcheduleTD.ColSpan = 2;
            //    aOutcheduleLink.InnerText = "Schedule";
            //}

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
        txtInsuranceAddress.Text = "";
        txtInsuranceCity.Text = "";
        txtInsuranceZip.Text = "";
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
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        DataSet ds = new DataSet();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //ArrayList _patient = new ArrayList();
        //_patient.Add(txtPatientFName.Text);
        //_patient.Add(txtPatientLName.Text);
        //_patient.Add(txtDateOfBirth.Text);
        //_patient.Add(extddlCaseType.Text);
        //_patient.Add(txtDateofAccident.Text);
        //if (_bill_Sys_PatientBO.PatientExist(_patient))
        //{
        Boolean chartflag = false;
        if (txtChartNo.Visible == true )
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
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert( '" + txtChartNo.Text + "' + ' Chart No Allready Exist ...!');</script>");
                    Page.RegisterStartupScript("mm", "<script language='javascript'>alert('" + txtChartNo.Text + "' + ' chart no is already exist ...!');</script>");

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
                    Page.RegisterStartupScript("mm", "<script language='javascript'>alert('" + txtRefChartNumber.Text + "' + ' chart no is already exist');</script>");
                }
            }
        }

        if (!chartflag)
        {
            SaveInformation();
        }
               

    }

    public void SaveInformation()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            txtPatientType.Text = "";
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
                //_arrayList.Add(txtSocialSecurityNumber.Text);
                _arrayList.Add(extddlCaseType.Text);

                if (txtDateOfBirth.Text != "") { _arrayList.Add(txtDateOfBirth.Text); } else { _arrayList.Add(null); }
                _arrayList.Add(txtCompanyID.Text);
                _arrayList.Add("existpatient");
                _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                String iexists = _bill_Sys_PatientBO.CheckPatientExists(_arrayList);


                if (iexists != "")
                {
                    msgPatientExists.InnerHtml = iexists;
                    Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage();</script>");
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



                    //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    //{
                    //    Response.Redirect("AJAX Pages/Bill_Sys_ReCaseDetails.aspx", false);
                    //}
                    //else
                    //{
                    //    Response.Redirect("AJAX Pages/Bill_Sys_CaseDetails.aspx", false);
                    //}
                    hidIsSaved.Value = "1";
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert('Accident date should not be greater than current date ...!');</script>");
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
        //}
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {//Logging Start
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
            ClearControl();
            lblMsg.Visible = true;
            Page.MaintainScrollPositionOnPostBack = false;
            lblMsg.Text = " Patient Information Updated successfully ! ";

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
    {//Logging Start
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

    protected void grdPatientList_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        try
        {
            Session["PatientID"] = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[1].Text;
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[2].Text != "&nbsp;") { txtPatientFName.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[2].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[3].Text != "&nbsp;") { txtPatientLName.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[3].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[4].Text != "&nbsp;") { txtPatientAge.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[4].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[5].Text != "&nbsp;") { txtPatientAddress.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[5].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[6].Text != "&nbsp;") { txtPatientStreet.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[6].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[7].Text != "&nbsp;") { txtPatientCity.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[7].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[8].Text != "&nbsp;") { txtPatientZip.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[8].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[9].Text != "&nbsp;") { txtPatientPhone.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[9].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[10].Text != "&nbsp;") { txtPatientEmail.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[10].Text; }

            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[11].Text != "&nbsp;") { txtMI.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[11].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[12].Text != "&nbsp;") { txtWCBNumber.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[12].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[13].Text != "&nbsp;") { txtSocialSecurityNumber.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[13].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[15].Text != "&nbsp;") { txtDateOfBirth.Text = Convert.ToDateTime(grdPatientList.Items[grdPatientList.SelectedIndex].Cells[15].Text).ToShortDateString(); }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[16].Text != "&nbsp;") { ddlSex.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[16].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[17].Text != "&nbsp;") { txtDateOfInjury.Text = Convert.ToDateTime(grdPatientList.Items[grdPatientList.SelectedIndex].Cells[17].Text).ToShortDateString(); }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[18].Text != "&nbsp;") { txtJobTitle.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[18].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[19].Text != "&nbsp;") { txtWorkActivites.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[19].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text != "&nbsp;") { txtState.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[21].Text != "&nbsp;") { txtCarrierCaseNo.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[21].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[22].Text != "&nbsp;") { txtEmployerName.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[22].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[23].Text != "&nbsp;") { txtEmployerPhone.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[23].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[24].Text != "&nbsp;") { txtEmployerAddress.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[24].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[25].Text != "&nbsp;") { txtEmployerCity.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[25].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[26].Text != "&nbsp;") { txtEmployerState.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[26].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[27].Text != "&nbsp;") { txtEmployerZip.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[27].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[28].Text != "&nbsp;") { txtWorkPhone.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[28].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[29].Text != "&nbsp;") { txtExtension.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[29].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[30].Text != "&nbsp;") { if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[30].Text.ToString() == "True") { chkWrongPhone.Checked = true; } else { chkWrongPhone.Checked = false; } }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[31].Text != "&nbsp;") { if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[31].Text.ToString() == "True") { chkTransportation.Checked = true; } else { chkTransportation.Checked = false; } }

            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            DataSet _patientAccidentDs = _bill_Sys_PatientBO.GetPatientAccidentDetails(Session["PatientID"].ToString());
            //txtAccidentID
            if (_patientAccidentDs.Tables[0].Rows.Count > 0)
            {
                if (_patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "&nbsp;") { txtAccidentID.Text = _patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); }
                if (_patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;") { txtPlatenumber.Text = _patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(); }
                if (_patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;") { txtAccidentAddress.Text = _patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(3).ToString(); }
                if (_patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;") { txtAccidentCity.Text = _patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(4).ToString(); }
                if (_patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() != "&nbsp;") { txtAccidentState.Text = _patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(5).ToString(); }
                if (_patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(6).ToString() != "&nbsp;") { txtPolicyReport.Text = _patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(6).ToString(); }
                if (_patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() != "&nbsp;") { txtListOfPatient.Text = _patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(7).ToString(); }
                if (_patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "&nbsp;") { txtDateofAccident.Text = _patientAccidentDs.Tables[0].Rows[0].ItemArray.GetValue(8).ToString(); }
            }
            Billing_Sys_ManageNotesBO manageNotes = new Billing_Sys_ManageNotesBO();
            DataSet _ds = manageNotes.GetCaseDetails(Session["PatientID"].ToString());
            if (_ds.Tables[0].Rows.Count > 0)
            {
                txtPatientID.Text = Session["PatientID"].ToString();
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "") { txtCaseID.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "") { extddlCaseType.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() != "") { extddlProvider.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "")
                {
                    extddlInsuranceCompany.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text).Tables[0];
                    lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    lstInsuranceCompanyAddress.DataValueField = "CODE";
                    lstInsuranceCompanyAddress.DataBind();
                }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() != "") { extddlCaseStatus.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() != "") { extddlAttorney.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(5).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() != "") { txtClaimNumber.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(7).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString() != "") { txtPolicyNumber.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(9).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(9).ToString() != "") { txtDateofAccident.Text = Convert.ToDateTime(_ds.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()).ToShortDateString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() != "") { extddlAdjuster.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(10).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() != "") { txtAssociateDiagnosisCode.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(11).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() != "") { txtPlatenumber.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(14).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() != "") { txtAccidentAddress.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(15).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() != "") { txtAccidentCity.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(16).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString() != "") { txtAccidentState.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(17).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(18).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(18).ToString() != "") { txtPolicyReport.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(18).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(19).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(19).ToString() != "") { txtListOfPatient.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(19).ToString(); }
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(20).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(20).ToString() != "" && _ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "")
                {
                    try
                    {
                        lstInsuranceCompanyAddress.SelectedValue = _ds.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();

                        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                        ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(lstInsuranceCompanyAddress.SelectedValue);
                        if (_arraylist.Count > 0)
                        {
                            txtInsuranceAddress.Text = _arraylist[2].ToString();
                            txtInsuranceCity.Text = _arraylist[3].ToString();
                            txtInsuranceState.Text = _arraylist[4].ToString();
                            txtInsuranceZip.Text = _arraylist[5].ToString();
                            txtInsuranceStreet.Text = _arraylist[6].ToString();
                        }
                    }
                    catch
                    {
                    }
                }
                if (txtAssociateDiagnosisCode.Text == "1")
                {
                    chkAssociateCode.Checked = true;
                }
                else
                {
                    chkAssociateCode.Checked = false;
                }
            }

            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            ArrayList arr = new ArrayList();
            arr = _bill_Sys_PatientBO.GetAdjusterDetail(extddlAdjuster.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (arr.Count > 0)
            {
                txtAdjusterPhone.Text = arr[0].ToString();
                txtAdjusterExtension.Text = arr[1].ToString();
                txtfax.Text = arr[2].ToString();
                txtEmail.Text = arr[3].ToString();
            }
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            lblMsg.Visible = false;
            hlnkShowNotes.Visible = true;
            pnlShowNotes.Visible = true;
            PopEx.Enabled = true;
            _objCaseObject = new Bill_Sys_CaseObject();
            _objCaseObject.SZ_CASE_ID = txtCaseID.Text;
            Session["CASE_OBJECT"] = _objCaseObject;

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

    protected void grdPatientList_DeleteCommand(object source, DataGridCommandEventArgs e)
    {//Logging Start
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

    protected void btnClear_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ClearControl();
            //txtPatientAge.Text = "";
            //txtCompanyID.Text = "";
            //txtPatientAddress.Text = "";
            //txtPatientCity.Text = "";
            //txtPatientEmail.Text = "";
            //txtPatientFName.Text = "";
            //txtPatientLName.Text = "";
            //txtPatientPhone.Text = "";
            //txtPatientStreet.Text = "";
            //txtPatientZip.Text = "";

            //txtMI.Text = "";
            //txtWCBNumber.Text = "";
            //txtSocialSecurityNumber.Text = "";
            //txtDateOfBirth.Text = "";
            //ddlSex.SelectedValue = "0";
            //txtDateOfInjury.Text = "";
            //txtJobTitle.Text = "";
            //txtWorkActivites.Text = "";
            //btnSave.Enabled = true;
            //btnUpdate.Enabled = false;
            //lblMsg.Visible = false;
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

    protected void grdPatientList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
 
        try
        {
            grdPatientList.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
            lblMsg.Visible = false;
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

    private void ClearControl()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            txtCarrierCaseNo.Text = "";
            txtCaseID.Text = "";
            txtClaimNumber.Text = "";
            txtDateofAccident.Text = "";
            txtDateOfBirth.Text = "";
            txtDateOfInjury.Text = "";
            txtJobTitle.Text = "";
            txtMI.Text = "";
            txtPatientAddress.Text = "";
            txtPatientAge.Text = "";
            txtPatientCity.Text = "";
            txtPatientEmail.Text = "";
            txtPatientFName.Text = "";
            txtPatientID.Text = "";
            txtPatientLName.Text = "";
            txtPatientPhone.Text = "";
            txtPatientStreet.Text = "";
            txtPatientZip.Text = "";
            txtPolicyNumber.Text = "";
            txtSocialSecurityNumber.Text = "";
            txtState.Text = "";
            txtWCBNumber.Text = "";
            txtWorkActivites.Text = "";
            extddlAdjuster.Text = "NA";
            extddlAttorney.Text = "NA";
            extddlCaseStatus.Text = "NA";
            extddlCaseType.Text = "NA";
            extddlInsuranceCompany.Text = "NA";
            extddlProvider.Text = "NA";
            ddlSex.SelectedValue = "0";
            chkTransportation.Checked = false;
            chkWrongPhone.Checked = false;
            txtWorkPhone.Text = "";
            txtExtension.Text = "";
            txtPlatenumber.Text = "";
            txtAccidentAddress.Text = "";
            txtAccidentCity.Text = "";
            txtAccidentState.Text = "";
            txtPolicyReport.Text = "";
            txtListOfPatient.Text = "";
            txtEmployerName.Text = "";
            txtEmployerAddress.Text = "";
            txtEmployerCity.Text = "";
            txtEmployerState.Text = "";
            txtEmployerZip.Text = "";
            txtEmployerPhone.Text = "";
            txtInsuranceCity.Text = "";
            txtInsuranceState.Text = "";
            txtInsuranceZip.Text = "";
            lstInsuranceCompanyAddress.Items.Clear();
            txtChartNo.Text = "";
            ClearAdjusterControl();

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            lblMsg.Visible = false;
            hlnkShowNotes.Visible = false;
            PopEx.Enabled = false;
            pnlShowNotes.Visible = false;

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

    protected void btnAccept_ButtonClick(object sender, EventArgs e)
    {
        string strInsuranceID = hdninsurancecode.Value;

        string[] newstr = strInsuranceID.Split('-');
        string sz_InsCompany_ID, sz_Ins_Addr_ID;
        sz_InsCompany_ID = newstr[0].Trim();
        sz_Ins_Addr_ID = newstr[1].Trim();
        extddlInsuranceCompany.Text = sz_InsCompany_ID;
        
        ClearInsurancecontrol();
        extddlInsuranceCode.Text = extddlInsuranceCompany.Text;
        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);
       // lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress("II000000000000000733");
        DataSet dslstInsuranceCompanyAddress = new DataSet();
        dslstInsuranceCompanyAddress = (DataSet)lstInsuranceCompanyAddress.DataSource;
        lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
        lstInsuranceCompanyAddress.DataValueField = "CODE";
        if (dslstInsuranceCompanyAddress.Tables[0].Rows.Count == 0)
        {
            lstInsuranceCompanyAddress.Items.Clear();
        }
        else
        {

            lstInsuranceCompanyAddress.DataBind();
            lstInsuranceCompanyAddress.SelectedValue = sz_Ins_Addr_ID;
            ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[0][0].ToString());
            txtInsuranceAddress.Text = _arraylist[2].ToString();
            txtInsuranceCity.Text = _arraylist[3].ToString();
            // txtInsuranceState.Text = _arraylist[4].ToString();
            extddlInsuranceState.Text = _arraylist[4].ToString();
            txtInsuranceZip.Text = _arraylist[5].ToString();
            txtInsuranceStreet.Text = _arraylist[6].ToString();
            if (_arraylist[8].ToString() != "")
                extddlInsuranceState.Text = _arraylist[8].ToString();
            Page.MaintainScrollPositionOnPostBack = true;

            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            ArrayList _arraylist1 = _bill_Sys_PatientBO.GetInsuranceAddressDetail(lstInsuranceCompanyAddress.SelectedValue);
            txtInsuranceAddress.Text = _arraylist1[2].ToString();
            txtInsuranceCity.Text = _arraylist1[3].ToString();
            txtInsuranceState.Text = _arraylist1[4].ToString();
            txtInsuranceZip.Text = _arraylist1[5].ToString();
            txtInsuranceStreet.Text = _arraylist1[6].ToString();
            if (_arraylist[8].ToString() != "")
                extddlInsuranceState.Text = _arraylist1[8].ToString();
            Page.MaintainScrollPositionOnPostBack = true;
        }

    }

    protected void extddlInsuranceCompany_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ClearInsurancecontrol();
            extddlInsuranceCode.Text = extddlInsuranceCompany.Text;
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);
           // lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress("II000000000000000733");
            DataSet dslstInsuranceCompanyAddress = new DataSet();
            dslstInsuranceCompanyAddress = (DataSet)lstInsuranceCompanyAddress.DataSource;
            lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            lstInsuranceCompanyAddress.DataValueField = "CODE";
            if (dslstInsuranceCompanyAddress.Tables[0].Rows.Count == 0)
            {
                lstInsuranceCompanyAddress.Items.Clear();
            }
            else
            {
               
                lstInsuranceCompanyAddress.DataBind();
                lstInsuranceCompanyAddress.SelectedIndex = 0;
                ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[0][0].ToString());
                txtInsuranceAddress.Text = _arraylist[2].ToString();
                txtInsuranceCity.Text = _arraylist[3].ToString();
               // txtInsuranceState.Text = _arraylist[4].ToString();
                extddlInsuranceState.Text = _arraylist[4].ToString();
                txtInsuranceZip.Text = _arraylist[5].ToString();
                txtInsuranceStreet.Text = _arraylist[6].ToString();
                if (_arraylist[8].ToString() != "")
                    extddlInsuranceState.Text = _arraylist[8].ToString();
                Page.MaintainScrollPositionOnPostBack = true;

            }

            PopupBO _objPopupBO = new PopupBO();

            Page.MaintainScrollPositionOnPostBack = true;

            if (lstInsuranceCompanyAddress.Items.Count == 1)
            {
                ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[0][0].ToString());
                txtInsuranceAddress.Text = _arraylist[2].ToString();
                txtInsuranceCity.Text = _arraylist[3].ToString();
                txtInsuranceState.Text = _arraylist[4].ToString();
                extddlInsuranceState.Text = _arraylist[4].ToString();
                txtInsuranceZip.Text = _arraylist[5].ToString();
                txtInsuranceStreet.Text = _arraylist[6].ToString();
                if (_arraylist[8].ToString() != "")
                    extddlInsuranceState.Text = _arraylist[8].ToString();
                Page.MaintainScrollPositionOnPostBack = true;


            }

            /*vivek 18 feb 2010*/
            ArrayList _arraylistnew = new ArrayList();
           _arraylistnew = _bill_Sys_PatientBO.GetDefaultDetailnew(extddlInsuranceCompany.Text);
             //_arraylistnew = _bill_Sys_PatientBO.GetDefaultDetailnew("II000000000000000733");
            if (_arraylistnew.Count == 0)
            {
                //txtInsuranceAddress.Text = "";
                //txtInsuranceCity.Text = "";
                //txtInsuranceState.Text = "";
                //txtInsuranceZip.Text = "";
                //txtInsuranceStreet.Text = "";
            }
            else
            {

                txtInsuranceAddress.Text = _arraylistnew[1].ToString();
                for (int i = 0; i < lstInsuranceCompanyAddress.Items.Count; i++)
                {


                    if (lstInsuranceCompanyAddress.Items[i].Value == _arraylistnew[0].ToString())
                    {
                        lstInsuranceCompanyAddress.SelectedIndex = i;

                        break;
                    }
                }

                txtInsuranceCity.Text = _arraylistnew[2].ToString();
                txtInsuranceZip.Text = _arraylistnew[3].ToString();
                txtInsuranceStreet.Text = _arraylistnew[4].ToString();
                txtInsuranceState.Text = _arraylistnew[6].ToString();
                extddlInsuranceState.Text = _arraylistnew[5].ToString();
                Page.MaintainScrollPositionOnPostBack = true;
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

    protected void extddlInsuranceCode_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ClearInsurancecontrol();
            extddlInsuranceCompany.Text = extddlInsuranceCode.Text;
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);
            DataSet dslstInsuranceCompanyAddress = new DataSet();
            dslstInsuranceCompanyAddress = (DataSet)lstInsuranceCompanyAddress.DataSource;
            lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            lstInsuranceCompanyAddress.DataValueField = "CODE";
            lstInsuranceCompanyAddress.DataBind();
            //
            if (dslstInsuranceCompanyAddress.Tables[0].Rows.Count == 0)
            {
                lstInsuranceCompanyAddress.Items.Clear();
            }
            else
            {

                lstInsuranceCompanyAddress.DataBind();
                lstInsuranceCompanyAddress.SelectedIndex = 0;
                ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[0][0].ToString());
                txtInsuranceAddress.Text = _arraylist[2].ToString();
                txtInsuranceCity.Text = _arraylist[3].ToString();
                // txtInsuranceState.Text = _arraylist[4].ToString();
                extddlInsuranceState.Text = _arraylist[4].ToString();
                txtInsuranceZip.Text = _arraylist[5].ToString();
                txtInsuranceStreet.Text = _arraylist[6].ToString();
                if (_arraylist[8].ToString() != "")
                    extddlInsuranceState.Text = _arraylist[8].ToString();
                Page.MaintainScrollPositionOnPostBack = true;

            }

            PopupBO _objPopupBO = new PopupBO();

            Page.MaintainScrollPositionOnPostBack = true;

            if (lstInsuranceCompanyAddress.Items.Count == 1)
            {
                ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[0][0].ToString());
                txtInsuranceAddress.Text = _arraylist[2].ToString();
                txtInsuranceCity.Text = _arraylist[3].ToString();
                txtInsuranceState.Text = _arraylist[4].ToString();
                extddlInsuranceState.Text = _arraylist[4].ToString();
                txtInsuranceZip.Text = _arraylist[5].ToString();
                txtInsuranceStreet.Text = _arraylist[6].ToString();
                if (_arraylist[8].ToString() != "")
                    extddlInsuranceState.Text = _arraylist[8].ToString();
                Page.MaintainScrollPositionOnPostBack = true;


            }

           
            ArrayList _arraylistnew = new ArrayList();
            _arraylistnew = _bill_Sys_PatientBO.GetDefaultDetailnew(extddlInsuranceCompany.Text);
            //_arraylistnew = _bill_Sys_PatientBO.GetDefaultDetailnew("II000000000000000733");
            if (_arraylistnew.Count == 0)
            {
                //txtInsuranceAddress.Text = "";
                //txtInsuranceCity.Text = "";
                //txtInsuranceState.Text = "";
                //txtInsuranceZip.Text = "";
                //txtInsuranceStreet.Text = "";
            }
            else
            {

                txtInsuranceAddress.Text = _arraylistnew[1].ToString();
                for (int i = 0; i < lstInsuranceCompanyAddress.Items.Count; i++)
                {


                    if (lstInsuranceCompanyAddress.Items[i].Value == _arraylistnew[0].ToString())
                    {
                        lstInsuranceCompanyAddress.SelectedIndex = i;

                        break;
                    }
                }

                txtInsuranceCity.Text = _arraylistnew[2].ToString();
                txtInsuranceZip.Text = _arraylistnew[3].ToString();
                txtInsuranceStreet.Text = _arraylistnew[4].ToString();
                txtInsuranceState.Text = _arraylistnew[6].ToString();
                extddlInsuranceState.Text = _arraylistnew[5].ToString();
                Page.MaintainScrollPositionOnPostBack = true;
            }

            //

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

    protected void extddlAdjuster_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ClearAdjusterControl();
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            ArrayList arr = new ArrayList();
            arr = _bill_Sys_PatientBO.GetAdjusterDetail(extddlAdjuster.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (arr.Count > 0)
            {

                if (arr.Count >= 1) { txtAdjusterPhone.Text = arr[0].ToString(); }
                if (arr.Count >= 2) { txtAdjusterExtension.Text = arr[1].ToString(); }
                if (arr.Count >= 3) { txtfax.Text = arr[2].ToString(); }
                if (arr.Count >= 4) { txtEmail.Text = arr[3].ToString(); }
            }
            Page.MaintainScrollPositionOnPostBack = true;
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

    private void ClearAdjusterControl()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            txtAdjusterExtension.Text = "";
            txtAdjusterPhone.Text = "";
            txtfax.Text = "";
            txtEmail.Text = "";
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

    protected void hlnkAssociate_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(hlnkAssociate, typeof(Button), "Msg", "window.open('Bill_Sys_AssociateDignosisCode.aspx?CaseId=" + Session["PassedCaseID"].ToString() + "','','titlebar=no,menubar=yes,resizable,alwaysRaised,scrollbars=yes,width=800,height=800,center ,top=0,left=0'); document.getElementById('ImageDiv').style.visibility = 'hidden'; ", true);
        }
        catch { }
    }

    protected void lstInsuranceCompanyAddress_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(lstInsuranceCompanyAddress.SelectedValue);
            txtInsuranceAddress.Text = _arraylist[2].ToString();
            txtInsuranceCity.Text = _arraylist[3].ToString();
            txtInsuranceState.Text = _arraylist[4].ToString();
            txtInsuranceZip.Text = _arraylist[5].ToString();
            txtInsuranceStreet.Text = _arraylist[6].ToString();
            if (_arraylist[8].ToString() != "")
                extddlInsuranceState.Text = _arraylist[8].ToString();
            Page.MaintainScrollPositionOnPostBack = true;


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

    private void ClearInsurancecontrol()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            txtInsuranceAddressCode.Text = "";
            txtInsuranceCityCode.Text = "";
            txtInsuranceState.Text = "";
            txtInsuranceZipCode.Text = "";
            txtInsuranceStreetCode.Text = "";
            txtInsuranceStateNewCode.Text = "";
            extddlStateCode.Text = "";
            txtInsuranceState.Text = "";
            extddlInsuranceState.Text = "";
            IDDefault.Checked = false;
           
       
         

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

    protected void btnAddAttorney_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            PopupBO _objPopupBO = new PopupBO();
            ArrayList _objAL = new ArrayList();
            _objAL.Add("");
            _objAL.Add(txtAttorneyFirstName.Text);
            _objAL.Add(txtAttorneyLastName.Text);
            _objAL.Add(txtAttorneyCity.Text);
            _objAL.Add(txtAttorneyState.Text);
            _objAL.Add(txtAttorneyZip.Text);
            _objAL.Add(txtAttorneyPhoneNo.Text);
            _objAL.Add(txtAttorneyFax.Text);
            _objAL.Add(txtAttorneyEmailID.Text);
            _objAL.Add(txtCompanyID.Text);
            _objAL.Add(extddlAttorneyState.Text);
            _objPopupBO.saveAttorney(_objAL);

            // Session["NEW_ATTORNEY_ID"] = _objPopupBO.getLatestID("SP_MST_ATTORNEY", txtCompanyID.Text);
            extddlAttorney.Text = _objPopupBO.getLatestID("SP_MST_ATTORNEY", txtCompanyID.Text);
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

    protected void btnSaveAdjuster_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            PopupBO _objPopupBO = new PopupBO();
            ArrayList _objAL = new ArrayList();
            _objAL.Add("");
            _objAL.Add(txtAdjusterPopupName.Text);
            _objAL.Add(txtAdjusterPopupPhone.Text);
            _objAL.Add(txtAdjusterPopupExtension.Text);
            _objAL.Add(txtAdjusterPopupFax.Text);
            _objAL.Add(txtAdjusterPopupEmail.Text);
            _objAL.Add(txtCompanyID.Text);
            _objPopupBO.saveAdjuster(_objAL);
            // Session["NEW_ADJUSTER_ID"] = _objPopupBO.getLatestID("SP_MST_ADJUSTER", txtCompanyID.Text);
            extddlAdjuster.Text = _objPopupBO.getLatestID("SP_MST_ADJUSTER", txtCompanyID.Text);
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

    protected void btnSaveInsurance_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            PopupBO _objPopupBO = new PopupBO();
            ArrayList _objAL = new ArrayList();
            _objAL.Add(txtInsuranceCompanyName.Text);
            _objAL.Add(txtInsurancePhoneNumber.Text);
            _objAL.Add(txtInsuranceEmail.Text);
            _objAL.Add(txtCompanyID.Text);
            _objAL.Add(txtInsCode.Text);
            _objPopupBO.saveInsurance(_objAL);
            // Session["NEW_ADJUSTER_ID"] = _objPopupBO.getLatestID("SP_MST_ADJUSTER", txtCompanyID.Text);
            extddlInsuranceCompany.Text = _objPopupBO.getLatestID("SP_MST_INSURANCE_COMPANY", txtCompanyID.Text);
            extddlInsuranceCode.Text = extddlInsuranceCompany.Text;
            _objAL = new ArrayList();
            _objAL.Add(extddlInsuranceCompany.Text);
            _objAL.Add(txtInsuranceAddressNew.Text);
            _objAL.Add(txtInsuranceStreetNew.Text);
            _objAL.Add(txtInsuranceCityNew.Text);
            _objAL.Add(txtInsuranceStateNew.Text);
            _objAL.Add(txtInsuranceZipNew.Text);
            _objAL.Add(txtCompanyID.Text);
            _objAL.Add(extddlInsuranceStateNew.Text);
            _objPopupBO.saveInsuranceAddress(_objAL);
            ClearInsurancecontrol();

            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);
            lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            lstInsuranceCompanyAddress.DataValueField = "CODE";
            lstInsuranceCompanyAddress.DataBind();
            Page.MaintainScrollPositionOnPostBack = true;
            lstInsuranceCompanyAddress.SelectedValue = _objPopupBO.getLatestID("SP_MST_INSURANCE_ADDRESS", txtCompanyID.Text);

            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();

            ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(lstInsuranceCompanyAddress.SelectedValue);
            txtInsuranceAddress.Text = _arraylist[2].ToString();
            txtInsuranceCity.Text = _arraylist[3].ToString();
            txtInsuranceState.Text = _arraylist[4].ToString();
            txtInsuranceZip.Text = _arraylist[5].ToString();
            txtInsuranceStreet.Text = _arraylist[6].ToString();
            extddlInsuranceStateNew.Text = _arraylist[8].ToString();
            Page.MaintainScrollPositionOnPostBack = true;


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

    private void ClearAddressDetails()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            txtInsuranceAddress.Text = "";
            txtInsuranceStreet.Text = "";
            txtInsuranceCity.Text = "";
            txtInsuranceState.Text = "";
            txtInsuranceZip.Text = "";

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

    protected void btnClearAddress_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ClearAddressDetails();
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
    {//Logging Start
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
            _saveOperation.Xml_File = "PatientDataEntry.xml";
            _saveOperation.SaveMethod();

            //_saveOperation.WebPage = this.Page;
            //_saveOperation.Xml_File = "Patient.xml";
            //_saveOperation.SaveMethod();

            //txtPatientID.Text = manageNotes.GetPatientLatestID();


            //_saveOperation.WebPage = this.Page;
            //_saveOperation.Xml_File = "PatientAccidentInfo.xml";
            //_saveOperation.SaveMethod();

            //_saveOperation.WebPage = this.Page;
            //_saveOperation.Xml_File = "PatientCaseMaster.xml";
            //_saveOperation.SaveMethod();

           // BindGrid();
            //ClearControl();
            lblMsg.Visible = true;
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


            //hlnkAssociate.Visible = true;
            hlnkShowNotes.Visible = true;
            PopEx.Enabled = true;
            pnlShowNotes.Visible = true;
            lblMsg.Text = " Patient Information Saved successfully ! ";
            Page.MaintainScrollPositionOnPostBack = false;
            //Response.Redirect("Bill_Sys_SearchCase.aspx?type=home", false);

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
    {//Logging Start
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
                Response.Redirect(" AJAX Pages/Bill_Sys_ReCaseDetails.aspx", false);
            }
            else
            {
                Response.Redirect("AJAX Pages/Bill_Sys_CaseDetails.aspx", false);
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


    protected void btnSaveAddress_Click(object sender, EventArgs e)
    {

        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            _saveOperation = new SaveOperation();
            try
            {
                txtInsuranceCompanyID.Text = extddlInsuranceCompany.Text;
                ArrayList _objAL = new ArrayList();
                PopupBO _objPopupBO = new PopupBO();
                _objAL.Add(txtInsuranceCompanyID.Text);
                _objAL.Add(txtInsuranceAddressCode.Text);
                _objAL.Add(txtInsuranceStreetCode.Text);
                _objAL.Add(txtInsuranceCityCode.Text);
                _objAL.Add(extddlStateCode.Text);
                _objAL.Add(txtInsuranceZipCode.Text);
                _objAL.Add(txtCompanyID.Text);
                _objAL.Add(extddlInsuranceStateNew.Text);
                if (IDDefault.Checked)
                {
                    _objAL.Add("1");

                }
                else
                {
                    _objAL.Add("0");
                }
                _objPopupBO.saveInsuranceAddressNew(_objAL);
                ClearInsurancecontrol();
                _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);

                lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                lstInsuranceCompanyAddress.DataValueField = "CODE";
                lstInsuranceCompanyAddress.DataBind();
                Page.MaintainScrollPositionOnPostBack = true;
                lstInsuranceCompanyAddress.SelectedValue = _objPopupBO.getLatestID("SP_MST_INSURANCE_ADDRESS", txtCompanyID.Text);
                _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(lstInsuranceCompanyAddress.SelectedValue);
                txtInsuranceAddress.Text = _arraylist[2].ToString();
                txtInsuranceCity.Text = _arraylist[3].ToString();
                // txtInsuranceState.Text = _arraylist[4].ToString();
                extddlInsuranceState.Text = _arraylist[8].ToString();
                txtInsuranceZip.Text = _arraylist[5].ToString();
                txtInsuranceStreet.Text = _arraylist[6].ToString();
                Page.MaintainScrollPositionOnPostBack = true;

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

    protected void lstInsuranceCompanyAddress_Load(object sender, EventArgs e)
    {


    }
    protected void chkAutoIncr_CheckedChanged(object sender, EventArgs e)
    {
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
    {//Logging Start
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

}

