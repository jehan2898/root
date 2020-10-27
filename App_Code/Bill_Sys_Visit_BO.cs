using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

public class Bill_Sys_Visit_BO
{
    private string strConn;

    private SqlConnection sqlCon;

    private SqlCommand sqlCmd;

    private SqlDataAdapter sqlda;

    private DataSet ds;

    public Bill_Sys_Visit_BO()
    {
        this.strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public bool Checkvisitexists(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        bool flag = false;
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_CHECK_VISIT_EXISTS_New", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DT_EVENT_DATE", objAL[3].ToString());
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
                if (this.ds.Tables[0].Rows.Count > 0)
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

    public int DeleteEvent(ArrayList objAL, string sz_companyid, string sz_userid)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        int num = 0;
        try
        {
            try
            {
                this.sqlCon.Open();
                for (int i = 0; i < objAL.Count; i++)
                {
                    this.sqlCmd = new SqlCommand("SP_DELETE_VISIT", this.sqlCon)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    this.sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_userid);
                    this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyid);
                    this.sqlCmd.Parameters.AddWithValue("@SZ_EVENT_ID", objAL[i].ToString());
                    num = this.sqlCmd.ExecuteNonQuery() + num;
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
        return num;
    }

    public DataTable FillProcedureCodeGrid(string sz_ProcedureCodeID, string sz_CompanyID)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        DataTable dataTable = new DataTable();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_COMPLETED_VISIT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE_ID", sz_ProcedureCodeID);
                this.sqlCmd.Parameters.AddWithValue("@Flag", "LoadProceduresCodes");
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(dataTable);
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
        return dataTable;
    }

    public DataSet GetBillDoctorList(string sz_CompanyID, string szBillNumber, string sz_flag)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_COMPLETED_VISIT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
                this.sqlCmd.Parameters.AddWithValue("@sz_Bill_No", szBillNumber);
                this.sqlCmd.Parameters.AddWithValue("@Flag", sz_flag);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
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
        return this.ds;
    }

    public DataSet GetBillVisits(string sz_CompanyID, string szBillNumber)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_VISIT_INFORMATION_FOR_BILL", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szBillNumber);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
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
        return this.ds;
    }

    public DataSet GetBlockInsurance_Company(string sz_CompanyID)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("GET_BLOCK_BILLING_INSURANCE_ID", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
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
        return this.ds;
    }

    public DataSet GetCompletedVisitList(string sz_CaseID, string sz_CompanyID, string sz_flag)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_COMPLETED_VISIT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_CaseID);
                this.sqlCmd.Parameters.AddWithValue("@Flag", sz_flag);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
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
        return this.ds;
    }

    public DataSet GetCompletedVisitListAllVisit(string sz_CaseID, string sz_CompanyID, string sz_DoctorId)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_COMPLETED_VISIT_ALL_VISIT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_CaseID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_DoctorId);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
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
        return this.ds;
    }

    public string GetDoctorID(string sz_Bill_No, string sz_CompanyId)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        string str = "";
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_COMPLETED_VISIT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);
                this.sqlCmd.Parameters.AddWithValue("@sz_Bill_No", sz_Bill_No);
                this.sqlCmd.Parameters.AddWithValue("@Flag", "GetDoctorID");
                SqlDataReader sqlDataReader = this.sqlCmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    str = sqlDataReader[0].ToString();
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
        return str;
    }

    public DataSet getFollowUpVisitReport(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_FOLLOWUP_VISIT_LIST_REPORT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[1].ToString());
                }
                if (objAL[2].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_FROM_EVENT_DATE", objAL[2].ToString());
                }
                if (objAL[3].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_TO_EVENT_DATE", objAL[3].ToString());
                }
                if (objAL[4].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@UN_BILLED", objAL[4].ToString());
                }
                if (objAL[5].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[5].ToString());
                }
                if (objAL[6].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[6].ToString());
                }
                if (objAL[7].ToString() != "" && objAL[7].ToString() != "NA")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[7].ToString());
                }
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
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

    public DataSet getInitialVisitReport(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_INITIL_FOLLOWUP_VISIT_LIST_REPORT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 1200000
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[1].ToString());
                }
                if (objAL[2].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_FROM_EVENT_DATE", objAL[2].ToString());
                }
                if (objAL[3].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_TO_EVENT_DATE", objAL[3].ToString());
                }
                if (objAL[4].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[4].ToString());
                }
                if (objAL[5].ToString() != "" && objAL[5].ToString() != "NA")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[5].ToString());
                }
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
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

    public DataTable GetProcedureCodeList(ArrayList arrobj)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        DataTable dataTable = new DataTable();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_PROCEDURECODES_EVENTID", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@I_Event_Id", arrobj[3].ToString());
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(dataTable);
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
        return dataTable;
    }

    public DataSet getVisitReport(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_VISIT_LIST_REPORT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[1].ToString());
                }
                if (objAL[2].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_FROM_EVENT_DATE", objAL[2].ToString());
                }
                if (objAL[3].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_TO_EVENT_DATE", objAL[3].ToString());
                }
                if (objAL[4].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@UN_BILLED", objAL[4].ToString());
                }
                if (objAL[5].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[5].ToString());
                }
                if (objAL[6].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[6].ToString());
                }
                if (objAL[7].ToString() != "" && objAL[7].ToString() != "NA")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[7].ToString());
                }
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
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

    public DataSet GetVisitTypeList(string sz_CompanyID, string sz_flag)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_COMPLETED_VISIT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
                this.sqlCmd.Parameters.AddWithValue("@Flag", sz_flag);
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
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
        return this.ds;
    }

    public Bill_Sys_Bulk_Visits InsertBulkVisit(ArrayList p_arrCaseNo, ArrayList p_arrProcedureCode)
    {
        ArrayList arrayLists = new ArrayList();
        ArrayList arrayLists1 = new ArrayList();
        Bill_Sys_Bulk_Visits billSysBulkVisit = null;
        this.sqlCon = new SqlConnection(this.strConn);
        this.sqlCon.Open();
        for (int i = 0; i < p_arrCaseNo.Count; i++)
        {
            SqlParameter sqlParameter = new SqlParameter();
            try
            {
                Bill_Sys_AddVisit_DAO billSysAddVisitDAO = new Bill_Sys_AddVisit_DAO();
                billSysAddVisitDAO = (Bill_Sys_AddVisit_DAO)p_arrCaseNo[i];
                this.sqlCmd = new SqlCommand("SP_INSERT_BULK_TXN_CALENDAR_EVENT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", billSysAddVisitDAO.CaseNo);
                this.sqlCmd.Parameters.AddWithValue("@DT_EVENT_DATE", billSysAddVisitDAO.EventDate);
                this.sqlCmd.Parameters.AddWithValue("@DT_EVENT_TIME", billSysAddVisitDAO.EventTime);
                this.sqlCmd.Parameters.AddWithValue("@SZ_EVENT_NOTES", billSysAddVisitDAO.EventNote);
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", billSysAddVisitDAO.CompanyId);
                this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", billSysAddVisitDAO.DoctorID);
                this.sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", billSysAddVisitDAO.TypeCodeId);
                this.sqlCmd.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", billSysAddVisitDAO.EventTimeType);
                this.sqlCmd.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", billSysAddVisitDAO.EventEndTimeType);
                this.sqlCmd.Parameters.AddWithValue("@DT_EVENT_END_TIME", billSysAddVisitDAO.EventEndTime);
                this.sqlCmd.Parameters.AddWithValue("@I_STATUS", billSysAddVisitDAO.IStatus);
                this.sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TYPE", billSysAddVisitDAO.VisitTypeId);
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                SqlParameter sqlParameter1 = new SqlParameter("@SZ_SUCCESS_ID", SqlDbType.NVarChar, 20)
                {
                    Direction = ParameterDirection.Output
                };
                this.sqlCmd.Parameters.Add(sqlParameter1);
                SqlParameter sqlParameter2 = new SqlParameter("@SZ_EVENT_ID", SqlDbType.NVarChar, 20)
                {
                    Direction = ParameterDirection.Output
                };
                this.sqlCmd.Parameters.Add(sqlParameter2);
                this.sqlCmd.ExecuteNonQuery();
                int num = 0;
                try
                {
                    num = Convert.ToInt32(sqlParameter2.Value);
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    num = 0;
                }
                string str = "";
                if (num != 0)
                {
                    str = sqlParameter1.Value.ToString();
                }
                int count = p_arrProcedureCode.Count;
                if (p_arrProcedureCode.Count != 0 && num != 0 && str != null && !str.Equals(""))
                {
                    Bill_Sys_AddVisit_DAO item = null;
                    for (int j = 0; j < p_arrProcedureCode.Count; j++)
                    {
                        SqlCommand sqlCommand = new SqlCommand("SP_SAVE_REFERRAL_PROC_CODE", this.sqlCon)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        item = new Bill_Sys_AddVisit_DAO();
                        item = (Bill_Sys_AddVisit_DAO)p_arrProcedureCode[j];
                        sqlCommand.Parameters.AddWithValue("@SZ_PROC_CODE", item.ProcedureCode);
                        sqlCommand.Parameters.AddWithValue("@I_EVENT_ID", num.ToString());
                        sqlCommand.Parameters.AddWithValue("@I_STATUS", "2");
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                if (str == "")
                {
                    arrayLists.Add(billSysAddVisitDAO.CaseNo);
                }
                else if (str != "")
                {
                    arrayLists1.Add(billSysAddVisitDAO.CaseNo);
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        if (this.sqlCon.State == ConnectionState.Open)
        {
            this.sqlCon.Close();
        }
        billSysBulkVisit = new Bill_Sys_Bulk_Visits()
        {
            InValidList = arrayLists,
            ValidList = arrayLists1
        };
        return billSysBulkVisit;
    }

    public int InsertVisit(ArrayList p_arrArrayList)
    {
        int num = 0;
        this.sqlCon = new SqlConnection(this.strConn);
        SqlParameter sqlParameter = new SqlParameter();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_INSERT_TXN_CALENDAR_EVENT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlParameter = this.sqlCmd.Parameters.Add("@Return", SqlDbType.Int);
                sqlParameter.Direction = ParameterDirection.ReturnValue;
                this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", p_arrArrayList[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DT_EVENT_DATE", p_arrArrayList[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DT_EVENT_TIME", p_arrArrayList[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_EVENT_NOTES", p_arrArrayList[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_arrArrayList[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_arrArrayList[5].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", p_arrArrayList[6].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DT_EVENT_TIME_TYPE", p_arrArrayList[7].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DT_EVENT_END_TIME_TYPE", p_arrArrayList[8].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DT_EVENT_END_TIME", p_arrArrayList[9].ToString());
                this.sqlCmd.Parameters.AddWithValue("@I_STATUS", p_arrArrayList[10].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TYPE", p_arrArrayList[11].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                this.sqlCmd.ExecuteNonQuery();
                num = Convert.ToInt32(this.sqlCmd.Parameters["@Return"].Value);
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
        return num;
    }

    public DataSet Last30DaysUnBilledVisitReport(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_LAST_30DAYS_UNBILLED_VISIT_LIST_REPORT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[1].ToString());
                }
                if (objAL[2].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_FROM_EVENT_DATE", objAL[2].ToString());
                }
                if (objAL[3].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_TO_EVENT_DATE", objAL[3].ToString());
                }
                if (objAL[4].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@UN_BILLED", objAL[4].ToString());
                }
                if (objAL[5].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[5].ToString());
                }
                if (objAL[6].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[6].ToString());
                }
                if (objAL[7].ToString() != "" && objAL[7].ToString() != "NA")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[7].ToString());
                }
                if (objAL[8].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", objAL[8].ToString());
                }
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
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

    public void saveProc(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_MST_PROCEDURE_CODES", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_CODE_DESCRIPTION", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FLT_AMOUNT", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[5].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
                this.sqlCmd.ExecuteNonQuery();
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
    }

    public void saveVisit(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_MST_BILL_PROC_TYPE", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_TYPE_CODE", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_TYPE_DESCRIPTION", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_TYPE_ID", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[4].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "ADD");
                this.sqlCmd.ExecuteNonQuery();
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
    }

    public DataSet UnBilledVisitReport(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_UNBILLED_VISIT_LIST_REPORT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[1].ToString());
                }
                if (objAL[2].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_FROM_EVENT_DATE", objAL[2].ToString());
                }
                if (objAL[3].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_TO_EVENT_DATE", objAL[3].ToString());
                }
                if (objAL[4].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@UN_BILLED", objAL[4].ToString());
                }
                if (objAL[5].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[5].ToString());
                }
                if (objAL[6].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[6].ToString());
                }
                if (objAL[7].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SORT_EXPRESSION", objAL[7].ToString());
                }
                if (objAL[8].ToString() != "NA" && objAL[8].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[8].ToString());
                }
                this.sqlCmd.Parameters.AddWithValue("@sz_groupby", objAL[9].ToString());
                if (objAL[10].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", objAL[10].ToString());
                }
                if (objAL.Count == 12)
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[11].ToString());
                }
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
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

    public void UpdateCalenderEvent(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_UPDATE_TXN_CALENDER_EVENT_VISIT_TYPE", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", objAL[0].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
                this.sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TYPE", objAL[2].ToString());
                this.sqlCmd.Parameters.AddWithValue("@I_STATUS", objAL[3].ToString());
                this.sqlCmd.Parameters.AddWithValue("@FLAG", "UPDATE");
                this.sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException sqlException)
            {
                sqlException.Message.ToString();
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

    public int UpdateEvent(string sz_companyid, string sz_doctorid, ArrayList arr_event_id)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.sqlCon.Open();
        int num = 0;
        try
        {
            try
            {
                for (int i = 0; i < arr_event_id.Count; i++)
                {
                    this.sqlCmd = new SqlCommand("SP_UPDATE_EVENT_DOCTOR", this.sqlCon)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyid);
                    this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_doctorid);
                    this.sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", arr_event_id[i].ToString());
                    num = this.sqlCmd.ExecuteNonQuery() + num;
                }
            }
            catch (SqlException sqlException)
            {
                sqlException.Message.ToString();
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

    public DataSet VisitReport(ArrayList objAL)
    {
        this.sqlCon = new SqlConnection(this.strConn);
        this.ds = new DataSet();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_VISIT_LIST", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
                if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[1].ToString());
                }
                if (objAL[2].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_FROM_EVENT_DATE", objAL[2].ToString());
                }
                if (objAL[3].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@DT_TO_EVENT_DATE", objAL[3].ToString());
                }
                if (objAL[4].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@UN_BILLED", objAL[4].ToString());
                }
                if (objAL[5].ToString() != "")
                {
                    this.sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[5].ToString());
                }
                this.sqlda = new SqlDataAdapter(this.sqlCmd);
                this.sqlda.Fill(this.ds);
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
}