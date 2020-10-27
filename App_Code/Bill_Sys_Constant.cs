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
/// Summary description for Bill_Sys_Constant
/// </summary>
public class Bill_Sys_Constant
{
    public Bill_Sys_Constant()
    {
       
    }

    /// <summary>
    /// Costant Used to decide position of next diagnosis code page.
    /// </summary>

    public const string constGenerateNextDiagPage = "CI_0000003"; // NO
    public const string constAFTER_FIRST_PAGE = "CK_0000001";//"AFTER_FIRST_PAGE";
    public const string constAFTER_BILL_INFORMATION = "CK_0000002";//"AFTER_BILL_INFORMATION";
    public const string constAT_THE_END = "CK_0000003";//"AT_THE_END";
    public const string constAFTER_AOB = "CK_0000004";//"AFTER_AOB";
    public const string constAFTER_EOB = "CK_0000005";//"AFTER_EOB";
    public const string constBEFORE_AOB = "CK_0000006";//"BEFORE_AOB";

}
