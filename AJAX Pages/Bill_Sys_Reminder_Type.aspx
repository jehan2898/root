<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Reminder_Type.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Reminder_Type"
    Title="Reminder Type" %>

<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
    
     function Confirm_Delete_Code()
         {     
                var f= document.getElementById("<%=grdremindertype.ClientID%>");
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {		
			            				
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {  bfFlag = true;   
			                    
    		                    
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
		            var msg = "Do you want to proceed?";
                     var result = confirm(msg);
                     if(result==true)
                     {
                        return true;
                     }
                     else
                     {
                        return false;
                     }
		            //return true;
		        }
         }
        
        
        
        
     function SelectAll(ival)
       {
            var f= document.getElementById("<%=grdremindertype.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {	
			       if(f.getElementsByTagName("input").item(i).disabled==false)
		             {					
			             f.getElementsByTagName("input").item(i).checked=ival;
			         }    
			    }			
		    }
       }
       
       
        function CheckControls()
       {
            if(document.getElementById('<%=txtremindertype.ClientID%>').value == '')
            {
                alert('Please Enter Reminder Type');
                
                
                return false;
            }
       }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" runat="server">
        <tr>
            <td style="height: 28px" bgcolor="#b5df82" class="txt2" align="center">
                <asp:Label ID="lblHeader" runat="server" Font-Size="Medium" Font-Names="Verdana"
                    Text="Reminder Type" Font-Bold="True"></asp:Label>
            </td>
        </tr>
    </table>--%>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <asp:UpdatePanel ID="UpdatePanemain" runat="server">
                    <ContentTemplate>
                        <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;
                            background-color: White">
                            <tr>
                                <td class="LeftTop">
                                </td>
                                <td class="CenterTop">
                                </td>
                                <td class="RightTop">
                                </td>
                            </tr>
                            <tr>
                                <td class="LeftCenter" style="height: 100%">
                                </td>
                                <td class="Center" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;
                                        background-color: White">
                                        <tr>
                                            <td colspan="4">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div id="ErrorDiv" style="color: red" visible="true">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <table border="0" cellpadding="3" cellspacing="2" class="ContentTable" style="width: 50%;
                                                    border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82; border-bottom: 1px solid #B5DF82">
                                                    <tr>
                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                            <b class="txt3">Parameters</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-widget-bc-search-desc-ch">
                                                            Reminder Type</td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtremindertype" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="btnSearch_Click" />
                                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px" />
                                                                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                                        Width="80px" />
                                                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 35px" align="center" colspan="2">
                                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel6"
                                                                DisplayAfter="10" DynamicLayout="true">
                                                                <progresstemplate>
                                                                            <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                            runat="Server">
                                                                            <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                            Height="25px" Width="24px"></asp:Image>
                                                                            Loading...
                                                                            </div>
                                                                        </progresstemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" valign="top">
                                                <table style="width: 100%; height: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:UpdatePanel ID="UP_grdPatientSearch" runat="server">
                                                                <ContentTemplate>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                                                                <b class="txt3"></b>
                                                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_grdPatientSearch"
                                                                                    DisplayAfter="10" DynamicLayout="true">
                                                                                    <progresstemplate>
                                                                                                <div id="Div1" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                                runat="Server">
                                                                                                <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                Height="25px" Width="24px"></asp:Image>
                                                                                                Loading...</div>
                                                                                            </progresstemplate>
                                                                                </asp:UpdateProgress>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table style="vertical-align: middle; width: 100%;">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="vertical-align: middle; width: 30%" align="left">
                                                                                    Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                                        CssClass="search-input">
                                                                                    </gridsearch:XGridSearchTextBox>
                                                                                    <%--<XCon:XControl ID="xcon" runat="server" Visible ="false"></XCon:XControl>--%>
                                                                                </td>
                                                                                <td style="width: 60%" align="right">
                                                                                    Record Count:
                                                                                    <%= this.grdremindertype.RecordCount%>
                                                                                    | Page Count:
                                                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                    <asp:Button ID="btndelete" runat="server" Visible="true" Text="Delete" OnClick="btnDelete_Click">
                                                                                    </asp:Button>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <xgrid:XGridViewControl ID="grdremindertype" runat="server" Width="100%" CssClass="mGrid"
                                                                                    DataKeyNames="I_REMINDER_TYPE_ID,SZ_REMINDER_TYPE" MouseOverColor="0, 153, 153"
                                                                                    EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                                    AlternatingRowStyle-BackColor="#EEEEEE" ExportToExcelFields="" ShowExcelTableBorder="true"
                                                                                    ExportToExcelColumnNames="" AllowPaging="true" XGridKey="Bill_Sys_Reminder_Type"
                                                                                    PageRowCount="40" PagerStyle-CssClass="pgr" AllowSorting="true" AutoGenerateColumns="false"
                                                                                    GridLines="None" Visible="true" OnRowCommand="grdremindertype_RowCommand" OnRowEditing="grdremindertype_RowEditing">
                                                                                    <Columns>
                                                                                        <%--0--%>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <itemtemplate>
                                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                                    CommandName="Edit"></asp:LinkButton>
                                                                                                </itemtemplate>
                                                                                        </asp:TemplateField>
                                                                                        <%--1--%>
                                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                            headertext="I_REMINDER_TYPE_ID" DataField="I_REMINDER_TYPE_ID" Visible="False" />
                                                                                        <%--2--%>
                                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                            headertext="Reminder Type" DataField="SZ_REMINDER_TYPE" />
                                                                                        <%--3--%>
                                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                            headertext="SZ_COMPANY_ID" DataField="SZ_COMPANY_ID" Visible="False" />
                                                                                        <%--4--%>
                                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                                            headertext="SZ_USER_ID" DataField="SZ_USER_ID" Visible="False" />
                                                                                        <%--5--%>
                                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                                            DataFormatString="{0:MM/dd/yyyy}" SortExpression="MST_REMINDER_TYPE.DT_DATE"
                                                                                            headertext=" Date" DataField="DT_DATE" />
                                                                                        <%--6--%>
                                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="right"
                                                                                            headertext="User Name" DataField="SZ_USER_NAME" />
                                                                                        <%-- 7--%>
                                                                                        <asp:TemplateField HeaderText="">
                                                                                            <headertemplate>
                                                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                                                                                </headertemplate>
                                                                                            <itemtemplate>
                                                                                                <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                                            </itemtemplate>
                                                                                            <headerstyle horizontalalign="center"></headerstyle>
                                                                                            <itemstyle horizontalalign="center"></itemstyle>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </xgrid:XGridViewControl>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="con" />
                                                                </Triggers>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
    <asp:TextBox ID="utxtUserId" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtremindertypeid" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtremindertype" runat="server" Width="10px" Visible="false"></asp:TextBox>
</asp:Content>
