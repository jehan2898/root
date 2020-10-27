using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;
using System.IO;
using log4net;


using System.Collections;
using System.ComponentModel;
using System.Drawing;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using testXFAItextSharp.DAO;
using Microsoft.SqlServer.Management.Common;
/// <summary>
/// Summary description for Bill_Sys_Formating_Pdf
/// </summary>
public class Bill_Sys_Formating_Pdf
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    private string szProcedureName = "";
    private static ILog log = LogManager.GetLogger("PDFValueReplacement");
    public Bill_Sys_Formating_Pdf()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public Object FoeMating_Pdf(string szBillNumber, string szXmlPath, string szPdfPath, string szCompnayId, string szCaseID,string CmpName)
    {
        log.Debug("FoeMating_Pdf");
     Bill_Sys_Data  obj = new Bill_Sys_Data();
    
       
        XmlDocument doc1 = new XmlDocument();

        doc1.Load(szXmlPath);

        XmlNodeList xmlProcNameList;
        xmlProcNameList = doc1.GetElementsByTagName("document");

        foreach (XmlNode x in xmlProcNameList)
        {
            szProcedureName = x.Attributes["procedure_name"].Value;
        }
        DataSet ds = new DataSet();
        ds = getDataSet(szBillNumber);
        DataTableReader dtr = ParseXMLFormatePdf(szXmlPath, ds, szCompnayId);

        string XML1 = ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(); 
        string XML2 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_P1"].ToString(); 
        string XML3 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_P2"].ToString(); 
        string XML4 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(); 
        string XML5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_P1"].ToString(); 
        string XML6 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_P2"].ToString(); 

        string Path = "";
        if (szPdfPath.Contains("c4.0Part1_New.pdf") || szPdfPath.Contains("C4.0Part2New.pdf") || szPdfPath.Contains("c4_worestrictions.pdf"))
        {
            if (szXmlPath == XML1)
            {
                Bill_Sys_C4PDF objC4 = new Bill_Sys_C4PDF();
                Path = objC4.printC4(szPdfPath, szBillNumber, CmpName, szCaseID);
            }
            else if (szXmlPath == XML2)
            {
                Bill_Sys_C4PDF objC4 = new Bill_Sys_C4PDF();
                Path = objC4.printC4Part1(szPdfPath, szBillNumber, CmpName, szCaseID);
            }
            else if (szXmlPath == XML3)
            {
                Bill_Sys_C4PDF objC4 = new Bill_Sys_C4PDF();
                Path = objC4.printC4Part2(szPdfPath, szBillNumber, CmpName, szCaseID);
            }
            else if (szXmlPath == XML4)
            {
                Bill_Sys_C4PDF objC4 = new Bill_Sys_C4PDF();
                Path = objC4.printC4ForTest(szPdfPath, szBillNumber, CmpName, szCaseID);
            }
            else if (szXmlPath == XML5)
            {
                Bill_Sys_C4PDF objC4 = new Bill_Sys_C4PDF();
                Path = objC4.printC4Part1ForTest(szPdfPath, szBillNumber, CmpName, szCaseID);
            }
            else if (szXmlPath == XML6)
            {
                Bill_Sys_C4PDF objC4 = new Bill_Sys_C4PDF();
                Path = objC4.printC4Part2ForTest(szPdfPath, szBillNumber, CmpName, szCaseID);
            }
        }
        else
        {
            PDFValueReplacement.PDFValueReplacement objReplce = new PDFValueReplacement.PDFValueReplacement();
            Path = objReplce.ReplaceAndFromatingPDFvalues(szXmlPath, szPdfPath, szBillNumber, CmpName, szCaseID, dtr);
        }
       
        //PDFValueReplacement.PDFValueReplacement objReplce = new PDFValueReplacement.PDFValueReplacement();
        //string Path = objReplce.ReplaceAndFromatingPDFvalues(szXmlPath, szPdfPath, szBillNumber, CmpName, szCaseID, dtr);   
          obj.billnumber=szBillNumber;
          obj.billurl=Path;
          obj.companyid=szCompnayId;

        return obj;
    }
    public Object FoeMating_Pdf(string szBillNumber, string szXmlPath, string szPdfPath, string szCompnayId, string szCaseID, string CmpName,ServerConnection conn)
    {
        log.Debug("FoeMating_Pdf");
        Bill_Sys_Data obj = new Bill_Sys_Data();


        XmlDocument doc1 = new XmlDocument();

        doc1.Load(szXmlPath);

        XmlNodeList xmlProcNameList;
        xmlProcNameList = doc1.GetElementsByTagName("document");

        foreach (XmlNode x in xmlProcNameList)
        {
            szProcedureName = x.Attributes["procedure_name"].Value;
        }
        DataSet ds = new DataSet();
        ds = getDataSet(szBillNumber,conn);
        DataTableReader dtr = ParseXMLFormatePdf(szXmlPath, ds, szCompnayId);

        string XML1 = ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString();
        string XML2 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_P1"].ToString();
        string XML3 = ConfigurationManager.AppSettings["BILLING_TEMPLATE_VARIABLES_FILE_FOR_C4_P2"].ToString();
        string XML4 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString();
        string XML5 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_P1"].ToString();
        string XML6 = ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4_P2"].ToString();

        string Path = "";
        if (szPdfPath.Contains("c4.0Part1_New.pdf") || szPdfPath.Contains("C4.0Part2New.pdf") || szPdfPath.Contains("c4_worestrictions.pdf"))
        {
            if (szXmlPath == XML1)
            {
                Bill_Sys_C4PDF objC4 = new Bill_Sys_C4PDF();
                Path = objC4.printC4(szPdfPath, szBillNumber, CmpName, szCaseID);
            }
            else if (szXmlPath == XML2)
            {
                Bill_Sys_C4PDF objC4 = new Bill_Sys_C4PDF();
                Path = objC4.printC4Part1(szPdfPath, szBillNumber, CmpName, szCaseID);
            }
            else if (szXmlPath == XML3)
            {
                Bill_Sys_C4PDF objC4 = new Bill_Sys_C4PDF();
                Path = objC4.printC4Part2(szPdfPath, szBillNumber, CmpName, szCaseID);
            }
            else if (szXmlPath == XML4)
            {
                Bill_Sys_C4PDF objC4 = new Bill_Sys_C4PDF();
                Path = objC4.printC4ForTest(szPdfPath, szBillNumber, CmpName, szCaseID);
            }
            else if (szXmlPath == XML5)
            {
                Bill_Sys_C4PDF objC4 = new Bill_Sys_C4PDF();
                Path = objC4.printC4Part1ForTest(szPdfPath, szBillNumber, CmpName, szCaseID);
            }
            else if (szXmlPath == XML6)
            {
                Bill_Sys_C4PDF objC4 = new Bill_Sys_C4PDF();
                Path = objC4.printC4Part2ForTest(szPdfPath, szBillNumber, CmpName, szCaseID);
            }
        }
        else
        {
            PDFValueReplacement.PDFValueReplacement objReplce = new PDFValueReplacement.PDFValueReplacement();
            Path = objReplce.ReplaceAndFromatingPDFvalues(szXmlPath, szPdfPath, szBillNumber, CmpName, szCaseID, dtr);
        }

        //PDFValueReplacement.PDFValueReplacement objReplce = new PDFValueReplacement.PDFValueReplacement();
        //string Path = objReplce.ReplaceAndFromatingPDFvalues(szXmlPath, szPdfPath, szBillNumber, CmpName, szCaseID, dtr);   
        obj.billnumber = szBillNumber;
        obj.billurl = Path;
        obj.companyid = szCompnayId;

        return obj;
    }

    public DataSet getDataSet(string sz_BillNumber)
    {
        log.Debug("getDataSet");
        conn = new SqlConnection(strConn);
        conn.Open();
        DataSet ds = new DataSet();
        try
        {
            comm = new SqlCommand(szProcedureName, conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", sz_BillNumber);
            comm.Parameters.AddWithValue("@FLAG", "ALL");
            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }
    public DataSet getDataSet(string sz_BillNumber,ServerConnection conn)
    {
        log.Debug("getDataSet");
       
        DataSet ds = new DataSet();
        try
        {
            string Query = "";
            Query = Query + "Exec "+ szProcedureName + " ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", sz_BillNumber, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "ALL", ",");
            Query = Query.TrimEnd(',');
            ds = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
          
        }

        return ds;
    }
    private DataTableReader ParseXMLFormatePdf(string p_szPath, DataSet ds,string sz_company_id)
    {
        log.Debug("ParseXMLFormatePdf");
        DataTableReader reader = null;
        try
        {
            ArrayList objRArrList = new ArrayList();
            ArrayList objDiagnosisGrid = new ArrayList();
            ArrayList objServiceGrid = new ArrayList();
            XmlDocument doc1 = new XmlDocument();

            doc1.Load(p_szPath);
            XmlNodeList xmlNodeList;
            xmlNodeList = doc1.GetElementsByTagName("field");

            string sz_phone_no = "";
            DAO_Bill_Sys_XMLPDFFormParameters objParameters = null;
            string s = "";
            foreach (XmlNode xm in xmlNodeList)
            {
                objParameters = new DAO_Bill_Sys_XMLPDFFormParameters();

                // IsTransactionDiagnosis is an optional attribute and will be applicable only to fields with diagnosis codes
                try
                {
                    s = xm.Attributes["is_date_formate"].Value;

                    //objParameters.IsPhoneFormate = xm.Attributes["is_phone_formate"].Value;
                    
                }
                catch (Exception ex)
                {
                    //objParameters.IsPhoneFormate = "0";
                    s="0";
                }
                try
                {
                    objParameters.IsPhoneFormate = xm.Attributes["is_phone_formate"].Value;
                }
                catch (Exception ex1)
                {
                    objParameters.IsPhoneFormate = "0";  
                   
                }
                if (s == "1")
                {
                    log.Debug("S=1");
                    objParameters.PDFFieldName = xm.Attributes["pdf_field_name"].Value;
                    string date = ds.Tables[0].Rows[0][objParameters.PDFFieldName.ToString()].ToString();
                    if (date != "")
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(date);
                            string sz_DateFormat = GetDateFormat(sz_company_id);
                            if (sz_DateFormat != "")
                            {
                                string newdt = dt.ToString(sz_DateFormat);
                                ds.Tables[0].Rows[0][objParameters.PDFFieldName.ToString()] = newdt;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    log.Debug("Done S");
                }
                if (objParameters.IsPhoneFormate == "1")
                {
                    log.Debug("IsPhoneFormate=1");
                    objParameters.PDFFieldName = xm.Attributes["pdf_field_name"].Value;
                   
                    string sz_phone_Fromate_Value = "";
                    try
                    {
                        sz_phone_no = ds.Tables[0].Rows[0][objParameters.PDFFieldName.ToString()].ToString();

                    }
                    catch (Exception _ex_)
                    {
                        sz_phone_no = "";
                        throw;
                    }
                    if (sz_phone_no != "")

                    {
                        int bit = 500;
                        string val = getkeySetting(sz_company_id);
                        if (val == "(xxx)xxx-xxxx")
                        {
                            bit = 1;
                        }
                        else if (val == "xxx-xxx-xxxx")
                        {
                            bit = 0;
                        }
                        if (val!="")
                        {
                        
                                if (bit == 1)
                                {
                                    if (sz_phone_no.Length > 10 && sz_phone_no.Contains("(") && sz_phone_no.Contains("-"))
                                    {
                                    }
                                    else if (sz_phone_no.Length > 10 && !sz_phone_no.Contains("(") && sz_phone_no.Contains("-"))
                                    {
                                        try
                                        {

                                            char[] phone = sz_phone_no.ToCharArray();
                                            if (phone[3].ToString() == "-" && phone[7].ToString()=="-")
                                            {
                                                ds.Tables[0].Rows[0][objParameters.PDFFieldName] = "(" + phone[0].ToString()+""+ phone[1].ToString()+""+ phone[2].ToString() + ")" + phone[4].ToString() + "" + phone[5].ToString() + "" + phone[6].ToString() + "-" + phone[8].ToString() + "" + phone[9].ToString() + "" + phone[10].ToString() +""+ phone[11].ToString();
                                            }
                                        }
                                        catch (Exception _ex_phone)
                                        {

                                        }
                                    }
                                    else if (!sz_phone_no.Contains("(") && !sz_phone_no.Contains("-"))
                                    {

                                        try
                                        {

                                            char[] phone = sz_phone_no.ToCharArray();
                                            ds.Tables[0].Rows[0][objParameters.PDFFieldName] = "(" + phone[0].ToString()+""+ phone[1].ToString()+""+ phone[2].ToString() + ")" + phone[3].ToString() + "" + phone[4].ToString() + "" + phone[5].ToString() + "-" + phone[6].ToString() + "" + phone[7].ToString() + "" + phone[8].ToString() + "" + phone[9].ToString();
                                        }
                                        catch (Exception _ex_phone)
                                        {

                                        }
                                    }


                                }

                                if (bit == 0)
                                {
                                    if (sz_phone_no.Length > 10 && sz_phone_no.Contains("(") && sz_phone_no.Length == 13)
                                    {
                                        try
                                        {
                                            char[] phone = sz_phone_no.ToCharArray();
                                            if (phone[0].ToString()=="(" && phone[4].ToString()==")")
                                            {
                                                ds.Tables[0].Rows[0][objParameters.PDFFieldName] = phone[1].ToString()+""+ phone[2].ToString()+""+ phone[3].ToString() + "-" + phone[5].ToString() + "" + phone[6].ToString() + "" + phone[7].ToString() + "-" + phone[9].ToString() + "" + phone[10].ToString() + "" + phone[11].ToString() + "" + phone[12].ToString();
                                            }
                                        }
                                        catch (Exception _ex_)
                                        {
                                        }
                                    }
                                    else if (sz_phone_no.Length > 10 && !sz_phone_no.Contains("(") && sz_phone_no.Contains("-"))
                                    {

                                    }
                                    else if (!sz_phone_no.Contains("(") && !sz_phone_no.Contains("-") && sz_phone_no.Length == 10)
                                    {

                                        try
                                        {

                                            char[] phone = sz_phone_no.ToCharArray();
                                            ds.Tables[0].Rows[0][objParameters.PDFFieldName] = phone[0].ToString() + "" + phone[1].ToString() + "" + phone[2].ToString() + "-" + phone[3].ToString() + "" + phone[4].ToString() + "" + phone[5].ToString() + "-" + phone[6].ToString() + "" + phone[7].ToString() + "" + phone[8].ToString() + "" + phone[9].ToString();
                                        }
                                        catch (Exception _ex_)
                                        {
                                        }
                                    }



                                }
                                log.Debug("IsPhoneFormate Done");
                    }
                    }

                }

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        reader = ds.Tables[0].CreateDataReader();

        return reader;
    }

    public string GetDateFormat(string STR_COMPANY_ID)
    {

        conn = new SqlConnection(strConn);
        conn.Open();
        DataSet ds = new DataSet();
        string szReturn = "";
        try
        {
            string query = "SELECT isnull( SZ_FORMAT_VALUE,'')  FROM TXN_WC_PRINT_CONFIGURATION WHERE SZ_COMPANY_ID='" + STR_COMPANY_ID + "' AND SZ_FORMAT_CODE= 'Date' AND SZ_TYPE='WC'";
            comm = new SqlCommand(query,conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader SDR;
            SDR = comm.ExecuteReader();
            while (SDR.Read())
            {
                szReturn = SDR[0].ToString();
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally 
        {
            conn.Close();
        }
        return szReturn;
    }

    //private string getkeySetting(string sz_compni_id,string sz_sys_key_id)
    //{

    //    conn = new SqlConnection(strConn);
    //    conn.Open();
    //    DataSet ds = new DataSet();
    //    string szReturn = "";
    //    try
    //    {
    //        comm = new SqlCommand("SP_GET_SYS_KEY_VALUE", conn);

    //        comm.CommandType = CommandType.StoredProcedure;
    //        comm.Parameters.AddWithValue("@SZ_SYS_SETTING_KEY_ID", sz_sys_key_id);
    //        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_compni_id);
    //        SqlDataReader dr1;
    //        dr1 = comm.ExecuteReader();
    //        while (dr1.Read())
    //        {
    //            szReturn = dr1[0].ToString();
    //        }
    //    }
    //    catch (Exception ex_getkeySetting)
    //    {
    //        szReturn = "";
    //    }
    //    finally
    //    {
    //        conn.Close();
           
    //    }

    //    return szReturn;


    //}

    private string getkeySetting(string sz_compni_id)
    {

        conn = new SqlConnection(strConn);
        conn.Open();
        DataSet ds = new DataSet();
        string szReturn = "";
        try
        {
            comm = new SqlCommand("SP_GET_WC_PHONE_CONFIG", conn);



            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_compni_id);
            SqlDataReader dr1;
            dr1 = comm.ExecuteReader();
            while (dr1.Read())
            {
                szReturn = dr1[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();

        }

        return szReturn;


    }

    private class DAO_Bill_Sys_XMLPDFFormParameters
    {
        private string szDBFieldName;
        private string szPDFFieldName;
        private string szFieldIndex;
        private string szSpacePadding;
        private string szIfBlank;
        private string szIsCheckBox;
        private string szIsBit;
        private string szFlipIndex;
        private string szIsTransactionDiagnosis;
        private string szIsService;
        private string szDBTextFieldName;
        private string szFlagName;
        private string szProcedureName;
        private string szIsPhoneFormate;

        public string IsPhoneFormate
        {
            get
            {
                return szIsPhoneFormate;
            }
            set
            {
                szIsPhoneFormate = value;
            }
        }

        public string IsService
        {
            get
            {
                return szIsService;
            }
            set
            {
                szIsService = value;
            }
        }

        public string IsBit
        {
            get
            {
                return szIsBit;
            }
            set
            {
                szIsBit = value;
            }
        }

        public string FlipIndex
        {
            get
            {
                return szFlipIndex;
            }
            set
            {
                szFlipIndex = value;
            }
        }

        public string IsCheckBox
        {
            get
            {
                return szIsCheckBox;
            }
            set
            {
                szIsCheckBox = value;
            }
        }

        public string IsTransactionDiagnosis
        {
            get
            {
                return szIsTransactionDiagnosis;
            }
            set
            {
                szIsTransactionDiagnosis = value;
            }
        }

        public string SpacePadding
        {
            get
            {
                return szSpacePadding;
            }
            set
            {
                szSpacePadding = value;
            }
        }

        public string IfBlank
        {
            get
            {
                return szIfBlank;
            }
            set
            {
                szIfBlank = value;
            }
        }

        public string DBFieldName
        {
            get
            {
                return szDBFieldName;
            }
            set
            {
                szDBFieldName = value;
            }
        }

        public string FieldIndex
        {
            get
            {
                return szFieldIndex;
            }
            set
            {
                szFieldIndex = value;
            }
        }


        public string PDFFieldName
        {
            get
            {
                return szPDFFieldName;
            }
            set
            {
                szPDFFieldName = value;
            }
        }

        public string DBTextFieldName
        {
            get
            {
                return szDBTextFieldName;
            }
            set
            {
                szDBTextFieldName = value;
            }
        }

        public string DBProcedureName
        {
            get
            {
                return szProcedureName;
            }
            set
            {
                szProcedureName = value;
            }
        }

        public string FlagName
        {
            get
            {
                return szFlagName;
            }
            set
            {
                szFlagName = value;
            }
        }



    }
}


