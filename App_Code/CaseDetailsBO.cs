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
using Microsoft.SqlServer.Management.Common;

/// <summary>
/// Summary description for ClassDetailsBO
/// </summary>
public class CaseDetailsBO
{
    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    public CaseDetailsBO()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public string GetCasePatientID(string _szCaseId, string _szPatientId)
    {
        string szId = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "GET_CASE_PATIENT_ID";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            if (_szCaseId != "") { comm.Parameters.AddWithValue("@SZ_CASE_ID", _szCaseId); }
            if (_szPatientId != "") { comm.Parameters.AddWithValue("@SZ_PATIENT_ID", _szPatientId); }
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szId = dr[0].ToString(); }

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally { conn.Close(); }
        return szId;
    }

    public string GetCaseType(string p_szBillID)
    {
        string szCaseType = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_CASE_DETAILS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillID);
            comm.Parameters.AddWithValue("@FLAG", "GET_CASE_TYPE");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szCaseType = dr[0].ToString(); }

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally { conn.Close(); }
        return szCaseType;
    }
    public string GetCaseType(string p_szBillID,ServerConnection sqlConnection)
    {
        SqlDataReader dr = null; 
        string szCaseType = "";
        try
        {
            //conn = new SqlConnection(strsqlCon);
            //conn.Open();
          string  Query = "";
            Query = Query + "Exec SP_GET_CASE_DETAILS ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", p_szBillID, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "GET_CASE_TYPE", ",");

            Query = Query.TrimEnd(',');



            dr = sqlConnection.ExecuteReader(Query);
            
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szCaseType = dr[0].ToString(); }
                
            }
            dr.Close();
        }
        catch (Exception ex)
        {
            
            throw ex;
            //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally {
            if (dr != null && !dr.IsClosed)
            {

                dr.Close();
            }
        }
        return szCaseType;
    }

    public string GetAbbrevationID(string p_szTypeID)
    {
        string szCaseType = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_CASE_TYPE";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", p_szTypeID);
            comm.Parameters.AddWithValue("@FLAG", "GETABBRIVATIONID");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szCaseType = dr[0].ToString(); }

            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szCaseType;
    }

    public string GetPatientID(string p_szBillID)
    {
        string szPatientID = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_CASE_DETAILS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillID);
            comm.Parameters.AddWithValue("@FLAG", "GET_PATIENT_ID");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szPatientID = dr[0].ToString(); }

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szPatientID;
    }
    public string GetPatientID(string p_szBillID,ServerConnection conn)
    {
        string szPatientID = "";
        try
        {
            string Query = "";
            Query = Query + "Exec SP_GET_CASE_DETAILS ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", p_szBillID, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "GET_PATIENT_ID", ",");
            Query = Query.TrimEnd(',');
            
            dr = conn.ExecuteReader(Query);
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szPatientID = dr[0].ToString(); }

            }
            dr.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally {
            if (dr != null && !dr.IsClosed) { dr.Close(); }

        }
        return szPatientID;
    }

    public string GetLatestInsuranceCompanyID()
    {
        string szInsuranceID = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_INSURANCE_ADDRESS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;

            comm.Parameters.AddWithValue("@FLAG", "GETINSURANCELATESTID");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szInsuranceID = dr[0].ToString(); }

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szInsuranceID;
    }

    public DataSet GetInsuranceAddressDetails(string id)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_INSURANCE_ADDRESS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_INSURANCE_ID", id);
            comm.Parameters.AddWithValue("@FLAG", "GETDETAIL");

            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    public string GetProviderName(String p_szProviderID)
    {
        string szProviderName = "";
        try
        {
            conn = new SqlConnection(strsqlCon);            
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "SP_MST_PROVIDER";
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_PROVIDER_ID", p_szProviderID);
            comm.Parameters.AddWithValue("@FLAG", "GET_NAME");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szProviderName = dr[0].ToString(); }

            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szProviderName;
    }

    public string GetCaseTypeName(String p_szCaseTypeID)
    {
        string szCaseTypeName = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "SP_MST_CASE_TYPE";
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", p_szCaseTypeID);
            comm.Parameters.AddWithValue("@FLAG", "GET_NAME");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szCaseTypeName = dr[0].ToString(); }

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szCaseTypeName;
    }

    public string GetInsuranceCompanyName(String p_szInsuranceCompanyID)
    {
        string szInsuranceCompanyName = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "SP_MST_INSURANCE_COMPANY";
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_INSURANCE_ID", p_szInsuranceCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GET_NAME");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szInsuranceCompanyName = dr[0].ToString(); }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szInsuranceCompanyName;
    }

    public DataSet QuickSearch(string Search_Text, string str_Company_Id, string str_Prefix)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_QUICKSEARCH";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            int flag = 0;
            int testflag = 4;
            //foreach (Char chTest in Search_Text)
            //{
            //    if (Char.IsNumber(chTest))
            //    {
            //        flag = 0;
            //    }
            //    else
            //    {
            //        flag = 1; 
            //    }
            //}

            foreach (Char chTest in Search_Text)
            {
                if (!Char.IsNumber(chTest))
                {
                    flag = 0;
                }
                else
                {
                    flag = 1;
                    testflag = 1;
                    break;
                }
            }

            foreach (Char chTest in Search_Text)
            {
                if (Char.IsNumber(chTest))
                {
                    flag = 2;
                    testflag = 2;
                    break;
                }
                else
                {
                    if (testflag != 4)
                    {
                        flag = 3;
                        testflag = 3;
                        break;
                    }
                }
            }

            if (flag == 2)
            {
                comm.Parameters.AddWithValue("@SZ_CASE_NO", Search_Text);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", str_Company_Id);
                comm.Parameters.AddWithValue("@SZ_FLAG", "CASE_NO");
            }
            if (flag == 0 && testflag == 4)
            {
                comm.Parameters.AddWithValue("@SZ_PATIENT_NAME", Search_Text);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", str_Company_Id);
                comm.Parameters.AddWithValue("@SZ_FLAG", "NAME");
            }
            if (flag == 3)
            {
                int i = 0;
                foreach (Char chTest in Search_Text)
                {
                    if (!Char.IsNumber(chTest))
                    {
                        i++;
                    }

                }
                string Case_No = Search_Text.Substring(i);
                comm.Parameters.AddWithValue("@SZ_CASE_NO", Case_No);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", str_Company_Id);
                comm.Parameters.AddWithValue("@SZ_PREFIX", str_Prefix);
                comm.Parameters.AddWithValue("@SZ_FLAG", "PREFIX");
            }
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }


    public string GetPatientName(String p_szPatientID)
    {
        string szPatientName = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "SP_MST_PATIENT";
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            comm.Parameters.AddWithValue("@FLAG", "GET_NAME");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szPatientName = dr[0].ToString(); }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szPatientName;
    }

    public string GetCaseStatus(String p_szCaseStatusID)
    {
        string szCaseStatus = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "SP_MST_CASE_STATUS";
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_CASE_STATUS_ID", p_szCaseStatusID);
            comm.Parameters.AddWithValue("@FLAG", "GET_NAME");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szCaseStatus = dr[0].ToString(); }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szCaseStatus;
    }

    public string GetPatientCompanyID(string p_szPatientID)
    {
        string szId = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "GET_PATIENT_COMPANY_ID";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            if (p_szPatientID != "") { comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID); }
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szId = dr[0].ToString(); }

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szId;
    }

    public string GetCaseID(string p_szCaseNo, string p_szCompanyID)
    {
        string szId = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "GET_CASE_NO";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            if (p_szCaseNo != "") { comm.Parameters.AddWithValue("@SZ_CASE_NO", p_szCaseNo); }
            if (p_szCompanyID != "") { comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID); }
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szId = dr[0].ToString(); }

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szId;
    }

    public string GetCaseNo(string p_szCaseNo, string p_szCompanyID)
    {
        string szId = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "Sp_GET_CASE_NO";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            if (p_szCaseNo != "") { comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseNo); }
            if (p_szCompanyID != "") { comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID); }
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szId = dr[0].ToString(); }

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szId;
    }

    public void SoftDelete(string p_szCaseID, string p_szCompanyID, bool p_btDelete)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_CASE_MASTER";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@BT_DELETE", p_btDelete);

            comm.Parameters.AddWithValue("@FLAG", "SOFT_DELETE");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public DataSet GetBillInfo(string caseID, string companyID)
    {
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_EXPORT_BILLS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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


    public DataSet GetHardDeleteList(string p_szCompanyID)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_HARD_DELETE";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "LIST");

            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    public void HardDelete(string p_szCaseID, string p_szCompanyID)
    {
        SqlTransaction sqltran = null;
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            sqltran = conn.BeginTransaction();


            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Transaction = sqltran;
            comm.CommandTimeout = 0;
            comm.CommandText = "SP_HARD_DELETE";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "HARDDELETE");
            comm.ExecuteNonQuery();
            sqltran.Commit();
        }
        catch (Exception ex)
        {

            sqltran.Rollback();
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally { conn.Close(); }
    }

    public void UnDeleteCase(string p_szCaseID, string p_szCompanyID)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_HARD_DELETE";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "UNDELETE");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }


    public bool CheckCaseExists(string p_szCaseNo, string p_szCompanyID)
    {
        bool szId = false;
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_CHECK_CASE_EXISTS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            if (p_szCaseNo != "") { comm.Parameters.AddWithValue("@SZ_CASE_NO", p_szCaseNo); }
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value)
                {
                    if (dr[0].ToString() != "0")
                    {
                        szId = true;
                    }
                }

            }

 

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

        return szId;
    }

    public void SaveAssociateCases(string p_szFirstCaseID, string p_szSecondCaseID, string p_szCompanyID, string p_szFlag)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_WORKAREA_ASSOCIATED_CASE";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_FIRST_CASE_ID", p_szFirstCaseID);
            comm.Parameters.AddWithValue("@SZ_SECOND_CASE_ID", p_szSecondCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", p_szFlag);
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public DataSet GetAssociateCases(string p_szCaseID, string p_szCompanyID, string p_szFlag)
    {
        DataSet ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_WORKAREA_ASSOCIATED_CASE";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_FIRST_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", p_szFlag);
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    public DataSet GetPOpupDetails(string p_szCaseID, string p_szCompanyID)
    {
        DataSet ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_RELATED_DATA_LIST";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);

            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    public string SearchInsuranceCompany(string p_szInsName, string p_szInsCode, string p_szCompanyID)
    {
        DataSet ds = new DataSet();
        String szInsID = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_INSURANCE_COMPANY";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_INSURANCE_NAME", p_szInsName);
            comm.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY_CODE", p_szInsCode);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "SEARCH_CASE_DETAILS");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szInsID = dr["CODE"].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szInsID;
    }

    public DataSet GetCaseSupplies(string p_szCaseID, string p_szCompanyID)
    {
        DataSet ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_CASE_SUPPLIES";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "LIST");

            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    public void DeleteCaseSupplies(string p_szCaseID, string p_szCompanyID)
    {
        DataSet ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_CASE_SUPPLIES";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "DELETE");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    public void InsertCaseSupplies(string p_szSuppliesID, string p_szCaseID, string p_szCompanyID)
    {
        DataSet ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_CASE_SUPPLIES";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_SUPPLIES_ID", p_szSuppliesID);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
    }

    //public DataSet GetPatientVisit(string p_szCaseID, string p_szCompanyID)
    //{
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        conn = new SqlConnection(strsqlCon);
    //        conn.Open();
    //        comm = new SqlCommand();
    //        comm.CommandText = "SP_GET_PATIENT_VISIT";
    //        comm.CommandType = CommandType.StoredProcedure;
    //        comm.Connection = conn;
    //        comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
    //        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
    //        sqlda = new SqlDataAdapter(comm);
    //        sqlda.Fill(ds);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //    finally { conn.Close(); }
    //    return ds;
    //}


    public string GetCaseName(string p_szCaseID, string p_szCompanyID)
    {
        string szPatientName = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.CommandText = "GET_CASE_PATIENT_NAME";
            comm.CommandTimeout = 0;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szPatientName = dr[0].ToString(); }
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally { conn.Close(); }
        return szPatientName;
    }


    public string GetCaseTypeCaseID(string p_szCaseID)
    {
        string szCaseType = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_CASE_DETAILS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@FLAG", "GET_CASE_TYPE_CASE_ID");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szCaseType = dr[0].ToString(); }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally { conn.Close(); }
        return szCaseType;
    }

    public DataSet GetCaseListPatientName(string p_PatientName, string p_szCompanyID)
    {
        bool szId = false;
        DataSet ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_CASE_MASTER";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            if (p_PatientName != "")
            {
                comm.Parameters.AddWithValue("@SZ_PATIENT_FIRST_LAST_NAME", p_PatientName);

            }
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            comm.Parameters.AddWithValue("@FLAG", "LIST");
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    public DataSet GetCaseListLocation(string sz_location_id, string sz_CompanyID)
    {
        bool szId = false;
        DataSet ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_CASE_MASTER";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_LOCATION_ID", sz_location_id);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            comm.Parameters.AddWithValue("@FLAG", "LIST");
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    public DataSet DoctorName(string sz_location_id, string sz_company_id)
    {
        SqlCommand comm;
        SqlConnection conn = null;
        SqlDataAdapter sqlda;
        DataSet ds = new DataSet();
        String strsqlCon;
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_DOCTOR";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_LOCATION_ID", sz_location_id);
            comm.Parameters.AddWithValue("@ID", sz_company_id);
            comm.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST_OFFICEWISE");

            sqlda = new SqlDataAdapter(comm);
            
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    public string GetPatientLocationID(string sz_CaseID, string sz_CompanyID)
    {
        string LocationID = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("SP_GET_LOCATION_ID", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", sz_CaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            dr = comm.ExecuteReader();

            while (dr.Read())
            {
                if (dr != null)
                {
                    LocationID = dr.GetValue(0).ToString();
                }
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return LocationID;
    }

    public string GetCaseStatusArchived(string sz_CompanyID, string sz_CaseID)
    {
        string sz_Casestatus = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("SP_GET_ARCHIVED_STATUS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", sz_CaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr != null)
                {
                    sz_Casestatus = dr.GetValue(0).ToString();
                }
            }

        }
       catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return sz_Casestatus;
    }

    public bool CheckCaseExistsWithPrefix(string sz_CompanyPrefix, string p_szCaseNo, string p_szCompanyID)
    {
        bool szId = false;
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_REF_CHECK_CASE_EXISTS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            if (p_szCaseNo != "") { comm.Parameters.AddWithValue("@SZ_CASE_NO", p_szCaseNo); }
            if (sz_CompanyPrefix != "") { comm.Parameters.AddWithValue("@SZ_PREFIX", sz_CompanyPrefix); }
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value)
                {
                    if (dr[0].ToString() != "0")
                    {
                        szId = true;
                    }
                }

            }



        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szId;
    }



    public string GetCaseIDEmpty(string p_szCaseNo, string p_szCompanyID)
    {
        string szId = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "GET_CASE_NO_EMPTY";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            if (p_szCaseNo != "") { comm.Parameters.AddWithValue("@SZ_CASE_NO", p_szCaseNo); }
            if (p_szCompanyID != "") { comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID); }
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szId = dr[0].ToString(); }

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return szId;
    }


    public string GetCaseSatusId(string sz_CompanyID)
    {
        string sz_Casestatus = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("SP_GET_CASE_TYPE_ID", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                sz_Casestatus = dr[0].ToString();
            }

        }
       catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

        return sz_Casestatus;
    }

    public DataSet GetAbbrivation()
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_ABBRIVATION";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;


            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    public string GetCaseIdByPatientID(string sz_CompanyID, string sz_PatientId)
    {
        string sz_Casestatus = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("GET_CASE_ID", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            comm.Parameters.AddWithValue("@SZ_PATIENT_ID", sz_PatientId);


            dr = comm.ExecuteReader();
            while (dr.Read())
            {

                sz_Casestatus = dr[0].ToString();

            }

        }
       catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }

        return sz_Casestatus;
    }
    public DataSet GetPriorityBit(string sz_insuranceId, string sz_CompanyID)
    {
        SqlCommand comm;
        SqlConnection conn = null;
        SqlDataAdapter sqlda;
        DataSet ds = new DataSet();
        String strsqlCon;
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_GET_PRIORITY_BILLING_BIT";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_INSURANCE_ID", sz_insuranceId);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlda = new SqlDataAdapter(comm);
           
            sqlda.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }
    public DataSet Get1500FormBitForInsurance(string sz_company_id, string sz_bill_no)
    {
        DataSet dsProcId = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adop;
        try
        {
            con = new SqlConnection(strsqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_BIT_1500_FORM_FOR_INSURANCE", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            cmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_bill_no);
            cmd.CommandType = CommandType.StoredProcedure;
            adop = new SqlDataAdapter(cmd);
            adop.Fill(dsProcId);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return dsProcId;
    }
    public DataSet Get1500FormBitForInsurance(string sz_company_id, string sz_bill_no,ServerConnection conn)
    {
        DataSet dsProcId = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adop;
        try
        {
            String Query = "";
            Query = Query + "Exec SP_GET_BIT_1500_FORM_FOR_INSURANCE ";
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", sz_company_id, ",");
            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", sz_bill_no, ",");
            

            Query = Query.TrimEnd(',');
            dsProcId = conn.ExecuteWithResults(Query);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            
        }
        return dsProcId;
    }

    public DataSet GetRefferingInfo(string sz_case_id, string sz_company_id)
    {
        SqlCommand comm;
        SqlConnection conn = null;
        SqlDataAdapter sqlda;
        DataSet ds = new DataSet();
        String strsqlCon;
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "sp_get_refferinginfo_for_add_visit";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_case_id", sz_case_id);
            comm.Parameters.AddWithValue("@sz_company_id", sz_company_id);
            
            sqlda = new SqlDataAdapter(comm);
            
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }

    public DataSet searchload(string companyid, string patientname, string caseno, string casetype)
    {
        string strconn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strconn);
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SP_HARD_DELETE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.Parameters.AddWithValue("@SZ_PATIENT_NAME", patientname);
            comm.Parameters.AddWithValue("@SZ_CASE_NO", caseno);
            if (casetype != null && casetype != "" && casetype != "NA")
            {
                comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", casetype);
            }
            comm.Parameters.AddWithValue("@FLAG", "SEARCH");
            comm.Connection = conn;
            da = new SqlDataAdapter(comm);
            da.Fill(ds);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;


    }

    public DataSet GetEmployerAddressDetails(string id)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_EMPLOYER_ADDRESS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_EMPLOYER_ID", id);
            comm.Parameters.AddWithValue("@FLAG", "GETDETAIL");

            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return ds;
    }
    public string GetLatestEmployerCompanyID()
    {
        string szEmployerID = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_MST_EMPLOYER_ADDRESS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;

            comm.Parameters.AddWithValue("@FLAG", "GETEMPLOYERLATESTID");
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { szEmployerID = dr[0].ToString(); }

            }



        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally { conn.Close(); }
        return szEmployerID;
    }

}
