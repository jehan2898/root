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

/// <summary>
/// Summary description for Bill_Sys_Envelope_Config
/// </summary>
/// //Bill_Sys_Envelope_Config.Savecompanyinfo(szcompanyid
public class Bill_Sys_Envelope_Config
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public Bill_Sys_Envelope_Config()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
       
    }
    public int  Savecompanyinfo(string szcompanyid, string szaddstree1, string szaddstree2, string szcity, string szzip, string szstate, string szcompanyname ,string szenvelopename)
    {
        sqlCon = new SqlConnection(strsqlCon);
        int iReturn = 0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SAVE_ENVELOPE_CONFIGURATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompanyid);
            sqlCmd.Parameters.AddWithValue("@BT_ADDRESS_STREET1", szaddstree1);
            sqlCmd.Parameters.AddWithValue("@BT_ADDRESS_STREET2", szaddstree2);
            sqlCmd.Parameters.AddWithValue("@BT_ADDRESS_CITY", szcity);
            sqlCmd.Parameters.AddWithValue("@BT_ADDRESS_ZIP", szzip);
            sqlCmd.Parameters.AddWithValue("@BT_ADDRESS_STATE", szstate);
            sqlCmd.Parameters.AddWithValue("@BT_COMPANY_NAME", szcompanyname);
            sqlCmd.Parameters.AddWithValue("@SZ_ENVELOPE_DISPLAY_NAME", szenvelopename);
            iReturn = sqlCmd.ExecuteNonQuery();
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

        return iReturn;
    }

    public DataSet Getenvelopeconfig(string szCompanyId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_ENVELOPE_CONFIGURATION", sqlCon);
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

    public DataSet GetenvelopeconfigFillRec(string szCompanyId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_ENVELOPE_Company", sqlCon);
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

    public int SavecompanyinfoNew(string szcompanyid, string szaddstree1, string szaddstree2, string szcity, string szzip, string szstate, string szcompanyname, string SZ_txtcusName, string SZ_txtcusAddress, string SZ_txtcusCity, string SZ_txtcusState, string SZ_txtcusZip,string flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        int iReturn = 0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SAVE_ENVELOPE_CONFIGURATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompanyid);
            sqlCmd.Parameters.AddWithValue("@BT_ADDRESS_STREET1", szaddstree1);
            sqlCmd.Parameters.AddWithValue("@BT_ADDRESS_STREET2", szaddstree2);
            sqlCmd.Parameters.AddWithValue("@BT_ADDRESS_CITY", szcity);
            sqlCmd.Parameters.AddWithValue("@BT_ADDRESS_ZIP", szzip);
            sqlCmd.Parameters.AddWithValue("@BT_ADDRESS_STATE", szstate);
            sqlCmd.Parameters.AddWithValue("@BT_COMPANY_NAME", szcompanyname);
           
            sqlCmd.Parameters.AddWithValue("@sz_display_name", SZ_txtcusName);
            sqlCmd.Parameters.AddWithValue("@sz_display_street", SZ_txtcusAddress);
            sqlCmd.Parameters.AddWithValue("@sz_display_city", SZ_txtcusCity);
            sqlCmd.Parameters.AddWithValue("@sz_display_state", SZ_txtcusState);
            sqlCmd.Parameters.AddWithValue("@sz_display_zip", SZ_txtcusZip);
            sqlCmd.Parameters.AddWithValue("@flag", flag);
            iReturn = sqlCmd.ExecuteNonQuery();
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

        return iReturn;
    }
}
