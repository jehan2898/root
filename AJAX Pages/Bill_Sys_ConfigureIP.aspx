<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_ConfigureIP.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_ConfigureIP"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
       <script type="text/javascript">
           function SelectAllUserRoll2(ival)
           {
           
            var f= document.getElementById("<%= grdUserRoll2.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        if(f.getElementsByTagName("input").item(i).disabled==false)
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
		    }
		    function SelectAllUserRoll(ival)
           {
            var f= document.getElementById("<%= grdUserRoll.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
		    }
           function checkslect(obj)
           {
           alert(11);
                //var val=  document.getElementById("ctl00_ContentPlaceHolder1_extddlUserRoll").value ;
                var val=  document.getElementById(obj).value ;
                alert(val);
             if(val=='NA')
             {
                alert('Please Select User Role');
                return false;
             }else
             {
                return true;
             }
           }
           
           function SelectCheck()
           {
                var f= document.getElementById("<%=grdUserRoll.ClientID%>");
		        var bfFlag = false;	

		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
		                
		           // if(f.getElementsByTagName("input").item(i).name.indexOf('chkSelect') !=-1)
		         //   {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {
			                    
			                    bfFlag = true;
			                }
			            }
			    //    }
		        }
		        if(bfFlag == false)
		        {
		            alert('Please Select Record.');
		            return false;
		        }
		        var check=confirm('Would You like to delete old IP Addresses');
		        if(check==true)
		        {
		            document.getElementById("<%=txtPreviousIP.ClientID %>").value="true";
		        }
		        else
		        {
		            document.getElementById("<%=txtPreviousIP.ClientID %>").value="false";
		        }
		        
           }
           function checkslect2(obj)
           {
           alert(10);
             //var val=  document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_EnableIPtab_ExtendedDropDownList12").value ;
             var val=  document.getElementById(obj).value ;
             alert(val);
             if(val=='NA')
             {
                alert('Please Select User Role');
                return false;
             }else
             {
                return true;
             }
           }
           
           function SelectCheck2()
           {
         
                var f= document.getElementById("<%=grdUserRoll2.ClientID%>");
		        var bfFlag = false;	

		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
		                
		           // if(f.getElementsByTagName("input").item(i).name.indexOf('chkSelect') !=-1)
		         //   {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {
			                    
			                    bfFlag = true;
			                }
			            }
			    //    }
		        }
		        if(bfFlag == false)
		        {
		            alert('Please Select Record.');
		            return false;
		        }
           }
      </script>
      <div id="div1" runat="server" visible="false" style="z-index:1">
      You are being redirected....
      </div>
      <ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
        <ajaxToolkit:TabPanel ID="EnableIPtab" runat="server">
            <HeaderTemplate>
                <div style="font-size:9pt">Enable IP Address</div>
            </HeaderTemplate>
            <ContentTemplate>
                <table style="width:100%; height:360px">
                   <tr>
                    <td style="width: 40%; vertical-align:top">
                                <table style="width:100%; border-width:0">
                                    <tr>
                                        <td>
                                            <table style="border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;
                                                width: 100%; border-bottom: #b5df82 1px solid; " onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')"
                                                    cellspacing="0" cellpadding="0" align="left" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="txt2" valign="middle" align="left" bgcolor="#b5df82" colspan="2" height="28">
                                                            <b class="txt3">Search Parameters</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <table style="width: 100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 30%" align="center">
                                                                            <asp:Label ID="Label12" runat="server" style="font-size:small" Text="User Roll"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 70%" align="left">
                                                                            <extddl:ExtendedDropDownList ID="ExtendedDropDownList12" runat="server" Width="85%" AutoPost_back="false"
                                                                                Selected_Text="---Select---" Procedure_Name="SP_GET_USER_ROLL" Flag_Key_Value="GET_KEY_LIST"
                                                                                Connection_Key="Connection_String" >
                                                                            </extddl:ExtendedDropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td style="width:40%"></td>
                                                                         <td style="width: 60%" align="left">
                                                                            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                                                <contenttemplate>
                                                                                    <asp:Button ID="btnSearch2"  runat="server" Width="80px" Text="Search" OnClick="btnSearch2_Click"></asp:Button>
                                                                                    &nbsp;&nbsp;&nbsp;
                                                                                    <asp:Button ID="btnSave2"  runat="server" Width="80px" Text="Save" Enabled="False" OnClick="btnSave2_Click"></asp:Button>
                                                                                </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                       </td>
                                                  </tr> 
                                            </tbody>
                                         </table>
                                       </td>
                                    </tr>
                                </table>
                    </td>
                    <td style="width: 60%; vertical-align:top; padding-right:1%; padding-left:1%">
                        <div style="width: 100%;">
                            <table style="height: auto; width: 95%; border: 1px solid #B5DF82;" class="txt2"
                                align="left" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td height="28" align="left" valign="middle" bgcolor="#b5df82" class="txt2" style="width: 100%">
                                        <b class="txt3">Enable IP Settings</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                            <contenttemplate>
                                             <asp:UpdateProgress ID="UpdateProgress122" runat="server" DisplayAfter="0" >
                                              <ProgressTemplate>
                                                <asp:Image ID="img12" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/LF/Images/simple-loading2.gif" AlternateText="Loading....."
                                                    Height="50px" Width="48px"></asp:Image>
                                               </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <table style="vertical-align: middle; width: 100%" >
                                                    <tbody>
                                                        <tr>
                                                            <td style="vertical-align: middle; width: 30%" align="left">
                                                              Search:  <gridsearch:XGridSearchTextBox ID="txtSearchBox2" runat="server" AutoPostBack="true" 
                                                                    CssClass="search-input">
                                                                </gridsearch:XGridSearchTextBox>
                                                                
                                                            </td>
                                                            <td style="vertical-align: middle; width: 70%;" align="right">
                                                                 Record Count:<%= this.grdUserRoll2.RecordCount%>
                                                                | Page Count:
                                                                <gridpagination:XGridPaginationDropDown ID="con2" runat="server">
                                                                </gridpagination:XGridPaginationDropDown>
                                                               <asp:LinkButton ID="lnkExportToExcel2" OnClick="lnkExportTOExcel2_onclick" runat="server"
                                                                                            Text="Export TO Excel">
                                                                                        <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                            </td>
                                                            
                                                        </tr>
                                                        
                                                    </tbody>
                                                </table>
                                                
                                                <xgrid:XGridViewControl ID="grdUserRoll2" runat="server" Width="100%"
                                                    CssClass="mGrid" AllowSorting="true" DataKeyNames="IPAuthorization"
                                                    PagerStyle-CssClass="pgr" PageRowCount="50" XGridKey="Bill_Sys_ConfigureIP2" GridLines="None"
                                                    AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                                                     ExportToExcelFields="UserName,UserRoleName,UserEmail"
                                                    ExportToExcelColumnNames="User Name,User RoleName,User Email" 
                                                    HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1"
                                                    EnableRowClick="false" ShowExcelTableBorder="true" ExcelFileNamePrefix="ConfigureIP"
                                                    MouseOverColor="0, 153, 153" AutoGenerateColumns="false" >
                                                    <HeaderStyle CssClass="GridViewHeader" ></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:TemplateField >
                                                            <HeaderTemplate>
                                                             <asp:CheckBox ID="chkSelectAll" runat="server" tooltip="Select All" onclick="javascript:SelectAllUserRoll2(this.checked);"/>
                                                             </HeaderTemplate>
                                                            <itemtemplate>
                                                             <asp:CheckBox ID="ChkIPAuthorization2" runat="server" />
                                                            </itemtemplate>
                                                        </asp:TemplateField>
                                                      
                                                        <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="MST_USERS.SZ_USER_NAME">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UserRoleName" HeaderText="User Role" SortExpression="MST_USER_ROLES.SZ_USER_ROLE">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                     
                                                        <asp:BoundField DataField="UserEmail" HeaderText="User Email" SortExpression="MST_USERS.SZ_EMAIL">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        
                                                        <asp:BoundField DataField="IPAuthorization" HeaderText="IPAuthorization" SortExpression="Case when (ISNULL(MST_USERS.BT_IP_ENABLED,0)=1) then 'Enabled' else case when (sz_sys_setting_id is not null) then 'Admin' else 'Disabled' end end">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CompanyId" HeaderText="CompanyId"
                                                            Visible="True">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UserRoleId" HeaderText="UserRoleId"
                                                            Visible="True">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UserId" HeaderText="UserId"
                                                            Visible="True">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </contenttemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                
                            </table>
                        </div>
                    </td>
                </tr>
               
            </table>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>    
        <ajaxToolkit:TabPanel ID="SetIpTab" runat="server">
            <HeaderTemplate>
                <div style="font-size:9pt;">Set Ip Address</div>
            </HeaderTemplate>
            <ContentTemplate>
                 <table style="width:100%; height:360px">
                   <tr>
                    <td style="width: 40%; vertical-align:top">
                                <table style="width:100%; border-width:0">
                                    <tr>
                                        <td>
                                            <table style="border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;
                                                width: 100%; border-bottom: #b5df82 1px solid; " onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')"
                                                    cellspacing="0" cellpadding="0" align="left" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="txt2" valign="middle" align="left" bgcolor="#b5df82" colspan="2" height="28">
                                                            <b class="txt3">Search Parameters</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <table style="width: 100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 30%" align="center">
                                                                            <asp:Label ID="lblKey" runat="server" style="font-size:small" Text="User Roll"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 70%" align="left">
                                                                            <extddl:ExtendedDropDownList ID="extddlUserRoll" runat="server" Width="85%" AutoPost_back="false"
                                                                                Selected_Text="---Select---" Procedure_Name="SP_GET_USER_ROLL" Flag_Key_Value="GET_KEY_LIST"
                                                                                Connection_Key="Connection_String" >
                                                                            </extddl:ExtendedDropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td style="width:40%"></td>
                                                                         <td style="width: 60%" align="left">
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <contenttemplate>
                                                                                    <asp:Button ID="btnSearch"  runat="server" Width="80px" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                                                                                </contenttemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                       </td>
                                                  </tr> 
                                            </tbody>
                                         </table>
                                       </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <asp:UpdatePanel ID="SaveIpUpdatePnl" runat="server">
                                            <contenttemplate>
                                            <table ID="SaveIpTable" runat="server" visible="false" style="border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;
                                                width: 100%; border-bottom: #b5df82 1px solid; margin-top:28px; " cellspacing="0" cellpadding="0" align="left" border="0">
                                                <tr>
                                                    <td valign="middle" align="left" bgcolor="#b5df82" colspan="2" height="28">
                                                        <b >Set IP Address</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <table style="width: 100%; ">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 30%" align="center">
                                                                            <asp:Label ID="lblIpAddress" Text="IP Address:-" runat="server" style="font-size:small;"></asp:Label>  
                                                                        </td>
                                                                        <td style="width: 70%" align="left">
                                                                            <asp:TextBox ID="txtIpAddress" Text="" Visible="true" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td style="width:40%"></td>
                                                                         <td style="width: 60%" align="left">
                                                                                    <asp:Button ID="btnSave" Text="Save" runat="server"  OnClick="btnSave_OnClick"/>
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                    </td>
                                                </tr>        
                                            </table>
                                            </contenttemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                    </td>
                    <td style="width: 60%; vertical-align:top; padding-right:1%; padding-left:1%">
                        <div style="width: 100%;">
                            <table style="height: auto; width: 95%; border: 1px solid #B5DF82;" class="txt2"
                                align="left" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td height="28" align="left" valign="middle" bgcolor="#b5df82" class="txt2" style="width: 100%">
                                        <b class="txt3">Key Settings</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <contenttemplate>
                                             <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0" >
                                              <ProgressTemplate>
                                                <asp:Image ID="img1" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/LF/Images/simple-loading2.gif" AlternateText="Loading....."
                                                    Height="50px" Width="48px"></asp:Image>
                                               </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <table style="vertical-align: middle; width: 100%" >
                                                    <tbody>
                                                        <tr>
                                                            <td style="vertical-align: middle; width: 30%" align="left">
                                                              Search:  <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true" 
                                                                    CssClass="search-input">
                                                                </gridsearch:XGridSearchTextBox>
                                                                
                                                            </td>
                                                            <td style="vertical-align: middle; width: 70%;" align="right">
                                                                 Record Count:<%= this.grdUserRoll.RecordCount%>
                                                                | Page Count:
                                                                <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                </gridpagination:XGridPaginationDropDown>
                                                               <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                                            Text="Export TO Excel">
                                                                                        <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                            </td>
                                                            
                                                        </tr>
                                                        
                                                    </tbody>
                                                </table>
                                                
                                                <xgrid:XGridViewControl ID="grdUserRoll" runat="server" Width="100%"
                                                    CssClass="mGrid" AllowSorting="true" DataKeyNames="IPAuthorization"
                                                    PagerStyle-CssClass="pgr" PageRowCount="50" XGridKey="Bill_Sys_ConfigureIP" GridLines="None"
                                                    AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                                                     ExportToExcelFields="UserName,UserRoleName,UserEmail"
                                                    ExportToExcelColumnNames="User Name,User RoleName,User Email" 
                                                    HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1"
                                                    EnableRowClick="false" ShowExcelTableBorder="true" ExcelFileNamePrefix="ConfigureIP"
                                                    MouseOverColor="0, 153, 153" AutoGenerateColumns="false" >
                                                    <HeaderStyle CssClass="GridViewHeader" ></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                    
                                                        <asp:TemplateField >
                                                            <HeaderTemplate>
                                                             <asp:CheckBox ID="chkSelectAll1" runat="server" tooltip="Select All" onclick="javascript:SelectAllUserRoll(this.checked);"/>
                                                             </HeaderTemplate>
                                                            <itemtemplate>
                                                                                     <asp:CheckBox ID="ChkIPAuthorization" runat="server" />
                                                                                    </itemtemplate>
                                                        </asp:TemplateField>
                                                      
                                                        <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="MST_USERS.SZ_USER_NAME">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UserRoleName" HeaderText="User Role" SortExpression="MST_USER_ROLES.SZ_USER_ROLE">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                     
                                                        <asp:BoundField DataField="UserEmail" HeaderText="User Email" SortExpression="MST_USERS.SZ_EMAIL">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        
                                                        <asp:BoundField DataField="IPAuthorization" HeaderText="IPAuthorization"
                                                            Visible="False">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CompanyId" HeaderText="CompanyId"
                                                            Visible="True">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UserRoleId" HeaderText="UserRoleId"
                                                            Visible="True">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UserId" HeaderText="UserId"
                                                            Visible="True">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="IP" HeaderText="Saved IP Address" >
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                     
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </contenttemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                
                            </table>
                        </div>
                    </td>
                </tr>
               
            </table>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
     
    </ajaxToolkit:TabContainer>
 <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
 <asp:TextBox ID="txtUserRoleID" runat="server" Visible="False" Width="10px"></asp:TextBox>
 <asp:HiddenField ID="txtPreviousIP" runat="server" />
 
</asp:Content>
