<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseBills.aspx.cs" Inherits="CaseVisits" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="CSS/rp.css" />
</head>
<body>
    <form id="form1" runat="server">
        <table id="page-title">
            <tr>
                <td>
                    Patient Bill(s)
                </td>
            </tr>
        </table>

        <table style="position:absolute;padding-top:30px;width:100%">
            <tr>
                <td>
                    <dx:ASPxGridView 
                        ID="grdBills"
                        runat="server" 
                        KeyFieldName="i_event_id" 
                        AutoGenerateColumns="False" 
                        SettingsPager-PageSize="20" 
                        SettingsCustomizationWindow-Height="330"
                        Settings-VerticalScrollableHeight="330" Width="97%">
                        
                        <Columns>
                            <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Visible="false"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Visible="false"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Visible="false"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_PATIENT_ID" Visible="false"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="I_EVENT_ID" Visible="false"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE_ID" Visible="false"></dx:GridViewDataColumn>
                    
                            <dx:GridViewDataColumn FieldName="SZ_CASE_NO" Caption="Case #" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_BILL_NUMBER" Caption="Bill #" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="Patient" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_INSURANCE_NAME" Caption="Insurance" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataDateColumn FieldName="DT_BILL_DATE" Caption="Billed On" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" >
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn FieldName="FLT_BILL_AMOUNT" Caption="$ Billed" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                <PropertiesTextEdit DisplayFormatString="{0:c}">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="PAID_AMOUNT" Caption="$ Paid" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                <PropertiesTextEdit DisplayFormatString="{0:c}">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataColumn FieldName="SPECIALITY" Caption="Specialty" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_BILL_STATUS_NAME" Caption="Status" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                        </Columns>
                        
                        <SettingsBehavior AllowFocusedRow="True" AllowSort="False" />
                        <SettingsPager PageSize="20" />
                        <Settings VerticalScrollableHeight="330" />
                        <SettingsCustomizationWindow Height="330px" />
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>