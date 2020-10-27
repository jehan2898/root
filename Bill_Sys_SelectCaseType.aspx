<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_SelectCaseType.aspx.cs" Inherits="Bill_Sys_SelectCaseType" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;padding-top: 3px; height: 100%;vertical-align:top;">
                    <table  id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%; ">
                        <tr>
                            <td class="LeftTop">
                            </td>
                            <td class="CenterTop">
                            </td>
                            <td class="RightTop">
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftCenter" style="height: 100%">
                            </td>
                            <td class="Center" valign="top">
                                <table width="100%">
                                
                                    <tr>
                                        <td width="100%" align="center" valign="top" scope="col" colspan="6" style="height: 428px">
                                            <div align="left">
                                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3" class="TDPart">
                                                    <tr>
                                                        <td>
                                                            <span style="font-weight:bold;">Add new patient</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" height="30">
                                                            <div id="ErrorDiv" style="color: red" visible="true">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" style="vertical-align:top; text-align:center;">      
                                                            <div class="lbl" style="text-align:center;"> 
                                                                Select Case Type
                                                                    <cc1:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="200px" Connection_Key="Connection_String" Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---"   />     
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" style="vertical-align:top; " width="100%" align="center">      
                                                            <asp:RadioButtonList ID="rdlst" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Normal View" Value="0" Selected="true"></asp:ListItem>
                                                                <asp:ListItem Text="Tabbed View" Value="1" Selected="true"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                                
                                                                    
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td colspan="6" style="vertical-align:top; text-align:center;">      
                                                            <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" CssClass="Buttons"/>
                                                                
                                                                    
                                                        </td>
                                                    </tr>
                                                </table> 
                                            </div> 
                                        </td>
                                    </tr> 
                                </table> 
                            </td>
                            <td class="RightCenter" style="width: 10px; height: 100%;">
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftBottom">
                            </td>
                            <td class="CenterBottom">
                            </td>
                            <td class="RightBottom" style="width: 10px">
                            </td>
                        </tr>
                    </table>                
                </td>
                </tr>
                </table> 
    </asp:Content>