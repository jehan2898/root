using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;
using System.IO;
using System.Data.Sql;
using log4net;
using System.Web;

/// <summary>
/// Summary description for GeneratePatientInfoPDF
/// </summary>
public class GeneratePatientInfoPDF
{
    public GeneratePatientInfoPDF()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    private static ILog log = LogManager.GetLogger("Bill_Sys_PatientInfoPDF");

    private ArrayList ParseXML(string p_szPath)
    {
        try
        {
            log.Debug("Bill_Sys_NF3Service. Method - ParseXML : ");
            Bill_Sys_KeyValue objKeyValue;
            ArrayList objRArrList = new ArrayList();
            XmlDocument doc1 = new XmlDocument();
            doc1.Load(p_szPath);
            XmlNodeList xmlNodeList;
            xmlNodeList = doc1.GetElementsByTagName("key");

            foreach (XmlNode xm in xmlNodeList)
            {
                objKeyValue = new Bill_Sys_KeyValue();
                objKeyValue.Key = xm.Attributes["name"].Value;
                objKeyValue.Value = xm.Attributes["value"].Value;
                objRArrList.Add(objKeyValue);
            }
            return objRArrList;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        return null;
    }


    public string getReplacedString(string p_htmlstring, string p_szCaseID, string p_szCompanyID)
    {
        SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        objSqlConn.Open();
        try
        {
            log.Debug("Bill_Sys_NF3Service. Method - getReplacedString : ");
            string szReplaceString = "";
            szReplaceString = p_htmlstring;
            SqlCommand objSqlComm1 = new SqlCommand("SP_PATIENT_INFORMATION", objSqlConn);
            objSqlComm1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader objDR;
            objSqlComm1.CommandType = CommandType.StoredProcedure;
            objSqlComm1.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            objSqlComm1.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            objDR = objSqlComm1.ExecuteReader();
            szReplaceString = ReplaceAll(szReplaceString, objDR);
            return szReplaceString;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally
        {
            if (objSqlConn.State == ConnectionState.Open)
            {
                objSqlConn.Close();
            }
        }
        return null;
    }


    public string ReplaceAll(string HtmString, SqlDataReader rs)
    {
        try
        {
            log.Debug("Bill_Sys_PatientInfoPDF. Method - ReplaceAll : ");
            if (rs.Read())
            {
                ArrayList objAL = new ArrayList();
                Bill_Sys_KeyValue objKeyValue;
                Int32 count;
                objAL = ParseXML(ConfigurationSettings.AppSettings["PATIENT_INFO_XML"]);

                for (count = 0; count <= objAL.Count - 1; count++)
                {
                    objKeyValue = (Bill_Sys_KeyValue)objAL[count];
                    HtmString = HtmString.Replace(objKeyValue.Key.ToString(), rs[objKeyValue.Value].ToString());
                }
            }
            return HtmString;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        return null;
    }

    #region "Generate NF2 sent mails"
    public string getNF2MailDetails(string p_htmlstring, string p_szCompanyID)
    {
        string szReplaceString = "";
        SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        objSqlConn.Open();
        try
        {
            log.Debug("Bill_Sys_NF3Service. Method - getReplacedString : ");

            szReplaceString = p_htmlstring;
            SqlCommand objSqlComm1 = new SqlCommand("SP_NF2_MAILS_DETAILS", objSqlConn);
            objSqlComm1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader objDR;
            objSqlComm1.CommandType = CommandType.StoredProcedure;
            objSqlComm1.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            objDR = objSqlComm1.ExecuteReader();
            szReplaceString = ReplaceNF2MailDetails(szReplaceString, objDR);
            return szReplaceString;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);return null;
        }
        finally
        {
            if (objSqlConn.State == ConnectionState.Open)
            {
                objSqlConn.Close();
            }
        }
        return null;
    }

    public string ReplaceNF2MailDetails(string HtmString, SqlDataReader rs)
    {
        try
        {
            log.Debug("Bill_Sys_PatientInfoPDF. Method - ReplaceAll : ");
            if (rs.Read())
            {
                ArrayList objAL = new ArrayList();
                Bill_Sys_KeyValue objKeyValue;
                Int32 count;
                objAL = ParseXML(ConfigurationSettings.AppSettings["NF2_SENT_MAIL_XML"]);

                for (count = 0; count <= objAL.Count - 1; count++)
                {
                    objKeyValue = (Bill_Sys_KeyValue)objAL[count];
                    HtmString = HtmString.Replace(objKeyValue.Key.ToString(), rs[objKeyValue.Value].ToString());
                }
            }
            return HtmString;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        return null;
    }
    
    
   public string getPrintPOM(string p_htmlstring, string p_szProviderID)
    {
        SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        objSqlConn.Open();
        try
        {
            log.Debug("PRINT POM. Method - print POM: ");
            string szReplaceString = "";
            szReplaceString = p_htmlstring;
            SqlCommand objSqlComm1 = new SqlCommand("SP_PROVIDER_DETAILS", objSqlConn);
            objSqlComm1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader objDR;
            objSqlComm1.CommandType = CommandType.StoredProcedure;
            objSqlComm1.Parameters.AddWithValue("@SZ_OFFICE_ID", p_szProviderID);
            objDR = objSqlComm1.ExecuteReader();
            szReplaceString = ReplaceNF2MailDetails(szReplaceString, objDR);
            return szReplaceString;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally
        {
            if (objSqlConn.State == ConnectionState.Open)
            {
                objSqlConn.Close();
            }
        }
        return null;
    }
#endregion


    public DataSet getPatientSummaryReport(string p_szCompanyID)
    {
        DataSet objDS = new DataSet();
        string sz_version = "";
        SqlDataReader dr;
        SqlConnection sqlCon = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_PATIENT_SUMMARY_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            SqlDataAdapter objDA = new SqlDataAdapter(sqlCmd);
            objDA.Fill(objDS);

        }
        catch (SqlException ex)
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
        return objDS;
    }


    public DataSet getPatientSummaryReport(string p_szCompanyID,string p_LocationID)
    {
        DataSet objDS = new DataSet();
        string sz_version = "";
        SqlDataReader dr;
        SqlConnection sqlCon = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_PATIENT_SUMMARY_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", p_LocationID);
            SqlDataAdapter objDA = new SqlDataAdapter(sqlCmd);
            objDA.Fill(objDS);

        }
        catch (SqlException ex)
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
        return objDS;
    }
}
