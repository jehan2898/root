<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_PatientData.aspx.cs" Inherits="Bill_Sys_PatientData" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
    
    
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>
    
    <script>
        function openPage(obj)
        {
            szURL = "Bill_Sys_PatientAllData.aspx?ID=" + obj;
            var win = window.open(szURL,null,"height=500,width=990,status=no,toolbar=no,menubar=no,location=no");
	        win.moveTo(0,100);
        }
    </script>
     
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
                                
                                <%-- <tr>
                                    <td class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Docket #&nbsp;</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtDocketNo" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Claim #&nbsp;</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtClaimNo" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Insurance Company</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtInsuranceComapany" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Venue&nbsp;</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtVenue" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons" OnClick="btnSearch_Click"
                                                        />
                                                   
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                 </tr>--%>
                            
                                 <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdPatientData" AutoGenerateColumns="false" runat="server" Width="100%"
                                            CssClass="GridTable"  >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Patient">
                                                     <ItemTemplate>
                                                        <a href="#" onclick='<%# "openPage(" +Eval("SZ_WCB_NO") + " );" %>'  >
                                                        
                                                        <%#  Eval("SZ_PATIENT_NAME")%>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_INSURER" HeaderText="Insurance company"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PROVIDER" HeaderText="Provider"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DOCKET_NO" HeaderText="Docket #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CLAIM_NO" HeaderText="Claim #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DATE_OF_ACCIDENT" HeaderText="Date of accident"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CLAIM_AMT" HeaderText="Claim Amount" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_BALANCE_AMT" HeaderText="Balance Amount" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_WCB_NO" HeaderText="SZ_WCB_NO" Visible="false"></asp:BoundColumn>
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
</asp:Content>
