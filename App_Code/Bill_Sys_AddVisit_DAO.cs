using System;

public class Bill_Sys_AddVisit_DAO
{
    private string _case_no;

    private string _doctor_id;

    private string _visit_type_id;

    private string _event_time;

    private string _event_date;

    private string _event_note;

    private string _company_id;

    private string _type_code_id;

    private string _event_time_type;

    private string _event_end_time_type;

    private string _event_end_time;

    private string _i_status;

    private string _procedure_code;

    public string CaseNo
    {
        get
        {
            return this._case_no;
        }
        set
        {
            this._case_no = value;
        }
    }

    public string CompanyId
    {
        get
        {
            return this._company_id;
        }
        set
        {
            this._company_id = value;
        }
    }

    public string DoctorID
    {
        get
        {
            return this._doctor_id;
        }
        set
        {
            this._doctor_id = value;
        }
    }

    public string EventDate
    {
        get
        {
            return this._event_date;
        }
        set
        {
            this._event_date = value;
        }
    }

    public string EventEndTime
    {
        get
        {
            return this._event_end_time;
        }
        set
        {
            this._event_end_time = value;
        }
    }

    public string EventEndTimeType
    {
        get
        {
            return this._event_end_time_type;
        }
        set
        {
            this._event_end_time_type = value;
        }
    }

    public string EventNote
    {
        get
        {
            return this._event_note;
        }
        set
        {
            this._event_note = value;
        }
    }

    public string EventTime
    {
        get
        {
            return this._event_time;
        }
        set
        {
            this._event_time = value;
        }
    }

    public string EventTimeType
    {
        get
        {
            return this._event_time_type;
        }
        set
        {
            this._event_time_type = value;
        }
    }

    public string IStatus
    {
        get
        {
            return this._i_status;
        }
        set
        {
            this._i_status = value;
        }
    }

    public string ProcedureCode
    {
        get
        {
            return this._procedure_code;
        }
        set
        {
            this._procedure_code = value;
        }
    }

    public string TypeCodeId
    {
        get
        {
            return this._type_code_id;
        }
        set
        {
            this._type_code_id = value;
        }
    }

    public string VisitTypeId
    {
        get
        {
            return this._visit_type_id;
        }
        set
        {
            this._visit_type_id = value;
        }
    }

    public Bill_Sys_AddVisit_DAO()
    {
    }
}