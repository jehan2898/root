using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for Intake_Documents_BO
/// </summary>
public class Intake_Documents_BO
{
   
	public Intake_Documents_BO()
	{
       
	}

   public DataSet select_intake_document(Intake_Documents_DAO o_dao)
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
            comm = new SqlCommand("SELECT mst_intake_provider_document.i_id, mst_billing_company.sz_company_name,mst_intake_provider_document.sz_name,mst_users.sz_user_name, mst_case_type.sz_case_type_name,mst_intake_provider_document.sz_case_type_id FROM mst_intake_provider_document JOIN  mst_billing_company ON mst_intake_provider_document.sz_company_id = mst_billing_company.SZ_COMPANY_ID JOIN mst_case_type ON mst_intake_provider_document.sz_case_type_id = mst_case_type.SZ_CASE_TYPE_ID JOIN mst_users ON mst_intake_provider_document.sz_created_by=mst_users.sz_user_id  where mst_intake_provider_document.sz_company_id='" + o_dao.sz_company_id + "'", conn1);
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

    public DataSet select_intake_document_for_update(Intake_Documents_DAO o_dao)
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
            comm = new SqlCommand("SELECT mst_intake_provider_document.i_id,mst_billing_company.sz_company_id,mst_billing_company.sz_company_name,mst_intake_provider_document.sz_name,mst_users.sz_user_name, mst_case_type.sz_case_type_name,mst_intake_provider_document.sz_case_type_id FROM mst_intake_provider_document JOIN  mst_billing_company ON mst_intake_provider_document.sz_company_id = mst_billing_company.SZ_COMPANY_ID INNER JOIN mst_case_type ON mst_intake_provider_document.sz_case_type_id = mst_case_type.SZ_CASE_TYPE_ID  JOIN mst_users ON mst_intake_provider_document.sz_created_by=mst_users.sz_user_id where mst_intake_provider_document.i_id=" + o_dao.i_id + "", conn1);
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

    public void update_intake_document(Intake_Documents_DAO o_dao)
    {
        String strConn;
        SqlConnection sqlcon;
        SqlCommand sqlCmd;
      
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlcon = new SqlConnection(strConn);
        
        try
        {
            sqlcon.Open();
            sqlCmd = new SqlCommand("sp_update_intake_document", sqlcon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@i_id", o_dao.i_id);
            sqlCmd.Parameters.AddWithValue("@sz_company_id", o_dao.sz_company_id);
            sqlCmd.Parameters.AddWithValue("@sz_case_type_id", o_dao.sz_case_type_id);
            sqlCmd.Parameters.AddWithValue("@sz_name", o_dao.sz_name);
            sqlCmd.Parameters.AddWithValue("@sz_created_by", o_dao.i_user_id);
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

    public DataSet Show_intake_document(Intake_Documents_DAO o_dao)
    {   
        String strConn;
        SqlConnection sqlcon;
        SqlCommand sqlCmd;
        SqlDataAdapter sqlda;

        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlcon = new SqlConnection(strConn);

          DataSet ds  = new DataSet();
        try
        {
            sqlcon.Open();
            sqlCmd = new SqlCommand("sp_select_intake_document", sqlcon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

       
            sqlCmd.Parameters.AddWithValue("@sz_company_id", o_dao.sz_company_id);
            sqlCmd.Parameters.AddWithValue("@sz_case_type_id", o_dao.sz_case_type_id);

            sqlda = new SqlDataAdapter(sqlCmd);
            
            sqlda.Fill(ds);
          

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
        return ds;

    }

    public void delete_intake_document(Intake_Documents_DAO o_dao)
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
            sqlCmd = new SqlCommand(" delete from mst_intake_provider_document where mst_intake_provider_document.sz_company_id ='" + o_dao.sz_company_id + "'and mst_intake_provider_document.i_id=" + o_dao.i_id + "", sqlcon);
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

    public string save_intake_documnet(ArrayList arrList)
    {
        String strConn;
        SqlConnection sqlcon;
        SqlCommand sqlCmd;
        

        string sRetuen = "F";

        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlcon = new SqlConnection(strConn);
        sqlcon.Open();
        SqlTransaction sqlTr;
        sqlTr = sqlcon.BeginTransaction();
            try
            {
                for (int i = 0; i < arrList.Count; i++)
                {
                    Intake_Documents_DAO Intakedao = new Intake_Documents_DAO();
                    Intakedao = (Intake_Documents_DAO)arrList[i];

                    sqlCmd = new SqlCommand("sp_update_intake_provider_document", sqlcon);
                    sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = sqlTr;
                    sqlCmd.CommandTimeout = 0; 
                    sqlCmd.Parameters.AddWithValue("@i_id",'0');
                    sqlCmd.Parameters.AddWithValue("@i_provider_id", Intakedao.i_provider_id);
                    sqlCmd.Parameters.AddWithValue("@i_document_id", Intakedao.i_documnet_id);
                    sqlCmd.Parameters.AddWithValue("@sz_company_id", Intakedao.sz_company_id);
                    sqlCmd.Parameters.AddWithValue("@sz_case_type_id", Intakedao.sz_case_type_id);
                    sqlCmd.Parameters.AddWithValue("@sz_created_by", Intakedao.i_user_id);
                    sqlCmd.ExecuteNonQuery();
                    
                }
                sqlTr.Commit();
                sRetuen = "S";
            }
            catch (SqlException ex)
            {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            sqlTr.Rollback();
               sRetuen=  ex.Message.ToString();
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
        return    sRetuen;
    }
}