using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using log4net;
using System.Configuration;
using System.Web;

namespace Scheduling
{




    public class OutSchedulePatientDAO
    {
        public string sPatientFirstName;
        public string sPatientMI;
        public string sPatientLastName;
        public string sPatientID;
        public string sCaseID;
        public string sSourceCompanyID;
        public string sDestinationCompanyID;
        public string sPatientAddress;
        public string sPatientCity;
        public string sPatientPhone;
        public string sPatientState;
        public string sCaseStatusID;
        public string sInsuranceID;
        public string sCaseTypeID;
        public string sPatientAge;

        public bool addPatient;
    }

    public class OutScheduleVisitDAO
    {
        private int iEventID;
        private string sPatientID;
        private string sCaseID;
        private string sSourceCompanyID;
        private string sVisitDate;
        private string sVisitTimeHH;
        private string sVisitTimeMM;
        private string sVisitType; // IE / FU etc
        private string sVisitTypeID;
    }

    public class OutScheduleProceduresDAO
    {
        private int iEventID;
        private string sPatientID;
        private string sProcedureCodeID;
        private string sProcedureCode;        
    }

    public class OutSchedulePatient
    {
        String strsqlCon;
        SqlConnection conn;
        SqlCommand comm;
        DataSet objDS;
        SqlDataAdapter objDAdp;
        Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
        private static ILog log = LogManager.GetLogger("OutSchedulePatientTransaction");
        String szLatestPatientID = "";

        public OutSchedulePatient()
	    {
            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        }
        public string GetCaseIdForDocumentPath(string event_id)
        {
            string newCaseID = "";
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            try
            {
                #region "Get case id from patient Id for genrating path of document"
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "sp_get_caseID_from_patientId";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Parameters.AddWithValue("@SZ_PATIENT_ID ", event_id);
                objDS = new DataSet();
                objDAdp = new SqlDataAdapter(comm);
                objDAdp.Fill(objDS);
                if (objDS != null)
                {
                    if (objDS.Tables[0] != null)
                    {
                        newCaseID = objDS.Tables[0].Rows[0][0].ToString();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return newCaseID;
        }
        public DataSet GetNodeIdForCopyDocument(string testCompanyID)
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            try
            {
                
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "sp_get_nodeList_for_copy_docs";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Parameters.AddWithValue("@SZ_DEST_COMPANY ", testCompanyID);
                objDS = new DataSet();
                objDAdp = new SqlDataAdapter(comm);
                objDAdp.Fill(objDS);
               
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return objDS;
        }

        public string GetSourcePath(string testCompanyID,string inodeType,string caseID)
        {
            string folderpath = "";
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("select dbo.FNC_WS_GET_FULL_PATH_FROM_NODE_TYPE('" + inodeType + "','" + caseID + "','" + testCompanyID + "')", conn);
                cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                folderpath = Convert.ToString(cmd.ExecuteScalar());
                conn.Close();

            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return folderpath;
        }
        //
        public string GetDestPath(string testCompanyID, string caseID)
        {
            string folderpath = "";
            DataSet objDS;
            SqlDataAdapter objDAdp;
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand comm;
            try
            {
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "SP_GET_DEST_COPY_DOC_PATH";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", testCompanyID);
                comm.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
                objDS = new DataSet();
                objDAdp = new SqlDataAdapter(comm);
                objDAdp.Fill(objDS);
                if (objDS != null)
                {
                    if (objDS.Tables.Count > 0)
                    {
                        if (objDS.Tables[0] != null)
                        {
                            folderpath = objDS.Tables[0].Rows[0][0].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return folderpath;
        }
        public string tblImageTag(string sz_UserName, string caseid, string testCompanyID, string NodeName, string DocImages, string inputString)
        {
            string folderpath = "";
            DataSet objDS;
            SqlDataAdapter objDAdp;
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand comm;
            try
            {
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "SP_INSERT_LOGICAL_NODE_INTO_tblImageTag"; //DocImages
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Parameters.AddWithValue("@sz_UserName", sz_UserName);
                comm.Parameters.AddWithValue("@SZ_CASE_ID", caseid);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", testCompanyID);
                comm.Parameters.AddWithValue("@NODENAME", NodeName);
                comm.Parameters.AddWithValue("@DocImages", DocImages);
                comm.Parameters.AddWithValue("@inputString", inputString);
                objDS = new DataSet();
                objDAdp = new SqlDataAdapter(comm);
                objDAdp.Fill(objDS);
                if (objDS != null)
                {
                    if (objDS.Tables.Count > 0)
                    {
                        if (objDS.Tables[0] != null)
                        {
                            folderpath = objDS.Tables[0].Rows[0][0].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return folderpath;
        }

        public string DocImages(string DocImages, string temppath)
        {
            string folderpath = "";
            DataSet objDS;
            SqlDataAdapter objDAdp;
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand comm;
            try
            {
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "SP_INSERT_LOGICAL_NODE_INTO_DocImages"; //DocImages
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Parameters.AddWithValue("@FILENAME", DocImages);
                comm.Parameters.AddWithValue("@FILE_PATH", temppath);
                objDS = new DataSet();
                objDAdp = new SqlDataAdapter(comm);
                objDAdp.Fill(objDS);
                if (objDS != null)
                {
                    if (objDS.Tables.Count > 0)
                    {
                        if (objDS.Tables[0] != null)
                        {
                            folderpath = objDS.Tables[0].Rows[0][0].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return folderpath;
        }

        public string Insertnodenew(string[] parts,  string testCompanyID)
        {
            string folderpath = "";
            string nodeId = null;
            DataSet objDS;
            SqlDataAdapter objDAdp;
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlTransaction transaction;
            transaction = conn.BeginTransaction();
            SqlCommand comm;
            try
            {

                string caseid = parts[4].ToString();
                int j = 0;
                for (j = 0; j < parts.Length - 1; j++)
                {
                    if (parts[j].ToString() == "Transferred Documents")
                        break;
                }
                for (int i = ++j; i < parts.Length - 1; i++)
                {
                    OutSchedulePatient objOutTran = new OutSchedulePatient();
                    string nodename = parts[i].ToString();
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

                    comm.CommandText = "SP_INSERT_LOGICAL_NODE_INTO_TBLTAGS_new";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Transaction = transaction;
                    comm.Parameters.AddWithValue("@NODE_NAME", nodename);
                    comm.Parameters.AddWithValue("@SZ_CASE_ID", caseid);
                    comm.Parameters.AddWithValue("@SZ_COMPANY_ID", testCompanyID);
                    comm.Parameters.AddWithValue("@node_id", nodeId);
                    objDS = new DataSet();
                    objDAdp = new SqlDataAdapter(comm);
                    objDAdp.Fill(objDS);
                    if (objDS != null)
                    {
                        if (objDS.Tables.Count > 0)
                        {
                            if (objDS.Tables[0] != null)
                            {
                                nodeId = objDS.Tables[0].Rows[0][0].ToString();
                            }
                        }
                    }

                }
                
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return folderpath;
        }

        public bool AssignOutScheduleDocumentsToTestFacility(ArrayList objAl, string sz_user_Id, string sz_company_id, string testFacilityCompanyID)
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlTransaction transaction;
            transaction = conn.BeginTransaction();


            try
            {
                #region Clear already confired documents to test facility
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "sp_clear_assigned_doc_to_test_facilty";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;
                comm.Parameters.AddWithValue("@sz_company_id", sz_company_id);
                comm.Parameters.AddWithValue("@sz_test_facilty", testFacilityCompanyID);
                comm.ExecuteNonQuery();
                #endregion

                #region configure new assigned documents to test facility
                for (int i = 0; i < objAl.Count;i++ )
                {
                    DAO_Assign_Doc DAO = new DAO_Assign_Doc();
                    DAO = (DAO_Assign_Doc)objAl[i];


                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandText = "sp_assign_out_schedule_docs_to_test_facility";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Transaction = transaction;
                    comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
                    comm.Parameters.AddWithValue("@I_NODE_ID", Convert.ToInt32(DAO.SelectedId));
                    comm.Parameters.AddWithValue("@SZ_USERID", sz_user_Id);
                    comm.Parameters.AddWithValue("@SZ_TEST_FACILITY_COMPANY_ID", DAO.SelectedRoleID);
                    comm.ExecuteNonQuery();
                }
                #endregion
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                transaction.Rollback();
                return false;
            }
            finally { conn.Close(); }
        }

        public calResult AddVisit(calOperation p_objcalOperation, OutSchedulePatientDAO p_objOutPatientDAO, ArrayList p_objALDoctorAmount, calEvent p_objcalEvent, ArrayList p_objALProcedureCodeEO, string UserId, string roomID, string extddlReferringFacility, string txtCompanyID, string extddlDoctor)
        {
            bool PatientExist = false;
            string newCaseID = "";
            string OldPatientID = "";
            int iEventID = 0;
            calResult objReturnResult = new calResult();
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlTransaction transaction;
            transaction = conn.BeginTransaction();
            OldPatientID = p_objcalEvent.SZ_PATIENT_ID.ToString();
            try
            {
                #region "Get Patient Id If It is already exist in test company"
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "SP_GET_PATIENTID_EXIST_IN_OTHER_COMPANY";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;
                comm.Parameters.AddWithValue("@SZ_SOURCE_COMPANY", txtCompanyID);
                comm.Parameters.AddWithValue("@SZ_DEST_COMPANY", extddlReferringFacility);
                comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_objcalEvent.SZ_PATIENT_ID);
                objDS = new DataSet();
                objDAdp = new SqlDataAdapter(comm);
                objDAdp.Fill(objDS);
                if (objDS != null)
                {
                    if (objDS.Tables.Count > 0)
                    {
                        if (objDS.Tables[0] != null)
                        {
                            p_objcalEvent.SZ_PATIENT_ID = objDS.Tables[0].Rows[0][0].ToString();
                            PatientExist = true;
                            if (p_objcalEvent.SZ_PATIENT_ID.ToString().ToLower() != "null")
                            {
                                PatientExist = true;
                            }
                        }
                    }
                }
                #endregion

                if (p_objcalOperation.add_patient == true)
                {
                    #region "Save Patient Information"
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandText = "SP_OUT_SCHEDULE_MST_PATIENT_DATA_ENTRY";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Transaction = transaction;

                    comm.Parameters.AddWithValue("@SZ_PATIENT_FIRST_NAME", p_objOutPatientDAO.sPatientFirstName);
                    comm.Parameters.AddWithValue("@SZ_PATIENT_LAST_NAME", p_objOutPatientDAO.sPatientLastName);
                    comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objOutPatientDAO.sSourceCompanyID);
                    comm.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", p_objOutPatientDAO.sCaseTypeID);
                    comm.Parameters.AddWithValue("@SZ_TEST_COMPANY_ID", p_objOutPatientDAO.sDestinationCompanyID);
                    if (p_objOutPatientDAO.sPatientAge != "")
                        comm.Parameters.AddWithValue("@I_PATIENT_AGE", p_objOutPatientDAO.sPatientAge);
                    if (p_objOutPatientDAO.sPatientAddress != "")
                        comm.Parameters.AddWithValue("@SZ_PATIENT_ADDRESS", p_objOutPatientDAO.sPatientAddress);
                    if (p_objOutPatientDAO.sPatientCity != "")
                        comm.Parameters.AddWithValue("@SZ_PATIENT_CITY", p_objOutPatientDAO.sPatientCity);
                    if (p_objOutPatientDAO.sPatientPhone != "")
                        comm.Parameters.AddWithValue("@SZ_PATIENT_PHONE", p_objOutPatientDAO.sPatientPhone);
                    if (p_objOutPatientDAO.sPatientState != "" && p_objOutPatientDAO.sPatientState != "NA")
                        comm.Parameters.AddWithValue("@SZ_PATIENT_STATE_ID", p_objOutPatientDAO.sPatientState);
                    if (p_objOutPatientDAO.sPatientMI != "")
                        comm.Parameters.AddWithValue("@MI", p_objOutPatientDAO.sPatientMI);
                    if (p_objOutPatientDAO.sCaseStatusID != "")
                        comm.Parameters.AddWithValue("@SZ_CASE_STATUS_ID", p_objOutPatientDAO.sCaseStatusID);
                    if (p_objOutPatientDAO.sInsuranceID != "")
                        comm.Parameters.AddWithValue("@SZ_INSURANCE_ID", p_objOutPatientDAO.sInsuranceID);
                    if (!UserId.ToString().Equals(""))
                        comm.Parameters.AddWithValue("@sz_user_id", UserId.ToString());
                    //comm.Parameters.AddWithValue("@FLAG", "ADD");
                    comm.ExecuteNonQuery();
                    #endregion
                }
                #region "Copy Patient"
                if (p_objcalOperation.add_patient != true)
                {
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandText = "sp_add_out_schedule_patient";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Transaction = transaction;

                    comm.Parameters.AddWithValue("@sz_connection_from", extddlReferringFacility);
                    comm.Parameters.AddWithValue("@sz_connection_to", txtCompanyID);
                    comm.Parameters.AddWithValue("@sz_user_id", UserId);
                    comm.Parameters.AddWithValue("@sz_doctor_id", extddlDoctor);
                    comm.Parameters.AddWithValue("@sz_patient_id", p_objcalEvent.SZ_PATIENT_ID);
                    comm.Parameters.AddWithValue("@event_id", iEventID.ToString());
                    comm.Parameters.AddWithValue("@room_id", roomID);

                    comm.ExecuteNonQuery();
                }
                #endregion
                //#region "Get Patient Id If It is already exist in test company"
                //comm = new SqlCommand();
                //comm.CommandText = "SP_GET_PATIENTID_EXIST_IN_OTHER_COMPANY";
                //comm.CommandType = CommandType.StoredProcedure;
                //comm.Connection = conn;
                //comm.Transaction = transaction;
                //comm.Parameters.AddWithValue("@SZ_SOURCE_COMMPANY", txtCompanyID);
                //comm.Parameters.AddWithValue("@SZ_DEST_COMPANY", extddlReferringFacility);
                //comm.Parameters.AddWithValue("@SZ_PATIENT_ID", p_objcalEvent.SZ_PATIENT_ID);
                //objDS = new DataSet();
                //objDAdp = new SqlDataAdapter(comm);
                //objDAdp.Fill(objDS);
                //if (objDS != null)
                //{
                //    if (objDS.Tables[0] != null)
                //    {
                //        p_objcalEvent.SZ_PATIENT_ID = objDS.Tables[0].Rows[0][0].ToString();
                //        PatientExist = true;
                //        if (p_objcalEvent.SZ_PATIENT_ID.ToString().ToLower() != "null")
                //        {
                //            PatientExist = true;
                //        }
                //    }
                //}
                //#endregion
                if (!PatientExist)
                {
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
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objcalEvent.SZ_REFERENCE_ID);
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

                #region "update txn_calendar_event for load out schedule calendar"
                if (p_objcalOperation.add_patient != true)
                {
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandText = "sp_out_schedule_calendar_event";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Transaction = transaction;
                    comm.Parameters.AddWithValue("@sz_patient_Id ", OldPatientID);
                    comm.Parameters.AddWithValue("@sz_company_id ", txtCompanyID);
                    comm.Parameters.AddWithValue("@event_id", iEventID);
                    comm.ExecuteNonQuery();
                }
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
                #region "Copy Patient"
                //if (p_objcalOperation.add_patient != true)
                //{
                //    comm = new SqlCommand();
                //    comm.CommandText = "sp_add_out_schedule_patient";
                //    comm.CommandType = CommandType.StoredProcedure;
                //    comm.Connection = conn;
                //    comm.Transaction = transaction;

                //    comm.Parameters.AddWithValue("@sz_connection_from", extddlReferringFacility);
                //    comm.Parameters.AddWithValue("@sz_connection_to", txtCompanyID);
                //    comm.Parameters.AddWithValue("@sz_user_id", UserId);
                //    comm.Parameters.AddWithValue("@sz_doctor_id", extddlDoctor);
                //    comm.Parameters.AddWithValue("@sz_patient_id", p_objcalEvent.SZ_PATIENT_ID);
                //    comm.Parameters.AddWithValue("@event_id", iEventID.ToString());
                //    comm.Parameters.AddWithValue("@room_id", roomID);

                //    comm.ExecuteNonQuery();
                //}
                #endregion
                transaction.Commit();

                #region "Create Result object"
                objReturnResult.event_id = iEventID.ToString();
                objReturnResult.msg = "Patient added successfully !";
                objReturnResult.msg_code = "SUCCESS";
                objReturnResult.sz_patient_id = p_objcalEvent.SZ_PATIENT_ID;
                #endregion
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                objReturnResult = new calResult();
                objReturnResult.msg = "Error";
                objReturnResult.msg_code = "ERROR";
                objReturnResult.event_id = "-1";
                transaction.Rollback();
            }
            return objReturnResult;
        }

        
    }
    
}
