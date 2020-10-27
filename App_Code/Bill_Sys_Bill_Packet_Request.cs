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
/// Summary description for Bill_Sys_Bill_Packet_Request
/// </summary>
public class Bill_Sys_Bill_Packet_Request
{
    
    private string _szBillNumber = "";
    public string SZ_BILL_NUMBER
    {
        get
        {
            return _szBillNumber;
        }
        set
        {
            _szBillNumber = value;
        }
    }
    private string _szCaseId = "";
    public string SZ_CASE_ID
    {
        get
        {
            return _szCaseId;
        }
        set
        {
            _szCaseId = value;
        }
    }

    private string _szSpeciaty = "";
    public string SZ_SPECIALTY
    {
        get
        {
            return _szSpeciaty;
        }
        set
        {
            _szSpeciaty = value;
        }
    }
    
     
}
