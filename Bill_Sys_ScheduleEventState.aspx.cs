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
using System.Xml;
using Componend;

public partial class Bill_Sys_ScheduleEventState : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           
            TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;             
        
            txtCaseID.Text = Session["SZ_CASE_ID"].ToString();
            hdnCaseID.Text = Session["SZ_CASE_ID"].ToString();
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!IsPostBack)
            {
                           
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                DisplayCalender(DateTime.Now.Date);
               
            }
            //if (ddlTime.SelectedItem.Text == "PM")
            //{
            //    txtTime.Text = (12 + Convert.ToInt32(ddlHours.SelectedItem.Text)) + "." + ddlMinutes.SelectedItem.Text;
            //}
            //else
            //{
            //    txtTime.Text =ddlHours.SelectedItem.Text + "." + ddlMinutes.SelectedItem.Text;
            //}
            lblMsg.Text = "";
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
       
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_ScheduleEventState.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayCalender(Convert.ToDateTime(Convert.ToInt16(ddlMonth.SelectedValue) + "/" + "1" + "/" + Convert.ToInt16(ddlYear.SelectedValue)));
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayCalender(Convert.ToDateTime(Convert.ToInt16(ddlMonth.SelectedValue) + "/" + "1" + "/" + Convert.ToInt16(ddlYear.SelectedValue)));
    }

    protected void imgbtnPrevDate_Click(object sender, EventArgs e)
    {

        DateTime objDateTime = new DateTime(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue), 1);
        objDateTime = objDateTime.AddMonths(-1);

        ddlMonth.SelectedValue = objDateTime.Month.ToString();
        ddlYear.SelectedValue = objDateTime.Year.ToString();
        DisplayCalender(Convert.ToDateTime( Convert.ToInt16(ddlMonth.SelectedValue) + "/" + "1" + "/" + Convert.ToInt16(ddlYear.SelectedValue)));

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        DateTime objDateTime = new DateTime(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue), 1);
        objDateTime = objDateTime.AddMonths(1);

        ddlMonth.SelectedValue = objDateTime.Month.ToString();
        ddlYear.SelectedValue = objDateTime.Year.ToString();
        DisplayCalender(Convert.ToDateTime( Convert.ToInt16(ddlMonth.SelectedValue) + "/" + "1" + "/"  + Convert.ToInt16(ddlYear.SelectedValue)));

    }


    private void DisplayCalender(DateTime _date)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string topLabel = "<table id='Calendar1' cellspacing='0' cellpadding='2' rules='all'" +
               "title='Calendar' border='1' style='width: 100%; height: 400px; font-size: 9pt;" +
               "font-family: Verdana; color: Black; border-width: 1px; border-style: Solid; border-color: Silver;" +
               "background-color: White; border-collapse: collapse;'>";

            string dayHeader = "<tr>" +
                         " <th align='center' abbr='Sunday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                          "  font-size: 8pt; font-weight: bold;'>" +
                          "  Sun</th>" +
                        "<th align='center' abbr='Monday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                         "   font-size: 8pt; font-weight: bold;'>" +
                          "  Mon</th>" +
                        "<th align='center' abbr='Tuesday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                         "   font-size: 8pt; font-weight: bold; width: 123px;'>" +
                          "  Tue</th>" +
                        "<th align='center' abbr='Wednesday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                         "   font-size: 8pt; font-weight: bold;'>" +
                          "  Wed</th>" +
                        "<th align='center' abbr='Thursday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                         "   font-size: 8pt; font-weight: bold;'>" +
                          "  Thu</th>" +
                        "<th align='center' abbr='Friday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                         "   font-size: 8pt; font-weight: bold;'>" +
                          "  Fri</th>" +
                        "<th align='center' abbr='Saturday' scope='col' style='border-width: 1px;height: 60px; border-style: solid;background-color:#CCCCCC;" +
                         "   font-size: 8pt; font-weight: bold;'>" +
                           " Sat</th>" +
                      
                    "</tr>";

            int setNumber = ComputeLastDayOfMonth(_date);
            int totalDays = DateTime.DaysInMonth(_date.Year, _date.Month);
            string innertabel = "";
            int dateStatus = 1;
            for (int i = 0; i <= 5; i++)
            {
                innertabel = innertabel + "<tr>";
                for (int j = 1; j <= 7; j++)
                {
                    if (setNumber == j && dateStatus <= totalDays)
                    {//href='javascript:
                        
                        string currentDate = _date.Month.ToString() + "/" + dateStatus.ToString() + "/" + _date.Year.ToString();
                        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                        ArrayList obj = new ArrayList();
                        obj.Add(txtCaseID.Text);
                        obj.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        obj.Add(currentDate);
                        decimal _percentage=_bill_Sys_Calender.GET_EVENT_PERCENTAGE(obj);
                        
                        
                        string _color=ParseXML(Server.MapPath("Config/EventColor.xml"),_percentage);

                        innertabel = innertabel + "<td align='left' valign='top' style='color: #999999; width: 8%; height: 85px; background-color:" + _color + "; '>" +
                            "<a href='#' onclick=\" var _date       = new Date(); _date='" + currentDate + "'; setDiv(_date);\" style='color: Black' title='" + getMonth(_date.Month) + " " + dateStatus.ToString() + "'>" +
                             "" + dateStatus.ToString() + "</a></td>";
                        dateStatus = dateStatus + 1;
                        setNumber = setNumber + 1;
                    }
                    else
                    {
                        innertabel = innertabel + "<td align='left' valign='top' style='color: #999999; width: 8%; height: 85px;'>" +
                         "</td>";
                    }


                }
                setNumber = 1;
                innertabel = innertabel + "</tr>";
            }

            Cal.InnerHtml = topLabel + dayHeader + innertabel + "</table>";
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

    private string ParseXML(string p_szPath,decimal _name )
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
          
            string objColor="";
            XmlDocument doc1 = new XmlDocument();
            doc1.Load(p_szPath);
            XmlNodeList xmlNodeList;
            xmlNodeList = doc1.GetElementsByTagName("color");

            foreach (XmlNode xm in xmlNodeList)
            {

                ((Label)this.FindControl("lbl" + xm.Attributes["percent"].Value.ToString())).Text = xm.Attributes["text"].Value.ToString() + " % : <table style='background-color:" + xm.Attributes["color"].Value + ";'><tr><td style='width: 5px;height:5px'></td></tr></table>";

                if (Convert.ToDecimal(xm.Attributes["percent"].Value) >= _name && _name != 0)
                {
                    objColor = xm.Attributes["color"].Value;
                    break;
                }
                else if (Convert.ToDecimal(xm.Attributes["percent"].Value) == 0)
                {
                    objColor = xm.Attributes["color"].Value;

                }
            }

           
            //if (Convert.ToDecimal(xmlNodeList[0].Attributes["percent"].Value) >= _name && _name !=0 )// && Convert.ToDecimal(xmlNodeList[1].Attributes["percent"].Value) <= _name)
            //    {
            //        objColor = xmlNodeList[0].Attributes["color"].Value;
                   
            //    }
            //    else if (Convert.ToDecimal(xmlNodeList[1].Attributes["percent"].Value) >= _name && _name != 0)//&& Convert.ToDecimal(xmlNodeList[2].Attributes["percent"].Value) <= _name)
            //    {
            //        objColor = xmlNodeList[1].Attributes["color"].Value;

            //    }
            //    else if (Convert.ToDecimal(xmlNodeList[2].Attributes["percent"].Value) >= _name && _name != 0)
            //    {
            //        objColor = xmlNodeList[2].Attributes["color"].Value;

            //    }
            //    else if (Convert.ToDecimal(xmlNodeList[3].Attributes["percent"].Value) >= _name )
            //    {
            //        objColor = xmlNodeList[3].Attributes["color"].Value;

            //    }
            return objColor;
        }
        catch (Exception ex)
        {

            return null;
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

    public int ComputeLastDayOfMonth(DateTime TheDate)
    {

        return getDayNumber(TheDate.AddDays(-(TheDate.Day- 1)).DayOfWeek.ToString());

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
   
}
