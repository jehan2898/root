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
/// Summary description for Bill_Sys_KeyValue
/// </summary>
public class Bill_Sys_KeyValue
{

    private string szKey;

    public string Key
    {
        get
        {
            return szKey;
        }
        set 
        {
            szKey = value;
        }
    }

    private string szValue;

    public string Value
    {
        get {
            return szValue;
        }
        set {
            szValue = value;
        }
    }

}
