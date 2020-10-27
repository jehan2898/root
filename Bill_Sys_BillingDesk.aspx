<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_BillingDesk.aspx.cs" Inherits="Bill_Sys_BillingDesk" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="validation.js"></script>
    
    <script>
        function showviewBills()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.height='180px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.visibility = 'visible';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.position = "absolute";
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.top = '250px';
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.left ='400px';
       }
        
       function CloseviewBills()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.height='0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.visibility = 'hidden';  
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
                        <td valign="top" class="TDPart">
                            <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%">
                                 <tr>
                                    <td>
                                        <a id="hlnkShowDiv" href="#" onclick="return ShowDiv();" runat="server" visible="false">
                                                        Dash board</a>
                                    </td>
                                </tr>
         
                                <tr>
                                    <td width="100%">
                                        <table width="100%">  
                                            <tr>    
                                                 <td width="50%" class="ContentLabel" style="text-align:left;">
                                                    <b>Billing Desk</b>
                                                </td>
                                            
                                                <td width="50%" align="right">
                                                    <asp:Button id="btnExportToExcel" runat="server" cssclass="Buttons" Text="Export To Excel" OnClick="btnExportToExcel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 302px;" >
                                    <div style="overflow: scroll; height: 300px; width: 100%;">
                                        <asp:DataGrid ID="grdLitigationDesk" runat="server" OnItemCommand="grdLitigationDesk_ItemCommand"
                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" AllowPaging="true"
                                            PageSize="10" PagerStyle-Mode="NumericPages" OnItemDataBound="grdLitigationDesk_ItemDataBound" OnPageIndexChanged="grdLitigationDesk_PageIndexChanged" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <%--0--%>
                                                <asp:BoundColumn DataField="BILL NUMBER" HeaderText="Bill Number">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%--1--%>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="File Number" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%--2--%>
                                                <asp:BoundColumn DataField="CASE ID" HeaderText="Case #" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%--3--%>
                                                <asp:BoundColumn DataField="INSURANCE COMPANY" HeaderText="Insurance Company">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <%--4--%>
                                                <asp:BoundColumn DataField="BILL AMOUNT" HeaderText="Bill Amount" DataFormatString="{0:0.00}" Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <%--5--%>
                                                <asp:BoundColumn DataField="PAID AMOUNT" HeaderText="Paid Amount" DataFormatString="{0:0.00}" Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <%--6--%>
                                                <asp:BoundColumn DataField="LITIGATION AMOUNT" HeaderText="Litigation Amount" DataFormatString="{0:0.00}" Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <%--7--%>
                                                <asp:BoundColumn DataField="BILL DATE" HeaderText="Bill Date">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <%--8--%>
                                                <asp:BoundColumn DataField="REASON" HeaderText="Reason">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <%--9--%>
                                                <asp:TemplateColumn HeaderText="Documents">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDocumentManager" runat="server" Text="Add Bills" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.CASE ID")%>'
                                                            CommandName="Document Manager"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--10--%>
                                                <asp:BoundColumn DataField="SZ_LEGAL_FIRM" HeaderText="Assigned Firm">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <%--11--%>
                                                <asp:TemplateColumn HeaderText="Select">
                                                    <ItemTemplate>
                                                         <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--12--%>
                                                <asp:BoundColumn DataField="TRANSFER_STATUS" HeaderText="Transfer Status">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <PagerStyle Mode="NumericPages" />
                                        </asp:DataGrid>
                                    </div>
                                        
                                        <asp:DataGrid ID="grdForReport" runat="server" Width="100%" CssClass="GridTable" AutoGenerateColumns="false" Visible="false"  >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="BILL NUMBER" HeaderText="Bill Number">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="CASE ID" HeaderText="File Number" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="CASE ID" HeaderText="Case #" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="INSURANCE COMPANY" HeaderText="Insurance Company">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="BILL AMOUNT" HeaderText="Bill Amount" DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="PAID AMOUNT" HeaderText="Paid Amount" DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="LITIGATION AMOUNT" HeaderText="Litigation Amount" DataFormatString="{0:0.00}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="REASON" HeaderText="Reason">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Documents">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDocumentManager" runat="server" Text="Add Bills" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.CASE ID")%>'
                                                            CommandName="Document Manager"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="SZ_LEGAL_FIRM" HeaderText="Assigned Firm">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Select">
                                                    <ItemTemplate>
                                                         <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="TRANSFER_STATUS" HeaderText="Transfer Status">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <PagerStyle Mode="NumericPages" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Law Firm :
                                        <extddl:ExtendedDropDownList ID="extddlUserLawFirm" runat="server" Width="150px"
                                                            Selected_Text="---Select---" Procedure_Name="SP_MST_LEGAL_LOGIN" Flag_Key_Value="GET_USER_LIST"
                                                            Connection_Key="Connection_String" Maintain_State="true"></extddl:ExtendedDropDownList>
                                        <asp:Button ID="btnAssign" Text="Assign" runat="server" OnClick="btnAssign_Click" CssClass="Buttons"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                            <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
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
    
    <div id="divDashBoard" visible="false" style="position: absolute; width: 600px; height: 480px;
        background-color: #DBE6FA; visibility: hidden; border-right: silver 1px solid;
        border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="document.getElementById('divDashBoard').style.visibility='hidden';" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                    padding-top: 3px; height: 100%" valign="top">
                    <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
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
                            <td class="Center" valign="top" width="45%">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td class="TDHeading" style="width: 100%">
                                            Today's Appointment</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <asp:Label ID="lblAppointmentToday" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                          
                            <td class="Center" width="45%" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td class="TDHeading" style="width: 100%">
                                            Weekly &nbsp;Appointment</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <asp:Label ID="lblAppointmentWeek" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="Center" valign="top" width="45%">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td class="TDHeading" style="width: 100%">
                                            Bill Status</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            You have &nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="SectionDevider">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                          
                            <td class="Center" width="45%" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td class="TDHeading" style="width: 100%">
                                            Desk</td>
                                    </tr>                                     
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                           You have &nbsp; <asp:Label ID="lblDesk" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                        
                            <td class="Center" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td class="TDHeading" style="width: 100%">
                                            Missing Information</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            You have &nbsp;<asp:Label ID="lblMissingInformation" runat="server"></asp:Label>
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
    </div>
    
     <asp:Panel ID="pnlviewBills" runat="server" Style="width: 450px; height: 0px; background-color: white;
        border-color: ThreeDFace; border-width: 1px; border-style: solid; visibility: hidden;">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="right" valign="top">
                   <table width="100%">
                        <tr>
                            <td width="80%" align="left">
                                List of Bills
                            </td>
                            <td width="20%" align="right">
                                <a onclick="CloseviewBills();" style="cursor: pointer;" title="Close">X</a>
                            </td>
                        </tr>
                   </table>
                </td>
            </tr>
            <tr>
                <td style="width: 102%" valign="top">
                    <div style="height: 150px; overflow-y: scroll;">
                        <asp:DataGrid ID="grdViewBills" runat="server" Width="97%" CssClass="GridTable"
                            AutoGenerateColumns="false" >
                            <ItemStyle CssClass="GridRow" />
                            <HeaderStyle CssClass="GridHeader" />
                            <Columns>
                                <asp:BoundColumn DataField="VERSION" HeaderText="Version" ItemStyle-HorizontalAlign="left"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="File Path"> 
                                    <ItemTemplate>
                                        <a href="<%# DataBinder.Eval(Container,"DataItem.PATH")%>"
                                            target="_blank"><%# DataBinder.Eval(Container, "DataItem.FILE_NAME")%></a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CREATION_DATE" HeaderText="Date Created" ItemStyle-HorizontalAlign="left" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                            </Columns>
                            
                            
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
</asp:Content>
