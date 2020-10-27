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
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using DevExpress.Web;



public partial class AJAX_Pages_Bill_Sys_Received_Report_PopupPage : PageBase
{
    Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
    Bill_Sys_DigosisCodeBO _digosisCodeBO;
    Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
    Bill_Sys_NF3_Template objNF3Template;
    string strLinkPath = null;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private DataSet _ds;
    private ArrayList _arrayList;
    private Bill_Sys_SystemObject objSystemObject;
    protected void Page_Load(object sender, EventArgs e)
    {

        bool _ischeck = false;
        string _caseID = "";
        int _isSameCaseID = 0;
       
        _dianosis_Association.EventProcID = Request.QueryString["szpatienttreatmentid"].ToString();
        _dianosis_Association.DoctorID = Request.QueryString["szdoctorid"].ToString();
        _dianosis_Association.CaseID = Request.QueryString["szcaseid"].ToString();
        _dianosis_Association.ProceuderGroupId = Request.QueryString["szprocgroupid"].ToString();
        Session["I_Event_ID"] = Request.QueryString["szeventid"].ToString();
        Session["DIAGNOS_ASSOCIATION"] = _dianosis_Association;
        txtEventProcID.Text = Request.QueryString["szpatienttreatmentid"].ToString();
        txtEventID.Text = Request.QueryString["szeventid"].ToString();
        btnAssign.Attributes.Add("OnClick", "callforSearch();");
        btnDeAssociate.Attributes.Add("OnClick", "callforSearch();");
        objSystemObject = (Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"];
      
        if (!(Request.QueryString["AcBilling"] == null))
        {
            redingdoctd1.InnerHtml = "&nbsp;";
            redingdoctd2.InnerHtml = "&nbsp;";
        }
      
        //tabcontainerDiagnosisCode.ActiveTabIndex = 1;
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            extddlSpecialityDia.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlDiagnosisType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlReadingDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
            _dianosis_Association = (Bil_Sys_Associate_Diagnosis)Session["DIAGNOS_ASSOCIATION"];
            extddlSpecialityDia.Text = _dianosis_Association.ProceuderGroupId;
            //_dianosis_Association.EventProcID
            txtCaseID.Text = _dianosis_Association.CaseID;
            // To Show Rading Doctor At Page Load:- Tushar
            extddlReadingDoctor.Text = _dianosis_Association.DoctorID;
            //End Of Code
            //Put Condition here
            //------Commented by Kunal--------------------------------------
            if (objSystemObject.SZ_ASSIGN_DIAGNOSIS_CODE_TO_VISIT == "1")
            {
                GetAssignedProcedureDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, _dianosis_Association.EventProcID, "GET_DIAGNOSIS_CODE");
                GetProcedureDiagnosisCode(txtCaseID.Text, txtCompanyID.Text, _dianosis_Association.EventProcID, "GET_DIAGNOSIS_CODE");
            }
            else
            {
                GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
                GetDiagnosisCode(txtCaseID.Text, txtCompanyID.Text, "", "GET_DIAGNOSIS_CODE");
            }
            //-----------End------------------------------------------------

            grdDiagnosisCode.Visible = false;
            
        }


        if (hdnSearch.Value != "true")
        {
            GetAssignedProcedureDiagnosisCode(txtCaseID.Text, txtCompanyID.Text, txtEventProcID.Text, "GET_PROCEDURE_ASSOCIATED_DIAGNOSIS_CODE");
            BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
        }
        else
        {
            hdnSearch.Value = "";
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
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
            //Put Condition here
            //-----Kunal--------------------------------------------------
            if (objSystemObject.SZ_ASSIGN_DIAGNOSIS_CODE_TO_VISIT == "1")
            {
                SaveProcedureDiagnosisCode();
            }
            else
                SaveDiagnosisCode();
            //------End-----------------------------------------------------
            lblMsg.Visible = true;
            BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
            lblMsg.Text = "Case Associate Diagnosis  Saved Successfully ...!";
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
                if (checkBox != null)
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

    private void GetProcedureDiagnosisCode(string caseID, string companyId, string szEventID, string flag)
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
            ds = _associateDiagnosisCodeBO.GetProcedureDiagnosisCode(caseID, companyId, szEventID, flag);
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
        ArrayList _arrayList;
        try
        {
            string szDiagnosisCode = "";
            _associateDiagnosisCodeBO.DeleteCaseAssociateDignosisCodeWithProcCode(txtCaseID.Text, txtCompanyID.Text, "");//, lstPTDProcCode.Items[j].Value.Substring(0, lstPTDProcCode.Items[j].Value.IndexOf("|")), lstPTDProcCode.Items[j].Value.Substring((lstPTDProcCode.Items[j].Value.IndexOf("|") + 1), ((lstPTDProcCode.Items[j].Value.Length - lstPTDProcCode.Items[j].Value.IndexOf("|")) - 1)));
            txtDiagnosisSetID.Text = _associateDiagnosisCodeBO.GetDiagnosisSetID();
            Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
            _dianosis_Association = (Bil_Sys_Associate_Diagnosis)Session["DIAGNOS_ASSOCIATION"];

            for (int i = 0; i < grdDiagnosisCode.VisibleRowCount; i++)
            {
                ASPxGridView _ASPxGridView = (ASPxGridView)grdid.FindControl("grdDiagnosisCode");
                GridViewDataColumn c = (GridViewDataColumn)grdDiagnosisCode.Columns[0];
                CheckBox checkBox = (CheckBox)grdDiagnosisCode.FindRowCellTemplateControl(i, c, "chkall");
                if (checkBox != null)
                {
                    if (checkBox.Checked)
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
                        szDiagnosisCode = Convert.ToString(grdDiagnosisCode.GetRowValues(i, "SZ_DIAGNOSIS_CODE_ID"));
                        _arrayList.Add(szDiagnosisCode);
                        _arrayList.Add(txtCompanyID.Text);
                        _arrayList.Add(_dianosis_Association.ProceuderGroupId);
                        _associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(_arrayList);
                    }
                }
            }
            

            //Put Condition here
            //---------Commented by Kunal---------------------------------------------------------
            if (objSystemObject.SZ_ASSIGN_DIAGNOSIS_CODE_TO_VISIT == "1")
            {
                GetAssignedProcedureDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, _dianosis_Association.EventProcID, "GET_DIAGNOSIS_CODE");
                GetProcedureDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, _dianosis_Association.EventProcID, "GET_DIAGNOSIS_CODE");
            }
            else
            {
                GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
                GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            }
            //--------------End---------------------------------------------------------------------
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
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_ReffPaidBills.aspx';</script>");
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
             grdDiagnosisCode.DataSource = ds.Tables[0]; //_associateDiagnosisCodeBO.GetDoctorDiagnosisCode(id, flag);
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

}
