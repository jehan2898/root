<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_PatientBillingReport.aspx.cs" Inherits="Bill_Sys_PatientBillingReport" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>
    
    <script>
        function OpenPage(venueid,date)
        {
            szURL = "Bill_Sys_PatientAllData.aspx?VID=" + venueid + "&DATE=" + date;
            var win = window.open(szURL,null,"height=600,width=840,status=no,toolbar=no,menubar=no,location=no");
	        win.moveTo(80,110);
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
                                
                                 <%--<tr>
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
                                                <asp:BoundColumn DataField="I_VENUE_ID" HeaderText="Venue ID" Visible="false"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Venue" >
                                            
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientData.aspx?VID=<%# DataBinder.Eval(Container,"DataItem.I_VENUE_ID")%>" >
                                                            <%#DataBinder.Eval(Container, "DataItem.SZ_VENUE")%>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="VENUE LINK" HeaderText="Venue" Visible="False"></asp:BoundColumn>
                                                 <asp:TemplateColumn HeaderText="Accident date" >
                                            
                                                    <ItemTemplate>
                                                        <a href="Bill_Sys_PatientData.aspx?DATE=<%# DataBinder.Eval(Container,"DataItem.MONTH")%>" >
                                                            <%#DataBinder.Eval(Container, "DataItem.MONTH")%>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="MONTH" HeaderText="Accident date" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PROVIDER" HeaderText="Provider"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="# OF BILLS" HeaderText="# of bills" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="CLAIM AMOUNT" HeaderText="Claim amount" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="BALANCE AMOUNT" HeaderText="Balance amount" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="OUTSTANDING %" HeaderText="Outstanding %" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
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
