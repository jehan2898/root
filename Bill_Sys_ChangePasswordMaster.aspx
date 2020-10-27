<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_ChangePasswordMaster.aspx.cs" Inherits="Bill_Sys_ChangePasswordMaster" %>

<%@ Register  Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

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
                                   User Name</td>                             
                              <td style="width: 35%">
                                   <asp:TextBox ID="txtUserName" runat="server" MaxLength="50" Width="151"></asp:TextBox></td>
                               <td class="ContentLabel" style="width: 15%">
                                   Old Password</td>
                             
                              <td style="width: 35%">
                                   <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" Width="151" MaxLength="10"></asp:TextBox></td>
                           </tr>
                           <tr></tr>
                           <tr>
                               <td class="ContentLabel" style="width: 15%">New Password</td>
                                <td style="width: 35%">
                                   <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Width="151" MaxLength="10"></asp:TextBox></td>
                             <td class="ContentLabel" style="width: 15%">Confirm Password</td>
                              <td style="width: 35%">
                                   <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="151" MaxLength="10"></asp:TextBox>
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
                                <asp:Button ID="btnSave" runat="server" Text="Update" Width="80px" OnClick="btnSave_Click"
                                    CssClass="Buttons" />

                                <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click"
                                CssClass="Buttons" />
                            </td>
                            </tr>
                      </table>
                                    
                                    </td>
                                </tr>
                                

                            </table>
                    <tr>
                    
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                        </tr>
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
