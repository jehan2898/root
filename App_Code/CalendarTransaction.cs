using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
using log4net;


public class CalendarTransaction
{
    #region "variables"
    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;
    DataSet objDS;
    SqlDataAdapter objDAdp;
    Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private static ILog log = LogManager.GetLogger("CalendarTransaction");
    String szLatestPatientID = "";
#endregion

    #region "Constructor"

    public CalendarTransaction()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    #endregion

    #region "Save Appointment"

    /* Purpose : Apply transaction for appointment save from Out schedule calendar page
     * Save patient , Add Appointment in one transaction.
     * Add Appintment - Save Procedure codes and save visit.
     */

    public calResult CopyOutSchedulePatient(string extddlReferringFacility, string txtCompanyID, string txtUserId, string txtPatientID, string extddlDoctor, string eventID, string roomID)
    {
        int iEventID = 0;
        calResult objReturnResult = new calResult();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {


            comm.CommandText = "sp_add_out_schedule_patient";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;

            comm.Parameters.AddWithValue("@sz_connection_from", extddlReferringFacility);
            comm.Parameters.AddWithValue("@sz_connection_to", txtCompanyID);
            comm.Parameters.AddWithValue("@sz_user_id", txtUserId);
            comm.Parameters.AddWithValue("@sz_doctor_id", extddlDoctor);
            comm.Parameters.AddWithValue("@sz_patient_id", txtPatientID);
            comm.Parameters.AddWithValue("@event_id", eventID);
            comm.Parameters.AddWithValue("@room_id", roomID);

            comm.ExecuteNonQuery();

            transaction.Commit();
            
            #region "Create Result object"
            objReturnResult.event_id = iEventID.ToString();
            objReturnResult.msg = "Patient added successfully !";
            objReturnResult.msg_code = "SUCCESS";
            #endregion
        }
        catch (Exception ex)
        {
            objReturnResult = new calResult();
            objReturnResult.msg = "Error";
            objReturnResult.msg_code = "ERROR";
            objReturnResult.event_id = "-1";
            transaction.Rollback();
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        return objReturnResult;
    }

    public calResult fnc_SaveAppointment(calOperation p_objcalOperation,calPatientEO p_objPatientEO, ArrayList p_objALDoctorAmount, calEvent p_objcalEvent, ArrayList p_objALProcedureCodeEO,string UserId)
    {
        int iEventID = 0;
        calResult objReturnResult = new calResult();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();

        try
        {
            if (p_objcalOperation.add_patient == true)
            {
                #region "Save Patient Information"
                comm.CommandText = "SP_MST_PATIENT_DATA_ENTRY";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;

                comm.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", p_objPatientEO.SZ_PATIENT_FIRST_NAME);
                comm.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME", p_objPatientEO.SZ_PATIENT_LAST_NAME);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objPatientEO.SZ_COMPANY_ID);
                comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", p_objPatientEO.SZ_CASE_TYPE_ID);

                if (p_objPatientEO.I_PATIENT_AGE != "")
                    comm.Parameters.AddWithValue("@I_PATIENT_AGE", p_objPatientEO.I_PATIENT_AGE);
                if (p_objPatientEO.SZ_PATIENT_ADDRESS != "")
                    comm.Parameters.AddWithValue("@SZ_PATIENT_ADDRESS", p_objPatientEO.SZ_PATIENT_ADDRESS);
                if (p_objPatientEO.SZ_PATIENT_CITY != "")
                    comm.Parameters.AddWithValue("@SZ_PATIENT_CITY", p_objPatientEO.SZ_PATIENT_CITY);
                if (p_objPatientEO.SZ_PATIENT_PHONE != "")
                    comm.Parameters.AddWithValue("@SZ_PATIENT_PHONE", p_objPatientEO.SZ_PATIENT_PHONE);
                if (p_objPatientEO.SZ_PATIENT_STATE_ID != "" && p_objPatientEO.SZ_PATIENT_STATE_ID != "NA")
                    comm.Parameters.AddWithValue("@SZ_PATIENT_STATE_ID", p_objPatientEO.SZ_PATIENT_STATE_ID);
                if (p_objPatientEO.MI != "")
                    comm.Parameters.AddWithValue("@MI", p_objPatientEO.MI);
                if (p_objPatientEO.SZ_CASE_STATUS_ID != "")
                    comm.Parameters.AddWithValue("@SZ_CASE_STATUS_ID", p_objPatientEO.SZ_CASE_STATUS_ID);
                if (p_objPatientEO.SZ_INSURANCE_ID != "")
                    comm.Parameters.AddWithValue("@SZ_INSURANCE_ID", p_objPatientEO.SZ_INSURANCE_ID);
                if (!UserId.ToString().Equals(""))
                    comm.Parameters.AddWithValue("@sz_user_id", UserId.ToString());
                comm.Parameters.AddWithValue("@FLAG", "ADD");
                comm.ExecuteNonQuery();
                #endregion

                #region "Get Latest Patient ID"
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "SP_MST_PATIENT";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;
                comm.Parameters.AddWithValue("@FLAG", "GET_LATEST_PATIENT_ID");
                objDS = new DataSet();
                objDAdp = new SqlDataAdapter(comm);
                objDAdp.Fill(objDS);
                if (objDS != null)
                {
                    if (objDS.Tables[0] != null)
                    {
                        p_objcalEvent.SZ_PATIENT_ID = objDS.Tables[0].Rows[0][0].ToString();
                    }
                }
                #endregion
            }

            #region "Add Doctor Amount"

            if (p_objALDoctorAmount != null)
            {
                for (int i = 0; i < p_objALDoctorAmount.Count; i++)
                {
                    if (((calDoctorAmount)p_objALDoctorAmount[i]).SZ_PROCEDURE_ID != "--- Select ---")
                    {
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_DOCTOR_PROCEDURE_AMOUNT";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", ((calDoctorAmount)p_objALDoctorAmount[i]).SZ_DOCTOR_ID);
                        comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", ((calDoctorAmount)p_objALDoctorAmount[i]).SZ_PROCEDURE_ID);
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", ((calDoctorAmount)p_objALDoctorAmount[i]).SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", ((calDoctorAmount)p_objALDoctorAmount[i]).SZ_TYPE_CODE_ID);
                        comm.Parameters.AddWithValue("@FLAG", "ADDREGERRING");
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion

            #region "Add Event"

            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_CALENDAR_EVENT";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_CASE_ID ", p_objcalEvent.SZ_PATIENT_ID);
            comm.Parameters.AddWithValue("@DT_EVENT_DATE", p_objcalEvent.DT_EVENT_DATE);
            comm.Parameters.AddWithValue("@DT_EVENT_TIME", p_objcalEvent.DT_EVENT_TIME);
            comm.Parameters.AddWithValue("@SZ_EVENT_NOTES", p_objcalEvent.SZ_EVENT_NOTES);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_objcalEvent.SZ_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", p_objcalEvent.SZ_TYPE_CODE_ID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objcalEvent.SZ_COMPANY_ID);
            comm.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", p_objcalEvent.DT_EVENT_TIME_TYPE);
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME", p_objcalEvent.DT_EVENT_END_TIME);
            comm.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", p_objcalEvent.DT_EVENT_END_TIME_TYPE);
            comm.Parameters.AddWithValue("@SZ_REFERENCE_ID", p_objcalEvent.SZ_REFERENCE_ID);
            comm.Parameters.AddWithValue("@BT_STATUS", p_objcalEvent.BT_STATUS);
            comm.Parameters.AddWithValue("@BT_TRANSPORTATION", p_objcalEvent.BT_TRANSPORTATION);

            if (p_objcalEvent.I_TRANSPORTATION_COMPANY != null)
                comm.Parameters.AddWithValue("@I_TRANSPORTATION_COMPANY", p_objcalEvent.I_TRANSPORTATION_COMPANY);
            if (p_objcalEvent.SZ_OFFICE_ID != null)
                comm.Parameters.AddWithValue("@SZ_OFFICE_ID", p_objcalEvent.SZ_OFFICE_ID);

            SqlParameter parmReturnValue = new SqlParameter("@RETURN", SqlDbType.Int);
            parmReturnValue.Direction = ParameterDirection.ReturnValue;
            comm.Parameters.Add(parmReturnValue);
            comm.Parameters.AddWithValue("@FLAG", "ADD2");
            comm.Parameters.AddWithValue("@SZ_USER_ID", UserId.ToString());
            comm.ExecuteNonQuery();

            iEventID = (int)comm.Parameters["@RETURN"].Value;
            #endregion

            #region "Add Procedure Codes"
            if (p_objALProcedureCodeEO != null)
            {
                for (int i1 = 0; i1 < p_objALProcedureCodeEO.Count; i1++)
                {
                    if (((calProcedureCodeEO)p_objALProcedureCodeEO[i1]).SZ_PROC_CODE != "--- Select ---")
                    {
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_SAVE_REFERRAL_PROC_CODE";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_PROC_CODE ", ((calProcedureCodeEO)p_objALProcedureCodeEO[i1]).SZ_PROC_CODE);
                        comm.Parameters.AddWithValue("@I_EVENT_ID", iEventID);
                        comm.Parameters.AddWithValue("@I_STATUS", ((calProcedureCodeEO)p_objALProcedureCodeEO[i1]).I_STATUS);
                        comm.ExecuteNonQuery();
                    }
                }
            }
            #endregion
            transaction.Commit();
            
            #region "Create Result object"
            objReturnResult.event_id = iEventID.ToString();
            objReturnResult.msg = "Patient added successfully !";
            objReturnResult.msg_code = "SUCCESS";
            #endregion
        }
        catch (Exception ex)
        {
            objReturnResult = new calResult();
            objReturnResult.msg = "Error";
            objReturnResult.msg_code = "ERROR";
            objReturnResult.event_id = "-1";
            transaction.Rollback(); Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return objReturnResult;
    }
    #endregion

    #region "Update Appointment"

    #endregion
}

#region "Patient Used to save all req information of patient"

public class calPatientEO
{
    private string _SZ_PATIENT_FIRST_NAME = "";
    public string SZ_PATIENT_FIRST_NAME
    {
        get { return _SZ_PATIENT_FIRST_NAME; }
        set { _SZ_PATIENT_FIRST_NAME = value; }
    }

    private string _SZ_PATIENT_LAST_NAME = "";
    public string SZ_PATIENT_LAST_NAME
    {
        get { return _SZ_PATIENT_LAST_NAME; }
        set { _SZ_PATIENT_LAST_NAME = value; }
    }

    private string _SZ_CASE_TYPE_ID = "";
    public string SZ_CASE_TYPE_ID
    {
        get { return _SZ_CASE_TYPE_ID; }
        set { _SZ_CASE_TYPE_ID = value; }
    }

    private string _I_PATIENT_AGE = "";
    public string I_PATIENT_AGE
    {
        get { return _I_PATIENT_AGE; }
        set { _I_PATIENT_AGE = value; }
    }

    private string _SZ_PATIENT_ADDRESS = "";
    public string SZ_PATIENT_ADDRESS
    {
        get { return _SZ_PATIENT_ADDRESS; }
        set { _SZ_PATIENT_ADDRESS = value; }
    }

    private string _SZ_PATIENT_CITY = "";
    public string SZ_PATIENT_CITY
    {
        get { return _SZ_PATIENT_CITY; }
        set { _SZ_PATIENT_CITY = value; }
    }

    private string _SZ_PATIENT_PHONE = "";
    public string SZ_PATIENT_PHONE
    {
        get { return _SZ_PATIENT_PHONE; }
        set { _SZ_PATIENT_PHONE = value; }
    }

    private string _SZ_PATIENT_STATE_ID = "";
    public string SZ_PATIENT_STATE_ID
    {
        get { return _SZ_PATIENT_STATE_ID; }
        set { _SZ_PATIENT_STATE_ID = value; }
    }

    private string _SZ_COMPANY_ID = "";
    public string SZ_COMPANY_ID
    {
        get { return _SZ_COMPANY_ID; }
        set { _SZ_COMPANY_ID = value; }
    }

    private string _MI = "";
    public string MI
    {
        get { return _MI; }
        set { _MI = value; }
    }

    private string _SZ_CASE_STATUS_ID = "";
    public string SZ_CASE_STATUS_ID
    {
        get { return _SZ_CASE_STATUS_ID; }
        set { _SZ_CASE_STATUS_ID = value; }
    }

    private string _SZ_INSURANCE_ID = "";
    public string SZ_INSURANCE_ID
    {
        get { return _SZ_INSURANCE_ID; }
        set { _SZ_INSURANCE_ID = value; }
    }

    private string _FLAG = "";
    public string FLAG
    {
        get { return _FLAG; }
        set { _FLAG = value; }
    }
}

#endregion

#region "Result of Save appointment and Update appointment."
public class calResult
{
    private string _sz_patient_id;
    public string sz_patient_id
    {
        get { return _sz_patient_id; }
        set { _sz_patient_id = value; }
    }

    private string _msg;
    public string msg
    {
        get { return _msg; }
        set { _msg = value; }
    }

    private string _msg_code;
    public string msg_code
    {
        get { return _msg_code; }
        set { _msg_code = value; }
    }

    private string _event_id;
    public string event_id
    {
        get { return _event_id; }
        set { _event_id = value; }
    }
}
#endregion

#region "Add Doctor Class"
public class calAddDoctorBO
{
    private string _sz_doctor_id;
    public string sz_doctor_id
    {
        get { return _sz_doctor_id; }
        set { _sz_doctor_id = value; }
    }

    private string _sz_patient_id;
    public string sz_patient_id
    {
        get { return _sz_patient_id; }
        set { _sz_patient_id = value; }
    }

    private string _sz_company_id;
    public string sz_company_id
    {
        get { return _sz_company_id; }
        set { _sz_company_id = value; }
    }
}
#endregion

#region "Procedure Code Class"

public class calProcedureCodeEO
{
    private string _SZ_PROC_CODE;
    public string SZ_PROC_CODE
    {
        get { return _SZ_PROC_CODE; }
        set { _SZ_PROC_CODE = value; }
    }

    private string _I_EVENT_ID;
    public string I_EVENT_ID
    {
        get { return _I_EVENT_ID; }
        set { _I_EVENT_ID = value; }
    }

    private string _I_STATUS;
    public string I_STATUS
    {
        get { return _I_STATUS; }
        set { _I_STATUS = value; }
    }
}

#endregion

#region "Doctor Amount"
public class calDoctorAmount
{
    private string _SZ_DOCTOR_ID;
    public string SZ_DOCTOR_ID
    {
        get { return _SZ_DOCTOR_ID; }
        set { _SZ_DOCTOR_ID = value; }
    }

    private string _SZ_PROCEDURE_ID;
    public string SZ_PROCEDURE_ID
    {
        get { return _SZ_PROCEDURE_ID; }
        set { _SZ_PROCEDURE_ID = value; }
    }

    private string _SZ_COMPANY_ID;
    public string SZ_COMPANY_ID
    {
        get { return _SZ_COMPANY_ID; }
        set { _SZ_COMPANY_ID = value; }
    }

    private string _SZ_TYPE_CODE_ID;
    public string SZ_TYPE_CODE_ID
    {
        get { return _SZ_TYPE_CODE_ID; }
        set { _SZ_TYPE_CODE_ID = value; }
    }

    private string _FLAG;
    public string FLAG
    {
        get { return _FLAG; }
        set { _FLAG = value; }
    }
}
#endregion

#region "Event BO"
public class calEvent
{

    private string _SZ_PATIENT_ID;
    public string SZ_PATIENT_ID
    {
        get { return _SZ_PATIENT_ID; }
        set { _SZ_PATIENT_ID = value; }
    }

    private string _SZ_CASE_ID;
    public string SZ_CASE_ID
    {
        get { return _SZ_CASE_ID; }
        set { _SZ_CASE_ID = value; }
    }

    private string _DT_EVENT_DATE;
    public string DT_EVENT_DATE
    {
        get { return _DT_EVENT_DATE; }
        set { _DT_EVENT_DATE = value; }
    }

    private string _DT_EVENT_TIME;
    public string DT_EVENT_TIME
    {
        get { return _DT_EVENT_TIME; }
        set { _DT_EVENT_TIME = value; }
    }

    private string _SZ_EVENT_NOTES;
    public string SZ_EVENT_NOTES
    {
        get { return _SZ_EVENT_NOTES; }
        set { _SZ_EVENT_NOTES = value; }
    }

    private string _SZ_DOCTOR_ID;
    public string SZ_DOCTOR_ID
    {
        get { return _SZ_DOCTOR_ID; }
        set { _SZ_DOCTOR_ID = value; }
    }

    private string _SZ_TYPE_CODE_ID;
    public string SZ_TYPE_CODE_ID
    {
        get { return _SZ_TYPE_CODE_ID; }
        set { _SZ_TYPE_CODE_ID = value; }
    }

    private string _SZ_COMPANY_ID;
    public string SZ_COMPANY_ID
    {
        get { return _SZ_COMPANY_ID; }
        set { _SZ_COMPANY_ID = value; }
    }

    private string _DT_EVENT_TIME_TYPE;
    public string DT_EVENT_TIME_TYPE
    {
        get { return _DT_EVENT_TIME_TYPE; }
        set { _DT_EVENT_TIME_TYPE = value; }
    }

    private string _DT_EVENT_END_TIME;
    public string DT_EVENT_END_TIME
    {
        get { return _DT_EVENT_END_TIME; }
        set { _DT_EVENT_END_TIME = value; }
    }

    private string _DT_EVENT_END_TIME_TYPE;
    public string DT_EVENT_END_TIME_TYPE
    {
        get { return _DT_EVENT_END_TIME_TYPE; }
        set { _DT_EVENT_END_TIME_TYPE = value; }
    }

    private string _SZ_REFERENCE_ID;
    public string SZ_REFERENCE_ID
    {
        get { return _SZ_REFERENCE_ID; }
        set { _SZ_REFERENCE_ID = value; }
    }

    private string _BT_STATUS;
    public string BT_STATUS
    {
        get { return _BT_STATUS; }
        set { _BT_STATUS = value; }
    }

    private string _BT_TRANSPORTATION;
    public string BT_TRANSPORTATION
    {
        get { return _BT_TRANSPORTATION; }
        set { _BT_TRANSPORTATION = value; }
    }


    private string _I_TRANSPORTATION_COMPANY;
    public string I_TRANSPORTATION_COMPANY
    {
        get { return _I_TRANSPORTATION_COMPANY; }
        set { _I_TRANSPORTATION_COMPANY = value; }
    }

    private string _SZ_OFFICE_ID;
    public string SZ_OFFICE_ID
    {
        get { return _SZ_OFFICE_ID; }
        set { _SZ_OFFICE_ID = value; }
    }

    private string _FLAG;
    public string FLAG
    {
        get { return _FLAG; }
        set { _FLAG = value; }
    }
}
#endregion

#region "Operation"

public class calOperation
{
    private bool _bt_add_patient;
    public bool add_patient
    {
        get { return _bt_add_patient; }
        set { _bt_add_patient = value; }
    }

    private bool _bt_add_doctor_amount;
    public bool bt_add_doctor_amount
    {
        get { return _bt_add_doctor_amount; }
        set { _bt_add_doctor_amount = value; }
    }

    private bool _bt_add_event;
    public bool bt_add_event
    {
        get { return _bt_add_event; }
        set { _bt_add_event = value; }
    }

    private bool _bt_add_procedure_codes;
    public bool bt_add_procedure_codes
    {
        get { return _bt_add_procedure_codes; }
        set { _bt_add_procedure_codes = value; }
    }
}

#endregion

