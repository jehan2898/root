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

public class Bill_Sys_Transfer_Report
{
    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;
    Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;

	public Bill_Sys_Transfer_Report()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet getLawfirmList(String p_szCompanyID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            comm = new SqlCommand("sp_get_lawfirm_list", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_company_id", p_szCompanyID);
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }

    public DataSet getTransferedCaseDetail(String p_szCompanyID, string sLawfirmIds, string fromdate, string todate)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            comm = new SqlCommand("get_tranfered_case_details", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_company_id", p_szCompanyID);
            if (fromdate != null && fromdate != "") { comm.Parameters.AddWithValue("@dt_from_date", fromdate); }
            if (todate != null && todate != "") { comm.Parameters.AddWithValue("@dt_to_date", todate); }
            comm.Parameters.AddWithValue("@sz_assigned_lawfirm_id", sLawfirmIds); 
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }
}