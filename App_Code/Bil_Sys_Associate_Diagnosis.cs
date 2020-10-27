using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Bil_Sys_Associate_Diagnosis
/// </summary>
/// 
/*Changed on 27-Dec-2014 */
[Serializable]
public class Bil_Sys_Associate_Diagnosis
{
    private string szCaseID;
    private string szProceuderGroupId;
    private string szEventProcID;
    private string szDoctorID;
    private string szProceuderGroupName;
    private string szPatientId;
    private string szDateOfService;
    private string szProcedureCode;
    private string szCompanyId;

    private string szRefferingDoctor;
    public string RefferingDoctor
    {
        get { return szRefferingDoctor; }
        set { szRefferingDoctor = value; }
    }

    public string CaseID
    {
        get
        {
            return szCaseID;
        }
        set
        {
            szCaseID = value;
        }
    }

    public string ProceuderGroupId
    {
        get
        {
            return szProceuderGroupId;
        }
        set
        {
            szProceuderGroupId = value;
        }
    }

    public string EventProcID
    {
        get
        {
            return szEventProcID;
        }
        set
        {
            szEventProcID = value;
        }
    }

    public string DoctorID
    {
        get
        {
            return szDoctorID;
        }
        set
        {
            szDoctorID = value;
        }
    }

    public string ProceuderGroupName
    {
        get
        {
            return szProceuderGroupName;
        }
        set
        {
            szProceuderGroupName = value;
        }
    }
    public string PatientId
    {
        get
        {
            return szPatientId;
        }
        set
        {
            szPatientId = value;
        }
    }
    public string DateOfService
    {
        get
        {
            return szDateOfService;
        }
        set
        {
            szDateOfService = value;
        }
    }
    public string ProcedureCode
    {
        get
        {
            return szProcedureCode;
        }
        set
        {
            szProcedureCode = value;
        }
    }
    public string CompanyId
    {
        get
        {
            return szCompanyId;
        }
        set
        {
            szCompanyId = value;
        }
    }

}
