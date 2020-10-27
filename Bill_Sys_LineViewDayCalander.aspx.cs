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

public partial class Bill_Sys_LineViewDayCalander : PageBase
{
    Bill_Sys_Calender bill_Sys_Calender=new Bill_Sys_Calender();
    ArrayList arrayReturn;
    public delegate void SimpleDelegate();
     public event SimpleDelegate StartEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtCaseID.Text = Session["CASE_ID"].ToString();
        if (!IsPostBack)
        {
            if (Session["CURRENT_TIME"] != null)
            {
                if (Session["CURRENT_DATE"] != null)
                {
                    Session["CALENDER_STATE"] = "Day";
                    GenerateDayDetail(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()), Convert.ToInt32(Session["CURRENT_TIME"]), "FOR_CASE");
                    divNavigation.Visible = false;
                    //Session["CURRENT_TIME"] = null;
                }
                else
                {
                    Session["CALENDER_STATE"] = "Day";
                    Session["CURRENT_DATE"] = DateTime.Now.Date.ToLongDateString();
                    GenerateDayDetail(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()), Convert.ToInt32(Session["CURRENT_TIME"]), "FOR_CASE");
                    divNavigation.Visible = false;
                    //Session["CURRENT_TIME"] = null;
                }
            }
            else
            {
                if (Session["CURRENT_DATE"] != null)
                {
                    Session["CALENDER_STATE"] = "Day";
                    GenerateDayDetail(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()), 0, "FOR_CASE");
                }
                else
                {
                    Session["CALENDER_STATE"] = "Day";
                    Session["CURRENT_DATE"] = DateTime.Now.Date.ToLongDateString();
                    GenerateDayDetail(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()), 0, "FOR_CASE");
                }
            }
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_LineViewDayCalander.aspx");
        }
        #endregion
    }


    # region "Get Month Name"
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
    #endregion

    #region GenerateDayDetail
    private void GenerateDayDetail(DateTime _date,int time,string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
   
            lblDate.Text = _date.ToLongDateString();
          
            tr25.Visible = false;
            tr26.Visible = false;
            tr27.Visible = false;
            tr28.Visible = false;
            tr29.Visible = false;
            tr30.Visible = false;
            tr31.Visible = false;
            int itime = 0;
            if (time > 0)
            {
                itime = time;
            }
            for (int i = itime; i <= 23; i++)
            {
                arrayReturn = new ArrayList();
                arrayReturn = bill_Sys_Calender.getDayDetails(Convert.ToDateTime(lblDate.Text), i, txtCaseID.Text, txtCompanyID.Text, flag);
                string str = "<table >";
                string firstRowStr = "<tr>";
                string secondRowStr = "<tr>";
                string thirdRowStr = "<tr>";
                string fourthRowStr = "<tr>";
                if (i == 0)
                {
                    Label0.Text = "00.00";
                    LnkBtn0.Visible = false;
                    LnkBtn0.CommandArgument = Label0.Text;
                    tr1.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                       
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn0.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal0.Text = str;
                }
                else if (i == 1)
                {
                    Label1.Text = "01.00";
                    LnkBtn1.Visible = false;
                    LnkBtn1.CommandArgument = Label1.Text;
                    tr2.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn1.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal1.Text = str;
                }
                else if (i == 2)
                {
                    Label2.Text = "02.00";
                    LnkBtn2.Visible = false;
                    LnkBtn2.CommandArgument = Label2.Text;
                    tr3.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn2.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal2.Text = str;
                }
                else if (i == 3)
                {
                    Label3.Text = "03.00";
                    LnkBtn3.Visible = false;
                    LnkBtn3.CommandArgument = Label3.Text;
                    tr4.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                       
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn3.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal3.Text = str;
                }
                else if (i == 4)
                {
                    Label4.Text = "04.00";
                    LnkBtn4.Visible = false;
                    LnkBtn4.CommandArgument = Label4.Text;
                    tr5.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn4.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal4.Text = str;
                }
                else if (i == 5)
                {
                    Label5.Text = "05.00";
                    LnkBtn5.Visible = false;
                    LnkBtn5.CommandArgument = Label5.Text;
                    tr6.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn5.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal5.Text = str;
                }
                else if (i == 6)
                {
                    Label6.Text = "06.00";
                    LnkBtn6.Visible = false;
                    LnkBtn6.CommandArgument = Label6.Text;
                    tr7.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn6.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal6.Text = str;
                }
                else if (i == 7)
                {
                    Label7.Text = "07.00";
                    LnkBtn7.Visible = false;
                    LnkBtn7.CommandArgument = Label7.Text;
                    tr8.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn7.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal7.Text = str;
                }
                else if (i == 8)
                {
                    Label8.Text = "08.00";
                    LnkBtn8.Visible = false;
                    LnkBtn8.CommandArgument = Label8.Text;
                    tr9.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn8.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal8.Text = str;
                }
                else if (i == 9)
                {
                    Label9.Text = "09.00";
                    LnkBtn9.Visible = false;
                    LnkBtn9.CommandArgument = Label9.Text;
                    tr10.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                       
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn9.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal9.Text = str;
                }
                else if (i == 10)
                {
                    Label10.Text = "10.00";
                    LnkBtn10.Visible = false;
                    LnkBtn10.CommandArgument = Label10.Text;
                    tr11.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                       
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn10.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal10.Text = str;
                }
                else if (i == 11)
                {
                    Label11.Text = "11.00";
                    LnkBtn11.Visible = false;
                    LnkBtn11.CommandArgument = Label11.Text;
                    tr12.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn11.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal11.Text = str;
                }
                else if (i == 12)
                {
                    Label12.Text = "12.00";
                    LnkBtn12.Visible = false;
                    LnkBtn12.CommandArgument = Label12.Text;
                    tr13.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn12.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal12.Text = str;
                }
                else if (i == 13)
                {
                    Label13.Text = "13.00";
                    LnkBtn13.Visible = false;
                    LnkBtn13.CommandArgument = Label13.Text;
                    tr14.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn13.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal13.Text = str;
                }
                else if (i == 14)
                {
                    Label14.Text = "14.00";
                    LnkBtn14.Visible = false;
                    LnkBtn14.CommandArgument = Label14.Text;
                    tr15.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn14.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal14.Text = str;
                }
                else if (i == 15)
                {
                    Label15.Text = "15.00";
                    LnkBtn15.Visible = false;
                    LnkBtn15.CommandArgument = Label15.Text;
                    tr16.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn15.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal15.Text = str;
                }
                else if (i == 16)
                {
                    Label16.Text = "16.00";
                    LnkBtn16.Visible = false;
                    LnkBtn16.CommandArgument = Label16.Text;
                    tr17.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                       
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn16.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal16.Text = str;
                }
                else if (i == 17)
                {
                    Label17.Text = "17.00";
                    LnkBtn17.Visible = false;
                    LnkBtn17.CommandArgument = Label17.Text;
                    tr18.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn17.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal17.Text = str;
                }
                else if (i == 18)
                {
                    Label18.Text = "18.00";
                    LnkBtn18.Visible = false;
                    LnkBtn18.CommandArgument = Label18.Text;
                    tr19.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn18.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal18.Text = str;
                }
                else if (i == 19)
                {
                    Label19.Text = "19.00";
                    LnkBtn19.Visible = false;
                    LnkBtn19.CommandArgument = Label19.Text;
                    tr20.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn19.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal19.Text = str;
                }
                else if (i == 20)
                {
                    Label20.Text = "20.00";
                    LnkBtn20.Visible = false;
                    LnkBtn20.CommandArgument = Label20.Text;
                    tr21.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                       
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn20.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal20.Text = str;
                }
                else if (i == 21)
                {
                    Label21.Text = "21.00";
                    LnkBtn21.Visible = false;
                    LnkBtn21.CommandArgument = Label21.Text;
                    tr22.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn21.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal21.Text = str;
                }
                else if (i == 22)
                {
                    Label22.Text = "22.00";
                    LnkBtn22.Visible = false;
                    LnkBtn22.CommandArgument = Label22.Text;
                    tr23.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn22.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal22.Text = str;
                }
                else if (i == 23)
                {
                    Label23.Text = "23.00";
                    LnkBtn23.Visible = false;
                    LnkBtn23.CommandArgument = Label23.Text;
                    tr24.Visible = true;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn23.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal23.Text = str;
                }

            }
        
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void GenerateDayDetailOnTime(DateTime _date,int time)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            tr25.Visible = false;
            tr26.Visible = false;
            tr27.Visible = false;
            tr28.Visible = false;
            tr29.Visible = false;
            tr30.Visible = false;
            tr31.Visible = false;
            int itime = 0;
            if (time > 0)
            {
                itime = time; 
            }
            int count=0;
            lblDate.Text = _date.ToLongDateString();
            string str = "<table>";
            string firstRowStr = "<tr>";
            string secondRowStr = "<tr>";
            string thirdRowStr = "<tr>";
            string fourthRowStr = "<tr>";
            for (int i = itime; i <= 23; i++)
            {

                //while (count < itime)
                //{
                //    firstRowStr = firstRowStr + "<td></td></tr>";
                //    secondRowStr = secondRowStr + "<td></td></tr>";
                //    thirdRowStr = thirdRowStr + "<td></td></tr>";
                //    str = str + fourthRowStr + "<td></td></tr></table>";
                //    count++;
                //}
                arrayReturn = new ArrayList();
                arrayReturn = bill_Sys_Calender.getDayDetails(Convert.ToDateTime(lblDate.Text), i,txtCaseID.Text,txtCompanyID.Text,"FOR_CASE");
                if (i == 0)
                {
                    
                    LnkBtn0.Visible = false;
                    LnkBtn0.CommandArgument = Label0.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn0.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal0.Text = str;
                }
                if (i == 1)
                {
                    LnkBtn1.CommandArgument = Label1.Text;
                    foreach (Object obj in arrayReturn)
                    {

                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";
                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn0.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal23.Text = str;
                }
                else if (i == 2)
                {
                    LnkBtn2.CommandArgument = Label2.Text;
                    LnkBtn2.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn2.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal2.Text = str;
                }
                else if (i == 3)
                {
                    LnkBtn3.CommandArgument = Label3.Text;
                    LnkBtn3.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn3.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal3.Text = str;
                }
                else if (i == 4)
                {
                    LnkBtn4.CommandArgument = Label4.Text;
                    LnkBtn4.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn4.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal4.Text = str;
                }
                else if (i == 5)
                {
                    LnkBtn5.CommandArgument = Label5.Text;
                    LnkBtn5.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn5.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal5.Text = str;
                }
                else if (i == 6)
                {
                    LnkBtn6.CommandArgument = Label6.Text;
                    LnkBtn6.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn6.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal6.Text = str;
                }
                else if (i == 7)
                {
                    LnkBtn7.CommandArgument = Label7.Text;
                    LnkBtn7.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn7.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal7.Text = str;
                }
                else if (i == 8)
                {
                    LnkBtn8.CommandArgument = Label8.Text;
                    LnkBtn8.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn8.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal8.Text = str;
                }
                else if (i == 9)
                {
                    LnkBtn9.CommandArgument = Label9.Text;
                    LnkBtn9.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn9.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal9.Text = str;
                }
                else if (i == 10)
                {
                    LnkBtn10.CommandArgument = Label10.Text;
                    LnkBtn10.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn10.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal10.Text = str;
                }
                else if (i == 11)
                {
                    LnkBtn11.CommandArgument = Label11.Text;
                    LnkBtn11.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn11.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";

                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal11.Text = str;
                }
                else if (i == 12)
                {
                    LnkBtn12.CommandArgument = Label12.Text;
                    LnkBtn12.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn12.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal12.Text = str;
                }
                else if (i == 13)
                {
                    LnkBtn13.CommandArgument = Label13.Text;
                    LnkBtn13.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn13.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal13.Text = str;
                }
                else if (i == 14)
                {
                    LnkBtn14.CommandArgument = Label14.Text;
                    LnkBtn14.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn14.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal14.Text = str;
                }
                else if (i == 15)
                {
                    LnkBtn15.CommandArgument = Label15.Text;
                    LnkBtn15.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn15.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal15.Text = str;
                }
                else if (i == 16)
                {
                    LnkBtn16.CommandArgument = Label16.Text;
                    LnkBtn16.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn16.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal16.Text = str;
                }
                else if (i == 17)
                {
                    LnkBtn17.CommandArgument = Label17.Text;
                    LnkBtn17.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn17.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal17.Text = str;
                }
                else if (i == 18)
                {
                    LnkBtn18.CommandArgument = Label18.Text;
                    LnkBtn18.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn18.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal18.Text = str;
                }
                else if (i == 19)
                {
                    LnkBtn19.CommandArgument = Label19.Text;
                    LnkBtn19.Visible = false;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn19.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal19.Text = str;
                }
                else if (i == 20)
                {
                    
                    LnkBtn20.Visible = false;
                    LnkBtn20.CommandArgument = Label20.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn20.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal20.Text = str;
                }
                else if (i == 21)
                {
                    
                    LnkBtn21.Visible = false;
                    LnkBtn21.CommandArgument = Label21.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn21.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal21.Text = str;
                }
                else if (i == 22)
                {
                
                    LnkBtn22.Visible = false;
                    LnkBtn22.CommandArgument = Label22.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn22.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal22.Text = str;
                }
                else if (i == 23)
                {
                 
                    LnkBtn23.Visible = false;
                    LnkBtn23.CommandArgument = Label23.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn23.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal23.Text = str;
                }
               
            }
           
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    #endregion

    #region GenerateMonthDetail

    private void GenerateMonthDetail(DateTime _date)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblDate.Text = _date.ToString("MMMM yyyy");
            int _month = _date.Month;
            int _year = _date.Year;
            int _Nmonth;
            int _Nyear;
            TimeSpan diffResult;
            if (_month != 12)
            {
               _Nmonth= _month + 1;
               diffResult = Convert.ToDateTime(_Nmonth.ToString() + "/01/" + _year.ToString()) - Convert.ToDateTime(_month.ToString() + "/01/" + _year.ToString());
            }
            else 
            {
                _Nmonth = 1;
                _Nyear = _year + 1;
                diffResult = Convert.ToDateTime(_Nmonth.ToString() + "/01/" + _Nyear.ToString()) - Convert.ToDateTime(_month.ToString() + "/01/" + _year.ToString());
            }
            tr25.Visible = true;
            tr26.Visible = true;
            tr27.Visible = true;
            tr28.Visible = true;
            if (diffResult.Days == 28)
            {
                tr29.Visible = false;
                tr30.Visible = false;
                tr31.Visible = false;
            }
            else if (diffResult.Days == 29)
            {
                tr29.Visible = true;
                tr30.Visible = false;
                tr31.Visible = false;
            }
            if (diffResult.Days == 30)
            {
                tr29.Visible = true;
                tr30.Visible = true;
                tr31.Visible = false;
            }
            if (diffResult.Days == 31)
            {
                tr29.Visible = true;
                tr30.Visible = true;
                tr31.Visible = true;
            }

            for (int i = 1; i <= diffResult.Days; i++)
            {
                arrayReturn = new ArrayList();
                arrayReturn = bill_Sys_Calender.getMonthDetails(Convert.ToDateTime(lblDate.Text), i, txtCaseID.Text, txtCompanyID.Text, "FOR_CASE");
                string str = "<table>";
                string firstRowStr = "<tr>";
                string secondRowStr = "<tr>";
                string thirdRowStr = "<tr>";
                string fourthRowStr = "<tr>";
                if (i == 1)
                {
                    Label0.Text = "01";
                    LnkBtn0.Visible = false;
                    LnkBtn0.CommandArgument = Label0.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn0.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal0.Text = str;
                }
                else if (i == 2)
                {
                    Label1.Text = "02";
                    LnkBtn1.Visible = false;
                    LnkBtn1.CommandArgument = Label1.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn1.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal1.Text = str;
                }
                else if (i == 3)
                {
                    Label2.Text = "03";
                    LnkBtn2.Visible = false;
                    LnkBtn2.CommandArgument = Label2.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn2.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal2.Text = str;
                }
                else if (i == 4)
                {
                    Label3.Text = "04";
                    LnkBtn3.Visible = false;
                    LnkBtn3.CommandArgument = Label3.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn3.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal3.Text = str;
                }
                else if (i == 5)
                {
                    Label4.Text = "05";
                    LnkBtn4.Visible = false;
                    LnkBtn4.CommandArgument = Label4.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn4.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal4.Text = str;
                }
                else if (i == 6)
                {
                    Label5.Text = "06";
                    LnkBtn5.Visible = false;
                    LnkBtn5.CommandArgument = Label5.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn5.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal5.Text = str;
                }
                else if (i == 7)
                {
                    Label6.Text = "07";
                    LnkBtn6.Visible = false;
                    LnkBtn6.CommandArgument = Label6.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3)
                        {
                            LnkBtn6.Visible = true;
                            LnkBtn6.Attributes.Add("onclick", "ShowAll()");
                        }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal6.Text = str;
                }
                else if (i == 8)
                {
                    Label7.Text = "08";
                    LnkBtn7.Visible = false;
                    LnkBtn7.CommandArgument = Label7.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn7.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal7.Text = str;
                }
                else if (i == 9)
                {
                    Label8.Text = "09";
                    LnkBtn8.Visible = false;
                    LnkBtn8.CommandArgument = Label8.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn8.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal8.Text = str;
                }
                else if (i == 10)
                {
                    Label9.Text = "10";
                    LnkBtn9.Visible = false;
                    LnkBtn9.CommandArgument = Label9.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn9.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal9.Text = str;
                }
                else if (i == 11)
                {
                    Label10.Text = "11";
                    LnkBtn10.Visible = false;
                    LnkBtn10.CommandArgument = Label10.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn10.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal10.Text = str;
                }
                else if (i == 12)
                {
                    Label11.Text = "12";
                    LnkBtn11.Visible = false;
                    LnkBtn11.CommandArgument = Label11.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn11.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";

                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal11.Text = str;
                }
                else if (i == 13)
                {
                    Label12.Text = "13";
                    LnkBtn12.Visible = false;
                    LnkBtn12.CommandArgument = Label12.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn12.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal12.Text = str;
                }
                else if (i == 14)
                {
                    Label13.Text = "14";
                    LnkBtn13.Visible = false;
                    LnkBtn13.CommandArgument = Label13.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn13.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal13.Text = str;
                }
                else if (i == 15)
                {
                    Label14.Text = "15";
                    LnkBtn14.Visible = false;
                    LnkBtn14.CommandArgument = Label14.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn14.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal14.Text = str;
                }
                else if (i == 16)
                {
                    Label15.Text = "16";
                    LnkBtn15.Visible = false;
                    LnkBtn15.CommandArgument = Label15.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn15.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal15.Text = str;
                }
                else if (i == 17)
                {
                    Label16.Text = "17";
                    LnkBtn16.Visible = false;
                    LnkBtn16.CommandArgument = Label16.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn16.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal16.Text = str;
                }
                else if (i == 18)
                {
                    Label17.Text = "18";
                    LnkBtn17.Visible = false;
                    LnkBtn17.CommandArgument = Label17.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn17.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal17.Text = str;
                }
                else if (i == 19)
                {
                    Label18.Text = "19";
                    LnkBtn18.Visible = false;
                    LnkBtn18.CommandArgument = Label18.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn18.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal18.Text = str;
                }
                else if (i == 20)
                {
                    Label19.Text = "20";
                    LnkBtn19.Visible = false;
                    
                    LnkBtn19.CommandArgument = Label19.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3)
                        {
                            LnkBtn19.Visible = true;                          
                        }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal19.Text = str;
                }
                else if (i == 21)
                {
                    Label20.Text = "21";
                    LnkBtn20.Visible = false;
                    LnkBtn20.CommandArgument = Label20.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn20.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal20.Text = str;
                }
                else if (i == 22)
                {
                    Label21.Text = "22";
                    LnkBtn21.Visible = false;
                    LnkBtn21.CommandArgument = Label21.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn21.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal21.Text = str;
                }
                else if (i == 23)
                {
                    Label22.Text = "23";
                    LnkBtn22.Visible = false;
                    LnkBtn22.CommandArgument = Label22.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn22.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal22.Text = str;
                }
                else if (i == 24)
                {
                    Label23.Text = "24";
                    LnkBtn23.Visible = false;
                    LnkBtn23.CommandArgument = Label23.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn23.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal23.Text = str;
                }
                else if (i == 25)
                {
                    Label24.Text = "25";
                    LnkBtn24.Visible = false;
                    LnkBtn24.CommandArgument = Label24.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn23.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal24.Text = str;
                }
                else if (i == 26)
                {
                    Label25.Text = "26";
                    LnkBtn25.Visible = false;
                    LnkBtn25.CommandArgument = Label25.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn23.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal25.Text = str;
                }
                else if (i == 27)
                {
                    Label26.Text = "27";
                    LnkBtn26.Visible = false;
                    LnkBtn26.CommandArgument = Label26.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn23.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal26.Text = str;
                }
                else if (i == 28)
                {
                    Label27.Text = "28";
                    LnkBtn27.Visible = false;
                    LnkBtn27.CommandArgument = Label27.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn23.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal27.Text = str;
                }
                else if (i == 29)
                {
                    Label28.Text = "29";
                    LnkBtn28.Visible = false;
                    LnkBtn28.CommandArgument = Label28.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn23.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal28.Text = str;
                }
                else if (i == 30)
                {
                    Label29.Text = "30";
                    LnkBtn29.Visible = false;
                    LnkBtn29.CommandArgument = Label29.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn23.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal29.Text = str;
                }
                else if (i == 31)
                {
                    Label30.Text = "31";
                    LnkBtn30.Visible = false;
                    LnkBtn30.CommandArgument = Label30.Text;
                    foreach (Object obj in arrayReturn)
                    {
                        firstRowStr = firstRowStr + "<td class='css-text'>" + ((Day_DETAIL)obj).EVENT_PATIENT + "</td>";
                        secondRowStr = secondRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_DOCTOR + "</td>";
                        thirdRowStr = thirdRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_TIME + "</td>";
                        fourthRowStr = fourthRowStr + "<td>" + ((Day_DETAIL)obj).EVENT_NOTES + "</td>";

                        if (((Day_DETAIL)obj).EVENT_COUNT > 3) { LnkBtn23.Visible = true; }
                    }
                    firstRowStr = firstRowStr + "</tr>";
                    secondRowStr = secondRowStr + "</tr>";
                    thirdRowStr = thirdRowStr + "</tr>";
                    fourthRowStr = fourthRowStr + "</tr>";
                    str = str + firstRowStr + secondRowStr + thirdRowStr + fourthRowStr + "</table>";
                    literal30.Text = str;
                }
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion

    #region NextPrev

    protected void btnDayView_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["CALENDER_STATE"] = "Day";
            lnkNext.Text = "Next Date";
            lnkPrevious.Text = "Previous Date";
            GenerateDayDetail(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()), 0, "FOR_CASE");
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnMonthView_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
          
            Session["CALENDER_STATE"] = "Month";
            lnkNext.Text = "Next Month";
            lnkPrevious.Text = "Previous Month";
            GenerateMonthDetail(Convert.ToDateTime(lblDate.Text.ToString()));
           
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if ( Session["CALENDER_STATE"].ToString() == "Day")
            {
                Session["CURRENT_DATE"] = Convert.ToDateTime(lblDate.Text.ToString()).AddDays(-1).ToLongDateString();
                GenerateDayDetail(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()), 0, "FOR_CASE");
            }
            else if (Session["CALENDER_STATE"].ToString() == "Month")
            {
                Session["CURRENT_DATE"] = Convert.ToDateTime(lblDate.Text.ToString()).AddMonths(-1).ToLongDateString();
                GenerateMonthDetail(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()));
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if ( Session["CALENDER_STATE"].ToString() == "Day")
            {
                Session["CURRENT_DATE"] = Convert.ToDateTime(lblDate.Text.ToString()).AddDays(1).ToLongDateString();
                GenerateDayDetail(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()), 0, "FOR_CASE");
            }
            else if (Session["CALENDER_STATE"].ToString() == "Month")
            {
                Session["CURRENT_DATE"] = Convert.ToDateTime(lblDate.Text.ToString()).AddMonths(1).ToLongDateString();
                GenerateMonthDetail(Convert.ToDateTime(Session["CURRENT_DATE"].ToString()));
            }
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion

    #region ShoWALL

     public void ShowAll(object sender,EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
        
        LinkButton lnkTest = (LinkButton)sender;

        if (Session["CALENDER_STATE"] == "Day")
        {
            string str = lnkTest.CommandArgument.ToString();
            
            
            Session["CURRENTTIME"] = Convert.ToInt32(str.Substring(0, 2));
            Session["CURRENT_DATE"] = Convert.ToDateTime(lblDate.Text.ToString());
        }
        else
        {
            Session["CURRENTTIME"] = null;
            Session["CURRENT_DATE"] = Convert.ToDateTime(lblDate.Text.ToString()).Month + "/" + Convert.ToInt32(lnkTest.CommandArgument.ToString()) + "/" + Convert.ToDateTime(lblDate.Text.ToString()).Year;
        }
            
        
        Session["CASE_ID"] = txtCaseID.Text;          
        Response.Redirect("Bill_Sys_DisplayEvents.aspx", false);
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    #endregion

    //protected void LnkBtn0_Click(object sender, CommandEventArgs e)
    //{
    //   // Session["CURRENT_DATE"] = Convert.ToDateTime(lblDate.Text.ToString()).Month.ToString() + "/01/" + Convert.ToDateTime(lblDate.Text.ToString()).Da.ToString()
    //    //LinkButton lnktest = (LinkButton)e.ToString();
    //    string str1 = e.CommandArgument.ToString();
    //    Session["CASE_ID"] = txtCaseID.Text;       
    
    //    Session["CURRENTTIME"] = Session["CURRENT_TIME"];
    //    Response.Redirect("Bill_Sys_DisplayEvents.aspx", false);
    //}
    
    //protected void LnkBtn1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Bill_Sys_DisplayEvents.aspx", false);
    //}
    //protected void LnkBtn2_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Bill_Sys_DisplayEvents.aspx", false);
    //}
    //protected void LnkBtn3_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Bill_Sys_DisplayEvents.aspx", false);
    //}
    //protected void LnkBtn4_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Bill_Sys_DisplayEvents.aspx", false);
    //}
    //protected void LnkBtn5_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Bill_Sys_DisplayEvents.aspx", false);
    //}
    //protected void LnkBtn30_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Bill_Sys_DisplayEvents.aspx", false);
    //}

    //protected void LnkBtn29_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Bill_Sys_DisplayEvents.aspx", false);
    //}
    //protected void LnkBtn28_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Bill_Sys_DisplayEvents.aspx", false);
    //}
    //protected void LnkBtn27_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Bill_Sys_DisplayEvents.aspx", false);
    //}
}
