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
using PDFValueReplacement;
using mbs.LienBills;
using iTextSharp.text.pdf;


public partial class Bill_Sys_ReffPaidBills : PageBase
{
    private string SESSION_checks = "SESSION_Checked";
    private Bill_Sys_Menu _bill_Sys_Menu;
    private Bill_Sys_InsertDefaultValues objDefaultValue;
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    CaseDetailsBO objCaseDetailsBO;
    PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    Bill_Sys_NF3_Template objNF3Template;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    //Flag To Check Weather Insurance PopUp Ok Button Clicked Or Not
    string PopUpFlag;
    //End Code
    string bt_include;
    String str_1500;
    MUVGenerateFunction _MUVGenerateFunction;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            PopUpFlag = "true";
            //btnPerPatient.Attributes.Add("onclick", "return isRecordsselected();");
            btnRevert.Attributes.Add("onclick", "return isRecordsselected();");
            btnSelectedBill.Attributes.Add("onclick", "return isRecordsselected();");
            if (!IsPostBack)
            {
                Bill_Sys_ReportBO objreport = new Bill_Sys_ReportBO();
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlCaseType.Text = objreport.GetNoFaultId(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
                Session["RefGridData"] = null;
                BindReffGrid();
                Session[SESSION_checks] = null;
                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "0")
                {
                    grdAllReports.Columns[3].Visible = false;
                    grdEEBillSearch.Columns[1].Visible = false;
                }

                setLabels();
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

    public void BindReffGrid()
    {
        DataSet dset = new DataSet();
        Bill_Sys_ReportBO objreport = new Bill_Sys_ReportBO();
        if (extddlCaseType.Text != "" && extddlCaseType.Text != "NA")
        {
            dset = objreport.GetReffPaidBillReportALL(txtCompanyID.Text, extddlCaseType.Text);
        }
        else
        {
            dset = objreport.GetReffPaidBillReport(txtCompanyID.Text);
        }
        if (dset.Tables.Count > 0)
        {
            grdAllReports.DataSource = dset;
            grdAllReports.DataBind();
            grdEEBillSearch.DataSource = dset;
            grdEEBillSearch.DataBind();
            Session["RefGridData"] = dset;
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
            lblMsg.Visible = false;
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
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdEEBillSearch.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdEEBillSearch.Columns.Count; i++)
                    {
                        if (grdEEBillSearch.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdEEBillSearch.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdEEBillSearch.Columns.Count; j++)
                {
                    if (grdEEBillSearch.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdEEBillSearch.Items[icount].Cells[j].Text);
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


    //added by shailesh 28april2010
    // change to show the confirm box of insurance id or address or chart no, before proceeding futhur to generate bill
    protected void btnSelectedBill_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //added by shailesh 28April2010
            //code to check whether select patient has the insurance company and insurance adress
            AddBillSameCase();

            BindReffGrid();
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
       
        finally
        {
            Session["Procedure_Code"] = null; 
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }



    #region "Logic to Generate PDF"

    private void GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality)
    //private void GenerateAddedBillPDF(string p_szBillNumber)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _MUVGenerateFunction = new MUVGenerateFunction();
            String szSpecility = p_szSpeciality;
            //String szSpecility = "MRI";
            //Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            Session["TM_SZ_CASE_ID"] = txtCaseID.Text;
            Session["TM_SZ_BILL_ID"] = p_szBillNumber;

            objNF3Template = new Bill_Sys_NF3_Template();

            CaseDetailsBO objCaseDetails = new CaseDetailsBO();
            String szCompanyID = "";
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
            {
                szCompanyID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            }
            else
            {
                szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }

            if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
            {
                String szDefaultPath = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szDefaultPath = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                else
                {
                    szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }

                String szSourceDir = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szSourceDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                else
                {
                    szSourceDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }

                String szDestinationDir = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szDestinationDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
                    //szDestinationDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/";

                }
                else
                {
                    szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
                    //szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/";

                }

                string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();

                string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                String szFile3 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                String szFile4 = ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                String szPDFPage1 = "";
                String szXMLFileName;
                String szOriginalPDFFileName;
                String szLastXMLFileName;
                String szLastOriginalPDFFileName;
                String sz3and4Page = "";
                Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
                String szDiagPDFFilePosition = objConfiguration.getConfigurationSettings(szCompanyID, "GET_DIAG_PAGE_POSITION");
                String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(szCompanyID, "DIAG_PAGE");

                szXMLFileName = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                szOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                szLastXMLFileName = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                szLastOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();


                Boolean fAddDiag = true;

               

                GeneratePDFFile.GenerateNF3PDF objGeneratePDF = new GeneratePDFFile.GenerateNF3PDF();
                objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();

                String szPDFFileName = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    // Note : Generate PDF with Billing Information table. **** II
                    szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID, _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID), ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);
                    //log.Debug("Bill Details PDF File : " + szPDFFileName);
                }
                else
                {
                    // Note : Generate PDF with Billing Information table. **** II
                    szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);
                    //log.Debug("Bill Details PDF File : " + szPDFFileName);
                }


                sz3and4Page = szFile3;//""objPDFReplacement.Merge3and4Page(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szFile3, szFile4);

                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID), Session["TM_SZ_CASE_ID"].ToString());
                }
                else
                {
                    szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
                }


                //log.Debug("Page1 : " + szPDFPage1);


                // Merge **** I AND **** II
                String szPDF_1_3 = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    // (                                                string p_szCompanyID,                                                           string p_szCompanyName,                                                             string p_szCaseID,                  string p_szBillID,              string p_szFile1, string p_szFile2)
                    szPDF_1_3 = objPDFReplacement.MergePDFFiles(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID, _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID), Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDFPage1, szPDFFileName);
                }
                else
                {
                    // (                                                string p_szCompanyID,                                                           string p_szCompanyName,                                                             string p_szCaseID,                  string p_szBillID,              string p_szFile1, string p_szFile2)
                    szPDF_1_3 = objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDFPage1, szPDFFileName);
                }



                String szLastPDFFileName = "";
                String szPDFPage3 = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szPDFPage3 = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, szLastOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID), Session["TM_SZ_CASE_ID"].ToString());
                }
                else
                {
                    szPDFPage3 = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, szLastOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
                }


                //Tushar
                string bt_CaseType;
                string strComp = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                bt_include = _MUVGenerateFunction.get_bt_include(strComp, szSpecility, "", "Speciality");
                bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000002", "CaseType");                
                if (bt_include == "True" && bt_CaseType == "True")
                {
                    str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                }


                MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3.Replace(".pdf", "_MER.pdf"));

                szLastPDFFileName = szPDFPage3.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, szPDFPage3);


                //Tushar
                if (bt_include == "True" && bt_CaseType == "True")
                {
                    MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szLastPDFFileName, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500.Replace(".pdf", "_MER.pdf"));
                    szLastPDFFileName = str_1500.Replace(".pdf", "_MER.pdf");
                }
                //


                //szLastPDFFileName = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDefaultPath + szLastPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
                String szGenereatedFileName = "";
                szGenereatedFileName = szDefaultPath + szLastPDFFileName;
                //log.Debug("GenereatedFileName : " + szGenereatedFileName);


                String szOpenFilePath = "";
                szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szGenereatedFileName;
                //szOpenFilePath = "C:\\LawAllies\\MBSUpload\\" + szGenereatedFileName;

                string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + "/" + szGenereatedFileName;


                CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
                string newPdfFilename = "";
                string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                objMyForm.initialize(KeyForCutePDF);

                if (objMyForm == null)
                {
                    // Response.Write("objMyForm not initialized");
                }
                else
                {
                    if (System.IO.File.Exists(szFileNameWithFullPath))
                    {
                        //    objMyForm.mergePDF(szFileNameWithFullPath, szFileNameWithFullPath.Replace(".pdf", "_Temp.pdf").ToString(), szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString());

                        if (szGenerateNextDiagPage != Bill_Sys_Constant.constGenerateNextDiagPage && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= 5 && szDiagPDFFilePosition == Bill_Sys_Constant.constAT_THE_END)
                        {
                            if (szGenerateNextDiagPage == "CI_0000004" && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) == 5)
                            {
                            }
                            else
                            {
                                //MergePDF.MergePDFFiles(szFileNameWithFullPath, objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString());
                                szPDFFileName = szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf");
                            }
                        }

                    }

                }

                // End Logic

                string szFileNameForSaving = "";

                // Save Entry in Table
                if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                {
                    szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();
                }

                // End

                if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString()))
                {
                    szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString();
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                }
                else
                {
                    if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                    }
                    else
                    {
                        szFileNameForSaving = szOpenFilePath.ToString();
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                    }
                }
                String[] szTemp;
                string szBillName = "";
                szTemp = szFileNameForSaving.Split('/');
                ArrayList objAL = new ArrayList();
                szFileNameForSaving = szFileNameForSaving.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                szBillName = szTemp[szTemp.Length - 1].ToString();

                if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + szBillName))
                {
                    if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                    {
                        Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                    }
                    File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + szBillName, objNF3Template.getPhysicalPath() + szDestinationDir + szBillName);
                }

                objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                objAL.Add(szDestinationDir + szBillName);
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                }
                else
                {
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                }
                objAL.Add(Session["TM_SZ_CASE_ID"]);
                objAL.Add(szTemp[szTemp.Length - 1].ToString());
                objAL.Add(szDestinationDir);
                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                objAL.Add(szSpecility);
                //objAL.Add("");
                objAL.Add("NF");
                objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                //objAL.Add(txtCaseNo.Text);
                objNF3Template.saveGeneratedBillPath(objAL);

                // Start : Save Notes for Bill.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }

                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                //BindLatestTransaction();

                // End 


            }
            else if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
            {
                Bill_Sys_PVT_Template _objPvtBill;
                _objPvtBill = new Bill_Sys_PVT_Template();
                bool isReferingFacility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string szCompanyId;
                string szCaseId = Session["TM_SZ_CASE_ID"].ToString();
               // string szSpecility ;
                string szCompanyName;
                string szBillId = Session["TM_SZ_BILL_ID"].ToString();
                string szUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                string szUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szCompanyName = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    szCompanyId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    szCompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    szCompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                _objPvtBill.GeneratePVTBill(isReferingFacility, szCompanyId, szCaseId, szSpecility, szCompanyName, szBillId, szUserName, szUserId);
                
                #region Generate Bill For Private cases
                //String szDefaultPath = "";
                //String szLastPDFFileName = "";
                //String szDestinationDir = "";
                //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                //{
                //    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                //    szDestinationDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
                //    //szDestinationDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/";
                //}
                //else
                //{
                //    szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
                //    //szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/";
                //}
                //String szSourceDir = "";
                //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                //{
                //    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                //    szSourceDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                //}
                //else
                //{
                //    szSourceDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                //}
                //String szGenereatedFileName = "";
               
               

                //objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                //string szXMLFileName;
                //string szOriginalPDFFileName;
                //szXMLFileName = ConfigurationManager.AppSettings["HCFA1500_XML_FILE"].ToString();
                //szOriginalPDFFileName = ConfigurationManager.AppSettings["HCFA1500_PDF_FILE"].ToString();
                //String szPDFPage = "";
                //szPDFPage = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());


                //#region File saving logic
                //String szOpenFilePath = "";
                //szGenereatedFileName =szDestinationDir + szPDFPage;
                //szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szGenereatedFileName;                               
                //string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + "/" + szGenereatedFileName;
                //string szFileNameForSaving = "";

                //// Save Entry in Table
                //if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                //{
                //    szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();
                //}
                //// End

                //if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString()))
                //{
                //    szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString();
                //    //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                //}
                //else
                //{
                //    if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                //    {
                //        szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
                //        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                //    }
                //    else
                //    {
                //        szFileNameForSaving = szOpenFilePath.ToString();
                //        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                //    }
                //}
                //String[] szTemp;
                //string szBillName = "";
                //szTemp = szFileNameForSaving.Split('/');
                //ArrayList objAL = new ArrayList();
                //szFileNameForSaving = szFileNameForSaving.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                //szBillName = szTemp[szTemp.Length - 1].ToString();

                //if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + szBillName))
                //{
                //    if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                //    {
                //        Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                //    }
                //    File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + szBillName, objNF3Template.getPhysicalPath() + szDestinationDir + szBillName);
                //}

                //objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                //objAL.Add(szDestinationDir + szBillName);
                //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                //{
                //    objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                //}
                //else
                //{
                //    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                //}
                //objAL.Add(Session["TM_SZ_CASE_ID"]);
                //objAL.Add(szTemp[szTemp.Length - 1].ToString());
                //objAL.Add(szDestinationDir);
                //objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                //objAL.Add(szSpecility);
                ////objAL.Add("");
                //objAL.Add("PVT");
                //objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
                ////objAL.Add(txtCaseNo.Text);
                //objNF3Template.saveGeneratedBillPath(objAL);

                //// Start : Save Notes for Bill.

                //_DAO_NOTES_EO = new DAO_NOTES_EO();
                //_DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                //_DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                //_DAO_NOTES_BO = new DAO_NOTES_BO();
                //_DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                //_DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                //_DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                //{
                //    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                //}
                //else
                //{
                //    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //}

                //_DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //#endregion

                ////Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szPDFPage + "'); ", true);

                #endregion
            }
            else if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000004")
            {
                String szDestinationDir = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szDestinationDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
                    //szDestinationDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/";

                }
                else
                {
                    szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
                    //szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/";

                }
                String szDefaultPath = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szDefaultPath = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                else
                {
                    szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }

                Bill_Sys_PVT_Template _objPvtBill;
                _objPvtBill = new Bill_Sys_PVT_Template();
                bool isReferingFacility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string szCompanyId;
                string szCaseId = Session["TM_SZ_CASE_ID"].ToString();
                // string szSpecility ;
                string szCompanyName;
                string szBillId = Session["TM_SZ_BILL_ID"].ToString();
                string szUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                string szUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
               
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szCompanyName = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    szCompanyId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    szCompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    szCompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                mbs.LienBills.Lien obj = new Lien();
                _MUVGenerateFunction = new MUVGenerateFunction();
                objNF3Template = new Bill_Sys_NF3_Template();
                 string path;
                    //Tushar
                    string bt_CaseType;
                    string strComp = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    bt_include = _MUVGenerateFunction.get_bt_include(strComp, szSpecility, "", "Speciality");
                    bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000004", "CaseType");
                    if (bt_include == "True" && bt_CaseType == "True")
                    {
                        str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                        String FileName;
                        FileName = obj.GenratePdfForLienWithMuv(szCompanyId, szBillId, szSpecility, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, szUserName, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO, szUserId);
                        MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDestinationDir + FileName, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                        path = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf");

                        ArrayList objAL = new ArrayList();
                        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                        objAL.Add(szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAL.Add(Session["TM_SZ_CASE_ID"]);
                        objAL.Add(str_1500.Replace(".pdf", "_MER.pdf"));
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(szSpecility);
                        objAL.Add("NF");
                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                        objNF3Template.saveGeneratedBillPath(objAL);

                        // Start : Save Notes for Bill.

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                    }
                    else
                    {
                        path = obj.GenratePdfForLien(szCompanyId, szBillId, szSpecility, szCaseId, szUserName, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO, szUserId);
                    }
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + path + "');", true);
            }
            else if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000001")
            {
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    string UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    string CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    string CaseId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    string CaseNO = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                    // string speciality = Session["WC_Speciality"].ToString();
                    WC_Bill_Generation _objNFBill = new WC_Bill_Generation();

                    //string szFinalPath = _objNFBill.GeneratePDFForWorkerComp(hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, rdbListPDFType.SelectedValue, szCompanyID, CmpName, UserId, UserName, CaseNO, hdnSpeciality.Value.ToString(), 1);
                    string szFinalPath = _objNFBill.GeneratePDFForReferalWorkerComp((string)Session["TM_SZ_BILL_ID"], ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, szCompanyID, CmpName, UserId, UserName, p_szSpeciality);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szFinalPath + "');", true);

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
                }
            }
            Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
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
    #endregion


   

    //added by shailesh 28april2010
    // change to show the confirm box of insurance id or address or chart no, before proceeding futhur to generate bill
    protected void btnPerPatient_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        lblMsg.Text = "";
        try
        {
            ArrayList arrlst = new ArrayList();
            foreach (DataGridItem item in grdAllReports.Items)
            {

                CheckBox chk = (CheckBox)(item.Cells[0].FindControl("chkSelect"));
                if (chk.Checked == true)
                {
                    if (item.Cells[20].Text == "&nbsp;" || item.Cells[21].Text == "&nbsp;" || item.Cells[22].Text == "&nbsp;")
                    {
                        arrlst.Add(item.Cells[17].Text);
                    }
                }
            }
            string patientcnt = "";
            string repeat = "";
            if (arrlst.Count > 0)
            {
                for (int i = 0; i < arrlst.Count; i++)
                {
                    int cnt = 0;
                    repeat = arrlst[i].ToString();
                    patientcnt += arrlst[i].ToString() + ",";
                    for (int j = 0; j < arrlst.Count; j++)
                    {
                        if (repeat == arrlst[j].ToString())
                        {
                            cnt++;
                        }
                    }
                    if (cnt > 1)
                    {
                        i += cnt - 1;
                    }
                }
                popupmsg.InnerText = "You do not have a claim number or an insurance company or an insurance company address added to these Case NO: '" + patientcnt + "'.You cannot proceed furher";
                Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage1();</script>");
            }
            else
            {
                AddBillDiffCase();
            }
            BindReffGrid();
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
        
        finally
        {
            Session["Procedure_Code"] = null;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    #region "Sorting Logic for Grid"



    #endregion


    protected void grdAllReports_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //Code To Open Popup Received Report Popup
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        if (e.CommandName.ToString() == "Edit")
        {
            bool _ischeck = false;
            string _caseID = "";
            int _isSameCaseID = 0;
            try
            {
                Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();

                _dianosis_Association.EventProcID = e.Item.Cells[19].Text;
                _dianosis_Association.DoctorID = e.Item.Cells[16].Text;
                _dianosis_Association.CaseID = e.Item.Cells[1].Text;
                _dianosis_Association.ProceuderGroupId = e.Item.Cells[23].Text;
                Session["DIAGNOS_ASSOCIATION"] = _dianosis_Association;

                Page.RegisterStartupScript("mm", "<script language='javascript'>showReceiveDocumentPopup();</script>");
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
           
        }
        else if (e.CommandName.ToString() == "PatientClick")
        {

            CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
            _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[2].Text; // PATIENT ID
            _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[1].Text; // CASE ID
            _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[17].Text; // CASE ID
            _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[24].Text; // PATIENT NAME
            _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)_bill_Sys_BillingCompanyDetails_BO.getCompanyDetailsOfCase(e.Item.Cells[1].Text)).SZ_COMPANY_ID; // COMPANY ID
            Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

            Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
            _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[1].Text; // CASE ID 

            Session["CASEINFO"] = _bill_Sys_Case;
            Response.Redirect("Bill_Sys_ReCaseDetails.aspx", false);
        }
        else
        {
            //End Code
            DataView dv;
            DataSet ds = new DataSet();
            ds = (DataSet)Session["RefGridData"];
            dv = ds.Tables[0].DefaultView;

            if (e.CommandName.ToString() == "ChartNumberSort")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }
            }
            else if (e.CommandName.ToString() == "PatientNameSort")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }
            }
            else if (e.CommandName.ToString() == "DateOfServiceSort")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }
            }
            else if (e.CommandName.ToString() == "CaseNoSort")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }
            }
            dv.Sort = txtSort.Text;
            grdAllReports.DataSource = dv;
            grdAllReports.DataBind();
            grdEEBillSearch.DataSource = dv;
            grdEEBillSearch.DataBind();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //added by shailesh 28april2010
    //call function to generate bills for similar patient on OK click of confirmbox to proceed futhur.

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            PopUpFlag = "false";
            AddBillSameCase();
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

    //added by shailesh 28april2010
    //call function to generate bills for different patient on OK click of confirmbox to proceed futhur.

    //protected void btnOK1_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        AddBillDiffCase();
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //}


    //added by shailesh 28April2010
    // function to add bill for same patient name
    public void AddBillSameCase()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblMsg.Visible = false;
            lblMsg.Text = "";
            string sz_compID = "";
            DataSet dset0;
            DataSet dset1;
            ArrayList arrbill = new ArrayList();
            ArrayList arrlst;
            ArrayList objArr;
            ArrayList _arraylist;
            Bill_Sys_ReportBO objreport;
            DataTable dtble = new DataTable();
            string patientID = "";
            int flag = 0;
            int _insertFlag = 0;
            string billno = "";
            string PatientTreatmentID = "";

            //Added Flag And Procedure Group :-Tushar
            int flag1 = 0;
            string Proc_Group_Id = "";
            //end

            foreach (DataGridItem item in grdAllReports.Items)
            {

                CheckBox chk = (CheckBox)(item.Cells[0].FindControl("chkSelect"));
                if (chk.Checked == true)
                {
                    patientID = item.Cells[2].Text;
                }
            }


            foreach (DataGridItem j in grdAllReports.Items)
            {
                CheckBox chkSelect = (CheckBox)(j.Cells[0].FindControl("chkSelect"));
                if (chkSelect.Checked == true)
                {
                    if (patientID == j.Cells[2].Text)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 2;
                        break;
                    }
                }
            }
            if (flag == 2)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "mm", "<script language='javascript'>alert('Select visits for same patient');</script>");
                lblMsg.Text = "Select visits for same patient";
                lblMsg.Visible = false;
            }
            else if (flag == 1)
            {
                ArrayList arrlst1 = new ArrayList();
                foreach (DataGridItem item in grdAllReports.Items)
                {

                    CheckBox chk = (CheckBox)(item.Cells[0].FindControl("chkSelect"));
                    if (chk.Checked == true)
                    {
                        if (item.Cells[20].Text == "&nbsp;" || item.Cells[21].Text == "&nbsp;" || item.Cells[22].Text == "&nbsp;")
                        {
                            arrlst1.Add(item.Cells[17].Text);
                        }
                    }
                }
                string patientcnt = "";
                string repeat = "";
                if (arrlst1.Count > 0 && PopUpFlag == "true")
                {
                    for (int i = 0; i < arrlst1.Count; i++)
                    {
                        int cnt = 0;
                        repeat = arrlst1[i].ToString();
                        patientcnt += arrlst1[i].ToString() + ",";
                        for (int j = 0; j < arrlst1.Count; j++)
                        {
                            if (repeat == arrlst1[j].ToString())
                            {
                                cnt++;
                            }
                        }
                        if (cnt > 1)
                        {
                            i += cnt - 1;
                        }
                    }
                    msgPatientExists.InnerText = "You do not have a claim number or an insurance company or an insurance company address added to these Case NO '" + patientcnt + "'. You cannot proceed furher.";
                    Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage();</script>");
                    lblMsg.Text = "You do not have a claim number";
                    lblMsg.Visible = false;
                }
                else
                {
                    PopUpFlag = "true";
                    //to check weather procedure group is same or not:-Tushar
                    foreach (DataGridItem item in grdAllReports.Items)
                    {

                        CheckBox chk = (CheckBox)(item.Cells[0].FindControl("chkSelect"));
                        if (chk.Checked == true)
                        {
                            Proc_Group_Id = item.Cells[23].Text;
                            Session["Procedure_Code"] = Proc_Group_Id;
                        }
                    }



                    foreach (DataGridItem j in grdAllReports.Items)
                    {
                        CheckBox chkSelect = (CheckBox)(j.Cells[0].FindControl("chkSelect"));
                        if (chkSelect.Checked == true)
                        {
                            if (Proc_Group_Id == j.Cells[23].Text)
                            {
                                flag1 = 1;
                            }
                            else
                            {
                                flag1 = 2;
                                break;
                            }
                        }
                    }

                    if (flag1 == 2)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "mm", "<script language='javascript'>alert('Select visits for same Speciality');</script>");
                        lblMsg.Text = "Select visits for same Speciality";
                        lblMsg.Visible = false;
                    }

                    else
                    {
                        //end Of Code



                        objreport = new Bill_Sys_ReportBO();
                        dtble = objreport.GetDoctorID(txtCompanyID.Text);
                        Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();

                        //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                        //{
                        //    txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        //}

                        foreach (DataGridItem ditem in grdAllReports.Items)
                        {

                            CheckBox chkSelect = (CheckBox)(ditem.Cells[0].FindControl("chkSelect"));
                            if (chkSelect.Checked == true)
                            {

                                _insertFlag = 1;
                                txtCaseID.Text = ditem.Cells[1].Text;
                                txtReadingDocID.Text = ditem.Cells[16].Text;
                                txtPatientID.Text = ditem.Cells[2].Text;
                                txtCaseNo.Text = ditem.Cells[17].Text;
                                sz_compID = ditem.Cells[18].Text;
                                //txtTypeCodeID.Text = ditem.Cells[11].Text;
                                //objArr = new ArrayList();
                                //objArr.Add(ditem.Cells[8].Text);
                                //objArr.Add(ditem.Cells[9].Text);
                                //objArr.Add(txtCompanyID.Text);
                                //objreport = new Bill_Sys_ReportBO();
                                //dset1 = new DataSet();
                                //dset1 = objreport.GetProcCodeDetails(objArr);

                                //_arraylist = new ArrayList();
                                //_arraylist.Add(dset1.Tables[0].Rows[0]["PROC_ID"].ToString());
                                //_arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                //_arraylist.Add(billno);
                                //_arraylist.Add(ditem.Cells[5].Text);
                                //_arraylist.Add(txtCompanyID.Text);
                                //_arraylist.Add("1");
                                //_arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                //_arraylist.Add("1");
                                //_arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                //_arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                //_arraylist.Add(dtble.Rows[0]["CODE"].ToString());
                                //_arraylist.Add(ditem.Cells[1].Text);
                                //_arraylist.Add(ditem.Cells[11].Text);
                                //_arraylist.Add("");
                                //_arraylist.Add("");
                                //_bill_Sys_BillTransaction.SaveTransactionData(_arrayList);
                            }
                        }

                        Bill_Sys_CaseObject obj1 = new Bill_Sys_CaseObject();
                        obj1.SZ_CASE_ID = txtCaseID.Text;
                        obj1.SZ_COMAPNY_ID = sz_compID;
                        obj1.SZ_CASE_NO = txtCaseNo.Text;
                        obj1.SZ_PATIENT_ID = txtPatientID.Text;
                        Session["CASE_OBJECT"] = obj1;

                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                        {
                            txtRefCompanyID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                        }
                        else
                        {
                            txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        }

                        if (_insertFlag == 1)
                        {
                            dset0 = new DataSet();
                            dset0 = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagnosisCode(Proc_Group_Id, txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                            if (dset0.Tables[0].Rows.Count > 0)
                            {
                                arrlst = new ArrayList();
                                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                objreport = new Bill_Sys_ReportBO();
                                arrlst.Add(txtCaseID.Text);
                                arrlst.Add(txtBillDate.Text);
                                arrlst.Add(txtCompanyID.Text);
                                arrlst.Add(dtble.Rows[0]["CODE"].ToString());
                                arrlst.Add("0");
                                arrlst.Add(txtReadingDocID.Text);
                                arrlst.Add(txtRefCompanyID.Text);
                                arrlst.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                                //TUSHAR:- To Save Procedure Group Id In Txn_Bill_Transaction Table
                                arrlst.Add(Session["Procedure_Code"].ToString());
                                //End
                                objreport.InsertBillTransactionData(arrlst);


                                _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                                billno = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());

                                _DAO_NOTES_EO = new DAO_NOTES_EO();
                                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

                                _DAO_NOTES_BO = new DAO_NOTES_BO();
                                _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                                _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                                _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                                foreach (DataGridItem ditem1 in grdAllReports.Items)
                                {
                                    PatientTreatmentID = "";
                                    arrlst = new ArrayList();
                                    CheckBox chkSelect = (CheckBox)(ditem1.Cells[0].FindControl("chkSelect"));
                                    if (chkSelect.Checked == true)
                                    {

                                        objreport = new Bill_Sys_ReportBO();
                                        arrlst = new ArrayList();
                                        arrlst.Add(txtPatientID.Text);
                                        arrlst.Add(ditem1.Cells[14].Text);
                                        arrlst.Add(txtCompanyID.Text);
                                        arrlst.Add(dtble.Rows[0]["CODE"].ToString());
                                        PatientTreatmentID = objreport.GetTreatmentID(arrlst);

                                        objArr = new ArrayList();
                                        objArr.Add(ditem1.Cells[9].Text);
                                        objArr.Add(ditem1.Cells[10].Text);
                                        objArr.Add(txtCompanyID.Text);
                                        objreport = new Bill_Sys_ReportBO();
                                        dset1 = new DataSet();
                                        dset1 = objreport.GetProcCodeDetails(objArr);

                                        _arraylist = new ArrayList();
                                        _arraylist.Add(dset1.Tables[0].Rows[0]["PROC_ID"].ToString());
                                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                        _arraylist.Add(billno);
                                        _arraylist.Add(ditem1.Cells[5].Text);
                                        _arraylist.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_EMG_BILL == "True")
                                        {
                                            if (ditem1.Cells[27].Text.ToString() == "" || ditem1.Cells[27].Text.ToString() == null || ditem1.Cells[27].Text.ToString() == "&nbsp;")
                                            {
                                                _arraylist.Add("1");
                                            }
                                            else
                                            {
                                                _arraylist.Add(ditem1.Cells[27].Text);
                                            }

                                        }
                                        else
                                        {
                                            _arraylist.Add("1");
                                        }
                                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                        _arraylist.Add("1");
                                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                        _arraylist.Add(dtble.Rows[0]["CODE"].ToString());
                                        _arraylist.Add(ditem1.Cells[1].Text);
                                        _arraylist.Add(ditem1.Cells[14].Text);
                                        _arraylist.Add("");
                                        _arraylist.Add("");
                                        //_arraylist.Add(PatientTreatmentID);
                                        _arraylist.Add(ditem1.Cells[19].Text);
                                        _bill_Sys_BillTransaction.SaveTransactionData(_arraylist);
                                    }
                                }

                                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
                                if (dset0.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < dset0.Tables[0].Rows.Count; i++)
                                    {
                                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                        _arraylist = new ArrayList();
                                        _arraylist.Add(dset0.Tables[0].Rows[i]["CODE"].ToString());
                                        _arraylist.Add(billno);
                                        _bill_Sys_BillTransaction.SaveTransactionDiagnosis(_arraylist);
                                    }
                                }

                                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                                {
                                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                    string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                                    //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID));
                                    GenerateAddedBillPDF(billno, sz_speciality);
                                    arrbill.Add(txtCaseNo.Text);
                                }
                                else
                                {
                                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                    string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                                    //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                                    GenerateAddedBillPDF(billno, sz_speciality);
                                    arrbill.Add(txtCaseNo.Text);
                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('No diagnosis code assign for the patient');</script>");
                                lblMsg.Text = "No diagnosis code assign for th";
                                lblMsg.Visible = false;
                            }
                        }
                    }
                 
                    if (arrbill.Count > 0)
                    {
                        string caseno = arrbill[0].ToString();
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('Bill Created Successfully For " + caseno + " CaseNo');</script>");
                    }
                    BindReffGrid();
                    setLabels();
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

    // added by shailesh 28april2010
    // function to add multiple bill
    #region "Generate Bill According to patient
    public void AddBillDiffCase()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblMsg.Visible = true;
            DataSet dset0;
            DataSet dset1;

            ArrayList arrdiag = new ArrayList();
            ArrayList arrbill = new ArrayList();
            ArrayList arrlst;
            ArrayList objArr;
            ArrayList _arraylist;

            string sz_compID = "";
            Bill_Sys_ReportBO objreport;
            DataTable dtble = new DataTable();
            string patientID = "";
            int flag = 0;
            int _insertFlag = 0;
            string billno = "";
            string PatientTreatmentID = "";
            int cnt = 0; // 
            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();

            DataTable dt = new DataTable();
            dt.Columns.Add("SZ_CASE_ID");
            dt.Columns.Add("SZ_PATIENT_ID");
            dt.Columns.Add("CHART NO");
            dt.Columns.Add("PATIENT NAME");
            dt.Columns.Add("DATE OF SERVICE");
            dt.Columns.Add("Patient name");
            dt.Columns.Add("Date Of Service");
            dt.Columns.Add("Procedure code");
            dt.Columns.Add("Description");
            dt.Columns.Add("Status");
            dt.Columns.Add("Code ID");
            dt.Columns.Add("EVENT ID");
            dt.Columns.Add("Doctor ID");
            dt.Columns.Add("CASE NO");
            dt.Columns.Add("Company ID");
            dt.Columns.Add("SZ_PATIENT_TREATMENT_ID");
            //
            dt.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            //
            dt.Columns.Add("SZ_STUDY_NUMBER");
            DataRow dr;

            objreport = new Bill_Sys_ReportBO();
            // 
            dtble = objreport.GetDoctorID(txtCompanyID.Text);
            foreach (DataGridItem ditem in grdAllReports.Items)
            {

                CheckBox chkSelect = (CheckBox)(ditem.Cells[0].FindControl("chkSelect"));
                if (chkSelect.Checked == true)
                {
                    dr = dt.NewRow();
                    dr["SZ_CASE_ID"] = ditem.Cells[1].Text;
                    dr["SZ_PATIENT_ID"] = ditem.Cells[2].Text;
                    dr["CHART NO"] = ditem.Cells[3].Text;
                    dr["PATIENT NAME"] = ditem.Cells[24].Text;
                    dr["DATE OF SERVICE"] = ditem.Cells[5].Text;
                    dr["Patient name"] = ditem.Cells[24].Text;
                    dr["Date Of Service"] = ditem.Cells[5].Text;
                    dr["Procedure code"] = ditem.Cells[9].Text;
                    dr["Description"] = ditem.Cells[10].Text;
                    dr["Status"] = ditem.Cells[13].Text;
                    dr["Code ID"] = ditem.Cells[14].Text;
                    dr["EVENT ID"] = ditem.Cells[15].Text;
                    dr["Doctor ID"] = ditem.Cells[16].Text;
                    dr["CASE NO"] = ditem.Cells[17].Text;
                    dr["Company ID"] = ditem.Cells[18].Text;
                    dr["SZ_PATIENT_TREATMENT_ID"] = ditem.Cells[19].Text;
                    //
                    dr["SZ_PROCEDURE_GROUP_ID"] = ditem.Cells[23].Text;
                    dr["SZ_STUDY_NUMBER"] = ditem.Cells[27].Text;

                    //
                    dt.Rows.Add(dr);
                }
            }
            dt.DefaultView.Sort = "SZ_CASE_ID ASC";
            //Session["test"] = dt;
            objreport = new Bill_Sys_ReportBO();
            dtble = objreport.GetDoctorID(txtCompanyID.Text);
            int c = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //
                Session["Procedure_Code"] = dt.Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString(); ;
                //
                cnt = 0;
                string Pid = dt.Rows[i]["SZ_PATIENT_ID"].ToString();
                String szSpecialityID = dt.Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                foreach (DataRow drow in dt.Rows)
                {
                    if (Pid == drow["SZ_PATIENT_ID"].ToString() && szSpecialityID == drow["SZ_PROCEDURE_GROUP_ID"].ToString())
                    {
                        cnt++;
                    }
                }
                if (cnt == 1)
                {
                    txtCaseID.Text = dt.Rows[i]["SZ_CASE_ID"].ToString();
                    txtReadingDocID.Text = dt.Rows[i]["Doctor ID"].ToString();
                    txtPatientID.Text = dt.Rows[i]["SZ_PATIENT_ID"].ToString();
                    txtCaseNo.Text = dt.Rows[i]["CASE NO"].ToString();
                    sz_compID = dt.Rows[i]["Company ID"].ToString();

                    Bill_Sys_CaseObject obj1 = new Bill_Sys_CaseObject();
                    obj1.SZ_CASE_ID = txtCaseID.Text;
                    obj1.SZ_COMAPNY_ID = sz_compID;
                    obj1.SZ_CASE_NO = txtCaseNo.Text;
                    obj1.SZ_PATIENT_ID = txtPatientID.Text;
                    Session["CASE_OBJECT"] = obj1;

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                    {
                        txtRefCompanyID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }


                    dset0 = new DataSet();
                    dset0 = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagnosisCode(szSpecialityID, txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                    if (dset0.Tables[0].Rows.Count > 0)
                    {
                        arrlst = new ArrayList();
                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        objreport = new Bill_Sys_ReportBO();
                        arrlst.Add(txtCaseID.Text);
                        arrlst.Add(txtBillDate.Text);
                        arrlst.Add(txtCompanyID.Text);
                        arrlst.Add(dtble.Rows[0]["CODE"].ToString());
                        arrlst.Add("0");
                        arrlst.Add(txtReadingDocID.Text);
                        arrlst.Add(txtRefCompanyID.Text);
                        arrlst.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                        //TUSHAR:- To Save Procedure Group Id In Txn_Bill_Transaction Table
                        arrlst.Add(Session["Procedure_Code"].ToString());
                        //End
                        objreport.InsertBillTransactionData(arrlst);

                        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                        billno = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                        objreport = new Bill_Sys_ReportBO();
                        arrlst = new ArrayList();
                        arrlst.Add(txtPatientID.Text);
                        arrlst.Add(dt.Rows[i]["Code ID"].ToString());
                        arrlst.Add(txtCompanyID.Text);
                        arrlst.Add(dtble.Rows[0]["CODE"].ToString());
                        PatientTreatmentID = objreport.GetTreatmentID(arrlst);

                        objArr = new ArrayList();
                        objArr.Add(dt.Rows[i]["Procedure code"].ToString());
                        objArr.Add(dt.Rows[i]["Description"].ToString());
                        objArr.Add(txtCompanyID.Text);
                        objreport = new Bill_Sys_ReportBO();
                        dset1 = new DataSet();
                        dset1 = objreport.GetProcCodeDetails(objArr);

                        _arraylist = new ArrayList();
                        _arraylist.Add(dset1.Tables[0].Rows[0]["PROC_ID"].ToString());
                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                        _arraylist.Add(billno);
                        _arraylist.Add(dt.Rows[i]["DATE OF SERVICE"].ToString());
                        _arraylist.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        //atul
                        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_EMG_BILL == "True")
                        {
                            if (dt.Rows[i]["SZ_STUDY_NUMBER"].ToString() == "" || dt.Rows[i]["SZ_STUDY_NUMBER"].ToString() == null || dt.Rows[i]["SZ_STUDY_NUMBER"].ToString() == "&nbsp;")
                            {
                                _arraylist.Add("1");
                            }
                            else
                            {
                                _arraylist.Add(dt.Rows[i]["SZ_STUDY_NUMBER"].ToString());
                            }
                        }
                        else
                        {
                            _arraylist.Add("1");
                        }
                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                        _arraylist.Add("1");
                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                        _arraylist.Add(dtble.Rows[0]["CODE"].ToString());
                        _arraylist.Add(dt.Rows[i]["SZ_CASE_ID"].ToString());
                        _arraylist.Add(dt.Rows[i]["Code ID"].ToString());
                        _arraylist.Add("");
                        _arraylist.Add("");
                        _arraylist.Add(dt.Rows[i]["SZ_PATIENT_TREATMENT_ID"].ToString());
                        _bill_Sys_BillTransaction.SaveTransactionData(_arraylist);

                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
                        if (dset0.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dset0.Tables[0].Rows.Count; j++)
                            {
                                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                _arraylist = new ArrayList();
                                _arraylist.Add(dset0.Tables[0].Rows[j]["CODE"].ToString());
                                _arraylist.Add(billno);
                                _bill_Sys_BillTransaction.SaveTransactionDiagnosis(_arraylist);
                            }
                        }

                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                        {
                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                            //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID));
                            GenerateAddedBillPDF(billno, sz_speciality);
                            arrbill.Add(dt.Rows[i]["CASE NO"].ToString());
                        }
                        else
                        {
                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                            //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                            GenerateAddedBillPDF(billno, sz_speciality);
                            arrbill.Add(dt.Rows[i]["CASE NO"].ToString());
                        }
                    }
                    else
                    {

                        //objArr = new ArrayList();
                        arrdiag.Add(dt.Rows[i]["CASE NO"].ToString());
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('No diagnosis code assign for the patient');</script>");
                    }
                }
                else if (cnt > 1)
                {

                    txtCaseID.Text = dt.Rows[i]["SZ_CASE_ID"].ToString();
                    txtReadingDocID.Text = dt.Rows[i]["Doctor ID"].ToString();
                    txtPatientID.Text = dt.Rows[i]["SZ_PATIENT_ID"].ToString();
                    txtCaseNo.Text = dt.Rows[i]["CASE NO"].ToString();
                    sz_compID = dt.Rows[i]["Company ID"].ToString();

                    Bill_Sys_CaseObject obj1 = new Bill_Sys_CaseObject();
                    obj1.SZ_CASE_ID = txtCaseID.Text;
                    obj1.SZ_COMAPNY_ID = sz_compID;
                    obj1.SZ_CASE_NO = txtCaseNo.Text;
                    obj1.SZ_PATIENT_ID = txtPatientID.Text;
                    Session["CASE_OBJECT"] = obj1;

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                    {
                        txtRefCompanyID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }


                    dset0 = new DataSet();
                    dset0 = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagnosisCode(dt.Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString(), txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                    if (dset0.Tables[0].Rows.Count > 0)
                    {
                        arrlst = new ArrayList();
                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        objreport = new Bill_Sys_ReportBO();
                        arrlst.Add(txtCaseID.Text);
                        arrlst.Add(txtBillDate.Text);
                        arrlst.Add(txtCompanyID.Text);
                        arrlst.Add(dtble.Rows[0]["CODE"].ToString());
                        arrlst.Add("0");
                        arrlst.Add(txtReadingDocID.Text);
                        arrlst.Add(txtRefCompanyID.Text);
                        arrlst.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                        //TUSHAR:- To Save Procedure Group Id In Txn_Bill_Transaction Table
                        arrlst.Add(Session["Procedure_Code"].ToString());
                        //End
                        objreport.InsertBillTransactionData(arrlst);

                        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                        billno = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                        objreport = new Bill_Sys_ReportBO();
                        arrlst = new ArrayList();
                        arrlst.Add(txtPatientID.Text);
                        arrlst.Add(dt.Rows[i]["Code ID"].ToString());
                        arrlst.Add(txtCompanyID.Text);
                        arrlst.Add(dtble.Rows[0]["CODE"].ToString());
                        PatientTreatmentID = objreport.GetTreatmentID(arrlst);

                        objArr = new ArrayList();
                        objArr.Add(dt.Rows[i]["Procedure code"].ToString());
                        objArr.Add(dt.Rows[i]["Description"].ToString());
                        objArr.Add(txtCompanyID.Text);
                        objreport = new Bill_Sys_ReportBO();
                        dset1 = new DataSet();
                        dset1 = objreport.GetProcCodeDetails(objArr);

                        for (int k = 0; k < cnt; k++)
                        {
                            //
                            //if ((k + i) < cnt)
                            //{
                            //    if (dt.Rows[k + i]["SZ_PROCEDURE_GROUP_ID"].ToString() == Session["Procedure_Code"].ToString())
                            //    {
                            //
                            objArr = new ArrayList();
                            objArr.Add(dt.Rows[k + i]["Procedure code"].ToString());
                            objArr.Add(dt.Rows[k + i]["Description"].ToString());
                            objArr.Add(txtCompanyID.Text);
                            objreport = new Bill_Sys_ReportBO();
                            dset1 = new DataSet();
                            dset1 = objreport.GetProcCodeDetails(objArr);

                            _arraylist = new ArrayList();
                            _arraylist.Add(dset1.Tables[0].Rows[0]["PROC_ID"].ToString());
                            _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                            _arraylist.Add(billno);
                            _arraylist.Add(dt.Rows[k + i]["DATE OF SERVICE"].ToString());
                            _arraylist.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);


                            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_EMG_BILL == "True")
                            {
                                if (dt.Rows[k + i]["SZ_STUDY_NUMBER"].ToString() == "" || dt.Rows[k + i]["SZ_STUDY_NUMBER"].ToString() == null || dt.Rows[k + i]["SZ_STUDY_NUMBER"].ToString() == "&nbsp;")
                                {
                                    _arraylist.Add("1");
                                }
                                else
                                {

                                    _arraylist.Add(dt.Rows[k + i]["SZ_STUDY_NUMBER"].ToString());
                                }

                            }
                            else
                            {
                                _arraylist.Add("1");
                            }
                            _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                            _arraylist.Add("1");
                            _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                            _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                            _arraylist.Add(dtble.Rows[0]["CODE"].ToString());
                            _arraylist.Add(dt.Rows[k + i]["SZ_CASE_ID"].ToString());
                            _arraylist.Add(dt.Rows[k + i]["Code ID"].ToString());
                            _arraylist.Add("");
                            _arraylist.Add("");
                            //_arraylist.Add(PatientTreatmentID);
                            _arraylist.Add(dt.Rows[k + i]["SZ_PATIENT_TREATMENT_ID"].ToString());
                            _bill_Sys_BillTransaction.SaveTransactionData(_arraylist);
                            // c = c + 1;
                            //    }
                            //}
                        }

                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
                        if (dset0.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dset0.Tables[0].Rows.Count; j++)
                            {
                                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                _arraylist = new ArrayList();
                                _arraylist.Add(dset0.Tables[0].Rows[j]["CODE"].ToString());
                                _arraylist.Add(billno);
                                _bill_Sys_BillTransaction.SaveTransactionDiagnosis(_arraylist);
                            }
                        }

                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                        {
                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                            //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID));
                            GenerateAddedBillPDF(billno, sz_speciality);
                            arrbill.Add(dt.Rows[i]["CASE NO"].ToString());
                        }
                        else
                        {
                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                            //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                            GenerateAddedBillPDF(billno, sz_speciality);
                            arrbill.Add(dt.Rows[i]["CASE NO"].ToString());
                        }

                    }
                    else
                    {

                        //objArr = new ArrayList();
                        arrdiag.Add(dt.Rows[i]["CASE NO"].ToString());
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('No diagnosis code assign for the patient');</script>");
                    }
                    // i = cnt - 1;
                    i += cnt - 1;

                }

            }
            string Billno = "";
            if (arrbill.Count > 0)
            {

                for (int l = 0; l < arrbill.Count; l++)
                {
                    if (l == arrbill.Count - 1)
                    {
                        Billno += arrbill[l].ToString();
                    }
                    else
                    {
                        Billno += arrbill[l].ToString() + ", ";
                    }

                }
                // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert(' create bill for  case id');</script>"); 
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('Bill Created Successfully For " + Billno + " CaseNo');</script>");

            }
            string patientcnt = "";
            if (arrdiag.Count > 0)
            {
                for (int l = 0; l < arrdiag.Count; l++)
                {
                    patientcnt += arrdiag[l].ToString() + ", ";
                }

                lblMsg.Text = "Cannot create bill for '" + patientcnt + "' as no diagnosis code assign for the patient";
                lblMsg.Visible = true;
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('Cannot create bill for '" + patientcnt + "' as no diagnosis code assign for the patient');</script>");
            }
         
            BindReffGrid();
            setLabels();
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
    #endregion

    #region "Bind Label For Dash Board"
    protected void setLabels()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO _obj = new DashBoardBO();
        Bill_Sys_BillTransaction_BO _billTransactionBO = new Bill_Sys_BillTransaction_BO();
        try
        {
            DayOfWeek day = Convert.ToDateTime(System.DateTime.Today.ToString()).DayOfWeek;
            int days = day - DayOfWeek.Sunday;

            DateTime start = Convert.ToDateTime(System.DateTime.Today.ToString()).AddDays(-days);
            DateTime end = start.AddDays(6);

            lblAppointmentToday.Text = _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "GET_APPOINTMENT");
            lblAppointmentWeek.Text = _obj.getAppoinmentCount(start.ToString(), end.ToString(), txtCompanyID.Text, "GET_APPOINTMENT");

            lblBillStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=Paid' onclick=\"javascript:OpenPage('Paid');\" > " + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_PAID_LIST_COUNT", txtCompanyID.Text) + "</a>";
            lblBillStatus.Text += " Paid Bills  </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=UnPaid' onclick=\"javascript:OpenPage('UnPaid');\" > " + _billTransactionBO.GetCaseCount("SP_MST_CASE_MASTER", "GET_UNPAID_LIST_COUNT", txtCompanyID.Text) + "</a> Un-Paid Bills </li></ul>";

            lblDesk.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_LitigationDesk.aspx?Type=Litigation' onclick=\"javascript:OpenPage('Litigation');\" > " + _billTransactionBO.GetCaseCount("SP_LITIGATION_WRITEOFF_DESK", "GET_LETIGATION_COUNT", txtCompanyID.Text) + "</a>" + " bills due for litigation";

            //lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=MissingProvider' onclick=\"javascript:OpenPage('MissingProvider');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_PROVIDER") + "</a>";
            //lblMissingInformation.Text += " provider information missing  </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
            lblMissingInformation.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingInsuranceCompany' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_INSURANCE_COMPANY") + "</a> ";
            lblMissingInformation.Text += " insurance company missing </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_ATTORNEY") + "</a>";
            lblMissingInformation.Text += " attorney missing </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_CLAIM_NUMBER") + "</a> claim number missing </li>";
            lblMissingInformation.Text += "<li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingReportNumber' onclick=\"javascript:OpenPage('MissingReportNumber');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_REPORT_NUMBER") + "</a> report number missing </li>";
            lblMissingInformation.Text += "<li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingPolicyHolder'> " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_POLICY_HOLDER") + "</a> policy holder missing </li>";
            lblMissingInformation.Text += "<li> <a href='Bill_Sys_ShowUnSentNF2.aspx' > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "UNSENTNF2") + "</a> unsent NF2 </li></ul>";



            // lblReport.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=R' onclick=\"javascript:OpenPage('Litigation');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_RECEIVED_COUNT") + "</a>" + " Received Report";
            lblReport.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='Bill_Sys_ReffPaidBills.aspx' onclick=\"javascript:OpenPage('Litigation');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_RECEIVED_COUNT") + "</a>" + " Received Report";
            lblReport.Text += "</li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=report&Type=P' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "DOCUMENT_PENDING_COUNT") + "</a> Pending Report </li></ul>";

            lblProcedureStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li>" + _obj.getBilledUnbilledProcCode(txtCompanyID.Text, "GET_BILLEDPROC") + " billed procedure codes";
            lblProcedureStatus.Text += "</li>  <li>" + _obj.getBilledUnbilledProcCode(txtCompanyID.Text, "GET_UNBILLEDPROC") + " Un-billed procedure codes </li></ul>";


            //lblVisits.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a id='hlnkTotalVisit' href='#' onclick='javascript:OpenTotalVisitPopup();'>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_VISIT_COUNT") + " </a> Visits</li>";
            //lblVisits.Text += "<li><a id='hlnkVisit' href='#' onclick='javascript:OpenVisitPopup();'>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_BILLED_VISIT_COUNT") + " </a>Billed visits </li>";
            //lblVisits.Text += "<li><a id='hlnkUnVisit' href='#' onclick='javascript:OpenUnVisitPopup();'>" + _obj.getTotalVisits(txtCompanyID.Text, "GET_UNBILLED_VISIT_COUNT") + "</a> Un-billed visits </a></li></ul>";

            lblTotalVisit.Text = _obj.getTotalVisits(txtCompanyID.Text, "GET_VISIT_COUNT");
            lblBilledVisit.Text = _obj.getTotalVisits(txtCompanyID.Text, "GET_BILLED_VISIT_COUNT");
            lblUnBilledVisit.Text = _obj.getTotalVisits(txtCompanyID.Text, "GET_UNBILLED_VISIT_COUNT");

            // 8 April - add patient visit status block on page - sachin
            lblPatientVisitStatus.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientscheduled' onclick=\"javascript:OpenPage('PatientScheduled');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_SCHEDULED_COUNT") + "</a> ";
            lblPatientVisitStatus.Text += " Patient Scheduled </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientnoshows' onclick=\"javascript:OpenPage('PatientNoShows');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_NO_SHOWS") + "</a>";
            lblPatientVisitStatus.Text += " Patient No Shows </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientrescheduled' onclick=\"javascript:OpenPage('PatientRescheduled');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_RESCHEDULED") + "</a>";
            lblPatientVisitStatus.Text += " Patient Rescheduled </li>  <li> <a href='Bill_Sys_ShowPatientVisitStatus.aspx?Flag=patientvisitcompleted' onclick=\"javascript:OpenPage('PatientVisitcompleted');\" > " + _obj.getPatientVisitStatusCount(txtCompanyID.Text, "GET_PATIENT_VISIT_COMPLETED") + "</a>Patient Visit completed </li></ul>";

            ConfigDashBoard();

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
    private void ConfigDashBoard()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO _objDashBoardBO = new DashBoardBO();
        try
        {

            DataTable dt = _objDashBoardBO.GetConfigDashBoard(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE);
            foreach (DataRow dr in dt.Rows)
            {
                switch (dr[0].ToString())
                {
                    case "Daily Appointment":
                        tblDailyAppointment.Visible = true;
                        break;
                    case "Weekly Appointment":
                        tblWeeklyAppointment.Visible = true;
                        break;
                    case "Bill Status":
                        tblBillStatus.Visible = true;
                        break;
                    case "Desk":
                        tblDesk.Visible = true;
                        break;
                    case "Missing Information":
                        tblMissingInfo.Visible = true;
                        break;
                    case "Report Section":
                        tblReportSection.Visible = true;
                        break;
                    case "Procedure Status":
                        tblBilledUnbilledProcCode.Visible = true;
                        break;
                    case "Visits":
                        tblVisits.Visible = true;
                        grdTotalVisit.DataSource = _objDashBoardBO.getVisitDetails(txtCompanyID.Text, "TOTALCOUNT");
                        grdTotalVisit.DataBind();
                        grdVisit.DataSource = _objDashBoardBO.getVisitDetails(txtCompanyID.Text, "BILLEDVISIT");
                        grdVisit.DataBind();
                        grdUnVisit.DataSource = _objDashBoardBO.getVisitDetails(txtCompanyID.Text, "UNBILLEDVISIT");
                        grdUnVisit.DataBind();
                        break;
                    case "Missing Speciality":
                        tblMissingSpeciality.Visible = true;
                        break;
                    case "Patient Visit Status":
                        tblPatientVisitStatus.Visible = true;
                        break;

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
    #endregion


    protected void btnRevert_Click(object sender, EventArgs e)
    {  ///revert the recored 
        for (int j = 0; j < grdAllReports.Items.Count; j++)
        {
            CheckBox chkDelete1 = (CheckBox)grdAllReports.Items[j].FindControl("chkSelect");
            if (chkDelete1.Checked)
            {

                Bill_Sys_ReportBO objUpdateReport = new Bill_Sys_ReportBO();
                objUpdateReport.RevertReport(Convert.ToInt32(grdAllReports.Items[j].Cells[19].Text.Trim().ToString()));
                lblMsg.Text = "Report Reverted Successfully";
                lblMsg.Visible = true;
            }

        }
        BindReffGrid();
        setLabels();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindReffGrid();
    }

}

