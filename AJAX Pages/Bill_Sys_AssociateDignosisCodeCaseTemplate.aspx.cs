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
using DevExpress.Web;
using DevExpress.Web.Data;

public partial class Bill_Sys_AssociateDignosisCodeCaseTemplate : PageBase
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    Bill_Sys_CheckoutBO _obj_CheckOutBO;
    private ArrayList _arrayList;
    private DataSet _ds;
    private string strCaseType = "";
    string sz_eventid = "";
    DataSet dsSelectedDiagnosis = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        lblErrorMessage.Text = "";
        lblMessage.Text = "";
        
        try
        {
            //sz_eventid = Request.QueryString["EID"];
            //if (Request.QueryString["Flag"] == "SYN")
            //{
            //    btnTemplateManager.Text = "Synaptic Notes";
            //}

            //if (Request.QueryString["fromCheckOut"] == "true")
            //{
            //    btnTemplateManager.Visible = false;
            //    txtCurrentPage.Text = "CheckOut";
            //}
            //if (Request.QueryString["fromCheckOut"] == "notes")
            //{
            //    txtCurrentPage.Text = "Notes";
            //}
            
            _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
            DataSet dsDoctorId = new DataSet();
            DataSet dsSpeciality = new DataSet();
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtSpeciality.Text = "Pharmacy";
            txtUserId.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            dsDoctorId = _obj_CheckOutBO.GetDoctorUserID(txtUserId.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            //txtDoctorId.Text = dsDoctorId.Tables[0].Rows[0][0].ToString();

            //dsSpeciality = GET_DoctorSpeciality(txtDoctorId.Text, txtCompanyID.Text);
            //txtSpeciality.Text = dsSpeciality.Tables[0].Rows[0][0].ToString();

            txtDiagonosisCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerDiagnosisCode_tabpnlAssociate_btnSeacrh').click(); return false;}} else {alert('Other');return true;}; ");
            if (!IsPostBack)
            {
                Session["DiagnosysList"] = "";

                extddlDiagnosisType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
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
                }

                else
                {
                    Response.Redirect("Bill_Sys_SearchCase.aspx", false);
                }

                strCaseType = _associateDiagnosisCodeBO.GetCaseType(txtCaseID.Text);
                if (Request.QueryString["DoctorID"] != null)
                {
                    //   GetDiagnosisCode(txtCaseID.Text, txtCompanyID.Text, "", "GET_DIAGNOSIS_CODE");
                }
                if (Request.QueryString["SetId"] != null)
                {
                    txtDiagnosisSetID.Text = Request.QueryString["SetId"].ToString();
                    _ds = _associateDiagnosisCodeBO.GetCaseAssociateDiagnosisDetails(txtDiagnosisSetID.Text);
                    GetDiagnosisCode(txtCaseID.Text, txtCompanyID.Text, "", "GET_DIAGNOSIS_CODE");
                    Visiblecontrol();
                }
                else
                {
                    txtDiagnosisSetID.Text = _associateDiagnosisCodeBO.GetDiagnosisSetID();
                }

                //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                //{
                GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
                GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
                grdNormalDgCode.DataSource = null;
                grdNormalDgCode.DataBind();
                //}
                extddlSpeciality.Visible = false;
                lblSpeciality.Visible = false;

                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    tabcontainerDiagnosisCode.ActiveTabIndex = 1;
                }
                else
                {
                    tabcontainerDiagnosisCode.ActiveTabIndex = 0;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_AssociateDignosisCodeCase.aspx");
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
            DataTable dt = new DataTable();
            dt.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            dt.Columns.Add("SZ_DIAGNOSIS_CODE");
            dt.Columns.Add("SZ_DESCRIPTION");
            dt.Columns.Add("SZ_COMPANY_ID");
            dt.Columns.Add("SZ_PROCEDURE_GROUP");
            dt.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataRow dr;
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            dtNew.Columns.Add("SZ_DIAGNOSIS_CODE");
            dtNew.Columns.Add("SZ_DESCRIPTION");
            dtNew.Columns.Add("SZ_COMPANY_ID");
            dtNew.Columns.Add("SZ_PROCEDURE_GROUP");
            dtNew.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataRow drNew;
            for (int intLoop = 0; intLoop < ds.Tables[0].Rows.Count; intLoop++)
            {
                //foreach (DataGridItem dgiProcedureCode in grdNormalDgCode.Items)
                //jyoti
                for (int k=0;k<grdNormalDgCode.VisibleRowCount;k++)
                {
                    //if (dgiProcedureCode.ItemIndex == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
                    if (k == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
                    {
                        dr = dt.NewRow();
                        dr["SZ_DIAGNOSIS_CODE_ID"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                        dr["SZ_DIAGNOSIS_CODE"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE"].ToString();
                        dr["SZ_DESCRIPTION"] = ds.Tables[0].Rows[intLoop]["SZ_DESCRIPTION"].ToString();
                        dr["SZ_COMPANY_ID"] = ds.Tables[0].Rows[intLoop]["SZ_COMPANY_ID"].ToString();
                        dr["SZ_PROCEDURE_GROUP"] = ds.Tables[0].Rows[intLoop]["Speciality"].ToString();
                        dr["SZ_PROCEDURE_GROUP_ID"] = ds.Tables[0].Rows[intLoop]["SZ_PROCEDURE_GROUP_ID"].ToString();
                        dt.Rows.Add(dr);

                    }

                }
            }
            //dsSelectedDiagnosis = new DataSet();
            dsSelectedDiagnosis.Tables.Clear();
            dsSelectedDiagnosis.Tables.Add(dt);
            Session["selectedDS"] = dsSelectedDiagnosis;
            grdSelectedDiagnosisCode.DataSource = dt;
            grdSelectedDiagnosisCode.DataBind();
            lblDiagnosisCount.Text = dt.Rows.Count.ToString();

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

    private void GetAssignedDiagnosisCode(string caseID, string companyId, string doctorId, string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Session["DiagnosysList"] = "";
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        ArrayList _arrayListDiagnosys = new ArrayList();
        int icnt = 0;
        try
        {
            DataSet ds = new DataSet();
            // please check the below function.. it returns all the rows from the database when a doctor is selected
            ds = _associateDiagnosisCodeBO.GetDiagnosisCode(caseID, companyId, doctorId, flag);
            //    ds = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
            grdNormalDgCode.DataSource = ds.Tables[0]; //_associateDiagnosisCodeBO.GetDoctorDiagnosisCode(id, flag);
            grdNormalDgCode.DataBind();
            DataTable dt = new DataTable();
            dt.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            dt.Columns.Add("SZ_DIAGNOSIS_CODE");
            dt.Columns.Add("SZ_DESCRIPTION");
            dt.Columns.Add("SZ_COMPANY_ID");
            dt.Columns.Add("SZ_PROCEDURE_GROUP");
            dt.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataRow dr;
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            dtNew.Columns.Add("SZ_DIAGNOSIS_CODE");
            dtNew.Columns.Add("SZ_DESCRIPTION");
            dtNew.Columns.Add("SZ_COMPANY_ID");
            dtNew.Columns.Add("SZ_PROCEDURE_GROUP");
            dtNew.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataRow drNew;
            
            for (int intLoop = 0; intLoop < ds.Tables[0].Rows.Count; intLoop++)
            {
                                
                for (int i = 0; i < grdNormalDgCode.VisibleRowCount;i++ )
                {
                    if (i== intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
                    {
                        dr = dt.NewRow();
                        dr["SZ_DIAGNOSIS_CODE_ID"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                        dr["SZ_DIAGNOSIS_CODE"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE"].ToString();
                        dr["SZ_DESCRIPTION"] = ds.Tables[0].Rows[intLoop]["SZ_DESCRIPTION"].ToString();
                        dr["SZ_COMPANY_ID"] = ds.Tables[0].Rows[intLoop]["SZ_COMPANY_ID"].ToString();
                        dr["SZ_PROCEDURE_GROUP"] = ds.Tables[0].Rows[intLoop]["Speciality"].ToString();
                        dr["SZ_PROCEDURE_GROUP_ID"] = ds.Tables[0].Rows[intLoop]["SZ_PROCEDURE_GROUP_ID"].ToString();
                        dt.Rows.Add(dr);
                        _arrayListDiagnosys.Add(grdNormalDgCode.GetRowValues(i, "SZ_DIAGNOSIS_CODE").ToString() + ' ' + grdNormalDgCode.GetRowValues(i, "SZ_DESCRIPTION"));
                    }
                }
            }
            Session["DiagnosysList"] = (ArrayList)_arrayListDiagnosys;

            grdAssignedDiagnosisCode.DataSource = dt;
            grdAssignedDiagnosisCode.DataBind();

            //for (int i = 0; i < grdAssignedDiagnosisCode.VisibleRowCount; i++)
            //{
            //    GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grdAssignedDiagnosisCode.Columns[0];
            //    CheckBox box = (CheckBox)this.grdAssignedDiagnosisCode.FindRowCellTemplateControl(i, gridViewDataColumn, "chkSelect");

            //    if (grdAssignedDiagnosisCode.GetRowValues(i, "SZ_PROCEDURE_GROUP_ID").ToString() != txtSpeciality.Text.ToString())
            //    {
            //        box.Enabled = false;
            //    }
            //}
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

    private void SaveDiagnosisCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        DataSet _billDs = new DataSet();
        ArrayList _arrayListDiagnosys = new ArrayList();
        
        try
        {
            _associateDiagnosisCodeBO.DeleteCaseAssociateDignosisCodeWithProcCode(txtCaseID.Text, txtCompanyID.Text, "");//, lstPTDProcCode.Items[j].Value.Substring(0, lstPTDProcCode.Items[j].Value.IndexOf("|")), lstPTDProcCode.Items[j].Value.Substring((lstPTDProcCode.Items[j].Value.IndexOf("|") + 1), ((lstPTDProcCode.Items[j].Value.Length - lstPTDProcCode.Items[j].Value.IndexOf("|")) - 1)));

            dsSelectedDiagnosis = (DataSet)Session["selectedDS"];

            grdSelectedDiagnosisCode.DataSource = dsSelectedDiagnosis;
            grdSelectedDiagnosisCode.DataBind();
            
            for (int i = 0; i < grdSelectedDiagnosisCode.VisibleRowCount;i++ )
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
                    _arrayList.Add("");
                }
                _arrayList.Add(grdSelectedDiagnosisCode.GetRowValues(i, "SZ_DIAGNOSIS_CODE_ID").ToString());
                _arrayList.Add(txtCompanyID.Text);
                _arrayList.Add(Request.QueryString["ProcGroupID"].ToString());
                _associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(_arrayList);
                _arrayListDiagnosys.Add(grdSelectedDiagnosisCode.GetRowValues(i,"SZ_DESCRIPTION").ToString() + ' ' + grdSelectedDiagnosisCode.GetRowValues(i, "SZ_DIAGNOSIS_CODE").ToString());
            }
            for (int j = 0; j < grdNormalDgCode.VisibleRowCount && j < 20; j++)
            {
                //CheckBox chkEmpty = (CheckBox)dgiProcedureCode.Cells[1].FindControl("chkSelect");
                CheckBox chkEmpty = (CheckBox)grdNormalDgCode.FindRowCellTemplateControl(j, null, "chkSelect");

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
                        _arrayList.Add("");
                    }
                    _arrayList.Add(Request.QueryString["ProcGroupID"].ToString());
                    _arrayList.Add(grdNormalDgCode.GetRowValues(j,"SZ_DIAGNOSIS_CODE_ID"));
                    _arrayList.Add(txtCompanyID.Text);
                    _arrayList.Add(Request.QueryString["ProcCode"]);
                    _arrayList.Add(txtSpeciality.Text);
                    _associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(_arrayList);

                    //_arrayListDiagnosys.Add(dgiProcedureCode.Cells[2].Text + ' ' + dgiProcedureCode.Cells[4].Text);
                    _arrayListDiagnosys.Add(grdNormalDgCode.GetRowValues(j, "SZ_DIAGNOSIS_CODE").ToString() + ' ' + grdNormalDgCode.GetRowValues(j, "SZ_DESCRIPTION"));
                }

            }

            Session["DiagnosysList"] = (ArrayList)_arrayListDiagnosys;

            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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
            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
                         
                SaveDiagnosisCode();
                // ClearControl();
                lblMsg.Visible = true;
                BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
                lblMsg.Text = "Case Associate Diagnosis  Saved Successfully ...!";
                // }
                //Session["DiagnosysCode"] = "Diagnosys";
                //if (txtCurrentPage.Text == "CheckOut")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "parent.document.getElementById('divid2').style.visibility = 'hidden';window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='Bill_Sys_IM_CheckOut.aspx';", true);
                //    //  Response.Redirect("Bill_Sys_IM_CheckOut.aspx");
                //}
                //else if (txtCurrentPage.Text == "Notes")
                //{
                //    return;
                //}
                //else
                //{
                //    Response.Redirect("TM/RTFEditter.aspx");
                //}
           
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void Visiblecontrol()
    {
        //try
        //{
        //    strCaseType = _associateDiagnosisCodeBO.GetCaseType(txtCaseID.Text);
        //    switch (_associateDiagnosisCodeBO.GetDoctorType(""))
        //    {
        //        case 0:

        //            tblDiagnosisCode.Visible = false;
        //            tblDiagnosisCodeFirst.Visible = true;

        //            break;
        //        case 1:
        //            if (strCaseType == "WC")
        //            {
        //                tblDiagnosisCode.Visible = true;
        //                tblDiagnosisCodeFirst.Visible = false;
        //            }
        //            else
        //            {
        //                tblDiagnosisCode.Visible = false;
        //                tblDiagnosisCodeFirst.Visible = true;
        //            }
        //            break;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    string strError = ex.Message.ToString();
        //    strError = strError.Replace("\n", " ");
        //    Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        //}
    }

    //protected void grdNormalDgCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    //{
    //    try
    //    {
    //        //grdNormalDgCode.CurrentPageIndex = e.NewPageIndex;
    //        grdNormalDgCode.PageIndex = e.NewPageIndex;
    //        GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    protected void grdNormalDgCode_PageIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //    try
        //    {
        //        string diagnosisCode = "";
        //        string diagnosisCodeDescription = "";
        //        String[] diagnosisCodeList;
        //        diagnosisCodeList = new String[txtSearchText.Text.Split(',').Length];
        //        Bill_Sys_DigosisCodeBO _bill_Sys_DigosisCodeBO = new Bill_Sys_DigosisCodeBO();
        //        if (chkDiagnosisCode.Checked == true)
        //        {
        //            diagnosisCode = txtSearchText.Text;

        //            diagnosisCodeList = txtSearchText.Text.Split(',');

        //            if (diagnosisCodeList.Length > 1)
        //            {
        //                string szListDiagCode = "";
        //                for (int ii = 0; ii < diagnosisCodeList.Length; ii++)
        //                {
        //                    szListDiagCode += "'" + diagnosisCodeList[ii].ToString() + "'";
        //                    if (ii != diagnosisCodeList.Length - 1)
        //                    {
        //                        szListDiagCode += ",";
        //                    }
        //                }
        //                grdNormalDgCode.DataSource = _bill_Sys_DigosisCodeBO.GeMultipleDignosisCode_List(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, szListDiagCode, diagnosisCodeDescription).Tables[0];
        //            }
        //            else
        //            {
        //                grdNormalDgCode.DataSource = _bill_Sys_DigosisCodeBO.GeDignosisCode_List(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosisCode, diagnosisCodeDescription).Tables[0];
        //            }
        //        }
        //        else if (chkDiagnosisCodeDescription.Checked == true)
        //        {
        //                diagnosisCodeDescription = txtSearchText.Text;
        //                grdNormalDgCode.DataSource = _bill_Sys_DigosisCodeBO.GeDignosisCode_List(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosisCode, diagnosisCodeDescription).Tables[0];
        //        }
        //        else
        //        {
        //            grdNormalDgCode.DataSource = _bill_Sys_DigosisCodeBO.GeDignosisCode_List(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "").Tables[0];
        //        }

        //        grdNormalDgCode.DataBind();
        //        grdNormalDgCode.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        string strError = ex.Message.ToString();
        //        strError = strError.Replace("\n", " ");
        //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        //    }
    }


    #region "New Logic for searching"

    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        try
        {
            //if (extddlSpeciality.Selected_Text == "---Select---")
            //{
            //    Lblext.Visible = true;
            //    Lblext.Text = "Please Select Diagnosis Type";
            //}
            //else
            //{
                Lblext.Visible = false;
                lblSpeciality.Visible = true;
                //extddlSpeciality.Visible = true;
                //jyoti

                BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
                grdNormalDgCode.Visible = true;
        //    }
        }
        catch
        {
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
    }


    private void BindGrid(string typeid, string code, string description)
    {
        _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            grdNormalDgCode.DataSource = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, typeid, code, description);
            grdNormalDgCode.DataBind();

        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.ToString(); 
        }
    }


    #endregion

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
          //  foreach (DataGridItem dgiItem in grdAssignedDiagnosisCode.Items)
            for (int i = 0; i < grdAssignedDiagnosisCode.VisibleRowCount;i++ )
            {
                //CheckBox chkEmpty = (CheckBox)dgiItem.Cells[0].FindControl("chkSelect");
                CheckBox chkEmpty = (CheckBox)grdAssignedDiagnosisCode.FindRowCellTemplateControl(i,null,"chkSelect");
                if (chkEmpty.Checked == true)
                {
                    _arrayList = new ArrayList();
                    //_arrayList.Add(dgiItem.Cells[1].Text.ToString());
                    _arrayList.Add(grdAssignedDiagnosisCode.GetRowValues(i,"SZ_DIAGNOSIS_CODE_ID").ToString());
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        _arrayList.Add("");
                    }
                    else
                    {
                        _arrayList.Add("");
                    }

                    _arrayList.Add(txtCaseID.Text);
                    _arrayList.Add(txtCompanyID.Text);
                    _arrayList.Add(grdAssignedDiagnosisCode.GetRowValues(i, "SZ_PROCEDURE_GROUP_ID").ToString());
                    if (_associateDiagnosisCodeBO.DeleteAssociateDiagonisCode(_arrayList))
                    {
                    }
                    else
                    {
                        //szDiagIDs += "  " + dgiItem.Cells[2].Text.ToString() + ",";
                        szDiagIDs += "  " + grdAssignedDiagnosisCode.GetRowValues(i,"SZ_DIAGNOSIS_CODE").ToString() + ",";
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

                lblMsg.Text = szDiagIDs + " diagnosis code used in bills. You can not de-associate it.";
            }

            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");

            //Session["DiagnosysCode"] = "Diagnosys";
            //if (txtCurrentPage.Text == "CheckOut")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "parent.document.getElementById('divid2').style.visibility = 'hidden';window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='Bill_Sys_IM_CheckOut.aspx';", true);
            //    //  Response.Redirect("Bill_Sys_IM_CheckOut.aspx");
            //}
            //else if (txtCurrentPage.Text == "Notes")
            //{
            //    return;
            //}
            //else
            //{
            //    Response.Redirect("TM/RTFEditter.aspx");
            //}

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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected void grdSelectedDiagnosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           // grdSelectedDiagnosisCode.CurrentPageIndex = e.NewPageIndex;
            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
    //protected void grdAssignedDiagnosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    //{
    //    try
    //    {
    //       // grdAssignedDiagnosisCode.CurrentPageIndex = e.NewPageIndex;
    //        GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
    //        tabcontainerDiagnosisCode.ActiveTabIndex = 1;
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    public DataSet GET_DoctorSpeciality(String strDoctorID, String sz_companyId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds;
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("SP_Doctor_Speciality", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", strDoctorID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyId);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
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
            return null;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnTemplateManager_Click(object sender, EventArgs e)
    {

        if (btnTemplateManager.Text == "Synaptic Notes")
        {
            Response.Redirect("Bill_Sys_SynopticUpdate.aspx?Flag=SYN" + "&CaseID=" + txtCaseID.Text + "&cmp=" + txtCompanyID.Text + "&EID=" + sz_eventid);
        }
        else
        {
            Session["DiagnosysCode"] = "Diagnosys";
            if (txtCurrentPage.Text == "CheckOut")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "parent.document.getElementById('divid2').style.visibility = 'hidden';window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='Bill_Sys_IM_CheckOut.aspx';", true);
            }
            else
            {
                Response.Redirect("TM/RTFEditter.aspx");
            }
        }
    }   
}