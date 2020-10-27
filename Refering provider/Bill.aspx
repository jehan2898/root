<%@ Page Title="" Language="C#" MasterPageFile="~/Refering provider/ProviderMasterPage.master" AutoEventWireup="true" CodeFile="Bill.aspx.cs" Inherits="Refering_provider_Bill" %>

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
         Bill
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
                                  Bill Number  </label>
                            </td>
                            <td class="manage-member-lable-td">
                                <label>
                                    Case Number</label>
                            </td>
                            <td class="manage-member-lable-td">
                                <label>
                                    Bill Status</label>
                            </td>
                            <td class="manage-member-lable-td">
                                <label>
                                   Speciality</label>
                            </td>
                            
                            
                           
                        </tr>
                        <tr>
                        <td class="registration-form-ibox">
                                <dxe:aspxtextbox CssClass="inputBox" runat="server" ID="Billno" Width="195px"  
                                    Height="30px">
                                </dxe:aspxtextbox>
                            </td>

                             <td class="registration-form-ibox">
                                <dxe:aspxtextbox CssClass="inputBox" runat="server" ID="Caseno" Width="195px"  
                                    Height="30px">
                                </dxe:aspxtextbox>
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
                                                                                                <td class="form-ibox">
                                                                                                   <dxe:aspxcombobox runat="server" EnableSynchronization="False" SelectedIndex="0"
                                                                                                        ValueType="System.String" ClientInstanceName="cIssueType" CssClass="inputBox"
                                                                                                        ID="ddlspeciality">
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
                            <td class="manage-member-lable-td">
                                <label>
                                    Case Type</label>
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
                                                                                                <td class="form-ibox">
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
                                                                                    </tr>
                                                                                      
                                                                                      <tr>
                                                                                        <td class="manage-member-lable-td">
                                <label>
                                   Patient Name</label>
                            </td>

                                                                                                                  
                                                                                      </tr>
                                                                                               <tr>
                                                                                                
                                                                                                <td class="registration-form-ibox">
                                                                                                    <dxe:aspxtextbox runat="server" Width="195px" Height="30px" Enabled="False" CssClass="inputBox"
                                                                                                        ID="txtchartno">
                                                                                                    </dxe:aspxtextbox>
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
                                                    <%--0--%>
                                                    <dx:GridViewDataColumn FieldName="Bill#" Caption="Bill #" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    </dx:GridViewDataColumn>
                                                    <%--1--%>
                                                    <dx:GridViewDataColumn FieldName="Case#" Caption="Case #" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    </dx:GridViewDataColumn>
                                                    <%--2--%>
                                                      <dx:GridViewDataColumn FieldName="Speciality" Caption="Speciality" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    
                                                    </dx:GridViewDataColumn>
                                                    <%--3--%>
                                                     <dx:GridViewDataColumn FieldName="Visit Date" Caption="Visit Date" 
                                                      HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    </dx:GridViewDataColumn>
                                                    <%--4--%>
                                                    <dx:GridViewDataColumn HeaderStyle-HorizontalAlign="Center"
                                                     FieldName="Bill_date" Caption="Bill Date"   Visible="true" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                      <%--5--%>
                                                    <dx:GridViewDataColumn FieldName="Bill_Status" Caption="Bill Status" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="true" HeaderStyle-Font-Bold="true">
                                                    </dx:GridViewDataColumn>
                                                    <%--6--%>
                                                    <dx:GridViewDataColumn FieldName="Write_off" Caption="Write Off" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                       <%--7--%>
                                                    <dx:GridViewDataColumn FieldName="Bill_Amount" Caption="Bill Amount" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                       <%--8--%>
                                                                                                         
                                                    <dx:GridViewDataColumn FieldName="Paid" Caption="Paid" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                     <%--10--%>
                                                    <dx:GridViewDataColumn FieldName="Outstanding" Caption="Outstanding" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                     <%--11--%>
                                                    <dx:GridViewDataColumn FieldName="Insurance" Caption="Insurance" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                      <%--12--%>
                                                    
                                                    <dx:GridViewDataColumn FieldName="Make_Payment" Caption="Make Payment" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                      <%--13--%>
                                                    <dx:GridViewDataColumn FieldName="" Caption="" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                     <%--13--%>
                                                    <dx:GridViewDataColumn FieldName="Verification" Caption="Verification" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>

                                                      <%--14--%>
                                                    <dx:GridViewDataColumn FieldName="Denial" Caption="Denial" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
                                                    </dx:GridViewDataColumn>
                                                    <%--15--%>
                                                    <dx:GridViewDataColumn Caption="" Settings-AllowSort="False" Width="25px">
                                                    
                                                      </dx:GridViewDataColumn>
                                                      
                                                      <dx:GridViewDataColumn FieldName="bill" Caption="" HeaderStyle-HorizontalAlign="Center"
                                                        Settings-AllowSort="False" Width="28px" HeaderStyle-Font-Bold="true" >
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

