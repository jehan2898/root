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

/// <summary>
/// Summary description for Bill_Sys_FileType_Settings
/// </summary>
public class Bill_Sys_FileType_Settings
{
    String  strsqlCon;
    SqlConnection sqlCon;
    SqlCommand  sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public Bill_Sys_FileType_Settings()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet GET_Settings(string companyID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_CONFIG_INFORMATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GetSettings");
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

    public void UpdateSettings(string SZ_CONFIG_ID, string SZ_CONFIG_KEY_ID, string SZ_DESCRIPTION, string SZ_COMPANY_ID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_CONFIG_INFORMATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_CONFIG_ID", SZ_CONFIG_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_CONFIG_KEY_ID", SZ_CONFIG_KEY_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_DESCRIPTION", SZ_DESCRIPTION);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
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

    //public void LhrDocuments(string i_event_id, string i_event_proc_id, string sz_remote_procedure_code, string sz_remote_document, string sz_company_id, string sz_remote_case_id, string dt_visit_date, string sz_visit_time, string sz_visit_time_type, string sz_remote_procedure_group, string sz_remote_procedure_desc, string sz_remote_appointment_id)
    //{
    //    //SqlParameter sqlParam = new SqlParameter();
    //    sqlCon = new SqlConnection(strsqlCon);

    //    try
    //    {
    //        sqlCon.Open();
    //        sqlCmd = new SqlCommand("SP_ADD_LHR_DOCUMENTS", sqlCon);
    //        sqlCmd.CommandType = CommandType.StoredProcedure;
    //        sqlCmd.Parameters.AddWithValue("@EVENT_ID", i_event_id);
    //        sqlCmd.Parameters.AddWithValue("@EVENT_PROC_ID ", i_event_proc_id);
    //        sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_CODE", sz_remote_procedure_code);
    //        sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_DOCUMENT", sz_remote_document);
    //        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
    //        sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_CASE_ID ", sz_remote_case_id);
    //        sqlCmd.Parameters.AddWithValue("@DT_VISIT_DATE", dt_visit_date);
    //        sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TIME", sz_visit_time);
    //        sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TIME_TYPE", sz_visit_time_type);
    //        sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_GROUP", sz_remote_procedure_group);
    //        sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_DESC", sz_remote_procedure_desc);
    //        sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_APPOINTMENT_ID", sz_remote_appointment_id);


    //        sqlCmd.ExecuteNonQuery();


    //    }
    //    catch (SqlException ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //    finally
    //    {
    //        if (sqlCon.State == ConnectionState.Open)
    //        {
    //            sqlCon.Close();
    //        }
    //    }

    //}


    public void LhrDocuments(ArrayList _arrayList)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ADD_LHR_DOCUMENTS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@EVENT_ID", _arrayList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@EVENT_PROC_ID ", _arrayList[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_CODE", _arrayList[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_DOCUMENT", _arrayList[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_CASE_ID ", _arrayList[5].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_VISIT_DATE", _arrayList[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TIME", _arrayList[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TIME_TYPE", _arrayList[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_GROUP", _arrayList[9].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_DESC", _arrayList[10].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_APPOINTMENT_ID", _arrayList[11].ToString());


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

    public void Check_and_Insert_Visit_Doc(ArrayList _arrayList)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_LHR_CHECK_FOR_VISIT_DOCUMENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@EVENT_ID", _arrayList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@EVENT_PROC_ID ", _arrayList[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_CODE", _arrayList[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_DOCUMENT", _arrayList[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_CASE_ID ", _arrayList[5].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_VISIT_DATE", _arrayList[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TIME", _arrayList[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TIME_TYPE", _arrayList[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_GROUP", _arrayList[9].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_PROCEDURE_DESC", _arrayList[10].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REMOTE_APPOINTMENT_ID", _arrayList[11].ToString());

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

    public DataSet GET_Visit_Info(string Event_Proc_ID, string companyID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_VISIT_DOC_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@EVENT_PROC_ID",Event_Proc_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID",companyID);
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


    public DataSet GET_IMAGE_ID(string Event_Proc_ID,string sz_filename, string companyID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_IMAGE_ID_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@EVENT_PROC_ID", Event_Proc_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", sz_filename);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
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

    public void LhrDeleteDocuments(ArrayList _arrayList)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("DELETE_PROCEDURE_DOCUMENTS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_ID", _arrayList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@EVENT_PROC_ID ", _arrayList[1].ToString());
           

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


    public DataSet GET_IMAGE_ID_LHR(string IMAGE_ID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_IMAGE_ID_", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@IMAGE_ID", IMAGE_ID);
            

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


    public void Deletelhrdocuments(ArrayList _arrayList)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("DELETE_LHR_DOCUMENTS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@IMAGE_ID", _arrayList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@FILE_NAME", _arrayList[1].ToString());

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
