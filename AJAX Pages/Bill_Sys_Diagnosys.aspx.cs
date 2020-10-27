/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_BillTransaction.aspx.cs
/*Purpose              :       To Add and Edit Bill Transaction
/*Author               :       Ananda Naphade
/*Date of creation     :       11 Aug 2010
/*Modified By          :
/*Modified Date        :
/************************************************************/

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Componend;
using System.Data.SqlClient;
using PDFValueReplacement;
using System.IO;
using log4net;
using mbs.LienBills;


public partial class Bill_Sys_Diagnosys : PageBase
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    Bill_Sys_DigosisCodeBO _digosisCodeBO;
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    private ArrayList _arrayList;
    Bill_Sys_CheckoutBO _obj_CheckOutBO;
    string DiagnosysList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            ds = new DataSet();
            _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
            txtCompanyID.Text =((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtUserId.Text = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();
            txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            extddlDiagnosisType.Flag_ID = txtCompanyID.Text;
            txtDiagnosisSetID.Text = _associateDiagnosisCodeBO.GetDiagnosisSetID();

            ds = _obj_CheckOutBO.GetDoctorUserID(txtUserId.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            txtDoctorId.Text = ds.Tables[0].Rows[0][0].ToString();

           ds= GET_DoctorSpeciality(txtDoctorId.Text,txtCompanyID.Text);
           txtSpeciality.Text = ds.Tables[0].Rows[0][0].ToString();


           GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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

    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            BindDiagnosisGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "<script>javascript:showDiagnosisCodePopup();</script>", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDiagnosisCodePopup();", true);            
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
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            //for (int i = 0; i < grdDiagonosisCode.Items.Count; i++)
            //{
            //    CheckBox chk = (CheckBox)grdDiagonosisCode.Items[i].FindControl("chkAssociateDiagnosisCode");
            //    if (chk.Checked)
            //    {
            //        _arrayList.Add(DiagnosysList + grdDiagonosisCode.Items[i].Cells[2].Text + ' ' + grdDiagonosisCode.Items[i].Cells[4].Text);                    
            //    }
            //}
            SaveDiagnosisCode();
          
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

    protected void grdDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            if (extddlDiagnosisType.Text == "NA")
            {
                BindDiagnosisGrid("");
            }
            else
            {
                BindDiagnosisGrid(extddlDiagnosisType.Text);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDiagnosisCodePopup();", true);
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

    private void BindDiagnosisGrid(string typeid)
    {
        _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            grdDiagonosisCode.DataSource = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, typeid);
            grdDiagonosisCode.DataBind();
        }
        catch (Exception ex)
        {          
        }
    }

    private void BindDiagnosisGrid(string typeid, string code, string description)
    {
        _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            DataSet ds = new DataSet();
            ds = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, typeid, code, description);
            grdDiagonosisCode.DataSource = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, typeid, code, description);
            grdDiagonosisCode.DataBind();
        }
        catch (Exception ex)
        {            
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='"../TM/RTFEditter.aspx?hidden=''"';", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "OnSave();", true);
        Session["DiagnosysCode"] = "Diagnosys";
        Response.Redirect("../TM/RTFEditter.aspx");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DiagnosysList = "";
        Session["DiagnosysList"] = "";
        for (int i = 0; i < grdDiagonosisCode.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)grdDiagonosisCode.Items[i].FindControl("chkAssociateDiagnosisCode");
            if (chk.Checked)
            {
                _arrayList.Add(DiagnosysList + grdDiagonosisCode.Items[i].Cells[2].Text + ' ' + grdDiagonosisCode.Items[i].Cells[4].Text);
                //DiagnosysList = DiagnosysList  + grdDiagonosisCode.Items[i].Cells[2].Text + ' ' + grdDiagonosisCode.Items[i].Cells[4].Text + ""; 
            }
        }
        Session["DiagnosysList"] = (ArrayList)_arrayList;
        //Session["DiagnosysList"] = DiagnosysList.ToString();
        lblMSG.Text = "Diagnosys Codes Updated Successfully";
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
                        _arrayList.Add("");
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
                lblMSG.Visible = true;
                lblMSG.Text = " Diagnosis code deassociated successfully ...!";
            }
            else
            {
                lblMSG.Visible = true;

                lblMSG.Text = szDiagIDs + " diagnosis code used in bills. You can not de-associate it.";
            }
            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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

    protected void grdAssignedDiagnosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdAssignedDiagnosisCode.CurrentPageIndex = e.NewPageIndex;
            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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
                foreach (DataGridItem dgiProcedureCode in grdNormalDgCode.Items)
                {
                    if (dgiProcedureCode.ItemIndex == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
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

             grdAssignedDiagnosisCode.DataSource = dt;
            grdAssignedDiagnosisCode.DataBind();

            //BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
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
                foreach (DataGridItem dgiProcedureCode in grdNormalDgCode.Items)
                {
                    if (dgiProcedureCode.ItemIndex == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
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
                    _arrayList.Add("");
                }
                _arrayList.Add(dgiProcedureCode.Cells[0].Text.ToString());
                _arrayList.Add(txtCompanyID.Text);
                _arrayList.Add(dgiProcedureCode.Cells[5].Text.ToString());
                _associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(_arrayList);
            }

            foreach (DataGridItem dgiProcedureCode in grdDiagonosisCode.Items)
            {
                CheckBox chkEmpty = (CheckBox)dgiProcedureCode.Cells[0].FindControl("chkAssociateDiagnosisCode");
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
                    _arrayList.Add(dgiProcedureCode.Cells[1].Text.ToString());
                    _arrayList.Add(txtCompanyID.Text);
                    _arrayList.Add(txtSpeciality.Text);
                    _associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(_arrayList);


                    _arrayListDiagnosys.Add(DiagnosysList + dgiProcedureCode.Cells[2].Text + ' ' + dgiProcedureCode.Cells[4].Text);
                }
            }

            Session["DiagnosysList"] = (ArrayList)_arrayList;
            lblMSG.Text = "Diagnosys Codes Added Successfully";
            btnOK.Enabled = false;            

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

    public DataSet  GET_DoctorSpeciality(String strDoctorID,String sz_companyId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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


  }

