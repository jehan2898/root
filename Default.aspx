<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" > 
              </asp:ScriptManager>
          <asp:ImageButton runat="Server" CausesValidation="false"  ID="ImageButton1" ImageUrl="~/Images/Icon1.jpg" AlternateText="Click here to display calendar" />
        <asp:TextBox ID="TextBox1"  runat="server"></asp:TextBox>
      <asp:RangeValidator ID="RangeValidator1" runat="server"  MinimumValue="01/01/1901" MaximumValue="12/01/2050" ControlToValidate="txtPurchaseddate" ErrorMessage="Enter valid date" Type="Date"></asp:RangeValidator>
        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
        TargetControlID="TextBox1" PopupButtonID="ImageButton1"/>
        
  
    </div>
        <asp:Calendar ID="Calendar1" runat="server" Height="255px" Width="542px"></asp:Calendar>
    </form>
</body>
</html>
