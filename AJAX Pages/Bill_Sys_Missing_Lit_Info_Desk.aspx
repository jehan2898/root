<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_Missing_Lit_Info_Desk.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Missing_Lit_Info_Desk" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="First" border="0" class="ContentTable" cellpadding="0" cellspacing="0" style="width: 100%; height:100%">
        <tr>
            <td valign="top" style="background-color:White">
 
              <table border="0" cellpadding="0" cellspacing="0" style="border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;
                                                width: 200px; border-bottom: #b5df82 1px solid">
                <tr>
                  <td style="width:100px;">
                   <b>Select parameter</b>
                    <asp:DropDownList ID="ddlIndex" runat="server" Width="125px">
                        <asp:ListItem>--Select--</asp:ListItem>
                        <asp:ListItem Value="Index Number">Index Number</asp:ListItem>
                        <asp:ListItem Value="Purchase Date">Purchase Date</asp:ListItem>
                        <asp:ListItem Value="Lawfirm Id">Lawfirm Case Id</asp:ListItem>
                    </asp:DropDownList>
                    
                 </td>
                 <td style="width: 950px;" valign="bottom">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                          <ContentTemplate>
                            <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_onclick" runat="server"/>
                             <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePanel2">
                        <ProgressTemplate>

                        <div id="Div2" style="position:absolute;left:660px;top:100px;z-index:1; text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                          runat="Server">
                         <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                           Height="25px" Width="24px"></asp:Image>
                          Loading...</div>
                        </ProgressTemplate>
                        </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                 </td>
               </tr>
               </table>
               <table>
               <tr>
                <td style="width: 1017px;" colspan="5">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="vertical-align: middle; width: 100%" id="tblSrch">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: middle; width: 30%" align="left">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Search:" Font-Size="Small"></asp:Label>
                                            <gridsearch:XGridSearchTextBox
                                                ID="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input">
                                            </gridsearch:XGridSearchTextBox>
                                        </td>
                                        <td style="vertical-align: middle; width: 30%" align="left">
                                         <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePanel1">
                        <ProgressTemplate>

                        <div id="Div1" style="position:absolute;left:660px;top:100px;z-index:1; text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                          runat="Server">
                         <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                           Height="25px" Width="24px"></asp:Image>
                          Loading...</div>
                        </ProgressTemplate>
                        </asp:UpdateProgress>
                        
                       
                                        </td>
                                        <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">
                                            Record Count:<%= this.grdMissingInfoDesk.RecordCount%>
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
                            <xgrid:XGridViewControl 
                                ID="grdMissingInfoDesk" 
                                runat="server" Height="148px" Width="100%"
                                CssClass="mGrid" 
                                ExportToExcelFields="SZ_BILL_NUMBER,Case_no,SPECIALITY,PROC_DATE,DT_BILL_DATE,SZ_BILL_STATUS_NAME,Bill,Paid,Outstanding,SZ_INDEX_NUMBER,SZ_LAW_FIRM_CASE_ID,DT_PURCHASED_DATE"
                                ExportToExcelColumnNames="Bill#,Case#,Specialty,Visit Date,Bill Date,Bill Status,Bill Amount,Paid,Balance,Index #,LawFirm Case #,Purchase Date"
                                AutoGenerateColumns="false" 
                                MouseOverColor="0, 153, 153" 
                                ExcelFileNamePrefix="ExcelInvoice_Report"
                                ShowExcelTableBorder="true" 
                                EnableRowClick="false" 
                                ContextMenuID="ContextMenu1"
                                HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                AllowPaging="true" GridLines="None" 
                                XGridKey="MissingInfo_Desk" 
                                DataKeyNames=""
                                PageRowCount="50" PagerStyle-CssClass="pgr" AllowSorting="true">
                                <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                <Columns>
                                    <%--  0--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                        visible="false" headertext="Case ID" DataField="Case_Id" />
                                    <%-- 1--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                        visible="false" headertext="BillNo" DataField="SZ_BILL_NUMBER" />
                                    <%--  2--%>
                                  <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                        visible="true" headertext="Bill#" DataField="SZ_BILL_NUMBER" SortExpression="SZ_BILL_NUMBER"  />
                                    <%--CAST(MCM.SZ_CASE_NO as int)"--%>
                                    <%--  3--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                        SortExpression="MST_CASE_MASTER.SZ_CASE_NO " headertext="Case#" DataField="Case_no" />
                                    <%--  4--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                        SortExpression="(select SZ_PROCEDURE_GROUP from MST_PROCEDURE_GROUP 
                                    where SZ_PROCEDURE_GROUP_ID =
                                    (select SZ_PROCEDURE_GROUP_ID  from MST_PROCEDURE_CODES where SZ_PROCEDURE_ID=
			                        (select top 1 SZ_PROCEDURE_ID from TXN_BILL_TRANSACTIONS_DETAIL  where SZ_BILL_NUMBER=TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER )) )" headertext="Specialty" DataField="SPECIALITY" />
                                    <%--  5--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                        SortExpression="convert(nvarchar(10),TXN_BILL_TRANSACTIONS.DT_FIRST_VISIT_DATE,101)	+ '-'+ CONVERT(nvarchar(10), TXN_BILL_TRANSACTIONS.DT_LAST_VISIT_DATE,101)"
                                        headertext="Visit Date" DataField="PROC_DATE" />
                                    <%--  6--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                        SortExpression="DT_BILL_DATE" headertext="Bill Date" DataField="DT_BILL_DATE" />
                                    <%--  7--%>
                                    <%--<asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                        SortExpression="MBS.SZ_BILL_STATUS_NAME" headertext="Status" DataField="SZ_BILL_STATUS_NAME" />--%>
                                    <asp:TemplateField HeaderText="Bill Status" SortExpression="SZ_BILL_STATUS_NAME">
                                        <itemtemplate>
                                                    <asp:Label    id="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_STATUS_NAME")%>'></asp:Label>
                                                </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                        SortExpression="(select SZ_OFFICE from MST_OFFICE where SZ_OFFICE_ID = (select SZ_OFFICE_ID from MST_DOCTOR where SZ_DOCTOR_ID=TXN_BILL_TRANSACTIONS.SZ_DOCTOR_ID) )"    headertext="Provider" DataField="Office_Name" />
                                    <%--  8 --%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                        SortExpression="TBT.FLT_WRITE_OFF" headertext="Write Off" DataFormatString="{0:C}"
                                        DataField="FLT_WRITE_OFF" visible="false" />
                                    <%--  9 --%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                        SortExpression="cast(TXN_BILL_TRANSACTIONS.FLT_BILL_AMOUNT as numeric(13,2))" headertext="Bill Amount"
                                        DataFormatString="{0:C}" DataField="Bill" />
                                    <%--  10--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                        SortExpression="cast(TXN_BILL_TRANSACTIONS.FLT_BILL_AMOUNT as numeric(13,2)) - cast(TXN_BILL_TRANSACTIONS.FLT_BALANCE as numeric(13,2))"
                                        headertext="Paid" DataFormatString="{0:C}" DataField="Paid" />
                                    <%--  11--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                        SortExpression="cast(TXN_BILL_TRANSACTIONS.FLT_BALANCE as numeric(13,2))" headertext="Outstanding" DataFormatString="{0:C}"
                                        DataField="Outstanding" />
                                    <%--  12--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                        SortExpression="SZ_INDEX_NUMBER" headertext="Index #" 
                                        DataField="SZ_INDEX_NUMBER" />
                                    <%--  13--%>
                                    <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                        SortExpression="SZ_LAW_FIRM_CASE_ID" headertext="LawFirm Case #" 
                                        DataField="SZ_LAW_FIRM_CASE_ID" />
                                    <%--  14--%>
                                 <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                        SortExpression="DT_PURCHASED_DATE" headertext="Purchase Date" 
                                        DataField="DT_PURCHASED_DATE" />
                                    <%--  15--%>
                                    
                                        
                                 
                                    <%--  16--%>
                                  
                                    <%--  17--%>
                                   
                                  
                                    <%--  18--%>
                                  
                                    <%--  19--%>
                                  
                                    <%--  19--%>
                                  
                                    <%--  20--%>
                                    
                                </Columns>
                            </xgrid:XGridViewControl>
                        </ContentTemplate>
                       <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" /> 
                     </Triggers> 
                    </asp:UpdatePanel>
                    <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
                     <asp:TextBox ID="txtIndex" runat="server" Visible="false"></asp:TextBox>
                </td>
               </tr>
    </table>
            </td>
        </tr>
    </table>
    
</asp:Content>