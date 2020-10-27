/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_SearchCaseCheckIn.aspx.cs
/*Purpose              :       To Search Case for Check in
/*Author               :       Manoj c
/*Date of creation     :       27 Jan 2010
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
using System.Data;
using System.Data.SqlClient;
using log4net;
using Componend;

public partial class Bill_Sys_SearchCaseCheckIn : PageBase
{
    ListOperation _listOperation;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    Bill_Sys_BillTransaction_BO _billTransactionBO;
    Bill_Sys_Case _bill_Sys_Case;
    CaseDetailsBO _caseDetailsBO;
    private static ILog log = LogManager.GetLogger("Bill_Sys_SearchCaseCheckIn");
    Bill_Sys_CheckinBO objCheckinBO;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _billTransactionBO = new Bill_Sys_BillTransaction_BO();

        try
        { 
          
            
            
            // Code added for autocomplete.

            String scriptFunc = "";
            String scriptFunc2 = "";

            scriptFunc = "<script language=JavaScript> " + "function autoComplete (field, select, property, forcematch) {" + "var found = false;" + "for (var i = 0; i < select.options.length; i++) {" + "if (select.options[i][property].toUpperCase().indexOf(field.value.toUpperCase()) == 0) {" + "		found=true; break;" + "}" + "}" + "if (found) { " + "select.selectedIndex = i; " + "}else {" + "select.selectedIndex = -1;" + "}" + "if (field.createTextRange) {" + "if (forcematch && !found) {" + "field.value=field.value.substring(0,field.value.length-1); " + "return;" + "}" + "var cursorKeys ='8;46;37;38;39;40;33;34;35;36;45;';" + "if (cursorKeys.indexOf(event.keyCode+';') == -1) {" + "var r1 = field.createTextRange();" + "var oldValue = r1.text;" + "var newValue = found ? select.options[i][property] : oldValue;" + "if (newValue != field.value) {" + "field.value = newValue;" + "var rNew = field.createTextRange();" + "rNew.moveStart('character', oldValue.length) ;" + "rNew.select();" + "}" + "}" + "}" + "} </script>";

            RegisterClientScriptBlock("ClientScript", scriptFunc);

           // txtProviderName.Attributes.Add("onKeyUp", "autoComplete(this,this.form._ctl0_ContentPlaceHolder1_extddlProvider,'text',true);");
            txtPatientName.Attributes.Add("onKeyUp", "autoComplete(this,this.form._ctl0_ContentPlaceHolder1_extddlPatient,'text',true);");
            btnCheckinSave.Attributes.Add("onclick", "return checkValidate();");
           // txtCaseType.Attributes.Add("onKeyUp", "autoComplete(this,this.form._ctl0_ContentPlaceHolder1_extddlCaseType,'text',true);");
            txtInsuranceCompany.Attributes.Add("onKeyUp", "autoComplete(this,this.form._ctl0_ContentPlaceHolder1_extddlInsurance,'text',true);");
            //txtCaseStatus.Attributes.Add("onKeyUp", "autoComplete(this,this.form._ctl0_ContentPlaceHolder1_extddlCaseStatus,'text',true);");

            //  End

            
            //btnDelete.Attributes.Add("onclick", "return confirm_delete();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlPatient.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
            extddlInsurance.Flag_ID = txtCompanyID.Text.ToString();
            extddlVisitType.Flag_ID = txtCompanyID.Text.ToString();
            txtVisitDate.Text=DateTime.Today.ToString("MM/dd/yyyy");
            
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                extddlOffice.Flag_ID = txtCompanyID.Text.ToString();
                lblOffice.Visible = true;
                extddlOffice.Visible = true;
                lblChartSearch.Visible = false;
                txtChartnoSearch.Visible = false;
            }

            if ((((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == false) && ((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
            {
                lblChartSearch.Visible = true;
                txtChartnoSearch.Visible = true;
            }
            else
            {
                lblChartSearch.Visible = false;
                txtChartnoSearch.Visible = false;
            }



           
           // extddlProvider.Flag_ID = txtCompanyID.Text.ToString();
            if (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE== "USR00003")
            {
               // extddlProvider.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_PROVIDER_ID;
               // extddlProvider.Enabled = false;
            }           
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
            if (!IsPostBack)
            {
                SearchList();
                BindDoctorList();
                //lnkShowPaidBills.Text = lnkShowPaidBills.Text + "(" + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_PAID_LIST_COUNT", txtCompanyID.Text) + ")";
                //lnkUnpaidCases.Text = lnkUnpaidCases.Text + "(" + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_UNPAID_LIST_COUNT", txtCompanyID.Text) + ")";
                //lnkWriteOffDesk.Text = lnkWriteOffDesk.Text + "(" + _billTransactionBO.GetCaseCount("SP_LITIGATION_WRITEOFF_DESK", "GET_WRITEOFF_COUNT", txtCompanyID.Text) + ")";
                //lnkLitigationDesk.Text = lnkLitigationDesk.Text + "(" + _billTransactionBO.GetCaseCount("SP_LITIGATION_WRITEOFF_DESK", "GET_LETIGATION_COUNT", txtCompanyID.Text) + ")";
                //lnkReferalReminders.Text = lnkReferalReminders.Text + "(" + _billTransactionBO.GetCaseCount("SP_GET_REMINDER_LIST", "GET_REFERAL_COUNT", txtCompanyID.Text) + ")";
                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SOFT_DELETE != "True")
                {
                    grdCaseMaster.Columns[36].Visible = false;
                    btnSoftDelete.Visible = false;
                }
            }
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SOFT_DELETE != "True")
            {
                grdCaseMaster.Columns[36].Visible = false;
                btnSoftDelete.Visible = false;
            }
            else
            {
                grdCaseMaster.Columns[36].Visible = true;
                btnSoftDelete.Visible = true;
            }

            
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - Page_Load : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("Bill_Sys_SearchCase. Method - Page_Load : " + ex.InnerException.Message.ToString());
                log.Debug("Bill_Sys_SearchCase. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
            }
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
            cv.MakeReadOnlyPage("Bill_Sys_SearchCaseCheckIn.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void BindDoctorList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            objCheckinBO = new Bill_Sys_CheckinBO();
            lbDoctor.DataSource = objCheckinBO.getDoctorList(txtCompanyID.Text);
            lbDoctor.DataTextField = "DESCRIPTION";
            lbDoctor.DataValueField = "CODE";
            lbDoctor.DataBind();
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - BindDoctorList : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - BindDoctorList : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("Bill_Sys_SearchCase. Method - BindDoctorList : " + ex.InnerException.Message.ToString());
                log.Debug("Bill_Sys_SearchCase. Method - BindDoctorList : " + ex.InnerException.StackTrace.ToString());
            }
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

    #region "Event Handler" 

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdCaseMaster.CurrentPageIndex = 0;
            SearchList();
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - btnSearch_Click : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - btnSearch_Click : " + ex.StackTrace.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - btnSearch_Click : " + ex.InnerException.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - btnSearch_Click : " + ex.InnerException.StackTrace.ToString());
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

    protected void btnQuickSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtChartSearch.Text = txtChartnoSearch.Text;
            txtCaseIDSearch.Text = txtCIDSearch.Text;
            txtPatientFNameSearch.Text = txtFNameSearch.Text;
            txtPatientLNameSearch.Text = txtLNameSearch.Text;
            SearchList();
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - btnSearch_Click : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - btnSearch_Click : " + ex.StackTrace.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - btnSearch_Click : " + ex.InnerException.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - btnSearch_Click : " + ex.InnerException.StackTrace.ToString());
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

    protected void grdCaseMaster_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "CaseSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                SearchList();
            }
            if (e.CommandName.ToString() == "ChartNoSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                SearchList();
            }
            if (e.CommandName.ToString() == "PatientSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                SearchList();
            }
            if (e.CommandName.ToString() == "DOASearch")
            {
                if (txtSearchOrder.Text == "cast(" + e.CommandArgument + " as datetime) ASC")
                {
                    txtSearchOrder.Text = "cast(" + e.CommandArgument + " as datetime) DESC";
                }
                else
                {
                    txtSearchOrder.Text = "cast(" + e.CommandArgument + " as datetime) ASC";
                }
                SearchList();
            }
            if (e.CommandName.ToString() == "CASETYPESearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                SearchList();
            }
            if (e.CommandName.ToString() == "DOOSearch")
            {
                if (txtSearchOrder.Text == "cast(" + e.CommandArgument + " as datetime) ASC")
                {
                    txtSearchOrder.Text = "cast(" + e.CommandArgument + " as datetime) DESC";
                }
                else
                {
                    txtSearchOrder.Text = "cast(" + e.CommandArgument + " as datetime) ASC";
                }
                SearchList();
            }
            if (e.CommandName.ToString() == "Select")
            {
                
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID= _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[34].Text;
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = e.Item.Cells[31].Text;

                _bill_Sys_CaseObject.SZ_CASE_NO = ((LinkButton)e.Item.FindControl("lnkSelectCase")).Text;

                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                  
                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID!= e.Item.Cells[28].Text)
                {
                    Response.Redirect("Bill_Sys_ReCaseDetails.aspx", false);
                }
                else
                {
                    Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
                }
            }

            else if (e.CommandName.ToString() == "Bill Transaction")
            {
                
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[34].Text;
                _bill_Sys_CaseObject.SZ_CASE_NO = ((LinkButton)e.Item.Cells[1].FindControl("lnkSelectCase")).Text;
                 
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;

                Response.Redirect("Bill_Sys_BillTransaction.aspx", false);
            }
            else if (e.CommandName.ToString() == "View Bills")
            {
                
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[34].Text;
                _bill_Sys_CaseObject.SZ_CASE_NO = ((LinkButton)e.Item.Cells[1].FindControl("lnkSelectCase")).Text;

                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;

                Response.Redirect("Bill_Sys_BillSearch.aspx?fromCase=true", false);
            }
            else if (e.CommandName.ToString() == "Document Manager")
            {
                // Create Session for document Manager
                Session["PassedCaseID"] = e.CommandArgument;
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
            //    Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
                Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");
            }
            else if (e.CommandName.ToString() == "Calender Event")
            {
                 
                LinkButton lnkPatient =(LinkButton ) e.Item.Cells[0].FindControl("lnkSelectCase");
                Session["SZ_CASE_ID"] = lnkPatient.CommandArgument;
                Session["PROVIDERNAME"] = e.CommandArgument;

                Response.Redirect("Bill_Sys_ScheduleEvent.aspx", false);
                //Response.Redirect("Bill_Sys_CalendarEvent.aspx", false);
            //    string szTemplateURL="Bill_Sys_TemplateManager.aspx";
                //string szTemplateURL = "TemplateManager/Bill_Sys_GeneratePDF.aspx";
                //Session["PassedCaseID"] = e.CommandArgument;
                //Response.Write("<script language='javascript'>window.open('" + szTemplateURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
            }
            else if (e.CommandName.ToString() == "Out Calender Event")
            {

                LinkButton lnkPatient = (LinkButton)e.Item.Cells[0].FindControl("lnkOutScheduleCalendarEvent");
                Session["SZ_CASE_ID"] = lnkPatient.CommandArgument;
                Session["PROVIDERNAME"] = e.CommandArgument;
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Response.Redirect("Bill_Sys_AppointPatientEntry.aspx?Flag=true", false);
                //Response.Redirect("Bill_Sys_CalendarEvent.aspx", false);
                //    string szTemplateURL="Bill_Sys_TemplateManager.aspx";
                //string szTemplateURL = "TemplateManager/Bill_Sys_GeneratePDF.aspx";
                //Session["PassedCaseID"] = e.CommandArgument;
                //Response.Write("<script language='javascript'>window.open('" + szTemplateURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
            }

            else if (e.CommandName.ToString() == "Template Manager")
            {
                Session["TM_SZ_CASE_ID"] = e.CommandArgument;
         //       Session["PROVIDERNAME"] = e.Item.Cells[4].Text;
         //       Response.Redirect("TemplateManager/templates.aspx", false);

                String szURL = "";
                szURL = "TemplateManager/templates.aspx";


          //      Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");

                Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");

                //    string szTemplateURL="Bill_Sys_TemplateManager.aspx";
                //string szTemplateURL = "TemplateManager/Bill_Sys_GeneratePDF.aspx";
                //Session["PassedCaseID"] = e.CommandArgument;
                //Response.Write("<script language='javascript'>window.open('" + szTemplateURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
            }
            
            else if (e.CommandName.ToString() == "Patient Desk")
            {

                LinkButton lnkPatient = (LinkButton)e.Item.Cells[0].FindControl("lnkPatientDesk");
                Session["SZ_CASE_ID"] = lnkPatient.CommandArgument;
                Session["PROVIDERNAME"] = e.CommandArgument;
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
              
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[34].Text;
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = e.Item.Cells[31].Text;

                _bill_Sys_CaseObject.SZ_CASE_NO = ((LinkButton)e.Item.FindControl("lnkSelectCase")).Text;

                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;
                /////////////

                Response.Redirect("Bill_SysPatientDesk.aspx?Flag=true", false);
                
            }
            else if (e.CommandName.ToString() == "Check in")
            {
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                LinkButton lnktempCheckin = (LinkButton)e.Item.Cells[0].FindControl("lnkCheckin");
                Session["CHECK_IN_CASE_ID"] = lnktempCheckin.CommandArgument;
                Session["CHECK_IN_PATIENT_ID"] = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                extddlVisitType.Text = "NA";
                lblCheckinMsg.Text = "";
                for (int i = 0; i < lbDoctor.Items.Count; i++)
                {
                    lbDoctor.Items[i].Selected = false;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "javascript:showCheckinPopup();", true);
            }
         }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.StackTrace.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.InnerException.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemCommand : " + ex.InnerException.StackTrace.ToString());
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

    protected void grdCaseMaster_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdCaseMaster.CurrentPageIndex = e.NewPageIndex;
            SearchList();
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_PageIndexChanged : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_PageIndexChanged : " + ex.StackTrace.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_PageIndexChanged : " + ex.InnerException.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_PageIndexChanged : " + ex.InnerException.StackTrace.ToString());
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
            
            txtClaimNumber.Text = "";
            txtDateofAccident.Text = "";
            txtPatientName.Text = "";
            //txtProviderName.Text = "";
            //txtCaseStatus.Text = "";
            txtInsuranceCompany.Text = "";
            //txtCaseType.Text = "";
           // txtCaseID.Text = "";
           // txtCaseName.Text = "";
            txtSSNNo.Text = "";
            extddlCaseStatus.Text = "NA";
            extddlCaseType.Text = "NA";
            extddlInsurance.Text = "NA";
            extddlPatient.Text = "NA";
            if (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE == "USR00003")
            {
                //extddlProvider.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_PROVIDER_ID;
                //extddlProvider.Enabled = false;
                //extddlProvider.Visible = false;
            }
            else
            {
                //extddlProvider.Text = "NA";
            }

          
            grdCaseMaster.CurrentPageIndex = 0;
            SearchList();

        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - btnClear_Click : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - btnClear_Click : " + ex.StackTrace.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - btnClear_Click : " + ex.InnerException.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - btnClear_Click : " + ex.InnerException.StackTrace.ToString());
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

    protected void lnkShowPaidBills_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            grdCaseMaster.Visible = false;
           // grdSearchpaidBills.Visible = true;
            //grdSearchUnpaidCaseList.Visible = false;
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

    protected void lnkUnpaidCases_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            grdCaseMaster.Visible = false;
            //grdSearchpaidBills.Visible = false;
            //grdSearchUnpaidCaseList.Visible = true;
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

    protected void lnkLitigationDesk_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {


            Response.Redirect("Bill_Sys_LitigationDesk.aspx", false);
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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

    protected void lnkWriteOffDesk_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            Response.Redirect("Bill_Sys_WriteOffDesk.aspx", false);
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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

    protected void lnkReferalManagement_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            Response.Redirect("Bill_Sys_ReferalManagement.aspx", false);
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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
    protected void lnkReferalReminders_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            Response.Redirect("Bill_Sys_ReferalReminders.aspx", false);
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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

    protected void grdSearchUnpaidCaseList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           // grdSearchUnpaidCaseList.CurrentPageIndex = e.NewPageIndex;
            SearchUnPaidCaseList();
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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

    



    protected void grdSearchpaidBills_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           // grdSearchpaidBills.CurrentPageIndex = e.NewPageIndex;
            searchpaidcaselist();
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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
    protected void grdSearchpaidBills_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Select")
            {
                Session["PassedCaseID"] = e.CommandArgument;
                Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
            }

            else if (e.CommandName.ToString() == "Bill Transaction")
            {
                Session["PassedCaseID"] = e.CommandArgument;
                Response.Redirect("Bill_Sys_BillTransaction.aspx", false);
            }
            else if (e.CommandName.ToString() == "View Bills")
            {
                Session["PassedCaseID"] = e.CommandArgument;
                Response.Redirect("Bill_Sys_BillSearch.aspx", false);
            }
            else if (e.CommandName.ToString() == "Document Manager")
            {
                // Create Session for document Manager
                Session["PassedCaseID"] = e.CommandArgument;
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
           //     Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
                Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");
            }
            else if (e.CommandName.ToString() == "Template Manager")
            {
                Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                //       Session["PROVIDERNAME"] = e.Item.Cells[4].Text;
                //       Response.Redirect("TemplateManager/templates.aspx", false);

                String szURL = "";
                szURL = "TemplateManager/templates.aspx";


            //    Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");

                Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");

                //    string szTemplateURL="Bill_Sys_TemplateManager.aspx";
                //string szTemplateURL = "TemplateManager/Bill_Sys_GeneratePDF.aspx";
                //Session["PassedCaseID"] = e.CommandArgument;
                //Response.Write("<script language='javascript'>window.open('" + szTemplateURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
            }
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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
    protected void grdSearchUnpaidCaseList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Select")
            {
                Session["PassedCaseID"] = e.CommandArgument;
                Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
            }

            else if (e.CommandName.ToString() == "Bill Transaction")
            {
                Session["PassedCaseID"] = e.CommandArgument;
                Response.Redirect("Bill_Sys_BillTransaction.aspx", false);
            }
            else if (e.CommandName.ToString() == "View Bills")
            {
                Session["PassedCaseID"] = e.CommandArgument;
                Response.Redirect("Bill_Sys_BillSearch.aspx", false);
            }
            else if (e.CommandName.ToString() == "Document Manager")
            {
                // Create Session for document Manager
                Session["PassedCaseID"] = e.CommandArgument;
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
              //  Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
                Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");
            }
            else if (e.CommandName.ToString() == "Template Manager")
            {
                Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                //       Session["PROVIDERNAME"] = e.Item.Cells[4].Text;
                //       Response.Redirect("TemplateManager/templates.aspx", false);

                String szURL = "";
                szURL = "TemplateManager/templates.aspx";


          //      Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");

                Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");

                //    string szTemplateURL="Bill_Sys_TemplateManager.aspx";
                //string szTemplateURL = "TemplateManager/Bill_Sys_GeneratePDF.aspx";
                //Session["PassedCaseID"] = e.CommandArgument;
                //Response.Write("<script language='javascript'>window.open('" + szTemplateURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
            }
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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
    #region "Fetch Method"

    private void SearchList()
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
            _listOperation.Xml_File = "SearchCase.xml";
            _listOperation.LoadList();


            
            if (Request.QueryString["Type"] != null)
            {
                if (Request.QueryString["Type"].ToString() == "Quick")
                {
                    grdCaseMaster.Columns[0].Visible = true;
                    grdCaseMaster.Columns[1].Visible = true;
                    grdCaseMaster.Columns[2].Visible = false;
                    grdCaseMaster.Columns[4].Visible = false;
                    grdCaseMaster.Columns[5].Visible = true;
                    grdCaseMaster.Columns[6].Visible = false;
                    grdCaseMaster.Columns[10].Visible = true;
                    grdCaseMaster.Columns[11].Visible = false;
                    grdCaseMaster.Columns[12].Visible = false;
                    grdCaseMaster.Columns[13].Visible = false;
                    grdCaseMaster.Columns[14].Visible = false;
                    grdCaseMaster.Columns[15].Visible = false;
                    grdCaseMaster.Columns[16].Visible = false;
                    grdCaseMaster.Columns[27].Visible = false;
                    grdCaseMaster.Columns[28].Visible = false;
                    grdCaseMaster.Columns[29].Visible = true;
                    grdCaseMaster.Columns[32].Visible = false;
                    //grdCaseMaster.Columns[15].Visible = false;
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == false)
                    {
                        grdCaseMaster.Columns[0].Visible = false;
                        grdCaseMaster.Columns[32].Visible = false;
                        
                        //grdCaseMaster.Columns[16].Visible = true;
                        //grdCaseMaster.Columns[17].Visible = true;
                       // grdCaseMaster.Columns[18].Visible = true;
                    }
                    else
                    {
                        //grdCaseMaster.Columns[26].Visible = false;
                        //grdCaseMaster.Columns[27].Visible = false;
                    }
                    //grdCaseMaster.Columns[28].Visible = true;
                }
                else
                {
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        grdCaseMaster.Columns[26].Visible = false;
                        grdCaseMaster.Columns[27].Visible = false;
                        grdCaseMaster.Columns[28].Visible = false;
                        grdCaseMaster.Columns[32].Visible = true;

                    }
                    else
                    {

                        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1")
                        {
                            grdCaseMaster.Columns[4].Visible = false;
                        }
                        else
                        {
                            grdCaseMaster.Columns[4].Visible = true;
                        }
                        grdCaseMaster.Columns[32].Visible = false;
                    }
                }
            }
            else
            {
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    grdCaseMaster.Columns[26].Visible = false;
                    grdCaseMaster.Columns[27].Visible = false;
                    grdCaseMaster.Columns[28].Visible = false;
                    grdCaseMaster.Columns[32].Visible = true;
                }
                else
                {
                    grdCaseMaster.Columns[32].Visible = false;
                }
            }

            if (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.Contains("ADMIN") ==false)
            {
                grdCaseMaster.Columns[13].Visible = false;               
                grdCaseMaster.Columns[15].Visible = false;
                LinkButton lnk = new LinkButton();
                for (int i = 0; i < grdCaseMaster.Items.Count; i++)
                {
                    lnk = (LinkButton)grdCaseMaster.Items[i].Cells[14].FindControl("lnkBillTransaction");
                    lnk.Visible = false;
                } 
                //btnDelete.Visible = false;
            }

            foreach (DataGridItem grditem in grdCaseMaster.Items)
            {
                if (grditem.Cells[35].Text == "True")
                {
                    grditem.Cells[0].Style.Add("text-decoration", "line-through");
                    grditem.Cells[1].Style.Add("text-decoration", "line-through");
                    grditem.Cells[2].Style.Add("text-decoration", "line-through");
                    grditem.Cells[3].Style.Add("text-decoration", "line-through");
                    grditem.Cells[4].Style.Add("text-decoration", "line-through");
                    grditem.Cells[5].Style.Add("text-decoration", "line-through");
                    grditem.Cells[6].Style.Add("text-decoration", "line-through");
                    grditem.Cells[7].Style.Add("text-decoration", "line-through");
                    grditem.Cells[8].Style.Add("text-decoration", "line-through");
                    grditem.Cells[9].Style.Add("text-decoration", "line-through");
                    grditem.Cells[10].Style.Add("text-decoration", "line-through");
                    grditem.Cells[11].Style.Add("text-decoration", "line-through");
                    grditem.Cells[12].Style.Add("text-decoration", "line-through");
                    grditem.Cells[13].Style.Add("text-decoration", "line-through");
                    grditem.Cells[14].Style.Add("text-decoration", "line-through");
                    grditem.Cells[15].Style.Add("text-decoration", "line-through");
                    grditem.Cells[16].Style.Add("text-decoration", "line-through");
                    grditem.Cells[17].Style.Add("text-decoration", "line-through");
                    grditem.Cells[18].Style.Add("text-decoration", "line-through");
                    grditem.Cells[19].Style.Add("text-decoration", "line-through");
                    grditem.Cells[20].Style.Add("text-decoration", "line-through");
                    grditem.Cells[21].Style.Add("text-decoration", "line-through");
                    grditem.Cells[22].Style.Add("text-decoration", "line-through");
                    grditem.Cells[23].Style.Add("text-decoration", "line-through");
                    grditem.Cells[24].Style.Add("text-decoration", "line-through");
                    grditem.Cells[25].Style.Add("text-decoration", "line-through");
                    grditem.Cells[26].Style.Add("text-decoration", "line-through");
                    grditem.Cells[27].Style.Add("text-decoration", "line-through");
                    grditem.Cells[28].Style.Add("text-decoration", "line-through");
                    grditem.Cells[29].Style.Add("text-decoration", "line-through");
                    grditem.Cells[30].Style.Add("text-decoration", "line-through");
                    grditem.Cells[31].Style.Add("text-decoration", "line-through");
                    grditem.Cells[32].Style.Add("text-decoration", "line-through");
                    grditem.Cells[33].Style.Add("text-decoration", "line-through");
                    grditem.Cells[34].Style.Add("text-decoration", "line-through");
                    grditem.Cells[35].Style.Add("text-decoration", "line-through");                  
                }
            }
            searchpaidcaselist();
            SearchUnPaidCaseList();

            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_NEW_BILL != "True" && ((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_VIEW_BILL != "True")
            {
                grdCaseMaster.Columns[29].Visible = false;                        
            }
            
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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
    private void searchpaidcaselist()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
           // grdSearchpaidBills.DataSource = _bill_Sys_BillingCompanyDetails_BO.SearchBills("GETPAIDLIST", txtCompanyID.Text);
            //grdSearchpaidBills.DataBind();
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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
    private void SearchUnPaidCaseList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
           // grdSearchUnpaidCaseList.DataSource = _bill_Sys_BillingCompanyDetails_BO.SearchBills("GETUNPAIDLIST", txtCompanyID.Text);
           // grdSearchUnpaidCaseList.DataBind();
        }
        catch (Exception ex)
        {
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.StackTrace.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.Message.ToString());
            log.Debug("Shared_MasterPage. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
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
    
  
    protected void extddlProvider_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        _caseDetailsBO = new CaseDetailsBO();
       // txtProviderName.Text = _caseDetailsBO.GetProviderName(extddlProvider.Text);
    }
   
    protected void extddlInsurance_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        _caseDetailsBO = new CaseDetailsBO();
        txtInsuranceCompany.Text = _caseDetailsBO.GetInsuranceCompanyName(extddlInsurance.Text);
    }

    protected void extddlPatient_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        _caseDetailsBO = new CaseDetailsBO();
        txtPatientName.Text = _caseDetailsBO.GetPatientName(extddlPatient.Text);
    }


    protected void grdCaseMaster_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {         

            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
              
            }
            else
            {
                e.Item.Cells[0].Visible = false;
            }

            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_NEW_BILL != "True")
            {              
                LinkButton lnkNew = (LinkButton)e.Item.FindControl("lnkBillTransaction");
                if(lnkNew!=null)
                lnkNew.Enabled = false;                           
            }
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_VIEW_BILL != "True")
            {
                LinkButton lnkview = (LinkButton)e.Item.FindControl("lnkViewBills");
                if (lnkview != null)
                lnkview.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemDataBound : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemDataBound : " + ex.StackTrace.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemDataBound : " + ex.InnerException.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemDataBound : " + ex.InnerException.StackTrace.ToString());
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Response.Write("Hello");
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
    protected void btnSoftDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _caseDetailsBO = new CaseDetailsBO();
        try
        {
             
            foreach (DataGridItem grdItem in grdCaseMaster.Items )
            {
                CheckBox chkDelete = (CheckBox)grdItem.FindControl("chkSoftDelete");
                if(chkDelete.Checked)
                {
                    LinkButton lnk =(LinkButton) grdItem.FindControl("lnkSelectCase");
                    _caseDetailsBO.SoftDelete(lnk.CommandArgument.ToString(), txtCompanyID.Text, true);
                }
            }
            SearchList();
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemDataBound : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemDataBound : " + ex.StackTrace.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemDataBound : " + ex.InnerException.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - grdCaseMaster_ItemDataBound : " + ex.InnerException.StackTrace.ToString());
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


    #region "Check in Screen"


    
    protected void btnCheckinSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int iDateCount = 4;
        String szCompletedMsg = "";
        int iCompleted = 0;
        String szFutureUncompletedMsg = "";
        int iFutureUncompleted = 0;
        String szMustExist_Initial_EvaluationMsg = "";
        int iMustExist_Initial_Evaluation = 0;
        String szAlreadyExistInitial_Evaluation = "";
        int iAlreadyExistInitial_Evaluation = 0;
        String szAlreadyExistVisit = "";
        int iAlreadyExistVisit = 0;
        Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
        ArrayList objAdd = new ArrayList();
        lblCheckinMsg.Text = "";
        try
        {
            for (int i = 0; i < lbDoctor.Items.Count; i++)
            {
                if (lbDoctor.Items[i].Selected == true)
                {

                    #region "Check for Visit"

                    Boolean iEvisitExists = false;
                    Boolean visitExists = false;
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings.Get("MyConnectionString"));
                    SqlCommand comd = new SqlCommand("SP_CHECK_INITIALE_VALUATIONEXISTS");
                    comd.CommandType = CommandType.StoredProcedure;
                    comd.Connection = con;
                    comd.Connection.Open();
                    comd.Parameters.AddWithValue("@SZ_CASE_ID", Session["CHECK_IN_CASE_ID"].ToString());
                    comd.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
                    comd.Parameters.AddWithValue("@SZ_PATIENT_ID", Session["CHECK_IN_PATIENT_ID"].ToString());
                    comd.Parameters.AddWithValue("@SZ_DOCTOR_ID", lbDoctor.Items[i].Value);
                    comd.Parameters.AddWithValue("@VISIT_DATE", txtVisitDate.Text);
                    //int intCountVisits = Convert.ToInt32(comd.ExecuteScalar());

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


                    if (iEvisitExists == false && extddlVisitType.Selected_Text != "IE")
                    {
                        if (iMustExist_Initial_Evaluation == iDateCount)
                        {
                            szMustExist_Initial_EvaluationMsg = szMustExist_Initial_EvaluationMsg + lbDoctor.Items[i].Text + " , <br/>";
                            iMustExist_Initial_Evaluation = 0;
                        }
                        else
                        {
                            szMustExist_Initial_EvaluationMsg = szMustExist_Initial_EvaluationMsg + lbDoctor.Items[i].Text + " , ";
                            iMustExist_Initial_Evaluation++;
                        }
                        continue;
                    }
                    if (iEvisitExists == true && extddlVisitType.Selected_Text == "IE")
                    {
                        if (iAlreadyExistInitial_Evaluation == iDateCount)
                        {
                            szAlreadyExistInitial_Evaluation = szAlreadyExistInitial_Evaluation + lbDoctor.Items[i].Text + " , <br/>";
                            iAlreadyExistInitial_Evaluation = 0;
                        }
                        else
                        {
                            szAlreadyExistInitial_Evaluation = szAlreadyExistInitial_Evaluation + lbDoctor.Items[i].Text + " , ";
                            iAlreadyExistInitial_Evaluation++;
                        }
                        continue;
                    }
                    if (visitExists == true)
                    {
                        if (iAlreadyExistVisit == iDateCount)
                        {
                            szAlreadyExistVisit = szAlreadyExistVisit + lbDoctor.Items[i].Text + " , <br/>";
                            iAlreadyExistVisit = 0;
                        }
                        else
                        {
                            szAlreadyExistVisit = szAlreadyExistVisit + lbDoctor.Items[i].Text + " , ";
                            iAlreadyExistVisit++;
                        }
                        continue;
                    }



                    #endregion

                    ArrayList objAL = new ArrayList();
                    objAL.Add(Session["CHECK_IN_CASE_ID"].ToString());
                    objAL.Add(Session["CHECK_IN_PATIENT_ID"].ToString());
                    objAL.Add(lbDoctor.Items[i].Value);
                    objAL.Add(txtVisitDate.Text);
                    objAL.Add(extddlVisitType.Text);
                    objAL.Add(txtCompanyID.Text);
                    objCheckinBO = new Bill_Sys_CheckinBO();
                  //  objCheckinBO.saveCheckinInformation(objAL);
                    szCompletedMsg = szCompletedMsg + lbDoctor.Items[i].Text + " , ";
                }
            }

            if (szMustExist_Initial_EvaluationMsg == "" && szAlreadyExistInitial_Evaluation == "" && szAlreadyExistVisit == "")
            {
                lblCheckinMsg.Text = "Appointment scheduled successfully.";
            }
            else
            {
                if (szCompletedMsg != "")
                {
                    lblCheckinMsg.Text = lblCheckinMsg.Text + szCompletedMsg + " -- Completed.<br/>";
                }

                if (szMustExist_Initial_EvaluationMsg != "")
                {
                    if (iMustExist_Initial_Evaluation > 2)
                    {
                        lblCheckinMsg.Text = lblCheckinMsg.Text + szMustExist_Initial_EvaluationMsg + "<br/> -- Schedule can not be saved patient is visiting first time hence there visit type should be Initial Evaluation.<br/>";
                    }
                    else
                    {
                        lblCheckinMsg.Text = lblCheckinMsg.Text + szMustExist_Initial_EvaluationMsg + " -- Schedule can not be saved patient is visiting first time hence there visit type should be Initial Evaluation.<br/>";
                    }
                }
                if (szAlreadyExistInitial_Evaluation != "")
                {
                    lblCheckinMsg.Text = lblCheckinMsg.Text + szAlreadyExistInitial_Evaluation + " -- Schedule can not be saved because patient already has Initial Evaluation.<br/>";
                }
                if (szAlreadyExistVisit != "")
                {
                    lblCheckinMsg.Text = lblCheckinMsg.Text + szAlreadyExistVisit + " -- Schedule can not be saved because patient already has this visit.<br/>";
                }
            }
            lblCheckinMsg.Focus();
            lblCheckinMsg.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "javascript:showCheckinPopup();", true);
            SearchList();
            extddlVisitType.Text = "NA";
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_SearchCase. Method - btnCheckinSave_Click : " + ex.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - btnCheckinSave_Click : " + ex.StackTrace.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - btnCheckinSave_Click : " + ex.InnerException.Message.ToString());
            log.Debug("Bill_Sys_SearchCase. Method - btnCheckinSave_Click : " + ex.InnerException.StackTrace.ToString());
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
}
