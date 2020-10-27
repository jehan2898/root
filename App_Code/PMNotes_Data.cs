using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace PMNotes
{
    class PMNotes_Data
    {
        public PMNotes_EVENTID_DAO GetEventID(DataSet ds)
        {
            ArrayList arr = new ArrayList();
            PMNotes_EVENTID_DAO ptobj = new PMNotes_EVENTID_DAO();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ptobj = new PMNotes_EVENTID_DAO();

                ptobj.SZ_BILL_NUMBER = Convert.ToString(ds.Tables[0].Rows[i]["SZ_BILL_NUMBER"]);
                ptobj.I_EVENT_ID = Convert.ToString(ds.Tables[0].Rows[i]["I_EVENT_ID"]);

                arr.Add(ptobj);
                ptobj.ar = arr;
            }

            return ptobj;
        }

        public PMNotes_DAO GetPMEventData(DataSet dsNotes)
        {
            PMNotes_DAO oPmNotesDao = new PMNotes_DAO();

            for (int i = 0; i < dsNotes.Tables[0].Rows.Count; i++)
            {
                if (dsNotes.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString() != null)
                {
                    oPmNotesDao.PatientName = dsNotes.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString() != null)
                {
                    oPmNotesDao.DateOfAccident = Convert.ToDateTime(dsNotes.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"]);
                }

                if (dsNotes.Tables[0].Rows[i]["dt_visit_date"].ToString() != null)
                {
                    oPmNotesDao.VisitDate = Convert.ToDateTime(dsNotes.Tables[0].Rows[i]["dt_visit_date"]);
                }

                if (dsNotes.Tables[0].Rows[i]["DT_DOB"].ToString() != null)
                {
                    oPmNotesDao.DOB = Convert.ToDateTime(dsNotes.Tables[0].Rows[i]["DT_DOB"]);
                }

                if (dsNotes.Tables[0].Rows[i]["PATIENT_ADDRESS"].ToString() != null)
                {
                    oPmNotesDao.PatientAddress = dsNotes.Tables[0].Rows[i]["PATIENT_ADDRESS"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtCurrentAllergies")
                {
                    oPmNotesDao.CurrentAllergies = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "ddlMedications")
                {
                    oPmNotesDao.CurrentMedications = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtHeight")
                {
                    oPmNotesDao.Height = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtWeight")
                {
                    oPmNotesDao.Weight = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtMedical")
                {
                    oPmNotesDao.Medical = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtInjuries")
                {
                    oPmNotesDao.Injuries = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtSurguries")
                {
                    oPmNotesDao.Surguries = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtSocialHistory")
                {
                    oPmNotesDao.SocialHistory = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtSmokingStatus")
                {
                    oPmNotesDao.SmokingStatus = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtAlcoholUse")
                {
                    oPmNotesDao.AlcoholUse = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtDrugUse")
                {
                    oPmNotesDao.DrugUse = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtEmployement")
                {
                    oPmNotesDao.Employement = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtChiefComplaint")
                {
                    oPmNotesDao.ChiefComplaint = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtReasonForVisit")
                {
                    oPmNotesDao.ReasonForVisit = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtROS")
                {
                    oPmNotesDao.ROS = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtPainDescription")
                {
                    oPmNotesDao.PainDescription = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "ddlPainLevelOne")
                {
                    oPmNotesDao.PainLevel = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "ddlLocation")
                {
                    oPmNotesDao.Location = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtQuality")
                {
                    oPmNotesDao.Quality = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtSeverity")
                {
                    oPmNotesDao.Severity = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtModifyingFactors")
                {
                    oPmNotesDao.ModifyingFactors = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtAssociatedSymptoms")
                {
                    oPmNotesDao.AssociatedSymptoms = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtPainDescription2")
                {
                    oPmNotesDao.PainDescriptionTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "ddlPainLevelTwo")
                {
                    oPmNotesDao.PainLevelTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "ddlLocationTwo")
                {
                    oPmNotesDao.LocationTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtQuality2")
                {
                    oPmNotesDao.QualityTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtSeverity2")
                {
                    oPmNotesDao.SeverityTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtModifyingFactors2")
                {
                    oPmNotesDao.ModifyingFactorsTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtAssociatedSymptoms2")
                {
                    oPmNotesDao.AssociatedSymptomsTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtModifyingFactorsExam")
                {
                    oPmNotesDao.ModifyingFactorsExam = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtAssociatedSymptomsExam")
                {
                    oPmNotesDao.AssociatedSymptomsExam = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtNeck")
                {
                    oPmNotesDao.Neck = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtBack")
                {
                    oPmNotesDao.Back = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtAssesment")
                {
                    oPmNotesDao.Assesment = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtDiagnosisCode")
                {
                    oPmNotesDao.DiagnosisCode = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtPlain")
                {
                    oPmNotesDao.Plain = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtFlexionNormalOne")
                {
                    oPmNotesDao.FlexionNormalOne = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtFlexionObservedOne")
                {
                    oPmNotesDao.FlexionObservedOne = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtFlexionNormalTwo")
                {
                    oPmNotesDao.FlexionNormalTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtFlexionObservedTwo")
                {
                    oPmNotesDao.FlexionObservedTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtExtensionNormalOne")
                {
                    oPmNotesDao.ExtensionNormalOne = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtExtensionObservedOne")
                {
                    oPmNotesDao.ExtensionObservedOne = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtExtensionNormalTwo")
                {
                    oPmNotesDao.ExtensionNormalTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtExtensionObservedTwo")
                {
                    oPmNotesDao.ExtensionObservedTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtRotationNormalOne")
                {
                    oPmNotesDao.RotationNormalOne = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtRotationObservedOne")
                {
                    oPmNotesDao.RotationObservedOne = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }

                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtRotationNormalTwo")
                {
                    oPmNotesDao.RotationNormalTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }
                if (dsNotes.Tables[0].Rows[i]["s_control_name"].ToString() == "txtRotationObservedTwo")
                {
                    oPmNotesDao.RotationObservedTwo = dsNotes.Tables[0].Rows[i]["s_value"].ToString();
                }
                if (dsNotes.Tables[0].Rows[i]["SZ_PATIENT_SIGN_PATH"].ToString() != "")
                {
                    oPmNotesDao.SZ_PATIENT_SIGN_PATH = dsNotes.Tables[0].Rows[i]["SZ_PATIENT_SIGN_PATH"].ToString();
                }
                if (dsNotes.Tables[0].Rows[i]["SZ_DOCTOR_SIGN_PATH"].ToString() != "")
                {
                    oPmNotesDao.SZ_DOCTOR_SIGN_PATH = dsNotes.Tables[0].Rows[i]["SZ_DOCTOR_SIGN_PATH"].ToString();
                }
                if (dsNotes.Tables[0].Rows[i]["bt_pat_sign_success"].ToString() != "")
                {
                    oPmNotesDao.bt_pat_sign_success = dsNotes.Tables[0].Rows[i]["bt_pat_sign_success"].ToString();
                }
                if (dsNotes.Tables[0].Rows[i]["bt_doc_sign_success"].ToString() != "")
                {
                    oPmNotesDao.bt_doc_sign_success = dsNotes.Tables[0].Rows[i]["bt_doc_sign_success"].ToString();
                }
            }
            return oPmNotesDao;
        }

        public PMNotes_ProcCode_DAO GetAllProcCode(DataSet ds)
        {
            ArrayList arAll = new ArrayList();
            PMNotes_ProcCode_DAO oPMNotes_DAO = new PMNotes_ProcCode_DAO();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                oPMNotes_DAO = new PMNotes_ProcCode_DAO();

                oPMNotes_DAO.code = Convert.ToString(ds.Tables[0].Rows[i]["code"]);
                oPMNotes_DAO.SZ_TYPE_CODE_ID = Convert.ToString(ds.Tables[0].Rows[i]["SZ_TYPE_CODE_ID"]);

                arAll.Add(oPMNotes_DAO);
                oPMNotes_DAO.ar = arAll;
            }

            return oPMNotes_DAO;
        }

        public PMNotes_ProcCode_DAO GetSelectedProcCode(DataSet ds)
        {
            ArrayList arSelected = new ArrayList();
            PMNotes_ProcCode_DAO oPMNotes_DAO = new PMNotes_ProcCode_DAO();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                oPMNotes_DAO = new PMNotes_ProcCode_DAO();

                oPMNotes_DAO.SZ_PROC_CODE = Convert.ToString(ds.Tables[0].Rows[i]["SZ_PROC_CODE"]);

                arSelected.Add(oPMNotes_DAO);
                oPMNotes_DAO.arpc = arSelected;
            }

            return oPMNotes_DAO;
        }
    }
}
