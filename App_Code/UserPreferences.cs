using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;

/// <summary>
/// Summary description for UserPreferences
/// </summary>
public class UserPreferences
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;
	public UserPreferences()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet GetUserPreferences(string szUserid, string szPageName)
    {
        DataSet objDS = new DataSet();
       
     
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_user_preferences", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_page_name", szPageName);
            sqlCmd.Parameters.AddWithValue("@sz_user_id", szUserid);
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

    public void save_Delivery_report(string pagename, string userid, string companyid, string preferences)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_add_user_preferences", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Parameters.AddWithValue("@sz_page_name", pagename);
            sqlCmd.Parameters.AddWithValue("@sz_user_id", userid);
            sqlCmd.Parameters.AddWithValue("@sz_company_id", companyid);
            sqlCmd.Parameters.AddWithValue("@sz_preferences", preferences);
            
            sqlCmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }

        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
    }

    public void UP_Remove(string userid, string companyid, string pagename)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_up_remove_user_preference", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", companyid);
            sqlCmd.Parameters.AddWithValue("@sz_user_id", userid);
            sqlCmd.Parameters.AddWithValue("@sz_page_name", pagename);
            sqlCmd.ExecuteNonQuery();
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

    }

}