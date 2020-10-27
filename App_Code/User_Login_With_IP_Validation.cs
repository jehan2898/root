using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using log4net;
using System.Configuration;
using System.Collections;

/// <summary>
/// Summary description for User_Login_With_IP_Validation
/// </summary>
public class User_Login_With_IP_Validation
{
    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;
    Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    SqlDataReader dr;
    private static ILog log = LogManager.GetLogger("User_Login_With_IP_Validation");

	public User_Login_With_IP_Validation()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public DataSet getCompanyWise_Users(String p_szCompanyID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            comm = new SqlCommand("SP_GET_USERS_WITH_IP_ADDRESS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }
    public DataSet GetValidateIP(string sz_Company_ID, string SZ_USER_ID, string sz_ip_address)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            comm = new SqlCommand("SP_GET_VALIDATED_USERS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            //string squery = "select * from tbl_insert_ip_address where sz_company_id='" + sz_Company_ID + "' and   sz_ip_address='" + sz_ip_address + "'";
            //comm = new SqlCommand(squery,conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            comm.Parameters.AddWithValue("@SZ_USER_ID", SZ_USER_ID);
            comm.Parameters.AddWithValue("@sz_ip_address", sz_ip_address);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }
    public string save_IP_With_Multiple(ArrayList p_objArrayList,string sz_company_id,string sz_ip_address)
    {
        log.Debug("In save_IP_With_Multiple");
        SqlTransaction tr;
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        comm.CommandTimeout = 0;
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        tr = conn.BeginTransaction();
        ArrayList arr_Validate_IP = new ArrayList();
        string str_success="0";
        Bill_Sys_IP_Objects obj = new Bill_Sys_IP_Objects();
        
        try
        {
            //Bill_Sys_IP_Objects objBill = (Bill_Sys_IP_Objects)p_objArrayList[0];
            log.Debug("Before SP_SAVE_IP_WITH_MULTIPLE_USERS(Delete)");
            SqlCommand cmd = new SqlCommand("SP_SAVE_IP_WITH_MULTIPLE_USERS", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            log.Debug("SqlCommand cmd");
            cmd.CommandTimeout = 0;
            log.Debug("cmd.CommandTimeout = 0;");
            cmd.Transaction = tr;
            log.Debug("cmd.Transaction = tr;");
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            log.Debug("sz_company_id " + sz_company_id);
            cmd.Parameters.AddWithValue("@SZ_IP_ADDRESS", sz_ip_address);
            log.Debug("sz_ip_address " + sz_ip_address);

            cmd.Parameters.AddWithValue("@FLAG", "DELETE");
            log.Debug("cmd.Parameters.AddWithValue(@FLAG, DELETE)");
            cmd.CommandType = CommandType.StoredProcedure;
            log.Debug("cmd.CommandType");
            cmd.ExecuteNonQuery();
            log.Debug("cmd.ExecuteNonQuery");
            //string squery = "delete from tbl_insert_ip_address where sz_company_id='" + sz_company_id + "' and   sz_ip_address='" + sz_ip_address + "'";
            //SqlCommand cmd = new SqlCommand(squery, conn);

            //cmd.CommandTimeout = 0;
            //cmd.Transaction = tr;
            //cmd.ExecuteNonQuery();

            for (int i = 0; i < p_objArrayList.Count; i++)
            {
                log.Debug("In the for loop");
                obj = (Bill_Sys_IP_Objects)p_objArrayList[i];
                DataSet ds = new DataSet();

                //string squery_insert = "insert into tbl_insert_ip_address (sz_company_id,sz_ip_address,sz_user_id,dt_created) values ('" + obj.SZ_COMPANY_ID + "','" + obj.SZ_IP_ADDRESS + "','" + obj.SZ_USER_ID + "','"+DateTime.Now.ToString()+"')";
                //cmd = new SqlCommand(squery_insert, conn);
                cmd = new SqlCommand("SP_SAVE_IP_WITH_MULTIPLE_USERS", conn);
                cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                log.Debug("cmd");
                cmd.CommandTimeout = 0;
                log.Debug("cmd.CommandTimeout = 0;");
                cmd.Transaction = tr;
                log.Debug("cmd.Transaction = tr;");
                log.Debug("obj.SZ_USER_ID " + obj.SZ_USER_ID);
                log.Debug("obj.SZ_COMPANY_ID " + obj.SZ_COMPANY_ID);
                log.Debug("obj.SZ_IP_ADDRESS" + obj.SZ_IP_ADDRESS);
                cmd.Parameters.AddWithValue("@SZ_USER_ID", obj.SZ_USER_ID);
                cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.SZ_COMPANY_ID);
                cmd.Parameters.AddWithValue("@SZ_IP_ADDRESS", obj.SZ_IP_ADDRESS);

                cmd.Parameters.AddWithValue("@FLAG", "ADD");
                cmd.CommandType = CommandType.StoredProcedure;
                log.Debug("cmd.CommandType");
                cmd.ExecuteNonQuery();
                log.Debug("cmd.ExecuteNonQuery");
                str_success = "1";
                log.Debug("str_success : " + str_success);
                
            }
            log.Debug("Before tr.Commit();");
            tr.Commit();
            log.Debug("After tr.Commit();");
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            tr.Rollback();
        }
        finally
        {
            conn.Close();
        }
        obj.SZ_PRINT_SUCCESS_MESSAGE = str_success;
        //obj.SZ_PRINT_ERROR_MESSAGE = str_error;
        arr_Validate_IP.Add(obj);
        return str_success;
    }
}
[Serializable]
public class Bill_Sys_IP_Objects
{
    private string _SZ_COMPANY_ID;
    public string SZ_COMPANY_ID
    {
        get
        {
            return _SZ_COMPANY_ID;
        }

        set
        {
            _SZ_COMPANY_ID = value;
        }
    }
    private string _SZ_USER_ID;
    public string SZ_USER_ID
    {
        get
        {
            return _SZ_USER_ID;
        }

        set
        {
            _SZ_USER_ID = value;
        }
    }
    private string _SZ_USER_NAME;
    public string SZ_USER_NAME
    {
        get
        {
            return _SZ_USER_NAME;
        }

        set
        {
            _SZ_USER_NAME = value;
        }
    }
    private string _SZ_IP_ADDRESS;
    public string SZ_IP_ADDRESS
    {
        get
        {
            return _SZ_IP_ADDRESS;
        }

        set
        {
            _SZ_IP_ADDRESS = value;
        }
    }
    private string _SZ_PRINT_SUCCESS_MESSAGE;
    public string SZ_PRINT_SUCCESS_MESSAGE
    {
        get
        {
            return _SZ_PRINT_SUCCESS_MESSAGE;
        }

        set
        {
            _SZ_PRINT_SUCCESS_MESSAGE = value;
        }
    }
    private string _SZ_PRINT_ERROR_MESSAGE;
    public string SZ_PRINT_ERROR_MESSAGE
    {
        get
        {
            return _SZ_PRINT_ERROR_MESSAGE;
        }

        set
        {
            _SZ_PRINT_ERROR_MESSAGE = value;
        }
    }
}