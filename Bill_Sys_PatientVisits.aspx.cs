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

public partial class Bill_Sys_PatientVisits : PageBase
{
    
    private Bill_Sys_TreatmentBO _treatmentBO;
    
    private ArrayList _arrayList;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;           
            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlViewDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            btnVisitSave.Attributes.Add("onclick", "return formValidator('frmPatientVisit','extddlDoctor,txtVisitDate');");
            if (!IsPostBack)
            {
                if (Request.QueryString["PatientID"] != null)
                {
                    if (Request.QueryString["PatientID"].ToString() != "")
                    {
                        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                        Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                        _bill_Sys_CaseObject.SZ_CASE_ID = _caseDetailsBO.GetCasePatientID("", Request.QueryString["PatientID"].ToString());
                        _bill_Sys_CaseObject.SZ_PATIENT_ID = Request.QueryString["PatientID"].ToString();
                        _bill_Sys_CaseObject.SZ_COMAPNY_ID = Request.QueryString["companyId"].ToString();
                        Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                    }
                }

                if (Session["CASE_OBJECT"] != null)
                {
                    txtPatientID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID;
                    //////////////////////
                    //CREATE SESSION FOR DOC MANAGER,TEMPLATE MANAGER,Notes,Bills

                    Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                    _bill_Sys_Case.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;

                    Session["CASEINFO"] = _bill_Sys_Case;


                    String szURL = "";
                    String szCaseID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    Session["QStrCaseID"] = szCaseID;
                    Session["Case_ID"] = szCaseID;
                    Session["Archived"] = "0";
                    Session["QStrCID"] = szCaseID;
                    Session["SelectedID"] = szCaseID;
                    Session["DM_User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    Session["User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    Session["SN"] = "0";
                    Session["LastAction"] = "vb_CaseInformation.aspx";


                    Session["SZ_CASE_ID_NOTES"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;

                    Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    GetPatientDeskList();
                    //
                    ///////////////////
                }
                else
                {
                    Response.Redirect("Bill_Sys_SearchCase.aspx", false);
                }
                
                if (Request.QueryString["DoctorID"] != null)
                {
                    tabcontainerPatientVisit.ActiveTabIndex = 3;
                    extddlDoctor.Text = Request.QueryString["DoctorID"].ToString();
                    extddlViewDoctor.Text = Request.QueryString["DoctorID"].ToString();
                    txtVisitDateFrom.Text = Convert.ToDateTime(Request.QueryString["Date"]).ToShortDateString();
                    GetDoctorVisitList();
                    GetTestList();
                    lstPatientVisits.SelectedValue = Request.QueryString["TypeCode"].ToString();

                }
                BindPatientList();
                Bind_Billed_UnBilled_Count();


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
            cv.MakeReadOnlyPage("Bill_Sys_PatientVisits.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    private void Bind_Billed_UnBilled_Count()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _treatmentBO = new Bill_Sys_TreatmentBO();
        try
        {
            lblTotalBilledCount.Text = _treatmentBO.GetTreatmentListCount(txtCompanyID.Text, txtPatientID.Text, "VISITS", "BILLEDLISTCOUNT");
            lblTotalUnBilledCount.Text = _treatmentBO.GetTreatmentListCount(txtCompanyID.Text, txtPatientID.Text, "VISITS", "UN_BILLEDLISTCOUNT");
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

    
    private void SaveVisitDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _treatmentBO = new Bill_Sys_TreatmentBO();
        
        try
        {
            for (int i = 0; i < lstPatientVisits.Items.Count; i++)
            {
                if (rdoOn.Checked == true)
                {
                    if (lstPatientVisits.Items[i].Selected)
                    {
                        _arrayList = new ArrayList();
                        _arrayList.Add(txtPatientID.Text);
                        _arrayList.Add(extddlDoctor.Text);
                        _arrayList.Add(txtVisitDateFrom.Text);
                        _arrayList.Add(lstPatientVisits.Items[i].Value);
                        _arrayList.Add(txtCompanyID.Text);
                        _treatmentBO.SaveTreatment(_arrayList);
                    }
                }
                else
                {
                    
                    if (lstPatientVisits.Items[i].Selected)
                    {
                        DateTime objFromDate = Convert.ToDateTime(txtVisitDateFrom.Text);
                        DateTime objToDate = Convert.ToDateTime(txtVisitDateTo.Text);
                        while (objFromDate <= objToDate)
                        {

                            _arrayList = new ArrayList();
                            _arrayList.Add(txtPatientID.Text);
                            _arrayList.Add(extddlDoctor.Text);
                            _arrayList.Add(objFromDate);
                            _arrayList.Add(lstPatientVisits.Items[i].Value);
                            _arrayList.Add(txtCompanyID.Text);
                            _treatmentBO.SaveTreatment(_arrayList);
                            objFromDate = objFromDate.AddDays(1);
                        }
                    }
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

    protected void btnVisitSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SaveVisitDetails();
            ClearControl();
           
            BindPatientList();
            Bind_Billed_UnBilled_Count();
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
            extddlDoctor.Text = "NA";            
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

    protected void extddlViewDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            tabcontainerPatientVisit.ActiveTabIndex = 2;
            if (extddlViewDoctor.Text != "NA")
            {
                GetDoctorVisitList();
            }
            else
            {
                lstViewDoctorVisit.Items.Clear();                
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

    private void GetDoctorVisitList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _treatmentBO = new Bill_Sys_TreatmentBO();
        _arrayList = new ArrayList();
        try
        {

            lstViewDoctorVisit.DataSource = _treatmentBO.GetTreatmentList(txtCompanyID.Text, extddlViewDoctor.Text, txtPatientID.Text, "VISITS");
           lstViewDoctorVisit.DataTextField = "DESCRIPTION";
           lstViewDoctorVisit.DataValueField = "CODE";
           lstViewDoctorVisit.DataBind();

           _arrayList = _treatmentBO.GetDoctorTreatmentCount(txtCompanyID.Text, extddlViewDoctor.Text, txtPatientID.Text, "VISITS");
           
           if (_arrayList.Count > 0)
           {
               lblTotalVisits.Text = _arrayList[1].ToString();
               if (_arrayList[0].ToString() != "")
               {
                   lblLastDate.Text = Convert.ToDateTime(_arrayList[0].ToString()).ToShortDateString();
               }
           }
           else
           {
               lblTotalVisits.Text = "";
               lblLastDate.Text = "";
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

    private void BindPatientList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _treatmentBO = new Bill_Sys_TreatmentBO();
        _arrayList = new ArrayList();
        try
        {
            grdScheduledVisits.DataSource = _treatmentBO.GetSceduledTreatmentList(txtCompanyID.Text, txtPatientID.Text, "VISITS");;
            grdScheduledVisits.DataBind();

            grdBilledVisits.DataSource = _treatmentBO.GetBilledTreatmentList(txtCompanyID.Text, txtPatientID.Text, "VISITS");
            grdBilledVisits.DataBind();

            grdPatientLatestVisit.DataSource = _treatmentBO.GetLatestTreatmentList(txtCompanyID.Text, txtPatientID.Text, "VISITS");
            grdPatientLatestVisit.DataBind();

            _arrayList = _treatmentBO.GetLatestDoctorTreatmentCount(txtCompanyID.Text, txtPatientID.Text, "VISITS");
           
            if (_arrayList.Count > 0)
            {
                if (_arrayList[0].ToString() != "")
                {
                    lblPatientLastVisitDate.Text = Convert.ToDateTime(_arrayList[0].ToString()).ToShortDateString();
                }
                lblTotalPatientVisitCount.Text = _arrayList[1].ToString();                
            }

            grdPatientVisitList.DataSource = _treatmentBO.GetSummaryTreatmentList(txtCompanyID.Text, txtPatientID.Text, "VISITS");
            grdPatientVisitList.DataBind();

            string strDName = "";
            for (int i = 0; i < grdBilledVisits.Items.Count; i++)
            {
                Label lblDoc = (Label)grdBilledVisits.Items[i].Cells[3].FindControl("lblDName");
                if (strDName != "" && strDName == lblDoc.Text)
                {
                    lblDoc.Text = "";
                }
                else
                {
                    strDName = lblDoc.Text;
                }
            }

            string strDocName = "";
            for (int i = 0; i < grdPatientLatestVisit.Items.Count; i++)
            {
                Label lblDoc = (Label)grdPatientLatestVisit.Items[i].Cells[3].FindControl("lblDoctorName");
                if (strDocName != "" && strDocName == lblDoc.Text)
                {
                    lblDoc.Text = "";
                }
                else
                {
                    strDocName = lblDoc.Text;
                }
            }

            string strDoctorName = "";

            for (int i = 0; i < grdPatientVisitList.Items.Count; i++)
            {
                Label lblDoctor = (Label)grdPatientVisitList.Items[i].Cells[3].FindControl("lblDocName");
                if (strDoctorName != "" && strDoctorName == lblDoctor.Text)
                {
                    LinkButton lnkAdd = (LinkButton)grdPatientVisitList.Items[i].Cells[3].FindControl("lnkAdd");
                    LinkButton lnkView = (LinkButton)grdPatientVisitList.Items[i].Cells[3].FindControl("lnkView");

                    lnkAdd.Text = "";
                    lnkView.Text = "";
                    lblDoctor.Text  = "";
                }
                else
                {
                    strDoctorName = lblDoctor.Text;
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

    protected void grdPatientVisitList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Add")
            {
                tabcontainerPatientVisit.ActiveTabIndex = 3;
                extddlDoctor.Text = e.CommandArgument.ToString(); 
            }
            else if (e.CommandName.ToString() == "View")
            {
                tabcontainerPatientVisit.ActiveTabIndex = 2;
                extddlViewDoctor.Text = e.CommandArgument.ToString();
                GetDoctorVisitList();
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
 
    protected void extddlDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            tabcontainerPatientVisit.ActiveTabIndex  = 3;
            GetTestList();
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

    private void GetTestList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _treatmentBO = new Bill_Sys_TreatmentBO();
        try
        {
            lstPatientVisits.DataSource = _treatmentBO.GetDoctorProcedureList(txtCompanyID.Text, extddlDoctor.Text, "visits");
            lstPatientVisits.DataTextField = "DESCRIPTION";
            lstPatientVisits.DataValueField = "CODE";
            lstPatientVisits.DataBind();
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

    protected void rdoFromTo_CheckedChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            tabcontainerPatientVisit.ActiveTabIndex = 3;
            txtVisitDateTo.Visible = true;
            lblDateOfService.Visible = true;
            lblTo.Visible = true;
            imgbtnVisitDateTo.Visible = true;
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

    protected void rdoOn_CheckedChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            tabcontainerPatientVisit.ActiveTabIndex = 3;
            txtVisitDateTo.Visible = false;
            lblDateOfService.Visible = false;
            lblTo.Visible = false;
            imgbtnVisitDateTo.Visible = false;
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

    protected void grdScheduledVisits_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if (e.CommandName.ToString() == "Add")
            {
                tabcontainerPatientVisit.ActiveTabIndex = 3;
                extddlDoctor.Text = e.CommandArgument.ToString();
                GetTestList();
                txtVisitDateFrom.Text = e.Item.Cells[3].Text;
                lstPatientVisits.SelectedValue = e.Item.Cells[6].Text;

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

    private void GetPatientDeskList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            grdPatientDeskList.DataSource = _bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            grdPatientDeskList.DataBind();

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
