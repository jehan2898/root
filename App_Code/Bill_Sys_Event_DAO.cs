using System;
using System.Collections.Generic;

using System.Web;

/// <summary>
/// Summary description for Bill_Sys_Event_DAO
/// </summary>
public class Bill_Sys_Event_DAO
{
    private string _SZ_CASE_ID;
    private string _DT_EVENT_DATE;
    private string _DT_EVENT_TIME;
    private string _SZ_EVENT_NOTES;
    private string _SZ_DOCTOR_ID;
    private string _SZ_TYPE_CODE_ID;
    private string _SZ_COMPANY_ID;
    private string _DT_EVENT_TIME_TYPE;
    private string _DT_EVENT_END_TIME;
    private string _DT_EVENT_END_TIME_TYPE;
    private string _I_STATUS;
    private string _SZ_VISIT_TYPE;
    private string _SZ_USER_ID;
    private string _SZ_NOTES;
    private bool _IS_DENIED = false;
    private string _SZ_BILLER_ID;
    private string _SZ_DOCTOR_NAME;
    private string _SZ_GROUP_CODE;
    private string _I_EVENT_ID;

    public string I_EVENT_ID
    {
        get
        {
            return _I_EVENT_ID;
        }
        set
        {
            _I_EVENT_ID = value;
        }
    }

    public string SZ_GROUP_CODE
    {
        get
        {
            return _SZ_GROUP_CODE;
        }
        set
        {
            _SZ_GROUP_CODE = value;
        }
    }

    public string SZ_DOCTOR_NAME
    {
        get
        {
            return _SZ_DOCTOR_NAME;
        }
        set
        {
            _SZ_DOCTOR_NAME = value;
        }
    }

    public string SZ_BILLER_ID
    {
        get
        {
            return _SZ_BILLER_ID;
        }
        set
        {
            _SZ_BILLER_ID = value;
        }
    }

    public bool IS_DENIED
    {
        get
        {
            return _IS_DENIED;
        }
        set
        {
            _IS_DENIED = value;
        }
    }

    public string SZ_USER_ID
    {
        get
        {
            return _SZ_USER_ID;
        }
        set
        {
            _SZ_USER_ID = value;
        }
    }

    public string SZ_VISIT_TYPE
    {
        get
        {
            return _SZ_VISIT_TYPE;
        }
        set
        {
            _SZ_VISIT_TYPE = value;
        }
    }

    public string I_STATUS
    {
        get
        {
            return _I_STATUS;
        }
        set
        {
            _I_STATUS = value;
        }
    }

    public string DT_EVENT_END_TIME_TYPE
    {
        get
        {
            return _DT_EVENT_END_TIME_TYPE;
        }
        set
        {
            _DT_EVENT_END_TIME_TYPE = value;
        }
    }

    public string DT_EVENT_END_TIME
    {
        get{
            return _DT_EVENT_END_TIME;
        }
        set{
            _DT_EVENT_END_TIME=value;
        }
    }

    public string DT_EVENT_TIME_TYPE
    {
        get
        {
            return _DT_EVENT_TIME_TYPE;
        }
        set
        {
            _DT_EVENT_TIME_TYPE = value;
        }
    }
    public string SZ_COMPANY_ID
    {
        get
        {
            return _SZ_COMPANY_ID;
        }
        set
        {
            _SZ_COMPANY_ID = value;
        }
    }
    public string SZ_TYPE_CODE_ID
    {
        get
        {
            return _SZ_TYPE_CODE_ID;
        }
        set
        {
            _SZ_TYPE_CODE_ID = value;
        }
    }
    public string SZ_DOCTOR_ID
    {
        get
        {
            return _SZ_DOCTOR_ID;
        }
        set
        {
            _SZ_DOCTOR_ID = value;
        }
    }
    public string SZ_EVENT_NOTES
    {
        get
        {
            return _SZ_EVENT_NOTES;
        }
        set
        {
            _SZ_EVENT_NOTES = value;
        }
    }
    public string DT_EVENT_TIME
    {
        get
        {
            return _DT_EVENT_TIME;
        }
        set
        {
            _DT_EVENT_TIME = value;
        }
    }
    public string DT_EVENT_DATE
    {
        get
        {
            return _DT_EVENT_DATE;
        }
        set
        {
            _DT_EVENT_DATE = value;
        }
    }
    public string SZ_CASE_ID
    {
        get
        {
            return _SZ_CASE_ID;
        }
        set
        {
            _SZ_CASE_ID = value;
        }
    }


}