<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_BillingCompany.aspx.cs"
    Inherits="Bill_Sys_BillingCompany" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Billing System</title>

    <script type="text/javascript" src="validation.js"></script>
    
 <link href="Css/main.css" type="text/css" rel="Stylesheet" />
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmBillingCompany" runat="server">
    <div align="center">
            <table class="maintable">
                <tr>
                    <td class="bannerImg" colspan="3">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/lawallies-logo.jpg"
                            Width="316px" Height="85px" /></td>
                </tr>
                <tr>
                    <td width="100%" height="7%" valign="top" colspan="3">
                        <table width="100%; height:100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="200" height="97px" align="left" valign="top" scope="row">
                                    <table width="200" height="97px" border="0" cellpadding="0" cellspacing="0" background="images/top-bg.jpg">
                                        <tr>
                                            <td align="left" valign="bottom" class="frame-1" scope="row">
                                                <table border="0" cellpadding="0" cellspacing="0" class="UserTable" style="width: 100%">
                                                    <tr>
                                                        <td style="width: 100px">
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left" valign="top">
                                    <table width="100%" height="80" border="0" cellpadding="0" cellspacing="0" background="images/top-bg.jpg">
                                        <tr>
                                            <td align="right" valign="top" scope="row" style="height: 80px">
                                                <table width="485" height="80" border="0" cellpadding="0" cellspacing="0">
                                                    <tr align="left" valign="middle">
                                                        <td align="center" scope="row">
                                                            <a href="#"></a>
                                                        </td>
                                                        <td width="12" class="space" scope="row">
                                                            <img src="images/divider.jpg" width="2" height="80"></td>
                                                        <td width="108" scope="row">
                                                        </td>
                                                        <td width="5" valign="middle">
                                                            <img src="images/sp.gif" width="5" height="1"></td>
                                                        <td width="152">
                                                        </td>
                                                        <td width="16" align="center" class="space">
                                                            <img src="images/divider.jpg" width="2" height="80"></td>
                                                        <td width="142" align="left" valign="bottom">
                                                            <table border="0" cellpadding="0" cellspacing="0" class="UserTable" style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="4" align="right" valign="top" scope="row" style="height: 97px">
                                                <img src="images/top-corn-rght.jpg" width="7" height="80"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="TDPart">
                         <div align="center">
            <table width="100%">
                <tr>
                <tr>
                    <td colspan="6" style="font-weight: bold">
                        Company Details</td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        Company Name</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" id="txtCompanyName" runat="server">
                    </td>
                    <td class="tablecellLabel">
                        Street</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" id="txtCompanyStreet" runat="server">
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        City</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" id="txtCompanyCity" runat="server">
                    </td>
                    <td class="tablecellLabel">
                        State</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" id="txtCompanyState" runat="server">
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        Zip</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" id="txtCompanyZip" runat="server">
                    </td>
                    <td class="tablecellLabel">
                        Phone No</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" id="txtCompanyPhoneNo" runat="Server">
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        Email ID</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" id="txtCompanyEmailID" runat="server">
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="font-weight: bold">
                        Company Contact Details</td>
                </tr>
                <tr>
                    <td colspan="6" style="height: 19px">
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        First Name</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" id="txtContFirstName" runat="Server">
                    </td>
                    <td class="tablecellLabel">
                        Last Name</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" id="txtContLastName" runat="server">
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        Admin Email</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" id="txtContAdminEmailID" runat="server">
                    </td>
                    <td class="tablecellLabel">
                        Office Phone</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" id="txtContOfficePhone" runat="server">
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                        Office Ext.</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" id="txtContOfficeExt" runat="server">
                    </td>
                    <td class="tablecellLabel">
                        Contact Email</td>
                    <td class="tablecellSpace">:
                    </td>
                    <td class="tablecellControl" runat="server" id="txtContEmail">
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel" style="height: 19px">
                        Scheme</td>
                    <td class="tablecellSpace" style="height: 19px">:
                    </td>
                    <td class="tablecellControl" id="txtContScheme" runat="server" style="height: 19px">
                    </td>
                    <td style="height: 19px">
                    </td>
                    <td style="height: 19px">
                    </td>
                    <td style="height: 19px">
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="font-weight: bold">
                        Set Prefix Charactor</td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                 <tr>
                    <td class="tablecellLabel" style="height: 26px">
                        Prefix Charactor</td>
                    <td class="tablecellSpace" style="height: 26px">:
                    </td>
                    <td class="tablecellControl" style="height: 26px" >
                        <asp:TextBox ID="txtPrefixChar" runat="server" MaxLength="3"></asp:TextBox></td>
                    <td class="tablecellLabel" style="height: 26px">
<%--                        <asp:TextBox ID="txtxtBillCompanyID" runat="server" Visible="false"></asp:TextBox>--%>
                       </td>
                    <td class="tablecellSpace" style="height: 26px">
                    </td>
                    <td class="tablecellControl" style="height: 26px">
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel" style="height: 26px">
                       </td>
                    <td class="tablecellSpace" style="height: 26px">
                    </td>
                    <td style="height: 26px" align="right">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="Buttons"/>
                    </td>
                    <td class="tablecellLabel" style="height: 26px">
                       </td>
                    <td class="tablecellSpace" style="height: 26px">
                    </td>
                    <td class="tablecellControl" style="height: 26px">
                    </td>
                </tr>
            </table>
        </div>
                    </td>
                </tr>
            </table>
        </div>
    
    
       
    </form>
</body>
</html>
