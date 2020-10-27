/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill Sys Room & Holidays days
/*Purpose              :       To Add and Edit Room & Holidays days
/*Author               :       Bhilendra Y
/*Date of creation     :       29 Nov 2009
/*Modified By          :      
/*Modified Date        :       
/************************************************************/



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
/// <summary>
/// Summary description for Bill_Sys_RoomDays
/// </summary>
public class Bill_Sys_RoomDays
{
    String strConn;
    SqlConnection sqlCon;
    SqlTransaction _Transaction;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

	public Bill_Sys_RoomDays()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }


    #region Save Room Days Information
    public void saveRoomDaysInformation(DataTable objDTRoom)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_ROOM_DAYS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
                      
            sqlCmd.Parameters.Add("@RoomID", SqlDbType.NVarChar, 20);
            sqlCmd.Parameters.Add("@Days", SqlDbType.NVarChar, 20);
            sqlCmd.Parameters.Add("@StartTime", SqlDbType.Decimal);
            sqlCmd.Parameters.Add("@EndTime", SqlDbType.Decimal);
            sqlCmd.Parameters.Add("@EffectiveTo", SqlDbType.DateTime);
            sqlCmd.Parameters.Add("@EffectiveFrom", SqlDbType.DateTime);
            sqlCmd.Parameters.Add("@FLAG", SqlDbType.NVarChar, 50);
                     
            sqlCmd.Parameters["@RoomID"].SourceColumn = "RoomID";
            sqlCmd.Parameters["@Days"].SourceColumn = "Days";
            sqlCmd.Parameters["@StartTime"].SourceColumn = "StartTime";
            sqlCmd.Parameters["@EndTime"].SourceColumn = "EndTime";
            sqlCmd.Parameters["@EffectiveTo"].SourceColumn = "EffectiveTo";
            sqlCmd.Parameters["@EffectiveFrom"].SourceColumn = "EffectiveFrom";
            sqlCmd.Parameters["@FLAG"].Value = "ADD";
          

            sqlCmd.Transaction = _Transaction;

            sqlda = new SqlDataAdapter();
          
            sqlda.InsertCommand = sqlCmd;          
           
            sqlda.Update(objDTRoom);            
           
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

    #endregion

    #region Save Holiday Information
    public void saveHolidayInformation(DataTable objDTHoliday)
    {
        sqlCon = new SqlConnection(strConn);

        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_MST_ROOM_HOLIDAY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            //sqlCmd.Parameters.Add("@RoomDaysID", SqlDbType.NVarChar,20);
            sqlCmd.Parameters.Add("@HOLIDAYS_DATE", SqlDbType.DateTime);
            sqlCmd.Parameters.Add("@ROOM_ID", SqlDbType.NVarChar, 20);
            sqlCmd.Parameters.Add("@DAYS", SqlDbType.NVarChar, 20);
            sqlCmd.Parameters.Add("@FLAG", SqlDbType.NVarChar, 50);

            //sqlCmd.Parameters["@RoomDaysID"].SourceColumn = "RoomDaysID";
            sqlCmd.Parameters["@HOLIDAYS_DATE"].SourceColumn = "HOLIDAYS_DATE";
            sqlCmd.Parameters["@ROOM_ID"].SourceColumn = "ROOM_ID";
            sqlCmd.Parameters["@DAYS"].SourceColumn = "DAYS"; 
            sqlCmd.Parameters["@FLAG"].Value = "ADD";


            sqlCmd.Transaction = _Transaction;

            sqlda = new SqlDataAdapter();

            sqlda.InsertCommand = sqlCmd;

            sqlda.Update(objDTHoliday);

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

    #endregion

    #region Get Room LatestID
    public string GetRoomLatestID()
    {
        string strRoomID = "";
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_MST_ROOM_DAYS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@FLAG", "GET_LATEST_ROOM_ID");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                strRoomID = dr[0].ToString();
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
        return strRoomID;
    }
    #endregion      

    #region Get Room Days Details
    public void GetRoomDaysDetails(string strRoomDaysID, ref DataTable dtRequestDetails)
    {       
        try
        {
            //dtRequestDetails = new DataTable();

            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_MST_ROOM_DAYS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@RoomID", strRoomDaysID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "ROOMDAYS_LIST");

            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(dtRequestDetails);
            
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
    #endregion

    #region Get Holiday Details
    public void GetHolidayDetails(string strRoomDaysID, ref DataTable dtHolidayDetails)
    {
        try
        {
       
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_MST_ROOM_HOLIDAY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@ROOM_ID", strRoomDaysID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "HOLIDAY_LIST");

            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(dtHolidayDetails);

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
    #endregion

    #region Delete Room & Holiday Details
    
    public void DeleteRoomDetails(string strRoomDaysID)
    {
        try
        {

            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_MST_ROOM_DAYS", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@RoomID", strRoomDaysID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");

            dr = sqlCmd.ExecuteReader();
            

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

    public void DeleteHolidayDetails(string strRoomDaysID)
    {
        try
        {

            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_MST_ROOM_HOLIDAY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@ROOM_ID", strRoomDaysID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");

            dr = sqlCmd.ExecuteReader();


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


    //public DataTable dtCalander(string strDaysName)
    //{
    //    DataTable dtCalander = new DataTable();
    //    sqlCon = new SqlConnection(strConn);
    //    sqlCon.Open();
    //    string strQuerys = "SELECT R.ROOMID,max(R.DAYS) AS R_DAYS ,max(R.STARTTIME) AS STARTTIME,max(R.ENDTIME) AS ENDTIME,	max(H.HOLIDAYS_DATE) AS HOLIDAYS_DATE,max(H.DAYS) AS H_DAYS" +
    //                        " FROM MST_ROOM_DAYS R INNER JOIN MST_HOLIDAYS H ON H.ROOM_ID = R.RoomID Where R.Days='" + strDaysName + "' GROUP BY R.ROOMID";

    //    sqlCmd = new SqlCommand(strQuerys, sqlCon);
    //    sqlCmd.CommandType = CommandType.Text;

    //    sqlda = new SqlDataAdapter(sqlCmd);
    //    sqlda.Fill(dtCalander);

    //    return dtCalander;
    //}

    #endregion

    #region "Check Can we add visit for perticular room on selected date"

    public bool checkRoomTiming(ArrayList objAL)
    {
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_CHECK_ROOM_DAY_TIME", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_ROOM_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_DATE", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@START_TIME", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@END_TIME", objAL[4].ToString());
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                return true;
            }
            return false;
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

    #endregion

    #region "Get room start time and end time"

    public string getRoomStart_EndTime(string p_szRoomID,string p_szEventDate,string p_szCompanyID)
    {
        string szReturn = "";
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_GET_ROOM_STARTENDTIME", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_ROOM_ID", p_szRoomID);
            sqlCmd.Parameters.AddWithValue("@DT_EVENT_DATE", p_szEventDate);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr["TIME"].ToString();
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
        return szReturn;
    }

    #endregion

    #region "Check referring facility is available for company"

    public bool checkReferringFacility(string szCompanyID)
    {
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_TXN_REFERRING_FACILITY", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@ID", szCompanyID);
            sqlCmd.Parameters.AddWithValue("@FLAG", "REFERRING_FACILITY_LIST");
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                return true;
            }
            return false;
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

    #endregion


    #region Get Room GROUP ID
    public string GetRoomProcedureGroupID(string szCompanyID,string szRommID)
    {
        string strGroupID = "";
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_GET_PROCEDURE_GROUP_ID_USING_ROMM_ID", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_ROOM_ID", szRommID);

            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                strGroupID = dr[0].ToString();
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
        return strGroupID;
    }
    #endregion    
}
