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
using System.IO; 

public class Bill_Sys_Document_Manager
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public Bill_Sys_Document_Manager()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public DataSet GetDocumentmanagerInfo(string szCaseId,string szCompanyId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_documnet_manager_info", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@CaseID", szCaseId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
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
    public DataSet GetDocumentmanagerMerge()
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_SELECTED_PDF_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
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
    //public string GetNodeTyefromtbltags(string NodeID)
    //{

    //    sqlCon = new SqlConnection(strsqlCon);
    //    string NodeTYpe = "";
    //    try
    //    {
    //        sqlCon.Open();
    //        sqlCmd = new SqlCommand("SP_Get_NodeTypefROMtBLtAGS", sqlCon);
    //        sqlCmd.CommandType = CommandType.StoredProcedure;
    //        sqlCmd.Parameters.AddWithValue("@NODEID", NodeID);
    //        dr = sqlCmd.ExecuteReader();
    //        while (dr.Read())
    //        {
    //            if (dr["NodeType"] != DBNull.Value) { NodeTYpe = dr["NodeType"].ToString(); }
    //        }

    //    }
    //    catch (SqlException ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //    finally
    //    {
    //        if (sqlCon.State == ConnectionState.Open)
    //        {
    //            sqlCon.Close();
    //        }
    //    }

    //    return NodeTYpe;
    //}

    public void deletenode(string Imageid)
    {

        sqlCon = new SqlConnection(strsqlCon);
       
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DELETE_NODE_FOR_DOCUMENT_MANAGER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Imageid", Imageid);
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

    public int getlogical(string sz_case_id, string sz_filename,string szCompanyID)
    {
        int i = 0;
        sqlCon = new SqlConnection(strsqlCon);
        try
        {

            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "SP_SAVE_MERGED_PDF_IN_MGR";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Parameters.AddWithValue("@p_szCaseID", sz_case_id);
            sqlCmd.Parameters.AddWithValue("@p_szFileName", sz_filename);
            sqlCmd.Parameters.AddWithValue("@p_szCompanyID", szCompanyID);
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
        return i;


    }

    public string GetPathFromNodeId(string NodeID)
    {

        sqlCon = new SqlConnection(strsqlCon);
        string nodepath = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_FULL_PATH", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_NODEID", NodeID);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                nodepath = dr[0].ToString(); 
            }

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

        return nodepath;
    }

    public string SaveFileInDOc(string NodeID,string FileName,string FilePath,string UserName)
    {

        sqlCon = new SqlConnection(strsqlCon);
        string nodepath = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ADD_FILE_IN_DOC_MANAGER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@NODE_ID", NodeID);
            sqlCmd.Parameters.AddWithValue("@FILE_NAME", FileName);
            sqlCmd.Parameters.AddWithValue("@FILE_PATH", FilePath);
            sqlCmd.Parameters.AddWithValue("@USER_NAME", UserName);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                nodepath = dr[0].ToString();
            }

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

        return nodepath;
    }




    [Serializable]
    public class Bill_Sys_Document_ManagerDAO
    {
       public string _FILE_NAME = string.Empty;
        public string FILE_NAME 
        {
            get
            {
                return _FILE_NAME;
            }
            set
            {
                _FILE_NAME= value;
            }
        }

        public string _FILE_PATH = string.Empty;
        public string FILE_PATH 
        {
            get
            {
                return _FILE_PATH;
            }
            set
            {
                _FILE_PATH= value;
            }
        }

    }

  
    

}
