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

/// <summary>
/// Summary description for Bill_Sys_GetDocumentTypeTree
/// </summary>
public class Bill_Sys_GetDocumentTypeTree
{
    SqlConnection conn;
    String strConn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

	public Bill_Sys_GetDocumentTypeTree()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public DataSet GetMasterNodes()
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_GetDocumentTypeTree";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@FLAG", "GETMASTERNODE");
            da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }
    public DataSet GetChildNodes(int p_inodeid)
    {
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_GetDocumentTypeTree";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_PARENT_ID", p_inodeid);
            comm.Parameters.AddWithValue("@FLAG", "GETCHILDNODE");
            da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }



    public string GetNodeId(string szCompanyId,string szNodeTYpe)
    {
        string szNodeID = "";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_NODE_ID_USING_NODE_TYPE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            comm.Parameters.AddWithValue("@SZ_NODE_TYPE", szNodeTYpe);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szNodeID = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szNodeID;
    }


    public void AddAll(string szCompanyId, string szNodeID,string szProcedureGorupId,string order,string szNodeName)
    {
       
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_ADD_TO_ALL";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_NODE_ID", szNodeID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szProcedureGorupId);
            comm.Parameters.AddWithValue("@I_ORDER", order);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            comm.Parameters.AddWithValue("@SZ_NODE_NAME", szNodeName);
            comm.ExecuteNonQuery();
          
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        
    }

    public void ReMoveAll(string szCompanyId, string szNodeID)
    {

        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_REMOVE_TO_ALL";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_NODE_ID", szNodeID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
           
            comm.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

    }

    public DataSet GeALLtNodeId(string szCompanyId)
    {
        
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_ASSIGNED_DOC_TO_SPECIALITY_TRANSFER_ALL";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
        return null;
    }






}
