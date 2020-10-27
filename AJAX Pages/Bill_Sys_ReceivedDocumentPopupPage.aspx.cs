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

public partial class AJAX_Pages_Bill_Sys_ReceivedDocumentPopupPage : PageBase
{

    Bill_Sys_DigosisCodeBO _digosisCodeBO;
    Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
    Bill_Sys_NF3_Template objNF3Template;
    string strLinkPath = null;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private DataSet _ds;
    DataTable _patientGrid_dt = new DataTable();
    private ArrayList _arrayList;
    private string _CompanyID = "";
    string strContextKey;

    private void BindGrid(string typeid, string code, string description, string SZ_TYPE_ID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdNormalDgCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid, code, description, SZ_TYPE_ID);
            this.grdNormalDgCode.DataBind();
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ArrayList list = new ArrayList();
        for (int i = 0; i < this.grdProCode.Items.Count; i++)
        {
            CheckBox box = (CheckBox)this.grdProCode.Items[i].FindControl("chkSelectProc");
            if (box.Checked)
            {
                list.Add(this.grdProCode.Items[i].Cells[0].Text);
            }
        }
        ArrayList list2 = (ArrayList)this.Session["EVENT_ID"];
        if ((list.Count > 0) && (list2.Count > 0))
        {
            for (int j = 0; j < list.Count; j++)
            {
                for (int k = 0; k < list2.Count; k++)
                {
                    new Bill_Sys_CheckoutBO().saveProcCodes(list2[k].ToString(), list[j].ToString());
                }
            }
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            this.SaveDiagnosisCode();
            this.lblMsg.Visible = true;
            this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text, rbl_SZ_TYPE_ID.SelectedValue);
            this.lblMsg.Text = "Case Associate Diagnosis  Saved Successfully ...!";
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
            string str = "";
            foreach (DataGridItem item in this.grdAssignedDiagnosisCode.Items)
            {
                CheckBox box = (CheckBox)item.Cells[0].FindControl("chkSelect");
                if (box.Checked)
                {
                    this._arrayList = new ArrayList();
                    this._arrayList.Add(item.Cells[1].Text.ToString());
                    this._arrayList.Add(this.txtCaseID.Text);
                    this._arrayList.Add(this.txtCompanyID.Text);
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    {
                        this._arrayList.Add("");
                    }
                    else
                    {
                        this._arrayList.Add("");
                    }
                    this._arrayList.Add(item.Cells[5].Text.ToString());
                   
                    if (!this._associateDiagnosisCodeBO.DeleteAssociateDiagonisCode(this._arrayList))
                    {
                        str = str + "  " + item.Cells[2].Text.ToString() + ",";
                    }
                }
            }
            if (str == "")
            {
                this.lblMsg.Visible = true;
                this.lblMsg.Text = "Diagnosis code deassociated successfully ...!";
            }
            else
            {
                this.lblMsg.Visible = true;
                this.lblMsg.Text = str + " diagnosis code used in bills. You can not de-associate it.";
            }
            this.GetDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            this.grdAssignedDiagnosisCode.CurrentPageIndex = 0;
            this.GetAssignedDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text, rbl_SZ_TYPE_ID.SelectedValue);
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

    //protected void btnSeacrh_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //this.grdNormalDgCode.Visible = true;
    //        this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text, rbl_SZ_TYPE_ID.SelectedValue);
    //    }
    //    catch
    //    {
    //    }
    //}

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.objNF3Template = new Bill_Sys_NF3_Template();
        try
        {
            string str = this.objNF3Template.getPhysicalPath();
            int num = 0;
            if (this.extddlReadingDoctor.Text == "NA")
            {
                this.Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select Reading Doctor !');</script>");
            }
            else if (!this.fuUploadReport.HasFile)
            {
                this.Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');</script>");
            }
            else
            {
                ArrayList list = (ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"];
                string extension = "";
                string result = "";
                extension = Path.GetExtension(fuUploadReport.FileName);
                result = Path.GetFileNameWithoutExtension(fuUploadReport.FileName);
                string FileName = result + "_" + DateTime.Now.ToString("MMddyyyysstt") + extension;
                foreach (object obj2 in list)
                {
                    Bil_Sys_Associate_Diagnosis diagnosis = (Bil_Sys_Associate_Diagnosis)obj2;
                    string companyName = "";
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == ((Bil_Sys_Associate_Diagnosis)((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"])[0]).CompanyId))
                    {
                        companyName = this.objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    }
                    else
                    {
                        companyName = this.objNF3Template.GetCompanyName(((Bil_Sys_Associate_Diagnosis)((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"])[0]).CompanyId);
                    }
                    companyName = companyName + "/" + this.txtCaseID.Text + "/No Fault File/Medicals/" + diagnosis.ProceuderGroupName.ToString() + "/";
                    this.strLinkPath = companyName + FileName;
                    if (!Directory.Exists(str + companyName))
                    {
                        Directory.CreateDirectory(str + companyName);
                    }
                    this.fuUploadReport.SaveAs(str + companyName + FileName);
                    ArrayList list2 = new ArrayList();
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == ((Bil_Sys_Associate_Diagnosis)((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"])[0]).CompanyId))
                    {
                        list2.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    }
                    else
                    {
                        list2.Add(((Bil_Sys_Associate_Diagnosis)((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"])[0]).CompanyId);
                    }
                    list2.Add(this.txtCaseID.Text);
                    list2.Add(FileName);
                    list2.Add(companyName);
                    list2.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                    list2.Add(diagnosis.ProceuderGroupName.ToString());
                    num = this.objNF3Template.saveReportInDocumentManager(list2);
                }
                bool flag = false;
                bool flag2 = false;
                foreach (object obj3 in list)
                {
                    Bil_Sys_Associate_Diagnosis diagnosis2 = (Bil_Sys_Associate_Diagnosis)obj3;
                    this._bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                    if (this._bill_Sys_ProcedureCode_BO.UpdateReportProcedureCodeListWithRefDoctor(Convert.ToInt32(diagnosis2.EventProcID.ToString()), this.strLinkPath, Convert.ToString(this.extddlReadingDoctor.Text), num) == 1)
                    {
                        flag = true;
                        Bill_Sys_ReferalEvent event2 = new Bill_Sys_ReferalEvent();
                        ArrayList list3 = new ArrayList();
                        list3.Add(diagnosis2.PatientId.ToString());
                        list3.Add(diagnosis2.DoctorID.ToString());
                        list3.Add(diagnosis2.DateOfService.ToString());
                        list3.Add(diagnosis2.ProcedureCode.ToString());
                        list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list3.Add("");
                        list3.Add(diagnosis2.EventProcID.ToString());
                        event2.AddPatientProc(list3);
                        this._bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                        this._bill_Sys_ProcedureCode_BO.UpdateStatusProcedureCodeList(Convert.ToInt32(diagnosis2.EventProcID.ToString()));
                    }
                    else
                    {
                        flag2 = true;
                    }
                }
                if (flag && !flag2)
                {
                    this.Page.RegisterClientScriptBlock("ss", "<script language='javascript'>alert('Upload succesfully');</script>");
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
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet set = new DataSet();
            set = this._associateDiagnosisCodeBO.GetDiagnosisCode(caseID, companyId, doctorId, flag);
            this.grdNormalDgCode.DataSource = set.Tables[0];
            this.grdNormalDgCode.DataBind();
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
                foreach (DataGridItem item in this.grdNormalDgCode.Items)
                {
                    if ((item.ItemIndex == i) && ((bool)set.Tables[0].Rows[i]["CHECKED"]))
                    {
                        DataRow row = table.NewRow();
                        row["sz_diagnosis_code_id"] = set.Tables[0].Rows[i]["sz_diagnosis_code_id"].ToString();
                        row["sz_diagnosis_code"] = set.Tables[0].Rows[i]["sz_diagnosis_code"].ToString();
                        row["sz_description"] = set.Tables[0].Rows[i]["sz_description"].ToString();
                        row["sz_company_id"] = set.Tables[0].Rows[i]["sz_company_id"].ToString();
                        row["sz_procedure_group"] = set.Tables[0].Rows[i]["speciality"].ToString();
                        row["sz_procedure_group_id"] = set.Tables[0].Rows[i]["sz_procedure_group_id"].ToString();
                        table.Rows.Add(row);
                    }
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

    private void GetDiagnosisCode(string caseID, string companyId, string doctorId, string flag)
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
            set = this._associateDiagnosisCodeBO.GetDiagnosisCode(caseID, companyId, doctorId, flag);
            this.grdNormalDgCode.DataSource = set.Tables[0];
            this.grdNormalDgCode.DataBind();
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
                foreach (DataGridItem item in this.grdNormalDgCode.Items)
                {
                    if ((item.ItemIndex == i) && ((bool)set.Tables[0].Rows[i]["CHECKED"]))
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
            }
            this.grdSelectedDiagnosisCode.DataSource = table;
            this.grdSelectedDiagnosisCode.DataBind();
            this.lblDiagnosisCount.Text = table.Rows.Count.ToString();
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
            this.grdAssignedDiagnosisCode.CurrentPageIndex = e.NewPageIndex;
            this.GetAssignedDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            this.tabcontainerDiagnosisCode.ActiveTabIndex = 1;
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
            this.grdNormalDgCode.CurrentPageIndex = e.NewPageIndex;
            this.GetDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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

    protected void grdNormalDgCode_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.txtDiagonosisCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerDiagnosisCode_tabpnlAssociate_btnSeacrh').click(); return false;}} else {alert('Other');return true;}; ");

        strContextKey = (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) + "," + rbl_SZ_TYPE_ID.SelectedValue;
        this.ajDignosisCode.ContextKey = strContextKey;

        if (!base.IsPostBack)
        {
            this.extddlDiagnosisType.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlReadingDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (base.Request.QueryString["SetId"] != null)
            {
                this.txtDiagnosisSetID.Text = base.Request.QueryString["SetId"].ToString();
                this._ds = this._associateDiagnosisCodeBO.GetCaseAssociateDiagnosisDetails(this.txtDiagnosisSetID.Text);
            }
            else
            {
                this.txtDiagnosisSetID.Text = this._associateDiagnosisCodeBO.GetDiagnosisSetID();
            }
            if ((this.Session["DIAGNOS_ASSOCIATION_PAID"] != null) && (((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"]).Count > 0))
            {
                this.txtCaseID.Text = ((Bil_Sys_Associate_Diagnosis)((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"])[0]).CaseID;
            }
            this.GetAssignedDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            //this.GetDiagnosisCode(this.txtCaseID.Text, this.txtCompanyID.Text, "", "GET_DIAGNOSIS_CODE");
            this.grdNormalDgCode.Visible = false;
            this.tabcontainerDiagnosisCode.ActiveTabIndex = 1;
            if (this.Session["PROCEDURE_CODE"] != null)
            {
                DataSet set = new DataSet();
                set = (DataSet)this.Session["PROCEDURE_CODE"];
                this.grdProCode.DataSource = set;
                this.grdProCode.DataBind();
                this.grdProCode.Visible = true;
                this.btnAdd.Visible = true;
                this.proc.Visible = true;
            }
            else
            {
                this.grdProCode.Visible = false;
                this.btnAdd.Visible = false;
                this.proc.Visible = false;
            }
        }
    }

    private void SaveDiagnosisCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        new DataSet();
        try
        {
            ArrayList list;
            this._associateDiagnosisCodeBO.DeleteCaseAssociateDignosisCodeWithProcCode(this.txtCaseID.Text, this.txtCompanyID.Text, "");

            //string Selected = ((hdnDiagnosis.Value).Replace("--,", "*")).Replace("--", "");

            string[] ArrCodes = (hdnDiagnosis.Value).Split(',');
            
            foreach (DataGridItem item in this.grdSelectedDiagnosisCode.Items)
            {
                list = new ArrayList();
                list.Add(this.txtDiagnosisSetID.Text);
                list.Add(this.txtCaseID.Text);
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    list.Add("");
                }
                else
                {
                    list.Add("");
                }
                list.Add(item.Cells[0].Text.ToString());
                list.Add(this.txtCompanyID.Text);
                list.Add(item.Cells[5].Text.ToString());
                this._associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(list);
            }
            for (int i = 0; i < ArrCodes.Length; i++)
            {
                    list = new ArrayList();
                    list.Add(this.txtDiagnosisSetID.Text);
                    list.Add(this.txtCaseID.Text);
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    {
                        list.Add("");
                    }
                    else
                    {
                        list.Add("");
                    }
                    list.Add(ArrCodes[i]);
                    list.Add(this.txtCompanyID.Text);
                    list.Add(((Bil_Sys_Associate_Diagnosis)((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"])[0]).ProceuderGroupId);
                    this._associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(list);
            }
            this.GetAssignedDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            //this.GetDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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
            this.BindDiagnosisGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text, rbl_SZ_TYPE_ID.SelectedValue);
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

    protected void rbl_SZ_TYPE_ID_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        tabcontainerDiagnosisCode.ActiveTabIndex = 0;
        strContextKey = (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) + "," + rbl_SZ_TYPE_ID.SelectedValue;
        this.ajDignosisCode.ContextKey = strContextKey;
    }
}