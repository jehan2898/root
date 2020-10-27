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
/// Summary description for Bill_Sys_Menu
/// </summary>
/// 
public class Bill_Sys_Menu
{

    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;

    SqlDataReader dr;

	public Bill_Sys_Menu()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public ArrayList GetMenuList(String userRoleID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_CHECK_MENU";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_ROLE_ID", userRoleID);
            comm.Parameters.AddWithValue("@FLAG", "GETMENULIST");
           

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                _return.Add(Convert.ToString(dr[0]));
                _return.Add(Convert.ToString(dr[1]));
                _return.Add(Convert.ToString(dr[2]));

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


    public ArrayList GetMenuID(string parentID)
    {
        conn = new SqlConnection(strConn);
        try
        {

            conn.Open();
            ArrayList _return = new ArrayList();

            comm = new SqlCommand("SP_MST_MENU", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@I_PARENT_ID", parentID);
            comm.Parameters.AddWithValue("@FLAG", "GET_MENU_CODE");

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                _return.Add(Convert.ToString(dr[0]));
                _return.Add(Convert.ToString(dr[1]));
            }
            return _return;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return null;
    }


    public void DeleteTxnMenuRole(string p_strRoleID)
    {
        conn = new SqlConnection(strConn);
        try
        {

            conn.Open();
            comm = new SqlCommand("SP_TXN_USER_ACCESS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_USER_ROLE_ID", p_strRoleID);
            comm.Parameters.AddWithValue("@FLAG", "DELETE");

            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }


    public void SaveTxnMenuRole(string p_strRoleID,int i_menu_id)
    {
        conn = new SqlConnection(strConn);
        try
        {

            conn.Open();
            comm = new SqlCommand("SP_TXN_USER_ACCESS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_USER_ROLE_ID", p_strRoleID);
            comm.Parameters.AddWithValue("@I_MENU_ID", i_menu_id);
            comm.Parameters.AddWithValue("@FLAG", "ADD");

            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }

    public DataSet GetRoleList()
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            ArrayList _return = new ArrayList();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_USER_ROLES";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@FLAG", "GET_USER_ROLE_LIST");

            da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds);
            return ds;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }


    public DataSet GetMasterMenu()
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_MENU";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@FLAG", "GETMASTERMENU");

            da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds);
            return ds;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet GetRoleList(String p_szCompanyID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_USER_ROLES";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GET_USER_ROLE_LIST");

            da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }


    public DataSet GetMainMenuList(String p_szCompanyID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_MST_MAIN_MENU_LIST";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@FLAG", "GET_MST_MAIN_MENU_LIST");

            da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }


    public DataSet GetChildMenu(int p_imenuid)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_MENU";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_MENU_ID", p_imenuid);
            comm.Parameters.AddWithValue("@FLAG", "GETCHILDMENU");

            da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds);
            return ds;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet GetSelectedMenu(String p_strRollID)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_USER_ACCESS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_USER_ROLE_ID", p_strRollID);
            comm.Parameters.AddWithValue("@FLAG", "GETSELECTMENU");

            da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds);
            return ds;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }
    public decimal GetICcodeAmount(string iccodeid)
    {
        decimal _return = 0;
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();

            comm = new SqlCommand("SP_MST_IC9_CODE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@SZ_IC9_ID", iccodeid);
            comm.Parameters.AddWithValue("@FLAG", "GET_IC_CODE_AMOUNT");

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                _return = Convert.ToDecimal(dr[0]);

            }
            return _return;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return 0;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return 0;
    }
    public DataSet GetICCode()
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_IC9_CODE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@FLAG", "GET_IC9CODE_LIST");
            da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }
}
