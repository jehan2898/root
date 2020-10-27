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

public partial class Bill_Sys_AssociateDignosisCode : PageBase
{
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    private ArrayList _arrayList;
    private DataSet _ds;
    private string strCaseType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            btnAssign.Attributes.Add("onclick", "return formValidator('frmAssociateDignosisCode','extddlDoctor');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlAPDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            
            if (!IsPostBack)
            {
                if (Request.QueryString["CaseId"] != null)
                {
                    Session["CASE_OBJECT"] = null;
                    txtCaseID.Text = Request.QueryString["CaseId"].ToString();
                    GetPatientDeskList();
                }
                else if (Session["CASE_OBJECT"] != null)
                {
                    txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
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
                    GetPatientDeskList();
                    //
                    ///////////////////
                }

                else
                {
                    Response.Redirect("Bill_Sys_SearchCase.aspx", false);
                }
           
                strCaseType = _associateDiagnosisCodeBO.GetCaseType(txtCaseID.Text);
                if (Request.QueryString["DoctorID"] != null)
                {
                    extddlDoctor.Text = Request.QueryString["DoctorID"].ToString();
                    extddlDoctor.Enabled = false;
                    GetDiagnosisCode(txtCaseID.Text, txtCompanyID.Text, extddlDoctor.Text, "GET_DIAGNOSIS_CODE");
                }
                if (Request.QueryString["SetId"] != null)
                {
                    txtDiagnosisSetID.Text = Request.QueryString["SetId"].ToString();
                    _ds = _associateDiagnosisCodeBO.GetCaseAssociateDiagnosisDetails(txtDiagnosisSetID.Text);
                    extddlDoctor.Text = _ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    GetDiagnosisCode(txtCaseID.Text, txtCompanyID.Text, extddlDoctor.Text, "GET_DIAGNOSIS_CODE");
                    //GetPTDiagnosisCode(extddlDoctor.Text, Request.QueryString["SetId"].ToString(), "GET_PT_DIAGNOSIS_CODE");
                    //GetEvaluationDiagnosisCode(extddlDoctor.Text, Request.QueryString["SetId"].ToString(), "GET_EVALUATION_DIAGNOSIS_CODE");
                    Visiblecontrol();
                    //BindControl();
                }
                else
                {
                    txtDiagnosisSetID.Text = _associateDiagnosisCodeBO.GetDiagnosisSetID();
                }

                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    extddlAPDoctor.Visible = false;
                    extddlDoctor.Visible = false;
                    lblDoctor.Visible = false;
                    lblDeDoctor.Visible = false;
                    GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
                    GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
                }
            }
            lblMsg.Text = "";
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
            cv.MakeReadOnlyPage("Bill_Sys_AssociateDignosisCode.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetDiagnosisCode(string caseID, string companyId, string doctorId, string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet ds = new DataSet();
            // please check the below function.. it returns all the rows from the database when a doctor is selected
            ds = _associateDiagnosisCodeBO.GetDiagnosisCode(caseID, companyId, doctorId, flag);

            grdNormalDgCode.DataSource = ds.Tables[0]; //_associateDiagnosisCodeBO.GetDoctorDiagnosisCode(id, flag);
            grdNormalDgCode.DataBind();
            DataTable dt=new DataTable();
            dt.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            dt.Columns.Add("SZ_DIAGNOSIS_CODE");
            dt.Columns.Add("SZ_DESCRIPTION");
            dt.Columns.Add("SZ_COMPANY_ID");
            DataRow dr;
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            dtNew.Columns.Add("SZ_DIAGNOSIS_CODE");
            dtNew.Columns.Add("SZ_DESCRIPTION");
            dtNew.Columns.Add("SZ_COMPANY_ID");
            DataRow drNew;
            for (int intLoop = 0; intLoop < ds.Tables[0].Rows.Count; intLoop++)
            {
                foreach (DataGridItem dgiProcedureCode in grdNormalDgCode.Items)
                {
                    if (dgiProcedureCode.ItemIndex == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
                    {
                        dr = dt.NewRow();
                        dr["SZ_DIAGNOSIS_CODE_ID"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                        dr["SZ_DIAGNOSIS_CODE"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE"].ToString();
                        dr["SZ_DESCRIPTION"] = ds.Tables[0].Rows[intLoop]["SZ_DESCRIPTION"].ToString();
                        dr["SZ_COMPANY_ID"] = ds.Tables[0].Rows[intLoop]["SZ_COMPANY_ID"].ToString();
                        dt.Rows.Add(dr);

                        //CheckBox chkEmpty = (CheckBox)dgiProcedureCode.Cells[0].FindControl("chkSelect");
                        //chkEmpty.Checked = true;
                    }
                    //else
                    //{
                    //    drNew = dtNew.NewRow();
                    //    drNew["SZ_DIAGNOSIS_CODE_ID"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                    //    drNew["SZ_DIAGNOSIS_CODE"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE"].ToString();
                    //    drNew["SZ_DESCRIPTION"] = ds.Tables[0].Rows[intLoop]["SZ_DESCRIPTION"].ToString();
                    //    drNew["SZ_COMPANY_ID"] = ds.Tables[0].Rows[intLoop]["SZ_COMPANY_ID"].ToString();
                    //    dtNew.Rows.Add(drNew);
                    //}
                }
            }

            grdSelectedDiagnosisCode.DataSource = dt;
            grdSelectedDiagnosisCode.DataBind();

           

            //grdNormalDgCode.DataSource = null;
            //grdNormalDgCode.DataBind();

            //grdNormalDgCode.DataSource = dtNew;
            //grdNormalDgCode.DataBind();
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

    private void GetAssignedDiagnosisCode(string caseID, string companyId, string doctorId, string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet ds = new DataSet();
            // please check the below function.. it returns all the rows from the database when a doctor is selected
            ds = _associateDiagnosisCodeBO.GetDiagnosisCode(caseID, companyId, doctorId, flag);

            grdNormalDgCode.DataSource = ds.Tables[0]; //_associateDiagnosisCodeBO.GetDoctorDiagnosisCode(id, flag);
            grdNormalDgCode.DataBind();
            DataTable dt = new DataTable();
            dt.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            dt.Columns.Add("SZ_DIAGNOSIS_CODE");
            dt.Columns.Add("SZ_DESCRIPTION");
            dt.Columns.Add("SZ_COMPANY_ID");
            DataRow dr;
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            dtNew.Columns.Add("SZ_DIAGNOSIS_CODE");
            dtNew.Columns.Add("SZ_DESCRIPTION");
            dtNew.Columns.Add("SZ_COMPANY_ID");
            DataRow drNew;
            for (int intLoop = 0; intLoop < ds.Tables[0].Rows.Count; intLoop++)
            {
                foreach (DataGridItem dgiProcedureCode in grdNormalDgCode.Items)
                {
                    if (dgiProcedureCode.ItemIndex == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
                    {
                        dr = dt.NewRow();
                        dr["SZ_DIAGNOSIS_CODE_ID"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                        dr["SZ_DIAGNOSIS_CODE"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE"].ToString();
                        dr["SZ_DESCRIPTION"] = ds.Tables[0].Rows[intLoop]["SZ_DESCRIPTION"].ToString();
                        dr["SZ_COMPANY_ID"] = ds.Tables[0].Rows[intLoop]["SZ_COMPANY_ID"].ToString();
                        dt.Rows.Add(dr);

                        //CheckBox chkEmpty = (CheckBox)dgiProcedureCode.Cells[0].FindControl("chkSelect");
                        //chkEmpty.Checked = true;
                    }
                    //else
                    //{
                    //    drNew = dtNew.NewRow();
                    //    drNew["SZ_DIAGNOSIS_CODE_ID"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                    //    drNew["SZ_DIAGNOSIS_CODE"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE"].ToString();
                    //    drNew["SZ_DESCRIPTION"] = ds.Tables[0].Rows[intLoop]["SZ_DESCRIPTION"].ToString();
                    //    drNew["SZ_COMPANY_ID"] = ds.Tables[0].Rows[intLoop]["SZ_COMPANY_ID"].ToString();
                    //    dtNew.Rows.Add(drNew);
                    //}
                }
            }

            grdAssignedDiagnosisCode.DataSource = dt;
            grdAssignedDiagnosisCode.DataBind();



            //grdNormalDgCode.DataSource = null;
            //grdNormalDgCode.DataBind();

            //grdNormalDgCode.DataSource = dtNew;
            //grdNormalDgCode.DataBind();
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

    private void GetPTDiagnosisCode(string id, string setId, string flag)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {

            DataSet ds = new DataSet();
            ds = _associateDiagnosisCodeBO.GetDoctorDiagnosisCode(id, setId, flag);

            grdPTDgCode.DataSource = _associateDiagnosisCodeBO.GetDoctorDiagnosisCode(id, "", flag);
            grdPTDgCode.DataBind();

            for (int intLoop = 0; intLoop < ds.Tables[0].Rows.Count; intLoop++)
            {
                foreach (DataGridItem dgiProcedureCode in grdPTDgCode.Items)
                {
                    if (dgiProcedureCode.ItemIndex == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
                    {
                        CheckBox chkEmpty = (CheckBox)dgiProcedureCode.Cells[0].FindControl("chkSelect");
                        chkEmpty.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetEvaluationDiagnosisCode(string id, string setId, string flag)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {

            DataSet ds = new DataSet();
            ds = _associateDiagnosisCodeBO.GetDoctorDiagnosisCode(id, setId, flag);

            grdDoctorEvaluationDgCode.DataSource = _associateDiagnosisCodeBO.GetDoctorDiagnosisCode(id, "", flag);
            grdDoctorEvaluationDgCode.DataBind();

            for (int intLoop = 0; intLoop < ds.Tables[0].Rows.Count; intLoop++)
            {
                foreach (DataGridItem dgiProcedureCode in grdDoctorEvaluationDgCode.Items)
                {
                    if (dgiProcedureCode.ItemIndex == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
                    {
                        CheckBox chkEmpty = (CheckBox)dgiProcedureCode.Cells[0].FindControl("chkSelect");
                        chkEmpty.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SaveDiagnosisCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        DataSet _billDs = new DataSet();
       
        try
        {

            _associateDiagnosisCodeBO.DeleteCaseAssociateDignosisCodeWithProcCode(txtCaseID.Text, txtCompanyID.Text, extddlDoctor.Text);//, lstPTDProcCode.Items[j].Value.Substring(0, lstPTDProcCode.Items[j].Value.IndexOf("|")), lstPTDProcCode.Items[j].Value.Substring((lstPTDProcCode.Items[j].Value.IndexOf("|") + 1), ((lstPTDProcCode.Items[j].Value.Length - lstPTDProcCode.Items[j].Value.IndexOf("|")) - 1)));
           


            foreach (DataGridItem dgiProcedureCode in grdSelectedDiagnosisCode.Items)
            {
               

                    _arrayList = new ArrayList();
                    _arrayList.Add(txtDiagnosisSetID.Text);
                    _arrayList.Add(txtCaseID.Text);
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        _arrayList.Add("");
                    }
                    else
                    {
                        _arrayList.Add(extddlDoctor.Text);
                    }
                    _arrayList.Add(dgiProcedureCode.Cells[0].Text.ToString());
                    _arrayList.Add(txtCompanyID.Text);
                    _associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(_arrayList);



            }
            foreach (DataGridItem dgiProcedureCode in grdNormalDgCode.Items)
            {
                CheckBox chkEmpty = (CheckBox)dgiProcedureCode.Cells[1].FindControl("chkSelect");
                if (chkEmpty.Checked == true)
                {

                    _arrayList = new ArrayList();
                    _arrayList.Add(txtDiagnosisSetID.Text);
                    _arrayList.Add(txtCaseID.Text);
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        _arrayList.Add("");
                    }
                    else
                    {
                        _arrayList.Add(extddlDoctor.Text);
                    }
                    _arrayList.Add(dgiProcedureCode.Cells[1].Text.ToString());
                    _arrayList.Add(txtCompanyID.Text);
                    _associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(_arrayList);


                }

            }
            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extddlDoctor.Text, "GET_DIAGNOSIS_CODE");
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
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            // ClearControl();
            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extddlDoctor.Text, "GET_DIAGNOSIS_CODE");
            // GetPTDiagnosisCode(extddlDoctor.Text, "", "GET_PT_DIAGNOSIS_CODE");
            // GetEvaluationDiagnosisCode(extddlDoctor.Text, "", "GET_EVALUATION_DIAGNOSIS_CODE");
            Visiblecontrol();
            tabcontainerDiagnosisCode.ActiveTabIndex = 0;
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

    protected void extddlAPDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            // ClearControl();
            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extddlAPDoctor.Text, "GET_DIAGNOSIS_CODE");
            // GetPTDiagnosisCode(extddlDoctor.Text, "", "GET_PT_DIAGNOSIS_CODE");
            // GetEvaluationDiagnosisCode(extddlDoctor.Text, "", "GET_EVALUATION_DIAGNOSIS_CODE");
            Visiblecontrol();
            tabcontainerDiagnosisCode.ActiveTabIndex = 1;
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

    protected void lstDiagnosis_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            // lstDProcCode.Items.Clear();
            // for (int i = 0; i < lstDiagnosis.Items.Count; i++)
            // {
            //    if (lstDiagnosis.Items[i].Selected)
            //   {
            // BindProcedureList(lstDiagnosis.SelectedValue);                       
            //  }
            //  }

            // BindProcedureList();                                 
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

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            // if (ValidateControl())
            // {                
            SaveDiagnosisCode();
            // ClearControl();
            lblMsg.Visible = true;
            lblMsg.Text = "Case Associate Diagnosis  Saved Successfully ...!";
            // }
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

    protected void btnCancel_Click(object sender, EventArgs e)
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

    private void Visiblecontrol()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            strCaseType = _associateDiagnosisCodeBO.GetCaseType(txtCaseID.Text);
            switch (_associateDiagnosisCodeBO.GetDoctorType(extddlDoctor.Text))
            {
                case 0:

                    tblDiagnosisCode.Visible = false;
                    tblDiagnosisCodeFirst.Visible = true;

                    break;
                case 1:
                    if (strCaseType == "WC")
                    {
                        tblDiagnosisCode.Visible = true;
                        tblDiagnosisCodeFirst.Visible = false;
                    }
                    else
                    {
                        tblDiagnosisCode.Visible = false;
                        tblDiagnosisCodeFirst.Visible = true;
                    }
                    break;
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

    protected void grdNormalDgCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdNormalDgCode.CurrentPageIndex = e.NewPageIndex;
            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extddlDoctor.Text, "GET_DIAGNOSIS_CODE");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string diagnosisCode = "";
            string diagnosisCodeDescription = "";
            String[] diagnosisCodeList;
            diagnosisCodeList = new String[txtSearchText.Text.Split(',').Length];
            Bill_Sys_DigosisCodeBO _bill_Sys_DigosisCodeBO = new Bill_Sys_DigosisCodeBO();
            if (chkDiagnosisCode.Checked == true)
            {
                diagnosisCode = txtSearchText.Text;

                diagnosisCodeList = txtSearchText.Text.Split(',');

                if (diagnosisCodeList.Length > 1)
                {
                    string szListDiagCode = "";
                    for (int ii = 0; ii < diagnosisCodeList.Length; ii++)
                    {
                        szListDiagCode += "'" + diagnosisCodeList[ii].ToString() + "'";
                        if (ii != diagnosisCodeList.Length - 1)
                        {
                            szListDiagCode += ",";
                        }
                    }
                    grdNormalDgCode.DataSource = _bill_Sys_DigosisCodeBO.GeMultipleDignosisCode_List(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, szListDiagCode, diagnosisCodeDescription).Tables[0];
                }
                else
                {
                    grdNormalDgCode.DataSource = _bill_Sys_DigosisCodeBO.GeDignosisCode_List(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosisCode, diagnosisCodeDescription).Tables[0];
                }
            }
            else if (chkDiagnosisCodeDescription.Checked == true)
            {
                    diagnosisCodeDescription = txtSearchText.Text;
                    grdNormalDgCode.DataSource = _bill_Sys_DigosisCodeBO.GeDignosisCode_List(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosisCode, diagnosisCodeDescription).Tables[0];
            }
            else
            {
                grdNormalDgCode.DataSource = _bill_Sys_DigosisCodeBO.GeDignosisCode_List(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "").Tables[0];
            }
            
            grdNormalDgCode.DataBind();
            grdNormalDgCode.Visible = true;
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


    protected void btnDeassociateDiagCode_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Boolean blStatus = false;
            string szDiagIDs = "";
            foreach (DataGridItem dgiItem in grdAssignedDiagnosisCode.Items)
            {
                CheckBox chkEmpty = (CheckBox)dgiItem.Cells[0].FindControl("chkSelect");
                if (chkEmpty.Checked == true)
                {
                    _arrayList = new ArrayList();
                    _arrayList.Add(dgiItem.Cells[1].Text.ToString());
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        _arrayList.Add("");
                    }
                    else
                    {
                        _arrayList.Add(extddlAPDoctor.Text);
                    }
                    
                    _arrayList.Add(txtCaseID.Text);
                    _arrayList.Add(txtCompanyID.Text);
                    _arrayList.Add(dgiItem.Cells[5].Text.ToString());
                    if (_associateDiagnosisCodeBO.DeleteAssociateDiagonisCode(_arrayList))
                    {
                    }
                    else
                    {
                        szDiagIDs += "  " + dgiItem.Cells[2].Text.ToString() + ",";
                    }
                }
            }
            if (szDiagIDs == "")
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Diagnosis code deassociated successfully ...!";
            }
            else
            {
                lblMsg.Visible = true;
                
                lblMsg.Text = szDiagIDs  + " diagnosis code used in bills. You can not de-associate it.";
            }
            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extddlAPDoctor.Text, "GET_DIAGNOSIS_CODE");
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
            grdPatientDeskList.DataSource = _bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTLIST", txtCaseID.Text);
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
