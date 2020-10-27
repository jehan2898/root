<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Registration.aspx.cs"
    Inherits="Bill_Sys_Registration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Billing System Registration</title>

    <script type="text/javascript" src="validation.js"></script>

    <link href="../Css/main.css" type="text/css" rel="Stylesheet" />
    <link href="../Css/mainmaster.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
			
			
			function ascii_value(c){
     c = c . charAt (0);
     var i;
     for (i = 0; i < 256; ++ i)
     {
      var h = i . toString (16);
      if (h . length == 1)
       h = "0" + h;
      h = "%" + h;
      h = unescape (h);
      if (h == c)
       break;
     }
     return i;
    }

 function clickButton1(e,charis)
{
        var keychar;
        if(navigator.appName.indexOf("Netscape")>(-1))
        {    
            var key = ascii_value(charis);
            if(e.charCode == key || e.charCode==0){
            return true;
           }else{
                 if (e.charCode < 48 || e.charCode > 57)
                 {             
                        return false;
                 } 
             }
         }
    if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
    {          
        var key=""
       if(charis!="")
       {         
             key = ascii_value(charis);
        }
        if(event.keyCode == key)
        {
            return true;
        }
        else
        {
                 if (event.keyCode < 48 || event.keyCode > 57)
                  {             
                     return false;
                  }
        }
    }
    
    
 }
 

 
    </script>

    <script type="text/javascript">
    function ValidateText()
    {
        var txtId;
        txtId = document.getElementById('txtCompanyName');
        if(txtId.value.length <= 4)
        {
            txtId.value=''; 
            document.getElementById('lblCompanyNameValid').style.visibility='visible'; 
            document.getElementById('lblCompanyNameValid').innerText ='Company name should greater than 4 characters';           
        } 
        else
        {
            document.getElementById('lblCompanyNameValid').style.visibility='hidden';
        }       
    }
    
     function MakeChange()
    {
    
        txtId = document.getElementById('extddlScheme').value;
    
        if(txtId == 'PS00001')
        {

        document.getElementById('lblnoOfUser').style.visibility='visible';
        document.getElementById('lblnoOfUser').innerText ='No of users'; 
//              alert('hi1');
      document.getElementById('txtnoOfUser').style.visibility='visible'; 
//          
          document.getElementById('lblDescription').style.visibility='visible'; 
  
           document.getElementById('lblDescription').innerText ='Charges is as per no of users';           

             document.getElementById('txtnoOfUser').value=''; 
        } 
        else  if(txtId == 'PS00002')
        {
            document.getElementById('lblnoOfUser').style.visibility='hidden'; 
              document.getElementById('lblnoOfUser').innerText =''; 
            document.getElementById('txtnoOfUser').style.visibility='hidden'; 
            document.getElementById('lblDescription').style.visibility='visible'; 
            document.getElementById('lblDescription').innerText ='Charges is as per billing company';           
            document.getElementById('txtnoOfUser').value='50'; 
        
        }
        else
        {
            document.getElementById('lblnoOfUser').style.visibility='hidden'; 
              document.getElementById('lblnoOfUser').innerText =''; 
            document.getElementById('txtnoOfUser').style.visibility='hidden'; 
            document.getElementById('lblDescription').style.visibility='hidden'; 
            document.getElementById('lblDescription').innerText ='';           
            document.getElementById('txtnoOfUser').value=''; 
           
        }       
    }
    
    </script>

</head>
<body>
    <form id="frmBill_Sys_Registration" runat="server">
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
                                    <table width="200" height="97px" border="0" cellpadding="0" cellspacing="0" background="../images/top-bg.jpg">
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
                                    <table width="100%" height="80" border="0" cellpadding="0" cellspacing="0" background="../images/top-bg.jpg">
                                        <tr>
                                            <td align="right" valign="top" scope="row" style="height: 80px">
                                                <table width="485" height="80" border="0" cellpadding="0" cellspacing="0">
                                                    <tr align="left" valign="middle">
                                                        <td align="center" scope="row">
                                                            <a href="#"></a>
                                                        </td>
                                                        <td width="12" class="space" scope="row">
                                                            <img src="../images/divider.jpg" width="2" height="80"></td>
                                                        <td width="108" scope="row">
                                                        </td>
                                                        <td width="5" valign="middle">
                                                            <img src="../images/sp.gif" width="5" height="1"></td>
                                                        <td width="152">
                                                        </td>
                                                        <td width="16" align="center" class="space">
                                                            <img src="../images/divider.jpg" width="2" height="80"></td>
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
                                                <img src="../images/top-corn-rght.jpg" width="7" height="80"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="TDPart">
                        <table>
                            <tr>
                                <td align="center" colspan="3" style="font-weight: bold" >
                                    <asp:LinkButton ID="lnkbtnRegBillComp" runat="server" OnClick="lnkbtnRegBillComp_Click"
                                        ToolTip="Register Bill Company">
                                        <img id="imgRegisterBillCompany" src="../Images/bfirm.gif" runat="server"  /></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkbtnRegReferringFacility" runat="server" OnClick="lnkbtnRegReferringFacility_Click"
                                        ToolTip="Register Referring Facility">
                                        <img id="imgReferringFacility" src="../Images/tfirm.gif" style="border-style: none;"
                                            runat="server" /></asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="lnkbtnLawFirm" runat="server" OnClick="lnkbtnLawFirm_Click" ToolTip="Register Law Firm">
                                        <img id="imgLawFirm" src="../Images/lfirm.gif" style="border-style: none;" runat="server"
                                           /></asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="font-weight: bold" align="left">
                                    <asp:Label ID="lblHeading" runat="server" Text="Billing Company Details"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="3" height="30px" style="text-align: right;">
                                    <span style="color: red">* - Mandatory Fields </span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" height="30px">
                                    <div id="ErrorDiv" visible="true" style="color: Red;">
                                    </div>
                                </td>
                            </tr>
                              <tr>
                                <td colspan="3" height="30px" align='center' >
                                    <asp:Label ID="lblError" runat="server"  Text="Thank you registering.<br/>Click <a href='../Bill_Sys_Login.aspx'> here </a> to go back to the login page to start using the website." style="color:Blue;" visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table width="100%">
                                         <tr>
                                            <td width="25%" align="left">
                                                User Name</td>
                                            <td width="24%">
                                                <asp:TextBox ID="txtUserName" runat="server" Width="250px" MaxLength="50"> </asp:TextBox>
                                                  
                                            </td>
                                            <td width="1%">
                                                <span style="color: red">*</span>
                                            </td>
                                            <td width="25%" align="left">
                                                </td>
                                            <td style="width: 24%">
                                                
                                                <td style="width: 24%">
                                            </td>
                                        </tr>
                                         <tr>
                                            <td width="25%" align="left">
                                                Password</td>
                                            <td width="24%">
                                                <asp:TextBox ID="txtNewPassword" runat="server" Width="250px" MaxLength="50" TextMode="Password"> </asp:TextBox>
                                                  
                                            </td>
                                            <td width="1%">
                                                <span style="color: red">*</span>
                                            </td>
                                            <td width="25%" align="left">
                                               Confirm Password : 
                                            </td>
                                            <td style="width: 24%">
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" Width="250px" MaxLength="50" TextMode="Password"> </asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password and Confirm Password Must be Same" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
                                            </td>
                                             <td width="1%">
                                                <span style="color: red">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="left">
                                                Company Name</td>
                                            <td width="24%">
                                                <asp:TextBox ID="txtCompanyName" runat="server" Width="250px" MaxLength="50" onfocusout="javascript:ValidateText();"> </asp:TextBox>
                                                
                                                <asp:Label ID="lblCompanyNameValid" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                            <td width="1%">
                                                <span style="color: red">*</span>
                                            </td>
                                            <td width="25%" align="left">
                                                Street</td>
                                            <td style="width: 24%">
                                                <asp:TextBox ID="txtCompanyStreet" runat="server" Width="250px" MaxLength="100"></asp:TextBox></td>
                                                <td style="width: 24%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="left">
                                                City</td>
                                            <td width="24%">
                                                <asp:TextBox ID="txtCompanyCity" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                                <td width="1%">
                                            </td>
                                            <td width="25%" align="left">
                                                State</td>
                                            <td style="width: 24%">
                                                <asp:TextBox ID="txtCompanyState" runat="server" Width="250px" MaxLength="20"></asp:TextBox></td>
                                                <td style="width: 24%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="left">
                                                Zip</td>
                                            <td width="24%">
                                                <asp:TextBox ID="txtCompanyZip" runat="server" Width="250px" MaxLength="10"></asp:TextBox></td>
                                                <td width="1%">
                                            </td>
                                            <td width="25%" align="left">
                                                Phone No</td>
                                            <td style="width: 24%">
                                                <asp:TextBox ID="txtCompanyPhoneNo" runat="server" Width="250px" MaxLength="20"></asp:TextBox></td>
                                                <td style="width: 24%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="24%" align="left">
                                                Email ID</td>
                                            <td width="24%">
                                                <asp:TextBox ID="txtCompanyEmailID" runat="server" Width="250px" MaxLength="50"></asp:TextBox><br />
                                                <span style="color: #ff0000">&nbsp;</span><asp:RegularExpressionValidator ID="revCompanyEmailID" runat="server" ControlToValidate="txtCompanyEmailID"
                                                    ErrorMessage="test@domain.com" ToolTip="*Require" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    SetFocusOnError="True" EnableClientScript="False"></asp:RegularExpressionValidator></td>
                                            <td width="1%">
                                            </td>
                                                    
                                            <td width="25%">
                                            </td>
                                            <td style="width: 24%">
                                            </td>
                                            <td align="left" style="width: 24%">
                                            
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                            </td>
                                            <td width="25%" colspan="2">
                                            </td>
                                            <td width="25%">
                                            </td>
                                            <td width="25%" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" align="left" style="font-weight: bold">
                                                Company Contact Details</td>
                                           
                                        </tr>
                                        <tr>
                                            <td width="100%" colspan="6">
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                            <td width="25%" style="height: 19px" align="left">
                                                First Name</td>
                                            <td width="24%" style="height: 19px">
                                                <asp:TextBox ID="txtContFirstName" runat="server" Width="250px" MaxLength="100"></asp:TextBox></td>
                                             <td width="1%">
                                                <span style="color: red">*</span>
                                            </td>   
                                            <td width="25%" style="height: 19px" align="left">
                                                Last Name</td>
                                            <td style="height: 19px; width: 24%;">
                                                <asp:TextBox ID="txtContLastName" runat="server" Width="250px" MaxLength="100"></asp:TextBox></td>
                                                <td style="width: 24%">
                                                <span style="color: red">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="left">
                                                Admin Email</td>
                                            <td width="24%">
                                                <asp:TextBox ID="txtContAdminEmailID" runat="server" Width="250px" MaxLength="50"></asp:TextBox><br />
                                                <span style="color: #ff0000">&nbsp;</span><asp:RegularExpressionValidator ID="revContAdminEmailID" runat="server" ControlToValidate="txtContAdminEmailID"
                                                    EnableClientScript="False" ErrorMessage="test@domain.com" ToolTip="*Require"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="True"></asp:RegularExpressionValidator></td>
                                                    <td width="1%">
                                                <span style="color: red">*</span>
                                            </td>
                                            <td width="25%" align="left">
                                                Office Phone</td>
                                            <td style="width: 24%">
                                                <asp:TextBox ID="txtContOfficePhone" runat="server" Width="250px" MaxLength="20"></asp:TextBox></td>
                                                <td style="width: 24%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="left" style="height: 43px">
                                                Office Ext.</td>
                                            <td width="24%" style="height: 43px">
                                                <asp:TextBox ID="txtContOfficeExt" runat="server" Width="250px" MaxLength="5"></asp:TextBox></td>
                                                <td width="1%">
                                            </td>
                                            <td width="25%" align="left" style="height: 43px">
                                                Contact Email</td>
                                            <td style="height: 43px; width: 24%;">
                                                <asp:TextBox ID="txtContEmail" runat="server" Width="250px" MaxLength="50"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="revContEmailID" runat="server" ControlToValidate="txtContEmail"
                                                    EnableClientScript="False" ErrorMessage="test@domain.com" ToolTip="*Require"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="True"></asp:RegularExpressionValidator></td>
                                                    <td style="width: 24%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="left" style="height: 46px">
                                                <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label>
                                            </td>
                                            <td width="25%" colspan="2" style="height: 46px" align="left">
                                                <asp:DropDownList ID="extddlScheme" runat="server" onChange="MakeChange();" Width="250px">
                                                    <asp:ListItem Selected="True" Value="NA">---SELECT---</asp:ListItem>
                                                     <asp:ListItem  Value="PS00001">Users</asp:ListItem>
                                                      <asp:ListItem  Value="PS00002">Transaction</asp:ListItem>
                                                </asp:DropDownList>
                                           

                                               <%-- <extddl:ExtendedDropDownList ID="extddlScheme" runat="server" Width="250px" Connection_Key="ConnectionString"
                                                    Flag_Key_Value="GET_SCHEME_LIST" Procedure_Name="SP_MST_PAYMENT_SCHEME" Selected_Text="---SELECT---"
                                                     Maintain_State="True" OnextendDropDown_SelectedIndexChanged="extddlScheme_extendDropDown_SelectedIndexChanged" />--%>
                                            </td>
                                            <td width="25%" style="height: 46px">
                                               
                                                     <asp:Label ID="lblnoOfUser" runat="server"  Text=""></asp:Label></td>
                                            <td width="25%" colspan="2" style="height: 46px">
                                                <asp:TextBox ID="txtnoOfUser" runat="server" Width="250px" MaxLength="10" style="visibility:hidden;"
                                                    onkeypress="return clickButton1(event,'/')"></asp:TextBox>
                                                <br />
                                                <asp:Label ID="lblDescription" runat="server" Text="" ></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                            </td>
                                            <td width="25%" colspan="2">
                                            </td>
                                            <td width="25%">
                                                <asp:TextBox ID="txtNewGUID" runat="server" Visible="False" Width="62px"></asp:TextBox></td>
                                            <td width="25%" colspan="2">
                                                <asp:TextBox ID="txtUserId" runat="server" Visible="False" Width="62px"></asp:TextBox>
                                                <asp:TextBox ID="txtPassword" runat="server" Visible="False" Width="62px"></asp:TextBox>
                                                <asp:TextBox ID="txtEncryptedPassword" runat="server" Visible="False" Width="62px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td width="25%">
                                            </td>
                                            <td width="25%" align="right" colspan="2">
                                                <asp:TextBox ID="txtReferringFacility" runat="server" Visible="false" Text="0" Width="10px"></asp:TextBox>
                                                <asp:TextBox ID="txtLawFirm" runat="server" Visible="false" Text="0" Width="10px"></asp:TextBox>
                                                <asp:Button ID="btnRegister" runat="server" Text="Register" Width="80px" OnClick="btnRegister_Click"
                                                    CssClass="Buttons" />
                                                &nbsp;&nbsp;</td>
                                            <td width="25%">
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click"
                                                    CssClass="Buttons" /></td>
                                            <td width="25%" colspan="2">
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
    </form>
</body>
</html>
