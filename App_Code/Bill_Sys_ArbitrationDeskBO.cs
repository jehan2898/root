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

public class Bill_Sys_ArbitrationDeskBO
{
  

    String  strsqlCon;
    SqlConnection sqlCon;
    SqlCommand  sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public Bill_Sys_ArbitrationDeskBO()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public DataSet GET_ARBITARATION_DESK(string companyId)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
         ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_ARBITARATION_DESK", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType  = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId); 
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


    public void UpdateSecondRequest(Boolean IS_SECOND_REQUEST, string SECOND_REQUEST_BILL,string billNumber, string companyId)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
       
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@BT_IS_SECOND_REQUEST", IS_SECOND_REQUEST);
             sqlCmd.Parameters.AddWithValue("@SZ_SECOND_REQUEST_BILL", SECOND_REQUEST_BILL);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", billNumber);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATESECONDREQUEST");
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
