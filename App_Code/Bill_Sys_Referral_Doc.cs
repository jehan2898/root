using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Bill_Sys_Referral_Doc
/// </summary>
public class Bill_Sys_Referral_Doc
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;
    SqlDataReader dr;
	public Bill_Sys_Referral_Doc()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public DataSet BindReferralDoc(string p_szCompanyId,string p_sz_CaseId)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_REFERRAL_DOC", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDETAIL");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_sz_CaseId);
            sqlCmd.CommandTimeout = 0;
            sqlda = new SqlDataAdapter(sqlCmd);
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
