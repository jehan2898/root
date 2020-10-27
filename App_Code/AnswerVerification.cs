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
/// Summary description for AnswerVerification
/// </summary>
public class AnswerVerification
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

	public AnswerVerification()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public void InsertPaymentImage(String _billNumber, string _companyId, string _imgId, string _userId, string _verificationID, string flag)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ANSWER_VERIFICATION_IMAGES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", _billNumber);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_IMAGE_ID", _imgId);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", _userId);
            sqlCmd.Parameters.AddWithValue("@I_VERIFICATION_ID", _verificationID);
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);
            sqlCmd.CommandTimeout = 0;
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