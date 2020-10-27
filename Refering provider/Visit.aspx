<%@ Page Title="" Language="C#" MasterPageFile="~/Refering provider/ProviderMasterPage.master" AutoEventWireup="true" CodeFile="Visit.aspx.cs" Inherits="Refering_provider_Default2" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <table>
   <tr>
      <td align="left" valign="middle" colspan="3" style="background-color: #CDCAB9; font-family: Calibri;
                  font-size: 20px; font-weight: normal; font-style: italic;">
         Visit
      </td>
    </tr>
   </table>
   
    <table id="manage-reg-filters" width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100%; vertical-align: top;">
                    <table style="width: 100%;">
                        <tr>
                            <td class="manage-member-lable-td">
                                <label>
                              Doctor Name </label>
                            </td>

                              <td class="manage-member-lable-td">
                                <label>
                                   Speciality</label>
                            </td>
                            
                            <td class="manage-member-lable-td">
                                <label>
                                    Case Type</label>
                            </td>
                           
                          
                            
                           
                        </tr>
                        <tr>
                        <td class="registration-form-ibox">
                                <dxe:aspxtextbox CssClass="inputBox" runat="server" ID="Billno" Width="195px"  
                                    Height="30px">
                                </dxe:aspxtextbox>
                            </td>

                             <td class="registration-form-ibox">
                              <dxe:aspxcombobox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                                                                                        ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                                                                                        ID="Aspxcombobox1">
                                                                                                        <Items>
                                                                                                            <dxe:ListEditItem Text="--Select--" Value="2" Selected="True"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Bill" Value="1"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Unbill" Value="0"></dxe:ListEditItem>
                                                                                                        </Items>
                                                                                                        <ItemStyle>
                                                                                                            <HoverStyle BackColor="#F6F6F6">
                                                                                                            </HoverStyle>
                                                                                                        </ItemStyle>
                                                                                                    </dxe:aspxcombobox>
                            </td>
                         <td class="form-ibox">
                                                                                                   <dxe:aspxcombobox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                                                                                        ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                                                                                        ID="ddlBillstatus">
                                                                                                        <Items>
                                                                                                            <dxe:ListEditItem Text="--Select--" Value="2" Selected="True"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Bill" Value="1"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Unbill" Value="0"></dxe:ListEditItem>
                                                                                                        </Items>
                                                                                                        <ItemStyle>
                                                                                                            <HoverStyle BackColor="#F6F6F6">
                                                                                                            </HoverStyle>
                                                                                                        </ItemStyle>
                                                                                                    </dxe:aspxcombobox>
                                                                                                </td>
                                                                                                
                                                                                                
                                                                                </tr>

                                                                                  <tr>
                                                                                
                                                                                <td class="manage-member-lable-td">
                                <label>
                                    Billing Date</label>
                            </td>
                            <td class="manage-member-lable-td">
                                <label>
                                   From </label>
                            </td>

                             <td class="manage-member-lable-td">
                                <label>
                                   To</label>
                            </td>
                            </tr>

                                                                                <tr>
                                                                                     
                                                                                                <td class="registration-form-ibox">
                                                                                                    <dxe:ASPxComboBox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                                                                                        ValueType="System.String" ClientInstanceName="cIssueDate" CssClass="inputBox"
                                                                                                        ID="ddlDateValues">
                                                                                                        <Items>
                                                                                                            <dxe:ListEditItem Text="--Select--" Value="NA" Selected="True"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="All" Value="0"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Today" Value="1"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="This Week" Value="2"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="This Month" Value="3"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="This Quarter" Value="4"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="This Year" Value="5"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Last Week" Value="6"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Last Month" Value="7"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Last Quarter" Value="9"></dxe:ListEditItem>
                                                                                                            <dxe:ListEditItem Text="Last Year" Value="8"></dxe:ListEditItem>
                                                                                                        </Items>
                                                                                                        <ItemStyle>
                                                                                                            <HoverStyle BackColor="#F6F6F6">
                                                                                                            </HoverStyle>
                                                                                                        </ItemStyle>
                                                                                                        <ClientSideEvents SelectedIndexChanged="OnIndexChnage" />
                                                                                                    </dxe:ASPxComboBox>
                                                                                                </td>
                                                                                                <td class="registration-form-ibox">
                                                                                                  <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdtfromdate" CssClass="inputBox"
                                                                                                        ID="dtfromdate">
                                                                                                    </dxe:ASPxDateEdit>
                                                                                                </td>
                                                                                                 <td class="registration-form-ibox">
                                                                                                  <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdtfromdate" CssClass="inputBox"
                                                                                                        ID="ASPxDateEdit2">
                                                                                                    </dxe:ASPxDateEdit>
                                                                                                </td>
                                                                                
                                                                                </tr>

                                                                                </table>


                                                         <table id="Table1" style="width: 100%;">
            <tr>
                <td style="text-align: right;">
                    <dx:ASPxButton runat="server" Text="Search" ID="btnSave">
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton runat="server" Text="Reset" ID="btnReset">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>                            
                   <table style="width: 100%">
                                 
                                    <tr>
                                        <td style="width: 100%">
                                            <div style="height: 400px; width: 100%; background-color: gray; overflow: scroll;">
                                                <dx:ASPxGridView ID="grdVisits" runat="server" KeyFieldName="SZ_CASE_ID" AutoGenerateColumns="false"
                                                    Width="100%" SettingsPager-PageSize="20" SettingsCustomizationWindow-Height="330"
                                                    Settings-VerticalScrollableHeight="330">
                                                    <columns>
                                                    
                                                    <%--1--%>
                                                    <dx:GridViewDataColumn FieldName="Case#" Caption="Case #" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    </dx:GridViewDataColumn>
                                                    <%--2--%>
                                                      <dx:GridViewDataColumn FieldName="Patient_name" Caption="Patient Name" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    
                                                    </dx:GridViewDataColumn>
                                                    <%--3--%>
                                                     <dx:GridViewDataColumn FieldName="Date_Of_Visit" Caption="Date Of Visit " 
                                                      HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    </dx:GridViewDataColumn>
                                                    <%--4--%>
                                                    <dx:GridViewDataColumn HeaderStyle-HorizontalAlign="Center"
                                                     FieldName="Visit_type" Caption="Visit Type"   Visible="true" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                      <%--5--%>
                                                    <dx:GridViewDataColumn FieldName="Doctor_name" Caption="Doctor Name" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    </dx:GridViewDataColumn>
                                                    <%--6--%>
                                                    <dx:GridViewDataColumn FieldName="Casetype" Caption="Case Type" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                       <%--7--%>
                                                    <dx:GridViewDataColumn FieldName="Speciality" Caption="Speciality" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                       <%--8--%>
                                                                                                         
                                                    <dx:GridViewDataColumn FieldName="Numberofdays" Caption="Number Of Days" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                     <%--10--%>
                                                    <dx:GridViewDataColumn FieldName="ClaimNumber" Caption="Claim Number" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                     <%--11--%>
                                                    <dx:GridViewDataColumn FieldName="DiagnosisCode" Caption="Diagnosis Code" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                      <%--12--%>
                                                    
                                                    <dx:GridViewDataColumn FieldName="Groupservices" Caption="Group Services" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                      <%--13--%>
                                                     <dx:GridViewDataColumn Caption="Open File" Settings-AllowSort="False" Width="25px">
                                                    
                                                        <HeaderTemplate >
                                                         Upload Document
                                                        </HeaderTemplate>
                                                        <DataItemTemplate>
                                                            <asp:linkbutton id="lnkAddVisit" runat="server" text='Upload Document' commandname="" onclick='<%# "showPopup2(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ", " + "\""+ Eval("SZ_PATIENT_NAME") +"\""+ ", " + "\""+ Eval("I_EVENT_ID") +"\""+ ", " + "\""+ Eval("SZ_PROCEDURE_GROUP_ID") +"\""+ ", "  + "\""+ Eval("SZ_VISIT_TYPE_ID") +"\""+ ", "  + "\""+ Eval("VISIT_TYPE") +"\""+ "," + "\""+ Eval("SZ_CASE_NO") +"\""+ "," + "\""+ Eval("SZ_DOCTOR_ID") +"\"); return false;" %> '>
                                                            </asp:linkbutton>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                    </dx:GridViewDataColumn>
                                                </columns>
                                                    <settingsbehavior allowfocusedrow="True" allowsort="False" />
                                                    <settingspager pagesize="20"></settingspager>
                                                    <settings verticalscrollableheight="330"></settings>
                                                    <settingscustomizationwindow height="330px"></settingscustomizationwindow>
                                                    <styles>
                                                    <FocusedRow CssClass="dxgvFocusedGroupRow">
                                                    </FocusedRow>
                                                    <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                                    </AlternatingRow>
                                                    <SelectedRow CssClass="dxgvFocusedGroupRow">
                                                    </SelectedRow>
                                                </styles>
                                                </dx:ASPxGridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            
                  
                  
                  
                  
                                                                </td>
                                                        </tr>
                                            </table>
</asp:Content>

