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
/// Summary description for ReportDAO
/// </summary>
public class ReportDAO
{
    public ReportDAO()
    {
      
    }
    private string szCompanyId;
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

    private string szDays;
    public string Days
    {
        get
        {
            return szDays;
        }
        set
        {
            szDays = value;
        }
    }


    private string szBillNo;
    public string BillNo
    {
        get
        {
            return szBillNo;
        }
        set
        {
            szBillNo = value;
        }
    }

    private string szPatientName;
    public string PatientName
    {
        get
        {
            return szPatientName;
        }
        set
        {
            szPatientName = value;
        }
    }

    private string szBillStatus;
    public string BillStatus
    {
        get
        {
            return szBillStatus;
        }
        set
        {
            szBillStatus = value;
        }
    }

    private string szBillStatusOr;
    public string BillStatusOr
    {
        get
        {
            return szBillStatusOr;
        }
        set
        {
            szBillStatusOr = value;
        }
    }

    private string szCaseNo;
    public string CaseNo
    {
        get
        {
            return szCaseNo;
        }
        set
        {
            szCaseNo = value;
        }
    }

    private DateTime DtFromdate;
    public DateTime Fromdate
    {
        get
        {
            return DtFromdate;
        }
        set
        {
            DtFromdate = value;
        }
    }

    private DateTime DtTodate;
    public DateTime Todate
    {
        get
        {
            return DtTodate;
        }
        set
        {
            DtTodate = value;
        }
    }

}
