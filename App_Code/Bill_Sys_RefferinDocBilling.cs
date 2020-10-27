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
using System.Data.SqlClient;

/// <summary>
/// Summary description for Bill_Sys_RefferinDocBilling
/// </summary>
public class Bill_Sys_RefferinDocBilling
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

	public Bill_Sys_RefferinDocBilling()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public string GetRefDocID(string sz_Company_Id, string sz_UserID)
    {
        string StatusID = "";

        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_GET_REF_OFF_ID", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_USER_ID", sz_UserID);
            comm.Parameters.AddWithValue("@SZ_BILLING_COMPANY", sz_Company_Id);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                StatusID = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); dr.Close(); }
        return StatusID;
    }

    public string CheckAssihnNo(string companyId, string assignno)
    {
        //SqlParameter sqlParam = new SqlParameter();
        conn = new SqlConnection(strConn);
        string _return = "";
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_CHECK_ASSIGN_NUMBER", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            comm.Parameters.AddWithValue("@SZ_ASSIGN_NUMBER", assignno);
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                _return = ds.Tables[0].Rows[0][0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return _return;
    }

    public int InsertDoctorMaster(string sz_Doctor_Name, string sz_Assign_no, string sz_Doc_Lic_No, string sz_Off_ID, string sz_Proc_id, string sz_Company_ID, int bt_reffering,string sz_npi)
    {
        int i = 0;
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_ADD_MST_DOC_REF", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_DOCTOR_NAME", sz_Doctor_Name);
            comm.Parameters.AddWithValue("@SZ_ASSIGN_NUMBER", sz_Assign_no);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_LICENSE_NUMBER", sz_Doc_Lic_No);
            comm.Parameters.AddWithValue("@SZ_OFFICE_ID", sz_Off_ID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", sz_Proc_id);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            comm.Parameters.AddWithValue("@BT_REFFERING", bt_reffering);
            comm.Parameters.AddWithValue("@SZ_NPI", sz_npi);
            i = comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            i = 0;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return i;
    }

    public int UpdateDoctorMaster(string sz_Doctor_Name, string sz_Assign_no, string sz_Doc_Lic_No, string sz_Off_ID, string sz_Proc_id, string sz_Company_ID, string sz_Doc_Id,string sz_npi)
    {
        int i = 0;
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_UPDATE_MST_DOC", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_DOCTOR_NAME", sz_Doctor_Name);
            comm.Parameters.AddWithValue("@SZ_ASSIGN_NUMBER", sz_Assign_no);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_LICENSE_NUMBER", sz_Doc_Lic_No);
            comm.Parameters.AddWithValue("@SZ_OFFICE_ID", sz_Off_ID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", sz_Proc_id);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_Doc_Id);
            comm.Parameters.AddWithValue("@SZ_NPI", sz_npi);
            i = comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            i = 0;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return i;
    }

    public DataSet getReffDocInformation(string sz_doctor_Id)
    {
        conn = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_get_billing_reff_doc_info", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_doctor_id", sz_doctor_Id);
            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds; 
    }

}