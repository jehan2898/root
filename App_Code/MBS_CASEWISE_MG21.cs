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
/// Summary description for MBS_CASEWISE_MG21
/// </summary>
public class MBS_CASEWISE_MG21
{
	public MBS_CASEWISE_MG21()
	{
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}

    String strConn;
    SqlConnection conn;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    public DataSet GetMG21Record(String sz_CompanyID, String i_case_id, String i_Id)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_get_txn_mg21_case_wise_details", conn);
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

    public string SaveMG21(MG21CasewiseDAO objMG2)
    {
        string i_ID = "0";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "sp_save_txn_mg21_case_wise_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@i_id", objMG2.I_ID);
            cmd.Parameters.AddWithValue("@sz_company_id", objMG2.sz_CompanyID);
            cmd.Parameters.AddWithValue("@i_case_id", objMG2.sz_CaseID);
            cmd.Parameters.AddWithValue("@sz_user_id", objMG2.sz_UserID);
            cmd.Parameters.AddWithValue("@sz_modified_by", objMG2.sz_UserID);

            cmd.Parameters.AddWithValue("@sz_wcb_case_no", objMG2.WCBCaseNumber);
            cmd.Parameters.AddWithValue("@sz_carrier_case_no", objMG2.carrierCaseNumber);
            cmd.Parameters.AddWithValue("@sz_date_of_injury", objMG2.dateOfInjury);
            cmd.Parameters.AddWithValue("@sz_patient_firstname", objMG2.firstName);
            cmd.Parameters.AddWithValue("@sz_patient_middlename", objMG2.middleName);
            cmd.Parameters.AddWithValue("@sz_patient_lastname", objMG2.lastName);
            cmd.Parameters.AddWithValue("@sz_doctor_Name", objMG2.sz_doctor_Name);
            cmd.Parameters.AddWithValue("@sz_doctor_id", objMG2.sz_doctor_id);
            cmd.Parameters.AddWithValue("@sz_patient_address", objMG2.patientAddress);
            cmd.Parameters.AddWithValue("@sz_security_no", objMG2.socialSecurityNumber);
            cmd.Parameters.AddWithValue("@sz_insurance_name_address", objMG2.insuranceNameAddress);
            cmd.Parameters.AddWithValue("@sz_doctorWCBAuth_number", objMG2.sz_doctorWCBAuth_number);

            cmd.Parameters.AddWithValue("@sz_guidelines_reference2", objMG2.sz_guidelines_reference2);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference22", objMG2.sz_guidelines_reference22);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference23", objMG2.sz_guidelines_reference23);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference24", objMG2.sz_guidelines_reference24);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference25", objMG2.sz_guidelines_reference25);

            if (objMG2.dt_DateOfService2 == null)
                cmd.Parameters.AddWithValue("@dt_DateOfService2", SqlDbType.DateTime).Value = System.DBNull.Value;
            else if(objMG2.dt_DateOfService2 == "")
                    cmd.Parameters.AddWithValue("@dt_DateOfService2", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_DateOfService2", objMG2.dt_DateOfService2);

            if (objMG2.dt_DateOfPrevious2 == null)
                cmd.Parameters.AddWithValue("@dt_DateOfPrevious2", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.dt_DateOfPrevious2 == "")
                cmd.Parameters.AddWithValue("@dt_DateOfPrevious2", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_DateOfPrevious2", objMG2.dt_DateOfPrevious2);

            cmd.Parameters.AddWithValue("@sz_ApprovalRequest2", objMG2.approvalRequest2);
            cmd.Parameters.AddWithValue("@sz_MedicalNecessity2", objMG2.sz_MedicalNecessity2);

            cmd.Parameters.AddWithValue("@sz_guidelines_reference3", objMG2.sz_guidelines_reference3);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference32", objMG2.sz_guidelines_reference32);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference33", objMG2.sz_guidelines_reference33);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference34", objMG2.sz_guidelines_reference34);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference35", objMG2.sz_guidelines_reference35);

            if (objMG2.dt_DateOfService3 == null)
                cmd.Parameters.AddWithValue("@dt_DateOfService3", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.dt_DateOfService3 == "")
                cmd.Parameters.AddWithValue("@dt_DateOfService3", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_DateOfService3", objMG2.dt_DateOfService3);

            if (objMG2.dt_DateOfPrevious3 == null)
                cmd.Parameters.AddWithValue("@dt_DateOfPrevious3", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.dt_DateOfPrevious3 == "")
                cmd.Parameters.AddWithValue("@dt_DateOfPrevious3", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_DateOfPrevious3", objMG2.dt_DateOfPrevious3);

            cmd.Parameters.AddWithValue("@sz_ApprovalRequest3", objMG2.approvalRequest3);
            cmd.Parameters.AddWithValue("@sz_MedicalNecessity3", objMG2.sz_MedicalNecessity3);

            cmd.Parameters.AddWithValue("@sz_guidelines_reference4", objMG2.sz_guidelines_reference4);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference42", objMG2.sz_guidelines_reference42);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference43", objMG2.sz_guidelines_reference43);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference44", objMG2.sz_guidelines_reference44);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference45", objMG2.sz_guidelines_reference45);

            if (objMG2.dt_DateOfService4 == null)
                cmd.Parameters.AddWithValue("@dt_DateOfService4", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.dt_DateOfService4 == "")
                cmd.Parameters.AddWithValue("@dt_DateOfService4", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_DateOfService4", objMG2.dt_DateOfService4);

            if (objMG2.dt_DateOfPrevious4 == null)
                cmd.Parameters.AddWithValue("@dt_DateOfPrevious4", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.dt_DateOfPrevious4 == "")
                cmd.Parameters.AddWithValue("@dt_DateOfPrevious4", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_DateOfPrevious4", objMG2.dt_DateOfPrevious4);

            cmd.Parameters.AddWithValue("@sz_ApprovalRequest4", objMG2.approvalRequest4);
            cmd.Parameters.AddWithValue("@sz_MedicalNecessity4", objMG2.sz_MedicalNecessity4);

            cmd.Parameters.AddWithValue("@sz_guidelines_reference5", objMG2.sz_guidelines_reference5);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference52", objMG2.sz_guidelines_reference52);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference53", objMG2.sz_guidelines_reference53);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference54", objMG2.sz_guidelines_reference54);
            cmd.Parameters.AddWithValue("@sz_guidelines_reference55", objMG2.sz_guidelines_reference55);

            if (objMG2.dt_DateOfService5 == null)
                cmd.Parameters.AddWithValue("@dt_DateOfService5", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.dt_DateOfService5 == "")
                cmd.Parameters.AddWithValue("@dt_DateOfService5", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_DateOfService5", objMG2.dt_DateOfService5);

            if (objMG2.dt_DateOfPrevious5 == null)
                cmd.Parameters.AddWithValue("@dt_DateOfPrevious5", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.dt_DateOfPrevious5 == "")
                cmd.Parameters.AddWithValue("@dt_DateOfPrevious5", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_DateOfPrevious5", objMG2.dt_DateOfPrevious5);

            cmd.Parameters.AddWithValue("@sz_approval_request5", objMG2.approvalRequest5);
            cmd.Parameters.AddWithValue("@sz_MedicalNecessity5", objMG2.sz_MedicalNecessity5);


            cmd.Parameters.AddWithValue("@sz_wcb_case_no2", objMG2.WCBCaseNumber2);
            cmd.Parameters.AddWithValue("@sz_date_of_injury2", objMG2.dateOfInjury2);
            cmd.Parameters.AddWithValue("@bt_did", objMG2.bt_did);
            cmd.Parameters.AddWithValue("@bt_not_did", objMG2.bt_not_did);

            if (objMG2.dt_Tele_Date == null)
                cmd.Parameters.AddWithValue("@dt_Tele_Date", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.dt_Tele_Date == "")
                cmd.Parameters.AddWithValue("@dt_Tele_Date", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_Tele_Date", objMG2.dt_Tele_Date);

            cmd.Parameters.AddWithValue("@sz_spoke_anyone", objMG2.sz_spoke_anyone);
            cmd.Parameters.AddWithValue("@bt_a_copy", objMG2.bt_a_copy);
            cmd.Parameters.AddWithValue("@sz_Fax", objMG2.sz_Fax);
            if (objMG2.providerSignDate == null)
                cmd.Parameters.AddWithValue("@dt_provider_signature_date", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.providerSignDate == "")
                cmd.Parameters.AddWithValue("@dt_provider_signature_date", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_provider_signature_date", objMG2.providerSignDate);

            cmd.Parameters.AddWithValue("@bt_employer", objMG2.bt_employer);
            cmd.Parameters.AddWithValue("@bt_CarrierReq2", objMG2.bt_CarrierReq2);
            cmd.Parameters.AddWithValue("@bt_CarrierReq3", objMG2.bt_CarrierReq3);
            cmd.Parameters.AddWithValue("@bt_CarrierReq4", objMG2.bt_CarrierReq4);
            cmd.Parameters.AddWithValue("@bt_CarrierReq5", objMG2.bt_CarrierReq5);
            cmd.Parameters.AddWithValue("@sz_CarrierPrintName", objMG2.sz_CarrierPrintName);
            cmd.Parameters.AddWithValue("@sz_CarrierPrintTitle", objMG2.sz_CarrierPrintTitle);

            if (objMG2.sz_CarrierPrintDate == null)
                cmd.Parameters.AddWithValue("@sz_CarrierPrintDate", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.sz_CarrierPrintDate == "")
                cmd.Parameters.AddWithValue("@sz_CarrierPrintDate", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@sz_CarrierPrintDate", objMG2.sz_CarrierPrintDate);

            cmd.Parameters.AddWithValue("@bt_granted2", objMG2.bt_granted2);
            cmd.Parameters.AddWithValue("@bt_granted_in_part2", objMG2.bt_granted_in_part2);
            cmd.Parameters.AddWithValue("@bt_denied2", objMG2.bt_denied2);
            cmd.Parameters.AddWithValue("@bt_burden2", objMG2.bt_burden2);
            cmd.Parameters.AddWithValue("@bt_substantialy2", objMG2.bt_substantialy2);
            cmd.Parameters.AddWithValue("@bt_without_prejudice2", objMG2.bt_without_prejudice2);


            cmd.Parameters.AddWithValue("@bt_granted3", objMG2.bt_granted3);
            cmd.Parameters.AddWithValue("@bt_granted_in_part3", objMG2.bt_granted_in_part3);
            cmd.Parameters.AddWithValue("@bt_denied3", objMG2.bt_denied3);
            cmd.Parameters.AddWithValue("@bt_burden3", objMG2.bt_burden3);
            cmd.Parameters.AddWithValue("@bt_substantialy3", objMG2.bt_substantialy3);
            cmd.Parameters.AddWithValue("@bt_without_prejudice3", objMG2.bt_without_prejudice3);

            cmd.Parameters.AddWithValue("@bt_granted4", objMG2.bt_granted4);
            cmd.Parameters.AddWithValue("@bt_granted_in_part4", objMG2.bt_granted_in_part4);
            cmd.Parameters.AddWithValue("@bt_denied4", objMG2.bt_denied4);
            cmd.Parameters.AddWithValue("@bt_burden4", objMG2.bt_burden4);
            cmd.Parameters.AddWithValue("@bt_substantialy4", objMG2.bt_substantialy4);
            cmd.Parameters.AddWithValue("@bt_without_prejudice4", objMG2.bt_without_prejudice4);

            cmd.Parameters.AddWithValue("@bt_granted5", objMG2.bt_granted5);
            cmd.Parameters.AddWithValue("@bt_granted_in_part5", objMG2.bt_granted_in_part5);
            cmd.Parameters.AddWithValue("@bt_denied5", objMG2.bt_denied5);
            cmd.Parameters.AddWithValue("@bt_burden5", objMG2.bt_burden5);
            cmd.Parameters.AddWithValue("@bt_substantialy5", objMG2.bt_substantialy5);
            cmd.Parameters.AddWithValue("@bt_without_prejudice5", objMG2.bt_without_prejudice5);


            cmd.Parameters.AddWithValue("@sz_Carrier", objMG2.sz_Carrier);
            cmd.Parameters.AddWithValue("@sz_NameOfMedProfessional", objMG2.sz_NameOfMedProfessional);
            cmd.Parameters.AddWithValue("@bt_byMedArb", objMG2.bt_byMedArb);
            cmd.Parameters.AddWithValue("@bt_byChair", objMG2.bt_byChair);
            cmd.Parameters.AddWithValue("@sz_print_name_D", objMG2.sz_print_name_D);
            cmd.Parameters.AddWithValue("@sz_title_D", objMG2.sz_title_D);

            if (objMG2.dt_date_D == null)
                cmd.Parameters.AddWithValue("@dt_date_D", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.dt_date_D == "")
                cmd.Parameters.AddWithValue("@dt_date_D", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_date_D", objMG2.dt_date_D);

            cmd.Parameters.AddWithValue("@bt_DenIRequest2", objMG2.bt_DenIRequest2);
            cmd.Parameters.AddWithValue("@bt_DenIRequest3", objMG2.bt_DenIRequest3);
            cmd.Parameters.AddWithValue("@bt_DenIRequest4", objMG2.bt_DenIRequest4);
            cmd.Parameters.AddWithValue("@bt_DenIRequest5", objMG2.bt_DenIRequest5);
            cmd.Parameters.AddWithValue("@sz_print_name_Den", objMG2.sz_print_name_Den);
            cmd.Parameters.AddWithValue("@sz_title_Den", objMG2.sz_title_Den);

            if (objMG2.dt_date_Den == "")
                cmd.Parameters.AddWithValue("@dt_date_Den", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.dt_date_Den == "")
                cmd.Parameters.AddWithValue("@dt_date_Den", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_date_Den", objMG2.dt_date_Den);

            cmd.Parameters.AddWithValue("@bt_IRequest", objMG2.bt_IRequest);
            cmd.Parameters.AddWithValue("@bt_IRequest2", objMG2.bt_IRequest2);
            cmd.Parameters.AddWithValue("@bt_IRequest3", objMG2.bt_IRequest3);
            cmd.Parameters.AddWithValue("@bt_IRequest4", objMG2.bt_IRequest4);
            cmd.Parameters.AddWithValue("@bt_IRequest5", objMG2.bt_IRequest5);

            cmd.Parameters.AddWithValue("@bt_byMedArb2", objMG2.bt_byMedArb2);
            cmd.Parameters.AddWithValue("@bt_Atwcb", objMG2.bt_Atwcb);

            if (objMG2.claimantSignDate == null)
                cmd.Parameters.AddWithValue("@dt_claimant_date", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG2.claimantSignDate == "")
                cmd.Parameters.AddWithValue("@dt_claimant_date", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_claimant_date", objMG2.claimantSignDate);

            cmd.Parameters.AddWithValue("@sz_bill_no", objMG2.sz_BillNo);
            cmd.Parameters.AddWithValue("@sz_patient_id", objMG2.PatientID);
            cmd.Parameters.AddWithValue("@sz_pdf_url", objMG2.sz_pdf_url);
            cmd.Parameters.AddWithValue("@sz_procedure_group_id", objMG2.sz_procedure_group_id);

            cmd.Parameters.AddWithValue("@sz_DateOfService2", objMG2.sz_DateOfService2);
            cmd.Parameters.AddWithValue("@sz_DateOfPrevious2", objMG2.sz_DateOfPrevious2);
            cmd.Parameters.AddWithValue("@sz_DateOfService3", objMG2.sz_DateOfService3);
            cmd.Parameters.AddWithValue("@sz_DateOfPrevious3", objMG2.sz_DateOfPrevious3);
            cmd.Parameters.AddWithValue("@sz_DateOfService4", objMG2.sz_DateOfService4);
            cmd.Parameters.AddWithValue("@sz_DateOfPrevious4", objMG2.sz_DateOfPrevious4);
            cmd.Parameters.AddWithValue("@sz_DateOfService5", objMG2.sz_DateOfService5);
            cmd.Parameters.AddWithValue("@sz_DateOfPrevious5", objMG2.sz_DateOfPrevious5);

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

    public DataSet GetMG21GridDetails(string szCaseId)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_MG_CASEDETAILS", sqlCon);
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

    public DataTable GetMG21Records(string sz_CompanyID, string i_case_id, string i_Id)
    {
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_get_txn_mg21_case_wise_details", conn);
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