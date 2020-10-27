using System;
using System.Collections.Generic;
using System.Web;

public class PReportDAO
{
    public PReportDAO()
	{
	}

    private string SZ_CONTROLKEY;
    public string SZCONTROLKEY
    {

        get { return SZ_CONTROLKEY; }
        set { SZ_CONTROLKEY = value; }
    }

    private string SZ_CONTROLVALUE;
    public string SZCONTROLVALUE
    {
        get { return SZ_CONTROLVALUE; }
        set { SZ_CONTROLVALUE = value; }
    }

    private string S_COMPANY;
    public string SCOMPANY
    {
        get
        {
            return S_COMPANY;
        }

        set
        {
            S_COMPANY = value;
        }
    }

    private string S_OFFICE;
    public string SOFFICE
    {
        get
        {
            return S_OFFICE;
        }

        set
        {

            S_OFFICE = value;
        }
    }


    private string S_SPECIALITY;
    public string SSPECIALITY
    {
        get
        {
            return S_SPECIALITY;
        }

        set
        {
            S_SPECIALITY = value;
        }
    
    }

    private string DT_TYPE;
    public string DTTYPE
    {
        get
        {
            return DT_TYPE;
        }

        set
        {
            DT_TYPE = value;
        }
    }

    private string DT_FROM;
    public string DTFROM
    {
        get
        {
            return DT_FROM;
        }
        set
        {
            DT_FROM = value;
        }
    
    }

    private string DT_TO;
    public string DTTO
    {
        get
        {
            return DT_TO;
        }
        set
        {
            DT_TO = value;
        }
    }


}