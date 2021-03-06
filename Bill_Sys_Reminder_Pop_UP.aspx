<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Reminder_Pop_UP.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_Reminder_Pop_UP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reminder</title>

    <script language="Javascript" type="text/javascript">
    function chkall(ival)	
	{	
	    var f= document.getElementById("grdReminder");	
		for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		{		
		    if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			{						
			    f.getElementsByTagName("input").item(i).checked=ival;
			    frmReminder.btnDelete.disabled=!ival;						
			}			
		}		
	} 
	
	function chk()	
	{	
	    var f= document.getElementById("grdReminder");	
		for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		{		
		    if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			{						
			    if(f.getElementsByTagName("input").item(i).checked==true)
			    {			    
			        document.getElementById('btnDismiss').disabled = false; 
			        return;
			    }	
			    else
			    {			        
			        document.getElementById('btnDismiss').disabled = true; 
			    }		
			}			
		}
		
	} 
	
	function chkCase()	
	{	
	    var f= document.getElementById("grdreminder_Case");	
		for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		{		
		    if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			{						
			    if(f.getElementsByTagName("input").item(i).checked==true)
			    {			    
			        document.getElementById('btnDismissCase').disabled = false; 
			        return;
			    }	
			    else
			    {			        
			        document.getElementById('btnDismissCase').disabled = true; 
			    }		
			}			
		}
		
	} 
	
	function ValidateText()
	{
	    //alert(txtDisReason);
	    if(document.getElementById('txtDismissReason').value=="")
	    {
	        alert("Please give dismiss reason!!");
	        return false;
	    }
	}
	
	function ValidateCaseText()
	{
	    if(document.getElementById('txtDismissCaseReason').value=="")
	    {
	        alert("Please give Dismiss Reason!!");
	        return false;
	    }
	}
	
	function ConfirmDisMis()
	{
	
	    var Val= confirm('Dismiss will stop the alert from appearing again. Do you want to remove it?..?');
	    if(Val==true)
	    {
	     return true;
	    }
	    else
	    {
	        return false;
	    }
	}
	
	    function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdReminder.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
       }
       
       function SelectAll1(ival)
       {
            var f= document.getElementById("<%= grdreminder_Case.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
       }

    </script>

    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmReminder" runat="server">
        <div align="center">
            <table width="100%">
                <tr>
                    <td align="center" style="font-size: 16px;" valign="middle" class="form-head" height="40">
                        <strong>Reminder</strong></td>
                </tr>
                <tr>
                    <td class="TDPart">
                        <div id="divReminder" runat="server">
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <table width="100%">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblDoctorReminder" runat="server" Text="Doctor's Reminder :" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    <asp:DataGrid ID="grdReminder" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                        CellPadding="4" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" Width="100%">
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <EditItemStyle BackColor="#2461BF" />
                                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <AlternatingItemStyle BackColor="White" />
                                                        <ItemStyle BackColor="#EFF3FB" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="i_recurrence_id" HeaderText="i_recurrence_id" Visible="False">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="I_REMINDER_ID" HeaderText="Reminder ID" Visible="False">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="sz_patient_name" HeaderText="Patient Name" Visible="False">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DT_REMINDER_DATE" HeaderText="Due Date" DataFormatString="{0:dd MMM yyyy}">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_REMINDER_DESC" HeaderText="Task"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_REMINDER_ASSIGN_TO" HeaderText="Assign To ID" Visible="False">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText=" Assigned to"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_REMINDER_ASSIGN_BY" HeaderText="REMINDER_ASSIGN_BY"
                                                                Visible="False"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_REMINDER_STATUS_ID" HeaderText="REMINDER_STATUS_ID"
                                                                Visible="False"></asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="Dismiss">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" ToolTip="Select All" onclick="javascript:SelectAll(this.checked);chk();" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="chk()" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#B5DF82" Font-Bold="True" ForeColor="White" />
                                                    </asp:DataGrid>
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp;<asp:Label ID="lblREsult" runat="server" Font-Bold="True" ForeColor="Red" Text="No Record Exists..!!"
                                                        Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="height: 30px" valign="top">
                                                    <asp:Label ID="lblDisReason" runat="server" Text="Dismiss Reason:" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtDismissReason" runat="server" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                    <asp:Button ID="btnDismiss" runat="server" Text="Dismiss" Enabled="false" OnClick="btnDismiss_Click"
                                                        Visible="false" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="SectionDevider">
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divCase_reminder" runat="server">
                            <table width="100%">
                                <tr>
                                    <td class="TDPart">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblCaseReminder" runat="server" Text="Case Reminder :" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                        <td align="left" valign="top">
                                            <asp:DataGrid ID="grdReminder" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                CellPadding="4" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" Width="100%">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditItemStyle BackColor="#2461BF" />
                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <ItemStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="i_recurrence_id" HeaderText="i_recurrence_id" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="I_REMINDER_ID" HeaderText="Reminder ID" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DT_REMINDER_DATE" HeaderText="Due Date" DataFormatString="{0:dd MMM yyyy}">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_REMINDER_DESC" HeaderText="Task"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_REMINDER_ASSIGN_TO" HeaderText="Assign To ID" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText=" Assigned to"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_REMINDER_ASSIGN_BY" HeaderText="REMINDER_ASSIGN_BY"
                                                        Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_REMINDER_STATUS_ID" HeaderText="REMINDER_STATUS_ID"
                                                        Visible="False"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Dismiss">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="chk()" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <HeaderStyle BackColor="#B5DF82" Font-Bold="True" ForeColor="White" />
                                            </asp:DataGrid>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp;<asp:Label ID="lblREsult" runat="server" Font-Bold="True" ForeColor="Red" Text="No Record Exists..!!"
                                                Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="height: 30px" valign="top">
                                            <asp:Label ID="lblDisReason" runat="server" Text="Dismiss Reason:" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtDismissReason" runat="server" TextMode="MultiLine" Visible="false"></asp:TextBox> 
                                            <asp:Button ID="btnDismiss" runat="server"  Text="Dismiss" Enabled="false" OnClick="btnDismiss_Click" Visible="false" /></td>
                                    </tr>--%>
                                            <tr>
                                                <td align="left" valign="top">
                                                    <asp:DataGrid ID="grdreminder_Case" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                        CellPadding="4" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" Width="100%">
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <EditItemStyle BackColor="#2461BF" />
                                                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <AlternatingItemStyle BackColor="White" />
                                                        <ItemStyle BackColor="#EFF3FB" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="i_recurrence_id" HeaderText="i_recurrence_id" Visible="False">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="I_REMINDER_ID" HeaderText="Reminder ID" Visible="False">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="sz_patient_name" HeaderText="Patient Name"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DT_REMINDER_DATE" HeaderText="Due Date" DataFormatString="{0:dd MMM yyyy}">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_REMINDER_DESC" HeaderText="Task"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_REMINDER_ASSIGN_TO" HeaderText="Assign To ID" Visible="False">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText=" Assigned to"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_REMINDER_ASSIGN_BY" HeaderText="REMINDER_ASSIGN_BY"
                                                                Visible="False"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_REMINDER_STATUS_ID" HeaderText="REMINDER_STATUS_ID"
                                                                Visible="False"></asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="Dismiss">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelectAll1" runat="server" ToolTip="Select All" onclick="javascript:SelectAll1(this.checked);chkCase();" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="chkCase()" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#B5DF82" Font-Bold="True" ForeColor="White" />
                                                    </asp:DataGrid>
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp;<asp:Label ID="lblCaseResult" runat="server" Font-Bold="True" ForeColor="Red"
                                                        Text="No Record Exists..!!" Visible="False"></asp:Label></td>
                                            </tr>
                                            <tr valign="top">
                                                <td align="center" valign="top">
                                                    <asp:Label ID="lblDisReasonCase" runat="server" Text="Dismiss Reason:" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtDismissCaseReason" runat="server" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                                    <asp:Button ID="btnDismissCase" runat="server" Text="Dismiss" Enabled="false" OnClick="btnDismissCase_Click"
                                                        Visible="false" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="SectionDevider">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
