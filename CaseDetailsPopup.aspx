<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseDetailsPopup.aspx.cs" Inherits="CaseDetailsPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" id="framehead">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td>
                <asp:DataGrid ID="grdDateOfAccList" Width="100%" CssClass="mGrid" GridLines="None" runat="Server" AutoGenerateColumns="False">
                    <HeaderStyle CssClass="GridViewHeader" BackColor="#B5DF82" Font-Bold="true"/>
                    <PagerStyle CssClass="pgr"/>
                        <AlternatingItemStyle BackColor="#EEEEEE"/>
                    <ItemStyle CssClass="GridRow" />
                    <Columns>
                        <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PATIENT_NAME" HeaderText="Patient Name" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SZ_CLAIM_NUMBER" HeaderText="Claim Number" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SZ_PLATE_NO" HeaderText="Plate Number" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SZ_REPORT_NO" HeaderText="Report Number" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SZ_ADJUSTER_NAME" HeaderText="Adjuster Name" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
      
    </table>
    
    </div>
    </form>
</body>
</html>
