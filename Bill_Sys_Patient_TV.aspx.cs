/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_Patient_TV.aspx.cs
/*Purpose              :       To Add and Edit Patient in Tabbed View.
/*Author               :       Sandeep Y
/*Date of creation     :       23 May 2008  
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

public partial class Bill_Sys_Patient_TV : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    Billing_Sys_ManageNotesBO manageNotes;
    Bill_Sys_CaseObject _objCaseObject;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
        //    imgbtnDateofBirth.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateOfBirth,'imgbtnDateofBirth','MM/dd/yyyy'); return false;");
        //    imgbtnDateofInjury.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateOfInjury,'imgbtnDateofInjury','MM/dd/yyyy'); return false;");
        //    imgbtnDateofAccident.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateofAccident,'imgbtnDateofAccident','MM/dd/yyyy'); return false;");

            //btnSave.Attributes.Add("onclick", "return formValidator('frmPatient','txtPatientFName,txtPatientLName,txtPatientAge,txtCaseName,extddlCaseType,extddlProvider,extddlInsuranceCompany,extddlCaseStatus,extddlAttorney,extddlAdjuster,txtClaimNumber,txtPolicyNumber,txtDateofAccident');");
            //btnUpdate.Attributes.Add("onclick", "return formValidator('frmPatient','txtPatientFName,txtPatientLName,txtPatientAge,txtCaseName,extddlCaseType,extddlProvider,extddlInsuranceCompany,extddlCaseStatus,extddlAttorney,extddlAdjuster,txtClaimNumber,txtPolicyNumber,txtDateofAccident');");
            btnSave.Attributes.Add("onclick", "return formValidator('frmPatient','tabcontainerPatientEntry:tabpnlPersonalInformation:txtPatientFName,tabcontainerPatientEntry:tabpnlPersonalInformation:txtPatientLName,tabcontainerPatientEntry:tabpnlPersonalInformation:extddlCaseType,tabcontainerPatientEntry:tabpnlPersonalInformation:extddlCaseStatus');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmPatient','tabcontainerPatientEntry:tabpnlPersonalInformation:txtPatientFName,tabcontainerPatientEntry:tabpnlPersonalInformation:txtPatientLName,tabcontainerPatientEntry:tabpnlPersonalInformation:extddlCaseType,tabcontainerPatientEntry:tabpnlPersonalInformation:extddlCaseStatus');");
            txtPatientEmail.Attributes.Add("onfocusout", "return isEmail('frmPatient','txtPatientEmail');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
            extddlProvider.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();
            extddlAttorney.Flag_ID = txtCompanyID.Text.ToString();           
            extddlAdjuster.Flag_ID = txtCompanyID.Text.ToString();
            extddlInsuranceCompany.Flag_ID = txtCompanyID.Text.ToString();

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
                    BindGrid();
                    btnUpdate.Enabled = false;
                }
            }
            if (Session["CASETYPE"] != null)
            {
                CaseDetailsBO _objCD = new CaseDetailsBO();
                extddlCaseType.Text = Session["CASETYPE"].ToString();
                if (_objCD.GetAbbrevationID(Session["CASETYPE"].ToString()) == "WC000000000000000002")
                {
                    txtAccidentAddress.Enabled = false;
                    txtAccidentCity.Enabled = false;
                    txtAccidentState.Enabled = false;
                    txtPlatenumber.Enabled = false;
                    txtDateofAccident.Enabled = false;
                    txtPolicyReport.Enabled = false;
                    txtListOfPatient.Enabled = false;
                    imgbtnDateofAccident.Enabled = false;

                }
                
                extddlCaseStatus.Text = GetOpenCaseStatus();// "CS000000000000000001";
                extddlCaseStatus.Enabled = false;
                //if (extddlCaseType.Text == "CT000000000000000001" ) { lblWcbNumber.Text = "WCB Number"; } else if (extddlCaseType.Text == "CT000000000000000002") { lblWcbNumber.Text = "Policy Number"; }
                if (extddlCaseType.Text == GetWCCaseType()) { lblWcbNumber.Text = "WCB Number"; } else if (extddlCaseType.Text == GetNFCaseType()) { lblWcbNumber.Text = "Policy Number"; }
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
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_Patient_TV.aspx");
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();

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
            manageNotes = new Billing_Sys_ManageNotesBO();
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "Patient_TV.xml";
           //     _saveOperation.SaveMethod();

                

                

                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "PatientCaseMaster.xml";
             //   _saveOperation.SaveMethod();

                SavePatientInformation();

                BindGrid();
                ClearControl();
                lblMsg.Visible = true;
                _bill_Sys_PatientBO=new Bill_Sys_PatientBO();
                Session["PassedCaseID"] = _bill_Sys_PatientBO.GetLatestPatientCaseID(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                _objCaseObject = new Bill_Sys_CaseObject();
                _objCaseObject.SZ_CASE_ID = Session["PassedCaseID"].ToString();
                Session["CASE_OBJECT"] = _objCaseObject;

                hlnkAssociate.Visible = true;
                
                lblMsg.Text = " Patient Information Saved successfully ! ";
                Page.MaintainScrollPositionOnPostBack = false;
                Response.Write("<script>window.opener.location.replace('Bill_Sys_CaseMaster.aspx')</script>");

                hlnkShowNotes.Visible = true;
                pnlShowNotes.Visible = true;
                PopEx.Enabled = true;
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

    protected void SavePatientInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        manageNotes = new Billing_Sys_ManageNotesBO();
        try
        {
            ArrayList _objAL = new ArrayList();
            _objAL.Add(txtPatientFName.Text);
            _objAL.Add(txtPatientLName.Text);
            _objAL.Add(txtPatientAge.Text);
            _objAL.Add(txtPatientAddress.Text);
            _objAL.Add(txtPatientStreet.Text);
            _objAL.Add(txtPatientCity.Text);
            _objAL.Add(txtPatientZip.Text);
            _objAL.Add(txtPatientPhone.Text);
            _objAL.Add(txtPatientEmail.Text);
            _objAL.Add(txtCompanyID.Text);
            _objAL.Add(txtWorkPhone.Text);
            _objAL.Add(txtExtension.Text);
            _objAL.Add(txtMI.Text);
            _objAL.Add(txtWCBNumber.Text);
            _objAL.Add(txtSocialSecurityNumber.Text);
            _objAL.Add(txtDateOfBirth.Text);
            _objAL.Add(ddlSex.SelectedValue);
            _objAL.Add(txtDateOfInjury.Text);
            _objAL.Add(txtJobTitle.Text);
            _objAL.Add(txtWorkActivites.Text);
            _objAL.Add(txtState.Text);
            _objAL.Add(txtCarrierCaseNo.Text);
            _objAL.Add(txtEmployerName.Text);
            _objAL.Add(txtEmployerPhone.Text);
            _objAL.Add(txtEmployerAddress.Text);
            _objAL.Add(txtEmployerCity.Text);
            _objAL.Add(txtEmployerState.Text);
            _objAL.Add(txtEmployerZip.Text);
            _objAL.Add("ADD");
             _objAL.Add("");
             if (chkTransportation.Checked) { _objAL.Add("True"); } else { _objAL.Add("False"); }
             if (chkWrongPhone.Checked) { _objAL.Add("True"); } else { _objAL.Add("False"); }
            Patient_TVBO objPatientBO = new Patient_TVBO();
            objPatientBO.savePatientInformation(_objAL);

            txtPatientID.Text = manageNotes.GetPatientLatestID();

            _objAL = new ArrayList();
            _objAL.Add("");
            _objAL.Add(txtPatientID.Text);
            _objAL.Add(txtPlatenumber.Text);
            _objAL.Add(txtDateofAccident.Text);
            _objAL.Add(txtAccidentAddress.Text);
            _objAL.Add(txtAccidentCity.Text);        
            _objAL.Add(txtAccidentState.Text);
            _objAL.Add(txtPolicyReport.Text);
            _objAL.Add(txtListOfPatient.Text);

            _objAL.Add(txtCompanyID.Text);
            _objAL.Add("ADD");
            objPatientBO.savePatientAccidentInformation(_objAL);



            _objAL = new ArrayList();
            _objAL.Add("");
            _objAL.Add("");
            _objAL.Add(extddlCaseType.Text);
            _objAL.Add(extddlInsuranceCompany.Text);
            _objAL.Add(extddlCaseStatus.Text);
            _objAL.Add(extddlAttorney.Text);
            _objAL.Add(txtPatientID.Text);
            _objAL.Add(txtCompanyID.Text);
            _objAL.Add(txtClaimNumber.Text);
            _objAL.Add(txtPolicyNumber.Text);
            _objAL.Add(txtDateofAccident.Text);
            _objAL.Add(extddlAdjuster.Text);
            _objAL.Add(txtAssociateDiagnosisCode.Text);
            _objAL.Add(lstInsuranceCompanyAddress.Text);
            _objAL.Add("ADD");

            objPatientBO = new Patient_TVBO();
            objPatientBO.saveCaseInformation(_objAL);

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

    protected void UpdatePatientInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        manageNotes = new Billing_Sys_ManageNotesBO();
        try
        {
            txtPatientID.Text = manageNotes.GetPatientLatestID();

            ArrayList _objAL = new ArrayList();
            _objAL.Add(txtPatientFName.Text);
            _objAL.Add(txtPatientLName.Text);
            _objAL.Add(txtPatientAge.Text);
            _objAL.Add(txtPatientAddress.Text);
            _objAL.Add(txtPatientStreet.Text);
            _objAL.Add(txtPatientCity.Text);
            _objAL.Add(txtPatientZip.Text);
            _objAL.Add(txtPatientPhone.Text);
            _objAL.Add(txtPatientEmail.Text);
            _objAL.Add(txtCompanyID.Text);
            _objAL.Add(txtWorkPhone.Text);
            _objAL.Add(txtExtension.Text);
            _objAL.Add(txtMI.Text);
            _objAL.Add(txtWCBNumber.Text);
            _objAL.Add(txtSocialSecurityNumber.Text);
            _objAL.Add(txtDateOfBirth.Text);
            _objAL.Add(ddlSex.SelectedValue);
            _objAL.Add(txtDateOfInjury.Text);
            _objAL.Add(txtJobTitle.Text);
            _objAL.Add(txtWorkActivites.Text);
            _objAL.Add(txtState.Text);
            _objAL.Add(txtCarrierCaseNo.Text);
            _objAL.Add(txtEmployerName.Text);
            _objAL.Add(txtEmployerPhone.Text);
            _objAL.Add(txtEmployerAddress.Text);
            _objAL.Add(txtEmployerCity.Text);
            _objAL.Add(txtEmployerState.Text);
            _objAL.Add(txtEmployerZip.Text);
            _objAL.Add("UPDATE");
            _objAL.Add(Session["PatientID"].ToString());
            if (chkTransportation.Checked) { _objAL.Add("True"); } else { _objAL.Add("False"); }
            if (chkWrongPhone.Checked) { _objAL.Add("True"); } else { _objAL.Add("False"); }

            Patient_TVBO objPatientBO = new Patient_TVBO();
            objPatientBO.savePatientInformation(_objAL);

            _objAL = new ArrayList();
            _objAL.Add(txtAccidentID.Text);
            _objAL.Add(txtPatientID.Text);
            _objAL.Add(txtPlatenumber.Text);
            _objAL.Add(txtDateofAccident.Text);
            _objAL.Add(txtAccidentAddress.Text);
            _objAL.Add(txtAccidentCity.Text);            
            _objAL.Add(txtAccidentState.Text);
            _objAL.Add(txtPolicyReport.Text);
            _objAL.Add(txtListOfPatient.Text);
            
            _objAL.Add(txtCompanyID.Text);
            _objAL.Add("UPDATE");
            objPatientBO.savePatientAccidentInformation(_objAL);


            _objAL = new ArrayList();
            _objAL.Add(txtCaseID.Text);
            _objAL.Add("");
            _objAL.Add(extddlCaseType.Text);
            _objAL.Add(extddlInsuranceCompany.Text);
            _objAL.Add(extddlCaseStatus.Text);
            _objAL.Add(extddlAttorney.Text);
            _objAL.Add(txtPatientID.Text);
            _objAL.Add(txtCompanyID.Text);
            _objAL.Add(txtClaimNumber.Text);
            _objAL.Add(txtPolicyNumber.Text);
            _objAL.Add(txtDateofAccident.Text);
            _objAL.Add(extddlAdjuster.Text);
            _objAL.Add(txtAssociateDiagnosisCode.Text);
            _objAL.Add(lstInsuranceCompanyAddress.Text);
            _objAL.Add("UPDATE");

            objPatientBO = new Patient_TVBO();
            objPatientBO.saveCaseInformation(_objAL);

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
         //   _editOperation.UpdateMethod();

            
            _editOperation.Primary_Value = txtCaseID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "PatientCaseMaster.xml";
        //    _editOperation.UpdateMethod();

            UpdatePatientInformation();
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdPatientList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["PatientID"] = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[1].Text;
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[2].Text != "&nbsp;") { txtPatientFName.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[2].Text; }
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[3].Text != "&nbsp;") {txtPatientLName.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[3].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[4].Text != "&nbsp;") {txtPatientAge.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[4].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[5].Text != "&nbsp;") {txtPatientAddress.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[5].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[6].Text != "&nbsp;") {txtPatientStreet.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[6].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[7].Text != "&nbsp;") {txtPatientCity.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[7].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[8].Text != "&nbsp;") {txtPatientZip.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[8].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[9].Text != "&nbsp;") {txtPatientPhone.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[9].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[10].Text != "&nbsp;") {txtPatientEmail.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[10].Text;}

            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[11].Text != "&nbsp;") {txtMI.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[11].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[12].Text != "&nbsp;") {txtWCBNumber.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[12].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[13].Text != "&nbsp;") {txtSocialSecurityNumber.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[13].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[15].Text != "&nbsp;") {txtDateOfBirth.Text =Convert.ToDateTime( grdPatientList.Items[grdPatientList.SelectedIndex].Cells[15].Text).ToShortDateString();}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[16].Text != "&nbsp;") {ddlSex.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[16].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[17].Text != "&nbsp;") {txtDateOfInjury.Text = Convert.ToDateTime(grdPatientList.Items[grdPatientList.SelectedIndex].Cells[17].Text).ToShortDateString();}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[18].Text != "&nbsp;") {txtJobTitle.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[18].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[19].Text != "&nbsp;") {txtWorkActivites.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[19].Text;}
            if (grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text != "&nbsp;") {txtState.Text = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[20].Text;}
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
            DataSet _ds =  manageNotes.GetCaseDetails(Session["PatientID"].ToString());
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
                if (_ds.Tables[0].Rows[0].ItemArray.GetValue(9).ToString() != "&nbsp;" && _ds.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()!="") { txtDateofAccident.Text = Convert.ToDateTime(_ds.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()).ToShortDateString(); }
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
                hlnkInschedule.Visible = true;
                hlnkOutschedule.Visible = true;
                PopEx.Enabled = true;
                _objCaseObject = new Bill_Sys_CaseObject();
                _objCaseObject.SZ_CASE_ID = txtCaseID.Text;
                _objCaseObject.SZ_PATIENT_ID = grdPatientList.Items[grdPatientList.SelectedIndex].Cells[1].Text;
                _objCaseObject.SZ_PATIENT_NAME = txtPatientFName.Text + " " + txtPatientLName.Text;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdPatientList_DeleteCommand(object source, DataGridCommandEventArgs e)
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdPatientList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void extddlInsuranceCompany_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ClearInsurancecontrol();
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);
            lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            lstInsuranceCompanyAddress.DataValueField = "CODE";
            lstInsuranceCompanyAddress.DataBind();
            Page.MaintainScrollPositionOnPostBack = true;
            tabcontainerPatientEntry.ActiveTabIndex = 2;
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

    protected void extddlAdjuster_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            arr= _bill_Sys_PatientBO.GetAdjusterDetail(extddlAdjuster.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (arr.Count > 0)
            {
                
                txtAdjusterPhone.Text = arr[0].ToString();
                txtAdjusterExtension.Text = arr[1].ToString();
                txtfax.Text = arr[2].ToString();
                txtEmail.Text = arr[3].ToString();
            }
            Page.MaintainScrollPositionOnPostBack = true;
            tabcontainerPatientEntry.ActiveTabIndex = 4;
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

    private void ClearAdjusterControl()
    {
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void hlnkAssociate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ScriptManager.RegisterClientScriptBlock(hlnkAssociate, typeof(Button), "Msg", "window.open('Bill_Sys_AssociateDignosisCode.aspx?CaseId=" + Session["PassedCaseID"].ToString() + "','','titlebar=no,menubar=yes,resizable,alwaysRaised,scrollbars=yes,width=800,height=800,center ,top=0,left=0'); document.getElementById('ImageDiv').style.visibility = 'hidden'; ", true);
        }
        catch(Exception ex)
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

    protected void lstInsuranceCompanyAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
          ArrayList _arraylist =  _bill_Sys_PatientBO.GetInsuranceAddressDetail(lstInsuranceCompanyAddress.SelectedValue);
          txtInsuranceAddress.Text = _arraylist[2].ToString();         
            txtInsuranceCity.Text = _arraylist[3].ToString();
            txtInsuranceState.Text = _arraylist[4].ToString();
            txtInsuranceZip.Text = _arraylist[5].ToString();
            txtInsuranceStreet.Text = _arraylist[6].ToString();
            Page.MaintainScrollPositionOnPostBack = true;
            tabcontainerPatientEntry.ActiveTabIndex = 2;
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

    private void ClearInsurancecontrol()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtInsuranceAddress.Text = "";
            txtInsuranceCity.Text = "";
            txtInsuranceState.Text = "";
            txtInsuranceZip.Text = "";
            txtInsuranceStreet.Text = "";

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


    private string GetOpenCaseStatus()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
        catch(Exception ex)
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
        return caseStatusID;
    }


    private string GetWCCaseType()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
        catch(Exception ex)
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
    
        return caseStatusID;
    }

    private string GetNFCaseType()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
        catch(Exception ex)
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
        return caseStatusID;
    }
}

