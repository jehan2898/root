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
using System.Data.SqlClient;
using RequiredDocuments;
using SautinSoft;
using CUTEFORMCOLib;
public partial class Bill_Sys_VerificationReceivedOrSentReport : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    CaseDetailsBO _objCaseDetailsBO = new CaseDetailsBO();
    ReportDAO _ReportDAO;
    Bill_Sys_NF3_Template _nf3Template;
    Bill_Sys_BillTransaction_BO _billTransactionBO;
    SearchReportBO _SearchReportBO;
    string statusno;


    public void BindGrid()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            new DataSet();
            this.fillControl();
            this.grdvDenial.XGridBindSearch();
            this.Clear();
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

    protected void btnCancelDesc_Click(object sender, EventArgs e)
    {
        this.grdvDenial.XGridBind();
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        this.ExportToExcel();
    }

    protected void btnLitigantion_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            bool flag = false;
            if (this.hdlverisent.Value.Trim() != "false")
            {
                string[] strArray = this.hdlbillnumber.Value.Split(new char[] { ',' });
                ArrayList list = new ArrayList();
                for (int i = 0; i < this.grdvDenial.Rows.Count; i++)
                {
                    CheckBox box = (CheckBox)this.grdvDenial.Rows[i].FindControl("ChkLitigantion");
                    if (box.Checked)
                    {
                        new Bill_sys_litigantion();
                        string text = ((Label)this.grdvDenial.Rows[i].FindControl("lblBill")).Text;
                        foreach (string str2 in strArray)
                        {
                            if (text.Equals(str2) && !text.Equals(""))
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            list.Add(text);
                            flag = false;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
                if (list.Count > 0)
                {
                    new Bill_Sys_NF3_Template().UpdateLitigantion(list, this.txtCompanyID.Text);
                    this.usrMessage.PutMessage("Selected bills were sent for litigation");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                }
            }
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
        this.creatPDF();
        this.BindGrid();
        this.usrMessage.PutMessage("Verifiation POM report generated");
        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
        this.usrMessage.Show();
    }

    protected void btnPrintEnv_Click(object sender, EventArgs e)
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
            string str2 = ConfigurationManager.AppSettings["PROVIDERNAME"];
            string str3 = template.getPhysicalPath();
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            for (int i = 0; i < this.grdvDenial.Rows.Count; i++)
            {
                string str9 = this.grdvDenial.DataKeys[i]["SZ_CASE_ID"].ToString();
                CheckBox box = (CheckBox)this.grdvDenial.Rows[i].FindControl("ChkLitigantion");
                if (box.Checked)
                {
                    string text = ((TextBox)this.grdvDenial.Rows[i].FindControl("txtInsDetails")).Text;
                    string str11 = ((TextBox)this.grdvDenial.Rows[i].FindControl("txtInsAddress")).Text;
                    string str12 = ((TextBox)this.grdvDenial.Rows[i].FindControl("txtInsState")).Text;
                    string str13 = this.grdvDenial.DataKeys[i]["SZ_OFFICE_ID"].ToString();
                    string str14 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + str9 + "/Packet Document/";
                    string str15 = new ReplacePdfValues().PrintEnvelope1(str2, str, str13, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, str9, text, str11, str12);
                    if (str4 == "")
                    {
                        str4 = str15;
                        str5 = str3 + str14 + str4;
                        str8 = str14 + str4;
                    }
                    else
                    {
                        str6 = str15;
                        str7 = str3 + str14 + str6;
                        this.lfnMergePDF(str5, str7, str3 + str14 + "_" + str4);
                        str4 = "_" + str4;
                        str5 = str3 + str14 + str4;
                        str8 = str14 + str4;
                    }
                }
            }
            ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "starScript", "window.open('" + (ApplicationSettings.GetParameterValue("DocumentManagerURL") + str8).ToString() + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
            this.BindGrid();
            this.usrMessage.PutMessage("Verification print POM envelope generated");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
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

    protected void btnSaveDesc_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList list = new ArrayList();
            bool flag = false;
            for (int i = 0; i < this.grdvDenial.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdvDenial.Rows[i].FindControl("ChkLitigantion");
                if (box.Checked)
                {
                    string text = ((Label)this.grdvDenial.Rows[i].FindControl("lblBill")).Text;
                    Bill_Sys_Verification_Desc desc = new Bill_Sys_Verification_Desc();
                    desc.sz_bill_no=text;
                    desc.sz_verification_date=DBNull.Value.ToString();
                    desc.i_verification=2;
                    desc.sz_company_id=this.txtCompanyID.Text;
                    desc.sz_user_id=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                    desc.sz_flag="VS";
                    list.Add(desc);
                    flag = true;
                }
            }
            if (flag)
            {
                new Bill_Sys_BillTransaction_BO().InsertUpdateBillStatus(list);
                this.usrMessage.PutMessage("Selected bill sended to verification sent");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSaveSendRequest_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = "";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            for (int i = 0; i < this.grdVerificationSend.Items.Count; i++)
            {
                Bill_Sys_Verification_Desc desc;
                TextBox box = (TextBox)this.grdVerificationSend.Items[i].FindControl("taxAns");
                if (box.Text.Trim().ToString() != "")
                {
                    desc = new Bill_Sys_Verification_Desc();
                    desc.sz_answer=box.Text;
                    desc.sz_bill_no=this.grdVerificationSend.Items[i].Cells[0].Text.ToString();
                    desc.sz_verification_id=this.grdVerificationSend.Items[i].Cells[8].Text.ToString();
                    desc.sz_case_id=this.grdVerificationSend.Items[i].Cells[9].Text.ToString();
                    list.Add(desc);
                    if (str == "")
                    {
                        str = "'" + this.grdVerificationSend.Items[i].Cells[0].Text.ToString() + "'";
                    }
                    else
                    {
                        str = str + ",'" + this.grdVerificationSend.Items[i].Cells[0].Text.ToString() + "'";
                    }
                }
                else
                {
                    desc = new Bill_Sys_Verification_Desc();
                    desc.sz_bill_no=this.grdVerificationSend.Items[i].Cells[0].Text.ToString();
                    desc.sz_verification_id=this.grdVerificationSend.Items[i].Cells[8].Text.ToString();
                    desc.sz_company_id=this.txtCompanyID.Text;
                    desc.sz_answer_id=this.grdVerificationSend.Items[i].Cells[7].Text.ToString();
                    list2.Add(desc);
                }
            }
            Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
            if (list2.Count > 0)
            {
                try
                {
                    new Bill_Sys_BillTransaction_BO().DeleteVerificationAns(list2);
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
            }
            if (list.Count > 0)
            {
                if (template.SetVerification_Answer(list, this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME) == 1)
                {
                    this.usrMessage1.PutMessage("Saved successfully.");
                    this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage1.Show();
                    DataSet set = new DataSet();
                    set = template.GetVerification_Answer(str, this.txtCompanyID.Text);
                    this.grdVerificationSend.DataSource = set.Tables[0];
                    this.grdVerificationSend.DataBind();
                }
                else
                {
                    this.usrMessage1.PutMessage("Error in transaction");
                    this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    this.usrMessage1.Show();
                }
            }
            else if ((list2.Count <= 0) && (list.Count <= 0))
            {
                this.usrMessage1.PutMessage("add atleast one answer...");
                this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage1.Show();
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

    protected void btnSearch_Click1(object sender, EventArgs e)
    {//Logging Start
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnVeriSent_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = "";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            for (int i = 0; i < this.grdvDenial.Rows.Count; i++)
            {
                Bill_Sys_Verification_Desc desc;
                TextBox box = (TextBox)this.grdvDenial.Rows[i].FindControl("txtAnswer");
                if (box.Text.Trim().ToString() != "")
                {
                    desc = new Bill_Sys_Verification_Desc();
                    desc.sz_answer=box.Text;
                    desc.sz_bill_no=this.grdvDenial.DataKeys[i]["SZ_BILL_NUMBER"].ToString();
                    desc.sz_verification_id=this.grdvDenial.DataKeys[i]["I_VERIFICATION_ID"].ToString();
                    desc.sz_case_id=this.grdvDenial.DataKeys[i]["SZ_CASE_ID"].ToString();
                    list.Add(desc);
                    if (str == "")
                    {
                        str = "'" + this.grdvDenial.DataKeys[i]["SZ_BILL_NUMBER"].ToString() + "'";
                    }
                    else
                    {
                        str = str + ",'" + this.grdvDenial.DataKeys[i]["SZ_BILL_NUMBER"].ToString() + "'";
                    }
                }
                else
                {
                    desc = new Bill_Sys_Verification_Desc();
                    desc.sz_bill_no=this.grdvDenial.DataKeys[i]["SZ_BILL_NUMBER"].ToString();
                    desc.sz_verification_id=this.grdvDenial.DataKeys[i]["I_VERIFICATION_ID"].ToString();
                    desc.sz_company_id=this.txtCompanyID.Text;
                    desc.sz_answer_id=this.grdvDenial.DataKeys[i]["SZ_ANS_ID"].ToString();
                    list2.Add(desc);
                }
            }
            Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
            if (list2.Count > 0)
            {
                try
                {
                    new Bill_Sys_BillTransaction_BO().DeleteVerificationAns(list2);
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
                    base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

                }
            }
            if (list.Count > 0)
            {
                if (template.SetVerification_Answer(list, this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME) == 1)
                {
                    this.usrMessage2.PutMessage("Saved successfully.");
                    this.usrMessage2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage2.Show();
                    DataSet set = new DataSet();
                    set = template.GetVerification_Answer(str, this.txtCompanyID.Text);
                    this.grdVerificationSend.DataSource = set.Tables[0];
                    this.grdVerificationSend.DataBind();
                    this.BindGrid();
                }
                else
                {
                    this.usrMessage2.PutMessage("Error in transaction");
                    this.usrMessage2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage2.Show();
                }
            }
            else if ((list2.Count <= 0) && (list.Count <= 0))
            {
                this.usrMessage2.PutMessage("add atleast one answer...");
                this.usrMessage2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage2.Show();
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
        this.creatPDF();
        this.BindGrid();
        this.usrMessage.PutMessage("Verifiation POM report generated");
        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
        this.usrMessage.Show();
    }

    public void Clear()
    {
        this.txtDay.Text = "";
        this.txtFromDate.Text = "";
        this.txtToDate.Text = "";
        this.txtBillNo.Text = "";
        this.txtCaseNo.Text = "";
        this.txtPatientName.Text = "";
        this.utxtCaseType.Text = "";
    }

    private DataSet CreateGroupData()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet set2=new DataSet();
        try
        {
            DataSet set = new DataSet();
            DataTable table = new DataTable();
            table.Columns.Add("Bill No");
            table.Columns.Add("Case Id");
            table.Columns.Add("Patient Name");
            table.Columns.Add("Speciality");
            table.Columns.Add("Reffering Office");
            table.Columns.Add("Bill Amount");
            table.Columns.Add("Bill Date");
            table.Columns.Add("Insurance Claim No");
            table.Columns.Add("Insurance Company");
            table.Columns.Add("Insurance Address");
            table.Columns.Add("Min Date Of Service");
            table.Columns.Add("Max Date Of Service");
            table.Columns.Add("CaseType Id");
            table.Columns.Add("WC_ADDRESS");
            table.Columns.Add("Case No");
            table.Columns.Add("InsDescription");
            for (int i = 0; i < this.grdvDenial.Rows.Count; i++)
            {
                if (((CheckBox)this.grdvDenial.Rows[i].FindControl("ChkLitigantion")).Checked)
                {
                    string text = ((TextBox)this.grdvDenial.Rows[i].FindControl("txtInsDetails")).Text;
                    string str2 = ((TextBox)this.grdvDenial.Rows[i].FindControl("txtInsAddress")).Text;
                    string str3 = ((TextBox)this.grdvDenial.Rows[i].FindControl("txtInsState")).Text;
                    string str4 = ((TextBox)this.grdvDenial.Rows[i].FindControl("txtInsDesc")).Text;
                    DataRow row = table.NewRow();
                    row["Case No"] = this.grdvDenial.Rows[i].Cells[1].Text;
                    row["Bill No"] = this.grdvDenial.DataKeys[i]["SZ_BILL_NUMBER"].ToString();
                    row["Case Id"] = this.grdvDenial.DataKeys[i]["SZ_CASE_ID"].ToString();
                    row["Patient Name"] = this.grdvDenial.DataKeys[i]["SZ_PATIENT_NAME"].ToString();
                    row["Speciality"] = this.grdvDenial.DataKeys[i]["SZ_SPECIALITY"].ToString();
                    row["Reffering Office"] = this.grdvDenial.DataKeys[i]["SZ_COMPANY_NAME"].ToString();
                    row["Bill Amount"] = this.grdvDenial.DataKeys[i]["FLT_BILL_AMOUNT"].ToString();
                    row["Bill Date"] = this.grdvDenial.Rows[i].Cells[2].Text;
                    row["Insurance Claim No"] = this.grdvDenial.DataKeys[i]["SZ_CLAIM_NUMBER"].ToString();
                    if ((!str2.Equals("") || !text.Equals("")) || !str3.Equals(""))
                    {
                        string str5 = "<b>" + text + "</b>";
                        string str6 = str2 + "<br/>" + str3;
                        row["Insurance Company"] = str5;
                        row["Insurance Address"] = str6;
                    }
                    else
                    {
                        row["Insurance Company"] = this.grdvDenial.Rows[i].Cells[6].Text;
                        row["Insurance Address"] = this.grdvDenial.DataKeys[i]["SZ_INSURANCE_ADDRESS"].ToString();
                    }
                    row["Min Date Of Service"] = this.grdvDenial.DataKeys[i]["SZ_MIN_SERVICE_DATE"].ToString();
                    row["Max Date Of Service"] = this.grdvDenial.DataKeys[i]["SZ_MAX_SERVICE_DATE"].ToString();
                    row["CaseType Id"] = this.grdvDenial.DataKeys[i]["SZ_CASE_TYPE_ID"].ToString();
                    row["WC_ADDRESS"] = this.grdvDenial.DataKeys[i]["WC_ADDRESS"].ToString();
                    if (!str4.Equals(""))
                    {
                        row["InsDescription"] = str4;
                    }
                    else
                    {
                        row["InsDescription"] = "";
                    }
                    table.Rows.Add(row);
                }
            }
            if (table.Rows.Count > 0)
            {
                DataView defaultView = table.DefaultView;
                defaultView.Sort = "Reffering Office";
                table = defaultView.ToTable();
            }
            set.Tables.Add(table);
            set2 = set;
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
        return set2;

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
        int num = 0;
        string str4 = "";
        string szInput = "";
        string str6 = "";
        string str7 = "";
        int num2 = 0;
        int num3 = 0;
        int num4 = 1;
        string str8 = "";
        string str9 = "";
        string str10 = "";
        Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
        StringBuilder builder = new StringBuilder();
        if (this.hdnPOMValue.Value.Equals("Yes"))
        {
            this._reportBO = new Bill_Sys_ReportBO();
            num = this._reportBO.POMSave(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, 0);
        }
        builder.AppendLine("");
        int num5 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        int num6 = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        try
        {
            template.getPhysicalPath();
            string str11 = ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET");
            int num7 = 0;
            DataSet set = this.CreateGroupData();
            int num8 = 0;
            int num9 = 0;
            string str12 = null;
            string str13 = null;
            bool flag = false;
            szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
            str6 = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
            int num10 = 0;
            int num11 = 0;
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                str13 = set.Tables[0].Rows[i]["CaseType Id"].ToString();
                if (str13 == "WC000000000000000001")
                {
                    builder.AppendLine("");
                    if (num7 == 0)
                    {
                        str12 = set.Tables[0].Rows[i]["Reffering Office"].ToString();
                        num7 = 1;
                        str8 = set.Tables[0].Rows[i]["Bill No"].ToString();
                        num9 = 0;
                    }
                    if (str12.Equals(set.Tables[0].Rows[i]["Reffering Office"].ToString()))
                    {
                        num2++;
                        num3++;
                        if (num9 == num6)
                        {
                            builder.Append(this.ReplaceHeaderAndFooter(str6, str7, num));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num9 = 1;
                            num10 = 1;
                        }
                        if (flag)
                        {
                            str6 = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            flag = false;
                        }
                        object obj2 = szInput;
                        szInput = string.Concat(new object[] { 
                            obj2, "<tr><td style='font-size:9px'>", num2.ToString(), "</td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Insurance Claim No"], "</td><td style='font-size:9px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Speciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:9px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />", 
                            set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Bill No"], "</td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case No"], "</td></tr>"
                         });
                        num11++;
                        object obj3 = str6;
                        str6 = string.Concat(new object[] { 
                            obj3, "<tr><td style='font-size:9px'>", num3.ToString(), "</td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Insurance Claim No"], "</td><td style='font-size:9px'><b> Worker's Compensation Board </b><br/>", set.Tables[0].Rows[i]["WC_ADDRESS"], "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Speciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:9px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:9px'></td><td style='font-size:9px'>", 
                            set.Tables[0].Rows[i]["Bill No"], "</td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case No"], "</td></tr>"
                         });
                        num10++;
                        num9++;
                        str8 = set.Tables[0].Rows[i]["Bill No"].ToString();
                        str9 = set.Tables[0].Rows[i]["Bill No"].ToString();
                    }
                    else
                    {
                        num2 = 1;
                        num3 = 1;
                        if (num10 > 0)
                        {
                            str6 = str6 + "</table>";
                            builder.Append(this.ReplaceHeaderAndFooter(str6, str8, num));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num3 = 1;
                        }
                        if (flag)
                        {
                            str6 = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            flag = false;
                        }
                        szInput = szInput + "</table>";
                        builder.Append(this.ReplaceHeaderAndFooter(szInput, str9, num));
                        builder.AppendLine("<span style='page-break-after:always'></span>");
                        str12 = set.Tables[0].Rows[i]["Reffering Office"].ToString();
                        str8 = set.Tables[0].Rows[i]["Bill No"].ToString();
                        str9 = set.Tables[0].Rows[i]["Bill No"].ToString();
                        szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                        object obj4 = szInput;
                        szInput = string.Concat(new object[] { 
                            obj4, "<tr><td style='font-size:9px'>", num2.ToString(), "</td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Insurance Claim No"], "</td><td style='font-size:9px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Speciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:9px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />", 
                            set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Bill No"], "</td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case No"], "</td></tr>"
                         });
                        num11++;
                        object obj5 = str6;
                        str6 = string.Concat(new object[] { 
                            obj5, "<tr><td style='font-size:9px'>", num3.ToString(), "</td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Insurance Claim No"], "</td><td style='font-size:9px'><b> Worker's Compensation Board </b><br/>", set.Tables[0].Rows[i]["WC_ADDRESS"], "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Speciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:9px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />", set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:9px'></td><td style='font-size:9px'>", 
                            set.Tables[0].Rows[i]["Bill No"], "</td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case No"], "</td></tr>"
                         });
                        num10++;
                        num4++;
                        num9 = 1;
                        num8 = 1;
                        num11 = 1;
                    }
                }
                else if (str13 != "WC000000000000000001")
                {
                    num8 = num11;
                    builder.AppendLine("");
                    if (num7 == 0)
                    {
                        str12 = set.Tables[0].Rows[i]["Reffering Office"].ToString();
                        num7 = 1;
                        str9 = set.Tables[0].Rows[i]["Bill No"].ToString();
                        num8 = 0;
                    }
                    if (str12.Equals(set.Tables[0].Rows[i]["Reffering Office"].ToString()))
                    {
                        num2++;
                        if (num8 == num5)
                        {
                            builder.Append(this.ReplaceHeaderAndFooter(szInput, set.Tables[0].Rows[i]["Bill No"].ToString(), num));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            num8 = 0;
                            num11 = 0;
                        }
                        object obj6 = szInput;
                        szInput = string.Concat(new object[] { 
                            obj6, "<tr><td style='font-size:9px'>", num2.ToString(), "</td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Insurance Claim No"], "</td><td style='font-size:9px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Speciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:9px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />", 
                            set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Bill No"], "</td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case No"], "</td></tr>"
                         });
                        num11++;
                        num8++;
                    }
                    else
                    {
                        num2 = 1;
                        str12 = set.Tables[0].Rows[i]["Reffering Office"].ToString();
                        szInput = szInput + "</table>";
                        if (num10 > 0)
                        {
                            str6 = str6 + "</table>";
                            builder.Append(this.ReplaceHeaderAndFooter(str6, str8, num));
                            builder.AppendLine("<span style='page-break-after:always'></span>");
                            flag = true;
                            num3 = 0;
                        }
                        builder.Append(this.ReplaceHeaderAndFooter(szInput, str9, num));
                        builder.AppendLine("<span style='page-break-after:always'></span>");
                        szInput = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                        object obj7 = szInput;
                        szInput = string.Concat(new object[] { 
                            obj7, "<tr><td style='font-size:9px'>", num2.ToString(), "</td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Insurance Claim No"], "</td><td style='font-size:9px'><b>", set.Tables[0].Rows[i]["Insurance Company"], "</b><br/>", set.Tables[0].Rows[i]["Insurance Address"], "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Speciality"], "<br />", set.Tables[0].Rows[i]["Min Date Of Service"], "</td><td style='font-size:9px' align='right'> $", set.Tables[0].Rows[i]["Bill Amount"], "<br />", 
                            set.Tables[0].Rows[i]["Max Date Of Service"], "</td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Bill No"], "</td><td style='font-size:9px'></td><td style='font-size:9px'>", set.Tables[0].Rows[i]["Patient Name"], ' ', set.Tables[0].Rows[i]["Case No"], "</td></tr>"
                         });
                        num11++;
                        num4++;
                        str9 = set.Tables[0].Rows[i]["Bill No"].ToString();
                        num8 = 1;
                        num11 = 1;
                    }
                }
            }
            string str14 = "";
            if (!flag && (num10 > 0))
            {
                str6 = str6 + "</table>";
                builder.AppendLine(this.ReplaceHeaderAndFooter(str6, str8, num));
                builder.AppendLine("<span style='page-break-after:always'></span>");
            }
            if (num11 > 0)
            {
                str14 = szInput + "</table>";
                builder.AppendLine(this.ReplaceHeaderAndFooter(str14, str9, num));
            }
            string str15 = builder.ToString();
            PdfMetamorphosis metamorphosis = new PdfMetamorphosis();
            metamorphosis.Serial="10007706603";
            string str16 = this.getFileName("P") + ".htm";
            str4 = this.getFileName("P") + ".pdf";
            string str17 = ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str4;
            StreamWriter writer = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str16);
            writer.Write(str15);
            writer.Close();
            metamorphosis.HtmlToPdfConvertFile(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str16, ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str4);
            if (this.hdnPOMValue.Value == "Yes")
            {
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                ArrayList list3 = new ArrayList();
                bool flag2 = false;
                string str18 = "";
                for (int j = 0; j < set.Tables[0].Rows.Count; j++)
                {
                    list3.Add(set.Tables[0].Rows[j]["Bill No"].ToString());
                    int num14 = 0;
                    if (list.Count == 0)
                    {
                        num14 = 1;
                    }
                    for (int k = 0; k < list.Count; k++)
                    {
                        list[0].ToString();
                        set.Tables[0].Rows[j]["Case Id"].ToString();
                        if (!list[k].ToString().Equals(set.Tables[0].Rows[j]["Case Id"].ToString()))
                        {
                            num14 = 1;
                        }
                        else
                        {
                            num14 = 0;
                            break;
                        }
                    }
                    if (num14 == 1)
                    {
                        ListItem item = new ListItem();
                        item.Text = set.Tables[0].Rows[j]["Case Id"].ToString();
                        list.Add(item);
                        this._reportBO = new Bill_Sys_ReportBO();
                        this._nf3Template = new Bill_Sys_NF3_Template();
                        this._billTransactionBO = new Bill_Sys_BillTransaction_BO();
                        string str19 = this._billTransactionBO.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "NFPRO");
                        string path = "";
                        string str21 = this._nf3Template.getPhysicalPath();
                        new Bill_Sys_RequiredDocumentBO();
                        string companyName = "";
                        companyName = this._nf3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        companyName = string.Concat(new object[] { companyName, @"\POM Generate\", num, @"\" }).Replace(@"\", "/");
                        str10 = companyName;
                        path = str21 + companyName;
                        string text1 = str21 + companyName;
                        if (!flag2)
                        {
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            path = str21 + companyName + "/" + str4;
                            string sourceFileName = str17;
                            File.Copy(sourceFileName, path);
                            flag2 = true;
                        }
                        ArrayList list4 = new ArrayList();
                        list4.Add(set.Tables[0].Rows[j]["Case Id"].ToString());
                        list4.Add("");
                        list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list4.Add(str4.ToString());
                        list4.Add(companyName.ToString());
                        list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list4.Add(str19);
                        str18 = this._nf3Template.SaveDocumentData(list4);
                        list2.Add(str18);
                    }
                }
                this._reportBO.POMEntry(num, DateTime.Today.ToString("MM/dd/yyyy"), Convert.ToInt32(list2[0].ToString()), str4.ToString(), str10 + "/", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, list3, "POMG");
                this.BindGrid();
            }
            string text2 = str11 + str4;
            if (str == "")
            {
                str = str4;
                string text3 = str11 + str;
                str3 = str11 + str;
            }
            else
            {
                str2 = str4;
                string text4 = str11 + str2;
            }
            str3 = str11 + str;
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str3.ToString() + "'); ", true);
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

    private void ExportToExcel()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<table border='1px'>");
            for (int i = 0; i < this.grdvDenial.Rows.Count; i++)
            {
                if (i == 0)
                {
                    builder.Append("<tr>");
                    for (int k = 0; k < (this.grdvDenial.Columns.Count - 2); k++)
                    {
                        if (this.grdvDenial.Columns[k].Visible)
                        {
                            builder.Append("<td>");
                            builder.Append(this.grdvDenial.Columns[k].HeaderText);
                            builder.Append("</td>");
                        }
                    }
                    builder.Append("</tr>");
                }
                builder.Append("<tr>");
                for (int j = 0; j < (this.grdvDenial.Columns.Count - 2); j++)
                {
                    if (this.grdvDenial.Columns[j].Visible && (j == 2))
                    {
                        builder.Append("<td>");
                        builder.Append(this.grdvDenial.Rows[i].Cells[this.grdvDenial.Columns.Count - 1].Text);
                        builder.Append("</td>");
                    }
                    else if (this.grdvDenial.Columns[j].Visible)
                    {
                        builder.Append("<td>");
                        builder.Append(this.grdvDenial.Rows[i].Cells[j].Text);
                        builder.Append("</td>");
                    }
                }
                builder.Append("</tr>");
            }
            builder.Append("</table>");
            string str = this.getFileName("EXCEL") + ".xls";
            StreamWriter writer = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + str);
            writer.Write(builder);
            writer.Close();
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + str + "';", true);
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

    public void fillControl()
    {
        this.txtBillStatus.Visible = true;
        this.txtPatientName.Visible = true;
        this.txtFlag.Visible = true;
        this.txtDay.Visible = true;
        this.txtDay.Visible = true;
        this.txtCompanyID.Visible = true;
        this.txtBillNo.Visible = true;
        this.txtCaseNo.Visible = true;
        this.txtFromDate.Visible = true;
        this.txtToDate.Visible = true;
        this.txtINS.Text = this.txtINS1.Text;
        this.txtOffice.Text = this.txtOffice1.Text;

        if (this.rblView.SelectedValue.ToString().Equals("0"))
        {
            this.statusno = "RVR";
        }
        else if (this.rblView.SelectedValue.ToString().Equals("1"))
        {
            this.statusno = "RVS";
        }
        else if (this.rblView.SelectedValue.ToString().Equals("2"))
        {
            this.statusno = "POD";
        }
        this.txtBillStatus.Text = this.statusno;
        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
        {
            this.txtFlag.Text = "REF";
        }
        else
        {
            this.txtFlag.Text = "BILLING";
        }
        if (this.txtDay.Text != "")
        {
            DateTime time = new DateTime();
            this.txtDay.Text = DateTime.Now.AddDays((double)-Convert.ToInt32(this.txtDay.Text)).ToString("MM/dd/yyyy");
        }

        this.txtBillNo.Text = this.txtupdateBillNo.Text;
        this.txtCaseNo.Text = this.txtupdateCaseNo.Text;
        this.txtFromDate.Text = this.txtupdateFromDate.Text;
        this.txtToDate.Text = this.txtupdateToDate.Text;
        this.txtPatientName.Text = this.txtupdatePatientName.Text;
        this.utxtCaseType.Text = this.extddlCaseType.Text;
        this.hdlbillnumber.Value = "";
        this.hdlverisent.Value = "";
        this.txtBillStatus.Visible = false;
        this.txtDay.Visible = false;
        this.txtCompanyID.Visible = false;
        this.txtFlag.Visible = false;
        this.txtBillNo.Visible = false;
        this.txtCaseNo.Visible = false;
        this.txtDay.Visible = false;
        this.txtFromDate.Visible = false;
        this.txtToDate.Visible = false;
        this.txtPatientName.Visible = false;
    }

    public DataSet GetDenialInfo(string sz_CompanyID, string SZ_BILL_NUMBER)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_MANAGE_GET_DENIAL_REASON", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            selectCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_BILL_NUMBER);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
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
        string str2="";
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        connection.Open();
        GeneratePatientInfoPDF opdf = new GeneratePatientInfoPDF();
        try
        {
            string str = "";
            str = p_htmlstring;
            SqlCommand command = new SqlCommand("SP_TEST_FACILITY_MAILS_DETAILS", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            command.Parameters.AddWithValue("@SZ_BILL_ID", sz_bill_id);
            SqlDataReader reader = command.ExecuteReader();
            str2 = opdf.ReplaceNF2MailDetails(str, reader);
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
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return str2;

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

    public DataSet GetVerification(string sz_CompanyID, string sz_input_bill_number)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        connection = new SqlConnection(connectionString);
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_MANAGE_VERIFICATION_ANSWER", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            selectCommand.Parameters.AddWithValue("@sz_input_bill_number", sz_input_bill_number);
            selectCommand.Parameters.AddWithValue("@bt_operation", 2);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
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

    protected void grdvDenial_RowCommand(object sender, GridViewCommandEventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int num = 0;
        try
        {
            if (e.CommandName.ToString() == "PLS")
            {
                DataTable table = new DataTable();
                table.Columns.Add("SZ_DOCTOR_NAME");
                table.Columns.Add("DT_VISIT_DATE");
                table.Columns.Add("FLT_BILL_AMOUNT");
                table.Columns.Add("PAID_AMOUNT");
                table.Columns.Add("FLT_BALANCE");
                table.Columns.Add("SZ_VERIFICATION_USER");
                table.Columns.Add("SZ_VERIFICATION_DATE");
                table.Columns.Add("SZ_ANS_USER");
                for (int i = 0; i < this.grdvDenial.Rows.Count; i++)
                {
                    LinkButton button = (LinkButton)this.grdvDenial.Rows[i].FindControl("lnkM");
                    LinkButton button2 = (LinkButton)this.grdvDenial.Rows[i].FindControl("lnkP");
                    if (button.Visible)
                    {
                        button.Visible = false;
                        button2.Visible = true;
                    }
                }
                this.grdvDenial.Columns[0x1c].Visible = true;
                num = Convert.ToInt32(e.CommandArgument);
                string str = "div";
                str = str + this.grdvDenial.DataKeys[num][2].ToString() + num;
                GridView view = (GridView)this.grdvDenial.Rows[num].FindControl("grdVerification");
                LinkButton button3 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkP");
                LinkButton button4 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkM");
                DataRow row = table.NewRow();
                row["SZ_DOCTOR_NAME"] = this.grdvDenial.DataKeys[num]["SZ_DOCTOR_NAME"].ToString();
                row["DT_VISIT_DATE"] = this.grdvDenial.DataKeys[num]["DT_VISIT_DATE"].ToString();
                row["FLT_BILL_AMOUNT"] = "$" + this.grdvDenial.DataKeys[num]["FLT_BILL_AMOUNT"].ToString();
                row["PAID_AMOUNT"] = "$" + this.grdvDenial.DataKeys[num]["PAID_AMOUNT"].ToString();
                row["FLT_BALANCE"] = "$" + this.grdvDenial.DataKeys[num]["FLT_BALANCE"].ToString();
                row["SZ_VERIFICATION_USER"] = this.grdvDenial.DataKeys[num]["SZ_VERIFICATION_USER"].ToString();
                DateTime time = new DateTime();
                string str2 = "";
                if ((this.grdvDenial.DataKeys[num]["SZ_VERIFICATION_DATE"].ToString() != "") && (this.grdvDenial.DataKeys[num]["SZ_VERIFICATION_DATE"].ToString() != "NA"))
                {
                    try
                    {
                        str2 = Convert.ToDateTime(this.grdvDenial.DataKeys[num]["SZ_VERIFICATION_DATE"].ToString()).ToString("MM/dd/yyyy");
                    }
                    catch (Exception ex)
                    {
                        str2 = "";
                    }
                }
                row["SZ_VERIFICATION_DATE"] = str2;
                row["SZ_ANS_USER"] = this.grdvDenial.DataKeys[num]["SZ_ANS_USER"].ToString();
                table.Rows.Add(row);
                DataSet set = new DataSet();
                set.Tables.Add(table);
                view.DataSource = set;
                view.DataBind();
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mp", "ShowChildGrid('" + str + "') ;", true);
                button3.Visible = false;
                button4.Visible = true;
            }
            if (e.CommandName.ToString() == "MNS")
            {
                this.grdvDenial.Columns[20].Visible = false;
                num = Convert.ToInt32(e.CommandArgument);
                string str3 = "div";
                str3 = str3 + this.grdvDenial.DataKeys[num][2].ToString() + num;
                LinkButton button5 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkP");
                LinkButton button6 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkM");
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", "HideChildGrid('" + str3 + "') ;", true);
                button5.Visible = true;
                button6.Visible = false;
            }
            if (e.CommandName.ToString() == "DenialPLS")
            {
                for (int j = 0; j < this.grdvDenial.Rows.Count; j++)
                {
                    LinkButton button7 = (LinkButton)this.grdvDenial.Rows[j].FindControl("lnkDM");
                    LinkButton button8 = (LinkButton)this.grdvDenial.Rows[j].FindControl("lnkDP");
                    if (button7.Visible)
                    {
                        button7.Visible = false;
                        button8.Visible = true;
                    }
                }
                this.grdvDenial.Columns[20].Visible = true;
                this.grdvDenial.Columns[0x13].Visible = false;
                num = Convert.ToInt32(e.CommandArgument);
                string str4 = "div1";
                str4 = str4 + this.grdvDenial.DataKeys[num][2].ToString() + num;
                GridView view2 = (GridView)this.grdvDenial.Rows[num].FindControl("grdDenial");
                LinkButton button9 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkDP");
                LinkButton button10 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkDM");
                string str5 = this.grdvDenial.DataKeys[num][2].ToString();
                string text = this.txtCompanyID.Text;
                DataSet denialInfo = this.GetDenialInfo(text, str5);
                view2.DataSource = denialInfo;
                view2.DataBind();
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mp", "ShowDenialChildGrid('" + str4 + "') ;", true);
                button9.Visible = false;
                button10.Visible = true;
                LinkButton button11 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkM");
                LinkButton button12 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkP");
                if (button11.Visible)
                {
                    button11.Visible = false;
                    button12.Visible = true;
                }
            }
            if (e.CommandName.ToString() == "DenialMNS")
            {
                this.grdvDenial.Columns[0x15].Visible = false;
                this.grdvDenial.Columns[20].Visible = false;
                num = Convert.ToInt32(e.CommandArgument);
                string str7 = "div";
                str7 = str7 + this.grdvDenial.DataKeys[num][2].ToString() + num;
                LinkButton button13 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkDP");
                LinkButton button14 = (LinkButton)this.grdvDenial.Rows[num].FindControl("lnkDM");
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", "HideDenialChildGrid('" + str7 + "') ;", true);
                button13.Visible = true;
                button14.Visible = false;
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

    protected void grdvVerificationSent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Sort"))
        {
            if (this.ViewState["SortExp"] == null)
            {
                this.ViewState["SortExp"] = e.CommandArgument.ToString();
                this.ViewState["SortOrder"] = "ASC";
                DataSet set = new DataSet();
                set = (DataSet)this.Session["Grid"];
                DataView defaultView = set.Tables[0].DefaultView;
                defaultView.Sort = this.ViewState["SortExp"] + " " + this.ViewState["SortOrder"];
                this.grdvDenial.DataSource = defaultView;
                this.grdvDenial.DataBind();
            }
            else if (this.ViewState["SortExp"].ToString() == e.CommandArgument.ToString())
            {
                if (this.ViewState["SortOrder"].ToString() == "ASC")
                {
                    this.ViewState["SortOrder"] = "DESC";
                }
                else
                {
                    this.ViewState["SortOrder"] = "ASC";
                }
            }
            else
            {
                this.ViewState["SortOrder"] = "ASC";
                this.ViewState["SortExp"] = e.CommandArgument.ToString();
            }
        }
    }

    protected void grdvVerificationSent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView view = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            int index = -1;
            foreach (DataControlField field in view.Columns)
            {
                e.Row.Cells[view.Columns.IndexOf(field)].CssClass = "headerstyle";
                if (field.SortExpression == view.SortExpression)
                {
                    index = view.Columns.IndexOf(field);
                }
            }
            if (index > -1)
            {
                e.Row.Cells[index].CssClass = (view.SortDirection == SortDirection.Ascending) ? "sortascheaderstyle" : "sortdescheaderstyle";
            }
        }
    }

    protected void grdvVerificationSent_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private string lfnMergePDF(string p_szSource1, string p_szSource2, string p_szDestinationFileName)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str2="";
        try
        {
            CutePDFDocumentClass class2 = new CutePDFDocumentClass();
            int num = 0;
            string str = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
            class2.initialize(str);
            if (((class2 != null) && File.Exists(p_szSource1)) && File.Exists(p_szSource2))
            {
                num = class2.mergePDF(p_szSource1, p_szSource2, p_szDestinationFileName);
            }
            if (num == 0)
            {
                return "FAIL";
            }
            str2 = "SUCCESS";
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
        return str2;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + this.grdvDenial.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET").ToString()) + "';", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        this.btnLitigantion.Attributes.Add("onclick", "return ChecklitgationDate()");
        this.btnSearch.Attributes.Add("onclick", "return chkrdo()");
        this.btnSendMail.Attributes.Add("onclick", "return POMConformation()");
        this.Clear();
        try
        {
            this.txtCompanyID.Visible = true;
            this.txtFlag.Visible = true;
            this.txtFromDate.Visible = true;
            this.txtToDate.Visible = true;
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.txtFlag.Text = "REF";
                this.grdvDenial.Columns[3].Visible = true;
            }
            else
            {
                this.txtFlag.Text = "BILLING";
                this.grdvDenial.Columns[2].Visible = true;
            }
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlCaseType.Flag_ID = this.txtCompanyID.Text;
            this.fillControl();
            this.con.SourceGrid=this.grdvDenial;
            this.txtSearchBox.SourceGrid=this.grdvDenial;
            this.grdvDenial.Page=this.Page;
            this.grdvDenial.PageNumberList=this.con;
            bool isPostBack = base.IsPostBack;
            this.txtCompanyID.Visible = false;
            this.txtFlag.Visible = false;
            this.txtToDate.Visible = false;
            this.txtFromDate.Visible = false;
            this.ajAutoIns.ContextKey=this.txtCompanyID.Text;
            this.ajAutoOffice.ContextKey=this.txtCompanyID.Text;
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

    protected void Page_PreRender(object sender, EventArgs e)
    {
        for (int i = 0; i < this.grdvDenial.Rows.Count; i++)
        {
            string str = this.grdvDenial.DataKeys[i]["status_code"].ToString();
            CheckBox box = (CheckBox)this.grdvDenial.Rows[i].FindControl("ChkLitigantion");
            switch (str)
            {
                case "TRF":
                case "DNL":
                    box.Enabled = false;
                    break;

                default:
                    box.Enabled = true;
                    break;
            }
            string str2 = this.grdvDenial.DataKeys[i]["BT_PAYMENT"].ToString();
            LinkButton button = (LinkButton)this.grdvDenial.Rows[i].FindControl("lnkPayment");
            if (str2 == "1")
            {
                button.Visible = true;
            }
            else
            {
                button.Visible = false;
            }
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
            str = this.getNF2MailDetails(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, sz_bill_no);
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
}
