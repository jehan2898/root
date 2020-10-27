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

public partial class Bill_Sys_Treatments : PageBase
{
    private Bill_Sys_TreatmentBO _treatmentBO;
    private ArrayList _arraylist;
   
    private const string SZ_PROCEDURE_TYPE = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            extddlDocName.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
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
                    tabcontainerPatientTreatment.ActiveTabIndex = 3;
                    extddlDocName.Text = Request.QueryString["DoctorID"].ToString();
                    txtTreatmentDateFrom.Text = Convert.ToDateTime(Request.QueryString["Date"]).ToShortDateString();
                    BindTreatmentList();
                    extddlDoctor.Text = Request.QueryString["DoctorID"].ToString();
                    BindViewTreatmentList();
                    lstDoctorTreatment.SelectedValue = Request.QueryString["TypeCode"].ToString();
              }

                GetTotalTreatmentCountAndDate();
                BindLatestTreatmentlist();
                BindSummaryTreatmentList();
                BindBilledTreatment();
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
            cv.MakeReadOnlyPage("Bill_Sys_Treatments.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void BindTreatmentList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _treatmentBO = new Bill_Sys_TreatmentBO();
        try
        {
            lstDoctorTreatment.DataSource = _treatmentBO.GetDoctorProcedureList(txtCompanyID.Text, extddlDocName.Text, "treatments");
            lstDoctorTreatment.DataTextField = "DESCRIPTION";
            lstDoctorTreatment.DataValueField = "CODE";
            lstDoctorTreatment.DataBind();
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
            lblTotalBilledCount.Text = _treatmentBO.GetTreatmentListCount(txtCompanyID.Text, txtPatientID.Text, "TREATMENTS", "BILLEDLISTCOUNT");
            lblTotalUnBilledCount.Text = _treatmentBO.GetTreatmentListCount(txtCompanyID.Text, txtPatientID.Text, "TREATMENTS", "UN_BILLEDLISTCOUNT");
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
            lstTreatmentList.DataSource = _treatmentBO.GetTreatmentList(txtCompanyID.Text, extddlDoctor.Text,txtPatientID.Text,"TREATMENTS" );
            lstTreatmentList.DataTextField = "DESCRIPTION";
            lstTreatmentList.DataValueField = "CODE";
            lstTreatmentList.DataBind();
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
   
    private void SaveTreatment()
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
            for (int i = 0; i < lstDoctorTreatment.Items.Count; i++)
            {
                _arraylist.Clear();

                if (rdoOn.Checked == true)
                {
                    if (lstDoctorTreatment.Items[i].Selected)
                    {
                        _arraylist.Add(txtPatientID.Text);
                        _arraylist.Add(extddlDocName.Text);
                        _arraylist.Add(txtTreatmentDateFrom.Text);
                        _arraylist.Add(lstDoctorTreatment.Items[i].Value);
                        _arraylist.Add(txtCompanyID.Text);
                        _treatmentBO.SaveTreatment(_arraylist);
                    }
                }
                else
                {
                    if (lstDoctorTreatment.Items[i].Selected)
                    {
                        DateTime objFromDate = Convert.ToDateTime(txtTreatmentDateFrom.Text);
                        DateTime objToDate = Convert.ToDateTime(txtTreatmentDateTo.Text);

                        while (objFromDate <= objToDate)
                        {
                            _arraylist.Clear();
                            _arraylist.Add(txtPatientID.Text);
                            _arraylist.Add(extddlDocName.Text);
                            _arraylist.Add(objFromDate);
                            _arraylist.Add(lstDoctorTreatment.Items[i].Value);
                            _arraylist.Add(txtCompanyID.Text);
                            _treatmentBO.SaveTreatment(_arraylist);
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
            _arraylist = _treatmentBO.GetLatestDoctorTreatmentCount(txtCompanyID.Text, txtPatientID.Text,"TREATMENTS");
            if (_arraylist.Count > 0)
            {

                lblNoOfTreatment.Text = _arraylist[1].ToString();


                if (_arraylist[0].ToString() != "")
                {
                    lblLastTreatment.Text = Convert.ToDateTime(_arraylist[0].ToString()).ToShortDateString();
                }
                else
                {
                    lblLastTreatment.Text = "";
                }
            }
            else
            {
                lblNoOfTreatment.Text = "";
                lblLastTreatment.Text = "";
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
            grdScheduledTreatments.DataSource = _treatmentBO.GetSceduledTreatmentList(txtCompanyID.Text, txtPatientID.Text, "TREATMENTS"); ;
            grdScheduledTreatments.DataBind();

            grdLatestTreatment.DataSource = _treatmentBO.GetLatestTreatmentList(txtCompanyID.Text, txtPatientID.Text, "TREATMENTS");
          grdLatestTreatment.DataBind();

          string strDoctorName = "";
          for (int i = 0; i < grdLatestTreatment.Items.Count; i++)
          {
              Label lblDoctorName =(Label) grdLatestTreatment.Items[i].Cells[3].FindControl("lblDoctorName");
              if (strDoctorName != "" && strDoctorName == lblDoctorName.Text)
              {
                  lblDoctorName.Text = "";
              }
              else
              {
                  strDoctorName = lblDoctorName.Text;
              }
          }
            //GetLatestTreatmentList
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
            grdSummaryTreatmentGrid.DataSource = _treatmentBO.GetSummaryTreatmentList(txtCompanyID.Text, txtPatientID.Text,"TREATMENTS");
            grdSummaryTreatmentGrid.DataBind();


            string strDoctorName = "";
            for (int i = 0; i < grdSummaryTreatmentGrid.Items.Count; i++)
            {
                Label lblDoctorName = (Label)grdSummaryTreatmentGrid.Items[i].Cells[3].FindControl("lblDocName");
                if (strDoctorName != "" && strDoctorName == lblDoctorName.Text)
                {
                    LinkButton lnkAdd = (LinkButton)grdSummaryTreatmentGrid.Items[i].Cells[3].FindControl("lnkAdd");
                    LinkButton lnkView = (LinkButton)grdSummaryTreatmentGrid.Items[i].Cells[3].FindControl("lnkView");

                    lnkAdd.Text = "";
                    lnkView.Text = "";
                    lblDoctorName.Text = "";
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
          _arraylist=  _treatmentBO.GetDoctorTreatmentCount(txtCompanyID.Text, extddlDoctor.Text,txtPatientID.Text,"TREATMENTS" );
          if (_arraylist.Count > 0)
          {
              if (_arraylist[0].ToString() != "")
              {
                  lblLastTreatmentDate.Text = Convert.ToDateTime(_arraylist[0].ToString()).ToShortDateString();
              }
              else
              {
                  lblLastTreatmentDate.Text = "";
              }
             
                  lblTotalTreatment.Text = _arraylist[1].ToString();
              

          }
          else
          {
              lblLastTreatmentDate.Text = "";
              lblTotalTreatment.Text = "";
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

    protected void btnSaveTreatment_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SaveTreatment();
            BindLatestTreatmentlist();
            BindSummaryTreatmentList();
            GetTotalTreatmentCountAndDate();
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

    protected void extddlDocName_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            tabcontainerPatientTreatment.ActiveTabIndex = 3;
            BindTreatmentList();
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

    protected void BindBilledTreatment()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _treatmentBO = new Bill_Sys_TreatmentBO();

        try
        {
            grdBilledTreatment.DataSource = _treatmentBO.GetBilledTreatmentList(txtCompanyID.Text, txtPatientID.Text, "TREATMENTS");
            grdBilledTreatment.DataBind();

            string strDName = "";
            for (int i = 0; i < grdBilledTreatment.Items.Count; i++)
            {
                Label lblDoc = (Label)grdBilledTreatment.Items[i].Cells[3].FindControl("lblDName");
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
            txtTreatmentDateTo.Visible = true;
            lblDateOfService.Visible = true;
            lblTo.Visible = true;
            imgbtnTreatmentDateTo.Visible = true;
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
            txtTreatmentDateTo.Visible = false;
            lblDateOfService.Visible = false;
            lblTo.Visible = false;
            imgbtnTreatmentDateTo.Visible = false;
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
                extddlDocName.Text = e.CommandArgument.ToString();
                BindTreatmentList();
                txtTreatmentDateFrom.Text = e.Item.Cells[3].Text;
                lstDoctorTreatment.SelectedValue = e.Item.Cells[6].Text;
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

    protected void grdSummaryTreatmentGrid_ItemCommand(object source, DataGridCommandEventArgs e)
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
                extddlDocName.Text = e.CommandArgument.ToString();
            }
            else if (e.CommandName.ToString() == "View")
            {
                tabcontainerPatientTreatment.ActiveTabIndex = 2;
                extddlDoctor.Text = e.CommandArgument.ToString(); 
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
