<%@ Page Language="C#" MasterPageFile="~/LF/MasterPage.master" AutoEventWireup="true"
    CodeFile="LF_ViewBill.aspx.cs" Inherits="LF_LF_ViewBill" Title="Green Your Bills - Lawfirm - View Bills" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<style type="text/css">
       .fontAdjust {
        font:  10px Arial;
	    text-align: center;
        }
        .Tabletitle{
        font:   10px Arial ;
        font-weight:    Bold ;
        
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
    <script type="text/javascript">
            
            function ascii_value(c){
                c = c . charAt (0);
                var i;
                for (i = 0; i < 256; ++ i)
                {
                    var h = i . toString (16);
                    if (h . length == 1)
                    h = "0" + h;
                    h = "%" + h;
                    h = unescape (h);
                    if (h == c)
                        break;
                }
                return i;
            }
            function CheckForInteger(e,charis)
            {
                var keychar;
                if(navigator.appName.indexOf("Netscape")>(-1))
                {    
                    var key = ascii_value(charis);
                    if(e.charCode == key || e.charCode==0){
                    return true;
                   }else{
                         if (e.charCode < 48 || e.charCode > 57)
                         {             
                                return false;
                         } 
                     }
                 }
                if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
                {          
                    var key=""
                    if(charis!="")
                    {         
                        key = ascii_value(charis);
                    }
                    if(event.keyCode == key)
                    {
                        return true;
                    }
                    else
                    {
                        if (event.keyCode < 48 || event.keyCode > 57)
                        {             
                            return false;
                        }
                    }
                }
            }
            
        function ShowChildGrid (obj)
        {
             var div = document.getElementById(obj);           
             div.style.display ='block';                       
        }

        function ShowDenialChildGrid (obj)
        {
             var div1 = document.getElementById(obj);           
             div1.style.display ='block';                       
        }
        
        function ShowPaymnentChildGrid (obj)
        {
             var div1 = document.getElementById(obj);           
             div1.style.display ='block';                       
        }

        function HideChildGrid (obj)
        {
             var div = document.getElementById(obj);
            div.style.display ='none';                       
        }      
    
        function HideDenialChildGrid (obj)
        {
             var div1 = document.getElementById(obj);
            div1.style.display ='none';                       
        }
        
        function HidePaymentChildGrid (obj)
        {
             var div1 = document.getElementById(obj);
            div1.style.display ='none';                       
        }     
    
     	function showUploadFilePopup()
       {
      
            //document.getElementById('_ctl0_ContentPlaceHolder1_pnlUploadFile').style.height='100px';
            document.getElementById('<%=pnlUploadFile.ClientID%>').style.height='100px';
            document.getElementById('<%=pnlUploadFile.ClientID%>').style.visibility = 'visible';
            document.getElementById('<%=pnlUploadFile.ClientID%>').style.position = "absolute";
	        document.getElementById('<%=pnlUploadFile.ClientID%>').style.top = '200px';
	        document.getElementById('<%=pnlUploadFile.ClientID%>').style.left ='350px';
	        document.getElementById('<%=pnlUploadFile.ClientID%>').style.zIndex= '0';
       }
       function CloseUploadFilePopup()
       {
            document.getElementById('<%=pnlUploadFile.ClientID%>').style.height='0px';
            document.getElementById('<%=pnlUploadFile.ClientID%>').style.visibility = 'hidden';  
          //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
       }

   function checkFile()
    {
          if(document.getElementById('<%=fuUploadReport.ClientID %>').value=='')
             {
                 alert("please select file for upload !.....'");
                 
                 return false;
             }else
             {
                return true
             }
    }
     function OpenDocManager(CaseNo,CaseId,CmpID)
        {                  
           
        window.open('../Document Manager/case/vb_CaseInformation.aspx?caseid='+CaseId+'&caseno='+CaseNo+'&cmpid='+CmpID ,'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
       
        }
        

    </script>

    <table width="100%">
        <tr>
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div>&nbsp;
    </div>
    <table width="100%" align="left" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="100%">
                <div id="psearch-list" width="100%">
                    <table width="100%" align="left" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="428" align="left" valign="top" width="100%">
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="100%" height="374" align="center" valign="top">
                                            <table height="324" border="0" align="left" cellpadding="0" cellspacing="0" class="border">
                                                <tr>
                                                    <td width="960px" height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2">
                                                        &nbsp;&nbsp;<b class="txt3">Bills in my account</b>
                                                    </td>
                                                    <td width="198" align="left" valign="middle" bgcolor="#B5DF82" class="txt2">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                <td>
                                                <asp:GridView ID="grdSearchTotal" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="40%" CellPadding="4" ForeColor="#333333">
                                                <HeaderStyle CssClass="GridViewHeader" BackColor="#B5DF82" Font-Bold="true"/>
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                 <Columns>                               
                                                     <asp:BoundField  ItemStyle-HorizontalAlign="Right" DataField="CLAIM_AMOUNT" DataFormatString="{0:C}"  HeaderText="Total Claim ($)"></asp:BoundField>
                                                     <asp:BoundField  DataField="PAID_AMOUNT"  DataFormatString="{0:C}"  ItemStyle-HorizontalAlign="Right"    HeaderText="Total Paid($)"></asp:BoundField>
                                                     <asp:BoundField DataField="BALANCE" DataFormatString="{0:C}" HeaderText="Total Outstanding($)"   ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                                </Columns>
                                                </asp:GridView> 
                                                </td>                                                
                                                </tr>
                                                <tr>
                                                    <td height="294" colspan="2" align="left" valign="top" width="100%">
                                                        <div width="100%">
                                                            <table width="100%" height="302" border="0" align="left" cellpadding="0" cellspacing="2">
                                                                <tr>
                                                                    <td height="206" align="left" valign="top">
                                                                        <div style="width: 350px; text-align: center;">
                                                                            <div id="grdOuter" runat="server" style="width: 600px; position: relative; text-align: center;
                                                                                left: 0px; top: 0px;">
                                                                                <div style="text-align: left;">
                                                                                    <cMenu:ContextMenu CssClass="cmenu" ID="ContextMenu1" runat="server" BackColor="#99CCCC"
                                                                                        ForeColor="Maroon" RolloverColor="#009999" Width="150" />
                                                                                </div>
                                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                    <contenttemplate>
                                                                                        <table style="vertical-align: middle" border="0" width="920px">
                                                                                            <tr>
                                                                                                <td style="width: 20%; text-align: left">
                                                                                                    Search:
                                                                                                    <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" CssClass="search-input"
                                                                                                        AutoPostBack="true"></gridsearch:XGridSearchTextBox>
                                                                                                </td>
                                                                                                <td style=" text-align: right;">
                                                                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                                                                                                        <ProgressTemplate>
                                                                                                            <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress" runat="Server">
                                                                                                            <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....." Height="25px" Width="24px"></asp:Image>
                                                                                                            Loading...</div>
                                                                                                        </ProgressTemplate>
                                                                                                    </asp:UpdateProgress>
                                                                                                     Record Count:
                                                                                                    <%= this.grdViewBill.RecordCount%>
                                                                                                    |    
                                                                                                </td>
                                                                                                <td style="vertical-align: middle; ; text-align: right" width="14%">
                                                                                                   
                                                                                                    Page Count:
                                                                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                                    </gridpagination:XGridPaginationDropDown>
                                                                                                    <asp:LinkButton ID="lnkExportToExcel" runat="server" OnClick="lnkExportTOExcel_onclick"
                                                                                                        Text="Export TO Excel">
                                                                                                    <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td visible="false">
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <table width="960px">
                                                                                            <tr>
                                                                                                <td width="100%" >
                                                                                                    <xgrid:XGridViewControl ID="grdViewBill" runat="server" CssClass="mGrid" AutoGenerateColumns="false" Width="100%"
                                                                                                        AllowSorting="true" PagerStyle-CssClass="pgr" PageRowCount="30" XGridKey="LF_ViewBill" AllowPaging="true"
                                                                                                        ExportToExcelColumnNames="Bill NO,Provider,DOS,Speciaity,Claim Amount,Paid Amount,Balance,Transfer Date,Law Firm Case ID,Index Number,Purchased Date"
                                                                                                        ShowExcelTableBorder="true" ExportToExcelFields="SZ_BILL_NUMBER,PROVIDER,DOS,SPECIALTY,CLAIM_AMOUNT,PAID_AMOUNT,BALANCE,TRANSFER_DATE,TRANSFER_ID,LF_CASE_ID,SZ_INDEX_NUMBER,DT_PURCHASED_DATE" AlternatingRowStyle-BackColor="#EEEEEE"
                                                                                                        HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                                                                                        MouseOverColor="0, 153, 153" DataKeyNames="SZ_BILL_NUMBER, SZ_COMPANY_ID,bt_denial,bt_verification,bt_Payment"  OnRowCommand="grdViewBill_RowCommand">
                                                                                                        <Columns>
                                                                                                            <%--<column:0>--%>
                                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" SortExpression="SZ_BILL_NUMBER" headertext="Bill NO." DataField="SZ_BILL_NUMBER" />
                                                                                                            <%--<column:1>--%>
                                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" SortExpression="SZ_OFFICE" headertext="Provider" DataField="PROVIDER" visible="true" />
                                                                                                            <%--<column:2>--%>
                                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" headertext="DOS" DataField="DOS" DataFormatString="{0:MM/dd/yyyy}" />
                                                                                                            <%--<column:3>--%>
                                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" SortExpression="SZ_PROCEDURE_GROUP" headertext="Speciaity" DataField="SPECIALTY" />
                                                                                                            <%--<column:4>--%>
                                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="right" SortExpression="FLT_BILL_AMOUNT" headertext="Claim Amount" DataField="CLAIM_AMOUNT" DataFormatString="{0:C}"  />
                                                                                                            <%--<column:5>--%>
                                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="right" SortExpression="FLT_BILL_AMOUNT-FLT_BALANCE" headertext="Paid Amount" DataField="PAID_AMOUNT" DataFormatString="{0:C}" />
                                                                                                            <%--<column:6>--%>
                                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="right" SortExpression="FLT_BALANCE" headertext="Balance" DataField="BALANCE" DataFormatString="{0:C}" />
                                                                                                            <%--<column:7>--%>
                                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" SortExpression="MST_CASE_MASTER.DT_TRANSFER_DATE" headertext="Transfer Date" DataField="TRANSFER_DATE"  DataFormatString="{0:MM/dd/yyyy}" />
                                                                                                            <%--<column:8>--%>
                                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" SortExpression="" headertext="Trasnfer Id" DataField="TRANSFER_ID" visible="false" />
                                                                                                            <%--<column:9>--%>
                                                                                                            <asp:TemplateField HeaderText="Law Firm Case ID" Visible="true">
                                                                                                                <itemtemplate>
                                                                                                                    <asp:TextBox id="txtLFCaseID" runat="server"  Width="80%"  Text=' <%# DataBinder.Eval(Container.DataItem, "LF_CASE_ID") %>'></asp:TextBox>
                                                                                                                </itemtemplate>
                                                                                                                <itemstyle horizontalalign="Center" width="100px" />
                                                                                                            </asp:TemplateField>
                                                                                                            <%--<column:10>--%>
                                                                                                            <asp:TemplateField HeaderText="Index Number" Visible="true">
                                                                                                                <itemtemplate>
                                                                                                                    <asp:TextBox id="txtInexNumber" runat="server" Width="80%"  Text=' <%# DataBinder.Eval(Container.DataItem, "SZ_INDEX_NUMBER") %>'></asp:TextBox>
                                                                                                                </itemtemplate>
                                                                                                                <itemstyle horizontalalign="Center" width="100px" />
                                                                                                            </asp:TemplateField>
                                                                                                            <%--<column:11>--%>
                                                                                                            <asp:TemplateField HeaderText="Purchased Date" Visible="true">
                                                                                                                <itemtemplate>
                                                                                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                                                        ControlToValidate="txtPurchaseddate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid" 
                                                                                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true" Width="100%"></ajaxToolkit:MaskedEditValidator>
                                                                                                                    <asp:TextBox ID="txtPurchaseddate" runat="server" Width="90px"  onkeypress="return CheckForInteger(event,'/')" Text=' <%# DataBinder.Eval(Container.DataItem, "DT_PURCHASED_DATE","{0:MM/dd/yyyy}") %>' ></asp:TextBox>
                                                                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"   Mask="99/99/9999"  MaskType="Date" TargetControlID="txtPurchaseddate" PromptCharacter="_" AutoComplete="true"></ajaxToolkit:MaskedEditExtender>
                                                                                                                    <asp:RangeValidator ID="RangeValidator1" runat="server"  MinimumValue="01/01/1901" MaximumValue="12/01/2050" ControlToValidate="txtPurchaseddate" ErrorMessage="Enter valid date" Type="Date"></asp:RangeValidator>
                                                                                                                     <%--<ajaxToolkit:CalendarExtender ID="calext" runat="server" PopupButtonID="imgPurchaseddate"
                                                                                                                        TargetControlID="txtPurchaseddate" PopupPosition="TopLeft" >
                                                                                                                    </ajaxToolkit:CalendarExtender>--%>
                                                                                                                </itemtemplate>
                                                                                                                <itemstyle horizontalalign="Center" width="130px" />
                                                                                                            </asp:TemplateField>
                                                                                                            <%--<column:12>--%>
                                                                                                            <asp:TemplateField HeaderText="Scan/ Upload"  Visible="true" >
                                                                                                                <itemtemplate>
                                                                                                                    <asp:LinkButton ID="lnkScan" runat="server" Text='Scan' CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Scan" Visible="true" CausesValidation="false"></asp:LinkButton> /
                                                                                                                    <asp:LinkButton ID="lnkUload" runat="server" Text='Upload' CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Upload" Visible="true" CausesValidation="false"></asp:LinkButton>
                                                                                                                </itemtemplate>
                                                                                                                <itemstyle horizontalalign="Center" width="30px" />
                                                                                                            </asp:TemplateField>
                                                                                                            <%--<column:13>--%>
                                                                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                                                                <itemtemplate>
                                                                                                                    <a href="javascript:OpenDocManager('<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_ID")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_COMPANY_ID")%> ');" />
                                                                                                                </itemtemplate>
                                                                                                                <itemstyle horizontalalign="Center" width="30px" />
                                                                                                            </asp:TemplateField>
                                                                                                            <%--<column:14>--%>
                                                                                                            <asp:TemplateField HeaderText="" Visible="true">
                                                                                                                <itemtemplate>
                                                                                                                    <%# DataBinder.Eval(Container,"DataItem.Img")%> 
                                                                                                                </itemtemplate>
                                                                                                                <itemstyle horizontalalign="Center" width="10px" />
                                                                                                            </asp:TemplateField>
                                                                                                            <%--<column:15>--%>
                                                                                                            <asp:TemplateField HeaderText="Verification" visible="true">
                                                                                                                <itemtemplate>
                                                                                                                    <asp:LinkButton ID="lnkP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="PLS"  font-size="15px" 
                                                                                                                        Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>  
                                                                                                                    <asp:LinkButton ID="lnkM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' font-size="15px"  Visible="false"></asp:LinkButton>                                                                                  
                                                                                                                </itemtemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <%--<column:16>--%>
                                                                                                            <asp:TemplateField HeaderText="Denial" visible="true">
                                                                                                                <itemtemplate>
                                                                                                                    <asp:LinkButton ID="lnkDP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="DenialPLS"  font-size="15px" 
                                                                                                                        Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>  
                                                                                                                    <asp:LinkButton ID="lnkDM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="DenialMNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' font-size="15px"  Visible="false"></asp:LinkButton>                                                                                  
                                                                                                                </itemtemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <%--<column:17>--%>
                                                                                                             <asp:TemplateField HeaderText="Payment" visible="true">
                                                                                                                <itemtemplate>
                                                                                                                    <asp:LinkButton ID="lnkPayP" Font-Underline="false" runat="server" CausesValidation="false" CommandName="PaymentPLS"  font-size="15px" 
                                                                                                                        Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>  
                                                                                                                    <asp:LinkButton ID="lnkPayM" Font-Underline="false" runat="server" CausesValidation="false" CommandName="PaymentMNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' font-size="15px"  Visible="false"></asp:LinkButton>                                                                                  
                                                                                                                </itemtemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <%--<column:18>--%>
                                                                                                            <asp:TemplateField visible="false">
	                                                                                                            <itemtemplate>                                            
	                                                                                                            <tr>
		                                                                                                            <td colspan="100%" align="left">
			                                                                                                            <div id="div<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position: relative;">
			                                                                                                            <asp:GridView ID="grdVerification" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="50%" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="mGrid">
			                                                                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
			                                                                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
			                                                                                                            <Columns>
				                                                                                                            <asp:BoundField DataField="verification_request" HeaderText="Verification Request" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left">
					                                                                                                            <ItemStyle Font-size="1.0em" />
				                                                                                                            </asp:BoundField>
				                                                                                                            <asp:BoundField DataField="verification_date" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Verification Date" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
					                                                                                                            <ItemStyle Font-size="1.0em" />
				                                                                                                            </asp:BoundField>
				                                                                                                            <asp:BoundField DataField="answer" HeaderText="Answer" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left">	
				                                                                                                                <ItemStyle Font-size="1.0em" />
				                                                                                                            </asp:BoundField>
				                                                                                                            <asp:BoundField DataField="answer_date" HeaderText="Answer Date" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">	
					                                                                                                            <ItemStyle Font-size="1.0em" />
				                                                                                                            </asp:BoundField>     
			                                                                                                            </Columns>
			                                                                                                            </asp:GridView>
			                                                                                                        
                                                                                                                    </div>
		                                                                                                            </td>
	                                                                                                            </tr>
	                                                                                                            
                                                                                                                   
	                                                                                                            </itemtemplate>
                                                                                                            </asp:TemplateField>
                                                                                                           <%-- <asp:TemplateField visible="false">
	                                                                                                            <itemtemplate>                                            
	                                                                                                            <tr>
		                                                                                                            <td colspan="100%">
			                                                                                                            <div id="div1" style="display: none; position: relative;">
			                                                                                                            <asp:GridView ID="grdNoRec" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="50%" CellPadding="4" ForeColor="#333333" GridLines="None">
			                                                                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
			                                                                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
			                                                                                                            <Columns>
				                                                                                                            <asp:TemplateField HeaderText="" visible="true">
                                                                                                                                <itemtemplate>
                                                                                                                                <asp:Label id="lblNoRec" runat="server" Text="No Records Found."></asp:Label>
                                                                                                                                    
                                                                                                                                </itemtemplate>
                                                                                                                            </asp:TemplateField>     
			                                                                                                            </Columns>
			                                                                                                            </asp:GridView>
			                                                                                                            </div>
		                                                                                                            </td>
	                                                                                                            </tr> 
	                                                                                                            </itemtemplate>
                                                                                                            </asp:TemplateField>--%>
                                                                                                            
                                                                                                            <%--<column:19>--%>
                                                                                                            <asp:TemplateField visible="false">
	                                                                                                            <itemtemplate>                                            
	                                                                                                            <tr>
		                                                                                                            <td colspan="50%" align="right">
			                                                                                                            <div id="div1<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position: relative; ">
			                                                                                                            <asp:GridView ID="grdDenial" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="500px" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="mGrid">
			                                                                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
			                                                                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
			                                                                                                            <Columns>
				                                                                                                            <asp:BoundField DataField="SZ_DENIAL_REASONS" HeaderText="Denial Reason" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left">
					                                                                                                            <ItemStyle Font-size="1.0em" />
				                                                                                                            </asp:BoundField>
				                                                                                                            <asp:BoundField DataField="DT_CREATED_DATE" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Denial Date" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
					                                                                                                            <ItemStyle Font-size="1.0em" />
				                                                                                                            </asp:BoundField>
				                                                                                                            <asp:BoundField DataField="SZ_DESCRIPTION" HeaderText="Description" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left">	
				                                                                                                                <ItemStyle Font-size="1.0em" />
				                                                                                                            </asp:BoundField>
			                                                                                                            </Columns>
			                                                                                                            </asp:GridView>
			                                                                                                            </div>
		                                                                                                            </td>
	                                                                                                            </tr> 
	                                                                                                            </itemtemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <%--<column:20>--%>
                                                                                                            <asp:TemplateField visible="false">
	                                                                                                            <itemtemplate>                                            
	                                                                                                            <tr>
		                                                                                                            <td colspan="50%" align="left">
			                                                                                                            <div id="div2<%# Eval("SZ_BILL_NUMBER") %>" style="display: none; position: relative; ">
			                                                                                                            <asp:GridView ID="grdPayment" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="500px" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="mGrid">
			                                                                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
			                                                                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
			                                                                                                            <Columns>
				                                                                                                            <asp:BoundField DataField="SZ_BILL_ID"  HeaderText="Bill No " HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left" visible="false" >
				                                                                                                            <ItemStyle Font-size="1.0em" />
				                                                                                                            </asp:BoundField>
				                                                                                                            <asp:BoundField DataField="SZ_CHECK_NUMBER" HeaderText="Check No " HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left">
				                                                                                                            <ItemStyle Font-size="1.0em" />
				                                                                                                            </asp:BoundField>
				                                                                                                            <asp:BoundField DataField="DT_CHECK_DATE" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Check Date" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
				                                                                                                            <ItemStyle Font-size="1.0em" />
				                                                                                                            </asp:BoundField>
				                                                                                                            <asp:BoundField DataField="FLT_CHECK_AMOUNT" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Right" HeaderText="Check Amount" DataFormatString="{0:C}">	
				                                                                                                            <ItemStyle Font-size="1.0em" />
				                                                                                                            </asp:BoundField>
			                                                                                                            </Columns>
			                                                                                                            </asp:GridView>
			                                                                                                            </div>
		                                                                                                            </td>
	                                                                                                            </tr> 
	                                                                                                            </itemtemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <%--<column:21>--%>    
                                                                                                          <asp:BoundField DataField="bt_denial" ItemStyle-Width="85px" HeaderText="bt_denial"  Visible="false">	
			                                                                                                <itemstyle horizontalalign="Left"></itemstyle>
			                                                                                              </asp:BoundField>
			                                                                                                <%--<column:22>--%>
                                                                                                            <asp:BoundField DataField="bt_verification"   Visible="false" ItemStyle-Width="105px" HeaderText="bt_verification">	
	                                                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                                                            </asp:BoundField>
                                                                                                            <%--<column:23>--%>
                                                                                                            <asp:BoundField DataField="bt_payment"   Visible="false" ItemStyle-Width="105px" HeaderText="bt_payment">	
	                                                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                                                            </asp:BoundField>
                                                                                                             <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"  headertext="Status" DataField="SZ_STATUS" />
                                                                                                        </Columns>
                                                                                                    </xgrid:XGridViewControl>
                                                                                                 </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="center">
                                                                                                    <asp:Button runat="server" Text="Update" ID="btnUpdate"   Visible="true"  OnClick="btnUpdate_Click"/>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                    </contenttemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlUploadFile" runat="server" Style="width: 450px; height: 0px; background-color: white;
        border-color: ThreeDFace; border-width: 1px; border-style: solid; visibility: hidden;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="CloseUploadFilePopup();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="width: 98%;" valign="top">
                    <table border="0" class="ContentTable" style="width: 100%">
                        <tr>
                            <td>
                                Upload Report :
                            </td>
                            <td>
                                <asp:FileUpload ID="fuUploadReport" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnUploadFile" runat="server" Text="Upload Report" CssClass="Buttons"
                                    OnClick="btnUploadFile_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
