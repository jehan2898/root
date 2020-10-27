using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using gbmodel = gb.mbs.da.model;
using gb.mbs.da.service.util;

namespace gb.mbs.da.services.patient.note
{
    public abstract class SrvNote
    {
        protected List<gbmodel.patient.note.Note> Select(List<gbmodel.bill.Bill> p_lstBill, gbmodel.patient.note.EnumNoteType p_eNoteType)
        {
            //common connection string
            //common logic to build bill number string
            //common logic to invoke procedure
            //different procedure names
            //common logic to return dataset
            DataSet dsNote = new DataSet();
            switch (p_eNoteType)
            {
                case gbmodel.patient.note.EnumNoteType.AC:
                    return SelectACNote(p_lstBill);
                case gbmodel.patient.note.EnumNoteType.PT:
                    return SelectPTNote(p_lstBill);
                case gbmodel.patient.note.EnumNoteType.CH:
                    return SelectCHNote(p_lstBill);
            }
            return null;
        }

        private List<gbmodel.patient.note.Note> SelectACNote(List<gbmodel.bill.Bill> p_oData)
        {
            List<gbmodel.patient.note.Note> oList = new List<gbmodel.patient.note.Note>();
            List<gbmodel.patient.note.Note> oListAc = new List<gbmodel.patient.note.Note>();

            SqlConnection sqlConnection = null;
            sqlConnection = new SqlConnection(DBUtil.ConnectionString);
            sqlConnection.Open();

            //TODO
            //Remove the per bill call from below and instead call the procedure sp_select_note_ac
            //Pass the table parameter with the bill number and company id columns
            //Sort the returned dataset in the order of bill number + event id
            //Loop through the dataset and print the note per bill

            //Take the procedure out of the for loop. Call the procedure only once.

            try
            {
                for (var i = 0; i < p_oData.Count; i++)
                {
                    SqlCommand sqlCommand = null;
                    sqlCommand = new SqlCommand("SP_PDFBILLS_MASTERBILLING_New", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@SZ_BILL_NUMBER", p_oData[i].Number));
                    sqlCommand.Parameters.Add(new SqlParameter("@SZ_COMPANY_ID", p_oData[i].Patient.Account.ID));
                    sqlCommand.Parameters.Add(new SqlParameter("@FLAG", "PDF"));

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        gbmodel.patient.note.ACNote oAcNote = new gbmodel.patient.note.ACNote();

                        string str = "";
                        int num = 0;
                        foreach (DataRow dr_list in ds.Tables[0].Rows)
                        {
                            if ((str == "" ? true : str != ds.Tables[0].Rows[i][1].ToString()))
                            {
                                oAcNote.Patient = new gbmodel.patient.Patient();
                                oAcNote.Carrier = new gbmodel.carrier.Carrier();

                                oAcNote.Patient.Name = string.Concat(dr_list["PatientFirstName"].ToString(), "", dr_list["PATIENT_LAST_NAME"].ToString());
                                if (dr_list["CASE_NO"] != DBNull.Value)
                                    oAcNote.Patient.CaseNo = Convert.ToInt32(dr_list["CaseNo"].ToString());
                                else
                                    oAcNote.Patient.CaseNo = 0;
                                if (dr_list["DT_DATE_OF_ACCIDENT"] != DBNull.Value)
                                    oAcNote.Patient.DOA = dr_list["DT_DATE_OF_ACCIDENT"].ToString();
                                else
                                    oAcNote.Patient.DOA = string.Empty;
                                if (dr_list["CLAIM_NUMBER"] != DBNull.Value)
                                    oAcNote.Patient.ClaimNumber = dr_list["CLAIM_NUMBER"].ToString();
                                else
                                    oAcNote.Patient.ClaimNumber = string.Empty;
                                if (dr_list["INSURANCE_NAME"] != DBNull.Value)
                                    oAcNote.Carrier.Name = dr_list["INSURANCE_NAME"].ToString();
                                else
                                    oAcNote.Carrier.Name = string.Empty;
                                if (dr_list["VISIT_DATE"] != DBNull.Value)
                                    oAcNote.Date = dr_list["VISIT_DATE"].ToString();
                                else
                                    oAcNote.Date = string.Empty;
                                if (dr_list["PATIENT_SIGN"] != DBNull.Value)
                                    oAcNote.PatientSign = dr_list["PATIENT_SIGN"].ToString();
                                else
                                    oAcNote.PatientSign = string.Empty;
                                if (dr_list["DOCTOR_SIGN"] != DBNull.Value)
                                    oAcNote.DoctorSign = dr_list["DOCTOR_SIGN"].ToString();
                                else
                                    oAcNote.DoctorSign = string.Empty;
                                if (dr_list["bt_patient_reported"] != DBNull.Value)
                                    oAcNote.PatientReported = dr_list["bt_patient_reported"].ToString();
                                else
                                    oAcNote.PatientReported = string.Empty;
                                if (dr_list["bt_patient_trated"] != DBNull.Value)
                                    oAcNote.PatientTreated = dr_list["bt_patient_trated"].ToString();
                                else
                                    oAcNote.PatientTreated = string.Empty;
                                if (dr_list["bt_pain_grades"] != DBNull.Value)
                                    oAcNote.PainGrades = dr_list["bt_pain_grades"].ToString();
                                else
                                    oAcNote.PainGrades = string.Empty;
                                if (dr_list["bt_head"] != DBNull.Value)
                                    oAcNote.Head = dr_list["bt_head"].ToString();
                                else
                                    oAcNote.Head = string.Empty;
                                if (dr_list["bt_neck"] != DBNull.Value)
                                    oAcNote.Neck = dr_list["bt_neck"].ToString();
                                else
                                    oAcNote.Neck = string.Empty;
                                if (dr_list["bt_throcic"] != DBNull.Value)
                                    oAcNote.Thoracic = dr_list["bt_throcic"].ToString();
                                else
                                    oAcNote.Thoracic = string.Empty;
                                if (dr_list["bt_lumber"] != DBNull.Value)
                                    oAcNote.Lumbar = dr_list["bt_lumber"].ToString();
                                else
                                    oAcNote.Lumbar = string.Empty;
                                if (dr_list["bt_rl_sh"] != DBNull.Value)
                                    oAcNote.RLShoulder = dr_list["bt_rl_sh"].ToString();
                                else
                                    oAcNote.RLShoulder = string.Empty;
                                if (dr_list["bt_rl_wrist"] != DBNull.Value)
                                    oAcNote.RLWrist = dr_list["bt_rl_wrist"].ToString();
                                else
                                    oAcNote.RLWrist = string.Empty;
                                if (dr_list["bt_rl_elow"] != DBNull.Value)
                                    oAcNote.RLElbow = dr_list["bt_rl_elow"].ToString();
                                else
                                    oAcNote.RLElbow = string.Empty;
                                if (dr_list["bt_rl_knee"] != DBNull.Value)
                                    oAcNote.RLRLKnee = dr_list["bt_rl_knee"].ToString();
                                else
                                    oAcNote.RLRLKnee = string.Empty;
                                if (dr_list["bt_rl_ankle"] != DBNull.Value)
                                    oAcNote.RLRLAnkle = dr_list["bt_rl_ankle"].ToString();
                                else
                                    oAcNote.RLRLAnkle = string.Empty;
                                if (dr_list["bt_rl_hil"] != DBNull.Value)
                                    oAcNote.RLHip = dr_list["bt_rl_hil"].ToString();
                                else
                                    oAcNote.RLHip = string.Empty;
                                if (dr_list["sz_doctornote"] != DBNull.Value)
                                    oAcNote.Notes = dr_list["sz_doctornote"].ToString();
                                else
                                    oAcNote.Notes = string.Empty;
                                if (dr_list["bt_patient_states"] != DBNull.Value)
                                    oAcNote.PatientStates = dr_list["bt_patient_states"].ToString();
                                else
                                    oAcNote.PatientStates = string.Empty;
                                if (dr_list["bt_patient_states_little"] != DBNull.Value)
                                    oAcNote.PatientStates1 = dr_list["bt_patient_states_little"].ToString();
                                else
                                    oAcNote.PatientStates1 = string.Empty;
                                if (dr_list["bt_patient_states_much"] != DBNull.Value)
                                    oAcNote.PatientStates2 = dr_list["bt_patient_states_much"].ToString();
                                else
                                    oAcNote.PatientStates2 = string.Empty;
                                if (dr_list["bt_patient_tolerated"] != DBNull.Value)
                                    oAcNote.ChkPatientTolerated = dr_list["bt_patient_tolerated"].ToString();
                                else
                                    oAcNote.ChkPatientTolerated = string.Empty;
                                if (dr_list["bt_patient_therapy"] != DBNull.Value)
                                    oAcNote.Continue = dr_list["bt_patient_therapy"].ToString();
                                else
                                    oAcNote.Continue = string.Empty;
                                if (dr_list["DOCTOR_NOTE"] != DBNull.Value)
                                    oAcNote.Acupuncture = dr_list["DOCTOR_NOTE"].ToString();
                                else
                                    oAcNote.Acupuncture = string.Empty;
                                if (dr_list["bt_acupuncture"] != DBNull.Value)
                                    oAcNote.PatientTreatedAcupuncture = dr_list["bt_acupuncture"].ToString();
                                else
                                    oAcNote.PatientTreatedAcupuncture = string.Empty;
                                if (dr_list["bt_electro"] != DBNull.Value)
                                    oAcNote.PatientTreatedElectro = dr_list["bt_electro"].ToString();
                                else
                                    oAcNote.PatientTreatedElectro = string.Empty;
                                if (dr_list["bt_moxa"] != DBNull.Value)
                                    oAcNote.PatientTreatedMoxa = dr_list["bt_moxa"].ToString();
                                else
                                    oAcNote.PatientTreatedMoxa = string.Empty;

                                if (dr_list["bt_cupping"] != DBNull.Value)
                                    oAcNote.PatientTreatedCupping = dr_list["bt_cupping"].ToString();
                                else
                                    oAcNote.PatientTreatedCupping = string.Empty;
                                if (dr_list["sz_doctor_name"] != DBNull.Value)
                                    oAcNote.DoctorName = dr_list["sz_doctor_name"].ToString();
                                else
                                    oAcNote.DoctorName = string.Empty;

                                oAcNote.EventId = ds.Tables[0].Rows[i][1].ToString();

                                str = ds.Tables[0].Rows[i][1].ToString();
                                string str1 = "";
                                for (int j = num; j < ds.Tables[0].Rows.Count; j++)
                                {
                                    if ((str == "" ? true : str == ds.Tables[0].Rows[j][1].ToString()))
                                    {
                                        if (!(str1 == ""))
                                        {
                                            string[] strArrays = new string[] { str1, ",", ds.Tables[0].Rows[num]["CODE"].ToString(), "-", ds.Tables[0].Rows[num]["DESCRIPTION"].ToString() };
                                            str1 = string.Concat(strArrays);
                                        }
                                        else
                                        {
                                            str1 = string.Concat(ds.Tables[0].Rows[num]["CODE"].ToString(), "-", ds.Tables[0].Rows[num]["DESCRIPTION"].ToString());
                                        }
                                        num++;
                                    }
                                }
                                oAcNote.Code = str1;

                                oListAc.Add(oAcNote);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return oListAc;
        }

        private List<gbmodel.patient.note.Note> SelectCHNote(List<gbmodel.bill.Bill> p_oData)
        {
            List<gbmodel.patient.note.Note> oList = new List<gbmodel.patient.note.Note>();
            List<gbmodel.patient.note.Note> oListCh = new List<gbmodel.patient.note.Note>();

            SqlConnection sqlConnection = null;
            sqlConnection = new SqlConnection(DBUtil.ConnectionString);
            sqlConnection.Open();
            try
            {
                for (var i = 0; i < p_oData.Count; i++)
                {
                    SqlCommand sqlCommand = null;
                    sqlCommand = new SqlCommand("SP_PDFBILLS_MASTERBILLING", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@SZ_BILL_NUMBER", p_oData[i].Number));
                    sqlCommand.Parameters.Add(new SqlParameter("@SZ_COMPANY_ID", p_oData[i].Patient.Account.ID));
                    sqlCommand.Parameters.Add(new SqlParameter("@FLAG", "GET_EVENT_ID"));

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds != null)
                    {
                        gbmodel.patient.note.CHNote oCHNote = new gbmodel.patient.note.CHNote();
                        oCHNote.EventId = ds.Tables[0].Rows[0]["I_EVENT_ID"].ToString();
                        if (oCHNote.EventId != null)
                        {
                            SqlCommand sqlCommand2 = null;
                            sqlCommand2 = new SqlCommand("SP_CHAIRO_NOTES", sqlConnection);
                            sqlCommand2.CommandType = CommandType.StoredProcedure;
                            sqlCommand2.Parameters.Add(new SqlParameter("@I_EVENT_ID", oCHNote.EventId));
                            SqlDataAdapter da2 = new SqlDataAdapter(sqlCommand2);
                            DataSet dsChView = new DataSet();
                            da2.Fill(dsChView);

                            foreach (DataRow dr_list in dsChView.Tables[0].Rows)
                            {
                                oCHNote.Patient = new gbmodel.patient.Patient();
                                oCHNote.Carrier = new gbmodel.carrier.Carrier();

                                if (dr_list["SZ_PATIENT_NAME"] != DBNull.Value)
                                    oCHNote.Patient.Name = dr_list["SZ_PATIENT_NAME"].ToString();
                                else
                                    oCHNote.Patient.Name = string.Empty;

                                if (dr_list["SZ_CASE_NO"] != DBNull.Value)
                                    oCHNote.Patient.CaseNo = Convert.ToInt32(dr_list["SZ_CASE_NO"].ToString());
                                else
                                    oCHNote.Patient.CaseNo = 0;

                                oCHNote.DoctorName = dr_list["SZ_DOCTOR_NAME"].ToString();
                                oCHNote.Date = dr_list["DT_DATE"].ToString();
                                oCHNote.NoChangeInMyCondition = dr_list["BT_NO_CHANGE_IN_MY_CONDITION"].ToString();
                                oCHNote.ChangeInMyCondition = dr_list["BT_CHANGE_IN_MY_CONDITION"].ToString();
                                oCHNote.MyConditionIsAboutSame = dr_list["BT_MY_CONDITION_IS_ABOUT_SAME"].ToString();
                                oCHNote.Mild = dr_list["BT_MILD"].ToString();
                                oCHNote.Moderate = dr_list["BT_MODERATE"].ToString();
                                oCHNote.Severe = dr_list["BT_SEVERE"].ToString();
                                oCHNote.VerySevere = dr_list["BT_VERY_SEVERE"].ToString();
                                oCHNote.HeadacheRight = dr_list["BT_HEADACHE_RIGHT"].ToString();
                                oCHNote.HeadacheLeft = dr_list["BT_HEADACHE_LEFT"].ToString();
                                oCHNote.HeadacheBoth = dr_list["BT_HEADACHE_BOTH"].ToString();
                                oCHNote.NeckRight = dr_list["BT_NECK_RIGHT"].ToString();
                                oCHNote.NeckLeft = dr_list["BT_NECK_LEFT"].ToString();
                                oCHNote.NeckBoth = dr_list["BT_NECK_BOTH"].ToString();
                                oCHNote.MidBackRight = dr_list["BT_MID_BACK_RIGHT"].ToString();
                                oCHNote.MidBackLeft = dr_list["BT_MID_BACK_LEFT"].ToString();
                                oCHNote.MidBackBoth = dr_list["BT_MID_BACK_BOTH"].ToString();
                                oCHNote.LowBackRight = dr_list["BT_LOW_BACK_RIGHT"].ToString();
                                oCHNote.LowBackLeft = dr_list["BT_LOW_BACK_LEFT"].ToString();
                                oCHNote.LowBackBoth = dr_list["BT_LOW_BACK_BOTH"].ToString();
                                oCHNote.JawRight = dr_list["BT_JAW_RIGHT"].ToString();
                                oCHNote.JawLeft = dr_list["BT_JAW_LEFT"].ToString();
                                oCHNote.JawBoth = dr_list["BT_JAW_BOTH"].ToString();
                                oCHNote.ShoulderRight = dr_list["BT_SHOULDER_RIGHT"].ToString();
                                oCHNote.ShoulderLeft = dr_list["BT_SHOULDER_LEFT"].ToString();
                                oCHNote.ShoulderBoth = dr_list["BT_SHOULDER_BOTH"].ToString();
                                oCHNote.ElbowRight = dr_list["BT_ELBOW_RIGHT"].ToString();
                                oCHNote.ElbowLeft = dr_list["BT_ELBOW_LEFT"].ToString();
                                oCHNote.ElbowBoth = dr_list["BT_ELBOW_BOTH"].ToString();
                                oCHNote.WristRight = dr_list["BT_WRIST_RIGHT"].ToString();
                                oCHNote.WristLeft = dr_list["BT_WRIST_LEFT"].ToString();
                                oCHNote.WristBoth = dr_list["BT_WRIST_BOTH"].ToString();
                                oCHNote.HandRight = dr_list["BT_HAND_RIGHT"].ToString();
                                oCHNote.HandLeft = dr_list["BT_HAND_LEFT"].ToString();
                                oCHNote.HandBoth = dr_list["BT_HAND_BOTH"].ToString();
                                oCHNote.FingersRight = dr_list["BT_FINGERS_RIGHT"].ToString();
                                oCHNote.FingersLeft = dr_list["BT_FINGERS_LEFT"].ToString();
                                oCHNote.FingersBoth = dr_list["BT_FINGERS_BOTH"].ToString();
                                oCHNote.HipRight = dr_list["BT_HIP_RIGHT"].ToString();
                                oCHNote.HipLeft = dr_list["BT_HIP_LEFT"].ToString();
                                oCHNote.HipBoth = dr_list["BT_HIP_BOTH"].ToString();
                                oCHNote.ThighRight = dr_list["BT_THIGH_RIGHT"].ToString();
                                oCHNote.ThighLeft = dr_list["BT_THIGH_LEFT"].ToString();
                                oCHNote.ThighBoth = dr_list["BT_THIGH_BOTH"].ToString();
                                oCHNote.KneeRight = dr_list["BT_KNEE_RIGHT"].ToString();
                                oCHNote.KneeLeft = dr_list["BT_KNEE_LEFT"].ToString();
                                oCHNote.KneeBOTH = dr_list["BT_KNEE_BOTH"].ToString();
                                oCHNote.LowerLegRight = dr_list["BT_LOWER_LEG_RIGHT"].ToString();
                                oCHNote.LowerLegLeft = dr_list["BT_LOWER_LEG_LEFT"].ToString();
                                oCHNote.LowerLegBOTH = dr_list["BT_LOWER_LEG_BOTH"].ToString();
                                oCHNote.FootRight = dr_list["BT_FOOT_RIGHT"].ToString();
                                oCHNote.FootLeft = dr_list["BT_FOOT_LEFT"].ToString();
                                oCHNote.FootBoth = dr_list["BT_FOOT_BOTH"].ToString();
                                oCHNote.ToesRight = dr_list["BT_TOES_RIGHT"].ToString();
                                oCHNote.ToesLeft = dr_list["BT_TOES_LEFT"].ToString();
                                oCHNote.ToesBoth = dr_list["BT_TOES_BOTH"].ToString();
                                oCHNote.SubjectiveAdditionalComments = dr_list["SZ_SUBJECTIVE_ADDITIONAL_COMMENTS"].ToString();
                                oCHNote.CervicalFlex = dr_list["BT_CERVICAL_FLEX"].ToString();
                                oCHNote.CervicalExt = dr_list["BT_CERVICAL_EXT"].ToString();
                                oCHNote.CervicalRtRot = dr_list["BT_CERVICAL_RT_ROT"].ToString();
                                oCHNote.CervicalLftRot = dr_list["BT_CERVICAL_LFT_ROT"].ToString();
                                oCHNote.CervicalRTLATFlex = dr_list["BT_CERVICAL_RT_LAT_FLEX"].ToString();
                                oCHNote.CervicalLftLatFlex = dr_list["BT_CERVICAL_LFT_LAT_FLEX"].ToString();
                                oCHNote.ThoracicFlex = dr_list["BT_THORACIC_FLEX"].ToString();
                                oCHNote.ThoracicRtRot = dr_list["BT_THORACIC_RT_ROT"].ToString();
                                oCHNote.ThoracicLftRot = dr_list["BT_THORACIC_LFT_ROT"].ToString();
                                oCHNote.LumbarFlex = dr_list["BT_LUMBAR_FLEX"].ToString();
                                oCHNote.LumbarExt = dr_list["BT_LUMBAR_EXT"].ToString();
                                oCHNote.LumbarRtLatFlex = dr_list["BT_LUMBAR_RT_LAT_FLEX"].ToString();
                                oCHNote.LumbarLftLatFlex = dr_list["BT_LUMBAR_LFT_LAT_FLEX"].ToString();
                                oCHNote.ObjectiveAdditionalComments = dr_list["SZ_OBJECTIVE_ADDITIONAL_COMMENTS"].ToString();
                                oCHNote.AssessmentNoChange = dr_list["BT_ASSESSMENT_NO_CHANGE"].ToString();
                                oCHNote.AssessmentImproving = dr_list["BT_ASSESSMENT_IMPROVING"].ToString();
                                oCHNote.AssessmentFlairUp = dr_list["BT_ASSESSMENT_FLAIR_UP"].ToString();
                                oCHNote.AssessmentAsExpected = dr_list["BT_ASSESSMENT_AS_EXPECTED"].ToString();
                                oCHNote.AssessmentSlowerThanExpected = dr_list["BT_ASSESSMENT_SLOWER_THAN_EXPECTED"].ToString();
                                oCHNote.StopAllActivities = dr_list["BT_STOP_ALL_ACTIVITES"].ToString();
                                oCHNote.ReduceAllActivities = dr_list["BT_REDUCE_ALL_ACTIVITES"].ToString();
                                oCHNote.ResumeLightActivities = dr_list["BT_RESUME_LIGHT_ACTIVITES"].ToString();
                                oCHNote.ResumeAllActivities = dr_list["BT_RESUME_ALL_ACTIVITES"].ToString();
                                oCHNote.TreatmentCervical = dr_list["BT_TREATMENT_CERVICAL"].ToString();
                                oCHNote.TreatmentThoracic = dr_list["BT_TREATMENT_THORACIC"].ToString();
                                oCHNote.TreatmentLumbar = dr_list["BT_TREATMENT_LUMBAR"].ToString();
                                oCHNote.TreatmentDorsoLumbar = dr_list["BT_TREATMENT_DORSOLUMBAR"].ToString();
                                oCHNote.TreatmentSacroiliac = dr_list["BT_TREATMENT_SACROILIAC"].ToString();
                                oCHNote.TreatmentTempromandibularJoint = dr_list["BT_TREATMENT_TEMPROMANDIBULAR_JOINT"].ToString();
                                oCHNote.PatientSignPath = dr_list["SZ_PATIENT_SIGN_PATH"].ToString();
                                oCHNote.DoctorSignPath = dr_list["SZ_DOCTOR_SIGN_PATH"].ToString();
                                oCHNote.BarcodePath = dr_list["SZ_BARCODE_PATH"].ToString();
                                oCHNote.ProcedureCode98940 = dr_list["BT_PROCEDURE_CODE_98940"].ToString();
                                oCHNote.ProcedureCode98941 = dr_list["BT_PROCEDURE_CODE_98941"].ToString();
                                oCHNote.ProcedureCode99203 = dr_list["BT_PROCEDURE_CODE_99203"].ToString();
                                oCHNote.ProcedureCode9921_1 = dr_list["BT_PROCEDURE_CODE_9921_1"].ToString();
                                oCHNote.ProcedureCode9921_2 = dr_list["BT_PROCEDURE_CODE_9921_2"].ToString();
                                oCHNote.Spasm = dr_list["BT_SPASM"].ToString();
                                oCHNote.Edema = dr_list["BT_EDEMA"].ToString();
                                oCHNote.TriggerPoints = dr_list["BT_TRIGGER_POINTS"].ToString();
                                oCHNote.Fixation = dr_list["BT_FIXATION"].ToString();
                                oCHNote.Cervical = dr_list["BT_CERVICAL"].ToString();
                                oCHNote.Thoracic = dr_list["BT_THORACIC"].ToString();
                                oCHNote.Lumbar = dr_list["BT_LUMBAR"].ToString();
                                oCHNote.Sacrum = dr_list["BT_SACRUM"].ToString();
                                oCHNote.Pelvis = dr_list["BT_PELVIS"].ToString();
                                oCHNote.Trapezius = dr_list["BT_TRAPEZIUS"].ToString();
                                oCHNote.Rhomboids = dr_list["BT_RHOMBOIDS"].ToString();
                                oCHNote.Piriformis = dr_list["BT_PIRIFORMIS"].ToString();
                                oCHNote.Quad = dr_list["BT_QUAD"].ToString();
                                oCHNote.SternocleiDomastoid = dr_list["BT_STERNOCLEIDOMASTOID"].ToString();
                                oCHNote.Ql = dr_list["BT_QL"].ToString();
                                oCHNote.LevatorScapulae = dr_list["BT_LEVATOR_SCAPULAE"].ToString();
                                oCHNote.CervicalParaSpinal = dr_list["BT_CERVICAL_PARASPINAL"].ToString();
                                oCHNote.ThoracicParaSpinal = dr_list["BT_THORACIC_PARASPINAL"].ToString();
                                oCHNote.LumbarParaSpinal = dr_list["BT_LUMBAR_PARASPINAL"].ToString();
                                oCHNote.ThoracicExt = dr_list["BT_THORACIC_EXT"].ToString();
                                oCHNote.ThoracicRtLatFlex = dr_list["BT_THORACIC_RT_LAT_FLEX"].ToString();
                                oCHNote.ThoracicLftLatFlex = dr_list["BT_THORACIC_LFT_LAT_FLEX"].ToString();
                                oCHNote.LumbarRtRot = dr_list["BT_LUMBAR_RT_ROT"].ToString();
                                oCHNote.LumbarLftRot = dr_list["BT_LUMBAR_LFT_ROT"].ToString();
                                oCHNote.TreatmentCervicoThoracic = dr_list["BT_TREATMENT_CERVICOTHORACIC"].ToString();
                                oCHNote.TreatmentLumboPelvic = dr_list["BT_TREATMENT_LUMBOPELVIC"].ToString();
                                oCHNote.PainLevelHeadache = dr_list["SZ_PAIN_LEVEL_HEADACHE"].ToString();
                                oCHNote.PainLevelNeck = dr_list["SZ_PAIN_LEVEL_NECK"].ToString();
                                oCHNote.PainLevelMidBack = dr_list["SZ_PAIN_LEVEL_MID_BACK"].ToString();
                                oCHNote.PainLevelLowBack = dr_list["SZ_PAIN_LEVEL_LOW_BACK"].ToString();
                                oCHNote.PainLevelJaw = dr_list["SZ_PAIN_LEVEL_JAW"].ToString();
                                oCHNote.PainLevelShoulder = dr_list["SZ_PAIN_LEVEL_SHOULDER"].ToString();
                                oCHNote.PainLevelElbow = dr_list["SZ_PAIN_LEVEL_ELBOW"].ToString();
                                oCHNote.PainLevelWrist = dr_list["SZ_PAIN_LEVEL_WRIST"].ToString();
                                oCHNote.PainLevelHand = dr_list["SZ_PAIN_LEVEL_HAND"].ToString();
                                oCHNote.PainLevelFingers = dr_list["SZ_PAIN_LEVEL_FINGERS"].ToString();
                                oCHNote.PainLevelHip = dr_list["SZ_PAIN_LEVEL_HIP"].ToString();
                                oCHNote.PainLevelThigh = dr_list["SZ_PAIN_LEVEL_THIGH"].ToString();
                                oCHNote.PainLevelKnee = dr_list["SZ_PAIN_LEVEL_KNEE"].ToString();
                                oCHNote.PainLevelLowerLeg = dr_list["SZ_PAIN_LEVEL_LOWER_LEG"].ToString();
                                oCHNote.PainLevelFoot = dr_list["SZ_PAIN_LEVEL_FOOT"].ToString();
                                oCHNote.PainLevelToes = dr_list["SZ_PAIN_LEVEL_TOES"].ToString();
                                oCHNote.ChiroPracticAdj = dr_list["BT_CHIROPRACTIC_ADJ"].ToString();
                                oCHNote.Cmt12 = dr_list["BT_CMT_12"].ToString();
                                oCHNote.Cmt34 = dr_list["BT_CMT_34"].ToString();
                                oCHNote.Extremity = dr_list["BT_EXTREMITY"].ToString();
                                oCHNote.Extremity1 = dr_list["SZ_EXTREMITY"].ToString();
                                oCHNote.TherapeuticsModalities = dr_list["BT_THERAPEUTICS_MODALITIES"].ToString();
                                oCHNote.MyOFascialRelease = dr_list["BT_MYOFASCIAL_RELEASE"].ToString();
                                oCHNote.MechanicalTRaction = dr_list["BT_MECHANICAL_TRACTION"].ToString();
                                oCHNote.EmsIf = dr_list["BT_EMS_IF"].ToString();
                                oCHNote.Hotcold = dr_list["BT_HOT_COLD"].ToString();
                                oCHNote.UltraSound = dr_list["BT_ULTRASOUND"].ToString();
                                oCHNote.MassageTherapy = dr_list["BT_MASSAGE_THERAPY"].ToString();
                                oCHNote.KineticActivity = dr_list["BT_KINETIC_ACTIVITY"].ToString();
                                oCHNote.Other = dr_list["BT_OTHER"].ToString();
                                oCHNote.Other1 = dr_list["SZ_OTHER"].ToString();
                                oCHNote.Location = dr_list["SZ_LOCATION"].ToString();
                                oCHNote.Intensity = dr_list["SZ_INTENSITY"].ToString();
                                oCHNote.Min = dr_list["SZ_MIN"].ToString();
                                oCHNote.Rxn = dr_list["SZ_RXN"].ToString();
                                oCHNote.InIt = dr_list["SZ_INIT"].ToString();
                                oCHNote.HomeInst = dr_list["BT_HOME_INST"].ToString();
                                oCHNote.IceTherapy = dr_list["BT_ICE_THERAPY"].ToString();
                                oCHNote.TrAction = dr_list["BT_TRACTION"].ToString();
                                oCHNote.TrAction1 = dr_list["SZ_TRACTION"].ToString();
                                oCHNote.SupportSleeBack = dr_list["BT_SUPPORT_SLEE_BACK"].ToString();
                                oCHNote.SupportSleeSide = dr_list["BT_SUPPORT_SLEE_SIDE"].ToString();
                                oCHNote.PersonalStretch = dr_list["BT_PERSONAL_STRETCH"].ToString();
                                oCHNote.Neck = dr_list["BT_NECK"].ToString();
                                oCHNote.Back = dr_list["BT_BACK"].ToString();
                                oCHNote.Ue = dr_list["BT_UE"].ToString();
                                oCHNote.Lf = dr_list["BT_LE"].ToString();
                                oCHNote.WholeBody = dr_list["BT_WHOLE_BODY"].ToString();
                                oCHNote.WholeBody1 = dr_list["SZ_WHOLE_BODY"].ToString();
                                oCHNote.ContCarePlan = dr_list["BT_CONT_CARE_PLAN"].ToString();
                                oCHNote.ModifyCarePlan = dr_list["BT_MODIFY_CARE_PLAN"].ToString();
                                oCHNote.RsReExam = dr_list["BT_RS_REEXAM"].ToString();
                                oCHNote.ReffrralEval = dr_list["BT_REFERRAL_EVAL"].ToString();
                                oCHNote.ReffrralEval1 = dr_list["SZ_REFERRAL_EVAL"].ToString();
                                oCHNote.ReffrralDiag = dr_list["BT_REFERRAL_DIAG"].ToString();
                                oCHNote.ReffrralDiag1 = dr_list["SZ_REFERRAL_DIAG"].ToString();
                                oCHNote.Notes = dr_list["SZ_NOTES"].ToString();

                                oListCh.Add(oCHNote);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return oListCh;
        }

        private List<gbmodel.patient.note.Note> SelectPTNote(List<gbmodel.bill.Bill> p_oData)
        {
            List<gbmodel.patient.note.Note> oList = new List<gbmodel.patient.note.Note>();
            List<gbmodel.patient.note.Note> oListPT = new List<gbmodel.patient.note.Note>();

            SqlConnection sqlConnection = null;
            sqlConnection = new SqlConnection(DBUtil.ConnectionString);
            sqlConnection.Open();
            try
            {
                for (var i = 0; i < p_oData.Count; i++)
                {

                    SqlCommand sqlCommand = null;
                    sqlCommand = new SqlCommand("SP_PDFBILLS_MASTERBILLING", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@SZ_BILL_NUMBER", p_oData[i].Number));
                    sqlCommand.Parameters.Add(new SqlParameter("@SZ_COMPANY_ID", p_oData[i].Patient.Account.ID));
                    sqlCommand.Parameters.Add(new SqlParameter("@FLAG", "PDF"));
                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    sqlCommand = new SqlCommand("exec SP_GET_PT_PATIENT_INFO @SZ_CASE_ID='" + ds.Tables[0].Rows[0]["CASEID"].ToString() + "'", sqlConnection);
                    sqlCommand.CommandTimeout = 0;
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    DataSet dsPatient = new DataSet();
                    adapter.Fill(dsPatient);

                    sqlCommand = new SqlCommand("exec SP_PDFBILLS_MASTERBILLING  @SZ_BILL_NUMBER='" + p_oData[i].Number + "', @FLAG='GET_EVENT_ID'", sqlConnection);
                    sqlCommand.CommandTimeout = 0;
                    adapter = new SqlDataAdapter(sqlCommand);
                    DataSet dsEvent = new DataSet();
                    adapter.Fill(dsEvent);

                    if (dsEvent != null)
                    {
                        gbmodel.patient.note.PTNote oPTNote = new gbmodel.patient.note.PTNote();
                        oPTNote.EventId = dsEvent.Tables[0].Rows[0]["I_EVENT_ID"].ToString();

                        if (oPTNote.EventId != null)
                        {
                            SqlCommand sqlCommand2 = null;
                            sqlCommand2 = new SqlCommand("SP_PT_NOTES", sqlConnection);
                            sqlCommand2.CommandType = CommandType.StoredProcedure;
                            sqlCommand2.Parameters.Add(new SqlParameter("@I_EVENT_ID", oPTNote.EventId));
                            sqlCommand2.Parameters.Add(new SqlParameter("@SZ_COMPANY_ID", p_oData[i].Patient.Account.ID));
                            SqlDataAdapter da2 = new SqlDataAdapter(sqlCommand2);
                            DataSet dsPTView = new DataSet();
                            da2.Fill(dsPTView);

                            foreach (DataRow dr_list in dsPTView.Tables[0].Rows)
                            {
                                oPTNote.Patient = new gbmodel.patient.Patient();
                                oPTNote.Carrier = new gbmodel.carrier.Carrier();

                                oPTNote.Patient.Name = dr_list["SZ_PATIENT_NAME"].ToString();
                                if (dr_list["SZ_CASE_NO"] != DBNull.Value)
                                    oPTNote.Patient.CaseNo = Convert.ToInt32(dr_list["SZ_CASE_NO"].ToString());
                                else
                                    oPTNote.Patient.CaseNo = 0;
                                if (dsPatient.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"] != DBNull.Value)
                                    oPTNote.Patient.DOA = Convert.ToDateTime(dsPatient.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString()).ToString("MM-dd-yyyy");
                                else
                                    oPTNote.Patient.DOA = string.Empty;
                                if (dsPatient.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"] != DBNull.Value)
                                    oPTNote.Patient.ClaimNumber = dsPatient.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                                else
                                    oPTNote.Patient.ClaimNumber = string.Empty;
                                if (dsPatient.Tables[0].Rows[0]["SZ_INSURANCE_NAME"] != DBNull.Value)
                                    oPTNote.Carrier.Name = dsPatient.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                                else
                                    oPTNote.Carrier.Name = string.Empty;

                                oPTNote.PrecAutions = dr_list["SZ_PRECAUTIONS"].ToString();
                                oPTNote.DtDate = dr_list["DT_DATE"].ToString();
                                oPTNote.Complaints = dr_list["SZ_COMPLAINTS"].ToString();
                                oPTNote.Objective1 = dr_list["BT_OBJECTIVE1"].ToString();
                                oPTNote.Objective2 = dr_list["BT_OBJECTIVE2"].ToString();
                                oPTNote.Objective3 = dr_list["BT_OBJECTIVE3"].ToString();
                                oPTNote.PatientTolerated = dr_list["BT_PATIENT_TOLERATED"].ToString();
                                oPTNote.AssotherComments = dr_list["BT_ASS_OTHER_COMMENTS"].ToString();
                                oPTNote.OtherComments = dr_list["SZ_OTHER_COMMENTS"].ToString();
                                oPTNote.CotinueTherapy = dr_list["BT_COTINUE_THERAPY"].ToString();
                                oPTNote.PlanOtherComments = dr_list["BT_PLAN_OTHER_COMMENTS"].ToString();
                                oPTNote.PlanOherComments2 = dr_list["SZ_BT_PLAN_OHER_COMMENTS"].ToString();
                                oPTNote.Mild = dr_list["BT_MILD"].ToString();
                                oPTNote.Moderate = dr_list["BT_MODERATE"].ToString();
                                oPTNote.Severe = dr_list["BT_SEVERE"].ToString();
                                oPTNote.Verysevere = dr_list["BT_VERY_SEVERE"].ToString();
                                oPTNote.Headacheright = dr_list["BT_HEADACHE_RIGHT"].ToString();
                                oPTNote.Headacheleft = dr_list["BT_HEADACHE_LEFT"].ToString();
                                oPTNote.Headacheboth = dr_list["BT_HEADACHE_BOTH"].ToString();
                                oPTNote.Neckright = dr_list["BT_NECK_RIGHT"].ToString();
                                oPTNote.Neckleft = dr_list["BT_NECK_LEFT"].ToString();
                                oPTNote.Neckboth = dr_list["BT_NECK_BOTH"].ToString();
                                oPTNote.Midbackright = dr_list["BT_MID_BACK_RIGHT"].ToString();
                                oPTNote.Midbackleft = dr_list["BT_MID_BACK_LEFT"].ToString();
                                oPTNote.Midbackboth = dr_list["BT_MID_BACK_BOTH"].ToString();
                                oPTNote.Lowbackright = dr_list["BT_LOW_BACK_RIGHT"].ToString();
                                oPTNote.Lowbackleft = dr_list["BT_LOW_BACK_LEFT"].ToString();
                                oPTNote.Lowbackboth = dr_list["BT_LOW_BACK_BOTH"].ToString();
                                oPTNote.Jawright = dr_list["BT_JAW_RIGHT"].ToString();
                                oPTNote.Jawleft = dr_list["BT_JAW_LEFT"].ToString();
                                oPTNote.Jawboth = dr_list["BT_JAW_BOTH"].ToString();
                                oPTNote.ShoulderRight = dr_list["BT_SHOULDER_RIGHT"].ToString();
                                oPTNote.ShoulderLeft = dr_list["BT_SHOULDER_LEFT"].ToString();
                                oPTNote.ShoulderBoth = dr_list["BT_SHOULDER_BOTH"].ToString();
                                oPTNote.ElbowRight = dr_list["BT_ELBOW_RIGHT"].ToString();
                                oPTNote.ElbowLeft = dr_list["BT_ELBOW_LEFT"].ToString();
                                oPTNote.Elbowboth = dr_list["BT_ELBOW_BOTH"].ToString();
                                oPTNote.WristRight = dr_list["BT_WRIST_RIGHT"].ToString();
                                oPTNote.WristLeft = dr_list["BT_WRIST_LEFT"].ToString();
                                oPTNote.WristBoth = dr_list["BT_WRIST_BOTH"].ToString();
                                oPTNote.HandRight = dr_list["BT_HAND_RIGHT"].ToString();
                                oPTNote.HandLeft = dr_list["BT_HAND_LEFT"].ToString();
                                oPTNote.HandBoth = dr_list["BT_HAND_BOTH"].ToString();
                                oPTNote.FingersRight = dr_list["BT_FINGERS_RIGHT"].ToString();
                                oPTNote.FingersLeft = dr_list["BT_FINGERS_LEFT"].ToString();
                                oPTNote.FingersBoth = dr_list["BT_FINGERS_BOTH"].ToString();
                                oPTNote.HipRight = dr_list["BT_HIP_RIGHT"].ToString();
                                oPTNote.HipLeft = dr_list["BT_HIP_LEFT"].ToString();
                                oPTNote.HipBoth = dr_list["BT_HIP_BOTH"].ToString();
                                oPTNote.ThighRight = dr_list["BT_THIGH_RIGHT"].ToString();
                                oPTNote.ThighLeft = dr_list["BT_THIGH_LEFT"].ToString();
                                oPTNote.ThighBoth = dr_list["BT_THIGH_BOTH"].ToString();
                                oPTNote.KneeRight = dr_list["BT_KNEE_RIGHT"].ToString();
                                oPTNote.KneeLeft = dr_list["BT_KNEE_LEFT"].ToString();
                                oPTNote.KneeBoth = dr_list["BT_KNEE_BOTH"].ToString();
                                oPTNote.LowerlegRight = dr_list["BT_LOWER_LEG_RIGHT"].ToString();
                                oPTNote.LowerlegLeft = dr_list["BT_LOWER_LEG_LEFT"].ToString();
                                oPTNote.LowerlegBoth = dr_list["BT_LOWER_LEG_BOTH"].ToString();
                                oPTNote.FootRight = dr_list["BT_FOOT_RIGHT"].ToString();
                                oPTNote.FootLeft = dr_list["BT_FOOT_LEFT"].ToString();
                                oPTNote.FootBoth = dr_list["BT_FOOT_BOTH"].ToString();
                                oPTNote.ToesRight = dr_list["BT_TOES_RIGHT"].ToString();
                                oPTNote.ToesLeft = dr_list["BT_TOES_LEFT"].ToString();
                                oPTNote.ToesBoth = dr_list["BT_TOES_BOTH"].ToString();
                                oPTNote.PatientSignPath = dr_list["SZ_PATIENT_SIGN_PATH"].ToString();
                                oPTNote.DoctorSignPath = dr_list["SZ_DOCTOR_SIGN_PATH"].ToString();
                                oPTNote.BarCodePath = dr_list["SZ_BARCODE_PATH"].ToString();
                                oPTNote.TreatmentHeadacheRight = dr_list["BT_TREATMENT_HEADACHE_RIGHT"].ToString();
                                oPTNote.TreatmentHeadacheLeft = dr_list["BT_TREATMENT_HEADACHE_LEFT"].ToString();
                                oPTNote.TreatmentHeadacheboth = dr_list["BT_TREATMENT_HEADACHE_BOTH"].ToString();
                                oPTNote.TreatmentNeckRight = dr_list["BT_TREATMENT_NECK_RIGHT"].ToString();
                                oPTNote.TreatmentNeckLeft = dr_list["BT_TREATMENT_NECK_LEFT"].ToString();
                                oPTNote.TreatmentNeckBoth = dr_list["BT_TREATMENT_NECK_BOTH"].ToString();
                                oPTNote.TreatmentMidbackRight = dr_list["BT_TREATMENT_MID_BACK_RIGHT"].ToString();
                                oPTNote.TreatmentMidbackLeft = dr_list["BT_TREATMENT_MID_BACK_LEFT"].ToString();
                                oPTNote.TreatmentMidbackBoth = dr_list["BT_TREATMENT_MID_BACK_BOTH"].ToString();
                                oPTNote.TreatmentLowbackRight = dr_list["BT_TREATMENT_LOW_BACK_RIGHT"].ToString();
                                oPTNote.TreatmentLowbackLeft = dr_list["BT_TREATMENT_LOW_BACK_LEFT"].ToString();
                                oPTNote.TreatmentLowbackBoth = dr_list["BT_TREATMENT_LOW_BACK_BOTH"].ToString();
                                oPTNote.TreatmentJawRight = dr_list["BT_TREATMENT_JAW_RIGHT"].ToString();
                                oPTNote.TreatmentJawLeft = dr_list["BT_TREATMENT_JAW_LEFT"].ToString();
                                oPTNote.TreatmentJawBoth = dr_list["BT_TREATMENT_JAW_BOTH"].ToString();
                                oPTNote.TreatmentShoulderRight = dr_list["BT_TREATMENT_SHOULDER_RIGHT"].ToString();
                                oPTNote.TreatmentShoulderLeft = dr_list["BT_TREATMENT_SHOULDER_LEFT"].ToString();
                                oPTNote.TreatmentShoulderboth = dr_list["BT_TREATMENT_SHOULDER_BOTH"].ToString();
                                oPTNote.TreatmentElbowRight = dr_list["BT_TREATMENT_ELBOW_RIGHT"].ToString();
                                oPTNote.TreatmentElbowLeft = dr_list["BT_TREATMENT_ELBOW_LEFT"].ToString();
                                oPTNote.TreatmentElbowBoth = dr_list["BT_TREATMENT_ELBOW_BOTH"].ToString();
                                oPTNote.TreatmentWristRight = dr_list["BT_TREATMENT_WRIST_RIGHT"].ToString();
                                oPTNote.TreatmentWristLeft = dr_list["BT_TREATMENT_WRIST_LEFT"].ToString();
                                oPTNote.TreatmentWristBoth = dr_list["BT_TREATMENT_WRIST_BOTH"].ToString();
                                oPTNote.TreatmentHandRight = dr_list["BT_TREATMENT_HAND_RIGHT"].ToString();
                                oPTNote.TreatmentHandLeft = dr_list["BT_TREATMENT_HAND_LEFT"].ToString();
                                oPTNote.TreatmentHandBoth = dr_list["BT_TREATMENT_HAND_BOTH"].ToString();
                                oPTNote.TreatmentFingersRight = dr_list["BT_TREATMENT_FINGERS_RIGHT"].ToString();
                                oPTNote.TreatmentFingersLeft = dr_list["BT_TREATMENT_FINGERS_LEFT"].ToString();
                                oPTNote.TreatmentFingersboth = dr_list["BT_TREATMENT_FINGERS_BOTH"].ToString();
                                oPTNote.TreatmentHipRight = dr_list["BT_TREATMENT_HIP_RIGHT"].ToString();
                                oPTNote.TreatmentHipLeft = dr_list["BT_TREATMENT_HIP_LEFT"].ToString();
                                oPTNote.TreatmentHipBoth = dr_list["BT_TREATMENT_HIP_BOTH"].ToString();
                                oPTNote.TreatmenttHighRight = dr_list["BT_TREATMENT_THIGH_RIGHT"].ToString();
                                oPTNote.TreatmenttHighLeft = dr_list["BT_TREATMENT_THIGH_LEFT"].ToString();
                                oPTNote.TreatmenttHighBoth = dr_list["BT_TREATMENT_THIGH_BOTH"].ToString();
                                oPTNote.TreatmentKneeRight = dr_list["BT_TREATMENT_KNEE_RIGHT"].ToString();
                                oPTNote.TreatmentkneeLeft = dr_list["BT_TREATMENT_KNEE_LEFT"].ToString();
                                oPTNote.TreatmentKneeBoth = dr_list["BT_TREATMENT_KNEE_BOTH"].ToString();
                                oPTNote.TreatmentLowerLegRight = dr_list["BT_TREATMENT_LOWER_LEG_RIGHT"].ToString();
                                oPTNote.TreatmentLowerLegLeft = dr_list["BT_TREATMENT_LOWER_LEG_LEFT"].ToString();
                                oPTNote.TreatmentLowerLegboth = dr_list["BT_TREATMENT_LOWER_LEG_BOTH"].ToString();
                                oPTNote.TreatmentFootRight = dr_list["BT_TREATMENT_FOOT_RIGHT"].ToString();
                                oPTNote.TreatmentFootLeft = dr_list["BT_TREATMENT_FOOT_LEFT"].ToString();
                                oPTNote.TreatmentFootBoth = dr_list["BT_TREATMENT_FOOT_BOTH"].ToString();
                                oPTNote.TreatmentToesRight = dr_list["BT_TREATMENT_TOES_RIGHT"].ToString();
                                oPTNote.TreatmentToesLeft = dr_list["BT_TREATMENT_TOES_LEFT"].ToString();
                                oPTNote.TreatmentToesBoth = dr_list["BT_TREATMENT_TOES_BOTH"].ToString();
                                oPTNote.PainlevelHeadache = dr_list["SZ_PAIN_LEVEL_HEADACHE"].ToString();
                                oPTNote.PainlevelNeck = dr_list["SZ_PAIN_LEVEL_NECK"].ToString();
                                oPTNote.PainlevelMidback = dr_list["SZ_PAIN_LEVEL_MID_BACK"].ToString();
                                oPTNote.PainlevelLowback = dr_list["SZ_PAIN_LEVEL_LOW_BACK"].ToString();
                                oPTNote.PainlevelJaw = dr_list["SZ_PAIN_LEVEL_JAW"].ToString();
                                oPTNote.PainlevelShoulder = dr_list["SZ_PAIN_LEVEL_SHOULDER"].ToString();
                                oPTNote.PainlevelElbow = dr_list["SZ_PAIN_LEVEL_ELBOW"].ToString();
                                oPTNote.PainlevelWrist = dr_list["SZ_PAIN_LEVEL_WRIST"].ToString();
                                oPTNote.PainlevelHand = dr_list["SZ_PAIN_LEVEL_HAND"].ToString();
                                oPTNote.PainlevelFingers = dr_list["SZ_PAIN_LEVEL_FINGERS"].ToString();
                                oPTNote.PainlevelHip = dr_list["SZ_PAIN_LEVEL_HIP"].ToString();
                                oPTNote.PainlevelThigh = dr_list["SZ_PAIN_LEVEL_THIGH"].ToString();
                                oPTNote.PainlevelKnee = dr_list["SZ_PAIN_LEVEL_KNEE"].ToString();
                                oPTNote.PainlevelLowerleg = dr_list["SZ_PAIN_LEVEL_LOWER_LEG"].ToString();
                                oPTNote.PainlevelFoot = dr_list["SZ_PAIN_LEVEL_FOOT"].ToString();
                                oPTNote.PainlevelToes = dr_list["SZ_PAIN_LEVEL_TOES"].ToString();
                                oPTNote.ArmRight = dr_list["BT_ARM_RIGHT"].ToString();
                                oPTNote.ArmLeft = dr_list["BT_ARM_LEFT"].ToString();
                                oPTNote.ArmBoth = dr_list["BT_ARM_BOTH"].ToString();
                                oPTNote.AnkleRight = dr_list["BT_ANKLE_RIGHT"].ToString();
                                oPTNote.AnkleLeft = dr_list["BT_ANKLE_LEFT"].ToString();
                                oPTNote.AnkleBoth = dr_list["BT_ANKLE_BOTH"].ToString();
                                oPTNote.ForeArmRight = dr_list["BT_FORE_ARM_RIGHT"].ToString();
                                oPTNote.ForeArmLeft = dr_list["BT_FORE_ARM_LEFT"].ToString();
                                oPTNote.ForeArmBoth = dr_list["BT_FORE_ARM_BOTH"].ToString();
                                oPTNote.PainLevelArm = dr_list["SZ_PAIN_LEVEL_ARM"].ToString();
                                oPTNote.PainLevelAnkle = dr_list["SZ_PAIN_LEVEL_ANKLE"].ToString();
                                oPTNote.PainLevelForeArm = dr_list["SZ_PAIN_LEVEL_FORE_ARM"].ToString();
                                oPTNote.TreatmentArmRight = dr_list["BT_TREATMENT_ARM_RIGHT"].ToString();
                                oPTNote.TreatmentArmLeft = dr_list["BT_TREATMENT_ARM_LEFT"].ToString();
                                oPTNote.TreatmentArmBoth = dr_list["BT_TREATMENT_ARM_BOTH"].ToString();
                                oPTNote.TreatmentForeArmRight = dr_list["BT_TREATMENT_FORE_ARM_RIGHT"].ToString();
                                oPTNote.TreatmentForeArmLeft = dr_list["BT_TREATMENT_FORE_ARM_LEFT"].ToString();
                                oPTNote.TreatmentForeArmBoth = dr_list["BT_TREATMENT_FORE_ARM_BOTH"].ToString();
                                oPTNote.TreatmentAnkleRight = dr_list["BT_TREATMENT_ANKLE_RIGHT"].ToString();
                                oPTNote.TreatmentAnkleLeft = dr_list["BT_TREATMENT_ANKLE_LEFT"].ToString();
                                oPTNote.TreatmentAnkleBoth = dr_list["BT_TREATMENT_ANKLE_BOTH"].ToString();
                                oPTNote.DoctorName = dr_list["sz_doctor_name"].ToString();

                                oListPT.Add(oPTNote);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return oListPT;
        }


        public abstract void PrintNote();
    }
}