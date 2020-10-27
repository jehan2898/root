using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class UserControl_Bill_Sys_Case : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // LinkButton1.Text = ((Bill_Sys_Case)Session["CASEINFO"]).SZ_CASE_ID;
        lnkUCCaseID.Text = ((Bill_Sys_Case)Session["CASEINFO"]).SZ_CASE_ID;
        Session["PassedCaseID"] = ((Bill_Sys_Case)Session["CASEINFO"]).SZ_CASE_ID;
        Session["SZ_CASE_ID"] = ((Bill_Sys_Case)Session["CASEINFO"]).SZ_CASE_ID;
        lnkUCCaseID.NavigateUrl = "../Bill_Sys_CaseDetails.aspx?caseId=" + ((Bill_Sys_Case)Session["CASEINFO"]).SZ_CASE_ID;
        lnkHome.NavigateUrl = "../Bill_Sys_SearchCase.aspx";
         
    }
   
}
