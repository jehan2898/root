<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_WC_Configure.aspx.cs" Inherits="AJAX_Pages_Bills_Sys_WC_Configure"
    Title=" Green Your Bills- WC Configuration" %>

<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
         function SelectAll(ival)
       {
       
            var f= document.getElementById("<%= grdConfiguration.ClientID %>");	
            var str = 1;
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {	
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
		       {		
		          f.getElementsByTagName("input").item(i).checked=ival;
        	   }	
			    
			}
       }
    function FunchkPalceService()
    {
           
          var headerchk = document.getElementById('<%=chkPalceService.ClientID %>');
                   
            if(headerchk.checked==true)
            {
               document.getElementById('<%=txtPlaceService.ClientID %>').disabled = true;
                document.getElementById('<%=txtPlaceService.ClientID %>').value="";
              
            }
            else if(headerchk.checked==false)
            {
              document.getElementById('<%=txtPlaceService.ClientID %>').disabled = false;
             
            }
    }
    </script>

    <table width="100%" style="background-color: white">
        <tr>
            <td valign="top">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <contenttemplate>
        <table width="65%">
            <tr>
                <td>
                   <table style="width:100%; border-bottom:#b5df82 1px solid; border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;"  >
                   
                        <tr>
                            <td style="background-color:#b4dd82; height:15%; font-weight:bold; font-size:small">
                                &nbsp;WC-Configuration
                            </td>
                        </tr>
                        <tr>
                        <td>
                            <asp:UpdatePanel ID="upmsg" runat="server">
                    <ContentTemplate>
                        <UserMessage:MessageControl runat="server" ID="usrMessage" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                        </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:35%" align="left">
                                            Format Code:
                                        </td>
                                        <td style="width:70%" align="left">
                                            <asp:DropDownList ID="ddlFormatCode" runat="server" OnSelectedIndexChanged="ddlFormatCode_SelectedIndexChanged" AutoPostBack="true" Width="125px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Format Value:
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlFormatValue" runat="server" OnSelectedIndexChanged="ddlFormatValue_SelectedIndexChanged" AutoPostBack="true" Width="125px">
                                            </asp:DropDownList> eg.  <asp:Label ID="lblExample"  runat="server"></asp:Label>
                                           
                                            
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left">
                                            Type:
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlType" runat="server" Width="125px">
                                                <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                                                <asp:ListItem Text="WC" Value="WC"></asp:ListItem>
                                                 <asp:ListItem Text="Nofault" Value="Nofault"></asp:ListItem>
                                                 <asp:ListItem Text="Lien" Value="Lien"></asp:ListItem>
                                                 <asp:ListItem Text="Private" Value="Private"></asp:ListItem>
                                                <asp:ListItem Text="Screen" Value="Screen"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                  <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
            <tr style="height:5px;">
            <td>
            
            </td>
            </tr>
            <tr>
            <td>
            <table style="width:100%; border-bottom:#b5df82 1px solid; border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;" >
                <tr>
               <td style="background-color:#b4dd82; height:15%; font-weight:bold; font-size:small">
                &nbsp;Configuration
               </td>
               </tr>
               <tr>
               <td>
              <asp:UpdatePanel ID="uplwconfig" runat="server">
               <ContentTemplate>
                <UserMessage:MessageControl runat="server" ID="usrMessage2" />
               </ContentTemplate>
                </asp:UpdatePanel>
               </td>
               </tr>
               <tr>
               <td>
                   <table width="100%" style=" height:50px;">
                   <tr style="height:5px;">
                   <td>
                   </td>
                   </tr>
                   <tr>
                   <td >
                      <asp:CheckBox ID="chkdtac" runat="server" text="Block bill creation when date of accident is missing"></asp:CheckBox>
                   </td>
                   </tr>
                   <tr style="height:5px;">
                   <td>
                   </td>
                   </tr>
                   <tr>
                   <td>
                    <asp:UpdatePanel ID="upwcconfig" runat="server">
                   <contenttemplate>
                     <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                    </contenttemplate>
                   </asp:UpdatePanel>
                   
                   </td>
                   </tr>
                   </table>
               </td>
               </tr>
            </table>
            
            </td>
            </tr>
            <tr height="15%">
            <td>
            </td>
            </tr>
            <tr>
            <td>
             <table style="width:100%; border-bottom:#b5df82 1px solid; border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;" >
                <tr>
               <td style="background-color:#b4dd82; height:15%; font-weight:bold; font-size:small">
                &nbsp;WC C-4AMR(PDF) Settings
               </td>
               </tr>
               <tr>
               <td>
              <asp:UpdatePanel ID="upcconfig" runat="server">
               <ContentTemplate>
                <UserMessage:MessageControl runat="server" ID="usrMessage3" />
               </ContentTemplate>
                </asp:UpdatePanel>
               </td>
               </tr>
               <tr>
               <td>
                   <table width="100%" style=" height:50px;">
                   <tr style="height:5px;">
                   <td>
                   </td>
                   </tr>
                   <tr>
                   <td width="100%" >
                     <div class="lbl" width="100%">
                      <asp:RadioButtonList ID="rdlstNPIConfig" AutoPostBack="true" runat="server"
                        RepeatDirection="Horizontal" width="100%">
                        <asp:ListItem Value="0"> NPI # of Treating Provider</asp:ListItem>
                        <asp:ListItem Value="1">NPI # of Treating Doctor</asp:ListItem>
                     </asp:RadioButtonList>
                     </div>
                   </td>
                   </tr>
                   <tr style="height:5px;">
                   <td width="100%" >
                     <div class="lbl" width="100%">
                     <asp:RadioButtonList ID="rdlstZipConfig" AutoPostBack="true" runat="server"
                        RepeatDirection="Horizontal" width="125%">
                        <asp:ListItem Value="0"> Account Zip Code</asp:ListItem>
                        <asp:ListItem Value="1"> Service Location Zip Code</asp:ListItem>
                     </asp:RadioButtonList>
                    </div>
                   </td>
                   </tr>
                   <tr height="5%">
                   <td width="100%">
                    <div class="lbl" width="100%">
                   <asp:CheckBox ID="chkPalceService" runat="server" Text="Default Place Of Service"></asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:TextBox ID="txtPlaceService" runat="server" ></asp:TextBox>
                   </div>
                   </td>
                   </tr>
                     <tr height="5%">
                   <td width="100%">
                    <div class="lbl" width="100%">
                   <asp:CheckBox ID="chkBalanceDue" runat="server" Text="Show Balance Due on PDF"></asp:CheckBox>
                  
                   </div>
                   </td>
                   </tr>
                   <tr height="5%">
                   <td>
                   </td>
                   </tr>
                   <tr>
                   <td>
                    <asp:UpdatePanel ID="upccconfig" runat="server">
                   <contenttemplate>
                     <asp:Button ID="btnUpdateC4AMR" runat="server" Text="Update" OnClick="btnUpdateC4AMR_Click" />
                    </contenttemplate>
                   </asp:UpdatePanel>
                   
                   </td>
                   </tr>
                   </table>
               </td>
               </tr>
            </table>
            </td>
            </tr>
        </table>
        </contenttemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width: 50%" valign="top" align="center">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <table style="width: 100%; border-bottom: #b5df82 1px solid; border-right: #b5df82 1px solid;
                                        border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;">
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="visibility: hidden;">
                                                            Search:
                                                            <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                Visible="false" CssClass="search-input"></gridsearch:XGridSearchTextBox>
                                                        </td>
                                                        <td align="right" style="visibility: hidden;">
                                                            Record Count:<%= this.grdConfiguration.RecordCount%>
                                                            | Page Count:
                                                            <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                            </gridpagination:XGridPaginationDropDown>
                                                            <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                Text="Export TO Excel">
                                        <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:UpdatePanel ID="upmsg1" runat="server">
                                                                <contenttemplate>
                        <UserMessage:MessageControl runat="server" ID="usrMessage1" />
                    </contenttemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="overflow: scroll; height: 200px;">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <xgrid:XGridViewControl ID="grdConfiguration" runat="server" Width="100%" CssClass="mGrid"
                                                                    MouseOverColor="0, 153, 153" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                                                    HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                                    DataKeyNames="I_CONFIGURATION_ID" ShowExcelTableBorder="true" ExportToExcelColumnNames="Format Code, Format Value,Case Type"
                                                                    ExportToExcelFields="SZ_FORMAT_CODE, SZ_FORMAT_VALUE, sz_case_type" AllowPaging="true"
                                                                    XGridKey="WC_Configuration" PageRowCount="50" PagerStyle-CssClass="pgr" AllowSorting="true"
                                                                    AutoGenerateColumns="false" GridLines="None">
                                                                    <Columns>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                            HeaderText="Format Code" DataField="SZ_FORMAT_CODE" SortExpression="SZ_FORMAT_CODE" />
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                            HeaderText="Format Value" DataField="SZ_FORMAT_VALUE" SortExpression="SZ_FORMAT_VALUE" />
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                            HeaderText="Case Type" DataField="sz_case_type" SortExpression="sz_case_type" />
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                            HeaderText="CONFIGURATION ID" DataField="I_CONFIGURATION_ID" SortExpression="I_CONFIGURATION_ID"
                                                                            visible="false" />
                                                                        <asp:TemplateField HeaderText="">
                                                                            <headertemplate>
                                                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All"  />
                                                                            </headertemplate>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                            <itemtemplate>
                                                                             <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                            </itemtemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </xgrid:XGridViewControl>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="2">
                                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txthdnCompanyID" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>