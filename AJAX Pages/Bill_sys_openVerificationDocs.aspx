<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_sys_openVerificationDocs.aspx.cs" Inherits="AJAX_Pages_Bill_sys_openVerificationDocs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="float: left; width: 28%">
                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Merge" Width="100%">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dx:ASPxGridView ID="grddocumnetmanager" ClientInstanceName="grddocumnetmanager"
                                            runat="server" AutoGenerateColumns="False" KeyFieldName="Filename"
                                            Width="100%">
                                            <Columns>
                                                <dx:GridViewDataButtonEditColumn Caption="File Name"
                                                    Width="307px" VisibleIndex="3">
                                                    <DataItemTemplate>
                                                        <a target="imageframe" href="<%#Eval("Link")%>" onclick="VisibleFrame();">
                                                            <%#Eval("Filename")%>
                                                        </a>
                                                    </DataItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                    <CellStyle HorizontalAlign="Left">
                                                    </CellStyle>
                                                </dx:GridViewDataButtonEditColumn>
                                                <dx:GridViewDataColumn FieldName="File_Path" Caption="File Path"
                                                    Width="240px" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" BackColor="#B5DF82" Font-Bold="True" />
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <SettingsPager Visible="False" PageSize="1000">
                                            </SettingsPager>
                                            <SettingsBehavior AllowSelectByRowClick="True" AllowFocusedRow="True" />
                                            <Settings ShowVerticalScrollBar="True" ShowHorizontalScrollBar="True" VerticalScrollableHeight="1000" />
                                            <Styles>
                                                <FocusedRow BackColor="#99CCFF">
                                                </FocusedRow>
                                                <AlternatingRow Enabled="True">
                                                </AlternatingRow>
                                                <Header BackColor="#99CCFF">
                                                </Header>
                                            </Styles>
                                            <SettingsCustomizationWindow Height="600px" />
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
            </div>
            <div style="float: right; width: 70%; height: 100%;">
                <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="View">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel1" Height="1054px" ClientInstanceName="CallbackPanel">
                                <ClientSideEvents EndCallback="OnEndCallback"></ClientSideEvents>
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent3" runat="server">
                                        <div id="divmyiframe">
                                            <iframe name="imageframe" id="myiframe" runat="server" frameborder="0" style="height: 1000px;
                                                width: 890px;"></iframe>
                                        </div>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
            </div>
    </div>
    </form>
</body>
</html>

