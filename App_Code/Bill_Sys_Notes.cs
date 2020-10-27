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


public class Bill_Sys_Notes
{
    private int _I_NOTE_ID = 0;
    public int I_NOTE_ID
    {
        get
        {
            return _I_NOTE_ID;
        }
        set
        {
            _I_NOTE_ID = value;
        }
    }
    private string _SZ_NOTE_CODE = string.Empty;
    public string SZ_NOTE_CODE
    {
        get
        {
            return _SZ_NOTE_CODE;
        }
        set
        {
            _SZ_NOTE_CODE = value;
        }
    }

    private string _SZ_COMPANY_ID = string.Empty;
    public string SZ_COMPANY_ID
    {
        get
        {
            return _SZ_COMPANY_ID;
        }
        set
        {
            _SZ_COMPANY_ID = value;
        }
    }

    private string _SZ_CASE_ID = string.Empty;
    public string SZ_CASE_ID
    {
        get
        {
            return _SZ_CASE_ID;
        }
        set
        {
            _SZ_CASE_ID = value;
        }
    }

    private string _SZ_USER_ID = string.Empty;
    public string SZ_USER_ID
    {
        get
        {
            return _SZ_USER_ID;
        }
        set
        {
            _SZ_USER_ID = value;
        }
    }

    private DateTime _DT_ADDED = System.DateTime.Today;
    public DateTime DT_ADDED
    {
        get
        {
            return _DT_ADDED;
        }
        set
        {
            _DT_ADDED = value;
        }
    }

    private string _SZ_NOTE_TYPE = string.Empty;
    public string SZ_NOTE_TYPE
    {
        get
        {
            return _SZ_NOTE_TYPE;
        }
        set
        {
            _SZ_NOTE_TYPE = value;
        }
    }

    private string _SZ_NOTE_DESCRIPTION = string.Empty;
    public string SZ_NOTE_DESCRIPTION
    {
        get
        {
            return _SZ_NOTE_DESCRIPTION;
        }
        set
        {
            _SZ_NOTE_DESCRIPTION = value;
        }
    }


    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
	public Bill_Sys_Notes()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public void SaveNotes()
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("TXN_NOTES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            //sqlParam = sqlCmd.Parameters.Add("@Return", SqlDbType.Int);
           // sqlParam.Direction = ParameterDirection.ReturnValue;

            //sqlCmd.Parameters.AddWithValue("@I_NOTE_ID", "");
            if (SZ_NOTE_CODE != "") { sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", SZ_NOTE_CODE); }
            if (SZ_COMPANY_ID != "") { sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID); }
            if (SZ_CASE_ID != "") { sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", SZ_CASE_ID); }
            if (SZ_USER_ID != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", SZ_USER_ID); }
            sqlCmd.Parameters.AddWithValue("@DT_ADDED", DT_ADDED);
            if (SZ_NOTE_TYPE != "") { sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", SZ_NOTE_TYPE); }
            if (SZ_NOTE_DESCRIPTION != "") { sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", ""); }
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();

            //return Convert.ToInt32(sqlCmd.Parameters["@Return"].Value);
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

    public DataSet GetNotes(string szCompanyId,string szNotesType,string szFromDate ,string szTodate )
    {
        //SqlParameter sqlParam = new SqlParameter();
        DataSet dsReturn = new DataSet();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_NOTES_BY_DATE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
           
            if (szCompanyId != "") { sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId); }
            if (szNotesType != "") { sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", szNotesType); }

            if (szFromDate != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", szFromDate); }
            if (szTodate != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", szTodate); }

            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(dsReturn);

          
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
        return dsReturn;

    }
}
