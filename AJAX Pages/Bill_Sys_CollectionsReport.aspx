<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_CollectionsReport.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_SearchInsuranceCompany"
    Title="Collection Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">

        
        
        //Nirmalkumar
        
        function SelectAll(ival) {
            var f = document.getElementById('<%=grdInsuranceCompany.ClientID%>');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }

                }
            }
        }
        function SelectAllCaseStatus(ival) {
            var f = document.getElementById('<%=grdCaseStatus.ClientID%>');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }

                }
            }
        }
        function SelectAllBill(ival) {
            var f = document.getElementById('<%=grdBillStatus.ClientID%>');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }

                }
            }
        }
        function mapSelectedClick() 
        {

            
            var flag = false;
            
            var f = document.getElementById('<%=grdInsuranceCompany.ClientID%>');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) 
            {

                if (f.getElementsByTagName("input").item(i).type == "checkbox") 
                {
                    if (f.getElementsByTagName("input").item(i).checked == true) 
                    {
                        flag=true;
                    }
                }
            }

            if (flag == false) {
                alert('Please select Insurance company');
                return false;
            }
            var flag1 = false;

            var f = document.getElementById('<%=grdBillStatus.ClientID%>');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {

                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked == true) {
                        flag1 = true;
                    }
                }
            }

            if (flag1 == false) {
                alert('Please Select Bill Status');
                return false;
            }

            
        }
        
        
    </script>

    <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; height: 50%;
        background-color: White;">
            
            
            <td align="left">
                <table style="border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82; border-bottom: 1px solid #B5DF82;
                    width: 60%;">
                    <tr>
                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" 
                            colspan="6">
                            <b class="txt3">Search Parameters</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <table>
                                <tr>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                        &nbsp; &nbsp; &nbsp; &nbsp;From Date:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            CssClass="text-box" MaxLength="10"></asp:TextBox><asp:ImageButton ID="imgbtnFromDate"
                                                runat="server" ImageUrl="~/Images/cal.gif" /><ajaxToolkit:CalendarExtender ID="calExtFromDate"
                                                    runat="server" TargetControlID="txtFromDate" PopupButtonID="imgbtnFromDate" />
                                    </td>
                                    <td colspan="1">
                                    </td>
                                    <td style="width: 100px">
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; To Date:</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                            CssClass="text-box" MaxLength="10"></asp:TextBox><asp:ImageButton ID="imgbtnToDate"
                                                runat="server" ImageUrl="~/Images/cal.gif" /><ajaxToolkit:CalendarExtender ID="calExtToDate"
                                                    runat="server" TargetControlID="txtToDate" PopupButtonID="imgbtnToDate" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td align="left" valign="middle" class="txt2">
                            <b class="txt3">Insurance Company:</b>
                        </td>
                        <td align="left" class="txt2" valign="middle">
                        </td>
                        <td align="left" class="txt2" valign="middle">
                        </td>
                        <td align="left" class="txt2" valign="middle">
                        </td>
                        <td align="left" class="txt2" valign="middle">
                            <input type="hidden" id="ddlElements" name="ddlElements" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="left" class="txt2" colspan="4" valign="middle">
                            <div style="float: left; width: 200px">
                                <dx:ASPxComboBox ID="ASPxComboBox1" ClientInstanceName="cb" runat="server" DropDownStyle="DropDown"
                                    IncrementalFilteringMode="StartsWith" DataSourceID="SqlDataSource1" TextField="SZ_INSURANCE_NAME"
                                    ValueField="SZ_INSURANCE_NAME" Width="150%">
                                </dx:ASPxComboBox>
                            </div>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<br />
                            &nbsp;</td>
                        <td align="left" class="txt2" valign="middle">
                            &nbsp;</td>
                    </tr>--%>
                    <tr>
                       <%-- <td align="left" class="txt2" colspan="3" valign="middle">
                        </td>
                        <td align="left" class="txt2" valign="middle">
                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text=">>" AutoPostBack="false">
                                <ClientSideEvents Click="AddItem" />
                            </dx:ASPxButton>
                        </td>--%>
                        <td>
                            <input type="hidden" id="ddlElements" name="ddlElements" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <table>
                                <tr>
                                    <td style="width:auto;">
                                        <div style="height: 200px; background-color: Gray; overflow: scroll;">
                                            <dx:ASPxGridView ID="grdInsuranceCompany" runat="server" Width="100%" SettingsBehavior-AllowSort="false"
                                                            SettingsPager-PageSize="20" ClientInstanceName="grdInsuranceCompany" KeyFieldName="CODE">
                                                <Columns>
                                                    <dx:GridViewDataColumn Caption="chk3" VisibleIndex="0" Width="30px">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                            ToolTip="Select All" />
                                                        </HeaderTemplate>
                                                        <DataItemTemplate>
                                                        <asp:CheckBox ID="chkall3" Visible="true" runat="server" />
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="DESCRIPTION" Caption="Insurance Company Name" VisibleIndex="1">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="CODE" Caption="Case Status ID" VisibleIndex="2" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                                <Styles>
                                                    <FocusedRow BackColor="#8C001A" ForeColor="White">
                                                    </FocusedRow>
                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                    </AlternatingRow>
                                                </Styles>
                                                <SettingsPager PageSize="1000">
                                                </SettingsPager>
                                            </dx:ASPxGridView>
                                        </div>
                                    </td>

                                    <td style="width:auto;">
                                        <div style="height: 200px; background-color: Gray; overflow: scroll;">
                                            <dx:ASPxGridView ID="grdCaseStatus" runat="server" Width="100%" SettingsBehavior-AllowSort="false"
                                                            SettingsPager-PageSize="20" ClientInstanceName="grdCaseStatus" KeyFieldName="SZ_CASE_STATUS_ID">
                                                <Columns>
                                                    <dx:GridViewDataColumn Caption="chk1" VisibleIndex="0" Width="30px">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAllCaseStatus" runat="server" onclick="javascript:SelectAllCaseStatus(this.checked);"
                                                            ToolTip="Select All" />
                                                        </HeaderTemplate>
                                                        <DataItemTemplate>
                                                        <asp:CheckBox ID="chkall1" Visible="true" runat="server" />
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_STATUS_NAME" Caption="Case Status" VisibleIndex="1">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_CASE_STATUS_ID" Caption="Case Status ID" VisibleIndex="2" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                                <Styles>
                                                    <FocusedRow BackColor="#8C001A" ForeColor="White">
                                                    </FocusedRow>
                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                    </AlternatingRow>
                                                </Styles>
                                                <SettingsPager PageSize="1000">
                                                </SettingsPager>
                                            </dx:ASPxGridView>
                                        </div>
                                    </td>
                                    <td style="width:auto;">
                                        <div style="height: 200px; background-color: Gray; overflow: scroll;">
                                            <dx:ASPxGridView ID="grdBillStatus" runat="server" Width="100%" SettingsBehavior-AllowSort="false"
                                                            SettingsPager-PageSize="20" ClientInstanceName="grdBillStatus" KeyFieldName="SZ_BILL_STATUS_ID">
                                                <Columns>
                                                    <dx:GridViewDataColumn Caption="chk2" VisibleIndex="0" Width="30px">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAllBillStatus" runat="server" onclick="javascript:SelectAllBill(this.checked);"
                                                            ToolTip="Select All" />
                                                        </HeaderTemplate>
                                                        <DataItemTemplate>
                                                        <asp:CheckBox ID="chkall2" Visible="true" runat="server" />
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_BILL_STATUS_NAME" Caption="Bill Status" VisibleIndex="1">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_BILL_STATUS_ID" Caption="Bill Status ID" VisibleIndex="2" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                                <Styles>
                                                    <FocusedRow BackColor="#8C001A" ForeColor="White">
                                                    </FocusedRow>
                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                    </AlternatingRow>
                                                </Styles>
                                                <SettingsPager PageSize="1000">
                                                </SettingsPager>
                                            </dx:ASPxGridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <%--<td align="left" class="txt2" valign="middle" rowspan="2">
                            <asp:ListBox ID="lstboxItems" runat="server" Height="184px" Width="257px"></asp:ListBox>
                            <asp:ListBox ID="lstInsuranceCompany" runat="server" SelectionMode="Multiple"
                            Height="100px" OnClientClick="javascript:unselectcheckboxforcom('chkCheck');"></asp:ListBox>
                        </td>--%>
                    </tr>
                   <%-- <tr>
                        <td align="left" class="txt2" colspan="3" valign="middle">
                        </td>
                        <td align="left" class="txt2" valign="middle">
                            <dx:ASPxButton ID="btnrermove" runat="server" Text="<<" AutoPostBack="false">
                                <ClientSideEvents Click="DeleteItem" />
                            </dx:ASPxButton>
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td style="width: 100%" valign="top" id="grdid" colspan="5" runat="server">
                        </td>
                        <td runat="server" colspan="1" style="width: 100%" valign="top" id="Td1">
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="SectionDevider" style="width: 100%">
                        </td>
                        <td class="SectionDevider" style="width: 100%">
                        </td>
                        <td class="SectionDevider" style="width: 100%">
                        </td>
                        <td class="SectionDevider" style="width: 100%">
                        </td>
                        <td class="SectionDevider" style="width: 100%">
                        </td>
                    </tr>
                    <tr style="background-color: White;">
                        <td style="width: 50%" colspan="2">
                            <div style="height: 200px; background-color: Gray; overflow: scroll;">
                                <dx:ASPxGridView ID="grdCaseStatus" runat="server" Width="100%" SettingsBehavior-AllowSort="false"
                                                SettingsPager-PageSize="20" ClientInstanceName="grdCaseStatus" KeyFieldName="SZ_CASE_STATUS_ID">
                                    <Columns>
                                        <dx:GridViewDataColumn Caption="chk1" VisibleIndex="0" Width="30px">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <DataItemTemplate>
                                            <asp:CheckBox ID="chkall1" Visible="true" runat="server" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SZ_STATUS_NAME" Caption="Case Status" VisibleIndex="1">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SZ_CASE_STATUS_ID" Caption="Case Status ID" VisibleIndex="2" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                    <Styles>
                                        <FocusedRow BackColor="#8C001A" ForeColor="White">
                                        </FocusedRow>
                                        <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                        </AlternatingRow>
                                    </Styles>
                                    <SettingsPager PageSize="1000">
                                    </SettingsPager>
                                </dx:ASPxGridView>
                            </div>
                        </td>
                        <td style="width: 50%" colspan="3">
                            <div style="height: 200px; background-color: Gray; overflow: scroll;">
                                <dx:ASPxGridView ID="grdBillStatus" runat="server" Width="100%" SettingsBehavior-AllowSort="false"
                                                SettingsPager-PageSize="20" ClientInstanceName="grdBillStatus" KeyFieldName="SZ_BILL_STATUS_ID">
                                    <Columns>
                                        <dx:GridViewDataColumn Caption="chk2" VisibleIndex="0" Width="30px">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <DataItemTemplate>
                                            <asp:CheckBox ID="chkall2" Visible="true" runat="server" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SZ_BILL_STATUS_NAME" Caption="Bill Status" VisibleIndex="1">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SZ_BILL_STATUS_ID" Caption="Bill Status ID" VisibleIndex="2" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                    <Styles>
                                        <FocusedRow BackColor="#8C001A" ForeColor="White">
                                        </FocusedRow>
                                        <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                        </AlternatingRow>
                                    </Styles>
                                    <SettingsPager PageSize="1000">
                                    </SettingsPager>
                                </dx:ASPxGridView>
                            </div>
                        </td>
                    </tr>--%>
                    <tr>
                        <td runat="server" colspan="5" style="width: 100%" valign="middle" id="Td4">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp;&nbsp;
                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox><asp:Button
                                ID="btnSearch" runat="server" Text="Run" Width="80px" OnClick="btnSearch_Click" />
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Button ID="btnclear" runat="server" Text="Clear" Width="80px" OnClick="btnclear_Click" /></td>
                        <td runat="server" colspan="1" style="width: 100%" valign="middle" id="Td5">
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td  height="28" align="left" valign="middle" class="txt2" >
                                    <asp:Label Id="lblErr" runat="server" Text="" visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                <%--Nirmal--%>
                <tablewidth="100%" style="background-color: White;">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                <%--Nirmal END--%>
                <table style="vertical-align: middle; width: 100%; background-color: White;">
                    <tbody>
                        <tr>
                            <td style="vertical-align: middle; width: 30%" align="left">
                            </td>
                            <td style="width: 60%" align="right">
                                <%-- Record Count:--%>
                                <%-- <%= this.grcollectionreport.RecordCount%>--%>
                                <%-- | Page Count:--%>
                                Export To Excel:
                                <%-- <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                        </gridpagination:XGridPaginationDropDown>--%>
                                <asp:LinkButton ID="btnExportToExcel" runat="server" Text="Export TO Excel" OnClick="btnExportToExcel_Click">
                                 <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table width="100%" style="background-color: White;">
                    <tr>
                        <td class="SectionDevider" style="width: 100%">
                            <table>
                                <tr>
                                    <td height="28" align="left" bgcolor="#B5DF82" class="txt2" style="width: 100%">
                                        <b class="txt3">Collection Report</b>
                                    </td>
                                </tr>
                                <tr style="background-color: White;">
                                    <td style="width: 100%">
                                        <div style="height: 400px; background-color: Gray; overflow: scroll;">
                                            <dx:ASPxGridView ID="grcollectionreport" runat="server" Width="100%" SettingsBehavior-AllowSort="false"
                                                SettingsPager-PageSize="20" ClientInstanceName="grcollectionreport" KeyFieldName="SZ_CASE_ID">
                                                <Columns>
                                                    <dx:GridViewDataColumn Caption="chk" VisibleIndex="0" Width="30px">
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <DataItemTemplate>
                                                            <asp:CheckBox ID="chkall" Visible="true" runat="server" />
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="PATIENT NAME" VisibleIndex="1">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="CASE NUMBER" VisibleIndex="2">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_INSURANCE_NAME" Caption="INSURANCE NAME" VisibleIndex="3">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_CLAIM_NUMBER" Caption="CLAIM NUMBER" VisibleIndex="4">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_POLICY_NO" Caption="POLICY NO" VisibleIndex="5">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_ADJUSTER_NAME" Caption="ADJUSTER NAME" VisibleIndex="6">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_PHONE" Caption="ADJUSTER PHONE" VisibleIndex="7">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="CASE ID" VisibleIndex="8" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                                                <Styles>
                                                    <FocusedRow BackColor="#8C001A" ForeColor="White">
                                                    </FocusedRow>
                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                    </AlternatingRow>
                                                </Styles>
                                                <SettingsPager PageSize="1000">
                                                </SettingsPager>
                                            </dx:ASPxGridView>
                                            <dx:ASPxGridViewExporter ID="grdExportTwo" runat="server" GridViewID="grcollectionreport">
                                            </dx:ASPxGridViewExporter>
                                            &nbsp;</div>
                                    </td>
                                    <td style="width: 771%" align="right">
                                        &nbsp;</td>
                                </tr>
                            </table>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Button ID="btnpdf" runat="server" Text="PDF" Width="70px" OnClick="btnpdf_Click" />
                            <asp:HiddenField ID="listt_comma_seprate" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ appSettings:Connection_String %>">
        <SelectParameters>
            <asp:SessionParameter Name="SZ_COMPANY_ID" SessionField="SZ_COMPANY_ID" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </table>
</asp:Content>
