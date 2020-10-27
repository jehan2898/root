using System;
using System.Collections.Generic;
using System.Text;
using gb.mbs.da.dbconstant;
using gb.mbs.da.model.intake;
using gb.mbs.da.common;
using System.Data.SqlClient;
using System.Data;
using gb.mbs.da.service.util;
using gbmodel=gb.mbs.da.model;

namespace gb.mbs.da.intake
{
    public class SrvIntake
    {
        public void insertIntake(gbmodel.user.User oUser, Intake oIntake)
        {
            
        }

        #region Enumeration Data methods

        public IsPregnant ResolvePregnantData(string pregnantModeString)
        {
            if (pregnantModeString != null)
            {
                switch (pregnantModeString.ToLower())
                {
                    case "1": 
                        return IsPregnant.Yes;
                    case "2": 
                        return IsPregnant.Not;
                    case "3": 
                        return IsPregnant.MayBe;                   
                    default:
                        throw new ApplicationException(string.Format("Unsupported Credit Card Type {0}!", pregnantModeString));
                }
            }
            throw new ApplicationException(string.Format("Unsupported Credit Card Type {0}!", pregnantModeString));
        }

        public PatientCaseTypes ResolvePatientCaseTypes(string patientType)
        {
            if (patientType != null)
            {
                switch (patientType.ToLower())
                {
                    case "1":
                        return PatientCaseTypes.WORKER_COMPENSATION;
                    case "2":
                        return PatientCaseTypes.NOFAULT;
                    case "3":
                        return PatientCaseTypes.OTHER;
                    
                    default:
                        throw new ApplicationException(string.Format("Unsupported Credit Card Type {0}!", patientType));
                }
            }
            throw new ApplicationException(string.Format("Unsupported Credit Card Type {0}!", patientType));
        }

        public MaritalStatusType ResolveMaritialStatus(string MaritialStatusString)
        {
            if (MaritialStatusString != null)
            {
                switch (MaritialStatusString.ToLower())
                {
                    case "1":
                        return MaritalStatusType.Married;
                    case "2":
                        return MaritalStatusType.UnMarried;
                    case "3":
                        return MaritalStatusType.Divocee;
                    case "4":
                        return MaritalStatusType.Widow;                    
                    default:
                        throw new ApplicationException(string.Format("Unsupported Credit Card Type {0}!", MaritialStatusString));
                }
            }
            throw new ApplicationException(string.Format("Unsupported Credit Card Type {0}!", MaritialStatusString));
        }

        public PatientTypes ResolvePatientTypes(string patienttypeString)
        {
            if (patienttypeString != null)
            {
                switch (patienttypeString.ToLower())
                {
                    case "1":
                        return PatientTypes.DRIVER;
                    case "2":
                        return PatientTypes.PASSENGER;
                    case "3":
                        return PatientTypes.PEDESTRIAN;
                    case "4":
                        return PatientTypes.OTHER;
                    default:
                        throw new ApplicationException(string.Format("Unsupported Credit Card Type {0}!", patienttypeString));
                }
            }
            throw new ApplicationException(string.Format("Unsupported Credit Card Type {0}!", patienttypeString));
        }

        #endregion Enumeration Data methods
    }
}
