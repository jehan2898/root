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
/// Summary description for Bill_Sys_Attorny
/// </summary>
public class Bill_Sys_Attorny
{

    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;

    SqlDataReader dr;
	public Bill_Sys_Attorny()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet SearchRecord(string sz_company_id, string sz_att_first_name)
    {
        conn = new SqlConnection();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_ATTORNEY", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@SZ_ATTORNEY_FIRST_NAME", sz_att_first_name);
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            cmd.Parameters.AddWithValue("@FLAG", "SEARCH");

            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
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
        return ds;
        //Method End


    }
}