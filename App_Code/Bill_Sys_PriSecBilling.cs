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
using log4net;
using System.Xml;

/// <summary>
/// Summary description for Bill_Sys_PriSecBilling
/// </summary>
public class Bill_Sys_PriSecBilling
{
    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;
    private static ILog log = LogManager.GetLogger("Bill_Sys_PriSecBilling");

	public Bill_Sys_PriSecBilling()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet GetInsuranceDetails(string CaseID)
    {
        DataSet ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();

            comm = new SqlCommand("SP_GET_PRI_SEC_INSURANCE_DETAILS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", CaseID);
            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
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
    }
}