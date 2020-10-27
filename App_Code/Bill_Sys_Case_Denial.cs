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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Bill_Sys_Case_Denial
/// </summary>
/// 
public class Bill_Sys_Denial_Desc
{
    private string _sz_description;
    public string sz_description
    {
        get
        {
            return _sz_description;
        }
        set
        {
            _sz_description = value;
        }
    }

    private string _sz_denial_reason_code;
    public string sz_denial_reason_code
    {
        get
        {
            return _sz_denial_reason_code;
        }
        set
        {
            _sz_denial_reason_code = value;
        }
    }

    private string _sz_verification_date;

    public string sz_verification_date
    {
        set
        {
            _sz_verification_date = value;
        }
        get
        {
            return _sz_verification_date;
        }
    }

    private int _i_varification_type;
    public int i_verification
    {
        get
        {
            return _i_varification_type;
        }
        set
        {
            _i_varification_type = value;
        }

    }

    private string _sz_company_id;
    public string sz_company_id
    {
        get
        {
            return _sz_company_id;
        }
        set
        {
            _sz_company_id = value;
        }
    }
    private string _sz_user_id;
    public string sz_user_id
    {
        set
        {
            _sz_user_id = value;
        }
        get
        {
            return _sz_user_id;
        }
    }

    private string _sz_user_name;
    public string sz_user_name
    {
        set
        {
            _sz_user_name = value;
        }
        get
        {
            return _sz_user_name;
        }
    }

    private string _sz_flag;
    public string sz_flag
    {
        set
        {
            _sz_flag = value;
        }
        get
        {
            return _sz_flag;
        }
    }

    private string _sz_verification_id;
    public string sz_verification_id
    {
        get
        {
            return _sz_verification_id;
        }
        set
        {
            _sz_verification_id = value;
        }
    }

    private string _sz_case_id;
    public string sz_case_id
    {
        get
        {
            return _sz_case_id;
        }
        set
        {
            _sz_case_id = value;
        }
    }

    private string _sz_case_no;
    public string sz_case_no
    {
        get
        {
            return _sz_case_no;
        }
        set
        {
            _sz_case_no = value;
        }
    }
}

public class Bill_Sys_Case_Denial
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

	public Bill_Sys_Case_Denial()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet GetCaseDenialDetail(ArrayList ar)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_case_denial_details", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_case_id", ar[0].ToString());
            sqlCmd.Parameters.AddWithValue("@sz_company_id", ar[1].ToString());
            
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
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
        return ds;
    }

    public int saveCaseDenials(ArrayList objAL)
    {
        int j = 0;
        sqlCon = new SqlConnection(strsqlCon);
        sqlCon.Open();
        try
        {

            if (sqlCon != null)
            {
                Bill_Sys_Denial_Desc obj = null;
                for (int i = 0; i < objAL.Count; i++)
                {
                    obj = (Bill_Sys_Denial_Desc)objAL[i];
                    sqlCmd = new SqlCommand("SP_SAVE_CASE_DENIAL_DESCRIPTION", sqlCon);
                    sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", obj.sz_case_id);
                    sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", obj.sz_case_no);
                    sqlCmd.Parameters.AddWithValue("@SZ_DESCRIPTION", obj.sz_description);
                    sqlCmd.Parameters.AddWithValue("@DT_VERIFICATION_DATE", obj.sz_verification_date);
                    sqlCmd.Parameters.AddWithValue("@I_VERIFICATION_TYPE", 3);
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj.sz_company_id);
                    sqlCmd.Parameters.AddWithValue("@SZ_CREATED_USER_ID", obj.sz_user_id);
                    sqlCmd.Parameters.AddWithValue("@SZ_DENIAL_REASONS_CODE", obj.sz_denial_reason_code);
                    sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", obj.sz_user_name);
                    sqlCmd.Parameters.AddWithValue("@FLAG", obj.sz_flag);
                   j = sqlCmd.ExecuteNonQuery();
                }
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

        return j;

        //Method End


    }

    public string GetDenialCountForCase(string caseID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        string count = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_case_denial_count", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_case_id", caseID);

            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                count = Convert.ToString(dr.GetValue(0).ToString());
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
                dr.Close();
                sqlCon.Close();
            }
        }
        return count;
    }

    public int deleteCaseDenials(string szCaseId, string denialId)
    {
        int k = 0;
        sqlCon = new SqlConnection(strsqlCon);
        sqlCon.Open();
        try
        {
            if (sqlCon != null)
            {
                sqlCmd = new SqlCommand("sp_delete_general_case_denial", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@sz_case_id", szCaseId);
                sqlCmd.Parameters.AddWithValue("@i_denial_id", denialId);
                k= sqlCmd.ExecuteNonQuery();
                
            }
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        return k;
        //Method End


    }

    public string GetCaseGeneralDenialNode(string caseID, string companyId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        
        string nodeID = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_case_denial_nodeID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_caseID", caseID);
            sqlCmd.Parameters.AddWithValue("@sz_compnay_id", companyId);

            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                nodeID = Convert.ToString(dr[0]);
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
        return nodeID;
    }

    public void UploadGeneralDenialsFile(ArrayList ar)
    {

        //SP_SAVE_GENERAL_DENIALS_IMAGE
        sqlCon = new SqlConnection(strsqlCon);
        
        string szRetImg = "";
        string szRetPath = "";
        string szOldCase = "";

        sqlCon.Open();

        try
        {
            sqlCmd = new SqlCommand("SP_SAVE_GENERAL_DENIALS_IMAGE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", ar[0].ToString());
            sqlCmd.Parameters.AddWithValue("@NODE_ID", ar[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", ar[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", ar[3].ToString());
            sqlCmd.Parameters.AddWithValue("@I_DENIAL_ID", ar[4].ToString());
            sqlCmd.Parameters.AddWithValue("@USER_NAME", ar[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ar[6].ToString());

            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szRetImg = Convert.ToString(dr[0]);
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
        //return szRetImg;
    }
}