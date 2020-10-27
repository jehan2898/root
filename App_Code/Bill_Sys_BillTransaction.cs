using BlazeFast.client;
using log4net;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.SqlServer.Management.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;

public class Bill_Sys_BillTransaction_BO
{
    private string strsqlCon;

    private SqlConnection sqlCon;

    private SqlCommand sqlCmd;

    private SqlDataAdapter sqlda;

    private SqlDataReader dr;

    private DataSet ds;

    private static ILog log;

    public Bill_Sys_BillTransaction_BO()
    {
        this.strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        Bill_Sys_BillTransaction_BO.log = LogManager.GetLogger("AJAX_Pages_Bill_sys_new_VisitPopup");
    }

    public int BillTrasaction(string szBillNo, string CheckNo, string checkdate, string checkamount, string companyId, string Comment, string userid, string Billstatus, string flag, string PaymentId)
    {
        int num = 0;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_PAYMENT_TRANSACTIONS", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@FLAG", flag);
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", szBillNo);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CHECK_NUMBER", CheckNo);
                this.sqlCmd.Parameters.AddWithValue("@DT_CHECK_DATE", checkdate);
                this.sqlCmd.Parameters.AddWithValue("@FLT_CHECK_AMOUNT", checkamount);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMMENT", Comment);
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", userid);
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", Billstatus);
                this.sqlCmd.Parameters.AddWithValue("@SZ_PAYMENT_ID", PaymentId);
                num = this.sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return num;
    }

    public DataSet BindTransactionData(string id)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL_NEW", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", id);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public DataSet BindTransactionData(string id, string companyid)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL_NEW", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", id);
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", companyid);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public DataSet CheckAndAddProcedureID(string sz_code_description, string sz_procedure_code, string sz_flt_amount, string szCompanyId, string sz_procedure_group_id)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_CHECK_AND_ADD_PROCEDURE_ID", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_CODE_DESCRIPTION", sz_code_description);
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE", sz_procedure_code);
                this.sqlCmd.Parameters.AddWithValue("@FLT_AMOUNT", sz_flt_amount);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", sz_procedure_group_id);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public bool checkNf2(string companyId, string szCaseId)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        bool flag = false;
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_CHECK_NF2", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
                (new SqlDataAdapter(this.sqlCmd)).Fill(this.ds);
                if (this.ds.Tables[0].Rows[0][0].ToString() == "True")
                {
                    flag = true;
                }
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return flag;
    }

    public bool checkUnit(string p_szDoctorID, string p_szCompanyID)
    {
        bool flag;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_CHECK_UNIT", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_szDoctorID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.dr = this.sqlCmd.ExecuteReader();
                if (this.dr.Read())
                {
                    flag = true;
                    return flag;
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return false;
        }
        finally
        {
            this.sqlCon.Close();
        }
        return flag;
    }

    public bool checkUnitRefferal(string p_szSpecialtyID, string p_szCompanyID)
    {
        bool flag;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_CHECK_UNIT_FOR_REFFERAL", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", p_szSpecialtyID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.dr = this.sqlCmd.ExecuteReader();
                if (this.dr.Read())
                {
                    flag = true;
                    return flag;
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return false;
        }
        finally
        {
            this.sqlCon.Close();
        }
        return flag;
    }

    public void DeleteBillRecord(string p_szBillID, string p_szCompanyID, string p_szStatusOrDelete)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_DELETE_BILL_RECORD", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", p_szBillID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
                this.sqlCmd.Parameters.AddWithValue("@CHANGE_STATUS_DELETE", p_szStatusOrDelete);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "");
                this.sqlCmd.CommandTimeout = 0;
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
    }

    public void DeleteReffBill(string sz_bill_no, string company_id)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_DELETE_REFF_BILL", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", sz_bill_no);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", company_id);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }

    public void DeleteTransactionDetails(string strTransactionDetailID)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_TXN_DETAIL_ID", strTransactionDetailID);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
    }

    public void DeleteTransactionDiagnosis(string billNumber)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", billNumber);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "DELETEBILLDIAGNOSIS");
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
    }

    public void DeleteVerificationAns(ArrayList objArrLst)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        ArrayList arrayLists = new ArrayList();
        try
        {
            try
            {
                this.sqlCon.Open();
                for (int i = 0; i < objArrLst.Count; i++)
                {
                    Bill_Sys_Verification_Desc item = null;
                    item = (Bill_Sys_Verification_Desc)objArrLst[i];
                    this.sqlCmd = new SqlCommand("SP_MANAGE_VERIFICATION_ANSWER", this.sqlCon)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    this.sqlCmd.Parameters.AddWithValue("@sz_bill_number", item.sz_bill_no);
                    this.sqlCmd.Parameters.AddWithValue("@sz_company_id", item.sz_company_id);
                    this.sqlCmd.Parameters.AddWithValue("@i_answer_id", item.sz_answer_id);
                    this.sqlCmd.Parameters.AddWithValue("@i_verification_id", item.sz_verification_id);
                    this.sqlCmd.Parameters.AddWithValue("@bt_operation", 0);
                    this.sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
    }

    public ArrayList DeleteVerificationNotes(ArrayList objArrLst)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        ArrayList arrayLists = new ArrayList();
        try
        {
            try
            {
                this.sqlCon.Open();
                for (int i = 0; i < objArrLst.Count; i++)
                {
                    Bill_sys_Verification_Pop item = null;
                    item = (Bill_sys_Verification_Pop)objArrLst[i];
                    this.sqlCmd = new SqlCommand("SP_DELETE_VERIFICATION", this.sqlCon)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", item.sz_bill_no);
                    this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANYID", item.sz_company_id);
                    this.sqlCmd.Parameters.AddWithValue("@SZ_STATUS_CODE", item.sz_bill_Status);
                    this.sqlCmd.Parameters.AddWithValue("@I_VERIFICATION_ID", item.i_verification_id);
                    this.sqlCmd.Parameters.AddWithValue("@FLAG", item.sz_flag);
                    SqlDataReader sqlDataReader = this.sqlCmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        arrayLists.Add(sqlDataReader.GetValue(1).ToString());
                    }
                    sqlDataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return arrayLists;
    }

    public DataSet GeSelectedDoctorProcedureCode_List(string procCodeID, string companyID, string doctorID, string typeID)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("GET_SELECTED_DOCTOR_PROC_CODE", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE", procCodeID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_TYPE_ID", typeID);
                (new SqlDataAdapter(this.sqlCmd)).Fill(this.ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
    }

    public string Get_Max_Transaction_id()
    {
        string str;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        string str1 = "";
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("GET_MAX_BILL_TRANSACTION_ID", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    str1 = Convert.ToString(this.dr.GetValue(0).ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                str = null;
                return str;
            }
            return str1;
        }
        finally
        {
            this.dr.Close();
        }
        return str;
    }

    public ArrayList Get_Node_Type(ArrayList arr)
    {
        ArrayList arrayLists;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        Bill_Sys_Verification_Desc billSysVerificationDesc = new Bill_Sys_Verification_Desc();
        ArrayList arrayLists1 = new ArrayList();
        this.sqlCon.Open();
        try
        {
            try
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    billSysVerificationDesc = (Bill_Sys_Verification_Desc)arr[i];
                    this.sqlCmd = new SqlCommand("SP_CHECK_NODE", this.sqlCon)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", billSysVerificationDesc.sz_bill_no);
                    this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", billSysVerificationDesc.sz_company_id);
                    this.sqlCmd.Parameters.AddWithValue("@SZ_PROCESS", billSysVerificationDesc.sz_flag);
                    this.sqlCmd.CommandTimeout = 0;
                    this.dr = this.sqlCmd.ExecuteReader();
                    while (this.dr.Read())
                    {
                        arrayLists1.Add(Convert.ToString(this.dr.GetValue(0).ToString()));
                    }
                    if (arrayLists1.Contains("NFVER"))
                    {
                        break;
                    }
                    this.dr.Close();
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                arrayLists = null;
                return arrayLists;
            }
            return arrayLists1;
        }
        finally
        {
            this.dr.Close();
        }
        return arrayLists;
    }

    public ArrayList Get_Node_Type(ArrayList arr, ServerConnection connection)
    {
        ArrayList arrayLists;
        // this.sqlCon = new SqlConnection(this.strsqlCon);
        Bill_Sys_Verification_Desc billSysVerificationDesc = new Bill_Sys_Verification_Desc();
        ArrayList arrayLists1 = new ArrayList();
        //this.sqlCon.Open();
        try
        {
            try
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    billSysVerificationDesc = (Bill_Sys_Verification_Desc)arr[i];

                    string Query = "";
                    Query = Query + "Exec SP_CHECK_NODE ";
                    Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", billSysVerificationDesc.sz_bill_no, ",");
                    Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", billSysVerificationDesc.sz_company_id, ",");
                    Query = Query + string.Format("{0}='{1}'{2}", "@SZ_PROCESS", billSysVerificationDesc.sz_flag, ",");

                    Query = Query.TrimEnd(',');



                    this.dr = connection.ExecuteReader(Query);

                    while (this.dr.Read())
                    {
                        arrayLists1.Add(Convert.ToString(this.dr.GetValue(0).ToString()));
                    }
                    if (arrayLists1.Contains("NFVER"))
                    {
                        break;
                    }
                    this.dr.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
                // Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                // arrayLists = null;
                // return arrayLists;
            }
            return arrayLists1;
        }
        finally
        {
            if (this.dr != null && !dr.IsClosed)
            {

                this.dr.Close();
            }
        }

    }

    public DataSet GET_PROC_CODE_USING_EVENT_ID(string sz_I_Event_ID)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_PROC_CODE_USING_EVENT_ID", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", sz_I_Event_ID);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public DataSet GetAllBills(string sz_Company_Id, string sz_bill_number, string sz_case_no, string sz_bill_status_id, string sz_patient_name, string sz_from_date, string sz_to_date, string sz_visit_date, string sz_from_visit_date, string sz_procedure_code_id, string sz_flt_amount, string sz_flt_to_amount, string sz_search_text)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("GET_ALL_BILLS_FOR_COMPANY", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_Id);
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_bill_number);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", sz_case_no);
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", sz_bill_status_id);
                this.sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", sz_patient_name);
                this.sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", sz_from_date);
                this.sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", sz_to_date);
                this.sqlCmd.Parameters.AddWithValue("@VISIT_DATE", sz_visit_date);
                this.sqlCmd.Parameters.AddWithValue("@FROM_VISIT_DATE", sz_from_visit_date);
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", sz_procedure_code_id);
                this.sqlCmd.Parameters.AddWithValue("@FLT_AMOUNT", sz_flt_amount);
                this.sqlCmd.Parameters.AddWithValue("@FLT_TO_AMOUNT", sz_flt_to_amount);
                this.sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", sz_search_text);
                (new SqlDataAdapter(this.sqlCmd)).Fill(this.ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
    }

    public DataSet Getalldetailsfor1500(string szCompanyId, string szSpecialtyId, string szCasetypeId, string szInsuranceId, string flag)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("sp_get_all_details_for_1500", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szSpecialtyId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", szCasetypeId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", szInsuranceId);
                this.sqlCmd.Parameters.AddWithValue("@flag", flag);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public DataSet GetAssociatedEntry(string setId)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("GETSETASSOCIATED_DIAGNOSIS_LIST", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_SET_ID", setId);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public DataSet GetAssociateDiagCode(string sz_procedurecode, string sz_case_id, string sz_company_id)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_REFF_PROCEDURE_CODES", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", sz_procedurecode);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "GETASSICIATEDIAGCODE");
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return dataSet;
    }

    public DataSet GetBillDetail(string szBillNo)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_PAYMENT_TRANSACTIONS", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", szBillNo);
                (new SqlDataAdapter(this.sqlCmd)).Fill(this.ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
    }

    public ArrayList GetBillInfo(string p_szBillID)
    {
        ArrayList arrayLists = new ArrayList();
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillID);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "GET_BILL_INFO");
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    arrayLists.Add(this.dr["SZ_DOCTOR_ID"].ToString());
                    arrayLists.Add(this.dr["SZ_READING_DOCTOR_ID"].ToString());
                    arrayLists.Add(this.dr["SZ_BILL_NUMBER"].ToString());
                    arrayLists.Add(this.dr["SZ_CASE_ID"].ToString());
                    arrayLists.Add(this.dr["DT_BILL_DATE"].ToString());
                    arrayLists.Add(this.dr["DT_BILL_DATE"].ToString());
                    arrayLists.Add(this.dr["SZ_SPECIALITY_ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return arrayLists;
    }

    public DataSet GetBills(string sz_company_id, string sz_login_company_id, string sz_office_id, string sz_search_text, string flag)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        DataSet dataSet = new DataSet();
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("sp_get_all_Office_Bill", this.sqlCon)
            {
                CommandTimeout = 0,
                CommandType = CommandType.StoredProcedure
            };
            this.sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_company_id);
            this.sqlCmd.Parameters.AddWithValue("@sz_login_company_id", sz_login_company_id);
            this.sqlCmd.Parameters.AddWithValue("@sz_office_id", sz_office_id);
            this.sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", sz_search_text);
            this.sqlCmd.Parameters.AddWithValue("@sz_flag", flag);
            this.sqlda = new SqlDataAdapter(this.sqlCmd);
            this.sqlda.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dataSet;
    }

    public string GetBillVisitDate(string p_szBillID, string p_szCompanyID)
    {
        string str = "";
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_BILL_VISIT_DATE", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_szBillID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    str = Convert.ToString(this.dr["DT_VISIT_DATE"].ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return str;
    }

    public int GetCaseCount(string procName, string flag, string companyid)
    {
        int num = 0;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand(procName, this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", flag);
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    num = Convert.ToInt32(this.dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return num;
    }

    public string GetCaseType(string caseID)
    {
        string str = "";
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("GETCASETYPE", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    str = Convert.ToString(this.dr[1].ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return str;
    }

    public DataSet GetCaseTypeID(string companyId, string CaseId)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_CASETYPEID", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", CaseId);
                (new SqlDataAdapter(this.sqlCmd)).Fill(this.ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
    }

    public ArrayList GetClaimInsurance(string p_szCaseID)
    {
        ArrayList arrayLists = new ArrayList();
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_CLAIMINSURANCE", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    arrayLists.Add(this.dr["SZ_INSURANCE_ID"].ToString());
                    arrayLists.Add(this.dr["SZ_CLAIM_NUMBER"].ToString());
                    arrayLists.Add(this.dr["SZ_INSURANCE_ADDRESS"].ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return arrayLists;
    }

    public DataSet GetCodeAmount(string szCaseID, string szCompanyID, string szInsuranceID, string szSpecilatyID, DataTable tblProcCode)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("sp_get_predefined_amount_schedule", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@i_case_id", szCaseID);
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", szCompanyID);
                this.sqlCmd.Parameters.AddWithValue("@sz_insurance_id", szInsuranceID);
                this.sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", szSpecilatyID);
                SqlParameter sqlParameter = this.sqlCmd.Parameters.AddWithValue("@typ_cyclic_type_code", tblProcCode);
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = "typ_cyclic_type_code";
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public ArrayList GetDenialReason(string[] _reason, string CompanyID)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        ArrayList arrayLists = new ArrayList();
        try
        {
            try
            {
                this.sqlCon.Open();
                for (int i = 0; i < (int)_reason.Length; i++)
                {
                    this.sqlCmd = new SqlCommand("SP_MST_DENIAL", this.sqlCon)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    this.sqlCmd.Parameters.Add("@SZ_DENIAL_REASON", _reason[i].ToString().Trim());
                    this.sqlCmd.Parameters.Add("@ID", CompanyID);
                    this.sqlCmd.Parameters.AddWithValue("@FLAG", "DENIAL_CODE");
                    SqlDataReader sqlDataReader = this.sqlCmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        arrayLists.Add(sqlDataReader.GetValue(0).ToString());
                    }
                    sqlDataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return arrayLists;
    }

    public DataSet GetDiagCodeTypeList(string szCompanyId)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_MST_DIAGNOSIS_TYPE", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@ID", szCompanyId);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "DIAGNOSIS_TYPE_LIST");
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public string GetDocSpeciality(string sz_billno)
    {
        string str;
        string str1 = "";
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_MRI_SPCIALITY", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_billno);
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    str1 = this.dr.GetValue(0).ToString();
                }
                str = str1;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return str;
    }

    public DataSet GetDoctorProcedureCodeList(string szDoctorId, string szTypeId, string szCaseID)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("GET_DOCTOR_PROCEDURE_CODE", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", szDoctorId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_TYPE_ID", szTypeId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public string GetDoctorSpeciality(string p_szBillID, string p_szCompanyID)
    {
        string str = "";
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_DOCTOR_SPECIALITY", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", p_szBillID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    str = Convert.ToString(this.dr["SPECIALTTY"].ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return str;
    }
    public string GetDoctorSpeciality(string p_szBillID, string p_szCompanyID, ServerConnection conn)
    {
        string str = "";
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                string Query = "";
                Query = Query + "Exec SP_GET_DOCTOR_SPECIALITY ";
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", p_szBillID, ",");
                Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", p_szCompanyID, ",");

                Query = Query.TrimEnd(',');

                this.dr = conn.ExecuteReader(Query);
                while (this.dr.Read())
                {
                    str = Convert.ToString(this.dr["SPECIALTTY"].ToString());
                }
                this.dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                // Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.dr != null && !dr.IsClosed)
            {

                this.dr.Close();
            }
        }
        return str;
    }

    public DataSet GetDoctorSpecialityAddToPrecedureListProcedureCodeList(string szDoctorId, string szCompanyId)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("GET_DOCT_SPECIALITY_ADD_TO_PREFERED_LIST_PROCEDURECODE", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", szDoctorId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public DataSet GetDoctorSpecialityGroupProcedureCodeList(string szDoctorId, string szCompanyId)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_GROUP_PROCEDURE_CODES", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", szDoctorId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "GETGROUPLIST");
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public DataSet GetDoctorSpecialityProcedureCodeList(string szDoctorId, string szCompanyId)
    {
        DataSet dataSet;
        DataSet dataSet1 = new DataSet();
        Bill_Sys_BillTransaction_BO.log.Debug("In GetDoctorSpecialityProcedureCodeList method to retrive procedure codes");
        Bill_Sys_BillTransaction_BO.log.Debug(string.Concat("Parameter DoctorId - ", szDoctorId));
        Bill_Sys_BillTransaction_BO.log.Debug(string.Concat("Parameter CompanyId - ", szCompanyId));
        bool flag = true;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                try
                {
                    dataSet1 = (new Client_Select()).@select("ALL_PROCEDURE_CODES", szCompanyId, szDoctorId);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    Bill_Sys_BillTransaction_BO.log.Debug(string.Concat("Error BlazeFast", exception.Message.ToString()));
                }
                if (dataSet1.Tables.Count > 0 && dataSet1.Tables[0].Rows.Count > 0)
                {
                    Bill_Sys_BillTransaction_BO.log.Debug("Data fetched from BlazeFast Service");
                    flag = false;
                }
                if (flag)
                {
                    Bill_Sys_BillTransaction_BO.log.Debug("No data found from BlazeFast Service");
                    this.sqlCon.Open();
                    this.sqlCmd = new SqlCommand("GET_DOCT_SPECIALITY_PROCEDURECODE", this.sqlCon)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", szDoctorId);
                    this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                    this.sqlda = new SqlDataAdapter(this.sqlCmd);
                    dataSet1 = new DataSet();
                    this.sqlda.Fill(dataSet1);
                    Bill_Sys_BillTransaction_BO.log.Debug("Data fetched from \"GET_DOCT_SPECIALITY_PROCEDURECODE\" stored procedure.");
                }
                dataSet = dataSet1;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public DataSet GetExcludeBillInformation(string companyId)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_EXCLUDE_BILL_INFO", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
                (new SqlDataAdapter(this.sqlCmd)).Fill(this.ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
    }

    public DataSet GetLocationIdusingBillID(string sz_bill_id, string szCompanyId)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_LOCATION_USING_BILLID", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", sz_bill_id);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public DataSet getModifiers(string szCompanyId)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("sp_get_modifier", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", szCompanyId);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public DataSet GetNF3ProcCodeList(string szDoctorId, string szCompanyId)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("GET_DOCT_SPECIALITY_NF3_PROCEDURECODE", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", szDoctorId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public string GetNodeID(string CompanyId, string CaseId, string NodeType)
    {
        string str;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        string str1 = "";
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_NODE_ID", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", CaseId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_NODE_ID", NodeType);
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    str1 = Convert.ToString(this.dr.GetValue(0).ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                str = null;
                return str;
            }
            return str1;
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

    public ArrayList GetNodeID_Nodes(string P_Company_Id, ArrayList arr_Node_Type)
    {
        ArrayList arrayLists;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        ArrayList arrayLists1 = new ArrayList();
        try
        {
            try
            {
                this.sqlCon.Open();
                for (int i = 0; i < arr_Node_Type.Count; i++)
                {
                    this.sqlCmd = new SqlCommand("SP_GET_NODEID_MST_NODES", this.sqlCon)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                    this.sqlCmd.Parameters.AddWithValue("@SZ_NODE_TYPE", arr_Node_Type[i]);
                    this.dr = this.sqlCmd.ExecuteReader();
                    while (this.dr.Read())
                    {
                        arrayLists1.Add(Convert.ToString(this.dr.GetValue(0).ToString()));
                    }
                    this.dr.Close();
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                arrayLists = null;
                return arrayLists;
            }
            return arrayLists1;
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return arrayLists;
    }

    public string GetNodeIDMST_Nodes(string P_Company_Id, string P_Node_Type)
    {
        string str;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        string str1 = "";
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_NODEID_MST_NODES", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                this.sqlCmd.Parameters.AddWithValue("@SZ_NODE_TYPE", P_Node_Type);
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    str1 = Convert.ToString(this.dr.GetValue(0).ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                str = null;
                return str;
            }
            return str1;
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

    public string GetNodeType(string szCompanyId, string szSpecialityID, string szCaseID)
    {
        string str;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        string str1 = "";
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_NODETYPE", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY_ID", szSpecialityID);
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    str1 = Convert.ToString(this.dr.GetValue(0).ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                str = null;
                return str;
            }
            return str1;
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

    public DataSet GetPathOfBills(string sz_Bills, string sz_Company_Id, string sz_Flag)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_BILLS_PATH_INFO", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_Bills);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_Id);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", sz_Flag);
                (new SqlDataAdapter(this.sqlCmd)).Fill(this.ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
    }

    public DataSet GetPatientDoctor_List(string patientId, string companyID)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("GET_PATIENT_DOCTOR", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
                (new SqlDataAdapter(this.sqlCmd)).Fill(this.ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
    }

    public double GetProcAmount(string procid, string caseid, string companyid)
    {
        double num = 0;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", procid);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseid);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "GET_TRANS_AMT");
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    num = Convert.ToDouble(this.dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return num;
    }

    public DataSet GetProcedure_Code_Amount(string companyId)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_MST_PROCEDURE_CODE_AMOUNT", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "GET_Procedure_Code_Amount_LIST");
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
                (new SqlDataAdapter(this.sqlCmd)).Fill(this.ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
    }

    public DataSet GetProcedureCodeList(string id)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_MST_PROCEDURE_CODES", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@ID", id);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "GET_PROCEDURE_CODE_LIST");
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public DataSet GetProcedureCodes(string sz_procedurecode, string patient_id, string sz_company_id, string sz_case_id)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_REFF_PROCEDURE_CODES", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", sz_procedurecode);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
                this.sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patient_id);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "GETPROCEDURECODES");
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return dataSet;
    }

    public DataSet GetReadingDoctorInformation(string companyId, string DoctorID)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_READING_DOCTOR_INFORMATION", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", DoctorID);
                (new SqlDataAdapter(this.sqlCmd)).Fill(this.ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
    }

    public DataSet GetReferringDoctorMandetoryInfo(string companyId, string CaseTypeId)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_READING_DOCTOR_MANDITORY_FIELDS", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", CaseTypeId);
                (new SqlDataAdapter(this.sqlCmd)).Fill(this.ds);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return this.ds;
    }

    public DataSet GetRoomId(string sz_procedurecode, string sz_company_id)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_ROOM_ID", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDUREGROUP_ID", sz_procedurecode);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return dataSet;
    }

    public ArrayList GetSpecialityIdFromPOM(string szPOMId, string szCompanyId)
    {
        ArrayList arrayLists;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        ArrayList arrayLists1 = new ArrayList();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_SPECIALITYID_FROM_POMID", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                this.sqlCmd.Parameters.AddWithValue("@I_POM_ID", szPOMId);
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    arrayLists1.Add(Convert.ToString(this.dr.GetValue(0).ToString()));
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                arrayLists = null;
                return arrayLists;
            }
            return arrayLists1;
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return arrayLists;
    }

    public string GetSpecId(string P_Company_Id, string szBillNo)
    {
        string str;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        string str1 = "";
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_SPECIALITY_ID", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", P_Company_Id);
                this.sqlCmd.Parameters.AddWithValue("@sz_bill_no", szBillNo);
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    str1 = Convert.ToString(this.dr.GetValue(0).ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                str = null;
                return str;
            }
            return str1;
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

    public string GetSpecName(string P_Company_Id, string szSpecId)
    {
        string str;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        string str1 = "";
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_SPECIALITY_NAME", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                this.sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY_ID", szSpecId);
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    str1 = Convert.ToString(this.dr.GetValue(0).ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                str = null;
                return str;
            }
            return str1;
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

    public string GetStatusCode(string sz_Company_ID, string SZ_STATUS_CODE)
    {
        string str;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        string str1 = "";
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_TXN_BILL_DETAILS", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@BILL_STATUS_ID", SZ_STATUS_CODE);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_ID);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "GETSTATUSCODE");
                this.dr = this.sqlCmd.ExecuteReader();
                while (this.dr.Read())
                {
                    str1 = Convert.ToString(this.dr.GetValue(0).ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                str = null;
                return str;
            }
            return str1;
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

    public DataSet GetTestType()
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("GETTESTTYPE", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }

    public DataSet GroupProcedureCodeList(string szGroupCodeId, string szCompanyId, string szGroupName)
    {
        DataSet dataSet;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_GROUP_PROCEDURE_CODES", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szGroupCodeId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_GROUP_NAME", szGroupName);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "GETPROCCODELIST");
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.ds = new DataSet();
                this.sqlda.Fill(this.ds);
                dataSet = this.ds;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
        return dataSet;
    }
    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;
    public string InsertUpdateBillStatus(ArrayList objAL)
    {
        string str;
        this.sqlCon = new SqlConnection(this.strsqlCon);
        SqlConnection sqlConnection = new SqlConnection(this.strsqlCon);
        this.sqlCon.Open();
        string str1 = "";
        SqlTransaction sqlTransaction = this.sqlCon.BeginTransaction();
        try
        {
            try
            {
                if (this.sqlCon != null)
                {
                    Bill_Sys_Verification_Desc item = null;
                    this.sqlCmd = new SqlCommand("GET_MAX_BILL_TRANSACTION_ID", this.sqlCon)
                    {
                        CommandTimeout = 0,
                        Transaction = sqlTransaction,
                        Connection = this.sqlCon
                    };
                    this.dr = this.sqlCmd.ExecuteReader();
                    while (this.dr.Read())
                    {
                        str1 = Convert.ToString(this.dr.GetValue(0).ToString());
                    }
                    this.dr.Close();
                    sqlConnection.Open();
                    for (int i = 0; i < objAL.Count; i++)
                    {
                        item = (Bill_Sys_Verification_Desc)objAL[i];
                        this.sqlCmd = new SqlCommand("SP_SAVE_VERIFICATION_RECEIVED_DESCRIPTION", sqlConnection)
                        {
                            CommandTimeout = 0,
                            CommandType = CommandType.StoredProcedure
                        };
                        this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", item.sz_bill_no);
                        this.sqlCmd.Parameters.AddWithValue("@SZ_DESCRIPTION", item.sz_description);
                        this.sqlCmd.Parameters.AddWithValue("@DT_VERIFICATION_DATE", item.sz_verification_date);
                        this.sqlCmd.Parameters.AddWithValue("@I_VERIFICATION_TYPE", item.i_verification);
                        this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", item.sz_company_id);
                        this.sqlCmd.Parameters.AddWithValue("@SZ_CREATED_USER_ID", item.sz_user_id);
                        this.sqlCmd.Parameters.AddWithValue("@FLAG", item.sz_flag);
                        this.sqlCmd.Parameters.AddWithValue("@i_id", str1);
                        this.sqlCmd.ExecuteNonQuery();

                        #region Activity_Log
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        if (item.sz_flag != "DEN")
                            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "VER_RECEIVED";
                        else
                            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "DENIAL_ADDED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "for  Bill Id : " + item.sz_bill_no + " On  : " + item.sz_verification_date;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)HttpContext.Current.Session["USER_OBJECT"]).SZ_USER_ID);
                        this._DAO_NOTES_EO.SZ_CASE_ID = new Bill_Sys_BillingCompanyDetails_BO().GetCaseID(item.sz_bill_no, ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                        #endregion
                    }
                }
                sqlTransaction.Commit();
                str = str1;
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
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

    public int RevertStataus(ArrayList al, string sz_compny_id)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        int num = 0;
        DataSet dataSet = new DataSet();
        DataSet dataSet1 = new DataSet();
        SqlTransaction sqlTransaction = null;
        ArrayList arrayLists = new ArrayList();
        ArrayList arrayLists1 = new ArrayList();
        ArrayList arrayLists2 = new ArrayList();
        try
        {
            this.sqlCon.Open();
            sqlTransaction = this.sqlCon.BeginTransaction();
            for (int i = 0; i < al.Count; i++)
            {
                this.sqlCmd = new SqlCommand("get_prev_status", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure,
                    Transaction = sqlTransaction
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_bill_number", al[i].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_compny_id", sz_compny_id);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(dataSet1);
                arrayLists1.Add(dataSet1.Tables[0].Rows[0][0].ToString());
                arrayLists.Add(dataSet1.Tables[0].Rows[0][5].ToString());
                arrayLists2.Add(al[i].ToString());
            }
            for (int j = 0; j < arrayLists1.Count; j++)
            {
                this.sqlCmd = new SqlCommand("sp_revert_ofiice_bill_status", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure,
                    Transaction = sqlTransaction
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_bill_number", arrayLists2[j].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_compny_id", sz_compny_id);
                this.sqlCmd.Parameters.AddWithValue("@sz_bill_status_id", arrayLists[j].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_uid", arrayLists1[j].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            sqlTransaction.Commit();
            num = 1;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            sqlTransaction.Rollback();
            num = 0;
            throw;
        }
        return num;
    }

    public int RevertStatausAll(string sz_company_id, string sz_login_company_id, string sz_office_id, string sz_search_text, string flag)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        int num = 0;
        DataSet dataSet = new DataSet();
        DataSet dataSet1 = new DataSet();
        SqlTransaction sqlTransaction = null;
        ArrayList arrayLists = new ArrayList();
        ArrayList arrayLists1 = new ArrayList();
        ArrayList arrayLists2 = new ArrayList();
        try
        {
            this.sqlCon.Open();
            sqlTransaction = this.sqlCon.BeginTransaction();
            this.sqlCmd = new SqlCommand("sp_get_all_Office_Bill", this.sqlCon)
            {
                CommandTimeout = 0,
                CommandType = CommandType.StoredProcedure,
                Transaction = sqlTransaction
            };
            this.sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_company_id);
            this.sqlCmd.Parameters.AddWithValue("@sz_login_company_id", sz_login_company_id);
            this.sqlCmd.Parameters.AddWithValue("@sz_office_id", sz_office_id);
            this.sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", sz_search_text);
            this.sqlCmd.Parameters.AddWithValue("@sz_flag", flag);
            this.sqlda = new SqlDataAdapter(this.sqlCmd);
            this.sqlda.Fill(dataSet);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                this.sqlCmd = new SqlCommand("get_prev_status", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure,
                    Transaction = sqlTransaction
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_bill_number", dataSet.Tables[0].Rows[i][0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_compny_id", sz_login_company_id);
                (new SqlDataAdapter(this.sqlCmd)).Fill(dataSet1);
                arrayLists1.Add(dataSet1.Tables[0].Rows[0][0].ToString());
                arrayLists.Add(dataSet1.Tables[0].Rows[0][5].ToString());
                arrayLists2.Add(dataSet.Tables[0].Rows[i][0].ToString());
            }
            for (int j = 0; j < arrayLists1.Count; j++)
            {
                this.sqlCmd = new SqlCommand("sp_revert_ofiice_bill_status", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure,
                    Transaction = sqlTransaction
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_bill_number", arrayLists2[j].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_compny_id", sz_login_company_id);
                this.sqlCmd.Parameters.AddWithValue("@sz_bill_status_id", arrayLists[j].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_uid", arrayLists1[j].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            sqlTransaction.Commit();
            num = 1;
        }
        catch (Exception ex)
        {
            sqlTransaction.Rollback();
            num = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return num;
    }

    public void SaveTransactionData(ArrayList _arrayList)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", _arrayList[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FL_AMOUNT", _arrayList[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", _arrayList[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", _arrayList[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", _arrayList[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@I_UNIT", _arrayList[5].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FLT_PRICE", _arrayList[6].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FLT_FACTOR", _arrayList[7].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DOCT_AMOUNT", _arrayList[6].ToString());
                this.sqlCmd.Parameters.AddWithValue("@PROC_AMOUNT", _arrayList[9].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", _arrayList[10].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", _arrayList[11].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", _arrayList[12].ToString());
                if (_arrayList[14].ToString() != "" && _arrayList[14].ToString() != "&nbsp;")
                {
                    this.sqlCmd.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", _arrayList[14].ToString());
                }
                if (_arrayList[13].ToString() != "" && _arrayList[13].ToString() != "&nbsp;")
                {
                    this.sqlCmd.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", _arrayList[13].ToString());
                }
                if (_arrayList.Count >= 16)
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_TREATMENT_ID", _arrayList[15].ToString());
                }
                if (_arrayList.Count >= 17)
                {
                    this.sqlCmd.Parameters.AddWithValue("@i_cyclic_id", _arrayList[16].ToString());
                }
                if (_arrayList.Count >= 18)
                {
                    this.sqlCmd.Parameters.AddWithValue("@bt_cyclic_applied", _arrayList[17].ToString());
                }
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
    }

    public void SaveTransactionDiagnosis(ArrayList _arrayList)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", _arrayList[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", _arrayList[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
    }

    public void saveVerificationReceivedInformation(ArrayList p_objALVerReceived)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SAVE_VERIFICATION_RECEIVED", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objALVerReceived[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_VERIFICATION_RECEIVED_NOTES", p_objALVerReceived[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objALVerReceived[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_VERIFICATION_RECEIVED_USER_ID", p_objALVerReceived[3].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
    }

    public void SaveVerificationRequest(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SAVE_VERIFICATION_REQUEST", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_VERIFICATION_NOTES", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[3].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
    }

    public void updateBillStatusToDenial(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("SP_UPDATE_VERIFICATION_RECEIVED_STATUS_DENIAL", this.sqlCon)
            {
                CommandTimeout = 0,
                CommandType = CommandType.StoredProcedure
            };
            this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID_LIST", objAL[0]);
            this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1]);
            SqlParameterCollection parameters = this.sqlCmd.Parameters;
            DateTime now = DateTime.Now;
            parameters.AddWithValue("@DT_BILL_DENIAL_DATE", now.ToString("MM/dd/yyyy"));
            this.sqlCmd.Parameters.AddWithValue("@SZ_DENIAL_USER_ID", objAL[2]);
            this.sqlCmd.Parameters.AddWithValue("@SZ_DENIAL_NOTES", objAL[3]);
            this.sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    public void UpdateDeleteDenilReason(ArrayList _objlist, string flag, string szVerificationid)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                for (int i = 0; i < _objlist.Count; i++)
                {
                    this.sqlCmd = new SqlCommand("SP_UPDATE_DELETE_DENIAL_REASON", this.sqlCon)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    this.sqlCmd.Parameters.AddWithValue("@I_DENIAL_REASON_ID", _objlist[i].ToString());
                    this.sqlCmd.Parameters.AddWithValue("@I_VERIFICATION_ID", szVerificationid);
                    this.sqlCmd.Parameters.AddWithValue("@FLAG", flag);
                    this.sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }

    public void UpdateDenialNotes(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            this.sqlCon.Open();
            this.sqlCmd = new SqlCommand("SP_UPDATE_STATUS_DENIAL", this.sqlCon)
            {
                CommandTimeout = 0,
                CommandType = CommandType.StoredProcedure
            };
            this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID_LIST", objAL[0]);
            this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1]);
            this.sqlCmd.Parameters.AddWithValue("@SZ_DENIAL_USER_ID", objAL[2]);
            this.sqlCmd.Parameters.AddWithValue("@SZ_DENIAL_NOTES", objAL[3]);
            this.sqlCmd.Parameters.AddWithValue("@I_VERIFICATION_ID", objAL[4].ToString());
            try
            {
                if (objAL.Count > 5)
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_VERIFICATION_DATE", objAL[5].ToString());
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            this.sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    public void UpdateDenialReason(string denial, ArrayList _objden, string _szBillno)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                for (int i = 0; i < _objden.Count; i++)
                {
                    this.sqlCmd = new SqlCommand("SP_UPDATE_DENIAL_REASON", this.sqlCon)
                    {
                        CommandTimeout = 0,
                        CommandType = CommandType.StoredProcedure
                    };
                    this.sqlCmd.Parameters.AddWithValue("@I_DENIAL_REASON_ID", _objden[i]);
                    this.sqlCmd.Parameters.AddWithValue("@I_TRANSACTION_ID", denial);
                    this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", _szBillno);
                    this.sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }

    public int UpdateStataus(ArrayList al, string sz_compny_id, string sz_satus_code)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        int num = 0;
        DataSet dataSet = new DataSet();
        SqlTransaction sqlTransaction = null;
        try
        {
            this.sqlCon.Open();
            sqlTransaction = this.sqlCon.BeginTransaction();
            for (int i = 0; i < al.Count; i++)
            {
                this.sqlCmd = new SqlCommand("update_ofiice_bill_status", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure,
                    Transaction = sqlTransaction
                };
                this.sqlCmd.Parameters.AddWithValue("@bill_number", al[i].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_compny_id);
                this.sqlCmd.Parameters.AddWithValue("@sz_bill_code", sz_satus_code);
                this.sqlCmd.ExecuteNonQuery();
            }
            sqlTransaction.Commit();
            num = 1;
        }
        catch (Exception ex)
        {
            sqlTransaction.Rollback();
            num = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return num;
    }

    public int UpdateStatausAll(string sz_company_id, string sz_login_company_id, string sz_office_id, string sz_search_text, string sz_bill_code, string flag)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        int num = 0;
        DataSet dataSet = new DataSet();
        SqlTransaction sqlTransaction = null;
        try
        {
            this.sqlCon.Open();
            sqlTransaction = this.sqlCon.BeginTransaction();
            this.sqlCmd = new SqlCommand("sp_get_all_Office_Bill", this.sqlCon)
            {
                CommandTimeout = 0,
                CommandType = CommandType.StoredProcedure,
                Transaction = sqlTransaction
            };
            this.sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_company_id);
            this.sqlCmd.Parameters.AddWithValue("@sz_login_company_id", sz_login_company_id);
            this.sqlCmd.Parameters.AddWithValue("@sz_office_id", sz_office_id);
            this.sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", sz_search_text);
            this.sqlCmd.Parameters.AddWithValue("@sz_flag", flag);
            this.sqlda = new SqlDataAdapter(this.sqlCmd);
            this.sqlda.Fill(dataSet);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                this.sqlCmd = new SqlCommand("update_ofiice_bill_status", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure,
                    Transaction = sqlTransaction
                };
                this.sqlCmd.Parameters.AddWithValue("@bill_number", dataSet.Tables[0].Rows[i][0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_login_company_id);
                this.sqlCmd.Parameters.AddWithValue("@sz_bill_code", sz_bill_code);
                this.sqlCmd.ExecuteNonQuery();
            }
            sqlTransaction.Commit();
            num = 1;
        }
        catch (Exception ex)
        {
            sqlTransaction.Rollback();
            num = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return num;
    }

    public int UpdateStatus(ArrayList al, string sz_compny_id, string sz_satus_code)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        int num = 0;
        DataSet dataSet = new DataSet();
        SqlTransaction sqlTransaction = null;
        try
        {
            this.sqlCon.Open();
            sqlTransaction = this.sqlCon.BeginTransaction();
            for (int i = 0; i < al.Count; i++)
            {
                this.sqlCmd = new SqlCommand("update_ofiice_bill_status", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure,
                    Transaction = sqlTransaction
                };
                this.sqlCmd.Parameters.AddWithValue("@bill_number", al[i].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_compny_id);
                this.sqlCmd.Parameters.AddWithValue("@sz_bill_code", sz_satus_code);
                num = this.sqlCmd.ExecuteNonQuery();
            }
            sqlTransaction.Commit();
        }
        catch (Exception ex)
        {
            sqlTransaction.Rollback();
            num = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return num;
    }

    public int UpdateStatusAll(string sz_company_id, string sz_login_company_id, string sz_office_id, string sz_search_text, string sz_bill_code, string flag)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        int num = 0;
        DataSet dataSet = new DataSet();
        SqlTransaction sqlTransaction = null;
        try
        {
            this.sqlCon.Open();
            sqlTransaction = this.sqlCon.BeginTransaction();
            this.sqlCmd = new SqlCommand("sp_update_all_Office_Bill_to_soldloaned", this.sqlCon)
            {
                CommandTimeout = 0,
                CommandType = CommandType.StoredProcedure,
                Transaction = sqlTransaction
            };
            this.sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_company_id);
            this.sqlCmd.Parameters.AddWithValue("@sz_login_company_id", sz_login_company_id);
            this.sqlCmd.Parameters.AddWithValue("@sz_office_id", sz_office_id);
            this.sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", sz_search_text);
            this.sqlCmd.Parameters.AddWithValue("@sz_flag", flag);
            this.sqlCmd.Parameters.AddWithValue("@sz_bill_code", sz_bill_code);
            num = this.sqlCmd.ExecuteNonQuery();
            sqlTransaction.Commit();
        }
        catch (Exception ex)
        {
            sqlTransaction.Rollback();
            num = 0;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return num;
    }

    public void UpdateTransactionData(ArrayList _arrayList)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", this.sqlCon)
                {
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_TXN_DETAIL_ID", _arrayList[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", _arrayList[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FL_AMOUNT", _arrayList[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", _arrayList[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", _arrayList[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@I_UNIT", _arrayList[6].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FLT_PRICE", _arrayList[7].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FLT_FACTOR", _arrayList[8].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DOCT_AMOUNT", _arrayList[7].ToString());
                this.sqlCmd.Parameters.AddWithValue("@PROC_AMOUNT", _arrayList[10].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", _arrayList[11].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
    }

    public void UpdateVerificationReceivedInformation(ArrayList p_objALVerReceived)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_UPDATE_VERIFICATION_RECEIVED", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objALVerReceived[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_VERIFICATION_RECEIVED_NOTES", p_objALVerReceived[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objALVerReceived[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_VERIFICATION_RECEIVED_USER_ID", p_objALVerReceived[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@I_VERIFICATION_ID", p_objALVerReceived[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DT_VERIFICATION_DATE", p_objALVerReceived[5].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
    }

    public void UpdateVerificationRequest(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_UPDATE_VERIFICATION_REQUEST", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_VERIFICATION_NOTES", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@I_VERIFICATION_ID", objAL[4].ToString());
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            this.sqlCon.Close();
        }
    }
    public DataSet FillEORReasons(string companyId)
    {
        /*string constr = ConfigurationManager.AppSettings["Connection_String"].ToString();
        List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@FLAG", "DENIAL_LIST"),
            new SqlParameter("@ID", companyId)
        };
        DataSet ds = SqlHelper.ExecuteDataset(constr, CommandType.StoredProcedure, "SP_MST_DENIAL", parameters.ToArray());
        return ds;*/
        string constr = ConfigurationManager.AppSettings["Connection_String"].ToString();
        List<SqlParameter> parameters = new List<SqlParameter>
            {
                //new SqlParameter("@FLAG", "DENIAL_LIST"),
                new SqlParameter("@SZ_COMPANY_ID", companyId)
            };
        DataSet ds = SqlHelper.ExecuteDataset(constr, CommandType.StoredProcedure, "SP_EOR_BILLING_REASON", parameters.ToArray());
        return ds;
    }

    public void UpdateEORReason(string denial, DataTable EORReasonsWithBill)
    {
        this.sqlCon = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.sqlCon.Open();

                this.sqlCmd = new SqlCommand("SP_UPDATE_EOR_REASON", this.sqlCon)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@EOR_BILL_MAPPING", EORReasonsWithBill);
                this.sqlCmd.Parameters.AddWithValue("@I_TRANSACTION_ID", denial);
                this.sqlCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
    }
    public DataSet GetBilEORDetails(DataTable billNumberData, string companyId)
    {
        string constr = ConfigurationManager.AppSettings["Connection_String"].ToString();
        List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@typ_bill",billNumberData),
                new SqlParameter("@sz_company_id",companyId)
            };
        try
        {
            DataSet ds = SqlHelper.ExecuteDataset(constr, CommandType.StoredProcedure, "SP_GET_EOR_RECEIVED_DESCRIPTION", parameters.ToArray());
            return ds;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}