using System;
using System.Collections;

public class Bill_Sys_Bulk_Visits
{
    private ArrayList _arrlst_Valid;

    private ArrayList _arrlst_InValid;

    public ArrayList InValidList
    {
        get
        {
            return this._arrlst_InValid;
        }
        set
        {
            this._arrlst_InValid = value;
        }
    }

    public ArrayList ValidList
    {
        get
        {
            return this._arrlst_Valid;
        }
        set
        {
            this._arrlst_Valid = value;
        }
    }

    public Bill_Sys_Bulk_Visits()
    {
    }
}