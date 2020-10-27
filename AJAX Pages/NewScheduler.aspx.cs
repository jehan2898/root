using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;



public partial class AJAX_Pages_NewScheduler : PageBase
{
    Bill_Sys_DeleteBO billSysDeleteBO = new Bill_Sys_DeleteBO();
    bool appointmentFormOpened = false;

    protected void NewScheduler1_BeforeExecuteCallbackCommand(object sender, SchedulerCallbackCommandEventArgs e)
    {
        if (e.CommandId == SchedulerCallbackCommandId.MenuView && Request["__CALLBACKPARAM"].Contains("NewAppointment"))
            appointmentFormOpened = true;
    }
    protected void Sche_Pre_Init(object sender, EventArgs e)
    {

        ResData.SelectCommand = @"Select MST_DOCTOR.SZ_DOCTOR_NAME+' ('+MST_PROCEDURE_GROUP.SZ_PROCEDURE_GROUP+')' as SZ_DOCTOR_NAME,MST_DOCTOR.SZ_DOCTOR_ID from MST_DOCTOR
JOIN TXN_DOCTOR_SPECIALITY ON MST_DOCTOR.SZ_DOCTOR_ID = TXN_DOCTOR_SPECIALITY.SZ_DOCTOR_ID
JOIN MST_PROCEDURE_GROUP ON MST_PROCEDURE_GROUP.SZ_PROCEDURE_GROUP_ID = TXN_DOCTOR_SPECIALITY.SZ_PROCEDURE_GROUP_ID
 Where MST_DOCTOR.SZ_COMPANY_ID = '" + ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "'";
    }

    private void BindSpecialty()
    {
        DataSet ds = new SchedularDAO().GetSpecialtyAll(((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        ddlSpeciality.DataSource = ds.Tables[0];
        ddlSpeciality.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ASPxScheduler1.Start = DateTime.Now;
            BindSpecialty();

        }
        ASPxScheduler1.OptionsCustomization.AllowInplaceEditor = DevExpress.XtraScheduler.UsedAppointmentType.None;
    }

    #region AppointmentChanging
    protected void ASPxScheduler1_AppointmentChanging(object sender, DevExpress.XtraScheduler.PersistentObjectCancelEventArgs e)
    {

        Appointment apt = (Appointment)e.Object;
        string[] clientData = apt.Location.Split(new char[] { '-' });
        if (clientData[2].Trim() == "null")
        {

            apt.Subject = apt.Description;
        }
        apt.CustomFields["SZ_PATIENT_ID"] = clientData[2];
        e.Cancel = false;
    }
    #endregion


    #region AppointmentsDeleted
    protected void ASPxScheduler1_AppointmentsDeleted(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
    {
        Appointment apt = (Appointment)e.Objects[0];

        //Delete Schedule
        DeleteSchedule objSchedule = new DeleteSchedule();
        objSchedule.Index = 0;
        objSchedule.AppointmentID = Convert.ToInt32(apt.Id);
        switch (apt.Type)
        {
            case AppointmentType.ChangedOccurrence:
                break;
            case AppointmentType.DeletedOccurrence:
            case AppointmentType.Pattern:
                objSchedule.Index = apt.RecurrenceIndex;
                billSysDeleteBO.DeleteEventSchedular(objSchedule);
                break;
            case AppointmentType.Normal:
                billSysDeleteBO.DeleteEventSchedular(objSchedule);
                break;
            case AppointmentType.Occurrence:

                break;
            default:
                break;
        }

    }
    #endregion

    #region AppointmentsInserted
    protected void ASPxScheduler1_AppointmentsInserted(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
    {
        //Task.Factory.StartNew(() => 
        //{
        Appointment apt = (Appointment)e.Objects[0];
        string[] clientData = apt.Location.Split(new char[] { '-' });
        if (clientData[2].Trim() != "null")
        {
            if (apt.Type == AppointmentType.Pattern)
            {
                OccurrenceCalculator oc = OccurrenceCalculator.CreateInstance(apt.RecurrenceInfo);
                TimeInterval ttc = new TimeInterval(apt.RecurrenceInfo.Start, apt.RecurrenceInfo.End.Add(apt.Duration));
                AppointmentBaseCollection appts = oc.CalcOccurrences(ttc, apt);

                foreach (Appointment item in appts)
                {
                    int index = item.RecurrenceIndex;
                    #region Data insert to TXN_CALENDAR
                    try
                    {
                        ArrayList objAdd;
                        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                        objAdd = new ArrayList();
                        objAdd.Add(_bill_Sys_Calender.GetCaseIDByPatient(clientData[2]));
                        objAdd.Add(item.Start);
                        objAdd.Add(item.Start.Hour + "." + item.Start.Minute);
                        objAdd.Add(apt.Description);
                        objAdd.Add(clientData[1]);
                        objAdd.Add("TY000000000000000003");
                        objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAdd.Add(apt.Start.ToString("tt", CultureInfo.InvariantCulture));
                        int endMin = Convert.ToInt32(item.End.Minute);
                        int endHr = Convert.ToInt32(item.End.Hour);
                        string endTime = item.End.Hour + "." + item.End.Minute;
                        if (endMin >= 60)
                        {
                            endMin = endMin - 60;
                            endHr = endHr + 1;
                            if (endHr > 12)
                            {
                                endHr = endHr - 12;
                                if (apt.End.Hour != 12)
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
                                if (apt.End.Hour != 12)
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
                        objAdd.Add(item.End.ToString("tt", CultureInfo.InvariantCulture));
                        objAdd.Add(apt.StatusId);
                        objAdd.Add(clientData[0]);
                        objAdd.Add(apt.Id);
                        objAdd.Add(index);
                        _bill_Sys_Calender.SaveEventFromSchedular(objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                        index++;
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                    #endregion

                }
                return;
            }
            else if (apt.Type == AppointmentType.Normal)
            {
                #region Data insert to TXN_CALENDAR
                try
                {
                    ArrayList objAdd;
                    Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                    objAdd = new ArrayList();
                    objAdd.Add(_bill_Sys_Calender.GetCaseIDByPatient(clientData[2]));
                    objAdd.Add(apt.Start);
                    objAdd.Add(apt.Start.Hour + "." + apt.Start.Minute);
                    objAdd.Add(apt.Description);
                    objAdd.Add(clientData[1]);
                    objAdd.Add("TY000000000000000003");
                    objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    objAdd.Add(apt.Start.ToString("tt", CultureInfo.InvariantCulture));
                    int endMin = Convert.ToInt32(apt.End.Minute);
                    int endHr = Convert.ToInt32(apt.End.Hour);
                    string endTime = apt.End.Hour + "." + apt.End.Minute;
                    if (endMin >= 60)
                    {
                        endMin = endMin - 60;
                        endHr = endHr + 1;
                        if (endHr > 12)
                        {
                            endHr = endHr - 12;
                            if (apt.End.Hour != 12)
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
                            if (apt.End.Hour != 12)
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
                    objAdd.Add(apt.End.ToString("tt", CultureInfo.InvariantCulture));
                    objAdd.Add(0);
                    objAdd.Add(clientData[0]);
                    objAdd.Add(apt.Id);
                    objAdd.Add(0);
                    _bill_Sys_Calender.SaveEventFromSchedular(objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
                #endregion
                return;
            }
            else if (apt.Type == AppointmentType.DeletedOccurrence)
            {
                //Deleted code in TXN_CALENDER_EVENTS
                DeleteSchedule objSchedule = new DeleteSchedule();
                objSchedule.Index = apt.RecurrenceIndex;
                objSchedule.AppointmentID = Convert.ToInt32(apt.RecurrencePattern.Id);
                billSysDeleteBO.DeleteEventSchedular(objSchedule);
                return;
            }
            else if (apt.Type == AppointmentType.ChangedOccurrence)
            {

                return;
            }
        }

    }
    #endregion
    protected void ASPxScheduler1_AppointmentRowDeleted(object sender, DevExpress.Web.ASPxScheduler.ASPxSchedulerDataDeletedEventArgs e)
    {

    }
   
    protected void ASPxScheduler_AppointmentViewInfoCustomizing1(object sender, DevExpress.Web.ASPxScheduler.AppointmentViewInfoCustomizingEventArgs e)
    {
        if (e.ViewInfo.Appointment.CustomFields["SZ_PATIENT_ID"].ToString().Trim() == "null")
            e.ViewInfo.AppointmentStyle.BackColor = Color.Red;
    }
    protected void ASPxScheduler1_AppointmentInserting(object sender, DevExpress.XtraScheduler.PersistentObjectCancelEventArgs e)
    {
        Appointment apt = (Appointment)e.Object;
        string[] clientData = apt.Location.Split(new char[] { '-' });
        apt.CustomFields["SZ_PATIENT_ID"] = clientData[2];
        if (clientData[2].Trim() == "null"){

            apt.Subject = apt.Description;
        }
        apt.CustomFields["SZ_DOCTOR_ID"] = clientData[1];
        apt.CustomFields["VISIT_TYPE_ID"] = clientData[0];
        apt.CustomFields["SZ_COMPANY_ID"] = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        e.Cancel = false;


    }
    protected void ASPxScheduler1_AppointmentFormShowing(object sender, DevExpress.Web.ASPxScheduler.AppointmentFormEventArgs e)
    {
        e.Container = new MyAppointmentFormTemplateContainer((ASPxScheduler)sender);

    }
    protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, SchedulerCallbackCommandEventArgs e)
    {
        if (e.CommandId == SchedulerCallbackCommandId.ClientSideInsertAppointment)
            /// e.Command = new MyAppointmentSaveCallbackCommand((ASPxScheduler)sender);
            if (e.CommandId == SchedulerCallbackCommandId.ClientSideUpdateAppointment)
            {


            }


        //if (e.CommandId == "MNUVIEW")
        //    e.Command = new CustomMenuViewCallbackCommand(ASPxScheduler1);
        //else if (e.CommandId == "USRAPTMENU")
        //    e.Command = new CustomMenuAppointmentCallbackCommand(ASPxScheduler1);
    }
    protected void ASPxScheduler1_PopupMenuShowing(object sender, DevExpress.Web.ASPxScheduler.PopupMenuShowingEventArgs e)
    {
        ASPxSchedulerPopupMenu menu = e.Menu;
        MenuItemCollection menuItems = menu.Items as MenuItemCollection;
        if (menu.MenuId.Equals(SchedulerMenuItemId.DefaultMenu))
        {
            ClearUnusedDefaultMenuItems(menu);
        }
        else if (menu.MenuId.Equals(SchedulerMenuItemId.AppointmentMenu))
        {
            // menu.ClientSideEvents.ItemClick = String.Format("function(s, e) {{ DefaultAppointmentMenuHandler({0}, s, e); }}", ASPxScheduler1.ClientID);
            // menu.Items.Clear();
            RemoveMenuItem(menu, "DeleteAppointment");
            RemoveMenuItem(menu, "EditSeries");
            RemoveMenuItem(menu, "RestoreOccurrence");
            RemoveMenuItem(menu, "StatusSubMenu");
            RemoveMenuItem(menu, "LabelSubMenu");
            //AddScheduleNewEventSubMenu(menu, "Visit status");
            //MenuItem deleteItem = new MenuItem("Delete", "DeleteId");
            //deleteItem.BeginGroup = true;
            ////menuItems.Add(deleteItem);

        }
    }
    protected void AddScheduleNewEventSubMenu(ASPxSchedulerPopupMenu menu, string caption)
    {
        //MenuItem newWithTemplateItem = new MenuItem(caption, "TemplateEvents");
        //newWithTemplateItem.BeginGroup = true;
        //menu.Items.Insert(0, newWithTemplateItem);
        //AddTemplatesSubMenuItems(newWithTemplateItem);
    }
    private static void AddTemplatesSubMenuItems(MenuItem parentMenuItem)
    {
        parentMenuItem.Items.Add(new MenuItem("Complete", "Complete"));
        parentMenuItem.Items.Add(new MenuItem("Finalize", "Finalize"));
        parentMenuItem.Items.Add(new MenuItem("Incomplete", "Incomplete"));
        parentMenuItem.Items.Add(new MenuItem("NoShow", "NoShow"));
        parentMenuItem.Items.Add(new MenuItem("Billed", "Billed"));
    }
    protected void ClearUnusedDefaultMenuItems(ASPxSchedulerPopupMenu menu)
    {
        //RemoveMenuItem(menu, "NewAppointment");
        RemoveMenuItem(menu, "NewAllDayEvent");
        RemoveMenuItem(menu, "NewRecurringAppointment");
        RemoveMenuItem(menu, "NewRecurringEvent");
        RemoveMenuItem(menu, "GotoToday");
        RemoveMenuItem(menu, "GotoDate");
        RemoveMenuItem(menu, "Delete");
    }
    protected void RemoveMenuItem(ASPxSchedulerPopupMenu menu, string menuItemName)
    {
        MenuItem item = menu.Items.FindByName(menuItemName);
        if (item != null)
            menu.Items.Remove(item);
    }



    #region AppointmentChanged
    protected void ASPxScheduler1_AppointmentsChanged(object sender, PersistentObjectsEventArgs e)
    {
        //Delete Schedule

        DevExpress.XtraScheduler.Internal.Implementations.AppointmentInstance AppointmentInstance = (DevExpress.XtraScheduler.Internal.Implementations.AppointmentInstance)e.Objects[0];
        DeleteSchedule objSchedule = new DeleteSchedule();

        objSchedule.Index = 0;
        if (AppointmentInstance.Type == AppointmentType.Normal)
            objSchedule.AppointmentID = Convert.ToInt32(AppointmentInstance.Id);
        else if (AppointmentInstance.Type == AppointmentType.Pattern)
            objSchedule.AppointmentID = Convert.ToInt32(AppointmentInstance.RecurrencePattern.Id);
        switch (AppointmentInstance.Type)
        {
            case AppointmentType.ChangedOccurrence:
                break;
            case AppointmentType.DeletedOccurrence:
            case AppointmentType.Pattern:
                objSchedule.Index = -1;
                billSysDeleteBO.DeleteEventSchedular(objSchedule);
                break;
            case AppointmentType.Normal:
                // billSysDeleteBO.DeleteEventSchedular(objSchedule);
                break;
            case AppointmentType.Occurrence:

                break;
            default:
                break;
        }




        if (AppointmentInstance.Type == AppointmentType.Pattern)
        {
            OccurrenceCalculator oc = OccurrenceCalculator.CreateInstance(AppointmentInstance.RecurrenceInfo);
            TimeInterval ttc = new TimeInterval(AppointmentInstance.RecurrenceInfo.Start, AppointmentInstance.RecurrenceInfo.End.Add(AppointmentInstance.Duration));
            AppointmentBaseCollection appts = oc.CalcOccurrences(ttc, AppointmentInstance);

            foreach (Appointment item in appts)
            {
                int index = item.RecurrenceIndex;
                #region Data insert to TXN_CALENDAR
                try
                {
                    ArrayList objAdd;
                    Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                    objAdd = new ArrayList();
                    objAdd.Add(_bill_Sys_Calender.GetCaseIDByPatient(AppointmentInstance.CustomFields[0].ToString()));
                    objAdd.Add(item.Start);
                    objAdd.Add(item.Start.Hour + "." + item.Start.Minute);
                    objAdd.Add(AppointmentInstance.Description);
                    objAdd.Add(AppointmentInstance.CustomFields[2]);
                    objAdd.Add("TY000000000000000003");
                    objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    objAdd.Add(AppointmentInstance.Start.ToString("tt", CultureInfo.InvariantCulture));
                    int endMin = Convert.ToInt32(item.End.Minute);
                    int endHr = Convert.ToInt32(item.End.Hour);
                    string endTime = item.End.Hour + "." + item.End.Minute;
                    if (endMin >= 60)
                    {
                        endMin = endMin - 60;
                        endHr = endHr + 1;
                        if (endHr > 12)
                        {
                            endHr = endHr - 12;
                            if (AppointmentInstance.End.Hour != 12)
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
                            if (AppointmentInstance.End.Hour != 12)
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
                    objAdd.Add(item.End.ToString("tt", CultureInfo.InvariantCulture));
                    objAdd.Add(AppointmentInstance.StatusKey);
                    objAdd.Add(item.CustomFields["VISIT_TYPE_ID"]);
                    objAdd.Add(AppointmentInstance.Id);
                    objAdd.Add(index);
                    _bill_Sys_Calender.SaveEventFromSchedular(objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    index++;
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
                #endregion

            }
            return;
        }
        else if (AppointmentInstance.Type == AppointmentType.Normal)
        {
            string[] clientData = AppointmentInstance.Location.Split(new char[] { '-' });
            Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
            int index = 0;
            if (clientData[2].Trim() != "null")
                if (_bill_Sys_Calender.CHECKVISIT_FOR_APPOINTMENT(Convert.ToInt32(AppointmentInstance.Id), index, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    #region Data Update to TXN_CALENDAR
                    try
                    {
                        ArrayList objAdd;

                        objAdd = new ArrayList();
                        objAdd.Add(_bill_Sys_Calender.GetCaseIDByPatient(clientData[2]));
                        objAdd.Add(AppointmentInstance.Start);
                        objAdd.Add(AppointmentInstance.Start.Hour + "." + AppointmentInstance.Start.Minute);
                        objAdd.Add(AppointmentInstance.Description);
                        objAdd.Add(clientData[1]);
                        objAdd.Add("TY000000000000000003");
                        objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAdd.Add(AppointmentInstance.Start.ToString("tt", CultureInfo.InvariantCulture));
                        int endMin = Convert.ToInt32(AppointmentInstance.End.Minute);
                        int endHr = Convert.ToInt32(AppointmentInstance.End.Hour);
                        string endTime = AppointmentInstance.End.Hour + "." + AppointmentInstance.End.Minute;
                        if (endMin >= 60)
                        {
                            endMin = endMin - 60;
                            endHr = endHr + 1;
                            if (endHr > 12)
                            {
                                endHr = endHr - 12;
                                if (AppointmentInstance.End.Hour != 12)
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
                                if (AppointmentInstance.End.Hour != 12)
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
                        objAdd.Add(AppointmentInstance.End.ToString("tt", CultureInfo.InvariantCulture));
                        objAdd.Add(AppointmentInstance.StatusKey);
                        objAdd.Add(clientData[0]);
                        objAdd.Add(AppointmentInstance.Id);
                        objAdd.Add(index);
                        objAdd.Add(AppointmentInstance.Id);
                        _bill_Sys_Calender.UPDATEEventByAppointmentId(objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                        index++;
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                    #endregion
                }
                else
                {

                    #region Data insert to TXN_CALENDAR
                    try
                    {
                        ArrayList objAdd;

                        objAdd = new ArrayList();
                        objAdd.Add(_bill_Sys_Calender.GetCaseIDByPatient(clientData[2]));
                        objAdd.Add(AppointmentInstance.Start);
                        objAdd.Add(AppointmentInstance.Start.Hour + "." + AppointmentInstance.Start.Minute);
                        objAdd.Add(AppointmentInstance.Description);
                        objAdd.Add(clientData[1]);
                        objAdd.Add("TY000000000000000003");
                        objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAdd.Add(AppointmentInstance.Start.ToString("tt", CultureInfo.InvariantCulture));
                        int endMin = Convert.ToInt32(AppointmentInstance.End.Minute);
                        int endHr = Convert.ToInt32(AppointmentInstance.End.Hour);
                        string endTime = AppointmentInstance.End.Hour + "." + AppointmentInstance.End.Minute;
                        if (endMin >= 60)
                        {
                            endMin = endMin - 60;
                            endHr = endHr + 1;
                            if (endHr > 12)
                            {
                                endHr = endHr - 12;
                                if (AppointmentInstance.End.Hour != 12)
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
                                if (AppointmentInstance.End.Hour != 12)
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
                        objAdd.Add(AppointmentInstance.End.ToString("tt", CultureInfo.InvariantCulture));
                        objAdd.Add(0);
                        objAdd.Add(clientData[0]);
                        objAdd.Add(AppointmentInstance.Id);
                        objAdd.Add(index);
                        _bill_Sys_Calender.SaveEventFromSchedular(objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                    #endregion



                }
        }
        else if (AppointmentInstance.Type == AppointmentType.DeletedOccurrence)
        {
            //Deleted code in TXN_CALENDER_EVENTS
            //DeleteSchedule objSchedule = new DeleteSchedule();
            //objSchedule.Index = apt.RecurrenceIndex;
            //objSchedule.AppointmentID = Convert.ToInt32(apt.RecurrencePattern.Id);
            //billSysDeleteBO.DeleteEventSchedular(objSchedule);
            //return;
        }
        else if (AppointmentInstance.Type == AppointmentType.ChangedOccurrence)
        {
            string[] clientData = AppointmentInstance.Location.Split(new char[] { '-' });

            int index = AppointmentInstance.RecurrenceIndex;
            #region Data Update to TXN_CALENDAR
            try
            {
                ArrayList objAdd;
                Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                objAdd = new ArrayList();
                objAdd.Add(_bill_Sys_Calender.GetCaseIDByPatient(clientData[2]));
                objAdd.Add(AppointmentInstance.Start);
                objAdd.Add(AppointmentInstance.Start.Hour + "." + AppointmentInstance.Start.Minute);
                objAdd.Add(AppointmentInstance.Description);
                objAdd.Add(clientData[1]);
                objAdd.Add("TY000000000000000003");
                objAdd.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                objAdd.Add(AppointmentInstance.Start.ToString("tt", CultureInfo.InvariantCulture));
                int endMin = Convert.ToInt32(AppointmentInstance.End.Minute);
                int endHr = Convert.ToInt32(AppointmentInstance.End.Hour);
                string endTime = AppointmentInstance.End.Hour + "." + AppointmentInstance.End.Minute;
                if (endMin >= 60)
                {
                    endMin = endMin - 60;
                    endHr = endHr + 1;
                    if (endHr > 12)
                    {
                        endHr = endHr - 12;
                        if (AppointmentInstance.End.Hour != 12)
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
                        if (AppointmentInstance.End.Hour != 12)
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
                objAdd.Add(AppointmentInstance.End.ToString("tt", CultureInfo.InvariantCulture));
                objAdd.Add(AppointmentInstance.StatusKey);
                objAdd.Add(clientData[0]);
                objAdd.Add(AppointmentInstance.RecurrencePattern.Id);
                objAdd.Add(index);
                objAdd.Add(AppointmentInstance.Id);
                _bill_Sys_Calender.UPDATEEventByAppointmentId(objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                index++;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            #endregion
        }
    }
    #endregion
    protected void ASPxScheduler1_InitAppointmentDisplayText(object sender, AppointmentDisplayTextEventArgs e)
    {
        Appointment apt = e.Appointment;
        e.Text = apt.Subject;
    }









    protected void ASPxScheduler1_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
    {
        e.Properties.Add("cpAppointmentFormOpened", appointmentFormOpened);
    }

    protected void ASPxScheduler1_InitNewAppointment(object sender, AppointmentEventArgs e)
    {

    }

    protected void ddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProvider();
    }
    private void BindProvider()
    {


        DataSet ds = new SchedularDAO().GetAllDoctorBySpecialty(ddlSpeciality.Value.ToString(), ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        ddlProvider.DataSource = ds.Tables[0];
        ddlProvider.DataBind();
        ddlProvider.SelectedIndex = -1;
    }

    protected void ddlProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ASPxScheduler1.ResourceNavigato
        ASPxScheduler1.Services.ResourceNavigation.GoToResourceById(ddlProvider.Value);
    }


    [System.Web.Services.WebMethod()]
    public static bool CheckVisitForVisitType(string Parameter)
    {
        string[] clientData = Parameter.Split(new char[] { '-' });
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();

        return !_bill_Sys_Calender.CheckVisitForVisitType(clientData[2], clientData[1], clientData[0], ((Bill_Sys_BillingCompanyObject)System.Web.HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    }
    [System.Web.Services.WebMethod()]
    public static string  GetPatientDetail(string Parameter)
    {
        DataSet ds = new SchedularDAO().GetPatientDetail(Parameter, ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            String resultJson = string.Empty;
            resultJson = resultJson + "{";
            resultJson = resultJson + "\"SZ_CASE_NO\":"+ "\""+ds.Tables[0].Rows[0]["sz_case_no"]+"\",";
            resultJson = resultJson + "\"PATIENT_NAME\":" + "\"" + ds.Tables[0].Rows[0]["PATIENT_NAME"] + "\",";
            resultJson = resultJson + "\"DT_DOB\":" + "\"" + ds.Tables[0].Rows[0]["DT_DOB"] + "\",";
            resultJson = resultJson + "\"DT_DATE_OF_ACCIDENT\":" + "\"" + ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"] + "\",";
            resultJson = resultJson + "\"SZ_PATIENT_PHONE\":" + "\"" + ds.Tables[0].Rows[0]["SZ_PATIENT_PHONE"] + "\",";
            resultJson = resultJson + "\"ATTORNEY_NAME\":" + "\"" + ds.Tables[0].Rows[0]["ATTORNEY_NAME"] + "\",";
            resultJson = resultJson + "\"SZ_ATTORNEY_PHONE\":" + "\"" + ds.Tables[0].Rows[0]["sz_attorney_phone"] + "\"";
            resultJson = resultJson + "}";
            return resultJson;
            //ddlPatient.JSProperties.Add("cpSZ_CASE_NO", ds.Tables[0].Rows[0]["sz_case_no"]);
            //ddlPatient.JSProperties.Add("cpPATIENT_NAME", ds.Tables[0].Rows[0]["PATIENT_NAME"]);
            //ddlPatient.JSProperties.Add("cpDT_DOB", ds.Tables[0].Rows[0]["DT_DOB"]);
            //ddlPatient.JSProperties.Add("cpDT_DATE_OF_ACCIDENT", ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"]);
            //ddlPatient.JSProperties.Add("cpSZ_PATIENT_PHONE", ds.Tables[0].Rows[0]["SZ_PATIENT_PHONE"]);
            //ddlPatient.JSProperties.Add("cpATTORNEY_NAME", ds.Tables[0].Rows[0]["ATTORNEY_NAME"]);
            //ddlPatient.JSProperties.Add("cpSZ_ATTORNEY_PHONE", ds.Tables[0].Rows[0]["sz_attorney_phone"]);

        }
        return string.Empty;
    }

       
}

#region CustomMenuViewCallbackCommand
public enum CustomLabel
{
    Default,
    Complete = 1,
    Finalize = 2,
    Incomplete = 3,
    NoShow = 4,
    Billed = 5
}
public enum CustomStatus
{
    Confirmed = 1,
    AwaitingConfirmation = 2,
    Cancelled = 3
}
public class CustomMenuViewCallbackCommand : MenuViewCallbackCommand
{
    string myMenuItemId;

    public CustomMenuViewCallbackCommand(ASPxScheduler control)
        : base(control)
    {
    }

    public string MyMenuItemId { get { return myMenuItemId; } }

    protected override void ParseParameters(string parameters)
    {
        this.myMenuItemId = parameters;
        base.ParseParameters(parameters);
    }
    //protected override void ExecuteCore()
    //{
    //    ExecuteUserMenuCommand(MyMenuItemId);
    //    base.ExecuteCore();
    //}
    //protected internal virtual void ExecuteUserMenuCommand(string menuItemId)
    //{
    //    CreateAppointment("Name", "Contact info:", CustomStatus.Cancelled, CustomLabel.Default);
    //}
    protected void CreateAppointment(string subject, string description, CustomStatus statusKey, CustomLabel labelKey)
    {
        //Appointment apt = Control.Storage.CreateAppointment(AppointmentType.Normal);
        //apt.Subject = subject;
        //apt.Description = description;
        //apt.Start = Control.SelectedInterval.Start;
        //apt.End = Control.SelectedInterval.End;
        //apt.ResourceId = Control.SelectedResource.Id;
        //apt.StatusKey = (int)statusKey;
        //apt.LabelKey = (int)labelKey;
        //Control.Storage.Appointments.Remove(apt);        
    }

}
#endregion

#region CustomMenuAppointmentCallbackCommand
public class CustomMenuAppointmentCallbackCommand : SchedulerCallbackCommand
{
    string menuItemId = String.Empty;

    public CustomMenuAppointmentCallbackCommand(ASPxScheduler control)
        : base(control)
    {
    }

    public override string Id { get { return "USRAPTMENU"; } }
    public string MenuItemId { get { return menuItemId; } }

    protected override void ParseParameters(string parameters)
    {
        this.menuItemId = parameters;
    }
    protected override void ExecuteCore()
    {
        Appointment apt = Control.SelectedAppointments[0];
        if (MenuItemId == "DeleteId")
            apt.Delete();
        else if (MenuItemId == "Default")
            UpdateAppointment(apt, string.Empty, CustomStatus.Cancelled, CustomLabel.Default);
    }
    protected void UpdateAppointment(Appointment apt, string subject, CustomStatus statusKey, CustomLabel labelKey)
    {
        //if (labelKey.Equals(CustomLabel.Default) || labelKey.Equals(CustomLabel.Default))
        //{
        //    apt.Subject = subject;
        //    apt.Description = string.Empty;
        //}
        //else if ((apt.LabelKey.Equals((int)CustomLabel.Default) || apt.LabelKey.Equals((int)CustomLabel.Default)) && !labelKey.Equals(CustomLabel.Default) && !labelKey.Equals(CustomLabel.Default))
        //{
        //    apt.Subject = "Name";
        //    apt.Description = "Contact info:";
        //}
        //apt.StatusKey = (int)statusKey;
        //apt.LabelKey = (int)labelKey;
    }
}
#endregion