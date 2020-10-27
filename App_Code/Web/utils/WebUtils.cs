using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// Summary description for WebUtils
/// </summary>
/// 
[Serializable()]
public  class WebUtils
{
    //String strConn;
    //SqlConnection sqlCon;
    //SqlCommand sqlCmd;
    //SqlDataAdapter sqlda;
    //DataSet ds;
	public  WebUtils()
	{
       // strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public static string CleanText(string value)
    {
        if (String.IsNullOrEmpty(value))
        {
            return value;
        }
        string lineSeparator = ((char)0x2028).ToString();
        string paragraphSeparator = ((char)0x2029).ToString();

        return value.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);
    }

    public static string CleanString(string value)
    {
        string returnstr;
        returnstr = value.Replace(System.Environment.NewLine, value);

        return returnstr;
    }
    public static string EncodeUrlString(string url)
    {
        string newUrl;
        newUrl = HttpUtility.UrlEncode(url);

        return newUrl;
    }
    public static string DecodeUrlString(string url)
    {
        string newUrl;
        while ((newUrl = Uri.UnescapeDataString(url)) != url)
            url = newUrl;
        return newUrl;
    }
}