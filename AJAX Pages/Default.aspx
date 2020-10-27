<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AJAX_Pages_Default" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function UpdateScheduler() {
            LoadingPanel.Show();
            ASPxClientScheduler1.Refresh(); 
        }
        function EnableEditors() {W
            LoadingPanel.Hide();
        }
    </script>
    <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" Width="30%" Height="150px" HeaderText="Group by" style="float: left; margin: 0 16px 16px 0;">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <dx:ASPxRadioButtonList ID="rbGroupType" runat="server" ValueType="System.Int32"
                    SelectedIndex="1" RepeatDirection="Vertical" TextWrap="False" Width="100%" ItemSpacing="2px"
                    EnableClientSideAPI="True">
                    <Border BorderStyle="None" />
                    <Items>
                        <dx:ListEditItem Text="None" Value="0" />
                        <dx:ListEditItem Text="Date" Value="1" />
                        <dx:ListEditItem Text="Resource" Value="2" />
                    </Items>
                    <Paddings Padding="0px" />
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
                    switch(s.savedSelectedIndex) {
                        case 0:
                            ASPxClientScheduler1.SetGroupType(ASPxSchedulerGroupType.None);
                            break;
                        case 1:
                            ASPxClientScheduler1.SetGroupType(ASPxSchedulerGroupType.Date);
                            break;
                        case 2:
                            ASPxClientScheduler1.SetGroupType(ASPxSchedulerGroupType.Resource);
                            break;
                    }
                    }" />
                </dx:ASPxRadioButtonList>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="60%" Height="150px" HeaderText="Allow appointment" style="float: left; margin: 0 0 16px 0;">
        <PanelCollection>
            <dx:PanelContent runat="server">
                <table class="OptionsTable">
                    <tr>
                        <td>
                            <dx:ASPxCheckBox ID="chkAllowCreate" runat="server" Text="Create" Checked="True"
                                Wrap="False">
                                <ClientSideEvents CheckedChanged="function(s, e) { UpdateScheduler(); }" />
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxCheckBox ID="chkAllowEdit" runat="server" Text="Edit" Checked="True" Wrap="False">
                                <ClientSideEvents CheckedChanged="function(s, e) { UpdateScheduler(); }" />
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxCheckBox ID="chkAllowResize" runat="server" Text="Resize" Checked="True"
                                Wrap="False">
                                <ClientSideEvents CheckedChanged="function(s, e) { UpdateScheduler(); }" />
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxCheckBox ID="chkAllowConflicts" runat="server" Text="Conflicts" Checked="True"
                                Wrap="False">
                                <ClientSideEvents CheckedChanged="function(s, e) { UpdateScheduler(); }" />
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxCheckBox ID="chkAllowDragBetweenResources" runat="server" Text="Drag Between Resources"
                                Checked="True" Wrap="False">
                                <ClientSideEvents CheckedChanged="function(s, e) { UpdateScheduler(); }" />
                            </dx:ASPxCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxCheckBox ID="chkAllowDelete" runat="server" Text="Delete" Checked="True"
                                Wrap="False">
                                <ClientSideEvents CheckedChanged="function(s, e) { UpdateScheduler(); }" />
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxCheckBox ID="chkAllowCopy" runat="server" Text="Copy" Checked="True" Wrap="False">
                                <ClientSideEvents CheckedChanged="function(s, e) { UpdateScheduler(); }" />
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxCheckBox ID="chkAllowDrag" runat="server" Text="Drag" Checked="True" Wrap="False">
                                <ClientSideEvents CheckedChanged="function(s, e) { UpdateScheduler(); }" />
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxCheckBox ID="chkAllowMultiSelect" runat="server" Text="Multi Select" Checked="True"
                                Wrap="False">
                                <ClientSideEvents CheckedChanged="function(s, e) { UpdateScheduler(); }" />
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxCheckBox ID="chkAllowInplaceEditor" runat="server" Text="Inplace Editor"
                                Checked="True" Wrap="False">
                                <ClientSideEvents CheckedChanged="function(s, e) { UpdateScheduler(); }" />
                            </dx:ASPxCheckBox>
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
    <br />
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" ContainerElementId="ASPxRoundPanel1"
        Modal="true">
    </dx:ASPxLoadingPanel>
    <dx:ASPxScheduler ID="ASPxScheduler1" runat="server" Width="100%" ActiveViewType="Day" GroupType="Date" ClientInstanceName="ASPxClientScheduler1"
        AppointmentDataSourceID="AppointmentDataSource" ResourceDataSourceID="efResourceDataSource">
        <Views>
            <DayView ResourcesPerPage="2" />
            <WorkWeekView ResourcesPerPage="2" />
            <WeekView Enabled="false" />
            <MonthView ResourcesPerPage="2" AppointmentDisplayOptions-ShowRecurrence="true" WeekCount="4">
                <MonthViewStyles>
                    <DateCellBody Height="100px" />
                </MonthViewStyles>
            </MonthView>
            <TimelineView ResourcesPerPage="2">
                <TimelineViewStyles>
                    <TimelineCellBody Height="300px" />
                </TimelineViewStyles>
            </TimelineView>
        </Views>
        <Storage EnableReminders="false" />
        <ClientSideEvents EndCallback="function(s,e) { EnableEditors(); }" />
    </dx:ASPxScheduler>
</asp:Content>