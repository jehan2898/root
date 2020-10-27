<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_ViewBillRecordSpeciality.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_ViewBillRecordSpeciality"
    AsyncTimeout="10000" Title="Green Your Bills - Spcialty Report" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>
    <style type="text/css">
        .HiddenColumn {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function OpenDocManager(CaseNo, CaseId) {

            window.open('../Document Manager/case/vb_CaseInformation.aspx?caseid=' + CaseId + '&caseno=' + CaseNo, 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');

        }
        function OpenPage(BillNo) {

            window.open('Bill_Sys_OpenBill.aspx?bno=' + BillNo, 'AdditionalData', 'width=800,height=600,left=30,top=30,scrollbars=1');

        }
        function SelectAll(ival) {

            var f = document.getElementById("<%= grdBillReportDetails.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    f.getElementsByTagName("input").item(i).checked = ival;
                }

            }
        }
        function showviewBills() {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.height = '180px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.visibility = 'visible';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.position = "absolute";
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.top = '250px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.left = '400px';
        }

        function CloseviewBills() {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.height = '0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.visibility = 'hidden';
        }

        function CheckVal() {
            var f = document.getElementById("<%=grdBillReportDetails.ClientID%>");
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkUpdateStatus') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            bfFlag = true;


                        }

                    }
                }
            }

            if (bfFlag == false) {
                alert('Please select record.');
                return false;
            }
            else {
                if (document.getElementById("ctl00_ContentPlaceHolder1_extddlBillStatus").value == 'NA') {
                    alert('Please select bill status.');
                    return false;
                }
                return true;
            }
        }

        //Function For POM Conformation
        function POMConformation() {
            if (Check()) {
                document.getElementById('div1').style.zIndex = 1;
                document.getElementById('div1').style.position = 'absolute';
                document.getElementById('div1').style.left = '360px';
                document.getElementById('div1').style.top = '250px';
                document.getElementById('div1').style.visibility = 'visible';
                return false;
            }
            else {
                return false;
            }
        }

        function Check() {
            var bills = "";
            var bInvalidBills;
            var f = document.getElementById("<%=grdBillReportDetails.ClientID%>");
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkUpdateStatus') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                        if (f.getElementsByTagName("input").item(i).checked != false) {
                            bfFlag = true;
                            if (f.getElementsByTagName("input").item(i).getAttribute('billid')) {
                                bInvalidBills = true;
                                if (bills == '') {
                                    bills = f.getElementsByTagName("input").item(i).getAttribute('billid');

                                } else {
                                    bills = bills + " , " + f.getElementsByTagName("input").item(i).getAttribute('billid')
                                }
                            }

                        }
                        
                        

                    }
                }
            }

            if (bfFlag == false) {
                alert('Please select record.');
                return false;
            }
            if (bInvalidBills == true) {

                alert( "Bills " + bills + " don't have Bill document in System Please Regenerate");
                return false;

            }


            return true;
        }

        function YesMassage() {
            //document.getElementById("<%= hdnPOMValue.ClientID%>").value="Yes";           
            document.getElementById('div1').style.visibility = 'hidden';
        }
        function NoMassage() {
            //document.getElementById("<%= hdnPOMValue.ClientID%>").value="No";         
            document.getElementById('div1').style.visibility = 'hidden';
        }
    </script>

    <table width="100%" style="background-color: white">
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" Visible="false" Style="color: Red;"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top">

                <table width="70%">
                    <tr>
                        <td>

                            <table style="width: 100%; border-bottom: #b5df82 1px solid; border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;">
                                <tr>
                                    <td style="background-color: #b4dd82; height: 15%; font-weight: bold; font-size: small">&nbsp;Bills
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
                                                <td style="width: 77%">Bill Status&nbsp;
                                                    <cc1:ExtendedDropDownList ID="extddlBillStatus" runat="server" Width="170px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_STATUS_LIST_NOT_TRF_DNL" Procedure_Name="SP_MST_BILL_STATUS"
                                                        Selected_Text="---Select---" />
                                                    &nbsp;
                                                  <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" OnClick="btnUpdateStatus_Click" />
                                                    &nbsp;&nbsp;
                                                    
                                                      <asp:Button ID="btnCreatePacket" runat="server" Text="Create Packet" OnClick="btnCreatePacket_Click" />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnPrintEnvelop"  Visible="false" runat="server" Text="Print Envelop" OnClick="btnPrintEnvelop_Click" />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnPrintPOM" Visible="false" runat="server" Text="Print POM" />
                                                    <asp:TextBox ID="txtSearchOrder" runat="server" Visible="false" Text=""></asp:TextBox>
                                                    <asp:Button ID="btn_BillPacket" runat="server" Text="Bill Packet" OnClick="btn_BillPacket_Click"></asp:Button>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_PacketDocument" runat="server" Text="Packet Document" OnClick="btn_PacketDocument_Click" Visible="false"></asp:Button>
                                                    <asp:Button ID="btn_Both" runat="server" Text="Both" OnClick="btn_Both_Click" Visible="false"></asp:Button>
                                                </td>
                                                <%--<td style="width: 20%" valign="top">
                                                    <asp:RadioButtonList ID="rdbpombills" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="&nbsp;Bills" Value="0" Selected="True">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="&nbsp;Other" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>--%>
                                            </tr>
                                        </table>
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
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; border-bottom: #b5df82 1px solid; border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;">
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="3" align="right">
                                                <asp:Button ID="btnDownloadPackets" runat="server" Text="Download Packets" OnClick="btnDownloadPackets_Click" OnClientClick="aspnetForm.target ='_blank'" />
                                                &nbsp;&nbsp;
                                        <asp:Button ID="btnExportData" runat="server" OnClick="btnExportData_Click" Text="Export Data" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Search:
                                                <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                    CssClass="search-input"></gridsearch:XGridSearchTextBox>
                                            </td>
                                            <td>
                                                <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DivStatus4" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                            runat="Server">
                                                            <asp:Image ID="img4" runat="server" ImageUrl="Images/rotation.gif" AlternateText="Loading....."
                                                                Height="25px" Width="24px"></asp:Image>
                                                            Loading...
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                            <td align="right">Record Count:<%= this.grdBillReportDetails.RecordCount%>| Page Count:
                                                <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                </gridpagination:XGridPaginationDropDown>
                                                <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                    Text="Export TO Excel">
                                 <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>

                                            </td>
                                        </tr>
                                    </table>

                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <xgrid:XGridViewControl ID="grdBillReportDetails" runat="server" Width="100%" CssClass="mGrid"
                                                    MouseOverColor="0, 153, 153" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                                    HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                    ShowExcelTableBorder="true" ExportToExcelColumnNames="ROWID,Bill #,Case #,Patient Name,Specialty,Provider,Bill Amount,Paid Amount,Write Off Amount,Balance,Visit Date,Bill Date,Referring Doctor,Insurance Name, Insurance Claim Number,Accident Date,Bill Status Date,Status,UserName"
                                                    ExportToExcelFields="ROWID,SZ_BILL_ID, SZ_CASE_NO, SZ_PATIENT_NAME, SZ_SPECIALITY,SZ_PROVIDER,SZ_BILL_AMOUNT,PAID_AMOUNT,FLT_WRITE_OFF,FLT_BALANCE,PROC_DATE,DT_BILL_DATE,SZ_REFFERING_DOC_NAME,SZ_INSURANCE_NAME,CLAIM_NO,DT_ACCIDENT_DATE,DT_BILL_STATUS_DATE,SZ_STATUS,SZ_USERNAME"
                                                    DataKeyNames="SZ_BILL_ID, SZ_CASE_ID, SZ_CASE_NO, SZ_CASE_TYPE, CLAIM_NO,SZ_INSURANCE_NAME,SZ_INSURANCE_ADDRESS,MIN_DATE_OF_SERVICE,MAX_DATE_OF_SERVICE,SZ_SPECIALITY"
                                                    AllowPaging="true" XGridKey="Bill_Sys_View_Bill_Record_Speciality" PageRowCount="200" PagerStyle-CssClass="pgr" AllowSorting="true"
                                                    AutoGenerateColumns="false" GridLines="None" OnRowDataBound="grdBillReportDetails_RowDataBound">
                                                    <Columns>
                                                        <%-- 0 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="SZ_CASE_ID" DataField="SZ_CASE_ID" Visible="false" />
                                                        <%-- 1 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="SZ_PATIENT_ID" DataField="SZ_PATIENT_ID" Visible="false" />
                                                        <%-- 2 --%>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);" ToolTip="Select All" />
                                                            </HeaderTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkUpdateStatus" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- 3 --%>
                                                        <xlink:XGridHyperlinkField SortExpression="convert(int,substring(TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER,3,len(TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)))"
                                                            HeaderText="Bill#" DataNavigateUrlFields="SZ_CASE_ID,SZ_BILL_ID" DataNavigateUrlFormatString="Bill_Sys_BillTransaction.aspx?Type=Search&CaseID={0}&bno={1}"
                                                            DataTextField="SZ_BILL_ID">
                                                        </xlink:XGridHyperlinkField>
                                                        <%-- 4 --%>

                                                        <asp:TemplateField HeaderText="Case #">
                                                            <ItemTemplate>
                                                                <a href="javascript:OpenDocManager('<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%> ','<%# DataBinder.Eval(Container, "DataItem.SZ_CASE_ID")%> ');"><%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%> </a>
                                                                <%--<asp:LinkButton ID="lnkDocumentManager" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                                    CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="Document Manager"></asp:LinkButton>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--<asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Case #" DataField="SZ_CASE_NO" SortExpression="MST_CASE_MASTER.SZ_CASE_NO" />--%>
                                                        <%-- 5 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="SZ_BILL_ID" DataField="SZ_BILL_ID" Visible="false" />
                                                        <%-- 6 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Patient Name" DataField="SZ_PATIENT_NAME" SortExpression="MST_PATIENT.SZ_PATIENT_LAST_NAME" />
                                                        <%-- 7 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Spciality" DataField="SZ_SPECIALITY" SortExpression="MST_PROCEDURE_GROUP.SZ_PROCEDURE_GROUP" />
                                                        <%-- 8 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Provider" DataField="SZ_PROVIDER" SortExpression="" />
                                                        <%-- 9 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            HeaderText="Bill Amount" DataField="SZ_BILL_AMOUNT" SortExpression="cast(ISNULL(FLT_BILL_AMOUNT,0) as numeric(13,2))" DataFormatString="{0:C}" />
                                                         <%-- 10 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            HeaderText="Paid Amount" DataField="PAID_AMOUNT" SortExpression="PAID_AMOUNT" DataFormatString="{0:C}"/>
                                                        <%-- 11 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            HeaderText="Write Off Amount" DataField="FLT_WRITE_OFF" SortExpression="FLT_WRITE_OFF" DataFormatString="{0:C}"/>
                                                        <%-- 12 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                            HeaderText="Balance" DataField="FLT_BALANCE" SortExpression="FLT_BALANCE" DataFormatString="{0:C}"/>                                                
                                                        <%-- 13 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Visit Date" DataField="PROC_DATE" SortExpression="convert(nvarchar(10),TXN_BILL_TRANSACTIONS.DT_FIRST_VISIT_DATE,101)" />
                                                        <%-- 14 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Bill Date" DataField="DT_BILL_DATE" SortExpression="TXN_BILL_TRANSACTIONS.DT_BILL_DATE" DataFormatString="{0:MMM-dd-yyyy}" />
                                                        <%-- 15 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Referring Doctor" DataField="SZ_REFFERING_DOC_NAME" SortExpression="" />
                                                        <%-- 16 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Insurance Name" DataField="SZ_INSURANCE_NAME" SortExpression="SZ_INSURANCE_NAME" />
                                                        <%-- 17 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Insurance Claim Number" DataField="CLAIM_NO" SortExpression="MST_CASE_MASTER.SZ_CLAIM_NUMBER" DataFormatString="{0:MMM-dd-yyyy}" />
                                                        <%-- 18 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Accident Date" DataField="DT_ACCIDENT_DATE" SortExpression="MST_CASE_MASTER.DT_DATE_OF_ACCIDENT" DataFormatString="{0:MMM-dd-yyyy}" />
                                                        <%-- 19 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Bill Status Date" DataField="DT_BILL_STATUS_DATE" SortExpression="TXN_BILL_TRANSACTIONS.DT_BILL_STATUS_DATE" DataFormatString="{0:MMM-dd-yyyy}" />
                                                        <%-- 20 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Status" DataField="SZ_STATUS" SortExpression="" />
                                                        <%-- 21 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="Username" DataField="SZ_USERNAME" SortExpression="" />
                                                        <%-- 22 --%>
                                                        <asp:TemplateField HeaderText="" Visible="true">
                                                            <ItemTemplate>
                                                                <img alt="" onclick="javascript:OpenPage('<%# DataBinder.Eval(Container, "DataItem.SZ_BILL_ID")%> ');" src="../Images/123.gif" title="view" style="cursor: pointer;" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="">
                                                                <itemtemplate>
                                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                                            CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                                            </itemtemplate>
                                                            </asp:TemplateField>--%>
                                                        <%-- 23 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="SZ_CASE_ID" DataField="SZ_CASE_ID" Visible="false" />
                                                        <%-- 24 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="SZ_PATIENT_ID" DataField="SZ_PATIENT_ID"
                                                            Visible="false" />
                                                        <%-- 25 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="SZ_BILL_ID" DataField="SZ_BILL_ID" Visible="false" />
                                                        <%-- 26 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="SZ_INSURANCE_NAME" DataField="SZ_INSURANCE_NAME"
                                                            Visible="false" />
                                                        <%-- 27 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="SZ_INSURANCE_ADDRESS" DataField="SZ_INSURANCE_ADDRESS"
                                                            Visible="false" />
                                                        <%-- 28 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="CLAIM_NO" DataField="CLAIM_NO" Visible="false" />
                                                        <%-- 29 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="MIN_DATE_OF_SERVICE" DataField="MIN_DATE_OF_SERVICE"
                                                            Visible="false" />
                                                        <%-- 30 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="MIN_DATE_OF_SERVICE" DataField="MIN_DATE_OF_SERVICE"
                                                            Visible="false" />
                                                        <%-- 31 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="MAX_DATE_OF_SERVICE" DataField="MAX_DATE_OF_SERVICE"
                                                            Visible="false" />
                                                        <%-- 32 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="SZ_CASE_TYPE" DataField="SZ_CASE_TYPE" Visible="false" />
                                                        <%-- 33 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="WC_ADDRESS" DataField="WC_ADDRESS">
                                                            <ItemStyle CssClass="HiddenColumn" />
                                                            <HeaderStyle CssClass="HiddenColumn" />
                                                        </asp:BoundField>
                                                        <%-- 34 --%>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="SZ_SPECIALITY_ID" DataField="SZ_SPECIALITY_ID">
                                                            <ItemStyle CssClass="HiddenColumn" />
                                                            <HeaderStyle CssClass="HiddenColumn" />
                                                        </asp:BoundField>
                                                        <%-- 35 --%>

                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                            HeaderText="SZ_OFFICE_ID" DataField="SZ_OFFICE_ID">
                                                            <ItemStyle CssClass="HiddenColumn" />
                                                            <HeaderStyle CssClass="HiddenColumn" />
                                                        </asp:BoundField>

                                                    </Columns>
                                                </xgrid:XGridViewControl>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnUpdateStatus" />
                        <%-- <asp:AsyncPostBackTrigger ControlID="btnCreatePacket" />--%>
                        <asp:AsyncPostBackTrigger ControlID="btnPrintEnvelop" />
                        <asp:AsyncPostBackTrigger ControlID="btnPrintPOM" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtProcedureGroupId" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtBillStatusId" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtUserID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtFromDate" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtTodate" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtFirstVisitDate" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtlastVisitDate" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtLoactionId" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtBillStatusdate" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtCaseTypeID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtCaseNumber" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtBillNumber" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtPatientName" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtInsuranceId" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div id="div1" style="position: absolute; left: 50%; top: 920px; width: 30%; height: 150px; background-color: White; visibility: hidden; border-right: #b4dd82 2px solid; border-top: #b4dd82 2px solid; border-left: #b4dd82 2px solid; border-bottom: #b4dd82 2px solid; text-align: center;">
        <div style="position: relative; width: 100%; height: 20px; text-align: left; float: left; font-family: Times New Roman; float: left; background-color: #b4dd82; left: 0px; top: 0px;">
            MSG
            
        </div>
        <%--<div style="position: relative; text-align: right; float: left; width: 60%; height: 20px;
            background-color: #8babe4; left: 0px; top: 0px;">
            <a onclick="CancelErrorMassage();" style="cursor: pointer;" title="Close">X</a>
        </div>--%>
        <br />
        <br />
        <div style="top: 50px; width: 90%; font-family: Times New Roman; text-align: center;">
            <span id="Span2" runat="server" style="font-size: 10pt; font-family: "></span>
        </div>
        <br />
        <br />
        <div style="text-align: center;">
            <asp:Button ID="Button1" Style="width: 15%;" runt="server" Text="Ok" OnClick="btnOK_Click" />
            <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click"
                Text="Yes" Width="80px" />
            <asp:Button ID="btnNo" runat="server" OnClick="btnNo_Click"
                Text="No" Width="80px" />
            <%-- <input type="button" class="Buttons" value="Yes" id="Button1" onclick="YesMassage();"
                style="width: 15%;" />
                <input type="button" class="Buttons" value="NO" id="Button3" onclick="NoMassage();"
                style="width: 15%;" />--%>
        </div>
        <asp:HiddenField ID="hdnPOMValue" runat="server" />
    </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <link href="../dist/styles/metro/notify-metro.css" rel="stylesheet" />
    <script src="../dist/notify.js"></script>
    <script src="../dist/styles/metro/notify-metro.js"></script>
    <script>
        function ShowMessage(message, title, style) {
            $.notify({
                title: title,
                text: message,

                image: "<img src='../images/Mail.png'/>"
            }, {
                style: 'metro',
                className: style,
                autoHide: false,
                clickToHide: true
            });
        }
    </script>

</asp:Content>
