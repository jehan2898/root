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
/// Summary description for Bill_Sys_Adjuster
/// </summary>
public class Bill_Sys_Adjuster
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;
	public Bill_Sys_Adjuster()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
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
        catch (Exception ex)
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

    public string getAdjusterID(string companyId, string caseID)
    {
        string szReturnID = "";
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
        catch (Exception ex)
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

        catch (Exception ex)
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

    public string getLatestID(string szSPName, string szCompanyID)
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
        return szReturnID;
    }
}