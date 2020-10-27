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
public class PatientDataBO
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

    public PatientDataBO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

   public DataSet getPatientData(string p_szCompanyID)
    {
        DataSet objDS = new DataSet();
        string sz_version = "";
        SqlDataReader dr;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_DATA", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
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


    public DataSet getPatientBillingReport(string p_szCompanyID)
    {
        DataSet objDS = new DataSet();
        string sz_version = "";
        SqlDataReader dr;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_DATA", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "CALC");
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

    public DataSet getPatientAllData(string p_szWCBNO,string p_szCompanyID)
    {
        DataSet objDS = new DataSet();
        string sz_version = "";
        SqlDataReader dr;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_DATA", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@WCB_ID", p_szWCBNO);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ALL");
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


    public DataSet searchPatientData(string szDocketNo, string szClaimNo, string szInsuranceCompany, string szVenue, string szVenueID, string szDate,string p_szCompanyID)
    {
        DataSet objDS = new DataSet();
        string sz_version = "";
        SqlDataReader dr;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_DATA", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCKET_NO", szDocketNo);
            sqlCmd.Parameters.AddWithValue("@SZ_CLAIM_NO", szClaimNo);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURER", szInsuranceCompany);
            sqlCmd.Parameters.AddWithValue("@SZ_VENUE", szVenue);
            sqlCmd.Parameters.AddWithValue("@I_VENUE_ID", szVenueID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_DATE", szDate);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
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


    public DataSet getPatientBillingData(string p_szVenueID, string szDate)
    {
        DataSet objDS = new DataSet();
        string sz_version = "";
        SqlDataReader dr;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_DATA", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_VENUE_ID", p_szVenueID);
            sqlCmd.Parameters.AddWithValue("@SZ_DATE", szDate);
            sqlCmd.Parameters.AddWithValue("@FLAG", "SINGLE_REPORT");
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
    public string getPatientName(string CaseID)
    {
              
        string sz_patientName = "";
        SqlDataReader dr;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_NAME_USING_CASEID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", CaseID);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                sz_patientName = dr[0].ToString();
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
        return sz_patientName;
    }
}
