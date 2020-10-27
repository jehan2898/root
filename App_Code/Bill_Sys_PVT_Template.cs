using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using log4net;
using Microsoft.SqlServer.Management.Common;
/// <summary>
/// Summary description for Bill_Sys_PVT_Template
/// </summary>
public class Bill_Sys_PVT_Template
{
    CaseDetailsBO objCaseDetailsBO;
    PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    Bill_Sys_NF3_Template objNF3Template;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private static ILog log = LogManager.GetLogger("Bill_Sys_PVT_Template");
    MUVGenerateFunction _MUVGenerateFunction;
    Bill_Sys_Verification_Desc objVerification_Desc;

    public Bill_Sys_PVT_Template()
    {

    }
    public string GeneratePVTBill(bool isReferingFacility, string szCompanyId, string szCaseId, string szSpecility, string szCompanyName, string szBillId, string szUserName, string szUserId)
    {
        string szDefaultPath = "";
        string szReturnPath = "";
        try
        {

            log.Debug("in GeneratePVTBill");
            objNF3Template = new Bill_Sys_NF3_Template();
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            #region Generate Bill For Private cases

            String szLastPDFFileName = "";
            String szDestinationDir = "";


            //changes for Doc manager for new Bill path -- pravin

            objVerification_Desc = new Bill_Sys_Verification_Desc();
            log.Debug("create  Bill_Sys_Verification_Desc object");


            objVerification_Desc.sz_bill_no = szBillId;
            objVerification_Desc.sz_company_id = szCompanyId;
            objVerification_Desc.sz_flag = "BILL";

            ArrayList arrNf_Para = new ArrayList();
            ArrayList arrNf_NodeType = new ArrayList();

            objCaseDetailsBO = new CaseDetailsBO();
            DataSet ds1500from = new DataSet();
            string sz_Type = "";
            string bt_1500_Form = "";


            arrNf_Para.Add(objVerification_Desc);

            arrNf_NodeType = _bill_Sys_BillTransaction.Get_Node_Type(arrNf_Para);
            log.Debug("called  _bill_Sys_BillTransaction ");
            log.Debug("arrNf_NodeType =" + arrNf_NodeType);
            if (arrNf_NodeType.Contains("NFVER"))
            {
                sz_Type = "OLD";
                szDestinationDir = szCompanyName + "/" + szCaseId + "/No Fault File/Bills/" + szSpecility + "/";
            }
            else
            {
                sz_Type = "NEW";
                szDestinationDir = szCompanyName + "/" + szCaseId + "/No Fault File/Medicals/" + szSpecility + "/" + "Bills/";
            }

            //szDestinationDir = szCompanyName + "/" + szCaseId + "/No Fault File/Bills/" + szSpecility + "/";

            String szSourceDir = "";
            szSourceDir = szCompanyName + "/" + szCaseId + "/Packet Document/";
            log.Debug("szSourceDir =" + szSourceDir);
            //changes for Add only 1500 Form For Insurance Company -- pravin

            ds1500from = objCaseDetailsBO.Get1500FormBitForInsurance(szCompanyId, szBillId);

            if (ds1500from.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1500from.Tables[0].Rows.Count; i++)
                {
                    bt_1500_Form = ds1500from.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                }
            }
            if (bt_1500_Form == "1")
            {
                string str_1500 = "";
                _MUVGenerateFunction = new MUVGenerateFunction();

                //str_1500 = _MUVGenerateFunction.FillPdf(szBillId.ToString());
                string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, szBillId, szCompanyName, szCaseId, szCompanyId);

                if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                {
                    if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                    {
                        Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                    }
                    File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                }
                szReturnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + str_1500;

                ArrayList objAL = new ArrayList();

                if (sz_Type == "OLD")
                {
                    objAL.Add(szBillId);
                    objAL.Add(szDestinationDir + str_1500);
                    objAL.Add(szCompanyId);
                    objAL.Add(szCaseId);
                    objAL.Add(str_1500);
                    objAL.Add(szDestinationDir);
                    objAL.Add(szUserName);
                    objAL.Add(szSpecility);
                    //objAL.Add("");
                    objAL.Add("PVT");
                    objAL.Add(szCaseId);
                    //objAL.Add(txtCaseNo.Text);
                    objNF3Template.saveGeneratedBillPath(objAL);
                }
                else
                {
                    objAL.Add(szBillId);
                    objAL.Add(szDestinationDir + str_1500);
                    objAL.Add(szCompanyId);
                    objAL.Add(szCaseId);
                    objAL.Add(str_1500);
                    objAL.Add(szDestinationDir);
                    objAL.Add(szUserName);
                    objAL.Add(szSpecility);
                    //objAL.Add("");
                    objAL.Add("PVT");
                    objAL.Add(szCaseId);
                    objAL.Add(arrNf_NodeType[0].ToString());
                    objNF3Template.saveGeneratedBillPath_New(objAL);
                }



                // Start : Save Notes for Bill.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = szUserId;
                _DAO_NOTES_EO.SZ_CASE_ID = szCaseId;
                _DAO_NOTES_EO.SZ_COMPANY_ID = szCompanyId;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

            }

            else
            {

                String szGenereatedFileName = "";
                objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                string szXMLFileName;
                string szOriginalPDFFileName;
                szXMLFileName = ConfigurationManager.AppSettings["HCFA1500_XML_FILE"].ToString();
                string path = GetPdfFilePath() + szCompanyId + "/HCFA -1500.pdf";
                log.Debug("szXMLFileName =" + szXMLFileName);
                log.Debug("path =" + path);
                if (File.Exists(path))
                {
                    szOriginalPDFFileName = path;
                }
                else
                {
                    szOriginalPDFFileName = ConfigurationManager.AppSettings["HCFA1500_PDF_FILE"].ToString();
                }
                String szPDFPage = "";
                log.Debug("Before PdfValue ReplacePDFvalues");
                string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                szPDFPage = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, szBillId, szCompanyName, szCaseId, szCompanyId);
                //szPDFPage = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalTemplatePDFFileName, szBillId, szCompanyName, szCaseId);
                log.Debug("after PdfValue ReplacePDFvalues");
                log.Debug("szPDFPage" + szPDFPage);

                #region File saving logic
                String szOpenFilePath = "";
                szGenereatedFileName = szDestinationDir + szPDFPage;
                log.Debug("szGenereatedFileName" + szGenereatedFileName);
                szOpenFilePath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szGenereatedFileName;
                log.Debug("szOpenFilePath" + szOpenFilePath);
                string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + "/" + szGenereatedFileName;
                log.Debug("szFileNameWithFullPath" + szFileNameWithFullPath);
                string szFileNameForSaving = "";
                szReturnPath = szOpenFilePath;
                log.Debug("szReturnPath" + szReturnPath);
                // Save Entry in Table
                if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                {
                    szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();

                }
                log.Debug("szGenereatedFileName" + szGenereatedFileName);
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

                log.Debug("szFileNameForSaving" + szFileNameForSaving);
                String[] szTemp;
                string szBillName = "";
                szTemp = szFileNameForSaving.Split('/');
                log.Debug("szTemp" + szTemp);
                ArrayList objAL = new ArrayList();
                szFileNameForSaving = szFileNameForSaving.Remove(0, ApplicationSettings.GetParameterValue("DocumentManagerURL").Length);
                log.Debug("szFileNameForSaving" + szFileNameForSaving);
                szBillName = szTemp[szTemp.Length - 1].ToString();
                log.Debug("szBillName" + szBillName);
                string bt_CaseType, bt_include, str_1500;
                string strComp = szCompanyId.ToString();
                _MUVGenerateFunction = new MUVGenerateFunction();
                if (System.Configuration.ConfigurationManager.AppSettings["Only_1500_in_PVT_Bill"].ToString() == "true")
                {
                    str_1500 = _MUVGenerateFunction.FillPdf(szBillId.ToString());
                    log.Debug("str_1500" + str_1500);
                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                    }
                    szBillName = str_1500;
                    //szBillName = str_1500.Replace(".pdf", "_MER.pdf");
                    szReturnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + szBillName;
                    log.Debug("szReturnPath" + szReturnPath);
                }
                else
                {
                    log.Debug("bjNF3Template.getPhysicalPath() + szSourceDir + szBillName" + objNF3Template.getPhysicalPath() + szSourceDir + szBillName);
                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + szBillName))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + szBillName, objNF3Template.getPhysicalPath() + szDestinationDir + szBillName);
                    }



                    //Tushar
                    bt_include = _MUVGenerateFunction.get_bt_include(strComp, szSpecility, "", "Speciality");
                    log.Debug("bt_include" + bt_include);
                    bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000003", "CaseType");
                    log.Debug("bt_CaseType" + bt_CaseType);
                    if (bt_include == "True" && bt_CaseType == "True")
                    {
                        str_1500 = _MUVGenerateFunction.FillPdf(szBillId.ToString());

                        MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDestinationDir + szBillName, objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                        szBillName = str_1500.Replace(".pdf", "_MER.pdf");
                        szReturnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + szBillName;
                    }

                    log.Debug("szReturnPath" + szReturnPath);
                }

                //changes for Doc manager for new Bill path -- pravin

                if (sz_Type == "OLD")
                {
                    objAL.Add(szBillId);
                    objAL.Add(szDestinationDir + szBillName);
                    objAL.Add(szCompanyId);
                    objAL.Add(szCaseId);
                    objAL.Add(szBillName);
                    objAL.Add(szDestinationDir);
                    objAL.Add(szUserName);
                    objAL.Add(szSpecility);
                    //objAL.Add("");
                    objAL.Add("PVT");
                    objAL.Add(szCaseId);
                    //objAL.Add(txtCaseNo.Text);
                    objNF3Template.saveGeneratedBillPath(objAL);
                }
                else
                {
                    objAL.Add(szBillId);
                    objAL.Add(szDestinationDir + szBillName);
                    objAL.Add(szCompanyId);
                    objAL.Add(szCaseId);
                    objAL.Add(szBillName);
                    objAL.Add(szDestinationDir);
                    objAL.Add(szUserName);
                    objAL.Add(szSpecility);
                    //objAL.Add("");
                    objAL.Add("PVT");
                    objAL.Add(szCaseId);
                    objAL.Add(arrNf_NodeType[0].ToString());
                    objNF3Template.saveGeneratedBillPath_New(objAL);
                }



                // Start : Save Notes for Bill.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = szUserId;
                _DAO_NOTES_EO.SZ_CASE_ID = szCaseId;
                _DAO_NOTES_EO.SZ_COMPANY_ID = szCompanyId;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                #endregion

                //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" +  ApplicationSettings.GetParameterValue("DocumentManagerURL") + szPDFPage + "'); ", true);
                #endregion
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return szReturnPath;
    }
    public string GeneratePVTBill(bool isReferingFacility, string szCompanyId, string szCaseId, string szSpecility, string szCompanyName, string szBillId, string szUserName, string szUserId, ServerConnection conn)
    {
        string szDefaultPath = "";
        string szReturnPath = "";
        try
        {

            log.Debug("in GeneratePVTBill");
            objNF3Template = new Bill_Sys_NF3_Template();
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            #region Generate Bill For Private cases

            String szLastPDFFileName = "";
            String szDestinationDir = "";


            //changes for Doc manager for new Bill path -- pravin

            objVerification_Desc = new Bill_Sys_Verification_Desc();
            log.Debug("create  Bill_Sys_Verification_Desc object");


            objVerification_Desc.sz_bill_no = szBillId;
            objVerification_Desc.sz_company_id = szCompanyId;
            objVerification_Desc.sz_flag = "BILL";

            ArrayList arrNf_Para = new ArrayList();
            ArrayList arrNf_NodeType = new ArrayList();

            objCaseDetailsBO = new CaseDetailsBO();
            DataSet ds1500from = new DataSet();
            string sz_Type = "";
            string bt_1500_Form = "";


            arrNf_Para.Add(objVerification_Desc);

            arrNf_NodeType = _bill_Sys_BillTransaction.Get_Node_Type(arrNf_Para, conn);
            log.Debug("called  _bill_Sys_BillTransaction ");
            log.Debug("arrNf_NodeType =" + arrNf_NodeType);
            if (arrNf_NodeType.Contains("NFVER"))
            {
                sz_Type = "OLD";
                szDestinationDir = szCompanyName + "/" + szCaseId + "/No Fault File/Bills/" + szSpecility + "/";
            }
            else
            {
                sz_Type = "NEW";
                szDestinationDir = szCompanyName + "/" + szCaseId + "/No Fault File/Medicals/" + szSpecility + "/" + "Bills/";
            }

            //szDestinationDir = szCompanyName + "/" + szCaseId + "/No Fault File/Bills/" + szSpecility + "/";

            String szSourceDir = "";
            szSourceDir = szCompanyName + "/" + szCaseId + "/Packet Document/";
            log.Debug("szSourceDir =" + szSourceDir);
            //changes for Add only 1500 Form For Insurance Company -- pravin

            ds1500from = objCaseDetailsBO.Get1500FormBitForInsurance(szCompanyId, szBillId, conn);

            if (ds1500from.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1500from.Tables[0].Rows.Count; i++)
                {
                    bt_1500_Form = ds1500from.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                }
            }
            if (bt_1500_Form == "1")
            {
                string str_1500 = "";
                _MUVGenerateFunction = new MUVGenerateFunction();

                //str_1500 = _MUVGenerateFunction.FillPdf(szBillId.ToString());
                string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, szBillId, szCompanyName, szCaseId, szCompanyId, conn);

                if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                {
                    if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                    {
                        Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                    }
                    File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                }
                szReturnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + str_1500;

                ArrayList objAL = new ArrayList();

                if (sz_Type == "OLD")
                {
                    objAL.Add(szBillId);
                    objAL.Add(szDestinationDir + str_1500);
                    objAL.Add(szCompanyId);
                    objAL.Add(szCaseId);
                    objAL.Add(str_1500);
                    objAL.Add(szDestinationDir);
                    objAL.Add(szUserName);
                    objAL.Add(szSpecility);
                    //objAL.Add("");
                    objAL.Add("PVT");
                    objAL.Add(szCaseId);
                    //objAL.Add(txtCaseNo.Text);
                    objNF3Template.saveGeneratedBillPath(objAL, conn);
                }
                else
                {
                    objAL.Add(szBillId);
                    objAL.Add(szDestinationDir + str_1500);
                    objAL.Add(szCompanyId);
                    objAL.Add(szCaseId);
                    objAL.Add(str_1500);
                    objAL.Add(szDestinationDir);
                    objAL.Add(szUserName);
                    objAL.Add(szSpecility);
                    //objAL.Add("");
                    objAL.Add("PVT");
                    objAL.Add(szCaseId);
                    objAL.Add(arrNf_NodeType[0].ToString());
                    objNF3Template.saveGeneratedBillPath_New(objAL, conn);
                }



                // Start : Save Notes for Bill.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = szUserId;
                _DAO_NOTES_EO.SZ_CASE_ID = szCaseId;
                _DAO_NOTES_EO.SZ_COMPANY_ID = szCompanyId;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

            }

            else
            {

                String szGenereatedFileName = "";
                objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                string szXMLFileName;
                string szOriginalPDFFileName;
                szXMLFileName = ConfigurationManager.AppSettings["HCFA1500_XML_FILE"].ToString();
                string path = GetPdfFilePath() + szCompanyId + "/HCFA -1500.pdf";
                log.Debug("szXMLFileName =" + szXMLFileName);
                log.Debug("path =" + path);
                if (File.Exists(path))
                {
                    szOriginalPDFFileName = path;
                }
                else
                {
                    szOriginalPDFFileName = ConfigurationManager.AppSettings["HCFA1500_PDF_FILE"].ToString();
                }
                String szPDFPage = "";
                log.Debug("Before PdfValue ReplacePDFvalues");
                string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                szPDFPage = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, szBillId, szCompanyName, szCaseId, szCompanyId, conn);
                //szPDFPage = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalTemplatePDFFileName, szBillId, szCompanyName, szCaseId);
                log.Debug("after PdfValue ReplacePDFvalues");
                log.Debug("szPDFPage" + szPDFPage);

                #region File saving logic
                String szOpenFilePath = "";
                szGenereatedFileName = szDestinationDir + szPDFPage;
                log.Debug("szGenereatedFileName" + szGenereatedFileName);
                szOpenFilePath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szGenereatedFileName;
                log.Debug("szOpenFilePath" + szOpenFilePath);
                string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + "/" + szGenereatedFileName;
                log.Debug("szFileNameWithFullPath" + szFileNameWithFullPath);
                string szFileNameForSaving = "";
                szReturnPath = szOpenFilePath;
                log.Debug("szReturnPath" + szReturnPath);
                // Save Entry in Table
                if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                {
                    szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();

                }
                log.Debug("szGenereatedFileName" + szGenereatedFileName);
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

                log.Debug("szFileNameForSaving" + szFileNameForSaving);
                String[] szTemp;
                string szBillName = "";
                szTemp = szFileNameForSaving.Split('/');
                log.Debug("szTemp" + szTemp);
                ArrayList objAL = new ArrayList();
                szFileNameForSaving = szFileNameForSaving.Remove(0, ApplicationSettings.GetParameterValue("DocumentManagerURL").Length);
                log.Debug("szFileNameForSaving" + szFileNameForSaving);
                szBillName = szTemp[szTemp.Length - 1].ToString();
                log.Debug("szBillName" + szBillName);
                string bt_CaseType, bt_include, str_1500;
                string strComp = szCompanyId.ToString();
                _MUVGenerateFunction = new MUVGenerateFunction();
                if (System.Configuration.ConfigurationManager.AppSettings["Only_1500_in_PVT_Bill"].ToString() == "true")
                {
                    str_1500 = _MUVGenerateFunction.FillPdf(szBillId.ToString(), conn);
                    log.Debug("str_1500" + str_1500);
                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + str_1500))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500);
                    }
                    szBillName = str_1500;
                    //szBillName = str_1500.Replace(".pdf", "_MER.pdf");
                    szReturnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + szBillName;
                    log.Debug("szReturnPath" + szReturnPath);
                }
                else
                {
                    log.Debug("bjNF3Template.getPhysicalPath() + szSourceDir + szBillName" + objNF3Template.getPhysicalPath() + szSourceDir + szBillName);
                    if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + szBillName))
                    {
                        if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                        {
                            Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                        }
                        File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + szBillName, objNF3Template.getPhysicalPath() + szDestinationDir + szBillName);
                    }



                    //Tushar
                    bt_include = _MUVGenerateFunction.get_bt_include(strComp, szSpecility, "", "Speciality");
                    log.Debug("bt_include" + bt_include);
                    bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000003", "CaseType");
                    log.Debug("bt_CaseType" + bt_CaseType);
                    if (bt_include == "True" && bt_CaseType == "True")
                    {
                        str_1500 = _MUVGenerateFunction.FillPdf(szBillId.ToString(), conn);

                        MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDestinationDir + szBillName, objNF3Template.getPhysicalPath() + szSourceDir + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                        szBillName = str_1500.Replace(".pdf", "_MER.pdf");
                        szReturnPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + szBillName;
                    }

                    log.Debug("szReturnPath" + szReturnPath);
                }

                //changes for Doc manager for new Bill path -- pravin

                if (sz_Type == "OLD")
                {
                    objAL.Add(szBillId);
                    objAL.Add(szDestinationDir + szBillName);
                    objAL.Add(szCompanyId);
                    objAL.Add(szCaseId);
                    objAL.Add(szBillName);
                    objAL.Add(szDestinationDir);
                    objAL.Add(szUserName);
                    objAL.Add(szSpecility);
                    //objAL.Add("");
                    objAL.Add("PVT");
                    objAL.Add(szCaseId);
                    //objAL.Add(txtCaseNo.Text);
                    objNF3Template.saveGeneratedBillPath(objAL, conn);
                }
                else
                {
                    objAL.Add(szBillId);
                    objAL.Add(szDestinationDir + szBillName);
                    objAL.Add(szCompanyId);
                    objAL.Add(szCaseId);
                    objAL.Add(szBillName);
                    objAL.Add(szDestinationDir);
                    objAL.Add(szUserName);
                    objAL.Add(szSpecility);
                    //objAL.Add("");
                    objAL.Add("PVT");
                    objAL.Add(szCaseId);
                    objAL.Add(arrNf_NodeType[0].ToString());
                    objNF3Template.saveGeneratedBillPath_New(objAL, conn);
                }



                // Start : Save Notes for Bill.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = szUserId;
                _DAO_NOTES_EO.SZ_CASE_ID = szCaseId;
                _DAO_NOTES_EO.SZ_COMPANY_ID = szCompanyId;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                #endregion

                //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" +  ApplicationSettings.GetParameterValue("DocumentManagerURL") + szPDFPage + "'); ", true);
                #endregion
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return szReturnPath;
    }
    public string GetPdfFilePath()
    {
        SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string path = "";
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ParameterValue from tblApplicationSettings where ParameterName = 'TemplateDocsPath'");
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            path = ds.Tables[0].Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return path;
    }



}
