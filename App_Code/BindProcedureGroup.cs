using System;
using System.Collections.Generic;

using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

/// <summary>
/// Summary description for BindProcedureGroup
/// </summary>
public class BindProcedureGroup
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

	public BindProcedureGroup()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public DataSet Search_GroupProcedureCodes(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_BIND_PROCEDURE_GROUP", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[2].ToString());

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
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
        return ds;
    }
    public DataSet Get_Procedure_Group_Details(string sz_ProcedureGroupName,string sz_companyID,string sz_Procedure_Group_ID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PROCEDURE_GROUP_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_GROUP_NAME", sz_ProcedureGroupName);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", sz_Procedure_Group_ID);
           

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
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
        return ds;
    }
    public void DELETE_PROCEDURE_CODES(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DELETE_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_GROUP_NAME", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[4].ToString());


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