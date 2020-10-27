<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="FillGrid.aspx.cs" Inherits="FillGrid" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
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
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Bill No &nbsp;&nbsp;</td>
                                                <td style="width: 35%">
                                                   <asp:TextBox ID="txtbillno" runat="server" ></asp:TextBox>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Case No &nbsp;
                                                </td>
                                               <td>
                                                <asp:TextBox ID="txtCaseNo" runat="server" ></asp:TextBox>
                                               </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%" valign="top">
                                                    Doctor Name&nbsp;
                                                </td>
                                                <td style="width: 25%; height: 18px;">
                                                    <cc1:extendeddropdownlist id="extddlDoctor" runat="server" width="270px" connection_key="Connection_String"
                                                        flag_key_value="GETDOCTORLIST" procedure_name="SP_MST_DOCTOR" selected_text="---Select---" />
                                                    &nbsp;
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;" valign="top">
                                                    Specialty&nbsp;
                                                </td>
                                               <td style="width: 25%; height: 18px;" valign="top">
                                                    <cc1:extendeddropdownlist id="extddlSpeciality" runat="server" connection_key="Connection_String"
                                                        procedure_name="SP_MST_PROCEDURE_GROUP" flag_key_value="GET_PROCEDURE_GROUP_LIST"
                                                        selected_text="---Select---" width="175px">
                                                </cc1:extendeddropdownlist>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    <asp:Label ID="lblLocationName" runat="server" CssClass="lbl" Text="Location Name"
                                                        Visible="False" Width="94px"></asp:Label></td>
                                               <td style="width: 35%">
                                                    <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="54%" Connection_Key="Connection_String" Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" Visible="false"/>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons" OnClick="btnSearch_Click"
                                                         />&nbsp;<asp:Button id="btnExportToExcel" runat="server" cssclass="Buttons" Text="Export To Excel" OnClick="btnExportToExcel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                                <%--<tr>
                                    <td style="width: 100%" class="TDPart">
                                        <div style="width: 50%; text-align: left;" class="ContentLabel">
                                            Total Count :
                                            <asp:Label ID="lblCount" Font-Bold="true" Font-Size="10" runat="server"></asp:Label>
                                            &nbsp; <a id="hlnkShowCount" href="#" runat="server" title="Total Count By Speciality"
                                                class="lbl">Total Count By Speciality</a>
                                            <!--<img src="Images/actionEdit.gif" style="border-style: none;" /> -->
                                            <ajaxToolkit:PopupControlExtender ID="PopEx" runat="server" TargetControlID="hlnkShowCount"
                                                PopupControlID="pnlShowCount" Position="Center" OffsetX="100" OffsetY="10" />
                                            <asp:Panel ID="pnlShowCount" runat="server" Style="background-color: white; border-color: SteelBlue;
                                                border-width: 1px; border-style: solid;">
                                                <asp:DataGrid ID="grdCount" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                                                    <ItemStyle CssClass="GridRow" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count"></asp:BoundColumn>
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHeader" />
                                                </asp:DataGrid>
                                            </asp:Panel>
                                        </div>
                                        <div style="text-align: right;">
                                            <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                OnClick="btnExportToExcel_Click" />
                                        </div>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="width: 100%; height: 157px;" class="TDPart">
                                        <div style="text-align:right;">
                                    
                                    </div>
                                    <br />
                                        <asp:DataGrid ID="grdBillSearch" runat="Server" AutoGenerateColumns="False"  CssClass="GridTable" Width="100%">
                                        <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="Bill no" HeaderText="Bill Id"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Case no" HeaderText="Case #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Patient Name" HeaderText="Patient Name">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Ins Company" HeaderText="Insurance Name">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Bill Date" DataFormatString="{0:dd MMM yyyy}" HeaderText="Bill Date">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Bill Status" HeaderText="Bill Status"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Speciality" HeaderText="Specialty"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="No Of days" HeaderText="No Of Days">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="First visit date" HeaderText="First visit date">
                                                </asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
