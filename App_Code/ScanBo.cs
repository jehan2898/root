using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ScanBo
/// </summary>
/// 
[Serializable]
public class ScanBo
{
    private FileUpload _FileUpload;
    private ArrayList arrImgId;
    private string str21 = "";
    private string strsqlCon = "";
    private DataSet ds;
    private SqlCommand sqlCmd;
    private SqlConnection sqlCon;
    private SqlDataAdapter sqlda;
    private SqlDataReader dr;

	public ScanBo()
	{
        this.strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public void SaveReqDoc(string SZ_CASE_DOCUMENT_ID, string SZ_CASE_ID, string SZ_CASE_TYPE_ID, string I_DOCUMENT_TYPE_ID, string I_RECIEVED, string SZ_NOTES, string SZ_ASSIGN_TO, string DT_ASSIGN_ON, string SZ_UPDATED_BY, string DT_UPDATED_ON, string SZ_STATUS, string SZ_COMPANY_ID, string I_IMAGE_ID, string Flag)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("SP_REQ_DOCUMENTS", this.sqlCon);
            this.sqlCmd.CommandType = CommandType.StoredProcedure;
            this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_DOCUMENT_ID", SZ_CASE_DOCUMENT_ID);
            this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", SZ_CASE_ID);
            this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", SZ_CASE_TYPE_ID);
            this.sqlCmd.Parameters.AddWithValue("@I_DOCUMENT_TYPE_ID", I_DOCUMENT_TYPE_ID);
            this.sqlCmd.Parameters.AddWithValue("@I_RECIEVED", I_RECIEVED);
            this.sqlCmd.Parameters.AddWithValue("@SZ_NOTES", SZ_NOTES);
            this.sqlCmd.Parameters.AddWithValue("@SZ_ASSIGN_TO", SZ_ASSIGN_TO);
            this.sqlCmd.Parameters.AddWithValue("@DT_ASSIGN_ON", DT_ASSIGN_ON);
            this.sqlCmd.Parameters.AddWithValue("@SZ_UPDATED_BY", SZ_UPDATED_BY);
            this.sqlCmd.Parameters.AddWithValue("@DT_UPDATED_ON", DT_UPDATED_ON);
            this.sqlCmd.Parameters.AddWithValue("@SZ_STATUS", SZ_STATUS);
            this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID);
            this.sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", I_IMAGE_ID);
            this.sqlCmd.Parameters.AddWithValue("@FLAG", Flag);
            this.sqlCmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }

    public string SaveDocumentData(ArrayList arrayList)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        string str = "";
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("SP_SAVE_REQ_DOCUMENT", this.sqlCon);
            this.sqlCmd.CommandType = CommandType.StoredProcedure;
            this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", arrayList[0].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_NODE_NAME", arrayList[1].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrayList[2].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", arrayList[3].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", arrayList[4].ToString());
            this.sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", arrayList[5].ToString());
            this.sqlCmd.Parameters.AddWithValue("@I_MST_NODE_ID", arrayList[6].ToString());
            SqlParameter parameter = new SqlParameter("@i_image_id", SqlDbType.Int);
            parameter.Direction = ParameterDirection.Output;
            this.sqlCmd.Parameters.Add(parameter);
            this.sqlCmd.ExecuteNonQuery();
            str = this.sqlCmd.Parameters["@i_image_id"].Value.ToString();
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return str;
    }

    public string GetNodePath(string p_szNodeID, string p_szCaseID, string p_szCompanyID)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        string str = "";
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("SP_REQUIRED_DOCUMENTS_GET_FULL_NODE_PATH", this.sqlCon);
            this.sqlCmd.CommandType = CommandType.StoredProcedure;
            this.sqlCmd.Parameters.AddWithValue("@SZ_NODEID", p_szNodeID);
            this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            this.sqlda = new SqlDataAdapter(this.sqlCmd);
            this.sqlda.Fill(this.ds);
            if ((this.ds != null) && (this.ds.Tables[0] != null))
            {
                str = this.ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return str;
    }

    public string getPhysicalPath()
    {
        string str = "";
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            this.sqlCon.Open();
            SqlDataReader reader = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", this.sqlCon).ExecuteReader();
            while (reader.Read())
            {
                str = reader["parametervalue"].ToString();
            }
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return str;
    }

    public string GetPatientName(String p_szPatientID)
    {
        string szPatientName = "";
        try
        {
            sqlCon = new SqlConnection(strsqlCon);
            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandText = "SP_MST_PATIENT";
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_NAME");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szPatientName = dr[0].ToString(); }
            }

            return szPatientName;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return null;

        }
        finally { sqlCon.Close(); }
    }

    public string GetCaseNo(string p_szCaseId, string p_szCompanyID)
    {
        string szId = "";
        try
        {
            sqlCon = new SqlConnection(strsqlCon);
            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "Sp_GET_CASE_NO";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            if (p_szCaseId != "") { sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseId); }
            if (p_szCompanyID != "") { sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID); }
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szId = dr[0].ToString(); }

            }

            return szId;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return null;

        }
        finally { sqlCon.Close(); }
    }
    public DataSet GetCaseInfo(string p_szCaseId, string p_szCompanyID)
    {

        try
        {
            sqlCon = new SqlConnection(strsqlCon);
            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "sp_get_case_no_patinet_name";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            if (p_szCaseId != "") { sqlCmd.Parameters.AddWithValue("@i_case_id", p_szCaseId); }
            if (p_szCompanyID != "") { sqlCmd.Parameters.AddWithValue("@sz_compnay_id", p_szCompanyID); }
            ds = new DataSet();
            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally { sqlCon.Close(); }
        return ds;
    }
}