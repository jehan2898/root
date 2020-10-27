using Aquaforest.PDF;
using CUTEFORMCOLib;
using Hangfire;
using SautinSoft;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_DownloadPackets : System.Web.UI.Page
{
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
    private Bill_Sys_BillingCompanyObject oC_Account = null;
    Bill_Sys_Upload_VisitReport report = new Bill_Sys_Upload_VisitReport();
    private Bill_Sys_ReportBO _reportBO;
    private Bill_Sys_UserObject oC_UserObject = null;

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        oC_UserObject = (Bill_Sys_UserObject)(Session["USER_OBJECT"]);
        oC_Account = (Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"]);
        this.txtCompanyId.Text = oC_Account.SZ_COMPANY_ID;
        this._reportBO = new Bill_Sys_ReportBO();
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    #endregion

    public void BtnRefresh_ServerClick(object sender, EventArgs e)
    {
        BindGrid();
    }

    #region Requeue Click
    public void btnRequeue_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow gr = (GridViewRow)btn.NamingContainer;
        string jobid = (gr.FindControl("hdRequeue") as HiddenField).Value;
        BackgroundJob.Requeue(jobid);
        SaveActivityLog("PACKET_REQUEUE", "Job id : " + jobid);
    }
    #endregion

    #region Bind Grid
    void BindGrid()
    {
        gvData.DataSource = report.GetDataFromHangfire(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        gvData.DataBind();
    }
    #endregion

    #region Download Button Click
    protected void btnDownLoad_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow gr = (GridViewRow)btn.NamingContainer;
            string filePath = (gr.FindControl("hidFilePath") as HiddenField).Value;
            string jobid = (gr.FindControl("hdRequeue") as HiddenField).Value;
            ProcessRequestURI(filePath);
            SaveActivityLog("PACKET_DOWNLOADED", "Job id : " + jobid);
        }
        catch
        {

        }
    }
    void ProcessRequestURI(string FilePath)
    {
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + FilePath + "'); ", true);
    }
    #endregion

    #region Generate File Name
    public string GenerateFileName(string filename)
    {
        return DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N") + filename;
    }
    #endregion

    #region Print Envelop Click
    protected void btnPrintEnvelop_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> pdfList = new List<string>();
            oC_Account = (Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"]);
            Button btn = (Button)sender;
            GridViewRow gr = (GridViewRow)btn.NamingContainer;
            string jobid = (gr.FindControl("hdRequeue") as HiddenField).Value;
            // string SZ_BILL_STATUS_ID = (gr.FindControl("hdPOM") as HiddenField).Value;
            string SZ_PROCEDURE_GROUP_ID = (gr.FindControl("hdEnvelop") as HiddenField).Value;
            string SZ_COMPANY_ID = (gr.FindControl("hdnCompanyID") as HiddenField).Value;


            #region Get All Bills // Mangesh
            string I_PACKET_REQUEST_ID = (gr.FindControl("hidPacketID") as HiddenField).Value;
            DataSet ds_ = report.GetAllBillsByPacketRequestID(Convert.ToInt32(I_PACKET_REQUEST_ID));

            List<string> lstBills = new List<string>();
            foreach (DataRow item in ds_.Tables[0].Rows)
            {
                lstBills.Add(item[0].ToString());
            }
            #endregion

            #region Dataset From DB FOr Envelop
            string bills = lstBills.Aggregate((x, y) => x + "," + y);
            DataSet ds = report.GetDataForPOMEnv(SZ_COMPANY_ID, SZ_PROCEDURE_GROUP_ID, bills);

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
                //this.txtCompanyId.Text = oC_Account.SZ_COMPANY_ID;
                set2 = new Bill_Sys_ProcedureCode_BO().Get_Sys_Key("SS00075", SZ_COMPANY_ID);
                string bt_get_wc = "";
                if (set2.Tables.Count > 0)
                {
                    if (set2.Tables[0].Rows.Count > 0)
                    {
                        bt_get_wc = set2.Tables[0].Rows[0][0].ToString();
                    }
                }
                //Change added on 19/06/2015 to print the single page envelope for WC
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string str17 = ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                    string str18 = ds.Tables[0].Rows[i]["SZ_BILL_ID"].ToString();
                    //CheckBox box = (CheckBox)this.grdBillReportDetails.Rows[i].FindControl("chkUpdateStatus");
                    //if (box.Checked)
                    //{
                    string str19 = ds.Tables[0].Rows[i]["SZ_CASE_TYPE"].ToString();
                    string str20 = ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();

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

                            str10 = GenerateFileName(str10);
                            MergePDF.MergePDFFiles(str11, str13, str4 + str15 + str10);

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
                            str9 = str16 + str5;
                        }
                        else
                        {
                            string str24 = oC_Account.SZ_COMPANY_NAME + "/" + str17 + "/Packet Document/";
                            str7 = str23;
                            str8 = str4 + str24 + str7;

                            str5 = GenerateFileName(str23);
                            MergePDF.MergePDFFiles(str6, str8, str4 + str16 + str5);

                            str6 = str4 + str16 + str5;
                            str9 = str16 + str5;
                        }
                    }
                    //}
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
                SaveActivityLog("ENVLP_GENERATED", "Job id : " + jobid);
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + str9.ToString() + "'); ", true);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            #endregion
        }
        catch
        {

        }
    }
    #endregion

    #region Print POM Click
    string jobid = string.Empty;
    protected void btnPrintPOM_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow gr = (GridViewRow)btn.NamingContainer;
        jobid = (gr.FindControl("hdRequeue") as HiddenField).Value;
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_NEW_POM == "1")
        {
            this.creatNewPOMPDF(sender);
        }
        else
        {
            this.creatPDF(sender);
        }
        this.hdnPOMValue.Value = "";
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        this.hdnPOMValue.Value = "Yes";
        this.btnYes.Attributes.Add("onclick", "YesMassage");
        //btnPrintPOM_Click(null, null);
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        this.hdnPOMValue.Value = "No";
        this.btnNo.Attributes.Add("onclick", "NoMassage");
        //btnPrintPOM_Click(null, null);
    }
    private void creatNewPOMPDF(object sender)
    {   //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string path1 = string.Empty;
        int pomid = this._reportBO.CheckPOM(oC_Account.SZ_COMPANY_ID, oC_UserObject.SZ_USER_ID, Convert.ToInt32(jobid), ref path1);
        if (pomid < 0 || (pomid > 0 && path1.Trim() == String.Empty))
        {
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
                this._reportBO.PacketUpdate(oC_Account.SZ_COMPANY_ID, oC_UserObject.SZ_USER_ID, Convert.ToInt32(jobid), num4);
            }
            if ((pomid > 0 && path1.Trim() == String.Empty))
            {

                num4 = pomid;
                this.hdnPOMValue.Value = "Yes";
            }
            builder.AppendLine("");
            int num5 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
            int num6 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
            try
            {
                template.getPhysicalPath();
                string str9 = ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET");
                int num7 = 0;
                DataSet set = this.CreateGroupData(sender);
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
                SaveActivityLog("POM_GENERATED", "Job Id : " + jobid);
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
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + path1.Replace("\\", "/") + "'); ", true);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void creatPDF(object sender)
    {//Logging Start

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string path1 = string.Empty;
        int pomid = this._reportBO.CheckPOM(oC_Account.SZ_COMPANY_ID, oC_UserObject.SZ_USER_ID, Convert.ToInt32(jobid), ref path1);
        if (pomid < 0 || (pomid > 0 && path1.Trim() == String.Empty))
        {
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
                this._reportBO.PacketUpdate(oC_Account.SZ_COMPANY_ID, oC_UserObject.SZ_USER_ID, Convert.ToInt32(jobid), num4);
            }
            if ((pomid > 0 && path1.Trim() == String.Empty))
            {

                num4 = pomid;
                 this.hdnPOMValue.Value = "Yes";
            }
            builder.AppendLine("");
            int num5 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
            int num6 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
            try
            {
                template.getPhysicalPath();
                string str9 = ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET");
                int num7 = 0;
                DataSet set = this.CreateGroupData(sender);
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

                SaveActivityLog("POM_GENERATED", "Job Id : " + jobid);
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
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + path1.Replace("\\", "/") + "'); ", true);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private DataSet CreateGroupData(object sender)
    {   //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet set = new DataSet();
        try
        {
            Button btn = (Button)sender;
            GridViewRow gr = (GridViewRow)btn.NamingContainer;

            string SZ_PROCEDURE_GROUP_ID = (gr.FindControl("hdEnvelop") as HiddenField).Value;
            string SZ_COMPANY_ID = (gr.FindControl("hdnCompanyID") as HiddenField).Value;

            #region Get All Bills // Mangesh
            string I_PACKET_REQUEST_ID = (gr.FindControl("hidPacketID") as HiddenField).Value;
            DataSet ds_ = report.GetAllBillsByPacketRequestID(Convert.ToInt32(I_PACKET_REQUEST_ID));

            List<string> lstBills = new List<string>();
            foreach (DataRow item in ds_.Tables[0].Rows)
            {
                lstBills.Add(item[0].ToString());
            }
            #endregion

            #region Dataset From DB FOr Envelop
            string bills = lstBills.Aggregate((x, y) => x + "," + y);
            DataSet ds = report.GetDataForPOMEnv(SZ_COMPANY_ID, SZ_PROCEDURE_GROUP_ID, bills);

            #endregion
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
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //if (((CheckBox)this.grdBillReportDetails.Rows[i].FindControl("chkUpdateStatus")).Checked)
                //{
                DataRow row = table.NewRow();
                row["Bill ID"] = ds.Tables[0].Rows[i]["SZ_BILL_ID"].ToString();
                row["Case #"] = ds.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                row["Patient Name"] = ds.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString().Replace(" ", ",").Trim();
                row["Spciality"] = ds.Tables[0].Rows[i]["SZ_SPECIALITY"].ToString();
                row["Provider"] = ds.Tables[0].Rows[i]["SZ_PROVIDER"].ToString();
                row["Bill Amount"] = ds.Tables[0].Rows[i]["SZ_BILL_AMOUNT"].ToString();
                row["Visit Date"] = ds.Tables[0].Rows[i]["PROC_DATE"].ToString();
                row["Bill Date"] = ds.Tables[0].Rows[i]["DT_BILL_DATE"].ToString();
                row["Bill Staus Date"] = ds.Tables[0].Rows[i]["DT_BILL_STATUS_DATE"].ToString();
                row["Status"] = ds.Tables[0].Rows[i]["SZ_STATUS"].ToString();
                row["Username"] = ds.Tables[0].Rows[i]["SZ_USERNAME"].ToString();
                row["Claim Number"] = ds.Tables[0].Rows[i]["CLAIM_NO"].ToString();
                row["Insurance Company"] = ds.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString();
                row["Insurance Address"] = ds.Tables[0].Rows[i]["SZ_INSURANCE_ADDRESS"].ToString();
                row["Min Date Of Service"] = ds.Tables[0].Rows[i]["MIN_DATE_OF_SERVICE"].ToString();
                row["Max Date Of Service"] = ds.Tables[0].Rows[i]["MAX_DATE_OF_SERVICE"].ToString();
                row["CaseType"] = ds.Tables[0].Rows[i]["SZ_CASE_TYPE"].ToString();
                row["WC_ADDRESS"] = ds.Tables[0].Rows[i]["WC_ADDRESS"].ToString();
                row["Case Id"] = ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                row["SpecialityId"] = ds.Tables[0].Rows[i]["SZ_SPECIALITY_ID"].ToString();
                row["ProviderId"] = ds.Tables[0].Rows[i]["SZ_OFFICE_ID"].ToString();
                int num2 = Convert.ToInt32(ds.Tables[0].Rows[i]["SZ_BILL_ID"].ToString().Substring(2));
                row["ID"] = num2;
                table.Rows.Add(row);
                //}
            }
            if (table.Rows.Count > 0)
            {
                DataView defaultView = table.DefaultView;
                if (sysSettingForSort == "1")
                {
                    defaultView.Sort = "Provider,ID";
                }
                else
                {
                    defaultView.Sort = "Provider";
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
    #endregion

   


    protected void btnBills_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            DataSet ds = report.GetAllBillsByPacketRequestID(Convert.ToInt32(btn.Text));
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append("<table class=table>");
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                str.Append("<tr><td>" + item[0].ToString() + "</td><tr/>");
            }
            str.Append("</Table>");
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + str.ToString() + "');", true);
        }
        catch
        {

        }
    }

    protected void btnPacketDetails_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow gr = (GridViewRow)btn.NamingContainer;
            string packetrequestid = (gr.FindControl("hidPacketID") as HiddenField).Value;
            DataSet ds = report.GetAllBillsPacketDetails(Convert.ToInt32(packetrequestid));
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append("<table class=table>");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    str.Append("<tr><td>" + item[0].ToString().Replace("\\", "//") + "</td><tr/>");
                }
            }
            else
            {
                str.Append("<tr><td>No log to display</td><tr/>");
            }
            str.Append("</Table>");
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + str.ToString() + "');", true);
        }
        catch
        {

        }
    }

    public void ProcessRequest(string FileLocation)
    {
        Stream stream = null;
        try
        {
            HttpContext.Current.Response.Clear();

            stream = new FileStream(FileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);
            long bytesToRead = stream.Length;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(FileLocation));
            while (bytesToRead > 0)
            {
                if (HttpContext.Current.Response.IsClientConnected)
                {
                    byte[] buffer = new Byte[10000];
                    int length = stream.Read(buffer, 0, 10000);
                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                    HttpContext.Current.Response.Flush();
                    bytesToRead = bytesToRead - length;
                }
                else
                {
                    bytesToRead = -1;
                }
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell statusCell = e.Row.Cells[5];
            Button btnDownload = (Button)e.Row.FindControl("btnDownLoad");
            Button btnRequeue = (Button)e.Row.FindControl("btnRequeue");
            Button btnPrintPOM = (Button)e.Row.FindControl("btnPOM");
            Button btnErrorLog = (Button)e.Row.FindControl("btnErrorLog");
            Button btnEnvelop = (Button)e.Row.FindControl("btnEnvelop");
            btnRequeue.Attributes["onclick"] = "javascript:return ConfirmSave();";
            btnPrintPOM.Attributes.Add("onclick", "return myFunction(this);");

            if (statusCell.Text == "Succeeded")
            {
                btnDownload.Enabled = true;
                btnRequeue.Enabled = false;
                btnPrintPOM.Enabled = true;
                btnErrorLog.Enabled = true;
                btnEnvelop.Enabled = true;
                statusCell.Attributes["style"] = "background-color: green;color:white;font-weight:bold";
            }
            else if (statusCell.Text == "Deleted")
            {
                btnDownload.Enabled = false;
                btnRequeue.Enabled = true;
                btnPrintPOM.Enabled = false;
                btnErrorLog.Enabled = true;
                btnEnvelop.Enabled = false;
                statusCell.Attributes["style"] = "background-color: red;color:white;font-weight:bold";
            }
            else if (statusCell.Text == "Deleted Job")
            {
                btnDownload.Enabled = true;
                btnRequeue.Enabled = false;
                btnPrintPOM.Enabled = true;
                btnErrorLog.Enabled = true;
                btnEnvelop.Enabled = true;
                statusCell.Style.Value = "text-decoration:line-through;";
                statusCell.Attributes["style"] = "color:red;font-weight:bold;text-decoration:line-through;";
            }
            else if (statusCell.Text == "Enqueued")
            {
                btnDownload.Enabled = false;
                btnRequeue.Enabled = false;
                btnPrintPOM.Enabled = false;
                btnErrorLog.Enabled = true;
                btnEnvelop.Enabled = false;
                statusCell.Attributes["style"] = "background-color: #FBD5CC;color:black;font-weight:bold";
            }
            else if (statusCell.Text == "Failed")
            {
                btnDownload.Enabled = false;
                btnRequeue.Enabled = true;
                btnPrintPOM.Enabled = false;
                btnErrorLog.Enabled = true;
                btnEnvelop.Enabled = false;
                statusCell.Attributes["style"] = "background-color: red;color:white;font-weight:bold";
            }
            else if (statusCell.Text == "Processing")
            {
                btnDownload.Enabled = false;
                btnRequeue.Enabled = false;
                btnPrintPOM.Enabled = false;
                btnErrorLog.Enabled = true;
                btnEnvelop.Enabled = false;
                statusCell.Attributes["style"] = "background-color: #F31802;color:white;font-weight:bold";
            }
            else if (statusCell.Text == "Scheduled")
            {
                btnDownload.Enabled = false;
                btnRequeue.Enabled = false;
                btnPrintPOM.Enabled = false;
                btnErrorLog.Enabled = true;
                btnEnvelop.Enabled = false;
                statusCell.Attributes["style"] = "background-color: #20F302;color:white;font-weight:bold";
            }
        }
    }

    protected void imgButton_Click(object sender, ImageClickEventArgs e)
    {
        BindGrid();
    }

    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}