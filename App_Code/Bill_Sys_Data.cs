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
/// Summary description for Bill_Sys_Data
/// </summary>
public class Bill_Sys_Data
{
    private string sz_errorMessage;
    private string sz_billURL;
    private string sz_companyid;
    private string sz_billnumber;
    private string sz_successmessage;

    public string successmessage
    {
        get
        {
            return sz_successmessage;
        }
        set
        {
            sz_successmessage = value;
        }
    }

    public string billnumber
    {
        get
        {
            return sz_billnumber;
        }
        set
        {
            sz_billnumber = value;
        }
    }

    public string companyid
    {
        get
        {
            return sz_companyid;
        }
        set
        {
            sz_companyid = value;
        }
    }

    public string billurl
    {
        get
        {
            return sz_billURL;
        }
        set
        {
            sz_billURL = value;
        }
    }

    public string errormessage
    {
        get
        {
            return sz_errorMessage;
        }
      set
        {
            sz_errorMessage = value;
        }
    }
        
}
  
