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
using System.IO; 

public class Bill_Sys_Mandatory_Doctor_setting
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public Bill_Sys_Mandatory_Doctor_setting()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public int SaveDoctorSettinginfo(string szcompanyid, string szcasetypeid, string sznpi, string szdoclicensenumber, string szwcbauthnumber, string szwcbratingcode, string szcasetypename)
    {
        sqlCon = new SqlConnection(strsqlCon);
        int iReturn = 0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SAVE_DOCTOR_SETTING_CONFIGURATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompanyid);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", szcasetypeid);
            sqlCmd.Parameters.AddWithValue("@BT_NPI", sznpi);
            sqlCmd.Parameters.AddWithValue("@BT_DOC_LICENSE_NUMBER", szdoclicensenumber);
            sqlCmd.Parameters.AddWithValue("@BT_WCB_AUTHORIZATION_NUMBER", szwcbauthnumber);
            sqlCmd.Parameters.AddWithValue("@BT_WCB_RATING_CODE", szwcbratingcode);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_NAME", szcasetypename);
            iReturn = sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            iReturn = 0;
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
    public DataSet GetDoctorsettingInfo(string szCompanyId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_MANDATORY_DOCTOR_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
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

    public void UpdateDoctorsettingInfo(string szcompanyid, string szcasetypeid, string sznpi, string szdoclicensenumber, string szwcbauthnumber, string szwcbratingcode, string szsettingid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_MANDATORY_DOCTOR_SETTING", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompanyid);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", szcasetypeid);
            sqlCmd.Parameters.AddWithValue("@BT_NPI", sznpi);
            sqlCmd.Parameters.AddWithValue("@BT_DOC_LICENSE_NUMBER", szdoclicensenumber);
            sqlCmd.Parameters.AddWithValue("@BT_WCB_AUTHORIZATION_NUMBER", szwcbauthnumber);
            sqlCmd.Parameters.AddWithValue("@BT_WCB_RATING_CODE", szwcbratingcode);
            sqlCmd.Parameters.AddWithValue("@I_SETTING_ID", szsettingid);
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public void RemoveMandatorySetting(string szcompanyid, string szcasetypeid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DELETE_MANDATORY_SETTING", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompanyid);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", szcasetypeid);
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }
}
