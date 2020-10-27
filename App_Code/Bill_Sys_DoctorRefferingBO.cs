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
/// Summary description for Bill_Sys_DoctorRefferingBO
/// </summary>
public class Bill_Sys_DoctorRefferingBO
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;
    SqlDataReader dr;

	public Bill_Sys_DoctorRefferingBO()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public int InsertOfficeMaster(string SZ_OFFICE, string SZ_COMPANY_ID, string SZ_OFFICE_ADDRESS, string SZ_OFFICE_CITY, string SZ_OFFICE_STATE, string SZ_OFFICE_ZIP, string SZ_OFFICE_PHONE, string SZ_BILLING_ADDRESS, string SZ_BILLING_CITY, string SZ_BILLING_STATE, string SZ_BILLING_ZIP, string SZ_BILLING_PHONE, string SZ_NPI, string SZ_FEDERAL_TAX_ID, string SZ_OFFICE_FAX, string SZ_OFFICE_EMAIL, string SZ_OFFICE_STATE_ID, string SZ_BILLING_STATE_ID, string BIT_IS_BILLING, string SZ_PREFIX, string SZ_LOCATION_ID, int BT_SoftFee, string szSoftwareFee, string sz_office_code,int bt_reffering)
    {
        int i = 0;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_OFFICE_REFF", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE", SZ_OFFICE);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ADDRESS", SZ_OFFICE_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_CITY", SZ_OFFICE_CITY);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_STATE", SZ_OFFICE_STATE);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ZIP", SZ_OFFICE_ZIP);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_PHONE", SZ_OFFICE_PHONE);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_ADDRESS", SZ_BILLING_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_CITY", SZ_BILLING_CITY);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_STATE", SZ_BILLING_STATE);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_ZIP", SZ_BILLING_ZIP);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_PHONE", SZ_BILLING_PHONE);
            sqlCmd.Parameters.AddWithValue("@SZ_NPI", SZ_NPI);
            sqlCmd.Parameters.AddWithValue("@SZ_FEDERAL_TAX_ID", SZ_FEDERAL_TAX_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_FAX", SZ_OFFICE_FAX);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_EMAIL", SZ_OFFICE_EMAIL);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_STATE_ID", SZ_OFFICE_STATE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_STATE_ID", SZ_BILLING_STATE_ID);

            sqlCmd.Parameters.AddWithValue("@BIT_IS_BILLING", BIT_IS_BILLING);
            sqlCmd.Parameters.AddWithValue("@SZ_PREFIX", SZ_PREFIX);
            sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", SZ_LOCATION_ID);
            // added by Kapil 05 Jan 2012
            sqlCmd.Parameters.AddWithValue("@SZ_IS_SOFTWARE_FEE_ADDED", BT_SoftFee);
            sqlCmd.Parameters.AddWithValue("@SZ_MN_SOFTWARE_FEE", szSoftwareFee);
            sqlCmd.Parameters.AddWithValue("@sz_place_of_service", sz_office_code);
            sqlCmd.Parameters.AddWithValue("@BT_REFFERING", bt_reffering);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");



            i = sqlCmd.ExecuteNonQuery();
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
        return i;
    }

    public int UpdateOfficeMaster(string SZ_OFFICE, string SZ_COMPANY_ID, string SZ_OFFICE_ADDRESS, string SZ_OFFICE_CITY, string SZ_OFFICE_STATE, string SZ_OFFICE_ZIP, string SZ_OFFICE_PHONE, string SZ_BILLING_ADDRESS, string SZ_BILLING_CITY, string SZ_BILLING_STATE, string SZ_BILLING_ZIP, string SZ_BILLING_PHONE, string SZ_NPI, string SZ_FEDERAL_TAX_ID, string SZ_OFFICE_FAX, string SZ_OFFICE_EMAIL, string SZ_OFFICE_STATE_ID, string SZ_BILLING_STATE_ID, string BIT_IS_BILLING, string SZ_PREFIX, string SZ_LOCATION_ID, string SZ_OFFICE_ID, int BT_SoftFee, string szSoftwareFee, string sz_office_code)
    {
        int i = 0;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_OFFICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE", SZ_OFFICE);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ADDRESS", SZ_OFFICE_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_CITY", SZ_OFFICE_CITY);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_STATE", SZ_OFFICE_STATE);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ZIP", SZ_OFFICE_ZIP);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_PHONE", SZ_OFFICE_PHONE);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_ADDRESS", SZ_BILLING_ADDRESS);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_CITY", SZ_BILLING_CITY);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_STATE", SZ_BILLING_STATE);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_ZIP", SZ_BILLING_ZIP);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_PHONE", SZ_BILLING_PHONE);
            sqlCmd.Parameters.AddWithValue("@SZ_NPI", SZ_NPI);
            sqlCmd.Parameters.AddWithValue("@SZ_FEDERAL_TAX_ID", SZ_FEDERAL_TAX_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_FAX", SZ_OFFICE_FAX);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_EMAIL", SZ_OFFICE_EMAIL);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_STATE_ID", SZ_OFFICE_STATE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_STATE_ID", SZ_BILLING_STATE_ID);

            sqlCmd.Parameters.AddWithValue("@BIT_IS_BILLING", BIT_IS_BILLING);
            sqlCmd.Parameters.AddWithValue("@SZ_PREFIX", SZ_PREFIX);
            sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", SZ_LOCATION_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", SZ_OFFICE_ID);
            // added by Kapil 05 Jan 2012
            sqlCmd.Parameters.AddWithValue("@SZ_IS_SOFTWARE_FEE_ADDED", BT_SoftFee);
            sqlCmd.Parameters.AddWithValue("@SZ_MN_SOFTWARE_FEE", szSoftwareFee);
            sqlCmd.Parameters.AddWithValue("@sz_place_of_service", sz_office_code);
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");



            i = sqlCmd.ExecuteNonQuery();
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
        return i;
    }

    public bool deleteRecord(string p_szSPName, string p_szIDName, string p_szIDValue)
    {
        String szValue = "";
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();

            sqlCmd = new SqlCommand(p_szSPName, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue(p_szIDName, p_szIDValue);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");

            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szValue = dr[0].ToString();

            }
            if (szValue.Equals("RECORD EXISTS"))
                return false;
            else
                return true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return false;
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return false;
    }

    public DataSet Location(string sz_location_id, string sz_company_id)
    {
        SqlCommand comm;
        SqlConnection conn = null;
        SqlDataAdapter sqlda;
        DataSet ds;
        String strsqlCon;
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_LOCATION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_LOCATION_ID", sz_location_id);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            comm.Parameters.AddWithValue("@FLAG", "LOCATION_DETAIL");




            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

            return ds;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }
    
    public bool checkForDelete(string p_szCompanyID, string p_szRoleID)
    {
        String szValue = "";
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_TXN_USER_CONFIGURATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_ROLE_ID", p_szRoleID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "CHECK_DELETE");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szValue = dr[0].ToString();
            }
            if (szValue != "")
                return false;
            else
                return true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return false;
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return false;
    }

    public DataSet getReffOfficeInformation(string sz_office_Id)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_billing_reff_office_info", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_office_id", sz_office_Id);
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
}