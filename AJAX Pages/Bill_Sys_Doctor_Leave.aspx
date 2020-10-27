<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Doctor_Leave.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Doctor_Leave"
    Title="Untitled Page" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
    function Clear()
    {
       // alert("call");
        document.getElementById("<%=txtDate.ClientID%>").value='';
        document.getElementById("<%=txtReason.ClientID%>").value='';
        var extvisible = document.getElementById("<%=ddlDoctor.ClientID %>");
         var extvisible1 = document.getElementById("<%=ddlReferringDoctor.ClientID %>");
        if (extvisible != null)
        {
            //document.getElementById("ctl00_ContentPlaceHolder1_extddlLocation").value = 'NA';
            document.getElementById("<%=ddlDoctor.ClientID %>").value = 'NA';
        }
        
         if (extvisible1 != null)
        {
            //document.getElementById("ctl00_ContentPlaceHolder1_extddlLocation").value = 'NA';
            document.getElementById("<%=ddlReferringDoctor.ClientID %>").value = 'NA';
        }
    }
    
    function CheckVal()
    { 
        var extvisible = document.getElementById("<%=ddlDoctor.ClientID %>");
        var extvisible1 = document.getElementById("<%=ddlReferringDoctor.ClientID %>");
        if (extvisible != null)
        {
            //document.getElementById("ctl00_ContentPlaceHolder1_extddlLocation").value = 'NA';
           if( document.getElementById("<%=ddlDoctor.ClientID %>").value =='NA')
           {
                alert('Please Select The Doctor');
                return false;
           }
           else if(document.getElementById("<%=txtDate.ClientID%>").value=='')
           {
              alert('Please Select The Date');
              return false;
           }else
           {
                return true;
           }
         }
        if(extvisible1!=null)
        {
            if( document.getElementById("<%=ddlReferringDoctor.ClientID %>").value =='NA')
           {
                alert('Please Select The Doctor');
                return false;
           }else if(document.getElementById("<%=txtDate.ClientID%>").value=='')
           {
              alert('Please Select The Date');
              return false;
           }else
           {
            return true;
           }
        }
   }
    
         function SelectAll(ival)
       {
       //alert(ival);
            var f= document.getElementById("<%= grdDoctorleaves.ClientID %>");	
            var str = 1;
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {	
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
		    {		
		        
		        
		        
		        if(f.getElementsByTagName("input").item(i).disabled==false)
		        {
                    f.getElementsByTagName("input").item(i).checked=ival;
                }

//			                    str=str+1;	
//			        
//			                     if (str < 10)
//		                        {
//		                            var statusnameid1 = document.getElementById("ctl00_ContentPlaceHolder1_grdBillSearch_ctl0"+str+"_lblStatus");
//		                           
//		                           alert(statusnameid1.innerHTML);
//		                              statusname  = statusnameid1.innerHTML;
//		                            
//		                              
//		                                    if(statusname.toLowerCase() != "transferred")
//		                                    {  alert(str); 
//		                                         f.getElementsByTagName("input").item(i).checked=ival; 
//        		                                
//		                                    }
//		                           }else
//		                            {
//		                                var statusnameid2 = document.getElementById("ctl00_ContentPlaceHolder1_grdBillSearch_ctl"+str+"_lblStatus");
//		                                    statusname  = statusnameid2.innerHTML;
//		                                      alert(statusname);
//		                                    if (statusname.toLowerCase() != "transferred")
//		                                    {  
//		                                         f.getElementsByTagName("input").item(i).checked=ival;
//		                                    }
//			                        }        
//			                 				
			       
			    }	
			    
			    	
		    }
       }
</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td>
                <table width="40%" border="1" cellpadding="0" cellspacing="0" background-color: White">
                
                    <tr>
                                                            <td colspan="3" style="height: 20px">
                                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                <ContentTemplate>
                                                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                                                </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                    <tr>
                         <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="1">
                            <b class="txt3">Doctor Leave</b>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table width="80%" cellpadding="1" cellspacing="1">
                                          
                                    
                            
                            
                            
                                <tr>
                                    <td align="center" class="td-widget-bc-search-desc-ch1">
                                        Doctor Name:
                                    </td>
                                    <td align="center" class="td-widget-bc-search-desc-ch1">
                                        Date:
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:DropDownList ID="ddlDoctor" runat="server" Width="97%" AutoPostBack="true" OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged"> </asp:DropDownList>
                                        <asp:DropDownList ID="ddlReferringDoctor" Width="97%" runat="server" Visible="false" OnSelectedIndexChanged="ddlReferringDoctor_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calExtDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgbtnAppointmentDate"></ajaxToolkit:CalendarExtender>
                                        <asp:ImageButton ID="imgbtnAppointmentDate" runat="server" ImageUrl="~/Images/cal.gif"></asp:ImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" class="td-widget-bc-search-desc-ch1">
                                        Reason:
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSearch_onclick"      />
                                        <input type="button" id="btnClear" onclick="Clear();" style="width:80px" value="Clear" />
                                        <asp:TextBox ID="txtCompanyID" runat="server"  Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtDoctorID" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="1">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="vertical-align: middle; width: 30%" id="Searchtd" runat="server" align="left">
                                        Search:&nbsp;
                                        <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input"></gridsearch:XGridSearchTextBox>
                                    </td>
                                    <td style="vertical-align: middle; width: 30%" align="left">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                                            <ProgressTemplate>
                                                <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress" runat="Server">
                                                <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....." Height="25px" Width="24px"></asp:Image>
                                                    Loading...
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                    <td style="vertical-align: middle; width: 40%; text-align: right" id="Exceltd" runat="server" align="right" colspan="2">
                                        Record Count:<%= grdDoctorleaves.RecordCount%>| Page Count:
                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown >
                                        <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                            Text="Export TO Excel" >
                                        <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" ></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <xgrid:XGridViewControl ID="grdDoctorleaves" runat="server" Height="148px" Width="1002px"
                                            CssClass="mGrid" AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="50"
                                            DataKeyNames="I_LEAVE_ID,SZ_DOCTOR_ID" XGridKey="Bill_Sys_Doctor_Leave" GridLines="None" AllowPaging="true"
                                            AlternatingRowStyle-BackColor="#EEEEEE" 
                                            HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                            ShowExcelTableBorder="true" ExcelFileNamePrefix="ExcelLitigation" MouseOverColor="0, 153, 153"
                                            AutoGenerateColumns="false">
                                            <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:BoundField DataField="I_LEAVE_ID" HeaderText="Leave Id" Visible="False" ></asp:BoundField>
                                                <asp:BoundField DataField="SZ_DOCTOR_ID" HeaderText="Doctor Id" Visible="False" ></asp:BoundField>
                                                <asp:BoundField DataField="SZ_DOCTOR_NAME" HeaderText="Doctor name" ></asp:BoundField>
                                                <asp:BoundField DataField="DT_LEAVE_DATE" HeaderText="Leave Date" ></asp:BoundField>
                                                <asp:BoundField DataField="SZ_LEAVE_REASON" HeaderText="Leave Reason" ></asp:BoundField>
                                                <asp:TemplateField HeaderText="">
                                                    <headertemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                                    </headertemplate>
                                                    <itemtemplate>
                                                         <asp:CheckBox ID="ChkDelete" runat="server" />
                                                    </itemtemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </xgrid:XGridViewControl>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
    </table>
    
    
</asp:Content>
