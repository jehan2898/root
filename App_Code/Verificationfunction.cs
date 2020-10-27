using System;
using System.Collections.Generic;

using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
/// <summary>
/// Summary description for Verificationfunction
/// </summary>
public class Verificationfunction
{

    string strsqlcon;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
	public Verificationfunction()
	{
        strsqlcon = ConfigurationSettings.AppSettings["Connection_String"].ToString();
	}



    public void Save_Reexamination(string reasonId, string companyId, string reason,string sz_created,string sz_modified )
    {
          conn = new SqlConnection(strsqlcon);
         
        try
        {

            {
                conn.Open();
                comm = new SqlCommand("sp_add_mst_verification_reason ", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Parameters.AddWithValue("@i_reason_id",reasonId);
                comm.Parameters.AddWithValue("@sz_reason", reason);
                comm.Parameters.AddWithValue("@sz_company_id", companyId);
                comm.Parameters.AddWithValue("@sz_created",sz_created);
                comm.Parameters.AddWithValue("@sz_modified",sz_modified);           
               
                comm.ExecuteNonQuery();

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
    }





    public void Update_Verification(string reasonId, string companyId, string reason, string sz_modified,string sz_created)
    {
        conn = new SqlConnection(strsqlcon);

        try
        {

            {
                conn.Open();
                comm = new SqlCommand("sp_add_mst_verification_reason ", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Parameters.AddWithValue("@i_reason_id", reasonId);
                comm.Parameters.AddWithValue("@sz_reason", reason);
                comm.Parameters.AddWithValue("@sz_company_id", companyId);
                comm.Parameters.AddWithValue("@sz_created",sz_created);
                comm.Parameters.AddWithValue("@sz_modified",sz_modified);

                comm.ExecuteNonQuery();

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
    }

      public void delete_Verification(string i_reason_id , string sz_company_id )
    {
        conn = new SqlConnection(strsqlcon);
     
          
          try
          {
            conn.Open();
            comm = new SqlCommand("sp_del_mst_verification_reason", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@i_reason_id", i_reason_id);
            comm.Parameters.AddWithValue("@sz_company_id",sz_company_id);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

    }


    public DataSet Loadverification(string sCompanyID)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter adap = null;
        conn = new SqlConnection(strsqlcon);
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_retrieve_mst_verification_reason1 ", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@sz_company_id", sCompanyID);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adap = new SqlDataAdapter(comm);
            adap.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return ds;
    }


    public DataSet Loadverificationforddl(string sCompanyID)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter adap = null;
        conn = new SqlConnection(strsqlcon);
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_verification_reason ", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@sz_company_id", sCompanyID);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adap = new SqlDataAdapter(comm);
            adap.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return ds;
    }
}