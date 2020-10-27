<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Billing_Provider_Address.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_Billing_Provider_Address" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, 
PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
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



<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

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
    
    
    function unselectcheckboxforcom(obj)
    {
     var x=document.getElementById(obj);
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
    
            
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <%--  <div style="background-color: #E9E9E9; width: 950px; height: 750px; margin-top: 10px;
            margin-left: 150px;">--%>
        <table style="width: 95%; background-color: White; margin-left: 23px; border: 2px;"
            border="0">
            <tr>
                <td style="vertical-align: top; width: 100%;">
                    <table style="width: 100%; border: 0px solid; margin-top: 20px; padding-left: 0px;"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; border-bottom: 0px solid #BABABA; padding-left: 0px;" align="center">
                                <dx:ASPxGridView ID="grdbillingprovider" runat="server" Width="99%" SettingsBehavior-AllowSort="false"
                                    SettingsPager-PageSize="20" OnRowCommand="grdbillingprovider_OnRowCommand" ClientInstanceName="grdbillingprovider"
                                    KeyFieldName="SZ_CASE_TYPE_ID" AutoGenerateColumns="False">
                                    <Columns>
                                        <dx:GridViewDataHyperLinkColumn Caption="Case Type Name" FieldName="SZ_CASE_TYPE_NAME"
                                            Name="SZ_CASE_TYPE_NAME" VisibleIndex="0">
                                            <Settings AllowAutoFilter="True" AutoFilterCondition="Contains" />
                                            <DataItemTemplate>
                                                <asp:LinkButton ID="lnkPatientInfo123" runat="server" Text='<%#Eval("SZ_CASE_TYPE_NAME")%>'
                                                    CommandName="Select">
                                                </asp:LinkButton>
                                            </DataItemTemplate>
                                        </dx:GridViewDataHyperLinkColumn>
                                        <dx:GridViewDataColumn FieldName="SZ_CASE_TYPE_ID" Caption="SZ_CASE_TYPE_ID" HeaderStyle-HorizontalAlign="Center"
                                            Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <Styles>
                                        <%--<FocusedRow BackColor="#8C001A" ForeColor="#FFFFFF">
                                        </FocusedRow>--%>
                                        <%-- <AlternatingRow Enabled="True" BackColor="#E3E4E7">
                                        </AlternatingRow>--%>
                                    </Styles>
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%" class="SectionDevider">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%" class="SectionDevider">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 48%;" valign="top" id="tbldefaultgaddress" runat="server">
                                            <table border="0">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lblprovideraddress" runat="server" Text="Default Addresss" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblName" runat="server" Text="Provider Name" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtOffice" runat="server" CssClass="textboxCSS" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblAddress" runat="server" Text="Provider Address" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtOfficeAdd" runat="server" CssClass="textboxCSS" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblCity" runat="server" Text="Provider City" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblState" runat="server" Text="Provider State" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblZip" runat="server" Text="Provider Zip" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtOfficeCity" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <cc1:ExtendedDropDownList ID="extddlOfficeState" runat="server" Width="100%" Selected_Text="--- Select ---"
                                                            Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                                                        </cc1:ExtendedDropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtOfficeZip" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFax" runat="server" Text="Provider Fax" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblPhone" runat="server" Text="Provider Phone" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtFax" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtOfficePhone" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 4%;">
                                        </td>
                                        <td style="width: 48%;" valign="top" id="tblbillingaddress" runat="server">
                                            <table border="0">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblbillingaddress" runat="server" Text="Billing Address" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblbillingname" runat="server" Text="Provider Name" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtproviderbillingname" runat="server" CssClass="textboxCSS" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblBillingProviderAddrress" runat="server" Text="Provider Address"
                                                            Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtbillingprovideraddress" runat="server" CssClass="textboxCSS"
                                                            Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblbillingprovidercity" runat="server" Text="Provider City" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblbillingproviderstate" runat="server" Text="Provider State" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblbillingproviderzip" runat="server" Text="Provider Zip" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtbillingprovidercity" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <cc1:ExtendedDropDownList ID="extddlBillingProviderState" runat="server" Width="100%"
                                                            Selected_Text="--- Select ---" Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE"
                                                            Connection_Key="Connection_String"></cc1:ExtendedDropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtbillingproviderzip" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblbillingproviderfax" runat="server" Text="Provider Fax" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblbillingproviderphone" runat="server" Text="Provider Phone" Font-Bold="true"
                                                            Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtbillingproviderfax" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtbillingproviderphone" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td align="right">
                                                        <dx:ASPxButton ID="btnAdd" runat="server" Text="Add" OnClick="btnSave_Click">
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td align="left">
                                                        <dx:ASPxButton ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click">
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtofficeid" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtcasetypeid" runat="server" Visible="false"></asp:TextBox>
        <%--</div>--%>
    </form>
</body>
</html>
