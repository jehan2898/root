using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using log4net;
using System.Web;

public class Bill_Sys_LoginBO
{

    String strConn;
    SqlConnection conn;
    SqlCommand comm;

    SqlDataReader dr;

    static ILog log;

    public Bill_Sys_LoginBO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        Bill_Sys_LoginBO.log = LogManager.GetLogger("AJAX_Pages_Bill_sys_new_VisitPopup");
    }
    public DataSet getLoginDetails(string userId, string strGUID, String IPAddress)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            DataSet _return = new DataSet();
            bool flag = true;
            try
            {

                BlazeFast.client.Client_Select oClient = new BlazeFast.client.Client_Select();
                _return = oClient.Select_Users_LoginData(BlazeFast.constants.CCHKeyConstants.CCHKEY_USER_LOGIN_DATA, userId);

            }
            catch (Exception ex)
            {
                log.Debug("Error BlazeFast" + ex.Message.ToString());

                _return = new DataSet();
                conn = new SqlConnection(strConn);
                conn.Open();

                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "SP_USER_LOGIN";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Parameters.AddWithValue("@SZ_USER_NAME", userId);
                //comm.Parameters.AddWithValue("@SZ_PASSWORD", passcode);
                if (strGUID != "")
                {
                    comm.Parameters.AddWithValue("@GUID", strGUID);
                }
                comm.Parameters.AddWithValue("@Flag", "CHECKLOGIN");
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(comm);
                sqlAdapter.Fill(_return);
            }
            //if (_return.Tables.Count > 0)
            //{
            //    if (_return.Tables[0].Rows.Count > 0)
            //    {
            //        log.Debug("Data fetched from BlazeFast Service");
            //        flag = false;
            //    }
            //}
            //if (flag)
            //{
            //    _return = new DataSet();
            //    conn = new SqlConnection(strConn);
            //    conn.Open();

            //    comm = new SqlCommand();
            //    comm.CommandText = "SP_USER_LOGIN";
            //    comm.CommandType = CommandType.StoredProcedure;
            //    comm.Connection = conn;
            //    comm.Parameters.AddWithValue("@SZ_USER_NAME", userId);
            //    //comm.Parameters.AddWithValue("@SZ_PASSWORD", passcode);
            //    if (strGUID != "")
            //    {
            //        comm.Parameters.AddWithValue("@GUID", strGUID);
            //    }
            //    comm.Parameters.AddWithValue("@Flag", "CHECKLOGIN");
            //    SqlDataAdapter sqlAdapter = new SqlDataAdapter(comm);
            //    sqlAdapter.Fill(_return);
            //}
            if (_return.Tables.Count > 0)
            {
                try
                {
                    if (_return.Tables[0].Rows[0]["IPCheck"].ToString() != "")
                    {
                        if (_return.Tables[0].Rows[0]["IPCheck"].ToString() == "True")
                        {
                            if (!CheckIP(_return.Tables[0].Rows[0][0].ToString(), IPAddress))
                            {
                                _return = new DataSet();
                                _return.Tables.Add();
                                _return.Tables[0].Columns.Add();
                                _return.Tables[0].Rows[0][0] = "False";
                                return _return;
                            }
                        }
                    }
                }
                catch { }
            }
            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }

    public ArrayList CheckForLogin(String userId, String strGUID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_USER_LOGIN";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_NAME", userId);
            //comm.Parameters.AddWithValue("@SZ_PASSWORD", passcode);
            if (strGUID != "")
            {
                comm.Parameters.AddWithValue("@GUID", strGUID);
            }
            comm.Parameters.AddWithValue("@Flag", "CHECKLOGIN");

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                _return.Add(Convert.ToString(dr[0]));
                _return.Add(Convert.ToString(dr[1]));
                _return.Add(Convert.ToString(dr[2]));
                _return.Add(Convert.ToString(dr[3]));
                _return.Add(Convert.ToString(dr[4]));
                _return.Add(Convert.ToString(dr[5]));
                if (dr[6] != DBNull.Value)
                { _return.Add(Convert.ToString(dr[6])); }
                else
                { _return.Add(""); }
                if (dr[7] != DBNull.Value)
                { _return.Add(Convert.ToString(dr[7])); }
                else
                { _return.Add(""); }
                _return.Add(Convert.ToString(dr[8]));
                _return.Add(Convert.ToString(dr[9]));
                if (dr[10] != DBNull.Value)
                { _return.Add(Convert.ToString(dr[10])); }
                else
                { _return.Add(""); }
                if (dr[11] != DBNull.Value)
                { _return.Add(Convert.ToString(dr[11])); }
                else
                { _return.Add(""); }

                if (dr[12] != DBNull.Value)
                { _return.Add(Convert.ToString(dr[12])); }
                else
                { _return.Add(""); }

                if (dr[13] != DBNull.Value)
                { _return.Add(Convert.ToString(dr[13])); }
                else
                { _return.Add(""); }


            }

            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }
    public ArrayList CheckForLogin(String userId, String strGUID, String IPAddress)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_USER_LOGIN";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_NAME", userId);
            //comm.Parameters.AddWithValue("@SZ_PASSWORD", passcode);
            if (strGUID != "")
            {
                comm.Parameters.AddWithValue("@GUID", strGUID);
            }
            comm.Parameters.AddWithValue("@Flag", "CHECKLOGIN");

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                _return.Add(Convert.ToString(dr[0]));
                _return.Add(Convert.ToString(dr[1]));
                _return.Add(Convert.ToString(dr[2]));
                _return.Add(Convert.ToString(dr[3]));
                _return.Add(Convert.ToString(dr[4]));
                _return.Add(Convert.ToString(dr[5]));
                if (dr[6] != DBNull.Value)
                { _return.Add(Convert.ToString(dr[6])); }
                else
                { _return.Add(""); }
                if (dr[7] != DBNull.Value)
                { _return.Add(Convert.ToString(dr[7])); }
                else
                { _return.Add(""); }
                _return.Add(Convert.ToString(dr[8]));
                _return.Add(Convert.ToString(dr[9]));
                if (dr[10] != DBNull.Value)
                { _return.Add(Convert.ToString(dr[10])); }
                else
                { _return.Add(""); }
                if (dr[11] != DBNull.Value)
                { _return.Add(Convert.ToString(dr[11])); }
                else
                { _return.Add(""); }

                if (dr[12] != DBNull.Value)
                { _return.Add(Convert.ToString(dr[12])); }
                else
                { _return.Add(""); }

                if (dr[13] != DBNull.Value)
                { _return.Add(Convert.ToString(dr[13])); }
                else
                { _return.Add(""); }

                if (dr["Force_Changed_Password"] != DBNull.Value)
                { _return.Add(Convert.ToString(dr["Force_Changed_Password"])); }
                else
                { _return.Add(""); }

                if (dr["DT_NEXT_EXPIRY"] != DBNull.Value)
                { _return.Add(Convert.ToString(dr["DT_NEXT_EXPIRY"])); }
                else
                { _return.Add(""); }

                if (dr["IPCheck"] != DBNull.Value)
                {
                    if (dr["IPCheck"].ToString() == "True")
                    {
                        if (!CheckIP(dr[0].ToString(), IPAddress))
                        {
                            _return = new ArrayList();
                            _return.Add("False");
                            return _return;
                        }
                    }
                }
                //Note: This should be the last element added to the Arraylist
                _return.Add(GetIPAdmin(dr[0].ToString(), dr[2].ToString()));


                if (dr["BT_ATTORNY"] != DBNull.Value)
                { _return.Add(Convert.ToString(dr["BT_ATTORNY"])); }
                else
                { _return.Add(""); }

                if (dr["BT_PROVIDER"] != DBNull.Value)            // Added By Kapil 12 March 2012
                { _return.Add(Convert.ToString(dr["BT_PROVIDER"])); }
                else
                { _return.Add(""); }
            }


            return _return;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }
    public String GetIPAdmin(String UserId, string CompanyId)
    {
        try
        {
            DataTable dt = new DataTable();
            conn = new SqlConnection(strConn);
            string commandtext = "select SZ_SYS_SETTING_VALUE From MST_SYS_SETTINGS join MST_SYS_SETTING_KEY on MST_SYS_SETTING_KEY.SZ_SYS_SETTING_KEY_ID=MST_SYS_SETTINGS.SZ_SYS_SETTING_KEY_ID" +
                                  " where MST_SYS_SETTING_KEY.SZ_SYS_SETTING_KEY='IP Admin' AND SZ_COMPANY_ID='" + CompanyId + "' and SZ_SYS_SETTING_VALUE='" + UserId + "'";
            SqlDataAdapter da = new SqlDataAdapter(commandtext, conn);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return "IPAdminTrue";
            }
            else
                return "IPAdminFalse";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return "IPAdminFalse";

        }
        finally { conn.Close(); }
    }
    public Boolean CheckIP(String UserId, String IPAddress)
    {
        try
        {
            DataTable dt = new DataTable();
            conn = new SqlConnection(strConn);
            string commandtext = "SELECT ISNULL(SZ_USER_ID,'') FROM MST_USERS_IP_CONFIG WHERE SZ_USER_ID='" + UserId + "' AND SZ_IP_ADDRESS='" + IPAddress + "'";
            SqlDataAdapter da = new SqlDataAdapter(commandtext, conn);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return false;
        }
        finally { conn.Close(); }
        return false;
    }
    public String Readonly()   //return true or false  add Prashant 26 march
    {
        string appFlag = null;
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_READONLY";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                appFlag = dr.GetString(0);
            }
            return appFlag;


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }

    public Int32 ChangePassword(string userId, String oldPassword, String newPassword)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            Int32 _return = 0;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_USER_LOGIN";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_NAME", userId);
            comm.Parameters.AddWithValue("@SZ_OLD_PASSWORD", oldPassword);
            comm.Parameters.AddWithValue("@SZ_PASSWORD", newPassword);
            comm.Parameters.AddWithValue("@Flag", "CHANGEPASSWORD");

            comm.Parameters.AddWithValue("@Return", SqlDbType.Int);
            comm.Parameters["@Return"].Direction = ParameterDirection.ReturnValue;
            comm.ExecuteNonQuery();
            _return = Convert.ToInt32(comm.Parameters["@Return"].Value);
            return _return;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return 0;
        }
        finally { conn.Close(); }
        return 0;
    }
    public void GetGUID()
    {
        try
        {

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    public Int32 ChangeLoginDate(string userId)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            Int32 _return = 0;
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_USER_LOGIN";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_ID", userId);
            comm.Parameters.AddWithValue("@Flag", "UPDATEDATE");

            comm.Parameters.AddWithValue("@Return", SqlDbType.Int);
            comm.Parameters["@Return"].Direction = ParameterDirection.ReturnValue;
            comm.ExecuteNonQuery();
            _return = Convert.ToInt32(comm.Parameters["@Return"].Value);
            return _return;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return 0;
        }
        finally { conn.Close(); }
        return 0;
    }

    public String getDefaultSettings(String p_szCompanyID, String p_szKeyID)
    {
        String szValue = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_SYS_SETTINGS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_SYS_SETTING_KEY_ID", p_szKeyID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GET_KEY_VALUE");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szValue = dr["SZ_SYS_SETTING_VALUE"].ToString();
            }
            return szValue;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }

    public DataTable GetConfigSettings(String p_szCompanyID, String p_szRoleID)
    {
        DataTable dt = new DataTable();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_USER_CONFIGURATION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_ROLE_ID", p_szRoleID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GETCONFIGSETTINGS");
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }

    public DataTable GetDocConfigSettings(String p_szCompanyID, String p_szConfigurationID)
    {
        DataTable dt = new DataTable();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_DOC_MANAGER_CONFIGURATION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CONFIGURATION_ID", p_szConfigurationID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GETCONFIGSETTINGS");
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }

    public Int32 ForceChangePassword(string strDays, string NextExpiry, string strConfirmPassword, string UID)
    {
        string strPassword;
        Int32 _return = 0;
        try
        {
            strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
            conn = new SqlConnection(strConn);
            conn.Open();

            String strPassPhrase = "Pas5pr@se";        // can be any string
            String strSaltValue = "s@1tValue";       // can be any string
            String strHashAlgorithm = "SHA1";         // can be "MD5"
            int intPasswordIterations = 2;          // can be any number
            String strInitVector = "@1B2c3D4e5F6g7H8";// must be 16 bytes
            int intKeySize = 256;
            strPassword = Bill_Sys_EncryDecry.Encrypt(strConfirmPassword, strPassPhrase, strSaltValue, strHashAlgorithm, intPasswordIterations, strInitVector, intKeySize);


           
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_USER_CHANGE_PASSWORD";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_ID", UID);
            comm.Parameters.AddWithValue("@SZ_PASSWORD", strPassword);
            comm.Parameters.AddWithValue("@DT_NEXT_EXPIRY", NextExpiry);
            comm.Parameters.AddWithValue("@I_PASSWORD_EXPIRES", strDays);
            comm.Parameters.AddWithValue("@Flag", "CHANGEPASSWORD");

            comm.Parameters.AddWithValue("@Return", SqlDbType.Int);
            comm.Parameters["@Return"].Direction = ParameterDirection.ReturnValue;
            comm.ExecuteNonQuery();
            _return = Convert.ToInt32(comm.Parameters["@Return"].Value);
            
            //string query = "UPDATE MST_USERS SET BT_FORCE_CHANGE_PASSWORD=1, SZ_PASSWORD='" + strPassword + "', i_password_expires='" + strDays + "', DT_NEXT_EXPIRY='" + NextExpiry + "', bt_force_change_password = 0 WHERE SZ_USER_ID='" + UID + "'";
            //SqlCommand Cmd = new SqlCommand(query, conn);
            //Cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally { conn.Close(); }
        return _return;
    }
    public string getconfiguration(string sz_Role_ID, string p_szCompanyID)
    {
        string szValue = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_CONFIGURATION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_ROLE_ID", sz_Role_ID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szValue = dr[0].ToString();
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szValue = "";
        }
        finally
        {
            conn.Close();
        }
        return szValue;
    }
    public string getconfigurationlocation(string sz_Role_ID, string p_szCompanyID)
    {
        string szValue = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_CONFIGURATION_LOCATION";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_ROLE_ID", sz_Role_ID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
             dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szValue = dr[0].ToString();
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szValue = "";
        }
        finally
        {
            conn.Close();
        }
        return szValue;
    }


}
[Serializable]
public class Bill_Sys_UserObject
{
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
    private string _SZ_USER_ROLE;
    public string SZ_USER_ROLE
    {
        get
        {
            return _SZ_USER_ROLE;
        }
        set
        {
            _SZ_USER_ROLE = value;
        }
    }

    private string _SZ_USER_ROLE_NAME;
    public string SZ_USER_ROLE_NAME
    {
        get
        {
            return _SZ_USER_ROLE_NAME;
        }
        set
        {
            _SZ_USER_ROLE_NAME = value;
        }
    }

    private string _SZ_PROVIDER_ID;
    public string SZ_PROVIDER_ID
    {
        get
        {
            return _SZ_PROVIDER_ID;
        }
        set
        {
            _SZ_PROVIDER_ID = value;
        }
    }

    private string _SZ_REFF_PROVIDER_ID;
    public string SZ_REFF_PROVIDER_ID
    {
        get
        {
            return _SZ_REFF_PROVIDER_ID;
        }
        set
        {
            _SZ_REFF_PROVIDER_ID = value;
        }
    }

    private string _SZ_USER_EMAIL;
    public string SZ_USER_EMAIL
    {
        get
        {
            return _SZ_USER_EMAIL;
        }
        set
        {
            _SZ_USER_EMAIL = value;
        }
    }
    public string _DomainName;
    public string DomainName
    {
        get
        {
            return _DomainName;
        }
        set
        {
            _DomainName = value;
        }
    }

}
[Serializable]
public class Bill_Sys_BillingCompanyObject
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

    private string _SZ_COMPANY_NAME;
    public string SZ_COMPANY_NAME
    {
        get
        {
            return _SZ_COMPANY_NAME;
        }
        set
        {
            _SZ_COMPANY_NAME = value;
        }
    }

    private string _SZ_PREFIX;
    public string SZ_PREFIX
    {

        get
        {
            return _SZ_PREFIX;
        }
        set
        {
            _SZ_PREFIX = value;
        }
    }

    private Boolean _BT_REFERRING_FACILITY;
    public Boolean BT_REFERRING_FACILITY
    {

        get
        {
            return _BT_REFERRING_FACILITY;
        }
        set
        {
            _BT_REFERRING_FACILITY = value;
        }
    }

    private Boolean _BT_LAW_FIRM;

    public Boolean BT_LAW_FIRM
    {

        get
        {
            return _BT_LAW_FIRM;
        }
        set
        {
            _BT_LAW_FIRM = value;
        }
    }


    private string _SZ_READ_ONLY;
    public string SZ_READ_ONLY
    {
        get
        {
            return _SZ_READ_ONLY;
        }
        set
        {
            _SZ_READ_ONLY = value;
        }
    }


    private string _SZ_ADDRESS;
    public string SZ_ADDRESS
    {

        get
        {
            return _SZ_ADDRESS;
        }
        set
        {
            _SZ_ADDRESS = value;
        }
    }

    private string _SZ_EMAIL;
    public string SZ_EMAIL
    {

        get
        {
            return _SZ_EMAIL;
        }
        set
        {
            _SZ_EMAIL = value;
        }
    }

    private string _SZ_PHONE;
    public string SZ_PHONE
    {

        get
        {
            return _SZ_PHONE;
        }
        set
        {
            _SZ_PHONE = value;
        }
    }

    private string _SZ_FAX;
    public string SZ_FAX
    {

        get
        {
            return _SZ_FAX;
        }
        set
        {
            _SZ_FAX = value;
        }
    }
    private string _SZ_ADDRESS_CITY;
    public string SZ_ADDRESS_CITY
    {

        get
        {
            return _SZ_ADDRESS_CITY;
        }
        set
        {
            _SZ_ADDRESS_CITY = value;
        }
    }

    private string _SZ_ADDRESS_STATE;
    public string SZ_ADDRESS_STATE
    {

        get
        {
            return _SZ_ADDRESS_STATE;
        }
        set
        {
            _SZ_ADDRESS_STATE = value;
        }
    }
    private string _SZ_ADDRESS_ZIP;
    public string SZ_ADDRESS_ZIP
    {

        get
        {
            return _SZ_ADDRESS_ZIP;
        }
        set
        {
            _SZ_ADDRESS_ZIP = value;
        }
    }
    private string _SZ_ADDRESS_STREET;
    public string SZ_ADDRESS_STREET
    {

        get
        {
            return _SZ_ADDRESS_STREET;
        }
        set
        {
            _SZ_ADDRESS_STREET = value;
        }
    }
    private Boolean _BT_ATTORNY;
    public Boolean BT_ATTORNY
    {
        get
        {
            return _BT_ATTORNY;
        }
        set
        {
            _BT_ATTORNY = value;
        }
    }

    private Boolean _BT_PROVIDER;
    public Boolean BT_PROVIDER
    {
        get
        {
            return _BT_PROVIDER;
        }
        set
        {
            _BT_PROVIDER = value;
        }
    }



}
[Serializable]
public class Bill_Sys_Case
{
    private string _SZ_CASE_ID;
    public string SZ_CASE_ID
    {
        get
        {
            return _SZ_CASE_ID;
        }
        set
        {
            _SZ_CASE_ID = value;
        }
    }
}

[Serializable]
public class Bill_Sys_CaseObject
{
    private string _SZ_CASE_ID;
    public string SZ_CASE_ID
    {
        get
        {
            return _SZ_CASE_ID;
        }
        set
        {
            _SZ_CASE_ID = value;
        }
    }
    private string _SZ_CASE_NO;
    public string SZ_CASE_NO
    {
        get
        {
            return _SZ_CASE_NO;
        }
        set
        {
            _SZ_CASE_NO = value;
        }
    }
    private string _SZ_PATIENT_ID;
    public string SZ_PATIENT_ID
    {
        get
        {
            return _SZ_PATIENT_ID;
        }
        set
        {
            _SZ_PATIENT_ID = value;
        }
    }
    private string _SZ_PATIENT_NAME;
    public string SZ_PATIENT_NAME
    {
        get
        {
            return _SZ_PATIENT_NAME;
        }
        set
        {
            _SZ_PATIENT_NAME = value;
        }
    }

    private string _SZ_COMAPNY_ID;
    public string SZ_COMAPNY_ID
    {
        get
        {
            return _SZ_COMAPNY_ID;
        }
        set
        {
            _SZ_COMAPNY_ID = value;
        }
    }
}
[Serializable]
public class Bill_Sys_SystemObject
{
    private string _SZ_DEFAULT_LAW_FIRM = "";
    public string SZ_DEFAULT_LAW_FIRM
    {
        get
        {
            return _SZ_DEFAULT_LAW_FIRM;
        }
        set
        {
            _SZ_DEFAULT_LAW_FIRM = value;
        }
    }

    private string _SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION = "";
    public string SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION
    {
        get
        {
            return _SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION;
        }
        set
        {
            _SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION = value;
        }
    }


    private string _SZ_SHOW_PROVIDER_DISPLAY_NAME = "";
    public string SZ_SHOW_PROVIDER_DISPLAY_NAME
    {
        get
        {
            return _SZ_SHOW_PROVIDER_DISPLAY_NAME;
        }
        set
        {
            _SZ_SHOW_PROVIDER_DISPLAY_NAME = value;
        }
    }

    private string _SZ_CHART_NO = "";
    public string SZ_CHART_NO
    {
        get
        {
            return _SZ_CHART_NO;
        }
        set
        {
            _SZ_CHART_NO = value;
        }
    }

    private string _SZ_SOFT_DELETE = "";
    public string SZ_SOFT_DELETE
    {
        get
        {
            return _SZ_SOFT_DELETE;
        }
        set
        {
            _SZ_SOFT_DELETE = value;
        }
    }
    private string _SZ_HARD_DELETE = "";

    public string SZ_HARD_DELETE
    {
        get
        {
            return _SZ_HARD_DELETE;
        }
        set
        {
            _SZ_HARD_DELETE = value;
        }
    }


    private string _SZ_NEW_BILL = "";

    public string SZ_NEW_BILL
    {
        get
        {
            return _SZ_NEW_BILL;
        }
        set
        {
            _SZ_NEW_BILL = value;
        }
    }

    private string _SZ_VIEW_BILL = "";

    public string SZ_VIEW_BILL
    {
        get
        {
            return _SZ_VIEW_BILL;
        }
        set
        {
            _SZ_VIEW_BILL = value;
        }
    }

    private string _SZ_LOCATION = "";

    public string SZ_LOCATION
    {
        get
        {
            return _SZ_LOCATION;
        }
        set
        {
            _SZ_LOCATION = value;
        }
    }

    private string _checkinvalue;

    public string SZ_CHECKINVALUE
    {
        get
        {
            return _checkinvalue;
        }
        set
        {
            _checkinvalue = value;
        }
    }

    private string _SZ_DELETE_BILLS = "";
    public string SZ_DELETE_BILLS
    {
        get
        {
            return _SZ_DELETE_BILLS;
        }
        set
        {
            _SZ_DELETE_BILLS = value;
        }
    }

    private string _SZ_DELETE_VIEWS = "";
    public string SZ_DELETE_VIEWS
    {
        get
        {
            return _SZ_DELETE_VIEWS;
        }
        set
        {
            _SZ_DELETE_VIEWS = value;
        }
    }


    private string _SZ_NOTE_DELETE = "";

    public string SZ_NOTE_DELETE
    {
        get
        {
            return _SZ_NOTE_DELETE;
        }
        set
        {
            _SZ_NOTE_DELETE = value;
        }
    }


    private string _SZ_NOTE_SOFT_DELETE = "";

    public string SZ_NOTE_SOFT_DELETE
    {
        get
        {
            return _SZ_NOTE_SOFT_DELETE;
        }
        set
        {
            _SZ_NOTE_SOFT_DELETE = value;
        }
    }

    private string szAddVisits_SearchByChartNumber = null;
    public string AddVisits_SearchByChartNumber
    {
        get
        {
            return szAddVisits_SearchByChartNumber;
        }
        set
        {
            szAddVisits_SearchByChartNumber = value;
        }
    }

    private string _SZ_EMG_BILL = "";
    public string SZ_EMG_BILL
    {
        get
        {
            return _SZ_EMG_BILL;
        }
        set
        {
            _SZ_EMG_BILL = value;
        }
    }
    private string _SZ_SHOW_PATIENT_PHONE = "";
    public string SZ_SHOW_PATIENT_PHONE
    {
        get
        {
            return _SZ_SHOW_PATIENT_PHONE;
        }
        set
        {
            _SZ_SHOW_PATIENT_PHONE = value;
        }
    }

    private string _SZ_SHOW_PATIENT_SIGNATURE_FOR_NF3 = "";
    public string SZ_SHOW_PATIENT_SIGNATURE_FOR_NF3
    {
        get
        {
            return _SZ_SHOW_PATIENT_SIGNATURE_FOR_NF3;
        }
        set
        {
            _SZ_SHOW_PATIENT_SIGNATURE_FOR_NF3 = value;
        }
    }
    private string _SZ_SHOW_DOCTOR_SIGNATURE_FOR_NF3 = "";
    public string SZ_SHOW_DOCTOR_SIGNATURE_FOR_NF3
    {
        get
        {
            return _SZ_SHOW_DOCTOR_SIGNATURE_FOR_NF3;
        }
        set
        {
            _SZ_SHOW_DOCTOR_SIGNATURE_FOR_NF3 = value;
        }
    }
    private string _SZ_SHOW_DATE_OF_FIRST_TREATMENT = "";
    public string SZ_SHOW_DATE_OF_FIRST_TREATMENT
    {
        get
        {
            return _SZ_SHOW_DATE_OF_FIRST_TREATMENT;
        }
        set
        {
            _SZ_SHOW_DATE_OF_FIRST_TREATMENT = value;
        }
    }
    private string _SZ_SHOW_NEW_POM = "";
    public string SZ_SHOW_NEW_POM
    {
        get
        {
            return _SZ_SHOW_NEW_POM;
        }
        set
        {
            _SZ_SHOW_NEW_POM = value;
        }
    }

    private string _SZ_ASSIGN_DIAGNOSIS_CODE_TO_VISIT = "";
    public string SZ_ASSIGN_DIAGNOSIS_CODE_TO_VISIT
    {
        get
        {
            return _SZ_ASSIGN_DIAGNOSIS_CODE_TO_VISIT;
        }
        set
        {
            _SZ_ASSIGN_DIAGNOSIS_CODE_TO_VISIT = value;
        }
    }

    private string _ASSOCIATE_CASE_TYPE_WITH_VISITS;
    public string ASSOCIATE_CASE_TYPE_WITH_VISITS
    {
        get
        {
            return _ASSOCIATE_CASE_TYPE_WITH_VISITS;
        }
        set
        {
            _ASSOCIATE_CASE_TYPE_WITH_VISITS = value;
        }
    }
    private string _ADD_LOCATION_TO_VISITS;
    public string ADD_LOCATION_TO_VISITS
    {
        get
        {
            return _ADD_LOCATION_TO_VISITS;
        }
        set
        {
            _ADD_LOCATION_TO_VISITS = value;
        }
    }
    private string _SZ_SHOW_INSURANCE_WITH_BILL = "";
    public string SZ_SHOW_INSURANCE_WITH_BILL
    {
        get
        {
            return _SZ_SHOW_INSURANCE_WITH_BILL;
        }
        set
        {
            _SZ_SHOW_INSURANCE_WITH_BILL = value;
        }
    }
    private string _SZ_ALLOW_TO_EDIT_NF2_PDF = "";
    public string SZ_ALLOW_TO_EDIT_NF2_PDF
    {
        get
        {
            return _SZ_ALLOW_TO_EDIT_NF2_PDF;
        }
        set
        {
            _SZ_ALLOW_TO_EDIT_NF2_PDF = value;
        }
    }
    private string _SZ_SHOW_NF3_PROCEDURE_CODE = "";
    public string SZ_SHOW_NF3_PROCEDURE_CODE
    {
        get
        {
            return _SZ_SHOW_NF3_PROCEDURE_CODE;
        }
        set
        {
            _SZ_SHOW_NF3_PROCEDURE_CODE = value;
        }
    }
    private string _ALLOW_TO_ADD_VISIT_FOR_FUTURE_DATE = "";
    public string ALLOW_TO_ADD_VISIT_FOR_FUTURE_DATE
    {
        get
        {
            return _ALLOW_TO_ADD_VISIT_FOR_FUTURE_DATE;
        }
        set
        {
            _ALLOW_TO_ADD_VISIT_FOR_FUTURE_DATE = value;
        }
    }

    private string _PHONE_FORMATE = "";
    public string PHONE_FORMATE
    {
        get
        {
            return _PHONE_FORMATE;
        }
        set
        {
            _PHONE_FORMATE = value;
        }
    }
    private string _DONOT_ALLOW_TO_CREATE_BILL_WITHOUT_PATIENT_PHONE = "";
    public string DONOT_ALLOW_TO_CREATE_BILL_WITHOUT_PATIENT_PHONE
    {
        get
        {
            return _DONOT_ALLOW_TO_CREATE_BILL_WITHOUT_PATIENT_PHONE;
        }
        set
        {
            _DONOT_ALLOW_TO_CREATE_BILL_WITHOUT_PATIENT_PHONE = value;
        }
    }
    private string _DONOT_ALLOW_TO_CREATE_BILL_WITHOUT_LOCATION = "";
    public string DONOT_ALLOW_TO_CREATE_BILL_WITHOUT_LOCATION
    {
        get
        {
            return _DONOT_ALLOW_TO_CREATE_BILL_WITHOUT_LOCATION;
        }
        set
        {
            _DONOT_ALLOW_TO_CREATE_BILL_WITHOUT_LOCATION = value;
        }
    }
    private string _SZ_SHOW_ADD_TO_PREFERED_LIST = "";
    public string SZ_SHOW_ADD_TO_PREFERED_LIST
    {
        get
        {
            return _SZ_SHOW_ADD_TO_PREFERED_LIST;
        }
        set
        {
            _SZ_SHOW_ADD_TO_PREFERED_LIST = value;
        }
    }
    private string _SZ_ADD_SECONDARY_INSURANCE = "";
    public string SZ_ADD_SECONDARY_INSURANCE
    {
        get
        {
            return _SZ_ADD_SECONDARY_INSURANCE;
        }
        set
        {
            _SZ_ADD_SECONDARY_INSURANCE = value;
        }
    }

    private string _SZ_SHOW_ADD_TO_PREFERED_LIST_FOR_DIAGNOSIS_CODE = "";
    public string SZ_SHOW_ADD_TO_PREFERED_LIST_FOR_DIAGNOSIS_CODE
    {
        get
        {
            return _SZ_SHOW_ADD_TO_PREFERED_LIST_FOR_DIAGNOSIS_CODE;
        }
        set
        {
            _SZ_SHOW_ADD_TO_PREFERED_LIST_FOR_DIAGNOSIS_CODE = value;
        }
    }
    private string _SZ_SHOW_PATIENT_WALK_IN_ON_WORKAREA = "";
    public string SZ_SHOW_PATIENT_WALK_IN_ON_WORKAREA
    {
        get
        {
            return _SZ_SHOW_PATIENT_WALK_IN_ON_WORKAREA;
        }
        set
        {
            _SZ_SHOW_PATIENT_WALK_IN_ON_WORKAREA = value;
        }
    }
    private string _SZ_COPY_PATIENT_TO_TEST_FACILITY = "";
    //NIRMAL
    public string SZ_COPY_PATIENT_TO_TEST_FACILITY
    {
        get
        {
            return _SZ_COPY_PATIENT_TO_TEST_FACILITY;
        }
        set
        {
            _SZ_COPY_PATIENT_TO_TEST_FACILITY = value;
        }
    }
    private string _SZ_ALLOW_MODIFIER_TO_UPDATE_FOR_PROC_CODES = "";
    public string SZ_ALLOW_MODIFIER_TO_UPDATE_FOR_PROCEDURE
    {
        get
        {
            return _SZ_ALLOW_MODIFIER_TO_UPDATE_FOR_PROC_CODES;
        }
        set
        {
            _SZ_ALLOW_MODIFIER_TO_UPDATE_FOR_PROC_CODES = value;
        }
    }
    private string _SZ_HP1_Display = "";
    public string SZ_HP1_Display
    {
        get
        {
            return _SZ_HP1_Display;
        }
        set
        {
            _SZ_HP1_Display = value;
        }
    }

    private string _SZ_ALLOW_TO_EDIT_CODE_AMOUNT = "";
    public string SZ_ALLOW_TO_EDIT_CODE_AMOUNT
    {
        get
        {
            return _SZ_ALLOW_TO_EDIT_CODE_AMOUNT;
        }
        set
        {
            _SZ_ALLOW_TO_EDIT_CODE_AMOUNT = value;
        }
    }

    private string _SZ_ALLOW_HP1_SIGN = "";
    public string SZ_ALLOW_HP1_SIGN
    {
        get
        {
            return _SZ_ALLOW_HP1_SIGN;
        }
        set
        {
            _SZ_ALLOW_HP1_SIGN = value;
        }
    }

    private string _SZ_BILL_WITH_SECONDARY = "";
    public string SZ_BILL_WITH_SECONDARY
    {
        get
        {
            return _SZ_BILL_WITH_SECONDARY;
        }
        set
        {
            _SZ_BILL_WITH_SECONDARY = value;
        }
    }

    private string _SZ_MG2_Display = "";
    //Avinash
    public string SZ_MG2_Display
    {
        get
        {
            return _SZ_MG2_Display;
        }
        set
        {
            _SZ_MG2_Display = value;
        }
    }

    private string _SZ_Copy_From = "";
    public string SZ_Copy_From
    {
        get
        {
            return _SZ_Copy_From;
        }
        set
        {
            _SZ_Copy_From = value;
        }
    }
    //52
    private string _SZ_Dental_Display = "";
    public string SZ_Dental_Display
    {
        get
        {
            return _SZ_Dental_Display;
        }
        set
        {
            _SZ_Dental_Display = value;
        }
    }

    //pratik
    private string _SZ_ALLOW_HPJ1_SIGN = "";
    public string SZ_ALLOW_HPJ1_SIGN
    {
        get
        {
            return _SZ_ALLOW_HPJ1_SIGN;
        }
        set
        {
            _SZ_ALLOW_HPJ1_SIGN = value;
        }
    }

    private string _SZ_COMPANY_DEFAULT_VISIT_TYPE = "";
    public string SZ_COMPANY_DEFAULT_VISIT_TYPE
    {
        get
        {
            return _SZ_COMPANY_DEFAULT_VISIT_TYPE;
        }
        set
        {
            _SZ_COMPANY_DEFAULT_VISIT_TYPE = value;
        }
    }

    private string _SZ_DEFAULT_PAYMENT_FROM;
    public string SZ_DEFAULT_PAYMENT_FROM
    {
      get { return _SZ_DEFAULT_PAYMENT_FROM; }
      set { _SZ_DEFAULT_PAYMENT_FROM = value; }
    }

    private string _SZ_HIDE_REFFERING_OFFICE_AND_DOCTOR;
    public string SZ_HIDE_REFFERING_OFFICE_AND_DOCTOR
    {
        get { return _SZ_HIDE_REFFERING_OFFICE_AND_DOCTOR; }
        set { _SZ_HIDE_REFFERING_OFFICE_AND_DOCTOR = value; }
    }


    private string _SZ_SHOW_SINGLE_BILL;
    public string SZ_SHOW_SINGLE_BILL
    {
        get { return _SZ_SHOW_SINGLE_BILL; }
        set { _SZ_SHOW_SINGLE_BILL = value; }
    }

    private string _SZ_ADD_APPOINTMENT = "";
    public string SZ_ADD_APPOINTMENT
    {
        get
        {
            return _SZ_ADD_APPOINTMENT;
        }
        set
        {
            _SZ_ADD_APPOINTMENT = value;
        }
    }

    private string _SZ_ENABLE_CYCLIC_PROCEDURE_CODE = "";
    private string _SZ_ENABLE_CONTRACT_PDF_GENERATION = ""; 
    public string SZ_ENABLE_CYCLIC_PROCEDURE_CODE
    {
        get
        {
            return _SZ_ENABLE_CYCLIC_PROCEDURE_CODE;
        }
        set
        {
            _SZ_ENABLE_CYCLIC_PROCEDURE_CODE = value;
        }
    }
    public string SZ_ENABLE_CONTRACT_PDF_GENERATION
    {
        get
        {
            return _SZ_ENABLE_CONTRACT_PDF_GENERATION;
        }
        set
        {
            _SZ_ENABLE_CONTRACT_PDF_GENERATION = value;
        }
    }

    private string _IS_EMPLOYER;
    public string IS_EMPLOYER
    {
        get
        {
            return _IS_EMPLOYER;
        }
        set
        {
            _IS_EMPLOYER = value;
        }
    }
}
public class Bill_Sys_DocManagerObject
{
    private string _SZ_DOC_FILE = "";
    public string SZ_DOC_FILE
    {
        get
        {
            return _SZ_DOC_FILE;
        }
        set
        {
            _SZ_DOC_FILE = value;
        }
    }

}

[Serializable]
public class Bill_Sys_DocumentManagerObject
{
    private string _SZ_CASE_ID;
    public string SZ_CASE_ID
    {
        get
        {
            return _SZ_CASE_ID;
        }
        set
        {
            _SZ_CASE_ID = value;
        }
    }
    private string _SZ_CASE_NO;
    public string SZ_CASE_NO
    {
        get
        {
            return _SZ_CASE_NO;
        }
        set
        {
            _SZ_CASE_NO = value;
        }
    }
    private string _SZ_COMAPNY_ID;
    public string SZ_COMAPNY_ID
    {
        get
        {
            return _SZ_COMAPNY_ID;
        }
        set
        {
            _SZ_COMAPNY_ID = value;
        }
    }
    private string _SZ_LAWFIRM_ID;
    public string SZ_LAWFIRM_ID
    {
        get
        {
            return _SZ_LAWFIRM_ID;
        }
        set
        {
            _SZ_LAWFIRM_ID = value;
        }
    }
}

[Serializable]
public class Bill_Sys_CheckBO
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

    private string _SZ_DOCTOR_ID;
    public string SZ_DOCTOR_ID
    {
        get
        {
            return _SZ_DOCTOR_ID;
        }

        set
        {
            _SZ_DOCTOR_ID = value;
        }
    }

    private string _SZ_PROCEDURE_GROUP_ID;
    public string SZ_PROCEDURE_GROUP_ID
    {
        get
        {
            return _SZ_PROCEDURE_GROUP_ID;
        }

        set
        {
            _SZ_PROCEDURE_GROUP_ID = value;
        }
    }

    private string _SZ_PROCEDURE_GROUP;
    public string SZ_PROCEDURE_GROUP
    {
        get
        {
            return _SZ_PROCEDURE_GROUP;
        }

        set
        {
            _SZ_PROCEDURE_GROUP = value;
        }
    }

    private string _I_EVENT_ID;
    public string I_EVENT_ID
    {
        get
        {
            return _I_EVENT_ID;
        }

        set
        {
            _I_EVENT_ID = value;
        }
    }

    private string _SZ_PATIENT_NAME;
    public string SZ_PATIENT_NAME
    {
        get
        {
            return _SZ_PATIENT_NAME;
        }

        set
        {
            _SZ_PATIENT_NAME = value;
        }
    }

    private string _SZ_CASE_NO;
    public string SZ_CASE_NO
    {
        get
        {
            return _SZ_CASE_NO;
        }

        set
        {
            _SZ_CASE_NO = value;
        }
    }

    private string _SZ_INSURANCE_COMPANY;
    public string SZ_INSURANCE_COMPANY
    {
        get
        {
            return _SZ_INSURANCE_COMPANY;
        }

        set
        {
            _SZ_INSURANCE_COMPANY = value;
        }
    }

    private string _SZ_CLAIM_NO;
    public string SZ_CLAIM_NO
    {
        get
        {
            return _SZ_CLAIM_NO;
        }

        set
        {
            _SZ_CLAIM_NO = value;
        }
    }

    private string _SZ_CASE_ID;
    public string SZ_CASE_ID
    {
        get
        {
            return _SZ_CASE_ID;
        }

        set
        {
            _SZ_CASE_ID = value;
        }
    }

    private string _SZ_VISIT_DATE;
    public string SZ_VISIT_DATE
    {
        get
        {
            return _SZ_VISIT_DATE;
        }

        set
        {
            _SZ_VISIT_DATE = value;
        }
    }

    private string _DT_DATE_OF_ACCIDENT;
    public string DT_DATE_OF_ACCIDENT
    {
        get
        {
            return _DT_DATE_OF_ACCIDENT;
        }

        set
        {
            _DT_DATE_OF_ACCIDENT = value;
        }
    }

    private string _SZ_DOCTOR_NAME;
    public string SZ_DOCTOR_NAME
    {
        get
        {
            return _SZ_DOCTOR_NAME;
        }

        set
        {
            _SZ_DOCTOR_NAME = value;
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

    private string _DT_EVENT_TIME;
    public string DT_EVENT_TIME
    {
        get
        {
            return _DT_EVENT_TIME;
        }

        set
        {
            _DT_EVENT_TIME = value;
        }
    }

    private string _SZ_EVENT_NOTES;
    public string SZ_EVENT_NOTES
    {
        get
        {
            return _SZ_EVENT_NOTES;
        }

        set
        {
            _SZ_EVENT_NOTES = value;
        }
    }

    private string _DT_EVENT_TIME_TYPE;
    public string DT_EVENT_TIME_TYPE
    {
        get
        {
            return _DT_EVENT_TIME_TYPE;
        }

        set
        {
            _DT_EVENT_TIME_TYPE = value;
        }
    }

    private string _DT_EVENT_END_TIME;
    public string DT_EVENT_END_TIME
    {
        get
        {
            return _DT_EVENT_END_TIME;
        }

        set
        {
            _DT_EVENT_END_TIME = value;
        }
    }

    private string _DT_EVENT_END_TIME_TYPE;
    public string DT_EVENT_END_TIME_TYPE
    {
        get
        {
            return _DT_EVENT_END_TIME_TYPE;
        }

        set
        {
            _DT_EVENT_END_TIME_TYPE = value;
        }
    }

    private string _SZ_TYPE_CODE_ID;
    public string SZ_TYPE_CODE_ID
    {
        get
        {
            return _SZ_TYPE_CODE_ID;
        }

        set
        {
            _SZ_TYPE_CODE_ID = value;
        }
    }

    private string _DT_EVENT_DATE;
    public string DT_EVENT_DATE
    {
        get
        {
            return _DT_EVENT_DATE;
        }

        set
        {
            _DT_EVENT_DATE = value;
        }
    }

    private string _SZ_PATIENT_LAST_NAME;
    public string SZ_PATIENT_LAST_NAME
    {
        get
        {
            return _SZ_PATIENT_LAST_NAME;
        }

        set
        {
            _SZ_PATIENT_LAST_NAME = value;
        }
    }

    private string _SZ_PATIENT_FIRST_NAME;
    public string SZ_PATIENT_FIRST_NAME
    {
        get
        {
            return _SZ_PATIENT_FIRST_NAME;
        }

        set
        {
            _SZ_PATIENT_FIRST_NAME = value;
        }
    }

    private string _SZ_DOCTOR_NOTE;
    public string SZ_DOCTOR_NOTE
    {
        get
        {
            return _SZ_DOCTOR_NOTE;
        }

        set
        {
            _SZ_DOCTOR_NOTE = value;
        }
    }

    private string _DT_DATE;
    public string DT_DATE
    {
        get
        {
            return _DT_DATE;
        }

        set
        {
            _DT_DATE = value;
        }
    }

    private string _BIT_OF_SIGNPATH;
    public string BIT_OF_SIGNPATH
    {
        get
        {
            return _BIT_OF_SIGNPATH;
        }

        set
        {
            _BIT_OF_SIGNPATH = value;
        }
    }

    private string _SZ_SIGN_PATH;
    public string SZ_SIGN_PATH
    {
        get
        {
            return _SZ_SIGN_PATH;
        }

        set
        {
            _SZ_SIGN_PATH = value;
        }
    }

    private string _SZ_CHECKIN_USER_ID;
    public string SZ_CHECKIN_USER_ID
    {
        get
        {
            return _SZ_CHECKIN_USER_ID;
        }

        set
        {
            _SZ_CHECKIN_USER_ID = value;
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

    private string _BT_TREATMENT_CODE_97810;
    public string BT_TREATMENT_CODE_97810
    {
        get
        {
            return _BT_TREATMENT_CODE_97810;
        }

        set
        {
            _BT_TREATMENT_CODE_97810 = value;
        }
    }
    
    private string _BT_TREATMENT_CODE_97813;
    public string BT_TREATMENT_CODE_97813
    {
        get
        {
            return _BT_TREATMENT_CODE_97813;
        }

        set
        {
            _BT_TREATMENT_CODE_97813 = value;
        }
    }

    private string _BT_TREATMENT_CODE_97811;
    public string BT_TREATMENT_CODE_97811
    {
        get
        {
            return _BT_TREATMENT_CODE_97811;
        }

        set
        {
            _BT_TREATMENT_CODE_97811 = value;
        }
    }

    private string _BT_TREATMENT_CODE_97814;
    public string BT_TREATMENT_CODE_97814
    {
        get
        {
            return _BT_TREATMENT_CODE_97814;
        }

        set
        {
            _BT_TREATMENT_CODE_97814 = value;
        }
    }

    private string _bt_patient_reported;
    public string bt_patient_reported
    {
        get
        {
            return _bt_patient_reported;
        }

        set
        {
            _bt_patient_reported = value;
        }
    }

    private string _bt_patient_trated;
    public string bt_patient_trated
    {
        get
        {
            return _bt_patient_trated;
        }

        set
        {
            _bt_patient_trated = value;
        }
    }

    private string _bt_pain_grades;
    public string bt_pain_grades
    {
        get
        {
            return _bt_pain_grades;
        }

        set
        {
            _bt_pain_grades = value;
        }
    }

    private string _bt_head;
    public string bt_head
    {
        get
        {
            return _bt_head;
        }

        set
        {
            _bt_head = value;
        }
    }

    private string _bt_neck;
    public string bt_neck
    {
        get
        {
            return _bt_neck;
        }

        set
        {
            _bt_neck = value;
        }
    }

    private string _bt_throcic;
    public string bt_throcic
    {
        get
        {
            return _bt_throcic;
        }

        set
        {
            _bt_throcic = value;
        }
    }

    private string _bt_lumber;
    public string bt_lumber
    {
        get
        {
            return _bt_lumber;
        }

        set
        {
            _bt_lumber = value;
        }
    }

    private string _bt_rl_sh;
    public string bt_rl_sh
    {
        get
        {
            return _bt_rl_sh;
        }

        set
        {
            _bt_rl_sh = value;
        }
    }

    private string _bt_rl_wrist;
    public string bt_rl_wrist
    {
        get
        {
            return _bt_rl_wrist;
        }

        set
        {
            _bt_rl_wrist = value;
        }
    }

    private string _bt_rl_elow;
    public string bt_rl_elow
    {
        get
        {
            return _bt_rl_elow;
        }

        set
        {
            _bt_rl_elow = value;
        }
    }

    private string _bt_rl_hil;
    public string bt_rl_hil
    {
        get
        {
            return _bt_rl_hil;
        }

        set
        {
            _bt_rl_hil = value;
        }
    }

    private string _bt_rl_knee;
    public string bt_rl_knee
    {
        get
        {
            return _bt_rl_knee;
        }

        set
        {
            _bt_rl_knee = value;
        }
    }

    private string _bt_rl_ankle;
    public string bt_rl_ankle
    {
        get
        {
            return _bt_rl_ankle;
        }

        set
        {
            _bt_rl_ankle = value;
        }
    }

    private string _bt_patient_states;
    public string bt_patient_states
    {
        get
        {
            return _bt_patient_states;
        }

        set
        {
            _bt_patient_states = value;
        }
    }

    private string _bt_patient_states_little;
    public string bt_patient_states_little
    {
        get
        {
            return _bt_patient_states_little;
        }

        set
        {
            _bt_patient_states_little = value;
        }
    }

    private string _bt_patient_states_much;
    public string bt_patient_states_much
    {
        get
        {
            return _bt_patient_states_much;
        }

        set
        {
            _bt_patient_states_much = value;
        }
    }

    private string _bt_patient_tolerated;
    public string bt_patient_tolerated
    {
        get
        {
            return _bt_patient_tolerated;
        }

        set
        {
            _bt_patient_tolerated = value;
        }
    }

    private string _bt_patient_therapy;
    public string bt_patient_therapy
    {
        get
        {
            return _bt_patient_therapy;
        }

        set
        {
            _bt_patient_therapy = value;
        }
    }

    private string _sz_doctornote;
    public string sz_doctornote
    {
        get
        {
            return _sz_doctornote;
        }

        set
        {
            _sz_doctornote = value;
        }
    }
    private string _SZ_PROC_CODE;
    public string SZ_PROC_CODE
    {
        get
        {
            return _SZ_PROC_CODE;
        }

        set
        {
            _SZ_PROC_CODE = value;
        }
    }
}