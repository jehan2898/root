<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_GenerateMenu.aspx.cs"
    Inherits="Bill_Sys_GenerateMenu" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" src="validation.js"></script>
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
                                                <td class="ContentLabel" style="text-align:center; height:25px;" colspan="4" >
                                                <asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
                                                <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                </td> 
                                                </tr> 
                                                
                                                 <tr>
                 <td class="ContentLabel" style="width: 15%">Menu Name</td>
                   <td style="width: 35%">
                        <asp:TextBox ID="txtMenuName" runat="server" Width="250px" MaxLength="100"></asp:TextBox></td>
                   <td class="ContentLabel" style="width: 15%"></td> 
                     <td style="width: 35%">
                        <asp:CheckBox ID="chkIsAccessible" runat="server" Checked="True" Text="Is Accessible" /></td>
                </tr>
                <tr>
                    <td class="ContentLabel" style="width: 15%">Parent Menu</td>
                   <td style="width: 35%">
                        <extddl:ExtendedDropDownList ID="extddlParentID" runat="server" Width="255px" Connection_Key="Connection_String"
                            Flag_Key_Value="GET_PARENT_MENU_LIST" Procedure_Name="SP_MST_MENU" Selected_Text="---Select---" />
                    </td>
                 <td class="ContentLabel" style="width: 15%">Menu Link</td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtMenuLink" runat="server" Width="250px" MaxLength="1000"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="ContentLabel" style="width: 15%">Description</td>
                    <td style="width: 35%">
                        <asp:TextBox ID="txtMenuDesc" runat="server" Width="250px" Height="100px" TextMode="MultiLine"
                            MaxLength="100"></asp:TextBox></td>
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
                                                   <asp:TextBox ID="txtMenuCode" runat="server" Width="10px" Visible="False" MaxLength="100"></asp:TextBox>
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px" CssClass="Buttons" />
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                            Width="80px" CssClass="Buttons" />
                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px" CssClass="Buttons"/>
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
                                    
                                    <asp:DataGrid ID="grdMenu" runat="server" OnPageIndexChanged="grdMenu_PageIndexChanged"
                            OnSelectedIndexChanged="grdMenu_SelectedIndexChanged" Width="100%" CssClass="GridTable" AutoGenerateColumns="false"  AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                          
                         <ItemStyle CssClass="GridRow"/>
                            <Columns>
                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="I_MENU_ID" HeaderText="MENU ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_MENU_NAME" HeaderText="NAME"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_MENU_CODE" HeaderText="CODE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_MENU_LINK" HeaderText="LINK"></asp:BoundColumn>
                                <asp:BoundColumn DataField="BIT_MENU_ACCESS" HeaderText="ACCESS"></asp:BoundColumn>
                                <asp:BoundColumn DataField="I_PARENT_ID" HeaderText="PARENT ID"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION"></asp:BoundColumn>
                                <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                            </Columns>
                          <HeaderStyle CssClass="GridHeader"/>
                        </asp:DataGrid>                                     
                                        
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



