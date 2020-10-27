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

/// <summary>
/// Summary description for Bill_Sys_NF3_Template
/// </summary>
public class PopupBO
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

    public PopupBO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public void savePatientInformation(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PATIENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@I_PATIENT_AGE", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ADDRESS", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_STREET", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_CITY", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ZIP", objAL[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_PHONE", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_EMAIL", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[9].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_WORK_PHONE", objAL[10].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_WORK_PHONE_EXTENSION", objAL[11].ToString());
            sqlCmd.Parameters.AddWithValue("@MI", objAL[12].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_WCB_NO", objAL[13].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_SOCIAL_SECURITY_NO", objAL[14].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_DOB", objAL[15].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_GENDER", objAL[16].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_INJURY", objAL[17].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_JOB_TITLE", objAL[18].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_WORK_ACTIVITIES", objAL[19].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_STATE", objAL[20].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CARRIER_CASE_NO", objAL[21].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_NAME", objAL[22].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_PHONE", objAL[23].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_ADDRESS", objAL[24].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_CITY", objAL[25].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_STATE", objAL[26].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_ZIP", objAL[27].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[28].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", objAL[29].ToString());
            sqlCmd.Parameters.AddWithValue("@BT_WRONG_PHONE", objAL[30].ToString());
            sqlCmd.Parameters.AddWithValue("@BT_TRANSPORTATION", objAL[31].ToString());
            
            sqlCmd.ExecuteNonQuery();

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
    }

    public void savePatientAccidentInformation(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_ACCIDENT_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_INFO_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PLATE_NO", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_ACCIDENT_DATE", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_ADDRESS", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_CITY", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACCIDENT_STATE", objAL[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REPORT_NO", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_FROM_CAR", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[9].ToString());                   
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[10].ToString());
            

            sqlCmd.ExecuteNonQuery();

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
    }

    public void saveAttorney(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_ATTORNEY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_FIRST_NAME", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_LAST_NAME", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_CITY", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_STATE", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_ZIP", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_PHONE", objAL[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_FAX", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_EMAIL", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[9].ToString());
            if (objAL[10].ToString() != "NA" )
                sqlCmd.Parameters.AddWithValue("@SZ_ATTORNEY_STATE_ID", objAL[10].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();
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
    }

    //Nirmalkumar
    public void saveAdjuster(ArrayList objAL)
    {
        
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction transaction;
        transaction = sqlCon.BeginTransaction();

        try
        {
           
            sqlCmd = new SqlCommand("SP_MST_ADJUSTER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_NAME", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ADDRESS", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CITY", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_STATE", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ZIP", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PHONE", objAL[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EXTENSION", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FAX", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMAIL", objAL[9].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[10].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[11].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();
            transaction.Commit();
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
                
            }
        }
    }

    public string getAdjusterID(string companyId,string caseID)
    {
        string szReturnID="";
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        
        try
        {
            sqlCmd = new SqlCommand("SP_GET_ADJUSTER_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            
            SqlDataReader objDR = sqlCmd.ExecuteReader();
            
            while (objDR.Read())
            {
                szReturnID = objDR["SZ_ADJUSTER_ID"].ToString();
            }
        }
        catch(SqlException ex)
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
        return szReturnID;
            
    }

    public void updateAdjuster(ArrayList objAL)
    {
       
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction transaction;
        transaction = sqlCon.BeginTransaction();
        try
        {
            
            sqlCmd = new SqlCommand("SP_MST_ADJUSTER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_NAME", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ADDRESS", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CITY", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_STATE", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ZIP", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PHONE", objAL[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EXTENSION", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FAX", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMAIL", objAL[9].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[10].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[11].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
            sqlCmd.ExecuteNonQuery();
            transaction.Commit();
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
    }

    public void updateCaseMaster(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);
        sqlCon.Open();
        SqlTransaction transaction;
        transaction = sqlCon.BeginTransaction();
        
        try
        {
            
            sqlCmd = new SqlCommand("SP_MST_ADJUSTER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_ID", objAL[0].ToString());
            //sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_NAME", objAL[1].ToString());
            //sqlCmd.Parameters.AddWithValue("@SZ_ADDRESS", objAL[2].ToString());
            //sqlCmd.Parameters.AddWithValue("@SZ_CITY", objAL[3].ToString());
            //sqlCmd.Parameters.AddWithValue("@SZ_STATE", objAL[4].ToString());
            //sqlCmd.Parameters.AddWithValue("@SZ_ZIP", objAL[5].ToString());
            //sqlCmd.Parameters.AddWithValue("@SZ_PHONE", objAL[6].ToString());
            //sqlCmd.Parameters.AddWithValue("@SZ_EXTENSION", objAL[7].ToString());
            //sqlCmd.Parameters.AddWithValue("@SZ_FAX", objAL[8].ToString());
            //sqlCmd.Parameters.AddWithValue("@SZ_EMAIL", objAL[9].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[10].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[11].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE_CASE");
            sqlCmd.ExecuteNonQuery();
            transaction.Commit();
           
        }
       
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
    }
    //END
    
    public void saveInsurance(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_INSURANCE_COMPANY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_NAME", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_PHONE", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_EMAIL", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY_CODE", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();
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
    }

    public void saveInsuranceAddress(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_INSURANCE_ADDRESS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ADDRESS", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_STREET", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_CITY", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_STATE", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ZIP", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[6].ToString());
            if (objAL[7].ToString() != "NA" )
                  sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_STATE_ID", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();
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
    }

    public string getLatestID(string szSPName,string szCompanyID)
    {
        string szReturnID = "";
        sqlCon = new SqlConnection(strConn);
        SqlDataReader objDR;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(szSPName, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LATEST_ID");
            objDR = sqlCmd.ExecuteReader();
            while (objDR.Read())
            {
                szReturnID = objDR["ID"].ToString();
            }
            
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
        return szReturnID;
    }


    public string GetCompanyID(string strPatientID)
    {
        string szReturnID = "";
        sqlCon = new SqlConnection(strConn);
        SqlDataReader objDR;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_COMPANY_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", strPatientID);        
            objDR = sqlCmd.ExecuteReader();
            while (objDR.Read())
            {
                szReturnID = objDR["SZ_COMPANY_ID"].ToString();
            }

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
        return szReturnID;
    }
    /*vivek*/
    public void saveInsuranceAddressNew(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_INSURANCE_ADDRESS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ADDRESS", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_STREET", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_CITY", objAL[3].ToString());
           // sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_STATE", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ZIP", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[6].ToString());
                        if (objAL[4].ToString() != "NA")
                sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_STATE_ID", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@I_DEFAULT",objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();
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
    }

    public DataSet InsuranceAddress(string szinsuranceid)
    {
        sqlCon = new SqlConnection(strConn);
        DataSet dsaddress = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_INSURANCE_ADDRESS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", szinsuranceid);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
            sqlda = new SqlDataAdapter(sqlCmd);           
            sqlda.Fill(dsaddress);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        return dsaddress;
    
    }

    public void saveInsuranceAddressCaseDetails(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_INSURANCE_ADDRESS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ADDRESS", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_STREET", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_CITY", objAL[3].ToString());
            // sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_STATE", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ZIP", objAL[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[6].ToString());
            if (objAL[7].ToString() != "NA")
                sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_STATE_ID", objAL[7].ToString());
            sqlCmd.Parameters.AddWithValue("@I_DEFAULT", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();
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
    }

   
}
