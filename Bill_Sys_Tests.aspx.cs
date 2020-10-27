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

public partial class Bill_Sys_Tests : PageBase
{

    private Bill_Sys_TreatmentBO _treatmentBO;
    private ArrayList _arraylist;
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
            extddlDoctorNameList.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!IsPostBack)
            {

                if (Request.QueryString["PatientID"] != null)
                {
                    if (Request.QueryString["PatientID"].ToString() != "")
                    {
                        CaseDetailsBO _caseDetailsBO=new CaseDetailsBO();
                        Bill_Sys_CaseObject _bill_Sys_CaseObject=new Bill_Sys_CaseObject();
                        _bill_Sys_CaseObject.SZ_CASE_ID=_caseDetailsBO.GetCasePatientID("",Request.QueryString["PatientID"].ToString());
                        _bill_Sys_CaseObject.SZ_PATIENT_ID=Request.QueryString["PatientID"].ToString();
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

                    Session["PassedCaseID"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
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

                //
                
                if (Request.QueryString["DoctorID"] != null)
                {
                    tabcontainerPatientTreatment.ActiveTabIndex = 3;
                    extddlDoctorNameList.Text = Request.QueryString["DoctorID"].ToString();
                    txtVisitDateFrom.Text = Convert.ToDateTime(Request.QueryString["Date"]).ToShortDateString();
                    GetTestList();
                    extddlDoctor.Text = Request.QueryString["DoctorID"].ToString();
                    BindViewTreatmentList();
                    lstTest.SelectedValue = Request.QueryString["TypeCode"].ToString();

                }

                BindLatestTreatmentlist();
                BindSummaryTreatmentList();
                GetTotalTreatmentCountAndDate();
                BindBilledTest();
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
            cv.MakeReadOnlyPage("Bill_Sys_Tests.aspx");
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
            lblTotalBilledCount.Text = _treatmentBO.GetTreatmentListCount(txtCompanyID.Text, txtPatientID.Text, "TEST", "BILLEDLISTCOUNT");
            lblTotalUnBilledCount.Text = _treatmentBO.GetTreatmentListCount(txtCompanyID.Text, txtPatientID.Text, "TEST", "UN_BILLEDLISTCOUNT");
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


    protected void btnAddTest_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _treatmentBO = new Bill_Sys_TreatmentBO();
        _arraylist = new ArrayList();
        try
        {
            for (int i = 0; i < lstTest.Items.Count; i++)
            {
                _arraylist.Clear();
                if (rdoOn.Checked == true)
                {
                    if (lstTest.Items[i].Selected)
                    {
                        _arraylist.Add(txtPatientID.Text);
                        _arraylist.Add(extddlDoctorNameList.Text);
                        _arraylist.Add(txtVisitDateFrom.Text);
                        _arraylist.Add(lstTest.Items[i].Value);
                        _arraylist.Add(txtCompanyID.Text);
                        _treatmentBO.SaveTreatment(_arraylist);
                    }
                }
                else
                {
                    if (lstTest.Items[i].Selected)
                    {
                        DateTime objFromDate = Convert.ToDateTime(txtVisitDateFrom.Text);
                        DateTime objToDate = Convert.ToDateTime(txtVisitDateTo.Text);

                        while (objFromDate <= objToDate)
                        {

                            _arraylist = new ArrayList();
                            _arraylist.Add(txtPatientID.Text);
                            _arraylist.Add(extddlDoctorNameList.Text);
                            _arraylist.Add(objFromDate);
                            _arraylist.Add(lstTest.Items[i].Value);
                            _arraylist.Add(txtCompanyID.Text);
                            _treatmentBO.SaveTreatment(_arraylist);
                            objFromDate = objFromDate.AddDays(1);
                        }

                    }
                }
            }
            BindLatestTreatmentlist();
            BindSummaryTreatmentList();           
            BindBilledTest();
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
            lstTest.DataSource = _treatmentBO.GetDoctorProcedureList(txtCompanyID.Text, extddlDoctorNameList.Text, "tests");
            lstTest.DataTextField = "DESCRIPTION";
            lstTest.DataValueField = "CODE";
            lstTest.DataBind();
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
    protected void extddlDoctorNameList_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            tabcontainerPatientTreatment.ActiveTabIndex = 3;
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


    private void BindSummaryTreatmentList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdSummaryTestGrid.DataSource = _treatmentBO.GetSummaryTreatmentList(txtCompanyID.Text, txtPatientID.Text, "TEST");
            grdSummaryTestGrid.DataBind();


            string strDoctorName = "";
            for (int i = 0; i < grdSummaryTestGrid.Items.Count; i++)
            {
                Label lblDoctorName = (Label)grdSummaryTestGrid.Items[i].Cells[3].FindControl("lblDocName");
                if (strDoctorName != "" && strDoctorName == lblDoctorName.Text)
                {
                    lblDoctorName.Text = "";
                    LinkButton lnkAdd = (LinkButton)grdSummaryTestGrid.Items[i].Cells[3].FindControl("lnkAdd");
                    LinkButton lnkView = (LinkButton)grdSummaryTestGrid.Items[i].Cells[3].FindControl("lnkView");

                    lnkAdd.Text = "";
                    lnkView.Text = "";
                }
                else
                {
                    strDoctorName = lblDoctorName.Text;
                }
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    private void BindLatestTreatmentlist()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _treatmentBO = new Bill_Sys_TreatmentBO();
        try
        {

            grdScheduledTests.DataSource = _treatmentBO.GetSceduledTreatmentList(txtCompanyID.Text, txtPatientID.Text, "TEST"); ;
            grdScheduledTests.DataBind();


            grdLatestTest.DataSource = _treatmentBO.GetLatestTreatmentList(txtCompanyID.Text, txtPatientID.Text, "TEST");
            grdLatestTest.DataBind();

            string strDoctorName = "";
            for (int i = 0; i < grdLatestTest.Items.Count; i++)
            {
                Label lblDoctorName = (Label)grdLatestTest.Items[i].Cells[3].FindControl("lblDoctorName");
                if (strDoctorName != "" && strDoctorName == lblDoctorName.Text)
                {
                    lblDoctorName.Text = "";
                }
                else
                {
                    strDoctorName = lblDoctorName.Text;
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

    protected void extddlDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            tabcontainerPatientTreatment.ActiveTabIndex = 2;
            ViewDoctorTreatmentList();
            BindViewTreatmentList();
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


    private void GetTotalTreatmentCountAndDate()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _treatmentBO = new Bill_Sys_TreatmentBO();
        _arraylist = new ArrayList();
        try
        {
            _arraylist = _treatmentBO.GetLatestDoctorTreatmentCount(txtCompanyID.Text, txtPatientID.Text, "TEST");
            if (_arraylist.Count > 0)
            {

                lblNoOfTest.Text = _arraylist[1].ToString();


                if (_arraylist[0].ToString() != "")
                {
                    lblLastTest.Text = Convert.ToDateTime(_arraylist[0].ToString()).ToShortDateString();
                }
                else
                {
                    lblLastTest.Text = "";
                }
            }
            else
            {
                lblNoOfTest.Text = "";
                lblLastTest.Text = "";
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


    private void ViewDoctorTreatmentList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _treatmentBO = new Bill_Sys_TreatmentBO();
        _arraylist = new ArrayList();
        try
        {
            _arraylist = _treatmentBO.GetDoctorTreatmentCount(txtCompanyID.Text, extddlDoctor.Text, txtPatientID.Text,"TEST");
            if (_arraylist.Count > 0)
            {
                if (_arraylist[0].ToString() != "")
                {
                    lblLastTestDate.Text = Convert.ToDateTime(_arraylist[0].ToString()).ToShortDateString();
                }
                else
                {
                    lblLastTestDate.Text = "";
                }

                lblTotalTest.Text = _arraylist[1].ToString();


            }
            else
            {
                lblLastTestDate.Text = "";
                lblTotalTest.Text = "";
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

    private void BindViewTreatmentList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _treatmentBO = new Bill_Sys_TreatmentBO();
        try
        {
            lstTestList.DataSource = _treatmentBO.GetTreatmentList(txtCompanyID.Text, extddlDoctor.Text, txtPatientID.Text, "TEST");
            lstTestList.DataTextField = "DESCRIPTION";
            lstTestList.DataValueField = "CODE";
            lstTestList.DataBind();
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

    protected void BindBilledTest()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _treatmentBO = new Bill_Sys_TreatmentBO();

        try
        {
            grdBilledTest.DataSource = _treatmentBO.GetBilledTreatmentList(txtCompanyID.Text, txtPatientID.Text, "TEST");
            grdBilledTest.DataBind();

            string strDName = "";
            for (int i = 0; i < grdBilledTest.Items.Count; i++)
            {
                Label lblDoc = (Label)grdBilledTest.Items[i].Cells[3].FindControl("lblDName");
                if (strDName != "" && strDName == lblDoc.Text)
                {
                    lblDoc.Text = "";
                }
                else
                {
                    strDName = lblDoc.Text;
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

    protected void rdoFromTo_CheckedChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            tabcontainerPatientTreatment.ActiveTabIndex = 3;
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
            tabcontainerPatientTreatment.ActiveTabIndex = 3;
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
                tabcontainerPatientTreatment.ActiveTabIndex = 3;
                extddlDoctorNameList.Text = e.CommandArgument.ToString();
                GetTestList();
                txtVisitDateFrom.Text = e.Item.Cells[3].Text;
                lstTest.SelectedValue = e.Item.Cells[6].Text;

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

    protected void grdSummaryTestGrid_ItemCommand(object source, DataGridCommandEventArgs e)
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
                
                extddlDoctorNameList.Text = e.CommandArgument.ToString();
                tabcontainerPatientTreatment.ActiveTabIndex = 3;
            }
            else if (e.CommandName.ToString() == "View")
            {
                extddlDoctor.Text = e.CommandArgument.ToString();
                tabcontainerPatientTreatment.ActiveTabIndex = 2;
                
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
