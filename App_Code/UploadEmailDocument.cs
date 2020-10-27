using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

/// <summary>
/// Summary description for UploadEmailDocument
/// </summary>
public class UploadEmailDocument
{
    string strcon;
    SqlConnection sqlcon;
    SqlCommand sqlcmd;
    SqlDataAdapter da;
    DataSet ds;
    public UploadEmailDocument()
    {
        strcon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public void SaveEmailSpecification(ArrayList _objarr, string _emailId)
    {
        try
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            foreach (UploadDoumentSpecification _objdoc in _objarr)
            {                    
                sqlcmd = new SqlCommand("sp_save_mst_notification_email_upload_document", sqlcon);
                sqlcmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@i_node_id", _objdoc.NodeID);
                sqlcmd.Parameters.AddWithValue("@sz_node_Description", _objdoc.DescriptionNode.Trim());
                sqlcmd.Parameters.AddWithValue("@sz_mail_id", _emailId.Trim());
                sqlcmd.Parameters.AddWithValue("@sz_lawfirm_id", _objdoc.LawFirmID.Trim());
                sqlcmd.Parameters.AddWithValue("@sz_company_id", _objdoc.CompanyID.Trim());
                sqlcmd.Parameters.AddWithValue("@sz_user_id", _objdoc.UserID.Trim());
                sqlcmd.Parameters.AddWithValue("@I_order_id", _objdoc.orderID);
                sqlcmd.Parameters.AddWithValue("@flag", "ADD");

                sqlcmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlcon.State == ConnectionState.Open)
            {
                sqlcon.Close();
            }
        }



    }
    public DataSet GetDataUploadDocument(string sz_company_id, string sz_lawfirmId)
    {
        try
        {
            sqlcon = new SqlConnection(strcon);
            sqlcon.Open();
            sqlcmd = new SqlCommand("sp_save_mst_notification_email_upload_document", sqlcon);
            sqlcmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@sz_lawfirm_id", sz_lawfirmId);
                sqlcmd.Parameters.AddWithValue("@sz_company_id", sz_company_id);
                sqlcmd.Parameters.AddWithValue("@flag", "GET");
                da = new SqlDataAdapter(sqlcmd);
                ds = new DataSet();
                da.Fill(ds);
                
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlcon.State == ConnectionState.Open)
            {
                sqlcon.Close();
            }
        }
        return ds;

    }

    public void DeleteEmailSpecification(string sz_company_id, string sz_lawfirmId)
    {
        try
        {
                sqlcon = new SqlConnection(strcon);
                sqlcon.Open();
                sqlcmd = new SqlCommand("sp_save_mst_notification_email_upload_document", sqlcon);
                sqlcmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@sz_lawfirm_id", sz_lawfirmId);
                    sqlcmd.Parameters.AddWithValue("@sz_company_id", sz_company_id);
                    sqlcmd.Parameters.AddWithValue("@flag", "DEL");
                    sqlcmd.ExecuteNonQuery();
                    
                
        }
           catch (Exception ex)
           {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
           

        
    }



}


public class UploadDoumentSpecification
{
    private int _nodeid;
    public int NodeID
    {
        get
        {
            return _nodeid;
        }
        set
        {
            _nodeid = value;
        }

    }

    private string _companyID;
    public string CompanyID
    {
        get
        {
            return _companyID;
        }
        set
        {
            _companyID = value;
        }
    }

    private string _nodeDescription;
    public string DescriptionNode
    {
        get
        {
            return _nodeDescription;
        }
        set
        {
            _nodeDescription = value;
        }
    }
    private string _lawFirmid;
    public string LawFirmID
    {
        get
        {
            return _lawFirmid;
        }
        set
        {
            _lawFirmid = value;
        }
    }
    private string _userid;
    public string UserID
    {
        get
        {
            return _userid;
        }
        set
        {
            _userid = value;
        }
    }
    private int _order;
    public int orderID
    {
        get
        {
            return _order;
        }
        set
        {
            _order = value;
        }
    }
}



