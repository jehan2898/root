<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    CodeFile="~/AJAX Pages/Bill_Sys_DeskInFo.aspx.cs" AutoEventWireup="true" Inherits="AJAX_Pages_Bill_Sys_DeskInFo"
    Title="Green Your Bills - Provider Summary" %>

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
        function SelectAll(ival) {

            var f = document.getElementById("<%= grdBillSearch.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    f.getElementsByTagName("input").item(i).checked = ival;
                }

            }
        }

        function checkSelected() {
            var f = document.getElementById("<%= grdBillSearch.ClientID %>");
            var str = 1;
            var iflag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked) {
                        iflag = true;
                        break;
                    }
                }

            }

            if (iflag == false) {
                alert('Please select record.');
                return false;
            }
            else {
                return true;
            }
        }
        function checkSelectedAll() {

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
                                    <td align="left" valign="middle" bgcolor="#b5df82" class="txt2" style="height: 3em">
                                        <b class="txt3" valign="middle">Account Summary</b>
                                    </td>
                                    <td align="center">
                                        <asp:UpdatePanel ID="UpdatePanellbl" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblInsName" runat="server" Font-Bold="true" Font-Size="12Px" Visible="false"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: center; vertical-align: top;">
                                        <asp:UpdatePanel ID="panel1" runat="server">
                                            <ContentTemplate>
                                                <xgrid:XGridViewControl ID="grdLitigationCompanyWise" runat="server" CssClass="mGrid"
                                                    AutoGenerateColumns="false" MouseOverColor="0, 153, 153" ExcelFileNamePrefix="ExcelLitigation"
                                                    ShowExcelTableBorder="true" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                                    HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                    AllowPaging="true" GridLines="None" XGridKey="Bill_Sys_DeskInfo" PageRowCount="10"
                                                    PagerStyle-CssClass="pgr" DataKeyNames="OFF_ID,SZ_OFFICE" AllowSorting="true"
                                                    OnRowCommand="grdLitigationCompanyWise_RowCommand">
                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:BoundField DataField="SZ_OFFICE" HeaderText="Title">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Amount($)" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" Font-Size="small" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.total")%>'></asp:Label>
                                                                <asp:LinkButton ID="lnkAmt" runat="server" Font-Underline="false" Text='<%# DataBinder.Eval(Container,"DataItem.total")%>'
                                                                    CommandName="ShowBills" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="OFF_ID" HeaderText="Title" Visible="false">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width: 50%;" align="right" valign="top">
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;" align="right" valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <xgrid:XGridViewControl ID="grdCaseCount" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                                    MouseOverColor="0, 153, 153" ExcelFileNamePrefix="" ShowExcelTableBorder="true"
                                                    EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                    AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="true" GridLines="None" XGridKey="Bill_Sys_DeskInfo_Count"
                                                    PageRowCount="10" PagerStyle-CssClass="pgr" DataKeyNames="id" AllowSorting="true"
                                                    OnRowCommand="grdCaseCount_RowCommand">
                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                    <Columns>
                                                        <asp:BoundField DataField="Title" HeaderText="Title">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Count" SortExpression="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkcount" runat="server" Font-Underline="false" Text='<%# DataBinder.Eval(Container,"DataItem.Count")%>'
                                                                    CommandName="ShowBills" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="id" HeaderText="id" Visible="false">
                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Sum" HeaderText="Sum($)">
                                                            <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 100%">
                                        <dx:ASPxGridView ID="grdProvider" runat="server" KeyFieldName="OFF_ID" AutoGenerateColumns="false"
                                            Width="100%" OnRowCommand="grdProvider_RowCommand">
                                            <Styles>
                                                <Header BackColor="#b5df82">
                                                </Header>
                                            </Styles>
                                            <Columns>
                                                <dx:GridViewDataColumn FieldName="OFF_ID" Caption="ProviderID" Width="200px" Visible="false">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="SZ_OFFICE" Caption="Provider" Width="200px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Total" Caption="Total Transferred" Width="20px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="recovered" Caption="Total Recovered($)" Width="20px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="recovered_per" Caption="Total Recovered(%)" Width="20px"
                                                    Visible="false">
                                                </dx:GridViewDataColumn>
                                            
                                                <%--<dx:GridViewDataColumn Caption="Select" Settings-AllowSort="False">
                                     <DataItemTemplate>
                                    
                                        <asp:LinkButton ID="lnkAmt" runat="server"  Font-Underline="false" Text='<%# DataBinder.Eval(Container,"DataItem.recovered_per")%>' CommandName='<%# DataBinder.Eval(Container,"DataItem.OFF_ID")%>' CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  ></asp:LinkButton>
                                    </DataItemTemplate>
                                    </dx:GridViewDataColumn>--%>
                                                <dx:GridViewDataColumn Caption="Total Recovered(%)" Settings-AllowSort="False" Width="20px">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="lnkAmt" runat="server" Font-Underline="false" Text='<%# DataBinder.Eval(Container,"DataItem.recovered_per")%>'></asp:LinkButton>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                </dx:GridViewDataColumn>

                                                    <%--<dx:GridViewDataColumn FieldName="bill" Caption="BillAmount($)" Width="20px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="paid" Caption="Paid($)" Width="20px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="wf" Caption="Write off($)" Width="20px">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="bal" Caption="Balance($)" Width="20px">
                                                </dx:GridViewDataColumn>--%>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--<dx:ASPxGridView ID="grdProvider" runat="server" KeyFieldName="id" AutoGenerateColumns="false" Width="100%">
                            <Styles>
                            <Header BackColor="#b5df82"></Header>
                            </Styles>
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="GB_Case_ID" Caption="ProviderWise Summary" Width="60px"></dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="Total Transferred" Width="60px"></dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn FieldName="recovered" Caption="Total Recovered($)" Width="60px"></dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn FieldName="recovered" Caption="Total Recovered(%)" Width="60px"></dx:GridViewDataColumn>
                                </Columns>
                            </dx:ASPxGridView>--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%; vertical-align: top;">
                                        <tbody>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:UpdateProgress ID="UpdateProgress123" runat="server" DisplayAfter="0">
                                                        <ProgressTemplate>
                                                            <asp:Image ID="Updateimg" Style="position: absolute; z-index: 1; top: 50%; left: 50%"
                                                                runat="server" src="Images/loading_transp.gif"></asp:Image>
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
                                                <td style="vertical-align: middle; width: 85%" align="left">
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Search:" Font-Size="Small"></asp:Label>
                                                    <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="True"
                                                        CssClass="search-input">
                                                    </gridsearch:XGridSearchTextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Total Outstanding($):"
                                                        Font-Size="Small"></asp:Label>
                                                    <asp:TextBox ID="txtSum" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                                                    <asp:Button ID="btnselect" Text="Select" runat="server" OnClick="btnselect_Click" />&nbsp;<asp:Button
                                                        ID="btnSelectAll" Text="Select All" runat="server" OnClick="btnSelectAll_Click" />&nbsp;
                                                    <%--<asp:Button ID="btnsold" Visible="false" Text="Mark as sold" runat="server" OnClick="btnsold_Click" />&nbsp;<asp:Button
                                                        ID="btnloaned" Visible="false" Text="Mark as loaned" runat="server" OnClick="btnloaned_Click" />
                                                    <asp:Button ID="btnRevert" Text="Revert" runat="server" OnClick="btnRevert_Click"
                                                        Visible="false" />--%>
                                                </td>
                                                <td style="vertical-align: middle; width: 15%; text-align: right" align="right" colspan="2">
                                                    Record Count:<%= this.grdBillSearch.RecordCount %>| Page Count:
                                                    <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                    </gridpagination:XGridPaginationDropDown>
                                                    <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                        Text="Export TO Excel">
                                                                                <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <xgrid:XGridViewControl ID="grdBillSearch" runat="server" Width="100%" CssClass="mGrid"
                                        ExportToExcelFields="SZ_CASE_NO,SZ_BILL_NUMBER,PATIENT_NAME,INS_NAME,FLT_BILL_AMOUNT,PAID_AMOUNT,FLT_BALANCE,SZ_BILL_STATUS_NAME,SPECIALITY,PROC_DATE,Transfer_Date,Index_No,LAW_FIRM_CASE_ID,Purchase_Date,sz_location_name"
                                        ExportToExcelColumnNames="Case#,BillNo,Patient Name,Insurance Company,Bill Amount,Paid,Write Off,Outstanding,status,specialty,Visit Date,Transfer Date,Index Number,Law Firm  ID,Purchase Date,Provider"
                                        AutoGenerateColumns="false" MouseOverColor="0, 153, 153" ExcelFileNamePrefix="BillSearch"
                                        ShowExcelTableBorder="true" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                        HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                        AllowPaging="true" GridLines="None" XGridKey="Bill_Sys_Desk_Office_Info" DataKeyNames="FLT_BALANCE,SZ_BILL_NUMBER"
                                        PageRowCount="50" PagerStyle-CssClass="pgr" AllowSorting="true">
                                        <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                        <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                        <Columns>
                                            <%--  0--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                Visible="false" HeaderText="Case ID" DataField="SZ_CASE_ID" />
                                            <%-- 1--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                HeaderText="Case#" DataField="SZ_CASE_NO" SortExpression="MCM.SZ_CASE_NO" />
                                            <%-- 2--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                HeaderText="BillNo" DataField="SZ_BILL_NUMBER" SortExpression="TBT.SZ_BILL_NUMBER" />
                                            <%-- 3--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                HeaderText="Patient Name" DataField="PATIENT_NAME" SortExpression="MP.SZ_PATIENT_FIRST_NAME" />
                                            <%-- 4--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                HeaderText="Insurance Company" DataField="INS_NAME" SortExpression="MIC.SZ_INSURANCE_NAME" />
                                            <%--  5 --%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                SortExpression="CAST(TBT.FLT_BILL_AMOUNT  as float)" HeaderText="Bill Amount"
                                                DataFormatString="{0:C}" DataField="FLT_BILL_AMOUNT" />
                                            <%--  6--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                SortExpression="CAST(ISNULL((SELECT SUM(FLT_CHECK_AMOUNT) FROM TXN_PAYMENT_TRANSACTIONS WHERE SZ_BILL_ID=TBT.SZ_BILL_NUMBER),0)as float)"
                                                HeaderText="Paid" DataFormatString="{0:C}" DataField="PAID_AMOUNT" />
                                            <%--  7 --%>
                                           <%-- <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                SortExpression="CAST(TBT.FLT_WRITE_OFF  as float)" HeaderText="Write Off" DataFormatString="{0:C}"
                                                DataField="FLT_WRITE_OFF" />--%>
                                            <%--  8--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                SortExpression="CAST(TBT.FLT_BALANCE as float)" HeaderText="Outstanding" DataFormatString="{0:C}"
                                                DataField="FLT_BALANCE" />
                                            <%-- 9--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                HeaderText="status" DataField="SZ_BILL_STATUS_NAME" SortExpression="MBS.SZ_BILL_STATUS_NAME" />
                                            <%-- 10--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                HeaderText="specialty" DataField="SPECIALITY" SortExpression="(select SZ_PROCEDURE_GROUP from MST_PROCEDURE_GROUP where SZ_PROCEDURE_GROUP_ID =(select SZ_PROCEDURE_GROUP_ID  from MST_PROCEDURE_CODES where SZ_PROCEDURE_ID=(select top 1 SZ_PROCEDURE_ID from TXN_BILL_TRANSACTIONS_DETAIL  where SZ_BILL_NUMBER=TBT.SZ_BILL_NUMBER )) )" />
                                            <%--11--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                SortExpression="convert(nvarchar(10),TBT.DT_FIRST_VISIT_DATE,101)	+ '-'+ CONVERT(nvarchar(10), TBT.DT_LAST_VISIT_DATE,101)"
                                                HeaderText="Visit Date" DataField="PROC_DATE" />
                                            <%--12--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                SortExpression="convert(nvarchar(10),tbt.dt_transferred_on,101)" HeaderText="Transfer Date"
                                                DataField="Transfer_Date" />
                                            <%--13--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                SortExpression="isnull(tbt.SZ_INDEX_NUMBER,'')" HeaderText="Index Number" DataField="Index_No" />
                                            <%--14--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                SortExpression="isnull(tbt.SZ_LAW_FIRM_CASE_ID,'')" HeaderText="Law Firm  ID"
                                                DataField="LAW_FIRM_CASE_ID" />
                                            <%--15--%>
                                            <asp:TemplateField HeaderText="Purchase Date" SortExpression="tbt.DT_PURCHASED_DATE">
                                                <ItemTemplate>
                                                    <%# CheckNull(Eval("Purchase_Date")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--16--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                HeaderText="Provider" DataField="sz_location_name" />
                                            <%--17--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                SortExpression="CONVERT(NVARCHAR(20),TXN_BILL_TRANSACTIONS.DT_TRIAL,101)" HeaderText="Trial Date"
                                                DataField="DT_TRIAL" DataFormatString="{0:MM-dd-yyyy}" />
                                            <%--18--%>
                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                SortExpression="isnull(TXN_BILL_TRANSACTIONS.SZ_LAWFIRM_STATUS,'''')" HeaderText="Lawfirm Status"
                                                DataField="SZ_LAWFIRM_STATUS" />
                                            <%--19--%>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                        ToolTip="Select All" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkDelete" runat="server" />
                                                </ItemTemplate>
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
                <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtLoginCompanyId" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtOfficeID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtFlag" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtSubFlag" runat="server" Visible="false"></asp:TextBox>
                <asp:HiddenField ID="hselectVlaue" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
