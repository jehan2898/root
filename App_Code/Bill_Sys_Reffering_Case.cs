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
/// Summary description for Bill_Sys_Reffering_Case
/// </summary>
public class Bill_Sys_Reffering_Case
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

	public Bill_Sys_Reffering_Case()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public void saveRefferingInformation(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("update_case_for_reffering_information", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@sz_case_id", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[2].ToString());
            if (objAL[3].ToString() != "NA")
                sqlCmd.Parameters.AddWithValue("@sz_reffering_office_id", objAL[3].ToString());

            if (objAL[4].ToString() != "NA")
                sqlCmd.Parameters.AddWithValue("@sz_reffering_doctor_id", objAL[4].ToString());
            
            sqlCmd.ExecuteNonQuery();

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
    }
}