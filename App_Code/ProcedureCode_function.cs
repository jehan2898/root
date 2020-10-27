using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using System.Configuration;

public class ProcedureCode_function
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
	public ProcedureCode_function()
	{		
        strsqlCon = ConfigurationSettings.AppSettings["Connection_String"].ToString();
	}


    public void Save_Update_ProcedureCode(string szCode, string szProcedureGroupID, string szDescription, string szamount, int iVisitType, string szModifier, string szCompanyID, string szProcedureID, string szFlag, string szAddToPreferred, string szRevCode, string szValueCode, string szProcedureLongDesc, string szModifierDesc, string szRVU)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE", szCode);
            sqlCmd.Parameters.AddWithValue("@SZ_CODE_DESCRIPTION", szDescription);
            double fltAmt = Convert.ToDouble(szamount);
            sqlCmd.Parameters.AddWithValue("@FLT_AMOUNT", fltAmt);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szProcedureGroupID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_MODIFIER", szModifier);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", szProcedureID);
            sqlCmd.Parameters.AddWithValue("@I_VISIT_TYPE", iVisitType);
            sqlCmd.Parameters.AddWithValue("@BT_ADD_TO_PREFERRED_LIST", szAddToPreferred);
            sqlCmd.Parameters.AddWithValue("@SZ_REV_CODE", szRevCode);
            sqlCmd.Parameters.AddWithValue("@SZ_VALUE_CODE", szValueCode);
            sqlCmd.Parameters.AddWithValue("@SZ_CODE_LONG_DESC", szProcedureLongDesc);
            sqlCmd.Parameters.AddWithValue("@SZ_MODIFIER_LONG_DESC", szModifierDesc);
            sqlCmd.Parameters.AddWithValue("@SZ_RVU", szRVU);
            sqlCmd.Parameters.AddWithValue("@FLAG", szFlag);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }
       
    public DataSet loadProcedureCode(string sCompanyID)  
    
    {
        DataSet ds = new DataSet();
        SqlDataAdapter adap = null;
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_MST_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sCompanyID);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            adap = new SqlDataAdapter(sqlCmd);
            adap.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return ds;
    }


    }



