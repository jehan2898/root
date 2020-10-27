using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using gb.mbs.da.service.util;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.service.patient.report
{
    public class SrvMissingProcedure
    {
        public DataTable SelectMissingProcedure(model.account.Account p_oAccount,model.appointment.Appointment p_StartDate, model.appointment.Appointment p_EndDate, List<model.specialty.Specialty> p_oSpecialty, List<model.carrier.Carrier> p_oCarrier,List<model.procedure.Procedure> p_oProcedure,model.casetype.CaseType p_oCasetype,model.casestatus.CaseStatus p_oCaseStatus)
        {
            SqlConnection oConnection = new SqlConnection(DBUtil.ConnectionString);
            oConnection.Open();
            SqlTransaction oTransaction = null;
            
            try
            {
                oTransaction = oConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                DataSet ds = new DataSet();

                DataTable oDTCarrier = null;
                if (p_oCarrier != null && p_oCarrier.Count > 0)
                {
                    oDTCarrier = gbmodel.carrier.type.TypeCarrier.FillDBType(p_oCarrier);
                }

                DataTable oDTProcedure = null;
                if (p_oProcedure != null && p_oProcedure.Count > 0)
                {
                    oDTProcedure = gbmodel.procedure.type.TypeProcedure.FillDBType(p_oProcedure);
                }

                string sQuery = null;
                if (p_oProcedure != null && p_oProcedure.Count > 0)
                {
                    sQuery = " select t6.sz_case_no [CaseNo], t7.sz_patient_last_name + ', ' + t7.sz_patient_first_name [PatientName], t8.sz_insurance_name [CarrierName],";
                    sQuery += " t6.dt_date_of_accident[AccidentDate],t7.sz_patient_cellno[Phone1],t7.sz_patient_phone[Phone2] from mst_case_master t6 JOIN mst_patient t7 on t7.sz_patient_id = t6.sz_patient_id AND t7.sz_company_id = @sz_company_id";
                    sQuery += " left join mst_insurance_company t8 on t8.sz_insurance_id = t6.sz_insurance_id where t6.sz_case_id not in (select t2.sz_case_id from mst_procedure_group t1";
                    sQuery += " left join txn_doctor_speciality t5 on t5.sz_procedure_group_id = t1.sz_procedure_group_id AND t1.sz_company_id = @sz_company_id";
                    sQuery += " left join mst_doctor t3 on t3.sz_doctor_id = t5.sz_doctor_id AND t3.sz_company_id = @sz_company_id";
                    sQuery += " left join txn_calendar_event t2 on t2.sz_doctor_id = t3.sz_doctor_id AND t2.sz_company_id = @sz_company_id";
                    sQuery += " left join txn_calender_event_prpcedure t6 on t6.i_event_id = t2.i_event_id and t2.sz_company_id = @sz_company_id ";
                    sQuery += " where t1.sz_company_id = @sz_company_id and t1.sz_procedure_group_id IN (@sz_specialty_id) ";
                    sQuery += " AND t6.sz_proc_code IN (SELECT sz_procedure_id FROM @tvp_procedure) ";
                    sQuery += " and t2.sz_case_id IS NOT NULL AND t2.dt_event_date between @dt_start AND @dt_end ) AND t6.sz_company_id = @sz_company_id ";

                    if (p_oCasetype != null && p_oCasetype.ID != null && p_oCasetype.ID != "NA")
                        sQuery += "and t6.sz_case_type_id=@sz_case_type_id";
                    if (p_oCaseStatus != null && p_oCaseStatus.ID != null && p_oCaseStatus.ID != "NA")
                        sQuery += " and t6.sz_case_status_id=@sz_case_status_id";
                }
                else
                {
                    sQuery = " select t6.sz_case_no [CaseNo], t7.sz_patient_last_name + ', ' + t7.sz_patient_first_name [PatientName], t8.sz_insurance_name [CarrierName],";
                    sQuery += " t6.dt_date_of_accident[AccidentDate],t7.sz_patient_cellno[Phone1],t7.sz_patient_phone[Phone2] from mst_case_master t6 JOIN mst_patient t7 on t7.sz_patient_id = t6.sz_patient_id AND t7.sz_company_id = @sz_company_id";
                    sQuery += " left join mst_insurance_company t8 on t8.sz_insurance_id = t6.sz_insurance_id where t6.sz_case_id not in (select t2.sz_case_id from mst_procedure_group t1";
                    sQuery += " left join txn_doctor_speciality t5 on t5.sz_procedure_group_id = t1.sz_procedure_group_id AND t1.sz_company_id = @sz_company_id";
                    sQuery += " left join mst_doctor t3 on t3.sz_doctor_id = t5.sz_doctor_id AND t3.sz_company_id = @sz_company_id";
                    sQuery += " left join txn_calendar_event t2 on t2.sz_doctor_id = t3.sz_doctor_id AND t2.sz_company_id = @sz_company_id";
                    sQuery += " where t1.sz_company_id = @sz_company_id and t1.sz_procedure_group_id IN (@sz_specialty_id)";
                    sQuery += " and t2.sz_case_id IS NOT NULL AND t2.dt_event_date between @dt_start AND @dt_end ) AND t6.sz_company_id = @sz_company_id ";

                    if (p_oCasetype != null && p_oCasetype.ID != null && p_oCasetype.ID != "NA")
                        sQuery += "and t6.sz_case_type_id=@sz_case_type_id";
                    if (p_oCaseStatus != null && p_oCaseStatus.ID != null && p_oCaseStatus.ID != "NA")
                        sQuery += " and t6.sz_case_status_id=@sz_case_status_id";
                }

                if (oDTCarrier != null && oDTCarrier.Rows.Count > 0)
                {
                    sQuery += " AND (t6.sz_insurance_id IN (SELECT sz_insurance_id FROM mst_insurance_company tc where tc.sz_company_id = @sz_company_id and ltrim(rtrim(tc.sz_insurance_name)) IN (SELECT ltrim(rtrim(sz_name)) FROM @tvp_carrier)))";
                }

                sQuery += " order by t6.sz_case_id ";

                SqlCommand com = new SqlCommand(sQuery, oConnection);
                com.Transaction = oTransaction;
                com.Parameters.AddWithValue("@sz_company_id", p_oAccount.ID);
                com.Parameters.AddWithValue("@dt_start", p_StartDate.Date);
                com.Parameters.AddWithValue("@dt_end", p_EndDate.Date);
                com.Parameters.AddWithValue("@sz_specialty_id", p_oSpecialty[0].ID);
                com.Parameters.AddWithValue("@sz_case_type_id", p_oCasetype.ID);
                com.Parameters.AddWithValue("@sz_case_status_id", p_oCaseStatus.ID);

                if (p_oProcedure != null && p_oProcedure.Count > 0)
                {
                    SqlParameter tvpParamProcedure = com.Parameters.AddWithValue(
                        "@tvp_procedure", oDTProcedure);
                    tvpParamProcedure.SqlDbType = SqlDbType.Structured;
                    tvpParamProcedure.TypeName = gbmodel.procedure.type.TypeProcedure.GetTypeName();
                }

                if (oDTCarrier != null)
                {
                    SqlParameter tvpParamCarrier = com.Parameters.AddWithValue(
                        "@tvp_carrier", oDTCarrier);
                    tvpParamCarrier.SqlDbType = SqlDbType.Structured;
                    tvpParamCarrier.TypeName = gbmodel.carrier.type.TypeCarrier.GetTypeName();
                }

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(ds);
                oTransaction.Commit();

                if (ds != null)
                {
                    if (ds.Tables[0] != null)
                    {
                        return ds.Tables[0];
                    }
                }
            }
            catch(Exception x)
            {
                oTransaction.Rollback();
            }
            finally
            {
                if(oConnection != null)
                {
                    oConnection.Close();
                    oConnection = null;
                }
            }
            return null;
        }
    }
}