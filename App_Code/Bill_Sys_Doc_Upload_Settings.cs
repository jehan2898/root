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

/// <summary>
/// Summary description for Bill_Sys_Doc_Upload_Settings
/// </summary>
public class Bill_Sys_Doc_Upload_Settings
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
	public Bill_Sys_Doc_Upload_Settings()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public DataSet getDocType(string szCompanyID,string szDocumentSource)
    {
       
        conn = new SqlConnection(strConn);
        conn.Open();
        DataSet ds = new DataSet();
        try
        {
            comm = new SqlCommand("GET_DOCUMENT_TYPE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            comm.Parameters.AddWithValue("@SZ_DOCUMENT_SOURCE", szDocumentSource);
            da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;

    }

    public Boolean  SaveUploadDoc(string szCompanyID, string szDocumentSource,string szDocType,string szSpeciality,string szNodeType)
    {

        conn = new SqlConnection(strConn);
        conn.Open();
        DataSet ds = new DataSet();
        try
        {
            comm = new SqlCommand("SP_SAVE_UPLOAD_DOCUMENT_TYPES", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_DOCUMENT_SOURCE", szDocumentSource);
            comm.Parameters.AddWithValue("@SZ_DOCUMENT_TYPE", szDocType);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szSpeciality);
            comm.Parameters.AddWithValue("@SZ_NODE_TYPE", szNodeType);
            da = new SqlDataAdapter(comm);
            da.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return true;
        //Method End

    }

    public string GetNodeId(string szCompanyID, string szDocumentSource,string szDocType,string szSpeciality)
    {

        conn = new SqlConnection(strConn);
        conn.Open();
        string szReturn = "";
        try
        {
            comm = new SqlCommand("SP_GET_DOCUMENT_NODE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_DOCUMENT_SOURCE", szDocumentSource);
            comm.Parameters.AddWithValue("@SZ_DOCUMENT_TYPE", szDocType);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szSpeciality);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
            }
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }

        return szReturn;
        //Method End


    }
}
