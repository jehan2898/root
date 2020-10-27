<%@ Page Language="C#" AutoEventWireup="true" 
    CodeFile="Bill_Sys_PatientAllData.aspx.cs" Inherits="Bill_Sys_PatientAllData" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>

    <script type="text/javascript" src="validation.js"></script>
    <html>
    
    <head id="Head1" runat="server">
    <title></title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
     <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop">
                        </td>
                        <td class="CenterTop">
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <div style="height: 480px; overflow:auto;width:970px;">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                 <tr>
                                    <td style="width: 100%" class="TDPart" valign="top">
                                        <asp:DataGrid ID="grdPatientAllData" AutoGenerateColumns="false" runat="server" Width="100%"
                                            CssClass="GridTable" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                               <asp:BoundColumn DataField="SZ_CLUSTER_ID" HeaderText="Cluster ID" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="SZ_VENUE" HeaderText="Venue" ></asp:BoundColumn>
                                               <asp:BoundColumn DataField="SZ_DOCKET_NO" HeaderText="Docket #" ></asp:BoundColumn>
                                               <asp:BoundColumn DataField="SZ_LAST_STATUS" HeaderText="Last Status" ></asp:BoundColumn>
                                               <asp:BoundColumn DataField="SZ_PROVIDER" HeaderText="Provider" ></asp:BoundColumn>
                                               <asp:BoundColumn DataField="SZ_INSURER" HeaderText="Insurer" ></asp:BoundColumn>
                                               <asp:BoundColumn DataField="SZ_DEF_ATTORNEY" HeaderText="Defendant / Attorney" ></asp:BoundColumn>
                                               <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" ></asp:BoundColumn>
                                               <asp:BoundColumn DataField="SZ_CLAIM_NO" HeaderText="Claim #" ></asp:BoundColumn>
                                               <asp:BoundColumn DataField="SZ_DATE_OF_ACCIDENT" HeaderText="Accident Date" ></asp:BoundColumn>
                                               <asp:BoundColumn DataField="SZ_DATE_OF_START" HeaderText="Start Date" ></asp:BoundColumn>
                                               <asp:BoundColumn DataField="SZ_DATE_OF_END" HeaderText="End Date" ></asp:BoundColumn>
                                               
                                               <asp:BoundColumn DataField="SZ_CLAIM_AMT" HeaderText="Claim Amount" ></asp:BoundColumn>
                                               <asp:BoundColumn DataField="SZ_BALANCE_AMT" HeaderText="Balance Amount" ></asp:BoundColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                            </div>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
     
   

