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

public class Bill_Sys_DisplaySpeciality
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

    public Bill_Sys_DisplaySpeciality()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataTable getSpecialityCount(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_SPECIALITY_COUNT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@dt_from_event_date", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@dt_to_event_date", objAL[3].ToString());
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@sz_doctor_id", objAL[4].ToString());}
            sqlda = new SqlDataAdapter(sqlCmd);
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
        //return ds.Tables[1];
        return ds.Tables[0];
    }

    public DataSet getSpecialityPatientList(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_SPECIALITY_PATIENT_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_DATE", objAL[1].ToString()); 
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString()); 
            sqlda = new SqlDataAdapter(sqlCmd);
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

    public DataSet GetSpecialityList(string sz_company_id)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PROCEDURE_GROUP",sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID",sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG","GET_PROCEDURE_GROUP_LIST");
            sqlda = new SqlDataAdapter(sqlCmd);
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

    public void InsertAssociateSpeciality(string sz_first_specialityID, string sz_second_specialityID, string sz_companyId)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_INSERT_ASSOCIATE_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_FIRST_SPECIALITY_ID",sz_first_specialityID);
            sqlCmd.Parameters.AddWithValue("@SZ_SECOND_SPECIALITY_ID", sz_second_specialityID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyId);
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

    public string GetLatestProcedureGroupID(string sz_company_id)
    {
        sqlCon = new SqlConnection(strConn);
        SqlDataReader dreader;
        string LatestID = "";
        try 
	    {	        
		    sqlCon.Open();
            sqlCmd = new SqlCommand("SP_INSERT_ASSOCIATE_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "PROCEDURE_GROUP_ID");
            dreader = sqlCmd.ExecuteReader();
            while(dreader.Read())
            {
                LatestID = dreader["LatestId"].ToString();
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
        return LatestID;
    }

    public DataSet GetAssociateSpecialityData(string sz_first_specialityID, string sz_company_id)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_INSERT_ASSOCIATE_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_FIRST_SPECIALITY_ID", sz_first_specialityID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDATA");
            sqlda = new SqlDataAdapter(sqlCmd);
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

    public void DeleteUpdateAssociateSpeciality(string id, string sz_first_specialityID, string sz_companyId)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_INSERT_ASSOCIATE_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_FIRST_SPECIALITY_ID", sz_first_specialityID);
            sqlCmd.Parameters.AddWithValue("@I_SPECIALITY_LINK_ID", id);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyId);
            sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATEDELETE");
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

    public void DeleteAssociateSpeciality(string sz_first_specialityID, string sz_companyId)
    {
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_INSERT_ASSOCIATE_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_FIRST_SPECIALITY_ID", sz_first_specialityID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyId);
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

    public DataTable GetSpecialityToDelete(string sz_first_specialityID, string sz_companyId)
    {
        sqlCon = new SqlConnection(strConn);
        SqlDataReader dreader;
        DataTable Id = new DataTable();
          
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_INSERT_ASSOCIATE_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_FIRST_SPECIALITY_ID", sz_first_specialityID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyId);
            sqlCmd.Parameters.AddWithValue("@FLAG", "SELECTBEFOREDELETE");
            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(Id);
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
        return Id;
    }

    public int MoveVisit(string sz_company_id, string sz_cas_id, string sz_event_id, string sz_doctor_id, string sz_event_date, string sz_start_event_time, string sz_start_event_type, string sz_end_event_time, string sz_end_event_type)
    {
        sqlCon = new SqlConnection(strConn);
        int iReturn = 0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MOVE_VISIT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPNY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_cas_id);
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", sz_event_id);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_doctor_id);
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_DATE", sz_event_date);
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_TIME", sz_start_event_time);

            sqlCmd.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", sz_start_event_type);
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_END_TIME", sz_end_event_time);
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", sz_end_event_type);

          iReturn=  sqlCmd.ExecuteNonQuery();
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
        return iReturn;
    }

    public DataSet CheckEvent(string szCaseId, string szPatientid, string szDoctorId, string szEventDate, string szDateTime, string szDateTimetype,string szCompanyID)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_CHECK_VISIT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_EVENT_DATE", szEventDate);
            sqlCmd.Parameters.AddWithValue("@SZ_TIME", szDateTime);
            sqlCmd.Parameters.AddWithValue("@SZ_TIME_TYPE", szDateTimetype);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", szPatientid);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", szDoctorId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
           
            sqlda = new SqlDataAdapter(sqlCmd);
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

    public int InsertNodeType(string sz_company_id, string sz_spec_name, int i_reff_bit)
    {
        sqlCon = new SqlConnection(strConn);
        int iReturn = 0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_INSERT_NODES_FORNEW_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@SZ_SPEC_NAME", sz_spec_name);
            sqlCmd.Parameters.AddWithValue("@SZ_REFFERAL_BIT", i_reff_bit);
            iReturn = sqlCmd.ExecuteNonQuery();
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
        return iReturn;
    }

    public int DeleteNodeType(string sz_company_id, string sz_node_type, string i_reff_bit, string szProcName, string szProcID)
    {
        sqlCon = new SqlConnection(strConn);
        int iReturn = 0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DELETE_NODES_FOR_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@SZ_NODE_TYPE", sz_node_type);
            sqlCmd.Parameters.AddWithValue("@SZ_REFFERAL_BIT", i_reff_bit);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szProcID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP", szProcName);
            iReturn = sqlCmd.ExecuteNonQuery();
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
        return iReturn;
    }

    public int UpdateNodeType(string sz_company_id, string sz_node_type, string sz_node_name)
    {
        sqlCon = new SqlConnection(strConn);
        int iReturn = 0;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_NODES_FORNEW_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_NODE_TYPE", sz_node_type);
            sqlCmd.Parameters.AddWithValue("@SZ_NODE_NAME", sz_node_name);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            
            iReturn = sqlCmd.ExecuteNonQuery();
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
        return iReturn;
    }

    public DataSet GetCaseDocuments(string sz_company_id, string CaseID)
    {
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DOCUMENTS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", Convert.ToInt32(CaseID));
            sqlda = new SqlDataAdapter(sqlCmd);
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

    public string CopyDocuments(string sz_company_id, string CaseID, int nodeid, string FileName, string LoginUser)
    {
        string szRetPath = "";
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_COPY_DOCUMENTS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", Convert.ToInt32(CaseID));
            sqlCmd.Parameters.AddWithValue("@SZ_NODE_ID", Convert.ToInt32(nodeid));
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", FileName);
            sqlCmd.Parameters.AddWithValue("@SZ_LOGIN_USER", LoginUser);

            SqlParameter paramPath = new SqlParameter("@Path", SqlDbType.NVarChar, 255);
            paramPath.Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add(paramPath);

            sqlCmd.ExecuteNonQuery();

            szRetPath = sqlCmd.Parameters["@Path"].Value.ToString();
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
        return szRetPath;
    }

    public string GetCopiedCaseID(string OldCompanyid, string NewCompanyId, int caseid)
    {
        string szRetCaseID = "";
        sqlCon = new SqlConnection(strConn);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_COPIED_CASEID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseid);
            sqlCmd.Parameters.AddWithValue("@SZ_OLD_COMPANY_ID", OldCompanyid);
            sqlCmd.Parameters.AddWithValue("@SZ_NEW_COMPANY_ID", NewCompanyId);


            SqlParameter paramPath = new SqlParameter("@SZ_NEW_CASE_ID", SqlDbType.Int);
            paramPath.Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add(paramPath);

            sqlCmd.ExecuteNonQuery();

            szRetCaseID = sqlCmd.Parameters["@SZ_NEW_CASE_ID"].Value.ToString();
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
        return szRetCaseID;
    }

    public DataSet GetMasterNodes(string sz_company_id, string CaseID)
    {
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "SP_GET_TREE_NODES";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETMASTERNODE");
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", CaseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            //sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { sqlCon.Close(); }
        return null;
    }

    public DataSet GetChildNodes(int p_inodeid, string sz_company_id, string CaseID)
    {
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandText = "SP_GET_TREE_NODES";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Parameters.AddWithValue("@I_PARENT_ID", p_inodeid);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", CaseID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETCHILDNODE");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { sqlCon.Close(); }
        return null;
    }
}
