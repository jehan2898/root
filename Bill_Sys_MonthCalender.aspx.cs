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


public partial class Bill_Sys_MonthCalender : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
        if (Page.IsPostBack == false)
        {
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            GetMonthCalender(DateTime.Now.Month, DateTime.Now.Year);
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_MonthCalender.aspx");
        }
        #endregion
    }

    public string getMonth(int i)
    {
        switch (i)
        {
            case 1: return "January"; break;
            case 2: return "Feb"; break;
            case 3: return "April"; break;
            case 4: return "March"; break;
            case 5: return "May"; break;
            case 6: return "June"; break;
            case 7: return "July"; break;
            case 8: return "August"; break;
            case 9: return "September"; break;
            case 10: return "Octomber"; break;
            case 11: return "November"; break;
            case 12: return "December"; break;

        }
        return "";
    }

    public string getDay(int i)
    {
        switch (i)
        {
            case 1: return "Sunday"; break;
            case 2: return "Monday"; break;
            case 3: return "Tuesday"; break;
            case 4: return "Wednesday"; break;
            case 5: return "Thrusday"; break;
            case 6: return "Friday"; break;
            case 7: return "Saturday"; break;
            

        }
        return "";
    }

    public int getDayNumber(string p_szDay)
    {
        switch (p_szDay)
        {
            case "Sunday": return 1; break;
            case "Monday": return 2; break;
            case "Tuesday": return 3; break;
            case "Wednesday": return 4; break;
            case "Thursday": return 5; break;
            case "Friday": return 6; break;
            case "Saturday": return 7; break;


        }
        return 0;
    }

    public string GetMonthCalender(int p_intMonth,int p_intYear)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Session["CaseID"] = Session["SZ_CASE_ID"];
        String szHtmlString = "<table width='98%' bgcolor='gray' border=1><tr>";
        Bill_Sys_BillingCompanyDetails_BO _obj = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {
            int iDaysInMonths = 0;
            
            for (int k = 1; k <= 7; k++)
            {
                szHtmlString = szHtmlString + "<td width='14%' height='60px' bgcolor='blue' align='center' style='color:White'><b>" + getDay(k) + "</b></td>";
            }
            szHtmlString = szHtmlString + "</tr><tr>";

            DateTime objDate = new DateTime(p_intYear, p_intMonth, 1);
            iDaysInMonths = System.DateTime.DaysInMonth(p_intYear, p_intMonth);
            int iStartNumber = getDayNumber(objDate.DayOfWeek.ToString());
            int j=1;
            while (j < iStartNumber)
            {
                szHtmlString = szHtmlString + "<td width='14%' height='60px'>&nbsp</td>";
                j++;
            }
            for (int i= 1 ; i<=iDaysInMonths ; i++)
            {
                szHtmlString = szHtmlString + "<td width='14%' height='60px' style='color:WhiteSmoke;'><table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> " + i + "</td></tr><tr><td width='100%'>" + _obj.GetPerDayEvent(Session["CaseID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ddlMonth.SelectedValue + "/" + i + "/" + ddlYear.SelectedValue) + "</td></tr></table></td>";
                
                if ( (i+iStartNumber) % 7 ==1)
                    szHtmlString = szHtmlString + "</tr><tr>";
                
            }
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
       
        tdMonthCalender.InnerHtml = szHtmlString;
        return null;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }



   
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetMonthCalender(Convert.ToInt16(ddlMonth.SelectedValue), Convert.ToInt16(ddlYear.SelectedValue));
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetMonthCalender(Convert.ToInt16(ddlMonth.SelectedValue), Convert.ToInt16(ddlYear.SelectedValue));
    }
 
    protected void imgbtnPrevDate_Click(object sender, EventArgs e)
    {
        
        DateTime objDateTime = new DateTime(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue), 1);
        objDateTime = objDateTime.AddMonths(-1);

        ddlMonth.SelectedValue = objDateTime.Month.ToString();
        ddlYear.SelectedValue = objDateTime.Year.ToString();
        GetMonthCalender(objDateTime.Month, objDateTime.Year);
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        DateTime objDateTime = new DateTime(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue), 1);
        objDateTime = objDateTime.AddMonths(1);
    
        ddlMonth.SelectedValue = objDateTime.Month.ToString();
        ddlYear.SelectedValue = objDateTime.Year.ToString();
        GetMonthCalender(objDateTime.Month, objDateTime.Year);
    }
    protected void btnSetSession_Click(object sender, EventArgs e)
    {
        Session["CURRENT_DATE"] = hdnSessionValue.Value;
    }
}
