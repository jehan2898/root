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
using System.Text;

public partial class AJAX_Pages_Bill_Sys_Associate_Documents : PageBase
{
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private Bill_Sys_Case _bill_Sys_Case;
    private Bill_Sys_ReportBO _reportBO;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private Bill_Sys_SystemObject objSessionSystem;
    private Bill_Sys_BillingCompanyObject objSessionBillingCompany;
    private Bill_Sys_BillingCompanyObject objSessionCompanyAppStatus;
    private Bill_Sys_UserObject objSessionUser;

    const int COLIDX_GRIDPAIDBILLS_CHECKBOX = 0;
    const int COLIDX_GRIDPAIDBILLS_TESTDATA = 22;
    const int COLIDX_GRIDPAIDBILLS_CHART_NO = 1;
    const int COLIDX_GRIDBILLS_CHART_NO = 4;
    const int COLIDX_GRIDPAIDBILLS_LHRCODE = 21;
    const int COLIDX_GRIDPAIDBILLS_VISIT_STATUS = 11;
    const int COLIDX_GRIDPAIDBILLS_DAYS = 23;
    const int COLIDX_GRIDPAIDBILLS_CASE_TYPE = 24;
    StringBuilder szExportoExcelColumname = new StringBuilder();
    StringBuilder szExportoExcelField = new StringBuilder();


    protected void Page_Load(object sender, EventArgs e)
    {

        objSessionSystem = (Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"];
        objSessionBillingCompany = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
        objSessionCompanyAppStatus = (Bill_Sys_BillingCompanyObject)Session["APPSTATUS"];
        objSessionUser = (Bill_Sys_UserObject)Session["USER_OBJECT"];
        txtireortreceive.Text = "false";
        //txtVisit.Text = drpdown_Documents.SelectedValue.ToString();
        txtCompanyid.Text = objSessionBillingCompany.SZ_COMPANY_ID;
        extddlCaseType.Flag_ID = txtCompanyid.Text;
        this.con1.SourceGrid = grdpaidbills;
        this.txtSearchBox1.SourceGrid = grdpaidbills;
        this.grdpaidbills.Page = this.Page;
        this.grdpaidbills.PageNumberList = this.con1;
        ddlDateValues.Attributes.Add("onChange", "javascript:SetVisitDate();");
        btnAddReason.Attributes.Add("onclick", "return CheckSelect();");
        btnChangeCaseType.Attributes.Add("onclick", "return CheckSelect();");

        if (!IsPostBack)
        {
            grdpaidbills.XGridBindSearch();
        }


    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Bill_Sys_ProcedureCode_BO objGetImageID = new Bill_Sys_ProcedureCode_BO();
        DataSet ds = objGetImageID.GetImgIdUsingEvenetProcId(txtCompanyid.Text);

        if (grdpaidbills.Visible == true)
        {
            for (int j = 0; j < grdpaidbills.Rows.Count; j++)
            {


                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i][0].ToString().Trim() == grdpaidbills.DataKeys[j]["I_EVENT_PROC_ID"].ToString().Trim())
                        {

                            grdpaidbills.Rows[j].BackColor = System.Drawing.Color.Yellow;

                        }
                    }
                }



            }
        }
    }


    protected void txtUpdate_Click(object sender, EventArgs e)
    {
        int Page_index = grdpaidbills.PageNumberList.SelectedIndex;
        grdpaidbills.PageIndex = Page_index;
        grdpaidbills.XGridBind();
        con1.SelectedIndex = Page_index;
        img2.Visible = false;
    }
    //protected void drpdown_Documents_SelectionChanged(object sender, EventArgs e)
    //{

    //    grdpaidbills.XGridBindSearch();

    //}


    //private void BindGridQueryString(string szFlag)
    //{


    //    txtVisit.Text = drpdown_Documents.SelectedValue.ToString();
    //    ReceivedReport.Visible = true;
    //    txtireortreceive.Text = szFlag;

    //    if (Session["CON_VAL"] != null)
    //    {
    //        string szIndex = Session["CON_VAL"].ToString();
    //        int iIndex = Convert.ToInt32(szIndex) - 1;
    //        if (iIndex == 0)
    //        {

    //            grdpaidbills.XGridBindSearch();
    //            grdpaidbills.Visible = true;
    //            //btnnext.Visible = true;
    //            img2.Visible = false;
    //        }
    //        else
    //        {


    //            //grdpaidbills.XGridBindSearch();


    //            grdpaidbills.Visible = true;
    //           // btnnext.Visible = true;

    //            int Page_index = iIndex;
    //            grdpaidbills.Start_Index = grdpaidbills.PageRowCount * Page_index + 1;
    //            grdpaidbills.PageIndex = Page_index;
    //            grdpaidbills.XGridBind();
    //            // grdpaidbills.XGridBindSearch();
    //            // int Page_index = iIndex;
    //            // grdpaidbills.PageIndex = Page_index;
    //            // grdpaidbills.PageNumberList.SelectedIndex = Page_index + 1;
    //            // con1.SelectedIndex = Page_index + 1;
    //            // grdpaidbills.XGridBindSearch();
    //            // grdpaidbills.PageIndex = Page_index+1;
    //            // grdpaidbills.PageNumberList.SelectedIndex = Page_index + 1;
    //            // con1.SelectedIndex = Page_index+1;
    //            // grdpaidbills.XGridBindSearch();
    //            ////
    //            Session["CON_VAL"] = null;
    //            img2.Visible = false;


    //        }

    //    }
    //    else
    //    {
    //        grdpaidbills.XGridBindSearch();
    //        grdpaidbills.Visible = true;
    //        //btnnext.Visible = true;
    //    }
    //    _reportBO = new Bill_Sys_ReportBO();
    //    try
    //    {
    //        //grdpaidbills.DataSource = _reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", txtCompanyid.Text, szFlag);
    //        //grdpaidbills.DataBind();
    //        Bill_Sys_ProcedureCode_BO obj = new Bill_Sys_ProcedureCode_BO();

    //        string sIsShowProcedureCode = objSessionSystem.SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION;
    //        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
    //        {
    //            szExportoExcelColumname.Append("Chart No,");
    //            szExportoExcelField.Append("SZ_CHART_NO,");
    //        }
    //        if (sIsShowProcedureCode != "1")
    //        {
    //            //Edit Procedure Code
    //            // grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_EDIT_PROCEDURE_CODE].Visible = false;
    //            //View Document
    //            //btnnext.Visible = true;
    //            grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_CHECKBOX].Visible = true;
    //            grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_TESTDATA].Visible = false;
    //            grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_DAYS].Visible = false;
    //            grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_CASE_TYPE].Visible = false;
    //            //COLIDX_GRIDPAIDBILLS_CASE_TYPE
    //            grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_LHRCODE].Visible = false;
    //            grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_VISIT_STATUS].Visible = true;
    //            drpdown_Documents.Visible = false;
    //            chkAOb.Visible = false;
    //            chkReferral.Visible = false;
    //            chkReport.Visible = false;
    //            szExportoExcelColumname.Append("Status,");
    //            szExportoExcelField.Append("SZ_STATUS,");
    //            grdpaidbills.ExportToExcelColumnNames = szExportoExcelColumname.ToString();
    //            //grdPatientList.Con = xcon;
    //            grdpaidbills.ExportToExcelFields = szExportoExcelField.ToString();
    //        }
    //        else
    //        {    //Edit Procedure Code
    //            //grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_EDIT_PROCEDURE_CODE].Visible = true;
    //            //View Document
    //           // btnnext.Visible = false;
    //            grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_CHECKBOX].Visible = false;
    //            grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_TESTDATA].Visible = true;
    //            grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_DAYS].Visible = true;
    //            grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_LHRCODE].Visible = true;
    //            grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_CASE_TYPE].Visible = true;
    //            grdpaidbills.Columns[COLIDX_GRIDPAIDBILLS_VISIT_STATUS].Visible = false;
    //            drpdown_Documents.Visible = true;
    //            chkAOb.Visible = true;
    //            chkReferral.Visible = true;
    //            chkReport.Visible = true;
    //            szExportoExcelColumname.Append("LHR Code,");
    //            szExportoExcelField.Append("SZ_LHR_CODE,");
    //            szExportoExcelColumname.Append("No. of Days,");
    //            szExportoExcelField.Append("I_NO_OF_DAYS,");
    //            szExportoExcelColumname.Append("Case Type,");
    //            szExportoExcelField.Append("SZ_CASE_TYPE_NAME,");
    //            szExportoExcelColumname.Append("Patient Id,");
    //            szExportoExcelField.Append("SZ_PATIENT_ID,");
    //            grdpaidbills.ExportToExcelColumnNames = szExportoExcelColumname.ToString();
    //            grdpaidbills.ExportToExcelFields = szExportoExcelField.ToString();


    //        }
    //        if (Session["SORT_DS"] != null)
    //        {
    //            Session["SORT_DS"] = null;
    //            Session["SORT_DS"] = (DataSet)_reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", txtCompanyid.Text, szFlag);
    //        }
    //        else
    //        {
    //            Session["SORT_DS"] = (DataSet)_reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", txtCompanyid.Text, szFlag);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        usrMessage.PutMessage(ex.ToString());
    //        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
    //        usrMessage.Show();
    //    }
    //}

    //protected void Page_PreRender(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        con1.SelectedIndex = grdpaidbills.PageIndex;

    //    }
    //    catch (Exception ex)
    //    {


    //    }

    //    if (grdBills.Visible == true)
    //    {
    //        try
    //        {
    //            BillsSum.DataSource = ((DataSet)(grdBills.DataSource)).Tables[2];
    //            BillsSum.DataBind();
    //        }
    //        catch (Exception ex1)
    //        {

    //        }


    //    }
    //    Bill_Sys_ProcedureCode_BO objGetImageID = new Bill_Sys_ProcedureCode_BO();
    //    DataSet ds = objGetImageID.GetImgIdUsingEvenetProcId(txtCompanyid.Text);

    //    if (grdpaidbills.Visible == true)
    //    {
    //        for (int j = 0; j < grdpaidbills.Rows.Count; j++)
    //        {


    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                {
    //                    if (ds.Tables[0].Rows[i][0].ToString().Trim() == grdpaidbills.DataKeys[j]["I_EVENT_PROC_ID"].ToString().Trim())
    //                    {



    //                        grdpaidbills.Rows[j].BackColor = System.Drawing.Color.Yellow;

    //                    }
    //                }
    //            }

    //            if (grdpaidbills.Rows[j].Cells[10].Text.ToLower().Contains("unknown"))
    //            {

    //                grdpaidbills.Rows[j].Cells[10].Text = "";
    //                grdpaidbills.Rows[j].Cells[9].Text = "";
    //            }

    //        }
    //    }
    //}

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    // BindReffGrid();
    //    grdpaidbills.XGridBindSearch();
    //}

    protected void btnSearchvisit_Click(object sender, EventArgs e)
    {
        // this.BindGridQueryString("false");
        grdpaidbills.XGridBindSearch();

    }


    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdpaidbills.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    protected void grdpaidbills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();

        if (e.CommandName.ToString() == "Edit")
        {


            int i = Convert.ToInt32(e.CommandArgument.ToString());
            DataSet dsRoom = new DataSet();
            //string szCaseType = grdpaidbills.Rows[i].Cells[24].Text;

            Bill_Sys_BillTransaction_BO objTransaction = new Bill_Sys_BillTransaction_BO();
            if ((grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString().ToUpper() != "OT" && grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString().ToUpper() != ""))
            {
                dsRoom = objTransaction.GetRoomId(grdpaidbills.DataKeys[i]["SZ_PROCEDURE_GROUP_ID"].ToString(), txtCompanyid.Text);
                string szRoomId = dsRoom.Tables[0].Rows[0][0].ToString();
                string ProcId1 = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
                Session["GETROOMID"] = szRoomId;
                Session["EVENTPROCID"] = ProcId1;
            }
            else
            {
                string ProcId1 = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
                Session["GETROOMID"] = "All";
                Session["EVENTPROCID"] = ProcId1;
            }


            string ProcId = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
            string szCaseID = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString();
            //string szEventProcID = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
            string procGId = grdpaidbills.DataKeys[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
            string Patientid = grdpaidbills.DataKeys[i]["SZ_PATIENT_ID"].ToString();
            string EventID = grdpaidbills.DataKeys[i]["I_EVENT_ID"].ToString();
            string szSpeciality = grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString();
            string patientname = grdpaidbills.DataKeys[i]["PATIENT_NAME"].ToString();
            //string dateofservice = grdpaidbills.Rows[i].Cells[8].Text;
            string dateofservice = grdpaidbills.DataKeys[i]["DT_DATE_OF_SERVICE"].ToString();
            string lhrcode = grdpaidbills.Rows[i].Cells[21].Text;
            //string caseno = grdpaidbills.Rows[i].Cells[2].Text;
            string caseno = grdpaidbills.DataKeys[i]["CASE_NO"].ToString();
            string szCaseType = grdpaidbills.Rows[i].Cells[24].Text;


            bool _ischeck = false;
            string _caseID = "";
            int _isSameCaseID = 0;
            string ProcGroupId = "";
            string PatientID = "";
            int _isSameProcGroupID = 0;
            ArrayList objArrOneD = new ArrayList();
            ArrayList arrEventID = new ArrayList();
            arrEventID.Add(grdpaidbills.DataKeys[i]["I_EVENT_ID"].ToString());


            Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();

            _dianosis_Association.EventProcID = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
            _dianosis_Association.DoctorID = grdpaidbills.DataKeys[i]["SZ_DOCTOR_ID"].ToString();
            _dianosis_Association.CaseID = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString();
            _dianosis_Association.ProceuderGroupId = grdpaidbills.DataKeys[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
            _dianosis_Association.ProceuderGroupName = grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString();
            _dianosis_Association.PatientId = grdpaidbills.DataKeys[i]["SZ_PATIENT_ID"].ToString();
            _dianosis_Association.DateOfService = grdpaidbills.DataKeys[i]["DT_DATE_OF_SERVICE"].ToString();
            _dianosis_Association.ProcedureCode = grdpaidbills.DataKeys[i]["SZ_PROC_CODE"].ToString();
            _dianosis_Association.CompanyId = txtCompanyid.Text;
            objArrOneD.Add(_dianosis_Association);

            Session["DIAGNOS_ASSOCIATION_PAID"] = objArrOneD;

            DataSet dscode = new DataSet();
            dscode = objTransaction.GetRoomId(procGId, txtCompanyid.Text);
            string sz_proc_code = grdpaidbills.Rows[i].Cells[9].Text;
            string sz_proc_desc = grdpaidbills.Rows[i].Cells[10].Text;
            Bill_Sys_ProcedureCode_BO obj = new Bill_Sys_ProcedureCode_BO();
            DataSet dsSys = obj.Get_Sys_Key("SS00014", txtCompanyid.Text);
            if (dsSys.Tables[0].Rows[0][0].ToString() == "1")
            {
                Session["EVENT_ID"] = arrEventID;
            }
            ArrayList arrPgeValue = new ArrayList();
            arrPgeValue.Add(txtVisitDate.Text);
            arrPgeValue.Add(txtToVisitDate.Text);
            arrPgeValue.Add(extddlCaseType.Text);
            arrPgeValue.Add(txtNumberOfDays.Text);
            //arrPgeValue.Add(drpdown_Documents.SelectedValue);
            //if (chkAOb.Checked)
            //{
            //    arrPgeValue.Add("1");
            //}
            //else
            //{
            //    arrPgeValue.Add("0");
            //}

            //if (chkReport.Checked)
            //{
            //    arrPgeValue.Add("1");
            //}
            //else
            //{
            //    arrPgeValue.Add("0");
            //}

            //if (chkReferral.Checked)
            //{
            //    arrPgeValue.Add("1");
            //}
            //else
            //{
            //    arrPgeValue.Add("0");
            //}
            arrPgeValue.Add(con1.SelectedValue);
            Session["PAGE_VALUES"] = arrPgeValue;
            img2.Visible = true;
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mmupdateproc", "showEditPopup('" + szCaseID + "','" + ProcId + "','" + procGId + "','" + Patientid + "','" + EventID + "','" + szSpeciality + "','" + sz_proc_desc + "','" + sz_proc_code + "','" + szCaseType + "','" + patientname + "','" + dateofservice + "','" + lhrcode + "','" + caseno + "');", true);
        }
    }

    protected void grdpaidbills_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void btnAddReason_Click(object sender, EventArgs e)
    {
        ArrayList arrAddReason = new ArrayList();
        for (int i = 0; i < grdpaidbills.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)grdpaidbills.Rows[i].FindControl("ChkReason");
            if (chk.Checked)
            {
                string Sz_EventProcID = grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
                arrAddReason.Add(Sz_EventProcID);

            }

        }
        Session["AddUnbilledReason"] = arrAddReason;
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mmPopup", "showAddReasonPopup();", true);
    }

    protected void btnChangeCaseType_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        ArrayList arrcheck = new ArrayList();
        _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
        try
        {
            if (extddlCaseType.Text == "NA")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Please Select CaseType');", true);
                return;
            }
            for (int i = 0; i < grdpaidbills.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdpaidbills.Rows[i].FindControl("ChkReason");
                if (chk.Checked)
                {
                    string SzEventID = grdpaidbills.DataKeys[i]["I_EVENT_ID"].ToString();
                    string SzBillStatus = grdpaidbills.DataKeys[i]["BT_BILL_STATUS"].ToString();
                    string SZ_CASE_ID = grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString();
                    if (SzBillStatus.ToLower() == "false" && SzEventID != "" && SZ_CASE_ID != "")
                    {
                        _bill_Sys_ProcedureCode_BO.UpdateCaseTypeOfAssociateDocument(SzEventID, SZ_CASE_ID, extddlCaseType.Text, txtCompanyid.Text);
                    }
                    else
                    {
                        arrcheck.Add(SzEventID);
                    }
                }
            }
            if (arrcheck.Count > 0)
            {
                usrMessage.PutMessage("You Can not change Case Type for billed Visit..");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
                usrMessage.Focus();
            }
            else
            {
                grdpaidbills.XGridBind();
                usrMessage.PutMessage("Case Type Changed successfully...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
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

    protected void txtAddReason_Click(object sender, EventArgs e)
    {
        int Page_index = grdpaidbills.PageNumberList.SelectedIndex;
        grdpaidbills.PageIndex = Page_index;
        grdpaidbills.XGridBind();
        con1.SelectedIndex = Page_index;
        img2.Visible = false;
    }




}
