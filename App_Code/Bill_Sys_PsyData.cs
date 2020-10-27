using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using ProcedureCode;
using Bill_Sys_Psy_DAO;


namespace WCPDFData
{
    class WCPDFData
    {
        public PsychologistBill GetWCPdfData(DataSet ds, DataSet ds1)
        {
            PsychologistBill pb = new PsychologistBill();
            ArrayList arr = new ArrayList();
            ArrayList diagArr = new ArrayList();

            //PsychDiagCode objDiag = new PsychDiagCode();

            string Diagcodes = "";
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                if (Diagcodes == "")
                    Diagcodes = (i + 1).ToString();
                else
                    Diagcodes = Diagcodes + "," + (i + 1).ToString();
                diagArr.Add(ds1.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE"].ToString());
            }
            pb.DiagAr = diagArr;
            //DiaCode.Length
            //pb.DiagCodeA = DiaCode[3];
            //pb.DiagCodeB = DiaCode[7];
            //pb.DiagCodeC = DiaCode[11];
            //pb.DiagCodeD = DiaCode[15];


            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {
                ProcedureCodeDetails procobj = new ProcedureCodeDetails();
                procobj.frMM = ds1.Tables[1].Rows[i]["MONTH"].ToString();
                procobj.frDD = ds1.Tables[1].Rows[i]["DAY"].ToString();
                procobj.frYY = ds1.Tables[1].Rows[i]["YEAR"].ToString();
                procobj.toMM = ds1.Tables[1].Rows[i]["TO_MONTH"].ToString();
                procobj.toDD = ds1.Tables[1].Rows[i]["TO_DAY"].ToString();
                procobj.toYY = ds1.Tables[1].Rows[i]["TO_YEAR"].ToString();
                procobj.placeOfService = ds1.Tables[1].Rows[i]["PLACE_OF_SERVICE"].ToString();
                procobj.leaveBlank = "";
                procobj.CPT_HCPCS = ds1.Tables[1].Rows[i]["SZ_PROCEDURE_CODE"].ToString();
                procobj.modifier = ds1.Tables[1].Rows[i]["SZ_MODIFIER"].ToString();
                procobj.submodifier = "";
                procobj.diagCode = Diagcodes;
                procobj.charges = ds1.Tables[1].Rows[i]["FL_AMOUNT"].ToString();
                procobj.subcharges = "";
                procobj.daysUnit = ds1.Tables[1].Rows[i]["I_UNIT"].ToString();
                procobj.COB = "";
                procobj.zip = ds1.Tables[1].Rows[i]["ZIP_CODE"].ToString();
                arr.Add(procobj);

            }
            pb.ar = arr;



            pb.PPOYes = Convert.ToString(ds.Tables[0].Rows[0]["bt_service_provider"]); //
            pb.AttendingTime = Convert.ToString(ds.Tables[0].Rows[0]["i_attending_psy"]);
            //pb._48HR = Convert.ToString(ds.Tables[0].Rows[0]["BT_48HR"]); //
            //pb._15Days = Convert.ToString(ds.Tables[0].Rows[0]["BT_15Day"]);//
            //pb._90Days = Convert.ToString(ds.Tables[0].Rows[0]["BT_90Day"]);//
            pb.WCBCaseNo = Convert.ToString(ds.Tables[0].Rows[0]["WCB CASE NORow1"]);
            pb.CarrierCaseNo = Convert.ToString(ds.Tables[0].Rows[0]["CARRIER CASE NORow1"]);
            pb.DateOfInjury = Convert.ToString(ds.Tables[0].Rows[0]["DATE OF INJURYRow1"]);
            pb.Time = Convert.ToString(ds.Tables[0].Rows[0]["TIME OF INJURY"]);
            pb.AddressWhereInjuryOccured = Convert.ToString(ds.Tables[0].Rows[0]["ADDRESS WHERE INJURY OCCURRED CITY TOWN OR VILLAGERow1"]);
            pb.InjuredPersonSSN = Convert.ToString(ds.Tables[0].Rows[0]["INJURED PERSONS SOCIAL SECURITY NUMBERRow1"]);
            pb.InjuredPersonFirstName = Convert.ToString(ds.Tables[0].Rows[0]["PATIENT FIRSTNAME"]);
            pb.InjuredPersonMI = Convert.ToString(ds.Tables[0].Rows[0]["PATIENT MI"]);
            pb.InjuredPersonLastName = Convert.ToString(ds.Tables[0].Rows[0]["PATIENT LASTNAME"]);
            pb.InjuredPersonAddress = Convert.ToString(ds.Tables[0].Rows[0]["ADDRESS OF PATIENT"]);
            pb.InjuredPersonTelephoneNo = Convert.ToString(ds.Tables[0].Rows[0]["TELEPHONE NO"]);
            pb.EmployerName = Convert.ToString(ds.Tables[0].Rows[0]["First Name Middle Initial Last NameEMPLOYER"]);
            pb.EmployerAddress = Convert.ToString(ds.Tables[0].Rows[0]["ADDRESS Include Apt NoEMPLOYER"]);
            pb.PatientDateOfBirth = Convert.ToString(ds.Tables[0].Rows[0]["PATIENTS DATE OF BIRTH"]);
            pb.InsuranceCarrierName = Convert.ToString(ds.Tables[0].Rows[0]["INSURANCE CARRIER"]);
            pb.InsuranceCarrierAddress = Convert.ToString(ds.Tables[0].Rows[0]["ADDRESS OF INSURANCE"]);
            pb.ReferringPhysicianName = Convert.ToString(ds.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN"]);
            pb.ReferringPhysicianAddress = Convert.ToString(ds.Tables[0].Rows[0]["SZ_REFFERING_PHYSICIAN_ADDRESS"]);
            pb.ReferringPhysicianTelephoneNo = Convert.ToString(ds.Tables[0].Rows[0]["SZ_REFFERING_TELEPHONE_NO"]);


            pb.TreatmentUnderCHKBox = Convert.ToString(ds.Tables[0].Rows[0]["i_vfbl_or_vawbl"]);
            pb.PreviousHistoryDate = Convert.ToString(ds.Tables[0].Rows[0]["dt_historyof_injury"]);
            pb.IncidentHistory = Convert.ToString(ds.Tables[0].Rows[0]["sz_incident_description"]);
            pb.HistoryPreExistingInjury = Convert.ToString(ds.Tables[0].Rows[0]["sz_preexisting_psy"]);
            pb.ReferralCHKBox = Convert.ToString(ds.Tables[0].Rows[0]["bt_referal_for"]);
            pb.YourEvaluation = Convert.ToString(ds.Tables[0].Rows[0]["sz_evalution"]);
            pb.PatientConditionAndProgress = Convert.ToString(ds.Tables[0].Rows[0]["sz_patient_condition"]);
            pb.PlannedFutureTreatment = Convert.ToString(ds.Tables[0].Rows[0]["sz_authentication_req"]);
            pb.TreatmentB2CHKBox = Convert.ToString(ds.Tables[0].Rows[0]["bt_authentication_req"]);
            pb.DateOfVisitOnWichReportIsBased = Convert.ToString(ds.Tables[0].Rows[0]["dt_dateof_visited"]);
            pb.DateOfFirstVisit = Convert.ToString(ds.Tables[0].Rows[0]["dt_first_dateof_visit"]);
            pb.PatientSeenAgainCHKBox = Convert.ToString(ds.Tables[0].Rows[0]["bt_will_patient_see_again"]);
            pb.WhenWillPatientSeenAgain = Convert.ToString(ds.Tables[0].Rows[0]["dt_yes_seen"]);
            pb.PatientAttendingDoctor = Convert.ToString(ds.Tables[0].Rows[0]["i_no_seen"]);
            pb.PatienWorking = Convert.ToString(ds.Tables[0].Rows[0]["i_is_patient_working"]);
            pb.ResumedLimitedWork = Convert.ToString(ds.Tables[0].Rows[0]["sz_yes_patient_working"]);
            pb.ResumedRegularWork = Convert.ToString(ds.Tables[0].Rows[0]["sz_patient_regular_work"]);
            pb.OccurenceCauseInjuryCHKBox = Convert.ToString(ds.Tables[0].Rows[0]["i_sustained"]);
            pb.AdditionalPertinentInformation = Convert.ToString(ds.Tables[0].Rows[0]["sz_additional_info"]);
            //string tempDaig = Convert.ToString(ds.Tables[0].Rows[0]["SZ_DIGNOSIS"]);
            //string[] DiaCode = tempDaig.Split(' ');
            //pb.DiagCodeA=DiaCode[3];
            //pb.DiagCodeB = DiaCode[7];
            //pb.DiagCodeC = DiaCode[11];
            //pb.DiagCodeD = DiaCode[15];
            pb.FederalTaxIDNO = Convert.ToString(ds.Tables[0].Rows[0]["SZ_FEDERAL_TAX_ID"]);
            pb.ssn = Convert.ToString(ds.Tables[0].Rows[0]["ssn"]);
            pb.ein = Convert.ToString(ds.Tables[0].Rows[0]["ein"]);
            pb.WCBAuthorizationNo = " ";//
            pb.PatientAccountNo = Convert.ToString(ds.Tables[0].Rows[0]["sz_patient_acc_number"]);
            //pb.TotalCharges = Convert.ToString(ds.Tables[0].Rows[0]["Total Charges"]);
            pb.AmountPaid = "0.00";// Convert.ToString(ds.Tables[0].Rows[0]["PAID_AMOUNT"]);//
            //pb.BalanceDue = Convert.ToString(ds.Tables[0].Rows[0]["BALANCE"]);//
            pb.SignatureOfTreatingPsychologist = " ";//
            pb.PsychologistNameAdressPhoneNO = Convert.ToString(ds.Tables[0].Rows[0]["Therapists Name Address  Phone No"]);
            pb.BillingNameAdressPhoneNO = Convert.ToString(ds.Tables[0].Rows[0]["Therapists Billing Name Address  Phone No"]);


            return (pb);

        }
    }
}
