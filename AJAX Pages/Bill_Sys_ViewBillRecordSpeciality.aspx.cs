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
using System.Text;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using log4net;
using RequiredDocuments;
using mbs.lawfirm;
using XMLData;
using System.Data.OleDb;
using mbs.ApplicationSettings;
using CUTEFORMCOLib;
using PDFValueReplacement;
using SautinSoft;
using URLIntegrationSecurity;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Hangfire;

public partial class AJAX_Pages_Bill_Sys_ViewBillRecordSpeciality : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    Bill_Sys_NF3_Template _nf3Template;
    Bill_Sys_BillTransaction_BO _billTransactionBO;
    Bill_Sys_LoginBO objLoginBO = new Bill_Sys_LoginBO();
    private mbs.ApplicationSettings.ApplicationSettings_BO objApplicationBO;

    private Bill_Sys_UserObject oC_UserObject = null;
    private Bill_Sys_BillingCompanyObject oC_Account = null;

    private static ILog log = LogManager.GetLogger("Bill_Sys_ViewBillRecordSpeciality");


    protected void BindGrid()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        new ArrayList();
        try
        {
            if (this.Session["sz_StatusID"] != null)
            {
                this.txtBillStatusId.Text = this.Session["sz_StatusID"].ToString();
            }
            else
            {
                this.Session["sz_StatusID"] = "";
                this.txtBillStatusId.Text = "";
            }
            if (this.Session["sz_SpecialityID"] != null)
            {
                this.txtProcedureGroupId.Text = this.Session["sz_SpecialityID"].ToString();
            }
            else
            {
                this.Session["sz_SpecialityID"] = "";
                this.txtProcedureGroupId.Text = "";
            }
            if (this.Session["sz_LOCATION_Id"] != null)
            {
                this.txtLoactionId.Text = this.Session["sz_LOCATION_Id"].ToString();
            }
            else
            {
                this.txtLoactionId.Text = "";
                this.Session["sz_LOCATION_Id"] = "";
            }
            if (this.Session["User_ID"] != null)
            {
                this.txtUserID.Text = this.Session["User_ID"].ToString();
            }
            else
            {
                this.Session["User_ID"] = "";
                this.txtUserID.Text = "";
            }
            if (this.Session["sz_FromDate"] != null)
            {
                this.txtFromDate.Text = this.Session["sz_FromDate"].ToString();
            }
            else
            {
                this.Session["sz_FromDate"] = "";
                this.txtFromDate.Text = "";
            }
            if (this.Session["sz_Todate"] != null)
            {
                this.txtTodate.Text = this.Session["sz_Todate"].ToString();
            }
            else
            {
                this.txtTodate.Text = "";
                this.Session["sz_Todate"] = "";
            }
            if (this.Session["sz_visitFromDate"] != null)
            {
                this.txtFirstVisitDate.Text = this.Session["sz_visitFromDate"].ToString();
            }
            else
            {
                this.Session["sz_visitFromDate"] = "";
                this.txtFirstVisitDate.Text = "";
            }
            if (this.Session["sz_visitTodate"] != null)
            {
                this.txtlastVisitDate.Text = this.Session["sz_visitTodate"].ToString();
            }
            else
            {
                this.txtlastVisitDate.Text = "";
                this.Session["sz_visitTodate"] = "";
            }
            if (base.Request.QueryString["CaseType"] != null)
            {
                this.txtCaseTypeID.Text = base.Request.QueryString["CaseType"].ToString();
            }
            else
            {
                this.txtCaseTypeID.Text = "";
            }
            if (this.Session["CaseNumber"] != null)
            {
                this.txtCaseNumber.Text = this.Session["CaseNumber"].ToString();
            }
            else
            {
                this.Session["CaseNumber"] = "";
                this.txtCaseNumber.Text = "";
            }
            if (this.Session["BillNumber"] != null)
            {
                this.txtBillNumber.Text = this.Session["BillNumber"].ToString();
            }
            else
            {
                this.Session["BillNumber"] = "";
                this.txtBillNumber.Text = "";
            }
            if (this.Session["PatientName"] != null)
            {
                this.txtPatientName.Text = this.Session["PatientName"].ToString();
            }
            else
            {
                this.Session["PatientName"] = "";
                this.txtPatientName.Text = "";
            }
            if (this.Session["SZ_INSURANCE_ID"] != null)
            {
                this.txtInsuranceId.Text = this.Session["SZ_INSURANCE_ID"].ToString();
            }
            else
            {
                this.txtInsuranceId.Text = "";
                this.Session["SZ_INSURANCE_ID"] = "";
            }
            this.grdBillReportDetails.XGridBind();
            this.grdBillReportDetails.XGridBindSearch();
            DataSet dataSet = new DataSet();
            for (int i = 0; i < this.grdBillReportDetails.Rows.Count; i++)
            {
                string str = this.grdBillReportDetails.DataKeys[i][0].ToString();
                SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
                SqlCommand selectCommand = new SqlCommand("sp_get_status_code_for_bill", connection);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.CommandTimeout = 0;
                selectCommand.Parameters.AddWithValue("@sz_bill_number", str);
                new SqlDataAdapter(selectCommand).Fill(dataSet);
            }
            if (dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count != 0)
                {
                    for (int j = 0; j < this.grdBillReportDetails.Rows.Count; j++)
                    {
                        CheckBox box = (CheckBox)this.grdBillReportDetails.Rows[j].FindControl("chkUpdateStatus");
                        if ((dataSet.Tables[0].Rows[j][0].ToString() == "TRF") || (dataSet.Tables[0].Rows[j][0].ToString() == "DNL"))
                        {
                            box.Enabled = false;
                        }
                        else
                        {
                            box.Enabled = true;
                        }
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
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        ViewState["update"] = Session["update"];
    }
    private void SaveActivityLog(string Title, string desc)
    {
        DAO_NOTES_EO _DAO_NOTES_EO = null;
        DAO_NOTES_BO _DAO_NOTES_BO = null;
        Bill_Sys_BillingCompanyDetails_BO billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
        _DAO_NOTES_EO = new DAO_NOTES_EO();
        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = Title;
        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = desc;
        _DAO_NOTES_BO = new DAO_NOTES_BO();
        _DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
        // _DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);



    }
    protected void btnCreatePacket_Click(object sender, EventArgs e)
    {
        // If page not Refreshed
        if (Session["update"].ToString() == ViewState["update"].ToString())
        {
            //Logging Start
            string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
            using (Utils utility = new Utils())
            {
                utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            try
            {
                ArrayList list = new ArrayList();
                string str = "";

                foreach (GridViewRow item in this.grdBillReportDetails.Rows)
                {
                    CheckBox box = (CheckBox)item.Cells[2].FindControl("chkUpdateStatus");
                    if (box.Checked)
                    {
                        Bill_Sys_Bill_Packet_Request request = new Bill_Sys_Bill_Packet_Request();
                        request.SZ_CASE_ID = this.grdBillReportDetails.DataKeys[item.RowIndex]["SZ_CASE_ID"].ToString();
                        request.SZ_BILL_NUMBER = this.grdBillReportDetails.DataKeys[item.RowIndex]["SZ_BILL_ID"].ToString();
                        list.Add(request);
                        grdBillReportDetails.Rows[item.RowIndex].Visible = false;
                    }
                }

                Bill_Sys_Upload_VisitReport report = new Bill_Sys_Upload_VisitReport();

                str = report.GetBillPacketRequest(oC_UserObject.SZ_USER_ID, oC_Account.SZ_COMPANY_ID, list);

                if (str != "")
                {
                    report.BillStatusUpdatePNG(str, "ERROR");
                    var jobid = BackgroundJob.Enqueue(() => report.CreatePacket(this.txtCompanyId.Text, list, this.txtProcedureGroupId.Text, str));
                    report.UpdateBillPacket(str, Convert.ToInt32(jobid), this.txtProcedureGroupId.Text);

                    string Message = "Packeting scheduled for Job ID: " + jobid + ".Please visit again after some time.";
                    SaveActivityLog("PACKET_SCHEDULED", Message);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowMessage('" + Message + "','Packet submitted','success');", true); this.BindGrid();

                    //string str2 = report.CreatePacket(this.txtCompanyId.Text, list, this.txtProcedureGroupId.Text);
                    //if (str2.Contains("ERROR"))
                    //{
                    //    string[] strArray = str2.Split(new char[] { ',' });
                    //    this.usrMessage.PutMessage("Document are not found for bill number" + strArray[1].ToString());
                    //    this.usrMessage.SetMessageType(0);
                    //    this.usrMessage.Show();
                    //}
                    //else
                    //{

                    //    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + ConfigurationSettings.AppSettings["PACKET_DOC_URL"].ToString() + str2 + "'); ", true);
                    //    this.BindGrid();
                    //}
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
    }

    protected void btnExportData_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string billNO = "";
            for (int i = 0; i < this.grdBillReportDetails.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdBillReportDetails.Rows[i].FindControl("chkUpdateStatus");
                if (box.Checked)
                {
                    if (billNO == "")
                    {
                        billNO = "'" + this.grdBillReportDetails.DataKeys[i]["SZ_BILL_ID"].ToString() + "'";
                    }
                    else
                    {
                        billNO = billNO = billNO + ",'" + this.grdBillReportDetails.DataKeys[i]["SZ_BILL_ID"].ToString() + "'";
                    }
                }
            }
            Bill_Sys_CollectDocAndZip zip = new Bill_Sys_CollectDocAndZip();
            DataSet pateintInfo = new DataSet();
            pateintInfo = this.GetPateintInfo(billNO);
            if (pateintInfo.Tables[0].Rows.Count > 0)
            {
                string str2 = zip.getFileName().Replace(".xml", ".xls");
                string sourceFileName = ConfigurationSettings.AppSettings["XLPATH"].ToString();
                string str4 = ConfigurationSettings.AppSettings["REPORTFOLDER"].ToString();
                log.Debug(" temp xl path" + sourceFileName);
                log.Debug("Path:" + this.getPhysicalPath() + "Reports/" + str2);
                try
                {
                    if (!Directory.Exists(this.getPhysicalPath() + "Reports/"))
                    {
                        Directory.CreateDirectory(this.getPhysicalPath() + "Reports/");
                    }
                    File.Copy(sourceFileName, this.getPhysicalPath() + "Reports/" + str2);
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
                this.GenerateXL(pateintInfo.Tables[0], this.getPhysicalPath() + "Reports/" + str2);
                if (File.Exists(this.getPhysicalPath() + str4 + str2))
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + str2 + "'); ", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "alert('Batch is Empty');", true);
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

    protected void btnNo_Click(object sender, EventArgs e)
    {
        this.hdnPOMValue.Value = "No";
        this.btnNo.Attributes.Add("onclick", "NoMassage");
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_NEW_POM == "1")
        {
            this.creatNewPOMPDF();
        }
        else
        {
            this.creatPDF();
        }
    }

    protected void btnPrintEnvelop_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
        try
        {
            string str = ConfigurationManager.AppSettings["NF2_ENVELOPE_PDF_FILE"];
            string str2 = ConfigurationManager.AppSettings["NF3_ENVELOPE_XML_FILE"];
            string str3 = ConfigurationManager.AppSettings["NF3_ENVELOPE_XML_WC_FILE"];
            string str4 = template.getPhysicalPath();
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            string str9 = "";
            string str10 = "";
            string str11 = "";
            string str12 = "";
            string str13 = "";
            string str14 = "";
            string str15 = "";
            string str16 = "";
            //Change added on 19/06/2015 to print the single page envelope for WC
            DataSet set2 = new DataSet();
            this.txtCompanyId.Text = oC_Account.SZ_COMPANY_ID;
            set2 = new Bill_Sys_ProcedureCode_BO().Get_Sys_Key("SS00075", this.txtCompanyId.Text);
            string bt_get_wc = "";
            if (set2.Tables.Count > 0)
            {
                if (set2.Tables[0].Rows.Count > 0)
                {
                    bt_get_wc = set2.Tables[0].Rows[0][0].ToString();
                }
            }
            //Change added on 19/06/2015 to print the single page envelope for WC
            for (int i = 0; i < this.grdBillReportDetails.Rows.Count; i++)
            {
                string str17 = this.grdBillReportDetails.DataKeys[i][1].ToString();
                string str18 = this.grdBillReportDetails.DataKeys[i][0].ToString();
                CheckBox box = (CheckBox)this.grdBillReportDetails.Rows[i].FindControl("chkUpdateStatus");
                if (box.Checked)
                {
                    string str19 = this.grdBillReportDetails.DataKeys[i][3].ToString();
                    string str20 = this.grdBillReportDetails.DataKeys[i][1].ToString();

                    if (str19 == "WC000000000000000001" && bt_get_wc != "1")
                    {
                        PDFValueReplacement.PDFValueReplacement replacement = new PDFValueReplacement.PDFValueReplacement();
                        string str21 = replacement.ReplacePDFvalues(str3, str, str18, oC_Account.SZ_COMPANY_NAME, str20);
                        if (str10 == "")
                        {
                            str15 = oC_Account.SZ_COMPANY_NAME + "/" + str20 + "/Packet Document/";
                            str10 = str21;
                            str11 = str4 + str15 + str10;
                            str14 = str15 + str5;
                            str12 = replacement.ReplacePDFvalues(str2, str, str18, oC_Account.SZ_COMPANY_NAME, str20);
                            str13 = str4 + str15 + str12;
                            MergePDF.MergePDFFiles(str11, str13, str4 + str15 + "_" + str10);
                            str10 = "_" + str10;
                            str11 = str4 + str15 + str10;
                            str14 = str15 + str10;
                        }
                        else
                        {
                            string str22 = oC_Account.SZ_COMPANY_NAME + "/" + str20 + "/Packet Document/";
                            str12 = str21;
                            str13 = str4 + str22 + str12;
                            MergePDF.MergePDFFiles(str11, str13, str4 + str15 + str10);
                            str12 = replacement.ReplacePDFvalues(str2, str, str18, oC_Account.SZ_COMPANY_NAME, str20);
                            str13 = str4 + str22 + str12;
                            MergePDF.MergePDFFiles(str11, str13, str4 + str15 + str10);
                            str11 = str4 + str15 + str10;
                            str14 = str15 + str10;
                        }
                    }
                    else
                    {
                        string str23 = new PDFValueReplacement.PDFValueReplacement().ReplacePDFvalues(str2, str, str18, oC_Account.SZ_COMPANY_NAME, str17);
                        if (str5 == "")
                        {
                            str16 = oC_Account.SZ_COMPANY_NAME + "/" + str17 + "/Packet Document/";
                            str5 = str23;
                            str6 = str4 + str16 + str5;
                            str5 = str5;
                            str9 = str16 + str5;
                        }
                        else
                        {
                            string str24 = oC_Account.SZ_COMPANY_NAME + "/" + str17 + "/Packet Document/";
                            str7 = str23;
                            str8 = str4 + str24 + str7;
                            MergePDF.MergePDFFiles(str6, str8, str4 + str16 + "_" + str5);
                            str5 = "_" + str5;
                            str6 = str4 + str16 + str5;
                            str9 = str16 + str5;
                        }
                    }
                }
            }
            if ((str14 != "") && (str9 != ""))
            {
                MergePDF.MergePDFFiles(str4 + str14, str4 + str9, str4 + str14);
                str9 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str14;
            }
            else if (str9 != "")
            {
                str9 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str9;
            }
            else if (str14 != "")
            {
                str9 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str14;
            }
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + str9.ToString() + "'); ", true);
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

    protected void btnPrintPOM_Click(object sender, EventArgs e)
    {
    }
    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;
    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.extddlBillStatus.Text != "NA")
            {
                this._reportBO = new Bill_Sys_ReportBO();
                ArrayList list = new ArrayList();
                string str = "";
                bool flag = false;
                for (int i = 0; i < this.grdBillReportDetails.Rows.Count; i++)
                {
                    CheckBox box = (CheckBox)this.grdBillReportDetails.Rows[i].FindControl("chkUpdateStatus");
                    if (box.Checked)
                    {
                        if (!flag)
                        {
                            str = "'" + this.grdBillReportDetails.DataKeys[i][0].ToString() + "'";
                            flag = true;
                        }
                        else
                        {
                            str = str + ",'" + this.grdBillReportDetails.DataKeys[i][0].ToString() + "'";
                        }
                    }
                }
                if (str != "")
                {
                    list.Add(this.extddlBillStatus.Text);
                    list.Add(str);
                    list.Add(oC_Account.SZ_COMPANY_ID);
                    list.Add(this.txtBillStatusdate.Text);
                    list.Add(oC_UserObject.SZ_USER_ID);
                    this._reportBO.updateBillStatus(list);
                    SaveActivityLog("BILL_STATUS_UPDATED", "Bills  : " + str + " updated to :" + extddlBillStatus.Selected_Text);
                    this.usrMessage.PutMessage("Bill status updated successfully.");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                    this.BindGrid();
                }
                else
                {
                    this.usrMessage.PutMessage("Can not update bill status for" + str);
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    this.usrMessage.Show();
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

    protected void btnYes_Click(object sender, EventArgs e)
    {
        this.hdnPOMValue.Value = "Yes";
        this.btnYes.Attributes.Add("onclick", "YesMassage");
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_NEW_POM == "1")
        {
            this.creatNewPOMPDF();
        }
        else
        {
            this.creatPDF();
        }
    }

    private DataSet CreateGroupData()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet set = new DataSet();
        try
        {
            this._reportBO = new Bill_Sys_ReportBO();
            string sysSettingForSort = this._reportBO.GetSysSettingForSort(this.txtCompanyId.Text.ToString());
            DataSet set2 = new DataSet();
            DataTable table = new DataTable();
            table.Columns.Add("Bill ID");
            table.Columns.Add("Case #");
            table.Columns.Add("Patient Name");
            table.Columns.Add("Spciality");
            table.Columns.Add("Provider");
            table.Columns.Add("Bill Amount");
            table.Columns.Add("Visit Date");
            table.Columns.Add("Bill Date");
            table.Columns.Add("Bill Staus Date");
            table.Columns.Add("Status");
            table.Columns.Add("Username");
            table.Columns.Add("Claim Number");
            table.Columns.Add("Insurance Company");
            table.Columns.Add("Insurance Address");
            table.Columns.Add("Min Date Of Service");
            table.Columns.Add("Max Date Of Service");
            table.Columns.Add("CaseType");
            table.Columns.Add("WC_ADDRESS");
            table.Columns.Add("Case Id");
            table.Columns.Add("SpecialityId");
            table.Columns.Add("ProviderId");
            table.Columns.Add("ID", typeof(int));
            for (int i = 0; i < this.grdBillReportDetails.Rows.Count; i++)
            {
                if (((CheckBox)this.grdBillReportDetails.Rows[i].FindControl("chkUpdateStatus")).Checked)
                {
                    DataRow row = table.NewRow();
                    row["Bill ID"] = this.grdBillReportDetails.DataKeys[i][0].ToString();
                    row["Case #"] = this.grdBillReportDetails.DataKeys[i][2].ToString();
                    row["Patient Name"] = this.grdBillReportDetails.Rows[i].Cells[6].Text.ToString().Replace(" ", ",").Trim();
                    row["Spciality"] = this.grdBillReportDetails.Rows[i].Cells[7].Text;
                    row["Provider"] = this.grdBillReportDetails.Rows[i].Cells[8].Text;
                    row["Bill Amount"] = this.grdBillReportDetails.Rows[i].Cells[9].Text.ToString().Replace("$", "").Trim();
                    row["Visit Date"] = this.grdBillReportDetails.Rows[i].Cells[10].Text;
                    row["Bill Date"] = this.grdBillReportDetails.Rows[i].Cells[11].Text;
                    row["Bill Staus Date"] = this.grdBillReportDetails.Rows[i].Cells[0x10].Text;
                    row["Status"] = this.grdBillReportDetails.Rows[i].Cells[0x11].Text;
                    row["Username"] = this.grdBillReportDetails.Rows[i].Cells[0x12].Text;
                    row["Claim Number"] = this.grdBillReportDetails.DataKeys[i][4].ToString();
                    row["Insurance Company"] = this.grdBillReportDetails.DataKeys[i][5].ToString();
                    row["Insurance Address"] = this.grdBillReportDetails.DataKeys[i][6].ToString();
                    row["Min Date Of Service"] = this.grdBillReportDetails.DataKeys[i][7].ToString();
                    row["Max Date Of Service"] = this.grdBillReportDetails.DataKeys[i][8].ToString();
                    row["CaseType"] = this.grdBillReportDetails.DataKeys[i][3].ToString();
                    row["WC_ADDRESS"] = this.grdBillReportDetails.Rows[i].Cells[30].Text;
                    row["Case Id"] = this.grdBillReportDetails.DataKeys[i][1].ToString();
                    row["SpecialityId"] = this.grdBillReportDetails.Rows[i].Cells[0x1f].Text;
                    row["ProviderId"] = this.grdBillReportDetails.Rows[i].Cells[32].Text;
                    int num2 = Convert.ToInt32(this.grdBillReportDetails.DataKeys[i][0].ToString().Substring(2));
                    row["ID"] = num2;
                    table.Rows.Add(row);
                }
            }
            if (table.Rows.Count > 0)
            {
                DataView defaultView = table.DefaultView;
                if (sysSettingForSort == "1")
                {
                    defaultView.Sort = "ProviderId,ID";
                }
                else
                {
                    defaultView.Sort = "ProviderId";
                }
                table = defaultView.ToTable();
            }
            set2.Tables.Add(table);
            set = set2;
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
        return set;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void creatNewPOMOtherPDF()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = "";
        string str2 = "";
        string str3 = "";
        string szInput = "";
        string str5 = "";
        int num = 0;
        int num2 = 0;
        int num3 = 1;
        string str6 = "";
        string str7 = "";
        int num4 = 0;
        string path = "";
        Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
        StringBuilder builder = new StringBuilder();
        if (this.hdnPOMValue.Value.Equals("Yes"))
        {
            this._reportBO = new Bill_Sys_ReportBO();
            num4 = this._reportBO.POMSaveOther(oC_Account.SZ_COMPANY_ID, oC_UserObject.SZ_USER_ID, 0);
        }
        builder.AppendLine("");
        int num5 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        int num6 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        try
        {
            template.getPhysicalPath();
            string str9 = ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET");
            int num7 = 0;
            DataSet set = this.CreateGroupData();
            int num8 = 0;
            int num9 = 0;
            string str10 = null;
            string str11 = null;
            bool flag = false;
            szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:10px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
            str5 = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:10px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
            int num10 = 0;
            int num11 = 0;
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                str11 = set.Tables[0].Rows[i]["CaseType"].ToString();
                if (str11 == "WC000000000000000001")
                {
                    builder.AppendLine("");
                    if (num7 == 0)
                    {
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        num7 = 1;
                        str6 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        num9 = 0;
                    }
                    if (str10.Equals(set.Tables[0].Rows[i]["Provider"].ToString()))
                    {
                        num++;
                        num2++;
                        if (num9 >= num6)
                        {
                            builder.Append(this.ReplaceHeaderAndFooter(str5, str6, num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num9 = 1;
                            num10 = 1;
                            num2 = 1;
                        }
                        if (flag)
                        {
                            str5 = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            flag = false;
                        }
                        object obj2 = szInput;
                        szInput = string.Concat(new object[] {
                            obj2, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "<br />", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Case #"], "<br /></td><td style='font-size:11px; width:50px;' align='left'> ",
                            set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "-", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        if (num11 >= num5)
                        {
                            num8 = num11;
                            if (num8 >= num5)
                            {
                                builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill ID"].ToString(), num4));
                                builder.AppendLine("<span style='page-break-after:always'></span>");
                                szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                                num8 = 0;
                                num11 = 0;
                                num = 0;
                            }
                        }
                        object obj3 = str5;
                        str5 = string.Concat(new object[] {
                            obj3, "<tr><td style='font-size:11px'>", num2.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b> Worker's Compensation Board </b><br/>", set.Tables[0].Rows[i]["WC_ADDRESS"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "<br />", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Case #"], "</td><td style='font-size:11px; width:50px;' align='left'> ", set.Tables[0].Rows[i]["Spciality"], "<br />",
                            set.Tables[0].Rows[i]["Min Date Of Service"], "-", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num10++;
                        num9++;
                        str6 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                    }
                    else
                    {
                        num = 1;
                        num2 = 1;
                        if (num10 > 0)
                        {
                            str5 = str5 + "</table>";
                            builder.Append(this.ReplaceHeaderAndFooter(str5, str6, num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num2 = 1;
                        }
                        if (flag)
                        {
                            str5 = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            flag = false;
                        }
                        szInput = szInput + "</table>";
                        builder.Append(this.ReplaceHeaderAndFooter(szInput, str7, num4));
                        builder.AppendLine("<span style='page-break-after:always'></span>");
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        str6 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                        object obj4 = szInput;
                        szInput = string.Concat(new object[] {
                            obj4, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "<br />", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Case #"], "</td><td style='font-size:11px; width:50px;' align='left'> ",
                            set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "-", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        if (num11 >= num5)
                        {
                            num8 = num11;
                            if (num8 >= num5)
                            {
                                builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill ID"].ToString(), num4));
                                builder.AppendLine("<span style='page-break-after:always'></span>");
                                szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                                num8 = 0;
                                num11 = 0;
                                num = 1;
                            }
                        }
                        object obj5 = str5;
                        str5 = string.Concat(new object[] {
                            obj5, "<tr><td style='font-size:11px'>", num2.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b> Worker's Compensation Board </b><br/>", set.Tables[0].Rows[i]["WC_ADDRESS"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "<br />", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Case #"], "</td><td style='font-size:11px; width:50px;' align='left'> ", set.Tables[0].Rows[i]["Spciality"], "<br />",
                            set.Tables[0].Rows[i]["Min Date Of Service"], "-", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num10++;
                        num3++;
                        num9 = 1;
                        num8 = 1;
                        num11 = 1;
                    }
                }
                else if (str11 != "WC000000000000000001")
                {
                    num8 = num11;
                    builder.AppendLine("");
                    if (num7 == 0)
                    {
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        num7 = 1;
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        num8 = 0;
                    }
                    if (str10.Equals(set.Tables[0].Rows[i]["Provider"].ToString()))
                    {
                        num++;
                        if (num8 >= num5)
                        {
                            builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill ID"].ToString(), num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            num8 = 0;
                            num11 = 0;
                            num = 1;
                        }
                        object obj6 = szInput;
                        szInput = string.Concat(new object[] {
                            obj6, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "<br />", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Case #"], "</td><td style='font-size:11px; width:50px; ' align='left'> ",
                            set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "-", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        num8++;
                    }
                    else
                    {
                        num = 1;
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        szInput = szInput + "</table>";
                        if (num10 > 0)
                        {
                            str5 = str5 + "</table>";
                            builder.Append(this.ReplaceHeaderAndFooter(str5, str6, num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num2 = 0;
                        }
                        builder.Append(this.ReplaceHeaderAndFooter(szInput, str7, num4));
                        builder.AppendLine("<span style='page-break-after:always'></span>");
                        szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                        object obj7 = szInput;
                        szInput = string.Concat(new object[] {
                            obj7, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "<br />", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Case #"], "</td><td style='font-size:11px; width:50px;' align='left'> ",
                            set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "-", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        num3++;
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        num8 = 1;
                        num11 = 1;
                    }
                }
            }
            string str12 = "";
            if (!flag && (num10 > 0))
            {
                str5 = str5 + "</table>";
                builder.AppendLine(this.ReplaceHeaderAndFooter(str5, str6, num4));
                builder.AppendLine("<span style='page-break-after:always'></span>");
            }
            if (num11 > 0)
            {
                str12 = szInput + "</table>";
                builder.AppendLine(this.ReplaceHeaderAndFooter(str12, str7, num4));
            }
            string str13 = builder.ToString();
            PdfMetamorphosis metamorphosis = new PdfMetamorphosis();
            metamorphosis.Serial = "10007706603";
            string str14 = this.getFileName("P") + ".htm";
            str3 = this.getFileName("P") + ".pdf";
            string text1 = ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str3;
            StreamWriter writer = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str14);
            writer.Write(str13);
            writer.Close();
            metamorphosis.HtmlToPdfConvertFile(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str14, ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str3);
            Bill_Sys_UploadFile file = new Bill_Sys_UploadFile();
            if (this.hdnPOMValue.Value == "Yes")
            {
                new ArrayList();
                new ArrayList();
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                ArrayList list3 = new ArrayList();
                new ArrayList();
                for (int j = 0; j < set.Tables[0].Rows.Count; j++)
                {
                    list.Add(set.Tables[0].Rows[j]["Bill ID"].ToString());
                    list2.Add(set.Tables[0].Rows[j]["SpecialityId"].ToString());
                    list3.Add(set.Tables[0].Rows[j]["Case Id"].ToString());
                }
                ArrayList list4 = new ArrayList();
                file.sz_bill_no = list;
                file.sz_case_id = list3;
                file.sz_speciality_id = list2;
                file.sz_company_id = this.txtCompanyId.Text;
                file.sz_flag = "POM";
                file.sz_FileName = str3;
                file.sz_File = File.ReadAllBytes(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str3);
                file.sz_UserId = oC_UserObject.SZ_USER_ID.ToString();
                file.sz_UserName = oC_UserObject.SZ_USER_NAME.ToString();
                FileUpload upload = new FileUpload();
                list4 = upload.UploadFile(file);
                path = upload.GetPath(Convert.ToInt32(list4[0].ToString()));
                this._reportBO.POMEntryOther(num4, DateTime.Today.ToString("MM/dd/yyyy"), Convert.ToInt32(list4[0].ToString()), str3.ToString(), path, oC_Account.SZ_COMPANY_ID, oC_UserObject.SZ_USER_ID, list, "POMG");
                this.BindGrid();
            }
            string text2 = str9 + str3;
            if (str == "")
            {
                str = str3;
                string text3 = str9 + str;
                string text4 = str9 + str;
            }
            else
            {
                str2 = str3;
                string text5 = str9 + str2;
            }
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + (str9 + str).ToString() + "'); ", true);
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

    private void creatNewPOMPDF()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = "";
        string str2 = "";
        string str3 = "";
        string szInput = "";
        string str5 = "";
        int num = 0;
        int num2 = 0;
        int num3 = 1;
        string str6 = "";
        string str7 = "";
        int num4 = 0;
        string path = "";
        Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
        StringBuilder builder = new StringBuilder();
        if (this.hdnPOMValue.Value.Equals("Yes"))
        {
            this._reportBO = new Bill_Sys_ReportBO();
            num4 = this._reportBO.POMSave(oC_Account.SZ_COMPANY_ID, oC_UserObject.SZ_USER_ID, 0);
        }
        builder.AppendLine("");
        int num5 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        int num6 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        try
        {
            template.getPhysicalPath();
            string str9 = ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET");
            int num7 = 0;
            DataSet set = this.CreateGroupData();
            int num8 = 0;
            int num9 = 0;
            string str10 = null;
            string str11 = null;
            bool flag = false;
            szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:10px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
            str5 = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:10px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
            int num10 = 0;
            int num11 = 0;
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                str11 = set.Tables[0].Rows[i]["CaseType"].ToString();
                if (str11 == "WC000000000000000001")
                {
                    builder.AppendLine("");
                    if (num7 == 0)
                    {
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        num7 = 1;
                        str6 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        num9 = 0;
                    }
                    if (str10.Equals(set.Tables[0].Rows[i]["Provider"].ToString()))
                    {
                        num++;
                        num2++;
                        if (num9 >= num6)
                        {
                            builder.Append(this.ReplaceHeaderAndFooter(str5, str6, num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num9 = 1;
                            num10 = 1;
                            num2 = 1;
                        }
                        if (flag)
                        {
                            str5 = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            flag = false;
                        }
                        object obj2 = szInput;
                        szInput = string.Concat(new object[] {
                            obj2, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "<br />", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Case #"], "<br /></td><td style='font-size:11px; width:50px;' align='left'> ",
                            set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "-", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        if (num11 >= num5)
                        {
                            num8 = num11;
                            if (num8 >= num5)
                            {
                                builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill ID"].ToString(), num4));
                                builder.AppendLine("<span style='page-break-after:always'></span>");
                                szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                                num8 = 0;
                                num11 = 0;
                                num = 0;
                            }
                        }
                        object obj3 = str5;
                        str5 = string.Concat(new object[] {
                            obj3, "<tr><td style='font-size:11px'>", num2.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b> Worker's Compensation Board </b><br/>", set.Tables[0].Rows[i]["WC_ADDRESS"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "<br />", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Case #"], "</td><td style='font-size:11px; width:50px;' align='left'> ", set.Tables[0].Rows[i]["Spciality"], "<br />",
                            set.Tables[0].Rows[i]["Min Date Of Service"], "-", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num10++;
                        num9++;
                        str6 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                    }
                    else
                    {
                        num = 1;
                        num2 = 1;
                        if (num10 > 0)
                        {
                            str5 = str5 + "</table>";
                            builder.Append(this.ReplaceHeaderAndFooter(str5, str6, num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num2 = 1;
                        }
                        if (flag)
                        {
                            str5 = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            flag = false;
                        }
                        szInput = szInput + "</table>";
                        builder.Append(this.ReplaceHeaderAndFooter(szInput, str7, num4));
                        builder.AppendLine("<span style='page-break-after:always'></span>");
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        str6 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                        object obj4 = szInput;
                        szInput = string.Concat(new object[] {
                            obj4, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "<br />", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Case #"], "</td><td style='font-size:11px; width:50px;' align='left'> ",
                            set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "-", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        if (num11 >= num5)
                        {
                            num8 = num11;
                            if (num8 >= num5)
                            {
                                builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill ID"].ToString(), num4));
                                builder.AppendLine("<span style='page-break-after:always'></span>");
                                szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                                num8 = 0;
                                num11 = 0;
                                num = 1;
                            }
                        }
                        object obj5 = str5;
                        str5 = string.Concat(new object[] {
                            obj5, "<tr><td style='font-size:11px'>", num2.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b> Worker's Compensation Board </b><br/>", set.Tables[0].Rows[i]["WC_ADDRESS"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "<br />", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Case #"], "</td><td style='font-size:11px; width:50px;' align='left'> ", set.Tables[0].Rows[i]["Spciality"], "<br />",
                            set.Tables[0].Rows[i]["Min Date Of Service"], "-", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num10++;
                        num3++;
                        num9 = 1;
                        num8 = 1;
                        num11 = 1;
                    }
                }
                else if (str11 != "WC000000000000000001")
                {
                    num8 = num11;
                    builder.AppendLine("");
                    if (num7 == 0)
                    {
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        num7 = 1;
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        num8 = 0;
                    }
                    if (str10.Equals(set.Tables[0].Rows[i]["Provider"].ToString()))
                    {
                        num++;
                        if (num8 >= num5)
                        {
                            builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill ID"].ToString(), num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            num8 = 0;
                            num11 = 0;
                            num = 1;
                        }
                        object obj6 = szInput;
                        szInput = string.Concat(new object[] {
                            obj6, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "<br />", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Case #"], "</td><td style='font-size:11px; width:50px; ' align='left'> ",
                            set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "-", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        num8++;
                    }
                    else
                    {
                        num = 1;
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        szInput = szInput + "</table>";
                        if (num10 > 0)
                        {
                            str5 = str5 + "</table>";
                            builder.Append(this.ReplaceHeaderAndFooter(str5, str6, num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num2 = 0;
                        }
                        builder.Append(this.ReplaceHeaderAndFooter(szInput, str7, num4));
                        builder.AppendLine("<span style='page-break-after:always'></span>");
                        szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='4%'>Article Number</td>    <td style='font-size:11px' width='22%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='6%'>Postage</td>    <td style='font-size:11px' width='4%'>Fee</td>    <td style='font-size:11px' width='6%'>Handling Charge</td>    <td style='font-size:11px' width='3%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='4%'>Due Sender if COD</td>    <td style='font-size:11px' width='3%'>R.R. fee</td>    <td style='font-size:11px' width='3%'>S.D. fee</td>    <td style='font-size:11px' width='3%'>S.H. fee</td>    <td style='font-size:11px' width='9%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                        object obj7 = szInput;
                        szInput = string.Concat(new object[] {
                            obj7, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "<br />", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Case #"], "</td><td style='font-size:11px; width:50px;' align='left'> ",
                            set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "-", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        num3++;
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        num8 = 1;
                        num11 = 1;
                    }
                }
            }
            string str12 = "";
            if (!flag && (num10 > 0))
            {
                str5 = str5 + "</table>";
                builder.AppendLine(this.ReplaceHeaderAndFooter(str5, str6, num4));
                builder.AppendLine("<span style='page-break-after:always'></span>");
            }
            if (num11 > 0)
            {
                str12 = szInput + "</table>";
                builder.AppendLine(this.ReplaceHeaderAndFooter(str12, str7, num4));
            }
            string str13 = builder.ToString();
            PdfMetamorphosis metamorphosis = new PdfMetamorphosis();
            metamorphosis.Serial = "10007706603";
            string str14 = this.getFileName("P") + ".htm";
            str3 = this.getFileName("P") + ".pdf";
            string text1 = ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str3;
            StreamWriter writer = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str14);
            writer.Write(str13);
            writer.Close();
            metamorphosis.HtmlToPdfConvertFile(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str14, ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str3);
            Bill_Sys_UploadFile file = new Bill_Sys_UploadFile();
            if (this.hdnPOMValue.Value == "Yes")
            {
                new ArrayList();
                new ArrayList();
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                ArrayList list3 = new ArrayList();
                new ArrayList();
                for (int j = 0; j < set.Tables[0].Rows.Count; j++)
                {
                    list.Add(set.Tables[0].Rows[j]["Bill ID"].ToString());
                    list2.Add(set.Tables[0].Rows[j]["SpecialityId"].ToString());
                    list3.Add(set.Tables[0].Rows[j]["Case Id"].ToString());
                }
                ArrayList list4 = new ArrayList();
                file.sz_bill_no = list;
                file.sz_case_id = list3;
                file.sz_speciality_id = list2;
                file.sz_company_id = this.txtCompanyId.Text;
                file.sz_flag = "POM";
                file.sz_FileName = str3;
                file.sz_File = File.ReadAllBytes(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str3);
                file.sz_UserId = oC_UserObject.SZ_USER_ID.ToString();
                file.sz_UserName = oC_UserObject.SZ_USER_NAME.ToString();
                FileUpload upload = new FileUpload();
                list4 = upload.UploadFile(file);
                path = upload.GetPath(Convert.ToInt32(list4[0].ToString()));
                this._reportBO.POMEntry(num4, DateTime.Today.ToString("MM/dd/yyyy"), Convert.ToInt32(list4[0].ToString()), str3.ToString(), path, oC_Account.SZ_COMPANY_ID, oC_UserObject.SZ_USER_ID, list, "POMG");
                this.BindGrid();
            }
            string text2 = str9 + str3;
            if (str == "")
            {
                str = str3;
                string text3 = str9 + str;
                string text4 = str9 + str;
            }
            else
            {
                str2 = str3;
                string text5 = str9 + str2;
            }
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + (str9 + str).ToString() + "'); ", true);
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

    private void creatPDF()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = "";
        string str2 = "";
        string str3 = "";
        string szInput = "";
        string str5 = "";
        int num = 0;
        int num2 = 0;
        int num3 = 1;
        string str6 = "";
        string str7 = "";
        int num4 = 0;
        string path = "";
        Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
        StringBuilder builder = new StringBuilder();
        if (this.hdnPOMValue.Value.Equals("Yes"))
        {
            this._reportBO = new Bill_Sys_ReportBO();
            num4 = this._reportBO.POMSave(oC_Account.SZ_COMPANY_ID, oC_UserObject.SZ_USER_ID, 0);
        }
        builder.AppendLine("");
        int num5 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        int num6 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        try
        {
            template.getPhysicalPath();
            string str9 = ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET");
            int num7 = 0;
            DataSet set = this.CreateGroupData();
            int num8 = 0;
            int num9 = 0;
            string str10 = null;
            string str11 = null;
            bool flag = false;
            szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
            str5 = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
            int num10 = 0;
            int num11 = 0;
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                str11 = set.Tables[0].Rows[i]["CaseType"].ToString();
                if (str11 == "WC000000000000000001")
                {
                    builder.AppendLine("");
                    if (num7 == 0)
                    {
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        num7 = 1;
                        str6 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        num9 = 0;
                    }
                    if (str10.Equals(set.Tables[0].Rows[i]["Provider"].ToString()))
                    {
                        num++;
                        num2++;
                        if (num9 >= num6)
                        {
                            builder.Append(this.ReplaceHeaderAndFooter(str5, str6, num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num9 = 1;
                            num10 = 1;
                            num2 = 1;
                        }
                        if (flag)
                        {
                            str5 = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            flag = false;
                        }
                        object obj2 = szInput;
                        szInput = string.Concat(new object[] {
                            obj2, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:11px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />",
                            set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        if (num11 >= num5)
                        {
                            num8 = num11;
                            if (num8 >= num5)
                            {
                                builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill ID"].ToString(), num4));
                                builder.AppendLine("<span style='page-break-after:always'></span>");
                                szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                                num8 = 0;
                                num11 = 0;
                                num = 0;
                            }
                        }
                        object obj3 = str5;
                        str5 = string.Concat(new object[] {
                            obj3, "<tr><td style='font-size:11px'>", num2.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b> Worker's Compensation Board </b><br/>", set.Tables[0].Rows[i]["WC_ADDRESS"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:11px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>",
                            set.Tables[0].Rows[i]["Bill ID"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num10++;
                        num9++;
                        str6 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                    }
                    else
                    {
                        num = 1;
                        num2 = 1;
                        if (num10 > 0)
                        {
                            str5 = str5 + "</table>";
                            builder.Append(this.ReplaceHeaderAndFooter(str5, str6, num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num2 = 1;
                        }
                        if (flag)
                        {
                            str5 = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            flag = false;
                        }
                        szInput = szInput + "</table>";
                        builder.Append(this.ReplaceHeaderAndFooter(szInput, str7, num4));
                        builder.AppendLine("<span style='page-break-after:always'></span>");
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        str6 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                        object obj4 = szInput;
                        szInput = string.Concat(new object[] {
                            obj4, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:11px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />",
                            set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        if (num11 >= num5)
                        {
                            num8 = num11;
                            if (num8 >= num5)
                            {
                                builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill ID"].ToString(), num4));
                                builder.AppendLine("<span style='page-break-after:always'></span>");
                                szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                                num8 = 0;
                                num11 = 0;
                                num = 1;
                            }
                        }
                        object obj5 = str5;
                        str5 = string.Concat(new object[] {
                            obj5, "<tr><td style='font-size:11px'>", num2.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b> Worker's Compensation Board </b><br/>", set.Tables[0].Rows[i]["WC_ADDRESS"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:11px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>",
                            set.Tables[0].Rows[i]["Bill ID"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num10++;
                        num3++;
                        num9 = 1;
                        num8 = 1;
                        num11 = 1;
                    }
                }
                else if (str11 != "WC000000000000000001")
                {
                    num8 = num11;
                    builder.AppendLine("");
                    if (num7 == 0)
                    {
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        num7 = 1;
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        num8 = 0;
                    }
                    if (str10.Equals(set.Tables[0].Rows[i]["Provider"].ToString()))
                    {
                        num++;
                        if (num8 >= num5)
                        {
                            builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill ID"].ToString(), num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            num8 = 0;
                            num11 = 0;
                            num = 1;
                        }
                        object obj6 = szInput;
                        szInput = string.Concat(new object[] {
                            obj6, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:11px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />",
                            set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        num8++;
                    }
                    else
                    {
                        num = 1;
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        szInput = szInput + "</table>";
                        if (num10 > 0)
                        {
                            str5 = str5 + "</table>";
                            builder.Append(this.ReplaceHeaderAndFooter(str5, str6, num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num2 = 0;
                        }
                        builder.Append(this.ReplaceHeaderAndFooter(szInput, str7, num4));
                        builder.AppendLine("<span style='page-break-after:always'></span>");
                        szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                        object obj7 = szInput;
                        szInput = string.Concat(new object[] {
                            obj7, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:11px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />",
                            set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        num3++;
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        num8 = 1;
                        num11 = 1;
                    }
                }
            }
            string str12 = "";
            if (!flag && (num10 > 0))
            {
                str5 = str5 + "</table>";
                builder.AppendLine(this.ReplaceHeaderAndFooter(str5, str6, num4));
                builder.AppendLine("<span style='page-break-after:always'></span>");
            }
            if (num11 > 0)
            {
                str12 = szInput + "</table>";
                builder.AppendLine(this.ReplaceHeaderAndFooter(str12, str7, num4));
            }
            string str13 = builder.ToString();
            PdfMetamorphosis metamorphosis = new PdfMetamorphosis();
            metamorphosis.Serial = "10007706603";
            string str14 = this.getFileName("P") + ".htm";
            str3 = this.getFileName("P") + ".pdf";
            string text1 = ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str3;
            StreamWriter writer = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str14);
            writer.Write(str13);
            writer.Close();
            metamorphosis.HtmlToPdfConvertFile(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str14, ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str3);
            Bill_Sys_UploadFile file = new Bill_Sys_UploadFile();
            if (this.hdnPOMValue.Value == "Yes")
            {
                new ArrayList();
                new ArrayList();
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                ArrayList list3 = new ArrayList();
                new ArrayList();
                for (int j = 0; j < set.Tables[0].Rows.Count; j++)
                {
                    list.Add(set.Tables[0].Rows[j]["Bill ID"].ToString());
                    list2.Add(set.Tables[0].Rows[j]["SpecialityId"].ToString());
                    list3.Add(set.Tables[0].Rows[j]["Case Id"].ToString());
                }
                ArrayList list4 = new ArrayList();
                file.sz_bill_no = list;
                file.sz_case_id = list3;
                file.sz_speciality_id = list2;
                file.sz_company_id = this.txtCompanyId.Text;
                file.sz_flag = "POM";
                file.sz_FileName = str3;
                file.sz_File = File.ReadAllBytes(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str3);
                file.sz_UserId = oC_UserObject.SZ_USER_ID.ToString();
                file.sz_UserName = oC_UserObject.SZ_USER_NAME.ToString();
                FileUpload upload = new FileUpload();
                list4 = upload.UploadFile(file);
                path = upload.GetPath(Convert.ToInt32(list4[0].ToString()));
                this._reportBO.POMEntry(num4, DateTime.Today.ToString("MM/dd/yyyy"), Convert.ToInt32(list4[0].ToString()), str3.ToString(), path, oC_Account.SZ_COMPANY_ID, oC_UserObject.SZ_USER_ID, list, "POMG");
                this.BindGrid();
            }
            string text2 = str9 + str3;
            if (str == "")
            {
                str = str3;
                string text3 = str9 + str;
                string text4 = str9 + str;
            }
            else
            {
                str2 = str3;
                string text5 = str9 + str2;
            }
            SaveActivityLog("POM_GENERATED", "");
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + (str9 + str).ToString() + "'); ", true);
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

    private void creatPDFForOther()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = "";
        string str2 = "";
        string str3 = "";
        string szInput = "";
        string str5 = "";
        int num = 0;
        int num2 = 0;
        int num3 = 1;
        string str6 = "";
        string str7 = "";
        int num4 = 0;
        string path = "";
        Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
        StringBuilder builder = new StringBuilder();
        if (this.hdnPOMValue.Value.Equals("Yes"))
        {
            this._reportBO = new Bill_Sys_ReportBO();
            num4 = this._reportBO.POMSaveOther(oC_Account.SZ_COMPANY_ID, oC_UserObject.SZ_USER_ID, 0);
            log.Debug("Get i_pom_id for POMSaveOther " + num4);
        }
        builder.AppendLine("");
        int num5 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        int num6 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        try
        {
            log.Debug("Enter Try ");
            log.Debug("Get  POMSaveOther szBasePhysicalPath" + template.getPhysicalPath());
            string str9 = ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET");
            log.Debug("Get POMSaveOther  szDefaultPath" + str9);
            int num7 = 0;
            DataSet set = this.CreateGroupData();
            int num8 = 0;
            int num9 = 0;
            string str10 = null;
            string str11 = null;
            bool flag = false;
            szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
            str5 = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
            log.Debug("Get POMSaveOther  genhtmlWC" + str5);
            int num10 = 0;
            int num11 = 0;
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                str11 = set.Tables[0].Rows[i]["CaseType"].ToString();
                log.Debug("Get POMSaveOther  strCasetype" + str11);
                if (str11 == "WC000000000000000001")
                {
                    builder.AppendLine("");
                    if (num7 == 0)
                    {
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        num7 = 1;
                        str6 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        num9 = 0;
                    }
                    if (str10.Equals(set.Tables[0].Rows[i]["Provider"].ToString()))
                    {
                        num++;
                        num2++;
                        if (num9 >= num6)
                        {
                            builder.Append(this.ReplaceHeaderAndFooter(str5, str6, num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num9 = 1;
                            num10 = 1;
                            num2 = 1;
                        }
                        if (flag)
                        {
                            str5 = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            flag = false;
                        }
                        object obj2 = szInput;
                        szInput = string.Concat(new object[] {
                            obj2, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:11px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />",
                            set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        if (num11 >= num5)
                        {
                            num8 = num11;
                            if (num8 >= num5)
                            {
                                builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill ID"].ToString(), num4));
                                builder.AppendLine("<span style='page-break-after:always'></span>");
                                szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                                num8 = 0;
                                num11 = 0;
                                num = 0;
                            }
                        }
                        object obj3 = str5;
                        str5 = string.Concat(new object[] {
                            obj3, "<tr><td style='font-size:11px'>", num2.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b> Worker's Compensation Board </b><br/>", set.Tables[0].Rows[i]["WC_ADDRESS"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:11px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>",
                            set.Tables[0].Rows[i]["Bill ID"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num10++;
                        num9++;
                        str6 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                    }
                    else
                    {
                        num = 1;
                        num2 = 1;
                        if (num10 > 0)
                        {
                            str5 = str5 + "</table>";
                            builder.Append(this.ReplaceHeaderAndFooter(str5, str6, num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num2 = 1;
                        }
                        if (flag)
                        {
                            str5 = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            flag = false;
                        }
                        szInput = szInput + "</table>";
                        builder.Append(this.ReplaceHeaderAndFooter(szInput, str7, num4));
                        builder.AppendLine("<span style='page-break-after:always'></span>");
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        str6 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                        object obj4 = szInput;
                        szInput = string.Concat(new object[] {
                            obj4, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:11px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />",
                            set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        if (num11 >= num5)
                        {
                            num8 = num11;
                            if (num8 >= num5)
                            {
                                builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill ID"].ToString(), num4));
                                builder.AppendLine("<span style='page-break-after:always'></span>");
                                szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                                num8 = 0;
                                num11 = 0;
                                num = 1;
                            }
                        }
                        object obj5 = str5;
                        str5 = string.Concat(new object[] {
                            obj5, "<tr><td style='font-size:11px'>", num2.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b> Worker's Compensation Board </b><br/>", set.Tables[0].Rows[i]["WC_ADDRESS"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:11px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>",
                            set.Tables[0].Rows[i]["Bill ID"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num10++;
                        num3++;
                        num9 = 1;
                        num8 = 1;
                        num11 = 1;
                    }
                }
                else if (str11 != "WC000000000000000001")
                {
                    log.Debug("  else if (strCasetype != WC000000000000000001");
                    num8 = num11;
                    builder.AppendLine("");
                    if (num7 == 0)
                    {
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        num7 = 1;
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        num8 = 0;
                    }
                    if (str10.Equals(set.Tables[0].Rows[i]["Provider"].ToString()))
                    {
                        num++;
                        if (num8 >= num5)
                        {
                            builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill ID"].ToString(), num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            num8 = 0;
                            num11 = 0;
                            num = 1;
                        }
                        object obj6 = szInput;
                        szInput = string.Concat(new object[] {
                            obj6, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:11px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />",
                            set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        num8++;
                    }
                    else
                    {
                        num = 1;
                        str10 = set.Tables[0].Rows[i]["Provider"].ToString();
                        szInput = szInput + "</table>";
                        if (num10 > 0)
                        {
                            str5 = str5 + "</table>";
                            builder.Append(this.ReplaceHeaderAndFooter(str5, str6, num4));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num2 = 0;
                        }
                        builder.Append(this.ReplaceHeaderAndFooter(szInput, str7, num4));
                        builder.AppendLine("<span style='page-break-after:always'></span>");
                        szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:11px' width='3%'>Line</td>    <td style='font-size:11px' width='5%'>Article Number</td>    <td style='font-size:11px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:11px' width='5%'>Postage</td>    <td style='font-size:11px' width='5%'>Fee</td>    <td style='font-size:11px' width='5%'>Handling Charge</td>    <td style='font-size:11px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:11px' width='5%'>Insured Value</td>    <td style='font-size:11px' width='5%'>Due Sender if COD</td>    <td style='font-size:11px' width='5%'>R.R. fee</td>    <td style='font-size:11px' width='5%'>S.D. fee</td>    <td style='font-size:11px' width='5%'>S.H. fee</td>    <td style='font-size:11px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                        object obj7 = szInput;
                        szInput = string.Concat(new object[] {
                            obj7, "<tr><td style='font-size:11px'>", num.ToString(), "</td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Claim Number"], "</td><td style='font-size:11px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Spciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:11px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />",
                            set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Bill ID"], "</td><td style='font-size:11px'></td><td style='font-size:11px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case #"], "</td></tr>"
                         });
                        num11++;
                        num3++;
                        str7 = set.Tables[0].Rows[i]["Bill ID"].ToString();
                        num8 = 1;
                        num11 = 1;
                    }
                }
            }
            string str12 = "";
            if (!flag && (num10 > 0))
            {
                str5 = str5 + "</table>";
                builder.AppendLine(this.ReplaceHeaderAndFooter(str5, str6, num4));
                builder.AppendLine("<span style='page-break-after:always'></span>");
            }
            if (num11 > 0)
            {
                str12 = szInput + "</table>";
                builder.AppendLine(this.ReplaceHeaderAndFooter(str12, str7, num4));
            }
            string str13 = builder.ToString();
            PdfMetamorphosis metamorphosis = new PdfMetamorphosis();
            metamorphosis.Serial = "10007706603";
            string str14 = this.getFileName("P") + ".htm";
            str3 = this.getFileName("P") + ".pdf";
            string text1 = ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str3;
            StreamWriter writer = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str14);
            writer.Write(str13);
            writer.Close();
            metamorphosis.HtmlToPdfConvertFile(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str14, ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str3);
            Bill_Sys_UploadFile file = new Bill_Sys_UploadFile();
            if (this.hdnPOMValue.Value == "Yes")
            {
                new ArrayList();
                new ArrayList();
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                ArrayList list3 = new ArrayList();
                new ArrayList();
                for (int j = 0; j < set.Tables[0].Rows.Count; j++)
                {
                    list.Add(set.Tables[0].Rows[j]["Bill ID"].ToString());
                    list2.Add(set.Tables[0].Rows[j]["SpecialityId"].ToString());
                    list3.Add(set.Tables[0].Rows[j]["Case Id"].ToString());
                }
                ArrayList list4 = new ArrayList();
                file.sz_bill_no = list;
                file.sz_case_id = list3;
                file.sz_speciality_id = list2;
                file.sz_company_id = this.txtCompanyId.Text;
                file.sz_flag = "POM";
                file.sz_FileName = str3;
                file.sz_File = File.ReadAllBytes(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str3);
                file.sz_UserId = oC_UserObject.SZ_USER_ID.ToString();
                file.sz_UserName = oC_UserObject.SZ_USER_NAME.ToString();
                FileUpload upload = new FileUpload();
                list4 = upload.UploadFile(file);
                path = upload.GetPath(Convert.ToInt32(list4[0].ToString()));
                this._reportBO.POMEntryOther(num4, DateTime.Today.ToString("MM/dd/yyyy"), Convert.ToInt32(list4[0].ToString()), str3.ToString(), path, oC_Account.SZ_COMPANY_ID, oC_UserObject.SZ_USER_ID, list, "POMG");
                this.BindGrid();
            }
            string text2 = str9 + str3;
            if (str == "")
            {
                str = str3;
                string text3 = str9 + str;
                string text4 = str9 + str;
            }
            else
            {
                str2 = str3;
                string text5 = str9 + str2;
            }
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + (str9 + str).ToString() + "'); ", true);
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

    public void GenerateXL(DataTable dt, string file)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ApplicationSettings_BO objApplicationBO = this.objApplicationBO;
        if (objApplicationBO == null)
        {
            objApplicationBO = new ApplicationSettings_BO();
        }
        string path = objApplicationBO.getParameterValue("lawfirm_integration_url").ParameterValue;
        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties='Excel 12.0;HDR=Yes;'");
        try
        {

            log.Debug("Rows Count :" + dt.Rows.Count);
            connection.Open();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OleDbCommand command = new OleDbCommand();
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 0;
                string provider = this.GetProvider(dt.Rows[i]["SZ_BILL_NUMBER"].ToString());
                string str3 = this.GetEncryptedURLIntegrationData(path, dt.Rows[i]["SZ_CASE_ID"].ToString(), dt.Rows[i]["SZ_CASE_NO"].ToString(), dt.Rows[i]["SZ_COMPANY_ID"].ToString(), dt.Rows[i]["SZ_BILL_NUMBER"].ToString(), dt.Rows[i]["LAWFIRM_ID"].ToString());
                command.CommandText = string.Concat(new object[] {
                    "INSERT INTO [Sheet1$]([Bill number],[Case No],[Patient Name],[Speciality],[Provider], [Bill Amount],[Visit Date],[Bill Date],[Referring Doctor],[Insurance Name],[Insurance Claim Number], [Accident Date],[Bill Status Date],[Status],[Username]) values(",
                    dt.Rows[i]["SZ_BILL_NUMBER"], ",", dt.Rows[i]["SZ_CASE_NO"], ",'", dt.Rows[i]["SZ_PATIENT_NAME"], "','", dt.Rows[i]["Specialty"],
                    "','", provider, "','", dt.Rows[i]["FLT_BILL_AMOUNT"], "','", dt.Rows[i]["MIN_DATE_OF_SERVICE"] + "-" + dt.Rows[i]["MAX_DATE_OF_SERVICE"], "','", dt.Rows[i]["DT_BILL_DATE"], "','", dt.Rows[i]["SZ_REFFERING_DOC_NAME"],
                    "','", dt.Rows[i]["SZ_INSURANCE_NAME"], "','", dt.Rows[i]["SZ_CLAIM_NUMBER"],"','", dt.Rows[i]["DT_ACCIDENT_DATE"],
                    "','", dt.Rows[i]["DT_BILL_STATUS_DATE"], "','", dt.Rows[i]["SZ_STATUS"], "','", dt.Rows[i]["SZ_USERNAME"], "')"
                 });
                command.Connection = connection;
                command.ExecuteNonQuery();
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
        finally
        {
            connection.Close();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string GetEncryptedURLIntegrationData(string path, string caseid, string caseno, string cmpid, string billno, string lawfirmid)
    {
        Encryption encryption = new Encryption();
        string str = string.Concat(new object[] { caseid, ',', caseno, ',', cmpid, ',', billno, ',', lawfirmid });
        string str2 = encryption.EncryptURLData(str);
        if (((str2.Contains("+") || str2.Contains("&")) || (str2.Contains("$") || str2.Contains(","))) || ((str2.Contains(":") || str2.Contains(";")) || (str2.Contains("?") || str2.Contains("@"))))
        {
            str2 = str2.Replace("+", "~pl~").Replace("&", "~am~").Replace("$", "~dl~").Replace(",", "~cm~").Replace(":", "~cl~").Replace(";", "~scl~").Replace("?", "~qs~").Replace("@", "~at~");
        }
        return (path + "?source=int_url&dt=" + str2);
    }

    private string getFileName(string p_szBillNumber)
    {
        DateTime now = DateTime.Now;
        return (p_szBillNumber + "_" + this.getRandomNumber() + "_" + now.ToString("yyyyMMddHHmmssms"));
    }

    public string getNF2MailDetails(string p_htmlstring, string p_szCompanyID, string sz_bill_id)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = "";
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        connection.Open();
        GeneratePatientInfoPDF opdf = new GeneratePatientInfoPDF();
        try
        {
            string str2 = "";
            str2 = p_htmlstring;
            SqlCommand command = new SqlCommand("SP_NF3_MAILS_DETAILS", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            command.Parameters.AddWithValue("@SZ_BILL_ID", sz_bill_id);
            SqlDataReader reader = command.ExecuteReader();
            str = opdf.ReplaceNF2MailDetails(str2, reader);
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
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return str;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetPateintInfo(string BillNO)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            log.Debug("SP_GET_DOCUMENT_INFO @SZ_BILL_NUMBER'" + BillNO + "'");
            SqlCommand selectCommand = new SqlCommand("SP_GET_DOCUMENT_INFO", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.CommandTimeout = 0;
            selectCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", BillNO);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
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
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public string getPhysicalPath()
    {
        string str = "";
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlDataReader reader = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", connection).ExecuteReader();
            while (reader.Read())
            {
                str = reader["parametervalue"].ToString();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return str;
    }

    public string GetProvider(string szbillno)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string str = "";
        try
        {
            connection.Open();
            log.Debug("sp_get_provider_name_using_bill_no @billno'" + szbillno + "'");
            SqlCommand command = new SqlCommand("sp_get_provider_name_using_bill_no", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@sz_bill_number", szbillno);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                str = reader[0].ToString();
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
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return str;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string getRandomNumber()
    {
        Random random = new Random();
        return random.Next(1, 0x2710).ToString();
    }

   

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + this.grdBillReportDetails.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")) + "';", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        oC_UserObject = (Bill_Sys_UserObject)(Session["USER_OBJECT"]);
        oC_Account = (Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"]);

        if (oC_Account == null || oC_UserObject == null)
        {
            DisableInput();
            lblMessage.Text = "Oops! Your session has expired. You must re-login to proceed !";
            lblMessage.ForeColor = Color.Red;
            return;
        }

        this.extddlBillStatus.Flag_ID = oC_Account.SZ_COMPANY_ID;
        this.txtCompanyId.Text = oC_Account.SZ_COMPANY_ID;

        this.con.SourceGrid = this.grdBillReportDetails;
        this.txtSearchBox.SourceGrid = this.grdBillReportDetails;
        this.grdBillReportDetails.Page = this.Page;
        this.grdBillReportDetails.PageNumberList = this.con;
        this.Span2.InnerHtml = "Is this the final POM?";
        this.btnUpdateStatus.Attributes.Add("onclick", "return CheckVal();");
        this.btnCreatePacket.Attributes.Add("onclick", "return Check();");
        this.btnPrintEnvelop.Attributes.Add("onclick", "return Check();");
        this.btnPrintPOM.Attributes.Add("onclick", "return POMConformation();");
        this.btnExportData.Attributes.Add("onclick", "return Check();");
        if (!this.Page.IsPostBack)
        {
            this.txtBillStatusdate.Text = DateTime.Today.ToShortDateString();
            if (base.Request.QueryString["flag"].ToString() == "View")
            {
                this.Session["sz_StatusID"] = base.Request.QueryString["Status"].ToString();
                this.Session["sz_SpecialityID"] = base.Request.QueryString["speciality"].ToString();
                this.Session["CaseNumber"] = base.Request.QueryString["CaseNumber"].ToString();
                this.Session["BillNumber"] = base.Request.QueryString["BillNumber"].ToString();
                this.Session["PatientName"] = base.Request.QueryString["PatientName"].ToString();

            }
            this.BindGrid();
        }
    }

    protected string ReplaceHeaderAndFooter(string szInput, string sz_bill_no, int i_pom_id)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = "";
        try
        {
            if (this.hdnPOMValue.Value.Equals("Yes"))
            {
                str = File.ReadAllText(ConfigurationManager.AppSettings["TEST_POM_HTML"]);
                str = str.Replace("VL_SZ_POM_ID", i_pom_id.ToString());
            }
            else
            {
                str = File.ReadAllText(ConfigurationManager.AppSettings["POM_HTML"]);
            }
            str = this.getNF2MailDetails(str, oC_Account.SZ_COMPANY_ID, sz_bill_no);
            str = str.Replace("VL_SZ_TABLE_DATA", szInput);
            str = str.Replace("VL_SZ_CASE_COUNT", "");
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

    private string RequestParameter(string p_szKey)
    {
        if (Request.QueryString != null)
        {
            string sQueryString = DecodeUrlString(Request.QueryString.ToString());

            System.Collections.Specialized.NameValueCollection collection =
                System.Web.HttpUtility.ParseQueryString(sQueryString);

            return collection[p_szKey].ToString();
        }
        else
        {
            return null;
        }
    }

    private string DecodeUrlString(string url)
    {
        string newUrl;
        while ((newUrl = Uri.UnescapeDataString(url)) != url)
            url = newUrl;
        return newUrl;
    }

    private void DisableInput()
    {
        List<Control> lstControl = new List<Control>();
        IterateFormControls(lstControl, this.Page);
        foreach (Control c in lstControl)
        {
            if (c.GetType() == typeof(TextBox))
            {
                TextBox t = (TextBox)c;
                t.Enabled = false;
            }

            if (c.GetType() == typeof(Button))
            {
                Button b = (Button)c;
                b.Enabled = false;
            }

            if (c.GetType() == typeof(RadioButton))
            {
                RadioButton r = (RadioButton)c;
                r.Enabled = false;
            }

            if (c.GetType() == typeof(GridView))
            {
                GridView g = (GridView)c;
                g.Enabled = false;
            }
        }
    }

    private void IterateFormControls(List<Control> lstControl, Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c is Control)
            {
                lstControl.Add(c);

                if (c.Controls.Count > 0)
                {
                    IterateFormControls(lstControl, c);
                }
            }
        }
    }

    protected void btn_BillPacket_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList list = new ArrayList();
            string str = "";
            for (int i = 0; i < this.grdBillReportDetails.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdBillReportDetails.Rows[i].Cells[2].FindControl("chkUpdateStatus");
                if (box.Checked)
                {
                    Bill_Sys_Bill_Packet_Request request = new Bill_Sys_Bill_Packet_Request();
                    request.SZ_CASE_ID = this.grdBillReportDetails.DataKeys[i]["SZ_CASE_ID"].ToString();
                    request.SZ_BILL_NUMBER = this.grdBillReportDetails.DataKeys[i]["SZ_BILL_ID"].ToString();
                    request.SZ_SPECIALTY = this.grdBillReportDetails.DataKeys[i]["SZ_SPECIALITY"].ToString();
                    list.Add(request);
                }
            }
            Bill_Sys_Upload_VisitReport report = new Bill_Sys_Upload_VisitReport();
            str = "121";
            if (str != "")
            {
                string str2 = "";
                str2 = report.CreateBillPacket(this.txtCompanyId.Text, list);
                if (str2.Contains("ERROR"))
                {
                    string[] strArray = str2.Split(new char[] { ',' });
                    this.usrMessage.PutMessage("Document are not found for bill number" + strArray[1].ToString());
                    this.usrMessage.SetMessageType(0);
                    this.usrMessage.Show();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + ApplicationSettings.GetParameterValue("PACKET_DOC_URL") + str2 + "'); ", true);
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

    protected void btn_Both_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList list = new ArrayList();
            string str = "";
            for (int i = 0; i < this.grdBillReportDetails.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdBillReportDetails.Rows[i].Cells[2].FindControl("chkUpdateStatus");
                if (box.Checked)
                {
                    Bill_Sys_Bill_Packet_Request request = new Bill_Sys_Bill_Packet_Request();
                    request.SZ_CASE_ID = this.grdBillReportDetails.DataKeys[i]["SZ_CASE_ID"].ToString();
                    request.SZ_BILL_NUMBER = this.grdBillReportDetails.DataKeys[i]["SZ_BILL_ID"].ToString();
                    request.SZ_SPECIALTY = this.grdBillReportDetails.DataKeys[i]["SZ_SPECIALITY"].ToString();
                    list.Add(request);
                }
            }
            Bill_Sys_Upload_VisitReport report = new Bill_Sys_Upload_VisitReport();
            str = "121";
            if (str != "")
            {
                string str2 = report.CreateBothPacket(this.txtCompanyId.Text, list);
                if (str2.Contains("ERROR"))
                {
                    string[] strArray = str2.Split(new char[] { ',' });
                    this.usrMessage.PutMessage("Document are not found for bill number" + strArray[1].ToString());
                    this.usrMessage.SetMessageType(0);
                    this.usrMessage.Show();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + ApplicationSettings.GetParameterValue("PACKET_DOC_URL") + str2 + "'); ", true);
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

    protected void btn_PacketDocument_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList list = new ArrayList();
            string str = "";
            for (int i = 0; i < this.grdBillReportDetails.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdBillReportDetails.Rows[i].Cells[2].FindControl("chkUpdateStatus");
                if (box.Checked)
                {
                    Bill_Sys_Bill_Packet_Request request = new Bill_Sys_Bill_Packet_Request();
                    request.SZ_CASE_ID = this.grdBillReportDetails.DataKeys[i]["SZ_CASE_ID"].ToString();
                    request.SZ_BILL_NUMBER = this.grdBillReportDetails.DataKeys[i]["SZ_BILL_ID"].ToString();
                    request.SZ_SPECIALTY = this.grdBillReportDetails.DataKeys[i]["SZ_SPECIALITY"].ToString();
                    list.Add(request);
                }
            }
            Bill_Sys_Upload_VisitReport report = new Bill_Sys_Upload_VisitReport();
            str = "121";
            if (str != "")
            {
                string str2 = report.CreateBillPacketDocument(this.txtCompanyId.Text, list);
                if (str2.Contains("ERROR"))
                {
                    string[] strArray = str2.Split(new char[] { ',' });
                    this.usrMessage.PutMessage("Document are not found for bill number" + strArray[1].ToString());
                    this.usrMessage.SetMessageType(0);
                    this.usrMessage.Show();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + ApplicationSettings.GetParameterValue("PACKET_DOC_URL") + str2 + "'); ", true);
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
    protected void btnDownloadPackets_Click(object sender, EventArgs e)
    {
        Response.Redirect("downloadpackets.aspx");
    }



    protected void grdBillReportDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (System.DBNull.Value.Equals((e.Row.DataItem as System.Data.DataRowView).Row["bill_path"]) || (e.Row.DataItem as System.Data.DataRowView).Row["bill_path"] == null)
            {
                (e.Row.FindControl("chkUpdateStatus") as CheckBox).InputAttributes.Add("BillId", (e.Row.DataItem as System.Data.DataRowView).Row["SZ_BILL_ID"].ToString());
            }
            else if (!File.Exists((e.Row.DataItem as System.Data.DataRowView).Row["bill_path"].ToString()))
            {
                (e.Row.FindControl("chkUpdateStatus") as CheckBox).InputAttributes.Add("BillId", (e.Row.DataItem as System.Data.DataRowView).Row["SZ_BILL_ID"].ToString());
            }

        }
    }


}
