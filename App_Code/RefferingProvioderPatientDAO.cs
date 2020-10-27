using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for RefferingProvioderPatientDAO
/// </summary>
public class RefferingProvioderPatientDAO
{
	public RefferingProvioderPatientDAO()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _CaseNo;
    public string CaseNo
    {
        get { return _CaseNo; }
        set { _CaseNo = value; }
    }

    private string _CaseType;
    public string CaseType
    {
        get { return _CaseType; }
        set { _CaseType = value; }
    }

    private string _CaseStatus;
    public string CaseStatus
    {
        get { return _CaseStatus; }
        set { _CaseStatus = value; }
    }

    private string _PatientName;
    public string PatientName
    {
        get { return _PatientName; }
        set { _PatientName = value; }
    }

    private string _Insurance;
    public string Insurance
    {
        get { return _Insurance; }
        set { _Insurance = value; }
    }

    private string _claimNumber;
    public string claimNumber
    {
        get { return _claimNumber; }
        set { _claimNumber = value; }
    }

    private string _ssnNO;
    public string ssnNO
    {
        get { return _ssnNO; }
        set { _ssnNO = value; }
    }

    private string _accidentDate;
    public string accidentDate
    {
        get { return _accidentDate; }
        set { _accidentDate = value; }
    }

    private string _birthDate;
    public string birthDate
    {
        get { return _birthDate; }
        set { _birthDate = value; }
    }

    private string _location;
    public string location
    {
        get { return _location; }
        set { _location = value; }
    }

    private string _companyID;
    public string companyID
    {
        get { return _companyID; }
        set { _companyID = value; }
    }

    private string _userID;
    public string userID
    {
        get { return _userID; }
        set { _userID = value; }
    }

    private string _patientID;
    public string patientID
    {
        get { return _patientID; }
        set { _patientID = value; }
    }

    private string _reffOfficeID;
    public string reffOfficeID
    {
        get { return _reffOfficeID; }
        set { _reffOfficeID = value; }
    }
}