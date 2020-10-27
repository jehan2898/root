using System.Web.UI;
using DevExpress.Web.ASPxScheduler;

public partial class UserForms_ScriptRecurrenceForm : ASPxSchedulerClientFormBase {
    public override string ClassName { get { return "ASPxClientRecurrenceAppointmentForm"; } }

    protected override Control[] GetChildControls() {
        Control[] controls = new Control[] { edtDailyRecurrenceControl, edtWeeklyRecurrenceControl, 
            edtMonthlyRecurrenceControl, edtYearlyRecurrenceControl, 
            edtRecurrenceTypeEdit, edtRecurrenceRangeControl};
        return controls;
    }
}
