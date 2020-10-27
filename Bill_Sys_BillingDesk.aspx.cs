/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_BillingDesk.aspx.cs
/*Purpose              :       Display Billing Desk Record
/*Author               :       Sandeep y
/*Date of creation     :       12 Oct 2009 
/*Modified By          :
/*Modified Date        :
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
using MergeDocumentNodes;
using MergeTIFFANDPDF;
using ExtendedDropDownList;
using System.Text;
using System.IO;
using mbs.bl.litigation;

public partial class Bill_Sys_BillingDesk : PageBase
{
    Bill_Sys_BillingCompanyDetails_BO _obj;
    Bill_Sys_NF3_Template objNF3Template;
    PDFValueReplacement.PDFValueReplacement objPDFReplacement;

    MergeTIFFANDPDF.MergeTIFFANDPDF  _objMergeTiffAndPDF;
    Bill_Sys_BillTransaction_BO _billTransactionBO;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        if (Request.QueryString["Type"] != null)
        {
            hlnkShowDiv.Visible = true;
        }
        btnAssign.Attributes.Add("onclick", "return formValidator('aspnetForm','extddlUserLawFirm');");
        if (Page.IsPostBack == false)
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            BindgrdLitigationdesk();
            setLabels();
        }
        extddlUserLawFirm.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_DEFAULT_LAW_FIRM != "")
        {
            extddlUserLawFirm.Text = ((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_DEFAULT_LAW_FIRM;
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_BillingDesk.aspx");
        }
        #endregion
    }

    public void BindgrdLitigationdesk()
    {
        _obj = new Bill_Sys_BillingCompanyDetails_BO();
        grdLitigationDesk.DataSource = _obj.getBillingDesk(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        grdLitigationDesk.DataBind();


        grdForReport.DataSource = _obj.getBillingDesk(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        grdForReport.DataBind();
    }
  
    protected void grdLitigationDesk_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        
        if (e.CommandName.ToString() == "Document Manager")
        {



            Session["TM_SZ_CASE_ID"] = e.CommandArgument;
            Session["TM_SZ_BILL_ID"] = e.Item.Cells[0].Text;
            String szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            #region "Generate Bill Code"

 //           CaseDetailsBO objCaseDetails = new CaseDetailsBO();

 //           if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
 //           {
 //               String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
 //               string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();

 //               string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
 //               String szFile3 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
 //               String szFile4 = ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
 //               String szPDFPage1;
 //               String szXMLFileName;
 //               String szOriginalPDFFileName;
 //               String sz3and4Page;
 //               Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
 //               String szDiagPDFFilePosition = objConfiguration.getConfigurationSettings(szCompanyID,"GET_DIAG_PAGE_POSITION");
 //               String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(szCompanyID,"DIAG_PAGE");
               
 //               szXMLFileName = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
 //               szOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                
 //               objNF3Template = new Bill_Sys_NF3_Template();
 //               Boolean fAddDiag = true;

 //               GeneratePDFFile.GenerateNF3PDF objGeneratePDF = new GeneratePDFFile.GenerateNF3PDF();
 //               objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();



 //               // Note : Generate PDF with Billing Information table. **** II
 //               String szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);


 //               // Note : Generate PDF File with More than 5 diagonis code entry.
 //               String szNextDiagPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);

                

 //               // Note : Merge Last and Second-Last Page
 //               sz3and4Page = objPDFReplacement.Merge3and4Page(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szFile3, szFile4);

 //               if (szGenerateNextDiagPage != Bill_Sys_Constant.constGenerateNextDiagPage  && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= 5 && szDiagPDFFilePosition == Bill_Sys_Constant.constBEFORE_AOB)
 //               {
 //                   if (szGenerateNextDiagPage == "CI_0000004" && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) == 5)
 //                   { 
 //                   }
 //                   else
 //                   {
 //                       MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + sz3and4Page, objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, objNF3Template.getPhysicalPath() + szDefaultPath + sz3and4Page.Replace(".pdf", "_new34.pdf"));
 //                       sz3and4Page = sz3and4Page.Replace(".pdf", "_new34.pdf");
 //                   }
 //               }

 //               // Note : Generate First Page [Replace value from database] **** I
 //               szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());

 //               if (szGenerateNextDiagPage != Bill_Sys_Constant.constGenerateNextDiagPage && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= 5 && szDiagPDFFilePosition == Bill_Sys_Constant.constAFTER_FIRST_PAGE)
 //               {
 //                   if (szGenerateNextDiagPage == "CI_0000004" && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) == 5)
 //                   {
 //                   }
 //                   else
 //                   {
 //                       MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage1, objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage1.Replace(".pdf", "_MER.pdf"));
 //                       szPDFPage1 = szPDFPage1.Replace(".pdf", "_MER.pdf");
 //                   }
 //               }

 //               if (szGenerateNextDiagPage != Bill_Sys_Constant.constGenerateNextDiagPage && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= 5 && szDiagPDFFilePosition == Bill_Sys_Constant.constAFTER_BILL_INFORMATION)
 //               {
 //                   if (szGenerateNextDiagPage == "CI_0000004" && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) == 5)
 //                   {
 //                   }
 //                   else
 //                   {
 //                       MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDFFileName, objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFFileName.Replace(".pdf", "_MER.pdf"));
 //                       szPDFFileName = szPDFFileName.Replace(".pdf", "_MER.pdf");
 //                   }
 //               }


 //               // Merge **** I AND **** II
 //               String szPDF_1_3;
 //               // (                                                string p_szCompanyID,                                                           string p_szCompanyName,                                                             string p_szCaseID,                  string p_szBillID,              string p_szFile1, string p_szFile2)
 //               szPDF_1_3 = objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDFPage1, szPDFFileName);

 //               String szLastPDFFileName;
 //               szLastPDFFileName = objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, sz3and4Page);

 //               String szGenereatedFileName = "";
 //               szGenereatedFileName = szDefaultPath + szLastPDFFileName;

                

 //               String szOpenFilePath = "";
 //               szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szGenereatedFileName;

 //               // Logic Start For Get Node From AOB , EOB , Denials

 //               ProcessDocumentMergeList objProcessDocumentMergeList = new ProcessDocumentMergeList(ConfigurationSettings.AppSettings["DocumentMergeXML"].ToString());

 //               DAO_Bill_Sys_Case objDAO = new DAO_Bill_Sys_Case();
 //               objDAO.CaseID = Session["TM_SZ_CASE_ID"].ToString();
 //               objDAO.BillingCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
 //               objDAO.MergeType = "litigation";
 //               ArrayList objList = objProcessDocumentMergeList.getNodeList(objDAO);

 //               objList = lfnGetListWithPDFName(objList, objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, szDiagPDFFilePosition, szGenerateNextDiagPage, objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()));

 //               string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + "/" + szGenereatedFileName;

 ////               objList.Insert(0, szFileNameWithFullPath);

 //               _objMergeTiffAndPDF = new MergeTIFFANDPDF.MergeTIFFANDPDF();
 //               _objMergeTiffAndPDF.CreateTiffToPDFList(szFileNameWithFullPath.Replace(".pdf","_Temp.pdf").ToString() , objList);
                
 //               CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
 //               string newPdfFilename = "";
 //               string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
 //               objMyForm.initialize(KeyForCutePDF);

 //               if (objMyForm == null)
 //               {
 //                   // Response.Write("objMyForm not initialized");
 //               }
 //               else
 //               {
 //                   if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf","_Temp.pdf").ToString()))
 //                   {
 //                       objMyForm.mergePDF(szFileNameWithFullPath, szFileNameWithFullPath.Replace(".pdf", "_Temp.pdf").ToString(), szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString());

 //                       if (szGenerateNextDiagPage != Bill_Sys_Constant.constGenerateNextDiagPage && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= 5 && szDiagPDFFilePosition == Bill_Sys_Constant.constAT_THE_END)
 //                       {
 //                           if (szGenerateNextDiagPage == "CI_0000004" && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) == 5)
 //                           {
 //                           }
 //                           else
 //                           {
 //                               MergePDF.MergePDFFiles(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString(), objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString());
 //                               szPDFFileName = szPDFFileName.Replace(".pdf", "_NewMerge.pdf");
 //                           }
 //                       }

 //                   }

 //               }

 //               // End Logic

 //               string szFileNameForSaving = "";

 //               // Save Entry in Table
 //               if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
 //               {
 //                   szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();
 //               }
             
 //               // End

 //                if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString()))
 //               {
 //                   szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString();
 //                   Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true); 
 //               }
 //               else
 //               {
 //                   if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
 //                   {
 //                       szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
 //                      Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
 //                   }
 //                   else
 //                   {
 //                       szFileNameForSaving = szOpenFilePath.ToString();
 //                       Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
 //                   }
 //               }

 //               ArrayList objAL = new ArrayList();
 //               szFileNameForSaving = szFileNameForSaving.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
 //               objAL.Add(szFileNameForSaving);
 //               objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
 //               objAL.Add(Session["TM_SZ_CASE_ID"].ToString());
 //               objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
 //               objNF3Template.saveGeneratedNF3File(objAL);
                
 //           }
 //           else
 //           {
 //               //Session["TM_SZ_CASE_ID"] = e.CommandArgument;
 //               //Session["TM_SZ_BILL_ID"] = e.Item.Cells[2].Text;
 //               Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
 //           }
            #endregion

            #region "Logic to view bills"
            objNF3Template = new Bill_Sys_NF3_Template();
            DataSet objDS = new DataSet();
            objDS = objNF3Template.getBillList(e.Item.Cells[0].Text);
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
           
    }

   

   

    private ArrayList lfnGetListWithPDFName(ArrayList objAL, string p_szPDFFileName, string p_szDiagPosKey,string p_szAddDiagNextPage,int p_iDiagCount)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i=0; i < objAL.Count - 1 ; i++)
            {
                if (p_szDiagPosKey == Bill_Sys_Constant.constAFTER_AOB && p_szAddDiagNextPage != Bill_Sys_Constant.constGenerateNextDiagPage && p_iDiagCount >= 5)
                {
                    if (objAL[i].ToString().Equals("AOB"))
                    {
                        objAL[i] = p_szPDFFileName;
                    }
                }
                if (p_szDiagPosKey == Bill_Sys_Constant.constAFTER_EOB && p_szAddDiagNextPage != Bill_Sys_Constant.constGenerateNextDiagPage && p_iDiagCount >= 5)
                {
                    if (objAL[i].ToString().Equals("EOB"))
                    {
                        objAL[i] = p_szPDFFileName;
                    }
                }
            }
            for (int i = 0; i < objAL.Count; i++)
            {
                if (objAL[i].ToString().Equals("AOB") || objAL[i].ToString().Equals("EOB") || objAL[i].ToString().ToLower().Equals("denials"))
                {
                    objAL.RemoveAt(i);
                    i = i - 1;
                }
           }
           return objAL;

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
            return null;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    


    //protected void btnAssign_Click(object sender, EventArgs e)
    //{
    //    LitigationDeskBO _objLD;
    //    if (extddlUserLawFirm.Text != "NA")
    //    {
    //        foreach (DataGridItem dr in grdLitigationDesk.Items)
    //        {
    //            if (((CheckBox)dr.Cells[11].FindControl("chkSelect")).Checked != false)
    //            {
    //                _objLD = new LitigationDeskBO();
    //                ArrayList _objAL = new ArrayList();
    //                _objAL.Add(dr.Cells[1].Text);
    //                _objAL.Add(dr.Cells[0].Text);
    //                _objAL.Add(extddlUserLawFirm.Text);
    //                _objAL.Add("0");
    //                _objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                _objLD.saveLegalCases(_objAL);
    //            }
    //        }
    //        BindgrdLitigationdesk();
    //    }
    //}
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        LitigationDeskBO _objLD;
        if (extddlUserLawFirm.Text != "NA")
        {
            Litigation litigation = new Litigation();
            LitigationDAO ndao = null;
            ArrayList list = new ArrayList();
            foreach (DataGridItem dr in grdLitigationDesk.Items)
            {
                if (((CheckBox)dr.Cells[11].FindControl("chkSelect")).Checked != false)
                {
                    //_objLD = new LitigationDeskBO();
                    //ArrayList _objAL = new ArrayList();
                    //_objAL.Add(dr.Cells[1].Text);
                    //_objAL.Add(dr.Cells[0].Text);
                    //_objAL.Add(extddlUserLawFirm.Text);
                    //_objAL.Add("0");
                    //_objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    //_objLD.saveLegalCases(_objAL);

                    ndao = new LitigationDAO();
                    ndao.CaseID = dr.Cells[1].Text;
                    ndao.BillNumber = dr.Cells[0].Text;
                    ndao.LawFirmID = this.extddlUserLawFirm.Text;
                    ndao.UserID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID; //(Bill_Sys_UserObject)this.Session["USER_OBJECT"];
                    ndao.TransferDate = DateTime.Today.ToString("MM/dd/yyyy");
                    ndao.CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    list.Add(ndao);

                }
            }
            if (list.Count > 0)
            {
                string szBatchId = litigation.saveLitigationBills(list);
            }
            BindgrdLitigationdesk();
        }



    }
    protected void grdLitigationDesk_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                
                if (e.Item.Cells[10].Text != "" && e.Item.Cells[10].Text != "&nbsp;")
                {
                    ((CheckBox)e.Item.FindControl("chkSelect")).Visible = false;

                }
            }
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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #region "Bind Label For Dash Board"
    protected void setLabels()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DashBoardBO _obj = new DashBoardBO();
        _billTransactionBO = new Bill_Sys_BillTransaction_BO();
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
            //lblMissingInformation.Text += " insurance company missing </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingAttorney' onclick=\"javascript:OpenPage('MissingAttorney');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_ATTORNEY") + "</a>";
            //lblMissingInformation.Text += " attorney missing </li>  <li> <a href='Bill_Sys_PaidBills.aspx?Flag=MissingClaimNumber' onclick=\"javascript:OpenPage('MissingClaimNumber');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), txtCompanyID.Text, "MISSING_CLAIM_NUMBER") + "</a> claim number missing </li></ul>";
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

    protected void grdLitigationDesk_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        grdLitigationDesk.CurrentPageIndex = e.NewPageIndex;
        BindgrdLitigationdesk();
    }

    #region "Export Excel"
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
            StreamWriter sw = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + filename, false);


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
    #endregion
}