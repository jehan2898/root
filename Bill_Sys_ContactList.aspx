<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_ContactList.aspx.cs" Inherits="Bill_Sys_ContactList" %>


<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization" TagPrefix="CPA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align:top;">
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
                                                <td class="ContentLabel" colspan="4" style="text-align:center; height:25px;">
                                                </td> 
                                                </tr> 
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Search:</td>
                                                <td style="width: 35%">
                                                   <asp:TextBox ID="txtSearchList" runat="server" Width="250px"></asp:TextBox>
                                                   </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    </td>
                                                <td style="width: 35%">
                                                    </td>
                                            </tr>
                                        
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">                                                                   
              <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="Buttons" Text="Search" OnClick="btnSearch_Click" />
                                                    </td>
                                            </tr>
                                        </table>
                                    
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                    <asp:DataGrid ID="grdSearchContactList" runat="server" OnPageIndexChanged="grdSearchContactList_PageIndexChanged" Width="100%" CssClass="GridTable" AutoGenerateColumns="false"  AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                                   <ItemStyle CssClass="GridRow"/>
                                   <HeaderStyle CssClass="GridHeader" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false" ></asp:BoundColumn>
                        <asp:BoundColumn DataField="NAME" HeaderText="NAME"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ADDRESS" HeaderText="ADDRESS"></asp:BoundColumn>
                        <asp:BoundColumn DataField="STREET" HeaderText="STREET"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CITY" HeaderText="CITY"></asp:BoundColumn>
                        <asp:BoundColumn DataField="STATE" HeaderText="STATE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ZIP" HeaderText="ZIP"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PHONE" HeaderText="PHONE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="FAX" HeaderText="FAX"></asp:BoundColumn>
                        <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TYPE" HeaderText="TYPE"></asp:BoundColumn>
                    </Columns>
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

