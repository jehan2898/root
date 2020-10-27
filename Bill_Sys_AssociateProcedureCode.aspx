<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_AssociateProcedureCode.aspx.cs" Inherits="Bill_Sys_AssociateProcedureCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Billing System</title>
      <link href="Css/main.css" type="text/css" rel="Stylesheet" />
 <link href="Css/UI.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <th valign="top" scope="col">
     <span class="message-text">
         <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></span>
    </th>
             <th valign="top" scope="col">
                                    <asp:DataGrid ID="grdProcedureCode" runat="server"    >
                                        <FooterStyle />
                                        <SelectedItemStyle />
                                        <PagerStyle />
                                        <AlternatingItemStyle />
                                        <ItemStyle />
                                        <Columns>
                                            <asp:TemplateColumn>
                                            <ItemTemplate>
                                            
                                                <asp:CheckBox ID="chkSelect" runat="server"  />
                                            
                                            
                                            </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="Procedure Code" Visible="False"></asp:BoundColumn>
                                             <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Procedure Group" ></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code" ></asp:BoundColumn>
                                            
                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="false"></asp:BoundColumn>
                                           
                                            
                                            
                                        </Columns>
                                        <HeaderStyle />
                                    </asp:DataGrid>
                                </th>
                                 <th valign="top" scope="col">
                                  <asp:Button ID="btnAssociate" runat="server" Text="Associate" Width="80px" OnClick="btnAssociate_Click"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px"  />
                                 </th>
    </div>
    </form>
</body>
</html>
