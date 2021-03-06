/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_CaseDetails.aspx.cs
/*Purpose              :       associate case
/*Author               :       Manoj c
/*Date of creation     :       17 Dec 2008  
/*Modified By          :       prashant z
/*Modified Date        :       3 may 2010
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
using NOTES_OBJECT;
using NotesComponent;
using System.IO;
using System.Text.RegularExpressions;
using log4net;
public partial class Bill_Sys_ReCaseDetails : PageBase
{

    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Billing_Sys_ManageNotesBO _manageNotesBO;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private ArrayList _arrayList;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    private NotesOperation _notesOperation;
    public string caseID = "";

    private string associatecaseno = "";
    private string associtecasenoAllow = ""; // concanate allow case
    private string associatecasenoNotAllow = "";// concanate caseno for  different address
    private string associatecasenoUpdate = ""; //only update sourcepath but all destinati path case same need
    private string associatecasenoNull = ""; // concanate  all source and destination null
    Boolean updateFlag = false;
    Regex commonrange = new Regex("[^0-9)]");

    private static ILog log = LogManager.GetLogger("Bill_Sys_Casedetails");

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try   
        {
            ajAutoIns.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            Session["CASE_LIST_GO_BUTTON"] = null;
            bool bt_referring_facility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;

            //if ((bt_referring_facility == true) || (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
            //{
            //    lblViewLocation.Visible = false;
            //    lblLocation.Visible = false;
            //    lblLocationddl.Visible = false;
            //    extddlLocation.Visible = false;
            //}
            //else
            //{
            //    lblViewLocation.Visible = true;
            //    lblLocation.Visible = true;
            //    lblLocationddl.Visible = true;
            //    extddlLocation.Visible = true;
            //}


            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlLocation.Flag_ID = txtCompanyID.Text.ToString();

            btnSaveAdjuster.Attributes.Add("onclick", "return PopupformValidator('aspnetForm','txtAdjusterPopupName','AdjusterErrordiv');");
            lnkGenerateNF2.Attributes.Add("onclick", "return ShowGenerateNF2Link();");
            PopupBO _popupBO = new PopupBO();
            


            txtCompanyIDForNotes.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            btnPatientUpdate.Attributes.Add("onclick", "return formValidator('frmPatient','tabcontainerPatientEntry:tabpnlPersonalInformation:txtPatientFName,tabcontainerPatientEntry:tabpnlPersonalInformation:txtPatientLName,tabcontainerPatientEntry:tabpnlPersonalInformation:extddlCaseType,tabcontainerPatientEntry:tabpnlPersonalInformation:extddlCaseStatus');");
            //  imgbtnAccidentDate.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateofAccident,'imgbtnAccidentDate','MM/dd/yyyy'); return false;");
            if (!IsPostBack)
            {
                //  Session["CONTROL_OLD_VALUES"] = null;
                if (Request.QueryString["CaseID"] != null)
                {

                    //caseID = Request.QueryString["CaseID"];
                    if (Request.QueryString["CaseID"].ToString() != "")
                    {
                        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                        Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                        _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(Request.QueryString["CaseID"].ToString(), "");
                        _bill_Sys_CaseObject.SZ_CASE_ID = Request.QueryString["CaseID"].ToString();
                        if (Request.QueryString["cmp"] != null)
                        {
                            _bill_Sys_CaseObject.SZ_COMAPNY_ID = Request.QueryString["cmp"].ToString();
                            _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(Request.QueryString["CaseID"].ToString(), Request.QueryString["cmp"].ToString());
                            Session["Company"] = Request.QueryString["cmp"].ToString();
                        }
                        else
                        {
                            _bill_Sys_CaseObject.SZ_COMAPNY_ID = Session["Company"].ToString();
                            _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(Request.QueryString["CaseID"].ToString(), Session["Company"].ToString());
                        }

                       
                        _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                        Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                    }

                }
                if (Session["CASE_OBJECT"] != null)
                {
                    txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    Session["Company"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    Response.Redirect("Bill_Sys_SearchCase.aspx", false);
                }

                LoadNoteGrid();
                if (Request.QueryString["CaseID"] != null)
                {
                    // caseID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    caseID = Request.QueryString["CaseID"].ToString();
                    txtCaseID.Text = caseID;
                    ShowPopupNotes(caseID);
                }  
               


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

                CheckTemplateStatus(txtCaseID.Text);
                LoadDataOnPage();

                GetPatientDeskList();
                // Current Company ID
                String strCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                // Patient Company ID
                String strPatientCompanyID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                EnableDisableControl(!strCompanyID.Equals(strPatientCompanyID));
            }
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
            {
                Label lblView = (Label)DtlView.Items[0].FindControl("lblView");
                Label lblViewChartNo = (Label)DtlView.Items[0].FindControl("lblViewChartNo");
                lblChart.Visible = true;
                txtChartNo.Visible = true;
                lblViewChartNo.Visible = true;
                lblView.Visible = true;
            }
            else
            {
                Label lblView = (Label)DtlView.Items[0].FindControl("lblView");
                Label lblViewChartNo = (Label)DtlView.Items[0].FindControl("lblViewChartNo");
                lblChart.Visible = false;
                txtChartNo.Visible = false;
                lblViewChartNo.Visible = false;
                lblView.Visible = false;
            }
            txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            txtNoteCode.Text = Note_Code.New_Note_Added;

            #region "Get list of Associate Cases"
            GetAssociateCases();
            #endregion

            #region "Get list of Associate Cases"
            //GetRelatedData();
            #endregion

            if ((bt_referring_facility == true) || (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
            {
                Label lblVLocation12 = (Label)DtlView.Items[0].FindControl("lblVLocation1");
                Label lblLocation = (Label)DtlView.Items[0].FindControl("lblLocation");

                lblVLocation12.Visible = false;
                lblLocation.Visible = false;
                lblLocationddl.Visible = false;
                extddlLocation.Visible = false;
            }
            else
            {
                Label lblVLocation13 = (Label)DtlView.Items[0].FindControl("lblVLocation1");
                Label lblLocation = (Label)DtlView.Items[0].FindControl("lblLocation");
                lblVLocation13.Visible = true;
                lblLocation.Visible = true;
                lblLocationddl.Visible = true;
                extddlLocation.Visible = true;
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
            cv.MakeReadOnlyPage("Bill_Sys_ReCaseDetails.aspx");
        }
        #endregion
        if (btnPatientUpdate.Visible == true)
        {
            btnAssociate.Visible = true;
            btassociate.Visible = true;
            btnDAssociate.Visible = true;
            txtAssociateCases.Visible  = true;
            lblassociate.Visible = true;
        }
        else
        {
            btnAssociate.Visible = false;
            btassociate.Visible = false;
            btnDAssociate.Visible = false;
            txtAssociateCases.Visible  = false;
            lblassociate.Visible = false;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
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
            //extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
            //extddlProvider.Flag_ID = txtCompanyID.Text.ToString();
            //extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();
            //extddlAttorney.Flag_ID = txtCompanyID.Text.ToString();
            //extddlAdjuster.Flag_ID = txtCompanyID.Text.ToString();
            //extddlInsuranceCompany.Flag_ID = txtCompanyID.Text.ToString();


            extddlCaseType.Flag_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            extddlProvider.Flag_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            extddlCaseStatus.Flag_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            extddlAttorney.Flag_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            extddlAdjuster.Flag_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            extddlInsuranceCompany.Flag_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;


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




                Session["AttornyID"] = extddlAttorney.Text; // Girish
                BindSuppliesGrid();
                //lblSaveCaseName.Text = txtCaseName.Text;

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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

    #region "Bill Event"
    protected void lnkAddbills_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            Session["PassedCaseID"] = txtCaseID.Text;
            Response.Redirect("Bill_Sys_BillTransaction.aspx", false);
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

    protected void lnkViewBills_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            Session["PassedCaseID"] = txtCaseID.Text;
            Response.Redirect("Bill_Sys_BillSearch.aspx", false);
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

    protected void lnkPaidBills_Click(object sender, EventArgs e)
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

    protected void lnkUnpaidBills_Click(object sender, EventArgs e)
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
    #endregion

    #region "Document Manager Link"

    protected void lnkDocumentManager_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
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
            szURL = "Document Manager/case/vb_CaseInformation.aspx";
            Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
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

    #endregion

    #region "Img Button Event"
    //protected void CaseNameLinkImg_Click(object sender, ImageClickEventArgs e)
    //{

    //  //  Response.Write(CaseNameLinkImg.CommandName);
    //    if (CaseNameLinkImg.CommandName.ToString() == "save")
    //    {
    //        UpdateData();
    //        CaseNameLinkImg.CommandName = "edit";
    //        CaseNameLinkImg.ImageUrl = "~/Images/actionEdit.gif";
    //        txtCaseName.Visible = false;
    //        lblSaveCaseName.Visible = true;

    //    }
    //    else
    //    {
    //        CaseNameLinkImg.CommandName = "save";
    //        CaseNameLinkImg.ImageUrl = "~/Images/saveicon.gif";
    //        lblSaveCaseName.Visible = false;
    //        txtCaseName.Visible = true;
    //        lblMsg.Visible = false;
    //    }
    //}

    #endregion

    protected void lnkNotes_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["SZ_CASE_ID_NOTES"] = txtCaseID.Text;
            Response.Redirect("Bill_Sys_Notes.aspx", false);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        try
        {
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "notes.xml";
            _saveOperation.SaveMethod();
            ClearControl();
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
            LoadNoteGrid();
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


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadNoteGrid();
    }



    protected void lnkTemplateManager_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["TM_SZ_CASE_ID"] = txtCaseID.Text;
            String szURL = "";
            szURL = "TemplateManager/templates.aspx";
            Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");
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
    protected void lnkManageNotes_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["Manage_Case_ID"] = txtCaseID.Text;
            Response.Redirect("Bill_Sys_ManageNotes.aspx", false);
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
            // ScriptManager.RegisterClientScriptBlock(hlnkAssociate, typeof(Button), "Msg", "window.open('Bill_Sys_AssociateDignosisCode.aspx?CaseId=" + Session["PassedCaseID"].ToString() + "','_self'); document.getElementById('ImageDiv').style.visibility = 'hidden'; ", true);
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

    protected void hlnkPatientDesk_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            // ScriptManager.RegisterClientScriptBlock(hlnkAssociate, typeof(Button), "Msg", "window.open('Bill_SysPatientDesk.aspx?CaseId=" + Session["PassedCaseID"].ToString() + "','_self'); document.getElementById('ImageDiv').style.visibility = 'hidden'; ", true);
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



    //****************//

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
    }  //Not use

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

            DataSet _adjusterDs = _bill_Sys_PatientBO.GetAdjusterDetail(extddlAdjuster.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "FORTEST");

            if (_adjusterDs.Tables[0].Rows.Count > 0)
            {
                txtAdjusterPhone.Text = _adjusterDs.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                txtAdjusterExtension.Text = _adjusterDs.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                txtfax.Text = _adjusterDs.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                txtEmail.Text = _adjusterDs.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            }


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
            ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(lstInsuranceCompanyAddress.SelectedValue);
            txtInsuranceAddress.Text = _arraylist[2].ToString();
            txtInsuranceCity.Text = _arraylist[3].ToString();
            txtInsuranceState.Text = _arraylist[4].ToString();
            txtInsuranceZip.Text = _arraylist[5].ToString();
            txtInsuranceStreet.Text = _arraylist[6].ToString();
            txtInsFax.Text = _arraylist[9].ToString();
            txtInsPhone.Text = _arraylist[10].ToString();
            txtInsContactPerson.Text = _arraylist[11].ToString();

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


    private void GetPatientDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            DataSet _patientDs = _bill_Sys_PatientBO.GetPatientInfo(txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            if (_patientDs.Tables[0].Rows.Count > 0)
            {
                DtlView.DataSource = _patientDs ;
                DtlView.DataBind();

                if (_patientDs.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;")
                {
                    txtPatientFName.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                    //lblViewFirstName.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                }
                txtPatientLName.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                //lblViewLastName.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                txtPatientAge.Text = _patientDs.Tables[0].Rows[0]["I_PATIENT_AGE"].ToString();

                txtPatientAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
                //lblViewPatientAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();


                txtPatientCity.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
                //lblViewPatientCity.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();

                txtPatientZip.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
                //lblViewPatientZip.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();

                txtPatientPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();
                //lblViewHomePhone.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();

                txtPatientEmail.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_EMAIL"].ToString();
                //lblViewPatientEmail.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_EMAIL"].ToString();

                txtWorkPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_WORK_PHONE"].ToString();
                //lblViewWorkPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_WORK_PHONE"].ToString();

                txtExtension.Text = _patientDs.Tables[0].Rows[0]["SZ_WORK_PHONE_EXTENSION"].ToString();
                //lblViewPatientExt.Text = _patientDs.Tables[0].Rows[0]["SZ_WORK_PHONE_EXTENSION"].ToString();

                if (_patientDs.Tables[0].Rows[0]["BT_WRONG_PHONE"].ToString() == "True" && _patientDs.Tables[0].Rows[0]["BT_WRONG_PHONE"].ToString() != "")
                {
                    CheckBox chkViewWrongPhone = (CheckBox)DtlView.FindControl("chkViewWrongPhone");
                    chkWrongPhone.Checked = true;
                    chkViewWrongPhone.Checked = true;
                }
                else
                {
                    //CheckBox chkViewWrongPhone = (CheckBox)DtlView.FindControl("chkViewWrongPhone");
                    chkWrongPhone.Checked = false;
                    //chkViewWrongPhone.Checked = false;
                }

                txtMI.Text = _patientDs.Tables[0].Rows[0]["MI"].ToString();
               Label lblViewMiddleName = (Label)DtlView.Items[0].FindControl("lblViewMiddleName");
                //Label lblViewMiddleName = (Label)DtlView.FindControl("lblViewMiddleName");
                lblViewMiddleName.Text = _patientDs.Tables[0].Rows[0]["MI"].ToString();

                txtWCBNo.Text = _patientDs.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();


                txtSocialSecurityNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                //lblViewSSN.Text = _patientDs.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();

                if (_patientDs.Tables[0].Rows[0]["DT_DOB"].ToString() != "01/01/1900" && _patientDs.Tables[0].Rows[0]["DT_DOB"].ToString() != "&nbsp;")
                {
                    txtDateOfBirth.Text = _patientDs.Tables[0].Rows[0]["DT_DOB"].ToString();
                    //lblViewDateofbirth.Text = _patientDs.Tables[0].Rows[0]["DT_DOB"].ToString();
                }
                else
                {
                    txtDateOfBirth.Text = "";
                    //lblViewDateofbirth.Text = "";
                }
                Label lblViewGender = (Label)DtlView.Items[0].FindControl("lblViewGender"); 
                ddlSex.SelectedValue = _patientDs.Tables[0].Rows[0]["SZ_GENDER"].ToString();
                lblViewGender.Text = ddlSex.SelectedItem.Text;

                if (_patientDs.Tables[0].Rows[0]["DT_INJURY"].ToString() != "01/01/1900" && _patientDs.Tables[0].Rows[0]["DT_INJURY"].ToString() != "&nbsp;")
                {
                    txtDateOfInjury.Text = _patientDs.Tables[0].Rows[0]["DT_INJURY"].ToString();
                }
                else
                {
                    txtDateOfInjury.Text = "";
                }


                txtJobTitle.Text = _patientDs.Tables[0].Rows[0]["SZ_JOB_TITLE"].ToString();
                txtWorkActivites.Text = _patientDs.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES"].ToString();
                txtState.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString();
                txtCarrierCaseNo.Text = _patientDs.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString();

                txtEmployerName.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();
                //lblViewEmployerName.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();

                txtEmployerPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE"].ToString();
                //lblViewEmployerPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE"].ToString();

                txtEmployerAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString();
                //lblViewEmployerAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString();

                txtEmployerCity.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString();
                //lblViewEmployerCity.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString();

                txtEmployerState.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString();
                //lblViewEmployerState.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString();

                txtEmployerZip.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString();
                //lblViewEmployerZip.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString();

                if (_patientDs.Tables[0].Rows[0]["BT_TRANSPORTATION"]== "True" && _patientDs.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"]!= "")
                {
                    CheckBox chkTransportation=(CheckBox)DtlView.Items[0].FindControl("chkTransportation");
                    chkTransportation.Checked = true;
                    chkTransportation.Checked = true;
                }
                else
                {
                    chkTransportation.Checked = false;
                    chkTransportation.Checked = false;
                }
                extddlPatientState.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();

                Label lblViewPatientState = (Label)DtlView.Items[0].FindControl("lblViewPatientState");
                if (extddlPatientState.Text != "NA" && extddlPatientState.Text != "")
                    lblViewPatientState.Text = extddlPatientState.Selected_Text;
                extddlEmployerState.Text = _patientDs.Tables[0].Rows[0]["SZ_EMPLOYER_STATE_ID"].ToString();

                Label lblViewEmployerState = (Label)DtlView.Items[0].FindControl("lblViewEmployerState");
                if (extddlEmployerState.Text != "NA" && extddlEmployerState.Text != "")
                    lblViewEmployerState.Text = extddlEmployerState.Selected_Text;

                txtChartNo.Text = _patientDs.Tables[0].Rows[0]["SZ_CHART_NO"].ToString();
                //lblViewChartNo.Text = _patientDs.Tables[0].Rows[0]["SZ_CHART_NO"].ToString();

                if (_patientDs.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString() != "01/01/1900" && _patientDs.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString() != "&nbsp;")
                {
                    txtDateofFirstTreatment.Text = _patientDs.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString();
                    //lblViewDateOfFirstTreatment.Text = _patientDs.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString();
                }
                else
                {
                    txtDateofFirstTreatment.Text = "";
                    //lblViewDateOfFirstTreatment.Text = "";
                }


            }

            //_bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            //DataSet _patientAccidentDs = _bill_Sys_PatientBO.GetPatientAccidentDetails(txtPatientID.Text);
            //txtAccidentID
            //if (_patientDs.Tables[0].Rows.Count > 0)
            //{
                ClearPatientAccidentControl();
                if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_INFO_ID"].ToString() != "&nbsp;")
                {
                    txtAccidentID.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_INFO_ID"].ToString();
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString() != "&nbsp;")
                {
                    txtPlatenumber.Text = _patientDs.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString();
                    //lblViewAccidentPlatenumber.Text = _patientDs.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString();
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != "&nbsp;")
                {
                    txtAccidentAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
                    //lblViewAccidentAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != "&nbsp;")
                {
                    txtAccidentCity.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
                    //lblViewAccidentCity.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString() != "&nbsp;")
                {
                    txtAccidentState.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString();
                    //lblViewAccidentState.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString();
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString() != "&nbsp;")
                {
                    txtPolicyReport.Text = _patientDs.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString();
                    ////lblViewAccidentReportNumber.Text = _patientDs.Tables[0].Rows[0].ItemsArray.GetValue(6).ToString();
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString() != "&nbsp;")
                {
                    txtListOfPatient.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString();
                    ////lblViewAdditionalPatient.Text = _patientDs.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                }
                if (_patientDs.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "01/01/1900")
                {
                    txtDateofAccident.Text = _patientDs.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
                    //lblViewAccidentDate.Text = _patientDs.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
                }
                else
                {
                    txtDateofAccident.Text = "";
                    //lblViewAccidentDate.Text = "";
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString() != "&nbsp;")
                {
                    string sz_patienttype = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString();
                    foreach (ListItem li in rdolstPatientType.Items)
                    {
                        if (li.Value.ToString() == sz_patienttype)
                        {
                            li.Selected = true;
                            //lblPatientType.Text = "Bicyclist Driver";
                            break;
                        }
                    }

                    foreach (ListItem li in rdolstPatientType.Items)
                    {
                        if (li.Selected == true)
                        {
                            Label lblPatientType = (Label)DtlView.Items[0].FindControl("lblPatientType"); 
                            lblPatientType.Text = li.Text.ToString();
                        }
                    }

                }
                //   if (_patientDs.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() != "&nbsp;") { extddlAccidentState.Text = _patientDs.Tables[0].Rows[0].ItemArray.GetValue(10).ToString(); }
            //}


            //Billing_Sys_ManageNotesBO manageNotes = new Billing_Sys_ManageNotesBO();
            //DataSet _patientDs = manageNotes.GetCaseDetails(txtPatientID.Text);
            //if (_patientDs.Tables[0].Rows.Count > 0)
            //{
                Label lblVLocation = (Label)DtlView.Items[0].FindControl("lblVLocation1");
                lblVLocation.Text = _patientDs.Tables[0].Rows[0]["sz_location_Name"].ToString();
                //txtPatientID.Text = Session["PatientID"].ToString();
                if (_patientDs.Tables[0].Rows[0]["SZ_CASE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_CASE_ID"].ToString() != "") { txtCaseID.Text = _patientDs.Tables[0].Rows[0]["SZ_CASE_ID"].ToString(); }
                if (_patientDs.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString() != "")
                {

                    extddlCaseType.Text = _patientDs.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString();

                    Label lblViewCasetype =(Label)DtlView.Items[0].FindControl("lblViewCasetype");
                    if (extddlCaseType.Text != "NA" && extddlCaseType.Text != "")
                        lblViewCasetype.Text = extddlCaseType.Selected_Text;
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString() != "")
                {
                    extddlProvider.Text = _patientDs.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString();

                }
                /*vivek patil*/
                if (_patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "")
                {
                    extddlInsuranceCompany.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString(); // Not in use
                    hdninsurancecode.Value = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
                    txtInsuranceCompany.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();

                    if (extddlInsuranceCompany.Text != "NA" && extddlInsuranceCompany.Text != "") //Not use
                      //  lblViewInsuranceName.Text = extddlInsuranceCompany.Selected_Text;
                    if (txtInsuranceCompany.Text != "" && txtInsuranceCompany.Text != "No suggestions found for your search")
                        //lblViewInsuranceName.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                    _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                  //  lstInsuranceCompanyAddress.Items.Clear();
                  //notuse  lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text).Tables[0];
                    lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(hdninsurancecode.Value).Tables[0];
                    lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    lstInsuranceCompanyAddress.DataValueField = "CODE";
                    lstInsuranceCompanyAddress.DataBind();
                    
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString() != "")
                {
                    extddlCaseStatus.Text = _patientDs.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString();

                    Label lblViewCaseStatus = (Label)DtlView.Items[0].FindControl("lblViewCaseStatus");
                    if (extddlCaseStatus.Text != "NA" && extddlCaseStatus.Text != "")
                        lblViewCaseStatus.Text = extddlCaseStatus.Selected_Text;
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString() != "")
                {
                    extddlAttorney.Text = _patientDs.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString();
                    Label lblViewAttorney = (Label)DtlView.Items[0].FindControl("lblViewAttorney"); 
                    if (extddlAttorney.Text != "NA" && extddlAttorney.Text != "")
                        lblViewAttorney.Text = extddlAttorney.Selected_Text;
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "")
                {
                    txtClaimNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                    //lblViewInsuranceClaimNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                    if (txtClaimNumber.Text.Equals("NA"))
                    {
                        txtClaimNumber.Text = "";
                        //lblViewInsuranceClaimNumber.Text = "";
                    }
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "")
                {
                    txtPolicyNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString();
                    //lblViewPolicyNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString();
                    if (txtPolicyNumber.Text.Equals("NA")) {
                        txtPolicyNumber.Text = "";
                        //lblViewPolicyNumber.Text = "";
                    }
                }
                //    if (_patientDs.Tables[0].Rows[0].ItemArray.GetValue(9).ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0].ItemArray.GetValue(9).ToString() != "") { txtDateofAccident.Text = Convert.ToDateTime(_patientDs.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()).ToShortDateString(); }
                if (_patientDs.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString() != "")
                {
                    extddlAdjuster.Text = _patientDs.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString();
                    Label lblViewAdjusterName = (Label)DtlView.Items[0].FindControl("lblViewAdjusterName");
                    if (extddlAdjuster.Text != "NA" && extddlAdjuster.Text != "")
                        lblViewAdjusterName.Text = extddlAdjuster.Selected_Text;
                }
                if (_patientDs.Tables[0].Rows[0]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString() != "")
                {
                    txtAssociateDiagnosisCode.Text = _patientDs.Tables[0].Rows[0]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString();

                }
                if (_patientDs.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString() != "")
                {
                    txtPlatenumber.Text = _patientDs.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString();
                    //lblViewAccidentPlatenumber.Text = _patientDs.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString();
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != "")
                {
                    txtAccidentAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
                    //lblViewAccidentAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != "")
                {
                    txtAccidentCity.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
                    //lblViewAccidentCity.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString() != "")
                {
                    txtAccidentState.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString();
                    //lblViewAccidentState.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString();
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString() != "")
                {
                    txtPolicyReport.Text = _patientDs.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString();

                }
                if (_patientDs.Tables[0].Rows[0]["SZ_LIST_OF_PATIENTS"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_LIST_OF_PATIENTS"].ToString() != "")
                {
                    txtListOfPatient.Text = _patientDs.Tables[0].Rows[0]["SZ_LIST_OF_PATIENTS"].ToString();

                }
                if (_patientDs.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString() != "")
                {
                    extddlLocation.Text = _patientDs.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString();
                    Label lblVLocation14 = (Label)DtlView.Items[0].FindControl("lblVLocation1");
                    if (extddlLocation.Text != "NA" && extddlLocation.Text != "")
                        lblVLocation14.Text = extddlLocation.Selected_Text;
                }
                if (_patientDs.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString() != "" && _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "")
                {
                    try
                    {
                        lstInsuranceCompanyAddress.SelectedValue = _patientDs.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString();

                        //_bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                        //ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(lstInsuranceCompanyAddress.SelectedValue);
                        //if (_arraylist.Count > 0)
                        //{
                            txtInsuranceAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                            //lblViewInsuranceAddressOne.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();

                            txtInsuranceCity.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                            //lblViewInsuranceCity.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                            txtInsuranceState.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                            //lblViewInsuranceState.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                            txtInsuranceZip.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                            //lblViewInsuranceZip.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                            txtInsuranceStreet.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_STREET"].ToString();

                            txtInsFax.Text = _patientDs.Tables[0].Rows[0]["sz_fax_number"].ToString();
                            //lblInsFax.Text = _patientDs.Tables[0].Rows[0]["sz_fax_number"].ToString();
                            txtInsPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                            //lblInsPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                            txtInsContactPerson.Text = _patientDs.Tables[0].Rows[0]["sz_contact_person"].ToString();
                            //lblInsContactPerson.Text = _patientDs.Tables[0].Rows[0]["sz_contact_person"].ToString();

                        //}
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
           

            if (_patientDs.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString() != "&nbsp;" && _patientDs.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString() != "")
            {
                
                //lblViewPolicyHolder.Text = _patientDs.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString();
                txtPolicyHolder.Text = _patientDs.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString();
                if (txtPolicyHolder.Text.Equals("NA"))
                {
                    txtPolicyHolder.Text = "";
                    //lblViewPolicyHolder.Text = "";
                }
            }
            //_bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            //ArrayList arr = new ArrayList();
            //arr = _bill_Sys_PatientBO.GetAdjusterDetail(extddlAdjuster.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            //if (arr.Count > 0)
            //{
                txtAdjusterPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_PHONE"].ToString();
                //lblViewAdjusterPhone.Text = _patientDs.Tables[0].Rows[0]["SZ_PHONE"].ToString();
            //}
            //if (arr.Count > 1)
            //{
                txtAdjusterExtension.Text = _patientDs.Tables[0].Rows[0]["SZ_EXTENSION"].ToString();
                //lblViewAdjusterExt.Text = _patientDs.Tables[0].Rows[0]["SZ_EXTENSION"].ToString();
            //}
            //if (arr.Count > 2)
            //{
                txtfax.Text = _patientDs.Tables[0].Rows[0]["SZ_FAX"].ToString();
                //lblViewAdjusterFax.Text = _patientDs.Tables[0].Rows[0]["SZ_FAX"].ToString();
           // }
            //if (arr.Count > 3)
           // {
                txtEmail.Text = _patientDs.Tables[0].Rows[0]["SZ_EMAIL"].ToString();
                //lblViewAdjusterEmail.Text = _patientDs.Tables[0].Rows[0]["SZ_EMAIL"].ToString();
            //}



            //////////////////// DISPLAY ACCIDENT INFORMATION
            //Patient_TVBO objPBO = new Patient_TVBO();
            //DataSet _patientDs = objPBO.GetAccidentInformation(txtPatientID.Text);
            //txtAccidentID
            //if (_patientDs.Tables[0].Rows.Count > 0)
            //{
                ClearPatientAccidentControl();
                txtATPlateNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString();

                if (_patientDs.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "01/01/1900")
                {
                    txtATAccidentDate.Text = _patientDs.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
                }
                else
                {
                    txtATAccidentDate.Text = "";
                }
                txtATAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
                //lblViewAccidentAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();

                txtATCity.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
                //lblViewAccidentCity.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();

                txtATReportNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString();
                //lblViewAccidentReportNumber.Text = _patientDs.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString();

                txtATAdditionalPatients.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString();
                //lblViewAdditionalPatient.Text = _patientDs.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString();

                extddlATAccidentState.Text = _patientDs.Tables[0].Rows[0]["SZ_ACCIDENT_STATE_ID"].ToString();
                Label lblViewAccidentState = (Label)DtlView.Items[0].FindControl("lblViewAccidentState");
                if (extddlATAccidentState.Text != "NA" && extddlATAccidentState.Text != "")
                    lblViewAccidentState.Text = extddlATAccidentState.Selected_Text;

                txtATHospitalName.Text = _patientDs.Tables[0].Rows[0]["SZ_HOSPITAL_NAME"].ToString();
                //lblViewAccidentHospitalName.Text = _patientDs.Tables[0].Rows[0]["SZ_HOSPITAL_NAME"].ToString();

                txtATHospitalAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_HOSPITAL_ADDRESS"].ToString();

                //lblViewAccidentHospitalAddress.Text = _patientDs.Tables[0].Rows[0]["SZ_HOSPITAL_ADDRESS"].ToString();

                txtATDescribeInjury.Text = _patientDs.Tables[0].Rows[0]["SZ_DESCRIBE_INJURY"].ToString();
                //lblViewDescribeInjury.Text = _patientDs.Tables[0].Rows[0]["SZ_DESCRIBE_INJURY"].ToString();

                txtATAdmissionDate.Text = _patientDs.Tables[0].Rows[0]["DT_ADMISSION_DATE"].ToString();
                //lblViewAccidentDateofAdmission.Text = _patientDs.Tables[0].Rows[0]["DT_ADMISSION_DATE"].ToString();

            //}
            // END


            //btnSave.Enabled = false;
            //btnPatientUpdate.Enabled = true;
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
    //***************//
    protected void btnPatientUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //_editOperation = new EditOperation();
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
            Boolean chartflag = false;
            if (txtChartNo.Visible == true)
            {
                if (!txtChartNo.Text.Equals(""))
                {
                    chartflag = true;
                    _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    Label lblViewChartNo = (Label)DtlView.Items[0].FindControl("lblViewChartNo");
                    if (!lblViewChartNo.Text.Equals(txtChartNo.Text))
                    {
                        string flag = "CHART";
                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            flag = "REF";
                        }
                        if (!_bill_Sys_PatientBO.ExistChartNumber(txtCompanyID.Text, txtChartNo.Text, flag))
                        {
                            UpdatePatientInformation();

                        }
                        else
                        {
                            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>alert( '" + txtChartNo.Text + "' + ' Chart No Allready Exist ...!');</script>");
                            // Page.RegisterStartupScript("mm", "<script language='javascript'>alert('" + txtChartNo.Text + "' + ' chart no is already exist ...!');</script>");
                            usrMessage.PutMessage(txtChartNo.Text + "  chart no is already exist ...!");
                            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
                            usrMessage.Show();
                            return;
                        }
                    }
                    else
                    {
                        chartflag = false;
                    }
                }

            }
            if (!chartflag)
            {
             
                UpdatePatientInformation();
            }

           // UpdatePatientInformation();
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            _bill_Sys_PatientBO.UpdateTemplateStatus(Session["TM_SZ_CASE_ID"].ToString(), chkStatusProc.Checked == true ? 1 : 0, txtNF2Date.Text);
           // lblMsg.Visible = true;
            Page.MaintainScrollPositionOnPostBack = false;
            CheckTemplateStatus(Session["TM_SZ_CASE_ID"].ToString());
            LoadDataOnPage();
           // lblMsg.Text = " Patient Information Updated successfully ! ";
            usrMessage.PutMessage("Patient Information Updated successfully !");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            Session["AttornyID"] = extddlAttorney.Text; // Girish
            SaveNotes();
            LoadNoteGrid();
            GetPatientDeskList();
            if (!btassociate.Checked)  //Prashant Assocoite case no update 
            {
                if (!associatecaseno.Equals(""))
                {
                    UpdateCopyToCase(associatecaseno);
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


    protected void UpdatePatientInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
            //_objAL.Add(txtWCBNumber.Text);
            _objAL.Add(txtPolicyNumber.Text);
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
            Label lblVLocationAdd = (Label)DtlView.Items[0].FindControl("lblVLocation1");
            _objAL.Add(lblVLocationAdd.Text);

            _objAL.Add("UPDATE");
            _objAL.Add(txtPatientID.Text);

            if (chkWrongPhone.Checked) { _objAL.Add("True"); } else { _objAL.Add("False"); }
            if (chkTransportation.Checked) { _objAL.Add("True"); } else { _objAL.Add("False"); }

            //_objAL.Add(txtDateofAccident.Text);
            //_objAL.Add(txtPlatenumber.Text);
            //_objAL.Add(txtPolicyReport.Text);

            _objAL.Add(extddlEmployerState.Text);
            _objAL.Add(extddlPatientState.Text);

            _objAL.Add(txtChartNo.Text);
            _objAL.Add(txtDateofFirstTreatment.Text);
            

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

            if (txtAccidentID.Text != "")
            {
                _objAL.Add("UPDATE");
            }
            else
            {
                _objAL.Add("Add");
            }
            _objAL.Add(extddlATAccidentState.Text);
            _objAL.Add("");

            objPatientBO.savePatientAccidentInformation(_objAL);




            _objAL = new ArrayList();
            _objAL.Add(txtCaseID.Text);
            _objAL.Add("");
            _objAL.Add(extddlCaseType.Text);
            //if (!extddlInsuranceCompany.Text.Equals("") && !extddlInsuranceCompany.Text.Equals("NA"))
            //{
            //    _objAL.Add(extddlInsuranceCompany.Text);
            //}
            //else
            //{
            //    _objAL.Add("");
            //}
            if (!txtInsuranceCompany.Text.Equals("") && !txtInsuranceCompany.Text.Equals("No suggestions found for your search") && hdninsurancecode.Value != "")
            {
                _objAL.Add(hdninsurancecode.Value);
            }
            else
            {
                _objAL.Add("");
            }
            
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
            _objAL.Add(txtPolicyHolder.Text);
            _objAL.Add(extddlLocation.Text);
            _objAL.Add(txtWCBNo.Text);
            //_objAL.Add (txtAdjusterPhone.Text);
            //_objAL.Add (txtAdjusterExtension.Text);
            //_objAL.Add (txtfax.Text);
            //_objAL.Add(txtEmail.Text);

            objPatientBO = new Patient_TVBO();
            objPatientBO.saveCaseInformation(_objAL);

            /*
             @DT_ACCIDENT_DATE", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_ADDRESS", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_CITY", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_STATE", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REPORT_NO", objAL[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_FROM_CAR", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_STATE_ID", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_HOSPITAL_NAME", objAL[9].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_HOSPITAL_ADDRESS", objAL[10].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DESCRIBE_INJURY", objAL[11].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_ADMISSION_DATE", 
             */

            // Save Accident Information

            ArrayList objAccidentInfo = new ArrayList();
            objAccidentInfo.Add(txtPatientID.Text);
            objAccidentInfo.Add(txtATPlateNumber.Text);
            objAccidentInfo.Add(txtATAccidentDate.Text);
            objAccidentInfo.Add(txtATAddress.Text);
            objAccidentInfo.Add(txtATCity.Text);
            objAccidentInfo.Add(txtATReportNumber.Text);
            objAccidentInfo.Add(txtATAdditionalPatients.Text);
            objAccidentInfo.Add(extddlATAccidentState.Text);
            objAccidentInfo.Add(txtATHospitalName.Text);
            objAccidentInfo.Add(txtATHospitalAddress.Text);
            objAccidentInfo.Add(txtATDescribeInjury.Text);
            objAccidentInfo.Add(txtATAdmissionDate.Text);
            objAccidentInfo.Add("");
            objPatientBO.saveAccidentInformation(objAccidentInfo);
            // End

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


    private void ClearPatientAccidentControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //txtAccidentID.Text = "";
            //txtPlatenumber.Text = "";
            //txtAccidentAddress.Text = "";
            //txtAccidentCity.Text = "";
            //txtAccidentState.Text = "";
            //txtPolicyReport.Text = "";
            //txtListOfPatient.Text = "";
            //txtDateofAccident.Text = "";

            //txtPlatenumber.Text = "";
            txtATAccidentDate.Text = "";
            txtATAddress.Text = "";
            txtATCity.Text = "";
            txtATReportNumber.Text = "";
            txtATAdditionalPatients.Text = "";
            extddlATAccidentState.Text = "NA";
            txtATHospitalName.Text = "";
            txtATHospitalAddress.Text = "";
            txtATDescribeInjury.Text = "";
            txtATAdmissionDate.Text = "";
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


    // To load Patient Details
    private void GetPatientDeskList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
          //  grdPatientDeskList.DataSource = _bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
          //  grdPatientDeskList.DataBind();
            rptPatientDeskList.DataSource = _bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            rptPatientDeskList.DataBind();

            //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == false && (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
            {
               // grdPatientDeskList.Columns[1].Visible = true;
            }
            else
            {
                //grdPatientDeskList.Columns[1].Visible = false;
                for (int i = 0; i < rptPatientDeskList.Items.Count; i++)
                {
                    HtmlTableCell tblcell = (HtmlTableCell)rptPatientDeskList.Controls[0].FindControl("tblheader");
                    HtmlTableCell tblcellvalue = (HtmlTableCell)rptPatientDeskList.Items[i].FindControl("tblvalue");
                    tblcell.Visible = false;
                    tblcellvalue.Visible = false;

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

    //protected void CopyToCase()
    //{
    //    string szCaseNos = txtAssociateCases.Text;
    //    string szMsg = null;

    //    try
    //    {
    //        if (string.IsNullOrEmpty(szCaseNos))
    //            throw new FormatException();

    //        if (szCaseNos.IndexOf(',') == -1)
    //            szCaseNos += ",";

    //        string szInsuranceAddress = "";
    //        if (!txtInsuranceAddress.Text.Equals(""))
    //            szInsuranceAddress = lstInsuranceCompanyAddress.Text;

    //        string[] szCaseNo = szCaseNos.Split(',');

    //        for (int i = 0; i < szCaseNo.Length; i++)
    //        {
    //            Patient_TVBO objPatientBO = new Patient_TVBO();
    //            ArrayList _objAL = new ArrayList();
    //            _objAL.Add(szCaseNo[i].ToString());
    //            _objAL.Add(extddlInsuranceCompany.Text);
    //            _objAL.Add(szInsuranceAddress);
    //            _objAL.Add(txtCompanyID.Text);
    //            _objAL.Add(txtClaimNumber.Text);
    //            _objAL.Add(txtPolicyNumber.Text);
    //            _objAL.Add(extddlAdjuster.Text);
    //            _objAL.Add(txtPolicyHolder.Text);
    //            _objAL.Add("COPYTO");
    //            objPatientBO.UpdateInsurancetoCase(_objAL);
    //        }
    //    }
    //    catch (FormatException fe)
    //    {
    //        szMsg = "Please enter Copy to Case No.";
    //    }
    //    finally
    //    {
    //        if (string.IsNullOrEmpty(szMsg))
    //            szMsg = "The Insurance Company and Address copied to case no�s successfully.";

    //        lblMsg.Visible = true;
    //        lblMsg.Text = szMsg;
    //    }
    //}


    protected void CopyToCase()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        // string szCaseNos = txtAssociateCases.Text; // comment for only allow coditionwise
        string szCaseNos = associtecasenoAllow;
        string szMsg = null;
        string szMsgNotAssociate = null;


        try
        {

            if (string.IsNullOrEmpty(szCaseNos))
                throw new FormatException();

            if (szCaseNos.IndexOf(',') == -1)
                szCaseNos += ",";

            string szInsuranceAddress = "";
            if (!txtInsuranceAddress.Text.Equals(""))
                szInsuranceAddress = lstInsuranceCompanyAddress.Text;

            string[] szCaseNo = szCaseNos.Split(',');

            for (int i = 0; i < szCaseNo.Length; i++)
            {
                Patient_TVBO objPatientBO = new Patient_TVBO();
                // string _check =   objPatientBO.SavetoSaveToAllowed(szCaseNo[i].ToString(),txtCompanyID.Text,(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID).ToString());
                // ArrayList _objAL = new ArrayList();
                //_objAL.Add(szCaseNo[i].ToString());
                //_objAL.Add(extddlInsuranceCompany.Text);
                //_objAL.Add(szInsuranceAddress);
                //_objAL.Add(txtCompanyID.Text);
                //_objAL.Add(txtClaimNumber.Text);
                //_objAL.Add(txtPolicyNumber.Text);
                //_objAL.Add(extddlAdjuster.Text);
                //_objAL.Add(txtPolicyHolder.Text);
                //_objAL.Add("COPYTO");
                //objPatientBO.UpdateInsurancetoCase(_objAL);
                string _sz_case_id = "";
                if (!commonrange.IsMatch((((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO).ToString()))
                {
                    _sz_case_id = (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO).ToString();
                }
                else
                {
                    _sz_case_id = (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO).ToString().Remove(0,2);
                }

                
                objPatientBO.UpdatetoSaveToAllowed(szCaseNo[i].ToString(), txtCompanyID.Text,_sz_case_id , "InsAddressUpdate");


            }
            LoadDataOnPage();

        }
        catch (FormatException ex)
        {
            szMsg = "Please enter Copy to Case No.";
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        finally
        {
            if (string.IsNullOrEmpty(szMsg))
                if (associatecasenoNotAllow.Equals(""))
                {
                    szMsg = "The Insurance Company and Address copied to case no�s successfully.";
                    usrMessage.PutMessage(szMsg);
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                }
                else
                {
                    szMsg = associatecasenoNotAllow + " not allow to asscociate";
                    usrMessage.PutMessage(szMsg);
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
                    usrMessage.Show();
                }

            //lblMsg.Visible = true;
            //lblMsg.Text = szMsg;


        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #region "Save Notes"

    private void SaveNotes()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _notesOperation = new NotesOperation();
        try
        {

            _notesOperation.WebPage = this.Page;
            _notesOperation.Xml_File = "InformationNotesXML.xml";
            _notesOperation.Case_ID = txtCaseID.Text;
            _notesOperation.User_ID = txtUserID.Text;
            _notesOperation.Company_ID = txtCompanyID.Text;
            _notesOperation.SaveNotesOperation();

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

    #endregion
    protected void lnkGenerateNF2_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //NF2 TEMPLATE START

            PDFValueReplacement.PDFValueReplacement objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();

            String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
            String szPDFPhysicalpath = ConfigurationManager.AppSettings["NF2_PDF_FILE"];
            String szXMLPhysicalpath = ConfigurationManager.AppSettings["NF2_XML_FILE"];
            string szGeneratedPDFName = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpath, szPDFPhysicalpath, Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());

            //string szGeneratedPDFPath = objNF3Template.getPhysicalPath() + "/" + szDefaultPath + szGeneratedPDFName;

            String szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDefaultPath + szGeneratedPDFName;


            CheckTemplateStatus(Session["TM_SZ_CASE_ID"].ToString());
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
            //NF2 TEMPLATE END

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

    private void CheckTemplateStatus(string p_szCaseID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            DataTable dt = _bill_Sys_PatientBO.GetTemplateStatus(p_szCaseID);
            if (Convert.ToBoolean(dt.Rows[0].ItemArray.GetValue(0)) == true)
            {
                chkStatusProc.Checked = true;
                txtNF2Date.Text = dt.Rows[0].ItemArray.GetValue(1).ToString();
            }
            else
            {
                chkStatusProc.Checked = false;
                txtNF2Date.Text = "";
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
    protected void grdPatientDeskList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "genpdf")
            {
                string szDefaultPath = ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"];
                string szPDFName = "";
                szPDFName = szDefaultPath + GeneratePDF();
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFName.ToString() + "'); ", true);
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




    protected void rptPatientDeskList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "genpdf")
            {
                string szDefaultPath = ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"];
                string szPDFName = "";
                szPDFName = szDefaultPath + GeneratePDF();
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFName.ToString() + "'); ", true);
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

    protected string GeneratePDF()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
        string pdffilename = "";
        try
        {

            string strHtml = File.ReadAllText(ConfigurationManager.AppSettings["PATIENT_INFO_HTML"]);
            strHtml = objPDF.getReplacedString(strHtml, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            SautinSoft.PdfMetamorphosis objHTMToPDF = new SautinSoft.PdfMetamorphosis();
            objHTMToPDF.Serial = "10007706603";
            string htmfilename = getFileName("P") + ".htm";
            pdffilename = getFileName("P") + ".pdf";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + htmfilename);
            sw.Write(strHtml);
            sw.Close();
            Int32 iTemp;
            iTemp = objHTMToPDF.HtmlToPdfConvertFile(ConfigurationManager.AppSettings["EXCEL_SHEET"] + htmfilename, ConfigurationManager.AppSettings["EXCEL_SHEET"] + pdffilename);
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
        return pdffilename;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
      
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




    protected void lnkNF2Envelope_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //NF2 TEMPLATE START

            PDFValueReplacement.PDFValueReplacement objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();

            String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
            String szPDFPhysicalpath = ConfigurationManager.AppSettings["NF2_ENVELOPE_PDF_FILE"];
            String szXMLPhysicalpath = ConfigurationManager.AppSettings["NF2_ENVELOPE_XML_FILE"];
            string szGeneratedPDFName = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpath, szPDFPhysicalpath, Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());

            //string szGeneratedPDFPath = objNF3Template.getPhysicalPath() + "/" + szDefaultPath + szGeneratedPDFName;

            String szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDefaultPath + szGeneratedPDFName;

            //_bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            //_bill_Sys_PatientBO.UpdateTemplateStatus(Session["TM_SZ_CASE_ID"].ToString());

            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
            //NF2 TEMPLATE END

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

    protected void btnAssociate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (allowAssociate())
        {
            CopyToCase();
            CaseDetailsBO obj = new CaseDetailsBO();
            string secondCase = "";
            //      obj.SaveAssociateCases(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, "", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DELETE");
            try
            {
                if (txtAssociateCases.Text != "")
                {
                 // string[] szListOfCaseIDs = txtAssociateCases.Text.Split(',');
                  string[] szListOfCaseIDs = associtecasenoAllow.Split(',');
                    for (int j = 0; j < szListOfCaseIDs.Length; j++)
                    {
                        obj.SaveAssociateCases(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, szListOfCaseIDs[j].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "ADD");

                        for (int i = j + 1; i < szListOfCaseIDs.Length; i++)
                        {
                            secondCase = obj.GetCaseID(szListOfCaseIDs[j].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            if (secondCase.Equals("")) //if second case is null 
                            {
                                secondCase = obj.GetCaseIDEmpty(szListOfCaseIDs[j].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            }
                            obj.SaveAssociateCases(secondCase, szListOfCaseIDs[i].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "ADD");
                           secondCase = "";
                        }
                    }
                }
                GetAssociateCases();
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
            
        }
        else
        {
            //lblMsg.Text = "Associate case not Allowed";
           // lblMsg.Visible = true;
            usrMessage.PutMessage("Associate case not Allowed");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
            usrMessage.Show();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void GetAssociateCases()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        CaseDetailsBO obj = new CaseDetailsBO();
        divAssociateCaseID.Controls.Clear();
        String szLink = "";
        try
        {
            DataSet dsAssociateCases = new DataSet();
            dsAssociateCases = obj.GetAssociateCases(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "LIST");
            for (int i = 0; i < dsAssociateCases.Tables[0].Rows.Count; i++)
            {
                LinkButton lnkTemp = new LinkButton();
                lnkTemp.ID = "lnk" + dsAssociateCases.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                lnkTemp.Text = dsAssociateCases.Tables[0].Rows[i]["NAME"].ToString() + " ";
                lnkTemp.CssClass = "lbl";
                lnkTemp.CommandArgument = dsAssociateCases.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                lnkTemp.Command += new CommandEventHandler(OnClick);
                divAssociateCaseID.Controls.Add(lnkTemp);
                associatecaseno = associatecaseno + dsAssociateCases.Tables[0].Rows[i]["SZ_CASE_NO"].ToString() + ",";
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

    //public void GetRelatedData()
    //{
    //    CaseDetailsBO obj = new CaseDetailsBO();
    //    try
    //    {
    //        DataSet dsAllData = new DataSet();
    //        dsAllData = obj.GetPOpupDetails(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

    //        if (dsAllData.Tables.Count > 0)
    //        {
    //            grdDateOfAccList.DataSource = dsAllData.Tables[0];
    //            grdDateOfAccList.DataBind();
    //        }
    //        if (dsAllData.Tables.Count > 1)
    //        {
    //            grdPlateNo.DataSource = dsAllData.Tables[1];
    //            grdPlateNo.DataBind();
    //        }
    //        if (dsAllData.Tables.Count > 2)
    //        {
    //            grdReportNO.DataSource = dsAllData.Tables[2];
    //            grdReportNO.DataBind();
    //        }
    //        if (dsAllData.Tables.Count > 3)
    //        {
    //            grdAdjuster.DataSource = dsAllData.Tables[3];
    //            grdAdjuster.DataBind();
    //        }
    //        if (dsAllData.Tables.Count > 4)
    //        {
    //            grdClaimNumber.DataSource = dsAllData.Tables[4];
    //            grdClaimNumber.DataBind();
    //        }
    //        if (dsAllData.Tables.Count > 5)
    //        {
    //            grdPolicyNumber.DataSource = dsAllData.Tables[5];
    //            grdPolicyNumber.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        usrMessage.PutMessage(ex.ToString());
    //        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
    //        usrMessage.Show();
    //    }
    //}



    //The event handler
    private void OnClick(object sender, CommandEventArgs e)
    {
        Response.Redirect("Bill_Sys_CaseDetails.aspx?CaseID=" + e.CommandArgument);
    }

    protected void btnSearchInsCompany_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            CaseDetailsBO _obj = new CaseDetailsBO();
       //     extddlInsuranceCompany.Text = _obj.SearchInsuranceCompany(txtSearchName.Text, txtSearchCode.Text, txtCompanyID.Text);
           hdninsurancecode.Value  = _obj.SearchInsuranceCompany(txtSearchName.Text, txtSearchCode.Text, txtCompanyID.Text);
            ClearInsurancecontrol();
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            //lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);
            lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(hdninsurancecode.Value);
            lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            lstInsuranceCompanyAddress.DataValueField = "CODE";
            lstInsuranceCompanyAddress.DataBind();
            Page.MaintainScrollPositionOnPostBack = true;
            tabcontainerPatientEntry.ActiveTabIndex = 2;
            txtSearchName.Text = "";
            txtSearchCode.Text = "";
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

    protected void btnAssignSupplies_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            CaseDetailsBO _objCDB = new CaseDetailsBO();
            _objCDB.DeleteCaseSupplies(txtCaseID.Text, txtCompanyID.Text);
            for (int i = 0; i < grdAssignSupplies.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)grdAssignSupplies.Items[i].FindControl("chkAssignSupplies");
                if (chk.Checked)
                {
                    _objCDB.InsertCaseSupplies(grdAssignSupplies.Items[i].Cells[2].Text, txtCaseID.Text, txtCompanyID.Text);
                }
            }
            BindSuppliesGrid();
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

    protected void BindSuppliesGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            CaseDetailsBO _objCDB = new CaseDetailsBO();
            grdAssignSupplies.DataSource = _objCDB.GetCaseSupplies(txtCaseID.Text, txtCompanyID.Text);
            grdAssignSupplies.DataBind();

            for (int i = 0; i < grdAssignSupplies.Items.Count; i++)
            {
                if (grdAssignSupplies.Items[i].Cells[1].Text == "1")
                {
                    CheckBox chk = (CheckBox)grdAssignSupplies.Items[i].FindControl("chkAssignSupplies");
                    chk.Checked = true;
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

    protected void btnSaveAdjuster_Click(object sender, EventArgs e)
    {
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

            extddlAdjuster.Text = _objPopupBO.getLatestID("SP_MST_ADJUSTER", txtCompanyID.Text);
            if (extddlAdjuster.Text != "")
            {
                txtAdjusterPhone.Text = txtAdjusterPopupPhone.Text;
                txtAdjusterExtension.Text = txtAdjusterPopupExtension.Text;
                txtfax.Text = txtAdjusterPopupFax.Text;
                txtEmail.Text = txtAdjusterPopupEmail.Text;
            }
            txtAdjusterPopupName.Text = "";
            txtAdjusterPopupPhone.Text = "";
            txtAdjusterPopupExtension.Text = "";
            txtAdjusterPopupFax.Text = "";
            txtAdjusterPopupEmail.Text = "";


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
            grdNotes.CurrentPageIndex = 0;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    #region "Set Read only fiedls"

    public void EnableDisableControl(bool value)
    {
        txtPatientFName.ReadOnly = value;
        txtMI.ReadOnly = value;
        txtPatientLName.ReadOnly = value;
        txtDateOfBirth.ReadOnly = value;
        CalendarExtender2.Enabled = !value;
        txtSocialSecurityNumber.ReadOnly = value;
        ddlSex.Enabled = !value;
        txtPatientAddress.ReadOnly = value;
        txtPatientStreet.ReadOnly = value;
        txtPatientCity.ReadOnly = value;
        txtState.ReadOnly = value;
        extddlPatientState.Enabled = !value;
        txtPatientZip.ReadOnly = value;
        txtPatientPhone.ReadOnly = value;
        txtWorkPhone.ReadOnly = value;
        txtExtension.ReadOnly = value;
        chkWrongPhone.Enabled = !value;
        txtPatientEmail.ReadOnly = value;
        extddlAttorney.Enabled = !value;

        extddlCaseType.Enabled = !value;


        chkTransportation.Enabled = !value;
        txtDateofAccident.ReadOnly = value;
        CalendarExtender3.Enabled = !value;
        imgbtnDateofAccident.Enabled = !value;
        txtPlatenumber.ReadOnly = value;
        txtPolicyReport.ReadOnly = value;
        txtEmployerName.ReadOnly = value;
        txtEmployerAddress.ReadOnly = value;
        txtEmployerCity.ReadOnly = value;
        txtEmployerState.ReadOnly = value;
        extddlEmployerState.Enabled = !value;
        txtEmployerZip.ReadOnly = value;
        txtEmployerPhone.ReadOnly = value;

        //extddlInsuranceCompany.Enabled = !value;
        txtInsuranceCompany.Enabled = !value;
        lstInsuranceCompanyAddress.Enabled = !value;
        txtInsuranceAddress.ReadOnly = value;
        txtInsuranceCity.ReadOnly = value;
        txtInsuranceState.ReadOnly = value;
        txtInsuranceZip.ReadOnly = value;

        txtClaimNumber.ReadOnly = value;
        txtWCBNumber.ReadOnly = value;
        extddlAdjuster.Enabled = !value;

        btnPatientUpdate.Visible = !value;
        btnPatientClear.Visible = !value;
        extddlCaseStatus.Enabled = !value;

        txtATAccidentDate.ReadOnly = value;
        calATAccidentDate.Enabled = !value;
        txtATReportNumber.ReadOnly = value;
        txtATPlateNumber.ReadOnly = value;
        txtATAddress.ReadOnly = value;
        txtATCity.ReadOnly = value;
        extddlATAccidentState.Enabled = !value;
        txtATHospitalName.ReadOnly = value;
        txtATHospitalAddress.ReadOnly = value;
        txtATAdmissionDate.ReadOnly = value;
        calATAdmissionDate.Enabled = !value;
        txtATAdditionalPatients.ReadOnly = value;
        txtATDescribeInjury.ReadOnly = value;

        txtDateofFirstTreatment.ReadOnly = value;
        CalendarExtender1.Enabled = !value;

        txtPolicyHolder.ReadOnly = value;

        txtPolicyNumber.ReadOnly = value;

        txtAssociateCases.ReadOnly = value;

        txtChartNo.ReadOnly = value;

        //btnPatientUpdate.Visible = value;
        //btnPatientClear.Visible = value;
        //  extddlCaseStatus.Enabled = value;

    }

    #endregion

    protected void UpdateCopyToCase(string associatecase) //for update
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string szCaseNos = associatecase;
        string szMsg = null;
        Boolean msgFlag = false;


        try
        {

            if (string.IsNullOrEmpty(szCaseNos))
                throw new FormatException();

            if (szCaseNos.IndexOf(',') == -1)
                szCaseNos += ",";

            string szInsuranceAddress = "";
            if (!txtInsuranceAddress.Text.Equals(""))
                szInsuranceAddress = lstInsuranceCompanyAddress.Text;

            string[] szCaseNo = szCaseNos.Split(',');

            for (int i = 0; i < szCaseNo.Length; i++)
            {
                Patient_TVBO objPatientBO = new Patient_TVBO();
                ArrayList _objAL = new ArrayList();
                _objAL.Add(szCaseNo[i].ToString());
         //       _objAL.Add(extddlInsuranceCompany.Text);
                _objAL.Add(hdninsurancecode.Value);
                _objAL.Add(szInsuranceAddress);
                _objAL.Add(txtCompanyID.Text);
                _objAL.Add(txtClaimNumber.Text);
                _objAL.Add(txtPolicyNumber.Text);
                _objAL.Add(extddlAdjuster.Text);
                _objAL.Add(txtPolicyHolder.Text);
                _objAL.Add("COPYTO");
                objPatientBO.UpdateInsurancetoCase(_objAL);
            }

        }
        catch (FormatException ex)
        {
            szMsg = "";
            msgFlag = true;

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        finally
        {
            if (!msgFlag)
                szMsg = "The Insurance Company and Address Updated to " + associatecaseno + "successfully.";
            usrMessage.PutMessage(szMsg);
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

            //lblMsg.Visible = true;
            //lblMsg.Text = szMsg;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public Boolean allowAssociate()  //check copy insusrance data allowed
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string szCaseNos = txtAssociateCases.Text;
        string szMsg = null;
        string szMsgNotAssociate = null;
        Boolean flag = false;
        int i = 0;
        string sz_CaseNo = "";
        string _check = "";

         string _checkCaseID = "";
         if (!commonrange.IsMatch((((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO).ToString()))
         {
            _checkCaseID = (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO).ToString();
         }
         else
         {
             _checkCaseID = (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO).ToString().Remove(0,2);
         }


        try
        {

            if (string.IsNullOrEmpty(szCaseNos))
                throw new FormatException();

            if (szCaseNos.IndexOf(',') == -1)
                szCaseNos += ",";

            string szInsuranceAddress = "";
            if (!txtInsuranceAddress.Text.Equals(""))
                szInsuranceAddress = lstInsuranceCompanyAddress.Text;

            string[] szCaseNo = szCaseNos.Split(',');

           if (!szCaseNo[1].ToString().Equals("")) {
               updateFlag = false;
               if (!CheckUpdate(szCaseNo, 0))
               {
                   return false;
               }

           }
           updateFlag = true;
            for (i = 0; i < szCaseNo.Length; i++)
            {
                if (szCaseNo[i] != "")
                {
                    Regex isnumber = new Regex("[^0-9]"); // check  prefix attched or not  if prefix attach remove first to character
                    if (isnumber.IsMatch(szCaseNo[i]))
                    {
                        sz_CaseNo = szCaseNo[i].Remove(0, 2);
                    }
                    else
                    {
                        sz_CaseNo = szCaseNo[i].ToString();
                    }
                    Patient_TVBO objPatientBO = new Patient_TVBO();
                   // _check = objPatientBO.SavetoSaveToAllowed(sz_CaseNo, txtCompanyID.Text, (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO).ToString(), "InsAddress");
                    _check = objPatientBO.SavetoSaveToAllowed(sz_CaseNo, txtCompanyID.Text, _checkCaseID, "InsAddress");
                   
                    
                    if (_check == "Same")
                    {
                        associtecasenoAllow = associtecasenoAllow + sz_CaseNo.ToString() + ",";
                        flag = true;
                    }
                    else if (_check == "Update")
                    {
                        if (CheckUpdate(szCaseNo, i))
                        {
                           // objPatientBO.UpdatetoSaveToAllowed(sz_CaseNo.ToString(), txtCompanyID.Text, (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO).ToString(), "InsAddressUpdate");
                            objPatientBO.UpdatetoSaveToAllowed(sz_CaseNo.ToString(), txtCompanyID.Text,_checkCaseID , "InsAddressUpdate");
                            associtecasenoAllow = associtecasenoAllow + sz_CaseNo.ToString() + ",";
                            LoadDataOnPage();
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                    else if (_check == "NotAllowed")
                    {
                       // associatecasenoNotAllow = associatecasenoNotAllow + szCaseNo[i].ToString() + ",";
                        return false;
                    }
                    else if (_check == "null")
                    {
                        associtecasenoAllow = associtecasenoAllow + sz_CaseNo.ToString() + ",";
                        associatecasenoNull = associatecasenoNull + szCaseNo[i].ToString() + ",";
                        flag = false;
                    }
                }
            }

        }
        catch (FormatException ex)
        {
            szMsg = "";
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        finally
        {
            //if (string.IsNullOrEmpty(szMsg))
            //  szMsg = "The Insurance Company and Address copied to case no�s successfully.";

            //lblMsg.Visible = true;
            //lblMsg.Text = szMsg;
        }
        return flag;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    public Boolean CheckUpdate(String[] szCase, int j)
    {
        Boolean flagCheck = true;
        string sz_updatecaseno="";
        string sz_jupdatecaseno = "";
        //if (szCase.Length = i + 1)
        //{

        //}
        Regex ischar = new Regex("[[A-z]");
        if (ischar.IsMatch(szCase[0]))
        {
            if (!szCase[1].Equals(""))
            {
                string temp = szCase[1];
                szCase[1] = szCase[0];
                szCase[0] = temp;
            }
        }
        for (int i = j + 1; i < szCase.Length; i++)
        {
            if (szCase[i] != "")
            {

                
                Regex isnumber = new Regex("[^0-9]"); // check  prefix attched or not  if prefix attach remove first to character

                if (isnumber.IsMatch(szCase[i]))
                {
                    sz_updatecaseno = szCase[i].Remove(0, 2);
                }
                else
                {
                    sz_updatecaseno = szCase[i].ToString();
                }

                if (isnumber.IsMatch(szCase[j]))
                {
                    sz_jupdatecaseno = szCase[j].Remove(0, 2);
                }
                else
                {
                    sz_jupdatecaseno = szCase[j].ToString();
                }
                Patient_TVBO objPatientBO = new Patient_TVBO();
                string _check = objPatientBO.SavetoSaveToAllowed(sz_updatecaseno, txtCompanyID.Text, sz_jupdatecaseno, "InsAddress");
                if (_check == "Same")
                {
                    // associtecasenoAllow = associtecasenoAllow + szCaseNo[i].ToString() + ",";
                    if (updateFlag)
                    {
                        flagCheck = true;
                        associtecasenoAllow = associtecasenoAllow + sz_updatecaseno + ",";
                    }
                }
                else if (_check == "NotAllowed")
                {
                    //associatecasenoNotAllow = associatecasenoNotAllow + szCaseNo[i].ToString() + ",";
                    return false;
                }

            }
        }
        return flagCheck;
    }

    protected void btnDAssociate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string szCaseNos = txtAssociateCases.Text;
        string szMsg = null;
        string szMsgNotAssociate = null;
        string sznotassociate = "";
        Boolean DassociateFlag = false;
        string sz_dassociate_caseno = "";
        string sz_session_caseno = "";


        try
        {


            CaseDetailsBO _objDCDB = new CaseDetailsBO();
            DataSet dsAssociateCases = new DataSet();
            dsAssociateCases = _objDCDB.GetAssociateCases(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "LIST");

            if (string.IsNullOrEmpty(szCaseNos))
                throw new FormatException();

            if (szCaseNos.IndexOf(',') == -1)
                szCaseNos += ",";

            string szInsuranceAddress = "";
            if (!txtInsuranceAddress.Text.Equals(""))
                szInsuranceAddress = lstInsuranceCompanyAddress.Text;

            string[] szCaseNo = szCaseNos.Split(',');


            for (int i = 0; i < szCaseNo.Length; i++)
            {
                for (int j = 0; j < dsAssociateCases.Tables[0].Rows.Count; j++)
                {
                    if (szCaseNo[i].ToString().Equals(dsAssociateCases.Tables[0].Rows[j]["SZ_CASE_NO"].ToString()))
                    {
                        DassociateFlag = true;
                        Regex isnumber = new Regex("[^0-9]"); // check  prefix attched or not  if prefix attach remove first to character
                        if (isnumber.IsMatch(szCaseNo[i]))
                        {
                            sz_dassociate_caseno = szCaseNo[i].Remove(0, 2);
                        }
                        else
                        {
                            sz_dassociate_caseno = szCaseNo[i].ToString();
                        }
                        sz_session_caseno = (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO).ToString();
                        if (isnumber.IsMatch(sz_session_caseno)) 
                        {
                            sz_session_caseno = sz_session_caseno.Remove(0, 2);
                        }
                       
                    }

                    if (DassociateFlag)
                    {
                        Patient_TVBO objPatientBO = new Patient_TVBO();
                        objPatientBO.UpdatetoSaveToAllowed(sz_dassociate_caseno, txtCompanyID.Text, sz_session_caseno,"Delete");

                    }

                }
                if (DassociateFlag == false)
                {
                    sznotassociate = sznotassociate + szCaseNo[i].ToString() + ",";
                }
                else
                {
                    DassociateFlag = false;
                }
            }
            //LoadDataOnPage();
            GetAssociateCases();

        }
        catch (FormatException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        finally
        {
            if (string.IsNullOrEmpty(szMsg))
                if (sznotassociate.Equals("") || sznotassociate.Equals(","))
                {
                    szMsg = "The  case no�s successfully DeAssociate.";
                    usrMessage.PutMessage(szMsg);
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                }
                else
                {
                    szMsg = sznotassociate + " not DeAssociate";
                    usrMessage.PutMessage(szMsg);
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
                    usrMessage.Show();
                }

            //lblMsg.Visible = true;
            //lblMsg.Text = szMsg;


        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected void txtInsuranceCompany_TextChanged(object sender, EventArgs args)
    {
        
        string strInsuranceID = hdninsurancecode.Value;
        if (txtInsuranceCompany.Text != "")
        {
            if (strInsuranceID != "0")
            {
                try
                {
                    ClearInsurancecontrol();
                    _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(hdninsurancecode.Value);
                    lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    lstInsuranceCompanyAddress.DataValueField = "CODE";
                    lstInsuranceCompanyAddress.DataBind();
                    Page.MaintainScrollPositionOnPostBack = true;
                    tabcontainerPatientEntry.ActiveTabIndex = 2;
                }
                catch (Exception ex)
                {
                    usrMessage.PutMessage(ex.ToString());
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    usrMessage.Show();
                }
            }
            else
            {
                lstInsuranceCompanyAddress.Items.Clear();
                hdninsurancecode.Value = "";
            }
        }
        else
        {
            lstInsuranceCompanyAddress.Items.Clear();
            txtInsuranceAddress.Text = "";
            txtInsuranceCity.Text = "";
            txtInsuranceState.Text = "";
            txtInsuranceZip.Text = "";
            txtInsuranceStreet.Text = "";
            txtInsPhone.Text = "";
            hdninsurancecode.Value = "";
            //Page.MaintainScrollPositionOnPostBack = true;
        }
    }

}




