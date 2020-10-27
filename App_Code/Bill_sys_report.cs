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
using System.Data.Sql;
using System.Collections;

/// <summary>
/// Summary description for Bill_sys_report
/// </summary>
public class Bill_sys_report
{
    string strcon;
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    
    public Bill_sys_report()
	{
        strcon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}


    public DataSet getData(string companyId, string val)
    {
       try
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            cmd = new SqlCommand("sp_get_reports", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@sz_company_id", companyId);
            cmd.Parameters.Add("@flag", val);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return ds;

    }


    public DataSet getColumnData(string val)
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            cmd = new SqlCommand("sp_get_columnData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@sz_company_id", companyId);
            cmd.Parameters.Add("@flag", val);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return ds;

    }


    public DataSet geIntialRevalCases(ArrayList arrList)
    {
        try
        {

            con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            cmd = new SqlCommand("SP_GET_INITIAL_RE_EVAL_CASES", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add("@SZ_PROCEDURE_GROUP_ID", arrList[0].ToString());
            cmd.Parameters.Add("@SZ_COMPANY_ID", arrList[1].ToString());
            cmd.Parameters.Add("@SZ_INSURANCE_ID", arrList[2].ToString());
            cmd.Parameters.Add("@SZ_OFFICE_ID", arrList[3].ToString());
            cmd.Parameters.Add("@SZ_CASE_TYPE_ID", arrList[4].ToString());
            cmd.Parameters.Add("@SZ_START_DATE", arrList[5].ToString());
            cmd.Parameters.Add("@SZ_END_DATE", arrList[6].ToString());
            cmd.Parameters.Add("@SZ_TYPE", arrList[7].ToString());
            cmd.Parameters.Add("@SZ_CASE_STATUS_ID", arrList[8].ToString());
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return ds;

    }


}
