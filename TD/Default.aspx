<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function showVisits(objCaseID, objCompanyID) {
            var url = "CaseVisits.aspx?CaseID=" + objCaseID + "&cmpId=" + objCompanyID + "";
            IFrame_CaseVisits.SetContentUrl(url);
            IFrame_CaseVisits.Show();
            return false;
        }

        function showBills(objCaseID, objCompanyID) {
            var url = "CaseBills.aspx?CaseID=" + objCaseID + "&cmpId=" + objCompanyID + "";
            IFrame_CaseBills.SetContentUrl(url);
            IFrame_CaseBills.Show();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HyperLink runat="server" ID="hthht" NavigateUrl="PatientDesk.aspx"> Open </asp:HyperLink>

        <a href="javascript:showVisits('8291','CO00023');">Show Visits</a>
        <dx:ASPxPopupControl 
            ID="IFrame_CaseVisits" runat="server" CloseAction="CloseButton"
            Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ClientInstanceName="IFrame_CaseVisits" 
            HeaderText="View Appointments"
            HeaderStyle-HorizontalAlign="Left"
            HeaderStyle-ForeColor="White"
            HeaderStyle-BackColor="#000000" 
            AllowDragging="True" 
            EnableAnimation="False"
            EnableViewState="True" Width="800px" ToolTip="View Appointments" PopupHorizontalOffset="0"
            PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
            RenderIFrameForPopupElements="Default" Height="500px">
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <a href="javascript:showBills('8291','CO00023');">Show Bills</a>
        <dx:ASPxPopupControl 
            ID="IFrame_CaseBills" runat="server" CloseAction="CloseButton"
            Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ClientInstanceName="IFrame_CaseBills" 
            HeaderText="View Bills"
            HeaderStyle-HorizontalAlign="Left"
            HeaderStyle-ForeColor="White"
            HeaderStyle-BackColor="#000000" 
            AllowDragging="True" 
            EnableAnimation="False"
            EnableViewState="True" Width="800px" ToolTip="View Bills" PopupHorizontalOffset="0"
            PopupVerticalOffset="0"   AutoUpdatePosition="true" ScrollBars="Auto"
            RenderIFrameForPopupElements="Default" Height="500px">
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>

        <a href="PatientDesk.aspx">Patient's Desk</a>

    </div>
    </form>
</body>
</html>
