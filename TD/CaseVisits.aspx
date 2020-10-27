<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseVisits.aspx.cs" Inherits="CaseVisits" %>

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
                    Patient Appointment(s)
                </td>
            </tr>
        </table>

        <table style="position:absolute;padding-top:30px;width:100%">
            <tr>
                <td>
                    <dx:ASPxGridView 
                        ID="grdVisits" 
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
                            <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE_NAME" Caption="Type" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_PATIENT_NAME" Caption="Patient" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataDateColumn FieldName="DT_EVENT_DATE" Caption="Appointment" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy" >
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataColumn FieldName="SZ_VISIT_TYPE" Caption="IE/FU/C" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_DOCTOR_NAME" Caption="Treated By" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP" Caption="Specialty" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                            </dx:GridViewDataColumn>                    
                            <dx:GridViewDataColumn FieldName="BILL_STATUS" Caption="Bill Status" Visible="true">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="VISIT_TYPE" Visible="false"></dx:GridViewDataColumn>
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