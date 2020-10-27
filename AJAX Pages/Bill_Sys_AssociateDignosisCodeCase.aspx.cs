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

public partial class Bill_Sys_AssociateDignosisCodeCase : PageBase
{
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    private ArrayList _arrayList;
    private DataSet _ds;
    private string strCaseType = "";
    string strContextKey;
    private Bill_Sys_UserObject oC_UserObject = null;
    private Bill_Sys_BillingCompanyObject oC_Account = null;
    private Bill_Sys_CaseObject oC_CaseObject = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        oC_UserObject = (Bill_Sys_UserObject)(Session["USER_OBJECT"]);
        oC_Account = (Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"]);
        oC_CaseObject = (Bill_Sys_CaseObject)Session["CASE_OBJECT"];

        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            strContextKey = oC_Account.SZ_COMPANY_ID+ "," + rbl_SZ_TYPE_ID.SelectedValue;
            this.ajDignosisCode.ContextKey = strContextKey;
            //this.btnAssign.Attributes.Add("onclick", "return validate();");
            this.txtCompanyID.Text = oC_Account.SZ_COMPANY_ID;
            this.txtDiagonosisCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerDiagnosisCode_tabpnlAssociate_btnSeacrh').click(); return false;}} else {alert('Other');return true;}; ");
            if (!base.IsPostBack)
            {
                this.extddlDiagnosisType.Flag_ID = oC_Account.SZ_COMPANY_ID;
                this.extddlSpeciality.Flag_ID = oC_Account.SZ_COMPANY_ID;
                if (base.Request.QueryString["CaseId"] != null)
                {
                    this.Session["CASE_OBJECT"] = null;
                    this.txtCaseID.Text = base.Request.QueryString["CaseId"].ToString();
                    this.GetPatientDeskList();
                }
                else if (this.Session["CASE_OBJECT"] != null)
                {
                    this.txtCaseID.Text = oC_CaseObject.SZ_CASE_ID;
                    Bill_Sys_Case @case = new Bill_Sys_Case();
                    @case.SZ_CASE_ID = this.txtCaseID.Text;
                    this.Session["CASEINFO"] = @case;
                    this.Session["PassedCaseID"] = this.txtCaseID.Text;
                    string str = this.Session["PassedCaseID"].ToString();
                    this.Session["QStrCaseID"] = str;
                    this.Session["Case_ID"] = str;
                    this.Session["Archived"] = "0";
                    this.Session["QStrCID"] = str;
                    this.Session["SelectedID"] = str;
                    this.Session["DM_User_Name"] = oC_UserObject.SZ_USER_NAME;
                    this.Session["User_Name"] = oC_UserObject.SZ_USER_NAME;
                    this.Session["SN"] = "0";
                    this.Session["LastAction"] = "vb_CaseInformation.aspx";
                    this.Session["SZ_CASE_ID_NOTES"] = this.txtCaseID.Text;
                    this.Session["TM_SZ_CASE_ID"] = this.txtCaseID.Text;
                    this.GetPatientDeskList();
                }
                else
                {
                    base.Response.Redirect("Bill_Sys_SearchCase.aspx", false);
                }
                this.strCaseType = this._associateDiagnosisCodeBO.GetCaseType(this.txtCaseID.Text);
                string text1 = base.Request.QueryString["DoctorID"];
                if (base.Request.QueryString["SetId"] != null)
                {
                    this.txtDiagnosisSetID.Text = base.Request.QueryString["SetId"].ToString();
                    this._ds = this._associateDiagnosisCodeBO.GetCaseAssociateDiagnosisDetails(this.txtDiagnosisSetID.Text);
                    this.GetDiagnosisCode(this.txtCaseID.Text, this.txtCompanyID.Text, "", "GET_DIAGNOSIS_CODE");
                    this.Visiblecontrol();
                }
                else
                {
                    this.txtDiagnosisSetID.Text = this._associateDiagnosisCodeBO.GetDiagnosisSetID();
                }
                this.GetDiagnosisCode(this.txtCaseID.Text, oC_Account.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
                this.GetAssignedDiagnosisCode(this.txtCaseID.Text, oC_Account.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
                this.grdNormalDgCode.DataSource = null;
                this.grdNormalDgCode.DataBind();
                this.extddlSpeciality.Visible = false;
                this.lblSpeciality.Visible = false;
                if (oC_Account.BT_REFERRING_FACILITY)
                {
                    this.tabcontainerDiagnosisCode.ActiveTabIndex = 1;
                }
                else
                {
                    this.tabcontainerDiagnosisCode.ActiveTabIndex = 0;
                }

                BindGridSpecialty();
            }
            this.lblMsg.Text = "";
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
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            new Bill_Sys_ChangeVersion(this.Page).MakeReadOnlyPage("Bill_Sys_AssociateDignosisCodeCase.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindGridSpecialty()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdSpeciality.DataSource = this._digosisCodeBO.GetSpecialtyGrid(this.txtCompanyID.Text);
            this.grdSpeciality.DataBind();
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

    //Change added on 28/09/2015

    protected void btnSearch_Click(object sender, EventArgs e)
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

    protected void btnSubmit_OnClick(object sender, EventArgs e)
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
            //this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text, rbl_SZ_TYPE_ID.SelectedValue);
            BindGridSpecialty();
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
            this._associateDiagnosisCodeBO.DeleteCaseAssociateDignosisCodeWithProcCode(this.txtCaseID.Text, this.txtCompanyID.Text, "");
            DataSet set = new DataSet();

            string Selected = ((hdnDiagnosis.Value).Replace("--,", "*")).Replace("--","");

            string[] ArrCodes = Selected.Split('*');

            string[] ArrSpeciality = (hdnSpeciality.Value).Split(',');


            string str = this.extddlSpeciality.Text;
            ArrayList checkSpecialty = new ArrayList();

            set = this._associateDiagnosisCodeBO.Get_Associate_Speciality(str, this.txtCompanyID.Text);
            foreach (DataGridItem item in this.grdSelectedDiagnosisCode.Items)
            {
                //string[] arrInsert = ArrCodes[i].Split('~');

                this._arrayList = new ArrayList();
                this._arrayList.Add(this.txtDiagnosisSetID.Text);
                this._arrayList.Add(this.txtCaseID.Text);
                if (oC_Account.BT_REFERRING_FACILITY)
                {
                    this._arrayList.Add("");
                }
                else
                {
                    this._arrayList.Add("");
                }
                this._arrayList.Add(item.Cells[0].Text.ToString());
                this._arrayList.Add(this.txtCompanyID.Text);
                //this._arrayList.Add(checkSpecialty);
                this._arrayList.Add(item.Cells[5].Text.ToString());
                this._associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(this._arrayList);
            }

            for (int i = 0; i < ArrCodes.Length; i++)
            {
                string[] arrInsert = ArrCodes[i].Split('~');
                //for (int j = 0; j < ArrSpeciality.Length; j++)
                //{
                //    string[] arrInsertS = ArrSpeciality[j].Split('~');

                    this._arrayList = new ArrayList();
                    this._arrayList.Add(this.txtDiagnosisSetID.Text);
                    this._arrayList.Add(this.txtCaseID.Text);
                    if (oC_Account.BT_REFERRING_FACILITY)
                    {
                        this._arrayList.Add("");
                    }
                    else
                    {
                        this._arrayList.Add("");
                    }

                    this._arrayList.Add(arrInsert[1]);
                    this._arrayList.Add(this.txtCompanyID.Text);
                    //checkSpecialty.Add(item4.Cells[2].Text.ToString());
                    this._arrayList.Add(arrInsert[0]);
                    //this._arrayList.Add(this.extddlSpeciality.Text);
                    this._associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(this._arrayList);
                //}

                if (set != null)
                {
                    for (int k = 0; k < set.Tables[0].Rows.Count; k++)
                    {
                        if (set.Tables[0].Rows[i]["SZ_ASSOCIATE_SPECIALITY"].ToString() != "")
                        {
                            this._arrayList = new ArrayList();
                            this._arrayList.Add(this.txtDiagnosisSetID.Text);
                            this._arrayList.Add(this.txtCaseID.Text);
                            if (oC_Account.BT_REFERRING_FACILITY)
                            {
                                this._arrayList.Add("");
                            }
                            else
                            {
                                this._arrayList.Add("");
                            }
                            this._arrayList.Add(arrInsert[1]);
                            this._arrayList.Add(this.txtCompanyID.Text);
                            this._arrayList.Add(set.Tables[0].Rows[i]["SZ_ASSOCIATE_SPECIALITY"].ToString());
                            this._associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(this._arrayList);
                        }
                    }
                }
            }

            this.GetAssignedDiagnosisCode(this.txtCaseID.Text, oC_Account.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            this.GetDiagnosisCode(this.txtCaseID.Text, oC_Account.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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
            this.GetDiagnosisCode(this.txtCaseID.Text, oC_Account.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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
    }

    private void GetPatientDeskList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO tbo = new Bill_Sys_PatientBO();
        try
        {
            this.grdPatientDeskList.DataSource = tbo.GetPatienDeskList("GETPATIENTLIST", this.txtCaseID.Text);
            this.grdPatientDeskList.DataBind();
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
                    if (oC_Account.BT_REFERRING_FACILITY)
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
            this.GetDiagnosisCode(this.txtCaseID.Text, oC_Account.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
			grdAssignedDiagnosisCode.CurrentPageIndex = 0;
            this.GetAssignedDiagnosisCode(this.txtCaseID.Text, oC_Account.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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
        strContextKey = oC_Account.SZ_COMPANY_ID + "," + rbl_SZ_TYPE_ID.SelectedValue;
        this.ajDignosisCode.ContextKey = strContextKey;
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
            this.GetDiagnosisCode(this.txtCaseID.Text, oC_Account.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            grdAssignedDiagnosisCode.CurrentPageIndex = e.NewPageIndex;
            this.GetAssignedDiagnosisCode(this.txtCaseID.Text, oC_Account.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            tabcontainerDiagnosisCode.ActiveTabIndex= 1;
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

}
