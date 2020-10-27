<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_EditRProcPopupPage.aspx.cs"
    Inherits="Bill_Sys_EditRProcPopupPage" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
    <%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">

   </style>
    <title>Untitled Page</title>

    <script type="text/javascript" src="Registration/validation.js"></script>

    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script type="text/javascript">

   function confirm_update_bill_status()
         {     
               
               
         
                var f= document.getElementById('grdProCode');
		        var bfFlag = false;	
		        var cnt=0;
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('chkSelectProc') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {		
			            				
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {  bfFlag = true;   
			                    cnt=cnt+1;
    		                    
		                    }
			                    
			                }
			            }
			        }   			
		        
		        if(bfFlag == false)
		        {
		            alert('Please select record.');
		            return false;
		        }
		        else
		        {
		            if(cnt>1)
		            {
		                 alert('Please select only one record.');
		                return false;
		            }else
		            {
		                return true;
		             }
		        }
         }
         
         function confirm_update_bill_status_LHR()
         {     
               
               
         
                var e=document.getElementById('extddlDoctor');
                var strUser = e.options[e.selectedIndex].value;
                //alert(strUser); 
                if(strUser=='NA')
                {
                    alert('Please select doctor.');
                    return false;
                }             
                var f= document.getElementById('grdProCode');
		        var bfFlag = false;	
		        var cnt=0;
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('chkSelectProc') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {		
			            				
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {  bfFlag = true;   
			                    cnt=cnt+1;
    		                    
		                    }
			                    
			                }
			            }
			        }   			
		        
		        if(bfFlag == false)
		        {
		            alert('Please select record.');
		            return false;
		        }
		        else
		        {
		            if(cnt>1)
		            {
		                 alert('Please select only one record.');
		                return false;
		            }else
		            {
		                return true;
		             }
		        }
         }

    </script>

    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <UserMessage:MessageControl runat="server" ID="usrMessage" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="overflow: scroll; height: 250px; width: 100%;">
                            <asp:DataGrid Width="100%" ID="grdProCode" runat="server" CssClass="GridTable" AutoGenerateColumns="False">
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:BoundColumn DataField="CODE" HeaderText="Procedure CodeID" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIPTION" HeaderText="Procedure Code"></asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectProc" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="GridHeader" />
                                <PagerStyle Mode="NumericPages" />
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    
                    <td style="font-weight:600;" valign="top" colspan="2">
                        <asp:Label ID="lblDoctor" runat="server" Text="Doctor:" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td class="td-widget-bc-search-desc-ch3" valign="top" colspan="2">
                        <cc1:extendeddropdownlist id="extddlDoctor" runat="server" width="240px" connection_key="Connection_String"
                            flag_key_value="GETDOCTORLIST" procedure_name="SP_MST_DOCTOR" selected_text="---Select---" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnUPdate" runat="server" Text="Update" Width="104px" CssClass="Buttons"
                            OnClick="btnUPdate_Click" Visible="false" />
                         <asp:Button ID="btnUpdateNew" runat="server" Text="Update" Width="104px" CssClass="Buttons" Visible="false" OnClick="btnUpdateNew_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
