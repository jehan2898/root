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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class Bill_Sys_ReportBO
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    string phone;
    string fax;
    public Bill_Sys_ReportBO()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public DataSet GET_INSURANCE_CARRIER_BILLS(string insuranceCarrierID, int billStatus, string fromDate, string toDate, string companyId)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_INSURANCE_CARRIER_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", insuranceCarrierID);
            sqlCmd.Parameters.AddWithValue("@BILL_STATUS", billStatus);
            sqlCmd.Parameters.AddWithValue("@FROM_DATE", fromDate);
            sqlCmd.Parameters.AddWithValue("@TO_DATE", toDate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
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

    public DataSet GET_DOCTOR_BILLS(string doctorID, int billStatus, string fromDate, string toDate, string companyId)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_DOCTOR_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", doctorID);
            sqlCmd.Parameters.AddWithValue("@BILL_STATUS", billStatus);
            sqlCmd.Parameters.AddWithValue("@FROM_DATE", fromDate);
            sqlCmd.Parameters.AddWithValue("@TO_DATE", toDate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
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

    public DataSet GET_Provider_BILLS(string providerID, int billStatus, string fromDate, string toDate, string companyId)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_PROVIDER_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PROVIDER_ID", providerID);
            sqlCmd.Parameters.AddWithValue("@BILL_STATUS", billStatus);
            sqlCmd.Parameters.AddWithValue("@FROM_DATE", fromDate);
            sqlCmd.Parameters.AddWithValue("@TO_DATE", toDate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
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

    public DataSet GetAllReports(string procname, string fromdate, string todate, string companyid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(procname, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_START_DATE", fromdate);
            sqlCmd.Parameters.AddWithValue("@SZ_END_DATE", todate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);


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
    public DataSet GetAllReportsCase(string procname, string fromdate, string todate, string companyid, string casetypeid)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SET DATEFORMAT 'ymd'", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd = new SqlCommand(procname, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_START_DATE", fromdate);
            sqlCmd.Parameters.AddWithValue("@SZ_END_DATE", todate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            if (casetypeid.ToString() != "" && casetypeid.ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", casetypeid); }


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

    //OverLoad Method GetAllReports to send Location Id
    public DataSet GetAllReports(string procname, string fromdate, string todate, string companyid, string locationId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(procname, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_START_DATE", fromdate);
            sqlCmd.Parameters.AddWithValue("@SZ_END_DATE", todate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            if (locationId.ToString() != "" && locationId.ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", locationId); }

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
    //end of method overload
    public DataSet GetProcedureReports(string procname, string fromdate, string todate, string szOfficeID, string szStatus, string companyid, string szFlag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(procname, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@DT_START_DATE", fromdate);
            sqlCmd.Parameters.AddWithValue("@DT_END_DATE", todate);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", szOfficeID);
            sqlCmd.Parameters.AddWithValue("@I_STATUS", szStatus);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            if (szFlag != "")
                sqlCmd.Parameters.AddWithValue("@I_REPORT_RECEIVED", szFlag);
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

    //Overloaded Function GetProcedureReports to pass parameter Location_id
    public DataSet GetProcedureReports(string procname, string fromdate, string todate, string szOfficeID, string szStatus, string companyid, string szFlag, string Location_id)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(procname, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@DT_START_DATE", fromdate);
            sqlCmd.Parameters.AddWithValue("@DT_END_DATE", todate);
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", szOfficeID);
            sqlCmd.Parameters.AddWithValue("@I_STATUS", szStatus);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            if (szFlag != "")
                sqlCmd.Parameters.AddWithValue("@I_REPORT_RECEIVED", szFlag);

            if (Location_id != "" && Location_id != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", Location_id); }

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

    public DataSet GetTransportSearchReport(string szCmpId, string strFromDate, string strTodate, string strStartTime, string strStartTimeType, string strEndTime, string strEndTimeType, string strTransportName)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            //no condition search
            if ((strFromDate == "" || strTodate == "") && (strStartTime == "00.00" || strEndTime == "00.00") && strTransportName == "--Selecte--")
            {
                sqlCmd = new SqlCommand("SP_TRANSPORT_REPORT", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            //search from date, to date
            else if ((strFromDate != "" && strTodate != "") && (strStartTime == "00.00" || strEndTime == "00.00") && strTransportName == "--Selecte--")
            {
                sqlCmd = new SqlCommand("SP_SEARCH_DATE_RANGE_TRANSPORT_REPORT", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            //search from time to time
            else if ((strFromDate == "" || strTodate == "") && strStartTime != "00.00" && strEndTime != "00.00" && strTransportName == "--Selecte--")
            {
                sqlCmd = new SqlCommand("SP_SEARCH_TIME_RANGE_TRANSPORT_REPORT", sqlCon);
            }//for transport name
            else if ((strFromDate == "" || strTodate == "") && (strStartTime == "00.00" || strEndTime == "00.00") && strTransportName != "--Selecte--")
            {
                sqlCmd = new SqlCommand("SP_SEARCH_BY_TRANSPORT_NAME", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if (strFromDate != "" && strTodate != "" && strStartTime == "00.00" && strEndTime == "00.00" && strTransportName != "--Selecte--")
            {
                sqlCmd = new SqlCommand("SP_SEARCH_DATE_RANGE_AND_TRANSPORT_NAME", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate == "" || strTodate == "") && strStartTime != "00.00" && strEndTime != "00.00" && strTransportName != "--Selecte--")
            {
                sqlCmd = new SqlCommand("SP_SEARCH_TIME_RANGE_AND_TRANSPORT_NAME", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if (strFromDate != "" && strTodate != "" && strStartTime != "00.00" && strEndTime != "00.00" && strTransportName == "--Selecte--")
            {
                sqlCmd = new SqlCommand("SP_SEARCH_DATE_AND_TIME", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }



            //search from date ,to date , from condition to condition
            else if (strFromDate != "" && strTodate != "" && strStartTime != "00.00" && strEndTime != "00.00" && strTransportName != "--Selecte--")
            {
                sqlCmd = new SqlCommand("SP_SEARCH_TRANSPORT_REPORT", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }

            //adding paramerte to stpre procedure
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID ", szCmpId);
            if (strFromDate != "" && strTodate != "") sqlCmd.Parameters.AddWithValue("@SZ_FROM_DATE", strFromDate);
            if (strTodate != "" && strFromDate != "") sqlCmd.Parameters.AddWithValue("@SZ_TO_DATE", strTodate);
            decimal dST = Convert.ToDecimal(strStartTime);
            if (strStartTime != "00.00" && strEndTime != "00.00") sqlCmd.Parameters.AddWithValue("@SZ_EVENT_TIME", dST);
            if (strStartTime != "00.00" && strEndTime != "00.00") sqlCmd.Parameters.AddWithValue("@SZ_EVENT_TIME_TYPE", strStartTimeType);
            decimal dET = Convert.ToDecimal(strEndTime);
            if (strStartTime != "00.00" && strEndTime != "00.00") sqlCmd.Parameters.AddWithValue("@SZ_EVENT_END_TIME", dET);
            if (strStartTime != "00.00" && strEndTime != "00.00") sqlCmd.Parameters.AddWithValue("@SZ_EVENT_TIME_END_TYPE", strEndTimeType);
            if (strTransportName != "--Selecte--") sqlCmd.Parameters.AddWithValue("@SZ_TARNSPOTATION_COMPANY_NAME", strTransportName);
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


    public DataSet GetMissingSpecialityReport(string p_szSpecialityID, string p_szCompanyID, string p_szSpecialityID1)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MISSING_SPECIALITY_PATIENT_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY_ID", p_szSpecialityID);
            if (!p_szSpecialityID1.ToString().Equals(""))
            {
                sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY_ID1", p_szSpecialityID1);
            }
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);

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

    // Overloaded function GetMissingSpecialityReport to send parameter LocationId
    public DataSet GetMissingSpecialityReport(string p_szSpecialityID, string p_szCompanyID, string LocationId, string p_szSpecialityID1)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MISSING_SPECIALITY_PATIENT_LIST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY_ID", p_szSpecialityID);
            if (!p_szSpecialityID1.ToString().Equals(""))
            {
                sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY_ID1", p_szSpecialityID1);
            }
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            if (LocationId.ToString() != "" && LocationId.ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", LocationId); }

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
    //end Overloading function


    public DataSet getBillReport(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[7].ToString()); }
            //sqlCmd.Parameters.AddWithValue("@bt_has_verification_sent", objAL[8].ToString());
            //sqlCmd.Parameters.AddWithValue("@bt_has_verification_received", objAL[9].ToString());
            //sqlCmd.Parameters.AddWithValue("@bt_has_denial", objAL[10].ToString());
            //sqlCmd.Parameters.AddWithValue("@bt_has_payment", objAL[11].ToString());
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



    public DataSet getBillReportSelect(ArrayList objAL, string Proceure)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(Proceure, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[7].ToString()); }
            sqlCmd.Parameters.AddWithValue("@BT_STATUS", objAL[8].ToString());

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

    public DataSet getBillReportThree(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SELECT_THREE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[7].ToString()); }
            sqlCmd.Parameters.AddWithValue("@BT_STATUS", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@BT_STATUS1", objAL[9].ToString());
            sqlCmd.Parameters.AddWithValue("@BT_STATUS2", objAL[10].ToString());

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

    public DataSet getBillReportTwo(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SELECT_TWO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[7].ToString()); }
            sqlCmd.Parameters.AddWithValue("@BT_STATUS", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@BT_STATUS1", objAL[9].ToString());


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

    public DataSet getBillReportDetails(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_SORTING_ORDER", objAL[3].ToString());
            if (objAL[4].ToString() != "" && objAL[3].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[4].ToString()); }
            if (objAL[5].ToString() != "" && objAL[5].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objAL[5].ToString()); }
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



    public DataSet getBillReportSpeciality(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", objAL[7].ToString()); } // Kapil
            //Kiran :: 29 Aug
            if (objAL[8].ToString() != "NA" && objAL[8].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", objAL[8].ToString()); } // Kapil
            if (objAL[9].ToString() != "NA" && objAL[9].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", objAL[9].ToString()); } // Kapil
            if (objAL[10].ToString() != "NA" && objAL[10].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", objAL[10].ToString()); } // Kapil
            //Nirmalkumar 14 nov
            if (objAL[11].ToString() != "NA" && objAL[11].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_VISIT_DATE", objAL[11].ToString()); }
            if (objAL[12].ToString() != "NA" && objAL[12].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_VISIT_DATE", objAL[12].ToString()); }
            if (objAL[13].ToString() != "NA" && objAL[13].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", objAL[13].ToString()); }
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


    public DataSet getBillReportSpecialitySelect(ArrayList objAL, string proc)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(proc, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "")
            {
                sqlCmd.Parameters.AddWithValue("@BT_STATUS", objAL[7].ToString());
            }
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


    public DataSet getBillReportSpecialityTwo(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_TWO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "")
            {
                sqlCmd.Parameters.AddWithValue("@BT_STATUS", objAL[7].ToString());
            }
            if (objAL[7].ToString() != "NA" && objAL[8].ToString() != "")
            {
                sqlCmd.Parameters.AddWithValue("@BT_STATUS1", objAL[8].ToString());
            }
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
    public DataSet getBillReportSpecialityThree(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_THREE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "")
            {
                sqlCmd.Parameters.AddWithValue("@BT_STATUS", objAL[7].ToString());
            }
            if (objAL[7].ToString() != "NA" && objAL[8].ToString() != "")
            {
                sqlCmd.Parameters.AddWithValue("@BT_STATUS1", objAL[8].ToString());
            }
            if (objAL[7].ToString() != "NA" && objAL[9].ToString() != "")
            {
                sqlCmd.Parameters.AddWithValue("@BT_STATUS2", objAL[9].ToString());
            }
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



    public DataSet getBillReportSpecialityDetails(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_SORTING_ORDER", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[3].ToString());
            if (objAL.Count > 4)
            {
                if (objAL[4].ToString() != "" && objAL[4].ToString() != "NA")
                {
                    sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[4].ToString());
                }
            }
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

    public DataSet getBillStatusReportDetails(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_SORTING_ORDER", objAL[3].ToString());
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



    public void updateBillStatus(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_BILL_STATUS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID_LIST", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_BILL_STATUS_DATE", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[4].ToString());
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
    //public void Revert_Billstatus(ArrayList objAL)
    //{
    //    sqlCon = new SqlConnection(strsqlCon);
    //    try
    //    {
    //        sqlCon.Open();
    //        sqlCmd = new SqlCommand("SP_REVERT_BILL", sqlCon);
    //        sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
    //        sqlCmd.CommandTimeout = 0;
    //        sqlCmd.CommandType = CommandType.StoredProcedure;
    //        sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", objAL[0].ToString());
    //        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
    //        sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[2].ToString());
    //        sqlCmd.Parameters.AddWithValue("@DT_BILL_STATUS_DATE", objAL[3].ToString());
    //        sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID_LIST", objAL[4].ToString());
    //        sqlCmd.ExecuteNonQuery();

    //    }
    //    catch (Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //    }
    //    finally
    //    {
    //        if (sqlCon.State == ConnectionState.Open)
    //        {
    //            sqlCon.Close();
    //        }
    //    }

    //}
    public void revertBillStatus(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_REVERT_BILL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
           
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID_LIST", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());


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
    public DataSet getAssociatedDiagnosisCode(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_CASE_DIAGNOSISCODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[3].ToString());
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


    public String getTop1Speciality(String p_szCompanyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_PROCEDURE_GROUP", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_TOP_1");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            SqlDataReader objDR = sqlCmd.ExecuteReader();
            while (objDR.Read())
            {
                return objDR["Code"].ToString();
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
        return "NA";
    }

    public DataSet GetBillDetails(string companyid, string DoctorID, string Speciality, string CaseNO, string BillNO)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_TXN_BILL_NO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", CaseNO);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", BillNO);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", Speciality);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", DoctorID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
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

    //overload function tp pass parameter Location id
    public DataSet GetBillDetails(string companyid, string DoctorID, string Speciality, string CaseNO, string BillNO, string LocationId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_TXN_BILL_NO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", CaseNO);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID", BillNO);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", Speciality);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", DoctorID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyid);
            if (LocationId.ToString() != "" && LocationId.ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", LocationId); }
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
    //end of overload 

    //public DataSet GetPaymentDetails(ArrayList arrPayment)
    //{
    //    sqlCon = new SqlConnection(strsqlCon);
    //    ds = new DataSet();
    //    try
    //    {
    //        sqlCon.Open();
    //        sqlCmd = new SqlCommand("SP_GET_PAYMENT_REPORT", sqlCon);
    //        sqlCmd.CommandType = CommandType.StoredProcedure;
    //        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrPayment[0].ToString());
    //        sqlCmd.Parameters.AddWithValue("@FROMDATE", arrPayment[1].ToString());
    //        sqlCmd.Parameters.AddWithValue("@TODATE", arrPayment[2].ToString());
    //        sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", arrPayment[3].ToString());
    //        sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", arrPayment[4].ToString());
    //        if (arrPayment[5].ToString() != "" && arrPayment[5].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", arrPayment[5].ToString()); }
    //        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
    //        da.Fill(ds);

    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Message.ToString();

    //    }
    //    finally
    //    {
    //        if (sqlCon.State == ConnectionState.Open)
    //        {
    //            sqlCon.Close();
    //        }
    //    }
    //    return ds;
    //}

    public DataSet GetPaymentDetails(ArrayList arrPayment)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PAYMENT_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrPayment[0].ToString());
            sqlCmd.Parameters.AddWithValue("@FROMDATE", arrPayment[1].ToString());
            sqlCmd.Parameters.AddWithValue("@TODATE", arrPayment[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", arrPayment[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", arrPayment[4].ToString());
            if (arrPayment[5].ToString() != "" && arrPayment[5].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", arrPayment[5].ToString()); }
            sqlCmd.Parameters.AddWithValue("@CHECKFROMDATE", arrPayment[6].ToString());
            sqlCmd.Parameters.AddWithValue("@CHECKTODATE", arrPayment[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHECKNUMBER", arrPayment[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHECKAMOUNT", arrPayment[9].ToString());
            if (arrPayment[10].ToString() != "" && arrPayment[10].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", arrPayment[10].ToString());
            if (arrPayment[11].ToString() != "" && arrPayment[11].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY_NAME", arrPayment[11].ToString());
            sqlCmd.Parameters.AddWithValue("@PAYFROMDATE", arrPayment[12].ToString());
            sqlCmd.Parameters.AddWithValue("@PAYTODATE", arrPayment[13].ToString());
            if (arrPayment[14].ToString() != "" && arrPayment[14].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", arrPayment[14].ToString());
            if (arrPayment[15].ToString() != "" && arrPayment[15].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_COLLECTIONS", arrPayment[15].ToString());
            if (arrPayment.Count > 16)
            {
                sqlCmd.Parameters.AddWithValue("@SZ_VISIT_FROMDATE", arrPayment[16].ToString());
            }
            if (arrPayment.Count > 17)
            {
                sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TODATE", arrPayment[17].ToString());
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);

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

    #region "Get Bill Report for provider"

    public DataSet GetBillReportProvider(ArrayList arr)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_PROVIDER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", arr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", arr[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE", arr[3].ToString());
            if (arr[4].ToString() != "" && arr[4].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", arr[4].ToString()); }

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();

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

    public DataSet GetBillReportForProvider(ArrayList arr)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_bill_report_for_provider", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", arr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", arr[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE", arr[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", arr[4].ToString());
            if (arr[5].ToString() != "" && arr[5].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", arr[5].ToString()); }

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();

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

    #endregion

    public DataSet GetDetailBillReport(ArrayList arrPayment)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DETAIL_BILL_REPORT_PROVIDER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrPayment[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", arrPayment[1].ToString());
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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



    #region "Get Referral Schedule Report"

    public DataSet Get_Referral_Schedule_Report(ArrayList arr)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_REFFERAL_SCHEDULE_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", arr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", arr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", arr[2].ToString());
            sqlCmd.Parameters.AddWithValue("@I_STATUS", arr[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arr[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", arr[5].ToString());
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
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

    #endregion


    #region "Get Missing Procedure Report"

    public DataSet Get_Missing_Procedure_Report(ArrayList arr)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MISSING_PROCEDURE_CODES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            //sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", arr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE_ID", arr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", arr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arr[2].ToString());
            if (arr.Count > 3)
            {
                if (arr[3].ToString() != "" && arr[3].ToString() != "NA")
                {
                    sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", arr[3].ToString());
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
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

    #endregion

    //public DataSet GetPaymentDetailsMRI(ArrayList arrPayment)
    //{
    //    sqlCon = new SqlConnection(strsqlCon);
    //    ds = new DataSet();
    //    try
    //    {
    //        sqlCon.Open();
    //        sqlCmd = new SqlCommand("SP_GET_PAYMENT_REPORT_MRI", sqlCon);
    //        sqlCmd.CommandType = CommandType.StoredProcedure;
    //        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrPayment[0].ToString());
    //        sqlCmd.Parameters.AddWithValue("@FROMDATE", arrPayment[1].ToString());
    //        sqlCmd.Parameters.AddWithValue("@TODATE", arrPayment[2].ToString());
    //        sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", arrPayment[3].ToString());
    //        sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", arrPayment[4].ToString());
    //        if (arrPayment[5].ToString() != "" && arrPayment[5].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", arrPayment[5].ToString()); }

    //        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
    //        da.Fill(ds);

    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Message.ToString();

    //    }
    //    finally
    //    {
    //        if (sqlCon.State == ConnectionState.Open)
    //        {
    //            sqlCon.Close();
    //        }
    //    }
    //    return ds;
    //}


    public DataSet GetPaymentDetailsMRI(ArrayList arrPayment)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PAYMENT_REPORT_MRI", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrPayment[0].ToString());
            sqlCmd.Parameters.AddWithValue("@FROMDATE", arrPayment[1].ToString());
            sqlCmd.Parameters.AddWithValue("@TODATE", arrPayment[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", arrPayment[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", arrPayment[4].ToString());
            if (arrPayment[5].ToString() != "" && arrPayment[5].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", arrPayment[5].ToString()); }
            sqlCmd.Parameters.AddWithValue("@CHECKFROMDATE", arrPayment[6].ToString());
            sqlCmd.Parameters.AddWithValue("@CHECKTODATE", arrPayment[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHECKNUMBER", arrPayment[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHECKAMOUNT", arrPayment[9].ToString());
            if (arrPayment[10].ToString() != "" && arrPayment[10].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", arrPayment[10].ToString());
            if (arrPayment[11].ToString() != "" && arrPayment[11].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY_NAME", arrPayment[11].ToString());
            sqlCmd.Parameters.AddWithValue("@PAYFROMDATE", arrPayment[12].ToString());
            sqlCmd.Parameters.AddWithValue("@PAYTODATE", arrPayment[13].ToString());
            if (arrPayment[14].ToString() != "" && arrPayment[14].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", arrPayment[14].ToString());
            if (arrPayment[15].ToString() != "" && arrPayment[15].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_COLLECTIONS", arrPayment[15].ToString());
            if (arrPayment.Count > 16)
            {
                sqlCmd.Parameters.AddWithValue("@SZ_VISIT_FROMDATE", arrPayment[16].ToString());
            }
            if (arrPayment.Count > 17)
            {
                sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TODATE", arrPayment[17].ToString());
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);

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



    public DataSet GetPatientList(string sz_CompanyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_USERS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILLING_COMPANY", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETUSERLIST");
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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

    public DataSet GetReportByReport(string sz_companyid, string sz_specialtyID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DETAIL_PAYMENT_REPORT_BY_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyid);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", sz_specialtyID);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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

    public DataSet GetReportByReport(string sz_companyid, string sz_specialtyID, string Location_Id)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DETAIL_PAYMENT_REPORT_BY_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyid);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", sz_specialtyID);
            if (Location_Id != "" && Location_Id != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", Location_Id); }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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


    public void setLitigate(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_STATUS_TO_LITIGATE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", Convert.ToInt32(objAL[2]));
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

    public DataSet GetReportbyVerification(ArrayList arl)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_VERIFICATION_STATUS_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arl[0].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", arl[1].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", arl[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", arl[3].ToString());
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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


    #region "Get Payment Report for Speciality"

    public DataSet GetPaymentReportBySpeciality(ArrayList arr)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PAYMENT_REPORT_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", arr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", arr[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", arr[3].ToString());
            if (arr[4].ToString() != "" && arr[4].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", arr[4].ToString()); }
            //added on 24/12/2014
            //if (arr[5].ToString() != "" && arr[5].ToString() != "NA")
            //{
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", arr[5].ToString());
            //}
            //added on 24/12/2014

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();

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

    #endregion



    public DataSet GetPatientVisitSummary(string ProcedureName, string Company_ID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(ProcedureName, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", Company_ID);

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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


    public DataSet GetOfficeName(string szCmpId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {

            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_OFFICE_NAME", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCmpId);


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


    public DataSet GetReceived_Report(string szCmpId, string strFromDate, string strToDate, string strPatinetName, string strOfficeID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            //show all records 
            if ((strFromDate == "" || strToDate == "") && strPatinetName == "" && strOfficeID == "--select--")
            {
                sqlCmd = new SqlCommand("SP_GET_RECEIVED_REPORTS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }///show records within date range
            else if ((strFromDate != "" && strToDate != "") && strPatinetName == "" && strOfficeID == "--select--")
            {
                sqlCmd = new SqlCommand("SP_GET_RECEIVED_SEARCH_BY_DATE_RANGE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//show records  match with patient name
            else if ((strFromDate == "" || strToDate == "") && strPatinetName != "" && strOfficeID == "--select--")
            {
                sqlCmd = new SqlCommand("SP_GET_RECEIVED_SEARCH_BY_PATIENT_NAME", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//show records  match  office id
            else if ((strFromDate == "" || strToDate == "") && strPatinetName == "" && strOfficeID != "--select--")
            {
                sqlCmd = new SqlCommand("SP_GET_RECEIVED_SEARCH_BY_OFFICEID", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//NAME AND DATE RANGE
            else if ((strFromDate != "" && strToDate != "") && strPatinetName != "" && strOfficeID == "--select--")
            {
                sqlCmd = new SqlCommand("SP_GET_RECEIVED_SEARCH_BY_NAME_AND_DATE_RANGE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//OFFICE AND DATE RANGE
            else if ((strFromDate != "" && strToDate != "") && strPatinetName == "" && strOfficeID != "--select--")
            {
                sqlCmd = new SqlCommand("SP_GET_RECEIVED_BY_OFFICE_AND_DATE_RANGE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//OFFICE AND NAME
            else if ((strFromDate == "" || strToDate == "") && strPatinetName != "" && strOfficeID != "--select--")
            {
                sqlCmd = new SqlCommand("SP_GET_RECEIVED_SEARCH_BY_OFFICE_AND_NAME", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            //show records  match  with patient name and office id and within date range
            else if ((strFromDate != "" && strToDate != "") && strPatinetName != "" && strOfficeID != "--select--")
            {
                sqlCmd = new SqlCommand("SP_GET_RECEIVED_SEARCH_ALL", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }

            sqlCon.Open();
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCmpId);
            if (strFromDate != "" && strToDate != "") sqlCmd.Parameters.AddWithValue("@SZ_STRAT_DATE", strFromDate);
            if (strToDate != "" && strFromDate != "") sqlCmd.Parameters.AddWithValue("@SZ_END_DATE", strToDate);
            if (strPatinetName != "") sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", strPatinetName);
            if (strOfficeID != "--select--") sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", strOfficeID);

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

    public DataSet Get_Received_Search_Report(string szCmpId, string strPname, string strFromDate, string ToDate, string OfficeId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {

            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_RECEIVED_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCmpId);
            if (!strPname.Equals(""))
            {
                sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", strPname);
            }
            if (!strFromDate.Equals("") && !ToDate.Equals(""))
            {
                sqlCmd.Parameters.AddWithValue("@DT_START_DATE", strFromDate);
                sqlCmd.Parameters.AddWithValue("@DT_END_START", ToDate);
            }
            if (!OfficeId.Equals("--select--"))
            {
                sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", OfficeId);
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
    public DataSet GetTransportName(string szCmpId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_TRANSPOTATION_NAME", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCmpId);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

            da.Fill(ds);
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

    // aaded by shailesh 23aprilfunction

    public DataSet GetReffPaidBillReport(string sz_company_id)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_REFF_COMPANY_PAID_BILL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "LIST");
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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
    //Wrote Function To Search With All Case Types :-TUSHAR 19may
    public DataSet GetReffPaidBillReportALL(string sz_company_id, string sz_case_type_id)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_REFF_COMPANY_PAID_BILL_ALL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", sz_case_type_id);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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
    //END Function
    public DataTable GetDoctorID(string sz_company_id)
    {
        sqlCon = new SqlConnection(strsqlCon);
        DataTable dt = new DataTable();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_REFF_COMPANY_PAID_BILL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST");
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

    public void InsertBillTransactionData(ArrayList objArr)
    {
        sqlCon = new SqlConnection(strsqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_TXN_BILL_TRANSACTIONS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objArr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_BILL_DATE", objArr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objArr[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objArr[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_TYPE", objArr[4].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_READING_DOCTOR_ID", objArr[5].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_COMPANY_ID", objArr[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objArr[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROC_GROUP_ID", objArr[8].ToString());
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

    public DataSet GetProcCodeDetails(ArrayList arrObj)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_REFF_COMPANY_PAID_BILL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE", arrObj[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CODE_DESCRIPTION", arrObj[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrObj[2].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETPROCID");
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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

    public string GetTreatmentID(ArrayList objArr)
    {
        sqlCon = new SqlConnection(strsqlCon);
        string treatmentID = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_REFF_COMPANY_PAID_BILL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", objArr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE", objArr[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objArr[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objArr[3].ToString());
            sqlCmd.Parameters.AddWithValue("@FLAG", "GETTREATMENTID");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                treatmentID = dr["TREATMENT_ID"].ToString();
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
        return treatmentID;
    }
    public void RevertReport(int iId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_RECEVED_EVENT_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_EVENT_PROC_ID", iId);
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


    public DataSet GetBillReports(string P_Company_Id, string P_Speciality, string P_Status_Id, string P_User_Id)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_BILL_REPORT_FOR_TEST_FACILITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            if (P_User_Id != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id); }
            if (P_Speciality != "" && P_Speciality != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
            if (P_Status_Id != "" && P_Status_Id != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", P_Status_Id); }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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

    public DataSet GetBillReportsByDate(string P_Company_Id, string P_From_Date, string P_ToDate, string P_Speciality, string P_Status_Id, string P_User_Id)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_BILL_REPORT_FOR_TEST_FACILITY_DATE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            if (P_From_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", P_From_Date); }
            if (P_ToDate != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", P_ToDate); }
            if (P_Speciality != "" && P_Speciality != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
            if (P_Status_Id != "" && P_Status_Id != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", P_Status_Id); }
            if (P_User_Id != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id); }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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


    public DataSet GetBillReportsByDateAndProcedure(string P_Company_Id, string P_From_Date, string P_ToDate, string P_Speciality, string P_Status_Id)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_BILL_REPORT_FOR_TEST_FACILITY_PROCEDURE_GROUP_DATE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            if (P_From_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", P_From_Date); }
            if (P_ToDate != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", P_ToDate); }
            if (P_Speciality != "" && P_Speciality != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
            if (P_Status_Id != "" && P_Status_Id != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", P_Status_Id); }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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


    public DataSet GetBillReportsByProcedure(string P_Company_Id, string P_Speciality, string P_Status_Id)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_BILL_REPORT_FOR_TEST_FACILITY_PROCEDURE_GROUP", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            if (P_Speciality != "" && P_Speciality != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
            if (P_Status_Id != "" && P_Status_Id != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", P_Status_Id); }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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
    #region "Code for searching test unbilled report"
    public DataSet GetUnbilled_Report(string szCmpId, string strFromDate, string strToDate, string strSpeciality, string strCaseType)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            //show all records 
            if ((strFromDate == "" || strToDate == "") && strSpeciality == "---Select---" && strCaseType == "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }///show records within date range //SP_GET_TEST_UNBILLED_REPORTS
            else if ((strFromDate != "" || strToDate != "") && strSpeciality == "---Select---" && strCaseType == "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_DATE_RANGE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            }//show records  match with patient name
            else if ((strFromDate == "" || strToDate == "") && strSpeciality != "---Select---" && strCaseType == "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_SPECIALITY", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate != "" || strToDate != "") && strSpeciality != "---Select---" && strCaseType == "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            }
            else if ((strFromDate == "" || strToDate == "") && strSpeciality == "---Select---" && strCaseType != "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_CASE_TYPE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate != "" && strToDate != "") && strSpeciality == "---Select---" && strCaseType != "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_DATE_RANGE_AND_CASE_TYPE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate == "" || strToDate == "") && strSpeciality != "---Select---" && strCaseType != "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_SPECIALITY_AND_CASE_TYPE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate != "" && strToDate != "") && strSpeciality != "---Select---" && strCaseType != "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }


            sqlCon.Open();
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCmpId);
            if (strFromDate != "" && strToDate != "") sqlCmd.Parameters.AddWithValue("@SZ_STRAT_DATE", strFromDate);
            if (strToDate != "" && strFromDate != "") sqlCmd.Parameters.AddWithValue("@SZ_END_DATE", strToDate);
            if (strSpeciality != "---Select---") sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", strSpeciality);
            if (strCaseType != "--- Select ---") sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_NAME", strCaseType);


            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
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

    public DataSet Get30DaysUnbilled_Report(string szCmpId, string strFromDate, string strToDate, string strSpeciality, string strCaseType)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            //show all records 
            if ((strFromDate == "" || strToDate == "") && strSpeciality == "---Select---" && strCaseType == "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_30DAYS_UNBILLED_REPORTS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }///show records within date range
            else if ((strFromDate != "" || strToDate != "") && strSpeciality == "---Select---" && strCaseType == "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_30DAYS_UNBILLED_REPORTS_BY_DATE_RANGE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            }//show records  match with patient name
            else if ((strFromDate == "" || strToDate == "") && strSpeciality != "---Select---" && strCaseType == "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_30DAYS_UNBILLED_REPORTS_BY_SPECIALITY", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate != "" || strToDate != "") && strSpeciality != "---Select---" && strCaseType == "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_30DAYS_UNBILLED_REPORTS_BY_ALL", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            }
            else if ((strFromDate == "" || strToDate == "") && strSpeciality == "---Select---" && strCaseType != "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_30DAYS_UNBILLED_REPORTS_CASE_TYPE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate != "" && strToDate != "") && strSpeciality == "---Select---" && strCaseType != "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_30DAYS_UNBILLED_REPORTS_BY_DATE_RANGE_CASE_TYPE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate == "" || strToDate == "") && strSpeciality != "---Select---" && strCaseType != "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_30DAYS_UNBILLED_REPORTS_BY_SPECIALITY_AND_CASE_TYPE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate != "" && strToDate != "") && strSpeciality != "---Select---" && strCaseType != "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_30DAYS_UNBILLED_REPORTS_BY_ALL3", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_30DAYS_UNBILLED_REPORTS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }


            sqlCon.Open();
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCmpId);
            if (strFromDate != "" && strToDate != "") sqlCmd.Parameters.AddWithValue("@SZ_STRAT_DATE", strFromDate);
            if (strToDate != "" && strFromDate != "") sqlCmd.Parameters.AddWithValue("@SZ_END_DATE", strToDate);
            if (strSpeciality != "---Select---") sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", strSpeciality);
            if (strCaseType != "--- Select ---") sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_NAME", strCaseType);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
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

    public DataSet Get40DaysUnbilled_Report(string szCmpId, string strFromDate, string strToDate, string strSpeciality, string strCaseType)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            //show all records 
            if ((strFromDate == "" || strToDate == "") && strSpeciality == "---Select---" && strCaseType == "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_40DAYS_UNBILLED_REPORTS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }///show records within date range
            else if ((strFromDate != "" || strToDate != "") && strSpeciality == "---Select---" && strCaseType == "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_40DAYS_UNBILLED_REPORTS_BY_DATE_RANGE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            }//show records  match with patient name
            else if ((strFromDate == "" || strToDate == "") && strSpeciality != "---Select---" && strCaseType == "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_40DAYS_UNBILLED_REPORTS_BY_SPECIALITY", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate != "" || strToDate != "") && strSpeciality != "---Select---" && strCaseType == "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_40DAYS_UNBILLED_REPORTS_BY_ALL", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            }
            else if ((strFromDate == "" || strToDate == "") && strSpeciality == "---Select---" && strCaseType != "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_40DAYS_UNBILLED_REPORTS_CASE_TYPE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate != "" && strToDate != "") && strSpeciality == "---Select---" && strCaseType != "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_40DAYS_UNBILLED_REPORTS_BY_DATE_RANGE_AND_CASE_TYPE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate == "" || strToDate == "") && strSpeciality != "---Select---" && strCaseType != "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_40DAYS_UNBILLED_REPORTS_BY_SPECIALITY_AND_CASE_TYPE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((strFromDate != "" && strToDate != "") && strSpeciality != "---Select---" && strCaseType != "--- Select ---")
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_40DAYS_UNBILLED_REPORTS_BY_ALL3", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else
            {
                sqlCmd = new SqlCommand("SP_GET_TEST_40DAYS_UNBILLED_REPORTS", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }


            sqlCon.Open();
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCmpId);
            if (strFromDate != "" && strToDate != "") sqlCmd.Parameters.AddWithValue("@SZ_STRAT_DATE", strFromDate);
            if (strToDate != "" && strFromDate != "") sqlCmd.Parameters.AddWithValue("@SZ_END_DATE", strToDate);
            if (strSpeciality != "---Select---") sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", strSpeciality);
            if (strCaseType != "--- Select ---") sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_NAME", strCaseType);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
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
    #endregion



    public DataSet getBillReportSpecialityForTest(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_FOR_TEST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_SERVICE_DATE", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_SERVICE_DATE", objAL[7].ToString()); }
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
    public DataSet getBillReportSpecialityForTestSelect(ArrayList objAL, string proc)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand(proc, sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_SERVICE_DATE", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_SERVICE_DATE", objAL[7].ToString()); }
            sqlCmd.Parameters.AddWithValue("@BT_STATUS", objAL[8].ToString());
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

    public DataSet getBillReportSpecialityForTestTwo(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_FOR_TEST_TWO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_SERVICE_DATE", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_SERVICE_DATE", objAL[7].ToString()); }
            sqlCmd.Parameters.AddWithValue("@BT_STATUS", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@BT_STATUS1", objAL[9].ToString());
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
    public DataSet getBillReportSpecialityForTestThree(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_FOR_TEST_THREE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_SERVICE_DATE", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_SERVICE_DATE", objAL[7].ToString()); }
            sqlCmd.Parameters.AddWithValue("@BT_STATUS", objAL[8].ToString());
            sqlCmd.Parameters.AddWithValue("@BT_STATUS1", objAL[9].ToString());
            sqlCmd.Parameters.AddWithValue("@BT_STATUS2", objAL[10].ToString());
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




    // Function To Search Bill Report For Provider:- TUSHAR
    public DataSet GetDetailBillReportBySpeciality(ArrayList arrPayment)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            if (arrPayment[5].ToString() != "" && arrPayment[1].ToString() == "" && arrPayment[2].ToString() == "" && arrPayment[3].ToString() == "" && (arrPayment[4].ToString() == "NA" || arrPayment[4].ToString() == ""))
            {
                sqlCmd = new SqlCommand("SP_GET_DETAIL_BILL_REPORT_PROVIDER_SPECIALITY_TOTAL_OFFICEID", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if (arrPayment[5].ToString() != "" && arrPayment[1].ToString() == "" && arrPayment[2].ToString() != "" && arrPayment[3].ToString() != "" && (arrPayment[4].ToString() == "NA" || arrPayment[4].ToString() == ""))
            {
                sqlCmd = new SqlCommand("SP_GET_DETAIL_BILL_REPORT_PROVIDER_SPECIALITY_TOTAL_OFFICEID_DATE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if (arrPayment[5].ToString() != "" && arrPayment[1].ToString() == "" && arrPayment[2].ToString() == "" && arrPayment[3].ToString() == "" && (arrPayment[4].ToString() != "NA" || arrPayment[4].ToString() != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_DETAIL_BILL_REPORT_PROVIDER_SPECIALITY_TOTAL_OFFICEID_LOCATIONID", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if (arrPayment[5].ToString() != "" && arrPayment[1].ToString() == "" && arrPayment[2].ToString() != "" && arrPayment[3].ToString() != "" && (arrPayment[4].ToString() != "NA" || arrPayment[4].ToString() != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_DETAIL_BILL_REPORT_PROVIDER_SPECIALITY_TOTAL_OFFICEID_LOCATIONID_DATE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if (arrPayment[1].ToString() == "" && arrPayment[2].ToString() == "" && arrPayment[3].ToString() == "" && (arrPayment[4].ToString() == "NA" || arrPayment[4].ToString() == ""))
            {
                sqlCmd = new SqlCommand("SP_GET_DETAIL_BILL_REPORT_PROVIDER_SPECIALITY_TOTAL", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if (arrPayment[1].ToString() == "" && arrPayment[2].ToString() != "" && arrPayment[3].ToString() != "" && (arrPayment[4].ToString() == "NA" || arrPayment[4].ToString() == ""))
            {
                sqlCmd = new SqlCommand("SP_GET_DETAIL_BILL_REPORT_PROVIDER_SPECIALITY_TOTAL_DATE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if (arrPayment[1].ToString() == "" && arrPayment[2].ToString() == "" && arrPayment[3].ToString() == "" && (arrPayment[4].ToString() != "NA" || arrPayment[4].ToString() != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_DETAIL_BILL_REPORT_PROVIDER_SPECIALITY_TOTAL_LOCATIONID", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if (arrPayment[2].ToString() == "" && arrPayment[3].ToString() == "" && (arrPayment[4].ToString() == "NA" || arrPayment[4].ToString() == ""))
            {
                sqlCmd = new SqlCommand("SP_GET_DETAIL_BILL_REPORT_PROVIDER_SPECIALITY", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if (arrPayment[2].ToString() != "" && arrPayment[3].ToString() != "" && (arrPayment[4].ToString() == "NA" || arrPayment[4].ToString() == ""))
            {
                sqlCmd = new SqlCommand("SP_GET_DETAIL_BILL_REPORT_PROVIDER_DATE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if (arrPayment[2].ToString() == "" && arrPayment[3].ToString() == "" && (arrPayment[4].ToString() != "NA" || arrPayment[4].ToString() != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_DETAIL_BILL_REPORT_PROVIDER_LOCATION_ID", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if (arrPayment[2].ToString() != "" && arrPayment[3].ToString() != "" && (arrPayment[4].ToString() != "NA" || arrPayment[4].ToString() != ""))
            {
                sqlCmd = new SqlCommand("SP_GET_DETAIL_BILL_REPORT_PROVIDER_DATE_LOCATION_ID", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            if (arrPayment[5].ToString() != "" && arrPayment[1].ToString() != "")
            {
                arrPayment[5] = "";
            }
            sqlCon.Open();
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrPayment[0].ToString());
            if (arrPayment[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", arrPayment[1].ToString()); }
            if (arrPayment[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@FROMDATE", arrPayment[2].ToString()); }
            if (arrPayment[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@TODATE", arrPayment[3].ToString()); }
            if (arrPayment[4].ToString() != "NA" && arrPayment[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", arrPayment[4].ToString()); }
            if (arrPayment[5].ToString() != "NA" && arrPayment[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_OFFICE", arrPayment[5].ToString()); }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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
    //END

    //Function To Search Bill Report By Speciality:-ATUL
    public DataSet getBillReportSpecialitySearch(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {        //no contorl
            if ((objAL[6].ToString() == "" || objAL[7].ToString() == "") && objAL[4].ToString() == "" && objAL[5].ToString() == "")
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_DETAILS_FOR_GRID", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//date range
            else if ((objAL[6].ToString() != "" && objAL[7].ToString() != "") && objAL[4].ToString() == "" && objAL[5].ToString() == "")
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_DETAILS_DATE_RANGE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }//location
            else if ((objAL[6].ToString() == "" || objAL[7].ToString() == "") && objAL[4].ToString() != "" && objAL[5].ToString() == "")
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_DETAILS_LOCATION", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((objAL[6].ToString() == "" || objAL[7].ToString() == "") && objAL[4].ToString() == "" && objAL[5].ToString() != "")
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_DETAILS_USER_ID", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((objAL[6].ToString() != "" && objAL[7].ToString() != "") && objAL[4].ToString() != "" && objAL[5].ToString() == "")
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_DETAILS_LOCATION_AND_DATE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((objAL[6].ToString() != "" && objAL[7].ToString() != "") && objAL[4].ToString() == "" && objAL[5].ToString() != "")
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_DETAILS_USER_ID_AND_DATE", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((objAL[6].ToString() == "" || objAL[7].ToString() == "") && objAL[4].ToString() != "" && objAL[5].ToString() != "")
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_DETAILS_USER_ID_AND_LOCATION", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            else if ((objAL[6].ToString() != "" && objAL[7].ToString() != "") && objAL[4].ToString() != "" && objAL[5].ToString() != "")
            {
                sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_DETAILS_ALL", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            }
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_SORTING_ORDER", "");
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[3].ToString());
            if (objAL[6].ToString() != "" && objAL[7].ToString() != "") sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", objAL[6].ToString());
            if (objAL[6].ToString() != "" && objAL[7].ToString() != "") sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", objAL[7].ToString());
            if (objAL[4].ToString() != "") sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[4].ToString());
            if (objAL[5].ToString() != "") sqlCmd.Parameters.AddWithValue("@SZ_CREATED_USER_ID", objAL[5].ToString());

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
    //END


    #region"popup dignosis"
    public DataSet getAssociatedDiagnosisCodeForTest(string szCompanyId, string sz_SpecialityId, string sz_Case_Id)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DIAGNOSIS_CODE_TEST", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_Case_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", sz_SpecialityId);
            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
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
    #endregion


    public string GetNoFaultId(string sz_company_id)
    {
        string CaseTypeId = "";
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        sqlCon = new SqlConnection(strsqlCon);
        sqlCon.Open();
        try
        {
            sqlCmd = new SqlCommand("SP_GET_NOFAULT_TYPE_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
            CaseTypeId = ds.Tables[0].Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return CaseTypeId;
    }



    public DataSet GetBillReportsByAllDates(string P_Company_Id, string P_Speciality, string P_Status_Id, string P_From_Date, string P_ToDate, string P_From_Service_Date, string P_To_Service_Date)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_BILL_REPORT_FOR_TEST_FACILITY_DATES", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            if (P_From_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", P_From_Date); }
            if (P_ToDate != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", P_ToDate); }
            if (P_Speciality != "" && P_Speciality != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
            if (P_Status_Id != "" && P_Status_Id != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", P_Status_Id); }
            if (P_From_Service_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_SERVICE_DATE", P_From_Service_Date); }
            if (P_To_Service_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_SERVICE_DATE", P_To_Service_Date); }

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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



    public DataSet GetBillReportsByServiceDatesandUser(string P_Company_Id, string P_Speciality, string P_Status_Id, string P_UserId, string P_From_Service_Date, string P_To_Service_Date)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_BILL_REPORT_FOR_TEST_FACILITY_SERVICE_DATE_USERID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            if (P_Speciality != "" && P_Speciality != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
            if (P_Status_Id != "" && P_Status_Id != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", P_Status_Id); }
            if (P_From_Service_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_SERVICE_DATE", P_From_Service_Date); }
            if (P_To_Service_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_SERVICE_DATE", P_To_Service_Date); }
            if (P_UserId != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_UserId); }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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



    public DataSet GetBillReportsByServiceDate(string P_Company_Id, string P_Speciality, string P_Status_Id, string P_From_Service_Date, string P_To_Service_Date)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_BILL_REPORT_FOR_TEST_FACILITY_SERVICE_DATE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            if (P_Speciality != "" && P_Speciality != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
            if (P_Status_Id != "" && P_Status_Id != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", P_Status_Id); }
            if (P_From_Service_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_SERVICE_DATE", P_From_Service_Date); }
            if (P_To_Service_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_SERVICE_DATE", P_To_Service_Date); }

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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


    public DataSet GetBillReportsByAllDateAndUser(string P_Company_Id, string P_Speciality, string P_Status_Id, string P_User_Id, string P_From_Bill_Date, string P_To_Bill_Date, string P_From_Service_Date, string P_To_Service_Date)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_BILL_REPORT_FOR_TEST_FACILITY_ALL_DATE_USER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            if (P_Speciality != "" && P_Speciality != "NA") { sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", P_Speciality); }
            if (P_Status_Id != "" && P_Status_Id != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", P_Status_Id); }
            if (P_From_Service_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_SERVICE_DATE", P_From_Service_Date); }
            if (P_To_Service_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_SERVICE_DATE", P_To_Service_Date); }
            if (P_From_Bill_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", P_From_Bill_Date); }
            if (P_To_Bill_Date != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", P_To_Bill_Date); }
            if (P_User_Id != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id); }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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

    public int POMSave(string P_Company_Id, string P_User_Id, int P_Pom_verification)
    {
        int i_POM_Id = 0;
        sqlCon = new SqlConnection(strsqlCon);
        SqlTransaction transaction;
        sqlCon.Open();
        transaction = sqlCon.BeginTransaction();
        try
        {

            #region "Insert POM Entry Into TXN_BILL_POM TABLE AND GET LATEST POM_ID"
            sqlCmd = new SqlCommand("SP_SAVE_TXN_BILL_POM", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_POM_DATE", DateTime.Today.ToString("MM/dd/yyyy"));
            sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", 0);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", "");
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", "");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id);
            sqlCmd.Parameters.AddWithValue("@I_POM_ID", SqlDbType.Int);
            sqlCmd.Parameters["@I_POM_ID"].Direction = ParameterDirection.ReturnValue;
            sqlCmd.Parameters.AddWithValue("@I_VERIFICATION_POM", P_Pom_verification);
            sqlCmd.ExecuteNonQuery();
            i_POM_Id = Convert.ToInt32(sqlCmd.Parameters["@I_POM_ID"].Value.ToString());

            //   sqlCmd.ExecuteNonQuery();
            #endregion "End Of Code"
            transaction.Commit();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return i_POM_Id;


    }
    public void PacketUpdate(string P_Company_Id, string P_User_Id, int jobid, int pomid)
    {
        int i_POM_Id = 0;
        sqlCon = new SqlConnection(strsqlCon);

        sqlCon.Open();

        try
        {

            #region "Insert POM Entry Into TXN_BILL_POM TABLE AND GET LATEST POM_ID"
            sqlCmd = new SqlCommand("UPDATE_PACKET_POM", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id);
            sqlCmd.Parameters.AddWithValue("@I_POM_ID", pomid);
            sqlCmd.Parameters.AddWithValue("@JOB_ID", jobid);
            sqlCmd.ExecuteNonQuery();
            #endregion "End Of Code"

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
    public int CheckPOM(string P_Company_Id, string P_User_Id, int jobid, ref string filePath)
    {
        int i_POM_Id = 0;
        sqlCon = new SqlConnection(strsqlCon);

        sqlCon.Open();

        try
        {

            #region "Insert POM Entry Into TXN_BILL_POM TABLE AND GET LATEST POM_ID"
            sqlCmd = new SqlCommand("CHECK_POM", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id);
            sqlCmd.Parameters.AddWithValue("@JOB_ID", jobid);
            sqlCmd.Parameters.Add("@I_POM_ID",SqlDbType.Int);
            sqlCmd.Parameters["@I_POM_ID"].Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@FILE_PATH", SqlDbType.VarChar,500);
            sqlCmd.Parameters["@FILE_PATH"].Direction = ParameterDirection.Output;
            sqlCmd.ExecuteNonQuery();
            i_POM_Id = Convert.ToInt32(sqlCmd.Parameters["@I_POM_ID"].Value.ToString());
            filePath = sqlCmd.Parameters["@FILE_PATH"].Value.ToString();
            #endregion "End Of Code"

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

        return i_POM_Id;

    }
    public void POMEntry(int i_pom_id, string P_POM_Date, int ImageId, string P_File_Name, string P_File_Path, string P_Company_Id, string P_User_Id, ArrayList P_Bill_No, string P_Bill_Status)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        SqlTransaction transaction;
        sqlCon.Open();
        transaction = sqlCon.BeginTransaction();
        try
        {
            #region "Update POM Entry Into TXN_BILL_POM TABLE AND GET LATEST POM_ID"
            sqlCmd = new SqlCommand("SP_UPDATE_TXN_BILL_POM", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_POM_DATE", P_POM_Date);
            sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", ImageId);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", P_File_Name);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", P_File_Path);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id);
            sqlCmd.Parameters.AddWithValue("@I_POM_ID", i_pom_id);

            sqlCmd.ExecuteNonQuery();
            #endregion "End Of Code"

            #region "Update Txn_Bill_Transation set POM_ID Against All Bill_No On Which POM is Generated."
            for (int i = 0; i < P_Bill_No.Count; i++)
            {
                sqlCmd = new SqlCommand("SP_UPDATE_TXN_BILL_TRNSACTION", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandTimeout = 0;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                sqlCmd.Parameters.AddWithValue("@I_POM_ID", i_pom_id);
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", P_Bill_No[i]);
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS", P_Bill_Status);
                sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id);

                sqlCmd.ExecuteNonQuery();
            }
            #endregion"End Of Code"

            transaction.Commit();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();

        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
    }


    //public DataSet GetPOM_Report(string strFromBillDate, string strToBillDate, string strFromServiceDate, string strToServiceDate, string strFromPrintedDate, string strToPrintedDate, string strFromRecDate, string strTorecDate, string strBillNo, string strSpeciality, string strPatientName,string sz_CompanyID)
    //{
    //    sqlCon = new SqlConnection(strsqlCon);
    //    ds = new DataSet();
    //    try
    //    {
    //        //show all records 
    //        if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_POM_REPORT", sqlCon);
    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_DATE_RANGE", sqlCon);

    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_SPECIALITY", sqlCon);
    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL", sqlCon);

    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_CASE_TYPE", sqlCon);
    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_DATE_RANGE_AND_CASE_TYPE", sqlCon);
    //        }
    //        else if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_SPECIALITY_AND_CASE_TYPE", sqlCon);
    //        }
    //        else if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }//POM DATE SEARCH
    //        else if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_POM_REPORT_POM_DATE", sqlCon);
    //        }
    //        else if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        else if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        else if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality == "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        /////recors for speciality is not All for same date combination

    //        if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS", sqlCon);
    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_DATE_RANGE", sqlCon);

    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_SPECIALITY", sqlCon);
    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL", sqlCon);

    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_CASE_TYPE", sqlCon);
    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_DATE_RANGE_AND_CASE_TYPE", sqlCon);
    //        }
    //        else if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_SPECIALITY_AND_CASE_TYPE", sqlCon);
    //        }
    //        else if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        else if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        else if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        else if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        else if ((strFromBillDate == "" || strToBillDate == "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate == "" || strTorecDate == "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate == "" || strToServiceDate == "") && (strFromPrintedDate == "" || strToPrintedDate == "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        else if ((strFromBillDate != "" || strToBillDate != "") && (strFromServiceDate != "" || strToServiceDate != "") && (strFromPrintedDate != "" || strToPrintedDate != "") && (strFromRecDate != "" || strTorecDate != "") && strSpeciality != "--- Select ---")
    //        {
    //            sqlCmd = new SqlCommand("SP_GET_TEST_UNBILLED_REPORTS_BY_ALL3", sqlCon);
    //        }
    //        /////end




    //        sqlCon.Open();

    //        sqlCmd.CommandType = CommandType.StoredProcedure;
    //        sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
    //        if (strFromPrintedDate != "" && strToPrintedDate != "") sqlCmd.Parameters.AddWithValue("@SZ_POM_START_DATE", strFromPrintedDate);
    //        if (strFromPrintedDate != "" && strToPrintedDate != "") sqlCmd.Parameters.AddWithValue("@SZ_POM_END_DATE", strToPrintedDate);
    //        /* if (strToDate != "" && strFromDate != "") sqlCmd.Parameters.AddWithValue("@SZ_END_DATE", strToDate);
    //         if (strSpeciality != "--- Select ---") sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", strSpeciality);
    //         if (strCaseType != "--- Select ---") sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_NAME", strCaseType);
    //         */

    //        SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
    //        sqlda.Fill(ds);

    //    }
    //    catch (SqlException ex)
    //    {
    //        ex.Message.ToString();
    //    }
    //    finally
    //    {
    //        if (sqlCon.State == ConnectionState.Open)
    //        {
    //            sqlCon.Close();
    //        }
    //    }
    //    return ds;
    //}
    //atul add searching in pom report
    public DataSet GetPOM_Report(string strFromBillDate, string strToBillDate, string strFromServiceDate, string strToServiceDate, string strFromPrintedDate, string strToPrintedDate, string strFromRecDate, string strTorecDate, string strBillNo, string strSpeciality, string strPatientName, string sz_CompanyID, string szBillNO, string szPatientName)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            //show all records 

            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_POM_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_POM_START_DATE", strFromPrintedDate);
            sqlCmd.Parameters.AddWithValue("@SZ_POM_END_DATE", strToPrintedDate);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_START_DATE", strFromBillDate);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_END_DATE", strToBillDate);
            sqlCmd.Parameters.AddWithValue("@SZ_SERVICE_START_DATE", strFromServiceDate);
            sqlCmd.Parameters.AddWithValue("@SZ_SERVICE_END_DATE", strToServiceDate);
            sqlCmd.Parameters.AddWithValue("@SZ_RECEIVED_START_DATE", strFromRecDate);
            sqlCmd.Parameters.AddWithValue("@SZ_RECEIVED_END_DATE", strTorecDate);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillNO);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", szPatientName);
            if (!strSpeciality.Equals("NA") && !strSpeciality.Equals(""))
            {
                sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", strSpeciality);
            }


            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
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
    //style='font-weight:bold'
    public string GenrateHtmlForShowReport(string sz_ComapnyID, string StartDate, string EndDate, string OfficeId, string DocorId, string Status, int iFlag)
    {
        string szHtmlPath = ConfigurationSettings.AppSettings["SHOW_REPORT_HTML"].ToString();
        string szLine = "";
        string szPdfPath = "";
        StreamReader objReader;
        DataSet ds = new DataSet();
        DataTable OBJDTSum = new DataTable();
        DataRow objDRSum;
        string Office_Id = "";
        Bill_Sys_BillingCompanyDetails_BO _BillingCompany;
        objReader = new StreamReader(szHtmlPath);
        do
        {
            szLine = szLine + objReader.ReadLine() + "\r\n";
        } while (objReader.Peek() != -1);
        _BillingCompany = new Bill_Sys_BillingCompanyDetails_BO();
        DataSet ds1 = new DataSet();
        ds1 = _BillingCompany.GetBillingCompanyInfo(sz_ComapnyID);
        string szCmp = ds1.Tables[0].Rows[0][1].ToString();
        string szAddress = ds1.Tables[0].Rows[0][2].ToString();
        string szCityStateZip = ds1.Tables[0].Rows[0][3].ToString() + "," + ds1.Tables[0].Rows[0][5].ToString() + "&nbsp;" + ds1.Tables[0].Rows[0][4].ToString();
        string szPhoneFax = "";

        if (ds1.Tables[0].Rows[0][6].ToString().Equals(""))
        {
            szPhoneFax = "P:&nbsp;" + ",";
        }
        else
        {
            szPhoneFax = "P:&nbsp;" + ds1.Tables[0].Rows[0][6].ToString() + ",";
        }
        if (ds1.Tables[0].Rows[0][7].ToString().Equals(""))
        {
            szPhoneFax = szPhoneFax + "Email:&nbsp;";
        }
        else
        {
            szPhoneFax = szPhoneFax + "Email:&nbsp;" + ds1.Tables[0].Rows[0][7].ToString();
        }

        int IRecoreds = Convert.ToInt32(ConfigurationSettings.AppSettings["NO_OF_SHOW_RECORDS"].ToString());
        string szFileName = "SHOW REPORT";
        string szHtml = "";
        DataSet DsFinal = new DataSet();
        DataTable objDSOfficeWise = new DataTable();
        int count = 0;
        int ICount = 0;
        ds = _BillingCompany.GetOfficeWisePatientInfo(sz_ComapnyID, StartDate.ToString(), EndDate.ToString(), OfficeId.ToString(), DocorId.ToString(), Status.ToString());
        DsFinal = DisplayOfficeInGrid(ds, sz_ComapnyID, Status, StartDate, EndDate, DocorId, ds.Tables[0].Rows.Count);
        DataSet dsCount = new DataSet();
        try
        {

            //objDSOfficeWise = (DataTable)DsFinal.Tables[0];
            //DataTable dt = new DataTable();
            //dt = objDSOfficeWise;
            //dsCount.Tables.Add((DataTable)DsFinal.Tables[0]);
            int Total1 = 0, Total2 = 0, Total3 = 0, Total4 = 0, Total5 = 0, Total6 = 0, Total7 = 0, Total8 = 0, Total9 = 0;
            int t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0;
            for (int i = 0; i < DsFinal.Tables[1].Rows.Count; i++)
            {
                if (DsFinal.Tables[1].Rows[i][1].ToString() != null && DsFinal.Tables[1].Rows[i][1].ToString() != "" && DsFinal.Tables[1].Rows[i][1].ToString() != "&nbsp;")
                {
                    t1 = Convert.ToInt32(DsFinal.Tables[1].Rows[i][1].ToString());

                }

                if (DsFinal.Tables[1].Rows[i][2].ToString() != null && DsFinal.Tables[1].Rows[i][2].ToString() != "" && DsFinal.Tables[1].Rows[i][2].ToString() != "&nbsp;")
                {
                    t2 = Convert.ToInt32(DsFinal.Tables[1].Rows[i][2].ToString());

                }
                if (DsFinal.Tables[1].Rows[i][3].ToString() != null && DsFinal.Tables[1].Rows[i][3].ToString() != "" && DsFinal.Tables[1].Rows[i][3].ToString() != "&nbsp;")
                {
                    t3 = Convert.ToInt32(DsFinal.Tables[1].Rows[i][3].ToString());

                }
                if (DsFinal.Tables[1].Rows[i][4].ToString() != null && DsFinal.Tables[1].Rows[i][4].ToString() != "" && DsFinal.Tables[1].Rows[i][4].ToString() != "&nbsp;")
                {
                    t4 = Convert.ToInt32(DsFinal.Tables[1].Rows[i][4].ToString());

                }
                if (DsFinal.Tables[1].Rows[i][5].ToString() != null && DsFinal.Tables[1].Rows[i][5].ToString() != "" && DsFinal.Tables[1].Rows[i][5].ToString() != "&nbsp;")
                {
                    t5 = Convert.ToInt32(DsFinal.Tables[1].Rows[i][5].ToString());

                }
                if (DsFinal.Tables[1].Rows[i][6].ToString() != null && DsFinal.Tables[1].Rows[i][6].ToString() != "" && DsFinal.Tables[1].Rows[i][6].ToString() != "&nbsp;")
                {
                    t6 = Convert.ToInt32(DsFinal.Tables[1].Rows[i][6].ToString());

                }
                if (DsFinal.Tables[1].Rows[i][7].ToString() != null && DsFinal.Tables[1].Rows[i][7].ToString() != "" && DsFinal.Tables[1].Rows[i][7].ToString() != "&nbsp;")
                {
                    t7 = Convert.ToInt32(DsFinal.Tables[1].Rows[i][7].ToString());

                }
                if (DsFinal.Tables[1].Rows[i][8].ToString() != null && DsFinal.Tables[1].Rows[i][8].ToString() != "" && DsFinal.Tables[1].Rows[i][8].ToString() != "&nbsp;")
                {
                    t8 = Convert.ToInt32(DsFinal.Tables[1].Rows[i][8].ToString());

                }
                if (DsFinal.Tables[1].Rows[i][9].ToString() != null && DsFinal.Tables[1].Rows[i][9].ToString() != "" && DsFinal.Tables[1].Rows[i][9].ToString() != "&nbsp;")
                {
                    t9 = Convert.ToInt32(DsFinal.Tables[1].Rows[i][9].ToString());

                }


                Total1 = Total1 + t1;
                Total2 = Total2 + t2;
                Total3 = Total3 + t3;
                Total4 = Total4 + t4;
                Total5 = Total5 + t5;
                Total6 = Total6 + t6;
                Total7 = Total7 + t7;
                Total8 = Total8 + t8;
                Total9 = Total9 + t9;
            }
            int TotalVC = Total1 + Total4 + Total7;
            int TotalRS = Total2 + Total5 + Total8;
            int TotalNS = Total3 + Total6 + Total9;
            string Date_Range = "";
            try
            {
                if ((EndDate == "") && (StartDate == ""))
                    Date_Range = "All";
                else if ((EndDate != "") && (StartDate != ""))
                    Date_Range = StartDate.ToString() + " - " + EndDate.ToString();
                else if (EndDate == "")
                    Date_Range = StartDate.ToString() + " - " + "Till Date";
                else
                    Date_Range = "Till " + EndDate.ToString();
            }
            catch { }

            szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "<br/> Report Date : " + Date_Range + "</div><br/></font>";
            szHtml = szHtml + "<table  width='80%' border='1'><tr><td width='28%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Office Name</td> <td colspan='3' width='24%' align='center' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">MRI</td> <td  colspan='3' width='24%' align='center' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">CT-Scan</td> <td colspan='3' width='24%' align='center' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">X Ray</td> </tr><tr><td>&nbsp;</td><td width='8%' align='center'><b>VC</b></td><td width='8%' align='center'><b>RS</b></td><td width='8%' align='center'><b>NS</b></td><td width='8%' align='center'><b>VC</b></td><td width='8%' align='center'><b>RS</b></td><td width='8%' align='center'><b>NS</b></td><td width='8%' align='center'><b>VC</b></td><td width='8%' align='center'><b>RS</b></td><td width='8%' align='center'><b>NS</b></td></tr>";
            if (DsFinal.Tables[1].Rows.Count == 0)
            {
                if (DsFinal.Tables[1].Rows.Count == 0)
                {
                    szHtml = szHtml + "<tr><td width='28%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + "><b>TOTAL</b></td><td align='center' width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total1 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total2 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total3 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total4 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total5 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total6 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total7 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total8 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total9 + "</td></tr>";
                    szHtml = szHtml + "</table>";
                }
            }
            else
            {


                for (int k = 0; k < DsFinal.Tables[1].Rows.Count; k++)
                {


                    szHtml = szHtml + "<tr><td width='28%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + DsFinal.Tables[1].Rows[k][0].ToString() + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + DsFinal.Tables[1].Rows[k][1] + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + DsFinal.Tables[1].Rows[k][2] + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + DsFinal.Tables[1].Rows[k][3] + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + DsFinal.Tables[1].Rows[k][4] + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + DsFinal.Tables[1].Rows[k][5] + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + DsFinal.Tables[1].Rows[k][6] + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + DsFinal.Tables[1].Rows[k][7] + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + DsFinal.Tables[1].Rows[k][8] + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + DsFinal.Tables[1].Rows[k][9] + "</td></tr>";



                }
                szHtml = szHtml + "<tr><td width='28%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + "><b>TOTAL</b></td><td align='center' width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total1 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total2 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total3 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total4 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total5 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total6 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total7 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total8 + "</td><td width='8%' align='center' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + Total9 + "</td></tr>";
                szHtml = szHtml + "</table> <br/>";
            }
            szHtml = szHtml + "<font size='2'><div ALIGN=LEFT>Total Visit Completed–- &nbsp;" + TotalVC + "<br/> Total No Show-&nbsp;" + TotalNS + "<br/> TOtal Re schedule- &nbsp;" + TotalRS + "</div></font><span style='page-break-before: always;'>";
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count == 1)
            {
                int i = 0;
                if (ds.Tables[0].Rows.Count == 1)
                {
                    szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "<br/> Report Date : " + Date_Range + "</div><br/></font>";
                    szHtml = szHtml + "<font size='2'><b>" + ds.Tables[0].Rows[i][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[i][2].ToString() + "<br/>" + ds.Tables[0].Rows[i][3].ToString() + "," + ds.Tables[0].Rows[i][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][5].ToString() + "<br/>P:&nbsp;" + ds.Tables[0].Rows[i][13].ToString() + ",&nbsp;FAX:&nbsp;" + ds.Tables[0].Rows[i][14].ToString() + ",&nbsp;Email:&nbsp;" + ds.Tables[0].Rows[i][15].ToString() + "<br/><br/></font>";
                    szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Referring Doctor</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + " >Date Of Visit/Time Of Visit</td><td width='9%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Insurance Name<br/>[Claim Number]</td><td width='10%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Date Of Accident</td><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Treatment Codes</td><td width='14%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Next Appointmen</td></tr>";
                    szHtml = szHtml + "<tr><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='9%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='10%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][10].ToString() + " </td><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][11].ToString() + "</td><td width='14%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][12].ToString() + "</td></tr>";
                    szHtml = szHtml + "</table> <table width='100%' border = '1'><tr><td width='33%'" + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][0].ToString() + "</td><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][1].ToString() + "</td><td width='33%' >" + DsFinal.Tables[0].Rows[ICount][2].ToString() + "</td></tr><tr><td colspan='3' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][5].ToString() + "</td></tr></table>";
                    szHtml = szHtml + "</table>";
                    ICount++;
                }
                else
                {
                    szHtml = szHtml + "<font size='2'><b><div ALIGN=CENTER>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "<br/> Report Date : " + Date_Range + "</div><br/></font>";
                    szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Referring Doctor</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + " >Date Of Visit/Time Of Visit</td><td width='9%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Insurance Name<br/>[Claim Number]</td><td width='10%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Date Of Accident</td><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Treatment Codes</td><td width='14%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Next Appointmen</td></tr>";
                    szHtml = szHtml + "</table> ";
                    szHtml = szHtml + "</table> <h6> <table width='100%' border = '1'><tr><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >MRI -show(0), NS(0) , RS(0) , Total-0 </td><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " > X-RAY -show(0), NS(0) , RS(0) , Total-0</td><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >CT-SCAN -show(0), NS(0) , RS(0) , Total-0 </td></tr><tr><td colspan='3' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >Total -show(0), NS(0) , RS(0) , Total-0 </td></tr></table>";

                }

            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {


                        szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "<br/> Report Date : " + Date_Range + "</div><br/></font>";
                        szHtml = szHtml + "<font size='2'><b>" + ds.Tables[0].Rows[i][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[i][2].ToString() + "<br/>" + ds.Tables[0].Rows[i][3].ToString() + "," + ds.Tables[0].Rows[i][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][5].ToString() + "<br/>P:&nbsp;" + ds.Tables[0].Rows[i][13].ToString() + ",&nbsp;FAX:&nbsp;" + ds.Tables[0].Rows[i][14].ToString() + ",&nbsp;Email:&nbsp;" + ds.Tables[0].Rows[i][15].ToString() + "<br/><br/></font>";
                        szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Referring Doctor</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + " >Date Of Visit/Time Of Visit</td><td width='9%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Insurance Name<br/>[Claim Number]</td><td width='10%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Date Of Accident</td><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Treatment Codes</td><td width='14%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Next Appointmen</td></tr>";
                        szHtml = szHtml + "<tr><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='9%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='10%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][10].ToString() + " </td><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][11].ToString() + "</td><td width='14%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][12].ToString() + "</td></tr>";
                        count++;

                    }
                    else if (ds.Tables[0].Rows[i - 1][0].ToString().Equals(ds.Tables[0].Rows[i][0].ToString()) && i == ds.Tables[0].Rows.Count - 1)
                    {
                        if (count < IRecoreds)
                        {
                            szHtml = szHtml + "<tr><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='9%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='10%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][10].ToString() + " </td><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][11].ToString() + "</td><td width='14%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][12].ToString() + "</td></tr>";
                            szHtml = szHtml + "</table> <table width='100%' border = '1'><tr><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][0].ToString() + "</td><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][1].ToString() + "</td><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][2].ToString() + "</td></tr><tr><td colspan='3' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][5].ToString() + "</td></tr></table>";
                            szHtml = szHtml + "</table>";
                            ICount++;
                        }
                        else
                        {
                            count = 1;
                            szHtml = szHtml + "</table><span style='page-break-before: always;'>";
                            szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "<br/> Report Date : " + Date_Range + "</div></font>";
                            szHtml = szHtml + "<font size='2'><b>" + ds.Tables[0].Rows[i][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[i][2].ToString() + "<br/>" + ds.Tables[0].Rows[i][3].ToString() + "," + ds.Tables[0].Rows[i][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][5].ToString() + "<br/>P:&nbsp;" + ds.Tables[0].Rows[i][13].ToString() + ",&nbsp;FAX:&nbsp;" + ds.Tables[0].Rows[i][14].ToString() + ",&nbsp;Email:&nbsp;" + ds.Tables[0].Rows[i][15].ToString() + "<br/><br/></font>";
                            szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Referring Doctor</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + " >Date Of Visit/Time Of Visit</td><td width='9%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Insurance Name<br/>[Claim Number]</td><td width='10%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Date Of Accident</td><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Treatment Codes</td><td width='14%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Next Appointmen</td></tr>";
                            string a = ds.Tables[0].Rows[i][6].ToString();
                            szHtml = szHtml + "<tr><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='9%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='10%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][10].ToString() + " </td><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][11].ToString() + "</td><td width='14%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][12].ToString() + "</td></tr>";
                            szHtml = szHtml + "</table> <table width='100%' border = '1'><tr><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][0].ToString() + "</td><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][1].ToString() + "</td><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + "  >" + DsFinal.Tables[0].Rows[ICount][2].ToString() + "</td></tr><tr><td colspan='3' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + "  >" + DsFinal.Tables[0].Rows[ICount][5].ToString() + "</td></tr></table>";

                            ICount++;

                        }
                    }
                    else if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        count = 1;
                        szHtml = szHtml + "</table> <h6>" + DsFinal.Tables[0].Rows[ICount][0].ToString() + "&nbsp;&nbsp;" + DsFinal.Tables[0].Rows[ICount][1].ToString() + "&nbsp;&nbsp;" + DsFinal.Tables[0].Rows[ICount][2].ToString() + "&nbsp;&nbsp;" + DsFinal.Tables[0].Rows[ICount][5].ToString() + "</h6> <span style='page-break-before: always;'>";
                        szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "<br/> Report Date : " + Date_Range + "</div><br/></font>";
                        szHtml = szHtml + "<font size='2'><b>" + ds.Tables[0].Rows[i][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[i][2].ToString() + "<br/>" + ds.Tables[0].Rows[i][3].ToString() + "," + ds.Tables[0].Rows[i][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][5].ToString() + "<br/>P:&nbsp;" + ds.Tables[0].Rows[i][13].ToString() + ",&nbsp;FAX:&nbsp;" + ds.Tables[0].Rows[i][14].ToString() + ",&nbsp;Email:&nbsp;" + ds.Tables[0].Rows[i][15].ToString() + "<br/></font>";
                        szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Referring Doctor</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + " >Date Of Visit/Time Of Visit</td><td width='9%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Insurance Name<br/>[Claim Number]</td><td width='10%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Date Of Accident</td><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Treatment Codes</td><td width='14%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Next Appointmen</td></tr>";
                        szHtml = szHtml + "<tr><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='9%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='10%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][10].ToString() + " </td><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][11].ToString() + "</td><td width='14%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][12].ToString() + "</td></tr>";
                        szHtml = szHtml + "</table> <table width='100%' border = '1'><tr><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][0].ToString() + "</td><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][1].ToString() + "</td><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][2].ToString() + "</td></tr><tr><td colspan='3'" + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][5].ToString() + "</td></tr></table><span style='page-break-before: always;'>";
                        ICount++;
                    }
                    else if (i != ds.Tables[0].Rows.Count - 1)
                    {
                        if (ds.Tables[0].Rows[i - 1][0].ToString().Equals(ds.Tables[0].Rows[i][0].ToString()))
                        {
                            if (count < IRecoreds)
                            {
                                count++;
                                szHtml = szHtml + "<tr><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='9%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='10%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][10].ToString() + " </td><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][11].ToString() + "</td><td width='14%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][12].ToString() + "</td></tr>";
                            }
                            else
                            {
                                count = 1;
                                szHtml = szHtml + "</table><span style='page-break-before: always;'>";

                                szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "<br/> Report Date : " + Date_Range + "</div></font>";

                                szHtml = szHtml + "<font size='2'><b>" + ds.Tables[0].Rows[i][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[i][2].ToString() + "<br/>" + ds.Tables[0].Rows[i][3].ToString() + "," + ds.Tables[0].Rows[i][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][5].ToString() + "<br/>P:&nbsp;" + ds.Tables[0].Rows[i][13].ToString() + ",&nbsp;FAX:&nbsp;" + ds.Tables[0].Rows[i][14].ToString() + ",&nbsp;Email:&nbsp;" + ds.Tables[0].Rows[i][15].ToString() + "<br/><br/></font>";

                                szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Referring Doctor</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + " >Date Of Visit/Time Of Visit</td><td width='9%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Insurance Name<br/>[Claim Number]</td><td width='10%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Date Of Accident</td><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Treatment Codes</td><td width='14%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Next Appointmen</td></tr>";
                                szHtml = szHtml + "<tr><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='9%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='10%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][10].ToString() + " </td><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][11].ToString() + "</td><td width='14%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][12].ToString() + "</td></tr>";

                            }
                        }
                        else
                        {

                            szHtml = szHtml + "</table> <table width='100%' border = '1'><tr><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][0].ToString() + "</td><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][1].ToString() + "</td><td width='33%' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][2].ToString() + "</td></tr><tr><td colspan='3' " + ConfigurationManager.AppSettings["Bottomstyle"].ToString() + " >" + DsFinal.Tables[0].Rows[ICount][5].ToString() + "</td></tr></table><span style='page-break-before: always;'>";
                            ICount++;
                            count = 1;
                            szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "<br/> Report Date : " + Date_Range + "</div><br/></font>";

                            szHtml = szHtml + "<font size='2'><b>" + ds.Tables[0].Rows[i][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[i][2].ToString() + "<br/>" + ds.Tables[0].Rows[i][3].ToString() + "," + ds.Tables[0].Rows[i][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][5].ToString() + "<br/>P:&nbsp;" + ds.Tables[0].Rows[i][13].ToString() + ",&nbsp;FAX:&nbsp;" + ds.Tables[0].Rows[i][14].ToString() + ",&nbsp;Email:&nbsp;" + ds.Tables[0].Rows[i][15].ToString() + "<br/><br/></font>";
                            szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Referring Doctor</td><td width='8%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + " >Date Of Visit/Time Of Visit</td><td width='9%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Insurance Name<br/>[Claim Number]</td><td width='10%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Date Of Accident</td><td width='25%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Treatment Codes</td><td width='14%' " + ConfigurationManager.AppSettings["Headingstyle"].ToString() + ">Next Appointmen</td></tr>";
                            string a = ds.Tables[0].Rows[i][6].ToString();
                            szHtml = szHtml + "<tr><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='8%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='9%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='10%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][10].ToString() + " </td><td width='25%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][11].ToString() + "</td><td width='14%' " + ConfigurationManager.AppSettings["Datastyle"].ToString() + ">" + ds.Tables[0].Rows[i][12].ToString() + "</td></tr>";

                        }


                    }
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        szLine = szLine.Replace("SHOWREPORT", szHtml);
        Bill_Sys_NF3_Template _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
        string szPhisicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + "Reports/";
        string path = GeneratePDFWithPageN(szLine, szFileName, szPhisicalPath);
        string open_Path = ApplicationSettings.GetParameterValue("DocumentManagerURL").ToString() + "Reports/" + path;
        string Mopen_path = szPhisicalPath + path;
        if (iFlag == 1)
        {
            return Mopen_path;
        }
        else
        {
            return open_Path;
        }


    }
    public string GeneratePDFWithPageN(string strHtml, string FileName, string szFilePath)
    {
        GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
        string pdffilename = "";
        try
        {
            //  string szFileData = File.ReadAllText(ConfigurationManager.AppSettings["NF2_SENT_MAIL_HTML"]);
            //  szFileData = objPDF.getNF2MailDetails(szFileData, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            //string strHtml = GenerateHTML();
            //szFileData = szFileData.Replace("VL_SZ_TABLE_DATA", strHtml);
            //szFileData = szFileData.Replace("VL_SZ_CASE_COUNT", Session["VL_COUNT"].ToString());




            SautinSoft.PdfMetamorphosis objHTMToPDF = new SautinSoft.PdfMetamorphosis();
            objHTMToPDF.Serial = "10007706603";

            //objHTMToPDF.PageStyle.PageSize.WidthInch(11f);
            //objHTMToPDF.PageStyle.PageSize.Heightmm(8.5f);
            objHTMToPDF.PageStyle.PageOrientation.Landscape();
            objHTMToPDF.PageStyle.PageNumFormat = "{page}";
            string htmfilename = getFileName(FileName) + ".htm";
            pdffilename = getFileName(FileName) + ".pdf";

            if (!Directory.Exists(szFilePath))
            {
                Directory.CreateDirectory(szFilePath);

            }
            StreamWriter sw = new StreamWriter(szFilePath + htmfilename);
            sw.Write(strHtml);
            sw.Close();
            Int32 iTemp;
            iTemp = objHTMToPDF.HtmlToPdfConvertFile(szFilePath + htmfilename, szFilePath + pdffilename);
            string issue = objHTMToPDF.ErrorTrace.Message;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally
        {
        }
        string path = pdffilename;

        return path;

    }
    public string GeneratePDF(string strHtml, string FileName, string szFilePath)
    {
        GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
        string pdffilename = "";
        try
        {
            //  string szFileData = File.ReadAllText(ConfigurationManager.AppSettings["NF2_SENT_MAIL_HTML"]);
            //  szFileData = objPDF.getNF2MailDetails(szFileData, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            //string strHtml = GenerateHTML();
            //szFileData = szFileData.Replace("VL_SZ_TABLE_DATA", strHtml);
            //szFileData = szFileData.Replace("VL_SZ_CASE_COUNT", Session["VL_COUNT"].ToString());




            SautinSoft.PdfMetamorphosis objHTMToPDF = new SautinSoft.PdfMetamorphosis();
            objHTMToPDF.Serial = "10007706603";

            //objHTMToPDF.PageStyle.PageSize.WidthInch(11f);
            //objHTMToPDF.PageStyle.PageSize.Heightmm(8.5f);
            objHTMToPDF.PageStyle.PageOrientation.Landscape();
            string htmfilename = getFileName(FileName) + ".htm";
            pdffilename = getFileName(FileName) + ".pdf";

            if (!Directory.Exists(szFilePath))
            {
                Directory.CreateDirectory(szFilePath);

            }
            StreamWriter sw = new StreamWriter(szFilePath + htmfilename);
            sw.Write(strHtml);
            sw.Close();
            Int32 iTemp;
            iTemp = objHTMToPDF.HtmlToPdfConvertFile(szFilePath + htmfilename, szFilePath + pdffilename);
            string issue = objHTMToPDF.ErrorTrace.Message;

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
        }
        string path = pdffilename;

        return path;


    }

    public string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }
    public DataSet DisplayOfficeInGrid(DataSet p_objDS, string cmpid, string Status, string StartDate, string EndDate, string DocorId, int Icount)
    {
        Bill_Sys_BillingCompanyDetails_BO _BillingCompany;
        string MRIShow, MRINS, MRIRS, MRITotal, Show, NS, RS, Total;
        int TotalShow = 0, TotalNS = 0, TotalRS = 0, Totaltotal = 0;
        DataSet objDS = new DataSet();
        objDS = p_objDS;
        DataSet objdscount = new DataSet();
        DataSet objdsRowcount = new DataSet();
        DataTable objDT = new DataTable();
        DataTable objCnt = new DataTable();
        string sz_office_ID = "";
        int i;
        _BillingCompany = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {



            objDT.Columns.Add("SZ_PATIENT_NAME");
            objDT.Columns.Add("SZ_DOCTOR_NAME");
            objDT.Columns.Add("DT_EVENT_DATE");
            objDT.Columns.Add("SZ_INSURANCE_NAME");
            objDT.Columns.Add("DT_ACCIDENT_DATE");
            objDT.Columns.Add("SZ_PROC_CODE");
            objDT.Columns.Add("SZ_OFFICE");
            objDT.Columns.Add("SZ_OFFICE_ADDRESS");
            objDT.Columns.Add("SZ_OFFICE_CITY");
            objDT.Columns.Add("SZ_OFFICE_STATE");
            objDT.Columns.Add("SZ_OFFICE_ZIP");
            objDT.Columns.Add("Office_Id");

            objCnt.Columns.Add("OfficeName");
            objCnt.Columns.Add("Mri VC");
            objCnt.Columns.Add("Mri RS");
            objCnt.Columns.Add("Mri NS");

            objCnt.Columns.Add("CTSCAN VC");
            objCnt.Columns.Add("CTSCAN RS");
            objCnt.Columns.Add("CTSCAN NS");

            objCnt.Columns.Add("XRAY VC");
            objCnt.Columns.Add("XRAY RS");
            objCnt.Columns.Add("XRAY NS");

            objCnt.Columns.Add("EMG VC");
            objCnt.Columns.Add("EMG RS");
            objCnt.Columns.Add("EMG NS");

            DataRow objDR;
            DataRow objROWCnt;
            string sz_Office_Name = "NA";




            for (i = 0; i < objDS.Tables[0].Rows.Count; i++)
            {
                if (objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString().Equals(sz_Office_Name))
                {

                    //objDR = objDT.NewRow();
                    //objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    //objDR["SZ_DOCTOR_NAME"] = objDS.Tables[0].Rows[i]["SZ_DOCTOR_NAME"].ToString();
                    //objDR["DT_EVENT_DATE"] = objDS.Tables[0].Rows[i]["DT_EVENT_DATE"].ToString();
                    //objDR["SZ_INSURANCE_NAME"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString();
                    //objDR["DT_ACCIDENT_DATE"] = objDS.Tables[0].Rows[i]["DT_ACCIDENT_DATE"].ToString();
                    //objDR["SZ_PROC_CODE"] = objDS.Tables[0].Rows[i]["SZ_PROC_CODE"].ToString();
                    //objDR["SZ_OFFICE"] = objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString();
                    //objDR["SZ_OFFICE_ADDRESS"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_ADDRESS"].ToString();
                    //objDR["SZ_OFFICE_CITY"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_CITY"].ToString();
                    //objDR["SZ_OFFICE_STATE"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_STATE"].ToString();
                    //objDR["SZ_OFFICE_ZIP"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_ZIP"].ToString();
                    //objDR["Office_Id"] = objDS.Tables[0].Rows[i]["Office_Id"].ToString();
                    //objDT.Rows.Add(objDR);
                    sz_Office_Name = objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString();
                }
                else
                {
                    if (sz_Office_Name != "NA")
                    {
                        MRIShow = "0"; MRINS = "0"; MRIRS = "0"; MRITotal = "0"; Show = "0"; NS = "0"; RS = "0"; Total = "0";
                        TotalShow = 0; TotalNS = 0; TotalRS = 0; Totaltotal = 0;
                        //Code To get Room Count
                        objdsRowcount = _BillingCompany.GetCompanyWiseRoomCount(cmpid);

                        //end Of Code

                        objDR = objDT.NewRow();
                        objROWCnt = objCnt.NewRow();
                        for (int j = 0; j < objdsRowcount.Tables[0].Rows.Count; j++)
                        {
                            //Code to Show Patient Count Type Wise
                            if (Status.ToString() == "2" || Status.ToString() == "NA")
                            {
                                objdscount = _BillingCompany.GetTypeWisePatientCount(sz_office_ID, objdsRowcount.Tables[0].Rows[j][1].ToString(), "2", StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                                MRIShow = objdscount.Tables[0].Rows[0][0].ToString();
                            }
                            if (Status.ToString() == "3" || Status.ToString() == "NA")
                            {
                                objdscount = _BillingCompany.GetTypeWisePatientCount(sz_office_ID, objdsRowcount.Tables[0].Rows[j][1].ToString(), "3", StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                                MRINS = objdscount.Tables[0].Rows[0][0].ToString();
                            }
                            if (Status.ToString() == "1" || Status.ToString() == "NA")
                            {
                                objdscount = _BillingCompany.GetTypeWisePatientCount(sz_office_ID, objdsRowcount.Tables[0].Rows[j][1].ToString(), "1", StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                                MRIRS = objdscount.Tables[0].Rows[0][0].ToString();
                            }

                            objdscount = _BillingCompany.GetTypeWisePatientCount(sz_office_ID, objdsRowcount.Tables[0].Rows[j][1].ToString(), Status.ToString(), StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                            MRITotal = objdscount.Tables[0].Rows[0][0].ToString();



                            TotalShow = TotalShow + Convert.ToInt32(MRIShow);
                            TotalNS = TotalNS + Convert.ToInt32(MRINS);
                            TotalRS = TotalRS + Convert.ToInt32(MRIRS);
                            Totaltotal = Totaltotal + Convert.ToInt32(MRITotal);

                            if (objdsRowcount.Tables[0].Rows[j][0].ToString().Trim().Equals("CT-SCAN"))
                            {
                                objROWCnt["OfficeName"] = sz_Office_Name;
                                objROWCnt["CTSCAN VC"] = MRIShow.ToString();
                                objROWCnt["CTSCAN RS"] = MRIRS.ToString();
                                objROWCnt["CTSCAN NS"] = MRINS.ToString();


                            }
                            if (objdsRowcount.Tables[0].Rows[j][0].ToString().Trim().Equals("MRI"))
                            {
                                objROWCnt["OfficeName"] = sz_Office_Name;
                                objROWCnt["Mri VC"] = MRIShow.ToString();
                                objROWCnt["Mri RS"] = MRIRS.ToString();
                                objROWCnt["Mri NS"] = MRINS.ToString();

                            }
                            if (objdsRowcount.Tables[0].Rows[j][0].ToString().Trim().Equals("XRAY"))
                            {
                                objROWCnt["OfficeName"] = sz_Office_Name;
                                objROWCnt["XRAY VC"] = MRIShow.ToString();
                                objROWCnt["XRAY RS"] = MRIRS.ToString();
                                objROWCnt["XRAY NS"] = MRINS.ToString();


                            }
                            if (objdsRowcount.Tables[0].Rows[j][0].ToString().Trim().Equals("EMG"))
                            {
                                objROWCnt["OfficeName"] = sz_Office_Name;
                                objROWCnt["EMG VC"] = MRIShow.ToString();
                                objROWCnt["EMG RS"] = MRIRS.ToString();
                                objROWCnt["EMG NS"] = MRINS.ToString();


                            }


                            //objdscount = _BillingCompany.GetTypeWisePatientCount(sz_office_ID, "PG000000000000000037", "2");
                            //XrayShow = objdscount.Tables[0].Rows[0][0].ToString();

                            //objdscount = _BillingCompany.GetTypeWisePatientCount(sz_office_ID, "PG000000000000000037", "3");
                            //XrayNS = objdscount.Tables[0].Rows[0][0].ToString();

                            //objdscount = _BillingCompany.GetTypeWisePatientCount(sz_office_ID, "PG000000000000000037", "1");
                            //XrayRS = objdscount.Tables[0].Rows[0][0].ToString();

                            //objdscount = _BillingCompany.GetTypeWisePatientCount(sz_office_ID, "PG000000000000000037", "");
                            //XrayTotal = objdscount.Tables[0].Rows[0][0].ToString();


                            //objdscount = _BillingCompany.GetTypeWisePatientCount(sz_office_ID, "PG000000000000000075", "2");
                            //CTShow = objdscount.Tables[0].Rows[0][0].ToString();

                            //objdscount = _BillingCompany.GetTypeWisePatientCount(sz_office_ID, "PG000000000000000075", "3");
                            //CTNS = objdscount.Tables[0].Rows[0][0].ToString();

                            //objdscount = _BillingCompany.GetTypeWisePatientCount(sz_office_ID, "PG000000000000000075", "1");
                            //CTRS = objdscount.Tables[0].Rows[0][0].ToString();

                            //objdscount = _BillingCompany.GetTypeWisePatientCount(sz_office_ID, "PG000000000000000075", "");
                            //CTTotal = objdscount.Tables[0].Rows[0][0].ToString();

                            //TotalShow = Convert.ToInt32(MRIShow) + Convert.ToInt32(XrayShow) + Convert.ToInt32(CTShow);
                            //TotalNS = Convert.ToInt32(MRINS) + Convert.ToInt32(XrayNS) + Convert.ToInt32(CTNS);
                            //TotalRS = Convert.ToInt32(MRIRS) + Convert.ToInt32(XrayRS) + Convert.ToInt32(CTRS);
                            //Totaltatal = Convert.ToInt32(MRITotal) + Convert.ToInt32(XrayTotal) + Convert.ToInt32(CTTotal);
                            //objCnt.Columns.Add("Mri VC");
                            //objCnt.Columns.Add("Mri RS");
                            //objCnt.Columns.Add("Mri NS");

                            objDR[j] = "<b>" + objdsRowcount.Tables[0].Rows[j][0].ToString() + " -– show(" + MRIShow.ToString() + "), NS(" + MRINS.ToString() + ") , RS(" + MRIRS.ToString() + ") , Total-" + MRITotal.ToString() + " </b>";





                            if (j == objdsRowcount.Tables[0].Rows.Count - 1)
                            {
                                objDR[5] = "<b>Total -show(" + TotalShow.ToString() + "), NS(" + TotalNS.ToString() + ") , RS(" + TotalRS.ToString() + ") , Total-" + Totaltotal.ToString() + " </b>";
                            }
                            //objDR = objDT.NewRow();
                            //objDR["SZ_PATIENT_NAME"] = "<b>" + objdscount.Tables[0].Rows[j][0].ToString() + " – show(" + MRIShow.ToString() + "), NS(" + MRINS.ToString() + ") , RS(" + MRIRS.ToString() + ") , Total-" + MRITotal.ToString() + " </b>";
                            //objDR["SZ_DOCTOR_NAME"] = "<b>" + objdscount.Tables[0].Rows[j][0].ToString() + " – show(" + MRIShow.ToString() + "), NS(" + MRINS.ToString() + ") , RS(" + MRIRS.ToString() + ") , Total-" + MRITotal.ToString() + " </b>";
                            //objDR["DT_EVENT_DATE"] = "<b>" + objdscount.Tables[0].Rows[j][0].ToString() + " – show(" + MRIShow.ToString() + "), NS(" + MRINS.ToString() + ") , RS(" + MRIRS.ToString() + ") , Total-" + MRITotal.ToString() + " </b>";
                            //objDR["SZ_INSURANCE_NAME"] = "";
                            //objDR["DT_ACCIDENT_DATE"] = "";
                            //objDR["SZ_PROC_CODE"] = "<b>TOTAL – show(" + TotalShow + "), NS(" + TotalNS + ") , RS(" + TotalRS + ") , Total-" + Totaltatal + " </b>";
                            //objDR["SZ_OFFICE"] = "";
                            //objDR["SZ_OFFICE_ADDRESS"] = "";
                            //objDR["SZ_OFFICE_CITY"] = "";
                            //objDR["SZ_OFFICE_STATE"] = "";
                            //objDR["SZ_OFFICE_ZIP"] = "";
                            //objDR["Office_Id"] = "";
                            //objDT.Rows.Add(objDR);
                        }
                        objDT.Rows.Add(objDR);
                        objCnt.Rows.Add(objROWCnt);

                    }
                    //End Code
                    if (sz_Office_Name != "NA")
                    {
                        //objDR = objDT.NewRow();
                        //objDR["SZ_PATIENT_NAME"] = "<p style='page-break-before:always;' >";
                        //objDR["SZ_DOCTOR_NAME"] = "";
                        //objDR["DT_EVENT_DATE"] = "";
                        //objDR["SZ_INSURANCE_NAME"] = "";
                        //objDR["DT_ACCIDENT_DATE"] = "";
                        //objDR["SZ_PROC_CODE"] = "";
                        //objDR["SZ_OFFICE"] = "";
                        //objDR["SZ_OFFICE_ADDRESS"] = "";
                        //objDR["SZ_OFFICE_CITY"] = "";
                        //objDR["SZ_OFFICE_STATE"] = "";
                        //objDR["SZ_OFFICE_ZIP"] = "";
                        //objDR["Office_Id"] = "</p>";

                        //objDT.Rows.Add(objDR);
                    }
                    //objDR = objDT.NewRow();
                    //objDR["SZ_PATIENT_NAME"] = "<b>" + objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString() + "</b>";
                    //objDR["SZ_DOCTOR_NAME"] = "<b>" + objDS.Tables[0].Rows[i]["SZ_OFFICE_ADDRESS"].ToString() + "<b>";
                    //objDR["DT_EVENT_DATE"] = "<b>" + objDS.Tables[0].Rows[i]["SZ_OFFICE_CITY"].ToString() + "<b>" + "<b>" + objDS.Tables[0].Rows[i]["SZ_OFFICE_STATE"].ToString() + "<b>" + "<b>" + objDS.Tables[0].Rows[i]["SZ_OFFICE_ZIP"].ToString() + "<b>";
                    //objDR["SZ_INSURANCE_NAME"] = "";
                    //objDR["DT_ACCIDENT_DATE"] = "";
                    //objDR["SZ_PROC_CODE"] = "";
                    //objDR["SZ_OFFICE"] = "";
                    //objDR["SZ_OFFICE_ADDRESS"] = "";
                    //objDR["SZ_OFFICE_CITY"] = "";
                    //objDR["SZ_OFFICE_STATE"] = "";
                    //objDR["SZ_OFFICE_ZIP"] = "";
                    //objDR["Office_Id"] = "";
                    int count = Icount;
                    //objDT.Rows.Add(objDR);


                    //objDR = objDT.NewRow();
                    //objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    //objDR["SZ_DOCTOR_NAME"] = objDS.Tables[0].Rows[i]["SZ_DOCTOR_NAME"].ToString();
                    //objDR["DT_EVENT_DATE"] = objDS.Tables[0].Rows[i]["DT_EVENT_DATE"].ToString();
                    //objDR["SZ_INSURANCE_NAME"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString();
                    //objDR["DT_ACCIDENT_DATE"] = objDS.Tables[0].Rows[i]["DT_ACCIDENT_DATE"].ToString();
                    //objDR["SZ_PROC_CODE"] = objDS.Tables[0].Rows[i]["SZ_PROC_CODE"].ToString();
                    //objDR["SZ_OFFICE"] = objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString();
                    //objDR["SZ_OFFICE_ADDRESS"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_ADDRESS"].ToString();
                    //objDR["SZ_OFFICE_CITY"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_CITY"].ToString();
                    //objDR["SZ_OFFICE_STATE"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_STATE"].ToString();
                    //objDR["SZ_OFFICE_ZIP"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_ZIP"].ToString();
                    //objDR["Office_Id"] = objDS.Tables[0].Rows[i]["Office_Id"].ToString();
                    //objDT.Rows.Add(objDR);
                    sz_Office_Name = objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString();
                    sz_office_ID = objDS.Tables[0].Rows[i]["Office_Id"].ToString();

                }
            }



            //Code to Show Patient Count Type Wise
            if (i != 0)
            {
                MRIShow = "0"; MRINS = "0"; MRIRS = "0"; MRITotal = "0"; Show = "0"; NS = "0"; RS = "0"; Total = "0";
                TotalShow = 0; TotalNS = 0; TotalRS = 0; Totaltotal = 0;
                //Code To get Room Count
                objdsRowcount = _BillingCompany.GetCompanyWiseRoomCount(cmpid);
                //end Of Code

                objDR = objDT.NewRow();
                objROWCnt = objCnt.NewRow();
                for (int j = 0; j < objdsRowcount.Tables[0].Rows.Count; j++)
                {
                    //Code to Show Patient Count Type Wise
                    if (Status.ToString() == "2" || Status.ToString() == "NA")
                    {
                        objdscount = _BillingCompany.GetTypeWisePatientCount(objDS.Tables[0].Rows[i - 1]["Office_Id"].ToString(), objdsRowcount.Tables[0].Rows[j][1].ToString(), "2", StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                        MRIShow = objdscount.Tables[0].Rows[0][0].ToString();
                    }
                    if (Status.ToString() == "3" || Status.ToString() == "NA")
                    {
                        objdscount = _BillingCompany.GetTypeWisePatientCount(objDS.Tables[0].Rows[i - 1]["Office_Id"].ToString(), objdsRowcount.Tables[0].Rows[j][1].ToString(), "3", StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                        MRINS = objdscount.Tables[0].Rows[0][0].ToString();
                    }
                    if (Status.ToString() == "1" || Status.ToString() == "NA")
                    {
                        objdscount = _BillingCompany.GetTypeWisePatientCount(objDS.Tables[0].Rows[i - 1]["Office_Id"].ToString(), objdsRowcount.Tables[0].Rows[j][1].ToString(), "1", StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                        MRIRS = objdscount.Tables[0].Rows[0][0].ToString();
                    }

                    objdscount = _BillingCompany.GetTypeWisePatientCount(objDS.Tables[0].Rows[i - 1]["Office_Id"].ToString(), objdsRowcount.Tables[0].Rows[j][1].ToString(), Status.ToString(), StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                    MRITotal = objdscount.Tables[0].Rows[0][0].ToString();

                    TotalShow = TotalShow + Convert.ToInt32(MRIShow);
                    TotalNS = TotalNS + Convert.ToInt32(MRINS);
                    TotalRS = TotalRS + Convert.ToInt32(MRIRS);
                    Totaltotal = Totaltotal + Convert.ToInt32(MRITotal);

                    if (objdsRowcount.Tables[0].Rows[j][0].ToString().Trim().Equals("CT-SCAN"))
                    {
                        objROWCnt["OfficeName"] = sz_Office_Name;
                        objROWCnt["OfficeName"] = sz_Office_Name;
                        objROWCnt["CTSCAN VC"] = MRIShow.ToString();
                        objROWCnt["CTSCAN RS"] = MRIRS.ToString();
                        objROWCnt["CTSCAN NS"] = MRINS.ToString();


                    }
                    if (objdsRowcount.Tables[0].Rows[j][0].ToString().Trim().Equals("MRI"))
                    {
                        objROWCnt["OfficeName"] = sz_Office_Name;
                        objROWCnt["Mri VC"] = MRIShow.ToString();
                        objROWCnt["Mri RS"] = MRIRS.ToString();
                        objROWCnt["Mri NS"] = MRINS.ToString();

                    }
                    if (objdsRowcount.Tables[0].Rows[j][0].ToString().Trim().Equals("XRAY"))
                    {
                        objROWCnt["OfficeName"] = sz_Office_Name;
                        objROWCnt["XRAY VC"] = MRIShow.ToString();
                        objROWCnt["XRAY RS"] = MRIRS.ToString();
                        objROWCnt["XRAY NS"] = MRINS.ToString();


                    }
                    if (objdsRowcount.Tables[0].Rows[j][0].ToString().Trim().Equals("EMG"))
                    {
                        objROWCnt["OfficeName"] = sz_Office_Name;
                        objROWCnt["EMG VC"] = MRIShow.ToString();
                        objROWCnt["EMG RS"] = MRIRS.ToString();
                        objROWCnt["EMG NS"] = MRINS.ToString();


                    }

                    objDR[j] = "<b>" + objdsRowcount.Tables[0].Rows[j][0].ToString() + " - show(" + MRIShow.ToString() + "), NS(" + MRINS.ToString() + ") , RS(" + MRIRS.ToString() + ") , Total-" + MRITotal.ToString() + " </b>";

                    if (j == objdsRowcount.Tables[0].Rows.Count - 1)
                    {
                        objDR[5] = "<b>Total -  show(" + TotalShow.ToString() + "), NS(" + TotalNS.ToString() + ") , RS(" + TotalRS.ToString() + ") , Total-" + Totaltotal.ToString() + " </b>";
                    }


                }
                objDT.Rows.Add(objDR);
                objCnt.Rows.Add(objROWCnt);
            }
            //End Code

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        DataSet dsFinal = new DataSet();
        dsFinal.Tables.Add(objDT);
        dsFinal.Tables.Add(objCnt);


        return dsFinal;
    }






    public string GenrateHtmlForMissingReport(string szCompanyId, string szOfficeId, int iFlag)
    {
        string szHtmlPath = ConfigurationSettings.AppSettings["MISSING_REPORT_HTML"].ToString();
        string szLine = "";
        string szPdfPath = "";
        StreamReader objReader;
        DataSet ds = new DataSet();
        DataTable OBJDTSum = new DataTable();
        DataRow objDRSum;
        string Office_Id = "";
        int count = 0;
        int IRecoreds = Convert.ToInt32(ConfigurationSettings.AppSettings["NO_OF_SHOW_RECORDS"].ToString());
        Bill_Sys_BillingCompanyDetails_BO _BillingCompany = new Bill_Sys_BillingCompanyDetails_BO();
        ds = GetMissingRecored(szCompanyId, szOfficeId);
        objReader = new StreamReader(szHtmlPath);
        do
        {
            szLine = szLine + objReader.ReadLine() + "\r\n";
        } while (objReader.Peek() != -1);


        string szFileName = "MISSING REPORT";
        string szHtml = "";


        DataSet ds1 = new DataSet();
        ds1 = _BillingCompany.GetBillingCompanyInfo(szCompanyId);
        string szCmp = ds1.Tables[0].Rows[0][1].ToString();
        string szAddress = ds1.Tables[0].Rows[0][2].ToString();
        string szCityStateZip = ds1.Tables[0].Rows[0][3].ToString() + "," + ds1.Tables[0].Rows[0][5].ToString() + "&nbsp;" + ds1.Tables[0].Rows[0][4].ToString();
        string szPhoneFax = "";

        if (ds1.Tables[0].Rows[0][6].ToString().Equals(""))
        {
            szPhoneFax = "P:&nbsp;" + ",";
        }
        else
        {
            szPhoneFax = "P:&nbsp;" + ds1.Tables[0].Rows[0][6].ToString() + ",";
        }
        if (ds1.Tables[0].Rows[0][7].ToString().Equals(""))
        {
            szPhoneFax = szPhoneFax + "Email:&nbsp;";
        }
        else
        {
            szPhoneFax = szPhoneFax + "Email:&nbsp;" + ds1.Tables[0].Rows[0][7].ToString();
        }





        int ColoumnCount = ds.Tables[0].Columns.Count;

        ArrayList ColumnName = new ArrayList();
        for (int i = 17; i <= ColoumnCount - 1; i++)
        {
            ColumnName.Add(ds.Tables[0].Columns[i].Caption);

        }
        for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
        {


            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count == 1)
            {
                if (ds.Tables[0].Rows.Count == 0)
                {
                    szHtml = szHtml + "<font size='1'><b><div ALIGN=CENTER>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div><br/></font>";
                    //office haeding
                    szHtml = szHtml + "<font size='1'><b>" + ds.Tables[0].Rows[k][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[k][2].ToString() + "<br/>" + ds.Tables[0].Rows[k][3].ToString() + "," + ds.Tables[0].Rows[k][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[k][5].ToString() + "<br/>P:&nbsp;" + ds.Tables[0].Rows[k][12].ToString() + ",&nbsp;FAX:&nbsp;" + ds.Tables[0].Rows[k][13].ToString() + ",&nbsp;Email:&nbsp;" + "<br/><br/>/font>";
                    //szHtml = szHtml + "<table width='100%'><tr><td width='20%'>Patient Name<br/>(Chart Number)-<br/>Phone</td><td width='10%'>Referring <br/>Doc</td><td width='15%'>Insurance<br/>Claim Number</td><td width='10%'>Date Of <br/>Accident</td>";
                    szHtml = szHtml + "<table width='100%'><tr><td width='20%'>Patient Name<br/>(Chart Number)-<br/>Phone</td><td width='15%'>Date Of <br/>Accident</td><td width='15%'>Insurance</td><td width='10%'>Claim Number</td><td width='10%'>Ref Date</td><td width='15%'>Ref Proc<br/>Code</td>";
                    for (int i = 17; i <= ColoumnCount - 1; i++)
                    {

                        szHtml = szHtml + "<td width='2%'>" + ds.Tables[0].Rows[k][i] + "</td>";
                    }
                    szHtml = szHtml + "</tr>";
                    szHtml = szHtml + "<tr><td width='100%'colspan='4'><hr align=left noshade size='2' width='50%'/></td></tr> </table>";
                }
                else
                {   //company heading
                    szHtml = szHtml + "<font size='1'><b><div ALIGN=CENTER>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div><br/></font>";
                    //office heading
                    //szHtml = szHtml + "<font size='1'><b>" + ds.Tables[0].Rows[k][1].ToString()+"</b><br/>"+ ds.Tables[0].Rows[k][2].ToString()+"<br/>"+ ds.Tables[0].Rows[k][3].ToString()+","+ ds.Tables[0].Rows[k][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[k][5].ToString()+"<br/>P:&nbsp;" + ds.Tables[0].Rows[k][10].ToString() + ",&nbsp;FAX:&nbsp;" + ds.Tables[0].Rows[k][11].ToString() + ",&nbsp;Email:&nbsp;" + "<br/><br/></font>";
                    //table heading
                    // szHtml = szHtml + "<table width='100%'><tr><td width='20%'>Patient Name<br/>(Chart Number)-<br/>Phone</td><td width='10%'>Referring <br/>Doc</td><td width='15%'>Insurance<br/>Claim Number</td><td width='10%'>Date Of <br/>Accident</td>";
                    szHtml = szHtml + "<table width='100%'><tr><td width='20%'>Patient Name<br/>(Chart Number)-<br/>Phone</td><td width='15%'>Date Of <br/>Accident</td><td width='15%'>Insurance</td><td width='10%'>Claim Number</td><td width='15%'>Ref Date</td><td width='10%'>Ref Proc<br/>Code</td>";

                    //adding column heading
                    for (int i = 0; i < ColumnName.Count; i++)
                    {
                        szHtml = szHtml + "<td width='2%'>" + ColumnName[i].ToString() + "</td>";


                    }
                    szHtml = szHtml + "</tr>";
                    szHtml = szHtml + "<tr><td width='100%'colspan='4'><hr align=left noshade size='2' width='50%'/></td></tr>";

                    // szHtml = szHtml + "<tr><td width='20%'>" + ds.Tables[0].Rows[k][6] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][7] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][8] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][9] + "</td>";
                    szHtml = szHtml + "<tr><td width='20%'>" + ds.Tables[0].Rows[k][6] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][11] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][8] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][9] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][10] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][15] + "</td>";
                    //adding data for dyanamic column
                    for (int i = 17; i <= ColoumnCount - 1; i++)
                    {

                        szHtml = szHtml + "<td width='2%'>" + ds.Tables[0].Rows[k][i] + "</td>";
                    }
                    szHtml = szHtml + "</tr></table>";
                }

            }
            else
            {
                if (k == 0)
                {
                    //company heading
                    szHtml = szHtml + "<font size='1'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div><br/></font>";
                    //office heading
                    szHtml = szHtml + "<font size='1'><div ALIGN=LEFT><b>" + ds.Tables[0].Rows[k][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[k][2].ToString() + "<br/>" + ds.Tables[0].Rows[k][3].ToString() + "," + ds.Tables[0].Rows[k][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[k][5].ToString() + "<br/>P:&nbsp;" + ds.Tables[0].Rows[k][12].ToString() + ",&nbsp;FAX:&nbsp;" + ds.Tables[0].Rows[k][13].ToString() + ",&nbsp;Email:&nbsp;" + "</div><br/><br/></font>";
                    //table heading
                    //szHtml = szHtml + "<table width='100%'><tr><td width='20%'>Patient Name<br/>(Chart Number)-<br/>Phone</td><td width='10%'>Referring <br/>Doc</td><td width='15%'>Insurance<br/>Claim Number</td><td width='10%'>Date Of <br/>Accident</td>";
                    // szHtml = szHtml + "<table width='100%'><tr><td width='20%'>Patient Name<br/>(Chart Number)-<br/>Phone</td><td width='10%'>Date Of <br/>Accident</td><td width='15%'>Insurance</td><td width='10%'>Claim Number</td><td width='10%'>Ref Date</td><td width='10%'>Ref Proc<br/>Code</td>";
                    szHtml = szHtml + "<table width='100%'><tr><td width='20%'>Patient Name<br/>(Chart Number)-<br/>Phone</td><td width='15%'>Date Of <br/>Accident</td><td width='15%'>Insurance</td><td width='10%'>Claim Number</td><td width='15%'>Ref Date</td><td width='10%'>Ref Proc<br/>Code</td>";

                    //adding column heading
                    for (int i = 0; i < ColumnName.Count; i++)
                    {
                        szHtml = szHtml + "<td width='2%'>" + ColumnName[i].ToString() + "</td>";


                    }
                    szHtml = szHtml + "</tr>";
                    szHtml = szHtml + "<tr><td width='100%'colspan='4'><hr align=left noshade size='2' width='50%'/></td></tr>";
                    //szHtml = szHtml + "<tr><td width='20%'>" + ds.Tables[0].Rows[k][6] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][7] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][8] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][9] + "</td>";
                    szHtml = szHtml + "<tr><td width='20%'>" + ds.Tables[0].Rows[k][6] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][11] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][8] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][9] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][10] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][15] + "</td>";
                    //adding data for dyanamic column
                    for (int i = 17; i <= ColoumnCount - 1; i++)
                    {

                        szHtml = szHtml + "<td width='2%'>" + ds.Tables[0].Rows[k][i] + "</td>";
                    }
                    szHtml = szHtml + "</tr>";
                    count++;
                }
                else if (k == ds.Tables[0].Rows.Count - 1)
                {
                    if (count < IRecoreds)
                    {
                        //szHtml = szHtml + "<tr><td width='20%'>" + ds.Tables[0].Rows[k][6] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][7] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][8] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][9] + "</td>";
                        szHtml = szHtml + "<tr><td width='20%'>" + ds.Tables[0].Rows[k][6] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][11] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][8] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][9] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][10] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][15] + "</td>";
                        //adding data for dyanamic column
                        for (int i = 17; i <= ColoumnCount - 1; i++)
                        {

                            szHtml = szHtml + "<td width='2%'>" + ds.Tables[0].Rows[k][i] + "</td>";
                        }
                        szHtml = szHtml + "</tr> </table>";
                        count++;
                    }
                    else
                    {
                        szHtml = szHtml + "</table><span style='page-break-before: always;'>";

                        //company heading
                        szHtml = szHtml + "<font size='1'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div><br/></font>";
                        //office heading
                        szHtml = szHtml + "<font size='1'><b>" + ds.Tables[0].Rows[k][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[k][2].ToString() + "<br/>" + ds.Tables[0].Rows[k][3].ToString() + "," + ds.Tables[0].Rows[k][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[k][5].ToString() + "<br/>P:&nbsp;" + ds.Tables[0].Rows[k][12].ToString() + ",&nbsp;FAX:&nbsp;" + ds.Tables[0].Rows[k][13].ToString() + ",&nbsp;Email:&nbsp;" + "<br/><br/></font>";

                        //table heading
                        // szHtml = szHtml + "<table width='100%'><tr><td width='20%'>Patient Name<br/>(Chart Number)-<br/>Phone</td><td width='10%'>Referring <br/>Doc</td><td width='15%'>Insurance<br/>Claim Number</td><td width='10%'>Date Of <br/>Accident</td>";
                        szHtml = szHtml + "<table width='100%'><tr><td width='20%'>Patient Name<br/>(Chart Number)-<br/>Phone</td><td width='15%'>Date Of <br/>Accident</td><td width='15%'>Insurance</td><td width='10%'>Claim Number</td><td width='15%'>Ref Date</td><td width='10%'>Ref Proc<br/>Code</td>";

                        //adding column heading
                        for (int i = 0; i < ColumnName.Count; i++)
                        {
                            szHtml = szHtml + "<td width='2%'>" + ColumnName[i].ToString() + "</td>";


                        }

                        szHtml = szHtml + "</tr>";
                        szHtml = szHtml + "<tr><td width='100%'colspan='4'><hr align=left noshade size='2' width='50%'/></td></tr>";
                        // szHtml=szHtml+"<tr><td width='20%'>" + ds.Tables[0].Rows[k][6] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][7] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][8] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][9] + "</td>";
                        szHtml = szHtml + "<tr><td width='20%'>" + ds.Tables[0].Rows[k][6] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][11] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][8] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][9] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][10] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][15] + "</td>";
                        //adding data for dyanamic column
                        for (int i = 17; i <= ColoumnCount - 1; i++)
                        {

                            szHtml = szHtml + "<td width='2%'>" + ds.Tables[0].Rows[k][i] + "</td>";
                        }
                        szHtml = szHtml + "</tr></table>";
                        count = 1;
                    }

                }
                else if (k != ds.Tables[0].Rows.Count - 1)
                {
                    if (count < IRecoreds)
                    {
                        //szHtml = szHtml + "<tr><td width='20%'>" + ds.Tables[0].Rows[k][6] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][7] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][8] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][9] + "</td>";
                        szHtml = szHtml + "<tr><td width='20%'>" + ds.Tables[0].Rows[k][6] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][11] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][8] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][9] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][10] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][15] + "</td>";
                        //adding data for dyanamic column
                        for (int i = 17; i <= ColoumnCount - 1; i++)
                        {

                            szHtml = szHtml + "<td width='2%'>" + ds.Tables[0].Rows[k][i] + "</td>";
                        }
                        szHtml = szHtml + "</tr>";
                        count++;

                    }
                    else
                    {
                        szHtml = szHtml + "</table><span style='page-break-before: always;'>";

                        //company heading
                        szHtml = szHtml + "<font size='1'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div><br/></font>";
                        //office heading
                        szHtml = szHtml + "<font size='1'><b>" + ds.Tables[0].Rows[k][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[k][2].ToString() + "<br/>" + ds.Tables[0].Rows[k][3].ToString() + "," + ds.Tables[0].Rows[k][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[k][5].ToString() + "<br/>P:&nbsp;" + ds.Tables[0].Rows[k][12].ToString() + ",&nbsp;FAX:&nbsp;" + ds.Tables[0].Rows[k][13].ToString() + ",&nbsp;Email:&nbsp;" + "<br/><br/></font>";

                        //table heading
                        // szHtml = szHtml + "<table width='100%'><tr><td width='20%'>Patient Name<br/>(Chart Number)-<br/>Phone</td><td width='10%'>Referring <br/>Doc</td><td width='15%'>Insurance<br/>Claim Number</td><td width='10%'>Date Of <br/>Accident</td>";
                        szHtml = szHtml + "<table width='100%'><tr><td width='20%'>Patient Name<br/>(Chart Number)-<br/>Phone</td><td width='15%'>Date Of <br/>Accident</td><td width='15%'>Insurance</td><td width='10%'>Claim Number</td><td width='15%'>Ref Date</td><td width='10%'>Ref Proc<br/>Code</td>";

                        //adding column heading
                        for (int i = 0; i < ColumnName.Count; i++)
                        {
                            szHtml = szHtml + "<td width='2%'>" + ColumnName[i].ToString() + "</td>";


                        }

                        szHtml = szHtml + "</tr>";
                        szHtml = szHtml + "<tr><td width='100%'colspan='4'><hr align=left noshade size='2' width='50%'/></td></tr>";
                        //  szHtml=szHtml+"  <tr><td width='20%'>" + ds.Tables[0].Rows[k][6] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][7] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][8] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][9] + "</td>";
                        szHtml = szHtml + "<tr><td width='20%'>" + ds.Tables[0].Rows[k][6] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][11] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][8] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][9] + "</td><td width='15%'>" + ds.Tables[0].Rows[k][10] + "</td><td width='10%'>" + ds.Tables[0].Rows[k][15] + "</td>";
                        //adding data for dyanamic column
                        for (int i = 17; i <= ColoumnCount - 1; i++)
                        {

                            szHtml = szHtml + "<td width='2%'>" + ds.Tables[0].Rows[k][i] + "</td>";
                        }
                        szHtml = szHtml + "</tr>";
                        count = 1;
                    }
                }
            }
        }

        szLine = szLine.Replace("MISSINGREPORT", szHtml);
        Bill_Sys_NF3_Template _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
        string szPhisicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + "Reports/";
        string path = GeneratePDF(szLine, szFileName, szPhisicalPath);
        string open_Path = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + "Reports/" + path;
        string Mopen_path = szPhisicalPath + path;
        if (iFlag == 1)
        {
            return Mopen_path;
        }
        else
        {
            return open_Path;
        }
    }

    public DataSet GetMissingRecored(string szCompanyID, string szOfficeID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_OFFICEWISE_MISSING_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            if (szOfficeID != "" && szOfficeID != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", szOfficeID); }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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


    public DataSet GetMiscPaymentDetails(ArrayList arrPayment)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_MISC_PAYMENT_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrPayment[0].ToString());
            sqlCmd.Parameters.AddWithValue("@FROMDATE", arrPayment[1].ToString());
            sqlCmd.Parameters.AddWithValue("@TODATE", arrPayment[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", arrPayment[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", arrPayment[4].ToString());
            sqlCmd.Parameters.AddWithValue("@CHECKFROMDATE", arrPayment[5].ToString());
            sqlCmd.Parameters.AddWithValue("@CHECKTODATE", arrPayment[6].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHECKNUMBER", arrPayment[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHECKAMOUNT", arrPayment[8].ToString());
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);

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



    public DataSet GetBillDetails(String sz_PomId, String sz_CompanyId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);
            sqlCmd.Parameters.AddWithValue("@I_POM_ID", sz_PomId);

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);

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



    public DataSet GetPaymentDetails(String sz_BillNo, String sz_CompanyId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PAYMENT_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", sz_BillNo);

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);

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



    public int Update_Insurace_Claim(string szComapnyId, string szCaseID, string szInsCmpId, string InsAddId, string ClaimNO)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        int iReturn = 0;

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_INSURACE_ADDRESS_AND_CLAIM_NUMBERSE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szComapnyId);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseID);
            if (szInsCmpId != "" && szInsCmpId != "NA")
            {
                sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_ID", szInsCmpId);
            }
            if (InsAddId != "" && InsAddId != "NA")
            {
                sqlCmd.Parameters.AddWithValue("@SZ_INS_ADDRESS_ID", InsAddId);
            }
            if (ClaimNO != "")
            {

                sqlCmd.Parameters.AddWithValue("@SZ_CLAIM_NUMBER", ClaimNO);
            }
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

    public DataSet Get_Attorney_Report(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_ATTORNEY_INFORMATION", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0]);
            if (objAL[1] != "") { sqlCmd.Parameters.AddWithValue("@DAT_OF_ACCIDENT", objAL[1]); }
            if (objAL[2] != "") { sqlCmd.Parameters.AddWithValue("@PATIENT_NAME", objAL[2]); }
            if (objAL[3] != "") { sqlCmd.Parameters.AddWithValue("@CASE_NO", objAL[3]); }
            if (objAL[4].ToString() != "" && objAL[4].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@ATTORNEY", objAL[4]); }
            if (objAL[5].ToString() != "" && objAL[5].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@CASE_STATUS", objAL[5]); }
            if (objAL[6].ToString() != "" && objAL[6].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@CASE_TYPE", objAL[6]); }
            if (objAL[7].ToString() != "" && objAL[7].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@INSURANCE_COMPANY", objAL[7]); }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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

    public void UpdateRadingDoctorByEventId(string szDocId, string szEventId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_READINGDOCTOR_BY_EVENT_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_REFERRING_DOCTOR_ID", szDocId);
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", szEventId);
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
    public DataSet Get_Billing_Report(string szCmpId, string strFromDate, string strToDate, string strSpeciality, string strCaseType, string strDoctorID)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILLING_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCmpId);
            sqlCmd.Parameters.AddWithValue("@SZ_STRAT_DATE", strFromDate);
            sqlCmd.Parameters.AddWithValue("@SZ_END_DATE", strToDate);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", strDoctorID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", strSpeciality);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", strCaseType);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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
    public Result SaveBillTransaction(ArrayList savebill, string sz_EventId, ArrayList p_objProcedureCodes, ArrayList p_objALDiagCode)
    {
        Result objResult = new Result();
        SqlConnection conn = new SqlConnection(strsqlCon);
        conn.Open();
        SqlCommand comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();

        String szLatestBillNumber = "";
        try

        {
            comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", savebill[0].ToString());
            comm.Parameters.AddWithValue("@DT_BILL_DATE", savebill[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", savebill[2].ToString());
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", savebill[3].ToString());
            comm.Parameters.AddWithValue("@SZ_TYPE", savebill[4].ToString());
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR_ID", savebill[5].ToString());
            comm.Parameters.AddWithValue("@SZ_REFERRING_COMPANY_ID", savebill[6].ToString());
            comm.Parameters.AddWithValue("@SZ_USER_ID", savebill[7].ToString());
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();


            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_LATEST_BILL_TRANSACTIONS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;

            comm.Parameters.AddWithValue("@FLAG", "GETLATESTBILLID");
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", savebill[2].ToString());

            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szLatestBillNumber = Convert.ToString(dr[0]);
            }
            dr.Close();


            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Transaction = transaction;
            comm.CommandText = "CHECK_EVENT_BILLED_STATUS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            SqlDataReader dr2 = comm.ExecuteReader();
            while (dr2.Read())
            {
                string szMsg = "";
                szMsg = Convert.ToString(dr2["msg"]);
                if (szMsg == "Error")
                {
                    objResult.msg_code = "ERR";
                    objResult.msg = "You can not add multiple bill for same visit.";
                    objResult.bill_no = "";
                    dr2.Close();
                    dr.Close();
                    transaction.Rollback();
                    return objResult;
                }
            }
            dr2.Close();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Transaction = transaction;
            comm.CommandText = "UPDATE_EVENT_STATUS";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
            comm.Parameters.AddWithValue("@BT_STATUS", "1");
            comm.Parameters.AddWithValue("@I_STATUS", "2");
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
            comm.Parameters.AddWithValue("@DT_BILL_DATE", savebill[1].ToString());
            comm.ExecuteNonQuery();



            if (p_objProcedureCodes != null)
            {
                if (p_objProcedureCodes.Count > 0)
                {
                    for (int i = 0; i < p_objProcedureCodes.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();
                        objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_SAVE_REFERRAL_PROC_CODE";
                        comm.CommandTimeout = 0;
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_PROC_CODE", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        comm.Parameters.AddWithValue("@I_EVENT_ID", sz_EventId);
                        comm.Parameters.AddWithValue("@I_STATUS", "2");
                        comm.ExecuteNonQuery();
                    }
                }
            }


            #region "Save Diagnosis Code."

            if (p_objALDiagCode != null)
            {
                if (p_objALDiagCode.Count > 0)
                {
                    for (int i = 0; i < p_objALDiagCode.Count; i++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)p_objALDiagCode[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        comm.CommandTimeout = 0;
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion

            #region "Save procedure codes."

            if (p_objProcedureCodes != null)
            {
                if (p_objProcedureCodes.Count > 0)
                {
                    for (int i = 0; i < p_objProcedureCodes.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                        objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        comm.CommandTimeout = 0;
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        comm.Parameters.AddWithValue("@FL_AMOUNT", objBillProcedureCodeEO.FL_AMOUNT);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE);
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@I_UNIT", objBillProcedureCodeEO.I_UNIT);
                        comm.Parameters.AddWithValue("@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE);
                        comm.Parameters.AddWithValue("@FLT_FACTOR", objBillProcedureCodeEO.FLT_FACTOR);
                        comm.Parameters.AddWithValue("@DOCT_AMOUNT", objBillProcedureCodeEO.DOCT_AMOUNT);
                        comm.Parameters.AddWithValue("@PROC_AMOUNT", objBillProcedureCodeEO.PROC_AMOUNT);
                        comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID);
                        comm.Parameters.AddWithValue("@SZ_CASE_ID", objBillProcedureCodeEO.SZ_CASE_ID);
                        comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID);
                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString());
                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString());
                        if (objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "" && objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "&nbsp;")
                        {
                            comm.Parameters.AddWithValue("@SZ_PATIENT_TREATMENT_ID", objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID);
                        }
                        comm.Parameters.AddWithValue("@FLAG", "ADD");
                        comm.ExecuteNonQuery();
                        dr.Close();
                    }
                }
            }

            #endregion
            transaction.Commit();
            objResult.bill_no = szLatestBillNumber;
            objResult.msg = "Sccuess";
            objResult.msg_code = "SCC";

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return objResult;
    }
    public DataSet GetProcdureCode(string sz_CompanyID, string sz_Event_Id)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_ASSOCIATE_PROCEDURE_CODE_FOR_BILL_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", sz_Event_Id);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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

    public string GetTypeCode(string sz_CompanyID, string ProcId)
    {
        sqlCon = new SqlConnection(strsqlCon);

        string strReturn = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_ASSOCIATE_PROCEDURE_CODE_FOR_BILL_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_CODE_ID", "ProcId");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                strReturn = dr[0].ToString();
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
        return strReturn;
    }


    //For Bing Drop Down On Shcedule report:- TUSHAR
    public DataSet GetProcedureCode(string sz_CompanyID, string ProcId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PROCEDURE_CODE_FOR_SHCEDULE_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SPECIALITY", ProcId);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_REFERRAL_PROC_CODE");
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


    public DataSet GetProcedureDesc(string sz_CompanyID, string ProcId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PROCEDURE_CODE_FOR_SHCEDULE_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SPECIALITY", ProcId);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_REFERRAL_PROC_DESC");
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

    public string GenrateHtmlForShowReportEMG(string sz_ComapnyID, string StartDate, string EndDate, string OfficeId, string DocorId, string Status, int iFlag)
    {
        string szHtmlPath = ConfigurationSettings.AppSettings["SHOW_REPORT_HTML"].ToString();
        string szLine = "";
        string szPdfPath = "";
        StreamReader objReader;
        DataSet ds = new DataSet();
        DataTable OBJDTSum = new DataTable();
        DataRow objDRSum;
        string Office_Id = "";
        Bill_Sys_BillingCompanyDetails_BO _BillingCompany;
        objReader = new StreamReader(szHtmlPath);
        do
        {
            szLine = szLine + objReader.ReadLine() + "\r\n";
        } while (objReader.Peek() != -1);
        _BillingCompany = new Bill_Sys_BillingCompanyDetails_BO();
        DataSet ds1 = new DataSet();
        ds1 = _BillingCompany.GetBillingCompanyInfo(sz_ComapnyID);
        string szCmp = ds1.Tables[0].Rows[0][1].ToString();
        string szAddress = ds1.Tables[0].Rows[0][2].ToString();
        string szCityStateZip = ds1.Tables[0].Rows[0][3].ToString() + "," + ds1.Tables[0].Rows[0][5].ToString() + "&nbsp;" + ds1.Tables[0].Rows[0][4].ToString();
        string szPhoneFax = "";

        if (ds1.Tables[0].Rows[0][6].ToString().Equals(""))
        {
            szPhoneFax = "Tel:&nbsp;" + ",";
        }
        else
        {
            szPhoneFax = "Tel:&nbsp;" + ds1.Tables[0].Rows[0][6].ToString() + ",";
        }
        if (ds1.Tables[0].Rows[0][7].ToString().Equals(""))
        {
            szPhoneFax = szPhoneFax + "Email:&nbsp;";
        }
        else
        {
            szPhoneFax = szPhoneFax + "Email:&nbsp;" + ds1.Tables[0].Rows[0][7].ToString();
        }

        int IRecoreds = Convert.ToInt32(ConfigurationSettings.AppSettings["NO_OF_SHOW_RECORDS_EMG"].ToString());
        string szFileName = "SHOW REPORT";
        string szHtml = "";
        DataSet DsFinal = new DataSet();
        DataTable objDSOfficeWise = new DataTable();
        int count = 0;
        int ICount = 0;
        ds = GetOfficeWisePatientInfoEMG(sz_ComapnyID, StartDate.ToString(), EndDate.ToString(), OfficeId.ToString(), DocorId.ToString(), Status.ToString());
        DsFinal = DisplayOfficeInGrid(ds, sz_ComapnyID, Status, StartDate, EndDate, DocorId, ds.Tables[0].Rows.Count);
        DataSet dsCount = new DataSet();
        try
        {

            //objDSOfficeWise = (DataTable)DsFinal.Tables[0];
            //DataTable dt = new DataTable();
            //dt = objDSOfficeWise;
            //dsCount.Tables.Add((DataTable)DsFinal.Tables[0]);
            int Total1 = 0, Total2 = 0, Total3 = 0;
            int t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0, t8 = 0, t9 = 0;
            for (int i = 0; i < DsFinal.Tables[1].Rows.Count; i++)
            {
                if (DsFinal.Tables[1].Rows[i][10].ToString() != null && DsFinal.Tables[1].Rows[i][10].ToString() != "" && DsFinal.Tables[1].Rows[i][10].ToString() != "&nbsp;")
                {
                    t1 = Convert.ToInt32(DsFinal.Tables[1].Rows[i][10].ToString());

                }

                if (DsFinal.Tables[1].Rows[i][11].ToString() != null && DsFinal.Tables[1].Rows[i][11].ToString() != "" && DsFinal.Tables[1].Rows[i][11].ToString() != "&nbsp;")
                {
                    t2 = Convert.ToInt32(DsFinal.Tables[1].Rows[i][11].ToString());

                }
                if (DsFinal.Tables[1].Rows[i][12].ToString() != null && DsFinal.Tables[1].Rows[i][12].ToString() != "" && DsFinal.Tables[1].Rows[i][12].ToString() != "&nbsp;")
                {
                    t3 = Convert.ToInt32(DsFinal.Tables[1].Rows[i][12].ToString());

                }


                Total1 = Total1 + t1;
                Total2 = Total2 + t2;
                Total3 = Total3 + t3;

            }
            int TotalVC = Total1;
            int TotalRS = Total2;
            int TotalNS = Total3;



            //  szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div><br/></font>";
            szHtml = szHtml + "<font size='2'><table  width='80%' border='1' align='center'><tr><td width='28%'><b>Office Name</b></td> <td colspan='3' width='24%' align='center'><b>EMG</b></td> </tr><tr><td>&nbsp;</td><td width='8%' align='center'><b>VC</b></td><td width='8%' align='center'><b>RS</b></td><td width='8%' align='center'><b>NS</b></td></tr>";
            if (DsFinal.Tables[1].Rows.Count == 0)
            {
                if (DsFinal.Tables[1].Rows.Count == 0)
                {
                    szHtml = szHtml + "<tr><td width='28%'><b>TOTAL</b></td><td width='8%'>" + Total1 + "</td><td width='8%' align='center'>" + Total2 + "</td><td width='8%' align='center'>" + Total3 + "</td></tr>";
                    szHtml = szHtml + "</table>";
                }
            }
            else
            {


                for (int k = 0; k < DsFinal.Tables[1].Rows.Count; k++)
                {


                    szHtml = szHtml + "<tr><td width='28%'>" + DsFinal.Tables[1].Rows[k][0].ToString() + "</td><td width='8%' align='center'>" + DsFinal.Tables[1].Rows[k][10] + "</td><td width='8%' align='center'>" + DsFinal.Tables[1].Rows[k][11] + "</td><td width='8%' align='center'>" + DsFinal.Tables[1].Rows[k][12] + "</td></tr>";



                }
                szHtml = szHtml + "<tr><td width='28%'><b>TOTAL</b></td><td align='center' width='8%'>" + Total1 + "</td><td width='8%' align='center'>" + Total2 + "</td><td width='8%' align='center'>" + Total3 + "</td></tr>";
                szHtml = szHtml + "</table> <br/>";
            }
            szHtml = szHtml + "<font size='2'><div ALIGN=LEFT>Total Visit Completed–- &nbsp;" + TotalVC + "<br/> Total No Show-&nbsp;" + TotalNS + "<br/> TOtal Re schedule- &nbsp;" + TotalRS + "</div></font><span style='page-break-before: always;'>";
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count == 1)
            {
                int i = 0;
                if (ds.Tables[0].Rows.Count == 1)
                {



                    //psz   szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div><br/></font>";
                    szHtml = szHtml + "<font size='2'><b>" + ds.Tables[0].Rows[i][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[i][2].ToString() + "<br/>" + ds.Tables[0].Rows[i][3].ToString() + "," + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][5].ToString() + "<br/>Tel:&nbsp;" + phoneformat(ds.Tables[0].Rows[i][13].ToString()) + ",&nbsp;FAX:&nbsp;" + phoneformat(ds.Tables[0].Rows[i][14].ToString()) + ",&nbsp;Email:&nbsp;" + ds.Tables[0].Rows[i][15].ToString() + "<br/><br/></font>";

                    // szHtml = szHtml + "<table width='100%'><tr><td width='100%'>&nbsp;</td></tr>";pppp11
                    szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='24%' style='font-weight:bold'>PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%'>Referring Doctor</td><td width='5%' style='font-weight:bold' >Date Of Visit</td><td width='8%' style='font-weight:bold' >Insurance Name</td><td width='8%'>Claim Number</td><td width='8%'>Case Type</td><td width='9%' style='font-weight:bold' >Date Of Accident</td><td width='17%' style='font-weight:bold'  align='center' colspan='3' >Treatment Codes</td><td width='11%' style='font-weight:bold' >Status</td></tr>";
                    szHtml = szHtml + "<tr><td width='24%' style='font-weight:bold'>&nbsp;</td><td width='7%'>&nbsp;</td><td width='5%' style='font-weight:bold' >&nbsp;</td><td width='8%' style='font-weight:bold' >&nbsp;</td><td width='8%'>&nbsp;</td><td width='8%'>&nbsp;</td><td width='9%' style='font-weight:bold' ></td><td  style='border-right-width:1; font-weight:bold' width='5%' align='center'  >Upper</td><td style='border-right-width:1; font-weight:bold' width='5%' align='center'   > Lower</td><td  style='font-weight:bold' width='5%' align='center'   >Consult</td><td width='11%' style='font-weight:bold' >&nbsp;</td></tr>";
                    szHtml = szHtml + "<tr><td width='24%'>" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='7%'>" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='5%'>" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_CLAIM_NUMBER"].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_Case_type"].ToString() + "</td><td width='9%'>" + ds.Tables[0].Rows[i][10].ToString() + " </td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[1].Rows[i]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_NORMAL"].ToString() + "</td> <td width='11%'>" + ds.Tables[0].Rows[i]["sz_notes"].ToString() + "</td></tr>";
                    ///e


                    szHtml = szHtml + "<tr><td width='24%'><b>Total : " + ds.Tables[0].Rows.Count.ToString() + "</td><td width='45%' colspan='6'>&nbsp;</td><td  width='5%' align='center' >" + ds.Tables[2].Rows[0]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[2].Rows[0]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[2].Rows[0]["SZ_consult"].ToString() + "</td> <td width='11%'>&nbsp;</td></tr></table> ";



                    szHtml = szHtml + "</table>";
                    ICount++;
                }
                else
                {
                    // szHtml = szHtml + "<font size='1'><b><div ALIGN=CENTER>" + szCmp + "<br/>" + szAddress + "<br/>" + szPhoneFax + "</DIV><br/> </b></font>";
                    //psz      szHtml = szHtml + "<font size='2'><b><div ALIGN=CENTER>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div><br/></font>";
                    //atulj1
                    //szHtml = szHtml + "<table width='100% border = '1''><tr><td width='25%' style='font-weight:bold'>PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%' style='font-weight:bold'>Referring Doctor</td><td width='8%' style='font-weight:bold' >Date Of Visit/Time Of Visit</td><td width='9%' style='font-weight:bold'>Insurance Name<br/>[Claim Number]</td><td width='10%' style='font-weight:bold'>Date Of Accident</td><td width='25%' style='font-weight:bold'>Treatment Codes</td><td width='14%' style='font-weight:bold'>Next Appointmen</td></tr>";
                    szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='24%' style='font-weight:bold'>PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%'>Referring Doctor</td><td width='5%' style='font-weight:bold' >Date Of Visit</td><td width='8%' style='font-weight:bold' >Insurance Name</td><td width='8%'>Claim Number</td><td width='8%'>Case Type</td><td width='9%' style='font-weight:bold' >Date Of Accident</td><td width='17%' style='font-weight:bold'  align='center' colspan='3' >Treatment Codes</td><td width='11%' style='font-weight:bold' >Status</td></tr>";
                    szHtml = szHtml + "<tr><td width='24%' style='font-weight:bold'>&nbsp;</td><td width='7%'>&nbsp;</td><td width='5%' style='font-weight:bold' >&nbsp;</td><td width='8%' style='font-weight:bold' >&nbsp;</td><td width='8%'>&nbsp;</td><td width='8%'>&nbsp;</td><td width='9%' style='font-weight:bold' ></td><td  style='border-right-width:1; font-weight:bold' width='5%' align='center'>Upper</td><td style='border-right-width:1; font-weight:bold' width='5%' align='center'   > Lower</td><td  style='font-weight:bold' width='5%' align='center'   >Consult</td><td width='11%' style='font-weight:bold' >&nbsp;</td></tr>";
                    // szHtml = szHtml + "</table> ";

                    //  szHtml = szHtml + "<h6>MRI -show(0), NS(0) , RS(0) , Total-0 &nbsp; &nbsp; &nbsp; &nbsp;X-RAY -show(0), NS(0) , RS(0) , Total-0 &nbsp; &nbsp; &nbsp; &nbsp;CT-SCAN -show(0), NS(0) , RS(0) , Total-0 <br/>Total -show(0), NS(0) , RS(0) , Total-0  </h6>";
                    szHtml = szHtml + "<tr><td width='24%'><b>Total : 0</td><td width='45%' colspan='6'>&nbsp;</td><td  width='5%' align='center' >0</td><td  width='5%' align='center' >0</td><td  width='5%' align='center' >0</td> <td width='11%'>&nbsp;</td></tr></table>";

                }

            }
            else
            {
                int recordcount = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {


                        //psz   szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div><br/></font>";
                        szHtml = szHtml + "<font size='2'><b>" + ds.Tables[0].Rows[i][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[i][2].ToString() + "<br/>" + ds.Tables[0].Rows[i][3].ToString() + "," + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][5].ToString() + "<br/>Tel:&nbsp;" + phoneformat(ds.Tables[0].Rows[i][13].ToString()) + ",&nbsp;FAX:&nbsp;" + phoneformat(ds.Tables[0].Rows[i][14].ToString()) + ",&nbsp;Email:&nbsp;" + ds.Tables[0].Rows[i][15].ToString() + "<br/><br/></font>";
                        //szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='25%' style='font-weight:bold'>PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%'>Referring Doctor</td><td width='8%' style='font-weight:bold' >Date Of Visit/Time Of Visit</td><td width='9%' style='font-weight:bold' >Insurance Name<br/>[Claim Number]</td><td width='10%' style='font-weight:bold' >Date Of Accident</td><td width='25%' style='font-weight:bold' >Treatment Codes</td><td width='14%' style='font-weight:bold' >Next Appointmen</td></tr>";
                        //szHtml = szHtml + "<tr><td width='25%'>" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='9%'>" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='10%'>" + ds.Tables[0].Rows[i][10].ToString() + " </td><td width='25%'>" + ds.Tables[0].Rows[i][11].ToString() + "</td><td width='14%'>" + ds.Tables[0].Rows[i][12].ToString() + "</td></tr>";

                        szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='24%' style='font-weight:bold'>PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%'>Referring Doctor</td><td width='5%' style='font-weight:bold' >Date Of Visit</td><td width='8%' style='font-weight:bold' >Insurance Name</td><td width='8%'>Claim Number</td><td width='8%'>Case Type</td><td width='9%' style='font-weight:bold' >Date Of Accident</td><td width='17%' style='font-weight:bold'  align='center' colspan='3' >Treatment Codes</td><td width='11%' style='font-weight:bold' >Status</td></tr>";
                        szHtml = szHtml + "<tr><td width='24%' style='font-weight:bold'>&nbsp;</td><td width='7%'>&nbsp;</td><td width='5%' style='font-weight:bold' >&nbsp;</td><td width='8%' style='font-weight:bold' >&nbsp;</td><td width='8%'>&nbsp;</td><td width='8%'>&nbsp;</td><td width='9%' style='font-weight:bold' ></td><td  style='border-right-width:1; font-weight:bold' width='5%' align='center'  >Upper</td><td style='border-right-width:1; font-weight:bold' width='5%' align='center'   > Lower</td><td  style='font-weight:bold' width='5%' align='center'   >Consult</td><td width='11%' style='font-weight:bold' >&nbsp;</td></tr>";
                        szHtml = szHtml + "<tr><td width='24%'>" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='7%'>" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='5%'>" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_CLAIM_NUMBER"].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_Case_type"].ToString() + "</td><td width='9%'>" + ds.Tables[0].Rows[i][10].ToString() + " </td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[1].Rows[i]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_NORMAL"].ToString() + "</td> <td width='11%'>" + ds.Tables[0].Rows[i]["sz_notes"].ToString() + "</td></tr>";


                        count++;

                    }
                    else if (ds.Tables[0].Rows[i - 1][0].ToString().Equals(ds.Tables[0].Rows[i][0].ToString()) && i == ds.Tables[0].Rows.Count - 1)
                    {
                        if (count < IRecoreds)
                        {

                            szHtml = szHtml + "<tr><td width='24%'>" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='7%'>" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='5%'>" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_CLAIM_NUMBER"].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_Case_type"].ToString() + "</td><td width='9%'>" + ds.Tables[0].Rows[i][10].ToString() + " </td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[1].Rows[i]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_NORMAL"].ToString() + "</td> <td width='11%'>" + ds.Tables[0].Rows[i]["sz_notes"].ToString() + "</td></tr>";

                            //psz                            szHtml = szHtml + "</table> <table width='100%' border = '1'><tr><td width='33%'>" + DsFinal.Tables[0].Rows[ICount][0].ToString() + "</td><td width='33%'>" + DsFinal.Tables[0].Rows[ICount][1].ToString() + "</td><td width='33%'>" + DsFinal.Tables[0].Rows[ICount][2].ToString() + "</td></tr><tr><td colspan='3'>" + DsFinal.Tables[0].Rows[ICount][5].ToString() + "</td></tr></table>";

                            if (ds.Tables[0].Rows.Count == recordcount)
                            {
                                szHtml = szHtml + " <tr><td width='24%'><b>Total : " + ds.Tables[0].Rows.Count.ToString() + "</td><td width='45%' colspan='6'>&nbsp;</td><td  width='5%' align='center' >" + ds.Tables[2].Rows[0]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[2].Rows[0]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[2].Rows[0]["SZ_consult"].ToString() + "</td> <td width='11%'>&nbsp;</td></tr></table>";
                            }
                            else
                            {
                                szHtml = szHtml + "</table>";
                            }

                            szHtml = szHtml + "</table>";
                            ICount++;
                        }
                        else
                        {
                            count = 1;
                            szHtml = szHtml + "</table><span style='page-break-before: always;'>";
                            //psz  szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div></font>";
                            szHtml = szHtml + "<font size='2'><b>" + ds.Tables[0].Rows[i][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[i][2].ToString() + "<br/>" + ds.Tables[0].Rows[i][3].ToString() + "," + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][5].ToString() + "<br/>Tel:&nbsp;" + phoneformat(ds.Tables[0].Rows[i][13].ToString()) + ",&nbsp;FAX:&nbsp;" + phoneformat(ds.Tables[0].Rows[i][14].ToString()) + ",&nbsp;Email:&nbsp;" + ds.Tables[0].Rows[i][15].ToString() + "<br/><br/></font>";

                            szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='24%' style='font-weight:bold'>PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%'>Referring Doctor</td><td width='5%' style='font-weight:bold' >Date Of Visit</td><td width='8%' style='font-weight:bold' >Insurance Name</td><td width='8%'>Claim Number</td><td width='8%'>Case Type</td><td width='9%' style='font-weight:bold' >Date Of Accident</td><td width='17%' style='font-weight:bold'  align='center' colspan='3' >Treatment Codes</td><td width='11%' style='font-weight:bold' >Status</td></tr>";
                            szHtml = szHtml + "<tr><td width='24%' style='font-weight:bold'>&nbsp;</td><td width='7%'>&nbsp;</td><td width='5%' style='font-weight:bold' >&nbsp;</td><td width='8%' style='font-weight:bold' >&nbsp;</td><td width='8%'>&nbsp;</td><td width='8%'>&nbsp;</td><td width='9%' style='font-weight:bold' ></td><td  style='border-right-width:1; font-weight:bold' width='5%' align='center'  >Upper</td><td style='border-right-width:1; font-weight:bold' width='5%' align='center'   > Lower</td><td  style='font-weight:bold' width='5%' align='center'   >Consult</td><td width='11%' style='font-weight:bold' >&nbsp;</td></tr>";
                            szHtml = szHtml + "<tr><td width='24%'>" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='7%'>" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='5%'>" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_CLAIM_NUMBER"].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_Case_type"].ToString() + "</td><td width='9%'>" + ds.Tables[0].Rows[i][10].ToString() + " </td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[1].Rows[i]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_NORMAL"].ToString() + "</td> <td width='11%'>" + ds.Tables[0].Rows[i]["sz_notes"].ToString() + "</td></tr>";
                            // psz  szHtml = szHtml + "</table> <table width='100%' border = '1'><tr><td width='33%'>" + DsFinal.Tables[0].Rows[ICount][0].ToString() + "</td><td width='33%'>" + DsFinal.Tables[0].Rows[ICount][1].ToString() + "</td><td width='33%'>" + DsFinal.Tables[0].Rows[ICount][2].ToString() + "</td></tr><tr><td colspan='3'>" + DsFinal.Tables[0].Rows[ICount][5].ToString() + "</td></tr></table>";
                            if (ds.Tables[0].Rows.Count == recordcount)
                            {
                                szHtml = szHtml + " <tr><td width='24%'><b>Total : " + ds.Tables[0].Rows.Count.ToString() + "</td><td width='45%' colspan='6'>&nbsp;</td><td  width='5%' align='center' >" + ds.Tables[2].Rows[0]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[2].Rows[0]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[2].Rows[0]["SZ_consult"].ToString() + "</td> <td width='11%'>&nbsp;</td></tr></table>";
                            }
                            else
                            {
                                szHtml = szHtml + "</table>";
                            }


                            ICount++;

                        }
                    }
                    else if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        count = 1;
                        //psz                        szHtml = szHtml + "</table> <h6>" + DsFinal.Tables[0].Rows[ICount][0].ToString() + "&nbsp;&nbsp;" + DsFinal.Tables[0].Rows[ICount][1].ToString() + "&nbsp;&nbsp;" + DsFinal.Tables[0].Rows[ICount][2].ToString() + "&nbsp;&nbsp;" + DsFinal.Tables[0].Rows[ICount][5].ToString() + "</h6> <span style='page-break-before: always;'>";
                        szHtml = szHtml + " <tr><td width='24%'><b>Total : " + ds.Tables[0].Rows.Count.ToString() + "</td><td width='45%' colspan='6'>&nbsp;</td><td  width='5%' align='center' >" + ds.Tables[2].Rows[0]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[2].Rows[0]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[2].Rows[0]["SZ_consult"].ToString() + "</td> <td width='11%'>&nbsp;</td></tr></table> <span style='page-break-before: always;'>";

                        //psz szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div><br/></font>";

                        szHtml = szHtml + "<font size='2'><b>" + ds.Tables[0].Rows[i][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[i][2].ToString() + "<br/>" + ds.Tables[0].Rows[i][3].ToString() + "," + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][5].ToString() + "<br/>Tel:&nbsp;" + phoneformat(ds.Tables[0].Rows[i][13].ToString()) + ",&nbsp;FAX:&nbsp;" + phoneformat(ds.Tables[0].Rows[i][14].ToString()) + ",&nbsp;Email:&nbsp;" + ds.Tables[0].Rows[i][15].ToString() + "<br/></font>";



                        szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='24%' style='font-weight:bold'>PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%'>Referring Doctor</td><td width='5%' style='font-weight:bold' >Date Of Visit</td><td width='8%' style='font-weight:bold' >Insurance Name</td><td width='8%'>Claim Number</td><td width='8%'>Case Type</td><td width='9%' style='font-weight:bold' >Date Of Accident</td><td width='17%' style='font-weight:bold'  align='center' colspan='3' >Treatment Codes</td><td width='11%' style='font-weight:bold' >Status</td></tr>";
                        szHtml = szHtml + "<tr><td width='24%' style='font-weight:bold'>&nbsp;</td><td width='7%'>&nbsp;</td><td width='5%' style='font-weight:bold' >&nbsp;</td><td width='8%' style='font-weight:bold' >&nbsp;</td><td width='8%'>&nbsp;</td><td width='8%'>&nbsp;</td><td width='9%' style='font-weight:bold' ></td><td  style='border-right-width:1; font-weight:bold' width='5%' align='center'  >Upper</td><td style='border-right-width:1; font-weight:bold' width='5%' align='center'   > Lower</td><td  style='font-weight:bold' width='5%' align='center'   >Consult</td><td width='11%' style='font-weight:bold' >&nbsp;</td></tr>";
                        szHtml = szHtml + "<tr><td width='24%'>" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='7%'>" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='5%'>" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_CLAIM_NUMBER"].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_Case_type"].ToString() + "</td><td width='9%'>" + ds.Tables[0].Rows[i][10].ToString() + " </td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[1].Rows[i]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_NORMAL"].ToString() + "</td> <td width='11%'>" + ds.Tables[0].Rows[i]["sz_notes"].ToString() + "</td></tr>";

                        //szHtml = szHtml + "</table> <h6>" + DsFinal.Tables[0].Rows[ICount][0].ToString() + "&nbsp;&nbsp;" + DsFinal.Tables[0].Rows[ICount][1].ToString() + "&nbsp;&nbsp;" + DsFinal.Tables[0].Rows[ICount][2].ToString() + "&nbsp;&nbsp;" + DsFinal.Tables[0].Rows[ICount][5].ToString() + "</h6> <span style='page-break-before: always;'>";

                        //psz szHtml = szHtml + "</table> <table width='100%' border = '1'><tr><td width='33%'>" + DsFinal.Tables[0].Rows[ICount][0].ToString() + "</td><td width='33%'>" + DsFinal.Tables[0].Rows[ICount][1].ToString() + "</td><td width='33%'>" + DsFinal.Tables[0].Rows[ICount][2].ToString() + "</td></tr><tr><td colspan='3'>" + DsFinal.Tables[0].Rows[ICount][5].ToString() + "</td></tr></table><span style='page-break-before: always;'>";
                        if (ds.Tables[0].Rows.Count == recordcount)
                        {
                            szHtml = szHtml + "<tr><td width='24%'><b>Total : " + ds.Tables[0].Rows.Count.ToString() + "</td><td width='45%' colspan='6'>&nbsp;</td><td  width='5%' align='center' >" + ds.Tables[2].Rows[0]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[2].Rows[0]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[2].Rows[0]["SZ_consult"].ToString() + "</td> <td width='11%'>&nbsp;</td></tr></table> <span style='page-break-before: always;'>";
                        }
                        else
                        {
                            szHtml = szHtml + "</table> <span style='page-break-before: always;'>";
                        }

                        ICount++;
                    }
                    else if (i != ds.Tables[0].Rows.Count - 1)
                    {
                        if (ds.Tables[0].Rows[i - 1][0].ToString().Equals(ds.Tables[0].Rows[i][0].ToString()))
                        {
                            if (count < IRecoreds)
                            {

                                count++;
                                //szHtml = szHtml + "<tr><td width='25%'>" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='9%'>" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='10%'>" + ds.Tables[0].Rows[i][10].ToString() + " </td><td width='25%'>" + ds.Tables[0].Rows[i][11].ToString() + "</td><td width='14%'>" + ds.Tables[0].Rows[i][12].ToString() + "</td></tr>";
                                szHtml = szHtml + "<tr><td width='24%'>" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='7%'>" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='5%'>" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_CLAIM_NUMBER"].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_Case_type"].ToString() + "</td><td width='9%'>" + ds.Tables[0].Rows[i][10].ToString() + " </td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[1].Rows[i]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_NORMAL"].ToString() + "</td> <td width='11%'>" + ds.Tables[0].Rows[i]["sz_notes"].ToString() + "</td></tr>";
                            }
                            else
                            {
                                count = 1;
                                szHtml = szHtml + "</table><span style='page-break-before: always;'>";

                                //psz   szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div></font>";

                                szHtml = szHtml + "<font size='2'><b>" + ds.Tables[0].Rows[i][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[i][2].ToString() + "<br/>" + ds.Tables[0].Rows[i][3].ToString() + "," + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][5].ToString() + "<br/>Tel:&nbsp;" + phoneformat(ds.Tables[0].Rows[i][13].ToString()) + ",&nbsp;FAX:&nbsp;" + phoneformat(ds.Tables[0].Rows[i][14].ToString()) + ",&nbsp;Email:&nbsp;" + ds.Tables[0].Rows[i][15].ToString() + "<br/><br/></font>";


                                szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='24%' style='font-weight:bold'>PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%'>Referring Doctor</td><td width='5%' style='font-weight:bold' >Date Of Visit</td><td width='8%' style='font-weight:bold' >Insurance Name</td><td width='8%'>Claim Number</td><td width='8%'>Case Type</td><td width='9%' style='font-weight:bold' >Date Of Accident</td><td width='17%' style='font-weight:bold'  align='center' colspan='3' >Treatment Codes</td><td width='11%' style='font-weight:bold' >Status</td></tr>";
                                szHtml = szHtml + "<tr><td width='24%' style='font-weight:bold'>&nbsp;</td><td width='7%'>&nbsp;</td><td width='5%' style='font-weight:bold' >&nbsp;</td><td width='8%' style='font-weight:bold' >&nbsp;</td><td width='8%'>&nbsp;</td><td width='8%'>&nbsp;</td><td width='9%' style='font-weight:bold' ></td><td  style='border-right-width:1; font-weight:bold' width='5%' align='center'  >Upper</td><td style='border-right-width:1; font-weight:bold' width='5%' align='center'   > Lower</td><td  style='font-weight:bold' width='5%' align='center'   >Consult</td><td width='11%' style='font-weight:bold' >&nbsp;</td></tr>";
                                szHtml = szHtml + "<tr><td width='24%'>" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='7%'>" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='5%'>" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_CLAIM_NUMBER"].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_Case_type"].ToString() + "</td><td width='9%'>" + ds.Tables[0].Rows[i][10].ToString() + " </td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[1].Rows[i]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_NORMAL"].ToString() + "</td> <td width='11%'>" + ds.Tables[0].Rows[i]["sz_notes"].ToString() + "</td></tr>";


                            }
                        }
                        else
                        {


                            //psz szHtml = szHtml + "</table> <table width='100%' border = '1'><tr><td width='33%'>" + DsFinal.Tables[0].Rows[ICount][0].ToString() + "</td><td width='33%'>" + DsFinal.Tables[0].Rows[ICount][1].ToString() + "</td><td width='33%'>" + DsFinal.Tables[0].Rows[ICount][2].ToString() + "</td></tr><tr><td colspan='3'>" + DsFinal.Tables[0].Rows[ICount][5].ToString() + "</td></tr></table><span style='page-break-before: always;'>";
                            if (ds.Tables[0].Rows.Count == recordcount)
                            {
                                szHtml = szHtml + "<tr><td width='24%'><b>Total : " + ds.Tables[0].Rows.Count.ToString() + "</td><td width='45%' colspan='6'>&nbsp;</td><td  width='5%' align='center' >" + ds.Tables[2].Rows[0]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[2].Rows[0]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[2].Rows[0]["SZ_consult"].ToString() + "</td> <td width='11%'>&nbsp;</td></tr></table><span style='page-break-before: always;'>";
                            }
                            else
                            {
                                szHtml = szHtml + "</table><span style='page-break-before: always;'>";
                            }
                            ICount++;
                            count = 1;
                            //psz    szHtml = szHtml + "<font size='2'><div ALIGN=CENTER><b>" + szCmp + "</b><br/>" + szAddress + "<br/>" + szCityStateZip + "<br/>" + szPhoneFax + "</div><br/></font>";

                            szHtml = szHtml + "<font size='2'><b>" + ds.Tables[0].Rows[i][1].ToString() + "</b><br/>" + ds.Tables[0].Rows[i][2].ToString() + "<br/>" + ds.Tables[0].Rows[i][3].ToString() + "," + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][4].ToString() + "&nbsp;&nbsp;" + ds.Tables[0].Rows[i][5].ToString() + "<br/>Tel:&nbsp;" + phoneformat(ds.Tables[0].Rows[i][13].ToString()) + ",&nbsp;FAX:&nbsp;" + phoneformat(ds.Tables[0].Rows[i][14].ToString()) + ",&nbsp;Email:&nbsp;" + ds.Tables[0].Rows[i][15].ToString() + "<br/><br/></font>";


                            szHtml = szHtml + "<table width='100%' border = '1' ><tr><td width='24%' style='font-weight:bold'>PatientName<br/>(Chart Number)<br/>[Patient Phone]</td><td width='8%'>Referring Doctor</td><td width='5%' style='font-weight:bold' >Date Of Visit</td><td width='8%' style='font-weight:bold' >Insurance Name</td><td width='8%'>Claim Number</td><td width='8%'>Case Type</td><td width='9%' style='font-weight:bold' >Date Of Accident</td><td width='17%' style='font-weight:bold'  align='center' colspan='3' >Treatment Codes</td><td width='11%' style='font-weight:bold' >Status</td></tr>";
                            szHtml = szHtml + "<tr><td width='24%' style='font-weight:bold'>&nbsp;</td><td width='7%'>&nbsp;</td><td width='5%' style='font-weight:bold' >&nbsp;</td><td width='8%' style='font-weight:bold' >&nbsp;</td><td width='8%'>&nbsp;</td><td width='8%'>&nbsp;</td><td width='9%' style='font-weight:bold' ></td><td  style='border-right-width:1; font-weight:bold' width='5%' align='center'  >Upper</td><td style='border-right-width:1; font-weight:bold' width='5%' align='center'   > Lower</td><td  style='font-weight:bold' width='5%' align='center'   >Consult</td><td width='11%' style='font-weight:bold' >&nbsp;</td></tr>";
                            szHtml = szHtml + "<tr><td width='24%'>" + ds.Tables[0].Rows[i][6].ToString() + "</td><td width='7%'>" + ds.Tables[0].Rows[i][7].ToString() + "</td><td width='5%'>" + ds.Tables[0].Rows[i][8].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i][9].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_CLAIM_NUMBER"].ToString() + "</td><td width='8%'>" + ds.Tables[0].Rows[i]["SZ_Case_type"].ToString() + "</td><td width='9%'>" + ds.Tables[0].Rows[i][10].ToString() + " </td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_UPPER"].ToString() + "</td><td  width='5%' align='center' > " + ds.Tables[1].Rows[i]["SZ_LOWER"].ToString() + "</td><td  width='5%' align='center' >" + ds.Tables[1].Rows[i]["SZ_NORMAL"].ToString() + "</td> <td width='11%'>" + ds.Tables[0].Rows[i]["sz_notes"].ToString() + "</td></tr>";

                        }


                    }
                    recordcount++;
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        szLine = szLine.Replace("SHOWREPORT", szHtml);
        Bill_Sys_NF3_Template _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
        string szPhisicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + "Reports/";
        string path = GeneratePDF(szLine, szFileName, szPhisicalPath);
        string open_Path = ApplicationSettings.GetParameterValue("DocumentManagerURL") + "Reports/" + path;
        string Mopen_path = szPhisicalPath + path;
        if (iFlag == 1)
        {
            return Mopen_path;
        }
        else
        {
            return open_Path;
        }


    }

    public DataSet GetOfficeWisePatientInfoEMG(string P_Company_Id, string P_StartDate, string P_EndDate, string P_OfficeId, string P_DocorId, string P_Status)
    {
        ds = new DataSet();
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("GET_OFFICEWISE_SHOW_REPORT_EMG", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            if (P_StartDate != "" && P_StartDate != "NA") { sqlCmd.Parameters.AddWithValue("@DT_START_DATE", P_StartDate); }
            if (P_EndDate != "" && P_EndDate != "NA") { sqlCmd.Parameters.AddWithValue("@DT_END_DATE", P_EndDate); }
            if (P_OfficeId != "" && P_OfficeId != "NA" && P_OfficeId != "--- Select ---") { sqlCmd.Parameters.AddWithValue("@SZ_OFFICE_ID", P_OfficeId); }
            if (P_DocorId != "" && P_DocorId != "NA" && P_DocorId != "---Select---") { sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", P_DocorId); }
            if (P_Status != "" && P_Status != "NA") { sqlCmd.Parameters.AddWithValue("@I_STATUS", P_Status); }
            sqlda = new SqlDataAdapter(sqlCmd);

            sqlda.Fill(ds);

            DataTable objDT = new DataTable();
            objDT.Columns.Add("SZ_UPPER");
            objDT.Columns.Add("SZ_LOWER");
            objDT.Columns.Add("SZ_NORMAL");
            DataTable objcount = new DataTable();
            objcount.Columns.Add("SZ_UPPER");
            objcount.Columns.Add("SZ_LOWER");
            objcount.Columns.Add("SZ_CONSULT");
            int UCount = 0;
            int LCount = 0;
            int CCount = 0;
            Boolean uflag = false;
            Boolean lflag = false;
            Boolean cflag = false;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string szColval = ds.Tables[0].Rows[i]["SZ_PROC_CODE"].ToString();
                string[] str = szColval.Split(',');
                string szUpper = "", szLower = "", szNormal = "";

                DataRow dr;
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j].ToString() != "")
                    {
                        if (str[j].ToString().ToLower().Contains("(lower)") || str[j].ToString().ToLower().Contains("(upper & lower)"))
                        {
                            szLower = szLower + " " + "X";
                            if (!lflag)
                            {
                                LCount++;
                                lflag = true;
                            }
                        }
                        if (str[j].ToString().ToLower().Contains("(upper)") || str[j].ToString().ToLower().Contains("(upper & lower)"))
                        {
                            szUpper = szUpper + " " + "X";
                            if (!uflag)
                            {
                                UCount++;
                                uflag = true;
                            }
                        }
                        else if (!str[j].ToString().ToLower().Contains("(lower)") && !str[j].ToString().ToLower().Contains("(upper & lower)"))
                        {
                            szNormal = szNormal + " " + "X";
                            if (!cflag)
                            {
                                CCount++;
                                cflag = true;
                            }
                        }
                    }

                }
                dr = objDT.NewRow();
                dr["SZ_UPPER"] = szUpper;
                dr["SZ_LOWER"] = szLower;
                dr["SZ_NORMAL"] = szNormal;
                objDT.Rows.Add(dr);
                cflag = false;
                lflag = false;
                uflag = false;

                //string szFinalVal = "<table width='100%'  style=' height:100%;' border='1' valign='top'><tr><td width='33%'>" + szUpper + "</td><td width='33%'  >" + szLower + "</td><td width='33%' >" + szNormal + "</td></tr></table>";

                //ds.Tables[0].Rows[i]["SZ_PROC_CODE"] = szFinalVal;
            }

            ds.Tables.Add(objDT);
            DataRow drcount = objcount.NewRow();
            drcount["SZ_UPPER"] = UCount.ToString();
            drcount["SZ_LOWER"] = LCount.ToString();
            drcount["SZ_CONSULT"] = CCount.ToString();
            objcount.Rows.Add(drcount);
            ds.Tables.Add(objcount);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return ds;
    }

    private string phoneformat(string num)
    {
        num = num.Replace("(", "").Replace(")", "").Replace("-", "");
        string result = string.Empty;
        string phoneformat = @"(\d{3})(\d{3})(\d{4})";
        result = Regex.Replace(num, phoneformat, "$1-$2-$3");
        return result;

    }

    public DataSet GET_SPECIALTY_BILLCOUNT(string companyId)
    {
        //SqlParameter sqlParam = new SqlParameter();
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_SPECIALITY_BILLCOUNT_ALL", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyId);
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

    public void DeleteAddedEventProcId(string i_event_proc_id)
    {
        try
        {

            sqlCon = new SqlConnection(strsqlCon);
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_DELETE_EVENT_PROC_ID_FOR_REVERT_REPORT", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@i_event_proc_id", i_event_proc_id);
            sqlCmd.ExecuteNonQuery();
            // return true;//transaction succesfull

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            sqlCon.Close();
        }
    }
    public DataSet getBillReportSpecialityLHR(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_SPECIALITY_LHR", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", objAL[7].ToString()); } // Kapil
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

    public int POMSaveOther(string P_Company_Id, string P_User_Id, int P_Pom_verification)
    {
        int i_POM_Id = 0;
        sqlCon = new SqlConnection(strsqlCon);
        SqlTransaction transaction;
        sqlCon.Open();
        transaction = sqlCon.BeginTransaction();
        try
        {

            #region "Insert POM Entry Into TXN_BILL_POM TABLE AND GET LATEST POM_ID"
            sqlCmd = new SqlCommand("SP_SAVE_TXN_BILL_POM_OTHER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_POM_DATE", DateTime.Today.ToString("MM/dd/yyyy"));
            sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", 0);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", "");
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", "");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id);
            sqlCmd.Parameters.AddWithValue("@I_POM_ID", SqlDbType.Int);
            sqlCmd.Parameters["@I_POM_ID"].Direction = ParameterDirection.ReturnValue;
            sqlCmd.Parameters.AddWithValue("@I_VERIFICATION_POM", P_Pom_verification);
            sqlCmd.ExecuteNonQuery();
            i_POM_Id = Convert.ToInt32(sqlCmd.Parameters["@I_POM_ID"].Value.ToString());

            //   sqlCmd.ExecuteNonQuery();
            #endregion "End Of Code"
            transaction.Commit();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return i_POM_Id;


    }

    public void POMEntryOther(int i_pom_id, string P_POM_Date, int ImageId, string P_File_Name, string P_File_Path, string P_Company_Id, string P_User_Id, ArrayList P_Bill_No, string P_Bill_Status)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        SqlTransaction transaction;
        sqlCon.Open();
        transaction = sqlCon.BeginTransaction();
        try
        {
            #region "Update POM Entry Into TXN_BILL_POM TABLE AND GET LATEST POM_ID"
            sqlCmd = new SqlCommand("SP_UPDATE_TXN_BILL_POM_OTHER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Transaction = transaction;
            sqlCmd.Parameters.AddWithValue("@SZ_POM_DATE", P_POM_Date);
            sqlCmd.Parameters.AddWithValue("@I_IMAGE_ID", ImageId);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", P_File_Name);
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", P_File_Path);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id);
            sqlCmd.Parameters.AddWithValue("@I_POM_ID", i_pom_id);

            sqlCmd.ExecuteNonQuery();
            #endregion "End Of Code"
            #region "Update Txn_Bill_Transation set POM_ID Against All Bill_No On Which POM is Generated."
            for (int i = 0; i < P_Bill_No.Count; i++)
            {
                sqlCmd = new SqlCommand("SP_UPDATE_TXN_BILL_TRNSACTION_POM_OTHER", sqlCon);
                sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCmd.CommandTimeout = 0;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Transaction = transaction;
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", P_Company_Id);
                sqlCmd.Parameters.AddWithValue("@I_POM_OTHER_ID", i_pom_id);
                sqlCmd.Parameters.AddWithValue("@SZ_BILL_NO", P_Bill_No[i]);
                sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", P_User_Id);
                sqlCmd.ExecuteNonQuery();
            }
            #endregion"End Of Code"


            transaction.Commit();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
    }

    public DataSet GetPomotherCaseId(string szPomId, string szCompanyId)
    {
        DataSet objDS = new DataSet();
        SqlDataAdapter ds;
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_POM_OTHER_CASE_ID ", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_POM_ID", szPomId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            ds = new SqlDataAdapter(sqlCmd);
            ds.Fill(objDS);
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
        return objDS;
    }

    public void UpdateReportPomOtherPath(string fileName, string filePath, string userId, string pomId, string recimgid, string flag)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_REPORT_POM_OTHER_PATH", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_POM_ID", pomId);
            sqlCmd.Parameters.AddWithValue("@SZ_RECEIVED_FILE_NAME", fileName);
            sqlCmd.Parameters.AddWithValue("@SZ_RECEIVED_FILE_PATH", filePath);
            sqlCmd.Parameters.AddWithValue("@SZ_RECEIVED_USER_ID", userId);
            sqlCmd.Parameters.AddWithValue("@I_RECEIVED_IMAGE_ID", recimgid);
            sqlCmd.Parameters.AddWithValue("@FLAG", flag);
            sqlCmd.ExecuteNonQuery();
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
    }


    public DataSet GetBillDetailsOther(String sz_PomId, String sz_CompanyId)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_BILL_POM_OTHER", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);
            sqlCmd.Parameters.AddWithValue("@I_POM_OTHER_ID", sz_PomId);

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);

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

    public string GetSysSettingForSort(string sz_CompanyID)
    {
        sqlCon = new SqlConnection(strsqlCon);

        string strReturn = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_sys_value_for_key", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@sz_sys_setting_key_id", "SS00056");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                strReturn = dr[0].ToString();
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
        return strReturn;
    }

    public DataSet loaduserlist(string companyid)
    {
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(connectionString);
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("sp_get_alluser", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_company_id", companyid);
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

    //update user
    public void updateuser(DataTable dt)
    {

        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(connectionString);
        try
        {

            conn.Open();
            SqlCommand comm = new SqlCommand("sp_assign_procedure_code_to_user", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            SqlParameter tblvaluetype = comm.Parameters.AddWithValue("@tt_assign_user", dt);  //Passing table value parameter
            tblvaluetype.SqlDbType = SqlDbType.Structured; // This one is used to tell ADO.NET we are passing Table value Parameter
            int result = comm.ExecuteNonQuery();
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


    public void updateEventStatus(string EventIDs, int allowBilling)
    {
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_update_event_to_billable", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@i_event_proc_id", EventIDs);
            sqlCmd.Parameters.AddWithValue("@bt_allow_billing", allowBilling);
            sqlCmd.ExecuteNonQuery();
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
    }
    public DataSet GetCaseTypeReport(ArrayList arr)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_DETAIL_PAYMENT_REPORT_BY_SPECIALITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arr[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", arr[1].ToString());
            if (arr[2].ToString() != "" && arr[2].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", arr[2].ToString()); }
            //added on 24/12/2014
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", arr[3].ToString());
            //added on 24/12/2014

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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
    public DataSet getProviderBills(ArrayList objAL)
    {
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("sp_get_bill_report_specialty_by_provider", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", objAL[0].ToString());
            if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@sz_bill_status_id", objAL[1].ToString()); }
            if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", objAL[2].ToString()); }
            if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@dt_from_bill_date", objAL[3].ToString()); }
            if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@dt_to_bill_date", objAL[4].ToString()); }
            if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@sz_user_id", objAL[5].ToString()); }
            if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@sz_location_id", objAL[6].ToString()); }
            if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "") { sqlCmd.Parameters.AddWithValue("@sz_case_type_id", objAL[7].ToString()); } // Kapil
                                                                                                                                                       //Kiran :: 29 Aug
            if (objAL[8].ToString() != "NA" && objAL[8].ToString() != "") { sqlCmd.Parameters.AddWithValue("@sz_case_no", objAL[8].ToString()); } // Kapil
            if (objAL[9].ToString() != "NA" && objAL[9].ToString() != "") { sqlCmd.Parameters.AddWithValue("@sz_bill_no", objAL[9].ToString()); } // Kapil
            if (objAL[10].ToString() != "NA" && objAL[10].ToString() != "") { sqlCmd.Parameters.AddWithValue("@sz_patient_name", objAL[10].ToString()); } // Kapil
                                                                                                                                                          //Nirmalkumar 14 nov
            if (objAL[11].ToString() != "NA" && objAL[11].ToString() != "") { sqlCmd.Parameters.AddWithValue("@dt_from_visit_date", objAL[11].ToString()); }
            if (objAL[12].ToString() != "NA" && objAL[12].ToString() != "") { sqlCmd.Parameters.AddWithValue("@dt_to_visit_date", objAL[12].ToString()); }
            if (objAL[13].ToString() != "NA" && objAL[13].ToString() != "") { sqlCmd.Parameters.AddWithValue("@sz_provider_id", objAL[13].ToString()); }
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

}
