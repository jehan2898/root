<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_ReffPaidBills.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_ReffPaidBills" Title="Received Reports" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <%--   <script language="javascript">


          function click() {
              var hD = document.getElementById("<%= hDnl.ClientID %>");

            if (hD.value == '') {//alert(hD.value);
                return true;
            }
            if (hD.value == 'dnl') {
                // alert(hD.value);
                alert("Please wait. Download in Progress");
                return false;
            } else {
                return true;
            }

        }
        document.onmousedown = click
    </script>--%>
    <script language="javascript" type="text/javascript">


        function isRecordsselected() {


            var f = document.getElementById("<%=grdAllReports.ClientID%>");
            var bfFlag = false;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkSelect') != -1) {
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


                var hD = document.getElementById("<%= hDnl.ClientID %>");
                hD.value = "dnl";
                return true;


            }
        }

        function ClosePopup() {
            document.getElementById('divid').style.visibility = 'hidden';
            document.getElementById('divid').style.zIndex = -1;
        }
        function showReceiveDocumentPopup() {

            //document.getElementById('divid').style.zIndex = 1;
            //document.getElementById('divid').style.position = 'absolute';
            //document.getElementById('divid').style.left = '300px';
            //document.getElementById('divid').style.top = '100px';
            //document.getElementById('divid').style.visibility = 'visible';
            //document.getElementById('frameeditexpanse').src = '../Bill_Sys_ReceivedReportPopupPage.aspx';
            var url = "Bill_Sys_ReceivedReportPopupPage.aspx";
            DignosisPopup.SetContentUrl(url);
            DignosisPopup.Show();
            return false;
            return false;

        }

        function openExistsPage1() {
            document.getElementById('div1').style.zIndex = 1;
            document.getElementById('div1').style.position = 'absolute';
            document.getElementById('div1').style.left = '360px';
            document.getElementById('div1').style.top = '100px';
            document.getElementById('div1').style.visibility = 'visible';

            return false;
        }
        function ShowDiv() {

            document.getElementById('divDashBoard').style.position = 'absolute';
            document.getElementById('divDashBoard').style.left = '200px';
            document.getElementById('divDashBoard').style.top = '120px';
            document.getElementById('divDashBoard').style.visibility = 'visible';
            document.getElementById("<%=  extddlCaseType.ClientID%>").style.visibility = 'hidden';
            return false;
        }

        function SelectAll(ival) {
            var f = document.getElementById("<%= grdAllReports.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length ; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }

    </script>

    <script language="javascript" type="text/javascript">


        function click() {
            var hD = document.getElementById("<%= hDnl.ClientID %>");

              if (hD.value == '') {//alert(hD.value);
                  return true;
              }
              if (hD.value == 'dnl') {
                  // alert(hD.value);
                  alert("Please wait. Billing in Progress");
                  return false;
              } else {
                  return true;
              }

          }
          document.onmousedown = click

          function showDiagnosisCodePopup() {
              document.getElementById('<%= pnlDiagnosisCode.ClientID %>').style.height = '180px';
            document.getElementById('<%= pnlDiagnosisCode.ClientID %>').style.visibility = 'visible';
            document.getElementById('<%= pnlDiagnosisCode.ClientID %>').style.position = "absolute";
            document.getElementById('<%= pnlDiagnosisCode.ClientID %>').style.top = '300px';
            document.getElementById('<%= pnlDiagnosisCode.ClientID %>').style.left = '700px';
            //    document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value=''; 

            //    document.getElementById('_ctl0_ContentPlaceHolder1_txtDateofService').value='';   
            MA.length = 0;
        }
    </script>
    <script type="text/javascript">
        function ShowDignosisPopup() {
            //var url = "Dignosis_Code_Popup.aspx";
            var url = "Dignosis_Code_Popup.aspx";
            DignosisPopup.SetContentUrl(url);
            DignosisPopup.Show();
            return false;
        }


    </script>
    <script type="text/javascript">
        function DeleteDignosisCodes() {
            var button = document.getElementById('<%=btnRemoveDGCodes.ClientID%>');
            button.click();
        }
    </script>
    <script type="text/javascript">
        function fncParent(array) {
            var htmlSelect = document.getElementById('<%=lstDiagnosisCodes.ClientID%>');
            var SeletedDGCodes = document.getElementById('<%=hndSeletedDGCodes.ClientID%>');
            SeletedDGCodes.value = array;
            var button = document.getElementById('<%=btnAddDGCodes.ClientID%>');
            DignosisPopup.Hide();
            button.click();
        }
    </script>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%; vertical-align: top;">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="3">


                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />

                                </td>
                            </tr>



                            <tr>
                                <td class="LeftCenter" style="height: 100%"></td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                        <tr>
                                            <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%" colspan="3">
                                                <b class="txt3">Received Report</b>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td style="width: 90%" align="left">
                                                <a id="hlnkShowDiv" href="#" onclick="ShowDiv()" runat="server" visible="false">Dash board</a>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 90%" align="left" valign="bottom">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td style="width: 90%" align="center">
                                                            <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>Diagnosis Code:
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="bottom">Case Type
                                   <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="36%" Connection_Key="Connection_String"
                                       Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---"
                                       OldText="" StausText="False" />
                                                            <asp:ListBox ID="lstDiagnosisCodes" runat="server" Width="30%" SelectionMode="Multiple"></asp:ListBox>

                                                            <%-- <a href="#" onclick="showDiagnosisCodePopup();" style="font-size: 12px; vertical-align: top;">
                                                                                Add Diagnosis</a></td>--%>
                                                            <asp:LinkButton ID="lnkAddDiagnosis" runat="server" Text="Add Diagnosis" OnClientClick="ShowDignosisPopup();return false"
                                                                Style="text-align: right; font-size: 12px; vertical-align: top;">
                                                            </asp:LinkButton>
                                                            &nbsp &nbsp
                                        <asp:LinkButton ID="lnkbtnRemoveDiag" runat="server" Text="Remove Diagnosis" Style="font-size: 12px; vertical-align: top;"
                                            OnClientClick="DeleteDignosisCodes();return false"></asp:LinkButton>


                                                        </td>

                                                        <caption>
                                                            &nbsp;
                                        <%-- <tr>
                                       
                                        <td>
                                            Diagnosis Code:
                                             <asp:ListBox ID="lstDiagnosisCodes" runat="server" Width="30%" SelectionMode="Multiple" >
                                                                            </asp:ListBox>
                                        </td>
                                        <td>
                                            <td valign="top">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <a href="#" onclick="showDiagnosisCodePopup();" style="font-size: 12px; vertical-align: top;">
                                                                                Add Diagnosis</a></td>
                                                                            <asp:LinkButton ID="lnkAddDiagnosis" runat="server" Text="Add Diagnosis" OnClientClick="ShowDignosisPopup();return false"
                                                                                Style="text-align: right; font-size: 12px; vertical-align: top;">
                                                                            </asp:LinkButton>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                           <asp:LinkButton ID="lnkbtnRemoveDiag" runat="server" Text="Remove Diagnosis" Style="font-size: 12px;
                                                                                vertical-align: top;" OnClick="lnkbtnRemoveDiag_Click"></asp:LinkButton></td>
                                                                            <asp:LinkButton ID="lnkbtnRemoveDiag" runat="server" Text="Remove Diagnosis" Style="font-size: 12px;
                                                                                vertical-align: top;" OnClientClick="DeleteDignosisCodes();return false"></asp:LinkButton>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                        </td>
                                    </tr>--%>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnRevert" runat="server" OnClick="btnRevert_Click" Text=" Revert Report" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnPerPatient" runat="server" OnClick="btnPerPatient_Click" Text="Create Bill Per Patient" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnSelectedBill" runat="server" OnClick="btnSelectedBill_Click" Text="Create Bill Per Proc" />
                                                                </td>
                                                                <%-- <td > <asp:Button ID="btnExportToExcel" runat="server"  Text="Export To Excel"  /> </td>--%>
                                                                <asp:HiddenField ID="hDnl" runat="server" />
                                                            </tr>
                                                        </caption>


                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 99%">

                                                <div style="overflow: scroll; height: 600px">
                                                    &nbsp;
                   <%--<asp:Panel runat="server" ID="pnlSrch" Visible="false">
                                        <td style="text-align: left" valign="top">
                                            Search:
                                            <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" CssClass="search-input"
                                                AutoPostBack="true"></gridsearch:XGridSearchTextBox>
                                            Record Count:
                                            <%= this.grdAllReports.RecordCount%>
                                            | Page Count:
                                            <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                            </gridpagination:XGridPaginationDropDown>
                                            <asp:LinkButton ID="lnkExportToExcel" runat="server" Visible="false" Text="Export TO Excel">  OnClick="lnkExportTOExcel_onclick"
                                                                        <img src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel" visible="false"/></asp:LinkButton>
                                        </td>
                                    </asp:Panel>--%>
                                                    <table style="vertical-align: middle; width: 100%;">
                                                        <tbody>
                                                            <tr>
                                                                <td style="vertical-align: middle; width: 30%" align="left">Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                    CssClass="search-input">
                                                                </gridsearch:XGridSearchTextBox>
                                                                    <%--<XCon:XControl ID="xcon" runat="server" Visible ="false"></XCon:XControl>--%>
                                                                </td>
                                                                <td style="width: 60%" align="right">Record Count:
                             <%= this.grdAllReports.RecordCount%>
                             | Page Count:
                             <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                             </gridpagination:XGridPaginationDropDown>
                                                                    <asp:LinkButton ID="lnkExportToExcel" OnClick="btnExportToExcel_Click" runat="server" Text="Export TO Excel">
                                 <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                                    <%--<asp:Button ID="btnSoftDelete" OnClick="btnSoftDelete_Click" runat="server" Visible="false" Text="Soft Delete"></asp:Button>--%>
                                                                    <%--<asp:Button ID="Button1" runat="server" Text="Export Bills" OnClick="btnExportToExcel_Click" />--%>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>

                                                    <xgrid:XGridViewControl ID="grdAllReports" runat="server" Width="100%" CssClass="mGrid"
                                                        MouseOverColor="0, 153, 153" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                                        HeaderStyle-CssClass="GridViewHeader" AlternatingRowStyle-BackColor="#EEEEEE"
                                                        ExportToExcelFields="" AllowPaging="true" XGridKey="Bill_Sys_RepaidBills"
                                                        DataKeyNames="SZ_CASE_ID,SZ_PATIENT_ID,DT_DATE_OF_SERVICE,
                        SZ_CODE_ID,SZ_EVENT_ID,DOCTOR_ID,CASE_NO,Company_ID,SZ_PATIENT_TREATMENT_ID,Insurance_Company,
                        Insurance_Address,CLAIM_NO,SZ_PROCEDURE_GROUP_ID,PATIENT_NAME,SZ_STUDY_NUMBER"
                                                        PageRowCount="50" PagerStyle-CssClass="pgr" AllowSorting="true" AutoGenerateColumns="false"
                                                        GridLines="None" OnRowCommand="grdAllReports_RowCommand" OnRowEditing="grdAllReports_RowEditing">
                                                        <Columns>
                                                            <%-- 0--%>
                                                            <asp:TemplateField HeaderText="Select All">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);" ToolTip="Select All" />
                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <%-- 1--%>
                                                            <asp:BoundField DataField="SZ_CASE_ID" HeaderText="case ID" Visible="false" />
                                                            <%-- 2--%>
                                                            <asp:BoundField DataField="SZ_PATIENT_ID" HeaderText="PATIENT ID" Visible="false" />
                                                            <%-- 3--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Chart#" DataField="CHART_NO" Visible="true" SortExpression=" MST_PATIENT.I_RFO_CHART_NO" />
                                                            <%-- 4--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Case#" DataField="CASE_NO" Visible="true" SortExpression="MST_CASE_MASTER.SZ_CASE_NO" />

                                                            <%-- 5--%>
                                                            <asp:TemplateField HeaderText="Edit Diagnosis Code" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkSelectEdit" runat="server" Text="Edit" Visible="true" CommandName="Edit"
                                                                        CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- 6--%>
                                                            <asp:BoundField DataField="DT_DATE_OF_SERVICE" HeaderText="Date Of Service" Visible="false" />

                                                            <%-- 7--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Patient Name" DataField="PATIENT_NAME" Visible="true" SortExpression="SZ_PATIENT_FIRST_NAME" />
                                                            <%-- 8--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Case Type Name" DataField="SZ_CASE_TYPE_NAME" Visible="true" SortExpression=" MST_CASE_TYPE.SZ_CASE_TYPE_NAME" />
                                                            <%-- 9--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Procedure Code" DataField="SZ_CODE" SortExpression="MST_BILL_PROC_TYPE.SZ_TYPE_CODE" />
                                                            <%-- 10--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Description " DataField="SZ_CODE_DESCRIPTION" SortExpression="MST_BILL_PROC_TYPE.SZ_TYPE_DESCRIPTION" />
                                                            <%-- 11--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Office Name " DataField="SZ_OFFICE_NAME" Visible="true" SortExpression=" MST_OFFICE.SZ_OFFICE" />
                                                            <%-- 12--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Reffering Doctor " DataField="DOC_NAME" Visible="true" SortExpression=" MST_DOCTOR.SZ_DOCTOR_NAME" />
                                                            <%-- 13--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Date Of Service" DataField="DATE_SORT" Visible="true" SortExpression="TXN_CALENDAR_EVENT.DT_EVENT_DATE" />
                                                            <%-- 14--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Status" DataField="SZ_STATUS" Visible="true" SortExpression="" />
                                                            <%-- 15--%>
                                                            <asp:BoundField DataField="SZ_CODE_ID" HeaderText="Code ID" Visible="false" />
                                                            <%-- 16--%>
                                                            <asp:BoundField DataField="SZ_EVENT_ID" HeaderText="EVENT ID" Visible="false" />
                                                            <%-- 17--%>
                                                            <asp:BoundField DataField="DOCTOR_ID" HeaderText="Doctor ID" Visible="false" />
                                                            <%-- 18--%>
                                                            <asp:BoundField DataField="CASE_NO" HeaderText="CASE NO" Visible="false" />
                                                            <%-- 19--%>
                                                            <asp:BoundField DataField="Company_ID" HeaderText="Company ID" Visible="false" />
                                                            <%-- 20--%>
                                                            <asp:BoundField DataField="SZ_PATIENT_TREATMENT_ID" HeaderText="PATIENT TREATMENT ID" Visible="false" />
                                                            <%-- 21--%>
                                                            <asp:BoundField DataField="Insurance_Company" HeaderText="Insurance Company" Visible="false" />
                                                            <%-- 22--%>
                                                            <asp:BoundField DataField="Insurance_Address" HeaderText="Insurance Address" Visible="false" />
                                                            <%-- 23--%>
                                                            <asp:BoundField DataField="CLAIM_NO" HeaderText="CLAIM NO" Visible="false" />
                                                            <%-- 24--%>
                                                            <asp:BoundField DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Speciality" Visible="false" />
                                                            <%-- 25--%>
                                                            <asp:BoundField DataField="PATIENT_NAME" HeaderText="Patient Name" Visible="false" />
                                                            <%-- 26--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Insurance Company " DataField="SZ_INSURANCE_COMPANY_NAME" Visible="true" SortExpression="MST_INSURANCE_COMPANY.SZ_INSURANCE_NAME" />
                                                            <%-- 27--%>
                                                            <asp:TemplateField HeaderText="Report" Visible="true">
                                                                <ItemTemplate>

                                                                    <img alt="" onclick="javascript:window.open('<%# DataBinder.Eval(Container, "DataItem.REPORT PATH")%> ');" src="../Images/123.gif" title="view" style="cursor: pointer;" />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- 28--%>
                                                            <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                                                                HeaderText="Study Number" DataField="SZ_STUDY_NUMBER" Visible="true" />

                                                            <asp:BoundField DataField="SZ_TYPE_CODE_ID" HeaderText="Procode" Visible="false" />
                                                        </Columns>
                                                    </xgrid:XGridViewControl>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%" class="SectionDevider">
                                                <%--<asp:HiddenField ID="HndPatientID" runat="server" />
                                        <asp:HiddenField ID="hndJavascriptsPatientID" runat="server" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99%; height: auto; float: left;">

                                                <div id="divid" style="position: absolute; width: 600px; height: 600px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                                    <div style="position: relative; text-align: right; background-color: #8babe4; width: 600px;">
                                                        <asp:Button ID="txtUpdate3" Text="X" BackColor="#8babe4" BorderStyle="None" runat="server" OnClick="txtUpdate_Click" />
                                                    </div>
                                                    <iframe id="frameeditexpanse" src="" frameborder="0" height="600px" width="600px"></iframe>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="right">
                                                <asp:TextBox ID="txtSort" runat="server" Visible="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="RightCenter" style="width: 10px; height: 100%;"></td>
                            </tr>
                            <tr>
                                <td class="LeftBottom"></td>
                                <td class="CenterBottom"></td>
                                <td class="RightBottom" style="width: 10px"></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div id="divid2" style="position: absolute; left: 428px; top: 920px; width: 300px; height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid; border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid; text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left; font-family: Times New Roman; float: left; background-color: #8babe4;">
            Msg
        </div>
        <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px; background-color: #8babe4;">
            <a onclick="CancelExistPatient();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <span id="msgPatientExists" runat="server"></span>
        <br />
        <br />
        <br />
        <%--<asp:Button ID="btnOK" runat ="server" OnClick="btnOK_Click" Text = "OK" CssClass="Buttons" />--%>
        <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
            style="width: 80px;" />
        <br />
        <%--div style="text-align: center;">
            <input type="button" runat="server" class="Buttons" value="OK" id="btnClientOK" onclick="SaveExistPatient();"
                style="width: 80px;" />
            <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
                style="width: 80px;" />--%>
        <div style="text-align: center;">
        </div>
    </div>
    <div id="div1" style="position: absolute; left: 428px; top: 920px; width: 300px; height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid; border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid; text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left; font-family: Times New Roman; float: left; background-color: #8babe4; left: 0px; top: 0px;">
            Msg
        </div>
        <div style="position: relative; text-align: right; float: left; width: 60%; height: 20px; background-color: #8babe4;">
            <a onclick="CancelExistPatient1();" style="cursor: pointer;" title="Close">X</a>
        </div>
        <br />
        <div style="top: 50px; width: 231px; font-family: Times New Roman; text-align: center;">
            <span id="popupmsg" runat="server"></span>
            <br />
            <br />
            <br />
            <%--<asp:Button ID="btnOK1" runat ="server" OnClick="btnOK1_Click" Text = "OK" CssClass="Buttons" />--%>
            <input type="button" class="Buttons" value="Cancel" id="btnCancel1" onclick="CancelExistPatient1();"
                style="width: 80px;" />
        </div>
        <br />
        <%--div style="text-align: center;">
            <input type="button" runat="server" class="Buttons" value="OK" id="btnClientOK" onclick="SaveExistPatient();"
                style="width: 80px;" />
            <input type="button" class="Buttons" value="Cancel" id="btnCancelExist" onclick="CancelExistPatient();"
                style="width: 80px;" />--%>
        <div style="text-align: center;">
        </div>
    </div>
    <div id="divDashBoard" visible="true" style="position: absolute; width: 800px; height: 475px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="document.getElementById('divDashBoard').style.visibility='hidden';" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 430; float: left; position: relative;">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%; padding-top: 3px; height: 100%"
                    valign="top">
                    <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="LeftTop"></td>
                            <td class="CenterTop"></td>
                            <td class="RightTop"></td>
                        </tr>
                        <tr>
                            <td class="LeftCenter" style="height: 100%"></td>
                            <td style="width: 97%" class="TDPart">
                                <table id="tblMissingSpeciality" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 99%; height: 130px; float: left; position: relative; left: 0px; top: 0px; vertical-align: top"
                                    visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">Missing Speciality</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <table>
                                                <tr>
                                                    <td>You have
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMissingSpecialityText" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%; height: 10px;" class="SectionDevider"></td>
                                    </tr>
                                </table>
                                <table border="0" id="tblDailyAppointment" runat="server" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">Today's Appointment</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <asp:Label ID="lblAppointmentToday" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider"></td>
                                    </tr>
                                </table>
                                <table id="tblWeeklyAppointment" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%">Weekly &nbsp;Appointment</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart">
                                            <asp:Label ID="lblAppointmentWeek" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider"></td>
                                    </tr>
                                </table>
                                <table id="tblBillStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; vertical-align: top; float: left; position: relative;"
                                    visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">Bill Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">You have &nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider"></td>
                                    </tr>
                                </table>
                                <table id="tblDesk" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 32%; height: 195px; float: left; position: relative;"
                                    visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%;" valign="top">Desk</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">You have&nbsp;
                                            <asp:Label ID="lblDesk" runat="server"></asp:Label>
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider"></td>
                                    </tr>
                                </table>
                                <table id="tblMissingInfo" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">Missing Information</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%;" class="TDPart" valign="top">You have &nbsp;<asp:Label ID="lblMissingInformation" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider"></td>
                                    </tr>
                                </table>
                                <table id="tblReportSection" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative;" visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">Report Section</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">You have &nbsp;<asp:Label ID="lblReport" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider"></td>
                                    </tr>
                                </table>
                                <table id="tblBilledUnbilledProcCode" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 32%; height: 195px; float: left; position: relative; left: 0px; top: 0px;"
                                    visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">Procedure Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">You have &nbsp;<asp:Label ID="lblProcedureStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider"></td>
                                    </tr>
                                </table>
                                <table id="tblVisits" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 32%; height: 195px; float: left; position: relative; left: 0px; top: 0px;"
                                    visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">Visits</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">
                                            <asp:Label ID="lblVisits" runat="server" Visible="true"></asp:Label>
                                            <table>
                                                <tr>
                                                    <td>You have
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ul style="list-style-type: disc; padding-left: 60px;">
                                                            <li><a id="hlnkTotalVisit" href="#" runat="server">
                                                                <asp:Label ID="lblTotalVisit" runat="server"></asp:Label></a>&nbsp;Total Visit</li>
                                                            <li>
                                                                <a id="hlnkBilledVisit" href="#" runat="server">
                                                                    <asp:Label ID="lblBilledVisit" runat="server"></asp:Label></a>&nbsp;Billed Visit
                                                            </li>
                                                            <li><a id="hlnkUnBilledVisit" href="#" runat="server">
                                                                <asp:Label ID="lblUnBilledVisit" runat="server"></asp:Label></a>&nbsp;UnBilled Visit
                                                            </li>
                                                        </ul>
                                                        <ajaxToolkit:PopupControlExtender ID="PopExTotalVisit" runat="server" TargetControlID="hlnkTotalVisit"
                                                            PopupControlID="pnlTotalVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                        <ajaxToolkit:PopupControlExtender ID="PopExBilledVisit" runat="server" TargetControlID="hlnkBilledVisit"
                                                            PopupControlID="pnlBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                        <ajaxToolkit:PopupControlExtender ID="PopExUnBilledVisit" runat="server" TargetControlID="hlnkUnBilledVisit"
                                                            PopupControlID="pnlUnBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider"></td>
                                    </tr>
                                </table>
                                <table id="tblPatientVisitStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 33%; height: 195px; float: left; position: relative; left: 0px; top: 0px; vertical-align: top"
                                    visible="false">
                                    <tr>
                                        <td class="TDHeading" style="width: 99%" valign="top">Patient Visit Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="TDPart" valign="top">You have &nbsp;<asp:Label ID="lblPatientVisitStatus" runat="server"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99%" class="SectionDevider"></td>
                                    </tr>
                                </table>
                            </td>
                            <td class="RightCenter" style="width: 10px; height: 100%;"></td>
                        </tr>
                        <tr>
                            <td class="LeftBottom"></td>
                            <td class="CenterBottom"></td>
                            <td class="RightBottom" style="width: 10px"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            
          
        </table>
    </div>
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
            visible="false"></iframe>
    </div>
    <%--Total Visit--%>
    <asp:Panel ID="pnlTotalVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdTotalVisit" runat="server" Width="25px" CssClass="GridTable"
                        AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--Visit--%>
    <asp:Panel ID="pnlBilledVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--UnVisit--%>
    <asp:Panel ID="pnlUnBilledVisit" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <asp:DataGrid ID="grdUnVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDiagnosisCode" runat="server" Style="width: 450px; height: 0px;
        background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid;
        visibility: hidden;">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
               <%-- <td align="right">
                    <a onclick="CloseDiagnosisCodePopup();" style="cursor: pointer;" title="Close">X</a>
                </td>--%>
            </tr>
            <tr>
                <td style="width: 102%;" valign="top">
                    
                </td>
            </tr>
           
        </table>
        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" CloseAction="CloseButton" Modal="true"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="DignosisPopup"
            HeaderText="Diagnosis Codes" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White"
            HeaderStyle-BackColor="#000000" AllowDragging="True" EnableAnimation="False"
            EnableViewState="True" Width="900px" PopupHorizontalOffset="0" PopupVerticalOffset="0"
              AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
            Height="540px">
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
        <div style="visibility: hidden;">
            <asp:Button ID="btnAddDGCodes" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
                OnClick="btnAddDGCodes_Click" />
        </div>
        <div style="visibility: hidden;">
            <asp:Button ID="btnRemoveDGCodes" Text="X" BackColor="#B5DF82" BorderStyle="None"
                runat="server" OnClick="lnkbtnRemoveDiag_Click" />
        </div>
        <asp:Panel ID="pnlPDFWorkerComp" runat="server" Style="width: 250px; height: 0px;
            background-color: white; border-color: ThreeDFace; border-width: 1px; border-style: solid;
            visibility: hidden;">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%"
                class="TDPart">
                <tr>
                    <td align="right" valign="top">
                        <a style="cursor: pointer;" title="Close">X</a>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="text-align: left;" class="ContentLabel">
                        <asp:RadioButtonList ID="rdbListPDFType" runat="server">
                            <asp:ListItem Value="1" Text="Doctor's Initial Report" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Doctor's Progress Report"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Doctor's Report Of MMI"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="center">
                        <asp:Button ID="btnGenerateWCPDF" runat="server" Text="Generate PDF" 
                            CssClass="Buttons" />
                        <input type="hidden" runat="server" id="hndSeletedDGCodes" />
                        <asp:HiddenField ID="hdnWCPDFBillNumber" runat="server" />
                        <asp:HiddenField ID="hdnSpeciality" runat="server" />

                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <%-- </table> --%>
    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtRefCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtBillDate" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtReadingDocID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtPatientID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtCaseNo" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtCaseType" runat="server" Width="10px" Visible="False"></asp:TextBox>


    <dx:ASPxPopupControl ID="DGCODEPOPUP" runat="server" CloseAction="CloseButton" Modal="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="DignosisPopup"
        HeaderText="Diagnosis Codes" HeaderStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="White"
        HeaderStyle-BackColor="#000000" AllowDragging="True" EnableAnimation="False"
        EnableViewState="True" Width="900px" PopupHorizontalOffset="0" PopupVerticalOffset="0"
        AutoUpdatePosition="true" ScrollBars="Auto" RenderIFrameForPopupElements="Default"
        Height="540px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
   
    <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0">

        <ProgressTemplate>
            <asp:Image ID="img1" runat="server" Style="position: absolute; z-index: 1; left: 50%; top: 50%" ImageUrl="~/Ajax Pages/Images/loading_transp.gif" AlternateText="Loading....."
                Height="60px" Width="60px"></asp:Image>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
