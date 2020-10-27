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



public class Bill_Sys_MemoBO
{
   
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public Bill_Sys_MemoBO()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public void SaveMemo(ArrayList p_objArrayList)
    {
        
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_MEMO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objArrayList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_objArrayList[1].ToString());
         
            sqlCmd.Parameters.AddWithValue("@SZ_MEMO", p_objArrayList[2].ToString());
           
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
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

    
    public string GET_MEMO(string sz_company_id, string sz_case_Id)
    {
        //SqlParameter sqlParam = new SqlParameter();
        string memo = "";
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_MEMO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                memo=dr[0].ToString();
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
        return memo;
        
    }

    
}
