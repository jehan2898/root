using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using DevExpress.Web;

public partial class AJAX_Pages_Bill_Sys_AssociateDiagnosisCodeVisit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        extddlSpecialityDia.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        extddlDiagnosisType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtCaseID.Text = Request.QueryString["CaseID"].ToString();
        txtEventID.Text = Request.QueryString["EventId"].ToString();
        txtEventProcID.Text = Request.QueryString["EventProcID"].ToString();
        btnAssign.Attributes.Add("OnClick", "callforSearch();");
        btnDeAssociate.Attributes.Add("OnClick", "callforSearch();");
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            
        }
        if (hdnSearch.Value!="true")
        {
            GetAssignedProcedureDiagnosisCode(txtCaseID.Text, txtCompanyID.Text, txtEventProcID.Text, "GET_PROCEDURE_ASSOCIATED_DIAGNOSIS_CODE");
            BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
        }
        else
            hdnSearch.Value = "";
            
    }

    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdDiagnosisCode.Visible = true;
            BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
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

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (extddlSpecialityDia.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Please select Specialty.');", true);
                extddlSpecialityDia.Focus();
                return;
            }
            SaveProcedureDiagnosisCode();
            lblMsg.Visible = true;
            lblMsg.Text = "Diagnosis Code Associated Successfully!!";
            carTabPage.ActiveTabIndex = 1;
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

    protected void btnDeAssociate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
        _dianosis_Association = (Bil_Sys_Associate_Diagnosis)Session["DIAGNOS_ASSOCIATION"];

        Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        DataSet _billDs = new DataSet();
        ArrayList _arrayList;
        try
        {
            string szDiagnosisCode = "";
            for (int i = 0; i < grdAssociatedDiagCode.VisibleRowCount; i++)
            {
                ASPxGridView _ASPxGridView = (ASPxGridView)grdid.FindControl("grdAssociatedDiagCode");
                GridViewDataColumn c = (GridViewDataColumn)grdAssociatedDiagCode.Columns[0];
                CheckBox checkBox = (CheckBox)grdAssociatedDiagCode.FindRowCellTemplateControl(i, c, "chkall");
                if (checkBox.Checked)
                {
                    _arrayList = new ArrayList();
                    szDiagnosisCode = Convert.ToString(grdAssociatedDiagCode.GetRowValues(i, "SZ_ASSOCIATED_DIAG_CODE_ID"));
                    _arrayList.Add(szDiagnosisCode);
                    _arrayList.Add(txtCaseID.Text);
                    _arrayList.Add(txtCompanyID.Text);
                    _arrayList.Add("");
                    _associateDiagnosisCodeBO.DeleteAssociateProcedureDiagonisCode(_arrayList);
                }
            }
            GetAssignedProcedureDiagnosisCode(txtCaseID.Text, txtCompanyID.Text, txtEventProcID.Text, "GET_PROCEDURE_ASSOCIATED_DIAGNOSIS_CODE");
            lblMsg.Visible = true;
            lblMsg.Text = "Diagnosis Code De-Associated Successfully!!";
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

    private void BindGrid(string typeid, string code, string description)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_DigosisCodeBO _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            grdDiagnosisCode.DataSource = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, typeid, code, description);
            grdDiagnosisCode.DataBind();

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

    public void SaveProcedureDiagnosisCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
        _dianosis_Association = (Bil_Sys_Associate_Diagnosis)Session["DIAGNOS_ASSOCIATION"];

        Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        DataSet _billDs = new DataSet();
        ArrayList _arrayList;
        try
        {
            string szDiagnosisCode = "";
            for (int i = 0; i < grdDiagnosisCode.VisibleRowCount; i++)
            {
                ASPxGridView _ASPxGridView = (ASPxGridView)grdid.FindControl("grdDiagnosisCode");
                GridViewDataColumn c = (GridViewDataColumn)grdDiagnosisCode.Columns[0];
                CheckBox checkBox = (CheckBox)grdDiagnosisCode.FindRowCellTemplateControl(i, c, "chkall");
                if (checkBox!=null)
                {
                    if (checkBox.Checked)
                    {
                        _arrayList = new ArrayList();
                        _arrayList.Add(txtEventProcID.Text);
                        _arrayList.Add(txtCaseID.Text);
                        _arrayList.Add(txtEventID.Text);
                        szDiagnosisCode = Convert.ToString(grdDiagnosisCode.GetRowValues(i, "SZ_DIAGNOSIS_CODE_ID"));
                        _arrayList.Add(szDiagnosisCode);
                        _arrayList.Add(extddlSpecialityDia.Text);
                        _arrayList.Add(txtCompanyID.Text);
                        _arrayList.Add("");
                        _associateDiagnosisCodeBO.SaveProcedureAssociateDignosisCode(_arrayList);
                    }
                }
            }
            GetAssignedProcedureDiagnosisCode(txtCaseID.Text, txtCompanyID.Text, txtEventProcID.Text, "GET_PROCEDURE_ASSOCIATED_DIAGNOSIS_CODE");
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

    private void GetAssignedProcedureDiagnosisCode(string caseID, string companyId, string szEventProcID, string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet ds = new DataSet();
            ds = _associateDiagnosisCodeBO.GetProcedureDiagnosisCode(caseID, companyId, szEventProcID, flag);
            grdAssociatedDiagCode.DataSource = ds;
            grdAssociatedDiagCode.DataBind();
            
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

    //protected void btnDeAssociate_Click(object sender, EventArgs e)
    //{
    //    Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
    //    _dianosis_Association = (Bil_Sys_Associate_Diagnosis)Session["DIAGNOS_ASSOCIATION"];

    //    Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
    //    DataSet _billDs = new DataSet();
    //    ArrayList _arrayList;
    //    try
    //    {
    //        string szDiagnosisCode = "";
    //        for (int i = 0; i < grdAssociatedDiagCode.VisibleRowCount; i++)
    //        {
    //            ASPxGridView _ASPxGridView = (ASPxGridView)grdid.FindControl("grdAssociatedDiagCode");
    //            GridViewDataColumn c = (GridViewDataColumn)grdAssociatedDiagCode.Columns[0];
    //            CheckBox checkBox = (CheckBox)grdAssociatedDiagCode.FindRowCellTemplateControl(i, c, "chkall");
    //            if (checkBox.Checked)
    //            {
    //                _arrayList = new ArrayList();
    //                szDiagnosisCode = Convert.ToString(grdAssociatedDiagCode.GetRowValues(i, "SZ_ASSOCIATED_DIAG_CODE_ID"));
    //                _arrayList.Add(szDiagnosisCode);
    //                _arrayList.Add(txtCaseID.Text);
    //                _arrayList.Add(txtCompanyID.Text);
    //                _arrayList.Add("");
    //                _associateDiagnosisCodeBO.DeleteAssociateProcedureDiagonisCode(_arrayList);
    //            }
    //        }
    //        GetAssignedProcedureDiagnosisCode(txtCaseID.Text, txtCompanyID.Text, txtEventProcID.Text, "GET_PROCEDURE_ASSOCIATED_DIAGNOSIS_CODE");
    //        lblMsg.Visible = true;
    //        lblMsg.Text = "Diagnosis Code De-Associated Successfully!!";
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

   

    //private void GetProcedureDiagnosisCode(string caseID, string companyId, string szEventID, string flag)
    //{
    //    _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        // please check the below function.. it returns all the rows from the database when a doctor is selected
    //        ds = _associateDiagnosisCodeBO.GetProcedureDiagnosisCode(caseID, companyId, szEventID, flag);
    //        grdNormalDgCode.DataSource = ds.Tables[0];
    //        grdNormalDgCode.DataBind();
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
    //        dt.Columns.Add("SZ_DIAGNOSIS_CODE");
    //        dt.Columns.Add("SZ_DESCRIPTION");
    //        dt.Columns.Add("SZ_COMPANY_ID");
    //        dt.Columns.Add("SZ_PROCEDURE_GROUP");
    //        dt.Columns.Add("SZ_PROCEDURE_GROUP_ID");
    //        DataRow dr;
    //        DataTable dtNew = new DataTable();
    //        dtNew.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
    //        dtNew.Columns.Add("SZ_DIAGNOSIS_CODE");
    //        dtNew.Columns.Add("SZ_DESCRIPTION");
    //        dtNew.Columns.Add("SZ_COMPANY_ID");
    //        dtNew.Columns.Add("SZ_PROCEDURE_GROUP");
    //        dtNew.Columns.Add("SZ_PROCEDURE_GROUP_ID");
    //        DataRow drNew;
    //        for (int intLoop = 0; intLoop < ds.Tables[0].Rows.Count; intLoop++)
    //        {
    //            foreach (DataGridItem dgiProcedureCode in grdNormalDgCode.Items)
    //            {
    //                if (dgiProcedureCode.ItemIndex == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
    //                {
    //                    dr = dt.NewRow();
    //                    dr["SZ_DIAGNOSIS_CODE_ID"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE_ID"].ToString();
    //                    dr["SZ_DIAGNOSIS_CODE"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE"].ToString();
    //                    dr["SZ_DESCRIPTION"] = ds.Tables[0].Rows[intLoop]["SZ_DESCRIPTION"].ToString();
    //                    dr["SZ_COMPANY_ID"] = ds.Tables[0].Rows[intLoop]["SZ_COMPANY_ID"].ToString();
    //                    dr["SZ_PROCEDURE_GROUP"] = ds.Tables[0].Rows[intLoop]["Speciality"].ToString();
    //                    dr["SZ_PROCEDURE_GROUP_ID"] = ds.Tables[0].Rows[intLoop]["SZ_PROCEDURE_GROUP_ID"].ToString();
    //                    dt.Rows.Add(dr);

    //                }

    //            }
    //        }

    //        grdSelectedDiagnosisCode.DataSource = dt;
    //        grdSelectedDiagnosisCode.DataBind();
    //        lblDiagnosisCount.Text = dt.Rows.Count.ToString();

    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}
}