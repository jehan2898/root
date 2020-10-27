<%@ Page Title="" Language="C#" MasterPageFile="~/Provider/MasterPage.master" AutoEventWireup="true" CodeFile="ProviderSearchCase.aspx.cs" Inherits="Provider_ProviderSearchCase" %>

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
    <script type="text/javascript">
        function Export() {
            expLoadingPanel.Show();
            Callback1.PerformCallback();
        }
        function OnCallbackComplete(s, e) {
            alert('hi');
            expLoadingPanel.Hide();
            var url = "../Provider/DownloadFiles.aspx";
            IFrame_DownloadFiles.SetContentUrl(url);
            IFrame_DownloadFiles.Show();
            return false;
        }
        function showEditAllFrame() {
            var ofid = document.getElementById('_ctl0_ContentPlaceHolder1_txtofcid').value;
            window.open('../Bill_Sys_ShowProviderReports.aspx?ofcid=' + ofid, 'SupportMail', 'channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=430,height=365');
        }

        function Clear() {
            // alert("call");
            document.getElementById("<%=txtCaseNo.ClientID %>").value = '';

            document.getElementById("<%=txtClaimNumber.ClientID %>").value = '';
            // document.getElementById("<%=txtPatientID.ClientID %>").value ='';
            document.getElementById("<%=txtPatientName.ClientID %>").value = '';
            document.getElementById("<%=txtDateofAccident.ClientID %>").value = '';
            document.getElementById("<%=txtDateofBirth.ClientID %>").value = '';


            //document.getElementById("ctl00_ContentPlaceHolder1_extddlCaseStatus").value ='NA';
            document.getElementById("<%=extddlCaseStatus.ClientID %>").value = 'NA';
            document.getElementById("<%=txtInsuranceCompany.ClientID %>").value = '';

        }

        function showPateintFrame(objCaseID, objCompanyID) {
            // alert(objCaseID + ' ' + objCompanyID);
            var obj3 = "";
            document.getElementById('divfrmPatient').style.zIndex = 1;
            document.getElementById('divfrmPatient').style.position = 'absolute';
            document.getElementById('divfrmPatient').style.left = '400px';
            document.getElementById('divfrmPatient').style.top = '250px';
            document.getElementById('divfrmPatient').style.visibility = 'visible';
            document.getElementById('frmpatient').src = "PatientViewFrame.aspx?CaseID=" + objCaseID + "&cmpId=" + objCompanyID + "";
            return false;
        }

        function ClosePatientFramePopup() {
            //   alert("");
            //document.getElementById('divfrmPatient').style.height='0px';
            document.getElementById('divfrmPatient').style.visibility = 'hidden';
            document.getElementById('divfrmPatient').style.top = '-10000px';
            document.getElementById('divfrmPatient').style.left = '-10000px';
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000" />
    <div id="psearch-search" style="height: 250px; position: relative; z-index: 0;">
        <div>
            <table border="0" width="100%" cellpadding="0" cellspacing="0" id="table-inner-menu-diplay">
                <tr>
                    <td style="width:40%;">
                        <table border="0" cellpadding="0" cellspacing="0" class="border" style="padding-top: 10px;" width="100%">
                            <tr>
                                <td align="left" width="100%">
                                    <table border="0" align="left" cellpadding="2" cellspacing="2" style="width: 100%;
                                        height: 50%; border: 0px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, <%=btnSearch.ClientID %>)">
                                        <tr>
                                            <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="2">
                                                <b class="txt3">Search Parameters</b>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table border="0">
                                                    <tr>
                                                        <td>
                                                            <b>Case# </b>
                                                        </td>
                                                        <td>
                                                            <b>Case Type </b>
                                                        </td>
                                                        <td>
                                                            <b>Case Status</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="" style="width: 33%">
                                                            <asp:TextBox ID="txtCaseNo" runat="server" Width="98%" CssClass="search-input"></asp:TextBox>
                                                        </td>
                                                        <td class="" style="width: 33%">
                                                            <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="100%" Selected_Text="---Select---"
                                                                Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Connection_Key="Connection_String"
                                                                CssClass="search-input"></extddl:ExtendedDropDownList>
                                                        </td>
                                                        <td class="" style="width: 33%">
                                                            <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="100%" Selected_Text="OPEN"
                                                                Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Connection_Key="Connection_String"
                                                                CssClass="search-input"></extddl:ExtendedDropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table>
                                                    <tr>
                                                        <td class="td-widget-bc-search-desc-ch1">
                                                            <b>Patient</b>
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch1">
                                                            <b>Insurance </b>
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch1">
                                                            <b>Claim Number</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td-widget-bc-search-desc-ch3">
                                                            <asp:TextBox ID="txtPatientName" runat="server" Width="98%" autocomplete="off" CssClass="search-input"></asp:TextBox>
                                                            <extddl:ExtendedDropDownList ID="extddlPatient" runat="server" Width="50%" Selected_Text="--- Select ---"
                                                                Procedure_Name="SP_MST_PATIENT" Flag_Key_Value="REF_PATIENT_LIST" Connection_Key="Connection_String"
                                                                AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlPatient_extendDropDown_SelectedIndexChanged"
                                                                Visible="false"></extddl:ExtendedDropDownList>
                                                            <asp:CheckBox ID="chkJmpCaseDetails" runat="server" Text="Jump to Case Details" Visible="false">
                                                            </asp:CheckBox>
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch3">
                                                            <asp:TextBox ID="txtInsuranceCompany" runat="server" Width="98%" autocomplete="off"
                                                                CssClass="search-input"></asp:TextBox>
                                                            <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Width="50%" Selected_Text="---Select---"
                                                                Procedure_Name="SP_MST_INSURANCE_COMPANY" Flag_Key_Value="INSURANCE_LIST" Connection_Key="Connection_String"
                                                                AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlInsurance_extendDropDown_SelectedIndexChanged"
                                                                Visible="false"></extddl:ExtendedDropDownList>
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch3">
                                                            <asp:TextBox ID="txtClaimNumber" runat="server" Width="98%" CssClass="search-input"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table>
                                                    <tr>
                                                        <%--<td class="td-widget-bc-search-desc-ch1">
                                                            SSN#
                                                        </td>--%>
                                                        <td class="td-widget-bc-search-desc-ch1">
                                                            <b>Date of Accident</b>
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch1">
                                                            <b>Date of Birth</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <%--<td class="td-widget-bc-search-desc-ch3" valign="bottom">
                                                            <asp:TextBox ID="txtSSNNo" runat="server" Width="98%" CssClass="search-input"></asp:TextBox>
                                                        </td>--%>
                                                        <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                            <asp:TextBox ID="txtDateofAccident" onkeypress="return CheckForInteger(event,'/')"
                                                                runat="server" MaxLength="10" Width="80%" CssClass="search-input"></asp:TextBox>
                                                            <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif"
                                                                valign="bottom"></asp:ImageButton>
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                            <asp:TextBox ID="txtDateofBirth" onkeypress="return CheckForInteger(event,'/')" runat="server"
                                                                MaxLength="10" Width="75%" CssClass="search-input"></asp:TextBox>
                                                            <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif"
                                                                valign="bottom"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch3" valign="bottom">
                                                            <ajaxToolkit:CalendarExtender ID="calExtDateofAccident" runat="server" TargetControlID="txtDateofAccident"
                                                                PopupButtonID="imgbtnDateofAccident">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                                MaskType="Date" TargetControlID="txtDateofAccident" PromptCharacter="_" AutoComplete="true">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                ControlToValidate="txtDateofAccident" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                                            <asp:RangeValidator runat="server" ID="rngDate" ControlToValidate="txtDateofAccident"
                                                                Type="Date" MinimumValue="01/01/1901" MaximumValue="12/31/2099" ErrorMessage="Enter a valid date " />
                                                        </td>
                                                        <td class="td-widget-bc-search-desc-ch3" valign="bottom">
                                                            <ajaxToolkit:CalendarExtender ID="calExtDateofBirth" runat="server" TargetControlID="txtDateofBirth"
                                                                PopupButtonID="imgbtnDateofBirth">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                                MaskType="Date" TargetControlID="txtDateofBirth" PromptCharacter="_" AutoComplete="true">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                                ControlToValidate="txtDateofBirth" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                                IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                                            <asp:RangeValidator runat="server" ID="rngDateBirth" ControlToValidate="txtDateofBirth"
                                                                Type="Date" MinimumValue="01/01/1901" MaximumValue="12/31/2099" ErrorMessage="Enter a valid date " />
                                                        </td>
                                                    </tr>
                                                    <%--<tr> 
                                                <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                    <asp:Label ID="lblLocation" runat="server" Text="Location" class="td-widget-bc-search-desc-ch1"></asp:Label>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                    <asp:Label ID="lblpatientid" runat="server" Text="Patient Id" class="td-widget-bc-search-desc-ch1"></asp:Label>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1" valign="top">
                                                    <asp:Label ID="lblChart" runat="server" Text="Chart No" class="td-widget-bc-search-desc-ch1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="100%" Selected_Text="---Select---"
                                                        Procedure_Name="SP_MST_LOCATION" Flag_Key_Value="LOCATION_LIST" Connection_Key="Connection_String">
                                                    </extddl:ExtendedDropDownList>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtpatientid" runat="server" Width="98%"></asp:TextBox>
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="top">
                                                    <asp:TextBox ID="txtChartNo" runat="server" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>--%>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="80px"
                                                            Text="Search"></asp:Button>
                                                        <%--<asp:Button ID="btnClear"  OnClientClick="Clear();"  Width="80px" Text="Clear"></asp:Button>
                                                        <input type="button" id="btnClear" onclick="Clear();" style="width: 80px" value="Clear" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel> --%>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="80px"
                                                            Text="Search"></asp:Button>
                                                        <asp:Button ID="btnClearP" runat="server" Width="80px" Text="Clear"></asp:Button>
                                                        <%--<input type="button" id="btnClear" onclick="Clear();"  style="width: 80px" value="Clear" />--%>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: White;">
                                    <asp:TextBox ID="txtChartSearch" runat="server" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtPatientLNameSearch" runat="server" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtPatientFNameSearch" runat="server" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtPatientID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtOfficeID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtUserID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" align="left">
                        <asp:LinkButton ID="lnk" runat="server" Text="Report" Style="text-align: right;
                            font-size: 12px; vertical-align: top;" OnClientClick="showEditAllFrame();"></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <%---------------------------- Start X-Grid ------------------------------%>
            <table>
                <tr>
                    <asp:UpdatePanel ID="UP_grdPatientSearch" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                        <b class="txt3">Patient list</b>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_grdPatientSearch"
                                            DisplayAfter="10" DynamicLayout="true">
                                            <ProgressTemplate>
                                                <div id="Div1" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                    runat="Server">
                                                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                        Height="25px" Width="24px"></asp:Image>
                                                    Loading...</div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                            DisplayAfter="10" DynamicLayout="true">
                                            <ProgressTemplate>
                                                <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                    runat="Server">
                                                    <asp:Image ID="img3" runat="server" ImageUrl="../Images/rotation.gif" AlternateText="Loading....."
                                                        Height="25px" Width="24px"></asp:Image>
                                                    Loading...</div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                            <table style="vertical-align: middle; width: 100%;">
                                <tbody>
                                    <tr>
                                       <td style="vertical-align: middle; width: 30%" align="left">
                                           <%--  Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                CssClass="search-input">
                                            </gridsearch:XGridSearchTextBox>
                                            <%--<XCon:XControl ID="xcon" runat="server" Visible ="false"></XCon:XControl>--%>
                                        </td>
                                        <td style="width: 60%" align="right">
                                        <%--    Record Count:
                                            <%= this.grdPatientList.RecordCount%>
                                            | Page Count:
                                            <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                            </gridpagination:XGridPaginationDropDown>
                                            <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server"
                                                Text="Export TO Excel">
                                        <img src="../Images/Excel.jpg" alt="" style="border: none;" height="15px" width="15px"
                                            title="Export TO Excel" /></asp:LinkButton>
                                        </td>--%>
                                    </tr>
                                </tbody>
                            </table>
                            <table width="100%">
                                <tr>
                                     <td style="width: 100%; text-align: right;">
                                        <dx:ASPxHyperLink Text="[Excel]" runat="server" ID="xExcel">
                                            <ClientSideEvents Click="Export" />
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="Callback1"
                                            OnCallback="ASPxCallback1_Callback">
                                            <ClientSideEvents CallbackComplete="OnCallbackComplete" />
                                        </dx:ASPxCallback>
                                        <dx:ASPxLoadingPanel Text="Exporting..." runat="server" ID="expLoadingPanel" ClientInstanceName="expLoadingPanel">
                                        </dx:ASPxLoadingPanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:aspxgridview id="grdPatientList" runat="server" keyfieldname="SZ_CASE_ID" autogeneratecolumns="false"
                                        width="100%" settingspager-pagesize="50" settingscustomizationwindow-height="100"
                                        settings-verticalscrollableheight="250">
                                            <Columns>
                                                <%--0--%>
                                                <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="Case ID" HeaderStyle-HorizontalAlign="Center"
                                                Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataColumn>
                                                <%--1--%>
                                                <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="Company ID" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataColumn>
                                                <%--2--%>
                                                <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="Case #" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataColumn>
                                                <%--3--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="Patient Name" HeaderStyle-HorizontalAlign="Center"
                                                    Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataColumn>
                                                <%--4--%>
                                                <dx:GridViewDataColumn Caption="#" Width="12%" CellStyle-HorizontalAlign="Center" Settings-AllowSort="True">
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCase" runat="server" Text="#" />
                                                    </HeaderTemplate>
                                                    <DataItemTemplate>
                                                        <a target="_self" href='../Provider/Bill_Sys_CaseDetails.aspx?caseid=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>&cmpid=<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>&caseno=<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'>
                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%></a>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                </dx:GridViewDataColumn>
                                                <%--5--%>
                                                <dx:GridViewDataColumn Caption="Patient" Width="12%" CellStyle-HorizontalAlign="Center" Settings-AllowSort="True">
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblPatient" runat="server" Text="Patient" />
                                                    </HeaderTemplate>
                                                    <DataItemTemplate>
                                                        <a id="lnkframePatient" href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>'>
                                                            <%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%></a>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                </dx:GridViewDataColumn>
                                                <%--6--%>
                                                <dx:GridViewDataColumn FieldName="DT_DATE_OF_ACCIDENT" Caption="Accident Date" HeaderStyle-HorizontalAlign="Center" Settings-AllowSort="True">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataColumn>
                                                <%--7--%>
                                                <dx:GridViewDataColumn FieldName="DT_DATE_OPEN" Caption="Opened" HeaderStyle-HorizontalAlign="Center" Settings-AllowSort="True">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataColumn>
                                                <%--8--%>
                                                <dx:GridViewDataColumn FieldName="SZ_INSURANCE_NAME" Caption="Insurance" HeaderStyle-HorizontalAlign="Center" Settings-AllowSort="True">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataColumn>
                                                <%--9--%>
                                                <dx:GridViewDataColumn FieldName="SZ_CLAIM_NUMBER" Caption="Claim Number" HeaderStyle-HorizontalAlign="Center" Settings-AllowSort="True">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataColumn>
                                                <%--10--%>
                                                <dx:GridViewDataColumn FieldName="SZ_POLICY_NUMBER" Caption="Policy Number" HeaderStyle-HorizontalAlign="Center" Settings-AllowSort="True">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataColumn>
                                                <%--11--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PROVIDER_NAME" Caption="Provider Name" HeaderStyle-HorizontalAlign="Center" Settings-AllowSort="True">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataColumn>
                                                <%--12--%>
                                                <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE" Caption="Case Type" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataColumn>
                                                <%--13--%>
                                                <dx:GridViewDataColumn FieldName="SZ_PATIENT_ID" Caption="Patient ID" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                </dx:GridViewDataColumn>

                                            </Columns>
                                            <Settings ShowVerticalScrollBar="true" ShowFilterRow="false" ShowGroupPanel="false" />
                                            <SettingsBehavior AllowFocusedRow="false" />
                                            <SettingsBehavior AllowSelectByRowClick="true" />
                                            <SettingsPager Position="Bottom" />
                                            <SettingsCustomizationWindow Height="100px"></SettingsCustomizationWindow>
                                        </dx:aspxgridview>
                                        <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdPatientList">
                                        </dx:ASPxGridViewExporter>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="con" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID="btnExportToExcel" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </tr>
            </table>
        </div>
    </div>
    <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
        visibility: hidden; border-left: silver 1px solid; width: 500px; border-bottom: silver 1px solid;
        position: absolute; top: 682px; height: 280px; background-color: #B5DF82" id="divfrmPatient">
        <div style="position: relative; background-color: #B5DF82; text-align: right">
            <a style="cursor: pointer" title="Close" onclick="ClosePatientFramePopup();">X</a>
        </div>
        <iframe id="frmpatient" src="" frameborder="0" width="500" height="380"></iframe>
    </div>
    <asp:TextBox ID="utxtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtClaimNumber" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtPatientName" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtDateofCreated" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtDateofAccident" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtOfficeID" runat="server" Width="10px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="utxtDateofBirth" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtCaseType" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtCaseStatus" runat="server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtInsuranceName" runat="Server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtCaseId" runat="Server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtCaseNo" runat="Server" Width="10px" Visible="false"></asp:TextBox>
    <asp:TextBox ID="utxtPatientID" runat="Server" Width="10px" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="txtofcid" runat="server" />
    <dx:ASPxPopupControl ID="IFrame_DownloadFiles" runat="server" CloseAction="CloseButton"
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="IFrame_DownloadFiles" HeaderText="Data Export" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" AllowDragging="True"
        EnableAnimation="False" EnableViewState="True" Width="600px" ToolTip="Download File(s)"
        PopupHorizontalOffset="0" PopupVerticalOffset="0"   AutoUpdatePosition="true"
        ScrollBars="Auto" RenderIFrameForPopupElements="Default" Height="200px">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
</asp:Content>

