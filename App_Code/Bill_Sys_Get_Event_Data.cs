using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Globalization;

/// <summary>
/// Summary description for Bill_Sys_Get_Event_Data
/// </summary>
public class Bill_Sys_Get_Event_Data
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;

	public Bill_Sys_Get_Event_Data()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public DataSet Getvisits(string szComapnyID, string szProcedureGroupId, string szDoctorID, string szFromDate, string szToDate,string szLocation)
    {
        DataSet objDS = new DataSet();

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_INITAL_VISITS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMAPNY_ID", szComapnyID);
            sqlCmd.Parameters.AddWithValue("@PROCEDURE_GROUP_ID", szProcedureGroupId);
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", szDoctorID);
            sqlCmd.Parameters.AddWithValue("@FROM_DATE", szFromDate);
            sqlCmd.Parameters.AddWithValue("@TO_DATE", szToDate);
            sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", szLocation);
            SqlDataAdapter objDA = new SqlDataAdapter(sqlCmd);
            objDA.Fill(objDS);

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
        return objDS;
    }

    public DataTable GetAllvisits(DataSet dsEvents)
    {

        DataTable dtAllVisits = new DataTable();
        dtAllVisits.Columns.Add("SZ_CASE_ID");
        dtAllVisits.Columns.Add("SZ_PATIENT_ID");
        dtAllVisits.Columns.Add("SZ_DOCTOR_ID");

        dtAllVisits.Columns.Add("SZ_PROCEDURE_GROUP_ID");
        dtAllVisits.Columns.Add("SZ_CASE_NO");
        dtAllVisits.Columns.Add("PATIENT_NAME");
        dtAllVisits.Columns.Add("DT_EVENT_DATE");
        dtAllVisits.Columns.Add("DT_NEXT_VISIT_DATE");
        dtAllVisits.Columns.Add("SZ_PROCEDURE_GROUP");
        dtAllVisits.Columns.Add("VISIT_TYPE");
        dtAllVisits.Columns.Add("I_FOLLOWUP_TIMES");
        dtAllVisits.Columns.Add("DATE_CMP");
        dtAllVisits.Columns.Add("I_INITIAL_FOLLOWUP");
        dtAllVisits.Columns.Add("SZ_PATIENT_PHONE");
        dtAllVisits.Columns.Add("SZ_DOCTOR_NAME");
        dtAllVisits.Columns.Add("SZ_VISIT");
        dtAllVisits.Columns.Add("BT_STATUS");
        dtAllVisits.Columns.Add("I_STATUS");
        dtAllVisits.Columns.Add("I_EVENT_ID");
        dtAllVisits.Columns.Add("I_EVENT_ID_COPIED");
        dtAllVisits.Columns.Add("SZ_COMPANY_ID");
        dtAllVisits.Columns.Add("VISIT_TYPE_ID");
        

        try
        {
            dsEvents.Tables[0].Columns["DATE_CMP"].DataType = typeof(DateTime);

            for (int i = 0; i < dsEvents.Tables[0].Rows.Count; i++)
            {


                if (dsEvents.Tables[0].Rows[i]["VISIT_TYPE"].ToString().ToLower() == "c")
                {


                }
                else
                {
                    //  string filter = string.Format(CultureInfo.InvariantCulture, "DATE_CMP >= #{0:MM/dd/yyyy}#", Convert.ToDateTime(dsEvents.Tables[0].Rows[i]["DATE_CMP"].ToString()).ToString("MM/dd/yyyy"));
                    DataRow[] nextVisit = dsEvents.Tables[0].Select("SZ_CASE_ID='" + dsEvents.Tables[0].Rows[i]["SZ_CASE_ID"].ToString() + "' and  DATE_CMP > '" + dsEvents.Tables[0].Rows[i]["DATE_CMP"].ToString() + "' AND SZ_PROCEDURE_GROUP_ID='" + dsEvents.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString() + "'");

                    //DataRow[] nextVisit = dsEvents.Tables[0].Select("SZ_CASE_ID='" + dsEvents.Tables[0].Rows[i]["SZ_CASE_ID"].ToString() + "' AND SZ_PROCEDURE_GROUP_ID='" + dsEvents.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString() + "'");
                    int RowCount = nextVisit.Length + 1 + i;
                    int iTime = Convert.ToInt32(dsEvents.Tables[0].Rows[i]["I_FOLLOWUP_TIMES"].ToString());
                    if (iTime <= nextVisit.Length + 1)
                    {
                        i += nextVisit.Length;
                    }
                    else
                    {
                        int RemainTime = iTime - nextVisit.Length;
                        int iDyas = Convert.ToInt32(dsEvents.Tables[0].Rows[i]["I_INITIAL_FOLLOWUP"].ToString());
                        string LastDate = "";
                        for (int j = i; j < RowCount; j++)
                        {
                            DataRow drExisting = dtAllVisits.NewRow();
                            drExisting["SZ_CASE_ID"] = dsEvents.Tables[0].Rows[j]["SZ_CASE_ID"].ToString();
                            drExisting["SZ_PATIENT_ID"] = dsEvents.Tables[0].Rows[j]["SZ_PATIENT_ID"].ToString();
                            drExisting["SZ_DOCTOR_ID"] = dsEvents.Tables[0].Rows[j]["SZ_DOCTOR_ID"].ToString();
                            drExisting["SZ_PROCEDURE_GROUP_ID"] = dsEvents.Tables[0].Rows[j]["SZ_PROCEDURE_GROUP_ID"].ToString();
                            drExisting["SZ_CASE_NO"] = dsEvents.Tables[0].Rows[j]["SZ_CASE_NO"].ToString();
                            drExisting["PATIENT_NAME"] = dsEvents.Tables[0].Rows[j]["PATIENT_NAME"].ToString();
                            drExisting["DT_EVENT_DATE"] = dsEvents.Tables[0].Rows[j]["DT_EVENT_DATE"].ToString();
                            drExisting["VISIT_TYPE"] = dsEvents.Tables[0].Rows[j]["VISIT_TYPE"].ToString();
                            drExisting["DT_NEXT_VISIT_DATE"] = dsEvents.Tables[0].Rows[j]["DT_NEXT_VISIT_DATE"].ToString();
                            drExisting["I_FOLLOWUP_TIMES"] = dsEvents.Tables[0].Rows[j]["I_FOLLOWUP_TIMES"].ToString();
                            drExisting["DATE_CMP"] = dsEvents.Tables[0].Rows[j]["DATE_CMP"].ToString();
                            drExisting["SZ_PROCEDURE_GROUP"] = dsEvents.Tables[0].Rows[j]["SZ_PROCEDURE_GROUP"].ToString();
                            drExisting["SZ_PATIENT_PHONE"] = dsEvents.Tables[0].Rows[j]["SZ_PATIENT_PHONE"].ToString();
                            drExisting["SZ_DOCTOR_NAME"] = dsEvents.Tables[0].Rows[j]["SZ_DOCTOR_NAME"].ToString();

                            drExisting["BT_STATUS"] = dsEvents.Tables[0].Rows[j]["BT_STATUS"].ToString();
                            drExisting["I_STATUS"] = dsEvents.Tables[0].Rows[j]["I_STATUS"].ToString();
                            drExisting["I_EVENT_ID"] = dsEvents.Tables[0].Rows[j]["I_EVENT_ID"].ToString();
                            drExisting["I_EVENT_ID_COPIED"] = "";
                            drExisting["SZ_COMPANY_ID"] = dsEvents.Tables[0].Rows[j]["SZ_COMPANY_ID"].ToString();
                            drExisting["VISIT_TYPE_ID"] = "";
                            drExisting["SZ_VISIT"] = "1";
                            
                            dtAllVisits.Rows.Add(drExisting);
                            LastDate = dsEvents.Tables[0].Rows[j]["DT_NEXT_VISIT_DATE"].ToString();


                        }
                        for (int k = 0; k < RemainTime - 1; k++)
                        {
                            DateTime dtNext = Convert.ToDateTime(LastDate).AddDays(iDyas);
                            if (k == RemainTime - 2)
                            {
                                DataRow drExisting = dtAllVisits.NewRow();
                                drExisting["SZ_CASE_ID"] = dsEvents.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                                drExisting["SZ_PATIENT_ID"] = dsEvents.Tables[0].Rows[i]["SZ_PATIENT_ID"].ToString();
                                drExisting["SZ_DOCTOR_ID"] = dsEvents.Tables[0].Rows[i]["SZ_DOCTOR_ID"].ToString();
                                drExisting["SZ_PROCEDURE_GROUP_ID"] = dsEvents.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                                drExisting["SZ_CASE_NO"] = dsEvents.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                                drExisting["PATIENT_NAME"] = dsEvents.Tables[0].Rows[i]["PATIENT_NAME"].ToString();
                                drExisting["DT_EVENT_DATE"] = LastDate;
                                drExisting["VISIT_TYPE"] = "C";
                                drExisting["DT_NEXT_VISIT_DATE"] = "";
                                drExisting["I_FOLLOWUP_TIMES"] = dsEvents.Tables[0].Rows[i]["I_FOLLOWUP_TIMES"].ToString();
                                drExisting["DATE_CMP"] = dsEvents.Tables[0].Rows[i]["DATE_CMP"].ToString();
                                drExisting["SZ_PROCEDURE_GROUP"] = dsEvents.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP"].ToString();
                                drExisting["SZ_PATIENT_PHONE"] = dsEvents.Tables[0].Rows[i]["SZ_PATIENT_PHONE"].ToString();
                                drExisting["SZ_DOCTOR_NAME"] = dsEvents.Tables[0].Rows[i]["SZ_DOCTOR_NAME"].ToString();


                                drExisting["BT_STATUS"] = "";
                                drExisting["I_STATUS"] = "";
                                drExisting["I_EVENT_ID"] = "";
                                drExisting["I_EVENT_ID_COPIED"] = dsEvents.Tables[0].Rows[i]["I_EVENT_ID"].ToString();
                                drExisting["SZ_COMPANY_ID"] = dsEvents.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
                                drExisting["VISIT_TYPE_ID"] = dsEvents.Tables[0].Rows[i]["VISIT_TYPE_ID"].ToString();
                                

                                drExisting["SZ_VISIT"] = "0";


                                dtAllVisits.Rows.Add(drExisting);
                            }
                            else
                            {
                                DataRow drExisting = dtAllVisits.NewRow();
                                drExisting["SZ_CASE_ID"] = dsEvents.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                                drExisting["SZ_PATIENT_ID"] = dsEvents.Tables[0].Rows[i]["SZ_PATIENT_ID"].ToString();
                                drExisting["SZ_DOCTOR_ID"] = dsEvents.Tables[0].Rows[i]["SZ_DOCTOR_ID"].ToString();
                                drExisting["SZ_PROCEDURE_GROUP_ID"] = dsEvents.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                                drExisting["SZ_CASE_NO"] = dsEvents.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                                drExisting["PATIENT_NAME"] = dsEvents.Tables[0].Rows[i]["PATIENT_NAME"].ToString();
                                drExisting["DT_EVENT_DATE"] = LastDate;
                                drExisting["VISIT_TYPE"] = "C";
                                drExisting["DT_NEXT_VISIT_DATE"] = dtNext.ToString("MM/dd/yyyy");
                                drExisting["I_FOLLOWUP_TIMES"] = dsEvents.Tables[0].Rows[i]["I_FOLLOWUP_TIMES"].ToString();
                                drExisting["DATE_CMP"] = dsEvents.Tables[0].Rows[i]["DATE_CMP"].ToString();
                                drExisting["SZ_PROCEDURE_GROUP"] = dsEvents.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP"].ToString();
                                drExisting["SZ_PATIENT_PHONE"] = dsEvents.Tables[0].Rows[i]["SZ_PATIENT_PHONE"].ToString();
                                drExisting["SZ_DOCTOR_NAME"] = dsEvents.Tables[0].Rows[i]["SZ_DOCTOR_NAME"].ToString();

                                drExisting["BT_STATUS"] = "";
                                drExisting["I_STATUS"] = "";
                                drExisting["I_EVENT_ID"] = "";
                                drExisting["I_EVENT_ID_COPIED"] = dsEvents.Tables[0].Rows[i]["I_EVENT_ID"].ToString();
                                drExisting["SZ_COMPANY_ID"] = dsEvents.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
                                drExisting["VISIT_TYPE_ID"] = dsEvents.Tables[0].Rows[i]["VISIT_TYPE_ID"].ToString();
                                
                                
                                drExisting["SZ_VISIT"] = "0";
                                dtAllVisits.Rows.Add(drExisting);
                            }
                            LastDate = dtNext.ToString("MM/dd/yyyy");
                        }
                        i = i + nextVisit.Length;
                    } //2eles
                }//eles
            }//for
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return dtAllVisits;
    }
    public DataSet GetEventInfo(string szComapnyID, string szEventID)
    {
        DataSet objDS = new DataSet();

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_EVENT_INFO", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szComapnyID);
            sqlCmd.Parameters.AddWithValue("@SZ_EVENT_ID", szEventID);
            SqlDataAdapter objDA = new SqlDataAdapter(sqlCmd);
            objDA.Fill(objDS);

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
        return objDS;
    }

    public DataSet GetProcCode( string szEventID)
    {
        DataSet objDS = new DataSet();

        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_PROCEDURE_CODE", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@I_EVENT_ID", szEventID);
            SqlDataAdapter objDA = new SqlDataAdapter(sqlCmd);
            objDA.Fill(objDS);

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
        return objDS;
    }

   

}