<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Doctor.ascx.cs" Inherits="UserControl_Doctor" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<div id="divDummyArea" runat="server">
    <table>
        <tr>
            <td>
                <dx:ASPxLabel ID="lblReadingDoctor" runat="server" Text="Reading Doctor">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="cmbActiveDoctor" runat="server" Width="170px" DropDownWidth="550"
                    DropDownStyle="DropDownList" ValueField="SZ_DOCTOR_ID" ClientInstanceName="cntDoctor"
                    ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith"
                    CallbackPageSize="30">
                    <Columns>
                        <dx:ListBoxColumn FieldName="SZ_DOCTOR_NAME" Width="130px" />
                        <dx:ListBoxColumn FieldName="SZ_OFFICE" Width="100px" />
                        <dx:ListBoxColumn FieldName="SZ_DOCTOR_LICENSE_NUMBER" Width="70px" />
                        <dx:ListBoxColumn FieldName="SZ_NPI" Width="100px" />
                        <dx:ListBoxColumn FieldName="SZ_WCB_RATING_CODE" Width="100px" />
                        <dx:ListBoxColumn FieldName="SZ_PROCEDURE_GROUP" Width="100px" />
                    </Columns>
                </dx:ASPxComboBox>
            </td>
        </tr>
    </table>
</div>
<div id="divArea" runat="server">
    <table>
        <tr>
            <td>
                <dx:ASPxLabel ID="lblReferringDoctor" runat="server" Text="Referring Doctor">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="cmbReferrringDoctor" runat="server" Width="170px" DropDownWidth="550"
                    DropDownStyle="DropDown" ValueField="SZ_DOCTOR_ID" ClientInstanceName="cntDoctor1"
                    ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith"
                    CallbackPageSize="30">
                    <Columns>
                        <dx:ListBoxColumn FieldName="SZ_DOCTOR_NAME" Width="130px" />
                        <dx:ListBoxColumn FieldName="SZ_OFFICE" Width="100px" />
                        <dx:ListBoxColumn FieldName="SZ_DOCTOR_LICENSE_NUMBER" Width="70px" />
                        <dx:ListBoxColumn FieldName="SZ_NPI" Width="100px" />
                        <dx:ListBoxColumn FieldName="SZ_WCB_RATING_CODE" Width="100px" />
                        <dx:ListBoxColumn FieldName="SZ_OFFICE" Width="100px" />
                        <dx:ListBoxColumn FieldName="SZ_PROCEDURE_GROUP" Width="100px" />
                    </Columns>
                </dx:ASPxComboBox>
            </td>
        </tr>
    </table>
</div>
