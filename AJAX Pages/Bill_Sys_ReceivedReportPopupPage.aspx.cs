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
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class Bill_Sys_ReceivedReportPopupPage : PageBase
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    Bill_Sys_DigosisCodeBO _digosisCodeBO;
    Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
    Bill_Sys_NF3_Template objNF3Template;
    string strLinkPath = null;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private DataSet _ds;
    private ArrayList _arrayList;
    private Bill_Sys_SystemObject objSystemObject;
    Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();

    //private void BindGrid(string typeid, string code, string description)
    //{
    //    this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
    //    try
    //    {
    //        this.grdNormalDgCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid, code, description);
    //        this.grdNormalDgCode.DataBind();
    //    }
    //    catch (Exception exception)
    //    {
    //        string str = exception.Message.ToString().Replace("\n", " ");
    //        base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str);
    //    }
    //}

    //protected void Btn_Update_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (this.extddlReadingDoctor.Text != "---Select---")
    //        {
    //            Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
    //            diagnosis = (Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"];
    //            new Bill_Sys_ProcedureCode_BO().UpdateReadingDoctor(Convert.ToInt32(diagnosis.EventProcID), this.extddlReadingDoctor.Text);
    //            this.lblMsg.Text = "Doctor Updated Sucessfully";
    //            this.lblMsg.Visible = true;

    //            if (txtSource.Text.ToString().ToLower() == "rr")
    //            {
    //                //window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='/application/AJAX Pages/Bill_Sys_ReffPaidBills.aspx';
    //                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "parent.document.getElementById('ctl00_ContentPlaceHolder1_hfupadate').value = 'true';", true);
    //            }
    //        }
    //        else
    //        {
    //            this.lblMsg.Text = "Please Select Doctor To Update";
    //            this.lblMsg.Visible = true;
    //        }
    //    }
    //    catch (Exception)
    //    {
    //    }
    //}

    //protected void btnAssign_Click(object sender, EventArgs e)
    //{
    //    this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
    //    try
    //    {
    //        this.SaveProcedureDiagnosisCode();
    //        this.lblMsg.Visible = true;
    //        this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
    //        this.lblMsg.Text = "Case Associate Diagnosis  Saved Successfully ...!";
    //    }
    //    catch (Exception exception)
    //    {
    //        string str = exception.Message.ToString().Replace("\n", " ");
    //        base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str);
    //    }
    //}

    //protected void btnDeassociateDiagCode_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string str = "";
    //        foreach (DataGridItem item in this.grdAssignedDiagnosisCode.Items)
    //        {
    //            CheckBox box = (CheckBox)item.Cells[0].FindControl("chkSelect");
    //            if (box.Checked)
    //            {
    //                this._arrayList = new ArrayList();
    //                this._arrayList.Add(item.Cells[1].Text.ToString());
    //                this._arrayList.Add(this.txtCaseID.Text);
    //                this._arrayList.Add(this.txtCompanyID.Text);
    //                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
    //                {
    //                    this._arrayList.Add("");
    //                }
    //                else
    //                {
    //                    this._arrayList.Add("");
    //                }
    //                if (!this._associateDiagnosisCodeBO.DeleteAssociateProcedureDiagonisCode(this._arrayList))
    //                {
    //                    str = str + "  " + item.Cells[2].Text.ToString() + ",";
    //                }
    //            }
    //        }
    //        if (str == "")
    //        {
    //            this.lblMsg.Visible = true;
    //            this.lblMsg.Text = " Diagnosis code deassociated successfully ...!";
    //        }
    //        else
    //        {
    //            this.lblMsg.Visible = true;
    //            this.lblMsg.Text = str + " diagnosis code used in bills. You can not de-associate it.";
    //        }
    //        Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
    //        diagnosis = (Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"];
    //        this.GetProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
    //        this.grdAssignedDiagnosisCode.CurrentPageIndex = 0;
    //        this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
    //        this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
    //    }
    //    catch (Exception exception)
    //    {
    //        string str2 = exception.Message.ToString().Replace("\n", " ");
    //        base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
    //    }
    //}

    //protected void btnSeacrh_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        this.grdNormalDgCode.Visible = true;
    //        this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
    //    }
    //    catch
    //    {
    //    }
    //}

    //private void GetAssignedDiagnosisCode(string caseID, string companyId, string doctorId, string flag)
    //{
    //    this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
    //    try
    //    {
    //        DataSet set = new DataSet();
    //        set = this._associateDiagnosisCodeBO.GetDiagnosisCode(caseID, companyId, doctorId, flag);
    //        this.grdNormalDgCode.DataSource = set.Tables[0];
    //        this.grdNormalDgCode.DataBind();
    //        DataTable table = new DataTable();
    //        table.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
    //        table.Columns.Add("SZ_DIAGNOSIS_CODE");
    //        table.Columns.Add("SZ_DESCRIPTION");
    //        table.Columns.Add("SZ_COMPANY_ID");
    //        table.Columns.Add("SZ_PROCEDURE_GROUP");
    //        table.Columns.Add("SZ_PROCEDURE_GROUP_ID");
    //        DataTable table2 = new DataTable();
    //        table2.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
    //        table2.Columns.Add("SZ_DIAGNOSIS_CODE");
    //        table2.Columns.Add("SZ_DESCRIPTION");
    //        table2.Columns.Add("SZ_COMPANY_ID");
    //        table2.Columns.Add("SZ_PROCEDURE_GROUP");
    //        table2.Columns.Add("SZ_PROCEDURE_GROUP_ID");
    //        for (int i = 0; i < set.Tables[0].Rows.Count; i++)
    //        {
    //            foreach (DataGridItem item in this.grdNormalDgCode.Items)
    //            {
    //                if ((item.ItemIndex == i) && ((bool)set.Tables[0].Rows[i]["CHECKED"]))
    //                {
    //                    DataRow row = table.NewRow();
    //                    row["SZ_DIAGNOSIS_CODE_ID"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE_ID"].ToString();
    //                    row["SZ_DIAGNOSIS_CODE"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE"].ToString();
    //                    row["SZ_DESCRIPTION"] = set.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString();
    //                    row["SZ_COMPANY_ID"] = set.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
    //                    row["SZ_PROCEDURE_GROUP"] = set.Tables[0].Rows[i]["Speciality"].ToString();
    //                    row["SZ_PROCEDURE_GROUP_ID"] = set.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
    //                    table.Rows.Add(row);
    //                }
    //            }
    //        }
    //        this.grdAssignedDiagnosisCode.DataSource = table;
    //        this.grdAssignedDiagnosisCode.DataBind();
    //    }
    //    catch (Exception exception)
    //    {
    //        string str = exception.Message.ToString().Replace("\n", " ");
    //        base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str);
    //    }
    //}

    private void GetAssignedProcedureDiagnosisCode(string caseID, string companyId, string szEventProcID, string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet set = new DataSet();
            set = this._associateDiagnosisCodeBO.GetProcedureDiagnosisCode(caseID, companyId, szEventProcID, flag);
            //this.grdNormalDgCode.DataSource = set.Tables[0];
            //this.grdNormalDgCode.DataBind();
            DataTable table = new DataTable();
            table.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            table.Columns.Add("SZ_DIAGNOSIS_CODE");
            table.Columns.Add("SZ_DESCRIPTION");
            table.Columns.Add("SZ_COMPANY_ID");
            table.Columns.Add("SZ_PROCEDURE_GROUP");
            table.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataTable table2 = new DataTable();
            table2.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            table2.Columns.Add("SZ_DIAGNOSIS_CODE");
            table2.Columns.Add("SZ_DESCRIPTION");
            table2.Columns.Add("SZ_COMPANY_ID");
            table2.Columns.Add("SZ_PROCEDURE_GROUP");
            table2.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                if (((bool)set.Tables[0].Rows[i]["CHECKED"]))
                {
                    DataRow row = table.NewRow();
                    row["SZ_DIAGNOSIS_CODE_ID"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                    row["SZ_DIAGNOSIS_CODE"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE"].ToString();
                    row["SZ_DESCRIPTION"] = set.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString();
                    row["SZ_COMPANY_ID"] = set.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
                    row["SZ_PROCEDURE_GROUP"] = set.Tables[0].Rows[i]["Speciality"].ToString();
                    row["SZ_PROCEDURE_GROUP_ID"] = set.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                    table.Rows.Add(row);
                }

            }
            this.grdAssignedDiagnosisCode.DataSource = table;
            this.grdAssignedDiagnosisCode.DataBind();
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

    //private void GetDiagnosisCode(string caseID, string companyId, string doctorId, string flag)
    //{
    //    this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
    //    try
    //    {
    //        DataSet set = new DataSet();
    //        set = this._associateDiagnosisCodeBO.GetDiagnosisCode(caseID, companyId, doctorId, flag);
    //        this.grdNormalDgCode.DataSource = set.Tables[0];
    //        this.grdNormalDgCode.DataBind();
    //        DataTable table = new DataTable();
    //        table.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
    //        table.Columns.Add("SZ_DIAGNOSIS_CODE");
    //        table.Columns.Add("SZ_DESCRIPTION");
    //        table.Columns.Add("SZ_COMPANY_ID");
    //        table.Columns.Add("SZ_PROCEDURE_GROUP");
    //        table.Columns.Add("SZ_PROCEDURE_GROUP_ID");
    //        DataTable table2 = new DataTable();
    //        table2.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
    //        table2.Columns.Add("SZ_DIAGNOSIS_CODE");
    //        table2.Columns.Add("SZ_DESCRIPTION");
    //        table2.Columns.Add("SZ_COMPANY_ID");
    //        table2.Columns.Add("SZ_PROCEDURE_GROUP");
    //        table2.Columns.Add("SZ_PROCEDURE_GROUP_ID");
    //        for (int i = 0; i < set.Tables[0].Rows.Count; i++)
    //        {
    //            foreach (DataGridItem item in this.grdNormalDgCode.Items)
    //            {
    //                if ((item.ItemIndex == i) && ((bool)set.Tables[0].Rows[i]["CHECKED"]))
    //                {
    //                    DataRow row = table.NewRow();
    //                    row["SZ_DIAGNOSIS_CODE_ID"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE_ID"].ToString();
    //                    row["SZ_DIAGNOSIS_CODE"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE"].ToString();
    //                    row["SZ_DESCRIPTION"] = set.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString();
    //                    row["SZ_COMPANY_ID"] = set.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
    //                    row["SZ_PROCEDURE_GROUP"] = set.Tables[0].Rows[i]["Speciality"].ToString();
    //                    row["SZ_PROCEDURE_GROUP_ID"] = set.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
    //                    table.Rows.Add(row);
    //                }
    //            }
    //        }
    //        this.grdSelectedDiagnosisCode.DataSource = table;
    //        this.grdSelectedDiagnosisCode.DataBind();
    //        this.lblDiagnosisCount.Text = table.Rows.Count.ToString();
    //    }
    //    catch (Exception exception)
    //    {
    //        string str = exception.Message.ToString().Replace("\n", " ");
    //        base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str);
    //    }
    //}

    private void GetProcedureDiagnosisCode(string caseID, string companyId, string szEventID, string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet set = new DataSet();
            set = this._associateDiagnosisCodeBO.GetProcedureDiagnosisCode(caseID, companyId, szEventID, flag);
            //this.grdNormalDgCode.DataSource = set.Tables[0];
            //this.grdNormalDgCode.DataBind();
            DataTable table = new DataTable();
            table.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            table.Columns.Add("SZ_DIAGNOSIS_CODE");
            table.Columns.Add("SZ_DESCRIPTION");
            table.Columns.Add("SZ_COMPANY_ID");
            table.Columns.Add("SZ_PROCEDURE_GROUP");
            table.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataTable table2 = new DataTable();
            table2.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            table2.Columns.Add("SZ_DIAGNOSIS_CODE");
            table2.Columns.Add("SZ_DESCRIPTION");
            table2.Columns.Add("SZ_COMPANY_ID");
            table2.Columns.Add("SZ_PROCEDURE_GROUP");
            table2.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                if (((bool)set.Tables[0].Rows[i]["CHECKED"]))
                {
                    DataRow row = table.NewRow();
                    row["SZ_DIAGNOSIS_CODE_ID"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                    row["SZ_DIAGNOSIS_CODE"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE"].ToString();
                    row["SZ_DESCRIPTION"] = set.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString();
                    row["SZ_COMPANY_ID"] = set.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
                    row["SZ_PROCEDURE_GROUP"] = set.Tables[0].Rows[i]["Speciality"].ToString();
                    row["SZ_PROCEDURE_GROUP_ID"] = set.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                    table.Rows.Add(row);
                }

            }
            //this.grdSelectedDiagnosisCode.DataSource = table;
            //this.grdSelectedDiagnosisCode.DataBind();
            //this.lblDiagnosisCount.Text = table.Rows.Count.ToString();
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
    //        this.grdAssignedDiagnosisCode.CurrentPageIndex = e.NewPageIndex;
    //        Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
    //        this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"]).EventProcID, "GET_DIAGNOSIS_CODE");
    //        this.tabcontainerDiagnosisCode.ActiveTabIndex=1;
    //    }
    //    catch (Exception exception)
    //    {
    //        string str = exception.Message.ToString().Replace("\n", " ");
    //        base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str);
    //    }
    //}

    //protected void grdNormalDgCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    //{
    //    try
    //    {
    //        this.grdNormalDgCode.CurrentPageIndex = e.NewPageIndex;
    //        Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
    //        this.GetProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"]).EventProcID, "GET_DIAGNOSIS_CODE");
    //    }
    //    catch (Exception exception)
    //    {
    //        string str = exception.Message.ToString().Replace("\n", " ");
    //        base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str);
    //    }
    //}

    //protected void grdNormalDgCode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //}

    protected void Page_Load(object sender, EventArgs e)
    {

        string strContextKey = (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) + "," + rbl_SZ_TYPE_ID.SelectedValue;
        this.ajDignosisCode.ContextKey = strContextKey;


        this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        //this.txtDiagonosisCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerDiagnosisCode_tabpnlAssociate_btnSeacrh').click(); return false;}} else {alert('Other');return true;}; ");
        if (!base.IsPostBack)
        {
            this.extddlSpecialityDia.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlReadingDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
           
            this.extddlDiagnosisType.Flag_ID = this.txtCompanyID.Text;
            Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
            _dianosis_Association = (Bil_Sys_Associate_Diagnosis)Session["DIAGNOS_ASSOCIATION"];
            extddlSpecialityDia.Text = _dianosis_Association.ProceuderGroupId;

            this.txtCaseID.Text = _dianosis_Association.CaseID;

            extddlReadingDoctor.Text = _dianosis_Association.DoctorID;

            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            GetDiagnosisCode(txtCaseID.Text, txtCompanyID.Text, "", "GET_DIAGNOSIS_CODE");
            tabcontainerDiagnosisCode.ActiveTabIndex = 0; 
        }

      
    }

    //private void SaveDiagnosisCode()
    //{
    //    this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
    //    new DataSet();
    //    try
    //    {
    //        ArrayList list;
    //        this._associateDiagnosisCodeBO.DeleteCaseAssociateDignosisCodeWithProcCode(this.txtCaseID.Text, this.txtCompanyID.Text, "");
    //        this.txtDiagnosisSetID.Text = this._associateDiagnosisCodeBO.GetDiagnosisSetID();
    //        Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
    //        diagnosis = (Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"];
    //        foreach (DataGridItem item in this.grdSelectedDiagnosisCode.Items)
    //        {
    //            list = new ArrayList();
    //            list.Add(this.txtDiagnosisSetID.Text);
    //            list.Add(this.txtCaseID.Text);
    //            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
    //            {
    //                list.Add("");
    //            }
    //            else
    //            {
    //                list.Add("");
    //            }
    //            list.Add(item.Cells[0].Text.ToString());
    //            list.Add(this.txtCompanyID.Text);
    //            list.Add(item.Cells[5].Text.ToString());
    //            this._associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(list);
    //        }
    //        foreach (DataGridItem item2 in this.grdNormalDgCode.Items)
    //        {
    //            CheckBox box = (CheckBox)item2.Cells[1].FindControl("chkSelect");
    //            if (box.Checked)
    //            {
    //                list = new ArrayList();
    //                list.Add(this.txtDiagnosisSetID.Text);
    //                list.Add(this.txtCaseID.Text);
    //                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
    //                {
    //                    list.Add("");
    //                }
    //                else
    //                {
    //                    list.Add("");
    //                }
    //                list.Add(item2.Cells[1].Text.ToString());
    //                list.Add(this.txtCompanyID.Text);
    //                list.Add(diagnosis.ProceuderGroupId);
    //                this._associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(list);
    //            }
    //        }
    //        this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
    //        this.GetProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
    //    }
    //    catch (Exception exception)
    //    {
    //        string str = exception.Message.ToString().Replace("\n", " ");
    //        base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str);
    //    }
    //}

    //public void SaveProcedureDiagnosisCode()
    //{
    //    Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
    //    diagnosis = (Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"];
    //    this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
    //    new DataSet();
    //    try
    //    {
    //        ArrayList list;
    //        this._associateDiagnosisCodeBO.DeleteProcedureAssociateDignosisCode(this.txtCaseID.Text, this.txtCompanyID.Text, diagnosis.EventProcID);
    //        foreach (DataGridItem item in this.grdSelectedDiagnosisCode.Items)
    //        {
    //            list = new ArrayList();
    //            list.Add(diagnosis.EventProcID);
    //            list.Add(this.txtCaseID.Text);
    //            list.Add(this.Session["I_Event_ID"].ToString());
    //            list.Add(item.Cells[0].Text.ToString());
    //            list.Add(item.Cells[5].Text.ToString());
    //            list.Add(this.txtCompanyID.Text);
    //            list.Add(this.txtDiagnosisSetID.Text);
    //            this._associateDiagnosisCodeBO.SaveProcedureAssociateDignosisCode(list);
    //        }
    //        foreach (DataGridItem item2 in this.grdNormalDgCode.Items)
    //        {
    //            CheckBox box = (CheckBox)item2.Cells[1].FindControl("chkSelect");
    //            if (box.Checked)
    //            {
    //                list = new ArrayList();
    //                list.Add(diagnosis.EventProcID);
    //                list.Add(this.txtCaseID.Text);
    //                //list.Add(this.Session["I_Event_ID"]);
    //                list.Add(item2.Cells[1].Text.ToString());
    //                list.Add(this.extddlSpecialityDia.Text);
    //                list.Add(this.txtCompanyID.Text);
    //                list.Add(this.txtDiagnosisSetID.Text);
    //                this._associateDiagnosisCodeBO.SaveProcedureAssociateDignosisCode(list);
    //            }
    //        }
    //        this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, this.txtCompanyID.Text, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
    //        this.GetProcedureDiagnosisCode(this.txtCaseID.Text, this.txtCompanyID.Text, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
    //    }
    //    catch (Exception exception)
    //    {
    //        string str = exception.Message.ToString().Replace("\n", " ");
    //        base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str);
    //    }
    //}
    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.BindDiagnosisGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text, rbl_SZ_TYPE_ID.SelectedValue);
            //ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", "showDiagnosisCodePopup();", true);
            //this.ModalPopupExtender1.Show();
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

    private void BindDiagnosisGrid(string typeid, string code, string description, string SZ_TYPE_ID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdDiagonosisCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid, code, description, SZ_TYPE_ID);
            this.grdDiagonosisCode.DataBind();
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

    public DataSet GetDiagnosisCodeReferalList(string companyID, string strtypeid, string code, string description, string SZ_TYPE_ID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DIAGNOSIS_CODE_FOR_REFERAL", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            if (strtypeid != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_TYPE_ID", strtypeid); }
            if (code != "") { sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE", code); }
            if (description != "") { sqlCmd.Parameters.AddWithValue("@SZ_DESCRIPTION", description); }
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE_ID", SZ_TYPE_ID);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
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
            this.grdDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            if (this.extddlDiagnosisType.Text == "NA")
            {
                this.BindDiagnosisGrid("");
            }
            else
            {
                this.BindDiagnosisGrid(this.extddlDiagnosisType.Text);
            }
            //ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", "showDiagnosisCodePopup();", true);
            //this.ModalPopupExtender1.Show();
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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdDiagonosisCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid);
            this.grdDiagonosisCode.DataBind();
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

    protected void lnkAddDignosisCode_OnClick(object sender, EventArgs e)
    {
        string[] dgarr = txtSearchDignosisCode.Text.Split(',');
        ListItem item = new ListItem(dgarr[1], dgarr[0]);
        //if (!this.lstDgCodes.Items.Contains(item))
        //{
        //    this.lstDgCodes.Items.Add(item);
        //    ALSelectedDignosisCodes.Add(item);
        //    txtSearchDignosisCode.Text = "";
        //}
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
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
            txtDiagnosisSetID.Text = _associateDiagnosisCodeBO.GetDiagnosisSetID();
            Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
            _dianosis_Association = (Bil_Sys_Associate_Diagnosis)Session["DIAGNOS_ASSOCIATION"];


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
                //_arrayList.Add(_dianosis_Association.ProceuderGroupId);
                _arrayList.Add(dgiProcedureCode.Cells[5].Text.ToString());
                _associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(_arrayList);



               
            }
            string Codes = hdnDiagnosis.Value.Replace("--,", "`");
            Codes = Codes.Replace("--", "");
            string[] DiaCode = Codes.Split('`');
            for (int i = 0; i < DiaCode.Length; i++)
            {
                string[] Values = DiaCode[i].Split('~');
                string procedurid = extddlSpecialityDia.Text;

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
                _arrayList.Add(Values[3].ToString());
                _arrayList.Add(txtCompanyID.Text);
                _arrayList.Add(_dianosis_Association.ProceuderGroupId);
                _associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(_arrayList);
            }

            Response.Write("<script>alert('Diagnosis Codes Associated Successfully')</script>");

            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            //new code from associate diagnosis page
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

    #region "New code from Associate Diagnosis code page"
    private void GetDiagnosisCode(string caseID, string companyId, string doctorId, string flag)
    {
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet ds = new DataSet();
            // please check the below function.. it returns all the rows from the database when a doctor is selected
            ds = _associateDiagnosisCodeBO.GetDiagnosisCode(caseID, companyId, doctorId, flag);

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
                
                    if ( ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
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

            grdSelectedDiagnosisCode.DataSource = dt;
            grdSelectedDiagnosisCode.DataBind();
            lblDiagnosisCount.Text = dt.Rows.Count.ToString();

        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }
    #endregion

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
                    if ( ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
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
                    _arrayList.Add(txtCaseID.Text);
                    _arrayList.Add(txtCompanyID.Text);
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        _arrayList.Add("");
                    }
                    else
                    {
                        _arrayList.Add("");
                    }

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

                lblMsg.Text = szDiagIDs + " diagnosis code used in bills. You can not de-associate it.";
            }
            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            grdAssignedDiagnosisCode.CurrentPageIndex = 0;
            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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

    protected void Btn_Update_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (extddlReadingDoctor.Text != "---Select---")
            {
                Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
                _dianosis_Association = (Bil_Sys_Associate_Diagnosis)Session["DIAGNOS_ASSOCIATION"];
                Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                _bill_Sys_ProcedureCode_BO.UpdateReadingDoctor(Convert.ToInt32(_dianosis_Association.EventProcID), extddlReadingDoctor.Text);
                lblMsg.Text = "Doctor Updated Sucessfully";
                lblMsg.Visible = true;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_ReffPaidBills.aspx';</script>");
            }
            else
            {

                lblMsg.Text = "Please Select Doctor To Update";
                lblMsg.Visible = true;
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

    protected void tabcontainerDiagnosisCode_ActiveTabChanged(object sender, EventArgs e)
    {
        int Index = tabcontainerDiagnosisCode.ActiveTabIndex;
        if (Index == 0)
        {
            lblMsg.Text = "";        
        }
        else if (Index == 1)
        {
            lblMsg.Text = "";
        }
        else if (Index ==2)
        {
            lblMsg.Text ="";
        }
    }
}
