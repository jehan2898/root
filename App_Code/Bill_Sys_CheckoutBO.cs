using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Bill_Sys_CheckoutBO
/// </summary>
public class Bill_Sys_CheckoutBO
{
    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    public Bill_Sys_CheckoutBO()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataSet GetDoctorUserID(string sz_UserID, string sz_CompanyID)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("SP_Doctor_UserID", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_USER_ID", sz_UserID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GETDOCTORID");
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet GetProcedureGroupID(string sz_User_ID, string sz_CompanyID)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("SP_CO_GET_SPECIALITY", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_USER_ID", sz_User_ID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return null;
        }
        finally { conn.Close(); }
    }

    public DataSet GetPatientsListSpeciality(ArrayList _objarrLst)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("SP_CO_GET_PATIENT", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_USER_ID", _objarrLst[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", _objarrLst[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _objarrLst[2].ToString());
            comm.Parameters.AddWithValue("@DT_FROM_DATE", _objarrLst[3].ToString());
            comm.Parameters.AddWithValue("@DT_TO_DATE", _objarrLst[4].ToString());
            comm.Parameters.AddWithValue("@I_STATUS", _objarrLst[5].ToString());
            //comm.Parameters.AddWithValue("@SZ_VISIT_TYPE_ID", _objarrLst[6].ToString());
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet GetCaseDiagnosis(string sz_Case_ID, string sz_company_ID, string sz_Procedure_ID, string sz_User_id)
    {
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("SP_CO_GET_DETAILS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", sz_Case_ID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_ID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", sz_Procedure_ID);
            comm.Parameters.AddWithValue("@SZ_USER_ID", sz_User_id);
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return null;
    }

    public void saveProcedureCodes(ArrayList p_objArrayList)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_CO_SAVE_PROECEDURE_CODE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_INDEX_ID", p_objArrayList[0].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", p_objArrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objArrayList[2].ToString());
            comm.ExecuteNonQuery();
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    public DataSet PatientName(string sz_Event_ID)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adop;
        try
        {
            con = new SqlConnection(strsqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_PATIENT_NAME_EVENT_ID", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_Event_ID);

            cmd.CommandType = CommandType.StoredProcedure;
            adop = new SqlDataAdapter(cmd);
            adop.Fill(ds);
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
        return ds;
    }

    public void save_PT_DocMang(ArrayList p_objArrayList)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_CO_SAVE_PT_NOTES_PDF";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objArrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PT_FILE_NAME", p_objArrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PT_FILE_PATH", p_objArrayList[2].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_NAME", p_objArrayList[3].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objArrayList[4].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", p_objArrayList[5].ToString());
            comm.ExecuteNonQuery();
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    public DataSet PTNotesPatientTreatment(string I_EVENT_ID, string SZ_COMPANY_ID)
    {
        DataSet PTNotes = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adop;
        try
        {
            con = new SqlConnection(strsqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_PT_NOTES", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", I_EVENT_ID);
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID);
            cmd.Parameters.AddWithValue("@FLAG", "GET_TREATMENT");

            cmd.CommandType = CommandType.StoredProcedure;
            adop = new SqlDataAdapter(cmd);
            adop.Fill(PTNotes);
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
        return PTNotes;

    }

    public string PTNotesPatientInformation(string EventID, string storeproc)
    {
        string strBillID = "";
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = storeproc;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID", EventID);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                strBillID = Convert.ToString(dr[0]);
            }
            return strBillID;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }
        return null;
    }

    public DataSet PTNotesPatientTreatmentDelete(string I_EVENT_ID)
    {
        DataSet PTNotesDelete = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adop;
        try
        {
            con = new SqlConnection(strsqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_PT_NOTES", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", I_EVENT_ID);
            cmd.Parameters.AddWithValue("@FLAG", "DELETE_TREATMENT");
            cmd.CommandType = CommandType.StoredProcedure;
            adop = new SqlDataAdapter(cmd);
            adop.Fill(PTNotesDelete);
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
        return PTNotesDelete;

    }

    public void UpdateSignPath(string sz_EventId, string sz_Patient_ImagePath, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_PT_NOTES", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGN_PATH", sz_Patient_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_SIGN_PATH", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATESIGNIMAGEPATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public DataSet getCheckinList(String CaseID, String CompanyID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            comm = new SqlCommand("SP_GET_CHECKIN_LIST", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", CaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }

    public DataSet GetNodeID(string CompanyID, string sz_Eventid)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        //string sz_NodeID = "";
        try
        {
            comm = new SqlCommand("SP_GET_PT_NODE_ID", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_ID", sz_Eventid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsReturn);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }

    public void UpdateIMSignPath(string sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_IM_IMAGE_PATH", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_SIGN_PATH", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATESIGNIMAGEPATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void UpdateIMSignPathACCU_REEVAL(string sz_PATIENT_ID, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_IM_IMAGE_PATH_MST_ACCU_REEVAL", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_PATIENT_ID", sz_PATIENT_ID);
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_SIGN_PATH", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATESIGNIMAGEPATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void save_IM_DocMang(ArrayList p_objArrayList)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_IM_SAVE_PT_NOTES_PDF";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objArrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PT_FILE_NAME", p_objArrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PT_FILE_PATH", p_objArrayList[2].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_NAME", p_objArrayList[3].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objArrayList[4].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", p_objArrayList[5].ToString());
            comm.ExecuteNonQuery();
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }


    public Boolean CheckImgPath(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_CHECK_IMAGE_FOR_IM_FORM", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter objDAdap = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        objDAdap.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public Boolean CheckImgPathAC(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_CHECK_IMAGE_FOR_ACCU_REEVAL", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_PATIENT_ID", iEventId);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = "found";
        }
        if (!str.Equals(""))
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    public void save_AC_REEVAL_DM(ArrayList p_objArrayList)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_CO_SAVE_AC_REEVAL";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objArrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_AC_FILE_NAME", p_objArrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_AC_FILE_PATH", p_objArrayList[2].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_NAME", p_objArrayList[3].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objArrayList[4].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", p_objArrayList[5].ToString());
            comm.ExecuteNonQuery();
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
    public Boolean CheckIMData(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_CHECKDATA", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = "found";
        }
        if (!str.Equals(""))
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    public void SaveCODigosisCode(int iEventId, ArrayList AL)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();


        for (int i = 0; i < AL.Count; i++)
        {
            SqlCommand cmd = new SqlCommand("SP_CO_SAVE_DIAGNOSIS_CODE", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
            cmd.Parameters.AddWithValue("@I_INDEX", AL[i].ToString());
            cmd.Parameters.AddWithValue("@FLAG", "ADD");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
        }

        conn.Close();


    }

    public void deleteCODigosisCode(String iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();

        try
        {
            SqlCommand cmd = new SqlCommand("SP_CO_SAVE_DIAGNOSIS_CODE", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
            cmd.Parameters.AddWithValue("@FLAG", "DELETE");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally { conn.Close(); }


    }

    public DataSet GetCODiagnosis(string iEventID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        //string sz_NodeID = "";
        try
        {
            comm = new SqlCommand("SP_CO_GET_DIAGNOSIS_CODE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", iEventID);
            comm.Parameters.AddWithValue("@FLAG", "GET_DIAGNOSIS_CODE");
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsReturn);
            return dsReturn;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }



    //public void save_AC_REEVAL_DM(ArrayList p_objArrayList)
    //{
    //    try
    //    {
    //        conn = new SqlConnection(strsqlCon);
    //        conn.Open();
    //        #region "Save Event Reffer Procedure"
    //        comm = new SqlCommand();
    //        comm.CommandText = "SP_CO_SAVE_AC_REEVAL";
    //        comm.CommandType = CommandType.StoredProcedure;
    //        comm.Connection = conn;
    //        comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objArrayList[0].ToString());
    //        comm.Parameters.AddWithValue("@SZ_AC_FILE_NAME", p_objArrayList[1].ToString());
    //        comm.Parameters.AddWithValue("@SZ_AC_FILE_PATH", p_objArrayList[2].ToString());
    //        comm.Parameters.AddWithValue("@SZ_USER_NAME", p_objArrayList[3].ToString());
    //        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objArrayList[4].ToString());
    //        comm.Parameters.AddWithValue("@I_EVENT_ID", p_objArrayList[5].ToString());
    //        comm.ExecuteNonQuery();
    //        #endregion
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //}

    public bool checkImageForAccuFollowup(string p_szEventID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            comm = new SqlCommand("SP_ACCU_FOLLOWUP", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_ID", p_szEventID);
            comm.Parameters.AddWithValue("@FLAG", "IS_IMAGE_EXIST");
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsReturn);
            if (dsReturn.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
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
        return false;
    }
    public void updateAccFollowupPatientPath(string sz_EventId, string sz_Patient_ImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SP_ACCU_FOLLOWUP", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGN_PATH", sz_Patient_ImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATE_PATIENT_SIGN_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }
    public void updateAccFollowupProviderPath(string sz_EventId, string sz_Provider_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_ACCU_FOLLOWUP", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PROVIDER_SIGN_PATH", sz_Provider_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATE_PROVIDER_SIGN_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void SaveIMFollowupSignPath(string sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_IM_FOLLOWING_PATH", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PROVIDER_SIGN_PATH", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "SAVE_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }
    public Boolean ChekIMFollowupSignPath(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_MST_IM_FOLLOWING_PATH", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.Parameters.AddWithValue("@FLAG", "IS_PATH_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = "found";
        }
        if (!str.Equals(""))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }

    }



    public Boolean ChekCOChiroSignPath(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_MST_CO_CHIRO_PATH", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.Parameters.AddWithValue("@FLAG", "IS_PATH_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = "found";
        }
        if (!str.Equals(""))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }

    }


    public void SaveCOChiroSignPath(string sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_CO_CHIRO_PATH", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PROVIDER_SIGN_PATH", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "SAVE_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void UpdateRomSignPath(string sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_ROM_SIGNPATH", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_ROM_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_SIGNATURE", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATESIGNIMAGEPATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void UpdateChairoSign(string sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_MST_CHIRO_CA", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_SIGNATURE", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "SAVE_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void UpdatePatientChairoSign(string sz_EventId, string sz_Doctor_ImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_MST_CHIRO_CA", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_INITIAL_PATH", sz_Doctor_ImagePath);
            //  cmd.Parameters.AddWithValue("@SZ_BARCODE", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "PATIENT_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public bool CheckImagePath(int c_EventID, string spName)
    {
        conn = new SqlConnection(strsqlCon);
        SqlCommand cmd = new SqlCommand(spName, conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_Event_ID", c_EventID);
        cmd.Parameters.AddWithValue("@FLAG", "IS_PATH_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        conn.Open();
        dr = cmd.ExecuteReader();
        string imgstr = " ";
        while (dr.Read())
        {
            imgstr = "found";

        }
        if (!imgstr.Equals(" "))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }


    }

    public bool CheckRomImagePath(int c_EventID, string spName)
    {
        conn = new SqlConnection(strsqlCon);
        SqlCommand cmd = new SqlCommand(spName, conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_ROM_ID", c_EventID);
        cmd.Parameters.AddWithValue("@FLAG", "IS_PATH_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        conn.Open();
        dr = cmd.ExecuteReader();
        string imgstr = " ";
        while (dr.Read())
        {
            imgstr = "found";
        }
        if (!imgstr.Equals(" "))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }


    }


    public void save_ROM_DocMang(ArrayList p_objArrayList)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_CO_SAVE_ROM_NOTES_PDF";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objArrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_ROM_FILE_NAME", p_objArrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_ROM_FILE_PATH", p_objArrayList[2].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_NAME", p_objArrayList[3].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objArrayList[4].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", p_objArrayList[5].ToString());
            comm.ExecuteNonQuery();
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    public void save_CHIRO_DocMang(ArrayList p_objArrayList)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_CHAIRO_SAVE__NOTES_PDF";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objArrayList[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PT_FILE_NAME", p_objArrayList[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PT_FILE_PATH", p_objArrayList[2].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_NAME", p_objArrayList[3].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objArrayList[4].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", p_objArrayList[5].ToString());
            comm.ExecuteNonQuery();
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }



    public Boolean ChekCMALLSignPath(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_MST_CM_ALL_PATH", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.Parameters.AddWithValue("@FLAG", "IS_PATH_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = "found";
        }
        if (!str.Equals(""))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }

    }


    public void SaveCMALLSignPath(string sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_CM_ALL_PATH", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@IMG_SIGNATURE", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@IMG_BARCODE", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "SAVE_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    //public Boolean ChekCMALLQuestionaireSignPath(int iEventId)
    //{
    //    conn = new SqlConnection(strsqlCon);
    //    conn.Open();
    //    SqlCommand cmd = new SqlCommand("SP_MST_CM_ALL_Questionaire_PATH", conn);
    //    cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
    //    cmd.Parameters.AddWithValue("@FLAG", "IS_PATH_EXIST");
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    SqlDataReader dr;
    //    dr = cmd.ExecuteReader();
    //    string str = "";
    //    while (dr.Read())
    //    {
    //        str = "found";
    //    }
    //    if (!str.Equals(""))
    //    {
    //        conn.Close();
    //        return true;
    //    }
    //    else
    //    {
    //        conn.Close();
    //        return false;
    //    }

    //}

    public void SaveCMALLQuestionaireSignPath(string sz_EventId, string sz_Doctor_ImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_CM_ALL_Questionaire_PATH", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@IMG_QUESTIONARE_SIGNATURE", sz_Doctor_ImagePath);
           
            cmd.Parameters.AddWithValue("@FLAG", "SAVE_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }




    public Boolean ChekECGDoctorSignPath(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_MST_ECG_CHK", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.Parameters.AddWithValue("@FLAG", "IS_PATH_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = "found";
        }
        if (!str.Equals(""))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }

    }


    public void SaveECGDoctorSignPath(string sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_ECG_CHK", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_SIGNATURE", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "SAVE_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void SaveECGPatientSignPath(string sz_EventId, string sz_Doctor_ImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_ECG_CHK_PATIENT_SIGN", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGNATURE", sz_Doctor_ImagePath);
         
            cmd.Parameters.AddWithValue("@FLAG", "SAVE_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }


    public void UpdatePhysicaltherapySignPath(String sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_PHYSICAL_THERAPY_AQUA", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_SIGNATURE", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "DOCTOR_UPDATE_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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


    }



    public void UpdatePatientPhyscalTherapySign(string sz_EventId, string sz_Doctor_ImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_PATIENT_PHYSICAL_SIGN", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGN", sz_Doctor_ImagePath);
            //  cmd.Parameters.AddWithValue("@SZ_BARCODE", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "PATIENT_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public bool CheckSignExist(int c_EventID)
    {
        conn = new SqlConnection(strsqlCon);
        SqlCommand cmd = new SqlCommand("SP_PATH_EXIST", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", c_EventID);
        cmd.Parameters.AddWithValue("@FLAG", "IS_PATH_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        conn.Open();
        dr = cmd.ExecuteReader();
        string imgstr = " ";
        while (dr.Read())
        {
            imgstr = "found";

        }
        if (!imgstr.Equals(" "))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }


    }



    public Boolean ChekCORefferalSignPath(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_MST_CO_REFFERAL_PATH", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.Parameters.AddWithValue("@FLAG", "IS_PATH_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = "found";
        }
        if (!str.Equals(""))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }

    }


    public void SaveCORefferalSignPath(string sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_CO_REFFERAL_PATH", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_SIGNATURE", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "SAVE_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public Boolean ChekCMINITIALEVALSignPath(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_MST_CM_FOLLOWING_PATH", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.Parameters.AddWithValue("@FLAG", "IS_PATH_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = "found";
        }
        if (!str.Equals(""))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }

    }

    public void SaveCMINITIALEVALSignPath(string sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_CM_FOLLOWING_PATH", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_IMG_DOCTOR_SIGNATURE", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "SAVE_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void updateAccInitialDoctorPath(string sz_EventId, string sz_Provider_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_AC_ACCU_INITIAL_1", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_SIGNATURE", sz_Provider_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            //cmd.Parameters.AddWithValue("@FLAG", "UPDATE_DOCTOR_SIGN_PATH");
            cmd.Parameters.AddWithValue("@FLAG", "UPDATE_PROVIDER_SIGN_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }
    
    public Boolean ChekAccInitialDoctorPath(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_MST_AC_ACCU_INITIAL_1", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.Parameters.AddWithValue("@FLAG", "IS_IMAGE_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = "found";
        }
        if (!str.Equals(""))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }

    }

    public string  ChekVisitStaus(int iEventId, string  sz_Company_id)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_TXN_EVENT_CALENDAR_HELPER", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_id);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        if (dr.Read())
        {
            str = dr.GetValue(0).ToString();  
        }
        return str;
        conn.Close();
    }
    public ArrayList ChekCheckBoxStaus(int iEventId)
    {
        ArrayList objal = new ArrayList();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_MST_INITIAL_EXAMINATION_CHECKBOX", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        
        while (dr.Read())
        {
            objal.Add(dr.GetValue(0).ToString());
            objal.Add(dr.GetValue(1).ToString());
            objal.Add(dr.GetValue(2).ToString());
            objal.Add(dr.GetValue(3).ToString());
            objal.Add(dr.GetValue(4).ToString());
            objal.Add(dr.GetValue(5).ToString());
            objal.Add(dr.GetValue(6).ToString());
            objal.Add(dr.GetValue(7).ToString());
            objal.Add(dr.GetValue(8).ToString()); 
            objal.Add(dr.GetValue(9).ToString());
            objal.Add(dr.GetValue(10).ToString());
            objal.Add(dr.GetValue(11).ToString());
            objal.Add(dr.GetValue(12).ToString());
            objal.Add(dr.GetValue(13).ToString());
            objal.Add(dr.GetValue(14).ToString());
            objal.Add(dr.GetValue(15).ToString());
          
        }
        return objal;
        conn.Close();
    }
    public ArrayList ChekIMCheckBoxStaus(int iEventId)
    {
        ArrayList objal = new ArrayList();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_MST_IM_INITIAL_EXAMINATION_CHECKBOX", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            objal.Add(dr.GetValue(0).ToString());
            objal.Add(dr.GetValue(1).ToString());
            objal.Add(dr.GetValue(2).ToString());
            objal.Add(dr.GetValue(3).ToString());
            objal.Add(dr.GetValue(4).ToString());
            objal.Add(dr.GetValue(5).ToString());
            objal.Add(dr.GetValue(6).ToString());
            objal.Add(dr.GetValue(7).ToString());
            objal.Add(dr.GetValue(8).ToString());
            objal.Add(dr.GetValue(9).ToString());
            objal.Add(dr.GetValue(10).ToString());
            objal.Add(dr.GetValue(11).ToString());
            objal.Add(dr.GetValue(12).ToString());
            objal.Add(dr.GetValue(13).ToString());
             

        }
        return objal;
        conn.Close();
    }

    public Boolean PatientIntekSignPath(string iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_PATIENTINTEK_INFORMATION", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@SZ_CASE_ID", iEventId);
        cmd.Parameters.AddWithValue("@FLAG", "IS_SIGN_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = "found";
        }
        if (!str.Equals(""))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }

    }
    public void UpdatePatientIntekSign(string iEventID, string PatientPath, string BarCodePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_SIGN", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@SZ_CASE_ID", iEventID);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGNATURE_PATH", PatientPath);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_BARCODE_PATH", BarCodePath);
            //cmd.Parameters.AddWithValue("@FLAG", "UPDATE_DOCTOR_SIGN_PATH");
            //cmd.Parameters.AddWithValue("@FLAG", "UPDATE_PROVIDER_SIGN_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }
    public Boolean  SignPathLine(string iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_MST_LIEN", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@SZ_CASE_ID", iEventId);
        cmd.Parameters.AddWithValue("@FLAG", "IS_IMAGE_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = "found";
        }
        if (!str.Equals(""))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }
    }

    public void LienPatientPath(string sz_EventId, string sz_representive,  string sz_BarcodeImagePath,string sz_patient_path,string sz_attoreny_path)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_LIEN", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGNATURE", sz_patient_path);
            cmd.Parameters.AddWithValue("@SZ_ATTORNEY_SIGNATURE", sz_attoreny_path);
            cmd.Parameters.AddWithValue("SZ_PATIENT_REPRESNTIVE_SIGNATURE", sz_representive);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATE");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void TestIntakeMri(string sz_EventId, string sz_BarcodeImagePath, string sz_Patient_path, string sz_Attorney_Path, string sz_ParentOfMinorPatient_path, string sz_Gardian_Path)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_TEST_FACILITY_INTAKE", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGNATURE", sz_Patient_path);
            cmd.Parameters.AddWithValue("@SZ_ATTORNEY_SIGNATURE", sz_Attorney_Path);
            cmd.Parameters.AddWithValue("@SZ_PARENT_OF_MINOR_PATIENT_SIGNATURE", sz_ParentOfMinorPatient_path);
            cmd.Parameters.AddWithValue("@SZ_GARDIAN_SUGNATURE",sz_Gardian_Path );
            cmd.Parameters.AddWithValue("@SZ_BARCODE", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATE");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void MRI_Questionary_SaveSignPath(string sz_EventId, string sz_BarcodeImagePath, string sz_Patient_path)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_INSERT_MRI_QUESTIONARY", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_SIGN_PATH", sz_Patient_path);
           
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATE");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }
    public Boolean ChekIntakeMriPath(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_TEST_FACILITY_INTAKE_CHEK_PATH", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
       
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = dr[0].ToString();
        }


        if (!str.Equals(""))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }

    }

    public Boolean ChekMRIQuestionaire(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_MRI_GET_SIGNATURE", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);

        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = dr[0].ToString();
        }


        if (!str.Equals(""))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }

    }

    public Boolean ChekAOBPatientPath(int iEventId)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_MST_AOB", conn);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        cmd.Parameters.AddWithValue("@I_EVENT_ID", iEventId);
        cmd.Parameters.AddWithValue("@FLAG", "IS_IMAGE_EXIST");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        string str = "";
        while (dr.Read())
        {
            str = "found";
        }
        if (!str.Equals(""))
        {
            conn.Close();
            return true;
        }
        else
        {
            conn.Close();
            return false;
        }

    }

    public void UpdatePatientAOBSign(string sz_EventId, string sz_Doctor_ImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_AOB", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGN_PATH", sz_Doctor_ImagePath);
            //  cmd.Parameters.AddWithValue("@SZ_BARCODE", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATE_PATIENT_SIGN_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }


    public void UpdateAOBSign(string sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_AOB", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PROVIDER_SIGN_PATH", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATE_PROVIDER_SIGN_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }


    public void PatientIntakeBarcodeForPrint(string sz_Event_Id,string sz_BrcodePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_PATIENT_INTAKE_BARCODESIGN", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_Event_Id);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_BARCODE_PATH", sz_BrcodePath);   
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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
    }

    public void UpdateAOBBarcodeSign(string sz_EventId, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_AOB_BARCODESIGN", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);          
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);           
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void UpdateLienBarcodePath(string sz_EventId,string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_LIEN_BARCODESIGN", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_EventId); 
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath); 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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
    }


    public DataSet GetIntakeSheetNodeID(string CompanyID, string sz_Eventid,string sz_NodeType)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        //string sz_NodeID = "";
        try
        {
            comm = new SqlCommand("SP_GET_NODE_ID_USING_NODETYPE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", sz_Eventid);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            comm.Parameters.AddWithValue("@SZ_NODE_TYPE", sz_NodeType);
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsReturn);
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return dsReturn;
    }



    public void updateSYNFollowupPatientPath(string sz_EventId, string sz_Patient_ImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_SYN_FOLLOWUP", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGN_PATH", sz_Patient_ImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATE_PATIENT_SIGN_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void updateSYNFollowupProviderPath(string sz_EventId, string sz_Provider_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_SYN_FOLLOWUP", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PROVIDER_SIGN_PATH", sz_Provider_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATE_PROVIDER_SIGN_PATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }


    public bool checkImageForSynFollowup(string p_szEventID)
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());

        try
        {
            comm = new SqlCommand("SP_SYN_FOLLOWUP", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_ID", p_szEventID);
            comm.Parameters.AddWithValue("@FLAG", "IS_IMAGE_EXIST");
            SqlDataAdapter objDAdap = new SqlDataAdapter(comm);
            objDAdap.Fill(dsReturn);
            if (dsReturn.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
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
        return false;
    }


    //TUSHAR:- To Save Visit From SYN Doctor Screen
    public void Add_SYN_Visit(ArrayList _VisitInfo)     
    {
        DataSet dsReturn = new DataSet();
        conn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        conn.Open();
        try
        {
            comm = new SqlCommand("SP_SYN_FOLLOWUP", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_ID", _VisitInfo[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME", _VisitInfo[1].ToString());
            comm.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", _VisitInfo[2].ToString());
            if (_VisitInfo[3].ToString() != "" && _VisitInfo[3].ToString() != null)
            {
                comm.Parameters.AddWithValue("@DT_DOA", Convert.ToDateTime(_VisitInfo[3].ToString()));
            }
            if (_VisitInfo[4].ToString() != "" && _VisitInfo[4].ToString() != null)
            {
                comm.Parameters.AddWithValue("@DT_CURRENT_DATE", Convert.ToDateTime(_VisitInfo[4].ToString()));
            }
            comm.Parameters.AddWithValue("@SZ_DOCTOR_NOTE", _VisitInfo[5].ToString());
            comm.Parameters.AddWithValue("@SZ_TREATMENT_TIME", _VisitInfo[6].ToString());
            comm.Parameters.AddWithValue("@SZ_LOP_BEFORE", _VisitInfo[7].ToString());
            comm.Parameters.AddWithValue("@SZ_INTENSITY", _VisitInfo[8].ToString());

            comm.Parameters.AddWithValue("@FLT_BIAS", Convert.ToDecimal(_VisitInfo[9].ToString()));
            comm.Parameters.AddWithValue("@SZ_LOP_AFTER", _VisitInfo[10].ToString());
            if (_VisitInfo[11].ToString() != "" && _VisitInfo[11].ToString()!="---Select---")
            comm.Parameters.AddWithValue("@SZ_AREA", _VisitInfo[11].ToString());
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();            
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }        
    }


    //To Get Parient Information For PT Notes
    public DataSet PatientInfoFprPTNotes(string SZ_CASE_ID)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adop;
        try
        {
            con = new SqlConnection(strsqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_PATIENT_INFO_PT_NOTES", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@SZ_CASE_ID", SZ_CASE_ID);

            cmd.CommandType = CommandType.StoredProcedure;
            adop = new SqlDataAdapter(cmd);
            adop.Fill(ds);
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
        return ds;
    }

    //To Get Procedure Code For PT Notes
    public DataSet GetProcCode(string sz_Doctor_ID)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adop;
        try
        {
            con = new SqlConnection(strsqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_find_procedure_code_from_doctor_PT", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@sz_doctor_id", sz_Doctor_ID);

            cmd.CommandType = CommandType.StoredProcedure;
            adop = new SqlDataAdapter(cmd);
            adop.Fill(ds);
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
        return ds;
    }

    //To save PT Notes
    public void saveProcedureCodesForPT(ArrayList p_objArrayList)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_CO_SAVE_PROECEDURE_CODE_PT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PROC_CODE", p_objArrayList[0].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", p_objArrayList[1].ToString());            
            comm.ExecuteNonQuery();
            #endregion
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }


    //To Save Chiro Patient And Doctor Signature
    public void UpdateSignPathCH(string sz_EventId, string sz_Patient_ImagePath, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_CHIRO_CA", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGN_PATH", sz_Patient_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_SIGN_PATH", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATESIGNIMAGEPATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }


    //To Get Treatment For Particular Visit
    public DataSet CHNotesPatientTreatment(string I_EVENT_ID, string SZ_COMPANY_ID)
    {
        DataSet PTNotes = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adop;
        try
        {
            con = new SqlConnection(strsqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_CHIRO_CA", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", I_EVENT_ID);
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID);
            cmd.Parameters.AddWithValue("@FLAG", "GET_TREATMENT");

            cmd.CommandType = CommandType.StoredProcedure;
            adop = new SqlDataAdapter(cmd);
            adop.Fill(PTNotes);
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
        return PTNotes;

    }

    //To Delete Treatment Code For Particular Event
    public DataSet CHNotesPatientTreatmentDelete(string I_EVENT_ID)
    {
        DataSet PTNotesDelete = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adop;
        try
        {
            con = new SqlConnection(strsqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_CHIRO_CA", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", I_EVENT_ID);
            cmd.Parameters.AddWithValue("@FLAG", "DELETE_TREATMENT");
            cmd.CommandType = CommandType.StoredProcedure;
            adop = new SqlDataAdapter(cmd);
            adop.Fill(PTNotesDelete);
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
        return PTNotesDelete;
    }

    //To Save LMT Patient And Doctor Signature
    public void UpdateSignPathLMT(string sz_EventId, string sz_Patient_ImagePath, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_LMT", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGN_PATH", sz_Patient_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_SIGN_PATH", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATESIGNIMAGEPATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    public void updateIMDoctorSignPath(string sz_EventId, string ImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_IM_SIGN_PATH", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_SIGN_PATH", ImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATEDOCTORSIGNIMAGEPATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }
    public void updateIMPatientSignPath(string sz_EventId, string ImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_IM_SIGN_PATH", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_SIGN_PATH", ImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATEPATIENTSIGNIMAGEPATH");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }


    public void UpdateSignPathPatient(string sz_EventId, string sz_Patient_ImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_PT_NOTES", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGN_PATH", sz_Patient_ImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATESIGNIMAGEPATHPATIENT");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }


    public void UpdateSignPathDoctor(string sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_PT_NOTES", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);            
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_SIGN_PATH", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATESIGNIMAGEPATHDOCTOR");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    //To Save Chiro Patient Signature
    public void UpdateSignPathCHPatient(string sz_EventId, string sz_Patient_ImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_CHIRO_CA", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            cmd.Parameters.AddWithValue("@SZ_PATIENT_SIGN_PATH", sz_Patient_ImagePath);            
            cmd.Parameters.AddWithValue("@FLAG", "UPDATESIGNIMAGEPATHPATIENT");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }

    //To Save Chiro   Doctor Signature
    public void UpdateSignPathCHDoctor(string sz_EventId, string sz_Doctor_ImagePath, string sz_BarcodeImagePath)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_CHIRO_CA", conn);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);            
            cmd.Parameters.AddWithValue("@SZ_DOCTOR_SIGN_PATH", sz_Doctor_ImagePath);
            cmd.Parameters.AddWithValue("@SZ_BARCODE_PATH", sz_BarcodeImagePath);
            cmd.Parameters.AddWithValue("@FLAG", "UPDATESIGNIMAGEPATHDOCTOR");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
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

    }
    public void saveProcCodes(string sz_EventId,string ProcCode)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_SAVE_PROECEDURE_CODE_FOR_EVENT_ID";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PROC_CODE",ProcCode );
            comm.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            comm.ExecuteNonQuery();
            #endregion
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    public int  UpdateProc(string sz_Event_Proc_Id, string ProcCode)
    {
        int iReturn = 0;
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_UPADTE_PROC_CODE";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PROC_CODE", ProcCode);
            comm.Parameters.AddWithValue("@I_EVENT_POROC_ID", sz_Event_Proc_Id);
            iReturn=comm.ExecuteNonQuery();
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            iReturn = 0;
        }
        return iReturn;
    }

    public int UpdateProcLHR(string sz_Event_Proc_Id, string ProcCode, string sz_doctor_id)
    {
        int iReturn = 0;
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_UPADTE_PROC_CODE_LHR";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PROC_CODE", ProcCode);
            comm.Parameters.AddWithValue("@I_EVENT_POROC_ID", sz_Event_Proc_Id);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_doctor_id);
            iReturn = comm.ExecuteNonQuery();
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            iReturn = 0;
        }
        return iReturn;
    }
    public void AddProcedureCodeNotesForLhr(string sz_Event_Id, string sz_ProcedureGroup_ID, string sz_Old_Proc_Id, string sz_New_Proc_Id, string sz_Event_Proc_Id, string sz_company_Id, string sz_case_Id, string lhrCode, string strNewProcedureGroupID, string szUserId, int iUpdateSpecialty, string szDesc)
    {

        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_INSERT_INTO_TXN_PROCEDURE_CODE_AUDIT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID", sz_Event_Id);
            comm.Parameters.AddWithValue("@SZ_PROCEDUREGROUP_ID", sz_ProcedureGroup_ID);
            comm.Parameters.AddWithValue("@SZ_OLD_PROC_CODE", sz_Old_Proc_Id);
            comm.Parameters.AddWithValue("@SZ_NEW_PROC_CODE", sz_New_Proc_Id);
            comm.Parameters.AddWithValue("@SZ_EVENT_PROC_ID", sz_Event_Proc_Id);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_Id);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_Id);
            comm.Parameters.AddWithValue("@SZ_NEW_PROCEDURE_GROUP_ID", strNewProcedureGroupID);
            comm.Parameters.AddWithValue("@SZ_LHRCODE", lhrCode);
            comm.Parameters.AddWithValue("@SZ_USER_ID", szUserId);
            comm.Parameters.AddWithValue("@BT_UPDATE_SPECIALTY", iUpdateSpecialty);
            comm.Parameters.AddWithValue("@SZ_DESCRIPTION", szDesc);

            comm.ExecuteNonQuery();
            #endregion
        }
       catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

    }
    public DataSet GetProcIdusingEventProcId(string sz_Event_Proc_Id)
    {
        DataSet dsProcId = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adop;
        try
        {
            con = new SqlConnection(strsqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_PROC_CODE", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_POROC_ID", sz_Event_Proc_Id);
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
    public DataSet GetProcIdAfterProcCode(string sz_Event_Proc_Id)
    {
        DataSet dsProcId = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adop;
        try
        {
            con = new SqlConnection(strsqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_UPDATE_PROC_CODE", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@I_EVENT_POROC_ID", sz_Event_Proc_Id);
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

    public DataSet GetAuditDetails(string szCompanyId)
    {
        DataSet dsAuditDetails = new DataSet();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter daAudit;
        try
        {
            con = new SqlConnection(strsqlCon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_AUDIT_DETAILS", con);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            cmd.CommandType = CommandType.StoredProcedure;
            daAudit = new SqlDataAdapter(cmd);
            daAudit.Fill(dsAuditDetails);
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
        return dsAuditDetails;
    }

}

  