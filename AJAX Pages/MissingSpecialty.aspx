<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MissingSpecialty.aspx.cs" Inherits="AJAX_Pages_MissingSpecialty" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" src="~/validation.js"></script>
    <script language="javascript" type="text/javascript">
        function Export() {
            expLoadingPanel.Show();
            Callback1.PerformCallback();
        }
        function OnCallbackComplete(s, e) {
            expLoadingPanel.Hide();
            var url = "../RP/DownloadFiles.aspx";
            IFrame_DownloadFiles.SetContentUrl(url);
            IFrame_DownloadFiles.Show();
            return false;
        }
        
         function SelectAllSpeciality(ival) {
            //alert(ival);
            var f = document.getElementById('<%=grdSpeciality.ClientID%>');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }
    </script>



   
<asp:ScriptManager id="ScriptManager1" runat="server">
</asp:ScriptManager>
<div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">
<table border="0" style="margin-left:10px;margin-bottom:5px; margin-top:5px;">
    <tr>
    <td>
     <table style="border: 1px solid #d3d3d3;" width="500px" border="0">
        <tr>
            <td height="28" align="left" valign="middle" bgcolor="#d3d3d3" colspan="3">
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                <b>Search Parameters</b>
            </td>
        </tr>
        <tr>
            <td class="td-widget-bc-search-desc-ch1" align="left" style="width:20%">
                Missing Specialty
            </td>
            </tr>
         <tr>
           <td class="td-widget-bc-search-desc-ch3" style="width:50%" >
                  <div style="height: 160px; background-color: Gray; overflow: scroll;">
                <dx:ASPxGridView ID="grdSpeciality" runat="server" Width="100%" SettingsBehavior-AllowSort="false"
                                SettingsPager-PageSize="20" ClientInstanceName="grdSpeciality" KeyFieldName="CODE">
                    <Columns>
                        <dx:GridViewDataColumn Caption="chk1" VisibleIndex="0" Width="30px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAllSpeciality" runat="server" onclick="javascript:SelectAllSpeciality(this.checked);"
                                ToolTip="Select All" />
                            </HeaderTemplate>
                            <DataItemTemplate>
                            <asp:CheckBox ID="chkall1" Visible="true" runat="server" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="description" Caption="Specialty Name" VisibleIndex="1">
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="code" Caption="Speciality Id" VisibleIndex="2" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewDataColumn>
                    </Columns>
                    <SettingsPager PageSize="1000">
                    </SettingsPager>
                </dx:ASPxGridView>
            </div>
            </td>   
            
         </tr>
     
        <tr>
            <td align="center" height="28" colspan="3">
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px"  OnClick="btnSearch_Click" />
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
     </table>
     </td>
    </tr>
</table>

<asp:UpdatePanel ID="UpdatePanel5" runat="server">
<ContentTemplate>
<table style="vertical-align: middle; width: 100%;">
    <tr>
        <td>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                <ProgressTemplate>
                    <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                        runat="Server">
                        <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                            Height="25px" Width="24px"></asp:Image>
                        Loading...</div>

                </ProgressTemplate>
            </asp:UpdateProgress>
        </td>
    </tr>
</table>
<table style="width:99%" >
    <tr>
       
        <td style="width: 100%; text-align: right;">
           <asp:Label ID="lbl" runat="server" Text="count:" Font-Bold="True" Font-Size="Small" ></asp:Label>
            <asp:Label ID="lblcount" runat="server"  Text="count" Font-Size="Small"></asp:Label>
            
            <dx:ASPxHyperLink Text="[Excel]" runat="server" ID="xExcel">
                <ClientSideEvents Click="Export" />
            </dx:ASPxHyperLink>
            <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="Callback1"
                OnCallback="ASPxCallback1_Callback">
                <ClientSideEvents CallbackComplete="OnCallbackComplete" />
            </dx:ASPxCallback>
            <dx:ASPxLoadingPanel Text="Exporting..." runat="server" ID="expLoadingPanel" ClientInstanceName="expLoadingPanel">
            </dx:ASPxLoadingPanel>
        </td>
    </tr>
    <tr>
        <td style="width: 90%">
             
               <dx:ASPxGridView ID="grdMissingSpeciality" runat="server"
                                Border-BorderColor="#aaaaaa" SettingsPager-PageSize="20" Width="100%" AutoGenerateColumns="False"
                                Settings-ShowHorizontalScrollBar="true" Settings-ShowGroupedColumns="true" Settings-VerticalScrollableHeight="330"
                                Settings-ShowGroupPanel="true"
                                SettingsBehavior-AllowGroup="true" Settings-ShowFooter="true" SettingsBehavior-AutoExpandAllGroups="true">
                                <Columns>
                                    <dx:GridViewDataColumn Caption="Specialty" VisibleIndex="1" FieldName="Speicalty" Width="70px"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Patient" VisibleIndex="2" FieldName="Patient" Width="200px"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Case #" VisibleIndex="3" FieldName="sz_case_no" Width="50px"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn Caption="Case Type" VisibleIndex="4" FieldName="SZ_CASE_TYPE_NAME" Settings-AllowSort="True"
                                        HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Accident Date" VisibleIndex="5" Width="100px" FieldName="DT_DATE_OF_ACCIDENT"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Date Opened" VisibleIndex="6" FieldName="dt_created_date"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataColumn Caption="Insurance" VisibleIndex="7" FieldName="sz_insurance_name" Width="200px"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="ClaimNo" VisibleIndex="8" FieldName="sz_claim_number" Width="140px"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn Caption="CellNo" VisibleIndex="9" Width="120px" FieldName="sz_patient_cellno"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                      <dx:GridViewDataTextColumn Caption="PhoneNo" VisibleIndex="10" Width="120px" FieldName="SZ_PATIENT_PHONE"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Days Open" VisibleIndex="11" Width="80px" FieldName="DaysOpen"
                                        Settings-AllowSort="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <Settings AllowSort="True" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AutoExpandAllGroups="True" />
                                <SettingsPager PageSize="20">
                                </SettingsPager>
                                <Settings ShowGroupPanel="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded"
                                    ShowGroupButtons="false" />
                            </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="grdExport" runat="server" GridViewID="grdMissingSpeciality">
                </dx:ASPxGridViewExporter>
            </td>
    </tr>
</table>
    </ContentTemplate>
</asp:UpdatePanel>

   <%-- <ajaxToolkit:CalendarExtender ID="calExtFromBillDate" runat="server" TargetControlID="txtFromBillDate"
        PopupButtonID="imgbtnFromBillDate" />
    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtFromBillDate" PromptCharacter="_" AutoComplete="true">
    </ajaxToolkit:MaskedEditExtender>
    <ajaxToolkit:CalendarExtender ID="calExtToBillDate" runat="server" TargetControlID="txtToBillDate"
        PopupButtonID="imgbtnToBillDate" />
    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtToBillDate" PromptCharacter="_" AutoComplete="true">
    </ajaxToolkit:MaskedEditExtender>

    <ajaxToolkit:CalendarExtender ID="calExtFromPaymentDate" runat="server" TargetControlID="txtFromPaymentDate"
        PopupButtonID="imgbtnFromPaymentDate" />
    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtFromPaymentDate" PromptCharacter="_" AutoComplete="true">
    </ajaxToolkit:MaskedEditExtender>
    <ajaxToolkit:CalendarExtender ID="calExtToPaymentDate" runat="server" TargetControlID="txtToPaymentDate"
        PopupButtonID="imgbtnToPaymentDate" />
    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtToPaymentDate" PromptCharacter="_" AutoComplete="true">
    </ajaxToolkit:MaskedEditExtender>

    <ajaxToolkit:CalendarExtender ID="calExtFromVisitDate" runat="server" TargetControlID="txtFromVisitDate"
        PopupButtonID="imgbtnFromVisitDate" />
    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtFromVisitDate" PromptCharacter="_" AutoComplete="true">
    </ajaxToolkit:MaskedEditExtender>
    <ajaxToolkit:CalendarExtender ID="calExtToVisitDate" runat="server" TargetControlID="txtToVisitDate"
        PopupButtonID="imgbtnToVisitDate" />
    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtToVisitDate" PromptCharacter="_" AutoComplete="true">
    </ajaxToolkit:MaskedEditExtender>--%>
</div>
<%--<dx:ASPxPopupControl ID="IFrame_DownloadFiles" runat="server" CloseAction="CloseButton"
    Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ClientInstanceName="IFrame_DownloadFiles" HeaderText="Data Export" HeaderStyle-HorizontalAlign="Left"
    HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#000000" AllowDragging="True"
    EnableAnimation="False" EnableViewState="True" Width="600px" ToolTip="Download File(s)"
    PopupHorizontalOffset="0" PopupVerticalOffset="0" RenderMode="Classic" AutoUpdatePosition="true"
    ScrollBars="Auto" RenderIFrameForPopupElements="Default" Height="200px">
    <ContentStyle>
        <Paddings PaddingBottom="5px" />
    </ContentStyle>
</dx:ASPxPopupControl>--%>
</asp:Content>



