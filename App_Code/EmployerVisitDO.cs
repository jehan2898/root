using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for EmployerVisitDO
/// </summary>
public class EmployerVisitDO
{
    private string _VisitId;
    public string VisitId
    {
        get { return _VisitId; }
        set { _VisitId = value; }
    }
    private string _CaseID;
    public string CaseID
    {
        get { return _CaseID; }
        set { _CaseID = value; }

    }
    private string _CompanyID;
    public string CompanyID
    {
        get { return _CompanyID; }
        set { _CompanyID = value; }
    }
    private string _EmploerID;
    public string EmploerID
    {
        get { return _EmploerID; }
        set { _EmploerID = value; }
    }
    private string _UserID;
    public string UserID
    {
        get { return _UserID; }
        set { _UserID = value; }
    }
    private string _VisitDate;
    public string VisitDate
    {
        get { return _VisitDate; }
        set { _VisitDate = value; }
    }
    private string _InvoiceNo;
    public string InvoiceNo
    {
        get { return _InvoiceNo; }
        set { _InvoiceNo = value; }
    }

    private string _From_Visit_Date;
    public string From_Visit_Date
    {
        get { return _From_Visit_Date; }
        set { _From_Visit_Date = value; }
    }

    private string _To_Visit_Date;
    public string To_Visit_Date
    {
        get { return _To_Visit_Date; }
        set { _To_Visit_Date = value; }
    }

    private string _CaseNO;
    public string CaseNO
    {
        get { return _CaseNO; }
        set { _CaseNO = value; }

    }

    private string _UserName;
    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }

    }
    private string _CompanyName;
    public string CompanyName
    {
        get { return _CompanyName; }
        set { _CompanyName = value; }
    }

    private string _FileName;
    public string FileName
    {
        get { return _FileName; }
        set { _FileName = value; }
    }


    private string _ImageId;
    public string ImageId
    {
        get { return _ImageId; }
        set { _ImageId = value; }
    }
   
   public  List<EmployerVisitProcedure> EmployerVisitProcedure;

}
public class EmployerVisitProcedure
{
    private string _ProcedureCode;
    public string ProcedureCode
    {
        get { return _ProcedureCode; }
        set { _ProcedureCode = value; }
    }

    private string _ProcedureGroupId;
    public string ProcedureGroupId
    {
        get { return _ProcedureGroupId; }
        set { _ProcedureGroupId = value; }
    }

    private string _VisitId;
    public string VisitId
    {
        get { return _VisitId; }
        set { _VisitId = value; }
    }
}