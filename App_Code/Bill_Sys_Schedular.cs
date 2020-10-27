using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Bill_Sys_Calender
/// </summary>
public class Bill_Sys_Schedular
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    public Bill_Sys_Schedular()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataTable GET_EVENT_DETAIL(string sz_company_id,DateTime _date,decimal dc_interval,string sz_referring)
    {
        try
        {
            decimal _value = new decimal();
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand("GET_ROOM_DETAILS_TEMP", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            comm.Parameters.AddWithValue("@DT_DATE", _date);
            comm.Parameters.AddWithValue("@I_INTERVAL", dc_interval);
            comm.Parameters.AddWithValue("@SZ_REFERRING_ID", sz_referring);
            
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return ds.Tables[0];
    }
    
    

}


