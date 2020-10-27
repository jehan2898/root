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
/// Summary description for Bill_Sys_Doctor_Summary_Report
/// </summary>
public class Bill_Sys_Doctor_Summary_Report
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    public Bill_Sys_Doctor_Summary_Report()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public DataSet Get_Doctor_Summary_Report(string szcompayid, string fromDate, string toDate)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_DOCTOR_REPORT", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_company_id", szcompayid);
            if (fromDate != string.Empty)
                comm.Parameters.Add("@DT_FROM", SqlDbType.NVarChar).Value = fromDate;
            if (toDate != string.Empty)
                comm.Parameters.Add("@dt_to", SqlDbType.NVarChar).Value = toDate;
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

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
        return dsResult;
    }

    public DataSet Get_Details_Summary_Report(string szcompayid, string fromDate, string toDate)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("SP_GET_INSURANCE_DOCTOR_REPORT", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_company_id", szcompayid);
            if (fromDate != string.Empty)
                comm.Parameters.Add("@DT_FROM", SqlDbType.NVarChar).Value = fromDate;
            if (toDate != string.Empty)
                comm.Parameters.Add("@dt_to", SqlDbType.NVarChar).Value = toDate;
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return dsResult;
    }
}
