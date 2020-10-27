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
/// Summary description for MBS_CASEWISE_MG1
/// </summary>
public class MBS_CASEWISE_MG1
{

    String strConn;
    SqlConnection conn;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;
    public MBS_CASEWISE_MG1()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public DataSet GetInitialRecordsMG1(string sz_caseID)
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();

        try
        {
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_MG1_INITIAL_DETAILS", con);
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

    public string SaveMG1(AddMG1CasewiseDAO objMG1)
    {
        string i_ID = "0";
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "SP_txn_mg1_case_wise_details";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@sz_company_id", objMG1.sz_CompanyID);
            cmd.Parameters.AddWithValue("@i_case_id", objMG1.sz_CaseID);
            cmd.Parameters.AddWithValue("@sz_user_id", objMG1.sz_UserID);
            cmd.Parameters.AddWithValue("@sz_modified_by", objMG1.sz_UserID);

            cmd.Parameters.AddWithValue("@sz_attending_doctor_name_address", objMG1.attendingDoctorNameAddress);//DDLGiidline .SelectedItem .Text
            cmd.Parameters.AddWithValue("@sz_guidelines_reference", objMG1.sz_DoctorID);
            cmd.Parameters.AddWithValue("@dt_date_request_submitted", objMG1.date_request_submitted);

            //cmd.Parameters.AddWithValue("@sz_approval_request", objMG1.approvalRequest);
            cmd.Parameters.AddWithValue("@sz_wcb_case_file", objMG1.dateOfService);
            cmd.Parameters.AddWithValue("@sz_comments", objMG1.sz_comments);
            cmd.Parameters.AddWithValue("@sz_applicable", objMG1.datesOfDeniedRequest);

            cmd.Parameters.AddWithValue("@sz_procedure_Requested", objMG1.ProcedureRequest);

            cmd.Parameters.AddWithValue("@bt_did", objMG1.chkDid);
            cmd.Parameters.AddWithValue("@bt_not_did", objMG1.chkDidNot);
            cmd.Parameters.AddWithValue("@sz_spoke", objMG1.contactDate);
            cmd.Parameters.AddWithValue("@sz_spoke_anyone", objMG1.personContacted);

            cmd.Parameters.AddWithValue("@bt_a_copy", objMG1.chkCopySent);
            cmd.Parameters.AddWithValue("@sz_fund_by", objMG1.faxEmail);

            //cmd.Parameters.AddWithValue("@bt_equipped", objMG1.chkCopyNotSent);
            //cmd.Parameters.AddWithValue("@sz_indicated", objMG1.indicatedFaxEmail);
            //cmd.Parameters.AddWithValue("@sz_provider_signature",TxtProviderSig .Text);            
            cmd.Parameters.AddWithValue("@dt_provider_signature_date", objMG1.providerSignDate);

            //cmd.Parameters.AddWithValue("@bt_self_insurrer", objMG1.chkNoticeGiven);
            cmd.Parameters.AddWithValue("@sz_print_name_D", objMG1.printCarrierEmployerNoticeName);
            cmd.Parameters.AddWithValue("@sz_title_D", objMG1.noticeTitle);
            //cmd.Parameters.AddWithValue("@sz_signature_D",TxtSignatureD .Text);
            if (objMG1.noticeCarrierSignDate == null)
                cmd.Parameters.AddWithValue("@dt_date_D", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG1.noticeCarrierSignDate == "")
                cmd.Parameters.AddWithValue("@dt_date_D", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_date_D", objMG1.noticeCarrierSignDate);

            if (objMG1.carrierDenial == null)
                cmd.Parameters.AddWithValue("@dt_initial_denied", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG1.carrierDenial == "")
                cmd.Parameters.AddWithValue("@dt_initial_denied", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_initial_denied", objMG1.carrierDenial);

            cmd.Parameters.AddWithValue("@bt_granted", objMG1.chkGranted); 
            cmd.Parameters.AddWithValue("@bt_without_prejudice", objMG1.chkWithoutPrejudice);
            cmd.Parameters.AddWithValue("@bt_denied", objMG1.chkDenied);

            cmd.Parameters.AddWithValue("@sz_carrier_reverse", objMG1.CarrierOnReverse);
            //cmd.Parameters.AddWithValue("@bt_substantialy", objMG1.chkSubstantiallySimilar);
            cmd.Parameters.AddWithValue("@sz_if_applicable", objMG1.medicalProfessional);
            //cmd.Parameters.AddWithValue("@bt_made_E", objMG1.chkMedicalArbitrator);
            //cmd.Parameters.AddWithValue("@bt_chair_E", objMG1.chkWCBHearing);
            //cmd.Parameters.AddWithValue("@sz_print_name_E", objMG1.printCarrierEmployerResponseName);
            //cmd.Parameters.AddWithValue("@sz_title_E", objMG1.responseTitle);
            //cmd.Parameters.AddWithValue("@sz_signature_E",TxtSignatureE .Text );

            if (objMG1.responseCarrierSignDate == null)
                cmd.Parameters.AddWithValue("@dt_date_E", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG1.responseCarrierSignDate == "")
                cmd.Parameters.AddWithValue("@dt_date_E", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_date_E", objMG1.responseCarrierSignDate);

            if (objMG1.dt_supporting_medical_on == null)
                cmd.Parameters.AddWithValue("@dt_supporting_medical_on", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG1.dt_supporting_medical_on == "")
                cmd.Parameters.AddWithValue("@dt_supporting_medical_on", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
            cmd.Parameters.AddWithValue("@dt_supporting_medical_on", objMG1.dt_supporting_medical_on);

            cmd.Parameters.AddWithValue("@sz_certify", objMG1.ChkCertify);
            cmd.Parameters.AddWithValue("@sz_print_name_F", objMG1.printDenialCarrierName);
            cmd.Parameters.AddWithValue("@sz_title_F", objMG1.denialTitle);
            //cmd.Parameters.AddWithValue("@sz_signature_F", TxtSignatureF.Text);

            if (objMG1.denialCarrierSignDate == null)
                cmd.Parameters.AddWithValue("@dt_date_F", SqlDbType.DateTime).Value = System.DBNull.Value;
                else if(objMG1.denialCarrierSignDate == "")
                cmd.Parameters.AddWithValue("@dt_date_F", SqlDbType.DateTime).Value = System.DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@dt_date_F", objMG1.denialCarrierSignDate);

            //cmd.Parameters.AddWithValue("@bt_i_request", objMG1.chkRequestWC);

            cmd.Parameters.AddWithValue("@sz_section_E", objMG1.chkMedicalArbitratorByWC);

            //cmd.Parameters.AddWithValue("@bt_chair_G", objMG1.chkWCBHearingByWC);
            ////cmd.Parameters.AddWithValue("@sz_claimant_signature",TxtClairmantSignature .Text );
            //cmd.Parameters.AddWithValue("@dt_claimant_date", objMG1.claimantSignDate);

            cmd.Parameters.AddWithValue("@sz_wcb_case_no", objMG1.WCBCaseNumber);
            cmd.Parameters.AddWithValue("@sz_carrier_case_no", objMG1.carrierCaseNumber);
            cmd.Parameters.AddWithValue("@sz_date_of_injury", objMG1.dateOfInjury);
            cmd.Parameters.AddWithValue("@sz_patient_name", objMG1.firstName);
          
            cmd.Parameters.AddWithValue("@sz_patient_address", objMG1.patientAddress);
            cmd.Parameters.AddWithValue("@sz_employee_name_address", objMG1.employerNameAddress);
            cmd.Parameters.AddWithValue("@sz_insurance_name_address", objMG1.insuranceNameAddress);
            cmd.Parameters.AddWithValue("@sz_doctor_name_address", objMG1.sz_DoctorID);//DDLAttendingDoctors.Text
            cmd.Parameters.AddWithValue("@sz_individual_provider1", objMG1.providerWCBNumber1);

            cmd.Parameters.AddWithValue("@sz_individual_provider2", objMG1.providerWCBNumber2);
            cmd.Parameters.AddWithValue("@sz_individual_provider3", objMG1.providerWCBNumber3);
            cmd.Parameters.AddWithValue("@sz_individual_provider4", objMG1.providerWCBNumber4);
            cmd.Parameters.AddWithValue("@sz_individual_provider5", objMG1.providerWCBNumber5);
            cmd.Parameters.AddWithValue("@sz_individual_provider6", objMG1.providerWCBNumber6);
            cmd.Parameters.AddWithValue("@sz_individual_provider7", objMG1.providerWCBNumber7);
            cmd.Parameters.AddWithValue("@sz_individual_provider8", objMG1.providerWCBNumber8);

            cmd.Parameters.AddWithValue("@sz_teltphone_no", objMG1.doctorPhone);
            cmd.Parameters.AddWithValue("@sz_fax_no", objMG1.doctorFax);
            cmd.Parameters.AddWithValue("@sz_Guidline_Char", objMG1.bodyInitial);

            cmd.Parameters.AddWithValue("@sz_Guidline1", objMG1.guidelineSection1);
            cmd.Parameters.AddWithValue("@sz_Guidline2", objMG1.guidelineSection2);
            cmd.Parameters.AddWithValue("@sz_Guidline3", objMG1.guidelineSection3);
            cmd.Parameters.AddWithValue("@sz_Guidline4", objMG1.guidelineSection4);
            cmd.Parameters.AddWithValue("@sz_security_no", objMG1.socialSecurityNumber);
            //cmd.Parameters.AddWithValue("@sz_bill_no", objMG2.sz_BillNo);

            //cmd.Parameters.AddWithValue("@sz_patient_id", objMG2.PatientID);

            cmd.Parameters.AddWithValue("@I_ID", objMG1.I_ID);
            //cmd.Parameters.AddWithValue("@sz_procedure_group_id", objMG1.sz_procedure_group_id);
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

    public DataSet GetMG1Record(string sz_CompanyID, string sz_CaseID, string i_Id)
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_txn_mg1_case_wise_details", conn);
            cmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            cmd.Parameters.AddWithValue("@i_case_id", sz_CaseID);
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

    /*Mahesh*/

    public DataSet GetMG1GridDetails(string szCaseId)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_MG1_CASEDETAIL", sqlCon);
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

    public DataTable GetMG1Records(String sz_CompanyID, String i_case_id, String i_Id)
    {
        DataTable ds = new DataTable();
        SqlConnection con = new SqlConnection();
        try
        {
            conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_txn_mg1_case_wise_details", conn);
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
