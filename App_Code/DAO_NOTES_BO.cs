using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DAO_NOTES_BO
/// </summary>
public class DAO_NOTES_BO
{

    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public DAO_NOTES_BO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public void SaveActivityNotes(DAO_NOTES_EO _notesEO)
    {
        XmlDocument doc = new XmlDocument();
        sqlCon = new SqlConnection(strConn);
        try
        {
            doc.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + "XML/ActivityNotesXML.xml");
            XmlNodeList nl = doc.SelectNodes("NOTES/" + _notesEO.SZ_MESSAGE_TITLE + "/MESSAGE");
            string strMessage = _notesEO.SZ_ACTIVITY_DESC + " " + nl.Item(0).InnerText;


            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_NOTES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", _notesEO.SZ_CASE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", _notesEO.SZ_USER_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
            sqlCmd.Parameters.AddWithValue("@IS_DENIED", _notesEO.IS_DENIED);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _notesEO.SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@FLAG","ADD");
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

    
    
}
public class DAO_NOTES_EO
{
    
    private string _SZ_MESSAGE_TITLE = "";
    public string SZ_MESSAGE_TITLE
    {
        get
        {
            return _SZ_MESSAGE_TITLE;
        }
        set
        {
            _SZ_MESSAGE_TITLE = value;
        }
    }

    private string _SZ_MESSAGE_DESC="";
    public string SZ_MESSAGE_DESC
    {
        get
        {
            return _SZ_MESSAGE_DESC;
        }
        set
        {
            _SZ_MESSAGE_DESC = value;
        }
    }

    private string _SZ_ACTIVITY_DESC = "";
    public string SZ_ACTIVITY_DESC 
    {
        get
        {
            return _SZ_ACTIVITY_DESC;
        }
        set
        {
            _SZ_ACTIVITY_DESC = value;
        }
    }


    private string _SZ_USER_ID = "";
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

    private string _SZ_CASE_ID = "";
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


    private bool _IS_DENIED=false;
    public bool IS_DENIED
    {
        get
        {
            return _IS_DENIED;
        }
        set
        {
            _IS_DENIED = value;
        }
    }

    private string _SZ_COMPANY_ID;
    public string SZ_COMPANY_ID
    {
        get { return _SZ_COMPANY_ID; }
        set { _SZ_COMPANY_ID = value; }
    }

}
