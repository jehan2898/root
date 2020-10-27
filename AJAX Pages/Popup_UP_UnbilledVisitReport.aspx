<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_UP_UnbilledVisitReport.aspx.cs" Inherits="AJAX_Pages_Popup_UP_UnbilledVisitReport" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table style="width:100%;padding-left:0px;height:30px;" border="0">
                <tr>
                    <td style="background-color:#B5DF82;font-family:Calibri;font-size:20px;font-weight:normal;font-style:italic;"><%--#CDCAB9--%>
                       All unbilled visits
                    </td>
                </tr>
            </table>

            <table width="100%" border="0">
                <tr>
                    <td style="width:100%;text-align:center;font-family:Calibri;font-size:14px;">
                        <dx:ASPxLabel runat="server" id="lblErrorMessage" ForeColor="Red"></dx:ASPxLabel>
                        <dx:ASPxLabel runat="server" id="lblMessage" ForeColor="Green"></dx:ASPxLabel>
                    </td>
                </tr>
            </table>

            <table style="width:70%;padding-left:0px;" border="0">
                <tr>
                    <td>
                        <dxe:ASPxRadioButton ClientInstanceName="rbt1"  GroupName="UnbilledVisits" runat="server" ID="rbt1" ></dxe:ASPxRadioButton>
                    </td>
                    <td class="pref-desc">
                      All unbilled visits - Shows all unbilled visits in your account as per the filter settings you make
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxRadioButton ClientInstanceName="rbt2" ID="rbt2" runat="server"  GroupName="UnbilledVisits"></dxe:ASPxRadioButton>
                    </td>
                    <td>
                      First unbilled visit - Shows the first unbilled visit among all the unbilled visits for a patient
                    </td>
                </tr>

             
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table style="width:50%">
                <tr>
                    <td>
                        <dx:ASPxButton 
                            width="200px"
                            runat="server" Text="Submit" ID="btnsave" OnClick="btnsave_Click" AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e) {
                                if (!rbt1.GetChecked() && !rbt2.GetChecked()) {
                                    alert('Select atleast one preference. ');
                                    e.processOnServer = false;
                                }
                            }" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                       <dx:ASPxButton runat="server" ID="btnRemovePreferences" Text="Remove Preferences" 
                            width="200px"
                            AutoPostBack="false" OnClick="btnRemovePreferences_Click">
                            <ClientSideEvents Click="function(s, e) {
                                e.processOnServer = confirm('Are you sure you want to remove the preferences?');
                            }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
