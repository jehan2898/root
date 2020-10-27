using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Text;
using System.Data.SqlClient;

 
public class Bill_Sys_MissingInformationDAO
{

    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataTable  dt;

    public Bill_Sys_MissingInformationDAO()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataTable GET_MISSING_INSURANCE(string sz_Company_Id, int sz_Start_Index, int sz_End_Index)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        dt = new DataTable();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_MISSING_INSURANCE_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_Id);
            sqlCmd.Parameters.AddWithValue("@I_START_INDEX", sz_Start_Index);
            sqlCmd.Parameters.AddWithValue("@SZ_ORDER_BY", "");
            sqlCmd.Parameters.AddWithValue("@I_END_INDEX", sz_End_Index);
            sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", "");
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(dt);
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
        return dt;
    }

    public DataTable GET_MISSING_REPORT_NUMBER(string sz_Company_Id, int sz_Start_Index, int sz_End_Index)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        dt = new DataTable();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_missing_information_report_number_dev_temp", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_Id);
            sqlCmd.Parameters.AddWithValue("@I_START_INDEX", sz_Start_Index);
            sqlCmd.Parameters.AddWithValue("@SZ_ORDER_BY", "");
            sqlCmd.Parameters.AddWithValue("@I_END_INDEX", sz_End_Index);
            sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", "");
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(dt);
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
        return dt;
    }

    public DataTable GET_MISSING_ATTORNEY(string sz_Company_Id, int sz_Start_Index, int sz_End_Index)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        dt = new DataTable();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_MISSING_ATTORNEY_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_Id);
            sqlCmd.Parameters.AddWithValue("@I_START_INDEX", sz_Start_Index);
            sqlCmd.Parameters.AddWithValue("@SZ_ORDER_BY", "");
            sqlCmd.Parameters.AddWithValue("@I_END_INDEX", sz_End_Index);
            sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", "");
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(dt);
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
        return dt;
    }

    public DataTable GET_MISSING_POLICY_HOLDER(string sz_Company_Id, int sz_Start_Index, int sz_End_Index)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        dt = new DataTable();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_MISSING_POLICY_HOLDER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_Id);
            sqlCmd.Parameters.AddWithValue("@I_START_INDEX", sz_Start_Index);
            sqlCmd.Parameters.AddWithValue("@SZ_ORDER_BY", "");
            sqlCmd.Parameters.AddWithValue("@I_END_INDEX", sz_End_Index);
            sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", "");
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(dt);
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
        return dt;
    }

    public DataTable GET_MISSING_CLAIMNO(string sz_Company_Id, int sz_Start_Index, int sz_End_Index)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        dt = new DataTable();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_MISSING_CLAIM_NUMBER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_Id);
            sqlCmd.Parameters.AddWithValue("@I_START_INDEX", sz_Start_Index);
            sqlCmd.Parameters.AddWithValue("@SZ_ORDER_BY", "");
            sqlCmd.Parameters.AddWithValue("@I_END_INDEX", sz_End_Index);
            sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", "");
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(dt);
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
        return dt;
    }
}
