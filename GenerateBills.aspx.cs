using AjaxControlToolkit;
using Componend;
using CUTEFORMCOLib;
using ExtendedDropDownList;
using GeneratePDFFile;
using iTextSharp.text.pdf;
using log4net;
using mbs.LienBills;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using PDFValueReplacement;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class GenerateBills : System.Web.UI.Page
{
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private Bill_Sys_LoginBO _bill_Sys_LoginBO;
    private Bill_Sys_Menu _bill_Sys_Menu;
    private Bill_Sys_NF3_Template _bill_Sys_NF3_Template;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private MUVGenerateFunction _MUVGenerateFunction;
    private SaveOperation _saveOperation;
    private static string bt_Change_Amount;
    private static string bt_diagnosis_code;
    private string bt_include;
    private const int I_COL_GRID_COMPLETED_VISITS_ADDED_BY_DOCTOR = 12;
    private const int I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER = 15;
    private const int I_COL_GRID_COMPLETED_VISITS_FINALIZED = 13;
    private static ILog log = LogManager.GetLogger("Bill_Sys_BillTransaction");
    private CaseDetailsBO objCaseDetailsBO;
    private Bill_Sys_InsertDefaultValues objDefaultValue;
    private Bill_Sys_DigosisCodeBO objDiagCodeBO;
    private Bill_Sys_NF3_Template objNF3Template;
    private PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    private Bill_Sys_SystemObject objSystemObject;
    private Bill_Sys_Verification_Desc objVerification_Desc;
    private string pdfpath;
    private SqlConnection Sqlcon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
    private string str_1500;
    private string strHdnDiagnosisCode;
    string strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {

        string billNo = Request.QueryString["billNo"];
        string companyId = Request.QueryString["companyId"];
        string caseType = Request.QueryString["caseType"];
        string caseId = Request.QueryString["caseId"];
        string caseNo = Request.QueryString["caseNo"];
        string userId = Request.QueryString["userId"];
        string userName = Request.QueryString["userName"];
        string pdfType = Request.QueryString["pdfType"];
        string compName = "";
        this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
            DataSet ds = this._bill_Sys_BillingCompanyDetails_BO.GetBillingCompanyInfo(companyId);
            compName = ds.Tables[0].Rows[0]["SZ_COMPANY_NAME"].ToString();
        }
        catch { }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        ServerConnection currentConnection = BeginBillTranaction();
        if (caseType == "WC000000000000000002")
        {
            this.GenerateAddedBillPDF(billNo, caseId, caseNo, companyId, compName, userId, userName, this._bill_Sys_BillTransaction.GetDoctorSpeciality(billNo, companyId, currentConnection), currentConnection);
        }
        if (caseType == "WC000000000000000003")
        {
            this.GenerateAddedBillPDF(billNo, caseId, caseNo, companyId, compName, userId, userName, this._bill_Sys_BillTransaction.GetDoctorSpeciality(billNo, companyId, currentConnection), currentConnection);
        }
        else if (caseType == "WC000000000000000001")
        {
            WC_Bill_Generation generation = new WC_Bill_Generation();
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

            generation.GeneratePDFForWorkerComp(billNo, caseId, pdfType, companyId, compName, userId, userName, caseNo, this._bill_Sys_BillTransaction.GetDoctorSpeciality(billNo, companyId), 0);
        }
        if (caseType == "WC000000000000000004")
        {
            string str5;
            this.objNF3Template = new Bill_Sys_NF3_Template();
            Lien lien = new Lien();
            this._MUVGenerateFunction = new MUVGenerateFunction();
            string doctorSpeciality = this._bill_Sys_BillTransaction.GetDoctorSpeciality(billNo, companyId, currentConnection);
            string str7 = companyId;
            billNo = billNo;
            this.objVerification_Desc = new Bill_Sys_Verification_Desc();
            this.objVerification_Desc.sz_bill_no = billNo;
            this.objVerification_Desc.sz_company_id = companyId;
            this.objVerification_Desc.sz_flag = "BILL";
            ArrayList list5 = new ArrayList();
            ArrayList list6 = new ArrayList();
            string str8 = "";
            string str9 = "";
            list5.Add(this.objVerification_Desc);
            list6 = this._bill_Sys_BillTransaction.Get_Node_Type(list5, currentConnection);
            if (list6.Contains("NFVER"))
            {
                str8 = "OLD";
                str9 = compName + "/" + caseId + "/No Fault File/Bills/" + doctorSpeciality + "/";
            }
            else
            {
                str8 = "NEW";
                str9 = compName + "/" + caseId + "/No Fault File/Medicals/" + doctorSpeciality + "/Bills/";
            }
            string str10 = compName + "/" + caseId + "/Packet Document/";
            this.objCaseDetailsBO = new CaseDetailsBO();
            DataSet set = new DataSet();
            string str11 = "";
            set = this.objCaseDetailsBO.Get1500FormBitForInsurance(companyId, billNo, currentConnection);
            if (set.Tables[0].Rows.Count > 0)
            {
                for (int k = 0; k < set.Tables[0].Rows.Count; k++)
                {
                    str11 = set.Tables[0].Rows[k]["BT_1500_FORM"].ToString();
                }
            }
            if (str11 == "1")
            {
                string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                this.str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, billNo, compName, caseId, companyId, currentConnection);
                if (File.Exists(this.objNF3Template.getPhysicalPath() + str10 + this.str_1500))
                {
                    if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str9))
                    {
                        Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str9);
                    }
                    File.Copy(this.objNF3Template.getPhysicalPath() + str10 + this.str_1500, this.objNF3Template.getPhysicalPath() + str9 + this.str_1500);
                }
                str5 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str9 + this.str_1500;
                ArrayList list7 = new ArrayList();
                if (str8 == "OLD")
                {
                    list7.Add(billNo);
                    list7.Add(str9 + this.str_1500);
                    list7.Add(companyId);
                    list7.Add(caseId);
                    list7.Add(this.str_1500);
                    list7.Add(str9);
                    list7.Add(userName);
                    list7.Add(doctorSpeciality);
                    list7.Add("LN");
                    list7.Add(caseNo);
                    this.objNF3Template.saveGeneratedBillPath(list7, currentConnection);
                }
                else
                {
                    list7.Add(billNo);
                    list7.Add(str9 + this.str_1500);
                    list7.Add(companyId);
                    list7.Add(caseId);
                    list7.Add(this.str_1500);
                    list7.Add(str9);
                    list7.Add(userName);
                    list7.Add(doctorSpeciality);
                    list7.Add("LN");
                    list7.Add(caseNo);
                    list7.Add(list6[0].ToString());
                    this.objNF3Template.saveGeneratedBillPath_New(list7, currentConnection);
                }
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = userId;
                this._DAO_NOTES_EO.SZ_CASE_ID = caseId;
                this._DAO_NOTES_EO.SZ_COMPANY_ID = companyId;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
            }
            else
            {
                string str12 = this._MUVGenerateFunction.get_bt_include(str7, doctorSpeciality, "", "Speciality");
                string str13 = this._MUVGenerateFunction.get_bt_include(str7, "", "WC000000000000000004", "CaseType");
                if ((str12 == "True") && (str13 == "True"))
                {
                    string str14 = compName + "/" + caseId + "/Packet Document/";
                    string str15 = userId;
                    string str16 = userName;
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(billNo, currentConnection);
                   MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str9 + lien.GenratePdfForLienWithMuv(str7, billNo, this._bill_Sys_BillTransaction.GetDoctorSpeciality(billNo, companyId), caseId, str16, caseNo, str15, currentConnection), this.objNF3Template.getPhysicalPath() + str14 + this.str_1500, this.objNF3Template.getPhysicalPath() + str9 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                    str5 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str9 + this.str_1500.Replace(".pdf", "_MER.pdf");
                    ArrayList list8 = new ArrayList();
                    if (str8 == "OLD")
                    {
                        list8.Add(billNo);
                        list8.Add(str9 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list8.Add(companyId);
                        list8.Add(caseId);
                        list8.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list8.Add(str9);
                        list8.Add(userName);
                        list8.Add(doctorSpeciality);
                        list8.Add("LN");
                        list8.Add(caseNo);
                        this.objNF3Template.saveGeneratedBillPath(list8, currentConnection);
                    }
                    else
                    {
                        list8.Add(billNo);
                        list8.Add(str9 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list8.Add(companyId);
                        list8.Add(caseId);
                        list8.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list8.Add(str9);
                        list8.Add(userName);
                        list8.Add(doctorSpeciality);
                        list8.Add("LN");
                        list8.Add(caseNo);
                        list8.Add(list6[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list8, currentConnection);
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = userId;
                    this._DAO_NOTES_EO.SZ_CASE_ID = caseId;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = companyId;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                }
                else
                {
                    str5 = lien.GenratePdfForLien(companyId, billNo, this._bill_Sys_BillTransaction.GetDoctorSpeciality(billNo, companyId, currentConnection), caseId, userName, caseNo, userId, currentConnection);
                }
            }

            //this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.open('" + str5 + "');", true);
            //ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
        }
        else if (caseType == "WC000000000000000007")
        {
            string str5;
            this.objNF3Template = new Bill_Sys_NF3_Template();
            Employer lien = new Employer();
            this._MUVGenerateFunction = new MUVGenerateFunction();
            string doctorSpeciality = this._bill_Sys_BillTransaction.GetDoctorSpeciality(billNo, companyId, currentConnection);
            string str7 = companyId;

            this.objVerification_Desc = new Bill_Sys_Verification_Desc();
            this.objVerification_Desc.sz_bill_no = billNo;
            this.objVerification_Desc.sz_company_id = companyId;
            this.objVerification_Desc.sz_flag = "BILL";
            ArrayList list5 = new ArrayList();
            ArrayList list6 = new ArrayList();
            string str8 = "";
            string str9 = "";
            list5.Add(this.objVerification_Desc);
            list6 = this._bill_Sys_BillTransaction.Get_Node_Type(list5, currentConnection);
            if (list6.Contains("NFVER"))
            {
                str8 = "OLD";
                str9 = compName + "/" + caseId + "/No Fault File/Bills/" + doctorSpeciality + "/";
            }
            else
            {
                str8 = "NEW";
                str9 = compName + "/" + caseId + "/No Fault File/Medicals/" + doctorSpeciality + "/Bills/";
            }
            string str10 = compName + "/" + caseId + "/Packet Document/";
            this.objCaseDetailsBO = new CaseDetailsBO();
            DataSet set = new DataSet();
            string str11 = "";
            set = this.objCaseDetailsBO.Get1500FormBitForInsurance(companyId, caseId, currentConnection);
            if (set.Tables[0].Rows.Count > 0)
            {
                for (int k = 0; k < set.Tables[0].Rows.Count; k++)
                {
                    str11 = set.Tables[0].Rows[k]["BT_1500_FORM"].ToString();
                }
            }
            if (str11 == "1")
            {
                string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                this.str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, caseId, compName, caseId, companyId, currentConnection);

                if (File.Exists(this.objNF3Template.getPhysicalPath() + str10 + this.str_1500))
                {
                    if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str9))
                    {
                        Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str9);
                    }
                    File.Copy(this.objNF3Template.getPhysicalPath() + str10 + this.str_1500, this.objNF3Template.getPhysicalPath() + str9 + this.str_1500);
                }
                str5 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str9 + this.str_1500;
                ArrayList list7 = new ArrayList();
                if (str8 == "OLD")
                {
                    list7.Add(caseId);
                    list7.Add(str9 + this.str_1500);
                    list7.Add(companyId);
                    list7.Add(caseId);
                    list7.Add(this.str_1500);
                    list7.Add(str9);
                    list7.Add(userName);
                    list7.Add(doctorSpeciality);
                    list7.Add("LN");
                    list7.Add(caseNo);
                    this.objNF3Template.saveGeneratedBillPath(list7, currentConnection);
                }
                else
                {
                    list7.Add(caseId);
                    list7.Add(str9 + this.str_1500);
                    list7.Add(companyId);
                    list7.Add(caseId);
                    list7.Add(this.str_1500);
                    list7.Add(str9);
                    list7.Add(userName);
                    list7.Add(doctorSpeciality);
                    list7.Add("LN");
                    list7.Add(caseNo);
                    list7.Add(list6[0].ToString());
                    this.objNF3Template.saveGeneratedBillPath_New(list7, currentConnection);
                }
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = userId;
                this._DAO_NOTES_EO.SZ_CASE_ID = caseId;
                this._DAO_NOTES_EO.SZ_COMPANY_ID = companyId;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
            }
            else
            {
                string str12 = this._MUVGenerateFunction.get_bt_include(str7, doctorSpeciality, "", "Speciality");
                string str13 = this._MUVGenerateFunction.get_bt_include(str7, "", "WC000000000000000007", "CaseType");
                if ((str12 == "True") && (str13 == "True"))
                {
                    string str14 = compName + "/" + caseId + "/Packet Document/";
                    string str15 = userId;
                    string str16 = userName;
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(caseId, currentConnection);
                   MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str9 + lien.GenratePdfForEmployerWithMuv(str7, billNo, this._bill_Sys_BillTransaction.GetDoctorSpeciality(billNo, companyId), caseId, str16, caseNo, str15), this.objNF3Template.getPhysicalPath() + str14 + this.str_1500, this.objNF3Template.getPhysicalPath() + str9 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                    str5 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str9 + this.str_1500.Replace(".pdf", "_MER.pdf");
                    ArrayList list8 = new ArrayList();
                    if (str8 == "OLD")
                    {
                        list8.Add(caseId);
                        list8.Add(str9 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list8.Add(companyId);
                        list8.Add(caseId);
                        list8.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list8.Add(str9);
                        list8.Add(userName);
                        list8.Add(doctorSpeciality);
                        list8.Add("LN");
                        list8.Add(caseNo);
                        this.objNF3Template.saveGeneratedBillPath(list8, currentConnection);
                    }
                    else
                    {
                        list8.Add(caseId);
                        list8.Add(str9 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list8.Add(companyId);
                        list8.Add(caseId);
                        list8.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list8.Add(str9);
                        list8.Add(userName);
                        list8.Add(doctorSpeciality);
                        list8.Add("LN");
                        list8.Add(caseNo);
                        list8.Add(list6[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list8, currentConnection);
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = userId;
                    this._DAO_NOTES_EO.SZ_CASE_ID = caseId;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = companyId;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                }
                else
                {
                    str5 = lien.GenratePdfForEmployer(companyId, billNo, this._bill_Sys_BillTransaction.GetDoctorSpeciality(billNo, companyId, currentConnection), caseId, userName, caseNo, userId, currentConnection);
                }
            }

            //ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.open('" + str5 + "');", true);
            //ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
        }
    }
    public ServerConnection BeginBillTranaction()
    {



        SqlConnection conn = new SqlConnection(strsqlCon);
        conn.Open();
        ServerConnection svrConnection = new ServerConnection(conn);
        Server server = new Server(svrConnection);


        // server.ConnectionContext.BeginTransaction();
        return server.ConnectionContext;
    }
    private void GenerateAddedBillPDF(string p_szBillNumber, string caseId, string caseNo, string companyId, string compName, string userId, string userName, string p_szSpeciality, ServerConnection conn)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._MUVGenerateFunction = new MUVGenerateFunction();
            string str = p_szSpeciality;
            // caseId = caseId;
            string billNo = p_szBillNumber;
            this.objNF3Template = new Bill_Sys_NF3_Template();
            this.objVerification_Desc = new Bill_Sys_Verification_Desc();
            this.objVerification_Desc.sz_bill_no = p_szBillNumber;
            this.objVerification_Desc.sz_company_id = companyId;
            this.objVerification_Desc.sz_flag = "BILL";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            string str2 = "";
            string str3 = "";
            string str4 = "";
            list.Add(this.objVerification_Desc);
            list2 = this._bill_Sys_BillTransaction.Get_Node_Type(list, conn);
            if (list2.Contains("NFVER"))
            {
                str2 = "OLD";
                str4 = compName + "/" + caseId + "/No Fault File/Bills/" + str + "/";
            }
            else
            {
                str2 = "NEW";
                str4 = compName + "/" + caseId + "/No Fault File/Medicals/" + str + "/Bills/";
            }
            CaseDetailsBO sbo = new CaseDetailsBO();
            string str5 = companyId;
            if (sbo.GetCaseType(billNo, conn) == "WC000000000000000002")
            {
                string str6 = compName + "/" + caseId + "/Packet Document/";
                string str7 = compName + "/" + caseId + "/Packet Document/";
                this.objCaseDetailsBO = new CaseDetailsBO();
                DataSet set = new DataSet();
                string str8 = "";
                set = this.objCaseDetailsBO.Get1500FormBitForInsurance(companyId, p_szBillNumber, conn);
                if (set.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                    {
                        str8 = set.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                    }
                }
                if (str8 == "1")
                {
                    string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                    Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                    this.str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, p_szBillNumber, compName, caseId, companyId, conn);
                    ArrayList list3 = new ArrayList();
                    if (File.Exists(this.objNF3Template.getPhysicalPath() + str7 + this.str_1500))
                    {
                        if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str4))
                        {
                            Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str4);
                        }
                        File.Copy(this.objNF3Template.getPhysicalPath() + str7 + this.str_1500, this.objNF3Template.getPhysicalPath() + str4 + this.str_1500);
                    }
                    str3 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str4 + this.str_1500;
                    if (str2 == "OLD")
                    {
                        list3.Add(p_szBillNumber);
                        list3.Add(str4 + this.str_1500);
                        list3.Add(companyId);
                        list3.Add(caseId);
                        list3.Add(this.str_1500);
                        list3.Add(str4);
                        list3.Add(userName);
                        list3.Add(str);
                        list3.Add("NF");
                        list3.Add(caseNo);
                        this.objNF3Template.saveGeneratedBillPath(list3, conn);
                    }
                    else
                    {
                        list3.Add(p_szBillNumber);
                        list3.Add(str4 + this.str_1500);
                        list3.Add(companyId);
                        list3.Add(caseId);
                        list3.Add(this.str_1500);
                        list3.Add(str4);
                        list3.Add(userName);
                        list3.Add(str);
                        list3.Add("NF");
                        list3.Add(caseNo);
                        list3.Add(list2[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list3, conn);
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = userId;
                    this._DAO_NOTES_EO.SZ_CASE_ID = caseId;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = companyId;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);



                    //this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str3.ToString() + "'); ", true);
                    //ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                }
                else
                {
                    string str9 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                    ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                    ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                    ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                    Bill_Sys_Configuration configuration = new Bill_Sys_Configuration();
                    string str10 = configuration.getConfigurationSettings(str5, "GET_DIAG_PAGE_POSITION");
                    string str11 = configuration.getConfigurationSettings(str5, "DIAG_PAGE");
                    string str12 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                    string str13 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                    string str14 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                    string str15 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                    GenerateNF3PDF enfpdf = new GenerateNF3PDF();
                    this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                    string str16 = enfpdf.GeneratePDF(companyId, compName, userId, userName, caseId, p_szBillNumber, "", str9, conn);
                    log.Debug("Bill Details PDF File : " + str16);
                    string str17 = this.objPDFReplacement.ReplacePDFvalues(str12, str13, p_szBillNumber, compName, caseId, conn);
                    log.Debug("Page1 : " + str17);
                    string str18 = this.objPDFReplacement.MergePDFFiles(companyId, compName, caseId, p_szBillNumber, str17, str16);
                    string str19 = this.objPDFReplacement.ReplacePDFvalues(str14, str15, p_szBillNumber, compName, caseId, conn);
                    string str20 = companyId;
                    this.bt_include = this._MUVGenerateFunction.get_bt_include(str20, str, "", "Speciality");
                    string str21 = this._MUVGenerateFunction.get_bt_include(str20, "", "WC000000000000000002", "CaseType");
                    if ((this.bt_include == "True") && (str21 == "True"))
                    {
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(p_szBillNumber, conn);
                    }
                    log.Debug(str18 + "merge : " + str19);
                   MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str6 + str18, this.objNF3Template.getPhysicalPath() + str6 + str19, this.objNF3Template.getPhysicalPath() + str6 + str19.Replace(".pdf", "_MER.pdf"));
                    string str22 = str19.Replace(".pdf", "_MER.pdf");
                    if ((this.bt_include == "True") && (str21 == "True"))
                    {
                       MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str6 + str22, this.objNF3Template.getPhysicalPath() + str6 + this.str_1500, this.objNF3Template.getPhysicalPath() + str6 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        str22 = this.str_1500.Replace(".pdf", "_MER.pdf");
                    }
                    string str23 = "";
                    str23 = str6 + str22;
                    log.Debug("GenereatedFileName : " + str23);
                    string str24 = "";
                    str24 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str23;
                    string path = this.objNF3Template.getPhysicalPath() + "/" + str23;
                    CutePDFDocumentClass class2 = new CutePDFDocumentClass();
                    string str26 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                    class2.initialize(str26);
                    if ((((class2 != null) && File.Exists(path)) && ((str11 != "CI_0000003") && (this.objNF3Template.getDiagnosisCodeCount(p_szBillNumber, conn) >= 5))) && ((str10 == "CK_0000003") && ((str11 != "CI_0000004") || (this.objNF3Template.getDiagnosisCodeCount(p_szBillNumber, conn) != 5))))
                    {
                        str16 = path.Replace(".pdf", "_NewMerge.pdf");
                    }
                    string str27 = "";
                    if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str23 = path.Replace(".pdf", "_New.pdf").ToString();
                    }
                    if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_NewMerge.pdf").ToString()))
                    {
                        str27 = str24.Replace(".pdf", "_NewMerge.pdf").ToString();
                        //this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str24.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                        //this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                    }
                    else if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str27 = str24.Replace(".pdf", "_New.pdf").ToString();
                        //this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str24.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                        //this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                    }
                    else
                    {
                        str27 = str24.ToString();
                        //this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str24.ToString() + "'); ", true);
                        //this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                    }
                    this.pdfpath = str27;
                    string str28 = "";
                    string[] strArray = str27.Split(new char[] { '/' });
                    ArrayList list4 = new ArrayList();
                    str27 = str27.Remove(0, ApplicationSettings.GetParameterValue("DocumentManagerURL").Length);
                    str28 = strArray[strArray.Length - 1].ToString();
                    if (File.Exists(this.objNF3Template.getPhysicalPath() + str7 + str28))
                    {
                        if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str4))
                        {
                            Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str4);
                        }
                        File.Copy(this.objNF3Template.getPhysicalPath() + str7 + str28, this.objNF3Template.getPhysicalPath() + str4 + str28);
                    }
                    if (str2 == "OLD")
                    {
                        list4.Add(p_szBillNumber);
                        list4.Add(str4 + str28);
                        list4.Add(companyId);
                        list4.Add(caseId);
                        list4.Add(strArray[strArray.Length - 1].ToString());
                        list4.Add(str4);
                        list4.Add(userName);
                        list4.Add(str);
                        list4.Add("NF");
                        list4.Add(caseNo);
                        this.objNF3Template.saveGeneratedBillPath(list4, conn);
                    }
                    else
                    {
                        list4.Add(p_szBillNumber);
                        list4.Add(str4 + str28);
                        list4.Add(companyId);
                        list4.Add(caseId);
                        list4.Add(strArray[strArray.Length - 1].ToString());
                        list4.Add(str4);
                        list4.Add(userName);
                        list4.Add(str);
                        list4.Add("NF");
                        list4.Add(caseNo);
                        list4.Add(list2[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list4, conn);
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str28;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = userId;
                    this._DAO_NOTES_EO.SZ_CASE_ID = caseId;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = companyId;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    //this.BindLatestTransaction();
                }
            }
            else if (sbo.GetCaseType(billNo, conn) == "WC000000000000000003")
            {
                string str29;
                string companyName;
                Bill_Sys_PVT_Template template = new Bill_Sys_PVT_Template();
                bool flag = false;
                string str31 = caseId;
                string str32 = p_szBillNumber;
                string str33 = userName;
                string str34 = userId;
                //if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != companyId))
                //{
                //    companyName = new Bill_Sys_NF3_Template().GetCompanyName(companyId);
                //    str29 = companyId;
                //}
                // else
                {
                    companyName = compName;
                    str29 = companyId;
                }


                template.GeneratePVTBill(flag, str29, str31, str, companyName, str32, str33, str34, conn);
                //this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
            }
            else
            {
                //this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);


            }
            new Bill_Sys_BillTransaction_BO();
        }
        catch (Exception ex)
        {
            throw ex;
            //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //using (Utils utility = new Utils())
            //{
            //    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            //}
            //string str2 = "Error Request=" + id + ".Please share with Technical support.";
            //base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
   

    //public static string CheckWCFile(string billNo)
    //{

    //    DataSet dataSet = new DataSet();
    //    dataSet = getBillList(billNo);

    //    string str1 = dataSet.Tables[0].Rows[0][1].ToString();
    //    str1 = str1.Replace("\\", "/");
    //    string[] strArrays = str1.Split(new char[] { '/' });
    //    string str2 = "";
    //    for (int i = 0; i < (int)strArrays.Length; i++)
    //    {
    //        str2 = (i != 0 ? string.Concat(str2, "/", strArrays[i].Trim()) : strArrays[i].Trim());
    //    }
    //    string str3 = str2;
    //    Bill_Sys_NF3_Template billSysNF3Template1 = new Bill_Sys_NF3_Template();
    //    string str4 = dataSet.Tables[0].Rows[0][5].ToString();
    //    string physicalPath = dataSet.Tables[0].Rows[0][4].ToString();
    //    string str5 = str3;
    //    str5 = str5.Replace(str4, physicalPath);

    //    using (PdfReader reader = new PdfReader(str5))
    //    {
    //        StringBuilder text = new StringBuilder();

    //        for (int i = 1; i <= reader.NumberOfPages; i++)
    //        {
    //            text.Append(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i));
    //        }


    //        string str = "1";
    //        if (text.ToString().Contains("C-4.2"))
    //        {
    //            str = "2";
    //        }
    //        return str.ToString();
    //    }

    //}
    public static DataSet getBillList(string p_szBillNumber)
    {
        DataSet dataSet = new DataSet();
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("SP_GET_BILL_LIST", sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillNumber);
                (new SqlDataAdapter(sqlCmd)).Fill(dataSet);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return dataSet;
    }
}