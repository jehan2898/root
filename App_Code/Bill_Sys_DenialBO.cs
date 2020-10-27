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

public class Bill_Sys_DenialBO
{
  

    String  strsqlCon;
    SqlConnection sqlCon;
    SqlCommand  sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public Bill_Sys_DenialBO()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public DataSet GET_Denial(string billNumber)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
         ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("DENIAL_DESK", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType  = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", billNumber);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST"); 
            //sqlCmd.ExecuteNonQuery();
            
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
            
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

    public void UpdateDenial(string billNumber, int denial, string denialRemark, DateTime denialDate,  string flag)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
       
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("DENIAL_DESK", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", billNumber);
            sqlCmd.Parameters.AddWithValue("@I_DENIEL", denial);
            sqlCmd.Parameters.AddWithValue("@SZ_DENIEL_REMARK", denialRemark);
            sqlCmd.Parameters.AddWithValue("@DT_DENIEL_DATE", denialDate);
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);
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

    public int UpdateDenialMaster(string DenialID,string UpdateReason,string CompanyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        int i=0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_denial_master_update", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@sz_denial_id", DenialID);
            sqlCmd.Parameters.AddWithValue("@sz_company_id", CompanyID);
            sqlCmd.Parameters.AddWithValue("@sz_denial_reason", UpdateReason.Trim());
            sqlCmd.Parameters.AddWithValue("@flag", "Update");
            i=sqlCmd.ExecuteNonQuery();
             return i;
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return i;
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
            
        }
    }

    public int AddDenialMaster(string UpdateReason,string CompanyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        int i=0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_denial_master_update", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@flag", "Add");
            sqlCmd.Parameters.AddWithValue("@sz_company_id", CompanyID);
            sqlCmd.Parameters.AddWithValue("@sz_denial_reason", UpdateReason.Trim());
            i=sqlCmd.ExecuteNonQuery();
             return i;
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return 0;
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }

        }
        return 0;
    }

    public int DeleteenialMaster(string DenialId, string CompanyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        int i = 0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DELETE_DENIAL_REASONS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;


            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            sqlCmd.Parameters.AddWithValue("@I_DENIAL_REASON_ID", DenialId);
            i = sqlCmd.ExecuteNonQuery();
            return i;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return 0;

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }

        }
        return 0;
    }

}
