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
/// Summary description for BindGrid
/// </summary>
public class GeneratePDFBO
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

    public GeneratePDFBO()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public String GeneratePDF(String p_szCaseID, String szXMLFileName, String szDefaultPDFFileName)
    {
        String szPDFFileName = "";
        try
        {

            PDFValueReplacement.PDFValueReplacement _obj = new PDFValueReplacement.PDFValueReplacement();
            szPDFFileName = _obj.ReplacePDFvalues(szXMLFileName, szDefaultPDFFileName, p_szCaseID, p_szCaseID, p_szCaseID);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return szPDFFileName;
    }

    public string getPhysicalPath()
    {
        String szParamValue = "";
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            SqlCommand cmdQuery = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", sqlCon);
            cmdQuery.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader dr;
            dr = cmdQuery.ExecuteReader();
            while (dr.Read())
            {
                szParamValue = dr["parametervalue"].ToString();
            }
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
        return szParamValue;
    }


    public string GetDocPhysicalPath(int templateid)
    {
        String szParamValue = "";
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("GET_DOC_FILEPATH", sqlCon);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@I_TEMPLATE_ID", templateid);
            SqlDataReader dr=cmd.ExecuteReader();            
            while (dr.Read())
            {
                szParamValue = dr["SZ_TEMPLATE_FILENAME"].ToString();
            }
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
        return szParamValue;
    }

    public int GetDocFlag(int templateid, string outfilepath, string caseid)
    {
        int i_ParamValue = 0;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SP_CHECK_TXN_GENERATE_DOC_FLAG", sqlCon);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@I_TEMPLATE_ID", templateid);
            cmd.Parameters.AddWithValue("@SZ_DESTINATION_FILE", outfilepath);
            cmd.Parameters.AddWithValue("@SZ_CASE_ID", caseid);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i_ParamValue = Convert.ToInt32(dr["I_FLAG"].ToString());
            }
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            i_ParamValue = 2;
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return i_ParamValue;
    }


    public void SaveDocTemplate(ArrayList array)
    {        
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SP_INSERT_TXN_GENERATE_DOC", sqlCon);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@I_TEMPLATE_ID", array[0].ToString());
            cmd.Parameters.AddWithValue("@SZ_SOURCE_FILE", array[1].ToString());
            cmd.Parameters.AddWithValue("@SZ_DESTINATION_FILE", array[2].ToString());
            cmd.Parameters.AddWithValue("@SZ_CASE_ID", array[3].ToString());
            cmd.Parameters.AddWithValue("@SZ_HTTP_URL", array[4].ToString());
            cmd.ExecuteNonQuery();
           
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


    public bool GetTemplateType(string templateid)
    {
        string i_ParamValue = "";
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_TEMPLATE_TYPE", sqlCon);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@I_TEMPLATE_ID", templateid);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i_ParamValue = dr["TYPE"].ToString();
                if (i_ParamValue == "PDF")
                {
                    return true;
                }
            }
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
        return false;
    }
    
}
