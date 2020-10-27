<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_UserVrification.aspx.cs" Inherits="Bill_Sys_UserVrification" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div  >  
        <table width="100%">
            <tr style="height:100px">
                <td >
                </td>
                
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="40%" style="border-collapse:collapse; border-color:Black; border-style:solid; border-width:2px;" >
                        <tr>
                            <td align="center" ><%--style="background-color:#368BDB"--%>
                               <table>
                                    <tr>
                                        <td align="center">
                                            VeriFication
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Red" Text="Enter Valid Email-ID."
                                                Visible="False"></asp:Label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                ControlToValidate="txtEmailAddress" ErrorMessage="Enter valid E-mail ID." 
                                                ValidationGroup="EmailVal" Display="Dynamic" 
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmailAddress"
                                                ErrorMessage="Enter E-Mail Address" EnableTheming="True" 
                                                ValidationGroup="EmailVal" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  align="center" >
                                            <table >
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Email Address:"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox>
                                                        
                                                    </td>
                                                    <td>                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3">
                                                        <asp:Button ID="btnVerify" runat="server" Text="Verify" 
                                                            ValidationGroup="EmailVal" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                               </table>
                            </td>
                        </tr>
                    </table>    
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <div id="divConfirm" ><%--style="visibility:hidden;"--%>
                        <table width="40%" style="border-collapse:collapse; border-color:Black; border-style:solid; border-width:2px;" >
                           <tr>
                            <td align="center" ><%--style="background-color:#368BDB"--%>
                               <table >
                                    <tr>
                                        <td align="center">
                                            Confirmation
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red" Text="Confirmation Code is wrong"
                                                Visible="False"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtConfirmation"
                                                        ErrorMessage="Enter Confirmation Code"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  align="center" >
                                            <table >
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblConfirmationErr" runat="server" Text="Confirmation Code:"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtConfirmation" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3">
                                                        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                               </table>
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
