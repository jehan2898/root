using System;
using System.Collections.Generic;
using System.Text;
using gbmodel = gb.mbs.da.model;
using System.Data;
using System.Data.SqlClient;
using gb.mbs.da.service.util;

namespace gb.mbs.da.services.office.report
{
    public class SrvReferringOfficeReport
    {
        private string sSQLCon = dataaccess.ConnectionManager.GetConnectionString(null);

        public DataTable Select(model.account.Account p_oAccount, model.appointment.Appointment p_oAppointment, List<model.office.Office> p_oOffice, List<model.specialty.Specialty> p_oSpecialty, List<model.physician.Physician> p_lstReferringDoctor, List<model.carrier.Carrier> p_oCarrier, List<model.carriergroup.CarrierGroup> p_oCarrierGroup, List<model.procedure.Procedure> p_oProcedure, List<model.provider.Provider> p_lstProvider)
        {
            SqlConnection oConnection = new SqlConnection(DBUtil.ConnectionString);
            oConnection.Open();
            SqlTransaction oTransaction = null;

            try
            {
                oTransaction = oConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                DataSet ds = new DataSet();

                DataTable oDTReferringDoctor = null;
                if (p_lstReferringDoctor != null && p_lstReferringDoctor.Count > 0)
                {
                    oDTReferringDoctor = gbmodel.physician.type.TypePhysician.FillDBType(p_lstReferringDoctor);
                }

                DataTable oDTSpecialty = null;
                if (p_oSpecialty != null && p_oSpecialty.Count > 0)
                {
                    oDTSpecialty = gbmodel.specialty.type.TypeSpecialty.FillDBType(p_oSpecialty);
                }

                DataTable oDTOffice = null;
                if (p_oOffice != null && p_oOffice.Count > 0)
                {
                    oDTOffice = gbmodel.office.type.TypeOffice.FillDBType(p_oOffice);
                }

                DataTable oDTCarrier = null;
                if (p_oCarrier != null && p_oCarrier.Count > 0)
                {
                    oDTCarrier = gbmodel.carrier.type.TypeCarrier.FillDBType(p_oCarrier);
                }

                DataTable oDTCarrierGroup = null;
                
                oDTCarrierGroup = gbmodel.carriergroup.type.TypeCarrierGroup.FillDBType(p_oCarrierGroup);
               
                //Procedure Codes

                DataTable oDTProcedure = null;
                if (p_oProcedure != null && p_oProcedure.Count > 0)
                {
                    oDTProcedure = gbmodel.procedure.type.TypeProcedure.FillDBType(p_oProcedure);
                }

                // Provider

                DataTable oDTProvider = null;
                if (p_lstProvider != null && p_lstProvider.Count > 0)
                {
                    oDTProvider = gbmodel.provider.type.TypeProvider.FillDBType(p_lstProvider);
                }

                string sQuery = null;

                sQuery = " SELECT DISTINCT t2.sz_case_no [CaseNo],t3.sz_patient_last_name + ', ' + t3.sz_patient_first_name [PatientName],";
                sQuery += " t4.sz_case_type_name [CaseType],t5.sz_insurance_id [InsuranceID],t5.sz_insurance_name [InsuranceName],dt_event_date [AppointmentDate],";
                sQuery += " t1.i_status [AppointmentStatus],CASE WHEN t1.i_status = 2 THEN 'Completed'	WHEN t1.i_status = 0 THEN 'Scheduled' END [Appointment Status] , CASE WHEN ISNULL(t1.bt_status, 0) = 1 	THEN 'Billed' ELSE 'UnBilled'END [IsBilled],";
                sQuery += " t8.sz_bill_number [BillNumber],ISNULL(t6.sz_doctor_name, 'N/A') [ReferringDoctor],ISNULL(t7.sz_office, 'N/A') [ReferringOffice],";
                sQuery += " dbo.fnc_get_procedure_codes_selected(t1.i_event_id,@tvp_procedure) [EventProcedures],dbo.fnc_get_procedure_sum_for_event(t1.i_event_id,@tvp_procedure) [ProcedureSum],t8.flt_bill_amount [BillAmount]";
                sQuery += " FROM txn_calendar_event t1 JOIN mst_case_master t2 ON t2.sz_case_id = t1.sz_case_id";
                sQuery += " JOIN mst_patient t3 ON t3.sz_patient_id = t2.sz_patient_id 	JOIN mst_case_type t4 ON t4.sz_case_type_id = t2.sz_case_type_id ";
                sQuery += " JOIN mst_office t7 ON t7.sz_office_id = t1.sz_reffering_office_id AND t1.sz_reffering_office_id IN (SELECT sz_office_id FROM @tvp_office) ";
                sQuery += " JOIN mst_doctor t6 ON t6.sz_doctor_id = t1.sz_reffering_doctor_id ";

                // add referring doctor only if selected
                if (p_lstReferringDoctor != null && p_lstReferringDoctor.Count > 0)
                {
                    sQuery += "AND t1.sz_reffering_doctor_id IN (SELECT sz_doctor_id FROM @tvp_doctor) ";
                }
                sQuery += " JOIN mst_doctor t9 ON t9.sz_doctor_id = t1.sz_doctor_id AND t9.sz_doctor_id IN (select sz_doctor_id from txn_doctor_speciality";
                sQuery += " WHERE txn_doctor_speciality.sz_procedure_group_id IN (SELECT sz_specialty_id FROM @tvp_specialty)";
                sQuery += " AND sz_company_id = @sz_company_id) AND t9.sz_doctor_id IN (SELECT sz_doctor_id FROM mst_doctor WHERE sz_office_id IN (SELECT sz_provider_id FROM @tvp_provider)) ";
                sQuery += " LEFT JOIN mst_insurance_company t5 ON t5.sz_insurance_id = t2.sz_insurance_id";
                sQuery += " LEFT JOIN txn_bill_transactions t8 on t8.sz_bill_number = t1.sz_bill_number	";

                //when procedure code is selected 
                if (oDTProcedure != null && oDTProcedure.Rows.Count > 0)
                {
                    sQuery += "JOIN txn_calender_event_prpcedure t10 ON t10.i_event_id = t1.i_event_id AND (t10.sz_proc_code IN (SELECT sz_procedure_id FROM @tvp_procedure))";
                }
                    
                sQuery += " WHERE t1.sz_company_id = @sz_company_id AND t1.sz_reffering_office_id IS NOT NULL ";

                sQuery += " AND t1.sz_reffering_office_id <> ''";
                
                // when start date and end date is selected
                if (p_oAppointment.Date!=null && p_oAppointment.LastVisitDate!=null)
                {
                    if (p_oAppointment.Date != String.Empty && p_oAppointment.LastVisitDate != String.Empty)
                    {
                        sQuery += " AND t1.dt_event_date BETWEEN @dt_visit_start AND @dt_visit_end";
                    }                        
                }

                sQuery += " AND (t5.sz_insurance_id IN (SELECT sz_insurance_id FROM @tvp_carrier)";
                sQuery += " OR t5.sz_insurance_id IN (select sz_insurance_id from mst_insurance_groups where sz_company_id = @sz_company_id AND sz_group_name IN (select sz_name from @tvp_carriergroup)))";
                sQuery += " ORDER BY t1.dt_event_date DESC ";

                SqlCommand com = new SqlCommand(sQuery, oConnection);
                com.Transaction = oTransaction;
                com.Parameters.AddWithValue("@sz_company_id", p_oAccount.ID);

                if (p_oAppointment.Date != null && p_oAppointment.LastVisitDate != null)
                {
                    if (p_oAppointment.Date != String.Empty && p_oAppointment.LastVisitDate != String.Empty)
                    {
                        com.Parameters.AddWithValue("@dt_visit_start", p_oAppointment.Date);
                        com.Parameters.AddWithValue("@dt_visit_end", p_oAppointment.LastVisitDate);
                    }
                }

                if (oDTReferringDoctor != null && oDTReferringDoctor.Rows.Count > 0)
                {
                    SqlParameter tvpParamPhysician = com.Parameters.AddWithValue(
                        "@tvp_doctor", oDTReferringDoctor);
                    tvpParamPhysician.SqlDbType = SqlDbType.Structured;
                    tvpParamPhysician.TypeName = gbmodel.physician.type.TypePhysician.GetTypeName();
                }

                if (oDTSpecialty != null && oDTSpecialty.Rows.Count > 0)
                {
                    SqlParameter tvpParamSpecialty = com.Parameters.AddWithValue(
                        "@tvp_specialty", oDTSpecialty);
                    tvpParamSpecialty.SqlDbType = SqlDbType.Structured;
                    tvpParamSpecialty.TypeName = gbmodel.specialty.type.TypeSpecialty.GetTypeName();
                }

                if (oDTOffice != null && oDTOffice.Rows.Count > 0)
                {
                    SqlParameter tvpParamOffice = com.Parameters.AddWithValue(
                        "@tvp_office", oDTOffice);
                    tvpParamOffice.SqlDbType = SqlDbType.Structured;
                    tvpParamOffice.TypeName = gbmodel.office.type.TypeOffice.GetTypeName();
                }

                if (oDTCarrier != null)
                {
                    SqlParameter tvpParamCarrier = com.Parameters.AddWithValue(
                        "@tvp_carrier", oDTCarrier);
                    tvpParamCarrier.SqlDbType = SqlDbType.Structured;
                    tvpParamCarrier.TypeName = gbmodel.carrier.type.TypeCarrier.GetTypeName();
                }
               
                SqlParameter tvpParamcarriergroup = com.Parameters.AddWithValue(
                    "@tvp_carriergroup", oDTCarrierGroup);
                tvpParamcarriergroup.SqlDbType = SqlDbType.Structured;
                tvpParamcarriergroup.TypeName = gbmodel.carriergroup.type.TypeCarrierGroup.GetTypeName();

                if (p_oProcedure != null )
                {
                    SqlParameter tvpParamProcedure = com.Parameters.AddWithValue(
                        "@tvp_procedure", oDTProcedure);
                    tvpParamProcedure.SqlDbType = SqlDbType.Structured;
                    tvpParamProcedure.TypeName = gbmodel.procedure.type.TypeProcedure.GetTypeName();
                }

                if (p_lstProvider != null)
                {
                    SqlParameter tvpParamProcedure = com.Parameters.AddWithValue(
                        "@tvp_provider", oDTProvider);
                    tvpParamProcedure.SqlDbType = SqlDbType.Structured;
                    tvpParamProcedure.TypeName = gbmodel.provider.type.TypeProvider.GetTypeName();
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
            catch (Exception x)
            {
                oTransaction.Rollback();
                throw x;
            }
            finally
            {
                if (oConnection != null)
                {
                    oConnection.Close();
                    oConnection = null;
                }
            }
            return null;
        }    
    }
}