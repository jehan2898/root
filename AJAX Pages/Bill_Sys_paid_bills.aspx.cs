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
using System.IO;

public partial class AJAX_Pages_Bill_Sys_paid_bills : PageBase
{
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private Bill_Sys_Case _bill_Sys_Case;
    private Bill_Sys_ReportBO _reportBO;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    string strLinkPath = null;
    Bill_Sys_NF3_Template objNF3Template;

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
    //const int COLIDX_GRIDPAIDBILLS_EDIT_PROCEDURE_CODE = 21;
    //const int COLIDX_GRIDPAIDBILLS_VIEW_DOCUMENTS = 22;	
    string caseId = "";
    string companyID = "";
    string proc_id = "";

    private void BindBillsGrid(string szFlag)
    {
        this.PaidBills.Visible = true;
        this.txtireortreceive.Text = szFlag;
        if (this.objSessionBillingCompany.BT_REFERRING_FACILITY)
        {
            this.grdBills.Columns[4].SortExpression = "MST_PATIENT.I_RFO_CHART_NO";
        }
        else
        {
            this.grdBills.Columns[4].SortExpression = "MST_PATIENT.SZ_CHART_NO";
        }
        this.grdBills.XGridBindSearch();
        this.grdBills.Visible = true;
        this._reportBO = new Bill_Sys_ReportBO();
    }

    private void BindGridQueryString(string szFlag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.txtVisit.Text = this.drpdown_Documents.SelectedValue.ToString();
        this.ReceivedReport.Visible = true;
        this.txtireortreceive.Text = szFlag;
        this.grdpaidbills.XGridBindSearch();
        this.grdpaidbills.Visible = true;
        this.btnnext.Visible = true;
        this._reportBO = new Bill_Sys_ReportBO();
        try
        {
            new Bill_Sys_ProcedureCode_BO();
            if (this.objSessionSystem.SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION != "1")
            {
                this.btnnext.Visible = true;
                this.grdpaidbills.Columns[0].Visible = true;
                this.grdpaidbills.Columns[0x15].Visible = false;
                this.drpdown_Documents.Visible = false;
            }
            else
            {
                this.btnnext.Visible = false;
                this.grdpaidbills.Columns[0].Visible = false;
                this.grdpaidbills.Columns[0x15].Visible = true;
                this.drpdown_Documents.Visible = true;
            }
            if (this.Session["SORT_DS"] != null)
            {
                this.Session["SORT_DS"] = null;
                this.Session["SORT_DS"] = this._reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", this.txtCompanyid.Text, szFlag);
            }
            else
            {
                this.Session["SORT_DS"] = this._reportBO.GetProcedureReports("SP_DASH_BOARD_PROCEDURE_REPORT", "", "", "NA", "NA", this.txtCompanyid.Text, szFlag);
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

    protected void btnnext_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        bool flag = false;
        string str = "";
        int num = 0;
        string str2 = "";
        int num2 = 0;
        ArrayList list = new ArrayList();
        ArrayList list2 = new ArrayList();
        try
        {
            for (int i = 0; i < this.grdpaidbills.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdpaidbills.Rows[i].FindControl("chkSelect");
                if (box.Checked)
                {
                    if (num == 0)
                    {
                        str = this.grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString();
                        this.grdpaidbills.DataKeys[i]["SZ_PATIENT_ID"].ToString();
                        num = 1;
                    }
                    if (str != Convert.ToString(this.grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString()))
                    {
                        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mmer", "alert('please select same patient !');", true);
                        return;
                    }
                    if (num2 == 0)
                    {
                        str2 = this.grdpaidbills.DataKeys[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                        num2 = 1;
                    }
                    if (str2 != this.grdpaidbills.DataKeys[i]["SZ_PROCEDURE_GROUP_ID"].ToString())
                    {
                        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mmer1", "alert('please select same Speciality !');", true);
                        return;
                    }
                    list2.Add(this.grdpaidbills.DataKeys[i]["I_EVENT_ID"].ToString());
                    Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
                    diagnosis.EventProcID=this.grdpaidbills.DataKeys[i]["I_EVENT_PROC_ID"].ToString();
                    diagnosis.DoctorID=this.grdpaidbills.DataKeys[i]["SZ_DOCTOR_ID"].ToString();
                    diagnosis.CaseID=this.grdpaidbills.DataKeys[i]["SZ_CASE_ID"].ToString();
                    diagnosis.ProceuderGroupId=this.grdpaidbills.DataKeys[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                    diagnosis.ProceuderGroupName=this.grdpaidbills.DataKeys[i]["sz_procedure_group"].ToString();
                    diagnosis.PatientId=this.grdpaidbills.DataKeys[i]["SZ_PATIENT_ID"].ToString();
                    diagnosis.DateOfService=this.grdpaidbills.DataKeys[i]["DT_DATE_OF_SERVICE"].ToString();
                    diagnosis.ProcedureCode=this.grdpaidbills.DataKeys[i]["SZ_PROC_CODE"].ToString();
                    diagnosis.CompanyId=this.txtCompanyid.Text;
                    list.Add(diagnosis);
                    flag = true;
                }
            }
            if (!flag)
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mmSelect", "alert('please select record from grid !');", true);
                list.Clear();
                this.lblMsg.Text = "";
                this.lblMsg.Visible = false;
            }
            else
            {
                this.Session["DIAGNOS_ASSOCIATION_PAID"] = list;
                DataSet roomId = new DataSet();
                roomId = new Bill_Sys_BillTransaction_BO().GetRoomId(str2, this.txtCompanyid.Text);
                if (roomId.Tables[0].Rows.Count > 0)
                {
                    Bill_Sys_ProcedureCode_BO e_bo = new Bill_Sys_ProcedureCode_BO();
                    if (e_bo.Get_Sys_Key("SS00014", this.txtCompanyid.Text).Tables[0].Rows[0][0].ToString() == "1")
                    {
                        string str3 = roomId.Tables[0].Rows[0][0].ToString();
                        ArrayList list3 = new ArrayList();
                        list3.Add(this.txtCompanyid.Text);
                        list3.Add(str3);
                        DataSet referringProcCodeList = new Bill_Sys_ManageVisitsTreatmentsTests_BO().GetReferringProcCodeList(list3);
                        this.Session["PROCEDURE_CODE"] = referringProcCodeList;
                        this.Session["EVENT_ID"] = list2;
                    }
                    else
                    {
                        this.Session["PROCEDURE_CODE"] = null;
                    }
                }
                ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "mmPopup", "ShowDignosisPopup();", true);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string errorMessage = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + errorMessage);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.grdpaidbills.XGridBindSearch();
    }

    protected void btnSearchvisit_Click(object sender, EventArgs e)
    {
        this.BindGridQueryString("false");
    }

    private void ConfigDashBoard()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO dbo = new DashBoardBO();
        try
        {
            foreach (DataRow row in dbo.GetConfigDashBoard(this.objSessionUser.SZ_USER_ROLE).Rows)
            {
                switch (row[0].ToString())
                {
                    case "Daily Appointment":
                        {
                            this.tblDailyAppointment.Visible = true;
                            continue;
                        }
                    case "Weekly Appointment":
                        {
                            this.tblWeeklyAppointment.Visible = true;
                            continue;
                        }
                    case "Bill Status":
                        {
                            this.tblBillStatus.Visible = true;
                            continue;
                        }
                    case "Desk":
                        {
                            this.tblDesk.Visible = true;
                            continue;
                        }
                    case "Missing Information":
                        {
                            this.tblMissingInfo.Visible = true;
                            continue;
                        }
                    case "Report Section":
                        {
                            this.tblReportSection.Visible = true;
                            continue;
                        }
                    case "Procedure Status":
                        {
                            this.tblBilledUnbilledProcCode.Visible = true;
                            continue;
                        }
                    case "Visits":
                        {
                            this.tblVisits.Visible = true;
                            this.grdTotalVisit.DataSource = dbo.getVisitDetails(this.txtCompanyid.Text, "TOTALCOUNT");
                            this.grdTotalVisit.DataBind();
                            this.grdVisit.DataSource = dbo.getVisitDetails(this.txtCompanyid.Text, "BILLEDVISIT");
                            this.grdVisit.DataBind();
                            this.grdUnVisit.DataSource = dbo.getVisitDetails(this.txtCompanyid.Text, "UNBILLEDVISIT");
                            this.grdUnVisit.DataBind();
                            continue;
                        }
                    case "Missing Speciality":
                        {
                            this.tblMissingSpeciality.Visible = true;
                            continue;
                        }
                    case "Patient Visit Status":
                        {
                            this.tblPatientVisitStatus.Visible = true;
                            continue;
                        }
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

    protected void drpdown_Documents_SelectionChanged(object sender, EventArgs e)
    {
        this.grdpaidbills.XGridBindSearch();
    }

    protected void grdBills_RowBound(object sender, GridViewRowEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.objSessionSystem.SZ_NEW_BILL != "True")
            {
                LinkButton button = (LinkButton)e.Row.FindControl("lnkSelectBill");
                if (button != null)
                {
                    button.Enabled = false;
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

    protected void grdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            int num = Convert.ToInt32(e.CommandArgument.ToString());
            this._bill_Sys_Case = new Bill_Sys_Case();
            this._bill_Sys_Case.SZ_CASE_ID=this.grdBills.DataKeys[num]["sz_case_id"].ToString();
            CaseDetailsBO sbo = new CaseDetailsBO();
            Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
            obj2.SZ_PATIENT_ID=sbo.GetCasePatientID(this._bill_Sys_Case.SZ_CASE_ID, "");
            obj2.SZ_CASE_ID=this._bill_Sys_Case.SZ_CASE_ID;
            obj2.SZ_COMAPNY_ID=sbo.GetPatientCompanyID(obj2.SZ_PATIENT_ID);
            obj2.SZ_PATIENT_NAME=sbo.GetPatientName(obj2.SZ_PATIENT_ID);
            obj2.SZ_CASE_NO=this.grdBills.DataKeys[num]["sz_case_no"].ToString();
            this.Session["CASEINFO"] = this._bill_Sys_Case;
            this.Session["CASE_OBJECT"] = obj2;
            this.Session["PassedCaseID"] = this._bill_Sys_Case.SZ_CASE_ID;
            this.Session["SZ_BILL_NUMBER"] = this.grdBills.DataKeys[num]["sz_bill_number"].ToString();
            base.Response.Redirect("Bill_Sys_BillTransaction.aspx?Type=Search", false);
        }
    }

    protected void grdBills_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void grdpaidbills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        DataSet set = new DataSet();
        set = (DataSet)this.Session["SORT_DS"];
        DataView defaultView = set.Tables[0].DefaultView;
        if (e.CommandName.ToString() == "workarea")
        {
            int num = Convert.ToInt32(e.CommandArgument.ToString());
            this.Session["SZ_CASE_ID"] = this.grdpaidbills.DataKeys[num]["SZ_CASE_ID"].ToString();
            this.Session["PROVIDERNAME"] = this.grdpaidbills.DataKeys[num]["PATIENT_NAME"].ToString();
            new CaseDetailsBO();
            Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
            obj2.SZ_PATIENT_ID=this.grdpaidbills.DataKeys[num]["SZ_PATIENT_ID"].ToString();
            obj2.SZ_CASE_ID=this.grdpaidbills.DataKeys[num]["SZ_CASE_ID"].ToString();
            obj2.SZ_CASE_NO=this.grdpaidbills.DataKeys[num]["CASE_NO"].ToString();
            obj2.SZ_PATIENT_NAME=this.grdpaidbills.DataKeys[num]["PATIENT_NAME"].ToString();
            obj2.SZ_COMAPNY_ID=this._bill_Sys_BillingCompanyDetails_BO.getCompanyDetailsOfCase(this.grdpaidbills.DataKeys[num]["SZ_CASE_ID"].ToString()).SZ_COMPANY_ID;
            this.Session["CASE_OBJECT"] = obj2;
            this._bill_Sys_Case = new Bill_Sys_Case();
            this._bill_Sys_Case.SZ_CASE_ID=this.grdpaidbills.DataKeys[num]["SZ_CASE_ID"].ToString();
            this.Session["CASEINFO"] = this._bill_Sys_Case;
            base.Response.Redirect("../Bill_Sys_StatusProceudure.aspx", false);
        }
        if (e.CommandName.ToString() == "appointment")
        {
            int num2 = Convert.ToInt32(e.CommandArgument.ToString());
            if (this.objSessionBillingCompany.BT_REFERRING_FACILITY)
            {
                this.Session["SZ_CASE_ID"] = this.grdpaidbills.DataKeys[num2]["SZ_CASE_ID"].ToString();
                this.Session["PROVIDERNAME"] = this.grdpaidbills.DataKeys[num2]["PATIENT_NAME"].ToString();
                new CaseDetailsBO();
                Bill_Sys_CaseObject obj3 = new Bill_Sys_CaseObject();
                obj3.SZ_PATIENT_ID=this.grdpaidbills.DataKeys[num2]["SZ_PATIENT_ID"].ToString();
                obj3.SZ_CASE_ID=this.grdpaidbills.DataKeys[num2]["SZ_CASE_ID"].ToString();
                obj3.SZ_CASE_NO=this.grdpaidbills.DataKeys[num2]["CASE_NO"].ToString();
                obj3.SZ_PATIENT_NAME=this.grdpaidbills.DataKeys[num2]["PATIENT_NAME"].ToString();
                obj3.SZ_COMAPNY_ID=this._bill_Sys_BillingCompanyDetails_BO.getCompanyDetailsOfCase(this.grdpaidbills.DataKeys[num2]["SZ_CASE_ID"].ToString()).SZ_COMPANY_ID;
                this.Session["CASE_OBJECT"] = obj3;
                this._bill_Sys_Case = new Bill_Sys_Case();
                this._bill_Sys_Case.SZ_CASE_ID=this.grdpaidbills.DataKeys[num2]["SZ_CASE_ID"].ToString();
                this.Session["CASEINFO"] = this._bill_Sys_Case;
                DateTime time = new DateTime();
                time = Convert.ToDateTime(this.grdpaidbills.DataKeys[num2]["DT_DATE_OF_SERVICE"]);
                string str = "";
                str = "&idate=" + time.ToShortDateString();
                base.Response.Redirect("Bill_Sys_AppointPatientEntry.aspx?Flag=true" + str, false);
            }
            else
            {
                this.Session["SZ_CASE_ID"] = this.grdpaidbills.DataKeys[num2]["SZ_CASE_ID"].ToString();
                this.Session["PROVIDERNAME"] = this.grdpaidbills.DataKeys[num2]["PATIENT_NAME"].ToString();
                new CaseDetailsBO();
                Bill_Sys_CaseObject obj4 = new Bill_Sys_CaseObject();
                obj4.SZ_PATIENT_ID=this.grdpaidbills.DataKeys[num2]["SZ_PATIENT_ID"].ToString();
                obj4.SZ_CASE_ID=this.grdpaidbills.DataKeys[num2]["SZ_CASE_ID"].ToString();
                obj4.SZ_CASE_NO=this.grdpaidbills.DataKeys[num2]["CASE_NO"].ToString();
                obj4.SZ_PATIENT_NAME=this.grdpaidbills.DataKeys[num2]["PATIENT_NAME"].ToString();
                obj4.SZ_COMAPNY_ID=this._bill_Sys_BillingCompanyDetails_BO.getCompanyDetailsOfCase(this.grdpaidbills.DataKeys[num2]["SZ_CASE_ID"].ToString()).SZ_COMPANY_ID;
                this.Session["CASE_OBJECT"] = obj4;
                this.Session["CASE_OBJECT"] = obj4;
                this._bill_Sys_Case = new Bill_Sys_Case();
                this._bill_Sys_Case.SZ_CASE_ID=this.grdpaidbills.DataKeys[num2]["SZ_CASE_ID"].ToString();
                this.Session["CASEINFO"] = this._bill_Sys_Case;
                DateTime time2 = new DateTime();
                time2 = Convert.ToDateTime(this.grdpaidbills.DataKeys[num2]["DT_DATE_OF_SERVICE"]);
                string str2 = "?_day=" + time2.ToShortDateString() + "&idate=" + time2.ToShortDateString();
                base.Response.Redirect("Bill_Sys_ScheduleEvent.aspx" + str2, false);
            }
        }
        if (e.CommandName.ToString() == "edit")
        {
            int num3 = Convert.ToInt32(e.CommandArgument.ToString());
            new DataSet();
            Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
            if (this.grdpaidbills.DataKeys[num3]["SZ_PROCEDURE_GROUP_ID"].ToString() != "")
            {
                string str3 = n_bo.GetRoomId(this.grdpaidbills.DataKeys[num3]["SZ_PROCEDURE_GROUP_ID"].ToString(), this.txtCompanyid.Text).Tables[0].Rows[0][0].ToString();
                string str4 = this.grdpaidbills.DataKeys[num3]["I_EVENT_PROC_ID"].ToString();
                this.Session["GETROOMID"] = str3;
                this.Session["EVENTPROCID"] = str4;
            }
            else
            {
                string str5 = this.grdpaidbills.DataKeys[num3]["I_EVENT_PROC_ID"].ToString();
                this.Session["GETROOMID"] = "All";
                this.Session["EVENTPROCID"] = str5;
            }
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mmupdateproc", "showProcPopup();", true);
        }
        if (e.CommandName.ToString() == "view")
        {
            int num4 = Convert.ToInt32(e.CommandArgument.ToString());
            string str6 = this.grdpaidbills.DataKeys[num4]["SZ_CASE_ID"].ToString();
            string str7 = this.grdpaidbills.DataKeys[num4]["I_EVENT_PROC_ID"].ToString();
            string str8 = this.grdpaidbills.DataKeys[num4]["sz_procedure_group"].ToString();
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mmupdate", "ViewDocumentPopup('" + str6 + "','" + str7 + "','" + str8 + "');", true);
        }
        if (e.CommandName.ToString() == "Edit")
        {
            int num5 = Convert.ToInt32(e.CommandArgument.ToString());
            new DataSet();
            Bill_Sys_BillTransaction_BO n_bo2 = new Bill_Sys_BillTransaction_BO();
            if ((this.grdpaidbills.DataKeys[num5]["sz_procedure_group"].ToString().ToUpper() != "OT") && (this.grdpaidbills.DataKeys[num5]["sz_procedure_group"].ToString().ToUpper() != ""))
            {
                string str9 = n_bo2.GetRoomId(this.grdpaidbills.DataKeys[num5]["SZ_PROCEDURE_GROUP_ID"].ToString(), this.txtCompanyid.Text).Tables[0].Rows[0][0].ToString();
                string str10 = this.grdpaidbills.DataKeys[num5]["I_EVENT_PROC_ID"].ToString();
                this.Session["GETROOMID"] = str9;
                this.Session["EVENTPROCID"] = str10;
            }
            else
            {
                string str11 = this.grdpaidbills.DataKeys[num5]["I_EVENT_PROC_ID"].ToString();
                this.Session["GETROOMID"] = "All";
                this.Session["EVENTPROCID"] = str11;
            }
            string str12 = this.grdpaidbills.DataKeys[num5]["I_EVENT_PROC_ID"].ToString();
            string str13 = this.grdpaidbills.DataKeys[num5]["SZ_CASE_ID"].ToString();
            string str14 = this.grdpaidbills.DataKeys[num5]["SZ_PROCEDURE_GROUP_ID"].ToString();
            string str15 = this.grdpaidbills.DataKeys[num5]["SZ_PATIENT_ID"].ToString();
            string str16 = this.grdpaidbills.DataKeys[num5]["I_EVENT_ID"].ToString();
            string str17 = this.grdpaidbills.DataKeys[num5]["sz_procedure_group"].ToString();
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            list2.Add(this.grdpaidbills.DataKeys[num5]["I_EVENT_ID"].ToString());
            Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
            diagnosis.EventProcID=this.grdpaidbills.DataKeys[num5]["I_EVENT_PROC_ID"].ToString();
            diagnosis.DoctorID=this.grdpaidbills.DataKeys[num5]["SZ_DOCTOR_ID"].ToString();
            diagnosis.CaseID=this.grdpaidbills.DataKeys[num5]["SZ_CASE_ID"].ToString();
            diagnosis.ProceuderGroupId=this.grdpaidbills.DataKeys[num5]["SZ_PROCEDURE_GROUP_ID"].ToString();
            diagnosis.ProceuderGroupName=this.grdpaidbills.DataKeys[num5]["sz_procedure_group"].ToString();
            diagnosis.PatientId=this.grdpaidbills.DataKeys[num5]["SZ_PATIENT_ID"].ToString();
            diagnosis.DateOfService=this.grdpaidbills.DataKeys[num5]["DT_DATE_OF_SERVICE"].ToString();
            diagnosis.ProcedureCode=this.grdpaidbills.DataKeys[num5]["SZ_PROC_CODE"].ToString();
            diagnosis.CompanyId=this.txtCompanyid.Text;
            list.Add(diagnosis);
            this.Session["DIAGNOS_ASSOCIATION_PAID"] = list;
            new DataSet();
            n_bo2.GetRoomId(str14, this.txtCompanyid.Text);
            string text = this.grdpaidbills.Rows[num5].Cells[9].Text;
            string str19 = this.grdpaidbills.Rows[num5].Cells[10].Text;
            Bill_Sys_ProcedureCode_BO e_bo = new Bill_Sys_ProcedureCode_BO();
            if (e_bo.Get_Sys_Key("SS00014", this.txtCompanyid.Text).Tables[0].Rows[0][0].ToString() == "1")
            {
                this.Session["EVENT_ID"] = list2;
            }
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mmupdateproc", "showEditPopup('" + str13 + "','" + str12 + "','" + str14 + "','" + str15 + "','" + str16 + "','" + str17 + "','" + str19 + "','" + text + "');", true);
        }
    }

    protected void grdpaidbills_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void lnkExcelUnPaid_onclick(object sender, EventArgs e)
    {
        if (this.objSessionSystem.SZ_CHART_NO != "0")
        {
            this.grdBills.ExportToExcelFields=("sz_bill_number,sz_case_no,sz_chart_no,sz_patient_name,sz_insurance_name,dt_bill_date,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE");
            this.grdBills.ExportToExcelColumnNames=("Bill Number,Case #,Chart #,Patient name,Insurance Name,Bill Date,Bill Amount,Paid Amount,Balance");
        }
        else
        {
            this.grdBills.ExportToExcelFields=("sz_bill_number,sz_case_no,sz_patient_name,sz_insurance_name,dt_bill_date,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE");
            this.grdBills.ExportToExcelColumnNames=("Bill Number,Case #,Patient name,Insurance Name,Bill Date,Bill Amount,Paid Amount,Balance");
        }
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", "window.location.href ='" +  ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + this.grdBills.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")) + "';", true);
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + this.grdpaidbills.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")) + "';", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.objSessionSystem = (Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"];
        this.objSessionBillingCompany = (Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"];
        this.objSessionCompanyAppStatus = (Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"];
        this.objSessionUser = (Bill_Sys_UserObject)this.Session["USER_OBJECT"];
        this.con1.SourceGrid=this.grdpaidbills;
        this.txtSearchBox1.SourceGrid=this.grdpaidbills;
        this.grdpaidbills.Page=this.Page;
        this.grdpaidbills.PageNumberList=this.con1;
        this.XGridPaginationDropDownUnPaid.SourceGrid=this.grdBills;
        this.XGridSearchTextBoxUnPaid.SourceGrid=this.grdBills;
        this.grdBills.Page=this.Page;
        this.grdBills.PageNumberList=this.XGridPaginationDropDownUnPaid;
        this.txtUpdate1.Attributes.Add("onclick", "javascript:CloseEditProcPopup();");
        this.txtUpdate2.Attributes.Add("onclick", "javascript:CloseDocumentPopup();");
        this.txtUpdate3.Attributes.Add("onclick", "javascript:CloseDocPopup();");
        this.txtUpdate4.Attributes.Add("onclick", "javascript:CloseDocPopup();");
        this.ddlDateValues.Attributes.Add("onChange", "javascript:SetVisitDate();");
        try
        {
            if (base.Request.QueryString["popup"] != null)
            {
                if (base.Request.QueryString["popup"].ToString().Equals("done"))
                {
                    this.lblMsg.Text = "Report received successfully.";
                    this.lblMsg.Visible = true;
                }
                if (base.Request.QueryString["popup"].ToString().Equals("done1"))
                {
                    this.lblMsg.Visible = false;
                }
            }
            if (!base.IsPostBack)
            {
                this.txtCompanyid.Text = this.objSessionBillingCompany.SZ_COMPANY_ID;
                if (this.lblMsg.Text.Equals(""))
                {
                    this.lblMsg.Visible = false;
                }
                if (base.Request.QueryString["Flag"] != null)
                {
                    if (base.Request.QueryString["Flag"].ToString().ToLower() == "paid")
                    {
                        this.lblHeader.Text = "Paid Bills";
                        this.txtHeading.Text = "Paid Bills";
                        this.BindBillsGrid("paid");
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "unpaid")
                    {
                        this.lblHeader.Text = "Un-Paid Bills";
                        this.txtHeading.Text = "Un-Paid Bills";
                        this.BindBillsGrid("unpaid");
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "missingattorney")
                    {
                        this.lblHeader.Text = "Missing Attorney";
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "missinginsurancecompany")
                    {
                        this.lblHeader.Text = "Missing Insurance Company";
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "missingprovider")
                    {
                        this.lblHeader.Text = "Missing Provider";
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "missingclaimnumber")
                    {
                        this.lblHeader.Text = "Missing Claim Number";
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "missingreportnumber")
                    {
                        this.lblHeader.Text = "Missing Report Number";
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "missingpolicyholder")
                    {
                        this.lblHeader.Text = "Missing Policy Holder";
                    }
                    else if ((base.Request.QueryString["Flag"].ToString().ToLower() == "report") && (base.Request.QueryString["Type"] != null))
                    {
                        if (base.Request.QueryString["Type"].ToString() == "R")
                        {
                            this.lblHeader.Text = "Received Report";
                            this.BindGridQueryString("True");
                        }
                        else
                        {
                            this.lblHeader.Text = "Pending Report";
                            this.txtHeading.Text = "Pending Report";
                            this.BindGridQueryString("False");
                        }
                    }
                }
                if (this.objSessionSystem.SZ_CHART_NO == "0")
                {
                    this.grdpaidbills.Columns[1].Visible = false;
                    this.grdBills.Columns[4].Visible = false;
                }
                else
                {
                    this.grdBills.Columns[4].Visible = true;
                }
                if (this.objSessionSystem.SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION != "1")
                {
                    this.ddlDateValues.Visible = false;
                    this.lblvisitdate.Visible = false;
                    this.lblfrom.Visible = false;
                    this.lblto.Visible = false;
                    this.txtVisitDate.Visible = false;
                    this.imgVisit.Visible = false;
                    this.btnSearch.Visible = false;
                    this.txtToVisitDate.Visible = false;
                    this.imgVisite1.Visible = false;
                }
                else
                {
                    this.ddlDateValues.Visible = true;
                    this.lblvisitdate.Visible = true;
                    this.lblfrom.Visible = true;
                    this.lblto.Visible = true;
                    this.txtVisitDate.Visible = true;
                    this.imgVisit.Visible = true;
                    this.btnSearch.Visible = true;
                    this.txtToVisitDate.Visible = true;
                    this.imgVisite1.Visible = true;
                }
            }
            this.txtVisit.Text = this.drpdown_Documents.SelectedValue.ToString();
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
        if (this.objSessionCompanyAppStatus.SZ_READ_ONLY.ToString().Equals("True"))
        {
            new Bill_Sys_ChangeVersion(this.Page).MakeReadOnlyPage("Bill_Sys_paid_bills.aspx");
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.grdBills.Visible)
        {
            try
            {
                this.BillsSum.DataSource = ((DataSet)this.grdBills.DataSource).Tables[2];
                this.BillsSum.DataBind();
            }
            catch
            {
            }
        }
        if (this.grdpaidbills.Visible)
        {
            for (int i = 0; i < this.grdpaidbills.Rows.Count; i++)
            {
                if (this.grdpaidbills.Rows[i].Cells[10].Text.ToLower().Contains("unknown"))
                {
                    this.grdpaidbills.Rows[i].Cells[10].Text = "";
                    this.grdpaidbills.Rows[i].Cells[9].Text = "";
                }
            }
        }
    }

    protected void txtUpdate_Click(object sender, EventArgs e)
    {
        int selectedIndex = this.grdpaidbills.PageNumberList.SelectedIndex;
        this.grdpaidbills.XGridBindSearch();
        this.grdpaidbills.PageNumberList.SelectedIndex = selectedIndex;
    }

    protected void ASPxPopupControl1_WindowCallback(object source, DevExpress.Web.PopupWindowCallbackArgs e)
    {
        ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "RefreshPage", "RefreshPage();", true);
    }

}
