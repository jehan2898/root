using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for ReferralBillTransaction
/// </summary>
public class Referral_Bill_Transaction
{

    String strsqlCon;
    SqlConnection conn;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    public Referral_Bill_Transaction()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public Result saveReferralBill(BillTransactionEO objBillTransactionEO, DAO_NOTES_EO _DAO_NOTES_EO, ArrayList p_objProcedureCodes, ArrayList p_objALDiagCode)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        sqlCmd = new SqlCommand();
        sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        string strBillID = "";
        try
        {
           // conn.Open();
            sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objBillTransactionEO.SZ_CASE_ID);
            sqlCmd.Parameters.AddWithValue("@DT_BILL_DATE", objBillTransactionEO.DT_BILL_DATE);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillTransactionEO.SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillTransactionEO.SZ_DOCTOR_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", objBillTransactionEO.SZ_TYPE);
            sqlCmd.Parameters.AddWithValue("@SZ_READING_DOCTOR_ID", objBillTransactionEO.SZ_READING_DOCTOR_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_COMPANY_ID", objBillTransactionEO.SZ_Referring_Company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objBillTransactionEO.SZ_USER_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROC_GROUP_ID", objBillTransactionEO.SZ_PROCEDURE_GROUP_ID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();

            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "SP_TXN_LATEST_BILL_TRANSACTIONS";
            sqlCmd.Transaction = transaction;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Connection = conn;

            sqlCmd.Parameters.AddWithValue("@FLAG", "GETLATESTBILLID");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillTransactionEO.SZ_COMPANY_ID);

            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                strBillID = Convert.ToString(dr[0]);
            }
            dr.Close();

            sqlCmd = new SqlCommand("SP_TXN_NOTES", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", _DAO_NOTES_EO.SZ_CASE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", _DAO_NOTES_EO.SZ_USER_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION",strBillID+ _DAO_NOTES_EO.SZ_ACTIVITY_DESC);
            sqlCmd.Parameters.AddWithValue("@IS_DENIED", _DAO_NOTES_EO.IS_DENIED);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _DAO_NOTES_EO.SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();

            #region "Save procedure codes."

            if (p_objProcedureCodes != null)
            {
                if (p_objProcedureCodes.Count > 0)
                {
                    for (int i = 0; i < p_objProcedureCodes.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                        objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];
                        sqlCmd = new SqlCommand();
                        sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        sqlCmd.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Connection = conn;
                        sqlCmd.Transaction = transaction;
                        sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        sqlCmd.Parameters.AddWithValue("@FL_AMOUNT", objBillProcedureCodeEO.FL_AMOUNT);
                        sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", strBillID);
                        sqlCmd.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE);
                        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID);
                        sqlCmd.Parameters.AddWithValue("@I_UNIT", objBillProcedureCodeEO.I_UNIT);
                        sqlCmd.Parameters.AddWithValue("@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE);
                        sqlCmd.Parameters.AddWithValue("@FLT_FACTOR", objBillProcedureCodeEO.FLT_FACTOR);
                        sqlCmd.Parameters.AddWithValue("@DOCT_AMOUNT", objBillProcedureCodeEO.DOCT_AMOUNT);
                        sqlCmd.Parameters.AddWithValue("@PROC_AMOUNT", objBillProcedureCodeEO.PROC_AMOUNT);
                        sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objBillProcedureCodeEO.SZ_CASE_ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID);
                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "&nbsp;")
                            sqlCmd.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString());

                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "&nbsp;")
                            sqlCmd.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString());

                        if (objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "" && objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "&nbsp;")
                        {
                            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_TREATMENT_ID", objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID);
                        }
                        if (objBillProcedureCodeEO.i_cyclic_id != "" && objBillProcedureCodeEO.i_cyclic_id != "&nbsp;" && objBillProcedureCodeEO.i_cyclic_id != null)
                        {
                            sqlCmd.Parameters.AddWithValue("@i_cyclic_id", objBillProcedureCodeEO.i_cyclic_id);
                        }
                        if (objBillProcedureCodeEO.bt_cyclic_applied != "" && objBillProcedureCodeEO.bt_cyclic_applied != "&nbsp;" && objBillProcedureCodeEO.bt_cyclic_applied != null)
                        {
                            sqlCmd.Parameters.AddWithValue("@bt_cyclic_applied", objBillProcedureCodeEO.bt_cyclic_applied);
                        }
                        sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                        sqlCmd.ExecuteNonQuery();


                       
                        
                    }
                }
            }

            #endregion


            sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", strBillID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETEBILLDIAGNOSIS");
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();

            #region "Save Diagnosis Code."

            if (p_objALDiagCode != null)
            {
                if (p_objALDiagCode.Count > 0)
                {
                    for (int iCount = 0; iCount < p_objALDiagCode.Count; iCount++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)p_objALDiagCode[iCount];
                        sqlCmd = new SqlCommand();
                        sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        sqlCmd.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Connection = conn;
                        sqlCmd.Transaction = transaction;
                        sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", strBillID);
                        sqlCmd.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }

            #endregion
            transaction.Commit();
            objResult.bill_no = strBillID;
            objResult.msg = "Sccuess";
            objResult.msg_code = "SCC";

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            objResult.msg_code = "ERR";
            objResult.msg = "System can not generate bill for this case. Contact GreenBills support <br/> " + ex.Message;
            objResult.bill_no = "";
            transaction.Rollback(); ;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return objResult;
    }
    public Result saveReferralBill(BillTransactionEO objBillTransactionEO, DAO_NOTES_EO _DAO_NOTES_EO, ArrayList p_objProcedureCodes, ArrayList p_objALDiagCode,float contractAmount)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        sqlCmd = new SqlCommand();
        sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        string strBillID = "";
        try
        {
            // conn.Open();
            sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objBillTransactionEO.SZ_CASE_ID);
            sqlCmd.Parameters.AddWithValue("@DT_BILL_DATE", objBillTransactionEO.DT_BILL_DATE);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillTransactionEO.SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillTransactionEO.SZ_DOCTOR_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", objBillTransactionEO.SZ_TYPE);
            sqlCmd.Parameters.AddWithValue("@SZ_READING_DOCTOR_ID", objBillTransactionEO.SZ_READING_DOCTOR_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_COMPANY_ID", objBillTransactionEO.SZ_Referring_Company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objBillTransactionEO.SZ_USER_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROC_GROUP_ID", objBillTransactionEO.SZ_PROCEDURE_GROUP_ID);
            sqlCmd.Parameters.AddWithValue("@FLT_CONTRACT_AMOUNT", contractAmount);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();

            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "SP_TXN_LATEST_BILL_TRANSACTIONS";
            sqlCmd.Transaction = transaction;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Connection = conn;

            sqlCmd.Parameters.AddWithValue("@FLAG", "GETLATESTBILLID");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillTransactionEO.SZ_COMPANY_ID);

            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                strBillID = Convert.ToString(dr[0]);
            }
            dr.Close();

            sqlCmd = new SqlCommand("SP_TXN_NOTES", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", _DAO_NOTES_EO.SZ_CASE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", _DAO_NOTES_EO.SZ_USER_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strBillID + _DAO_NOTES_EO.SZ_ACTIVITY_DESC);
            sqlCmd.Parameters.AddWithValue("@IS_DENIED", _DAO_NOTES_EO.IS_DENIED);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _DAO_NOTES_EO.SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();

            #region "Save procedure codes."

            if (p_objProcedureCodes != null)
            {
                if (p_objProcedureCodes.Count > 0)
                {
                    for (int i = 0; i < p_objProcedureCodes.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                        objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];
                        sqlCmd = new SqlCommand();
                        sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        sqlCmd.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Connection = conn;
                        sqlCmd.Transaction = transaction;
                        sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        sqlCmd.Parameters.AddWithValue("@FL_AMOUNT", objBillProcedureCodeEO.FL_AMOUNT);
                        sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", strBillID);
                        sqlCmd.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE);
                        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID);
                        sqlCmd.Parameters.AddWithValue("@I_UNIT", objBillProcedureCodeEO.I_UNIT);
                        sqlCmd.Parameters.AddWithValue("@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE);
                        sqlCmd.Parameters.AddWithValue("@FLT_FACTOR", objBillProcedureCodeEO.FLT_FACTOR);
                        sqlCmd.Parameters.AddWithValue("@DOCT_AMOUNT", objBillProcedureCodeEO.DOCT_AMOUNT);
                        sqlCmd.Parameters.AddWithValue("@PROC_AMOUNT", objBillProcedureCodeEO.PROC_AMOUNT);
                        sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objBillProcedureCodeEO.SZ_CASE_ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID);
                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "&nbsp;")
                            sqlCmd.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString());

                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "&nbsp;")
                            sqlCmd.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString());

                        if (objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "" && objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "&nbsp;")
                        {
                            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_TREATMENT_ID", objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID);
                        }
                        if (objBillProcedureCodeEO.i_cyclic_id != "" && objBillProcedureCodeEO.i_cyclic_id != "&nbsp;" && objBillProcedureCodeEO.i_cyclic_id != null)
                        {
                            sqlCmd.Parameters.AddWithValue("@i_cyclic_id", objBillProcedureCodeEO.i_cyclic_id);
                        }
                        if (objBillProcedureCodeEO.bt_cyclic_applied != "" && objBillProcedureCodeEO.bt_cyclic_applied != "&nbsp;" && objBillProcedureCodeEO.bt_cyclic_applied != null)
                        {
                            sqlCmd.Parameters.AddWithValue("@bt_cyclic_applied", objBillProcedureCodeEO.bt_cyclic_applied);
                        }
                        sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                        sqlCmd.ExecuteNonQuery();




                    }
                }
            }

            #endregion


            sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", strBillID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETEBILLDIAGNOSIS");
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();

            #region "Save Diagnosis Code."

            if (p_objALDiagCode != null)
            {
                if (p_objALDiagCode.Count > 0)
                {
                    for (int iCount = 0; iCount < p_objALDiagCode.Count; iCount++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)p_objALDiagCode[iCount];
                        sqlCmd = new SqlCommand();
                        sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        sqlCmd.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Connection = conn;
                        sqlCmd.Transaction = transaction;
                        sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", strBillID);
                        sqlCmd.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }

            #endregion
            transaction.Commit();
            objResult.bill_no = strBillID;
            objResult.msg = "Sccuess";
            objResult.msg_code = "SCC";

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            objResult.msg_code = "ERR";
            objResult.msg = "System can not generate bill for this case. Contact GreenBills support <br/> " + ex.Message;
            objResult.bill_no = "";
            transaction.Rollback(); ;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return objResult;
    }
    public Result saveBillTransaction(ArrayList objBillTransactionEO,ArrayList objUpdateStatus,ArrayList SaveEventRefferPrcedure, DAO_NOTES_EO _DAO_NOTES_EO, ArrayList p_objProcedureCodes, ArrayList p_objALDiagCode)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        sqlCmd = new SqlCommand();
        sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        string strBillID = "";
        try
        {
            // conn.Open();
            sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objBillTransactionEO[0].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_BILL_DATE", objBillTransactionEO[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID",  objBillTransactionEO[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillTransactionEO[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillTransactionEO[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", objBillTransactionEO[4].ToString());            
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();

            sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandText = "SP_TXN_LATEST_BILL_TRANSACTIONS";
            sqlCmd.Transaction = transaction;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Connection = conn;

            sqlCmd.Parameters.AddWithValue("@FLAG", "GETLATESTBILLID");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillTransactionEO[2].ToString());

            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                strBillID = Convert.ToString(dr[0]);
            }
            dr.Close();


            if (objUpdateStatus.Count>0)
            {
                for (int i = 0; i < objUpdateStatus.Count; i++)
                {
                     UpdateEventStatus objUpdateEventStatus = new UpdateEventStatus();
                     objUpdateEventStatus = (UpdateEventStatus)objUpdateStatus[i];

                     sqlCmd = new SqlCommand();
                     sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                     sqlCmd.CommandText = "UPDATE_EVENT_STATUS";
                     sqlCmd.CommandType = CommandType.StoredProcedure;
                     sqlCmd.Connection = conn;
                     sqlCmd.Parameters.AddWithValue("@I_EVENT_ID ", objUpdateEventStatus.I_EVENT_ID);
                     sqlCmd.Parameters.AddWithValue("@BT_STATUS", objUpdateEventStatus.BT_STATUS);
                     
                     sqlCmd.Parameters.AddWithValue("@I_STATUS", objUpdateEventStatus.I_STATUS); 
                     sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", strBillID);
                     sqlCmd.Parameters.AddWithValue("@DT_BILL_DATE", objUpdateEventStatus.DT_BILL_DATE);
                     sqlCmd.ExecuteNonQuery();
                }
            }

              if (SaveEventRefferPrcedure.Count>0)
            {
                for (int i = 0; i < SaveEventRefferPrcedure.Count; i++)
                {
                     UpdateEventStatus objUpdateEventStatus = new UpdateEventStatus();
                    objUpdateEventStatus = (UpdateEventStatus)SaveEventRefferPrcedure[i];
                    sqlCmd = new SqlCommand();
                    sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    sqlCmd.CommandText = "SP_SAVE_REFERRAL_PROC_CODE";
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Connection = conn;
                    sqlCmd.Parameters.AddWithValue("@SZ_PROC_CODE ", objUpdateEventStatus.sz_procode_id);
                    sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", objUpdateEventStatus.I_EVENT_ID);
                    sqlCmd.Parameters.AddWithValue("@I_STATUS", objUpdateEventStatus.I_STATUS);
                    sqlCmd.ExecuteNonQuery();
                }
              }


            sqlCmd = new SqlCommand("SP_TXN_NOTES", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", _DAO_NOTES_EO.SZ_CASE_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", _DAO_NOTES_EO.SZ_USER_ID);
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
            sqlCmd.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strBillID + _DAO_NOTES_EO.SZ_ACTIVITY_DESC);
            sqlCmd.Parameters.AddWithValue("@IS_DENIED", _DAO_NOTES_EO.IS_DENIED);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _DAO_NOTES_EO.SZ_COMPANY_ID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
            sqlCmd.ExecuteNonQuery();

            #region "Save procedure codes."

            if (p_objProcedureCodes != null)
            {
                if (p_objProcedureCodes.Count > 0)
                {
                    for (int i = 0; i < p_objProcedureCodes.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                        objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];
                        sqlCmd = new SqlCommand();
                        sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        sqlCmd.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Connection = conn;
                        sqlCmd.Transaction = transaction;
                        sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        sqlCmd.Parameters.AddWithValue("@FL_AMOUNT", objBillProcedureCodeEO.FL_AMOUNT);
                        sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", strBillID);
                        sqlCmd.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE);
                        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID);
                        sqlCmd.Parameters.AddWithValue("@I_UNIT", objBillProcedureCodeEO.I_UNIT);
                        sqlCmd.Parameters.AddWithValue("@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE);
                        sqlCmd.Parameters.AddWithValue("@FLT_FACTOR", objBillProcedureCodeEO.FLT_FACTOR);
                        sqlCmd.Parameters.AddWithValue("@DOCT_AMOUNT", objBillProcedureCodeEO.DOCT_AMOUNT);
                        sqlCmd.Parameters.AddWithValue("@PROC_AMOUNT", objBillProcedureCodeEO.PROC_AMOUNT);
                        sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objBillProcedureCodeEO.SZ_CASE_ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID);
                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "&nbsp;")
                            sqlCmd.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString());

                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "&nbsp;")
                            sqlCmd.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString());

                        if (objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "" && objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "&nbsp;")
                        {
                            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_TREATMENT_ID", objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID);
                        }
                        if (objBillProcedureCodeEO.i_cyclic_id != "" && objBillProcedureCodeEO.i_cyclic_id != "&nbsp;" && objBillProcedureCodeEO.i_cyclic_id != null)
                        {
                            sqlCmd.Parameters.AddWithValue("@i_cyclic_id", objBillProcedureCodeEO.i_cyclic_id);
                        }
                        if (objBillProcedureCodeEO.bt_cyclic_applied != "" && objBillProcedureCodeEO.bt_cyclic_applied != "&nbsp;" && objBillProcedureCodeEO.bt_cyclic_applied != null)
                        {
                            sqlCmd.Parameters.AddWithValue("@bt_cyclic_applied", objBillProcedureCodeEO.bt_cyclic_applied);
                        }
                        sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                        sqlCmd.ExecuteNonQuery();




                    }
                }
            }

            #endregion


            sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", conn);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", strBillID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETEBILLDIAGNOSIS");
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.ExecuteNonQuery();

            #region "Save Diagnosis Code."

            if (p_objALDiagCode != null)
            {
                if (p_objALDiagCode.Count > 0)
                {
                    for (int iCount = 0; iCount < p_objALDiagCode.Count; iCount++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)p_objALDiagCode[iCount];
                        sqlCmd = new SqlCommand();
                        sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        sqlCmd.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Connection = conn;
                        sqlCmd.Transaction = transaction;
                        sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID);
                        sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", strBillID);
                        sqlCmd.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }

            #endregion
            transaction.Commit();
            objResult.bill_no = strBillID;
            objResult.msg = "Sccuess";
            objResult.msg_code = "SCC";

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            objResult.msg_code = "ERR";
            objResult.msg = "System can not generate bill for this case. Contact GreenBills support <br/> " + ex.Message;
            objResult.bill_no = "";
            transaction.Rollback(); ;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return objResult;
    }
}