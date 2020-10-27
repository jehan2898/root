<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_UP_PatientList.aspx.cs" Inherits="Popup_UP_PatientList" %>
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
    <title>Greenyourbills.com - User Preferences - Patient List</title>
    
    <script type="text/javascript" language="javascript">
        function check(s, e) {
            if (chk_load1.GetChecked()) {
                cpddl_list.SetEnabled(false);
                cpddl_list.SetSelectedIndex(0);
                e.processOnServer = false;
            }
            else {
                cpddl_list.SetEnabled(true);
            }
            e.processOnServer = false;
        }
    </script>
    <style type="text/css">
        .pref-desc
        {
            font-family:Calibri;
            text-align:left;
            font-size:15px;
            font-style:normal;
            vertical-align:text-top;
            padding-top:1px;
        }        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table style="width:100%;padding-left:0px;height:30px;" border="0">
                <tr>
                    <td style="background-color:#B5DF82;font-family:Calibri;font-size:20px;font-weight:normal;font-style:italic;"><%--#CDCAB9--%>
                        My Preferences
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
                        <dxe:ASPxRadioButton ClientInstanceName="rbt1"  GroupName="patientlist" runat="server" ID="rbt1" ></dxe:ASPxRadioButton>
                    </td>
                    <td class="pref-desc">
                        Do not show patient(s) on login
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxRadioButton ClientInstanceName="rbt2" ID="rbt2" runat="server"  GroupName="patientlist"></dxe:ASPxRadioButton>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td class="pref-desc">
                                    Show last
                                </td>
                                <td>
                                    <dxe:aspxcombobox runat="server" enablesynchronization="False" selectedindex="0" valuetype="System.String" clientinstancename="cpddl_list" cssclass="inputBox" ID="ddl_list">
                                        <Items>
                                            <dx:ListEditItem Text="5" Value="5"/>
                                            <dx:ListEditItem Text="10" Value="10" />
                                            <dx:ListEditItem Text="20" Value="20" />
                                            <dx:ListEditItem Text="30" Value="30" />
                                            <dx:ListEditItem Text="40" Value="40" />
                                            <dx:ListEditItem Text="50" Value="50" />
                                            <dx:ListEditItem Text="60" Value="60" />
                                            <dx:ListEditItem Text="70" Value="70" />
                                            <dx:ListEditItem Text="80" Value="80" />
                                            <dx:ListEditItem Text="90" Value="90" />
                                            <dx:ListEditItem Text="100" Value="100" />     
                                        </Items>
                                        <ItemStyle>
                                            <HoverStyle BackColor="#F6F6F6">
                                            </HoverStyle>
                                        </ItemStyle>
                                    </dxe:aspxcombobox>
                                </td>
                                <td class="pref-desc">
                                    patients added to <%= szCompanyName %>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr style="visibility:hidden;">
                    <td>
                        <dx:ASPxRadioButton ClientInstanceName="rbt3" ID="rbt3"  runat="server" GroupName="patientlist"></dx:ASPxRadioButton>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td class="pref-desc">
                                    Show last
                                </td>
                                <td>
                                    <dxe:aspxcombobox runat="server" enablesynchronization="False" selectedindex="0" 
                                        valuetype="System.String" clientinstancename="cpddl_list40" cssclass="inputBox" ID="ddl_list40">
                                        <Items>
                                            <dx:ListEditItem Text="None" Value="00" />
                                            <dx:ListEditItem Text="5" Value="5"/>
                                            <dx:ListEditItem Text="10" Value="10" />
                                            <dx:ListEditItem Text="20" Value="20" />
                                            <dx:ListEditItem Text="30" Value="30" />
                                            <dx:ListEditItem Text="40" Value="40" />
                                            <dx:ListEditItem Text="50" Value="50" />
                                            <dx:ListEditItem Text="60" Value="60" />
                                            <dx:ListEditItem Text="70" Value="70" />
                                            <dx:ListEditItem Text="80" Value="80" />
                                            <dx:ListEditItem Text="90" Value="90" />
                                            <dx:ListEditItem Text="100" Value="100" />     
                                        </Items>
                                        <ItemStyle>
                                            <HoverStyle BackColor="#F6F6F6">
                                            </HoverStyle>
                                        </ItemStyle>
                                    </dxe:aspxcombobox>
                                </td>
                                <td class="pref-desc">
                                    patients I have viewed
                                </td>
                            </tr>
                        </table>
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
                                if (!rbt1.GetChecked() && !rbt2.GetChecked() && !rbt3.GetChecked()) {
                                    alert('Select atleast one preference. To desect all preferences select \'None\' from either options ');
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
