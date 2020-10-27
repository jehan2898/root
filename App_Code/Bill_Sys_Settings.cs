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
using System.IO;
using System.Text;
/// <summary>
/// Summary description for Bill_Sys_Settings
/// </summary>
public class Bill_Sys_Settings
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
	public Bill_Sys_Settings()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public string GET_KEY_BIT(string key)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        string szReturn = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_SETTING_KEY_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_SYS_SETTING_KEY_ID", key);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
            }
           

        }
        catch (SqlException ex)
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
        return szReturn;
    }

    public int Save_Key_Settings(string szKeyId, string szValue,string szCompanyId,string szSubValue)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        int iReturn = 0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SAVE_SYS_SETTING", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_SYS_SETTING_KEY_ID", szKeyId);
            sqlCmd.Parameters.AddWithValue("@SZ_SYS_SETTING_VALUE", szValue);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_SYS_SUB_SETTING_VALUE", szSubValue);
            iReturn= sqlCmd.ExecuteNonQuery();
            


        }
        catch (SqlException ex)
        {
            iReturn = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return iReturn;
    }

    //public void ForceChangedbit(string sz_companyID)
    //{
    //    try
    //    {
    //        sqlCon = new SqlConnection(strsqlCon);
    //        sqlCon.Open();
    //        string query = "UPDATE MST_USERS SET bt_force_change_password=1 WHERE SZ_BILLING_COMPANY='" + sz_companyID + "'";
    //        SqlCommand Cmd = new SqlCommand(query, sqlCon);
    //        Cmd.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    finally 
    //    {
    //        if (sqlCon.State == ConnectionState.Open)
    //        {
    //            sqlCon.Close();
    //        }
    //    }
    //}

    public int Update_Key_Settings(string szKeyId, string szValue, string szSettingID, string szSubValue)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        int iReturn = 0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_SYS_SETTING", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_SYS_SETTING_KEY_ID", szKeyId);
            sqlCmd.Parameters.AddWithValue("@SZ_SYS_SETTING_VALUE", szValue);
            sqlCmd.Parameters.AddWithValue("@SZ_SYS_SETTING_ID", szSettingID);
            sqlCmd.Parameters.AddWithValue("@SZ_SYS_SUB_SETTING_VALUE", szSubValue);
            iReturn = sqlCmd.ExecuteNonQuery();



        }
        catch (SqlException ex)
        {
            iReturn = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return iReturn;
    }

    public DataSet GetDoctorNotes(String selected_item, string company_Id)
    {
        // ArrayList _return = new ArrayList();
        ds = new DataSet();
        try
        {
           

            sqlCon = new SqlConnection(strsqlCon);
            sqlCon.Open();

            //sqlCmd = new SqlCommand("MS_DOCTOR_NOTES", sqlCon);
            sqlCmd = new SqlCommand("SP_MANDATORY_DOCTOR_NOTES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            //sqlCmd.Parameters.AddWithValue("@sz_Speciality", selected_item);
            sqlCmd.Parameters.AddWithValue("@sz_Speciality_Id", selected_item);
            sqlCmd.Parameters.AddWithValue("@sz_Company_Id", company_Id);

            sqlda = new SqlDataAdapter(sqlCmd);

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

    public string GetSpeciality(String selected_item, string company_Id)
    {
       
        sqlCon = new SqlConnection(strsqlCon);
        string szReturn = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_SPECIALITY", selected_item);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_Id", company_Id);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
            }
           

        }
        catch (SqlException ex)
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
        return szReturn;
    }
    public DataSet GetSpeciality(string Company_Id)
    {
      
        ds = new DataSet();
        try
        {


            sqlCon = new SqlConnection(strsqlCon);
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PROCEDURE_GROUP", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            //sqlCmd = new SqlCommand("SP_MST_GET_PROCEDURE_GROUP", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            //sqlCmd.Parameters.AddWithValue("@FLAG", "SP_MST_GET_PROCEDURE_GROUP");
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_PROCEDURE_GROUP_LIST");
            sqlCmd.Parameters.AddWithValue("@id", Company_Id);

            sqlda = new SqlDataAdapter(sqlCmd);

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

    public DataSet DeleteNodes(string id, string company_Id, string user_id)
    {

        ds = new DataSet();
        try
        {


            sqlCon = new SqlConnection(strsqlCon);
            sqlCon.Open();

            sqlCmd = new SqlCommand("DELETE_DOCTOR_NODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@i_mst_mandatory_id", id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_Id", company_Id);
            sqlCmd.Parameters.AddWithValue("@sz_created_user_id", user_id);

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
        return ds;
    }
    public DataSet GetNotes(string id, string company_Id, string user_id, string selected_item)
    {

        ds = new DataSet();
        try
        {


            sqlCon = new SqlConnection(strsqlCon);
            sqlCon.Open();

            sqlCmd = new SqlCommand("INSERT_DOCTOR_NODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@i_mst_mandatory_id", id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_Id", company_Id);
            sqlCmd.Parameters.AddWithValue("@sz_created_user_id", user_id);
            sqlCmd.Parameters.AddWithValue("@sz_specialty_id", selected_item);

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
        return ds;
    }
}
