<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_UpgradePatient.aspx.cs"
    Inherits="Bill_Sys_UpgradePatient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
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
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 50%">
                                                        Patient Information <a href="#anch_top">(Top)</a>
                                                    </td>
                                                    <td align="right" style="width: 50%">
                                                        <a id="hlnkShowNotes" href="#" runat="server">
                                                            Add Note</a>
                                                        <ajaxToolkit:PopupControlExtender ID="PopEx" runat="server" TargetControlID="hlnkShowNotes"
                                                            PopupControlID="pnlShowNotes" Position="Bottom" OffsetX="-420" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <asp:DataGrid ID="grdPatientList" Width="100%" CssClass="GridTable" runat="Server"
                                                AutoGenerateColumns="False">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case #" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="SectionDevider">
                                        </td>
                                    </tr>
                                    <tr>
                                    <td style="width: 100%" class="TDPart">
                                    
                                    <table id="tblTreatmentInformation" runat="server"  style="width: 100%; vertical-align:top; position:relative;" height="60px"  visible="false">
                                    <tr>
                                    <td style="float:left; vertical-align:top; position:relative; height:20px;">
                                    Treatment Information <a href="#anch_top">(Top)</a>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td style="float:left; vertical-align:top; position:relative;height:40px;width: 100%;">
                                    
                                        <asp:DataGrid ID="grdTreatment" runat="Server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="TOTAL_TREATMENTS" HeaderText="No Of Treatments Till date"
                                                    ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="WEEKLY_TREATMENT" HeaderText="Treatments in this week"
                                                    ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                    </tr>
                                    </table>
                                    
                                    <table id="tblVisitInformation" runat="server" style="width: 100%" visible="false" >
                                    <tr>
                                     <td style="float:left; vertical-align:top; position:relative;">
                                   Visit Information <a href="#anch_top">(Top)</a>
                                    </td>
                                    </tr>
                                     <tr>
                                     <td style="float:left; vertical-align:top; position:relative;width: 100%;">
                                      <asp:DataGrid ID="grdVisitInformation" Width="100%" CssClass="GridTable" runat="Server"
                                                AutoGenerateColumns="False">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridRow" />
                                                 <Columns>
                                                    <asp:BoundColumn DataField="DT_VISIT_DATE" HeaderText="Visit Date"
                                                        ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name"
                                                        ItemStyle-HorizontalAlign="Left" ></asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                    </td>
                                    </tr>
                                    </table>
                                    
                                    <table id="tblLastTreatment" runat="server" style="width: 100%; vertical-align:top; position:relative;"   visible="false">
                                    <tr>
                                     <td style="float:left; vertical-align:top; position:relative; ">
                                    Last Treatment <a href="#anch_top">(Top)</a>
                                    </td>
                                    </tr>
                                     <tr>
                                     <td style="float:left; vertical-align:top; position:relative;width: 100%;">
                                   <asp:DataGrid ID="grdDoctorTreatment" Width="100%" CssClass="GridTable" runat="Server"
                                            AutoGenerateColumns="False">
                                         <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Last Treating Doctor">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="Last Treatment Date"
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Last Treatment" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                    </tr>
                                    </table> 
                                    
                                    <table id="tblAllTreatment" runat="server" style="width: 100%; vertical-align:top; position:relative;"  visible="false">
                                    <tr>
                                     <td style="float:left; vertical-align:top; position:relative;">
                                   All Treatments <a href="#anch_top">(Top)</a>
                                    </td>
                                    </tr>
                                     <tr>
                                     <td style="float:left; vertical-align:top; position:relative;width: 100%;">
                                   <asp:DataGrid ID="grdDoctorTreatmentList" Width="100%" CssClass="GridTable" runat="Server"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Treating Doctor">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoctorName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_DOCTOR_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="DT_DATE_OF_SERVICE" HeaderText="Treatment Date" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Treatment Description"
                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                    </tr>
                                    </table> 
                                    
                                    <table id="tblBillingInformation" runat="server" style="width: 100%" visible="false">
                                    <tr>
                                    <td style="float:left; vertical-align:top; position:relative; ">
                                   Billing Information <a href="#anch_top">(Top)</a>
                                    </td>
                                    </tr>
                                     <tr>
                                     <td style="float:left; vertical-align:top; position:relative;width: 100%;">
                                   <asp:DataGrid ID="grdBillingInformation" Width="100%" CssClass="GridTable" runat="Server"
                                                AutoGenerateColumns="False">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridRow" />
                                                 <Columns>
                                                    <asp:BoundColumn DataField="Total_Paid_Bills" HeaderText="No Of Paid Bills"
                                                        ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Total_Unpaid_Bills" HeaderText="No Of Unpaid Bills"
                                                        ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                    </td>
                                    </tr>
                                    </table>
                                    
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
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlShowNotes" runat="server" Style="width: 420px; height: 220px; background-color: white;
                    border-color: SteelBlue; border-width: 1px; border-style: solid;">
                    <iframe id="Iframe2" src="Bill_Sys_PopupNotes.aspx" frameborder="0" height="220px"
                        width="420px" visible="false"></iframe>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
