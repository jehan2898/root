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
/// Summary description for Bill_Sys_SelectedDiagnosisObject
/// </summary>
public class Bill_Sys_SelectedDiagnosisObject
{
   
         private string _dIAGNOSIS_CODEID = "";
    public string DIAGNOSIS_CODEID
    {
        get
        {
            return _dIAGNOSIS_CODEID;
        }
        set
        {
            _dIAGNOSIS_CODEID = value;
        }
    }

    private string _dIAGNOSIS_CODE = "";
    public string DIAGNOSIS_CODE
    {
        get
        {
            return _dIAGNOSIS_CODE;
        }
        set
        {
            _dIAGNOSIS_CODE = value;
        }
    }

    private string _pROCEDURE_CODEID = "";
    public string PROCEDURE_CODEID
    {
        get
        {
            return _pROCEDURE_CODEID;
        }
        set
        {
            _pROCEDURE_CODEID = value;
        }
    }

    private string _pROCEDURE_CODE = "";
    public string PROCEDURE_CODE
    {
        get
        {
            return _pROCEDURE_CODE;
        }
        set
        {
            _pROCEDURE_CODE = value;
        }
    }

   
    
}
