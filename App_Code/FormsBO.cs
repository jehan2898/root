using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web;

public class FormsBO
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataReader dr;

    public FormsBO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataSet loadData(string CaseID)
    {
        DataSet dataSet = new DataSet();
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("sp_load_c4auth_data", connection);
            selectCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@i_case_id", CaseID);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
    }

    public DataSet GetDoctorInfo(string doctorid)
    {
        DataSet dataSet = new DataSet();
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("sp_get_attending_doctor_info", connection);
            selectCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorid);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
    }

    public void deleteC4ByCaseID(ArrayList obj)
    {
        try
        {
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
            conn = new SqlConnection(strConn);
            conn.Open();

            for (int i = 0; i < obj.Count; i++)
            {
                FormsBO objForm = new FormsBO();
                objForm = (FormsBO)obj[i];
                string query = "Delete from txn_pdf_c4auth where i_case_id='" + objForm.i_case_id + "' and sz_company_id='" + objForm.sz_company_id + "'";
                SqlCommand Cmd = new SqlCommand(query, conn);
                Cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                Cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }
    public void saveC4Pdf(ArrayList obj)
    {
        try
        {
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
            conn = new SqlConnection(strConn);
            conn.Open();

            for (int i = 0; i < obj.Count; i++)
            {
                FormsBO objForm = new FormsBO();
                objForm = (FormsBO)obj[i];
                string query = "INSERT INTO [txn_pdf_c4auth] ([i_case_id],[sz_company_id],[s_control_name],[s_value],[s_control_type],[s_pdf_control_name],[dt_created],[sz_user_id])VALUES('" + objForm.i_case_id + "','" + objForm.sz_company_id + "','" + objForm.s_control_name + "','" + objForm.s_value + "','" + objForm.s_control_type + "','" + objForm.s_pdf_control_name + "',GETDATE(),'" + objForm.sz_user_id + "')";
                SqlCommand Cmd = new SqlCommand(query, conn);
                Cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                
                Cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    #region MG2
    public void saveMG2Pdf(ArrayList obj)
    {
        try
        {
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
            conn = new SqlConnection(strConn);
            conn.Open();

            FormsBO objForm1 = new FormsBO();
            objForm1 = (FormsBO)obj[0];
            //string query1 = "Delete from txn_pdf_c4auth where i_case_id='" + objForm1.i_case_id + "' and sz_company_id='" + objForm1.sz_company_id + "'";
            //SqlCommand Cmd1 = new SqlCommand(query1, conn);

            comm = new SqlCommand("sp_delete_MG2Pdf", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@i_case_id", objForm1.i_case_id);
            comm.Parameters.AddWithValue("@z_company_id", objForm1.sz_company_id);
            comm.ExecuteNonQuery();

            for (int i = 0; i < obj.Count; i++)
            {
                FormsBO objForm = new FormsBO();
                objForm = (FormsBO)obj[i];
                //string query = "INSERT INTO [txn_pdf_mg2] ([i_case_id],[sz_company_id],[s_control_name],[s_value],[s_control_type],[s_pdf_control_name],[dt_created],[sz_user_id])VALUES('" + objForm.i_case_id + "','" + objForm.sz_company_id + "','" + objForm.s_control_name + "','" + objForm.s_value + "','" + objForm.s_control_type + "','" + objForm.s_pdf_control_name + "',GETDATE(),'" + objForm.sz_user_id + "')";
                //SqlCommand Cmd = new SqlCommand(query, conn);

                comm = new SqlCommand("sp_save_MG2Pdf", conn);
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@i_case_id", objForm.i_case_id);
                comm.Parameters.AddWithValue("@sz_company_id", objForm.sz_company_id);
                comm.Parameters.AddWithValue("@s_control_name", objForm.s_control_name);
                comm.Parameters.AddWithValue("@s_value", objForm.s_value);
                comm.Parameters.AddWithValue("@s_control_type", objForm.s_control_type);
                comm.Parameters.AddWithValue("@s_pdf_control_name", objForm.s_pdf_control_name);
                comm.Parameters.AddWithValue("@sz_user_id", objForm.sz_user_id);
                comm.ExecuteNonQuery();

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    public DataSet getMG2Data(string CaseID)
    {
        DataSet dataSet = new DataSet();
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("sp_get_mg2_data", connection);
            selectCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@i_case_id", CaseID);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
    }
    #endregion

    private string _i_case_id;
    public string i_case_id
    {
        get
        {
            return _i_case_id;
        }
        set
        {
            _i_case_id = value;
        }
    }

    private string _sz_company_id;
    public string sz_company_id
    {
        get
        {
            return _sz_company_id;
        }
        set
        {
            _sz_company_id = value;
        }
    }

    private string _s_control_name;
    public string s_control_name
    {
        get
        {
            return _s_control_name;
        }
        set
        {
            _s_control_name = value;
        }
    }

    private string _s_value;
    public string s_value
    {
        get
        {
            return _s_value;
        }
        set
        {
            _s_value = value;
        }
    }

    private string _s_control_type;
    public string s_control_type
    {
        get
        {
            return _s_control_type;
        }
        set
        {
            _s_control_type = value;
        }
    }

    private string _s_pdf_control_name;
    public string s_pdf_control_name
    {
        get
        {
            return _s_pdf_control_name;
        }
        set
        {
            _s_pdf_control_name = value;
        }
    }

    private string _sz_user_id;
    public string sz_user_id
    {
        get
        {
            return _sz_user_id;
        }
        set
        {
            _sz_user_id = value;
        }
    }

    private DateTime _dt_created;
    public DateTime dt_created
    {
        get
        {
            return _dt_created;
        }
        set
        {
            _dt_created = value;
        }
    }

}