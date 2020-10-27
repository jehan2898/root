<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Billing_Summary.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Billing_Summary"
    Title="Billing Summary" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
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



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script type="text/javascript">
 
   function selectall(obj)
    {
       var listbox = document.getElementById('ctl00_ContentPlaceHolder1_lstprovidernamename');
       var y=listbox.options.length;
        if(obj.checked)
       {
       
        for(var count=0; count < y; count++) 
        {
           listbox.options[count].selected = true;
        }
    
       }
    else
    {
       for(var count=0; count < y; count++) 
        {
           listbox.options[count].selected = false;
        }
    }  
  }
    
     function selectallfac(obj)
    {
   
       var listboxfirm = document.getElementById('lstLawFirm');
       var y=listboxfirm.options.length;
       if(obj.checked)
       {
       
        for(var count=0; count < y; count++) 
        {
           listboxfirm.options[count].selected = true;
        }
    
      
     }
    else
    {
       for(var count=0; count < y; count++) 
        {
           listboxfirm.options[count].selected = false;
        }
    }  
    }
    
    
    function unselectcheckboxforcom()
    {
     var x=document.getElementById('ctl00_ContentPlaceHolder1_chkCheck');
       if(x.checked)
       {
          x.checked=false;
         
       }
    }
        function unselectcheckboxforlaw(obj)
        {
            var x=document.getElementById(obj);
                if(x.checked)
                {
                     x.checked=false;
                }
        }
    
    
        function ShowVerificationPopup(szcaseid,billno,szcompanyid)
        {
            var url = "Bill_Sys_VerificationDetails.aspx?CaseID="+szcaseid+"&billno="+billno+"&CompanyID="+szcompanyid;
            VerificationPopup.SetContentUrl(url);
            VerificationPopup.Show();

        }

        function ShowDenialPopup(szcaseid,billno)
        {
            var url = "Bill_Sys_DenialDetails.aspx?CaseID="+szcaseid+"&billno="+billno;
            DenialPopup.SetContentUrl(url);
            DenialPopup.Show();

        }
            
    </script>

    <table style="width: 95%; background-color: White; margin-left: 23px; height: 690px;
        border: 1px;">
        <tr>
            <td style="vertical-align: top; width: 100%;">
                <div id="patientdetails" runat="server">
                    <table width="100%" border="0" id="tblcom" runat="server" style="padding-top: 20px">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblprovideraddress" runat="server" Text="Patient Information" Font-Bold="true"
                                    Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="grdPatientDetails" ClientInstanceName="grdPatientDetails" runat="server"
                                    Width="100%" KeyFieldName="SZ_CASE_ID">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="SZ_CASE_ID" Caption="SZ_CASE_ID" HeaderStyle-HorizontalAlign="Center"
                                            Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="SZ_PATIENT_NAME" Caption="Patient Name" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="DOA" Caption="Date Of Accident" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SZ_INSURANCE_NAME" Caption="Insurance Name"
                                            HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SZ_POLICY_NO" Caption="Policy No" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SZ_CLAIM_NUMBER" Caption="Claim No" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SZ_POLICY_HOLDER" Caption="Policy Holder" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <Styles>
                                        <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                        </FocusedRow>
                                        <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                        </AlternatingRow>
                                    </Styles>
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" id="Table3" runat="server" style="padding-top: 20px">
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label1" runat="server" Text="Attorney Information" Font-Bold="true"
                                    Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="grdAttorneyDetails" ClientInstanceName="grdAttorneyDetails"
                                    runat="server" Width="100%" KeyFieldName="SZ_ATTORNEY_ID">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="SZ_ATTORNEY_ID" Caption="SZ_ATTORNEY_ID " HeaderStyle-HorizontalAlign="Center"
                                            Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="NAME" Caption="Name" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ADDRESS" Caption="Address" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PHONE" Caption="Phone No" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="FAX" Caption="Fax No" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EMAIL" Caption="Email" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <Styles>
                                        <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                        </FocusedRow>
                                        <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                        </AlternatingRow>
                                    </Styles>
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" id="Table4" runat="server" style="padding-top: 20px">
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label2" runat="server" Text="Adjuster Information" Font-Bold="true"
                                    Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="grdadjuster" ClientInstanceName="grdPatientDetails" runat="server"
                                    Width="100%" KeyFieldName="SZ_ADJUSTER_ID">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="SZ_ADJUSTER_ID" Caption="SZ_ADJUSTER_ID" HeaderStyle-HorizontalAlign="Center"
                                            Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="NAME" Caption="Adjuster Name" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PHONE" Caption="Phone" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="FAX" Caption="Fax" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SZ_EXTENSION" Caption="Extension" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EMAIL" Caption="Email" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <Styles>
                                        <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                        </FocusedRow>
                                        <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                        </AlternatingRow>
                                    </Styles>
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="Div1" runat="server">
                    <table style="width: 100%; border: 0px solid;">
                        <tr>
                            <td style="border-bottom: 2px solid #BABABA;">
                                <span style="text-align: left; color: Black; font-family: Arial; font-size: 12px;
                                    font-weight: bold;">Search Parameters</span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divsearch" style="width: 100%;" runat="server">
                    <table>
                        <tr>
                            <td style="font-family: Arial; font-size: 13px; text-decoration: none; color: #026CA8;
                                border: 2px; width: 50%; font-weight: bold;" colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; border: 0px solid Gray;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: right; color: #626262; font-family: Arial; font-size: 14px;
                                padding-top: 20px; width: 15%" valign="top">
                                <dxe:ASPxLabel ID="lbcompanyname" runat="server" AssociatedControlID="tbcompanyname"
                                    Text="Provider">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="padding-top: 0px; margin-top: 0px; padding-left: 15px; padding-top: 20px;
                                width: 35%">
                                <asp:ListBox Width="90%" ID="lstprovidernamename" runat="server" SelectionMode="Multiple"
                                    Height="100px" OnClientClick="javascript:unselectcheckboxforcom('');"></asp:ListBox>
                            </td>
                            <td style="text-align: right; color: #626262; font-family: Arial; font-size: 14px;
                                padding-top: 20px; width: 15%" valign="top" visible="false">
                            </td>
                            <td style="padding-top: 0px; margin-top: 0px; padding-left: 15px; padding-top: 20px;
                                width: 35%" visible="false">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; border: 0px solid Gray;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="padding-left: 10%; width: 50%;">
                                <input id="chkCheck" runat="server" type="checkbox" name="Select All" onclick="javascript:selectall(this);" />&nbsp;
                                Select All
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; border: 0px solid Gray; padding-top: 20px;" cellpadding="0"
                        cellspacing="0">
                        <tr>
                            <td style="font-size: 11px; font-family: Arial; vertical-align: top;" align="center">
                                <dxe:ASPxButton ClientInstanceName="" ID="btnreportsearch" runat="server" Text="Run Report"
                                    OnClick="btnReportSearch_Click" Width="82px">
                                    <clientsideevents click="function(s, e) { LoadingPanel.Show(); }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="asd" runat="server">
                    <table width="100%" border="0" id="Table1" runat="server" style="padding-top: 20px">
                        <tr>
                            <td align="right">
                                <asp:LinkButton ID="LinkButton1" OnClick="btnXlsExport_Click" runat="server" Text="Export TO Excel">
                                 <img 
                                    src="Images/Excel.jpg" 
                                    alt="" 
                                    style="border:none;" 
                                    height="15px" 
                                    width ="15px" 
                                    title = "Export TO Excel"/>
                                </asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="grdGrandTotals" ClientInstanceName="grdGrandTotals" runat="server"
                                    Width="100%" OnPageIndexChanged="grdGrandTotals_PageIndexChanged" KeyFieldName="bill_amount">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="bill_amount" Caption="bill_amount" HeaderStyle-HorizontalAlign="Center"
                                            Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="bill_amount" Caption="Bill Amount($)" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                            <PropertiesTextEdit DisplayFormatString="c" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="paid_amount" Caption="Received($)" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                            <PropertiesTextEdit DisplayFormatString="c" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="flt_balance" Caption="Balance($)" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                            <PropertiesTextEdit DisplayFormatString="c" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="transferred_amount" Caption="Transferred Amount($)"
                                            HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right">
                                            <PropertiesTextEdit DisplayFormatString="c" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="writeoff_amount" Caption="WriteOff Amount($)"
                                            HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right">
                                            <PropertiesTextEdit DisplayFormatString="c" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <Styles>
                                        <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                        </FocusedRow>
                                        <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                        </AlternatingRow>
                                    </Styles>
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <%--<dx:ASPxButton ID="btnXlsExport1" runat="server" Text="" UseSubmitBehavior="False" Visible="false" Height="20px" Width="20px"
                                     OnClick="btnXlsExport1_Click" Image-Url ="Images/Excel.jpg"/>--%>
                                <asp:LinkButton ID="btnXlsExport1" OnClick="btnXlsExport1_Click" runat="server" Text="Export TO Excel"
                                    Visible="true">
                                      <img 
                                           src="Images/Excel.jpg" 
                                           alt="" 
                                           style="border:none;" 
                                           height="15px" 
                                           width ="15px" 
                                           title = "Export TO Excel"/>
                                </asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="gridprovider" ClientInstanceName="gridprovider" runat="server"
                                    Width="100%" SettingsPager-PageSize="20" KeyFieldName="SZ_OFFICE_ID">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="SZ_OFFICE_ID" Caption="Office Id" HeaderStyle-HorizontalAlign="Center"
                                            Visible="false">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SZ_OFFICE" Caption="Office Name" HeaderStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="BILL_AMOUNT" Caption="Bill Amount($)" HeaderStyle-HorizontalAlign="Center">
                                            <PropertiesTextEdit DisplayFormatString="c" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PAID_AMOUNT" Caption="Received($)" HeaderStyle-HorizontalAlign="Center">
                                            <PropertiesTextEdit DisplayFormatString="c" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="FLT_BALANCE" Caption="Balance($)" HeaderStyle-HorizontalAlign="Center">
                                            <PropertiesTextEdit DisplayFormatString="c" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TRANSFERRED_AMOUNT" Caption="Transferred Amount($)"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <PropertiesTextEdit DisplayFormatString="c" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="writeoff_amount" Caption="WriteOff Amount($)"
                                            HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right">
                                            <PropertiesTextEdit DisplayFormatString="c" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Templates>
                                        <DetailRow>
                                            <table width="100%" id="tbl">
                                                <tr>
                                                    <td align="right">
                                                        <dx:ASPxButton ID="btnXlsExport2" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                                            Visible="false" OnClick="btnXlsExport2_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <dx:ASPxGridView ID="gridproviderwise" runat="server" Width="100%" ClientInstanceName="gridproviderwise"
                                                OnBeforePerformDataSelect="detailGrid_DataSelect" DataSourceID="sdsProviderMaster"
                                                KeyFieldName="SZ_OFFICE_ID">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE_ID" Caption="Office ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false" />
                                                    <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="SZ_CASE_ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false" />
                                                    <dx:GridViewDataColumn FieldName="SZ_COMPANY_ID" Caption="SZ_COMPANY_ID" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false" />
                                                    <dx:GridViewDataColumn FieldName="SZ_OFFICE" Caption="Provider" HeaderStyle-HorizontalAlign="Left" />
                                                    <dx:GridViewDataColumn FieldName="BILL#" Caption="Bill No" HeaderStyle-HorizontalAlign="Center" />
                                                    <dx:GridViewDataColumn FieldName="BILL_DATE" Caption="Bill Date" HeaderStyle-HorizontalAlign="Center"
                                                        CellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SERVICE_DATE" Caption="Service Date" HeaderStyle-HorizontalAlign="Center"
                                                        CellStyle-HorizontalAlign="Right">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataTextColumn FieldName="BILL_AMOUNT" Caption="Bill Amount($)" HeaderStyle-HorizontalAlign="Center">
                                                        <PropertiesTextEdit DisplayFormatString="c" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="PAID_AMOUNT" Caption="Received($)" HeaderStyle-HorizontalAlign="Center">
                                                        <PropertiesTextEdit DisplayFormatString="c" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="FLT_BALANCE" Caption="Balance($)" HeaderStyle-HorizontalAlign="Center">
                                                        <PropertiesTextEdit DisplayFormatString="c" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="TRANSFERRED_AMOUNT" Caption="Transferred Amount($)"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <PropertiesTextEdit DisplayFormatString="c" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="writeoff_amount" Caption="WriteOff Amount($)"
                                                        HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right">
                                                        <PropertiesTextEdit DisplayFormatString="c" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataHyperLinkColumn Caption="Verification" Visible="true">
                                                        <DataItemTemplate>
                                                            <a href="javascript:void(0);" onclick="ShowVerificationPopup('<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.BILL#")%>','<%# DataBinder.Eval(Container,"DataItem.SZ_COMPANY_ID")%>')">
                                                                View</a>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" />
                                                    </dx:GridViewDataHyperLinkColumn>
                                                    <dx:GridViewDataHyperLinkColumn Caption="Denail" Visible="true">
                                                        <DataItemTemplate>
                                                            <a href="javascript:void(0);" onclick="ShowDenialPopup('<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>','<%# DataBinder.Eval(Container,"DataItem.BILL#")%>')">
                                                                View</a>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" />
                                                    </dx:GridViewDataHyperLinkColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <Styles>
                                                    <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                                    </FocusedRow>
                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                    </AlternatingRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </DetailRow>
                                    </Templates>
                                    <SettingsDetail ShowDetailRow="true" ExportMode="Expanded" />
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <Styles>
                                        <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                        </FocusedRow>
                                        <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                        </AlternatingRow>
                                    </Styles>
                                </dx:ASPxGridView>
                                <table width="100%" id="tbl">
                                    <tr>
                                        <td align="right">
                                            <dx:ASPxButton ID="btnXlsExport2" runat="server" Text="Export to XLS" OnClick="btnXlsExport2_Click"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                                <dx:ASPxGridViewExporter ID="grdExportThirtyFour" runat="server" GridViewID="gridprovider">
                                </dx:ASPxGridViewExporter>
                                <dx:ASPxGridViewExporter ID="grdExport2" runat="server" GridViewID="gridproviderwise">
                                </dx:ASPxGridViewExporter>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxTextBox ID="txtbtreff" runat="server" Visible="false">
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" id="Table2" runat="server" style="padding-top: 20px">
                        <%-- <tr>
                            <td align="right">
                                <asp:LinkButton ID="lnknotes" OnClick="btnXlsExport_Click" runat="server" Text="Export TO Excel"
                                    Visible="false">
                                 <img 
                                    src="Images/Excel.jpg" 
                                    alt="" 
                                    style="border:none;" 
                                    height="15px" 
                                    width ="15px" 
                                    title = "Export TO Excel"/>
                                </asp:LinkButton>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="grdccasenotes" ClientInstanceName="grdccasenotes" runat="server"
                                    Width="100%" OnPageIndexChanged="grdccasenotes_PageIndexChanged" KeyFieldName="Bill#">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Bill#" Caption="BILL NO " HeaderStyle-HorizontalAlign="Center"
                                            Visible="false" />
                                        <dx:GridViewDataTextColumn FieldName="Notes" Caption="Notes" HeaderStyle-HorizontalAlign="Center"
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <Styles>
                                        <FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                        </FocusedRow>
                                        <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                        </AlternatingRow>
                                    </Styles>
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                    </table>
                    <asp:SqlDataSource ID="sdsCompanyMaster" runat="server" ConnectionString="<%$ appSettings:Connection_String %>">
                        <SelectParameters>
                            <asp:SessionParameter Name="SZ_OFFICE_ID" SessionField="SZ_OFFICE_ID" Type="String" />
                            <asp:SessionParameter Name="SZ_COMPANY_ID" SessionField="SZ_COMPANY_ID" Type="String" />
                            <asp:SessionParameter Name="SZ_CASE_ID" SessionField="SZ_CASE_ID" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="sdsProviderMaster" runat="server" ConnectionString="<%$ appSettings:Connection_String %>">
                        <SelectParameters>
                            <asp:SessionParameter Name="SZ_OFFICE_ID" SessionField="SZ_OFFICE_ID" Type="String" />
                            <asp:SessionParameter Name="SZ_COMPANY_ID" SessionField="SZ_COMPANY_ID" Type="String" />
                            <asp:SessionParameter Name="SZ_CASE_ID" SessionField="SZ_CASE_ID" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                        Modal="True">
                    </dx:ASPxLoadingPanel>
                    <dx:ASPxGridViewExporter ID="exporter" runat="server" GridViewID="grdGrandTotals" />
                </div>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtcaseid" runat="server" Visible="false"></asp:TextBox>
    <dx:ASPxPopupControl ID="VerificationPopup" runat="server" CloseAction="CloseButton"
        HeaderStyle-BackColor="#B5DF82" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ClientInstanceName="VerificationPopup" HeaderText="Verification" HeaderStyle-HorizontalAlign="Left"
        HeaderStyle-Font-Bold="true" AllowDragging="True" EnableAnimation="False" EnableViewState="True"
        Width="500px" ToolTip="Verification" PopupHorizontalOffset="0" PopupVerticalOffset="0"
          AutoUpdatePosition="true" RenderIFrameForPopupElements="Default"
        ScrollBars="None" Height="370px" EnableHierarchyRecreation="True">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="DenialPopup" runat="server" CloseAction="CloseButton" HeaderStyle-BackColor="#B5DF82"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="DenialPopup"
        HeaderText="Denial" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true"
        AllowDragging="True" EnableAnimation="False" EnableViewState="True" Width="500px"
        ToolTip="Denial" PopupHorizontalOffset="0" PopupVerticalOffset="0"  
        AutoUpdatePosition="true" RenderIFrameForPopupElements="Default" ScrollBars="None"
        Height="370px" EnableHierarchyRecreation="True">
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
</asp:Content>
