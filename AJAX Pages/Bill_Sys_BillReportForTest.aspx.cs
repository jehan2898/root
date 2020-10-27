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
using System.Data.Sql;
using System.Data.SqlClient;
using RequiredDocuments;


public partial class Bill_Sys_BillReportForTest : PageBase
{  
    private Bill_Sys_ReportBO _reportBO;
    Bill_Sys_BillTransaction_BO _billTransactionBO;
    Bill_Sys_NF3_Template _nf3Template;
    string status_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Image1.Visible = false;
            Span2.InnerHtml = "Is this the final POM?";
            btnPrintPOM.Attributes.Add("onclick", "return POMConformation();");            
            btnPrintEnvelop.Attributes.Add("onclick", "return Validate('_ctl0_ContentPlaceHolder1_btnPrintEnvelop')");
            //btnPrintPOM.Attributes.Add("onclick", "return Validate('_ctl0_ContentPlaceHolder1_btnPrintPOM')");
            //btnUpdateStatus.Attributes.Add("onclick", "return Validate('_ctl0_ContentPlaceHolder1_btnUpdateStatus')");
            btnUpdateStatus.Attributes.Add("onclick", "return UpdateStatus();");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {

                if (Request.QueryString["flag"] != null)
                {
                    if (Request.QueryString["flag"].ToString() == "View")
                    {
                        Session["sz_StatusID_For_Test"] = Request.QueryString["Status"].ToString();
                        Session["sz_SpecialityID_For_Test"] = Request.QueryString["speciality"].ToString();
                        Bill_Sys_BillingCompanyDetails_BO objstatus = new Bill_Sys_BillingCompanyDetails_BO();
                        status_id = GetBillStatusCode(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["sz_StatusID_For_Test"].ToString());
                        if (status_id == "DNL" || status_id == "TRF" || status_id == "SLD" || status_id == "LND")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ss", "DisabledAll();", true);
                        }
                    }
                }
                txtSort.Text="";
                txtBillStatusdate.Text = System.DateTime.Today.ToShortDateString();
               
                extddlBillStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //extddlBillStatus.Flag_ID ="CO00023";
                lblHeader.Text = "Reports - Procedure";
                //txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //extddlSpeciality.Flag_ID = txtCompanyID.Text;           
                BindGrid();
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "ss", "DisabledAll()", true);
              
               
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
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_ProcedureReport.aspx");
        }
        #endregion


        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _reportBO = new Bill_Sys_ReportBO();
        try
        {
               DataSet objds = new DataSet();
          
               if (Session["sz_From_Date"] != "" && Session["sz_To_Date"] != "" && Session["sz_User"].ToString() == "NA" && Session["sz_From_Service_Date"] == "" && Session["sz_To_Service_Date"]=="")
               {
                   objds = _reportBO.GetBillReportsByDateAndProcedure(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["sz_From_Date"].ToString(), Session["sz_To_Date"].ToString(), Session["sz_SpecialityID_For_Test"].ToString(), Session["sz_StatusID_For_Test"].ToString());
               }
               else if (Session["sz_From_Date"] != "" && Session["sz_To_Date"] != "" && Session["sz_User"].ToString() != "NA" && Session["sz_From_Service_Date"] == "" && Session["sz_To_Service_Date"] == "")
               {
                   objds = _reportBO.GetBillReportsByDate(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["sz_From_Date"].ToString(), Session["sz_To_Date"].ToString(), Session["sz_SpecialityID_For_Test"].ToString(), Session["sz_StatusID_For_Test"].ToString(), Session["sz_User"].ToString());
               }
               else if (Session["sz_From_Date"] == "" && Session["sz_To_Date"] == "" && Session["sz_User"].ToString() != "NA" && Session["sz_From_Service_Date"] == "" && Session["sz_To_Service_Date"] == "")
               {
                   objds = _reportBO.GetBillReports(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["sz_SpecialityID_For_Test"].ToString(), Session["sz_StatusID_For_Test"].ToString(), Session["sz_User"].ToString());
               }
              //Service Date Wise Search
               else if (Session["sz_From_Date"] != "" && Session["sz_To_Date"] != "" && Session["sz_User"].ToString() == "NA" && Session["sz_From_Service_Date"] != "" && Session["sz_To_Service_Date"] != "")
               {
                   objds = _reportBO.GetBillReportsByAllDates(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["sz_SpecialityID_For_Test"].ToString(), Session["sz_StatusID_For_Test"].ToString(), Session["sz_From_Date"].ToString(), Session["sz_To_Date"].ToString(), Session["sz_From_Service_Date"].ToString() ,Session["sz_To_Service_Date"].ToString());
               }
               else if (Session["sz_From_Date"] == "" && Session["sz_To_Date"] == "" && Session["sz_User"].ToString() != "NA" && Session["sz_From_Service_Date"] != "" && Session["sz_To_Service_Date"] != "")
               {
                   objds = _reportBO.GetBillReportsByServiceDatesandUser(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["sz_SpecialityID_For_Test"].ToString(), Session["sz_StatusID_For_Test"].ToString(), Session["sz_User"].ToString(), Session["sz_From_Service_Date"].ToString() ,Session["sz_To_Service_Date"].ToString());
               }
               else if (Session["sz_From_Date"] == "" && Session["sz_To_Date"] == "" && Session["sz_User"].ToString() == "NA" && Session["sz_From_Service_Date"] != "" && Session["sz_To_Service_Date"] != "")
               {
                   objds = _reportBO.GetBillReportsByServiceDate(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["sz_SpecialityID_For_Test"].ToString(), Session["sz_StatusID_For_Test"].ToString(), Session["sz_From_Service_Date"].ToString() ,Session["sz_To_Service_Date"].ToString());
               }
               else if (Session["sz_From_Date"] != "" && Session["sz_To_Date"] != "" && Session["sz_User"].ToString() != "NA" && Session["sz_From_Service_Date"] != "" && Session["sz_To_Service_Date"] != "")
               {
                   objds = _reportBO.GetBillReportsByAllDateAndUser(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["sz_SpecialityID_For_Test"].ToString(), Session["sz_StatusID_For_Test"].ToString(), Session["sz_User"].ToString(), Session["sz_From_Date"].ToString(), Session["sz_To_Date"].ToString(), Session["sz_From_Service_Date"].ToString() ,Session["sz_To_Service_Date"].ToString());
               }
               else
               {
                   objds = _reportBO.GetBillReportsByProcedure(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["sz_SpecialityID_For_Test"].ToString(), Session["sz_StatusID_For_Test"].ToString());
               }

                grdAllReports.DataSource = objds;
                grdAllReports.DataBind();


                grdForReport.DataSource = objds;
                grdForReport.DataBind();

                grdForExcel.DataSource = objds;
                grdForExcel.DataBind();

                Session["DataBind"] = objds;
                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
                {
                    grdAllReports.Columns[6].Visible = false;
                    grdForReport.Columns[6].Visible = false;
                }
            //Code To Show Bill Amount Total at End Of Grid:- TUSHAR
                float  Tot = 0;
                for (int i = 0; i < grdAllReports.Items.Count; i++)
                {
                    if (grdAllReports.Items[i].Cells[15].Text != "&nbsp;")
                    {
                        Tot = (float)(Tot + Convert.ToDouble(grdAllReports.Items[i].Cells[15].Text));//13
                    }
                }
                lblTotalVal.Text = Tot.ToString();
            //End OF Code
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            BindGrid();
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


            //Method End
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }
    }
    protected void grdAllReports_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdAllReports.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
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

    private void ExportToExcel()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdForExcel.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdForExcel.Columns.Count; i++)
                    {
                        if (grdForExcel.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdForExcel.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdForExcel.Columns.Count; j++)
                {
                    if (grdForExcel.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdForExcel.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);


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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ExportToExcel();
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

    //PRINT ENVELOP
    protected void btnPrintEnvelop_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_NF3_Template _objTemp = new Bill_Sys_NF3_Template();
        try
        {
            String szPDFPhysicalpath = ConfigurationManager.AppSettings["NF2_ENVELOPE_PDF_FILE"];
            String szXMLPhysicalpath = ConfigurationManager.AppSettings["TEST_FACILITY_ENVELOPE_XML_FILE"];
            String szXMLPhysicalpathWC = ConfigurationManager.AppSettings["TEST_FACILITY_ENVELOPE_XML_WC_FILE"];
            String szBasePhysicalPath = _objTemp.getPhysicalPath();
            String szSourceFile1 = "";
            String szSourceFile1_FullPath = "";
            String szSourceFile2 = "";
            String szSourceFile2_FullPath = "";
            String szOpenFilePath = "";

            String szSourceFileWC1 = "";
            String szSourceFileWC1_FullPath = "";
            String szSourceFileWC2 = "";
            String szSourceFileWC2_FullPath = "";
            String szOpenFilePathWC = "";
            String szDefaultPathWC = "";
            String szDefaultPath = "";
            for (int i = 0; i < grdAllReports.Items.Count; i++)
            {

                String szCaseID = grdAllReports.Items[i].Cells[5].Text; //4
                String szBillID = grdAllReports.Items[i].Cells[19].Text; //17
                
                CheckBox chkDelete1 = (CheckBox)grdAllReports.Items[i].FindControl("chkSelect");
                if (chkDelete1.Checked)
                {
                    string _caseType = grdAllReports.Items[i].Cells[1].Text;
                    String szCaseIDWC = grdAllReports.Items[i].Cells[5].Text; //4
                    if (_caseType == "WC000000000000000001")
                    {
                        //szDefaultPathWC = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseIDWC + "/Packet Document/";
                        PDFValueReplacement.PDFValueReplacement objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                        string szGeneratedPDFName = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpathWC, szPDFPhysicalpath, szBillID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseIDWC);
                        if (szSourceFileWC1 == "")
                        {
                            szDefaultPathWC = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseIDWC + "/Packet Document/";

                            szSourceFileWC1 = szGeneratedPDFName;
                            szSourceFileWC1_FullPath = szBasePhysicalPath + szDefaultPathWC + szSourceFileWC1;
                            szOpenFilePathWC = szDefaultPathWC + szSourceFile1;
                            string szGeneratedPDFName1 = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpath, szPDFPhysicalpath, szBillID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseIDWC);

                            szSourceFileWC2 = szGeneratedPDFName1;
                            szSourceFileWC2_FullPath = szBasePhysicalPath + szDefaultPathWC + szSourceFileWC2;
                            MergePDF.MergePDFFiles(szSourceFileWC1_FullPath, szSourceFileWC2_FullPath, szBasePhysicalPath + szDefaultPathWC + "_" + szSourceFileWC1);
                            szSourceFileWC1 = "_" + szSourceFileWC1;
                            szSourceFileWC1_FullPath = szBasePhysicalPath + szDefaultPathWC + szSourceFileWC1;
                            szOpenFilePathWC = szDefaultPathWC + szSourceFileWC1;
                        }
                        else
                        {
                            //string szGeneratedPDFName1 = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpath, szPDFPhysicalpath, szBillID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                            String szDefaultPathWCElse = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseIDWC + "/Packet Document/";

                            szSourceFileWC2 = szGeneratedPDFName;
                            szSourceFileWC2_FullPath = szBasePhysicalPath + szDefaultPathWCElse + szSourceFileWC2;
                            MergePDF.MergePDFFiles(szSourceFileWC1_FullPath, szSourceFileWC2_FullPath, szBasePhysicalPath + szDefaultPathWC + szSourceFileWC1);

                            string szGeneratedPDFName2 = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpath, szPDFPhysicalpath, szBillID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseIDWC);
                            szSourceFileWC2 = szGeneratedPDFName2;

                            szSourceFileWC2_FullPath = szBasePhysicalPath + szDefaultPathWCElse + szSourceFileWC2;
                            MergePDF.MergePDFFiles(szSourceFileWC1_FullPath, szSourceFileWC2_FullPath, szBasePhysicalPath + szDefaultPathWC + szSourceFileWC1);
                            // szSourceFile1 = "_" + szSourceFile1;
                            szSourceFileWC1_FullPath = szBasePhysicalPath + szDefaultPathWC + szSourceFileWC1;
                            szOpenFilePathWC = szDefaultPathWC + szSourceFileWC1;
                        }
                    }
                    else
                    {

                        //szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/";
                        PDFValueReplacement.PDFValueReplacement objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                        string szGeneratedPDFNameNF = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpath, szPDFPhysicalpath, szBillID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                        if (szSourceFile1 == "")
                        {
                            szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/";

                            szSourceFile1 = szGeneratedPDFNameNF;
                            szSourceFile1_FullPath = szBasePhysicalPath + szDefaultPath + szSourceFile1;
                            szSourceFile1 = szSourceFile1;
                            szOpenFilePath = szDefaultPath + szSourceFile1;
                        }
                        else
                        {
                            String szDefaultPathNF = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/";

                            szSourceFile2 = szGeneratedPDFNameNF;
                            szSourceFile2_FullPath = szBasePhysicalPath + szDefaultPathNF + szSourceFile2;
                            MergePDF.MergePDFFiles(szSourceFile1_FullPath, szSourceFile2_FullPath, szBasePhysicalPath + szDefaultPath + "_" + szSourceFile1);
                            szSourceFile1 = "_" + szSourceFile1;
                            szSourceFile1_FullPath = szBasePhysicalPath + szDefaultPath + szSourceFile1;
                            szOpenFilePath = szDefaultPath + szSourceFile1;
                        }

                    }

                }

            }

            if (szOpenFilePathWC != "" && szOpenFilePath != "")
            {

                MergePDF.MergePDFFiles(szBasePhysicalPath + szOpenFilePathWC, szBasePhysicalPath + szOpenFilePath, szBasePhysicalPath + szOpenFilePathWC);
                szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szOpenFilePathWC;

            }
            else if (szOpenFilePath != "")
            {
                szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szOpenFilePath;
            }
            else if (szOpenFilePathWC != "")
            {
                szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szOpenFilePathWC;
            }

            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
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

   
    //END PRINT ENVELOP


    //Print POM
    protected void btnPrintPOM_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //Span2.InnerHtml = "IS THIS THE FINAL POM?";
            //Page.RegisterStartupScript("mm", "<script language='javascript'> POMConformation(); </script>");            
            //creatPDF();
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

    private void creatPDF()
    {
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
        int i_pom_id=0;
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
            i_pom_id = _reportBO.POMSave(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID,0);
        }
        sbFinalHTML.AppendLine("");
        int iRecordOnpageNF = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        int iRecordOnpageWC = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        try
        {

            String szBasePhysicalPath = _objTemp.getPhysicalPath();
            string szDefaultPath = ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"];
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
                            sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlWC, billIDWC,i_pom_id));
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
                            sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlWC, billidWC,i_pom_id));
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
                        sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlNF, billidNF,i_pom_id));
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
                            sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlNF, ds.Tables[0].Rows[j]["Bill No"].ToString(),i_pom_id));
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
                            sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlWC, billidWC,i_pom_id));
                            sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                            iWCCount = true;
                            // genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            printiWC = 0;
                        }
                        sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlNF, billidNF,i_pom_id));
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
                sbFinalHTML.AppendLine(ReplaceHeaderAndFooter(genhtmlWC, billidWC,i_pom_id));
                sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
            }


            if (_iCountNF > 0)
            {
                genhtmlNF += "</table>";
                genHtml = genhtmlNF;
                sbFinalHTML.AppendLine(ReplaceHeaderAndFooter(genHtml, billidNF,i_pom_id));

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
            string file_Path = ConfigurationManager.AppSettings["EXCEL_SHEET"] + pdffilename;
          
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + htmfilename);
            sw.Write(strHtml);
            sw.Close();
            Int32 iTemp;
            iTemp = objHTMToPDF.HtmlToPdfConvertFile(ConfigurationManager.AppSettings["EXCEL_SHEET"] + htmfilename, ConfigurationManager.AppSettings["EXCEL_SHEET"] + pdffilename);
            
            //Code To upload generated POM pdf in document manager of selected case no : TUSHAR 4 JUNE
            if  (hdnPOMValue.Value=="Yes")
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
                            string szNodePath = _nf3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            szNodePath = szNodePath + "\\POM Generate\\" + i_pom_id +"\\" ;
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
            _reportBO.POMEntry(i_pom_id, DateTime.Today.ToString("MM/dd/yyyy"), Convert.ToInt32(ImageId[0].ToString()), pdffilename.ToString(), NodeIdPath, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, BillNo,"POMG");
            
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
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private DataSet CreateGroupData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds = new DataSet();
        try
        {
          
            DataTable dt = new DataTable();
            dt.Columns.Add("Bill No");
            dt.Columns.Add("Case Id");
            dt.Columns.Add("Patient Name");
            dt.Columns.Add("Speciality");
            dt.Columns.Add("Reffering Office");
            dt.Columns.Add("Bill Amount");
            //dt.Columns.Add("Visit Date");
            dt.Columns.Add("Bill Date");
            //dt.Columns.Add("Bill Staus Date");
            //dt.Columns.Add("Status");
            //dt.Columns.Add("Username");
            dt.Columns.Add("Insurance Claim No");
            dt.Columns.Add("Insurance Company");
            dt.Columns.Add("Insurance Address");
            dt.Columns.Add("Min Date Of Service");
            dt.Columns.Add("Max Date Of Service");
            dt.Columns.Add("CaseType Id");
            dt.Columns.Add("WC_ADDRESS");
            dt.Columns.Add("Case No");
            foreach (DataGridItem dg in grdAllReports.Items)
            {
                if (((CheckBox)dg.Cells[0].FindControl("chkSelect")).Checked == true)
                {

                    DataRow dr = dt.NewRow();
                    dr["Bill No"] = dg.Cells[19].Text; //17
                    dr["Case Id"] = dg.Cells[5].Text;
                    dr["Patient Name"] = dg.Cells[22].Text; //20
                    dr["Speciality"] = dg.Cells[27].Text;//25
                    dr["Reffering Office"] = dg.Cells[28].Text; //26
                    dr["Bill Amount"] = dg.Cells[15].Text;//13
                    //dr["Visit Date"] = dg.Cells[12].Text;
                    dr["Bill Date"] = dg.Cells[20].Text;//18
                    //dr["Bill Staus Date"] = dg.Cells[14].Text;
                    //dr["Status"] = dg.Cells[15].Text;
                    //dr["Username"] = dg.Cells[16].Text;
                    dr["Insurance Claim No"] = dg.Cells[25].Text;//23
                    dr["Insurance Company"] = dg.Cells[24].Text;//22
                    dr["Insurance Address"] = dg.Cells[16].Text;//14
                    dr["Min Date Of Service"] = dg.Cells[17].Text;//15
                    dr["Max Date Of Service"] = dg.Cells[18].Text;//16
                    dr["CaseType Id"] = dg.Cells[1].Text;
                    dr["WC_ADDRESS"] = dg.Cells[29].Text;//27
                    dr["Case No"] =dg.Cells[6].Text;//5
                    dt.Rows.Add(dr);

                }
            }
            if (dt.Rows.Count > 0)
            {
                //convert DataTable to DataView   
                DataView dv = dt.DefaultView;
                //apply the sort on CustomerSurname column   
                dv.Sort = "Reffering Office";
                //save our newly ordered results back into our datatable   
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
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        return ds;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected string ReplaceHeaderAndFooter(String szInput, string sz_bill_no,int i_pom_id)
    {
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
                //GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
                szFileData = szFileData.Replace("VL_SZ_POM_ID", i_pom_id.ToString());
            }
            else
            {
                szFileData = File.ReadAllText(ConfigurationManager.AppSettings["POM_HTML"]);

            }
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
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
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
    {
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
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally
        {
            if (objSqlConn.State == ConnectionState.Open)
            {
                objSqlConn.Close();
            }

        }
        return szReplaceString;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    //End Print POM


    //SORT LOGIC
    protected void grdAllReports_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Generate bill")
        {
            #region "Logic to view bills"
            Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
            DataSet objDS = new DataSet();
            objDS = objNF3Template.getBillList(e.Item.Cells[19].Text);
            if (objDS.Tables[0].Rows.Count > 1)
            {
                grdViewBills.DataSource = objDS;
                grdViewBills.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showviewBills();", true);
            }
            else if (objDS.Tables[0].Rows.Count == 1)
            {
                string szBillName = objDS.Tables[0].Rows[0]["PATH"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + szBillName + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "alert('No bill generated ...!');", true);
            }

            #endregion
        }
        DataView dv;
        DataSet ds = new DataSet();
        ds = (DataSet)Session["DataBind"];
        dv = ds.Tables[0].DefaultView;
        if (e.CommandName.ToString() == "Bill No")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text  = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "Bill Date")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + " DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "Chart No")
        {
          
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + " DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "Patient Name")
        {

            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + " DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "Reffering Office")
        {

            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + " DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "Insurance Company")
        {

            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + " DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }
        }
        else if (e.CommandName.ToString() == "Insurance Claim No")
        {

            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + " DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }
        }
        else if (e.CommandName.ToString() == "Speciality")
        {

            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + " DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }
        }


        dv.Sort = txtSort.Text;
        grdForExcel.DataSource = dv;
        grdForExcel.DataBind();
        grdForReport.DataSource = dv;
        grdForReport.DataBind();
        grdAllReports.DataSource = dv;
        grdAllReports.DataBind();

        
    }

    //END SORT LOGIC


    //Logic For Update Status
    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (extddlBillStatus.Text != "NA")
            {
                _reportBO = new Bill_Sys_ReportBO();
                ArrayList objAL = new ArrayList();
                string szListOfBillIDs = "";
                Boolean flag = false;
                for (int i = 0; i < grdAllReports.Items.Count; i++)
                {
                    CheckBox chkDelete1 = (CheckBox)grdAllReports.Items[i].FindControl("chkSelect");
                    if (chkDelete1.Checked)
                    {
                        if (flag == false)
                        {
                            szListOfBillIDs = "'" + grdAllReports.Items[i].Cells[19].Text + "'";//18
                            flag = true;
                        }
                        else
                        {
                            szListOfBillIDs = szListOfBillIDs + ",'" + grdAllReports.Items[i].Cells[19].Text + "'";//18
                        }
                    }
                }
                if (szListOfBillIDs != "")
                {
                    objAL.Add(extddlBillStatus.Text);
                    objAL.Add(szListOfBillIDs);
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    objAL.Add(txtBillStatusdate.Text);
                    objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    _reportBO.updateBillStatus(objAL);
                   lblMessage.Text = "Bill status updated successfully.";
                    lblMessage.Visible = true;
                    BindGrid();
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
    //end of Logic
    protected void btn_CreatePacket_Click(object sender, EventArgs e)
    {
        Image1.Visible = true;
        //Label15.Visible = true;                           
        Bill_Sys_RequiredDocumentBO _RequsredDocumentBO = new Bill_Sys_RequiredDocumentBO();
        ArrayList objArrayL;      
        objArrayL = new ArrayList();
        string flag = "1";
        for (int i = 0; i < grdAllReports.Items.Count; i++)
        { 
            CheckBox chk = (CheckBox)grdAllReports.Items[i].Cells[0].FindControl("chkSelect");
            if (chk.Checked == true)
            {
                flag = "0";
                Bill_Sys_Bill_Packet_Request _BillPacketRequest = new Bill_Sys_Bill_Packet_Request();
                _BillPacketRequest.SZ_CASE_ID = grdAllReports.Items[i].Cells[5].Text;
                _BillPacketRequest.SZ_BILL_NUMBER = grdAllReports.Items[i].Cells[19].Text;
                objArrayL.Add(_BillPacketRequest);
            }
        }
        if (flag.ToString() == "1")
        {
            Image1.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "CheckValidation();", true);
        }
        else
        {
            Session["PacketId"] = _RequsredDocumentBO.GetBillPacketRequest(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, objArrayL);
            Timer1.Enabled = true;
        }
       
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        DataSet objDSLT = new DataSet();
        Bill_Sys_RequiredDocumentBO _RequsredDocumentBO = new Bill_Sys_RequiredDocumentBO();
        string PhisicalPath = "";
        objDSLT = _RequsredDocumentBO.GetBillPacketRequestErrorStatus(Session["PacketId"].ToString());
        if (objDSLT.Tables[0].Rows[0][0].ToString() == "False" && objDSLT.Tables[0].Rows[0][1].ToString() == "False")
        {
        }//Process is Not Complete
        else if (objDSLT.Tables[0].Rows[0][0].ToString() == "True" && objDSLT.Tables[0].Rows[0][1].ToString() == "False")
        {
            Image1.Visible = false;
            PhisicalPath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + objDSLT.Tables[0].Rows[0][3];
            PhisicalPath = PhisicalPath + objDSLT.Tables[0].Rows[0][4];
            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + PhisicalPath.ToString() + "'); ", true);                      
            Timer1.Enabled = false;
            BindGrid();
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "openURL('" + PhisicalPath + "');", true);
        }//Process Is Complete Without Error
        else if (objDSLT.Tables[0].Rows[0][0].ToString() == "False" && objDSLT.Tables[0].Rows[0][1].ToString() == "True")
        {
            Span1.InnerHtml = objDSLT.Tables[0].Rows[0][2].ToString();
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "openErrorMessage();",  true);
            Timer1.Enabled = false;
            Image1.Visible = false;           
        }//Process Is Complete With Error    
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        hdnPOMValue.Value = "Yes";
        btnYes.Attributes.Add("onclick", "YesMassage");
        //document.getElementById('div1').style.visibility='hidden';   
        creatPDF();
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        hdnPOMValue.Value = "No";
        btnNo.Attributes.Add("onclick", "NoMassage");
        //document.getElementById('div1').style.visibility='hidden';   
        creatPDF();
    }

    public string GetBillStatusCode(string sz_Company_Id, string sz_Bill_Status_Code)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string StatusID = "";
     SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SP_GET_BILL_STATUS_CODE", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_Id);
            comm.Parameters.AddWithValue("@SZ_BILL_SATAUS_CODE", sz_Bill_Status_Code);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                StatusID = dr[0].ToString();
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
        finally { conn.Close();  }
        return StatusID;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}


