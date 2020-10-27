using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using MG2PDF.DataAccessObject;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

/// <summary>
/// Summary description for MBS_CASEWISE_MG11
/// </summary>
public class MBS_CASEWISE_MG11
{
    String strConn;
    SqlConnection conn;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

	public MBS_CASEWISE_MG11()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    public string SaveMG11(AddMG11CaseWiseDAO objMG11)
    {
        string i_ID = "0";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "SP_Insert_MG11_Case_Wise_Details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@sz_CompanyID", objMG11.sz_CompanyID);
            cmd.Parameters.AddWithValue("@sz_CaseID", objMG11.sz_CaseID);
            cmd.Parameters.AddWithValue("@sz_UserID", objMG11.sz_UserID);
            cmd.Parameters.AddWithValue("@WCBCaseNumber", objMG11.WCBCaseNumber);
            cmd.Parameters.AddWithValue("@carrierCaseNumber", objMG11.carrierCaseNumber);

            if (objMG11.dateOfInjury == null)
                cmd.Parameters.AddWithValue("@dateOfInjury", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG11.dateOfInjury == "")
                cmd.Parameters.AddWithValue("@dateOfInjury", SqlDbType.DateTime).Value = System.DBNull.Value;
            else 
                cmd.Parameters.AddWithValue("@dateOfInjury", objMG11.dateOfInjury);

            cmd.Parameters.AddWithValue("@firstName", objMG11.firstName);
            cmd.Parameters.AddWithValue("@MiddleName", objMG11.MiddleName);
            cmd.Parameters.AddWithValue("@lastName", objMG11.lastName);
            cmd.Parameters.AddWithValue("@socialSecurityNumber", objMG11.socialSecurityNumber);
            cmd.Parameters.AddWithValue("@patientAddress", objMG11.patientAddress);
            cmd.Parameters.AddWithValue("@DoctorWCBNumber", objMG11.DoctorWCBNumber);
            cmd.Parameters.AddWithValue("@TreatmentOne", objMG11.TreatmentOne);
            cmd.Parameters.AddWithValue("@GuidelineOne", objMG11.GuidelineOne);
            cmd.Parameters.AddWithValue("@GuidelineBoxOne", objMG11.GuidelineBoxOne);
            cmd.Parameters.AddWithValue("@GuidelineBoxTwo", objMG11.GuidelineBoxTwo);
            cmd.Parameters.AddWithValue("@GuidelineBoxThree", objMG11.GuidelineBoxThree);
            cmd.Parameters.AddWithValue("@GuidelineBoxfour", objMG11.GuidelineBoxfour);

            if (objMG11.dateOfServiceOne == null)
                cmd.Parameters.AddWithValue("@dateOfServiceOne", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG11.dateOfServiceOne == "")
                cmd.Parameters.AddWithValue("@dateOfServiceOne", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dateOfServiceOne", objMG11.dateOfServiceOne);

            cmd.Parameters.AddWithValue("@sz_Comments", objMG11.sz_Comments);
            cmd.Parameters.AddWithValue("@TreatmentTwo", objMG11.TreatmentTwo);
            cmd.Parameters.AddWithValue("@GuidelineTwo", objMG11.GuidelineTwo);
            cmd.Parameters.AddWithValue("@GuidelineFive", objMG11.GuidelineFive);
            cmd.Parameters.AddWithValue("@GuidelineBoxSix", objMG11.GuidelineBoxSix);
            cmd.Parameters.AddWithValue("@GuidelineBoxSeven", objMG11.GuidelineBoxSeven);
            cmd.Parameters.AddWithValue("@GuidelineBoxEight", objMG11.GuidelineBoxEight);

            if (objMG11.dateOfServiceTwo == null)
                cmd.Parameters.AddWithValue("@dateOfServiceTwo", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG11.dateOfServiceTwo == "")
                cmd.Parameters.AddWithValue("@dateOfServiceTwo", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dateOfServiceTwo", objMG11.dateOfServiceTwo);

            cmd.Parameters.AddWithValue("@sz_Comments_two", objMG11.sz_Comments_two);
            cmd.Parameters.AddWithValue("@TreatmentThree", objMG11.TreatmentThree);
            cmd.Parameters.AddWithValue("@GuidelineThree", objMG11.GuidelineThree);
            cmd.Parameters.AddWithValue("@GuidelineBoxNine", objMG11.GuidelineBoxNine);
            cmd.Parameters.AddWithValue("@GuidelineBoxTen", objMG11.GuidelineBoxTen);
            cmd.Parameters.AddWithValue("@GuidelineBoxeleven", objMG11.GuidelineBoxeleven);
            cmd.Parameters.AddWithValue("@GuidelineBoxtwelve", objMG11.GuidelineBoxtwelve);

            if (objMG11.dateOfServiceThree == null)
                cmd.Parameters.AddWithValue("@dateOfServiceThree", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG11.dateOfServiceThree == "")
                cmd.Parameters.AddWithValue("@dateOfServiceThree", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dateOfServiceThree", objMG11.dateOfServiceThree);

            cmd.Parameters.AddWithValue("@sz_Comments_three", objMG11.sz_Comments_three);
            cmd.Parameters.AddWithValue("@TreatmentFour", objMG11.TreatmentFour);
            cmd.Parameters.AddWithValue("@GuidelineFour", objMG11.GuidelineFour);
            cmd.Parameters.AddWithValue("@GuidelineThirteen", objMG11.GuidelineThirteen);
            cmd.Parameters.AddWithValue("@GuidelineBoxfourteen", objMG11.GuidelineBoxfourteen);
            cmd.Parameters.AddWithValue("@GuidelineBoxfifteen", objMG11.GuidelineBoxfifteen);
            cmd.Parameters.AddWithValue("@GuidelineBoxsixteen", objMG11.GuidelineBoxsixteen);

            if (objMG11.dateOfServiceFour == null)
                cmd.Parameters.AddWithValue("@dateOfServiceFour", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG11.dateOfServiceFour == "")
                cmd.Parameters.AddWithValue("@dateOfServiceFour", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dateOfServiceFour", objMG11.dateOfServiceFour);

            cmd.Parameters.AddWithValue("@sz_Comments_four", objMG11.sz_Comments_four);
            cmd.Parameters.AddWithValue("@Carrier_One", objMG11.Carrier_One);
            cmd.Parameters.AddWithValue("@Carrier_two", objMG11.Carrier_two);
            cmd.Parameters.AddWithValue("@Carrier_three", objMG11.Carrier_three);
            if (objMG11.CarrierDate == null)
                cmd.Parameters.AddWithValue("@CarrierDate", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG11.CarrierDate == "")
                cmd.Parameters.AddWithValue("@CarrierDate", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@CarrierDate", objMG11.CarrierDate);

            cmd.Parameters.AddWithValue("@Employer", objMG11.Employer);
            cmd.Parameters.AddWithValue("@MedicalProfessional", objMG11.MedicalProfessional);
            cmd.Parameters.AddWithValue("@PrintNameOne", objMG11.PrintNameOne);
            cmd.Parameters.AddWithValue("@TitleOne", objMG11.TitleOne);
            if (objMG11.EmployerDate == null)
                cmd.Parameters.AddWithValue("@EmployerDate", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG11.EmployerDate == "")
                cmd.Parameters.AddWithValue("@EmployerDate", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@EmployerDate", objMG11.EmployerDate);

            if (objMG11.MedicalDate == null)
                cmd.Parameters.AddWithValue("@MedicalDate", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG11.MedicalDate == "")
                cmd.Parameters.AddWithValue("@MedicalDate", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@MedicalDate", objMG11.MedicalDate);

            if (objMG11.ProviderDate == null)
                cmd.Parameters.AddWithValue("@ProviderDate", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG11.ProviderDate == "")
                cmd.Parameters.AddWithValue("@ProviderDate", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@ProviderDate", objMG11.ProviderDate);

            cmd.Parameters.AddWithValue("@Provider_request", objMG11.Provider_request);
            cmd.Parameters.AddWithValue("@Print_Name_two", objMG11.Print_Name_two);
            cmd.Parameters.AddWithValue("@Title_two", objMG11.Title_two);
            if (objMG11.EmployerDate_two == null)
                cmd.Parameters.AddWithValue("@EmployerDate_two", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG11.EmployerDate_two == "")
                cmd.Parameters.AddWithValue("@EmployerDate_two", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@EmployerDate_two", objMG11.EmployerDate_two);

            cmd.Parameters.AddWithValue("@cbGranted_One", objMG11.cbGranted_One);
            cmd.Parameters.AddWithValue("@CbGrantedPrejudice_One", objMG11.CbGrantedPrejudice_One);
            cmd.Parameters.AddWithValue("@CbDenied_One", objMG11.CbDenied_One);
            cmd.Parameters.AddWithValue("@cbGranted_Two", objMG11.cbGranted_Two);
            cmd.Parameters.AddWithValue("@CbGrantedPrejudice_Two", objMG11.CbGrantedPrejudice_Two);
            cmd.Parameters.AddWithValue("@CbDenied_Two", objMG11.CbDenied_Two);
            cmd.Parameters.AddWithValue("@cbGranted_Three", objMG11.cbGranted_Three);
            cmd.Parameters.AddWithValue("@CbGrantedPrejudice_Three", objMG11.CbGrantedPrejudice_Three);
            cmd.Parameters.AddWithValue("@CbDenied_Three", objMG11.CbDenied_Three);
            cmd.Parameters.AddWithValue("@cbGranted_Four", objMG11.cbGranted_Four);
            cmd.Parameters.AddWithValue("@CbGrantedPrejudice_Four", objMG11.CbGrantedPrejudice_Four);
            cmd.Parameters.AddWithValue("@CbDenied_Four", objMG11.CbDenied_Four);
            cmd.Parameters.AddWithValue("@CbContactOne", objMG11.CbContactOne);
            cmd.Parameters.AddWithValue("@CbContacttwo", objMG11.CbContacttwo);
            cmd.Parameters.AddWithValue("@CbCarrier_One", objMG11.CbCarrier_One);
            cmd.Parameters.AddWithValue("@CBProvider", objMG11.CBProvider);
            cmd.Parameters.AddWithValue("@Medical_request_two", objMG11.Medical_request_two);
            cmd.Parameters.AddWithValue("@MedicalDate_three", objMG11.MedicalDate_three);
            cmd.Parameters.AddWithValue("@CBMedical_request_four", objMG11.CBMedical_request_four);
            cmd.Parameters.AddWithValue("@MedicalDate_five", objMG11.MedicalDate_five);
            cmd.Parameters.AddWithValue("@CBProvider_request", objMG11.CBProvider_request);
            cmd.Parameters.AddWithValue("@CB_request_two", objMG11.CB_request_two);
            cmd.Parameters.AddWithValue("@CB_request_three", objMG11.CB_request_three);
            cmd.Parameters.AddWithValue("@CB_request_four", objMG11.CB_request_four);
            cmd.Parameters.AddWithValue("@CB_request_five", objMG11.CB_request_five);

            cmd.Parameters.AddWithValue("@sz_doctor_ID", objMG11.sz_DoctorID);

            cmd.Parameters.AddWithValue("@SZ_Doctor_Name", objMG11.sz_Doctor_Name);

            cmd.Parameters.AddWithValue("@i_mg11_id", objMG11.I_ID);

            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i_ID = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return i_ID;
    }

    public DataSet GetMG11GridDetails(string szCaseId)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_MG11_CASEDETAIL", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
        }
        catch (SqlException _ex)
        {
            _ex.Message.ToString();
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

    public DataSet GetInitialRecordsMG11(string sz_caseID)
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();

        try
        {
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_MG11_INITIAL_DETAILS", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_caseID);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
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

    public DataTable GetMG11Records(string sz_CompanyID, string i_case_id, string i_Id)
    {
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_txn_mg11_case_wise_details", conn);
            cmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            cmd.Parameters.AddWithValue("@i_case_id", i_case_id);
            cmd.Parameters.AddWithValue("@i_Id", i_Id);

            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            throw ex;
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
}