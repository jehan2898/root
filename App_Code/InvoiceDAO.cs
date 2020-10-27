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
using System.IO;  
using System.Collections;
 

/// <summary>
/// Summary description for InvoiceDAO
/// </summary>
/// 

public class InvoiceGetSet
{

    private string szItemID = null;
    private string szQuantity = null;
    private string szUnitePrice = null;
    private string szAmount = null;
    private string szInvoiceItemId = null;


    public string ItemID
    {
        get
        {
            return szItemID;
        }
        set
        {
            this.szItemID = value;
        }
    }

    public string Quantity
    {
        get
        {
            return szQuantity;
        }
        set
        {
            this.szQuantity = value;
        }
    }

    public string UnitePrice
    {
        get
        {
            return szUnitePrice;
        }
        set
        {
            this.szUnitePrice = value;
        }
    }

    public string Amount
    {
        get
        {
            return szAmount;
        }
        set
        {
            this.szAmount = value;
        }
    }

    public string InvoiceItemId
    {
        get
        {
            return szInvoiceItemId;
        }
        set
        {
            this.szInvoiceItemId = value;
        }
    }
}

public class InvoiceDAO
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;
    string Company_Name = "",Case_Id="";
    InvoiceDAO _InvoiceDAO;
    public InvoiceDAO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }    

    public DataSet getInvoiceItemPrice(string   P_szItemID,string p_sz_Company_Id)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_INVOICE_ITEM_PRICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            if (P_szItemID.ToString() != "NA")
            {
                sqlCmd.Parameters.AddWithValue("@I_INVOICE_ITEM_ID", Convert.ToInt32(P_szItemID));
            }
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_sz_Company_Id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_ITEM_PTICE");
            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
        return ds;
    }

    public string SaveInvoice(string P_Case_Id, string P_Company_Id, string P_Date_Of_Service, double P_Total, string P_Created_Date, string P_Created_User_Id, double P_Shipping, string P_Company_Name, string P_Flag,string P_InvoiceId,string P_User_Name,string P_Case_No,string P_ProviderID, ArrayList objArr,ArrayList objArrRecords,string Sz_Person_Name,string SZ_Person_Address,string SZ_Zip,string SZ_State,string SZ_City,string SZ_User_name)
    {
        ArrayList objDeletList = new ArrayList();
        Company_Name = P_Company_Name;
        Case_Id = P_Case_Id;
        InvoiceGetSet _InvoiceGetSet;
        InvoiceDAO _InvoiceDAO = new InvoiceDAO();
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        DataSet ds2 = new DataSet();
        DataSet ds1 = new DataSet();
        sqlCmd = new SqlCommand();
        sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        string InvoiceId="";
        SqlTransaction transaction;
        sqlCon.Open();                 
        transaction = sqlCon.BeginTransaction();
        try
        {
            if (P_Flag == "UPDATE")
            {
                for (int j = 0; j < objArrRecords.Count; j++)
                {
                    string flag = "true";
                    for (int i = 0; i < objArr.Count; i++)
                    {
                        _InvoiceGetSet = new InvoiceGetSet();
                        _InvoiceGetSet = (InvoiceGetSet)objArr[i];
                        if (_InvoiceGetSet.InvoiceItemId == objArrRecords[j].ToString())
                        {
                            flag = "false";
                        }
                    }
                    if (flag == "true")
                    {
                        objDeletList.Add(objArrRecords[j].ToString());
                    }
                }
            }
            sqlCmd.CommandText = "SP_SAVE_TXN_INVOICE";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Transaction = transaction;

            
                #region "Save Invoice in Txn_Invoice"
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", P_Case_Id);
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                sqlCmd.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", P_Date_Of_Service);
                sqlCmd.Parameters.AddWithValue("@FLT_TOTAL", P_Total);
                sqlCmd.Parameters.AddWithValue("@FLT_RECIVED", 0.00);
                sqlCmd.Parameters.AddWithValue("@FLT_PENDING", 0.00);                
                sqlCmd.Parameters.AddWithValue("@DT_UPDATED_DATE", P_Created_Date);
                sqlCmd.Parameters.AddWithValue("@SZ_UPDATED_USEr_ID", P_Created_User_Id);
                sqlCmd.Parameters.AddWithValue("@FLT_SHIIPING_HANDELING", P_Shipping);
                sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", P_ProviderID);
                sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_ID", P_InvoiceId);

                sqlCmd.Parameters.AddWithValue("@SZ_PERSON_NAME", Sz_Person_Name);
                sqlCmd.Parameters.AddWithValue("@SZ_PERSON_ADDRESS", SZ_Person_Address);
                sqlCmd.Parameters.AddWithValue("@SZ_PERSON_CITY", SZ_City);
                sqlCmd.Parameters.AddWithValue("@SZ_PERSON_STATE",Convert.ToInt32(SZ_State));
                sqlCmd.Parameters.AddWithValue("@SZ_PERSON_ZIP", SZ_Zip);


                sqlCmd.Parameters.AddWithValue("@FLAG", P_Flag);
                sqlCmd.Parameters.AddWithValue("@I_INVOICE_ID", SqlDbType.NVarChar);
                sqlCmd.Parameters["@I_INVOICE_ID"].Direction = ParameterDirection.ReturnValue;
                sqlCmd.ExecuteNonQuery();
                InvoiceId = sqlCmd.Parameters["@I_INVOICE_ID"].Value.ToString();
                #endregion

                #region "Delete Particular Item From TXN_INVOICE_DETAILS"
                if (objDeletList.Count > 0)
                {
                    for (int i = 0; i < objDeletList.Count; i++)
                    {                        
                        SqlCommand sqlCmd1 = new SqlCommand();
                        sqlCmd1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        sqlCmd1.CommandText = "SP_SAVE_TXN_INVOICE_DETAIL";
                        sqlCmd1.CommandType = CommandType.StoredProcedure;
                        sqlCmd1.Connection = sqlCon;
                        sqlCmd1.Transaction = transaction;
                        sqlCmd1.Parameters.AddWithValue("@I_INVOICE_DETAIL_ID", objDeletList[i].ToString());                      
                        sqlCmd1.Parameters.AddWithValue("@FLAG", "DELETE");
                        sqlCmd1.ExecuteNonQuery();
                    }
                }
                #endregion

                #region "Save Invoice Details in TXN_INVOICE_DETAIL"
                for (int i = 0; i < objArr.Count; i++)
                {
                    _InvoiceGetSet = new InvoiceGetSet();
                    _InvoiceGetSet = (InvoiceGetSet)objArr[i];
                    SqlCommand sqlCmd1 = new SqlCommand();
                    sqlCmd1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    sqlCmd1.CommandText = "SP_SAVE_TXN_INVOICE_DETAIL";
                    sqlCmd1.CommandType = CommandType.StoredProcedure;
                    sqlCmd1.Connection = sqlCon;
                    sqlCmd1.Transaction = transaction;
                    sqlCmd1.Parameters.AddWithValue("@I_INVOICE_ID", InvoiceId);
                    sqlCmd1.Parameters.AddWithValue("@I_INVOICE_ITEM_ID", _InvoiceGetSet.ItemID);
                    sqlCmd1.Parameters.AddWithValue("@I_QUANTITY", _InvoiceGetSet.Quantity);
                    sqlCmd1.Parameters.AddWithValue("@FLT_UNIT_PRICE", Convert.ToDouble(_InvoiceGetSet.UnitePrice));
                    sqlCmd1.Parameters.AddWithValue("@FLT_AMOUNT", Convert.ToDouble(_InvoiceGetSet.Amount));
                    sqlCmd1.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);                   
                    sqlCmd1.Parameters.AddWithValue("@DT_UPDATED_DATE", P_Created_Date);
                    sqlCmd1.Parameters.AddWithValue("@SZ_UPDATED_USEr_ID", P_Created_User_Id);
                    sqlCmd1.Parameters.AddWithValue("@I_INVOICE_DETAIL_ID", _InvoiceGetSet.InvoiceItemId);
                    sqlCmd1.Parameters.AddWithValue("@FLAG", P_Flag);
                    sqlCmd1.ExecuteNonQuery();
                }
                #endregion               

                #region "Update Invoice Amount in Txn_Invoice"
                SqlCommand sqlCmd2 = new SqlCommand();
                sqlCmd2.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd2.CommandText = "SP_SAVE_TXN_INVOICE";
                sqlCmd2.CommandType = CommandType.StoredProcedure;
                sqlCmd2.Connection = sqlCon;
                sqlCmd2.Transaction = transaction;

                sqlCmd2.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                sqlCmd2.Parameters.AddWithValue("@SZ_INVOICE_ID", P_InvoiceId);
                sqlCmd2.Parameters.AddWithValue("@FLAG", "UPDATEAMOUNT");
                sqlCmd2.Parameters.AddWithValue("@I_INVOICE_ID", SqlDbType.NVarChar);
                sqlCmd2.Parameters["@I_INVOICE_ID"].Direction = ParameterDirection.ReturnValue;
                sqlCmd2.ExecuteNonQuery();
                InvoiceId = sqlCmd.Parameters["@I_INVOICE_ID"].Value.ToString();
                #endregion

                #region "save  multiple date of service"
                string[] values = P_Date_Of_Service.Split(',');

                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i].ToString().Trim() != "" && values[i].ToString() != "1900-01-01 00:00:00.000")
                    {
                        SqlCommand sqlCmd3 = new SqlCommand();
                        sqlCmd3.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        sqlCmd3.CommandText = "SP_SAVE_MUL_DATE_INVOICE";
                        sqlCmd3.CommandType = CommandType.StoredProcedure;
                        sqlCmd3.Connection = sqlCon;
                        sqlCmd3.Transaction = transaction;
                        sqlCmd3.Parameters.AddWithValue("@SZ_CASE_ID", P_Case_Id);
                        sqlCmd3.Parameters.AddWithValue("@DT_DATEOF_SERVICE", values[i].ToString());
                        sqlCmd3.Parameters.AddWithValue("@I_INVOICE_ID", InvoiceId);
                        sqlCmd3.Parameters.AddWithValue("@SZ_USER_NAME", SZ_User_name);
                        sqlCmd3.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                        sqlCmd3.ExecuteNonQuery();
                    }
                }
                
                #endregion
                transaction.Commit();


            #region "Generate And Save PDF"
            ds1 = _InvoiceDAO.getCaseWiseCompanyPatientDetails(P_Case_Id, P_ProviderID);
            ds2 = _InvoiceDAO.getInvoiceWiseItemDetails(InvoiceId);

            string strhtml = GenerateHTML(ds1, ds2, P_Case_No);
            GeneratePDF(strhtml, P_Company_Id, P_Case_Id, P_User_Name, P_Case_No, InvoiceId);
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            transaction.Dispose();
            sqlCon.Close();           
        }
        return InvoiceId;
    }

    public DataSet getInvoiceWiseItemDetails(string P_szInvoiceId)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            if (P_szInvoiceId.ToString() != "")
            {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_INVOICEWISE_ITEM_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());


                       

            sqlCmd.CommandType = CommandType.StoredProcedure;           
            sqlCmd.Parameters.AddWithValue("@I_INVOICE_ID", P_szInvoiceId);            
            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);            
           }
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
        return ds;
    }

    public DataSet getCaseWiseCompanyPatientDetails(string P_szCaseId,string P_Provider_Id)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {          
                sqlCon.Open();
                sqlCmd = new SqlCommand("SP_GET_COMPANY_PATIENT_INFO", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", P_szCaseId);
                sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", P_Provider_Id);
                sqlda = new SqlDataAdapter(sqlCmd);
                sqlda.Fill(ds);            
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
        return ds;
    }

   


    public string GenerateHTML(DataSet objDSLT, DataSet objItemList, string CaseNo)
    {

        string str = "";
        string str1 = "";
        string str2 = "";

        str = "<table width='100%' border='0' cellpadding='0' cellspacing='0'><tr><td width='100%' style='border-color :White;'>" +
              "<table width='672' border='0' cellpadding='0' cellspacing='0' align='center'><tbody> <tr><td valign='top' width='400'></td><td valign='top' width='270'> </td></tr>" +
              "<tr><td></td><td style='font-size:12px' align='right' width='200'><b>Case No:" + CaseNo + "-" + objItemList.Tables[0].Rows[0]["SZ_PREFIX"].ToString() + "</b></td></tr>" +
              "<tr> <td valign='bottom' width='400' style='font-size:14px font-family:Trebuchet MS'><b>" + objDSLT.Tables[0].Rows[0][0].ToString() + "</b></td>" +
              "<td valign='top' width='200' style='font-size:10px' align='right'>Date:  " + objItemList.Tables[0].Rows[0]["DT_CREATED_DATE"].ToString() + "</td></tr>" +

              "<tr><td valign='bottom' width='400' style='font-size:10px font-family:Trebuchet MS'>" + objDSLT.Tables[0].Rows[0][2].ToString() + "<BR>" + objDSLT.Tables[0].Rows[0][3].ToString() + "<BR>" +
              "PHONE:  " + objDSLT.Tables[0].Rows[0][4].ToString() + "<BR>FAX:  " + objDSLT.Tables[0].Rows[0][5].ToString() + "</td>" +
              "<td width='270' style='font-size:11px font-family:Trebuchet MS'>Tax Identification Number: " + objDSLT.Tables[0].Rows[0][6].ToString() + "</td></tr>"+              
             "<tr><td valign='bottom' width='400' style='font-size:10px font-family:Trebuchet MS'></td>" +
              "<td width='270' style='font-size:11px font-family:Trebuchet MS'></td></tr></tbody></table>";



        str1 = "<table width='672' border='0' cellpadding='0' cellspacing='0' align='center'><tr><td style='font-size:12px;'>Bill To</td><td></td></tr>" +
              "<tr> <td valign='top' width='400' style='font-size:14px; font-family:Trebuchet MS;`'><b>" + objItemList.Tables[0].Rows[0][7].ToString() + "</b></td>" +
              "<td valign='top' width='200' style='font-size:10px' align='right'></td></tr>" +
              "<tr><td valign='bottom' width='400' style='font-size:10px font-family:Trebuchet MS'>" + objItemList.Tables[0].Rows[0][8].ToString() + "<BR>" + objItemList.Tables[0].Rows[0][10].ToString()  +
              " , " + objItemList.Tables[0].Rows[0][9].ToString() + " " + objItemList.Tables[0].Rows[0][11].ToString() + "</td>" +
              "<td valign='top' width='270' style='font-size:10px font-family:Trebuchet MS'>PATIENT:  " + objDSLT.Tables[0].Rows[0][7].ToString() + "<BR>&nbsp<BR>" +
              "DATE OF LOSS:  " + objDSLT.Tables[0].Rows[0][8].ToString() + "<BR>&nbsp<BR>" +
              "DATE OF SERVICE:  " + objItemList.Tables[0].Rows[0][12].ToString() + "</td></tr></table>"; 


        str2 = "<div align='center'><table width='672' border='1' cellpadding='0' cellspacing='0' Bordercolor='#000066'><tbody><tr>" +
              "<td width='84' style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF'>QTY</td>" +
              "<td width='372' align='center' style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF'>DESCRIPTION</td> " +
              "<td width='108'  style='font-size:10px font-family:Trebuchet MS' style='background-color:#CECEFF' align='center'>UNIT PRICE</td>" +
              "<td  width='110'  style='font-size:10px font-family:Trebuchet MS' align='center' style='background-color:#CECEFF' >TOTAL AMOUNT</td></tr>";

        for (int i = 0; i < objItemList.Tables[0].Rows.Count; i++)
        {
            str2 = str2 + "<tr>" +
              "<td width='84' style='font-size:10px font-family:Trebuchet MS' align='left'>" + objItemList.Tables[0].Rows[i][0].ToString() + "</td>" +
              "<td width='372' style='font-size:10px font-family:Trebuchet MS'>" + objItemList.Tables[0].Rows[i][1].ToString() + "</td>" +
              "<td width='108' style='font-size:10px font-family:Trebuchet MS' align='Right'>$" + objItemList.Tables[0].Rows[i][2].ToString() + "</td>" +
              "<td width='110' style='font-size:10px font-family:Trebuchet MS' align='Right'>$" + objItemList.Tables[0].Rows[i][3].ToString() + "</td></tr>";
        }
        str2 = str2 + "<tr><td width='84' style='font-size:10px font-family:Trebuchet MS' align='left'></td>" +
                 "<td width='372' style='font-size:10px font-family:Trebuchet MS'></td>"+
                "<td width='108' style='font-size:10px font-family:Trebuchet MS' align='right'>SUBTOTAL</td>" +
                "<td width='108' style='font-size:10px font-family:Trebuchet MS' align='right'>$" + objItemList.Tables[0].Rows[0][4].ToString() + "</td></tr>" +

                "<tr><td width='84' style='font-size:10px font-family:Trebuchet MS' align='left'></td>" +
                 "<td width='372' style='font-size:10px font-family:Trebuchet MS'></td>" +
                "<td width='108' style='font-size:10px font-family:Trebuchet MS' align='right'>SHIPPING & <br>HANDLING</td>" +
                "<td width='108' style='font-size:10px font-family:Trebuchet MS' align='right'>$" + objItemList.Tables[0].Rows[0][5].ToString() + "</td></tr>" +

                "<tr><td width='84' style='font-size:10px font-family:Trebuchet MS' align='left'></td>" +
                 "<td width='372' style='font-size:10px font-family:Trebuchet MS'></td>" +
                "<td width='108' style='font-size:10px font-family:Trebuchet MS' align='right'>TOTAL</td>" +
                "<td width='108' style='font-size:10px font-family:Trebuchet MS' align='right'>$" + objItemList.Tables[0].Rows[0][6].ToString() + "</td></tr></tbody></table></div>" +

                " <table width='672' border='0' cellpadding='0' cellspacing='0' align='center'>" +
                "<tr><td colspan='2' width='666' align='center'>" +
                "<tr><td  style='font-size:10px font-family:Trebuchet MS'>" +
                "<b> Make all checks payable to " + objDSLT.Tables[0].Rows[0][0].ToString() + "<BR>THANK YOU FOR YOUR BUSINESS!</b></td></tr>" +
                "</table></td></tr></td></tr></table>";

        string str4 = str + str1 + str2;
        return str4;

    }

    private string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }

    protected string GeneratePDF(string strHtml, string P_Company_Id, string P_Case_Id, string P_User_Name, string P_Case_No,string P_Invoice_ID)
    {
        GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
        string pdffilename = "";
        try
        {            
            SautinSoft.PdfMetamorphosis objHTMToPDF = new SautinSoft.PdfMetamorphosis();
            _InvoiceDAO = new InvoiceDAO();
            objHTMToPDF.Serial = "10007706603";
            string htmfilename = getFileName("I") + ".htm";
            string FullPath = "";
            pdffilename = getFileName("P") + ".pdf";
            StreamWriter sw = new StreamWriter( ApplicationSettings.GetParameterValue("EXCEL_SHEET") + htmfilename);             
            sw.Write(strHtml);
            sw.Close();
            Int32 iTemp;
            iTemp = objHTMToPDF.HtmlToPdfConvertFile(ApplicationSettings.GetParameterValue("EXCEL_SHEET")+ htmfilename, ApplicationSettings.GetParameterValue("EXCEL_SHEET") + pdffilename);           
            String szDestinationDir = Company_Name + "/" + Case_Id + "/No Fault File/Invoice/";
            FullPath = objNF3Template.getPhysicalPath().ToString() + szDestinationDir.ToString() + pdffilename.ToString();
            string FilePath = szDestinationDir.ToString();
            string Path = szDestinationDir.ToString() + pdffilename.ToString(); 
            if (File.Exists(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + pdffilename))
            {
                if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                {
                    Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                }
                File.Copy(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + pdffilename, objNF3Template.getPhysicalPath() + szDestinationDir + pdffilename);                
            }

            ArrayList objAL1 = new ArrayList();
            objAL1.Add(P_Company_Id);
            objAL1.Add(P_Case_Id);
            objAL1.Add(pdffilename); // SZ_BILL_NAME
            objAL1.Add(FilePath); // SZ_BILL_FILE_PATH
            objAL1.Add(P_User_Name);            
            objAL1.Add("NF");
            objAL1.Add(P_Case_No);
            objAL1.Add(Path);
            objAL1.Add(P_Invoice_ID);
            _InvoiceDAO.saveGeneratedInvoicePath(objAL1);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return pdffilename;
    }

    public DataTable getInvoiceDetails(string P_szInvoiceId,string P_szCompanyId,string P_szCaseId,string P_Flag)
    {
        sqlCon = new SqlConnection(strConn);
        DataTable dt = new DataTable();
        try
        {
            if (P_szInvoiceId.ToString() != "")
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand("SP_GET_INVOICE_DETAILS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@I_INVOICE_ID", P_szInvoiceId);
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_szCompanyId);
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", P_szCaseId);
                sqlCmd.Parameters.AddWithValue("@FLAG", P_Flag); 
                sqlda = new SqlDataAdapter(sqlCmd);
                sqlda.Fill(dt);
            }
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
        return dt;
    }

    public void DeleteInvoiceRecord(string P_InvoiceId,string P_sz_Company_Id)
    {
        sqlCon = new SqlConnection(strConn);
        sqlCmd = new SqlCommand();
        sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DETELE_INVOICE_RECORD", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_ID", P_InvoiceId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_sz_Company_Id);
            sqlCmd.ExecuteNonQuery();
           
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }         
    }

    public void saveGeneratedInvoicePath(ArrayList objArr)//To Save Generated Invoice pdf in Doc manager And Update Path And Image Id In Txn_invoice
    {
        sqlCon = new SqlConnection(strConn);
        SqlCommand sqlCmdDocMan;
        try
        {             
                sqlCon.Open();
                sqlCmdDocMan = new SqlCommand("SP_TXN_INVOICE_GENERATED", sqlCon);
                sqlCmdDocMan.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmdDocMan.CommandType = CommandType.StoredProcedure;
                sqlCmdDocMan.Parameters.AddWithValue("@SZ_COMPANY_ID", objArr[0].ToString());
                sqlCmdDocMan.Parameters.AddWithValue("@SZ_CASE_ID", objArr[1].ToString());
                sqlCmdDocMan.Parameters.AddWithValue("@SZ_INVOICE_NAME", objArr[2].ToString());
                sqlCmdDocMan.Parameters.AddWithValue("@SZ_INVOICE_FILE_PATH", objArr[3].ToString());
                sqlCmdDocMan.Parameters.AddWithValue("@SZ_USER_NAME", objArr[4].ToString());
                sqlCmdDocMan.Parameters.AddWithValue("@CASE_TYPE", objArr[5].ToString());
                sqlCmdDocMan.Parameters.AddWithValue("@SZ_CASE_NO", objArr[6].ToString());             
                sqlCmdDocMan.Parameters.AddWithValue("@SZ_PATH", objArr[7].ToString());
                sqlCmdDocMan.Parameters.AddWithValue("@SZ_INVOICE_ID", objArr[8].ToString());
                sqlCmdDocMan.ExecuteNonQuery();            
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }       
    }

    public string GetPDFPath(string P_invoice_Id, string p_sz_Company_Id)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        string PDF_Path="";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_INVOICE_ITEM_PRICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;            
            sqlCmd.Parameters.AddWithValue("@SZ_INVOICE_ID", P_invoice_Id);             
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_sz_Company_Id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_INVOICE_PATH");
            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
            PDF_Path = ds.Tables[0].Rows[0][0].ToString();
            
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
        return PDF_Path;
    }     


    //Make Payment PopUp Functions
    public void SavePayment(ArrayList objArr)
    {
        sqlCon = new SqlConnection(strConn);
        SqlCommand sqlCmdDocMan=new SqlCommand();
        sqlCmdDocMan.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
         
        sqlCon.Open();
        string InvoiceId="";
         
        try
        {
            #region "Save Or Update TXN_MISC_PAYMENT_TRANSACTIONS table"
            sqlCmdDocMan.CommandText = "SP_TXN_MISC_PAYMENT_TRANSACTIONS";
            sqlCmdDocMan.CommandType = CommandType.StoredProcedure;
            sqlCmdDocMan.Connection = sqlCon;
          
                        
            sqlCmdDocMan.Parameters.AddWithValue("@I_PAYMENT_ID", objArr[0].ToString());
            sqlCmdDocMan.Parameters.AddWithValue("@SZ_CASE_ID", objArr[1].ToString());
            sqlCmdDocMan.Parameters.AddWithValue("@SZ_CHECK_NUMBER", objArr[2].ToString());
            sqlCmdDocMan.Parameters.AddWithValue("@DT_CHECK_DATE", objArr[3].ToString());
            sqlCmdDocMan.Parameters.AddWithValue("@FLT_CHECK_AMOUNT", objArr[4].ToString());
            sqlCmdDocMan.Parameters.AddWithValue("@SZ_COMPANY_ID", objArr[5].ToString());
            sqlCmdDocMan.Parameters.AddWithValue("@SZ_USER_ID", objArr[6].ToString());
            sqlCmdDocMan.Parameters.AddWithValue("@SZ_DESCRIPTION", objArr[7].ToString());
            sqlCmdDocMan.Parameters.AddWithValue("@I_INVOICE_ID",Convert.ToInt32(objArr[8].ToString()));
            sqlCmdDocMan.Parameters.AddWithValue("@FLAG", objArr[9].ToString());
            sqlCmdDocMan.ExecuteNonQuery();
            #endregion            
           
            UpdatePayment(objArr[5].ToString(), objArr[8].ToString());
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
    }

    public void UpdatePayment(string P_CompanyId,string P_InvoiceId)
    {
        sqlCon = new SqlConnection(strConn);
        SqlCommand sqlCmdDocMan = new SqlCommand();
        sqlCmdDocMan.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

        sqlCon.Open();
        string InvoiceId = "";

        try
        {            
            #region "Update Invoice Amount in Txn_Invoice"
            SqlCommand sqlCmd2 = new SqlCommand();
            sqlCmd2.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd2.CommandText = "SP_SAVE_TXN_INVOICE";
            sqlCmd2.CommandType = CommandType.StoredProcedure;
            sqlCmd2.Connection = sqlCon;

            sqlCmd2.Parameters.AddWithValue("@SZ_COMPANY_ID", P_CompanyId.ToString());
            sqlCmd2.Parameters.AddWithValue("@SZ_INVOICE_ID", Convert.ToInt32(P_InvoiceId.ToString()));
            sqlCmd2.Parameters.AddWithValue("@FLAG", "UPDATEAMOUNT");
            sqlCmd2.Parameters.AddWithValue("@I_INVOICE_ID", SqlDbType.NVarChar);
            sqlCmd2.Parameters["@I_INVOICE_ID"].Direction = ParameterDirection.ReturnValue;
            sqlCmd2.ExecuteNonQuery();
            InvoiceId = sqlCmd2.Parameters["@I_INVOICE_ID"].Value.ToString();
            #endregion
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
    }

    public DataSet  GetPayment(string P_szCase_Id,string P_szCompany_Id,string P_InvoiceId)
    {
        sqlCon = new SqlConnection(strConn);
        SqlCommand sqlCmdDocMan;        
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmdDocMan = new SqlCommand("SP_TXN_MISC_PAYMENT_TRANSACTIONS", sqlCon);
            sqlCmdDocMan.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmdDocMan.CommandType = CommandType.StoredProcedure;
            sqlCmdDocMan.Parameters.AddWithValue("@SZ_CASE_ID", P_szCase_Id);
            sqlCmdDocMan.Parameters.AddWithValue("@SZ_COMPANY_ID", P_szCompany_Id);
            sqlCmdDocMan.Parameters.AddWithValue("@I_INVOICE_ID", P_InvoiceId);
            sqlCmdDocMan.Parameters.AddWithValue("@FLAG", "LIST");
            sqlda = new SqlDataAdapter(sqlCmdDocMan);
            sqlda.Fill(ds);
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
        return ds;
    }

    public DataSet GetBalance(string P_szCompany_Id, string P_InvoiceId)
    {
        sqlCon = new SqlConnection(strConn);
        SqlCommand sqlCmdDocMan;
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmdDocMan = new SqlCommand("SP_SAVE_TXN_INVOICE", sqlCon);
            sqlCmdDocMan.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmdDocMan.CommandType = CommandType.StoredProcedure;             
            sqlCmdDocMan.Parameters.AddWithValue("@SZ_COMPANY_ID", P_szCompany_Id);
            sqlCmdDocMan.Parameters.AddWithValue("@SZ_INVOICE_ID", P_InvoiceId);
            sqlCmdDocMan.Parameters.AddWithValue("@FLAG", "GET_BALANCE");
            sqlda = new SqlDataAdapter(sqlCmdDocMan);
            sqlda.Fill(ds);
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
        return ds;
    }
    //End Of Make Payment PopUp Functions


    //Misc Report PopUp Function
    public DataSet GetMiscPaymentDetails(string P_InvoiceId,string P_CompanyId)
    {
        sqlCon = new SqlConnection(strConn);
        SqlCommand sqlCmdDocMan;
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmdDocMan = new SqlCommand("SP_GET_MISC_PAYMENT_DETAILS", sqlCon);
            sqlCmdDocMan.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmdDocMan.CommandType = CommandType.StoredProcedure;
            sqlCmdDocMan.Parameters.AddWithValue("@SZ_COMPANY_ID", P_CompanyId);
            sqlCmdDocMan.Parameters.AddWithValue("@I_INVOICE_ID", P_InvoiceId);             
            sqlda = new SqlDataAdapter(sqlCmdDocMan);
            sqlda.Fill(ds);
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
        return ds;
    }

    public DataSet GetMiscPaymentDetails(ArrayList arrPayment)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_MISC_PAYMENT_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrPayment[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", arrPayment[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NUMBER", arrPayment[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", arrPayment[3].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_FROM_INVOICE_DATE", arrPayment[4].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_INVOICE_DATE", arrPayment[5].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_FROM_CHECK_DATE", arrPayment[6].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_CHECK_DATE", arrPayment[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHECKNUMBER", arrPayment[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHECKAMOUNT", arrPayment[9].ToString());
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
    }
    //End Misc Report PopUp Function
}

public class invoiceobject
{
    private string szcopmanyid = null;
    private string szuserid = null;
    private string dtcreateddate = null;
    private string szpaid = null;
    private string szImageId = null;
    private string szinvoicepath = null;
    private string szinvoicefilename = null;
    private string szbillno = null;


    public string copmanyid
    {
        get
        {
            return szcopmanyid;
        }
        set
        {
            this.szcopmanyid = value;
        }
    }

    public string userid
    {
        get
        {
            return szuserid;
        }
        set
        {
            this.szuserid = value;
        }
    }

    public string createddate
    {
        get
        {
            return dtcreateddate;
        }
        set
        {
            this.dtcreateddate = value;
        }
    }

    public string paid
    {
        get
        {
            return szpaid;
        }
        set
        {
            this.szpaid = value;
        }
    }

    public string ImageId
    {
        get
        {
            return szImageId;
        }
        set
        {
            this.szImageId = value;
        }
    }
    public string invoicepath 
    {
        get
        {
            return szinvoicepath;
        }
        set
        {
            this.szinvoicepath = value;
        }
    }
    public string invoicefilename
    {
        get
        {
            return szinvoicefilename;
        }
        set
        {
            this.szinvoicefilename = value;
        }
    }
    public string billno
    {
        get
        {
            return szbillno;
        }
        set
        {
            this.szbillno = value;
        }
    }
}
