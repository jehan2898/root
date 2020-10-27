using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;

public partial class AJAX_Pages_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
            The following code utilizes the DataHelper class, which is implemented
            solely for the ASPxScheduler Demo project.
            It is intended to hide unnecessary details and clarify the main idea.
            For the recommended data binding techniques, please refer to the
            Data Binding section modules:
              ~/DataBinding/BoundMode.aspx for MS Access database
              ~/DataBinding/ListBoundMode.aspx for the object with IBindingList interface.
              ~/DataBinding/XPOBoundMode.aspx for XPO data framework.
        */
        //ASPxScheduler1.DataBind();

        ApplyUserRestrictions();
    }

    void ApplyUserRestrictions()
    {
        ASPxScheduler1.BeginUpdate();
        try
        {
            SchedulerOptionsCustomization options = ASPxScheduler1.OptionsCustomization;
            options.AllowAppointmentConflicts = chkAllowConflicts.Checked ? AppointmentConflictsMode.Allowed : AppointmentConflictsMode.Forbidden;
            options.AllowAppointmentCopy = ToUsedAppointmentType(chkAllowCopy);
            options.AllowAppointmentCreate = ToUsedAppointmentType(chkAllowCreate);
            options.AllowAppointmentDelete = ToUsedAppointmentType(chkAllowDelete);
            options.AllowAppointmentDrag = ToUsedAppointmentType(chkAllowDrag);
            options.AllowAppointmentDragBetweenResources = ToUsedAppointmentType(chkAllowDragBetweenResources);
            options.AllowAppointmentEdit = ToUsedAppointmentType(chkAllowEdit);
            options.AllowInplaceEditor = ToUsedAppointmentType(chkAllowInplaceEditor);
            options.AllowAppointmentMultiSelect = chkAllowMultiSelect.Checked;
            options.AllowAppointmentResize = ToUsedAppointmentType(chkAllowResize);
        }
        finally
        {
            ASPxScheduler1.EndUpdate();
        }
        ASPxScheduler1.ApplyChanges(ASPxSchedulerChangeAction.RenderViewMenu);
    }

    UsedAppointmentType ToUsedAppointmentType(ASPxCheckBox chk)
    {
        return chk.Checked ? UsedAppointmentType.All : UsedAppointmentType.None;
    }
}