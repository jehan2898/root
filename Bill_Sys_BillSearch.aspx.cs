/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_CaseDetails.aspx.cs
/*Purpose              :       show grid row color if bill status 'Billed By BSBG
/*Author               :       Sandeep y
/*Date of creation     :       17 Dec 2008  
/*Modified By          :       Prashant zope
/*Modified Date        :       4 may 2010
/************************************************************/

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
using Componend;
using PDFValueReplacement;
using System.IO;
using System.Text;



public partial class Bill_Sys_BillSearch : PageBase
{
   
    private ListOperation _listOperation;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    Bill_Sys_Case _bill_Sys_Case;
    CaseDetailsBO objCaseDetailsBO;
    Bill_Sys_NF3_Template objNF3Template;
    PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    Bill_Sys_BillTransaction_BO objBillTransactionBO;
    private DAO_NOTES_EO _DAO_NOTES_EO;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            btnDelete.Attributes.Add("onclick", "return confirm_bill_delete();");
            btnUpdateStatus.Attributes.Add("onclick", "return confirm_update_bill_status();");
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
         //   imgbtnFromDate.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtFromDate,'imgbtnFromDate','MM/dd/yyyy'); return false;");
         //   imgbtnToDate.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtToDate,'imgbtnToDate','MM/dd/yyyy'); return false;");
            if (Request.QueryString["CaseID"] != null)
            {
               Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
               CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(Request.QueryString["CaseID"].ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = Request.QueryString["CaseID"].ToString();
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = Request.QueryString["pname"].ToString();
                _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, Request.QueryString["cmpid"].ToString());

                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = Request.QueryString["CaseID"].ToString();

                Session["CASEINFO"] = _bill_Sys_Case;
            }
            
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlProvider.Flag_ID = txtCompanyID.Text;
            
            if (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE == "USR00003")
            {
                extddlProvider.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_PROVIDER_ID;
                extddlProvider.Enabled = false;
            }
            if (!IsPostBack)
            {
                txtBillStatusdate.Text = System.DateTime.Today.ToShortDateString();
                extddlBillStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if (Session["SZ_CASE_ID"] != null)
                {
                    if (Session["SZ_CASE_ID"] != "")
                    {
                        txtCaseID.Text = Session["SZ_CASE_ID"].ToString();
                        txtCaseID.ReadOnly = true;
                        Session["SZ_CASE_ID"] = "";
                        GetPatientDeskList();
                    }
                }
                if (Request.QueryString["fromCase"] != null)
                {
                    if (Request.QueryString["fromCase"].ToString() == "true")
                    {
                        if (Session["CASE_OBJECT"] != null)
                        {
                            txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                            txtCaseID.ReadOnly = true;
                            txtCaseNO.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                            txtCaseNO.ReadOnly = true;
                            Session["SZ_CASE_ID"] = "";
                            GetPatientDeskList();
                        }
                    }
                }
             // Sandeep Y
                 BindGrid();
                
            }
            //foreach (DataGridItem objItem in grdBillSearch.Items)
            //{
            //    if (objItem.Cells[8].Text == "" || objItem.Cells[8].Text == "&nbsp;" || objItem.Cells[8].Text == "0.00")
            //    {
            //        objItem.Cells[11].Text = "";
            //    }
            //}
            objCaseDetailsBO = new CaseDetailsBO();
            foreach (DataGridItem objItem in grdBillSearch.Items)
            {
                if (objCaseDetailsBO.GetCaseType(objItem.Cells[2].Text) != "WC000000000000000001")
                {
                    objItem.Cells[17].Text = "";
                    objItem.Cells[18].Text = "";
                    objItem.Cells[19].Text = "";
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
       

        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_BillSearch.aspx");
            //  string strButtonList = cv.VisibleButton("Bill_Sys_BillSearch.aspx");
            // cv.VisibleButton("Bill_Sys_BillSearch.aspx");            
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        { 
            //Session["PassedCaseID"] = null;
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

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _listOperation = new ListOperation();
        try
        {
            //_listOperation.WebPage = this.Page;
            //_listOperation.Xml_File = "BillSearch.xml";
            //_listOperation.LoadList();

            BillSearchDAO obj = new BillSearchDAO();
            ArrayList objAL = new ArrayList();
            objAL.Add(txtBillNumber.Text);
            objAL.Add(txtCaseNO.Text);
            objAL.Add(txtFromDate.Text);
            objAL.Add(txtToDate.Text);
            objAL.Add(txtCompanyID.Text);
            
            // Check for prefix
            if (txtCaseNO.Text != "")
            {
                String szEnteredCaseNO = txtCaseNO.Text;
                if (!Char.IsNumber(szEnteredCaseNO[0]))
                {
                    objAL.Add("ST");
                }
                else
                {
                    objAL.Add("");
                }
            }
            else
            {
                objAL.Add("");
            }
            
            DataSet objResultDataSet = new DataSet();
            objResultDataSet = obj.getBillSearchList(objAL);
            Decimal SumOfBillAmount = 0.00M;
            Decimal SumOfOutstandingAmount = 0.00M;
            for (int i = 0; i < objResultDataSet.Tables[0].Rows.Count; i++)
            { 
               SumOfBillAmount  = SumOfBillAmount + Convert.ToDecimal(objResultDataSet.Tables[0].Rows[i]["FLT_BILL_AMOUNT"].ToString());
               SumOfOutstandingAmount = SumOfOutstandingAmount + Convert.ToDecimal(objResultDataSet.Tables[0].Rows[i]["FLT_BALANCE"].ToString());
            }
           

            lblTotalBillAmount.Text = "Total Bill Amount = <b>" + Decimal.Round(SumOfBillAmount,2) + "<//b>";
            lblTotalOutstandingAmount.Text = "Total Outstanding Amount = <b>" + Decimal.Round(SumOfOutstandingAmount, 2) + "<//b>";
            grdBillSearch.DataSource = objResultDataSet;
            grdBillSearch.DataBind();
            grdForReport.DataSource = objResultDataSet;
            grdForReport.DataBind();

            for (int j = 0; j < grdBillSearch.Items.Count; j++) // Check Bill status 'Billed By BSBG' and show green color row
            {

                
                //if (grdBillSearch.Items[j].Cells[8].Text.Equals("BS000000000000000063"))
                if (grdBillSearch.Items[j].Cells[8].Text.Equals("Billed By BSBG")) 
                {
                    grdBillSearch.Items[j].BackColor = System.Drawing.Color.LawnGreen;
                }
                if (grdBillSearch.Items[j].Cells[8].Text.Equals("Billed By KH")) 
                {
                    grdBillSearch.Items[j].BackColor = System.Drawing.Color.Yellow;
                }
                
            }
           
           

            if (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE_NAME != "ADMIN")
            {
                grdBillSearch.Columns[0].Visible = false;
                grdBillSearch.Columns[15].Visible = false;
                grdBillSearch.Columns[17].Visible = false;

                grdForReport.Columns[0].Visible = false;
                grdForReport.Columns[15].Visible = false;
                grdForReport.Columns[17].Visible = false; 
               // btnDelete.Visible = false;
            }
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY== true)
            {
                grdBillSearch.Columns[5].Visible = false;
                grdForReport.Columns[5].Visible = false;
              
            }
            //foreach (DataGridItem objItem in grdBillSearch.Items)
            //{
            //    if (objItem.Cells[8].Text == "" || objItem.Cells[8].Text == "&nbsp;" || objItem.Cells[8].Text == "0.00")
            //    {
            //        objItem.Cells[11].Text = "";
            //    }
            //}

            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_VIEW_BILL != "True")
            {
                grdBillSearch.Columns[20].Visible = false;
                grdForReport.Columns[20].Visible = false;
            }
                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_DELETE_BILLS == "True")
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdBillSearch.CurrentPageIndex = 0;
            BindGrid();
            lblMsg.Visible = false;
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

    protected void grdBillSearch_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdBillSearch.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
            lblMsg.Visible = false;
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
    
    protected void grdBillSearch_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Add IC9 Code")
            {
                Session["PassedBillID"] = e.CommandArgument;
                Response.Redirect("Bill_Sys_BillIC9Code.aspx", false);
            }
            if (e.CommandName.ToString() == "Generate bill")
            {
                #region "Generate Bill Login but we need to view bills"

                //Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                //Session["TM_SZ_BILL_ID"] = e.Item.Cells[2].Text;
                //CaseDetailsBO objCaseDetails = new CaseDetailsBO();
                //String szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
                //{
                //    String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                //    string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();

                //    string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                //    String szFile3 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                //    String szFile4 = ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                //    String szPDFPage1;
                //    String szXMLFileName;
                //    String szOriginalPDFFileName;
                //    String szLastXMLFileName;
                //    String szLastOriginalPDFFileName;
                //    String sz3and4Page;
                //    Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
                //    String szDiagPDFFilePosition = objConfiguration.getConfigurationSettings(szCompanyID, "GET_DIAG_PAGE_POSITION");
                //    String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(szCompanyID, "DIAG_PAGE");

                //    szXMLFileName = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                //    szOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                //    szLastXMLFileName = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                //    szLastOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();

                //    objNF3Template = new Bill_Sys_NF3_Template();
                //    Boolean fAddDiag = true;

                //    GeneratePDFFile.GenerateNF3PDF objGeneratePDF = new GeneratePDFFile.GenerateNF3PDF();
                //    objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();



                //    // Note : Generate PDF with Billing Information table. **** II
                //    String szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);


                //    sz3and4Page = szFile3;//""objPDFReplacement.Merge3and4Page(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szFile3, szFile4);

                //    szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());



                //    // Merge **** I AND **** II
                //    String szPDF_1_3;
                //    // (                                                string p_szCompanyID,                                                           string p_szCompanyName,                                                             string p_szCaseID,                  string p_szBillID,              string p_szFile1, string p_szFile2)
                //    szPDF_1_3 = objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDFPage1, szPDFFileName);

                //    String szLastPDFFileName;
                //    String szPDFPage3;
                //    szPDFPage3 = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, szLastOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());

                //    MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3.Replace(".pdf", "_MER.pdf"));

                //    szLastPDFFileName = szPDFPage3.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, szPDFPage3);
                //    //szLastPDFFileName = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDefaultPath + szLastPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
                //    String szGenereatedFileName = "";
                //    szGenereatedFileName = szDefaultPath + szLastPDFFileName;



                //    String szOpenFilePath = "";
                //    szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szGenereatedFileName;


                //    string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + "/" + szGenereatedFileName;


                //    CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
                //    string newPdfFilename = "";
                //    string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                //    objMyForm.initialize(KeyForCutePDF);

                //    if (objMyForm == null)
                //    {
                //        // Response.Write("objMyForm not initialized");
                //    }
                //    else
                //    {
                //        if (System.IO.File.Exists(szFileNameWithFullPath))
                //        {
                //            //    objMyForm.mergePDF(szFileNameWithFullPath, szFileNameWithFullPath.Replace(".pdf", "_Temp.pdf").ToString(), szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString());

                //            if (szGenerateNextDiagPage != Bill_Sys_Constant.constGenerateNextDiagPage && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= 5 && szDiagPDFFilePosition == Bill_Sys_Constant.constAT_THE_END)
                //            {
                //                if (szGenerateNextDiagPage == "CI_0000004" && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) == 5)
                //                {
                //                }
                //                else
                //                {
                //                    //MergePDF.MergePDFFiles(szFileNameWithFullPath, objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString());
                //                    szPDFFileName = szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf");
                //                }
                //            }

                //        }

                //    }

                //    // End Logic

                //    string szFileNameForSaving = "";

                //    // Save Entry in Table
                //    if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                //    {
                //        szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();
                //    }

                //    // End

                //    if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString()))
                //    {
                //        szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString();
                //        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                //    }
                //    else
                //    {
                //        if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                //        {
                //            szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                //        }
                //        else
                //        {
                //            szFileNameForSaving = szOpenFilePath.ToString();
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                //        }
                //    }

                //    ArrayList objAL = new ArrayList();
                //    szFileNameForSaving = szFileNameForSaving.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                //    objAL.Add(szFileNameForSaving);
                //    objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                //    objAL.Add(Session["TM_SZ_CASE_ID"].ToString());
                //    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                //    objNF3Template.saveGeneratedNF3File(objAL);
                //}
                //else
                //{
                //    //Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                //    //Session["TM_SZ_BILL_ID"] = e.Item.Cells[2].Text;
                //    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
                //}


                ////     Response.Redirect("TemplateManager/Bill_Sys_GeneratePDF.aspx", false);

                #endregion


                #region "Logic to view bills"
                objNF3Template = new Bill_Sys_NF3_Template();
                DataSet objDS = new DataSet();
                objDS = objNF3Template.getBillList(e.Item.Cells[2].Text);
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
            if (e.CommandName.ToString() == "Deniel")
            {
                Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                Session["TM_SZ_BILL_ID"] = e.Item.Cells[2].Text;
                //string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                //GeneratePDFFile.GeneratePDF objGeneratePDF = new GeneratePDFFile.GeneratePDF();

                //String szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_Denial.aspx'); ", true);

                //     Response.Redirect("TemplateManager/Bill_Sys_GeneratePDF.aspx", false);
            }
            if (e.CommandName.ToString() == "Make Payment")
            {

                if (e.Item.Cells[13].Text.ToString() != "" && e.Item.Cells[13].Text.ToString() != "&nbsp;" )
                {
                    //if (e.Item.Cells[18].Text.ToString() != "1" && e.Item.Cells[18].Text.ToString() != "2")
                    //{

                        //if (Convert.ToDecimal(e.Item.Cells[13].Text.ToString()) > 0)
                        //{

                            _bill_Sys_Case = new Bill_Sys_Case();
                            _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[3].Text.ToString();
                            CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                            _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.Item.Cells[3].Text.ToString(), "");
                            _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[3].Text.ToString();
                            _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                            _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                            _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[4].Text.ToString();
                            Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                            Session["CASEINFO"] = _bill_Sys_Case;
                            Session["PassedCaseID"] = e.Item.Cells[3].Text;
                            Session["SZ_BILL_NUMBER"] = e.CommandArgument;

                            Session["PassedBillID"] = e.CommandArgument;
                            Session["Balance"] = e.Item.Cells[13].Text;

                            Response.Redirect("Bill_Sys_PaymentTransactions.aspx", false);

                        //}
                        //else
                        //{
                        //    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Payment is completed for this bill!'); ", true);
                        //}
                    //}
                    //else
                    //{
                    //    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Bill is litigated or write off.You can not process this bill!'); ", true);
                    //}
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Please first add services to bill!'); ", true);
                }
            }
            if (e.CommandName.ToString() == "Edit")
            {
                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[3].Text.ToString();
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.Item.Cells[3].Text.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[3].Text.ToString();
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[4].Text.ToString();
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Session["CASEINFO"] = _bill_Sys_Case;
                Session["PassedCaseID"] = e.Item.Cells[3].Text;
                Session["SZ_BILL_NUMBER"] = e.CommandArgument;
                Response.Redirect("Bill_Sys_BillTransaction.aspx?Type=Search", false);
            }

            if (e.CommandName.ToString() == "Doctor's Initial Report")
            {
                Session["TEMPLATE_BILL_NO"] = e.Item.Cells[2].Text;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformation.aspx'); ", true);
                //     Response.Redirect("TemplateManager/Bill_Sys_GeneratePDF.aspx", false);
            }
            if (e.CommandName.ToString() == "Doctor's Progress Report")
            {
                Session["TEMPLATE_BILL_NO"] = e.Item.Cells[2].Text;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_2.aspx'); ", true);
                //     Response.Redirect("TemplateManager/Bill_Sys_GeneratePDF.aspx", false);
            }
            if (e.CommandName.ToString() == "Doctor's Report Of MMI")
            {
                Session["TEMPLATE_BILL_NO"] = e.Item.Cells[2].Text;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_3.aspx'); ", true);
                //     Response.Redirect("TemplateManager/Bill_Sys_GeneratePDF.aspx", false);
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

    

    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtBillNumber.Text = "";
            lblMsg.Visible = false;
            txtFromDate.Text = "";
            txtToDate.Text = "";
            Session["SZ_CASE_ID"] = "";
            //if (Session["PassedCaseID"] != null)
            //{
            //    txtCaseID.Text = Session["PassedCaseID"].ToString();
            //    txtCaseID.ReadOnly = true;
            //}
            //else
            //{
            //    txtCaseID.Text = "";
            //}
            if (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE == "USR00003")
            {
                extddlProvider.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_PROVIDER_ID;
                extddlProvider.Enabled = false;
                //extddlProvider.Visible = false;
            }
            else
            {
                extddlProvider.Text = "NA";
            }
            grdBillSearch.CurrentPageIndex = 0;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnBulkPayment_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList _arrlist;
        ArrayList _arrFalselist;
       // bool _return=false;
        try
        {
            _arrlist = new ArrayList();
          
            foreach (DataGridItem gridItem in this.grdBillSearch.Items)
            {
                CheckBox grdChk = (CheckBox)gridItem.Cells[0].Controls[1];
                if (grdChk.Checked)
                {
                   
                        _arrlist.Add(gridItem.Cells[2].Text);
                       // _return = true;

                   
                   
                }
            }

            if (_arrlist.Count > 0 )
            {
                Session["BulkData"] = _arrlist;
                Response.Redirect("Bill_Sys_BulkPayment.aspx", false);
            }
            //else if ( _arrFalselist.Count > 0)
            //{
            //    string _str = "";

            //    foreach (Object obj in _arrFalselist)
            //    {
            //        _str = _str + obj.ToString() + ",";
            //    }
            //    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('" + _str + " bills are litigated or write off.You can not process this bills!'); ", true);
            //}
            else if (_arrlist.Count == 0)
            {
               
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Please select bills!'); ", true);
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

    protected void grdBillSearch_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            objCaseDetailsBO = new CaseDetailsBO();
            //if (e.Item.Cells[8].Text == "" || e.Item.Cells[8].Text == "&nbsp;" || e.Item.Cells[8].Text == "0.00")
            //{
            //    e.Item.Cells[11].Text = "";
            //}
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_NEW_BILL != "True")
            {
                LinkButton lnk = (LinkButton) e.Item.FindControl("lnkSelectCase");
                if (lnk != null)
                {
                    lnk.Enabled = false;
                }
            }           
          
            if (objCaseDetailsBO.GetCaseType(e.Item.Cells[2].Text) != "WC000000000000000001")
            {
                e.Item.Cells[17].Text = "";
                e.Item.Cells[18].Text = "";
                e.Item.Cells[19].Text = "";  
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
   
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        objBillTransactionBO = new Bill_Sys_BillTransaction_BO();
        string szVisitStatus = lblVisitStatus.Value;
        Result objResult = new Result();
        string BillStatus = "";
        int Flag = 0;
        try
        {
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == false)
            {
                foreach (DataGridItem gridItem in this.grdBillSearch.Items)
                {
                    CheckBox grdChk = (CheckBox)gridItem.FindControl("ChkDelete");
                    if (grdChk.Checked)
                    {
                        
                        if (gridItem.Cells[25].Text != "BLD")
                        {
                            BillStatus += gridItem.Cells[2].Text + ",";
                            Flag = 1;
                        }
                       
                    }
                }
                if (Flag == 1)
                {
                    Page.RegisterStartupScript("ss", "<script language='javascript'> alert('You can not delete Bill No. " + BillStatus.ToString() + "');</script>");
                    
                }
                else
                {
  
                    foreach (DataGridItem gridItem in this.grdBillSearch.Items)
                    {
                       
                        
                        CheckBox grdChk = (CheckBox)gridItem.FindControl("ChkDelete");
                        if (grdChk.Checked)
                        {
                            objBillTransactionBO.DeleteBillRecord(gridItem.Cells[2].Text, txtCompanyID.Text, lblVisitStatus.Value);

                            // Start : Save Notes for Bill.
                            DAO_NOTES_EO _DAO_NOTES_EO = new DAO_NOTES_EO();
                            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_DELETED";
                           // _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno; 
                            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = gridItem.Cells[2].Text ;

                            DAO_NOTES_BO _DAO_NOTES_BO = new DAO_NOTES_BO();
                            _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                            _DAO_NOTES_EO.SZ_CASE_ID = gridItem.Cells[3].Text; 
                            //_DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                            _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                        }
                    }
                    lblMsg.Text = "Bill deleted Successfully ...!";
                    lblMsg.Visible = true;
                    BindGrid();

                }
                //lblMsg.Text = "Bill deleted Successfully ...!";
                //lblMsg.Visible = true;
                //BindGrid();
            }
                //added by shailesh 3may 2010
                // delete functionality for testing facility
            else if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                foreach (DataGridItem gridItem in this.grdBillSearch.Items)
                {
                    CheckBox grdChk = (CheckBox)gridItem.FindControl("ChkDelete");
                    if (grdChk.Checked)
                    {

                        if (gridItem.Cells[25].Text != "BLD")
                        {
                            BillStatus += gridItem.Cells[2].Text + ",";
                            Flag = 1;
                        }
                       
                    }
                }
                if (Flag == 1)
                {
                    Page.RegisterStartupScript("ss", "<script language='javascript'> alert('You can not delete Bill No. " + BillStatus.ToString() + "');</script>");
                    
                }
                else
                {

                    foreach (DataGridItem gridItem in this.grdBillSearch.Items)
                    {
                        CheckBox grdChk = (CheckBox)gridItem.FindControl("ChkDelete");
                        if (grdChk.Checked)
                        {

                            objBillTransactionBO.DeleteReffBill(gridItem.Cells[2].Text, txtCompanyID.Text);

                            // Start : Save Notes for Bill.
                            _DAO_NOTES_EO = new DAO_NOTES_EO();
                            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_DELETED";
                            //_DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;
                            _DAO_NOTES_EO.SZ_ACTIVITY_DESC =  gridItem.Cells[2].Text ;

                            DAO_NOTES_BO _DAO_NOTES_BO = new DAO_NOTES_BO();
                            _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                            _DAO_NOTES_EO.SZ_CASE_ID = gridItem.Cells[3].Text; 
                            //_DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                            _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                        }
                    }
                    lblMsg.Text = "Bill deleted Successfully ...!";
                    lblMsg.Visible = true;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetPatientDeskList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            grdPatientDeskList.DataSource = _bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTLIST", txtCaseID.Text);
            grdPatientDeskList.DataBind();

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

    protected void grdBillSearch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    #region "Change Bill Status"

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_ReportBO _reportBO = new Bill_Sys_ReportBO();
        try
        {
            if (extddlBillStatus.Text != "NA")
            {

                ArrayList objAL = new ArrayList();
                string szListOfBillIDs = "";
                Boolean flag = false;
                for (int i = 0; i < grdBillSearch.Items.Count; i++)
                {
                    CheckBox chkDelete1 = (CheckBox)grdBillSearch.Items[i].FindControl("ChkDelete");
                    if (chkDelete1.Checked)
                    {
                        if (flag == false)
                        {
                            szListOfBillIDs = "'" + grdBillSearch.Items[i].Cells[2].Text + "'";
                            flag = true;
                        }
                        else
                        {
                            szListOfBillIDs = szListOfBillIDs + ",'" + grdBillSearch.Items[i].Cells[2].Text + "'";
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
                    lblMsg.Text = "Bill status updated successfully.";
                    lblMsg.Visible = true;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion

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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #region "Export to excel"
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
            for (int icount = 0; icount < grdForReport.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdForReport.Columns.Count; i++)
                    {
                        if (grdForReport.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdForReport.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdForReport.Columns.Count; j++)
                {
                    if (grdForReport.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdForReport.Items[icount].Cells[j].Text);
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
#endregion


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
}
 