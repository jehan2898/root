<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_sys_Ticket.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css"rel="stylesheet" type="text/css" />
 <script language="javascript">
    function DisableLabel()
    {
        document.getElementById('lblMsg').style.visibility='hidden'; 
    }
 
    </script>
    <style type="text/css">
        .textbox{
            font-family: Arial;
            font-color: #808080;
            font-size: 12px;
            border: 1px solid;
        }
        
        .label{
            font-family: Arial;
            font-color: #808080;
            font-size: 12px;
        }        
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div align ="center">
       
        <table id="tblTicket" runat="server" style="width: 431px; height: 366px;vertical-align:top;" class="TDPart">
             
            <tr style="height:20px;">
            
            <td style="width:80px; height: 12px;">
            
            </td>
           <%-- <td style="width: 400px; height: 12px;">--%>
          <%-- <table>--%>
          <%-- <tr>--%>
           <td style="height: 12px">
                <asp:Label ID="lblMsg" runat="server" Height="12px" Width="242px" ForeColor="Blue" CssClass="label"></asp:Label>
           
           </td>
           
          <%-- <td style="height: 12px; width:330px;" align="left";>--%>
           
                <%--<asp:Label ID="Label1" runat="server" Height="12px" Width="175px" ForeColor="Red">*Fields are mandatory</asp:Label>--%>
           
           <%--</td>--%>
           <%--</tr>--%>
          <%-- </table>--%>
                          
           <%-- </td> --%>
            
            </tr>
            
            <tr>
                <td style="width: 138px; height: 25px;">
                    <asp:Label ID="lblTicketNo" runat="server" Text="Ticket #" Visible="false" CssClass="label"></asp:Label>
                </td>
                <td style="width: 428px; height: 25px;">
                    <asp:TextBox ID="txtTicketNo" runat="server" CssClass="textbox"  Visible="false" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
        <tr>       
            <td style="width: 138px; height: 23px;">
                <asp:Label ID="lblName" runat="server" Text="Name" Width="40px" CssClass="label"></asp:Label>
            </td>
        
            <td style="width: 428px; height: 23px;">
                <asp:TextBox ID="txtName" onfocusout = "javascript:DisableLabel();" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NameValidator" runat="server" ControlToValidate="txtName" ErrorMessage="Enter Name">*</asp:RequiredFieldValidator>
                </td>
        </tr>        
        <tr>
            <td style="width: 138px; height: 23px;">
                <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="label"></asp:Label>
            </td>
            <td style="width: 428px; height: 23px;">
                <asp:TextBox ID="txtEmail" onfocusout = "javascript:DisableLabel();" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailValidator" runat="server" ControlToValidate="txtEmail" ErrorMessage="Enter Email">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Please enter valid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td style="width: 138px; height: 25px;">
                <asp:Label ID="lblSupport" runat="server" Text="Support" CssClass="label"></asp:Label>
            </td>
            
            <td style="width: 428px; height: 25px;">
                <asp:DropDownList ID="ddlSupport" onfocusout = "javascript:DisableLabel();" runat="server" CssClass="textbox">
                    <asp:ListItem>&lt;--- Select ---&gt;</asp:ListItem>
                    <asp:ListItem Value="1">Technical Support</asp:ListItem>
                    <asp:ListItem Value="2">Application query</asp:ListItem>
                    <asp:ListItem Value="3">Feature request</asp:ListItem>
                    <asp:ListItem Value="4">Raise a bug</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="SupportValidator" runat="server" ControlToValidate="ddlSupport"
                    ErrorMessage="Select Support" InitialValue="<--- Select --->" Width="1px">*</asp:RequiredFieldValidator></td>
        </tr>
         <tr>
         <td style="width: 138px; height: 59px;">
                
                 <asp:Label ID="lblUpload" runat="server" Text="Upload File" CssClass="label"></asp:Label>
            </td>
              <td style="width: 428px; height: 59px">
               
                <asp:FileUpload ID="FileUploadControl"  runat="server" Height="27px" Width="267px"></asp:FileUpload><asp:Button ID="btnupload" runat="server" Height="25px" OnClick="btnupload_Click" Text="Upload" Width="60px" CausesValidation="False" CssClass="Buttons" />&nbsp;<br />
                  <asp:Label ID="lblFileName" runat="server" CssClass="label" ForeColor="Blue" Height="12px"
                      Width="164px"></asp:Label>
                  <asp:TextBox ID="txtupload" runat="server" CssClass="textbox" onfocusout="javascript:DisableLabel();"
                      Visible="False" Width="5px"></asp:TextBox></td>
        </tr>
        
        <tr>
        <td style="width: 138px; height: 84px;">
            <asp:Label ID="lblComments" runat="server" Text="Comments" CssClass="label"></asp:Label>
        </td>
        
        <td style="width: 428px; height: 84px;">
            <asp:TextBox ID="txtAreaComments" onfocusout = "javascript:DisableLabel();" runat="server" MaxLength="4000" TextMode="MultiLine" CssClass="textbox" Width="326px" Height="116px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAreaComments">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 138px; height: 42px;">
                &nbsp;
            </td>
            <td style="width: 428px; height: 42px">
                <asp:Button ID="btnUpdate" runat="server" Text="Submit" Width="80px" OnClick="btnUpdate_Click1" CssClass="Buttons" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CssClass="Buttons" OnClick="btnCancel_Click" CausesValidation="False" /></td>
           
        </tr>
       
        </table>
   
        </div>
    </form>        

</body>
</html>