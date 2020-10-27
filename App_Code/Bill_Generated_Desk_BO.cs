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
/// Summary description for Bill_Generated_Desk_BO
/// </summary>
public class Bill_Generated_Desk_BO
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

    public Bill_Generated_Desk_BO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataSet getBillGeneratedDesk(string p_szBillID,string p_szCaseID,string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        SqlDataReader dr;
        String szConfigValue = "";
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GENEREATED_BILL_DESK", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillID);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_GENERATED_BILL_DESK");
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
