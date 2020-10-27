<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_DenialReasons.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_DenialReasons" Title="Denial Reasons Master" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"  TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
    .hiddencol
    {
        display:none;
    }
</style>

 <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
 <script type="text/javascript">
 
    
      function   checkSelected()
      {
           
            var f= document.getElementById("<%= grdDenialReasons.ClientID %>");	
            var str = 1;
           var iflag=false;
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {	
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
		       {		
		          if(f.getElementsByTagName("input").item(i).checked)
		          {
		            iflag=true;
		            break;
		          }
        	   }	
			    
			}
			
			 if(iflag == false)
		        {
		            alert('Please select record.');
		            return false;
		        }
		        else
		        {
		            return true;
		        }
      }
 
           function SelectAll(ival)
           {
           
            var f= document.getElementById("<%= grdDenialReasons.ClientID %>");	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
		            if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			        {						
			            if(f.getElementsByTagName("input").item(i).disabled==false)
			            f.getElementsByTagName("input").item(i).checked=ival;
			        }			
		        }
		    }
 </script>
  <asp:UpdateProgress ID="UpdateProgress122" runat="server" DisplayAfter="0" >
                      <ProgressTemplate>
                        <asp:Image ID="img12" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/LF/Images/simple-loading2.gif" AlternateText="Loading....."
                            Height="50px" Width="48px"></asp:Image>
                       </ProgressTemplate>
  </asp:UpdateProgress>
 <asp:UpdatePanel ID="UpdatePanel12" runat="server">
    <contenttemplate>
 <table align="center" style="width:80%; text-align:center">
 
    <tr style="margin-bottom:5%; text-align:left">
        <td style="width:80%">
            <asp:TextBox ID="txtDenialID" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox width="90%" ID="txtDenialReason" runat="server"></asp:TextBox>
        </td>
        <td style="width:10%">
            <asp:Button width="80%" Text="Add" OnClick="btnAdd_Click" ID="btnAdd" runat="server" />
        </td>
        <td style="width:10%">
            <asp:Button width="80%" Text="Update" OnClick="btnUpdate_Click" ID="btnUpdate" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            
                    
                    <table style="vertical-align: middle; width: 100%" >
                            <tbody>
                                <tr>
                                    <td colspan="2">
                                                <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                    </td>
                              </tr>
                                <tr>
                                    <td style="vertical-align: middle; width: 30%" align="left">
                                      Search:  <gridsearch:XGridSearchTextBox ID="txtsearch" runat="server" AutoPostBack="true" 
                                            CssClass="search-input">
                                        </gridsearch:XGridSearchTextBox>
                                        
                                    </td>
                                    <td style="vertical-align: middle; width: 70%;" align="right">
                                         Record Count:<%= this.grdDenialReasons.RecordCount%>
                                        | Page Count:
                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown>
                                       <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                    Text="Export TO Excel">
                                        <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                    </td>
                                    
                                </tr>
                                <tr>
                               
                                  <td align="right" colspan="2">
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"  />
                                    </td>
                               
                                </tr>
                                
                            </tbody>
                        </table>
                        
                        <xgrid:XGridViewControl ID="grdDenialReasons" runat="server" Width="100%"
                            CssClass="mGrid" AllowSorting="true" DataKeyNames="DenialID,DenialReason"
                            PagerStyle-CssClass="pgr" PageRowCount="50" XGridKey="Bill_Sys_DenialMaster" GridLines="None"
                            AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                             ExportToExcelFields="DenialReason"
                            ExportToExcelColumnNames="DenialReason" 
                            HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1"
                            EnableRowClick="false" ShowExcelTableBorder="true" ExcelFileNamePrefix="DenialReason" 
                            MouseOverColor="0, 153, 153" AutoGenerateColumns="false" OnRowCommand="grdDenialReasons_RowCommand" >
                            <HeaderStyle CssClass="GridViewHeader" ></HeaderStyle>
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField ItemStyle-width="20%">
                                    <itemtemplate>
                                     <asp:LinkButton ID="btnSelect" AutoPostBack="False" runat="server" Text="Select" CommandName="Select" 
                                                    CommandArgument='<%# ((GridViewRow) Container).RowIndex%>' />
                                    </itemtemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DenialID">
                                    <headerstyle cssclass="hiddencol" ></headerstyle>
                                    <itemstyle cssclass="hiddencol" ></itemstyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DenialReason" HeaderText="Denial Reason" SortExpression="MST_DENIAL_REASONS.sz_denial_reasons">
                                    <headerstyle horizontalalign="Left"></headerstyle>
                                    <itemstyle  Width="80%"  horizontalalign="Left"></itemstyle>
                                </asp:BoundField>
                                <asp:TemplateField >
                                    <HeaderTemplate>
                                     <asp:CheckBox ID="chkSelectAll" runat="server" tooltip="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                     </HeaderTemplate>
                                    <itemtemplate>
                                     <asp:CheckBox ID="ChkDenail" runat="server" />
                                    </itemtemplate>
                                </asp:TemplateField>
                            </Columns>
                        </xgrid:XGridViewControl>
                   
            </td>    
        
    </tr>
 </table>   
  </contenttemplate>
</asp:UpdatePanel>
 <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox> 
</asp:Content>

