<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_MissingProcedureReport.aspx.cs" Inherits="Bill_Sys_MissingProcedureReport"
    Title="Untitled Page" %>
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
                                                    <b>Missing Procedure Report</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <%--<td class="ContentLabel" style="width: 15%" valign="top">
                                                    Doctor&nbsp;
                                                </td>
                                                <td style="width: 25%; height: 18px;">
                                                    <cc1:ExtendedDropDownList id="extddlDoctor" runat="server" Width="97%" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                                    </td>--%>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;" valign="top">
                                                    Specialty&nbsp;
                                                </td>
                                                <td style="width: 25%; height: 18px;" valign="top">
                                                    <cc1:ExtendedDropDownList id="extddlSpeciality" runat="server" Width="97%" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_REFERRAL_PROC_GROUP" Procedure_Name="SP_MST_PROCEDURE_GROUP" Selected_Text="---Select---" OnextendDropDown_SelectedIndexChanged="extddlSpeciality_extendDropDown_SelectedIndexChanged" AutoPost_back = "true"/>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;" valign="top">
                                                    Procedure Code&nbsp;
                                                </td>
                                                <td style="width: 25%; height: 18px;" valign="top">
                                                    <asp:DropDownList ID="ddlProcedureCode" runat="server" Width="97%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 18px" valign="top">
                                                    <asp:Label ID="lblLocationName" runat="server" CssClass="lbl" Text="Location Name"
                                                        Visible="False" Width="94px"></asp:Label></td>
                                                <td style="width: 35%">
                                                    <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="65%" Connection_Key="Connection_String" Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---" Visible="false"/>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 18px" valign="top">
                                                </td>
                                                <td style="width: 25%; height: 18px" valign="top">
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click1" /> &nbsp; <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                OnClick="btnExportToExcel_Click" />
                                                <asp:Button ID="btnExcludeProcedure" runat="server" CssClass="Buttons" Text="Exclude Procedure" OnClick="btnExcludeProcedure_Click1"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                   
                                <tr>
                                    <td style="width: 100%;" class="TDPart" valign="top">

                                        <asp:DataGrid ID="grdMissingProcedure" runat="Server" AutoGenerateColumns="False" CssClass="GridTable"
                                            Width="100%">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_NAME" HeaderText="Provider Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="Patient Phone"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_FIRST_TREATMENT" HeaderText="First Treatment"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Accident Date"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_VISIT_DATE" HeaderText="Last Visit Date"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_SPECIALITY" HeaderText="Specialty"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="I_ASSOCIATE_ID" HeaderText="Associate ID" Visible = "false"></asp:BoundColumn>
                                                 <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%" style="text-align:left;" class="TDPart">
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
