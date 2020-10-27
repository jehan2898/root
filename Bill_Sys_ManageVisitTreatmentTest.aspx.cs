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

public partial class Bill_Sys_ManageVisitTreatmentTest : PageBase
{
    private Bill_Sys_VisitTreatmentBO _visitTreatmentBO;
    private ArrayList _arrayList;
    protected string[] szTabCaptions = { "Visits in your account", "Visit prices", "Visits of Doctor -" };
    private int iProcedureCodeCount = 0;
    private DataTable objTable;
    private string _szDoctorName = "";
    private Bill_Sys_TreatmentBO _bill_Sys_TreatmentBO;

    protected int getProcedureCount()
    {
        return iProcedureCodeCount;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        txtDoctorID.Text = Request.QueryString["DoctorID"].ToString();
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        _visitTreatmentBO = new Bill_Sys_VisitTreatmentBO();
        btnClose.Attributes.Add("onclick", "return CloseWindow();");
        btnCloseFromTPrices.Attributes.Add("onclick", "return CloseWindow();");
        
        if (Request.QueryString["Flag"] != null)
        {
            _szDoctorName = _visitTreatmentBO.GetDoctorName(txtDoctorID.Text);
            if (Request.QueryString["Flag"] == "Visit")
            {
                szTabCaptions[0] = "Vists in your account";
                szTabCaptions[1] = "Visit prices";
                szTabCaptions[2] = "Visits of Doctor";
                lblHeaderText.Text = "Visit";
                lblDoctorNameViewTreatment.Text = "Manage visits  for Doctor - Mr. " + _szDoctorName + " - Select the visits which";
                lblDoctorHeaderPrice.Text = "Assign visits Price for Doctor - Mr. " + _szDoctorName;
                lblAssociateDoctorHeader.Text = "Manage visits  for Doctor - Mr. " + _szDoctorName + " - Select the visits which";
                if(!Page.IsPostBack)
                {
                    LoadTypes("visits");
                }
            }
            else if (Request.QueryString["Flag"] == "Treatment")
            {
                szTabCaptions[0] = "Treatments in your account";
                szTabCaptions[1] = "Treatment prices";
                szTabCaptions[2] = "Treatments of Doctor";
                lblHeaderText.Text ="Treatment";
                lblDoctorNameViewTreatment.Text = "Manage treatments  for Doctor - Mr. " + _szDoctorName + " - Select the treatments which";
                lblDoctorHeaderPrice.Text = "Assign treatment Price for Doctor - Mr. " + _szDoctorName;
                lblAssociateDoctorHeader.Text = "Manage treatments  for Doctor - Mr. " + _szDoctorName + " - Select the treatments which";
                //Manage visits  for Doctor - Mr. Cohen - Select the visits which 
                if (!Page.IsPostBack)
                {
                    LoadTypes("treatments");
                }
            }
            else if (Request.QueryString["Flag"] == "Test")
            {
                szTabCaptions[0] = "Tests in your account";
                szTabCaptions[1] = "Test prices";
                szTabCaptions[2] = "Tests of Doctor";
                lblHeaderText.Text = "Test";
                lblDoctorNameViewTreatment.Text = "Manage tests  for Doctor - Mr. " + _szDoctorName + " - Select the tests which";
                lblDoctorHeaderPrice.Text = "Assign tests Price for Doctor - Mr. " + _szDoctorName;
                lblAssociateDoctorHeader.Text = "Manage tests  for Doctor - Mr. " + _szDoctorName + " - Select the tests which";
                if (!Page.IsPostBack)
                {
                    LoadTypes("tests");
                }
            }
        }

        try
        {
            txtDoctorID.Text = Request.QueryString["DoctorID"].ToString();
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!IsPostBack)
            {
                BindGrid();
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
            cv.MakeReadOnlyPage("Bill_Sys_ManageVisitTreatmentTest.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void LoadTypes(string p_szCodeType)
    {
        ArrayList objArr = new ArrayList();
        objArr.Add(txtDoctorID.Text);
        objArr.Add(txtCompanyID.Text);
        string szKeyCode = "";

        btnAssign.Attributes.Add("onclick", "javascript:shout();");
        if (p_szCodeType.CompareTo("visits") == 0)
        {
            szKeyCode = "DOCTORS_VISITS";
        }
        else if (p_szCodeType.CompareTo("treatments") == 0)
        {
            szKeyCode = "DOCTORS_TREATMENTS";
        }
        else if (p_szCodeType.CompareTo("tests") == 0)
        {
            szKeyCode = "DOCTORS_TESTS";
        }
        objArr.Add(szKeyCode);
        Bill_Sys_ManageVisitsTreatmentsTests_BO objBO = new Bill_Sys_ManageVisitsTreatmentsTests_BO();
        ddlTestNames.DataSource = objBO.GetDoctorSpecificTypeList(objArr);
        ddlTestNames.DataTextField = "description";
        ddlTestNames.DataValueField = "code";
        ddlTestNames.DataBind();
        ddlTestNames.Items.Insert(0, "--- Select ---");

        _visitTreatmentBO = new Bill_Sys_VisitTreatmentBO();
        ddlTestList.DataSource = _visitTreatmentBO.GetDoctorSpecificTypeList(objArr);
        ddlTestList.DataTextField = "description";
        ddlTestList.DataValueField = "code";
        ddlTestList.DataBind();
        ddlTestList.Items.Insert(0, "--- Select ---");
    }

    protected DataTable getProcedureTable()
    {
        return this.objTable;
    }
    
    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _visitTreatmentBO = new Bill_Sys_VisitTreatmentBO();
        try
        {
            if (Request.QueryString["Flag"] != null)
            {
                if (Request.QueryString["Flag"] == "Visit")
                {
                    grdTreatmentTestVisit.DataSource = _visitTreatmentBO.GetTotalList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "GETDOCTORVISITLIST");
                    grdTreatmentTestVisit.DataBind();                  

                }
                else if (Request.QueryString["Flag"] == "Treatment")
                {
                    grdTreatmentTestVisit.DataSource = _visitTreatmentBO.GetTotalList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID,"GETDOCTORTREATMENTLIST");
                    grdTreatmentTestVisit.DataBind();
                }
                else if (Request.QueryString["Flag"] == "Test")
                {
                    grdTreatmentTestVisit.DataSource = _visitTreatmentBO.GetTotalList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID,"GETDOCTORTESTLIST");
                    grdTreatmentTestVisit.DataBind();
                }
                foreach (DataGridItem grdRow in grdTreatmentTestVisit.Items)
                {
                    if (grdRow.Cells[7].Text.ToString() == "True")
                    {
                        grdRow.Cells[6].FindControl("lnlMakeBillable").Visible = false;
                        grdRow.Cells[6].FindControl("lnlMakeNonBillable").Visible = true;
                    }
                    else if (grdRow.Cells[7].Text.ToString() == "False")
                    {
                        grdRow.Cells[6].FindControl("lnlMakeBillable").Visible = true;
                        grdRow.Cells[6].FindControl("lnlMakeNonBillable").Visible = false;
                    }
                }
            }
            //Response.Write("cont " + objTable.Rows.Count);
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

    private void SaveList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _visitTreatmentBO = new Bill_Sys_VisitTreatmentBO();
        _arrayList = new ArrayList();
        try
        {
            for (int i = 0; i < grdTreatmentTestVisit.Items.Count; i++)
            {
                _arrayList.Clear();
                CheckBox chkList = (CheckBox)grdTreatmentTestVisit.Items[i].Cells[0].FindControl("chkAssign");
                if (chkList.Checked)
                {
                    _arrayList.Add(txtDoctorID.Text);
                    _arrayList.Add(grdTreatmentTestVisit.Items[i].Cells[1].Text);
                    _arrayList.Add(txtCompanyID.Text);
                    _visitTreatmentBO.SaveList(_arrayList);
                }

            }
            lblMsg.Visible = true;
            lblMsg.Text = "Assigned Successfully....";

            if (ddlTestNames.SelectedValue != "0")
            {
                GetProcedureCodeList();
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


    protected void ddlTestNames_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            tabcontainerPatientTreatment.ActiveTabIndex = 1;
            if (ddlTestNames.SelectedValue != "NA")
            {
                GetProcedureCodeList();
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

    protected void btnSaveTreatmentPrices_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        /*
         * 
         *  
         *  sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", _arrayList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", _arrayList[1].ToString());
            sqlCmd.Parameters.AddWithValue("@FLT_AMOUNT", _arrayList[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[3].ToString());

            sqlCmd.Parameters.AddWithValue("@FLAG", "ADDUPDATE");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[3].ToString());

         * 
        */

        try
        {
            string[] doctorAmount = Request["txtDoctorAmount"].ToString().Split(new char[] { ',' });
            int statId = 0;
            foreach (ListItem lstItem in lstProcedures.Items)
            {
                if (lstItem.Selected == true)
                {
                    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                    ArrayList _arrayList = new ArrayList();

                    _arrayList.Add(Request.QueryString["DoctorID"].ToString());
                    _arrayList.Add(lstItem.Value);
                    _arrayList.Add(doctorAmount[statId].ToString());
                    _arrayList.Add(txtCompanyID.Text);
                    _arrayList.Add(ddlTestNames.SelectedValue);
                    _bill_Sys_ProcedureCode_BO.SaveDoctorProcedureCodeAmountDetails(_arrayList);
                    statId = statId + 1;
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
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SaveList();
            if (Request.QueryString["Flag"] != null)
            {
                if (Request.QueryString["Flag"] == "Visit")
                {
                    LoadTypes("visits");
                }
                if (Request.QueryString["Flag"] == "Treatment")
                {
                    LoadTypes("treatments");
                }
                if (Request.QueryString["Flag"] == "Test")
                {
                    LoadTypes("tests");
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
    protected void ddlTestList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _visitTreatmentBO = new Bill_Sys_VisitTreatmentBO();
        try
        {
            tabcontainerPatientTreatment.ActiveTabIndex = 2;
            if (ddlTestList.SelectedValue != "0")
            {
                grdDoctorAssociateProcedureCode.DataSource = _visitTreatmentBO.GetDoctorProcAmountList("GET_PROC_AMT_LIST", ddlTestList.SelectedValue, txtDoctorID.Text, txtCompanyID.Text);
                grdDoctorAssociateProcedureCode.DataBind();
            }
            else
            {
                grdDoctorAssociateProcedureCode.DataSource = "";
            }
            if (ddlTestNames.SelectedValue != "--- Select ---")
            {
                GetProcedureCodeList();
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


    private void GetProcedureCodeList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList objList = new ArrayList();
            objList.Add(txtCompanyID.Text);
            objList.Add(ddlTestNames.SelectedValue);
            objList.Add(Request.QueryString["DoctorId"]);
            objList.Add("name"); // sort order

            Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCodes = new Bill_Sys_ProcedureCode_BO();
            DataSet objData = _bill_Sys_ProcedureCodes.GetAllProcedureCodeList(objList);
            DataTable objTable = objData.Tables[0];
            this.objTable = objTable;

            // Set KOEL amount. Any row will have the same amount. Iterate the loop just 1's
            if (objTable != null)
            {
                foreach (DataRow row in objTable.Rows)
                {
                    txtProviderKOEL.Text = "" + row["KOEL"];
                    break;
                }
            }

            iProcedureCodeCount = objTable.Rows.Count;
            lstProcedures.DataSource = objData;
            lstProcedures.DataTextField = "DESCRIPTION";
            lstProcedures.DataValueField = "CODE";
            lstProcedures.DataBind();
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

    protected void grdTreatments_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "MakeBillable")
            {
                _bill_Sys_TreatmentBO = new Bill_Sys_TreatmentBO();
                _bill_Sys_TreatmentBO.UpdateBillableNonBillable(true,e.CommandArgument.ToString(),((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                BindGrid();
            }
            else if (e.CommandName == "MakeNonBillable")
            {
                _bill_Sys_TreatmentBO = new Bill_Sys_TreatmentBO();
                _bill_Sys_TreatmentBO.UpdateBillableNonBillable(false, e.CommandArgument.ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                BindGrid();
            }
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
   
}