using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using log4net;
using System.Configuration;

public partial class AJAX_Pages_Bill_Sys_TodayVisit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.con.SourceGrid = this.grdTodayVisit;
            this.txtSearchBox.SourceGrid = this.grdTodayVisit;
            this.grdTodayVisit.Page = this.Page;
            this.grdTodayVisit.PageNumberList = this.con;
            if (!IsPostBack)
            {
                ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
                btnUpdateStatus.Attributes.Add("onclick", "return UpdateVisit()");
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlProvider.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                txtFromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                Bind_Grid();
                BindTimeControl();
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void Bind_Grid()
    {
        try
        {
            txtFDate.Text = txtFromDate.Text;
            txtTDate.Text = txtToDate.Text;
            txtStatus.Text = rblTodaysVisit.SelectedValue.ToString();
            txtSpeciality.Text = extddlSpeciality.Text;
            txtProvider.Text = extddlProvider.Text;
            txtDoctor.Text = extddlDoctor.Text;
            grdTodayVisit.XGridBindSearch();
            
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind_Grid();
    }

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        int iFlag = 0;
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
        Bill_Sys_Event_BO _Bill_Sys_Event_BO = new Bill_Sys_Event_BO();
        ArrayList objAdd;
        try
        {
            Boolean _valid = true;

            if (_valid == true)
            {

                for (int i = 0; i < this.grdTodayVisit.Rows.Count; i++)
                {
                   
                    CheckBox chkSelected = (CheckBox)grdTodayVisit.Rows[i].FindControl("ChkSelect");
                    string iEventID = "";
                    string szcaseID = "";
                    string szDoctorID = "";
                    string szhave_login = "";
                    string szgroupcode = "";
                    if (chkSelected.Checked == true)
                    {
                        iEventID = grdTodayVisit.DataKeys[i]["I_EVENT_ID"].ToString(); 
                        szcaseID = grdTodayVisit.DataKeys[i]["SZ_CASE_ID"].ToString(); 
                        szDoctorID = grdTodayVisit.DataKeys[i]["SZ_DOCTOR_ID"].ToString(); 
                        szhave_login = grdTodayVisit.DataKeys[i]["IS_HAVE_LOGIN"].ToString(); 
                        szgroupcode = grdTodayVisit.DataKeys[i]["GROUP_CODE"].ToString(); 
                        if (szhave_login == "1")
                        {
                            if (ddlStatus.SelectedValue == "1")
                            {
                                int endMin = Convert.ToInt32(ddlReSchMinutes.SelectedValue) + Convert.ToInt32("30");
                                int endHr = Convert.ToInt32(ddlReSchHours.SelectedValue);
                                string endTime = ddlReSchTime.SelectedValue;
                                if (endMin >= 60)
                                {
                                    endMin = endMin - 60;
                                    endHr = endHr + 1;
                                    if (endHr > 12)
                                    {
                                        endHr = endHr - 12;
                                        if (ddlReSchHours.SelectedValue != "12")
                                        {
                                            if (endTime == "AM")
                                            {
                                                endTime = "PM";
                                            }
                                            else if (endTime == "PM")
                                            {
                                                endTime = "AM";
                                            }
                                        }
                                    }
                                    else if (endHr == 12)
                                    {
                                        if (ddlReSchHours.SelectedValue != "12")
                                        {
                                            if (endTime == "AM")
                                            {
                                                endTime = "PM";
                                            }
                                            else if (endTime == "PM")
                                            {
                                                endTime = "AM";
                                            }
                                        }
                                    }
                                }

                                _Bill_Sys_Event_BO.UpdateRescheduledoctorvisits(iEventID, "", txtReScheduleDate.Text, szgroupcode, ddlReSchHours.SelectedValue.ToString() + "." + ddlReSchMinutes.SelectedValue.ToString(), ddlReSchTime.SelectedValue, endHr.ToString().PadLeft(2, '0').ToString() + "." + endMin.ToString().PadLeft(2, '0').ToString(), endTime);

                            }
                            else if (ddlStatus.SelectedValue == "3")
                            {
                                Bill_Sys_Calender _bill_Sys_Calender1 = new Bill_Sys_Calender();
                                ArrayList objAdd1 = new ArrayList();
                                objAdd1.Add(iEventID);
                                objAdd1.Add(false);
                                objAdd1.Add(ddlStatus.SelectedValue);
                                _bill_Sys_Calender1.UPDATE_Event_Status(objAdd1);

                            }

                            else
                            {
                                iFlag = 1;

                            }
                        }
                        else
                        {
                            if (ddlStatus.SelectedValue == "1")
                            {
                                objAdd = new ArrayList();
                                objAdd.Add(szcaseID);
                                objAdd.Add(txtReScheduleDate.Text);
                                objAdd.Add(ddlReSchHours.SelectedValue.ToString() + "." + ddlReSchMinutes.SelectedValue.ToString());
                                objAdd.Add("");
                                objAdd.Add(szDoctorID);
                                objAdd.Add("TY000000000000000003");
                                objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                objAdd.Add(ddlReSchTime.SelectedValue);
                                int endMin = Convert.ToInt32(ddlReSchMinutes.SelectedValue) + Convert.ToInt32("30");
                                int endHr = Convert.ToInt32(ddlReSchHours.SelectedValue);
                                string endTime = ddlReSchTime.SelectedValue;
                                if (endMin >= 60)
                                {
                                    endMin = endMin - 60;
                                    endHr = endHr + 1;
                                    if (endHr > 12)
                                    {
                                        endHr = endHr - 12;
                                        if (ddlReSchHours.SelectedValue != "12")
                                        {
                                            if (endTime == "AM")
                                            {
                                                endTime = "PM";
                                            }
                                            else if (endTime == "PM")
                                            {
                                                endTime = "AM";
                                            }
                                        }
                                    }
                                    else if (endHr == 12)
                                    {
                                        if (ddlReSchHours.SelectedValue != "12")
                                        {
                                            if (endTime == "AM")
                                            {
                                                endTime = "PM";
                                            }
                                            else if (endTime == "PM")
                                            {
                                                endTime = "AM";
                                            }
                                        }
                                    }
                                }
                                objAdd.Add(endHr.ToString().PadLeft(2, '0').ToString() + "." + endMin.ToString().PadLeft(2, '0').ToString());
                                objAdd.Add(endTime.ToString());
                                _bill_Sys_Calender.SaveEvent(objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                            }


                            _bill_Sys_Calender = new Bill_Sys_Calender();
                            objAdd = new ArrayList();
                            objAdd.Add(iEventID);
                            objAdd.Add(false);
                            objAdd.Add(ddlStatus.SelectedValue);
                            _bill_Sys_Calender.UPDATE_Event_Status(objAdd);




                        }

                    }

                }



                lblMessage.Text = "";
                usrMessage.PutMessage("Update Sucessfully ...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage); 
                usrMessage.Show();
                Bind_Grid();
                //Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
                //DataSet dsVisits = new DataSet();
                //dsVisits = objGetVisits.GetCaledarVisits(txtGetDay.Value, txtCompanyID.Text, extddlSpeciality.Text, txtProvider.Text, extddlDoctor.Text);
                //grdTodayVisit.XGridBindSearch();
                //ViewState["griddata"] = dsVisits;
                clear();




            }
        }
        catch (Exception ex)
        {
        }
    }
    public void clear()
    {

        ddlStatus.SelectedIndex = 0;
        ddlReSchHours.SelectedIndex = 0;
        ddlReSchMinutes.SelectedIndex = 0;
        ddlReSchTime.SelectedIndex = 0;
        //extddlSpeciality.Text = "NA";
        //extddlDoctor.Text = "NA";
        //txtPatientName.Text = "";
    }
    protected void btnDeletVisit_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string szListOfProcedureCode = "";
            for (int i = 0; i < grdTodayVisit.Rows.Count; i++)
            {
                CheckBox chkSelected = (CheckBox)grdTodayVisit.Rows[i].FindControl("ChkSelect");
                if (chkSelected.Checked == true)
                {
                    string iEventID = grdTodayVisit.DataKeys[i]["I_EVENT_ID"].ToString();
                    Bill_Sys_DeleteBO _deleteOpeation = new Bill_Sys_DeleteBO();
                    if (!_deleteOpeation.deleteRecord("SP_TXN_CALENDAR_EVENT", "@I_EVENT_ID", iEventID))
                    {
                        if (szListOfProcedureCode == "")
                        {
                            // Org  -- szListOfProcedureCode = gridTabInfo.Items[i].Cells[1].Text + "-" + gridTabInfo.Items[i].Cells[3].Text;
                            szListOfProcedureCode = grdTodayVisit.DataKeys[i]["DT_EVENT_DATE"].ToString();
                        }
                        else
                        {
                            // Org  -- szListOfProcedureCode = szListOfProcedureCode + " , " + gridTabInfo.Items[i].Cells[1].Text + "-" + gridTabInfo.Items[i].Cells[3].Text;
                            szListOfProcedureCode = szListOfProcedureCode + " , " + grdTodayVisit.DataKeys[i]["DT_EVENT_DATE"].ToString();
                        }
                    }
                }
            }
            Bind_Grid();
            //Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
            //DataSet dsVisits = new DataSet();
            //dsVisits = objGetVisits.GetCaledarVisits(txtGetDay.Value, txtCompanyID.Text, extddlSpeciality.Text, txtProvider.Text, extddlDoctor.Text);
            //grdTodayVisit.XGridBindSearch();
            //ViewState["griddata"] = dsVisits;
            clear();
            if (szListOfProcedureCode != "")
            {
                usrMessage.PutMessage("Error  visits not deleted for " + szListOfProcedureCode + " event dates");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
            else
            {
                usrMessage.PutMessage("Visit Delete Sucessfully ...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            //LoadCalendarAccordingToYearAndMonth();
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void BindTimeControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i = 0; i <= 12; i++)
            {
                if (i > 9)
                {
                    ddlReSchHours.Items.Add(i.ToString());

                }
                else
                {
                    ddlReSchHours.Items.Add("0" + i.ToString());

                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i > 9)
                {
                    ddlReSchMinutes.Items.Add(i.ToString());

                }
                else
                {
                    ddlReSchMinutes.Items.Add("0" + i.ToString());

                }
            }
            ddlReSchTime.Items.Add("AM");
            ddlReSchTime.Items.Add("PM");



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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat("window.location.href ='", ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString(), this.grdTodayVisit.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()), "';"), true);
    }
}