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
/// Summary description for LocationBO
/// </summary>
public class LocationBO
{
    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
	public LocationBO()
	{

        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
		
	}
    public DataSet Location(string sz_location_id, string sz_company_id)
    {
        SqlCommand comm;
        SqlConnection conn = null;
        SqlDataAdapter sqlda;
        DataSet ds = new DataSet();
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
            
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }
}
