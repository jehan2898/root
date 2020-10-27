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


public class Bill_Sys_PatientBO
{



    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    DataTable dt;

    public Bill_Sys_PatientBO()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataSet GetDenialList(string szcaseID)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_DENIAL_BILLS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szcaseID);
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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
    public DataSet GetTreatmentList(string szcaseID)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_PATIENT_GETTREATMENTS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szcaseID);
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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

    public DataSet GetInsuranceCompanyAddress(string insuranceCompany)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        string address = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_INSURANCE_ADDRESS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", insuranceCompany);


            sqlCmd.Parameters.AddWithValue("@FLAG", "GETINSURANCEADDRESSLIST");
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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

    public string GetLatestEnterCaseID(string companyID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        string caseID = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PATIENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_LATEST_ENTER_CASE_ID");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["SZ_CASE_ID"] != DBNull.Value) { caseID = dr["SZ_CASE_ID"].ToString(); }
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

        return caseID;
    }

    public string GetLatestPatientCaseID(string companyID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        string caseID = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PATIENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_LATEST_PATIENT_CASE_ID");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["SZ_CASE_ID"] != DBNull.Value) { caseID = dr["SZ_CASE_ID"].ToString(); }
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

        return caseID;
    }

    public string GetMaxChartNumber(string _companyID)
    {
        sqlCon = new SqlConnection(strConn);
        ArrayList arr = new ArrayList();
        string _latestID = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PATIENT_DATA_ENTRY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _companyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETMAXCHARTNO");
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

    public string GetRefCompany(string _companyID)
    {
        sqlCon = new SqlConnection(strConn);
        ArrayList arr = new ArrayList();
        string _latestID = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PATIENT_DATA_ENTRY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _companyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETREFCOMPANY");
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


    public ArrayList GetAdjusterDetail(string adjusterID, string companyID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        ArrayList arr = new ArrayList();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_ADJUSTER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();


            sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_ID", adjusterID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);

            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_ADJUSTER_DETAIL");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["SZ_PHONE"] != DBNull.Value) { arr.Add(dr["SZ_PHONE"].ToString()); }
                if (dr["SZ_EXTENSION"] != DBNull.Value) { arr.Add(dr["SZ_EXTENSION"].ToString()); }
                if (dr["SZ_FAX"] != DBNull.Value) { arr.Add(dr["SZ_FAX"].ToString()); }
                if (dr["SZ_EMAIL"] != DBNull.Value) { arr.Add(dr["SZ_EMAIL"].ToString()); }
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

        return arr;
    }


    public DataSet GetAdjusterDetail(string adjusterID, string companyID, string szGM)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_ADJUSTER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;


            sqlCmd.Parameters.AddWithValue("@SZ_ADJUSTER_ID", adjusterID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);

            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_ADJUSTER_DETAIL");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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

    public ArrayList GetInsuranceAddressDetail(string insuranceaddressid)
    {

        sqlCon = new SqlConnection(strConn);
        ArrayList arr = new ArrayList();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_INSURANCE_ADDRESS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();


            sqlCmd.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", insuranceaddressid);


            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["SZ_INS_ADDRESS_ID"] != DBNull.Value) { arr.Add(dr["SZ_INS_ADDRESS_ID"].ToString()); }
                if (dr["SZ_INSURANCE_ID"] != DBNull.Value) { arr.Add(dr["SZ_INSURANCE_ID"].ToString()); }
                if (dr["SZ_INSURANCE_ADDRESS"] != DBNull.Value) { arr.Add(dr["SZ_INSURANCE_ADDRESS"].ToString()); }


                if (dr["SZ_INSURANCE_CITY"] != DBNull.Value) { arr.Add(dr["SZ_INSURANCE_CITY"].ToString()); }
                if (dr["SZ_INSURANCE_STATE"] != DBNull.Value) { arr.Add(dr["SZ_INSURANCE_STATE"].ToString()); }
                if (dr["SZ_INSURANCE_ZIP"] != DBNull.Value) { arr.Add(dr["SZ_INSURANCE_ZIP"].ToString()); }
                if (dr["SZ_INSURANCE_STREET"] != DBNull.Value) { arr.Add(dr["SZ_INSURANCE_STREET"].ToString()); }
                if (dr["SZ_COMPANY_ID"] != DBNull.Value) { arr.Add(dr["SZ_COMPANY_ID"].ToString()); }
                if (dr["SZ_INSURANCE_STATE_ID"] != DBNull.Value) { arr.Add(dr["SZ_INSURANCE_STATE_ID"].ToString()); }
                if (dr["SZ_FAX_NUMBER"] != DBNull.Value) { arr.Add(dr["SZ_FAX_NUMBER"].ToString()); } else { arr.Add(""); }
                if (dr["SZ_INSURANCE_PHONE"] != DBNull.Value) { arr.Add(dr["SZ_INSURANCE_PHONE"].ToString()); } else { arr.Add(""); }
                if (dr["SZ_CONTACT_PERSON"] != DBNull.Value) { arr.Add(dr["SZ_CONTACT_PERSON"].ToString()); } else { arr.Add(""); }

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

        return arr;
    }

    //SP_GET_PATIENT_DESK_DETAILS
    public DataSet GetPatienDeskList(string flag, string caseID, string companyID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_DESK_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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

    //Tab
    public DataTable GetTabTreating(string flag, string caseID, string companyID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_DESK_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);

            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }

    public DataSet GetPatienDeskList(string flag, string caseID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_DESK_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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
    //added by kunal
    public DataSet GetPatientInfo(string patientid, string companyid)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_CASE_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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
    //End

    //vaibhav
    public DataSet GetSecInsuranceInfo(string patientid, string companyid)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_GET_SEC_INSURANCE_DETAIL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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
    //End
    public DataSet GetPatientDetailList(string patientid)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_DETAILS_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;


            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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


    public DataSet GetPatientAccidentDetails(string patientid)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_ACCIDENT_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;


            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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



    public string CheckPatientExists(ArrayList arraylist)
    {
        SqlParameter sqlParam = new SqlParameter();
        SqlDataReader sqlreader;
        sqlCon = new SqlConnection(strConn);
        String _return = "";

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_CHECK_PATIENT_EXISTS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            //sqlParam = sqlCmd.Parameters.Add("@Return", SqlDbType.Int);
            sqlParam = sqlCmd.Parameters.Add("@Return", SqlDbType.Text);
            sqlParam.Direction = ParameterDirection.ReturnValue;


            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", arraylist[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME", arraylist[1].ToString());
            if (arraylist[2] != null) { sqlCmd.Parameters.AddWithValue("@DT_INJURY", Convert.ToDateTime(arraylist[2].ToString())); }
            //sqlCmd.Parameters.AddWithValue("@SZ_SOCIAL_SECURITY_NO", arraylist[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", arraylist[3].ToString());
            if (arraylist[4] != null) { sqlCmd.Parameters.AddWithValue("@DT_DOB", Convert.ToDateTime(arraylist[4].ToString())); }
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arraylist[5].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", arraylist[6].ToString());

            // sqlCmd.ExecuteNonQuery();
            sqlreader = sqlCmd.ExecuteReader();
            while (sqlreader.Read())
            {
                _return = sqlreader[0].ToString();
            }
            //_return = Convert.ToInt32(sqlCmd.Parameters["@Return"].Value);
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

        return _return;
    }


    public void UpdateTemplateStatus(string p_szCaseID, int i_status, string dt_nf2date)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        string caseID = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_MST_CASEMASTER_FOR_NF2", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            sqlCmd.Parameters.AddWithValue("@STATUS", i_status);
            if (dt_nf2date != "") { sqlCmd.Parameters.AddWithValue("@DT_NF2SEND_DATE", dt_nf2date); }
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
    public DataTable GetTemplateStatus(string p_szCaseID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        DataTable p_blTemplateStatus = new DataTable();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_MST_CASEMASTER_FOR_NF2", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_LIST_FOR_STATUS");
            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
            p_blTemplateStatus = ds.Tables[0];
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

        return p_blTemplateStatus;
    }
    public DataSet GetPatientVisitDeskList(string p_szCaseID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_CALENDAR_EVENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);

            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_VISIT_LIST");

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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
    //

    public DataTable GetConfigPatientDesk(String p_szUserRoleID)
    {
        DataTable dt = new DataTable();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DASH_BOARD", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_USER_ROLE_ID", p_szUserRoleID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_CONFIG_PATIENTDESK");
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(dt);
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
        return dt;
    }


    public DataSet GetVisitInformation(string p_szPatientID, string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_TEST_INFORMATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;


            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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

    public DataTable Get_Tab_TestInformation_TEMP(string p_szCaseID, string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_TAB_TESTINFORMATION_TEMP", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);



            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }

    public DataTable Get_Tab_TestInformation(string p_szPatientID, string p_szCompanyID, string strSpeciality)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_TAB_TESTINFORMATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_TAB_INFORMATION");
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@SPECIALITY", strSpeciality);


            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }

    public DataTable Get_SpecialityList(string p_szPatientID, string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_TAB_TESTINFORMATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_TAB_LIST");
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);

            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }

    public DataTable Get_SpecialityList(string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_SPECIALITY_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            //sqlCmd.Parameters.AddWithValue("@FLAG", "TABLIST");

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);

            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }

    public DataTable Get_SpecialityCountReport(string list_case_id, string p_szCompanyID, string Visit_FromDate, string Visit_ToDate)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_TREATMENT_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", list_case_id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            if (Visit_FromDate != "")
                sqlCmd.Parameters.AddWithValue("@Dt_Visit_From", Visit_FromDate);
            if (Visit_ToDate != "")
                sqlCmd.Parameters.AddWithValue("@Dt_Visit_To", Visit_ToDate);

            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }

    public ArrayList GetDefaultDetailnew(string SZINSURANCEID)
    {

        sqlCon = new SqlConnection(strConn);
        ArrayList arr = new ArrayList();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_INSURANCE_ADDRESS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", SZINSURANCEID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDEFAULT");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["SZ_INS_ADDRESS_ID"] != DBNull.Value)
                {
                    arr.Add(dr["SZ_INS_ADDRESS_ID"].ToString());
                }
                if (dr["SZ_INSURANCE_ADDRESS"] != DBNull.Value)
                {
                    arr.Add(dr["SZ_INSURANCE_ADDRESS"].ToString());
                }
                if (dr["SZ_INSURANCE_CITY"] != DBNull.Value)
                {
                    arr.Add(dr["SZ_INSURANCE_CITY"].ToString());
                }
                else
                {
                    arr.Add("");
                }

                if (dr["SZ_INSURANCE_ZIP"] != DBNull.Value)
                {
                    arr.Add(dr["SZ_INSURANCE_ZIP"].ToString());
                }
                else
                {
                    arr.Add("");
                }

                if (dr["SZ_INSURANCE_STREET"] != DBNull.Value) { arr.Add(dr["SZ_INSURANCE_STREET"].ToString()); }
                else
                {
                    arr.Add("");
                }

                if (dr["SZ_INSURANCE_STATE_ID"] != DBNull.Value) { arr.Add(dr["SZ_INSURANCE_STATE_ID"].ToString()); }
                else
                {
                    arr.Add("");
                }

                //if (dr["I_UNIQUE_ID"] != DBNull.Value) { arr.Add(dr["I_UNIQUE_ID"].ToString()); }
                //else
                //{
                //    arr.Add("");
                //}
                if (dr["SZ_INSURANCE_STATE"] != DBNull.Value) { arr.Add(dr["SZ_INSURANCE_STATE"].ToString()); }
                else
                {
                    arr.Add("");
                }

                if (dr["I_DEFAULT"] != DBNull.Value) { arr.Add(dr["I_DEFAULT"].ToString()); }
                else
                {
                    arr.Add("");
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

        return arr;
    }
    // 5 April 2010, #145 , to display previous max chart no. on patient page  - sachin 
    public DataTable Get_Max_RFO_and_ChartNo(string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PATIENT_DATA_ENTRY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_MAX_CHART_NO");

            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }
    // 13 April,2010 show reschedule list on patient desk page for referring facility company --- sachin
    public DataSet GetRescheduleList(string flag, string caseID, string companyID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PATIENT_DESK_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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


    public DataTable Get_Outschedule_Tab_Information(string p_szCaseID, string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_OUSCHEDULE_TAB_INFORMATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);

            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }

    public DataTable Get_PatientDeskRoomList(string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_PATIENT_DESK_ROOM_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);
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
        return dt;
    }


    public String Get_PatientOfficeID(string p_szPatientID, string p_szCompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        String szOfficeID = "";

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_PATIENT_OFFICE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", p_szPatientID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_OFFICE_ID");
            SqlDataReader objDataRed = sqlCmd.ExecuteReader();
            while (objDataRed.Read())
            {
                szOfficeID = objDataRed["SZ_OFFICE_ID"].ToString();
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
        return szOfficeID;
    }

    public Boolean PatientExist(ArrayList Patient)
    {
        return false;
    }

    public String Check_Appointment(ArrayList objarr)
    {
        string eventid = "";
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_CHECK_APPOINTMENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objarr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objarr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ROOM_ID", objarr[2].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_DATE", objarr[3].ToString());
            sqlCmd.Parameters.AddWithValue("@sz_procedure_code", objarr[4].ToString());
            sqlCmd.Parameters.AddWithValue("@sz_patient_id", objarr[5].ToString());

            SqlDataReader objDataRed = sqlCmd.ExecuteReader();
            while (objDataRed.Read())
            {
                if (eventid == "")
                    eventid = objDataRed["sz_procedure_code"].ToString();
                else
                    eventid = eventid + " - " + objDataRed["sz_procedure_code"].ToString();
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
        return eventid;
    }

    public String Check_Appointment_For_Period(ArrayList objarr)
    {
        string eventid = "";
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_CHECK_APPOINTMENT_FOR_PERIOD", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objarr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objarr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ROOM_ID", objarr[2].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_DATE", objarr[3].ToString());
            sqlCmd.Parameters.AddWithValue("@DC_START_TIME", objarr[4].ToString());
            sqlCmd.Parameters.AddWithValue("@DC_END_TIME", objarr[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", objarr[6].ToString());


            SqlDataReader objDataRed = sqlCmd.ExecuteReader();
            while (objDataRed.Read())
            {
                eventid = objDataRed[0].ToString();
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
        return eventid;
    }

    public Boolean ExistChartNumber(string CompanyID, string ChartNo, string Flag)
    {
        sqlCon = new SqlConnection(strConn);
        string ExistChartno = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_EXIST_CHART_NO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_CHART_NO", ChartNo);
            sqlCmd.Parameters.AddWithValue("@FLAG", Flag);
            SqlDataReader dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                ExistChartno = dr[0].ToString();
                return true;
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return false;
    }

    public DataSet GetPatienView(string caseID, string companyID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_PATIENT_VIEW ", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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


    public DataSet Get_Patient_Visit_Summary_Report(ArrayList arr)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_PATIENT_VISIT_SUMMARY_REPORT_FOR_ALL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NUMBER", arr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", arr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arr[2].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", arr[3].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", arr[4].ToString());
            if (!arr[5].ToString().Equals("NA"))
            {
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE", arr[5].ToString());
            }
            sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE_OPEN", arr[6].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_DATE__OPEN", arr[7].ToString());

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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
    public DataTable Get_SpecialityCountReport_New(ArrayList arr)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_TREATMENT_DETAILS_FOR_ALL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NUMBER", arr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", arr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arr[2].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", arr[3].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", arr[4].ToString());
            if (!arr[5].ToString().Equals("NA"))
            {
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE", arr[5].ToString());
            }
            sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE_OPEN", arr[6].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_DATE__OPEN", arr[7].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_FROM_VISIT", arr[8].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_VISIT", arr[9].ToString());

            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }
    public DataTable Get_Patient_Visit_Summary_Report_Get_Treatment(ArrayList arr)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_PATIENT_VISIT_SUMMARY_REPORT_ALL_GET_TREATMENT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NUMBER", arr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", arr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arr[2].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", arr[3].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", arr[4].ToString());
            if (!arr[5].ToString().Equals("NA"))
            {
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE", arr[5].ToString());
            }
            sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE_OPEN", arr[6].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_DATE__OPEN", arr[7].ToString());

            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }

    public void UpdateInsuranceInformation(string szCasdeID, string ComapnyId, string szCarrierCode)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_CARRIER_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCasdeID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ComapnyId);
            sqlCmd.Parameters.AddWithValue("@SZ_CARRIER_CODE", szCarrierCode);

            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public String CheckCarriercode(string szcaseid, string szcompanyid)
    {
        string szCarriercode = "";
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_CARRIER_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szcaseid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompanyid);



            SqlDataReader objDataRed = sqlCmd.ExecuteReader();
            while (objDataRed.Read())
            {
                szCarriercode = objDataRed[0].ToString();
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
        return szCarriercode;
    }


    public DataSet GetInsuranceInfo(string I_EVENT_ID, string @I_EVENT_PROC_ID)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_INSURANCE_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", I_EVENT_ID);
            sqlCmd.Parameters.AddWithValue("@I_EVENT_PROC_ID", @I_EVENT_PROC_ID);

            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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
    public void UpdateInsuranceInfo(string i_event_id, string i_event_proc_id, string sz_insurance_id, string sz_insu_add_id, string sz_claim_no, string sz_policy_no, string sz_wcb_no, string dt_acc_date)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SAVE_VISIT_INS_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", i_event_id);
            sqlCmd.Parameters.AddWithValue("@I_EVENT_PROC_ID", i_event_proc_id);
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ID", sz_insurance_id);
            sqlCmd.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", sz_insu_add_id);
            sqlCmd.Parameters.AddWithValue("@SZ_CLAIM_NO", sz_claim_no);
            sqlCmd.Parameters.AddWithValue("@SZ_POLICY_NO", sz_policy_no);
            sqlCmd.Parameters.AddWithValue("@SZ_WCB_NO", sz_wcb_no);
            sqlCmd.Parameters.AddWithValue("@DT_ACCIDENT_DATE", dt_acc_date);


            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public void UpdateInsuranceInfoevent(string ComapnyId, string sz_insurance_id, string sz_insu_add_id, string sz_eventid, string szCaseID, string szClaimno, string szpolicyno, string szcasetypeid, string szpolicyholder, string dateofaccident, string szpatientphoneno, string szloactionid)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_add_insurance_for_event", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ComapnyId);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", sz_insurance_id);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ADD_ID", sz_insu_add_id);
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", sz_eventid);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_CLAIM_NO", szClaimno);
            sqlCmd.Parameters.AddWithValue("@SZ_POLICY_NO", szpolicyno);
            sqlCmd.Parameters.AddWithValue("@SZ_CASETYPE_ID", szcasetypeid);
            sqlCmd.Parameters.AddWithValue("@SZ_POLICYHOLDER", szpolicyholder);
            sqlCmd.Parameters.AddWithValue("@DT_DATEOF_ACCIDENT", dateofaccident);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_PHONE_NO", szpatientphoneno);
            sqlCmd.Parameters.AddWithValue("@BT_OC", "0");
            sqlCmd.Parameters.AddWithValue("@SZ_LOCATIONID", szloactionid);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }

    public void RemoveInsuranceInfoevent(string sz_eventid, string ComapnyId)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_remove_inusrance_info", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", sz_eventid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ComapnyId);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }
    public DataSet GetPatienInfoView(string caseID, string companyID)
    {
        DataSet ds = new DataSet();
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_PATIENTPOP_VIEW", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

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

    public DataTable Get_Outschedule_Tab_Information_LHR(string p_szCaseID, string p_szCompanyID, string p_szOfficeId)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_OUSCHEDULE_TAB_INFORMATION_FOR_PROVIDER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", p_szOfficeId);

            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }

    public void UpdateInsuranceInfoeventforOC(string ComapnyId, string sz_insurance_id, string sz_insu_add_id, string sz_eventid, string szCaseID, string szClaimno, string szpolicyno, string szcasetypeid, string szpolicyholder, string dateofaccident, string szpatientphoneno, string bt_Oc, string szloactionid)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_add_insurance_for_event", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", ComapnyId);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", sz_insurance_id);
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ADD_ID", sz_insu_add_id);
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", sz_eventid);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_CLAIM_NO", szClaimno);
            sqlCmd.Parameters.AddWithValue("@SZ_POLICY_NO", szpolicyno);
            sqlCmd.Parameters.AddWithValue("@SZ_CASETYPE_ID", szcasetypeid);
            sqlCmd.Parameters.AddWithValue("@SZ_POLICYHOLDER", szpolicyholder);
            sqlCmd.Parameters.AddWithValue("@DT_DATEOF_ACCIDENT", dateofaccident);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_PHONE_NO", szpatientphoneno);
            sqlCmd.Parameters.AddWithValue("@BT_OC", bt_Oc);
            sqlCmd.Parameters.AddWithValue("@SZ_LOCATIONID", szloactionid);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { sqlCon.Close(); }
    }
    //Created By Mohan
    public string Get__Compid_Caseid(string CaseID, string CompanyID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        string szReturn = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_ACCESS_TYPE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", CaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);

            dr = sqlCmd.ExecuteReader();

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
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }

        return szReturn;
    }

    public DataTable Get_SpecAttorneyCountReport(string lawfirmid, string p_szCompanyID, string Visit_FromDate, string Visit_ToDate)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_TREATMENT_DETAILS_FOR_ATT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_ASSIGN_LAW_FIRM", lawfirmid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            if (Visit_FromDate != "")
                sqlCmd.Parameters.AddWithValue("@Dt_Visit_From", Visit_FromDate);
            if (Visit_ToDate != "")
                sqlCmd.Parameters.AddWithValue("@Dt_Visit_To", Visit_ToDate);

            sqlda = new SqlDataAdapter(sqlCmd);
            dt = new DataTable();
            sqlda.Fill(dt);

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
        return dt;
    }

    //attorney list
    public DataSet loadattorney(string flag, string companyid)
    {
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(connectionString);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SP_MST_LEGAL_LOGIN", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@FLAG", flag);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            comm.Connection = conn;
            SqlDataAdapter adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);

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

    public DataSet loadgrid(string companyid, string attorney, string fromdate, string todate)//(ArrayList arrlist
    {
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        SqlConnection conn;
        SqlCommand comm;
        string strconnect = ConfigurationManager.AppSettings["Connection_String"].ToString();
        conn = new SqlConnection(strconnect);
        try
        {
            conn.Open();
            comm = new SqlCommand("SP_GET_LAWFIRM_COUNT", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID ", companyid);
            comm.Parameters.AddWithValue("@SZ_ASSIGN_LAW_FIRM", attorney);
            if (todate.ToString() != "")
            {
                comm.Parameters.AddWithValue("@DT_TO_DATE", todate);
            }
            if (fromdate.ToString() != "")
            {
                comm.Parameters.AddWithValue("@DT_FROM_DATE", fromdate);
            }
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
    public void SaveEmployer(string employerId, string employeraddressid, string companyid, string caseid)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SAVE_EMPLOYER_FOR_CASE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_EMP_ADDRESS_ID", employeraddressid);
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_ID", employerId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseid);
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
    public DataSet GetCaseEmployer(string casid, string companyid)
    {

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_EMPLOYER_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", casid);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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

    public DataSet GetEmployeCase(ArrayList arrList)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SEARCH_FOR_MULT_VISIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrList[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_ID", arrList[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", arrList[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", arrList[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_ACTIVE", arrList[4].ToString());
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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

    public DataSet GetEmployerCompanyAddress(string EmployerCompany, string CompanyID)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strConn);
        string address = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_EMPLOYER_ADDRESS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_EMPLOYER_ID", EmployerCompany);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETEMPLOYERADDRESSLIST");
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
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

    public ArrayList GetEmployerAddressDetails(string employerAddressID)
    {

        sqlCon = new SqlConnection(strConn);
        ArrayList arr = new ArrayList();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_EMPLOYER_ADDRESS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();


            sqlCmd.Parameters.AddWithValue("@SZ_EMP_ADDRESS_ID", employerAddressID);


            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["SZ_EMP_ADDRESS_ID"] != DBNull.Value) { arr.Add(dr["SZ_EMP_ADDRESS_ID"].ToString()); }
                if (dr["SZ_EMPLOYER_ID"] != DBNull.Value) { arr.Add(dr["SZ_EMPLOYER_ID"].ToString()); }
                if (dr["SZ_EMPLOYER_ADDRESS"] != DBNull.Value) { arr.Add(dr["SZ_EMPLOYER_ADDRESS"].ToString()); }


                if (dr["SZ_EMPLOYER_CITY"] != DBNull.Value) { arr.Add(dr["SZ_EMPLOYER_CITY"].ToString()); }
                if (dr["SZ_EMPLOYER_STATE"] != DBNull.Value) { arr.Add(dr["SZ_EMPLOYER_STATE"].ToString()); }
                if (dr["SZ_EMPLOYER_ZIP"] != DBNull.Value) { arr.Add(dr["SZ_EMPLOYER_ZIP"].ToString()); }
                if (dr["SZ_EMPLOYER_STREET"] != DBNull.Value) { arr.Add(dr["SZ_EMPLOYER_STREET"].ToString()); }
                if (dr["SZ_COMPANY_ID"] != DBNull.Value) { arr.Add(dr["SZ_COMPANY_ID"].ToString()); }
                if (dr["SZ_EMPLOYER_STATE_ID"] != DBNull.Value) { arr.Add(dr["SZ_EMPLOYER_STATE_ID"].ToString()); }
                if (dr["sz_fax_number"] != DBNull.Value) { arr.Add(dr["sz_fax_number"].ToString()); } else { arr.Add(""); }
                if (dr["SZ_EMPLOYER_PHONE"] != DBNull.Value) { arr.Add(dr["SZ_EMPLOYER_PHONE"].ToString()); } else { arr.Add(""); }
                if (dr["sz_contact_person"] != DBNull.Value) { arr.Add(dr["sz_contact_person"].ToString()); } else { arr.Add(""); }

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

        return arr;
    }

    public ArrayList GetDefaultDetail(string SZINSURANCEID)
    {

        sqlCon = new SqlConnection(strConn);
        ArrayList arr = new ArrayList();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_EMPLOYER_ADDRESS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", SZINSURANCEID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDEFAULT");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["SZ_EMP_ADDRESS_ID"] != DBNull.Value)
                {
                    arr.Add(dr["SZ_EMP_ADDRESS_ID"].ToString());
                }
                if (dr["SZ_EMPLOYER_ADDRESS"] != DBNull.Value)
                {
                    arr.Add(dr["SZ_EMPLOYER_ADDRESS"].ToString());
                }
                if (dr["SZ_EMPLOYER_CITY"] != DBNull.Value)
                {
                    arr.Add(dr["SZ_EMPLOYER_CITY"].ToString());
                }
                else
                {
                    arr.Add("");
                }

                if (dr["SZ_EMPLOYER_ZIP"] != DBNull.Value)
                {
                    arr.Add(dr["SZ_EMPLOYER_ZIP"].ToString());
                }
                else
                {
                    arr.Add("");
                }

                if (dr["SZ_EMPLOYER_STREET"] != DBNull.Value) { arr.Add(dr["SZ_EMPLOYER_STREET"].ToString()); }
                else
                {
                    arr.Add("");
                }

                if (dr["SZ_EMPLOYER_STATE_ID"] != DBNull.Value) { arr.Add(dr["SZ_EMPLOYER_STATE_ID"].ToString()); }
                else
                {
                    arr.Add("");
                }

                //if (dr["I_UNIQUE_ID"] != DBNull.Value) { arr.Add(dr["I_UNIQUE_ID"].ToString()); }
                //else
                //{
                //    arr.Add("");
                //}
                if (dr["SZ_EMPLOYER_STATE"] != DBNull.Value) { arr.Add(dr["SZ_EMPLOYER_STATE"].ToString()); }
                else
                {
                    arr.Add("");
                }

                if (dr["I_DEFAULT"] != DBNull.Value) { arr.Add(dr["I_DEFAULT"].ToString()); }
                else
                {
                    arr.Add("");
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

        return arr;
    }
}
