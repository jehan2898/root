<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_Officetype.aspx.cs" Inherits="Bill_Sys_Officetype" %>

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
                                                <td class="ContentLabel" style="width: 15%">
                                                    Office Type:</td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtOfficeType" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
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
                        <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" cssclass="Buttons" OnClick="btnSave_Click"/>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update"
                            Width="80px" cssclass="Buttons" OnClick="btnUpdate_Click"/>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" cssclass="Buttons" OnClick="btnClear_Click"/>
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
                                    <asp:DataGrid ID="grdOfficeType" runat="server" OnPageIndexChanged="grdAdjuster_PageIndexChanged" OnSelectedIndexChanged="grdAdjuster_SelectedIndexChanged" Width="100%" CssClass="GridTable" AutoGenerateColumns="false"  AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                           
                            <ItemStyle CssClass="GridRow"/>
                            <Columns>
                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="SZ_OFFICE_TYPE_ID" HeaderText="Procedure Group ID" Visible="false">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_OFFICE_TYPE" HeaderText="Office Type"></asp:BoundColumn>
                                
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


