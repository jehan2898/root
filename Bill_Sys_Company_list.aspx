<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_Company_list.aspx.cs" Inherits="Company_list" Title="Untitled Page" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
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
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Company List
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                   <cc1:ExtendedDropDownList ID="extddlcompname" Width="93%" runat="server" Connection_Key="Connection_String"
                                                    Procedure_Name="SP_MST_BILLING_COMPANY" Flag_Key_Value="GET_COMPANY_LIST"
                                                    Selected_Text="--- Select ---" Visible="true" Height="20px" /></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                  
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                   </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                         
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">                                                    
                                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" Width="80px"
                                                        CssClass="Buttons" />                                                   
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td style="width: 100%" class="TDPart">                        
                                        <asp:DataGrid ID="gridshow" CssClass="GridTable" Width="100%" runat="server" AutoGenerateColumns="False" Height="130px">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" HeaderText="Select">
                                                    <ItemStyle CssClass="grid-item-left" />
                                                </asp:ButtonColumn>                  
                                                <asp:BoundColumn  DataField="SZ_TASK" HeaderText="Task"></asp:BoundColumn>
                                                <asp:BoundColumn  DataField = "SZ_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>                                  
                                            </Columns>                                            
                                            <HeaderStyle CssClass="GridHeader" />
                                       </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftBottom" style="height: 10px">
                        </td>
                        <td class="CenterBottom" style="height: 10px">
                        </td>
                        <td class="RightBottom" style="width: 10px; height: 10px;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table> 
</asp:Content>

