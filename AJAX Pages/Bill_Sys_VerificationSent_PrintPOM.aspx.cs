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
using RequiredDocuments;
using System.Data.Sql;
using System.Data.SqlClient;


public partial class Bill_Sys_VerificationSent_PrintPOM : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    CaseDetailsBO _objCaseDetailsBO = new CaseDetailsBO();
    ReportDAO _ReportDAO;
    SearchReportBO _SearchReportBO;
    Bill_Sys_NF3_Template _nf3Template;
     Bill_Sys_BillTransaction_BO _billTransactionBO ;
     String strsqlCon;
     SqlConnection sqlCon;
     SqlCommand sqlCmd;
     SqlDataAdapter sqlda;
     SqlDataReader dr;
     DataSet ds;


    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Span2.InnerHtml = "Is this the final POM?";
        btnSendMail.Attributes.Add("onclick", "return POMConformation();");

        btnPrintEnv.Attributes.Add("onclick", "return confirm_check();");
        
        ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        //Clear();
        //txtBillStatus.Text = "VR";
        try
        {
            //txtCompanyID.Visible = true;
            //txtFlag.Visible = true;
            //txtFromDate.Visible = true;
            //txtToDate.Visible = true;
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.con.SourceGrid = grdVerReceive;
            this.txtSearchBox.SourceGrid = grdVerReceive;
            this.grdVerReceive.Page = this.Page;
            this.grdVerReceive.PageNumberList = this.con;
            fillControl();
            if (!IsPostBack)
            {

                grdVerReceive.XGridBindSearch();
                //BindGrid();
            }
            //txtCompanyID.Visible = false;
            //txtFlag.Visible = false;
            //txtToDate.Visible = false;
            //txtFromDate.Visible = false;
           
        }
        catch (Exception ex)
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

    protected void grdvVerificationSent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gridView = (GridView)sender;

        if (e.Row.RowType == DataControlRowType.Header)
        {
            int cellIndex = -1;
            foreach (DataControlField field in gridView.Columns)
            {
                e.Row.Cells[gridView.Columns.IndexOf(field)].CssClass = "headerstyle";

                if (field.SortExpression == gridView.SortExpression)
                {
                    cellIndex = gridView.Columns.IndexOf(field);
                }
            }

            if (cellIndex > -1)
            {
                //  this is a header row,
                //  set the sort style
                e.Row.Cells[cellIndex].CssClass =
                    gridView.SortDirection == SortDirection.Ascending
                    ? "sortascheaderstyle" : "sortdescheaderstyle";
            }
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
                DataSet ds = new DataSet();
                ds = (DataSet)Session["Grid"];
                DataView dv;
                dv = ds.Tables[0].DefaultView;
                dv.Sort = this.ViewState["SortExp"] + " " + this.ViewState["SortOrder"];
                grdVerReceive.DataSource = dv;
                grdVerReceive.DataBind();
            }
            else
            {
                if (this.ViewState["SortExp"].ToString() == e.CommandArgument.ToString())
                {
                    if (this.ViewState["SortOrder"].ToString() == "ASC")
                        this.ViewState["SortOrder"] = "DESC";
                    else
                        this.ViewState["SortOrder"] = "ASC";
                }
                else
                {
                    this.ViewState["SortOrder"] = "ASC";
                    this.ViewState["SortExp"] = e.CommandArgument.ToString();
                }
            }
        }
    }

    public void BindGrid()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            DataSet objds = new DataSet();


            fillControl();
            grdVerReceive.XGridBindSearch();
            Clear();
            


        }
        catch (Exception ex)
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

    public void fillControl()
    {
        txtBillStatus.Text = "VR";

        // _ReportDAO.BillStatusOr = "";
        txtBillStatus.Visible = true;
        txtPatientName.Visible = true;
        txtFlag.Visible = true;
        txtDay.Visible = true;
        txtDay.Visible = true;
        txtCompanyID.Visible = true;
        txtBillNo.Visible = true;
        txtCaseNo.Visible = true;
        txtFromDate.Visible = true;
        txtToDate.Visible = true;
        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
        {
            txtFlag.Text = "REF";
        }
        else
        {
            txtFlag.Text = "BILLING";
        }

        if (txtDay.Text != "")
        {
            DateTime DT = new DateTime();
            DT = DateTime.Now;
            DT = DT.AddDays(-Convert.ToInt32(txtDay.Text));
            txtDay.Text = DT.ToString("MM/dd/yyyy");
        }

        txtBillNo.Text = txtupdateBillNo.Text;
        txtCaseNo.Text = txtupdateCaseNo.Text;
        txtFromDate.Text = txtupdateFromDate.Text;
        txtToDate.Text = txtupdateToDate.Text;
        txtPatientName.Text = txtupdatePatientName.Text;
        txtBillStatus.Visible = false; ;
        txtDay.Visible = false; ;
        txtCompanyID.Visible = false;
        txtFlag.Visible = false;
        txtBillNo.Visible = false;
        txtCaseNo.Visible = false;
        txtDay.Visible = false;
        txtFromDate.Visible = false;
        txtToDate.Visible = false;
        txtPatientName.Visible = false;
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
            //   grdVerReceive.XGridBind();
            BindGrid();
        }
        catch (Exception ex)
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

    private string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }

    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        ExportToExcel();
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
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdVerReceive.Rows.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdVerReceive.Columns.Count - 2; i++)
                    {
                        if (grdVerReceive.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdVerReceive.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdVerReceive.Columns.Count - 2; j++)
                {
                    if ((grdVerReceive.Columns[j].Visible == true) && (j == 2))
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdVerReceive.Rows[icount].Cells[grdVerReceive.Columns.Count - 1].Text);
                        strHtml.Append("</td>");
                    }
                    else if (grdVerReceive.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdVerReceive.Rows[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");
            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET")  + filename);
            sw.Write(strHtml);
            sw.Close();
            //Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET")+ filename + "';", true);
        }
        catch (Exception ex)
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

    protected void grdvVerificationSent_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnLitigantion_Click(object sender, EventArgs e)
    {

        ArrayList _objarr = new ArrayList();
        for (int i = 0; i < grdVerReceive.Rows.Count; i++)
        {

            CheckBox chk = (CheckBox)grdVerReceive.Rows[i].FindControl("ChkverRec");
            if (chk.Checked)
            {
                Bill_sys_litigantion _objlitigantion = new Bill_sys_litigantion();
                string BillNo = grdVerReceive.Rows[i].Cells[0].Text;
                _objarr.Add(BillNo);
            }
        }
        Bill_Sys_NF3_Template objlitigate = new Bill_Sys_NF3_Template();
        objlitigate.UpdateLitigantion(_objarr, txtCompanyID.Text);
        BindGrid();

    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        // ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET")  + grdVerReceive.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET") .ToString()) + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET")  + grdVerReceive.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")) + "';", true);
    }
    public void Clear()
    {
        //txtBillStatus.Text = "";
        txtDay.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtBillNo.Text = "";
        txtCaseNo.Text = "";
        txtPatientName.Text = "";
    }

   
    protected void btnPrintEnv_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_NF3_Template _objTemp = new Bill_Sys_NF3_Template();
        try
        {
            String szPDFPhysicalpath = ConfigurationManager.AppSettings["NF2_ENVELOPE_PDF_FILE"];
            String szXMLPhysicalpath = ConfigurationManager.AppSettings["PROVIDERNAME"];
            String szBasePhysicalPath = _objTemp.getPhysicalPath();
            String szSourceFile1 = "";
            String szSourceFile1_FullPath = "";
            String szSourceFile2 = "";
            String szSourceFile2_FullPath = "";
            String szOpenFilePath = "";

            for (int i = 0; i < grdVerReceive.Rows.Count; i++)
            {
                String szCaseID = grdVerReceive.DataKeys[i]["SZ_CASE_ID"].ToString();
                CheckBox chkDelete1 = (CheckBox)grdVerReceive.Rows[i].FindControl("ChkverRec");
                if (chkDelete1.Checked)
                {
                    string InsCompanyName = ((TextBox)grdVerReceive.Rows[i].FindControl("txtInsDetails")).Text;
                    string Insdescription = ((TextBox)grdVerReceive.Rows[i].FindControl("txtInsAddress")).Text;
                    string InsDetials = ((TextBox)grdVerReceive.Rows[i].FindControl("txtInsState")).Text;
                    string ProviderName = grdVerReceive.DataKeys[i]["SZ_OFFICE_ID"].ToString();
                    String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/";
                    ReplacePdfValues objReplacePdf = new ReplacePdfValues();
                    string szGeneratedPDFName = objReplacePdf.PrintEnvelope1(szXMLPhysicalpath, szPDFPhysicalpath, ProviderName, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID, InsCompanyName, Insdescription, InsDetials);

                    if (szSourceFile1 == "")
                    {
                        szSourceFile1 = szGeneratedPDFName;
                        szSourceFile1_FullPath = szBasePhysicalPath + szDefaultPath + szSourceFile1;
                        szOpenFilePath = szDefaultPath + szSourceFile1;
                    }
                    else
                    {
                        szSourceFile2 = szGeneratedPDFName;
                        szSourceFile2_FullPath = szBasePhysicalPath + szDefaultPath + szSourceFile2;
                        MergePDF.MergePDFFiles(szSourceFile1_FullPath, szSourceFile2_FullPath, szBasePhysicalPath + szDefaultPath + "_" + szSourceFile1);
                        szSourceFile1 = "_" + szSourceFile1;
                        szSourceFile1_FullPath = szBasePhysicalPath + szDefaultPath + szSourceFile1;
                        szOpenFilePath = szDefaultPath + szSourceFile1;
                    }

                }
            }
            szOpenFilePath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szOpenFilePath;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + szOpenFilePath.ToString() + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
            BindGrid();
            usrMessage.PutMessage("Verification print POM envelope generated");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage );
            usrMessage.Show();
        }
        catch (Exception ex)
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

    protected void btnYes_Click(object sender, EventArgs e)
    {
        hdnPOMValue.Value = "Yes";
        btnYes.Attributes.Add("onclick", "YesMassage");
        //document.getElementById('div1').style.visibility='hidden';   
       creatPDF();
       BindGrid();
       usrMessage.PutMessage("Verifiation POM report generated");
       usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
       usrMessage.Show();
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        hdnPOMValue.Value = "No";
        btnNo.Attributes.Add("onclick", "NoMassage");
        creatPDF();
        BindGrid();
        usrMessage.PutMessage("Verifiation POM report generated");
        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
        usrMessage.Show();
    }

    private void creatPDF()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        String szSourceFile1 = "";
        String szSourceFile1_FullPath = "";
        String szSourceFile2 = "";
        String szSourceFile2_FullPath = "";
        String szOpenFilePath = "";
        int i_pom_id = 0;
        string pdffilename = "";
        string genhtmlNF = "";
        string genhtmlWC = "";
        string billIDWC = "";
        string billIDNF = "";
        string providerNameWC = "";
        string providerNameNF = "";
        int i = 0;
        int printi = 0;
        int printiWC = 0;
        int iPageCount = 1;
        string billidWC = "";
        string billidNF = "";
        string NodeIdPath = "";
        Bill_Sys_NF3_Template _objTemp = new Bill_Sys_NF3_Template();
        StringBuilder sbFinalHTML = new StringBuilder();
        if (hdnPOMValue.Value.Equals("Yes"))
        {
            _reportBO = new Bill_Sys_ReportBO();
            i_pom_id = _reportBO.POMSave(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, 0);
        }
        sbFinalHTML.AppendLine("");
        int iRecordOnpageNF = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        int iRecordOnpageWC = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        try
        {

            String szBasePhysicalPath = _objTemp.getPhysicalPath();
            string szDefaultPath = ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET");
            int iFlag = 0;
            DataSet ds = CreateGroupData();
            int iRecordsPerPageNF = 0;
            int iRecordsPerPageWC = 0;
            string strProvider = null;
            string strProviderNF = null;
            string strCasetype = null;
            bool iWCCount = false;

            genhtmlNF = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";//</td><td><b>Patient Name</b></td><td><b>Insurance Company</b></td><td><b>Insurance Address</b></td><td><b>Claim No</b></td><td><b>Date Of Accident</b></td></tr>";
            genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";//</td><td><b>Patient Name</b></td><td><b>Insurance Company</b></td><td><b>Insurance Address</b></td><td><b>Claim No</b></td><td><b>Date Of Accident</b></td></tr>";

            // create html and pdf for groupwise provider . print per page in pdf for provider 

            int _iCountWC = 0;
            int _iCountNF = 0;
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {

                strCasetype = ds.Tables[0].Rows[j]["CaseType Id"].ToString();

                if (strCasetype == "WC000000000000000001")
                {

                    sbFinalHTML.AppendLine("");
                    if (iFlag == 0)
                    {
                        strProvider = ds.Tables[0].Rows[j]["Reffering Office"].ToString();
                        iFlag = 1;
                        billidWC = ds.Tables[0].Rows[j]["Bill No"].ToString();
                        iRecordsPerPageWC = 0;
                    }
                    //IF PRESENT PROVIDER OR FIRST PROVIDER MATCH WITH PRIVISIOUS PROVIDER OR FRIST PROVIDER
                    if (strProvider.Equals(ds.Tables[0].Rows[j]["Reffering Office"].ToString()))
                    {
                        ++printi;
                        ++printiWC;
                        if (iRecordsPerPageWC == iRecordOnpageWC)
                        {
                            sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlWC, billIDWC, i_pom_id));
                            sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                            iWCCount = true;
                            //                             genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            iRecordsPerPageWC = 1;
                            _iCountWC = 1;
                        }
                        if (iWCCount == true)
                        {
                            genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            iWCCount = false;
                        }

                        // append nf html for wc bill
                        genhtmlNF += "<tr><td style='font-size:9px'>" + (printi).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Insurance Claim No"] + "</td><td style='font-size:9px'><b>" + ds.Tables[0].Rows[j]["Insurance Company"] + "</b><br/>" + ds.Tables[0].Rows[j]["Insurance Address"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Speciality"] + "<br />" + ds.Tables[0].Rows[j]["Min Date Of Service"] + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[j]["Bill Amount"] + "<br />" + ds.Tables[0].Rows[j]["Max Date Of Service"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Bill No"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case No"] + "</td></tr>";
                        _iCountNF++;


                        //append wc html for wc bill
                        //                                            Index                                                    Claim Number                                                                       wc Name                                wc  Address                                                                                                                                                                                              Speciality / Minimum date of service                                                                                          // Total Bill Amount / Last date of service of bill                                                                                           // Bill Number                                                                             // Patient Name
                        genhtmlWC += "<tr><td style='font-size:9px'>" + (printiWC).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Insurance Claim No"] + "</td><td style='font-size:9px'><b> Worker's Compensation Board </b><br/>" + ds.Tables[0].Rows[j]["WC_ADDRESS"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Speciality"] + "<br />" + ds.Tables[0].Rows[j]["Min Date Of Service"] + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[j]["Bill Amount"] + "<br />" + ds.Tables[0].Rows[j]["Max Date Of Service"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Bill No"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case No"] + "</td></tr>";
                        _iCountWC++;

                        iRecordsPerPageWC++;
                        billidWC = ds.Tables[0].Rows[j]["Bill No"].ToString();
                        billidNF = ds.Tables[0].Rows[j]["Bill No"].ToString();


                    }
                    else
                    {
                        printi = 1;
                        printiWC = 1;
                        // SUPPOSE NEW PROVIDER OR CHANGE PROVIDER WHICH NO MATCH WITH PRIVOUS PROIVDER
                        if (_iCountWC > 0)
                        {
                            genhtmlWC += "</table>";
                            sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlWC, billidWC, i_pom_id));
                            sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                            iWCCount = true;
                            //genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            printiWC = 1;
                        }
                        if (iWCCount == true)
                        {
                            genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            iWCCount = false;
                        }

                        genhtmlNF += "</table>";
                        sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlNF, billidNF, i_pom_id));
                        sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");

                        // NEW PROVIDER AND BILLID SAVE IN VARIABLES
                        strProvider = ds.Tables[0].Rows[j]["Reffering Office"].ToString();
                        billidWC = ds.Tables[0].Rows[j]["Bill No"].ToString();
                        billidNF = ds.Tables[0].Rows[j]["Bill No"].ToString();

                        genhtmlNF = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";

                        //append nf html for wc bill
                        genhtmlNF += "<tr><td style='font-size:9px'>" + (printi).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Insurance Claim No"] + "</td><td style='font-size:9px'><b>" + ds.Tables[0].Rows[j]["Insurance Company"] + "</b><br/>" + ds.Tables[0].Rows[j]["Insurance Address"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Speciality"] + "<br />" + ds.Tables[0].Rows[j]["Min Date Of Service"] + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[j]["Bill Amount"] + "<br />" + ds.Tables[0].Rows[j]["Max Date Of Service"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Bill No"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case No"] + "</td></tr>";
                        _iCountNF++;

                        //append wc html for wc bill
                        genhtmlWC += "<tr><td style='font-size:9px'>" + (printiWC).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Insurance Claim No"] + "</td><td style='font-size:9px'><b> Worker's Compensation Board </b><br/>" + ds.Tables[0].Rows[j]["WC_ADDRESS"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Speciality"] + "<br />" + ds.Tables[0].Rows[j]["Min Date Of Service"] + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[j]["Bill Amount"] + "<br />" + ds.Tables[0].Rows[j]["Max Date Of Service"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Bill No"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case No"] + "</td></tr>";
                        _iCountWC++;

                        iPageCount++;

                        iRecordsPerPageWC = 1;
                        iRecordsPerPageNF = 1;
                        _iCountNF = 1;
                    }



                }
                else if (strCasetype != "WC000000000000000001")
                {
                    // ONLY NF PROVIDER
                    iRecordsPerPageNF = _iCountNF;
                    sbFinalHTML.AppendLine("");
                    if (iFlag == 0)
                    {
                        strProvider = ds.Tables[0].Rows[j]["Reffering Office"].ToString();
                        iFlag = 1;
                        billidNF = ds.Tables[0].Rows[j]["Bill No"].ToString();
                        iRecordsPerPageNF = 0;
                    }
                    if (strProvider.Equals(ds.Tables[0].Rows[j]["Reffering Office"].ToString()))
                    {
                        ++printi;

                        if (iRecordsPerPageNF == iRecordOnpageNF)
                        {
                            sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlNF, ds.Tables[0].Rows[j]["Bill No"].ToString(), i_pom_id));
                            sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                            genhtmlNF = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            iRecordsPerPageNF = 0;
                            _iCountNF = 0;
                        }

                        ////changes from sy
                        //  if (_iCountWC > 0 )
                        //  {
                        //      genhtmlNF += "</table>";
                        //      sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlNF, billidNF));
                        //      sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                        //      genhtmlNF = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                        //  }
                        //  //======================================================================



                        genhtmlNF += "<tr><td style='font-size:9px'>" + (printi).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Insurance Claim No"] + "</td><td style='font-size:9px'><b>" + ds.Tables[0].Rows[j]["Insurance Company"] + "</b><br/>" + ds.Tables[0].Rows[j]["Insurance Address"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Speciality"] + "<br />" + ds.Tables[0].Rows[j]["Min Date Of Service"] + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[j]["Bill Amount"] + "<br />" + ds.Tables[0].Rows[j]["Max Date Of Service"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Bill No"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case No"] + "</td></tr>";
                        _iCountNF++;
                        iRecordsPerPageNF++;
                    }
                    else
                    {
                        printi = 1;
                        //save provider name in variable
                        strProvider = ds.Tables[0].Rows[j]["Reffering Office"].ToString();

                        genhtmlNF += "</table>";

                        if (_iCountWC > 0)
                        {
                            genhtmlWC += "</table>";
                            sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlWC, billidWC, i_pom_id));
                            sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                            iWCCount = true;
                            // genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            printiWC = 0;
                        }
                        sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlNF, billidNF, i_pom_id));
                        sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                        genhtmlNF = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";

                        genhtmlNF += "<tr><td style='font-size:9px'>" + (printi).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Insurance Claim No"] + "</td><td style='font-size:9px'><b>" + ds.Tables[0].Rows[j]["Insurance Company"] + "</b><br/>" + ds.Tables[0].Rows[j]["Insurance Address"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Speciality"] + "<br />" + ds.Tables[0].Rows[j]["Min Date Of Service"] + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[j]["Bill Amount"] + "<br />" + ds.Tables[0].Rows[j]["Max Date Of Service"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Bill No"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case No"] + "</td></tr>";
                        _iCountNF++;
                        iPageCount++;
                        billidNF = ds.Tables[0].Rows[j]["Bill No"].ToString();
                        iRecordsPerPageNF = 1;
                        _iCountNF = 1;

                    }

                }
            }

            string genHtml = "";
            if (iWCCount == false && _iCountWC > 0)
            {

                genhtmlWC += "</table>";
                sbFinalHTML.AppendLine(ReplaceHeaderAndFooter(genhtmlWC, billidWC, i_pom_id));
                sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
            }


            if (_iCountNF > 0)
            {
                genhtmlNF += "</table>";
                genHtml = genhtmlNF;
                sbFinalHTML.AppendLine(ReplaceHeaderAndFooter(genHtml, billidNF, i_pom_id));

            }
            if (_iCountWC > 0)
            {

                // sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");                     
                //genHtml = genhtmlWC;
                //sbFinalHTML.Append(ReplaceHeaderAndFooter(genHtml, billidWC ));       
            }

            string strHtml = sbFinalHTML.ToString();
            SautinSoft.PdfMetamorphosis objHTMToPDF = new SautinSoft.PdfMetamorphosis();
            objHTMToPDF.Serial = "10007706603";
            string htmfilename = getFileName("P") + ".htm";
            pdffilename = getFileName("P") + ".pdf";
            string file_Path = ApplicationSettings.GetParameterValue("EXCEL_SHEET")  + pdffilename;

            StreamWriter sw = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET")  + htmfilename);
            sw.Write(strHtml);
            sw.Close();
            Int32 iTemp;
            iTemp = objHTMToPDF.HtmlToPdfConvertFile(ApplicationSettings.GetParameterValue("EXCEL_SHEET")  + htmfilename, ApplicationSettings.GetParameterValue("EXCEL_SHEET")  + pdffilename);

            //Code To upload generated POM pdf in document manager of selected case no : TUSHAR 4 JUNE
            if (hdnPOMValue.Value == "Yes")
            {
                ArrayList lst = new ArrayList();
                ArrayList ImageId = new ArrayList();
                ArrayList BillNo = new ArrayList();
                Boolean updateflag = false;
                string image = "";
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    BillNo.Add(ds.Tables[0].Rows[j]["Bill No"].ToString());
                    int flag = 0;
                    if (lst.Count == 0)
                    {
                        flag = 1;
                    }
                    for (int a = 0; a < lst.Count; a++)
                    {
                        string lsttext = lst[0].ToString();
                        string caseid = ds.Tables[0].Rows[j]["Case Id"].ToString();
                        if (!lst[a].ToString().Equals(ds.Tables[0].Rows[j]["Case Id"].ToString()))
                        {

                            flag = 1;
                        }
                        else
                        {
                            flag = 0;
                            break;
                        }

                    }
                    if (flag == 1)
                    {
                        ListItem li = new ListItem();
                        li.Text = ds.Tables[0].Rows[j]["Case Id"].ToString();
                        lst.Add(li);

                        _reportBO = new Bill_Sys_ReportBO();
                        _nf3Template = new Bill_Sys_NF3_Template();
                        _billTransactionBO = new Bill_Sys_BillTransaction_BO();
                        
                            string NodeId = _billTransactionBO.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "NFPRO");

                            //Code To Copy File Phisicaly:TUSHAR
                            string filename = "";
                            string FilePath = "";
                            string szRootFolder = _nf3Template.getPhysicalPath();
                            Bill_Sys_RequiredDocumentBO bo = new Bill_Sys_RequiredDocumentBO();
                            //string szNodePath = bo.GetNodePath(NodeId, ds.Tables[0].Rows[j]["Case Id"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            string szNodePath = "";
                                szNodePath = _nf3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            
                            szNodePath = szNodePath + "\\POM Generate\\" + i_pom_id + "\\";
                            szNodePath = szNodePath.Replace("\\", "/");
                            NodeIdPath = szNodePath;
                            filename = szRootFolder + szNodePath;
                            FilePath = szRootFolder + szNodePath;
                            if (!updateflag)
                            {
                                if (Directory.Exists(filename) == false)
                                {
                                    Directory.CreateDirectory(filename);
                                }
                                filename = szRootFolder + szNodePath + "/" + pdffilename;
                                string szBasePhysicalFullPath = file_Path;
                                File.Copy(szBasePhysicalFullPath, filename);
                                updateflag = true;
                            }

                            //End oF code

                            //Code To Get Image Id
                            ArrayList _arraylist = new ArrayList();
                            _arraylist.Add(ds.Tables[0].Rows[j]["Case Id"].ToString());
                            _arraylist.Add("");
                            _arraylist.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            _arraylist.Add(pdffilename.ToString());
                            _arraylist.Add(szNodePath.ToString());
                            _arraylist.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            _arraylist.Add(NodeId);
                            image = _nf3Template.SaveDocumentData(_arraylist);
                            ImageId.Add(image);
                            
                        

                    }

                }
                _reportBO.POMEntry(i_pom_id, DateTime.Today.ToString("MM/dd/yyyy"), Convert.ToInt32(ImageId[0].ToString()), pdffilename.ToString(), NodeIdPath + "/", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, BillNo, "POMG");

                BindGrid();
            }
            //End Of Code




            string szPDFName = "";
            szPDFName = szDefaultPath + pdffilename;

            if (szSourceFile1 == "")
            {
                szSourceFile1 = pdffilename;
                szSourceFile1_FullPath = szDefaultPath + szSourceFile1;
                szOpenFilePath = szDefaultPath + szSourceFile1;
            }
            else
            {
                szSourceFile2 = pdffilename;
                szSourceFile2_FullPath = szDefaultPath + szSourceFile2;


            }
            szOpenFilePath = szDefaultPath + szSourceFile1;
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);


        }
        catch (Exception ex)
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




    private DataSet CreateGroupData()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        try
        {

            dt.Columns.Add("Bill No");
            dt.Columns.Add("Case Id");
            dt.Columns.Add("Patient Name");
            dt.Columns.Add("Speciality");
            dt.Columns.Add("Reffering Office");
            dt.Columns.Add("Bill Amount");
            dt.Columns.Add("Bill Date");
            dt.Columns.Add("Insurance Claim No");
            dt.Columns.Add("Insurance Company");
            dt.Columns.Add("Insurance Address");
            dt.Columns.Add("Min Date Of Service");
            dt.Columns.Add("Max Date Of Service");
            dt.Columns.Add("CaseType Id");
            dt.Columns.Add("WC_ADDRESS");
            dt.Columns.Add("Case No");
            dt.Columns.Add("InsDescription");
            for (int i = 0; i < grdVerReceive.Rows.Count; i++)
            {
                if (((CheckBox)grdVerReceive.Rows[i].FindControl("ChkverRec")).Checked == true)
                {
                    string insDetails = ((TextBox)grdVerReceive.Rows[i].FindControl("txtInsDetails")).Text;
                    string insAddress = ((TextBox)grdVerReceive.Rows[i].FindControl("txtInsAddress")).Text;
                    string insState = ((TextBox)grdVerReceive.Rows[i].FindControl("txtInsState")).Text;
                    string insDescription = ((TextBox)grdVerReceive.Rows[i].FindControl("txtInsDesc")).Text;
                    DataRow dr = dt.NewRow();
                    
                    dr["Case No"] = grdVerReceive.Rows[i].Cells[0].Text;
                    dr["Bill No"] = grdVerReceive.Rows[i].Cells[1].Text;
                    dr["Case Id"] = grdVerReceive.DataKeys[i]["SZ_CASE_ID"].ToString();
                    dr["Patient Name"] = grdVerReceive.Rows[i].Cells[3].Text;
                    dr["Speciality"] = grdVerReceive.DataKeys[i]["SZ_SPECIALITY"].ToString();
                    dr["Reffering Office"] = grdVerReceive.DataKeys[i]["SZ_COMPANY_NAME"].ToString();
                    dr["Bill Amount"] = grdVerReceive.DataKeys[i]["FLT_BILL_AMOUNT"].ToString();
                     dr["Bill Date"] = grdVerReceive.Rows[i].Cells[2].Text;
                     dr["Insurance Claim No"] = grdVerReceive.DataKeys[i]["SZ_CLAIM_NUMBER"].ToString();
                     
                     if (!insAddress.Equals("") || !insDetails.Equals("") || !insState.Equals(""))
                     {
                         string sz_ins_name = "<b>" + insDetails + "</b>";
                         string sz_address =  insAddress + "<br/>" + insState;
                         dr["Insurance Company"] = sz_ins_name;
                         dr["Insurance Address"] = sz_address;
                     }
                     else
                     {
                         dr["Insurance Company"] = grdVerReceive.Rows[i].Cells[4].Text;
                         dr["Insurance Address"] = grdVerReceive.DataKeys[i]["SZ_INSURANCE_ADDRESS"].ToString();
                     }
                     dr["Min Date Of Service"] = grdVerReceive.DataKeys[i]["SZ_MIN_SERVICE_DATE"].ToString();
                     dr["Max Date Of Service"] = grdVerReceive.DataKeys[i]["SZ_MAX_SERVICE_DATE"].ToString();
                     dr["CaseType Id"] = grdVerReceive.DataKeys[i]["SZ_CASE_TYPE_ID"].ToString();
                     dr["WC_ADDRESS"] = grdVerReceive.DataKeys[i]["WC_ADDRESS"].ToString();
                     if (!insDescription.Equals(""))
                     {
                         dr["InsDescription"] = insDescription;
                         
                     }
                     else
                     {
                         dr["InsDescription"] = "";
                     }
                    
                    dt.Rows.Add(dr);

                }
            }
            if (dt.Rows.Count > 0)
            {
                DataView dv = dt.DefaultView;
                dv.Sort = "Reffering Office";
                dt = dv.ToTable();
            }



            ds.Tables.Add(dt);


        }
        catch (Exception ex)
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
        return ds;
    }


    protected string ReplaceHeaderAndFooter(String szInput, string sz_bill_no, int i_pom_id)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string szFileData = "";
        try
        {
            if (hdnPOMValue.Value.Equals("Yes"))
            {
                szFileData = File.ReadAllText(ConfigurationManager.AppSettings["TEST_POM_HTML"]);
                szFileData = szFileData.Replace("VL_SZ_POM_ID", i_pom_id.ToString());
            }
            else
            {
                szFileData = File.ReadAllText(ConfigurationManager.AppSettings["POM_HTML"]);
            }
            //GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
            szFileData = getNF2MailDetails(szFileData, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, sz_bill_no);
            szFileData = szFileData.Replace("VL_SZ_TABLE_DATA", szInput);
            szFileData = szFileData.Replace("VL_SZ_CASE_COUNT", "");
          
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        return szFileData;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public string getNF2MailDetails(string p_htmlstring, string p_szCompanyID, string sz_bill_id)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string szReplaceString = "";
        SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        objSqlConn.Open();
        GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
        try
        {
            //log.Debug("Bill_Sys_BilPOM. Method - getNF2MailDetails : ");

            szReplaceString = p_htmlstring;
            SqlCommand objSqlComm1 = new SqlCommand("SP_TEST_FACILITY_MAILS_DETAILS", objSqlConn);
            SqlDataReader objDR;
            objSqlComm1.CommandType = CommandType.StoredProcedure;
            objSqlComm1.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            objSqlComm1.Parameters.AddWithValue("@SZ_BILL_ID", sz_bill_id);
            objDR = objSqlComm1.ExecuteReader();
            szReplaceString = objPDF.ReplaceNF2MailDetails(szReplaceString, objDR);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally
        {
            if (objSqlConn.State == ConnectionState.Open)
            {
                objSqlConn.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        return szReplaceString;
    }
    




    public void POMEntry(int i_pom_id, string P_POM_Date, int ImageId, string P_File_Name, string P_File_Path, string P_Company_Id, string P_User_Id, ArrayList P_Bill_No, string P_Bill_Status)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        ds = new DataSet();
        SqlTransaction transaction;
        sqlCon.Open();
        transaction = sqlCon.BeginTransaction();
        try
        {
            #region "Update POM Entry Into TXN_BILL_POM TABLE AND GET LATEST POM_ID"
            sqlCmd = new SqlCommand("SP_UPDATE_TXN_BILL_POM", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_POM_DATE", P_POM_Date);
            sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", ImageId);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", P_File_Name);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", P_File_Path);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id);
            sqlCmd.Parameters.AddWithValue("@I_POM_ID", i_pom_id);

            sqlCmd.ExecuteNonQuery();
            #endregion "End Of Code"

            #region "Update Txn_Bill_Transation set POM_ID Against All Bill_No On Which POM is Generated."
            for (int i = 0; i < P_Bill_No.Count; i++)
            {
                sqlCmd = new SqlCommand("SP_UPDATE_TXN_BILL_TRNSACTION_VERIFICATION", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                sqlCmd.Parameters.AddWithValue("@I_POM_ID", i_pom_id);
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", P_Bill_No[i]);
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS", P_Bill_Status);
                sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id);


                sqlCmd.ExecuteNonQuery();
            }
            #endregion"End Of Code"

            transaction.Commit();
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            transaction.Rollback();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


 
}
