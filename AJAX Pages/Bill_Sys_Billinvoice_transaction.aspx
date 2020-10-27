<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Billinvoice_transaction.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Billinvoice_transaction"
    Title="Software Invoice Transaction" %>

<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
     function Clear()
       {
        document.getElementById("<%=txttransactionname.ClientID%>").value='';
        document.getElementById("<%=txttransactioncost.ClientID%>").value='';
        document.getElementById("ctl00_ContentPlaceHolder1_btnSave").disabled = false;
        document.getElementById("ctl00_ContentPlaceHolder1_btninvoiceupdate").disabled = true;
        document.getElementById("ctl00_ContentPlaceHolder1_ddltype").value=0;
       }
        function Confirm_Delete_Code()
         {     
                var f= document.getElementById("<%=grdInvoiceTransaction.ClientID%>");
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
    function keyup(txttransactioncost)
    {
    if(isNaN(txttransactioncost.value))
    { txttransactioncost.value="";
    }
    }
     function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdInvoiceTransaction.ClientID %>");	
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
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" border="0">
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: White;">
                        <table>
                            <tr>
                                <td>
                                    <%--       <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;height: 50%; border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">--%>
                                    <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                        height: 50%; border: 1px solid #B5DF82;">
                                        <tr>
                                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                <b class="txt3">Invoice Transaction</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td class="td-widget-bc-search-desc-ch1" style="width: 50%">
                                                            Transaction Name
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch1" style="width: 28%">
                                                            Transaction Cost
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch1" style="width: 22%">
                                                            Type
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-widget-bc-search-desc-ch3">
                                                            <asp:TextBox ID="txttransactionname" runat="server" Width="98%" CssClass="search-input"></asp:TextBox>
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch3">
                                                            <input type="text" onkeyup="keyup(this)" runat="server" id="txttransactioncost" maxlength="10"
                                                                style="width: 80%; height: 90%" />
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch3">
                                                            <asp:DropDownList ID="ddltype" runat="server">
                                                                <asp:ListItem Text="Software" Value="0">
                                                                </asp:ListItem>
                                                                <asp:ListItem Text="Storage" Value="1">
                                                                </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btnSave" runat="server" Width="80px" Text="Save" OnClick="btnSave_Click">
                                                </asp:Button>
                                                <asp:Button ID="btninvoiceupdate" runat="server" Visible="true" Text="Update" OnClick="btnupdate_Click">
                                                </asp:Button>
                                                <input type="button" id="btnClear" style="width: 80px" value="Clear" onclick="Clear();" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="background-color: White;">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UP_grdPatientSearch" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                    <b class="txt3">Invoice</b>
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
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DisplayAfter="10" DynamicLayout="true">
                                        <progresstemplate>
                                 <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                     runat="Server">
                                     <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
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
                                        <%= this.grdInvoiceTransaction.RecordCount%>
                                        | Page Count:
                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown>
                                        <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                            Text="Export TO Excel">
                                        <img src="Images/Excel.jpg" alt="" style="border: none;" height="15px" width="15px"
                                            title="Export TO Excel" /></asp:LinkButton>
                                        <asp:Button ID="btndelete" runat="server" Visible="true" Text="Delete" OnClick="btndelete_Click">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    <xgrid:XGridViewControl ID="grdInvoiceTransaction" runat="server" Width="100%" CssClass="mGrid"
                                        DataKeyNames="INVOICE_TRANS_ID,INVOICE_DESC,INVOICE_COST,sz_company_id,TYPE"
                                        MouseOverColor="0, 153, 153" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                        HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                        ExportToExcelFields="INVOICE_DESC,INVOICE_COST" ShowExcelTableBorder="true" ExportToExcelColumnNames="Invoice Desc,Invoice Cost"
                                        AllowPaging="true" XGridKey="Invoice_Transaction" OnRowCommand="grdInvoiceTransaction_RowCommand"
                                        PageRowCount="40" PagerStyle-CssClass="pgr" AllowSorting="true" AutoGenerateColumns="false"
                                        GridLines="None">
                                        <Columns>
                                            <%--0--%>
                                            <asp:TemplateField HeaderText="">
                                                <itemtemplate>
                                             <asp:LinkButton ID="lnkSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' ></asp:LinkButton>
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <%--1--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                headertext="Invoice Description" DataField="INVOICE_DESC" />
                                            <%--2--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                SortExpression="mst_invoice_transaction.mn_cost" headertext="Invoice Cost" DataFormatString="{0:C}"
                                                DataField="INVOICE_COST" />
                                            <%--3--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                headertext="Company Id" DataField="sz_company_id" Visible="false" />
                                            <%--4--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                headertext="Invoicetrans Id" DataField="INVOICE_TRANS_ID" Visible="false" />
                                            <%--5--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                headertext="Type" DataField="TYPE" />
                                            <%--6--%>
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
    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtmncost" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtinvoicetransid" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtinvoicetrans" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txttransactiontype" runat="server" Width="10px" Visible="False"></asp:TextBox>
</asp:Content>
