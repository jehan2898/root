using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for LHR_Visit_ImportDAO
/// </summary>
public class LHR_Visit_ImportDAO
{
	public LHR_Visit_ImportDAO()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string  sz_First_Name;
    public string SZ_First_Name
    {
        get { return sz_First_Name; }
        set { sz_First_Name = value; }
    }

    private string  sz_Last_Name;
    public string SZ_Last_Name
    {
        get { return sz_Last_Name; }
        set { sz_Last_Name = value; }
    }

    private string  sz_Patient_ID;
    public string SZ_Patient_ID
    {
        get { return sz_Patient_ID; }
        set { sz_Patient_ID = value; }
    }

    private string  sz_Date_Of_Birth;
    public string SZ_Date_Of_Birth
    {
        get { return sz_Date_Of_Birth; }
        set { sz_Date_Of_Birth = value; }
    }

    private string  sz_Gender;
    public string SZ_Gender
    {
        get { return sz_Gender; }
        set { sz_Gender = value; }
    }

    private string  sz_Address;
    public string SZ_Address
    {
        get { return sz_Address; }
        set { sz_Address = value; }
    }

    private string  sz_Address2;
    public string SZ_Address2
    {
        get { return sz_Address2; }
        set { sz_Address2 = value; }
    }

    private string  sz_City;
    public string SZ_City
    {
        get { return sz_City; }
        set { sz_City = value; }
    }

    private string  sz_State;
    public string SZ_State
    {
        get { return sz_State; }
        set { sz_State = value; }
    }

    private string  sz_Zip;
    public string SZ_Zip
    {
        get { return sz_Zip; }
        set { sz_Zip = value; }
    }

    private string  sz_Case_Type;
    public string SZ_Case_Type
    {
        get { return sz_Case_Type; }
        set { sz_Case_Type = value; }
    }

    private string  sz_SSNO ;
    public string SZ_SSNO
    {
        get { return sz_SSNO; }
        set { sz_SSNO = value; }
    }

    private string  sz_Date_Of_Appointment ;
    public string SZ_Date_Of_Appointment
    {
        get { return sz_Date_Of_Appointment; }
        set { sz_Date_Of_Appointment = value; }
    }

    private string  sz_Visit_Time;
    public string SZ_Visit_Time
    {
        get { return sz_Visit_Time; }
        set { sz_Visit_Time = value; }
    }

    private string  sz_Procedure_Code ;
    public string SZ_Procedure_Code
    {
        get { return sz_Procedure_Code; }
        set { sz_Procedure_Code = value; }
    }

    private string  sz_Procedure_Desc;
    public string SZ_Procedure_Desc
    {
        get { return sz_Procedure_Desc; }
        set { sz_Procedure_Desc = value; }
    }

    private string  sz_Date_Of_Accident ;
    public string SZ_Date_Of_Accident
    {
        get { return sz_Date_Of_Accident; }
        set { sz_Date_Of_Accident = value; }
    }

    private string  sz_Book_Facility;
    public string SZ_Book_Facility
    {
        get { return sz_Book_Facility; }
        set { sz_Book_Facility = value; }
    }

    private string  sz_Modality;
    public string SZ_Modality
    {
        get { return sz_Modality; }
        set { sz_Modality = value; }
    }

    private string sz_Case_ID;
    public string SZ_Case_ID
    {
        get { return sz_Case_ID; }
        set { sz_Case_ID = value; }
    }

    private string _company_ID;

    public string Company_ID
    {
        get { return _company_ID; }
        set { _company_ID = value; }
    }

    private string _user_ID;

    public string User_ID
    {
        get { return _user_ID; }
        set { _user_ID = value; }
    }
}