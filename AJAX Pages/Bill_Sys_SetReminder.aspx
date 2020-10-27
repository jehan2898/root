<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_SetReminder.aspx.cs" Inherits="Bill_Sys_SetReminder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/Reminder.ascx" TagName="MessageControl" TagPrefix="Reminder" %>

 
        
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

 

      <head id="Head1" runat="server" >
    <title>Untitled Page</title>
    
</head>

<script language="javascript" type="text/javascript">
    

 
        
</script>
                                                                                
    

<body style="background-color:White; " >
    <form id="form1" runat="server" >
      
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
         <Reminder:MessageControl runat="server" id="reminderVisits" />
    </form>
    
</body>
</html>
