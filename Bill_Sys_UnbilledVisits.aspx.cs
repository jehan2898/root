using AjaxControlToolkit;
using ASP;
using ExtendedDropDownList;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Bill_Sys_UnbilledVisits : Page, IRequiresSessionState
{

    private Bill_Sys_ReportBO _reportBO;

    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;

    private ArrayList objAL;


    public Bill_Sys_UnbilledVisits()
    {
    }

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
        this.objAL = new ArrayList();
        try
        {
            this.objAL.Add(this.txtCompanyID.Text);
            this.objAL.Add(this.extddlDoctor.Text);
            this.objAL.Add(this.txtFromDate.Text);
            this.objAL.Add(this.txtToDate.Text);
            this.objAL.Add("");
            this.objAL.Add("");
            this.objAL.Add(this.extddlSpeciality.Text);
            if (this.txtSearchOrder.Text == "")
            {
                this.objAL.Add(this.txtSearchOrder.Text);
            }
            else
            {
                this.objAL.Add(this.txtSearchOrder.Text);
            }
            this.objAL.Add(this.extddlLocation.Text);
            this.objAL.Add(this.btnRadio_Group.SelectedValue);
            this.objAL.Add(this.extddlCaseType.Text);
            DataSet dataSet = new DataSet();
            dataSet = this._bill_Sys_Visit_BO.UnBilledVisitReport(this.objAL);
            Label str = this.lblCount;
            int count = dataSet.Tables[0].Rows.Count;
            str.Text = count.ToString();
            this.grdAllReports.DataSource = dataSet.Tables[0];
            this.grdAllReports.DataBind();
            Label label = this.lblCount;
            int num = dataSet.Tables[0].Rows.Count;
            label.Text = num.ToString();
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

    protected void BindGrid_DisplayDiagonosisCode(ArrayList p_objAL)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._reportBO = new Bill_Sys_ReportBO();
        try
        {
            this.grdDisplayDiagonosisCode.DataSource = this._reportBO.getAssociatedDiagnosisCode(p_objAL);
            this.grdDisplayDiagonosisCode.DataBind();
            ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", "showDisplayDiagnosisCodePopup();", true);
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

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ExportToExcelFixColumnName();
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

    protected void btnlClosePopUP_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", "closePopup();", true);
        this.BindGrid();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.BindGrid();
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

    private void ExportToExcel()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<table border='1px'>");
            for (int i = 0; i < this.grdAllReports.Items.Count; i++)
            {
                if (i == 0)
                {
                    stringBuilder.Append("<tr>");
                    for (int j = 0; j < this.grdAllReports.Columns.Count; j++)
                    {
                        if (this.grdAllReports.Columns[j].Visible)
                        {
                            stringBuilder.Append("<td>");
                            stringBuilder.Append(this.grdAllReports.Columns[j].HeaderText);
                            stringBuilder.Append("</td>");
                        }
                    }
                    stringBuilder.Append("</tr>");
                }
                stringBuilder.Append("<tr>");
                for (int k = 0; k < this.grdAllReports.Columns.Count; k++)
                {
                    if (this.grdAllReports.Columns[k].Visible)
                    {
                        stringBuilder.Append("<td>");
                        stringBuilder.Append(this.grdAllReports.Items[i].Cells[k].Text);
                        stringBuilder.Append("</td>");
                    }
                }
                stringBuilder.Append("</tr>");
            }
            stringBuilder.Append("</table>");
            string str = string.Concat(this.getFileName("EXCEL"), ".xls");
            StreamWriter streamWriter = new StreamWriter(string.Concat(ConfigurationManager.AppSettings["EXCEL_SHEET"], str));
            streamWriter.Write(stringBuilder);
            streamWriter.Close();
            base.Response.Redirect(string.Concat(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"], str), false);
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

    private void ExportToExcelFixColumnName()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<table border='1px'>");
            stringBuilder.Append("<tr>");
            stringBuilder.Append("<td>Case #</td>");
            stringBuilder.Append("<td>Patient Name</td>");
            stringBuilder.Append("<td>Date Of Visit</td>");
            stringBuilder.Append("<td>Visit Type</td>");
            stringBuilder.Append("<td>Doctor Name</td>");
            stringBuilder.Append("<td>Case Type</td>");
            stringBuilder.Append("<td>Speciality</td>");
            stringBuilder.Append("<td>No Of Days</td>");
            stringBuilder.Append("<td>Insurance Company</td>");
            stringBuilder.Append("<td>Claim Number</td>");
            stringBuilder.Append("<td>Diagnosis Code</td>");
            stringBuilder.Append("</tr>");
            for (int i = 0; i < this.grdAllReports.Items.Count; i++)
            {
                stringBuilder.Append("<tr><td>");
                stringBuilder.Append(this.grdAllReports.Items[i].Cells[10].Text);
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td>");
                stringBuilder.Append(this.grdAllReports.Items[i].Cells[9].Text);
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td>");
                stringBuilder.Append(this.grdAllReports.Items[i].Cells[18].Text);
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td>");
                stringBuilder.Append(this.grdAllReports.Items[i].Cells[5].Text);
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td>");
                stringBuilder.Append(this.grdAllReports.Items[i].Cells[19].Text);
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td>");
                stringBuilder.Append(this.grdAllReports.Items[i].Cells[7].Text);
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td>");
                stringBuilder.Append(this.grdAllReports.Items[i].Cells[20].Text);
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td>");
                stringBuilder.Append(this.grdAllReports.Items[i].Cells[12].Text);
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td>");
                stringBuilder.Append(this.grdAllReports.Items[i].Cells[13].Text);
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td>");
                stringBuilder.Append(this.grdAllReports.Items[i].Cells[14].Text);
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td>");
                stringBuilder.Append(this.grdAllReports.Items[i].Cells[21].Text);
                stringBuilder.Append("</td></tr>");
            }
            stringBuilder.Append("</table>");
            string str = string.Concat(this.getFileName("EXCEL"), ".xls");
            StreamWriter streamWriter = new StreamWriter(string.Concat(ConfigurationManager.AppSettings["EXCEL_SHEET"], str));
            streamWriter.Write(stringBuilder);
            streamWriter.Close();
            base.Response.Redirect(string.Concat(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"], str), false);
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

    private string getFileName(string p_szBillNumber)
    {
        DateTime now = DateTime.Now;
        string[] pSzBillNumber = new string[] { p_szBillNumber, "_", this.getRandomNumber(), "_", now.ToString("yyyyMMddHHmmssms") };
        return string.Concat(pSzBillNumber);
    }

    private string getRandomNumber()
    {
        return (new Random()).Next(1, 10000).ToString();
    }

    protected void grdAllReports_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "CaseSearch")
            {
                if (this.txtSearchOrder.Text != string.Concat(e.CommandArgument, " ASC"))
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " ASC");
                }
                else
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " DESC");
                }
                this.BindGrid();
            }
            if (e.CommandName.ToString() == "PatientNameSearch")
            {
                if (this.txtSearchOrder.Text != string.Concat(e.CommandArgument, " ASC"))
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " ASC");
                }
                else
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " DESC");
                }
                this.BindGrid();
            }
            if (e.CommandName.ToString() == "EventDateSearch")
            {
                if (this.txtSearchOrder.Text != string.Concat(e.CommandArgument, " DESC"))
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " DESC");
                }
                else
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " ASC");
                }
                this.BindGrid();
            }
            if (e.CommandName.ToString() == "DoctorNameSearch")
            {
                if (this.txtSearchOrder.Text != string.Concat(e.CommandArgument, " ASC"))
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " ASC");
                }
                else
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " DESC");
                }
                this.BindGrid();
            }
            if (e.CommandName.ToString() == "SpecialitySearch")
            {
                if (this.txtSearchOrder.Text != string.Concat(e.CommandArgument, " ASC"))
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " ASC");
                }
                else
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " DESC");
                }
                this.BindGrid();
            }
            if (e.CommandName.ToString() == "CaseID")
            {
                this.Session["CASE_OBJECT"] = "";
                Bill_Sys_Case billSysCase = new Bill_Sys_Case()
                {
                    SZ_CASE_ID = e.Item.Cells[0].Text.ToString()
                };
                CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
                //Bill_Sys_CaseObject billSysCaseObject = new Bill_Sys_CaseObject();
               // {
                Bill_Sys_CaseObject billSysCaseObject = new Bill_Sys_CaseObject();
                billSysCaseObject.SZ_PATIENT_ID = e.Item.Cells[11].Text.ToString();
                billSysCaseObject.SZ_CASE_ID = e.Item.Cells[0].Text.ToString();
                billSysCaseObject.SZ_COMAPNY_ID = caseDetailsBO.GetPatientCompanyID(billSysCaseObject.SZ_PATIENT_ID);
                billSysCaseObject.SZ_PATIENT_NAME = caseDetailsBO.GetPatientName(billSysCaseObject.SZ_PATIENT_ID);
                billSysCaseObject.SZ_CASE_NO = e.Item.Cells[10].Text.ToString();
                //};
                this.Session["CASE_OBJECT"] = billSysCaseObject;
                this.Session["CASEINFO"] = billSysCase;
                this.Session["PassedCaseID"] = e.Item.Cells[0].Text.ToString();
                string str = e.Item.Cells[0].Text.ToString();
                this.Session["QStrCaseID"] = str;
                this.Session["Case_ID"] = str;
                this.Session["Archived"] = "0";
                this.Session["QStrCID"] = str;
                this.Session["SelectedID"] = str;
                this.Session["DM_User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["SN"] = "0";
                this.Session["LastAction"] = "vb_CaseInformation.aspx";
                base.Response.Redirect("AJAX%20Pages/Bill_Sys_CaseDetails.aspx", false);
            }
            if (e.CommandName == "Group Service")
            {
                Bill_Sys_Case billSysCase1 = new Bill_Sys_Case()
                {
                    SZ_CASE_ID = e.CommandArgument.ToString()
                };
                CaseDetailsBO caseDetailsBO1 = new CaseDetailsBO();
                Bill_Sys_CaseObject billSysCaseObject1 = new Bill_Sys_CaseObject();
                // {
                billSysCaseObject1.SZ_PATIENT_ID = e.Item.Cells[11].Text.ToString();
                billSysCaseObject1.SZ_CASE_ID = e.CommandArgument.ToString();
                billSysCaseObject1.SZ_COMAPNY_ID = caseDetailsBO1.GetPatientCompanyID(billSysCaseObject1.SZ_PATIENT_ID);
                billSysCaseObject1.SZ_PATIENT_NAME = caseDetailsBO1.GetPatientName(billSysCaseObject1.SZ_PATIENT_ID);
                billSysCaseObject1.SZ_CASE_NO = e.Item.Cells[10].Text.ToString();
                // };
                this.Session["CASE_OBJECT"] = billSysCaseObject1;
                this.Session["CASEINFO"] = billSysCase1;
                this.Session["PassedCaseID"] = e.CommandArgument.ToString();
                string str1 = e.CommandArgument.ToString();
                this.Session["QStrCaseID"] = str1;
                this.Session["Case_ID"] = str1;
                this.Session["Archived"] = "0";
                this.Session["QStrCID"] = str1;
                this.Session["SelectedID"] = str1;
                this.Session["DM_User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["SN"] = "0";
                this.Session["LastAction"] = "vb_CaseInformation.aspx";
                Type type = base.GetType();
                string[] strArrays = new string[] { "setDiv('", e.Item.Cells[1].Text.ToString(), "',EID='", e.Item.Cells[17].Text.ToString(), "');" };
                ScriptManager.RegisterStartupScript(this, type, "starScript", string.Concat(strArrays), true);
                this.BindGrid();
            }
            if (e.CommandName == "Document Manager")
            {
                Bill_Sys_Case billSysCase2 = new Bill_Sys_Case()
                {
                    SZ_CASE_ID = e.CommandArgument.ToString()
                };
                CaseDetailsBO caseDetailsBO2 = new CaseDetailsBO();
                Bill_Sys_CaseObject billSysCaseObject2 = new Bill_Sys_CaseObject();
                //{
                billSysCaseObject2.SZ_PATIENT_ID = e.Item.Cells[11].Text.ToString();
                billSysCaseObject2.SZ_CASE_ID = e.CommandArgument.ToString();
                billSysCaseObject2.SZ_COMAPNY_ID = caseDetailsBO2.GetPatientCompanyID(billSysCaseObject2.SZ_PATIENT_ID);
                billSysCaseObject2.SZ_PATIENT_NAME = caseDetailsBO2.GetPatientName(billSysCaseObject2.SZ_PATIENT_ID);
                billSysCaseObject2.SZ_CASE_NO = e.Item.Cells[10].Text.ToString();
                //};
                this.Session["CASE_OBJECT"] = billSysCaseObject2;
                this.Session["CASEINFO"] = billSysCase2;
                this.Session["PassedCaseID"] = e.CommandArgument.ToString();
                string str2 = "";
                string str3 = e.CommandArgument.ToString();
                this.Session["QStrCaseID"] = str3;
                this.Session["Case_ID"] = str3;
                this.Session["Archived"] = "0";
                this.Session["QStrCID"] = str3;
                this.Session["SelectedID"] = str3;
                this.Session["DM_User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["SN"] = "0";
                this.Session["LastAction"] = "vb_CaseInformation.aspx";
                str2 = "Document Manager/case/vb_CaseInformation.aspx";
                base.Response.Write(string.Concat("<script language='javascript'>window.open('", str2, "', 'AdditionalData');</script>"));
            }
            if (e.CommandName == "Display Diag Code")
            {
                this.objAL = new ArrayList();
                this.objAL.Add(e.Item.Cells[0].Text.ToString());
                this.objAL.Add(e.Item.Cells[1].Text.ToString());
                this.objAL.Add("");
                this.objAL.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.BindGrid_DisplayDiagonosisCode(this.objAL);
                this.Session["DIAGINFO"] = this.objAL;
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

    protected void grdDisplayDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdDisplayDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            this.objAL = new ArrayList();
            this.objAL = (ArrayList)this.Session["DIAGINFO"];
            this.BindGrid_DisplayDiagonosisCode(this.objAL);
            ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", "showDisplayDiagnosisCodePopup();", true);
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

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!base.IsPostBack)
            {
                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this._reportBO = new Bill_Sys_ReportBO();
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
       
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_Sys_UnbilledVisits.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}