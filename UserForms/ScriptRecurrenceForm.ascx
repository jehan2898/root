<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ScriptRecurrenceForm.ascx.cs" Inherits="UserForms_ScriptRecurrenceForm" %>
<div id="mainDiv">
    <table>
        <tr>
            <td>
                <dx:RecurrenceTypeEdit ID="edtRecurrenceTypeEdit" runat="server" ItemSpacing="10px"
                    SelectedIndex="0" Border-BorderWidth="0px">
                </dx:RecurrenceTypeEdit>
            </td>
            <td style="vertical-align: top; padding-top: 3px">
                <dx:DailyRecurrenceControl ID="edtDailyRecurrenceControl" runat="server" ClientVisible="false">
                </dx:DailyRecurrenceControl>
                <dx:WeeklyRecurrenceControl ID="edtWeeklyRecurrenceControl" runat="server" ClientVisible="false">
                </dx:WeeklyRecurrenceControl>
                <dx:MonthlyRecurrenceControl ID="edtMonthlyRecurrenceControl" runat="server" ClientVisible="false">
                </dx:MonthlyRecurrenceControl>
                <dx:YearlyRecurrenceControl ID="edtYearlyRecurrenceControl" runat="server" ClientVisible="false">
                </dx:YearlyRecurrenceControl>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <dx:RecurrenceRangeControl ID="edtRecurrenceRangeControl" runat="server">
                </dx:RecurrenceRangeControl>
            </td>
        </tr>
    </table>
</div>
<script id="dxss_ASPxSchedulerClientRecurrenceForm" type="text/javascript">
    // <![CDATA[
    ASPxClientRecurrenceAppointmentForm = ASPx.CreateClass(ASPxClientFormBase, {
        Initialize: function () {
            this.defaultRecurrenceInfo = new ASPxClientRecurrenceInfo();
            this.controls.edtRecurrenceTypeEdit.SetSelectedIndex(0);
            this.controls.edtDailyRecurrenceControl.SetVisible(true);
            this.recurrenceEditors = {};
            this.recurrenceEditors[ASPxClientRecurrenceType.Daily] = this.controls.edtDailyRecurrenceControl;
            this.recurrenceEditors[ASPxClientRecurrenceType.Weekly] = this.controls.edtWeeklyRecurrenceControl;
            this.recurrenceEditors[ASPxClientRecurrenceType.Monthly] = this.controls.edtMonthlyRecurrenceControl;
            this.recurrenceEditors[ASPxClientRecurrenceType.Yearly] = this.controls.edtYearlyRecurrenceControl;
            this.controls.edtRecurrenceRangeControl.rbNoEndDate.SetVisible(false);
            this.controls.edtRecurrenceRangeControl.rbEndAfterNumberOfOccurrences.SetChecked(true);
            //this.controls.edtRecurrenceTypeEdit.rbYearly.SetVisible(false);
            this.controls.edtRecurrenceTypeEdit.SelectedIndexChanged.AddHandler(ASPx.CreateDelegate(this.OnEdtRecurrenceTypeEditSelectedIndexChanged, this));
        },
        OnEdtRecurrenceTypeEditSelectedIndexChanged: function (s, e) {
            for (var editor in this.recurrenceEditors)
                this.recurrenceEditors[editor].SetVisible(false);
            var activeEditor = this.recurrenceEditors[this.controls.edtRecurrenceTypeEdit.recurrenceType];
            activeEditor.SetVisible(true);
        },
        Parse: function () {
            var recurrenceInfo = new ASPxClientRecurrenceInfo();
            var recurrenceType = this.controls.edtRecurrenceTypeEdit.GetRecurrenceType();
            recurrenceInfo.SetRecurrenceType(recurrenceType);
            var valueAccessor = this.recurrenceEditors[recurrenceType].valueAccessor;
            recurrenceInfo.SetDayNumber(valueAccessor.GetDayNumber());
            recurrenceInfo.SetPeriodicity(valueAccessor.GetPeriodicity());
            recurrenceInfo.SetMonth(valueAccessor.GetMonth());
            recurrenceInfo.SetWeekDays(valueAccessor.GetWeekDays());
            recurrenceInfo.SetWeekOfMonth(valueAccessor.GetWeekOfMonth());
            recurrenceInfo.SetRange(this.controls.edtRecurrenceRangeControl.GetRange());
            recurrenceInfo.SetOccurrenceCount(this.controls.edtRecurrenceRangeControl.GetOccurrenceCount());
            recurrenceInfo.SetStart(this.controls.edtRecurrenceRangeControl.GetEndDate());
            recurrenceInfo.SetEnd(this.controls.edtRecurrenceRangeControl.GetEndDate());
            return recurrenceInfo;
        },
        Update: function (start, recurrenceInfo) {
            debugger;
            if (!recurrenceInfo || !start)
                return;
            var recurrenceTypeToHide = this.controls.edtRecurrenceTypeEdit.GetRecurrenceType();
            this.controls.edtRecurrenceTypeEdit.SetRecurrenceType(recurrenceInfo.type);
            this.recurrenceEditors[recurrenceTypeToHide].SetVisible(false);
            switch (recurrenceInfo.type) {
                case 0:
                    this.recurrenceEditors["Daily"].SetVisible(true);
                    break;
               
                case 1:
                    this.recurrenceEditors["Monthly"].SetVisible(true);
                    break;
                case 2:
                    this.recurrenceEditors["Weekly"].SetVisible(true);
                    break;
                case 3:
                    this.recurrenceEditors["Yearly"].SetVisible(true);
                    break;
                



            }

            this.controls.edtRecurrenceRangeControl.SetStart(start);
            this.controls.edtRecurrenceRangeControl.SetRange(recurrenceInfo.range);
            this.controls.edtRecurrenceRangeControl.SetOccurrenceCount(recurrenceInfo.occurrenceCount);
            this.controls.edtRecurrenceRangeControl.SetEndDate(recurrenceInfo.interval.GetEnd());
            for (var editorType in this.recurrenceEditors) {
                var editor = this.recurrenceEditors[editorType];
                var actualRecurrenceInfo = (editorType == recurrenceInfo.type) ? recurrenceInfo : this.defaultRecurrenceInfo;
                editor.Update(actualRecurrenceInfo);
            }
        },
        Clear: function () {
            this.Update(this.defaultRecurrenceInfo);
            this.SetVisible(false);
        },
        SetVisible: function (isVisible) {
            var element = document.getElementById('mainDiv');
            this.SetVisibleCore(element, isVisible);
        }
    });
    // ]]> 
</script>
