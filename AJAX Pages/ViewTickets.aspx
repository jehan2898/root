<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewTickets.aspx.cs" Inherits="ViewTickets" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="css/ticketing.css" type="text/css" />
    <script language="javascript" type="text/javascript">
        function openTicket(tid) {
            var cTid = document.getElementById('pUpdateTime_htid_I');
            cTid.value = tid;
            htid.Set("tid", cTid.value);
            pUpdateTime.PerformCallback();
        }

    </script>
    <style type="text/css">
        .thread-label
        {
            font-family:Calibri;font-size:14px;font-weight:normal;text-align:right;
            background-color:#EFEFEF;
            padding-right:5px;
            padding-top:10px;
        }
        .thread-content
        {
            font-family:Calibri;font-size:14px;font-weight:normal;
            padding-left:5px;
            background-color:#E8E8E8;
            padding-top:10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%;padding-left:0px;height:30px;" border="0">
            <tr>
                <td style="background-color:#CDCAB9;font-family:Calibri;font-size:20px;font-weight:normal;font-style:italic;">
                    View Ticket(s)
                </td>
            </tr>
        </table>

        <table style="padding-top:5px;" border="0">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>

        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" 
            meta:resourcekey="ASPxCallbackPanel1Resource1">
            <PanelCollection>
                <dx:PanelContent runat="server" meta:resourcekey="PanelContentResource1">
                    <table width="100%" border="0">
                        <tr>
                            <td style="width:100%;text-align:center;font-family:Calibri;font-size:14px;">
                                <dx:ASPxLabel runat="server" id="lblErrorMessage" ForeColor="Red" 
                                    meta:resourcekey="lblErrorMessageResource1"></dx:ASPxLabel>
                                <dx:ASPxLabel runat="server" id="lblMessage" ForeColor="Green" 
                                    meta:resourcekey="lblMessageResource1" ></dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100%;text-align:center;font-family:Calibri;font-size:14px;">
                                <dx:ASPxLabel runat="server" id="lblTicketNumber" ForeColor="DarkBlue" 
                                    meta:resourcekey="lblMessageResource1" ></dx:ASPxLabel>
                            </td>
                        </tr>
                    </table>

                    <!-- list of tickets -->
                    <table style="width:100%">
                        <tr>
                            <td>
                                <dx:ASPxGridView 
                                    runat="server" ID="grdTickets" Width="100%" 
                                    KeyFieldName="TicketID"
                                    meta:resourcekey="grdTicketsResource1">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="TicketID" Visible="false"></dx:GridViewDataColumn>
                                        
                                        <dx:GridViewDataHyperLinkColumn 
                                            FieldName="TicketNumber"
                                            Caption="Ticket #" Width="60px" 
                                            meta:resourcekey="GridViewDataColumnResource1">
                                            <DataItemTemplate>
                                                <a href="javascript:openTicket('<%# DataBinder.Eval(Container.DataItem, "TicketID") %>');"><%# DataBinder.Eval(Container.DataItem, "TicketNumber")%></a>
                                            </DataItemTemplate>
                                        </dx:GridViewDataHyperLinkColumn>
                                        
                                        <dx:GridViewDataDateColumn 
                                            FieldName = "RaisedOn"
                                            Caption="Created On" Width="40px" 
                                            meta:resourcekey="GridViewDataColumnResource2">
                                            <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy hh:mm tt">
                                            </PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                        
                                        <dx:GridViewDataColumn 
                                            FieldName = "Subject"
                                            Caption="Subject" CellStyle-Wrap="True" Width="200px" 
                                            meta:resourcekey="GridViewDataColumnResource3">
                                            <CellStyle Wrap="True"></CellStyle>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn 
                                            FieldName = "Status"
                                            Caption="Status" Width="60px">
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataColumn FieldName = "StatusID" Visible="false"></dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName = "CompanyID" Caption="Close Ticket" Visible="false" ></dx:GridViewDataColumn>
                                    </Columns>
                                    <SettingsPager PageSize="5"></SettingsPager>
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>

        <dx:ASPxCallbackPanel ID="pUpdateTime" runat="server" ClientInstanceName="pUpdateTime" OnCallback="onUpdateTime_CallBack" Visible="true">
            <PanelCollection>
                <dx:PanelContent>
                    <table style="width:100%">
                        <tr>
                            <td style="width:10%">
                                Reply
                                <span class="mandatory-asterix">*</span>
                            </td>
                            <td style="width:90%">
                                <dx:ASPxMemo
                                    CssClass="inputBox" 
                                    runat="server"
                                    ID="tDescription" 
                                    Height="30px"
                                    Rows="10"
                                    Width="100%"></dx:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center;padding-left:45%">
                                <table style="width:30%">
                                    <tr>
                                        <td style="padding-bottom:10px;">
                                            <dx:ASPxButton ID="btnReply" Text = "Post a Reply" runat="server" OnClick="btnReply_Click"></dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-bottom:10px;">
                                            <dx:ASPxButton ID="btnClose" Text = "Close Ticket" runat="server" OnClick="btnClose_Click"></dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%">
                        <tr>
                            <td style="width:100%;padding-top:20px;height:30px;border-bottom: 1px solid;border-top:0px;font-family:Calibri;font-size:18px;font-style:italic;">
                                Ticket History
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%;" cellspacing="0" cellpadding="0">
                        <asp:Repeater ID="rThread" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td class="thread-label">
                                        Updated On
                                    </td>
                                    <td class="thread-content">
                                        <%# DataBinder.Eval(Container.DataItem, "RaisedOn")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="thread-label">
                                        Raised By
                                    </td>
                                    <td class="thread-content">
                                        <%# DataBinder.Eval(Container.DataItem, "RaisedBy")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="thread-label">
                                        Subject
                                    </td>
                                    <td class="thread-content">
                                        <%# DataBinder.Eval(Container.DataItem, "Subject")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="thread-label">
                                        Status
                                    </td>
                                    <td class="thread-content">
                                        <%# DataBinder.Eval(Container.DataItem, "Status")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="thread-label">
                                        Description
                                    </td>
                                    <td class="thread-content">
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <dx:ASPxLabel ID="lDateTime" runat="server"></dx:ASPxLabel>
                    <dx:ASPxHiddenField ID="htid" ClientInstanceName="htid" runat="server"></dx:ASPxHiddenField>                    
                </dx:PanelContent>
            </PanelCollection>
            <ClientSideEvents BeginCallback="updateDatetime" />
        </dx:ASPxCallbackPanel>
    </form>
</body>
</html>
