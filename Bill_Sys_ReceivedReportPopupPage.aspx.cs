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
using DevExpress.Web;

public partial class Bill_Sys_ReceivedReportPopupPage : PageBase
{
    Bill_Sys_DigosisCodeBO _digosisCodeBO;
    Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
    Bill_Sys_NF3_Template objNF3Template;
    string strLinkPath = null;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private DataSet _ds;
    private ArrayList _arrayList;
    private Bill_Sys_SystemObject objSystemObject;
    Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();

    private void BindGrid(string typeid, string code, string description)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdNormalDgCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid, code, description);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            UserControl_Doctor objUsrDoc = new UserControl_Doctor();
            objUsrDoc = (UserControl_Doctor)FindControl("docname");
            ASPxComboBox cmbReading = (ASPxComboBox)objUsrDoc.FindControl("cmbActiveDoctor");
            
            objUsrDoc = (UserControl_Doctor)FindControl("docname");
            ASPxComboBox cmbReferring= (ASPxComboBox)objUsrDoc.FindControl("cmbReferrringDoctor");

            
            
            //if (this.extddlReadingDoctor.Text != "---Select---")
            //{
            //    Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
            //    diagnosis = (Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"];
            //    new Bill_Sys_ProcedureCode_BO().UpdateReadingDoctor(Convert.ToInt32(diagnosis.EventProcID), this.extddlReadingDoctor.Text);
                
            //    this.lblMsg.Text = "Doctor Updated Sucessfully";
            //    this.lblMsg.Visible = true;

            //    if (txtSource.Text.ToString().ToLower() == "rr")
            //    {
            //        //window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='/application/AJAX Pages/Bill_Sys_ReffPaidBills.aspx';
            //        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "parent.document.getElementById('ctl00_ContentPlaceHolder1_hfupadate').value = 'true';", true);
            //    }

                
            //}
            //else 
            //UserControl_Doctor objUsrDoc = new UserControl_Doctor();
            //objUsrDoc = (UserControl_Doctor)FindControl("docname");
            //ASPxComboBox cmbReferring = (ASPxComboBox)objUsrDoc.FindControl("cmbReferrringDoctor");
            if (TxtReferDoc.Text.Trim().ToString()!="")
            {
                if (CmbSpeciality.Value == "" || CmbSpeciality.Value == null)
                {
                    this.lblMsg.Text = "To add new reffering doctor speciality is mandatory.";
                    this.lblMsg.Visible = true;
                    return;
                }
                if (CmbOffice.Value == "" || CmbOffice.Value == null)
                {
                    this.lblMsg.Text = "To add new reffering doctor office is mandatory.";
                    this.lblMsg.Visible = true;
                    return;
                }
                cmbReferring.Value = AddReferringDoctor();
            }

            if ((cmbReading != null && cmbReferring != null) && (cmbReading.Text != "" && cmbReferring.Text != ""))
            {
                Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
                diagnosis = (Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"];
                string readingDoctorName = cmbReading.Text;
                string readingDoctorID = cmbReading.Value.ToString();

                string referrringDoctorName = cmbReferring.Text;
                string referrringDoctorID = cmbReferring.Value.ToString();

                new Bill_Sys_ProcedureCode_BO().UpdateReadingDoctor(Convert.ToInt32(diagnosis.EventProcID), readingDoctorID);
                UpdateReferringDoctor(Convert.ToInt32(diagnosis.EventProcID), referrringDoctorID);
                this.lblMsg.Text = "Doctor Updated Sucessfully";
                this.lblMsg.Visible = true;

                if (txtSource.Text.ToString().ToLower() == "rr")
                {
                    //window.parent.document.location.href=window.parent.document.location.href;window.self.close();window.top.parent.location='/application/AJAX Pages/Bill_Sys_ReffPaidBills.aspx';
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "Msg", "parent.document.getElementById('ctl00_ContentPlaceHolder1_hfupadate').value = 'true';", true);
                }
            }
            else
            {
                this.lblMsg.Text = "Please Select Doctor To Update";
                this.lblMsg.Visible = true;
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
            this.SaveProcedureDiagnosisCode();
            this.lblMsg.Visible = true;
            this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
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
                    if (!this._associateDiagnosisCodeBO.DeleteAssociateProcedureDiagonisCode(this._arrayList))
                    {
                        str = str + "  " + item.Cells[2].Text.ToString() + ",";
                    }
                }
            }
            if (str == "")
            {
                this.lblMsg.Visible = true;
                this.lblMsg.Text = " Diagnosis code deassociated successfully ...!";
            }
            else
            {
                this.lblMsg.Visible = true;
                this.lblMsg.Text = str + " diagnosis code used in bills. You can not de-associate it.";
            }
            Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
            diagnosis = (Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"];
            this.GetProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
            this.grdAssignedDiagnosisCode.CurrentPageIndex = 0;
            this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
            this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
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

    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdNormalDgCode.Visible = true;
            this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet set = new DataSet();
            set = this._associateDiagnosisCodeBO.GetProcedureDiagnosisCode(caseID, companyId, szEventProcID, flag);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
            this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"]).EventProcID, "GET_DIAGNOSIS_CODE");
            this.tabcontainerDiagnosisCode.ActiveTabIndex=1;
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
            this.grdNormalDgCode.CurrentPageIndex = e.NewPageIndex;
            Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
            this.GetProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"]).EventProcID, "GET_DIAGNOSIS_CODE");
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

    protected void grdNormalDgCode_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (base.Request.QueryString["AcBilling"] != null)
        {
            //this.redingdoctd1.InnerHtml = "&nbsp;";
            //this.redingdoctd2.InnerHtml = "&nbsp;";
        }
        this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.txtDiagonosisCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerDiagnosisCode_tabpnlAssociate_btnSeacrh').click(); return false;}} else {alert('Other');return true;}; ");

        docname.LoadDoctor(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        docname.RefferDoctor(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        UserControl_Doctor objUsrDoc = new UserControl_Doctor();
        objUsrDoc = (UserControl_Doctor)FindControl("docname");
        ASPxComboBox cmbReading = (ASPxComboBox)objUsrDoc.FindControl("cmbActiveDoctor");
        ASPxComboBox cmbReferring = (ASPxComboBox)objUsrDoc.FindControl("cmbReferrringDoctor");
        if (!base.IsPostBack)
        {
            this.extddlSpecialityDia.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlDiagnosisType.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //this.extddlReadingDoctor.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
            if (base.Request.QueryString["PatienetTreatmentID"] != null)
            {
                diagnosis.EventProcID = Request.QueryString["PatienetTreatmentID"].ToString();
            }
            if (base.Request.QueryString["doctorid"] != null)
            {
                diagnosis.DoctorID = Request.QueryString["doctorid"].ToString();
            }
            if (base.Request.QueryString["caseid"] != null)
            {
                diagnosis.CaseID = Request.QueryString["caseid"].ToString();
            }
            if (base.Request.QueryString["proceduregroupid"] != null)
            {
                diagnosis.ProceuderGroupId = Request.QueryString["proceduregroupid"].ToString();
            }
            if (base.Request.QueryString["eventid"] != null)
            {
                Session["I_Event_ID"] = Request.QueryString["eventid"].ToString();
            }
            if (base.Request.QueryString["companyid"] != null)
            {
                diagnosis.CompanyId = Request.QueryString["companyid"].ToString();
            }
            if (base.Request.QueryString["source"] != null)
            {
                if (Request.QueryString["source"] == "true")
                {
                    txtSource.Text = "RR";
                }
            }
            if (base.Request.QueryString["DocID"] != null)
            {
                diagnosis.RefferingDoctor = Request.QueryString["DocID"].ToString();
            }
            this.Session["DIAGNOS_ASSOCIATION"] = diagnosis;
            //diagnosis = (Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"];
            this.extddlSpecialityDia.Text=diagnosis.ProceuderGroupId;
            this.txtCaseID.Text = diagnosis.CaseID;
            
            //this.extddlReadingDoctor.Text=diagnosis.DoctorID;
            cmbReading.Value = diagnosis.DoctorID;
            cmbReferring.Value = diagnosis.RefferingDoctor;
            this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
            this.GetProcedureDiagnosisCode(this.txtCaseID.Text, this.txtCompanyID.Text, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
            this.grdNormalDgCode.Visible = false;
            this.tabcontainerDiagnosisCode.ActiveTabIndex=1;
            LoadSpeciality();
            LoadOffice();
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
            this.txtDiagnosisSetID.Text = this._associateDiagnosisCodeBO.GetDiagnosisSetID();
            Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
            diagnosis = (Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"];
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
            foreach (DataGridItem item2 in this.grdNormalDgCode.Items)
            {
                CheckBox box = (CheckBox)item2.Cells[1].FindControl("chkSelect");
                if (box.Checked)
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
                    list.Add(item2.Cells[1].Text.ToString());
                    list.Add(this.txtCompanyID.Text);
                    list.Add(diagnosis.ProceuderGroupId);
                    this._associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(list);
                }
            }
            this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
            this.GetProcedureDiagnosisCode(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
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

    public void SaveProcedureDiagnosisCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
        diagnosis = (Bil_Sys_Associate_Diagnosis)this.Session["DIAGNOS_ASSOCIATION"];
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        new DataSet();
        try
        {
            ArrayList list;
            this._associateDiagnosisCodeBO.DeleteProcedureAssociateDignosisCode(this.txtCaseID.Text, this.txtCompanyID.Text, diagnosis.EventProcID);
            foreach (DataGridItem item in this.grdSelectedDiagnosisCode.Items)
            {
                list = new ArrayList();
                list.Add(diagnosis.EventProcID);
                list.Add(this.txtCaseID.Text);
                list.Add(this.Session["I_Event_ID"].ToString());
                list.Add(item.Cells[0].Text.ToString());
                list.Add(item.Cells[5].Text.ToString());
                list.Add(this.txtCompanyID.Text);
                list.Add(this.txtDiagnosisSetID.Text);
                this._associateDiagnosisCodeBO.SaveProcedureAssociateDignosisCode(list);
            }
            foreach (DataGridItem item2 in this.grdNormalDgCode.Items)
            {
                CheckBox box = (CheckBox)item2.Cells[1].FindControl("chkSelect");
                if (box.Checked)
                {
                    list = new ArrayList();
                    list.Add(diagnosis.EventProcID);
                    list.Add(this.txtCaseID.Text);
                    list.Add(this.Session["I_Event_ID"].ToString());
                    list.Add(item2.Cells[1].Text.ToString());
                    list.Add(this.extddlSpecialityDia.Text);
                    list.Add(this.txtCompanyID.Text);
                    list.Add(this.txtDiagnosisSetID.Text);
                    this._associateDiagnosisCodeBO.SaveProcedureAssociateDignosisCode(list);
                }
            }
            this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, this.txtCompanyID.Text, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
            this.GetProcedureDiagnosisCode(this.txtCaseID.Text, this.txtCompanyID.Text, diagnosis.EventProcID, "GET_DIAGNOSIS_CODE");
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
    
    public void UpdateReferringDoctor(int EveProcID, string refDocID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon;
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_update_referringDoctor", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@i_event_id", Session["I_Event_ID"].ToString());
            sqlCmd.Parameters.AddWithValue("@sz_referring_doctor_id", refDocID);
            sqlCmd.ExecuteNonQuery();
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
        
        finally { sqlCon.Close(); }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public string InsertDoctorMaster(string sz_Doctor_Name, string sz_Off_ID, string sz_Proc_id, string sz_Company_ID, string sz_Assign_no, string sz_Doc_Lic_No)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;
        string strConn = "";
        SqlDataReader dr;
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        int i = 0;
        string _value = "";
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_ADD_MST_REFFER_DOC", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_DOCTOR_NAME", sz_Doctor_Name);
            comm.Parameters.AddWithValue("@SZ_OFFICE_ID", sz_Off_ID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", sz_Proc_id);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            //comm.Parameters.AddWithValue("@SZ_ASSIGN_NUMBER", sz_Assign_no);
            //comm.Parameters.AddWithValue("@SZ_DOCTOR_LICENSE_NUMBER", sz_Doc_Lic_No);
            //i = comm.ExecuteNonQuery();

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value)
                {
                    _value = dr[0].ToString();
                }
            }

        }
       catch (Exception ex)
        {
            
            i = 0;
            _value = "";
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return _value;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        
        UserControl_Doctor objUsrDoc = new UserControl_Doctor();
        objUsrDoc = (UserControl_Doctor)FindControl("docname");
        ASPxComboBox cmbReferring = (ASPxComboBox)objUsrDoc.FindControl("cmbReferrringDoctor");
        cmbReferring.Value = AddReferringDoctor();
    }

    public DataSet getSpeciality(string companyID, string Flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_MST_PROCEDURE_GROUP", conn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@flag", Flag);
            comm.Parameters.AddWithValue("@ID", companyID);

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
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
       
        finally
        {
            conn.Close();
        }

        return ds;
         //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void LoadSpeciality()
    {
        CmbSpeciality.TextField = "DESCRIPTION";
        CmbSpeciality.ValueField = "CODE";
        string companyID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string Flag = "GET_PROCEDURE_GROUP_LIST";
        CmbSpeciality.DataSource = getSpeciality(companyID, Flag);
        CmbSpeciality.DataBind();
    }

    public DataSet getOffice(string companyID, string Flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds;
        SqlCommand comm;
        SqlDataAdapter da;

        string strConn = "";
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn;
        conn = new SqlConnection(strConn);
        conn.Open();
        ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_MST_OFFICE", conn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@flag", Flag);
            comm.Parameters.AddWithValue("@ID", companyID);

            da = new SqlDataAdapter(comm);
            da.Fill(ds);
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
        
        finally
        {
            conn.Close();
        }

        return ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void LoadOffice()
    {
        CmbOffice.TextField = "DESCRIPTION";
        CmbOffice.ValueField = "Code";
        string companyid = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string Flag = "OFFICE_LIST";
        CmbOffice.DataSource = getOffice(companyid, Flag);
        CmbOffice.DataBind();
    }

    private string AddReferringDoctor()
    {
        string specID = CmbSpeciality.SelectedItem.Value.ToString();
        string DocName = TxtReferDoc.Text;
        string Office_Id = CmbOffice.SelectedItem.Value.ToString();
        string companyID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string sz_Assign_no = "";
        string sz_Doc_Lic_No = "";
        //if (this.CmbSpeciality.Text != "" || CmbOffice.Text != "" || TxtReferDoc.Text != "")
        //{
        //    this.LblAddMsg.Text = "Added Sucessfully";
        //    this.LblAddMsg.Visible = true;
        //}

        string docID = InsertDoctorMaster(DocName, Office_Id, specID, companyID, sz_Assign_no, sz_Doc_Lic_No);
        docname.RefferDoctor(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        //if (docID!="")
        //{
        //    this.LblAddMsg.Text = "Doctor Added Sucessfully";
        //    this.LblAddMsg.Visible = true;
        //}

        //else
        //{
        //    this.LblAddMsg.Text = "Insertion Failed";
        //    this.LblAddMsg.Visible = true;
        //}
        return docID;
    }
}
