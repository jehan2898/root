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
/// Summary description for Bill_Sys_NF3_Template
/// </summary>
public class LitigationDeskBO
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

    public LitigationDeskBO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public void saveLegalCases(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_LEAGAL_CASES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@BT_TRANSFER", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_LAWFIRM_ID", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TRANSFER_DATE", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[5].ToString()); 
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();

        }
        catch (SqlException ex)
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
    }


    public DataSet  GetLitigatedBills(string P_case_id ,string P_company_id)
    {
        sqlCon = new SqlConnection(strConn);
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_LITIGATED_BILLS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", P_case_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID",P_company_id);
            da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
        }
        catch (SqlException ex)
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
