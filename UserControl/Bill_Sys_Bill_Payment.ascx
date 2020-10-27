<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Bill_Payment.ascx.cs"
    Inherits="UserControl_Bill_Sys_Bill_Payment" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>



<table style="width: 100%">
    <tr>
        <td>
            <asp:repeater id="rptBillInfo" runat="server">
                <headertemplate>
                            <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; border: #D3D3D3 1px solid ;">
                                <tr>
                                    <td bgcolor="#D3D3D3"  style="font-size:small;border: 1px solid #848484; "  align="center">
                                      Case# 
                                    </td>

                                    <td bgcolor="#D3D3D3"  style="font-size:small;border: 1px solid #848484; "     align="center">
                                        Patient Name
                                    </td>
                                    <td bgcolor="#D3D3D3"  style="font-size:small;border: 1px solid #848484; "   id="tblheader" runat="server" align="center">
                                        Bill# 
                                    </td>
                                    <td bgcolor="#D3D3D3"   style="font-size:small;border: 1px solid #848484; " align="center">
                                        Bill Date
                                    </td>
                                    <td bgcolor="#D3D3D3"  style="font-size:small;border: 1px solid #848484; "  align="center">
                                        Specialty
                                    </td>
                                    <td bgcolor="#D3D3D3" style="font-size:small;border: 1px solid #848484; "   align="center">
                                        Bill Amout
                                    </td>

                                     <td bgcolor="#D3D3D3" style="font-size:small;border: 1px solid #848484; "  align="center">
                                        Paid
                                    </td>

                                       <td bgcolor="#D3D3D3"  style="font-size:small;border: 1px solid #848484; "  align="center">
                                        Outstanding
                                    </td>

                                </tr>
                              
                        </headertemplate>
                <itemtemplate>
             
                            <tr>
                                <td bgcolor="white" style="font-size:small;border: 1px solid #848484; " >
                                    <%# DataBinder.Eval(Container, "DataItem.SZ_CASE_NO")%>
                                </td>
                                <td bgcolor="white" style="font-size:small;border: 1px solid #848484; ">
                                    <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                </td>
                                <td bgcolor="white"  id="tblvalue" runat="server" style="font-size:small;border: 1px solid #848484; ">
                                    <%# DataBinder.Eval(Container, "DataItem.SZ_BILL_NUMBER")%>
                                </td>
                                <td bgcolor="white" style="font-size:small;border: 1px solid #848484; " align="center">
                                    <%# DataBinder.Eval(Container, "DataItem.DT_BILL_DATE", "{0:MM/dd/yyyy}")%>
                                </td>
                                <td bgcolor="white" style="font-size:small;border: 1px solid #848484; " id="tblRemoteValue"
                                    runat="server" align="center">
                                    <%# DataBinder.Eval(Container, "DataItem.SZ_PROCEDURE_GROUP")%>
                                </td>
                                 <td bgcolor="white" style="font-size:small;border: 1px solid #848484; " id="Td1" align="right"
                                    runat="server">
                                    <%# DataBinder.Eval(Container, "DataItem.FLT_BILL_AMOUNT", "{0:C}")%>
                                </td>
                                 <td bgcolor="white" style="font-size:small;border: 1px solid #848484; " id="Td2"  align="right"
                                    runat="server">
                                    <%# DataBinder.Eval(Container, "DataItem.MN_PAID", "{0:C}")%>
                                </td>
                                 <td bgcolor="white" style="font-size:small;border: 1px solid #848484; " id="Td3"  align="right"
                                    runat="server">
                                    <%# DataBinder.Eval(Container, "DataItem.FLT_BALANCE", "{0:C}")%>
                                </td>
                            </tr>
                            </table>
                        </itemtemplate>
                <footertemplate>
                </footertemplate>
            </asp:repeater>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td style="width: 100%">
            <dx:ASPxGridView ID="grdBillDetails" runat="server" KeyFieldName="SZ_BILL_NUMBER"
                AutoGenerateColumns="false" Width="100%" SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330"
                Settings-VerticalScrollableHeight="330" 
                onrowcommand="grdBillDetails_RowCommand">
                <Columns>
              
                    <%--1--%>
                    <dx:GridViewDataColumn FieldName="SZ_CHECK_NUMBER" Caption="Cheque No" HeaderStyle-HorizontalAlign="Center"
                         Settings-AllowSort="true">
                    </dx:GridViewDataColumn>
                    <%--2--%>
                    <dx:GridViewDataColumn FieldName="DT_CHECK_DATE" Caption="Cheque Date" HeaderStyle-HorizontalAlign="Center" >
                    </dx:GridViewDataColumn>

                     <%--3--%>
                    <dx:GridViewDataTextColumn FieldName="FLT_CHECK_AMOUNT" Caption="Cheque Aount" HeaderStyle-HorizontalAlign="Center"
                        Settings-AllowSort="False" Settings-AllowAutoFilter="False">
                        <PropertiesTextEdit DisplayFormatString="{0:c2}" />
                    </dx:GridViewDataTextColumn>


                    <%--4--%>
                    <dx:GridViewDataColumn FieldName="SZ_PAYMENT_TYPE" Caption="Payment Type" HeaderStyle-HorizontalAlign="Center"
                        Settings-AllowSort="False" Settings-AllowAutoFilter="False"  >
                    </dx:GridViewDataColumn>
                    <%--5--%>
                    <dx:GridViewDataColumn FieldName="DT_PAYMENT_DATE" Caption="Payment Date" HeaderStyle-HorizontalAlign="Center"
                        Settings-AllowSort="False" Settings-AllowAutoFilter="False" >
                    </dx:GridViewDataColumn>
                   
                </Columns>
            </dx:ASPxGridView>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:textbox id="txtBillNo" runat="server" visible="false">
            </asp:textbox>
            <asp:textbox id="txtCaseID" runat="server" visible="false">
            </asp:textbox>
            <asp:textbox id="txtCompanyID" runat="server" visible="false">
            </asp:textbox>
        </td>
    </tr>
</table>
