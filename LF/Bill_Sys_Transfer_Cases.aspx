<%@ Page Language="C#" MasterPageFile="~/LF/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Transfer_Cases.aspx.cs" Inherits="LF_Bill_Sys_Transfer_Cases"
    Title="Transfer Cases" %>

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
      function SelectAll(ival)
       {
       
            var f= document.getElementById("<%= grdBillSearch.ClientID %>");	
            var str = 1;
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
       
      function   checkSelected()
      {
           
            var f= document.getElementById("<%= grdBillSearch.ClientID %>");	
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
        function checkSelectedAll()
        
        {  
        
        return true;
       }
    </script>

    <table width="100%">
        <tr>
            <td style="width: 100%;">
                <table style="width: 100%; height: 300px; border: 0px solid blue;" class="txt2" align="left"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; border: 0px solid #B5DF82;"
                                border="0">
                                <tr>
                                    <td align="left" style="height: 3em" valign="middle" bgcolor="#b5df82" class="txt2"
                                       >
                                        <b class="txt3" >Company</b>
                                    </td>
                                    <td>
                                    &nbsp;
                                    </td>
                                    <td align="center" style="height: 3em" valign="middle" bgcolor="#b5df82" class="txt2"
                                       >
                                        <b class="txt3">Account Summary</b>
                                    </td>
                                     <td>
                                    &nbsp;
                                    </td>
                                    <td style="height: 3em" valign="bottom" align="center">
                                        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblOffName" Font-Bold="true" Font-Size="Small" runat="server"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%; text-align: center; vertical-align: top;">
                                        <asp:UpdatePanel ID="panel1" runat="server">
                                            <ContentTemplate>
                                                <xgrid:XGridViewControl ID="grdTranferCompanyWise" runat="server" CssClass="mGrid"
                                                    AutoGenerateColumns="false" MouseOverColor="0, 153, 153" ExcelFileNamePrefix="ExcelLitigation"
                                                    ShowExcelTableBorder="true" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                                    HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                    AllowPaging="true" GridLines="None" XGridKey="Bill_Sys_Transfer_Cases" PageRowCount="10"
                                                    PagerStyle-CssClass="pgr" DataKeyNames="SZ_COMPANY_ID" AllowSorting="true" OnRowCommand="grdTranferCompanyWise_RowCommand">
                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:BoundField DataField="SZ_COMPANY_NAME" HeaderText="Company Name">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Sum($)" SortExpression="">
                                                            <itemtemplate>
                                                                <asp:LinkButton  ID="lnkcount" runat="server"  Font-Underline="false" Text='<%# DataBinder.Eval(Container,"DataItem.SUM")%>' CommandName="Sum" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  ></asp:LinkButton>
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                     <td>
                                    &nbsp;
                                    </td>
                                    <td valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <xgrid:XGridViewControl ID="grdLitigationCompanyWise" runat="server" CssClass="mGrid"
                                                    AutoGenerateColumns="false" MouseOverColor="0, 153, 153" ExcelFileNamePrefix="ExcelLitigation"
                                                    ShowExcelTableBorder="true" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                                    HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                    AllowPaging="true" GridLines="None" XGridKey="Bill_Sys_LF_DeskInfo" PageRowCount="10"
                                                    PagerStyle-CssClass="pgr" DataKeyNames="OFF_ID,SZ_OFFICE" AllowSorting="true"
                                                    OnRowCommand="grdLitigationCompanyWise_RowCommand" OnRowDataBound="grdLitigationCompanyWise_RowBound">
                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:BoundField DataField="SZ_OFFICE" HeaderText="">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Amount($)" SortExpression="">
                                                            <itemtemplate>
                                                                <asp:Label id="lblAmount" Font-Size="small" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.total")%>' ></asp:Label>
                                                                <asp:LinkButton ID="lnkAmt" runat="server"  Font-Underline="false" Text='<%# DataBinder.Eval(Container,"DataItem.total")%>' CommandName="ShowBills" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  ></asp:LinkButton>
                                                            </itemtemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="OFF_ID" HeaderText="Title" Visible="false">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                     <td>
                                    &nbsp;
                                    </td>
                                    <td style="width: 50%;" align="right" valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <xgrid:XGridViewControl ID="grdCaseCount" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                                    MouseOverColor="0, 153, 153" ExcelFileNamePrefix="" ShowExcelTableBorder="true"
                                                    EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                    AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="true" GridLines="None" XGridKey="Bill_Sys_LF_DeskInfo_Count"
                                                    PageRowCount="10" PagerStyle-CssClass="pgr" DataKeyNames="id,link" AllowSorting="true"
                                                    OnRowCommand="grdCaseCount_RowCommand">
                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:BoundField DataField="Title" HeaderText="Title">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Count" SortExpression="">
                                                            <itemtemplate>
                                                                <asp:LinkButton  ID="lnkcount1" runat="server"  Font-Underline="false" Text='<%# DataBinder.Eval(Container,"DataItem.Count")%>' CommandName="ShowBills" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  ></asp:LinkButton>
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="id" HeaderText="id" Visible="false">
                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Sum" HeaderText="Sum($)">
                                                            <headerstyle horizontalalign="center"></headerstyle>
                                                            <itemstyle horizontalalign="right"></itemstyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" colspan="5">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%; vertical-align: top;" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="5">
                                                                <asp:UpdateProgress ID="UpdateProgress4" runat="server" DisplayAfter="10"
                                                                  >
                                                                    <ProgressTemplate>
                                                                        <div style="vertical-align: bottom; text-align: left" id="DivStatus5" class="PageUpdateProgress"
                                                                            runat="Server">
                                                                            <asp:Image ID="img5" runat="server" Height="10px" Width="24px" ImageUrl="~/Images/rotation.gif"
                                                                                AlternateText="Downloading....."></asp:Image>
                                                                            Processing wait...</div>
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>
                                                                <asp:UpdatePanel ID="UpdatePane44" runat="server">
                                                                    <ContentTemplate>
                                                                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr id="trSearch" runat="server">
                                                            <td style="vertical-align: middle; " align="left">
                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Search:" Font-Size="Small"></asp:Label>
                                                                <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="True"
                                                                    CssClass="search-input">
                                                                </gridsearch:XGridSearchTextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td style="vertical-align: middle; width: 20%; text-align: right" align="right" colspan="2">
                                                                Record Count:<%= this.grdBillSearch.RecordCount %>
                                                                | Page Count:
                                                                <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                </gridpagination:XGridPaginationDropDown>
                                                                <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                                    Text="Export TO Excel">
                                                                                <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        <td colspan="4" align="right">
                                                        <asp:Button ID="btnRejecte" runat="server" Text="Reject"  OnClick="btnRejecte_Click" />
                                                        <asp:Button ID="btnBatchXls" runat="server" Text="Download Bill Data" OnClick="btnBatchXls_Click"  />
                                                        </td>
                                                        </tr>
                                                        
                                                        
                                                    </tbody>
                                                </table>
                                                <xgrid:XGridViewControl ID="grdBillSearch" runat="server" Width="80%" CssClass="mGrid"
                                                    ExportToExcelFields="SZ_CASE_ID,SZ_BILL_NUMBER,PATIENT_NAME,INS_NAME,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE,SZ_BILL_STATUS_NAME,SPECIALITY,PROC_DATE,Transfer_Date,Index_No,LAW_FIRM_CASE_ID,Purchase_Date,sz_location_name"
                                                    ExportToExcelColumnNames="Case ID,BillNo,Patient Name,Insurance Company,Bill Amount,Paid,Outstanding,status,specialty,Visit Date,Transfer Date,Index Number,Law Firm  ID,Purchase Date,Provider"
                                                    AutoGenerateColumns="false" MouseOverColor="0, 153, 153" ExcelFileNamePrefix="BillSearch"
                                                    ShowExcelTableBorder="true" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                                    HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                    AllowPaging="true" GridLines="None" XGridKey="Bill_Sys_LF_Desk_Office_Info" DataKeyNames="FLT_BALANCE,SZ_BILL_NUMBER"
                                                    PageRowCount="50" PagerStyle-CssClass="pgr" AllowSorting="true" OnRowDataBound="grdBillSearch_RowDataBound">
                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <%--  0--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            visible="true" headertext="Case ID" DataField="SZ_CASE_ID" SortExpression="TBT.SZ_CASE_ID" />
                                                        <%-- 1--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left" visible="false"
                                                            headertext="Case#" DataField="SZ_CASE_NO" SortExpression=" ISNULL(MO.SZ_PREFIX ,'''')  + MCM.SZ_CASE_NO" />
                                                        <%-- 2--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            headertext="BillNo" DataField="SZ_BILL_NUMBER" SortExpression="TBT.SZ_BILL_NUMBER" />
                                                        <%-- 3--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            headertext="Patient Name" DataField="PATIENT_NAME" SortExpression="MP.SZ_PATIENT_FIRST_NAME" />
                                                        <%-- 4--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            headertext="Insurance Company" DataField="INS_NAME" SortExpression="MIC.SZ_INSURANCE_NAME" />
                                                        <%--  5 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            SortExpression="CAST(TBT.FLT_BILL_AMOUNT  as float)" headertext="Bill Amount"
                                                            DataFormatString="{0:C}" DataField="FLT_BILL_AMOUNT" />
                                                        <%--  5--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            SortExpression="CAST(ISNULL((SELECT SUM(FLT_CHECK_AMOUNT) FROM TXN_PAYMENT_TRANSACTIONS WHERE SZ_BILL_ID=TBT.SZ_BILL_NUMBER),0)as float)"
                                                            headertext="Paid" DataFormatString="{0:C}" DataField="PAID_AMOUNT" />
                                                        <%--  7--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            SortExpression="CAST(TBT.FLT_BALANCE as float)" headertext="Outstanding" DataFormatString="{0:C}"
                                                            DataField="FLT_BALANCE" />
                                                        <%-- 8--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            headertext="status" DataField="SZ_BILL_STATUS_NAME" SortExpression="MBS.SZ_BILL_STATUS_NAME" />
                                                        <%-- 9--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            headertext="specialty" DataField="SPECIALITY" SortExpression="(select SZ_PROCEDURE_GROUP from MST_PROCEDURE_GROUP where SZ_PROCEDURE_GROUP_ID =(select SZ_PROCEDURE_GROUP_ID  from MST_PROCEDURE_CODES where SZ_PROCEDURE_ID=(select top 1 SZ_PROCEDURE_ID from TXN_BILL_TRANSACTIONS_DETAIL  where SZ_BILL_NUMBER=TBT.SZ_BILL_NUMBER )) )" />
                                                        <%--10--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                            SortExpression="convert(nvarchar(10),TBT.DT_FIRST_VISIT_DATE,101)	+ '-'+ CONVERT(nvarchar(10), TBT.DT_LAST_VISIT_DATE,101)"
                                                            headertext="Visit Date" DataField="PROC_DATE" />
                                                        <%--11--%>
                                                        
                                                           <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                         SortExpression="convert(nvarchar(10),tbt.dt_transferred_on,101)"
                                                            headertext="Transfer Date" DataField="Transfer_Date" />
                                                            
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                            SortExpression="isnull(tbt.SZ_INDEX_NUMBER,'')" headertext="Index Number" DataField="Index_No" />
                                                        <%--12--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            SortExpression="isnull(tbt.SZ_LAW_FIRM_CASE_ID,'')" headertext="Law Firm  ID"
                                                            DataField="LAW_FIRM_CASE_ID" />
                                                        <%--13--%>
                                                        <%--<asp:TemplateField HeaderText="Purchase Date" SortExpression="tbt.DT_PURCHASED_DATE">
                                                                            <itemtemplate>
                                                                                <%# CheckNull(Eval("Purchase_Date")) %>
                                                                            </itemtemplate>
                                                                           </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            SortExpression="tbt.DT_PURCHASED_DATE" headertext="Purchase Date" DataField="Purchase_Date" DataFormatString="{0:MM-dd-yyyy}"  />
                                                              <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                            headertext="Provider" DataField="sz_location_name" />
                                                                          <%--14--%>     
                                                                      <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                         SortExpression="ISNULL(( select  top 1 sz_batch_name  from TXN_DOWNLOAD_HISTORY  where sz_batch_name is not null and sz_bill_number= TBT.SZ_BILL_NUMBER  order by   dt_download_datetime desc  ),'''') "
                                                            headertext="Batch Name" DataField="DOWNLOAD_BATCH" />        
                                                                            
                                                        <%--15--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            SortExpression="CONVERT(NVARCHAR(20),TXN_BILL_TRANSACTIONS.DT_TRIAL,101)" headertext="Trial Date" DataField="DT_TRIAL" DataFormatString="{0:MM-dd-yyyy}"  />
                                                        <%--16--%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            SortExpression="isnull(TXN_BILL_TRANSACTIONS.SZ_LAWFIRM_STATUS,'''')" headertext="Lawfirm Status" DataField="SZ_LAWFIRM_STATUS"   />
                                                        <%--17--%>   
                                                        <asp:TemplateField HeaderText=""   >
                                                            <headertemplate>
                                                                              <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                                                          </headertemplate>
                                                            <itemtemplate>
                                                                             <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                            </itemtemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchBox" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtCompany" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtLoginCompanyId" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtOfficeID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtFlag" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtSubFlag" runat="server" Visible="false"></asp:TextBox>
                <asp:HiddenField ID="hselectVlaue" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
