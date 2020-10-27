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

public class Bill_Sys_ProcedureCode_BO
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    public Bill_Sys_ProcedureCode_BO()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public DataSet GetProcedure_Code_Amount_List(string procedureCodeId, string szCompanyID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PROCEDURE_CODE_AMOUNT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE_ID", procedureCodeId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            //sqlCmd.ExecuteNonQuery();

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

    public void SaveProcedure_Code_Amount(ArrayList _arrayList)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PROCEDURE_CODE_AMOUNT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE_ID", _arrayList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", _arrayList[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_AMOUNT", _arrayList[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[3].ToString());

            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public void UpdateProcedure_Code_Amount(ArrayList _arrayList)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PROCEDURE_CODE_AMOUNT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_AMOUNT_ID", _arrayList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE_ID", _arrayList[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", _arrayList[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_AMOUNT", _arrayList[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[4].ToString());

            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public string GetLatestProcedureCodeId(String companyid)
    {
        string latestid = "";
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PROCEDURE_CODE_AMOUNT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);

            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_LATEST_ID");
            sqlCmd.CommandType = CommandType.StoredProcedure;
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                latestid = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }

        return latestid;
    }

    public DataSet GetAssociatedProcedureCode_List(string dignosisCodeId, string companyId)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_ASSCIATE_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", dignosisCodeId);
            //sqlCmd.ExecuteNonQuery();

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

    public DataSet GetAssociatedDiagnosisCode_List(string caseId, string companyId)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_ASSOCIATED_DIAGNOSIS_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseId);
            //sqlCmd.ExecuteNonQuery();

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

    public void SaveDoctorProcedureCodeAmountDetails(ArrayList _arrayList)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_PROCEDURE_AMOUNT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", _arrayList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", _arrayList[1].ToString());
            sqlCmd.Parameters.AddWithValue("@FLT_AMOUNT", _arrayList[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[3].ToString());

            sqlCmd.Parameters.AddWithValue("@FLAG", "ADDUPDATE");
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", _arrayList[4].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public DataSet GetDoctorProcedureCodeList(string doctorid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_PROCEDURE_AMOUNT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorid);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");


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



    public DataSet GetDoctorProcedureCodes(string doctorid, string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_PROCEDURE_AMOUNT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDOCTORPROCLIST");


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

    public DataSet GetDoctorSpecialityProcedureCodes(string doctorid, string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_DOCTOR_PROCEDURE_AMOUNT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDOCTORSPECIALITYPROCLIST");


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


    public DataSet GetAllProcedureCodeList(ArrayList objArr)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PROCEDURE_LISTING", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objArr[0]);
            sqlCmd.Parameters.AddWithValue("@SZ_TREATMENT_ID", objArr[1]);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objArr[2]);

            string szOrder = (string)objArr[3];
            if (szOrder.CompareTo("code") == 0)
            {
                sqlCmd.Parameters.AddWithValue("@FLAG", "LIST-FOR-PRICING-CODE-SORTED");
            }
            else
            {
                sqlCmd.Parameters.AddWithValue("@FLAG", "LIST-FOR-PRICING-NAME-SORTED");
            }

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

    public DataSet GetStatusProcedureCodeList(string caseID, string companyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_PROCEDURE_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);


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






    public DataSet UpdateReadingDoctor(int ID, string DocID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_READINGDOCTOR", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_PROC_ID", ID);
            sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_DOCTOR_ID", DocID);


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


    public void UpdateReportProcedureCodeList(int i_EVENT_PROC_ID, string strLinkPath, int imageId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_PROCEDURE_REPROT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_PROC_ID", i_EVENT_PROC_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_REPORT_PATH", strLinkPath);
            sqlCmd.Parameters.AddWithValue("@IMAGEID", imageId);

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
    public void UpdateReportProcedureCodeListByEventID(int i_EVENT_ID, string strLinkPath, int imageId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_PROCEDURE_REPROT_BY_EVENT_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", i_EVENT_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_REPORT_PATH", strLinkPath);
            sqlCmd.Parameters.AddWithValue("@IMAGEID", imageId);

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

    public void UpdateStatusProcedureCodeList(int i_EVENT_PROC_ID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_PROCEDURE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_PROC_ID", i_EVENT_PROC_ID);
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

    public DataSet Search_ProcedureCodes(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CODE_DESCRIPTION", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE_ID", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[5].ToString());
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

    public DataSet Search_GroupProcedureCodes(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_GROUP_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[2].ToString());

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

    public DataSet Search_GroupProcedureCodes(string ProcedureGroup,string CompanyID,string Flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_GROUP_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", ProcedureGroup);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", Flag);

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

    public void SaveUpdateGroupProcedureCodes(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_GROUP_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;


            sqlCmd.Parameters.AddWithValue("@I_GROUP_PROCEDURE_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_GROUP_NAME", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[5].ToString());


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

    public void SaveGroupProcedureCodeAmount(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PROCEDURE_GROUP_AMOUNT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;


            sqlCmd.Parameters.AddWithValue("@FLT_AMOUNT", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_NAME", objAL[3].ToString());

            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");

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

    public void RemoveGroupProcedureCodes(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_GROUP_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@I_GROUP_PROCEDURE_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", objAL[1].ToString());


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

    public void DeleteAssignProcCode(string p_szAssignProcCodeID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_ASSOCIATE_PROCEDURE_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_ASSOCIATE_ID", p_szAssignProcCodeID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");
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

    public void DeleteProcGroupAmount(string p_szProcGroupID, string p_szProcGroupName, string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PROCEDURE_GROUP_AMOUNT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;


            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", p_szProcGroupID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_NAME", p_szProcGroupName);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");

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

    public void UpdateAssignProcedure(string sz_I_AssociateID, string sz_CompanyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {

            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_ASSOCIATE_PROCEDURE_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@I_ASSOCIATE_ID", sz_I_AssociateID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
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
    // 24/03/10 Get max order amt for bill_sys_proceduregroup.aspx -- sachin
    public string GetMaxOrder(string _companyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ArrayList arr = new ArrayList();
        string _latestID = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PROCEDURE_GROUP", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _companyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_MAX_ORDER");
            dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr[0] != DBNull.Value) { _latestID = dr[0].ToString(); }
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

        return _latestID;
    }
    //==================================================================================

    //23 APRIL 2010 UPDATE CALENDAR PROCEDURE CODE FOR RECEIVE DOCUMENT POPUP PAGE-- SACHIN

    public int UpdateReportProcedureCodeListWithRefDoctor(int i_EVENT_PROC_ID, string strLinkPath, string RefDoctorID, int ImageID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        int _iReturnValue = 0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_PROCEDURE_REPROT_WITH_REFDOCTOR", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_PROC_ID", i_EVENT_PROC_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_REPORT_PATH", strLinkPath);
            sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_DOCTOR_ID", RefDoctorID);
            sqlCmd.Parameters.AddWithValue("@IMAGEID", ImageID);

            SqlParameter return_para = new SqlParameter("@RETURN_VALUE", DbType.Int32);
            return_para.Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add(return_para);

            sqlCmd.ExecuteNonQuery();
            _iReturnValue = int.Parse(sqlCmd.Parameters["@RETURN_VALUE"].Value.ToString());

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
        return _iReturnValue;

    }




    public DataSet GetViewDocs(string caseID, string companyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DOC", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);


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


    public DataSet Get_Sys_Key(string syskey, string companyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_SYS_SETTING_KEY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_SYS_SETTING_KEY_ID", syskey);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);


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

    public DataSet GetLHRDocs(string caseID, string companyID, string sz_prc_id)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_LHR_VISIT_DOC", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROC_ID", sz_prc_id);


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



    public void assignLHRDocument(ArrayList obj, int ImageId, string docTypeID, int EventProcID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_ASSIGN_PROCEDURE_DOCUMENTS_LHR", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", obj[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", obj[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", obj[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", obj[3].ToString());
            sqlCmd.Parameters.AddWithValue("@I_EVENT_PROC_ID", EventProcID);
            sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", ImageId);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCUMENT_TYPE_ID", docTypeID);

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
    }

    public DataSet GetCaseProcCode(string szEventId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILLED_CASE_PROC_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", szEventId);


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
    public string GetDocIdUsingEventProcId(string szEventProcId)
    {
        string szDocId = "";
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DOCOTOR_ID_USING_EVENT_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_PROC_ID", szEventProcId);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szDocId = dr[0].ToString();
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
        return szDocId;
    }

    public DataSet GetImgIdUsingEvenetProcId(string szEventProcId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_IMG_ID_USING_EVENT_PROC_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szEventProcId);


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


    public DataSet GetDocs(string caseID, string companyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_LHR_RESCHEDULE_DOC", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);



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


    public DataSet GetDocsNew(string caseID, string companyID, string sz_EventProcID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_LHR_RESCHEDULE_DOC_NEW", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlCmd.Parameters.AddWithValue("@i_Event_proc_id", sz_EventProcID);


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

    //=====================================================================================

    //public void Save_Update_ProcedureCode(string szCode, string szProcedureGroupID, string szDescription, string szAmount, int iVisitType, string szModifier, string szCompanyID, string szProcedureID, string szFlag, string szAddToPreferred,string szRevCode,string szValueCode )
    public void Save_Update_ProcedureCode(string szCode, string szProcedureGroupID, string szDescription,
        string szAmount, int iVisitType, string szModifier, string szCompanyID, string szProcedureID, string szFlag, 
        string szAddToPreferred,string szRevCode,string szValueCode,string szProcedureLongDesc,string  szModifierDesc, 
        string szRVU,string szLocation, string sz1500Desc,string dateapply,string contractamount,DataTable dtInsid )
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE", szCode);
            sqlCmd.Parameters.AddWithValue("@SZ_CODE_DESCRIPTION", szDescription);
            sqlCmd.Parameters.AddWithValue("@FLT_AMOUNT", szAmount);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szProcedureGroupID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_MODIFIER", szModifier);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", szProcedureID);
            sqlCmd.Parameters.AddWithValue("@I_VISIT_TYPE", iVisitType);
            sqlCmd.Parameters.AddWithValue("@BT_ADD_TO_PREFERRED_LIST", szAddToPreferred);
            sqlCmd.Parameters.AddWithValue("@SZ_REV_CODE", szRevCode);
            sqlCmd.Parameters.AddWithValue("@SZ_VALUE_CODE", szValueCode);
            sqlCmd.Parameters.AddWithValue("@SZ_CODE_LONG_DESC", szProcedureLongDesc);
            sqlCmd.Parameters.AddWithValue("@SZ_MODIFIER_LONG_DESC", szModifierDesc);
            sqlCmd.Parameters.AddWithValue("@SZ_RVU", szRVU);

            sqlCmd.Parameters.AddWithValue("@dt_date_after_apply", dateapply);
            sqlCmd.Parameters.AddWithValue("@contractamount", contractamount);
            sqlCmd.Parameters.AddWithValue("@Insurance_id", dtInsid);

            sqlCmd.Parameters.AddWithValue("@FLAG", szFlag);
            if (szLocation != "" && szLocation.ToUpper() != "NA")
            {
                sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", szLocation);
            }
            sqlCmd.Parameters.AddWithValue("@SZ_1500_DESC", sz1500Desc);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public void Update_Modifier(ArrayList arrProcCodeID, string szCompanyID, string szModifier)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            for (int i = 0; i < arrProcCodeID.Count; i++)
            {
                sqlCmd = new SqlCommand("SP_UPDATE_MODIFIER", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", Convert.ToString(arrProcCodeID[i]));
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
                sqlCmd.Parameters.AddWithValue("@SZ_MODIFIER", szModifier);

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.ExecuteNonQuery();
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public void UpdateSpecialty(string szCompanyId, string szProcedureGroupID, string szEventProcId, string szCaseId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_SPECIALITY_FOR_VISIT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szProcedureGroupID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            sqlCmd.Parameters.AddWithValue("@I_EVENT_PROC_ID", szEventProcId);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);

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

    public DataSet UpdateCoSignedBy(int EventID, string szCoSignedDoctor, string flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_CO_SIGNED_BY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", EventID);
            sqlCmd.Parameters.AddWithValue("@SZ_CO_SIGNEDBY_DOCTOR_ID", szCoSignedDoctor);
            sqlCmd.Parameters.AddWithValue("@flag", flag);
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


    public void UpdatePreferredList(ArrayList arrProcCodeID, string szCompanyID, string szPreferred)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            for (int i = 0; i < arrProcCodeID.Count; i++)
            {
                sqlCmd = new SqlCommand("SP_UPDATE_PREFERRED_BIT", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", Convert.ToString(arrProcCodeID[i]));
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
                sqlCmd.Parameters.AddWithValue("@BT_ADD_TO_PREFERRED_LIST", szPreferred);

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.ExecuteNonQuery();
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public void UpdateCaseTypeOfAssociateDocument(string szEventID, string szCaseId, string szCaseType, string szCompanyID)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_CASETYPE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EVENT_ID", szEventID);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            sqlCmd.Parameters.AddWithValue("@SZ_CASETYPE_ID", szCaseType);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public void Save_Update_EmployerProcedureCode(string szCode, string szProcedureGroupID, string szDescription, string szAmount, int iVisitType, string szModifier, string szEmployerID, string szCompanyID, string szProcedureID, string szFlag, string szAddToPreferred, string szRevCode, string szValueCode, string szProcedureLongDesc, string szModifierDesc, string szRVU, string szLocation)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_EMPLOYER_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE", szCode);
            sqlCmd.Parameters.AddWithValue("@SZ_CODE_DESCRIPTION", szDescription);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_ID", szEmployerID);
            sqlCmd.Parameters.AddWithValue("@FLT_AMOUNT", szAmount);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szProcedureGroupID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_MODIFIER", szModifier);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", szProcedureID);
            sqlCmd.Parameters.AddWithValue("@I_VISIT_TYPE", iVisitType);
            sqlCmd.Parameters.AddWithValue("@BT_ADD_TO_PREFERRED_LIST", szAddToPreferred);
            sqlCmd.Parameters.AddWithValue("@SZ_REV_CODE", szRevCode);
            sqlCmd.Parameters.AddWithValue("@SZ_VALUE_CODE", szValueCode);
            sqlCmd.Parameters.AddWithValue("@SZ_CODE_LONG_DESC", szProcedureLongDesc);
            sqlCmd.Parameters.AddWithValue("@SZ_MODIFIER_LONG_DESC", szModifierDesc);
            sqlCmd.Parameters.AddWithValue("@SZ_RVU", szRVU);
            sqlCmd.Parameters.AddWithValue("@FLAG", szFlag);
            if (szLocation != "" && szLocation.ToUpper() != "NA")
            {
                sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", szLocation);
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public void Copy_EmployerProcedureCode(string szFromEmployerId, string szToEmployerId, string szProcedureId)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_COPY_EMPLOYER_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FROM_EMPLOYER_ID", szFromEmployerId);
            sqlCmd.Parameters.AddWithValue("@SZ_TO_EMPLOYER_ID", szToEmployerId);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", szProcedureId);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public void Update_Employer_Modifier(ArrayList arrProcCodeID, string szCompanyID, string szModifier)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            for (int i = 0; i < arrProcCodeID.Count; i++)
            {
                sqlCmd = new SqlCommand("SP_MST_EMPLOYER_PROCEDURE_CODES", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", Convert.ToString(arrProcCodeID[i]));
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
                sqlCmd.Parameters.AddWithValue("@SZ_MODIFIER", szModifier);
                sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE_MODIFIER");

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.ExecuteNonQuery();
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }
    public void Update_Employer_PreferredList(ArrayList arrProcCodeID, string szCompanyID, string szPreferred)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            for (int i = 0; i < arrProcCodeID.Count; i++)
            {
                sqlCmd = new SqlCommand("SP_MST_EMPLOYER_PROCEDURE_CODES", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", Convert.ToString(arrProcCodeID[i]));
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
                sqlCmd.Parameters.AddWithValue("@BT_ADD_TO_PREFERRED_LIST", szPreferred);
                sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE_PREFERRED");
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.ExecuteNonQuery();
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

}
