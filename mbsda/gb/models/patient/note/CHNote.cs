﻿using System;
using System.Collections.Generic;
using System.Text;

namespace gb.mbs.da.model.patient.note
{
    public class CHNote : Note
    {
        public string DoctorSign { set; get; }
        public string PatientSign { set; get; }
        public string EventId { set; get; }
        public string DoctorName { set; get; }
        public string Date { set; get; }

        public string NoChangeInMyCondition { set; get; }
        public string ChangeInMyCondition { set; get; }
        public string MyConditionIsAboutSame { set; get; }
        public string Mild { set; get; }
        public string Moderate { set; get; }
        public string Severe { set; get; }
        public string VerySevere { set; get; }
        public string HeadacheRight { set; get; }
        public string HeadacheLeft { set; get; }
        public string HeadacheBoth { set; get; }
        public string NeckRight { set; get; }
        public string NeckLeft { set; get; }
        public string NeckBoth { set; get; }
        public string MidBackRight { set; get; }
        public string MidBackLeft { set; get; }
        public string MidBackBoth { set; get; }
        public string LowBackRight { set; get; }
        public string LowBackLeft { set; get; }
        public string LowBackBoth { set; get; }
        public string JawRight { set; get; }
        public string JawLeft { set; get; }
        public string JawBoth { set; get; }
        public string ShoulderRight { set; get; }
        public string ShoulderLeft { set; get; }
        public string ShoulderBoth { set; get; }
        public string ElbowRight { set; get; }
        public string ElbowLeft { set; get; }
        public string ElbowBoth { set; get; }
        public string WristRight { set; get; }
        public string WristLeft { set; get; }
        public string WristBoth { set; get; }
        public string HandRight { set; get; }
        public string HandLeft { set; get; }
        public string HandBoth { set; get; }
        public string FingersRight { set; get; }
        public string FingersLeft { set; get; }
        public string FingersBoth { set; get; }
        public string HipRight { set; get; }
        public string HipLeft { set; get; }
        public string HipBoth { set; get; }
        public string ThighRight { set; get; }
        public string ThighLeft { set; get; }
        public string ThighBoth { set; get; }
        public string KneeRight { set; get; }
        public string KneeLeft { set; get; }
        public string KneeBOTH { set; get; }
        public string LowerLegRight { set; get; }
        public string LowerLegLeft { set; get; }
        public string LowerLegBOTH { set; get; }
        public string FootRight { set; get; }
        public string FootLeft { set; get; }
        public string FootBoth { set; get; }
        public string ToesRight { set; get; }
        public string ToesLeft { set; get; }
        public string ToesBoth { set; get; }
        public string SubjectiveAdditionalComments { set; get; }
        public string CervicalFlex { set; get; }
        public string CervicalExt { set; get; }
        public string CervicalRtRot { set; get; }
        public string CervicalLftRot { set; get; }
        public string CervicalRTLATFlex { set; get; }
        public string CervicalLftLatFlex { set; get; }
        public string ThoracicFlex { set; get; }
        public string ThoracicRtRot { set; get; }
        public string ThoracicLftRot { set; get; }
        public string LumbarFlex { set; get; }
        public string LumbarExt { set; get; }
        public string LumbarRtLatFlex { set; get; }
        public string LumbarLftLatFlex { set; get; }
        public string ObjectiveAdditionalComments { set; get; }
        public string AssessmentNoChange { set; get; }
        public string AssessmentImproving { set; get; }
        public string AssessmentFlairUp { set; get; }
        public string AssessmentAsExpected { set; get; }
        public string AssessmentSlowerThanExpected { set; get; }
        public string StopAllActivities { set; get; }
        public string ReduceAllActivities { set; get; }
        public string ResumeLightActivities { set; get; }
        public string ResumeAllActivities { set; get; }
        public string TreatmentCervical { set; get; }
        public string TreatmentThoracic { set; get; }
        public string TreatmentLumbar { set; get; }
        public string TreatmentDorsoLumbar { set; get; }
        public string TreatmentSacroiliac { set; get; }
        public string TreatmentTempromandibularJoint { set; get; }
        public string PatientSignPath { set; get; }
        public string DoctorSignPath { set; get; }
        public string BarcodePath { set; get; }
        public string ProcedureCode98940 { set; get; }
        public string ProcedureCode98941 { set; get; }
        public string ProcedureCode99203 { set; get; }
        public string ProcedureCode9921_1 { set; get; }
        public string ProcedureCode9921_2 { set; get; }
        public string Spasm { set; get; }
        public string Edema { set; get; }
        public string TriggerPoints { set; get; }
        public string Fixation { set; get; }
        public string Cervical { set; get; }
        public string Thoracic { set; get; }
        public string Lumbar { set; get; }
        public string Sacrum { set; get; }
        public string Pelvis { set; get; }
        public string Trapezius { set; get; }
        public string Rhomboids { set; get; }
        public string Piriformis { set; get; }
        public string Quad { set; get; }
        public string SternocleiDomastoid { set; get; }
        public string Ql { set; get; }
        public string LevatorScapulae { set; get; }
        public string CervicalParaSpinal { set; get; }
        public string ThoracicParaSpinal { set; get; }
        public string LumbarParaSpinal { set; get; }
        public string ThoracicExt { set; get; }
        public string ThoracicRtLatFlex { set; get; }
        public string ThoracicLftLatFlex { set; get; }
        public string LumbarRtRot { set; get; }
        public string LumbarLftRot { set; get; }
        public string TreatmentCervicoThoracic { set; get; }
        public string TreatmentLumboPelvic { set; get; }
        public string PainLevelHeadache { set; get; }
        public string PainLevelNeck { set; get; }
        public string PainLevelMidBack { set; get; }
        public string PainLevelLowBack { set; get; }
        public string PainLevelJaw { set; get; }
        public string PainLevelShoulder { set; get; }
        public string PainLevelElbow { set; get; }
        public string PainLevelWrist { set; get; }
        public string PainLevelHand { set; get; }
        public string PainLevelFingers { set; get; }
        public string PainLevelHip { set; get; }
        public string PainLevelThigh { set; get; }
        public string PainLevelKnee { set; get; }
        public string PainLevelLowerLeg { set; get; }
        public string PainLevelFoot { set; get; }
        public string PainLevelToes { set; get; }
        public string ChiroPracticAdj { set; get; }
        public string Cmt12 { set; get; }
        public string Cmt34 { set; get; }
        public string Extremity { set; get; }
        public string Extremity1 { set; get; }
        public string TherapeuticsModalities { set; get; }
        public string MyOFascialRelease { set; get; }
        public string MechanicalTRaction{ set; get; }
        public string EmsIf { set; get; }
        public string Hotcold { set; get; }
        public string UltraSound { set; get; }
        public string MassageTherapy { set; get; }
        public string KineticActivity { set; get; }
        public string Other { set; get; }
        public string Other1 { set; get; }
        public string Location { set; get; }
        public string Intensity { set; get; }
        public string Min { set; get; }
        public string Rxn { set; get; }
        public string InIt { set; get; }
        public string HomeInst { set; get; }
        public string IceTherapy { set; get; }
        public string TrAction { set; get; }
        public string TrAction1 { set; get; }
        public string SupportSleeBack { set; get; }
        public string SupportSleeSide { set; get; }
        public string PersonalStretch { set; get; }
        public string Neck { set; get; }
        public string Back { set; get; }
        public string Ue { set; get; }
        public string Lf { set; get; }
        public string WholeBody { set; get; }
        public string WholeBody1 { set; get; }
        public string ContCarePlan { set; get; }
        public string ModifyCarePlan { set; get; }
        public string RsReExam { set; get; }
        public string ReffrralEval { set; get; }
        public string ReffrralEval1 { set; get; }
        public string ReffrralDiag { set; get; }
        public string ReffrralDiag1 { set; get; }
        public string Notes { set; get; }
        public string ObjectiveFindings { set; get; }
    }
}
