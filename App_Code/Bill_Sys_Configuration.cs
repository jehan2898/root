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


public class Bill_Sys_Configuration
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

    public Bill_Sys_Configuration()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public string getConfigurationSettings(string p_szCompanyID,string p_szFlag)
    {
        sqlCon = new SqlConnection(strConn);
        SqlDataReader dr;
        String szConfigValue = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_CONFIG_INFORMATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", p_szFlag);
            dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                szConfigValue = dr["VALUE"].ToString();
            }

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
        return szConfigValue;
    }




    public int SaveConfig( ArrayList arrList)
    {
        int iReturn = 0;
        //SqlParameter sqlParam = new SqlParameter();
        Bill_Sys_Configobject _objBill_Sys_Configobject;
        sqlCon = new SqlConnection(strConn);

        for (int i = 0; i < arrList.Count; i++)
        {
            _objBill_Sys_Configobject = (Bill_Sys_Configobject)arrList[i];
            try
            {
                sqlCon.Open();
                sqlCmd = new SqlCommand("SP_SAVE_CONFIGURATION", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _objBill_Sys_Configobject.sz_companyid);
                sqlCmd.Parameters.AddWithValue("@SZ_POINT", _objBill_Sys_Configobject.sz_point);
                sqlCmd.Parameters.AddWithValue("@BT_VISIBLE", _objBill_Sys_Configobject.bt_visible);
                sqlCmd.ExecuteNonQuery();
                iReturn = 1;
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

        return iReturn;

    }



   







}




public class Bill_Sys_Configobject
{
    private string _i_configid;
    public string i_configid
    {
        get
        {
            return _i_configid;
        }
        set
        {
            _i_configid = value;
        }
    }


    private string _sz_companyid;
    public string sz_companyid
    {
        get
        {
            return _sz_companyid;
        }
        set
        {
            _sz_companyid = value;
        }
    }
    private string _sz_point;
    public string sz_point
    {
        get
        {
            return _sz_point;
        }
        set
        {
            _sz_point = value;
        }
    }
    private string _sz_default_value;
    public string sz_default_value
    {
        get
        {
            return _sz_default_value;
        }
        set
        {
            _sz_default_value = value;
        }
    }
    private string _bt_visible;
    public string bt_visible
    {
        get
        {
            return _bt_visible;
        }
        set
        {
            _bt_visible = value;
        }
    }




}
