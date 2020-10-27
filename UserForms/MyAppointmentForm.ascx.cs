using System;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;
using System.Collections.Generic;
using System.Web.UI;

public partial class Templates_MyAppointmentForm : SchedulerFormControl {
    protected void Page_Load(object sender, EventArgs e) {
        PrepareChildControls();
        tbSubject.Focus();
        //AppointmentRecurrenceForm1.Controls[0].fi
        
        //List<DevExpress.Web.ASPxScheduler.Controls.AppointmentRecurrenceControl> labels = this.GetControls<DevExpress.Web.ASPxScheduler.Controls.AppointmentRecurrenceControl>().ToList();
        lblSubject.Visible = lblLocation.Visible = tbSubject.Visible = tbLocation.Visible = false;
    }
    private void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection)
 where T : Control
    {
        foreach (Control control in controlCollection)
        {
            //if (control.GetType() == typeof(T))
            if (control is T) // This is cleaner
                resultCollection.Add((T)control);

            if (control.HasControls())
                GetControlList(control.Controls, resultCollection);
        }
    }
    public override void DataBind() {
        base.DataBind();
        MyAppointmentFormTemplateContainer container = (MyAppointmentFormTemplateContainer)Parent;
        AppointmentRecurrenceForm1.Visible = container.ShouldShowRecurrence;
        Appointment apt = container.Appointment;
        
        if (apt.CustomFields[2] != null)
        {
            ddlDoctor.SelectedIndex = ddlDoctor.Items.FindByValue(apt.CustomFields[2].ToString()).Index;
            ddlPatient.SelectedIndex = ddlPatient.Items.FindByValue(apt.CustomFields[0].ToString()).Index;
            ddlVisit.SelectedIndex = ddlVisit.Items.FindByValue(apt.CustomFields[3].ToString()).Index;
        }
        btnOk.ClientSideEvents.Click = container.SaveHandler;
        btnCancel.ClientSideEvents.Click = container.CancelHandler;
        btnDelete.ClientSideEvents.Click = container.DeleteHandler;
    }
    protected override void PrepareChildControls() {
        AppointmentFormTemplateContainer container = (AppointmentFormTemplateContainer)Parent;
        ASPxScheduler control = container.Control;
        
        AppointmentRecurrenceForm1.EditorsInfo = new EditorsInfo(control, control.Styles.FormEditors, control.Images.FormEditors, control.Styles.Buttons);
        base.PrepareChildControls();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
       
    }
    
    protected override ASPxEditBase[] GetChildEditors() {
        ASPxEditBase[] edits = new ASPxEditBase[] {
		  lblSubject, tbSubject, 
			lblLocation, tbLocation, lblStartTime, edtStartDate,
			lblEndTime, edtEndDate, lblDescription,ddlPatient,ddlDoctor,ddlVisit
		};
        return edits;
    }
    protected override ASPxButton[] GetChildButtons() {
        ASPxButton[] buttons = new ASPxButton[] {
			btnOk, btnCancel, btnDelete
		};
        return buttons;
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        //ASPxLabel1.Text = ddlResource.KeyValue.ToString();
    }

    protected void AppointmentRecurrenceForm1_PreRender(object sender, EventArgs e)
    {
        List<Control> allControls = new List<Control>();
        GetControlList<Control>(AppointmentRecurrenceForm1.Controls, allControls);
        foreach (var childControl in allControls)
        {
            if (childControl.ClientID.Equals("Yearly"))
            {
                {
                    // childControl.Visible = false;
                }
            }
            if (childControl.ClientID.Equals("ctl00_ContentPlaceHolder1_ASPxScheduler1_formBlock_AptFrmContainer_AptFrmTemplateContainer_AppointmentForm_AppointmentRecurrenceForm1_AptRecCtl_RangeCtl_DeNoEnd"))
            {
                {

                    childControl.Visible = false;
                }
            }
            //List<DevExpress.Web.ASPxScheduler.Controls.RecurrenceRangeControl> allControls2 = new List<DevExpress.Web.ASPxScheduler.Controls.RecurrenceRangeControl>();
            //GetControlList<DevExpress.Web.ASPxScheduler.Controls.RecurrenceRangeControl>(childControl.Controls, allControls2);  //     call for all controls of the page
            //foreach (var childControl2 in allControls2)
            //{
            //    List<Control> allControls3 = new List<Control>();
            //    GetControlList<Control>(childControl2.Controls, allControls3);  //     call for all controls of the page 
            //    foreach (var childControl3 in allControls3)
            //    {
            //    }
        }
    }
}
