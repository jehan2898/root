<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditCases.aspx.vb" Inherits="Search_EditCases" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<HEAD>
		<title>Cases List</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href='<%=Page.ResolveUrl("~/CssAndJs/DemoStyles.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/Main.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/AxpStyleXPGrid3.css")%>' type="text/css" rel="stylesheet">
        <link href='<%=Page.ResolveUrl("~/CssAndJs/XP/LinkSelector.css")%>' type="text/css" rel="stylesheet">
        <link href="../CssAndJs/DocMgrCss.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/CssAndJs/milonic_src.js")%>'></script>
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/CssAndJs/mmenudom.js")%>'></script>
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/CssAndJs/menu_data.js")%>'></script>
        <script type="text/javascript" src='<%=Page.ResolveUrl("~/CssAndJs/script.js")%>'></script>
        
        
        <script type="text/javascript">
         
         function OnclickPlainTiff()
         {         
             document.getElementById('trFN').style.display ='block';
             document.getElementById('trLN').style.display  = 'block';
             document.getElementById('trButton').style.display ='block';
             
             return false;
         } 
         
         function OnclickShowPlainTiff()
         {
            document.getElementById('txtClientFirstName').value="";
            document.getElementById('txtClientLastName').value="";
            document.getElementById('btnAddClients').value="Add Client";
            document.getElementById('hndOperationType').value="Add";
            OnclickPlainTiff();
            return false;
         }  
         
         function CheckNull()
         {
            
            //alert(document.getElementById('txtClientLastName').value.length);
            if((document.getElementById('txtClientFirstName').value.length==0) || (document.getElementById('txtClientLastName').value.length==0))
            {
                alert("Clients Firstname & Lastname could not be blank");
                return false;
            }
         }     
         
         </script>
	</HEAD>
<body>
    <form id="form1" runat="server">
    <div>
        <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" />
        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="htblMain" runat="server">
          <tr>
            <td height="27" bgcolor="#E9E9E9"><asp:label id="lblPageHeader" runat="server" CssClass="black11arial">Home - Add Case</asp:label></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
          </tr>
          <tr >
            <td>
	            <table id="Table14" cellSpacing="0" cellPadding="0" width="100%" border="0">
		            <tr>
			            <td class="defaultText" height="49" align="center">
			            <table  width="100%">
			                <tr height="20px">
			                    <td></td>
			                    <td style="width: 761px"></td>
			                    <td></td>
			                </tr>
			                <tr>
			                    <td width="20%">
			                    </td>
			                    <td style="width: 761px" >
			                   <%-- <TABLE id="Table2" class="grid" height=100 cellSpacing=1 cellPadding=1 width="100%" border=0><TBODY>--%>
			                        <table  width="100%">
			                        <TR>
			                            <TD style="WIDTH: 146px;" align="left"><asp:label id="Label100" runat="server" CssClass="esnav" Width="154px">Case ID</asp:label></TD>
			                            <TD width=132 align="left">
			                            <asp:textbox id="txtCaseID" runat="server" CssClass="defaultText"></asp:textbox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCaseID"
                                                ErrorMessage="Case ID is mandatory !!" Width="163px"></asp:RequiredFieldValidator></TD>
			                        </TR>
			                        <TR>
			                            <TD style="WIDTH: 146px" align="left"><asp:label id="Label13" runat="server" CssClass="esnav" Width="154px" DESIGNTIMEDRAGDROP="1363">Provider</asp:label></TD>
			                            <TD width=132 align="left"><asp:textbox id="txtProvider" runat="server" CssClass="defaultText" Width="383px" DESIGNTIMEDRAGDROP="1771"></asp:textbox></TD>
			                        </TR>
			                        <TR>
			                            <TD style="WIDTH: 146px; height: 10px;" align="left"><asp:label id="Label14" runat="server" CssClass="esnav" Width="176px" DESIGNTIMEDRAGDROP="1364">Insurance Company</asp:label></TD>
			                            <TD width=132 style="height: 10px" align="left"><asp:textbox id="txtInsuranceCompany" runat="server" CssClass="defaultText" Width="383px"></asp:textbox></TD>
			                        </TR>
			                        <TR>
			                            <TD style="WIDTH: 146px" height=10 align="left"><asp:Label id="Label1" runat="server" CssClass="esnav" Width="181px" DESIGNTIMEDRAGDROP="1364">Case Description</asp:Label></TD>
			                            <TD width=132 height=10 align="left"><asp:TextBox id="txtCaseDescription" runat="server" CssClass="defaultText" Width="383px" TextMode="MultiLine"></asp:TextBox></TD>
			                        </TR>
			                        <TR>
			                            <TD style="WIDTH: 146px" height=10 align="left"><asp:Label id="Label5" runat="server" CssClass="esnav" Width="179px">Accident Date</asp:Label></TD>
			                            <TD width=132 height=10 align="left"><asp:TextBox id="txtAccidentDate" runat="server" CssClass="defaultText"></asp:TextBox></TD>
			                        </TR>
			                        <TR>
			                            <TD style="WIDTH: 146px; height: 10px;" align="left"><asp:Label id="Label7" runat="server" CssClass="esnav" Width="184px">Insurance Claim #</asp:Label></TD>
			                            <TD width=132 style="height: 10px" align="left"><asp:TextBox id="txtClaimNo" runat="server" CssClass="defaultText"></asp:TextBox></TD>
			                        </TR>
			                        <TR>
			                            <TD style="WIDTH: 146px" height=10 align="left"><asp:Label id="Label9" runat="server" CssClass="esnav" Width="179px">Claim Amount</asp:Label></TD>
			                            <TD width=132 height=10 align="left"><asp:TextBox id="txtClaimAmount" runat="server" CssClass="defaultText"></asp:TextBox></TD>
			                        </TR>
			                        <TR>
			                            <TD style="WIDTH: 146px" height=10 align="left"><asp:Label id="Label2" runat="server" CssClass="esnav" Width="177px">Paid Amount</asp:Label></TD>
			                            <TD width=132 height=10 align="left"><asp:TextBox id="txtPaidAmount" runat="server" CssClass="defaultText"></asp:TextBox></TD>
			                        </TR>
			                        <TR>
			                            <TD style="WIDTH: 146px" height=10 align="left"><asp:label id="Label15" runat="server" CssClass="esnav" Width="180px">Service Start Date</asp:label></TD>
			                            <TD width=132 height=10 align="left"><asp:textbox id="txtServiceStartDate" runat="server" CssClass="defaultText"></asp:textbox></TD>
			                        </TR>
			                        <TR>
			                            <TD style="WIDTH: 146px" height=10 align="left"><asp:Label id="Label3" runat="server" CssClass="esnav" Width="179px">Service End Date</asp:Label></TD>
			                            <TD width=132 height=10 align="left"><asp:TextBox id="txtServiceEndDate" runat="server" CssClass="defaultText"></asp:TextBox></TD>
			                        </TR>
			                        <TR>
			                            <TD style="WIDTH: 146px" height=10 align="left"><asp:label id="Label12" runat="server" CssClass="esnav" Width="179px">Status</asp:label></TD>
			                            <TD width=132 height=10 align="left"><asp:dropdownlist id="ddlStatus" runat="server" CssClass="defaultText" Width="146px">
                                            <asp:ListItem>Open</asp:ListItem>
                                            <asp:ListItem>Close</asp:ListItem>
                                        </asp:dropdownlist></TD>
			                        </TR>
                                        <tr>
                                            <td align="left" style="width: 146px; height: 10px">
                                            </td>
                                            <td align="left" style="height: 10px" width="132">
                                                <asp:LinkButton ID="lnkPlaintiff" runat="server" CssClass="esnav"
                                            Width="139px" >Add PlainTiff</asp:LinkButton></td>
                                        </tr>
			                        <TR id ="trFN" style="display:none" runat="server">
			                            <TD style="WIDTH: 146px" height=10 align="left"><asp:label id="Label4" runat="server" CssClass="esnav" Width="165px" DESIGNTIMEDRAGDROP="1829">Client's First Name</asp:label></TD>
			                            <TD width=132 height=10 align="left"><asp:textbox id="txtClientFirstName" runat="server" CssClass="defaultText" DESIGNTIMEDRAGDROP="569"></asp:textbox></TD>
			                        </TR>
			                        <TR id="trLN" style="display:none" runat="server">
			                            <TD style="WIDTH: 146px; HEIGHT: 10px" align="left"><asp:label id="Label6" runat="server" CssClass="esnav" Width="165px" DESIGNTIMEDRAGDROP="1102">Clients Last Name</asp:label></TD>
			                            <TD style="HEIGHT: 10px" width=132 align="left"><asp:textbox id="txtClientLastName" runat="server" CssClass="defaultText" DESIGNTIMEDRAGDROP="570"></asp:textbox>
                                        </TD>
			                        </TR>
                                        <tr id="trButton" style="display:none" runat="server">
                                            <td align="left" style="width: 146px; height: 10px">
                                            </td>
                                            <td align="left" style="height: 10px" width="132">
                                            <asp:Button ID="btnAddClients" runat="server" CssClass="box" Text="Add Client" Width="82px" /></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 146px; height: 10px">
                                            </td>
                                            <td align="left" style="height: 10px" width="132">
                                            </td>
                                        </tr>
			                        <TR>
			                            <TD style="WIDTH: 146px; HEIGHT: 10px" align=right></TD>
			                            <TD style="HEIGHT: 10px" width=132 align="left"><asp:button id="btnSave" runat="server" CssClass="esnav" Width="54px" Text="Submit"></asp:button> <asp:button id="btnReset" CausesValidation="false" runat="server" CssClass="esnav" Text="Reset"></asp:button></TD>
                                        <input id="hndOperationType" runat ="server" type="hidden"/></TR>
			                        <TR>
			                            <TD style="WIDTH: 146px; height: 10px;"></TD>
			                            <TD width=132 style="height: 10px">
                                            &nbsp;</TD>
			                        </TR>
			                       <%--</TBODY>--%>
			                      </TABLE>
                                    <asp:DataGrid ID="dgCasePlainTiff" CssClass="esnav" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" Width="635px">
                                        <FooterStyle BackColor="#5D7B9D" Font-Size="Medium" Font-Bold="True" ForeColor="White" />
                                        <EditItemStyle  CssClass="esnav" BackColor="#999999" />
                                        <SelectedItemStyle CssClass="esnav" BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" CssClass="esnav" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingItemStyle CssClass="esnav" BackColor="White" ForeColor="#284775" />
                                        <ItemStyle CssClass="esnav" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Size="Medium" Font-Bold="True" ForeColor="White" />
                                        
                                        <Columns>
                                            <asp:BoundColumn DataField="ID"  HeaderText="ID" Visible="False" >
                                                <ItemStyle CssClass="esnav" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="FirstName" HeaderText="First Name">
                                                <ItemStyle CssClass="esnav" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="LastName" HeaderText="Last Name">
                                                <ItemStyle CssClass="esnav" />
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="Select" Text="Select">
                                                <ItemStyle CssClass="esnav" />
                                            </asp:ButtonColumn>
                                            <asp:ButtonColumn CommandName="Delete" Text="Delete">
                                                <ItemStyle CssClass="esnav" />
                                            </asp:ButtonColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Width="408px"></asp:Label></td> 			                    			                                
			                                <td style="width: 736px"> </td>
			                                
			                           
			                    <td style="width: 20%">
			                    </td>
			                </tr>
			            </table>
			            </td>
		            </tr>
	            </table>
            </td>
            </tr>
        </table>						
    </div>
    </form>
</body>
</html>
