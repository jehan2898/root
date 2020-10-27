<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_BillTransactionEditAll.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_BillTransactionEditAll" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGridView ID="grdVisits" ClientInstanceName="grdVisits" runat="server" Width="100%"
            SettingsPager-PageSize="20" KeyFieldName="CaseID" AutoGenerateColumns="False"
            SettingsPager-Mode="ShowPager">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="CaseID" Caption="CaseID" HeaderStyle-HorizontalAlign="Center" Visible="false">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PatientID" Caption="PatientID" HeaderStyle-HorizontalAlign="Center" Visible="false">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DoctorName" Caption="Doctor Name" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="VisitType" Caption="Visit Type" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="VisitDate" Caption="Visit Date" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="SpecialityID" Caption="SpecialityID" HeaderStyle-HorizontalAlign="Center" Visible="false">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="I_EventID" Caption="I_EventID" HeaderStyle-HorizontalAlign="Center" Visible="false">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="DoctorID" Caption="DoctorID" HeaderStyle-HorizontalAlign="Center" Visible="false">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="ISREFFERAL" Caption="ISREFFERAL" HeaderStyle-HorizontalAlign="Center" Visible="false">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IS_ADDED_BY_DOCTOR" Caption="IS_ADDED_BY_DOCTOR" HeaderStyle-HorizontalAlign="Center" Visible="false">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="BT_FINALIZE" Caption="BT_FINALIZE" HeaderStyle-HorizontalAlign="Center" Visible="false">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="SZ_USER_NAME" Caption="SZ_USER_NAME" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="SZ_INSURANCE_NAME" Caption="SZ_INSURANCE_NAME" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataTextColumn>
            </Columns>
             
            <SettingsBehavior AllowFocusedRow="True" />
            <Styles>
                <FocusedRow BackColor="">
                </FocusedRow>
                <AlternatingRow Enabled="True">
                </AlternatingRow>
                <Header BackColor="#B5DF82">
                </Header>
            </Styles>
       </dx:ASPxGridView>
        <asp:TextBox ID="txtCompanyID" runat="server" Visible="false" />
    </div>
    </form>
</body>
</html>
