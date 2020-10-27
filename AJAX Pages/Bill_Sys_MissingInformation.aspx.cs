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
using XGridPagination;


public partial class Bill_Sys_MissingInformation : PageBase
{
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private Bill_Sys_Case _bill_Sys_Case;
    private Bill_Sys_ReportBO _reportBO;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    Bill_Sys_MissingInformationDAO _Bill_Sys_MissingInformationDAO;
    StringBuilder szExportoExcelColumname = new StringBuilder();
    StringBuilder szExportoExcelField = new StringBuilder();
    DataTable dt;
    string strLinkPath = null;
    //ashutosh
    string paidId = "";

    Bill_Sys_NF3_Template objNF3Template;

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.SearchMissingUnsentNF2();
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

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = this.GeneratePDF();
            if (str != "")
            {
                string str2 = ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"];
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + (str2 + str).ToString() + "'); ", true);
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
            foreach (DataRow row in dbo.GetConfigDashBoard(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE).Rows)
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
                            this.grdTotalVisit.DataSource = dbo.getVisitDetails(this.txtCompanyID.Text, "TOTALCOUNT");
                            this.grdTotalVisit.DataBind();
                            this.grdVisit.DataSource = dbo.getVisitDetails(this.txtCompanyID.Text, "BILLEDVISIT");
                            this.grdVisit.DataBind();
                            this.grdUnVisit.DataSource = dbo.getVisitDetails(this.txtCompanyID.Text, "UNBILLEDVISIT");
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

    public DataTable DisplayLocationInClaimNumberGrid(DataTable p_objDS)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataTable table = new DataTable();
        try
        {
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION.ToString() != "1")
            {
                table.Columns.Add("SZ_CASE_NO");
                table.Columns.Add("SZ_CASE_ID");
                table.Columns.Add("SZ_PATIENT_NAME");
                table.Columns.Add("SZ_CASE_TYPE_NAME");
                table.Columns.Add("SZ_OFFICE_NAME");
                table.Columns.Add("SZ_DOCTOR_NAME");
                table.Columns.Add("DT_DATE_OF_ACCIDENT");
                table.Columns.Add("SZ_INSURANCE_NAME");
                table.Columns.Add("SZ_Company_id");
                table.Columns.Add("SZ_LOCATION_NAME");
                string str = "NA";
                for (int j = 0; j < p_objDS.Rows.Count; j++)
                {
                    DataRow row;
                    if (p_objDS.Rows[j]["SZ_LOCATION_NAME"].ToString().Equals(str))
                    {
                        row = table.NewRow();
                        row["SZ_CASE_NO"] = p_objDS.Rows[j]["SZ_CASE_NO"].ToString();
                        row["SZ_CASE_ID"] = p_objDS.Rows[j]["SZ_CASE_ID"].ToString();
                        row["SZ_PATIENT_NAME"] = p_objDS.Rows[j]["SZ_PATIENT_NAME"].ToString();
                        row["SZ_CASE_TYPE_NAME"] = p_objDS.Rows[j]["SZ_CASE_TYPE_NAME"].ToString();
                        row["SZ_OFFICE_NAME"] = p_objDS.Rows[j]["SZ_OFFICE_NAME"].ToString();
                        row["SZ_DOCTOR_NAME"] = p_objDS.Rows[j]["SZ_DOCTOR_NAME"].ToString();
                        row["SZ_INSURANCE_NAME"] = p_objDS.Rows[j]["SZ_INSURANCE_NAME"].ToString();
                        row["SZ_Company_id"] = p_objDS.Rows[j]["SZ_Company_id"].ToString();
                        string str2 = p_objDS.Rows[j]["DT_DATE_OF_ACCIDENT"].ToString();
                        string str3 = "";
                        if (str2.ToString() != "")
                        {
                            str3 = Convert.ToDateTime(str2).ToString("ddMMMyyyy");
                        }
                        row["DT_DATE_OF_ACCIDENT"] = str3.ToString();
                        row["SZ_LOCATION_NAME"] = p_objDS.Rows[j]["SZ_LOCATION_NAME"].ToString();
                        table.Rows.Add(row);
                        str = p_objDS.Rows[j]["SZ_LOCATION_NAME"].ToString();
                    }
                    else
                    {
                        row = table.NewRow();
                        row["SZ_CASE_NO"] = "";
                        row["SZ_CASE_ID"] = "";
                        row["SZ_PATIENT_NAME"] = p_objDS.Rows[j]["SZ_LOCATION_NAME"].ToString();
                        row["SZ_CASE_TYPE_NAME"] = "";
                        row["SZ_OFFICE_NAME"] = "";
                        row["SZ_DOCTOR_NAME"] = "";
                        row["SZ_INSURANCE_NAME"] = "";
                        row["SZ_Company_id"] = "";
                        row["SZ_LOCATION_NAME"] = "";
                        table.Rows.Add(row);
                        row = table.NewRow();
                        row["SZ_CASE_NO"] = p_objDS.Rows[j]["SZ_CASE_NO"].ToString();
                        row["SZ_CASE_ID"] = p_objDS.Rows[j]["SZ_CASE_ID"].ToString();
                        row["SZ_PATIENT_NAME"] = p_objDS.Rows[j]["SZ_PATIENT_NAME"].ToString();
                        row["SZ_CASE_TYPE_NAME"] = p_objDS.Rows[j]["SZ_CASE_TYPE_NAME"].ToString();
                        row["SZ_OFFICE_NAME"] = p_objDS.Rows[j]["SZ_OFFICE_NAME"].ToString();
                        row["SZ_DOCTOR_NAME"] = p_objDS.Rows[j]["SZ_DOCTOR_NAME"].ToString();
                        row["SZ_INSURANCE_NAME"] = p_objDS.Rows[j]["SZ_INSURANCE_NAME"].ToString();
                        row["SZ_Company_id"] = p_objDS.Rows[j]["SZ_Company_id"].ToString();
                        string str4 = p_objDS.Rows[j]["DT_DATE_OF_ACCIDENT"].ToString();
                        string str5 = "";
                        if (str4.ToString() != "")
                        {
                            str5 = Convert.ToDateTime(str4).ToString("ddMMMyyyy");
                        }
                        row["DT_DATE_OF_ACCIDENT"] = str5.ToString();
                        row["SZ_LOCATION_NAME"] = p_objDS.Rows[j]["SZ_LOCATION_NAME"].ToString();
                        table.Rows.Add(row);
                        str = p_objDS.Rows[j]["SZ_LOCATION_NAME"].ToString();
                    }
                }
                return table;
            }
            table.Columns.Add("SZ_CASE_NO");
            table.Columns.Add("SZ_CASE_ID");
            table.Columns.Add("SZ_PATIENT_NAME");
            table.Columns.Add("SZ_CASE_TYPE_NAME");
            table.Columns.Add("SZ_OFFICE_NAME");
            table.Columns.Add("SZ_DOCTOR_NAME");
            table.Columns.Add("DT_DATE_OF_ACCIDENT");
            table.Columns.Add("SZ_INSURANCE_NAME");
            table.Columns.Add("SZ_Company_id");
            table.Columns.Add("SZ_LOCATION_NAME");
            table.Columns.Add("sz_remote_case_id");
            string str6 = "NA";
            for (int i = 0; i < p_objDS.Rows.Count; i++)
            {
                DataRow row2;
                if (p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString().Equals(str6))
                {
                    row2 = table.NewRow();
                    row2["SZ_CASE_NO"] = p_objDS.Rows[i]["SZ_CASE_NO"].ToString();
                    row2["SZ_CASE_ID"] = p_objDS.Rows[i]["SZ_CASE_ID"].ToString();
                    row2["SZ_PATIENT_NAME"] = p_objDS.Rows[i]["SZ_PATIENT_NAME"].ToString();
                    row2["SZ_CASE_TYPE_NAME"] = p_objDS.Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                    row2["SZ_OFFICE_NAME"] = p_objDS.Rows[i]["SZ_OFFICE_NAME"].ToString();
                    row2["SZ_DOCTOR_NAME"] = p_objDS.Rows[i]["SZ_DOCTOR_NAME"].ToString();
                    row2["SZ_INSURANCE_NAME"] = p_objDS.Rows[i]["SZ_INSURANCE_NAME"].ToString();
                    row2["SZ_Company_id"] = p_objDS.Rows[i]["SZ_Company_id"].ToString();
                    string str7 = p_objDS.Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    string str8 = "";
                    if (str7.ToString() != "")
                    {
                        str8 = Convert.ToDateTime(str7).ToString("ddMMMyyyy");
                    }
                    row2["DT_DATE_OF_ACCIDENT"] = str8.ToString();
                    row2["SZ_LOCATION_NAME"] = p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString();
                    row2["sz_remote_case_id"] = p_objDS.Rows[i]["sz_remote_case_id"].ToString();
                    table.Rows.Add(row2);
                    str6 = p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString();
                }
                else
                {
                    row2 = table.NewRow();
                    row2["SZ_CASE_NO"] = "";
                    row2["SZ_CASE_ID"] = "";
                    row2["SZ_PATIENT_NAME"] = p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString();
                    row2["SZ_CASE_TYPE_NAME"] = "";
                    row2["SZ_OFFICE_NAME"] = "";
                    row2["SZ_DOCTOR_NAME"] = "";
                    row2["SZ_INSURANCE_NAME"] = "";
                    row2["SZ_Company_id"] = "";
                    row2["SZ_LOCATION_NAME"] = "";
                    row2["sz_remote_case_id"] = "";
                    table.Rows.Add(row2);
                    row2 = table.NewRow();
                    row2["SZ_CASE_NO"] = p_objDS.Rows[i]["SZ_CASE_NO"].ToString();
                    row2["SZ_CASE_ID"] = p_objDS.Rows[i]["SZ_CASE_ID"].ToString();
                    row2["SZ_PATIENT_NAME"] = p_objDS.Rows[i]["SZ_PATIENT_NAME"].ToString();
                    row2["SZ_CASE_TYPE_NAME"] = p_objDS.Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                    row2["SZ_OFFICE_NAME"] = p_objDS.Rows[i]["SZ_OFFICE_NAME"].ToString();
                    row2["SZ_DOCTOR_NAME"] = p_objDS.Rows[i]["SZ_DOCTOR_NAME"].ToString();
                    row2["SZ_INSURANCE_NAME"] = p_objDS.Rows[i]["SZ_INSURANCE_NAME"].ToString();
                    row2["SZ_Company_id"] = p_objDS.Rows[i]["SZ_Company_id"].ToString();
                    string str9 = p_objDS.Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    string str10 = "";
                    if (str9.ToString() != "")
                    {
                        str10 = Convert.ToDateTime(str9).ToString("ddMMMyyyy");
                    }
                    row2["DT_DATE_OF_ACCIDENT"] = str10.ToString();
                    row2["SZ_LOCATION_NAME"] = p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString();
                    row2["sz_remote_case_id"] = p_objDS.Rows[i]["sz_remote_case_id"].ToString();
                    table.Rows.Add(row2);
                    str6 = p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString();
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
        return table;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataTable DisplayLocationInGrid(DataTable p_objDS)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataTable table = new DataTable();
        try
        {
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION.ToString() != "1")
            {
                table.Columns.Add("SZ_CASE_NO");
                table.Columns.Add("SZ_CASE_ID");
                table.Columns.Add("SZ_PATIENT_NAME");
                table.Columns.Add("SZ_CASE_TYPE_NAME");
                table.Columns.Add("SZ_OFFICE_NAME");
                table.Columns.Add("SZ_DOCTOR_NAME");
                table.Columns.Add("DT_DATE_OF_ACCIDENT");
                table.Columns.Add("SZ_Company_id");
                table.Columns.Add("SZ_LOCATION_NAME");
                string str = "NA";
                for (int j = 0; j < p_objDS.Rows.Count; j++)
                {
                    DataRow row;
                    if (p_objDS.Rows[j]["SZ_LOCATION_NAME"].ToString().Equals(str))
                    {
                        row = table.NewRow();
                        row["SZ_CASE_NO"] = p_objDS.Rows[j]["SZ_CASE_NO"].ToString();
                        row["SZ_CASE_ID"] = p_objDS.Rows[j]["SZ_CASE_ID"].ToString();
                        row["SZ_PATIENT_NAME"] = p_objDS.Rows[j]["SZ_PATIENT_NAME"].ToString();
                        row["SZ_CASE_TYPE_NAME"] = p_objDS.Rows[j]["SZ_CASE_TYPE_NAME"].ToString();
                        row["SZ_OFFICE_NAME"] = p_objDS.Rows[j]["SZ_OFFICE_NAME"].ToString();
                        row["SZ_DOCTOR_NAME"] = p_objDS.Rows[j]["SZ_DOCTOR_NAME"].ToString();
                        row["SZ_Company_id"] = p_objDS.Rows[j]["SZ_COMPANY_ID"].ToString();
                        string str2 = p_objDS.Rows[j]["DT_DATE_OF_ACCIDENT"].ToString();
                        string str3 = "";
                        if (str2.ToString() != "")
                        {
                            str3 = Convert.ToDateTime(str2).ToString("ddMMMyyyy");
                        }
                        row["DT_DATE_OF_ACCIDENT"] = str3.ToString();
                        row["SZ_LOCATION_NAME"] = p_objDS.Rows[j]["SZ_LOCATION_NAME"].ToString();
                        table.Rows.Add(row);
                        str = p_objDS.Rows[j]["SZ_LOCATION_NAME"].ToString();
                    }
                    else
                    {
                        row = table.NewRow();
                        row["SZ_CASE_NO"] = "";
                        row["SZ_CASE_ID"] = "";
                        row["SZ_PATIENT_NAME"] = p_objDS.Rows[j]["SZ_LOCATION_NAME"].ToString();
                        row["SZ_CASE_TYPE_NAME"] = "";
                        row["SZ_OFFICE_NAME"] = "";
                        row["SZ_DOCTOR_NAME"] = "";
                        row["SZ_Company_id"] = "";
                        row["SZ_LOCATION_NAME"] = "";
                        int count = this.grdCaseMaster.Rows.Count;
                        table.Rows.Add(row);
                        row = table.NewRow();
                        row["SZ_CASE_NO"] = p_objDS.Rows[j]["SZ_CASE_NO"].ToString();
                        row["SZ_CASE_ID"] = p_objDS.Rows[j]["SZ_CASE_ID"].ToString();
                        row["SZ_PATIENT_NAME"] = p_objDS.Rows[j]["SZ_PATIENT_NAME"].ToString();
                        row["SZ_CASE_TYPE_NAME"] = p_objDS.Rows[j]["SZ_CASE_TYPE_NAME"].ToString();
                        row["SZ_OFFICE_NAME"] = p_objDS.Rows[j]["SZ_OFFICE_NAME"].ToString();
                        row["SZ_DOCTOR_NAME"] = p_objDS.Rows[j]["SZ_DOCTOR_NAME"].ToString();
                        row["SZ_Company_id"] = p_objDS.Rows[j]["SZ_COMPANY_ID"].ToString();
                        string str4 = p_objDS.Rows[j]["DT_DATE_OF_ACCIDENT"].ToString();
                        string str5 = "";
                        if (str4.ToString() != "")
                        {
                            str5 = Convert.ToDateTime(str4).ToString("ddMMMyyyy");
                        }
                        row["DT_DATE_OF_ACCIDENT"] = str5.ToString();
                        row["SZ_LOCATION_NAME"] = p_objDS.Rows[j]["SZ_LOCATION_NAME"].ToString();
                        table.Rows.Add(row);
                        str = p_objDS.Rows[j]["SZ_LOCATION_NAME"].ToString();
                    }
                }
                return table;
            }
            table.Columns.Add("SZ_CASE_NO");
            table.Columns.Add("SZ_CASE_ID");
            table.Columns.Add("SZ_PATIENT_NAME");
            table.Columns.Add("SZ_CASE_TYPE_NAME");
            table.Columns.Add("SZ_OFFICE_NAME");
            table.Columns.Add("SZ_DOCTOR_NAME");
            table.Columns.Add("DT_DATE_OF_ACCIDENT");
            table.Columns.Add("SZ_Company_id");
            table.Columns.Add("SZ_LOCATION_NAME");
            table.Columns.Add("sz_remote_case_id");
            string str6 = "NA";
            for (int i = 0; i < p_objDS.Rows.Count; i++)
            {
                DataRow row2;
                if (p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString().Equals(str6))
                {
                    row2 = table.NewRow();
                    row2["SZ_CASE_NO"] = p_objDS.Rows[i]["SZ_CASE_NO"].ToString();
                    row2["SZ_CASE_ID"] = p_objDS.Rows[i]["SZ_CASE_ID"].ToString();
                    row2["SZ_PATIENT_NAME"] = p_objDS.Rows[i]["SZ_PATIENT_NAME"].ToString();
                    row2["SZ_CASE_TYPE_NAME"] = p_objDS.Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                    row2["SZ_OFFICE_NAME"] = p_objDS.Rows[i]["SZ_OFFICE_NAME"].ToString();
                    row2["SZ_DOCTOR_NAME"] = p_objDS.Rows[i]["SZ_DOCTOR_NAME"].ToString();
                    row2["SZ_Company_id"] = p_objDS.Rows[i]["SZ_COMPANY_ID"].ToString();
                    string str7 = p_objDS.Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    string str8 = "";
                    if (str7.ToString() != "")
                    {
                        str8 = Convert.ToDateTime(str7).ToString("ddMMMyyyy");
                    }
                    row2["DT_DATE_OF_ACCIDENT"] = str8.ToString();
                    row2["SZ_LOCATION_NAME"] = p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString();
                    row2["sz_remote_case_id"] = p_objDS.Rows[i]["sz_remote_case_id"].ToString();
                    table.Rows.Add(row2);
                    str6 = p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString();
                }
                else
                {
                    row2 = table.NewRow();
                    row2["SZ_CASE_NO"] = "";
                    row2["SZ_CASE_ID"] = "";
                    row2["SZ_PATIENT_NAME"] = p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString();
                    row2["SZ_CASE_TYPE_NAME"] = "";
                    row2["SZ_OFFICE_NAME"] = "";
                    row2["SZ_DOCTOR_NAME"] = "";
                    row2["SZ_Company_id"] = "";
                    row2["SZ_LOCATION_NAME"] = "";
                    row2["sz_remote_case_id"] = "";
                    int num3 = this.grdCaseMaster.Rows.Count;
                    table.Rows.Add(row2);
                    row2 = table.NewRow();
                    row2["SZ_CASE_NO"] = p_objDS.Rows[i]["SZ_CASE_NO"].ToString();
                    row2["SZ_CASE_ID"] = p_objDS.Rows[i]["SZ_CASE_ID"].ToString();
                    row2["SZ_PATIENT_NAME"] = p_objDS.Rows[i]["SZ_PATIENT_NAME"].ToString();
                    row2["SZ_CASE_TYPE_NAME"] = p_objDS.Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                    row2["SZ_OFFICE_NAME"] = p_objDS.Rows[i]["SZ_OFFICE_NAME"].ToString();
                    row2["SZ_DOCTOR_NAME"] = p_objDS.Rows[i]["SZ_DOCTOR_NAME"].ToString();
                    row2["SZ_Company_id"] = p_objDS.Rows[i]["SZ_COMPANY_ID"].ToString();
                    string str9 = p_objDS.Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    string str10 = "";
                    if (str9.ToString() != "")
                    {
                        str10 = Convert.ToDateTime(str9).ToString("ddMMMyyyy");
                    }
                    row2["DT_DATE_OF_ACCIDENT"] = str10.ToString();
                    row2["SZ_LOCATION_NAME"] = p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString();
                    row2["sz_remote_case_id"] = p_objDS.Rows[i]["sz_remote_case_id"].ToString();
                    table.Rows.Add(row2);
                    str6 = p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString();
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
        return table;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ExportToExcel(DataTable dt)
    {
        this.grdExportToExcel.DataSource = dt;
        this.grdExportToExcel.DataBind();
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION.ToString() != "1")
        {
            this.grdExportToExcel.Columns[11].Visible = false;
        }
        else
        {
            this.grdExportToExcel.Columns[11].Visible = true;
        }
        StringBuilder builder = new StringBuilder();
        builder.Append("<table border='1px'>");
        for (int i = 0; i < this.grdExportToExcel.Items.Count; i++)
        {
            if (i == 0)
            {
                builder.Append("<tr>");
                for (int k = 0; k < this.grdExportToExcel.Columns.Count; k++)
                {
                    if (this.grdExportToExcel.Columns[k].Visible)
                    {
                        builder.Append("<td>");
                        builder.Append(this.grdExportToExcel.Columns[k].HeaderText);
                        builder.Append("</td>");
                    }
                }
                builder.Append("</tr>");
            }
            builder.Append("<tr>");
            for (int j = 0; j < this.grdExportToExcel.Columns.Count; j++)
            {
                if (this.grdExportToExcel.Columns[j].Visible)
                {
                    builder.Append("<td>");
                    if ((j == 1) && (this.grdExportToExcel.Items[i].Cells[5].Text == "&nbsp;"))
                    {
                        builder.Append("<b>Location</b>");
                    }
                    else
                    {
                        builder.Append(this.grdExportToExcel.Items[i].Cells[j].Text);
                    }
                    builder.Append("</td>");
                }
            }
            builder.Append("</tr>");
        }
        builder.Append("</table>");
        string str = this.getFileName("EXCEL") + ".xls";
        StreamWriter writer = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + str);
        writer.Write(builder);
        writer.Close();
        base.Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + str, false);
    }

    protected void ExportToExcelClaimNo(DataTable dt)
    {
        this.grdExportToExcelClaimNo.DataSource = dt;
        this.grdExportToExcelClaimNo.DataBind();
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION.ToString() != "1")
        {
            this.grdExportToExcelClaimNo.Columns[12].Visible = false;
        }
        else
        {
            this.grdExportToExcelClaimNo.Columns[12].Visible = true;
        }
        StringBuilder builder = new StringBuilder();
        builder.Append("<table border='1px'>");
        for (int i = 0; i < this.grdExportToExcelClaimNo.Items.Count; i++)
        {
            if (i == 0)
            {
                builder.Append("<tr>");
                for (int k = 0; k < this.grdExportToExcelClaimNo.Columns.Count; k++)
                {
                    if (this.grdExportToExcelClaimNo.Columns[k].Visible)
                    {
                        builder.Append("<td>");
                        builder.Append(this.grdExportToExcelClaimNo.Columns[k].HeaderText);
                        builder.Append("</td>");
                    }
                }
                builder.Append("</tr>");
            }
            builder.Append("<tr>");
            for (int j = 0; j < this.grdExportToExcelClaimNo.Columns.Count; j++)
            {
                if (this.grdExportToExcelClaimNo.Columns[j].Visible)
                {
                    builder.Append("<td>");
                    if ((j == 1) && (this.grdExportToExcelClaimNo.Items[i].Cells[6].Text == "&nbsp;"))
                    {
                        builder.Append("<b>Location</b>");
                    }
                    else
                    {
                        builder.Append(this.grdExportToExcelClaimNo.Items[i].Cells[j].Text);
                    }
                    builder.Append("</td>");
                }
            }
            builder.Append("</tr>");
        }
        builder.Append("</table>");
        string str = this.getFileName("EXCEL") + ".xls";
        StreamWriter writer = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + str);
        writer.Write(builder);
        writer.Close();
        base.Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + str, false);
    }

    protected string GenerateHTML()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.Session["VL_COUNT"] = "";
        string str = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
        try
        {
            int num = 0;
            int num2 = 0;
            foreach (GridViewRow row in this.grdUnsentNF2.Rows)
            {
                if (((CheckBox)row.Cells[0].FindControl("ChkSent")).Checked)
                {
                    string str2 = str;
                    string[] strArray = new string[] { str2, "<tr><td style='font-size:9px'>", (num + 1).ToString(), "</td><td style='font-size:9px'>", this.grdUnsentNF2.DataKeys[num2][1].ToString(), "</td><td style='font-size:9px'><b>", row.Cells[3].Text, "</b><br/>", this.grdUnsentNF2.DataKeys[num2][2].ToString(), "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>", row.Cells[1].Text, "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>", row.Cells[2].Text, "</td></tr>" };
                    str = string.Concat(strArray);
                    num++;
                    this.Session["VL_COUNT"] = num;
                }
                num2++;
            }
            str = str + "</table>";
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
        return str;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected string GeneratePDF()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        GeneratePatientInfoPDF opdf = new GeneratePatientInfoPDF();
        string str = "";
        try
        {
            string str2 = File.ReadAllText(ConfigurationManager.AppSettings["NF2_SENT_MAIL_HTML"]);
            str2 = opdf.getNF2MailDetails(str2, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            string newValue = this.GenerateHTML();
            if (!this.Session["VL_COUNT"].ToString().Equals(""))
            {
                str2 = str2.Replace("VL_SZ_TABLE_DATA", newValue).Replace("VL_SZ_CASE_COUNT", this.Session["VL_COUNT"].ToString());
                SautinSoft.PdfMetamorphosis metamorphosis = new SautinSoft.PdfMetamorphosis();
                metamorphosis.Serial="10007706603";
                string str4 = this.getFileName("P") + ".htm";
                str = this.getFileName("P") + ".pdf";
                StreamWriter writer = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + str4);
                writer.Write(str2);
                writer.Close();
                metamorphosis.HtmlToPdfConvertFile(ConfigurationManager.AppSettings["EXCEL_SHEET"] + str4, ConfigurationManager.AppSettings["EXCEL_SHEET"] + str);
                return str;
            }
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", "CheckBoxClick();", true);
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
        return str;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string getFileName(string p_szBillNumber)
    {
        DateTime now = DateTime.Now;
        return (p_szBillNumber + "_" + this.getRandomNumber() + "_" + now.ToString("yyyyMMddHHmmssms"));
    }

    private string getRandomNumber()
    {
        Random random = new Random();
        return random.Next(1, 0x2710).ToString();
    }

    protected void grdCaseMaster_OnRowCommand(object source, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Select")
            {
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                string str = strArray[0];
                int num = Convert.ToInt32(strArray[1].ToString());
                CaseDetailsBO sbo = new CaseDetailsBO();
                Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
                obj2.SZ_PATIENT_ID=sbo.GetCasePatientID(str.ToString(), "");
                obj2.SZ_CASE_ID=str.ToString();
                obj2.SZ_PATIENT_NAME=this.grdCaseMaster.DataKeys[num][0].ToString();
                this.Session["CASE_OBJECT"] = obj2;
                this._bill_Sys_Case = new Bill_Sys_Case();
                this._bill_Sys_Case.SZ_CASE_ID=str.ToString();
                this.Session["CASEINFO"] = this._bill_Sys_Case;
                base.Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
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

    protected void grdCaseMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        int num = Convert.ToInt32(this.XGridPaginationDropDown2.SelectedValue);
        this.grdCaseMaster.SelectablePageIndexChanged(Convert.ToInt16(this.XGridPaginationDropDown2.SelectedValue) - 1);
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
        {
            this.searchcaselist("GET_MISSING_INSURANCE_LIST");
            this.XGridPaginationDropDown2.SelectedValue = num.ToString();
        }
    }

    protected void grdMissingAttorney_OnRowCommand(object source, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Select")
            {
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                string str = strArray[0];
                int num = Convert.ToInt32(strArray[1].ToString());
                CaseDetailsBO sbo = new CaseDetailsBO();
                Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
                obj2.SZ_PATIENT_ID=sbo.GetCasePatientID(str.ToString(), "");
                obj2.SZ_CASE_ID=str.ToString();
                obj2.SZ_PATIENT_NAME=this.grdMissingAttorney.DataKeys[num][0].ToString();
                this.Session["CASE_OBJECT"] = obj2;
                this._bill_Sys_Case = new Bill_Sys_Case();
                this._bill_Sys_Case.SZ_CASE_ID=str.ToString();
                this.Session["CASEINFO"] = this._bill_Sys_Case;
                base.Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
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

    protected void grdMissingAttorney_SelectedIndexChanged(object sender, EventArgs e)
    {
        int num = Convert.ToInt32(this.XGridPaginationDropDown3.SelectedValue);
        this.grdMissingAttorney.SelectablePageIndexChanged(Convert.ToInt16(this.XGridPaginationDropDown3.SelectedValue) - 1);
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
        {
            this.searchcaselistForMissingAttorney("GET_MISSING_ATTORNEY_LIST");
            this.XGridPaginationDropDown3.SelectedValue = num.ToString();
        }
    }

    protected void grdMissingClaimNo_OnRowCommand(object source, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Select")
            {
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                string str = strArray[0];
                int num = Convert.ToInt32(strArray[1].ToString());
                CaseDetailsBO sbo = new CaseDetailsBO();
                Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
                obj2.SZ_PATIENT_ID=sbo.GetCasePatientID(str.ToString(), "");
                obj2.SZ_CASE_ID=str.ToString();
                obj2.SZ_PATIENT_NAME=this.grdMissingClaimNo.DataKeys[num][0].ToString();
                this.Session["CASE_OBJECT"] = obj2;
                this._bill_Sys_Case = new Bill_Sys_Case();
                this._bill_Sys_Case.SZ_CASE_ID=str.ToString();
                this.Session["CASEINFO"] = this._bill_Sys_Case;
                base.Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
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

    protected void grdMissingClaimNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int num = Convert.ToInt32(this.XGridPaginationDropDown4.SelectedValue);
        this.grdMissingClaimNo.SelectablePageIndexChanged(Convert.ToInt16(this.XGridPaginationDropDown4.SelectedValue) - 1);
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
        {
            this.SearchMissingClaimNo("GET_MISSING_CLAIM_LIST");
            this.XGridPaginationDropDown4.SelectedValue = num.ToString();
        }
    }

    protected void grdMissingPolicyholder_OnRowCommand(object source, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Select")
            {
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                string str = strArray[0];
                int num = Convert.ToInt32(strArray[1].ToString());
                CaseDetailsBO sbo = new CaseDetailsBO();
                Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
                obj2.SZ_PATIENT_ID=sbo.GetCasePatientID(str.ToString(), "");
                obj2.SZ_CASE_ID=str.ToString();
                obj2.SZ_PATIENT_NAME=this.grdMissingPolicyholder.DataKeys[num][0].ToString();
                this.Session["CASE_OBJECT"] = obj2;
                this._bill_Sys_Case = new Bill_Sys_Case();
                this._bill_Sys_Case.SZ_CASE_ID=str.ToString();
                this.Session["CASEINFO"] = this._bill_Sys_Case;
                base.Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
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

    protected void grdMissingPolicyholder_SelectedIndexChanged(object sender, EventArgs e)
    {
        int num = Convert.ToInt32(this.XGridPaginationDropDown5.SelectedValue);
        this.grdMissingPolicyholder.SelectablePageIndexChanged(Convert.ToInt16(this.XGridPaginationDropDown5.SelectedValue) - 1);
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
        {
            this.SearchMissingPolicyHolder("GET_MISSING_POCLICY_HOLDER");
            this.XGridPaginationDropDown5.SelectedValue = num.ToString();
        }
    }

    protected void grdMissingReportNumber_OnRowCommand(object source, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Select")
            {
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                string str = strArray[0];
                int num = Convert.ToInt32(strArray[1].ToString());
                CaseDetailsBO sbo = new CaseDetailsBO();
                Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
                obj2.SZ_PATIENT_ID=sbo.GetCasePatientID(str.ToString(), "");
                obj2.SZ_CASE_ID=str.ToString();
                obj2.SZ_PATIENT_NAME=this.grdMissingReportNumber.DataKeys[num][0].ToString();
                this.Session["CASE_OBJECT"] = obj2;
                this._bill_Sys_Case = new Bill_Sys_Case();
                this._bill_Sys_Case.SZ_CASE_ID=str.ToString();
                this.Session["CASEINFO"] = this._bill_Sys_Case;
                base.Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
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

    protected void grdMissingReportNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        int num = Convert.ToInt32(this.XGridPaginationDropDown1.SelectedValue);
        this.grdMissingReportNumber.SelectablePageIndexChanged(Convert.ToInt16(this.XGridPaginationDropDown1.SelectedValue) - 1);
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
        {
            this.SearchMissingReportNumber("GET_MISSING_REPORT_LIST");
            this.XGridPaginationDropDown1.SelectedValue = num.ToString();
        }
    }

    protected void grdUnsentNF_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Select")
            {
                CaseDetailsBO sbo = new CaseDetailsBO();
                Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
                obj2.SZ_PATIENT_ID=sbo.GetCasePatientID(e.CommandArgument.ToString(), "");
                obj2.SZ_CASE_ID=e.CommandArgument.ToString();
                obj2.SZ_PATIENT_NAME=e.Item.Cells[2].Text;
                obj2.SZ_CASE_NO=((LinkButton)e.Item.FindControl("lnkSelectCase2")).Text;
                this.Session["CASE_OBJECT"] = obj2;
                this._bill_Sys_Case = new Bill_Sys_Case();
                this._bill_Sys_Case.SZ_CASE_ID=e.CommandArgument.ToString();
                this.Session["CASEINFO"] = this._bill_Sys_Case;
                base.Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
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

    protected void lnkExportTOExcelgrdgrdMissingClaimNo_onclick(object sender, EventArgs e)
    {
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
        {
            this._Bill_Sys_MissingInformationDAO = new Bill_Sys_MissingInformationDAO();
            this.dt = new DataTable();
            this.dt = this._Bill_Sys_MissingInformationDAO.GET_MISSING_CLAIMNO(this.txtCompanyID.Text, Convert.ToInt32(1), Convert.ToInt32(this.grdMissingClaimNo.RecordCount));
            if ((this.dt.Rows.Count > 0) && (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
            {
                this.dt = this.DisplayLocationInClaimNumberGrid(this.dt);
            }
            this.ExportToExcelClaimNo(this.dt);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdMissingClaimNo.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
        }
    }

    protected void lnkExportTOExcelgrdMissingAttorney_onclick(object sender, EventArgs e)
    {
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
        {
            this._Bill_Sys_MissingInformationDAO = new Bill_Sys_MissingInformationDAO();
            this.dt = new DataTable();
            this.dt = this._Bill_Sys_MissingInformationDAO.GET_MISSING_ATTORNEY(this.txtCompanyID.Text, Convert.ToInt32(1), Convert.ToInt32(this.grdMissingAttorney.RecordCount));
            if ((this.dt.Rows.Count > 0) && (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
            {
                this.dt = this.DisplayLocationInGrid(this.dt);
            }
            this.ExportToExcel(this.dt);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdMissingAttorney.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
        }
    }

    protected void lnkExportTOExcelgrdMissingPolicyholder_onclick(object sender, EventArgs e)
    {
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
        {
            this._Bill_Sys_MissingInformationDAO = new Bill_Sys_MissingInformationDAO();
            this.dt = new DataTable();
            this.dt = this._Bill_Sys_MissingInformationDAO.GET_MISSING_POLICY_HOLDER(this.txtCompanyID.Text, Convert.ToInt32(1), Convert.ToInt32(this.grdMissingPolicyholder.RecordCount));
            if ((this.dt.Rows.Count > 0) && (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
            {
                this.dt = this.DisplayLocationInGrid(this.dt);
            }
            this.ExportToExcel(this.dt);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdMissingPolicyholder.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
        }
    }

    protected void lnkExportTOExcelgrdMissingUnsentNF2_onclick(object sender, EventArgs e)
    {
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION.ToString() != "1")
        {
            //Change export to excel Column names and field names on 20_01_2015
            this.szExportoExcelColumname.Append("Case #,Patient Name,Insurance Company,Accident Date,Days,First Treatment,Attorney Name,Status");
            this.szExportoExcelField.Append("SZ_CASE_NO,SZ_PATIENT_NAME,SZ_INSURANCE_NAME,DT_DATE_OF_ACCIDENT,DAYS,dt_first_treatment,SZ_ATTORNEY_NAME,STATUS");
        }
        else
        {
            this.szExportoExcelColumname.Append("SZ_CASE_NO,SZ_PATIENT_NAME,SZ_INSURANCE_NAME,DT_DATE_OF_ACCIDENT,DAYS,dt_first_treatment,SZ_ATTORNEY_NAME,STATUS,Patient ID");
            this.szExportoExcelField.Append("Case #,Patient Name,Insurance Company,Accident Date,Days,First Treatment,Attorney Name,Status,sz_remote_case_id");
        }
        this.grdUnsentNF2.ExportToExcelColumnNames=this.szExportoExcelColumname.ToString();
        this.grdUnsentNF2.ExportToExcelFields=this.szExportoExcelField.ToString();
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdUnsentNF2.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    protected void lnkExportTOExcelMissingInsuranceCompany_onclick(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
            {
                this._Bill_Sys_MissingInformationDAO = new Bill_Sys_MissingInformationDAO();
                this.dt = new DataTable();
                this.dt = this._Bill_Sys_MissingInformationDAO.GET_MISSING_INSURANCE(this.txtCompanyID.Text, Convert.ToInt32(1), Convert.ToInt32(this.grdCaseMaster.RecordCount));
                if ((this.dt.Rows.Count > 0) && (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
                {
                    this.dt = this.DisplayLocationInGrid(this.dt);
                }
                this.ExportToExcel(this.dt);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdCaseMaster.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
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

    protected void lnkExportTOExcelMissingReportNumber_onclick(object sender, EventArgs e)
    {
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
        {
            this._Bill_Sys_MissingInformationDAO = new Bill_Sys_MissingInformationDAO();
            this.dt = new DataTable();
            this.dt = this._Bill_Sys_MissingInformationDAO.GET_MISSING_REPORT_NUMBER(this.txtCompanyID.Text, Convert.ToInt32(1), Convert.ToInt32(this.grdMissingReportNumber.RecordCount));
            if ((this.dt.Rows.Count > 0) && (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
            {
                this.dt = this.DisplayLocationInGrid(this.dt);
            }
            this.ExportToExcel(this.dt);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdMissingReportNumber.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.txtUserId.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            this.XGridPaginationDropDown1.SourceGrid=this.grdMissingReportNumber;
            this.grdMissingReportNumber.Page=this.Page;
            this.grdMissingReportNumber.PageNumberList=this.XGridPaginationDropDown1;
            this.XGridPaginationDropDown2.SourceGrid=this.grdCaseMaster;
            this.grdCaseMaster.Page=this.Page;
            this.grdCaseMaster.PageNumberList=this.XGridPaginationDropDown2;
            this.XGridPaginationDropDown3.SourceGrid=this.grdMissingAttorney;
            this.grdMissingAttorney.Page=this.Page;
            this.grdMissingAttorney.PageNumberList=this.XGridPaginationDropDown3;
            this.XGridPaginationDropDown4.SourceGrid=this.grdMissingClaimNo;
            this.grdMissingClaimNo.Page=this.Page;
            this.grdMissingClaimNo.PageNumberList=this.XGridPaginationDropDown4;
            this.XGridPaginationDropDown5.SourceGrid=this.grdMissingPolicyholder;
            this.grdMissingPolicyholder.Page=this.Page;
            this.grdMissingPolicyholder.PageNumberList=this.XGridPaginationDropDown5;
            this.XGridPaginationDropDown6.SourceGrid=this.grdUnsentNF2;
            this.grdUnsentNF2.Page=this.Page;
            this.grdUnsentNF2.PageNumberList=this.XGridPaginationDropDown6;
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
            {
                this.extddlLocation.Visible = true;
                this.lblLocationName.Visible = true;
                this.extddlLocation.Flag_ID=this.txtCompanyID.Text.ToString();
            }
            if ((base.Request.QueryString["popup"] != null) && base.Request.QueryString["popup"].ToString().Equals("done"))
            {
                this.lblMsg.Text = "Report received successfully.";
                this.lblMsg.Visible = true;
            }
            if (!base.IsPostBack)
            {
                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if (this.lblMsg.Text.Equals(""))
                {
                    this.lblMsg.Visible = false;
                }
                this.setLabels();
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    this.grdCaseMaster.Columns[6].Visible = true;
                    this.grdCaseMaster.Columns[7].Visible = true;
                    this.grdMissingReportNumber.Columns[6].Visible = true;
                    this.grdMissingReportNumber.Columns[7].Visible = true;
                    this.grdMissingAttorney.Columns[6].Visible = true;
                    this.grdMissingAttorney.Columns[7].Visible = true;
                    this.grdMissingPolicyholder.Columns[6].Visible = true;
                    this.grdMissingPolicyholder.Columns[7].Visible = true;
                    this.grdMissingClaimNo.Columns[7].Visible = true;
                    this.grdMissingClaimNo.Columns[8].Visible = true;
                    this.grdExportToExcel.Columns[6].Visible = true;
                    this.grdExportToExcel.Columns[7].Visible = true;
                }
                if (base.Request.QueryString["Flag"] != null)
                {
                    if (base.Request.QueryString["Flag"].ToString().ToLower() == "missingattorney")
                    {
                        this.searchcaselistForMissingAttorney("GET_MISSING_ATTORNEY_LIST");
                        this.lblHeader.Text = "Attorney";
                        this.tabcontainerMissingInformation.ActiveTabIndex=1;
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "missinginsurancecompany")
                    {
                        this.searchcaselist("GET_MISSING_INSURANCE_LIST");
                        this.lblHeader.Text = "Insurance Company";
                        this.tabcontainerMissingInformation.ActiveTabIndex=0;
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "missingprovider")
                    {
                        this.searchcaselist("GET_MISSING_PROVIDER_LIST");
                        this.lblHeader.Text = "Provider";
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "missingclaimnumber")
                    {
                        this.grdMissingClaimNo.Columns[5].Visible = true;
                        this.SearchMissingClaimNo("GET_MISSING_CLAIM_LIST");
                        this.lblHeader.Text = "Claim Number";
                        this.tabcontainerMissingInformation.ActiveTabIndex=2;
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "missingreportnumber")
                    {
                        this.SearchMissingReportNumber("GET_MISSING_REPORT_LIST");
                        this.lblHeader.Text = "Report Number";
                        this.tabcontainerMissingInformation.ActiveTabIndex=3;
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "missingpolicyholder")
                    {
                        this.SearchMissingPolicyHolder("GET_MISSING_POCLICY_HOLDER");
                        this.lblHeader.Text = "Policy Holder";
                        this.tabcontainerMissingInformation.ActiveTabIndex=4;
                    }
                    else if (base.Request.QueryString["Flag"].ToString().ToLower() == "unsentnf2")
                    {
                        this.SearchMissingUnsentNF2();
                        this.lblHeader.Text = "Unsent NF2";
                        this.tabcontainerMissingInformation.ActiveTabIndex=5;
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
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            new Bill_Sys_ChangeVersion(this.Page).MakeReadOnlyPage("Bill_Sys_MissingInformation.aspx");
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void searchcaselist(string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
            {
                this.grdCaseMaster.XGridDatasetNumber=2;
                this.grdCaseMaster.XGridBindSearch();
                DataTable table = new DataTable();
                table = this.grdCaseMaster.XGridDataset;
                if (table.Rows.Count > 0)
                {
                    DataView view = new DataView(this.DisplayLocationInGrid(table));
                    this.grdCaseMaster.DataSource = view;
                    this.grdCaseMaster.DataBind();
                }
                for (int i = 0; i < this.grdCaseMaster.Rows.Count; i++)
                {
                    if (this.grdCaseMaster.Rows[i].Cells[5].Text.ToString().ToString().Trim().ToString().Trim() == "&nbsp;")
                    {
                        ((Label)this.grdCaseMaster.Rows[i].Cells[0].FindControl("lblLocationName")).Visible = true;
                        ((LinkButton)this.grdCaseMaster.Rows[i].Cells[0].FindControl("lnkSelectCase2")).Visible = false;
                        this.grdCaseMaster.Rows[i].Cells[9].Visible = false;
                        this.grdCaseMaster.Rows[i].Cells[4].Font.Bold = true;
                    }
                }
            }
            else
            {
                this.grdCaseMaster.XGridBindSearch();
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

    private void searchcaselistForMissingAttorney(string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
            {
                this.grdMissingAttorney.XGridDatasetNumber=2;
                this.grdMissingAttorney.XGridBindSearch();
                DataTable table = new DataTable();
                table = this.grdMissingAttorney.XGridDataset;
                if (table.Rows.Count > 0)
                {
                    DataView view = new DataView(this.DisplayLocationInGrid(table));
                    this.grdMissingAttorney.DataSource = view;
                    this.grdMissingAttorney.DataBind();
                }
                for (int i = 0; i < this.grdMissingAttorney.Rows.Count; i++)
                {
                    if (this.grdMissingAttorney.Rows[i].Cells[5].Text.ToString().ToString().Trim().ToString().Trim() == "&nbsp;")
                    {
                        ((Label)this.grdMissingAttorney.Rows[i].Cells[0].FindControl("lblLocationName")).Visible = true;
                        ((LinkButton)this.grdMissingAttorney.Rows[i].Cells[0].FindControl("lnkSelectCase2")).Visible = false;
                        this.grdMissingAttorney.Rows[i].Cells[9].Visible = false;
                        this.grdMissingAttorney.Rows[i].Cells[4].Font.Bold = true;
                    }
                }
            }
            else
            {
                this.grdMissingAttorney.XGridBindSearch();
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

    private void SearchMissingClaimNo(string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
            {
                this.grdMissingClaimNo.XGridDatasetNumber=2;
                this.grdMissingClaimNo.XGridBindSearch();
                DataTable table = new DataTable();
                table = this.grdMissingClaimNo.XGridDataset;
                if (table.Rows.Count > 0)
                {
                    DataView view = new DataView(this.DisplayLocationInClaimNumberGrid(table));
                    this.grdMissingClaimNo.DataSource = view;
                    this.grdMissingClaimNo.DataBind();
                }
                for (int i = 0; i < this.grdMissingClaimNo.Rows.Count; i++)
                {
                    if (this.grdMissingClaimNo.Rows[i].Cells[6].Text.ToString().ToString().Trim().ToString().Trim() == "&nbsp;")
                    {
                        ((Label)this.grdMissingClaimNo.Rows[i].Cells[0].FindControl("lblLocationName")).Visible = true;
                        ((LinkButton)this.grdMissingClaimNo.Rows[i].Cells[0].FindControl("lnkSelectCase2")).Visible = false;
                        this.grdMissingClaimNo.Rows[i].Cells[10].Visible = false;
                        this.grdMissingClaimNo.Rows[i].Cells[4].Font.Bold = true;
                    }
                }
            }
            else
            {
                this.grdMissingClaimNo.XGridBindSearch();
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

    private void SearchMissingPolicyHolder(string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
            {
                this.grdMissingPolicyholder.XGridDatasetNumber=2;
                this.grdMissingPolicyholder.XGridBindSearch();
                DataTable table = new DataTable();
                table = this.grdMissingPolicyholder.XGridDataset;
                if (table.Rows.Count > 0)
                {
                    DataView view = new DataView(this.DisplayLocationInGrid(table));
                    this.grdMissingPolicyholder.DataSource = view;
                    this.grdMissingPolicyholder.DataBind();
                }
                for (int i = 0; i < this.grdMissingPolicyholder.Rows.Count; i++)
                {
                    if (this.grdMissingPolicyholder.Rows[i].Cells[5].Text.ToString().ToString().Trim().ToString().Trim() == "&nbsp;")
                    {
                        ((Label)this.grdMissingPolicyholder.Rows[i].Cells[0].FindControl("lblLocationName")).Visible = true;
                        ((LinkButton)this.grdMissingPolicyholder.Rows[i].Cells[0].FindControl("lnkSelectCase2")).Visible = false;
                        this.grdMissingPolicyholder.Rows[i].Cells[9].Visible = false;
                        this.grdMissingPolicyholder.Rows[i].Cells[4].Font.Bold = true;
                    }
                }
            }
            else
            {
                this.grdMissingPolicyholder.XGridBindSearch();
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

    private void SearchMissingReportNumber(string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
            {
                this.grdMissingReportNumber.XGridDatasetNumber=2;
                this.grdMissingReportNumber.XGridBindSearch();
                DataTable table = new DataTable();
                table = this.grdMissingReportNumber.XGridDataset;
                if (table.Rows.Count > 0)
                {
                    DataView view = new DataView(this.DisplayLocationInGrid(table));
                    this.grdMissingReportNumber.DataSource = view;
                    this.grdMissingReportNumber.DataBind();
                }
                for (int i = 0; i < this.grdMissingReportNumber.Rows.Count; i++)
                {
                    if (this.grdMissingReportNumber.Rows[i].Cells[3].Text.ToString().ToString().Trim().ToString().Trim() == "&nbsp;")
                    {
                        ((Label)this.grdMissingReportNumber.Rows[i].Cells[0].FindControl("lblLocationName")).Visible = true;
                        ((LinkButton)this.grdMissingReportNumber.Rows[i].Cells[0].FindControl("lnkSelectCase2")).Visible = false;
                        this.grdMissingReportNumber.Rows[i].Cells[7].Visible = false;
                        this.grdMissingReportNumber.Rows[i].Cells[2].Font.Bold = true;
                    }
                }
            }
            else
            {
                this.grdMissingReportNumber.XGridBindSearch();
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

    public void SearchMissingUnsentNF2()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtStatus.Text = this.ddlStatus.SelectedValue.ToString();
            this.txtLocationId.Text = this.extddlLocation.Text.ToString();
            this.grdUnsentNF2.XGridBindSearch();
            if (this.ddlStatus.SelectedValue == "0")
            {
                this.grdUnsentNF2.Columns[0].Visible = false;
            }
            else
            {
                this.grdUnsentNF2.Columns[0].Visible = true;
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

    protected void setLabels()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO dbo = new DashBoardBO();
        Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
        try
        {
            int dayOfWeek = (int)Convert.ToDateTime(DateTime.Today.ToString()).DayOfWeek;
            DateTime time = Convert.ToDateTime(DateTime.Today.ToString()).AddDays((double)-dayOfWeek);
            DateTime time2 = time.AddDays(6.0);
            this.lblAppointmentToday.Text = dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "GET_APPOINTMENT");
            this.lblAppointmentWeek.Text = dbo.getAppoinmentCount(time.ToString(), time2.ToString(), this.txtCompanyID.Text, "GET_APPOINTMENT");
            this.lblBillStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=Paid' onclick=\"javascript:OpenPage('Paid');\" > " + n_bo.GetCaseCount("SP_MST_CASE_MASTER", "GET_PAID_LIST_COUNT", this.txtCompanyID.Text) + "</a>";
            object text = this.lblBillStatus.Text;
            this.lblBillStatus.Text = string.Concat(new object[] { text, " Paid Bills  </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=UnPaid' onclick=\"javascript:OpenPage('UnPaid');\" > ", n_bo.GetCaseCount("SP_MST_CASE_MASTER", "GET_UNPAID_LIST_COUNT", this.txtCompanyID.Text), "</a> Un-Paid Bills </li></ul>" });
            this.lblDesk.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_LitigationDesk.aspx?Type=Litigation' onclick=\"javascript:OpenPage('Litigation');\" > " + n_bo.GetCaseCount("SP_LITIGATION_WRITEOFF_DESK", "GET_LETIGATION_COUNT", this.txtCompanyID.Text) + "</a> bills due for litigation";
            this.lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='Bill_Sys_MissingInformation.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
            this.lblMissingInformation.Text = this.lblMissingInformation.Text + " insurance company missing </li>  <li> <a href='Bill_Sys_MissingInformation.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "MISSING_ATTORNEY") + "</a>";
            this.lblMissingInformation.Text = this.lblMissingInformation.Text + " attorney missing </li>  <li> <a href='Bill_Sys_MissingInformation.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "MISSING_CLAIM_NUMBER") + "</a> claim number missing </li>";
            this.lblMissingInformation.Text = this.lblMissingInformation.Text + "<li> <a href='Bill_Sys_MissingInformation.aspx?Flag=MissingReportNumber' onclick=\"javascript:OpenPage('MissingReportNumber');\" > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "MISSING_REPORT_NUMBER") + "</a> report number missing </li>";
            this.lblMissingInformation.Text = this.lblMissingInformation.Text + "<li> <a href='Bill_Sys_MissingInformation.aspx?Flag=MissingPolicyHolder'> " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "MISSING_POLICY_HOLDER") + "</a> policy holder missing </li>";
            this.lblMissingInformation.Text = this.lblMissingInformation.Text + "<li> <a href='Bill_Sys_MissingInformation.aspx?Flag=UnsentNF2'> " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "UNSENTNF2") + "</a> unsent NF2 </li></ul>";
            this.lblReport.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_ReffPaidBills.aspx' onclick=\"javascript:OpenPage('Litigation');\" > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "DOCUMENT_RECEIVED_COUNT") + "</a> Received Report";
            this.lblReport.Text = this.lblReport.Text + "</li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=P' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + dbo.getAppoinmentCount(DateTime.Today.ToString(), DateTime.Today.ToString(), this.txtCompanyID.Text, "DOCUMENT_PENDING_COUNT") + "</a> Pending Report </li></ul>";
            this.lblProcedureStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li>" + dbo.getBilledUnbilledProcCode(this.txtCompanyID.Text, "GET_BILLEDPROC") + " billed procedure codes";
            this.lblProcedureStatus.Text = this.lblProcedureStatus.Text + "</li>  <li>" + dbo.getBilledUnbilledProcCode(this.txtCompanyID.Text, "GET_UNBILLEDPROC") + " Un-billed procedure codes </li></ul>";
            this.lblTotalVisit.Text = dbo.getTotalVisits(this.txtCompanyID.Text, "GET_VISIT_COUNT");
            this.lblBilledVisit.Text = dbo.getTotalVisits(this.txtCompanyID.Text, "GET_BILLED_VISIT_COUNT");
            this.lblUnBilledVisit.Text = dbo.getTotalVisits(this.txtCompanyID.Text, "GET_UNBILLED_VISIT_COUNT");
            this.lblPatientVisitStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientscheduled' onclick=\"javascript:OpenPage('PatientScheduled');\" > " + dbo.getPatientVisitStatusCount(this.txtCompanyID.Text, "GET_PATIENT_VISIT_SCHEDULED_COUNT") + "</a> ";
            this.lblPatientVisitStatus.Text = this.lblPatientVisitStatus.Text + " Patient Scheduled </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientnoshows' onclick=\"javascript:OpenPage('PatientNoShows');\" > " + dbo.getPatientVisitStatusCount(this.txtCompanyID.Text, "GET_PATIENT_VISIT_NO_SHOWS") + "</a>";
            this.lblPatientVisitStatus.Text = this.lblPatientVisitStatus.Text + " Patient No Shows </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientrescheduled' onclick=\"javascript:OpenPage('PatientRescheduled');\" > " + dbo.getPatientVisitStatusCount(this.txtCompanyID.Text, "GET_PATIENT_VISIT_RESCHEDULED") + "</a>";
            this.lblPatientVisitStatus.Text = this.lblPatientVisitStatus.Text + " Patient Rescheduled </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientvisitcompleted' onclick=\"javascript:OpenPage('PatientVisitcompleted');\" > " + dbo.getPatientVisitStatusCount(this.txtCompanyID.Text, "GET_PATIENT_VISIT_COMPLETED") + "</a>Patient Visit completed </li></ul>";
            DataTable table = new DataTable();
            try
            {
                string str = this.txtCompanyID.Text;
                table = dbo.getMissingSpecialityList(str);
                this.lblMissingSpecialityText.Text = "<table>";
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if ((i % 4) == 0)
                    {
                        if ((i != 0) && ((i % 4) == 0))
                        {
                            this.lblMissingSpecialityText.Text = this.lblMissingSpecialityText.Text + "</tr>";
                        }
                        string str2 = this.lblMissingSpecialityText.Text;
                        this.lblMissingSpecialityText.Text = str2 + "<tr><td><ul style=\"list-style-type:disc;padding-left:60px;\"><li><a href='#' onclick=\"javascript:OpenReport('" + table.Rows[i][2].ToString() + "')\">" + table.Rows[i][0].ToString() + "</a> - " + table.Rows[i][1].ToString() + "</li></ul></td>";
                    }
                    else
                    {
                        string str3 = this.lblMissingSpecialityText.Text;
                        this.lblMissingSpecialityText.Text = str3 + "<td><ul style=\"list-style-type:disc;padding-left:60px;\"><li><a href='#' onclick=\"javascript:OpenReport('" + table.Rows[i][2].ToString() + "')\">" + table.Rows[i][0].ToString() + "</a> - " + table.Rows[i][1].ToString() + "</li><ul></td>";
                    }
                }
                this.lblMissingSpecialityText.Text = this.lblMissingSpecialityText.Text + "</table>";
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
            this.ConfigDashBoard();
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

    protected void tab_changedMissingInformation(object sender, EventArgs e)
    {
        if (this.tabcontainerMissingInformation.ActiveTabIndex == 0)
        {
            if (this.grdCaseMaster.Rows.Count == 0)
            {
                this.searchcaselist("GET_MISSING_INSURANCE_LIST");
            }
            this.lblHeader.Text = "Insurance Company";
        }
        else if (this.tabcontainerMissingInformation.ActiveTabIndex == 1)
        {
            if (this.grdMissingAttorney.Rows.Count == 0)
            {
                this.searchcaselistForMissingAttorney("GET_MISSING_ATTORNEY_LIST");
            }
            this.lblHeader.Text = "Attorney";
        }
        else if (this.tabcontainerMissingInformation.ActiveTabIndex == 2)
        {
            this.grdMissingClaimNo.Columns[5].Visible = true;
            if (this.grdMissingClaimNo.Rows.Count == 0)
            {
                this.SearchMissingClaimNo("GET_MISSING_CLAIM_LIST");
            }
            this.lblHeader.Text = "Claim Number";
        }
        else if (this.tabcontainerMissingInformation.ActiveTabIndex == 3)
        {
            if (this.grdMissingReportNumber.Rows.Count == 0)
            {
                this.SearchMissingReportNumber("GET_MISSING_REPORT_LIST");
            }
            this.lblHeader.Text = "Report Number";
        }
        else if (this.tabcontainerMissingInformation.ActiveTabIndex == 4)
        {
            if (this.grdMissingPolicyholder.Rows.Count == 0)
            {
                this.SearchMissingPolicyHolder("GET_MISSING_POCLICY_HOLDER");
            }
            this.lblHeader.Text = "Policy Holder";
        }
        else if (this.tabcontainerMissingInformation.ActiveTabIndex == 5)
        {
            if (this.grdUnsentNF2.Rows.Count == 0)
            {
                this.SearchMissingUnsentNF2();
            }
            this.lblHeader.Text = "Unsent NF2";
        }
    }

}
