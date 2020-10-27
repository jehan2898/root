using System;

public class Calendar_DAO
{
    private string szInitDisplayMonth;

    private string szControlIDPrefix;

    private string szInitDisplayYear;

    public string ControlIDPrefix
    {
        get
        {
            return this.szControlIDPrefix;
        }
        set
        {
            this.szControlIDPrefix = value;
        }
    }

    public string InitialDisplayMonth
    {
        get
        {
            return this.szInitDisplayMonth;
        }
        set
        {
            this.szInitDisplayMonth = value;
        }
    }

    public string InitialDisplayYear
    {
        get
        {
            return this.szInitDisplayYear;
        }
        set
        {
            this.szInitDisplayYear = value;
        }
    }

    public Calendar_DAO()
    {
    }
}