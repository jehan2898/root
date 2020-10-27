using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for IntakeSheetProviderBO
/// </summary>
public class IntakeSheetProviderBO
{
	public IntakeSheetProviderBO()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataSet select_intake_sheet_provider(IntakeSheetProviderDAO o_dao)
    {
        String strConn;
        SqlConnection conn1;
        SqlCommand comm;
        SqlDataAdapter sqlda;
        DataSet ds = new DataSet();
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        conn1 = new SqlConnection(strConn);
        try
        {
            conn1.Open();
            // comm = new SqlCommand("sp_select_intake_document", conn1);
            comm = new SqlCommand("SELECT mst_intake_provider.i_id, mst_intake_provider.sz_company_id, mst_intake_provider.sz_name, mst_intake_provider.sz_address, mst_intake_provider.sz_city,mst_state.sz_state_name,mst_intake_provider.sz_zip,mst_intake_provider.sz_phone,mst_intake_provider.sz_email, mst_users.sz_user_name, mst_billing_company.sz_company_name FROM   mst_intake_provider JOIN mst_state ON mst_intake_provider.i_state_id = mst_state.i_state_id JOIN mst_billing_company ON mst_intake_provider.sz_company_id = mst_billing_company.sz_company_id  JOIN mst_users On mst_intake_provider.sz_created_by = mst_users.sz_user_id  WHERE  mst_intake_provider.sz_company_id='" + o_dao.sz_company_id + "'", conn1);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.Text;
            //comm.Parameters.AddWithValue("@sz_company_id", o_dao.sz_company_id);
            //comm.Parameters.AddWithValue("@sz_case_type_id", o_dao.sz_case_type_id);

            sqlda = new SqlDataAdapter(comm);
           
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn1.Close(); }
        return ds;
    }

    public DataSet select_intake_sheet_provider_for_update(IntakeSheetProviderDAO o_dao)
    {
        String strConn;
        SqlConnection conn1;
        SqlCommand comm;
        SqlDataAdapter sqlda;
        DataSet ds = new DataSet();
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        conn1 = new SqlConnection(strConn);
        try
        {
            conn1.Open();
            //comm = new SqlCommand("sp_select_intake_document", conn1);
            comm = new SqlCommand("SELECT mst_intake_provider.i_id,mst_state.i_state_id,mst_intake_provider.sz_company_id, mst_intake_provider.sz_name, mst_intake_provider.sz_address, mst_intake_provider.sz_city,mst_intake_provider.sz_zip,mst_intake_provider.sz_phone,mst_intake_provider.sz_email,mst_state.sz_state_name, mst_users.sz_user_name, mst_billing_company.sz_company_name FROM   mst_intake_provider JOIN mst_state ON mst_intake_provider.i_state_id = mst_state.i_state_id JOIN mst_billing_company ON mst_intake_provider.sz_company_id = mst_billing_company.sz_company_id JOIN mst_users On mst_intake_provider.sz_created_by = mst_users.sz_user_id where mst_intake_provider.i_id=" + o_dao.i_id + "", conn1);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.Text;
            //comm.Parameters.AddWithValue("@sz_company_id", o_dao.sz_company_id);
            //comm.Parameters.AddWithValue("@sz_case_type_id", o_dao.sz_case_type_id);

            sqlda = new SqlDataAdapter(comm);
            
            sqlda.Fill(ds);
  
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn1.Close(); }
        return ds;
    }

    public void update_intake_sheet_provider(IntakeSheetProviderDAO o_dao)
    {
        String strConn;
        SqlConnection sqlcon;
        SqlCommand sqlCmd;

        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlcon = new SqlConnection(strConn);

        try
        {
            sqlcon.Open();
            sqlCmd = new SqlCommand("sp_update_intake_provider", sqlcon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@i_id", o_dao.i_id);
            sqlCmd.Parameters.AddWithValue("@sz_company_id", o_dao.sz_company_id);
            sqlCmd.Parameters.AddWithValue("@sz_name", o_dao.sz_name);
            sqlCmd.Parameters.AddWithValue("@sz_address", o_dao.sz_address);
            sqlCmd.Parameters.AddWithValue("@sz_city", o_dao.sz_city);
            sqlCmd.Parameters.AddWithValue("@i_state_id", o_dao.i_state_id);
            sqlCmd.Parameters.AddWithValue("@sz_zip", o_dao.sz_zip);
            sqlCmd.Parameters.AddWithValue("@sz_phone", o_dao.sz_phone);
            sqlCmd.Parameters.AddWithValue("@sz_email", o_dao.sz_email);
            sqlCmd.Parameters.AddWithValue("@sz_created_by", o_dao.i_user_id);
            sqlCmd.Parameters.AddWithValue("@sz_updated_by", o_dao.i_user_id);

            sqlCmd.ExecuteNonQuery();

        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlcon.State == ConnectionState.Open)
            {
                sqlcon.Close();
            }
        }

    }

    public void delete_intake_document(IntakeSheetProviderDAO o_dao)
    {
        String strConn;
        SqlConnection sqlcon;
        SqlCommand sqlCmd;

        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlcon = new SqlConnection(strConn);

        try
        {
            sqlcon.Open();
            //sqlCmd = new SqlCommand("sp_delete_intake_document", sqlcon);
            // sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd = new SqlCommand(" delete from mst_intake_provider where mst_intake_provider.sz_company_id ='" + o_dao.sz_company_id + "'and mst_intake_provider.i_id=" + o_dao.i_id + "", sqlcon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@i_id", o_dao.i_id);
            sqlCmd.Parameters.AddWithValue("@sz_company_id", o_dao.sz_company_id);
            //sqlCmd.Parameters.AddWithValue("@sz_case_type_id", o_dao.sz_case_type_id);
            //sqlCmd.Parameters.AddWithValue("@sz_name", o_dao.sz_name);
            //sqlCmd.Parameters.AddWithValue("@sz_created_by", o_dao.i_user_id);
            sqlCmd.ExecuteNonQuery();

        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlcon.State == ConnectionState.Open)
            {
                sqlcon.Close();
            }
        }

    }
}