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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


public class Bill_Sys_Balance_Report
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

	public Bill_Sys_Balance_Report()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet GetBalanceReportData(ArrayList ar)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_balance_report", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ar[0].ToString());
            if (ar[1].ToString() != "" && ar[1].ToString() != null) { sqlCmd.Parameters.AddWithValue("@dt_from_date", ar[1].ToString()); }
            if (ar[2].ToString() != "" && ar[2].ToString() != null) { sqlCmd.Parameters.AddWithValue("@dt_to_date", ar[2].ToString()); }
            if (ar[3].ToString() != "" && ar[3].ToString() != null && ar[3].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@sz_case_type_id", ar[3].ToString()); }
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